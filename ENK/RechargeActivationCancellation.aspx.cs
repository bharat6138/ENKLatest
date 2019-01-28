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
using ENK.LycaAPI;

namespace ENK
{
    public partial class RechargeActivationCancellation : System.Web.UI.Page
    {
        Service1Client svc = new Service1Client();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
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

        protected void btnGetDetail_Click(object sender, EventArgs e)
        {
            if (ddlMSISDN.SelectedIndex > 0)
            {
                string Action = "";
                string SimNumber = "";
                if (ddlMSISDN.SelectedValue == "1")
                {
                    if (txtMSISDN.Text == "")
                    {
                        ShowPopUpMsg("Please insert MSISDN Number");
                        return;
                    }
                }
                else
                {
                    if (txtMSISDN.Text == "" && txtSerialNumber.Text == "")
                    {
                        ShowPopUpMsg("Please insert MSISDN/Sim Number ");
                        return;
                    }

                }
                if (ddlMSISDN.SelectedValue == "1")
                {
                    Action = "Recharge";
                }

                else
                {

                    Action = "Activation";

                }
                DataSet ds = new DataSet();

                if (txtSerialNumber.Text != "")
                {
                    SimNumber = txtSerialNumber.Text.Trim();
                }

                ds = svc.RechargeActivationCancelDetail(Convert.ToString(txtMSISDN.Text.Trim()), SimNumber, Action);
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        grdMSISDN.DataSource = ds.Tables[0];
                        grdMSISDN.DataBind();
                    }
                    else
                    {
                        grdMSISDN.DataSource = null;
                        grdMSISDN.DataBind();
                        ShowPopUpMsg("Recharge/Activation details are not available for this MSISDN");

                    }

                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                        {
                            for (int j = 0; j < grdMSISDN.Rows.Count; j++)
                            {
                                Label lblmsisdn = (Label)grdMSISDN.Rows[j].FindControl("lblMSISDN");
                                if (Convert.ToString(lblmsisdn.Text) == Convert.ToString(ds.Tables[1].Rows[i]["MSISDN"]))
                                {
                                    LinkButton lblCancel = (LinkButton)grdMSISDN.Rows[j].FindControl("lnkCancel");
                                    lblCancel.Visible = false;
                                }
                            }
                        }
                    }
                }

