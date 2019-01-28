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

namespace ENK
{
    public partial class SubscriberSim : System.Web.UI.Page
    {
        Service1Client svc = new Service1Client();
        string RequestRes = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                try
                {
                    divahannel.Visible = false;
                    divLanguage.Visible = false;
                    // 1 $ Add Regulatery 
                    DateTime today = DateTime.Today;
                    // DateTime date = new DateTime(2017, 07, 20);
                    DataSet dsReg = svc.GetRegulatery();
                    DateTime date = Convert.ToDateTime(dsReg.Tables[0].Rows[0]["StartDate"]);
                    if (date <= today)
                    {
                        txtRegulatry.Text = "1";

                    }
                    else
                    {

                        txtRegulatry.Text = "0";
                    }
                }
                catch (Exception ex)
                {

                }

            }
        }

        public string Activation(String X)
        {
            try
            {
                String strResponse = String.Empty;

                strResponse = SendRequest("http://192.30.220.110:2244", X);
                //strResponse = SendRequest("http://192.30.216.110:2244", X);//"<create-directory-number version=\"1\"> <authentication> <username>admin.puneet</username> <password>stay@9229</password> </authentication> <directory-number>" + DIDNumber + "</directory-number> <directory-number-vendor>toclionly</directory-number-vendor> </create-directory-number>"

                //strResponse = SendRequest("http://83.137.7.3:4006", X);//"<create-directory-number version=\"1\"> <authentication> <username>admin.puneet</username> <password>stay@9229</password> </authentication> <directory-number>" + DIDNumber + "</directory-number> <directory-number-vendor>toclionly</directory-number-vendor> </create-directory-number>"

                //Response.Write(strResponse);
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
    
        public string ActivateSim(string TariffTypeID, string TariffType, string TariffCode, string email, string TransactionID,string TariffAmount)
        {
            string response = "";
            try
            {
                string BundleID = TariffTypeID;
                string BundleCode = TariffCode;
                string BundleType = TariffType;
                string BundleAmount = TariffAmount;

               


                string EmailID = email;

                string X = "";

                string activation = "1";
                string SIMCARD = Convert.ToString(txtSIMCARD.Text.Trim());
                string ZIPCode = Convert.ToString(txtZIPCode.Text.Trim());
                string Language = "ENGLISH";// Convert.ToString(ddlLanguage.SelectedItem.Text);
                string ChannelID = ConfigurationManager.AppSettings.Get("TXN_SERIES");// Convert.ToString(txtChannelID.Text.Trim());


                //string X = "<ENVELOPE><HEADER><TRANSACTION_ID>L00001</TRANSACTION_ID><ENTITY>ENK</ENTITY><CHANNEL_REFERENCE>ENK</CHANNEL_REFERENCE></HEADER><BODY><ICC_ID>8919601000107857738</ICC_ID><P_MSISDN>18622708160</P_MSISDN><ACCOUNT_NUMBER>260642110361628</ACCOUNT_NUMBER><PASSWORD_PIN>10361628</PASSWORD_PIN><ZIP_CODE>07102</ZIP_CODE><PREFERRED_LANGUAGE>English</PREFERRED_LANGUAGE><NATIONAL_BUNDLE_CODE></NATIONAL_BUNDLE_CODE><NATIONAL_BUNDLE_AMOUNT></ NATIONAL_BUNDLE_AMOUNT><VOUCHER_PIN></VOUCHER_PIN><EMAIL_ID></ EMAIL_ID><CHANNEL_ID>ENK</CHANNEL_ID></BODY></ENVELOPE>";                   
                //string X = "<ENVELOPE><HEADER><TRANSACTION_ID>L00001</TRANSACTION_ID><ENTITY>ENK</ENTITY><CHANNEL_REFERENCE>ENK</CHANNEL_REFERENCE></HEADER><BODY><ACTIVATE_USIM_PORTIN_BUNDLE_REQUEST><DETAILS><ICC_ID>" + SIMCARD + "</ICC_ID><ZIP_CODE>" + ZIPCode + "</ZIP_CODE><PREFERRED_LANGUAGE>" + Language + "</PREFERRED_LANGUAGE><P_MSISDN>" + PHONETOPORT + "</P_MSISDN><ACCOUNT_NUMBER>" + ACCOUNT + "</ACCOUNT_NUMBER><PASSWORD_PIN>" + PIN + "</PASSWORD_PIN><NATIONAL_BUNDLE_CODE>" + NATIONAL_BUNDLE_CODE + "</NATIONAL_BUNDLE_CODE><NATIONAL_BUNDLE_AMOUNT>" + NATIONAL_BUNDLE_AMOUNT + "</NATIONAL_BUNDLE_AMOUNT><INTERNATIONAL_BUNDLE_CODE>" + INTERNATIONAL_BUNDLE_CODE + "</INTERNATIONAL_BUNDLE_CODE><INTERNATIONAL_BUNDLE_AMOUNT>" + INTERNATIONAL_BUNDLE_AMOUNT + "</INTERNATIONAL_BUNDLE_AMOUNT><TOPUP_AMOUNT>" + TOPUP_AMOUNT + "</TOPUP_AMOUNT><TOPUP_CARD_ID>" + TOPUP_CARD_ID + "</TOPUP_CARD_ID><VOUCHER_PIN>"+VOUCHER_PIN+"</VOUCHER_PIN><CHANNEL_ID>ENK</CHANNEL_ID><EMAIL_ID>" + EmailAddress + "</EMAIL_ID></DETAILS></ACTIVATE_USIM_PORTIN_BUNDLE_REQUEST></BODY></ENVELOPE>";
                //string X = "<ENVELOPE><HEADER><TRANSACTION_ID>L00001</TRANSACTION_ID><ENTITY>ENK</ENTITY><CHANNEL_REFERENCE>ENK</CHANNEL_REFERENCE></HEADER><BODY><ACTIVATE_USIM_PORTIN_BUNDLE_REQUEST><DETAILS><ICC_ID>8919601000107857738</ICC_ID><ZIP_CODE>07102</ZIP_CODE><PREFERRED_LANGUAGE>ENGLISH</PREFERRED_LANGUAGE><P_MSISDN>18622708160</P_MSISDN><ACCOUNT_NUMBER>260642110361628</ACCOUNT_NUMBER><PASSWORD_PIN>10361628</PASSWORD_PIN><NATIONAL_BUNDLE_CODE></NATIONAL_BUNDLE_CODE><NATIONAL_BUNDLE_AMOUNT></NATIONAL_BUNDLE_AMOUNT><INTERNATIONAL_BUNDLE_CODE></INTERNATIONAL_BUNDLE_CODE><INTERNATIONAL_BUNDLE_AMOUNT></INTERNATIONAL_BUNDLE_AMOUNT><TOPUP_AMOUNT></TOPUP_AMOUNT><TOPUP_CARD_ID></TOPUP_CARD_ID><VOUCHER_PIN></VOUCHER_PIN><CHANNEL_ID>ENK</CHANNEL_ID><EMAIL_ID>test@test.com</EMAIL_ID></DETAILS></ACTIVATE_USIM_PORTIN_BUNDLE_REQUEST></BODY></ENVELOPE>";
                //string X = "<ENVELOPE><HEADER><TRANSACTION_ID>L00001</TRANSACTION_ID><ENTITY>ENK</ENTITY><CHANNEL_REFERENCE>ENK</CHANNEL_REFERENCE></HEADER><BODY><ACTIVATE_USIM_PORTIN_BUNDLE_REQUEST><DETAILS><ICC_ID>" + SIMCARD + "</ICC_ID><ZIP_CODE>" + ZIPCode + "</ZIP_CODE><PREFERRED_LANGUAGE>" + Language + "</PREFERRED_LANGUAGE><P_MSISDN></P_MSISDN><ACCOUNT_NUMBER></ACCOUNT_NUMBER><PASSWORD_PIN></PASSWORD_PIN><NATIONAL_BUNDLE_CODE></NATIONAL_BUNDLE_CODE><NATIONAL_BUNDLE_AMOUNT></NATIONAL_BUNDLE_AMOUNT><INTERNATIONAL_BUNDLE_CODE></INTERNATIONAL_BUNDLE_CODE><INTERNATIONAL_BUNDLE_AMOUNT></INTERNATIONAL_BUNDLE_AMOUNT><TOPUP_AMOUNT></TOPUP_AMOUNT><TOPUP_CARD_ID></TOPUP_CARD_ID><VOUCHER_PIN></VOUCHER_PIN><CHANNEL_ID>" + ChannelID + "</CHANNEL_ID><EMAIL_ID></EMAIL_ID></DETAILS></ACTIVATE_USIM_PORTIN_BUNDLE_REQUEST></BODY></ENVELOPE>";

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
                return response;
                //ShowPopUpMsg(ex.Message);
            }
        }

        protected void btnSubscriber_Click(object sender, EventArgs e)
        {

            try
            {
                GetClientIPAddress();
            }
            catch {}
            

            
            int Regulatery = 0;
            int distr = 1;// Convert.ToInt32(Session["DistributorID"]);
            int clnt = 3;//Convert.ToInt32(Session["ClientTypeID"]);
            string simnumber = txtSIMCARD.Text.Trim();
            string transact = "";
            string TariffAmount = "";
            Boolean ValidSim = false;
            string erormsg = "";
            try
            {
                string ss = "";
                ss = "Simnumber- " + simnumber + "|" + "Zipcode- " + txtZIPCode.Text + "|" + "Customer name- " + txtCustomerName.Text + "|" + "address- " + txtAddress.Text + "|" + "Alternate Mobile number- " + txtAlternateNumber.Text + "|" + "Email- " + txtEmail.Text;
                Log2(ss, "Subscriber Activate SIM Information");
                Log2("", "split");
                DataSet ds = svc.CheckSimActivationService(distr, clnt, simnumber,"Activate");
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
                            hddnTariffTypeID.Value = Convert.ToString(ds.Tables[0].Rows[0]["TariffTypeID"]);
                            hddnTariffType.Value = Convert.ToString(ds.Tables[0].Rows[0]["TariffType"]);
                            hddnTariffCode.Value = Convert.ToString(ds.Tables[0].Rows[0]["TariffCode"]);
                            hddnTariffAmount.Value = Convert.ToString(ds.Tables[0].Rows[0]["Amount"]);
                           TariffAmount = hddnTariffAmount.Value;
                            hddnTariffID.Value = Convert.ToString(ds.Tables[0].Rows[0]["ID"]);
                            hddnMonths.Value = Convert.ToString(ds.Tables[0].Rows[0]["Months"]);

                            //  Add Rerulatry 1 $ from 1 jully 2017
                           
                            DateTime today = DateTime.Today;
                            // DateTime date = new DateTime(2017, 07, 20);
                            DataSet dsReg = svc.GetRegulatery();
                            DateTime date = Convert.ToDateTime(dsReg.Tables[0].Rows[0]["StartDate"]);
                            if (date <= today)
                            {
                                
                                double Amt = 0.0;

                                if (hddnMonths.Value == "1")
                                {
                                    Regulatery = 1;
                                    txtRegulatry.Text = "1";
                                    Amt = Convert.ToDouble(hddnTariffAmount.Value) + 1;
                                    
                                }
                                else if (hddnMonths.Value == "2")
                                {
                                    Regulatery = 2;
                                    txtRegulatry.Text = "2";
                                    Amt = Convert.ToDouble(hddnTariffAmount.Value) + 2;
                                }
                                else if (hddnMonths.Value == "3")
                                {
                                    Regulatery = 3;

                                    txtRegulatry.Text = "3";
                                    Amt = Convert.ToDouble(hddnTariffAmount.Value) + 3;
                                }

                                
                                TariffAmount = Convert.ToString(Amt); ;
                            }
                            else
                            {
                                Regulatery = 0;
                                TariffAmount = hddnTariffAmount.Value;
                            }




                        }
                        else
                        {
                            Log2("Subscriber Activate Sim Not Ready For Activation", "Reason");

                            Log2(ss, "Request");
                            Log2(simnumber, "Simnumber");
                            Log2(TariffAmount, "Amount");
                            Log2(hddnTariffCode.Value, "PLan");

                            Log2("", "split");


                        
                        
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
            

            string resp = ActivateSim(hddnTariffTypeID.Value, hddnTariffType.Value, hddnTariffCode.Value, txtEmail.Text.Trim(), transact, TariffAmount);
            //resp = "<ENVELOPE><HEADER><ERROR_CODE>0</ERROR_CODE><ERROR_DESC>Success</ERROR_DESC></HEADER><BODY><ACTIVATE_USIM_PORTIN_BUNDLE_RESPONSE><ALLOCATED_MSISDN>19736871250</ALLOCATED_MSISDN><PORTIN_REFERENCE_NUMBER/></ACTIVATE_USIM_PORTIN_BUNDLE_RESPONSE></BODY></ENVELOPE>";
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
                                SendMail(txtEmail.Text.Trim(), "Lycamobile Sim Activation", ALLOCATED_MSISDN, txtSIMCARD.Text.Trim(), "");

                                SPayment sp = new SPayment();
                                sp.ChargedAmount = Convert.ToDecimal(TariffAmount);
                                sp.PaymentType = 4;
                                sp.PayeeID = 1;
                                sp.PaymentFrom = 8;
                                sp.ActivationType = 6;
                                sp.ActivationStatus = 15;
                                sp.ActivationVia = 17;
                                sp.ActivationResp = resp;
                                sp.ActivationRequest = RequestRes;
                                sp.TransactionId = transact;
                                sp.TariffID = Convert.ToInt32(hddnTariffID.Value);
                                sp.ALLOCATED_MSISDN = ALLOCATED_MSISDN;

                                sp.PaymentMode = "Subscriber Activation";
                                sp.TransactionStatus = "Success";
                                sp.TransactionStatusId = 24;

                                sp.CusName = txtCustomerName.Text.Trim();
                                sp.Address = txtAddress.Text.Trim();
                                sp.Mobile = txtAlternateNumber.Text.Trim();
                                sp.EmailID = txtEmail.Text.Trim();

                                sp.Regulatery = Regulatery;

                                int dist = 1;
                                int loginID = 1;
                                string sim = txtSIMCARD.Text.Trim();
                                string zip = txtZIPCode.Text.Trim();
                                string Language = "ENGLISH";
                                string ChannelID = ConfigurationManager.AppSettings.Get("TXN_SERIES");
                                try
                                {
                                    int s = svc.InsertSubscriberActivationDetailService(dist, loginID, sim, zip, Language, ChannelID, sp);
                                    WriteLog(dist, loginID, sim, zip, Language, ChannelID, sp, "Record Save Success when sim activated In Subscriber Case");
                                }
                                catch (Exception ex)
                                {
                                    WriteLog(dist, loginID, sim, zip, Language, ChannelID, sp, "Record Save fail when sim activated In Subscriber Case");
                                }
                                ShowPopUpMsg("SIM activation done successfully\n with Mobile Number - " + ALLOCATED_MSISDN);
                                resetControls(1);
                            }
                            else
                            {
                                SaveDataSubscriber(resp, 16, transact, TariffAmount);
                                ShowPopUpMsg("SIM activation Fail \n Please Try Again");
                            }
                        }
                        else
                        {
                            SaveDataSubscriber(resp, 16, transact, TariffAmount);
                            ShowPopUpMsg("SIM activation Fail \n Please Try Again");
                        }
                    }
                    else
                    {
                        SaveDataSubscriber(resp, 16, transact, TariffAmount);
                        ShowPopUpMsg("SIM activation Fail \n Please Try Again");
                    }
                }
                catch (Exception ex)
                {
                    SaveDataSubscriber(resp, 16, transact, TariffAmount);
                    ShowPopUpMsg("SIM activation Fail \n Please Try Again");
                    //ShowPopUpMsg(ex.Message + "\n" + resp);
                }

            }
            else
            {
                SaveDataSubscriber(resp, 16, transact, TariffAmount);
                ShowPopUpMsg("SIM activation Fail \n Please Try Again");
            }


             
        }

        public void SaveDataSubscriber(string resp, int status, string TransactionID,string amount)
        {
            int Regulatery = 0;
            DateTime today = DateTime.Today;
            // DateTime date = new DateTime(2017, 07, 20);
            DataSet dsReg = svc.GetRegulatery();
            DateTime date = Convert.ToDateTime(dsReg.Tables[0].Rows[0]["StartDate"]);
            if (date <= today)
            {
                if (hddnMonths.Value == "1")
                {
                    Regulatery = 1;
                }
                else if (hddnMonths.Value == "2")
                {
                    Regulatery = 2;
                }
                else if (hddnMonths.Value == "3")
                {
                    Regulatery = 3;
                }
            }
            else { Regulatery = 0; }

            SPayment sp = new SPayment();
            sp.ChargedAmount = Convert.ToDecimal(amount);
            sp.PaymentType = 4;
            sp.PayeeID = 1;
            sp.PaymentFrom = 8;
            sp.ActivationType = 6;
            sp.ActivationStatus = status;
            sp.ActivationVia = 17;
            sp.ActivationResp = resp;
            sp.ActivationRequest = RequestRes;
            sp.TariffID = Convert.ToInt32(hddnTariffID.Value);
            sp.ALLOCATED_MSISDN = "";
            sp.TransactionId = TransactionID;

            sp.PaymentMode = "Subscriber Activation";
            sp.TransactionStatus = "Fail";
            sp.TransactionStatusId = 25;

            sp.CusName = txtCustomerName.Text.Trim();
            sp.Address = txtAddress.Text.Trim();
            sp.Mobile = txtAlternateNumber.Text.Trim();
            sp.EmailID = txtEmail.Text.Trim();
            sp.Regulatery = Regulatery;
            int dist = 1;
            int loginID = 1;
            string sim = txtSIMCARD.Text.Trim();
            string zip = txtZIPCode.Text.Trim();
            string Language = "ENGLISH";
            string ChannelID = ConfigurationManager.AppSettings.Get("TXN_SERIES");

            try
            {
                int s = svc.InsertSubscriberActivationDetailService(dist, loginID, sim, zip, Language, ChannelID, sp);
                WriteLog(dist, loginID, sim, zip, Language, ChannelID, sp, "Record Save Success when sim not activated In Subscriber Case");
            }
            catch (Exception ex)
            {
                WriteLog(dist, loginID, sim, zip, Language, ChannelID, sp, "Record Save Fail when sim not activated In Subscriber Case");
            }
            //ShowPopUpMsg("SIM activation Fail");
            resetControls(1);
        }

        private void resetControls(int condition)
        {
            if (condition == 1)
            {
                txtSIMCARD.Text = string.Empty;
                txtZIPCode.Text = string.Empty;
                txtAddress.Text = "";
                txtAlternateNumber.Text = "";
                txtCustomerName.Text = "";
                txtEmail.Text = "";


            }

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

            logdata.Append(sp.CusName);
            logdata.Append(sp.Address);
            logdata.Append(sp.EmailID);
            logdata.Append(sp.Mobile);

            string data = logdata.ToString();
            Log2(data, msgg);
            Log2("", "split");
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
            //string filename = "lycalog.txt";
            //string strPath = Server.MapPath("Log") + "/" + filename;
            //string root = "http://" + HttpContext.Current.Request.Url.Host + HttpContext.Current.Request.ApplicationPath + "/Log/" + filename;

            //if (File.Exists(strPath))
            //{
            //    StreamWriter sw = new StreamWriter(strPath, true, Encoding.Unicode);
            //    if (condition != "split")
            //    {                    
            //        sw.WriteLine(condition + "  " + DateTime.Now.ToString());                    
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

        public void Log1(string ss, string condition)
        {
            try
            {
                //string filename = "ActivationLog.txt";
                string filename = "ActivationLog.txt";
                string strPath = Server.MapPath("Log") + "/" + filename;
                string root = "http://" + HttpContext.Current.Request.Url.Host + HttpContext.Current.Request.ApplicationPath + "/Log/" + filename;

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

        public void Log2(string ss, string condition)
        {
            try
            {
                string filename = "Subscriberlog.txt";
                string strPath = Server.MapPath("Log") + "/" + filename;
                string root = "http://" + HttpContext.Current.Request.Url.Host + HttpContext.Current.Request.ApplicationPath + "/Log/" + filename;

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

                        sw.WriteLine("-----------------------------------------------------------------------------------------------");
                        sw.Close();
                    }
                }
            }
            catch (Exception ex)
            {

            }

        }
     
        protected void GetClientIPAddress()
        {
            try
            {
                System.Web.HttpContext context = System.Web.HttpContext.Current;

                string IP1 = HttpContext.Current.Request.UserHostAddress;
                string IP2 = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                //To get the IP address of the machine and not the proxy use the following code 
                string IP3 = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (!string.IsNullOrEmpty(IP3))
                {
                    string[] addresses = IP3.Split(',');
                    if (addresses.Length != 0)
                    {

                    }
                }
                if (IP3 == null)
                {
                    IP3 = "";
                }


                string Browser = "";
                string IsBrowser = "";
                IsBrowser = context.Request.Browser.IsMobileDevice.ToString();
                string Browser1 = Request.Browser.Browser + " " + Request.Browser.Version;
                if (IsBrowser == "True")
                {
                    Browser = "Mobile";
                }
                else
                {
                    Browser = "Web";
                }

                string BrowserDetail = "";
                if (context.Request.ServerVariables["HTTP_X_WAP_PROFILE"] != null)
                {
                    BrowserDetail = BrowserDetail + "|" + context.Request.ServerVariables["HTTP_X_WAP_PROFILE"];
                }

                if (context.Request.ServerVariables["HTTP_ACCEPT"] != null &&
                    context.Request.ServerVariables["HTTP_ACCEPT"].ToLower().Contains("wap"))
                {
                    BrowserDetail = BrowserDetail + "|" + context.Request.ServerVariables["HTTP_ACCEPT"];
                }

                if (context.Request.ServerVariables["HTTP_USER_AGENT"] != null)
                {
                    BrowserDetail = BrowserDetail + "|" + context.Request.ServerVariables["HTTP_USER_AGENT"];
                }

                //string ss = Request.Browser.ToString();
                //string ss2 = Request.Browser.Version.ToString();

                SLoginHistory sl = new SLoginHistory();

                sl.IpAddress1 = IP1;
                sl.IpAddress2 = IP2;
                sl.IpAddress3 = IP3;
                sl.BrowserName = Browser;
                sl.Browser1 = Browser1;
                sl.Location = "Subscriber";
                sl.IpDetail = BrowserDetail;
                sl.LoginID = 1;
                sl.LoginTime = DateTime.Now;
                svc.InsertLoginHistoryService(sl);
            }
            catch (Exception ex)
            {

            }
            // return context.Request.ServerVariables["REMOTE_ADDR"];
        }
        public void SendMail(string SendTo, string Subject, string MobileNumber, string SimNumber, string pass)
        {
            try
            {
                string LoginUrl = "http://" + HttpContext.Current.Request.Url.Host + HttpContext.Current.Request.ApplicationPath + "/Login.aspx";
                string LogoUrl = "http://" + HttpContext.Current.Request.Url.Host + HttpContext.Current.Request.ApplicationPath + "/img/logo.png";
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

                sb.Append("<p>Sim Number " + SimNumber + "  Activated successfully on Mobile Number  " + MobileNumber + "<p>");


                sb.Append("<p>");
                sb.Append("<br/>");
                sb.Append("<p> Activation successfully, If you have any issue  with Recharge please contact Lycamobile directly at +1-845-301-1633 / +1-866-277-0024 <p>");


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
    }
}