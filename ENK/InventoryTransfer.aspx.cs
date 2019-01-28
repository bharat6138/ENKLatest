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
using System.Configuration;

namespace ENK
{
    public partial class InventoryTransfer : System.Web.UI.Page
    {
        DataTable dtfillGridSIMPurchase;
        DataTable dtfillGridMobileSIMPurchase;
        Service1Client ssc = new Service1Client();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
                // string checkInventoryWAY = ConfigurationManager.AppSettings.Get("Inventory_Transfer");
                fillData();
                ddlAction.SelectedValue = "1";
                ddlAction.Attributes.Add("disabled", "disabled");
            }
        }

        protected void fillblankGridForSIM()
        {
            //SIMPurchase

            dtfillGridSIMPurchase = new DataTable();

            dtfillGridSIMPurchase.Columns.Add("SIMNo");
            dtfillGridSIMPurchase.Columns.Add("SIMID");
            RepeaterTransferSIM.DataSource = dtfillGridSIMPurchase;
            RepeaterTransferSIM.DataBind();



        }
        protected void fillblankGridForMobileSIM()
        {
            dtfillGridMobileSIMPurchase = new DataTable();
            dtfillGridMobileSIMPurchase.Columns.Add("MobileNo");
            dtfillGridMobileSIMPurchase.Columns.Add("MSISDN_SIM_ID");
            dtfillGridMobileSIMPurchase.Columns.Add("SIMNo");
            RepeaterTransfer.DataSource = dtfillGridMobileSIMPurchase;
            RepeaterTransfer.DataBind();
        }
        protected void chkAll_OnCheckedChanged(object sender, EventArgs e)
        {
            CheckBox chkAll = (CheckBox)sender;
            RepeaterItem row = (RepeaterItem)chkAll.NamingContainer;
            int a = row.ItemIndex;
            if (chkAll.Checked == true)
            {
                for (int i = 0; i < RepeaterTransfer.Items.Count; i++)
                {
                    CheckBox chk = (CheckBox)RepeaterTransfer.Items[i].FindControl("CheckBox2");
                    chk.Checked = true;
                }
            }
            if (chkAll.Checked == false)
            {
                for (int i = 0; i < RepeaterTransfer.Items.Count; i++)
                {
                    CheckBox chk = (CheckBox)RepeaterTransfer.Items[i].FindControl("CheckBox2");
                    chk.Checked = false;
                }
            }

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

        protected void chkBlankSIMTransfer_CheckedChanged(object sender, EventArgs e)
        {
            if (chkBlankSIMTransfer.Checked == true)
            {
                if (chkMSISDNTransfer.Checked == true)
                {
                    DivSIM.Visible = true;
                    DivMobile.Visible = true;
                }
                else
                {
                    DivSIM.Visible = true;
                    DivMobile.Visible = false;
                }

            }
            else
            {
                if (chkMSISDNTransfer.Checked == true)
                {
                    DivSIM.Visible = false;
                    DivMobile.Visible = true;
                }
                else
                {
                    DivSIM.Visible = false;
                    DivMobile.Visible = false;
                }
            }
        }
        protected void chkMSISDNTransfer_CheckedChanged(object sender, EventArgs e)
        {
            if (chkMSISDNTransfer.Checked == true)
            {
                if (chkBlankSIMTransfer.Checked == true)
                {
                    DivSIM.Visible = true;
                    DivMobile.Visible = true;
                }
                else
                {
                    DivSIM.Visible = false;
                    DivMobile.Visible = true;
                }

            }
            else
            {
                if (chkBlankSIMTransfer.Checked == true)
                {
                    DivSIM.Visible = true;
                    DivMobile.Visible = false;
                }
                else
                {
                    DivSIM.Visible = false;
                    DivMobile.Visible = false;
                }

            }
        }

        protected void fillData()
        {
            try
            {
                Distributor[] dis = null;
                dis = ssc.GetDistributorDDLService(0, Convert.ToInt32(Session["DistributorID"]));
                if (dis != null)
                {
                    ddlTransferTo.DataSource = dis;
                    ddlTransferTo.DataValueField = "distributorID";
                    ddlTransferTo.DataTextField = "distributorName";
                    ddlTransferTo.DataBind();
                    ddlTransferTo.Items.Insert(0, new ListItem("---Select Client---", "0"));
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
                        ddlNetwork.SelectedValue = "13";
                    }
                }







                //int ClientID = Convert.ToInt32(Session["DistributorID"]);
                //string Action = "NotMappedTariff";
                //DataSet ds = ssc.GetInventory(ClientID, Action);

                //ViewState["SIMPurchase"] = ds.Tables[1];


                //if (ds != null)
                //{
                //    if (ds.Tables.Count > 0)
                //    {
                //        if (ds.Tables[0].Rows.Count > 0)
                //        {
                //            RepeaterTransfer.DataSource = ds.Tables[0];
                //            RepeaterTransfer.DataBind();
                //        }
                //        else
                //        {
                //            //  RepeaterTransfer.DataSource = ds.Tables[0];
                //            // RepeaterTransfer.DataBind();
                //            fillblankGridForMobileSIM();
                //        }
                //        if (ds.Tables[1].Rows.Count > 0)
                //        {
                //            //RepeaterTransferSIM.DataSource = ds.Tables[1];
                //            // RepeaterTransferSIM.DataBind();
                //            // lblCount.Text = Convert.ToString(ds.Tables[1].Rows.Count);
                //        }
                //        else
                //        {
                //            //RepeaterTransferSIM.DataSource = ds.Tables[1];
                //            //RepeaterTransferSIM.DataBind();
                //            fillblankGridForSIM();
                //        }
                //    }
                //    else
                //    {
                //        fillblankGridForSIM();
                //        fillblankGridForMobileSIM();
                //    }
                //}
                //else
                //{
                //    fillblankGridForSIM();
                //    fillblankGridForMobileSIM();
                //}

            }
            catch (Exception ex)
            {

            }
        }

        protected void btnTransfer_Click(object sender, EventArgs e)
        {
            try
            {

                Boolean msisdn = false;
                Boolean blanksim = false;

                //if (chkMSISDNTransfer.Checked == true)
                //{
                //    for (int i = 0; i < RepeaterTransfer.Items.Count; i++)
                //    {
                //        CheckBox chk = (CheckBox)RepeaterTransfer.Items[i].FindControl("CheckBox2");

                //        if (chk.Checked == true)
                //        {
                //            msisdn = true;
                //            break;
                //        }

                //    }
                //}


                //if (ddlNetwork.SelectedValue == "0")
                //{
                //    ShowPopUpMsg("Please select Network");
                //    return;
                //}

                if (ddlTransferTo.SelectedValue == "0")
                {
                    ShowPopUpMsg("Please select Client");
                    return;
                }

                if (chkBlankSIMTransfer.Checked == true)
                {
                    for (int i = 0; i < RepeaterTransferSIM.Items.Count; i++)
                    {
                        CheckBox chk = (CheckBox)RepeaterTransferSIM.Items[i].FindControl("CheckBox2");

                        if (chk.Checked == true)
                        {
                            blanksim = true;
                            break;
                        }

                    }
                }
                //if (chkMSISDNTransfer.Checked == true)
                //{
                //    if (msisdn == false)
                //    {
                //        ShowPopUpMsg("Please select any MOBILE SIM");
                //        return;
                //    }
                //}

                if (chkBlankSIMTransfer.Checked == true)
                {
                    if (blanksim == false)
                    {
                        ShowPopUpMsg("Please select any SIM");
                        return;
                    }
                }

                if (chkBlankSIMTransfer.Checked == false)//chkMSISDNTransfer.Checked == false && 
                {
                    ShowPopUpMsg("Please select BLANK SIM or MOBILE SIM");
                    return;
                }


                SIM s = new SIM();

                if (ddlAction.SelectedValue == "1")
                {
                    s.TransferType = "Transfer";
                }
                else
                {
                    s.TransferType = "Accept";
                }
                int UserID = Convert.ToInt32(Session["LoginId"]);
                s.ClientID = Convert.ToInt32(Session["DistributorID"]);

                s.NewClientID = Convert.ToInt32(ddlTransferTo.SelectedValue);

                DataTable dt = new DataTable();
                dt.TableName = "dtMobile";
                dt.Columns.Add("MSISDN_SIM_ID");


                if (chkMSISDNTransfer.Checked == true)
                {
                    for (int i = 0; i < RepeaterTransfer.Items.Count; i++)
                    {
                        CheckBox chk = (CheckBox)RepeaterTransfer.Items[i].FindControl("CheckBox2");
                        HiddenField hdnMSISDN_SIM_ID = (HiddenField)RepeaterTransfer.Items[i].FindControl("hdnMSISDN_SIM_ID");
                        if (chk.Checked == true)
                        {
                            DataRow dr = dt.NewRow();


                            dr["MSISDN_SIM_ID"] = Convert.ToInt64(hdnMSISDN_SIM_ID.Value);

                            dt.Rows.Add(dr);
                            dt.AcceptChanges();
                        }

                    }
                }

                if (dt.Rows.Count == 0)
                {
                    DataRow dr = dt.NewRow();


                    dr["MSISDN_SIM_ID"] = "";

                    dt.Rows.Add(dr);
                    dt.AcceptChanges();
                }

                s.MobileDT = dt;
                //if (dt.Rows.Count == 0)
                //{
                //    ShowPopUpMsg("Please Select Mobile Number");
                //    return;
                //}



                DataTable dtSIM = new DataTable();
                dtSIM.TableName = "dtSIM";
                dtSIM.Columns.Add("SIMID");
                if (chkBlankSIMTransfer.Checked == true)
                {
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
                }
                if (dtSIM.Rows.Count == 0)
                {
                    DataRow drSIM = dtSIM.NewRow();


                    drSIM["SIMID"] = "";

                    dtSIM.Rows.Add(drSIM);
                    dtSIM.AcceptChanges();
                }

                s.SIMDt = dtSIM;

                //if (dtSIM.Rows.Count == 0)
                //{
                //    ShowPopUpMsg("Please Select SIM Number");
                //    return;
                //}
                //ankit singh- 1 or not1
                int retval = 0;


                //if (check != "1")
                //{
                string checkInventoryWAY = ConfigurationManager.AppSettings.Get("Inventory_Transfer");
                retval = ssc.InventoryTransfer(s, UserID, Actions.INSERT, checkInventoryWAY);
                //}
                //else 
                //{
                //    retval = ssc.InventoryTransfer(s, UserID, Actions.INSERT);
                //}
                if (retval > 0)
                {
                    if (ddlAction.SelectedValue == "1")
                    {
                        ShowPopUpMsg("Inventory Transfer successfully");
                    }
                    else
                    {
                        ShowPopUpMsg("Inventory Accepted successfully");
                    }


                }

                else
                {
                    ShowPopUpMsg("Error ! Please Check Data");
                }
                fillData();
                ddlAction.SelectedValue = "1";
                btnTransfer.Text = "Transfer";
                btnReject.Visible = false;
                lblTransferFromAndTo.Text = "Transfer To";
                rbSIM_NotMappped.Checked = true;

                rbSIM_NotMappped.Visible = true;
                rbSIM_MappedwithTariff.Visible = true;
                lblSIM_MappedwithTariff.Visible = true;
                lblSIM_NotMappped.Visible = true;
                chkBulkUpload.Visible = true;
                lblBulkTransfer.Visible = true;

                if (ddlNetwork.Items.Count == 2)
                {
                    ddlNetwork.SelectedIndex = 1;
                    ddlNetwork_SelectedIndexChanged(null, null);

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

        protected void ddlAction_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlAction.SelectedValue == "1")
                {
                    rbSIM_NotMappped.Visible = true;
                    rbSIM_MappedwithTariff.Visible = true;
                    lblSIM_MappedwithTariff.Visible = true;
                    lblSIM_NotMappped.Visible = true;
                    lblTransferFromAndTo.Text = "Transfer To";
                    btnTransfer.Text = "Transfer";
                    btnReject.Visible = false;

                    Distributor[] dis = null;
                    dis = ssc.GetDistributorDDLService(0, Convert.ToInt32(Session["DistributorID"]));
                    if (dis != null)
                    {
                        ddlTransferTo.DataSource = dis;
                        ddlTransferTo.DataValueField = "distributorID";
                        ddlTransferTo.DataTextField = "distributorName";
                        ddlTransferTo.DataBind();
                        ddlTransferTo.Items.Insert(0, new ListItem("---Select Client---", "0"));
                    }
                    fillData();
                    lblBulkTransfer.Visible = true;
                    chkBulkUpload.Visible = true;
                    chkBulkUpload.Checked = false;
                }
                else
                {
                    rbSIM_NotMappped.Visible = false;
                    rbSIM_MappedwithTariff.Visible = false;
                    lblSIM_MappedwithTariff.Visible = false;
                    lblSIM_NotMappped.Visible = false;
                    btnReject.Visible = true;


                    Distributor[] dis = null;
                    dis = ssc.GetDistributorDDLService(0, Convert.ToInt32(Session["DistributorID"]));
                    if (dis != null)
                    {
                        ddlTransferTo.DataSource = dis;
                        ddlTransferTo.DataValueField = "distributorID";
                        ddlTransferTo.DataTextField = "distributorName";
                        ddlTransferTo.DataBind();
                        ddlTransferTo.Items.Insert(0, new ListItem("---Select Client---", "0"));
                    }

                    lblTransferFromAndTo.Text = "Accept From";
                    btnTransfer.Text = "Accept";

                    fillblankGridForSIM();
                    fillblankGridForMobileSIM();

                    lblBulkTransfer.Visible = false;
                    chkBulkUpload.Visible = false;
                    BulkUpload.Visible = false;
                    chkBulkUpload.Checked = false;
                }
            }
            catch (Exception ex)
            {
            }
        }

        protected void ddlTransferTo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlAction.SelectedValue == "1")
                {

                }
                else
                {

                    int LoginClientID = Convert.ToInt32(Session["DistributorID"]);
                    int ClientID = Convert.ToInt32(ddlTransferTo.SelectedValue);
                    DataSet ds = ssc.GetInventoryForAccept(ClientID, LoginClientID);

                    if (ds != null)
                    {
                        if (ds.Tables.Count > 0)
                        {
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                RepeaterTransfer.DataSource = ds.Tables[0];
                                RepeaterTransfer.DataBind();
                            }
                            else
                            {
                                fillblankGridForMobileSIM();
                            }
                            if (ds.Tables[1].Rows.Count > 0)
                            {
                                RepeaterTransferSIM.DataSource = ds.Tables[1];
                                RepeaterTransferSIM.DataBind();
                            }
                            else
                            {
                                fillblankGridForSIM();
                            }
                        }
                        else
                        {
                            fillblankGridForSIM();
                            fillblankGridForMobileSIM();
                        }
                    }
                    else
                    {
                        fillblankGridForSIM();
                        fillblankGridForMobileSIM();
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            Response.Redirect("InventoryTransfer.aspx");
        }

        protected void btnReject_Click(object sender, EventArgs e)
        {
            try
            {
                Boolean msisdn = false;
                Boolean blanksim = false;


                if (ddlTransferTo.SelectedValue == "0")
                {
                    ShowPopUpMsg("Please select Client");
                    return;
                }

                if (chkMSISDNTransfer.Checked == true)
                {
                    for (int i = 0; i < RepeaterTransfer.Items.Count; i++)
                    {
                        CheckBox chk = (CheckBox)RepeaterTransfer.Items[i].FindControl("CheckBox2");

                        if (chk.Checked == true)
                        {
                            msisdn = true;
                            break;
                        }

                    }
                }
                if (chkBlankSIMTransfer.Checked == true)
                {
                    for (int i = 0; i < RepeaterTransferSIM.Items.Count; i++)
                    {
                        CheckBox chk = (CheckBox)RepeaterTransferSIM.Items[i].FindControl("CheckBox2");

                        if (chk.Checked == true)
                        {
                            blanksim = true;
                            break;
                        }

                    }
                }
                if (chkMSISDNTransfer.Checked == true)
                {
                    if (msisdn == false)
                    {
                        ShowPopUpMsg("Please select any MOBILE SIM");
                        return;
                    }
                }
                if (chkBlankSIMTransfer.Checked == true)
                {
                    if (blanksim == false)
                    {
                        ShowPopUpMsg("Please select any BLANK SIM");
                        return;
                    }
                }

                if (chkMSISDNTransfer.Checked == false && chkBlankSIMTransfer.Checked == false)
                {
                    ShowPopUpMsg("Please select BLANK SIM or MOBILE SIM");
                    return;
                }

                SIM s = new SIM();
                s.TransferType = "Reject";

                int UserID = Convert.ToInt32(Session["LoginId"]);
                s.ClientID = Convert.ToInt32(Session["DistributorID"]);
                s.NewClientID = Convert.ToInt32(ddlTransferTo.SelectedValue);

                DataTable dt = new DataTable();
                dt.TableName = "dtMobile";
                dt.Columns.Add("MSISDN_SIM_ID");

                for (int i = 0; i < RepeaterTransfer.Items.Count; i++)
                {
                    CheckBox chk = (CheckBox)RepeaterTransfer.Items[i].FindControl("CheckBox2");
                    HiddenField hdnMSISDN_SIM_ID = (HiddenField)RepeaterTransfer.Items[i].FindControl("hdnMSISDN_SIM_ID");
                    if (chk.Checked == true)
                    {
                        DataRow dr = dt.NewRow();


                        dr["MSISDN_SIM_ID"] = Convert.ToInt64(hdnMSISDN_SIM_ID.Value);

                        dt.Rows.Add(dr);
                        dt.AcceptChanges();
                    }

                }

                if (dt.Rows.Count == 0)
                {
                    DataRow dr = dt.NewRow();


                    dr["MSISDN_SIM_ID"] = "";

                    dt.Rows.Add(dr);
                    dt.AcceptChanges();
                }

                s.MobileDT = dt;
                //if (dt.Rows.Count == 0)
                //{
                //    ShowPopUpMsg("Please Select Mobile Number");
                //    return;
                //}



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
                if (dtSIM.Rows.Count == 0)
                {
                    DataRow drSIM = dtSIM.NewRow();


                    drSIM["SIMID"] = "";

                    dtSIM.Rows.Add(drSIM);
                    dtSIM.AcceptChanges();
                }

                s.SIMDt = dtSIM;

                //if (dtSIM.Rows.Count == 0)
                //{
                //    ShowPopUpMsg("Please Select SIM Number");
                //    return;
                //}
                string checkInventoryWAY = ConfigurationManager.AppSettings.Get("Inventory_Transfer");
                int retval = ssc.InventoryTransfer(s, UserID, Actions.INSERT, checkInventoryWAY);
                if (retval > 0)
                {
                    System.Text.StringBuilder javaScript = new System.Text.StringBuilder();
                    string scriptKey = "ConfirmationScript";

                    javaScript.Append("var userConfirmation = window.alert('" + "Inventory Rejected successfully" + "');\n");
                    ClientScript.RegisterStartupScript(this.GetType(), scriptKey, javaScript.ToString(), true);

                }
                else
                {

                    System.Text.StringBuilder javaScript = new System.Text.StringBuilder();
                    string scriptKey = "ConfirmationScript";

                    javaScript.Append("var userConfirmation = window.alert('" + "Check Data" + "');\n");
                    ClientScript.RegisterStartupScript(this.GetType(), scriptKey, javaScript.ToString(), true);
                }
            }
            catch (Exception ex)
            {

            }
        }

        //protected void rbSIM_MappedwithTariff_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (ddlAction.SelectedValue == "1")
        //    {
        //        int ClientID = Convert.ToInt32(Session["DistributorID"]);
        //        string Action = "MappedTariff";
        //        DataSet ds = ssc.GetInventory(ClientID, Action);
        //        ViewState["SIMPurchase"] = ds.Tables[1];
        //        if (ds != null)
        //        {
        //            if (ds.Tables.Count > 0)
        //            {
        //                if (ds.Tables[0].Rows.Count > 0)
        //                {
        //                    RepeaterTransfer.DataSource = ds.Tables[0];
        //                    RepeaterTransfer.DataBind();
        //                    lblCount.Text = Convert.ToString(ds.Tables[0].Rows.Count);
        //                }
        //                else
        //                {
        //                    //RepeaterTransfer.DataSource = ds.Tables[0];
        //                    //RepeaterTransfer.DataBind();
        //                    fillblankGridForMobileSIM();
        //                }
        //                if (ds.Tables[1].Rows.Count > 0)
        //                {
        //                    RepeaterTransferSIM.DataSource = ds.Tables[1];
        //                    RepeaterTransferSIM.DataBind();
        //                    lblCount.Text = Convert.ToString(ds.Tables[1].Rows.Count);

        //                }
        //                else
        //                {
        //                    //RepeaterTransferSIM.DataSource = ds.Tables[1];
        //                    //RepeaterTransferSIM.DataBind();
        //                    fillblankGridForSIM();
        //                }
        //            }
        //            else
        //            {
        //                fillblankGridForSIM();
        //                fillblankGridForMobileSIM();
        //            }
        //        }
        //        else
        //        {
        //            fillblankGridForSIM();
        //            fillblankGridForMobileSIM();
        //        }
        //    }
        //    else
        //    {
        //        int LoginClientID = Convert.ToInt32(Session["DistributorID"]);
        //        int ClientID = Convert.ToInt32(ddlTransferTo.SelectedValue);
        //        DataSet ds = ssc.GetInventoryForAccept(ClientID, LoginClientID);

        //        if (ds != null)
        //        {
        //            if (ds.Tables.Count > 0)
        //            {
        //                if (ds.Tables[0].Rows.Count > 0)
        //                {
        //                    RepeaterTransfer.DataSource = ds.Tables[0];
        //                    RepeaterTransfer.DataBind();
        //                    lblCount.Text = Convert.ToString(ds.Tables[0].Rows.Count);
        //                }
        //                else
        //                {
        //                    fillblankGridForMobileSIM();
        //                }
        //                if (ds.Tables[1].Rows.Count > 0)
        //                {
        //                    RepeaterTransferSIM.DataSource = ds.Tables[1];
        //                    RepeaterTransferSIM.DataBind();
        //                    lblCount.Text = Convert.ToString(ds.Tables[1].Rows.Count);
        //                }
        //                else
        //                {
        //                    fillblankGridForSIM();
        //                }
        //            }
        //            else
        //            {
        //                fillblankGridForSIM();
        //                fillblankGridForMobileSIM();
        //            }
        //        }
        //        else
        //        {
        //            fillblankGridForSIM();
        //            fillblankGridForMobileSIM();
        //        }
        //    }
        //    ddlNetwork_SelectedIndexChanged(null, null);

        //}

        //protected void rbSIM_NotMappped_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (ddlAction.SelectedValue == "1")
        //    {
        //        int ClientID = Convert.ToInt32(Session["DistributorID"]);
        //        string Action = "NotMappedTariff";
        //        DataSet ds = ssc.GetInventory(ClientID, Action);

        //        ViewState["SIMPurchase"] = ds.Tables[1];

        //        if (ds != null)
        //        {
        //            if (ds.Tables.Count > 0)
        //            {
        //                if (ds.Tables[0].Rows.Count > 0)
        //                {
        //                    RepeaterTransfer.DataSource = ds.Tables[0];
        //                    RepeaterTransfer.DataBind();
        //                    lblCount.Text = Convert.ToString(ds.Tables[0].Rows.Count);
        //                }
        //                else
        //                {
        //                    //RepeaterTransfer.DataSource = ds.Tables[0];
        //                    //RepeaterTransfer.DataBind();
        //                    fillblankGridForMobileSIM();
        //                }
        //                if (ds.Tables[1].Rows.Count > 0)
        //                {
        //                    RepeaterTransferSIM.DataSource = ds.Tables[1];
        //                    RepeaterTransferSIM.DataBind();
        //                    lblCount.Text = Convert.ToString(ds.Tables[1].Rows.Count);
        //                }
        //                else
        //                {
        //                    //RepeaterTransferSIM.DataSource = ds.Tables[1];
        //                    //RepeaterTransferSIM.DataBind();
        //                    fillblankGridForSIM();
        //                }
        //            }
        //            else
        //            {
        //                fillblankGridForSIM();
        //                fillblankGridForMobileSIM();
        //            }
        //        }
        //        else
        //        {
        //            fillblankGridForSIM();
        //            fillblankGridForMobileSIM();
        //        }
        //    }
        //    else
        //    {
        //        int LoginClientID = Convert.ToInt32(Session["DistributorID"]);
        //        int ClientID = Convert.ToInt32(ddlTransferTo.SelectedValue);
        //        DataSet ds = ssc.GetInventoryForAccept(ClientID, LoginClientID);

        //        if (ds != null)
        //        {
        //            if (ds.Tables.Count > 0)
        //            {
        //                if (ds.Tables[0].Rows.Count > 0)
        //                {
        //                    RepeaterTransfer.DataSource = ds.Tables[0];
        //                    RepeaterTransfer.DataBind();
        //                }
        //                else
        //                {
        //                    fillblankGridForMobileSIM();
        //                }
        //                if (ds.Tables[1].Rows.Count > 0)
        //                {
        //                    RepeaterTransferSIM.DataSource = ds.Tables[1];
        //                    RepeaterTransferSIM.DataBind();
        //                    lblCount.Text = Convert.ToString(ds.Tables[1].Rows.Count);
        //                }
        //                else
        //                {
        //                    fillblankGridForSIM();
        //                }
        //            }
        //            else
        //            {
        //                fillblankGridForSIM();
        //                fillblankGridForMobileSIM();
        //            }
        //        }
        //        else
        //        {
        //            fillblankGridForSIM();
        //            fillblankGridForMobileSIM();
        //        }
        //    }

        //    ddlNetwork_SelectedIndexChanged(null, null);

        //}

        protected void btnUploadFile_Click(object sender, EventArgs e)
        {
            ViewState["objDTSIM"] = null;


            FileUpload fuBulk = null;
            fuBulk = fuBulkUpload;
            UploadFile(fuBulk);

            int ClientID = Convert.ToInt32(Session["DistributorID"]);
            DataTable dt = (DataTable)ViewState["objDTSIM"];
            DataSet ds = ssc.GetInventoryBulkTransfer(ClientID, dt);
            if (ds != null)
            {
                RepeaterTransferSIM.DataSource = ds.Tables[0];
                RepeaterTransferSIM.DataBind();

                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < RepeaterTransferSIM.Items.Count; i++)
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
                    if (columnNames.Length != 1)
                    {
                        //lblMsg.Text = "Some column mistmatch in mobile bulk upload file";
                        iInvalidFileFormat = 1;
                        ScriptManager.RegisterStartupScript(this, Page.GetType(), "File Column Mismatch", "alert('SIM bulk upload File should contains 4 columns,please check your file')", true);
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
            }
            else
            {
                BulkUpload.Visible = false;
            }
        }

        protected void ddlNetwork_SelectedIndexChanged(object sender, EventArgs e)
        {

            try
            {
                if (ddlNetwork.SelectedValue == "13")
                {
                    txtFromSim.Attributes.Remove("disabled");
                    txtTosim.Attributes.Remove("disabled");
                    txtFromSim.Style.Clear();
                    txtTosim.Style.Clear();
                    TxtSimNumb.Text = "8919601";
                    TxtSimNumb.MaxLength = 19;
                }
                else if (ddlNetwork.SelectedValue == "15")
                {
                    txtFromSim.Attributes.Add("disabled", "disabled");
                    txtTosim.Attributes.Add("disabled", "disabled");
                    txtFromSim.Style.Add("background-color", "#d4d4d4");
                    txtTosim.Style.Add("background-color", "#d4d4d4");
                    txtFromSim.Style.Add("color", "white");
                    txtTosim.Style.Add("color", "white");
                    TxtSimNumb.Text = "";
                    TxtSimNumb.MaxLength = 20;
                }

                RepeaterTransferSIM.DataSource = null;
                RepeaterTransferSIM.DataBind();
                lblCount.Text = "";
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        protected void btnSearch_Click(object sender, EventArgs e)
        {
            int NetworkID = Convert.ToInt16(ddlNetwork.SelectedValue);
            if (rbSIM_NotMappped.Checked == true || rbSIM_MappedwithTariff.Checked == true)
            {
                DataSet ds = new DataSet();
                string Action = "";
                int ClientID = Convert.ToInt32(Session["DistributorID"]);
                if (rbSIM_NotMappped.Checked == true)
                {
                    Action = "NotMappedTariff";
                }

                else
                {
                    Action = "MappedTariff";
                }
                //ankit For Lyca AND H2O
                if (ddlNetwork.SelectedValue == "13")
                {
                    /////
                    if ((TxtSimNumb.Text == "" || TxtSimNumb.Text == "8919601") && (txtFromSim.Text == "" || txtFromSim.Text == "8919601") && (txtTosim.Text == "" || txtTosim.Text == "8919601"))
                    {
                        fillblankGridForSIM();
                        fillblankGridForMobileSIM();
                        lblCount.Text = "";
                        return;

                    }
                    /////
                    if (TxtSimNumb.Text != "" && (txtFromSim.Text == "" || txtFromSim.Text == "8919601") && (txtTosim.Text == "" || txtTosim.Text == "8919601"))
                    {
                        ds = ssc.FetchInventory(ClientID, Action, Convert.ToString(TxtSimNumb.Text), "", "", NetworkID);
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            RepeaterTransferSIM.DataSource = ds.Tables[0];
                            RepeaterTransferSIM.DataBind();
                            lblCount.Text = Convert.ToString(ds.Tables[0].Rows.Count);
                            for (int i = 0; i < RepeaterTransferSIM.Items.Count; i++)
                            {
                                CheckBox chk = (CheckBox)RepeaterTransferSIM.Items[i].FindControl("CheckBox2");
                                chk.Checked = true;
                            }
                        }

                        else
                        {
                            ShowPopUpMsg("This sim is not available in our inventory");
                        }
                    }

                    else if ((TxtSimNumb.Text == "" || TxtSimNumb.Text == "8919601") && txtFromSim.Text != "" && txtTosim.Text != "")
                    {
                        ds = ssc.FetchInventory(ClientID, Action, "", Convert.ToString(txtFromSim.Text), Convert.ToString(txtTosim.Text), NetworkID);
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            RepeaterTransferSIM.DataSource = ds.Tables[0];
                            RepeaterTransferSIM.DataBind();
                            lblCount.Text = Convert.ToString(ds.Tables[0].Rows.Count);
                            for (int i = 0; i < RepeaterTransferSIM.Items.Count; i++)
                            {
                                CheckBox chk = (CheckBox)RepeaterTransferSIM.Items[i].FindControl("CheckBox2");
                                chk.Checked = true;
                            }
                        }

                        else
                        {
                            ShowPopUpMsg("This sim is not available in our inventory");
                        }
                    }
                }

                else if (ddlNetwork.SelectedValue == "15")
                {
                    txtFromSim.Attributes.Add("disabled", "disabled");
                    txtTosim.Attributes.Add("disabled", "disabled");
                    string SimNumber = "";
                   
                    if (TxtSimNumb.Text != "")
                    {
                        SimNumber = TxtSimNumb.Text.Trim();
                    }

                    ds = ssc.FetchInventory(ClientID, Action, "", "", SimNumber, NetworkID);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        RepeaterTransferSIM.DataSource = ds.Tables[0];
                        RepeaterTransferSIM.DataBind();
                        lblCount.Text = Convert.ToString(ds.Tables[0].Rows.Count);
                        for (int i = 0; i < RepeaterTransferSIM.Items.Count; i++)
                        {
                            CheckBox chk = (CheckBox)RepeaterTransferSIM.Items[i].FindControl("CheckBox2");
                            chk.Checked = true;
                        }
                    }

                    else
                    {
                        ShowPopUpMsg("This SIM is not available in our H2O inventory");
                    }
                }
            }
            else
            {
                ShowPopUpMsg("Please Select Transfer Blank SIM/ Transfer Preloaded SIM");
            }

        }

        protected void btnResetFeilds_Click(object sender, EventArgs e)
        {
            TxtSimNumb.Text = "8919601";
            txtFromSim.Text = "8919601";
            txtTosim.Text = "8919601";

        }
    }
}
