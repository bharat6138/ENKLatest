﻿using System;
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
using ENK.LycaAPI;
namespace ENK
{
    public partial class ActivationForm : System.Web.UI.Page
    {
        Service1Client svc = new Service1Client();
        LycaAPI.LycaAPISoapClient la = new LycaAPI.LycaAPISoapClient();
        string RequestRes = "";
        string _Email = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {              

                txtChannelID.Text = ConfigurationManager.AppSettings.Get("TXN_SERIES");

                hddnAddOn.Value = Convert.ToString(0);
                hddnInternational.Value = Convert.ToString(0);

                hddnDataAddOnValue.Value = Convert.ToString(0);
                hddnDataAddOnDiscountedAmount.Value = Convert.ToString(0);
                hddnDataAddOnDiscountPercent.Value = Convert.ToString(0);

                hddnInternationalCreditValue.Value = Convert.ToString(0);
                hddnInternationalCreditDiscountedAmount.Value = Convert.ToString(0);
                hddnInternationalCreditDiscountPercent.Value = Convert.ToString(0);


                hddnMonths.Value = Convert.ToString(ddlMonth.SelectedValue);
                txtPHONETOPORT.Enabled = false;
                txtACCOUNT.Enabled = false;
                txtPIN.Enabled = false;
                try
                {
                    //if (Request.QueryString.Count > 0)
                    //{
                    //    long _logid = 0;
                    //    long.TryParse(Convert.ToString(Request.QueryString["lid"]), out _logid);

                    //    DataSet ds = new DataSet();
                    //    ds = svc.ValidateLoginApp(_logid);

                    //    if (ds != null)
                    //    {
                    //        if (ds.Tables[0].Rows.Count > 0)
                    //        {
                    //            Session["LoginID"] = Convert.ToString(ds.Tables[0].Rows[0]["ID"]);
                    //            Session["ClientType"] = Convert.ToString(ds.Tables[0].Rows[0]["Name"]);
                    //        }
                    //        else
                    //        {
                    //            Session["LoginID"] = null;
                    //            Session["ClientType"] = null;
                    //        }
                    //    }
                    //}

                    if (Session["LoginID"] != null)
                    {
                       // add by akash starts
                        string _Email = Convert.ToString(Session["EmailID"]);                       
                        // add by akash ends
                        ENK.net.emida.ws.webServicesService ws = new webServicesService();
                        // string ss1 = ws.Login2("01", "clerkterst", "clerk1234", "1");

                        // string ss1 = ws.Login2("01", "A&HPrepaid", "95222", "1");

                        DataTable dt = svc.GetVendor(Convert.ToInt32(Session["LoginId"]));
                        if (dt != null)
                        {
                            if (dt.Rows.Count > 0)
                            {
                                ddlNetwork.DataSource = dt;
                                ddlNetwork.DataValueField = "VendorID";
                                ddlNetwork.DataTextField = "VendorName";
                                ddlNetwork.DataBind();
                                ddlNetwork.Items.Insert(0, new ListItem("---Select Network---", "0"));
                            }
                            if (ddlNetwork.Items.Count == 2)
                            {
                                ddlNetwork.SelectedIndex = 1;
                                ddlNetwork_SelectedIndexChanged(null, null);
                            }
                        }

                        //BindH20Product();

                        divahannel.Visible = false;
                        divLanguage.Visible = false;
                        txtAmountPay.Attributes.Add("readonly", "true");
                        if (Convert.ToString(Session["ClientType"]) == "Company")
                        {
                            txtAmountPay.Text = "0.00";
                            BindDDL();
                            btnCompanyByAccount.Visible = true;
                            btnDistributorByAccount.Visible = false;
                            imgByPaypal.Visible = false;

                            //Ankit singh
                            BindDdlDataAddOn();
                            BindDdlInternationalCreadits();

                        }
                        else
                        {
                            txtAmountPay.Text = "0.00";
                            BindDDL();
                            //Ankit
                            CheckAccountBalance(Convert.ToDecimal(0.00));
                            //  CheckAccountBalance(0.00);
                            btnCompanyByAccount.Visible = false;
                            btnDistributorByAccount.Visible = true;
                            imgByPaypal.Visible = false;
                            lblPaypalMsg.Visible = false;
                            //Ankit singh
                            BindDdlDataAddOn();
                            BindDdlInternationalCreadits();

                        }
                    }

                }
                catch (Exception ex)
                {

                }

            }
        }

        protected void FetchSimNetwork()
        {
            try
            {
                DataSet ds = new DataSet();
                ds = svc.GetSimNetwork(Convert.ToString(txtSIMCARD.Text), "Activate");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlNetwork.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["VendorID"]);

                }
                else
                {

                    ddlNetwork.SelectedIndex = 0;
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        //ankit singh
        //public void CheckAccountBalance( double TariffAmount)
        public void CheckAccountBalance(decimal TariffAmount)
        {
            try
            {
                int dist = Convert.ToInt32(Session["DistributorID"]);
                Distributor[] dstbutor = svc.GetSingleDistributorService(dist);
                if (dstbutor != null)
                {
                    if (dstbutor.Length > 0)
                    {
                        //  double BalanceAmnt = dstbutor[0].balanceAmount;
                        decimal BalanceAmnt = dstbutor[0].balanceAmount;
                        if (TariffAmount > BalanceAmnt)
                        {
                            btnDistributorByAccount.Enabled = false;
                        }
                        else
                        {
                            btnDistributorByAccount.Enabled = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                btnDistributorByAccount.Enabled = false;
            }
        }
        //ankit singh
        //public Boolean CheckAccount(double TariffAmount)
        public Boolean CheckAccount(decimal TariffAmount)
        {
            Boolean IsBalance = false;
            try
            {
                int dist = Convert.ToInt32(Session["DistributorID"]);
                Distributor[] dstbutor = svc.GetSingleDistributorService(dist);
                if (dstbutor != null)
                {
                    if (dstbutor.Length > 0)
                    {
                        decimal BalanceAmnt = dstbutor[0].balanceAmount;
                        if (TariffAmount > BalanceAmnt)
                        {
                            IsBalance = false;
                        }
                        else
                        {
                            IsBalance = true;
                        }
                    }
                }
                return IsBalance;
            }
            catch (Exception ex)
            {
                return IsBalance;
            }
        }

        public void BindDdlDataAddOn()
        {
            try
            {
                DataSet ds = svc.GetAddOnANDInternationCreadit();

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlDataAddOn.DataSource = ds.Tables[0];
                        ddlDataAddOn.DataValueField = "ID";
                        ddlDataAddOn.DataTextField = "TariffCode";

                        ddlDataAddOn.DataBind();
                        ddlDataAddOn.Items.Insert(0, new ListItem("---Select---", "0"));
                    }
                    else
                    {
                        ddlDataAddOn.Items.Insert(0, new ListItem("---Select---", "0"));
                    }
                }
            }
            catch (Exception ex)
            {
                ddlDataAddOn.Items.Clear();
                ddlDataAddOn.Items.Insert(0, new ListItem("---Select---", "0"));
            }
        }

        public void BindDdlInternationalCreadits()
        {
            try
            {
                DataSet ds = svc.GetAddOnANDInternationCreadit();

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlInternationalCreadits.DataSource = ds.Tables[1];
                        ddlInternationalCreadits.DataValueField = "ID";
                        ddlInternationalCreadits.DataTextField = "TariffCode";

                        ddlInternationalCreadits.DataBind();
                        ddlInternationalCreadits.Items.Insert(0, new ListItem("---Select---", "0"));
                    }
                    else
                    {
                        ddlInternationalCreadits.Items.Insert(0, new ListItem("---Select---", "0"));
                    }
                }
            }
            catch (Exception ex)
            {
                ddlInternationalCreadits.Items.Clear();
                ddlInternationalCreadits.Items.Insert(0, new ListItem("---Select---", "0"));
            }
        }

        public void BindddlMonth()
        { }

