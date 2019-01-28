using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Net.Mail;
using System.Configuration;
using ENK.ServiceReference1;

namespace ENK
{
    public partial class Resetpassword : System.Web.UI.Page
    {
        Service1Client svc = new Service1Client();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Session["LoginID"] != null)
                {
                    txtUserID.Text=Convert.ToString(Session["UserName"]);
                    txtUserID.Attributes.Add("readonly", "true");
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                int DistributorID = Convert.ToInt32(Session["DistributorID"]);
                int LoginID = Convert.ToInt32(Session["LoginID"]);

                string UserID = txtUserID.Text.Trim();
                string oldpass = txtOldPass.Text.Trim();
                oldpass=Encryption.Encrypt(oldpass);
                string NewPass = txtNewPassword.Text.Trim();
                NewPass = Encryption.Encrypt(NewPass);
                string NewConfirmPass = txtConfirmNewPassword.Text.Trim();

                DataSet ds = svc.ResetPasswordService(UserID, oldpass, NewPass, DistributorID, LoginID);

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        string Response = Convert.ToString(ds.Tables[0].Rows[0]["Response"]);
                        string message = Convert.ToString(ds.Tables[0].Rows[0]["Message"]);
                        if (Response == "success")
                        {
                            string username = Convert.ToString(ds.Tables[0].Rows[0]["Name"]);
                            string mail = Convert.ToString(ds.Tables[0].Rows[0]["EmailID"]);
                            string userid = UserID;
                            string pwd = Convert.ToString(ds.Tables[0].Rows[0]["Password"]);
                            pwd = Encryption.Decrypt(pwd);

                            string LoginUrl = "https://" + HttpContext.Current.Request.Url.Host + HttpContext.Current.Request.ApplicationPath + "/Login.aspx";

                            SendMail(mail, "Password Reset ", username, userid, pwd, LoginUrl);
                            ShowPopUpMsg("Password has been reset successfully");
                           // txtUserID.Text = "";
                            txtOldPass.Text = "";
                            txtConfirmNewPassword.Text = "";
                            txtNewPassword.Text = "";
                        }
                        else
                        {
                            ShowPopUpMsg(message);
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                ShowPopUpMsg("Error \n Please Try Again \n Contact to system administrator");
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            //txtUserID.Text = "";
            txtOldPass.Text = "";
            txtNewPassword.Text = "";
            txtConfirmNewPassword.Text = "";
        }

       

        public void SendMail(string SendTo, string Subject, string UserName, string UserID, string pass, string LoginUrl)
        {
            try
            {
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

                sb.Append("<p>Dear " + UserName + ",<p>");

                sb.Append("<p>Please find the new credentials and get started.</p>");
                sb.Append("<p>");
                sb.Append("<br/>");

                sb.Append("Username: " + UserID + "<br>");
                sb.Append("Password: " + pass + "<br>");
                sb.Append("<br/>");
                sb.Append("<p>Your password is secure show don't share it</p>");

                sb.Append("<p>Use this link for login</p>");
                sb.Append("<p>" + LoginUrl + "</p>");
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


        private void ShowPopUpMsg(string msg)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("alert('");
            sb.Append(msg.Replace("\n", "\\n").Replace("\r", "").Replace("'", "\\'"));
            sb.Append("');");
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "showalert", sb.ToString(), true);
        }

        protected void btnSaveUp_Click(object sender, EventArgs e)
        {
            btnSave_Click(null, null);
        }

        protected void btnResetUp_Click(object sender, EventArgs e)
        {
            txtOldPass.Text = "";
            txtNewPassword.Text = "";
            txtConfirmNewPassword.Text = "";
        }
    }
}