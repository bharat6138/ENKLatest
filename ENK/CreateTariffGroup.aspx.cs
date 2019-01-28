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
    public partial class CreateTariffGroup : System.Web.UI.Page
    {
        Service1Client svc = new Service1Client();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    //if (Session["LoginID"] != null)
                    //{
                    //    if (Session["ClientType"].ToString() == "Distributor")
                    //    {
                    //        Response.Redirect("Login.aspx", false);
                    //        Session.Abandon();
                    //        return;
                    //    }

                    //}
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

                        divActive.Visible = true;
                        FillTariff(Identity);
                        ResetControl(2);
                        btnReset.Visible = false;
                        btnSave.Visible = false;
                        btnBack.Visible = true;
                        btnUpdate.Visible = false;


                    }
                    if (Condition == "Edit")
                    {

                        divActive.Visible = true;
                        FillTariff(Identity);
                        hddnTariffID.Value = Identity.ToString();
                        //txtTariffCode.Attributes.Add("readonly", "true");
                        btnReset.Visible = false;
                        btnSave.Visible = false;
                        btnBack.Visible = true;
                        btnUpdate.Visible = true;


                    }
                    if (Condition == "")
                    {

                        divActive.Visible = false;
                        btnBack.Visible = true;
                        btnUpdate.Visible = false;



                    }
                }
            }
            catch (Exception ex)
            {

            }

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

            try
            {
                STariff st = new STariff();
                st.TarifName = Convert.ToString(txtTraiffName.Text.Trim());
                st.Discount_on_Activation_PortIn = Convert.ToDouble(txtDisActivationPortIN.Text.Trim());
                st.Discount_on_Recharge = Convert.ToDouble(txtDiscount_on_Recharge.Text.Trim());
                st.SellerID = Convert.ToInt32(ddlseller.SelectedValue);

                int loginID = Convert.ToInt32(Session["LoginID"]);
                int distributorID = Convert.ToInt32(Session["DistributorID"]);

                int a = svc.AddNewTariffService(st, loginID, distributorID);
                if (a > 0)
                {
                    ResetControl(1);
                    ShowPopUpMsg("Plan Added Successfull");



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




        public void ResetControl(int cond)
        {
            if (cond == 1)
            {

                txtTraiffName.Text = "";

                txtDisActivationPortIN.Text = "";
                txtDiscount_on_Recharge.Text = "";

                ddlseller.SelectedIndex = 0;
                CheckBox1.Checked = true;

            }
            if (cond == 2)
            {
                txtTraiffName.Attributes.Add("readonly", "true");
                txtDisActivationPortIN.Attributes.Add("readonly", "true");
                txtDiscount_on_Recharge.Attributes.Add("readonly", "true");

                ddlseller.Attributes.Add("disabled", "disabled");
                CheckBox1.Enabled = false;
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

        protected void btnBack_Click1(object sender, EventArgs e)
        {
            Response.Redirect("TariffGroup.aspx");
        }

        protected void btnReset_Click1(object sender, EventArgs e)
        {
            ResetControl(1);
        }

        public void FillTariff(string TarifID)
        {
            try
            {
                int tar = Convert.ToInt32(TarifID);
                DataSet ds = svc.GetTariffGroupViewService(tar);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {

                        hddnTariffID.Value = Convert.ToString(ds.Tables[0].Rows[0]["TariffID"]);
                        txtTraiffName.Text = Convert.ToString(ds.Tables[0].Rows[0]["TariffName"]);
                        txtDisActivationPortIN.Text = Convert.ToString(ds.Tables[0].Rows[0]["DiscountonActivation_PortIn"]);
                        txtDiscount_on_Recharge.Text = Convert.ToString(ds.Tables[0].Rows[0]["DiscountonRecharge"]);

                        ddlseller.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["SellerID"]);
                        CheckBox1.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["IsActive"]);


                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void btnUpdate_Click1(object sender, EventArgs e)
        {
            try
            {
                STariff st = new STariff();

                st.TarifID = Convert.ToInt32(hddnTariffID.Value);
                st.TarifName = Convert.ToString(txtTraiffName.Text.Trim());
                st.Discount_on_Activation_PortIn = Convert.ToDouble(txtDisActivationPortIN.Text.Trim());
                st.Discount_on_Recharge = Convert.ToDouble(txtDiscount_on_Recharge.Text.Trim());
                st.SellerID = Convert.ToInt32(ddlseller.SelectedValue);

                int loginID = Convert.ToInt32(Session["LoginID"]);
                int distributorID = Convert.ToInt32(Session["DistributorID"]);
                st.IsActive = CheckBox1.Checked;



                int a = svc.UpdateTariffGroupService(st, loginID, distributorID);
                if (a > 0)
                {
                    ShowPopUpMsg("Tariff Group Updated Successfull");
                    ResetControl(1);
                    btnUpdate.Visible = false;

                    Response.Redirect("TariffGroup.aspx", false);
                }
                else
                {
                    ShowPopUpMsg("Tariff Group Updated UnSuccessfull \n Please Try Again");

                }

            }
            catch (Exception ex)
            {
                ShowPopUpMsg("Tariff Group Updated UnSuccessfull \n Please Try Again");
            }

        }
    }
}