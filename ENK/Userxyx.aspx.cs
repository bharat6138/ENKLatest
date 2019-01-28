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
    public partial class Userxyx : System.Web.UI.Page
    {

        Service1Client svc = new Service1Client();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {                   
                    int userid = 0;
                    int DistributorID = 1;
                    DataSet ds = svc.GetUserListService(userid, DistributorID);
                     
                    DataTable dt = new DataTable();
                    dt.Columns.Add("Username");
                    dt.Columns.Add("Pass");

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        string uname = "";
                        string pass = "";
                        uname = ds.Tables[0].Rows[i]["UserName"].ToString();
                        pass = ds.Tables[0].Rows[i]["Password"].ToString();
                        pass = Encryption.Decrypt(pass);

                        DataRow dr = dt.NewRow();
                        dr["Username"] = uname;
                        dr["Pass"] = pass;

                        dt.Rows.Add(dr);
                        dt.AcceptChanges();


                    }

                    if (ds != null)
                    {
                        RepeaterUserList.DataSource = dt;
                        RepeaterUserList.DataBind();
                    }
                }
                catch (Exception ex)
                {

                }
            }
        }

        
    }
}