using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ENK.ServiceReference1;

namespace ENK
{
    public partial class ApiMapping : System.Web.UI.Page
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
                }
                else
                {
                    Identity = "";
                }
                APIDetails("5516");
            }
        }
        protected void APIDetails(string id)
        {
            Int64 DistributorID = Convert.ToInt64(id);
            DataSet ds = new DataSet();
            ds = ssc.GetApiDetail(DistributorID);
            if (ds.Tables.Count > 0)
            {

                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["ClientCode"].ToString()))
                    {
                        txtClientCode.Text = ds.Tables[0].Rows[0]["ClientCode"].ToString();
                    }
                    
                    if (ds.Tables[2].Rows.Count > 0)
                    {
                        grdAPIMapping.DataSource = ds.Tables[2];
                        grdAPIMapping.DataBind();
                    }
                    if (Convert.ToString(ds.Tables[0].Rows[0]["ApiStatus"]) == "True")
                    {
                        chkOnOff.Checked = true;
                        grdAPIMapping.Visible = true;

                        if (ds.Tables[1].Rows.Count > 0)
                        {
                            for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                            {
                                for (int j = 0; j < grdAPIMapping.Rows.Count; j++)
                                {
                                    HiddenField hndApiID = (HiddenField)grdAPIMapping.Rows[j].FindControl("hddnApiID");
                                    if (Convert.ToString(ds.Tables[1].Rows[i]["ApiId"]) == Convert.ToString(hndApiID.Value))
                                    {
                                        CheckBox CheckAPI = (CheckBox)grdAPIMapping.Rows[j].FindControl("chkAPI");

                                        CheckAPI.Checked = true;

                                    }
                                }
                            }
                        }
                    }

                    else
                    {
                        chkOnOff.Checked = false;
                        grdAPIMapping.Visible = false;
                    }
                }

                else
                {

                }
            }
        }

        protected void btnApiSave_Click(object sender, EventArgs e)
        {
            Int64 distributorid = Convert.ToInt64(Session["DistributorID"]);
            int clientcode = 0;
            int chkApi = 0;
            if (txtClientCode.Text != string.Empty)
            {
                clientcode = Convert.ToInt32(txtClientCode.Text.Trim());
            }
            DataTable dt = new DataTable();
            dt.TableName = "dtAPI";
            dt.Columns.Add("ApiId");
            dt.Columns.Add("DistributorID");
            //dt.Columns.Add("isActive");

            if (chkOnOff.Checked == true)
            {
                chkApi = 1;
            }


            for (int i = 0; i < grdAPIMapping.Rows.Count; i++)
            {
                HiddenField hddnApiId = (HiddenField)grdAPIMapping.Rows[i].FindControl("hddnApiID");
                CheckBox CheckAPI = (CheckBox)grdAPIMapping.Rows[i].FindControl("chkAPI");

                if (CheckAPI.Checked == true)
                {
                    DataRow dr = dt.NewRow();
                    dr["APIId"] = Convert.ToInt16(hddnApiId.Value);
                    dr["DistributorID"] = Convert.ToInt16(distributorid);
                    dt.Rows.Add(dr);
                }
                dt.AcceptChanges();
            }

            int a = 0;

            a = ssc.SaveApiMapping(distributorid, clientcode, dt, chkApi); // save/update same Method
            if (a > 0)
            {
                Response.Write("<script>alert('Mapped successfully');</script>");
            }
            else
            {
                Response.Write("<script>alert('Unsuccessfull Mapping');</script>");
            }

        }
    }
}