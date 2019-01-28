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

namespace ENK
{
    public partial class Print : System.Web.UI.Page
    {
        Service1Client svc = new Service1Client();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Convert.ToString(Request.QueryString["URL"]) == "Recharge")
                {
                    btnRedirect.Text = "New Recharge";
                }
                if (Convert.ToString(Request.QueryString["URL"]) == "Activation")
                {
                    btnRedirect.Text = "Activate New Sim";
                }
                if (Convert.ToString(Request.QueryString["URL"]) == "Global Recharge")
                {
                    btnRedirect.Text = "New Global Recharge";
                }
                if (Convert.ToString(Request.QueryString["URL"]) == "H2O Recharge")
                {
                    btnRedirect.Text = "New H2O Recharge";
                }
                GetPrintRecipt();
            }
        }
        protected void GetPrintRecipt()
        {
            string tid = Convert.ToString(Request.QueryString["Tid"]);
            if (tid != "")
            {
                DataSet ds = null;
                ds = svc.GetPrintRecipt(tid);
                DataTable dtblprintDetails = new DataTable();
                if (ds != null && ds.Tables.Count > 1)
                {
                    dtblprintDetails = ds.Tables[0];
                    if (dtblprintDetails.Rows.Count > 0)
                    {
                        string PhnoFormate = "";
                        lblRetailerName.Text = Convert.ToString(dtblprintDetails.Rows[0][6]);
                        string Phone_Number = Convert.ToString(dtblprintDetails.Rows[0][0]);
                        if (Phone_Number.Length >= 10)
                        {
                            Phone_Number = Phone_Number.Substring(Phone_Number.Length - 10);
                            string PhoneNumber = Convert.ToString(Phone_Number);
                            PhnoFormate = Convert.ToInt64(PhoneNumber).ToString("(###)-###-####");
                        }
                        // string PhnoFormate = PhoneNumber.Format("{0:(###) ###-####}";
                        //string PhnoFormate = formatPhone(PhoneNumber);
                        lblMno.Text = PhnoFormate;
                        lblProduct.Text = Convert.ToString(dtblprintDetails.Rows[0][1]);
                        lblRefrenceNo.Text = Convert.ToString(dtblprintDetails.Rows[0][2]);
                        lblTransactionDate.Text = Convert.ToString(dtblprintDetails.Rows[0][3]);
                        lblRegulatoryFees.Text = Convert.ToString(dtblprintDetails.Rows[0][4]);
                        lblSalesAmount.Text = Convert.ToString(Convert.ToDecimal(dtblprintDetails.Rows[0][5]) + Convert.ToDecimal(dtblprintDetails.Rows[0][4]));
                    }
                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        if (Convert.ToString(ds.Tables[1].Rows[0][0]) != "")
                        {
                            lblInternationalCreditAmount.Text = Convert.ToString(Convert.ToDecimal(ds.Tables[1].Rows[0][0]));
                            lblSalesAmount.Text = Convert.ToString(Convert.ToDecimal(dtblprintDetails.Rows[0][5]) + Convert.ToDecimal(dtblprintDetails.Rows[0][4]) + Convert.ToDecimal(ds.Tables[1].Rows[0][0]));
                        }
                    }
                }
            }
        }

        protected void btnRedirect_Click(object sender, EventArgs e)
        {
            if (Convert.ToString(Request.QueryString["URL"]) == "Recharge")
            {
                Response.Redirect("LycaRecharge.aspx");
            }
            else if(Convert.ToString(Request.QueryString["URL"]) == "Activation")
            {
                Response.Redirect("ActivationSim.aspx");
            }
            else if (Convert.ToString(Request.QueryString["URL"]) == "Global Recharge")
            {
                Response.Redirect("GlobalRecharge.aspx");
            }
            else if (Convert.ToString(Request.QueryString["URL"]) == "H2O Recharge")
            {
                Response.Redirect("H2ORecharge.aspx");
            }
            
        }
    }
}