using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using ENK.ServiceReference1;
using System.IO;


namespace ENK
{

    public partial class Purchase : System.Web.UI.Page
    {
        Service1Client ssc = new Service1Client();
        DataTable dtfillGridSIMPurchase;
        DataTable dtfillGridMobileSIMPurchase;
        //ViewState["objDTMobile"]=null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["LoginID"] != null)
                {
                    if (Session["ClientType"].ToString() == "Distributor")
                    {
                        Response.Redirect("Login.aspx", false);
                        Session.Abandon();
                        return;
                    }

                }

                txtPIN.Text = "0000";
                txtPUK.Text = "0000000";


                //txtPIN.Enabled = false;
                //txtPUK.Enabled = false;

                fillblankGridForMobileSIM();
                fillblankGridForSIM();
                txtPurchaseDate.Text = Convert.ToString(DateTime.Now);

                //txtPurchaseDate.Text = Convert.ToString(DateTime.UtcNow.AddMinutes(330).ToString("yyyy-MM-dd"));

                DataTable dt = ssc.GetVendor(Convert.ToInt32(Session["LoginId"]));
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        ddlVendor.DataSource = dt;
                        ddlVendor.DataValueField = "VendorID";
                        ddlVendor.DataTextField = "VendorName";
                        ddlVendor.DataBind();

