using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Net.Mail;
using System.Configuration;
using ENK.ServiceReference1;
using ENK.net.emida.ws;
namespace ENK
{
    public partial class NetworkReplace : System.Web.UI.Page
    {      
        Service1Client svc = new Service1Client();
        DataTable dtfillGridSIMPurchase;
        DataTable dtfillGridMobileSIMPurchase;
       
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                   if (Session["LoginID"] != null)
                    {
                        DataTable dt = svc.GetVendor(Convert.ToInt32(Session["LoginId"]));
                        if (dt != null)
                        {
                            if (dt.Rows.Count > 0)
                            {
                                ddlNetwork.DataSource = dt;
                                ddlNetwork.DataValueField = "VendorID";
                                ddlNetwork.DataTextField = "VendorName";
                                ddlNetwork.DataBind();
                                ddlNetwork.Items.Insert(0, new ListItem("Select Network", "0"));
                            }
                        }

                        fillblankGridForMobileSIM();

                    }
                
            }
        }
        protected void txtSIMNumber_TextChanged(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = new DataSet();
                ds = svc.GetSimNetwork(Convert.ToString(txtSIMNumber.Text),"Activate");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    // 14 is Deactive Network(due to Error show)

                    if (ds.Tables[0].Rows[0]["VendorID"].ToString() != "14")
                    {
                       // hdnVendorID.Value = Convert.ToString(ds.Tables[0].Rows[0]["VendorID"]);
                        hdnPurchaseID.Value = Convert.ToString(ds.Tables[0].Rows[0]["purchaseid"]);
                        ddlNetwork.SelectedValue = Convert.ToInt32(ds.Tables[0].Rows[0]["VendorID"]).ToString();
                    }
                   else
                    {
                        hdnPurchaseID.Value = "0";
                        ddlNetwork.SelectedIndex = 0;

                    }
                }
                else
                {
                    hdnPurchaseID.Value = "0";
                    ddlNetwork.SelectedIndex = 0;
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }


        }

        protected void RepeaterMobileSIMPurchase_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Delete")
                {
                    if (ViewState["objDTMobile"] != null)
                    {
                        DataTable dt = (DataTable)ViewState["objDTMobile"];
                        DataRow drCurrentRow = null;

                        int rowIndex = Convert.ToInt32(e.Item.ItemIndex);

                        if (dt.Rows.Count > 1)
                        {
                            dt.Rows.Remove(dt.Rows[rowIndex]);
                            drCurrentRow = dt.NewRow();
                            ViewState["objDTMobile"] = dt;
                            RepeaterMobileSIMPurchase.DataSource = dt;
                            RepeaterMobileSIMPurchase.DataBind();

                        }
                        else if (dt.Rows.Count == 1)
                        {
                            dt.Rows.Remove(dt.Rows[rowIndex]);
                            fillblankGridForMobileSIM();
                        }
                    }

                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void fillblankGridForMobileSIM()
        {
            //MobileSIMPurchase
            rptErrot.Visible = false;
            RepeaterMobileSIMPurchase.Visible = true;
            btnReplace.Visible = true;
            if (ViewState["objDTMobile"] == null)
            {
                dtfillGridMobileSIMPurchase = new DataTable();

                //dtfillGridMobileSIMPurchase.Columns.Add("MobileNo");
                dtfillGridMobileSIMPurchase.Columns.Add("SIMNo");
               
            }
            else
            {
                dtfillGridMobileSIMPurchase = (DataTable)ViewState["objDTMobile"];
                dtfillGridMobileSIMPurchase.Rows.Clear();
            }



            DataRow dr = dtfillGridMobileSIMPurchase.NewRow();

            dr = dtfillGridMobileSIMPurchase.NewRow();
            for (int i = 0; i < dtfillGridMobileSIMPurchase.Columns.Count; i++)
            {

                dr[i] = "";

            }
            dtfillGridMobileSIMPurchase.Rows.Add(dr);
            dtfillGridMobileSIMPurchase.AcceptChanges();

            RepeaterMobileSIMPurchase.DataSource = dtfillGridMobileSIMPurchase;
            RepeaterMobileSIMPurchase.DataBind();
            ViewState["objDTMobile"] = dtfillGridMobileSIMPurchase;


            for (int i = 0; i < RepeaterMobileSIMPurchase.Items.Count; i++)
            {
                LinkButton linkbtn = (LinkButton)RepeaterMobileSIMPurchase.Items[i].FindControl("lbtnRemove");
                linkbtn.Enabled = false;
            }

        }
        
        protected void chkBulkUpload_CheckedChanged(object sender, EventArgs e)
        {
            try
            {

                ViewState["objDTMobile"] = null;
                

                fillblankGridForMobileSIM();
               // fillblankGridForSIM();

                if (chkBulkUpload.Checked == true)
                {
                    
                    DivBulkUpload.Visible = true;
                    DivMobile.Visible = false;
                    liSim.Visible = true;
                    btnADDNewRowForSIM.Visible = false;
                }
                else
                {
                    DivBulkUpload.Visible = false;
                    DivMobile.Visible = true;
                    liSim.Visible = false;
                    btnADDNewRowForSIM.Visible = true;
                }
            }
            catch (Exception ex)
            {

            }
        }
 
        
        private void ShowPopUpMsg(string msg)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("alert('");
            sb.Append(msg.Replace("\n", "\\n").Replace("\r", "").Replace("'", "\\'"));
            sb.Append("');");
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "showalert", sb.ToString(), true);
        }
    
        protected void btnResetAll_Click(object sender, EventArgs e)
        {
            txtSIMNumber.Text = "";
            ddlNetwork.SelectedIndex = 0;
            ViewState["objDTMobile"]=null;
            fillblankGridForMobileSIM();
        }
        protected void btnReplace_Click(object sender, EventArgs e)
        {
            try
            {
                 
                if (ddlNetwork.SelectedIndex > 0)
                {
                    DataTable objDt = (DataTable)ViewState["objDTMobile"];
                    DataSet ds = new DataSet();
                    ds = svc.CheckSimNumber(Convert.ToInt32(ddlNetwork.SelectedValue), objDt);
                    if (ds != null)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            rptErrot.Visible = true;
                            RepeaterMobileSIMPurchase.Visible = false;
                            btnReplace.Visible = false;
                           rptErrot.DataSource=ds.Tables[0];
                           rptErrot.DataBind();
                           
                            return;
                        }
                        else
                        {
                           
                            ds = svc.UpdateBulkNetwork(Convert.ToInt32(ddlNetwork.SelectedValue), objDt);
                            ShowPopUpMsg("Network Changed Successfully.");
                            ddlNetwork.SelectedIndex = 0;
                            txtSIMNumber.Text = "";
                            ViewState["objDTMobile"] = null;
                            fillblankGridForMobileSIM();
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        protected void btnADDNewRowForSIM_Click(object sender, EventArgs e)
        {
            try
            {
                RepeaterMobileSIMPurchase.Visible = true;
                rptErrot.Visible = false;
                btnReplace.Visible = true;

                DataTable objDt = (DataTable)ViewState["objDTMobile"];
                if (objDt.Rows.Count == 1)
                {
                    if (Convert.ToString(objDt.Rows[0]["SIMNo"]) == "")
                    {
                        objDt.Rows.RemoveAt(0);
                    }
                }
                DataRow objDr = objDt.NewRow();

                objDr["SimNo"] = txtSIMNumber.Text.Trim();
                 

                objDt.Rows.Add(objDr);
                objDt.AcceptChanges();
                ViewState["objDTMobile"] = objDt;
                RepeaterMobileSIMPurchase.DataSource = ViewState["objDTMobile"];
                RepeaterMobileSIMPurchase.DataBind();

                //ClaculateNoOfRecords();

                txtSIMNumber.Text = String.Empty;
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnUploadFile_Click(object sender, EventArgs e)
        {
            
            ViewState["objDTMobile"] = null;
            fillblankGridForMobileSIM();
            //fillblankGridForSIM();

            FileUpload fuBulk = null;
            fuBulk = fuBulkUpload;
            UploadFile(fuBulk);
        }
        protected void UploadFile(FileUpload FuBulkDetails)
        {
            try
            {
                if (FuBulkDetails.HasFile)
                {
                    if (FuBulkDetails.FileName.Contains(".csv"))
                    {
                        string strPath = Server.MapPath("InventoryFiles") + "/" + FuBulkDetails.FileName;

                        //if (rbMobileSIMPurchase.Checked == true)
                        //{
                        //    FuBulkDetails.SaveAs(strPath);
                        //    ViewState["MobileSIMPurchaseBulk"] = strPath;

                        //    ViewState["objDTMobile"] = CSVTODatTable(strPath, (DataTable)ViewState["objDTMobile"]);

                        //    RepeaterMobileSIMPurchase.DataSource = ViewState["objDTMobile"];
                        //    RepeaterMobileSIMPurchase.DataBind();

                        //}
                        //else
                       // {
                            FuBulkDetails.SaveAs(strPath);
                            ViewState["SIMPurchaseBulk"] = strPath;

                            ViewState["objDTMobile"] = CSVTODatTableSIMPurchaseBulk(strPath, (DataTable)ViewState["objDTMobile"]);

                            RepeaterMobileSIMPurchase.DataSource = ViewState["objDTMobile"];
                            RepeaterMobileSIMPurchase.DataBind();
                       // }


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
                table = new DataTable("MobileSIMPurchaseBulk");
                DataColumn column = null;
                for (int i = 0; i < columnNames.Length; i++)
                {
                    column = new DataColumn(columnNames[i]);
                    table.Columns.Add(column);

                }
                table.Columns.Add("SIMTypeID");


                while (!DataStreamReader.EndOfStream)
                {
                    int iRowIndex = table.Rows.Count;
                    if (columnNames.Length != 5)
                    {
                        //lblMsg.Text = "Some column mistmatch in mobile bulk upload file";
                        iInvalidFileFormat = 1;
                        ScriptManager.RegisterStartupScript(this, Page.GetType(), "File Column Mismatch", "alert('Mobile SIM bulk upload File should contains 5 columns,please check your file')", true);
                        break;
                    }
                    table.Rows.Add(DataStreamReader.ReadLine().Split(new string[] { "," }, StringSplitOptions.None));

                    //if (table.Rows[iRowIndex]["SIMType"].ToString() == "Normal SIM")
                    //{
                    //    table.Rows[iRowIndex]["SIMTypeID"] = 12;
                    //}
                    //if (table.Rows[iRowIndex]["SIMType"].ToString() == "Micro SIM")
                    //{
                    //    table.Rows[iRowIndex]["SIMTypeID"] = 13;
                    //}
                    //if (table.Rows[iRowIndex]["SIMType"].ToString() == "Nano SIM")
                    //{
                    //    table.Rows[iRowIndex]["SIMTypeID"] = 14;
                    //}
                    //table.Rows[iRowIndex]["SIMTypeID"] = ddlSIMType_SIM.SelectedValue;

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

        private DataTable CSVTODatTableSIMPurchaseBulk(string CSVPath, DataTable table)
        {
            try
            {
                int iInvalidFileFormat = 0;
                StreamReader DataStreamReader = new StreamReader(CSVPath);
                string[] columnNames = DataStreamReader.ReadLine().Split(new string[] { "," }, StringSplitOptions.None);
                table = new DataTable("SIMPurchaseBulk");
                DataColumn column = null;
                for (int i = 0; i < columnNames.Length; i++)
                {
                    column = new DataColumn(columnNames[i]);
                    table.Columns.Add(column);
                }
                //table.Columns.Add("PIN");
                //table.Columns.Add("PUK");
                //table.Columns.Add("SIMType");
                //table.Columns.Add("SIMTypeID");


                while (!DataStreamReader.EndOfStream)
                {
                    int iRowIndex = table.Rows.Count;
                    if (columnNames.Length != 1)
                    {
                        //lblMsg.Text = "Some column mistmatch in mobile bulk upload file";
                        iInvalidFileFormat = 1;
                        ScriptManager.RegisterStartupScript(this, Page.GetType(), "File Column Mismatch", "alert('SIM bulk upload File should contains 4 columns,please check your file')", true);
                        break;
                    }
                    table.Rows.Add(DataStreamReader.ReadLine().Split(new string[] { "," }, StringSplitOptions.None));

                    //table.Rows[iRowIndex]["PIN"] = "0000";
                    //table.Rows[iRowIndex]["PUK"] = "0000000";
                    //table.Rows[iRowIndex]["SIMType"] = "Normal SIM";
                    //table.Rows[iRowIndex]["SIMTypeID"] = "12";

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
       
    }
}