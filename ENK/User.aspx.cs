using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Net.Mail;
using System.Configuration;
using ENK.ServiceReference1;

namespace ENK
{
    public partial class User : System.Web.UI.Page
    {
        Service1Client svc = new Service1Client();

        protected void Page_Load(object sender, EventArgs e)
        {
            ScriptManager.GetCurrent(this).RegisterPostBackControl(btnUpdate);
            ScriptManager.GetCurrent(this).RegisterPostBackControl(btnUpdateUp);
            if (!Page.IsPostBack)
            {
                string Condition = Request.QueryString["Condition"];
                if (Condition != null && Condition != "")
                {
                    Condition = Encryption.Decrypt(Request.QueryString["Condition"]);
                }
                else
                {
                    Condition = "";
                }
                hddnMode.Value = Condition;
                string Identity = Request.QueryString["Identity"];
                if (Identity != null && Identity != "")
                {
                    Identity = Encryption.Decrypt(Request.QueryString["Identity"]);
                }
                else
                {
                    Identity = "";
                }
                if (Condition == "View")
                {
                    BindDDL();
                    FillUser(Identity);
                    hddnUserID.Value = Identity.ToString();
                    ResetControl(2);
                    divActive.Visible = true;
                    btnReset.Visible = false;
                    btnSave.Visible = false;
                    btnBack.Visible = true;
                    btnUpdate.Visible = false;
                    liUpdate.Visible = false;
                    liBack.Visible = true;
                }
                if (Condition == "Edit")
                {
                    BindDDL();
                    FillUser(Identity);
                    hddnUserID.Value = Identity.ToString();
                    divActive.Visible = true;
                    btnReset.Visible = false;
                    btnSave.Visible = false;
                    btnBack.Visible = true;
                    btnUpdate.Visible = true;
                    liUpdate.Visible = true;
                    liBack.Visible = true;
                }
                if (Condition == "")
                {
                    BindDDL();
                    divActive.Visible = false;
                    btnBack.Visible = true;
                    btnUpdate.Visible = false;
                    liUpdate.Visible = false;
                    liBack.Visible = true;
                    liSave.Visible = true;
                    liReset.Visible = true;
                }
            }


        }

        public void BindDDL()
        {
            try
            {
                int userid = Convert.ToInt32(Session["LoginId"]);
                int distributorID = Convert.ToInt32(Session["DistributorID"]);
                Distributor[] ds = svc.GetDistributorDDLService(userid, distributorID);
                //DataSet dss = svc.GetRoleService();
                //ddlRole.DataSource = dss.Tables[0];
                //ddlRole.DataValueField = "ID";
                //ddlRole.DataTextField = "Name";
                //ddlRole.DataBind();
                //ddlRole.Items.Insert(0, new ListItem("---Select---", "0"));

                ddlDistributor.DataSource = ds;
                ddlDistributor.DataValueField = "distributorID";
                ddlDistributor.DataTextField = "distributorName";
                ddlDistributor.DataBind();
                ddlDistributor.Items.Insert(0, new ListItem("---Select---", "0"));
            }
            catch (Exception ex)
            {

            }

        }

