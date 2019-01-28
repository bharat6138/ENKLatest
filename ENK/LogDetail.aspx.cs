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
    public partial class LogDetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                TextBox1.Attributes.Add("readonly", "true");
                TextBox2.Attributes.Add("readonly", "true");
                TextBox3.Attributes.Add("readonly", "true");

                TextBox1.Text = "";
                TextBox2.Text = "";
                TextBox3.Text = "";
            }
        }
        public void ShowLog()
        {
            

        }

        private void ShowPopUpMsg(string msg)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("alert('");
            sb.Append(msg.Replace("\n", "\\n").Replace("\r", "").Replace("'", "\\'"));
            sb.Append("');");
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "showalert", sb.ToString(), true);
        }

        protected void btnLog1_Click(object sender, EventArgs e)
        {
            try
            {
                divLog1.Visible = true;
                divLog2.Visible = false;
                divLog3.Visible = false;
                TextBox1.Text = "";
                TextBox2.Text = "";
                TextBox3.Text = "";

                string filename = "lycalog.txt";
                string strPath = Server.MapPath("Log") + "/" + filename;
                string root = "https://" + HttpContext.Current.Request.Url.Host + HttpContext.Current.Request.ApplicationPath + "/Log/" + filename;
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

            ScriptManager.RegisterStartupScript(this, GetType(), "", " validate1();", true);
        }

        protected void btnLog2_Click(object sender, EventArgs e)
        {
            try
            {
                divLog1.Visible = false;
                divLog2.Visible = true;
                divLog3.Visible = false;

                TextBox1.Text = "";
                TextBox2.Text = "";
                TextBox3.Text = "";

                string filename = "ActivationLog.txt";
                string strPath = Server.MapPath("Log") + "/" + filename;
                string root = "https://" + HttpContext.Current.Request.Url.Host + HttpContext.Current.Request.ApplicationPath + "/Log/" + filename;
                if (File.Exists(strPath))
                {
                    StreamReader sr = new StreamReader(strPath);
                    string sh = sr.ReadToEnd();
                    TextBox2.Text = sh;
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
            ScriptManager.RegisterStartupScript(this, GetType(), "", " validate2();", true);
        }

        protected void btnLog3_Click(object sender, EventArgs e)
        {
            try
            {
                divLog1.Visible = false;
                divLog2.Visible = false;
                divLog3.Visible = true;

                TextBox1.Text = "";
                TextBox2.Text = "";
                TextBox3.Text = "";

                string filename = "Topup.txt";
                string strPath = Server.MapPath("Log") + "/" + filename;
                string root = "https://" + HttpContext.Current.Request.Url.Host + HttpContext.Current.Request.ApplicationPath + "/Log/" + filename;
                if (File.Exists(strPath))
                {
                    StreamReader sr = new StreamReader(strPath);
                    string sh = sr.ReadToEnd();
                    TextBox3.Text = sh;
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
            ScriptManager.RegisterStartupScript(this, GetType(), "", " validate3();", true);
        }

        protected void linkLog1_Click(object sender, EventArgs e)
        {
            divLog1.Visible = true;
            divLog2.Visible = false;
            divLog3.Visible = false;

            TextBox1.Text = "";
            TextBox2.Text = "";
            TextBox3.Text = "";
        }

        protected void linkLog2_Click(object sender, EventArgs e)
        {
            divLog1.Visible = false;
            divLog2.Visible = true;
            divLog3.Visible = false;

            TextBox1.Text = "";
            TextBox2.Text = "";
            TextBox3.Text = "";
        }

        protected void linkLog3_Click(object sender, EventArgs e)
        {
            divLog1.Visible = false;
            divLog2.Visible = false;
            divLog3.Visible = true;

            TextBox1.Text = "";
            TextBox2.Text = "";
            TextBox3.Text = "";
        }

       
    }
}