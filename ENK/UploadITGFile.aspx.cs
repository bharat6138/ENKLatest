using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ENK.ServiceReference1;
using System.ServiceModel;
using System.IO;
using System.Text;
using System.Data.OleDb;
using System.Data.SqlClient;


namespace ENK
{
    public partial class UploadITGFile : System.Web.UI.Page
    {
        Service1Client ssc = new Service1Client(); 
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                //DataSet dttest = new DataSet();
                //dttest = WriteH2ORequest.H2ORecharge("19514197666", "20", "uday.v@virtuzo.in");
                lblStatus.InnerText = "Status : No Action Performed!";
            }
        }

        protected void UploadFile(FileUpload FuBulkDetails)
        {
            try
            {
                string filename = "";
                if (FuBulkDetails.HasFile)
                {
                    if (FuBulkDetails.FileName.Contains(".csv"))
                    {
                        string dirPath = Server.MapPath(string.Format("~/{0}", "ITGFile"));


                        if (!Directory.Exists(dirPath))
                        {
                            Directory.CreateDirectory(dirPath);
                        }

                        string path = Server.MapPath("~/ITGFile/" + FileUpload1.FileName);
                        //saving the file inside the MyFolder of the server
                        FuBulkDetails.SaveAs(path);
                        ViewState["PathURL"] = path;
                        ViewState["objDTfleet"] = CSVTODatTable(path, (DataTable)ViewState["objDTfleet"]);
                        DataTable dt = new DataTable();
                        dt = (DataTable)ViewState["objDTfleet"];

                        SaveData(dt, filename);
                        
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, Page.GetType(), "File Column Mismatch1", "alert('Please upload only .csv file')", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, Page.GetType(), "File Column Mismatch2", "alert('Please select file for upload')", true);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private DataTable CSVTODatTable(string CSVPath, DataTable table)
        {
            try
            {
                int iInvalidFileFormat = 0;
                StreamReader DataStreamReader = new StreamReader(CSVPath);
                string[] columnNames = DataStreamReader.ReadLine().Split(new string[] { "," }, StringSplitOptions.None);
                table = new DataTable("objDTfleet");
                DataColumn column = null;
                for (int i = 0; i < columnNames.Length; i++)
                {
                    column = new DataColumn(columnNames[i]);
                    table.Columns.Add(column);

                }
                // table.Columns.Add("SIMTypeID");


                while (!DataStreamReader.EndOfStream)
                {
                    int iRowIndex = table.Rows.Count;
                    if (columnNames.Length != 4)
                    {
                        //lblMsg.Text = "Some column mistmatch in mobile bulk upload file";
                        iInvalidFileFormat = 1;
                        ScriptManager.RegisterStartupScript(this, Page.GetType(), "File Column Mismatch", "alert('please check your file')", true);
                        break;
                    }
                    table.Rows.Add(DataStreamReader.ReadLine().Split(new string[] { "," }, StringSplitOptions.None));



                    if (iInvalidFileFormat == 1)
                    {
                        return null;
                    }
                    table.AcceptChanges();


                }
                DataStreamReader.Close();
                return table;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void SaveData(DataTable dtITRtable, string filename)
        {
            if (dtITRtable.Rows.Count > 0)
            {
                int retval = ssc.SaveITRFile(dtITRtable, filename);

                if (retval > 0)
                {
                    ShowPopUpMessage("Updated Successfully.");
                    lblStatus.InnerText = "Status : Step 1 Completed!";
                    //tr1.BgColor = "#4a8af4";
                    btnUpload.Enabled = false;
                    btnPostToITGReport.Visible = true;
                }
                else
                {
                    lblStatus.InnerText = "Status : Step 1 Failed! (Please Check File)";
                }
            }
            else
            {                    
                return;
            }
        }

        private void ShowPopUpMessage(string Message)
        {
            System.Text.StringBuilder javaScript = new System.Text.StringBuilder();
            string scriptKey = "ConfirmationScript";
            javaScript.Append("var userConfirmation = window.alert('" + Message + "');\n");
            ClientScript.RegisterStartupScript(this.GetType(), scriptKey, javaScript.ToString(), true);
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            try
            {
                string filename = "";

                DataTable dt = new DataTable();
                dt.TableName = "dtITRtable";
                {
                    //Upload and save the file
                    string csvPath = Server.MapPath("~/Files/") + Path.GetFileName(FileUpload1.PostedFile.FileName);
                    FileUpload1.SaveAs(csvPath);

                    filename = Convert.ToString(FileUpload1.PostedFile.FileName);

                    //Create a DataTable.                
                    dt.Columns.AddRange(new DataColumn[17] 
                    { 
                        new DataColumn("TopupSeq", typeof(string)),
                        new DataColumn("MSISDN", typeof(string)),
                        new DataColumn("Pinnumber", typeof(string)),
                        new DataColumn("TopupDate", typeof(string)),
                        new DataColumn("face_value", typeof(string)),
                        new DataColumn("Iccid", typeof(string)),
                        new DataColumn("bundlecode", typeof(string)),
                        new DataColumn("Operation_code", typeof(string)),
                        new DataColumn("Recharge_type", typeof(string)),
                        new DataColumn("Payment_Mode", typeof(string)),
                        new DataColumn("planid", typeof(string)),
                        new DataColumn("iccidprefix", typeof(string)),
                        new DataColumn("brand", typeof(string)),
                        new DataColumn("Bundle_name", typeof(string)),
                        new DataColumn("Bundle_validity", typeof(string)),
                        new DataColumn("FIRST_ICCID", typeof(string)),
                        new DataColumn("reseller_id",typeof(string)) 
                    });
           
                    //Read the contents of CSV file.
                    string csvData = File.ReadAllText(csvPath);

                    //Execute a loop over the rows.
                    foreach (string row in csvData.Split('\n'))
                    {
                        if (!string.IsNullOrEmpty(row))
                        {
                            dt.Rows.Add();
                            int i = 0;

                            //Execute a loop over the columns.
                            foreach (string cell in row.Split(','))
                            {
                                dt.Rows[dt.Rows.Count - 1][i] = cell;
                                i++;
                            }
                        }
                    }

                }

                SaveData(dt, filename);

                
            }
            catch (Exception)
            {                
                
            }
        }

        protected void btnPostToITGReport_Click(object sender, EventArgs e)
        {
            try
            {
                int _retval = ProcessingManualCommission("STEP 2");
                lblStatus.InnerText = "Status : Step 2 Completed!";
                btnPostToITGReport.Enabled = false;
                btnInventoryNotExists.Visible = true;
            }
            catch (Exception)
            {
                lblStatus.InnerText = "Status : Step 2 Failed!";
            }
        }

        protected void btnInventoryNotExists_Click(object sender, EventArgs e)
        {
            try
            {
                int _retval = ProcessingManualCommission("STEP 3");
                lblStatus.InnerText = "Status : Step 3 Completed!";
                btnInventoryNotExists.Enabled = false;
                btnPreloadedSIM.Visible = true;
            }
            catch (Exception)
            {
                lblStatus.InnerText = "Status : Step 3 Failed!";
            }
        }

        protected void btnPreloadedSIM_Click(object sender, EventArgs e)
        {
            try
            {
                int _retval = ProcessingManualCommission("STEP 4");
                lblStatus.InnerText = "Status : Step 4 Completed!";
                btnPreloadedSIM.Enabled = false;
                btnCheckTopupSequence.Visible = true;
            }
            catch (Exception)
            {
                lblStatus.InnerText = "Status : Step 4 Failed!";
            }
        }

        protected void btnCheckTopupSequence_Click(object sender, EventArgs e)
        {
            try
            {
                int _retval = ProcessingManualCommission("STEP 5");
                lblStatus.InnerText = "Status : Step 5 Completed!";
                btnCheckTopupSequence.Enabled = false;
                Button3.Visible = true;
            }
            catch (Exception)
            {
                lblStatus.InnerText = "Status : Step 5 Failed!";
            }
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            try
            {
                int _retval = ProcessingManualCommission("STEP 6");
                lblStatus.InnerText = "Status : Step 6 Completed!";
                Button3.Enabled = false;
                Button4.Visible = true;
            }
            catch (Exception)
            {
                lblStatus.InnerText = "Status : Step 6 Failed!";
            }
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            try
            {
                int _retval = ProcessingManualCommission("STEP 7");
                lblStatus.InnerText = "Status : Step 7 Completed!";
                Button4.Enabled = false;
                Button5.Visible = true;
            }
            catch (Exception)
            {
                lblStatus.InnerText = "Status : Step 7 Failed!";
            }
        }

        protected void Button5_Click(object sender, EventArgs e)
        {
            try
            {
                int _retval = ProcessingManualCommission("STEP 8");
                lblStatus.InnerText = "Status : Step 8 Completed!";
                Button5.Enabled = false;
                Button6.Visible = true;
            }
            catch (Exception)
            {
                lblStatus.InnerText = "Status : Step 8 Failed!";
            }
        }

        protected void Button6_Click(object sender, EventArgs e)
        {
            try
            {
                int _retval = ProcessingManualCommission("STEP 9");
                lblStatus.InnerText = "Status : Step 9 Completed!";
                Button6.Enabled = false;
                Button7.Visible = true;
            }
            catch (Exception)
            {
                lblStatus.InnerText = "Status : Step 9 Failed!";
            }
        }

        protected void Button7_Click(object sender, EventArgs e)
        {
            try
            {
                int _retval = ProcessingManualCommission("STEP 10");
                lblStatus.InnerText = "Status : Step 10 Completed!";
                Button7.Enabled = false;
                Button8.Visible = true;
            }
            catch (Exception)
            {
                lblStatus.InnerText = "Status : Step 10 Failed!";
            }
        }

        protected void Button8_Click(object sender, EventArgs e)
        {
            try
            {
                int _retval = ProcessingManualCommission("STEP 11");
                lblStatus.InnerText = "Status : Step 11 Completed!";
                Button8.Enabled = false;
                Button9.Visible = true;
            }
            catch (Exception)
            {
                lblStatus.InnerText = "Status : Step 11 Failed!";
            }
        }

        protected void Button9_Click(object sender, EventArgs e)
        {
            try
            {
                int _retval = ProcessingManualCommission("STEP 12");
                lblStatus.InnerText = "Status : Step 12 Completed!";
                Button9.Enabled = false;
                Button10.Visible = true;
            }
            catch (Exception)
            {
                lblStatus.InnerText = "Status : Step 12 Failed!";
            }
        }

        protected void Button10_Click(object sender, EventArgs e)
        {
            try
            {
                int _retval = ProcessingManualCommission("STEP 13");
                lblStatus.InnerText = "Status : Step 13 Completed!";
                Button10.Enabled = false;
                Button1.Visible = true;
            }
            catch (Exception)
            {
                lblStatus.InnerText = "Status : Step 13 Failed!";
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                int _retval = ProcessingManualCommission("STEP 14");
                lblStatus.InnerText = "Status : Step 14 Completed!";
                Button1.Enabled = false;
                Button2.Visible = true;
            }
            catch (Exception)
            {
                lblStatus.InnerText = "Status : Step 14 Failed!";
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            try
            {
                int _retval = ProcessingManualCommission("STEP 15");
                lblStatus.InnerText = "Status : Step 15 Completed!";
                Button2.Enabled = false;
                Button11.Visible = true;
            }
            catch (Exception)
            {
                lblStatus.InnerText = "Status : Step 15 Failed!";
            }
        }

        protected void Button11_Click(object sender, EventArgs e)
        {
            try
            {
                int _retval = ProcessingManualCommission("STEP 16");
                lblStatus.InnerText = "Status : Step 16 Completed!";
                Button11.Enabled = false;
                Button12.Visible = true;
            }
            catch (Exception)
            {
                lblStatus.InnerText = "Status : Step 16 Failed!";
            }
        }

        protected void Button12_Click(object sender, EventArgs e)
        {
            try
            {
                int _retval = ProcessingManualCommission("STEP 17");
                lblStatus.InnerText = "Status : Step 17 Completed!";
                Button12.Enabled = false;
                Button13.Visible = true;
            }
            catch (Exception)
            {
                lblStatus.InnerText = "Status : Step 17 Failed!";
            }
        }

        protected void Button13_Click(object sender, EventArgs e)
        {
            try
            {
                int _retval = ProcessingManualCommission("STEP 18");
                lblStatus.InnerText = "Status : Step 18 Completed!";
                Button13.Enabled = false;
                btnPostToProcessCommissionTable.Visible = true;
            }
            catch (Exception)
            {
                lblStatus.InnerText = "Status : Step 18 Failed!";
            }
        }

        protected void btnPostToProcessCommissionTable_Click(object sender, EventArgs e)
        {
            try
            {
                int _retval = ProcessingManualCommission("STEP 19");
                lblStatus.InnerText = "Status : Step 19 Completed!";
                btnPostToProcessCommissionTable.Enabled = false;
                btnProcessCommission.Visible = true;
            }
            catch (Exception)
            {
                lblStatus.InnerText = "Status : Step 19 Failed!";
            }
        }

        protected void btnProcessCommission_Click(object sender, EventArgs e)
        {
            try
            {
                int _retval = ProcessingManualCommission("STEP 20");
                lblStatus.InnerText = "Status : Step 20 Completed!";
                btnProcessCommission.Enabled = false;
                btnPostToAfterCommissionProcess.Visible = true;
            }
            catch (Exception)
            {
                lblStatus.InnerText = "Status : Step 20 Failed!";
            }
        }

        protected void btnPostToAfterCommissionProcess_Click(object sender, EventArgs e)
        {
            try
            {
                int _retval = ProcessingManualCommission("STEP 21");
                lblStatus.InnerText = "Status : Step 21 Completed!";
                btnPostToAfterCommissionProcess.Enabled = false;
                btnTopUpManualCommission_June.Visible = true;
            }
            catch (Exception)
            {
                lblStatus.InnerText = "Status : Step 21 Failed!";
            }
        }

        protected void btnTopUpManualCommission_June_Click(object sender, EventArgs e)
        {
            try
            {
                int _retval = ProcessingManualCommission("STEP 22");
                lblStatus.InnerText = "Status : Step 22 Completed!";
                btnTopUpManualCommission_June.Enabled = false;
                btnInsertToSIMActivation.Visible = true;
            }
            catch (Exception)
            {
                lblStatus.InnerText = "Status : Step 22 Failed!";
            }
        }

        protected void btnInsertToSIMActivation_Click(object sender, EventArgs e)
        {
            try
            {
                int _retval = ProcessingManualCommission("STEP 23");
                lblStatus.InnerText = "Status : Step 23 Completed!";
                btnInsertToSIMActivation.Enabled = false;
            }
            catch (Exception)
            {
                lblStatus.InnerText = "Status : Step 23 Failed!";
            }
        }

        private int ProcessingManualCommission(string stepNo)
        {
            try
            {
                int retval = ssc.ProcessingManualCommission(stepNo);

                return retval;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

    }
}