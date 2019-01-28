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
    public partial class IccidMsisdnSearch : System.Web.UI.Page
    {
        Service1Client ssc = new Service1Client();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    if (Session["LoginId"] != null)
                    {


                    }

                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void btnGet_Click(object sender, EventArgs e)
        {
            int LoginID = Convert.ToInt32(Session["LoginId"]);
            int distributorID = Convert.ToInt32(Session["DistributorID"]);
            int ClientTypeID = Convert.ToInt32(Session["ClientTypeID"]);
            if (txtSimNumber.Text != "")
            {
                DataSet ds = ssc.GetReportSIMHistory(distributorID, ClientTypeID, LoginID, txtSimNumber.Text);

                if (ds != null)
                {
                    RepeaterTransfer.DataSource = ds.Tables[0];
                    RepeaterTransfer.DataBind();
                }
            }
            else
            {
                ShowPopUpMsg("Please Fill SIM/MOBILE number");
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

        protected void btnExportToExcel_Click(object sender, EventArgs e)
        {
            int LoginID = Convert.ToInt32(Session["LoginId"]);
            int distributorID = Convert.ToInt32(Session["DistributorID"]);
            int ClientTypeID = Convert.ToInt32(Session["ClientTypeID"]);
            if (txtSimNumber.Text != "")
            {
                DataSet ds = ssc.GetReportSIMHistory(distributorID, ClientTypeID, LoginID, txtSimNumber.Text);

                if (ds != null)
                {
                    RepeaterTransfer.DataSource = ds.Tables[0];
                    RepeaterTransfer.DataBind();
                }

                //DataTable dtMain = new DataTable();
                if (ds != null)
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        DataView view = new DataView(dt);

                        DataTable dtExcel = view.ToTable("Selected", false, "SIMStatus", "MobileNumber", "SIMSerialNumber", "Plan", "CurrentDistributor", "TransferFrom", "TransferTo", "ActivationDate");//, "INTERNATIONAL_BUNDLE_CODE", "INTERNATIONAL_BUNDLE_AMOUNT"

                        if (dtExcel.Rows.Count > 0)
                        {
                            string filename = " SIMHistoryReport.xls";
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
}