using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ENK
{
    public partial class paypal : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (PaymentOption == "PayPal")
            //{
            //    NVPAPICaller test = new NVPAPICaller();

            //    string retMsg = "";
            //    string token = "";

            //    if (Session["payment_amt"] != null)
            //    {
            //        string amt = Session["payment_amt"].ToString();

            //        //Optional Shipping Address entered on the merchant site
            //        string shipToName = "<PAYMENTREQUEST_0_SHIPTONAME>";
            //        string shipToStreet = "<PAYMENTREQUEST_0_SHIPTOSTREET>";
            //        string shipToStreet2 = "<PAYMENTREQUEST_0_SHIPTOSTREET2>";
            //        string shipToCity = "<PAYMENTREQUEST_0_SHIPTOCITY>";
            //        string shipToState = "<PAYMENTREQUEST_0_SHIPTOSTATE>";
            //        string shipToZip = "<PAYMENTREQUEST_0_SHIPTOZIP>";
            //        string shipToCountryCode = "<PAYMENTREQUEST_0_SHIPTOCOUNTRYCODE>";

            //        bool ret = test.MarkExpressCheckout(amt, shipToName, shipToStreet, shipToStreet2,
            //                        shipToCity, shipToState, shipToZip, shipToCountryCode,
            //                        ref token, ref retMsg);
            //        if (ret)
            //        {
            //            Session["token"] = token;
            //            Response.Redirect(retMsg);
            //        }
            //        else
            //        {
            //            Response.Redirect("APIError.aspx?" + retMsg);
            //        }
            //    }
            //    else
            //    {
            //        Response.Redirect("APIError.aspx?ErrorCode=AmtMissing");
            //    }
            //}
        }
    }
}