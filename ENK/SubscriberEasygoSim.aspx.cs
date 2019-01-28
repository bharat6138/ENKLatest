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

namespace ENK
{
    public partial class SubscriberEasygoSim : System.Web.UI.Page
    {
        Service1Client svc = new Service1Client();
        string RequestRes = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                try
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

                    divahannel.Visible = false;
                    divLanguage.Visible = false;
                }
                catch (Exception ex)
                {

                }

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
            catch
            {

            }
            // return context.Request.ServerVariables["REMOTE_ADDR"];
        }

        protected void btnSubscriber_Click(object sender, EventArgs e)
        {
            try
            {
                GetClientIPAddress();
            }
            catch { }
            ///-----------------------------------EasyGo------------------
            ///

            ENK.net.emida.ws.webServicesService ws = new webServicesService();

            int distr = 1;// Convert.ToInt32(Session["DistributorID"]);
            int clnt = 3;//Convert.ToInt32(Session["ClientTypeID"]);
            string simnumber = txtSIMCARD.Text.Trim();
            string City = txtCity.Text.Trim();
            string transact = "";
            string TariffAmount = "";
            Boolean ValidSim = false;
            string erormsg = "";


            try
            {
                string ss = "";
                ss = "Simnumber- " + simnumber + "|" + "Zipcode- " + txtZIPCode.Text + "|" + "Customer name- " + txtCustomerName.Text + "|" + "address- " + txtAddress.Text + "|" + "Alternate Mobile number- " + txtAlternateNumber.Text + "|" + "Email- " + txtEmail.Text + "|" + "City- " + txtCity.Text;
                Log2(ss, "Subscriber Activate EasyGo SIM Information");
                Log2("", "split");
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
                            hddnTariffTypeID.Value = Convert.ToString(ds.Tables[0].Rows[0]["TariffTypeID"]);
                            hddnTariffType.Value = Convert.ToString(ds.Tables[0].Rows[0]["TariffType"]);
                            hddnTariffCode.Value = Convert.ToString(ds.Tables[0].Rows[0]["TariffCode"]);
                            hddnTariffAmount.Value = Convert.ToString(ds.Tables[0].Rows[0]["Amount"]);
                            TariffAmount = hddnTariffAmount.Value;
                            hddnTariffID.Value = Convert.ToString(ds.Tables[0].Rows[0]["ID"]);

                            hddnMonths.Value = Convert.ToString(ds.Tables[0].Rows[0]["Months"]);
                        }
                        else
                        {
                            Log2("Subscriber Activate  EasyGo Sim Not Ready For Activation", "Reason");

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






            string InvoiceNo = DateTime.Now.ToString().GetHashCode().ToString("X");
            InvoiceNo = "AC" + InvoiceNo;
            string ss2 = ws.GetPinProductsForActivation("01", "3756263", "1234", hddnTariffCode.Value, InvoiceNo, "1");
            string resp = "";

            StringReader theReader = new StringReader(Convert.ToString(ss2));
            DataSet theDataSet = new DataSet();
            theDataSet.ReadXml(theReader);
            if (theDataSet.Tables.Count > 0)
            {

                DataTable dt = new DataTable();
                if (theDataSet.Tables.Count > 1)
                {
                    dt = theDataSet.Tables[1];
                }
                else
                {
                    SaveDataSubscriber(ss2, 16, transact, TariffAmount);
                    //WriteLog(dist, loginID, sim, zip, Language, ChannelID, sp, ss2);
                    ShowPopUpMsg("NO RECORDS FOUND.");
                    return;
                }



                if (dt.Rows.Count > 0 && dt.Columns.Contains("PinProductId"))
                {
                    string PINid = dt.Rows[0]["PinProductId"].ToString();

                    string ProductDescription = dt.Rows[0]["PinProductDescription"].ToString();
                    if (PINid != "")
                    {

                        //resp = ws.LocusActivateGSMsim("3756263", PINid, "1234", hddnTariffCode.Value, "1", InvoiceNo, "01", "305", simnumber, City, txtZIPCode.Text);
                        resp = ws.LocusActivateGSMsim("3756263", PINid, "1234", hddnTariffCode.Value, "1", InvoiceNo, "01", "305", simnumber, City, txtZIPCode.Text);

                        //resp = ws.LocusActivateGSMsim("3756263", PINid, "1234", "93000030", "1", InvoiceNo, "01", "305", simnumber, City, zip);



                        StringReader Reader = new StringReader(resp);
                        DataSet ds = new DataSet();
                        ds.ReadXml(Reader);
                        string Currency = Convert.ToString("$");
                        // Save Record ProductMaster

                        int NetworkID = 16;// EasyGo
                        svc.SaveProductMaster(Convert.ToInt32(NetworkID), Convert.ToInt32(hddnTariffID.Value), ProductDescription, ProductDescription, Currency, TariffAmount, Convert.ToInt32(1));


                        if (ds.Tables.Count > 0)
                        {
                            DataTable dtMsg = ds.Tables[0];
                            if (dtMsg.Rows.Count > 0)
                            {
                                string ResponseCode = dtMsg.Rows[0]["ResponseCode"].ToString();
                                string ResponseMessage = dtMsg.Rows[0]["ResponseMessage"].ToString();
                                if (ResponseCode == "00")
                                {
                                    string ALLOCATED_MSISDN = dtMsg.Rows[0]["min"].ToString(); ;
                                    SendMail(txtEmail.Text.Trim(), "EasyGo Sim Activation", ALLOCATED_MSISDN, txtSIMCARD.Text.Trim(), "");

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

                                    sp.PaymentMode = "Subscriber EasyGo Activation";
                                    sp.TransactionStatus = "Success";
                                    sp.TransactionStatusId = 24;

                                    sp.CusName = txtCustomerName.Text.Trim();
                                    sp.Address = txtAddress.Text.Trim();
                                    sp.Mobile = txtAlternateNumber.Text.Trim();
                                    sp.EmailID = txtEmail.Text.Trim();

                                    int dist = 1;
                                    int loginID = 1;
                                    string sim = txtSIMCARD.Text.Trim();
                                    string zip = txtZIPCode.Text.Trim();


                                    string Language = "ENGLISH";
                                    string ChannelID = ConfigurationManager.AppSettings.Get("TXN_SERIES");


                                    svc.SaveTransactionDetails(Convert.ToInt32(NetworkID), Convert.ToInt32(hddnTariffID.Value), "10", PINid, simnumber, InvoiceNo, TariffAmount, Currency, City, txtZIPCode.Text, "305", 1, TariffAmount);

                                    try
                                    {
                                        int s = svc.InsertSubscriberActivationDetailService(dist, loginID, sim, zip, Language, ChannelID, sp);
                                        WriteLog(dist, loginID, sim, zip, Language, ChannelID, sp, "Record Save Success when EasyGo sim activated In Subscriber Case");
                                    }
                                    catch (Exception ex)
                                    {
                                        WriteLog(dist, loginID, sim, zip, Language, ChannelID, sp, "Record Save fail when EasyGo sim activated In Subscriber Case");
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

        public void SaveDataSubscriber(string resp, int status, string TransactionID, string amount)
        {
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

            sp.PaymentMode = "Subscriber EasyGo Activation";
            sp.TransactionStatus = "Fail";
            sp.TransactionStatusId = 25;

            sp.CusName = txtCustomerName.Text.Trim();
            sp.Address = txtAddress.Text.Trim();
            sp.Mobile = txtAlternateNumber.Text.Trim();
            sp.EmailID = txtEmail.Text.Trim();

            int dist = 1;
            int loginID = 1;
            string sim = txtSIMCARD.Text.Trim();
            string zip = txtZIPCode.Text.Trim();
            string Language = "ENGLISH";
            string ChannelID = ConfigurationManager.AppSettings.Get("TXN_SERIES");

            try
            {
                int s = svc.InsertSubscriberActivationDetailService(dist, loginID, sim, zip, Language, ChannelID, sp);
                WriteLog(dist, loginID, sim, zip, Language, ChannelID, sp, "Record Save Success when EasyGo sim not activated In Subscriber Case");
            }
            catch (Exception ex)
            {
                WriteLog(dist, loginID, sim, zip, Language, ChannelID, sp, "Record Save Fail when EasyGo sim not activated In Subscriber Case");
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
            Log1(data, msgg);
            Log1("", "split");
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

                sb.Append("<p>Sim Number  " + SimNumber + " Activated successfully on Mobile Number  " + MobileNumber + " <p>");


                sb.Append("<p>");
                sb.Append("<br/>");

                sb.Append("<p> Activation successfully, If you have any issue  with Recharge please contact H2O WIRELESS. <p>");

                sb.Append("<p> Dealer Hotline 1-800-939-1261 <p>");
                sb.Append("<p> We are open 7 days a week from 9AM EST to 12AM EST Monday - Friday, and 9AM EST to 11PM EST on weekends. <p>");
                sb.Append("<p> H2O GSM Support 1-800-643-4926 <p>");
                sb.Append("<p>Email: customercare@h2owirelessnow.com <p>");


                sb.Append("<br/>");
                sb.Append("<p>Sincerely,");
                sb.Append("<p>" + ConfigurationManager.AppSettings.Get("COMPANY_NAME")+ "</p>");
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