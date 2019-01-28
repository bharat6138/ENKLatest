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
using System.Net.Mail;
using System.IO;

namespace ENK
{
    public partial class Login : System.Web.UI.Page
    {
        Service1Client svc = new Service1Client();
        string dir = "~/Image";

        protected void Page_Load(object sender, EventArgs e)
        {
            //DecryptPwd("hrI9T6MkwBUfYWc7o6NUkmJbUbKnHfyUpUnv8CqIz6E=");
            txtUserName.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" + txtPassword.UniqueID + "').focus();return false;}} else {return true}; ");
            txtPassword.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" + Button1.UniqueID + "').click();return false;}} else {return true}; ");

            //Response.Cookies.Clear();
            //ExpireAllCookies();
            int ImgFlag = 1;
            //Session["ImgFlag"] = Convert.ToInt32(Request.QueryString["ImgFlag"]);
            getImage1FromDB(ImgFlag);

        }
        private void DecryptPwd(string Pwd)
        {
            string pass = Encryption.Decrypt(Pwd);
            lblwarning.Style.Add("display", "block");
            // lblwarning.Attributes.Add("display", "block");
            lblwarning.Text = "Password is: " + pass;
        }

        private void ExpireAllCookies()
        {
            if (HttpContext.Current != null)
            {
                int cookieCount = HttpContext.Current.Request.Cookies.Count;
                for (var i = 0; i < cookieCount; i++)
                {
                    var cookie = HttpContext.Current.Request.Cookies[i];
                    if (cookie != null)
                    {
                        var cookieName = cookie.Name;
                        var expiredCookie = new HttpCookie(cookieName, "") { Expires = DateTime.Now.AddDays(-1) };
                        HttpContext.Current.Response.Cookies.Add(expiredCookie); // overwrite it
                    }
                }

                // clear cookies server side
                HttpContext.Current.Request.Cookies.Clear();
            }
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = new DataSet();
                string uname = txtUserName.Text.Trim();
                string pass = txtPassword.Text.Trim();
                // string s = Encryption.Decrypt("VW5p6xW+0Dw/yhkKFKasvli/Vs/aKciCdkBF7Tkmdq4=");
                pass = Encryption.Encrypt(pass);

                ds = svc.ValidateLoginService(uname, pass);

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {

                        Session["LoginId"] = Convert.ToString(ds.Tables[0].Rows[0]["ID"]);
                        Session["UserName"] = Convert.ToString(ds.Tables[0].Rows[0]["UserName"]);
                        Session["RoleID"] = Convert.ToString(ds.Tables[0].Rows[0]["RoleID"]);
                        Session["DistributorID"] = Convert.ToString(ds.Tables[0].Rows[0]["DistributorID"]);
                        Session["Pass"] = Convert.ToString(ds.Tables[0].Rows[0]["Password"]);
                        Session["Fname"] = Convert.ToString(ds.Tables[0].Rows[0]["Fname"]);
                        Session["Lname"] = Convert.ToString(ds.Tables[0].Rows[0]["Lname"]);
                        Session["EmailID"] = Convert.ToString(ds.Tables[0].Rows[0]["EmailID"]);
                        Session["MobileNuber"] = Convert.ToString(ds.Tables[0].Rows[0]["MobileNumber"]);
                        Session["IsActive"] = Convert.ToString(ds.Tables[0].Rows[0]["IsActive"]);
                        Session["ActiveFrom"] = Convert.ToString(ds.Tables[0].Rows[0]["ActiveFromDtTm"]);
                        Session["ActiveTo"] = Convert.ToString(ds.Tables[0].Rows[0]["ActiveToDtTm"]);
                        Session["ClientTypeID"] = Convert.ToString(ds.Tables[0].Rows[0]["ClientType"]);
                        Session["ClientType"] = Convert.ToString(ds.Tables[0].Rows[0]["Name"]);
                        Session["CurrencyName"] = Convert.ToString(ds.Tables[0].Rows[0]["Currency"]);
                        Session["CurrencyId"] = Convert.ToString(ds.Tables[0].Rows[0]["CurrencyId"]);
                        Session["TariffGroupID"] = Convert.ToString(ds.Tables[0].Rows[0]["TariffGroupID"]);
                        Session["DistributorName"] = Convert.ToString(ds.Tables[0].Rows[0]["DistributorName"]);
                        // add by akash starts
                        Session["AccountBalance"] = Convert.ToString(ds.Tables[0].Rows[0]["AccountBalance"]);
                        // add by akash ends
                        string ishold = Convert.ToString(ds.Tables[0].Rows[0]["isHold"]);
                        GetClientIPAddress();
                        if (ishold == "True")
                        {
                            DataSet dsH = new DataSet();
                            string Msg = "";
                            dsH = svc.GetHoldReason(Convert.ToInt64(Session["DistributorID"]));
                            if (dsH.Tables[0].Rows.Count > 0)
                            {
                                Msg = Convert.ToString(dsH.Tables[0].Rows[0]["HoldReason"]);
                                if (Msg != "")
                                {
                                    lblwarning.Style.Add("display", "block");
                                    btnReply.Style.Add("display", "block");
                                    lblwarning.Text = "Your account is on Hold, " + "<br/>" + " Reason : " + Msg + "<br/>" + "Please contact Administrator." + "<br/>" + "Customer Care Number :" + " " + ConfigurationManager.AppSettings["COMPANY_PHONE"] + "<br/>" + " " + "Email :" + " " + ConfigurationManager.AppSettings["COMPANY_EMAIL"];
                                    return;
                                }

                                else
                                {
                                    lblwarning.Style.Add("display", "block");
                                    btnReply.Style.Add("display", "block");
                                    lblwarning.Text = "Your account is on Hold, Please contact Administrator." + "<br/>" + "Customer Care Number :" + " " + ConfigurationManager.AppSettings["COMPANY_PHONE"] + "<br/>" + " " + "Email :" + " " + ConfigurationManager.AppSettings["COMPANY_EMAIL"];
                                    return;
                                }
                            }

                        }
                        Response.Redirect("Dashboard.aspx");
                    }
                    else
                    {
                        // lblwarning.Visible = true;
                        lblwarning.Style.Add("display", "block");
                        // lblwarning.Attributes.Add("display", "block");
                        lblwarning.Text = "Invalid UserID and Password";
                    }
                }
            }
            catch (Exception ex)
            {
                lblwarning.Style.Add("display", "block");
                //lblwarning.Attributes.Add("display", "block");
                lblwarning.Text = "Invalid UserID and Password";
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
                sl.Location = "Portal";
                sl.IpDetail = BrowserDetail;
                sl.LoginID = Convert.ToInt32(Session["LoginID"]);
                sl.LoginTime = DateTime.Now;
                svc.InsertLoginHistoryService(sl);
            }
            catch (Exception ex)
            {

            }
            // return context.Request.ServerVariables["REMOTE_ADDR"];
        }

        public static bool isMobileBrowser()
        {
            //GETS THE CURRENT USER CONTEXT
            HttpContext context = HttpContext.Current;

            //FIRST TRY BUILT IN ASP.NT CHECK
            if (context.Request.Browser.IsMobileDevice)
            {
                return true;
            }
            //THEN TRY CHECKING FOR THE HTTP_X_WAP_PROFILE HEADER
            if (context.Request.ServerVariables["HTTP_X_WAP_PROFILE"] != null)
            {
                return true;
            }
            //THEN TRY CHECKING THAT HTTP_ACCEPT EXISTS AND CONTAINS WAP
            if (context.Request.ServerVariables["HTTP_ACCEPT"] != null &&
                context.Request.ServerVariables["HTTP_ACCEPT"].ToLower().Contains("wap"))
            {
                return true;
            }
            //AND FINALLY CHECK THE HTTP_USER_AGENT 
            //HEADER VARIABLE FOR ANY ONE OF THE FOLLOWING
            if (context.Request.ServerVariables["HTTP_USER_AGENT"] != null)
            {
                //Create a list of all mobile types
                string[] mobiles =
                    new[]
                {
                    "midp", "j2me", "avant", "docomo", 
                    "novarra", "palmos", "palmsource", 
                    "240x320", "opwv", "chtml",
                    "pda", "windows ce", "mmp/", 
                    "blackberry", "mib/", "symbian", 
                    "wireless", "nokia", "hand", "mobi",
                    "phone", "cdm", "up.b", "audio", 
                    "SIE-", "SEC-", "samsung", "HTC", 
                    "mot-", "mitsu", "sagem", "sony"
                    , "alcatel", "lg", "eric", "vx", 
                    "NEC", "philips", "mmm", "xx", 
                    "panasonic", "sharp", "wap", "sch",
                    "rover", "pocket", "benq", "java", 
                    "pt", "pg", "vox", "amoi", 
                    "bird", "compal", "kg", "voda",
                    "sany", "kdd", "dbt", "sendo", 
                    "sgh", "gradi", "jb", "dddi", 
                    "moto", "iphone"
                };

                //Loop through each item in the list created above 
                //and check if the header contains that text
                foreach (string s in mobiles)
                {
                    if (context.Request.ServerVariables["HTTP_USER_AGENT"].
                                                        ToLower().Contains(s.ToLower()))
                    {
                        return true;
                    }
                }
            }

            return false;
        }


        private void ShowPopUpMsg(string msg)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("alert('");
            sb.Append(msg.Replace("\n", "\\n").Replace("\r", "").Replace("'", "\\'"));
            sb.Append("');");
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "showalert", sb.ToString(), true);
        }

        protected void btnReply_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "displayalertmessage", "HyperLinkClick();", true);
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string Docfile = Server.MapPath("Documents");

            if (FileUpload1.HasFile)
            {
                string FileName = Path.GetFileName(FileUpload1.PostedFile.FileName);


                //string SendTo = ConfigurationManager.AppSettings.Get("COMPANY_INFOEMAIL");
                string SendTo = Convert.ToString(ConfigurationManager.AppSettings.Get("Fromail"));
                string Subject = "Reply From " + Convert.ToString(Session["DistributorName"]);
                // DataSet dsNotification = svc.SaveAndSendNotification(Convert.ToInt64(Session["LoginID"]), Convert.ToInt64(Session["DistributorID"]), dt, txtTextString.Text, "", "insert", 0);

                SendMail(SendTo, Subject, FileName, Convert.ToString(txtHoldResponse.Text));
            }

            lblwarning.Style.Add("display", "none");
            btnReply.Style.Add("display", "none");
        }

        public void SendMail(string SendTo, string Subject, string Docfile, string Response)
        {
            try
            {
                string LogoUrl = "https://" + HttpContext.Current.Request.Url.Host + HttpContext.Current.Request.ApplicationPath + "/img/logo.png";
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient();


                string MailAddress = Convert.ToString(ConfigurationManager.AppSettings.Get("Fromail"));
                string PassWord = Convert.ToString(ConfigurationManager.AppSettings.Get("Password"));

                mail.From = new MailAddress(MailAddress);
                mail.To.Add(SendTo);
                TimeSpan ts = new TimeSpan(8, 0, 0);
                mail.Subject = Subject;
                if (Docfile != "")
                {
                    mail.Attachments.Add(new Attachment(FileUpload1.PostedFile.InputStream, Docfile));
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

                sb.Append("<div style=”background-color: #ece8d4; width:600px; height:200px; padding:30px; margin-top:30px;”>");
                //sb.Append("<p>Please find the new credentials and get started.</p>");

                sb.Append("<p>Dear " + ConfigurationManager.AppSettings.Get("COMPANY_NAME") + ",<p>");


                sb.Append("<br/>");
                sb.Append(Response);
                sb.Append("<br/>");

                sb.Append("<br/>");
                //sb.Append("<p>Sincerely,");
                //sb.Append("<p>" + ConfigurationManager.AppSettings.Get("COMPANY_NAME") + "</p>");
                sb.Append("<p>Thank you</p>");

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
                ShowPopUpMsg("Reply has been sent to Admin");

            }
            catch (Exception ex)
            {

            }
        }



        private void getImage1FromDB(int ImgFlag)
        {
            try
            {
                DataSet dst = svc.getImage1FromDB(ImgFlag);
                if (dst != null)
                {
                    if (dst.Tables[0].Rows.Count > 0)
                    {
                        if (dst.Tables[0].Rows[0]["Url"] != "")
                        {
                            string Photo1 = Convert.ToString(dst.Tables[0].Rows[0]["Url"]);
                            Img1.ImageUrl = dir + "/" + Photo1;
                            DivImg1.Visible = true;
                        }


                        if (dst.Tables[0].Rows.Count > 1)
                        {
                            if (dst.Tables[0].Rows[1]["Url"] != "")
                            {
                                string Photo2 = Convert.ToString(dst.Tables[0].Rows[1]["Url"]);
                                Img2.ImageUrl = dir + "/" + Photo2;
                                DivImg2.Visible = true;
                            }


                            if (dst.Tables[0].Rows.Count > 2)
                            {
                                if (dst.Tables[0].Rows[2]["Url"] != "")
                                {
                                    string Photo3 = Convert.ToString(dst.Tables[0].Rows[2]["Url"]);
                                    Img3.ImageUrl = dir + "/" + Photo3;
                                    DivImg3.Visible = true;
                                }
                            }
                        }
                    }
                }
                else
                {


                }


            }
            catch (Exception ex)
            {
                ShowPopUpMsg(ex.Message);
            }
        }
    }
}