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

namespace ENK
{
    public partial class Dashboard : System.Web.UI.Page
    {
        Service1Client svc = new Service1Client();
        string dir = "~/Image";

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                if (!Page.IsPostBack)
                {
                    if (Session["LoginID"] != null)
                    {
                        int NetworkID = 13;
                        FillSimDetail(NetworkID);
                        CountActivationPreloaded(NetworkID);
                        // FillAddressAccountActivity();                            
                        int ImgFlag = 2;
                        // Session["ImgFlag"] = Convert.ToInt32(Request.QueryString["ImgFlag"]);
                        getImage1FromDB(ImgFlag);
                    }

                    DataSet ds = svc.GetInventoryForAcceptOnDasboard(Convert.ToInt32(Session["DistributorID"]));

                    if (ds != null)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            lblPendingInventory.Visible = true;
                            lblDistributorforAcceptInventory.Visible = true;
                            lbAcceptInventory.Visible = true;

                            string selectValues = string.Empty; // "[";
                            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                            {
                                selectValues += ds.Tables[0].Rows[i]["DistributorName"] + ", ";
                            }

                            selectValues = selectValues.Substring(0, selectValues.Length - 1);

                            lblDistributorforAcceptInventory.Text = selectValues;
                        }
                        else
                        {
                            lblPendingInventory.Visible = false;
                            lblDistributorforAcceptInventory.Visible = false;
                            lbAcceptInventory.Visible = false;
                        }

                    }
                    else
                    {
                        lblPendingInventory.Visible = false;
                        lblDistributorforAcceptInventory.Visible = false;
                        lbAcceptInventory.Visible = false;
                    }

                }
            }
            catch (Exception ex)
            {

            }
        }

        public void FillAddressAccountActivity()
        {
            try
            {
                int userId = Convert.ToInt32(Session["LoginID"]);
                int Dist = Convert.ToInt32(Session["DistributorID"]);

                Distributor[] dst = svc.GetSingleDistributorService(Dist);
                DataSet ds = svc.GetUserService(userId, userId);
                if (dst != null)
                {
                    if (dst.Length > 0)
                    {
                        ////Blue address Panel
                        //lblDistributorName.Text = dst[0].distributorName;
                        //lblAddress1.Text = dst[0].address;
                        //lblAddress2.Text = dst[0].city + " " + dst[0].state;
                        ////lblEmail.Text = dst[0].emailID;
                        //lblPhoneNumber.Text = dst[0].contactNo;
                    }
                }

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        //Welcome pannel   
                        //lblUserName.Text = Convert.ToString(ds.Tables[0].Rows[0]["Fname"]) + " " + Convert.ToString(ds.Tables[0].Rows[0]["Lname"]);
                        //lblWelcome.Visible = false;
                        //lblUserName.Visible = false;
                    }
                    if (ds.Tables[1].Rows.Count > 0)
                    {

                        //Red Pannel
                        lblIP.Text = "IP " + Convert.ToString(ds.Tables[1].Rows[0]["IpAddress1"]);
                        lblBrowser.Text = "Browser " + Convert.ToString(ds.Tables[1].Rows[0]["Browser"]);
                        DateTime dt = Convert.ToDateTime(ds.Tables[1].Rows[0]["LoginTime"]);

                        DateTime dtcurrent = DateTime.UtcNow.AddMinutes(330);
                        TimeSpan ts = dtcurrent.Subtract(dt);

                        double dy = ts.TotalDays;
                        double dh = ts.TotalHours;
                        double dm = ts.TotalMinutes;
                        double dsecnd = ts.TotalSeconds;

                        if (dy > 1)
                        {
                            string day = dy.ToString();
                            day = day.Substring(0, day.IndexOf("."));
                            lblLastLogin1.Text = "Last " + day + " Days Ago";
                        }
                        else
                        {
                            if (dh > 1)
                            {
                                string hour = dh.ToString();
                                hour = hour.Substring(0, hour.IndexOf("."));
                                lblLastLogin1.Text = "Last " + hour + " Hour Ago";
                            }
                            else
                            {
                                if (dm > 1)
                                {
                                    string minute = dm.ToString();
                                    minute = minute.Substring(0, minute.IndexOf("."));
                                    lblLastLogin1.Text = "Last " + minute + " Minute Ago";
                                }
                                else
                                {
                                    if (dsecnd > 1)
                                    {
                                        string second = dsecnd.ToString();
                                        second = second.Substring(0, second.IndexOf("."));
                                        lblLastLogin1.Text = "Last " + second + " Second Ago";
                                    }
                                    else
                                    {
                                        lblLastLogin1.Text = "Last " + "1" + " Second Ago";
                                    }
                                }
                            }
                        }

                        lblLastLogin2.Visible = false;
                        // lblLastLogin2.Text = "Ago";
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        public void FillSimDetail(int NetworkID)
        {
            try
            {
                int clienttypeid = Convert.ToInt32(Session["ClientTypeID"]);
                int Dist = Convert.ToInt32(Session["DistributorID"]);

                DataSet ds = svc.ShowDashBoardDataService(Dist, clienttypeid, NetworkID);
                if (ds != null)
                {
                    //if (ds.Tables[0].Rows.Count > 0)
                    //{
                    //    lblActivation.Text = Convert.ToString(ds.Tables[0].Rows[0][0]);
                    //}
                    //else
                    //{

                    //    lblActivation.Text = "0";
                    //}
                    //if (ds.Tables[1].Rows.Count > 0)
                    //{
                    //    lblMobileSim.Text = Convert.ToString(ds.Tables[1].Rows[0][0]);
                    //}
                    //else
                    //{
                    //    lblMobileSim.Text = "";
                    //}
                    //if (ds.Tables[2].Rows.Count > 0)
                    //{
                    //    lblBlankSIm.Text = Convert.ToString(ds.Tables[2].Rows[0][0]);
                    //}
                    //else
                    //{
                    //    lblBlankSIm.Text = "";
                    //}

                    int month = DateTime.Now.Month;
                    // month = month - 1;
                    DateTime dtDate = new DateTime(DateTime.Now.Year, month, 1);
                    string sMonthFullName = dtDate.ToString("MMMM");
                    lblMonth.Text = Convert.ToString(sMonthFullName);

                    if (ds.Tables[3].Rows.Count > 0)
                    {
                        lblCommissionAmount.Text = Convert.ToString(ds.Tables[3].Rows[0][0]);
                    }
                    else
                    {
                        lblCommissionAmount.Text = "0";
                    }
                }
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

        protected void lbAcceptInventory_Click(object sender, EventArgs e)
        {
            Response.Redirect("InventoryAccept.aspx");
        }


        private void getImage1FromDB(int ImgFlag)
        {
            try
            {
                DataSet dst = svc.getImage1FromDB(ImgFlag);
                if (dst != null)
                {
                    if (dst.Tables[0].Rows.Count > 0)
                    {
                        if (dst.Tables[0].Rows[0]["Url"] != "")
                        {
                            string Photo1 = Convert.ToString(dst.Tables[0].Rows[0]["Url"]);
                            Img1.ImageUrl = dir + "/" + Photo1;
                            DivImg1.Visible = true;
                        }


                        if (dst.Tables[0].Rows.Count > 1)
                        {
                            if (dst.Tables[0].Rows[1]["Url"] != "")
                            {

                                string Photo2 = Convert.ToString(dst.Tables[0].Rows[1]["Url"]);
                                Img2.ImageUrl = dir + "/" + Photo2;
                                DivImg2.Visible = true;
                            }
                        }

                        if (dst.Tables[0].Rows.Count > 2)
                        {
                            if (dst.Tables[0].Rows[2]["Url"] != "")
                            {
                                string Photo3 = Convert.ToString(dst.Tables[0].Rows[2]["Url"]);
                                Img3.ImageUrl = dir + "/" + Photo3;
                                DivImg3.Visible = true;
                            }
                        }

                        if (dst.Tables[0].Rows.Count > 3)
                        {
                            if (dst.Tables[0].Rows[3]["Url"] != "")
                            {
                                string Photo4 = Convert.ToString(dst.Tables[0].Rows[3]["Url"]);
                                Img4.ImageUrl = dir + "/" + Photo4;
                                DivImg4.Visible = true;
                            }
                        }
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

        public void CountActivationPreloaded(int NetworkID)
        {
            try
            {

                int distr = Convert.ToInt32(Session["DistributorID"]);
                int clnt = Convert.ToInt32(Session["ClientTypeID"]);
                int login = Convert.ToInt32(Session["LoginID"]);
                int ActivationMonth = 0;
                int ActivationMonthLast = 0;
                int ActivationPreloadedMonth = 0;
                int ActivationPreloadedMonthLAST = 0;
                int BlankSim = 0;
                int BlankPreloadedSim = 0;
                //pCountSimActivationForDashBoard
                DataSet ds = svc.CountActivationPreloaded(distr, clnt, login, 0, 0, NetworkID);
                if (ds != null)
                {
                    if (ds.Tables[0] != null)
                    {
                        //Current Month
                        ActivationMonth = Convert.ToInt32(ds.Tables[0].Rows[0]["TotalActivationMSISDN"]);
                        lblActivationMonth.Text = ds.Tables[0].Rows[0]["TotalActivationMSISDN"].ToString();

                    }
                    if (ds.Tables[1] != null)
                    {
                        //Current Month
                        ActivationPreloadedMonth = Convert.ToInt32(ds.Tables[1].Rows[0]["TotalActivationPreloadedMSISDN"]);
                        lblactivationPreloadedMonth.Text = ds.Tables[1].Rows[0]["TotalActivationPreloadedMSISDN"].ToString();

                    }
                    //Current Month
                    lblActivation.Text = Convert.ToString(ActivationMonth + ActivationPreloadedMonth);

                    if (ds.Tables[2] != null)
                    {
                        lblBlankNonPreloadedSim.Text = ds.Tables[2].Rows[0]["BlankNonPreloadedSIM"].ToString();
                        BlankSim = Convert.ToInt32(ds.Tables[2].Rows[0]["BlankNonPreloadedSIM"]);
                    }
                    if (ds.Tables[3] != null)
                    {
                        lblBlankPreloadedSim.Text = ds.Tables[3].Rows[0]["BlankPreloadedSIM"].ToString();
                        BlankPreloadedSim = Convert.ToInt32(ds.Tables[3].Rows[0]["BlankPreloadedSIM"]);
                    }
                    lblBlankSIm.Text = Convert.ToString(BlankSim + BlankPreloadedSim);
                    //for last Month
                    if (ds.Tables[4] != null)
                    { 
                        ActivationMonthLast = Convert.ToInt32(ds.Tables[4].Rows[0]["TotalActivationMSISDNLAST"]);
                        lblActivationMonthLast.Text = ds.Tables[4].Rows[0]["TotalActivationMSISDNLAST"].ToString();
                    }
                    //for last Month 
                    if (ds.Tables[5] != null)
                    {
                        ActivationPreloadedMonthLAST = Convert.ToInt32(ds.Tables[5].Rows[0]["TotalActivationPreloadedMSISDNLAST"]);
                        lblactivationPreloadedMonthLAST.Text = ds.Tables[5].Rows[0]["TotalActivationPreloadedMSISDNLAST"].ToString();
                    }

                    //for last Month
                    lblActivationLAST.Text = Convert.ToString(ActivationMonthLast + ActivationPreloadedMonthLAST);
                    
                }
            }
            catch (Exception ex)
            {

            }
        }


        protected void switch_left_CheckedChanged(object sender, EventArgs e)
        {
            int NetworkID = 13;
            if (switch_left.Checked == true)
            {
                NetworkID = 13;
            }
            else
            {
                NetworkID = 15;
            }
            FillSimDetail(NetworkID);
            CountActivationPreloaded(NetworkID);
        }

        protected void switch_right_CheckedChanged(object sender, EventArgs e)
        {
            int NetworkID = 13;
            if (switch_left.Checked == true)
            {
                NetworkID = 13;
            }
            else
            {
                NetworkID = 15;

            }
            FillSimDetail(NetworkID);
            CountActivationPreloaded(NetworkID);
        }


    }
}