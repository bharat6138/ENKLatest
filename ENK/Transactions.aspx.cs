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
    public partial class Transactions : System.Web.UI.Page
    {

        Service1Client ssc = new Service1Client();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtFromDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
                txtToDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");

                FillRepeater();
            }
        }


        public void FillRepeater()
        {

            if (txtFromDate.Text != "" && txtToDate.Text != "")
            {
                DataSet ds = new DataSet();
                ds = ssc.GetTransactionReport(Convert.ToDateTime(txtFromDate.Text), Convert.ToDateTime(txtToDate.Text));

                if (ds.Tables[0].Rows.Count > 0)
                {
                    RepeaterData.DataSource = ds.Tables[0];
                    RepeaterData.DataBind();
                }

                else
                {
                    ShowPopUpMsg("Data not available");
                }

            }

            else
            {
                ShowPopUpMsg("Please select Date Range");
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

        protected void btnGet_Click(object sender, EventArgs e)
        {
            FillRepeater();
        }

        protected void btnExportToExcel_Click(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            //DateTime FromDate = Convert.ToDateTime(txtFromDate.Text.Trim());
            //DateTime ToDate = Convert.ToDateTime(txtToDate.Text.Trim());


            ds = ssc.GetTransactionReport(Convert.ToDateTime(txtFromDate.Text), Convert.ToDateTime(txtToDate.Text));
            //DataTable dtMain = new DataTable();
            if (ds != null)
            {
                DataTable dt = ds.Tables[0];

               

                if (dt.Rows.Count > 0)
                {
                    DataView view = new DataView(dt);

                    DataTable dtExcel = view.ToTable("Selected", false, "activationid", "TxnType", "transactionid", "MSISDN", "iccid", "bundlemonth", "activationdate", "distributorid", "bundle", "bundlecount");

                    if (dtExcel.Rows.Count > 0)
                    {
                        string filename = "TransactionReport.xls";
                        System.IO.StringWriter tw = new System.IO.StringWriter();
                        System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);
                        GridView grdView = new GridView();
                        //dgGrid.HeaderStyle
                        grdView.DataSource = dtExcel;
                        grdView.DataBind();

                        ScriptManager.RegisterStartupScript(this, GetType(), "", "HideProgress();", true);
                        //Get the HTML for the control.
                        grdView.RenderControl(hw);
                        //Write the HTML back to the browser.
                        //Response.ContentType = application/vnd.ms-excel;
                        Response.ContentType = "application/vnd.ms-excel";
                        Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename + "");
                        this.EnableViewState = false;
                        Response.Write(tw.ToString());
                        Response.End();

                        ScriptManager.RegisterStartupScript(this, GetType(), "", "HideProgress();", true);
                    }

                }
            }
        }


    }
}