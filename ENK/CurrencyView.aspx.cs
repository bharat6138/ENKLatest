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
    public partial class CurrencyView : System.Web.UI.Page
    {
        Service1Client svc = new Service1Client();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
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
                int clienttypeid = Convert.ToInt32(Session["ClientTypeID"]);
                DataSet ds = svc.GetCurrencyService(distributorID, clienttypeid);

                if (ds != null)
                {
                    rptCurrency.DataSource = ds;
                    rptCurrency.DataBind();
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}