using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ENK
{
    public partial class ENKEmailer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                string body = this.PopulateBody();
                this.SendHtmlFormattedEmail("Uday.v@Virtuzo.in", "Testing Emailer!", body);
            }
            catch (Exception ex)
            {
                ShowPopUpMsg("Error \n Please Try Again \n Contact to system administrator");
            }
        }

        //public void SendMail()
        //{
        //    try
        //    {
        //        string LogoUrl = "https://" + HttpContext.Current.Request.Url.Host + HttpContext.Current.Request.ApplicationPath + "/img/logo.png";
        //        MailMessage mail = new MailMessage();
        //        SmtpClient SmtpServer = new SmtpClient();


        //        string MailAddress = ConfigurationManager.AppSettings.Get("Fromail");
        //        string PassWord = ConfigurationManager.AppSettings.Get("Password");
        //        string SendTo = ConfigurationManager.AppSettings.Get("SendTo");
        //        mail.From = new MailAddress(MailAddress);
        //        mail.To.Add(SendTo);
        //        TimeSpan ts = new TimeSpan(8, 0, 0);
        //        mail.Subject = "ENK HTML" + " " + DateTime.UtcNow.Subtract(ts).ToString();

        //        StringBuilder sb = new StringBuilder();
        //        sb.Append("<html>");
        //        sb.Append("<body style=”color:grey; font-size:15px;”>");
        //        sb.Append("<font face=”Helvetica, Arial, sans-serif”>");

        //        sb.Append("<div style=”position:absolute; height:200px; width:100px; background-color:0d1d36; padding:30px;”>");
        //        sb.Append("<img src=" + LogoUrl + " />");
        //        sb.Append("</div>");

        //        sb.Append("<br/>");

        //        sb.Append("<br/>");

        //        sb.Append("<div style=”background-color: #ece8d4; width:600px; height:200px; padding:30px; margin-top:30px;”>");
        //        //sb.Append("<p>Please find the new credentials and get started.</p>");


        //        sb.Append("<p>----------------------------------------------------------------");
        //        sb.Append("<p>Please do not reply to this email. This mailbox is not monitored and you will not receive a response. ");

        //        sb.Append("<br/>");
        //        sb.Append("</div>");
        //        sb.Append("</body>");
        //        sb.Append("</html>");

        //        mail.Body = sb.ToString();
        //        mail.BodyEncoding = System.Text.Encoding.UTF8;
        //        mail.IsBodyHtml = true;
        //        mail.Priority = MailPriority.High;

        //        SmtpServer.Host = ConfigurationManager.AppSettings.Get("Host");
        //        SmtpServer.Port = 587;
        //        SmtpServer.Credentials = new System.Net.NetworkCredential(MailAddress, PassWord);
        //        SmtpServer.EnableSsl = true;

        //        SmtpServer.Send(mail);

        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}


        private void SendHtmlFormattedEmail(string recepientEmail, string subject, string body)
        {
            using (MailMessage mailMessage = new MailMessage())
            {

                string MailAddress = ConfigurationManager.AppSettings.Get("Fromail");
                string PassWord = ConfigurationManager.AppSettings.Get("Password");
                //string SendTo = ConfigurationManager.AppSettings.Get("SendTo");
                string SendTo = recepientEmail;

                mailMessage.From = new MailAddress(MailAddress);
                mailMessage.Subject = subject;
                mailMessage.Body = body;
                mailMessage.IsBodyHtml = true;
                mailMessage.To.Add(new MailAddress(SendTo));
                SmtpClient smtp = new SmtpClient();
                smtp.Host = ConfigurationManager.AppSettings.Get("Host");
                smtp.EnableSsl = true;
                System.Net.NetworkCredential NetworkCred = new System.Net.NetworkCredential();
                NetworkCred.UserName = MailAddress;
                NetworkCred.Password = PassWord;
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = NetworkCred;
                smtp.Port = 587;
                smtp.Send(mailMessage);
            }
        }


        //private string PopulateBody(string userName, string title, string url, string description)
        private string PopulateBody()
        {
            string body = string.Empty;
            using (StreamReader reader = new StreamReader(Server.MapPath("~/Emailer.htm")))
            {
                body = reader.ReadToEnd();
            }
            //body = body.Replace("{UserName}", userName);
            //body = body.Replace("{Title}", title);
            //body = body.Replace("{Url}", url);
            //body = body.Replace("{Description}", description);
            return body;
        }

        private void ShowPopUpMsg(string msg)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("alert('");
            sb.Append(msg.Replace("\n", "\\n").Replace("\r", "").Replace("'", "\\'"));
            sb.Append("');");
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "showalert", sb.ToString(), true);
        }
    }
}