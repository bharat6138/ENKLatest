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
using System.Net.Mail;
using System.Web.UI.HtmlControls;
using System.Web.Services;

namespace ENK
{
    public partial class TempleteViewMaster : System.Web.UI.Page
    {
        Service1Client ssc = new Service1Client();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
               // FillDistributor();

                DisableDeactivateButton();

                // add by akash starts
                int distributorID = Convert.ToInt32(Session["DistributorID"]);
                AdjustLevel(distributorID);
                // add by akash end


                sparks.Visible = false;
                DataSet ds = ssc.GetSingleDistributorTariffService(Convert.ToInt32(Session["DistributorID"]));
                if (ds.Tables[6].Rows.Count > 0)
                {
                    int sellerCheck = Convert.ToInt32(ds.Tables[6].Rows[0]["SellerCreationRight"]);
                    if (sellerCheck == 1)
                    {
                        sparks.Visible = true;
                    }
                }

            }
        }

        public void ShowBlankGrid()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("ACode");
            dt.Columns.Add("Name");
            dt.Columns.Add("ClientType");
            dt.Columns.Add("ContactPerson");
            dt.Columns.Add("ContactNumber");
            dt.Columns.Add("Email");
            dt.Columns.Add("Website");
            dt.Columns.Add("ParentDistributorID");
            dt.Columns.Add("ServiceTaxNumber");
            dt.Columns.Add("ServiceTaxPercentage");
            dt.Columns.Add("VATNumber");
            dt.Columns.Add("VATPercentage");
            dt.Columns.Add("CreatedDtTm");
            dt.Columns.Add("ModifiedBy");
            dt.Columns.Add("ModifiedDtTm");
            dt.Columns.Add("isServiceTaxExempted");
            dt.Columns.Add("AccountBalance");
            dt.Columns.Add("IsActive");

            DataRow dr;
            dr = dt.NewRow();
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                dr[i] = "";
            }
            dt.Rows.Add(dr);
            dt.AcceptChanges();
            RepeaterDistributorView.DataSource = dt;
            RepeaterDistributorView.DataBind();
        }

        //public void FillDistributor()
        //{
        //    try
        //    {
        //        int loginid = Convert.ToInt32(Session["LoginID"]);
        //        int distributorID = Convert.ToInt32(Session["DistributorID"]);
        //        Distributor[] dis = ssc.GetDistributorServiceWithDate(loginid, distributorID, "0", "0", "", null, null);
        //        if (dis != null)
        //        {
        //            RepeaterDistributorView.DataSource = dis;
        //            RepeaterDistributorView.DataBind();
        //        }
        //        else
        //        {
        //            RepeaterDistributorView.DataBind();
        //            //ShowBlankGrid();
        //        }


        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public void DisableDeactivateButton()
        {
            if (Convert.ToString(Session["ClientType"]) == "Company")
            {
                for (int i = 0; i < RepeaterDistributorView.Items.Count; i++)
                {
                    ImageButton lnkDeactivate = (ImageButton)RepeaterDistributorView.Items[i].FindControl("lnkDeactivate");
                    HiddenField hdnDistributortId = (HiddenField)RepeaterDistributorView.Items[i].FindControl("hdnDistributortId");
                    if (hdnDistributortId.Value == "1")
                    {
                        lnkDeactivate.Visible = false;
                    }
                }
            }
            else
            {
                for (int i = 0; i < RepeaterDistributorView.Items.Count; i++)
                {
                    ImageButton lnkDeactivate = (ImageButton)RepeaterDistributorView.Items[i].FindControl("lnkDeactivate");
                    lnkDeactivate.Visible = false;

                    HiddenField hdnDistributortId = (HiddenField)RepeaterDistributorView.Items[i].FindControl("hdnDistributortId");
                    if (hdnDistributortId.Value == "1")
                    {
                        lnkDeactivate.Visible = false;
                    }
                }
            }

            for (int i = 0; i < RepeaterDistributorView.Items.Count; i++)
            {
                HiddenField hdnDistributortId = (HiddenField)RepeaterDistributorView.Items[i].FindControl("hdnDistributortId");
                HiddenField hddnholdstatus = (HiddenField)RepeaterDistributorView.Items[i].FindControl("hiddeholdstatus");
                string s = hddnholdstatus.Value;
                if (hddnholdstatus.Value == "False" || hddnholdstatus.Value == null || hddnholdstatus.Value == "")
                {
                    Button btnhold = (Button)RepeaterDistributorView.Items[i].FindControl("btnHoldStatus");
                    btnhold.Text = "Hold";
                }
                else
                {
                    Button btnhold = (Button)RepeaterDistributorView.Items[i].FindControl("btnHoldStatus");
                    btnhold.Text = "Unhold";
                }

            }

            for (int i = 0; i < RepeaterDistributorView.Items.Count; i++)
            {
                if (RepeaterDistributorView.Items.Count <= 1)
                {

                    Button btnhold = (Button)RepeaterDistributorView.Items[i].FindControl("btnHoldStatus");
                    btnhold.Visible = false;

                }

                else
                {
                    HiddenField hdnDistributortId = (HiddenField)RepeaterDistributorView.Items[i].FindControl("hdnDistributortId");
                    if (Convert.ToString(hdnDistributortId.Value) == Convert.ToString(Session["DistributorID"]))
                    {
                        Button btnhold = (Button)RepeaterDistributorView.Items[i].FindControl("btnHoldStatus");
                        btnhold.Enabled = false;
                    }
                }
            }

            //for (int k = 0; k < RepeaterDistributorView.Items.Count; k++)
            //{
            //    HiddenField hdnDocFile = (HiddenField)RepeaterDistributorView.Items[k].FindControl("hiddenDocFile");

            //    if (hdnDocFile.Value == "")
            //    {
            //       // HtmlAnchor adocfile = (HtmlAnchor)RepeaterDistributorView.Items[k].FindControl("ViewDocFile");
            //        HyperLink hplink = (HyperLink)RepeaterDistributorView.Items[k].FindControl("hlFileName");

            //      //  adocfile.Disabled = true;
            //        hplink.Enabled = false;
            //    }
            //    //else
            //    //{
            //    //     HtmlAnchor adocfile = (HtmlAnchor)RepeaterDistributorView.Items[k].FindControl("ViewDocFile");
            //    //     adocfile.Attributes["href"] = "https://docs.google.com/viewerng/viewer?url=" + hdnDocFile.Value;
            //    //}
            //}



        }

        protected void RepeaterDistributorView_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            try
            {



                if (e.CommandName == "View")
                {
                    int id = Convert.ToInt32(e.CommandArgument);
                    string idnty = Encryption.Encrypt(id.ToString());
                    string condition = Encryption.Encrypt("View");
                    Response.Redirect("~/AddDistributor.aspx?Identity=" + idnty + "&Condition=" + condition, false);
                }
                if (e.CommandName == "RowEdit")
                {
                    int id = Convert.ToInt32(e.CommandArgument);
                    //hdnDistributortId.Value = id.ToString();
                    if (Convert.ToInt32(Session["DistributorID"]) == id && Convert.ToString(Session["ClientType"]) != "Company")
                    {

                    }
                    else
                    {

                        string idnty = Encryption.Encrypt(id.ToString());
                        string condition = Encryption.Encrypt("Edit");
                        Response.Redirect("~/AddDistributor.aspx?Identity=" + idnty + "&Condition=" + condition, false);
                    }
                }
                if (e.CommandName == "RowDelete")
                {
                    int id = Convert.ToInt32(e.CommandArgument);
                    string idnty = Encryption.Encrypt(id.ToString());
                    hddnDistributorId.Value = id.ToString();

                    int loginid = Convert.ToInt32(Session["LoginID"]);
                    int distributorID = Convert.ToInt32(Session["DistributorID"]);
                    DataSet ds = ssc.DeactivateDistirbutorService(id, loginid, "1");
                    if (ds != null)
                    {
                        string ss = Convert.ToString(ds.Tables[0].Rows[0]["ResponseCode"]);
                        if (ss == "0")
                        {
                            ShowPopUpMsg("Distributor Deactivated Successfully");
                            FillDistributor();
                        }
                        if (ss == "1")
                        {
                            rptDist.DataSource = ds.Tables[1];
                            rptDist.DataBind();
                            ScriptManager.RegisterStartupScript(this, GetType(), "", "ShowProgress();", true);
                        }
                    }
                }

                if (e.CommandName == "ApiStatus")
                {
                    int id = Convert.ToInt32(e.CommandArgument);
                    //hdnDistributortId.Value = id.ToString();
                    if (Convert.ToInt32(Session["DistributorID"]) == id && Convert.ToString(Session["ClientType"]) != "Company")
                    {

                    }
                    else
                    {

                        string idnty = Encryption.Encrypt(id.ToString());
                        string condition = Encryption.Encrypt("Edit");
                        Response.Redirect("~/ApiMapping.aspx?Identity=" + idnty + "&Condition=" + condition, false);
                        //Response.Redirect("~/AddDistributor.aspx?Identity=" + idnty + "&Condition=" + condition, false);
                    }
                }


                if (e.CommandName == "IpAddress")
                {
                    int id = Convert.ToInt32(e.CommandArgument);
                    //hdnDistributortId.Value = id.ToString();
                    if (Convert.ToInt32(Session["DistributorID"]) == id && Convert.ToString(Session["ClientType"]) != "Company")
                    {

                    }
                    else
                    {

                        string idnty = Encryption.Encrypt(id.ToString());
                        string condition = Encryption.Encrypt("Edit");
                        Response.Redirect("~/IpMapping.aspx?Identity=" + idnty + "&Condition=" + condition, false);
                    }
                }


                if (e.CommandName == "ViewDocument")
                {

                    HiddenField hdnDocFile = (HiddenField)e.Item.FindControl("hiddenDocFile");
                    if (hdnDocFile.Value != "")
                    {
                        LinkButton hplink = (LinkButton)e.Item.FindControl("hpLinkView");
                        if (hdnDocFile.Value.EndsWith(".png") || hdnDocFile.Value.EndsWith(".jpg") || hdnDocFile.Value.EndsWith(".jpeg")|| hdnDocFile.Value.EndsWith(".PNG") || hdnDocFile.Value.EndsWith(".JPG") || hdnDocFile.Value.EndsWith(".JPEG"))
                        {
                            Response.Redirect(hdnDocFile.Value.Trim());
                        }

                        else
                        {
                            string link = "https://docs.google.com/viewerng/viewer?url=" + hdnDocFile.Value.Trim();
                            Response.Redirect(link);
                        }
                    }
                    else
                    {
                        ShowPopUpMsg("This distributor hasn't uploaded tax document");
                    }

                }



                if (e.CommandName == "ViewCertificate")
                {

                    HiddenField hdnCertiFile = (HiddenField)e.Item.FindControl("hiddenCertifile");
                    if (hdnCertiFile.Value != "")
                    {
                        LinkButton LbtnCerti = (LinkButton)e.Item.FindControl("LbtnCertificate");
                        if (hdnCertiFile.Value.EndsWith(".png") || hdnCertiFile.Value.EndsWith(".jpg") || hdnCertiFile.Value.EndsWith(".jpeg")|| hdnCertiFile.Value.EndsWith(".PNG") || hdnCertiFile.Value.EndsWith(".JPG") || hdnCertiFile.Value.EndsWith(".JPEG"))
                        {
                            Response.Redirect(hdnCertiFile.Value.Trim());
                        }

                        else
                        {
                            string link = "https://docs.google.com/viewerng/viewer?url=" + hdnCertiFile.Value.Trim();
                            Response.Redirect(link);
                        }
                    }
                    else
                    {
                        ShowPopUpMsg("This distributor hasn't uploaded Certificate document");
                    }

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        protected void onclick_pwd(object sender, EventArgs e)
        {
        }

        public void SendMail(string SendTo, string Subject, string UserName, string UserID, string pass, string LoginUrl)
        {
            try
            {
                string LogoUrl = "https://" + HttpContext.Current.Request.Url.Host + HttpContext.Current.Request.ApplicationPath + "/img/logo.png";
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient();


                string MailAddress = ConfigurationManager.AppSettings.Get("Fromail");
                string PassWord = ConfigurationManager.AppSettings.Get("Password");

                mail.From = new MailAddress(MailAddress);
                mail.To.Add(SendTo);
                TimeSpan ts = new TimeSpan(8, 0, 0);
                mail.Subject = Subject + " " + DateTime.UtcNow.Subtract(ts).ToString();

                StringBuilder sb = new StringBuilder();
                sb.Append("<html>");
                sb.Append("<body style=”color:grey; font-size:15px;”>");
                sb.Append("<font face=”Helvetica, Arial, sans-serif”>");

                sb.Append("<div style=”position:absolute; height:200px; width:100px; background-color:0d1d36; padding:30px;”>");
                sb.Append("<img src=" + LogoUrl + " />");
                sb.Append("</div>");

                sb.Append("<br/>");

                sb.Append("<br/>");

                sb.Append("<div style=”background-color: #ece8d4; width:600px; height:200px; padding:30px; margin-top:30px;”>");
                //sb.Append("<p>Please find the new credentials and get started.</p>");

                sb.Append("<p>Dear " + UserName + ",<p>");

                sb.Append("<p>Please find the new credentials and get started.</p>");
                sb.Append("<p>");
                sb.Append("<br/>");

                sb.Append("Username: " + UserID + "<br>");
                sb.Append("Password: " + pass + "<br>");
                sb.Append("<br/>");
                sb.Append("<p>Your password is secure show don't share it</p>");

                sb.Append("<p>Use this link for login</p>");
                sb.Append("<p>" + LoginUrl + "</p>");
                sb.Append("<br/>");
                sb.Append("<p>Sincerely,");
                sb.Append("<p>" + ConfigurationManager.AppSettings.Get("COMPANY_NAME") + "</p>");
                sb.Append("<p>Thank you</p>");
                sb.Append("<p>----------------------------------------------------------------");
                sb.Append("<p>PROTECT YOUR PASSWORD");

                sb.Append("<p>NEVER give your password to anyone, including " + ConfigurationManager.AppSettings.Get("SHORT_COMPANY_NAME") + " employees. ");
                sb.Append("Protect yourself against fraudulent websites by opening a new web browser ");
                sb.Append("(e.g. Internet Explorer or Firefox) and typing in the " + ConfigurationManager.AppSettings.Get("SHORT_COMPANY_NAME") + " URL every time you log in to your account.");
                sb.Append("<p>----------------------------------------------------------------");
                sb.Append("<p>Please do not reply to this email. This mailbox is not monitored and you will not receive a response. ");

                sb.Append("<br/>");
                sb.Append("</div>");
                sb.Append("</body>");
                sb.Append("</html>");

                mail.Body = sb.ToString();
                mail.BodyEncoding = System.Text.Encoding.UTF8;
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.High;

                SmtpServer.Host = ConfigurationManager.AppSettings.Get("Host");
                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential(MailAddress, PassWord);
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);

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

        private void ShowConfirmPopUpMsg(string msg)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("alert('");
            sb.Append(msg.Replace("\n", "\\n").Replace("\r", "").Replace("'", "\\'"));
            sb.Append("');");
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "confirm", sb.ToString(), true);
        }

        protected void linkDeactivate_Click(object sender, EventArgs e)
        {
            int loginid = Convert.ToInt32(Session["LoginID"]);
            int distributorID = Convert.ToInt32(hddnDistributorId.Value);
            DataSet ds = ssc.DeactivateDistirbutorService(distributorID, loginid, "2");
            if (ds != null)
            {
                string ss = Convert.ToString(ds.Tables[0].Rows[0]["ResponseCode"]);
                if (ss == "2")
                {
                    ShowPopUpMsg("Distributor Deactivated Successfully");
                }
                else
                {
                    ShowPopUpMsg("Distributor Deactivated Fail");
                }
                FillDistributor();
            }
        }

        protected void linkBack_Click(object sender, EventArgs e)
        {
            FillDistributor();
        }

        //protected void btnExportToExcel_Click(object sender, EventArgs e)
        //{
        //    int loginid = Convert.ToInt32(Session["LoginID"]);
        //    int distributorID = Convert.ToInt32(Session["DistributorID"]);
        //    DataTable dtt = new DataTable();
        //    dtt.TableName = "Export";
        //    dtt.Columns.Add("distributorName");
        //    dtt.Columns.Add("contactPerson");
        //    dtt.Columns.Add("contactNo");
        //    dtt.Columns.Add("CompanyTypeName");
        //    dtt.Columns.Add("emailID");
        //    dtt.Columns.Add("Address");
        //    dtt.Columns.Add("City");
        //    dtt.Columns.Add("Zip");
        //    dtt.Columns.Add("State");
        //    dtt.Columns.Add("Country");
        //    dtt.Columns.Add("webSiteName");
        //    dtt.Columns.Add("taxId");
        //    dtt.Columns.Add("balanceAmount");
        //    dtt.Columns.Add("parentDistributor");
        //    dtt.Columns.Add("NoOfBlankSims");
        //    dtt.Columns.Add("NoOfActivationsMTD");
        //    dtt.Columns.Add("Created DateTime");
        //    dtt.Columns.Add("Modified DateTime");
        //    DataSet dset = ssc.GetDistributor1(loginid, distributorID);
        //    if (dset.Tables[0].Rows.Count > 0)
        //    {
        //        for (int i = 0; i < dset.Tables[0].Rows.Count; i++)
        //        {
        //            //Label distributorName = (Label)RepeaterDistributorView.Items[i].FindControl("distributorName");
        //            //Label contactNo = (Label)RepeaterDistributorView.Items[i].FindControl("contactNo");
        //            //Label CompanyTypeName = (Label)RepeaterDistributorView.Items[i].FindControl("CompanyTypeName");
        //            //Label emailID = (Label)RepeaterDistributorView.Items[i].FindControl("emailID");
        //            //Label webSiteName = (Label)RepeaterDistributorView.Items[i].FindControl("webSiteName");
        //            //Label balanceAmount = (Label)RepeaterDistributorView.Items[i].FindControl("balanceAmount");
        //            //Label parentDistributor = (Label)RepeaterDistributorView.Items[i].FindControl("parentDistributor");

        //            //DataRow dr =  dtt.NewRow();

        //            //dr["distributorName"] = distributorName.Text;
        //            //dr["contactNo"] = contactNo.Text;
        //            //dr["CompanyTypeName"] = CompanyTypeName.Text;
        //            //dr["emailID"] = emailID.Text;
        //            //dr["webSiteName"] = webSiteName.Text;
        //            //dr["balanceAmount"] = balanceAmount.Text;
        //            //dr["parentDistributor"] = parentDistributor.Text;

        //            //dtt.Rows.Add(dr);
        //            //dtt.AcceptChanges();
        //            DataRow dr = dtt.NewRow();

        //            dr["distributorName"] = dset.Tables[0].Rows[i]["Name"].ToString();
        //            dr["contactNo"] = dset.Tables[0].Rows[i]["ContactNumber"].ToString();
        //            dr["contactPerson"] = dset.Tables[0].Rows[i]["ContactPerson"].ToString();
        //            dr["CompanyTypeName"] = dset.Tables[0].Rows[i]["ClientTypeName"].ToString();
        //            dr["emailID"] = dset.Tables[0].Rows[i]["Email"].ToString();
        //            dr["Address"] = dset.Tables[0].Rows[i]["AddressLine"].ToString();
        //            dr["City"] = dset.Tables[0].Rows[i]["City"].ToString();
        //            dr["Zip"] = dset.Tables[0].Rows[i]["Zip"].ToString();
        //            dr["State"] = dset.Tables[0].Rows[i]["State"].ToString();
        //            dr["Country"] = dset.Tables[0].Rows[i]["CountryName"].ToString();
        //            dr["webSiteName"] = dset.Tables[0].Rows[i]["WebSite"].ToString();
        //            dr["taxId"] = dset.Tables[0].Rows[i]["EIN"].ToString();
        //            dr["balanceAmount"] = dset.Tables[0].Rows[i]["AccountBalance"].ToString();
        //            dr["parentDistributor"] = dset.Tables[0].Rows[i]["PName"].ToString();
        //            dr["NoOfBlankSims"] = dset.Tables[0].Rows[i]["NoOfBlankSim"].ToString();
        //            dr["NoOfActivationsMTD"] = dset.Tables[0].Rows[i]["NoOfActivation"].ToString();
        //            dr["Created DateTime"] = dset.Tables[0].Rows[i]["CreatedDtTm"].ToString();
        //            dr["Modified DateTime"] = dset.Tables[0].Rows[i]["ModifiedDtTm"].ToString();
        //            dtt.Rows.Add(dr);
        //            dtt.AcceptChanges();

        //        }
        //    }

        //    if (dtt.Rows.Count > 0)
        //    {
        //        DataView view = new DataView(dtt);

        //        DataTable dtExcel = view.ToTable("Selected", false, "distributorName", "contactNo", "contactPerson", "CompanyTypeName", "emailID", "Address", "City", "Zip", "State", "Country", "webSiteName", "taxId", "balanceAmount", "parentDistributor", "NoOfBlankSims", "NoOfActivationsMTD", "Created DateTime", "Modified DateTime");

        //        if (dtExcel.Rows.Count > 0)
        //        {
        //            string filename = " Distributor.xls";
        //            System.IO.StringWriter tw = new System.IO.StringWriter();
        //            System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);
        //            GridView grdView = new GridView();
        //            //dgGrid.HeaderStyle
        //            grdView.DataSource = dtExcel;
        //            grdView.DataBind();

        //            ScriptManager.RegisterStartupScript(this, GetType(), "", "HideProgress();", true);
        //            //Get the HTML for the control.
        //            grdView.RenderControl(hw);
        //            //Write the HTML back to the browser.
        //            //Response.ContentType = application/vnd.ms-excel;
        //            Response.ContentType = "application/vnd.ms-excel";
        //            Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename + "");
        //            this.EnableViewState = false;
        //            Response.Write(tw.ToString());
        //            Response.End();

        //            ScriptManager.RegisterStartupScript(this, GetType(), "", "HideProgress();", true);
        //        }

        //    }

        //}

        protected void lnkResetPass_Click(object sender, EventArgs e)
        {
            try
            {

                LinkButton btn = (LinkButton)(sender);

                int DistributorID = Convert.ToInt32(btn.CommandArgument);

                DataSet dset = ssc.GetRandomPassword(Convert.ToInt64(DistributorID));
                if (dset.Tables[0].Rows.Count > 0)
                {

                    string Password = Convert.ToString(dset.Tables[0].Rows[0]["NewPassword"]);
                    string EncPassword = Encryption.Encrypt(Password);

                    DataSet dsave = ssc.SaveRandomPassword(Convert.ToInt64(DistributorID), EncPassword);

                    DataSet ds1 = ssc.FetchContactDetail(DistributorID);
                    if (ds1.Tables[0].Rows.Count > 0)
                    {
                        if (ds1.Tables[1].Rows.Count > 0)
                        {
                            string username = Convert.ToString(ds1.Tables[1].Rows[0]["Name"]);
                            string mailID = Convert.ToString(ds1.Tables[1].Rows[0]["EmailID"]);
                            string userID = Convert.ToString(ds1.Tables[1].Rows[0]["UserName"]);


                            string LoginUrl = "https://" + HttpContext.Current.Request.Url.Host + HttpContext.Current.Request.ApplicationPath + "/Login.aspx";

                            SendMail(mailID, "Password Reset ", username, userID, Password, LoginUrl);
                            ShowPopUpMsg("Password sent to your registered Emailid. \n Please check your mail");
                        }
                    }

                    //Password = Encryption.Decrypt(Password);

                }
                else
                {
                    ShowPopUpMsg("");
                }




            }
            catch (Exception ex)
            {

            }
        }

        protected void btnHoldStatus_Click(object sender, EventArgs e)
        {
            int a = 0;
            int status = 0;
            Button btn = (Button)sender;
            int i = ((RepeaterItem)btn.NamingContainer).ItemIndex;
            Button buttonHold = (Button)RepeaterDistributorView.Items[i].FindControl("btnHoldStatus");
            HiddenField hdnDistributortId = (HiddenField)RepeaterDistributorView.Items[i].FindControl("hdnDistributortId");
            Int64 DistributorId = Convert.ToInt64(hdnDistributortId.Value.Trim());
            Session["DistIDH"] = Convert.ToString(DistributorId);
            ViewState["btn"] = buttonHold.ID;
            if (buttonHold.Text == "Hold")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "displayalertmessage", "HyperLinkClick();", true);
                //a = ssc.UpdateHoldStatus(Convert.ToInt64(Session["DistID"]), status, Reason);
                //if (a > 0)
                //{
                //    ShowPopUpMsg("Hold Successful");
                //    buttonHold.Text = "Unhold";
                //}
            }
            else
            {
                a = ssc.UpdateHoldStatus(DistributorId, status, "");
                if (a > 0)
                {
                    ShowPopUpMsg("Unhold Successful");
                    buttonHold.Text = "Hold";
                }

            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string Reason = "";
            int status = 1;
            int a = 0;
            Button btnH = new Button();
            Int64 s = Convert.ToInt64(Session["DistIDH"]);
            if (txtHoldReason.Text != "")
            {
                Reason = txtHoldReason.Text.Trim();
            }

            a = ssc.UpdateHoldStatus(Convert.ToInt64(Session["DistIDH"]), status, Reason);
            if (a > 0)
            {
                ShowPopUpMsg("Hold Successful");
                Response.Redirect("Distributorview.aspx");

            }
        }

        protected void btnGetReport_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime? FromDate;
                FromDate = null;
                DateTime? ToDate;
                ToDate = null;
                if (txtFromDate.Text != string.Empty)
                {
                    FromDate = Convert.ToDateTime(txtFromDate.Text);
                }


                if (txtToDate.Text != string.Empty)
                {
                    ToDate = Convert.ToDateTime(txtToDate.Text);
                }

                if (txtFromDate.Text == string.Empty || txtToDate.Text == string.Empty)
                {
                    FromDate = Convert.ToDateTime("1900-01-01");
                    ToDate = DateTime.Now;
                }


                int loginid = Convert.ToInt32(Session["LoginID"]);
                int distributorID = Convert.ToInt32(Session["DistributorID"]);
                string TaxDocument = ddlTaxDocument.SelectedValue;
                string ResellerCertificate = ddlResellerCertificate.SelectedValue;
                if (ddlLevel1.SelectedValue == "0")
                {

                    Distributor[] dis = ssc.GetDistributorServiceWithDate(loginid, distributorID, TaxDocument, ResellerCertificate, Convert.ToDateTime(FromDate), Convert.ToDateTime(ToDate));
                    if (dis != null)
                    {
                        RepeaterDistributorView.DataSource = dis;
                        RepeaterDistributorView.DataBind();
                    }
                    else
                    {
                        RepeaterDistributorView.DataBind();
                    }
                }
                else
                {

                    Distributor[] dis = ssc.GetDistributorServiceWithDate(loginid, Convert.ToInt32(ViewState["Dis"]), TaxDocument, ResellerCertificate, Convert.ToDateTime(FromDate), Convert.ToDateTime(ToDate));
               
                if (dis != null)
                {
                    RepeaterDistributorView.DataSource = dis;
                    RepeaterDistributorView.DataBind();
                }
                else
                {
                    RepeaterDistributorView.DataBind();
                }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void FillDistributor()
        {
            DateTime? FromDate;
            FromDate = null;
            DateTime? ToDate;
            ToDate = null;

            if (txtFromDate.Text == string.Empty || txtToDate.Text == string.Empty)
            {
                FromDate = Convert.ToDateTime("1900-01-01");
                ToDate = DateTime.Now;
            }

            int loginid = Convert.ToInt32(Session["LoginID"]);
            int distributorID = Convert.ToInt32(Session["DistributorID"]);
            string TaxDocument = ddlTaxDocument.SelectedValue;
            string ResellerCertificate = ddlResellerCertificate.SelectedValue;
            Distributor[] dis = ssc.GetDistributorServiceWithDate(loginid, distributorID, "0", "0", Convert.ToDateTime(FromDate), Convert.ToDateTime(ToDate));
            if (dis != null)
            {
                RepeaterDistributorView.DataSource = dis;
                RepeaterDistributorView.DataBind();
            }
            else
            {
                RepeaterDistributorView.DataBind();
            }

        }

        //protected void btntopup_Click(object sender, EventArgs e)
        //{
        //    int a = 0;
        //    int status = 0;
        //    Button btn = (Button)sender;
        //    int i = ((RepeaterItem)btn.NamingContainer).ItemIndex;
        //    Button buttonTopUp = (Button)RepeaterDistributorView.Items[i].FindControl("btntopup");
        //    HiddenField hdnDistributortId = (HiddenField)RepeaterDistributorView.Items[i].FindControl("hdnDistributortId");
        //    Int64 DistributorId = Convert.ToInt64(hdnDistributortId.Value.Trim());
        //    Session["DistIDH"] = Convert.ToString(DistributorId);
        //    ViewState["btn"] = buttonTopUp.ID;


        //    if (buttonTopUp.Text == "TopUp")
        //    {
        //        ScriptManager.RegisterStartupScript(this, GetType(), "displayalertmessage", "HyperLinkClicktopup();", true);

        //    }
        //    else
        //    {
        //        a = ssc.UpdateHoldStatus(DistributorId, status, "");
        //        if (a > 0)
        //        {
        //            ShowPopUpMsg("Unhold Successful");
        //            buttonTopUp.Text = "TopUp";
        //        }

        //    }
        //}

        protected void btnpopupsubmit_Click(object sender, EventArgs e)
        {
            try
            {

                int a = 0;
                Button btnH = new Button();
                Int64 distributorIdForTopup = Convert.ToInt64(HiddenField1.Value.Trim());
                if (txtmintopup.Text == "" && txtmaxtopup.Text == "" && txtfixed.Text == "")
                {
                    ShowPopUpMsg("Please enter required details to update");
                    return;
                }
                else
                {
                    if (txtmintopup.Text == "")
                        txtmintopup.Text = "0.00";
                    if (txtmaxtopup.Text == "")
                        txtmaxtopup.Text = "0.00";
                    if (txtfixed.Text == "")
                        txtfixed.Text = "0.00";
                    a = ssc.UpdateTopupOption(distributorIdForTopup, Convert.ToDecimal(txtmintopup.Text), Convert.ToDecimal(txtmaxtopup.Text), Convert.ToDecimal(txtfixed.Text), ddlPaypaltax.SelectedValue);
                    if (a > 0)
                    {
                        ShowPopUpMsg("TopUp Option Successful");
                        txtmintopup.Text = "";
                        txtmaxtopup.Text = "";
                        txtfixed.Text = "";
                        Response.Redirect("Distributorview.aspx", false);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GetToupDetails(int DistributorId)
        {
            try
            {
                int DistributorID = Convert.ToInt32(HiddenField1.Value.Trim());

                DataSet ds = new DataSet();
                ds = ssc.GetDistributorInformation(DistributorId);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        txtmintopup.Text = Convert.ToString(ds.Tables[0].Rows[0]["MinTopup"]);
                        txtmaxtopup.Text = Convert.ToString(ds.Tables[0].Rows[0]["MaxTopup"]);
                        txtfixed.Text = Convert.ToString(ds.Tables[0].Rows[0]["PaypalTax"]);
                        string taxtype = Convert.ToString(ds.Tables[0].Rows[0]["PaypalTaxType"]);
                        if(taxtype!="")
                        {
                            ddlPaypaltax.SelectedValue = Convert.ToString(taxtype);
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                ShowPopUpMsg(ex.Message);
            }
        }

        //protected void Button1_Click(object sender, EventArgs e)
        //{
        //   // Int64 distributorIdForTopup = Convert.ToInt64(HiddenField1.Value.Trim());
        //    GetToupDetails(Convert.ToInt32(HiddenField1.Value.Trim()));
        //}

        //[WebMethod]
        //protected void TopUpDetail()
        //{
        //    try
        //    {

        //    }
        //    catch (Exception)
        //    {
                
        //        throw;
        //    }
        //}

        public void AdjustLevel(Int32 distributorID)
        {
            if (distributorID == 1)
            {
                BindLevel1Distributor();
                ddlLevel2.Attributes.Add("disabled", "disabled");
                ddlLevel3.Attributes.Add("disabled", "disabled");
                ddlLevel4.Attributes.Add("disabled", "disabled");
                ddlLevel5.Attributes.Add("disabled", "disabled");
            }
            else
            {
                DataSet ds = ssc.GETLEVELDistributor(distributorID, 1);
                int Level = ds.Tables[1].Rows.Count;
                if (Level == 0)
                {
                    //Company Level
                }
                else if (Level == 1)
                {
                    ddlLevel1.DataTextField = "name";
                    ddlLevel1.DataValueField = "id";
                    ddlLevel1.DataSource = ds.Tables[1];
                    ddlLevel1.DataBind();

                    ddlLevel1.SelectedValue = Convert.ToString(ds.Tables[1].Rows[0]["id"]);

                    ddlLevel1CHange();

                    ViewState["Dis"] = Convert.ToInt32(ddlLevel1.SelectedValue);

                    ddlLevel1.Attributes.Add("disabled", "disabled");

                    LEVEL1.Text = "Distributor";
                    LEVEL2.Text = "Sub-Level1";
                    LEVEL3.Text = "Sub-Level2";
                    LEVEL4.Text = "Sub-Level3";
                    LEVEL5.Text = "Sub-Level4";
                }
                else if (Level == 2)
                {
                    ddlLevel1.DataTextField = "name";
                    ddlLevel1.DataValueField = "id";
                    ddlLevel1.DataSource = ds.Tables[1];
                    ddlLevel1.DataBind();

                    ddlLevel2.DataTextField = "name";
                    ddlLevel2.DataValueField = "id";
                    ddlLevel2.DataSource = ds.Tables[1];
                    ddlLevel2.DataBind();

                    ddlLevel1.SelectedValue = Convert.ToString(ds.Tables[1].Rows[0]["id"]);
                    ddlLevel2.SelectedValue = Convert.ToString(ds.Tables[1].Rows[1]["id"]);

                    ddlLevel2CHange();

                    ViewState["Dis"] = Convert.ToInt32(ddlLevel2.SelectedValue);

                    ddlLevel1.Attributes.Add("disabled", "disabled");
                    ddlLevel2.Attributes.Add("disabled", "disabled");

                    ddlLevel1.Visible = false;

                    LEVEL1.Visible = false;
                    LEVEL2.Text = "Distributor";
                    LEVEL3.Text = "Sub-Level1";
                    LEVEL4.Text = "Sub-Level2";
                    LEVEL5.Text = "Sub-Level3";
                }
                else if (Level == 3)
                {
                    ddlLevel1.DataTextField = "name";
                    ddlLevel1.DataValueField = "id";
                    ddlLevel1.DataSource = ds.Tables[1];
                    ddlLevel1.DataBind();

                    ddlLevel2.DataTextField = "name";
                    ddlLevel2.DataValueField = "id";
                    ddlLevel2.DataSource = ds.Tables[1];
                    ddlLevel2.DataBind();

                    ddlLevel3.DataTextField = "name";
                    ddlLevel3.DataValueField = "id";
                    ddlLevel3.DataSource = ds.Tables[1];
                    ddlLevel3.DataBind();

                    ddlLevel1.SelectedValue = Convert.ToString(ds.Tables[1].Rows[0]["id"]);
                    ddlLevel2.SelectedValue = Convert.ToString(ds.Tables[1].Rows[1]["id"]);
                    ddlLevel3.SelectedValue = Convert.ToString(ds.Tables[1].Rows[2]["id"]);

                    ddlLevel3CHange();

                    ViewState["Dis"] = Convert.ToInt32(ddlLevel3.SelectedValue);

                    ddlLevel1.Attributes.Add("disabled", "disabled");
                    ddlLevel2.Attributes.Add("disabled", "disabled");
                    ddlLevel3.Attributes.Add("disabled", "disabled");

                    ddlLevel1.Visible = false;
                    ddlLevel2.Visible = false;

                    LEVEL1.Visible = false;
                    LEVEL2.Visible = false;
                    LEVEL3.Text = "Distributor";
                    LEVEL4.Text = "Sub-Level1";
                    LEVEL5.Text = "Sub-Level2";
                }
                else if (Level == 4)
                {
                    ddlLevel1.DataTextField = "name";
                    ddlLevel1.DataValueField = "id";
                    ddlLevel1.DataSource = ds.Tables[1];
                    ddlLevel1.DataBind();

                    ddlLevel2.DataTextField = "name";
                    ddlLevel2.DataValueField = "id";
                    ddlLevel2.DataSource = ds.Tables[1];
                    ddlLevel2.DataBind();

                    ddlLevel3.DataTextField = "name";
                    ddlLevel3.DataValueField = "id";
                    ddlLevel3.DataSource = ds.Tables[1];
                    ddlLevel3.DataBind();

                    ddlLevel4.DataTextField = "name";
                    ddlLevel4.DataValueField = "id";
                    ddlLevel4.DataSource = ds.Tables[1];
                    ddlLevel4.DataBind();

                    ddlLevel1.SelectedValue = Convert.ToString(ds.Tables[1].Rows[0]["id"]);
                    ddlLevel2.SelectedValue = Convert.ToString(ds.Tables[1].Rows[1]["id"]);
                    ddlLevel3.SelectedValue = Convert.ToString(ds.Tables[1].Rows[2]["id"]);
                    ddlLevel4.SelectedValue = Convert.ToString(ds.Tables[1].Rows[3]["id"]);

                    ddlLevel4CHange();

                    ViewState["Dis"] = Convert.ToInt32(ddlLevel4.SelectedValue);

                    ddlLevel1.Attributes.Add("disabled", "disabled");
                    ddlLevel2.Attributes.Add("disabled", "disabled");
                    ddlLevel3.Attributes.Add("disabled", "disabled");
                    ddlLevel4.Attributes.Add("disabled", "disabled");

                    ddlLevel1.Visible = false;
                    ddlLevel2.Visible = false;
                    ddlLevel3.Visible = false;

                    LEVEL1.Visible = false;
                    LEVEL2.Visible = false;
                    LEVEL3.Visible = false;
                    LEVEL4.Text = "Distributor";
                    LEVEL5.Text = "Sub-Level1";
                }

                else if (Level == 5)
                {
                    ddlLevel1.DataTextField = "name";
                    ddlLevel1.DataValueField = "id";
                    ddlLevel1.DataSource = ds.Tables[1];
                    ddlLevel1.DataBind();

                    ddlLevel2.DataTextField = "name";
                    ddlLevel2.DataValueField = "id";
                    ddlLevel2.DataSource = ds.Tables[1];
                    ddlLevel2.DataBind();

                    ddlLevel3.DataTextField = "name";
                    ddlLevel3.DataValueField = "id";
                    ddlLevel3.DataSource = ds.Tables[1];
                    ddlLevel3.DataBind();

                    ddlLevel4.DataTextField = "name";
                    ddlLevel4.DataValueField = "id";
                    ddlLevel4.DataSource = ds.Tables[1];
                    ddlLevel4.DataBind();

                    ddlLevel5.DataTextField = "name";
                    ddlLevel5.DataValueField = "id";
                    ddlLevel5.DataSource = ds.Tables[1];
                    ddlLevel5.DataBind();

                    ddlLevel1.SelectedValue = Convert.ToString(ds.Tables[1].Rows[0]["id"]);
                    ddlLevel2.SelectedValue = Convert.ToString(ds.Tables[1].Rows[1]["id"]);
                    ddlLevel3.SelectedValue = Convert.ToString(ds.Tables[1].Rows[2]["id"]);
                    ddlLevel4.SelectedValue = Convert.ToString(ds.Tables[1].Rows[3]["id"]);
                    ddlLevel5.SelectedValue = Convert.ToString(ds.Tables[1].Rows[4]["id"]);


                    ViewState["Dis"] = Convert.ToInt32(ddlLevel5.SelectedValue);

                    ddlLevel1.Attributes.Add("disabled", "disabled");
                    ddlLevel2.Attributes.Add("disabled", "disabled");
                    ddlLevel3.Attributes.Add("disabled", "disabled");
                    ddlLevel4.Attributes.Add("disabled", "disabled");
                    ddlLevel5.Attributes.Add("disabled", "disabled");

                    ddlLevel1.Visible = false;
                    ddlLevel2.Visible = false;
                    ddlLevel3.Visible = false;
                    ddlLevel4.Visible = false;

                    LEVEL1.Visible = false;
                    LEVEL2.Visible = false;
                    LEVEL3.Visible = false;
                    LEVEL4.Visible = false;
                    LEVEL5.Text = "Distributor";
                }

            }
        }

        public void BindLevel1Distributor()
        {
            if (Convert.ToInt32(Session["DistributorID"]) == 1)
            {
                DataSet ds = ssc.GETLEVELDistributor(0, 0);
                if (ds != null)
                {
                    ddlLevel1.DataSource = ds.Tables[0];
                    ddlLevel1.DataValueField = "id";
                    ddlLevel1.DataTextField = "Name";
                    ddlLevel1.DataBind();
                    ddlLevel1.Items.Insert(0, new ListItem("ALL", "0"));
                    ViewState["Dis"] = 1;
                }
            }
        }

        public void ddlLevel1CHange()
        {
            {
                DataSet ds = ssc.GETLEVELDistributor(Convert.ToInt32(ddlLevel1.SelectedValue), 1);
                if (ds != null)
                {
                    ddlLevel2.Attributes.Remove("disabled");
                    ddlLevel2.DataValueField = "id";
                    ddlLevel2.DataSource = ds.Tables[0];
                    ddlLevel2.DataTextField = "Name";
                    ddlLevel2.DataBind();
                    ddlLevel2.Items.Insert(0, new ListItem("ALL", "0"));

                }
            }
        }

        public void ddlLevel2CHange()
        {
            {
                DataSet ds = ssc.GETLEVELDistributor(Convert.ToInt32(ddlLevel2.SelectedValue), 2);
                if (ds != null)
                {
                    ddlLevel3.Attributes.Remove("disabled");
                    ddlLevel3.DataSource = ds.Tables[0];
                    ddlLevel3.DataValueField = "id";
                    ddlLevel3.DataTextField = "Name";
                    ddlLevel3.DataBind();
                    ddlLevel3.Items.Insert(0, new ListItem("ALL", "0"));
                }
            }
        }

        public void ddlLevel3CHange()
        {
            {
                DataSet ds = ssc.GETLEVELDistributor(Convert.ToInt32(ddlLevel3.SelectedValue), 3);
                if (ds != null)
                {
                    ddlLevel4.Attributes.Remove("disabled");
                    ddlLevel4.DataSource = ds.Tables[0];
                    ddlLevel4.DataValueField = "id";
                    ddlLevel4.DataTextField = "Name";
                    ddlLevel4.DataBind();
                    ddlLevel4.Items.Insert(0, new ListItem("ALL", "0"));
                }
            }
        }

        public void ddlLevel4CHange()
        {
            {
                DataSet ds = ssc.GETLEVELDistributor(Convert.ToInt32(ddlLevel4.SelectedValue), 4);
                if (ds != null)
                {
                    ddlLevel5.Attributes.Remove("disabled");
                    ddlLevel5.DataSource = ds.Tables[0];
                    ddlLevel5.DataValueField = "id";
                    ddlLevel5.DataTextField = "Name";
                    ddlLevel5.DataBind();
                    ddlLevel5.Items.Insert(0, new ListItem("ALL", "0"));
                }
            }
        }

        protected void ddlLevel1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlLevel1CHange();
            ViewState["Dis"] = Convert.ToInt32(ddlLevel1.SelectedValue);
            if (Convert.ToInt32(ddlLevel1.SelectedValue) == 0)
            {
                ddlLevel2.SelectedValue = "0";
                ddlLevel2.Attributes.Add("disabled", "disabled");
            }
            else
            {
            }
            ddlLevel3.SelectedValue = "0";
            ddlLevel3.Attributes.Add("disabled", "disabled");
            ddlLevel4.SelectedValue = "0";
            ddlLevel4.Attributes.Add("disabled", "disabled");
            ddlLevel5.SelectedValue = "0";
            ddlLevel5.Attributes.Add("disabled", "disabled");
        }

        protected void ddlLevel2_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlLevel2CHange();
            if (Convert.ToInt32(ddlLevel2.SelectedValue) == 0)
            {
                ViewState["Dis"] = Convert.ToString(ddlLevel1.SelectedValue);
                ddlLevel3.SelectedValue = "0";
                ddlLevel3.Attributes.Add("disabled", "disabled");

            }
            else
            {
                ViewState["Dis"] = Convert.ToInt32(ddlLevel2.SelectedValue);
            }
            ddlLevel4.SelectedValue = "0";
            ddlLevel4.Attributes.Add("disabled", "disabled");
            ddlLevel5.SelectedValue = "0";
            ddlLevel5.Attributes.Add("disabled", "disabled");
        }

        protected void ddlLevel3_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlLevel3CHange();
            if (Convert.ToInt32(ddlLevel3.SelectedValue) == 0)
            {
                ViewState["Dis"] = Convert.ToString(ddlLevel2.SelectedValue);
                ddlLevel4.SelectedValue = "0";
                ddlLevel4.Attributes.Add("disabled", "disabled");
            }
            else
            {
                ViewState["Dis"] = Convert.ToInt32(ddlLevel3.SelectedValue);
            }
            ddlLevel5.SelectedValue = "0";
            ddlLevel5.Attributes.Add("disabled", "disabled");
        }

        protected void ddlLevel4_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlLevel4CHange();
            if (Convert.ToInt32(ddlLevel4.SelectedValue) == 0)
            {
                ViewState["Dis"] = Convert.ToString(ddlLevel3.SelectedValue);

                ddlLevel5.SelectedValue = "0";
                ddlLevel5.Attributes.Add("disabled", "disabled");
            }
            else
            {
                ViewState["Dis"] = Convert.ToInt32(ddlLevel4.SelectedValue);
            }
        }

        protected void ddlLevel5_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(ddlLevel5.SelectedValue) == 0)
            {
                ViewState["Dis"] = Convert.ToString(ddlLevel4.SelectedValue);
            }
            else
            {
                ViewState["Dis"] = Convert.ToInt32(ddlLevel5.SelectedValue);
            }
        }
    }
}