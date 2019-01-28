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
using System.Configuration;
using System.Net;
using System.IO;
 

namespace ENK
{
    public partial class RechargeSuccess : System.Web.UI.Page
    {
        Service1Client svc = new Service1Client();

        protected void Page_Load(object sender, EventArgs e)
        {
            string strResponse = "";
            string txToken = "";
            try
            {
                if (!Page.IsPostBack)
                {
                    txToken = Request.QueryString.Get("tx");
                    if (txToken != null && txToken != "")
                    {
                        string authToken = ConfigurationManager.AppSettings["PDTToken"];

                        string query = string.Format("cmd=_notify-synch&tx={0}&at={1}", txToken, authToken);
                        string url = ConfigurationManager.AppSettings["PayPalSubmitUrl"];
                        HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);

                        req.Method = "POST";
                        req.ContentType = "application/x-www-form-urlencoded";
                        req.ContentLength = query.Length;

                        StreamWriter stOut = new StreamWriter(req.GetRequestStream(), System.Text.Encoding.ASCII);
                        stOut.Write(query);
                        stOut.Close();

                        StreamReader stIn = new StreamReader(req.GetResponse().GetResponseStream());
                        strResponse = stIn.ReadToEnd();
                        stIn.Close();

                        lblresponse.Text = strResponse;

                        if (strResponse.IndexOf("SUCCESS") != -1)
                        {
                            
                            int a = 0;
                            a = UpdatePayment(strResponse, "Success", txToken,24);
                            if (a > 0)
                            {
                                MakeReceipt(strResponse);
                                lblMessage.Text = "Topup Successfull..........";                                
                            }
                            else
                            {
                                divPaymentDetail.Visible = false;
                                divBtn.Visible = false;
                                divlink.Visible = true;
                                string ss = Request.QueryString.ToString();
                                Log("TransactionFail", "Reason");
                                Log("May be tranactionid alredy exist", "Topup");
                                Log(strResponse + Convert.ToString(Session["DistributorID"]), "Detail");
                                Log(ss, "Detail");
                                Log("", "split");
                                lblMessage.Text = "Oooops, something went wrong with paypal... Transaction failed";                                
                            }
                            
                        }
                        else
                        {
                            UpdatePayment(strResponse, "Fail", txToken, 25);
                            divPaymentDetail.Visible = false;
                            divBtn.Visible = false;
                            divlink.Visible = true;
                            string ss = Request.QueryString.ToString();
                            Log("TransactionFail", "Reason");
                            Log("In indexof block", "Topup");
                            Log(strResponse + Convert.ToString(Session["DistributorID"]), "Detail");
                            Log(ss, "Detail");
                            Log("", "split");
                            lblMessage.Text = "Oooops, something went wrong with paypal... Transaction failed";                           
                        }
                        UpdateDashboardBalanceAmount();
                    }
                    else
                    {                       
                        divPaymentDetail.Visible = false;
                        divBtn.Visible = false;
                        divlink.Visible = true;
                        UpdatePayment(strResponse, "Fail", txToken, 25);
                        string ss = Request.QueryString.ToString();
                        Log("TransactionFail", "Reason");
                        Log("In txToken is null or blank", "Topup");
                        Log(Convert.ToString(Session["DistributorID"]), "Detail");
                        Log(ss, "Detail");
                        Log("", "split");
                        lblMessage.Text = "Oooops, something went wrong with paypal... Transaction failed";                       
                        UpdateDashboardBalanceAmount();
                    }
                }
            }
            catch (Exception ex)
            {
                divPaymentDetail.Visible = false;
                divBtn.Visible = false;
                divlink.Visible = true;
                UpdatePayment(strResponse, "Fail", txToken, 25);
                string ss = Request.QueryString.ToString();
                Log("TransactionFail in catch block", "Topup");
                Log(ex.Message, "Reason");
                Log(ex.Source, "Reason");
                Log(strResponse + Convert.ToString(Session["DistributorID"]), "Detail");
                Log(ss, "Detail");
                Log("", "split");
                lblMessage.Text = "Oooops, something went wrong with paypal... Transaction failed";               
                UpdateDashboardBalanceAmount();
            }
        }

