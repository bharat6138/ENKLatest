using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;
using ENK.ServiceReference1;

namespace ENK
{
    public partial class Createplans : System.Web.UI.Page
    {
        Service1Client svc = new Service1Client();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["LoginID"] != null)
            {
                if (!Page.IsPostBack)
                {

                    if (Session["ClientType"].ToString() == "Company")
                    {


                    }
                    if (Session["ClientType"].ToString() == "Distributor")
                    {
                        divNew.Visible = false;


                    }

                    FillTariff();
                    DDL();
                    if (ddlNetwork.Items.Count == 2)
                    {
                        ddlNetwork.SelectedIndex = 1;
                        ddlNetwork_SelectedIndexChanged(null, null);
                        ddlNetwork.Enabled = false;
                    }
                }
            }
        }


        private void DDL()
        {

            DataTable dt = svc.GetVendor(Convert.ToInt32(Session["LoginId"]));
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    ddlNetwork.DataSource = dt;
                    ddlNetwork.DataValueField = "VendorID";
                    ddlNetwork.DataTextField = "VendorName";
                    ddlNetwork.DataBind();

                    ddlNetwork.Items.Insert(0, new ListItem("All", "0"));
                }

                ddlNetwork.SelectedValue = "13";
                ddlNetwork_SelectedIndexChanged(null, null);
            }

        }

        public void FillTariff()
        {
            try
            {
                int userid = Convert.ToInt32(Session["LoginID"]);

                int DistributorID = Convert.ToInt32(Session["DistributorID"]);
                DataSet dst = svc.GetTariffService(userid, DistributorID);

                ViewState["Tariff"] = dst.Tables[4];


                if (dst != null)
                {
                    if (Session["ClientType"].ToString() == "Company")
                    {

                        RpTariff.DataSource = dst.Tables[3];
                        RpTariff.DataBind();
                    }
                    if (Session["ClientType"].ToString() == "Distributor")
                    {
                        rptTariffDist.Visible = true;
                        rptTariffDist.DataSource = dst.Tables[3];
                        rptTariffDist.DataBind();
                        RpTariff.Visible = false;
                    }

                    // ddlNetwork_SelectedIndexChanged(null, null);

                }
                else
                {

                }



            }
            catch (Exception ex)
            {

            }
        }
        protected void ddlNetwork_SelectedIndexChanged(object sender, EventArgs e)
        {

            try
            {
                DataTable dtSIMPurchase = new DataTable();

                if (ddlNetwork.SelectedIndex > 0)
                {

                    dtSIMPurchase = (DataTable)ViewState["Tariff"];
                    DataView dv = dtSIMPurchase.DefaultView;
                    dv.RowFilter = "NetworkID = " + Convert.ToInt32(ddlNetwork.SelectedValue);
                    dtSIMPurchase = dv.ToTable();
                    if (Session["ClientType"].ToString() == "Company")
                    {
                        RpTariff.DataSource = dtSIMPurchase;
                        RpTariff.DataBind();
                    }
                    if (Session["ClientType"].ToString() == "Distributor")
                    {
                        rptTariffDist.DataSource = dtSIMPurchase;
                        rptTariffDist.DataBind();
                        RpTariff.Visible = false;
                        rptTariffDist.Visible = true;
                    }

                }
                else
                {
                    dtSIMPurchase = (DataTable)ViewState["Tariff"];
                    if (Session["ClientType"].ToString() == "Company")
                    {
                        RpTariff.DataSource = dtSIMPurchase;
                        RpTariff.DataBind();
                    }

                    if (Session["ClientType"].ToString() == "Distributor")
                    {
                        rptTariffDist.DataSource = dtSIMPurchase;
                        rptTariffDist.DataBind();
                        RpTariff.Visible = false;
                        rptTariffDist.Visible = true;

                    }



                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        protected void RpTariff_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "View")
                {
                    int id = Convert.ToInt32(e.CommandArgument);
                    string idnty = Encryption.Encrypt(id.ToString());
                    string condition = Encryption.Encrypt("View");
                    Response.Redirect("~/Plans.aspx?Identity=" + idnty + "&Condition=" + condition, false);
                }
                if (e.CommandName == "RowEdit")
                {
                    int id = Convert.ToInt32(e.CommandArgument);
                    string idnty = Encryption.Encrypt(id.ToString());
                    string condition = Encryption.Encrypt("Edit");
                    Response.Redirect("~/Plans.aspx?Identity=" + idnty + "&Condition=" + condition, false);
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}