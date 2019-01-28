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
using ENK.net.emida.ws;
using ENK.ServiceReference1;
using ENK.LycaAPI;


namespace ENK
{
    public partial class ActivationPortin : System.Web.UI.Page
    {
        Service1Client svc = new Service1Client();
        LycaAPI.LycaAPISoapClient la = new LycaAPI.LycaAPISoapClient();
        string RequestRes = "";
        string _Email = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                try
                {
                    ActivationPort1.Visible = false;
                    txtChannelID.Text = ConfigurationManager.AppSettings.Get("TXN_SERIES");
                    //if ((Session["LoginID"] == null) && (Request.QueryString.Count > 0))
                    //{
                    //    int _logid = 0;
                    //    int.TryParse(Convert.ToString(Request.QueryString["lid"]), out _logid);

                    //    Session["LoginID"] = Convert.ToString(_logid);
                    //}
                    ////else
                    ////{
                    ////    Session["LoginID"] = null;
                    ////}

                    if (Session["LoginID"] != null)
                    {

                        //  ENK.net.emida.ws.webServicesService ws = new webServicesService();
                        // string ss1 = ws.Login2("01", "clerkterst", "clerk1234", "1");
                        // string ss1 = ws.Login2("01", "A&HPrepaid", "95222", "1");

                        // add by akash starts
                        if (txtEmailAddress.Text != "")
                        {
                            _Email = txtEmailAddress.Text.Trim();
                        }
                        else
                        {
                            _Email = Convert.ToString(Session["EmailID"]);
                        }
                        // add by akash ends

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


                        }
                    }

                    BtnModify.Enabled = false;
                    BtnCancelportin.Enabled = false;
                    // RdbtnREFNO.Checked = true;

