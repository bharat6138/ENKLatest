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
    public partial class VendorView : System.Web.UI.Page
    {
        Service1Client svc = new Service1Client();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
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

                    int distributorID = Convert.ToInt32(Session["DistributorID"]);
                    DataSet ds = svc.GetVendorListService();
                    if (ds != null)
                    {
                        rptVendor.DataSource = ds.Tables[0];
                        rptVendor.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
 
            }
        }

        protected void rptVendor_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "View")
                {
                    int id = Convert.ToInt32(e.CommandArgument);
                    string idnty = Encryption.Encrypt(id.ToString());
                    string condition = Encryption.Encrypt("View");
                    Response.Redirect("~/Vendor.aspx?Identity=" + idnty + "&Condition=" + condition, false);
                }
                if (e.CommandName == "RowEdit")
                {
                    int id = Convert.ToInt32(e.CommandArgument);
                    string idnty = Encryption.Encrypt(id.ToString());
                    string condition = Encryption.Encrypt("Edit");
                    Response.Redirect("~/Vendor.aspx?Identity=" + idnty + "&Condition=" + condition, false);
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}