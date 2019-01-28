<%@ Page Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="SimHistoryReport.aspx.cs" Inherits="ENK.SimHistoryReport" %>

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
        function validate() {

           
            if (document.getElementById("<%=txtSimNumber.ClientID%>").value == "") {
                alert("Please Fill SIM/MOBILE number");
                document.getElementById("<%=txtSimNumber.ClientID%>").focus();
                return false;
            }

            return true;
        }


    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- MAIN PANEL -->
		<div id="main" role="main" style="margin-top: 0px!important;">

			<!-- RIBBON -->
			 
			<!-- END RIBBON -->

			<!-- MAIN CONTENT -->
			<div id="content">
				<div class="row new-inerpage">
					<div class="col-xs-12 col-sm-5 col-md-5 col-lg-4">
						<h1 class="page-title txt-color-blueDark">
							<i class="fa fa-lg fa-fw fa-file-text"></i> 
								Report
							<span>> 
								SIM History Report					</span>						</h1>
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
									<h2>SIM History Report</h2>
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
                                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-4">                                    
                                    <label class="label">SIM/Mobile Number</label>                                       
                                    <label class="input">  
                                       <asp:TextBox ID="txtSimNumber" runat="server" title="SIM/Mobile Number......" ></asp:TextBox> 
                                             <b class="tooltip tooltip-top-right">
                                        <i class="fa fa-arrow-circle-down"></i> Enter SIM/Mobile Number</b>     
                                    </label>                                  
                                </div>
                                
                                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-3">
                                  <label class="label">&nbsp;</label>
                                    <asp:Button ID="btnGet" class="btn btn-primary" runat="server" Text="Get Report"  OnClick="btnGet_Click" OnClientClick="javascript:return validate();" /> 
                                    <asp:Button ID="btnExportToExcel"  runat="server" Text="Export To Excel"  OnClick="btnExportToExcel_Click" class="btn btn-primary"  />    
                                       
                                </div>

                                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-3">
                                </div>
                                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-2">
                                </div>
                                                 
                            </div>  
                            
                            <div class="row" id="DivMobile" runat="server">
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
</asp:Content> 
