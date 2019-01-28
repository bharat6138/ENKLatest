using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Text;

using ENK.ServiceReference1;
using System.Configuration;

namespace ENK
{
    public partial class MainMaster : System.Web.UI.MasterPage
    {
        Service1Client svc = new Service1Client();

        //protected void Page_Init(object sender, EventArgs e)
        //{
        //    if (Request.QueryString.Count > 0)
        //    {
        //        long _logid = 0;
        //        long.TryParse(Convert.ToString(Request.QueryString["lid"]), out _logid);

        //        DataSet ds = new DataSet();
        //        ds = svc.ValidateLoginApp(_logid);

        //        if (ds != null)
        //        {
        //            if (ds.Tables[0].Rows.Count > 0)
        //            {
        //                Session["LoginID"] = Convert.ToString(ds.Tables[0].Rows[0]["ID"]);
        //                Session["ClientType"] = Convert.ToString(ds.Tables[0].Rows[0]["Name"]);
        //                Session["CurrencyName"] = Convert.ToString(ds.Tables[0].Rows[0]["Currency"]);
        //                Session["DistributorID"] = Convert.ToString(ds.Tables[0].Rows[0]["DistributorID"]);
        //            }
        //            else
        //            {
        //                Session["LoginID"] = null;
        //                Session["ClientType"] = null;
        //                Session["CurrencyName"] = null;
        //                Session["DistributorID"] = null;
        //            }
        //        }
        //    }

        //    if (Session["LoginID"] != null)
        //    {
        //        if (Session["ClientType"].ToString() == "Company")
        //        {
        //            menu24.Visible = false;


        //        }
        //        if (Session["ClientType"].ToString() == "Distributor")
        //        {
        //            mnuLoginHistory.Visible = false;
        //            menu3.Visible = false;
        //            menu4.Visible = false;
        //            menu7.Visible = false;
        //            menuSIMTariff.Visible = false;
        //            menu13.Visible = false;
        //            menuSettings.Visible = false;
        //            menu22.Visible = false;
        //            menu19.Visible = false;
        //            menu23.Visible = false;
        //            menu24.Visible = true;
        //            DistributorPlanMappingChange.Visible = false;
        //            menuExcelImport.Visible = false;

        //            NetworkReplace.Visible = false;
        //            mnuDistributorBulkRecharge.Visible = false;
        //            RechargeFailReport.Visible = false;
        //            ActivationFailedReport.Visible = false;
        //        }
        //    }
        //    else
        //    {
        //        Response.Redirect("Login.aspx", false);
        //    }
        //}

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["LoginID"] != null)
                {
                    notification();

                    lblUserName.Text = Convert.ToString(Session["Fname"]) + " " + Convert.ToString(Session["Lname"]);

                    #region "ClientType - Company"
                    if (Session["ClientType"].ToString() == "Company")
                    {
                        int Dist = Convert.ToInt32(Session["DistributorID"]);
                        Distributor[] dst = svc.GetSingleDistributorService(Dist);
                        // lblAccounttext.Visible = false;
                        //lblBalance.Visible = false;
                        lblBalance.Text = Convert.ToString(Session["CurrencyName"]) + " " + Convert.ToString(Convert.ToDecimal(dst[0].balanceAmount));
                        menu7.Visible = false;
                        DistributorPlanMappingChange.Visible = false;
                        mnuDistributorBulkRecharge.Visible = false;
                        NetworkReplace.Visible = false;
                        menu24.Visible = false;

                        Label6.Text = Convert.ToString(ConfigurationManager.AppSettings["COMPANY_SALESREPRESENTATIVE"]);

                        Label7.Text = Convert.ToString(ConfigurationManager.AppSettings["COMPANY_PHONE"]);

                        Label8.Text = Convert.ToString(ConfigurationManager.AppSettings["COMPANY_SALESREPRESENTATIVEEMAIL"]);

                    }
                    #endregion

                    #region "ClientType - Distributor"
                    if (Session["ClientType"].ToString() == "Distributor")
                    {
                        int userId = Convert.ToInt32(Session["LoginID"]);
                        int Dist = Convert.ToInt32(Session["DistributorID"]);
                        Distributor[] dst = svc.GetSingleDistributorService(Dist);
                        //Welcome pannel   
                        lblBalance.Text = Convert.ToString(Session["CurrencyName"]) + " " + Convert.ToString(Convert.ToDecimal(dst[0].balanceAmount));
                        //  lblBalance1.Text = Convert.ToString(dst[0].balanceAmount);
                        DistributorPlanMappingChange.Visible = false;
                        mnuDistributorBulkRecharge.Visible = false;
                        NetworkReplace.Visible = false;
                        menu13.Visible = false;
                        menu3.Visible = false;
                        menu7.Visible = false;
                        //menu89.Visible = false;
                        menu24.Visible = false;
                        menu4.Visible = false;
                        menu14.Visible = false;
                        menuSIMTariff.Visible = false;
                        menu11.Visible = false;
                        divbanner.Visible = false;
                        contactdetails();
                    }
                    #endregion


                    //Apply Role & Rights
                    ApplyRoleRights();

                    #region "Customized Rights"
                    // add by akash starts
                    String ISTopup = Convert.ToString(ConfigurationManager.AppSettings["Fromail"]);
                    if (ISTopup.Contains("eaha") == true && Convert.ToInt32(Session["LoginId"]) == 115)
                    {                        
                        menu2.Visible = false;
                        A2.Visible = false;
                    }
                    // add by akash ends
                    #endregion
                }
                else
                {
                    Response.Redirect("Login.aspx", false);
                }


