using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using ENKDAL;


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

using Newtonsoft.Json;
using ENKService.net.emida.ws;

 

namespace ENKService
{
    
    public class ENKAPI_Json : IENKAPI_Json
    {
        string RequestRes = "";
        #region "Andriod"

        public string ValidateLoginService_Andriod(string UserName, string Pwd)
        {
            try
            {
                string result;
                DBUsers ud = new DBUsers();
                cGeneral.LogSteps(UserName + " " + Pwd, System.Web.Hosting.HostingEnvironment.MapPath("~\\ValidateLoginServicelog.txt"));
                string pass = Encryption.Encrypt(Pwd);
                DataSet ds = ud.ValidateLogin(UserName, pass);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    result = cGeneral.GetJson(ds.Tables[0]);
                    result = "{\"Response\" : [{\"Responsecode\" : \"0\" , \"Response\" : \"Success\"}] , \"Data\" : " + result + "}";
                }
                else
                {
                    result = "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \"Failure\" }]}";
                }

                return result;


            }
            catch (Exception ex)
            {
                ServiceData myServiceData = new ServiceData();
                myServiceData.Result = false;
                myServiceData.ErrorMessage = "unforeseen error occured. Please try later.";
                myServiceData.ErrorDetails = ex.ToString();
                throw new FaultException<ServiceData>(myServiceData, ex.ToString());
            }
        }

       
        public string GetTariffForActivationService(string LoginID, string DistributorID, string ClientTypeID)
        {
            try
            {
                string result;
                DBTariff dbt = new DBTariff();

                cGeneral.LogSteps(LoginID + " " + DistributorID + " " + ClientTypeID, System.Web.Hosting.HostingEnvironment.MapPath("~\\GetTariffForActivationServicelog.txt"));

                DataSet ds = dbt.GetTariffForActivation(Convert.ToInt32(LoginID), Convert.ToInt32(DistributorID), Convert.ToInt32(ClientTypeID));

                if (ds.Tables[0].Rows.Count > 0)
                {
                    result = cGeneral.GetJson(ds.Tables[0]);
                    result = "{\"Response\" : [{\"Responsecode\" : \"0\" , \"Response\" : \"Success\"}] , \"Data\" : " + result + "}";
                }
                else
                {
                    result = "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \"Failure\" }]}";
                }

                return result;

            }
            catch (Exception ex)
            {
                ServiceData myServiceData = new ServiceData();
                myServiceData.Result = false;
                myServiceData.ErrorMessage = "unforeseen error occured. Please try later.";
                myServiceData.ErrorDetails = ex.ToString();
                throw new FaultException<ServiceData>(myServiceData, ex.ToString());
            }
        }

        public string GetSingleDistributorService(string DistributorID)
        {
            try
            {
                string result;
                DBDistributor dis = new DBDistributor();
                cGeneral.LogSteps(DistributorID, System.Web.Hosting.HostingEnvironment.MapPath("~\\GetSingleDistributorServicelog.txt"));
                DataSet ds = dis.GetSingleDistributor(Convert.ToInt32(DistributorID));

                if (ds.Tables[0].Rows.Count > 0)
                {
                    result = cGeneral.GetJson(ds.Tables[0]);
                    result = "{\"Response\" : [{\"Responsecode\" : \"0\" , \"Response\" : \"Success\"}] , \"Data\" : " + result + "}";
                }
                else
                {
                    result = "{\"Response\" : [{\"Responsecode\" : \"0\" , \"Response\" : \"Failure\" }]}";
                }

                return result;


            }
            catch (Exception ex)
            {
                ServiceData myServiceData = new ServiceData();
                myServiceData.Result = false;
                myServiceData.ErrorMessage = "unforeseen error occured. Please try later.";
                myServiceData.ErrorDetails = ex.ToString();
                throw new FaultException<ServiceData>(myServiceData, ex.ToString());
            }
        }

        public Balance CheckBalance(string DistributorID)
        {
            try
            {
                Balance result;
                DBDistributor dis = new DBDistributor();
                cGeneral.LogSteps(DistributorID, System.Web.Hosting.HostingEnvironment.MapPath("~\\CheckBalancelog.txt"));
                DataSet ds = dis.GetSingleDistributor(Convert.ToInt32(DistributorID));

                if (ds.Tables[0].Rows.Count > 0)
                {
                    result = new Balance
                    {
                        ResponseCode = "0",
                        Response = "Success",
                        Amount = Convert.ToString(ds.Tables[0].Rows[0]["AccountBalance"])
                    };
                        
                }
                else
                {
                    result = new Balance
                    {
                        ResponseCode = "1",
                        Response = "Inavlid ID"
                    };
                }

                return result;
            }
            catch (Exception ex)
            {
                return new Balance
                {
                    ResponseCode = "1",
                    Response = "Failure"
                };
            }
        }

        public string CheckSimActivationService(string DistributorID, string ClientTypeID, string SimNumber)
        {
            DataSet ds = null;
            try
            {
                string result;
                string TRANSACTIONID;
                cSIM s = new cSIM();
                ds = s.CheckSimActivation(Convert.ToInt32(DistributorID), Convert.ToInt32(ClientTypeID), SimNumber,"Activate");

                if (ds.Tables[0].Rows.Count > 0)
                {
                    result = Convert.ToString(ds.Tables[0].Rows[0][0]);
                    int id = Convert.ToInt32(ds.Tables[1].Rows[0]["TRANSACTIONID"]);
                    TRANSACTIONID = id.ToString("00000");
                    TRANSACTIONID = "AHP" + TRANSACTIONID;


                    result = "{\"Response\" : [{\"Responsecode\" : \"0\" , \"Response\" : \"Success\" , \"SimStatus\" : \"" + result + "\", \"TRANSACTIONID\" : \"" + TRANSACTIONID + "\" }] }";
                }
                else
                {
                    result = "{\"Response\" : [{\"Responsecode\" : \"0\" , \"Response\" : \"Failure\" }]}";
                }

                return result;
            }
            catch (Exception ex)
            {
               return "{\"Response\" : [{\"Responsecode\" : \"0\" , \"Response\" : \"Failure\" }]}";
            }
        }

        public string GetSingleTariffDetailForActivationService(string LoginID, string DistributorID, string ClientTypeID, string TariffID, string Month, string Action)
        {
            DataSet ds = null;
            try
            {
                string result;
                DBTariff dbt = new DBTariff();

                ds = dbt.GetSingleTariffDetailForActivation(Convert.ToInt32(LoginID), Convert.ToInt32(DistributorID), Convert.ToInt32(ClientTypeID), Convert.ToInt32(TariffID), Convert.ToInt32(Month), Action);


                if (ds.Tables[0].Rows.Count > 0)
                {
                    result = cGeneral.GetJson(ds.Tables[0]);
                    result = "{\"Response\" : [{\"Responsecode\" : \"0\" , \"Response\" : \"Success\"}] , \"Data\" : " + result + "}";
                }
                else
                {
                    result = "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \"Failure\" }]}";
                }

                return result;
            }
            catch (Exception ex)
            {
                return "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \"Failure\" }]}";
            }
        }