                        ddlVendor.Items.Insert(0, new ListItem("---Select Network---", "0"));
                        ddlVendor.SelectedIndex = 1;
                    }
                }

                DataSet ds = ssc.GetShortCodeService("SIMType");
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            ddlSIMTypeforMobileSIM.DataSource = ds.Tables[0];
                            ddlSIMTypeforMobileSIM.DataValueField = "ID";
                            ddlSIMTypeforMobileSIM.DataTextField = "Value";
                            ddlSIMTypeforMobileSIM.DataBind();
                            ddlSIMTypeforMobileSIM.Items.Insert(0, new ListItem("---Select SIM Type---", "0"));

                            ddlSIMTypeForSIM.DataSource = ds.Tables[0];
                            ddlSIMTypeForSIM.DataValueField = "ID";
                            ddlSIMTypeForSIM.DataTextField = "Value";
                            ddlSIMTypeForSIM.DataBind();
                            ddlSIMTypeForSIM.Items.Insert(0, new ListItem("---Select SIM Type---", "0"));
                        }
                    }
                }
                ddlSIMTypeForSIM.SelectedIndex = 1;
                //ddlSIMTypeForSIM.Attributes.Add("disabled", "true");
                //ddlSIMTypeForSIM.Enabled = false;

                GetPurchaseCode();
                txtPurchaseNo.Enabled = false;
                txtInvoiceNo.Enabled = false;
            }
            else
            {

            }
        }

        //protected void chkBlankSIMPurchase_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (chkBlankSIMPurchase.Checked == true)
        //    {
        //        DivSIM.Visible = true;
        //        DivMobile.Visible = false;
        //    }
        //    else
        //    {
        //        DivSIM.Visible = false;
        //        DivMobile.Visible = true;
        //    }
        //}

        private void GetPurchaseCode()
        {
            try
            {
                DataSet ds = ssc.GetPurchaseCode();
                if (ds.Tables.Count > 0)
                {
                    txtInvoiceNo.Text = Convert.ToString(ds.Tables[0].Rows[0]["InvoiceNo"]);
                    txtPurchaseNo.Text = Convert.ToString(ds.Tables[0].Rows[0]["PruchaseCode"]);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        protected void btnSavePurchase_Click(object sender, EventArgs e)
        {
            try
            {
                //Salon1.ServiceReference1.Course p
                SIM s = new SIM();
                s.VendorID = Convert.ToInt32(ddlVendor.SelectedValue);
                s.PurchaseNo = Convert.ToString(txtPurchaseNo.Text);
                s.PurchaseDate = Convert.ToDateTime(txtPurchaseDate.Text);
                s.InvoiceNo = Convert.ToString(txtInvoiceNo.Text);
                s.DistributorID = Convert.ToInt32(Session["DistributorID"]);

                s.MobileDT = (DataTable)ViewState["objDTMobile"];

                s.SIMDt = (DataTable)ViewState["objDTSIM"];
                //if (rbMobileSIMPurchase.Checked == true)
                //{
                //    if (s.MobileDT.Rows.Count > 0)
                //    {
                //        if (Convert.ToString(s.MobileDT.Rows[0]["MobileNo"]) == "")
                //        {
                //            System.Text.StringBuilder javaScript = new System.Text.StringBuilder();
                //            string scriptKey = "ConfirmationScript";

                //            javaScript.Append("var userConfirmation = window.alert('" + "There is no data to Purchase" + "');\n");
                //            ClientScript.RegisterStartupScript(this.GetType(), scriptKey, javaScript.ToString(), true);
                //        }
                //    }
                //}
                if (rbSIMPurchase.Checked == true)
                {
                    if (s.SIMDt.Rows.Count > 0)
                    {
                        if (Convert.ToString(s.SIMDt.Rows[0]["SIMNo"]) == "")
                        {
                            System.Text.StringBuilder javaScript = new System.Text.StringBuilder();
                            string scriptKey = "ConfirmationScript";

                            javaScript.Append("var userConfirmation = window.alert('" + "There is no data to Purchase" + "');\n");
                            ClientScript.RegisterStartupScript(this.GetType(), scriptKey, javaScript.ToString(), true);
                        }
                    }
                }


                int UserID = Convert.ToInt32(Session["LoginID"]);

                int retval = ssc.SaveInventory(s, UserID, Actions.INSERT);
                if (retval > 0)
                {
                    System.Text.StringBuilder javaScript = new System.Text.StringBuilder();
                    string scriptKey = "ConfirmationScript";

                    javaScript.Append("var userConfirmation = window.alert('" + "Inventory Purchase successfully" + "');\n");
                    ClientScript.RegisterStartupScript(this.GetType(), scriptKey, javaScript.ToString(), true);

                    ViewState["objDTMobile"] = null;
                    ViewState["objDTSIM"] = null;

                    fillblankGridForMobileSIM();
                    fillblankGridForSIM();
                    GetPurchaseCode();
                }
                else
                {

                    System.Text.StringBuilder javaScript = new System.Text.StringBuilder();
                    string scriptKey = "ConfirmationScript";

                    javaScript.Append("var userConfirmation = window.alert('" + "Check Data" + "');\n");
                    ClientScript.RegisterStartupScript(this.GetType(), scriptKey, javaScript.ToString(), true);
                }

                txtPurchaseNo.Text = string.Empty;
                txtPurchaseDate.Text = string.Empty;
                txtInvoiceNo.Text = string.Empty;


            }
            catch (Exception ex)
            {
                throw ex;
                //ShowPopUpMsg(ex.Message);
            }

        }


        protected void fillblankGridForSIM()
        {
            //SIMPurchase
            if (ViewState["objDTSIM"] == null)
            {
                dtfillGridSIMPurchase = new DataTable();

                dtfillGridSIMPurchase.Columns.Add("SIMNo");
                dtfillGridSIMPurchase.Columns.Add("PIN");
                dtfillGridSIMPurchase.Columns.Add("PUK");
                dtfillGridSIMPurchase.Columns.Add("SIMType");
                dtfillGridSIMPurchase.Columns.Add("SIMTypeID");
            }
            else
            {
                dtfillGridSIMPurchase = (DataTable)ViewState["objDTSIM"];
                dtfillGridSIMPurchase.Rows.Clear();
            }


            DataRow drSIM = dtfillGridSIMPurchase.NewRow();

            drSIM = dtfillGridSIMPurchase.NewRow();
            for (int i = 0; i < dtfillGridSIMPurchase.Columns.Count; i++)
            {

                drSIM[i] = "";


            }
            dtfillGridSIMPurchase.Rows.Add(drSIM);
            dtfillGridSIMPurchase.AcceptChanges();

            RepeaterSIMPurchase.DataSource = dtfillGridSIMPurchase;
            RepeaterSIMPurchase.DataBind();
            ViewState["objDTSIM"] = dtfillGridSIMPurchase;

            for (int i = 0; i < RepeaterSIMPurchase.Items.Count; i++)
            {
                LinkButton linkbtn = (LinkButton)RepeaterSIMPurchase.Items[i].FindControl("lbtnRemove");
                linkbtn.Enabled = false;
            }
        }
        protected void fillblankGridForMobileSIM()
        {
            //MobileSIMPurchase
            if (ViewState["objDTMobile"] == null)
            {
                dtfillGridMobileSIMPurchase = new DataTable();

                dtfillGridMobileSIMPurchase.Columns.Add("MobileNo");
                dtfillGridMobileSIMPurchase.Columns.Add("SIMNo");
                dtfillGridMobileSIMPurchase.Columns.Add("PIN");
                dtfillGridMobileSIMPurchase.Columns.Add("PUK");
                dtfillGridMobileSIMPurchase.Columns.Add("SIMType");
                dtfillGridMobileSIMPurchase.Columns.Add("SIMTypeID");
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

        protected void RepeaterSIMPurchase_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Delete")
                {
                    if (ViewState["objDTMobile"] != null)
                    {
                        DataTable dt = (DataTable)ViewState["objDTSIM"];
                        DataRow drCurrentRow = null;
                        int rowIndex = Convert.ToInt32(e.Item.ItemIndex);
                        if (dt.Rows.Count > 1)
                        {
                            dt.Rows.Remove(dt.Rows[rowIndex]);
                            drCurrentRow = dt.NewRow();
                            ViewState["objDTSIM"] = dt;
                            RepeaterSIMPurchase.DataSource = dt;
                            RepeaterSIMPurchase.DataBind();

                        }
                        else if (dt.Rows.Count == 1)
                        {
                            dt.Rows.Remove(dt.Rows[rowIndex]);
                            fillblankGridForSIM();
                        }
                    }

                }
            }
            catch (Exception ex)
            {

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

        protected void btnADDNewRowForSIM_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable objDt = (DataTable)ViewState["objDTSIM"];
                if (objDt.Rows.Count == 1)
                {
                    if (Convert.ToString(objDt.Rows[0]["SIMNo"]) == "")
                    {
                        objDt.Rows.RemoveAt(0);
                    }
                }
                DataRow objDr = objDt.NewRow();

                objDr["SimNo"] = txtSIM.Text.Trim();
                objDr["PIN"] = txtPIN.Text.Trim();
                objDr["PUK"] = txtPUK.Text.Trim();
                objDr["SIMType"] = "Normal SIM";//ddlSIMTypeForSIM.SelectedItem.Text;
                objDr["SIMTypeID"] = 12;//ddlSIMTypeForSIM.SelectedValue;

                objDt.Rows.Add(objDr);
                objDt.AcceptChanges();
                ViewState["objDTSIM"] = objDt;
                RepeaterSIMPurchase.DataSource = ViewState["objDTSIM"];
                RepeaterSIMPurchase.DataBind();

                //ClaculateNoOfRecords();
                if (ddlVendor.SelectedValue == "13")
                {
                    txtSIM.Text = "8919601";
                }
                else
                {
                    txtSIM.Text = "";
                }
                txtPIN.Text = "0000";
                txtPUK.Text = "0000000";

                //txtPIN.Enabled = false;
                //txtPUK.Enabled = false;
                ddlSIMTypeForSIM.SelectedIndex = 1;
                //ddlSIMTypeForSIM.Attributes.Add("disabled", "true");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        protected void btnADDNewRowforMobileSIM_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable objDt = (DataTable)ViewState["objDTMobile"];
                if (objDt.Rows.Count == 1)
                {
                    if (Convert.ToString(objDt.Rows[0]["MobileNo"]) == "")
                    {
                        objDt.Rows.RemoveAt(0);
                    }
                }
                DataRow objDr = objDt.NewRow();
                objDr["MobileNo"] = txtMobileforMobileSIM.Text.Trim();
                objDr["SimNo"] = txtSIMforMobileSIM.Text.Trim();
                objDr["PIN"] = txtPINforMobileSIM.Text.Trim();
                objDr["PUK"] = txtPUKforMobileSIM.Text.Trim();
                objDr["SIMType"] = ddlSIMTypeforMobileSIM.SelectedItem.Text;
                objDr["SIMTypeID"] = ddlSIMTypeforMobileSIM.SelectedValue;

                objDt.Rows.Add(objDr);
                objDt.AcceptChanges();
                ViewState["objDTMobile"] = objDt;
                RepeaterMobileSIMPurchase.DataSource = ViewState["objDTMobile"];
                RepeaterMobileSIMPurchase.DataBind();

                //ClaculateNoOfRecords();
                txtMobileforMobileSIM.Text = string.Empty;
                txtSIMforMobileSIM.Text = string.Empty;
                txtPINforMobileSIM.Text = String.Empty;
                txtPUKforMobileSIM.Text = String.Empty;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnResetAll_Click(object sender, EventArgs e)
        {
            Response.Redirect("Purchase.aspx");
        }

        protected void chkBulkUpload_CheckedChanged(object sender, EventArgs e)
        {
            try
            {

                ViewState["objDTMobile"] = null;
                ViewState["objDTSIM"] = null;

                fillblankGridForMobileSIM();
                fillblankGridForSIM();

                if (chkBulkUpload.Checked == true)
                {

                    DivBulkUpload.Visible = true;
                    DivMobile.Visible = false;
                    DivSIM.Visible = false;
                    if (rbMobileSIMPurchase.Checked == true)
                    {
                        DivMobileRepeater.Visible = true;
                        DivSIMRepeater.Visible = false;
                        liSim.Visible = false;
                        liMobile.Visible = true;
                    }
                    if (rbSIMPurchase.Checked == true)
                    {
                        DivSIMRepeater.Visible = true;
                        DivMobileRepeater.Visible = false;
                        liSim.Visible = true;
                        liMobile.Visible = false;
                    }
                }
                else
                {
                    DivBulkUpload.Visible = false;
                    if (rbMobileSIMPurchase.Checked == true)
                    {
                        DivMobile.Visible = true;
                        DivMobileRepeater.Visible = true;

                        DivSIMRepeater.Visible = false;
                        DivSIM.Visible = false;
                    }
                    if (rbSIMPurchase.Checked == true)
                    {
                        DivSIMRepeater.Visible = true;
                        DivSIM.Visible = true;

                        DivMobile.Visible = false;
                        DivMobileRepeater.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void rbSIMPurchase_CheckedChanged(object sender, EventArgs e)
        {
            //if (rbSIMPurchase.Checked == true)
            //{
            //    DivSIM.Visible = true;
            //    DivMobile.Visible = false;
            //}
            //else
            //{
            //    DivSIM.Visible = false;
            //    DivMobile.Visible = true;
            //}

            if (chkBulkUpload.Checked == true)
            {

                DivBulkUpload.Visible = true;
                DivMobile.Visible = false;
                DivSIM.Visible = false;
                if (rbMobileSIMPurchase.Checked == true)
                {
                    DivMobileRepeater.Visible = true;
                    DivSIMRepeater.Visible = false;
                    liMobile.Visible = true;
                    liSim.Visible = false;
                }
                if (rbSIMPurchase.Checked == true)
                {
                    DivSIMRepeater.Visible = true;
                    DivMobileRepeater.Visible = false;
                    liMobile.Visible = false;
                    liSim.Visible = true;
                }
            }
            else
            {
                DivBulkUpload.Visible = false;
                if (rbMobileSIMPurchase.Checked == true)
                {
                    DivMobile.Visible = true;
                    DivMobileRepeater.Visible = true;

                    DivSIMRepeater.Visible = false;
                    DivSIM.Visible = false;
                    //liMobile.Visible = false;
                    //liSim.Visible = false;
                }
                if (rbSIMPurchase.Checked == true)
                {
                    DivSIMRepeater.Visible = true;
                    DivSIM.Visible = true;

                    DivMobile.Visible = false;
                    DivMobileRepeater.Visible = false;
                }
            }
        }

        protected void rbMobileSIMPurchase_CheckedChanged(object sender, EventArgs e)
        {
            //if (rbMobileSIMPurchase.Checked == true)
            //{
            //    DivSIM.Visible = false;
            //    DivMobile.Visible = true;
            //}
            //else
            //{
            //    DivSIM.Visible = true;
            //    DivMobile.Visible = false;
            //}

            if (chkBulkUpload.Checked == true)
            {

                DivBulkUpload.Visible = true;
                DivMobile.Visible = false;
                DivSIM.Visible = false;
                if (rbMobileSIMPurchase.Checked == true)
                {
                    DivMobileRepeater.Visible = true;
                    DivSIMRepeater.Visible = false;
                    liMobile.Visible = true;
                    liSim.Visible = false;
                }
                if (rbSIMPurchase.Checked == true)
                {
                    DivSIMRepeater.Visible = true;
                    DivMobileRepeater.Visible = false;
                    liMobile.Visible = false;
                    liSim.Visible = true;
                }
            }
            else
            {
                DivBulkUpload.Visible = false;
                if (rbMobileSIMPurchase.Checked == true)
                {
                    DivMobile.Visible = true;
                    DivMobileRepeater.Visible = true;

                    DivSIMRepeater.Visible = false;
                    DivSIM.Visible = false;
                }
                if (rbSIMPurchase.Checked == true)
                {
                    DivSIMRepeater.Visible = true;
                    DivSIM.Visible = true;

                    DivMobile.Visible = false;
                    DivMobileRepeater.Visible = false;
                }
            }
        }


        protected void btnUploadFile_Click(object sender, EventArgs e)
        {
            ViewState["objDTMobile"] = null;
            ViewState["objDTSIM"] = null;
            fillblankGridForMobileSIM();
            fillblankGridForSIM();

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

                        if (rbMobileSIMPurchase.Checked == true)
                        {
                            FuBulkDetails.SaveAs(strPath);
                            ViewState["MobileSIMPurchaseBulk"] = strPath;

                            ViewState["objDTMobile"] = CSVTODatTable(strPath, (DataTable)ViewState["objDTMobile"]);

                            RepeaterMobileSIMPurchase.DataSource = ViewState["objDTMobile"];
                            RepeaterMobileSIMPurchase.DataBind();
                            //if (RepeaterMobileSIMPurchase.Items.Count > 0)
                            //{
                            //    Grid.Visible = true;
                            //}
                            //else
                            //{
                            //    Grid.Visible = false;
                            //}
                        }
                        else
                        {
                            FuBulkDetails.SaveAs(strPath);
                            ViewState["SIMPurchaseBulk"] = strPath;

                            ViewState["objDTSIM"] = CSVTODatTableSIMPurchaseBulk(strPath, (DataTable)ViewState["objDTSIM"]);

                            RepeaterSIMPurchase.DataSource = ViewState["objDTSIM"];
                            RepeaterSIMPurchase.DataBind();
                        }


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

                    if (table.Rows[iRowIndex]["SIMType"].ToString() == "Normal SIM")
                    {
                        table.Rows[iRowIndex]["SIMTypeID"] = 12;
                    }
                    if (table.Rows[iRowIndex]["SIMType"].ToString() == "Micro SIM")
                    {
                        table.Rows[iRowIndex]["SIMTypeID"] = 13;
                    }
                    if (table.Rows[iRowIndex]["SIMType"].ToString() == "Nano SIM")
                    {
                        table.Rows[iRowIndex]["SIMTypeID"] = 14;
                    }
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
                table.Columns.Add("PIN");
                table.Columns.Add("PUK");
                table.Columns.Add("SIMType");
                table.Columns.Add("SIMTypeID");


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

                    table.Rows[iRowIndex]["PIN"] = "0000";
                    table.Rows[iRowIndex]["PUK"] = "0000000";
                    table.Rows[iRowIndex]["SIMType"] = "Normal SIM";
                    table.Rows[iRowIndex]["SIMTypeID"] = "12";

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

        protected void ddlVendor_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlVendor.SelectedValue == "13")
                {
                    txtSIM.Text = "8919601";
                    txtSIM.MaxLength = 19;
                }
                else if (ddlVendor.SelectedValue == "15")
                {
                    txtSIM.Text = "";
                    txtSIM.MaxLength = 20;
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}