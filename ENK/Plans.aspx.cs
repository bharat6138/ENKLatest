using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using ENK.ServiceReference1;

namespace ENK
{
    public partial class Plans : System.Web.UI.Page
    {
        Service1Client svc = new Service1Client();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    if (Session["LoginID"] != null)
                    {
                        if (Session["ClientType"].ToString() == "Distributor")
                        {
                            Response.Redirect("Login.aspx", false);
                            Session.Abandon();
                            return;
                        }

                    }
                    string Condition = Request.QueryString["Condition"];
                    if (Condition != null && Condition != "")
                    {
                        Condition = Encryption.Decrypt(Request.QueryString["Condition"]);
                    }
                    else
                    {
                        Condition = "";
                    }
                    string Identity = Request.QueryString["Identity"];
                    if (Identity != null && Identity != "")
                    {
                        Identity = Encryption.Decrypt(Request.QueryString["Identity"]);
                    }
                    else
                    {
                        Identity = "";
                    }
                    if (Condition == "View")
                    {
                        BindDDL();
                        divActive.Visible = true;
                        FillTariff(Identity);
                        ResetControl(2);
                        btnReset.Visible = false;
                        btnSave.Visible = false;
                        btnBack.Visible = true;
                        btnUpdate.Visible = false;
                        liUpdate.Visible = false;
                        liBack.Visible = true;
                    }
                    if (Condition == "Edit")
                    {
                        BindDDL();
                        divActive.Visible = true;
                        FillTariff(Identity);
                        hddnTariffID.Value = Identity.ToString();
                        //txtTariffCode.Attributes.Add("readonly", "true");
                        btnReset.Visible = false;
                        btnSave.Visible = false;
                        btnBack.Visible = true;
                        btnUpdate.Visible = true;
                        liUpdate.Visible = true;
                        liBack.Visible = true;
                    }
                    if (Condition == "")
                    {
                        BindDDL();
                        divActive.Visible = false;
                        btnBack.Visible = true;
                        btnUpdate.Visible = false;
                        liUpdate.Visible = false;
                        liBack.Visible = true;
                        liSave.Visible = true;
                        liReset.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        public void BindDDL()
        {
            try
            {
                DataSet ds = svc.GetShortCodeService("TariffType");

                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    ddlTariffType.DataSource = ds.Tables[0];
                    ddlTariffType.DataValueField = "ID";
                    ddlTariffType.DataTextField = "value";
                    ddlTariffType.DataBind();
                    ddlTariffType.Items.Insert(0, new ListItem("---Select---", "0"));
                }

                DataTable dt = svc.GetVendor(Convert.ToInt32(Session["LoginId"]));
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        ddlNetworkType.DataSource = dt;
                        ddlNetworkType.DataValueField = "VendorID";
                        ddlNetworkType.DataTextField = "VendorName";
                        ddlNetworkType.DataBind();

                        ddlNetworkType.SelectedValue = "13";
                    }
                }

            }
            catch (Exception ex)
            {

            }

        }

        public void FillTariff(string TarifID)
        {
            try
            {
                int tar = Convert.ToInt32(TarifID);
                DataSet ds = svc.GetSingleTariffService(tar);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        hddnTariffID.Value = Convert.ToString(ds.Tables[0].Rows[0]["ID"]);
                        txtTariffCode.Text = Convert.ToString(ds.Tables[0].Rows[0]["TariffCode"]);
                        txtDescription.Text = Convert.ToString(ds.Tables[0].Rows[0]["Description"]);
                        txtRental.Text = Convert.ToString(ds.Tables[0].Rows[0]["Rental"]);
                        txtValidDays.Text = Convert.ToString(ds.Tables[0].Rows[0]["ValidityDays"]);
                        ddlTariffType.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["TariffTypeID"]);
                        CheckBox1.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["IsActive"]);
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        public void ResetControl(int cond)
        {
            if (cond == 1)
            {
                txtTariffCode.Text = "";
                txtDescription.Text = "";
                txtValidDays.Text = "";
                txtRental.Text = "";
                ddlTariffType.SelectedIndex = 0;
                CheckBox1.Checked = true;

            }
            if (cond == 2)
            {
                txtTariffCode.Attributes.Add("readonly", "true");
                txtDescription.Attributes.Add("readonly", "true");
                txtValidDays.Attributes.Add("readonly", "true");
                txtRental.Attributes.Add("readonly", "true");
                ddlTariffType.Attributes.Add("disabled", "disabled");
                CheckBox1.Enabled = false;
            }

        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("CreatePlans.aspx");
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            ResetControl(1);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                STariff st = new STariff();
                st.TariffCode = txtTariffCode.Text.Trim();
                st.Description = txtDescription.Text.Trim();
                st.ValidDays = Convert.ToInt32(txtValidDays.Text.Trim());
                st.Rental = Convert.ToDouble(txtRental.Text.Trim());
                st.TariffType = Convert.ToInt32(ddlTariffType.SelectedValue);
                st.IsActive = CheckBox1.Checked;

                st.NetworkID = Convert.ToInt32(ddlNetworkType.SelectedValue);

                int loginID = Convert.ToInt32(Session["LoginID"]);
                int distributorID = Convert.ToInt32(Session["DistributorID"]);

                int a = svc.AddTariffService(st, loginID, distributorID);
                if (a > 0)
                {
                    ShowPopUpMsg("Plan Added Successfull");
                    ResetControl(1);
                }
                else
                {
                    ShowPopUpMsg("Plan Added UnSuccessfull \n Please Try Again");

                }
            }
            catch (Exception ex)
            {
                ShowPopUpMsg("Plan Added UnSuccessfull \n Please Try Again");
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                STariff st = new STariff();

                st.TarifID = Convert.ToInt32(hddnTariffID.Value);
                st.TariffCode = txtTariffCode.Text.Trim();
                st.Description = txtDescription.Text.Trim();
                st.ValidDays = Convert.ToInt32(txtValidDays.Text.Trim());
                st.Rental = Convert.ToDouble(txtRental.Text.Trim());
                st.TariffType = Convert.ToInt32(ddlTariffType.SelectedValue);
                st.IsActive = CheckBox1.Checked;

                int loginID = Convert.ToInt32(Session["LoginID"]);
                int distributorID = Convert.ToInt32(Session["DistributorID"]);

                int a = svc.UpdateTariffService(st, loginID, distributorID);
                if (a > 0)
                {
                    ShowPopUpMsg("Plan Updated Successfull");
                    ResetControl(1);
                    btnUpdate.Visible = false;
                    liUpdate.Visible = false;
                    liBack.Visible = true;
                    Response.Redirect("CreatePlans.aspx", false);
                }
                else
                {
                    ShowPopUpMsg("Plan Updated UnSuccessfull \n Please Try Again");

                }

            }
            catch (Exception ex)
            {
                ShowPopUpMsg("Plan Updated UnSuccessfull \n Please Try Again");
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

        protected void btnUpdateUp_Click(object sender, EventArgs e)
        {
            btnUpdate_Click(null, null);
        }

        protected void btnBackUp_Click(object sender, EventArgs e)
        {
            Response.Redirect("CreatePlans.aspx");
        }

        protected void btnSaveUp_Click(object sender, EventArgs e)
        {
            btnSave_Click(null, null);
        }

        protected void btnResetUp_Click(object sender, EventArgs e)
        {
            ResetControl(1);
        }


    }
}