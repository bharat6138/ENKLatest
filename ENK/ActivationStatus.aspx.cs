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
using System.Net.Mail;

namespace ENK
{
    public partial class ActivationStatus : System.Web.UI.Page
    {
        Service1Client svc = new Service1Client();
        string ActivationRequestString = "";
        string TariffTypeID= "";
        string TariffType = "";
        string TariffCode = "";
        string email = "";
        string TransactionID = "";
        string SIMCARD= "";
        string ZIPCode = "";
        string AmountPay = "";
        string TariffID = "";
        string ALLOCATED_MSISDN = "";
        Boolean ActivationResult = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            string strResponse = "";
            
            string txToken = "";
            try
            {
                if (!Page.IsPostBack)
                {
                    txToken = Request.QueryString.Get("tx");
                    if (txToken != null && txToken != "")
                    {
                        string authToken = ConfigurationManager.AppSettings["PDTToken"];

                        string query = string.Format("cmd=_notify-synch&tx={0}&at={1}", txToken, authToken);
                        string url = ConfigurationManager.AppSettings["PayPalSubmitUrl"];
                        HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);

                        req.Method = "POST";
                        req.ContentType = "application/x-www-form-urlencoded";
                        req.ContentLength = query.Length;

                        StreamWriter stOut = new StreamWriter(req.GetRequestStream(), System.Text.Encoding.ASCII);
                        stOut.Write(query);
                        stOut.Close();

                        StreamReader stIn = new StreamReader(req.GetResponse().GetResponseStream());
                        strResponse = stIn.ReadToEnd();
                        stIn.Close();

                       // DataSet ds = svc.GetTestDataService();
                       // strResponse = ds.Tables[1].Rows[0][0].ToString();

                        lblresponse.Text = strResponse;

                        if (strResponse.IndexOf("SUCCESS") != -1)
                        {
                            if (Session["RequestString"] != null)
                            {
                                ActivationRequestString = Convert.ToString(Session["RequestString"]);
                                string ssd = Convert.ToString(Session["RequestData"]);
                                string[] ActData = ssd.Split(',');
                                TariffTypeID = ActData[0];
                                TariffType = ActData[1];
                                TariffCode = ActData[2];
                                email = ActData[3];
                                TransactionID = ActData[4];
                                SIMCARD = ActData[5];
                                ZIPCode = ActData[6];
                                AmountPay = ActData[7];
                                TariffID = ActData[8];
                                ActivationProcess(ActivationRequestString);
                                if (ActivationResult == true)
                                {
                                    int a = 0;
                                    a = UpdatePayment(strResponse, "Success", 15, txToken, 24);
                                     
                                    divPaymentDetail.Visible = true;
                                    divActivationDetail.Visible = true;
                                    divBtn.Visible = true;
                                    divlink.Visible = false;
                                    string ss = Request.QueryString.ToString();
                                    ActivationLog("Activation Success via Paypal", "Reason");

                                    ActivationLog(strResponse + Convert.ToString(Session["DistributorID"]), "Detail");
                                    ActivationLog(ss, "Detail");
                                    ActivationLog("", "split");
                                    lblMessage.Text = "Sim Activation Successfully";
                                    MakeReceipt(1,strResponse);
                                    MakeActivationReceipt(1, "");
                                      
                                }
                                else
                                {
                                    UpdatePayment(strResponse, "Success", 16, txToken, 24);

                                    MakeReceipt(1, strResponse);
                                    divPaymentDetail.Visible = true;
                                    divActivationDetail.Visible = false;
                                    divBtn.Visible = true;

                                    divlink.Visible = false;
                                    string ss = Request.QueryString.ToString();
                                    ActivationLog("Activation Fail", "Reason");
                                    ActivationLog(" in Activation Request String null", "Reason");
                                    ActivationLog("Amount added to account balance", "Reason");
                                    ActivationLog(strResponse + Convert.ToString(Session["DistributorID"]), "Detail");
                                    ActivationLog(ss, "Detail");
                                    ActivationLog("", "split");
                                    lblMessage.Text = "Oooops, something went wrong in activation but Payment added to Account Balance...";
                                }
                                
                            }
                            else
                            {
                                UpdatePayment(strResponse, "Success",16, txToken, 24);
                                 
                                MakeReceipt(1,strResponse);
                                divPaymentDetail.Visible = true;
                                divActivationDetail.Visible = false;
                                divBtn.Visible = true;

                                divlink.Visible = false;
                                string ss = Request.QueryString.ToString();
                                ActivationLog("Activation Fail", "Reason");
                                ActivationLog(" in Activation Request String null", "Reason");
                                ActivationLog("Amount added to account balance", "Reason");
                                ActivationLog(strResponse + Convert.ToString(Session["DistributorID"]), "Detail");
                                ActivationLog(ss, "Detail");
                                ActivationLog("", "split");
                                lblMessage.Text = "Oooops, something went wrong in activation but Payment added to Account Balance...";
                            }

                        }
                        else
                        {
                            UpdatePayment(strResponse, "Fail", 16, txToken, 25);
                            divPaymentDetail.Visible = false;
                            divBtn.Visible = false;
                            divlink.Visible = true;
                            string ss = Request.QueryString.ToString();
                            ActivationLog("TransactionFail", "Reason");
                            ActivationLog("In indexof block", "Topup");
                            ActivationLog(strResponse + Convert.ToString(Session["DistributorID"]), "Detail");
                            ActivationLog(ss, "Detail");
                            ActivationLog("", "split");
                            lblMessage.Text = "Oooops, something went wrong with paypal... Transaction failed";
                        }
                       UpdateDashboardBalanceAmount();
                    }
                    else
                    {
                        divPaymentDetail.Visible = false;
                        divBtn.Visible = false;
                        divlink.Visible = true;
                        UpdatePayment(strResponse, "Fail", 16, txToken, 25);
                        string ss = Request.QueryString.ToString();
                        ActivationLog("TransactionFail", "Reason");
                        ActivationLog("In txToken is null or blank", "Topup");
                        ActivationLog(Convert.ToString(Session["DistributorID"]), "Detail");
                        ActivationLog(ss, "Detail");
                        ActivationLog("", "split");
                        lblMessage.Text = "Oooops, something went wrong with paypal... Transaction failed";
                        UpdateDashboardBalanceAmount();
                    }
                }
                UpdateDashboardBalanceAmount();
            }
            catch (Exception ex)
            {
                divPaymentDetail.Visible = false;
                divBtn.Visible = false;
                divlink.Visible = true;
                UpdatePayment(strResponse, "Fail",16, txToken, 25);
                string ss = Request.QueryString.ToString();
                ActivationLog("TransactionFail in catch block", "Topup");
                ActivationLog(ex.Message, "Reason");
                ActivationLog(ex.Source, "Reason");
                ActivationLog(strResponse + Convert.ToString(Session["DistributorID"]), "Detail");
                ActivationLog(ss, "Detail");
                ActivationLog("", "split");
                lblMessage.Text = "Oooops, something went wrong with paypal... Transaction failed";
                //UpdateDashboardBalanceAmount();
            }
        }

        public int UpdatePayment(string resp, string ststus,int ActivationStatus, string transactionid, int TranactionStatusId)
        {
            int a = 0;
            try
            {
                if (Session["PaymentId"] != null)
                {
                    if (transactionid == null)
                    {
                        transactionid = "";
                    }
                    if (Session["LoginID"] != null)
                    {
                        if (TranactionStatusId != 25)
                        {
                            string[] sd = resp.Split('\n');
                            List<string> sdlist = new List<string>();

                            SPayment sp = new SPayment();
                            sp.PaymentType = 4;
                            sp.ActivationStatus = ActivationStatus;
                            sp.PaymentId = Convert.ToInt32(Session["PaymentId"]);
                            sp.ChargedAmount = Convert.ToDecimal(Session["Amount"]);
                            sp.TxnId = transactionid;
                            sp.TxnAmount = sd[27].Replace("payment_fee=", "");
                            sp.TransactionStatus = ststus;
                            sp.TransactionStatusId = TranactionStatusId;
                            sp.ReceiptId = "";
                            sp.PayerId = sd[4].Replace("payer_id=", "");
                            sp.TxnDate = DateTime.Now.ToString();
                            sp.CheckSumm = resp;

                            int dist = Convert.ToInt32(Session["DistributorID"]);
                            int loginID = Convert.ToInt32(Session["LoginID"]);
                            a = svc.UpdatePaypalActivationService(dist, loginID, sp);
                            Session["PaymentId"] = null;
                            Session["Amount"] = null;
                        }
                        else
                        {


                            SPayment sp = new SPayment();
                            sp.PaymentType = 4;
                            sp.ActivationStatus = ActivationStatus;
                            sp.PaymentId = Convert.ToInt32(Session["PaymentId"]);
                            sp.ChargedAmount = Convert.ToDecimal(Session["Amount"]);
                            sp.TxnId = transactionid;
                            sp.TxnAmount = "";
                            sp.TransactionStatus = ststus;
                            sp.TransactionStatusId = TranactionStatusId;
                            sp.ReceiptId = "";
                            sp.PayerId = "";
                            sp.TxnDate = DateTime.Now.ToString();
                            sp.CheckSumm = resp;

                            int dist = Convert.ToInt32(Session["DistributorID"]);
                            int loginID = Convert.ToInt32(Session["LoginID"]);
                            a = svc.UpdatePaypalActivationService(dist, loginID, sp);
                            Session["PaymentId"] = null;
                            Session["Amount"] = null;
                        }
                        return a;
                    }
                    else
                    {
                        ActivationLog("Session null in database insertion block", "Reason");
                        ActivationLog(resp, "");
                        ActivationLog("", "split");
                        return a;
                    }
                }
                return a;
            }
            catch (Exception ex)
            {
                ActivationLog("database insertion catch block" + ex.Message, "Reason");
                ActivationLog(resp, "");
                ActivationLog("", "split");
                return a;
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
                    lblBalance.Text = Convert.ToString(Session["CurrencyName"]) + " " + Convert.ToString(dst[0].balanceAmount);
                    //lblBalance.Text = "Account Balance " + Convert.ToString(Session["CurrencyName"]) + " " + Convert.ToString(dst[0].balanceAmount);

                }
            }
            catch (Exception ex)
            {

            }

        }

        public void MakeReceipt(int condition, string resp)
        {
            //DataSet ds = svc.GetTestDataService();
            // string ss = ds.Tables[0].Rows[0][0].ToString();
            string[] sd = resp.Split('\n');
            List<string> sdlist = new List<string>();
            sdlist = sd.ToList();
            lblTransactionAmount.Text = "Transaction Amount=" + sd[1].Replace("mc_gross=", "");
            lblTransactionDate.Text = "Transaction Date=" + DateTime.Now.ToString();//sd[7];
            lblPayerName.Text = "Payer Name=" + sd[11].Replace("first_name=", "") + " " + sd[24].Replace("last_name=", "");
            lblAddress.Text = "Transactionid=" + sd[22].Replace("txn_id=", "");
        }

        public void MakeActivationReceipt(int condition, string resp)
        {
             
            lblSimnumber.Text ="Simcard - "+SIMCARD;
            lblmsisdn.Text = "Maped with Mobile Number - " + ALLOCATED_MSISDN;
            
            
        }

        private void ShowPopUpMsg(string msg)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("alert('");
            sb.Append(msg.Replace("\n", "\\n").Replace("\r", "").Replace("'", "\\'"));
            sb.Append("');");
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "showalert", sb.ToString(), true);
        }

        public void ActivationLog(string ss, string condition)
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

        public void ActivationProcess(string request)
        {
            string resp = ActivateSim(request);
            Session["RequestString"] = null;
            //DataSet ds = svc.GetTestDataService();
            //string resp = ds.Tables[0].Rows[0][0].ToString();
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
                                ActivationResult = true;
                                ALLOCATED_MSISDN = theDataSet.Tables[2].Rows[0]["ALLOCATED_MSISDN"].ToString();
                                SendMail(email, "Sim Activation", ALLOCATED_MSISDN, SIMCARD, "");

                                SPayment sp = new SPayment();
                                sp.ChargedAmount = Convert.ToDecimal(AmountPay);
                                sp.PaymentType = 4;
                                sp.PayeeID = Convert.ToInt32(Session["DistributorID"]);
                                sp.PaymentFrom = 9;
                                sp.ActivationType = 6;
                                sp.ActivationStatus = 15;
                                sp.ActivationVia = 18;
                                sp.ActivationResp = resp;
                                sp.ActivationRequest = request;
                                sp.TariffID = Convert.ToInt32(TariffID);
                                sp.ALLOCATED_MSISDN = ALLOCATED_MSISDN;
                                sp.TransactionId = TransactionID;
                                sp.PaymentId = Convert.ToInt32(Session["PaymentId"]);
                                sp.TransactionStatusId = 24;
                                int dist = Convert.ToInt32(Session["DistributorID"]);
                                int loginID = Convert.ToInt32(Session["LoginID"]);
                                string sim = SIMCARD;
                                string zip = ZIPCode;
                                string Language = "ENGLISH";
                                string ChannelID =  ConfigurationManager.AppSettings.Get("TXN_SERIES"); ;
                                try
                                {
                                    int s = svc.UpdatePaypalAccountBalanceService(dist, loginID, sim, zip, Language, ChannelID, sp);

                                    WriteLog(dist, loginID, sim, zip, Language, ChannelID, sp, "Record Save Success when sim activated In Distributor Case");
                                }
                                catch (Exception ex)
                                {
                                    WriteLog(dist, loginID, sim, zip, Language, ChannelID, sp, "Record Save Fail when sim activated  In Distributor Case");

                                }
                               // ShowPopUpMsg("SIM activation done successfully\n with Mobile Number - " + ALLOCATED_MSISDN);
                                 
                            }
                            else
                            {
                                SaveData(resp, 16, TransactionID);
                                //ShowPopUpMsg("SIM activation Fail \n Please Try Again");
                            }
                        }
                        else
                        {
                            SaveData(resp, 16, TransactionID);
                           // ShowPopUpMsg("SIM activation Fail \n Please Try Again");
                        }
                    }
                    else
                    {
                        SaveData(resp, 16, TransactionID);
                        //ShowPopUpMsg("SIM activation Fail \n Please Try Again");
                    }
                }
                catch (Exception ex)
                {
                    SaveData(resp, 16, TransactionID);
                    //ShowPopUpMsg(ex.Message + "\n" + resp);
                }
            }
            else
            {
                SaveData(resp, 16, TransactionID);
                //ShowPopUpMsg("SIM activation Fail \n Please Try Again");
            }
        }

        public void SaveData(string resp, int status, string TransactionID)
        {
            SPayment sp = new SPayment();
            sp.ChargedAmount = Convert.ToDecimal(AmountPay);
            sp.PaymentType = 4;
            sp.PayeeID = Convert.ToInt32(Session["DistributorID"]);
            sp.PaymentFrom = 9;
            sp.ActivationType = 6;
            sp.ActivationStatus = status;
            sp.ActivationVia = 18;
            sp.ActivationResp = resp;
            sp.ActivationRequest = ActivationRequestString;
            sp.TariffID = Convert.ToInt32(TariffID);
            sp.ALLOCATED_MSISDN = "";
            sp.TransactionId = TransactionID;
            sp.PaymentId = Convert.ToInt32(Session["PaymentId"]);
            sp.TransactionStatusId = 25;
            int dist = Convert.ToInt32(Session["DistributorID"]);
            int loginID = Convert.ToInt32(Session["LoginID"]);
            string sim = SIMCARD;
            string zip = ZIPCode;
            string Language = "ENGLISH";
            string ChannelID = ConfigurationManager.AppSettings.Get("TXN_SERIES");

            try
            {
                int s = svc.UpdatePaypalAccountBalanceService(dist, loginID, sim, zip, Language, ChannelID, sp);
                WriteLog(dist, loginID, sim, zip, Language, ChannelID, sp, "Record Save success when sim Not activated In Distributor Case");
            }
            catch (Exception ex)
            {
                WriteLog(dist, loginID, sim, zip, Language, ChannelID, sp, "Record Save Fail when sim not activate In Distributor Case");
            }
            //ShowPopUpMsg("SIM activation Fail");
             
        }

        public void WriteLog(int dist, int loginID, string sim, string zip, string Language, string ChannelID, SPayment sp, string msgg)
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

        public string ActivateSim(string Request)
        {
            string response = "";
            try
            {

                Log(Request, "Sending Request");
                string resp = Activation(Request);
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
                //string filename = "ActivationLog.txt";
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

                sb.Append("<p>Sim Number " + UserID + " Activated successfully on Mobile Number " + UserName + "<p>");


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

      

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("ActivationSim.aspx");
        }
    }
}