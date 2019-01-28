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
using System.Net.Mail;
using System.Configuration;

namespace ENK
{
    public partial class SimReplacement : System.Web.UI.Page
    {
        Service1Client ssc = new Service1Client();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                btnReplace.Attributes.Add("disabled", "true");
                //txtCurrentSIMNumber.ReadOnly = true;
                //txtCurrentSIMNumber.ReadOnly = false;

            }
        }

        protected void btnReplace_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    SIM s = new SIM();
            //    s.ClientID = Convert.ToInt32(Session["DistributorID"]);
            //    s.MSISDN_SIM_ID = Convert.ToInt64(hdnMSISDN_SIM_ID.Value);
            //    s.SIMNo = Convert.ToString(txtNewSIMNumber.Text.Trim());
            //    s.UserID = Convert.ToInt32(Session["LoginId"]);
            //    string simno = Convert.ToString(txtNewSIMNumber.Text.Trim());
            //    DataSet ds = ssc.GetInventoryForSIMReplacement("NewSIMNumber", simno, Convert.ToInt32(Session["DistributorID"]));

            //    if (ds != null)
            //    {
            //        if (ds.Tables.Count > 0)
            //        {

            //            if (ds.Tables[0].Rows.Count > 0)
            //            {

            //                int retval = ssc.SIMReplacement(s, Actions.INSERT);
            //                if (retval > 0)
            //                {
            //                    System.Text.StringBuilder javaScript = new System.Text.StringBuilder();
            //                    string scriptKey = "ConfirmationScript";

            //                    javaScript.Append("var userConfirmation = window.alert('" + "SIM Replacement successfully" + "');\n");
            //                    ClientScript.RegisterStartupScript(this.GetType(), scriptKey, javaScript.ToString(), true);

            //                }
            //                else
            //                {

            //                    System.Text.StringBuilder javaScript = new System.Text.StringBuilder();
            //                    string scriptKey = "ConfirmationScript";

            //                    javaScript.Append("var userConfirmation = window.alert('" + "Error ! Please check" + "');\n");
            //                    ClientScript.RegisterStartupScript(this.GetType(), scriptKey, javaScript.ToString(), true);
            //                }


            //                txtCurrentSIMNumber.Enabled = true;
            //                txtCurrentMobileNumber.Enabled = true;

            //                txtNewSIMNumber.Text = string.Empty;
            //                txtCurrentSIMNumber.Text = string.Empty;
            //                txtCurrentMobileNumber.Text = string.Empty;
            //                hdnMSISDN_SIM_ID.Value = string.Empty;

            //                btnReplace.Attributes.Add("disabled", "true");

            //            }
            //            else
            //            {
            //                System.Text.StringBuilder javaScript = new System.Text.StringBuilder();
            //                string scriptKey = "ConfirmationScript";

            //                javaScript.Append("var userConfirmation = window.alert('" + "New SIM Number Does Not Exist." + "');\n");
            //                ClientScript.RegisterStartupScript(this.GetType(), scriptKey, javaScript.ToString(), true);
            //            }
            //        }
            //    }

            //}
            //catch (Exception ex)
            //{

            //}
            // 
            try
            {
                string simno = Convert.ToString(txtNewSIMNumber.Text.Trim());
                DataSet ds = ssc.GetInventoryForSIMReplacement("NewSIMNumber", simno, Convert.ToInt32(Session["DistributorID"]));

                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {

                        if (ds.Tables[0].Rows.Count > 0)
                        {

                            string ReplacementUrl = "http://activatemysim.net/SimReplacementApproved.aspx?ClientID=" + Session["DistributorID"].ToString() + "&MSISDNSIMID=" + hdnMSISDN_SIM_ID.Value + "&SIMNo=" + txtNewSIMNumber.Text.Trim() + "&UserID=" + Session["LoginId"].ToString();
                            SendMailToDistributor(txtCurrentSIMNumber.Text, txtCurrentMobileNumber.Text, txtNewSIMNumber.Text);
                            SendMailToSupport(ReplacementUrl, txtCurrentSIMNumber.Text, txtCurrentMobileNumber.Text, txtNewSIMNumber.Text);
                            txtCurrentSIMNumber.Enabled = true;
                            txtCurrentMobileNumber.Enabled = true;

                            txtNewSIMNumber.Text = string.Empty;
                            txtCurrentSIMNumber.Text = string.Empty;
                            txtCurrentMobileNumber.Text = string.Empty;
                            hdnMSISDN_SIM_ID.Value = string.Empty;
                            ShowPopUpMsg("Our request for SIM replacement submitted successfully");
                        }
                        else
                        {
                            System.Text.StringBuilder javaScript = new System.Text.StringBuilder();
                            string scriptKey = "ConfirmationScript";

                            javaScript.Append("var userConfirmation = window.alert('" + "New SIM Number Does Not Exist." + "');\n");
                            ClientScript.RegisterStartupScript(this.GetType(), scriptKey, javaScript.ToString(), true);
                        }
                    }
                }

            }
            catch (Exception ex)
            {

            }


        }

        public void SendMailToSupport(string ReplacementUrl, string CurrentSIMNo, string CurrentMobileNo, string NewSIMNo)
        {
            try
            {
              
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient();
                string MailAddress = ConfigurationManager.AppSettings.Get("Fromail");
                string PassWord = ConfigurationManager.AppSettings.Get("Password");
                string Subject = "SIM Replacement Request From: " + Session["Fname"].ToString() + " " + Session["Lname"].ToString();
                mail.From = new MailAddress(MailAddress);
                mail.To.Add("support@virtuzo.net");
                mail.CC.Add("puneet.goel@virtuzo.in");
                mail.CC.Add("vivek.chaudhary@virtuzo.net");
                TimeSpan ts = new TimeSpan(8, 0, 0);
                mail.Subject = Subject + " " + DateTime.UtcNow.Subtract(ts).ToString();
                StringBuilder sb = new StringBuilder();
                sb.Append("<html>");
                sb.Append("<body style=”color:grey; font-size:15px;”>");
                sb.Append("<font face=”Helvetica, Arial, sans-serif”>");
                sb.Append("<br/>");
                sb.Append("<div style=”background-color: #ece8d4; width:600px; height:200px; padding:30px; margin-top:30px;”>");
                sb.Append("<p> Hi Support Team <p>");
                sb.Append("<p>SIM Replacement Request Detail:</p>");
                sb.Append("<p>");
                sb.Append("<br/>");
                sb.Append("Username: " + Session["Fname"].ToString() + " " + Session["Lname"].ToString() + "<br>");
                sb.Append("Current SIM Number: " + CurrentSIMNo + "<br>");
                sb.Append("Current Mobile Number: " + CurrentMobileNo + "<br>");
                sb.Append("New SIM Number: " + NewSIMNo + "<br>");
                sb.Append("<br/>");
                sb.Append("<div style=”position:absolute; height:50px; width:150px; background-color:0d1d36; padding:30px;”>");
                sb.Append("<a href=" + ReplacementUrl + ">Update SIM Replacement</a>");
                sb.Append("</div>");
                sb.Append("<br/>");
                sb.Append("<p>Sincerely,");
                sb.Append("<p>" + ConfigurationManager.AppSettings.Get("COMPANY_NAME") + "</p>");
                sb.Append("<p>Thank you</p>");
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
        public void SendMailToDistributor(string CurrentSIMNo, string CurrentMobileNo, string NewSIMNo)
        {
            try
            {

                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient();

                string Subject = "SIM replacement request submitted";
                string MailAddress = ConfigurationManager.AppSettings.Get("Fromail");
                string PassWord = ConfigurationManager.AppSettings.Get("Password");

                mail.From = new MailAddress(MailAddress);
                mail.To.Add(Session["EmailID"].ToString());
                TimeSpan ts = new TimeSpan(8, 0, 0);
                mail.Subject = Subject + " " + DateTime.UtcNow.Subtract(ts).ToString();

                StringBuilder sb = new StringBuilder();
                sb.Append("<html>");
                sb.Append("<body style=”color:grey; font-size:15px;”>");
                sb.Append("<font face=”Helvetica, Arial, sans-serif”>");


                sb.Append("<div style=”background-color: #ece8d4; width:600px; height:200px; padding:30px; margin-top:30px;”>");
                //sb.Append("<p>Please find the new credentials and get started.</p>");

                sb.Append("<p>Dear " + Session["Fname"].ToString() + " " + Session["Lname"].ToString() + ",<p>");

                sb.Append("<p>Your SIM replacement request submitted successfuly. SIM replacement detail are following: </p>");
                sb.Append("<p>");
                sb.Append("<br/>");
                sb.Append("Current SIM Number: " + CurrentSIMNo + "<br>");
                sb.Append("Current Mobile Number: " + CurrentMobileNo + "<br>");
                sb.Append("New SIM Number: " + NewSIMNo + "<br>");

                sb.Append("<br/>");

                sb.Append("<p>Sincerely,");
                sb.Append("<p>" + ConfigurationManager.AppSettings.Get("COMPANY_NAME") + "</p>");
                sb.Append("<p>Thank you</p>");
                sb.Append("<p>----------------------------------------------------------------");

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

        protected void btnVerify_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtCurrentSIMNumber.Text != "" || txtCurrentMobileNumber.Text != "")
                {
                    //DataTable dtSimMobile = objcStockInController.GetSIMOrMobileNo(txtCurrentSimNo.Text.Trim(), txtCurrentMSISDNNo.Text.Trim());
                    string simno = Convert.ToString(txtCurrentSIMNumber.Text.Trim());
                    string mobileno = Convert.ToString(txtCurrentMobileNumber.Text.Trim());
                    if (simno == "")
                    {
                        simno = "No";

                    }
                    if (mobileno == "")
                    {
                        mobileno = "No";
                    }

                    DataSet ds = ssc.GetInventoryForSIMReplacement(mobileno, simno, Convert.ToInt32(Session["DistributorID"]));
                    if (ds != null)
                    {
                        if (ds.Tables.Count > 0)
                        {

                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                if (ds.Tables[0].Rows[0]["Action"].ToString() == "SIM is not activated.")
                                {
                                    System.Text.StringBuilder javaScript = new System.Text.StringBuilder();
                                    string scriptKey = "ConfirmationScript";

                                    javaScript.Append("var userConfirmation = window.alert('" + "SIM replacement can not be done as SIM is not activated." + "');\n");
                                    ClientScript.RegisterStartupScript(this.GetType(), scriptKey, javaScript.ToString(), true);
                                }
                                else
                                {
                                    txtCurrentSIMNumber.Text = ds.Tables[0].Rows[0]["SerialNumber"].ToString();
                                    txtCurrentMobileNumber.Text = ds.Tables[0].Rows[0]["MSISDN"].ToString();
                                    //hdnCurrentSIMID.Value = ds.Tables[0].Rows[0]["SIMID"].ToString();
                                    //hdnMobileID.Value = ds.Tables[0].Rows[0]["MSISDNID"].ToString();
                                    hdnMSISDN_SIM_ID.Value = ds.Tables[0].Rows[0]["MSISDN_SIM_ID"].ToString();

                                    //txtCurrentSIMNumber.ReadOnly = true;
                                    //txtCurrentMobileNumber.ReadOnly = true;
                                    txtCurrentSIMNumber.Enabled = false;
                                    txtCurrentMobileNumber.Enabled = false;

                                    //txtCurrentSIMNumber.Attributes.Add("ReadOnly","true");// = true;
                                    //txtCurrentMobileNumber.Attributes.Add("ReadOnly", "true");

                                    btnReplace.Attributes.Remove("disabled");
                                }

                            }
                            else
                            {
                                System.Text.StringBuilder javaScript = new System.Text.StringBuilder();
                                string scriptKey = "ConfirmationScript";

                                javaScript.Append("var userConfirmation = window.alert('" + "SIM or Mobile does not exist." + "');\n");
                                ClientScript.RegisterStartupScript(this.GetType(), scriptKey, javaScript.ToString(), true);
                            }
                        }
                    }


                }
                else
                {
                    System.Text.StringBuilder javaScript = new System.Text.StringBuilder();
                    string scriptKey = "ConfirmationScript";

                    javaScript.Append("var userConfirmation = window.alert('" + "Please enter either Current SIM number or Mobile number" + "');\n");
                    ClientScript.RegisterStartupScript(this.GetType(), scriptKey, javaScript.ToString(), true);
                }
            }
            catch (Exception ex)
            {
            }
        }

        protected void btnResetAll_Click(object sender, EventArgs e)
        {
            Response.Redirect("SimReplacement.aspx");
        }

    }
    }
