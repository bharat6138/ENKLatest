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
using System.IO;
using System.Xml.Linq;


namespace ENK
{
    public partial class ExcelImport : System.Web.UI.Page
    {
        Service1Client ssc = new Service1Client();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                try
                {
                    if (Session["LoginID"] != null)
                    {
                        ////////////////////////////
                        DataTable dt = ssc.GetVendor(Convert.ToInt32(Session["LoginId"]));
                        if (dt != null)
                        {
                            if (dt.Rows.Count > 0)
                            {
                                ddlNetwork.DataSource = dt;
                                ddlNetwork.DataValueField = "VendorID";
                                ddlNetwork.DataTextField = "VendorName";
                                ddlNetwork.DataBind();
                                ddlNetwork.SelectedIndex = 1;

                            }
                        }
                        ////////////////////////////

                        //DataTable dt = ssc.GetVendor(Convert.ToInt32(Session["LoginId"]));
                        //if (dt != null)
                        //{
                        //    if (dt.Rows.Count > 0)
                        //    {
                        //        ddlNetwork.DataSource = dt;
                        //        ddlNetwork.DataValueField = "VendorID";
                        //        ddlNetwork.DataTextField = "VendorName";
                        //        ddlNetwork.DataBind();
                        //        ddlNetwork.Items.Insert(0, new ListItem("---Select Network---", "0"));
                        //    }
                        //}
                    }
                }
                catch { }
            }
        }

        protected void btnUploadFile_Click(object sender, EventArgs e)
        {
            if (fuBulkUpload.HasFile)
            {
                if (fuBulkUpload.FileName.Contains(".csv"))
                {
                }
                else { ScriptManager.RegisterStartupScript(this, Page.GetType(), "File Column Mismatch1", "alert('Please upload only .csv file')", true);
                }
                
                   
                
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, Page.GetType(), "File Column Mismatch2", "alert('Please select file for upload')", true);
            }

            
            ViewState["objDT"] = null;
            //fillblankGridForMobileSIM();
            //fillblankGridForSIM();

            FileUpload fuBulk = null;
            fuBulk = fuBulkUpload;
            if (ddlNetwork.SelectedItem.Text == "Lyca Mobile")
            {
                UploadFile(fuBulk);
            }
            else if (ddlNetwork.SelectedItem.Text == "EasyGo" || ddlNetwork.SelectedItem.Text == "H20")
            {
                UploadFileH2OANDEasygo(fuBulk);
            }
        }

        protected void UploadFileH2OANDEasygo(FileUpload FuBulkDetails)
        {
            try
            {
                string fileNametext = string.Empty;
                if (FuBulkDetails.HasFile)
                {
                    if (FuBulkDetails.FileName.Contains(".csv"))
                    {
                        fileNametext = FuBulkDetails.FileName;
                        string strPath = Server.MapPath("InventoryFiles") + "/" + FuBulkDetails.FileName;
                        FuBulkDetails.SaveAs(strPath);
                        ViewState["FileImport"] = strPath;

                        //-----Datatable for imported files complete data
                        ViewState["objDT"] = CSVTODataTable(strPath, (DataTable)ViewState["objDT"]);
                        DataTable dt = (DataTable)ViewState["objDT"];

                        //
                        bool iscolExists = false;
                        foreach (DataColumn dcol in dt.Columns)
                        {
                            if (dcol.ColumnName.ToUpper() == "ICCID ON WHICH THAT BUNDLE ACTIVATED")
                            {
                                iscolExists = true;
                                break;
                            }
                        }
                        if (!iscolExists)
                            dt.Columns.Add(new DataColumn("ICCID ON WHICH THAT BUNDLE ACTIVATED"));
                        //

                        //------Datatable for get mapped details from DB
                        DataTable objDt = new DataTable();
                        objDt.TableName = "tDtSIMBulkTransfer";
                        objDt.Columns.Add("ICCID ON WHICH THAT BUNDLE ACTIVATED");
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            string ICCIDnumber = string.Empty, iccidprefix = string.Empty;
                            //iccidprefix = Convert.ToString(dt.Rows[i]["iccidPREFIX"]).ToUpper();
                            //if (!iscolExists)
                            //    ICCIDnumber = (((iccidprefix == string.Empty || iccidprefix=="NULL") ? "8919601" : Convert.ToString(dt.Rows[i]["iccidPREFIX"])) 
                            //        + Convert.ToString(dt.Rows[i]["iccid"]));
                            //else

                            iccidprefix = Convert.ToString(dt.Rows[i]["Sim Number"]).ToUpper();
                            ICCIDnumber = (Convert.ToString(dt.Rows[i]["Sim Number"]));
                            ICCIDnumber = ICCIDnumber.Replace("'", "");
                            dt.Rows[i]["ICCID ON WHICH THAT BUNDLE ACTIVATED"] = ICCIDnumber;

                            DataRow objDr = objDt.NewRow();
                            objDr["ICCID ON WHICH THAT BUNDLE ACTIVATED"] = ICCIDnumber;
                            objDt.Rows.Add(objDr);

                            //DataRow objDr = objDt.NewRow();
                            //objDr["ICCID ON WHICH THAT BUNDLE ACTIVATED"] = Convert.ToString(dt.Rows[i]["ICCID ON WHICH THAT BUNDLE ACTIVATED"]);
                            //objDt.Rows.Add(objDr);
                        }
                        objDt.AcceptChanges();


                        string s1 = "";
                        DataView dv1 = objDt.DefaultView;
                        dv1.RowFilter = "[ICCID ON WHICH THAT BUNDLE ACTIVATED]<>'" + s1 + "'";
                        objDt = dv1.ToTable();

                        string str = "<Table>";
                        foreach (DataRow dr in objDt.Rows)
                        {
                            str = str + "<Row><SimNo>" + dr["ICCID ON WHICH THAT BUNDLE ACTIVATED"] + "</SimNo></Row>";
                        }
                        str = str + "</Table>";


                        DataSet ds = ssc.GetImportFile(str);
                        //  GetImportFileDetailsService(objDt, "Import")  =>Error :-The remote server returned an error: (413) Request Entity Too Large
                      
                        // ds = ssc.GetImportFileDetailsService(objDt, "Import");
                        if (ds != null)
                        {
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                DataTable FileObjectImportedTable = (DataTable)(ViewState["objDT"]);
                                string s = "";

                                DataView dv = FileObjectImportedTable.DefaultView;
                                dv.RowFilter = "[Sim Number]<>'" + s+"'";
                                FileObjectImportedTable = dv.ToTable();
                                DataTable ICCIDMappingTable = ds.Tables[0];

                                DataTable FinalMappedTable = DataTableJoinHelper.JoinTwoDataTablesOnOneColumn(FileObjectImportedTable, ICCIDMappingTable,
                                    "ICCID ON WHICH THAT BUNDLE ACTIVATED", DataTableJoinHelper.JoinType.Left);
                                if (FinalMappedTable != null)
                                {
                                    //grdDetails.DataSource = FinalMappedTable;
                                    //grdDetails.DataBind();

                                    //DataView view = new DataView(FinalMappedTable);

                                    //DataTable dtExcel = view.ToTable();
                                    FinalMappedTable.Columns.Remove("ICCID ON WHICH THAT BUNDLE ACTIVATED");
                                    FinalMappedTable.AcceptChanges();

                                    if (FinalMappedTable.Rows.Count > 0)
                                    {
                                        ViewState["FinalMappedTable"] = FinalMappedTable;
                                        grdDetails.DataSource = FinalMappedTable;
                                        grdDetails.DataBind();

                                        // -------------shadab Ali--------------- due to Export Button

                                        //string filename = fileNametext + "_ReportWithDistributor" +
                                        //    "_"+DateTime.UtcNow.ToString("ddMMyyyyHHmm")+ ".xls";
                                        //System.IO.StringWriter tw = new System.IO.StringWriter();
                                        //System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);
                                        //GridView grdView = new GridView();
                                        ////dgGrid.HeaderStyle
                                        //grdView.DataSource = FinalMappedTable;
                                        //grdView.DataBind();
                                        //ScriptManager.RegisterStartupScript(this, GetType(), "", "HideProgress();", true);
                                        ////Get the HTML for the control.
                                        //grdView.RenderControl(hw);
                                        ////Write the HTML back to the browser.
                                        ////Response.ContentType = application/vnd.ms-excel;
                                        //Response.ContentType = "application/vnd.ms-excel";
                                        //Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename + "");
                                        //this.EnableViewState = false;
                                        //Response.Write(tw.ToString());
                                        // Response.End();
                                        // ScriptManager.RegisterStartupScript(this, GetType(), "", "HideProgress();", true);


                                    }


                                }
                            }
                        }
                    }
                    else
                        ScriptManager.RegisterStartupScript(this, Page.GetType(), "File Column Mismatch1", "alert('Please upload only .csv file')", true);
                }
                else
                    ScriptManager.RegisterStartupScript(this, Page.GetType(), "File Column Mismatch2", "alert('Please select file for upload')", true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                ViewState["FileImport"] = null;
                ViewState["objDT"] = null;
            }
        }





        protected void UploadFile(FileUpload FuBulkDetails)
        {
            try
            {
                string fileNametext = string.Empty;
                if (FuBulkDetails.HasFile)
                {
                    if (FuBulkDetails.FileName.Contains(".csv"))
                    {
                        fileNametext = FuBulkDetails.FileName;
                        string strPath = Server.MapPath("InventoryFiles") + "/" + FuBulkDetails.FileName;
                        FuBulkDetails.SaveAs(strPath);
                        ViewState["FileImport"] = strPath;

                        //-----Datatable for imported files complete data
                        ViewState["objDT"] = CSVTODataTable(strPath, (DataTable)ViewState["objDT"]);
                        DataTable dt = (DataTable)ViewState["objDT"];

                        //
                        bool iscolExists = false;
                        foreach (DataColumn dcol in dt.Columns)
                        {
                            if (dcol.ColumnName.ToUpper() == "ICCID ON WHICH THAT BUNDLE ACTIVATED")
                            {
                                iscolExists = true;
                                break;
                            }
                        }
                        if (!iscolExists)
                            dt.Columns.Add(new DataColumn("ICCID ON WHICH THAT BUNDLE ACTIVATED"));
                        //

                        //------Datatable for get mapped details from DB
                        DataTable objDt = new DataTable();
                        objDt.TableName = "tDtSIMBulkTransfer";
                        objDt.Columns.Add("ICCID ON WHICH THAT BUNDLE ACTIVATED");
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            string ICCIDnumber=string.Empty,iccidprefix=string.Empty;
                            iccidprefix = Convert.ToString(dt.Rows[i]["iccidPREFIX"]).ToUpper();
                            if (!iscolExists)
                                ICCIDnumber = (((iccidprefix == string.Empty || iccidprefix == "NULL") ? "8919601" : Convert.ToString(dt.Rows[i]["iccidPREFIX"]))
                                    + Convert.ToString(dt.Rows[i]["iccid"]));
                            else
                             
                              
                            dt.Rows[i]["ICCID ON WHICH THAT BUNDLE ACTIVATED"] = ICCIDnumber;

                            DataRow objDr = objDt.NewRow();
                            objDr["ICCID ON WHICH THAT BUNDLE ACTIVATED"] = ICCIDnumber;
                            objDt.Rows.Add(objDr);

                            //DataRow objDr = objDt.NewRow();
                            //objDr["ICCID ON WHICH THAT BUNDLE ACTIVATED"] = Convert.ToString(dt.Rows[i]["ICCID ON WHICH THAT BUNDLE ACTIVATED"]);
                            //objDt.Rows.Add(objDr);
                        }
                        objDt.AcceptChanges();

                        DataSet ds = ssc.GetImportFileDetailsService(objDt, "Import");
                        if (ds != null)
                        {
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                DataTable FileObjectImportedTable = (DataTable)(ViewState["objDT"]);
                                DataTable ICCIDMappingTable = ds.Tables[0];

                                DataTable FinalMappedTable = DataTableJoinHelper.JoinTwoDataTablesOnOneColumn(FileObjectImportedTable, ICCIDMappingTable,
                                    "ICCID ON WHICH THAT BUNDLE ACTIVATED", DataTableJoinHelper.JoinType.Left);
                                if (FinalMappedTable != null)
                                {
                                    //grdDetails.DataSource = FinalMappedTable;
                                    //grdDetails.DataBind();
                                    
                                    //DataView view = new DataView(FinalMappedTable);

                                    //DataTable dtExcel = view.ToTable();
                                    FinalMappedTable.Columns.Remove("ICCID ON WHICH THAT BUNDLE ACTIVATED");
                                    FinalMappedTable.AcceptChanges();

                                    if (FinalMappedTable.Rows.Count > 0)
                                    {
                                        ViewState["FinalMappedTable"] = FinalMappedTable;
                                        grdDetails.DataSource = FinalMappedTable;
                                        grdDetails.DataBind();
                                        
                                        // -------------shadab Ali--------------- due to Export Button

                                        //string filename = fileNametext + "_ReportWithDistributor" +
                                        //    "_"+DateTime.UtcNow.ToString("ddMMyyyyHHmm")+ ".xls";
                                        //System.IO.StringWriter tw = new System.IO.StringWriter();
                                        //System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);
                                        //GridView grdView = new GridView();
                                        ////dgGrid.HeaderStyle
                                        //grdView.DataSource = FinalMappedTable;
                                        //grdView.DataBind();
                                        //ScriptManager.RegisterStartupScript(this, GetType(), "", "HideProgress();", true);
                                        ////Get the HTML for the control.
                                        //grdView.RenderControl(hw);
                                        ////Write the HTML back to the browser.
                                        ////Response.ContentType = application/vnd.ms-excel;
                                        //Response.ContentType = "application/vnd.ms-excel";
                                        //Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename + "");
                                        //this.EnableViewState = false;
                                        //Response.Write(tw.ToString());
                                        // Response.End();
                                        // ScriptManager.RegisterStartupScript(this, GetType(), "", "HideProgress();", true);

                                       

                                    }

                    
                                }
                            }
                        }
                    }
                    else
                        ScriptManager.RegisterStartupScript(this, Page.GetType(), "File Column Mismatch1", "alert('Please upload only .csv file')", true);
                }
                else
                    ScriptManager.RegisterStartupScript(this, Page.GetType(), "File Column Mismatch2", "alert('Please select file for upload')", true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                ViewState["FileImport"] = null;
                ViewState["objDT"] = null;
            }
        }

        private DataTable CSVTODataTable(string CSVPath, DataTable table)
        {
            try
            {
                int iInvalidFileFormat = 0;
                StreamReader DataStreamReader = new StreamReader(CSVPath);
                table = new DataTable("FileImport");
                try
                {
                    string[] columnNames = DataStreamReader.ReadLine().Split(new string[] { "," }, StringSplitOptions.None);

                    DataColumn column = null;
                    for (int i = 0; i < columnNames.Length; i++)
                    {
                        column = new DataColumn(columnNames[i]);
                        //string column1 = column.ToString();
                        //string s = column1.Replace("\"", "");
                        table.Columns.Add(column);
                    }

                    while (!DataStreamReader.EndOfStream)
                    {
                        int iRowIndex = table.Rows.Count;
                        //if (columnNames.Length != 5)
                        //{
                        //    //lblMsg.Text = "Some column mistmatch in mobile bulk upload file";
                        //    iInvalidFileFormat = 1;
                        //    ScriptManager.RegisterStartupScript(this, Page.GetType(), "File Column Mismatch", "alert('Mobile SIM bulk upload File should contains 5 columns,please check your file')", true);
                        //    break;
                        //}
                        string streamtext = DataStreamReader.ReadLine();
                        if (streamtext == null)
                            continue;
                        if (streamtext.Split(new string[] { "," }, StringSplitOptions.None).Length > 10)
                            continue;

                        table.Rows.Add(streamtext.Split(new string[] { "," }, StringSplitOptions.None));

                        //if (table.Rows[iRowIndex]["SIMType"].ToString() == "Normal SIM")
                        //{
                        //    table.Rows[iRowIndex]["SIMTypeID"] = 12;
                        //}
                        //if (table.Rows[iRowIndex]["SIMType"].ToString() == "Micro SIM")
                        //{
                        //    table.Rows[iRowIndex]["SIMTypeID"] = 13;
                        //}
                        //if (table.Rows[iRowIndex]["SIMType"].ToString() == "Nano SIM")
                        //{
                        //    table.Rows[iRowIndex]["SIMTypeID"] = 14;
                        //}
                        //table.Rows[iRowIndex]["SIMTypeID"] = ddlSIMType_SIM.SelectedValue;

                        if (iInvalidFileFormat == 1)
                        {
                            return null;
                        }
                        table.AcceptChanges();

                    }
                    DataStreamReader.Close();

                    return table;
                }
                catch
                {
                    if (DataStreamReader!=null)
                        DataStreamReader.Close();

                    throw;
                }                
            }
            catch (Exception ex)
            {
                throw ex;
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
        protected void btnExportToExcel_Click(object sender, EventArgs e)
        {
            //ShowReport();

            if (grdDetails.Rows.Count > 0)
            {
                DataTable dt = (DataTable)ViewState["FinalMappedTable"];


                foreach (DataRow row in dt.Rows)
                {
                    string simnumber = Convert.ToString(row["Sim Number"]);
                    simnumber = "'" + simnumber + "'";
                    row["Sim Number"] = simnumber;
                    row.EndEdit();
                    dt.AcceptChanges();

                }





                if (dt.Rows.Count > 0)
                {
                   
                    string filename = ddlNetwork.SelectedItem.Text + "_ReportWithDistributor" +
                        "_" + DateTime.UtcNow.ToString("ddMMyyyyHHmm") + ".xls";
                    System.IO.StringWriter tw = new System.IO.StringWriter();
                    System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);
                    GridView grdView = new GridView();
                    //dgGrid.HeaderStyle
                    grdView.DataSource = dt;
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
        }

        private void ShowReport()
        {
            try
            {
                if (grdDetails.Rows.Count > 0)
                {
                    DataTable dt = (DataTable)ViewState["objDT"];

                    //------Datatable for get mapped details from DB
                    DataTable objDt = new DataTable();
                    objDt.TableName = "tDtSIMBulkTransfer";
                    objDt.Columns.Add("ICCID ON WHICH THAT BUNDLE ACTIVATED");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataRow objDr = objDt.NewRow();
                        objDr["ICCID ON WHICH THAT BUNDLE ACTIVATED"] = Convert.ToString(dt.Rows[i]["ICCID ON WHICH THAT BUNDLE ACTIVATED"]);
                        objDt.Rows.Add(objDr);
                    }
                    objDt.AcceptChanges();

                    DataSet ds = ssc.GetImportFileDetailsService(objDt, "Import");
                    if (ds != null)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            DataTable FileObjectImportedTable = (DataTable)(ViewState["objDT"]);
                            DataTable ICCIDMappingTable = ds.Tables[0];

                            DataTable FinalMappedTable = DataTableJoinHelper.JoinTwoDataTablesOnOneColumn(FileObjectImportedTable, ICCIDMappingTable,
                                "ICCID ON WHICH THAT BUNDLE ACTIVATED", DataTableJoinHelper.JoinType.Left);
                            if (FinalMappedTable != null)
                            {
                                DataView view = new DataView(FinalMappedTable);

                                DataTable dtExcel = view.ToTable();

                                if (dtExcel.Rows.Count > 0)
                                {
                                    string filename = " FileWithDistributor.xls";
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
                        }
                    }
                }
                else
                {
                    ShowPopUpMsg("There is no row to export to excel!!");
                    return;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        
        
        }

        
    }
}