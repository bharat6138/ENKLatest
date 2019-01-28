using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Net.Mail;
using System.Configuration;
using ENK.ServiceReference1;

namespace ENK
{
    public partial class RechargeBulk : System.Web.UI.Page
    {
        Service1Client svc = new Service1Client();
        DataTable dtfillGrid;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int userid = Convert.ToInt32(Session["LoginId"]);
                int distributorID = Convert.ToInt32(Session["DistributorID"]);

                DataTable dt = svc.GetVendor(Convert.ToInt32(Session["LoginId"]));
                
                
                
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        ddlNetwork.DataSource = dt;
                        ddlNetwork.DataValueField = "VendorID";
                        ddlNetwork.DataTextField = "VendorName";
                        ddlNetwork.DataBind();
                        ddlNetwork.Items.Insert(0, new ListItem("---All Network---", "0"));
                    }
                }



                DataSet  ds = svc.GetDistributor(0);

                ddlTariffPlan.DataSource = ds;
                ddlTariffPlan.DataValueField = "ID";
                ddlTariffPlan.DataTextField = "Name";
                ddlTariffPlan.DataBind();
                ddlTariffPlan.Items.Insert(0, new ListItem("ALL", "0"));
                
                Repeater.DataSource = ds;
                Repeater.DataBind();
                
                //fillblankGrid();

                if (Convert.ToString(Session["ClientType"]) == "Company")
                {
                    btnDone.Visible = true;
                }
                else
                {
                    btnDone.Visible = false;
                }
            }
        }

        protected void fillblankGrid()
        {
            //SIMPurchase

            dtfillGrid = new DataTable();

            dtfillGrid.Columns.Add("DistributorName");
            dtfillGrid.Columns.Add("DistributorID");
            //dtfillGrid.Columns.Add("TariffCode");
            //dtfillGrid.Columns.Add("Description");
            //dtfillGrid.Columns.Add("TariffID");

            Repeater.DataSource = dtfillGrid;
            Repeater.DataBind();

        }
        protected void btnDone_Click(object sender, EventArgs e)
        {
             Boolean flag = false;
            if (Repeater.Items.Count == 0 || Repeater.Items.Count < 0)
            {
                ShowPopUpMsg("Please select any Distributor");
                return;
            }
            //for (int i = 0; i < Repeater.Items.Count; i++)
            //{
            //    CheckBox chk = (CheckBox)Repeater.Items[i].FindControl("CheckBox2");

            //    if (chk.Checked == true)
            //    {
            //        flag = true;
            //        break;
            //    }

            //}

            //if (flag == false)
            //{
            //    ShowPopUpMsg("Please select any Distributor");
            //    return;
            //}

            DataTable dt = new DataTable();
            dt.TableName = "dtMobile";
            dt.Columns.Add("DistributorID");
           // dt.Columns.Add("IsCheck");

            for (int i = 0; i < Repeater.Items.Count; i++)
            {
                CheckBox chk = (CheckBox)Repeater.Items[i].FindControl("CheckBox2");
                HiddenField hdnDistributorID = (HiddenField)Repeater.Items[i].FindControl("hdnDistributorID");

                DataRow dr = dt.NewRow();

                if (chk.Checked == true)
                {
                    flag = true;
                    dr["DistributorID"] = Convert.ToInt64(hdnDistributorID.Value);
                    dt.Rows.Add(dr);
                    dt.AcceptChanges();
                }
               
            }
            if (flag == false)
            {
                ShowPopUpMsg("Please select any Distributor");
                return;
            }



            int retval = svc.SaveDistributorRechageBulk(Convert.ToDecimal(txtRental.Text.Trim()), dt, Convert.ToInt32(ddlNetwork.SelectedValue));
            if (retval > 0)
            {
                txtRental.Text = "0";
                ShowPopUpMsg("Distributor % Rechage update successfully.");
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
        protected void chkAllSIM_OnCheckedChanged(object sender, EventArgs e)
        {
            CheckBox chkAll = (CheckBox)sender;
            RepeaterItem row = (RepeaterItem)chkAll.NamingContainer;
            int a = row.ItemIndex;
            if (chkAll.Checked == true)
            {
                for (int i = 0; i < Repeater.Items.Count; i++)
                {
                    CheckBox chk = (CheckBox)Repeater.Items[i].FindControl("CheckBox2");
                    chk.Checked = true;
                }
            }
            if (chkAll.Checked == false)
            {
                for (int i = 0; i < Repeater.Items.Count; i++)
                {
                    CheckBox chk = (CheckBox)Repeater.Items[i].FindControl("CheckBox2");
                    chk.Checked = false;
                }
            }
        }
        protected void ddlTariffPlan_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            
               // txtRental.Text = string.Empty;

                DataSet ds = svc.GetDistributor(Convert.ToInt32(ddlTariffPlan.SelectedValue));

                if (ds != null)
                {
                    Repeater.DataSource = ds.Tables[0];
                    Repeater.DataBind();

                    for (int i = 0; i < Repeater.Items.Count; i++)
                    {
                        CheckBox CheckBox = (CheckBox)Repeater.Items[i].FindControl("CheckBox2");
                       // HiddenField hdnTariffID = (HiddenField)Repeater.Items[i].FindControl("hdnTariffID");
                        //if (Convert.ToInt32(hdnTariffID.Value) > 0)
                        //{
                        //    CheckBox.Checked = true;
                        //}
                        //else
                        //{
                        //    CheckBox.Checked = false;
                        //}
                        if (ddlTariffPlan.SelectedIndex > 0)
                        {
                            CheckBox.Checked = false;
                        }
                        else { CheckBox.Checked = true; }
                    }
                }
            

        }
    }
}