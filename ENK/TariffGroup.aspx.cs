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
    public partial class TariffGroup : System.Web.UI.Page
    {

        Service1Client svc = new Service1Client();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["LoginID"] != null)
            {
                if (!Page.IsPostBack)
                {

                    sparks.Visible = false;
                    DataSet ds = svc.GetSingleDistributorTariffService(Convert.ToInt32(Session["DistributorID"]));
                    if(ds.Tables[6].Rows.Count>0)
                    {
                    int tariffCheck = Convert.ToInt32(ds.Tables[6].Rows[0]["TarrifGroupCreationRight"]);
                    if (tariffCheck == 1)
                    {
                    sparks.Visible = true;
                    }
                    }
                    FillTariff();

                }
            }

        }


        public void FillTariff()
        {
            try
            {
                int userid = Convert.ToInt32(Session["LoginID"]);

                int DistributorID = Convert.ToInt32(Session["DistributorID"]);
                DataSet dst = svc.GetTariffGroupService(userid, DistributorID);

                //ViewState["Tariff"] = dst.Tables[4];


                if (dst != null)
                {

                    RpTariff.DataSource = dst.Tables[0];
                    RpTariff.DataBind();


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

        protected void RpTariff_ItemCommand(object source, RepeaterCommandEventArgs e)
        {

            try
            {
                if (e.CommandName == "View")
                {
                    int id = Convert.ToInt32(e.CommandArgument);
                    string idnty = Encryption.Encrypt(id.ToString());
                    string condition = Encryption.Encrypt("View");
                    Response.Redirect("~/TariffGroupN.aspx?Identity=" + idnty + "&Condition=" + condition, false);
                }
                if (e.CommandName == "RowEdit")
                {
                    int id = Convert.ToInt32(e.CommandArgument);
                    if (Convert.ToInt32(Session["TariffGroupID"]) == id && Convert.ToString(Session["ClientType"]) != "Company")
                    {                    
                    }
                    else
                    {
                    string idnty = Encryption.Encrypt(id.ToString());
                    string condition = Encryption.Encrypt("Edit");
                    Response.Redirect("~/TariffGroupN.aspx?Identity=" + idnty + "&Condition=" + condition, false);
                    }
                }
            }
            catch (Exception ex)
            {

            }


        }


    }
}