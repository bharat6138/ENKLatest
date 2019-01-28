using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Text;
using System.IO;
using System.Data;
using ENK.ServiceReference1;
using Newtonsoft.Json;
using System.Configuration;
using Newtonsoft.Json.Linq;

namespace ENK
{
    public partial class GlobalRecharge : System.Web.UI.Page
    {
        Service1Client svc = new Service1Client();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Session["LoginId"] != null)
                {
                    GetCountries();
                    Session["Discount"] = null;
                }
            }
        }
        public void GetCountries()
        {
            DataTable dtCountry = new DataTable();
            dtCountry = svc.GetCountriesForDing();
            if (dtCountry.Rows.Count > 0)
            {
                ddlCountry.DataSource = dtCountry;
                ddlCountry.DataValueField = "CountryIso";
                ddlCountry.DataTextField = "CountryName";
                ddlCountry.DataBind();
                ddlCountry.Items.Insert(0, new ListItem("-SELECT-", "0"));
            }
        }
        public void GetOperator()
        {
            DataTable dtOperator = new DataTable();
            dtOperator = svc.OperatorAginstCountry(Convert.ToString(ddlCountry.SelectedValue));
            if (dtOperator.Rows.Count > 0 && dtOperator != null)
            {
                Session["Discount"] = dtOperator;

                ddlOperator.DataSource = dtOperator;
                ddlOperator.DataValueField = "ProviderCode";
                ddlOperator.DataTextField = "Name";
                ddlOperator.DataBind();
                ddlOperator.Items.Insert(0, new ListItem("-SELECT-", "0"));
            }
            else
            {
            }
        }
        protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            //OPERATOR WILL BE BING OF COUNTRY BASIS
            GetOperator();
            Session["Country"] = ddlCountry.SelectedValue;
        }
        public static string InsertGlobalRechargeDetails(string respString)
        {
            if (HttpContext.Current != null)
            {
                Service1Client svcStatic = new Service1Client();
                if (HttpContext.Current.Session["LoginID"] != null)
                {
                    DataTable dtTrans = svcStatic.GetTransactionIDService();
                    int id = Convert.ToInt32(dtTrans.Rows[0]["TRANSACTIONID"]);
                    string transact = id.ToString("00000");
                    transact = ConfigurationManager.AppSettings.Get("TXN_SERIES") + transact;
                    //string res =  "0|1000|15.86|null|null|Complete|1|IN_ID_TopUp_1000.00|15.86|USD|910000000000|1234567";  // Demo Response
                    string[] parameters = respString.Split('|');
                    string TransferRef = parameters[0];
                    string ReceiveValue = parameters[1];
                    decimal TxnAmount = Convert.ToDecimal(parameters[2]); //SendValue
                    string StartedUtc = parameters[3];
                    string TxnDate = parameters[4]; //CompletedUtc
                    string RespMsg = parameters[5]; //ProcessingState
                    string RespCode = parameters[6]; //ResultCode
                    string SkuTariffCode = parameters[7];
                    string SendCurrencyIso = parameters[9];
                    string MobileNumber = parameters[10]; //AccountNumber
                    string DistributorRef = parameters[11];
                    string req = "{SkuCode: " + SkuTariffCode + ",  SendValue: " + TxnAmount + ",  SendCurrencyIso: USD,  AccountNumber: " + MobileNumber + ",  DistributorRef: " + transact + ",  ValidateOnly: true}";
                    string resp = "{TransferRef: " + TransferRef + ",  SkuCode: " + SkuTariffCode + ",  ReceiveValue: " + ReceiveValue + ",  SendValue: " + TxnAmount + ",  StartedUtc: " + StartedUtc + ",  CompletedUtc: " + TxnDate + ", ProcessingState: " + RespMsg + ",  AccountNumber: " + MobileNumber + ",  ResultCode: " + RespCode + "}";
                    string TariffDescription = SkuTariffCode + "SendValue" + TxnAmount;
                    string Country = Convert.ToString(HttpContext.Current.Session["Country"]);
                    string Operator = parameters[12];

                    //Get the discount
                    DataView DvDiscount = new DataView();
                    DvDiscount.Table = (DataTable)(HttpContext.Current.Session["Discount"]);
                    DvDiscount.RowFilter = "ProviderCode='" + Operator + "'";
                    DataTable Dis = DvDiscount.ToTable();
                    decimal Discount = Convert.ToDecimal(Dis.Rows[0]["Discount"]);

                    if (respString.Trim() != null && respString.Trim() != "")
                    {
                        try
                        {
                            SPayment sp = new SPayment();
                            sp.ChargedAmount = ((Convert.ToDecimal(TxnAmount) / Convert.ToDecimal(100.00)) * (Convert.ToDecimal(100.00) - Convert.ToDecimal(Discount)));
                            sp.PaymentType = 26;
                            sp.PayeeID = Convert.ToInt32(HttpContext.Current.Session["DistributorID"]);
                            sp.PaymentFrom = 9; // Distributor
                            sp.ActivationType = 6; //Recharge
                            sp.ActivationStatus = 15; //Active
                            sp.ActivationVia = 17; //Account balance
                            sp.ActivationResp = resp;
                            sp.ActivationRequest = req;
                            sp.TransactionId = transact;
                            sp.PaymentMode = "Global Recharge";
                            sp.TransactionStatusId = 24;
                            sp.TransactionStatus = "Success";
                            int dist = Convert.ToInt32(HttpContext.Current.Session["DistributorID"]);
                            int loginID = Convert.ToInt32(HttpContext.Current.Session["LoginID"]);
                            string Language = "ENGLISH";
                            string ChannelID = ConfigurationManager.AppSettings.Get("CHANNEL");
                            try
                            {
                                int s = svcStatic.UpdateAccountDingRecharge(dist, loginID, MobileNumber, ChannelID, Language, sp, SkuTariffCode, TariffDescription, TransferRef, TxnAmount, RespCode, RespMsg, TxnDate, Country, Operator, SendCurrencyIso);
                                WriteLog(dist, loginID, MobileNumber, transact, Language, ChannelID, "Record Save Success when Ding Recharge is processed");
                            }
                            catch (Exception ex)
                            {
                                WriteLog(dist, loginID, MobileNumber, ex.Message, Language, ChannelID, "Record Save Success when Ding Recharge is processed");
                            }
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                    if (RespMsg == "Complete")
                    {
                        return ("Print.aspx?Tid=" + transact + "&URL=Global Recharge");
                    }
                    else
                    {
                        return ("Recharge Got failed, Please try again");
                    }
                }
                else
                {
                    return ("Session Out");
                }
            }
            else
            {
                return ("Session Out");
            }
        }
        public static void Log1(string ss, string condition)
        {
            try
            {

                string filename = "Recharge.txt";
                string strPath = HttpContext.Current.Server.MapPath("Log") + "/" + filename;
                string root = "https://" + HttpContext.Current.Request.Url.Host + HttpContext.Current.Request.ApplicationPath + "/Log/" + filename;

                if (File.Exists(strPath))
                {
                    StreamWriter sw = new StreamWriter(strPath, true, Encoding.Unicode);
                    if (condition != "split")
                    {
                        sw.WriteLine(condition + "  " + DateTime.UtcNow.AddMinutes(330));
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
        public static void WriteLog(int dist, int loginID, string sim, string zip, string Language, string ChannelID, string msgg)
        {
            StringBuilder logdata = new StringBuilder();
            logdata.Append(dist + "|");
            logdata.Append(loginID + "|");
            logdata.Append(sim + "|");
            logdata.Append(zip + "|");
            logdata.Append(Language + "|");
            logdata.Append(ChannelID + "|");
            string data = logdata.ToString();
            Log1(data, msgg);
            Log1("", "split");
        }

        [System.Web.Services.WebMethod]
        public static string GetRechargeDetails(string Resp)
        {
            string response = InsertGlobalRechargeDetails(Resp);
            return response;
        }
    }
}