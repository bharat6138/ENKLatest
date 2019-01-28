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
    public partial class AddDistributor : System.Web.UI.Page
    {
        Service1Client svc = new Service1Client();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    txtCode.Attributes.Add("readonly", "true");

                    if (ConfigurationManager.AppSettings["TAX_Mandatory"].ToString() == "1")
                    {
                        spanTaxID.Visible = true;
                    }
                    if (ConfigurationManager.AppSettings["TAX_Doc_Mandatory"].ToString() == "1")
                    {
                        spanMDoc.Visible = true;
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
                        BindTariff(Convert.ToInt32(Session["DistributorID"]));
                        divActive.Visible = true;
                        FillDistributor(Identity);
                        ResetControl(3);
                        btnReset.Visible = false;
                        btnSave.Visible = false;
                        btnBack.Visible = true;
                        btnUpdate.Visible = false;
                        liUpdate.Visible = false;
                        liBack.Visible = true;
                        divCode.Visible = true;
                        txtPassword.Enabled = false;
                        txtPassword.BackColor = System.Drawing.Color.Gray;
                        if (Convert.ToString(Session["ClientType"]) == "Company")
                        {
                            divAcntBalance.Visible = true;
                        }
                        else
                        {

                            txtEin.Enabled = false;
                        }
                       // divfileDoc.Visible = false;
                    }
                    if (Condition == "Edit")
                    {
                        BindDDL();
                        if (Convert.ToString(Session["ClientType"]) != "Company")
                        {
                            txtEmailID.Enabled = false;
                            txtEin.Enabled = false;
                            //divfileDoc.Visible = false;
                        }
                        BindTariff(Convert.ToInt32(Session["DistributorID"]));
                        divActive.Visible = true;
                        FillDistributor(Identity);
                        hddDistributorID.Value = Identity.ToString();
                        txtCode.Attributes.Add("readonly", "true");
                        btnReset.Visible = false;
                        btnSave.Visible = false;
                        btnBack.Visible = true;
                        btnUpdate.Visible = true;
                        liUpdate.Visible = true;
                        liBack.Visible = true;
                        divCode.Visible = true;
                        txtUserID.BackColor = System.Drawing.Color.Gray;
                        txtPassword.BackColor = System.Drawing.Color.Gray;
                        txtUserID.Enabled = false;
                        txtPassword.Enabled = false;
                        if (Convert.ToString(Session["ClientType"]) == "Company")
                        {
                            divAcntBalance.Visible = true;
                        }

                    }
                    if (Condition == "")
                    {
                        BindDDL();
                        BindTariff(Convert.ToInt32(Session["DistributorID"]));
                        divActive.Visible = false;
                        btnBack.Visible = true;
                        btnUpdate.Visible = false;
                        liUpdate.Visible = false;
                        liBack.Visible = true;
                        liSave.Visible = true;
                        liReset.Visible = true;
                        divCode.Visible = false;
                        divAcntBalance.Visible = false;
                        txtAccountBalance.Text = "0";
                        btnSave.Visible = true;
                        DataSet ds1 = svc.GetSingleDistributorTariffService(Convert.ToInt32(Session["DistributorID"]));
                        int tariffCheck1 = Convert.ToInt32(ds1.Tables[6].Rows[0]["TarrifGroupCreationRight"]);
                        if (tariffCheck1 == 0)
                        {
                            Chktariffgrp.Checked = false;
                            Chktariffgrp.Enabled = false;
                        }

                        int sellerCheck1 = Convert.ToInt32(ds1.Tables[6].Rows[0]["SellerCreationRight"]);
                        if (sellerCheck1 == 0)
                        {
                            ChkSeller.Checked = false;
                            ChkSeller.Enabled = false;
                        }

                    }

                }
            }
            catch (Exception ex)
            {

            }
        }

        public void FillDistributor(string identity)
        {
            int id = Convert.ToInt32(identity);
            Distributor[] dis = svc.GetSingleDistributorService(id);
            DataSet ds = svc.GetSingleDistributorTariffService(id);


            DataTable dtRecharge = new DataTable();

            // ds.Tables[4] --------------For Recharge----------------------
            DataView dv1 = new DataView(ds.Tables[4]);
            dv1.RowFilter = "NetworkID = 13";
            dtRecharge = dv1.ToTable();
            if (dtRecharge.Rows.Count > 0)
            {
                txtLycaPerRecharge.Text = Convert.ToDecimal(dtRecharge.Rows[0]["RechargePer"]).ToString();
            }


            for (int i = 0; i < RepeaterTariff.Items.Count; i++)
            {
                CheckBox chk = (CheckBox)RepeaterTariff.Items[i].FindControl("CheckBox2");
                RadioButton rbtn = (RadioButton)RepeaterTariff.Items[i].FindControl("RadioButton1");
                rbtn.Checked = false;

            }


            for (int i = 0; i < RepeaterTariff.Items.Count; i++)
            {
                CheckBox chk = (CheckBox)RepeaterTariff.Items[i].FindControl("CheckBox2");
                RadioButton rbtn = (RadioButton)RepeaterTariff.Items[i].FindControl("RadioButton1");
                HiddenField hddnTariffID = (HiddenField)RepeaterTariff.Items[i].FindControl("hdnTariffId");
                TextBox txtRental = (TextBox)RepeaterTariff.Items[i].FindControl("txtRental");
                rbtn.Checked = false;
                for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
                {
                    if (hddnTariffID.Value == ds.Tables[0].Rows[j]["TariffID"].ToString())
                    {
                        chk.Checked = true;
                        string ss = Convert.ToString(ds.Tables[0].Rows[j]["Rental"]);
                        if (ss.Trim() == "")
                        {
                            ss = "0.00";
                        }
                        txtRental.Text = ss;
                        if (ds.Tables[0].Rows[j]["IsDefault"].ToString() == "True")
                        {
                            rbtn.Checked = true;
                        }

                    }
                }
            }


            string distID = Convert.ToString(Session["DistributorID"]);
            if (Session["LoginID"] != null)
            {
                if (Session["ClientType"].ToString() == "Company")
                {
                    if (distID == identity)
                    {
                        for (int i = 0; i < RepeaterTariff.Items.Count; i++)
                        {
                            TextBox txtRentall = (TextBox)RepeaterTariff.Items[i].FindControl("txtRental");
                            txtRentall.Attributes.Add("readonly", "true");
                        }
                    }
                }
                if (Session["ClientType"].ToString() == "Distributor")
                {
                    if (distID == identity)
                    {
                        for (int i = 0; i < RepeaterTariff.Items.Count; i++)
                        {
                            TextBox txtRentall = (TextBox)RepeaterTariff.Items[i].FindControl("txtRental");
                            txtRentall.Attributes.Add("readonly", "true");
                        }
                    }
                }

            }

            txtDistributorName.Text = dis[0].distributorName;
            txtCode.Text = Convert.ToString(dis[0].distributorCode);
            hddnDistributorType.Value = Convert.ToString(dis[0].companyType);
            hddnParent.Value = Convert.ToString(dis[0].parent);
            txtContactPerson.Text = dis[0].contactPerson;
            txtContactNo.Text = dis[0].contactNo;
            txtWebSiteName.Text = dis[0].webSiteName;
            txtEmailID.Text = dis[0].emailID;
            txtaddress.Text = dis[0].address;
            txtCity.Text = dis[0].city;
            txtState.Text = dis[0].state;
            txtzip.Text = dis[0].zip;
            ddlCountry.SelectedValue = Convert.ToString(dis[0].countryid);
            ddlTarrifGroup.SelectedValue = Convert.ToString(dis[0].TariffGroupID);

            txtAccountBalance.Text = Convert.ToString(dis[0].balanceAmount);

            CheckBox1.Checked = Convert.ToBoolean(dis[0].isActive);
            txtAccountBalance.Text = Convert.ToString(dis[0].balanceAmount);
            txtEin.Text = dis[0].EIN;
            txtSSN.Text = dis[0].SSN;
            txtPan.Text = dis[0].PanNumber;

            //Ankit singh 

            txtUserID.Text = Convert.ToString(ds.Tables[5].Rows[0]["UserName"]);

            int sellerCheck = Convert.ToInt32(ds.Tables[6].Rows[0]["SellerCreationRight"]);
            int tariffCheck = Convert.ToInt32(ds.Tables[6].Rows[0]["TarrifGroupCreationRight"]);

            if (sellerCheck == 1)
            {
                ChkSeller.Checked = true;

            }
            else
            {

                ChkSeller.Checked = false;

            }

            if (tariffCheck == 1)
            {
                Chktariffgrp.Checked = true;

            }
            else
            {

                Chktariffgrp.Checked = false;

            }
            //checking login Distributor that is parentDistributor has right to create TarrifGroup or not,if yes then it can update further
            DataSet ds1 = svc.GetSingleDistributorTariffService(Convert.ToInt32(Session["DistributorID"]));
            int tariffCheck1 = Convert.ToInt32(ds1.Tables[6].Rows[0]["TarrifGroupCreationRight"]);
            if (tariffCheck1 == 0)
            {
                Chktariffgrp.Checked = false;
                Chktariffgrp.Enabled = false;
            }

            int sellerCheck1 = Convert.ToInt32(ds1.Tables[6].Rows[0]["SellerCreationRight"]);
            if (sellerCheck1 == 0)
            {
                ChkSeller.Checked = false;
                ChkSeller.Enabled = false;
            }

            //Checking distributor whose detail is getting edited is parents distributor or not
            //but if it is company then it do any thing.
            if (id == Convert.ToInt32(Session["DistributorID"]) && Convert.ToString(Session["ClientType"]) != "Company")
            {
                Chktariffgrp.Enabled = false;
            }
        }

        public void BindDDL()
        {
            DataSet ds = null;
            int loginid = Convert.ToInt32(Session["LoginID"]);
            int distributorID = Convert.ToInt32(Session["DistributorID"]);
            ds = svc.GetClientType(loginid, distributorID);

            if (ds != null)
            {
                //ddlDistributorType.DataSource = ds.Tables[0];
                //ddlDistributorType.DataValueField = "ID";
                //ddlDistributorType.DataTextField = "Name";
                //ddlDistributorType.DataBind();
                //ddlDistributorType.Items.Insert(0, new ListItem("---Select Client Type---", "0"));

                ddlCountry.DataSource = ds.Tables[1];
                ddlCountry.DataValueField = "ID";
                ddlCountry.DataTextField = "Name";
                ddlCountry.DataBind();
                ddlCountry.Items.Insert(0, new ListItem("---Select Country---", "0"));
                ddlCountry.SelectedValue = "435";

                ddlTarrifGroup.DataSource = ds.Tables[3];
                ddlTarrifGroup.DataValueField = "TariffGroupID";
                ddlTarrifGroup.DataTextField = "TariffGroup";
                ddlTarrifGroup.DataBind();
                ddlTarrifGroup.Items.Insert(0, new ListItem("---Select Plan Group---", "0"));

                RepeaterTariff.DataSource = ds.Tables[2];
                RepeaterTariff.DataBind();
                for (int i = 0; i < RepeaterTariff.Items.Count; i++)
                {
                    CheckBox chk = (CheckBox)RepeaterTariff.Items[i].FindControl("CheckBox2");
                    RadioButton rbtn = (RadioButton)RepeaterTariff.Items[i].FindControl("RadioButton1");
                    rbtn.Checked = true;
                    break;
                }
            }

            //Distributor[] dis = null;
            //dis = svc.GetDistributorDDLService(0);
            //if (dis != null)
            //{
            //ddlParent.DataSource = dis;
            //ddlParent.DataValueField = "distributorID";
            //ddlParent.DataTextField = "distributorName";
            //ddlParent.DataBind();
            //ddlParent.Items.Insert(0, new ListItem("---Select Client Type---", "0"));
            //}
        }

        public void BindTariff(int DistributorID)
        {
            int LoginID = Convert.ToInt32(Session["LoginID"]);
            DataSet ds = svc.GetTariffService(LoginID, DistributorID);
            if (ds != null)
            {
                RepeaterTariff.DataSource = ds.Tables[0];
                RepeaterTariff.DataBind();

                rptH2O.DataSource = ds.Tables[1];
                rptH2O.DataBind();

                rptEasyGo.DataSource = ds.Tables[2];
                rptEasyGo.DataBind();


                rptUltraMobile.DataSource = ds.Tables[3];
                rptUltraMobile.DataBind();

                rptATT.DataSource = ds.Tables[5];
                rptATT.DataBind();

                //Ankit Singh
                for (int i = 0; i < RepeaterTariff.Items.Count; i++)
                {
                    CheckBox chk = (CheckBox)RepeaterTariff.Items[i].FindControl("CheckBox2");
                    if (Convert.ToInt32(ds.Tables[0].Rows[i]["IsDefault"]) == 1)
                    {
                        chk.Checked = true;
                    }
                    else
                    {
                        chk.Checked = false;
                    }
                    RadioButton rbtn = (RadioButton)RepeaterTariff.Items[i].FindControl("RadioButton1");
                    rbtn.Checked = true;
                    break;
                }


                for (int i = 0; i < rptH2O.Items.Count; i++)
                {
                    CheckBox chk = (CheckBox)rptH2O.Items[i].FindControl("CheckBox2");
                    RadioButton rbtn = (RadioButton)rptH2O.Items[i].FindControl("RadioButton1");
                    rbtn.Checked = true;
                    break;
                }


                for (int i = 0; i < rptEasyGo.Items.Count; i++)
                {
                    CheckBox chk = (CheckBox)rptEasyGo.Items[i].FindControl("CheckBox2");
                    RadioButton rbtn = (RadioButton)rptEasyGo.Items[i].FindControl("RadioButton1");
                    rbtn.Checked = true;
                    break;
                }

                for (int i = 0; i < rptUltraMobile.Items.Count; i++)
                {
                    CheckBox chk = (CheckBox)rptUltraMobile.Items[i].FindControl("CheckBox2");
                    RadioButton rbtn = (RadioButton)rptUltraMobile.Items[i].FindControl("RadioButton1");
                    rbtn.Checked = true;
                    break;
                }

                for (int i = 0; i < rptATT.Items.Count; i++)
                {
                    CheckBox chk = (CheckBox)rptATT.Items[i].FindControl("CheckBox2");
                    RadioButton rbtn = (RadioButton)rptATT.Items[i].FindControl("RadioButton1");
                    rbtn.Checked = true;
                    break;
                }

            }


        }

        public void ResetControl(int cond)
        {
            if (cond == 1)
            {
                txtEasyGoPerRecharge.Text = "";
                txth20PerRecharge.Text = "";
                txtLycaPerRecharge.Text = "";

                txtUltraMobileReCharge.Text = "";
                txtATTRecharge.Text = "";

                txtDistributorName.Text = "";
                txtCode.Text = "";
                txtContactPerson.Text = "";
                txtContactNo.Text = "";
                txtWebSiteName.Text = "";
                txtEmailID.Text = "";
                txtaddress.Text = "";
                txtCity.Text = "";
                txtState.Text = "";
                txtzip.Text = "";
                ddlCountry.SelectedIndex = 0;
                ddlTarrifGroup.SelectedIndex = 0;
                txtEin.Text = "";
                txtUserID.Text = "";
                txtSSN.Text = "";
                txtPan.Text = "";
                txtAccountBalance.Text = "0";
                CheckBox1.Checked = true;
                for (int i = 0; i < RepeaterTariff.Items.Count; i++)
                {
                    CheckBox chk = (CheckBox)RepeaterTariff.Items[i].FindControl("CheckBox2");
                    RadioButton rbtn = (RadioButton)RepeaterTariff.Items[i].FindControl("RadioButton1");
                    TextBox txtRental = (TextBox)RepeaterTariff.Items[i].FindControl("txtRental");
                    chk.Checked = false;
                    txtRental.Attributes.Add("resdonly", "true");

                }

                for (int i = 0; i < rptH2O.Items.Count; i++)
                {
                    CheckBox chk = (CheckBox)rptH2O.Items[i].FindControl("CheckBox2");
                    RadioButton rbtn = (RadioButton)rptH2O.Items[i].FindControl("RadioButton1");
                    TextBox txtRental = (TextBox)rptH2O.Items[i].FindControl("txtRental");
                    chk.Checked = false;
                    txtRental.Attributes.Add("resdonly", "true");

                }


                for (int i = 0; i < rptEasyGo.Items.Count; i++)
                {
                    CheckBox chk = (CheckBox)rptEasyGo.Items[i].FindControl("CheckBox2");
                    RadioButton rbtn = (RadioButton)rptEasyGo.Items[i].FindControl("RadioButton1");
                    TextBox txtRental = (TextBox)rptEasyGo.Items[i].FindControl("txtRental");
                    chk.Checked = false;
                    txtRental.Attributes.Add("resdonly", "true");

                }


                for (int i = 0; i < rptUltraMobile.Items.Count; i++)
                {
                    CheckBox chk = (CheckBox)rptUltraMobile.Items[i].FindControl("CheckBox2");
                    RadioButton rbtn = (RadioButton)rptUltraMobile.Items[i].FindControl("RadioButton1");
                    TextBox txtRental = (TextBox)rptUltraMobile.Items[i].FindControl("txtRental");
                    chk.Checked = false;
                    txtRental.Attributes.Add("resdonly", "true");

                }

                for (int i = 0; i < rptATT.Items.Count; i++)
                {
                    CheckBox chk = (CheckBox)rptATT.Items[i].FindControl("CheckBox2");
                    RadioButton rbtn = (RadioButton)rptATT.Items[i].FindControl("RadioButton1");
                    TextBox txtRental = (TextBox)rptATT.Items[i].FindControl("txtRental");
                    chk.Checked = false;
                    txtRental.Attributes.Add("resdonly", "true");

                }


            }
            if (cond == 2)
            {
                txtDistributorName.Attributes.Add("readonly", "true");
                txtCode.Attributes.Add("readonly", "true");

                txtEasyGoPerRecharge.Attributes.Add("readonly", "true");
                txth20PerRecharge.Attributes.Add("readonly", "true");
                txtLycaPerRecharge.Attributes.Add("readonly", "true");

                txtUltraMobileReCharge.Attributes.Add("readonly", "true");

                txtATTRecharge.Attributes.Add("readonly", "true");





                txtContactPerson.Attributes.Add("readonly", "true");
                txtContactNo.Attributes.Add("readonly", "true");
                txtWebSiteName.Attributes.Add("readonly", "true");
                txtEmailID.Attributes.Add("readonly", "true");
                txtaddress.Attributes.Add("readonly", "true");
                txtCity.Attributes.Add("readonly", "true");
                txtState.Attributes.Add("readonly", "true");
                txtzip.Attributes.Add("readonly", "true");
                CheckBox1.Enabled = false;
                ddlCountry.Attributes.Add("disabled", "disabled");
                txtEin.Attributes.Add("readonly", "true");
                txtSSN.Attributes.Add("readonly", "true");
                txtPan.Attributes.Add("readonly", "true");
                txtAccountBalance.Attributes.Add("readonly", "true");
                //ddlDistributorType.Attributes.Add("disabled", "disabled");
                //ddlParent.Attributes.Add("disabled", "disabled");                

            }
            if (cond == 3)
            {
                txtEasyGoPerRecharge.Attributes.Add("readonly", "true");
                txth20PerRecharge.Attributes.Add("readonly", "true");
                txtLycaPerRecharge.Attributes.Add("readonly", "true");
                txtUltraMobileReCharge.Attributes.Add("readonly", "true");
                txtATTRecharge.Attributes.Add("readonly", "true");


                txtDistributorName.Attributes.Add("readonly", "true");
                txtCode.Attributes.Add("readonly", "true");
                txtContactPerson.Attributes.Add("readonly", "true");
                txtContactNo.Attributes.Add("readonly", "true");
                txtWebSiteName.Attributes.Add("readonly", "true");
                txtEmailID.Attributes.Add("readonly", "true");
                txtaddress.Attributes.Add("readonly", "true");
                txtCity.Attributes.Add("readonly", "true");
                txtState.Attributes.Add("readonly", "true");
                txtzip.Attributes.Add("readonly", "true");
                CheckBox1.Enabled = false;
                ddlCountry.Attributes.Add("disabled", "disabled");
                txtEin.Attributes.Add("readonly", "true");
                txtSSN.Attributes.Add("readonly", "true");
                txtPan.Attributes.Add("readonly", "true");
                txtAccountBalance.Attributes.Add("readonly", "true");
                //ddlDistributorType.Attributes.Add("disabled", "disabled");
                //ddlParent.Attributes.Add("disabled", "disabled");

                for (int i = 0; i < RepeaterTariff.Items.Count; i++)
                {
                    CheckBox chk = (CheckBox)RepeaterTariff.Items[i].FindControl("CheckBox2");
                    RadioButton rbtn = (RadioButton)RepeaterTariff.Items[i].FindControl("RadioButton1");
                    TextBox txtRental = (TextBox)RepeaterTariff.Items[i].FindControl("txtRental");
                    chk.Enabled = false;
                    rbtn.Enabled = false;
                    txtRental.Attributes.Add("readonly", "true");
                }

                for (int i = 0; i < rptH2O.Items.Count; i++)
                {
                    CheckBox chk = (CheckBox)rptH2O.Items[i].FindControl("CheckBox2");
                    RadioButton rbtn = (RadioButton)rptH2O.Items[i].FindControl("RadioButton1");
                    TextBox txtRental = (TextBox)rptH2O.Items[i].FindControl("txtRental");
                    chk.Enabled = false;
                    rbtn.Enabled = false;
                    txtRental.Attributes.Add("readonly", "true");
                }


                for (int i = 0; i < rptEasyGo.Items.Count; i++)
                {
                    CheckBox chk = (CheckBox)rptEasyGo.Items[i].FindControl("CheckBox2");
                    RadioButton rbtn = (RadioButton)rptEasyGo.Items[i].FindControl("RadioButton1");
                    TextBox txtRental = (TextBox)rptEasyGo.Items[i].FindControl("txtRental");
                    chk.Enabled = false;
                    rbtn.Enabled = false;
                    txtRental.Attributes.Add("readonly", "true");
                }




                for (int i = 0; i < rptUltraMobile.Items.Count; i++)
                {
                    CheckBox chk = (CheckBox)rptUltraMobile.Items[i].FindControl("CheckBox2");
                    RadioButton rbtn = (RadioButton)rptUltraMobile.Items[i].FindControl("RadioButton1");
                    TextBox txtRental = (TextBox)rptUltraMobile.Items[i].FindControl("txtRental");
                    chk.Enabled = false;
                    rbtn.Enabled = false;
                    txtRental.Attributes.Add("readonly", "true");
                }
                for (int i = 0; i < rptATT.Items.Count; i++)
                {
                    CheckBox chk = (CheckBox)rptATT.Items[i].FindControl("CheckBox2");
                    RadioButton rbtn = (RadioButton)rptATT.Items[i].FindControl("RadioButton1");
                    TextBox txtRental = (TextBox)rptATT.Items[i].FindControl("txtRental");
                    chk.Enabled = false;
                    rbtn.Enabled = false;
                    txtRental.Attributes.Add("readonly", "true");
                }

            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int a = 0;
            int loginid = Convert.ToInt32(Session["LoginId"]);
            try
            {
                Boolean DefaultTariff = false;
                DataTable dt = new DataTable();
                dt.TableName = "dtTariff";
                dt.Columns.Add("TariffId", typeof(int));
                dt.Columns.Add("Default", typeof(int));
                dt.Columns.Add("Rental", typeof(decimal));
                dt.Columns.Add("NetworkID", typeof(int));

                for (int i = 0; i < RepeaterTariff.Items.Count; i++)
                {
                    CheckBox chk = (CheckBox)RepeaterTariff.Items[i].FindControl("CheckBox2");
                    RadioButton rbtn = (RadioButton)RepeaterTariff.Items[i].FindControl("RadioButton1");
                    HiddenField hddnTariffID = (HiddenField)RepeaterTariff.Items[i].FindControl("hdnTariffId");
                    TextBox txtRental = (TextBox)RepeaterTariff.Items[i].FindControl("txtRental");
                    if (chk.Checked == true)
                    {
                        DataRow dr = dt.NewRow();
                        dr["TariffId"] = Convert.ToInt32(hddnTariffID.Value);
                        dr["NetworkID"] = 13;

                        string ss = txtRental.Text.Trim();
                        if (ss == "")
                        {
                            ss = "0.0";
                        }
                        dr["Rental"] = Convert.ToDecimal(ss);
                        if (rbtn.Checked == true)
                        {
                            dr["Default"] = "1";
                            DefaultTariff = true;
                        }
                        else
                        {
                            dr["Default"] = "0";
                        }
                        dt.Rows.Add(dr);
                        dt.AcceptChanges();
                    }

                }
           
                if (dt.Rows.Count == 0)
                {
                    ShowPopUpMsg("Please Select Tariff");
                    return;
                }

                if (DefaultTariff == false)
                {
                    ShowPopUpMsg("Please Select a Tariff as Default");
                    return;
                }


                DataTable dtRecharge = new DataTable();
                dtRecharge.TableName = "dtRecharge";

                dtRecharge.Columns.Add("NetworkID", typeof(int));
                dtRecharge.Columns.Add("RechargePer", typeof(decimal));

                //lycaPerRecharge

                DataRow dr1 = dtRecharge.NewRow();
                dr1["NetworkID"] = "13";
                if (txtLycaPerRecharge.Text != "")
                {
                    dr1["RechargePer"] = Convert.ToDecimal(txtLycaPerRecharge.Text.Trim());
                }
                else { dr1["RechargePer"] = 0; }
                dtRecharge.Rows.Add(dr1);



                dtRecharge.AcceptChanges();

                string root1 = "";
                if (fileUploadDocument.HasFile)
                {
                    if (fileUploadDocument.FileName.Contains(".doc") || fileUploadDocument.FileName.Contains(".docx") || fileUploadDocument.FileName.Contains(".pdf") || fileUploadDocument.FileName.Contains(".jpeg") || fileUploadDocument.FileName.Contains(".png") || fileUploadDocument.FileName.Contains(".pdf") || fileUploadDocument.FileName.Contains(".jpg") || fileUploadDocument.FileName.Contains(".DOC") || fileUploadDocument.FileName.Contains(".DOCX") || fileUploadDocument.FileName.Contains(".JPEG") || fileUploadDocument.FileName.Contains(".PNG") || fileUploadDocument.FileName.Contains(".PDF") || fileUploadDocument.FileName.Contains(".JPG"))
                    {
                        string fileName = txtDistributorName.Text.Trim() + DateTime.Now.Date.ToString("yyyyMMdd") + DateTime.Now.Minute + DateTime.Now.Millisecond + fileUploadDocument.FileName;
                        root1 = "http://" + HttpContext.Current.Request.Url.Host + HttpContext.Current.Request.ApplicationPath + "/Documents/" + fileName;
                        fileUploadDocument.SaveAs(Server.MapPath("Documents/" + fileName));
                    }
                    else
                    {
                        ShowPopUpMsg("Please upload document in pdf/doc/docx/jpg/jpeg/png format");
                        return;
                    }
                }
                else
                {
                    ShowPopUpMsg("Please Upload the document");
                    return;
                }


                string root2 = "";
                if (fileUploadCertificate.HasFile)
                {
                    if (fileUploadCertificate.FileName.Contains(".doc") || fileUploadCertificate.FileName.Contains(".docx") || fileUploadCertificate.FileName.Contains(".jpeg") || fileUploadCertificate.FileName.Contains(".png") || fileUploadCertificate.FileName.Contains(".pdf") || fileUploadCertificate.FileName.Contains(".jpg") || fileUploadCertificate.FileName.Contains(".DOC") || fileUploadCertificate.FileName.Contains(".DOCX") || fileUploadCertificate.FileName.Contains(".JPEG") || fileUploadCertificate.FileName.Contains(".PNG") || fileUploadCertificate.FileName.Contains(".PDF") || fileUploadCertificate.FileName.Contains(".JPG"))
                    {
                        string fileName = txtDistributorName.Text.Trim() + DateTime.Now.Date.ToString("yyyyMMdd") + DateTime.Now.Minute + DateTime.Now.Millisecond + fileUploadCertificate.FileName;
                        root2 = "http://" + HttpContext.Current.Request.Url.Host + HttpContext.Current.Request.ApplicationPath + "/Certificate/" + fileName;
                        fileUploadCertificate.SaveAs(Server.MapPath("Certificate/" + fileName));
                    }
                    else
                    {
                        ShowPopUpMsg("Please upload Certificate in pdf/doc/docx/jpg/jpeg/png format");
                        return;
                    }
                }
                else
                {
                    ShowPopUpMsg("Please Upload the Certificate");
                    return;
                }

                Distributor dst = new Distributor();
                dst.distributorName = txtDistributorName.Text.Trim();
                dst.distributorCode = 0;
                dst.companyType = 2;
                dst.parent = Convert.ToInt32(Session["DistributorID"]);              
                if (Convert.ToInt32(Session["DistributorID"]) == 0)
                {
                    ShowPopUpMsg("Session Out");
                    return;
                }
                dst.contactPerson = txtContactPerson.Text.Trim();
                dst.contactNo = txtContactNo.Text.Trim();
                dst.webSiteName = txtWebSiteName.Text.Trim();
                dst.emailID = txtEmailID.Text.Trim();

                dst.address = txtaddress.Text.Trim();
                dst.city = txtCity.Text.Trim();
                dst.state = txtState.Text.Trim();
                dst.zip = txtzip.Text.Trim();
                dst.countryid = Convert.ToInt32(ddlCountry.SelectedValue);
                dst.TariffGroupID = Convert.ToInt32(ddlTarrifGroup.SelectedValue);
                dst.isActive = CheckBox1.Checked;
                dst.balanceAmount = 0;
                dst.EIN = txtEin.Text.Trim();
                dst.SSN = txtSSN.Text.Trim();
                dst.PanNumber = txtPan.Text.Trim();
                dst.Document = Convert.ToString(root1);
                dst.Certificate = Convert.ToString(root2);
                string Username = txtUserID.Text;
                string passw = txtPassword.Text;
                dst.Password = Encryption.Encrypt(passw);
                DataSet dsCHECK = svc.CHECKDistributor(Username);
                int ChkSellr = ChkSeller.Checked ? 1 : 0;
                int Chktariffgroup = Chktariffgrp.Checked ? 1 : 0;

                //Check duplicate Tax ID
                if (txtEin.Text != "")
                {
                    DataSet dsTaxId = svc.checkTaxId(txtEin.Text, 0);
                    if (dsTaxId != null && dsTaxId.Tables[0].Rows.Count > 0)
                    {
                        string distributor = dsTaxId.Tables[0].Rows[0]["Distributor"].ToString();
                        ShowPopUpMsg("This tax id is already linked with another distributor");
                        return;
                    }
                }
                // To make taxid Mandatory //
                if (txtEin.Text == "")
                {
                    ShowPopUpMsg("Tax Id is Mandatory for the process");
                    return;
                }
                ////

                DataSet ds = null;
                if (dsCHECK.Tables[0].Rows.Count == 0)
                {
                    ds = svc.AddDistirbutorService(dst, loginid, dt, dtRecharge, Username, ChkSellr, Chktariffgroup);

                }
                else
                {
                    ShowPopUpMsg("User Already Exists");
                }
                if (ds != null || ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Columns["Error"] != null)
                        {
                            string ErrorMsg = Convert.ToString(ds.Tables[0].Rows[0]["Error"]);
                            ShowPopUpMsg(ErrorMsg);
                        }

                        else
                        {
                            string resp = Convert.ToString(ds.Tables[0].Rows[0]["Response"]);
                            string userid = Convert.ToString(ds.Tables[0].Rows[0]["Name"]);
                            string pass = Convert.ToString(ds.Tables[0].Rows[0]["Password"]);
                            if (resp == "success")
                            {
                                string LoginUrl = "https://" + HttpContext.Current.Request.Url.Host + HttpContext.Current.Request.ApplicationPath + "Login.aspx";
                                SendMail(txtEmailID.Text.Trim(), "Login Detail", txtDistributorName.Text.Trim(), userid, passw, LoginUrl);
                                ResetControl(1);
                                ScriptManager.RegisterStartupScript(this, GetType(), "", "JScriptConfirmationPopupBox();", true);

                            }
                            else
                            {
                                ShowPopUpMsg("Distributor Added UnSuccessfully Please Try Again");
                            }
                        }
                    }
                    else
                    {
                        ShowPopUpMsg("Distributor Added UnSuccessfully Please Try Again");
                    }
                }
                else
                {
                    ShowPopUpMsg("Distributor Added UnSuccessfully Please Try Again");
                }
            }
            catch (Exception ex)
            {
                ShowPopUpMsg(ex.Message);
            }

        }

        protected string genPass()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            var result = new string(
                Enumerable.Repeat(chars, 16)
                          .Select(s => s[random.Next(s.Length)])
                          .ToArray());
            return result;
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            ResetControl(1);
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            int a = 0;
            try
            {
                if (txtAccountBalance.Text.Trim() == "")
                {
                    ShowPopUpMsg("Account Balance Can't be Blank");
                    return;
                }

                Boolean DefaultTariff = false;
                DataTable dt = new DataTable();
                dt.TableName = "dtTariff";
                dt.Columns.Add("TariffId", typeof(int));
                dt.Columns.Add("Default", typeof(int));
                dt.Columns.Add("Rental", typeof(decimal));
                dt.Columns.Add("NetworkID", typeof(int));

                for (int i = 0; i < RepeaterTariff.Items.Count; i++)
                {
                    CheckBox chk = (CheckBox)RepeaterTariff.Items[i].FindControl("CheckBox2");
                    RadioButton rbtn = (RadioButton)RepeaterTariff.Items[i].FindControl("RadioButton1");
                    HiddenField hddnTariffID = (HiddenField)RepeaterTariff.Items[i].FindControl("hdnTariffId");
                    TextBox txtRental = (TextBox)RepeaterTariff.Items[i].FindControl("txtRental");
                    if (chk.Checked == true)
                    {
                        DataRow dr = dt.NewRow();
                        dr["TariffId"] = Convert.ToInt32(hddnTariffID.Value);
                        dr["NetworkID"] = 13;
                        string ss = txtRental.Text.Trim();
                        if (ss == "")
                        {
                            ss = "0.0";
                        }
                        dr["Rental"] = Convert.ToDecimal(ss);
                        if (rbtn.Checked == true)
                        {
                            dr["Default"] = "1";
                            DefaultTariff = true;
                        }
                        else
                        {
                            dr["Default"] = "0";
                        }
                        dt.Rows.Add(dr);
                        dt.AcceptChanges();
                    }

                }
                DataTable dtRecharge = new DataTable();
                dtRecharge.TableName = "dtRecharge";

                dtRecharge.Columns.Add("NetworkID", typeof(int));
                dtRecharge.Columns.Add("RechargePer", typeof(decimal));

                DataRow dr1 = dtRecharge.NewRow();
                dr1["NetworkID"] = "13";
                if (txtLycaPerRecharge.Text != "")
                {
                    dr1["RechargePer"] = Convert.ToDecimal(txtLycaPerRecharge.Text.Trim());
                }
                else { dr1["RechargePer"] = 0; }
                dtRecharge.Rows.Add(dr1);

                dtRecharge.AcceptChanges();
                string root1 = "";
                if (fileUploadDocument.HasFile)
                {
                    if (fileUploadDocument.FileName.Contains(".doc") || fileUploadDocument.FileName.Contains(".docx") || fileUploadDocument.FileName.Contains(".pdf") || fileUploadDocument.FileName.Contains(".jpeg") || fileUploadDocument.FileName.Contains(".png") || fileUploadDocument.FileName.Contains(".pdf") || fileUploadDocument.FileName.Contains(".jpg") || fileUploadDocument.FileName.Contains(".DOC") || fileUploadDocument.FileName.Contains(".DOCX") || fileUploadDocument.FileName.Contains(".JPEG") || fileUploadDocument.FileName.Contains(".PNG") || fileUploadDocument.FileName.Contains(".PDF") || fileUploadDocument.FileName.Contains(".JPG"))
                    {

                        string fileName = txtDistributorName.Text.Trim() + DateTime.Now.Date.ToString("yyyyMMdd") + DateTime.Now.Minute + DateTime.Now.Millisecond + fileUploadDocument.FileName;
                        root1 = "http://" + HttpContext.Current.Request.Url.Host + HttpContext.Current.Request.ApplicationPath + "/Documents/" + fileName;
                        fileUploadDocument.SaveAs(Server.MapPath("Documents/" + fileName));
                    }
                    else
                    {
                        ShowPopUpMsg("Please upload document in pdf/doc/docx/jpg/jpeg/png format");
                        return;
                    }
                }


                string root2 = "";
                if (fileUploadCertificate.HasFile)
                {
                    if (fileUploadCertificate.FileName.Contains(".doc") || fileUploadCertificate.FileName.Contains(".docx") || fileUploadCertificate.FileName.Contains(".pdf") || fileUploadCertificate.FileName.Contains(".jpeg") || fileUploadCertificate.FileName.Contains(".png") || fileUploadCertificate.FileName.Contains(".pdf") || fileUploadCertificate.FileName.Contains(".jpg") || fileUploadCertificate.FileName.Contains(".DOC") || fileUploadCertificate.FileName.Contains(".DOCX") || fileUploadCertificate.FileName.Contains(".JPEG") || fileUploadCertificate.FileName.Contains(".PNG") || fileUploadCertificate.FileName.Contains(".PDF") || fileUploadCertificate.FileName.Contains(".JPG"))
                    {

                        string fileName = txtDistributorName.Text.Trim() + DateTime.Now.Date.ToString("yyyyMMdd") + DateTime.Now.Minute + DateTime.Now.Millisecond + fileUploadCertificate.FileName;
                        root2 = "http://" + HttpContext.Current.Request.Url.Host + HttpContext.Current.Request.ApplicationPath + "/Certificate/" + fileName;
                        fileUploadCertificate.SaveAs(Server.MapPath("Certificate/" + fileName));
                    }

                    else
                    {
                        ShowPopUpMsg("Please upload Certificate in pdf/doc/docx/jpg/jpeg/png format");
                        return;
                    }
                }

                Distributor dst = new Distributor();
                dst.distributorID = Convert.ToInt32(hddDistributorID.Value);
                dst.distributorName = txtDistributorName.Text.Trim();
                dst.contactPerson = txtContactPerson.Text.Trim();
                dst.contactNo = txtContactNo.Text.Trim();
                dst.webSiteName = txtWebSiteName.Text.Trim();
                dst.emailID = txtEmailID.Text.Trim();
                dst.address = txtaddress.Text.Trim();
                dst.city = txtCity.Text.Trim();
                dst.state = txtState.Text.Trim();
                dst.zip = txtzip.Text.Trim();
                dst.countryid = Convert.ToInt32(ddlCountry.SelectedValue);
                //dst.TariffGroupID = Convert.ToInt32(ddlTarrifGroup.SelectedValue);
                dst.isActive = CheckBox1.Checked;
                dst.EIN = txtEin.Text.Trim();
                dst.SSN = txtSSN.Text.Trim();
                dst.PanNumber = txtPan.Text.Trim();
                dst.Document = Convert.ToString(root1);
                dst.Certificate = Convert.ToString(root2);
                dst.TariffGroupID = Convert.ToInt32(ddlTarrifGroup.SelectedValue);
                if (txtAccountBalance.Text == "")
                {
                    dst.balanceAmount = 0;
                }
                else
                {
                    //ankit singh
                    dst.balanceAmount = Convert.ToDecimal(txtAccountBalance.Text);
                }
                string Username = txtUserID.Text;
                string pass = txtPassword.Text;
                string passw = Encryption.Encrypt(pass);

                int ChkSellr = ChkSeller.Checked ? 1 : 0;
                int Chktariffgroup = Chktariffgrp.Checked ? 1 : 0;
                if (txtEin.Text != "")
                {
                    DataSet dsTaxId = svc.checkTaxId(txtEin.Text, Convert.ToInt64(hddDistributorID.Value));
                    if (dsTaxId != null && dsTaxId.Tables[0].Rows.Count > 0)
                    {
                        string distributor = dsTaxId.Tables[0].Rows[0]["Distributor"].ToString();
                        ShowPopUpMsg("This tax id is already linked with another distributor");
                        return;
                    }
                }
                // To make taxid Mandatory //
                if (txtEin.Text == "")
                {
                    ShowPopUpMsg("Tax Id is Mandatory for the process");
                    return;
                }
                ////

                a = svc.UpdateDistirbutorService(dst, 1, dt, dtRecharge, Username, passw, ChkSellr, Chktariffgroup);
                if (a > 0)
                {
                    ResetControl(1);
                    btnUpdate.Visible = false;
                    liBack.Visible = true;
                    liUpdate.Visible = false;
                    ScriptManager.RegisterStartupScript(this, GetType(), "", "JScriptConfirmationUpdatePopupBox();", true);

                }
                else
                {
                    ShowPopUpMsg("Distributor Updated UnSuccessfully Please Try Again");
                }
            }
            catch (Exception ex)
            {
                ShowPopUpMsg(ex.Message);
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("DistributorView.aspx");
        }

        private void ShowPopUpMsg(string msg)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("alert('");
            sb.Append(msg.Replace("\n", "\\n").Replace("\r", "").Replace("'", "\\'"));
            sb.Append("');");
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "showalert", sb.ToString(), true);
        }

        protected void chkAll_OnCheckedChanged(object sender, EventArgs e)
        {
            CheckBox chkAll = (CheckBox)sender;
            RepeaterItem row = (RepeaterItem)chkAll.NamingContainer;
            int a = row.ItemIndex;
            if (chkAll.Checked == true)
            {
                for (int i = 0; i < RepeaterTariff.Items.Count; i++)
                {
                    CheckBox chk = (CheckBox)RepeaterTariff.Items[i].FindControl("CheckBox2");
                    chk.Checked = true;
                }
            }
            if (chkAll.Checked == false)
            {
                for (int i = 0; i < RepeaterTariff.Items.Count; i++)
                {
                    CheckBox chk = (CheckBox)RepeaterTariff.Items[i].FindControl("CheckBox2");
                    chk.Checked = false;
                }
            }

        }

        protected void RadioButton1_OnCheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = (RadioButton)sender;
            RepeaterItem row = (RepeaterItem)rb.NamingContainer;

            int a = row.ItemIndex;
            for (int i = 0; i < RepeaterTariff.Items.Count; i++)
            {
                RadioButton rbtn = (RadioButton)RepeaterTariff.Items[i].FindControl("RadioButton1");

                if (i != a)
                {
                    rbtn.Checked = false;
                }
            }

        }

        protected void chkAllH2O_OnCheckedChanged(object sender, EventArgs e)
        {
            CheckBox chkAll = (CheckBox)sender;
            RepeaterItem row = (RepeaterItem)chkAll.NamingContainer;
            int a = row.ItemIndex;
            if (chkAll.Checked == true)
            {
                for (int i = 0; i < rptH2O.Items.Count; i++)
                {
                    CheckBox chk = (CheckBox)rptH2O.Items[i].FindControl("CheckBox2");
                    chk.Checked = true;
                }
            }
            if (chkAll.Checked == false)
            {
                for (int i = 0; i < rptH2O.Items.Count; i++)
                {
                    CheckBox chk = (CheckBox)rptH2O.Items[i].FindControl("CheckBox2");
                    chk.Checked = false;
                }
            }

        }


        protected void chkAllEastGo_OnCheckedChanged(object sender, EventArgs e)
        {
            CheckBox chkAll = (CheckBox)sender;
            RepeaterItem row = (RepeaterItem)chkAll.NamingContainer;
            int a = row.ItemIndex;
            if (chkAll.Checked == true)
            {
                for (int i = 0; i < rptEasyGo.Items.Count; i++)
                {
                    CheckBox chk = (CheckBox)rptEasyGo.Items[i].FindControl("CheckBox2");
                    chk.Checked = true;
                }
            }
            if (chkAll.Checked == false)
            {
                for (int i = 0; i < rptEasyGo.Items.Count; i++)
                {
                    CheckBox chk = (CheckBox)rptEasyGo.Items[i].FindControl("CheckBox2");
                    chk.Checked = false;
                }
            }

        }
        protected void chkAllUltraMobile_OnCheckedChanged(object sender, EventArgs e)
        {
            CheckBox chkAll = (CheckBox)sender;
            RepeaterItem row = (RepeaterItem)chkAll.NamingContainer;
            int a = row.ItemIndex;
            if (chkAll.Checked == true)
            {
                for (int i = 0; i < rptUltraMobile.Items.Count; i++)
                {
                    CheckBox chk = (CheckBox)rptUltraMobile.Items[i].FindControl("CheckBox2");
                    chk.Checked = true;
                }
            }
            if (chkAll.Checked == false)
            {
                for (int i = 0; i < rptUltraMobile.Items.Count; i++)
                {
                    CheckBox chk = (CheckBox)rptUltraMobile.Items[i].FindControl("CheckBox2");
                    chk.Checked = false;
                }
            }

        }

        protected void chkAllATT_OnCheckedChanged(object sender, EventArgs e)
        {
            CheckBox chkAll = (CheckBox)sender;
            RepeaterItem row = (RepeaterItem)chkAll.NamingContainer;
            int a = row.ItemIndex;
            if (chkAll.Checked == true)
            {
                for (int i = 0; i < rptATT.Items.Count; i++)
                {
                    CheckBox chk = (CheckBox)rptATT.Items[i].FindControl("CheckBox2");
                    chk.Checked = true;
                }
            }
            if (chkAll.Checked == false)
            {
                for (int i = 0; i < rptATT.Items.Count; i++)
                {
                    CheckBox chk = (CheckBox)rptATT.Items[i].FindControl("CheckBox2");
                    chk.Checked = false;
                }
            }

        }

        protected void rdoAllH2O_OnCheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = (RadioButton)sender;
            RepeaterItem row = (RepeaterItem)rb.NamingContainer;

            int a = row.ItemIndex;
            for (int i = 0; i < rptH2O.Items.Count; i++)
            {
                RadioButton rbtn = (RadioButton)rptH2O.Items[i].FindControl("RadioButton1");

                if (i != a)
                {
                    rbtn.Checked = false;
                }
            }

        }


        protected void rdoAllEasyGo_OnCheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = (RadioButton)sender;
            RepeaterItem row = (RepeaterItem)rb.NamingContainer;

            int a = row.ItemIndex;
            for (int i = 0; i < rptEasyGo.Items.Count; i++)
            {
                RadioButton rbtn = (RadioButton)rptEasyGo.Items[i].FindControl("RadioButton1");

                if (i != a)
                {
                    rbtn.Checked = false;
                }
            }

        }


        protected void rdoAllUltraMobile_OnCheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = (RadioButton)sender;
            RepeaterItem row = (RepeaterItem)rb.NamingContainer;

            int a = row.ItemIndex;
            for (int i = 0; i < rptUltraMobile.Items.Count; i++)
            {
                RadioButton rbtn = (RadioButton)rptUltraMobile.Items[i].FindControl("RadioButton1");

                if (i != a)
                {
                    rbtn.Checked = false;
                }
            }

        }

        protected void rdoAllATT_OnCheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = (RadioButton)sender;
            RepeaterItem row = (RepeaterItem)rb.NamingContainer;

            int a = row.ItemIndex;
            for (int i = 0; i < rptATT.Items.Count; i++)
            {
                RadioButton rbtn = (RadioButton)rptUltraMobile.Items[i].FindControl("RadioButton1");

                if (i != a)
                {
                    rbtn.Checked = false;
                }
            }

        }






        protected void btnUpdateUp_Click(object sender, EventArgs e)
        {
            btnUpdate_Click(null, null);
        }

        protected void btnBackUp_Click(object sender, EventArgs e)
        {
            Response.Redirect("DistributorView.aspx");
        }

        public void SendMail(string SendTo, string Subject, string UserName, string UserID, string pass, string LoginUrl)
        {
            try
            {
                string LogoUrl = "https://" + HttpContext.Current.Request.Url.Host + HttpContext.Current.Request.ApplicationPath + "/img/logo.png";
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient();


                string MailAddress = ConfigurationManager.AppSettings.Get("Fromail");
                string PassWord = ConfigurationManager.AppSettings.Get("Password");

                mail.From = new MailAddress(MailAddress);
                mail.To.Add(SendTo);
                TimeSpan ts = new TimeSpan(8, 0, 0);
                mail.Subject = Subject + " " + DateTime.UtcNow.Subtract(ts).ToString();

                StringBuilder sb = new StringBuilder();
                sb.Append("<html>");
                sb.Append("<body style=”color:grey; font-size:15px;”>");
                sb.Append("<font face=”Helvetica, Arial, sans-serif”>");

                sb.Append("<div style=”position:absolute; height:200px; width:100px; background-color:0d1d36; padding:30px;”>");
                sb.Append("<img src=" + LogoUrl + " />");
                sb.Append("</div>");

                sb.Append("<br/>");

                sb.Append("<br/>");

                sb.Append("<div style=”background-color: #ece8d4; width:600px; height:200px; padding:30px; margin-top:30px;”>");
                //sb.Append("<p>Please find the new credentials and get started.</p>");

                sb.Append("<p>Dear " + UserName + ",<p>");

                sb.Append("<p>Please find the new credentials and get started.</p>");
                sb.Append("<p>");
                sb.Append("<br/>");

                sb.Append("Username: " + UserID + "<br>");
                sb.Append("Password: " + pass + "<br>");
                sb.Append("<br/>");
                sb.Append("<p>Your password is secure show don't share it</p>");

                sb.Append("<p>Use this link for login</p>");
                sb.Append("<p>" + LoginUrl + "</p>");
                sb.Append("<br/>");
                sb.Append("<p>Sincerely,");

                sb.Append("<p>" + ConfigurationManager.AppSettings.Get("COMPANY_NAME") + "</p>");
                sb.Append("<p>Thank you</p>");
                sb.Append("<p>----------------------------------------------------------------");
                sb.Append("<p>PROTECT YOUR PASSWORD");

                sb.Append("<p>NEVER give your password to anyone, including " + ConfigurationManager.AppSettings.Get("SHORT_COMPANY_NAME") + " employees. ");
                sb.Append("Protect yourself against fraudulent websites by opening a new web browser ");
                sb.Append("(e.g. Internet Explorer or Firefox) and typing in the " + ConfigurationManager.AppSettings.Get("SHORT_COMPANY_NAME") + " URL every time you log in to your account.");
                sb.Append("<p>----------------------------------------------------------------");
                sb.Append("<p>Please do not reply to this email. This mailbox is not monitored and you will not receive a response. ");

                sb.Append("<br/>");
                sb.Append("</div>");
                sb.Append("</body>");
                sb.Append("</html>");

                mail.Body = sb.ToString();
                mail.BodyEncoding = System.Text.Encoding.UTF8;
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.High;

                SmtpServer.Host = ConfigurationManager.AppSettings.Get("Host");
                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential(MailAddress, PassWord);
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);

            }
            catch (Exception ex)
            {

            }
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