                else
                {
                    ShowPopUpMsg("Recharge/Activation details are not available for this MSISDN");
                }
            }


            else
            {
                ShowPopUpMsg("Please Select Recharge/Activation type");
                grdMSISDN.DataSource = null;
                grdMSISDN.DataBind();
            }
        }

        protected void lnkCancel_Click(object sender, EventArgs e)
        {
            LinkButton lbtn = (LinkButton)sender;
            GridViewRow grvRow = (GridViewRow)lbtn.NamingContainer;
            int i = grvRow.RowIndex;
            string SerialNumber = "";
            if (txtSerialNumber.Text != "")
            {
                SerialNumber = txtSerialNumber.Text.Trim();
            }

            HiddenField hdnID = (HiddenField)grdMSISDN.Rows[i].FindControl("hddnID");
            HiddenField hdnMSISDN = (HiddenField)grdMSISDN.Rows[i].FindControl("hddnMSISDN");
            HiddenField hdnTariffID = (HiddenField)grdMSISDN.Rows[i].FindControl("hddnTariffID");
            HiddenField hdnTariffCode = (HiddenField)grdMSISDN.Rows[i].FindControl("hddnTariffCode");
            HiddenField hdnRental = (HiddenField)grdMSISDN.Rows[i].FindControl("hddnRental");
            HiddenField hdnTariffDesc = (HiddenField)grdMSISDN.Rows[i].FindControl("hddnTariffDesc");
            HiddenField hddnRegFee = (HiddenField)grdMSISDN.Rows[i].FindControl("hddnRegFee");
            HiddenField hddnmonth = (HiddenField)grdMSISDN.Rows[i].FindControl("hddnmonth");
            TextBox txtcancelmonth = (TextBox)grdMSISDN.Rows[i].FindControl("txtcancelMonth");
            LinkButton lnkCancel = (LinkButton)grdMSISDN.Rows[i].FindControl("lnkCancel");

            if (Convert.ToInt32(txtcancelmonth.Text) > Convert.ToInt32(hddnmonth.Value))
            {
                ShowPopUpMsg("Cancellation Month should be less than Plan Month");
                txtcancelmonth.Text = hddnmonth.Value;
                return;
            }

            if (Convert.ToInt32(txtcancelmonth.Text) == 0 || txtcancelmonth.Text == "")
            {
                ShowPopUpMsg("Cancellation Month can not be zero or blank");
                txtcancelmonth.Text = hddnmonth.Value;
                return;
            }

            string PayAmount = Convert.ToString(Convert.ToDecimal(hdnRental.Value));

            string Subject = ConfigurationManager.AppSettings.Get("SHORT_COMPANY_NAME") + " " + ":" + " " + "CANCEL PLAN" + " " + hdnTariffDesc.Value + " " + "on" + " " + hdnMSISDN.Value + " " + " and Refund to Invoice";

            string SendTo = Convert.ToString(ConfigurationManager.AppSettings.Get("GTAC_Mail"));



            int a = 0;
            a = svc.SaveRechargeActivationCancelDetails(Convert.ToInt32(hdnID.Value), Convert.ToString(hdnMSISDN.Value), Convert.ToInt32(hdnTariffID.Value), Convert.ToDecimal(hdnRental.Value), Convert.ToDecimal(hddnRegFee.Value), Convert.ToString(ddlMSISDN.SelectedItem), Convert.ToInt16(txtcancelmonth.Text));

            if (a > 0)
            {
                //  SendMail(SendTo, Subject, Convert.ToString(hdnMSISDN.Value), Convert.ToString(hdnTariffDesc.Value), Convert.ToString(hddnmonth.Value), PayAmount);
                ShowPopUpMsg("Recharge/Activation cancellation request has been sent");
                lnkCancel.Visible = false;
            }
            else
            {
                ShowPopUpMsg("Recharge/Activation cancellation request Failed");
            }


        }

        public void SendMail(string SendTo, string Subject, string MSISDN, string PlanCode, string month, string PayAmount)
        {
            try
            {
                string LogoUrl = "https://" + HttpContext.Current.Request.Url.Host + HttpContext.Current.Request.ApplicationPath + "/img/logo.png";
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient();


                string MailAddress = ConfigurationManager.AppSettings.Get("Fromail");
                string PassWord = ConfigurationManager.AppSettings.Get("Password");
                string UserName = "GTAC";

                mail.From = new MailAddress(MailAddress);
                mail.To.Add(SendTo);
                TimeSpan ts = new TimeSpan(8, 0, 0);
                mail.Subject = Subject;

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

                sb.Append("<p>Greetings!!</p>");
                sb.Append("<p>");
                sb.Append("<br/>");

                sb.Append("<p>Please cancel the " + PlanCode + " for " + month + " month,  Value " + PayAmount + " in " + MSISDN + " and Refund it to Invoice</p>");
                sb.Append("<br/>");
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
                ShowPopUpMsg("Recharge/Activation cancellation request has been sent to GTAC");

            }
            catch (Exception ex)
            {
                ShowPopUpMsg("Recharge/Activation cancellation Request Failed");
            }
        }

        protected void ddlMSISDN_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(ddlMSISDN.SelectedValue) == 2)
            {
                divSerialNumber.Visible = true;
            }
            else
            {
                divSerialNumber.Visible = false;
            }

            if (ddlMSISDN.SelectedIndex == 0)
            {
                grdMSISDN.DataSource = null;
                grdMSISDN.DataBind();

            }
        }

    }
}