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


using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Security.Cryptography.X509Certificates;

using ENK.ServiceReference1;
namespace ENK
{
    public partial class Notification : System.Web.UI.Page
    {
        Service1Client ssc = new Service1Client();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //SendRequest("", "Hi Sir, How are you?");

                // string querystring = Convert.ToString(Request.QueryString["MsgId"]);

                if (Convert.ToString(Request.QueryString["MsgId"]) != null && Convert.ToString(Request.QueryString["View"]) != null)
                {
                    if (Convert.ToString(Request.QueryString["View"]) == "s")
                    {
                        ViewNotification();
                    }

                    if (Convert.ToString(Request.QueryString["View"]) == "r")
                    {

                        notification();
                    }
                }

                else
                {
                    FillData();
                }
            }
        }

        public string SendRequest(string strURI, string data)
        {
            string _result;
            _result = " ";
            try
            {
                strURI = "http://104.197.150.75/ahios/productionAppNotification.php?message=" + data + "&authkey=439729daf54fc2f9fb6b60cce83a6a7131db0462&deviceToken=" + strURI + "";
                //strURI = "http://104.197.150.75/ahios/appNotification.php?message=" + data + "&authkey=439729daf54fc2f9fb6b60cce83a6a7131db0462&deviceToken=" + strURI + "";
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(strURI);
                //req.Headers.Add("deviceToken", "c34574a0491ff1c53b9c117df8fd1fe40b3ace1d41bf832bb989b5a822e10b9d");
                //req.Headers.Add("authkey", "439729daf54fc2f9fb6b60cce83a6a7131db0462");
                //req.Headers.Add("message","Hi Sir, How are you?");
                //req.Headers.Add("SOAPAction", "\"ACTIVATE_USIM_PORTIN_BUNDLE\"");

                req.ContentType = "text/plan";
                //req.ContentType = "application/x-www-form-urlencoded";
                //req.ContentLength = 359;
                //req.Expect = "100-continue";
                //req.Connection = "Keep-Alive";
                //req.SOAPAction="ACTIVATE_USIM";
                //req.Accept = "text/xml";
                req.Method = "POST";
                //request.ContentType = "application/x-www-form-urlencoded";
                var writer = new StreamWriter(req.GetRequestStream());
                writer.Write(data);
                writer.Close();

                var response = (HttpWebResponse)req.GetResponse();
                var streamResponse = response.GetResponseStream();
                var streamRead = new StreamReader(streamResponse);
                _result = streamRead.ReadToEnd().Trim();
                streamRead.Close();
                streamResponse.Close();
                response.Close();

                return _result;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        protected void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chkAll = (CheckBox)sender;
            RepeaterItem row = (RepeaterItem)chkAll.NamingContainer;
            int a = row.ItemIndex;
            if (chkAll.Checked == true)
            {
                for (int i = 0; i < RepeaterDistributor.Items.Count; i++)
                {
                    CheckBox chk = (CheckBox)RepeaterDistributor.Items[i].FindControl("CheckBox2");
                    chk.Checked = true;
                }
            }
            if (chkAll.Checked == false)
            {
                for (int i = 0; i < RepeaterDistributor.Items.Count; i++)
                {
                    CheckBox chk = (CheckBox)RepeaterDistributor.Items[i].FindControl("CheckBox2");
                    chk.Checked = false;
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Boolean sim = false;
                Boolean flag = false;

                for (int i = 0; i < RepeaterDistributor.Items.Count; i++)
                {
                    CheckBox chk = (CheckBox)RepeaterDistributor.Items[i].FindControl("CheckBox2");

                    if (chk.Checked == true)
                    {
                        sim = true;
                        break;
                    }

                }

                if (sim == false)
                {
                    ShowPopUpMsg("Please select any Distributor");
                    return;
                }

                string DistributorID = Convert.ToString(Session["DistributorID"]);
                //string TransferTo = Convert.ToString(ddlTransferTo.SelectedValue);
                string ActionType = string.Empty;
                string MessageOrImage = string.Empty;

                ActionType = "Text";
                MessageOrImage = txtTextString.Text.Trim();

                DataTable dt = new DataTable();
                dt.TableName = "Distributor";
                dt.Columns.Add("DistributorID");

                for (int i = 0; i < RepeaterDistributor.Items.Count; i++)
                {
                    HiddenField hnddistributorID = (HiddenField)RepeaterDistributor.Items[i].FindControl("hnddistributorID");
                    CheckBox CheckBox2 = (CheckBox)RepeaterDistributor.Items[i].FindControl("CheckBox2");

                    if (CheckBox2.Checked == true)
                    {
                        DataRow dr = dt.NewRow();
                        dr["DistributorID"] = hnddistributorID.Value;
                        dt.Rows.Add(dr);
                    }


                }

                dt.AcceptChanges();

                DataSet dsNotification = ssc.SaveAndSendNotification(Convert.ToInt64(Session["LoginID"]), Convert.ToInt64(Session["DistributorID"]), dt, txtTextString.Text, "", "insert", 0);
                if (dsNotification != null)
                {

                    flag = true;
                    //if (dsNotification.Tables[0].Rows.Count > 0)
                    //{
                    //    for (int i = 0; i < dsNotification.Tables[0].Rows.Count; i++)
                    //    {
                    //        if (Convert.ToString(dsNotification.Tables[0].Rows[i]["IOSDeviceTokenID"]) != "No Device")
                    //        {
                    //            string response = SendRequest(Convert.ToString(dsNotification.Tables[0].Rows[i]["IOSDeviceTokenID"]), txtTextString.Text);
                    //            if (response == "Message successfully delivered")
                    //            {
                    //                flag = true;
                    //            }
                    //        }
                    //    }

                    //    List<string> arraylist = new List<string>();
                    //    string[] sc;
                    //    for (int i = 0; i < dsNotification.Tables[0].Rows.Count; i++)
                    //    {
                    //        if (Convert.ToString(dsNotification.Tables[0].Rows[i]["AndroidDeviceTokenID"]) != "No Device")
                    //        {
                    //            arraylist.Add(Convert.ToString(dsNotification.Tables[0].Rows[i]["AndroidDeviceTokenID"]));
                    //        }
                    //    }
                    //    sc = arraylist.ToArray();

                    //    string message = txtTextString.Text;//Convert.ToString(dss.Tables[1].Rows[0][0]); //result1 = cGeneral.GetJson(dss.Tables[1]);

                    //    AndroidGCMPushNotification a = new AndroidGCMPushNotification();
                    //    a.SendNotification(sc, message);

                    //}
                }

                if (flag == true)
                {
                    ShowPopUpMsg("Notification has been sent successfully");
                    txtTextString.Text = string.Empty;

                    for (int i = 0; i < RepeaterDistributor.Items.Count; i++)
                    {
                        CheckBox CheckBox2 = (CheckBox)RepeaterDistributor.Items[i].FindControl("CheckBox2");

                        CheckBox2.Checked = false;
                    }
                }
                else
                {
                    ShowPopUpMsg("Invalid! something went wrong..");
                }
            }
            catch (Exception ex)
            {
                throw (ex);
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
        protected void btnReset_Click(object sender, EventArgs e)
        {
            Response.Redirect("Notification.aspx");
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("Dashboard.aspx");
        }

        public void FillData()
        {
            try
            {
                int userid = Convert.ToInt32(Session["LoginId"]);
                int distributorID = Convert.ToInt32(Session["DistributorID"]);
                Distributor[] ds = ssc.GetDistributorDDLService(userid, distributorID);

                RepeaterDistributor.DataSource = ds;

                RepeaterDistributor.DataBind();

            }
            catch (Exception ex)
            {

            }
        }

        public void ViewNotification()
        {
            DataTable dt = new DataTable();
            Int64 distributorID = Convert.ToInt64(Session["DistributorID"]);
            DataSet ds = ssc.ViewNotification(distributorID, Convert.ToInt32(Request.QueryString["MsgId"]));


            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    RepeaterDistributor.DataSource = ds.Tables[0];

                    RepeaterDistributor.DataBind();

                    txtTextString.Text = ds.Tables[0].Rows[0]["NotificationText"].ToString();

                    for (int i = 0; i < RepeaterDistributor.Items.Count; i++)
                    {
                        CheckBox CheckBox2 = (CheckBox)RepeaterDistributor.Items[i].FindControl("CheckBox2");

                        CheckBox2.Checked = true;
                       
                    }
                }
            }
            Label1.Text = "Sent";
            btnSave.Visible = false;
            btnReset.Visible = false;
        }

        public void notification()
        {
            DataTable dt = new DataTable();
            Int64 distributorID = Convert.ToInt64(Session["DistributorID"]);
            DataSet ds = ssc.GetNotification(distributorID, Convert.ToInt32(Request.QueryString["MsgId"]));
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[1].Rows.Count > 0)
                {
                    RepeaterDistributor.DataSource = ds.Tables[1];

                    RepeaterDistributor.DataBind();

                    txtTextString.Text = ds.Tables[0].Rows[0]["NotificationText"].ToString();

                    for (int i = 0; i < RepeaterDistributor.Items.Count; i++)
                    {
                        CheckBox CheckBox2 = (CheckBox)RepeaterDistributor.Items[i].FindControl("CheckBox2");

                        CheckBox2.Checked = true;
                        //CheckBox2.Visible = false;
                        //Label lbl = (Label)RepeaterDistributor.Items[i].FindControl("distributorName");
                        //lbl.Visible = false;
                        RepeaterDistributor.Visible = false;
                    }

                }
            }

            Label1.Text = "Received";
            btnSave.Visible = false;
            btnReset.Visible = false;
        }
    }
}