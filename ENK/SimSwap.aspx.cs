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


namespace ENK
{
    public partial class SimSwap : System.Web.UI.Page
    {
        Service1Client svc = new Service1Client();
        LycaAPI.LycaAPISoapClient la = new LycaAPI.LycaAPISoapClient();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }


        protected void btnGet_Click(object sender, EventArgs e)
        {
            if (txtMSSIDN.Text != string.Empty && txtNewICCID.Text != string.Empty && txtOldICCID.Text != string.Empty)
            {
                string response = "";
                //int a = 0;
                //a = svc.VerifySimNumber(Convert.ToString(txtOldICCID.Text), Convert.ToString(txtNewICCID.Text));
                //if (a == 0)
                //{
                //    ShowPopUpMsg("New Sim Number does not exists in our inventory");
                //    return;
                //}
                // response = "<ENVELOPE><HEADER><ERROR_CODE>0</ERROR_CODE><ERROR_DESC>Success</ERROR_DESC></HEADER><BODY><SWAP_SIM_RESPONSE></SWAP_SIM_RESPONSE></BODY></ENVELOPE>";
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
                            DataTable dt = ds.Tables[0];
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                if (Convert.ToString(ds.Tables[0].Rows[0]["ERROR_CODE"]) == "0" && Convert.ToString(ds.Tables[0].Rows[0]["ERROR_DESC"]) == "Success")
                                {
                                    //if (Convert.ToInt32(ds.Tables[1].Rows.Count) > 0)
                                    //{
                                    //GrdSwappedSim.DataSource = ds.Tables[1];
                                    //GrdSwappedSim.DataBind();
                                    int x = svc.SwapSimRequest(Convert.ToString(txtMSSIDN.Text), Convert.ToString(txtOldICCID.Text), Convert.ToString(txtNewICCID.Text), Convert.ToInt32(Session["LoginId"]), Convert.ToString(Session["Request"]), Convert.ToString(response), "success");
                                    if (x > 0)
                                    {
                                    }
                                    int b = svc.UpdateSwapSim(Convert.ToString(txtMSSIDN.Text), Convert.ToString(txtOldICCID.Text), Convert.ToString(txtNewICCID.Text), Convert.ToInt64(Session["LoginId"]));

                                    if (b > 0)
                                    {
                                        GrdSwappedSim.DataSource = ds.Tables[0];
                                        GrdSwappedSim.DataBind();
                                        ShowPopUpMsg("Sim Swapped Successfullly");
                                    }

                                    //}
                                }

                                else
                                {

                                    int x = svc.SwapSimRequest(Convert.ToString(txtMSSIDN.Text), Convert.ToString(txtOldICCID.Text), Convert.ToString(txtNewICCID.Text), Convert.ToInt32(Session["LoginId"]), Convert.ToString(Session["Request"]), Convert.ToString(response), "Fail");
                                    if (x > 0)
                                    {

                                    }
                                    GrdSwappedSim.DataSource = ds.Tables[0];
                                    GrdSwappedSim.DataBind();
                                    ShowPopUpMsg("Something went wrong...");
                                }

                            }
                        }


                    }
                    catch (Exception ex)
                    {

                    }
                }
            }

            else
            {
                ShowPopUpMsg("Fields cannot be blank");
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
                string MSISDN = Convert.ToString(txtMSSIDN.Text.Trim());
                string OLDICCID = Convert.ToString(txtOldICCID.Text.Trim());
                string NEWICCID = Convert.ToString(txtNewICCID.Text.Trim());
                string ChannelID = ConfigurationManager.AppSettings.Get("CHANNEL");


                string X = "<ENVELOPE><HEADER><TRANSACTION_ID>" + transact + "</TRANSACTION_ID><ENTITY>" + ChannelID + "</ENTITY><CHANNEL_REFERENCE>" + ChannelID + "</CHANNEL_REFERENCE></HEADER><BODY><SWAP_SIM_REQUEST><DETAILS><OLD_MSISDN>" + MSISDN + "</OLD_MSISDN><OLD_ICCID>" + OLDICCID + "</OLD_ICCID><NEW_ICCID>" + NEWICCID + "</NEW_ICCID></DETAILS></SWAP_SIM_REQUEST></BODY></ENVELOPE>";
                RequestRes = X;
                Session["Request"] = X;
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

        private void ShowPopUpMsg(string msg)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("alert('");
            sb.Append(msg.Replace("\n", "\\n").Replace("\r", "").Replace("'", "\\'"));
            sb.Append("');");
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "showalert", sb.ToString(), true);
        }

        public string Activation(String X, string SOAPAction = "SWAP_SIM")
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
                //string filename = "SimSwap.txt";
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

                string filename = "SimSwap.txt";
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