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
using ENK.ServiceReference1;
using OfficeOpenXml;

namespace ENK
{
    public partial class CommissionReport : System.Web.UI.Page
    {
        Service1Client svc = new Service1Client();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                try
                {
                    int month = System.DateTime.Now.Month;
                   // month = month - 1;
                    DateTime dtDate = new DateTime(2000, month, 1);
                    string sMonthName = dtDate.ToString("MMM") + "-" + DateTime.Now.Year.ToString();
                    string sMonthFullName = dtDate.ToString("MMM");
                    lblMonth.Text = Convert.ToString(sMonthFullName);
                    int distrID = Convert.ToInt32(Session["DistributorID"]);

                    DataSet ds = svc.GetCommissionDetail(distrID, 0, 0,"");

                    

                    if (ds != null)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {

                            ViewState["dtpgld"] = ds.Tables[0];
                            grdCommission.DataSource = ds.Tables[0];
                            grdCommission.DataBind();

                        }
                        if (ds.Tables.Count > 1)
                        {
                            if (ds.Tables[1].Rows.Count > 0)
                            {
                                lblAmount.Text = ds.Tables[1].Rows[0]["CommissionAmount"].ToString();
                            }
                        }
                        else
                        {
                            lblAmount.Text = "";
                        }
                        ////------////
                        //if (ds.Tables.Count > 2)
                        //{
                        //    if (ds.Tables[2].Rows.Count > 0)
                        //    {

                        //        ddlMonth.DataSource = ds.Tables[2];
                        //        ddlMonth.DataValueField = "Month";
                        //        ddlMonth.DataBind();
                        //        ddlMonth.Items.Insert(0, new ListItem("---Select Month---", "0"));

                        //        ddlYear.DataSource = ds.Tables[2];
                        //        ddlYear.DataValueField = "Year";
                        //        ddlYear.DataBind();
                        //        ddlYear.Items.Insert(0, new ListItem("---Select Year---", "0"));
                        //    }
                        //}
                        ////--////
                    }
                }
                catch (Exception ex)
                {

                }
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            int distrID = Convert.ToInt32(Session["DistributorID"]);
            int month = 0;
            int year = 0;


            //if(ddlMonth.SelectedItem.Text == "---Select Month---")
            //{
            //    month = 0;
            //}
            //else
            //{
            //    month = 0;
            //}

            //if (ddlYear.SelectedItem.Text == "---Select Year---")
            //{
            //    year = 0;
            //}
            //else
            //{
            //    year = 0;
            //}

            //string MonthYear = Convert.ToString(ddlMonth.SelectedItem);
            string monthyr = txtMonthYr.Text.Trim();
            month = Convert.ToInt16(monthyr.Substring(0, 2));
            year = Convert.ToInt16(monthyr.Substring(3, 4));

            DateTime dtDate = new DateTime(2000, month, 1);
            string sMonthName = dtDate.ToString("MMM") + "-" + DateTime.Now.Year.ToString();
            string sMonthFullName = dtDate.ToString("MMM");
            lblMonth.Text = Convert.ToString(sMonthFullName);

            DataSet ds = svc.GetCommissionDetail(distrID, month, year, "");
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    //Ankit singh(change repeater to grid) 
                    ViewState["dtpgld"] = ds.Tables[0];
                    grdCommission.DataSource = ds.Tables[0];
                    grdCommission.DataBind();
                    //
                }
                if (ds.Tables.Count > 1)
                {
                    lblAmount.Text = ds.Tables[1].Rows[0]["CommissionAmount"].ToString();
                }
                else
                {
                    lblAmount.Text = "";
                }
            }
        }

        protected void btnExportToExcel_Click(object sender, EventArgs e)
        {
            //Response.Clear();

            //Response.AddHeader("content-disposition", "attachment;filename = CommissionReport.xls");

            //Response.ContentType = "application/vnd.xls";

            //System.IO.StringWriter stringWrite = new System.IO.StringWriter();

            //System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);

            //grdCommission.RenderControl(htmlWrite);

            //Response.Write(stringWrite.ToString());

            //Response.End();


            ExcelPackage excel = new ExcelPackage();
            var workSheet = excel.Workbook.Worksheets.Add("Sheet1");
            workSheet.TabColor = System.Drawing.Color.Black;
            workSheet.DefaultRowHeight = 12;
            DataTable dtexport = (DataTable)ViewState["dtpgld"];
            workSheet.Cells["A1"].LoadFromDataTable(dtexport, true);
            workSheet.Cells["A1:A25"].Style.Numberformat.Format = "@";
            string excelName = "CommissionReport";
            using (var memoryStream = new MemoryStream())
            {
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment; filename=" + excelName + ".xlsx");
                excel.SaveAs(memoryStream);
                memoryStream.WriteTo(Response.OutputStream);
                Response.Flush();
                Response.End();

            }
        }

        //Ankit singh
        //To remove requirement that grid must be in form tag
        public override void VerifyRenderingInServerForm(Control control)
        {
            return;
        }
    }
}