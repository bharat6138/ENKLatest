using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.IO;


namespace ENK
{
    public partial class SuscriberLog : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLog_Click(object sender, EventArgs e)
        {
            try
            {
                string filename = "Subscriberlog.txt";
                string strPath = Server.MapPath("Log") + "/" + filename;
                string root = "http://" + HttpContext.Current.Request.Url.Host + HttpContext.Current.Request.ApplicationPath + "/Log/" + filename;
                if (File.Exists(strPath))
                {
                    StreamReader sr = new StreamReader(strPath);
                    string sh = sr.ReadToEnd();
                    TextBox1.Text = sh;
                    sr.Close();

                }
                else
                {
                    ShowPopUpMsg("File Not Found");
                }
            }
            catch (Exception ex)
            {
                ShowPopUpMsg(ex.Message);
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