                #region "Class Active Region"
                string pageName = this.ContentPlaceHolder1.Page.GetType().Name;

                HiddenField hddnActivation = (HiddenField)this.ContentPlaceHolder1.FindControl("hddnActivation");

                if (pageName == "activationsim_aspx")
                {
                    menu8.Attributes.Add("class", "active");
                }
                else
                {
                    menu8.Attributes.Clear();
                }

                if (pageName == "activationportin_aspx")
                {
                    menu9.Attributes.Add("class", "active");
                }
                else
                {
                    menu9.Attributes.Clear();
                }

                if (pageName == "dashboard_aspx")
                {
                    menuDashBoard.Attributes.Add("class", "active");
                }
                else
                {
                    menuDashBoard.Attributes.Clear();
                }

                if (pageName == "distributorview_aspx")
                {
                    menu1.Attributes.Add("class", "active");

                }
                else
                {
                    menu1.Attributes.Clear();
                }

                if (pageName == "topup_aspx")
                {
                    menu2.Attributes.Add("class", "active");
                }
                else
                {
                    menu2.Attributes.Clear();
                }

                if (pageName == "createplans_aspx")
                {
                    menu3.Attributes.Add("class", "active");
                }
                else
                {
                    menu3.Attributes.Clear();
                }

                if (pageName == "purchase_aspx")
                {
                    menu4.Attributes.Add("class", "active");
                }
                else
                {
                    menu4.Attributes.Clear();
                }

                if (pageName == "inventorytransfer_aspx")
                {
                    menu5.Attributes.Add("class", "active");
                }
                else
                {
                    menu5.Attributes.Clear();
                }
                if (pageName == "stocktransferreport_aspx")
                {
                    mnuStockTransferReport.Attributes.Add("class", "active");
                }
                else
                {
                    mnuStockTransferReport.Attributes.Clear();
                }
                if (pageName == "lycarecharge_aspx")
                {
                    Li3.Attributes.Add("class", "active");
                }
                else
                {
                    Li3.Attributes.Clear();

                }

                if (pageName == "userlist_aspx")
                {
                    menu6.Attributes.Add("class", "active");
                }
                else
                {
                    menu6.Attributes.Clear();
                }

                if (pageName == "rolelist_aspx")
                {
                    menu7.Attributes.Add("class", "active");
                }
                else
                {
                    menu7.Attributes.Clear();
                }

                if (pageName == "simreplacement_aspx")
                {
                    menu10.Attributes.Add("class", "active");
                }
                else
                {
                    menu10.Attributes.Clear();
                }

                if (pageName == "pos_aspx")
                {
                    menu12.Attributes.Add("class", "active");
                }
                else
                {
                    menu12.Attributes.Clear();
                }

                if (pageName == "vendorview_aspx")
                {
                    menu13.Attributes.Add("class", "active");
                }
                else
                {
                    menu13.Attributes.Clear();
                }

