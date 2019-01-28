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


namespace ENK
{
    public partial class SubscriberH2OPortIn : System.Web.UI.Page
    {
        Service1Client svc = new Service1Client();
        string RequestRes = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                try
                {

                   // ENK.net.emida.ws.webServicesService ws = new webServicesService();
                    // string ss1 = ws.Login2("01", "clerkterst", "clerk1234", "1");
                   // string ss1 = ws.Login2("01", "A&HPrepaid", "95222", "1");

                    divahannel.Visible = false;
                    divLanguage.Visible = false;
                }
                catch (Exception ex)
                {

                }

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
                sl.LoginID =1;
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
            
            int distr = 1;
            int clnt = 3;
            string simnumber = txtSIMCARD.Text.Trim();
            string transact = "";
            DataTable dtTrans = svc.GetTransactionIDService();

            int id = Convert.ToInt32(dtTrans.Rows[0]["TRANSACTIONID"]);
            transact = id.ToString("00000");
            transact = ConfigurationManager.AppSettings.Get("TXN_SERIES") + transact;
            Boolean ValidSim = false;
            string TariffAmount = "";
             string TariffPlan = "";
            string Network="H2O";
            string erormsg = "";
            try
            {
                string ss = "";
                ss = "Simnumber- " + simnumber + "|" + "Zipcode- " + txtZIPCode.Text + "|" + "Pin- " + txtPIN.Text + "|" + "Account- " + txtACCOUNT.Text + "|" + "Phone to port- " + txtPHONETOPORT.Text + "|" + "Customer name- " + txtCustomerName.Text + "|" + "address- " + txtAddress.Text + "|" + "Email- " + txtEmailAddress.Text;
                ss = ss + "Current Service Provider Name- " + txtServiceProvider.Text + "|" + "State- " + txtState.Text + "|" + "City- " + txtCity.Text + "|" + "Customer 1st and Last Name- " + txtCustomerName.Text;
                
                Log1(ss, "Subscriber H2O PORTIN Information");
                Log1("", "split");
                DataSet ds = svc.CheckSimPortINService(distr, clnt, simnumber);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        erormsg = Convert.ToString(ds.Tables[0].Rows[0][0]);
                        if (erormsg == "Ready to Activation")
                        {
                          //  RequestRes = ss;

                            ValidSim = true;
                            hddnTariffTypeID.Value = Convert.ToString(ds.Tables[0].Rows[0]["TariffTypeID"]);
                            hddnTariffType.Value = Convert.ToString(ds.Tables[0].Rows[0]["TariffType"]);
                            hddnTariffCode.Value = Convert.ToString(ds.Tables[0].Rows[0]["TariffCode"]);
                            hddnTariffAmount.Value = Convert.ToString(ds.Tables[0].Rows[0]["Amount"]);
                            TariffAmount = hddnTariffAmount.Value;
                            hddnTariffID.Value = Convert.ToString(ds.Tables[0].Rows[0]["ID"]);
                            TariffPlan = Convert.ToString(ds.Tables[0].Rows[0]["TrariffPlan"]);
                            hddnMonths.Value = Convert.ToString(ds.Tables[0].Rows[0]["Months"]);
                        }
                        else
                        {
                            Log1("Sim Not Ready For H2O PortIN", "Reason");
                            Log1(ss, "Detail");
                            Log1(simnumber, "Simnumber");
                            Log1(txtPHONETOPORT.Text, "Phone to port");
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




            string InvoiceNo = DateTime.Now.ToString().GetHashCode().ToString("X");


            // ENK.net.emida.ws.webServicesService ws = new webServicesService();
            // string ss1 = ws.LycaPortinPin("01", "3756263", "1234", hddnTariffCode.Value, txtSIMCARD.Text, txtPHONETOPORT.Text, txtACCOUNT.Text, txtPIN.Text, txtZIPCode.Text, "1", txtEmailAddress.Text, "$10", txtAmountPay.Text, InvoiceNo, "1");


            try
            {

                // SendMail(txtEmailAddress.Text.Trim(), "Sim PortIn", "", txtSIMCARD.Text.Trim(), "");
                SendMailH20AndEasyGo(txtEmailAddress.Text.Trim(), "H2O Sim PortIn", "", txtSIMCARD.Text.Trim(), "", Network, TariffPlan, TariffAmount);


                SPayment sp = new SPayment();
                sp.ChargedAmount = Convert.ToDecimal(TariffAmount);
                sp.PaymentType = 5;
                sp.PayeeID = distr;
                sp.PaymentFrom = 9;
                sp.ActivationType = 7;
                sp.ActivationStatus = 15;
                sp.ActivationVia = 17;
                sp.ActivationResp = "";
                sp.ActivationRequest = RequestRes;
                sp.TariffID = Convert.ToInt32(hddnTariffID.Value);
                sp.ALLOCATED_MSISDN = "";
                sp.TransactionId = transact;

                sp.PaymentMode = "Subscriber H2O PortIn";
                sp.TransactionStatusId = 24;
                sp.TransactionStatus = "Success";

                int dist = 1;// Convert.ToInt32(Session["DistributorID"]);
                int loginID = 1;// Convert.ToInt32(Session["LoginID"]);
                string sim = txtSIMCARD.Text.Trim();
                string zip = txtZIPCode.Text.Trim();
                string Language = "ENGLISH";
                string ChannelID = ConfigurationManager.AppSettings.Get("TXN_SERIES");
                try
                {
                    int s = svc.UpdateAccountBalanceService(dist, loginID, sim, zip, Language, ChannelID, sp);

                    WriteLog(dist, loginID, sim, zip, Language, ChannelID, sp, "Record Save Success when H2O sim PortIn Success In Subscriber Case");
                }
                catch (Exception ex)
                {
                    WriteLog(dist, loginID, sim, zip, Language, ChannelID, sp, "Record Save Fail when H2O sim PortIn Success  In Subscriber Case");

                }
                ShowPopUpMsg("H2O PORTIN Request submitted Successfully\n With Sim Number - " + txtSIMCARD.Text);
                resetControls(1);


            }
            catch (Exception ex)
            {
                SaveDataCompany(txtSIMCARD.Text, 16, transact, TariffAmount);
                ShowPopUpMsg("PORTIN Request Fail \n Please Try Again");
                //ShowPopUpMsg(ex.Message + "\n" + resp);
            }
             
            //else
            //{
            //    SaveDataSubscriber(resp, 16, transact, TariffAmount);
            //    ShowPopUpMsg("PORTIN Request Fail \n Please Try Again");
            //}


        }
        public void SaveDataCompany(string resp, int status, string TransactionID,string Amount)
        {
            SPayment sp = new SPayment();
            sp.ChargedAmount = Convert.ToDecimal(Amount);
            sp.PaymentType = 5;
            sp.PayeeID = 1;// Convert.ToInt32(Session["DistributorID"]);
            sp.PaymentFrom = 9;
            sp.ActivationType = 7;
            sp.ActivationStatus = status;
            sp.ActivationVia = 17;
            sp.ActivationResp = resp;
            sp.ActivationRequest = RequestRes;
            sp.TariffID = 0;
            sp.ALLOCATED_MSISDN = "";
            sp.TransactionId = TransactionID;

            sp.PaymentMode = "Subscriber PortIn";
            sp.TransactionStatusId = 25;
            sp.TransactionStatus = "Fail";

            int dist = 1;//Convert.ToInt32(Session["DistributorID"]);
            int loginID = 1;// Convert.ToInt32(Session["LoginID"]);
            string sim = txtSIMCARD.Text.Trim();
            string zip = txtZIPCode.Text.Trim();
            string Language = "ENGLISH";
            string ChannelID = ConfigurationManager.AppSettings.Get("TXN_SERIES");

            try
            {
                int s = svc.UpdateAccountBalanceService(dist, loginID, sim, zip, Language, ChannelID, sp);
                WriteLog(dist, loginID, sim, zip, Language, ChannelID, sp, "Record Save Success when sim PortIn Not Success In Subscriber Case");
            }
            catch (Exception ex)
            {
                WriteLog(dist, loginID, sim, zip, Language, ChannelID, sp, "Record Save Fail when sim PortIn Not Success In Subscriber Case");
            }
            //ShowPopUpMsg("PORTIN Request Fail");
            resetControls(1);
        }


        public void SendMailH20AndEasyGo(string SendTo, string Subject, string UserName, string UserID, string pass, string Network,string Plan, string Amount)
        {
            try
            {
                string LoginUrl = "http://" + HttpContext.Current.Request.Url.Host + HttpContext.Current.Request.ApplicationPath + "/Login.aspx";
                string LogoUrl = "http://" + HttpContext.Current.Request.Url.Host + HttpContext.Current.Request.ApplicationPath + "/img/logo.png";
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
               // mail.Subject = Subject + " " + DateTime.UtcNow.Subtract(ts).ToString();

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
                sb.Append("<p><strong> Network </strong> : " + Network + "<p>");
                sb.Append("<p><strong> Tariff/Plan </strong> : " + Plan + "<p>");
                sb.Append("<p><strong> Amount</strong> : " + Amount + "<p>");
                sb.Append("<p><strong> Zip Code</strong> : " + txtZIPCode.Text + "<p>");
                sb.Append("<p><strong> Phone Number</strong> : " + txtPHONETOPORT.Text + "<p>");
                sb.Append("<p><strong> Account Number</strong> : " + txtACCOUNT.Text + "<p>");
                sb.Append("<p><strong> Pin </strong>:" + txtPIN.Text + "<p>");
                sb.Append("<p><strong> Email Address</strong> : " + txtEmailAddress.Text + "<p>");

                sb.Append("<p><strong> Current Service Provider </strong> : " + txtServiceProvider.Text + "<p>");
                sb.Append("<p><strong>State</strong> : " + txtState.Text + "<p>");
                sb.Append("<p><strong> City</strong> : " + txtCity.Text + "<p>");
                sb.Append("<p><strong>Customer 1st and Last Name</strong> : " + txtLastname.Text + "<p>");
                sb.Append("<p><strong>Address </strong> : " + txtAddress.Text + "<p>");
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
     
        
        private void resetControls(int condition)
        {
            if (condition == 1)
            {
                txtSIMCARD.Text = string.Empty;
                txtZIPCode.Text = string.Empty;
                txtChannelID.Text = string.Empty;
                ddlLanguage.SelectedIndex = 0;

                txtEmailAddress.Text = string.Empty;

                txtServiceProvider.Text = string.Empty;
                txtCity.Text = string.Empty;
                txtState.Text = string.Empty;
                txtLastname.Text = string.Empty;

                txtAddress.Text = string.Empty;
                txtPIN.Text = "";
                txtPHONETOPORT.Text = "";
                txtACCOUNT.Text = "";
                txtCustomerName.Text = "";
               

            }
            else
            {
                txtSIMCARD.Text = string.Empty;
                txtZIPCode.Text = string.Empty;
                txtChannelID.Text = string.Empty;
                ddlLanguage.SelectedIndex = 0;


                txtPHONETOPORT.Text = string.Empty;
                txtACCOUNT.Text = string.Empty;
                txtPIN.Text = string.Empty;

                txtEmailAddress.Text = string.Empty;

                txtEmailAddress.Text = string.Empty;

                txtServiceProvider.Text = string.Empty;
                txtCity.Text = string.Empty;
                txtState.Text = string.Empty;
                txtLastname.Text = string.Empty;

                txtAddress.Text = string.Empty;
                txtPIN.Text = "";
                txtPHONETOPORT.Text = "";
                txtACCOUNT.Text = "";
                txtCustomerName.Text = "";
                
            }
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

                logdata.Append(sp.CusName);
                logdata.Append(sp.Address);
                logdata.Append(sp.EmailID);
                logdata.Append(sp.Mobile);
                string data = logdata.ToString();
                Log1(data, msgg);
                Log1("", "split");
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

        public void Log(string ss, string condition)
        {
            try
            {

                string filename = "PortinLog.txt";
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

                        sw.WriteLine("------------------------------------------------Gap-----------------------------------------------");
                        sw.Close();
                    }
                }
            }
            catch (Exception EX)
            {


            }
        }

        public void Log1(string ss, string condition)
        {
            try
            {
                string filename = "PortinLog.txt";
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

                        sw.WriteLine("------------------------------------------------Gap-----------------------------------------------");
                        sw.Close();
                    }
                }
            }
            catch (Exception ex)
            {

            }

        }



        
    }
}