        public void FillUser(string UserID)
        {
            try
            {
                int LoginID = Convert.ToInt32(Session["LoginId"]);

                DataSet ds = svc.GetUserService(Convert.ToInt32(UserID), LoginID);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        hddnUserID.Value = Convert.ToString(ds.Tables[0].Rows[0]["ID"]);
                        txtUserFName.Text = Convert.ToString(ds.Tables[0].Rows[0]["Fname"]);
                        txtUserLName.Text = Convert.ToString(ds.Tables[0].Rows[0]["Lname"]);
                        ddlDistributor.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["DistributorID"]);
                        hdnDistributor.Value = Convert.ToString(Session["DistributorID"]);
                        hddnRole.Value = Convert.ToString(ds.Tables[0].Rows[0]["RoleID"]);
                        //ddlRole.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["RoleID"]);
                        txtUserId.Text = Convert.ToString(ds.Tables[0].Rows[0]["UserName"]);
                        if (Convert.ToInt32(Session["DistributorID"]) == Convert.ToInt32(ds.Tables[0].Rows[0]["DistributorID"]) || Convert.ToString(Session["ClientType"]) == "Company")
                        {
                            txtPassWord.Text = Encryption.Decrypt(Convert.ToString(ds.Tables[0].Rows[0]["Password"]));
                            txtConfirmPass.Text = Encryption.Decrypt(Convert.ToString(ds.Tables[0].Rows[0]["Password"]));
                        }
                        else
                        {
                            hdnPassword.Value = Encryption.Decrypt(Convert.ToString(ds.Tables[0].Rows[0]["Password"]));
                            txtPassWord.Text = "";
                            txtPassWord.Enabled = false;
                            txtConfirmPass.Text = "";
                            txtConfirmPass.Enabled = false;
                        }
                        txtEmail.Text = Convert.ToString(ds.Tables[0].Rows[0]["EmailID"]);
                        txtMobile.Text = Convert.ToString(ds.Tables[0].Rows[0]["MobileNumber"]);

                        DateTime dt1 = Convert.ToDateTime(ds.Tables[0].Rows[0]["ActiveFromDtTm"]).Date;
                        txtActiveFrom.Text = Convert.ToString(dt1.ToString("yyyy-MM-dd"));
                        DateTime dt2 = Convert.ToDateTime(ds.Tables[0].Rows[0]["ActiveToDtTm"]).Date;
                        txtActiveTo.Text = Convert.ToString(dt2.ToString("yyyy-MM-dd"));

                        CheckBox1.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["IsActive"]);
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        public void ResetControl(int cond)
        {
            if (cond == 1)
            {
                txtUserFName.Text = "";
                txtUserLName.Text = "";
                ddlDistributor.SelectedIndex = 0;
                // ddlRole.SelectedIndex = 0;
                txtUserId.Text = "";
                txtPassWord.Text = "";
                txtConfirmPass.Text = "";
                txtEmail.Text = "";
                txtMobile.Text = "";
                txtActiveFrom.Text = "";
                txtActiveTo.Text = "";
                CheckBox1.Checked = true;

            }
            if (cond == 2)
            {
                txtUserFName.Attributes.Add("readonly", "true");
                txtUserLName.Attributes.Add("readonly", "true");
                ddlDistributor.Attributes.Add("disabled", "disabled");
                // ddlRole.Attributes.Add("disabled", "disabled");
                txtUserId.Attributes.Add("readonly", "true");
                txtPassWord.Attributes.Add("readonly", "true");
                txtConfirmPass.Attributes.Add("readonly", "true");
                txtEmail.Attributes.Add("readonly", "true");
                txtMobile.Attributes.Add("readonly", "true");
                txtActiveFrom.Attributes.Add("readonly", "true");
                txtActiveTo.Attributes.Add("readonly", "true");
                CheckBox1.Enabled = false;
            }

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                CheckUserID();
                if (hddnMatch.Value == "True")
                {
                    ShowPopUpMsg("UserID already exist \n Please change userID");
                    return;
                }

                if (txtEmail.Text.Trim() == "")
                {
                    ShowPopUpMsg("EmailID Can't Blank");
                    return;
                }

                if (txtPassWord.Text.Trim() != txtConfirmPass.Text.Trim())
                {
                    ShowPopUpMsg("Password and Confirm Password Different \n Please Type Same Pasword");
                    return;
                }

                SUsers ud = new SUsers();
                ud.firstName = txtUserFName.Text.Trim();
                ud.lastName = txtUserLName.Text.Trim();
                ud.userName = txtUserId.Text.Trim();
                ud.emailID = txtEmail.Text.Trim();
                ud.contactNo = txtMobile.Text.Trim();
                ud.distributorID = Convert.ToInt32(ddlDistributor.SelectedValue);
                ud.roleID = 0;
                ud.pwd = Encryption.Encrypt(txtPassWord.Text.Trim());
                ud.activeFrom = Convert.ToDateTime(txtActiveFrom.Text.Trim());
                ud.activeTo = Convert.ToDateTime(txtActiveTo.Text.Trim());
                ud.isActive = CheckBox1.Checked;

                int LoginId = Convert.ToInt32(Session["LoginId"]);
                int a = svc.AddUserService(ud, LoginId);
                if (a > 0)
                {
                    string LoginUrl = "http://" + HttpContext.Current.Request.Url.Host + HttpContext.Current.Request.ApplicationPath + "/Login.aspx";
                    SendMail(txtEmail.Text.Trim(), "Login Detail", txtUserFName.Text.Trim() + " " + txtUserLName.Text.Trim(), txtUserId.Text.Trim(), txtPassWord.Text.Trim(), LoginUrl);
                    ShowPopUpMsg("User Added Successfuly");
                    ResetControl(1);
                }
                else
                {
                    ShowPopUpMsg("User Added Unsuccessfuly" + "\n" + "Please Try Again");
                }

            }
            catch (Exception ex)
            {

            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            ResetControl(1);
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                CheckUserID();

                if (hddnMatch.Value == "True")
                {
                    ShowPopUpMsg("UserID already exist \n Please change userID");
                    return;
                }

                if (txtEmail.Text.Trim() == "")
                {
                    ShowPopUpMsg("EmailID Can't Blank");
                    return;
                }

                if (Convert.ToString(Session["ClientType"]) == "Company")
                {
                    if (txtPassWord.Text.Trim() != txtConfirmPass.Text.Trim())
                    {
                        ShowPopUpMsg("Password and Confirm Password Different \n Please Type Same Pasword");
                        return;
                    }
                }

                SUsers ud = new SUsers();
                ud.userID = Convert.ToInt32(hddnUserID.Value);
                ud.firstName = txtUserFName.Text.Trim();
                ud.lastName = txtUserLName.Text.Trim();
                ud.userName = txtUserId.Text.Trim();
                ud.emailID = txtEmail.Text.Trim();
                ud.contactNo = txtMobile.Text.Trim();
                ud.distributorID = Convert.ToInt32(ddlDistributor.SelectedValue);
                ud.roleID = Convert.ToInt32(hddnRole.Value);
                if (Convert.ToString(Session["ClientType"]) == "Company")
                {
                    ud.pwd = Encryption.Encrypt(txtPassWord.Text.Trim());

                }
                else
                {
                    ud.pwd = Encryption.Encrypt(hdnPassword.Value.Trim());
                }
                ud.activeFrom = Convert.ToDateTime(txtActiveFrom.Text.Trim());
                ud.activeTo = Convert.ToDateTime(txtActiveTo.Text.Trim());
                ud.isActive = CheckBox1.Checked;

                int LoginId = Convert.ToInt32(Session["LoginId"]);
                int a = svc.UpdateUserService(ud, LoginId);
                if (a > 0)
                {
                    string LoginUrl = "http://" + HttpContext.Current.Request.Url.Host + HttpContext.Current.Request.ApplicationPath + "/Login.aspx";
                    SendMail(txtEmail.Text.Trim(), "Login Detail", txtUserFName.Text.Trim() + " " + txtUserLName.Text.Trim(), txtUserId.Text.Trim(), txtPassWord.Text.Trim(), LoginUrl);
                    ShowPopUpMsg("User Updated Successfuly");
                    ResetControl(1);
                    btnUpdate.Visible = false;
                    liBack.Visible = true;
                    liUpdate.Visible = false;
                    //btnUpdateUp.Visible = false;
                }
                else
                {
                    ShowPopUpMsg("User  Updation Fail" + "\n" + "Please Try Again");
                }

            }
            catch (Exception ex)
            {

            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("UserList.aspx");
        }

