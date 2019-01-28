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

namespace ENK
{
    public partial class DistributorNotes : System.Web.UI.Page
    {
        Service1Client ssc = new Service1Client();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Session["LoginId"] != null)
                {
                    

                    BindDDL();
                    int LoginID = Convert.ToInt32(Session["LoginId"]);
                    int distributorID = Convert.ToInt32(Session["DistributorID"]);
                    int ClientTypeID = Convert.ToInt32(Session["ClientTypeID"]);
                    //DateTime FromDate = DateTime.Now;
                    //DateTime ToDate = DateTime.Now;
                }
            }
        }

        public void BindDDL()
        {
            try
            {
                int userid = Convert.ToInt32(Session["LoginId"]);
                int distributorID = Convert.ToInt32(Session["DistributorID"]);
                Distributor[] ds = ssc.GetDistributorDDLService(userid, distributorID);

                ddlDistributor.DataSource = ds;
                ddlDistributor.DataValueField = "distributorID";
                ddlDistributor.DataTextField = "distributorName";
                ddlDistributor.DataBind();
                ddlDistributor.Items.Insert(0, new ListItem("ALL", "0"));

                ddlDistributor.SelectedValue = Convert.ToString(distributorID); ;


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
        protected void btnSave_Click(object sender, EventArgs e)
        {
            int userid = Convert.ToInt32(Session["LoginId"]);
            int a = ssc.SaveDistributorNotes(Convert.ToInt32(ddlDistributor.SelectedValue), txtNotes.Text.ToString(), userid);
            
            if (a > 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "", "JScriptConfirmationUpdatePopupBox();", true);
                    
        
            }
        }


        public void SendMail(string SendTo="puneet.goel@virtuzo.in", string Subject="Test email")
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

                //sb.Append("<p>Sim Number  " + UserID + "  Activated successfully on Mobile Number  " + UserName + " <p>");


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

                SmtpClient sc = new SmtpClient();

                sc.Host = ConfigurationManager.AppSettings.Get("Host");
                sc.Port = 587;
                sc.Credentials = new System.Net.NetworkCredential(MailAddress, PassWord);
                sc.EnableSsl = true;

                sc.Send(mail);

            }
            catch (Exception ex)
            {

            }
        }



        protected void btnExportToExcel_Click(object sender, EventArgs e)
        {
            Response.Redirect("NotesReport.aspx");
        }
    }
}