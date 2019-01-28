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
    public partial class POS : System.Web.UI.Page
    {
        Service1Client ssc = new Service1Client();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                btnSave.Attributes.Add("disabled", "true");
                btnSaveUp.Attributes.Add("disabled", "true");

            }
        }

        protected void btnVerify_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtCurrentSIMNumber.Text != "" || txtCurrentMobileNumber.Text != "")
                {
                    //DataTable dtSimMobile = objcStockInController.GetSIMOrMobileNo(txtCurrentSimNo.Text.Trim(), txtCurrentMSISDNNo.Text.Trim());
                    string simno = Convert.ToString(txtCurrentSIMNumber.Text.Trim());
                    string mobileno = Convert.ToString(txtCurrentMobileNumber.Text.Trim());
                    if (simno == "")
                    {
                        simno = "No";

                    }
                    if (mobileno == "")
                    {
                        mobileno = "No";
                    }

                    DataSet ds = ssc.GetInventoryForSIMReplacement(mobileno, simno, Convert.ToInt32(Session["DistributorID"]));
                    if (ds != null)
                    {
                        if (ds.Tables.Count > 0)
                        {

                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                if (ds.Tables[0].Rows[0]["Action"].ToString() == "SIM is not activated.")
                                {
                                    ShowPopUpMsg("Sale can not be done as SIM is not activated.");
                                }
                                else
                                {
                                    txtCurrentSIMNumber.Text = ds.Tables[0].Rows[0]["SerialNumber"].ToString();
                                    txtCurrentMobileNumber.Text = ds.Tables[0].Rows[0]["MSISDN"].ToString();
                                    //hdnCurrentSIMID.Value = ds.Tables[0].Rows[0]["SIMID"].ToString();
                                    //hdnMobileID.Value = ds.Tables[0].Rows[0]["MSISDNID"].ToString();
                                    hdnMSISDN_SIM_ID.Value = ds.Tables[0].Rows[0]["MSISDN_SIM_ID"].ToString();

                                    //txtCurrentSIMNumber.ReadOnly = true;
                                    //txtCurrentMobileNumber.ReadOnly = true;
                                    txtCurrentSIMNumber.Enabled = false;
                                    txtCurrentMobileNumber.Enabled = false;

                                    //txtCurrentSIMNumber.Attributes.Add("ReadOnly","true");// = true;
                                    //txtCurrentMobileNumber.Attributes.Add("ReadOnly", "true");

                                    btnSave.Attributes.Remove("disabled");
                                    btnSaveUp.Attributes.Remove("disabled");
                                }

                            }
                            else
                            {
                                ShowPopUpMsg("SIM or Mobile does not exist.");
                            }
                        }
                    }
                }
                else
                {
                    ShowPopUpMsg("Please enter either Current SIM number or Mobile number");
                }
            }
            catch (Exception ex)
            {
 
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            Response.Redirect("POS.aspx");
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (hdnMSISDN_SIM_ID.Value == "")
                {
                    ShowPopUpMsg("Please Varify Sim Number Or Mobile Number First");
                    return;
                }

                SPOS sp = new SPOS();

                sp.MSISDN_SIM_ID = Convert.ToInt64(hdnMSISDN_SIM_ID.Value);
                sp.CoustomerName = txtCustomerName.Text.Trim();
                sp.Email = txtEmail.Text.Trim();
                sp.Address = txtAddress.Text.Trim();
                sp.MobileNumber = txtAlternateNumber.Text.Trim();
                int distr = Convert.ToInt32(Session["DistributorID"]);
                int LoginID = Convert.ToInt32(Session["LoginID"]);

                int a = ssc.SavePOSService(distr, LoginID,sp);
                if (a > 0)
                {
                    ShowPopUpMsg("POS Save Successfully");
                    ResetControl(1);
                }
                else
                {
                    ShowPopUpMsg("POS Save UnSuccessfully \n Please Try Again");
                }
            }
            catch(Exception ex)
            {
                ShowPopUpMsg("POS Save UnSuccessfully \n Please Try Again");
            }
        }

        public void ResetControl(int cond)
        {
            if (cond == 1)
            {
                txtCurrentMobileNumber.Text = "";
                txtCurrentSIMNumber.Text = "";
                txtCustomerName.Text = "";
                txtEmail.Text = "";
                txtAddress.Text = "";
                txtAlternateNumber.Text = "";
                hdnMSISDN_SIM_ID.Value = "";
                btnSave.Attributes.Add("disabled", "true");
               
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

        protected void btnBackUp_Click(object sender, EventArgs e)
        {
            Response.Redirect("POSView.aspx");
        }

        protected void btnSaveUp_Click(object sender, EventArgs e)
        {
            btnSave_Click(null, null);
        }

        protected void btnResetUp_Click(object sender, EventArgs e)
        {
            Response.Redirect("POS.aspx");
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("POSView.aspx");
        }

        

       
         

        
    }
}