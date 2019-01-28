using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ENK.ServiceReference1;

namespace ENK
{
    public partial class IpMapping : System.Web.UI.Page
    {
        Service1Client ssc = new Service1Client();       
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!Page.IsPostBack)
            {
                
                string Identity = Request.QueryString["Identity"];
                if (Identity != null && Identity != "")
                {
                    Identity = Encryption.Decrypt(Request.QueryString["Identity"]);
                    /////////////
                    //DataTable dt = new DataTable();
                    //dt = ssc.GetIPMapping(Convert.ToInt32(Identity));
                    //if (dt.Rows.Count != 0)
                    //{
                    //    grdIPAddlist.DataSource = dt;
                    //    grdIPAddlist.DataBind();
                    //    ViewState["objDTIP"] = dt;
                     ViewState["Did"] = Identity;
                     GetMappedIP();
                    //}
                    ///////////
                }
                else
                {
                    Identity = "";
                }
               
                //ViewState["objIP"] = null;
                //DataTable objDt = new DataTable();
               
            }
        }


        protected void btnIpSave_Click(object sender, EventArgs e)
        {
            // change by akash for save IP

            // change by akash for save IP
            Int64 distributorid = Convert.ToInt64(ViewState["Did"]);//Convert.ToInt64(Session["DistributorID"]);
           // DataTable objDt = (DataTable)ViewState["objDTIP"];
            string _IP = Convert.ToString(txtMultiIPAdress.Text);
            //if (objDt.Rows.Count > 0)
            //{
                int chkRistrictIP = 1;
                int save = ssc.SaveIpMapping(distributorid, _IP, chkRistrictIP);
                if (save == 1)
                {
                    GetMappedIP();
                    Response.Write("<script>alert('IP has been mapped');</script>");
                    txtMultiIPAdress.Text = "";
                }
                else
                {
                    Response.Write("<script>alert('Mapping Unsuccessful');</script>");
                }
            //}
            //else {
            //    Response.Write("<script>alert('Mapping Ip List is empty');</script>");
            //}
        }


        public void btnAddIpAddr_Click(object sender, EventArgs e)
        {           
                //int x = 0;
                //DataTable objDt1 = (DataTable)ViewState["objDTIP"];
                //if (ViewState["objDTIP"] != null)
                //{
                //    foreach (DataRow row in objDt1.Rows)
                //    {
                //        if (row["IP"].ToString() == "*")
                //        {
                //            x = 1;
                //        }
                //    }
                //}
                //if (x != 1)
                //{
                //    addrow(Convert.ToInt64(ViewState["Did"]));
                //}
                //else
                //{
                //    Response.Write("<script>alert('Wild Card IP already exist');</script>");
                //}
        }


        protected void grdIPAddlist_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //int index = Convert.ToInt32(e.RowIndex);
            //DataTable dt = ViewState["objDTIP"] as DataTable;
            ////dt.Rows[index].Delete();
            //dt.Rows.Remove(dt.Rows[index]);
            //ViewState["objDTIP"] = dt;
            //grdIPAddlist.DataSource = dt;
            //grdIPAddlist.DataBind();
            int a = 0;
            try
            {
                Label lid = (Label)grdIPAddlist.Rows[e.RowIndex].FindControl("lblID");
                int ID = Convert.ToInt32(lid.Text);
                a = ssc.DeleteMappingID(ID);
                if (a != null)
                {
                    GetMappedIP();
                    Response.Write("<script>alert('Delete successfully');</script>");
                }
            }
            catch (Exception ex)
            {

            }
        }

        public void addrow(long disID)
        {
            try
            {
                if (txtMultiIPAdress.Text != "")
                {
                    if (ViewState["objDTIP"] == null)
                    {
                        DataTable dt = new DataTable();
                        dt.Columns.Add("IP");
                        dt.Columns.Add("DistributorID");
                        ViewState["objDTIP"] = dt;
                    }

                    DataTable objDt = (DataTable)ViewState["objDTIP"];


                    DataRow objDr = objDt.NewRow();

                    if (txtMultiIPAdress.Text == "*")
                    {
                        objDt.Rows.Clear();

                    }

                    string input = txtMultiIPAdress.Text;
                    Match match = Regex.Match(input, @"\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}");
                    if (match.Success || txtMultiIPAdress.Text == "*")
                    {
                        objDr["IP"] = txtMultiIPAdress.Text;
                        objDr["DistributorID"] = disID;//Convert.ToInt64(Session["DistributorID"]);
                        objDt.Rows.Add(objDr);
                        objDt.AcceptChanges();
                        ViewState["objDTIP"] = objDt;
                        grdIPAddlist.DataSource = ViewState["objDTIP"];
                        grdIPAddlist.DataBind();

                    }
                    else {
                        Response.Write("<script>alert('Invalid Ip input');</script>");
                    }
                }
                else
                {
                    Response.Write("<script>alert('You can't add null IP');</script>");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GetMappedIP()
        {
            Int64 distributorid = Convert.ToInt64(ViewState["Did"]);
            DataTable dt = new DataTable();
            dt = ssc.GetIPMapping(Convert.ToInt32(distributorid));
            if (dt.Rows.Count != 0)
            {
                grdIPAddlist.DataSource = dt;
                grdIPAddlist.DataBind();
            }
            else
            {
                grdIPAddlist.DataSource = null;
                grdIPAddlist.DataBind();
            }
        }

    }
}