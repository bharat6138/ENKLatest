﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="InventoryAccept.aspx.cs" Inherits="ENK.InventoryAccept" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">

        
   </script>

        <link type="text/css" rel="stylesheet" href="http://code.jquery.com/ui/1.10.3/themes/smoothness/jquery-ui.css" />
<script type="text/javascript" src="http://code.jquery.com/jquery-1.8.2.js"></script>
<script type="text/javascript" src="http://code.jquery.com/ui/1.10.3/jquery-ui.js"></script>
    <script type="text/javascript">
        function ShowProgress() {
            $("#popupdiv").dialog({
                title: "",
                width: 430,
                height: 250,
                modal: false,
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

        $('form').live("submit", function () {
            ShowProgress();
        });



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
		<div id="main" role="main" style="margin-top: 0px!important;" >

			<!-- RIBBON -->
			 
			<!-- END RIBBON -->

			<!-- MAIN CONTENT -->
			<div id="content">
				<div class="row new-inerpage">
					<div class="col-xs-12 col-sm-5 col-md-5 col-lg-4">
						<h1 class="page-title txt-color-blueDark">
							<i class="fa fa-lg fa-fw fa-money"></i> 
								Inventory
							<span>> 
								Accept						</span>						</h1>
					</div>
					<div class="col-xs-12 col-sm-7 col-md-7 col-lg-8">
						<%--<ul id="sparks" class="">
							<li class="sparks-info">
								<a href="newpage.html" title="Save">
                                	<span><i class="fa fa-floppy-o"></i></span> Save                              	</a>							</li>
							<li class="sparks-info">
								<a href="#" title="Save &amp; Close">
                                	<span><i class="fa fa-times"></i></span> Save &amp; Close                              	</a>                            </li>
							<li class="sparks-info">
								<a href="#" title="Save &amp; New">
                                	<span><i class="fa fa-folder"></i></span> Save &amp; New                              	</a>                            </li>
                            <li class="sparks-info">
								<a href="#" title="Close">
                                	<span><i class="fa fa-times"></i></span> Close                                </a>                            </li>
						</ul>--%>
					</div>
				</div>

				<!-- widget grid -->
				<section id="widget-grid" class="">

					<!-- row -->
					<div class="row" >
						<!-- NEW WIDGET START -->
						<article class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
							<!-- Widget ID (each widget will need unique ID)-->
							<div class="jarviswidget jarviswidget-color-blueDark" id="wid-id-0"  data-widget-colorbutton="false" data-widget-editbutton="false" data-widget-togglebutton="false" data-widget-deletebutton="false" data-widget-fullscreenbutton="false" data-widget-custombutton="false" data-widget-collapsed="false" data-widget-sortable="false">
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
									<h2>Accept</h2>
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
                                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">                                    
                                    <asp:Label ID="lblTransferFromAndTo" runat="server" Text="Accept From"></asp:Label>                                    
                                    <label class="input">  
                                        <asp:DropDownList  class="chosen-select text-area" ID="ddlTransferTo" OnSelectedIndexChanged="ddlTransferTo_SelectedIndexChanged" AutoPostBack="true" runat="server">
                                             
                                        </asp:DropDownList> 
                                    </label>                                  
                                </div>
                                                 
                            </div>   

                            

                             <div class="row" id="DivSIM" runat="server" visible="true">
                                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12"> 
                                       <label class="label">&nbsp;</label> 
                                      <%-- <div class="table-responsive" id="dd" runat="server" visible="false">
                                       <table class="table table-bordered" id="dataTables-example">
                                                <thead>
                                                    <tr>
                                                        <th style="width: 24px;"></th>
                                                        <th style="width: 101px;">Tariff Code</th>
                                                        <th style="width: 200px;">Tariff Description</th>
                                                        <th>Default Tariff</th>                                                         
                                                    </tr> 
                                                </thead>    
                                       </table>
                                       </div>--%>
                                       <div class="table-responsive" style="max-height: 237px; overflow-y: auto;" >
                                       
                                        <asp:Repeater ID="RepeaterTransferSIM" runat="server">
                                         <HeaderTemplate >
                                              <table class="table table-bordered"  id="dataTables-example">
                                                <thead>
                                                    <tr>
                                                        <th> <asp:CheckBox ID="chkAllSIM" OnCheckedChanged="chkAllSIM_OnCheckedChanged" AutoPostBack="true" runat="server" /></th>
                                                        
                                                        <th>Sim Number</th>
                                                        <th>Plan</th>                                     
                                                    </tr> 
                                                <tbody>             
                                              </HeaderTemplate>
                                       
                                              <ItemTemplate> 
                                                <tr>
                                                     
                                                    <td>
                                                        <asp:CheckBox ID="CheckBox2" runat="server" />
                                                    </td>
                                                     
                                                    <td>
                                                    <asp:Label runat="server" ID="Label7" text='<%# Eval("SimNo") %>' />
                                                        <asp:HiddenField ID="hdnSIMID" runat="server" Value='<%# Bind("SIMID") %>' />
                                                    </td> 
                                                    <td>
                                                    <asp:Label runat="server" ID="Label2" text='<%# Eval("TariffCode") %>' />
                                                        
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

                            <div class="row" id="DivMobile" runat="server">
                                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12"> 
                                       <label class="label">&nbsp;</label> 
                                      <%-- <div class="table-responsive" id="dd" runat="server" visible="false">
                                       <table class="table table-bordered" id="dataTables-example">
                                                <thead>
                                                    <tr>
                                                        <th style="width: 24px;"></th>
                                                        <th style="width: 101px;">Tariff Code</th>
                                                        <th style="width: 200px;">Tariff Description</th>
                                                        <th>Default Tariff</th>                                                         
                                                    </tr> 
                                                </thead>    
                                       </table>
                                       </div>--%>
                                       <div class="table-responsive" style="max-height: 237px; overflow-y: auto;" >
                                       
                                        <asp:Repeater ID="RepeaterTransfer" runat="server" Visible="false">
                                         <HeaderTemplate >
                                              <table class="table table-bordered"  id="dataTables-example">
                                                <thead>
                                                    <tr>
                                                        <th> <asp:CheckBox ID="chkAll" OnCheckedChanged="chkAll_OnCheckedChanged" AutoPostBack="true" runat="server" /></th>
                                                        <th>Mobile Number</th>
                                                        <th>Sim Number</th>
                                                                                                                
                                                    </tr> 
                                                <tbody>             
                                              </HeaderTemplate>
                                       
                                              <ItemTemplate> 
                                                <tr>
                                                     
                                                    <td>
                                                        <asp:CheckBox ID="CheckBox2" runat="server" />
                                                    </td>
                                                    <td>
                                                    <asp:Label runat="server" ID="Label1" text='<%# Eval("MobileNo") %>' />
                                                    <asp:HiddenField ID="hdnMSISDN_SIM_ID" runat="server" Value='<%# Bind("MSISDN_SIM_ID") %>' />                                                    
                                                    </td>  
                                                    <td>
                                                    <asp:Label runat="server" ID="Label7" text='<%# Eval("SimNo") %>' />
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
                
                <footer class="boxBtnsubmit">
                     <asp:Button ID="btnReset" runat="server" Text="Back" OnClick="btnReset_Click" class="btn btn-primary"   />
                    <asp:Button ID="btnReject" runat="server" Text="Reject" OnClick="btnReject_Click" Visible="true" class="btn btn-primary"  />     
                     <asp:Button ID="btnTransfer" runat="server" Text="Accept" OnClick="btnTransfer_Click" class="btn btn-primary"  />     
                                           
                </footer>
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
