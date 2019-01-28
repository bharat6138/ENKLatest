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
    public partial class Currency : System.Web.UI.Page
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

        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            txtCurrency.Text = "";
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtCurrency.Text != "")
                {
                    string name = txtCurrency.Text.Trim();
                    int a = svc.InsertCurrencyService(name);

                    if (a > 0)
                    {
                        ShowPopUpMsg("Currency Save Successfully");
                        txtCurrency.Text = "";
                    }
                    else
                    {
                        ShowPopUpMsg("Currency Save Unsuccessfully \n Please Try Again");
                    }
                }
                else
                {
                    ShowPopUpMsg("Please Fill Currency Name");
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

        protected void btnBackUp_Click(object sender, EventArgs e)
        {
            Response.Redirect("CurrencyView.aspx");
        }

        protected void btnResetUp_Click(object sender, EventArgs e)
        {
            txtCurrency.Text = "";
        }

        protected void btnSaveUp_Click(object sender, EventArgs e)
        {
            btnSave_Click(null, null);
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("CurrencyView.aspx");
        }

        

    }
}