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
    public partial class POSView : System.Web.UI.Page
    {
        Service1Client ssc = new Service1Client();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                int distr = Convert.ToInt32(Session["DistributorID"]);
                int clnt = Convert.ToInt32(Session["ClientTypeID"]);
                DataSet ds = ssc.ShowPOSService(distr, clnt);
                if (ds != null)
                {
                    rptPOS.DataSource = ds.Tables[0];
                    rptPOS.DataBind();
                }
            }
            catch (Exception ex)
            {
 
            }
        }
    }
}