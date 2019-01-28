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

namespace ENK
{
    public partial class ActivationFailedReport : System.Web.UI.Page
    {
        Service1Client ssc = new Service1Client();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                txtFromDate.Attributes.Add("readonly", "true");
                txtToDate.Attributes.Add("readonly", "true");

                if (!Page.IsPostBack)
                {
                    if (Session["LoginId"] != null)
                    {
                        ////////////////////////////
                        DataTable dt = ssc.GetVendor(Convert.ToInt32(Session["LoginId"]));
                        if (dt != null)
                        {
                            if (dt.Rows.Count > 0)
                            {
                                ddlNetwork.DataSource = dt;
                                ddlNetwork.DataValueField = "VendorID";
                                ddlNetwork.DataTextField = "VendorName";
                                ddlNetwork.DataBind();
                                ddlNetwork.SelectedIndex = 1;

                            }
                        }
                        ////////////////////////////
                        BindDDL();
                        int LoginID = Convert.ToInt32(Session["LoginId"]);
                        int distributorID = Convert.ToInt32(Session["DistributorID"]);
                        int ClientTypeID = Convert.ToInt32(Session["ClientTypeID"]);
                        //DateTime FromDate = DateTime.Now;
                        //DateTime ToDate = DateTime.Now;

                        DateTime today = DateTime.Today;
                        int numberOfDaysInMonth = DateTime.DaysInMonth(today.Year, today.Month);

                        DateTime FromDate = new DateTime(today.Year, today.Month, 1);
                        DateTime ToDate = new DateTime(today.Year, today.Month, numberOfDaysInMonth);

                        txtFromDate.Text = Convert.ToString(FromDate.ToString("dd-MMM-yyyy"));
                        txtToDate.Text = Convert.ToString(ToDate.ToString("dd-MMM-yyyy"));

                        DataSet ds = ssc.GetReportAcivationFail(distributorID, 1, LoginID, FromDate, ToDate);

                        if (ds != null)
                        {
                            ViewState["SIMPurchase"] = ds.Tables[0];
                            RepeaterTransfer.DataSource = ds.Tables[0];
                            RepeaterTransfer.DataBind();
                        }
                    }

                }
            }
            catch (Exception ex)
            {

            }
        }

        public void BindDDL()
        {
            try
            {
                int userid = Convert.ToInt32(Session["LoginId"]);
                int distributorID = Convert.ToInt32(Session["DistributorID"]);
                Distributor[] ds = ssc.GetDistributorDDLService(userid, distributorID);

                ddlDistributor.DataSource = ds;
                ddlDistributor.DataValueField = "distributorID";
                ddlDistributor.DataTextField = "distributorName";
                ddlDistributor.DataBind();
                ddlDistributor.Items.Insert(0, new ListItem("ALL", "0"));


                DataTable dt = ssc.GetVendor(Convert.ToInt32(Session["LoginId"]));
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        ddlNetwork.DataSource = dt;
                        ddlNetwork.DataValueField = "VendorID";
                        ddlNetwork.DataTextField = "VendorName";
                        ddlNetwork.DataBind();
                        //ddlNetwork.Items.Insert(0, new ListItem("All Network", "0"));
                    }
                }



            }
            catch (Exception ex)
            {

            }

        }

        protected void btnGet_Click(object sender, EventArgs e)
        {
            DateTime FromDate;
            DateTime ToDate;

            int DistributorID = Convert.ToInt32(Session["DistributorID"]);
            int ClientTypeID = Convert.ToInt32(Session["ClientTypeID"]);
            int LoginID = Convert.ToInt32(Session["LoginId"]);

            if (txtFromDate.Text.Trim() == "" && txtToDate.Text.Trim() == "")
            {
                FromDate = Convert.ToDateTime("1900-01-01");
                ToDate = DateTime.Now;
                int distributorID = Convert.ToInt32(Session["DistributorID"]);
                //   DataSet ds1 = ssc.GetReportAcivationFail(distributorID, 1, LoginID, FromDate, ToDate);
                DataSet ds = ssc.GetReportAcivationFail(Convert.ToInt32(ddlDistributor.SelectedValue), ClientTypeID, LoginID, FromDate, ToDate);

                if (ds != null)
                {
                    ViewState["SIMPurchase"] = ds.Tables[0];
                    RepeaterTransfer.DataSource = ds.Tables[0];
                    RepeaterTransfer.DataBind();
                }

            }
            else if (txtFromDate.Text.Trim() != "" && txtToDate.Text.Trim() == "")
            {
                FromDate = Convert.ToDateTime("1900-01-01");
                ToDate = DateTime.Now;

                DataSet ds = ssc.GetReportAcivationFail(Convert.ToInt32(ddlDistributor.SelectedValue), ClientTypeID, LoginID, FromDate, ToDate);

                if (ds != null)
                {
                    ViewState["SIMPurchase"] = ds.Tables[0];
                    RepeaterTransfer.DataSource = ds.Tables[0];
                    RepeaterTransfer.DataBind();
                }
            }
            else if (txtFromDate.Text.Trim() != "" && txtToDate.Text.Trim() != "")
            {
                FromDate = Convert.ToDateTime(txtFromDate.Text.Trim());
                ToDate = Convert.ToDateTime(txtToDate.Text.Trim());

                //DataSet ds = ssc.GetReportRechargeSIM(Convert.ToInt32(ddlDistributor.SelectedValue), 0, LoginID, FromDate, ToDate);
                DataSet ds;
                if (ddlDistributor.SelectedValue == "0")
                {
                    ds = ssc.GetReportAcivationFail(DistributorID, ClientTypeID, LoginID, FromDate, ToDate);//ssc.GetSaleReportForActivationAndPortIn(DistributorID, 1, FromDate, ToDate);

                }
                else
                {
                    ds = ssc.GetReportAcivationFail(Convert.ToInt32(ddlDistributor.SelectedValue), ClientTypeID, LoginID, FromDate, ToDate);//ssc.GetSaleReportForActivationAndPortIn(Convert.ToInt32(ddlDistributor.SelectedValue), 0, FromDate, ToDate);


                }

                if (ds != null)
                {
                    ViewState["SIMPurchase"] = ds.Tables[0];
                    RepeaterTransfer.DataSource = ds.Tables[0];
                    RepeaterTransfer.DataBind();
                }
            }

            ddlNetwork_SelectedIndexChanged(null, null);
        }

        protected void btnExportToExcel_Click(object sender, EventArgs e)
        {
            DateTime FromDate = Convert.ToDateTime(txtFromDate.Text.Trim());
            DateTime ToDate = Convert.ToDateTime(txtToDate.Text.Trim());

            int DistributorID = Convert.ToInt32(Session["DistributorID"]);
            int ClientTypeID = Convert.ToInt32(Session["ClientTypeID"]);
            int LoginID = Convert.ToInt32(Session["LoginId"]);

            //DataSet ds = ssc.GetReportRechargeSIM(Convert.ToInt32(ddlDistributor.SelectedValue), 0, LoginID, FromDate, ToDate);
            DataSet ds;
            if (ddlDistributor.SelectedValue == "0")
            {
                ds = ssc.GetReportAcivationFail(DistributorID, 1, LoginID, FromDate, ToDate);//ssc.GetSaleReportForActivationAndPortIn(DistributorID, 1, FromDate, ToDate);
            }
            else
            {
                ds = ssc.GetReportAcivationFail(Convert.ToInt32(ddlDistributor.SelectedValue), 0, LoginID, FromDate, ToDate);//ssc.GetSaleReportForActivationAndPortIn(Convert.ToInt32(ddlDistributor.SelectedValue), 0, FromDate, ToDate);
            }

            //DataTable dtMain = new DataTable();
            if (ds != null)
            {
                DataTable dt = ds.Tables[0];

                if (ddlNetwork.SelectedIndex > 0)
                {


                    DataView dv = dt.DefaultView;
                    dv.RowFilter = "VendorID = " + Convert.ToInt32(ddlNetwork.SelectedValue);
                    dt = dv.ToTable();

                }




                if (dt.Rows.Count > 0)
                {
                    DataView view = new DataView(dt);

                    DataTable dtExcel = view.ToTable("Selected", false, "RechargeStatus", "RechargeVia", "TariffCode", "TariffPlan", "PlanAmount", "VendorName", "Distributor", "SimNumber", "Amount", "Tax", "W/Tax", "State", "ZipCode", "TxnID", "PinNumber", "RechargeDate", "Regulatery");//, "INTERNATIONAL_BUNDLE_CODE", "INTERNATIONAL_BUNDLE_AMOUNT"

                    if (dtExcel.Rows.Count > 0)
                    {
                        string filename = "ActivationFailedReport.xls";
                        System.IO.StringWriter tw = new System.IO.StringWriter();
                        System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);
                        GridView grdView = new GridView();
                        //dgGrid.HeaderStyle
                        grdView.DataSource = dtExcel;
                        grdView.DataBind();

                        ScriptManager.RegisterStartupScript(this, GetType(), "", "HideProgress();", true);
                        //Get the HTML for the control.
                        grdView.RenderControl(hw);
                        //Write the HTML back to the browser.
                        //Response.ContentType = application/vnd.ms-excel;
                        Response.ContentType = "application/vnd.ms-excel";
                        Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename + "");
                        this.EnableViewState = false;
                        Response.Write(tw.ToString());
                        Response.End();

                        ScriptManager.RegisterStartupScript(this, GetType(), "", "HideProgress();", true);
                    }

                }
            }



        }

        protected void ddlNetwork_SelectedIndexChanged(object sender, EventArgs e)
        {

            try
            {
                DataTable dtSIMPurchase = new DataTable();

                if (ddlNetwork.SelectedIndex > 0)
                {

                    dtSIMPurchase = (DataTable)ViewState["SIMPurchase"];
                    DataView dv = dtSIMPurchase.DefaultView;
                    dv.RowFilter = "VendorID = " + Convert.ToInt32(ddlNetwork.SelectedValue);
                    dtSIMPurchase = dv.ToTable();

                    RepeaterTransfer.DataSource = dtSIMPurchase;
                    RepeaterTransfer.DataBind();
                }
                else
                {
                    dtSIMPurchase = (DataTable)ViewState["SIMPurchase"];
                    RepeaterTransfer.DataSource = dtSIMPurchase;
                    RepeaterTransfer.DataBind();


                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        protected void btnShow_Click(object sender, EventArgs e)
        {
            try
            {
                int DistributorID = Convert.ToInt32(Session["DistributorID"]);
                int ClientTypeID = Convert.ToInt32(Session["ClientTypeID"]);
                int LoginID = Convert.ToInt32(Session["LoginId"]);


                int distributorID = Convert.ToInt32(Session["DistributorID"]);
                DataSet ds;
                if (ddlDistributor.SelectedValue == "0")
                {
                    // ds = ssc.GetReportAcivationFail(DistributorID, 1, LoginID, FromDate, ToDate, Convert.ToString(ddlRechargeVia.SelectedItem.Text));//ssc.GetSaleReportForActivationAndPortIn(DistributorID, 1, FromDate, ToDate);
                    ds = ssc.GetReportRechargeFilterwise(Convert.ToInt32(DistributorID), ClientTypeID, LoginID, txtMobileNo.Text.ToString(), txnID.Text.ToString(), Convert.ToString(ddlRechargeVia.SelectedItem.Text));

                }
                else
                {
                    //ds = ssc.GetReportAcivationFail(Convert.ToInt32(ddlDistributor.SelectedValue), 0, LoginID, FromDate, ToDate, Convert.ToString(ddlRechargeVia.SelectedItem.Text));//ssc.GetSaleReportForActivationAndPortIn(Convert.ToInt32(ddlDistributor.SelectedValue), 0, FromDate, ToDate);
                    ds = ssc.GetReportRechargeFilterwise(Convert.ToInt32(ddlDistributor.SelectedValue), ClientTypeID, LoginID, txtMobileNo.Text.ToString(), txnID.Text.ToString(), Convert.ToString(ddlRechargeVia.SelectedItem.Text));


                }

                if (ds != null)
                {
                    ViewState["SIMPurchase"] = ds.Tables[0];
                    RepeaterTransfer.DataSource = ds.Tables[0];
                    RepeaterTransfer.DataBind();
                }

            }
            catch (Exception EX)
            {

                throw EX;
            }
        }
        protected void RepeaterTransfer_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            try
            {


                //int index = e.Item;
                DateTime FromDate;
                DateTime ToDate;
                int Distributor = 0;
                int ClientTypeID = Convert.ToInt32(Session["ClientTypeID"]);
                if (e.CommandName == "ChangeStatus")
                {
                    long ActivationID = Convert.ToInt32(e.CommandArgument);

                    Label lblChargedAmount = (Label)RepeaterTransfer.FindControl("lblChargedAmount");
                    int retval = 0;
                    retval = ssc.UpdateStatusActivation(Convert.ToInt32(ActivationID), Convert.ToInt32(Session["LoginId"]));
                    if (retval > 0)
                    {
                        ShowPopUpMsg("Change Status Successfully.");

                        if (ddlDistributor.SelectedValue == "0")
                        {
                            Distributor = 1;
                        }
                        else
                        {
                            Distributor = Convert.ToInt32(ddlDistributor.SelectedValue);
                        }
                        if (txtFromDate.Text.Trim() != "" && txtToDate.Text.Trim() != "")
                        {
                            FromDate = Convert.ToDateTime(txtFromDate.Text.Trim());
                            ToDate = Convert.ToDateTime(txtToDate.Text.Trim());


                        }
                        else
                        {
                            DateTime today = DateTime.Today;
                            int numberOfDaysInMonth = DateTime.DaysInMonth(today.Year, today.Month);

                            FromDate = new DateTime(today.Year, today.Month, 1);
                            ToDate = new DateTime(today.Year, today.Month, numberOfDaysInMonth);
                        }
                        DataSet ds = ssc.GetReportAcivationFail(Distributor, ClientTypeID, Convert.ToInt32(Session["LoginId"]), FromDate, ToDate);
                        {
                            RepeaterTransfer.DataSource = ds.Tables[0];
                            RepeaterTransfer.DataBind();

                        }
                    }
                    else
                    {
                        ShowPopUpMsg("Error! Please Check.");
                    }

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
    }
}