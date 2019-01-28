<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="ExcelImport.aspx.cs" Inherits="ENK.ExcelImport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <link type="text/css" rel="stylesheet" href="http://code.jquery.com/ui/1.10.3/themes/smoothness/jquery-ui.css" />
<script type="text/javascript" src="http://code.jquery.com/jquery-1.8.2.js"></script>
<script type="text/javascript" src="http://code.jquery.com/ui/1.10.3/jquery-ui.js"></script>
     <link href="css/plugins/chosen/chosen.css" rel="stylesheet"/>
    <style>
       .chosen-container-single .chosen-single {
    position: relative;
    display: block;
    overflow: hidden;
    padding: 0 0 0 2px;
    height: 34px;
    border: 1px solid #aaa;
    border-radius: 5px;
    background-color: #fff;
    background: -webkit-gradient(linear, 50% 0%, 50% 100%, color-stop(20%, #ffffff), color-stop(50%, #f6f6f6), color-stop(52%, #eeeeee), color-stop(100%, #f4f4f4));
    background: -webkit-linear-gradient(top, #ffffff 20%, #f6f6f6 50%, #eeeeee 52%, #f4f4f4 100%);
    background: -moz-linear-gradient(top, #ffffff 20%, #f6f6f6 50%, #eeeeee 52%, #f4f4f4 100%);
    background: -o-linear-gradient(top, #ffffff 20%, #f6f6f6 50%, #eeeeee 52%, #f4f4f4 100%);
    background: linear-gradient(top, #ffffff 20%, #f6f6f6 50%, #eeeeee 52%, #f4f4f4 100%);
    background-clip: padding-box;
    box-shadow: 0 0 3px white inset, 0 1px 1px rgba(0, 0, 0, 0.1);
    color: #444;
    text-decoration: none;
    white-space: nowrap;
}
    </style>
    
    
    <script type="text/javascript">

        function validateControlsActivate() {
            debugger;
           

            if (document.getElementById('<%=ddlNetwork.ClientID%>').selectedIndex == 0) {
                alert('Please Select  Network');
                document.getElementById("<%=ddlNetwork.ClientID%>").focus();
                return false;
            }

            var validFilesTypes = ["csv"];

            var file = document.getElementById("<%=fuBulkUpload.ClientID%>");
            var path = file.value;
            if (path == "") {

                alert('Please select file for upload');
                return false;
            }

             var ext = path.substring(path.lastIndexOf(".") + 1, path.length).toLowerCase();
             var isValidFile = false;
             for (var i = 0; i < validFilesTypes.length; i++) {
                 if (ext == validFilesTypes[i]) {

                     isValidFile = true;
                     break;
                 }
             }
             if (!isValidFile) {


                 alert("Please upload only .csv file");
                 return false;
             }
             
             ShowProgress();
            return true;
        }

        
       
    
 

        function ShowProgress() {
            $("#popupdiv").dialog({
                title: "",
                width: 430,
                height: 250,
                modal: true,
                buttons: {
                    Close: function () {
                        $(this).dialog('close');
                    }
                }
            });
            $(".ui-dialog-buttonpane").hide();
            $(".ui-dialog-titlebar").hide();
            $(".ui-widget-overlay").css({
                background: "#0F0101 url(images/ui-bg_flat_0_aaaaaa_40x100.png) repeat-x",
                opacity: .3
                /*background: "#454545",
                opacity: ".9 !important",
                filter: "Alpha(Opacity=50)"*/
            });
            return true;
        };

        function HideProgress() {
            setTimeout(function () {
                var modal = $('<div />');
                modal.addClass("modal");
                $('body').append(modal);
                var loading = $(".loading");

                loading.hide();
                var top = Math.max($(window).height() / 2 - loading[0].offsetHeight / 2, 0);
                var left = Math.max($(window).width() / 2 - loading[0].offsetWidth / 2, 0);
                loading.css({ top: top, left: left });
            }, 200);
        }
        //$('form').live("submit", function () {
        //    ShowProgress();
        //});


</script>

    <script type="text/javascript">
        function Search_Gridview(phrase) {
            var words = phrase.value.toLowerCase().split(' ');
            var table = document.getElementById("<%=grdDetails.ClientID %>");
            var ele;
            for (var r = 1; r < table.rows.length; r++) {
                ele = table.rows[r].innerHTML.replace(/<[^>]+>/g, '');
                var displayStyle = 'none';
                for (var i = 0; i < words.length; i++) {
                    if (ele.toLowerCase().indexOf(words[i]) >= 0)
                        displayStyle = '';
                    else {
                        displayStyle = 'none';
                        break;
                    }
                }
                table.rows[r].style.display = displayStyle;
            }
        }
    </script>

    <style type="text/css">
    .modal
    {
       
        top: 0;
        left: 0;        
        z-index: 99;
        opacity: 0.8;
        filter: alpha(opacity=80);
        -moz-opacity: 0.8;
        min-height: 100%;
        width: 100%;
    }
    .loading
    {
        font-family: Arial;
        font-size: 10pt;
        border: 5px solid #67CFF5;
        width: 550px;
        height: 150px;
        display: none;
        position:fixed; 
        background-color: White;
        z-index: 999;
    }
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- MAIN PANEL -->
		<div id="main" role="main" style="margin-top: 0px!important;">

			<!-- RIBBON -->
			 
			<!-- END RIBBON -->

			<!-- MAIN CONTENT -->
			<div id="content">
				<div class="row new-inerpage">
					<div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
						<h1 class="page-title txt-color-blueDark">
							<i class="fa fa-lg fa-fw fa-file-text"></i> 
								Report
							<span>> 
								Bundle Renewal with Distributor Report					</span>						</h1>
					</div>
					<div class="col-xs-12 col-sm-7 col-md-7 col-lg-8">
						
					</div>
				</div>

				<!-- widget grid -->
				<section id="widget-grid" class="">

					<!-- row -->
					<div class="row" >
						<!-- NEW WIDGET START -->
						<article class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
							<!-- Widget ID (each widget will need unique ID)-->
							<div class="jarviswidget jarviswidget-color-blueDark" id="wid-id-0" data-widget-colorbutton="false" data-widget-editbutton="false" data-widget-togglebutton="false" data-widget-deletebutton="false" data-widget-fullscreenbutton="false" data-widget-custombutton="false" data-widget-collapsed="false" data-widget-sortable="false">
								<!-- widget options:
								usage: <div class="jarviswidget" id="wid-id-0" data-widget-editbutton="false">

								data-widget-colorbutton="false"
								data-widget-editbutton="false"
								data-widget-togglebutton="false"
								data-widget-deletebutton="false"
								data-widget-fullscreenbutton="false"
								data-widget-custombutton="false"
								data-widget-collapsed="true"
								data-widget-sortable="false"

								-->
								<header>
									<span class="widget-icon"> <i class="fa fa-table"></i> </span>
									<h2>Bundle Renewal with Distributor Report</h2>
								</header>

								<!-- widget div-->
								<div>
									<!-- widget edit box -->
									<div class="jarviswidget-editbox">
										<!-- This area used as dropdown edit box -->
									</div>
									<!-- end widget edit box -->

									<!-- widget content -->
                                    <div id="login-form" class="smart-form client-form">
                    
                        <div class="box-input">        
                           <div class="row">
                                 
                                  <div id="div1" runat="server" class="col-xs-12 col-sm-12 col-md-12 col-lg-4">                                  
                                <label class="label">Network</label>                                       
                                <label class="input"> 
                                <asp:DropDownList ID="ddlNetwork" class="form-control chosen-select text-area" runat="server" >
                                     
                                </asp:DropDownList>                            
                                </label>                                  
                                
                                    </div>
                                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-4">                                    
                                    <label class="label">File Upload(only .csv file allow)</label>                                          
                                    <label class="input">  
                                       <asp:FileUpload ID="fuBulkUpload" runat="server" />    
                                    </label>                                  
                                </div>
                                
                                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-3">
                                  <label class="label">&nbsp;</label>
                                   <asp:Button ID="btnExportToExcel" runat="server" Text="Export To Excel"    OnClick="btnExportToExcel_Click" class="btn btn-primary pull-right"  style="margin-left: 13px;/* height:20px; *//* width:150px; */"   />    
                                      <asp:Button ID="btnUploadFile" runat="server" OnClick="btnUploadFile_Click" Text="Import Data"  class="btn btn-primary pull-right"  OnClientClick="javascript:return validateControlsActivate();" />                               
                                      
                                </div>

                                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-3">

                                    
                                            
                                </div>
                                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-2">
                                </div>
                                                 
                            </div>  
                            
                           <%-- <div class="row" id="DivMobile" runat="server">
                                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12"> 
                                       <label class="label">&nbsp;</label> 
                                      
                                     <div class="table-responsive" style="max-height: 237px; overflow-y: auto;" >
                                       
                                        <asp:Repeater ID="RepeaterTransfer" runat="server" >
                                         <HeaderTemplate >
                                              <table class="table table-bordered"  id="dataTables-example">
                                                <thead>
                                                    <tr>
                                                        
                                                        <th>SIM Status</th>
                                                        <th>Mobile Number</th>
                                                        <th>SIM Serial Number</th>
                                                        <th>Plan</th>
                                                        <th>Current Distributor</th>
                                                        <th>Transfer From</th>
                                                        <th>Transfer To</th>
                                                        <th>ActivationDate</th>
                                                         
                                                    </tr> 
                                                <tbody>             
                                              </HeaderTemplate>
                                       
                                              <ItemTemplate> 
                                                <tr>
                                                    <td>
                                                    <asp:Label runat="server" ID="Label7" text='<%# Eval("SIMStatus") %>' />
                                                    </td>
                                                    <td>
                                                    <asp:Label runat="server" ID="Label1" text='<%# Eval("MobileNumber") %>' />
                                                    </td>  
                                                    <td>
                                                    <asp:Label runat="server" ID="Label3" text='<%# Eval("SIMSerialNumber") %>' />
                                                    </td>
                                                    <td>
                                                    <asp:Label runat="server" ID="Label2" text='<%# Eval("Plan") %>' />
                                                    </td>
                                                    <td>
                                                    <asp:Label runat="server" ID="Label4" text='<%# Eval("CurrentDistributor") %>' />
                                                    </td> 
                                                    <td>
                                                    <asp:Label runat="server" ID="Label5" text='<%# Eval("TransferFrom") %>' />
                                                    </td>
                                                    <td>
                                                    <asp:Label runat="server" ID="Label6" text='<%# Eval("TransferTo") %>' />
                                                    </td> 
                                                    <td>
                                                    <asp:Label runat="server" ID="Label8" text='<%# Eval("ActivationDate") %>' />
                                                    </td>
                                                     

                                                </tr>          
                                              </ItemTemplate>  
                                            
                                              <FooterTemplate>
                                               </thead> 
                                               </table>
                                               </tbody>
                                               
                                              </FooterTemplate>
                                    </asp:Repeater>
                                       </div>  
                                     </div>
                            </div>--%>

                           
                            
                        </div>
                
                        <!-- start footer -->
                        <!-- end footer -->
                </div>
									<!-- end widget content -->
								</div>
								<!-- end widget div -->
							</div>
							<!-- end widget -->
						</article>
						<!-- WIDGET END -->
					</div>
                      <asp:TextBox ID="txtSearchgv" runat="server" placeholder="Search Text..." CssClass="form-control"
                                onkeyup="Search_Gridview(this)"></asp:TextBox>
                    <div style="width: 1080px; overflow: auto;height: 400px;">
                        <asp:GridView ID="grdDetails" runat="server" AutoGenerateColumns="true"  RowStyle-Wrap="false" CssClass="table table-striped table-bordered table-hover"
                                                   Width="100%" HeaderStyle-BackColor="#B01116" AllowSorting="false"
                                                GridLines="Both" HeaderStyle-Font-Bold="true" CellPadding="0" CellSpacing="0" >
                                                <RowStyle Height="" HorizontalAlign="left" Wrap="true" />
                        </asp:GridView>
                             
                    </div>
					<!-- end row -->
				</section>
				<!-- end widget grid -->
			</div>
			<!-- END MAIN CONTENT -->
		</div>
		<!-- END MAIN PANEL -->
    <div id="popupdiv" class="loading" title="Basic modal dialog" style="display:none;font-family:Arial;font-size: 10pt; border: 2px solid #67CFF5;text-align: -webkit-center;">
        
      <b style="text-align:center!important;"> Please Wait...........</b>
        <br />
        <br />
        <img src="img/loader.gif" alt="" style="text-align:center!important;" />
    </div>
      <script src="js/jquery-2.1.1.js"></script>
    <script src="js/plugin/chosen/chosen.jquery.js"></script> 

    <script type="text/javascript">



        var config = {
            '.chosen-select': {},
            '.chosen-select-deselect': { allow_single_deselect: true },
            '.chosen-select-no-single': { disable_search_threshold: 10 },
            '.chosen-select-no-results': { no_results_text: 'Oops, nothing found!' },
            '.chosen-select-width': { width: "100%" }
        }
        for (var selector in config) {
            $(selector).chosen(config[selector]);
        }
    </script>
</asp:Content>
