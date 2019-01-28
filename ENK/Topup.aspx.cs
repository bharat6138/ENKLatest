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

namespace ENK
{
    public partial class Topup : System.Web.UI.Page
    {
        Service1Client svc = new Service1Client();
        string topup = "0";
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {


                if (!Page.IsPostBack)
                {
                    btnCheckAmount.Visible = false;


                    if (Session["LoginID"] != null)
                    {
                        if (Convert.ToString(Session["ClientType"]) == "Company")
                        {
                            btndeduct.Visible = true;
                            divCompany.Visible = true;
                            divDistributor.Visible = false;
                            btnPaypal.Visible = false;
                            btnCompanyTopup.Visible = true;

                            int user = 0;// Convert.ToInt32(Session["LoginID"]);
                            int distributorid = 0;// Convert.ToInt32(Session["DistributorID"]);

                            Distributor[] dstt = svc.GetDistributorDDLService(user, distributorid);

                            if (dstt.Length > 0)
                            {
                                ddlDistributor.DataSource = dstt;
                                ddlDistributor.DataValueField = "distributorID";
                                ddlDistributor.DataValueField = "distributorID";
                                ddlDistributor.DataTextField = "distributorName";
                                ddlDistributor.DataBind();
                                ddlDistributor.Items.Insert(0, new ListItem("---Select---", "0"));
                            }
                            lblBalanceAmount.Text = Convert.ToString(Session["CurrencyName"]) + " " + "0.00";
                            txtTopupAmount.Text = "0.00";
                        }
                        else
                        {
                            btndeduct.Visible = false;
                            divCompany.Visible = false;
                            divDistributor.Visible = true;
                            btnCompanyTopup.Visible = false;
                            //btnPaypal.Visible = true;
                            btnCheckAmount.Visible = true;


                            Distributor[] dst = svc.GetSingleDistributorService(Convert.ToInt32(Session["DistributorID"]));
                            if (dst.Length > 0)
                            {
                                lblDistributor.Text = dst[0].distributorName;
                                lblBalanceAmount.Text = Convert.ToString(Session["CurrencyName"]) + " " + Convert.ToString(dst[0].balanceAmount);
                                hddnDistributorID.Value = Convert.ToString(dst[0].distributorID);
                                txtTopupAmount.Text = "0.00";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        protected void ddlDistributor_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlDistributor.SelectedIndex > 0)
                {
                    int distributorid = Convert.ToInt32(ddlDistributor.SelectedValue);
                    Distributor[] dst = svc.GetSingleDistributorService(distributorid);
                    if (dst.Length > 0)
                    {
                        // lblDistributor.Text = dst[0].distributorName;
                        lblBalanceAmount.Text = Convert.ToString(Session["CurrencyName"]) + " " + Convert.ToString(dst[0].balanceAmount);
                        hddnDistributorID.Value = Convert.ToString(dst[0].distributorID);
                        txtTopupAmount.Text = "0.00";
                    }
                }
                else
                {
                    lblBalanceAmount.Text = Convert.ToString(Session["CurrencyName"]) + "0.00";
                    hddnDistributorID.Value = "0";
                    txtTopupAmount.Text = "0.00";
                }
            }
            catch (Exception ex)
            {

            }
        }

        //--In case of company topupbutton
        protected void btnCompanyTopup_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlDistributor.SelectedIndex > 0)
                {
                    if (Convert.ToDouble(txtTopupAmount.Text.Trim()) > 0)
                    {

                        SPayment sp = new SPayment();
                        sp.ChargedAmount = Convert.ToDecimal(txtTopupAmount.Text.Trim());
                        sp.PaymentType = 3;
                        sp.PayeeID = Convert.ToInt32(ddlDistributor.SelectedValue);
                        sp.PaymentFrom = 9;
                        sp.ActivationVia = 17;
                        sp.TransactionStatus = "Success";
                        sp.CheckSumm = "";
                        sp.Remarks = Convert.ToString(txtReason.Text.Trim());
                        sp.PaymentMode = "Company Topup";
                        sp.TransactionStatusId = 24;
                        sp.Currency = Convert.ToInt32(Session["CurrencyId"]);
                        int dist = Convert.ToInt32(ddlDistributor.SelectedValue);
                        int loginID = Convert.ToInt32(Session["LoginID"]);
                        int a = svc.InsertCompanyTopupBalanceService(dist, loginID, sp);

                        if (a > 0)
                        {
                            Distributor[] dst = svc.GetSingleDistributorService(Convert.ToInt32(ddlDistributor.SelectedValue));
                            if (dst.Length > 0)
                            {

                                lblBalanceAmount.Text = Convert.ToString(Session["CurrencyName"]) + " " + Convert.ToString(dst[0].balanceAmount);
                                hddnDistributorID.Value = Convert.ToString(dst[0].distributorID);
                            }
                            ShowPopUpMsg("Topup Successful");
                            txtTopupAmount.Text = "0.00";
                            txtReason.Text = string.Empty;
                            lblwarning.Text = "";
                            lblwarning.Style.Add("display", "none");
                        }
                        else
                        {
                            ShowPopUpMsg("Topup Unsuccessful\n Please Try Again");

                        }
                    }
                    else
                    {
                        lblwarning.Text = "Amount Should be Greater Than Zero";
                        lblwarning.Style.Add("display", "block");
                    }
                }
                else
                {
                    ShowPopUpMsg("Please Select Distributor");
                }


            }
            catch (Exception ex)
            {
                ShowPopUpMsg("Topup Unsuccessfull\n Please Try Again");
            }
        }

