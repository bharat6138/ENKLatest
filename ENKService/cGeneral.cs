using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System;
using System.Web;
using System.Data;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net;
using System.Net.Mail;

namespace ENKService
{

    public class cGeneral
    {

        public static string SendRequest(string strURI, string data)
        {
            string _result;
            _result = " ";
            try
            {
                var request = (HttpWebRequest)WebRequest.Create(strURI);
                request.Method = "POST";
                request.ContentType = "text/xml";
                var writer = new StreamWriter(request.GetRequestStream());
                writer.Write(data);
                writer.Close();

                var response = (HttpWebResponse)request.GetResponse();
                var streamResponse = response.GetResponseStream();
                var streamRead = new StreamReader(streamResponse);

                _result = streamRead.ReadToEnd().Trim();

                streamRead.Close();
                streamResponse.Close();
                response.Close();

                return _result;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public static void LogSteps(String strInput, String LogFilePath)
        {
            try
            {
                using (System.IO.StreamWriter w = System.IO.File.AppendText(LogFilePath))
                {
                    w.WriteLine(DateTime.UtcNow.ToString("dd-MMM-yyyy HH:mm:ss").ToString() + "|" + strInput);
                }


            }
            catch
            {
                // in case of exception occured in log
                //Server.ClearError();

            }

        }

        public static void CreateJsonTextfile(String strInput, String LogFilePath)
        {
            try
            {
                using (System.IO.StreamWriter w = System.IO.File.AppendText(LogFilePath))
                {
                    w.Write(strInput);
                }


            }
            catch
            {
                // in case of exception occured in log
                //Server.ClearError();

            }

        }

        public static String ReadJsonTextfile(String FilePath)
        {
            try
            {
                using (System.IO.StreamReader r = new StreamReader(FilePath))
                {

                    String line = r.ReadToEnd();
                    return line;
                }


            }
            catch (Exception ex)
            {
                // in case of exception occured in log
                //Server.ClearError();
                throw ex;
            }

        }


       
        public static string GetJson(DataTable dt)
        {
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new

            System.Web.Script.Serialization.JavaScriptSerializer();
            List<Dictionary<string, object>> rows =
              new List<Dictionary<string, object>>();
            Dictionary<string, object> row = null;

            foreach (DataRow dr in dt.Rows)
            {
                row = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    row.Add(col.ColumnName.Trim(), dr[col]);
                }
                rows.Add(row);
            }
            return serializer.Serialize(rows);
        }


        public static string ReadStream(System.IO.Stream strInput)
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

        

    }

}