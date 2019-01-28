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
    public partial class RoleList : System.Web.UI.Page
    {
        Service1Client svc = new Service1Client();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["LoginID"] != null)
            {
                if (!Page.IsPostBack)
                {
                    if (Session["ClientType"].ToString() == "Company")
                    {


                    }
                    if (Session["ClientType"].ToString() == "Distributor")
                    {
                        divNew.Visible = false;
                    }

                }
            }
            if (!IsPostBack)
            {
                FillRole();
            }
        }

        public void FillRole()
        {
            try
            {
                DataSet ds = svc.GetRoleService();
                if (ds != null)
                {
                    grdRole.DataSource = ds;
                    grdRole.DataBind();
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void grdRole_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "View")
            {
                int id = Convert.ToInt32(e.CommandArgument);
                //string idnty = Encryption.Encrypt(id.ToString());
                //string condition = Encryption.Encrypt("View");
                string condition = "v";
                Response.Redirect("~/Role.aspx?Id=" + id + "&ac=" + condition, false);
            }
            if (e.CommandName == "RowEdit")
            {
                int id = Convert.ToInt32(e.CommandArgument);
               // string idnty = Encryption.Encrypt(id.ToString());
                //string condition = Encryption.Encrypt("Edit");
                string condition = "e";
                Response.Redirect("~/Role.aspx?Id=" + id + "&ac=" + condition, false);
            }
        }

      
    }
}