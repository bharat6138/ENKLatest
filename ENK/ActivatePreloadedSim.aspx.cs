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
using ENK.LycaAPI;

namespace ENK
{
    public partial class ActivatePreloadedSim : System.Web.UI.Page
    {
        Service1Client svc = new Service1Client();
        LycaAPI.LycaAPISoapClient la = new LycaAPI.LycaAPISoapClient();

        string RequestRes = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                DivVal.Visible = false;
                txtChannelID.Text = ConfigurationManager.AppSettings.Get("TXN_SERIES");
                hddnAddOn.Value = Convert.ToString(0);
                hddnInternational.Value = Convert.ToString(0);

                hddnDataAddOnValue.Value = Convert.ToString(0);
                hddnDataAddOnDiscountedAmount.Value = Convert.ToString(0);
                hddnDataAddOnDiscountPercent.Value = Convert.ToString(0);

                hddnInternationalCreditValue.Value = Convert.ToString(0);
                hddnInternationalCreditDiscountedAmount.Value = Convert.ToString(0);
                hddnInternationalCreditDiscountPercent.Value = Convert.ToString(0);


                hddnMonths.Value = Convert.ToString(txtMonth.Text);
                txtPHONETOPORT.Enabled = false;
                txtACCOUNT.Enabled = false;
                txtPIN.Enabled = false;
                try
                {
                    if (Session["LoginID"] != null)
                    {
                        ENK.net.emida.ws.webServicesService ws = new webServicesService();
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
                            }
                        }
                        ViewState["EmailBYDefault"] = ConfigurationManager.AppSettings.Get("COMPANY_EMAIL");
                        divahannel.Visible = false;
                        divLanguage.Visible = false;
                        txtAmountPay.Attributes.Add("readonly", "true");
                        if (Convert.ToString(Session["ClientType"]) == "Company")
                        {
                            txtAmountPay.Text = "0.00";
                            btnCompanyByAccount.Visible = true;
                            btnDistributorByAccount.Visible = false;       


                        }
                        else
                        {
                            txtAmountPay.Text = "0.00";
                            btnCompanyByAccount.Visible = false;
                            btnDistributorByAccount.Visible = true;
                            ddlDataAddOn.Enabled = false;
                            ddlInternationalCreadits.Enabled = false;
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
        protected void btnCompanyByAccount_Click(object sender, EventArgs e)
        {
            int _networkid = 0;
            int.TryParse(Convert.ToString(ddlNetwork.SelectedValue), out _networkid);

            if (chkActivePort.Checked == true && (txtPIN.Text.Trim() == "" || txtACCOUNT.Text.Trim() == "" || txtPHONETOPORT.Text.Trim() == ""))
            {
                ShowPopUpMsg("You have selected the Port-In, Please enter the Mandatory Fields");
                return;

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
                        //ClientID = 1, ClientTypeID = 3
                        DataSet ds = svc.CheckSimActivationService(1, 3, simnumber, "Activate");
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
                       // DataSet ds = svc.CheckSimPortINService(distr, clnt, simnumber);
                        DataSet ds = svc.CheckSimPortINService(1, 3, simnumber);
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
                if (ValidSim == false)
                {
                    ShowPopUpMsg(erormsg);
                    return;
                }
                string resp = "";

                if (ddlNetwork.SelectedItem.Text == "Lyca Mobile")
                {                   
                      Decimal Regulatery = 0;

                    if (chkActivePort.Checked == false)
                    {
                        // resp = ActivateLycaSim(hddnTariffTypeID.Value, hddnTariffType.Value, hddnTariffCode.Value, txtEmail.Text.Trim(), transact);
                         resp = ActivateLycaSim(hddnTariffTypeID.Value, hddnTariffType.Value, "", ViewState["EmailBYDefault"].ToString(), transact);

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
                                            string MNPNO = theDataSet.Tables[2].Rows[0]["PORTIN_REFERENCE_NUMBER"].ToString();
                                       //     SendMail(ViewState["EmailBYDefault"].ToString(), "Lycamobile Preloaded Sim Activation", ALLOCATED_MSISDN.Trim(), txtSIMCARD.Text.Trim(), "");

                                            SPayment sp = new SPayment();
                                            txtAmountPay.Text = "0.00";
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
                                                int tariffDataAddOnID = 0;
                                                int tariffInternationalCreaditsID = 0;

                                                decimal DataAddOnValue = Convert.ToDecimal("0.00");
                                                decimal DataAddOnDiscountedAmount = Convert.ToDecimal("0.00");
                                                decimal DataAddOnDiscountPercent = Convert.ToDecimal("0.00");
                                                decimal InternationalCreditValue = Convert.ToDecimal("0.00");
                                                decimal InternationalCreditDiscountedAmount = Convert.ToDecimal("0.00");
                                                decimal InternationalCreditDiscountPercent = Convert.ToDecimal("0.00");
                                                int s = svc.UpdateAccountBalanceServiceActivation(dist, loginID, sim, zip, ChannelID, Language, _networkid, sp, tariffDataAddOnID, tariffInternationalCreaditsID, DataAddOnValue, DataAddOnDiscountedAmount, DataAddOnDiscountPercent, InternationalCreditValue, InternationalCreditDiscountedAmount, InternationalCreditDiscountPercent, MNPNO, "");

                                                WriteLog(dist, loginID, sim, zip, ChannelID, Language, sp, "Record Save Success when Preloaded sim activated In Company Case");
                      
                                            }
                                            catch (Exception ex)
                                            {
                                                WriteLog(dist, loginID, sim, zip, Language, ChannelID, sp, "Record Save fail when sim Preloaded activated In Company Case");
                                            }

                                            ShowPopUpMsg("SIM Preloaded activation done successfully\n with Mobile Number - " + ALLOCATED_MSISDN);
                                            resetControls(1);
                                        }
                                        else
                                        {
                                            SaveDataCompany(resp, 16, transact);
                                            ShowPopUpMsg("SIM Preloaded activation Fail \n Please Try Again");
                                        }
                                    }
                                    else
                                    {
                                        SaveDataCompany(resp, 16, transact);
                                        ShowPopUpMsg("SIM Preloaded activation Fail \n Please Try Again");
                                    }
                                }
                                else
                                {
                                    SaveDataCompany(resp, 16, transact);
                                    ShowPopUpMsg("SIM Preloaded activation Fail \n Please Try Again");
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
                            ShowPopUpMsg("SIM Preloaded activation Fail \n Please Try Again");
                        }

                    }
                    else
                    {
                        //string respP = ActivateSim(hddnTariffTypeID.Value, hddnTariffType.Value, hddnTariffCode.Value, txtEmail.Text.Trim(), transact);
                        string respP = ActivateSim(hddnTariffTypeID.Value, hddnTariffType.Value, "", ViewState["EmailBYDefault"].ToString(), transact);

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
                                        //    SendMail(ViewState["EmailBYDefault"].ToString(), "Sim PortIn", ALLOCATED_MSISDN, txtSIMCARD.Text.Trim(), "");

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
                                            sp.TariffID = Convert.ToInt32(hddnTariffID.Value);
                                            sp.ALLOCATED_MSISDN = ALLOCATED_MSISDN;

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

                                                int tariffDataAddOnID = 0; 
                                                int tariffInternationalCreaditsID = 0; 
                                                decimal DataAddOnValue = Convert.ToDecimal("0.00");
                                                decimal DataAddOnDiscountedAmount = Convert.ToDecimal("0.00");
                                                decimal DataAddOnDiscountPercent = Convert.ToDecimal("0.00");
                                                decimal InternationalCreditValue = Convert.ToDecimal("0.00");
                                                decimal InternationalCreditDiscountedAmount = Convert.ToDecimal("0.00");
                                                decimal InternationalCreditDiscountPercent = Convert.ToDecimal("0.00");

                                                int s = svc.UpdateAccountBalanceServiceActivation(dist, loginID, sim, zip, ChannelID, Language, _networkid, sp, tariffDataAddOnID, tariffInternationalCreaditsID, DataAddOnValue, DataAddOnDiscountedAmount, DataAddOnDiscountPercent, InternationalCreditValue, InternationalCreditDiscountedAmount, InternationalCreditDiscountPercent, MNPNO, "");
                                                WriteLog(dist, loginID, sim, zip, ChannelID, Language, sp, "Record Save Success when sim PortIn Success In Company Case");

                                            }
                                            catch (Exception ex)
                                            {
                                                WriteLog(dist, loginID, sim, zip, Language, ChannelID, sp, "Record Save Fail when sim Preloaded PortIn Success  In Company Case");

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
                            }
                        }
                        else
                        {
                            SaveDataCompany(resp, 16, transact);
                            ShowPopUpMsg("PORTIN Request Fail \n Please Try Again");
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
                string SIMCARD = Convert.ToString(txtSIMCARD.Text.Trim());
                string ZIPCode = Convert.ToString(txtZIPCode.Text.Trim());
                string EmailAddress = Convert.ToString(ViewState["EmailBYDefault"].ToString());
                string Language = "ENGLISH";
                string ChannelID = ConfigurationManager.AppSettings.Get("CHANNEL");
                string PHONETOPORT = Convert.ToString(txtPHONETOPORT.Text.Trim());
                string ACCOUNT = Convert.ToString(txtACCOUNT.Text.Trim());
                string PIN = Convert.ToString(txtPIN.Text.Trim());
                string TOPUP_AMOUNT = hddnInternationalCreditValue.Value;

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

                }
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
                ShowPopUpMsg(ex.Message);
                return response;
            }
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
        public void SaveDataCompany(string resp, int status, string TransactionID)
        {
            int _networkid = 0;
            int.TryParse(Convert.ToString(ddlNetwork.SelectedValue), out _networkid);

            Decimal Regulatery = 0;
            SPayment sp = new SPayment();
            sp.ChargedAmount = Convert.ToDecimal("0.00");
            sp.PaymentType = 4;
            sp.PayeeID = Convert.ToInt32(Session["DistributorID"]);
            sp.PaymentFrom = 9;
            sp.ActivationType = 6;
            sp.ActivationStatus = status;
            sp.ActivationVia = 17;
            sp.ActivationResp = resp;
            sp.ActivationRequest = RequestRes;
            //ankit
           // sp.TariffID = Convert.ToInt32(ddlTariff.SelectedValue);
              sp.TariffID = Convert.ToInt32(hddnTariffID.Value);         
            //ankit
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
                int tariffDataAddOnID = 0;
                int tariffInternationalCreaditsID = 0; 
                decimal DataAddOnValue = Convert.ToDecimal("0.00");
                decimal DataAddOnDiscountedAmount = Convert.ToDecimal("0.00");
                decimal DataAddOnDiscountPercent = Convert.ToDecimal("0.00");
                decimal InternationalCreditValue = Convert.ToDecimal("0.00");
                decimal InternationalCreditDiscountedAmount = Convert.ToDecimal("0.00");
                decimal InternationalCreditDiscountPercent = Convert.ToDecimal("0.00");
                string MNPNO = "";
                int s = svc.UpdateAccountBalanceServiceActivation(dist, loginID, sim, zip, ChannelID, Language, _networkid, sp, tariffDataAddOnID, tariffInternationalCreaditsID, DataAddOnValue, DataAddOnDiscountedAmount, DataAddOnDiscountPercent, InternationalCreditValue, InternationalCreditDiscountedAmount, InternationalCreditDiscountPercent, MNPNO, "");
                WriteLog(dist, loginID, sim, zip, ChannelID, Language, sp, "Record Save Success when sim not activated In Company Case");

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

            if (chkActivePort.Checked == true && (txtPIN.Text.Trim() == "" || txtACCOUNT.Text.Trim() == "" || txtPHONETOPORT.Text.Trim() == ""))
            {
                ShowPopUpMsg("You have selected the Port-In, Please enter the Mandatory Fields");
                return;

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

                
                Boolean ValidSim = false;
                string erormsg = "";
                try
                {
                    if (chkActivePort.Checked == false)
                    {
                        //DataSet ds = svc.CheckSimActivationService(distr, clnt, simnumber, "Activate");
                        DataSet ds = svc.CheckSimActivationService(1, 3, simnumber, "Activate");
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
                        //DataSet ds = svc.CheckSimPortINService(distr, clnt, simnumber);
                        DataSet ds = svc.CheckSimPortINService(1, 3, simnumber);
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
                    Decimal Regulatery = 0;
                    if (chkActivePort.Checked == false)
                    {
                        // resp = ActivateLycaSim(hddnTariffTypeID.Value, hddnTariffType.Value, hddnTariffCode.Value, txtEmail.Text.Trim(), transact);
                        resp = ActivateLycaSim(hddnTariffTypeID.Value, hddnTariffType.Value, "", ViewState["EmailBYDefault"].ToString(), transact);
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
                                           // SendMail(ViewState["EmailBYDefault"].ToString(), "Lycamobile Sim Activation", ALLOCATED_MSISDN, txtSIMCARD.Text.Trim(), "");

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
                                                int tariffDataAddOnID = 0;
                                                int tariffInternationalCreaditsID = 0;
                                                decimal DataAddOnValue = Convert.ToDecimal("0.00");
                                                decimal DataAddOnDiscountedAmount = Convert.ToDecimal("0.00");
                                                decimal DataAddOnDiscountPercent = Convert.ToDecimal("0.00");
                                                decimal InternationalCreditValue = Convert.ToDecimal("0.00");
                                                decimal InternationalCreditDiscountedAmount = Convert.ToDecimal("0.00");
                                                decimal InternationalCreditDiscountPercent = Convert.ToDecimal("0.00");

                                                int s = svc.UpdateAccountBalanceServiceActivation(dist, loginID, sim, zip, ChannelID, Language, _networkid, sp, tariffDataAddOnID, tariffInternationalCreaditsID, DataAddOnValue, DataAddOnDiscountedAmount, DataAddOnDiscountPercent, InternationalCreditValue, InternationalCreditDiscountedAmount, InternationalCreditDiscountPercent, MNPNO, "");
                                                WriteLog(dist, loginID, sim, zip, ChannelID, Language, sp, "Record Save Success when sim activated In Distributor Case");
                                           
                                            }
                                            catch (Exception ex)
                                            {
                                                WriteLog(dist, loginID, sim, zip, Language, ChannelID, sp, "Record Save Fail when sim activated  In Distributor Case");

                                            }

                                            ShowPopUpMsg("SIM Preloaded activation done successfully\n with Mobile Number - " + ALLOCATED_MSISDN);
                                            resetControls(1);
                                        }
                                        else
                                        {
                                            SaveData(resp, 16, transact);
                                            ShowPopUpMsg("SIM Preloaded activation Fail \n Please Try Again");
                                        }
                                    }
                                    else
                                    {
                                        SaveData(resp, 16, transact);
                                        ShowPopUpMsg("SIM Preloaded activation Fail \n Please Try Again");
                                    }
                                }
                                else
                                {
                                    SaveData(resp, 16, transact);
                                    ShowPopUpMsg("SIM Preloaded activation Fail \n Please Try Again");
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
                            ShowPopUpMsg("SIM Preloaded activation Fail \n Please Try Again");
                        }
                    }
                    else
                    {
                        //string respP = ActivateSim(hddnTariffTypeID.Value, hddnTariffType.Value, hddnTariffCode.Value, txtEmail.Text.Trim(), transact);
                        string respP = ActivateSim(hddnTariffTypeID.Value, hddnTariffType.Value, "", ViewState["EmailBYDefault"].ToString(), transact);
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
                                       //     SendMail(ViewState["EmailBYDefault"].ToString(), "Sim PortIn", ALLOCATED_MSISDN, txtSIMCARD.Text.Trim(), "");

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
                                            //string zip = txtZIPCode.Text.Trim();
                                            string Language = "ENGLISH";
                                            string ChannelID = ConfigurationManager.AppSettings.Get("TXN_SERIES");
                                            try
                                            {
                                                int tariffDataAddOnID = 0;
                                                int tariffInternationalCreaditsID = 0;
                                                decimal DataAddOnValue = Convert.ToDecimal("0.00");
                                                decimal DataAddOnDiscountedAmount = Convert.ToDecimal("0.00");
                                                decimal DataAddOnDiscountPercent = Convert.ToDecimal("0.00");
                                                decimal InternationalCreditValue = Convert.ToDecimal("0.00");
                                                decimal InternationalCreditDiscountedAmount = Convert.ToDecimal("0.00");
                                                decimal InternationalCreditDiscountPercent = Convert.ToDecimal("0.00");

                                                int s = svc.UpdateAccountBalanceServiceActivation(dist, loginID, sim, zip, ChannelID, Language, _networkid, sp, tariffDataAddOnID, tariffInternationalCreaditsID, DataAddOnValue, DataAddOnDiscountedAmount, DataAddOnDiscountPercent, InternationalCreditValue, InternationalCreditDiscountedAmount, InternationalCreditDiscountPercent, MNPNO, "");
                                                WriteLog(dist, loginID, sim, zip, ChannelID, Language, sp, "Record Save Success when sim PortIn Success In Distributor Case");
                                         
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

                }

            }
        }
        public void SaveData(string resp, int status, string TransactionID)
        {
            int _networkid = 0;
            int.TryParse(Convert.ToString(ddlNetwork.SelectedValue), out _networkid);

            Decimal Regulatery = 0;
            SPayment sp = new SPayment();
            sp.ChargedAmount = Convert.ToDecimal("0.00");
            sp.PaymentType = 4;
            sp.PayeeID = Convert.ToInt32(Session["DistributorID"]);
            sp.PaymentFrom = 9;
            sp.ActivationType = 6;
            sp.ActivationStatus = status;
            sp.ActivationVia = 17;
            sp.ActivationResp = resp;
            sp.ActivationRequest = RequestRes;
            //ankit
            //sp.TariffID = Convert.ToInt32(ddlTariff.SelectedValue);
            sp.TariffID = Convert.ToInt32(hddnTariffID.Value);
            //ankit
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
                int tariffDataAddOnID = 0;
                int tariffInternationalCreaditsID = 0;
                decimal DataAddOnValue = Convert.ToDecimal(hddnDataAddOnValue.Value);
                decimal DataAddOnDiscountedAmount = Convert.ToDecimal("0.00");
                decimal DataAddOnDiscountPercent = Convert.ToDecimal("0.00");
                decimal InternationalCreditValue = Convert.ToDecimal("0.00");
                decimal InternationalCreditDiscountedAmount = Convert.ToDecimal("0.00");
                decimal InternationalCreditDiscountPercent = Convert.ToDecimal("0.00");
                string MNPNO = "";
                int s = svc.UpdateAccountBalanceServiceActivation(dist, loginID, sim, zip, ChannelID, Language, _networkid, sp, tariffDataAddOnID, tariffInternationalCreaditsID, DataAddOnValue, DataAddOnDiscountedAmount, DataAddOnDiscountPercent, InternationalCreditValue, InternationalCreditDiscountedAmount, InternationalCreditDiscountPercent, MNPNO, "");
                WriteLog(dist, loginID, sim, zip, ChannelID, Language, sp, "Record Save success when sim Not activated In Distributor Case");
            }
            catch (Exception ex)
            {
                WriteLog(dist, loginID, sim, zip, Language, ChannelID, sp, "Record Save Fail when sim not activate In Distributor Case");
            }
            resetControls(1);
        }
        public void WriteLog(int dist, int loginID, string sim, string zip, string Language, string ChannelID, SPayment sp, string msgg)
        {
            StringBuilder logdata = new StringBuilder();
            logdata.Append(dist + "|");
            logdata.Append(loginID + "|");
            logdata.Append(sim + "|");
            logdata.Append(zip + "|");
            logdata.Append(ViewState["EmailBYDefault"].ToString() + "|");
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
        public string ActivateLycaSim(string TariffTypeID, string TariffType, string TariffCode, string email, string TransactionID)
        {
            string response = "";
            try
            {
                string BundleID = TariffTypeID;
                string BundleCode = TariffCode;
                string BundleType = TariffType;
                string BundleAmount = "";
                string EmailID = email;
                string X = "";
                string activation = "1";
                string SIMCARD = Convert.ToString(txtSIMCARD.Text.Trim());
                string ZIPCode = Convert.ToString(txtZIPCode.Text.Trim());
                string Language = "ENGLISH";
                string ChannelID = ConfigurationManager.AppSettings.Get("CHANNEL");


                if (BundleType == "National")
                {
                    X = "<ENVELOPE><HEADER><TRANSACTION_ID>" + TransactionID + "</TRANSACTION_ID><ENTITY>" + ChannelID + "</ENTITY><CHANNEL_REFERENCE>" + ChannelID + "</CHANNEL_REFERENCE></HEADER><BODY><ACTIVATE_USIM_PORTIN_BUNDLE_REQUEST><DETAILS><ICC_ID>" + SIMCARD + "</ICC_ID><ZIP_CODE>" + ZIPCode + "</ZIP_CODE><PREFERRED_LANGUAGE>" + Language + "</PREFERRED_LANGUAGE><P_MSISDN></P_MSISDN><ACCOUNT_NUMBER></ACCOUNT_NUMBER><PASSWORD_PIN></PASSWORD_PIN><NO_OF_MONTHS>" + hddnMonths.Value + "</NO_OF_MONTHS><NATIONAL_BUNDLE_CODE>" + BundleCode + "</NATIONAL_BUNDLE_CODE><NATIONAL_BUNDLE_AMOUNT>" + BundleAmount + "</NATIONAL_BUNDLE_AMOUNT><INTERNATIONAL_BUNDLE_CODE></INTERNATIONAL_BUNDLE_CODE><INTERNATIONAL_BUNDLE_AMOUNT></INTERNATIONAL_BUNDLE_AMOUNT><TOPUP_AMOUNT></TOPUP_AMOUNT><TOPUP_CARD_ID></TOPUP_CARD_ID><VOUCHER_PIN></VOUCHER_PIN><CHANNEL_ID>" + ChannelID + "</CHANNEL_ID><EMAIL_ID>" + EmailID + "</EMAIL_ID></DETAILS></ACTIVATE_USIM_PORTIN_BUNDLE_REQUEST></BODY></ENVELOPE>";
                }
                else
                {
                    X = "<ENVELOPE><HEADER><TRANSACTION_ID>" + TransactionID + "</TRANSACTION_ID><ENTITY>" + ChannelID + "</ENTITY><CHANNEL_REFERENCE>" + ChannelID + "</CHANNEL_REFERENCE></HEADER><BODY><ACTIVATE_USIM_PORTIN_BUNDLE_REQUEST><DETAILS><ICC_ID>" + SIMCARD + "</ICC_ID><ZIP_CODE>" + ZIPCode + "</ZIP_CODE><PREFERRED_LANGUAGE>" + Language + "</PREFERRED_LANGUAGE><P_MSISDN></P_MSISDN><ACCOUNT_NUMBER></ACCOUNT_NUMBER><PASSWORD_PIN></PASSWORD_PIN><NO_OF_MONTHS>" + hddnMonths.Value + "</NO_OF_MONTHS><NATIONAL_BUNDLE_CODE>" + BundleCode + "</NATIONAL_BUNDLE_CODE><NATIONAL_BUNDLE_AMOUNT>" + BundleAmount + "</NATIONAL_BUNDLE_AMOUNT><INTERNATIONAL_BUNDLE_CODE></INTERNATIONAL_BUNDLE_CODE><INTERNATIONAL_BUNDLE_AMOUNT></INTERNATIONAL_BUNDLE_AMOUNT><TOPUP_AMOUNT></TOPUP_AMOUNT><TOPUP_CARD_ID></TOPUP_CARD_ID><VOUCHER_PIN></VOUCHER_PIN><CHANNEL_ID>" + ChannelID + "</CHANNEL_ID><EMAIL_ID>" + EmailID + "</EMAIL_ID></DETAILS></ACTIVATE_USIM_PORTIN_BUNDLE_REQUEST></BODY></ENVELOPE>";
                }



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
                ShowPopUpMsg(response + ' ' + ex.Message);
                return response;
                
            }
        }
        public string Activation(String X)
        {
            try
            {
                String strResponse = String.Empty;

                //strResponse = SendRequest(ConfigurationManager.AppSettings.Get("APIURL"), X);
                strResponse = la.LycaAPIRequest(ConfigurationManager.AppSettings.Get("APIURL"), X.Replace("<", "==").Replace(">", "!!"), "ACTIVATE_USIM_PORTIN_BUNDLE");
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
                txtSIMCARD.Text = string.Empty;
                txtZIPCode.Text = string.Empty;
                txtChannelID.Text = string.Empty;
                ddlLanguage.SelectedIndex = 0;

                txtAmountPay.Text = "0.00";
                txtZIPCode.Text = "";
                ddlNetwork.SelectedIndex = 0;
                ddlTariff.SelectedIndex = 0;

            }
            else
            {
                txtSIMCARD.Text = string.Empty;
                txtZIPCode.Text = string.Empty;
                txtChannelID.Text = string.Empty;
                ddlLanguage.SelectedIndex = 0;
                txtAmountPay.Text = "0.00";
                txtZIPCode.Text = "";
                ddlNetwork.SelectedIndex = 0;
                ddlTariff.SelectedIndex = 0;

            }
        }
        public string ActivateSimString(string TariffTypeID, string TariffType, string TariffCode, string email, string TransactionID)
        {
            string request = "";
            try
            {
                string ss = TariffTypeID + "," + TariffType + "," + TariffCode + "," + email + "," + TransactionID + "," + txtSIMCARD.Text.Trim() + "," + txtZIPCode.Text.Trim() + "," + txtAmountPay.Text.Trim() + "," + ddlTariff.SelectedValue;
                Session["RequestData"] = ss;
                string BundleID = TariffTypeID;
                string BundleCode = TariffCode;
                string BundleType = TariffType;
                string BundleAmount = "";
                string EmailID = email;
                string X = "";
                string SIMCARD = Convert.ToString(txtSIMCARD.Text.Trim());
                string ZIPCode = Convert.ToString(txtZIPCode.Text.Trim());
                string Language = "ENGLISH";
                string ChannelID = ConfigurationManager.AppSettings.Get("CHANNEL");
                
                if (BundleType == "National")
                {
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
        private void ShowPopUpMsg(string msg)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("alert('");
            sb.Append(msg.Replace("\n", "\\n").Replace("\r", "").Replace("'", "\\'"));
            sb.Append("');");
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "showalert", sb.ToString(), true);
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

                sb.Append("<p>Preloaded Sim Number  " + UserID + "  Activated successfully on Mobile Number  " + UserName + " <p>");


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
        protected void btnCHECKPreloadedSim_Click(object sender, EventArgs e)
        {
            DataSet dsPreloadedSim = svc.ValidatePreloadedSim(txtSIMNO.Text.Trim());
            if (dsPreloadedSim != null)
            {
                if (dsPreloadedSim.Tables[0].Rows.Count == 0)
                {
                    ShowPopUpMsg("This is No preloaded SIM against with SIM number");
                    return;
                }
                else
                {

                    hddnTariffTypeID.Value = Convert.ToString(dsPreloadedSim.Tables[0].Rows[0]["TariffTypeID"]);
                    hddnTariffID.Value = Convert.ToString(dsPreloadedSim.Tables[0].Rows[0]["ID"]);

                    DivValCHECK.Visible = false;
                    DivVal.Visible = true;
                    txtSIMCARD.Text = txtSIMNO.Text;
                    txtSIMCARD.Enabled = false;
                    DataSet ds = new DataSet();
                    hddnPrepaidSIM.Value = "0";
                    ds = svc.GetSimNetwork(Convert.ToString(txtSIMCARD.Text), "Activate");
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        txtMonth.Text = Convert.ToString(ds.Tables[0].Rows[0]["Months"]);
                        txtMonth.Enabled = false;
                        if (hddnMonths.Value.ToString() == "")
                        {
                            hddnMonths.Value = txtMonth.Text;
                        }
                        // 14 is Deactive Network(due to Error show)

                        if (ds.Tables[0].Rows[0]["VendorID"].ToString() != "14")
                        {
                            ddlNetwork.SelectedValue = Convert.ToInt32(ds.Tables[0].Rows[0]["VendorID"]).ToString();
                            hdnVendorID.Value = Convert.ToString(ds.Tables[0].Rows[0]["VendorID"]);
                            hdnPurchaseID.Value = Convert.ToString(ds.Tables[0].Rows[0]["purchaseid"]);
                            if (Convert.ToInt32(ds.Tables[0].Rows[0]["tariffid"]) > 0)
                            {
                                ddlTariff.DataSource = null;
                                ddlTariff.DataSource = ds.Tables[0];
                                ddlTariff.DataValueField = "tariffid";
                                ddlTariff.DataTextField = "Description";
                                ddlTariff.DataBind();
                                ddlTariff.Attributes.Add("disabled", "disable");
                                div2.Visible = false;
                                div3.Visible = false;
                                hddnPrepaidSIM.Value = "1";
                                hddnMonths.Value = Convert.ToInt32(ds.Tables[0].Rows[0]["Months"]).ToString();
                                hddnTariffCode.Value = Convert.ToString(ds.Tables[0].Rows[0]["tariffCode"]);
                                hddnTariffType.Value = null;
                            }
                            else
                            {
                                ddlTariff.Enabled = false;
                                hddnPrepaidSIM.Value = "0";
                                ShowPopUpMsg("This is No preloaded SIM number is not bound with preloaded tariff");
                                return;
                            }
                        }
                        else
                        {
                            hdnVendorID.Value = "0";
                            hdnPurchaseID.Value = "0";
                            ShowPopUpMsg("This Vendor belongs to deactivated network");
                            return;
                        }
                    }
                    else
                    {
                        hdnVendorID.Value = "0";
                        hdnPurchaseID.Value = "0";
                        ShowPopUpMsg("There is some connection problem(s)");
                    }
                }
            }   
        }
    }
}
