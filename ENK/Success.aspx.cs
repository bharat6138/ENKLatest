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
    public partial class Success : System.Web.UI.Page
    {
        
        Service1Client svc = new Service1Client();
        
        Boolean ActivationResult = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            string strResponse = "";

            string txToken = "";
            string MobileNo = "";
            string TotalAmount = "";
            string Network = "";
            string TariffCode = "";
            string EmailID = "";
            string PayPalRequest = "";
            string ZIPCode = "";
            string State = "";
            string RechargeAmount = "";
            string Success = "";
            string Tax = "";
            string Regulatery = "";

            
            try
            {
                if (!Page.IsPostBack)
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

                    PayPalRequest = Request.QueryString.Get("PayPal");

                    Log2("Paypal All Response Transaction", "Transaction");
                    Log2(PayPalRequest, "PayPalRequest");
                    Log2("", "split");

                    if (PayPalRequest != null && PayPalRequest != "")
                    {
                        if (PayPalRequest == "Cancel")
                        {
                            PayPalRequest = Request.QueryString.Get("PayPal");
                            Log2("Paypal Cancel Transaction", "Transaction");
                            Log2(PayPalRequest, "PayPalRequest");
                            Log2("", "split");
                            lblMessage.Text = "Paypal Transaction Cancel";
                            ScriptManager.RegisterStartupScript(this, GetType(), "", "JScriptConfirmationFail();", true);
                            return;
                        }

                        string ssd = Convert.ToString(PayPalRequest);
                        string[] ActData = ssd.Split(',');
                        MobileNo = ActData[0];
                        TotalAmount = ActData[1];
                        Network = ActData[2];
                        TariffCode = ActData[3];
                        EmailID = ActData[4];

                        RechargeAmount = ActData[5];
                        State = ActData[6];
                        ZIPCode = ActData[7];
                        Tax = ActData[8];
                        Regulatery = ActData[9];

                        txToken = Request.QueryString.Get("tx");

                        Log2("Paypal Responce Transaction", "Transaction");
                        Log2(MobileNo, "SimNumber");
                        Log2(EmailID, "EmailID");
                        Log2(txToken, "txToken");
                        Log2(State, "State");
                        Log2(TotalAmount, "AmountPay");
                        Log2(TariffCode, "TariffCode");
                        Log2("", "split");

                        // ScriptManager.RegisterStartupScript(this, GetType(), "", "RemoveQueryString();", true);

                        if (txToken != null && txToken != "")
                        {

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

                                lblresponse.Text = strResponse;

                                if (strResponse.IndexOf("SUCCESS") != -1)
                                {
                                    string ss = Request.QueryString.ToString();
                                    Log2("Paypal Recharge Transaction Success", "Reason");

                                    Log2(strResponse + Convert.ToString(1), "Detail");
                                    Log2(ss, "Detail");
                                    Log2(MobileNo, "SimNumber");
                                    Log2(TotalAmount, "AmountPay");
                                    Log2(TariffCode, "TariffCode");
                                    Log2("", "split");


                                    try
                                    {
                                        DataSet dsDuplicateTxnID = new DataSet();
                                        dsDuplicateTxnID = svc.CheckDuplicatePaypalTxnID(txToken);
                                        if (dsDuplicateTxnID != null)
                                        {
                                            //  Rows.Count > 0  Duplicate TxnID
                                            if (dsDuplicateTxnID.Tables[0].Rows.Count > 0)
                                            {
                                                ScriptManager.RegisterStartupScript(this, GetType(), "", "JScriptConfirmationSuccess();", true);
                                            }
                                            else
                                            {
                                                Recharge(Network, TariffCode, MobileNo, TotalAmount, EmailID, strResponse, RechargeAmount, State, ZIPCode, txToken, Tax, Regulatery);
                                                //MakeReceipt(strResponse);
                                            }
                                        }
                                    }
                                    catch { }

                                }
                                else
                                {

                                    string ss = Request.QueryString.ToString();
                                    Log2("Recharge TransactionFail", "Reason");
                                    Log2("In indexof block", "Recharge");
                                    Log2(strResponse + Convert.ToString(1), "Detail");
                                    Log2(ss, "Detail");
                                    Log2(MobileNo, "SimNumber");
                                    Log2(TotalAmount, "AmountPay");
                                    Log2(TariffCode, "TariffCode");
                                    Log2("", "split");
                                    lblMessage.Text = "Oooops, something went wrong with paypal... Transaction failed";

                                    ScriptManager.RegisterStartupScript(this, GetType(), "", "JScriptConfirmationFail();", true);

                                }

                            }


                        }
                        else
                        {

                            divPaymentDetail.Visible = false;
                            string ss = Request.QueryString.ToString();
                            Log2("TransactionFail", "Reason");
                            Log2("In txToken is null or blank", "Topup");
                            Log2(Convert.ToString(1), "Detail");
                            Log2(ss, "Detail");
                            Log2("", "split");
                            lblMessage.Text = "Oooops, something went wrong with paypal... Transaction failed";
                            ScriptManager.RegisterStartupScript(this, GetType(), "", "JScriptConfirmationFail();", true);


                        }
                    }
                    else
                    {
                        lblMessage.Text = "Oooops, something went wrong with Transaction failed";
                        ScriptManager.RegisterStartupScript(this, GetType(), "", "JScriptConfirmationFail();", true);
                    
                    }
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Oooops, something went wrong with paypal... Transaction failed";
                Log2("TransactionFail", "CatchPageload");
                Log2("Oooops, something went wrong with paypal... Transaction failed", "Reason");
                //UpdateDashboardBalanceAmount();
                ScriptManager.RegisterStartupScript(this, GetType(), "", "JScriptConfirmationFail();", true);
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


        private void Recharge(string Network, string TariffCode, string MobileNo, string TotalAmount, string EmailID, string strResponse, string RechargeAmount, string State, string ZIPCode, string TxnID, string Tax, string Regulatery)
        {
            try
            {
                string ss = "";
                ENK.net.emida.ws.webServicesService ws = new webServicesService();
                string simNumber = MobileNo;
                int NetworkID = Convert.ToInt32(Network);


                int distr = 1;// Convert.ToInt32(Session["DistributorID"]);
                int clnt = 3;//Convert.ToInt32(Session["ClientTypeID"]);
                int DistributorID = 1;

               // string TariffAmount = "";
               // TariffAmount = TotalAmount;
                hddnTariffCode.Value = TariffCode;

               
                if (TotalAmount == "")
                {
                    TotalAmount = "0";
                }

                if (RechargeAmount == "")
                {
                    RechargeAmount = "0";
                }
                if (Tax == "")
                {
                    Tax = "0";
                }


                hddnTariffID.Value = TariffCode;
                ViewState["AmountPay"] = "$";

                string InvoiceNo = DateTime.Now.ToString().GetHashCode().ToString("X");
                InvoiceNo = "RC" + InvoiceNo;
                DataSet dsDuplicate = new DataSet();
                string Number = "";
                if (hddnInvoice.Value == "" || hddnInvoice.Value =="0")
                {
                    Number =  "0" ;
                }
                else
                {
                    Number = hddnInvoice.Value  ;
                }

                dsDuplicate = svc.CheckRechargeDuplicate(Convert.ToInt32(NetworkID), simNumber, Convert.ToInt32(hddnTariffID.Value), Number);
               

                // ViewState["InvoiceNo"] = InvoiceNo;
                hddnInvoice.Value = InvoiceNo;

               
                string Request = "01, 3756263, 1234, " + hddnTariffCode.Value + "," + simNumber + "," + RechargeAmount + "," + InvoiceNo + ", 1";

                if (dsDuplicate != null)
                {
                    if (Convert.ToInt32(dsDuplicate.Tables[0].Rows[0]["IsValid"]) == 0)
                    {
                        // RechargeAmount is original Plan Recharge Amount
                        //TariffAmount is With Surcharge Pay Amount


                        ss = "Mobile- " + MobileNo + "|" + "AmountPay- " + TotalAmount + "|" + "Network- " + Network + "|" + "TariffCode- " + TariffCode + "|" + "RechargeAmount- " + RechargeAmount + "|" + "State- " + State + "|" + "ZIPCode- " + ZIPCode + "|" + "TxnID- " + TxnID + "| Tax- " + Tax;
                        Log2("Subscriber Recharge", "Subscriber Recharge");
                        Log2(ss, "Subscriber Recharge Network");
                        //Log2("", "split");
                        Log2(dsDuplicate.Tables[0].Rows[0]["Msg"].ToString(), "Show Check Message");
                        Log2(dsDuplicate.Tables[0].Rows[0]["IsValid"].ToString(), "IsValid");
                        Log2(Convert.ToInt32(NetworkID) + ' ' + simNumber + ' ' + Convert.ToInt32(hddnTariffID.Value) + ' ' + Number, "Duplicate Request Parameters");
                        Log2(simNumber, "SimNumber");
                        Log2(TotalAmount, "AmountPay");
                        Log2(RechargeAmount, "RechageAmount");
                        Log2("", "split");

                        DataSet dsDuplicateTxnID = new DataSet();
                        dsDuplicateTxnID = svc.CheckDuplicatePaypalTxnID(TxnID);
                        if (dsDuplicateTxnID != null)
                        {
                            //  Rows.Count > 0  Duplicate TxnID
                            if (dsDuplicateTxnID.Tables[0].Rows.Count > 0)
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "", "JScriptConfirmationSuccess();", true);
                            }
                            else
                            {
                               
                                string ss2 = ws.PinDistSale("01", "3756263", "1234", hddnTariffID.Value, simNumber, RechargeAmount, InvoiceNo, "1");

                                StringReader Reader = new StringReader(ss2);
                                DataSet ds1 = new DataSet();

                                ds1.ReadXml(Reader);
                                if (ds1.Tables.Count > 0)
                                {
                                    DataTable dtMsg = ds1.Tables[0];
                                    if (dtMsg.Rows.Count > 0)
                                    {
                                        string ResponseCode = dtMsg.Rows[0]["ResponseCode"].ToString();
                                        string ResponseMessage = dtMsg.Rows[0]["ResponseMessage"].ToString();
                                        string PinNumber = "";

                                        if (ResponseCode == "00")
                                        {

                                            PinNumber = dtMsg.Rows[0]["Pin"].ToString();
                                            string Currency = "$";
                                            string TransactionID = Convert.ToString(dtMsg.Rows[0]["TransactionID"]);

                                            DataSet dsTransaction = svc.SaveTransactionDetails(Convert.ToInt32(NetworkID), Convert.ToInt32(hddnTariffID.Value), "11", Convert.ToString(PinNumber), simNumber, InvoiceNo, Convert.ToString(RechargeAmount), Currency, State, ZIPCode, "305", Convert.ToInt32(1), Convert.ToString(TotalAmount));
                                            if (dsTransaction != null)
                                            {

                                                //string TransactionID = Convert.ToString( dsTransaction.Tables[0].Rows[0]["TransactionID"]);
                                                //int DistributorID = Convert.ToInt32(Session["DistributorID"]);
                                                int loginID = 1;
                                                string RechargeStatus = "27";
                                                string @RechargeVia = "29";
                                                int TransactionStatusID = 27;

                                                SendMail(EmailID, (simNumber + "  Recharge successful!"), PinNumber, simNumber, "", NetworkID);

                                                int s1 = svc.UpdateAccountBalanceAfterRecharge(Convert.ToInt32(NetworkID), Convert.ToInt32(hddnTariffID.Value), Convert.ToString(simNumber), Convert.ToDecimal(TotalAmount), DistributorID, Convert.ToString(ZIPCode), RechargeStatus, RechargeVia, Request, ss2, loginID, 9, "Cash", TransactionID, 1, "Subscriber Recharge  successfully", TransactionStatusID, PinNumber, State, TxnID, Tax, Convert.ToString(RechargeAmount), InvoiceNo, "Subscriber", Regulatery.ToString());
                                              //  MakeReceipt(strResponse);
                                                Log2("Recharge Transaction Success", "Reason");
                                                Log2(Request, "Request");
                                                Log2(ResponseCode, "Response");
                                                Log2(ResponseMessage, "ResponseMessage");
                                                Log2(ss, "Detail");
                                                Log2("", "split");
                                                lblMessage.Text = "Mobile Number  " + simNumber + " Recharge  successfully ,  Pin Number " + PinNumber;

                                                ws.Logout("01", "A&HPrepaid", "95222", "1");
                                                string redirecturl = "";

                                                redirecturl = simNumber + "," + RechargeAmount + "," + TotalAmount + "," + Network + "," + TxnID + "," + ResponseMessage;

                                                Response.Redirect("~/RechargePrint.aspx?RechrgeSuccess=" + redirecturl);

                                                return;
                                            }




                                        }
                                        else
                                        {
                                            string TransactionID = Convert.ToString(0);
                                            //int DistributorID = Convert.ToInt32(Session["DistributorID"]);
                                            int loginID = 1;
                                            string RechargeStatus = "28";
                                            string RechargeVia = "29";
                                            int TransactionStatusID = 28;
                                            int s1 = svc.UpdateAccountBalanceAfterRecharge(Convert.ToInt32(NetworkID), Convert.ToInt32(hddnTariffID.Value), Convert.ToString(simNumber), Convert.ToDecimal(TotalAmount), DistributorID, Convert.ToString(""), RechargeStatus, RechargeVia, Request, ss2, loginID, 9, "Cash", TransactionID, 1, "Subscriber Recharge  Fail", TransactionStatusID, PinNumber, State, TxnID, Tax, Convert.ToString(RechargeAmount), InvoiceNo, "Subscriber", Regulatery.ToString());


                                            Log2("Recharge Transaction Fail", "Reason");
                                            Log2(Request, "Request");
                                            Log2(ResponseCode, "Response");
                                            Log2(ResponseMessage, "ResponseMessage");
                                            Log2("", "split");
                                            lblMessage.Text = ResponseMessage;
                                            SendFailureMailRecharge(EmailID, (simNumber + "  Recharge Failed!"), simNumber, Convert.ToInt32(NetworkID), TariffCode, RechargeAmount, "","");

                                            ShowPopUpMsg(ResponseMessage);

                                            ScriptManager.RegisterStartupScript(this, GetType(), "", "JScriptConfirmationFail();", true);
                                            return;
                                        }


                                    }

                                    else
                                    {

                                        Log2("NO Record from API PinDistSale", "PinDistSale");
                                        Log2(Request, "Request");
                                        Log2(simNumber, "SimNumber");
                                        Log2(TotalAmount, "AmountPay");
                                        Log2(hddnTariffID.Value, "TariffCode");
                                        Log2(State, "State-City");
                                        Log2(ZIPCode, "Zip");
                                        Log2(RechargeAmount, "RechargeAmount");
                                        Log2("", "split");
                                        ScriptManager.RegisterStartupScript(this, GetType(), "", "JScriptConfirmationFail();", true);
                                    }


                                }
                                else
                                {

                                    Log2("NO Record from API PinDistSale", "PinDistSale");
                                    Log2(Request, "Request");
                                    Log2(simNumber, "SimNumber");
                                    Log2(TotalAmount, "AmountPay");
                                    Log2(hddnTariffID.Value, "TariffCode");
                                    Log2(State, "State-City");
                                    Log2(ZIPCode, "Zip");
                                    Log2(RechargeAmount, "RechargeAmount");
                                    Log2("", "split");
                                    ScriptManager.RegisterStartupScript(this, GetType(), "", "JScriptConfirmationFail();", true);
                                }
                            }
                        }

                    }
                    else
                    {


                        try
                        {
                            string RechargeDate = Convert.ToString(dsDuplicate.Tables[0].Rows[0]["RechargeDate"]).ToString();
                            string amt = Convert.ToString(dsDuplicate.Tables[0].Rows[0]["Amount"]).ToString();

                            lblmsg.Text = "You already Recharged " + simNumber + "  with Amount " + amt + " at " + RechargeDate + " Successfully. If you want to recharge it again  please wait 5 min.";
                            lblmsg.Visible = true;
                           

                        }
                        catch { }

                        Log2("Recharge Transaction Duplicate Recharge", "Duplicate Recharge");
                        Log2(dsDuplicate.Tables[0].Rows[0]["Msg"].ToString(), "Show Check Duplicate Message");
                        Log2(dsDuplicate.Tables[0].Rows[0]["IsValid"].ToString(), "IsValid");
                        Log2(simNumber, "SimNumber");
                        Log2(RechargeAmount, "AmountPay");

                        Log2("", "split");

                        ScriptManager.RegisterStartupScript(this, GetType(), "", "JScriptConfirmationFail();", true);
                    }
                }

                
            }
            catch (Exception ex)
            {


                lblmsg.Text = ex.Message;
                lblmsg.Visible = true;          
                Log2("Transaction", "CatchFail");
                Log2(MobileNo, "SimNumber");
                Log2(TxnID, "TxnID");
                Log2(hddnTariffID.Value, "TariffCode");
                Log2(State, "State-City");
                Log2(ZIPCode, "Zip");
                Log2(RechargeAmount, "RechargeAmount");
                Log2("", "split");
                ScriptManager.RegisterStartupScript(this, GetType(), "", "JScriptConfirmationFail();", true);
                 
            }
        }

        public void SendFailureMailRecharge(string SendTo, string Subject, string MobileNumber, int NetworkID, string TariffCode, string RechageAmount, string ProductDescription, string Responce)
        {
            try
            {
                string LoginUrl = "http://" + HttpContext.Current.Request.Url.Host + HttpContext.Current.Request.ApplicationPath + "/Login.aspx";
                string LogoUrl = "http://" + HttpContext.Current.Request.Url.Host + HttpContext.Current.Request.ApplicationPath + "/img/logo.png";
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient();


                string MailAddress = ConfigurationManager.AppSettings.Get("Fromail");
                string ToMailID = ConfigurationManager.AppSettings.Get("ToMailID");
                string PassWord = ConfigurationManager.AppSettings.Get("Password");


                SendTo = SendTo + "," + ConfigurationManager.AppSettings.Get("COMPANY_INFOEMAIL") ;
                // SendTo = SendTo + "," + "shadab.a@virtuzo.in";

             


                mail.From = new MailAddress(MailAddress);
                // mail.To.Add(SendTo);
                mail.To.Add(SendTo);
                TimeSpan ts = new TimeSpan(7, 0, 0);
                mail.Subject = Subject + " " + DateTime.UtcNow.Subtract(ts).ToString();

                StringBuilder sb = new StringBuilder();
                sb.Append("<html>");
                sb.Append("<body style=”color:grey; font-size:15px;font-style:italic;”>");
                sb.Append("<font face=”Helvetica, Arial, sans-serif”>");
                sb.Append("<div style=” border:1px solid black; background-color: #ece8d4; width:600px; height:200px; padding:30px; margin-top:30px;font-style:italic;”>");
                sb.Append("<div style=”position:absolute; height:200px; width:100px; background-color:0d1d36; padding:30px;”>");
                sb.Append("</div>");
                sb.Append("<img src=" + LogoUrl + " />");

                sb.Append("<div style=”background-color: #ece8d4; width:600px; height:200px; padding:30px; margin-top:30px;”>");
                //sb.Append("<p>Please find the new credentials and get started.</p>");
                sb.Append("<div style='border:1px solid black;padding-left: 24px;width: 388px; BORDER-RADIUS: 25px;font-style:italic;'>");

                sb.Append("<p> TariffCode  " + TariffCode + ". <p>");
               

                sb.Append("<p>Rechage Amount  " + RechageAmount + "$. <p>");

                sb.Append("<p>Mobile Number " + MobileNumber + "  Recharge not successful.  <p>");

               


                sb.Append("</div>");
                sb.Append("</div>");

                sb.Append("<br/>");
                if (NetworkID == 13)
                {

                    sb.Append("<p> Recharge not successfully, If you have any issue  with Recharge please contact Lycamobile directly at +1-845-301-1633 / +1-866-277-0024. <p>");

                }
                else if (NetworkID == 15)
                {
                    sb.Append("<p> Recharge not successfully , If you have any issue  with Recharge please contact H2O WIRELESS. <p>");
                    sb.Append("<p> Dealer Hotline 1-800-939-1261 <p>");
                    sb.Append("<p> We are open 7 days a week from 9AM EST to 12AM EST Monday - Friday, and 9AM EST to 11PM EST on weekends. <p>");
                    sb.Append("<p> H2O GSM Support 1-800-643-4926 <p>");
                    sb.Append("<p>Email: customercare@h2owirelessnow.com <p>");
                }


                sb.Append("<p>");
                sb.Append("<br/>");


                sb.Append("<br/>");
                sb.Append("<p>Sincerely,");
                sb.Append("<p>" + ConfigurationManager.AppSettings.Get("COMPANY_NAME") + "</p>");
                sb.Append("<p>Thank you</p>");


                sb.Append("<p>----------------------------------------------------------------<br/>");

                sb.Append("Please send us email " + ConfigurationManager.AppSettings.Get("COMPANY_INFOEMAIL") + " or call us  209 297-3200.   Text us:  209 890 8006  with in 24 Hours ");


                sb.Append("<p>Please do not reply to this email. This mailbox is not monitored and you will not receive a response. ");

                sb.Append("<br/>");
                sb.Append("</div>");
                sb.Append("</body>");
                sb.Append("</html>");

                mail.Body = sb.ToString();
                mail.BodyEncoding = System.Text.Encoding.UTF8;
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.High;

                SmtpServer.Host = ConfigurationManager.AppSettings.Get("Host") ;
                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential(MailAddress, PassWord);
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);

            }
            catch (Exception ex)
            {

            }
        }



        public void SendMail(string SendTo, string Subject, string PinNumber, string MobileNumber, string pass, int NetworkID)
        {
            try
            {
                string LoginUrl = "http://" + HttpContext.Current.Request.Url.Host + HttpContext.Current.Request.ApplicationPath + "/Login.aspx";
                string LogoUrl = "http://" + HttpContext.Current.Request.Url.Host + HttpContext.Current.Request.ApplicationPath + "/img/logo.png";
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient();


                string MailAddress = ConfigurationManager.AppSettings.Get("Fromail");
                string ToMailID = ConfigurationManager.AppSettings.Get("ToMailID");
                string PassWord = ConfigurationManager.AppSettings.Get("Password");


                string ToMail = SendTo;

                if (ToMail == "")
                {
                    ToMail = ToMailID;
                
                }
                  





                mail.From = new MailAddress(MailAddress);
               // mail.To.Add(SendTo);
                mail.To.Add(ToMail);
                TimeSpan ts = new TimeSpan(7, 0, 0);
                mail.Subject = Subject + " " + DateTime.UtcNow.Subtract(ts).ToString();

                StringBuilder sb = new StringBuilder();
                sb.Append("<html>");
                sb.Append("<body style=”color:grey; font-size:15px;”>");
                sb.Append("<font face=”Helvetica, Arial, sans-serif”>");

                sb.Append("<div style=”position:absolute; height:200px; width:100px; background-color:0d1d36; padding:30px;”>");
                sb.Append("<img src=" + LogoUrl + " />");
                sb.Append("</div>");

                sb.Append("<br/>");
 

                sb.Append("<div style=”background-color: #ece8d4; width:600px; height:200px; padding:30px; margin-top:30px;”>");
                //sb.Append("<p>Please find the new credentials and get started.</p>");
                sb.Append("<div style='border:1px solid black;padding-left: 24px;width: 388px; BORDER-RADIUS: 25px;font-style:italic;'>");
                sb.Append("<p>Mobile Number " + MobileNumber + "  Recharge successfully.  <p>");
                sb.Append("<br/>");
                sb.Append("<p> Pin Number " + PinNumber + "<p>");
                sb.Append("<br/>");

                sb.Append("</div>");

                if (NetworkID ==13)
                {

                    sb.Append("<p> Recharge  successfully, If you have any issue  with Recharge please contact Lycamobile directly at +1-845-301-1633 / +1-866-277-0024 <p>");

                }
                else if    (NetworkID ==15)
                 
                {
                    sb.Append("<p> Recharge  successfully, If you have any issue  with Recharge please contact H2O WIRELESS. <p>");

                    sb.Append("<p> Dealer Hotline 1-800-939-1261 <p>");
                    sb.Append("<p> We are open 7 days a week from 9AM EST to 12AM EST Monday - Friday, and 9AM EST to 11PM EST on weekends. <p>");
                    sb.Append("<p> H2O GSM Support 1-800-643-4926 <p>");
                    sb.Append("<p>Email: customercare@h2owirelessnow.com <p>");
                
                
                }
                
                
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
            catch  
            {

            }
        }

        public void Log2(string ss, string condition)
        {
            try
            {
                string filename = "NewSubscriberRechargeLog.txt";
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


        public void MakeReceipt(string resp)
        {
            try
            {

                string[] sd = resp.Split('\n');
                List<string> sdlist = new List<string>();
                sdlist = sd.ToList();
                lblTransactionAmount.Text = "Transaction Amount=" + sd[1].Replace("mc_gross=", "");
                lblTransactionDate.Text = "Transaction Date=" + DateTime.Now.ToString();//sd[7];
                lblPayerName.Text = "Payer Name=" + sd[11].Replace("first_name=", "") + " " + sd[24].Replace("last_name=", "");
                lblAddress.Text = "Transactionid=" + sd[22].Replace("txn_id=", "");

            }
            catch { }

        }
    }
}