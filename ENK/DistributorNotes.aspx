

<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="DistributorNotes.aspx.cs" Inherits="ENK.DistributorNotes" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link type="text/css" rel="stylesheet" href="http://code.jquery.com/ui/1.10.3/themes/smoothness/jquery-ui.css" />
<script type="text/javascript" src="http://code.jquery.com/jquery-1.8.2.js"></script>
<script type="text/javascript" src="http://code.jquery.com/ui/1.10.3/jquery-ui.js"></script>









    <script type="text/javascript">
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

      <script type="text/javascript">

          function validateControlsActivatePort() {

              if (document.getElementById('<%=ddlDistributor.ClientID%>').selectedIndex == 0) {
                  alert('Please Select  Distributor');
                document.getElementById("<%=ddlDistributor.ClientID%>").focus();
                return false;
            }

            if (document.getElementById("<%=txtNotes.ClientID%>").value == "") {
                alert("Notes Can't be Blank");
                document.getElementById("<%=txtNotes.ClientID%>").focus();
                return false;
            }

            
            
            return true;
          }

          function JScriptConfirmationUpdatePopupBox() {
              alert("Distributor Notes Saved Successfully.");
              window.location.href = "NotesReport.aspx";
          }

 </script>
    




    <link href="css/plugins/chosen/chosen.css" rel="stylesheet"/>
    <style>
       .chosen-container-single .chosen-single {
    position: relative;
    display: block;
    overflow: hidden;
    padding: 0 0 0 8px;
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
    line-height: 24px;
}
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- MAIN PANEL -->
		<div id="main" role="main" style="margin-top: 0px!important;">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
			<!-- RIBBON -->
			 
			<!-- END RIBBON -->

			<!-- MAIN CONTENT -->
			<div id="content">

				<div class="row new-inerpage">
					<div class="col-xs-12 col-sm-5 col-md-5 col-lg-4">
						<h1 class="page-title txt-color-blueDark">
							<i class="fa fa-lg fa-fw fa-file-text"></i> 
								 
							<span>> 
								New Notes			</span>						</h1>
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
									<h2>
							<span>New Notes			</span>						&nbsp;</h2>
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
                    
                       
                                       <div class="row">


                                <div class="col-xs-12 col-md-4" style ="padding-left: 18px">                                    
                                    <label class="label">Distributor</label>                                       
                                    <label class="input">  
                                        <asp:DropDownList  class="chosen-select text-area" ID="ddlDistributor" runat="server" AutoPostBack="false">
                                             <%--<asp:ListItem Value="1" Text="-Select-"></asp:ListItem>
                                            <asp:ListItem Value="1" Text="Mobile SIM"></asp:ListItem>
                                            <asp:ListItem Value="2" Text="Blank SIM"></asp:ListItem>--%>
                                        </asp:DropDownList> 
                                    </label>                                  
                                </div>
                                 <div class="col-xs-12 col-md-4">
                               </div>
                                   <div class="col-xs-12 col-md-4">
                               </div>              
                          </div>
                            
                              
                                 <div class="row">
                                <div class="col-xs-12 col-md-4" style ="padding-left: 18px">                                    
                                    <label class="label">Notes</label>                                       
                                    <label class="input">  
                                        <asp:TextBox ID="txtNotes" CssClass="form-control" TextMode="MultiLine" Rows="5" runat="server"></asp:TextBox>
                                    </label>                                  
                               </div>
                                 <div class="col-xs-12 col-md-4">
                               </div>
                                   <div class="col-xs-12 col-md-4">
                               </div>              
                          </div>
                                 <div class="row">
                                <div class="col-xs-12 col-md-4" style ="padding-left: 18px;padding-bottom: 10px;">  
                                    <label class="label">&nbsp;</label> 

                                      <asp:Button ID="btnGet" runat="server"   width="73px"  HEIGHT="33PX" class="btn btn-primary" Text="Save"  OnClick="btnSave_Click" OnClientClick="javascript:return validateControlsActivatePort();"  /> 
                                    <asp:Button ID="btnExportToExcel"  runat="server" width="73px"  HEIGHT="33PX" Text="Back"  OnClick="btnExportToExcel_Click" class="btn btn-primary"  />                 
                              
                                                    
                               </div>
                                 <div class="col-xs-12 col-md-4">
                               </div>
                                   <div class="col-xs-12 col-md-4">
                               </div>              
                          </div>
                               
                                                 
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

					<!-- end row -->
				</section>
				<!-- end widget grid -->
			</div>
			<!-- END MAIN CONTENT -->
		</div>
		<!-- END MAIN PANEL -->
    <div id="popupdiv" class="loading" title="Basic modal dialog" style="display:nonefont-family:Arial;font-size: 10pt; border: 2px solid #67CFF5;text-align: -webkit-center;">
        
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
            '.chosen-select-width': { width: "95%" }
        }
        for (var selector in config) {
            $(selector).chosen(config[selector]);
        }
    </script>

</asp:Content>


