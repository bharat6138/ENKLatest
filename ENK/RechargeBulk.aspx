

<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="RechargeBulk.aspx.cs" Inherits="ENK.RechargeBulk" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link type="text/css" rel="stylesheet" href="http://code.jquery.com/ui/1.10.3/themes/smoothness/jquery-ui.css" />
<script type="text/javascript" src="http://code.jquery.com/jquery-1.8.2.js"></script>
<script type="text/javascript" src="http://code.jquery.com/ui/1.10.3/jquery-ui.js"></script>

    <script type="text/javascript">


        function ConfirmOnDelete() {

            if (document.getElementById("<%=ddlNetwork.ClientID%>").selectedIndex == 0) {
                alert("Please select Network");
                document.getElementById("<%=ddlNetwork.ClientID%>").focus();
                return false;
            }

            if (document.getElementById("<%=txtRental.ClientID%>").value == "") {
                alert("  Recharge % Can't be Blank");
                document.getElementById("<%=txtRental.ClientID%>").focus();
                return false;
            }
            var prodDis = Math.floor(document.getElementById('<%=txtRental.ClientID%>').value);
            if (prodDis >= 100) {
                alert(" % Recharge can not greter than 100%");
                document.getElementById("<%=txtRental.ClientID%>").focus();
                return false;
            }
            if (confirm("Are you sure  % Recharge update ?") == true) {

                return true;
            }
            else
                return false;
        }


        function validate() {

            if (document.getElementById("<%=ddlNetwork.ClientID%>").selectedIndex == 0) {
                alert("Please select Network");
                document.getElementById("<%=ddlNetwork.ClientID%>").focus();
                return false;
            }

            if (document.getElementById("<%=txtRental.ClientID%>").value == "") {
                alert("  Recharge % Can't be Blank");
                document.getElementById("<%=txtRental.ClientID%>").focus();
                return false;
            }
            var prodDis = Math.floor(document.getElementById('<%=txtRental.ClientID%>').value);
            if (prodDis >= 100) {
                alert(" % Recharge can not greter than 100%");
                document.getElementById("<%=txtRental.ClientID%>").focus();
                 return false;
             }


            return true;
        }

        function onlynumericDecimal(evt, cnt) {

            var keycode;
            var textbox = cnt;
            if (window.event) {
                event = window.event;
            }

            if (event.keyCode) //For IE
            {
                keycode = event.keyCode;

            }
            else if (evt.Which) {
                keycode = event.Which;  // For FireFox

            }
            else {
                keycode = event.charCode; // Other Browser

            }

            if (keycode != 8) //if the key is the backspace key
            {

                if ((keycode >= 48 && keycode <= 57) || keycode == 46) {
                    //alert(textbox.value);
                    var tmp;
                    if (event.keyCode == 46) {
                        tmp = textbox.value + ".";
                    }
                    if (tmp.split(".").length > 2) {
                        textbox.value = tmp.trim().substr(0, tmp.length - 1);
                        //alert("Only one decimal allowed.");
                        //return false;
                    }
                    else {

                        //return true;
                    }

                }
                else {
                    alert("Only numbers allowed.");

                    return false; // disable key press
                }


            }

        }//end


        </script>


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
								Admin
							<span>> 
								Distributor Bulk % Rechage				</span>						</h1>
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
									<%--<h2>Distributor Bulk Rechage</h2>--%>
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
                                     
                           <div class="row" style="border:black solid 1px;  margin: auto;">
                                
                                 <div class="col-xs-12 col-sm-12 col-md-12 col-lg-4">                                    
                                  <label class="label">Network</label>                             
                                  <label class="input">  
                                        
                                                    <asp:DropDownList  class="chosen-select text-area" ID="ddlNetwork"    runat="server">
                                             
                                                     </asp:DropDownList> 

                                    </label>                                  
                                </div>
                                
                                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-4">                                    
                                     <label class="label">Recharge % </label>                                       
                                    <label class="input">  
                                         <asp:TextBox ID="txtRental" maxlength="4" onkeypress="javascript:return onlynumericDecimal(event,this);" runat="server" Placeholder="Recharge %"></asp:TextBox>
                                    </label> 
                                                                 
                                </div>
                                
                                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-2"> 
                                    <label class="label">&nbsp;</label> 
                                    <asp:Button ID="btnDone" class="btn btn-primary" runat="server" Text="Apply" Style="float: right; height: 31px;margin: 10px 0 0 5px;padding: 0 44px;margin-top:0px; font: 300 15px/29px 'Open Sans',Helvetica,Arial,sans-serif; cursor: pointer;" OnClick="btnDone_Click"  OnClientClick="return ConfirmOnDelete();" />
                                            
                                </div>
                                                 
                            </div> 
                            
                            <div class="row" id="DivMobile" runat="server" style="border:black solid 0px;  margin: auto;">
                                 <div class="col-xs-12 col-sm-12 col-md-12 col-lg-4">                                    
                                    <label class="label">Distributor</label>                                       
                                    <label class="input">  
                                        <asp:DropDownList  class="chosen-select text-area" ID="ddlTariffPlan" runat="server" OnSelectedIndexChanged="ddlTariffPlan_SelectedIndexChanged" AutoPostBack="true">
                                             
                                            
                                        </asp:DropDownList> 
                                    </label>                                
                                </div>
                                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12"> 
                                      
                                    
                                    <label class="label">&nbsp;</label> 
                                      
                                     <div class="table-responsive" style="max-height: 237px; overflow-y: auto;" >
                                       
                                        <asp:Repeater ID="Repeater" runat="server">
                                         <HeaderTemplate >
                                              <table class="table table-bordered"  id="dataTables-example">
                                                <thead>
                                                    <tr>
                                                        <th> <asp:CheckBox ID="chkAllSIM" OnCheckedChanged="chkAllSIM_OnCheckedChanged" AutoPostBack="true" runat="server" /></th>
                                                        
                                                        <th>Distributor</th>
                                                                                                                
                                                    </tr> 
                                                <tbody>             
                                              </HeaderTemplate>
                                       
                                              <ItemTemplate> 
                                                <tr>
                                                     
                                                    <td>
                                                        <asp:CheckBox ID="CheckBox2" runat="server" />
                                                    </td>
                                                     
                                                    <td>
                                                    <asp:Label runat="server" ID="Label7" text='<%# Eval("Name") %>' />
                                                    <asp:HiddenField ID="hdnDistributorID" runat="server" Value='<%# Bind("ID") %>' />
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
