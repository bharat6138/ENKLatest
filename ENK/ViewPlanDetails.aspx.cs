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
using ENK.net.emida.ws;
using ENK.ServiceReference1;
using ENK.LycaAPI;
using System.Globalization;

namespace ENK
{
    public partial class ViewPlanDetails : System.Web.UI.Page
    {
        Service1Client svc = new Service1Client();
        LycaAPI.LycaAPISoapClient la = new LycaAPI.LycaAPISoapClient();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
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


        protected void btnGet_Click(object sender, EventArgs e)
        {
            string response = "";
            string StartDate = "";
            string EndDate = "";
            response = ActivateSim();
            if (response.Trim() != null && response.Trim() != "")
            {
                try
                {
                    StringReader theReader = new StringReader(response);
                    DataSet ds = new DataSet();
                    ds.ReadXml(theReader);

                    if (ds.Tables.Count > 0)
                    {
                        

                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            if (Convert.ToString(ds.Tables[0].Rows[0]["ERROR_CODE"]) == "0" && Convert.ToString(ds.Tables[0].Rows[0]["ERROR_DESC"]) == "success")
                            {
                                DataTable dt = ds.Tables[4];
                                dt.Columns.Add("MSISDN");
                                dt.Columns.Add("ICCID");
                                dt.Columns.Add("PRIMARY_IMSI");
                                dt.Columns.Add("SECONDARY_IMSI");
                                dt.AcceptChanges();
                                dt.Columns["MSISDN"].SetOrdinal(0);
                                dt.Columns["ICCID"].SetOrdinal(1);
                                dt.Columns["PRIMARY_IMSI"].SetOrdinal(2);
                                dt.Columns["SECONDARY_IMSI"].SetOrdinal(3);
                                dt.Columns["BUNDLE_START_DATE"].SetOrdinal(6);
                                dt.Columns["BUNDLE_EXPIRY_TIME"].SetOrdinal(8);

                                for (int i = 0; i < dt.Rows.Count; i++)
                                {
                                    StartDate =  dt.Rows[i]["BUNDLE_START_DATE"].ToString();
                                    StartDate = DateTime.ParseExact(StartDate, "dd-MM-yyyy", CultureInfo.InvariantCulture).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
                                    dt.Rows[i]["BUNDLE_START_DATE"] = StartDate;
                                    EndDate = dt.Rows[i]["BUNDLE_EXPIRY"].ToString();
                                    EndDate = DateTime.ParseExact(EndDate, "dd-MM-yyyy", CultureInfo.InvariantCulture).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
                                    dt.Rows[i]["BUNDLE_EXPIRY"] = EndDate;
                                }
                                if (Convert.ToInt32(ds.Tables[4].Rows.Count) > 0)
                                {
                                    dt.Rows[0]["MSISDN"] = ds.Tables[2].Rows[0]["MSISDN"];
                                    dt.Rows[0]["ICCID"] = ds.Tables[2].Rows[0]["ICC_ID"];
                                    dt.Rows[0]["PRIMARY_IMSI"] = ds.Tables[2].Rows[0]["PRIMARY_IMSI"];
                                    dt.Rows[0]["SECONDARY_IMSI"] = ds.Tables[2].Rows[0]["SECONDARY_IMSI"];
                                    dt.AcceptChanges();

                                    GridPlanDetails.DataSource = dt; //ds.Tables[4]
                                    GridPlanDetails.DataBind();
                                }
                            }

                            else
                            {

                                GridPlanDetails.DataSource = ds.Tables[0];
                                GridPlanDetails.DataBind();
                            }

                        }
                    }
                    //txtMSSIDN.Text = string.Empty;

                }
                catch (Exception ex)
                {

                }
            }
        }

