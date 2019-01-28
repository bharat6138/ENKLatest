using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ENK
{
    public partial class TestPaypal : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void PayWithPayPal()
        {
            string redirecturl = "";

            //Mention URL to redirect content to paypal site
            redirecturl += "https://www.paypal.com/cgi-bin/webscr?cmd=_xclick&business=" + ConfigurationManager.AppSettings["paypalemail"].ToString();
          //  redirecturl += "https://www.sandbox.paypal.com/cgi-bin/webscr?cmd=_xclick&business=" + ConfigurationManager.AppSettings["paypalemail1"].ToString();
            //First name i assign static based on login details assign this value
            redirecturl += "&first_name=" + txtName.Text;

            //City i assign static based on login user detail you change this value
            redirecturl += "&city=bhubaneswar";

            //State i assign static based on login user detail you change this value
            redirecturl += "&state=Odisha";

            //Product Name
            redirecturl += "&item_name=" + txtPurpose.Text;

            //Product Name
            redirecturl += "&amount=" + txtAmount.Text;

            //Phone No
            redirecturl += "&night_phone_a=" + txtPhone.Text;

            //Product Name
            redirecturl += "&item_name=" + txtPurpose.Text;

            //Address 
            redirecturl += "&address1=" + txtEmailId.Text;

            //Business contact id
            // redirecturl += "&business=k.tapankumar@gmail.com";

            //Shipping charges if any
            redirecturl += "&shipping=0";

            //Handling charges if any
            redirecturl += "&handling=0";

            //Tax amount if any
            redirecturl += "&tax=0";

            //Add quatity i added one only statically 
            redirecturl += "&quantity=1";

            //Currency code 
            redirecturl += "&currency=" + ddlCurrency.Text;

            //Success return page url
            redirecturl += "&return=" +
              ConfigurationManager.AppSettings["RechargeSuccessURL"].ToString();

            //Failed return page url
            redirecturl += "&cancel_return=" +
              ConfigurationManager.AppSettings["RechargeSuccessURL"].ToString();

            Response.Redirect(redirecturl);
        }

        protected void btnPay_AsPerYourChoice(object sender, EventArgs e)
        {
            PayWithPayPal();

        }
    }
}