        public string ActivateSIM(string ClientTypeID, string DistributorID, string TariffID, string SimNumber, string TariffAmount, string LoginID, string EmailID, string ZipCode,string VoucherPIN="", string TopUP="")
        {
            try
            {
                string result;
                Boolean IsBalance = false;
                Boolean IsSimStatus = false;
                string SimStatus = "";
                string TRANSACTIONID;
                double TariffAmt = Convert.ToDouble(TariffAmount);

                cSIM s = new cSIM();
                DBDistributor dis = new DBDistributor();
                cGeneral.LogSteps(DistributorID, System.Web.Hosting.HostingEnvironment.MapPath("~\\CheckBalancelog.txt"));
                DataSet ds = dis.GetSingleDistributor(Convert.ToInt32(DistributorID));

                if (ds.Tables[0].Rows.Count > 0)
                {
                    double BalanceAmnt = Convert.ToDouble(ds.Tables[0].Rows[0]["AccountBalance"]);
                    if (TariffAmt > BalanceAmnt)
                    {
                        IsBalance = false;
                    }
                    else
                    {
                        IsBalance = true;

                        DataSet dsSim = s.CheckSimActivation(Convert.ToInt32(DistributorID), Convert.ToInt32(ClientTypeID), SimNumber, "Activate");

                        if (dsSim.Tables[0].Rows.Count > 0)
                        {
                            SimStatus = Convert.ToString(dsSim.Tables[0].Rows[0][0]);
                            int id = Convert.ToInt32(dsSim.Tables[1].Rows[0]["TRANSACTIONID"]);
                            TRANSACTIONID = id.ToString("00000");
                            TRANSACTIONID = "AHP" + TRANSACTIONID;
                            if (SimStatus == "Ready to Activation")
                            {
                                IsSimStatus = true;

                                string resp = ActivateSim(DistributorID, LoginID, TariffID, EmailID, TRANSACTIONID, ClientTypeID, SimNumber, ZipCode);

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
                                                    SendMail(EmailID, "Sim Activation", ALLOCATED_MSISDN, SimNumber, "");

                                                    DBPayment sp = new DBPayment();
                                                    sp.ChargedAmount = Convert.ToDecimal(TariffAmount);
                                                    sp.PaymentType = 4;
                                                    sp.PayeeID = Convert.ToInt32(DistributorID);
                                                    sp.PaymentFrom = 9;
                                                    sp.ActivationType = 6;
                                                    sp.ActivationStatus = 15;
                                                    sp.ActivationVia = 17;
                                                    sp.ActivationResp = resp;
                                                    sp.ActivationRequest = RequestRes;
                                                    sp.TariffID = Convert.ToInt32(TariffID);
                                                    sp.ALLOCATED_MSISDN = ALLOCATED_MSISDN;
                                                    sp.TransactionId = TRANSACTIONID;
                                                    sp.PaymentMode = "Distributor Activation";
                                                    sp.TransactionStatusId = 24;
                                                    sp.TransactionStatus = "Success";

                                                    SPayment sp1 = new SPayment();
                                                    sp1.ChargedAmount = Convert.ToDecimal(TariffAmount);
                                                    sp1.PaymentType = 4;
                                                    sp1.PayeeID = Convert.ToInt32(DistributorID);
                                                    sp1.PaymentFrom = 9;
                                                    sp1.ActivationType = 6;
                                                    sp1.ActivationStatus = 15;
                                                    sp1.ActivationVia = 17;
                                                    sp1.ActivationResp = resp;
                                                    sp1.ActivationRequest = RequestRes;
                                                    sp1.TariffID = Convert.ToInt32(TariffID);
                                                    sp1.ALLOCATED_MSISDN = ALLOCATED_MSISDN;
                                                    sp1.TransactionId = TRANSACTIONID;
                                                    sp1.PaymentMode = "Distributor Activation";
                                                    sp1.TransactionStatusId = 24;
                                                    sp1.TransactionStatus = "Success";


                                                    int dist = Convert.ToInt32(DistributorID);
                                                    int loginID = Convert.ToInt32(LoginID);
                                                    string sim = SimNumber;
                                                    string zip = ZipCode;
                                                    string Language = "ENGLISH";
                                                    string ChannelID = "ENK";

                                                    int a = sp.UpdateAccountBalance(dist, loginID, sim, zip, ChannelID, Language);


                                                    result = "{\"Response\" : [{\"Responsecode\" : \"0\" , \"Response\" : \"Success\" , \"Message\" : \"SIM activation done successfully with Mobile Number - " + ALLOCATED_MSISDN + "\" }] }";
                                                }
                                                else
                                                {
                                                    SaveData(resp, 16, TRANSACTIONID, TariffAmount, DistributorID, TariffID, LoginID, SimNumber, ZipCode);
                                                    result = "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \"Failure\", \"Message\" : \"SIM activation Fail Please Try Again\" }]}";
                                                }
                                            }
                                            else
                                            {
                                                SaveData(resp, 16, TRANSACTIONID, TariffAmount, DistributorID, TariffID, LoginID, SimNumber, ZipCode);
                                                result = "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \"Failure\", \"Message\" : \"SIM activation Fail Please Try Again\" }]}";
                                            }
                                        }
                                        else
                                        {
                                            SaveData(resp, 16, TRANSACTIONID, TariffAmount, DistributorID, TariffID, LoginID, SimNumber, ZipCode);
                                            result = "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \"Failure\", \"Message\" : \"SIM activation Fail Please Try Again\" }]}";
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        SaveData(resp, 16, TRANSACTIONID, TariffAmount, DistributorID, TariffID, LoginID, SimNumber, ZipCode);
                                        result = "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \"Failure\", \"Message\" : \"SIM activation Fail Please Try Again\" }]}";
                                    }
                                }
                                else
                                {
                                    SaveData(resp, 16, TRANSACTIONID, TariffAmount, DistributorID, TariffID, LoginID, SimNumber, ZipCode);
                                    result = "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \"Failure\", \"Message\" : \"SIM activation Fail Please Try Again\" }]}";
                                }
                            }
                            else
                            {
                                IsSimStatus = false;
                            }

                        }

                    }

                    if (IsBalance == false)
                    {
                        result = "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \"Failure\", \"Message\" : \"Your Account Balance is Low. Please Recharge Your Balance\" }]}";
                    }
                    else
                    {
                        if (IsSimStatus == false)
                        {
                            result = "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \"Failure\", \"Message\" : \"" + SimStatus + "\" }]}";
                        }
                        else
                        {
                            result = "{\"Response\" : [{\"Responsecode\" : \"0\" , \"Response\" : \"Success\", \"Message\" : \"" + SimStatus + "\" }]}";
                        }
                    }
                }
                else
                {
                    result = "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \"Failure\" }]}";
                }

                return result;
            }
            catch (Exception ex)
            {
                return "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \"Failure\" }]}";
            }
        }

        #endregion
        #region "Andriod  Shadab Ali-20-Jun-2017"

        public string SubscriberForgetPassword(string EmailID)
        {
            try
            {
                string result;
                RechargeAndroid dis = new RechargeAndroid();
                DataSet ds = dis.SubscriberForgetPassword(EmailID);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    string Response = Convert.ToString(ds.Tables[0].Rows[0]["Response"]);
                    string message = Convert.ToString(ds.Tables[0].Rows[0]["Message"]);
                    if (Response == "success")
                    {

                        string EmailIDs = Convert.ToString(ds.Tables[0].Rows[0]["EmailID"]);
                        string pwd = Convert.ToString(ds.Tables[0].Rows[0]["Password"]);
                        SendMailForgetPassword(EmailIDs, "Forget Password", EmailID, pwd);
                        result = "{\"Response\" : [{\"Responsecode\" : \"0\" , \"Response\" : \"Password has been send to your mail box.\" }]}";
                       
                    }
                    else
                    {
                        result = "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \"Invaild EmailID\" }]}";
                    }
                }
                else
                {
                    result = "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \"Failure\" }]}";
                }
                return result;
                 
            }
            catch (Exception ex)
            {
                
                return "{\"Response\" : [{\"Responsecode\" : \"2\" , \"Response\" : \"Something Went Wrong..\" }]}";

            }
        }
        public void SendMailForgetPassword(string SendTo, string Subject, string UserID, string pass)
        {
            try
            {
                 
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient();
                string LogoUrl = "http://www.activatemysim.net/img/logo.png";
                

                //string MailAddress = ConfigurationManager.AppSettings.Get("Fromail");
                //string PassWord = ConfigurationManager.AppSettings.Get("Password");
                string MailAddress = "ENK.sim@gmail.com";
                string PassWord = "9711679656";
                string Host = "smtp.gmail.com";
             

                //string hosturi = GetAppUrl(HttpContext.Current.Request.Url, "approvals.aspx");
                string hosturi = "https://www.activatemysim.net/approvals.aspx";
                
                hosturi = hosturi + (hosturi.Contains("?") ? "&" : "?");

                mail.From = new MailAddress(MailAddress);
                mail.To.Add(SendTo);
                TimeSpan ts = new TimeSpan(8, 0, 0);
                mail.Subject = Subject + " " + DateTime.UtcNow.Subtract(ts).ToString();

                StringBuilder sb = new StringBuilder();
                sb.Append("<html>");
                sb.Append("<body style=”color:grey; font-size:15px;”>");
                sb.Append("<font face=”Helvetica, Arial, sans-serif”>");

                sb.Append("<div style=”position:absolute; height:200px; width:100px; background-color:0d1d36; padding:30px;”>");

                sb.Append("</div>");

                sb.Append("<div style=”background-color: #ece8d4; width:600px; height:200px; padding:30px; margin-top:30px;”>");
                sb.Append("<img src=" + LogoUrl + " />");
                sb.Append("<div style='border:1px solid black;padding-left: 24px;width: 388px; BORDER-RADIUS: 25px;font-style:italic;'>");
                

                sb.Append("<p>Dear  Subscriber,<p>");

                sb.Append("<p>Please find the new credentials and get started.</p>");
                sb.Append("<p>");
                

                sb.Append("Username: " + UserID + "<br>");
                //sb.Append("Password: " + pass + "<br>");

                string Type = "ForgotPassword";

                sb.Append("<p>&nbsp;</p><p><div style='font-family: sans-serif;'>" +
                          "<a  id='btnapprove' style='background-color:forestgreen;color:white; font-size: larger;border-style: double;text-decoration: none; padding: 10px;' href=' " +
                          hosturi + "req=" + HttpUtility.UrlEncode(Encryption.Encrypt(Type)) + "&id=" + HttpUtility.UrlEncode(Encryption.Encrypt(UserID.ToString())) +
                          "&act=" + HttpUtility.UrlEncode(Encryption.Encrypt(1.ToString())) + "&token=" + HttpUtility.UrlEncode(Encryption.Encrypt(Type + UserID.ToString() + 1.ToString())) +
                          "'>Reset Password</a>&nbsp;&nbsp;");
                   

                sb.Append("<br/>");

                sb.Append("</div>");
                

                sb.Append("<br/>");
                sb.Append("<p>Your password is secure show don't share it</p>");

                sb.Append("<p>Use this link for login</p>");
                
                sb.Append("<br/>");
                sb.Append("<p>Sincerely,");
                sb.Append("<p>" + ConfigurationManager.AppSettings.Get("COMPANY_NAME") + "</p>");
                sb.Append("<p>Thank you</p>");
                sb.Append("<p>----------------------------------------------------------------");
                sb.Append("<p>PROTECT YOUR PASSWORD");

                sb.Append("<p>NEVER give your password to anyone, including " + ConfigurationManager.AppSettings.Get("SHORT_COMPANY_NAME") + " employees. ");
                sb.Append("Protect yourself against fraudulent websites by opening a new web browser ");
                sb.Append("(e.g. Internet Explorer or Firefox) and typing in the " + ConfigurationManager.AppSettings.Get("SHORT_COMPANY_NAME") + " URL every time you log in to your account.");
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

                SmtpServer.Host = Host;
                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential(MailAddress, PassWord);
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);

            }
            catch (Exception ex)
            {

            }
        }
        private string GetAppUrl(Uri uri, string _targetpage)
        {
            string baseUrl = string.Empty;
            try
            {
                baseUrl = uri.Scheme + "://" + uri.Authority;
                if (baseUrl.EndsWith("/"))
                    baseUrl = baseUrl.Substring(0, baseUrl.Length - 1);
                baseUrl += HttpContext.Current.Request.ApplicationPath;
                if (!baseUrl.EndsWith("/"))
                    baseUrl += "/";
            }
            catch
            { }
            return (baseUrl + _targetpage);
        }



        public string GetNetwork()
        {
            try
            {
                string result;
                RechargeAndroid dis = new RechargeAndroid();
                //  cGeneral.LogSteps(DistributorID, System.Web.Hosting.HostingEnvironment.MapPath("~\\CheckBalancelog.txt"));
                DataSet ds = dis.GetNetworkApp();

                if (ds.Tables[0].Rows.Count > 0)
                {
                    result = cGeneral.GetJson(ds.Tables[0]);
                   // result = "{\"Response\" : [{\"Responsecode\" : \"0\" , \"Response\" : \"Success\" , \"Data\" : \"" + result + "\" }] }";
                    result = "{\"Response\" : [{\"Responsecode\" : \"0\" , \"Response\" : \"Success\"}] , \"Data\" : " + result + "}";
                }
                else
                {
                    result = "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \"Failure\" }]}";
                }

                return result;
            }
            catch (Exception ex)
            {
                cGeneral.LogSteps("GetNetwork", System.Web.Hosting.HostingEnvironment.MapPath("~\\AndriodRecharge.txt"));
                return "{\"Response\" : [{\"Responsecode\" : \"2\" , \"Response\" : \"Something Went Wrong..\" }]}";

            }
        }
        public string GetState()
        {
            try
            {
                string result;
                RechargeAndroid dis = new RechargeAndroid();
                //  cGeneral.LogSteps(DistributorID, System.Web.Hosting.HostingEnvironment.MapPath("~\\CheckBalancelog.txt"));
                DataSet ds = dis.GetStateApp();

                if (ds.Tables[0].Rows.Count > 0)
                {
                    result = cGeneral.GetJson(ds.Tables[0]);
                  
                    result = "{\"Response\" : [{\"Responsecode\" : \"0\" , \"Response\" : \"Success\"}] , \"Data\" : " + result + "}";
                }
                else
                {
                    result = "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \"Failure\" }]}";
                }

                return result;
            }
            catch (Exception ex)
            {
                cGeneral.LogSteps("GetStateApp", System.Web.Hosting.HostingEnvironment.MapPath("~\\AndriodRecharge.txt"));
                return "{\"Response\" : [{\"Responsecode\" : \"2\" , \"Response\" : \"Something Went Wrong..\" }]}";

            }
        }
        public string GetRechargePlan(int NetworkID,string State )
        {
            try
            {
                string result;
                RechargeAndroid dis = new RechargeAndroid();
                // cGeneral.LogSteps(DistributorID, System.Web.Hosting.HostingEnvironment.MapPath("~\\CheckBalancelog.txt"));
                DataSet ds = dis.GetRechargePlan(NetworkID, State);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    result = cGeneral.GetJson(ds.Tables[0]);
                  //  result = "{\"Response\" : [{\"Responsecode\" : \"0\" , \"Response\" : \"Success\" , \"Data\" : \"" + result + "\" }] }";
                    result = "{\"Response\" : [{\"Responsecode\" : \"0\" , \"Response\" : \"Success\"}] , \"Data\" : " + result + "}";
                }
                else
                {
                    result = "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \"Failure\" }]}";
                }

                return result;
            }
            catch (Exception ex)
            {
                cGeneral.LogSteps("GetRechargePlan", System.Web.Hosting.HostingEnvironment.MapPath("~\\AndriodRecharge.txt"));
                return "{\"Response\" : [{\"Responsecode\" : \"2\" , \"Response\" : \"Something Went Wrong..\" }]}";

            }
        }

        public string CreateSubscriberUser( string EmailID, string MobileNumber, string Password, string UserType)
        {
            try
            {
                RechargeAndroid svc = new RechargeAndroid();
                string MSG=EmailID + " " + MobileNumber + " " + Password + " " + UserType;
                cGeneral.LogSteps(MSG, System.Web.Hosting.HostingEnvironment.MapPath("~\\AndriodUserlog.txt"));
                
                string result;
                
             

                DataSet ds = svc.CreateSuscriberUser("", "", EmailID, MobileNumber, Password, UserType);
                if (ds.Tables.Count > 0)
                {
                  string   LoginUrl = "";
                  string OTP = ds.Tables[0].Rows[0]["OTPCode"].ToString();
                  string IsValid = ds.Tables[0].Rows[0]["IsValid"].ToString();
                  if (IsValid == "0")
                  {
                      result = cGeneral.GetJson(ds.Tables[0]);
                      SendMailCreateUser(EmailID.Trim(), "Login Detail", "", EmailID.Trim(), Password.Trim(), LoginUrl, OTP);

                      //result = "{\"Response\" : [{\"Responsecode\" : \"0\" , \"Response\" : \"Success\" }]}";
                      result = "{\"Response\" : [{\"Responsecode\" : \"0\" , \"Response\" : \"Success\"}] , \"Data\" : " + result + "}";
                  }
                  else {

                      result = "{\"Response\" : [{\"Responsecode\" : \"3\" , \"Response\" : \"Sorry ! EmailID  already exists\" }]}";
                  }
                 
                }
                else
                {
                    result = "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \"Failure\" }]}";
                }

                return result;
            }
            catch (Exception ex)
            {

               return "{\"Response\" : [{\"Responsecode\" : \"2\" , \"Response\" : \"Something wrong\" }]}";
            }
        }
        public string ValidateSubscriberLogin(string UserName, string Pwd)
        {
            try
            {
                string result;
                 
                RechargeAndroid ud = new RechargeAndroid();
                cGeneral.LogSteps(UserName + " " + Pwd, System.Web.Hosting.HostingEnvironment.MapPath("~\\AndriodUserlog.txt"));
               // string pass = Encryption.Encrypt(Pwd);
                DataSet ds = ud.ValidateSuscriberLogin(UserName, Pwd);

 
                if (ds.Tables[0].Rows.Count > 0)
                {

                    result = cGeneral.GetJson(ds.Tables[0]);
                  
                  
                        result = "{\"Response\" : [{\"Responsecode\" : \"0\" , \"Response\" : \"Success\"}] , \"Data\" : " + result + "}";
                    
                }
                else
                {
                    result = "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \"Failure\" }]}";
                }

                return result;


            }
            catch (Exception ex)
            {
                return "{\"Response\" : [{\"Responsecode\" : \"2\" , \"Response\" : \"something wrong\" }]}";
          
            }
        }
        public string ValidateSubscriberFacebookLogin(string EmailID)
        {
            try
            {
                string result;

                RechargeAndroid ud = new RechargeAndroid();
                cGeneral.LogSteps(EmailID, System.Web.Hosting.HostingEnvironment.MapPath("~\\AndriodUserlog.txt"));
                // string pass = Encryption.Encrypt(Pwd);
                DataSet ds = ud.ValidateSubscriberFacebookLogin(EmailID);


                if (ds.Tables[0].Rows.Count > 0)
                {
                    result = cGeneral.GetJson(ds.Tables[0]);
                  
                     
                        result = "{\"Response\" : [{\"Responsecode\" : \"0\" , \"Response\" : \"Success\"}] , \"Data\" : " + result + "}";
                    
                }
                else
                {
                    result = "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \"Failure\" }]}";
                }

                return result;


            }
            catch (Exception ex)
            {
                return "{\"Response\" : [{\"Responsecode\" : \"2\" , \"Response\" : \"something wrong\" }]}";

            }
        }

        public string SubscriberUserVerification(string UserName, string OTP)
        {
            try
            {
                string result;

                RechargeAndroid ud = new RechargeAndroid();
                cGeneral.LogSteps(UserName + " " + OTP, System.Web.Hosting.HostingEnvironment.MapPath("~\\AndriodUserlog.txt"));
                // string pass = Encryption.Encrypt(Pwd);
                DataSet ds = ud.SubscriberUserVerification(UserName, OTP);


                if (ds.Tables[0].Rows.Count > 0)
                {
                    result = cGeneral.GetJson(ds.Tables[0]);
                    result = "{\"Response\" : [{\"Responsecode\" : \"0\" , \"Response\" : \"Success\"}] , \"Data\" : " + result + "}";
                }
                else
                {
                    result = "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \"Failure\" }]}";
                }

                return result;


            }
            catch (Exception ex)
            {
                return "{\"Response\" : [{\"Responsecode\" : \"2\" , \"Response\" : \"Failure\" }]}";
          
            }
        }

        public InitiatePaymentResponse InitiateTopupPayment(int loginID, int  DistributorID, string ChargedAmount)
        {
            try
            {
                DBPayment svc = new DBPayment();
                

                string MSG = loginID + " " + DistributorID + " " + ChargedAmount ;
                cGeneral.LogSteps(MSG, System.Web.Hosting.HostingEnvironment.MapPath("~\\PaypalTopup.txt"));
                   InitiatePaymentResponse result;
                   DataSet ds =  new  DataSet() ;

                   svc.ChargedAmount = Convert.ToDecimal(ChargedAmount);
                   svc.PaymentType = 3;
                   svc.PayeeID = Convert.ToInt32(DistributorID);
                   svc.PaymentFrom = 9;
                   svc.ActivationVia = 18;
                   svc.TransactionStatusId = 23;
                   svc.TransactionStatus = "Pending";
                   svc.PaymentMode = "PayPal Topup";
                   svc.TxnDate = DateTime.Now.ToString();
                   svc.Currency = 1;
                    int dist = DistributorID; 
                    ds= svc.InsertPaypalPaymentAPP(dist, loginID);

                  
                if (ds.Tables.Count > 0)
                {
                    string LoginUrl = "";
                    string  a = Convert.ToString(ds.Tables[0].Rows[0][0]);
                    result = new InitiatePaymentResponse
                    {
                        ResponseCode = "0",
                        Response = "Success",
                        PaymentID = Convert.ToString(ds.Tables[0].Rows[0][0])
                    };
                    //result = cGeneral.GetJson(ds.Tables[0]);
                        //result = "{\"Response\" : [{\"Responsecode\" : \"0\" , \"Response\" : \"Success\" }]}";
                    //result = "{\"Response\" : [{\"Responsecode\" : \"0\" , \"Response\" : \"Success\"}] , \"Data\" : " + result + "}";
                    

                }
                else
                {
                    result = new InitiatePaymentResponse
                    {
                        ResponseCode = "1",
                        Response = "No Record"
                    };
                    //result = "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \"Failure\" }]}";
                }

                return result;
            }
            catch (Exception ex)
            {

                return new InitiatePaymentResponse
                {
                    ResponseCode = "1",
                    Response = "Failure"
                };
            }
        }

        public ConfirmPaymentResponse UpdateTopupPayment(string Response,  string TxnID, int StatusID, string PaymentId, string loginID, string DistributorID, string ChargedAmount)
        {
            int a = 0;
            try
            {
                ConfirmPaymentResponse result;
                string MSG = loginID + " " + DistributorID + " " + ChargedAmount;
                cGeneral.LogSteps(MSG, System.Web.Hosting.HostingEnvironment.MapPath("~\\PaypalTopup.txt"));
              
                DBPayment sp = new DBPayment();

                if (PaymentId != null && PaymentId != "")
                {
                    if (TxnID == null)
                    {
                        TxnID = "";
                    }
                    if (loginID != "" && DistributorID != "")
                    {
                        if (StatusID != 25)
                        {
                            string MSG1 = "ChargedAmount:-" + ChargedAmount + "Response with paypal... Transaction Success" + " Reason :- " + Response;
                            cGeneral.LogSteps(MSG1, System.Web.Hosting.HostingEnvironment.MapPath("~\\PaypalTopup.txt"));
                            string[] sd = Response.Split('\n');
                            List<string> sdlist = new List<string>();


                            sp.PaymentId = Convert.ToInt32(PaymentId);
                            sp.ChargedAmount = Convert.ToDecimal(ChargedAmount);
                            sp.TxnId = TxnID;
                            sp.TxnAmount = "0"; //sd[27].Replace("payment_fee=", "");
                            sp.TransactionStatus = "Success";
                            sp.TransactionStatusId = StatusID;
                            sp.ReceiptId = "";
                            sp.PayerId = "";//sd[4].Replace("payer_id=", "");
                            sp.TxnDate = DateTime.Now.ToString();
                            sp.CheckSumm = Response;

                            int dist = Convert.ToInt32(DistributorID);
                            int LoginID = Convert.ToInt32(loginID);
                            a = sp.UpdatePaypalPaymentAPP(dist, LoginID);

                            result = new ConfirmPaymentResponse
                            {
                                ResponseCode = "0",
                                Response = "Success"
                            };

                            //result = "{\"Response\" : [{\"Responsecode\" : \"0\" , \"Response\" : \"Success\" }]}";

                           

                        }
                        else
                        {
                            string MSG1 = " ChargedAmount:-" + ChargedAmount + " Failure with paypal... Transaction failed" + " Reason :- " + Response;
                            cGeneral.LogSteps(MSG1, System.Web.Hosting.HostingEnvironment.MapPath("~\\PaypalTopup.txt"));

                            sp.PaymentId = Convert.ToInt32(PaymentId);
                            sp.ChargedAmount = Convert.ToDecimal(ChargedAmount);
                            sp.TxnId = TxnID;
                            sp.TxnAmount = "";
                            sp.TransactionStatus = "Fail";
                            sp.TransactionStatusId = 25;
                            sp.ReceiptId = "";
                            sp.PayerId = "";
                            sp.TxnDate = DateTime.Now.ToString();
                            sp.CheckSumm = Response;

                            int dist = Convert.ToInt32(DistributorID);
                            int LoginID = Convert.ToInt32(loginID);
                            a = sp.UpdatePaypalPaymentAPP(dist, LoginID);

                            result = new ConfirmPaymentResponse
                            {
                                ResponseCode = "1",
                                Response = "Failure"
                            };

                            //result = "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \"Failure\" }]}";

                            

                        }
                    }

                    else
                    {

                        string MSG1 = " ChargedAmount:-" + ChargedAmount + "Oooops, something went wrong with paypal... Transaction failed" + " Reason :- " + Response;
                        cGeneral.LogSteps(MSG1, System.Web.Hosting.HostingEnvironment.MapPath("~\\PaypalTopup.txt"));


                        sp.PaymentId = Convert.ToInt32(PaymentId);
                        sp.ChargedAmount = Convert.ToDecimal(ChargedAmount);
                        sp.TxnId = TxnID;
                        sp.TxnAmount = "";
                        sp.TransactionStatus = "Fail";
                        sp.TransactionStatusId = 25;
                        sp.ReceiptId = "";
                        sp.PayerId = "";
                        sp.TxnDate = DateTime.Now.ToString();
                        sp.CheckSumm = Response;

                        int dist = Convert.ToInt32(DistributorID);
                        int LoginID = Convert.ToInt32(loginID);
                        a = sp.UpdatePaypalPaymentAPP(dist, LoginID);


                        result = new ConfirmPaymentResponse
                        {
                            ResponseCode = "1",
                            Response = "Failure :- loginID is null"
                        };

                        //result = "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \"Failure :- loginID is null\" }]}";

                        
                    }
                }
                else
                {
                    //result = "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \"Failure :- PaymentId is null\" }]}";
                    result = new ConfirmPaymentResponse
                    {
                        ResponseCode = "1",
                        Response = "Failure :- PaymentId is null"
                    };


                }
                return result;
            }
            catch (Exception ex)
            {
                return new ConfirmPaymentResponse
                {
                    ResponseCode = "2",
                    Response = "Something Went Wrong"
                }; 
                     
            }
        }

        public void SendMailCreateUser(string SendTo, string Subject    ,string UserName, string UserID, string pass, string LoginUrl,string OTPCode)
        {
            try
            {
                //string LogoUrl = "http://" + HttpContext.Current.Request.Url.Host + HttpContext.Current.Request.ApplicationPath + "/img/logo.png";
                string LogoUrl = "http://www.activatemysim.net/img/logo.png";
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient();


                //string MailAddress = ConfigurationManager.AppSettings.Get("Fromail");
                //string PassWord = ConfigurationManager.AppSettings.Get("Password");
                //string Host = ConfigurationManager.AppSettings.Get("Host");

                string MailAddress = "ENK.sim@gmail.com";
                string PassWord = "9711679656";
                string Host = "smtp.gmail.com";

                //cGeneral.LogSteps(MailAddress + " " + (PassWord + " " + Host), System.Web.Hosting.HostingEnvironment.MapPath("~\\AndriodUserlog.txt"));

                mail.From = new MailAddress(MailAddress);
                mail.To.Add(SendTo);
                TimeSpan ts = new TimeSpan(8, 0, 0);
                mail.Subject = Subject + " " + DateTime.UtcNow.Subtract(ts).ToString();

                StringBuilder sb = new StringBuilder();
                sb.Append("<html>");
                sb.Append("<body style=”color:grey; font-size:15px;”>");
                sb.Append("<font face=”Helvetica, Arial, sans-serif”>");

                sb.Append("<div style=”position:absolute; height:200px; width:100px; background-color:0d1d36; padding:30px;”>");
               
                sb.Append("</div>");

               

                sb.Append("<div style=”background-color: #ece8d4; width:600px; height:200px; padding:30px; margin-top:30px;”>");
                sb.Append("<img src=" + LogoUrl + " />");
                //sb.Append("<p>Please find the new credentials and get started.</p>");
                sb.Append("<div style='border:1px solid black;padding-left: 24px;width: 388px; BORDER-RADIUS: 25px;font-style:italic;'>");
                sb.Append("<p>Dear Subscriber,<p>");

                sb.Append("<p>Please find the new credentials and get started.</p>");
                sb.Append("<p>");
                sb.Append("<br/>");

                sb.Append("Username: " + UserID + "<br/>");
                //sb.Append("Password: " + pass + "<br><br/>");
                sb.Append("OTP: " + OTPCode + "<br/>");
                sb.Append("<br/>");
                sb.Append("<p>Your password is secure show don't share it</p>");
                sb.Append("</div>");
               

                //sb.Append("<p>Use this link for login</p>");
               
                sb.Append("<br/>");
                sb.Append("<p>Sincerely,");
                sb.Append("<p>" + ConfigurationManager.AppSettings.Get("COMPANY_NAME") + "</p>");
                sb.Append("<p>Thank you</p>");
                sb.Append("<p>----------------------------------------------------------------<br/>");
                sb.Append("<p>PROTECT YOUR PASSWORD");

                sb.Append("<p>NEVER give your password to anyone, including " + ConfigurationManager.AppSettings.Get("SHORT_COMPANY_NAME") + " employees. ");
                sb.Append("Protect yourself against fraudulent websites by opening a new web browser ");
                sb.Append("(e.g. Internet Explorer or Firefox) and typing in the " + ConfigurationManager.AppSettings.Get("SHORT_COMPANY_NAME") + " URL every time you log in to your account.");
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

                SmtpServer.Host = Host;
                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential(MailAddress, PassWord);
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);

            }
            catch (Exception ex)
            {
                cGeneral.LogSteps("SendMailCreateUser-" + "Error", System.Web.Hosting.HostingEnvironment.MapPath("~\\AndriodUserlog.txt"));
            }
        }

        public string GetRechargeDistributorPlan(int NetworkID ,int DistributorID)
        {
            try
            {
                string result;
                RechargeAndroid dis = new RechargeAndroid();
                // cGeneral.LogSteps(DistributorID, System.Web.Hosting.HostingEnvironment.MapPath("~\\CheckBalancelog.txt"));
                DataSet ds = dis.GetRechargeDistributorPlan(NetworkID, DistributorID);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    result = cGeneral.GetJson(ds.Tables[0]);
                    //  result = "{\"Response\" : [{\"Responsecode\" : \"0\" , \"Response\" : \"Success\" , \"Data\" : \"" + result + "\" }] }";
                    result = "{\"Response\" : [{\"Responsecode\" : \"0\" , \"Response\" : \"Success\"}] , \"Data\" : " + result + "}";
                }
                else
                {
                    result = "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \"Failure\" }]}";
                }

                return result;
            }
            catch (Exception ex)
            {
                cGeneral.LogSteps("GetRechargeDistributorPlan", System.Web.Hosting.HostingEnvironment.MapPath("~\\AndriodRecharge.txt"));
                return "{\"Response\" : [{\"Responsecode\" : \"2\" , \"Response\" : \"Something Went Wrong..\" }]}";

            }
        }

        public string InitiatePaymentWaletRecharge(int loginID, int DistributorID, string ChargedAmount)
        {
            try
            {
                DBPayment svc = new DBPayment();


                string MSG = loginID + " " + DistributorID + " " + ChargedAmount;
                cGeneral.LogSteps(MSG, System.Web.Hosting.HostingEnvironment.MapPath("~\\PaypalTopup.txt"));
                string result;
                DataSet ds = new DataSet();

                svc.ChargedAmount = Convert.ToDecimal(ChargedAmount);
                svc.PaymentType = 26;
                svc.PayeeID = Convert.ToInt32(DistributorID);
                svc.PaymentFrom = 9;
                svc.ActivationVia = 29;
                svc.TransactionStatusId = 23;
                svc.TransactionStatus = "Pending";
                svc.PaymentMode = "Cash";
                svc.TxnDate = DateTime.Now.ToString();
                svc.Currency = 1;
                int dist = DistributorID;
                ds = svc.InitiatePaymentWaletRechargeApp(dist, loginID);


                if (ds.Tables.Count > 0)
                {
                    string LoginUrl = "";
                    string PaymentId = Convert.ToString(ds.Tables[0].Rows[0]["PaymentId"]);
                    result = cGeneral.GetJson(ds.Tables[0]);
                    if (PaymentId != "0")
                    {
                        //result = "{\"Response\" : [{\"Responsecode\" : \"0\" , \"Response\" : \"Success\" }]}";
                        result = "{\"Response\" : [{\"Responsecode\" : \"0\" , \"Response\" : \"Success\"}] , \"Data\" : " + result + "}";
                    }
                    else {

                        result = "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \"sorry !! your walet account balance is low\" }]}";
                    }

                }
                else
                {
                    result = "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \"Failure\" }]}";
                }

                return result;
            }
            catch (Exception ex)
            {

                return "{\"Response\" : [{\"Responsecode\" : \"2\" , \"Response\" : \"Something wrong\" }]}";
            }
        }

        public string InitiatePaymentPaypalRecharge(int loginID, int DistributorID, string ChargedAmount)
        {
            try
            {
                DBPayment svc = new DBPayment();


                string MSG = loginID + " " + DistributorID + " " + ChargedAmount;
                cGeneral.LogSteps(MSG, System.Web.Hosting.HostingEnvironment.MapPath("~\\PaypalTopup.txt"));
                string result;
                DataSet ds = new DataSet();

                svc.ChargedAmount = Convert.ToDecimal(ChargedAmount);
                svc.PaymentType = 26;
                svc.PayeeID = Convert.ToInt32(DistributorID);
                svc.PaymentFrom = 9;
                svc.ActivationVia = 31;
                svc.TransactionStatusId = 23;
                svc.TransactionStatus = "Pending";
                svc.PaymentMode = "Cash";
                svc.TxnDate = DateTime.Now.ToString();
                svc.Currency = 1;
                int dist = DistributorID;
                ds = svc.InitiatePaymentPaypalRechargeApp(dist, loginID);


                if (ds.Tables.Count > 0)
                {
                    string LoginUrl = "";
                    string PaymentId = Convert.ToString(ds.Tables[0].Rows[0]["PaymentId"]);
                    result = cGeneral.GetJson(ds.Tables[0]);
                    if (PaymentId != "0")
                    {
                        //result = "{\"Response\" : [{\"Responsecode\" : \"0\" , \"Response\" : \"Success\" }]}";
                        result = "{\"Response\" : [{\"Responsecode\" : \"0\" , \"Response\" : \"Success\"}] , \"Data\" : " + result + "}";
                    }
                    else
                    {

                        result = "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \"Failure\" }]}";
                    }

                }
                else
                {
                    result = "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \"Failure\" }]}";
                }

                return result;
            }
            catch (Exception ex)
            {

                return "{\"Response\" : [{\"Responsecode\" : \"2\" , \"Response\" : \"Something wrong\" }]}";
            }
        }

        public string Recharge(int PaymentID,string NetworkID, string TariffCode, string MobileNo, string TotalAmount, string EmailID, string RechargeAmount, string State, string ZIPCode, string TxnID, string Tax, string Regulatery, int DistributorID, int LoginID,string RechargeVia,string PlanDescription,int IsWalet)
        {
           
            try
            {
                if(RechargeVia=="")
                {
                    RechargeVia = "29"; 
                }

                string result = ""; ;
                string ss = "";
                net.emida.ws.webServicesService ws = new webServicesService();
                RechargeAndroid svc = new RechargeAndroid();
                string simNumber = MobileNo;

                try
                {
                    string ss1 = ws.Login2("01", "A&HPrepaid", "95222", "1");
                }
                catch (Exception ex)
                {
                   string TransactionID = Convert.ToString(0);
                   string RechargeStatus = "28";
                   // string RechargeVia = "29";
                   int TransactionStatusID = 28;
                   int s1 = svc.UpdateAccountBalanceAfterRechargeAPP(Convert.ToInt32(PaymentID), Convert.ToInt32(NetworkID), Convert.ToInt32(TariffCode), Convert.ToString(simNumber), Convert.ToDecimal(TotalAmount), DistributorID, Convert.ToString(""), RechargeStatus, RechargeVia, "", "", LoginID, 9, "Cash", TransactionID, 1, "App Recharge  Fail", TransactionStatusID, "0", State, TxnID, Tax, Convert.ToString(RechargeAmount), "", "Android", Regulatery.ToString(), IsWalet);
                   SendFailureMailRecharge(EmailID, (simNumber + "  Recharge Failed!"), simNumber, Convert.ToInt32(NetworkID), TariffCode, RechargeAmount, PlanDescription, ex.Message);
                   return result = "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \"" + ex.Message + "\"}]}";
                  
                }
       

               

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

                string InvoiceNo = DateTime.Now.ToString().GetHashCode().ToString("X");
                InvoiceNo = "RC" + InvoiceNo;
                DataSet dsDuplicate = new DataSet();
                string Number = "";
                dsDuplicate = svc.CheckRechargeDuplicate(Convert.ToInt32(NetworkID), simNumber, Convert.ToInt32(TariffCode), Number);
                string Request = "01, 3756263, 1234, " + TariffCode + "," + simNumber + "," + RechargeAmount + "," + InvoiceNo + ", 1";

                if (dsDuplicate != null)
                {
                    if (Convert.ToInt32(dsDuplicate.Tables[0].Rows[0]["IsValid"]) == 0)
                    {

                        ss = "Mobile- " + MobileNo + "|" + "AmountPay- " + TotalAmount + "|" + "Network- " + NetworkID + "|" + "TariffCode- " + TariffCode + "|" + "RechargeAmount- " + RechargeAmount + "|" + "State- " + State + "|" + "ZIPCode- " + ZIPCode + "|" + "TxnID- " + TxnID + "| Tax- " + Tax;
                        Log2("Subscriber Recharge", "Subscriber Recharge");
                        Log2(ss, "Subscriber Recharge Network");

                        //  cGeneral.LogSteps(UserName + " " + Pwd, System.Web.Hosting.HostingEnvironment.MapPath("~\\ValidateLoginServicelog.txt"));
                        Log2(dsDuplicate.Tables[0].Rows[0]["Msg"].ToString(), "Show Check Message");
                        Log2(dsDuplicate.Tables[0].Rows[0]["IsValid"].ToString(), "IsValid");
                        Log2(simNumber, "SimNumber");
                        Log2(TotalAmount, "AmountPay");
                        Log2(RechargeAmount, "RechageAmount");
                        Log2("", "split");


                        string ss2 = ws.PinDistSale("01", "3756263", "1234", TariffCode, simNumber, RechargeAmount, InvoiceNo, "1");

                        // string ss2 = "<PinDistSaleResponse><Version>01</Version><InvoiceNo>RCED362DA8</InvoiceNo><ResponseCode>00</ResponseCode><Pin>2093954866</Pin><ControlNo>181477130</ControlNo><CarrierControlNo></CarrierControlNo><CustomerServiceNo>2093954866</CustomerServiceNo><TransactionDateTime>07/10/2017 23:22:21:660</TransactionDateTime><H2H_RESULT_CODE>0</H2H_RESULT_CODE><ResponseMessage>Lyca Mobile RTR $23.00 Unlimited National Plan&#x0a;&#x0a;DATE AND TIME: 07/10/2017 23:22:21:660&#x0a;&#x0a;ACCOUNT: (209) 395-4866&#x0a;&#x0a;TRANS ID: 312855630&#x0a;&#x0a;To recharge your balance: DIAL *611*PIN# OR 611&#x0a;from your Lycamobile and follow instructions.&#x0a;Customer Services: Dial 612 from your Lycamobile&#x0a;or 1 845 301 1612&#x0a;&#x0a;Recargue su saldo: Marque  *611*PIN# o 611 desde&#x0a;su Lycamobile y siga instrucciones. Atencion al&#x0a;Cliente:Marque 612 desde su Lycamobile o&#x0a;1 845 301 1612&#x0a;&#x0a;This voucher is valid for use for 120 days from&#x0a;and including the date of its redemption.&#x0a;&#x0a;Este cupon es valido por 120 dias a partir la&#x0a;fecha de su redencion.&#x0a;&#x0a;Voucher/Cupon No.: &#x0a;&#x0a;Not refundable/No reembolsable&#x0a;Not exchangeable/No intercambiable&#x0a;&#x0a;For Terms and Conditions and latest offers&#x0a;please visit:  www.lycamobile.com</ResponseMessage><TransactionId>312855630</TransactionId></PinDistSaleResponse>";
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
                                    DataSet dsTransaction = svc.SaveTransactionDetails(Convert.ToInt32(NetworkID), Convert.ToInt32(TariffCode), "11", Convert.ToString(PinNumber), simNumber, InvoiceNo, Convert.ToString(RechargeAmount), Currency, State, ZIPCode, "305", Convert.ToInt32(LoginID), Convert.ToString(TotalAmount));
                                    if (dsTransaction != null)
                                    {

                                        string RechargeStatus = "27";
                                        // string @RechargeVia = "29";
                                        int TransactionStatusID = 27;

                                        SendMailRecharge(EmailID, (simNumber + "  Recharge successful!"), PinNumber, simNumber, "", Convert.ToInt32(NetworkID));

                                        int s1 = svc.UpdateAccountBalanceAfterRechargeAPP(Convert.ToInt32(PaymentID), Convert.ToInt32(NetworkID), Convert.ToInt32(TariffCode), Convert.ToString(simNumber), Convert.ToDecimal(TotalAmount), DistributorID, Convert.ToString(ZIPCode), RechargeStatus, RechargeVia, Request, ss2, LoginID, 9, "Cash", TransactionID, 1, "App Recharge  Successfully", TransactionStatusID, PinNumber, State, TxnID, Tax, Convert.ToString(RechargeAmount), InvoiceNo, "Android", Regulatery.ToString(), IsWalet);
                                        //  MakeReceipt(strResponse);

                                        Log2("Recharge Transaction Success", "Reason");

                                        Log2(Request, "Request");
                                        Log2(ResponseCode, "Response");
                                        Log2(ResponseMessage, "ResponseMessage");
                                        Log2(simNumber, "SimNumber");
                                        Log2(TotalAmount, "AmountPay");
                                        Log2(State, "State-City");
                                        Log2(ZIPCode, "Zip");
                                        Log2(RechargeAmount, "RechargeAmount");
                                        Log2(ss, "Detail");
                                        Log2("", "split");

                                        ws.Logout("01", "A&HPrepaid", "95222", "1");
                                        string str = "Mobile Number  " + simNumber + " Recharge  successfully ,  Pin Number " + PinNumber;


                                        result = "{\"Response\" : [{\"Responsecode\" : \"0\" , \"Response\" : \"" + str + "\"}]}";

                                        //result = "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \"Success\"}] , \"Data\" : " + str + "}";

                                        return result;

                                    }
                                    else
                                    {


                                        string TransactionIDs = Convert.ToString(0);
                                        string RechargeStatus = "28";
                                        // string RechargeVia = "29";
                                        int TransactionStatusID = 28;
                                        int s1 = svc.UpdateAccountBalanceAfterRechargeAPP(Convert.ToInt32(PaymentID), Convert.ToInt32(NetworkID), Convert.ToInt32(TariffCode), Convert.ToString(simNumber), Convert.ToDecimal(TotalAmount), DistributorID, Convert.ToString(""), RechargeStatus, RechargeVia, Request, ss2, LoginID, 9, "Cash", TransactionIDs, 1, "App Recharge  Fail", TransactionStatusID, PinNumber, State, TxnID, Tax, Convert.ToString(RechargeAmount), InvoiceNo, "Android", Regulatery.ToString(), IsWalet);


                                        SendFailureMailRecharge(EmailID, (simNumber + "  Recharge Failed!"), simNumber, Convert.ToInt32(NetworkID), TariffCode, RechargeAmount, PlanDescription, ResponseMessage);

                                        result = "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \"" + ResponseMessage + "\"}]}";


                                        return result;

                                    }




                                }
                                else
                                {
                                    string TransactionID = Convert.ToString(0);
                                    string RechargeStatus = "28";
                                    // string RechargeVia = "29";
                                    int TransactionStatusID = 28;
                                    int s1 = svc.UpdateAccountBalanceAfterRechargeAPP(Convert.ToInt32(PaymentID), Convert.ToInt32(NetworkID), Convert.ToInt32(TariffCode), Convert.ToString(simNumber), Convert.ToDecimal(TotalAmount), DistributorID, Convert.ToString(""), RechargeStatus, RechargeVia, Request, ss2, LoginID, 9, "Cash", TransactionID, 1, "App Recharge  Fail", TransactionStatusID, PinNumber, State, TxnID, Tax, Convert.ToString(RechargeAmount), InvoiceNo, "Android", Regulatery.ToString(), IsWalet);


                                    Log2("Recharge Transaction Fail", "Reason");
                                    Log2(Request, "Request");
                                    Log2(ResponseCode, "Response");
                                    Log2(ResponseMessage, "ResponseMessage");
                                    Log2(simNumber, "SimNumber");
                                    Log2(TotalAmount, "AmountPay");
                                    Log2(TariffCode, "TariffCode");
                                    Log2(State, "State-City");
                                    Log2(ZIPCode, "Zip");
                                    Log2(RechargeAmount, "RechargeAmount");
                                    Log2("", "split");

                                    SendFailureMailRecharge(EmailID, (simNumber + "  Recharge Failed!"), simNumber, Convert.ToInt32(NetworkID), TariffCode, RechargeAmount, PlanDescription, ResponseMessage);

                                    // result = "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \"Failure\"}] , \"Data\" : " + ResponseMessage + "}";
                                    result = "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \"" + ResponseMessage + "\"}]}";
                                    return result;
                                }


                            }

                            else
                            {

                                string TransactionID = Convert.ToString(0);
                                string RechargeStatus = "28";
                                // string RechargeVia = "29";
                                int TransactionStatusID = 28;
                                int s1 = svc.UpdateAccountBalanceAfterRechargeAPP(Convert.ToInt32(PaymentID), Convert.ToInt32(NetworkID), Convert.ToInt32(TariffCode), Convert.ToString(simNumber), Convert.ToDecimal(TotalAmount), DistributorID, Convert.ToString(""), RechargeStatus, RechargeVia, Request, ss2, LoginID, 9, "Cash", TransactionID, 1, "App Recharge  Fail", TransactionStatusID, "", State, TxnID, Tax, Convert.ToString(RechargeAmount), InvoiceNo, "Android", Regulatery.ToString(), IsWalet);

                                Log2("NO Record from API PinDistSale", "PinDistSale");
                                Log2(Request, "Request");

                                Log2(simNumber, "SimNumber");
                                Log2(TotalAmount, "AmountPay");
                                Log2(TariffCode, "TariffCode");
                                Log2(State, "State-City");
                                Log2(ZIPCode, "Zip");
                                Log2(RechargeAmount, "RechargeAmount");
                                Log2("", "split");
                                SendFailureMailRecharge(EmailID, (simNumber + "  Recharge Failed!"), simNumber, Convert.ToInt32(NetworkID), TariffCode, RechargeAmount, PlanDescription, "No Record from Recharge API PinDistSale");

                                result = "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \"Failure:-NO Record from Recharge API PinDistSale\" }]}";

                                return result;
                            }


                        }
                        else
                        {

                            string TransactionID = Convert.ToString(0);
                            string RechargeStatus = "28";
                            // string RechargeVia = "29";
                            int TransactionStatusID = 28;
                            int s1 = svc.UpdateAccountBalanceAfterRechargeAPP(Convert.ToInt32(PaymentID), Convert.ToInt32(NetworkID), Convert.ToInt32(TariffCode), Convert.ToString(simNumber), Convert.ToDecimal(TotalAmount), DistributorID, Convert.ToString(""), RechargeStatus, RechargeVia, Request, ss2, LoginID, 9, "Cash", TransactionID, 1, "App Recharge  Fail", TransactionStatusID, "", State, TxnID, Tax, Convert.ToString(RechargeAmount), InvoiceNo, "Android", Regulatery.ToString(), IsWalet);



                            Log2("NO Record from API PinDistSale", "PinDistSale");
                            Log2(Request, "Request");

                            Log2(simNumber, "SimNumber");
                            Log2(TotalAmount, "AmountPay");
                            Log2(TariffCode, "TariffCode");
                            Log2(State, "State-City");
                            Log2(ZIPCode, "Zip");
                            Log2(RechargeAmount, "RechargeAmount");
                            Log2("", "split");

                            SendFailureMailRecharge(EmailID, (simNumber + "  Recharge Failed!"), simNumber, Convert.ToInt32(NetworkID), TariffCode, RechargeAmount, PlanDescription, "No Record from Recharge API PinDistSale");

                            result = "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \"Failure:-NO Record from Recharge API PinDistSale\" }]}";
                            return result;
                        }

                    }

                    else
                    {
                        try
                        {
                            string TransactionID = Convert.ToString(0);
                            string RechargeStatus = "28";
                            // string RechargeVia = "29";
                            int TransactionStatusID = 28;
                            int s1 = svc.UpdateAccountBalanceAfterRechargeAPP(Convert.ToInt32(PaymentID), Convert.ToInt32(NetworkID), Convert.ToInt32(TariffCode), Convert.ToString(simNumber), Convert.ToDecimal(TotalAmount), DistributorID, Convert.ToString(""), RechargeStatus, RechargeVia, Request, "", LoginID, 9, "Cash", TransactionID, 1, "App Recharge  Fail", TransactionStatusID, "0", State, TxnID, Tax, Convert.ToString(RechargeAmount), InvoiceNo, "Android", Regulatery.ToString(), IsWalet);

                            Log2("Recharge Transaction Duplicate Recharge", "Duplicate Recharge");
                            Log2(dsDuplicate.Tables[0].Rows[0]["Msg"].ToString(), "Show Check Duplicate Message");
                            Log2(dsDuplicate.Tables[0].Rows[0]["IsValid"].ToString(), "IsValid");
                            Log2(simNumber, "SimNumber");
                            Log2(RechargeAmount, "AmountPay");
                            Log2("", "split");

                            string RechargeDate = Convert.ToString(dsDuplicate.Tables[0].Rows[0]["RechargeDate"]).ToString();
                            string amt = Convert.ToString(dsDuplicate.Tables[0].Rows[0]["Amount"]).ToString();

                            string str = "You already Recharged " + simNumber + "  with Amount " + amt + " at " + RechargeDate + " Successfully. If you want to recharge it again  please wait 5 min.";

                            SendFailureMailRecharge(EmailID, (simNumber + "  Recharge Failed!"), simNumber, Convert.ToInt32(NetworkID), TariffCode, RechargeAmount, PlanDescription, str);


                            // result = "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : " + str + "}]}";
                            result = "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \"" + str + "\"}]}";
                            return result;
                        }
                        catch { }




                    }
                }
                else
                {

                    string TransactionID = Convert.ToString(0);
                    string RechargeStatus = "28";
                    // string RechargeVia = "29";
                    int TransactionStatusID = 28;
                    int s1 = svc.UpdateAccountBalanceAfterRechargeAPP(Convert.ToInt32(PaymentID), Convert.ToInt32(NetworkID), Convert.ToInt32(TariffCode), Convert.ToString(simNumber), Convert.ToDecimal(TotalAmount), DistributorID, Convert.ToString(""), RechargeStatus, RechargeVia, Request, "", LoginID, 9, "Cash", TransactionID, 1, "App Recharge  Fail", TransactionStatusID, "0", State, TxnID, Tax, Convert.ToString(RechargeAmount), InvoiceNo, "Android", Regulatery.ToString(), IsWalet);
                    SendFailureMailRecharge(EmailID, (simNumber + "  Recharge Failed!"), simNumber, Convert.ToInt32(NetworkID), TariffCode, RechargeAmount, PlanDescription, "Duplicate Recharge Something is wrong");


                    return "{\"Response\" : [{\"Responsecode\" : \"2\" , \"Response\" : \"Duplicate Recharge Something is wrong\" }]}";
                }
                return result;
               
            }

            catch (Exception ex)
            {
                net.emida.ws.webServicesService ws = new webServicesService();
                RechargeAndroid svc = new RechargeAndroid();
              

                string TransactionID = Convert.ToString(0);
                string RechargeStatus = "28";
                // string RechargeVia = "29";
                int TransactionStatusID = 28;
                int s1 = svc.UpdateAccountBalanceAfterRechargeAPP(Convert.ToInt32(PaymentID), Convert.ToInt32(NetworkID), Convert.ToInt32(TariffCode), Convert.ToString(MobileNo), Convert.ToDecimal(TotalAmount), DistributorID, Convert.ToString(""), RechargeStatus, RechargeVia, "", "", LoginID, 9, "Cash", TransactionID, 1, "App Recharge  Fail", TransactionStatusID, "0", State, TxnID, Tax, Convert.ToString(RechargeAmount), "", "Android", Regulatery.ToString(), IsWalet);
                SendFailureMailRecharge(EmailID, (MobileNo + "  Recharge Failed!"), MobileNo, Convert.ToInt32(NetworkID), TariffCode, RechargeAmount, PlanDescription, ex.Message );

                return "{\"Response\" : [{\"Responsecode\" : \"2\" , \"Response\" : \"Something is wrong\" }]}";
               
            }

        }

        public void Log2(string ss, string condition)
        {
            try
            {
              
                string MSG = ss + " " + condition;
                cGeneral.LogSteps(MSG, System.Web.Hosting.HostingEnvironment.MapPath("~\\AppRecharge.txt"));

                
            }
            catch (Exception ex)
            {

            }

        }

        public void SendMailRecharge(string SendTo, string Subject, string PinNumber, string MobileNumber, string pass, int NetworkID)
           {
               try
               {
                   //string LoginUrl = "http://" + HttpContext.Current.Request.Url.Host + HttpContext.Current.Request.ApplicationPath + "/Login.aspx";
                   //string LogoUrl = "http://" + HttpContext.Current.Request.Url.Host + HttpContext.Current.Request.ApplicationPath + "/img/logo.png";
                   MailMessage mail = new MailMessage();
                   SmtpClient SmtpServer = new SmtpClient();
                   string LogoUrl = "http://www.activatemysim.net/img/logo.png";

                   //string MailAddress = ConfigurationManager.AppSettings.Get("Fromail");
                   //string ToMailID = ConfigurationManager.AppSettings.Get("ToMailID");
                   //string PassWord = ConfigurationManager.AppSettings.Get("Password");

                   string MailAddress = "ENK.sim@gmail.com";
                   string PassWord = "9711679656";
                   string Host = "smtp.gmail.com";


                   mail.From = new MailAddress(MailAddress);
                   // mail.To.Add(SendTo);
                   mail.To.Add(SendTo);
                   TimeSpan ts = new TimeSpan(7, 0, 0);
                   mail.Subject = Subject + " " + DateTime.UtcNow.Subtract(ts).ToString();

                   StringBuilder sb = new StringBuilder();
                   sb.Append("<html>");
                   sb.Append("<body style=”color:grey; font-size:15px;font-style:italic;”>");
                   sb.Append("<font face=”Helvetica, Arial, sans-serif”>");

                   sb.Append("<div style=”position:absolute; height:200px; width:100px; background-color:0d1d36; padding:30px;”>"); 
                   sb.Append("</div>");
                   sb.Append("<img src=" + LogoUrl + " />");

                   sb.Append("<div style=”background-color: #ece8d4; width:600px; height:200px; padding:30px; margin-top:30px;”>");
                   //sb.Append("<p>Please find the new credentials and get started.</p>");
                   sb.Append("<div style='border:1px solid black;padding-left: 24px;width: 388px; BORDER-RADIUS: 25px;font-style:italic;'>");
                   sb.Append("<br/>");
                   sb.Append("<p>Mobile Number " + MobileNumber + "  Recharge successfully.  <p>");
              
                   sb.Append("<p> Pin Number " + PinNumber + "<p>");
                   sb.Append("</div>");
                   if (NetworkID == 13)
                   {

                       sb.Append("<p> Recharge  successfully, If you have any issue  with Recharge please contact Lycamobile directly at +1-845-301-1633 / +1-866-277-0024 <p>");

                   }
                   else if (NetworkID == 15)
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

                   SmtpServer.Host = Host;
                   SmtpServer.Port = 587;
                   SmtpServer.Credentials = new System.Net.NetworkCredential(MailAddress, PassWord);
                   SmtpServer.EnableSsl = true;

                   SmtpServer.Send(mail);

               }
               catch (Exception ex)
               {

               }
           }
       
        public void SendFailureMailRecharge(string SendTo, string Subject, string MobileNumber, int NetworkID, string TariffCode, string RechageAmount, string ProductDescription,string Responce)
        {
            try
            {
                //string LoginUrl = "http://" + HttpContext.Current.Request.Url.Host + HttpContext.Current.Request.ApplicationPath + "/Login.aspx";
                //string LogoUrl = "http://" + HttpContext.Current.Request.Url.Host + HttpContext.Current.Request.ApplicationPath + "/img/logo.png";
               string LogoUrl = "http://www.activatemysim.net/img/logo.png";
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient();


                //string MailAddress = ConfigurationManager.AppSettings.Get("Fromail");
                //string ToMailID = ConfigurationManager.AppSettings.Get("ToMailID");
                //string PassWord = ConfigurationManager.AppSettings.Get("Password");


              
                SendTo = SendTo + "," + "info@ENK.com";
               // SendTo = SendTo + "," + "shadab.a@virtuzo.in";

                string MailAddress = "ENK.sim@gmail.com";
                string PassWord = "9711679656";
                string Host = "smtp.gmail.com";


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
                sb.Append("<p> Plan Description  " + ProductDescription + ". <p>");
               
                
                sb.Append("<p>Rechage Amount  " + RechageAmount + "$. <p>");
               
                sb.Append("<p>Mobile Number " + MobileNumber + "  Recharge not successful.  <p>");
                
                sb.Append("<p>Responce :- " + Responce + " <p>");


                sb.Append("</div>");
                sb.Append("</div>"); 
               
                sb.Append("<br/>");
                if (NetworkID == 13)
                {

                    sb.Append("<p> Recharge not successfully, If you have any issue  with Recharge please contact Lycamobile directly at +1-845-301-1633 / +1-866-277-0024 <p>");

                }
                else if (NetworkID == 15)
                {
                    sb.Append("<p> Recharge not successfully, If you have any issue  with Recharge please contact H2O WIRELESS. <p>");
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

                sb.Append("Please send us email info@ENK.com or call us  209 297-3200.   Text us:  209 890 8006  with in 24 Hours ");


                sb.Append("<p>Please do not reply to this email. This mailbox is not monitored and you will not receive a response. ");

                sb.Append("<br/>");
                sb.Append("</div>");
                sb.Append("</body>");
                sb.Append("</html>");

                mail.Body = sb.ToString();
                mail.BodyEncoding = System.Text.Encoding.UTF8;
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.High;

                SmtpServer.Host = Host;
                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential(MailAddress, PassWord);
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);

            }
            catch (Exception ex)
            {

            }
        }

        public string GetActivationPlan(int NetworkID, int DistributorID)
        {
            try
            {
                string result;
                RechargeAndroid dis = new RechargeAndroid();
                // cGeneral.LogSteps(DistributorID, System.Web.Hosting.HostingEnvironment.MapPath("~\\CheckBalancelog.txt"));
                DataSet ds = dis.GetDistributorActivationPlanApp(NetworkID, DistributorID);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    result = cGeneral.GetJson(ds.Tables[0]);
                    //  result = "{\"Response\" : [{\"Responsecode\" : \"0\" , \"Response\" : \"Success\" , \"Data\" : \"" + result + "\" }] }";
                    result = "{\"Response\" : [{\"Responsecode\" : \"0\" , \"Response\" : \"Success\"}] , \"Data\" : " + result + "}";
                }
                else
                {
                    result = "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \"Failure\" }]}";
                }

                return result;
            }
            catch (Exception ex)
            {
                cGeneral.LogSteps("GetDistributorActivationPlanApp", System.Web.Hosting.HostingEnvironment.MapPath("~\\AndriodRecharge.txt"));
                return "{\"Response\" : [{\"Responsecode\" : \"2\" , \"Response\" : \"Something Went Wrong..\" }]}";

            }
        }

        public string ActivateSIMForLycaMobile(string ClientTypeID, string DistributorID, string TariffID, string SimNumber, string TariffAmount, string LoginID, string EmailID, string ZipCode, string Month, int IsWalet, string TariffPlan)
        {
            try
            {
                string result;
                Boolean IsBalance = false;
                Boolean IsSimStatus = false;
                string SimStatus = "";
                string TRANSACTIONID;
                // double TariffAmt = Convert.ToDouble(TariffAmount);

                cSIM s = new cSIM();
                DBDistributor dis = new DBDistributor();
                cGeneral.LogSteps(DistributorID, System.Web.Hosting.HostingEnvironment.MapPath("~\\CheckBalancelog.txt"));

                string PaymentId = "0";



                // 1 $ Add Regulatery 
                int Regulatery = 0;
                DateTime today = DateTime.Today;
                cReport rpt = new cReport();
                // DateTime date = new DateTime(2017, 07, 20);
                DataSet dsReg = rpt.GetRegulatery();
                DateTime date = Convert.ToDateTime(dsReg.Tables[0].Rows[0]["StartDate"]); 
                if (date <= today)
                {
                    if (Month == "1")
                    {
                        Regulatery = 1;
                        TariffAmount = Convert.ToString(Convert.ToDouble(TariffAmount) + 1);

                    }
                    else if (Month == "2")
                    {
                        Regulatery = 2;
                        TariffAmount = Convert.ToString(Convert.ToDouble(TariffAmount) + 2);


                    }
                    else if (Month == "3")
                    {
                        Regulatery = 3;
                        TariffAmount = Convert.ToString(Convert.ToDouble(TariffAmount) + 3);
                    }
                }
 
                try  // check Sim Activation with 24 Hour 
                {
                    DataSet dsDuplicate = s.CheckDuplicateSIMActivation(SimNumber);
                    if (dsDuplicate != null)
                    {      // IsValid =0 is Allow for Activation
                        if (Convert.ToInt32(dsDuplicate.Tables[0].Rows[0]["IsValid"]) == 1)
                        {
                            return result = "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \"Failure\", \"Message\" : \" Sim number cannot be activated after 24 Hour. \" }]}";

                        }
                    }
                    else
                    {

                        return result = "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \"Failure\", \"Message\" : \"somthing wrong check Sim Activation. \" }]}";

                    }
                }
                catch
                {
                    return result = "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \"Failure\", \"Message\" : \"somthing wrong check Sim Activation. \" }]}";
                }

               


                DataSet dsInitiate = new DataSet();
                DataSet dsSim = s.CheckSimActivation(Convert.ToInt32(DistributorID), Convert.ToInt32(ClientTypeID), SimNumber,"Activate");

                if (dsSim.Tables[0].Rows.Count > 0)
                {
                    SimStatus = Convert.ToString(dsSim.Tables[0].Rows[0][0]);

                    if (SimStatus == "Ready to Activation")
                    {


                        DBPayment svc = new DBPayment();
                        svc.ChargedAmount = Convert.ToDecimal(TariffAmount);
                        svc.PaymentType = 4;
                        svc.PayeeID = Convert.ToInt32(DistributorID);
                        svc.PaymentFrom = 9;
                        svc.ActivationVia = 17;
                        svc.TransactionStatusId = 23;
                        svc.TransactionStatus = "Pending";
                        svc.PaymentMode = "App Distributor Activation";
                        svc.TxnDate = DateTime.Now.ToString();
                        svc.Currency = 1;
                        svc.TariffID = Convert.ToInt32(TariffID);
                        int dist = Convert.ToInt32(DistributorID);

                        dsInitiate = svc.InitiatePaymentWaletActivationApp(dist, Convert.ToInt32(LoginID));


                        PaymentId = Convert.ToString(dsInitiate.Tables[0].Rows[0]["PaymentId"]);

                        if (PaymentId == "0")
                        {
                            IsBalance = false;
                         return    result = "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \"Failure\", \"Message\" : \"Your Account Balance is Low. Please Recharge Your Balance\" }]}";
                
                        }
                        else
                        {
                            IsBalance = true;

                            if (dsInitiate.Tables.Count > 0)
                            {

                                int id = Convert.ToInt32(dsSim.Tables[1].Rows[0]["TRANSACTIONID"]);
                                TRANSACTIONID = id.ToString("00000");
                                TRANSACTIONID = "AHP" + TRANSACTIONID;

                                IsSimStatus = true;
                              //  string resp = "<ENVELOPE><HEADER><ERROR_CODE>0</ERROR_CODE><ERROR_DESC>Success</ERROR_DESC></HEADER><BODY><ACTIVATE_USIM_PORTIN_BUNDLE_RESPONSE><ALLOCATED_MSISDN>16462696753</ALLOCATED_MSISDN><PORTIN_REFERENCE_NUMBER/></ACTIVATE_USIM_PORTIN_BUNDLE_RESPONSE></BODY></ENVELOPE>";
                               
                              string resp = ActivateSimForLycaAPI(DistributorID, LoginID, TariffID, EmailID, TRANSACTIONID, ClientTypeID, SimNumber, ZipCode);
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
                                                    SendMailActivation(EmailID, "Sim Activation", ALLOCATED_MSISDN, SimNumber, "");

                                                    DBPayment sp = new DBPayment();
                                                    sp.ChargedAmount = Convert.ToDecimal(TariffAmount);
                                                    sp.PaymentType = 4;
                                                    sp.PayeeID = Convert.ToInt32(DistributorID);
                                                    sp.PaymentFrom = 9;
                                                    sp.ActivationType = 6;
                                                    sp.ActivationStatus = 15;
                                                    sp.ActivationVia = 17;
                                                    sp.ActivationResp = resp;
                                                    sp.ActivationRequest = RequestRes;
                                                    sp.TariffID = Convert.ToInt32(TariffID);
                                                    sp.ALLOCATED_MSISDN = ALLOCATED_MSISDN;
                                                    sp.TransactionId = TRANSACTIONID;
                                                    sp.PaymentMode = "App Distributor Activation";
                                                    sp.TransactionStatusId = 24;
                                                    sp.TransactionStatus = "Success";
                                                    sp.Regulatery = Regulatery;
                                                    sp.PaymentId = Convert.ToInt32(PaymentId);
                                                    sp.IsWalet = IsWalet;

                                                     


                                                    int loginID = Convert.ToInt32(LoginID);
                                                    string sim = SimNumber;
                                                    string zip = ZipCode;
                                                    string Language = "ENGLISH";
                                                    string ChannelID = "ENK";

                                                    int a = sp.UpdateAccountBalanceApp(Convert.ToInt32(DistributorID), loginID, sim, zip, ChannelID, Language);


                                                    result = "{\"Response\" : [{\"Responsecode\" : \"0\" , \"Response\" : \"Success\" , \"Message\" : \"SIM activation done successfully with Mobile Number - " + ALLOCATED_MSISDN + "\" }] }";
                                                }
                                                else
                                                {
                                                    SaveDataApp(resp, 16, TRANSACTIONID, TariffAmount, DistributorID, TariffID, LoginID, SimNumber, ZipCode, Convert.ToInt32(PaymentId), IsWalet);
                                                    SendMailActivationFailed(EmailID, "Sim Activation Failed", TariffPlan, SimNumber, TariffAmount, resp);
                                                             
                                                    result = "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \"Failure\", \"Message\" : \"SIM activation Fail Please Try Again\" }]}";

                                                }
                                            }
                                            else
                                            {
                                                SaveDataApp(resp, 16, TRANSACTIONID, TariffAmount, DistributorID, TariffID, LoginID, SimNumber, ZipCode, Convert.ToInt32(PaymentId), IsWalet);
                                                SendMailActivationFailed(EmailID, "Sim Activation Failed", TariffPlan, SimNumber, TariffAmount, resp);
                                                result = "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \"Failure\", \"Message\" : \"SIM activation Fail Please Try Again\" }]}";
                                            }
                                        }
                                        else
                                        {
                                            SaveDataApp(resp, 16, TRANSACTIONID, TariffAmount, DistributorID, TariffID, LoginID, SimNumber, ZipCode, Convert.ToInt32(PaymentId), IsWalet);
                                            SendMailActivationFailed(EmailID, "Sim Activation Failed", TariffPlan, SimNumber, TariffAmount, resp);
                                            result = "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \"Failure\", \"Message\" : \"SIM activation Fail Please Try Again\" }]}";
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        SaveDataApp(resp, 16, TRANSACTIONID, TariffAmount, DistributorID, TariffID, LoginID, SimNumber, ZipCode, Convert.ToInt32(PaymentId), IsWalet);
                                        SendMailActivationFailed(EmailID, "Sim Activation Failed", TariffPlan, SimNumber, TariffAmount, resp);
                                        return result = "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \"Failure\", \"Message\" : \"SIM activation Fail Please Try Again\" }]}";
                                    }
                                }
                                else
                                {
                                    SaveDataApp(resp, 16, TRANSACTIONID, TariffAmount, DistributorID, TariffID, LoginID, SimNumber, ZipCode, Convert.ToInt32(PaymentId), IsWalet);
                                    SendMailActivationFailed(EmailID, "Sim Activation Failed", TariffPlan, SimNumber, TariffAmount, resp);
                                    result = "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \"Failure\", \"Message\" : \"SIM activation Fail Please Try Again\" }]}";
                                }
                            }
                            else
                            {

                                IsSimStatus = false;
                                result = "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \"Failure\", \"Message\" : \"Your Account Balance is Low. Please Recharge Your Balance\" }]}";

                            }

                        }

                    }
                    else
                    {
                        result = "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \"Failure\", \"Message\" : \"" + SimStatus + "\" }]}";
                    }
                    //if (IsBalance == false)
                    //{
                    //    result = "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \"Failure\", \"Message\" : \"Your Account Balance is Low. Please Recharge Your Balance\" }]}";
                    //}
                    
                }
                else
                {
                    IsSimStatus = false;
                    result = "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \"Failure\", \"Message\" : \"" + SimStatus + "\" }]}";
                }

                return result;
            }
            catch (Exception ex)
            {
                return "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \"Failure Catch\" }]}";
            }
        }

        public string InitiatePaymentPaypalAcivationForLycaMobile(int loginID, int DistributorID, string ChargedAmount, string TariffID)
        {
            try
            {
                
                
                
                DBPayment svc = new DBPayment();

                string MSG = loginID + " " + DistributorID + " " + ChargedAmount;
                cGeneral.LogSteps(MSG, System.Web.Hosting.HostingEnvironment.MapPath("~\\PaypalTopup.txt"));
                string result;
                DataSet ds = new DataSet();

                svc.ChargedAmount = Convert.ToDecimal(ChargedAmount);
                svc.PaymentType = 4;
                svc.PayeeID = Convert.ToInt32(DistributorID);
                svc.PaymentFrom = 9;
                svc.ActivationVia = 17;
                svc.TransactionStatusId = 23;
                svc.TransactionStatus = "Pending";
                svc.PaymentMode = "App Distributor Activation";
                svc.TxnDate = DateTime.Now.ToString();
                svc.Currency = 1;
                svc.TariffID= Convert.ToInt32(TariffID);
                int dist = Convert.ToInt32(DistributorID);

                ds = svc.InitiatePaymentPaypalActivationApp(dist, loginID);


                if (ds.Tables.Count > 0)
                {
                    string LoginUrl = "";
                    string PaymentId = Convert.ToString(ds.Tables[0].Rows[0]["PaymentId"]);
                    result = cGeneral.GetJson(ds.Tables[0]);
                    if (PaymentId != "0")
                    {
                        //result = "{\"Response\" : [{\"Responsecode\" : \"0\" , \"Response\" : \"Success\" }]}";
                        result = "{\"Response\" : [{\"Responsecode\" : \"0\" , \"Response\" : \"Success\"}] , \"Data\" : " + result + "}";
                    }
                    else
                    {

                        result = "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \"Failure\" }]}";
                    }

                }
                else
                {
                    result = "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \"Failure\" }]}";
                }

                return result;
            }
            catch (Exception ex)
            {

                return "{\"Response\" : [{\"Responsecode\" : \"2\" , \"Response\" : \"Something wrong\" }]}";
            }
        }
        public string ActivateSIMForLycaMobileWithPaypal(string ClientTypeID, string DistributorID, string TariffID, string SimNumber, string TariffAmount, string LoginID, string EmailID, string ZipCode, string Month, int PaymentID, string TariffPlan)
        {
            try
            {
                string result;
                int IsWalet = 2; // For WALET

                Boolean IsBalance = false;
                Boolean IsSimStatus = false;
                string SimStatus = "";
                string TRANSACTIONID;
                // double TariffAmt = Convert.ToDouble(TariffAmount);

                cSIM s = new cSIM();
                DBDistributor dis = new DBDistributor();
                cGeneral.LogSteps(DistributorID, System.Web.Hosting.HostingEnvironment.MapPath("~\\CheckBalancelog.txt"));

                // 1 $ Add Regulatery 
                int Regulatery = 0;
                DateTime today = DateTime.Today;
                cReport rpt = new cReport();
                // DateTime date = new DateTime(2017, 07, 20);
                DataSet dsReg = rpt.GetRegulatery();
                DateTime date = Convert.ToDateTime(dsReg.Tables[0].Rows[0]["StartDate"]); 
                if (date <= today)
                {
                    if (Month == "1")
                    {
                        Regulatery = 1;
                        TariffAmount = Convert.ToString(Convert.ToDouble(TariffAmount) + 1);

                    }
                    else if (Month == "2")
                    {
                        Regulatery = 2;
                        TariffAmount = Convert.ToString(Convert.ToDouble(TariffAmount) + 2);


                    }
                    else if (Month == "3")
                    {
                        Regulatery = 3;
                        TariffAmount = Convert.ToString(Convert.ToDouble(TariffAmount) + 3);
                    }
                }
                else { Regulatery = 0; }


                try  // check Sim Activation with 24 Hour 
                {
                    DataSet dsDuplicate = s.CheckDuplicateSIMActivation(SimNumber);
                    if (dsDuplicate != null)
                    {      // IsValid =0 is Allow for Activation
                        if (Convert.ToInt32(dsDuplicate.Tables[0].Rows[0]["IsValid"]) == 1)
                        {
                            return result = "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \"Failure\", \"Message\" : \" Sim number cannot be activated after 24 Hour. \" }]}";

                        }
                    }
                    else
                    {

                        return result = "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \"Failure\", \"Message\" : \"somthing wrong check Sim Activation. \" }]}";

                    }
                }
                catch
                {
                    return result = "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \"Failure\", \"Message\" : \"somthing wrong check Sim Activation. \" }]}";
                }

                DataSet dsSim = s.CheckSimActivation(Convert.ToInt32(DistributorID), Convert.ToInt32(ClientTypeID), SimNumber,"Activate");
                if (dsSim.Tables[0].Rows.Count > 0)
                {
                    SimStatus = Convert.ToString(dsSim.Tables[0].Rows[0][0]);

                    if (SimStatus == "Ready to Activation")
                    {

                        
                            int id = Convert.ToInt32(dsSim.Tables[1].Rows[0]["TRANSACTIONID"]);
                            TRANSACTIONID = id.ToString("00000");
                            TRANSACTIONID = "AHP" + TRANSACTIONID;

                            IsSimStatus = true;
                            //string resp = "<ENVELOPE><HEADER><ERROR_CODE>0</ERROR_CODE><ERROR_DESC>Success</ERROR_DESC></HEADER><BODY><ACTIVATE_USIM_PORTIN_BUNDLE_RESPONSE><ALLOCATED_MSISDN>16462696753</ALLOCATED_MSISDN><PORTIN_REFERENCE_NUMBER/></ACTIVATE_USIM_PORTIN_BUNDLE_RESPONSE></BODY></ENVELOPE>";
                         
                           string resp = ActivateSimForLycaAPI(DistributorID, LoginID, TariffID, EmailID, TRANSACTIONID, ClientTypeID, SimNumber, ZipCode);
                              
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
                                                SendMailActivation(EmailID, "Sim Activation", ALLOCATED_MSISDN, SimNumber, "");

                                                DBPayment sp = new DBPayment();
                                                sp.ChargedAmount = Convert.ToDecimal(TariffAmount);
                                                sp.PaymentType = 4;
                                                sp.PayeeID = Convert.ToInt32(DistributorID);
                                                sp.PaymentFrom = 9;
                                                sp.ActivationType = 6;
                                                sp.ActivationStatus = 15;
                                                sp.ActivationVia = 17;
                                                sp.ActivationResp = resp;
                                                sp.ActivationRequest = RequestRes;
                                                sp.TariffID = Convert.ToInt32(TariffID);
                                                sp.ALLOCATED_MSISDN = ALLOCATED_MSISDN;
                                                sp.TransactionId = TRANSACTIONID;
                                                sp.PaymentMode = "App Distributor Activation";
                                                sp.TransactionStatusId = 24;
                                                sp.TransactionStatus = "Success";
                                                sp.Regulatery = Regulatery;
                                                sp.PaymentId = Convert.ToInt32(PaymentID);
                                                sp.IsWalet = IsWalet;
                                               


                                                int loginID = Convert.ToInt32(LoginID);
                                                string sim = SimNumber;
                                                string zip = ZipCode;
                                                string Language = "ENGLISH";
                                                string ChannelID = "ENK";

                                                int a = sp.UpdateAccountBalanceApp(Convert.ToInt32(DistributorID), loginID, sim, zip, ChannelID, Language);


                                                result = "{\"Response\" : [{\"Responsecode\" : \"0\" , \"Response\" : \"Success\" , \"Message\" : \"SIM activation done successfully with Mobile Number - " + ALLOCATED_MSISDN + "\" }] }";
                                            }
                                            else
                                            {
                                                SaveDataApp(resp, 16, TRANSACTIONID, TariffAmount, DistributorID, TariffID, LoginID, SimNumber, ZipCode, PaymentID,IsWalet);
                                                SendMailActivationFailed(EmailID, "Sim Activation Failed", TariffPlan, SimNumber, TariffAmount, resp);
                                        
                                                result = "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \"Failure\", \"Message\" : \"SIM activation Fail Please Try Again\" }]}";

                                            }
                                        }
                                        else
                                        {
                                            SaveDataApp(resp, 16, TRANSACTIONID, TariffAmount, DistributorID, TariffID, LoginID, SimNumber, ZipCode, PaymentID,IsWalet);
                                            SendMailActivationFailed(EmailID, "Sim Activation Failed", TariffPlan, SimNumber, TariffAmount, resp);
                                            result = "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \"Failure\", \"Message\" : \"SIM activation Fail Please Try Again\" }]}";
                                        }
                                    }
                                    else
                                    {
                                        SaveDataApp(resp, 16, TRANSACTIONID, TariffAmount, DistributorID, TariffID, LoginID, SimNumber, ZipCode, PaymentID,IsWalet);
                                        SendMailActivationFailed(EmailID, "Sim Activation Failed", TariffPlan, SimNumber, TariffAmount, resp);
                                        result = "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \"Failure\", \"Message\" : \"SIM activation Fail Please Try Again\" }]}";
                                    }
                                }
                                catch (Exception ex)
                                {
                                    SaveDataApp(resp, 16, TRANSACTIONID, TariffAmount, DistributorID, TariffID, LoginID, SimNumber, ZipCode, PaymentID,IsWalet);
                                    SendMailActivationFailed(EmailID, "Sim Activation Failed", TariffPlan, SimNumber, TariffAmount, resp);
                                   return  result = "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \"Failure\", \"Message\" : \"SIM activation Fail Please Try Again\" }]}";
                                }
                            }
                            else
                            {
                                SaveDataApp(resp, 16, TRANSACTIONID, TariffAmount, DistributorID, TariffID, LoginID, SimNumber, ZipCode, PaymentID,IsWalet);
                                SendMailActivationFailed(EmailID, "Sim Activation Failed", TariffPlan, SimNumber, TariffAmount, resp);
                                result = "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \"Failure\", \"Message\" : \"SIM activation Fail Please Try Again\" }]}";
                            }
                        

                    }



                    else
                    {
                        
                        result = "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \"Failure\", \"Message\" : \"" + SimStatus + "\" }]}";
                    }
                }
                else
                {
                    result = "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \"Failure Sim Invalid\" }]}";
                }

                return result;
            }
            catch (Exception ex)
            {
                return "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \" some thingwrong\" }]}";
            }
        }
        public string ActivateSimForLycaAPI(string DistributorID, string LoginID, string TariffID, string email, string TransactionID, string ClientTypeID, string SimNumber, string ZipCode)
        {
            string response = "";

            string months = "1";
            try
            {
                RechargeAndroid dbt = new RechargeAndroid();

                DataSet dsTariff = dbt.GetSingleTariffDetailForActivationAPP(Convert.ToInt32(LoginID), Convert.ToInt32(DistributorID), Convert.ToInt32(ClientTypeID), Convert.ToInt32(TariffID));
                if (dsTariff.Tables[0].Rows.Count > 0)
                {
                    months = Convert.ToString(dsTariff.Tables[0].Rows[0]["Months"]);
                }
                string BundleID = Convert.ToString(dsTariff.Tables[0].Rows[0]["TariffTypeID"]);
                string BundleCode = Convert.ToString(dsTariff.Tables[0].Rows[0]["TariffCode"]);
                string BundleType = Convert.ToString(dsTariff.Tables[0].Rows[0]["TariffType"]); ;
                string BundleAmount = Convert.ToString(dsTariff.Tables[0].Rows[0]["LycaAmount"]);
                string EmailID = email;

                string X = "";

                string activation = "1";
                string SIMCARD = SimNumber;
                string ZIPCode = ZipCode;
                string Language = "ENGLISH";
                string ChannelID = "ENK";


                // 1 $ Add Regulatery 
                int Regulatery = 0;
                DateTime today = DateTime.Today;
                cReport rpt = new cReport();
                // DateTime date = new DateTime(2017, 07, 20);
                DataSet dsReg = rpt.GetRegulatery();
                DateTime date = Convert.ToDateTime(dsReg.Tables[0].Rows[0]["StartDate"]); 
                if (date <= today)
                {
                    if (months == "1")
                    {
                        Regulatery = 1;
                        BundleAmount = Convert.ToString(Convert.ToDouble(BundleAmount) + 1);
                    }
                    else if (months == "2")
                    {
                        Regulatery = 2;
                        BundleAmount = Convert.ToString(Convert.ToDouble(BundleAmount) + 2);
                    }
                    else if (months == "3")
                    {
                        Regulatery = 3;
                        BundleAmount = Convert.ToString(Convert.ToDouble(BundleAmount) + 3);
                    }


                }
                else { Regulatery = 0; }





                if (BundleType == "National")
                {
                    X = "<ENVELOPE><HEADER><TRANSACTION_ID>" + TransactionID + "</TRANSACTION_ID><ENTITY>ENK</ENTITY><CHANNEL_REFERENCE>ENK</CHANNEL_REFERENCE></HEADER><BODY><ACTIVATE_USIM_PORTIN_BUNDLE_REQUEST><DETAILS><ICC_ID>" + SIMCARD + "</ICC_ID><ZIP_CODE>" + ZIPCode + "</ZIP_CODE><PREFERRED_LANGUAGE>" + Language + "</PREFERRED_LANGUAGE><P_MSISDN></P_MSISDN><ACCOUNT_NUMBER></ACCOUNT_NUMBER><PASSWORD_PIN></PASSWORD_PIN><NO_OF_MONTHS>" + months + "</NO_OF_MONTHS><NATIONAL_BUNDLE_CODE>" + BundleCode + "</NATIONAL_BUNDLE_CODE><NATIONAL_BUNDLE_AMOUNT>" + BundleAmount + "</NATIONAL_BUNDLE_AMOUNT><INTERNATIONAL_BUNDLE_CODE></INTERNATIONAL_BUNDLE_CODE><INTERNATIONAL_BUNDLE_AMOUNT></INTERNATIONAL_BUNDLE_AMOUNT><TOPUP_AMOUNT></TOPUP_AMOUNT><TOPUP_CARD_ID></TOPUP_CARD_ID><VOUCHER_PIN></VOUCHER_PIN><CHANNEL_ID>" + ChannelID + "</CHANNEL_ID><EMAIL_ID>" + EmailID + "</EMAIL_ID></DETAILS></ACTIVATE_USIM_PORTIN_BUNDLE_REQUEST></BODY></ENVELOPE>";


                }
                else
                {
                    X = "<ENVELOPE><HEADER><TRANSACTION_ID>" + TransactionID + "</TRANSACTION_ID><ENTITY>ENK</ENTITY><CHANNEL_REFERENCE>ENK</CHANNEL_REFERENCE></HEADER><BODY><ACTIVATE_USIM_PORTIN_BUNDLE_REQUEST><DETAILS><ICC_ID>" + SIMCARD + "</ICC_ID><ZIP_CODE>" + ZIPCode + "</ZIP_CODE><PREFERRED_LANGUAGE>" + Language + "</PREFERRED_LANGUAGE><P_MSISDN></P_MSISDN><ACCOUNT_NUMBER></ACCOUNT_NUMBER><PASSWORD_PIN></PASSWORD_PIN><NO_OF_MONTHS>" + months + "</NO_OF_MONTHS><NATIONAL_BUNDLE_CODE>" + BundleCode + "</NATIONAL_BUNDLE_CODE><NATIONAL_BUNDLE_AMOUNT>" + BundleAmount + "</NATIONAL_BUNDLE_AMOUNT><INTERNATIONAL_BUNDLE_CODE></INTERNATIONAL_BUNDLE_CODE><INTERNATIONAL_BUNDLE_AMOUNT></INTERNATIONAL_BUNDLE_AMOUNT><TOPUP_AMOUNT></TOPUP_AMOUNT><TOPUP_CARD_ID></TOPUP_CARD_ID><VOUCHER_PIN></VOUCHER_PIN><CHANNEL_ID>" + ChannelID + "</CHANNEL_ID><EMAIL_ID>" + EmailID + "</EMAIL_ID></DETAILS></ACTIVATE_USIM_PORTIN_BUNDLE_REQUEST></BODY></ENVELOPE>";

                }

                RequestRes = X;
                //Log(X, "Sending Request");
                string resp = Activation(X);
                response = resp;
                //Log(resp, "Response");
                //Log("", "split");
                return response;


            }
            catch (Exception ex)
            {
                return response;
                //ShowPopUpMsg(ex.Message);
            }
        }

        public void SendMailActivation(string SendTo, string Subject, string UserName, string UserID, string pass)
        {
            try
            {
                //string LoginUrl = "http://" + HttpContext.Current.Request.Url.Host + HttpContext.Current.Request.ApplicationPath + "/Login.aspx";
                //string LogoUrl = "http://" + HttpContext.Current.Request.Url.Host + HttpContext.Current.Request.ApplicationPath + "/img/logo.png";
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient();
                string LogoUrl = "http://www.activatemysim.net/img/logo.png";

                //string MailAddress = ConfigurationManager.AppSettings.Get("Fromail");
                //string PassWord = ConfigurationManager.AppSettings.Get("Password");

                string MailAddress = "ENK.sim@gmail.com";
                string PassWord = "9711679656";
                string Host = "smtp.gmail.com";

                mail.From = new MailAddress(MailAddress);
                mail.To.Add(SendTo);
                TimeSpan ts = new TimeSpan(8, 0, 0);
                mail.Subject = Subject + " " + DateTime.UtcNow.Subtract(ts).ToString();

                StringBuilder sb = new StringBuilder();
                sb.Append("<html>");
                sb.Append("<body style=”color:grey; font-size:15px;font-style:italic;”>");
                sb.Append("<font face=”Helvetica, Arial, sans-serif”>");

                sb.Append("<div style=”position:absolute; height:200px; width:100px; background-color:0d1d36; padding:30px;”>");
               
                sb.Append("</div>");
                sb.Append("<img src=" + LogoUrl + " />");
                sb.Append("<br/>");

           

                sb.Append("<div style=”background-color: #ece8d4; width:600px; height:200px; padding:30px; margin-top:30px;”>");
                //sb.Append("<p>Please find the new credentials and get started.</p>");
                sb.Append("<div style='border:1px solid black;padding-left: 24px;width: 388px; BORDER-RADIUS: 25px;font-style:italic;'>");
                sb.Append("<br/>");
                sb.Append("<p>" + UserID +"  "+Subject+ " successfully on Mobile Number " + UserName + "<p>");
                sb.Append("<p>");
                sb.Append("</div>");
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

                SmtpServer.Host = Host;
                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential(MailAddress, PassWord);
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);

            }
            catch (Exception ex)
            {

            }
        }
        public void SendMailActivationFailed(string SendTo, string Subject, string Plan, string UserID, string Amount, string Response)
        {
            try
            {
                //string LoginUrl = "http://" + HttpContext.Current.Request.Url.Host + HttpContext.Current.Request.ApplicationPath + "/Login.aspx";
                //string LogoUrl = "http://" + HttpContext.Current.Request.Url.Host + HttpContext.Current.Request.ApplicationPath + "/img/logo.png";
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient();
                string LogoUrl = "http://www.activatemysim.net/img/logo.png";

                //string MailAddress = ConfigurationManager.AppSettings.Get("Fromail");
                //string PassWord = ConfigurationManager.AppSettings.Get("Password");

                string MailAddress = "ENK.sim@gmail.com";
                string PassWord = "9711679656";
                string Host = "smtp.gmail.com";

                 SendTo = SendTo + "," + "info@ENK.com";
                //SendTo = SendTo + "," + "shadab.a@virtuzo.in";



                mail.From = new MailAddress(MailAddress);
                mail.To.Add(SendTo);
                TimeSpan ts = new TimeSpan(8, 0, 0);
                mail.Subject = Subject + " " + DateTime.UtcNow.Subtract(ts).ToString();

                StringBuilder sb = new StringBuilder();
                sb.Append("<html>");
                sb.Append("<body style=”color:grey; font-size:15px;font-style:italic;”>");
                sb.Append("<font face=”Helvetica, Arial, sans-serif”>");

                sb.Append("<div style=”position:absolute; height:200px; width:100px; background-color:0d1d36; padding:30px;”>");

                sb.Append("</div>");
                sb.Append("<img src=" + LogoUrl + " />");
                sb.Append("<div style=”background-color: #ece8d4; width:600px; height:200px; padding:30px; margin-top:30px;”>");
                sb.Append("<div style='border:1px solid black;padding-left: 24px;width: 388px; BORDER-RADIUS: 25px;font-style:italic;'>");
              
                //sb.Append("<p>Please find the new credentials and get started.</p>");
                sb.Append("<p>PlanAmount " + Amount + "$. <p>");
                
                sb.Append("<p>Plan Description " + Plan + ". <p>");
             
                sb.Append("<p> " + UserID+" " + Subject+ "<p>");
        

                sb.Append("<p>Response :- " + Response + "<p>");

                sb.Append("<p>");
                sb.Append("</div>");


                sb.Append("<br/>");
                sb.Append("<p>Sincerely,");
                sb.Append("<p>" + ConfigurationManager.AppSettings.Get("COMPANY_NAME") + "</p>");
                sb.Append("<p>Thank you</p>");


                sb.Append("<p>----------------------------------------------------------------<br/>");
                sb.Append("Please send us email info@ENK.com or call us  209 297-3200.   Text us:  209 890 8006 with in 24 Hours ");
                sb.Append("<p>Please do not reply to this email. This mailbox is not monitored and you will not receive a response. ");

                sb.Append("<br/>");
                sb.Append("</div>");
                sb.Append("</body>");
                sb.Append("</html>");

                mail.Body = sb.ToString();
                mail.BodyEncoding = System.Text.Encoding.UTF8;
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.High;

                SmtpServer.Host = Host;
                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential(MailAddress, PassWord);
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);

            }
            catch (Exception ex)
            {

            }
        }


        public string ActivateSIMForH2O(string ClientTypeID, string DistributorID, string TariffID, string SimNumber, string TariffAmount, string LoginID, string EmailID, string ZipCode, string City, int IsWalet, string TariffPlan)
        {
            try
            {
                string result;
                Boolean IsBalance = false;
                Boolean IsSimStatus = false;
                string SimStatus = "";
                string TRANSACTIONID;
                int Regulatery = 0;
                // double TariffAmt = Convert.ToDouble(TariffAmount);
                RechargeAndroid dbt = new RechargeAndroid();
                cSIM s = new cSIM();
                DBDistributor dis = new DBDistributor();
                cGeneral.LogSteps(DistributorID, System.Web.Hosting.HostingEnvironment.MapPath("~\\CheckBalancelog.txt"));

                string PaymentId = "0";

                int NetworkID = 0;

                net.emida.ws.webServicesService ws = new webServicesService();
               

                try
                {
                    string ss1 = ws.Login2("01", "A&HPrepaid", "95222", "1");
                }
                catch (Exception ex)
                {
                   return  result = "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \"" + ex.Message + "\"}]}";

                }
 
                try  // check Sim Activation with 24 Hour 
                {
                    DataSet dsDuplicate = s.CheckDuplicateSIMActivation(SimNumber);
                    if (dsDuplicate != null)
                    {      // IsValid =0 is Allow for Activation
                        if (Convert.ToInt32(dsDuplicate.Tables[0].Rows[0]["IsValid"]) == 1)
                        {
                            return result = "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \"Failure\", \"Message\" : \" Sim number cannot be activated after 24 Hour. \" }]}";

                        }
                    }
                    else
                    {

                        return result = "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \"Failure\", \"Message\" : \"somthing wrong check Sim Activation. \" }]}";

                    }
                }
                catch
                {
                    return result = "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \"Failure\", \"Message\" : \"somthing wrong check Sim Activation. \" }]}";
                }


                DataSet dsInitiate = new DataSet();
                DataSet dsSim = s.CheckSimActivation(Convert.ToInt32(DistributorID), Convert.ToInt32(ClientTypeID), SimNumber,"Activate");

                if (dsSim.Tables[0].Rows.Count > 0)
                {
                    SimStatus = Convert.ToString(dsSim.Tables[0].Rows[0][0]);

                    if (SimStatus == "Ready to Activation")
                    {


                        string TariffCode = "0";
                        string PlanDescription = "";
                        DataSet dsTariff = dbt.GetSingleTariffDetailForActivationAPP(Convert.ToInt32(LoginID), Convert.ToInt32(DistributorID), Convert.ToInt32(ClientTypeID), Convert.ToInt32(TariffID));
                        if (dsTariff.Tables[0].Rows.Count > 0)
                        {
                            TariffCode = Convert.ToString(dsTariff.Tables[0].Rows[0]["TariffCode"]);
                            PlanDescription = Convert.ToString(dsTariff.Tables[0].Rows[0]["Description"]); ;
                            NetworkID = Convert.ToInt32(dsTariff.Tables[0].Rows[0]["NetworkID"]);
                        }
                        else
                        {
                            return result = "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \"Failure\", \"Message\" : \"Tariff Plan is not mapped for activation \" }]}";

                        }



                        DBPayment svc = new DBPayment();
                        svc.ChargedAmount = Convert.ToDecimal(TariffAmount);
                        svc.PaymentType = 4;
                        svc.PayeeID = Convert.ToInt32(DistributorID);
                        svc.PaymentFrom = 9;
                        svc.ActivationVia = 17;
                        svc.TransactionStatusId = 23;
                        svc.TransactionStatus = "Pending";
                        svc.PaymentMode = "App Distributor Activation";
                        svc.TxnDate = DateTime.Now.ToString();
                        svc.Currency = 1;
                        svc.TariffID = Convert.ToInt32(TariffID);
                        int dist = Convert.ToInt32(DistributorID);

                        dsInitiate = svc.InitiatePaymentWaletActivationApp(dist, Convert.ToInt32(LoginID));


                        PaymentId = Convert.ToString(dsInitiate.Tables[0].Rows[0]["PaymentId"]);

                        if (PaymentId == "0")
                        {
                            IsBalance = false;
                           return  result = "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \"Failure\", \"Message\" : \"Your Account Balance is Low. Please Recharge Your Balance\" }]}";

                        }
                        else
                        {
                            IsBalance = true;

                            if (dsInitiate.Tables.Count > 0)
                            {

                                int id = Convert.ToInt32(dsSim.Tables[1].Rows[0]["TRANSACTIONID"]);
                                TRANSACTIONID = id.ToString("00000");
                                TRANSACTIONID = "AHP" + TRANSACTIONID;

                                IsSimStatus = true;


                               

                               
                                string Language = "ENGLISH";
                                string ChannelID = "ENK";
                               

                                string InvoiceNo = DateTime.Now.ToString().GetHashCode().ToString("X");
                                InvoiceNo = "AC" + InvoiceNo;
                                string resp = ws.GetPinProductsForActivation("01", "3756263", "1234", TariffCode, InvoiceNo, "1");
                                 
                                 
                                
                                if (resp.Trim() != null && resp.Trim() != "")
                                {
                                    try
                                    {

                                        StringReader theReader = new StringReader(Convert.ToString(resp));
                                        DataSet theDataSet = new DataSet();
                                        theDataSet.ReadXml(theReader);

                                        if (theDataSet.Tables.Count > 0)
                                        {
                                            DataTable dt = theDataSet.Tables[1];

                                            
                                           if (dt.Rows.Count > 0 && dt.Columns.Contains("PinProductId"))
                                            {
                                                
                                                string PINid = dt.Rows[0]["PinProductId"].ToString();

                                                string ProductDescription = dt.Rows[0]["PinProductDescription"].ToString();
                                                if (PINid != "")
                                                {

                                                    RequestRes = "3756263 ," + PINid + ", " + 1234 + ", " + TariffCode + ", 1," + InvoiceNo + ", 01, 305," + SimNumber + "," + City + ", " + ZipCode;
                                                   
                                                  //  resp="<ActivateGSMsimResponse><Version>01</Version><ResponseCode>00</ResponseCode><ResponseMessage>Success</ResponseMessage><serial>07041709246983|07041709247054</serial><min>4043537673</min></ActivateGSMsimResponse>";
                                
                                                    resp = ws.LocusActivateGSMsim("3756263", PINid, "1234", TariffCode, "1", InvoiceNo, "01", "305", SimNumber, City, ZipCode);

                                                    StringReader Reader = new StringReader(resp);
                                                    DataSet ds = new DataSet();
                                                    ds.ReadXml(Reader);
                                                    string Currency = Convert.ToString("$");
                                                    // Save Record ProductMaster
                                                    try
                                                    {
                                                        svc.SaveProductMaster(Convert.ToInt32(NetworkID), Convert.ToInt32(TariffID), PlanDescription.ToString(), ProductDescription.ToString(), "$", TariffAmount, Convert.ToInt32(LoginID));

                                                    }
                                                    catch { }
                                                    
                                                   
                                                    int loginID = Convert.ToInt32(LoginID);
                                                    string sim = SimNumber;
                                                    string zip = ZipCode;


                                                    if (ds.Tables.Count > 0)
                                                    {
                                                        DataTable dtMsg = ds.Tables[0];
                                                        if (dtMsg.Rows.Count > 0)
                                                        {
                                                            string ResponseCode = dtMsg.Rows[0]["ResponseCode"].ToString();
                                                            string ResponseMessage = dtMsg.Rows[0]["ResponseMessage"].ToString();
                                                            if (ResponseCode == "00")
                                                            {
                                                                string ALLOCATED_MSISDN = dtMsg.Rows[0]["min"].ToString();
                                                                SendMailActivation(EmailID, "Sim Activation", ALLOCATED_MSISDN, SimNumber.Trim(), "");
                                                                

                                                                DBPayment sp = new DBPayment();
                                                                sp.ChargedAmount = Convert.ToDecimal(TariffAmount);
                                                                sp.PaymentType = 4;
                                                                sp.PayeeID = Convert.ToInt32(DistributorID);
                                                                sp.PaymentFrom = 9;
                                                                sp.ActivationType = 6;
                                                                sp.ActivationStatus = 15;
                                                                sp.ActivationVia = 17;
                                                                sp.ActivationResp = resp;
                                                                sp.ActivationRequest = RequestRes;
                                                                sp.TariffID = Convert.ToInt32(TariffID);
                                                                sp.ALLOCATED_MSISDN = ALLOCATED_MSISDN;
                                                                sp.TransactionId = TRANSACTIONID;
                                                                sp.PaymentMode = "App Distributor Activation";
                                                                sp.TransactionStatusId = 24;
                                                                sp.TransactionStatus = "Success";
                                                                sp.Regulatery = Regulatery;
                                                                sp.PaymentId = Convert.ToInt32(PaymentId);
                                                                sp.IsWalet = IsWalet;

                                                                try
                                                                {
                                                                    svc.SaveTransactionDetails(Convert.ToInt32(NetworkID), Convert.ToInt32(TariffCode), "10", PINid, SimNumber, InvoiceNo, TariffAmount, Currency, City, zip, "305", Convert.ToInt32(LoginID), TariffAmount.ToString());

                                                                }
                                                                catch { }
                                                                
                                                                try
                                                                {
                                                                    int a = sp.UpdateAccountBalanceApp(Convert.ToInt32(DistributorID), loginID, sim, zip, ChannelID, Language);

                                                                    result = "{\"Response\" : [{\"Responsecode\" : \"0\" , \"Response\" : \"Success\" , \"Message\" : \"SIM activation done successfully with Mobile Number - " + ALLOCATED_MSISDN + "\" }] }";


                                                                }
                                                                catch (Exception ex)
                                                                {
                                                                    SaveDataApp(resp, 16, TRANSACTIONID, TariffAmount, DistributorID, TariffID, LoginID, SimNumber, ZipCode, Convert.ToInt32(PaymentId), IsWalet);
                                                                    SendMailActivationFailed(EmailID, "Sim Activation Failed", TariffPlan, SimNumber, TariffAmount, resp);
                                                             
                                                                    return    result = "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \"Failure\", \"Message\" : \"SIM activation Fail Please Try Again\" }]}";

                                                                }


                                                            }
                                                            else
                                                            {
                                                                SaveDataApp(resp, 16, TRANSACTIONID, TariffAmount, DistributorID, TariffID, LoginID, SimNumber, ZipCode, Convert.ToInt32(PaymentId), IsWalet);
                                                                SendMailActivationFailed(EmailID, "Sim Activation Failed", TariffPlan, SimNumber, TariffAmount, resp);
                                                               // result = "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \"Failure\", \"Message\" : \"SIM activation Fail Please Try Again\" }]}";
                                                                result = "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \"Failure\", \"Message\" : \"" + ResponseMessage + "\" }]}";


                                                            }


                                                        }
                                                        else
                                                        {
                                                            SaveDataApp(resp, 16, TRANSACTIONID, TariffAmount, DistributorID, TariffID, LoginID, SimNumber, ZipCode, Convert.ToInt32(PaymentId), IsWalet);
                                                            SendMailActivationFailed(EmailID, "Sim Activation Failed", TariffPlan, SimNumber, TariffAmount, resp);
                                                            result = "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \"Failure\", \"Message\" : \"SIM activation Fail Please Try Again\" }]}";
                                                        }


                                                    }

                                                    else
                                                    {
                                                        SaveDataApp(resp, 16, TRANSACTIONID, TariffAmount, DistributorID, TariffID, LoginID, SimNumber, ZipCode, Convert.ToInt32(PaymentId), IsWalet);
                                                        SendMailActivationFailed(EmailID, "Sim Activation Failed", TariffPlan, SimNumber, TariffAmount, resp);
                                                        result = "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \"Failure\", \"Message\" : \"SIM activation Fail Please Try Again\" }]}";
                                                    }





                                                }
                                                else
                                                {
                                                    SaveDataApp(resp, 16, TRANSACTIONID, TariffAmount, DistributorID, TariffID, LoginID, SimNumber, ZipCode, Convert.ToInt32(PaymentId), IsWalet);
                                                    SendMailActivationFailed(EmailID, "Sim Activation Failed", TariffPlan, SimNumber, TariffAmount, resp);
                                                             
                                                    result = "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \"Failure\", \"Message\" : \"SIM activation Fail Please Try Again\" }]}";

                                                }
                                            }
                                            else
                                            {
                                                SaveDataApp(resp, 16, TRANSACTIONID, TariffAmount, DistributorID, TariffID, LoginID, SimNumber, ZipCode, Convert.ToInt32(PaymentId), IsWalet);
                                                SendMailActivationFailed(EmailID, "Sim Activation Failed", TariffPlan, SimNumber, TariffAmount, resp);
                                                result = "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \"Failure\", \"Message\" : \"SIM activation Fail Please Try Again\" }]}";
                                            }
                                        }
                                        else
                                        {
                                            SaveDataApp(resp, 16, TRANSACTIONID, TariffAmount, DistributorID, TariffID, LoginID, SimNumber, ZipCode, Convert.ToInt32(PaymentId), IsWalet);
                                            SendMailActivationFailed(EmailID, "Sim Activation Failed", TariffPlan, SimNumber, TariffAmount, resp);
                                            result = "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \"Failure\", \"Message\" : \"SIM activation Fail Please Try Again\" }]}";
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        SaveDataApp(resp, 16, TRANSACTIONID, TariffAmount, DistributorID, TariffID, LoginID, SimNumber, ZipCode, Convert.ToInt32(PaymentId), IsWalet);
                                        SendMailActivationFailed(EmailID, "Sim Activation Failed", TariffPlan, SimNumber, TariffAmount, resp);
                                        result = "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \"Failure\", \"Message\" : \"SIM activation Fail Please Try Again\" }]}";
                                    }
                                }
                                else
                                {
                                    SaveDataApp(resp, 16, TRANSACTIONID, TariffAmount, DistributorID, TariffID, LoginID, SimNumber, ZipCode, Convert.ToInt32(PaymentId), IsWalet);
                                    SendMailActivationFailed(EmailID, "Sim Activation Failed", TariffPlan, SimNumber, TariffAmount, resp);
                                    result = "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \"Failure\", \"Message\" : \"SIM activation Fail Please Try Again\" }]}";
                                }
                            }
                            else
                            {

                                IsSimStatus = false;
                                result = "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \"Failure\", \"Message\" : \"Your Account Balance is Low. Please Recharge Your Balance\" }]}";

                            }

                        }

                    }
                    else
                    {
                        result = "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \"Failure\", \"Message\" : \"" + SimStatus + "\" }]}";
                    }
                    

                }
                else
                {
                    IsSimStatus = false;
                    result = "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \"Failure\", \"Message\" : \"" + SimStatus + "\" }]}";
                }

                return result;
            }
            catch (Exception ex)
            {
                return "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \"Failure Catch\" }]}";
            }
        }

        public string ActivateSIMForH2OWithPaypal(string ClientTypeID, string DistributorID, string TariffID, string SimNumber, string TariffAmount, string LoginID, string EmailID, string ZipCode, string City,  string PaymentId,string TariffPlan)
        {
            try
            {
                string result;
                Boolean IsBalance = false;
                Boolean IsSimStatus = false;
                string SimStatus = "";
                string TRANSACTIONID;
                int Regulatery = 0;
                int IsWalet = 2; // For WALET

                // double TariffAmt = Convert.ToDouble(TariffAmount);
                RechargeAndroid dbt = new RechargeAndroid();
                cSIM s = new cSIM();
                DBDistributor dis = new DBDistributor();
                cGeneral.LogSteps(DistributorID, System.Web.Hosting.HostingEnvironment.MapPath("~\\CheckBalancelog.txt"));



                int NetworkID = 0;

                net.emida.ws.webServicesService ws = new webServicesService();


                try
                {
                    string ss1 = ws.Login2("01", "A&HPrepaid", "95222", "1");
                }
                catch (Exception ex)
                {
                    return result = "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \"" + ex.Message + "\"}]}";

                }

                try  // check Sim Activation with 24 Hour 
                {
                    DataSet dsDuplicate = s.CheckDuplicateSIMActivation(SimNumber);
                    if (dsDuplicate != null)
                    {      // IsValid =0 is Allow for Activation
                        if (Convert.ToInt32(dsDuplicate.Tables[0].Rows[0]["IsValid"]) == 1)
                        {
                            return result = "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \"Failure\", \"Message\" : \" Sim number cannot be activated after 24 Hour. \" }]}";
                        }
                    }
                    else
                    {

                        return result = "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \"Failure\", \"Message\" : \"somthing wrong check Sim Activation. \" }]}";

                    }
                }
                catch
                {
                    return result = "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \"Failure\", \"Message\" : \"somthing wrong check Sim Activation. \" }]}";
                }



                DataSet dsInitiate = new DataSet();
                DataSet dsSim = s.CheckSimActivation(Convert.ToInt32(DistributorID), Convert.ToInt32(ClientTypeID), SimNumber, "Activate");

                if (dsSim.Tables[0].Rows.Count > 0)
                {
                    SimStatus = Convert.ToString(dsSim.Tables[0].Rows[0][0]);

                    if (SimStatus == "Ready to Activation")
                    {


                        DBPayment svc = new DBPayment();
                       

                        if (PaymentId == "0")
                        {
                            IsBalance = false;
                            return result = "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \"Failure\", \"Message\" : \"Your PaymentId is null \" }]}";

                        }
                        else
                        {
                               IsBalance = true;
 
                                int id = Convert.ToInt32(dsSim.Tables[1].Rows[0]["TRANSACTIONID"]);
                                TRANSACTIONID = id.ToString("00000");
                                TRANSACTIONID = "AHP" + TRANSACTIONID;

                                IsSimStatus = true;

                              string TariffCode ="0";
                              string PlanDescription = "";
                                DataSet dsTariff = dbt.GetSingleTariffDetailForActivationAPP(Convert.ToInt32(LoginID), Convert.ToInt32(DistributorID), Convert.ToInt32(ClientTypeID), Convert.ToInt32(TariffID));
                                if (dsTariff.Tables[0].Rows.Count > 0)
                                {
                                    TariffCode = Convert.ToString(dsTariff.Tables[0].Rows[0]["TariffCode"]);
                                    PlanDescription = Convert.ToString(dsTariff.Tables[0].Rows[0]["Description"]); ;
                                    NetworkID = Convert.ToInt32(dsTariff.Tables[0].Rows[0]["NetworkID"]);
                                }
                                else {
                                    return result = "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \"Failure\", \"Message\" : \"Tariff Plan is not mapped for activation \" }]}";

                                }
                             
                               
                             
                                string Language = "ENGLISH";
                                string ChannelID = "ENK";


                                string InvoiceNo = DateTime.Now.ToString().GetHashCode().ToString("X");
                                InvoiceNo = "AC" + InvoiceNo;
                                string resp = ws.GetPinProductsForActivation("01", "3756263", "1234", TariffCode, InvoiceNo, "1");
                                if (resp.Trim() != null && resp.Trim() != "")
                                {
                                    try
                                    {

                                        StringReader theReader = new StringReader(Convert.ToString(resp));
                                        DataSet theDataSet = new DataSet();
                                        theDataSet.ReadXml(theReader);

                                        if (theDataSet.Tables.Count > 0)
                                        {
                                            DataTable dt = theDataSet.Tables[1];


                                            if (dt.Rows.Count > 0 && dt.Columns.Contains("PinProductId"))
                                            {

                                                string PINid = dt.Rows[0]["PinProductId"].ToString();

                                                string ProductDescription = dt.Rows[0]["PinProductDescription"].ToString();
                                                if (PINid != "")
                                                {

                                                    RequestRes = "3756263 ," + PINid + ", " + 1234 + ", " + TariffCode + ", 1," + InvoiceNo + ", 01, 305," + SimNumber + "," + City + ", " + ZipCode;
                                                  //resp = "<ActivateGSMsimResponse><Version>01</Version><ResponseCode>00</ResponseCode><ResponseMessage>Success</ResponseMessage><serial>07041709246983|07041709247054</serial><min>4043537673</min></ActivateGSMsimResponse>";
 
                                                  resp = ws.LocusActivateGSMsim("3756263", PINid, "1234", TariffCode, "1", InvoiceNo, "01", "305", SimNumber, City, ZipCode);
                                                   

                                                    StringReader Reader = new StringReader(resp);
                                                    DataSet ds = new DataSet();
                                                    ds.ReadXml(Reader);
                                                    string Currency = Convert.ToString("$");
                                                    // Save Record ProductMaster
                                                    try
                                                    {
                                                        svc.SaveProductMaster(Convert.ToInt32(NetworkID), Convert.ToInt32(TariffID), PlanDescription.ToString(), ProductDescription.ToString(), "$", TariffAmount, Convert.ToInt32(LoginID));

                                                    }
                                                    catch { }


                                                    int loginID = Convert.ToInt32(LoginID);
                                                    string sim = SimNumber;
                                                    string zip = ZipCode;


                                                    if (ds.Tables.Count > 0)
                                                    {
                                                        DataTable dtMsg = ds.Tables[0];
                                                        if (dtMsg.Rows.Count > 0)
                                                        {
                                                            string ResponseCode = dtMsg.Rows[0]["ResponseCode"].ToString();
                                                            string ResponseMessage = dtMsg.Rows[0]["ResponseMessage"].ToString();
                                                            if (ResponseCode == "00")
                                                            {
                                                                string ALLOCATED_MSISDN = dtMsg.Rows[0]["min"].ToString();
                                                                SendMailActivation(EmailID, "Sim Activation", ALLOCATED_MSISDN, SimNumber.Trim(), "");


                                                                DBPayment sp = new DBPayment();
                                                                sp.ChargedAmount = Convert.ToDecimal(TariffAmount);
                                                                sp.PaymentType = 4;
                                                                sp.PayeeID = Convert.ToInt32(DistributorID);
                                                                sp.PaymentFrom = 9;
                                                                sp.ActivationType = 6;
                                                                sp.ActivationStatus = 15;
                                                                sp.ActivationVia = 17;
                                                                sp.ActivationResp = resp;
                                                                sp.ActivationRequest = RequestRes;
                                                                sp.TariffID = Convert.ToInt32(TariffID);
                                                                sp.ALLOCATED_MSISDN = ALLOCATED_MSISDN;
                                                                sp.TransactionId = TRANSACTIONID;
                                                                sp.PaymentMode = "App Distributor Activation";
                                                                sp.TransactionStatusId = 24;
                                                                sp.TransactionStatus = "Success";
                                                                sp.Regulatery = Regulatery;
                                                                sp.PaymentId = Convert.ToInt32(PaymentId);
                                                                sp.IsWalet = IsWalet;

                                                                try
                                                                {
                                                                    svc.SaveTransactionDetails(Convert.ToInt32(NetworkID), Convert.ToInt32(TariffCode), "10", PINid, SimNumber, InvoiceNo, TariffAmount, Currency, City, zip, "305", Convert.ToInt32(LoginID), TariffAmount.ToString());

                                                                }
                                                                catch { }

                                                                try
                                                                {
                                                                    int a = sp.UpdateAccountBalanceApp(Convert.ToInt32(DistributorID), loginID, sim, zip, ChannelID, Language);

                                                                    result = "{\"Response\" : [{\"Responsecode\" : \"0\" , \"Response\" : \"Success\" , \"Message\" : \"SIM activation done successfully with Mobile Number - " + ALLOCATED_MSISDN + "\" }] }";


                                                                }
                                                                catch (Exception ex)
                                                                {
                                                                    SaveDataApp(resp, 16, TRANSACTIONID, TariffAmount, DistributorID, TariffID, LoginID, SimNumber, ZipCode, Convert.ToInt32(PaymentId), IsWalet);
                                                                    SendMailActivationFailed(EmailID, "Sim Activation Failed", TariffPlan, SimNumber, TariffAmount, resp);
                                                             
                                                                    return result = "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \"Failure\", \"Message\" : \"SIM activation Fail Please Try Again\" }]}";

                                                                }


                                                            }
                                                            else
                                                            {
                                                                SaveDataApp(resp, 16, TRANSACTIONID, TariffAmount, DistributorID, TariffID, LoginID, SimNumber, ZipCode, Convert.ToInt32(PaymentId), IsWalet);
                                                                SendMailActivationFailed(EmailID, "Sim Activation Failed", TariffPlan, SimNumber, TariffAmount, resp);

                                                                result = "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \"Failure\", \"Message\" : \"" + ResponseMessage + "\" }]}";

                                                                

                                                            }


                                                        }
                                                        else
                                                        {
                                                            SaveDataApp(resp, 16, TRANSACTIONID, TariffAmount, DistributorID, TariffID, LoginID, SimNumber, ZipCode, Convert.ToInt32(PaymentId), IsWalet);
                                                            SendMailActivationFailed(EmailID, "Sim Activation Failed", TariffPlan, SimNumber, TariffAmount, resp);
                                                            result = "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \"Failure\", \"Message\" : \"SIM activation Fail Please Try Again\" }]}";
                                                        }


                                                    }

                                                    else
                                                    {
                                                        SaveDataApp(resp, 16, TRANSACTIONID, TariffAmount, DistributorID, TariffID, LoginID, SimNumber, ZipCode, Convert.ToInt32(PaymentId), IsWalet);
                                                        SendMailActivationFailed(EmailID, "Sim Activation Failed", TariffPlan, SimNumber, TariffAmount, resp);
                                                        result = "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \"Failure\", \"Message\" : \"SIM activation Fail Please Try Again\" }]}";
                                                    }





                                                }
                                                else
                                                {
                                                    SaveDataApp(resp, 16, TRANSACTIONID, TariffAmount, DistributorID, TariffID, LoginID, SimNumber, ZipCode, Convert.ToInt32(PaymentId), IsWalet);
                                                    SendMailActivationFailed(EmailID, "Sim Activation Failed", TariffPlan, SimNumber, TariffAmount, resp);
                                                             
                                                    result = "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \"Failure\", \"Message\" : \"SIM activation Fail Please Try Again\" }]}";

                                                }
                                            }
                                            else
                                            {
                                                SaveDataApp(resp, 16, TRANSACTIONID, TariffAmount, DistributorID, TariffID, LoginID, SimNumber, ZipCode, Convert.ToInt32(PaymentId), IsWalet);
                                                SendMailActivationFailed(EmailID, "Sim Activation Failed", TariffPlan, SimNumber, TariffAmount, resp);
                                                result = "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \"Failure\", \"Message\" : \"SIM activation Fail Please Try Again\" }]}";
                                            }
                                        }
                                        else
                                        {
                                            SaveDataApp(resp, 16, TRANSACTIONID, TariffAmount, DistributorID, TariffID, LoginID, SimNumber, ZipCode, Convert.ToInt32(PaymentId), IsWalet);
                                            SendMailActivationFailed(EmailID, "Sim Activation Failed", TariffPlan, SimNumber, TariffAmount, resp);
                                            result = "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \"Failure\", \"Message\" : \"SIM activation Fail Please Try Again\" }]}";
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        SaveDataApp(resp, 16, TRANSACTIONID, TariffAmount, DistributorID, TariffID, LoginID, SimNumber, ZipCode, Convert.ToInt32(PaymentId), IsWalet);
                                        SendMailActivationFailed(EmailID, "Sim Activation Failed", TariffPlan, SimNumber, TariffAmount, resp);
                                        result = "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \"Failure\", \"Message\" : \"SIM activation Fail Please Try Again\" }]}";
                                    }
                                }
                                else
                                {
                                    SaveDataApp(resp, 16, TRANSACTIONID, TariffAmount, DistributorID, TariffID, LoginID, SimNumber, ZipCode, Convert.ToInt32(PaymentId), IsWalet);
                                    SendMailActivationFailed(EmailID, "Sim Activation Failed", TariffPlan, SimNumber, TariffAmount, resp);
                                    result = "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \"Failure\", \"Message\" : \"SIM activation Fail Please Try Again\" }]}";
                                }
                           
                             

                        }

                    }
                    else
                    {
                        result = "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \"Failure\", \"Message\" : \"" + SimStatus + "\" }]}";
                    }


                }
                else
                {
                    IsSimStatus = false;
                    result = "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \"Failure\", \"Message\" : \"" + SimStatus + "\" }]}";
                }

                return result;
            }
            catch (Exception ex)
            {
                return "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \"Failure Catch\" }]}";
            }
        }

        public string SubscriberRecharge(int PaymentID, string NetworkID, string TariffCode, string MobileNo, string TotalAmount, string EmailID, string RechargeAmount, string State, string ZIPCode, string TxnID, string Tax, string Regulatery,  string PlanDescription,string IMEI,string Location,string IPAddress)
        {
            int DistributorID = 1;
            int LoginID = 1;
            string RechargeVia = "31";
            int IsWalet = 3;
            try
            {
                
                

                string result = ""; ;
                string ss = "";
                net.emida.ws.webServicesService ws = new webServicesService();
                RechargeAndroid svc = new RechargeAndroid();
                string simNumber = MobileNo;

                try
                {
                    string ss1 = ws.Login2("01", "A&HPrepaid", "95222", "1");
                }
                catch (Exception ex)
                {
                    string TransactionID = Convert.ToString(0);
                    string RechargeStatus = "28";
                    // string RechargeVia = "29";
                    int TransactionStatusID = 28;
                    int s1 = svc.UpdateAccountBalanceAfterRechargeSubscriberAPP(Convert.ToInt32(PaymentID), Convert.ToInt32(NetworkID), Convert.ToInt32(TariffCode), Convert.ToString(simNumber), Convert.ToDecimal(TotalAmount), DistributorID, Convert.ToString(""), RechargeStatus, RechargeVia, "", "", LoginID, 9, "Cash", TransactionID, 1, "Subscriber App Recharge  Fail", TransactionStatusID, "0", State, TxnID, Tax, Convert.ToString(RechargeAmount), "", "Subscriber", Regulatery.ToString(), IsWalet, IMEI, Location, IPAddress);
                    SendFailureMailRecharge(EmailID, (simNumber + "  Recharge Failed!"), simNumber, Convert.ToInt32(NetworkID), TariffCode, RechargeAmount, PlanDescription, ex.Message);


                    return result = "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \"" + ex.Message + "\"}]}";
                }




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

                string InvoiceNo = DateTime.Now.ToString().GetHashCode().ToString("X");
                InvoiceNo = "RC" + InvoiceNo;
                DataSet dsDuplicate = new DataSet();
                string Number = "";
                dsDuplicate = svc.CheckRechargeDuplicate(Convert.ToInt32(NetworkID), simNumber, Convert.ToInt32(TariffCode), Number);
                string Request = "01, 3756263, 1234, " + TariffCode + "," + simNumber + "," + RechargeAmount + "," + InvoiceNo + ", 1";

                if (dsDuplicate != null)
                {
                    if (Convert.ToInt32(dsDuplicate.Tables[0].Rows[0]["IsValid"]) == 0)
                    {

                        ss = "Mobile- " + MobileNo + "|" + "AmountPay- " + TotalAmount + "|" + "Network- " + NetworkID + "|" + "TariffCode- " + TariffCode + "|" + "RechargeAmount- " + RechargeAmount + "|" + "State- " + State + "|" + "ZIPCode- " + ZIPCode + "|" + "TxnID- " + TxnID + "| Tax- " + Tax;
                        Log2("Subscriber Recharge", "Subscriber Recharge");
                        Log2(ss, "Subscriber Recharge Network");

                        //  cGeneral.LogSteps(UserName + " " + Pwd, System.Web.Hosting.HostingEnvironment.MapPath("~\\ValidateLoginServicelog.txt"));
                        Log2(dsDuplicate.Tables[0].Rows[0]["Msg"].ToString(), "Show Check Message");
                        Log2(dsDuplicate.Tables[0].Rows[0]["IsValid"].ToString(), "IsValid");
                        Log2(simNumber, "SimNumber");
                        Log2(TotalAmount, "AmountPay");
                        Log2(RechargeAmount, "RechageAmount");
                        Log2("", "split");

                       // string ss2 = "<PinDistSaleResponse><Version>01</Version><InvoiceNo>RCED362DA8</InvoiceNo><ResponseCode>00</ResponseCode><Pin>2093954866</Pin><ControlNo>181477130</ControlNo><CarrierControlNo></CarrierControlNo><CustomerServiceNo>2093954866</CustomerServiceNo><TransactionDateTime>07/10/2017 23:22:21:660</TransactionDateTime><H2H_RESULT_CODE>0</H2H_RESULT_CODE><ResponseMessage>Lyca Mobile RTR $23.00 Unlimited National Plan&#x0a;&#x0a;DATE AND TIME: 07/10/2017 23:22:21:660&#x0a;&#x0a;ACCOUNT: (209) 395-4866&#x0a;&#x0a;TRANS ID: 312855630&#x0a;&#x0a;To recharge your balance: DIAL *611*PIN# OR 611&#x0a;from your Lycamobile and follow instructions.&#x0a;Customer Services: Dial 612 from your Lycamobile&#x0a;or 1 845 301 1612&#x0a;&#x0a;Recargue su saldo: Marque  *611*PIN# o 611 desde&#x0a;su Lycamobile y siga instrucciones. Atencion al&#x0a;Cliente:Marque 612 desde su Lycamobile o&#x0a;1 845 301 1612&#x0a;&#x0a;This voucher is valid for use for 120 days from&#x0a;and including the date of its redemption.&#x0a;&#x0a;Este cupon es valido por 120 dias a partir la&#x0a;fecha de su redencion.&#x0a;&#x0a;Voucher/Cupon No.: &#x0a;&#x0a;Not refundable/No reembolsable&#x0a;Not exchangeable/No intercambiable&#x0a;&#x0a;For Terms and Conditions and latest offers&#x0a;please visit:  www.lycamobile.com</ResponseMessage><TransactionId>312855630</TransactionId></PinDistSaleResponse>";
                      
                        string ss2 = ws.PinDistSale("01", "3756263", "1234", TariffCode, simNumber, RechargeAmount, InvoiceNo, "1");

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
                                    DataSet dsTransaction = svc.SaveTransactionDetails(Convert.ToInt32(NetworkID), Convert.ToInt32(TariffCode), "11", Convert.ToString(PinNumber), simNumber, InvoiceNo, Convert.ToString(RechargeAmount), Currency, State, ZIPCode, "305", Convert.ToInt32(LoginID), Convert.ToString(TotalAmount));
                                    if (dsTransaction != null)
                                    {

                                        string RechargeStatus = "27";
                                        // string @RechargeVia = "29";
                                        int TransactionStatusID = 27;

                                        SendMailRecharge(EmailID, (simNumber + "  Recharge successful!"), PinNumber, simNumber, "", Convert.ToInt32(NetworkID));

                                        int s1 = svc.UpdateAccountBalanceAfterRechargeSubscriberAPP(Convert.ToInt32(PaymentID), Convert.ToInt32(NetworkID), Convert.ToInt32(TariffCode), Convert.ToString(simNumber), Convert.ToDecimal(TotalAmount), DistributorID, Convert.ToString(ZIPCode), RechargeStatus, RechargeVia, Request, ss2, LoginID, 9, "Cash", TransactionID, 1, "Subscriber Recharge  Successfully", TransactionStatusID, PinNumber, State, TxnID, Tax, Convert.ToString(RechargeAmount), InvoiceNo, "Subscriber", Regulatery.ToString(), IsWalet, IMEI, Location, IPAddress);
                                        //  MakeReceipt(strResponse);

                                        Log2("Recharge Transaction Success", "Reason");

                                        Log2(Request, "Request");
                                        Log2(ResponseCode, "Response");
                                        Log2(ResponseMessage, "ResponseMessage");
                                        Log2(simNumber, "SimNumber");
                                        Log2(TotalAmount, "AmountPay");
                                        Log2(State, "State-City");
                                        Log2(ZIPCode, "Zip");
                                        Log2(RechargeAmount, "RechargeAmount");
                                        Log2(ss, "Detail");
                                        Log2("", "split");

                                        ws.Logout("01", "A&HPrepaid", "95222", "1");
                                        string str = "Mobile Number  " + simNumber + " Recharge  successfully ,  Pin Number " + PinNumber;


                                        result = "{\"Response\" : [{\"Responsecode\" : \"0\" , \"Response\" : \"" + str + "\"}]}";

                                        //result = "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \"Success\"}] , \"Data\" : " + str + "}";

                                        return result;

                                    }
                                    else
                                    {

                                        string TransactionIDs = Convert.ToString(0);
                                        string RechargeStatus = "28";
                                        // string RechargeVia = "29";
                                        int TransactionStatusID = 28;
                                        int s1 = svc.UpdateAccountBalanceAfterRechargeSubscriberAPP(Convert.ToInt32(PaymentID), Convert.ToInt32(NetworkID), Convert.ToInt32(TariffCode), Convert.ToString(simNumber), Convert.ToDecimal(TotalAmount), DistributorID, Convert.ToString(""), RechargeStatus, RechargeVia, Request, ss2, LoginID, 9, "Cash", TransactionIDs, 1, "Subscriber App Recharge  Fail", TransactionStatusID, PinNumber, State, TxnID, Tax, Convert.ToString(RechargeAmount), InvoiceNo, "Subscriber", Regulatery.ToString(), IsWalet, IMEI, Location, IPAddress);


                                        SendFailureMailRecharge(EmailID, (simNumber + "  Recharge Failed!"), simNumber, Convert.ToInt32(NetworkID), TariffCode, RechargeAmount, PlanDescription, ResponseMessage);

                                        result = "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \"" + ResponseMessage + "\"}]}";

                                        // result = "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \"Failure\"}] , \"Data\" : " + ResponseMessage + "}";
                                        return result;

                                    }




                                }
                                else
                                {
                                    string TransactionID = Convert.ToString(0);
                                    string RechargeStatus = "28";
                                    // string RechargeVia = "29";
                                    int TransactionStatusID = 28;
                                    int s1 = svc.UpdateAccountBalanceAfterRechargeSubscriberAPP(Convert.ToInt32(PaymentID), Convert.ToInt32(NetworkID), Convert.ToInt32(TariffCode), Convert.ToString(simNumber), Convert.ToDecimal(TotalAmount), DistributorID, Convert.ToString(""), RechargeStatus, RechargeVia, Request, ss2, LoginID, 9, "Cash", TransactionID, 1, "Subscriber App Recharge  Fail", TransactionStatusID, PinNumber, State, TxnID, Tax, Convert.ToString(RechargeAmount), InvoiceNo, "Subscriber", Regulatery.ToString(), IsWalet, IMEI, Location, IPAddress);


                                    Log2("Recharge Transaction Fail", "Reason");
                                    Log2(Request, "Request");
                                    Log2(ResponseCode, "Response");
                                    Log2(ResponseMessage, "ResponseMessage");
                                    Log2(simNumber, "SimNumber");
                                    Log2(TotalAmount, "AmountPay");
                                    Log2(TariffCode, "TariffCode");
                                    Log2(State, "State-City");
                                    Log2(ZIPCode, "Zip");
                                    Log2(RechargeAmount, "RechargeAmount");
                                    Log2("", "split");

                                    SendFailureMailRecharge(EmailID, (simNumber + "  Recharge Failed!"), simNumber, Convert.ToInt32(NetworkID), TariffCode, RechargeAmount, PlanDescription, ResponseMessage);

                                    // result = "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \"Failure\"}] , \"Data\" : " + ResponseMessage + "}";
                                    result = "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \"" + ResponseMessage + "\"}]}";
                                    return result;
                                }


                            }

                            else
                            {

                                string TransactionID = Convert.ToString(0);
                                string RechargeStatus = "28";
                                // string RechargeVia = "29";
                                int TransactionStatusID = 28;
                                int s1 = svc.UpdateAccountBalanceAfterRechargeSubscriberAPP(Convert.ToInt32(PaymentID), Convert.ToInt32(NetworkID), Convert.ToInt32(TariffCode), Convert.ToString(simNumber), Convert.ToDecimal(TotalAmount), DistributorID, Convert.ToString(""), RechargeStatus, RechargeVia, Request, ss2, LoginID, 9, "Cash", TransactionID, 1, "Subscriber App Recharge  Fail", TransactionStatusID, "", State, TxnID, Tax, Convert.ToString(RechargeAmount), InvoiceNo, "Subscriber", Regulatery.ToString(), IsWalet, IMEI, Location, IPAddress);

                                Log2("NO Record from API PinDistSale", "PinDistSale");
                                Log2(Request, "Request");

                                Log2(simNumber, "SimNumber");
                                Log2(TotalAmount, "AmountPay");
                                Log2(TariffCode, "TariffCode");
                                Log2(State, "State-City");
                                Log2(ZIPCode, "Zip");
                                Log2(RechargeAmount, "RechargeAmount");
                                Log2("", "split");
                                SendFailureMailRecharge(EmailID, (simNumber + "  Recharge Failed!"), simNumber, Convert.ToInt32(NetworkID), TariffCode, RechargeAmount, PlanDescription, "No Record from Recharge API PinDistSale");

                                result = "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \"Failure:-NO Record from Recharge API PinDistSale\" }]}";

                                return result;
                            }


                        }
                        else
                        {

                            string TransactionID = Convert.ToString(0);
                            string RechargeStatus = "28";
                            // string RechargeVia = "29";
                            int TransactionStatusID = 28;
                            int s1 = svc.UpdateAccountBalanceAfterRechargeSubscriberAPP(Convert.ToInt32(PaymentID), Convert.ToInt32(NetworkID), Convert.ToInt32(TariffCode), Convert.ToString(simNumber), Convert.ToDecimal(TotalAmount), DistributorID, Convert.ToString(""), RechargeStatus, RechargeVia, Request, ss2, LoginID, 9, "Cash", TransactionID, 1, "Subscriber App Recharge  Fail", TransactionStatusID, "", State, TxnID, Tax, Convert.ToString(RechargeAmount), InvoiceNo, "Subscriber", Regulatery.ToString(), IsWalet, IMEI, Location, IPAddress);



                            Log2("NO Record from API PinDistSale", "PinDistSale");
                            Log2(Request, "Request");

                            Log2(simNumber, "SimNumber");
                            Log2(TotalAmount, "AmountPay");
                            Log2(TariffCode, "TariffCode");
                            Log2(State, "State-City");
                            Log2(ZIPCode, "Zip");
                            Log2(RechargeAmount, "RechargeAmount");
                            Log2("", "split");

                            SendFailureMailRecharge(EmailID, (simNumber + "  Recharge Failed!"), simNumber, Convert.ToInt32(NetworkID), TariffCode, RechargeAmount, PlanDescription, "No Record from Recharge API PinDistSale");

                            result = "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \"Failure:-NO Record from Recharge API PinDistSale\" }]}";
                            return result;
                        }

                    }

                    else
                    {
                        try
                        {


                            string TransactionID = Convert.ToString(0);
                            string RechargeStatus = "28";
                            // string RechargeVia = "29";
                            int TransactionStatusID = 28;
                            int s1 = svc.UpdateAccountBalanceAfterRechargeSubscriberAPP(Convert.ToInt32(PaymentID), Convert.ToInt32(NetworkID), Convert.ToInt32(TariffCode), Convert.ToString(simNumber), Convert.ToDecimal(TotalAmount), DistributorID, Convert.ToString(""), RechargeStatus, RechargeVia, Request, "", LoginID, 9, "Cash", TransactionID, 1, "Subscriber App Recharge  Fail", TransactionStatusID, "", State, TxnID, Tax, Convert.ToString(RechargeAmount), InvoiceNo, "Subscriber", Regulatery.ToString(), IsWalet, IMEI, Location, IPAddress);


                            Log2("Recharge Transaction Duplicate Recharge", "Duplicate Recharge");
                            Log2(dsDuplicate.Tables[0].Rows[0]["Msg"].ToString(), "Show Check Duplicate Message");
                            Log2(dsDuplicate.Tables[0].Rows[0]["IsValid"].ToString(), "IsValid");
                            Log2(simNumber, "SimNumber");
                            Log2(RechargeAmount, "AmountPay");
                            Log2("", "split");

                            string RechargeDate = Convert.ToString(dsDuplicate.Tables[0].Rows[0]["RechargeDate"]).ToString();
                            string amt = Convert.ToString(dsDuplicate.Tables[0].Rows[0]["Amount"]).ToString();

                            string str = "You already Recharged " + simNumber + "  with Amount " + amt + " at " + RechargeDate + " Successfully. If you want to recharge it again  please wait 5 min.";

                            SendFailureMailRecharge(EmailID, (simNumber + "  Recharge Failed!"), simNumber, Convert.ToInt32(NetworkID), TariffCode, RechargeAmount, PlanDescription, str);


                            // result = "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : " + str + "}]}";
                            result = "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \"" + str + "\"}]}";
                            return result;
                        }
                        catch { }




                    }
                }
                else
                {
                    string TransactionID = Convert.ToString(0);
                    string RechargeStatus = "28";
                    // string RechargeVia = "29";
                    int TransactionStatusID = 28;
                    int s1 = svc.UpdateAccountBalanceAfterRechargeSubscriberAPP(Convert.ToInt32(PaymentID), Convert.ToInt32(NetworkID), Convert.ToInt32(TariffCode), Convert.ToString(simNumber), Convert.ToDecimal(TotalAmount), DistributorID, Convert.ToString(""), RechargeStatus, RechargeVia, Request, "", LoginID, 9, "Cash", TransactionID, 1, "Subscriber App Recharge  Fail", TransactionStatusID, "", State, TxnID, Tax, Convert.ToString(RechargeAmount), InvoiceNo, "Subscriber", Regulatery.ToString(), IsWalet, IMEI, Location, IPAddress);
                    SendFailureMailRecharge(EmailID, (simNumber + "  Recharge Failed!"), simNumber, Convert.ToInt32(NetworkID), TariffCode, RechargeAmount, PlanDescription, "");

                    return "{\"Response\" : [{\"Responsecode\" : \"2\" , \"Response\" : \"Duplicate Recharge Something is wrong\" }]}";
                }
                return result;

            }

            catch (Exception ex)
            {
                RechargeAndroid svc = new RechargeAndroid();
                string TransactionID = Convert.ToString(0);
                string RechargeStatus = "28";
                // string RechargeVia = "29";
                int TransactionStatusID = 28;
                int s1 = svc.UpdateAccountBalanceAfterRechargeSubscriberAPP(Convert.ToInt32(PaymentID), Convert.ToInt32(NetworkID), Convert.ToInt32(TariffCode), Convert.ToString(MobileNo), Convert.ToDecimal(TotalAmount), DistributorID, Convert.ToString(""), RechargeStatus, RechargeVia, "", "", LoginID, 9, "Cash", TransactionID, 1, "Subscriber App Recharge  Fail", TransactionStatusID, "", State, TxnID, Tax, Convert.ToString(RechargeAmount), "", "Subscriber", Regulatery.ToString(), IsWalet, IMEI, Location, IPAddress);
                SendFailureMailRecharge(EmailID, (MobileNo  + "  Recharge Failed!"), MobileNo , Convert.ToInt32(NetworkID), TariffCode, RechargeAmount, PlanDescription, ex.Message );

                return "{\"Response\" : [{\"Responsecode\" : \"2\" , \"Response\" : \"Something is wrong\" }]}";

            }

        }


        public string PortInSimForLycaMobile(string ClientTypeID, string DistributorID, string TariffID, string SimNumber, string TariffAmount, string LoginID, string EmailID, string ZipCode, string PhoneToPort, string AccountNumber, string PIN, int IsWalet, string TariffPlan, string Action)
        {
            try
            {

                string result;
                Boolean IsBalance = false;
                Boolean IsSimStatus = false;
                string SimStatus = "";
                string TRANSACTIONID;
                // double TariffAmt = Convert.ToDouble(TariffAmount);
                RechargeAndroid dbt = new RechargeAndroid();
                cSIM s = new cSIM();
                DBDistributor dis = new DBDistributor();
                cGeneral.LogSteps(DistributorID, System.Web.Hosting.HostingEnvironment.MapPath("~\\CheckBalancelog.txt"));
                string PaymentId = "0";
                string Month = "0";

                string TariffCode = "0";
                string PlanDescription = "";
                DataSet dsTariff = dbt.GetSingleTariffDetailForActivationAPP(Convert.ToInt32(LoginID), Convert.ToInt32(DistributorID), Convert.ToInt32(ClientTypeID), Convert.ToInt32(TariffID));
                if (dsTariff.Tables[0].Rows.Count > 0)
                {
                    Month = Convert.ToString(dsTariff.Tables[0].Rows[0]["Months"]);
                    TariffCode = Convert.ToString(dsTariff.Tables[0].Rows[0]["TariffCode"]);
                    PlanDescription = Convert.ToString(dsTariff.Tables[0].Rows[0]["Description"]); ;

                }
                else
                {
                    return result = "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \"Failure\", \"Message\" : \"Tariff Plan is not mapped for activation or portin \" }]}";

                }

                // 1 $ Add Regulatery 
                int Regulatery = 0;
                DateTime today = DateTime.Today;
                cReport rpt = new cReport();
                // DateTime date = new DateTime(2017, 07, 20);
                DataSet dsReg = rpt.GetRegulatery();
                DateTime date = Convert.ToDateTime(dsReg.Tables[0].Rows[0]["StartDate"]); 
                if (date <= today)
                {
                   
                        Regulatery = 1;
                        
                }

                try  // check Sim Activation with 24 Hour 
                {
                    DataSet dsDuplicate = s.CheckDuplicateSIMActivation(SimNumber);
                    if (dsDuplicate != null)
                    {      // IsValid =0 is Allow for Activation
                        if (Convert.ToInt32(dsDuplicate.Tables[0].Rows[0]["IsValid"]) == 1)
                        {
                            return result = "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \"Failure\", \"Message\" : \" Sim number cannot be portin after 24 Hour. \" }]}";

                        }
                    }
                    else
                    {

                        return result = "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \"Failure\", \"Message\" : \"somthing wrong check Sim portin. \" }]}";

                    }
                }
                catch
                {
                    return result = "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \"Failure\", \"Message\" : \"somthing wrong check Sim portin. \" }]}";
                }




                DataSet dsInitiate = new DataSet();
                DataSet dsSim = s.CheckSimPortIN(Convert.ToInt32(DistributorID), Convert.ToInt32(ClientTypeID), SimNumber);

                if (dsSim.Tables[0].Rows.Count > 0)
                {
                    SimStatus = Convert.ToString(dsSim.Tables[0].Rows[0][0]);

                    if (SimStatus == "Ready to Activation")
                    {


                        DBPayment svc = new DBPayment();
                        svc.ChargedAmount = Convert.ToDecimal(TariffAmount);
                        svc.PaymentType = 5;
                        svc.PayeeID = Convert.ToInt32(DistributorID);
                        svc.PaymentFrom = 9;
                        svc.ActivationVia = 17;
                        svc.TransactionStatusId = 23;
                        svc.TransactionStatus = "Pending";
                        svc.PaymentMode = "App Distributor PortIn";
                        svc.TxnDate = DateTime.Now.ToString();
                        svc.Currency = 1;
                        svc.TariffID = Convert.ToInt32(TariffID);
                        int dist = Convert.ToInt32(DistributorID);

                        dsInitiate = svc.InitiatePaymentWaletActivationApp(dist, Convert.ToInt32(LoginID));


                        PaymentId = Convert.ToString(dsInitiate.Tables[0].Rows[0]["PaymentId"]);

                        if (PaymentId == "0")
                        {
                            IsBalance = false;
                            return result = "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \"Failure\", \"Message\" : \"Your Account Balance is Low. Please Recharge Your Balance\" }]}";

                        }
                        else
                        {
                            IsBalance = true;

                            if (dsInitiate.Tables.Count > 0)
                            {

                                int id = Convert.ToInt32(dsSim.Tables[1].Rows[0]["TRANSACTIONID"]);
                                TRANSACTIONID = id.ToString("00000");
                                TRANSACTIONID = "ENK" + TRANSACTIONID;

                                IsSimStatus = true;
                                 // string resp = PortInSimForLycaAPI(DistributorID, LoginID, TariffID, EmailID, TRANSACTIONID, ClientTypeID, SimNumber, ZipCode, PIN, AccountNumber, PhoneToPort,Month);

                                string resp = "<ENVELOPE><HEADER><ERROR_CODE>0</ERROR_CODE><ERROR_DESC>Success</ERROR_DESC></HEADER><BODY><ACTIVATE_USIM_PORTIN_BUNDLE_RESPONSE><ALLOCATED_MSISDN>16466919962</ALLOCATED_MSISDN><PORTIN_REFERENCE_NUMBER>MNPPI0000667308</PORTIN_REFERENCE_NUMBER></ACTIVATE_USIM_PORTIN_BUNDLE_RESPONSE></BODY></ENVELOPE>";


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
                                                    SendMailActivation(EmailID, "Sim PortIn", ALLOCATED_MSISDN, SimNumber, "");

                                                    DBPayment sp = new DBPayment();
                                                    sp.ChargedAmount = Convert.ToDecimal(TariffAmount);
                                                    sp.PaymentType = 5;
                                                    sp.PayeeID = Convert.ToInt32(DistributorID);
                                                    sp.PaymentFrom = 9;
                                                    sp.ActivationType = 7;
                                                    sp.ActivationStatus = 15;
                                                    sp.ActivationVia = 17;
                                                    sp.ActivationResp = resp;
                                                    sp.ActivationRequest = RequestRes;
                                                    sp.TariffID = Convert.ToInt32(TariffID);
                                                    sp.ALLOCATED_MSISDN = ALLOCATED_MSISDN;
                                                    sp.TransactionId = TRANSACTIONID;
                                                    sp.PaymentMode = "App Distributor PortIn";
                                                    sp.TransactionStatusId = 24;
                                                    sp.TransactionStatus = "Success";
                                                    sp.Regulatery = Regulatery;
                                                    sp.PaymentId = Convert.ToInt32(PaymentId);
                                                    sp.IsWalet = IsWalet;




                                                    int loginID = Convert.ToInt32(LoginID);
                                                    string sim = SimNumber;
                                                    string zip = ZipCode;
                                                    string Language = "ENGLISH";
                                                    string ChannelID = "ENK";

                                                    int a = sp.UpdateAccountBalanceApp(Convert.ToInt32(DistributorID), loginID, sim, zip, ChannelID, Language);


                                                    result = "{\"Response\" : [{\"Responsecode\" : \"0\" , \"Response\" : \"Success\" , \"Message\" : \"PortIn Request submitted successfully with Mobile Number - " + ALLOCATED_MSISDN + "\" }] }";
                                                }
                                                else
                                                {
                                                    SaveDataAppPortin(resp, 16, TRANSACTIONID, TariffAmount, DistributorID, TariffID, LoginID, SimNumber, ZipCode, Convert.ToInt32(PaymentId), IsWalet);
                                                    SendMailActivationFailed(EmailID, "Sim PortIn Failed", TariffPlan, SimNumber, TariffAmount, resp);
                                            
                                                    result = "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \"Failure\", \"Message\" : \"PortIn Request Fail Please Try Again\" }]}";

                                                }
                                            }
                                            else
                                            {
                                                SaveDataAppPortin(resp, 16, TRANSACTIONID, TariffAmount, DistributorID, TariffID, LoginID, SimNumber, ZipCode, Convert.ToInt32(PaymentId), IsWalet);
                                                SendMailActivationFailed(EmailID, "Sim PortIn Failed", TariffPlan, SimNumber, TariffAmount, resp);
                                                result = "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \"Failure\", \"Message\" : \"PortIn Request Fail Please Try Again\" }]}";
                                            }
                                        }
                                        else
                                        {
                                            SaveDataAppPortin(resp, 16, TRANSACTIONID, TariffAmount, DistributorID, TariffID, LoginID, SimNumber, ZipCode, Convert.ToInt32(PaymentId), IsWalet);
                                            SendMailActivationFailed(EmailID, "Sim Activation Failed", TariffPlan, SimNumber, TariffAmount, resp);
                                            result = "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \"Failure\", \"Message\" : \"PortIn Request Fail Please Try Again\" }]}";
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        SaveDataAppPortin(resp, 16, TRANSACTIONID, TariffAmount, DistributorID, TariffID, LoginID, SimNumber, ZipCode, Convert.ToInt32(PaymentId), IsWalet);
                                        SendMailActivationFailed(EmailID, "Sim Activation Failed", TariffPlan, SimNumber, TariffAmount, resp);
                                        return result = "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \"Failure\", \"Message\" : \"PortIn Request Fail Please Try Again\" }]}";
                                    }
                                }
                                else
                                {
                                    SaveDataAppPortin(resp, 16, TRANSACTIONID, TariffAmount, DistributorID, TariffID, LoginID, SimNumber, ZipCode, Convert.ToInt32(PaymentId), IsWalet);
                                    SendMailActivationFailed(EmailID, "Sim Activation Failed", TariffPlan, SimNumber, TariffAmount, resp);
                                    result = "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \"Failure\", \"Message\" : \"PortIn Request Fail Please Try Again\" }]}";
                                }
                            }
                            else
                            {

                                IsSimStatus = false;
                                result = "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \"Failure\", \"Message\" : \"Your Account Balance is Low. Please Recharge Your Balance\" }]}";

                            }

                        }

                    }
                    else
                    {
                        result = "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \"Failure\", \"Message\" : \"" + SimStatus + "\" }]}";
                    }
                    //if (IsBalance == false)
                    //{
                    //    result = "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \"Failure\", \"Message\" : \"Your Account Balance is Low. Please Recharge Your Balance\" }]}";
                    //}

                }
                else
                {
                    IsSimStatus = false;
                    result = "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \"Failure\", \"Message\" : \"" + SimStatus + "\" }]}";
                }

                return result;
            }
            catch (Exception ex)
            {
                return "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \"Failure Catch\" }]}";
            }
        }

        public string PortInSimForLycaAPI(string DistributorID, string LoginID, string TariffID, string email, string TransactionID, string ClientTypeID, string SimNumber, string ZipCode, string Pin, string Account, string PhoneToPort, string Month, string Action)
        {
            string response = "";

            string months = "1";
            try
            {
                DBTariff dbt = new DBTariff();

                DataSet dsTariff = dbt.GetSingleTariffDetailForActivation(Convert.ToInt32(LoginID), Convert.ToInt32(DistributorID), Convert.ToInt32(ClientTypeID), Convert.ToInt32(TariffID), Convert.ToInt32(Month), Action);
                if (dsTariff.Tables[0].Rows.Count > 0)
                {
                    months = Convert.ToString(dsTariff.Tables[0].Rows[0]["Months"]);
                }
                string BundleID = Convert.ToString(dsTariff.Tables[0].Rows[0]["TariffTypeID"]);
                string BundleCode = Convert.ToString(dsTariff.Tables[0].Rows[0]["TariffCode"]);
                string BundleType = Convert.ToString(dsTariff.Tables[0].Rows[0]["TariffType"]); ;
                string BundleAmount = Convert.ToString(dsTariff.Tables[0].Rows[0]["LycaAmount"]);

                string PHONETOPORT = PhoneToPort;
                string ACCOUNT = Account;
                string PIN = Pin;

                string NATIONAL_BUNDLE_CODE = "";
                string NATIONAL_BUNDLE_AMOUNT = "";

                string INTERNATIONAL_BUNDLE_CODE = "";
                string INTERNATIONAL_BUNDLE_AMOUNT = "";

                string EmailID = email;

                string TOPUP_AMOUNT = "";
                string TOPUP_CARD_ID = "";
                string VOUCHER_PIN = "";



                string activation = "1";
                string SIMCARD = SimNumber;
                string ZIPCode = ZipCode;
                string Language = "ENGLISH";
                string ChannelID = "ENK";

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


                string X = "<ENVELOPE><HEADER><TRANSACTION_ID>" + TransactionID + "</TRANSACTION_ID><ENTITY>ENK</ENTITY><CHANNEL_REFERENCE>ENK</CHANNEL_REFERENCE></HEADER><BODY><ACTIVATE_USIM_PORTIN_BUNDLE_REQUEST><DETAILS><ICC_ID>" + SIMCARD + "</ICC_ID><ZIP_CODE>" + ZIPCode + "</ZIP_CODE><PREFERRED_LANGUAGE>" + Language + "</PREFERRED_LANGUAGE><P_MSISDN>" + PHONETOPORT + "</P_MSISDN><ACCOUNT_NUMBER>" + ACCOUNT + "</ACCOUNT_NUMBER><PASSWORD_PIN>" + PIN + "</PASSWORD_PIN><NO_OF_MONTHS>" + months + "</NO_OF_MONTHS><NATIONAL_BUNDLE_CODE>" + NATIONAL_BUNDLE_CODE + "</NATIONAL_BUNDLE_CODE><NATIONAL_BUNDLE_AMOUNT>" + NATIONAL_BUNDLE_AMOUNT + "</NATIONAL_BUNDLE_AMOUNT><INTERNATIONAL_BUNDLE_CODE></INTERNATIONAL_BUNDLE_CODE><INTERNATIONAL_BUNDLE_AMOUNT></INTERNATIONAL_BUNDLE_AMOUNT><TOPUP_AMOUNT>" + TOPUP_AMOUNT + "</TOPUP_AMOUNT><TOPUP_CARD_ID>" + TOPUP_CARD_ID + "</TOPUP_CARD_ID><VOUCHER_PIN>" + VOUCHER_PIN + "</VOUCHER_PIN><CHANNEL_ID>" + ChannelID + "</CHANNEL_ID><EMAIL_ID>" + EmailID + "</EMAIL_ID></DETAILS></ACTIVATE_USIM_PORTIN_BUNDLE_REQUEST></BODY></ENVELOPE>";


                RequestRes = X;

                string resp = Activation(X);
                response = resp;

                return response;


            }
            catch (Exception ex)
            {
                return response;
                //ShowPopUpMsg(ex.Message);
            }
        }

        public string InitiatePaymentPaypalPortInForLycaMobile(int loginID, int DistributorID, string ChargedAmount, string TariffID)
        {
            try
            {



                DBPayment svc = new DBPayment();

                string MSG = loginID + " " + DistributorID + " " + ChargedAmount;
                cGeneral.LogSteps(MSG, System.Web.Hosting.HostingEnvironment.MapPath("~\\PaypalTopup.txt"));
                string result;
                DataSet ds = new DataSet();

                svc.ChargedAmount = Convert.ToDecimal(ChargedAmount);
                svc.PaymentType = 5;
                svc.PayeeID = Convert.ToInt32(DistributorID);
                svc.PaymentFrom = 9;
                svc.ActivationVia = 17;
                svc.TransactionStatusId = 23;
                svc.TransactionStatus = "Pending";
                svc.PaymentMode = "App Distributor PortIn";
                svc.TxnDate = DateTime.Now.ToString();
                svc.Currency = 1;
                svc.TariffID = Convert.ToInt32(TariffID);
                int dist = Convert.ToInt32(DistributorID);

                ds = svc.InitiatePaymentPaypalActivationApp(dist, loginID);


                if (ds.Tables.Count > 0)
                {
                    string LoginUrl = "";
                    string PaymentId = Convert.ToString(ds.Tables[0].Rows[0]["PaymentId"]);
                    result = cGeneral.GetJson(ds.Tables[0]);
                    if (PaymentId != "0")
                    {
                        //result = "{\"Response\" : [{\"Responsecode\" : \"0\" , \"Response\" : \"Success\" }]}";
                        result = "{\"Response\" : [{\"Responsecode\" : \"0\" , \"Response\" : \"Success\"}] , \"Data\" : " + result + "}";
                    }
                    else
                    {

                        result = "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \"Failure\" }]}";
                    }

                }
                else
                {
                    result = "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \"Failure\" }]}";
                }

                return result;
            }
            catch (Exception ex)
            {

                return "{\"Response\" : [{\"Responsecode\" : \"2\" , \"Response\" : \"Something wrong\" }]}";
            }
        }
        public string PortInSIMForLycaMobileWithPaypal(string ClientTypeID, string DistributorID, string TariffID, string SimNumber, string TariffAmount, string LoginID, string EmailID, string ZipCode, string PhoneToPort, string AccountNumber, string PIN, int PaymentID, string TariffPlan, string Action)
        {
            try
            {
                string result;
                int IsWalet = 2; // For WALET

                Boolean IsBalance = false;
                Boolean IsSimStatus = false;
                string SimStatus = "";
                string TRANSACTIONID;
                // double TariffAmt = Convert.ToDouble(TariffAmount);

                cSIM s = new cSIM();
                DBDistributor dis = new DBDistributor();
                cGeneral.LogSteps(DistributorID, System.Web.Hosting.HostingEnvironment.MapPath("~\\CheckBalancelog.txt"));

                // 1 $ Add Regulatery 
                int Regulatery = 0;
                DateTime today = DateTime.Today;
                cReport rpt = new cReport();
                // DateTime date = new DateTime(2017, 07, 20);
                DataSet dsReg = rpt.GetRegulatery();
                DateTime date = Convert.ToDateTime(dsReg.Tables[0].Rows[0]["StartDate"]); 
                if (date <= today)
                {
                    
                        Regulatery = 1;
                     
                }
                

                try  // check Sim Activation with 24 Hour 
                {
                    DataSet dsDuplicate = s.CheckDuplicateSIMActivation(SimNumber);
                    if (dsDuplicate != null)
                    {      // IsValid =0 is Allow for Activation
                        if (Convert.ToInt32(dsDuplicate.Tables[0].Rows[0]["IsValid"]) == 1)
                        {
                            return result = "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \"Failure\", \"Message\" : \" Sim number cannot be portin after 24 Hour. \" }]}";

                        }
                    }
                    else
                    {

                        return result = "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \"Failure\", \"Message\" : \"somthing wrong check Sim portin. \" }]}";

                    }
                }
                catch
                {
                    return result = "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \"Failure\", \"Message\" : \"somthing wrong check Sim portin. \" }]}";
                }

                DataSet dsSim = s.CheckSimPortIN(Convert.ToInt32(DistributorID), Convert.ToInt32(ClientTypeID), SimNumber);
                if (dsSim.Tables[0].Rows.Count > 0)
                {
                    SimStatus = Convert.ToString(dsSim.Tables[0].Rows[0][0]);

                    if (SimStatus == "Ready to Activation")
                    {


                        int id = Convert.ToInt32(dsSim.Tables[1].Rows[0]["TRANSACTIONID"]);
                        TRANSACTIONID = id.ToString("00000");
                        TRANSACTIONID = "AHP" + TRANSACTIONID;

                        IsSimStatus = true;
                       
                        string resp = PortInSimForLycaAPI(DistributorID, LoginID, TariffID, EmailID, TRANSACTIONID, ClientTypeID, SimNumber, ZipCode, PIN, AccountNumber, PhoneToPort, "1", Action); //Month variable needs correction
                        //string resp = "<ENVELOPE><HEADER><ERROR_CODE>0</ERROR_CODE><ERROR_DESC>Success</ERROR_DESC></HEADER><BODY><ACTIVATE_USIM_PORTIN_BUNDLE_RESPONSE><ALLOCATED_MSISDN>16466919962</ALLOCATED_MSISDN><PORTIN_REFERENCE_NUMBER>MNPPI0000667308</PORTIN_REFERENCE_NUMBER></ACTIVATE_USIM_PORTIN_BUNDLE_RESPONSE></BODY></ENVELOPE>";
 
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
                                            SendMailActivation(EmailID, "Sim PortIn", ALLOCATED_MSISDN, SimNumber, "");

                                            DBPayment sp = new DBPayment();
                                            sp.ChargedAmount = Convert.ToDecimal(TariffAmount);
                                            sp.PaymentType = 5;
                                            sp.PayeeID = Convert.ToInt32(DistributorID);
                                            sp.PaymentFrom = 9;
                                            sp.ActivationType = 7;
                                            sp.ActivationStatus = 15;
                                            sp.ActivationVia = 17;
                                            sp.ActivationResp = resp;
                                            sp.ActivationRequest = RequestRes;
                                            sp.TariffID = Convert.ToInt32(TariffID);
                                            sp.ALLOCATED_MSISDN = ALLOCATED_MSISDN;
                                            sp.TransactionId = TRANSACTIONID;
                                            sp.PaymentMode = "App Distributor PortIn";
                                            sp.TransactionStatusId = 24;
                                            sp.TransactionStatus = "Success";
                                            sp.Regulatery = Regulatery;
                                            sp.PaymentId = Convert.ToInt32(PaymentID);
                                            sp.IsWalet = IsWalet;



                                            int loginID = Convert.ToInt32(LoginID);
                                            string sim = SimNumber;
                                            string zip = ZipCode;
                                            string Language = "ENGLISH";
                                            string ChannelID = "ENK";

                                            int a = sp.UpdateAccountBalanceApp(Convert.ToInt32(DistributorID), loginID, sim, zip, ChannelID, Language);


                                            result = "{\"Response\" : [{\"Responsecode\" : \"0\" , \"Response\" : \"Success\" , \"Message\" : \"PortIn Request submitted successfully with Mobile Number  - " + ALLOCATED_MSISDN + "\" }] }";
                                        }
                                        else
                                        {
                                            SaveDataAppPortin(resp, 16, TRANSACTIONID, TariffAmount, DistributorID, TariffID, LoginID, SimNumber, ZipCode, PaymentID, IsWalet);
                                            SendMailActivationFailed(EmailID, "Sim PortIn Failed", TariffPlan, SimNumber, TariffAmount, resp);

                                            result = "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \"Failure\", \"Message\" : \"PortIn Request Fail Please Try Again\" }]}";

                                        }
                                    }
                                    else
                                    {
                                        SaveDataAppPortin(resp, 16, TRANSACTIONID, TariffAmount, DistributorID, TariffID, LoginID, SimNumber, ZipCode, PaymentID, IsWalet);
                                        SendMailActivationFailed(EmailID, "Sim PortIn Failed", TariffPlan, SimNumber, TariffAmount, resp);
                                        result = "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \"Failure\", \"Message\" : \"PortIn Request Fail Please Try Again\" }]}";
                                    }
                                }
                                else
                                {
                                    SaveDataAppPortin(resp, 16, TRANSACTIONID, TariffAmount, DistributorID, TariffID, LoginID, SimNumber, ZipCode, PaymentID, IsWalet);
                                    SendMailActivationFailed(EmailID, "Sim PortIn Failed", TariffPlan, SimNumber, TariffAmount, resp);
                                    result = "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \"Failure\", \"Message\" : \"PortIn Request Fail Please Try Again\" }]}";
                                }
                            }
                            catch (Exception ex)
                            {
                                SaveDataAppPortin(resp, 16, TRANSACTIONID, TariffAmount, DistributorID, TariffID, LoginID, SimNumber, ZipCode, PaymentID, IsWalet);
                                SendMailActivationFailed(EmailID, "Sim PortIn Failed", TariffPlan, SimNumber, TariffAmount, resp);
                                return result = "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \"Failure\", \"Message\" : \"PortIn Request Fail Please Try Again\" }]}";
                            }
                        }
                        else
                        {
                            SaveDataAppPortin(resp, 16, TRANSACTIONID, TariffAmount, DistributorID, TariffID, LoginID, SimNumber, ZipCode, PaymentID, IsWalet);
                            SendMailActivationFailed(EmailID, "Sim PortIn Failed", TariffPlan, SimNumber, TariffAmount, resp);
                            result = "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \"Failure\", \"Message\" : \"PortIn Request Fail Please Try Again\" }]}";
                        }


                    }



                    else
                    {

                        result = "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \"Failure\", \"Message\" : \"" + SimStatus + "\" }]}";
                    }
                }
                else
                {
                    result = "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \"Failure Sim Invalid\" }]}";
                }

                return result;
            }
            catch (Exception ex)
            {
                return "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \" some thingwrong\" }]}";
            }
        }

        
        public void SendMailH20AndEasyGoPortIn(string SendTo, string Subject, string UserName, string UserID, string pass, string Network, string PlanDescription, string PlanAmount, string EmailID, string ZipCode, string Phonetoport, string Account, string Pin, string ServiceProvider, string State, string City, string CustomerName, string Address)
        {
            try
            {
                string LogoUrl = "http://www.activatemysim.net/img/logo.png";

                //string MailAddress = ConfigurationManager.AppSettings.Get("Fromail");
                //string PassWord = ConfigurationManager.AppSettings.Get("Password");

                string MailAddress = "ENK.sim@gmail.com";
                string PassWord = "9711679656";
                string Host = "smtp.gmail.com";
                string sCcEmail = "cs@emida.net;customerservice@emida.net";
                //string sCcEmail = "SHADAB.A@VIRTUZO.IN";
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient();

                
                
             

                
                mail.From = new MailAddress(MailAddress);
                mail.To.Add(SendTo);
                TimeSpan ts = new TimeSpan(8, 0, 0);
                mail.Subject = "SiteID-3756263  " + Subject + " " + DateTime.UtcNow.Subtract(ts).ToString();


               
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
                sb.Append("<div style='border:1px solid black;padding-left: 24px;width: 388px; BORDER-RADIUS: 25px;font-style:italic;'>");
                sb.Append("<p><strong> Sim Number </strong> : " + UserID + "<p>");
                sb.Append("<p><strong> Network </strong> : " + Network + "<p>");
                sb.Append("<p><strong> Tariff/Plan </strong> : " + PlanDescription + "<p>");
                sb.Append("<p><strong> Amount</strong> : " +PlanAmount  + "<p>");
                sb.Append("<p><strong> Zip Code</strong> : " + ZipCode  + "<p>");
                sb.Append("<p><strong> Phone Number</strong> : " + Phonetoport + "<p>");
                sb.Append("<p><strong> Account Number</strong> : " + Account  + "<p>");
                sb.Append("<p><strong> Pin </strong>:" + Pin + "<p>");
                sb.Append("<p><strong> Email Address</strong> : " + EmailID + "<p>");

                sb.Append("<p><strong> Current Service Provider </strong> : " + ServiceProvider + "<p>");
                sb.Append("<p><strong>State</strong> : " + State + "<p>");
                sb.Append("<p><strong> City</strong> : " + City + "<p>");
                sb.Append("<p><strong>Customer 1st and Last Name</strong> : " + CustomerName + "<p>");
                sb.Append("<p><strong>Address </strong> : " + Address + "<p>");
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

                SmtpServer.Host = Host;
                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential(MailAddress, PassWord);
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);

            }
            catch (Exception ex)
            {

            }
        }

        public  string PortInSIMForH2O(string ClientTypeID, string DistributorID, string TariffID, string SimNumber, string TariffAmount, string LoginID, string EmailID, string ZipCode, string City, string Phonetoport, string AccountNumber, string PIN, string ServiceProvider, string State, string CustomerName, string Address, int IsWalet)
       {
            try
            {
                string result;
                Boolean IsBalance = false;
                Boolean IsSimStatus = false;
                string SimStatus = "";
                string TRANSACTIONID;
                int Regulatery = 0;
                // double TariffAmt = Convert.ToDouble(TariffAmount);
                RechargeAndroid dbt = new RechargeAndroid();
                cSIM s = new cSIM();
                DBDistributor dis = new DBDistributor();
                cGeneral.LogSteps(DistributorID, System.Web.Hosting.HostingEnvironment.MapPath("~\\CheckBalancelog.txt"));

                string PaymentId = "0";

                int NetworkID = 0;

                
 
                try  // check Sim Activation with 24 Hour 
                {
                    DataSet dsDuplicate = s.CheckDuplicateSIMActivation(SimNumber);
                    if (dsDuplicate != null)
                    {      // IsValid =0 is Allow for Activation
                        if (Convert.ToInt32(dsDuplicate.Tables[0].Rows[0]["IsValid"]) == 1)
                        {
                            return result = "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \"Failure\", \"Message\" : \" Sim number cannot be portin after 24 Hour. \" }]}";

                        }
                    }
                    else
                    {

                        return result = "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \"Failure\", \"Message\" : \"somthing wrong check Sim portin. \" }]}";

                    }
                }
                catch
                {
                    return result = "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \"Failure\", \"Message\" : \"somthing wrong check Sim portin. \" }]}";
                }


                DataSet dsInitiate = new DataSet();
                DataSet dsSim = s.CheckSimPortIN(Convert.ToInt32(DistributorID), Convert.ToInt32(ClientTypeID), SimNumber);

                if (dsSim.Tables[0].Rows.Count > 0)
                {
                    SimStatus = Convert.ToString(dsSim.Tables[0].Rows[0][0]);


                    if (SimStatus == "Ready to Activation")
                    {

                        string TariffCode = "0";
                        string PlanDescription = "";
                        string Network = "";
                        DataSet dsTariff = dbt.GetSingleTariffDetailForActivationAPP(Convert.ToInt32(LoginID), Convert.ToInt32(DistributorID), Convert.ToInt32(ClientTypeID), Convert.ToInt32(TariffID));
                        if (dsTariff.Tables[0].Rows.Count > 0)
                        {
                            TariffCode = Convert.ToString(dsTariff.Tables[0].Rows[0]["TariffCode"]);
                            PlanDescription = Convert.ToString(dsTariff.Tables[0].Rows[0]["Description"]); ;
                            NetworkID = Convert.ToInt32(dsTariff.Tables[0].Rows[0]["NetworkID"]);
                            Network = Convert.ToString(dsTariff.Tables[0].Rows[0]["Network"]);
                        }
                        else
                        {
                            return result = "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \"Failure\", \"Message\" : \"Tariff Plan is not mapped for portin \" }]}";

                        }


                        DBPayment svc = new DBPayment();
                        svc.ChargedAmount = Convert.ToDecimal(TariffAmount);
                        svc.PaymentType =5;
                        svc.PayeeID = Convert.ToInt32(DistributorID);
                        svc.PaymentFrom = 9;
                        svc.ActivationVia = 17;
                        svc.TransactionStatusId = 23;
                        svc.TransactionStatus = "Pending";
                        svc.PaymentMode = "App Distributor PortIn";
                        svc.TxnDate = DateTime.Now.ToString();
                        svc.Currency = 1;
                        svc.TariffID = Convert.ToInt32(TariffID);
                        int dist = Convert.ToInt32(DistributorID);

                        dsInitiate = svc.InitiatePaymentWaletActivationApp(dist, Convert.ToInt32(LoginID));


                        PaymentId = Convert.ToString(dsInitiate.Tables[0].Rows[0]["PaymentId"]);

                        if (PaymentId == "0")
                        {
                            IsBalance = false;
                           return  result = "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \"Failure\", \"Message\" : \"Your Account Balance is Low. Please Recharge Your Balance\" }]}";

                        }
                        else
                        {
                            IsBalance = true;

                            if (dsInitiate.Tables.Count > 0)
                            {

                                int id = Convert.ToInt32(dsSim.Tables[1].Rows[0]["TRANSACTIONID"]);
                                TRANSACTIONID = id.ToString("00000");
                                TRANSACTIONID = "AHP" + TRANSACTIONID;

                                IsSimStatus = true;
                                string Language = "ENGLISH";
                                string ChannelID = "ENK";
                               

                                string InvoiceNo = DateTime.Now.ToString().GetHashCode().ToString("X");
                                InvoiceNo = "AC" + InvoiceNo;
                             
                                 
                                                                DBPayment sp = new DBPayment();
                                                                sp.ChargedAmount = Convert.ToDecimal(TariffAmount);
                                                                sp.PaymentType = 5;
                                                                sp.PayeeID = Convert.ToInt32(DistributorID);
                                                                sp.PaymentFrom = 9;
                                                                sp.ActivationType = 7;
                                                                sp.ActivationStatus = 15;
                                                                sp.ActivationVia = 17;
                                                                sp.ActivationResp = "";
                                                                sp.ActivationRequest = "";
                                                                sp.TariffID = Convert.ToInt32(TariffID);
                                                                sp.ALLOCATED_MSISDN = "";
                                                                sp.TransactionId = TRANSACTIONID;
                                                                sp.PaymentMode = "App Distributor PortIn";
                                                                sp.TransactionStatusId = 24;
                                                                sp.TransactionStatus = "Success";
                                                                sp.Regulatery = Regulatery;
                                                                sp.PaymentId = Convert.ToInt32(PaymentId);
                                                                sp.IsWalet = IsWalet;

                                                                try
                                                                {
                                                                    sp.SaveProductMaster(Convert.ToInt32(NetworkID), Convert.ToInt32(TariffID), PlanDescription.ToString(), PlanDescription.ToString(), "$", TariffAmount, Convert.ToInt32(LoginID));

                                                                    sp.SaveTransactionDetails(Convert.ToInt32(NetworkID), Convert.ToInt32(TariffCode), "10", "0", SimNumber, InvoiceNo, TariffAmount, "$", City, ZipCode, "305", Convert.ToInt32(LoginID), TariffAmount.ToString());

                                                                }
                                                                catch
                                                                {
                                                                 SaveDataAppPortin("", 16, TRANSACTIONID, TariffAmount, DistributorID, TariffID, LoginID, SimNumber, ZipCode, Convert.ToInt32(PaymentId), IsWalet);
                                                                 SendMailActivationFailed(EmailID, "Sim PortIn Failed", PlanDescription, SimNumber, TariffAmount, "");
                                                                 return result = "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \"Failure\", \"Message\" : \"PortIn Request Fail Please Try Again\" }]}";
                        
                                                                }
                                                                
                                                                try
                                                                {
                                                                    int a = sp.UpdateAccountBalanceApp(Convert.ToInt32(DistributorID), Convert.ToInt32(LoginID), SimNumber, ZipCode, ChannelID, Language);
                                                                    SendMailH20AndEasyGoPortIn(EmailID.Trim(), "Sim PortIn", "", SimNumber.Trim(), "", Network, PlanDescription, TariffAmount, EmailID, ZipCode, Phonetoport, AccountNumber, PIN, ServiceProvider, State, City, CustomerName,Address);
                                                                    result = "{\"Response\" : [{\"Responsecode\" : \"0\" , \"Response\" : \"Success\" , \"Message\" : \"PortIn Request submitted successfully\" }] }";


                                                                }
                                catch
                                {
                                SaveDataAppPortin("", 16, TRANSACTIONID, TariffAmount, DistributorID, TariffID, LoginID, SimNumber, ZipCode, Convert.ToInt32(PaymentId), IsWalet);
                                SendMailActivationFailed(EmailID, "Sim PortIn Failed", PlanDescription, SimNumber, TariffAmount, "");
                                return result = "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \"Failure\", \"Message\" : \"PortIn Request Fail Please Try Again\" }]}";
                        
                                }
                                
                      
                            
                                 
                            }
                            else
                            {

                                IsSimStatus = false;
                                result = "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \"Failure\", \"Message\" : \"Your Account Balance is Low. Please Recharge Your Balance\" }]}";

                            }

                        }

                    }
                    else
                    {
                        result = "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \"Failure\", \"Message\" : \"" + SimStatus + "\" }]}";
                    }
                    

                }
                else
                {
                    IsSimStatus = false;
                    result = "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \"Failure\", \"Message\" : \"" + SimStatus + "\" }]}";
                }

                return result;
            }
            catch (Exception ex)
            {
                return "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \"Failure Catch\" }]}";
            }
         }

        public string PortInSIMForH2OWithPaypal(string ClientTypeID, string DistributorID, string TariffID, string SimNumber, string TariffAmount, string LoginID, string EmailID, string ZipCode, string City, string Phonetoport, string AccountNumber, string PIN, string ServiceProvider, string State, string CustomerName, string Address, string PaymentId)
        {
            try
            {
                string result;
                Boolean IsBalance = false;
                Boolean IsSimStatus = false;
                string SimStatus = "";
                string TRANSACTIONID;
                int Regulatery = 0;
                // double TariffAmt = Convert.ToDouble(TariffAmount);
                RechargeAndroid dbt = new RechargeAndroid();
                cSIM s = new cSIM();
                DBDistributor dis = new DBDistributor();
                cGeneral.LogSteps(DistributorID, System.Web.Hosting.HostingEnvironment.MapPath("~\\CheckBalancelog.txt"));

                int IsWalet = 2; // For WALET

                int NetworkID = 0;

                 

                try  // check Sim Activation with 24 Hour 
                {
                    DataSet dsDuplicate = s.CheckDuplicateSIMActivation(SimNumber);
                    if (dsDuplicate != null)
                    {      // IsValid =0 is Allow for Activation
                        if (Convert.ToInt32(dsDuplicate.Tables[0].Rows[0]["IsValid"]) == 1)
                        {
                            return result = "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \"Failure\", \"Message\" : \" Sim number cannot be portin after 24 Hour. \" }]}";

                        }
                    }
                    else
                    {

                        return result = "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \"Failure\", \"Message\" : \"somthing wrong check Sim portin. \" }]}";

                    }
                }
                catch
                {
                    return result = "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \"Failure\", \"Message\" : \"somthing wrong check Sim portin. \" }]}";
                }


               
                DataSet dsSim = s.CheckSimPortIN(Convert.ToInt32(DistributorID), Convert.ToInt32(ClientTypeID), SimNumber);

                if (dsSim.Tables[0].Rows.Count > 0)
                {
                    SimStatus = Convert.ToString(dsSim.Tables[0].Rows[0][0]);


                    if (SimStatus == "Ready to Activation")
                    {

                       


                        string TariffCode = "0";
                        string PlanDescription = "";
                        string Network = "";
                        DataSet dsTariff = dbt.GetSingleTariffDetailForActivationAPP(Convert.ToInt32(LoginID), Convert.ToInt32(DistributorID), Convert.ToInt32(ClientTypeID), Convert.ToInt32(TariffID));
                        if (dsTariff.Tables[0].Rows.Count > 0)
                        {
                            TariffCode = Convert.ToString(dsTariff.Tables[0].Rows[0]["TariffCode"]);
                            PlanDescription = Convert.ToString(dsTariff.Tables[0].Rows[0]["Description"]); ;
                            NetworkID = Convert.ToInt32(dsTariff.Tables[0].Rows[0]["NetworkID"]);
                            Network = Convert.ToString(dsTariff.Tables[0].Rows[0]["Network"]);
                        }
                        else
                        {
                            return result = "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \"Failure\", \"Message\" : \"Tariff Plan is not mapped for portin \" }]}";

                        }





                        if (PaymentId == "0")
                        {
                            IsBalance = false;
                            return result = "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \"Failure\", \"Message\" : \"Your PaymentId is null \" }]}";

                        }
                        else
                        {
                                IsBalance = true;
 
                                int id = Convert.ToInt32(dsSim.Tables[1].Rows[0]["TRANSACTIONID"]);
                                TRANSACTIONID = id.ToString("00000");
                                TRANSACTIONID = "AHP" + TRANSACTIONID;

                                IsSimStatus = true;
                                string Language = "ENGLISH";
                                string ChannelID = "ENK";


                                string InvoiceNo = DateTime.Now.ToString().GetHashCode().ToString("X");
                                InvoiceNo = "AC" + InvoiceNo;

                              
                                DBPayment sp = new DBPayment();
                                sp.ChargedAmount = Convert.ToDecimal(TariffAmount);
                                sp.PaymentType = 5;
                                sp.PayeeID = Convert.ToInt32(DistributorID);
                                sp.PaymentFrom = 9;
                                sp.ActivationType = 7;
                                sp.ActivationStatus = 15;
                                sp.ActivationVia = 17;
                                sp.ActivationResp = "";
                                sp.ActivationRequest = "";
                                sp.TariffID = Convert.ToInt32(TariffID);
                                sp.ALLOCATED_MSISDN = "";
                                sp.TransactionId = TRANSACTIONID;
                                sp.PaymentMode = "App Distributor PortIn";
                                sp.TransactionStatusId = 24;
                                sp.TransactionStatus = "Success";
                                sp.Regulatery = Regulatery;
                                sp.PaymentId = Convert.ToInt32(PaymentId);
                                sp.IsWalet = IsWalet;

                                try
                                {
                                    sp.SaveProductMaster(Convert.ToInt32(NetworkID), Convert.ToInt32(TariffID), PlanDescription.ToString(), PlanDescription.ToString(), "$", TariffAmount, Convert.ToInt32(LoginID));

                                    sp.SaveTransactionDetails(Convert.ToInt32(NetworkID), Convert.ToInt32(TariffCode), "10", "0", SimNumber, InvoiceNo, TariffAmount, "$", City, ZipCode, "305", Convert.ToInt32(LoginID), TariffAmount.ToString());

                                }
                                catch
                                {
                                    SaveDataAppPortin("", 16, TRANSACTIONID, TariffAmount, DistributorID, TariffID, LoginID, SimNumber, ZipCode, Convert.ToInt32(PaymentId), IsWalet);
                                    SendMailActivationFailed(EmailID, "Sim PortIn Failed", PlanDescription, SimNumber, TariffAmount, "");
                                    return result = "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \"Failure\", \"Message\" : \"PortIn Request Fail Please Try Again\" }]}";

                                }

                                try
                                {
                                    int a = sp.UpdateAccountBalanceApp(Convert.ToInt32(DistributorID), Convert.ToInt32(LoginID), SimNumber, ZipCode, ChannelID, Language);
                                    SendMailH20AndEasyGoPortIn(EmailID.Trim(), "Sim PortIn", "", SimNumber.Trim(), "", Network, PlanDescription, TariffAmount, EmailID, ZipCode, Phonetoport, AccountNumber, PIN, ServiceProvider, State, City, CustomerName, Address);
                                    result = "{\"Response\" : [{\"Responsecode\" : \"0\" , \"Response\" : \"Success\" , \"Message\" : \"PortIn Request submitted successfully\" }] }";


                                }
                                catch 
                                {
                                    SaveDataAppPortin("", 16, TRANSACTIONID, TariffAmount, DistributorID, TariffID, LoginID, SimNumber, ZipCode, Convert.ToInt32(PaymentId), IsWalet);
                                    SendMailActivationFailed(EmailID, "Sim PortIn Failed", PlanDescription, SimNumber, TariffAmount, "");
                                    return result = "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \"Failure\", \"Message\" : \"PortIn Request Fail Please Try Again\" }]}";

                                }




                            
                             

                        }

                    }
                    else
                    {
                        result = "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \"Failure\", \"Message\" : \"" + SimStatus + "\" }]}";
                    }


                }
                else
                {
                    IsSimStatus = false;
                    result = "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \"Failure\", \"Message\" : \"" + SimStatus + "\" }]}";
                }

                return result;
            }
            catch (Exception ex)
            {
                return "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \"Failure Catch\" }]}";
            }
        }


        #endregion

        #region "IOS"

        public string ValidateLoginService_IOS2(string UserName, string Pwd)
        {
            try
            {
                DataSet dsMain = new DataSet();
                string result;
                DBUsers ud = new DBUsers();
                cGeneral.LogSteps(UserName + " " + Pwd, System.Web.Hosting.HostingEnvironment.MapPath("~\\ValidateLoginServicelog.txt"));
                string pass = Encryption.Encrypt(Pwd);
                DataSet ds = ud.ValidateLogin(UserName, pass);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    //DataTable dt = new DataTable();
                    //dt.TableName = "Response";
                    //dt.Columns.Add("Responsecode");
                    //dt.Columns.Add("Response");

                    //DataRow dr = dt.NewRow();
                    //dr["Responsecode"] = "0";
                    //dr["Response"] = "Success";
                    //dt.Rows.Add(dr);
                    //dt.AcceptChanges();

                    //dsMain.Tables.Add(dt);

                    //DataTable dtmain = new DataTable();
                    //dtmain = ds.Tables[0];

                    //dsMain.Tables.Add(dtmain.Copy());
                    //dsMain.Tables[1].TableName = "Data";

                    //result = Newtonsoft.Json.JsonConvert.SerializeObject(dsMain, Formatting.None);

                    result = "{\"Response\":[{\"Responsecode\":\"0\",\"Response\":\"Success\"}],\"Data\":[{\"ID\":1,\"DistributorID\":1,\"RoleID\":0,\"UserName\":\"admin\",\"Password\":\"LEDdWe3AXsJy+ECechi3fQ==\",\"Fname\":\"ENK.Ltd\",\"Lname\":\"\",\"EmailID\":\"info@ENK.com\",\"MobileNumber\":\"9711679656\",\"ActiveFromDtTm\":\"20-May-2015\",\"ActiveToDtTm\":\"30-Aug-2015\",\"CreatedBy\":1,\"ClientType\":1,\"Name\":\"Company\",\"Currency\":\"USD\",\"ClientTypeID\":1,\"CurrencyId\":1}]}";
                    //result = cGeneral.GetJson(ds.Tables[0]);
                    //result = "{\"Response\" : [{\"Responsecode\" : \"0\" , \"Response\" : \"Success\"}] , " + result + "}";
                    
                }
                else
                {
                    result = "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \"Failure\" }]}";
                }

                return result;


            }
            catch (Exception ex)
            {
                ServiceData myServiceData = new ServiceData();
                myServiceData.Result = false;
                myServiceData.ErrorMessage = "unforeseen error occured. Please try later.";
                myServiceData.ErrorDetails = ex.ToString();
                throw new FaultException<ServiceData>(myServiceData, ex.ToString());
            }
        }

        public List<Person> GetPlayers()
        {
            List<Person> players = new List<Person>();
            players.Add(new Person("Peyton", "Manning",35));
            players.Add(new Person("Drew", "Brees",  31));
            players.Add(new Person ("Brett",  "Favre",  58));

            return players;
        }

        public string ValidateLoginService_IOS1(string UserName, string Pwd)
        {
            try
            {
                DataSet dsMain = new DataSet();
                string result;
                DBUsers ud = new DBUsers();
                cGeneral.LogSteps(UserName + " " + Pwd, System.Web.Hosting.HostingEnvironment.MapPath("~\\ValidateLoginServicelog.txt"));
                string pass = Encryption.Encrypt(Pwd);
                DataSet ds = ud.ValidateLogin(UserName, pass);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    //DataTable dt = new DataTable();
                    //dt.TableName = "Response";
                    //dt.Columns.Add("Responsecode");
                    //dt.Columns.Add("Response");

                    //DataRow dr = dt.NewRow();
                    //dr["Responsecode"] = "0";
                    //dr["Response"] = "Success";
                    //dt.Rows.Add(dr);
                    //dt.AcceptChanges();

                    //dsMain.Tables.Add(dt);

                    //DataTable dtmain = new DataTable();
                    //dtmain = ds.Tables[0];

                    //dsMain.Tables.Add(dtmain.Copy());
                    //dsMain.Tables[1].TableName = "Data";

                    result = Newtonsoft.Json.JsonConvert.SerializeObject(ds.Tables[0], Formatting.None);
                    return result;
                    //return DataTableToJSON(ds.Tables[0]);
                    //result = cGeneral.GetJson(ds.Tables[0]);
                    //result = "{\"Response\" : [{\"Responsecode\" : \"0\" , \"Response\" : \"Success\"}] , " + result + "}";

                }
                else
                {
                    //result = "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \"Failure\" }]}";
                    return null;
                }

                //return result;


            }
            catch (Exception ex)
            {
                ServiceData myServiceData = new ServiceData();
                myServiceData.Result = false;
                myServiceData.ErrorMessage = "unforeseen error occured. Please try later.";
                myServiceData.ErrorDetails = ex.ToString();
                throw new FaultException<ServiceData>(myServiceData, ex.ToString());
            }
        }

        public List<User> ValidateLoginService(string UserName, string Pwd, string DeviceTokenID, string DeviceType)
        {
            try
            {
                DataSet dsMain = new DataSet();

                List<User> Users = new List<User>();
                DBUsers ud = new DBUsers();
                cGeneral.LogSteps(UserName + " " + Pwd + " " + DeviceTokenID + " " + DeviceType, System.Web.Hosting.HostingEnvironment.MapPath("~\\ValidateLoginServicelog.txt"));
                string pass = Encryption.Encrypt(Pwd);
                DataSet ds = ud.ValidateLoginApp(UserName, pass, DeviceTokenID, DeviceType);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    Users.Add(new User { 
                        ResponseCode = "0", 
                        Response = "Success",
                        Message = "Success", 
                        FirstName = ds.Tables[0].Rows[0]["fname"].ToString(), 
                        LastName = ds.Tables[0].Rows[0]["lname"].ToString(), 
                        DistributorID = ds.Tables[0].Rows[0]["DistributorID"].ToString(), 
                        ClientType = ds.Tables[0].Rows[0]["ClientType"].ToString(),
                        ClientTypeID = ds.Tables[0].Rows[0]["ClientTypeID"].ToString(),
                        UserName = ds.Tables[0].Rows[0]["UserName"].ToString(),
                        Password = ds.Tables[0].Rows[0]["Password"].ToString(),
                        RoleID = ds.Tables[0].Rows[0]["RoleID"].ToString(),
                        MobileNumber = ds.Tables[0].Rows[0]["MobileNumber"].ToString(),
                        EmailID = ds.Tables[0].Rows[0]["EmailID"].ToString(),
                        ActiveFromDtTm = ds.Tables[0].Rows[0]["ActiveFromDtTm"].ToString(),
                        ActiveToDtTm = ds.Tables[0].Rows[0]["ActiveToDtTm"].ToString(),
                        CreatedBy = ds.Tables[0].Rows[0]["CreatedBy"].ToString(),
                        Name = ds.Tables[0].Rows[0]["Name"].ToString(),
                        Currency = ds.Tables[0].Rows[0]["Currency"].ToString(),
                        CurrencyId = ds.Tables[0].Rows[0]["CurrencyId"].ToString(),
                        ID=ds.Tables[0].Rows[0]["ID"].ToString(),
                        TotalTopup = ds.Tables[0].Rows[0]["AccountBalance"].ToString()
                    });
                   
                    return Users;
                }
                else
                {
                    //result = "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \"Failure\" }]}";
                    Users.Add(new User { ResponseCode = "1", Response = "Failure", Message = "Invalid User" });
                    //, FirstName = "", LastName = "", DistributorID = "", ClientType = "" 
                    return Users;
                    //return null;
                }

                //return result;


            }
            catch (Exception ex)
            {
                List<User> Users = new List<User>();

                Users.Add(new User { ResponseCode = "1", Response = "Failure", Message = "Invalid User" });
                return Users;
            }
        }


        public string DataTableToJSON(DataTable Dt)
        {
            string[] StrDc = new string[Dt.Columns.Count];

            string HeadStr = string.Empty;
            for (int i = 0; i < Dt.Columns.Count; i++)
            {

                StrDc[i] = Dt.Columns[i].Caption;
                HeadStr += "\"" + StrDc[i] + "\":\"" + StrDc[i] + i.ToString() + "¾" + "\",";

            }

            HeadStr = HeadStr.Substring(0, HeadStr.Length - 1);

            StringBuilder Sb = new StringBuilder();

            Sb.Append("[");

            for (int i = 0; i < Dt.Rows.Count; i++)
            {

                string TempStr = HeadStr;

                for (int j = 0; j < Dt.Columns.Count; j++)
                {

                    TempStr = TempStr.Replace(Dt.Columns[j] + j.ToString() + "¾", Dt.Rows[i][j].ToString().Trim());
                }
                //Sb.AppendFormat("{{{0}}},",TempStr);

                Sb.Append("{" + TempStr + "},");
            }

            Sb = new StringBuilder(Sb.ToString().Substring(0, Sb.ToString().Length - 1));

            if (Sb.ToString().Length > 0)
                Sb.Append("]");
            //return Sb;
            string str = Sb.ToString();
            return str.Replace(@"\", "");
            //return StripControlChars( str.Replace("'\'",""););

        }
        //To strip control characters:

        //A character that does not represent a printable character but //serves to initiate a particular action.

        public static string StripControlChars(string s)
        {
            return System.Text.RegularExpressions.Regex.Replace(s, @"[^\x20-\x7F]", "");
        }


        public static object DataTableToJSONobject(DataTable table)
        {
            var list = new List<Dictionary<string, object>>();
            //var list = new List<Dictionary<object>>();
            foreach (DataRow row in table.Rows)
            {
                var dict = new Dictionary<string, object>();

                foreach (DataColumn col in table.Columns)
                {
                    dict[col.ColumnName] = (Convert.ToString(row[col]));
                }
                list.Add(dict);
            }
            //System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            return list;
            //return serializer.Serialize(list);
        }
        public List<Tariff> GetTariffService(string LoginID, string DistributorID, string ClientTypeID)
        {
            try
            {
                //string result;
                List<Tariff> Tariff = new List<Tariff>();
                DBTariff dbt = new DBTariff();

                cGeneral.LogSteps(LoginID + " " + DistributorID + " " + ClientTypeID, System.Web.Hosting.HostingEnvironment.MapPath("~\\GetTariffForActivationServicelog.txt"));

                DataSet ds = dbt.GetTariffForActivation(Convert.ToInt32(LoginID), Convert.ToInt32(DistributorID), Convert.ToInt32(ClientTypeID));

                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        Tariff.Add(new Tariff
                        {
                            ResponseCode = "0",
                            Response = "Success",
                            Message = "Success",
                            TariffID = Convert.ToString(dr["ID"]),
                            TariffCode = Convert.ToString(dr["TariffCode"])
                        });
                    }
                }
                else
                {
                    Tariff.Add(new Tariff
                    {
                        ResponseCode = "1",
                        Response = "Failure",
                        Message = "Something Invalid" 
                       
                    });
                }
                   
                    return Tariff;
            }
            catch (Exception ex)
            {
                List<Tariff> Tariff = new List<Tariff>();
                Tariff.Add(new Tariff
                {
                    ResponseCode = "1",
                    Response = "Failure",
                    Message = "Something Invalid"

                });
                return Tariff;
            }
        }

        public string GetSingleDistributorService_IOS(string DistributorID)
        {
            try
            {
                string result;
                DBDistributor dis = new DBDistributor();
                cGeneral.LogSteps(DistributorID, System.Web.Hosting.HostingEnvironment.MapPath("~\\GetSingleDistributorServicelog.txt"));
                DataSet ds = dis.GetSingleDistributor(Convert.ToInt32(DistributorID));

                if (ds.Tables[0].Rows.Count > 0)
                {
                    result = cGeneral.GetJson(ds.Tables[0]);
                    result = "{\"Response\" : [{\"Responsecode\" : \"0\" , \"Response\" : \"Success\"}] , \"Data\" : " + result + "}";
                }
                else
                {
                    result = "{\"Response\" : [{\"Responsecode\" : \"0\" , \"Response\" : \"Failure\" }]}";
                }

                return result;


            }
            catch (Exception ex)
            {
                ServiceData myServiceData = new ServiceData();
                myServiceData.Result = false;
                myServiceData.ErrorMessage = "unforeseen error occured. Please try later.";
                myServiceData.ErrorDetails = ex.ToString();
                throw new FaultException<ServiceData>(myServiceData, ex.ToString());
            }
        }

        public string CheckBalance_IOS(string DistributorID)
        {
            try
            {
                string result;
                DBDistributor dis = new DBDistributor();
                cGeneral.LogSteps(DistributorID, System.Web.Hosting.HostingEnvironment.MapPath("~\\CheckBalancelog.txt"));
                DataSet ds = dis.GetSingleDistributor(Convert.ToInt32(DistributorID));

                if (ds.Tables[0].Rows.Count > 0)
                {
                    result = Convert.ToString(ds.Tables[0].Rows[0]["AccountBalance"]);
                    result = "{\"Response\" : [{\"Responsecode\" : \"0\" , \"Response\" : \"Success\" , \"Balance\" : \"" + result + "\" }] }";
                }
                else
                {
                    result = "{\"Response\" : [{\"Responsecode\" : \"0\" , \"Response\" : \"Failure\" }]}";
                }

                return result;
            }
            catch (Exception ex)
            {
                return "{\"Response\" : [{\"Responsecode\" : \"0\" , \"Response\" : \"Failure\" }]}";
            }
        }

        public string CheckSimActivationService_IOS(string DistributorID, string ClientTypeID, string SimNumber)
        {
            DataSet ds = null;
            try
            {
                string result;
                string TRANSACTIONID;
                cSIM s = new cSIM();
                ds = s.CheckSimActivation(Convert.ToInt32(DistributorID), Convert.ToInt32(ClientTypeID), SimNumber, "Activate");

                if (ds.Tables[0].Rows.Count > 0)
                {
                    result = Convert.ToString(ds.Tables[0].Rows[0][0]);
                    int id = Convert.ToInt32(ds.Tables[1].Rows[0]["TRANSACTIONID"]);
                    TRANSACTIONID = id.ToString("00000");
                    TRANSACTIONID = "AHP" + TRANSACTIONID;


                    result = "{\"Response\" : [{\"Responsecode\" : \"0\" , \"Response\" : \"Success\" , \"SimStatus\" : \"" + result + "\", \"TRANSACTIONID\" : \"" + TRANSACTIONID + "\" }] }";
                }
                else
                {
                    result = "{\"Response\" : [{\"Responsecode\" : \"0\" , \"Response\" : \"Failure\" }]}";
                }

                return result;
            }
            catch (Exception ex)
            {
                return "{\"Response\" : [{\"Responsecode\" : \"0\" , \"Response\" : \"Failure\" }]}";
            }
        }

        public List<SingleTariff> GetTariffDetailService(string LoginID, string DistributorID, string ClientTypeID, string TariffID, string Month, string Action)
        {
            DataSet ds = null;
            try
            {
                //string result;
                cGeneral.LogSteps(LoginID + " " + DistributorID + " " + ClientTypeID, System.Web.Hosting.HostingEnvironment.MapPath("~\\GetTariffForActivationServicelog.txt"));
                DBTariff dbt = new DBTariff();
                List<SingleTariff> SingleTariff = new List<SingleTariff>();

                ds = dbt.GetSingleTariffDetailForActivation(Convert.ToInt32(LoginID), Convert.ToInt32(DistributorID), Convert.ToInt32(ClientTypeID), Convert.ToInt32(TariffID), Convert.ToInt32(Month), Action);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        SingleTariff.Add(new SingleTariff
                        {
                            ResponseCode = "0",
                            Response = "Success",
                            Message = "Success",
                            TariffID = ds.Tables[0].Rows[0]["TariffID"].ToString(),
                            TariffCode = ds.Tables[0].Rows[0]["TariffCode"].ToString(),
                            Description = ds.Tables[0].Rows[0]["Description"].ToString(),
                            Rental = ds.Tables[0].Rows[0]["Rental"].ToString(),
                            ValidityDays = ds.Tables[0].Rows[0]["ValidityDays"].ToString(),
                            isActive = ds.Tables[0].Rows[0]["isActive"].ToString(),
                            IsDefault = ds.Tables[0].Rows[0]["IsDefault"].ToString(),
                            TariffTypeID = ds.Tables[0].Rows[0]["TariffTypeID"].ToString(),
                            TariffType = ds.Tables[0].Rows[0]["TariffType"].ToString(),
                            LycaAmount = ds.Tables[0].Rows[0]["LycaAmount"].ToString(),
                            Months = ds.Tables[0].Rows[0]["Months"].ToString()
                        });
                    }
                }
                else
                {
                    SingleTariff.Add(new SingleTariff
                    {
                        ResponseCode = "1",
                        Response = "Failure",
                        Message = "Something Invalid"

                    });
                }

                return SingleTariff;

            }
            catch (Exception ex)
            {
                List<SingleTariff> SingleTariff = new List<SingleTariff>();
                SingleTariff.Add(new SingleTariff
                {
                    ResponseCode = "1",
                    Response = "Failure",
                    Message = "Something Invalid"

                });
                return SingleTariff;
            }
        }

        public List<Common> ActivateSIMService(string ClientTypeID, string DistributorID, string TariffID, string SimNumber, string TariffAmount, string LoginID, string EmailID, string ZipCode)
        {
            try
            {
                List<Common> Common = new List<Common>();
                //string result;
                Boolean IsBalance = false;
                Boolean IsSimStatus = false;
                string SimStatus = "";
                string TRANSACTIONID;
                double TariffAmt = Convert.ToDouble(TariffAmount);

                cSIM s = new cSIM();
                DBDistributor dis = new DBDistributor();
                cGeneral.LogSteps(DistributorID, System.Web.Hosting.HostingEnvironment.MapPath("~\\CheckBalancelog.txt"));
                DataSet ds = dis.GetSingleDistributor(Convert.ToInt32(DistributorID));

                if (ds.Tables[0].Rows.Count > 0)
                {
                    double BalanceAmnt = Convert.ToDouble(ds.Tables[0].Rows[0]["AccountBalance"]);
                    if (TariffAmt > BalanceAmnt)
                    {
                        IsBalance = false;
                    }
                    else
                    {
                        IsBalance = true;

                       DataSet dsSim = s.CheckSimActivation(Convert.ToInt32(DistributorID), Convert.ToInt32(ClientTypeID), SimNumber, "Activate");

                       if (dsSim.Tables[0].Rows.Count > 0)
                       {
                           SimStatus = Convert.ToString(dsSim.Tables[0].Rows[0][0]);
                           int id = Convert.ToInt32(dsSim.Tables[1].Rows[0]["TRANSACTIONID"]);
                           TRANSACTIONID = id.ToString("00000");
                           TRANSACTIONID = "AHP" + TRANSACTIONID;
                           if (SimStatus == "Ready to Activation")
                           {
                               IsSimStatus = true;

                               string resp = ActivateSim(DistributorID, LoginID, TariffID, EmailID, TRANSACTIONID, ClientTypeID, SimNumber, ZipCode);

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
                                                   SendMail(EmailID, "Sim Activation", ALLOCATED_MSISDN, SimNumber, "");

                                                   DBPayment sp = new DBPayment();
                                                   sp.ChargedAmount = Convert.ToDecimal(TariffAmount);
                                                   sp.PaymentType = 4;
                                                   sp.PayeeID = Convert.ToInt32(DistributorID);
                                                   sp.PaymentFrom = 9;
                                                   sp.ActivationType = 6;
                                                   sp.ActivationStatus = 15;
                                                   sp.ActivationVia = 17;
                                                   sp.ActivationResp = resp;
                                                   sp.ActivationRequest = RequestRes;
                                                   sp.TariffID = Convert.ToInt32(TariffID);
                                                   sp.ALLOCATED_MSISDN = ALLOCATED_MSISDN;
                                                   sp.TransactionId = TRANSACTIONID;
                                                   sp.PaymentMode = "Distributor Activation";
                                                   sp.TransactionStatusId = 24;
                                                   sp.TransactionStatus = "Success";

                                                   //SPayment sp1 = new SPayment();
                                                   //sp1.ChargedAmount = Convert.ToDecimal(TariffAmount);
                                                   //sp1.PaymentType = 4;
                                                   //sp1.PayeeID = Convert.ToInt32(DistributorID);
                                                   //sp1.PaymentFrom = 9;
                                                   //sp1.ActivationType = 6;
                                                   //sp1.ActivationStatus = 15;
                                                   //sp1.ActivationVia = 17;
                                                   //sp1.ActivationResp = resp;
                                                   //sp1.ActivationRequest = RequestRes;
                                                   //sp1.TariffID = Convert.ToInt32(TariffID);
                                                   //sp1.ALLOCATED_MSISDN = ALLOCATED_MSISDN;
                                                   //sp1.TransactionId = TRANSACTIONID;
                                                   //sp1.PaymentMode = "Distributor Activation";
                                                   //sp1.TransactionStatusId = 24;
                                                   //sp1.TransactionStatus = "Success";


                                                   int dist = Convert.ToInt32(DistributorID);
                                                   int loginID = Convert.ToInt32(LoginID);
                                                   string sim = SimNumber;
                                                   string zip = ZipCode;
                                                   string Language = "ENGLISH";
                                                   string ChannelID = "ENK";
                                                   
                                                   int a = sp.UpdateAccountBalance(dist, loginID, sim, zip, ChannelID, Language);

                                                   
                                                   //result = "{\"Response\" : [{\"Responsecode\" : \"0\" , \"Response\" : \"Success\" , \"Message\" : \"SIM activation done successfully with Mobile Number - " + ALLOCATED_MSISDN + "\" }] }";


                                                   Common.Add(new Common
                                                   {
                                                       ResponseCode = "0",
                                                       Response = "Success",
                                                       Message = "SIM activation done successfully with Mobile Number - " + ALLOCATED_MSISDN

                                                   });
                                                   return Common;
                                               }
                                               else
                                               {
                                                   SaveData(resp, 16, TRANSACTIONID,TariffAmount,DistributorID,TariffID,LoginID,SimNumber,ZipCode);
                                                   //result = "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \"Failure\", \"Message\" : \"SIM activation Fail Please Try Again\" }]}";

                                                   Common.Add(new Common
                                                   {
                                                       ResponseCode = "1",
                                                       Response = "Failure",
                                                       Message = "SIM activation Fail Please Try Again"

                                                   });
                                                   return Common;
                                               }
                                           }
                                           else
                                           {
                                               SaveData(resp, 16, TRANSACTIONID, TariffAmount, DistributorID, TariffID, LoginID, SimNumber, ZipCode);
                                               //result = "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \"Failure\", \"Message\" : \"SIM activation Fail Please Try Again\" }]}";

                                               Common.Add(new Common
                                               {
                                                   ResponseCode = "1",
                                                   Response = "Failure",
                                                   Message = "SIM activation Fail Please Try Again"

                                               });
                                               return Common;
                                           }
                                       }
                                       else
                                       {
                                           SaveData(resp, 16, TRANSACTIONID, TariffAmount, DistributorID, TariffID, LoginID, SimNumber, ZipCode);
                                           //result = "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \"Failure\", \"Message\" : \"SIM activation Fail Please Try Again\" }]}";

                                           Common.Add(new Common
                                           {
                                               ResponseCode = "1",
                                               Response = "Failure",
                                               Message = "SIM activation Fail Please Try Again"

                                           });
                                           return Common;
                                       }
                                   }
                                   catch (Exception ex)
                                   {
                                       SaveData(resp, 16, TRANSACTIONID, TariffAmount, DistributorID, TariffID, LoginID, SimNumber, ZipCode);
                                       //result = "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \"Failure\", \"Message\" : \"SIM activation Fail Please Try Again\" }]}";
                                       Common.Add(new Common
                                       {
                                           ResponseCode = "1",
                                           Response = "Failure",
                                           Message = "SIM activation Fail Please Try Again"

                                       });
                                       return Common;
                                   }
                               }
                               else
                               {
                                   SaveData(resp, 16, TRANSACTIONID, TariffAmount, DistributorID, TariffID, LoginID, SimNumber, ZipCode);
                                   //result = "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \"Failure\", \"Message\" : \"SIM activation Fail Please Try Again\" }]}";
                                   Common.Add(new Common
                                   {
                                       ResponseCode = "1",
                                       Response = "Failure",
                                       Message = "SIM activation Fail Please Try Again"

                                   });
                                   return Common;
                               }
                           }
                           else
                           {
                               IsSimStatus = false;
                           }

                       }
                       
                    }

                    if (IsBalance == false)
                    {
                        //result = "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \"Failure\", \"Message\" : \"Your Account Balance is Low. Please Recharge Your Balance\" }]}";
                        Common.Add(new Common
                        {
                            ResponseCode = "1",
                            Response = "Failure",
                            Message = "Your Account Balance is Low. Please Recharge Your Balance"

                        });
                        return Common;
                    }
                    else
                    {
                        if (IsSimStatus == false)
                        {
                            Common.Add(new Common
                            {
                                ResponseCode = "1",
                                Response = "Failure",
                                Message = SimStatus

                            });
                            return Common;
                            //result = "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \"Failure\", \"Message\" : \"" + SimStatus + "\" }]}";
                        }
                        else
                        {
                            Common.Add(new Common
                            {
                                ResponseCode = "1",
                                Response = "Failure",
                                Message = SimStatus

                            });
                            return Common;
                            //result = "{\"Response\" : [{\"Responsecode\" : \"0\" , \"Response\" : \"Success\", \"Message\" : \"" + SimStatus + "\" }]}";
                        }
                    }
                }
                else
                {
                    //result = "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \"Failure\" }]}";
                    Common.Add(new Common
                    {
                        ResponseCode = "1",
                        Response = "Failure",
                        Message = "Failure"

                    });
                    return Common;
                }

                //return Common;
            }
            catch (Exception ex)
            {
                List<Common> Common = new List<Common>();
                Common.Add(new Common
                {
                    ResponseCode = "1",
                    Response = "Failure",
                    Message = "Failure"

                });
                return Common;
            }
        }

        public List<Common> PortInService(string ClientTypeID, string DistributorID, string TariffID, string SimNumber, string TariffAmount, string LoginID, string EmailID, string ZipCode, string Pin, string Account, string PhoneToPort, string Action)
        {
            try
            {
                List<Common> Common = new List<Common>();
                //string result;
                Boolean IsBalance = false;
                Boolean IsSimStatus = false;
                string SimStatus = "";
                string TRANSACTIONID;
                double TariffAmt = Convert.ToDouble(TariffAmount);

                cSIM s = new cSIM();
                DBDistributor dis = new DBDistributor();
                cGeneral.LogSteps(DistributorID, System.Web.Hosting.HostingEnvironment.MapPath("~\\CheckBalancelog.txt"));
                DataSet ds = dis.GetSingleDistributor(Convert.ToInt32(DistributorID));

                if (ds.Tables[0].Rows.Count > 0)
                {
                    double BalanceAmnt = Convert.ToDouble(ds.Tables[0].Rows[0]["AccountBalance"]);
                    if (TariffAmt > BalanceAmnt)
                    {
                        IsBalance = false;
                    }
                    else
                    {
                        IsBalance = true;

                        DataSet dsSim = s.CheckSimActivation(Convert.ToInt32(DistributorID), Convert.ToInt32(ClientTypeID), SimNumber, "Activate");

                        if (dsSim.Tables[0].Rows.Count > 0)
                        {
                            SimStatus = Convert.ToString(dsSim.Tables[0].Rows[0][0]);
                            int id = Convert.ToInt32(dsSim.Tables[1].Rows[0]["TRANSACTIONID"]);
                            TRANSACTIONID = id.ToString("00000");
                            TRANSACTIONID = "ENK" + TRANSACTIONID;
                            if (SimStatus == "Ready to Activation")
                            {
                                IsSimStatus = true;

                                string resp = PortInSim(DistributorID, LoginID, TariffID, EmailID, TRANSACTIONID, ClientTypeID, SimNumber, ZipCode, Pin,  Account,  PhoneToPort, Action);

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
                                                    SendMail(EmailID, "Sim Activation", ALLOCATED_MSISDN, SimNumber, "");

                                                    DBPayment sp = new DBPayment();
                                                    sp.ChargedAmount = Convert.ToDecimal(TariffAmount);
                                                    sp.PaymentType = 4;
                                                    sp.PayeeID = Convert.ToInt32(DistributorID);
                                                    sp.PaymentFrom = 9;
                                                    sp.ActivationType = 6;
                                                    sp.ActivationStatus = 15;
                                                    sp.ActivationVia = 17;
                                                    sp.ActivationResp = resp;
                                                    sp.ActivationRequest = RequestRes;
                                                    sp.TariffID = Convert.ToInt32(TariffID);
                                                    sp.ALLOCATED_MSISDN = ALLOCATED_MSISDN;
                                                    sp.TransactionId = TRANSACTIONID;
                                                    sp.PaymentMode = "Distributor PortIn";
                                                    sp.TransactionStatusId = 24;
                                                    sp.TransactionStatus = "Success";

                                                    int dist = Convert.ToInt32(DistributorID);
                                                    int loginID = Convert.ToInt32(LoginID);
                                                    string sim = SimNumber;
                                                    string zip = ZipCode;
                                                    string Language = "ENGLISH";
                                                    string ChannelID = "ENK";

                                                    int a = sp.UpdateAccountBalance(dist, loginID, sim, zip, ChannelID, Language);


                                                    //result = "{\"Response\" : [{\"Responsecode\" : \"0\" , \"Response\" : \"Success\" , \"Message\" : \"SIM activation done successfully with Mobile Number - " + ALLOCATED_MSISDN + "\" }] }";


                                                    Common.Add(new Common
                                                    {
                                                        ResponseCode = "0",
                                                        Response = "Success",
                                                        Message = "PORTIN Request submitted successfully with Mobile Number - " + ALLOCATED_MSISDN

                                                    });
                                                    return Common;
                                                }
                                                else
                                                {
                                                    SaveData(resp, 16, TRANSACTIONID, TariffAmount, DistributorID, TariffID, LoginID, SimNumber, ZipCode);
                                                    //result = "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \"Failure\", \"Message\" : \"SIM activation Fail Please Try Again\" }]}";

                                                    Common.Add(new Common
                                                    {
                                                        ResponseCode = "1",
                                                        Response = "Failure",
                                                        Message = "PORTIN Request Fail Please Try Again"

                                                    });
                                                    return Common;
                                                }
                                            }
                                            else
                                            {
                                                SaveData(resp, 16, TRANSACTIONID, TariffAmount, DistributorID, TariffID, LoginID, SimNumber, ZipCode);
                                                //result = "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \"Failure\", \"Message\" : \"SIM activation Fail Please Try Again\" }]}";

                                                Common.Add(new Common
                                                {
                                                    ResponseCode = "1",
                                                    Response = "Failure",
                                                    Message = "PORTIN Request Fail Please Try Again"

                                                });
                                                return Common;
                                            }
                                        }
                                        else
                                        {
                                            SaveData(resp, 16, TRANSACTIONID, TariffAmount, DistributorID, TariffID, LoginID, SimNumber, ZipCode);
                                            //result = "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \"Failure\", \"Message\" : \"SIM activation Fail Please Try Again\" }]}";

                                            Common.Add(new Common
                                            {
                                                ResponseCode = "1",
                                                Response = "Failure",
                                                Message = "PORTIN Request Fail Please Try Again"

                                            });
                                            return Common;
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        SaveData(resp, 16, TRANSACTIONID, TariffAmount, DistributorID, TariffID, LoginID, SimNumber, ZipCode);
                                        //result = "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \"Failure\", \"Message\" : \"SIM activation Fail Please Try Again\" }]}";
                                        Common.Add(new Common
                                        {
                                            ResponseCode = "1",
                                            Response = "Failure",
                                            Message = "PORTIN Request Fail Please Try Again"

                                        });
                                        return Common;
                                    }
                                }
                                else
                                {
                                    SaveData(resp, 16, TRANSACTIONID, TariffAmount, DistributorID, TariffID, LoginID, SimNumber, ZipCode);
                                    //result = "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \"Failure\", \"Message\" : \"SIM activation Fail Please Try Again\" }]}";
                                    Common.Add(new Common
                                    {
                                        ResponseCode = "1",
                                        Response = "Failure",
                                        Message = "PORTIN Request Fail Please Try Again"

                                    });
                                    return Common;
                                }
                            }
                            else
                            {
                                IsSimStatus = false;
                            }

                        }

                    }

                    if (IsBalance == false)
                    {
                        //result = "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \"Failure\", \"Message\" : \"Your Account Balance is Low. Please Recharge Your Balance\" }]}";
                        Common.Add(new Common
                        {
                            ResponseCode = "1",
                            Response = "Failure",
                            Message = "Your Account Balance is Low. Please Recharge Your Balance"

                        });
                        return Common;
                    }
                    else
                    {
                        if (IsSimStatus == false)
                        {
                            Common.Add(new Common
                            {
                                ResponseCode = "1",
                                Response = "Failure",
                                Message = SimStatus

                            });
                            return Common;
                            //result = "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \"Failure\", \"Message\" : \"" + SimStatus + "\" }]}";
                        }
                        else
                        {
                            Common.Add(new Common
                            {
                                ResponseCode = "1",
                                Response = "Failure",
                                Message = SimStatus

                            });
                            return Common;
                            //result = "{\"Response\" : [{\"Responsecode\" : \"0\" , \"Response\" : \"Success\", \"Message\" : \"" + SimStatus + "\" }]}";
                        }
                    }
                }
                else
                {
                    //result = "{\"Response\" : [{\"Responsecode\" : \"1\" , \"Response\" : \"Failure\" }]}";
                    Common.Add(new Common
                    {
                        ResponseCode = "1",
                        Response = "Failure",
                        Message = "Failure"

                    });
                    return Common;
                }

                //return Common;
            }
            catch (Exception ex)
            {
                List<Common> Common = new List<Common>();
                Common.Add(new Common
                {
                    ResponseCode = "1",
                    Response = "Failure",
                    Message = "Failure"

                });
                return Common;
            }
        }

        #endregion

        public string ActivateSim(string DistributorID, string LoginID, string TariffID, string email, string TransactionID, string ClientTypeID,string SimNumber, string ZipCode,string VoucherPIN="", string TopUP="", string Action="Activate")
        {
            string response = "";
            string BundleID = "";
            string BundleCode = "";
            string BundleType = "";
            string BundleAmount = "";
            
            try
            {
                string months = "1";
                if (TariffID!="0")
                {
                    DBTariff dbt = new DBTariff();
                    DataSet dsTariff = dbt.GetSingleTariffDetailForActivation(Convert.ToInt32(LoginID), Convert.ToInt32(DistributorID), Convert.ToInt32(ClientTypeID), Convert.ToInt32(TariffID), Convert.ToInt32(months), Action);
                    if (dsTariff.Tables[0].Rows.Count > 0)
                    {
                        months = Convert.ToString(dsTariff.Tables[0].Rows[0]["Months"]);
                    }
                    BundleID = Convert.ToString(dsTariff.Tables[0].Rows[0]["TariffTypeID"]);
                    BundleCode = Convert.ToString(dsTariff.Tables[0].Rows[0]["TariffCode"]);
                    BundleType = Convert.ToString(dsTariff.Tables[0].Rows[0]["TariffType"]);;
                    BundleAmount = Convert.ToString(dsTariff.Tables[0].Rows[0]["LycaAmount"]);
                }    
                string EmailID = email;

                string X = "";

                string activation = "1";
                string SIMCARD = SimNumber;
                string ZIPCode = ZipCode;
                string Language = "ENGLISH";
                string ChannelID = "ENKINC";


                if (BundleType.ToLower().Equals("national"))
                {
                    X = "<ENVELOPE><HEADER><TRANSACTION_ID>" + TransactionID + "</TRANSACTION_ID><ENTITY>" + ChannelID + "</ENTITY><CHANNEL_REFERENCE>" + ChannelID + "</CHANNEL_REFERENCE></HEADER><BODY><ACTIVATE_USIM_PORTIN_BUNDLE_REQUEST><DETAILS><ICC_ID>" + SIMCARD + "</ICC_ID><ZIP_CODE>" + ZIPCode + "</ZIP_CODE><PREFERRED_LANGUAGE>" + Language + "</PREFERRED_LANGUAGE><P_MSISDN></P_MSISDN><ACCOUNT_NUMBER></ACCOUNT_NUMBER><PASSWORD_PIN></PASSWORD_PIN><NO_OF_MONTHS>" + months + "</NO_OF_MONTHS><NATIONAL_BUNDLE_CODE>" + BundleCode + "</NATIONAL_BUNDLE_CODE><NATIONAL_BUNDLE_AMOUNT>" + BundleAmount + "</NATIONAL_BUNDLE_AMOUNT><INTERNATIONAL_BUNDLE_CODE></INTERNATIONAL_BUNDLE_CODE><INTERNATIONAL_BUNDLE_AMOUNT></INTERNATIONAL_BUNDLE_AMOUNT><TOPUP_AMOUNT>"+TopUP+"</TOPUP_AMOUNT><TOPUP_CARD_ID></TOPUP_CARD_ID><VOUCHER_PIN>"+VoucherPIN+"</VOUCHER_PIN><CHANNEL_ID>" + ChannelID + "</CHANNEL_ID><EMAIL_ID>" + EmailID + "</EMAIL_ID></DETAILS></ACTIVATE_USIM_PORTIN_BUNDLE_REQUEST></BODY></ENVELOPE>";
                }
                else
                {
                    X = "<ENVELOPE><HEADER><TRANSACTION_ID>" + TransactionID + "</TRANSACTION_ID><ENTITY>" + ChannelID + "</ENTITY><CHANNEL_REFERENCE>" + ChannelID + "</CHANNEL_REFERENCE></HEADER><BODY><ACTIVATE_USIM_PORTIN_BUNDLE_REQUEST><DETAILS><ICC_ID>" + SIMCARD + "</ICC_ID><ZIP_CODE>" + ZIPCode + "</ZIP_CODE><PREFERRED_LANGUAGE>" + Language + "</PREFERRED_LANGUAGE><P_MSISDN></P_MSISDN><ACCOUNT_NUMBER></ACCOUNT_NUMBER><PASSWORD_PIN></PASSWORD_PIN><NO_OF_MONTHS>" + months + "</NO_OF_MONTHS><NATIONAL_BUNDLE_CODE>" + BundleCode + "</NATIONAL_BUNDLE_CODE><NATIONAL_BUNDLE_AMOUNT>" + BundleAmount + "</NATIONAL_BUNDLE_AMOUNT><INTERNATIONAL_BUNDLE_CODE></INTERNATIONAL_BUNDLE_CODE><INTERNATIONAL_BUNDLE_AMOUNT></INTERNATIONAL_BUNDLE_AMOUNT><TOPUP_AMOUNT>"+TopUP+"</TOPUP_AMOUNT><TOPUP_CARD_ID></TOPUP_CARD_ID><VOUCHER_PIN>"+VoucherPIN+"</VOUCHER_PIN><CHANNEL_ID>" + ChannelID + "</CHANNEL_ID><EMAIL_ID>" + EmailID + "</EMAIL_ID></DETAILS></ACTIVATE_USIM_PORTIN_BUNDLE_REQUEST></BODY></ENVELOPE>";
                }
               
                RequestRes = X;
                string resp = Activation(X);
                response = resp;
                return response;


            }
            catch (Exception ex)
            {
                return response;
                //ShowPopUpMsg(ex.Message);
            }
        }

        public string PortInSim(string DistributorID, string LoginID, string TariffID, string email, string TransactionID, string ClientTypeID, string SimNumber, string ZipCode,string Pin, string Account, string PhoneToPort, string Action)
        {
            string response = "";

            string months = "1";
            try
            {
                DBTariff dbt = new DBTariff();

                DataSet dsTariff = dbt.GetSingleTariffDetailForActivation(Convert.ToInt32(LoginID), Convert.ToInt32(DistributorID), Convert.ToInt32(ClientTypeID), Convert.ToInt32(TariffID), Convert.ToInt32(months), Action);
                if (dsTariff.Tables[0].Rows.Count > 0)
                {
                    months = Convert.ToString(dsTariff.Tables[0].Rows[0]["Months"]);
                }
                string BundleID = Convert.ToString(dsTariff.Tables[0].Rows[0]["TariffTypeID"]);
                string BundleCode = Convert.ToString(dsTariff.Tables[0].Rows[0]["TariffCode"]);
                string BundleType = Convert.ToString(dsTariff.Tables[0].Rows[0]["TariffType"]); ;
                string BundleAmount = Convert.ToString(dsTariff.Tables[0].Rows[0]["LycaAmount"]);

                string PHONETOPORT = PhoneToPort;
                string ACCOUNT = Account;
                string PIN = Pin;

                string NATIONAL_BUNDLE_CODE = "";
                string NATIONAL_BUNDLE_AMOUNT = "";

                string INTERNATIONAL_BUNDLE_CODE = "";
                string INTERNATIONAL_BUNDLE_AMOUNT = "";

                string EmailID = email;

                string TOPUP_AMOUNT = "";
                string TOPUP_CARD_ID = "";
                string VOUCHER_PIN = "";

                

                string activation = "1";
                string SIMCARD = SimNumber;
                string ZIPCode = ZipCode;
                string Language = "ENGLISH";
                string ChannelID = "ENKINC";

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


                string X = "<ENVELOPE><HEADER><TRANSACTION_ID>" + TransactionID + "</TRANSACTION_ID><ENTITY>" + ChannelID + "</ENTITY><CHANNEL_REFERENCE>" + ChannelID + "</CHANNEL_REFERENCE></HEADER><BODY><ACTIVATE_USIM_PORTIN_BUNDLE_REQUEST><DETAILS><ICC_ID>" + SIMCARD + "</ICC_ID><ZIP_CODE>" + ZIPCode + "</ZIP_CODE><PREFERRED_LANGUAGE>" + Language + "</PREFERRED_LANGUAGE><P_MSISDN>" + PHONETOPORT + "</P_MSISDN><ACCOUNT_NUMBER>" + ACCOUNT + "</ACCOUNT_NUMBER><PASSWORD_PIN>" + PIN + "</PASSWORD_PIN><NO_OF_MONTHS>" + months + "</NO_OF_MONTHS><NATIONAL_BUNDLE_CODE>" + NATIONAL_BUNDLE_CODE + "</NATIONAL_BUNDLE_CODE><NATIONAL_BUNDLE_AMOUNT>" + NATIONAL_BUNDLE_AMOUNT + "</NATIONAL_BUNDLE_AMOUNT><INTERNATIONAL_BUNDLE_CODE></INTERNATIONAL_BUNDLE_CODE><INTERNATIONAL_BUNDLE_AMOUNT></INTERNATIONAL_BUNDLE_AMOUNT><TOPUP_AMOUNT>" + TOPUP_AMOUNT + "</TOPUP_AMOUNT><TOPUP_CARD_ID>" + TOPUP_CARD_ID + "</TOPUP_CARD_ID><VOUCHER_PIN>" + VOUCHER_PIN + "</VOUCHER_PIN><CHANNEL_ID>" + ChannelID + "</CHANNEL_ID><EMAIL_ID>" + EmailID + "</EMAIL_ID></DETAILS></ACTIVATE_USIM_PORTIN_BUNDLE_REQUEST></BODY></ENVELOPE>";

                RequestRes = X;
                
                string resp = Activation(X);
                response = resp;
                
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

                strResponse = SendRequest("http://83.137.2.1:5443", X);
               // strResponse = SendRequest("http://192.30.216.110:2244", X);//"<create-directory-number version=\"1\"> <authentication> <username>admin.puneet</username> <password>stay@9229</password> </authentication> <directory-number>" + DIDNumber + "</directory-number> <directory-number-vendor>toclionly</directory-number-vendor> </create-directory-number>"

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

        public void SendMail(string SendTo, string Subject, string UserName, string UserID, string pass)
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

                sb.Append("<p>Sim Number " + UserID + "Activated successfully on Mobile Number " + UserName + "<p>");


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
        public void SaveDataApp(string resp, int status, string TransactionID, string TariffAmount, string DistributorID, string TariffID, string LoginID, string SimNumber, string ZipCode, int PaymentID, int IsWalet)
        {
            DBPayment sp = new DBPayment();
            sp.ChargedAmount = Convert.ToDecimal(TariffAmount);
            sp.PaymentType = 4;
            sp.PayeeID = Convert.ToInt32(DistributorID);
            sp.PaymentFrom = 9;
            sp.ActivationType = 6;
            sp.ActivationStatus = status;
            sp.ActivationVia = 17;
            sp.ActivationResp = resp;
            sp.ActivationRequest = RequestRes;
            sp.TariffID = Convert.ToInt32(TariffID);
            sp.ALLOCATED_MSISDN = "";
            sp.TransactionId = TransactionID;
            sp.PaymentMode = "App Distributor Activation";
            sp.TransactionStatusId = 25;
            sp.TransactionStatus = "Fail";
            sp.PaymentId = PaymentID;
            sp.IsWalet = IsWalet;

            int dist = Convert.ToInt32(DistributorID);
            int loginID = Convert.ToInt32(LoginID);
            string sim = SimNumber;
            string zip = ZipCode;
            string Language = "ENGLISH";
            string ChannelID = "ENKINC";

            try
            {
                int a = sp.UpdateAccountBalanceApp(dist, loginID, sim, zip, ChannelID, Language);
                //int s = svc.UpdateAccountBalanceService(dist, loginID, sim, zip, Language, ChannelID, sp);
            }
            catch (Exception ex)
            {

            }

        }
        public void SaveDataAppPortin(string resp, int status, string TransactionID, string TariffAmount, string DistributorID, string TariffID, string LoginID, string SimNumber, string ZipCode, int PaymentID, int IsWalet)
        {
            DBPayment sp = new DBPayment();
            sp.ChargedAmount = Convert.ToDecimal(TariffAmount);
            sp.PaymentType = 5;
            sp.PayeeID = Convert.ToInt32(DistributorID);
            sp.PaymentFrom = 9;
            sp.ActivationType = 7;
            sp.ActivationStatus = status;
            sp.ActivationVia = 17;
            sp.ActivationResp = resp;
            sp.ActivationRequest = RequestRes;
            sp.TariffID = Convert.ToInt32(TariffID);
            sp.ALLOCATED_MSISDN = "";
            sp.TransactionId = TransactionID;
            sp.PaymentMode = "App Distributor PortIn";
            sp.TransactionStatusId = 25;
            sp.TransactionStatus = "Fail";
            sp.PaymentId = PaymentID;
            sp.IsWalet = IsWalet;

            int dist = Convert.ToInt32(DistributorID);
            int loginID = Convert.ToInt32(LoginID);
            string sim = SimNumber;
            string zip = ZipCode;
            string Language = "ENGLISH";
            string ChannelID = "ENKINC";

            try
            {
                int a = sp.UpdateAccountBalanceApp(dist, loginID, sim, zip, ChannelID, Language);
                //int s = svc.UpdateAccountBalanceService(dist, loginID, sim, zip, Language, ChannelID, sp);
            }
            catch (Exception ex)
            {

            }

        }

        public void SaveData(string resp, int status, string TransactionID, string TariffAmount, string DistributorID,string TariffID,string LoginID,string SimNumber,string ZipCode)
        {
            DBPayment sp = new DBPayment();
            sp.ChargedAmount = Convert.ToDecimal(TariffAmount);
            sp.PaymentType = 4;
            sp.PayeeID = Convert.ToInt32(DistributorID);
            sp.PaymentFrom = 9;
            sp.ActivationType = 6;
            sp.ActivationStatus = status;
            sp.ActivationVia = 17;
            sp.ActivationResp = resp;
            sp.ActivationRequest = RequestRes;
            sp.TariffID = Convert.ToInt32(TariffID);
            sp.ALLOCATED_MSISDN = "";
            sp.TransactionId = TransactionID;
            sp.PaymentMode = "Distributor Activation";
            sp.TransactionStatusId = 25;
            sp.TransactionStatus = "Fail";
            int dist = Convert.ToInt32(DistributorID);
            int loginID = Convert.ToInt32(LoginID);
            string sim = SimNumber;
            string zip = ZipCode;
            string Language = "ENGLISH";
            string ChannelID = "ENKINC";

            try
            {
                int a = sp.UpdateAccountBalance(dist, loginID, sim, zip, ChannelID, Language);
                //int s = svc.UpdateAccountBalanceService(dist, loginID, sim, zip, Language, ChannelID, sp);
            }
            catch (Exception ex)
            {
               
            }
           
        }

        public List<Common> SimReplacementSerive(string CurrentSimNumber, string CurrentMobileNumber, string NewSimNumber, string DistributorID, string LoginID)
        {
            try
            {
                List<Common> Common = new List<Common>();
                    cSIM s = new cSIM();
                    s.MSISDNNo = "No";
                    s.SIMNo = CurrentSimNumber;
                    s.ClientID = Convert.ToInt32(DistributorID);

                    DataSet ds = s.GetInventoryForSIMReplacement();
                    
                    if (ds != null)
                    {
                        if (ds.Tables.Count > 0)
                        {
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                if (ds.Tables[0].Rows[0]["Action"].ToString() == "SIM is not activated.")
                                {
                                    Common.Add(new Common
                                    {
                                        ResponseCode = "1",
                                        Response = "Failure",
                                        Message = "SIM replacement can not be done as SIM is not activated."

                                    });
                                    return Common;
                                }
                                else
                                {
                                    s.ClientID = Convert.ToInt32(DistributorID);
                                    s.MSISDN_SIM_ID = Convert.ToInt64(ds.Tables[0].Rows[0]["MSISDN_SIM_ID"]);
                                    s.SIMNo = Convert.ToString(NewSimNumber);
                                    s.UserID = Convert.ToInt32(LoginID);
                                    s.MSISDNNo = "NewSIMNumber";
                                    DataSet dsForNewSim = s.GetInventoryForSIMReplacement();

                                    if (dsForNewSim != null)
                                    {
                                        if (dsForNewSim.Tables.Count > 0)
                                        {

                                            if (dsForNewSim.Tables[0].Rows.Count > 0)
                                            {

                                                int retval = s.SIMReplacement();
                                                if (retval > 0)
                                                {
                                                    Common.Add(new Common
                                                    {
                                                        ResponseCode = "0",
                                                        Response = "Success",
                                                        Message = "SIM Replacement successfully"

                                                    });
                                                    return Common;
                                                }
                                                else
                                                {
                                                    Common.Add(new Common
                                                    {
                                                        ResponseCode = "1",
                                                        Response = "Failure",
                                                        Message = "Something Invalid. Please check"

                                                    });
                                                    return Common;

                                                }

                                            }
                                            else
                                            {
                                                Common.Add(new Common
                                                {
                                                    ResponseCode = "1",
                                                    Response = "Failure",
                                                    Message = "New SIM Number Does Not Exist"

                                                });
                                                return Common;
                                            }
                                        }
                                    }
                                }
                                return Common;

                            }
                            else
                            {
                                Common.Add(new Common
                                {
                                    ResponseCode = "1",
                                    Response = "Failure",
                                    Message = "SIM or Mobile does not exist."

                                });
                                return Common;
                            }
                        }

                        return Common;
                    }

                    return Common;
                
            }
            catch (Exception ex)
            {
                List<Common> Common = new List<Common>();

                Common.Add(new Common
                {
                    ResponseCode = "1",
                    Response = "Failure",
                    Message = "Failure"

                });
                return Common;
            }
        }

        public List<Topup> TopupService(string DistributorID, string LoginID, string Amount, string Status, string TxnID, string TxnDate)
        {
            try
            {
                List<Topup> Topup = new List<Topup>();
                DBTariff dbt = new DBTariff();
                DataSet ds = dbt.Topup(Convert.ToInt32(DistributorID), Convert.ToInt64(LoginID), Convert.ToDecimal(Amount), Status, TxnID, TxnDate);

                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            Topup.Add(new Topup
                            {
                                ResponseCode = "0",
                                Response = "Success",
                                Message = "Topup successfully",
                                TotalTopup = ds.Tables[0].Rows[0]["AccountBalance"].ToString()

                            });
                            return Topup;
                            //if (ds.Tables[0].Rows[0]["Action"].ToString() == "SIM is not activated.")
                            //{

                            //}
                        }
                        else
                        {
                            Topup.Add(new Topup
                            {
                                ResponseCode = "1",
                                Response = "Failure",
                                Message = "Something wrong!! Please try again.",
                                TotalTopup = "0"

                            });
                            return Topup;
                        }
                    }
                    else
                    {
                        Topup.Add(new Topup
                        {
                            ResponseCode = "1",
                            Response = "Failure",
                            Message = "Something wrong!! Please try again.",
                            TotalTopup = "0"

                        });
                        return Topup;
                    }
                }
                else
                {
                    Topup.Add(new Topup
                    {
                        ResponseCode = "1",
                        Response = "Failure",
                        Message = "Something wrong!! Please try again.",
                        TotalTopup = "0"

                    });
                    return Topup;
                }

                //return Topup;
            }
            catch (Exception ex)
            {
                List<Topup> Topup = new List<Topup>();
                Topup.Add(new Topup
                {
                    ResponseCode = "1",
                    Response = "Failure",
                    Message = "Something wrong!! Please try again.",
                    TotalTopup = "0"

                });
                return Topup;
            }
           
        }
    }
}