        public void MakeReceipt(string resp)
        {
            //DataSet ds = svc.GetTestDataService();

           // string ss = ds.Tables[0].Rows[0][0].ToString();
            string[] sd = resp.Split('\n');
            List<string> sdlist = new List<string>();
            sdlist = sd.ToList();
            lblTransactionAmount.Text="Transaction Amount="+sd[1].Replace("mc_gross=","");
            lblTransactionDate.Text = "Transaction Date=" + DateTime.Now.ToString();//sd[7];
            lblPayerName.Text = "Payer Name=" + sd[11].Replace("first_name=", "") + " " + sd[24].Replace("last_name=", "");
            lblAddress.Text = "Transactionid=" + sd[22].Replace("txn_id=", "");
        }

        public int UpdatePayment(string resp,string ststus,string transactionid,int TranactionStatusId)
        {
            int a=0;
            try
            {
                if (Session["PaymentId"] != null)
                {
                    if (transactionid == null)
                    {
                        transactionid = "";
                    }
                    if (Session["LoginID"] != null)
                    {
                        if (TranactionStatusId != 25)
                        {
                            string[] sd = resp.Split('\n');
                            List<string> sdlist = new List<string>();

                            SPayment sp = new SPayment();                            
                            sp.PaymentId = Convert.ToInt32(Session["PaymentId"]);
                            sp.ChargedAmount = Convert.ToDecimal(Session["Amount"]);
                            sp.TxnId = transactionid;
                            sp.TxnAmount = sd[27].Replace("payment_fee=", "");
                            sp.TransactionStatus = ststus;
                            sp.TransactionStatusId = TranactionStatusId;
                            sp.ReceiptId = "";
                            sp.PayerId = sd[4].Replace("payer_id=", "");
                            sp.TxnDate = DateTime.Now.ToString();
                            sp.CheckSumm = resp;

                            int dist = Convert.ToInt32(Session["DistributorID"]);
                            int loginID = Convert.ToInt32(Session["LoginID"]);
                            a = svc.UpdatePaypalTopupService(dist, loginID, sp);
                            Session["PaymentId"] = null;
                            Session["Amount"] = null;
                        }
                        else
                        {
                            SPayment sp = new SPayment();
                            sp.PaymentId = Convert.ToInt32(Session["PaymentId"]);
                            sp.ChargedAmount = Convert.ToDecimal(Session["Amount"]);
                            sp.TxnId = transactionid;
                            sp.TxnAmount = "";
                            sp.TransactionStatus = ststus;
                            sp.TransactionStatusId = TranactionStatusId;
                            sp.ReceiptId = "";
                            sp.PayerId = "";
                            sp.TxnDate = DateTime.Now.ToString();
                            sp.CheckSumm = resp;

                            int dist = Convert.ToInt32(Session["DistributorID"]);
                            int loginID = Convert.ToInt32(Session["LoginID"]);
                            a = svc.UpdatePaypalTopupService(dist, loginID, sp);
                            Session["PaymentId"] = null;
                            Session["Amount"] = null;
                        }
                        return a;
                    }
                    else
                    {
                        Log("Session null in database insertion block", "Reason");
                        Log(resp, "");
                        Log("", "split");
                        return a;
                    }
                }
                return a;
            }
            catch (Exception ex)
            {
                Log("database insertion catch block" + ex.Message, "Reason");
                Log(resp, "");               
                Log("", "split");
                return a;
            }
        }

        public void UpdateDashboardBalanceAmount()
        {
            try
            {
                if (Session["ClientType"].ToString() == "Distributor")
                {
                    int userId = Convert.ToInt32(Session["LoginID"]);
                    int Dist = Convert.ToInt32(Session["DistributorID"]);
                    Distributor[] dst = svc.GetSingleDistributorService(Dist);
                    lblBalance.Text = Convert.ToString(Session["CurrencyName"]) + " " + Convert.ToString(dst[0].balanceAmount);
                    //lblBalance.Text = "Account Balance " + Convert.ToString(Session["CurrencyName"]) + " " + Convert.ToString(dst[0].balanceAmount);
                   
                }
            }
            catch (Exception ex)
            {
 
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

        public void Log(string ss, string condition)
        {
            try
            {
                string filename = "Topup.txt";
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
                        sw.WriteLine("------------------------------------------------Gap-----------------------------------------------");
                        
                        sw.Close();
                    }
                }
            }
            catch (Exception ex)
            {

            }

        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("Dashboard.aspx");
        }
    }
}