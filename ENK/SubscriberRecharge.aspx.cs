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
    public partial class SubscriberRecharge : System.Web.UI.Page
    {
        Service1Client svc = new Service1Client();
        string RequestRes = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                try
                {
                   // ENK.net.emida.ws.webServicesService ws = new webServicesService();
                    //string ss1 = ws.Login2("01", "A&HPrepaid", "95222", "1");
                    

                   // lblProduct.Text = "Tariff";
                    ViewState["Tax"] = "0";

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
                        }
                        else
                        {
                            ddlNetwork.Items.Insert(0, new ListItem("Select Network", "0"));
                        }

                    }


                    DataSet ds = svc.GetState();
                    if (ds != null)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            ddlState.DataSource = ds.Tables[0];
                            ddlState.DataValueField = "StateID";
                            ddlState.DataTextField = "StateName";
                            ddlState.DataBind();
                            ddlState.Items.Insert(0, new ListItem("Select State", "0"));
                        }
                        else {

                            ddlState.Items.Insert(0, new ListItem("Select State", "0"));
                        
                        }
                    }



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
                catch (Exception ex)
                {

                }

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
                    ddlProduct.Items.Insert(0, new ListItem("Select", "0"));
                    ViewState["Product"] = dt;
                }
                else
                {
                    ddlProduct.Items.Clear();
                    ddlProduct.Items.Insert(0, new ListItem("Select", "0"));

                }


            }

            catch (Exception ex)
            {

                throw ex;
            }
        }

        protected void ddlNetwork_SelectedIndexChanged(object sender, EventArgs e)
        {

            try
            {
                DataTable dtSIMPurchase = new DataTable();
                txtAmount.Text = Convert.ToString(0);
                txtRerulatry.Text = "0";
                txtStateTax.Text = "0";
                txtplanAmount.Text = "0";
                lblRegulatory.Text = "";

                txtplanAmount.Attributes.Add("readonly", "true");

                if (ddlNetwork.SelectedItem.Text == "H20")
                {

                    // BindH2OProduct();
                    BindProduct();

                    txtAmount.Text = "";

                }
                else if (ddlNetwork.SelectedItem.Text == "EasyGo")
                {    txtAmount.Text = "";
                    //BindEASRGOProduct();
                    BindProduct();
                }

                else if (ddlNetwork.SelectedItem.Text == "AT&T")
                {

                    txtplanAmount.Attributes.Remove("readonly");
                    BindProduct();


                }

                else
                {

                    txtAmount.Text = "";
           
                  //  lblProduct.Text = "Tariff";
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
                 DataTable dtSIMPurchase = new DataTable();
          //  int distributorid = Convert.ToInt32(Session["DistributorID"]);
            if (ddlProduct.SelectedIndex > 0)
            {


                if (ddlNetwork.SelectedItem.Text == "AT&T")
                {
                    dtSIMPurchase = (DataTable)ViewState["Product"];

                    if (Convert.ToString(ddlProduct.SelectedValue) == "8816130")
                    {

                        txtplanAmount.Attributes.Add("readonly", "true");
                    }
                    else
                    {

                        txtplanAmount.Attributes.Remove("readonly");

                    }
                }







                dtSIMPurchase = (DataTable)ViewState["Product"];

                DataView dv = dtSIMPurchase.DefaultView;
                dv.RowFilter = "ProductCode  = " + Convert.ToInt32(ddlProduct.SelectedValue);
                dtSIMPurchase = dv.ToTable();
                string Amount = dtSIMPurchase.Rows[0]["Amount"].ToString();
                ViewState["CurrencySymbol"] = dtSIMPurchase.Rows[0]["CurrencySymbol"].ToString();
                if (Amount != "")
                {
                    ViewState["AmountPay"] = Amount;
                    txtAmount.Text = Amount;
                    txtplanAmount.Text = Amount;
                    txtRerulatry.Text = "0";
                    txtStateTax.Text = "0";
                    lblRegulatory.Text = "";
                   

                    // 1$ add regulatery for Lyca Mobile AND  Altra Mobile

                    DateTime today = DateTime.Today;
                    // DateTime date = new DateTime(2017, 07, 20);
                    DataSet dsReg = svc.GetRegulatery();
                    DateTime date = Convert.ToDateTime(dsReg.Tables[0].Rows[0]["StartDate"]);
                            


                    if (ddlNetwork.SelectedItem.Text == "Lyca Mobile" && date <= today)
                    {
                        double amt = 0.0;
                        amt = Convert.ToDouble(Amount) + 1;
                        txtRerulatry.Text = "1";
                        lblRegulatory.Text = "1$ Regulatory Fee";
                        txtAmount.Text = Convert.ToString(amt);
                    }

                   else  if (ddlNetwork.SelectedItem.Text == "Ultra Mobile")
                    {
                       double  amt =0.0;
                       amt = Convert.ToDouble(Amount) + 1;
                       txtRerulatry.Text = "1";
                       lblRegulatory.Text = "1$ Regulatory Fee";
                       txtAmount.Text = Convert.ToString(amt);
                    }
                     

                }
                else
                {
                    ViewState["AmountPay"] = "0";
                    txtAmount.Text = "0";


                }



 


                //if (ddlNetwork.SelectedItem.Text == "H20")
                //{
                //    dtSIMPurchase = (DataTable)ViewState["Product"];

                //    DataView dv = dtSIMPurchase.DefaultView;
                //    dv.RowFilter = "ProductCode  = " + Convert.ToInt32(ddlProduct.SelectedValue);
                //    dtSIMPurchase = dv.ToTable();
                //    string Amount = dtSIMPurchase.Rows[0]["Amount"].ToString();
                //    ViewState["CurrencySymbol"] = dtSIMPurchase.Rows[0]["CurrencySymbol"].ToString();
                //    if (Amount != "")
                //    {
                //        ViewState["AmountPay"] = Amount;
                //        txtAmount.Text = Amount;


                //    }
                //    else
                //    {
                //        ViewState["AmountPay"] = "0";
                //        txtAmount.Text = "0";
                //    }

                //}
                

            if (ddlState.SelectedItem.Text == "California")
            {
                SurchageTax();
            
            }
            }
            else
            {
                lblSurCharge.Text = "";

                double Amt = 0.0;

                txtAmount.Text = Convert.ToString(Amt);
                ViewState["Tax"] = 0;
                txtRerulatry.Text = "0";
                txtStateTax.Text = "0";
                txtplanAmount.Text = "0";
                lblRegulatory.Text = "";
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

        protected void PayWithPayPal(string itemInfo, string amount)
        {

            try
            {

                //string RechAmount = amount;
                //double amt1 = Convert.ToDouble(amount);
                //amount = Convert.ToString(Convert.ToDecimal(1.03 * amt1));


                 if (ViewState["AmountPay"] != "" && ViewState["AmountPay"] != null)
                {

            string redirecturl = "";
            redirecturl += "https://www.paypal.com/cgi-bin/webscr?cmd=_xclick&business=" + ConfigurationManager.AppSettings["paypalemail"].ToString();
            //redirecturl += "https://www.sandbox.paypal.com/cgi-bin/webscr?cmd=_xclick&business=" + ConfigurationManager.AppSettings["paypalemail"].ToString();
            redirecturl += "&first_name=";
            redirecturl += "&city=";
            redirecturl += "&state=";
            redirecturl += "&item_name=" + itemInfo+ " ( "+ddlNetwork.SelectedItem.Text.ToString()+" )";
            redirecturl += "&amount=" + amount;
            redirecturl += "&night_phone_a=";
            // redirecturl += "&item_name=" + itemInfo;
           // redirecturl += "&Network="+ddlNetwork.SelectedItem.Text.ToString();
            redirecturl += "&address1=";
            redirecturl += "&shipping=";
            redirecturl += "&handling=";
            redirecturl += "&tax=";

            redirecturl += "&quantity=1";
            redirecturl += "&currency=USD";

                
            string RechargeAmount =ViewState["AmountPay"].ToString();
            string Regulatery = "0";


            // 1$ add regulatery for Lyca Mobile AND  Altra Mobile

            DateTime today = DateTime.Today;
            // DateTime date = new DateTime(2017, 07, 20);
            DataSet dsReg = svc.GetRegulatery();
            DateTime date = Convert.ToDateTime(dsReg.Tables[0].Rows[0]["StartDate"]);
                            

            if (ddlNetwork.SelectedItem.Text == "Lyca Mobile" && date <= today)
            {
                double amt = 0.0;
                amt = Convert.ToDouble(RechargeAmount) + 1;
                //amt = Convert.ToDouble(RechargeAmount);
                RechargeAmount = Convert.ToString(amt);
                Regulatery = "1";
            }
                           
            if (ddlNetwork.SelectedItem.Text == "Ultra Mobile")
            {
                double amt = 0.0;
                amt = Convert.ToDouble(RechargeAmount) + 1;
                RechargeAmount = Convert.ToString(amt);
                Regulatery = "1";
            }




            string State=ddlState.SelectedItem.Text.ToString();
            string ZipCode= txtzipcode.Text.ToString();
            string Tax = ViewState["Tax"].ToString();

            //string ss = txtMobileNo.Text.ToString() + "," + txtAmount.Text.ToString() + "," + ddlNetwork.SelectedValue + "," + ddlProduct.SelectedValue + "," + txtEmail.Text.ToString() + "," + RechargeAmount + "," + State + "," + ZipCode + "," + Tax + "," + Regulatery;

            string ss = txtMobileNo.Text.ToString() + "," + txtAmount.Text.ToString() + "," + ddlNetwork.SelectedValue + "," + ddlProduct.SelectedValue + "," + txtEmail.Text.ToString() + "," + RechargeAmount + "," + State + "," + ZipCode + "," + Tax + "," + Regulatery;


            Log2("Subscriber Request for Rechrge", "RechrgePaypalTransaction");
            Log2(ddlNetwork.SelectedItem.Text.ToString(), "Network");
            Log2(txtMobileNo.Text.ToString(), "Mobile");
            Log2(ss, "RequestDetail");
            Log2("", "split");

                
            //redirecturl += "&return=" + ConfigurationManager.AppSettings["RechargeSuccessURL"].ToString() + "?MobileNo=" + txtMobileNo.Text.ToString() + "&Amount=" + txtAmount.Text.ToString() + "&Network=" + ddlNetwork.SelectedValue + "&TariffCode=" + ddlProduct.SelectedValue + "&EmailID=" + txtEmail.Text.ToString();
            redirecturl += "&return=" + ConfigurationManager.AppSettings["RechargeSuccessURL"].ToString() + "?PayPal=" + ss;
            redirecturl += "&cancel_return=" + ConfigurationManager.AppSettings["RechargeSuccessURL"].ToString() + "?PayPal=Cancel";



            int a = svc.SaveRechageRequest(Convert.ToInt32(ddlNetwork.SelectedValue), Convert.ToInt32(ddlProduct.SelectedValue), txtMobileNo.Text, txtzipcode.Text, ss, txtEmail.Text, ddlState.SelectedItem.Text, Convert.ToDecimal(RechargeAmount), Convert.ToDecimal(txtStateTax.Text), Convert.ToDecimal(txtAmount.Text), Convert.ToDecimal(txtRerulatry.Text), 1, 1);

           if (a > 0)
           {

               Response.Redirect(redirecturl);
            
            
           }

            }
           else {
                 
                    ShowPopUpMsg("Session Expire  !!  \n Please select again Tairiff plan");
                     return;
                 }

            }
            catch  
            { 
            }
           

        }
        protected void GetClientIPAddress()
        {
            try
            {
                System.Web.HttpContext context = System.Web.HttpContext.Current;

                string IP1 = HttpContext.Current.Request.UserHostAddress;
                string IP2 = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                //To get the IP address of the machine and not the proxy use the following code 
                string IP3 = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (!string.IsNullOrEmpty(IP3))
                {
                    string[] addresses = IP3.Split(',');
                    if (addresses.Length != 0)
                    {

                    }
                }
                if (IP3 == null)
                {
                    IP3 = "";
                }


                string Browser = "";
                string IsBrowser = "";
                IsBrowser = context.Request.Browser.IsMobileDevice.ToString();
                string Browser1 = Request.Browser.Browser + " " + Request.Browser.Version;
                if (IsBrowser == "True")
                {
                    Browser = "Mobile";
                }
                else
                {
                    Browser = "Web";
                }

                string BrowserDetail = "";
                if (context.Request.ServerVariables["HTTP_X_WAP_PROFILE"] != null)
                {
                    BrowserDetail = BrowserDetail + "|" + context.Request.ServerVariables["HTTP_X_WAP_PROFILE"];
                }

                if (context.Request.ServerVariables["HTTP_ACCEPT"] != null &&
                    context.Request.ServerVariables["HTTP_ACCEPT"].ToLower().Contains("wap"))
                {
                    BrowserDetail = BrowserDetail + "|" + context.Request.ServerVariables["HTTP_ACCEPT"];
                }

                if (context.Request.ServerVariables["HTTP_USER_AGENT"] != null)
                {
                    BrowserDetail = BrowserDetail + "|" + context.Request.ServerVariables["HTTP_USER_AGENT"];
                }

                //string ss = Request.Browser.ToString();
                //string ss2 = Request.Browser.Version.ToString();

                SLoginHistory sl = new SLoginHistory();

                sl.IpAddress1 = IP1;
                sl.IpAddress2 = IP2;
                sl.IpAddress3 = IP3;
                sl.BrowserName = Browser;
                sl.Browser1 = Browser1;
                sl.Location = "Subscriber";
                sl.IpDetail = BrowserDetail;
                sl.LoginID = 1;
                sl.LoginTime = DateTime.Now;
                svc.InsertLoginHistoryService(sl);
            }
            catch (Exception ex)
            {

            }
            // return context.Request.ServerVariables["REMOTE_ADDR"];
        }
        protected void btnSubscriber_Click(object sender, EventArgs e)
        {
            try
            {
                try
                {
                    GetClientIPAddress();
                }
                catch { }
            

                PayWithPayPal(txtMobileNo.Text, txtAmount.Text);


            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void resetControls(int condition)
        {
            if (condition == 1)
            {
                txtMobileNo.Text = string.Empty;

                txtAmount.Text = "";
                txtEmail.Text = "";



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
        protected void btnPaypal_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                PayWithPayPal(txtMobileNo.Text, txtAmount.Text);


            }
            catch 
            {}
        }
       
        public void Log2(string ss, string condition)
        {
            try
            {
                string filename = "NewSubscriberRechargeLog.txt";
                string strPath = Server.MapPath("Log") + "/" + filename;
                string root = "http://" + HttpContext.Current.Request.Url.Host + HttpContext.Current.Request.ApplicationPath + "/Log/" + filename;

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

        protected void ddlSate_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                SurchageTax();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void SurchageTax()
        {

            try
            {
                if (ViewState["AmountPay"] != "" && ViewState["AmountPay"] != null)
                {
                    if (ddlProduct.SelectedIndex > 0)
                    {
                       

                        if (ddlState.SelectedItem.Text == "California")
                        {
                            double Charge = 5.90;
                           // lblSurCharge.Text = " (Exclusive  Surcharge Amount with 5.90%)";
                            lblSurCharge.Text = "(Surcharge Amount with 5.90%)";
                            ViewState["Tax"] = "5.90";
                            string Amount = ViewState["AmountPay"].ToString();
                            double Amt = 0.0;
                            Amt = Convert.ToDouble(Amount);
                            Amt = Convert.ToDouble(((Convert.ToDouble(Amount)) + ((Convert.ToDouble(Amount) * 5.90) / 100)).ToString());
                            Amt = Math.Round(Amt, 2);

                            txtStateTax.Text = "5.90";
                            txtplanAmount.Text = Amount;


                            // 1$ add regulatery for Lyca Mobile AND  Altra Mobile

                            DateTime today = DateTime.Today;
                            // DateTime date = new DateTime(2017, 07, 20);
                            DataSet dsReg = svc.GetRegulatery();
                            DateTime date = Convert.ToDateTime(dsReg.Tables[0].Rows[0]["StartDate"]);
                            

                            
                            if (ddlNetwork.SelectedItem.Text == "Lyca Mobile" && date <= today)
                            {
                                Amt = Amt + 1;
                                txtRerulatry.Text = "1";
                                txtAmount.Text = Convert.ToString(Amt);
                                lblRegulatory.Text = "1$ Regulatory Fee";
                            }
                            
                            
                           else  if (ddlNetwork.SelectedItem.Text == "Ultra Mobile")
                            {
                                Amt = Amt + 1;
                                txtRerulatry.Text = "1";
                                txtAmount.Text = Convert.ToString(Amt);
                                lblRegulatory.Text = "1$ Regulatory Fee";
                            }
                            else
                            {
                                txtRerulatry.Text = "0";
                                txtAmount.Text = Convert.ToString(Amt);
                                lblRegulatory.Text = "";
                            }
                           



                        }
                        else
                        {
                            ViewState["Tax"] = "0";
                            lblSurCharge.Text = "";
                            string Amount = ViewState["AmountPay"].ToString();
                            double Amt = 0.0;
                            Amt = Convert.ToDouble(Amount);

                            txtStateTax.Text = "0";
                            txtplanAmount.Text = Amount;
                           
                          
                            // 1$ add regulatery for Lyca Mobile AND  Altra Mobile

                            DateTime today = DateTime.Today;
                            // DateTime date = new DateTime(2017, 07, 20);
                            DataSet dsReg = svc.GetRegulatery();
                            DateTime date = Convert.ToDateTime(dsReg.Tables[0].Rows[0]["StartDate"]);
                            

                            if (ddlNetwork.SelectedItem.Text == "Lyca Mobile" && date <= today)
                            {
                                Amt = Amt + 1;
                                txtRerulatry.Text = "1";
                                txtAmount.Text = Convert.ToString(Amt);
                                //lblRegulatory.Text = "1$ Regulatory Fee";
                            }



                      else  if (ddlNetwork.SelectedItem.Text == "Ultra Mobile")
                            {
                                Amt = Amt + 1;
                                txtAmount.Text = Convert.ToString(Amt);
                                txtRerulatry.Text = "1";
                            }
                            else
                            {   txtRerulatry.Text = "0";
                                txtAmount.Text = Convert.ToString(Amt);
                                lblRegulatory.Text = "";
                            }
                           


                        }
                    }
                    else {
                        ViewState["Tax"] = "0";
                        lblSurCharge.Text = "";
                       
                        double Amt = 0.0;
                      
                        txtAmount.Text = Convert.ToString(Amt);
                        txtRerulatry.Text = "0";
                        txtStateTax.Text = "0";
                        txtplanAmount.Text = "0";
                    }
                }

            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }

        protected void txtplanAmount_TextChanged(object sender, EventArgs e)
        {
            if (ddlNetwork.SelectedItem.Text == "AT&T")
            {


                if (ddlState.SelectedItem.Text == "California")
                {

                    double Amount = Convert.ToDouble(Convert.ToDecimal(txtplanAmount.Text));
                    double Regulatry = Convert.ToDouble(Convert.ToDecimal(txtRerulatry.Text));
                    //Discount via Procedure level
                    Amount = Math.Round((Amount), 2);

                    double Amt = 0.0;
                    Amt = Convert.ToDouble(Amount);
                    Amt = Convert.ToDouble(((Convert.ToDouble(Amount)) + ((Convert.ToDouble(Amount) * 5.90) / 100)).ToString());
                    Amt = Math.Round(Amt, 2);
                    Amt = Math.Round((Amt + Regulatry), 2);

                    txtAmount.Text = Convert.ToString(Amt);
                }
                else
                {
                    double Amt = Convert.ToDouble(Convert.ToDecimal(txtplanAmount.Text));
                    double Regulatry = Convert.ToDouble(Convert.ToDecimal(txtRerulatry.Text));
                    //Discount via Procedure level
                    Amt = Math.Round((Amt + Regulatry), 2);

                    txtAmount.Text = Convert.ToString(Amt);



                }
            }

         
        }
    }
}