//using System.Net.Http;
//using System.Web.Http;
using Newtonsoft.Json;
using System.Data;
using Newtonsoft.Json.Linq;
using System.Text;
using System.Web.Routing;
using System.Configuration;
using System.Net;
using System;
using System.Data.SqlClient;
using System.Web;
using System.IO;
using System.Xml;
using System.Linq;
using Microsoft.SqlServer;
using System.Security.Cryptography;
using System.Diagnostics;

namespace ENK
{
    public class WriteH2ORequest
    {
        public static string ProcessString="";

        public static DataSet H2OActivation(string ICCID, string plancode, string locationcode, string cityname, string emailid)
        {

            int flag = 0;
            ProcessString = "";

            DataTable rsp = new DataTable();

            string str12 = @"D:\Projects\API_test_H2o\H2O\Requests\" + ICCID + ".txt";

            if (File.Exists(@"D:\Projects\API_test_H2o\H2O\Complete\" + ICCID + "_processed.txt"))
            {
                rsp = ReadRequest(@"D:\Projects\API_test_H2o\H2O\Processing\" + ICCID + "_processed.txt");

                if (rsp.Rows.Count > 0)
                {
                    flag = 1;
                }
                else
                {
                    flag = 0;
                }
            }
            else
            {
               //RunH2OEXE
                Log("Activation");

                if (str12 != null && str12.ToString() != "")
                {
                    File.Delete(str12);
                }
                WriteRequest(plancode + "::" + locationcode + "::" + cityname + "::" + emailid
                    , (@"D:\Projects\API_test_H2o\H2O\Requests\" + ICCID + ".txt"));

                int numberOfColumns = 5;
                string filePath = @"D:\Projects\API_test_H2o\H2O\Processing\" + ICCID + "_processed.txt";

                DateTime PreviousDateTime = DateTime.Now;

                TimeSpan diff;
                double seconds = 0;

                while (rsp.Rows.Count == 0 && seconds <= 120)
                {
                    diff = DateTime.Now - PreviousDateTime;
                    seconds = diff.TotalSeconds;
                    rsp = ReadRequest(@"D:\Projects\API_test_H2o\H2O\Processing\" + ICCID + "_processed.txt");

                    if(rsp.Rows.Count > 0)
                    {
                        flag = 1;
                    }
                    else
                    {
                        flag = 0;
                    }
                }
            }
            if (flag == 1)
            {
                DataSet theDataSet = new DataSet();
                DataTable dt = new DataTable();

                if (Convert.ToString(rsp.Rows[0]["Column5"]) != null && (Convert.ToString(rsp.Rows[0]["Column5"]) != "0"))
                {
                    string CompleteFile = @"D:\Projects\API_test_H2o\H2O\Complete\" + ICCID + "_Complete.txt";

                    WriteComletion(CompleteFile);
                    File.Delete(@"D:\Projects\API_test_H2o\H2O\Processing\" + ICCID + "_processed.txt");
                    
                    dt.TableName = "Response";
                    dt.Columns.Add("ERROR_CODE");
                    dt.Columns.Add("ERROR_DESC");
                    dt.AcceptChanges();
                    DataRow dr;
                    dr = dt.Rows.Add();
                    dr["ERROR_CODE"] = "E0100";
                    dr["ERROR_DESC"] = "Success";
                    dt.AcceptChanges();
                    theDataSet.Tables.Add(dt);

                    DataTable re = new DataTable();
                    re.TableName = "Details";
                    re.Columns.Add("OrderNumber");
                    re.Columns.Add("PhoneNumber");
                    re.Columns.Add("TransactionNumber");
                    re.Columns.Add("OrderDateTime");
                    re.AcceptChanges();
                    DataRow dr1;
                    dr1 = re.Rows.Add();
                    dr1["OrderNumber"] = rsp.Rows[0]["Column1"];
                    dr1["PhoneNumber"] = rsp.Rows[0]["Column2"];
                    dr1["TransactionNumber"] = rsp.Rows[0]["Column3"];
                    dr1["OrderDateTime"] = rsp.Rows[0]["Column4"];
                    re.AcceptChanges();
                    theDataSet.Tables.Add(re);
                }
                else
                {
                    string CompleteFile = @"D:\Projects\API_test_H2o\H2O\Complete\" + ICCID + "_Complete.txt";

                    //Selenium Error Tracking.
                    string _errordesc = "";
                    string _error = Convert.ToString(rsp.Rows[0]["Column6"]);

                    if (_error == "5")
                    {
                        _errordesc = "Sim Already Activated, ErrorCode: 005";
                    }
                    else
                    {
                        _errordesc = "Something Went Wrong, ErrorCode: 00" + _error;
                    }

                    WriteComletion(CompleteFile);
                    File.Delete(@"D:\Projects\API_test_H2o\H2O\Processing\" + ICCID + "_processed.txt");

                    dt.TableName = "Response";
                    dt.Columns.Add("ERROR_CODE");
                    dt.Columns.Add("ERROR_DESC");
                    dt.AcceptChanges();
                    DataRow dr;
                    dr = dt.Rows.Add();
                    dr["ERROR_CODE"] = "E0100";
                    dr["ERROR_DESC"] = _errordesc;
                    dt.AcceptChanges();
                    theDataSet.Tables.Add(dt);

                    DataTable re = new DataTable();
                    re.TableName = "Details";
                    re.Columns.Add("OrderNumber");
                    re.Columns.Add("PhoneNumber");
                    re.Columns.Add("TransactionNumber");
                    re.Columns.Add("OrderDateTime");
                    re.AcceptChanges();
                    DataRow dr1;
                    dr1 = re.Rows.Add();
                    dr1["OrderNumber"] = rsp.Rows[0]["Column1"];
                    dr1["PhoneNumber"] = rsp.Rows[0]["Column2"];
                    dr1["TransactionNumber"] = rsp.Rows[0]["Column3"];
                    dr1["OrderDateTime"] = rsp.Rows[0]["Column4"];
                    re.AcceptChanges();
                    theDataSet.Tables.Add(re);
                }                

                string json = JsonConvert.SerializeObject(theDataSet, Newtonsoft.Json.Formatting.None);
                return theDataSet;
            }
            else
            {
                DataSet theDataSet = new DataSet();
                DataTable dt = new DataTable();
                dt.TableName = "Response";
                dt.Columns.Add("ERROR_CODE");
                dt.Columns.Add("ERROR_DESC");
                dt.AcceptChanges();
                DataRow dr;
                dr = dt.Rows.Add();
                dr["ERROR_CODE"] = "E0100";
                dr["ERROR_DESC"] = "Processed File Not Found with ICCID " + ICCID;
                dt.AcceptChanges();
                theDataSet.Tables.Add(dt);
                string json = JsonConvert.SerializeObject(theDataSet, Newtonsoft.Json.Formatting.None);
                return theDataSet;
            }
        }

        public static DataSet H2OPortIn(string ICCID, string plancode, string AccountNumber, string PIN, string ServiceProvider, string FirstName
            , string LastName, string Address, string City, string State, string ZipCode, string SIMNumber, string EmailID)
        {

            int flag = 0;
            ProcessString = "";

            DataTable rsp = new DataTable();

            string str12 = @"D:\Projects\API_test_H2o\H2O\Requests\" + ICCID + ".txt";

            if (File.Exists(@"D:\Projects\API_test_H2o\H2O\Complete\" + ICCID + "_processed.txt"))
            {
                rsp = ReadRequest(@"D:\Projects\API_test_H2o\H2O\Processing\" + ICCID + "_processed.txt");

                if (rsp.Rows.Count > 0)
                {
                    flag = 1;
                }
                else
                {
                    flag = 0;
                }
            }
            else
            {
                //RunH2OEXE
                Log("Portin");

                if (str12 != null && str12.ToString() != "")
                {
                    File.Delete(str12);
                }
                WriteRequest(plancode + "::" + AccountNumber + "::" + PIN + "::" + ServiceProvider + "::" + FirstName + "::" + LastName + "::" + Address + "::" + City + "::" + State + "::" + ZipCode + "::" + SIMNumber + "::" + EmailID
                    , (@"D:\Projects\API_test_H2o\H2O\Requests\" + ICCID + ".txt"));

                int numberOfColumns = 5;
                string filePath = @"D:\Projects\API_test_H2o\H2O\Processing\" + ICCID + "_processed.txt";

                DateTime PreviousDateTime = DateTime.Now;

                TimeSpan diff;
                double seconds = 0;

                while (rsp.Rows.Count == 0 && seconds <= 120)
                {
                    diff = DateTime.Now - PreviousDateTime;
                    seconds = diff.TotalSeconds;
                    rsp = ReadRequest(@"D:\Projects\API_test_H2o\H2O\Processing\" + ICCID + "_processed.txt");

                    if (rsp.Rows.Count > 0)
                    {
                        flag = 1;
                    }
                    else
                    {
                        flag = 0;
                    }
                }
            }
            if (flag == 1)
            {
                DataSet theDataSet = new DataSet();
                DataTable dt = new DataTable();

                if (Convert.ToString(rsp.Rows[0]["Column5"]) != null && (Convert.ToString(rsp.Rows[0]["Column5"]) != "0"))
                {
                    string CompleteFile = @"D:\Projects\API_test_H2o\H2O\Complete\" + ICCID + "_Complete.txt";

                    WriteComletion(CompleteFile);
                    File.Delete(@"D:\Projects\API_test_H2o\H2O\Processing\" + ICCID + "_processed.txt");

                    dt.TableName = "Response";
                    dt.Columns.Add("ERROR_CODE");
                    dt.Columns.Add("ERROR_DESC");
                    dt.AcceptChanges();
                    DataRow dr;
                    dr = dt.Rows.Add();
                    dr["ERROR_CODE"] = "E0100";
                    dr["ERROR_DESC"] = "Success";
                    dt.AcceptChanges();
                    theDataSet.Tables.Add(dt);

                    DataTable re = new DataTable();
                    re.TableName = "Details";
                    re.Columns.Add("OrderNumber");
                    re.Columns.Add("PhoneNumber");
                    re.Columns.Add("TransactionNumber");
                    re.Columns.Add("OrderDateTime");
                    re.AcceptChanges();
                    DataRow dr1;
                    dr1 = re.Rows.Add();
                    dr1["OrderNumber"] = rsp.Rows[0]["Column1"];
                    dr1["PhoneNumber"] = rsp.Rows[0]["Column2"];
                    dr1["TransactionNumber"] = rsp.Rows[0]["Column3"];
                    dr1["OrderDateTime"] = rsp.Rows[0]["Column4"];
                    re.AcceptChanges();
                    theDataSet.Tables.Add(re);
                }
                else
                {
                    string CompleteFile = @"D:\Projects\API_test_H2o\H2O\Complete\" + ICCID + "_Complete.txt";

                    //Selenium Error Tracking.
                    string _errordesc = "";
                    string _error = Convert.ToString(rsp.Rows[0]["Column6"]);

                    _errordesc = "Something Went Wrong, ErrorCode: 00" + _error;

                    WriteComletion(CompleteFile);
                    File.Delete(@"D:\Projects\API_test_H2o\H2O\Processing\" + ICCID + "_processed.txt");

                    dt.TableName = "Response";
                    dt.Columns.Add("ERROR_CODE");
                    dt.Columns.Add("ERROR_DESC");
                    dt.AcceptChanges();
                    DataRow dr;
                    dr = dt.Rows.Add();
                    dr["ERROR_CODE"] = "E0100";
                    dr["ERROR_DESC"] = _errordesc;
                    dt.AcceptChanges();
                    theDataSet.Tables.Add(dt);

                    DataTable re = new DataTable();
                    re.TableName = "Details";
                    re.Columns.Add("OrderNumber");
                    re.Columns.Add("PhoneNumber");
                    re.Columns.Add("TransactionNumber");
                    re.Columns.Add("OrderDateTime");
                    re.AcceptChanges();
                    DataRow dr1;
                    dr1 = re.Rows.Add();
                    dr1["OrderNumber"] = rsp.Rows[0]["Column1"];
                    dr1["PhoneNumber"] = rsp.Rows[0]["Column2"];
                    dr1["TransactionNumber"] = rsp.Rows[0]["Column3"];
                    dr1["OrderDateTime"] = rsp.Rows[0]["Column4"];
                    re.AcceptChanges();
                    theDataSet.Tables.Add(re);
                }

                string json = JsonConvert.SerializeObject(theDataSet, Newtonsoft.Json.Formatting.None);
                return theDataSet;
            }
            else
            {
                DataSet theDataSet = new DataSet();
                DataTable dt = new DataTable();
                dt.TableName = "Response";
                dt.Columns.Add("ERROR_CODE");
                dt.Columns.Add("ERROR_DESC");
                dt.AcceptChanges();
                DataRow dr;
                dr = dt.Rows.Add();
                dr["ERROR_CODE"] = "E0100";
                dr["ERROR_DESC"] = "Processed File Not Found with ICCID " + ICCID;
                dt.AcceptChanges();
                theDataSet.Tables.Add(dt);
                string json = JsonConvert.SerializeObject(theDataSet, Newtonsoft.Json.Formatting.None);
                return theDataSet;
            }
        }

        public static DataSet H2ORecharge(string ICCID, string plancode, string EmailID)
        {

            int flag = 0;
            ProcessString = "";

            DataTable rsp = new DataTable();

            string str12 = @"D:\Projects\API_test_H2o\H2O\Requests\" + ICCID + ".txt";

            if (File.Exists(@"D:\Projects\API_test_H2o\H2O\Complete\" + ICCID + "_processed.txt"))
            {
                rsp = ReadRequestRecharge(@"D:\Projects\API_test_H2o\H2O\Processing\" + ICCID + "_processed.txt");

                if (rsp.Rows.Count > 0)
                {
                    flag = 1;
                }
                else
                {
                    flag = 0;
                }
            }
            else
            {
                //RunH2OEXE
                //Log("Recharge");

                if (str12 != null && str12.ToString() != "")
                {
                    File.Delete(str12);
                }
                WriteRequest(plancode + "::" + EmailID
                    , (@"D:\Projects\API_test_H2o\H2O\Requests\" + ICCID + ".txt"));

                int numberOfColumns = 5;
                string filePath = @"D:\Projects\API_test_H2o\H2O\Processing\" + ICCID + "_processed.txt";

                DateTime PreviousDateTime = DateTime.Now;

                TimeSpan diff;
                double seconds = 0;

                while (rsp.Rows.Count == 0 && seconds <= 120)
                {
                    diff = DateTime.Now - PreviousDateTime;
                    seconds = diff.TotalSeconds;
                    rsp = ReadRequestRecharge(@"D:\Projects\API_test_H2o\H2O\Processing\" + ICCID + "_processed.txt");

                    if (rsp.Rows.Count > 0)
                    {
                        flag = 1;
                    }
                    else
                    {
                        flag = 0;
                    }
                }
            }
            if (flag == 1)
            {
                DataSet theDataSet = new DataSet();
                DataTable dt = new DataTable();

                if (Convert.ToString(rsp.Rows[0]["Column5"]) != null && (Convert.ToString(rsp.Rows[0]["Column5"]) != "0"))
                {
                    string CompleteFile = @"D:\Projects\API_test_H2o\H2O\Complete\" + ICCID + "_Complete.txt";

                    WriteComletion(CompleteFile);
                    File.Delete(@"D:\Projects\API_test_H2o\H2O\Processing\" + ICCID + "_processed.txt");

                    dt.TableName = "Response";
                    dt.Columns.Add("ERROR_CODE");
                    dt.Columns.Add("ERROR_DESC");
                    dt.AcceptChanges();
                    DataRow dr;
                    dr = dt.Rows.Add();
                    dr["ERROR_CODE"] = "E0100";
                    dr["ERROR_DESC"] = "Success";
                    dt.AcceptChanges();
                    theDataSet.Tables.Add(dt);

                    DataTable re = new DataTable();
                    re.TableName = "Details";
                    re.Columns.Add("OrderNumber");
                    re.Columns.Add("PhoneNumber");
                    re.Columns.Add("TransactionNumber");
                    re.Columns.Add("OrderDateTime");
                    re.AcceptChanges();
                    DataRow dr1;
                    dr1 = re.Rows.Add();
                    dr1["OrderNumber"] = rsp.Rows[0]["Column1"];
                    dr1["PhoneNumber"] = rsp.Rows[0]["Column2"];
                    dr1["TransactionNumber"] = rsp.Rows[0]["Column3"];
                    dr1["OrderDateTime"] = rsp.Rows[0]["Column4"];
                    re.AcceptChanges();
                    theDataSet.Tables.Add(re);
                }
                else
                {
                    string CompleteFile = @"D:\Projects\API_test_H2o\H2O\Complete\" + ICCID + "_Complete.txt";

                    //Selenium Error Tracking.
                    string _errordesc = "";
                    string _error = Convert.ToString(rsp.Rows[0]["Column6"]);

                    _errordesc = "Something Went Wrong, ErrorCode: 00" + _error;

                    WriteComletion(CompleteFile);
                    File.Delete(@"D:\Projects\API_test_H2o\H2O\Processing\" + ICCID + "_processed.txt");

                    dt.TableName = "Response";
                    dt.Columns.Add("ERROR_CODE");
                    dt.Columns.Add("ERROR_DESC");
                    dt.AcceptChanges();
                    DataRow dr;
                    dr = dt.Rows.Add();
                    dr["ERROR_CODE"] = "E0100";
                    dr["ERROR_DESC"] = _errordesc;
                    dt.AcceptChanges();
                    theDataSet.Tables.Add(dt);

                    DataTable re = new DataTable();
                    re.TableName = "Details";
                    re.Columns.Add("OrderNumber");
                    re.Columns.Add("PhoneNumber");
                    re.Columns.Add("TransactionNumber");
                    re.Columns.Add("OrderDateTime");
                    re.AcceptChanges();
                    DataRow dr1;
                    dr1 = re.Rows.Add();
                    dr1["OrderNumber"] = rsp.Rows[0]["Column1"];
                    dr1["PhoneNumber"] = rsp.Rows[0]["Column2"];
                    dr1["TransactionNumber"] = rsp.Rows[0]["Column3"];
                    dr1["OrderDateTime"] = rsp.Rows[0]["Column4"];
                    re.AcceptChanges();
                    theDataSet.Tables.Add(re);
                }

                string json = JsonConvert.SerializeObject(theDataSet, Newtonsoft.Json.Formatting.None);
                return theDataSet;
            }
            else
            {
                DataSet theDataSet = new DataSet();
                DataTable dt = new DataTable();
                dt.TableName = "Response";
                dt.Columns.Add("ERROR_CODE");
                dt.Columns.Add("ERROR_DESC");
                dt.AcceptChanges();
                DataRow dr;
                dr = dt.Rows.Add();
                dr["ERROR_CODE"] = "E0100";
                dr["ERROR_DESC"] = "Processed File Not Found with ICCID " + ICCID;
                dt.AcceptChanges();
                theDataSet.Tables.Add(dt);
                string json = JsonConvert.SerializeObject(theDataSet, Newtonsoft.Json.Formatting.None);
                return theDataSet;
            }
        }

        public static void WriteRequest(String strInput, String RequestFilePath)
        {
            try
            {
                using (System.IO.StreamWriter w = System.IO.File.AppendText(RequestFilePath))
                {
                    w.WriteLine(strInput);
                }
            }
            catch (Exception Ex)
            {

            }

        }

        public static DataTable ReadRequest(String RequestFilePath)
        {
            DataTable tbl = new DataTable();
            try
            {
                using (StreamReader sr = new StreamReader(RequestFilePath))
                {
                    while (sr.Peek() >= 0)
                    {
                        ProcessString += Convert.ToString(sr.ReadLine());
                    }
                }
                for (int col = 0; col < 6; col++)
                    tbl.Columns.Add(new DataColumn("Column" + (col + 1).ToString()));

                //var cols = ProcessString.Split(':');
                string[] FileData = ProcessString.Split(new[] { ";;" }, StringSplitOptions.None);
                string RequestDetails = FileData[0]; 
                string OrderDetails =FileData[1];

                string[] ResponseDetails = OrderDetails.Split(new[] { "::" }, StringSplitOptions.None);

                

                DataRow dr = tbl.NewRow();
                for (int cIndex = 0; cIndex < 6; cIndex++)
                {
                    if (cIndex == 5)
                    {
                        if (Convert.ToString(dr[cIndex - 1]) != "0")
                        {
                            dr[cIndex - 1] = "1";
                            dr[cIndex] = "8";
                        }
                        else
                        {
                            dr[cIndex] = ResponseDetails[cIndex];
                        }
                    }
                    else
                    {
                        dr[cIndex] = ResponseDetails[cIndex];
                    }
                }

                tbl.Rows.Add(dr);

                return tbl;

            }
            catch (Exception ex)
            {
                return tbl;
            }

        }

        public static DataTable ReadRequestRecharge(String RequestFilePath)
        {
            DataTable tbl = new DataTable();
            try
            {
                using (StreamReader sr = new StreamReader(RequestFilePath))
                {
                    while (sr.Peek() >= 0)
                    {
                        ProcessString += Convert.ToString(sr.ReadLine());
                    }
                }
                for (int col = 0; col < 6; col++)
                    tbl.Columns.Add(new DataColumn("Column" + (col + 1).ToString()));

                //var cols = ProcessString.Split(':');
                string[] FileData = ProcessString.Split(new[] { ";;" }, StringSplitOptions.None);
                string RequestDetails = FileData[0];
                string ResponseDetails = FileData[1];

                string[] ResponseDetailsArr = ResponseDetails.Split(new[] { "::" }, StringSplitOptions.None);

                int _arrLength = 0;
                int.TryParse(Convert.ToString(ResponseDetailsArr.Length), out _arrLength);

                if (_arrLength < 6)
                {
                    if (Convert.ToString(ResponseDetailsArr[_arrLength-1]) == "0")
                    {
                        return tbl;
                    }
                }

                DataRow dr = tbl.NewRow();
                for (int cIndex = 0; cIndex < 6; cIndex++)
                {
                    if (cIndex == 5)
                    {
                        if (Convert.ToString(dr[cIndex - 1]) != "0")
                        {
                            dr[cIndex - 1] = "1";
                            dr[cIndex] = "8";
                        }
                        else
                        {
                            dr[cIndex] = ResponseDetailsArr[cIndex];
                        }
                    }
                    else
                    {
                        dr[cIndex] = ResponseDetailsArr[cIndex];
                    }
                }

                tbl.Rows.Add(dr);

                return tbl;

            }
            catch (Exception ex)
            {
                return tbl;
            }

        }

        public static void WriteComletion(String RequestFilePath)
        {
            try
            {
                using (System.IO.StreamWriter w = System.IO.File.AppendText(RequestFilePath))
                {
                    w.WriteLine(ProcessString);
                }

            }
            catch
            {
                // in case of exception occured in log
                //Server.ClearError();

            }

        }

        public DataTable ConvertToDataTable(string filePath, int numberOfColumns)
        {

            DataTable tbl = new DataTable();

            for (int col = 0; col < numberOfColumns; col++)
                tbl.Columns.Add(new DataColumn("Column" + (col + 1).ToString()));


            string[] lines = System.IO.File.ReadAllLines(filePath);

            foreach (string line in lines)
            {
                var cols = line.Split(':');

                DataRow dr = tbl.NewRow();
                for (int cIndex = 0; cIndex < 3; cIndex++)
                {
                    dr[cIndex] = cols[cIndex];
                }

                tbl.Rows.Add(dr);
            }

            return tbl;
        }

        public static string ProcessedFile(System.IO.Stream strInput)
        {
            try
            {

                String strmContents;
                Int32 counter, strLen, strRead;

                // Find number of bytes in stream.
                strLen = Convert.ToInt32(strInput.Length);
                // Create a byte array.
                byte[] strArr = new byte[strLen];
                // Read stream into byte array.
                strRead = strInput.Read(strArr, 0, strLen);

                // Convert byte array to a text string.
                strmContents = "";
                for (counter = 0; counter < strLen; counter++)
                {
                    strmContents = strmContents + strArr[counter].ToString();
                }
                return strmContents;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
                
        public static int ExecuteCommand(string command, int timeout)
        {
            var processInfo = new ProcessStartInfo(command)
            {
                CreateNoWindow = true,
                UseShellExecute = false,
                WorkingDirectory = @"E:\Deepak Soni\csharp code\csharp\bin\Debug\WebDriverScraping.exe",
            };

            var process = Process.Start(processInfo);
            process.WaitForExit(timeout);
            var exitCode = process.ExitCode;
            process.Close();
            return exitCode;
        }
        
        static void Log(string action)
        {
            try
            {
                if (!EventLog.SourceExists("H2OService"))
                {
                    EventLog.CreateEventSource("H2OService", "Application");
                }

                EventLog.WriteEntry("H2OService", "New " + action + " Request", EventLogEntryType.Information);
            }
            catch (Exception)
            {

            }
        }

        public static DataSet H2OActivationTest(string ICCID, string plancode, string locationcode, string cityname, string emailid)
        {
            int flag = 0;

            DataTable rsp = new DataTable();

            string str12 = @"D:\Projects\API_test_H2o\H2O\Requests\" + ICCID + ".txt";

            if (File.Exists(@"D:\Projects\API_test_H2o\H2O\Complete\" + ICCID + "_processed.txt") && 1==2)
            {
                rsp = ReadRequest(@"D:\Projects\API_test_H2o\H2O\Processing\" + ICCID + "_processed.txt");

                if (rsp.Rows.Count > 0)
                {
                    flag = 1;
                }
                else
                {
                    flag = 0;
                }
            }
            else
            {
                //RunH2OEXE();
                if (str12 != null && str12.ToString() != "")
                {
                    File.Delete(str12);
                }
                //WriteRequest(plancode + "::" + locationcode + "::" + cityname + "::" + emailid, (@"D:\Projects\API_test_H2o\H2O\Requests\" + ICCID + ".txt"));

                int numberOfColumns = 5;
                string filePath = @"D:\Projects\API_test_H2o\H2O\Processing\" + ICCID + "_processed.txt";

                DateTime PreviousDateTime = DateTime.Now;

                TimeSpan diff;
                double seconds = 0;


                while (rsp.Rows.Count == 0 && seconds <= 120)
                {
                    diff = DateTime.Now - PreviousDateTime;
                    seconds = diff.TotalSeconds;
                    rsp = ReadRequest(@"D:\Projects\API_test_H2o\H2O\Processing\" + ICCID + "_processed.txt");

                    if (rsp.Rows.Count > 0)
                    {
                        flag = 1;
                    }
                    else
                    {
                        flag = 0;
                    }
                }
            }
            if (flag == 1)
            {
                string CompleteFile = @"D:\Projects\API_test_H2o\H2O\Complete\" + ICCID + "_Complete.txt";

                WriteComletion(CompleteFile);
                File.Delete(@"D:\Projects\API_test_H2o\H2O\Processing\" + ICCID + "_processed.txt");
                DataSet theDataSet = new DataSet();
                DataTable dt = new DataTable();
                dt.TableName = "Response";
                dt.Columns.Add("ERROR_CODE");
                dt.Columns.Add("ERROR_DESC");
                dt.AcceptChanges();
                DataRow dr;
                dr = dt.Rows.Add();
                dr["ERROR_CODE"] = "E0000";
                dr["ERROR_DESC"] = "Success";
                dt.AcceptChanges();
                theDataSet.Tables.Add(dt);

                DataTable re = new DataTable();
                re.TableName = "Details";
                re.Columns.Add("OrderNumber");
                re.Columns.Add("PhoneNumber");
                re.Columns.Add("TransactionNumber");
                re.Columns.Add("OrderDateTime");
                re.AcceptChanges();
                DataRow dr1;
                dr1 = re.Rows.Add();
                dr1["OrderNumber"] = rsp.Rows[0]["Column1"];
                dr1["PhoneNumber"] = rsp.Rows[0]["Column2"];
                dr1["TransactionNumber"] = rsp.Rows[0]["Column3"];
                dr1["OrderDateTime"] = rsp.Rows[0]["Column4"];
                re.AcceptChanges();
                theDataSet.Tables.Add(re);

                string json = JsonConvert.SerializeObject(theDataSet, Newtonsoft.Json.Formatting.None);

                return theDataSet;
            }
            else
            {
                DataSet theDataSet = new DataSet();
                DataTable dt = new DataTable();
                dt.TableName = "Response";
                dt.Columns.Add("ERROR_CODE");
                dt.Columns.Add("ERROR_DESC");
                dt.AcceptChanges();
                DataRow dr;
                dr = dt.Rows.Add();
                dr["ERROR_CODE"] = "E0100";
                dr["ERROR_DESC"] = "Processed File Not Found with ICCID " + ICCID;
                dt.AcceptChanges();
                theDataSet.Tables.Add(dt);
                string json = JsonConvert.SerializeObject(theDataSet, Newtonsoft.Json.Formatting.None);

                return theDataSet;
            }
        }
    }

}