                if (pageName == "currencyview_aspx")
                {
                    menu14.Attributes.Add("class", "active");
                }
                else
                {
                    menu14.Attributes.Clear();
                }

                if (pageName == "resetpassword_aspx")
                {
                    menu15.Attributes.Add("class", "active");
                }
                else
                {
                    menu15.Attributes.Clear();
                }

                if (pageName == "activationsimreport_aspx")
                {
                    menu16.Attributes.Add("class", "active");
                }
                else
                {
                    menu16.Attributes.Clear();
                }

                if (pageName == "rechargerequestreport_aspx")
                {
                    menu32.Attributes.Add("class", "active");
                }
                else
                {
                    menu32.Attributes.Clear();
                }
                if (pageName == "rechargesimreport_aspx")
                {
                    menu31.Attributes.Add("class", "active");
                }
                else
                {
                    menu31.Attributes.Clear();
                }

                if (pageName == "rechargefailreport_aspx")
                {
                    RechargeFailReport.Attributes.Add("class", "active");
                }
                else
                {
                    RechargeFailReport.Attributes.Clear();
                }
                if (pageName == "activationfailedreport_aspx")
                {
                    ActivationFailedReport.Attributes.Add("class", "active");
                }
                else
                {
                    ActivationFailedReport.Attributes.Clear();
                }
                if (pageName == "salesreportforactivationandportin_aspx")
                {
                    menu17.Attributes.Add("class", "active");
                }
                else
                {
                    menu17.Attributes.Clear();
                }

                if (pageName == "inventorystatusreport_aspx")
                {
                    menu18.Attributes.Add("class", "active");
                }
                else
                {
                    menu18.Attributes.Clear();
                }

                if (pageName == "inventorypurchasereport_aspx")
                {
                    menu19.Attributes.Add("class", "active");
                }
                else
                {
                    menu19.Attributes.Clear();
                }

                if (pageName == "excelimport_aspx")
                {
                    menuExcelImport.Attributes.Add("class", "active");
                }
                else
                {
                    menuExcelImport.Attributes.Clear();
                }
                if (pageName == "loginhistory_aspx")
                {
                    mnuLoginHistory.Attributes.Add("class", "active");
                }
                else
                {
                    mnuLoginHistory.Attributes.Clear();
                }
                if (pageName == "ledgerreport_aspx")
                {
                    mnuLedgerReport.Attributes.Add("class", "active");
                }
                else
                {
                    mnuLedgerReport.Attributes.Clear();
                }
                if (pageName == "topupledger_aspx")
                {
                    mnuTopupLedger.Attributes.Add("class", "active");
                }
                else
                {
                    mnuTopupLedger.Attributes.Clear();
                }

                if (pageName == "deductreport_aspx")
                {
                    mnuDeductReport.Attributes.Add("class", "active");
                }
                else
                {
                    mnuDeductReport.Attributes.Clear();
                }


                if (pageName == "simhistoryreport_aspx")
                {
                    menu20.Attributes.Add("class", "active");
                }
                else
                {
                    menu20.Attributes.Clear();
                }

                if (pageName == "salesreport_aspx")
                {
                    menu21.Attributes.Add("class", "active");
                }
                else
                {
                    menu21.Attributes.Clear();
                }

                if (pageName == "suscriberlog_aspx")
                {
                    menu22.Attributes.Add("class", "active");
                }
                else
                {
                    menu22.Attributes.Clear();
                }



                if (pageName == "simtariffmapping_aspx")
                {
                    menuSIMTariff.Attributes.Add("class", "active");
                }
                else
                {
                    menuSIMTariff.Attributes.Clear();
                }

                if (pageName == "paymentdetail_aspx")
                {
                    menu23.Attributes.Add("class", "active");
                }
                else
                {
                    menu23.Attributes.Clear();
                }

                if (pageName == "networkreplace_aspx")
                {
                    NetworkReplace.Attributes.Add("class", "active");
                }
                else
                {
                    NetworkReplace.Attributes.Clear();
                }
                if (pageName == "rechargebulk_aspx")
                {
                    mnuDistributorBulkRecharge.Attributes.Add("class", "active");
                }
                else
                {
                    mnuDistributorBulkRecharge.Attributes.Clear();
                }




