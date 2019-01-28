using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ENK
{
    public partial class Reports : System.Web.UI.Page
    {
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
    }
}