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
using System.Net.Mail;
using System.Configuration;
using ENK.ServiceReference1;
using ENK.net.emida.ws;
using ENK.LycaAPI;

namespace ENK
{
    public partial class RechargeActivationCancelReport : System.Web.UI.Page
    {
        Service1Client svc = new Service1Client();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
                txtToDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
                BindCancelReport();
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



        protected void btnGetReport_Click(object sender, EventArgs e)
        {

            BindCancelReport();
            //DataSet ds = new DataSet();
            //if (txtDate.Text != "")
            //{
            //    ds = svc.GetRechargeActivationCancelReport(Convert.ToDateTime(txtDate.Text));
            //    if (ds.Tables[0].Rows.Count > 0)
            //    {
            //        RepeaterCancelReport.DataSource = ds.Tables[0];
            //        RepeaterCancelReport.DataBind();
            //    }

            //    else
            //    {
            //        ShowPopUpMsg("Cancellation details are not available on this date");
            //    }
            //}
            //else
            //{
            //    ShowPopUpMsg("Please Select Date");
            //}
        }

        public void BindCancelReport()
        {
            DataSet ds = new DataSet();
                if (txtDate.Text != "" && txtToDate.Text != "")
                {
                    ds = svc.GetRechargeActivationCancelReport(Convert.ToDateTime(txtDate.Text), Convert.ToDateTime(txtToDate.Text));
                    if (ds != null)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            RepeaterCancelReport.DataSource = ds.Tables[0];
                            RepeaterCancelReport.DataBind();
                        }

                        else
                        {
                            ShowPopUpMsg("Cancellation details are not available on this date");
                        }
                    }
                }
                else
                {
                    ShowPopUpMsg("Please Select the Date period");
                }
        }
    }
}