                if (pageName == "createplans_aspx")
                {
                    menu24.Attributes.Add("class", "active");
                }
                else
                {
                    menu24.Attributes.Clear();
                }

                if (pageName == "inventoryaccept_aspx")
                {
                    menu25.Attributes.Add("class", "active");
                }
                else
                {
                    menu25.Attributes.Clear();
                }

                if (pageName == "topupreport_aspx")
                {
                    menu30.Attributes.Add("class", "active");
                }
                else
                {
                    menu30.Attributes.Clear();
                }
                #endregion

            }
            catch (Exception ex)
            {

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

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("Login.aspx");
        }

        protected void linkSim_Click(object sender, EventArgs e)
        {

            Response.Redirect("ActivationSim.aspx", false);
        }


        protected void linkPortin_Click(object sender, EventArgs e)
        {
            Response.Redirect("ActivationPortIn.aspx", false);
        }

        public void notification()
        {
            DataTable dt = new DataTable();
            Int64 distributorID = Convert.ToInt64(Session["DistributorID"]);
            DataSet ds = svc.GetNotification(distributorID, 0);
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    rpt.DataSource = ds.Tables[0];
                    rpt.DataBind();

                }
                else
                {
                    rpt.DataSource = dt;
                    rpt.DataBind();
                }
                if (ds.Tables[2].Rows.Count > 0)
                {
                    lblcount.Text = Convert.ToString(ds.Tables[2].Rows[0]["count"]);
                }
                else
                {
                    lblcount.Text = "0";
                }
            }
            else
            {
                rpt.DataSource = dt;
                rpt.DataBind();
                lblcount.Text = "0";
            }

        }

        //public void ViewNotification()
        //{
        //    DataTable dt = new DataTable();
        //    Int64 distributorID = Convert.ToInt64(Session["DistributorID"]);
        //    DataSet ds = svc.ViewNotification(distributorID, 0);


        //    if (ds.Tables.Count > 0)
        //    {
        //        if (ds.Tables[0].Rows.Count > 0)
        //        {
        //            rpt1.DataSource = ds.Tables[0];
        //            rpt1.DataBind();

        //        }
        //        else
        //        {
        //            rpt1.DataSource = dt;
        //            rpt1.DataBind();
        //        }
        //        if (ds.Tables[2].Rows.Count > 0)
        //        {
        //            lblcount1.Text = Convert.ToString(ds.Tables[2].Rows[0]["count"]);
        //        }
        //        else
        //        {
        //            lblcount1.Text = "0";
        //        }
        //    }
        //    else
        //    {
        //        rpt.DataSource = dt;
        //        rpt.DataBind();
        //        lblcount.Text = "0";
        //    }
        //}

        protected void rpt_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Dismiss")
            {
                int id = Convert.ToInt32(e.CommandArgument);
                Int64 distributorID = Convert.ToInt64(Session["DistributorID"]);
                DataTable dt = new DataTable();
                dt.TableName = "Distributor";
                dt.Columns.Add("DistributorID");
                DataRow dr = dt.NewRow();
                dr["DistributorID"] = "";
                dt.Rows.Add(dr);
                DataSet dsNotification = svc.SaveAndSendNotification(0, distributorID, dt, "", "", "update", id);
                if (dsNotification.Tables[0].Rows.Count > 0)
                {
                    LinkButton localLink = (LinkButton)rpt.FindControl("lnkDismiss");
                    localLink.Enabled = false;
                }
                Response.Redirect("Dashboard.aspx");
            }


        }

        protected void contactdetails()
        {

            DataSet ds = new DataSet();

            ds = svc.FetchContactDetail(Convert.ToInt32(Session["DistributorID"]));
            if (ds.Tables[0].Rows.Count > 0)
            {
                //Label4.Text = Convert.ToString(ds.Tables[0].Rows[0]["ContactNumber"]);
                //Label5.Text = Convert.ToString(ds.Tables[0].Rows[0]["Email"]);

                Label6.Text = Convert.ToString(ds.Tables[0].Rows[0]["ContactPerson"]);
                if (Label6.Text == "")
                {
                    Label6.Text = Convert.ToString(ConfigurationManager.AppSettings["COMPANY_SALESREPRESENTATIVE"]);
                }
                Label7.Text = Convert.ToString(ds.Tables[0].Rows[0]["ContactNumber"]);
                if (Label7.Text == "")
                {
                    Label7.Text = Convert.ToString(ConfigurationManager.AppSettings["COMPANY_PHONE"]);
                }
                Label8.Text = Convert.ToString(ds.Tables[0].Rows[0]["Email"]);
                if (Label8.Text == "")
                {
                    Label8.Text = Convert.ToString(ConfigurationManager.AppSettings["COMPANY_SALESREPRESENTATIVEEMAIL"]);
                }
            }


        }

        /// <summary>
        /// <Method>GetScreenRights</Method>
        /// <Author>Uday</Author>
        /// <Created Date>22-Jan-2019</created>
        /// <Params>Either RoleID or LoginID of Logged-In User.</Params>
        /// <Desc></Desc>
        /// </summary>
        /// <returns>Returns Datatable with all active screens and their rights according to the RoleID of Logged-In user.</returns>
        public DataTable GetScreenRights()
        {
            DataTable dt = new DataTable();
            try
            {
                //Session["LoginId"]
                if (Session["RoleID"] != null)
                {
                    int _roleid = 0;
                    int.TryParse(Convert.ToString(Session["RoleID"]), out _roleid);

                    dt = svc.GetRolewiseScreen(0, _roleid);
                }

                return dt;
            }
            catch (Exception)
            {
                return dt;
            }
        }


        /// <summary>
        /// <Method>ApplyRoleRights</Method>
        /// <Author>Uday</Author>
        /// <Created Date>22-Jan-2019</created>
        /// <Desc>Make Screens visible true/false on the basis of data we get from "GetScreenRights" method.</Desc>
        /// </summary>
        /// <returns></returns>
        public void ApplyRoleRights()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = GetScreenRights();

                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            if (Convert.ToBoolean(dt.Rows[i]["sView"]) == true)
                            {
                                if (Convert.ToString(dt.Rows[i]["ScreenName"]) == "AddFunds")
                                {
                                    menu2.Visible = true;
                                }
                                else if (Convert.ToString(dt.Rows[i]["ScreenName"]) == "SimSwap")
                                {
                                    menu10.Visible = true;
                                }
                                else if (Convert.ToString(dt.Rows[i]["ScreenName"]) == "CheckPortInStatus")
                                {
                                    menu9.Visible = true;
                                }
                                else if (Convert.ToString(dt.Rows[i]["ScreenName"]) == "H2ORecharge")
                                {
                                    Li8.Visible = true;
                                }
                                else if (Convert.ToString(dt.Rows[i]["ScreenName"]) == "Recharge")
                                {
                                    Li3.Visible = true;
                                }
                                else if (Convert.ToString(dt.Rows[i]["ScreenName"]) == "Activate")
                                {
                                    menu8.Visible = true;
                                }
                                else if (Convert.ToString(dt.Rows[i]["ScreenName"]) == "Activate Preloaded Sim")
                                {
                                    A1.Visible = true;
                                }
                            }
                            else
                            {
                                if (Convert.ToString(dt.Rows[i]["ScreenName"]) == "AddFunds")
                                {
                                    menu2.Visible = false;
                                }
                                else if (Convert.ToString(dt.Rows[i]["ScreenName"]) == "SimSwap")
                                {
                                    menu10.Visible = false;
                                }
                                else if (Convert.ToString(dt.Rows[i]["ScreenName"]) == "CheckPortInStatus")
                                {
                                    menu9.Visible = false;
                                }
                                else if (Convert.ToString(dt.Rows[i]["ScreenName"]) == "H2ORecharge")
                                {
                                    Li8.Visible = false;
                                }
                                else if (Convert.ToString(dt.Rows[i]["ScreenName"]) == "Recharge")
                                {
                                    Li3.Visible = false;
                                }
                                else if (Convert.ToString(dt.Rows[i]["ScreenName"]) == "Activate")
                                {
                                    menu8.Visible = false;
                                }
                                else if (Convert.ToString(dt.Rows[i]["ScreenName"]) == "Activate Preloaded Sim")
                                {
                                    A1.Visible = false;
                                }
                            }
                        }
                    }
                }

            }
            catch (Exception)
            {                
                
            }
        }
    }

}