        public void BindDDL()
        {
            try
            {
                int loginid = Convert.ToInt32(Session["LoginId"]);
                int distributorid = Convert.ToInt32(Session["DistributorID"]);
                int clienttypeid = Convert.ToInt32(Session["ClientTypeID"]);
                DataSet ds = svc.GetTariffForActivationService(loginid, distributorid, clienttypeid);

                ViewState["H20Product"] = ds.Tables[1];
                ViewState["EasyGoProduct"] = ds.Tables[2];

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlTariff.DataSource = ds.Tables[0];
                        ddlTariff.DataValueField = "ID";
                        ddlTariff.DataTextField = "TariffCode";
                        ddlTariff.DataBind();
                        ddlTariff.Items.Insert(0, new ListItem("---Select---", "0"));
                    }
                    else
                    {
                        ddlTariff.Items.Insert(0, new ListItem("---Select---", "0"));
                    }
                }
            }
            catch (Exception ex)
            {
                ddlTariff.Items.Clear();
                ddlTariff.Items.Insert(0, new ListItem("---Select---", "0"));
            }
        }

        protected void ddlTariff_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (Session["LoginID"] != null)
                {
                    txtRegulatry.Text = "0";
                    txtAmt.Text = "0";

                    if (Convert.ToString(Session["ClientType"]) == "Company")
                    {
                        if (ddlTariff.SelectedIndex > 0)
                        {
                            int DistributorID = Convert.ToInt32(Session["DistributorID"]);
                            int ClientTypeID = Convert.ToInt32(Session["ClientTypeID"]);
                            int LoginID = Convert.ToInt32(Session["LoginId"]);
                            int tariffID = Convert.ToInt32(ddlTariff.SelectedValue);
                            DataSet dst = svc.GetSingleTariffDetailForActivationService(LoginID, DistributorID, ClientTypeID, tariffID, Convert.ToByte(hddnMonths.Value.ToString()), "Activate");
                            if (dst != null)
                            {
                                if (dst.Tables[0].Rows.Count > 0)
                                {
                                    txtAmountPay.Text = Convert.ToString(dst.Tables[0].Rows[0]["Rental"]);
                                    double amnt = 0.0;
                                    amnt = Convert.ToDouble(dst.Tables[0].Rows[0]["Rental"]);
                                    hddnTariffTypeID.Value = Convert.ToString(dst.Tables[0].Rows[0]["TariffTypeID"]);
                                    hddnTariffType.Value = Convert.ToString(dst.Tables[0].Rows[0]["TariffType"]);
                                    hddnTariffCode.Value = Convert.ToString(dst.Tables[0].Rows[0]["TariffCode"]);
                                    hddnLycaAmount.Value = Convert.ToString(dst.Tables[0].Rows[0]["LycaAmount"]);
                                    //hddnMonths.Value = Convert.ToString(dst.Tables[0].Rows[0]["Months"]);

                                    // 1 $ Add Regulatery 
                                    DateTime today = DateTime.Today;
                                    // DateTime date = new DateTime(2017, 07, 20);
                                    DataSet dsReg = svc.GetRegulatery();
                                    DateTime date = Convert.ToDateTime(dsReg.Tables[0].Rows[0]["StartDate"]);
                                    if (date <= today)
                                    {
                                        amnt = Math.Round(amnt, 2);
                                        txtAmt.Text = Convert.ToString(amnt);
                                        if ((Convert.ToString(hddnTariffCode.Value).ToLower() == "payg") || (Convert.ToString(hddnTariffCode.Value).ToLower().StartsWith("payg")))
                                        {
                                            txtRegulatry.Text = "0.00";
                                            amnt = amnt + Convert.ToDouble(txtRegulatry.Text);
                                        }
                                        else
                                        {
                                            txtRegulatry.Text = (int.Parse(hddnMonths.Value.ToString()) * Convert.ToDecimal(dsReg.Tables[0].Rows[0]["RegulatoryFeePerMonth"])).ToString();
                                            amnt = amnt + Convert.ToDouble(txtRegulatry.Text);
                                        }

                                        hddnLycaAmount.Value = Convert.ToString(Convert.ToDouble(hddnLycaAmount.Value)); // + Convert.ToDouble(txtRegulatry.Text));
                                        if (hddnPrepaidSIM.Value == "0")
                                        {
                                            txtAmountPay.Text = Convert.ToString(amnt);
                                        }
                                        else
                                        {
                                            txtAmountPay.Text = Convert.ToString(0);

                                        }
                                        // hddnLycaAmount.Value = Convert.ToString(Convert.ToDouble(hddnLycaAmount.Value) + 1);
                                    }
                                    else
                                    {
                                        amnt = Math.Round(amnt, 2);
                                        txtRegulatry.Text = "0.00";
                                        txtAmt.Text = Convert.ToString(amnt);
                                        if (hddnPrepaidSIM.Value == "0")
                                        {
                                            txtAmountPay.Text = Convert.ToString(amnt);
                                        }
                                        else
                                        {
                                            txtAmountPay.Text = Convert.ToString(0);

                                        }

                                    }
                                }
                                else
                                {

                                    txtAmountPay.Text = "0.00";
                                    hddnTariffTypeID.Value = "";
                                    hddnTariffType.Value = "";
                                    hddnTariffCode.Value = "";
                                    hddnLycaAmount.Value = "";

                                    //hddnMonths.Value = "";
                                }
                            }
                            else
                            {
                                txtAmountPay.Text = "0.00";
                                hddnTariffTypeID.Value = "";
                                hddnTariffType.Value = "";
                                hddnTariffCode.Value = "";
                                hddnLycaAmount.Value = "";

                                //hddnMonths.Value = "";
                            }
                            //ddlDataAddOn.Enabled = false;
                            //ddlInternationalCreadits.Enabled = false;
                        }
                        else
                        {
                            txtAmountPay.Text = "0.00";
                            hddnTariffTypeID.Value = "";
                            hddnTariffType.Value = "";
                            hddnTariffCode.Value = "";
                            hddnLycaAmount.Value = "";
                            //ddlDataAddOn.Enabled = true;
                            //ddlInternationalCreadits.Enabled = true;
                            //hddnMonths.Value = "";
                        }
                    }
                    else
                    {
                        if (ddlTariff.SelectedIndex > 0)
                        {
                            int DistributorID = Convert.ToInt32(Session["DistributorID"]);
                            int ClientTypeID = Convert.ToInt32(Session["ClientTypeID"]);
                            int LoginID = Convert.ToInt32(Session["LoginId"]);
                            int tariffID = Convert.ToInt32(ddlTariff.SelectedValue);

                            DataSet dst = svc.GetSingleTariffDetailForActivationService(LoginID, DistributorID, ClientTypeID, tariffID, Convert.ToInt16(hddnMonths.Value), "Activate");
                            if (dst != null)
                            {
                                if (dst.Tables[0].Rows.Count > 0)
                                {
                                    txtAmountPay.Text = Convert.ToString(dst.Tables[0].Rows[0]["Rental"]);
                                    double amnt = 0.0;
                                    amnt = Convert.ToDouble(dst.Tables[0].Rows[0]["Rental"]);


                                    //CheckAccountBalance(amnt); //Commented by Puneet for the time being. Dated 11-Feb-2018


                                    hddnTariffTypeID.Value = Convert.ToString(dst.Tables[0].Rows[0]["TariffTypeID"]);
                                    hddnTariffType.Value = Convert.ToString(dst.Tables[0].Rows[0]["TariffType"]);
                                    hddnTariffCode.Value = Convert.ToString(dst.Tables[0].Rows[0]["TariffCode"]);
                                    hddnLycaAmount.Value = Convert.ToString(dst.Tables[0].Rows[0]["LycaAmount"]);

                                    // hddnMonths.Value = Convert.ToString(dst.Tables[0].Rows[0]["Months"]);

                                    // 1,2,3 $ Add Regulatery  according Month
                                    DateTime today = DateTime.Today;
                                    // DateTime date = new DateTime(2017, 07, 20);
                                    DataSet dsReg = svc.GetRegulatery();
                                    DateTime date = Convert.ToDateTime(dsReg.Tables[0].Rows[0]["StartDate"]);
                                    if (date <= today)
                                    {

                                        amnt = Math.Round(amnt, 2);
                                        txtAmt.Text = Convert.ToString(amnt);
                                        if ((Convert.ToString(hddnTariffCode.Value).ToLower() == "payg") || (Convert.ToString(hddnTariffCode.Value).ToLower().StartsWith("payg")))
                                        {
                                            txtRegulatry.Text = "0.00";
                                            amnt = amnt + Convert.ToDouble(txtRegulatry.Text);
                                        }
                                        else
                                        {
                                            txtRegulatry.Text = (int.Parse(hddnMonths.Value.ToString()) * Convert.ToDecimal(dsReg.Tables[0].Rows[0]["RegulatoryFeePerMonth"])).ToString();
                                            amnt = amnt + Convert.ToDouble(txtRegulatry.Text);
                                        }

                                        //txtRegulatry.Text = (int.Parse(hddnMonths.Value.ToString()) * Convert.ToDecimal(dsReg.Tables[0].Rows[0]["RegulatoryFeePerMonth"])).ToString();
                                        //amnt = amnt + Convert.ToDouble(txtRegulatry.Text);
                                        hddnLycaAmount.Value = Convert.ToString(Convert.ToDouble(hddnLycaAmount.Value));// + Convert.ToDouble(txtRegulatry.Text));

                                        if (hddnPrepaidSIM.Value == "0")
                                        {
                                            txtAmountPay.Text = Convert.ToString(amnt);
                                        }
                                        else
                                        {
                                            txtAmountPay.Text = Convert.ToString(0);

                                        }
                                        // hddnLycaAmount.Value = Convert.ToString(Convert.ToDouble(hddnLycaAmount.Value) + 1);
                                    }
                                    else
                                    {
                                        amnt = Math.Round(amnt, 2);
                                        txtRegulatry.Text = "0";
                                        txtAmt.Text = Convert.ToString(amnt);
                                        if (hddnPrepaidSIM.Value == "0")
                                        {
                                            txtAmountPay.Text = Convert.ToString(amnt);
                                        }
                                        else
                                        {
                                            txtAmountPay.Text = Convert.ToString(0);

                                        }
                                    }
                                }
                                else
                                {
                                    txtAmountPay.Text = "0.00";
                                    hddnTariffTypeID.Value = "";
                                    hddnTariffType.Value = "";
                                    hddnTariffCode.Value = "";
                                    hddnLycaAmount.Value = "";

                                    //hddnMonths.Value = "";
                                }
                            }
                            else
                            {
                                txtAmountPay.Text = "0.00";
                                hddnTariffTypeID.Value = "";
                                hddnTariffType.Value = "";
                                hddnTariffCode.Value = "";
                                hddnLycaAmount.Value = "";

                                //hddnMonths.Value = "";
                            }
                        }
                        else
                        {
                            txtAmountPay.Text = "0.00";

                            //ankit
                            CheckAccountBalance(Convert.ToDecimal(0.00));
                            hddnTariffTypeID.Value = "";
                            hddnTariffType.Value = "";
                            hddnTariffCode.Value = "";
                            hddnLycaAmount.Value = "";

                            //hddnMonths.Value = "";
                        }

                    }
                }
                else
                {

                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void btnCompanyByAccount_Click(object sender, EventArgs e)
        {
           
            int _networkid = 0;
            int.TryParse(Convert.ToString(ddlNetwork.SelectedValue), out _networkid);

            // add by akash for find current account balance starts
            string _CurrentBalance = "0";
            string _PreviousBalance = "0";
            _PreviousBalance = GetDistributorBalance();
           
            // add by akash for find current account balance end

            if (txtEmail.Text != "")
            {
                _Email = txtEmail.Text.Trim();
            }
            else
            {
                _Email = Convert.ToString(Session["EmailID"]);
            }

           

            if (int.Parse(hddnMonths.Value.ToString()) > 6)
            {
                ShowPopUpMsg("Plan cannot be activated for more than 6 months!!");
                return;

            }

            if (int.Parse(hddnMonths.Value.ToString()) == 3 && Convert.ToInt32(ddlTariff.SelectedValue) == 15)
            {
                ShowPopUpMsg("Selected Plan cannot be activated for 3 months!!");
                return;
            }


            if (chkActivePort.Checked == true && (txtPIN.Text.Trim() == "" || txtACCOUNT.Text.Trim() == "" || txtPHONETOPORT.Text.Trim() == ""))
            {
                ShowPopUpMsg("You have selected the Port-In, Please enter the Mandatory Fields");
                return;

            }

            //Ankit Singh
            if (chkActivePort.Checked == true)
            {


                ////// ankit change 23
                //DataTable dtApiDown = svc.PerticularAPIDOWN("PortIn");
                //bool APIStatus = Convert.ToBoolean(dtApiDown.Rows[0]["IsActive"]);
                //if (APIStatus == false)
                //{
                //    Response.Redirect("ErrorPage.aspx", false);
                //    return;
                //}
                //////
                string MSISDNPort = txtPHONETOPORT.Text;
                //if (txtPHONETOPORT.Text.StartsWith("1"))
                //{
                //    MSISDNPort = txtPHONETOPORT.Text.Substring(1);
                //}

                DataSet ds = svc.CheckAlreadySuccessfullPortIn(txtSIMCARD.Text.Trim(), MSISDNPort);
                if (ds.Tables[0].Rows.Count != 0)
                {
                    ShowPopUpMsg("This SIM number already has been ported before");
                    return;
                }
                if (ds.Tables[1].Rows.Count != 0)
                {
                    ShowPopUpMsg("This mobile number already has been ported before");
                    return;
                }
            }

            if (Session["LoginID"] != null)
            {
                int distr = Convert.ToInt32(Session["DistributorID"]);
                int clnt = Convert.ToInt32(Session["ClientTypeID"]);
                string simnumber = txtSIMCARD.Text.Trim();
                string City = txtCity.Text.Trim();
                string zip = txtZIPCode.Text.Trim();

                string transact = "";
                Boolean ValidSim = false;
                string erormsg = "";
                if (chkActivePort.Checked == true)
                {
                    DataTable dtTrans = svc.GetTransactionIDService();

                    int id = Convert.ToInt32(dtTrans.Rows[0]["TRANSACTIONID"]);
                    transact = id.ToString("00000");
                    transact = ConfigurationManager.AppSettings.Get("TXN_SERIES") + transact;

                }
                try
                {
                    if (chkActivePort.Checked == false)
                    {

                        ////// ankit change 23
                        //DataTable dtApiDown = svc.PerticularAPIDOWN("LycaActivateSim");
                        //bool APIStatus = Convert.ToBoolean(dtApiDown.Rows[0]["IsActive"]);
                        //if (APIStatus == false)
                        //{
                        //    Response.Redirect("ErrorPage.aspx", false);
                        //    return;
                        //}
                        //////

                        DataSet ds = svc.CheckSimActivationService(distr, clnt, simnumber, "Activate");
                        if (ds != null)
                        {
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                erormsg = Convert.ToString(ds.Tables[0].Rows[0][0]);
                                if (erormsg == "Ready to Activation")
                                {
                                    ValidSim = true;
                                    int id = Convert.ToInt32(ds.Tables[1].Rows[0]["TRANSACTIONID"]);
                                    transact = id.ToString("00000");
                                    transact = ConfigurationManager.AppSettings.Get("TXN_SERIES") + transact;
                                }
                            }
                        }
                    }
                    else
                    {

                        DataSet ds = svc.CheckSimPortINService(distr, clnt, simnumber);
                        if (ds != null)
                        {
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                erormsg = Convert.ToString(ds.Tables[0].Rows[0][0]);
                                if (erormsg == "Ready to Activation")
                                {
                                    ValidSim = true;

                                }
                                else
                                {
                                    Log1("Sim Not Ready For PortIN", "Reason");
                                    Log1(simnumber, "Simnumber");
                                    Log1(txtPHONETOPORT.Text, "Phone to port");
                                    Log1(txtACCOUNT.Text, "");
                                    Log1(txtZIPCode.Text, "");
                                    Log1("", "split");


                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    erormsg = ex.Message;
                    ValidSim = false;
                }
                //ValidSim = true;
                if (ValidSim == false)
                {
                    ShowPopUpMsg(erormsg);
                    return;
                }
                string resp = "";

                if (ddlNetwork.SelectedItem.Text == "Lyca Mobile")
                {
                    if (ddlTariff.SelectedIndex == 0)
                    {
                        ShowPopUpMsg("Please select Tariff");
                        return;
                    }


                    // 1 $ Add Regulatery 
                    Decimal Regulatery = 0;
                    DateTime today = DateTime.Today;
                    // DateTime date = new DateTime(2017, 07, 20);
                    DataSet dsReg = svc.GetRegulatery();
                    DateTime date = Convert.ToDateTime(dsReg.Tables[0].Rows[0]["StartDate"]);
                    if (date <= today)
                    {
                        if (Convert.ToString(hddnTariffCode.Value).ToLower() == "payg")
                        {
                            Regulatery = 0;     //in case of Payg plan activation regulatory fee should be 0
                        }
                        else
                        {
                            Regulatery = int.Parse(hddnMonths.Value.ToString()) * Convert.ToDecimal(dsReg.Tables[0].Rows[0]["RegulatoryFeePerMonth"]);
                        }
                    }

                    if (chkActivePort.Checked == false)
                    {
                        //resp = "<soapenv:Envelope xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:header=\"http://www.plintron.com/header/\"><soapenv:Header><header:ERROR_CODE> 0 </header:ERROR_CODE><header:ERROR_DESC> Success </header:ERROR_DESC></soapenv:Header><soapenv:Body>"+
                        //    "<ACTIVATE_USIM_PORTIN_BUNDLE_RESPONSE><ALLOCATED_MSISDN> 12013490305 </ALLOCATED_MSISDN><PORTIN_REFERENCE_NUMBER/></ACTIVATE_USIM_PORTIN_BUNDLE_RESPONSE></soapenv:Body></soapenv:Envelope>"   ;                 


                        decimal DataAddOnAmt = Convert.ToDecimal(hddnDataAddOnValue.Value);
                        decimal InternationCreditAmount = Convert.ToDecimal(hddnInternationalCreditValue.Value);
                       
                        resp = ActivateLycaSim(hddnTariffTypeID.Value, hddnTariffType.Value, hddnTariffCode.Value, txtEmail.Text.Trim(), transact, DataAddOnAmt, InternationCreditAmount);



                        if (resp.Trim() != null && resp.Trim() != "")
                        {
                            try
                            {

                                StringReader theReader = new StringReader(resp);
                                DataSet theDataSet = new DataSet();
                                theDataSet.ReadXml(theReader);

                                if (theDataSet.Tables.Count > 0)
                                {
                                    DataTable dt = theDataSet.Tables[0];
                                    if (dt.Rows.Count > 0)
                                    {
                                        string errorDesc = dt.Rows[0]["ERROR_DESC"].ToString();
                                        if (dt.Rows[0]["ERROR_CODE"].ToString().Trim() == "0")
                                        {
                                            string ALLOCATED_MSISDN = theDataSet.Tables[2].Rows[0]["ALLOCATED_MSISDN"].ToString();
                                            //ankit singh
                                            string MNPNO = theDataSet.Tables[2].Rows[0]["PORTIN_REFERENCE_NUMBER"].ToString();

                                          

                                            SPayment sp = new SPayment();
                                            sp.ChargedAmount = Convert.ToDecimal(txtAmountPay.Text.Trim());
                                            sp.PaymentType = 4;
                                            sp.PayeeID = Convert.ToInt32(Session["DistributorID"]);
                                            sp.PaymentFrom = 9; // Distributor
                                            sp.ActivationType = 6; //Activation
                                            sp.ActivationStatus = 15; //Active
                                            sp.ActivationVia = 17; //Account balance
                                            sp.ActivationResp = resp;
                                            sp.ActivationRequest = RequestRes;
                                            sp.TransactionId = transact;
                                            sp.TariffID = Convert.ToInt32(ddlTariff.SelectedValue);
                                            sp.ALLOCATED_MSISDN = ALLOCATED_MSISDN;
                                            sp.PaymentMode = "Company Activation";
                                            sp.TransactionStatusId = 24;
                                            sp.TransactionStatus = "Success";
                                            sp.Regulatery = Regulatery;
                                            sp.Month = Convert.ToInt16(hddnMonths.Value);
                                            int dist = Convert.ToInt32(Session["DistributorID"]);
                                            int loginID = Convert.ToInt32(Session["LoginID"]);
                                            string sim = txtSIMCARD.Text.Trim();
                                            // string zip = txtZIPCode.Text;
                                            string Language = "ENGLISH";
                                            string ChannelID = ConfigurationManager.AppSettings.Get("CHANNEL");
                                            try
                                            {
                                                int tariffDataAddOnID = Convert.ToInt32(ddlDataAddOn.SelectedValue);
                                                int tariffInternationalCreaditsID = Convert.ToInt32(ddlInternationalCreadits.SelectedValue);

                                                decimal DataAddOnValue = Convert.ToDecimal(hddnDataAddOnValue.Value);
                                                decimal DataAddOnDiscountedAmount = Convert.ToDecimal(hddnDataAddOnDiscountedAmount.Value);
                                                decimal DataAddOnDiscountPercent = Convert.ToDecimal(hddnDataAddOnDiscountPercent.Value);
                                                decimal InternationalCreditValue = Convert.ToDecimal(hddnInternationalCreditValue.Value);
                                                decimal InternationalCreditDiscountedAmount = Convert.ToDecimal(hddnInternationalCreditDiscountedAmount.Value);
                                                decimal InternationalCreditDiscountPercent = Convert.ToDecimal(hddnInternationalCreditDiscountPercent.Value);
                                                int s = svc.UpdateAccountBalanceServiceActivation(dist, loginID, sim, zip, ChannelID, Language, _networkid, sp, tariffDataAddOnID, tariffInternationalCreaditsID, DataAddOnValue, DataAddOnDiscountedAmount, DataAddOnDiscountPercent, InternationalCreditValue, InternationalCreditDiscountedAmount, InternationalCreditDiscountPercent, MNPNO, "");

                                                WriteLog(dist, loginID, sim, zip, Language, ChannelID, sp, "Record Save Success when sim activated In Company Case");
                                                // add by akash for find current account balance starts

                                                _CurrentBalance = GetDistributorBalance();
                                              
                                                // add by akash for find current account balance end

                                                if (_Email != "")
                                                {
                                                   // SendMail(_Email, "Lycamobile Sim Activation", ALLOCATED_MSISDN.Trim(), txtSIMCARD.Text.Trim(), "");
                                                    if (ddlTariff.SelectedItem.Text == "PAYG")
                                                    {
                                                    }
                                                    else
                                                    {
                                                        SendMailWithDetail(_Email, "Lycamobile Sim Activation", ALLOCATED_MSISDN.Trim(), txtSIMCARD.Text.Trim(), transact, _PreviousBalance, _CurrentBalance);
                                                    }
                                                }
                                                
                                                Response.Redirect("Print.aspx?Tid=" + sp.TransactionId + "&URL=" + "Activation", false);
                                                // ankitsingh testing breakpoint
                                            }
                                            catch (Exception ex)
                                            {
                                                WriteLog(dist, loginID, sim, zip, Language, ChannelID, sp, "Record Save fail when sim activated In Company Case");
                                            }

                                            ShowPopUpMsg("SIM activation done successfully\n with Mobile Number - " + ALLOCATED_MSISDN);
                                            resetControls(1);
                                        }
                                        else
                                        {
                                            SaveDataCompany(resp, 16, transact);
                                            ShowPopUpMsg("SIM activation Fail \n Please Try Again");
                                        }
                                    }
                                    else
                                    {
                                        SaveDataCompany(resp, 16, transact);
                                        ShowPopUpMsg("SIM activation Fail \n Please Try Again");
                                    }
                                }
                                else
                                {
                                    SaveDataCompany(resp, 16, transact);
                                    ShowPopUpMsg("SIM activation Fail \n Please Try Again");
                                }
                            }
                            catch (Exception ex)
                            {
                                SaveDataCompany(resp, 16, transact);
                                ShowPopUpMsg(ex.Message + "\n" + resp);
                            }

                        }
                        else
                        {
                            SaveDataCompany(resp, 16, transact);
                            ShowPopUpMsg("SIM activation Fail \n Please Try Again");
                        }

                    }
                    else
                    {
                        DataSet ds = new DataSet();
                        string MSISDN = "";
                        if (txtPHONETOPORT.Text.StartsWith("1"))
                        {
                            MSISDN = txtPHONETOPORT.Text.Substring(1);
                        }
                        else
                        {
                            MSISDN = txtPHONETOPORT.Text;
                        }
                        if (MSISDN.Length == 10)
                        {
                            MSISDN = "1" + MSISDN;
                            ds = svc.CheckSimAndMobileExistance(txtSIMCARD.Text, MSISDN);
                            if (ds != null && ds.Tables[0].Rows.Count == 0)
                            {
                                if (ds.Tables[1].Rows.Count == 0)
                                {

                                    string respP = ActivateSim(hddnTariffTypeID.Value, hddnTariffType.Value, hddnTariffCode.Value, txtEmail.Text.Trim(), transact);

                                    if (respP.Trim() != null && respP.Trim() != "")
                                    {
                                        try
                                        {
                                            StringReader theReader = new StringReader(respP);
                                            DataSet theDataSet = new DataSet();
                                            theDataSet.ReadXml(theReader);

                                            if (theDataSet.Tables.Count > 0)
                                            {
                                                DataTable dt = theDataSet.Tables[0];
                                                if (dt.Rows.Count > 0)
                                                {
                                                    string errorDesc = dt.Rows[0]["ERROR_DESC"].ToString();
                                                    if (dt.Rows[0]["ERROR_CODE"].ToString() == "0")
                                                    {
                                                        string ALLOCATED_MSISDN = theDataSet.Tables[2].Rows[0]["ALLOCATED_MSISDN"].ToString();
                                                        string MNPNO = theDataSet.Tables[2].Rows[0]["PORTIN_REFERENCE_NUMBER"].ToString();
                                                      
                                                        SPayment sp = new SPayment();
                                                        sp.ChargedAmount = Convert.ToDecimal(txtAmountPay.Text.Trim());
                                                        sp.PaymentType = 5;
                                                        sp.PayeeID = Convert.ToInt32(Session["DistributorID"]);
                                                        sp.PaymentFrom = 9;
                                                        sp.ActivationType = 7;
                                                        sp.ActivationStatus = 15;
                                                        sp.ActivationVia = 17;
                                                        sp.ActivationResp = respP;
                                                        sp.ActivationRequest = RequestRes;
                                                        sp.TariffID = Convert.ToInt32(ddlTariff.SelectedValue);
                                                        sp.ALLOCATED_MSISDN = ALLOCATED_MSISDN;
                                                        sp.Month = Convert.ToInt16(hddnMonths.Value);
                                                        sp.TransactionId = transact;

                                                        sp.PaymentMode = "Company PortIn";
                                                        sp.TransactionStatusId = 24;
                                                        sp.TransactionStatus = "Success";
                                                        sp.Regulatery = Regulatery;

                                                        int dist = Convert.ToInt32(Session["DistributorID"]);
                                                        int loginID = Convert.ToInt32(Session["LoginID"]);
                                                        string sim = txtSIMCARD.Text.Trim();
                                                        //string zipC = txtZIPCode.Text.Trim();
                                                        string Language = "ENGLISH";
                                                        string ChannelID = ConfigurationManager.AppSettings.Get("CHANNEL");
                                                        try
                                                        {

                                                            int tariffDataAddOnID = Convert.ToInt32(ddlDataAddOn.SelectedValue);
                                                            int tariffInternationalCreaditsID = Convert.ToInt32(ddlInternationalCreadits.SelectedValue);
                                                            decimal DataAddOnValue = Convert.ToDecimal(hddnDataAddOnValue.Value);
                                                            decimal DataAddOnDiscountedAmount = Convert.ToDecimal(hddnDataAddOnDiscountedAmount.Value);
                                                            decimal DataAddOnDiscountPercent = Convert.ToDecimal(hddnDataAddOnDiscountPercent.Value);
                                                            decimal InternationalCreditValue = Convert.ToDecimal(hddnInternationalCreditValue.Value);
                                                            decimal InternationalCreditDiscountedAmount = Convert.ToDecimal(hddnInternationalCreditDiscountedAmount.Value);
                                                            decimal InternationalCreditDiscountPercent = Convert.ToDecimal(hddnInternationalCreditDiscountPercent.Value);

                                                            int s = svc.UpdateAccountBalanceServiceActivation(dist, loginID, sim, zip, ChannelID, Language, _networkid, sp, tariffDataAddOnID, tariffInternationalCreaditsID, DataAddOnValue, DataAddOnDiscountedAmount, DataAddOnDiscountPercent, InternationalCreditValue, InternationalCreditDiscountedAmount, InternationalCreditDiscountPercent, MNPNO, "");
                                                            //Response.Write("<script> window.open( 'Print.aspx','_blank' ); </script>"); //Print receipt
                                                            WriteLog(dist, loginID, sim, zip, Language, ChannelID, sp, "Record Save Success when sim PortIn Success In Company Case");
                                                            // add by akash for find current account balance starts

                                                            _CurrentBalance = GetDistributorBalance();
                                                            
                                                            // add by akash for find current account balance end

                                                            if (_Email != "")
                                                            {
                                                               // SendMail(_Email, "Lycamobile Sim Activation", ALLOCATED_MSISDN.Trim(), txtSIMCARD.Text.Trim(), "");
                                                                if (ddlTariff.SelectedItem.Text == "PAYG")
                                                                {
                                                                }
                                                                else
                                                                {
                                                                    SendMailWithDetail(_Email, "Lycamobile Sim Activation", ALLOCATED_MSISDN.Trim(), txtSIMCARD.Text.Trim(), transact, _PreviousBalance, _CurrentBalance);
                                                                }
                                                            }
                                                            Response.Redirect("Print.aspx?Tid=" + sp.TransactionId + "&URL=" + "Activation", false);
                                                        }
                                                        catch (Exception ex)
                                                        {
                                                            WriteLog(dist, loginID, sim, zip, Language, ChannelID, sp, "Record Save Fail when sim PortIn Success  In Company Case");

                                                        }
                                                        ShowPopUpMsg("PORTIN Request submitted successfully\n with Mobile Number - " + ALLOCATED_MSISDN);
                                                        resetControls(1);
                                                    }
                                                    else
                                                    {
                                                        SaveDataCompany(resp, 16, transact);
                                                        ShowPopUpMsg("PORTIN Request Fail \n Please Try Again");
                                                    }
                                                }
                                                else
                                                {
                                                    SaveDataCompany(resp, 16, transact);
                                                    ShowPopUpMsg("PORTIN Request Fail \n Please Try Again");
                                                }
                                            }
                                            else
                                            {
                                                SaveDataCompany(resp, 16, transact);
                                                ShowPopUpMsg("PORTIN Request Fail \n Please Try Again");
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            SaveDataCompany(resp, 16, transact);
                                            ShowPopUpMsg("PORTIN Request Fail \n Please Try Again");
                                            //ShowPopUpMsg(ex.Message + "\n" + resp);
                                        }
                                    }
                                    else
                                    {
                                        SaveDataCompany(resp, 16, transact);
                                        ShowPopUpMsg("PORTIN Request Fail \n Please Try Again");
                                    }
                                }
                                else
                                {
                                    ShowPopUpMsg("Mobile Number is already linked with a Sim Number");
                                }
                            }
                            else
                            {
                                ShowPopUpMsg("Sim Number is already linked with a Mobile Number");
                            }
                        }
                        else
                        {
                            ShowPopUpMsg("Invalid Msisdn Formate, Please Try Again");
                        }
                    }
                }

            }
        }

        public string ActivateSim(string TariffTypeID, string TariffType, string TariffCode, string email, string TransactionID)
        {
            string response = "";
            try
            {
                email = ConfigurationManager.AppSettings.Get("Fromail");
                string SIMCARD = Convert.ToString(txtSIMCARD.Text.Trim());
                string ZIPCode = Convert.ToString(txtZIPCode.Text.Trim());
                string EmailAddress = Convert.ToString(txtEmail.Text.Trim());
                string Language = "ENGLISH";// Convert.ToString(ddlLanguage.SelectedItem.Text);
                string ChannelID = ConfigurationManager.AppSettings.Get("CHANNEL");// Convert.ToString(txtChannelID.Text.Trim());
                string PHONETOPORT = Convert.ToString(txtPHONETOPORT.Text.Trim());
                string ACCOUNT = Convert.ToString(txtACCOUNT.Text.Trim());
                string PIN = Convert.ToString(txtPIN.Text.Trim());
                string TOPUP_AMOUNT = Convert.ToInt32(hddnInternationalCreditValue.Value).ToString();//txtAmountPay.Text.Trim();

                string NATIONAL_BUNDLE_CODE = "";
                string NATIONAL_BUNDLE_AMOUNT = "";

                string INTERNATIONAL_BUNDLE_CODE = "";
                string INTERNATIONAL_BUNDLE_AMOUNT = "";

                string TOPUP_CARD_ID = "";
                string VOUCHER_PIN = "";

                string BundleID = TariffTypeID;
                string BundleCode = TariffCode;
                string BundleType = TariffType;
                string BundleAmount = hddnLycaAmount.Value;  // txtAmountPay.Text.Trim();

                string EmailID = email;

                if (Convert.ToString(hddnTariffCode.Value).ToLower() == "payg")
                {
                    BundleCode = "";
                    BundleAmount = "";
                }


                if (BundleType == "National")
                {
                    NATIONAL_BUNDLE_CODE = BundleCode;
                    NATIONAL_BUNDLE_AMOUNT = BundleAmount;
                }
                else
                {
                    NATIONAL_BUNDLE_CODE = BundleCode;
                    NATIONAL_BUNDLE_AMOUNT = BundleAmount;

                    //INTERNATIONAL_BUNDLE_CODE = BundleCode;
                    //INTERNATIONAL_BUNDLE_AMOUNT = BundleAmount;
                }

                //string X = "<ENVELOPE><HEADER><TRANSACTION_ID>" + TransactionID + "</TRANSACTION_ID><ENTITY>ENK</ENTITY><CHANNEL_REFERENCE>ENK</CHANNEL_REFERENCE></HEADER><BODY><ACTIVATE_USIM_PORTIN_BUNDLE_REQUEST><DETAILS><ICC_ID>" + SIMCARD + "</ICC_ID><ZIP_CODE>" + ZIPCode + "</ZIP_CODE><PREFERRED_LANGUAGE>" + Language + "</PREFERRED_LANGUAGE><P_MSISDN>" + PHONETOPORT + "</P_MSISDN><ACCOUNT_NUMBER>" + ACCOUNT + "</ACCOUNT_NUMBER><PASSWORD_PIN>" + PIN + "</PASSWORD_PIN><NO_OF_MONTHS>" + hddnMonths.Value + "</NO_OF_MONTHS><NATIONAL_BUNDLE_CODE>" + NATIONAL_BUNDLE_CODE + "</NATIONAL_BUNDLE_CODE><NATIONAL_BUNDLE_AMOUNT>" + NATIONAL_BUNDLE_AMOUNT + "</NATIONAL_BUNDLE_AMOUNT><INTERNATIONAL_BUNDLE_CODE></INTERNATIONAL_BUNDLE_CODE><INTERNATIONAL_BUNDLE_AMOUNT></INTERNATIONAL_BUNDLE_AMOUNT><TOPUP_AMOUNT>" + TOPUP_AMOUNT + "</TOPUP_AMOUNT><TOPUP_CARD_ID>" + TOPUP_CARD_ID + "</TOPUP_CARD_ID><VOUCHER_PIN>" + VOUCHER_PIN + "</VOUCHER_PIN><CHANNEL_ID>" + ChannelID + "</CHANNEL_ID><EMAIL_ID>" + EmailAddress + "</EMAIL_ID></DETAILS></ACTIVATE_USIM_PORTIN_BUNDLE_REQUEST></BODY></ENVELOPE>";

                string X = "<ENVELOPE><HEADER><TRANSACTION_ID>" + TransactionID + "</TRANSACTION_ID><ENTITY>" + ChannelID + "</ENTITY><CHANNEL_REFERENCE>" + ChannelID + "</CHANNEL_REFERENCE></HEADER><BODY><ACTIVATE_USIM_PORTIN_BUNDLE_REQUEST><DETAILS><ICC_ID>" + SIMCARD + "</ICC_ID><ZIP_CODE>" + ZIPCode + "</ZIP_CODE><PREFERRED_LANGUAGE>" + Language + "</PREFERRED_LANGUAGE><P_MSISDN>" + PHONETOPORT + "</P_MSISDN><ACCOUNT_NUMBER>" + ACCOUNT + "</ACCOUNT_NUMBER><PASSWORD_PIN>" + PIN + "</PASSWORD_PIN><NO_OF_MONTHS>" + hddnMonths.Value + "</NO_OF_MONTHS><NATIONAL_BUNDLE_CODE>" + NATIONAL_BUNDLE_CODE + "</NATIONAL_BUNDLE_CODE><NATIONAL_BUNDLE_AMOUNT>" + NATIONAL_BUNDLE_AMOUNT + "</NATIONAL_BUNDLE_AMOUNT><INTERNATIONAL_BUNDLE_CODE></INTERNATIONAL_BUNDLE_CODE><INTERNATIONAL_BUNDLE_AMOUNT></INTERNATIONAL_BUNDLE_AMOUNT><TOPUP_AMOUNT>" + TOPUP_AMOUNT + "</TOPUP_AMOUNT><TOPUP_CARD_ID>" + TOPUP_CARD_ID + "</TOPUP_CARD_ID><VOUCHER_PIN>" + VOUCHER_PIN + "</VOUCHER_PIN><CHANNEL_ID>" + ChannelID + "</CHANNEL_ID><EMAIL_ID>" + EmailAddress + "</EMAIL_ID></DETAILS></ACTIVATE_USIM_PORTIN_BUNDLE_REQUEST></BODY></ENVELOPE>";


                //string X = "<ENVELOPE><HEADER><TRANSACTION_ID>" + TransactionID + "</TRANSACTION_ID><ENTITY>ENK</ENTITY><CHANNEL_REFERENCE>ENK</CHANNEL_REFERENCE></HEADER><BODY><ACTIVATE_USIM_PORTIN_BUNDLE_REQUEST><DETAILS><ICC_ID>" + SIMCARD + "</ICC_ID><ZIP_CODE>" + ZIPCode + "</ZIP_CODE><PREFERRED_LANGUAGE>" + Language + "</PREFERRED_LANGUAGE><P_MSISDN>" + PHONETOPORT + "</P_MSISDN><ACCOUNT_NUMBER>" + ACCOUNT + "</ACCOUNT_NUMBER><PASSWORD_PIN>" + PIN + "</PASSWORD_PIN><NO_OF_MONTHS>" + hddnMonths.Value + "</NO_OF_MONTHS><NATIONAL_BUNDLE_CODE></NATIONAL_BUNDLE_CODE><NATIONAL_BUNDLE_AMOUNT></NATIONAL_BUNDLE_AMOUNT><INTERNATIONAL_BUNDLE_CODE>" + INTERNATIONAL_BUNDLE_CODE + "</INTERNATIONAL_BUNDLE_CODE><INTERNATIONAL_BUNDLE_AMOUNT>" + INTERNATIONAL_BUNDLE_AMOUNT + "</INTERNATIONAL_BUNDLE_AMOUNT><TOPUP_AMOUNT>" + TOPUP_AMOUNT + "</TOPUP_AMOUNT><TOPUP_CARD_ID>" + TOPUP_CARD_ID + "</TOPUP_CARD_ID><VOUCHER_PIN>1565403680666</VOUCHER_PIN><CHANNEL_ID>" + ChannelID + "</CHANNEL_ID><EMAIL_ID>" + EmailAddress + "</EMAIL_ID></DETAILS></ACTIVATE_USIM_PORTIN_BUNDLE_REQUEST></BODY></ENVELOPE>";
                RequestRes = X;
                Log(X, "Sending Request");
                response = Activation(X);
                Log1(response, "Port In Response");
                Log("", "split");
                return response;


            }
            catch (Exception ex)
            {
                return response;
                //ShowPopUpMsg(ex.Message);
            }
        }

        public void SaveDataCompany(string resp, int status, string TransactionID)
        {
            // new add 
            int _networkid = 0;
            int.TryParse(Convert.ToString(ddlNetwork.SelectedValue), out _networkid);
            //
            // 1 $ Add Regulatery 
            Decimal Regulatery = 0;
            DateTime today = DateTime.Today;
            // DateTime date = new DateTime(2017, 07, 20);
            DataSet dsReg = svc.GetRegulatery();
            DateTime date = Convert.ToDateTime(dsReg.Tables[0].Rows[0]["StartDate"]);
            if (ddlNetwork.SelectedItem.Text == "Lyca Mobile" && date <= today)
            {
                Regulatery = int.Parse(hddnMonths.Value.ToString()) * Convert.ToDecimal(dsReg.Tables[0].Rows[0]["RegulatoryFeePerMonth"]);

            }
            else { Regulatery = 0; }



            SPayment sp = new SPayment();
            sp.ChargedAmount = Convert.ToDecimal(txtAmountPay.Text.Trim());
            sp.PaymentType = 4;
            sp.PayeeID = Convert.ToInt32(Session["DistributorID"]);
            sp.PaymentFrom = 9;
            sp.ActivationType = 6;
            sp.ActivationStatus = status;
            sp.ActivationVia = 17;
            sp.ActivationResp = resp;
            sp.ActivationRequest = RequestRes;
            sp.TariffID = Convert.ToInt32(ddlTariff.SelectedValue);
            sp.ALLOCATED_MSISDN = "";
            sp.TransactionId = TransactionID;
            sp.PaymentMode = "Company Activation";
            sp.TransactionStatusId = 25;
            sp.TransactionStatus = "Fail";
            sp.Regulatery = Regulatery;
            sp.Month = Convert.ToInt16(hddnMonths.Value);
            int dist = Convert.ToInt32(Session["DistributorID"]);
            int loginID = Convert.ToInt32(Session["LoginID"]);
            string sim = txtSIMCARD.Text.Trim();
            string zip = txtZIPCode.Text.Trim();
            string Language = "ENGLISH";
            string ChannelID = ConfigurationManager.AppSettings.Get("CHANNEL");

            try
            {
                int tariffDataAddOnID = Convert.ToInt32(ddlDataAddOn.SelectedValue);
                int tariffInternationalCreaditsID = Convert.ToInt32(ddlInternationalCreadits.SelectedValue);
                decimal DataAddOnValue = Convert.ToDecimal(hddnDataAddOnValue.Value);
                decimal DataAddOnDiscountedAmount = Convert.ToDecimal(hddnDataAddOnDiscountedAmount.Value);
                decimal DataAddOnDiscountPercent = Convert.ToDecimal(hddnDataAddOnDiscountPercent.Value);
                decimal InternationalCreditValue = Convert.ToDecimal(hddnInternationalCreditValue.Value);
                decimal InternationalCreditDiscountedAmount = Convert.ToDecimal(hddnInternationalCreditDiscountedAmount.Value);
                decimal InternationalCreditDiscountPercent = Convert.ToDecimal(hddnInternationalCreditDiscountPercent.Value);
                string MNPNO = "";
                int s = svc.UpdateAccountBalanceServiceActivation(dist, loginID, sim, zip, ChannelID, Language, _networkid, sp, tariffDataAddOnID, tariffInternationalCreaditsID, DataAddOnValue, DataAddOnDiscountedAmount, DataAddOnDiscountPercent, InternationalCreditValue, InternationalCreditDiscountedAmount, InternationalCreditDiscountPercent, MNPNO, "");
                WriteLog(dist, loginID, sim, zip, Language, ChannelID, sp, "Record Save Success when sim not activated In Company Case");
            }
            catch (Exception ex)
            {
                WriteLog(dist, loginID, sim, zip, Language, ChannelID, sp, "Record Save Fail when sim not activated In Company Case");
            }
            ShowPopUpMsg("SIM activation Fail");
            resetControls(1);
        }

        protected void btnDistributorByAccount_Click(object sender, EventArgs e)
        {
            
            int _networkid = 0;
            int.TryParse(Convert.ToString(ddlNetwork.SelectedValue), out _networkid);

            // add by akash for find current account balance starts

            string _CurrentBalance = "0";
            string _PreviousBalance = "0";
            _PreviousBalance = GetDistributorBalance();
          
            // add by akash for find current account balance end

           
            if (txtEmail.Text != "")
            {
                _Email = txtEmail.Text.Trim();
            }
            else
            {
                _Email = Convert.ToString(Session["EmailID"]);
            }
           

            if (int.Parse(hddnMonths.Value.ToString()) > 6)
            {
                ShowPopUpMsg("Plan cannot be activated for more than 6 months!!");
                return;

            }


            if (int.Parse(hddnMonths.Value.ToString()) == 3 && Convert.ToInt32(ddlTariff.SelectedValue) == 15)
            {
                ShowPopUpMsg("Selected Plan cannot be activated for 3 months!!");
                return;
            }

            if (chkActivePort.Checked == true && (txtPIN.Text.Trim() == "" || txtACCOUNT.Text.Trim() == "" || txtPHONETOPORT.Text.Trim() == ""))
            {
                ShowPopUpMsg("You have selected the Port-In, Please enter the Mandatory Fields");
                return;

            }


            //Ankit Singh
            if (chkActivePort.Checked == true)
            {

                ////// ankit change 23
                //DataTable dtApiDown = svc.PerticularAPIDOWN("PortIn");
                //bool APIStatus = Convert.ToBoolean(dtApiDown.Rows[0]["IsActive"]);
                //if (APIStatus == false)
                //{
                //    Response.Redirect("ErrorPage.aspx", false);
                //    return;
                //}
                //////

                string MSISDNPort = txtPHONETOPORT.Text;
                //if (txtPHONETOPORT.Text.StartsWith("1"))
                //{
                //    MSISDNPort = txtPHONETOPORT.Text.Substring(1);
                //}

                DataSet ds = svc.CheckAlreadySuccessfullPortIn(txtSIMCARD.Text.Trim(), MSISDNPort);
                if (ds.Tables[0].Rows.Count != 0)
                {
                    ShowPopUpMsg("This SIM number already has been ported before");
                    return;
                }
                if (ds.Tables[1].Rows.Count != 0)
                {
                    ShowPopUpMsg("This mobile number already has been ported before");
                    return;
                }
            }

            if (Session["LoginID"] != null)
            {
                int distr = Convert.ToInt32(Session["DistributorID"]);
                int clnt = Convert.ToInt32(Session["ClientTypeID"]);
                string simnumber = txtSIMCARD.Text.Trim();
                string City = txtCity.Text.Trim();
                string zip = txtZIPCode.Text.Trim();
                string transact = "";

                if (chkActivePort.Checked == true)
                {
                    DataTable dtTrans = svc.GetTransactionIDService();

                    int id = Convert.ToInt32(dtTrans.Rows[0]["TRANSACTIONID"]);
                    transact = id.ToString("00000");
                    transact = ConfigurationManager.AppSettings.Get("TXN_SERIES") + transact;
                }


                Boolean IsBalance = true;
                //In case its a post paid SIM, then check for balance. else without payment check allow activation.
                if (hddnPrepaidSIM.Value.ToString() == "0")
                {
                    IsBalance = CheckAccount(Convert.ToDecimal(txtAmountPay.Text.Trim()));
                    if (IsBalance == false)
                    {
                        ShowPopUpMsg("Your Account Balance is Low \n Please Recharge Your Balance");
                        return;
                    }
                }

                Boolean ValidSim = false;
                string erormsg = "";
                try
                {
                    if (chkActivePort.Checked == false)
                    {

                        ////// ankit change 23
                        //DataTable dtApiDown = svc.PerticularAPIDOWN("LycaActivateSim");
                        //bool APIStatus = Convert.ToBoolean(dtApiDown.Rows[0]["IsActive"]);
                        //if (APIStatus == false)
                        //{
                        //    Response.Redirect("ErrorPage.aspx", false);
                        //    return;
                        //}
                        //////

                        DataSet ds = svc.CheckSimActivationService(distr, clnt, simnumber, "Activate");
                        if (ds != null)
                        {
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                erormsg = Convert.ToString(ds.Tables[0].Rows[0][0]);
                                if (erormsg == "Ready to Activation" || erormsg == "Plan is already mapped with SIM")
                                {
                                    ValidSim = true;
                                    int id = Convert.ToInt32(ds.Tables[1].Rows[0]["TRANSACTIONID"]);
                                    transact = id.ToString("00000");
                                    transact = ConfigurationManager.AppSettings.Get("TXN_SERIES") + transact;
                                }

                            }
                        }
                    }
                    else
                    {
                        DataSet ds = svc.CheckSimPortINService(distr, clnt, simnumber);
                        if (ds != null)
                        {
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                erormsg = Convert.ToString(ds.Tables[0].Rows[0][0]);
                                if (erormsg == "Ready to Activation")
                                {
                                    ValidSim = true;

                                }
                                else
                                {
                                    Log1("Sim Not Ready For PortIN", "Reason");

                                    Log1(simnumber, "Simnumber");
                                    Log1(txtPHONETOPORT.Text, "Phone to port");
                                    Log1(txtACCOUNT.Text, "");
                                    Log1(txtZIPCode.Text, "");
                                    Log1("", "split");


                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    ValidSim = false;
                    erormsg = ex.Message;
                }
                if (ValidSim == false)
                {
                    ShowPopUpMsg(erormsg);
                    return;
                }
                string resp = "";
                if (ddlNetwork.SelectedItem.Text == "Lyca Mobile")
                {

                    if (ddlTariff.SelectedIndex == 0)
                    {
                        ShowPopUpMsg("Please select Tariff");
                        return;
                    }
                    // 1 $ Add Regulatery 
                    Decimal Regulatery = 0;
                    DateTime today = DateTime.Today;
                    // DateTime date = new DateTime(2017, 07, 20);
                    DataSet dsReg = svc.GetRegulatery();
                    DateTime date = Convert.ToDateTime(dsReg.Tables[0].Rows[0]["StartDate"]);
                    if (date <= today)
                    {
                        if (Convert.ToString(hddnTariffCode.Value).ToLower() == "payg")
                        {
                            Regulatery = 0;     //in case of Payg plan activation regulatory fee should be 0
                        }
                        else
                        {
                            Regulatery = int.Parse(hddnMonths.Value.ToString()) * Convert.ToDecimal(dsReg.Tables[0].Rows[0]["RegulatoryFeePerMonth"]);
                        }
                    }
                    else
                    {

                        Regulatery = 0;
                    }
                    if (chkActivePort.Checked == false)
                    {                       
                        decimal DataAddOnAmt = Convert.ToDecimal(hddnDataAddOnValue.Value);
                        decimal InternationCreditAmount = Convert.ToDecimal(hddnInternationalCreditValue.Value);
                      
                       resp = ActivateLycaSim(hddnTariffTypeID.Value, hddnTariffType.Value, hddnTariffCode.Value, txtEmail.Text.Trim(), transact, DataAddOnAmt, InternationCreditAmount);

                        if (resp.Trim() != null && resp.Trim() != "")
                        {
                            try
                            {
                                StringReader theReader = new StringReader(resp);
                                DataSet theDataSet = new DataSet();
                                theDataSet.ReadXml(theReader);

                                if (theDataSet.Tables.Count > 0)
                                {
                                    DataTable dt = theDataSet.Tables[0];
                                    if (dt.Rows.Count > 0)
                                    {
                                        string errorDesc = dt.Rows[0]["ERROR_DESC"].ToString();
                                        if (dt.Rows[0]["ERROR_CODE"].ToString() == "0")
                                        {
                                            string ALLOCATED_MSISDN = theDataSet.Tables[2].Rows[0]["ALLOCATED_MSISDN"].ToString();
                                            string MNPNO = theDataSet.Tables[2].Rows[0]["PORTIN_REFERENCE_NUMBER"].ToString();
                                            //SendMail(txtEmail.Text.Trim(), "Lycamobile Sim Activation", ALLOCATED_MSISDN, txtSIMCARD.Text.Trim(), "");
                                          
                                            SPayment sp = new SPayment();
                                            sp.ChargedAmount = Convert.ToDecimal(txtAmountPay.Text.Trim());
                                            sp.PaymentType = 4;
                                            sp.PayeeID = Convert.ToInt32(Session["DistributorID"]);
                                            sp.PaymentFrom = 9;
                                            sp.ActivationType = 6;
                                            sp.ActivationStatus = 15;
                                            sp.ActivationVia = 17;
                                            sp.ActivationResp = resp;
                                            sp.ActivationRequest = RequestRes;
                                            sp.TariffID = Convert.ToInt32(ddlTariff.SelectedValue);
                                            sp.ALLOCATED_MSISDN = ALLOCATED_MSISDN;
                                            sp.TransactionId = transact;
                                            sp.PaymentMode = "Distributor Activation";
                                            sp.TransactionStatusId = 24;
                                            sp.TransactionStatus = "Success";
                                            sp.Regulatery = Regulatery;
                                            sp.Month = Convert.ToInt16(hddnMonths.Value);
                                            int dist = Convert.ToInt32(Session["DistributorID"]);
                                            int loginID = Convert.ToInt32(Session["LoginID"]);
                                            string sim = txtSIMCARD.Text.Trim();
                                            //string zip = txtZIPCode.Text.Trim();
                                            string Language = "ENGLISH";
                                            string ChannelID = ConfigurationManager.AppSettings.Get("CHANNEL");
                                            try
                                            {
                                                int tariffDataAddOnID = Convert.ToInt32(ddlDataAddOn.SelectedValue);
                                                int tariffInternationalCreaditsID = Convert.ToInt32(ddlInternationalCreadits.SelectedValue);
                                                decimal DataAddOnValue = Convert.ToDecimal(hddnDataAddOnValue.Value);
                                                decimal DataAddOnDiscountedAmount = Convert.ToDecimal(hddnDataAddOnDiscountedAmount.Value);
                                                decimal DataAddOnDiscountPercent = Convert.ToDecimal(hddnDataAddOnDiscountPercent.Value);
                                                decimal InternationalCreditValue = Convert.ToDecimal(hddnInternationalCreditValue.Value);
                                                decimal InternationalCreditDiscountedAmount = Convert.ToDecimal(hddnInternationalCreditDiscountedAmount.Value);
                                                decimal InternationalCreditDiscountPercent = Convert.ToDecimal(hddnInternationalCreditDiscountPercent.Value);

                                                int s = svc.UpdateAccountBalanceServiceActivation(dist, loginID, sim, zip, ChannelID, Language, _networkid, sp, tariffDataAddOnID, tariffInternationalCreaditsID, DataAddOnValue, DataAddOnDiscountedAmount, DataAddOnDiscountPercent, InternationalCreditValue, InternationalCreditDiscountedAmount, InternationalCreditDiscountPercent, MNPNO,"");
                                                //Response.Write("<script> window.open( 'Print.aspx','_blank' ); </script>"); //Print receipt

                                                WriteLog(dist, loginID, sim, zip, Language, ChannelID, sp, "Record Save Success when sim activated In Distributor Case");
                                                // add by akash for find current account balance starts

                                                _CurrentBalance = GetDistributorBalance();
                                               
                                                // add by akash for find current account balance end

                                                
                                                if (_Email != "")
                                                {
                                                   // SendMail(_Email, "Sim PortIn", ALLOCATED_MSISDN, txtSIMCARD.Text.Trim(), "");
                                                    if (ddlTariff.SelectedItem.Text == "PAYG")
                                                    {
                                                    }
                                                    else
                                                    {
                                                        SendMailWithDetail(_Email, "Sim PortIn", ALLOCATED_MSISDN.Trim(), txtSIMCARD.Text.Trim(), transact, _PreviousBalance, _CurrentBalance);
                                                    }
                                                }
                                               
                                                Response.Redirect("Print.aspx?Tid=" + sp.TransactionId + "&URL=" + "Activation", false);
                                            }
                                            catch (Exception ex)
                                            {
                                                WriteLog(dist, loginID, sim, zip, Language, ChannelID, sp, "Record Save Fail when sim activated  In Distributor Case");

                                            }

                                            //  Update   Network in Purchasemaster table due to deferent network
                                            //if (Convert.ToString(hdnVendorID.Value) != "0" && Convert.ToString(hdnPurchaseID.Value) != "0")
                                            //{

                                            //    if (Convert.ToInt32(ddlNetwork.SelectedValue) != Convert.ToInt32(hdnVendorID.Value))
                                            //    {

                                            //        UpdatePurchaseSimNetwork();
                                            //    }
                                            //}


                                            ShowPopUpMsg("SIM activation done successfully\n with Mobile Number - " + ALLOCATED_MSISDN);
                                            resetControls(1);
                                        }
                                        else
                                        {
                                            SaveData(resp, 16, transact);
                                            ShowPopUpMsg("SIM activation Fail \n Please Try Again");
                                        }
                                    }
                                    else
                                    {
                                        SaveData(resp, 16, transact);
                                        ShowPopUpMsg("SIM activation Fail \n Please Try Again");
                                    }
                                }
                                else
                                {
                                    SaveData(resp, 16, transact);
                                    ShowPopUpMsg("SIM activation Fail \n Please Try Again");
                                }
                            }
                            catch (Exception ex)
                            {
                                SaveData(resp, 16, transact);
                                ShowPopUpMsg(ex.Message + "\n" + resp);
                            }
                        }
                        else
                        {
                            SaveData(resp, 16, transact);
                            ShowPopUpMsg("SIM activation Fail \n Please Try Again");
                        }
                    }
                    else
                    {
                        DataSet ds = new DataSet();
                        string MSISDN = "";
                        if (txtPHONETOPORT.Text.StartsWith("1"))
                        {
                            MSISDN = txtPHONETOPORT.Text.Substring(1);
                        }
                        else
                        {
                            MSISDN = txtPHONETOPORT.Text;
                        }
                        if (MSISDN.Length == 10)
                        {
                            MSISDN = "1" + MSISDN;
                            ds = svc.CheckSimAndMobileExistance(txtSIMCARD.Text, MSISDN);
                            if (ds != null && ds.Tables[0].Rows.Count == 0)
                            {
                                if (ds.Tables[1].Rows.Count == 0)
                                {

                                     string respP = ActivateSim(hddnTariffTypeID.Value, hddnTariffType.Value, hddnTariffCode.Value, txtEmail.Text.Trim(), transact);
                                   
                                    if (respP.Trim() != null && respP.Trim() != "")
                                    {
                                        try
                                        {
                                            StringReader theReader = new StringReader(respP);
                                            DataSet theDataSet = new DataSet();
                                            theDataSet.ReadXml(theReader);

                                            if (theDataSet.Tables.Count > 0)
                                            {
                                                DataTable dt = theDataSet.Tables[0];
                                                if (dt.Rows.Count > 0)
                                                {
                                                    string errorDesc = dt.Rows[0]["ERROR_DESC"].ToString();
                                                    if (dt.Rows[0]["ERROR_CODE"].ToString() == "0")
                                                    {
                                                        string ALLOCATED_MSISDN = theDataSet.Tables[2].Rows[0]["ALLOCATED_MSISDN"].ToString();
                                                        string MNPNO = theDataSet.Tables[2].Rows[0]["PORTIN_REFERENCE_NUMBER"].ToString();
                                                      

                                                        SPayment sp = new SPayment();
                                                        sp.ChargedAmount = Convert.ToDecimal(txtAmountPay.Text.Trim());
                                                        sp.PaymentType = 5;
                                                        sp.PayeeID = Convert.ToInt32(Session["DistributorID"]);
                                                        sp.PaymentFrom = 9;
                                                        sp.ActivationType = 7;
                                                        sp.ActivationStatus = 15;
                                                        sp.ActivationVia = 17;
                                                        sp.ActivationResp = respP;
                                                        sp.ActivationRequest = RequestRes;
                                                        sp.TariffID = Convert.ToInt32(ddlTariff.SelectedValue);
                                                        sp.ALLOCATED_MSISDN = ALLOCATED_MSISDN;
                                                        sp.TransactionId = transact;
                                                        sp.Month = Convert.ToInt16(hddnMonths.Value);
                                                        sp.PaymentMode = "Distributor PortIn";
                                                        sp.TransactionStatusId = 24;
                                                        sp.TransactionStatus = "Success";
                                                        sp.Regulatery = Regulatery;

                                                        int dist = Convert.ToInt32(Session["DistributorID"]);
                                                        int loginID = Convert.ToInt32(Session["LoginID"]);
                                                        string sim = txtSIMCARD.Text.Trim();
                                                        //string zip = txtZIPCode.Text.Trim();
                                                        string Language = "ENGLISH";
                                                        string ChannelID = ConfigurationManager.AppSettings.Get("TXN_SERIES");
                                                        try
                                                        {
                                                            int tariffDataAddOnID = Convert.ToInt32(ddlDataAddOn.SelectedValue);
                                                            int tariffInternationalCreaditsID = Convert.ToInt32(ddlInternationalCreadits.SelectedValue);
                                                            decimal DataAddOnValue = Convert.ToDecimal(hddnDataAddOnValue.Value);
                                                            decimal DataAddOnDiscountedAmount = Convert.ToDecimal(hddnDataAddOnDiscountedAmount.Value);
                                                            decimal DataAddOnDiscountPercent = Convert.ToDecimal(hddnDataAddOnDiscountPercent.Value);
                                                            decimal InternationalCreditValue = Convert.ToDecimal(hddnInternationalCreditValue.Value);
                                                            decimal InternationalCreditDiscountedAmount = Convert.ToDecimal(hddnInternationalCreditDiscountedAmount.Value);
                                                            decimal InternationalCreditDiscountPercent = Convert.ToDecimal(hddnInternationalCreditDiscountPercent.Value);

                                                            int s = svc.UpdateAccountBalanceServiceActivation(dist, loginID, sim, zip, ChannelID, Language, _networkid, sp, tariffDataAddOnID, tariffInternationalCreaditsID, DataAddOnValue, DataAddOnDiscountedAmount, DataAddOnDiscountPercent, InternationalCreditValue, InternationalCreditDiscountedAmount, InternationalCreditDiscountPercent, MNPNO,"");
                                                            WriteLog(dist, loginID, sim, zip, Language, ChannelID, sp, "Record Save Success when sim PortIn Success In Distributor Case");

                                                            // add by akash for find current account balance starts

                                                            _CurrentBalance = GetDistributorBalance();
                                                            
                                                            // add by akash for find current account balance end

                                                            // add by akash starts
                                                            if (_Email != "")
                                                            {
                                                                //SendMail(_Email, "Sim PortIn", ALLOCATED_MSISDN, txtSIMCARD.Text.Trim(), "");
                                                                if (ddlTariff.SelectedItem.Text == "PAYG")
                                                                {
                                                                }
                                                                else
                                                                {
                                                                    SendMailWithDetail(_Email, "Sim PortIn", ALLOCATED_MSISDN.Trim(), txtSIMCARD.Text.Trim(), transact, _PreviousBalance, _CurrentBalance);
                                                                }

                                                            }
                                                            // add by akash ends
                                                            Response.Redirect("Print.aspx?Tid=" + sp.TransactionId + "&URL=" + "Activation", false);
                                                        }
                                                        catch (Exception ex)
                                                        {
                                                            WriteLog(dist, loginID, sim, zip, Language, ChannelID, sp, "Record Save Fail when sim PortIn Success  In Distributor Case");

                                                        }
                                                        ShowPopUpMsg("PORTIN Request submitted successfully\n with Mobile Number - " + ALLOCATED_MSISDN);
                                                        resetControls(1);
                                                    }
                                                    else
                                                    {
                                                        SaveData(respP, 16, transact);
                                                        ShowPopUpMsg("PORTIN Request Fail \n Please Try Again");
                                                    }
                                                }
                                                else
                                                {
                                                    SaveData(respP, 16, transact);
                                                    ShowPopUpMsg("PORTIN Request Fail \n Please Try Again");
                                                }
                                            }
                                            else
                                            {
                                                SaveData(respP, 16, transact);

                                                ShowPopUpMsg("PORTIN Request Fail \n Please Try Again");
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            SaveData(respP, 16, transact);
                                            ShowPopUpMsg("PORTIN Request Fail \n Please Try Again");
                                            //ShowPopUpMsg(ex.Message + "\n" + resp);
                                        }
                                    }
                                    else
                                    {
                                        SaveData(respP, 16, transact);
                                        ShowPopUpMsg("PORTIN Request Fail \n Please Try Again");
                                    }
                                }
                                else
                                {
                                    ShowPopUpMsg("Mobile Number is already linked with a Sim Number");
                                }
                            }
                            else
                            {
                                ShowPopUpMsg("Sim Number is already linked with a Mobile Number");
                            }
                        }
                        else
                        {
                            ShowPopUpMsg("Invalid Msisdn Formate, Please Try Again");
                        }
                    }
                }
            }
        }

        public void SaveData(string resp, int status, string TransactionID)
        {
            // new add 
            int _networkid = 0;
            int.TryParse(Convert.ToString(ddlNetwork.SelectedValue), out _networkid);
            //
            // 1 $ Add Regulatery 
            Decimal Regulatery = 0;
            DateTime today = DateTime.Today;
            // DateTime date = new DateTime(2017, 07, 20);
            DataSet dsReg = svc.GetRegulatery();
            DateTime date = Convert.ToDateTime(dsReg.Tables[0].Rows[0]["StartDate"]);

            if (ddlNetwork.SelectedItem.Text == "Lyca Mobile" && date <= today)
            {
                Regulatery = int.Parse(hddnMonths.Value.ToString()) * Convert.ToDecimal(dsReg.Tables[0].Rows[0]["RegulatoryFeePerMonth"]);
                //if (hddnMonths.Value == "1")
                //{
                //    Regulatery = 1;
                //}
                //else if (hddnMonths.Value == "2")
                //{
                //    Regulatery = 2;
                //}
                //else if (hddnMonths.Value == "3")
                //{
                //    Regulatery = 3;
                //}
            }
            else { Regulatery = 0; }

            SPayment sp = new SPayment();
            sp.ChargedAmount = Convert.ToDecimal(txtAmountPay.Text.Trim());
            sp.PaymentType = 4;
            sp.PayeeID = Convert.ToInt32(Session["DistributorID"]);
            sp.PaymentFrom = 9;
            sp.ActivationType = 6;
            if (chkActivePort.Checked == true && (txtPIN.Text.Trim() != "" && txtACCOUNT.Text.Trim() != "" && txtPHONETOPORT.Text.Trim() != ""))
            {
                sp.ActivationType = 7;
            }
            sp.ActivationStatus = status;
            sp.ActivationVia = 17;
            sp.ActivationResp = resp;
            sp.ActivationRequest = RequestRes;
            sp.TariffID = Convert.ToInt32(ddlTariff.SelectedValue);
            sp.ALLOCATED_MSISDN = "";
            sp.TransactionId = TransactionID;
            sp.PaymentMode = "Distributor Activation";
            sp.TransactionStatusId = 25;
            sp.TransactionStatus = "Fail";
            sp.Regulatery = Regulatery;
            sp.Month = Convert.ToInt16(hddnMonths.Value);
            int dist = Convert.ToInt32(Session["DistributorID"]);
            int loginID = Convert.ToInt32(Session["LoginID"]);
            string sim = txtSIMCARD.Text.Trim();
            string zip = txtZIPCode.Text.Trim();
            string Language = "ENGLISH";
            string ChannelID = ConfigurationManager.AppSettings.Get("CHANNEL");

            try
            {
                int tariffDataAddOnID = Convert.ToInt32(ddlDataAddOn.SelectedValue);
                int tariffInternationalCreaditsID = Convert.ToInt32(ddlInternationalCreadits.SelectedValue);
                decimal DataAddOnValue = Convert.ToDecimal(hddnDataAddOnValue.Value);
                decimal DataAddOnDiscountedAmount = Convert.ToDecimal(hddnDataAddOnDiscountedAmount.Value);
                decimal DataAddOnDiscountPercent = Convert.ToDecimal(hddnDataAddOnDiscountPercent.Value);
                decimal InternationalCreditValue = Convert.ToDecimal(hddnInternationalCreditValue.Value);
                decimal InternationalCreditDiscountedAmount = Convert.ToDecimal(hddnInternationalCreditDiscountedAmount.Value);
                decimal InternationalCreditDiscountPercent = Convert.ToDecimal(hddnInternationalCreditDiscountPercent.Value);
                string MNPNO = "";
                int s = svc.UpdateAccountBalanceServiceActivation(dist, loginID, sim, zip, ChannelID, Language, _networkid, sp, tariffDataAddOnID, tariffInternationalCreaditsID, DataAddOnValue, DataAddOnDiscountedAmount, DataAddOnDiscountPercent, InternationalCreditValue, InternationalCreditDiscountedAmount, InternationalCreditDiscountPercent, MNPNO,"");
                WriteLog(dist, loginID, sim, zip, Language, ChannelID, sp, "Record Save success when sim Not activated In Distributor Case");
            }
            catch (Exception ex)
            {
                WriteLog(dist, loginID, sim, zip, Language, ChannelID, sp, "Record Save Fail when sim not activate In Distributor Case");
            }
            // ShowPopUpMsg("SIM activation Fail");
            resetControls(1);
        }

        public void WriteLog(int dist, int loginID, string sim, string zip, string Language, string ChannelID, SPayment sp, string msgg)
        {
            StringBuilder logdata = new StringBuilder();
            logdata.Append(dist + "|");
            logdata.Append(loginID + "|");
            logdata.Append(sim + "|");
            logdata.Append(zip + "|");
            logdata.Append(txtEmail.Text.ToString() + "|");
            logdata.Append(Language + "|");
            logdata.Append(ChannelID + "|");
            logdata.Append(sp.ChargedAmount + "|");
            logdata.Append(sp.PaymentType + "|");
            logdata.Append(sp.PayeeID + "|");
            logdata.Append(sp.PaymentFrom + "|");
            logdata.Append(sp.ActivationType + "|");
            logdata.Append(sp.ActivationStatus + "|");
            logdata.Append(sp.ActivationVia + "|");
            logdata.Append(sp.ActivationRequest + "|");
            logdata.Append(sp.ActivationResp + "|");
            logdata.Append(sp.TariffID);
            logdata.Append(sp.TransactionId);


            string data = logdata.ToString();
            Log1(data, msgg);
            Log1("", "split");
        }

        public string ActivateLycaSim(string TariffTypeID, string TariffType, string TariffCode, string email, string TransactionID, decimal DataAddOnAmt, decimal InternationCreditAmount)
        {
            string response = "";
            try
            {
                email = ConfigurationManager.AppSettings.Get("Fromail");
                string BundleID = TariffTypeID;


                string BundleCode = TariffCode;
                if (BundleCode.ToLower() == "payg")
                {
                    BundleCode = "";
                }
                string BundleType = TariffType;

                string BundleAmount = hddnLycaAmount.Value;// txtAmountPay.Text.Trim();
                if (BundleCode.ToLower() == "payg")
                {
                    BundleAmount = "";
                }
                string DataAddOnAMOUNT = Convert.ToString(DataAddOnAmt);
                string InternationalCreditAMT = Convert.ToInt32(InternationCreditAmount).ToString();


                string EmailID = email;

                string X = "";

                string activation = "1";
                string SIMCARD = Convert.ToString(txtSIMCARD.Text.Trim());
                string ZIPCode = Convert.ToString(txtZIPCode.Text.Trim());
                string Language = "ENGLISH";// Convert.ToString(ddlLanguage.SelectedItem.Text);
                string ChannelID = ConfigurationManager.AppSettings.Get("CHANNEL");
                if (Convert.ToString(hddnTariffCode.Value).ToLower() == "payg")
                {
                    BundleCode = "";
                    BundleAmount = "";
                }

                if (BundleType == "National")
                {
                    //X = "<ENVELOPE><HEADER><TRANSACTION_ID>" + TransactionID + "</TRANSACTION_ID><ENTITY>ENK</ENTITY><CHANNEL_REFERENCE>ENK</CHANNEL_REFERENCE></HEADER><BODY><ACTIVATE_USIM_PORTIN_BUNDLE_REQUEST><DETAILS><ICC_ID>" + SIMCARD + "</ICC_ID><ZIP_CODE>" + ZIPCode + "</ZIP_CODE><PREFERRED_LANGUAGE>" + Language + "</PREFERRED_LANGUAGE><P_MSISDN></P_MSISDN><ACCOUNT_NUMBER></ACCOUNT_NUMBER><PASSWORD_PIN></PASSWORD_PIN><NATIONAL_BUNDLE_CODE></NATIONAL_BUNDLE_CODE><NATIONAL_BUNDLE_AMOUNT></NATIONAL_BUNDLE_AMOUNT><INTERNATIONAL_BUNDLE_CODE></INTERNATIONAL_BUNDLE_CODE><INTERNATIONAL_BUNDLE_AMOUNT></INTERNATIONAL_BUNDLE_AMOUNT><TOPUP_AMOUNT></TOPUP_AMOUNT><TOPUP_CARD_ID></TOPUP_CARD_ID><VOUCHER_PIN>2329651762394</VOUCHER_PIN><CHANNEL_ID>" + ChannelID + "</CHANNEL_ID><EMAIL_ID>" + EmailID + "</EMAIL_ID></DETAILS></ACTIVATE_USIM_PORTIN_BUNDLE_REQUEST></BODY></ENVELOPE>";

                    X = "<ENVELOPE><HEADER><TRANSACTION_ID>" + TransactionID + "</TRANSACTION_ID><ENTITY>" + ChannelID + "</ENTITY><CHANNEL_REFERENCE>" + ChannelID + "</CHANNEL_REFERENCE></HEADER><BODY><ACTIVATE_USIM_PORTIN_BUNDLE_REQUEST><DETAILS><ICC_ID>" + SIMCARD + "</ICC_ID><ZIP_CODE>" + ZIPCode + "</ZIP_CODE><PREFERRED_LANGUAGE>" + Language + "</PREFERRED_LANGUAGE><P_MSISDN></P_MSISDN><ACCOUNT_NUMBER></ACCOUNT_NUMBER><PASSWORD_PIN></PASSWORD_PIN><NO_OF_MONTHS>" + hddnMonths.Value + "</NO_OF_MONTHS><NATIONAL_BUNDLE_CODE>" + BundleCode + "</NATIONAL_BUNDLE_CODE><NATIONAL_BUNDLE_AMOUNT>" + BundleAmount + "</NATIONAL_BUNDLE_AMOUNT><INTERNATIONAL_BUNDLE_CODE></INTERNATIONAL_BUNDLE_CODE><INTERNATIONAL_BUNDLE_AMOUNT></INTERNATIONAL_BUNDLE_AMOUNT><TOPUP_AMOUNT>" + InternationalCreditAMT + "</TOPUP_AMOUNT><TOPUP_CARD_ID></TOPUP_CARD_ID><VOUCHER_PIN></VOUCHER_PIN><CHANNEL_ID>" + ChannelID + "</CHANNEL_ID><EMAIL_ID>" + EmailID + "</EMAIL_ID></DETAILS></ACTIVATE_USIM_PORTIN_BUNDLE_REQUEST></BODY></ENVELOPE>";
                }

                else
                {
                    X = "<ENVELOPE><HEADER><TRANSACTION_ID>" + TransactionID + "</TRANSACTION_ID><ENTITY>" + ChannelID + "</ENTITY><CHANNEL_REFERENCE>" + ChannelID + "</CHANNEL_REFERENCE></HEADER><BODY><ACTIVATE_USIM_PORTIN_BUNDLE_REQUEST><DETAILS><ICC_ID>" + SIMCARD + "</ICC_ID><ZIP_CODE>" + ZIPCode + "</ZIP_CODE><PREFERRED_LANGUAGE>" + Language + "</PREFERRED_LANGUAGE><P_MSISDN></P_MSISDN><ACCOUNT_NUMBER></ACCOUNT_NUMBER><PASSWORD_PIN></PASSWORD_PIN><NO_OF_MONTHS>" + hddnMonths.Value + "</NO_OF_MONTHS><NATIONAL_BUNDLE_CODE>" + BundleCode + "</NATIONAL_BUNDLE_CODE><NATIONAL_BUNDLE_AMOUNT>" + BundleAmount + "</NATIONAL_BUNDLE_AMOUNT><INTERNATIONAL_BUNDLE_CODE></INTERNATIONAL_BUNDLE_CODE><INTERNATIONAL_BUNDLE_AMOUNT></INTERNATIONAL_BUNDLE_AMOUNT><TOPUP_AMOUNT>" + InternationalCreditAMT + "</TOPUP_AMOUNT><TOPUP_CARD_ID></TOPUP_CARD_ID><VOUCHER_PIN></VOUCHER_PIN><CHANNEL_ID>" + ChannelID + "</CHANNEL_ID><EMAIL_ID>" + EmailID + "</EMAIL_ID></DETAILS></ACTIVATE_USIM_PORTIN_BUNDLE_REQUEST></BODY></ENVELOPE>";
                }

                int successInsert = svc.StoredAPIRequestBeforeCall("Activation Req Before API Call", X, Convert.ToInt32(Session["DistributorID"]), TransactionID, SIMCARD, "");

                RequestRes = X;               
                Log(X, "Sending Request");
                string resp = Activation(X);
                response = resp;
                Log(resp, "Response");
                Log("", "split");
                return response;


            }
            catch (Exception ex)
            {
                return response;
                ShowPopUpMsg(response + ' ' + ex.Message);
            }
        }

        public string Activation(String X)
        {
            try
            {
                String strResponse = String.Empty;
                strResponse = la.LycaAPIRequest(ConfigurationManager.AppSettings.Get("APIURL"), X.Replace("<", "==").Replace(">", "!!"), "ACTIVATE_USIM_PORTIN_BUNDLE");

                //strResponse = SendRequest(ConfigurationManager.AppSettings.Get("APIURL"), X);

                return strResponse;

            }
            catch (Exception Ex)
            {
                return "*2*" + Ex.Message + "*";
            }

        }

        public string SendRequest(string strURI, string data)
        {
            string _result;
            _result = " ";
            try
            {
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(strURI);
                req.Headers.Add("SOAPAction", "\"ACTIVATE_USIM_PORTIN_BUNDLE\"");

                req.ContentType = "text/xml";
                req.Method = "POST";
                var writer = new StreamWriter(req.GetRequestStream());
                writer.Write(data);
                writer.Close();

                var response = (HttpWebResponse)req.GetResponse();
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

        private void resetControls(int condition)
        {
            if (condition == 1)
            {

                //ddlActivation.SelectedIndex = 0;
                txtSIMCARD.Text = string.Empty;
                txtZIPCode.Text = string.Empty;
                txtChannelID.Text = string.Empty;
                ddlLanguage.SelectedIndex = 0;

                txtAmountPay.Text = "0";
                txtZIPCode.Text = "";
                txtEmail.Text = "";
                ddlNetwork.SelectedIndex = 0;
                //ddlProduct.SelectedIndex = 0;
                ddlTariff.SelectedIndex = 0;

            }
            else
            {
                //ddlActivation.SelectedIndex = 0;
                txtSIMCARD.Text = string.Empty;
                txtZIPCode.Text = string.Empty;
                txtChannelID.Text = string.Empty;
                ddlLanguage.SelectedIndex = 0;

                txtAmountPay.Text = "0";
                txtZIPCode.Text = "";
                txtEmail.Text = "";
                ddlNetwork.SelectedIndex = 0;
                //ddlProduct.SelectedIndex = 0;
                ddlTariff.SelectedIndex = 0;

            }
        }

        public void UpdateDashboardBalanceAmount()
        {
            try
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
            catch (Exception ex)
            {

            }
        }

        protected void imgByPaypal_Click(object sender, ImageClickEventArgs e)
        {
            if (Session["LoginID"] != null)
            {
                int distr = Convert.ToInt32(Session["DistributorID"]);
                int clnt = Convert.ToInt32(Session["ClientTypeID"]);
                string simnumber = txtSIMCARD.Text.Trim();
                string transact = "";

                Boolean ValidSim = false;
                string erormsg = "";
                try
                {
                    DataSet ds = svc.CheckSimActivationService(distr, clnt, simnumber, "Activate");
                    if (ds != null)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            erormsg = Convert.ToString(ds.Tables[0].Rows[0][0]);
                            if (erormsg == "Ready to Activation")
                            {
                                ValidSim = true;
                                int id = Convert.ToInt32(ds.Tables[1].Rows[0]["TRANSACTIONID"]);
                                transact = id.ToString("00000");
                                transact = ConfigurationManager.AppSettings.Get("TXN_SERIES") + transact;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    ValidSim = false;
                    erormsg = ex.Message;
                }
                if (ValidSim == false)
                {
                    ShowPopUpMsg(erormsg);
                    return;
                }

                string amnt = "";
                double aa = (Convert.ToDouble(100 + Convert.ToDouble(ConfigurationManager.AppSettings.Get("PaypalCommission"))) * Convert.ToDouble(txtAmountPay.Text.Trim())) / 100;
                amnt = String.Format("{0:0.00}", aa);


                string iteminfo = "Activation";
                if (amnt.Trim() != "")
                {
                    if (Convert.ToDouble(amnt) > 0)
                    {

                        Session["Amount"] = txtAmountPay.Text.Trim();
                        int a = InsertPayment("Pending");

                        if (a > 0)
                        {
                            Session["PaymentId"] = a;
                            string request = ActivateSimString(hddnTariffTypeID.Value, hddnTariffType.Value, hddnTariffCode.Value, txtEmail.Text.Trim(), transact);
                            Session["RequestString"] = request;
                            //PayWithPayPal(amnt, iteminfo);    //Commented by Puneet on 11-Feb-18
                        }
                        else
                        {
                            ShowPopUpMsg("Please Contact to System Administrator");
                        }
                    }
                    else
                    {
                        ShowPopUpMsg("Amount Should be Greater Than Zero");

                    }
                }
                else
                {
                    ShowPopUpMsg("Please Fill Topup Amount");

                }

            }
        }

        public string ActivateSimString(string TariffTypeID, string TariffType, string TariffCode, string email, string TransactionID)
        {
            string request = "";
            try
            {
                email = ConfigurationManager.AppSettings.Get("Fromail");
                string ss = TariffTypeID + "," + TariffType + "," + TariffCode + "," + email + "," + TransactionID + "," + txtSIMCARD.Text.Trim() + "," + txtZIPCode.Text.Trim() + "," + txtAmountPay.Text.Trim() + "," + ddlTariff.SelectedValue;
                Session["RequestData"] = ss;
                string BundleID = TariffTypeID;
                string BundleCode = TariffCode;
                string BundleType = TariffType;
                string BundleAmount = hddnLycaAmount.Value; // txtAmountPay.Text.Trim();




                string EmailID = email;

                string X = "";
                string SIMCARD = Convert.ToString(txtSIMCARD.Text.Trim());
                string ZIPCode = Convert.ToString(txtZIPCode.Text.Trim());
                string Language = "ENGLISH";
                string ChannelID = ConfigurationManager.AppSettings.Get("CHANNEL");


                //if (BundleType == "National")
                //{
                //    //X = "<ENVELOPE><HEADER><TRANSACTION_ID>" + TransactionID + "</TRANSACTION_ID><ENTITY>ENK</ENTITY><CHANNEL_REFERENCE>ENK</CHANNEL_REFERENCE></HEADER><BODY><ACTIVATE_USIM_PORTIN_BUNDLE_REQUEST><DETAILS><ICC_ID>" + SIMCARD + "</ICC_ID><ZIP_CODE>" + ZIPCode + "</ZIP_CODE><PREFERRED_LANGUAGE>" + Language + "</PREFERRED_LANGUAGE><P_MSISDN></P_MSISDN><ACCOUNT_NUMBER></ACCOUNT_NUMBER><PASSWORD_PIN></PASSWORD_PIN><NATIONAL_BUNDLE_CODE></NATIONAL_BUNDLE_CODE><NATIONAL_BUNDLE_AMOUNT></NATIONAL_BUNDLE_AMOUNT><INTERNATIONAL_BUNDLE_CODE></INTERNATIONAL_BUNDLE_CODE><INTERNATIONAL_BUNDLE_AMOUNT></INTERNATIONAL_BUNDLE_AMOUNT><TOPUP_AMOUNT></TOPUP_AMOUNT><TOPUP_CARD_ID></TOPUP_CARD_ID><VOUCHER_PIN>2329651762394</VOUCHER_PIN><CHANNEL_ID>" + ChannelID + "</CHANNEL_ID><EMAIL_ID>" + EmailID + "</EMAIL_ID></DETAILS></ACTIVATE_USIM_PORTIN_BUNDLE_REQUEST></BODY></ENVELOPE>";

                //    X = "<ENVELOPE><HEADER><TRANSACTION_ID>" + TransactionID + "</TRANSACTION_ID><ENTITY>ENK</ENTITY><CHANNEL_REFERENCE>ENK</CHANNEL_REFERENCE></HEADER><BODY><ACTIVATE_USIM_PORTIN_BUNDLE_REQUEST><DETAILS><ICC_ID>" + SIMCARD + "</ICC_ID><ZIP_CODE>" + ZIPCode + "</ZIP_CODE><PREFERRED_LANGUAGE>" + Language + "</PREFERRED_LANGUAGE><P_MSISDN></P_MSISDN><ACCOUNT_NUMBER></ACCOUNT_NUMBER><PASSWORD_PIN></PASSWORD_PIN><NO_OF_MONTHS>" + hddnMonths.Value + "</NO_OF_MONTHS><NATIONAL_BUNDLE_CODE></NATIONAL_BUNDLE_CODE><NATIONAL_BUNDLE_AMOUNT></NATIONAL_BUNDLE_AMOUNT><INTERNATIONAL_BUNDLE_CODE></INTERNATIONAL_BUNDLE_CODE><INTERNATIONAL_BUNDLE_AMOUNT></INTERNATIONAL_BUNDLE_AMOUNT><TOPUP_AMOUNT></TOPUP_AMOUNT><TOPUP_CARD_ID></TOPUP_CARD_ID><VOUCHER_PIN>6677106918513</VOUCHER_PIN><CHANNEL_ID>" + ChannelID + "</CHANNEL_ID><EMAIL_ID>" + EmailID + "</EMAIL_ID></DETAILS></ACTIVATE_USIM_PORTIN_BUNDLE_REQUEST></BODY></ENVELOPE>";

                //    //" + BundleCode + " " + BundleAmount + "
                //}
                //else
                //{
                //    X = "<ENVELOPE><HEADER><TRANSACTION_ID>" + TransactionID + "</TRANSACTION_ID><ENTITY>ENK</ENTITY><CHANNEL_REFERENCE>ENK</CHANNEL_REFERENCE></HEADER><BODY><ACTIVATE_USIM_PORTIN_BUNDLE_REQUEST><DETAILS><ICC_ID>" + SIMCARD + "</ICC_ID><ZIP_CODE>" + ZIPCode + "</ZIP_CODE><PREFERRED_LANGUAGE>" + Language + "</PREFERRED_LANGUAGE><P_MSISDN></P_MSISDN><ACCOUNT_NUMBER></ACCOUNT_NUMBER><PASSWORD_PIN></PASSWORD_PIN><NO_OF_MONTHS>" + hddnMonths.Value + "</NO_OF_MONTHS><NATIONAL_BUNDLE_CODE></NATIONAL_BUNDLE_CODE><NATIONAL_BUNDLE_AMOUNT></NATIONAL_BUNDLE_AMOUNT><INTERNATIONAL_BUNDLE_CODE></INTERNATIONAL_BUNDLE_CODE><INTERNATIONAL_BUNDLE_AMOUNT></INTERNATIONAL_BUNDLE_AMOUNT><TOPUP_AMOUNT></TOPUP_AMOUNT><TOPUP_CARD_ID></TOPUP_CARD_ID><VOUCHER_PIN>6677106918513</VOUCHER_PIN><CHANNEL_ID>" + ChannelID + "</CHANNEL_ID><EMAIL_ID>" + EmailID + "</EMAIL_ID></DETAILS></ACTIVATE_USIM_PORTIN_BUNDLE_REQUEST></BODY></ENVELOPE>";
                //}
                if (Convert.ToString(hddnTariffCode.Value).ToLower() == "payg")
                {
                    BundleCode = "";
                    BundleAmount = "";
                }

                if (BundleType == "National")
                {
                    //X = "<ENVELOPE><HEADER><TRANSACTION_ID>" + TransactionID + "</TRANSACTION_ID><ENTITY>ENK</ENTITY><CHANNEL_REFERENCE>ENK</CHANNEL_REFERENCE></HEADER><BODY><ACTIVATE_USIM_PORTIN_BUNDLE_REQUEST><DETAILS><ICC_ID>" + SIMCARD + "</ICC_ID><ZIP_CODE>" + ZIPCode + "</ZIP_CODE><PREFERRED_LANGUAGE>" + Language + "</PREFERRED_LANGUAGE><P_MSISDN></P_MSISDN><ACCOUNT_NUMBER></ACCOUNT_NUMBER><PASSWORD_PIN></PASSWORD_PIN><NATIONAL_BUNDLE_CODE></NATIONAL_BUNDLE_CODE><NATIONAL_BUNDLE_AMOUNT></NATIONAL_BUNDLE_AMOUNT><INTERNATIONAL_BUNDLE_CODE></INTERNATIONAL_BUNDLE_CODE><INTERNATIONAL_BUNDLE_AMOUNT></INTERNATIONAL_BUNDLE_AMOUNT><TOPUP_AMOUNT></TOPUP_AMOUNT><TOPUP_CARD_ID></TOPUP_CARD_ID><VOUCHER_PIN>2329651762394</VOUCHER_PIN><CHANNEL_ID>" + ChannelID + "</CHANNEL_ID><EMAIL_ID>" + EmailID + "</EMAIL_ID></DETAILS></ACTIVATE_USIM_PORTIN_BUNDLE_REQUEST></BODY></ENVELOPE>";

                    X = "<ENVELOPE><HEADER><TRANSACTION_ID>" + TransactionID + "</TRANSACTION_ID><ENTITY>" + ChannelID + "</ENTITY><CHANNEL_REFERENCE>" + ChannelID + "</CHANNEL_REFERENCE></HEADER><BODY><ACTIVATE_USIM_PORTIN_BUNDLE_REQUEST><DETAILS><ICC_ID>" + SIMCARD + "</ICC_ID><ZIP_CODE>" + ZIPCode + "</ZIP_CODE><PREFERRED_LANGUAGE>" + Language + "</PREFERRED_LANGUAGE><P_MSISDN></P_MSISDN><ACCOUNT_NUMBER></ACCOUNT_NUMBER><PASSWORD_PIN></PASSWORD_PIN><NO_OF_MONTHS>" + hddnMonths.Value + "</NO_OF_MONTHS><NATIONAL_BUNDLE_CODE>" + BundleCode + "</NATIONAL_BUNDLE_CODE><NATIONAL_BUNDLE_AMOUNT>" + BundleAmount + "</NATIONAL_BUNDLE_AMOUNT><INTERNATIONAL_BUNDLE_CODE></INTERNATIONAL_BUNDLE_CODE><INTERNATIONAL_BUNDLE_AMOUNT></INTERNATIONAL_BUNDLE_AMOUNT><TOPUP_AMOUNT></TOPUP_AMOUNT><TOPUP_CARD_ID></TOPUP_CARD_ID><VOUCHER_PIN></VOUCHER_PIN><CHANNEL_ID>" + ChannelID + "</CHANNEL_ID><EMAIL_ID>" + EmailID + "</EMAIL_ID></DETAILS></ACTIVATE_USIM_PORTIN_BUNDLE_REQUEST></BODY></ENVELOPE>";
                }
                else
                {
                    X = "<ENVELOPE><HEADER><TRANSACTION_ID>" + TransactionID + "</TRANSACTION_ID><ENTITY>" + ChannelID + "</ENTITY><CHANNEL_REFERENCE>" + ChannelID + "</CHANNEL_REFERENCE></HEADER><BODY><ACTIVATE_USIM_PORTIN_BUNDLE_REQUEST><DETAILS><ICC_ID>" + SIMCARD + "</ICC_ID><ZIP_CODE>" + ZIPCode + "</ZIP_CODE><PREFERRED_LANGUAGE>" + Language + "</PREFERRED_LANGUAGE><P_MSISDN></P_MSISDN><ACCOUNT_NUMBER></ACCOUNT_NUMBER><PASSWORD_PIN></PASSWORD_PIN><NO_OF_MONTHS>" + hddnMonths.Value + "</NO_OF_MONTHS><NATIONAL_BUNDLE_CODE>" + BundleCode + "</NATIONAL_BUNDLE_CODE><NATIONAL_BUNDLE_AMOUNT>" + BundleAmount + "</NATIONAL_BUNDLE_AMOUNT><INTERNATIONAL_BUNDLE_CODE></INTERNATIONAL_BUNDLE_CODE><INTERNATIONAL_BUNDLE_AMOUNT></INTERNATIONAL_BUNDLE_AMOUNT><TOPUP_AMOUNT></TOPUP_AMOUNT><TOPUP_CARD_ID></TOPUP_CARD_ID><VOUCHER_PIN></VOUCHER_PIN><CHANNEL_ID>" + ChannelID + "</CHANNEL_ID><EMAIL_ID>" + EmailID + "</EMAIL_ID></DETAILS></ACTIVATE_USIM_PORTIN_BUNDLE_REQUEST></BODY></ENVELOPE>";
                }


                request = X;
                return request;


            }
            catch (Exception ex)
            {
                return request;

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
                    sp.PaymentType = 4;
                    sp.PayeeID = Convert.ToInt32(Session["DistributorID"]);
                    sp.PaymentFrom = 9;
                    sp.ActivationVia = 18;
                    sp.TransactionStatusId = 23;
                    sp.TransactionStatus = ststus;
                    sp.PaymentMode = "PayPal Activation";
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
            redirecturl += "&return=" + ConfigurationManager.AppSettings["ActivationSuccessURL"].ToString();
            redirecturl += "&cancel_return=" + ConfigurationManager.AppSettings["ActivationFailedURL"].ToString();
            Response.Redirect(redirecturl);
        }

        private void ShowPopUpMsg(string msg)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("alert('");
            sb.Append(msg.Replace("\n", "\\n").Replace("\r", "").Replace("'", "\\'"));
            sb.Append("');");
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "showalert", sb.ToString(), true);
        }

        public void Log(string ss, string condition)
        {
            try
            {
                //string filename = "lycalog.txt";
                //string strPath = Server.MapPath("Log") + "/" + filename;
                //string root = "http://" + HttpContext.Current.Request.Url.Host + HttpContext.Current.Request.ApplicationPath + "/Log/" + filename;

                //if (File.Exists(strPath))
                //{
                //    StreamWriter sw = new StreamWriter(strPath, true, Encoding.Unicode);
                //    if (condition != "split")
                //    {                        
                //        sw.WriteLine(condition + "  " + DateTime.UtcNow.AddMinutes(330));                       
                //        sw.WriteLine(ss);                      
                //        sw.Close();
                //    }
                //    else
                //    {                        
                //        sw.WriteLine("------------------------------------------------Gap-----------------------------------------------");                      
                //        sw.Close();
                //    }
                //}
            }
            catch (Exception ex)
            {

            }

        }

        public void Log1(string ss, string condition)
        {
            try
            {

                string filename = "ActivationLog.txt";
                string strPath = Server.MapPath("Log") + "/" + filename;
                string root = "https://" + HttpContext.Current.Request.Url.Host + HttpContext.Current.Request.ApplicationPath + "/Log/" + filename;

                if (File.Exists(strPath))
                {
                    StreamWriter sw = new StreamWriter(strPath, true, Encoding.Unicode);
                    if (condition != "split")
                    {
                        sw.WriteLine(condition + "  " + DateTime.UtcNow.AddMinutes(330));
                        sw.WriteLine(ss);
                        sw.Close();
                    }
                    else
                    {
                        sw.WriteLine("------------------------------------------------Gap-----------------------------------------------");
                        sw.Close();
                    }
                }
            }
            catch (Exception ex)
            {

            }

        }

        public void SendMail(string SendTo, string Subject, string UserName, string UserID, string pass)
        {
            try
            {
                string LoginUrl = "https://" + HttpContext.Current.Request.Url.Host + HttpContext.Current.Request.ApplicationPath + "/Login.aspx";
                string LogoUrl = "https://" + HttpContext.Current.Request.Url.Host + HttpContext.Current.Request.ApplicationPath + "/img/logo.png";
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient();


                string MailAddress = ConfigurationManager.AppSettings.Get("Fromail");
                string PassWord = ConfigurationManager.AppSettings.Get("Password");

                mail.From = new MailAddress(MailAddress);
                mail.To.Add(SendTo);
                TimeSpan ts = new TimeSpan(8, 0, 0);
                mail.Subject = Subject + " " + DateTime.UtcNow.Subtract(ts).ToString();

                StringBuilder sb = new StringBuilder();
                sb.Append("<html>");
                sb.Append("<body style=”color:grey; font-size:15px;”>");
                sb.Append("<font face=”Helvetica, Arial, sans-serif”>");

                sb.Append("<div style=”position:absolute; height:200px; width:100px; background-color:0d1d36; padding:30px;”>");
                sb.Append("<img src=" + LogoUrl + " />");
                sb.Append("</div>");

                sb.Append("<br/>");

                sb.Append("<br/>");

                sb.Append("<div style=”background-color: #ece8d4; width:600px; height:200px; padding:30px; margin-top:30px;”>");
                //sb.Append("<p>Please find the new credentials and get started.</p>");

                sb.Append("<p>Sim Number  " + UserID + "  Activated successfully on Mobile Number  " + UserName + " <p>");


                sb.Append("<p>");
                sb.Append("<br/>");



                if (ddlNetwork.SelectedItem.Text == "Lyca Mobile")
                {

                    sb.Append("<p> Activation successfully, If you have any issue  with Recharge please contact Lycamobile directly at +1-845-301-1633 / +1-866-277-0024 <p>");

                }
                else if (ddlNetwork.SelectedItem.Text == "H20")
                {
                    sb.Append("<p> Activation successfully, If you have any issue  with Recharge please contact H2O WIRELESS. <p>");

                    sb.Append("<p> Dealer Hotline 1-800-939-1261 <p>");
                    sb.Append("<p> We are open 7 days a week from 9AM EST to 12AM EST Monday - Friday, and 9AM EST to 11PM EST on weekends. <p>");
                    sb.Append("<p> H2O GSM Support 1-800-643-4926 <p>");
                    sb.Append("<p>Email: customercare@h2owirelessnow.com <p>");


                }





                sb.Append("<br/>");
                sb.Append("<p>Sincerely,");
                sb.Append("<p>" + ConfigurationManager.AppSettings.Get("COMPANY_NAME") + "</p>");
                sb.Append("<p>Thank you</p>");


                sb.Append("<p>----------------------------------------------------------------");
                sb.Append("<p>Please do not reply to this email. This mailbox is not monitored and you will not receive a response. ");

                sb.Append("<br/>");
                sb.Append("</div>");
                sb.Append("</body>");
                sb.Append("</html>");

                mail.Body = sb.ToString();
                mail.BodyEncoding = System.Text.Encoding.UTF8;
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.High;

                SmtpServer.Host = ConfigurationManager.AppSettings.Get("Host");
                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential(MailAddress, PassWord);
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);

            }
            catch (Exception ex)
            {

            }
        }

        // add by akash starts
        public void SendMailWithDetail(string SendTo, string Subject, string UserName, string UserID, string transaction, string _PreviousBalance, string _CurrentBalance)
        {
            try
            {
                // *****
                string _RetailerName = "";
                string _PhnNo = "";
                string _Product = "";
                string _RefrenceNo = "";
                string _TransactionDate = "";
                string _Month = "";
                string _SalesAmount = "";
              
                //
                // current balance for distributor
                //int DistributorID = Convert.ToInt32(Session["DistributorID"]);
                //DataSet ds1 = null;
                //ds1 = svc.GetCurrentbalance(DistributorID);
                //if (ds1 != null)
                //{
                //    if (ds1.Tables[0].Rows.Count > 0)
                //    {
                //        _CurrentBalance = Convert.ToString(ds1.Tables[0].Rows[0]["AccountBalance"]);
                //    }
                //}
                //
                string tid = Convert.ToString(transaction);
                if (tid != "")
                {
                    DataSet ds = null;
                    ds = svc.GetPrintRecipt(tid);
                    DataTable dtblprintDetails = new DataTable();
                    if (ds != null && ds.Tables.Count > 1)
                    {
                        dtblprintDetails = ds.Tables[0];
                        if (dtblprintDetails.Rows.Count > 0)
                        {
                            string PhnoFormate = "";
                            _RetailerName = Convert.ToString(dtblprintDetails.Rows[0][6]);
                            string Phone_Number = Convert.ToString(dtblprintDetails.Rows[0][0]);
                            if (Phone_Number.Length >= 10)
                            {
                                Phone_Number = Phone_Number.Substring(Phone_Number.Length - 10);
                                string PhoneNumber = Convert.ToString(Phone_Number);
                                PhnoFormate = Convert.ToInt64(PhoneNumber).ToString("(###)-###-####");
                            }
                            // string PhnoFormate = PhoneNumber.Format("{0:(###) ###-####}";
                            //string PhnoFormate = formatPhone(PhoneNumber);
                             _PhnNo = PhnoFormate;
                             _Product = Convert.ToString(dtblprintDetails.Rows[0][1]);
                             _RefrenceNo = Convert.ToString(dtblprintDetails.Rows[0][2]);
                             _TransactionDate = Convert.ToString(dtblprintDetails.Rows[0][3]);
                             _Month = Convert.ToString(dtblprintDetails.Rows[0][8]);
                             //_CurrentBalance = Convert.ToString(Convert.ToDecimal(_PAccountBal) - Convert.ToDecimal(dtblprintDetails.Rows[0][9]));                            
                             _SalesAmount = Convert.ToString(Convert.ToDecimal(dtblprintDetails.Rows[0][9]));
                        }
                    }
                }                 

                // *****

                string LoginUrl = "https://" + HttpContext.Current.Request.Url.Host + HttpContext.Current.Request.ApplicationPath + "/Login.aspx";
                string LogoUrl = "https://" + HttpContext.Current.Request.Url.Host + HttpContext.Current.Request.ApplicationPath + "/img/logo.png";
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient();


                string MailAddress = ConfigurationManager.AppSettings.Get("Fromail");
                string PassWord = ConfigurationManager.AppSettings.Get("Password");

                mail.From = new MailAddress(MailAddress);
                mail.To.Add(SendTo);
                TimeSpan ts = new TimeSpan(8, 0, 0);
                mail.Subject = Subject + " " + DateTime.UtcNow.Subtract(ts).ToString();

                StringBuilder sb = new StringBuilder();
                sb.Append("<html>");
                sb.Append("<body style=”color:grey; font-size:15px;”>");
                sb.Append("<font face=”Helvetica, Arial, sans-serif”>");

                sb.Append("<div style=”position:absolute; height:200px; width:100px; background-color:0d1d36; padding:30px;”>");
                sb.Append("<img src=" + LogoUrl + " />");
                sb.Append("</div>");

                sb.Append("<br/>");

                sb.Append("<br/>");

                sb.Append("<div style=”background-color: #ece8d4; width:600px; height:200px; padding:30px; margin-top:30px;”>");
                //sb.Append("<p>Please find the new credentials and get started.</p>");

                sb.Append("<p>Sim Number/Recharge  " + UserID + "  Activated successfully on Mobile Number  " + UserName + " <p>");

                sb.Append("<p>Transaction Date :  " + _TransactionDate + " <p>");
                sb.Append("<p>TransactionID :  " + tid + " <p>");
                sb.Append("<p>Plan : " + _Product + " <p>");
                sb.Append("<p>No. Of Month : " + _Month + " <p>");
                sb.Append("<p>Plan Cost : $" + _SalesAmount + " <p>");
                sb.Append("<p>Previous Balance : $" + _PreviousBalance + " <p>");
                sb.Append("<p>Current Balance : $" + _CurrentBalance + " <p>");

                //sb.Append("<table width='100%' border='1'><tr>");
                //sb.Append("<td><b>Date</b></td>");
                //sb.Append("<td><b>TransactionID</b></td>");
                //sb.Append("<td><b>Plan</b></td>");
                //sb.Append("<td><b>No. Of Month</b></td>");
                //sb.Append("<td><b>Plan Cost</b></td>");
                //sb.Append("<td><b>Previous Balance</b></td>");
                //sb.Append("<td><b>Current Balance</b></td></tr>");

                //sb.Append("<tr><td> " + _TransactionDate + "</td>");
                //sb.Append("<td> " + tid + "</td>");
                //sb.Append("<td> " + _Product + "</td>");
                //sb.Append("<td> " + _RegulatoryFees + "</td>");
                //sb.Append("<td>" + _SalesAmount + "</td>");
                //sb.Append("<td> " + _PAccountBal + "</td>");
                //sb.Append("<td>" + _CurrentBalance + "</td></tr>");


                //sb.Append("<p>");
                //sb.Append("<br/>");
                //sb.Append("</div>");

                //sb.Append("<br/>");

                //sb.Append("<br/>");

                sb.Append("<div style=”background-color: #ece8d4; width:600px; height:200px; padding:30px; margin-top:30px;”>");
                //if (ddlNetwork.SelectedItem.Text == "Lyca Mobile")
                //{

                sb.Append("<p> Activation successfully, If you have any issue  with Recharge please contact Lycamobile directly at +1-213-213-5880 <p>");

                //}
                //else if (ddlNetwork.SelectedItem.Text == "H20")
                //{
                //    sb.Append("<p> Activation successfully, If you have any issue  with Recharge please contact H2O WIRELESS. <p>");

                //    sb.Append("<p> Dealer Hotline 1-800-939-1261 <p>");
                //    sb.Append("<p> We are open 7 days a week from 9AM EST to 12AM EST Monday - Friday, and 9AM EST to 11PM EST on weekends. <p>");
                //    sb.Append("<p> H2O GSM Support 1-800-643-4926 <p>");
                //    sb.Append("<p>Email: customercare@h2owirelessnow.com <p>");


                //}





                sb.Append("<br/>");
                sb.Append("<p>Sincerely,");
                sb.Append("<p>" + ConfigurationManager.AppSettings.Get("COMPANY_NAME") + "</p>");
                sb.Append("<p>Thank you</p>");


                sb.Append("<p>----------------------------------------------------------------");
                sb.Append("<p>Please do not reply to this email. This mailbox is not monitored and you will not receive a response. ");

                sb.Append("<br/>");
                sb.Append("</div>");
                sb.Append("</body>");
                sb.Append("</html>");

                mail.Body = sb.ToString();
                mail.BodyEncoding = System.Text.Encoding.UTF8;
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.High;

                SmtpServer.Host = ConfigurationManager.AppSettings.Get("Host");
                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential(MailAddress, PassWord);
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);

            }
            catch (Exception ex)
            {

            }
        }
        // add by akash ends

        protected void ddlNetwork_SelectedIndexChanged(object sender, EventArgs e)
        {

            try
            {
                DataTable dtSIMPurchase = new DataTable();

                txtRegulatry.Text = "0";
                txtAmt.Text = "0";

                divCity.Attributes.Add("style", "display:none");
                divProduct.Attributes.Add("style", "display:none");

                divActivation.Attributes.Add("style", "display:block");
                ddlTariff.SelectedIndex = -1;
                ddlProduct.SelectedIndex = -1;
                txtAmountPay.Text = "0";

                //// add by akash starts
                //txtZIPCode.Text = "";
                //ddlCountry.Items.Clear();
                //if (Convert.ToInt32(ddlNetwork.SelectedValue) == 15)
                //{
                //    lblZipcode.InnerText = "Area Code *";
                //    DivCountry.Visible = true;
                //    Session["IsH2O"] = 1;
                //}
                //else
                //{
                //    lblZipcode.InnerText = "Zip Code *";
                //    DivCountry.Visible = false;
                //    Session["IsH2O"] = 0;
                //}
                //// add by akash ends

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        protected void ddlProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dtSIMPurchase = new DataTable();
            txtRegulatry.Text = "0";
            txtAmt.Text = "0";

            if (ddlNetwork.SelectedItem.Text == "H20")
            {

                //dtSIMPurchase = (DataTable)ViewState["Product"];

                //DataView dv = dtSIMPurchase.DefaultView;
                //dv.RowFilter = "ProductId  = "+ Convert.ToInt32(ddlProduct.SelectedValue);
                //dtSIMPurchase = dv.ToTable();
                //string Amount = dtSIMPurchase.Rows[0]["Amount"].ToString();
                // ViewState["CurrencySymbol"] = dtSIMPurchase.Rows[0]["CurrencySymbol"].ToString();

                int DistributorID = Convert.ToInt32(Session["DistributorID"]);
                int ClientTypeID = Convert.ToInt32(Session["ClientTypeID"]);
                int LoginID = Convert.ToInt32(Session["LoginId"]);
                int tariffID = Convert.ToInt32(ddlProduct.SelectedValue);
                DataSet dst = svc.GetSingleTariffDetailForActivationService(LoginID, DistributorID, ClientTypeID, tariffID, Convert.ToByte(hddnMonths.Value.ToString()), "Activate");

                DataTable dt = new DataTable();
                dt = dst.Tables[1];
                string Amount = "0";
                if (dt.Rows.Count > 0)
                {
                    Amount = dt.Rows[0]["Rental"].ToString();
                    hddnTariffCode.Value = dt.Rows[0]["TariffCode"].ToString();
                }
                ViewState["CurrencySymbol"] = "$";

                if (Amount != "")
                {
                    txtAmt.Text = Amount;
                    txtAmountPay.Text = Amount;
                }
                else
                {
                    txtAmountPay.Text = "0";
                    txtAmt.Text = "0";
                }

            }

            if (ddlNetwork.SelectedItem.Text == "EasyGo")
            {
                //dtSIMPurchase = (DataTable)ViewState["Product"];
                //DataView dv = dtSIMPurchase.DefaultView;
                //dv.RowFilter = "ProductId  = " + Convert.ToInt32(ddlProduct.SelectedValue);
                //dtSIMPurchase = dv.ToTable();
                //string Amount = dtSIMPurchase.Rows[0]["Amount"].ToString();
                //ViewState["CurrencySymbol"] = dtSIMPurchase.Rows[0]["CurrencySymbol"].ToString();

                int DistributorID = Convert.ToInt32(Session["DistributorID"]);
                int ClientTypeID = Convert.ToInt32(Session["ClientTypeID"]);
                int LoginID = Convert.ToInt32(Session["LoginId"]);
                int tariffID = Convert.ToInt32(ddlProduct.SelectedValue);
                DataSet dst = svc.GetSingleTariffDetailForActivationService(LoginID, DistributorID, ClientTypeID, tariffID, Convert.ToByte(hddnMonths.Value.ToString()), "Activate");

                DataTable dt = new DataTable();
                dt = dst.Tables[2];
                string Amount = "0";
                if (dt.Rows.Count > 0)
                {
                    Amount = dt.Rows[0]["Rental"].ToString();
                    hddnTariffCode.Value = dt.Rows[0]["TariffCode"].ToString();


                }

                ViewState["CurrencySymbol"] = "$";


                if (Amount != "")
                {
                    txtAmountPay.Text = Amount;
                    txtAmt.Text = Amount;
                }
                else
                {
                    txtAmountPay.Text = "0";
                }

            }

        }

        public string CheckMsisdnWithLyca(String X)
        {
            try
            {
                String strResponse = String.Empty;
                strResponse = la.LycaAPIRequest(ConfigurationManager.AppSettings.Get("APIURL"), X.Replace("<", "==").Replace(">", "!!"), "CHECK_AUPB_TRANSACTION_STATUS");

                //strResponse = SendRequest(ConfigurationManager.AppSettings.Get("APIURL"), X);

                return strResponse;

            }
            catch (Exception Ex)
            {
                return "*2*" + Ex.Message + "*";
            }

        }

        protected void txtSIMCARD_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ddlNetwork.Attributes.Remove("Disabled");
                DataTable dtPreCheck = new DataTable();
                dtPreCheck = svc.PreloadedSIMCheck(Convert.ToString(txtSIMCARD.Text));
                if (dtPreCheck.Rows.Count > 0)
                {
                    ShowPopUpMsg("It can't be activated from this page.This is a preloaded Sim,");
                    return;
                }
                //CHECK_AUPB_TRANSACTION_STATUS
                //<ENVELOPE><HEADER><TRANSACTION_ID>E089</TRANSACTION_ID><ENTITY>ENKWIR</ENTITY><CHANNEL_REFERENCE>CCARE</CHANNEL_REFERENCE></HEADER><BODY><CHECK_AUPB_TRANSACTION_STATUS_REQUEST><DETAILS><ICC_ID>8919601000184470157</ICC_ID><TRANSACTION_ID></TRANSACTION_ID></DETAILS></CHECK_AUPB_TRANSACTION_STATUS_REQUEST></BODY></ENVELOPE>
                string SIMCARD = Convert.ToString(txtSIMCARD.Text.Trim());
                if (SIMCARD.Length > 0)
                {
                    string transact = "";
                    string X = "";
                    string ChannelID = ConfigurationManager.AppSettings.Get("CHANNEL");

                    DataTable dtTrans = svc.GetTransactionIDService();
                    int id = Convert.ToInt32(dtTrans.Rows[0]["TRANSACTIONID"]);
                    transact = id.ToString("00000");
                    transact = ConfigurationManager.AppSettings.Get("TXN_SERIES") + transact;

                    X = "<ENVELOPE><HEADER><TRANSACTION_ID>" + transact + "</TRANSACTION_ID><ENTITY>" + ChannelID + "</ENTITY><CHANNEL_REFERENCE>" + ChannelID + "</CHANNEL_REFERENCE></HEADER><BODY><CHECK_AUPB_TRANSACTION_STATUS_REQUEST><DETAILS><ICC_ID>" + SIMCARD + "</ICC_ID><TRANSACTION_ID></TRANSACTION_ID></DETAILS></CHECK_AUPB_TRANSACTION_STATUS_REQUEST></BODY></ENVELOPE>";
                    RequestRes = X;
                    Log(X, "Sending Request");

                    //
                  
                    string resp = CheckMsisdnWithLyca(X);

                    Log(resp, "Response");
                    Log("", "split");

                    if (resp.Trim() != null && resp.Trim() != "")
                    {
                        StringReader theReader = new StringReader(resp);
                        DataSet theDataSet = new DataSet();
                        theDataSet.ReadXml(theReader);
                        if (theDataSet.Tables.Count > 0)
                        {
                            DataTable dt = theDataSet.Tables[0];
                            if (dt.Rows.Count > 0)
                            {
                                if (dt.Rows[0]["ERROR_CODE"].ToString() == "0")
                                {
                                    ShowPopUpMsg("Sim is already activated with Mobile No. " + theDataSet.Tables[2].Rows[0]["ALLOCATED_MSISDN"].ToString());
                                    btnCompanyByAccount.Enabled = false;
                                    btnDistributorByAccount.Enabled = false;
                                    return;
                                }

                            }
                        }
                    }

                    btnCompanyByAccount.Enabled = true;
                    btnDistributorByAccount.Enabled = true;
                    ddlMonth.SelectedValue = Convert.ToString(1);
                    DataSet ds = new DataSet();
                    hddnPrepaidSIM.Value = "0";
                    ds = svc.GetSimNetwork(Convert.ToString(txtSIMCARD.Text), "Activate");
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        if (Convert.ToString(ds.Tables[0].Rows[0]["Months"]) != "0")
                        {
                            ddlMonth.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["Months"]);
                        }
                        else
                        { ddlMonth.SelectedValue = "1"; }
                        //ddlMonth.SelectedValue.Enabled = false;
                        if (hddnMonths.Value.ToString() == "")
                        {
                            hddnMonths.Value = ddlMonth.SelectedValue;
                        }
                        // 14 is Deactive Network(due to Error show)

                        if (ds.Tables[0].Rows[0]["VendorID"].ToString() != "14")
                        {
                            ddlNetwork.SelectedValue = Convert.ToInt32(ds.Tables[0].Rows[0]["VendorID"]).ToString();
                            hdnVendorID.Value = Convert.ToString(ds.Tables[0].Rows[0]["VendorID"]);
                            hdnPurchaseID.Value = Convert.ToString(ds.Tables[0].Rows[0]["purchaseid"]);
                            // add 
                            ddlNetwork.Attributes.Add("disabled", "disabled");
                            //
                            ddlNetwork_SelectedIndexChanged(null, null);
                            if (Convert.ToInt32(ds.Tables[0].Rows[0]["tariffid"]) > 0)
                            {

                                ddlTariff.Text = ds.Tables[0].Rows[0]["tariffid"].ToString();
                                ddlTariff_SelectedIndexChanged(null, null);
                                ddlTariff.Enabled = false;
                                hddnPrepaidSIM.Value = "1";
                                ddlMonth.Enabled = false;
                            }
                            else
                            {
                                ddlTariff.Enabled = true;
                                hddnPrepaidSIM.Value = "0";
                            }

                        }


                        else
                        {
                            hdnVendorID.Value = "0";
                            hdnPurchaseID.Value = "0";
                            ddlNetwork.SelectedIndex = 1;
                            ddlTariff.Enabled = true;
                        }
                    }
                    else
                    {
                        hdnVendorID.Value = "0";
                        hdnPurchaseID.Value = "0";
                        ddlNetwork.SelectedIndex = 1;
                        ddlMonth.Enabled = true;
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }

        private void UpdatePurchaseSimNetwork()
        {
            try
            {
                if (hdnPurchaseID.Value != "" && hdnVendorID.Value != "")
                {
                    svc.UpdatePurchaseSimNetwork(Convert.ToInt32(ddlNetwork.SelectedValue), Convert.ToString(txtSIMCARD.Text), Convert.ToInt64(hdnPurchaseID.Value));
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }

        protected void chkActivePort_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkActivePort.Checked == true)
                {
                    DivPort.Visible = true;
                    txtPHONETOPORT.Enabled = true;
                    txtACCOUNT.Enabled = true;
                    txtPIN.Enabled = true;
                }
                else
                {
                    DivPort.Visible = false;
                    txtPHONETOPORT.Enabled = false;
                    txtACCOUNT.Enabled = false;
                    txtPIN.Enabled = false;
                }
            }
            catch (Exception ex)
            {

            }
        }

        //Both DDl ANKIT SINGH 
        //protected void ddlDataAddOn_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (Session["LoginID"] != null)
        //        {
        //            if (ddlDataAddOn.SelectedIndex > 0)
        //            {

        //                int DistributorID = Convert.ToInt32(Session["DistributorID"]);
        //                int tariffID = Convert.ToInt32(ddlDataAddOn.SelectedValue);
        //                DataSet dst = svc.GetDiscountAndRental(DistributorID, tariffID);
        //                if (dst != null)
        //                {
        //                    if (dst.Tables[0].Rows.Count > 0)
        //                    {
        //                        decimal Discount = Convert.ToDecimal(dst.Tables[0].Rows[0]["Commission"]);
        //                        decimal rental = Convert.ToDecimal(dst.Tables[0].Rows[0]["Rental"]);

        //                        decimal INCREMENT = (rental / Convert.ToDecimal(100.00)) * ((Convert.ToDecimal(100.00) - Discount));
        //                        hddnAddOn.Value = INCREMENT.ToString("0.00");
        //                        decimal AMT = Convert.ToDecimal(txtAmt.Text);
        //                        decimal Regulatry = Convert.ToDecimal(txtRegulatry.Text);
        //                        decimal SET = INCREMENT + AMT + Regulatry;
        //                        txtAmountPay.Text = (SET + Convert.ToDecimal(hddnInternational.Value)).ToString("0.00");


        //                        hddnDataAddOnValue.Value = rental.ToString("0.00");
        //                        hddnDataAddOnDiscountedAmount.Value = INCREMENT.ToString("0.00");
        //                        hddnDataAddOnDiscountPercent.Value = Discount.ToString("0.00");
        //                    }
        //                }
        //            }
        //            else
        //            {
        //                decimal Discount = Convert.ToDecimal(0.00);
        //                decimal rental = Convert.ToDecimal(0.00);
        //                decimal INCREMENT = (rental / Convert.ToDecimal(100.00)) * ((Convert.ToDecimal(100.00) - Discount));
        //                hddnAddOn.Value = INCREMENT.ToString("0.00");
        //                decimal AMT = Convert.ToDecimal(txtAmt.Text);
        //                decimal Regulatry = Convert.ToDecimal(txtRegulatry.Text);
        //                decimal SET = INCREMENT + AMT + Regulatry;
        //                txtAmountPay.Text = (SET + Convert.ToDecimal(hddnInternational.Value)).ToString("0.00");


        //                hddnDataAddOnValue.Value = rental.ToString("0.00");
        //                hddnDataAddOnDiscountedAmount.Value = INCREMENT.ToString("0.00");
        //                hddnDataAddOnDiscountPercent.Value = Discount.ToString("0.00");
        //            }
        //            // }

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //}

        //protected void ddlInternationalCreadits_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (Session["LoginID"] != null)
        //        {
        //            if (ddlDataAddOn.SelectedIndex > 0)
        //            {

        //                int DistributorID = Convert.ToInt32(Session["DistributorID"]);
        //                int tariffID = Convert.ToInt32(ddlInternationalCreadits.SelectedValue);
        //                DataSet dst = svc.GetDiscountAndRental(DistributorID, tariffID);
        //                if (dst != null)
        //                {
        //                    if (dst.Tables[0].Rows.Count > 0)
        //                    {
        //                        decimal Discount = Convert.ToDecimal(dst.Tables[0].Rows[0]["Commission"]);
        //                        decimal rental = Convert.ToDecimal(dst.Tables[0].Rows[0]["Rental"]);

        //                        decimal INCREMENT = (rental / Convert.ToDecimal(100.00)) * ((Convert.ToDecimal(100.00) - Discount));
        //                        hddnInternational.Value = INCREMENT.ToString("0.00");

        //                        decimal AMT = Convert.ToDecimal(txtAmt.Text);
        //                        decimal Regulatry = Convert.ToDecimal(txtRegulatry.Text);
        //                        decimal SET = INCREMENT + AMT + Regulatry;
        //                        txtAmountPay.Text = (SET + Convert.ToDecimal(hddnAddOn.Value)).ToString("0.00");



        //                        hddnInternationalCreditValue.Value = rental.ToString("0.00");
        //                        hddnInternationalCreditDiscountedAmount.Value = INCREMENT.ToString("0.00");
        //                        hddnInternationalCreditDiscountPercent.Value = Discount.ToString("0.00");


        //                    }
        //                }
        //            }
        //            else
        //            {
        //                decimal Discount = Convert.ToDecimal(0.00);
        //                decimal rental = Convert.ToDecimal(0.00);

        //                decimal INCREMENT = (rental / Convert.ToDecimal(100.00)) * ((Convert.ToDecimal(100.00) - Discount));
        //                hddnInternational.Value = INCREMENT.ToString("0.00");

        //                decimal AMT = Convert.ToDecimal(txtAmt.Text);
        //                decimal Regulatry = Convert.ToDecimal(txtRegulatry.Text);
        //                decimal SET = INCREMENT + AMT + Regulatry;
        //                txtAmountPay.Text = (SET + Convert.ToDecimal(hddnAddOn.Value)).ToString("0.00");



        //                hddnInternationalCreditValue.Value = rental.ToString("0.00");
        //                hddnInternationalCreditDiscountedAmount.Value = INCREMENT.ToString("0.00");
        //                hddnInternationalCreditDiscountPercent.Value = Discount.ToString("0.00");
        //            }
        //        }
        //    }

        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}


        protected void ddlDataAddOn_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (Session["LoginID"] != null)
                {
                    //txtRegulatry.Text = "0";
                    //txtAmt.Text = "0";

                    //if (Convert.ToString(Session["ClientType"]) == "Company")
                    //{
                    if (ddlDataAddOn.SelectedIndex > 0)
                    {

                        int DistributorID = Convert.ToInt32(Session["DistributorID"]);
                        int tariffID = Convert.ToInt32(ddlDataAddOn.SelectedValue);
                        DataSet dst = svc.GetDiscountAndRental(DistributorID, tariffID);
                        if (dst != null)
                        {
                            if (dst.Tables[0].Rows.Count > 0)
                            {
                                decimal Discount = Convert.ToDecimal(dst.Tables[0].Rows[0]["Commission"]);
                                decimal rental = Convert.ToDecimal(dst.Tables[0].Rows[0]["Rental"]);

                                decimal INCREMENT = (rental / Convert.ToDecimal(100.00)) * ((Convert.ToDecimal(100.00) - Discount));
                                hddnAddOn.Value = INCREMENT.ToString("0.00");
                                decimal AMT = Convert.ToDecimal(txtAmt.Text);
                                decimal Regulatry = Convert.ToDecimal(txtRegulatry.Text);
                                decimal SET = INCREMENT + AMT + Regulatry;
                                txtAmountPay.Text = (SET + Convert.ToDecimal(hddnInternational.Value)).ToString("0.00");


                                hddnDataAddOnValue.Value = rental.ToString("0.00");
                                hddnDataAddOnDiscountedAmount.Value = INCREMENT.ToString("0.00");
                                hddnDataAddOnDiscountPercent.Value = Discount.ToString("0.00");
                            }
                        }
                        //ddlTariff.Enabled = false;
                        //ddlInternationalCreadits.Enabled = false;
                    }
                    else
                    {
                        hddnAddOn.Value = "0.00";
                        decimal AMT = Convert.ToDecimal(txtAmt.Text);
                        decimal Regulatry = Convert.ToDecimal(txtRegulatry.Text);
                        decimal SET = AMT + Regulatry;
                        txtAmountPay.Text = (SET + Convert.ToDecimal(hddnInternational.Value)).ToString("0.00");

                        hddnDataAddOnValue.Value = "0.00";
                        hddnDataAddOnDiscountedAmount.Value = "0.00";
                        hddnDataAddOnDiscountPercent.Value = "0.00";
                        ddlInternationalCreadits.Enabled = true;
                        ddlTariff.Enabled = true;

                    }
                    //}
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        protected void ddlInternationalCreadits_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (Session["LoginID"] != null)
                {
                    //txtRegulatry.Text = "0";
                    //txtAmt.Text = "0";

                    //if (Convert.ToString(Session["ClientType"]) == "Company")
                    //{
                    if (ddlInternationalCreadits.SelectedIndex > 0)
                    {

                        int DistributorID = Convert.ToInt32(Session["DistributorID"]);
                        int tariffID = Convert.ToInt32(ddlInternationalCreadits.SelectedValue);
                        DataSet dst = svc.GetDiscountAndRental(DistributorID, tariffID);
                        if (dst != null)
                        {
                            if (dst.Tables[0].Rows.Count > 0)
                            {
                                decimal Discount = Convert.ToDecimal(dst.Tables[0].Rows[0]["Commission"]);
                                decimal rental = Convert.ToDecimal(dst.Tables[0].Rows[0]["Rental"]);

                                decimal INCREMENT = (rental / Convert.ToDecimal(100.00)) * ((Convert.ToDecimal(100.00) - Discount));
                                hddnInternational.Value = INCREMENT.ToString("0.00");

                                decimal AMT = Convert.ToDecimal(txtAmt.Text);
                                decimal Regulatry = Convert.ToDecimal(txtRegulatry.Text);
                                decimal SET = INCREMENT + AMT + Regulatry;
                                txtAmountPay.Text = (SET + Convert.ToDecimal(hddnAddOn.Value)).ToString("0.00");



                                hddnInternationalCreditValue.Value = rental.ToString("0.00");
                                hddnInternationalCreditDiscountedAmount.Value = INCREMENT.ToString("0.00");
                                hddnInternationalCreditDiscountPercent.Value = Discount.ToString("0.00");


                            }
                        }
                        //ddlTariff.Enabled = false;
                        //ddlDataAddOn.Enabled = false;
                    }
                    else
                    {
                        hddnInternational.Value = "0.00";
                        decimal AMT = Convert.ToDecimal(txtAmt.Text);
                        decimal Regulatry = Convert.ToDecimal(txtRegulatry.Text);
                        decimal SET = AMT + Regulatry;
                        txtAmountPay.Text = (SET + Convert.ToDecimal(hddnAddOn.Value)).ToString("0.00");

                        hddnInternationalCreditValue.Value = "0.00";
                        hddnInternationalCreditDiscountedAmount.Value = "0.00";
                        hddnInternationalCreditDiscountPercent.Value = "0.00";
                        //ddlDataAddOn.Enabled = false;
                        //ddlTariff.Enabled = false;

                    }
                    //}
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //protected void btnPortIn_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (btnPortIn.Text == "Port In Active")
        //        {
        //            DivPort.Visible = true;
        //            txtPHONETOPORT.Enabled = true;
        //            txtACCOUNT.Enabled = true;
        //            txtPIN.Enabled = true;
        //            //btnPortIn.Text = "Port In";
        //        }
        //        else if(btnPortIn.Text == "Port In")
        //        {
        //            DivPort.Visible = false;
        //            txtPHONETOPORT.Enabled = false;
        //            txtACCOUNT.Enabled = false;
        //            txtPIN.Enabled = false;
        //            //btnPortIn.Text = "Port In Active";
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}


        protected void btnUpdateMSISDN_Click(object sender, EventArgs e)
        {
            DataSet ds = svc.GetActivationsWithoutMSISDN();
            string MSISDN;

            DataTable dt = ds.Tables[0];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string SIMCARD = Convert.ToString(dt.Rows[i]["SerialNumber"].ToString());
                if (SIMCARD.Length > 0)
                {
                    string transact = "";
                    string X = "";
                    string ChannelID = ConfigurationManager.AppSettings.Get("CHANNEL");

                    DataTable dtTrans = svc.GetTransactionIDService();
                    int id = Convert.ToInt32(dtTrans.Rows[0]["TRANSACTIONID"]);
                    transact = id.ToString("00000");
                    transact = ConfigurationManager.AppSettings.Get("TXN_SERIES") + transact;

                    X = "<ENVELOPE><HEADER><TRANSACTION_ID>" + transact + "</TRANSACTION_ID><ENTITY>" + ChannelID + "</ENTITY><CHANNEL_REFERENCE>" + ChannelID + "</CHANNEL_REFERENCE></HEADER><BODY><CHECK_AUPB_TRANSACTION_STATUS_REQUEST><DETAILS><ICC_ID>" + SIMCARD + "</ICC_ID><TRANSACTION_ID></TRANSACTION_ID></DETAILS></CHECK_AUPB_TRANSACTION_STATUS_REQUEST></BODY></ENVELOPE>";
                    RequestRes = X;
                    Log(X, "Sending Request");
                    string resp = CheckMsisdnWithLyca(X);

                    Log(resp, "Response");
                    Log("", "split");

                    if (resp.Trim() != null && resp.Trim() != "")
                    {
                        StringReader theReader = new StringReader(resp);
                        DataSet theDataSet = new DataSet();
                        theDataSet.ReadXml(theReader);
                        if (theDataSet.Tables.Count > 0)
                        {
                            DataTable dt1 = theDataSet.Tables[0];
                            if (dt1.Rows.Count > 0)
                            {
                                if (dt1.Rows[0]["ERROR_CODE"].ToString() == "0")
                                {
                                    //ShowPopUpMsg("Sim is already activated with Mobile No. " + theDataSet.Tables[2].Rows[0]["ALLOCATED_MSISDN"].ToString());
                                    //btnCompanyByAccount.Enabled = false;
                                    //btnDistributorByAccount.Enabled = false;
                                    //return;
                                    svc.UpdateActivationMSISDN(Convert.ToInt64(dt.Rows[i]["ActivationID"]), theDataSet.Tables[2].Rows[0]["ALLOCATED_MSISDN"].ToString());
                                }

                            }
                        }
                    }


                }

            }
        }

        protected void ddlMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            hddnMonths.Value = Convert.ToString(ddlMonth.SelectedValue);
            ddlTariff_SelectedIndexChanged(null, null);
        }

        //protected void txtZIPCode_TextChanged(object sender, EventArgs e)
        //{
        //    if (Convert.ToInt32(Session["IsH2O"]) == 1)
        //    {
        //        ddlCountry.Items.Clear();
        //        int _AreaCode = 0;
        //        int.TryParse(Convert.ToString(txtZIPCode.Text), out _AreaCode);
        //        //int _AreaCode = Convert.ToInt32(txtZIPCode.Text);
        //        DataSet ds = svc.GetCountryByAreaCode(_AreaCode);
        //        if (ds != null && ds.Tables[0].Rows.Count > 0)
        //        {
        //            ddlCountry.DataSource = ds.Tables[0];
        //            ddlCountry.DataTextField = "CityName";
        //            ddlCountry.DataValueField = "ID";
        //            ddlCountry.DataBind();
        //            ddlCountry.Items.Insert(0, new ListItem("--- Select ---", "0"));
        //        }
        //        else
        //        {
        //            ShowPopUpMsg("Invalid Area code");
        //        }
               
        //    }
        //    else
        //    {

        //    }
        //}

        private string GetDistributorBalance()
        {
            string _pBalance = "0";
            try
            {
                int DistributorID = Convert.ToInt32(Session["DistributorID"]);
                DataSet ds1 = null;
                ds1 = svc.GetCurrentbalance(DistributorID);
                if (ds1 != null)
                {
                    if (ds1.Tables[0].Rows.Count > 0)
                    {
                        _pBalance = Convert.ToString(ds1.Tables[0].Rows[0]["AccountBalance"]);
                    }
                }
                return _pBalance;
            }
            catch (Exception ex)
            {
                return _pBalance;
            }
        }
    }


}
