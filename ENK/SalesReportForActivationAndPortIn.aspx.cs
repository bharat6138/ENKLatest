using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using ENK.ServiceReference1;


namespace ENK
{
    public partial class SalesReportForActivationAndPortIn : System.Web.UI.Page
    {
        Service1Client ssc = new Service1Client();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                txtFromDate.Attributes.Add("readonly", "true");
                txtToDate.Attributes.Add("readonly", "true");
            if (!Page.IsPostBack)
            {
                ////////////////////////////
                DataTable dt1 = ssc.GetVendor(Convert.ToInt32(Session["LoginId"]));
                if (dt1 != null)
                {
                    if (dt1.Rows.Count > 0)
                    {
                        ddlNetwork.DataSource = dt1;
                        ddlNetwork.DataValueField = "VendorID";
                        ddlNetwork.DataTextField = "VendorName";
                        ddlNetwork.DataBind();
                        ddlNetwork.SelectedIndex = 1;
                       


                    }
                }
                ////////////////////////////
                BindDDL();

                //DateTime FromDate = DateTime.Now;
                //DateTime ToDate = DateTime.Now;
                DateTime today = DateTime.Today;
                int numberOfDaysInMonth = DateTime.DaysInMonth(today.Year, today.Month);

                DateTime FromDate = new DateTime(today.Year, today.Month, 1);
                DateTime ToDate = new DateTime(today.Year, today.Month, numberOfDaysInMonth);

                txtFromDate.Text = Convert.ToString(FromDate.ToString("dd-MMM-yyyy"));
                txtToDate.Text = Convert.ToString(ToDate.ToString("dd-MMM-yyyy"));

                int DistributorID = Convert.ToInt32(Session["DistributorID"]);
                int ClientTypeID = Convert.ToInt32(Session["ClientTypeID"]);
                if (ddlNetwork.SelectedIndex > 0)
                {

                    DataSet ds = ssc.GetSaleReportForActivationAndPortIn(DistributorID, 1, FromDate, ToDate);

                    if (ds != null)
                    {


                        DataTable dt = ds.Tables[0];
                        if (ddlNetwork.SelectedIndex > 0)
                        {
                            DataTable dtSIMPurchase = new DataTable();
                            dtSIMPurchase = ds.Tables[0];
                            DataView dv = dtSIMPurchase.DefaultView;
                            dv.RowFilter = "VendorID = " + Convert.ToInt32(ddlNetwork.SelectedValue);
                            dt = dv.ToTable();

                        }


                        DataTable dtMain = new DataTable();
                        dtMain.Columns.Add("Type");
                        dtMain.Columns.Add("Name");
                        dtMain.Columns.Add("RetialerBundleAmount");
                        dtMain.Columns.Add("Channel Transaction ID");
                        dtMain.Columns.Add("ZipCode");
                        dtMain.Columns.Add("PaymentChannel");
                        dtMain.Columns.Add("ICCID");
                        dtMain.Columns.Add("RealMSISDN");
                        dtMain.Columns.Add("Bundle Code");
                        dtMain.Columns.Add("Bundle Amount");
                        dtMain.Columns.Add("No of Months");
                        //dtMain.Columns.Add("International Bundle Code");
                        //dtMain.Columns.Add("International Bundle Amount");
                        dtMain.Columns.Add("RequestDate");
                        dtMain.Columns.Add("ProcessedDate");
                        dtMain.Columns.Add("PinNumber");
                        dtMain.Columns.Add("Topup Amount");
                        dtMain.Columns.Add("Account Number");

                        if (dt.Rows.Count > 0)
                        {

                            if (ddlNetwork.SelectedItem.Text == "H20" || ddlNetwork.SelectedItem.Text == "EasyGo")
                            {
                                BindH2OAndEasyGo(dt, dtMain);
                            }
                            else if (ddlNetwork.SelectedItem.Text == "Lyca Mobile")
                            {

                                for (int i = 0; i < dt.Rows.Count; i++)
                                {
                                    try
                                    {
                                        DataRow dr = dtMain.NewRow();

                                        StringReader theReader = new StringReader(Convert.ToString(dt.Rows[i]["ActivationRequest"]));
                                        DataSet theDataSet = new DataSet();
                                        theDataSet.ReadXml(theReader);

                                        //DataTable dtDetails = theDataSet.Tables[3];

                                        dr["Type"] = dt.Rows[i]["Type"];
                                        dr["Name"] = dt.Rows[i]["Name"];
                                        dr["RetialerBundleAmount"] = dt.Rows[i]["RetialerBundleAmount"];
                                        dr["Channel Transaction ID"] = theDataSet.Tables[0].Rows[0]["TRANSACTION_ID"];
                                        dr["ZipCode"] = theDataSet.Tables[3].Rows[0]["ZIP_CODE"];
                                        dr["PaymentChannel"] = theDataSet.Tables[3].Rows[0]["CHANNEL_ID"];
                                        dr["ICCID"] = theDataSet.Tables[3].Rows[0]["ICC_ID"];
                                        dr["RealMSISDN"] = dt.Rows[i]["MSISDN"];
                                        if (theDataSet.Tables[3].Rows[0]["NATIONAL_BUNDLE_CODE"] == "")
                                        {
                                            dr["Bundle Code"] = theDataSet.Tables[3].Rows[0]["INTERNATIONAL_BUNDLE_CODE"];
                                            dr["Bundle Amount"] = theDataSet.Tables[3].Rows[0]["INTERNATIONAL_BUNDLE_AMOUNT"];
                                        }
                                        else
                                        {
                                            dr["Bundle Code"] = theDataSet.Tables[3].Rows[0]["NATIONAL_BUNDLE_CODE"];
                                            dr["Bundle Amount"] = theDataSet.Tables[3].Rows[0]["NATIONAL_BUNDLE_AMOUNT"];
                                        }

                                        if (theDataSet.Tables[3].Columns.Count == 17)
                                        {
                                            dr["No of Months"] = theDataSet.Tables[3].Rows[0]["NO_OF_MONTHS"];
                                        }
                                        else
                                        {
                                            dr["No of Months"] = "1";
                                        }
                                        //dr["International Bundle Code"] = theDataSet.Tables[3].Rows[0]["INTERNATIONAL_BUNDLE_CODE"];
                                        //dr["International Bundle Amount"] = theDataSet.Tables[3].Rows[0]["INTERNATIONAL_BUNDLE_AMOUNT"];
                                        dr["RequestDate"] = dt.Rows[i]["ActivationDate"];
                                        dr["ProcessedDate"] = dt.Rows[i]["ActivationDate"];
                                        dr["PinNumber"] = theDataSet.Tables[3].Rows[0]["PASSWORD_PIN"];
                                        dr["Topup Amount"] = theDataSet.Tables[3].Rows[0]["TOPUP_AMOUNT"];
                                        dr["Account Number"] = theDataSet.Tables[3].Rows[0]["ACCOUNT_NUMBER"];

                                        dtMain.Rows.Add(dr);
                                    }
                                    catch { }
                                }

                                RepeaterData.DataSource = dtMain;
                                RepeaterData.DataBind();
                            }
                        }
                        else
                        {
                            RepeaterData.DataSource = dtMain;
                            RepeaterData.DataBind();
                            ShowPopUpMsg("There is no data");
                        }
                    }
                }
                else
                {

                }
                //StringReader theReader = new StringReader("<ENVELOPE><HEADER><TRANSACTION_ID>AHP00062</TRANSACTION_ID><ENTITY>ENK</ENTITY><CHANNEL_REFERENCE>ENK</CHANNEL_REFERENCE></HEADER><BODY><ACTIVATE_USIM_PORTIN_BUNDLE_REQUEST><DETAILS><ICC_ID>8919601000131500395</ICC_ID><ZIP_CODE>95993</ZIP_CODE><PREFERRED_LANGUAGE>ENGLISH</PREFERRED_LANGUAGE><P_MSISDN></P_MSISDN><ACCOUNT_NUMBER></ACCOUNT_NUMBER><PASSWORD_PIN></PASSWORD_PIN><NATIONAL_BUNDLE_CODE>1029</NATIONAL_BUNDLE_CODE><NATIONAL_BUNDLE_AMOUNT>12.00</NATIONAL_BUNDLE_AMOUNT><INTERNATIONAL_BUNDLE_CODE></INTERNATIONAL_BUNDLE_CODE><INTERNATIONAL_BUNDLE_AMOUNT></INTERNATIONAL_BUNDLE_AMOUNT><TOPUP_AMOUNT></TOPUP_AMOUNT><TOPUP_CARD_ID></TOPUP_CARD_ID><VOUCHER_PIN></VOUCHER_PIN><CHANNEL_ID>ENK</CHANNEL_ID><EMAIL_ID>info@ENK.com</EMAIL_ID></DETAILS></ACTIVATE_USIM_PORTIN_BUNDLE_REQUEST></BODY></ENVELOPE>");
                //DataSet theDataSet = new DataSet();
                //theDataSet.ReadXml(theReader);
                //DataSet dss = theDataSet;
            }
            }
            catch (Exception ex)
            {

            }
        }

        public void BindDDL()
        {
            try
            {
                int userid = Convert.ToInt32(Session["LoginId"]);
                int distributorID = Convert.ToInt32(Session["DistributorID"]);
                Distributor[] ds = ssc.GetDistributorDDLService(userid, distributorID);

                ddlDistributor.DataSource = ds;
                ddlDistributor.DataValueField = "distributorID";
                ddlDistributor.DataTextField = "distributorName";
                ddlDistributor.DataBind();
                ddlDistributor.Items.Insert(0, new ListItem("ALL", "0"));

                DataTable dt = ssc.GetVendor(Convert.ToInt32(Session["LoginId"]));
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        ddlNetwork.DataSource = dt;
                        ddlNetwork.DataValueField = "VendorID";
                        ddlNetwork.DataTextField = "VendorName";
                        ddlNetwork.DataBind();
                        //ddlNetwork.Items.Insert(0, new ListItem("---Select Network---", "0"));
                    }
                }



            }
            catch (Exception ex)
            {

            }

        }

        protected void btnGet_Click(object sender, EventArgs e)
        {
            DateTime FromDate;
            DateTime ToDate;

            int DistributorID = Convert.ToInt32(Session["DistributorID"]);
            int ClientTypeID = Convert.ToInt32(Session["ClientTypeID"]);
            int LoginID = Convert.ToInt32(Session["LoginId"]);

            if (txtFromDate.Text.Trim() == "" && txtToDate.Text.Trim() == "")
            {
                FromDate = Convert.ToDateTime("1900-01-01");
                ToDate = DateTime.Now;

                DataSet ds = ssc.GetSaleReportForActivationAndPortIn(Convert.ToInt32(ddlDistributor.SelectedValue), 0, FromDate, ToDate);

                if (ds != null)
                {

                    DataTable dt = ds.Tables[0];
                    if (ddlNetwork.SelectedIndex > 0)
                    {



                        DataTable dtSIMPurchase = new DataTable();
                        dtSIMPurchase = ds.Tables[0];
                        DataView dv = dtSIMPurchase.DefaultView;
                        dv.RowFilter = "VendorID = " + Convert.ToInt32(ddlNetwork.SelectedValue);
                        dt = dv.ToTable();

                    }


                    if (dt.Rows.Count > 0)
                    {
                        DataTable dtMain = new DataTable();

                        dtMain.Columns.Add("Type");
                        dtMain.Columns.Add("Name");
                        dtMain.Columns.Add("RetialerBundleAmount");
                        dtMain.Columns.Add("Channel Transaction ID");
                        dtMain.Columns.Add("ZipCode");
                        dtMain.Columns.Add("PaymentChannel");
                        dtMain.Columns.Add("ICCID");
                        dtMain.Columns.Add("RealMSISDN");
                        dtMain.Columns.Add("Bundle Code");
                        dtMain.Columns.Add("Bundle Amount");
                        dtMain.Columns.Add("No of Months");
                        
                        //dtMain.Columns.Add("International Bundle Code");
                        //dtMain.Columns.Add("International Bundle Amount");
                        dtMain.Columns.Add("RequestDate");
                        dtMain.Columns.Add("ProcessedDate");
                        dtMain.Columns.Add("PinNumber");
                        dtMain.Columns.Add("Topup Amount");
                        dtMain.Columns.Add("Account Number");

                        if (ddlNetwork.SelectedItem.Text == "H20" || ddlNetwork.SelectedItem.Text == "EasyGo")
                        {
                            BindH2OAndEasyGo(dt, dtMain);
                        }
                        else if (ddlNetwork.SelectedItem.Text == "Lyca Mobile")
                        {

                           

                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                 try
                            {
                                
                                DataRow dr = dtMain.NewRow();

                                StringReader theReader = new StringReader(Convert.ToString(dt.Rows[i]["ActivationRequest"]));
                                DataSet theDataSet = new DataSet();
                                theDataSet.ReadXml(theReader);

                                //DataTable dtDetails = theDataSet.Tables[3];

                                dr["Type"] = dt.Rows[i]["Type"];
                                dr["Name"] = dt.Rows[i]["Name"];
                                dr["RetialerBundleAmount"] = dt.Rows[i]["RetialerBundleAmount"];

                                dr["Channel Transaction ID"] = theDataSet.Tables[0].Rows[0]["TRANSACTION_ID"];
                                dr["ZipCode"] = theDataSet.Tables[3].Rows[0]["ZIP_CODE"];
                                dr["PaymentChannel"] = theDataSet.Tables[3].Rows[0]["CHANNEL_ID"];
                                dr["ICCID"] = theDataSet.Tables[3].Rows[0]["ICC_ID"];
                                dr["RealMSISDN"] = dt.Rows[i]["MSISDN"];
                                if (theDataSet.Tables[3].Rows[0]["NATIONAL_BUNDLE_CODE"] == "")
                                {
                                    dr["Bundle Code"] = theDataSet.Tables[3].Rows[0]["INTERNATIONAL_BUNDLE_CODE"];
                                    dr["Bundle Amount"] = theDataSet.Tables[3].Rows[0]["INTERNATIONAL_BUNDLE_AMOUNT"];
                                }
                                else
                                {
                                    dr["Bundle Code"] = theDataSet.Tables[3].Rows[0]["NATIONAL_BUNDLE_CODE"];
                                    dr["Bundle Amount"] = theDataSet.Tables[3].Rows[0]["NATIONAL_BUNDLE_AMOUNT"];
                                }

                                if (theDataSet.Tables[3].Columns.Count == 17)
                                {
                                    dr["No of Months"] = theDataSet.Tables[3].Rows[0]["NO_OF_MONTHS"];
                                }
                                else
                                {
                                    dr["No of Months"] = "1";
                                }
                                //dr["International Bundle Code"] = theDataSet.Tables[3].Rows[0]["INTERNATIONAL_BUNDLE_CODE"];
                                //dr["International Bundle Amount"] = theDataSet.Tables[3].Rows[0]["INTERNATIONAL_BUNDLE_AMOUNT"];
                                dr["RequestDate"] = dt.Rows[i]["ActivationDate"];
                                dr["ProcessedDate"] = dt.Rows[i]["ActivationDate"];
                                dr["PinNumber"] = theDataSet.Tables[3].Rows[0]["PASSWORD_PIN"];
                                dr["Topup Amount"] = theDataSet.Tables[3].Rows[0]["TOPUP_AMOUNT"];
                                dr["Account Number"] = theDataSet.Tables[3].Rows[0]["ACCOUNT_NUMBER"];

                                dtMain.Rows.Add(dr);
                            }
                                 catch { }
                            }

                            RepeaterData.DataSource = dtMain;
                            RepeaterData.DataBind();
                           
                        }

                    }
                    else
                    {

                    }
                }
                else
                {

                }

            }
            else if (txtFromDate.Text.Trim() != "" && txtToDate.Text.Trim() == "")
            {
                FromDate = Convert.ToDateTime("1900-01-01");
                ToDate = DateTime.Now;

                DataSet ds = ssc.GetSaleReportForActivationAndPortIn(Convert.ToInt32(ddlDistributor.SelectedValue), 0, FromDate, ToDate);

                if (ds != null)
                {
                    DataTable dt = ds.Tables[0];
                    if (ddlNetwork.SelectedIndex > 0)
                    {
                        DataTable dtSIMPurchase = new DataTable();
                        dtSIMPurchase = ds.Tables[0];
                        DataView dv = dtSIMPurchase.DefaultView;
                        dv.RowFilter = "VendorID = " + Convert.ToInt32(ddlNetwork.SelectedValue);
                        dt = dv.ToTable();

                    }


                    if (dt.Rows.Count > 0)
                    {
                        DataTable dtMain = new DataTable();

                        dtMain.Columns.Add("Type");
                        dtMain.Columns.Add("Name");
                        dtMain.Columns.Add("RetialerBundleAmount");
                        dtMain.Columns.Add("Channel Transaction ID");
                        dtMain.Columns.Add("ZipCode");
                        dtMain.Columns.Add("PaymentChannel");
                        dtMain.Columns.Add("ICCID");
                        dtMain.Columns.Add("RealMSISDN");
                        dtMain.Columns.Add("Bundle Code");
                        dtMain.Columns.Add("Bundle Amount");
                        dtMain.Columns.Add("No of Months");
                        
                        //dtMain.Columns.Add("International Bundle Code");
                        //dtMain.Columns.Add("International Bundle Amount");
                        dtMain.Columns.Add("RequestDate");
                        dtMain.Columns.Add("ProcessedDate");
                        dtMain.Columns.Add("PinNumber");
                        dtMain.Columns.Add("Topup Amount");
                        dtMain.Columns.Add("Account Number");

                        if (ddlNetwork.SelectedItem.Text == "H20" || ddlNetwork.SelectedItem.Text == "EasyGo")
                        {
                            BindH2OAndEasyGo(dt, dtMain);
                        }
                        else if (ddlNetwork.SelectedItem.Text == "Lyca Mobile")
                        {

                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                try
                                {

                                    DataRow dr = dtMain.NewRow();

                                    StringReader theReader = new StringReader(Convert.ToString(dt.Rows[i]["ActivationRequest"]));
                                    DataSet theDataSet = new DataSet();
                                    theDataSet.ReadXml(theReader);

                                    //DataTable dtDetails = theDataSet.Tables[3];

                                    dr["Type"] = dt.Rows[i]["Type"];
                                    dr["Name"] = dt.Rows[i]["Name"];
                                    dr["RetialerBundleAmount"] = dt.Rows[i]["RetialerBundleAmount"];

                                    dr["Channel Transaction ID"] = theDataSet.Tables[0].Rows[0]["TRANSACTION_ID"];
                                    dr["ZipCode"] = theDataSet.Tables[3].Rows[0]["ZIP_CODE"];
                                    dr["PaymentChannel"] = theDataSet.Tables[3].Rows[0]["CHANNEL_ID"];
                                    dr["ICCID"] = theDataSet.Tables[3].Rows[0]["ICC_ID"];
                                    dr["RealMSISDN"] = dt.Rows[i]["MSISDN"];
                                    if (theDataSet.Tables[3].Rows[0]["NATIONAL_BUNDLE_CODE"] == "")
                                    {
                                        dr["Bundle Code"] = theDataSet.Tables[3].Rows[0]["INTERNATIONAL_BUNDLE_CODE"];
                                        dr["Bundle Amount"] = theDataSet.Tables[3].Rows[0]["INTERNATIONAL_BUNDLE_AMOUNT"];
                                    }
                                    else
                                    {
                                        dr["Bundle Code"] = theDataSet.Tables[3].Rows[0]["NATIONAL_BUNDLE_CODE"];
                                        dr["Bundle Amount"] = theDataSet.Tables[3].Rows[0]["NATIONAL_BUNDLE_AMOUNT"];
                                    }

                                    if (theDataSet.Tables[3].Columns.Count == 17)
                                    {
                                        dr["No of Months"] = theDataSet.Tables[3].Rows[0]["NO_OF_MONTHS"];
                                    }
                                    else
                                    {
                                        dr["No of Months"] = "1";
                                    }
                                    //dr["International Bundle Code"] = theDataSet.Tables[3].Rows[0]["INTERNATIONAL_BUNDLE_CODE"];
                                    //dr["International Bundle Amount"] = theDataSet.Tables[3].Rows[0]["INTERNATIONAL_BUNDLE_AMOUNT"];
                                    dr["RequestDate"] = dt.Rows[i]["ActivationDate"];
                                    dr["ProcessedDate"] = dt.Rows[i]["ActivationDate"];
                                    dr["PinNumber"] = theDataSet.Tables[3].Rows[0]["PASSWORD_PIN"];
                                    dr["Topup Amount"] = theDataSet.Tables[3].Rows[0]["TOPUP_AMOUNT"];
                                    dr["Account Number"] = theDataSet.Tables[3].Rows[0]["ACCOUNT_NUMBER"];

                                    dtMain.Rows.Add(dr);
                                }
                                catch { }
                            }

                            RepeaterData.DataSource = dtMain;
                            RepeaterData.DataBind();
                        }
                    }
                    else
                    {

                    }
                }
                else
                {

                }
            }
            else if (txtFromDate.Text.Trim() != "" && txtToDate.Text.Trim() != "")
            {
                FromDate = Convert.ToDateTime(txtFromDate.Text.Trim());
                ToDate = Convert.ToDateTime(txtToDate.Text.Trim());
                DataSet ds;
                if (ddlDistributor.SelectedValue == "0")
                {
                     ds = ssc.GetSaleReportForActivationAndPortIn(DistributorID, 1, FromDate, ToDate);
                    
                }
                else
                {
                     ds = ssc.GetSaleReportForActivationAndPortIn(Convert.ToInt32(ddlDistributor.SelectedValue), 0, FromDate, ToDate);
               
                }
                

                if (ds != null)
                {
                    DataTable dt = ds.Tables[0];
                    if (ddlNetwork.SelectedIndex > 0)
                    {
                        DataTable dtSIMPurchase = new DataTable();
                        dtSIMPurchase = ds.Tables[0];
                        DataView dv = dtSIMPurchase.DefaultView;
                        dv.RowFilter = "VendorID = " + Convert.ToInt32(ddlNetwork.SelectedValue);
                        dt = dv.ToTable();

                    }


                    if (dt.Rows.Count > 0)
                    {
                        DataTable dtMain = new DataTable();

                        dtMain.Columns.Add("Type");
                        dtMain.Columns.Add("Name");
                        dtMain.Columns.Add("RetialerBundleAmount");
                        dtMain.Columns.Add("Channel Transaction ID");
                        dtMain.Columns.Add("ZipCode");
                        dtMain.Columns.Add("PaymentChannel");
                        dtMain.Columns.Add("ICCID");
                        dtMain.Columns.Add("RealMSISDN");
                        dtMain.Columns.Add("Bundle Code");
                        dtMain.Columns.Add("Bundle Amount");
                        dtMain.Columns.Add("No of Months");
                       
                        //dtMain.Columns.Add("International Bundle Code");
                        //dtMain.Columns.Add("International Bundle Amount");
                        dtMain.Columns.Add("RequestDate");
                        dtMain.Columns.Add("ProcessedDate");
                        dtMain.Columns.Add("PinNumber");
                        dtMain.Columns.Add("Topup Amount");
                        dtMain.Columns.Add("Account Number");

                        if (ddlNetwork.SelectedItem.Text == "H20" || ddlNetwork.SelectedItem.Text == "EasyGo")
                        {
                            BindH2OAndEasyGo(dt, dtMain);
                        }
                        else if (ddlNetwork.SelectedItem.Text == "Lyca Mobile")
                        {

                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                try
                                {
                                    DataRow dr = dtMain.NewRow();

                                    StringReader theReader = new StringReader(Convert.ToString(dt.Rows[i]["ActivationRequest"]));
                                    DataSet theDataSet = new DataSet();
                                    theDataSet.ReadXml(theReader);

                                    //DataTable dtDetails = theDataSet.Tables[3];

                                    dr["Type"] = dt.Rows[i]["Type"];
                                    dr["Name"] = dt.Rows[i]["Name"];
                                    dr["RetialerBundleAmount"] = dt.Rows[i]["RetialerBundleAmount"];

                                    dr["Channel Transaction ID"] = theDataSet.Tables[0].Rows[0]["TRANSACTION_ID"];
                                    dr["ZipCode"] = theDataSet.Tables[3].Rows[0]["ZIP_CODE"];
                                    dr["PaymentChannel"] = theDataSet.Tables[3].Rows[0]["CHANNEL_ID"];
                                    dr["ICCID"] = theDataSet.Tables[3].Rows[0]["ICC_ID"];
                                    dr["RealMSISDN"] = dt.Rows[i]["MSISDN"];
                                    if (theDataSet.Tables[3].Rows[0]["NATIONAL_BUNDLE_CODE"] == "")
                                    {
                                        dr["Bundle Code"] = theDataSet.Tables[3].Rows[0]["INTERNATIONAL_BUNDLE_CODE"];
                                        dr["Bundle Amount"] = theDataSet.Tables[3].Rows[0]["INTERNATIONAL_BUNDLE_AMOUNT"];
                                    }
                                    else
                                    {
                                        dr["Bundle Code"] = theDataSet.Tables[3].Rows[0]["NATIONAL_BUNDLE_CODE"];
                                        dr["Bundle Amount"] = theDataSet.Tables[3].Rows[0]["NATIONAL_BUNDLE_AMOUNT"];
                                    }
                                    if (theDataSet.Tables[3].Columns.Count == 17)
                                    {
                                        dr["No of Months"] = theDataSet.Tables[3].Rows[0]["NO_OF_MONTHS"];
                                    }
                                    else
                                    {
                                        dr["No of Months"] = "1";
                                    }

                                    //dr["International Bundle Code"] = theDataSet.Tables[3].Rows[0]["INTERNATIONAL_BUNDLE_CODE"];
                                    //dr["International Bundle Amount"] = theDataSet.Tables[3].Rows[0]["INTERNATIONAL_BUNDLE_AMOUNT"];
                                    dr["RequestDate"] = dt.Rows[i]["ActivationDate"];
                                    dr["ProcessedDate"] = dt.Rows[i]["ActivationDate"];
                                    dr["PinNumber"] = theDataSet.Tables[3].Rows[0]["PASSWORD_PIN"];
                                    dr["Topup Amount"] = theDataSet.Tables[3].Rows[0]["TOPUP_AMOUNT"];
                                    dr["Account Number"] = theDataSet.Tables[3].Rows[0]["ACCOUNT_NUMBER"];

                                    dtMain.Rows.Add(dr);
                                }
                                catch { }
                            }
                            RepeaterData.DataSource = dtMain;
                            RepeaterData.DataBind();
                        }

                        
                    }
                    else
                    {
                        DataTable dtMain = new DataTable();

                        dtMain.Columns.Add("Type");
                        dtMain.Columns.Add("Name");
                        dtMain.Columns.Add("RetialerBundleAmount");
                        dtMain.Columns.Add("Channel Transaction ID");
                        dtMain.Columns.Add("ZipCode");
                        dtMain.Columns.Add("PaymentChannel");
                        dtMain.Columns.Add("ICCID");
                        dtMain.Columns.Add("RealMSISDN");
                        dtMain.Columns.Add("Bundle Code");
                        dtMain.Columns.Add("Bundle Amount");
                        dtMain.Columns.Add("No of Months");
                       
                        //dtMain.Columns.Add("International Bundle Code");
                        //dtMain.Columns.Add("International Bundle Amount");
                        dtMain.Columns.Add("RequestDate");
                        dtMain.Columns.Add("ProcessedDate");
                        dtMain.Columns.Add("PinNumber");
                        dtMain.Columns.Add("Topup Amount");
                        dtMain.Columns.Add("Account Number");

                        DataRow dr = dtMain.NewRow();

                        dr["Type"] = "";
                        dr["Name"] = "";
                        dr["RetialerBundleAmount"] = "";
                        
                        dr["Channel Transaction ID"] = "";
                        dr["ZipCode"] = "";
                        dr["PaymentChannel"] = "";
                        dr["ICCID"] = "";
                        dr["RealMSISDN"] = "";
                        dr["Bundle Code"] = "";
                        dr["Bundle Amount"] = "";
                        
                        dr["No of Months"] = "";
                        //dr["International Bundle Code"] = "";
                        //dr["International Bundle Amount"] = "";
                        dr["RequestDate"] = "";
                        dr["ProcessedDate"] = "";
                        dr["PinNumber"] = "";
                        dr["Topup Amount"] = "";
                        dr["Account Number"] = "";

                        dtMain.Rows.Add(dr);

                        RepeaterData.DataSource = dtMain;
                        RepeaterData.DataBind();
                        ShowPopUpMsg("There is no data.");
                    }
                }
                else
                {
                    DataTable dtMain = new DataTable();

                    dtMain.Columns.Add("Type");
                    dtMain.Columns.Add("Name");
                    dtMain.Columns.Add("RetialerBundleAmount");
                    dtMain.Columns.Add("Channel Transaction ID");
                    dtMain.Columns.Add("ZipCode");
                    dtMain.Columns.Add("PaymentChannel");
                    dtMain.Columns.Add("ICCID");
                    dtMain.Columns.Add("RealMSISDN");
                    dtMain.Columns.Add("National Bundle Code");
                    dtMain.Columns.Add("National Bundle Amount");
                    dtMain.Columns.Add("International Bundle Code");
                    dtMain.Columns.Add("International Bundle Amount");
                    dtMain.Columns.Add("RequestDate");
                    dtMain.Columns.Add("ProcessedDate");
                    dtMain.Columns.Add("PinNumber");
                    dtMain.Columns.Add("Topup Amount");
                    dtMain.Columns.Add("Account Number");

                    DataRow dr = dtMain.NewRow();

                    dr["Type"] = "";
                    dr["Name"] = "";
                    dr["RetialerBundleAmount"] = "";
                    
                    dr["Channel Transaction ID"] = "";
                    dr["ZipCode"] = "";
                    dr["PaymentChannel"] = "";
                    dr["ICCID"] = "";
                    dr["RealMSISDN"] = "";
                    dr["National Bundle Code"] = "";
                    dr["National Bundle Amount"] = "";
                    dr["International Bundle Code"] = "";
                    dr["International Bundle Amount"] = "";
                    dr["RequestDate"] = "";
                    dr["ProcessedDate"] = "";
                    dr["PinNumber"] = "";
                    dr["Topup Amount"] = "";
                    dr["Account Number"] = "";

                    dtMain.Rows.Add(dr);

                    RepeaterData.DataSource = dtMain;
                    RepeaterData.DataBind();

                    ShowPopUpMsg("There is no data.");
                }
            }
            
        }


        private void BindH2OAndEasyGo(DataTable dt, DataTable dtMain)
        {
            try
            {
                               for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                try
                                {

                                    DataRow dr = dtMain.NewRow();

                                    dr["Type"] = dt.Rows[i]["Type"];
                                    dr["Name"] = dt.Rows[i]["Name"];
                                    dr["RetialerBundleAmount"] = dt.Rows[i]["RetialerBundleAmount"];

                                    dr["Channel Transaction ID"] = dt.Rows[i]["TransactionID"];
                                    dr["ZipCode"] = dt.Rows[i]["Zip"];
                                    dr["PaymentChannel"] = System.Configuration.ConfigurationManager.AppSettings.Get("TXN_SERIES");
                                    dr["ICCID"] = dt.Rows[i]["SerialNumber"];
                                    dr["RealMSISDN"] = dt.Rows[i]["MSISDN"];
                                    dr["Bundle Code"] = "";
                                    dr["Bundle Amount"] = dt.Rows[i]["BundleAmount_New"];

                                    dr["No of Months"] = "1";


                                    dr["RequestDate"] = dt.Rows[i]["ActivationDate"];
                                    dr["ProcessedDate"] = dt.Rows[i]["ActivationDate"];
                                    dr["PinNumber"] = "";
                                    dr["Topup Amount"] = "";
                                    dr["Account Number"] = "";

                                    dtMain.Rows.Add(dr);
                                }
                                catch { }
                            }
                            RepeaterData.DataSource = dtMain;
                            RepeaterData.DataBind();
            }
            catch (Exception)
            {
                
                throw;
            }
        
        
        }


        protected void btnExportToExcel_Click(object sender, EventArgs e)
        {
            DateTime FromDate = Convert.ToDateTime(txtFromDate.Text.Trim());
            DateTime ToDate = Convert.ToDateTime(txtToDate.Text.Trim());

            int DistributorID = Convert.ToInt32(Session["DistributorID"]);
            int ClientTypeID = Convert.ToInt32(Session["ClientTypeID"]);
            DataSet ds;
            if (ddlDistributor.SelectedValue == "0")
            {
                 ds = ssc.GetSaleReportForActivationAndPortIn(DistributorID, 1, FromDate, ToDate);
            }
            else
            {
                 ds = ssc.GetSaleReportForActivationAndPortIn(Convert.ToInt32(ddlDistributor.SelectedValue), 0, FromDate, ToDate);
            }
            
            DataTable dtMain = new DataTable();
            if (ds != null)
            {

                DataTable dt = ds.Tables[0];

                //if (ddlNetwork.SelectedIndex > 0)
                //{

                  
                //    DataView dv = dt.DefaultView;
                //    dv.RowFilter = "VendorID = " + Convert.ToInt32(ddlNetwork.SelectedValue);
                //    dt = dv.ToTable();

                //}
               

                if (ddlNetwork.SelectedIndex > 0)
                {
                    DataTable dtSIMPurchase = new DataTable();
                    dtSIMPurchase = ds.Tables[0];
                    DataView dv = dtSIMPurchase.DefaultView;
                    dv.RowFilter = "VendorID = " + Convert.ToInt32(ddlNetwork.SelectedValue);
                    dt = dv.ToTable();

                }
                if (dt.Rows.Count > 0)
                {


                    dtMain.Columns.Add("Type");
                    dtMain.Columns.Add("Name");
                    dtMain.Columns.Add("RetialerBundleAmount");
                    dtMain.Columns.Add("TRANSACTION_ID");
                    dtMain.Columns.Add("ZIP_CODE");
                    dtMain.Columns.Add("CHANNEL_ID");
                    dtMain.Columns.Add("ICC_ID");
                    dtMain.Columns.Add("MSISDN");
                    dtMain.Columns.Add("BUNDLE_CODE");
                    dtMain.Columns.Add("BUNDLE_AMOUNT");
                    dtMain.Columns.Add("No of Months");
                    
                    //dtMain.Columns.Add("INTERNATIONAL_BUNDLE_CODE");
                    //dtMain.Columns.Add("INTERNATIONAL_BUNDLE_AMOUNT");
                    dtMain.Columns.Add("ActivationDate");
                    dtMain.Columns.Add("ActivationDate1");
                    dtMain.Columns.Add("PASSWORD_PIN");
                    dtMain.Columns.Add("TOPUP_AMOUNT");
                    dtMain.Columns.Add("ACCOUNT_NUMBER");

                    if (ddlNetwork.SelectedItem.Text == "H20" || ddlNetwork.SelectedItem.Text == "EasyGo")
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            try
                            {
                                DataRow dr = dtMain.NewRow();

                                dr["Type"] = dt.Rows[i]["Type"];
                                dr["Name"] = dt.Rows[i]["Name"];
                                dr["RetialerBundleAmount"] = dt.Rows[i]["RetialerBundleAmount"];

                                dr["TRANSACTION_ID"] = dt.Rows[i]["TransactionID"];
                                dr["ZIP_CODE"] = dt.Rows[i]["Zip"];
                                dr["CHANNEL_ID"] = System.Configuration.ConfigurationManager.AppSettings.Get("TXN_SERIES");
                                dr["ICC_ID"] = "'" + dt.Rows[i]["SerialNumber"];
                                dr["MSISDN"] = "'" + dt.Rows[i]["MSISDN"];

                                dr["BUNDLE_CODE"] = "";
                                dr["BUNDLE_AMOUNT"] = dt.Rows[i]["BundleAmount_New"];

                                dr["No of Months"] = "1";


                                dr["ActivationDate"] = dt.Rows[i]["ActivationDate"];
                                dr["ActivationDate1"] = dt.Rows[i]["ActivationDate"];
                                //dr["ProcessedDate"] = dt.Rows[i]["ActivationDate"];
                                dr["PASSWORD_PIN"] = "";
                                dr["TOPUP_AMOUNT"] = "";
                                dr["ACCOUNT_NUMBER"] = "";

                                dtMain.Rows.Add(dr);
                            }
                            catch { }
                        }
                    }
                    else if (ddlNetwork.SelectedItem.Text == "Lyca Mobile")
                    {



                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            try
                            {

                                DataRow dr = dtMain.NewRow();

                                StringReader theReader = new StringReader(Convert.ToString(dt.Rows[i]["ActivationRequest"]));
                                DataSet theDataSet = new DataSet();
                                theDataSet.ReadXml(theReader);

                                //DataTable dtDetails = theDataSet.Tables[3];

                                dr["Type"] = dt.Rows[i]["Type"];
                                dr["Name"] = dt.Rows[i]["Name"];
                                dr["RetialerBundleAmount"] = dt.Rows[i]["RetialerBundleAmount"];

                                dr["TRANSACTION_ID"] = theDataSet.Tables[0].Rows[0]["TRANSACTION_ID"];
                                dr["ZIP_CODE"] = theDataSet.Tables[3].Rows[0]["ZIP_CODE"];
                                dr["CHANNEL_ID"] = theDataSet.Tables[3].Rows[0]["CHANNEL_ID"];
                                dr["ICC_ID"] = "'" + theDataSet.Tables[3].Rows[0]["ICC_ID"];
                                dr["MSISDN"] = "'" + dt.Rows[i]["MSISDN"];
                                if (theDataSet.Tables[3].Rows[0]["NATIONAL_BUNDLE_CODE"] == "")
                                {
                                    dr["BUNDLE_CODE"] = theDataSet.Tables[3].Rows[0]["INTERNATIONAL_BUNDLE_CODE"];
                                    dr["BUNDLE_AMOUNT"] = theDataSet.Tables[3].Rows[0]["INTERNATIONAL_BUNDLE_AMOUNT"];
                                }
                                else
                                {
                                    dr["BUNDLE_CODE"] = theDataSet.Tables[3].Rows[0]["NATIONAL_BUNDLE_CODE"];
                                    dr["BUNDLE_AMOUNT"] = theDataSet.Tables[3].Rows[0]["NATIONAL_BUNDLE_AMOUNT"];
                                }

                                if (theDataSet.Tables[3].Columns.Count == 17)
                                {
                                    dr["No of Months"] = theDataSet.Tables[3].Rows[0]["NO_OF_MONTHS"];
                                }
                                else
                                {
                                    dr["No of Months"] = "1";
                                }
                                //dr["NATIONAL_BUNDLE_CODE"] = theDataSet.Tables[3].Rows[0]["NATIONAL_BUNDLE_CODE"];
                                //dr["NATIONAL_BUNDLE_AMOUNT"] = theDataSet.Tables[3].Rows[0]["NATIONAL_BUNDLE_AMOUNT"];
                                //dr["INTERNATIONAL_BUNDLE_CODE"] = theDataSet.Tables[3].Rows[0]["INTERNATIONAL_BUNDLE_CODE"];
                                //dr["INTERNATIONAL_BUNDLE_AMOUNT"] = theDataSet.Tables[3].Rows[0]["INTERNATIONAL_BUNDLE_AMOUNT"];
                                dr["ActivationDate"] = dt.Rows[i]["ActivationDate"];
                                dr["ActivationDate1"] = dt.Rows[i]["ActivationDate"];
                                dr["PASSWORD_PIN"] = theDataSet.Tables[3].Rows[0]["PASSWORD_PIN"];
                                dr["TOPUP_AMOUNT"] = theDataSet.Tables[3].Rows[0]["TOPUP_AMOUNT"];
                                dr["ACCOUNT_NUMBER"] = "'" + theDataSet.Tables[3].Rows[0]["ACCOUNT_NUMBER"];

                                dtMain.Rows.Add(dr);
                            }
                            catch { }
                        }
                    }
                    DataView view = new DataView(dtMain);

                    DataTable dtExcel = view.ToTable("Selected", false, "Type", "Name", "RetialerBundleAmount", "TRANSACTION_ID", "ZIP_CODE", "CHANNEL_ID", "ICC_ID", "MSISDN", "BUNDLE_CODE", "BUNDLE_AMOUNT", "No of Months", "ActivationDate", "ActivationDate1", "PASSWORD_PIN", "TOPUP_AMOUNT", "ACCOUNT_NUMBER");//, "INTERNATIONAL_BUNDLE_CODE", "INTERNATIONAL_BUNDLE_AMOUNT"


                    dtExcel.Columns["Type"].ColumnName = "Type";
                    dtExcel.Columns["Name"].ColumnName = "Name";
                    dtExcel.Columns["RetialerBundleAmount"].ColumnName = "RetialerBundleAmount";
                    
                    dtExcel.Columns["TRANSACTION_ID"].ColumnName = "TRANSACTION_ID";
                    dtExcel.Columns["ZIP_CODE"].ColumnName = "ZIP_CODE";
                    dtExcel.Columns["CHANNEL_ID"].ColumnName = "CHANNEL_ID";
                    dtExcel.Columns["ICC_ID"].ColumnName = "ICC_ID";
                    dtExcel.Columns["MSISDN"].ColumnName = "RealMSISDN";

                    dtExcel.Columns["BUNDLE_CODE"].ColumnName = "BUNDLE_CODE";
                    dtExcel.Columns["BUNDLE_AMOUNT"].ColumnName = "BUNDLE_AMOUNT";
                    dtExcel.Columns["No of Months"].ColumnName = "No of Months";
                    //dtExcel.Columns["INTERNATIONAL_BUNDLE_CODE"].ColumnName = "INTERNATIONAL_BUNDLE_CODE";
                    //dtExcel.Columns["INTERNATIONAL_BUNDLE_AMOUNT"].ColumnName = "INTERNATIONAL_BUNDLE_AMOUNT";
                    dtExcel.Columns["ActivationDate"].ColumnName = "RequestDate";
                    dtExcel.Columns["ActivationDate1"].ColumnName = "ProcessedDate";
                    dtExcel.Columns["PASSWORD_PIN"].ColumnName = "PASSWORD_PIN";
                    dtExcel.Columns["TOPUP_AMOUNT"].ColumnName = "TOPUP_AMOUNT";
                    dtExcel.Columns["ACCOUNT_NUMBER"].ColumnName = "ACCOUNT_NUMBER";


                    if (dtExcel.Rows.Count > 0)
                    {
                        string filename = "SalesReport.xls";
                        System.IO.StringWriter tw = new System.IO.StringWriter();
                        System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);
                        GridView grdView = new GridView();
                        //dgGrid.HeaderStyle
                        grdView.DataSource = dtExcel;
                        grdView.DataBind();

                        ScriptManager.RegisterStartupScript(this, GetType(), "", "HideProgress();", true);
                        //Get the HTML for the control.
                        grdView.RenderControl(hw);
                        //Write the HTML back to the browser.
                        //Response.ContentType = application/vnd.ms-excel;
                        Response.ContentType = "application/vnd.ms-excel";
                        Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename + "");
                        this.EnableViewState = false;
                        Response.Write(tw.ToString());
                        Response.End();

                        ScriptManager.RegisterStartupScript(this, GetType(), "", "HideProgress();", true);
                    }
                }
                else
                {
                    //DataTable dtMain = new DataTable();

                    dtMain.Columns.Add("Type");
                    dtMain.Columns.Add("Name");
                    dtMain.Columns.Add("RetialerBundleAmount");
                    dtMain.Columns.Add("Channel Transaction ID");
                    dtMain.Columns.Add("ZipCode");
                    dtMain.Columns.Add("PaymentChannel");
                    dtMain.Columns.Add("ICCID");
                    dtMain.Columns.Add("RealMSISDN");
                    dtMain.Columns.Add("Bundle Code");
                    dtMain.Columns.Add("Bundle Amount");
                    dtMain.Columns.Add("No of Months");
                    
                    //dtMain.Columns.Add("International Bundle Code");
                    //dtMain.Columns.Add("International Bundle Amount");
                    dtMain.Columns.Add("RequestDate");
                    dtMain.Columns.Add("ProcessedDate");
                    dtMain.Columns.Add("PinNumber");
                    dtMain.Columns.Add("Topup Amount");
                    dtMain.Columns.Add("Account Number");

                    DataRow dr = dtMain.NewRow();

                    dr["Type"] = "";
                    dr["Name"] = "";
                    dr["RetialerBundleAmount"] = "";
                    
                    dr["Channel Transaction ID"] = "";
                    dr["ZipCode"] = "";
                    dr["PaymentChannel"] = "";
                    dr["ICCID"] = "";
                    dr["RealMSISDN"] = "";
                    dr["Bundle Code"] = "";
                    dr["Bundle Amount"] = "";
                    
                    dr["No of Months"] = "";
                    //dr["International Bundle Code"] = "";
                    //dr["International Bundle Amount"] = "";
                    dr["RequestDate"] = "";
                    dr["ProcessedDate"] = "";
                    dr["PinNumber"] = "";
                    dr["Topup Amount"] = "";
                    dr["Account Number"] = "";

                    dtMain.Rows.Add(dr);

                    RepeaterData.DataSource = dtMain;
                    RepeaterData.DataBind();
                    ShowPopUpMsg("There is no data to Export to Excel");
                }
            }
            else
            {
                //DataTable dtMain = new DataTable();

                dtMain.Columns.Add("Type");
                dtMain.Columns.Add("Name");
                dtMain.Columns.Add("RetialerBundleAmount");
                dtMain.Columns.Add("Channel Transaction ID");
                dtMain.Columns.Add("ZipCode");
                dtMain.Columns.Add("PaymentChannel");
                dtMain.Columns.Add("ICCID");
                dtMain.Columns.Add("RealMSISDN");
                dtMain.Columns.Add("Bundle Code");
                dtMain.Columns.Add("Bundle Amount");
                dtMain.Columns.Add("No of Months");
                
                //dtMain.Columns.Add("International Bundle Code");
                //dtMain.Columns.Add("International Bundle Amount");
                dtMain.Columns.Add("RequestDate");
                dtMain.Columns.Add("ProcessedDate");
                dtMain.Columns.Add("PinNumber");
                dtMain.Columns.Add("Topup Amount");
                dtMain.Columns.Add("Account Number");

                DataRow dr = dtMain.NewRow();

                dr["Type"] = "";
                dr["Name"] = "";
                dr["RetialerBundleAmount"] = "";
                
                dr["Channel Transaction ID"] = "";
                dr["ZipCode"] = "";
                dr["PaymentChannel"] = "";
                dr["ICCID"] = "";
                dr["RealMSISDN"] = "";
                dr["Bundle Code"] = "";
                dr["Bundle Amount"] = "";
               
                dr["No of Months"] = "";
                //dr["International Bundle Code"] = "";
                //dr["International Bundle Amount"] = "";
                dr["RequestDate"] = "";
                dr["ProcessedDate"] = "";
                dr["PinNumber"] = "";
                dr["Topup Amount"] = "";
                dr["Account Number"] = "";

                dtMain.Rows.Add(dr);

                RepeaterData.DataSource = dtMain;
                RepeaterData.DataBind();

                ShowPopUpMsg("There is no data to Export to Excel");
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

        protected void ddlNetwork_SelectedIndexChanged(object sender, EventArgs e)
        {

            //try
            //{
            //    DataTable dtSIMPurchase = new DataTable();

            //    if (ddlNetwork.SelectedIndex > 0)
            //    {

            //        dtSIMPurchase = (DataTable)ViewState["SIMPurchase"];
            //        DataView dv = dtSIMPurchase.DefaultView;
            //        dv.RowFilter = "VendorID = " + Convert.ToInt32(ddlNetwork.SelectedValue);
            //        dtSIMPurchase = dv.ToTable();

            //        RepeaterData.DataSource = dtSIMPurchase;
            //        RepeaterData.DataBind();
            //    }
            //    else
            //    {
            //        dtSIMPurchase = (DataTable)ViewState["SIMPurchase"];
            //        RepeaterData.DataSource = dtSIMPurchase;
            //        RepeaterData.DataBind();


            //    }

            //}
            //catch (Exception ex)
            //{

            //    throw ex;
            //}
        }

       
    }
}