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
    public partial class approvals : System.Web.UI.Page
    {
        Service1Client svc = new Service1Client();
        protected void Page_Load(object sender, EventArgs e)
        {
            string _req = (Convert.ToString(Request.QueryString["req"]) == null ? string.Empty
                : Convert.ToString(Request.QueryString["req"]));
            string _requestid = (Convert.ToString(Request.QueryString["id"]) == null ? string.Empty
               : Convert.ToString(Request.QueryString["id"]));
            string _action = (Convert.ToString(Request.QueryString["act"]) == null ? string.Empty
               : Convert.ToString(Request.QueryString["act"]));
            string _token = (Convert.ToString(Request.QueryString["token"]) == null ? string.Empty
               : Convert.ToString(Request.QueryString["token"]));

            _req = Encryption.Decrypt(HttpUtility.UrlDecode(_req));
            _requestid = Encryption.Decrypt(HttpUtility.UrlDecode(_requestid));
            _action = Encryption.Decrypt(HttpUtility.UrlDecode(_action));
            _token = Encryption.Decrypt(HttpUtility.UrlDecode(_token));

            hddnEmailid.Value = _requestid;
            if (_req.Trim().ToLower() == "forgotpassword")
                divPassword.Visible = true;
        }

        protected void btnReasonforCancelled_Click(object sender, EventArgs e)
        {
            if (hddnEmailid.Value != "")
            {
                int a = svc.ResetSubscriberPassword(Convert.ToString(hddnEmailid.Value), txtpass.Text.ToString().Trim());
                if (a > 0)
                {
                    ShowPopUpMsg("Password Reset Successfull.");
                    divPassword.Visible = false;
                }
            }
            else {

                ShowPopUpMsg("somthing is wrong");
                return ;
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