                  //  TxtMSISDN.Enabled = false;
                    // TxtICCID.Enabled = false;
                }
                catch (Exception ex)
                {

                }

            }
        }
        public void CheckAccountBalance(decimal TariffAmount)
        //  public void CheckAccountBalance(double TariffAmount)
        {
            try
            {
                int dist = Convert.ToInt32(Session["DistributorID"]);
                Distributor[] dstbutor = svc.GetSingleDistributorService(dist);
                if (dstbutor != null)
                {
                    if (dstbutor.Length > 0)
                    {
                        //double BalanceAmnt = dstbutor[0].balanceAmount;
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
        //ankit
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
                        // double BalanceAmnt = dstbutor[0].balanceAmount;
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

                BindH20ProductFromTariffTable();
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
                            DataSet dst = svc.GetSingleTariffDetailForActivationService(LoginID, DistributorID, ClientTypeID, tariffID, 1,"Activate"); //to be corrected by Puneet
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

                                    hddnMonths.Value = Convert.ToString(dst.Tables[0].Rows[0]["Months"]);

                                    // 1 $ Add Regulatery 
                                    DateTime today = DateTime.Today;
                                    // DateTime date = new DateTime(2017, 07, 20);
                                    DataSet dsReg = svc.GetRegulatery();
                                    DateTime date = Convert.ToDateTime(dsReg.Tables[0].Rows[0]["StartDate"]);
                                    if (date <= today)
                                    {
                                        amnt = Math.Round(amnt, 2);
                                        txtAmt.Text = Convert.ToString(amnt);
                                        txtRegulatry.Text = "1";

                                        amnt = amnt + 1;
                                        txtAmountPay.Text = Convert.ToString(amnt);
                                        hddnLycaAmount.Value = Convert.ToString(Convert.ToDouble(hddnLycaAmount.Value) + 1);
                                    }
                                    else
                                    {
                                        amnt = Math.Round(amnt, 2);
                                        txtRegulatry.Text = "0";
                                        txtAmt.Text = Convert.ToString(amnt);
                                        txtAmountPay.Text = Convert.ToString(amnt);


                                    }



                                }
                                else
                                {
                                    txtAmountPay.Text = "0.00";
                                    hddnTariffTypeID.Value = "";
                                    hddnTariffType.Value = "";
                                    hddnTariffCode.Value = "";
                                    hddnLycaAmount.Value = "";

                                    hddnMonths.Value = "";
                                }
                            }
                            else
                            {
                                txtAmountPay.Text = "0.00";
                                hddnTariffTypeID.Value = "";
                                hddnTariffType.Value = "";
                                hddnTariffCode.Value = "";
                                hddnLycaAmount.Value = "";

                                hddnMonths.Value = "";
                            }
                        }
                        else
                        {
                            txtAmountPay.Text = "0.00";
                            hddnTariffTypeID.Value = "";
                            hddnTariffType.Value = "";
                            hddnTariffCode.Value = "";
                            hddnLycaAmount.Value = "";

                            hddnMonths.Value = "";
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

                            DataSet dst = svc.GetSingleTariffDetailForActivationService(LoginID, DistributorID, ClientTypeID, tariffID, Convert.ToByte(hddnMonths.Value.ToString()),"Activate");
                            if (dst != null)
                            {
                                if (dst.Tables[0].Rows.Count > 0)
                                {
                                    txtAmountPay.Text = Convert.ToString(dst.Tables[0].Rows[0]["Rental"]);
                                    //ANKIT SINGH
                                    // double amnt = 0.0;
                                    // amnt = Convert.ToDouble(dst.Tables[0].Rows[0]["Rental"]);
                                    decimal amnt = Convert.ToDecimal(0.00);
                                    amnt = Convert.ToDecimal(dst.Tables[0].Rows[0]["Rental"]);
                                    //////////////
                                    CheckAccountBalance(amnt);
                                    hddnTariffTypeID.Value = Convert.ToString(dst.Tables[0].Rows[0]["TariffTypeID"]);
                                    hddnTariffType.Value = Convert.ToString(dst.Tables[0].Rows[0]["TariffType"]);
                                    hddnTariffCode.Value = Convert.ToString(dst.Tables[0].Rows[0]["TariffCode"]);
                                    hddnLycaAmount.Value = Convert.ToString(dst.Tables[0].Rows[0]["LycaAmount"]);

                                    hddnMonths.Value = Convert.ToString(dst.Tables[0].Rows[0]["Months"]);

                                    // 1 $ Add Regulatery 
                                    DateTime today = DateTime.Today;
                                    // DateTime date = new DateTime(2017, 07, 20);
                                    DataSet dsReg = svc.GetRegulatery();
                                    DateTime date = Convert.ToDateTime(dsReg.Tables[0].Rows[0]["StartDate"]);
                                    if (date <= today)
                                    {
                                        amnt = Math.Round(amnt, 2);
                                        txtAmt.Text = Convert.ToString(amnt);
                                        txtRegulatry.Text = "1";
                                        amnt = amnt + 1;

                                        txtAmountPay.Text = Convert.ToString(amnt);
                                        hddnLycaAmount.Value = Convert.ToString(Convert.ToDouble(hddnLycaAmount.Value) + 1);
                                    }
                                    else
                                    {
                                        amnt = Math.Round(amnt, 2);
                                        txtRegulatry.Text = "0";
                                        txtAmt.Text = Convert.ToString(amnt);
                                        txtAmountPay.Text = Convert.ToString(amnt);


                                    }


                                }
                                else
                                {
                                    txtAmountPay.Text = "0.00";
                                    hddnTariffTypeID.Value = "";
                                    hddnTariffType.Value = "";
                                    hddnTariffCode.Value = "";
                                    hddnLycaAmount.Value = "";

                                    hddnMonths.Value = "";
                                }
                            }
                            else
                            {
                                txtAmountPay.Text = "0.00";
                                hddnTariffTypeID.Value = "";
                                hddnTariffType.Value = "";
                                hddnTariffCode.Value = "";
                                hddnLycaAmount.Value = "";

                                hddnMonths.Value = "";
                            }
                        }
                        else
                        {
                            txtAmountPay.Text = "0.00";
                            /////////////////
                            //ANKIT
                            CheckAccountBalance(Convert.ToDecimal(0.00));
                            //  CheckAccountBalance(0.00);
                            ////////////////
                            hddnTariffTypeID.Value = "";
                            hddnTariffType.Value = "";
                            hddnTariffCode.Value = "";
                            hddnLycaAmount.Value = "";

                            hddnMonths.Value = "";
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

        public string Activation(String X)
        {
            try
            {
                String strResponse = String.Empty;
                strResponse = la.LycaAPIRequest(ConfigurationManager.AppSettings.Get("APIURL"), X.Replace("<", "==").Replace(">", "!!"), "GET_PORTIN_DETAILS");
                //strResponse = SendRequest(ConfigurationManager.AppSettings.Get("APIURL"), X);

                return strResponse;

            }
            catch (Exception Ex)
            {
                return "*2*" + Ex.Message + "*";
            }

        }

        public string SendRequest(string strURI, string data, string SOAPAction = "GET_PORTIN_DETAILS")
        {
            string _result;
            _result = " ";
            try
            {
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(strURI);
                req.Headers.Add("SOAPAction", "\"" + SOAPAction + "\"");

                req.ContentType = "text/xml";
                //req.ContentType = "application/x-www-form-urlencoded";
                //req.ContentLength = 359;
                //req.Expect = "100-continue";
                //req.Connection = "Keep-Alive";
                //req.SOAPAction="ACTIVATE_USIM";
                //req.Accept = "text/xml";
                req.Method = "POST";
                //request.ContentType = "application/x-www-form-urlencoded";
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

        public string ActivateSim(string TariffTypeID, string TariffType, string TariffCode, string email, string TransactionID)
        {
            string response = "";
            try
            {

                string SIMCARD = Convert.ToString(txtSIMCARD.Text.Trim());
                string ZIPCode = Convert.ToString(txtZIPCode.Text.Trim());
                string EmailAddress = Convert.ToString(txtEmailAddress.Text.Trim());
                string Language = "ENGLISH";// Convert.ToString(ddlLanguage.SelectedItem.Text);
                string ChannelID = "NKWIRE";// Convert.ToString(txtChannelID.Text.Trim());
                string PHONETOPORT = Convert.ToString(txtPHONETOPORT.Text.Trim());
                string ACCOUNT = Convert.ToString(txtACCOUNT.Text.Trim());
                string PIN = Convert.ToString(txtPIN.Text.Trim());
                string TOPUP_AMOUNT = "";//txtAmountPay.Text.Trim();

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
                Log(response, "Response");
                Log("", "split");
                return response;


            }
            catch (Exception ex)
            {
                return response;
                //ShowPopUpMsg(ex.Message);
            }
        }

        protected void btnCompanyByAccount_Click(object sender, EventArgs e)
        {

            if (Session["LoginID"] != null)
            {            

                int distr = Convert.ToInt32(Session["DistributorID"]);
                int clnt = Convert.ToInt32(Session["ClientTypeID"]);
                string simnumber = txtSIMCARD.Text.Trim();
                string transact = "";
                DataTable dtTrans = svc.GetTransactionIDService();

                int id = Convert.ToInt32(dtTrans.Rows[0]["TRANSACTIONID"]);
                transact = id.ToString("00000");
                transact = ConfigurationManager.AppSettings.Get("TXN_SERIES") + transact;
                Boolean ValidSim = false;
                string erormsg = "";
                try
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


                if (ddlNetwork.SelectedItem.Text == "Lyca Mobile")
                {
                    if (ddlTariff.SelectedIndex == 0)
                    {
                        ShowPopUpMsg("Please select Tariff");
                        return;
                    }
                    // 1 $ Add Regulatery 
                    int Regulatery = 0;
                    DateTime today = DateTime.Today;
                    // DateTime date = new DateTime(2017, 07, 20);
                    DataSet dsReg = svc.GetRegulatery();
                    DateTime date = Convert.ToDateTime(dsReg.Tables[0].Rows[0]["StartDate"]);
                    if (date <= today)
                    {
                        Regulatery = 1;
                    }
                    else { Regulatery = 0; }
                    string resp = ActivateSim(hddnTariffTypeID.Value, hddnTariffType.Value, hddnTariffCode.Value, txtEmailAddress.Text.Trim(), transact);

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
                                       
                                      

                                        SPayment sp = new SPayment();
                                        sp.ChargedAmount = Convert.ToDecimal(txtAmountPay.Text.Trim());
                                        sp.PaymentType = 5;
                                        sp.PayeeID = Convert.ToInt32(Session["DistributorID"]);
                                        sp.PaymentFrom = 9;
                                        sp.ActivationType = 7;
                                        sp.ActivationStatus = 15;
                                        sp.ActivationVia = 17;
                                        sp.ActivationResp = resp;
                                        sp.ActivationRequest = RequestRes;
                                        sp.TariffID = Convert.ToInt32(ddlTariff.SelectedValue);
                                        sp.ALLOCATED_MSISDN = ALLOCATED_MSISDN;
                                        sp.TransactionId = transact;

                                        sp.PaymentMode = "Company PortIn";
                                        sp.TransactionStatusId = 24;
                                        sp.TransactionStatus = "Success";
                                        sp.Regulatery = Regulatery;

                                        int dist = Convert.ToInt32(Session["DistributorID"]);
                                        int loginID = Convert.ToInt32(Session["LoginID"]);
                                        string sim = txtSIMCARD.Text.Trim();
                                        string zip = txtZIPCode.Text.Trim();
                                        string Language = "ENGLISH";
                                        string ChannelID = ConfigurationManager.AppSettings.Get("TXN_SERIES");
                                        try
                                        {
                                            int s = svc.UpdateAccountBalanceService(dist, loginID, sim, zip, Language, ChannelID, sp);

                                            WriteLog(dist, loginID, sim, zip, Language, ChannelID, sp, "Record Save Success when sim PortIn Success In Company Case");
                                            // add by akash starts
                                            if (_Email != "")
                                            {
                                               // SendMail(_Email, "Sim PortIn", ALLOCATED_MSISDN, txtSIMCARD.Text.Trim(), "");
                                                if (ddlTariff.SelectedItem.Text == "PAYG")
                                                {
                                                }
                                                else
                                                {
                                                    SendMailWithDetail(_Email, "Sim PortIn", ALLOCATED_MSISDN.Trim(), txtSIMCARD.Text.Trim(), transact);
                                                }
                                            }
                                            // add by akash ends

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


                else if (ddlNetwork.SelectedItem.Text == "H20" || ddlNetwork.SelectedItem.Text == "EasyGo")
                {

                    ENK.net.emida.ws.webServicesService ws = new webServicesService();

                    try
                    {
                        string ss1 = ws.Login2("01", "A&HPrepaid", "95222", "1");

                        // ws.Logout("01", "A&HPrepaid", "95222", "1");
                    }
                    catch (Exception ex)
                    {
                        ShowPopUpMsg(ex.Message);
                        return;

                    }


                    if (ddlProduct.SelectedIndex == 0)
                    {
                        ShowPopUpMsg("Please select Tariff");
                        return;
                    }
                    if (txtServiceProvider.Text == "")
                    {
                        ShowPopUpMsg("Please Enter Service Provider");
                        txtServiceProvider.Focus();
                        return;

                    }
                    if (txtState.Text == "")
                    {
                        ShowPopUpMsg("Please Enter State");
                        txtState.Focus();
                        return;

                    }
                    if (txtCity.Text == "")
                    {
                        ShowPopUpMsg("Please Enter City");
                        txtCity.Focus();
                        return;

                    }
                    if (txtLastname.Text == "")
                    {
                        ShowPopUpMsg("Please Enter 1st and Last Name");
                        txtLastname.Focus();
                        return;

                    }
                    if (txtaddress.Text == "")
                    {
                        ShowPopUpMsg("Please Enter Address");
                        txtaddress.Focus();
                        return;

                    }
                    string InvoiceNo = DateTime.Now.ToString().GetHashCode().ToString("X");


                    // ENK.net.emida.ws.webServicesService ws = new webServicesService();
                    // string ss1 = ws.LycaPortinPin("01", "3756263", "1234", hddnTariffCode.Value, txtSIMCARD.Text, txtPHONETOPORT.Text, txtACCOUNT.Text, txtPIN.Text, txtZIPCode.Text, "1", txtEmailAddress.Text, "$10", txtAmountPay.Text, InvoiceNo, "1");


                    try
                    {

                        // SendMail(txtEmailAddress.Text.Trim(), "Sim PortIn", "", txtSIMCARD.Text.Trim(), "");
                        SendMailH20AndEasyGo(txtEmailAddress.Text.Trim(), "Sim PortIn", "", txtSIMCARD.Text.Trim(), "");


                        SPayment sp = new SPayment();
                        sp.ChargedAmount = Convert.ToDecimal(txtAmountPay.Text.Trim());
                        sp.PaymentType = 5;
                        sp.PayeeID = Convert.ToInt32(Session["DistributorID"]);
                        sp.PaymentFrom = 9;
                        sp.ActivationType = 7;
                        sp.ActivationStatus = 15;
                        sp.ActivationVia = 17;
                        sp.ActivationResp = "";
                        sp.ActivationRequest = RequestRes;
                        sp.TariffID = Convert.ToInt32(ddlProduct.SelectedValue);
                        sp.ALLOCATED_MSISDN = "";
                        sp.TransactionId = transact;

                        sp.PaymentMode = "Company PortIn";
                        sp.TransactionStatusId = 24;
                        sp.TransactionStatus = "Success";

                        int dist = Convert.ToInt32(Session["DistributorID"]);
                        int loginID = Convert.ToInt32(Session["LoginID"]);
                        string sim = txtSIMCARD.Text.Trim();
                        string zip = txtZIPCode.Text.Trim();
                        string Language = "ENGLISH";
                        string ChannelID = ConfigurationManager.AppSettings.Get("TXN_SERIES");
                        try
                        {
                            int s = svc.UpdateAccountBalanceService(dist, loginID, sim, zip, Language, ChannelID, sp);

                            WriteLog(dist, loginID, sim, zip, Language, ChannelID, sp, "Record Save Success when sim PortIn Success In Company Case");
                        }
                        catch (Exception ex)
                        {
                            WriteLog(dist, loginID, sim, zip, Language, ChannelID, sp, "Record Save Fail when sim PortIn Success  In Company Case");

                        }
                        ShowPopUpMsg("PORTIN Request submitted Successfully\n With Sim Number - " + txtSIMCARD.Text);
                        resetControls(1);


                    }
                    catch (Exception ex)
                    {
                        SaveDataCompany(txtSIMCARD.Text, 16, transact);
                        ShowPopUpMsg("PORTIN Request Fail \n Please Try Again");
                        //ShowPopUpMsg(ex.Message + "\n" + resp);
                    }


                }





            }
        }

        public void SaveDataCompany(string resp, int status, string TransactionID)
        {



            SPayment sp = new SPayment();
            sp.ChargedAmount = Convert.ToDecimal(txtAmountPay.Text.Trim());
            sp.PaymentType = 5;
            sp.PayeeID = Convert.ToInt32(Session["DistributorID"]);
            sp.PaymentFrom = 9;
            sp.ActivationType = 7;
            sp.ActivationStatus = status;
            sp.ActivationVia = 17;
            sp.ActivationResp = resp;
            sp.ActivationRequest = RequestRes;
            sp.TariffID = 0;
            sp.ALLOCATED_MSISDN = "";
            sp.TransactionId = TransactionID;

            sp.PaymentMode = "Company PortIn";
            sp.TransactionStatusId = 25;
            sp.TransactionStatus = "Fail";

            int dist = Convert.ToInt32(Session["DistributorID"]);
            int loginID = Convert.ToInt32(Session["LoginID"]);
            string sim = txtSIMCARD.Text.Trim();
            string zip = txtZIPCode.Text.Trim();
            string Language = "ENGLISH";
            string ChannelID = ConfigurationManager.AppSettings.Get("TXN_SERIES");

            try
            {
                int s = svc.UpdateAccountBalanceService(dist, loginID, sim, zip, Language, ChannelID, sp);
                WriteLog(dist, loginID, sim, zip, Language, ChannelID, sp, "Record Save Success when sim PortIn Not Success In Company Case");
            }
            catch (Exception ex)
            {
                WriteLog(dist, loginID, sim, zip, Language, ChannelID, sp, "Record Save Fail when sim PortIn Not Success In Company Case");
            }
            //ShowPopUpMsg("PORTIN Request Fail");
            resetControls(1);
        }

        protected void btnDistributorByAccount_Click(object sender, EventArgs e)
        {
            if (Session["LoginID"] != null)
            {
                int distr = Convert.ToInt32(Session["DistributorID"]);
                int clnt = Convert.ToInt32(Session["ClientTypeID"]);
                string simnumber = txtSIMCARD.Text.Trim();
                string transact = "";
                //ankit
                Boolean IsBalance = CheckAccount(Convert.ToDecimal(txtAmountPay.Text.Trim()));
                // Boolean IsBalance = CheckAccount(Convert.ToDouble(txtAmountPay.Text.Trim()));

                if (IsBalance == false)
                {
                    ShowPopUpMsg("Your Account Balance is Low \n Please Recharge Your Balance");
                    return;
                }
                DataTable dtTrans = svc.GetTransactionIDService();

                int id = Convert.ToInt32(dtTrans.Rows[0]["TRANSACTIONID"]);
                transact = id.ToString("00000");
                transact = ConfigurationManager.AppSettings.Get("TXN_SERIES") + transact;
                Boolean ValidSim = false;
                string erormsg = "";
                try
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



                if (ddlNetwork.SelectedItem.Text == "Lyca Mobile")
                {

                    if (ddlTariff.SelectedIndex == 0)
                    {
                        ShowPopUpMsg("Please select Tariff");
                        return;
                    }
                    // 1 $ Add Regulatery 
                    int Regulatery = 0;
                    DateTime today = DateTime.Today;
                    // DateTime date = new DateTime(2017, 07, 20);
                    DataSet dsReg = svc.GetRegulatery();
                    DateTime date = Convert.ToDateTime(dsReg.Tables[0].Rows[0]["StartDate"]);
                    if (date <= today)
                    {
                        Regulatery = 1;
                    }
                    else { Regulatery = 0; }
                    string resp = ActivateSim(hddnTariffTypeID.Value, hddnTariffType.Value, hddnTariffCode.Value, txtEmailAddress.Text.Trim(), transact);


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
                                      // SendMail(txtEmailAddress.Text.Trim(), "Sim PortIn", ALLOCATED_MSISDN, txtSIMCARD.Text.Trim(), "");
                                      

                                        SPayment sp = new SPayment();
                                        sp.ChargedAmount = Convert.ToDecimal(txtAmountPay.Text.Trim());
                                        sp.PaymentType = 5;
                                        sp.PayeeID = Convert.ToInt32(Session["DistributorID"]);
                                        sp.PaymentFrom = 9;
                                        sp.ActivationType = 7;
                                        sp.ActivationStatus = 15;
                                        sp.ActivationVia = 17;
                                        sp.ActivationResp = resp;
                                        sp.ActivationRequest = RequestRes;
                                        sp.TariffID = Convert.ToInt32(ddlTariff.SelectedValue);
                                        sp.ALLOCATED_MSISDN = ALLOCATED_MSISDN;
                                        sp.TransactionId = transact;

                                        sp.PaymentMode = "Distributor PortIn";
                                        sp.TransactionStatusId = 24;
                                        sp.TransactionStatus = "Success";
                                        sp.Regulatery = Regulatery;

                                        int dist = Convert.ToInt32(Session["DistributorID"]);
                                        int loginID = Convert.ToInt32(Session["LoginID"]);
                                        string sim = txtSIMCARD.Text.Trim();
                                        string zip = txtZIPCode.Text.Trim();
                                        string Language = "ENGLISH";
                                        string ChannelID = ConfigurationManager.AppSettings.Get("TXN_SERIES");
                                        try
                                        {
                                            int s = svc.UpdateAccountBalanceService(dist, loginID, sim, zip, Language, ChannelID, sp);

                                            WriteLog(dist, loginID, sim, zip, Language, ChannelID, sp, "Record Save Success when sim PortIn Success In Distributor Case");
                                            // add by akash starts
                                            
                                            if (_Email != "")
                                            {
                                              //  SendMail(_Email, "Sim PortIn", ALLOCATED_MSISDN, txtSIMCARD.Text.Trim(), "");
                                                if (ddlTariff.SelectedItem.Text == "PAYG")
                                                {
                                                }
                                                else
                                                {
                                                    SendMailWithDetail(_Email, "Sim PortIn", ALLOCATED_MSISDN.Trim(), txtSIMCARD.Text.Trim(), transact);
                                                }
                                            }
                                           
                                            // add by akash ends
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
                                        SaveData(resp, 16, transact);
                                        ShowPopUpMsg("PORTIN Request Fail \n Please Try Again");
                                    }
                                }
                                else
                                {
                                    SaveData(resp, 16, transact);
                                    ShowPopUpMsg("PORTIN Request Fail \n Please Try Again");
                                }
                            }
                            else
                            {
                                SaveData(resp, 16, transact);

                                ShowPopUpMsg("PORTIN Request Fail \n Please Try Again");
                            }
                        }
                        catch (Exception ex)
                        {
                            SaveData(resp, 16, transact);
                            ShowPopUpMsg("PORTIN Request Fail \n Please Try Again");
                            //ShowPopUpMsg(ex.Message + "\n" + resp);
                        }
                    }
                    else
                    {
                        SaveData(resp, 16, transact);
                        ShowPopUpMsg("PORTIN Request Fail \n Please Try Again");
                    }
                }
                else if (ddlNetwork.SelectedItem.Text == "H20" || ddlNetwork.SelectedItem.Text == "EasyGo")
                {
                    ENK.net.emida.ws.webServicesService ws = new webServicesService();

                    try
                    {
                        string ss1 = ws.Login2("01", "A&HPrepaid", "95222", "1");

                        // ws.Logout("01", "A&HPrepaid", "95222", "1");
                    }
                    catch (Exception ex)
                    {
                        ShowPopUpMsg(ex.Message);
                        return;

                    }



                    if (ddlProduct.SelectedIndex == 0)
                    {
                        ShowPopUpMsg("Please select Tariff");
                        return;
                    }

                    if (txtServiceProvider.Text == "")
                    {
                        ShowPopUpMsg("Please Enter Service Provider");
                        txtServiceProvider.Focus();
                        return;

                    }
                    if (txtState.Text == "")
                    {
                        ShowPopUpMsg("Please Enter State");
                        txtState.Focus();
                        return;

                    }
                    if (txtCity.Text == "")
                    {
                        ShowPopUpMsg("Please Enter City");
                        txtCity.Focus();
                        return;

                    }
                    if (txtLastname.Text == "")
                    {
                        ShowPopUpMsg("Please Enter 1st and Last Name");
                        txtLastname.Focus();
                        return;

                    }
                    if (txtaddress.Text == "")
                    {
                        ShowPopUpMsg("Please Enter Address");
                        txtaddress.Focus();
                        return;

                    }
                    string InvoiceNo = DateTime.Now.ToString().GetHashCode().ToString("X");


                    // ENK.net.emida.ws.webServicesService ws = new webServicesService();
                    // string ss1 = ws.LycaPortinPin("01", "3756263", "1234", hddnTariffCode.Value, txtSIMCARD.Text, txtPHONETOPORT.Text, txtACCOUNT.Text, txtPIN.Text, txtZIPCode.Text, "1", txtEmailAddress.Text, "$10", txtAmountPay.Text, InvoiceNo, "1");


                    try
                    {

                        // SendMail(txtEmailAddress.Text.Trim(), "Sim PortIn", "", txtSIMCARD.Text.Trim(), "");
                        SendMailH20AndEasyGo(txtEmailAddress.Text.Trim(), "SIM PortIn", "", txtSIMCARD.Text.Trim(), "");


                        SPayment sp = new SPayment();
                        sp.ChargedAmount = Convert.ToDecimal(txtAmountPay.Text.Trim());
                        sp.PaymentType = 5;
                        sp.PayeeID = Convert.ToInt32(Session["DistributorID"]);
                        sp.PaymentFrom = 9;
                        sp.ActivationType = 7;
                        sp.ActivationStatus = 15;
                        sp.ActivationVia = 17;
                        sp.ActivationResp = "";
                        sp.ActivationRequest = RequestRes;
                        sp.TariffID = Convert.ToInt32(ddlProduct.SelectedValue);
                        sp.ALLOCATED_MSISDN = "";
                        sp.TransactionId = transact;

                        sp.PaymentMode = "Company PortIn";
                        sp.TransactionStatusId = 24;
                        sp.TransactionStatus = "Success";

                        int dist = Convert.ToInt32(Session["DistributorID"]);
                        int loginID = Convert.ToInt32(Session["LoginID"]);
                        string sim = txtSIMCARD.Text.Trim();
                        string zip = txtZIPCode.Text.Trim();
                        string Language = "ENGLISH";
                        string ChannelID = ConfigurationManager.AppSettings.Get("TXN_SERIES");
                        try
                        {
                            int s = svc.UpdateAccountBalanceService(dist, loginID, sim, zip, Language, ChannelID, sp);

                            WriteLog(dist, loginID, sim, zip, Language, ChannelID, sp, "Record Save Success when sim PortIn Success In Company Case");
                        }
                        catch (Exception ex)
                        {
                            WriteLog(dist, loginID, sim, zip, Language, ChannelID, sp, "Record Save Fail when sim PortIn Success  In Company Case");

                        }
                        ShowPopUpMsg("PORTIN Request submitted successfully\n with Sim Number - " + txtSIMCARD.Text);
                        resetControls(1);


                    }
                    catch (Exception ex)
                    {
                        SaveDataCompany(txtSIMCARD.Text, 16, transact);
                        ShowPopUpMsg("PORTIN Request Fail \n Please Try Again");
                        //ShowPopUpMsg(ex.Message + "\n" + resp);
                    }


                }



            }
        }

        public void SaveData(string resp, int status, string TransactionID)
        {
            SPayment sp = new SPayment();
            sp.ChargedAmount = Convert.ToDecimal(txtAmountPay.Text.Trim());
            sp.PaymentType = 5;
            sp.PayeeID = Convert.ToInt32(Session["DistributorID"]);
            sp.PaymentFrom = 9;
            sp.ActivationType = 7;
            sp.ActivationStatus = status;
            sp.ActivationVia = 17;
            sp.ActivationResp = resp;
            sp.ActivationRequest = RequestRes;
            sp.TariffID = 0;
            sp.ALLOCATED_MSISDN = "";
            sp.TransactionId = TransactionID;

            sp.PaymentMode = "Distributor PortIn";
            sp.TransactionStatusId = 25;
            sp.TransactionStatus = "Fail";

            int dist = Convert.ToInt32(Session["DistributorID"]);
            int loginID = Convert.ToInt32(Session["LoginID"]);
            string sim = txtSIMCARD.Text.Trim();
            string zip = txtZIPCode.Text.Trim();
            string Language = "ENGLISH";
            string ChannelID = ConfigurationManager.AppSettings.Get("TXN_SERIES");

            try
            {
                int s = svc.UpdateAccountBalanceService(dist, loginID, sim, zip, Language, ChannelID, sp);
                WriteLog(dist, loginID, sim, zip, Language, ChannelID, sp, "Record Save success when sim PortIn Not Success In Distributor Case");
            }
            catch (Exception ex)
            {
                WriteLog(dist, loginID, sim, zip, Language, ChannelID, sp, "Record Save Fail when sim PortIn Not Success In Distributor Case");
            }
            // ShowPopUpMsg("PORTIN Request Fail");
            resetControls(1);
        }

        public void WriteLog(int dist, int loginID, string sim, string zip, string Language, string ChannelID, SPayment sp, string msgg)
        {
            try
            {
                StringBuilder logdata = new StringBuilder();
                logdata.Append(dist + "|");
                logdata.Append(loginID + "|");
                logdata.Append(sim + "|");
                logdata.Append(zip + "|");
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
            catch (Exception)
            {


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


                txtPHONETOPORT.Text = string.Empty;
                txtACCOUNT.Text = string.Empty;
                txtPIN.Text = string.Empty;
                //txtNATIONAL_BUNDLE_CODE.Text = string.Empty;
                //txtNATIONAL_BUNDLE_AMOUNT.Text = string.Empty;
                //txtINTERNATIONAL_BUNDLE_CODE.Text = string.Empty;
                //txtINTERNATIONAL_BUNDLE_AMOUNT.Text = string.Empty;
                //txtTOPUP_CARD_ID.Text = string.Empty;
                //txtTOPUP_AMOUNT.Text = string.Empty;
                //txtVOUCHER_PIN.Text = string.Empty;
                txtEmailAddress.Text = string.Empty;

                txtServiceProvider.Text = string.Empty;
                txtCity.Text = string.Empty;
                txtState.Text = string.Empty;
                txtLastname.Text = string.Empty;

                txtaddress.Text = string.Empty;

                txtAmountPay.Text = "";
                ddlNetwork.SelectedIndex = 0;
                ddlProduct.SelectedIndex = 0;
                ddlTariff.SelectedIndex = 0;
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

        public void Log(string ss, string condition)
        {
            try
            {
                string filename = "PortinLog.txt";
                string strPath = Server.MapPath("Log") + "/" + filename;

                string root = "https://" + HttpContext.Current.Request.Url.Host + HttpContext.Current.Request.ApplicationPath + "/Log/" + filename;

                if (File.Exists(strPath))
                {
                    StreamWriter sw = new StreamWriter(strPath, true, Encoding.Unicode);
                    if (condition != "split")
                    {
                        sw.WriteLine(condition + "  " + DateTime.Now.ToString());
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
            catch (Exception)
            {


            }
        }

        public void Log1(string ss, string condition)
        {
            try
            {
                string filename = "PortinLog.txt";
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

                sb.Append("<p>Sim Number  " + UserID + "  Sim PortIn successfully on Mobile Number  " + UserName + "<p>");
                sb.Append("<br/>");
                sb.Append("<p> PORTIN Request submitted successfully, If you have any issue  with PORTIN please contact Lycamobile directly at +1-845-301-1633 / +1-866-277-0024 <p>");
                sb.Append("<p>");
                sb.Append("<br/>");


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
        public void SendMailWithDetail(string SendTo, string Subject, string UserName, string UserID, string transaction)
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
                string _CurrentBalance = "";
                string _PAccountBal = Convert.ToString(Session["AccountBalance"]);

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
                            _CurrentBalance = Convert.ToString(Convert.ToDecimal(_PAccountBal) - Convert.ToDecimal(dtblprintDetails.Rows[0][9]));
                            Session["AccountBalance"] = _CurrentBalance;
                           // _SalesAmount = Convert.ToString(Convert.ToDecimal(dtblprintDetails.Rows[0][5]) + Convert.ToDecimal(dtblprintDetails.Rows[0][4]));
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
                sb.Append("<p>Previous Balance : $" + _PAccountBal + " <p>");
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

        protected void imgByPaypal_Click(object sender, ImageClickEventArgs e)
        {
            if (Session["LoginID"] != null)
            {
                int distr = Convert.ToInt32(Session["DistributorID"]);
                int clnt = Convert.ToInt32(Session["ClientTypeID"]);
                string simnumber = txtSIMCARD.Text.Trim();
                string transact = "";

                DataTable dtTrans = svc.GetTransactionIDService();

                int id = Convert.ToInt32(dtTrans.Rows[0]["TRANSACTIONID"]);
                transact = id.ToString("00000");
                transact = ConfigurationManager.AppSettings.Get("TXN_SERIES") + transact;
                Boolean ValidSim = false;
                string erormsg = "";
                try
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


                string iteminfo = "Portin";
                if (amnt.Trim() != "")
                {
                    if (Convert.ToDouble(amnt) > 0)
                    {

                        Session["Amount"] = txtAmountPay.Text.Trim();
                        int a = InsertPayment("Pending");

                        if (a > 0)
                        {
                            Session["PaymentId"] = a;
                            string request = ActivateSimString(hddnTariffTypeID.Value, hddnTariffType.Value, hddnTariffCode.Value, txtEmailAddress.Text.Trim(), transact);
                            Session["RequestString"] = request;
                            PayWithPayPal(amnt, iteminfo);
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
                string ss = TariffTypeID + "," + TariffType + "," + TariffCode + "," + email + "," + TransactionID + "," + txtSIMCARD.Text.Trim() + "," + txtZIPCode.Text.Trim() + "," + txtAmountPay.Text.Trim() + "," + ddlTariff.SelectedValue + "," + txtPHONETOPORT.Text.Trim() + "," + txtACCOUNT.Text.Trim() + "," + txtPIN.Text.Trim();
                Session["RequestData"] = ss;

                string SIMCARD = Convert.ToString(txtSIMCARD.Text.Trim());
                string ZIPCode = Convert.ToString(txtZIPCode.Text.Trim());
                string EmailAddress = Convert.ToString(txtEmailAddress.Text.Trim());
                string Language = "ENGLISH";// Convert.ToString(ddlLanguage.SelectedItem.Text);
                string ChannelID = ConfigurationManager.AppSettings.Get("TXN_SERIES");// Convert.ToString(txtChannelID.Text.Trim());
                string PHONETOPORT = Convert.ToString(txtPHONETOPORT.Text.Trim());
                string ACCOUNT = Convert.ToString(txtACCOUNT.Text.Trim());
                string PIN = Convert.ToString(txtPIN.Text.Trim());
                string TOPUP_AMOUNT = "";//txtAmountPay.Text.Trim();

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


                string X = "<ENVELOPE><HEADER><TRANSACTION_ID>" + TransactionID + "</TRANSACTION_ID><ENTITY>" + ChannelID + "</ENTITY><CHANNEL_REFERENCE>" + ChannelID + "</CHANNEL_REFERENCE></HEADER><BODY><ACTIVATE_USIM_PORTIN_BUNDLE_REQUEST><DETAILS><ICC_ID>" + SIMCARD + "</ICC_ID><ZIP_CODE>" + ZIPCode + "</ZIP_CODE><PREFERRED_LANGUAGE>" + Language + "</PREFERRED_LANGUAGE><P_MSISDN>" + PHONETOPORT + "</P_MSISDN><ACCOUNT_NUMBER>" + ACCOUNT + "</ACCOUNT_NUMBER><PASSWORD_PIN>" + PIN + "</PASSWORD_PIN><NO_OF_MONTHS>" + hddnMonths.Value + "</NO_OF_MONTHS><NATIONAL_BUNDLE_CODE>" + NATIONAL_BUNDLE_CODE + "</NATIONAL_BUNDLE_CODE><NATIONAL_BUNDLE_AMOUNT>" + NATIONAL_BUNDLE_AMOUNT + "</NATIONAL_BUNDLE_AMOUNT><INTERNATIONAL_BUNDLE_CODE></INTERNATIONAL_BUNDLE_CODE><INTERNATIONAL_BUNDLE_AMOUNT></INTERNATIONAL_BUNDLE_AMOUNT><TOPUP_AMOUNT>" + TOPUP_AMOUNT + "</TOPUP_AMOUNT><TOPUP_CARD_ID>" + TOPUP_CARD_ID + "</TOPUP_CARD_ID><VOUCHER_PIN>" + VOUCHER_PIN + "</VOUCHER_PIN><CHANNEL_ID>" + ChannelID + "</CHANNEL_ID><EMAIL_ID>" + EmailAddress + "</EMAIL_ID></DETAILS></ACTIVATE_USIM_PORTIN_BUNDLE_REQUEST></BODY></ENVELOPE>";

                //string X = "<ENVELOPE><HEADER><TRANSACTION_ID>" + TransactionID + "</TRANSACTION_ID><ENTITY>ENK</ENTITY><CHANNEL_REFERENCE>ENK</CHANNEL_REFERENCE></HEADER><BODY><ACTIVATE_USIM_PORTIN_BUNDLE_REQUEST><DETAILS><ICC_ID>" + SIMCARD + "</ICC_ID><ZIP_CODE>" + ZIPCode + "</ZIP_CODE><PREFERRED_LANGUAGE>" + Language + "</PREFERRED_LANGUAGE><P_MSISDN>" + PHONETOPORT + "</P_MSISDN><ACCOUNT_NUMBER>" + ACCOUNT + "</ACCOUNT_NUMBER><PASSWORD_PIN>" + PIN + "</PASSWORD_PIN><NATIONAL_BUNDLE_CODE></NATIONAL_BUNDLE_CODE><NATIONAL_BUNDLE_AMOUNT></NATIONAL_BUNDLE_AMOUNT><INTERNATIONAL_BUNDLE_CODE>" + INTERNATIONAL_BUNDLE_CODE + "</INTERNATIONAL_BUNDLE_CODE><INTERNATIONAL_BUNDLE_AMOUNT>" + INTERNATIONAL_BUNDLE_AMOUNT + "</INTERNATIONAL_BUNDLE_AMOUNT><TOPUP_AMOUNT>" + TOPUP_AMOUNT + "</TOPUP_AMOUNT><TOPUP_CARD_ID>" + TOPUP_CARD_ID + "</TOPUP_CARD_ID><VOUCHER_PIN>1565403680666</VOUCHER_PIN><NO_OF_MONTHS>" + hddnMonths.Value + "</NO_OF_MONTHS><CHANNEL_ID>" + ChannelID + "</CHANNEL_ID><EMAIL_ID>" + EmailAddress + "</EMAIL_ID></DETAILS></ACTIVATE_USIM_PORTIN_BUNDLE_REQUEST></BODY></ENVELOPE>";
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
                    sp.PaymentType = 5;
                    sp.PayeeID = Convert.ToInt32(Session["DistributorID"]);
                    sp.PaymentFrom = 9;
                    sp.ActivationVia = 18;
                    sp.TransactionStatusId = 23;
                    sp.TransactionStatus = ststus;
                    sp.PaymentMode = "PayPal Portin";
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
            redirecturl += "&return=" + ConfigurationManager.AppSettings["PortinSuccessURL"].ToString();
            redirecturl += "&cancel_return=" + ConfigurationManager.AppSettings["PortinFailedURL"].ToString();
            Response.Redirect(redirecturl);
        }

        protected void ddlNetwork_SelectedIndexChanged(object sender, EventArgs e)
        {

            try
            {
                DataTable dtSIMPurchase = new DataTable();
                txtRegulatry.Text = "0";
                txtAmt.Text = "0";

                if (ddlNetwork.SelectedItem.Text == "H20")
                {

                    divH20.Attributes.Add("style", "display:block");
                    divProduct.Attributes.Add("style", "display:block");
                    divPortIn.Attributes.Add("style", "display:none");
                    ddlTariff.SelectedIndex = -1;
                    ddlProduct.SelectedIndex = -1;


                    //BindH2OProduct();
                    BindH20ProductFromTariffTable();

                }

                else if (ddlNetwork.SelectedItem.Text == "EasyGo")
                {
                    // divCity.Visible = true;

                    divProduct.Attributes.Add("style", "display:block");
                    divH20.Attributes.Add("style", "display:block");
                    divPortIn.Attributes.Add("style", "display:none");
                    ddlTariff.SelectedIndex = -1;
                    ddlProduct.SelectedIndex = -1;



                    // BindEASRGOProduct();
                    BindEASRGOProductFromTariffTable();

                }
                else if (ddlNetwork.SelectedItem.Text == "Ultra Mobile")
                {
                    divProduct.Attributes.Add("style", "display:block");
                    divH20.Attributes.Add("style", "display:block");
                    divPortIn.Attributes.Add("style", "display:none");
                    ddlTariff.SelectedIndex = -1;
                    ddlProduct.SelectedIndex = -1;



                    ddlProduct.Items.Clear();

                    ddlProduct.Items.Insert(0, new ListItem("---Select---", "0"));
                    //  BindEASRGOProductFromTariffTable();
                    txtAmountPay.Text = "0";

                }


                else
                {
                    // divCity.Visible = false;

                    divProduct.Attributes.Add("style", "display:none");
                    divH20.Attributes.Add("style", "display:None");
                    divPortIn.Attributes.Add("style", "display:block");
                    ddlTariff.SelectedIndex = -1;
                    ddlProduct.SelectedIndex = -1;
                }


            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void BindH20ProductFromTariffTable()
        {
            try
            {
                DataTable dtH20 = new DataTable();
                dtH20 = (DataTable)ViewState["H20Product"];
                if (dtH20.Rows.Count > 0)
                {

                    ddlProduct.DataSource = dtH20;
                    ddlProduct.DataValueField = "Id";
                    ddlProduct.DataTextField = "TariffCode";
                    ddlProduct.DataBind();
                    ddlProduct.Items.Insert(0, new ListItem("---Select---", "0"));

                }
                else
                {
                    ddlProduct.Items.Clear();
                    ddlProduct.Items.Insert(0, new ListItem("---Select---", "0"));

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }


        private void BindEASRGOProductFromTariffTable()
        {
            try
            {
                DataTable dtEasyGo = new DataTable();
                dtEasyGo = (DataTable)ViewState["EasyGoProduct"];
                if (dtEasyGo.Rows.Count > 0)
                {

                    ddlProduct.DataSource = dtEasyGo;
                    ddlProduct.DataValueField = "Id";
                    ddlProduct.DataTextField = "TariffCode";
                    ddlProduct.DataBind();
                    ddlProduct.Items.Insert(0, new ListItem("---Select---", "0"));

                }
                else
                {
                    ddlProduct.Items.Clear();
                    ddlProduct.Items.Insert(0, new ListItem("---Select---", "0"));

                }
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
                DataSet dst = svc.GetSingleTariffDetailForActivationService(LoginID, DistributorID, ClientTypeID, tariffID, 1,"Activate"); //to be corrected by Puneet

                DataTable dt = new DataTable();
                dt = dst.Tables[1];
                string Amount = "0";
                if (dt.Rows.Count > 0)
                {
                    Amount = dt.Rows[0]["Rental"].ToString();
                    hddnTariffCode.Value = dt.Rows[0]["TariffCode"].ToString();
                    txtAmt.Text = Convert.ToString(Amount);
                }
                ViewState["CurrencySymbol"] = "$";

                if (Amount != "")
                {
                    txtAmountPay.Text = Amount;

                }
                else
                {
                    txtAmountPay.Text = "0";
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
                DataSet dst = svc.GetSingleTariffDetailForActivationService(LoginID, DistributorID, ClientTypeID, tariffID, 1,"Activate"); //to be corrected by Puneet

                DataTable dt = new DataTable();
                dt = dst.Tables[2];
                string Amount = "0";
                if (dt.Rows.Count > 0)
                {
                    Amount = dt.Rows[0]["Rental"].ToString();
                    hddnTariffCode.Value = dt.Rows[0]["TariffCode"].ToString();
                    txtAmt.Text = Convert.ToString(Amount);
                }

                ViewState["CurrencySymbol"] = "$";


                if (Amount != "")
                {
                    txtAmountPay.Text = Amount;
                }
                else
                {
                    txtAmountPay.Text = "0";
                }

            }

        }

        public void SendMailH20AndEasyGo(string SendTo, string Subject, string UserName, string UserID, string pass)
        {
            try
            {
                string LoginUrl = "https://" + HttpContext.Current.Request.Url.Host + HttpContext.Current.Request.ApplicationPath + "/Login.aspx";
                string LogoUrl = "https://" + HttpContext.Current.Request.Url.Host + HttpContext.Current.Request.ApplicationPath + "/img/logo.png";
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient();


                string MailAddress = ConfigurationManager.AppSettings.Get("Fromail");
                string PassWord = ConfigurationManager.AppSettings.Get("Password");
                string ToMailID = ConfigurationManager.AppSettings.Get("ToMailID");

                if (ToMailID != "")
                {
                    SendTo = SendTo + "," + ToMailID;
                }
                mail.From = new MailAddress(MailAddress);
                mail.To.Add(SendTo);
                TimeSpan ts = new TimeSpan(8, 0, 0);
                mail.Subject = "SiteID-3756263  " + Subject + " " + DateTime.UtcNow.Subtract(ts).ToString();


                string sCcEmail = ConfigurationManager.AppSettings.Get("CCMailID");
                if (sCcEmail != "")
                {
                    try
                    {
                        if (sCcEmail.Trim() != string.Empty)
                        {
                            List<string> cclist = sCcEmail.Split(';').ToList();
                            if (cclist != null && cclist.Count > 0)
                            {
                                foreach (string ccto in cclist)
                                {
                                    mail.CC.Add(new MailAddress(ccto));
                                }
                            }
                        }
                    }
                    catch { }

                }



                StringBuilder sb = new StringBuilder();
                sb.Append("<html>");
                sb.Append("<body style=”color:grey; font-size:15px;”>");
                sb.Append("<font face=”Helvetica, Arial, sans-serif”>");

                sb.Append("<div style=”position:absolute; height:200px; width:100px; background-color:0d1d36; padding:30px;”>");
                sb.Append("<img src=" + LogoUrl + " />");
                sb.Append("</div>");

                sb.Append("<br/>");

                sb.Append("<br/>");

                sb.Append("<div style=” border:1px solid black; background-color: #ece8d4; width:600px; height:200px; padding:30px; margin-top:30px;”>");
                //sb.Append("<p>Please find the new credentials and get started.</p>");
                sb.Append("<div style='border:1px solid black;padding-left: 24px;width: 388px; BORDER-RADIUS: 25px;'>");
                sb.Append("<p><strong> Sim Number </strong> : " + UserID + "<p>");
                sb.Append("<p><strong> Network </strong> : " + ddlNetwork.SelectedItem.Text + "<p>");
                sb.Append("<p><strong> Tariff/Plan </strong> : " + ddlProduct.SelectedItem.Text + "<p>");
                sb.Append("<p><strong> Amount</strong> : " + txtAmountPay.Text + "<p>");
                sb.Append("<p><strong> Zip Code</strong> : " + txtZIPCode.Text + "<p>");
                sb.Append("<p><strong> Phone Number</strong> : " + txtPHONETOPORT.Text + "<p>");
                sb.Append("<p><strong> Account Number</strong> : " + txtACCOUNT.Text + "<p>");
                sb.Append("<p><strong> Pin </strong>:" + txtPIN.Text + "<p>");
                sb.Append("<p><strong> Email Address</strong> : " + txtEmailAddress.Text + "<p>");

                sb.Append("<p><strong> Current Service Provider </strong> : " + txtServiceProvider.Text + "<p>");
                sb.Append("<p><strong>State</strong> : " + txtState.Text + "<p>");
                sb.Append("<p><strong> City</strong> : " + txtCity.Text + "<p>");
                sb.Append("<p><strong>Customer 1st and Last Name</strong> : " + txtLastname.Text + "<p>");
                sb.Append("<p><strong>Address </strong> : " + txtaddress.Text + "<p>");
                sb.Append("<br/>");
                sb.Append("</div>");

                sb.Append("<br/>");
                //   sb.Append("<p> PORTIN Request submitted successfully, If you have any issue  with PORTIN please contact Lycamobile directly at +1-845-301-1633 / +1-866-277-0024 <p>");

                sb.Append("<p> PORTIN Request submitted successfully, If you have any issue  with PORTIN please contact H2O WIRELESS. <p>");
                sb.Append("<p> Dealer Hotline 1-800-939-1261 <p>");
                sb.Append("<p> We are open 7 days a week from 9AM EST to 12AM EST Monday - Friday, and 9AM EST to 11PM EST on weekends. <p>");
                sb.Append("<p> H2O GSM Support 1-800-643-4926 <p>");
                sb.Append("<p>Email: customercare@h2owirelessnow.com <p>");

                sb.Append("<p>");
                sb.Append("<br/>");


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

        private void SaveButton()
        {
            {
                int distr = Convert.ToInt32(Session["DistributorID"]);
                int clnt = Convert.ToInt32(Session["ClientTypeID"]);
                string simnumber = txtSIMCARD.Text.Trim();
                string transact = "";
                //  Boolean IsBalance = CheckAccount(Convert.ToDouble(txtAmountPay.Text.Trim()));
                Boolean IsBalance = CheckAccount(Convert.ToDecimal(txtAmountPay.Text.Trim()));

                if (IsBalance == false)
                {
                    ShowPopUpMsg("Your Account Balance is Low \n Please Recharge Your Balance");
                    return;
                }
                DataTable dtTrans = svc.GetTransactionIDService();

                int id = Convert.ToInt32(dtTrans.Rows[0]["TRANSACTIONID"]);
                transact = id.ToString("00000");
                transact = ConfigurationManager.AppSettings.Get("TXN_SERIES") + transact;
                Boolean ValidSim = false;
                string erormsg = "";
                try
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



                if (ddlNetwork.SelectedItem.Text == "Lyca Mobile")
                {

                    if (ddlTariff.SelectedIndex == 0)
                    {
                        ShowPopUpMsg("Please select Tariff");
                        return;
                    }

                    string resp = ActivateSim(hddnTariffTypeID.Value, hddnTariffType.Value, hddnTariffCode.Value, txtEmailAddress.Text.Trim(), transact);


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
                                       // SendMail(txtEmailAddress.Text.Trim(), "Sim PortIn", ALLOCATED_MSISDN, txtSIMCARD.Text.Trim(), "");
                                        

                                        SPayment sp = new SPayment();
                                        sp.ChargedAmount = Convert.ToDecimal(txtAmountPay.Text.Trim());
                                        sp.PaymentType = 5;
                                        sp.PayeeID = Convert.ToInt32(Session["DistributorID"]);
                                        sp.PaymentFrom = 9;
                                        sp.ActivationType = 7;
                                        sp.ActivationStatus = 15;
                                        sp.ActivationVia = 17;
                                        sp.ActivationResp = resp;
                                        sp.ActivationRequest = RequestRes;
                                        sp.TariffID = Convert.ToInt32(ddlTariff.SelectedValue);
                                        sp.ALLOCATED_MSISDN = ALLOCATED_MSISDN;
                                        sp.TransactionId = transact;

                                        sp.PaymentMode = "Distributor PortIn";
                                        sp.TransactionStatusId = 24;
                                        sp.TransactionStatus = "Success";

                                        int dist = Convert.ToInt32(Session["DistributorID"]);
                                        int loginID = Convert.ToInt32(Session["LoginID"]);
                                        string sim = txtSIMCARD.Text.Trim();
                                        string zip = txtZIPCode.Text.Trim();
                                        string Language = "ENGLISH";
                                        string ChannelID = ConfigurationManager.AppSettings.Get("TXN_SERIES");
                                        try
                                        {
                                            int s = svc.UpdateAccountBalanceService(dist, loginID, sim, zip, Language, ChannelID, sp);

                                            WriteLog(dist, loginID, sim, zip, Language, ChannelID, sp, "Record Save Success when sim PortIn Success In Distributor Case");
                                            // add by akash starts
                                            if (ddlTariff.SelectedItem.Text == "PAYG")
                                            {
                                            }
                                            else
                                            {
                                                SendMailWithDetail(txtEmailAddress.Text.Trim(), "Sim PortIn", ALLOCATED_MSISDN.Trim(), txtSIMCARD.Text.Trim(), transact);
                                            }
                                            // add by akash ends
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
                                        SaveData(resp, 16, transact);
                                        ShowPopUpMsg("PORTIN Request Fail \n Please Try Again");
                                    }
                                }
                                else
                                {
                                    SaveData(resp, 16, transact);
                                    ShowPopUpMsg("PORTIN Request Fail \n Please Try Again");
                                }
                            }
                            else
                            {
                                SaveData(resp, 16, transact);

                                ShowPopUpMsg("PORTIN Request Fail \n Please Try Again");
                            }
                        }
                        catch (Exception ex)
                        {
                            SaveData(resp, 16, transact);
                            ShowPopUpMsg("PORTIN Request Fail \n Please Try Again");
                            //ShowPopUpMsg(ex.Message + "\n" + resp);
                        }
                    }
                    else
                    {
                        SaveData(resp, 16, transact);
                        ShowPopUpMsg("PORTIN Request Fail \n Please Try Again");
                    }
                }
                else if (ddlNetwork.SelectedItem.Text == "H20")
                {
                    if (ddlProduct.SelectedIndex == 0)
                    {
                        ShowPopUpMsg("Please select Tariff");
                        return;
                    }


                    string InvoiceNo = DateTime.Now.ToString().GetHashCode().ToString("X");


                    ENK.net.emida.ws.webServicesService ws = new webServicesService();
                    string ss1 = ws.LycaPortinPin("01", "3756263", "1234", hddnTariffCode.Value, txtSIMCARD.Text, txtPHONETOPORT.Text, txtACCOUNT.Text, txtPIN.Text, txtZIPCode.Text, "1", txtEmailAddress.Text, "$10", txtAmountPay.Text, InvoiceNo, "1");

                    if (ss1.Trim() != null && ss1.Trim() != "")
                    {
                        try
                        {

                            StringReader theReader = new StringReader(ss1);
                            DataSet theDataSet = new DataSet();
                            theDataSet.ReadXml(theReader);

                            if (theDataSet.Tables.Count > 0)
                            {
                                DataTable dt = theDataSet.Tables[0];
                                if (dt.Rows.Count > 0)
                                {

                                    string ResponseCode = dt.Rows[0]["ResponseCode"].ToString();
                                    string ResponseMessage = dt.Rows[0]["ResponseMessage"].ToString();
                                    if (ResponseCode == "00")
                                    {
                                        string ALLOCATED_MSISDN = theDataSet.Tables[2].Rows[0]["ALLOCATED_MSISDN"].ToString();
                                      //  SendMail(txtEmailAddress.Text.Trim(), "Sim PortIn", ALLOCATED_MSISDN, txtSIMCARD.Text.Trim(), "");
                                      

                                        SPayment sp = new SPayment();
                                        sp.ChargedAmount = Convert.ToDecimal(txtAmountPay.Text.Trim());
                                        sp.PaymentType = 5;
                                        sp.PayeeID = Convert.ToInt32(Session["DistributorID"]);
                                        sp.PaymentFrom = 9;
                                        sp.ActivationType = 7;
                                        sp.ActivationStatus = 15;
                                        sp.ActivationVia = 17;
                                        sp.ActivationResp = ResponseMessage;
                                        sp.ActivationRequest = RequestRes;
                                        sp.TariffID = Convert.ToInt32(ddlTariff.SelectedValue);
                                        sp.ALLOCATED_MSISDN = ALLOCATED_MSISDN;
                                        sp.TransactionId = transact;

                                        sp.PaymentMode = "Company PortIn";
                                        sp.TransactionStatusId = 24;
                                        sp.TransactionStatus = "Success";

                                        int dist = Convert.ToInt32(Session["DistributorID"]);
                                        int loginID = Convert.ToInt32(Session["LoginID"]);
                                        string sim = txtSIMCARD.Text.Trim();
                                        string zip = txtZIPCode.Text.Trim();
                                        string Language = "ENGLISH";
                                        string ChannelID = ConfigurationManager.AppSettings.Get("TXN_SERIES");
                                        try
                                        {
                                            int s = svc.UpdateAccountBalanceService(dist, loginID, sim, zip, Language, ChannelID, sp);

                                            WriteLog(dist, loginID, sim, zip, Language, ChannelID, sp, "Record Save Success when sim PortIn Success In Company Case");
                                            // add by akash starts
                                            if (ddlTariff.SelectedItem.Text == "PAYG")
                                            {
                                            }
                                            else
                                            {
                                                SendMailWithDetail(txtEmailAddress.Text.Trim(), "Sim PortIn", ALLOCATED_MSISDN.Trim(), txtSIMCARD.Text.Trim(), transact);
                                            }
                                            // add by akash ends
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
                                        SaveDataCompany(ss1, 16, transact);
                                        ShowPopUpMsg("PORTIN Request Fail \n Please Try Again");
                                    }
                                }
                                else
                                {
                                    SaveDataCompany(ss1, 16, transact);
                                    ShowPopUpMsg("PORTIN Request Fail \n Please Try Again");
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            SaveDataCompany(ss1, 16, transact);
                            ShowPopUpMsg("PORTIN Request Fail \n Please Try Again");
                            //ShowPopUpMsg(ex.Message + "\n" + resp);
                        }

                    }
                }


                else if (ddlNetwork.SelectedItem.Text == "EasyGo")
                {

                    if (ddlProduct.SelectedIndex == 0)
                    {
                        ShowPopUpMsg("Please select Tariff");
                        return;
                    }


                    string InvoiceNo = DateTime.Now.ToString().GetHashCode().ToString("X");


                    ENK.net.emida.ws.webServicesService ws = new webServicesService();
                    string ss1 = ws.LycaPortinPin("01", "3756263", "1234", hddnTariffCode.Value, txtSIMCARD.Text, txtPHONETOPORT.Text, txtACCOUNT.Text, txtPIN.Text, txtZIPCode.Text, "1", txtEmailAddress.Text, "$10", txtAmountPay.Text, InvoiceNo, "1");

                    if (ss1.Trim() != null && ss1.Trim() != "")
                    {
                        try
                        {

                            StringReader theReader = new StringReader(ss1);
                            DataSet theDataSet = new DataSet();
                            theDataSet.ReadXml(theReader);

                            if (theDataSet.Tables.Count > 0)
                            {
                                DataTable dt = theDataSet.Tables[0];
                                if (dt.Rows.Count > 0)
                                {

                                    string ResponseCode = dt.Rows[0]["ResponseCode"].ToString();
                                    string ResponseMessage = dt.Rows[0]["ResponseMessage"].ToString();
                                    if (ResponseCode == "00")
                                    {
                                        string ALLOCATED_MSISDN = theDataSet.Tables[2].Rows[0]["ALLOCATED_MSISDN"].ToString();
                                      //  SendMail(txtEmailAddress.Text.Trim(), "Sim PortIn", ALLOCATED_MSISDN, txtSIMCARD.Text.Trim(), "");
                                       

                                        SPayment sp = new SPayment();
                                        sp.ChargedAmount = Convert.ToDecimal(txtAmountPay.Text.Trim());
                                        sp.PaymentType = 5;
                                        sp.PayeeID = Convert.ToInt32(Session["DistributorID"]);
                                        sp.PaymentFrom = 9;
                                        sp.ActivationType = 7;
                                        sp.ActivationStatus = 15;
                                        sp.ActivationVia = 17;
                                        sp.ActivationResp = ResponseMessage;
                                        sp.ActivationRequest = RequestRes;
                                        sp.TariffID = Convert.ToInt32(ddlTariff.SelectedValue);
                                        sp.ALLOCATED_MSISDN = ALLOCATED_MSISDN;
                                        sp.TransactionId = transact;

                                        sp.PaymentMode = "Company PortIn";
                                        sp.TransactionStatusId = 24;
                                        sp.TransactionStatus = "Success";

                                        int dist = Convert.ToInt32(Session["DistributorID"]);
                                        int loginID = Convert.ToInt32(Session["LoginID"]);
                                        string sim = txtSIMCARD.Text.Trim();
                                        string zip = txtZIPCode.Text.Trim();
                                        string Language = "ENGLISH";
                                        string ChannelID = ConfigurationManager.AppSettings.Get("TXN_SERIES");
                                        try
                                        {
                                            int s = svc.UpdateAccountBalanceService(dist, loginID, sim, zip, Language, ChannelID, sp);

                                            WriteLog(dist, loginID, sim, zip, Language, ChannelID, sp, "Record Save Success when sim PortIn Success In Company Case");
                                            // add by akash starts
                                            if (ddlTariff.SelectedItem.Text == "PAYG")
                                            {
                                            }
                                            else
                                            {
                                                SendMailWithDetail(txtEmailAddress.Text.Trim(), "Sim PortIn", ALLOCATED_MSISDN.Trim(), txtSIMCARD.Text.Trim(), transact);
                                            }
                                            // add by akash ends
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
                                        SaveDataCompany(ss1, 16, transact);
                                        ShowPopUpMsg("PORTIN Request Fail \n Please Try Again");
                                    }
                                }
                                else
                                {
                                    SaveDataCompany(ss1, 16, transact);
                                    ShowPopUpMsg("PORTIN Request Fail \n Please Try Again");
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            SaveDataCompany(ss1, 16, transact);
                            ShowPopUpMsg("PORTIN Request Fail \n Please Try Again");
                            //ShowPopUpMsg(ex.Message + "\n" + resp);
                        }

                    }
                }
            }

        }

        private string GetTxnResult(string numberType, string number)
        {
            string reslt = "";
            /////////////////
            //API-HIT
            /////////////////
            return reslt;
        }

        //protected void BtnModify_Click(object sender, EventArgs e)
        //{
        //    //Response.Redirect("ActivationSim.aspx");
        //    //API function
        //}


        //anksin
        protected void BtnCancelportin_Click(object sender, EventArgs e)
        {
            //API function
            string result = "";
            result = "<ENVELOPE><HEADER><ERROR_CODE>0</ERROR_CODE><ERROR_DESC>Success</ERROR_DESC></HEADER><BODY></BODY></ENVELOPE>";
            string strRequest = "";

            result = CancelPortIn(TxtRefno.Text.ToString(), TxtMSISDN.Text.ToString(), ref strRequest);

            if (result.Trim() != null && result.Trim() != "") // should be added&&(!result.Contains("unable"));
            {
                StringReader theReader = new StringReader(result);
                DataSet theDataSet = new DataSet();
                theDataSet.ReadXml(theReader);
                if (result.Contains("Success"))
                {
                    //post reverse charges to be done.
                    svc.UpdateAccountBalanceAfterCancelPortIn(TxtRefno.Text.ToString(), strRequest, result, Convert.ToInt32(Session["LoginId"]));
                    ShowPopUpMsg("PORTIN Request Cancelled successfully!");
                }
                else
                {
                    ShowPopUpMsg("PORTIN Cancellation request FAILED!");
                }
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            // string MSISDNvalue =  TxtMSISDN.Text;
            //string ICCIDvalue = TxtICCID.Text;
            string Refnovalue = TxtRefno.Text;
            //btnSearch.Enabled = true;

            string result = "";

            //if (MSISDNvalue != "")
            //{
            //    result = CheckPortInStatus("PMSISDN", TxtMSISDN.Text);
            //}
            //else if (ICCIDvalue != "")
            //{
            //    result = CheckPortInStatus("ICC_ID", TxtMSISDN.Text);
            //}
            /////


            // 11 April 2017
            ////////////////////////////
            if (TxtRefno.Text != "")
            {
                //  DataSet ds = svc.GetMsimDetails("", TxtRefno.Text.Trim());
                //if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                //{
                result = CheckPortInStatus("REF_CODE", TxtRefno.Text);
                //}
                //else
                //{
                //    ShowPopUpMsg("No Request found. Port-In might be requested from outside the portal.");
                //    return;
                //}

            }
            else if (TxtRefno.Text == "" && TxtMSISDN.Text != "")
            {
                string MSISDN = "";

                if (TxtMSISDN.Text.StartsWith("1"))
                {
                    MSISDN = TxtMSISDN.Text.Substring(1);
                }
                else
                { MSISDN = TxtMSISDN.Text; }

                //  DataSet ds = svc.GetMsimDetails(Convert.ToString("1" + MSISDN), "");

                //if (ds.Tables.Count > 1)
                //{
                //    if (ds.Tables[1].Rows.Count > 0)
                //    {
                //if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                //{
                //string refno = Convert.ToString(ds.Tables[1].Rows[0]["MNPno"]).Trim();
                //TxtRefno.Text = refno;

                if (MSISDN.Length == 10)
                {
                    result = CheckPortInStatus("PMSISDN", MSISDN);
                }
                else
                {
                    ShowPopUpMsg("Invalid MSISDN");
                }
                //  }
                //else
                //{
                //    ShowPopUpMsg("No Request found. Port-In might be requested from outside the portal.");
                //    return;
                //}
                //}
                //else
                //{
                //    ShowPopUpMsg("No Request found. Port-In might be requested from outside the portal.");
                //    return;
                //}
            }
            ///////////////////////

            // <P_MSISDN>2162028681</P_MSISDN>
            //result = "<ENVELOPE><HEADER><ERROR_CODE>0</ERROR_CODE><ERROR_DESC>Success</ERROR_DESC></HEADER><BODY><GET_PORTIN_DETAILS_RESPONSE><REFERENCE_CODE>MNPPI0000005919</REFERENCE_CODE><RETURN_DESC>Request Rejected</RETURN_DESC><MSISDN>96327064300681</MSISDN><P_MSISDN>8628728322</P_MSISDN><PORTIN_DATE>2015-04-22</PORTIN_DATE><REQUEST_DATE>2015-04-22</REQUEST_DATE><CHANNEL>MBOS</CHANNEL><DONOR/><RECIPENT>XX55</RECIPENT><REJECT_CODE>GEN-0047</REJECT_CODE><REJECT_REASON>PortIn Resolution Required</REJECT_REASON><NAME>Lakshman A</NAME><CITY>Chennai</CITY><STATE>TN</STATE><COUNTRY>USA</COUNTRY><ZIP_CODE>32145</ZIP_CODE><STREET_NUMBER>A</STREET_NUMBER><STREET_NAME>A</STREET_NAME><STREET_DIRECTIONAL>E</STREET_DIRECTIONAL><PWD_PIN/><ACCOUNT_NUMBER>123456</ACCOUNT_NUMBER><SSN_TAX_ID/></GET_PORTIN_DETAILS_RESPONSE></BODY></ENVELOPE>";
            ////
            if (result.Trim() != null && result.Trim() != "")
            {
                StringReader theReader = new StringReader(result);
                DataSet theDataSet = new DataSet();
                theDataSet.ReadXml(theReader);
                //Lbltxn.Text = result;
                if (theDataSet.Tables.Contains("GET_PORTIN_DETAILS_RESPONSE") == true)
                {
                    GridPlanDetails.DataSource = theDataSet.Tables["GET_PORTIN_DETAILS_RESPONSE"]; //ds.Tables[4]
                    GridPlanDetails.DataBind();

                    TxtMSISDN.Text = theDataSet.Tables["GET_PORTIN_DETAILS_RESPONSE"].Rows[0]["P_MSISDN"].ToString();
                    TxtRefno.Text = theDataSet.Tables["GET_PORTIN_DETAILS_RESPONSE"].Rows[0]["REFERENCE_CODE"].ToString();

                    if ((theDataSet.Tables["GET_PORTIN_DETAILS_RESPONSE"].Rows[0]["RETURN_DESC"].ToString().ToLower() == "Request Rejected".ToLower()) || (theDataSet.Tables["GET_PORTIN_DETAILS_RESPONSE"].Rows[0]["RETURN_DESC"].ToString().ToLower() == "Processing Request".ToLower()))
                    {
                        //Lbltxn.Text = result;
                        BtnCancelportin.Enabled = true;
                        BtnModify.Enabled = true;

                    }
                    else
                    {
                        BtnCancelportin.Enabled = false;
                    }
                }

                else
                {
                    //Lbltxn.Text = result;
                    GridPlanDetails.DataSource = theDataSet.Tables[0]; //ds.Tables[4]
                    GridPlanDetails.DataBind();
                    BtnCancelportin.Enabled = false;
                }
            }
            else
            {


            }





        }


        private String CheckPortInStatus(String ReferenceType, String ReferenceCode)
        {

            try
            {
                String strResponse = String.Empty;
                String strRequest = "";
                String ChannelID = ConfigurationManager.AppSettings.Get("CHANNEL");
                String Entity = ConfigurationManager.AppSettings.Get("ENTITY");

                DataTable dtTrans = svc.GetTransactionIDService();

                int id = Convert.ToInt32(dtTrans.Rows[0]["TRANSACTIONID"]);
                String transact = id.ToString("00000");
                transact = ConfigurationManager.AppSettings.Get("TXN_SERIES") + transact;


                strRequest = "<ENVELOPE><HEADER><CHANNEL_REFERENCE>" + ChannelID + "</CHANNEL_REFERENCE><ENTITY>" + Entity + "</ENTITY><TRANSACTION_ID>" + transact + "</TRANSACTION_ID>" +
                             "</HEADER><BODY><GET_PORTIN_DETAILS_REQUEST><DETAILS><REFERENCE_NUMBER>" + ReferenceCode + "</REFERENCE_NUMBER>" +
                             "<REFERENCE_TYPE>" + ReferenceType + "</REFERENCE_TYPE>" +
                             "</DETAILS></GET_PORTIN_DETAILS_REQUEST></BODY></ENVELOPE>";
                Log(strRequest, "Check Portin Status");
                //strResponse = SendRequest(ConfigurationManager.AppSettings.Get("APIURL"), strRequest, "GET_PORTIN_DETAILS");
                strResponse = la.LycaAPIRequest(ConfigurationManager.AppSettings.Get("APIURL"), strRequest.Replace("<", "==").Replace(">", "!!"), "GET_PORTIN_DETAILS");
                Log(strResponse, "Check Portin Status");
                return strResponse;
            }
            catch (Exception Ex)
            {
                return "*2*" + Ex.Message + "*";
            }
        }



        //private String CheckPortInStatusByMSISDN(String ReferenceType, String ReferenceCode)
        //{

        //    try
        //    {
        //        String strResponse = String.Empty;
        //        String strRequest = "";
        //        String ChannelID = ConfigurationManager.AppSettings.Get("CHANNEL");
        //        String Entity = ConfigurationManager.AppSettings.Get("ENTITY");

        //        DataTable dtTrans = svc.GetTransactionIDService();

        //        int id = Convert.ToInt32(dtTrans.Rows[0]["TRANSACTIONID"]);
        //        String transact = id.ToString("00000");
        //        transact = ConfigurationManager.AppSettings.Get("TXN_SERIES") + transact;


        //        strRequest = "<ENVELOPE><HEADER><CHANNEL_REFERENCE>" + ChannelID + "</CHANNEL_REFERENCE><ENTITY>" + Entity + "</ENTITY><TRANSACTION_ID>" + transact + "</TRANSACTION_ID>" +
        //                     "</HEADER><BODY><GET_PORTIN_DETAILS_REQUEST><DETAILS><REFERENCE_NUMBER>" + ReferenceCode + "</REFERENCE_NUMBER>" +
        //                     "<REFERENCE_TYPE>" + ReferenceType + "</REFERENCE_TYPE>" +
        //                     "</DETAILS></GET_PORTIN_DETAILS_REQUEST></BODY></ENVELOPE>";
        //        Log(strRequest, "Check Portin Status");
        //        //strResponse = SendRequest(ConfigurationManager.AppSettings.Get("APIURL"), strRequest, "GET_PORTIN_DETAILS");
        //        strResponse = la.LycaAPIRequest(ConfigurationManager.AppSettings.Get("APIURL"), strRequest.Replace("<", "==").Replace(">", "!!"), "GET_PORTIN_DETAILS");
        //        Log(strResponse, "Check Portin Status");
        //        return strResponse;
        //    }
        //    catch (Exception Ex)
        //    {
        //        return "*2*" + Ex.Message + "*";
        //    }
        //}



        private String CancelPortIn(String ReferenceCode, String MSISDN, ref String strRequest)
        {

            try
            {
                String strResponse = String.Empty;
                strRequest = "";
                String ChannelID = ConfigurationManager.AppSettings.Get("CHANNEL");
                String Entity = ConfigurationManager.AppSettings.Get("ENTITY");

                DataTable dtTrans = svc.GetTransactionIDService();

                int id = Convert.ToInt32(dtTrans.Rows[0]["TRANSACTIONID"]);
                String transact = id.ToString("00000");
                transact = ConfigurationManager.AppSettings.Get("TXN_SERIES") + transact;
                //string X = "<ENVELOPE><HEADER><CHANNEL_REFERENCE>" + ChannelID + "</CHANNEL_REFERENCE><ENTITY>" + ChannelID + "</ENTITY><TRANSACTION_ID>" + TXNID + "</TRANSACTION_ID></HEADER><BODY>
                //<CANCEL_PORTIN_REQUEST><DETAILS><REFERENCE_CODE>" + MNPCode + "</REFERENCE_CODE><P_MSISDN>" + MSISDN + "</P_MSISDN><CHANNEL>BNK</CHANNEL></DETAILS></CANCEL_PORTIN_REQUEST></BODY></ENVELOPE>";
                
                strRequest = "<ENVELOPE><HEADER><CHANNEL_REFERENCE>" + ChannelID + "</CHANNEL_REFERENCE><ENTITY>" + Entity + "</ENTITY><TRANSACTION_ID>" + transact +
                "</TRANSACTION_ID></HEADER><BODY><CANCEL_PORTIN_REQUEST><DETAILS><REFERENCE_CODE>" + ReferenceCode + "</REFERENCE_CODE><P_MSISDN>"
                + MSISDN + "</P_MSISDN><CHANNEL>" + ChannelID + "</CHANNEL></DETAILS></CANCEL_PORTIN_REQUEST></BODY></ENVELOPE>";
                Log(strRequest, "CancelPortinBegin");
                //strResponse = SendRequest(ConfigurationManager.AppSettings.Get("APIURL"), strRequest, "CANCEL_PORTIN");
                strResponse = la.LycaAPIRequest(ConfigurationManager.AppSettings.Get("APIURL"), strRequest.Replace("<", "==").Replace(">", "!!"), "CANCEL_PORTIN");
                Log(strResponse, "Cancel Portin Stop");
                return strResponse;
            }
            catch (Exception Ex)
            {
                return "*2*" + Ex.Message + "*";
            }
        }


        private String ModifyPortIn(String ReferenceCode, String MSISDN, String ICCID, String AccountNumber, String PIN, String ZIP, ref String strRequest)
        {

            try
            {
                String strResponse = String.Empty;
                strRequest = "";
                String ChannelID = ConfigurationManager.AppSettings.Get("CHANNEL");
                String Entity = ConfigurationManager.AppSettings.Get("ENTITY");

                DataTable dtTrans = svc.GetTransactionIDService();

                int id = Convert.ToInt32(dtTrans.Rows[0]["TRANSACTIONID"]);
                String transact = id.ToString("00000");
                transact = ConfigurationManager.AppSettings.Get("TXN_SERIES") + transact;
                //string X = "<ENVELOPE><HEADER><CHANNEL_REFERENCE>" + ChannelID + "</CHANNEL_REFERENCE><ENTITY>" + ChannelID + "</ENTITY><TRANSACTION_ID>" + TXNID + 
                //"</TRANSACTION_ID></HEADER><BODY><MODIFY_PORTIN_REQUEST><DETAILS><REFERENCE_CODE>" + MNPCode + "</REFERENCE_CODE><ICC_ID>" + ICCID + "</ICC_ID>
                //<P_MSISDN>" + MSISDN + "</P_MSISDN><ACCOUNT_NUMBER>" + AccountNumber + "</ACCOUNT_NUMBER><PWD_PIN>" + PIN + "</PWD_PIN><ZIP_CODE>" + ZipCode + "</ZIP_CODE><NAME>Lakshman</NAME><STREET_NUMBER>874</STREET_NUMBER><STREET_NAME>TUSCIS</STREET_NAME><STREET_DIRECTIONAL>E</STREET_DIRECTIONAL><CITY>SEATTLE</CITY><STATE>WA</STATE><COUNTRY>USA</COUNTRY><CHANNEL>" + ChannelID + "</CHANNEL></DETAILS></MODIFY_PORTIN_REQUEST></BODY></ENVELOPE>";
                strRequest = "<ENVELOPE><HEADER><CHANNEL_REFERENCE>" + ChannelID + "</CHANNEL_REFERENCE><ENTITY>" + Entity + "</ENTITY><TRANSACTION_ID>" + transact +
                    "</TRANSACTION_ID></HEADER><BODY><MODIFY_PORTIN_REQUEST><DETAILS><REFERENCE_CODE>" + ReferenceCode + "</REFERENCE_CODE><ICC_ID>" +
                    ICCID + "</ICC_ID><P_MSISDN>" + MSISDN + "</P_MSISDN><ACCOUNT_NUMBER>" + AccountNumber + "</ACCOUNT_NUMBER><PWD_PIN>" + PIN + "</PWD_PIN><ZIP_CODE>" + ZIP +
                    "</ZIP_CODE><CHANNEL>" + ChannelID + "</CHANNEL></DETAILS></MODIFY_PORTIN_REQUEST></BODY></ENVELOPE>";
                Log(strRequest, "Modify Portin Begins");
                //strResponse = SendRequest(ConfigurationManager.AppSettings.Get("APIURL"), strRequest, "MODIFY_PORTIN");
                strResponse = la.LycaAPIRequest(ConfigurationManager.AppSettings.Get("APIURL"), strRequest.Replace("<", "==").Replace(">", "!!"), "MODIFY_PORTIN");
                Log(strResponse, "Modify Portin Stop");
                return strResponse;
            }
            catch (Exception Ex)
            {
                return "*2*" + Ex.Message + "*";
            }
        }

        //Ankit Singh
        protected void BtnModify_Click(object sender, EventArgs e)
        {
             try
            {
                ActivationPort.Visible = false;
                ActivationPort1.Visible = true;

                //TO SET MODIFIED PORTIN DETAILS IN TEXTBOX
                DataSet ds = new DataSet();
                ds = svc.GetMsimDetails(Convert.ToString("1" + TxtMSISDN.Text),"");
                DataTable dtMSIM = new DataTable();
                dtMSIM = ds.Tables[0];
                //Ankit Singh
                if (dtMSIM.Rows.Count > 0)
                ////
                {
                    string strActivationRequest = Convert.ToString(dtMSIM.Rows[0][10]).Trim();
                    StringReader theReader = new StringReader(strActivationRequest);
                    DataSet theDataSet = new DataSet();
                    theDataSet.ReadXml(theReader);

                    if (theDataSet.Tables.Count > 2)
                    {
                        DataTable dt = theDataSet.Tables[3];
                        if (dt.Rows.Count > 0)
                        {
                            txtAccountNumber.Text = Convert.ToString(dt.Rows[0][4]);
                            txtPinNumber.Text = Convert.ToString(dt.Rows[0][5]);
                            txtZip.Text = Convert.ToString(dt.Rows[0][1]);
                        }
                    }

                }
                else
                {
                    ShowPopUpMsg("This sim wasn't ported in by this portal");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        //Ankit Singh
        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                string APIResponse = ""; //-to save result decide, success or failure
                // get ICCID & strActivationRequest
                DataSet ds = new DataSet();
                ds = svc.GetMsimDetails(Convert.ToString("1" + TxtMSISDN.Text),"");
                DataTable dtMSIM = new DataTable();
                dtMSIM = ds.Tables[0];
                string strActivationRequest = Convert.ToString(dtMSIM.Rows[0][10]).Trim();
                StringReader theReader = new StringReader(strActivationRequest);
                DataSet theDataSet = new DataSet();
                theDataSet.ReadXml(theReader);
                string ICCID = "";
                
                if (theDataSet.Tables.Count > 2)
                {
                    DataTable dt = theDataSet.Tables[3];
                    if (dt.Rows.Count > 0)
                    {
                        ICCID = Convert.ToString(dt.Rows[0][0]);

                    }
                }
                
                //TO CALL API
                string AccountNumber = txtAccountNumber.Text.Trim();
                string PIN = txtPinNumber.Text.Trim();
                string ZIP = txtZip.Text.Trim();
                string strRequest = "";
                string ReferenceCode = TxtRefno.Text.Trim();
                string MSISDN1 = TxtMSISDN.Text.Trim();
                
                if (ICCID != "")
                {
                    string result1 = ModifyPortIn(ReferenceCode, MSISDN1, ICCID, AccountNumber, PIN, ZIP, ref strRequest);

                    if (result1.Trim() != null && result1.Trim() != "" && !result1.Trim().Contains("Unable"))
                    {
                        StringReader theReader1 = new StringReader(result1);
                        DataSet theDataSet1 = new DataSet();
                        theDataSet1.ReadXml(theReader1);
                        if (result1.Contains("Success"))
                        {
                            APIResponse = "Success";
                            ShowPopUpMsg("PORTIN Request Modified successfully!");
                        }
                        else
                        {
                            APIResponse = "Failure";
                            ShowPopUpMsg("PORTIN Modification Request FAILED!");
                        }
                    }
                        //TO SAVE MODIFIED PORTIN DETAILS
                        DateTime RequestedTime = DateTime.Now;
                        int Requestedby = Convert.ToInt32(Session["LoginId"]);
                        string MSISDN = TxtMSISDN.Text.Trim();
                        svc.SaveModifiedPortinDetails(strRequest, APIResponse, RequestedTime, Requestedby, MSISDN);
                        ShowPopUpMsg("Modified portin details are saved");
                    
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //protected void RdbtnMSISDN_CheckedChanged(object sender, EventArgs e)
        //{
        //    RdbtnICCID.Checked= false;
        //    RdbtnREFNO.Checked = false;

        //    TxtMSISDN.Enabled = true;
        //    TxtMSISDN.Focus();
        //    TxtICCID.Enabled = false;
        //    TxtICCID.Text = "";
        //    TxtRefno.Enabled = false;
        //    TxtRefno.Text = "";
        //}

        //protected void RdbtnICCID_CheckedChanged(object sender, EventArgs e)
        //{
        //    RdbtnMSISDN.Checked= false;
        //    RdbtnREFNO.Checked = false;

        //    TxtMSISDN.Enabled = false;
        //    TxtMSISDN.Text = "";
        //    //TxtICCID.Enabled = true;
        //    //TxtICCID.Focus();
        //    TxtRefno.Enabled = false;
        //    TxtRefno.Text = "";
        //}

        //protected void RdbtnREFNO_CheckedChanged(object sender, EventArgs e)
        //{
        //   // RdbtnMSISDN.Checked= false;
        //    //RdbtnICCID.Checked = false;

        //    TxtMSISDN.Enabled = false;
        //    TxtMSISDN.Text = "";
        //    //TxtICCID.Enabled = false;
        //    //TxtICCID.Text = "";
        //    TxtRefno.Enabled = true;
        //    TxtRefno.Focus();
        //}

    }
}