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
using System.IO;

namespace ENK
{
    public partial class Banner : System.Web.UI.Page
    {
        Service1Client svc = new Service1Client();
       // private object priority;
        //string dir="~/D:/Backup/3/New folder (2)/ENK_HOST (16)/ENK/Image";
        string dir = "~/Image";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int ImgFlag = Convert.ToInt32(Request.QueryString["ImgFlag"]);
                Session["ImgFlag"] = Convert.ToInt32(Request.QueryString["ImgFlag"]);
                getImage(ImgFlag);
            }
        }

        private void getImage(int ImgFlag)
        {



            try
            {
                //int login = Convert.ToInt32(Session["LoginID"]);

                //// int DistributorID = Convert.ToInt32(Session["Url"]);
                DataSet dst = svc.GetImageUrl(ImgFlag);
                if (dst != null)
                {
                    grdImage.DataSource = dst.Tables[0];
                    grdImage.DataBind();
                }


            }
            catch (Exception ex)
            {

            }
        }

        protected void UploadButton_Click(object sender, EventArgs e)
        {
            try
            {
                string Url = "";

                if (FileUploadControl.HasFile)
                {

                    string Exten = Path.GetExtension(FileUploadControl.FileName);//image uploads
                    // string img1 = (DomainName);
                    Url = "~/Image/" + FileUploadControl.FileName;
                    FileUploadControl.SaveAs(Server.MapPath(Url));




                    string fileName = FileUploadControl.FileName;
                    Url = fileName;
                    int ImgFlag = Convert.ToInt32(Session["ImgFlag"]);

                    int a = 0;
                    a = svc.pGetImeurl(Url, ImgFlag);
                    if (a > 0)
                    {
                        getImage(Convert.ToInt32(Session["ImgFlag"]));
                        ShowPopUpMsg("Image Saved.");


                    }
                    else
                    {
                        ShowPopUpMsg("Image Not Save");
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

        protected void btnADDNewRowForSIM_Click(object sender, EventArgs e)
        {
            try
            {

                if (ViewState["objDTBanner"] == null)
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add("filename");
                    ViewState["objDTBanner"] = dt;
                }

                DataTable objDt = (DataTable)ViewState["objDTBanner"];

                DataRow objDr = objDt.NewRow();
                string Exten = Path.GetExtension(FileUploadControl.FileName);//image uploads
                //Url = "~/Image/" + FileUploadControl.FileName;
                //FileUploadControl.SaveAs(Server.MapPath(Url));
               string filename =FileUploadControl.FileName;

                objDr["filename"] = "h";

                objDt.Rows.Add(objDr);
                objDt.AcceptChanges();
                ViewState["objDTBanner"] = objDt;
                grdImage.DataSource = ViewState["objDTBanner"];
                grdImage.DataBind();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //protected void RepeaterBanner_ItemCommand(object source, RepeaterCommandEventArgs e)
        //{
        //    try
        //    {
        //        if (e.CommandName == "Delete")
        //        {
        //            if (ViewState["objDTBanner"] != null)
        //            {
        //                DataTable dt = (DataTable)ViewState["objDTBanner"];
        //                DataRow drCurrentRow = null;
        //                int rowIndex = Convert.ToInt32(e.Item.ItemIndex);
        //                if (dt.Rows.Count > 1)
        //                {
        //                    dt.Rows.Remove(dt.Rows[rowIndex]);
        //                    drCurrentRow = dt.NewRow();
        //                    ViewState["objDTBanner"] = dt;
        //                    RepeaterBanner.DataSource = dt;
        //                    RepeaterBanner.DataBind();

        //                }
        //                else if (dt.Rows.Count == 1)
        //                {
        //                    dt.Rows.Remove(dt.Rows[rowIndex]);
        //                    //fillblankGridForSIM();
        //                }
        //            }

        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}

 
        protected void grdImage_RowDataBound(object sender, GridViewRowEventArgs e)
        {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {

                    Label lblPhotoId = (Label)e.Row.FindControl("lblId");

                    Image imgPhoto = (Image)e.Row.FindControl("Img");

                    //string Photo = lblPhotoId.Text;

                    string Photo = imgPhoto.ImageUrl;

                    imgPhoto.ImageUrl = dir + "/" + Photo;

                }
            }

        protected void grdImage_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            Label lid = (Label)grdImage.Rows[e.RowIndex].FindControl("lblId");
            int ImageID = Convert.ToInt32(lid.Text);

            DataSet dst = svc.DeleteImage(ImageID);
            if (dst != null)
            {
                getImage(Convert.ToInt32(Session["ImgFlag"]));
                ShowPopUpMsg("Image Delete Successfully.");
            }
            else
            {
                ShowPopUpMsg("Image Not Delete.");
            }
        }

        protected void grdImage_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grdImage.EditIndex = -1;
            getImage(Convert.ToInt32(Session["ImgFlag"]));
        }

        protected void grdImage_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grdImage.EditIndex = e.NewEditIndex;
            getImage(Convert.ToInt32(Session["ImgFlag"]));
        }

        protected void grdImage_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            Label lblID = (Label)grdImage.Rows[e.RowIndex].FindControl("lblId");
            Label lblPosition = (Label)grdImage.Rows[e.RowIndex].FindControl("lblPosition");
            TextBox txtPosition = (TextBox)grdImage.Rows[e.RowIndex].FindControl("txtPosition");
            int ImageID = Convert.ToInt32(lblID.Text);
            int Position = Convert.ToInt32(txtPosition.Text);

            int a = 0;
            DataSet dst = svc.pUpdateImagePosition(ImageID, Position);
            if (dst != null)
            {
                ShowPopUpMsg("Information Updated.");
                grdImage.EditIndex = -1;
                getImage(Convert.ToInt32(Session["ImgFlag"]));
                


            }
            else
            {
                ShowPopUpMsg("Information not Update");
            }

        }
        }
    }


        
  