using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using ENK.ServiceReference1;
namespace ENK
{
    public partial class Vendor : System.Web.UI.Page
    {
        Service1Client svc = new Service1Client();

        protected void Page_Load(object sender, EventArgs e)
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

            try
            {
                if (!Page.IsPostBack)
                {
                   // txtVendorCode.Attributes.Add("readonly", "true");

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
                        FillVendor(Identity);
                        ResetControl(2);
                        divActive.Visible = true;
                        btnReset.Visible = false;
                        btnSave.Visible = false;
                        btnBack.Visible = true;
                        btnUpdate.Visible = false;
                        liUpdate.Visible = false;
                        liBack.Visible = true;
                    }
                    if (Condition == "Edit")
                    {
                        FillVendor(Identity);
                        hddnVendorID.Value = Identity.ToString();
                        divActive.Visible = true;
                       // txtVendorCode.Attributes.Add("readonly", "true");
                        btnReset.Visible = false;
                        btnSave.Visible = false;
                        btnBack.Visible = true;
                        btnUpdate.Visible = true;
                        liUpdate.Visible = true;
                        liBack.Visible = true;
                    }
                    if (Condition == "")
                    {
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

        public void FillVendor(string Identity)
        {
            SVendor cv = new SVendor();
            cv.VendorID = Convert.ToInt32(Identity);
            DataSet ds = svc.GetSingleVendorService(cv);
            if (ds != null)
            {
                hddnVendorID.Value = Convert.ToString(ds.Tables[0].Rows[0]["VendorID"]);
                txtVendorName.Text = Convert.ToString(ds.Tables[0].Rows[0]["VendorName"]);
                txtVendorCode.Text = Convert.ToString(ds.Tables[0].Rows[0]["VendorCode"]);
                txtEmail.Text = Convert.ToString(ds.Tables[0].Rows[0]["VendorEmail"]);
                txtContactPerson.Text = Convert.ToString(ds.Tables[0].Rows[0]["ContactPerson"]);
                txtAddress.Text = Convert.ToString(ds.Tables[0].Rows[0]["VendorAddress"]);
                txtContactNumber.Text = Convert.ToString(ds.Tables[0].Rows[0]["VendorMobile"]);

                CheckBox1.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["FlagActive"]);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtVendorName.Text != "")
                {
                    int DistributorID = Convert.ToInt32(Session["DistributorID"]);
                    int LoginID = Convert.ToInt32(Session["LoginID"]);

                    SVendor vendr = new SVendor();
                    vendr.VendorName = txtVendorName.Text.Trim();
                    vendr.VendorCode = txtVendorCode.Text.Trim();
                    vendr.VendorEmail = txtEmail.Text.Trim();
                    vendr.VendorContactPerson = txtContactPerson.Text.Trim();
                    vendr.VendorAddress = txtAddress.Text.Trim();
                    vendr.VendorMobile = txtContactNumber.Text.Trim();
                    vendr.IsActive = CheckBox1.Checked;
                    int ClientTypeID = Convert.ToInt32(Session["ClientTypeID"]);
                    int a = svc.InsertVendorService(DistributorID, LoginID,ClientTypeID, vendr);

                    if (a > 0)
                    {
                        ShowPopUpMsg("Network Save Successfully");
                        ResetControl(1);
                    }
                    else
                    {
                        ShowPopUpMsg("Network Save Unsuccessfully \n Please Try Again");
                    }
                }
                else
                {
                    ShowPopUpMsg("Please Fill Network Name");
                }
            }
            catch (Exception ex)
            {
 
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (hddnVendorID.Value != "")
                {
                    int DistributorID = Convert.ToInt32(Session["DistributorID"]);
                    int LoginID = Convert.ToInt32(Session["LoginID"]);

                    SVendor vendr = new SVendor();
                    vendr.VendorID = Convert.ToInt32(hddnVendorID.Value);
                    vendr.VendorName = txtVendorName.Text.Trim();
                    vendr.VendorCode =txtVendorCode.Text;
                    vendr.VendorEmail = txtEmail.Text.Trim();
                    vendr.VendorContactPerson = txtContactPerson.Text.Trim();
                    vendr.VendorAddress = txtAddress.Text.Trim();
                    vendr.VendorMobile = txtContactNumber.Text.Trim();
                    vendr.IsActive = CheckBox1.Checked;
                    int ClientTypeID = Convert.ToInt32(Session["ClientTypeID"]);
                    int a = svc.UpdateVendorService(DistributorID, LoginID, ClientTypeID, vendr);

                    if (a > 0)
                    {
                        ShowPopUpMsg("Network Updated Successfully");
                        ResetControl(1);
                         
                        btnUpdate.Visible = false;
                        liUpdate.Visible = false;
                    }
                    else
                    {
                        ShowPopUpMsg("Network Updated Unsuccessfully \n Please Try Again");
                    }
                }
                else
                {
                    //ShowPopUpMsg("Please Fill Vendor Name");
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            
            ResetControl(1);
        }

        protected void btnUpdateUp_Click(object sender, EventArgs e)
        {
            btnUpdate_Click(null, null);
        }

        protected void btnBackUp_Click(object sender, EventArgs e)
        {
            Response.Redirect("VendorView.aspx");
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("VendorView.aspx");
        }

        public void ResetControl(int cond)
        {
            if (cond == 1)
            {
                txtVendorName.Text = "";
                txtVendorCode.Text = "";
                txtEmail.Text = "";
                txtContactPerson.Text = "";
                txtAddress.Text = "";
                txtContactNumber.Text = "";

                CheckBox1.Checked = true;

            }
            if (cond == 2)
            {
                txtVendorName.Attributes.Add("readonly", "true");
                txtVendorCode.Attributes.Add("readonly", "true");
                txtEmail.Attributes.Add("readonly", "true");
                txtContactPerson.Attributes.Add("readonly", "true");
                txtAddress.Attributes.Add("readonly", "true");
                txtContactNumber.Attributes.Add("readonly", "true");

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