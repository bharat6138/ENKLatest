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

namespace ENK
{
    public partial class TestAPI : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnAPI_Click(object sender, EventArgs e)
        {
            lblAPIResponse.Text = Activation("<ENVELOPE><HEADER><TRANSACTION_ID>AHH1428</TRANSACTION_ID><ENTITY>ENK</ENTITY><CHANNEL_REFERENCE>ENK</CHANNEL_REFERENCE></HEADER><BODY><GET_RESERVED_BUNDLES_REQUEST><MSISDN>19734895289</MSISDN></GET_RESERVED_BUNDLES_REQUEST></BODY></ENVELOPE>");
        }

        public string Activation(String X)
        {
            try
            {
                String strResponse = String.Empty;
                strResponse = SendRequest("http://192.30.220.110:2244", X);
                //strResponse = SendRequest("http://192.30.216.110:2244", X);//"<create-directory-number version=\"1\"> <authentication> <username>admin.puneet</username> <password>stay@9229</password> </authentication> <directory-number>" + DIDNumber + "</directory-number> <directory-number-vendor>toclionly</directory-number-vendor> </create-directory-number>"

                //strResponse = SendRequest("http://83.137.7.3:4006", X);//"<create-directory-number version=\"1\"> <authentication> <username>admin.puneet</username> <password>stay@9229</password> </authentication> <directory-number>" + DIDNumber + "</directory-number> <directory-number-vendor>toclionly</directory-number-vendor> </create-directory-number>"

                //Response.Write(strResponse);
                return strResponse;

            }
            catch (Exception Ex)
            {
                return "*2*" + Ex.Message + "*";
            }

        }

        public string SendRequest(string strURI, string data)
        {
            string _result;
            _result = " ";
            try
            {
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(strURI);
                req.Headers.Add("SOAPAction", "\"GET_RESERVED_BUNDLES\"");

                req.ContentType = "text/xml";
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
    }
}