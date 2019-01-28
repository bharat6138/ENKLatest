using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ENK
{
    public partial class RechargePrint : System.Web.UI.Page
    {
        string msg = "";
        string txToken = "";
        string MobileNo = "";
        string TotalAmount = "";
        string Network = "";
        string TariffCode = "";
        string EmailID = "";
        string PayPalRequest = "";
        string ZIPCode = "";
        string State = "";
        string RechargeAmount = "";
        string Success = "";
        string Tax = "";
        string Regulatery = "";
        string TxnID = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

                string resp = Request.QueryString.Get("RechrgeSuccess");
                if (resp != "" && resp != null)
                {
                    string ssd = Convert.ToString(resp);
                    string[] ActData = ssd.Split(',');
                    MobileNo = ActData[0];
                    RechargeAmount = ActData[1];
                    TotalAmount = ActData[2];
                    Network = ActData[3];

                    TxnID = ActData[4];
                    msg = ActData[5];

                    Log2("Recharge Print Success Report", "Reason");
                    Log2(MobileNo, "SimNumber");
                    Log2(TotalAmount, "AmountPay");
                    Log2(State, "State-City");
                    Log2(ZIPCode, "Zip");
                    Log2(RechargeAmount, "RechargeAmount");
                    Log2(Network, "Network");
                    Log2("", "split");

                    img.Visible = true;

                    lblMessage.Text = "Mobile Number  " + MobileNo + " Recharge  successfully .";

                    lblTransactionAmount.Text = "Transaction Amount =" + TotalAmount;
                    lblRechageAmount.Text = "Plan/Tariff Amount =" + RechargeAmount;
                    lblTransactionDate.Text = "Transaction Date =" + DateTime.Now.ToString();//sd[7];
                    lblPayerName.Text = "Network =" + Network;
                    lblTxnID.Text = "TxnID =" + TxnID;
                    lblmsg.Text =  msg;
                 //   ScriptManager.RegisterStartupScript(this, GetType(), "", "RemoveQueryString();", true);
                    ScriptManager.RegisterStartupScript(this, GetType(), "", "JScriptConfirmationSuccess();", true);
                }

            }
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
    }
}