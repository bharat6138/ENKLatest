using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Text;
using System.Net;

public partial class PayPalEC : System.Web.UI.Page 
{
    protected void Page_Load(object sender, EventArgs e)
    {
        NVPAPICaller test = new NVPAPICaller();
        string retMsg = "";
        string token = genId();
        Session["payment_amt"] = "10.00";
        if ( HttpContext.Current.Session["payment_amt"] != null)
        {
            string amt = HttpContext.Current.Session["payment_amt"].ToString();

            bool ret = test.ShortcutExpressCheckout(amt, ref token, ref retMsg);
            if (ret)
            {
				HttpContext.Current.Session["token"] = token;
                Response.Redirect( retMsg );
            }
            else
            {
                Response.Redirect("APIError.aspx?" + retMsg);
            }
        }
        else
        {
            Response.Redirect( "APIError.aspx?ErrorCode=AmtMissing" );
        }
    }
    protected string genId()
    {
        var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        var random = new Random();
        var result = new string(
            Enumerable.Repeat(chars, 16)
                      .Select(s => s[random.Next(s.Length)])
                      .ToArray());
        return result; //add a prefix to avoid confusion with the "SECURETOKEN"
    }
}