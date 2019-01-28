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
    public partial class ViewNotificationList : System.Web.UI.Page
    {
        Service1Client ssc = new Service1Client();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //SendRequest("", "Hi Sir, How are you?");


                if (Request.QueryString["MsgId"] != null)
                {
                    
                     FillDataParticular();
                }

                else 
                {
                    FillData();
                }

            }
        }


        protected void FillData()
        {
            try
            {
                int userid = Convert.ToInt32(Session["LoginId"]);
                Int64 distributorID = Convert.ToInt64(Session["DistributorID"]);
                DataSet ds = ssc.ViewNotification(distributorID, 0);
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        GridNotification.DataSource = ds.Tables[0];
                        GridNotification.DataBind();
                    }

                    else
                    {
                    }

                }

                else
                {

                }

            }
            catch (Exception ex)
            {

            }
        }

        protected void FillDataParticular()
        {
            try
            {
                int userid = Convert.ToInt32(Session["LoginId"]);
                Int64 distributorID = Convert.ToInt64(Session["DistributorID"]);
                DataSet ds = ssc.ViewNotification(distributorID, Convert.ToInt32(Request.QueryString["MsgId"]));
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        GridNotification.DataSource = ds.Tables[0];
                        GridNotification.DataBind();
                    }

                    else
                    {
                    }

                }

                else
                {

                }

            }
            catch (Exception ex)
            {

            }
        }

        //protected void GridNotification_ItemCommand(object source, RepeaterCommandEventArgs e)
        //{
        //    if (e.CommandName == "View")
        //    {
        //        int id = Convert.ToInt32(e.CommandName);
        //        Response.Redirect("Notification.aspx?MsgId=" + id);
        //    }

        //}

        private void ShowPopUpMsg(string msg)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("alert('");
            sb.Append(msg.Replace("\n", "\\n").Replace("\r", "").Replace("'", "\\'"));
            sb.Append("');");
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "showalert", sb.ToString(), true);
        }

        protected void lblView_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)(sender);
            string id = btn.CommandArgument;
            Response.Redirect("Notification.aspx?MsgId=" + id + "&View=" +"s");
        }

    }
}