        //--In case of distributor topupbutton
        protected void btnDistributorTopup_Click(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToDouble(txtTopupAmount.Text.Trim()) > 0)
                {
                    SPayment sp = new SPayment();
                    sp.ChargedAmount = Convert.ToDecimal(txtTopupAmount.Text.Trim());
                    sp.PaymentType = 3;
                    sp.PayeeID = Convert.ToInt32(Session["DistributorID"]);
                    sp.PaymentFrom = 9;
                    sp.ActivationVia = 17;
                    sp.TransactionStatusId = 24;
                    int dist = Convert.ToInt32(Session["DistributorID"]);
                    int loginID = Convert.ToInt32(Session["LoginID"]);
                    int a = svc.InsertCompanyTopupBalanceService(dist, loginID, sp);

                    if (a > 0)
                    {
                        Distributor[] dst = svc.GetSingleDistributorService(Convert.ToInt32(Session["DistributorID"]));
                        if (dst.Length > 0)
                        {
                            lblDistributor.Text = dst[0].distributorName;
                            lblBalanceAmount.Text = Convert.ToString(Session["CurrencyName"]) + " " + Convert.ToString(dst[0].balanceAmount);
                            hddnDistributorID.Value = Convert.ToString(dst[0].distributorID);
                        }
                        UpdateDashboardBalanceAmount();
                        ShowPopUpMsg("Topup Successfull");
                        txtTopupAmount.Text = "0.00";
                        lblwarning.Text = "";
                        lblwarning.Style.Add("display", "none");
                    }
                    else
                    {
                        ShowPopUpMsg("Topup Unsuccessfull\n Please Try Again");
                    }
                }
                else
                {
                    lblwarning.Text = "Amount Should be Greater Than Zero";
                    lblwarning.Style.Add("display", "block");
                }
            }
            catch (Exception ex)
            {
                ShowPopUpMsg("Topup Unsuccessfull\n Please Try Again");
            }
        }

        //----paymentgateway button
        protected void btnPaypal_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                string amnt = ViewState["TotalTopUp"].ToString();
                //hddnFinalAmount.Value;
                string iteminfo = "Topup";
                if (amnt.Trim() != "")
                {
                    if (Convert.ToDouble(amnt) > 0)
                    {

                        Session["Amount"] = txtTopupAmount.Text.Trim();
                        int a = InsertPayment("Pending");

                        if (a > 0)
                        {
                            Session["PaymentId"] = a;
                            PayWithPayPal(amnt, iteminfo);
                        }
                        else
                        {
                            ShowPopUpMsg("Please Contact to System Administrator");
                        }
                    }
                    else
                    {
                        lblwarning.Text = "Amount Should be Greater Than Zero";
                        lblwarning.Style.Add("display", "block");
                    }
                }
                else
                {
                    lblwarning.Text = "Please Fill Topup Amount";
                    lblwarning.Style.Add("display", "block");
                }


            }
            catch (Exception ex)
            {

            }
        }

        public int InsertPayment(string ststus)
        {
            int a = 0;
            DataSet ds = null;
            try
            {
                if (Session["LoginID"] != null)
                {
                    SPayment sp = new SPayment();

                    sp.ChargedAmount = Convert.ToDecimal(Session["Amount"]);
                    sp.PaymentType = 3;
                    sp.PayeeID = Convert.ToInt32(Session["DistributorID"]);
                    sp.PaymentFrom = 9;
                    sp.ActivationVia = 18;
                    sp.TransactionStatusId = 23;
                    sp.TransactionStatus = ststus;
                    sp.PaymentMode = "PayPal Topup";
                    sp.TxnDate = DateTime.Now.ToString();
                    sp.Currency = Convert.ToInt32(Session["CurrencyId"]);

                    int dist = Convert.ToInt32(Session["DistributorID"]);
                    int loginID = Convert.ToInt32(Session["LoginID"]);
                    ds = svc.InsertPaypalTopupService(dist, loginID, sp);
                    if (ds != null)
                    {
                        a = Convert.ToInt32(ds.Tables[0].Rows[0][0]);
                    }
                    return a;
                }
                else
                {

                    return a;
                }
            }
            catch (Exception ex)
            {

                return a;
            }
        }

        protected void PayWithPayPal(string amount, string itemInfo)
        {
            string redirecturl = "";
            redirecturl += "https://www.paypal.com/cgi-bin/webscr?cmd=_xclick&business=" + ConfigurationManager.AppSettings["paypalemail"].ToString();
            //redirecturl += "https://www.sandbox.paypal.com/cgi-bin/webscr?cmd=_xclick&business=" + ConfigurationManager.AppSettings["paypalemail"].ToString();
            redirecturl += "&first_name=";
            redirecturl += "&city=";
            redirecturl += "&state=";
            redirecturl += "&item_name=" + itemInfo;
            redirecturl += "&amount=" + amount;
            redirecturl += "&night_phone_a=";
            // redirecturl += "&item_name=" + itemInfo;
            redirecturl += "&address1=";
            redirecturl += "&shipping=";
            redirecturl += "&handling=";
            redirecturl += "&tax=";
            redirecturl += "&quantity=1";
            redirecturl += "&currency=USD";
            redirecturl += "&return=" + ConfigurationManager.AppSettings["SuccessURL"].ToString();
            redirecturl += "&cancel_return=" + ConfigurationManager.AppSettings["FailedURL"].ToString();
            Response.Redirect(redirecturl, false);
        }

        public void UpdateDashboardBalanceAmount()
        {
            if (Session["ClientType"].ToString() == "Distributor")
            {

                int userId = Convert.ToInt32(Session["LoginID"]);
                int Dist = Convert.ToInt32(Session["DistributorID"]);
                Distributor[] dst = svc.GetSingleDistributorService(Dist);
                //Welcome pannel   
                Label lblBalance = (Label)this.Master.FindControl("lblBalance");
                lblBalance.Text = Convert.ToString(Session["CurrencyName"]) + " " + Convert.ToString(dst[0].balanceAmount);
                //  lblBalance1.Text = Convert.ToString(dst[0].balanceAmount);
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



        protected void btnCheckAmount_Click(object sender, EventArgs e)
        {
            TopUpCheck();
            if (topup == "0")
            {
                string amnt = txtTopupAmount.Text;
                if (amnt.Trim() != "")
                {
                    if (Convert.ToDouble(amnt) > 0)
                    {

                        int userId = Convert.ToInt32(Session["LoginID"]);
                        double a = (Convert.ToDouble(100 + Convert.ToDouble(ConfigurationManager.AppSettings.Get("PaypalCommission"))) * Convert.ToDouble(txtTopupAmount.Text.Trim())) / 100;
                        double b = Convert.ToDouble(0);
                        if (userId != 1)
                        {
                            b = Convert.ToDouble(ViewState["TotalTopUp"]);
                        }
                        if (a > b)
                        {
                            lblFinalAmount.Text = "USD " + String.Format("{0:0.00}", a);
                            hddnFinalAmount.Value = String.Format("{0:0.00}", a);
                        }
                        else
                        {
                            lblFinalAmount.Text = "USD " + String.Format("{0:0.00}", b);
                            hddnFinalAmount.Value = String.Format("{0:0.00}", b);
                        }
                        divProceed.Visible = false;
                        divpaypal.Visible = true;
                        btnPaypal.Visible = true;
                        Button1.Visible = true;
                        btnCheckAmount.Visible = false;
                        btndeduct.Visible = false;
                    }
                    else
                    {
                        lblwarning.Text = "Amount Should be Greater Than Zero";
                        lblwarning.Style.Add("display", "block");
                    }
                }
                else
                {
                    lblwarning.Text = "Please Fill Topup Amount";
                    lblwarning.Style.Add("display", "block");
                    // ShowPopUpMsg("Please Fill Topup Amount");
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            divProceed.Visible = true;
            divpaypal.Visible = false;
            btnPaypal.Visible = false;
            Button1.Visible = false;
            btnCheckAmount.Visible = true;
            //btndeduct.Visible = true;
        }

        protected void btndeduct_Click(object sender, EventArgs e)
        {
            try
            {

                if (ddlDistributor.SelectedIndex > 0)
                {
                    string amnt = txtTopupAmount.Text;

                    if (amnt.Trim() != "")
                    {
                        if (Convert.ToDouble(amnt) > 0)
                        {
                            DataSet ds = svc.DeductDistributorTopUpAmount(Convert.ToInt32(ddlDistributor.SelectedValue), Convert.ToDecimal(txtTopupAmount.Text), Convert.ToString(txtReason.Text.Trim()));
                            if (ds != null)
                            {

                                txtTopupAmount.Text = "0.0";
                                ddlDistributor_SelectedIndexChanged(null, null);

                                ShowPopUpMsg("Amount deduct successfully. ");
                            }
                            else
                            {
                                ShowPopUpMsg("Sorry ! Some issue");
                            }
                        }
                        else
                        {
                            lblwarning.Text = "deduct Amount Should be Greater Than Zero";
                            lblwarning.Style.Add("display", "block");
                        }
                    }
                    else
                    {
                        lblwarning.Text = "Please Fill deduct Amount";
                        lblwarning.Style.Add("display", "block");
                        // ShowPopUpMsg("Please Fill Topup Amount");
                    }
                }
                else
                {
                    ShowPopUpMsg("Please Select Distributor");
                    return;
                }





            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public void TopUpCheck()
        {

            lblTopupCommission.Visible = false;
            int dist = Convert.ToInt32(Session["DistributorID"]);
            DataSet ds = svc.GetDistributorInformation(dist);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    decimal Min = Convert.ToDecimal(ds.Tables[0].Rows[0]["MinTopup"]);
                    decimal Max = Convert.ToDecimal(ds.Tables[0].Rows[0]["MaxTopup"]);
                    decimal PaypalTax = Convert.ToDecimal(ds.Tables[0].Rows[0]["PaypalTax"]);
                    string PaypalTaxType = Convert.ToString(ds.Tables[0].Rows[0]["PaypalTaxType"]);
                    decimal RechargeVal = 0;
                    if (txtTopupAmount.Text.Trim() != "" && txtTopupAmount.Text.Trim() != " ")
                    {
                        RechargeVal = Convert.ToDecimal(txtTopupAmount.Text.Trim());
                    }

                    ViewState["TotalTopUp"] = RechargeVal;
                    if (RechargeVal < Min)
                    {
                        btnCheckAmount.Attributes.Add("disabled", "disabled");
                        lblTopupCommission.Visible = true;
                        lblTopupCommission.Text = "Recharge Amount Must be Equal Or Gretar Than Defined Minimum Amount";
                        btnRetry.Visible = true;
                        topup = "1";
                        return;
                    }

                    else if (RechargeVal > Max && Max != Convert.ToDecimal(0.00))
                    {
                        btnCheckAmount.Attributes.Add("disabled", "disabled");
                        lblTopupCommission.Visible = true;
                        lblTopupCommission.Text = "Recharge Amount Must be Equal Or Less Than Defined Maximum Amount";
                        btnRetry.Visible = true;
                        topup = "1";
                        return;
                    }

                    else if (PaypalTaxType != null && PaypalTax != Convert.ToDecimal(0.00))
                    {
                        if (PaypalTaxType == "Percentage")
                        {
                            lblTopupCommission.Visible = true;
                            decimal Taxvalue = (PaypalTax / Convert.ToDecimal(100.00)) * RechargeVal;
                            lblTopupCommission.Text = Taxvalue + " Tax will be appiled on top up amount(Tax Amount ='" + (Taxvalue + RechargeVal) + "')";
                            ViewState["TotalTopUp"] = RechargeVal + Taxvalue;

                        }
                        if (PaypalTaxType == "Fixed Amount")
                        {
                            lblTopupCommission.Visible = true;
                            lblTopupCommission.Text = PaypalTax + " Tax will be appiled on top up amount(Total Amount ='" + (RechargeVal + PaypalTax) + "')";
                            ViewState["TotalTopUp"] = RechargeVal + PaypalTax;

                        }

                        btnCheckAmount.Attributes.Remove("disabled");
                    }

                    else
                    {
                        btnCheckAmount.Attributes.Remove("disabled");
                    }
                    btnCheckAmount.Visible = true;
                }
                else
                {
                    ShowPopUpMsg("Session Timeout");
                }
            }
            else
            {
                Response.Redirect("Login.aspx", false);
            }
        }

        protected void btnRetry_Click(object sender, EventArgs e)
        {
            Response.Redirect("Topup.aspx", false);
        }



    }
}