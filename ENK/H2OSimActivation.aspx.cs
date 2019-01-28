using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ENK.net.emida.ws;
using System.IO;
using System.Data;

namespace ENK
{
    public partial class H2OSimActivation : System.Web.UI.Page
    {
        //H2OAPI.webServicesClient H2O = new H2OAPI.webServicesClient();
        ENK.net.emida.ws.webServicesService ws = new webServicesService();
        protected void Page_Load(object sender, EventArgs e)
        {
            string LogInRes = Login("Enkcrt2018", "3nk$crt82018");
            ViewState["LogInRes"] = LogInRes;

            GetProductList();
            ActivateGSMsim();
        }
        public string Login(string username, string password)
        {
            string LogInRes = ws.Login2("01", username, password, "1");
            return LogInRes;
        }
        public void GetProductList()
        {
            string GetProductListRes = "";           
            if (ViewState["LogInRes"].ToString().Contains("USER LOGGED IN SUCCESSFULLY"))
            {
                GetProductListRes = ws.GetProductList("01", "9442391", "", "", "", "");

                StringReader theReader = new StringReader(GetProductListRes);
                DataSet theDataSet = new DataSet();
                theDataSet.ReadXml(theReader);
                if (theDataSet.Tables.Count > 0)
                {
                    DataTable dt = theDataSet.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                       //It means It has data
                    }
                }         
            }
        }
        public void ActivateGSMsim()
        {
            string ActivateGSMsimResp = "";
            if (ViewState["LogInRes"].ToString().Contains("USER LOGGED IN SUCCESSFULLY"))
            {
                ActivateGSMsimResp = ws.LocusActivateGSMsim("9442391", "8814310", "8978", "93000810", "1", "", "", "202", "89014102270204301528", "", "");



                ActivateGSMsimResp = ws.LocusActivateGSMsim("9442391", "8814310", "8978", "93000810", "1", "", "", "202", "89014102270204301510", "MIA", "33128");
                StringReader theReader = new StringReader(ActivateGSMsimResp);
                DataSet theDataSet = new DataSet();
                theDataSet.ReadXml(theReader);
                if (theDataSet.Tables.Count > 0)
                {
                    DataTable dt = theDataSet.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        //It means API is responding
                    }
                }
            }
        }

    }
}