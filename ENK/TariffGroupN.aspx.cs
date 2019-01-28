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
using System.Net.Mail;
using System.Configuration;
using ENK.ServiceReference1;

namespace ENK
{
    public partial class TariffGroupN : System.Web.UI.Page
    {
        Service1Client svc = new Service1Client();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["LoginID"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }
            string identity = "";
            identity = Convert.ToString(Request.QueryString["Identity"]);
            if (identity != null)
            {
                identity = Encryption.Decrypt(identity);
            }

            string mode = "";
            mode = Convert.ToString(Request.QueryString["Condition"]);
            if (mode != null)
            {
                mode = Encryption.Decrypt(mode);
            }
            else
            {
                mode = "";
            }
            int Id = 0;
            if (identity != "")
            {
                Id = Convert.ToInt32(identity);

            }
            if (!Page.IsPostBack)
            {

                try
                {

                    if (mode == "Edit")
                    {
                        enableControls();
                        FillGrid(mode, Id);
                        btnSave.Text = "Update";
                      //  txtGroup.Enabled = false;
                    }
                    else if (mode == "View")
                    {
                        disableControls();
                        FillGrid(mode, Id);
                    }
                    else
                    {
                        enableControls();
                        FillGrid(mode, Id);
                        btnSave.Text = "Save";
                    }

                }
                catch (Exception ex)
                {

                }
            }
        }
        protected void disableControls()
        {
            gvTariff.Enabled = false;
            btnSave.Enabled = false;
            txtComission.Enabled = false;
            txtRechageCom.Enabled = false;
            txtGroup.Enabled = false;
            // add by akash starts
            txtH2OGeneralDiscount.Enabled = false;
            txtH2ORechargeDiscount.Enabled = false;
            // add by akash ends
        }
        protected void enableControls()
        {
            gvTariff.Enabled = true;
            btnSave.Enabled = true;
            txtComission.Enabled = true;
            txtRechageCom.Enabled = true;
            txtGroup.Enabled = true;
            // add by akash starts
            txtH2OGeneralDiscount.Enabled = true;
            txtH2ORechargeDiscount.Enabled = true;
            // add by akash ends
        }
        protected void ResetControl()
        {

            FillGrid("", 0);
            btnSave.Enabled = true;
            txtComission.Text = "";
            txtRechageCom.Text = "";
            txtGroup.Text = "";
            // add by akash starts
            txtH2OGeneralDiscount.Text = "";
            txtH2ORechargeDiscount.Text = "";
            // add by akash ends
            btnSave.Text = "Save";
        }
        protected void FillGrid(string mode, int Id)
        {
            try
            {

                DataSet ds = svc.GetTariffSpiffDetails(mode, Id);

                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        txtGroup.Text = Convert.ToString(ds.Tables[0].Rows[0]["TariffGroup"]);
                        txtComission.Text = Convert.ToString(ds.Tables[0].Rows[0]["GeneralCommission"]);
                        txtRechageCom.Text = Convert.ToString(ds.Tables[0].Rows[0]["RechargeCommission"]);
                        // add by akash starts
                        txtH2OGeneralDiscount.Text = Convert.ToString(ds.Tables[0].Rows[0]["H2OGeneralCommission"]);
                        txtH2ORechargeDiscount.Text = Convert.ToString(ds.Tables[0].Rows[0]["H2ORechargeCommission"]);
                        // add by akash ends
                    }
                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        gvTariff.DataSource = ds.Tables[1];
                        gvTariff.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }
        public void CalculateComission(int index, string txtSpif, string txtComs)
        {
            try
            {
                int grv = index;

                TextBox txtSpiff = (TextBox)gvTariff.Rows[grv].FindControl(txtSpif);
                TextBox txtComm = (TextBox)gvTariff.Rows[grv].FindControl(txtComs);
                HiddenField hdnRental = (HiddenField)gvTariff.Rows[grv].FindControl("hdnRental");
                HiddenField hdnFlag = (HiddenField)gvTariff.Rows[grv].FindControl("hdnFlag");

                decimal Comission = 0;
                decimal spiff = 0;
                decimal Rental = 0;
                decimal TotalComission = 0;

                if (txtComission.Text.Trim() != "")
                {
                    Comission = Convert.ToDecimal(txtComission.Text);
                }
                if (txtSpiff.Text.Trim() != "")
                {
                    spiff = Convert.ToDecimal(txtSpiff.Text);
                }
                if (hdnRental.Value.Trim() != "")
                {
                    Rental = Convert.ToDecimal(hdnRental.Value);
                }
                //if(Comission > 0)
                //{
                //Rental - (Rental* comission) - spiff
                TotalComission = (Rental - ((Rental * Comission) / 100)) - spiff;
                //}

                txtComm.Text = Convert.ToString(TotalComission);
                hdnFlag.Value = "1";
            }
            catch (Exception ex)
            {
                ShowPopUpMsg(ex.Message);
            }
        }

