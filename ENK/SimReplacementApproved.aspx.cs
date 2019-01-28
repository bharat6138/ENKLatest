using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ENK.ServiceReference1;
using System.Configuration;

namespace ENK
{
    public partial class SimReplacementApproved : System.Web.UI.Page
    {
        Service1Client ssc = new Service1Client();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Request.QueryString["MSISDNSIMID"] != "" && Request.QueryString["SIMNo"] != "" && Request.QueryString["ClientID"] != "" && Request.QueryString["UserID"] != "")
                {
                    string MSISDN = Request.QueryString["MSISDNSIMID"].ToString();
                    string SIMNo = Request.QueryString["SIMNo"].ToString();
                    string ClientID = Request.QueryString["ClientID"].ToString();
                    string UserID = Request.QueryString["UserID"].ToString();
                    SIM s = new SIM();
                    s.ClientID = Convert.ToInt32(ClientID);
                    s.MSISDN_SIM_ID = Convert.ToInt64(MSISDN);
                    s.SIMNo = Convert.ToString(SIMNo);
                    s.UserID = Convert.ToInt32(UserID);
                    string simno = Convert.ToString(SIMNo);
                    DataSet ds = ssc.GetInventoryForSIMReplacement("NewSIMNumber", simno, Convert.ToInt32(ClientID));

                    if (ds != null)
                    {
                        if (ds.Tables.Count > 0)
                        {

                            if (ds.Tables[0].Rows.Count > 0 )
                            {

                                int retval = ssc.SIMReplacement(s, Actions.INSERT);
                                if (retval > 0)
                                {
                                    System.Text.StringBuilder javaScript = new System.Text.StringBuilder();
                                    string scriptKey = "ConfirmationScript";

                                    javaScript.Append("var userConfirmation = window.alert('" + "SIM Replacement successfully" + "');\n");
                                    ClientScript.RegisterStartupScript(this.GetType(), scriptKey, javaScript.ToString(), true);
                                    Response.Redirect(ConfigurationManager.AppSettings.Get("COMPANY_INFOEMAIL"));
                                }
                                else
                                {

                                    System.Text.StringBuilder javaScript = new System.Text.StringBuilder();
                                    string scriptKey = "ConfirmationScript";

                                    javaScript.Append("var userConfirmation = window.alert('" + "Error ! Please check" + "');\n");
                                    ClientScript.RegisterStartupScript(this.GetType(), scriptKey, javaScript.ToString(), true);
                                }
                                

                            }
                            else
                            {
                                System.Text.StringBuilder javaScript = new System.Text.StringBuilder();
                                string scriptKey = "ConfirmationScript";

                                javaScript.Append("var userConfirmation = window.alert('" + "New SIM Number Does Not Exist." + "');\n");
                                ClientScript.RegisterStartupScript(this.GetType(), scriptKey, javaScript.ToString(), true);
                            }
                        }
                    }
                }
                else
                {
                    Response.Redirect(ConfigurationManager.AppSettings.Get("COMPANY_INFOEMAIL"));
                }
            }
            catch (Exception ex)
            {

            }
             
            
        }
    }
}