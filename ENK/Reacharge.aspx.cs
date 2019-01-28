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
using ENK.net.emida.ws;

namespace ENK
{
    public partial class Reacharge : System.Web.UI.Page
    {
        Service1Client svc = new Service1Client();
        string RequestRes = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                try
                {
                    txtChannelID.Text = ConfigurationManager.AppSettings.Get("TXN_SERIES");
                    if (Request.QueryString.Count > 0)
                    {
                        long _logid = 0;
                        long.TryParse(Convert.ToString(Request.QueryString["lid"]), out _logid);

                        DataSet ds = new DataSet();
                        ds = svc.ValidateLoginApp(_logid);

                        if (ds != null)
                        {
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                Session["LoginID"] = Convert.ToString(ds.Tables[0].Rows[0]["ID"]);
                                Session["EmailID"] = Convert.ToString(ds.Tables[0].Rows[0]["EmailID"]);
                            }
                            else
                            {
                                Session["LoginID"] = null;
                                Session["EmailID"] = null;
                            }
                        }
                    }

                    if (Session["LoginID"] != null)
                    {

                        txtEmail.Text = Convert.ToString(Session["EmailID"]);
                        btnCompanyByAccount.Visible = true;

                        divCity.Attributes.Add("style", "display:none");

                        // ENK.net.emida.ws.webServicesService ws = new webServicesService();
                        //  string ss1 = ws.Login2("01", "clerkterst", "clerk1234", "1");
                        // string ss1 = ws.Login2("01", "A&HPrepaid", "95222", "1");

                        txtAmountPay.Attributes.Add("readonly", "true");

                        DataTable dt = svc.GetVendor(Convert.ToInt32(Session["LoginId"]));
                        if (dt != null)
                        {
                            if (dt.Rows.Count > 0)
                            {
                                ddlNetwork.DataSource = dt;
                                ddlNetwork.DataValueField = "VendorID";
                                ddlNetwork.DataTextField = "VendorName";
                                ddlNetwork.DataBind();
                                ddlNetwork.Items.Insert(0, new ListItem("Select Network", "0"));
                                //////
                                ddlNetwork.SelectedValue = "13";
                                BindProduct();
                                //////
                            }
                        }

                        // BindH2OProduct();


                        //  BindDDL();
                        divahannel.Visible = false;
                        divLanguage.Visible = false;


                        ENK.net.emida.ws.webServicesService ws = new webServicesService();
                        try
                        {
                            string ss1 = ws.Login2("01", "A&HPrepaid", "95222", "1");
                            // ws.Logout("01", "A&HPrepaid", "95222", "1");
                        }
                        catch (Exception ex)
                        {
                            ShowPopUpMsg(ex.Message);
                            return;

                        }



                    }
                  
                }
                catch (Exception ex)
                {

                }

            }
        }
      
        private void ShowPopUpMsg(string msg)
        {
            //StringBuilder sb = new StringBuilder();
            //sb.Append("alert('");
            //sb.Append(msg.Replace("\n", "\\n").Replace("\r", "").Replace("'", "\\'"));
            //sb.Append("');");
            //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "showalert", sb.ToString(), true);

           

            StringBuilder sb = new StringBuilder();
            sb.Append("$.alertable.alert('");
            sb.Append(msg.Replace("\n", "\\n").Replace("\r", "").Replace("'", "\\'"));
            sb.Append("').always(function () {console.log('Alert dismissed');});");
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "showalert", sb.ToString(), true);

        }
        protected void ddlNetwork_SelectedIndexChanged(object sender, EventArgs e)
        {

            try
            {
                DataTable dtSIMPurchase = new DataTable();
                txtRegulatry.Text = "0";
                txtAmt.Text = "0";
                lblCashback.Text = "";
                lblRechargePercentage.Text = "";
               
                txtAmt.Attributes.Add("readonly", "true");
                if (ddlNetwork.SelectedItem.Text == "H20")
                {
                     
                    divCity.Attributes.Add("style", "display:none");
                    
                    //lblProduct.Text = "Product";


                 // BindH2OProduct();
                    BindProduct();

                  txtAmountPay.Text = "";

                }
                else if (ddlNetwork.SelectedItem.Text == "AT&T")
                {

                    
                  
                  txtAmt.Attributes.Remove("readonly");
                 // txtAmt.Attributes.Add("readonly", "false");
                  BindProduct();
                   
                   
                }

                else if (ddlNetwork.SelectedItem.Text == "EasyGo")
                {
                     
                    divCity.Attributes.Add("style", "display:none");
                    
                    txtAmountPay.Text = "";
                    
                    BindProduct();
                }

                else
                {
                     
                    txtAmountPay.Text = "";
                    divCity.Attributes.Add("style", "display:none");
                    lblProduct.Text = "Tariff";
                    //BindlYCAProduct();
                    BindProduct();
                    



                }


            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        protected void ddlProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {


                if (ddlProduct.SelectedIndex > 0)
                {

                    DataTable dtSIMPurchase = new DataTable();
                    int distributorid = Convert.ToInt32(Session["DistributorID"]);


                    txtRegulatry.Text = "0";
                    txtAmt.Text = "0";



                    if (ddlNetwork.SelectedItem.Text == "H20")
                    {
                        dtSIMPurchase = (DataTable)ViewState["Product"];

                        DataView dv = dtSIMPurchase.DefaultView;
                        dv.RowFilter = "ProductCode  = " + Convert.ToInt32(ddlProduct.SelectedValue);
                        dtSIMPurchase = dv.ToTable();
                        string Amount = dtSIMPurchase.Rows[0]["Amount"].ToString();
                        ViewState["CurrencySymbol"] = dtSIMPurchase.Rows[0]["CurrencySymbol"].ToString();
                        if (Amount != "")
                        {
                            ViewState["AmountPay"] = Amount;
                            txtAmountPay.Text = Amount;

                            DataSet ds = svc.GetSingleDistributorTariffService(distributorid);


                            DataTable dtRecharge = new DataTable();


                            DataView dv1 = new DataView(ds.Tables[4]);
                            dv1.RowFilter = "NetworkID = " + Convert.ToString(ddlNetwork.SelectedValue) + "";
                            dtRecharge = dv1.ToTable();
                            if (dtRecharge.Rows.Count > 0)
                            {
                                double Amt = 0.0;
                               // Amt = Convert.ToDouble(((Convert.ToDecimal(txtAmountPay.Text)) - ((Convert.ToDecimal(txtAmountPay.Text) * Convert.ToDecimal(dtRecharge.Rows[0]["RechargePer"])) / 100)).ToString());
                                
                                Amt = Convert.ToDouble(Convert.ToDecimal(txtAmountPay.Text));
                                //Discount via Procedure level
                                 Amt = Math.Round(Amt, 2);
                                txtAmt.Text = Convert.ToString(Amt);
                                txtAmountPay.Text = Convert.ToString(Amt);


                                double CashBack = 0.0;
                                CashBack = Convert.ToDouble(((Convert.ToDecimal(txtAmountPay.Text) * Convert.ToDecimal(dtRecharge.Rows[0]["RechargePer"])) / 100).ToString());
                                lblCashback.Text = "After Rechage Success then You have Received Cachback  $ " + Convert.ToString(CashBack);

                                lblRechargePercentage.Text = " (Discounted Amount with " + Convert.ToDecimal(dtRecharge.Rows[0]["RechargePer"]).ToString() + "%)";
                            }
                            else
                            {

                                double Amt = 0.0;
                                Amt = Convert.ToDouble(Convert.ToDecimal(txtAmountPay.Text));
                                //Discount via Procedure level
                                
                                //Amt = Convert.ToDouble(((Convert.ToDecimal(txtAmountPay.Text)) - ((Convert.ToDecimal(txtAmountPay.Text) * 8) / 100)).ToString());
                               
                                Amt = Math.Round(Amt, 2);
                                txtAmt.Text = Convert.ToString(Amt);
                                txtAmountPay.Text = Convert.ToString(Amt);

                                // txtAmountPay.Text = ((Convert.ToDecimal(txtAmountPay.Text)) - ((Convert.ToDecimal(txtAmountPay.Text) * 8) / 100)).ToString();
                                lblCashback.Text = "";
                                lblRechargePercentage.Text = " (Discounted Amount with 0%)";
                            }
                        }
                        else
                        {
                            ViewState["AmountPay"] = "0";
                            txtAmountPay.Text = "0";
                        }

                    }
                    else if (ddlNetwork.SelectedItem.Text == "EasyGo")
                    {
                        dtSIMPurchase = (DataTable)ViewState["Product"];

                        DataView dv = dtSIMPurchase.DefaultView;
                        dv.RowFilter = "ProductCode  = " + Convert.ToInt32(ddlProduct.SelectedValue);
                        dtSIMPurchase = dv.ToTable();
                        string Amount = dtSIMPurchase.Rows[0]["Amount"].ToString();
                        ViewState["CurrencySymbol"] = dtSIMPurchase.Rows[0]["CurrencySymbol"].ToString();
                        if (Amount != "")
                        {
                            ViewState["AmountPay"] = Amount;
                            txtAmountPay.Text = Amount;

                            DataSet ds = svc.GetSingleDistributorTariffService(distributorid);


                            DataTable dtRecharge = new DataTable();


                            DataView dv1 = new DataView(ds.Tables[4]);
                            dv1.RowFilter = "NetworkID = " + Convert.ToString(ddlNetwork.SelectedValue) + "";
                            dtRecharge = dv1.ToTable();
                            if (dtRecharge.Rows.Count > 0)
                            {
                                double Amt = 0.0;
                               
                              
                                //Amt = Convert.ToDouble(((Convert.ToDecimal(txtAmountPay.Text)) - ((Convert.ToDecimal(txtAmountPay.Text) * Convert.ToDecimal(dtRecharge.Rows[0]["RechargePer"])) / 100)).ToString());
                                //-----Discount via Procedure level
                                Amt = Convert.ToDouble(Convert.ToDecimal(txtAmountPay.Text));
                                
                                
                                Amt = Math.Round(Amt, 2);
                                txtAmountPay.Text = Convert.ToString(Amt);
                                txtAmt.Text = Convert.ToString(Amt);
                                double CashBack = 0.0;
                                CashBack = Convert.ToDouble(((Convert.ToDecimal(txtAmountPay.Text) * Convert.ToDecimal(dtRecharge.Rows[0]["RechargePer"])) / 100).ToString());
                                lblCashback.Text = "After Rechage Success then You have Received Cachback  $ " + Convert.ToString(CashBack);

                                lblRechargePercentage.Text = " (Discounted Amount with " + Convert.ToDecimal(dtRecharge.Rows[0]["RechargePer"]).ToString() + "% extra)";
                            }
                            else
                            {
                                double Amt = 0.0;
                               // Amt = Convert.ToDouble(((Convert.ToDecimal(txtAmountPay.Text)) - ((Convert.ToDecimal(txtAmountPay.Text) * 8) / 100)).ToString());
                                //-----Discount via Procedure level
                                Amt = Convert.ToDouble(Convert.ToDecimal(txtAmountPay.Text));
                                
                                Amt = Math.Round(Amt, 2);
                                txtAmountPay.Text = Convert.ToString(Amt);
                                txtAmt.Text = Convert.ToString(Amt);
                                lblCashback.Text = "";
                                lblRechargePercentage.Text = " (Discounted Amount with 0%)";
                            }
                        }
                        else
                        {
                            ViewState["AmountPay"] = "0";
                            txtAmountPay.Text = "0";
                        }

                    }

                    else if (ddlNetwork.SelectedItem.Text == "Ultra Mobile")
                    {
                        dtSIMPurchase = (DataTable)ViewState["Product"];

                        DataView dv = dtSIMPurchase.DefaultView;
                        dv.RowFilter = "ProductCode  = " + Convert.ToInt32(ddlProduct.SelectedValue);
                        dtSIMPurchase = dv.ToTable();
                        string Amount = dtSIMPurchase.Rows[0]["Amount"].ToString();
                        ViewState["CurrencySymbol"] = dtSIMPurchase.Rows[0]["CurrencySymbol"].ToString();
                        if (Amount != "")
                        {
                            ViewState["AmountPay"] = Amount;
                            txtAmountPay.Text = Amount;

                            DataSet ds = svc.GetSingleDistributorTariffService(distributorid);

                            DataTable dtRecharge = new DataTable();
                            DataView dv1 = new DataView(ds.Tables[4]);
                            dv1.RowFilter = "NetworkID = " + Convert.ToString(ddlNetwork.SelectedValue) + "";
                            dtRecharge = dv1.ToTable();
                            if (dtRecharge.Rows.Count > 0)
                            {
                                double Amt = 0.0;

                                txtRegulatry.Text = "1";
                                
                               // Amt = Convert.ToDouble(((Convert.ToDecimal(txtAmountPay.Text)) - ((Convert.ToDecimal(txtAmountPay.Text) * Convert.ToDecimal(dtRecharge.Rows[0]["RechargePer"])) / 100)).ToString());
                                //-----Discount via Procedure level
                                Amt = Convert.ToDouble(Convert.ToDecimal(txtAmountPay.Text));
                                
                                
                                Amt = Math.Round(Amt, 2);
                                txtAmt.Text = Convert.ToString(Amt);
                                // 1$ add regulatery for Altra Mobile
                                Amt = Amt + 1;
                                txtAmountPay.Text = Convert.ToString(Amt);

                                double CashBack = 0.0;
                                CashBack = Convert.ToDouble(((Convert.ToDecimal(txtAmountPay.Text) * Convert.ToDecimal(dtRecharge.Rows[0]["RechargePer"])) / 100).ToString());
                                lblCashback.Text = "After Rechage Success then You have Received Cachback  $ " + Convert.ToString(CashBack);


                                lblRechargePercentage.Text = " (Discounted Amount with " + Convert.ToDecimal(dtRecharge.Rows[0]["RechargePer"]).ToString() + "% extra)";
                            }
                            else
                            {
                                double Amt = 0.0;
                                txtRegulatry.Text = "1";
                          
                               // Amt = Convert.ToDouble(((Convert.ToDecimal(txtAmountPay.Text)) - ((Convert.ToDecimal(txtAmountPay.Text) * 8) / 100)).ToString());
                                //-----Discount via Procedure level
                                Amt = Convert.ToDouble(Convert.ToDecimal(txtAmountPay.Text));
                                
                                Amt = Math.Round(Amt, 2);
                                txtAmt.Text = Convert.ToString(Amt);
                                Amt = Amt + 1;
                                txtAmountPay.Text = Convert.ToString(Amt);
                                lblCashback.Text = "";
                                lblRechargePercentage.Text = " (Discounted Amount with 0%)";
                            }
                        }
                        else
                        {
                            ViewState["AmountPay"] = "0";
                            txtAmountPay.Text = "0";
                        }

                    }



                    else if (ddlNetwork.SelectedItem.Text == "Lyca Mobile")
                    {

                        dtSIMPurchase = (DataTable)ViewState["Product"];

                        DataView dv = dtSIMPurchase.DefaultView;
                        dv.RowFilter = "ProductCode  = " + Convert.ToInt32(ddlProduct.SelectedValue);
                        dtSIMPurchase = dv.ToTable();
                        string Amount = dtSIMPurchase.Rows[0]["Amount"].ToString();
                        ViewState["CurrencySymbol"] = dtSIMPurchase.Rows[0]["CurrencySymbol"].ToString();
                        if (Amount != "")
                        {
                            ViewState["AmountPay"] = Amount;
                            txtAmountPay.Text = Amount;

                            DataSet ds = svc.GetSingleDistributorTariffService(distributorid);


                            DataTable dtRecharge = new DataTable();


                            DataView dv1 = new DataView(ds.Tables[4]);
                            dv1.RowFilter = "NetworkID = " + Convert.ToString(ddlNetwork.SelectedValue) + "";
                            dtRecharge = dv1.ToTable();
                            if (dtRecharge.Rows.Count > 0)
                            {
                                // 1 $ Add Regulatery 
                                    DateTime today = DateTime.Today;
                                   // DateTime date = new DateTime(2017, 07, 20);
                                    DataSet dsReg = svc.GetRegulatery();
                                    DateTime date = Convert.ToDateTime(dsReg.Tables[0].Rows[0]["StartDate"]);
                                    if (date <= today)
                                    {

                                        double Amt = 0.0;
                                        txtRegulatry.Text = "1";
                                        //Amt = Convert.ToDouble(((Convert.ToDecimal(txtAmountPay.Text)) - ((Convert.ToDecimal(txtAmountPay.Text) * Convert.ToDecimal(dtRecharge.Rows[0]["RechargePer"])) / 100)).ToString());

                                        //-----Discount via Procedure level
                                        Amt = Convert.ToDouble(Convert.ToDecimal(txtAmountPay.Text));
                                
                                        
                                        Amt = Math.Round(Amt, 2);
                                        txtAmt.Text = Convert.ToString(Amt);
                                        Amt = Amt + 1;
                                        txtAmountPay.Text = Convert.ToString(Amt);

                                        double CashBack = 0.0;
                                        CashBack = Convert.ToDouble(((Convert.ToDecimal(txtAmountPay.Text) * Convert.ToDecimal(dtRecharge.Rows[0]["RechargePer"])) / 100).ToString());
                                        lblCashback.Text = "After Rechage Success then You have Received Cachback  $ " + Convert.ToString(CashBack);


                                        lblRechargePercentage.Text = " (Discounted Amount with " + Convert.ToDecimal(dtRecharge.Rows[0]["RechargePer"]).ToString() + "%)";
                                    }

                                    else {

                                        double Amt = 0.0;
                                        txtRegulatry.Text = "0";
                                       // Amt = Convert.ToDouble(((Convert.ToDecimal(txtAmountPay.Text)) - ((Convert.ToDecimal(txtAmountPay.Text) * Convert.ToDecimal(dtRecharge.Rows[0]["RechargePer"])) / 100)).ToString());
                                        //-----Discount via Procedure level
                                        Amt = Convert.ToDouble(Convert.ToDecimal(txtAmountPay.Text));
                                
                                        
                                        Amt = Math.Round(Amt, 2);
                                        txtAmt.Text = Convert.ToString(Amt);
                                       
                                        txtAmountPay.Text = Convert.ToString(Amt);

                                        double CashBack = 0.0;
                                        CashBack = Convert.ToDouble(((Convert.ToDecimal(txtAmountPay.Text) * Convert.ToDecimal(dtRecharge.Rows[0]["RechargePer"])) / 100).ToString());
                                        lblCashback.Text = "After Rechage Success then You have Received Cachback  $ " + Convert.ToString(CashBack);

                                        lblRechargePercentage.Text = " (Discounted Amount with " + Convert.ToDecimal(dtRecharge.Rows[0]["RechargePer"]).ToString() + "%)";
                                  
                                    
                                    
                                    }
                            }
                            else
                            {
                                // 1 $ Add Regulatery 
                                DateTime today = DateTime.Today;
                                // DateTime date = new DateTime(2017, 07, 20);
                                DataSet dsReg = svc.GetRegulatery();
                                DateTime date = Convert.ToDateTime(dsReg.Tables[0].Rows[0]["StartDate"]);
                                if (date <= today)
                                {
                                    double Amt = 0.0;
                                    txtRegulatry.Text = "1";
                                   // Amt = Convert.ToDouble(((Convert.ToDecimal(txtAmountPay.Text)) - ((Convert.ToDecimal(txtAmountPay.Text) * 8) / 100)).ToString());
                                    //-----Discount via Procedure level
                                    Amt = Convert.ToDouble(Convert.ToDecimal(txtAmountPay.Text));
                                
                                    Amt = Math.Round(Amt, 2);
                                    txtAmt.Text = Convert.ToString(Amt);
                                    Amt = Amt + 1;
                                    txtAmountPay.Text = Convert.ToString(Amt);
                                    
                                    lblCashback.Text = "" ;

                                    lblRechargePercentage.Text = " (Discounted Amount with 0%)";

                                }
                                else
                                {
                                    double Amt = 0.0;
                                    txtRegulatry.Text = "0";
                                   // Amt = Convert.ToDouble(((Convert.ToDecimal(txtAmountPay.Text)) - ((Convert.ToDecimal(txtAmountPay.Text) * 8) / 100)).ToString());
                                    //-----Discount via Procedure level
                                    Amt = Convert.ToDouble(Convert.ToDecimal(txtAmountPay.Text));
                                
                                    
                                    Amt = Math.Round(Amt, 2);
                                    txtAmt.Text = Convert.ToString(Amt);
                                    txtAmountPay.Text = Convert.ToString(Amt);
                                    lblCashback.Text = "";
                                    lblRechargePercentage.Text = " (Discounted Amount with 0%)";
                                }
                                
                            }
                        }
                        else
                        {
                            ViewState["AmountPay"] = "0";
                            txtAmountPay.Text = "0";
                        }
                    }
                    else if (ddlNetwork.SelectedItem.Text == "AT&T")
                    {
                        dtSIMPurchase = (DataTable)ViewState["Product"];

                        if (Convert.ToString(ddlProduct.SelectedValue) == "8816130")
                        {
                            
                            txtAmt.Attributes.Add("readonly", "true");
                        }
                        else {

                            txtAmt.Attributes.Remove("readonly");
                           
                        }



                        DataView dv = dtSIMPurchase.DefaultView;
                        dv.RowFilter = "ProductCode  = " + Convert.ToInt32(ddlProduct.SelectedValue);
                        dtSIMPurchase = dv.ToTable();
                        string Amount = dtSIMPurchase.Rows[0]["Amount"].ToString();
                        ViewState["CurrencySymbol"] = dtSIMPurchase.Rows[0]["CurrencySymbol"].ToString();
                        if (Amount != "")
                        {
                            ViewState["AmountPay"] = Amount;
                            txtAmountPay.Text = Amount;

                            DataSet ds = svc.GetSingleDistributorTariffService(distributorid);


                            DataTable dtRecharge = new DataTable();


                            DataView dv1 = new DataView(ds.Tables[4]);
                            dv1.RowFilter = "NetworkID = " + Convert.ToString(ddlNetwork.SelectedValue) + "";
                            dtRecharge = dv1.ToTable();
                            if (dtRecharge.Rows.Count > 0)
                            {
                                double Amt = 0.0;


                                //Amt = Convert.ToDouble(((Convert.ToDecimal(txtAmountPay.Text)) - ((Convert.ToDecimal(txtAmountPay.Text) * Convert.ToDecimal(dtRecharge.Rows[0]["RechargePer"])) / 100)).ToString());
                                //-----Discount via Procedure level
                                Amt = Convert.ToDouble(Convert.ToDecimal(txtAmountPay.Text));


                                Amt = Math.Round(Amt, 2);
                                txtAmountPay.Text = Convert.ToString(Amt);
                                txtAmt.Text = Convert.ToString(Amt);
                                double CashBack = 0.0;
                                CashBack = Convert.ToDouble(((Convert.ToDecimal(txtAmountPay.Text) * Convert.ToDecimal(dtRecharge.Rows[0]["RechargePer"])) / 100).ToString());
                                lblCashback.Text = "After Rechage Success then You have Received Cachback  $ " + Convert.ToString(CashBack);

                                lblRechargePercentage.Text = " (Discounted Amount with " + Convert.ToDecimal(dtRecharge.Rows[0]["RechargePer"]).ToString() + "% extra)";
                            }
                            else
                            {
                                double Amt = 0.0;
                                // Amt = Convert.ToDouble(((Convert.ToDecimal(txtAmountPay.Text)) - ((Convert.ToDecimal(txtAmountPay.Text) * 8) / 100)).ToString());
                                //-----Discount via Procedure level
                                Amt = Convert.ToDouble(Convert.ToDecimal(txtAmountPay.Text));

                                Amt = Math.Round(Amt, 2);
                                txtAmountPay.Text = Convert.ToString(Amt);
                                txtAmt.Text = Convert.ToString(Amt);
                                lblCashback.Text = "";
                                lblRechargePercentage.Text = " (Discounted Amount with 0%)";
                            }
                        }
                        else
                        {
                            ViewState["AmountPay"] = "0";
                            txtAmountPay.Text = "0";
                        }

                    }

                    int clnt = Convert.ToInt32(Session["ClientTypeID"]);
                    if (clnt == 1)
                    {
                        lblRechargePercentage.Visible = false;
                        lblCashback.Visible = false;
                    }

                }

                else
                {
                    ViewState["AmountPay"] = "0";
                    txtAmountPay.Text = "0";
                }
                


            }
            catch (Exception ex)
            {
                
                throw ex;
            }

            
            
           

        }
        private void BindlYCAProduct()
        {
            try
            {
                ENK.net.emida.ws.webServicesService ws = new webServicesService();
                string ss2;
                ss2 = ws.GetProductList("01", "3756263", "", "", "", "");


                StringReader theReader = new StringReader(Convert.ToString(ss2));
                DataSet theDataSet = new DataSet();
                theDataSet.ReadXml(theReader);
                DataTable dt = new DataTable();
                dt = theDataSet.Tables[1];


                DataView dv = dt.DefaultView;
                dv.RowFilter = "Description  like 'Lyca%'";
                dt = dv.ToTable();
                ddlProduct.DataSource = dt;
                ddlProduct.DataValueField = "ProductId";
                ddlProduct.DataTextField = "Description";
                ddlProduct.DataBind();
                ddlProduct.Items.Insert(0, new ListItem("---Select---", "0"));
                ViewState["Product"] = dt;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private void BindH2OProduct()
        {
            try
            {
                ENK.net.emida.ws.webServicesService ws = new webServicesService();
                string ss2;
                ss2 = ws.GetProductList("01", "3756263", "", "", "", "");


                StringReader theReader = new StringReader(Convert.ToString(ss2));
                DataSet theDataSet = new DataSet();
                theDataSet.ReadXml(theReader);

                if (theDataSet.Tables.Count > 1)
                {
                    DataTable dt = theDataSet.Tables[1];
                    if (dt.Rows.Count > 0)
                    {

                        dt = theDataSet.Tables[1];


                        DataView dv = dt.DefaultView;
                        dv.RowFilter = "Description  like 'H2O%'";
                        dt = dv.ToTable();
                        ddlProduct.DataSource = dt;
                        ddlProduct.DataValueField = "ProductId";
                        ddlProduct.DataTextField = "Description";
                        ddlProduct.DataBind();
                        ddlProduct.Items.Insert(0, new ListItem("---Select---", "0"));
                        ViewState["Product"] = dt;
                    }
                    else
                    {
                        ddlProduct.Items.Clear();
                        ddlProduct.Items.Insert(0, new ListItem("---Select---", "0"));

                    }

                }
                else
                {
                    ddlProduct.Items.Clear();
                    ddlProduct.Items.Insert(0, new ListItem("---Select---", "0"));

                }
            }

            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void BindProduct()
        {
            try
            {
                DataSet theDataSet = svc.GetProductRecharge(Convert.ToInt32(ddlNetwork.SelectedValue));

                   DataTable dt = theDataSet.Tables[0];
                    if (dt.Rows.Count > 0)
                    {

                       
                        ddlProduct.DataSource = dt;
                        ddlProduct.DataValueField = "ProductCode";
                        ddlProduct.DataTextField = "ProductDescription";
                        ddlProduct.DataBind();
                        ddlProduct.Items.Insert(0, new ListItem("Select Tariff", "0"));
                        ViewState["Product"] = dt;
                    }
                    else
                    {
                        ddlProduct.Items.Clear();
                        ddlProduct.Items.Insert(0, new ListItem("Select Tariff", "0"));

                    }

                
            }

            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void Clear()
        {

            txtAmountPay.Text = "";
            txtChannelID.Text = "";
            txtCity.Text = "";
            txtEmail.Text = "";
            txtSIMCARD.Text = "";
            txtZIPCode.Text = "";
            txtConfirmSIMCARD.Text = "";

            ddlNetwork.SelectedIndex = 0;
            ddlProduct.SelectedIndex = 0;
        }
        //ankit singh
        // public Boolean CheckAccount(double TariffAmount)
        public Boolean CheckAccount(decimal TariffAmount)
        {
            Boolean IsBalance = false;
            try
            {
                int dist = Convert.ToInt32(Session["DistributorID"]);
                Distributor[] dstbutor = svc.GetSingleDistributorService(dist);
                if (dstbutor != null)
                {
                    if (dstbutor.Length > 0)
                    {
                        decimal BalanceAmnt = dstbutor[0].balanceAmount;
                        if (TariffAmount > BalanceAmnt)
                        {
                            IsBalance = false;
                        }
                        else
                        {
                            IsBalance = true;
                        }
                    }
                }
                return IsBalance;
            }
            catch (Exception ex)
            {
                return IsBalance;
            }
        }
        public void Log2(string ss, string condition)
        {
            try
            {
                string filename = "SubscriberRechargeLog.txt";
                string strPath = Server.MapPath("Log") + "/" + filename;
                string root = "https://" + HttpContext.Current.Request.Url.Host + HttpContext.Current.Request.ApplicationPath + "/Log/" + filename;

                if (File.Exists(strPath))
                {
                    StreamWriter sw = new StreamWriter(strPath, true, Encoding.Unicode);
                    if (condition != "split")
                    {
                        sw.WriteLine(condition + "  " + DateTime.Now.ToString());
                        sw.WriteLine(ss);
                        sw.Close();
                    }
                    else
                    {

                        sw.WriteLine("-----------------------------------------------------------------------------------------------");
                        sw.Close();
                    }
                }
            }
            catch (Exception ex)
            {

            }

        }


        protected void btnCompanyByAccount_Click(object sender, EventArgs e)
        {
            ENK.net.emida.ws.webServicesService ws = new webServicesService();
            try
            {
                string ss1 = ws.Login2("01", "A&HPrepaid", "95222", "1");
                
            }
            catch (Exception ex)
            {
                ShowPopUpMsg(ex.Message);
                return;

            }

            int clnt = Convert.ToInt32(Session["ClientTypeID"]);

            if (clnt != 1)
            {
                Boolean IsBalance = CheckAccount(Convert.ToDecimal(txtAmountPay.Text.Trim()));

                if (IsBalance == false)
                {
                    ShowPopUpMsg("Your Account Balance is Low \n Please Recharge Your Balance");
                    return;
                }
            }

            string ss = "";
           
            DataSet dsDuplicate = new DataSet();

            if (Session["LoginID"] != null)
            {
                if (ddlNetwork.SelectedItem.Text == "Lyca Mobile")
                {

                    string InvoiceNo = DateTime.Now.ToString().GetHashCode().ToString("X");
                    InvoiceNo = "RC" + InvoiceNo;

                    string Number = "";
                    if (hddnInvoice.Value == "" || hddnInvoice.Value == "0")
                    {
                        Number = "0";
                    }
                    else
                    {
                        Number = hddnInvoice.Value;
                    }

                    dsDuplicate = svc.CheckRechargeDuplicate(Convert.ToInt32(ddlNetwork.SelectedValue), Convert.ToString(txtSIMCARD.Text), Convert.ToInt32(ddlProduct.SelectedValue), Number);

                    string Request = "01, 3756263, 1234, " + ddlProduct.SelectedValue + "," + txtSIMCARD.Text + "," + Convert.ToString(ViewState["AmountPay"]) + "," + InvoiceNo + ", 1";
                  
                    //Session["InvoiceNo"] = InvoiceNo;
                    hddnInvoice.Value = InvoiceNo;
                    if (dsDuplicate != null)
                    {

                        if (Convert.ToInt32(dsDuplicate.Tables[0].Rows[0]["IsValid"]) == 0)
                        {

                            ss = "Mobile- " + txtSIMCARD.Text + "|" + "AmountPay- " + txtAmountPay.Text + "|" + "Network- " + ddlNetwork.SelectedItem.Text + "|" + "TariffCode- " + ddlProduct.SelectedValue + "|" + "RechargeAmount- " + txtAmountPay.Text + "|" + "State- " + "" + "|" + "ZIPCode- " + "";
                            Log2(ddlNetwork.SelectedItem.Text, "Recharge Network");
                            Log2(ss, "Recharge Request");
                            Log2("", "split");



                            Log2(dsDuplicate.Tables[0].Rows[0]["Msg"].ToString(), "Show Check Message");
                            Log2(dsDuplicate.Tables[0].Rows[0]["IsValid"].ToString(), "IsValid");
                            Log2(txtSIMCARD.Text.ToString(), "SimNumber");
                            //Log2(Convert.ToString(ViewState["AmountPay"]), "Rechargeamount");
                            Log2(txtAmountPay.Text, "AmountPay");

                            //string ss2 = ws.PinDistSale("01", "3756263", "1234", ddlProduct.SelectedValue, txtSIMCARD.Text, Convert.ToString(ViewState["AmountPay"]), InvoiceNo, "1");


                            //1 jully Add Regulatry----
                            string ss2 = "";
                            string Regulatery = "0";
                            string RechrgeAmont = "0";
                     
                            DateTime today = DateTime.Today;

                            // DateTime date = new DateTime(2017, 07, 20);
                            DataSet dsReg = svc.GetRegulatery();
                            DateTime date = Convert.ToDateTime(dsReg.Tables[0].Rows[0]["StartDate"]);
                            

                            if (date <= today)
                            {
                                Regulatery = "1";
                               
                                double amt = 0.0;
                                amt = Convert.ToDouble(Convert.ToString(ViewState["AmountPay"])) + 1;
                               // amt = Convert.ToDouble(Convert.ToString(ViewState["AmountPay"]));
                                RechrgeAmont = Convert.ToString(amt);
                                ss2 = ws.PinDistSale("01", "3756263", "1234", ddlProduct.SelectedValue, txtSIMCARD.Text, Convert.ToString(amt), InvoiceNo, "1");

                                Log2(Convert.ToString(amt), "Rechargeamount");
                                Log2("", "split");

                            }
                            else
                            {
                                Log2(Convert.ToString(ViewState["AmountPay"]), "Rechargeamount");
                                Log2("", "split");
                                Regulatery = "0";
                                RechrgeAmont = Convert.ToString(ViewState["AmountPay"]);
                                ss2 = ws.PinDistSale("01", "3756263", "1234", ddlProduct.SelectedValue, txtSIMCARD.Text, Convert.ToString(ViewState["AmountPay"]), InvoiceNo, "1");

                            }

                          
                            StringReader Reader = new StringReader(ss2);
                            DataSet ds = new DataSet();

                            ds.ReadXml(Reader);
                            if (ds.Tables.Count > 0)
                            {
                                DataTable dtMsg = ds.Tables[0];
                                if (dtMsg.Rows.Count > 0)
                                {
                                    string ResponseCode = dtMsg.Rows[0]["ResponseCode"].ToString();
                                    string ResponseMessage = dtMsg.Rows[0]["ResponseMessage"].ToString();
                                    string PinNumber = "";


                                    int DistributorID = Convert.ToInt32(Session["DistributorID"]);
                                    DataSet dsrecharge = svc.GetSingleDistributorTariffService(DistributorID);


                                    DataTable dtRecharge = new DataTable();

                                   

                                    if (ResponseCode == "00")
                                    {
                                        PinNumber = dtMsg.Rows[0]["Pin"].ToString();
                                        string Currency = "";
                                        string TransactionID = Convert.ToString(dtMsg.Rows[0]["TransactionID"]);
                                        DataSet dsTransaction = svc.SaveTransactionDetails(Convert.ToInt32(ddlNetwork.SelectedValue), Convert.ToInt32(ddlProduct.SelectedValue), "11", Convert.ToString(PinNumber), txtSIMCARD.Text, InvoiceNo, txtAmountPay.Text, Currency, txtCity.Text, "", "305", Convert.ToInt32(Session["LoginID"]), txtAmountPay.Text);
                                        // int s = svc.UpdateAccountBalanceService(dist, loginID, sim, zip, Language, ChannelID, sp);
                                        if (dsTransaction != null)
                                        {

                                            //string TransactionID = Convert.ToString( dsTransaction.Tables[0].Rows[0]["TransactionID"]);
                                            //int DistributorID = Convert.ToInt32(Session["DistributorID"]);
                                            int loginID = Convert.ToInt32(Session["LoginID"]);
                                            string RechargeStatus = "27";
                                            string @RechargeVia = "29";
                                            int TransactionStatusID = 27;
                                            // int s1 = svc.UpdateAccountBalanceAfterRecharge(Convert.ToInt32(ddlNetwork.SelectedValue), Convert.ToInt32(ddlProduct.SelectedValue), Convert.ToString(txtSIMCARD.Text), Convert.ToDecimal(txtAmountPay.Text), DistributorID, Convert.ToString(txtZIPCode.Text), RechargeStatus, RechargeVia, Request, ss2, loginID, 9, "Cash", TransactionID, 1, "Recharge  successfully", TransactionStatusID);


                                            int s1 = svc.UpdateAccountBalanceAfterRecharge(Convert.ToInt32(ddlNetwork.SelectedValue), Convert.ToInt32(ddlProduct.SelectedValue), Convert.ToString(txtSIMCARD.Text), Convert.ToDecimal(txtAmountPay.Text), DistributorID, Convert.ToString(""), RechargeStatus, RechargeVia, Request, ss2, loginID, 9, "Cash", TransactionID, 1, "Recharge  successfully", TransactionStatusID, PinNumber, "", "", "0", RechrgeAmont, InvoiceNo, "Portal", Regulatery);
                                            Log2("Recharge Transaction Success", "Reason");

                                            Log2(Request, "Request");
                                            Log2(ResponseCode, "Response");
                                            Log2(ResponseMessage, "ResponseMessage");
                                            Log2(txtSIMCARD.Text.ToString(), "SimNumber");
                                            Log2(txtAmountPay.Text, "AmountPay");
                                            Log2("", "State-City");
                                            Log2("", "Zip");
                                            Log2(txtAmountPay.Text, "RechargeAmount");
                                            Log2(ss, "Detail");
                                            Log2("", "split");


                                            SendMail("", (txtSIMCARD.Text.Trim() + "  Recharge successful!"), PinNumber, txtSIMCARD.Text.Trim(), "");
                                            ShowPopUpMsg("Mobile Number  " + txtSIMCARD.Text.Trim() + " Recharge  successfully \n Pin Number " + PinNumber);

                                            ws.Logout("01", "A&HPrepaid", "95222", "1");
                                            Clear();
                                        }
                                    }
                                    else
                                    {
                                        Log2("Recharge Transaction Fail", "Reason");

                                        Log2(Request, "Request");
                                        Log2(ResponseCode, "Response");
                                        Log2(ResponseMessage, "ResponseMessage");
                                        Log2(txtSIMCARD.Text.ToString(), "SimNumber");
                                        Log2(txtAmountPay.Text, "AmountPay");
                                        Log2("", "State-City");
                                        Log2("", "Zip");
                                        Log2(txtAmountPay.Text, "RechargeAmount");
                                        Log2(ss, "Detail");
                                        Log2("", "split");


                                        string TransactionID = Convert.ToString(0);
                                        //int DistributorID = Convert.ToInt32(Session["DistributorID"]);
                                        int loginID = Convert.ToInt32(Session["LoginID"]);
                                        string RechargeStatus = "28";
                                        string RechargeVia = "29";
                                        int TransactionStatusID = 28;
                                        int s1 = svc.UpdateAccountBalanceAfterRecharge(Convert.ToInt32(ddlNetwork.SelectedValue), Convert.ToInt32(ddlProduct.SelectedValue), Convert.ToString(txtSIMCARD.Text), Convert.ToDecimal(txtAmountPay.Text), DistributorID, Convert.ToString(txtZIPCode.Text), RechargeStatus, RechargeVia, Request, ss2, loginID, 9, "Cash", TransactionID, 1, "Recharge  Fail", TransactionStatusID, PinNumber, "", "", "0", txtAmt.Text, InvoiceNo, "Portal", Regulatery);
                                        SendFailureMailRecharge(txtEmail.Text, (txtSIMCARD.Text + "  Recharge Failed!"), txtSIMCARD.Text, Convert.ToInt32(Convert.ToInt32(ddlNetwork.SelectedValue)), ddlProduct.SelectedValue, RechrgeAmont, "", "");

                                        ShowPopUpMsg(ResponseMessage);
                                        return;
                                    }


                                }


                            }

                        }
                        else
                        {



                            try
                            {
                                string RechargeDate = Convert.ToString(dsDuplicate.Tables[0].Rows[0]["RechargeDate"]).ToString();
                                string Amount = Convert.ToString(dsDuplicate.Tables[0].Rows[0]["Amount"]).ToString();

                                lblmessage.Text = "You already Recharged " + txtSIMCARD.Text.ToString() + "  with Amount " + Amount + " at " + RechargeDate + " Successfully. If you want to recharge it again  please wait 5 min.";
                           
                                lblmessage.Visible = true;
                                //lblNote.Visible = true;
                                btnCompanyByAccount.Visible = false;
                            }
                            catch { }

                           

                            Log2("Recharge Transaction Duplicate Recharge", "Duplicate Recharge");
                            Log2(dsDuplicate.Tables[0].Rows[0]["Msg"].ToString(), "Show Check Duplicate Message");
                            Log2(dsDuplicate.Tables[0].Rows[0]["IsValid"].ToString(), "IsValid");
                            Log2(txtSIMCARD.Text.ToString(), "SimNumber");
                            Log2(txtAmountPay.Text, "AmountPay");
                           
                            Log2("", "split");

                           
                           


                        }
                    }
                  

                }
                else if (ddlNetwork.SelectedItem.Text == "H20" || ddlNetwork.SelectedItem.Text == "EasyGo" || ddlNetwork.SelectedItem.Text == "Ultra Mobile" || ddlNetwork.SelectedItem.Text == "AT&T")
                {

                    string InvoiceNo = DateTime.Now.ToString().GetHashCode().ToString("X");
                    InvoiceNo = "RC" + InvoiceNo;


                    string Number = "";
                    if (hddnInvoice.Value == "" || hddnInvoice.Value == "0")
                    {
                        Number = "0";
                    }
                    else
                    {
                        Number = hddnInvoice.Value;
                    }

                    dsDuplicate = svc.CheckRechargeDuplicate(Convert.ToInt32(ddlNetwork.SelectedValue), Convert.ToString(txtSIMCARD.Text), Convert.ToInt32(ddlProduct.SelectedValue), Number);

                    string Request = "01, 3756263, 1234, " + ddlProduct.SelectedValue + "," + txtSIMCARD.Text + "," + Convert.ToString(ViewState["AmountPay"]) + "," + InvoiceNo + ", 1";

                    //Session["InvoiceNo"] = InvoiceNo;
                    hddnInvoice.Value = InvoiceNo;
                    if (dsDuplicate != null)
                    {
                        if (Convert.ToInt32(dsDuplicate.Tables[0].Rows[0]["IsValid"]) == 0)
                        {


                            ss = "Mobile- " + txtSIMCARD.Text + "|" + "AmountPay- " + txtAmountPay.Text + "|" + "Network- " + ddlNetwork.SelectedItem.Text + "|" + "TariffCode- " + ddlProduct.SelectedValue + "|" + "RechargeAmount- " + txtAmountPay.Text + "|" + "State- " + "" + "|" + "ZIPCode- " + "";
                            Log2(ddlNetwork.SelectedItem.Text, "Recharge Network");
                            Log2(ss, "Recharge Request");
                            Log2("", "split");


                            Log2(dsDuplicate.Tables[0].Rows[0]["Msg"].ToString(), "Show Check Message");
                            Log2(dsDuplicate.Tables[0].Rows[0]["IsValid"].ToString(), "IsValid");
                            Log2(txtSIMCARD.Text.ToString(), "SimNumber");
                            Log2(txtAmountPay.Text, "AmountPay");
                          


                            string ss2 = "";
                            string Regulatery = "0";
                            string RechrgeAmont = "0";
                            // 1$ add regulatery for Altra Mobile
                            if (ddlNetwork.SelectedItem.Text == "Ultra Mobile")
                            {
                               

                                Regulatery = "1";
                                double amt = 0.0;
                                amt = Convert.ToDouble(Convert.ToString(ViewState["AmountPay"])) + 1;
                                RechrgeAmont = Convert.ToString(amt);
                                ss2 = ws.PinDistSale("01", "3756263", "1234", ddlProduct.SelectedValue, txtSIMCARD.Text, Convert.ToString(amt), InvoiceNo, "1");
                               
                                Log2(Convert.ToString(amt), "Rechargeamount");
                                Log2("", "split");

                            }
                            else if (ddlNetwork.SelectedItem.Text == "AT&T")
                            {
                                Log2(Convert.ToString(txtAmt.Text), "Rechargeamount");
                                Log2("", "split");
                                Regulatery = "0";
                                RechrgeAmont = Convert.ToString(txtAmt.Text);

                                ss2 = ws.PinDistSale("01", "3756263", "1234", ddlProduct.SelectedValue, txtSIMCARD.Text, Convert.ToString(txtAmt.Text), InvoiceNo, "1");

                            }
                            else
                            {
                                Log2(Convert.ToString(ViewState["AmountPay"]), "Rechargeamount");
                                Log2("", "split");
                                Regulatery = "0";
                                RechrgeAmont = Convert.ToString(ViewState["AmountPay"]);

                                ss2 = ws.PinDistSale("01", "3756263", "1234", ddlProduct.SelectedValue, txtSIMCARD.Text, Convert.ToString(ViewState["AmountPay"]), InvoiceNo, "1");

                            }


                         

                            StringReader Reader = new StringReader(ss2);
                            DataSet ds = new DataSet();
                            ds.ReadXml(Reader);

                            if (ds.Tables.Count > 0)
                            {
                                DataTable dtMsg = ds.Tables[0];
                                if (dtMsg.Rows.Count > 0)
                                {
                                    string ResponseCode = dtMsg.Rows[0]["ResponseCode"].ToString();

                                    string ResponseMessage = dtMsg.Rows[0]["ResponseMessage"].ToString();
                                    string PinNumber = "";

                                    int DistributorID = Convert.ToInt32(Session["DistributorID"]);
                                    DataSet dsrecharge = svc.GetSingleDistributorTariffService(DistributorID);


                                    DataTable dtRecharge = new DataTable();
                                     

                                    if (ResponseCode == "00")
                                    {
                                        PinNumber = dtMsg.Rows[0]["Pin"].ToString();
                                        string Currency = "";
                                        string TransactionID = Convert.ToString(dtMsg.Rows[0]["TransactionID"]);
                                        DataSet dsTransaction = svc.SaveTransactionDetails(Convert.ToInt32(ddlNetwork.SelectedValue), Convert.ToInt32(ddlProduct.SelectedValue), "11", "", txtSIMCARD.Text, InvoiceNo, txtAmountPay.Text, Currency, txtCity.Text, txtZIPCode.Text, "305", Convert.ToInt32(Session["LoginID"]), txtAmountPay.Text);
                                        // WriteLog(dist, loginID, sim, zip, Language, ChannelID, sp, "Record Save Success when sim activated In Company Case");

                                        if (dsTransaction != null)
                                        {

                                            // string TransactionID = Convert.ToString(dsTransaction.Tables[0].Rows[0]["TransactionID"]);
                                            //int DistributorID = Convert.ToInt32(Session["DistributorID"]);
                                            int loginID = Convert.ToInt32(Session["LoginID"]);
                                            string RechargeStatus = "27";
                                            string @RechargeVia = "29";
                                            int TransactionStatusID = 27;

                                            //  SendMail(txtEmail.Text.Trim(), "Sim Recharge", ALLOCATED_MSISDN, txtSIMCARD.Text.Trim(), "");

                                            int s1 = svc.UpdateAccountBalanceAfterRecharge(Convert.ToInt32(ddlNetwork.SelectedValue), Convert.ToInt32(ddlProduct.SelectedValue), Convert.ToString(txtSIMCARD.Text), Convert.ToDecimal(txtAmountPay.Text), DistributorID, Convert.ToString(txtZIPCode.Text), RechargeStatus, RechargeVia, Request, ss2, loginID, 9, "Cash", TransactionID, 1, "Recharge  successfully", TransactionStatusID, PinNumber, "", "", "0", RechrgeAmont, InvoiceNo, "Portal", Regulatery);



                                            Log2("Recharge Transaction Success", "Reason");

                                            Log2(Request, "Request");
                                            Log2(ResponseCode, "Response");
                                            Log2(ResponseMessage, "ResponseMessage");
                                            Log2(txtSIMCARD.Text.ToString(), "SimNumber");
                                            Log2(txtAmountPay.Text, "AmountPay");
                                            Log2("", "State-City");
                                            Log2("", "Zip");
                                            Log2(txtAmountPay.Text, "RechargeAmount");
                                            Log2(ss, "Detail");
                                            Log2("", "split");

                                        
                                            

                                            SendMail("", (txtSIMCARD.Text.Trim() + "  Recharge successful!"), PinNumber, txtSIMCARD.Text.Trim(), "");
                                      
                                            ShowPopUpMsg("Mobile Number  '" + txtSIMCARD.Text.Trim() + "' Recharge  successfully \n Pin Number " + PinNumber);
                                            ws.Logout("01", "A&HPrepaid", "95222", "1");

                                            Clear();
                                            return;
                                        }
                                    }
                                    else
                                    {

                                        Log2("Recharge Transaction Fail", "Reason");

                                        Log2(Request, "Request");
                                        Log2(ResponseCode, "Response");
                                        Log2(ResponseMessage, "ResponseMessage");
                                        Log2(txtSIMCARD.Text.ToString(), "SimNumber");
                                        Log2(txtAmountPay.Text, "AmountPay");
                                        Log2("", "State-City");
                                        Log2("", "Zip");
                                        Log2(txtAmountPay.Text, "RechargeAmount");
                                        Log2(ss, "Detail");
                                        Log2("", "split");

                                        string TransactionID = Convert.ToString(0);
                                        //int DistributorID = Convert.ToInt32(Session["DistributorID"]);
                                        int loginID = Convert.ToInt32(Session["LoginID"]);
                                        string RechargeStatus = "28";
                                        string @RechargeVia = "29";
                                        int TransactionStatusID = 28;
                                        int s1 = svc.UpdateAccountBalanceAfterRecharge(Convert.ToInt32(ddlNetwork.SelectedValue), Convert.ToInt32(ddlProduct.SelectedValue), Convert.ToString(txtSIMCARD.Text), Convert.ToDecimal(txtAmountPay.Text), DistributorID, Convert.ToString(txtZIPCode.Text), RechargeStatus, RechargeVia, Request, ss2, loginID, 9, "Cash", TransactionID, 1, "Recharge  Fail", TransactionStatusID, PinNumber, "", "", "0", RechrgeAmont, InvoiceNo, "Portal", Regulatery);

                                        ShowPopUpMsg(ResponseMessage);
                                        return;
                                    }


                                }


                            }
                        }
                        else
                        {
                            try
                            {
                                string RechargeDate = Convert.ToString(dsDuplicate.Tables[0].Rows[0]["RechargeDate"]).ToString();
                                string Amount = Convert.ToString(dsDuplicate.Tables[0].Rows[0]["Amount"]).ToString();

                                lblmessage.Text = "You already Recharged " + txtSIMCARD.Text.ToString() + "  with Amount " + Amount + " at " + RechargeDate + " Successfully. If you want to recharge it again  please wait 5 min.";
                                lblmessage.Visible = true;
                                //lblNote.Visible = true;
                                btnCompanyByAccount.Visible = false;
                            }
                            catch { }

                            Log2("Recharge Transaction Duplicate Recharge", "Duplicate Recharge");

                            Log2(dsDuplicate.Tables[0].Rows[0]["Msg"].ToString(), "Show Check Duplicate Message");
                            Log2(dsDuplicate.Tables[0].Rows[0]["IsValid"].ToString(), "IsValid");
                            Log2(txtSIMCARD.Text.ToString(), "SimNumber");
                            Log2(txtAmountPay.Text, "AmountPay");
                            Log2("", "split");
                        }
                    } 

                }
            }

        }

        public void SendMail(string SendTo, string Subject, string PinNumber, string MobileNumber, string pass)
        {
            try
            {
                string LoginUrl = "https://" + HttpContext.Current.Request.Url.Host + HttpContext.Current.Request.ApplicationPath + "/Login.aspx";
                string LogoUrl = "https://" + HttpContext.Current.Request.Url.Host + HttpContext.Current.Request.ApplicationPath + "/img/logo.png";
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient();


                string MailAddress = ConfigurationManager.AppSettings.Get("Fromail");
               string ToMailID = ConfigurationManager.AppSettings.Get("ToMailID");
                string PassWord = ConfigurationManager.AppSettings.Get("Password");


                string ToMail = Convert.ToString(Session["EmailID"]);

                if (ToMail == "")
                {
                    ToMail = ToMailID;
                
                }
                  





                mail.From = new MailAddress(MailAddress);
               // mail.To.Add(SendTo);
                mail.To.Add(ToMail);
                TimeSpan ts = new TimeSpan(7, 0, 0);
                mail.Subject = Subject + " " + DateTime.UtcNow.Subtract(ts).ToString();

                StringBuilder sb = new StringBuilder();
                sb.Append("<html>");
                sb.Append("<body style=”color:grey; font-size:15px;”>");
                sb.Append("<font face=”Helvetica, Arial, sans-serif”>");

                sb.Append("<div style=”position:absolute; height:200px; width:100px; background-color:0d1d36; padding:30px;”>");
                sb.Append("<img src=" + LogoUrl + " />");
                sb.Append("</div>");

           

                sb.Append("<br/>");

                sb.Append("<div style=”background-color: #ece8d4; width:600px; height:200px; padding:30px; margin-top:30px;”>");
                //sb.Append("<p>Please find the new credentials and get started.</p>");
                sb.Append("<div style='border:1px solid black;padding-left: 24px;width: 388px; BORDER-RADIUS: 25px;font-style:italic;'>");
                sb.Append("<p>Mobile Number " + MobileNumber + " Recharge successfully.  <p>");
                sb.Append("<p> Pin Number " + PinNumber + "<p>");
                sb.Append("<br/>");
                sb.Append("</div>");

                if (ddlNetwork.SelectedItem.Text == "Lyca Mobile")
                {

                    sb.Append("<p> Recharge  successfully, If you have any issue  with Recharge please contact Lycamobile directly at +1-845-301-1633 / +1-866-277-0024 <p>");

                }
                else if (ddlNetwork.SelectedItem.Text == "H20")
                 
                {
                    sb.Append("<p> Recharge  successfully, If you have any issue  with Recharge please contact H2O WIRELESS. <p>");

                    sb.Append("<p> Dealer Hotline 1-800-939-1261 <p>");
                    sb.Append("<p> We are open 7 days a week from 9AM EST to 12AM EST Monday - Friday, and 9AM EST to 11PM EST on weekends. <p>");
                    sb.Append("<p> H2O GSM Support 1-800-643-4926 <p>");
                    sb.Append("<p>Email: customercare@h2owirelessnow.com <p>");
                
                
                }
                
                
                sb.Append("<p>");
            


                sb.Append("<br/>");
                sb.Append("<p>Sincerely,");
                sb.Append("<p>" + ConfigurationManager.AppSettings.Get("COMPANY_NAME") + "</p>");
                sb.Append("<p>Thank you</p>");


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
            catch
            {

            }
        }

        public void SendFailureMailRecharge(string SendTo, string Subject, string MobileNumber, int NetworkID, string TariffCode, string RechageAmount, string ProductDescription, string Responce)
        {
            try
            {
                string LoginUrl = "http://" + HttpContext.Current.Request.Url.Host + HttpContext.Current.Request.ApplicationPath + "/Login.aspx";
                string LogoUrl = "http://" + HttpContext.Current.Request.Url.Host + HttpContext.Current.Request.ApplicationPath + "/img/logo.png";
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient();


                string MailAddress = ConfigurationManager.AppSettings.Get("Fromail");
                string ToMailID = ConfigurationManager.AppSettings.Get("ToMailID");
                string PassWord = ConfigurationManager.AppSettings.Get("Password");


                //SendTo = SendTo + "," + "info@ENK.com";
                // SendTo = SendTo + "," + "shadab.a@virtuzo.in";




                mail.From = new MailAddress(MailAddress);
                // mail.To.Add(SendTo);
                mail.To.Add(SendTo);
                TimeSpan ts = new TimeSpan(7, 0, 0);
                mail.Subject = Subject + " " + DateTime.UtcNow.Subtract(ts).ToString();

                StringBuilder sb = new StringBuilder();
                sb.Append("<html>");
                sb.Append("<body style=”color:grey; font-size:15px;font-style:italic;”>");
                sb.Append("<font face=”Helvetica, Arial, sans-serif”>");
                sb.Append("<div style=” border:1px solid black; background-color: #ece8d4; width:600px; height:200px; padding:30px; margin-top:30px;font-style:italic;”>");
                sb.Append("<div style=”position:absolute; height:200px; width:100px; background-color:0d1d36; padding:30px;”>");
                sb.Append("</div>");
                sb.Append("<img src=" + LogoUrl + " />");

                sb.Append("<div style=”background-color: #ece8d4; width:600px; height:200px; padding:30px; margin-top:30px;”>");
                //sb.Append("<p>Please find the new credentials and get started.</p>");
                sb.Append("<div style='border:1px solid black;padding-left: 24px;width: 388px; BORDER-RADIUS: 25px;font-style:italic;'>");

                sb.Append("<p> TariffCode  " + TariffCode + ". <p>");


                sb.Append("<p>Rechage Amount  " + RechageAmount + "$. <p>");

                sb.Append("<p>Mobile Number " + MobileNumber + "  Recharge not successful.  <p>");




                sb.Append("</div>");
                sb.Append("</div>");

                sb.Append("<br/>");
                if (NetworkID == 13)
                {

                    sb.Append("<p> Recharge not successfully, If you have any issue  with Recharge please contact Lycamobile directly at +1-845-301-1633 / +1-866-277-0024. <p>");

                }
                else if (NetworkID == 15)
                {
                    sb.Append("<p> Recharge not successfully , If you have any issue  with Recharge please contact H2O WIRELESS. <p>");
                    sb.Append("<p> Dealer Hotline 1-800-939-1261 <p>");
                    sb.Append("<p> We are open 7 days a week from 9AM EST to 12AM EST Monday - Friday, and 9AM EST to 11PM EST on weekends. <p>");
                    sb.Append("<p> H2O GSM Support 1-800-643-4926 <p>");
                    sb.Append("<p>Email: customercare@h2owirelessnow.com <p>");
                }


                sb.Append("<p>");
                sb.Append("<br/>");


                sb.Append("<br/>");
                sb.Append("<p>Sincerely,");
                sb.Append("<p>" + ConfigurationManager.AppSettings.Get("COMPANY_NAME") + "</p>");
                sb.Append("<p>Thank you</p>");


                sb.Append("<p>----------------------------------------------------------------<br/>");

                sb.Append("Please send us email " + ConfigurationManager.AppSettings.Get("COMPANY_INFOEMAIL") + " or call us  209 297-3200.   Text us:  209 890 8006  with in 24 Hours ");


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

        protected void txtAmt_TextChanged(object sender, EventArgs e)
        {
            

            if (ddlNetwork.SelectedItem.Text == "AT&T")
            {


                int distributorid = Convert.ToInt32(Session["DistributorID"]);
                double Amt = Convert.ToDouble(Convert.ToDecimal(txtAmt.Text));
                double Regulatry = 0;
                txtRegulatry.Text = "0";
                 
                Amt = Math.Round((Amt + Regulatry), 2);

                txtAmountPay.Text = Convert.ToString(Amt);

                if (Convert.ToString(Amt) != "")
                {
                    //ViewState["AmountPay"] = Amount;
                    // txtAmountPay.Text = Amount;

                    DataSet ds = svc.GetSingleDistributorTariffService(distributorid);


                    DataTable dtRecharge = new DataTable();


                    DataView dv1 = new DataView(ds.Tables[4]);
                    dv1.RowFilter = "NetworkID = " + Convert.ToString(ddlNetwork.SelectedValue) + "";
                    dtRecharge = dv1.ToTable();
                    if (dtRecharge.Rows.Count > 0)
                    {


                        
                        double CashBack = 0.0;
                        CashBack = Convert.ToDouble(((Convert.ToDecimal(txtAmountPay.Text) * Convert.ToDecimal(dtRecharge.Rows[0]["RechargePer"])) / 100).ToString());
                        lblCashback.Text = "After Rechage Success then You have Received Cachback  $ " + Convert.ToString(CashBack);

                        lblRechargePercentage.Text = " (Discounted Amount with " + Convert.ToDecimal(dtRecharge.Rows[0]["RechargePer"]).ToString() + "% extra)";
                    }
                    else
                    {

                        // Amt = Convert.ToDouble(((Convert.ToDecimal(txtAmountPay.Text)) - ((Convert.ToDecimal(txtAmountPay.Text) * 8) / 100)).ToString());
                        //-----Discount via Procedure level
                       
                        lblCashback.Text = "";
                        lblRechargePercentage.Text = " (Discounted Amount with 0%)";
                    }
                }
                else
                {
                    ViewState["AmountPay"] = "0";
                    txtAmountPay.Text = "0";
                }

            }
                    int clnt = Convert.ToInt32(Session["ClientTypeID"]);
                    if (clnt == 1)
                    {
                        lblRechargePercentage.Visible = false;
                        lblCashback.Visible = false;
                    }

            
        }

    }
}
    