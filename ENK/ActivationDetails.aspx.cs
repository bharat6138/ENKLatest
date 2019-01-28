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

namespace ENK
{
    public partial class ActivationDetails : System.Web.UI.Page
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
                    string FromDate = txtFromDate.Text;
                    string ToDate = txtToDate.Text;
                    if (Request.QueryString["sim"] == null)
                    {
                        DataSet ds = svc.ShowDashBoardActivationDataService(distr, clnt, login, "activation", 0, 0, FromDate, ToDate);
                        if (ds != null)
                        {
                            if (ds.Tables.Count > 1)
                            {
                                rptActivation.DataSource = ds.Tables[0];
                                rptActivation.DataBind();
                            }
                            string currentmonthname = DateTime.Now.Month.ToString();
                            if (ds.Tables.Count > 1)
                            {
                                lblNumber.Text = "No. of activated sim for Current month - " + ds.Tables[1].Rows[0]["TotalActivationMSISDN"].ToString();
                            }
                        }
                    }
                    else if (Request.QueryString["sim"] != null)
                    {
                        if (Request.QueryString["sim"].ToString() == "p")
                        {
                            DataSet ds = svc.ShowDashBoardActivationDataService(distr, clnt, login, "Preloaded", 0, 0, FromDate, ToDate);
                            if (ds != null)
                            {
                                if (ds.Tables.Count > 1)
                                {
                                    rptActivation.DataSource = ds.Tables[0];
                                    rptActivation.DataBind();
                                }
                                string currentmonthname = DateTime.Now.Month.ToString();
                                if (ds.Tables.Count > 1)
                                {
                                    lblNumber.Text = "No. of Preloaded activated sim for Current month - " + ds.Tables[1].Rows[0]["TotalActivationMSISDN"].ToString();
                                }
                            }
                        }
                        if (Request.QueryString["sim"].ToString() == "T")
                        {
                            DataSet ds = svc.ShowDashBoardActivationDataService(distr, clnt, login, "Total", 0, 0, FromDate, ToDate);
                            if (ds != null)
                            {
                                if (ds.Tables.Count > 1)
                                {
                                    rptActivation.DataSource = ds.Tables[0];
                                    rptActivation.DataBind();
                                }
                                string currentmonthname = DateTime.Now.Month.ToString();
                                if (ds.Tables.Count > 1)
                                {
                                    lblNumber.Text = "Total Number of Activated sim - " + ds.Tables[1].Rows[0]["TotalActivation"].ToString();
                                }
                            }
                        }


                        if (Request.QueryString["sim"].ToString() == "L")
                        {
                            int month = System.DateTime.Now.Month;
                            int year = Convert.ToInt16(DateTime.Now.Year);
                            if (month != 1)
                            {
                                month = month - 1;
                            }
                            else
                            {
                                month = 12;
                                year = year - 1;
                            }
                            DataSet ds = svc.ShowDashBoardActivationDataService(distr, clnt, login, "activation", month, year, FromDate, ToDate);
                            if (ds != null)
                            {
                                if (ds.Tables.Count > 1)
                                {
                                    rptActivation.DataSource = ds.Tables[0];
                                    rptActivation.DataBind();
                                }
                                string currentmonthname = DateTime.Now.Month.ToString();
                                if (ds.Tables.Count > 1)
                                {
                                    lblNumber.Text = "No. of Preloaded activated sim for Previous month - " + ds.Tables[1].Rows[0]["TotalActivationMSISDN"].ToString();
                                }
                            }
                        }
                        if (Request.QueryString["sim"].ToString() == "PL")
                        {
                            int month = System.DateTime.Now.Month;
                            int year = Convert.ToInt16(DateTime.Now.Year);
                            if (month != 1)
                            {
                                month = month - 1;
                            }
                            else
                            {
                                month = 12;
                                year = year - 1;
                            }
                            DataSet ds = svc.ShowDashBoardActivationDataService(distr, clnt, login, "Preloaded", month, year, FromDate, ToDate);
                            if (ds != null)
                            {
                                if (ds.Tables.Count > 1)
                                {
                                    rptActivation.DataSource = ds.Tables[0];
                                    rptActivation.DataBind();
                                }
                                string currentmonthname = DateTime.Now.Month.ToString();
                                if (ds.Tables.Count > 1)
                                {
                                    lblNumber.Text = "No. of Preloaded activated sim for Previous month - " + ds.Tables[1].Rows[0]["TotalActivationMSISDN"].ToString();
                                }
                            }
                        }

                        if (Request.QueryString["sim"].ToString() == "LT")
                        {
                            int month = System.DateTime.Now.Month;
                            int year = Convert.ToInt16(DateTime.Now.Year);
                            if (month != 1)
                            {
                                month = month - 1;
                            }
                            else
                            {
                                month = 12;
                                year = year - 1;
                            }
                            DataSet ds = svc.ShowDashBoardActivationDataService(distr, clnt, login, "Total", month, year, FromDate, ToDate);
                            if (ds != null)
                            {
                                if (ds.Tables.Count > 1)
                                {
                                    rptActivation.DataSource = ds.Tables[0];
                                    rptActivation.DataBind();
                                }
                                string currentmonthname = DateTime.Now.Month.ToString();
                                if (ds.Tables.Count > 1)
                                {
                                    lblNumber.Text = "No. of Total Preloaded activated sim - " + ds.Tables[1].Rows[0]["TotalActivation"].ToString();
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {

                }
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
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            int month = 0;
            int year = 0;
            if (txtFromDate.Text != "" && txtToDate.Text == "")
            {
                ShowPopUpMsg("Please Select To Date");
            }
            if (txtFromDate.Text == "" && txtToDate.Text != "")
            {
                ShowPopUpMsg("Please Select From Date");
            }

            if (ddlMonth.SelectedItem.Text == "---Select Month---")
            {
                month = 0;
            }
            else
            {
                month = Convert.ToInt16(ddlMonth.SelectedValue);
            }
            string MonthYear = Convert.ToString(ddlMonth.SelectedItem);
            lblMonth.Text = MonthYear.Substring(0, 3);
            int distr = Convert.ToInt32(Session["DistributorID"]);
            int clnt = Convert.ToInt32(Session["ClientTypeID"]);
            int login = Convert.ToInt32(Session["LoginID"]);
            if (Request.QueryString["sim"] == null || Request.QueryString["sim"].ToString() == "L")
            {
                DataSet ds = svc.ShowDashBoardActivationDataService(distr, clnt, login, "activation", month, year, txtFromDate.Text, txtToDate.Text);
                if (ds != null)
                {
                    if (ds.Tables.Count > 1)
                    {
                        rptActivation.DataSource = ds.Tables[0];
                        rptActivation.DataBind();
                    }
                    if (ds.Tables.Count > 1)
                    {
                        lblNumber.Text = "No. of activated sim for " + lblMonth.Text + " - " + ds.Tables[1].Rows[0]["TotalActivationMSISDN"].ToString();
                    }
                }
            }
            else if (Request.QueryString["sim"] != null)
            {
                if (Request.QueryString["sim"].ToString() == "p" || Request.QueryString["sim"].ToString() == "PL")
                {
                    DataSet ds = svc.ShowDashBoardActivationDataService(distr, clnt, login, "Preloaded", month, year, txtFromDate.Text, txtToDate.Text);
                    if (ds != null)
                    {
                        if (ds.Tables.Count > 1)
                        {
                            rptActivation.DataSource = ds.Tables[0];
                            rptActivation.DataBind();
                        }
                        if (ds.Tables.Count > 1)
                        {
                            lblNumber.Text = "No. of Preloaded activated sim for " + lblMonth.Text + " - " + ds.Tables[1].Rows[0]["TotalActivationMSISDN"].ToString();
                        }
                    }

                }
                if (Request.QueryString["sim"].ToString() == "T" || Request.QueryString["sim"].ToString() == "LT")
                {
                    DataSet ds = svc.ShowDashBoardActivationDataService(distr, clnt, login, "Total", month, year, txtFromDate.Text, txtToDate.Text);
                    if (ds != null)
                    {
                        if (ds.Tables.Count > 1)
                        {
                            rptActivation.DataSource = ds.Tables[0];
                            rptActivation.DataBind();
                        }
                        if (ds.Tables.Count > 1)
                        {
                            lblNumber.Text = "Total Number of Activated sim -" + lblMonth.Text + " - " + ds.Tables[1].Rows[0]["TotalActivation"].ToString();
                        }
                    }
                }
            }
        }
    }
}
