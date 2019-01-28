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

namespace ENK
{
    public partial class Role : System.Web.UI.Page
    {
        Service1Client svc = new Service1Client();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["LoginID"] != null)
            {
                if (Session["ClientType"].ToString() == "Distributor")
                {
                    Response.Redirect("Login.aspx", false);
                    Session.Abandon();
                    return;
                }

            }
            if (!Page.IsPostBack)
            {
                FillRepeater();

                string Condition = Request.QueryString["ac"];
                if (Condition != null && Condition != "")
                {
                    Condition = Request.QueryString["ac"];
                }
                else
                {
                    Condition = "";
                }

                string Identity = Request.QueryString["Id"];
                if (Identity != null && Identity != "")
                {
                    Identity = Request.QueryString["Id"];
                }
                else
                {
                    Identity = "";
                }

                if (Condition == "v")
                {
                    DisableButton();
                }
            }
        }


        private void DisableButton()
        {
            txtRole.Enabled = false;
            btnBack.Visible = false;
            btnSave.Visible = false;
            btnReset.Visible = false;
        }

        public void FillRepeater()
        {
          DataSet ds=  svc.GetScreenService(0);

          if (ds != null)
          {
              if (ds.Tables[0].Rows.Count > 0)
              {
                  RepeaterScreen.DataSource = ds.Tables[0];
                  RepeaterScreen.DataBind();
              }
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

        protected void btnBack_Click(object sender, EventArgs e)
        {

        }

        protected void btnReset_Click(object sender, EventArgs e)
        {

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {

        }
    }
}