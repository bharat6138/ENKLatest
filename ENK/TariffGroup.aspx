<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TariffGroup.aspx.cs" Inherits="ENK.TariffGroup" MasterPageFile="~/MainMaster.Master" %>

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
                 /*background: "#0F0101 url(images/ui-bg_flat_0_aaaaaa_40x100.png) repeat-x",
                   opacity: .3*/
                 background: rgb(99, 127, 239),
                 opacity: .3,
                 filter: Alpha(Opacity = 50)
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

<div id="main" role="main" style="margin-top: 0px!important;" >

			<!-- RIBBON -->
			 
			<!-- END RIBBON -->

			<!-- MAIN CONTENT -->
			<div id="content">
				<div class="row row-fullWidth">
					<div class="col-xs-5 col-sm-7 col-md-7 col-lg-4">
						<h1 class="page-title txt-color-blueDark">
							<i class="fa fa-user fa-fw"></i> 
								Admin
							<span>> 
								Plan Group
							</span>
						</h1>
					</div>
					<div id="divNew" runat="server" class="col-xs-7 col-sm-5 col-md-5 col-lg-8">
						<ul id="sparks" class="" runat="server" style="float: right;">
							<li class="sparks-info">
								<a href="TariffGroupN.aspx" title="New">
                                	<span><i class="fa fa-file"></i></span> New
                               	</a>
							</li>
							<%--<li class="sparks-info">
								<a href="#" title="Edit">
                                	<span><i class="fa fa-pencil-square-o"></i></span> Edit
                              	</a>
							</li>
							<li class="sparks-info">
								<a href="#" title="Trash">
                                	<span><i class="fa fa-trash-o"></i></span> Trash
                               	</a>
							</li>--%>
						</ul>
					</div>
				</div>

				<!-- widget grid -->
				<section id="widget-grid" class="">

					<!-- row -->
					<div class="row">
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
									<h2>Tariff Group</h2>

								</header>

								<!-- widget div-->
								<div>

									<!-- widget edit box -->
									<div class="jarviswidget-editbox">
										<!-- This area used as dropdown edit box -->

									</div>
									<!-- end widget edit box -->
                                     
									<!-- widget content -->


                                    <%-- runat="server" OnItemCommand="RpTariff_ItemCommand"--%>


									<div class="widget-body">
										<div class="table-responsive" style="max-height: 300px; overflow-y: auto;">
											 <asp:Repeater ID="RpTariff"  runat="server" OnItemCommand="RpTariff_ItemCommand">
                                              <HeaderTemplate>
                                              <table class="table table-bordered" id="dataTables-example">
                                                <thead>
                                                    <tr>
                                                        <th>Plan Group Name</th>
                                                       <%-- change General Commission to Discount--%>
                                                        <th>Discount</th>
                                                         <%--add by akash starts--%>
                                                        <th>H2O Discount</th>
                                                        <%--add by akash ends--%>
                                                        <th>Created By</th>
                                                        <th>Created Date</th>                                                        
                                                        <th>Action</th>
                                                    </tr> 
                                                <tbody>             
                                              </HeaderTemplate>
                                              <ItemTemplate> 
                                                <tr>
                                                    <td>
                                                    <asp:Label runat="server" ID="Label1" text='<%# Eval("TariffGroup") %>' />
                                                    <asp:HiddenField ID="hddnTariffID" runat="server" Value='<%# Bind("TariffGroupID") %>' />
                                                    
                                                    </td>  
                                                    <td>
                                                    <asp:Label runat="server" ID="Label7" text='<%# Eval("GeneralCommission") %>' />
                                                    </td> 
                                                     <%--add by akash starts--%>
                                                     <td>
                                                    <asp:Label runat="server" ID="Label4" text='<%# Eval("H2OGeneralCommission") %>' />
                                                    </td> 
                                                    <%--add by akash ends--%>
                                                     <td>
                                                    <asp:Label runat="server" ID="Label2" text='<%# Eval("CreatedBy") %>' />
                                                    </td> 
                                                    <td>
                                                    <asp:Label runat="server" ID="Label3" text='<%# Eval("CreatedDttm") %>' />
                                                    </td>                                                     
                                                 <td>
                                                    <asp:ImageButton id="lnkView" runat="server" CausesValidation="false" ToolTip="View Tariff Group" CommandName="View" CommandArgument='<%#Eval("TariffGroupID") %>' ImageUrl="~/img/1399984583_view.png" /> &nbsp
                                                    <asp:ImageButton ID="lnkEdit"  runat="server" CausesValidation="false" ToolTip="Edit Tariff Group" CommandName="RowEdit" CommandArgument='<%#Eval("TariffGroupID") %>' ImageUrl="~/img/Pencil-icon.png"  />&nbsp
                                                    </td>
                                                </tr>          
                                              
                                              </ItemTemplate>    
                                              <FooterTemplate>
                                               </thead> 
                                               </table>
                                               </tbody>
                                               
                                              </FooterTemplate>
                                              </asp:Repeater>  
                                             <asp:Repeater ID="rptTariffDist" runat="server"  Visible="false">
                                              <HeaderTemplate>
                                              <table class="table table-bordered" id="dataTables-example">
                                                <thead>
                                                    <tr>
                                                        <th>Code</th>
                                                        <th>Description</th>
                                                        <th>Plan Type</th>
                                                        <th>Rental</th>
                                                        
                                                        <th>Valid Days</th>
                                                         
                                                       
                                                    </tr> 
                                                <tbody>             
                                              </HeaderTemplate>
                                              <ItemTemplate> 
                                                <%--<tr>
                                                    <td>
                                                    <asp:Label runat="server" ID="Label1" text='<%# Eval("TariffCode") %>' />
                                                    <asp:HiddenField ID="hddnTariffID" runat="server" Value='<%# Bind("ID") %>' />
                                                    
                                                    </td>  
                                                    <td>
                                                    <asp:Label runat="server" ID="Label7" text='<%# Eval("Description") %>' />
                                                    </td> 
                                                     <td>
                                                    <asp:Label runat="server" ID="Label2" text='<%# Eval("TariffType") %>' />
                                                    </td> 
                                                    <td>
                                                    <asp:Label runat="server" ID="Label3" text='<%# Eval("Rental") %>' />
                                                    </td>
                                                     
                                                     <td>
                                                    <asp:Label runat="server" ID="Label4" text='<%# Eval("ValidityDays") %>' />
                                                    </td>
                                                      
                                                     
                                                </tr>    --%>      
                                              
                                              </ItemTemplate>    
                                              <FooterTemplate>
                                               </thead> 
                                               </table>
                                               </tbody>
                                               
                                              </FooterTemplate>
                                              </asp:Repeater>   
										</div>
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


