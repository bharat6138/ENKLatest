﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using ENK.ServiceReference1;
using System.Web.UI.HtmlControls;

namespace ENK
{
    public partial class ActivationLegderReport : System.Web.UI.Page
    {
        Service1Client ssc = new Service1Client();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                txtFromDate.Attributes.Add("readonly", "true");
                txtToDate.Attributes.Add("readonly", "true");

                if (!Page.IsPostBack)
                {
                    if (Session["LoginId"] != null)
                    {
                        int LoginID = Convert.ToInt32(Session["LoginId"]);
                        int distributorID = Convert.ToInt32(Session["DistributorID"]);
                        int ClientTypeID = Convert.ToInt32(Session["ClientTypeID"]);

                        DateTime today = DateTime.Today;
                        int numberOfDaysInMonth = DateTime.DaysInMonth(today.Year, today.Month);

                        DateTime FromDate = today.AddDays(-3);
                        DateTime ToDate = new DateTime(today.Year, today.Month, numberOfDaysInMonth);

                        txtFromDate.Text = Convert.ToString(FromDate.ToString("dd-MMM-yyyy"));
                        txtToDate.Text = Convert.ToString(ToDate.ToString("dd-MMM-yyyy"));

                        AdjustLevel(distributorID);

                    }
                }
            }
            catch (Exception ex)
            {
            }
        }
        protected void btnGet_Click(object sender, EventArgs e)
        {
            DateTime FromDate;
            DateTime ToDate;

            int DistributorID = Convert.ToInt32(Session["DistributorID"]);
            int ClientTypeID = Convert.ToInt32(Session["ClientTypeID"]);
            int LoginID = Convert.ToInt32(Session["LoginId"]);

            if (txtFromDate.Text.Trim() != "" && txtToDate.Text.Trim() != "")
            {
                FromDate = Convert.ToDateTime(txtFromDate.Text.Trim());
                ToDate = Convert.ToDateTime(txtToDate.Text.Trim());

                DataSet ds;
                if (ddlLevel1.SelectedValue == "0")
                {
                    ds = ssc.GetReportActivationLedger(DistributorID, 1, LoginID, FromDate, ToDate, DistributorID);


                }
                else
                {
                    ds = ssc.GetReportActivationLedger(Convert.ToInt32(ViewState["Dis"]), 0, LoginID, FromDate, ToDate, DistributorID);

                }

                if (ds != null)
                {
                    ViewState["SIMPurchase"] = ds.Tables[0];
                    grdTransfer.DataSource = ds.Tables[0];
                    grdTransfer.DataBind();
                    lblCount.Text = ds.Tables[0].Rows.Count.ToString();
                }
            }
        }
        protected void btnExportToExcel_Click(object sender, EventArgs e)
        {
            Response.Clear();

            Response.AddHeader("content-disposition", "attachment;filename = ActivationLegderReport.xls");

            Response.ContentType = "application/vnd.xls";

            System.IO.StringWriter stringWrite = new System.IO.StringWriter();

            System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);

            grdTransfer.RenderControl(htmlWrite);

            Response.Write(stringWrite.ToString());

            Response.End();

        }
        //Ankit singh
        //To remove requirement that grid must be in form tag
        public override void VerifyRenderingInServerForm(Control control)
        {
            return;
        }
        protected void Repeater1_ItemDataBound1(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Header)
            {
                Repeater headerRepeater = e.Item.FindControl("Header1") as Repeater;
                headerRepeater.DataSource = ((DataTable)ViewState["SIMPurchase"]).Columns;
                headerRepeater.DataBind();
            }

            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Repeater columnRepeater = e.Item.FindControl("Item1") as Repeater;
                var row = e.Item.DataItem as System.Data.DataRowView;
                columnRepeater.DataSource = row.Row.ItemArray;
                columnRepeater.DataBind();
            }
        }
        public void AdjustLevel(Int32 distributorID)
        {
            if (distributorID == 1)
            {
                BindLevel1Distributor();
                ddlLevel2.Attributes.Add("disabled", "disabled");
                ddlLevel3.Attributes.Add("disabled", "disabled");
                ddlLevel4.Attributes.Add("disabled", "disabled");
                ddlLevel5.Attributes.Add("disabled", "disabled");
            }
            else
            {
                DataSet ds = ssc.GETLEVELDistributor(distributorID, 1);
                int Level = ds.Tables[1].Rows.Count;
                if (Level == 0)
                {
                    //Company Level
                }
                else if (Level == 1)
                {
                    ddlLevel1.DataTextField = "name";
                    ddlLevel1.DataValueField = "id";
                    ddlLevel1.DataSource = ds.Tables[1];
                    ddlLevel1.DataBind();

                    ddlLevel1.SelectedValue = Convert.ToString(ds.Tables[1].Rows[0]["id"]);

                    ddlLevel1CHange();

                    ViewState["Dis"] = Convert.ToInt32(ddlLevel1.SelectedValue);

                    ddlLevel1.Attributes.Add("disabled", "disabled");

                    LEVEL1.Text = "Distributor";
                    LEVEL2.Text = "Sub-Level1";
                    LEVEL3.Text = "Sub-Level2";
                    LEVEL4.Text = "Sub-Level3";
                    LEVEL5.Text = "Sub-Level4";
                }
                else if (Level == 2)
                {
                    ddlLevel1.DataTextField = "name";
                    ddlLevel1.DataValueField = "id";
                    ddlLevel1.DataSource = ds.Tables[1];
                    ddlLevel1.DataBind();

                    ddlLevel2.DataTextField = "name";
                    ddlLevel2.DataValueField = "id";
                    ddlLevel2.DataSource = ds.Tables[1];
                    ddlLevel2.DataBind();

                    ddlLevel1.SelectedValue = Convert.ToString(ds.Tables[1].Rows[0]["id"]);
                    ddlLevel2.SelectedValue = Convert.ToString(ds.Tables[1].Rows[1]["id"]);

                    ddlLevel2CHange();

                    ViewState["Dis"] = Convert.ToInt32(ddlLevel2.SelectedValue);

                    ddlLevel1.Attributes.Add("disabled", "disabled");
                    ddlLevel2.Attributes.Add("disabled", "disabled");

                    ddlLevel1.Visible = false;

                    LEVEL1.Visible = false;
                    LEVEL2.Text = "Distributor";
                    LEVEL3.Text = "Sub-Level1";
                    LEVEL4.Text = "Sub-Level2";
                    LEVEL5.Text = "Sub-Level3";
                }                          
                else if (Level == 3)
                {
                    ddlLevel1.DataTextField = "name";
                    ddlLevel1.DataValueField = "id";
                    ddlLevel1.DataSource = ds.Tables[1];
                    ddlLevel1.DataBind();

                    ddlLevel2.DataTextField = "name";
                    ddlLevel2.DataValueField = "id";
                    ddlLevel2.DataSource = ds.Tables[1];
                    ddlLevel2.DataBind();

                    ddlLevel3.DataTextField = "name";
                    ddlLevel3.DataValueField = "id";
                    ddlLevel3.DataSource = ds.Tables[1];
                    ddlLevel3.DataBind();

                    ddlLevel1.SelectedValue = Convert.ToString(ds.Tables[1].Rows[0]["id"]);
                    ddlLevel2.SelectedValue = Convert.ToString(ds.Tables[1].Rows[1]["id"]);
                    ddlLevel3.SelectedValue = Convert.ToString(ds.Tables[1].Rows[2]["id"]);

                    ddlLevel3CHange();

                    ViewState["Dis"] = Convert.ToInt32(ddlLevel3.SelectedValue);

                    ddlLevel1.Attributes.Add("disabled", "disabled");
                    ddlLevel2.Attributes.Add("disabled", "disabled");
                    ddlLevel3.Attributes.Add("disabled", "disabled");

                    ddlLevel1.Visible = false;
                    ddlLevel2.Visible = false;

                    LEVEL1.Visible = false;
                    LEVEL2.Visible = false;
                    LEVEL3.Text = "Distributor";
                    LEVEL4.Text = "Sub-Level1";
                    LEVEL5.Text = "Sub-Level2";
                }
                else if (Level == 4)
                {
                    ddlLevel1.DataTextField = "name";
                    ddlLevel1.DataValueField = "id";
                    ddlLevel1.DataSource = ds.Tables[1];
                    ddlLevel1.DataBind();

                    ddlLevel2.DataTextField = "name";
                    ddlLevel2.DataValueField = "id";
                    ddlLevel2.DataSource = ds.Tables[1];
                    ddlLevel2.DataBind();

                    ddlLevel3.DataTextField = "name";
                    ddlLevel3.DataValueField = "id";
                    ddlLevel3.DataSource = ds.Tables[1];
                    ddlLevel3.DataBind();

                    ddlLevel4.DataTextField = "name";
                    ddlLevel4.DataValueField = "id";
                    ddlLevel4.DataSource = ds.Tables[1];
                    ddlLevel4.DataBind();

                    ddlLevel1.SelectedValue = Convert.ToString(ds.Tables[1].Rows[0]["id"]);
                    ddlLevel2.SelectedValue = Convert.ToString(ds.Tables[1].Rows[1]["id"]);
                    ddlLevel3.SelectedValue = Convert.ToString(ds.Tables[1].Rows[2]["id"]);
                    ddlLevel4.SelectedValue = Convert.ToString(ds.Tables[1].Rows[3]["id"]);

                    ddlLevel4CHange();

                    ViewState["Dis"] = Convert.ToInt32(ddlLevel4.SelectedValue);

                    ddlLevel1.Attributes.Add("disabled", "disabled");
                    ddlLevel2.Attributes.Add("disabled", "disabled");
                    ddlLevel3.Attributes.Add("disabled", "disabled");
                    ddlLevel4.Attributes.Add("disabled", "disabled");

                    ddlLevel1.Visible = false;
                    ddlLevel2.Visible = false;
                    ddlLevel3.Visible = false;

                    LEVEL1.Visible = false;
                    LEVEL2.Visible = false;
                    LEVEL3.Visible = false;
                    LEVEL4.Text = "Distributor";
                    LEVEL5.Text = "Sub-Level1";
                }

                else if (Level == 5)
                {
                    ddlLevel1.DataTextField = "name";
                    ddlLevel1.DataValueField = "id";
                    ddlLevel1.DataSource = ds.Tables[1];
                    ddlLevel1.DataBind();

                    ddlLevel2.DataTextField = "name";
                    ddlLevel2.DataValueField = "id";
                    ddlLevel2.DataSource = ds.Tables[1];
                    ddlLevel2.DataBind();

                    ddlLevel3.DataTextField = "name";
                    ddlLevel3.DataValueField = "id";
                    ddlLevel3.DataSource = ds.Tables[1];
                    ddlLevel3.DataBind();

                    ddlLevel4.DataTextField = "name";
                    ddlLevel4.DataValueField = "id";
                    ddlLevel4.DataSource = ds.Tables[1];
                    ddlLevel4.DataBind();

                    ddlLevel5.DataTextField = "name";
                    ddlLevel5.DataValueField = "id";
                    ddlLevel5.DataSource = ds.Tables[1];
                    ddlLevel5.DataBind();

                    ddlLevel1.SelectedValue = Convert.ToString(ds.Tables[1].Rows[0]["id"]);
                    ddlLevel2.SelectedValue = Convert.ToString(ds.Tables[1].Rows[1]["id"]);
                    ddlLevel3.SelectedValue = Convert.ToString(ds.Tables[1].Rows[2]["id"]);
                    ddlLevel4.SelectedValue = Convert.ToString(ds.Tables[1].Rows[3]["id"]);
                    ddlLevel5.SelectedValue = Convert.ToString(ds.Tables[1].Rows[4]["id"]);


                    ViewState["Dis"] = Convert.ToInt32(ddlLevel5.SelectedValue);

                    ddlLevel1.Attributes.Add("disabled", "disabled");
                    ddlLevel2.Attributes.Add("disabled", "disabled");
                    ddlLevel3.Attributes.Add("disabled", "disabled");
                    ddlLevel4.Attributes.Add("disabled", "disabled");
                    ddlLevel5.Attributes.Add("disabled", "disabled");

                    ddlLevel1.Visible = false;
                    ddlLevel2.Visible = false;
                    ddlLevel3.Visible = false;
                    ddlLevel4.Visible = false;

                    LEVEL1.Visible = false;
                    LEVEL2.Visible = false;
                    LEVEL3.Visible = false;
                    LEVEL4.Visible = false;
                    LEVEL5.Text = "Distributor";
                }

            }
        }
        public void BindLevel1Distributor()
        {
            if (Convert.ToInt32(Session["DistributorID"]) == 1)
            {
                DataSet ds = ssc.GETLEVELDistributor(0, 0);
                if (ds != null)
                {
                    ddlLevel1.DataSource = ds.Tables[0];
                    ddlLevel1.DataValueField = "id";
                    ddlLevel1.DataTextField = "Name";
                    ddlLevel1.DataBind();
                    ddlLevel1.Items.Insert(0, new ListItem("ALL", "0"));
                    ViewState["Dis"] = 1;
                }
            }
        }
        public void ddlLevel1CHange()
        {
            {
                DataSet ds = ssc.GETLEVELDistributor(Convert.ToInt32(ddlLevel1.SelectedValue), 1);
                if (ds != null)
                {
                    ddlLevel2.Attributes.Remove("disabled");
                    ddlLevel2.DataValueField = "id";
                    ddlLevel2.DataSource = ds.Tables[0];
                    ddlLevel2.DataTextField = "Name";
                    ddlLevel2.DataBind();
                    ddlLevel2.Items.Insert(0, new ListItem("ALL", "0"));

                }
            }
        }
        public void ddlLevel2CHange()
        {
            {
                DataSet ds = ssc.GETLEVELDistributor(Convert.ToInt32(ddlLevel2.SelectedValue), 2);
                if (ds != null)
                {
                    ddlLevel3.Attributes.Remove("disabled");
                    ddlLevel3.DataSource = ds.Tables[0];
                    ddlLevel3.DataValueField = "id";
                    ddlLevel3.DataTextField = "Name";
                    ddlLevel3.DataBind();
                    ddlLevel3.Items.Insert(0, new ListItem("ALL", "0"));
                }
            }
        }
        public void ddlLevel3CHange()
        {
            {
                DataSet ds = ssc.GETLEVELDistributor(Convert.ToInt32(ddlLevel3.SelectedValue), 3);
                if (ds != null)
                {
                    ddlLevel4.Attributes.Remove("disabled");
                    ddlLevel4.DataSource = ds.Tables[0];
                    ddlLevel4.DataValueField = "id";
                    ddlLevel4.DataTextField = "Name";
                    ddlLevel4.DataBind();
                    ddlLevel4.Items.Insert(0, new ListItem("ALL", "0"));
                }
            }
        }
        public void ddlLevel4CHange()
        {
            {
                DataSet ds = ssc.GETLEVELDistributor(Convert.ToInt32(ddlLevel4.SelectedValue), 4);
                if (ds != null)
                {
                    ddlLevel5.Attributes.Remove("disabled");
                    ddlLevel5.DataSource = ds.Tables[0];
                    ddlLevel5.DataValueField = "id";
                    ddlLevel5.DataTextField = "Name";
                    ddlLevel5.DataBind();
                    ddlLevel5.Items.Insert(0, new ListItem("ALL", "0"));
                }
            }
        }
        protected void ddlLevel1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlLevel1CHange();
            ViewState["Dis"] = Convert.ToInt32(ddlLevel1.SelectedValue);
            if (Convert.ToInt32(ddlLevel1.SelectedValue) == 0)
            {
                ddlLevel2.SelectedValue = "0";
                ddlLevel2.Attributes.Add("disabled", "disabled");
            }
            else
            {
            }
            ddlLevel3.SelectedValue = "0";
            ddlLevel3.Attributes.Add("disabled", "disabled");
            ddlLevel4.SelectedValue = "0";
            ddlLevel4.Attributes.Add("disabled", "disabled");
            ddlLevel5.SelectedValue = "0";
            ddlLevel5.Attributes.Add("disabled", "disabled");
        }
        protected void ddlLevel2_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlLevel2CHange();
            if (Convert.ToInt32(ddlLevel2.SelectedValue) == 0)
            {
                ViewState["Dis"] = Convert.ToString(ddlLevel1.SelectedValue);
                ddlLevel3.SelectedValue = "0";
                ddlLevel3.Attributes.Add("disabled", "disabled");
              
            }
            else
            {
                ViewState["Dis"] = Convert.ToInt32(ddlLevel2.SelectedValue);
            }
            ddlLevel4.SelectedValue = "0";
            ddlLevel4.Attributes.Add("disabled", "disabled");
            ddlLevel5.SelectedValue = "0";
            ddlLevel5.Attributes.Add("disabled", "disabled");
        }
        protected void ddlLevel3_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlLevel3CHange();
            if (Convert.ToInt32(ddlLevel3.SelectedValue) == 0)
            {
                ViewState["Dis"] = Convert.ToString(ddlLevel2.SelectedValue);
                ddlLevel4.SelectedValue = "0";
                ddlLevel4.Attributes.Add("disabled", "disabled");              
            }
            else
            {
                ViewState["Dis"] = Convert.ToInt32(ddlLevel3.SelectedValue);
            }
            ddlLevel5.SelectedValue = "0";
            ddlLevel5.Attributes.Add("disabled", "disabled");
        }
        protected void ddlLevel4_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlLevel4CHange();
            if (Convert.ToInt32(ddlLevel4.SelectedValue) == 0)
            {
                ViewState["Dis"] = Convert.ToString(ddlLevel3.SelectedValue);

                ddlLevel5.SelectedValue = "0";
                ddlLevel5.Attributes.Add("disabled", "disabled");
            }
            else
            {
                ViewState["Dis"] = Convert.ToInt32(ddlLevel4.SelectedValue);
            }
        }
        protected void ddlLevel5_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(ddlLevel5.SelectedValue) == 0)
            {
                ViewState["Dis"] = Convert.ToString(ddlLevel4.SelectedValue);
            }
            else
            {
                ViewState["Dis"] = Convert.ToInt32(ddlLevel5.SelectedValue);
            }
        }

        protected void GridView_PreRender(object sender, EventArgs e)
        {
            GridView gv = (GridView)sender;

            if ((gv.ShowHeader == true && gv.Rows.Count > 0)
                || (gv.ShowHeaderWhenEmpty == true))
            {
                //Force GridView to use <thead> instead of <tbody> - 11/03/2013 - MCR.
                gv.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
            if (gv.ShowFooter == true && gv.Rows.Count > 0)
            {
                //Force GridView to use <tfoot> instead of <tbody> - 11/03/2013 - MCR.
                gv.FooterRow.TableSection = TableRowSection.TableFooter;
            }

        }

    }
}