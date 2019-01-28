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
    public partial class UserList : System.Web.UI.Page
    {
        Service1Client svc = new Service1Client();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    int userid = Convert.ToInt32(Session["LoginId"]);
                    int DistributorID = Convert.ToInt32(Session["DistributorID"]);
                    DataSet ds = svc.GetUserListService(userid, DistributorID);


                    Session["User"] = ds.Tables[0];


                    if (ds != null)
                    {
                        RepeaterUserList.DataSource = ds;
                        RepeaterUserList.DataBind();


                    }
                }
                catch (Exception ex)
                {

                }
            }
        }

        protected void RepeaterUserList_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "View")
                {
                    int id = Convert.ToInt32(e.CommandArgument);
                    string idnty = Encryption.Encrypt(id.ToString());
                    string condition = Encryption.Encrypt("View");
                    Response.Redirect("~/User.aspx?Identity=" + idnty + "&Condition=" + condition, false);
                }
                if (e.CommandName == "RowEdit")
                {
                    int id = Convert.ToInt32(e.CommandArgument);
                    string idnty = Encryption.Encrypt(id.ToString());
                    string condition = Encryption.Encrypt("Edit");
                    Response.Redirect("~/User.aspx?Identity=" + idnty + "&Condition=" + condition, false);
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void btnsearch_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = (DataTable)Session["User"];
            if (txtSearch.Text == "" && txtEmail.Text == "")
            {
                RepeaterUserList.DataSource = dt;
                RepeaterUserList.DataBind();
                return;
            }
            if (dt.Rows.Count > 0)
            {

                string User = string.IsNullOrEmpty(txtSearch.Text) ? "" : "%" + txtSearch.Text + "%";
                string Email = string.IsNullOrEmpty(txtEmail.Text) ? "" : "%" + txtEmail.Text + "%";
                // string Uasername = string.IsNullOrEmpty(txtUasername.Text) ? "" : "%" + txtUasername.Text + "%";

                //dataView.RowFilter = @"Column1 like '" + Column1 + "'" + "OR Column2 like '" + Column2 + "'" + "OR Column3 like '" + Column3 + "'";



                DataView dv = new DataView(dt);
                if (User != "" && Email != "")
                {
                    dv.RowFilter = "FName like '" + User + "'" + "OR EmailID like '" + Email + "'";
                }
                else if (User != "" && Email == "")
                {
                    dv.RowFilter = "FName like '" + User + "'";
                }

                else if (User == "" && Email != "")
                {
                    dv.RowFilter = "EmailID like '" + Email + "'";
                }

                // dv.RowFilter = "UserName like '" + UserID + "'" + "OR EmailID like '" + Email + "'" + "OR fnAME like '" + Uasername + "'";
                dt = dv.ToTable();
                RepeaterUserList.DataSource = dt;
                RepeaterUserList.DataBind();


            }




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

        protected void btnExportToExcel_Click(object sender, EventArgs e)
        {

        }

        protected void lnkResetPass_Click(object sender, EventArgs e)
        {
            try
            {

                LinkButton btn = (LinkButton)(sender);

                int Id = Convert.ToInt32(btn.CommandArgument);

                DataSet dset = svc.GetRandomPassword(Convert.ToInt64(Id));
                if (dset.Tables[0].Rows.Count > 0)
                {

                    string Password = Convert.ToString(dset.Tables[0].Rows[0]["NewPassword"]);
                    string EncPassword = Encryption.Encrypt(Password);

                    DataSet dsave = svc.SaveRandomPassword(Id, EncPassword);

                    DataSet ds1 = svc.FetchContactDetail(Id);
                    if (ds1.Tables[0].Rows.Count > 0)
                    {
                        if (ds1.Tables[1].Rows.Count > 0)
                        {
                            string username = Convert.ToString(ds1.Tables[1].Rows[0]["Name"]);
                            string mailID = Convert.ToString(ds1.Tables[1].Rows[0]["EmailID"]);
                            string userID = Convert.ToString(ds1.Tables[1].Rows[0]["UserName"]);


                            string LoginUrl = "https://" + HttpContext.Current.Request.Url.Host + HttpContext.Current.Request.ApplicationPath + "/Login.aspx";

                            SendMail(mailID, "Password Reset ", username, userID, Password, LoginUrl);
                            ShowPopUpMsg("Password send to your registered Emailid \n check your mail");
                        }
                    }

                    //Password = Encryption.Decrypt(Password);

                }
                else
                {
                    ShowPopUpMsg("");
                }




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
    }
}