        public string ActivateSim()
        {
            string RequestRes = "";
            string response = "";
            string transact = "";
            try
            {

                DataTable dtTrans = svc.GetTransactionIDService();

                int id = Convert.ToInt32(dtTrans.Rows[0]["TRANSACTIONID"]);
                transact = id.ToString("00000");
                transact = ConfigurationManager.AppSettings.Get("TXN_SERIES") + transact;
                string MSISDN = Convert.ToString(txtMSSIDN.Text);
                string ChannelID = ConfigurationManager.AppSettings.Get("CHANNEL");

                //<ENVELOPE><HEADER><TRANSACTION_ID>ENK3000123458</TRANSACTION_ID><ENTITY>ENKWIR</ENTITY><CHANNEL_REFERENCE>ENKWIR</CHANNEL_REFERENCE></HEADER><BODY><GET_SUBSCRIBER_BUNDLE_INFO_REQUEST><MSISDN>16193257479</MSISDN></GET_SUBSCRIBER_BUNDLE_INFO_REQUEST></BODY></ENVELOPE>

                //string X = "<ENVELOPE><HEADER><TRANSACTION_ID>" + transact + "</TRANSACTION_ID><ENTITY>" + ChannelID + "</ENTITY><CHANNEL_REFERENCE>" + ChannelID + "</CHANNEL_REFERENCE></HEADER><BODY><GET_SUBSCRIBER_PLAN_DETAILS_REQUEST><MSISDN>" + MSISDN + "</MSISDN></GET_SUBSCRIBER_PLAN_DETAILS_REQUEST></BODY></ENVELOPE>";
                string X = "<ENVELOPE><HEADER><TRANSACTION_ID>" + transact + "</TRANSACTION_ID><ENTITY>" + ChannelID + "</ENTITY><CHANNEL_REFERENCE>" + ChannelID + "</CHANNEL_REFERENCE></HEADER><BODY><GET_SUBSCRIBER_BUNDLE_INFO_REQUEST><MSISDN>" + MSISDN + "</MSISDN></GET_SUBSCRIBER_BUNDLE_INFO_REQUEST></BODY></ENVELOPE>";

                RequestRes = X;
                Log(X, "Sending Request");
                response = Activation(X);
                Log1(response, "Port In Response");
                Log("", "split");
                return response;


            }


            catch (Exception ex)
            {
                return response;
                //ShowPopUpMsg(ex.Message);
            }
        }

        public string Activation(String X, string SOAPAction = "GET_SUBSCRIBER_BUNDLE_INFO")
        {
            try
            {
                String strResponse = String.Empty;
                strResponse = la.LycaAPIRequest(ConfigurationManager.AppSettings.Get("APIURL"), X.Replace("<", "==").Replace(">", "!!"), SOAPAction);
                //strResponse = SendRequest(ConfigurationManager.AppSettings.Get("APIURL"), X, SOAPAction);

                return strResponse;

            }
            catch (Exception Ex)
            {
                return "*2*" + Ex.Message + "*";
            }

        }

        public void Log(string ss, string condition)
        {
            try
            {
                //string filename = "lycalog.txt";
                //string strPath = Server.MapPath("Log") + "/" + filename;
                //string root = "http://" + HttpContext.Current.Request.Url.Host + HttpContext.Current.Request.ApplicationPath + "/Log/" + filename;

                //if (File.Exists(strPath))
                //{
                //    StreamWriter sw = new StreamWriter(strPath, true, Encoding.Unicode);
                //    if (condition != "split")
                //    {                        
                //        sw.WriteLine(condition + "  " + DateTime.UtcNow.AddMinutes(330));                       
                //        sw.WriteLine(ss);                      
                //        sw.Close();
                //    }
                //    else
                //    {                        
                //        sw.WriteLine("------------------------------------------------Gap-----------------------------------------------");                      
                //        sw.Close();
                //    }
                //}
            }
            catch (Exception ex)
            {

            }

        }


        public void Log1(string ss, string condition)
        {
            try
            {

                string filename = "Recharge.txt";
                string strPath = Server.MapPath("Log") + "/" + filename;
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

    }
}