        protected void txtSpiff1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                TextBox textBox = (TextBox)sender;
                GridViewRow row = (GridViewRow)textBox.NamingContainer;
                int grv = row.RowIndex;

                CalculateComission(grv, "txtSP1", "txtSComm1");
            }
            catch (Exception ex)
            {
                ShowPopUpMsg(ex.Message);
            }

        }
        protected void txtSpiff2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                TextBox textBox = (TextBox)sender;
                GridViewRow row = (GridViewRow)textBox.NamingContainer;
                int grv = row.RowIndex;

                CalculateComission(grv, "txtSP2", "txtSComm2");
            }
            catch (Exception ex)
            {
                ShowPopUpMsg(ex.Message);
            }

        }
        protected void txtSpiff3_TextChanged(object sender, EventArgs e)
        {
            try
            {
                TextBox textBox = (TextBox)sender;
                GridViewRow row = (GridViewRow)textBox.NamingContainer;
                int grv = row.RowIndex;

                CalculateComission(grv, "txtSP3", "txtSComm3");
            }
            catch (Exception ex)
            {
                ShowPopUpMsg(ex.Message);
            }

        }
        protected void txtSpiff4_TextChanged(object sender, EventArgs e)
        {
            try
            {
                TextBox textBox = (TextBox)sender;
                GridViewRow row = (GridViewRow)textBox.NamingContainer;
                int grv = row.RowIndex;

                CalculateComission(grv, "txtSP4", "txtSComm4");
            }
            catch (Exception ex)
            {
                ShowPopUpMsg(ex.Message);
            }

        }
        protected void txtSpiff5_TextChanged(object sender, EventArgs e)
        {
            try
            {
                TextBox textBox = (TextBox)sender;
                GridViewRow row = (GridViewRow)textBox.NamingContainer;
                int grv = row.RowIndex;

                CalculateComission(grv, "txtSP5", "txtSComm5");
            }
            catch (Exception ex)
            {
                ShowPopUpMsg(ex.Message);
            }

        }
        protected void txtSpiff6_TextChanged(object sender, EventArgs e)
        {
            try
            {
                TextBox textBox = (TextBox)sender;
                GridViewRow row = (GridViewRow)textBox.NamingContainer;
                int grv = row.RowIndex;

                CalculateComission(grv, "txtSP6", "txtSComm6");
            }
            catch (Exception ex)
            {
                ShowPopUpMsg(ex.Message);
            }

        }
        protected void txtSpiff7_TextChanged(object sender, EventArgs e)
        {
            try
            {
                TextBox textBox = (TextBox)sender;
                GridViewRow row = (GridViewRow)textBox.NamingContainer;
                int grv = row.RowIndex;

                CalculateComission(grv, "txtSP7", "txtSComm7");
            }
            catch (Exception ex)
            {
                ShowPopUpMsg(ex.Message);
            }

        }
        protected void txtSpiff8_TextChanged(object sender, EventArgs e)
        {
            try
            {
                TextBox textBox = (TextBox)sender;
                GridViewRow row = (GridViewRow)textBox.NamingContainer;
                int grv = row.RowIndex;

                CalculateComission(grv, "txtSP8", "txtSComm8");
            }
            catch (Exception ex)
            {
                ShowPopUpMsg(ex.Message);
            }

        }
        protected void txtSpiff9_TextChanged(object sender, EventArgs e)
        {
            try
            {
                TextBox textBox = (TextBox)sender;
                GridViewRow row = (GridViewRow)textBox.NamingContainer;
                int grv = row.RowIndex;

                CalculateComission(grv, "txtSP9", "txtSComm9");
            }
            catch (Exception ex)
            {
                ShowPopUpMsg(ex.Message);
            }

        }
        protected void txtSpiff10_TextChanged(object sender, EventArgs e)
        {
            try
            {
                TextBox textBox = (TextBox)sender;
                GridViewRow row = (GridViewRow)textBox.NamingContainer;
                int grv = row.RowIndex;

                CalculateComission(grv, "txtSP10", "txtSComm10");
            }
            catch (Exception ex)
            {
                ShowPopUpMsg(ex.Message);
            }

        }
        protected void txtSpiff11_TextChanged(object sender, EventArgs e)
        {
            try
            {
                TextBox textBox = (TextBox)sender;
                GridViewRow row = (GridViewRow)textBox.NamingContainer;
                int grv = row.RowIndex;

                CalculateComission(grv, "txtSP11", "txtSComm11");
            }
            catch (Exception ex)
            {
                ShowPopUpMsg(ex.Message);
            }

        }
        protected void txtSpiff12_TextChanged(object sender, EventArgs e)
        {
            try
            {
                TextBox textBox = (TextBox)sender;
                GridViewRow row = (GridViewRow)textBox.NamingContainer;
                int grv = row.RowIndex;

                CalculateComission(grv, "txtSP12", "txtSComm12");
            }
            catch (Exception ex)
            {
                ShowPopUpMsg(ex.Message);
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

        protected void gvTariff_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HiddenField hdnActive = (HiddenField)e.Row.FindControl("hdnActive");

                if (hdnActive.Value == "False")
                {
                    e.Row.Enabled = false;
                }
            }
        }
        protected DataTable SpiffDetail()
        {
            DataTable dt = new DataTable();
            dt.TableName = "SpiffDetail";
            dt.Columns.Add("TariffID");
            dt.Columns.Add("Spiff1");
            dt.Columns.Add("Spiff2");
            dt.Columns.Add("Spiff3");
            dt.Columns.Add("Spiff4");
            dt.Columns.Add("Spiff5");
            dt.Columns.Add("Spiff6");
            dt.Columns.Add("Spiff7");
            dt.Columns.Add("Spiff8");
            dt.Columns.Add("Spiff9");
            dt.Columns.Add("Spiff10");
            dt.Columns.Add("Spiff11");
            dt.Columns.Add("Spiff12");

            for (int i = 0; i < gvTariff.Rows.Count; i++)
            {
                DataRow dr = dt.NewRow();
                HiddenField hdnTariffId = (HiddenField)gvTariff.Rows[i].FindControl("hdnTariffId");
                HiddenField hdnFlag = (HiddenField)gvTariff.Rows[i].FindControl("hdnFlag");
                dr["TariffID"] = Convert.ToString(hdnTariffId.Value);
                for (int j = 1; j <= 12; j++)
                {
                    string txtSpif = "txtSP" + Convert.ToString(j);
                    string Spiff = "Spiff" + Convert.ToString(j);

                    TextBox txtSpiff = (TextBox)gvTariff.Rows[i].FindControl(txtSpif);

                    if (hdnFlag.Value == "1")
                    {

                        //dr["TariffID"] = Convert.ToString(hdnTariffId.Value);
                        if (Convert.ToString(txtSpiff.Text.Trim()) == "")
                        {
                            dr[Spiff] = "0";
                        }
                        else
                        {
                            dr[Spiff] = Convert.ToString(txtSpiff.Text.Trim());

                        }

                    }

                }
                if (hdnFlag.Value == "1")
                {
                    dt.Rows.Add(dr);
                }


            }

            dt.AcceptChanges();
            return dt;
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtGroup.Text.Trim() == "")
                {
                    ShowPopUpMsg("Please enter Tariff Group. ");
                    return;
                }
                if (btnSave.Text.Trim() == "Save")
                {
                    DataSet ds = svc.CheckTariffGroupExist(txtGroup.Text.Trim());
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ShowPopUpMsg("Tariff Group alredy exist with this name. ");
                        return;
                    }
                }

                STariff sTM = new STariff();
                //sTM.Comission = Convert.ToDouble(txtComission.Text.Trim());
                // add by akash starts
                //sTM.H2OGeneralDiscount = Convert.ToDouble(txtH2OGeneralDiscount.Text.Trim());
                // add by akash ends
                sTM.GroupName = Convert.ToString(txtGroup.Text.Trim());
                sTM.dtSpiffDetail = SpiffDetail();

                int loginID = Convert.ToInt32(Session["LoginID"]);
                // add by akash starts
                decimal H2ORechargeDiscount = 0;
                decimal.TryParse(Convert.ToString(txtH2ORechargeDiscount.Text), out H2ORechargeDiscount);
                 //H2ORechargeDiscount = Convert.ToDecimal(txtH2ORechargeDiscount.Text);
                // add by akash ends
                decimal rechargecommision = 0;
                decimal.TryParse(Convert.ToString(txtRechageCom.Text), out rechargecommision);
                // decimal rechargecommision = Convert.ToDecimal(txtRechageCom.Text);
                decimal _Comission = 0;
                decimal.TryParse(Convert.ToString(txtComission.Text), out _Comission);

                decimal _H2OGeneralDiscount = 0;
                decimal.TryParse(Convert.ToString(txtH2OGeneralDiscount.Text), out _H2OGeneralDiscount);

                if (btnSave.Text.Trim() == "Save")
                {
                    int TariggGroupId = 0;
                    int a = svc.SaveTariffGroupSpiffMapping(sTM, loginID, TariggGroupId, "INSERT", rechargecommision, H2ORechargeDiscount, Convert.ToDecimal(_Comission), Convert.ToDecimal(_H2OGeneralDiscount));
                    if (a > 0)
                    {
                        ShowPopUpMsg("Data Saved Successfully");
                        ResetControl();
                        Response.Redirect("TariffGroup.aspx");

                    }

                    else
                    {
                        ShowPopUpMsg("Detail not saved. \n Please Try Again");

                    }
                }
                else
                {
                    string identity = Convert.ToString(Request.QueryString["Identity"]);
                    identity = Encryption.Decrypt(identity);
                    int TariggGroupId = 0;
                    TariggGroupId = Convert.ToInt32(identity);


                    int a = svc.SaveTariffGroupSpiffMapping(sTM, loginID, TariggGroupId, "UPDATE", rechargecommision, H2ORechargeDiscount, Convert.ToDecimal(_Comission), Convert.ToDecimal(_H2OGeneralDiscount));
                    if (a > 0)
                    {
                        ShowPopUpMsg("Data Updated Successfully");
                        ResetControl();
                        //Response.Redirect("TariffGroup.aspx");
                    }
                    else
                    {
                        ShowPopUpMsg("Detail not Updated. \n Please Try Again");

                    }
                }




            }
            catch (Exception ex)
            {
                ShowPopUpMsg(ex.Message);
            }
        }

        protected void txtComission_Changed(object sender, EventArgs e)
        {
            try
            {
                decimal Comission = 0;
                if (txtComission.Text.Trim() != "")
                {
                    Comission = Convert.ToDecimal(txtComission.Text);
                }

                txtRechageCom.Text = Convert.ToString(Comission);
               
                for (int i = 0; i < gvTariff.Rows.Count; i++)
                {
                    for (int j = 1; j <= 12; j++)
                    {
                        string txtSpif = "txtSP" + Convert.ToString(j);
                        string txtComs = "txtSComm" + Convert.ToString(j); ;

                        TextBox txtSpiff = (TextBox)gvTariff.Rows[i].FindControl(txtSpif);
                        TextBox txtComm = (TextBox)gvTariff.Rows[i].FindControl(txtComs);
                        HiddenField hdnRental = (HiddenField)gvTariff.Rows[i].FindControl("hdnRental");
                        HiddenField hdnFlag = (HiddenField)gvTariff.Rows[i].FindControl("hdnFlag");

                        decimal spiff = 0;
                        decimal Rental = 0;
                        decimal TotalComission = 0;

                        if (txtSpiff.Text.Trim() != "")
                        {
                            spiff = Convert.ToDecimal(txtSpiff.Text);
                        }
                        if (hdnRental.Value.Trim() != "")
                        {
                            Rental = Convert.ToDecimal(hdnRental.Value);
                        }

                        if (spiff > 0)
                        {
                            TotalComission = (Rental - ((Rental * Comission) / 100)) - spiff;

                            txtComm.Text = Convert.ToString(TotalComission);
                            hdnFlag.Value = "1";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ShowPopUpMsg(ex.Message);
            }
        }

        //protected void btnBack_Click(object sender, EventArgs e)
        //{
        //    
        //}

        protected void btnBack_Click1(object sender, EventArgs e)
        {
            Response.Redirect("TariffGroup.aspx");
        }

        protected void txtH2OGeneralDiscount_TextChanged(object sender, EventArgs e)
        {

            try
            {
                decimal H2OComission = 0;
                if (txtH2OGeneralDiscount.Text.Trim() != "")
                {
                    H2OComission = Convert.ToDecimal(txtH2OGeneralDiscount.Text);
                }

                txtH2ORechargeDiscount.Text = Convert.ToString(H2OComission);

                for (int i = 0; i < gvTariff.Rows.Count; i++)
                {
                    for (int j = 1; j <= 12; j++)
                    {
                        string txtSpif = "txtSP" + Convert.ToString(j);
                        string txtComs = "txtSComm" + Convert.ToString(j); ;

                        TextBox txtSpiff = (TextBox)gvTariff.Rows[i].FindControl(txtSpif);
                        TextBox txtComm = (TextBox)gvTariff.Rows[i].FindControl(txtComs);
                        HiddenField hdnRental = (HiddenField)gvTariff.Rows[i].FindControl("hdnRental");
                        HiddenField hdnFlag = (HiddenField)gvTariff.Rows[i].FindControl("hdnFlag");

                        decimal spiff = 0;
                        decimal Rental = 0;
                        decimal TotalComission = 0;

                        if (txtSpiff.Text.Trim() != "")
                        {
                            spiff = Convert.ToDecimal(txtSpiff.Text);
                        }
                        if (hdnRental.Value.Trim() != "")
                        {
                            Rental = Convert.ToDecimal(hdnRental.Value);
                        }

                        if (spiff > 0)
                        {
                            TotalComission = (Rental - ((Rental * H2OComission) / 100)) - spiff;

                            txtComm.Text = Convert.ToString(TotalComission);
                            hdnFlag.Value = "1";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ShowPopUpMsg(ex.Message);
            }
        }

    }
}