using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.IO;
using ENK.ServiceReference1;

namespace ENK
{
    public partial class SimTariffMapping : System.Web.UI.Page
    {
        Service1Client ssc = new Service1Client();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
                ddlMonth.SelectedValue = Convert.ToString('1');
                if (Session["LoginID"] != null)
                {
                    if (Session["ClientType"].ToString() == "Distributor")
                    {
                        Response.Redirect("Login.aspx", false);
                        Session.Abandon();
                        return;
                    }

                    DataTable dt = ssc.GetVendor(Convert.ToInt32(Session["LoginId"]));
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            ddlNetwork.DataSource = dt;
                            ddlNetwork.DataValueField = "VendorID";
                            ddlNetwork.DataTextField = "VendorName";
                            ddlNetwork.DataBind();
                            ddlNetwork.Items.Insert(0, new ListItem("---Select Network---", "0"));
                            ddlNetwork.SelectedValue = "13";
                            //ddlNetwork.Attributes.Add("disabled", "disabled");
                        }
                    }
                }
                fillData();

                ddlNetwork_SelectedIndexChanged(null, null);
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
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            Boolean sim = false;
            for (int i = 0; i < RepeaterTransferSIM.Items.Count; i++)
            {
                CheckBox chk = (CheckBox)RepeaterTransferSIM.Items[i].FindControl("CheckBox2");

                if (chk.Checked == true)
                {
                    sim = true;
                    break;
                }

            }

            if (sim == false)
            {
                ShowPopUpMsg("Please select any SIM");
                return;
            }

            SIM s = new SIM();

            int UserID = Convert.ToInt32(Session["LoginId"]);
            s.ClientID = Convert.ToInt32(Session["DistributorID"]);
            s.TariffID = Convert.ToInt32(ddlTariff.SelectedValue);

            DataTable dtSIM = new DataTable();
            dtSIM.TableName = "dtSIM";
            dtSIM.Columns.Add("SIMID");

            for (int i = 0; i < RepeaterTransferSIM.Items.Count; i++)
            {
                CheckBox chk = (CheckBox)RepeaterTransferSIM.Items[i].FindControl("CheckBox2");
                HiddenField hdnSIMID = (HiddenField)RepeaterTransferSIM.Items[i].FindControl("hdnSIMID");
                if (chk.Checked == true)
                {
                    DataRow drSIM = dtSIM.NewRow();


                    drSIM["SIMID"] = Convert.ToInt64(hdnSIMID.Value);

                    dtSIM.Rows.Add(drSIM);
                    dtSIM.AcceptChanges();
                }

            }

            s.SIMDt = dtSIM;
            //Ankit Singh
            //int retval = ssc.SimTariffMapping(s, UserID, Actions.INSERT);
            if (Convert.ToInt32(ddlMonth.SelectedValue) > 6)
            {
                ShowPopUpMsg("Months must be less than or equal to 6");
            }

            else
            {
                int retval = ssc.SimTariffMapping(s, UserID, Actions.INSERT, Convert.ToInt32(ddlMonth.SelectedValue));
                if (retval > 0)
                {
                    ShowPopUpMsg("Plan Mapped with SIM successfully");
                }
                else
                {
                    ShowPopUpMsg("Error ! Please Check Data");

                }
                fillData();
                //ddlNetwork.SelectedIndex = 0;
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            Response.Redirect("SimTariffMapping.aspx");
        }
        protected void chkAllSIM_OnCheckedChanged(object sender, EventArgs e)
        {
            CheckBox chkAll = (CheckBox)sender;
            RepeaterItem row = (RepeaterItem)chkAll.NamingContainer;
            int a = row.ItemIndex;
            if (chkAll.Checked == true)
            {
                for (int i = 0; i < RepeaterTransferSIM.Items.Count; i++)
                {
                    CheckBox chk = (CheckBox)RepeaterTransferSIM.Items[i].FindControl("CheckBox2");
                    chk.Checked = true;
                }
            }
            if (chkAll.Checked == false)
            {
                for (int i = 0; i < RepeaterTransferSIM.Items.Count; i++)
                {
                    CheckBox chk = (CheckBox)RepeaterTransferSIM.Items[i].FindControl("CheckBox2");
                    chk.Checked = false;
                }
            }
        }

        protected void fillData()
        {
            try
            {
                //Distributor[] dis = null;
                int UserID = Convert.ToInt32(Session["LoginId"]);
                int ClientID = Convert.ToInt32(Session["DistributorID"]);
                DataSet dss = ssc.GetTariffService(UserID, ClientID);
                if (dss != null)
                {
                    ViewState["Lyca"] = dss.Tables[0];
                    ViewState["H2O"] = dss.Tables[1];
                    ViewState["EasyGo"] = dss.Tables[2];

                    ddlTariff.DataSource = dss.Tables[0];
                    ddlTariff.DataValueField = "ID";
                    ddlTariff.DataTextField = "TariffDetails";
                    ddlTariff.DataBind();
                    ddlTariff.Items.Insert(0, new ListItem("---Select Tariff---", "0"));
                }


                //DataSet ds = ssc.GetInventory(ClientID,"ALL");
                //ViewState["SIMPurchase"] = ds.Tables[1];
                //if (ds != null)
                //{
                //    if (ds.Tables.Count > 0)
                //    {
                //        if (ds.Tables[1].Rows.Count > 0)
                //        {
                //            RepeaterTransferSIM.DataSource = ds.Tables[1];
                //            RepeaterTransferSIM.DataBind();
                //            lblCount.Text = Convert.ToString(ds.Tables[1].Rows.Count);
                //        }
                //        else
                //        {
                //            lblCount.Text = Convert.ToString("0");
                //        }
                //    }
                //}
            }
            //Ankit Singh
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnUploadFile_Click(object sender, EventArgs e)
        {
            ViewState["objDTSIM"] = null;


            FileUpload fuBulk = null;
            fuBulk = fuBulkUpload;
            UploadFile(fuBulk);

            int ClientID = Convert.ToInt32(Session["DistributorID"]);
            DataTable dt = (DataTable)ViewState["objDTSIM"];
            DataSet ds = ssc.GetInventoryBulkTransfer1(ClientID, dt);
            if (ds != null)
            {
                RepeaterTransferSIM.DataSource = null;
                RepeaterTransferSIM.DataBind();
                RepeaterTransferSIM.DataSource = ds.Tables[0];
                RepeaterTransferSIM.DataBind();
                lblCount.Text = Convert.ToString(ds.Tables[0].Rows.Count);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < RepeaterTransferSIM.Items.Count; i++) //RepeaterTransferSIM
                    {
                        CheckBox chk = (CheckBox)RepeaterTransferSIM.Items[i].FindControl("CheckBox2");
                        chk.Checked = true;
                    }
                }
            }
        }

        protected void UploadFile(FileUpload FuBulkDetails)
        {
            try
            {
                if (FuBulkDetails.HasFile)
                {
                    if (FuBulkDetails.FileName.Contains(".csv"))
                    {
                        string strPath = Server.MapPath("InventoryFiles") + FuBulkDetails.FileName;


                        FuBulkDetails.SaveAs(strPath);
                        ViewState["SIMPurchaseBulk"] = strPath;

                        ViewState["objDTSIM"] = CSVTODatTableSIMPurchaseBulk(strPath, (DataTable)ViewState["objDTSIM"]);


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

                while (!DataStreamReader.EndOfStream)
                {
                    int iRowIndex = table.Rows.Count;
                    if (columnNames.Length != 2)
                    {
                        //lblMsg.Text = "Some column mistmatch in mobile bulk upload file";
                        iInvalidFileFormat = 1;
                        ScriptManager.RegisterStartupScript(this, Page.GetType(), "File Column Mismatch", "alert('SIM bulk upload File should contains 2 columns,please check your file')", true);
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

        protected void chkBulkUpload_CheckedChanged(object sender, EventArgs e)
        {
            if (chkBulkUpload.Checked == true)
            {
                BulkUpload.Visible = true;
                // btnUnMapped.Visible = false;
            }
            else
            {
                BulkUpload.Visible = false;
                //  btnUnMapped.Visible = true;
            }
        }
        protected void ddlNetwork_SelectedIndexChanged(object sender, EventArgs e)
        {

            try
            {
                DataTable dtSIMPurchase = new DataTable();
                //int UserID = Convert.ToInt32(Session["LoginId"]);
                //int ClientID = Convert.ToInt32(Session["DistributorID"]);
                //DataSet dss = ssc.GetTariffService(UserID, ClientID);

                if (ddlNetwork.SelectedItem.Text == "Lyca Mobile")
                {
                    DataTable dt = new DataTable();
                    dt = (DataTable)ViewState["Lyca"];
                    ddlTariff.DataSource = ViewState["Lyca"];
                    ddlTariff.DataValueField = "ID";
                    ddlTariff.DataTextField = "TariffDetails";
                    ddlTariff.DataBind();
                    ddlTariff.Items.Insert(0, new ListItem("---Select Tariff---", "0"));
                    btnSearch.Attributes.Remove("disabled");
                    btnUnMapped.Attributes.Remove("disabled");
                    btnSubmit.Attributes.Remove("disabled");
                }
                else if (ddlNetwork.SelectedItem.Text == "H2O")
                {
                    ShowPopUpMsg("SIM Plan mapping is not available for H2O");
                    btnSearch.Attributes.Add("disabled", "disabled");
                    btnUnMapped.Attributes.Add("disabled", "disabled");
                    btnSubmit.Attributes.Add("disabled", "disabled");
                    return;
                    //DataTable dt = new DataTable();
                    //dt = (DataTable)ViewState["H2O"];
                    //ddlTariff.DataSource = dt;
                    //ddlTariff.DataValueField = "ID";
                    //ddlTariff.DataTextField = "TariffDetails";
                    //ddlTariff.DataBind();
                    //ddlTariff.Items.Insert(0, new ListItem("---Select Tariff---", "0"));
                }
                //else if (ddlNetwork.SelectedItem.Text == "EasyGo")
                //{
                //    DataTable dt = new DataTable();
                //    dt = (DataTable)ViewState["EasyGo"];
                //    ddlTariff.DataSource = dt;
                //    ddlTariff.DataValueField = "ID";
                //    ddlTariff.DataTextField = "TariffDetails";
                //    ddlTariff.DataBind();
                //    ddlTariff.Items.Insert(0, new ListItem("---Select Tariff---", "0"));
                //}
                else
                {
                    ddlTariff.Items.Clear();
                    ddlTariff.Items.Insert(0, new ListItem("---Select Tariff---", "0"));

                }
                //if (ddlNetwork.SelectedIndex > 0)
                //{
                //    dtSIMPurchase = (DataTable)ViewState["SIMPurchase"];
                //    DataView dv = dtSIMPurchase.DefaultView;
                //    dv.RowFilter = "VendorID = " + Convert.ToInt32(ddlNetwork.SelectedValue);
                //    dtSIMPurchase = dv.ToTable();

                //    RepeaterTransferSIM.DataSource = dtSIMPurchase;
                //    RepeaterTransferSIM.DataBind();
                //}
                //else
                //{
                //    dtSIMPurchase = (DataTable)ViewState["SIMPurchase"];
                //    RepeaterTransferSIM.DataSource = dtSIMPurchase;
                //    RepeaterTransferSIM.DataBind();
                //}

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        protected void btnUnMapped_Click(object sender, EventArgs e)
        {

            if (ddlNetwork.SelectedIndex == 0)
            {
                ShowPopUpMsg("Please select network");
                return;
            }


            if (ddlMonth.SelectedValue == "")
            {
                ShowPopUpMsg("Please select Related tariff");
                return;
            }



            Boolean sim = false;
            for (int i = 0; i < RepeaterTransferSIM.Items.Count; i++)
            {
                CheckBox chk = (CheckBox)RepeaterTransferSIM.Items[i].FindControl("CheckBox2");

                if (chk.Checked == true)
                {
                    sim = true;
                    break;
                }

            }

            if (sim == false)
            {
                ShowPopUpMsg("Please select any SIM");
                return;
            }

            SIM s = new SIM();

            int UserID = Convert.ToInt32(Session["LoginId"]);
            s.ClientID = Convert.ToInt32(Session["DistributorID"]);
            s.TariffID = Convert.ToInt32(0);// Unmapped Plan;

            DataTable dtSIM = new DataTable();
            dtSIM.TableName = "dtSIM";
            dtSIM.Columns.Add("SIMID");

            for (int i = 0; i < RepeaterTransferSIM.Items.Count; i++)
            {
                CheckBox chk = (CheckBox)RepeaterTransferSIM.Items[i].FindControl("CheckBox2");
                HiddenField hdnSIMID = (HiddenField)RepeaterTransferSIM.Items[i].FindControl("hdnSIMID");
                if (chk.Checked == true)
                {
                    DataRow drSIM = dtSIM.NewRow();


                    drSIM["SIMID"] = Convert.ToInt64(hdnSIMID.Value);

                    dtSIM.Rows.Add(drSIM);
                    dtSIM.AcceptChanges();
                }

            }

            s.SIMDt = dtSIM;

            int retval = ssc.SimTariffMapping(s, UserID, Actions.INSERT, Convert.ToInt32(ddlMonth.SelectedValue));
            if (retval > 0)
            {
                ShowPopUpMsg("Plan UnMapped with SIM successfully");
            }
            else
            {
                ShowPopUpMsg("Error ! Please Check Data");

            }
            fillData();
            //ddlNetwork.SelectedIndex = 0;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            int ClientID = Convert.ToInt32(Session["DistributorID"]);
            if ((TxtSimNumb.Text == "" || TxtSimNumb.Text == "8919601") && (txtFromSim.Text == "" || txtFromSim.Text == "8919601") && (txtTosim.Text == "" || txtTosim.Text == "8919601"))
            {
                RepeaterTransferSIM.DataSource = null;
                RepeaterTransferSIM.DataBind();
                lblCount.Text = "0";
                ShowPopUpMsg("This N0 sim available against these SIM Number");
            }

            else if ((TxtSimNumb.Text == "" || TxtSimNumb.Text == "8919601") && txtFromSim.Text != "" && txtTosim.Text != "")
            {
                DataSet ds1 = ssc.GetInventory1(ClientID, "ALL", "", txtFromSim.Text, txtTosim.Text);
                DataTable dt1 = ds1.Tables[0];
                if (dt1.Rows.Count > 0)
                {
                    RepeaterTransferSIM.DataSource = dt1;
                    RepeaterTransferSIM.DataBind();
                    lblCount.Text = Convert.ToString(dt1.Rows.Count);
                    for (int i = 0; i < RepeaterTransferSIM.Items.Count; i++)
                    {
                        CheckBox chk = (CheckBox)RepeaterTransferSIM.Items[i].FindControl("CheckBox2");
                        chk.Checked = true;
                    }
                }
            }
            else if (TxtSimNumb.Text != "" && (txtFromSim.Text == "" || txtFromSim.Text == "8919601") && (txtTosim.Text == "" || txtTosim.Text == "8919601"))
            {
                DataSet ds = ssc.GetInventory1(ClientID, "ALL", TxtSimNumb.Text, "", "");
                DataTable dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    RepeaterTransferSIM.DataSource = dt;
                    RepeaterTransferSIM.DataBind();
                    lblCount.Text = Convert.ToString(dt.Rows.Count);
                    for (int i = 0; i < RepeaterTransferSIM.Items.Count; i++)
                    {
                        CheckBox chk = (CheckBox)RepeaterTransferSIM.Items[i].FindControl("CheckBox2");
                        chk.Checked = true;
                    }
                }
            }


        }


        protected void ddlTariff_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlMonth.Items.Clear();
            ddlMonth.Items.Insert(0, new ListItem("1", "1"));
            ddlMonth.Items.Insert(1, new ListItem("2", "2"));
            ddlMonth.Items.Insert(2, new ListItem("3", "3"));
            ddlMonth.Items.Insert(3, new ListItem("4", "4"));
            ddlMonth.Items.Insert(4, new ListItem("5", "5"));
            ddlMonth.Items.Insert(5, new ListItem("6", "6"));

            if (ddlTariff.SelectedValue == "45" || ddlTariff.SelectedValue == "46" || ddlTariff.SelectedValue == "47" || ddlTariff.SelectedValue == "48" || ddlTariff.SelectedValue == "49")
            {

                ddlMonth.Items.Clear();
                ddlMonth.Items.Insert(0, new ListItem("1", "1"));
                ddlMonth.Items.Insert(1, new ListItem("2", "2"));
                ddlMonth.Items.Insert(2, new ListItem("3", "3"));
                ddlMonth.Items.Insert(3, new ListItem("4", "4"));
            }
        }
    }
}