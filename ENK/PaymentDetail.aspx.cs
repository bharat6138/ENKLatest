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
using System.Configuration;
using System.Net;
using System.IO;


namespace ENK
{
    public partial class PaymentDetail : System.Web.UI.Page
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
                    BindDDL();

                    DateTime today = DateTime.Today;
                    int numberOfDaysInMonth = DateTime.DaysInMonth(today.Year, today.Month);

                    DateTime FromDate = new DateTime(today.Year, today.Month, 1);
                    DateTime ToDate = new DateTime(today.Year, today.Month, numberOfDaysInMonth);


                    //DateTime FromDate = DateTime.Now;
                    //DateTime ToDate = DateTime.Now;
                    txtFromDate.Text = Convert.ToString(FromDate.ToString("MM-dd-yyyy"));
                    txtToDate.Text = Convert.ToString(ToDate.ToString("MM-dd-yyyy"));


                    int DistributorID = Convert.ToInt32(Session["DistributorID"]);
                    int ClientTypeID = Convert.ToInt32(Session["ClientTypeID"]);
                    int LoginID = Convert.ToInt32(Session["LoginId"]);


                    DataSet ds = ssc.GetTopupPaymentDetailsService(DistributorID, LoginID, ClientTypeID, txtFromDate.Text, txtToDate.Text);

                    if (ds != null)
                    {
                        ViewState["tblexcel"] = ds.Tables[0];
                        RepeaterTransfer.DataSource = ds.Tables[0];
                        RepeaterTransfer.DataBind();
                        for (int i = 0; i < RepeaterTransfer.Items.Count; i++)
                        {
                            Label lblTransactionStatus = (Label)RepeaterTransfer.Items[i].FindControl("lblTransactionStatus");
                            LinkButton lnkChangeStatus = (LinkButton)RepeaterTransfer.Items[i].FindControl("lnkChangeStatus");
                            // Button ChangeStatus = (Button)RepeaterTransfer.Items[i].FindControl("ChangeStatus");
                            /////
                            if (lblTransactionStatus.Text == "Pending")
                            {
                                lnkChangeStatus.Visible = true;
                            }
                            else
                            {
                                lnkChangeStatus.Visible = false;
                            }
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
                ddlDistributor.Items.Insert(0, new ListItem("---Select---", "0"));
            }
            catch (Exception ex)
            {

            }

        }


