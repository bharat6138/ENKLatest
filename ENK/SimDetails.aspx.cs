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
using System.Data.SqlClient;
using ENK.ServiceReference1;

namespace ENK
{
    public partial class SimDetails : System.Web.UI.Page
    {
        Service1Client svc = new Service1Client();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                try
                {
                    int distr = Convert.ToInt32(Session["DistributorID"]);
                    int clnt = Convert.ToInt32(Session["ClientTypeID"]);
                    int login = Convert.ToInt32(Session["LoginID"]);
                    DataSet ds = svc.ShowDashBoardActivationDataService(distr, clnt, login, "available",0,0,"","");
                    int Count = 0;
                    if (ds != null)
                    {
                        if (ds.Tables.Count >= 2)
                        {
                            if (Request.QueryString["sim"] == null)
                            {
                                rptBlankSim.DataSource = ds.Tables[1];
                                rptBlankSim.DataBind();
                                Count = ds.Tables[1].Rows.Count;
                            }

                            else if (Request.QueryString["sim"] != null)
                            {
                                if (Request.QueryString["sim"].ToString() == "p")
                                {
                                    rptBlankSim.DataSource = ds.Tables[2];
                                    rptBlankSim.DataBind();
                                    Count = ds.Tables[2].Rows.Count;
                                }
                            }
                        }
                    }
                    lblCount.Text = Convert.ToString(Count) + " SIMs";
                }
                catch (Exception ex)
                {

                }
            }
        }
    }
}