        private void ShowPopUpMsg(string msg)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("alert('");
            sb.Append(msg.Replace("\n", "\\n").Replace("\r", "").Replace("'", "\\'"));
            sb.Append("');");
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "showalert", sb.ToString(), true);
        }

        protected void btnUpdateUp_Click(object sender, EventArgs e)
        {
            btnUpdate_Click(null, null);
            //UpdatePanel3.Update();
        }

        protected void btnBackUp_Click(object sender, EventArgs e)
        {
            Response.Redirect("UserList.aspx");
        }

        public void CheckUserID()
        {
            try
            {
                string UserID = txtUserId.Text.Trim();
                int DistributorID = Convert.ToInt32(Session["DistributorID"]);
                int ClientTypeID = Convert.ToInt32(Session["ClientTypeID"]);
                int LoginID = 0;
                if (hddnMode.Value == "Edit")
                {
                    LoginID = Convert.ToInt32(hddnUserID.Value);
                }
                if (hddnMode.Value == "")
                {
                    LoginID = 0;
                }


                DataSet ds = svc.VerifyUserIDService(DistributorID, ClientTypeID, LoginID, UserID);

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        lblMessage.Text = "UserID Already Exist";
                        btnSave.Attributes.Add("disabled", "disabled");
                        btnUpdate.Attributes.Add("disabled", "disabled");
                        btnUpdateUp.Attributes.Add("disabled", "disabled");
                        hddnMatch.Value = "True";
                    }
                    else
                    {
                        lblMessage.Text = "";
                        btnSave.Attributes.Remove("disabled");
                        btnUpdate.Attributes.Remove("disabled");
                        btnUpdateUp.Attributes.Remove("disabled");
                        hddnMatch.Value = "False";
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
        protected void txtUserId_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string UserID = txtUserId.Text.Trim();
                int DistributorID = Convert.ToInt32(Session["DistributorID"]);
                int ClientTypeID = Convert.ToInt32(Session["ClientTypeID"]);
                int LoginID = 0;
                if (hddnMode.Value == "Edit")
                {
                    LoginID = Convert.ToInt32(hddnUserID.Value);
                }
                if (hddnMode.Value == "")
                {
                    LoginID = 0;
                }


                DataSet ds = svc.VerifyUserIDService(DistributorID, ClientTypeID, LoginID, UserID);

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        lblMessage.Text = "UserID Already Exist";
                        btnSave.Attributes.Add("disabled", "disabled");
                        btnUpdate.Attributes.Add("disabled", "disabled");
                        btnUpdateUp.Attributes.Add("disabled", "disabled");
                        hddnMatch.Value = "True";
                    }
                    else
                    {
                        lblMessage.Text = "";
                        btnSave.Attributes.Remove("disabled");
                        btnUpdate.Attributes.Remove("disabled");
                        btnUpdateUp.Attributes.Remove("disabled");
                        hddnMatch.Value = "False";
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        public void SendMail(string SendTo, string Subject, string UserName, string UserID, string pass, string LoginUrl)
        {
            try
            {
                string LogoUrl = "http://" + HttpContext.Current.Request.Url.Host + HttpContext.Current.Request.ApplicationPath + "/img/logo.png";
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

        protected void btnSaveUp_Click(object sender, EventArgs e)
        {
            btnSave_Click(null, null);
        }

        protected void btnResetUp_Click(object sender, EventArgs e)
        {
            ResetControl(1);
        }

    }
}