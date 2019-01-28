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
    public partial class DistributorPlanMappingChange : System.Web.UI.Page
    {
        Service1Client svc = new Service1Client();
        DataTable dtfillGrid;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataSet ds = svc.GetDistributorofMappingwithPlan(0);

                if (ds != null)
                {
                    ddlTariffPlan.DataSource = ds.Tables[0];
                    ddlTariffPlan.DataValueField = "ID";
                    ddlTariffPlan.DataTextField = "Plan";
                    ddlTariffPlan.DataBind();
                    ddlTariffPlan.Items.Insert(0, new ListItem("---Select Plan---", "0"));
                }
                fillblankGrid();
            }
        }

        protected void fillblankGrid()
        {
            //SIMPurchase

            dtfillGrid = new DataTable();

            dtfillGrid.Columns.Add("DistributorName");
            dtfillGrid.Columns.Add("DistributorID");
            dtfillGrid.Columns.Add("TariffCode");
            dtfillGrid.Columns.Add("Description");
            dtfillGrid.Columns.Add("TariffID");

            Repeater.DataSource = dtfillGrid;
            Repeater.DataBind();

        }
        protected void btnDone_Click(object sender, EventArgs e)
        {
            //Boolean flag = false;
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
            dt.Columns.Add("IsCheck");
            
            for (int i = 0; i < Repeater.Items.Count; i++)
            {
                CheckBox chk = (CheckBox)Repeater.Items[i].FindControl("CheckBox2");
                HiddenField hdnDistributorID = (HiddenField)Repeater.Items[i].FindControl("hdnDistributorID");
                    
                DataRow dr = dt.NewRow();


                dr["DistributorID"] = Convert.ToInt64(hdnDistributorID.Value);
                dr["IsCheck"] = Convert.ToBoolean(chk.Checked);//Convert.ToInt64(hdnDistributorID.Value);

                dt.Rows.Add(dr);
                dt.AcceptChanges();
            }


            int retval = svc.SaveDistributorofMappingwithPlan(Convert.ToDecimal(txtRental.Text.Trim()), dt, Convert.ToInt32(ddlTariffPlan.SelectedValue));
            if (retval > 0)
            {
                ShowPopUpMsg("Changed successfully");
            }
            else
            {
                ShowPopUpMsg("Changed successfully");
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
            if (ddlTariffPlan.SelectedValue == "0")
            {
                fillblankGrid();
            }
            else 
            {
                txtRental.Text = string.Empty;

                DataSet ds = svc.GetDistributorofMappingwithPlan(Convert.ToInt32(ddlTariffPlan.SelectedValue));

                if (ds != null)
                {
                    Repeater.DataSource = ds.Tables[0];
                    Repeater.DataBind();

                    for (int i = 0; i < Repeater.Items.Count; i++)
                    {
                        CheckBox CheckBox = (CheckBox)Repeater.Items[i].FindControl("CheckBox2");
                        HiddenField hdnTariffID = (HiddenField)Repeater.Items[i].FindControl("hdnTariffID");
                        if (Convert.ToInt32(hdnTariffID.Value) > 0)
                        {
                            CheckBox.Checked = true;
                        }
                        else
                        {
                            CheckBox.Checked = false;
                        }
                    }
                }
            }
            
        }
    }
}