        protected void btnExportToExcel_Click(object sender, EventArgs e)
        {
            //Response.Clear();

            //Response.AddHeader("content-disposition", "attachment;filename = PaymentDetail.xls");

            //Response.ContentType = "application/vnd.xls";

            //System.IO.StringWriter stringWrite = new System.IO.StringWriter();

            //System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);

            //RepeaterTransfer.RenderControl(htmlWrite);

            //Response.Write(stringWrite.ToString());

            //Response.End();

            DataTable dt = (DataTable)ViewState["tblexcel"];
            dt.Columns.Remove("paymentId");
            dt.Columns.Remove("PaymentMode");
            dt.Columns.Remove("TransactionStatusId");
            dt.Columns.Remove("TxnAmount");
            dt.Columns.Remove("PaymentDate1");
            dt.Columns["TxnId"].SetOrdinal(0);
            dt.Columns["ChargedAmount"].SetOrdinal(1);
            dt.Columns["TransactionStatus"].SetOrdinal(1);

            if (dt.Rows.Count > 0)
            {
                string filename = "PaymentDetail.xls";
                System.IO.StringWriter tw = new System.IO.StringWriter();
                System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);
                DataGrid gridexcel = new DataGrid();
                gridexcel.DataSource = dt;
                gridexcel.DataBind();

                //Get the HTML for the control.
                gridexcel.RenderControl(hw);
                //Write the HTML back to the browser.
                Response.ContentType = "application/vnd.ms-excel";
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename + "");
                this.EnableViewState = false;
                Response.Write(tw.ToString());
                Response.End();
            }
            popupdiv.Visible = false;
        }

        //Ankit singh
        public override void VerifyRenderingInServerForm(Control control)
        {
            return;
        }
        protected void btnGet_Click(object sender, EventArgs e)
        {
            DateTime FromDate;
            DateTime ToDate;

            int DistributorID = Convert.ToInt32(Session["DistributorID"]);
            int ClientTypeID = Convert.ToInt32(Session["ClientTypeID"]);
            int LoginID = Convert.ToInt32(Session["LoginId"]);

            //if (txtFromDate.Text.Trim() == "" && txtToDate.Text.Trim() == "")
            //{
            //    FromDate = Convert.ToDateTime("1900-01-01");
            //    ToDate = DateTime.Now;

            //    DataSet ds = ssc.GetTopupPaymentDetailsService(Convert.ToInt32(ddlDistributor.SelectedValue), LoginID,0, FromDate, ToDate);

            //    if (ds != null)
            //    {
            //        RepeaterTransfer.DataSource = ds.Tables[0];
            //        RepeaterTransfer.DataBind();
            //    }

            //}
            //else if (txtFromDate.Text.Trim() != "" && txtToDate.Text.Trim() == "")
            //{
            //    FromDate = Convert.ToDateTime("1900-01-01");
            //    ToDate = DateTime.Now;

            //    DataSet ds = ssc.GetTopupPaymentDetailsService(Convert.ToInt32(ddlDistributor.SelectedValue), LoginID, 0, FromDate, ToDate);

            //    if (ds != null)
            //    {
            //        RepeaterTransfer.DataSource = ds.Tables[0];
            //        RepeaterTransfer.DataBind();
            //    }
            //}
            if (txtFromDate.Text.Trim() != "" && txtToDate.Text.Trim() != "")
            {
                FromDate = Convert.ToDateTime(txtFromDate.Text.Trim());
                ToDate = Convert.ToDateTime(txtToDate.Text.Trim());

                txtFromDate.Text = Convert.ToString(FromDate.ToString("MM-dd-yyyy"));
                txtToDate.Text = Convert.ToString(ToDate.ToString("MM-dd-yyyy"));
                DataSet ds = new DataSet();
                if (ddlDistributor.SelectedIndex == 0)
                {
                    ds = ssc.GetTopupPaymentDetailsService(Convert.ToInt32(DistributorID), LoginID, ClientTypeID, txtFromDate.Text.Trim(), txtToDate.Text.Trim());
                }
                else
                {
                    ds = ssc.GetTopupPaymentDetailsService(Convert.ToInt32(ddlDistributor.SelectedValue), LoginID, ClientTypeID, txtFromDate.Text.Trim(), txtToDate.Text.Trim());
                }
                //if (ds != null)
                //{
                ViewState["tblexcel"] = ds.Tables[0];
                RepeaterTransfer.DataSource = ds.Tables[0];
                RepeaterTransfer.DataBind();
                //for (int i = 0; i < RepeaterTransfer.Items.Count; i++)
                //{
                //    Label lblTransactionStatus = (Label)RepeaterTransfer.Items[i].FindControl("lblTransactionStatus");
                //    LinkButton lnkChangeStatus = (LinkButton)RepeaterTransfer.Items[i].FindControl("lnkChangeStatus");
                //    if (lblTransactionStatus.Text == "Pending")
                //    {
                //        lnkChangeStatus.Visible = true;
                //    }
                //    else
                //    {
                //        lnkChangeStatus.Visible = false;
                //    }
                //}
                //}
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
                    long PaymentID = Convert.ToInt32(e.CommandArgument);
                    Label lblChargedAmount = (Label)RepeaterTransfer.FindControl("lblChargedAmount");

                    int retval = 0;
                    //ssc.ChangeStatusForTopUp(Convert.ToInt32(Session["DistributorID"]), Convert.ToInt32(Session["LoginId"]), Convert.ToInt32(Session["ClientTypeID"]), PaymentID);
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

                            txtFromDate.Text = Convert.ToString(FromDate.ToString("MM-dd-yyyy"));
                            txtToDate.Text = Convert.ToString(ToDate.ToString("MM-dd-yyyy"));


                        }
                        else
                        {
                            DateTime today = DateTime.Today;
                            int numberOfDaysInMonth = DateTime.DaysInMonth(today.Year, today.Month);

                            FromDate = new DateTime(today.Year, today.Month, 1);
                            ToDate = new DateTime(today.Year, today.Month, numberOfDaysInMonth);

                            txtFromDate.Text = Convert.ToString(FromDate.ToString("MM-dd-yyyy"));
                            txtToDate.Text = Convert.ToString(ToDate.ToString("MM-dd-yyyy"));
                        }
                        DataSet ds = ssc.GetTopupPaymentDetailsService(Distributor, Convert.ToInt32(Session["LoginId"]), ClientTypeID, txtFromDate.Text.Trim(), txtToDate.Text.Trim());

                        if (ds != null)
                        {
                            RepeaterTransfer.DataSource = ds.Tables[0];
                            RepeaterTransfer.DataBind();
                            //for (int i = 0; i < RepeaterTransfer.Items.Count; i++)
                            //{
                            //    Label lblTransactionStatus = (Label)RepeaterTransfer.Items[i].FindControl("lblTransactionStatus");
                            //    LinkButton lnkChangeStatus = (LinkButton)RepeaterTransfer.Items[i].FindControl("lnkChangeStatus");
                            //    if (lblTransactionStatus.Text == "Pending")
                            //    {
                            //        lnkChangeStatus.Visible = true;
                            //    }
                            //    else
                            //    {
                            //        lnkChangeStatus.Visible = false;
                            //    }
                            //}
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

        protected void btnSuccess_Click(object sender, EventArgs e)
        {
            long paytid = Convert.ToInt64(this.HiddenField1.Value);
            // add by akash starts
            string SRemark = Convert.ToString(txtStatusRemark.Text);
            if (SRemark == "" || SRemark == null)
            {
                ShowPopUpMsg("Please Fill Remark and try again.");
                return;
            }
            // add by akash end
            int retval = ssc.ChangeStatusForTopUp(Convert.ToInt32(Session["DistributorID"]), Convert.ToInt32(Session["LoginId"]), Convert.ToInt32(Session["ClientTypeID"]), paytid, 1, SRemark);
            if (retval > 0)
            {
                ShowPopUpMsg("Change Status Successfully[Success].");
                txtStatusRemark.Text = "";
                Response.Redirect("PaymentDetail.aspx", false);
            }
            int DistributorID = Convert.ToInt32(Session["DistributorID"]);
            int ClientTypeID = Convert.ToInt32(Session["ClientTypeID"]);
            int LoginID = Convert.ToInt32(Session["LoginId"]);
            DataSet ds = ssc.GetTopupPaymentDetailsService(DistributorID, LoginID, ClientTypeID, txtFromDate.Text, txtToDate.Text);
            if (ds != null)
            {
                RepeaterTransfer.DataSource = ds.Tables[0];
                RepeaterTransfer.DataBind();

            }

        }

        protected void btnFail_Click(object sender, EventArgs e)
        {
            long paytid = Convert.ToInt64(this.HiddenField1.Value);
            // add by akash starts
            string SRemark = Convert.ToString(txtStatusRemark.Text);
            if (SRemark == "" || SRemark == null)
            {
                ShowPopUpMsg("Please Fill Remark and try again.");
                return;
            }
            // add by akash end
            int retval = ssc.ChangeStatusForTopUp(Convert.ToInt32(Session["DistributorID"]), Convert.ToInt32(Session["LoginId"]), Convert.ToInt32(Session["ClientTypeID"]), paytid, 0, SRemark);
            if (retval > 0)
            {
                ShowPopUpMsg("Change Status Successfully[Fail].");
                txtStatusRemark.Text = "";
            }
            int DistributorID = Convert.ToInt32(Session["DistributorID"]);
            int ClientTypeID = Convert.ToInt32(Session["ClientTypeID"]);
            int LoginID = Convert.ToInt32(Session["LoginId"]);
            DataSet ds = ssc.GetTopupPaymentDetailsService(DistributorID, LoginID, ClientTypeID, txtFromDate.Text, txtToDate.Text);
            if (ds != null)
            {
                RepeaterTransfer.DataSource = ds.Tables[0];
                RepeaterTransfer.DataBind();

            }
        }
    }
}