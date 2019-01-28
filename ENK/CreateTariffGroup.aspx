<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreateTariffGroup.aspx.cs" Inherits="ENK.CreateTariffGroup"   MasterPageFile="~/MainMaster.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link type="text/css" rel="stylesheet" href="http://code.jquery.com/ui/1.10.3/themes/smoothness/jquery-ui.css" />
    <script type="text/javascript" src="http://code.jquery.com/jquery-1.8.2.js"></script>
    <script type="text/javascript" src="http://code.jquery.com/ui/1.10.3/jquery-ui.js"></script>

    <script type="text/javascript">

        function validate() {


            if (document.getElementById("<%=txtTraiffName.ClientID%>").value == "") {
               alert("Tariff Name Can't be Blank");
               document.getElementById("<%=txtTraiffName.ClientID%>").focus();
                return false;
            }

            if (document.getElementById("<%=txtDisActivationPortIN.ClientID%>").value == "") {
               alert("Discount on Activation/Port_In Can't be Blank");
               document.getElementById("<%=txtDisActivationPortIN.ClientID%>").focus();
                return false;
            }

            if (document.getElementById("<%=txtDiscount_on_Recharge.ClientID%>").value == "") {
               alert("Discount on Recharge Can't be Blank");
               document.getElementById("<%=txtDiscount_on_Recharge.ClientID%>").focus();
                return false;
            }


            if (document.getElementById('<%=ddlseller.ClientID%>').selectedIndex == 0) {
               alert('Please Select Seller Type');
               document.getElementById("<%=ddlseller.ClientID%>").focus();
               return false;
           }



           return true;
       }



    </script>


    <script type="text/javascript">


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
                        alert("Only one decimal allowed.");
                        return false;
                    }
                    else {

                        return true;
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
							<i class="fa fa-user fa-fw"></i> 
								Admin
							<span>> 
								Tariff Groups</span>						</h1>
					</div>
			
				</div>

				<!-- widget grid -->
				<section id="widget-grid" class="">

					<!-- row -->
					<div class="row">
						<!-- NEW WIDGET START -->
						<article class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
							<!-- Widget ID (each widget will need unique ID)-->
							<div class="jarviswidget jarviswidget-color-blueDark" id="wid-id-0" data-widget-colorbutton="false" data-widget-editbutton="false" data-widget-togglebutton="false" data-widget-deletebutton="false" data-widget-fullscreenbutton="false" data-widget-custombutton="false" data-widget-collapsed="false" data-widget-sortable="false">
								<%-- widget options:usage:--%>
								  

								<%--data-widget-colorbutton="false"
								data-widget-editbutton="false"
								data-widget-togglebutton="false"
								data-widget-deletebutton="false"
								data-widget-fullscreenbutton="false"
								data-widget-custombutton="false"
								data-widget-collapsed="true"
								data-widget-sortable="false"--%>

								
								<header>
									<span class="widget-icon"> <i class="fa fa-table"></i> </span>
									<h2>New Tariff Group</h2>
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
                                 <asp:HiddenField ID="hddnTariffID" runat="server" />
                                
                                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">                                   
                                    <label class="label">Traiff Group Name</label>                                       
                                    <label class="input">  
                                    <asp:TextBox ID="txtTraiffName" runat="server" title="Traiff Name...."></asp:TextBox> 
                                   
                                            <b class="tooltip tooltip-top-right">
                                    <i class="fa fa-arrow-circle-down"></i> Enter Group Name</b>
                                    </label>                                    
                                </div>
                                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">                                   
                                    <label class="label"> Activation/Port In Discount </label>                                       
                                    <label class="input">  
                                        <asp:TextBox ID="txtDisActivationPortIN" runat="server" onkeypress="javascript:return onlynumericDecimal(event,this);"  title="Discount on Activation/Port In...."></asp:TextBox> 
                                                <b class="tooltip tooltip-top-right">
                                        <i class="fa fa-arrow-circle-down"></i>  Activation/Port In Discount </b>
                                    </label>                                    
                                </div>
                                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">                                   
                                    <label class="label">Recharge Discount</label>                                  
                                    <label class="input">  
                                            <asp:TextBox ID="txtDiscount_on_Recharge" runat="server" onkeypress="javascript:return onlynumericDecimal(event,this);"></asp:TextBox>                          
                                        <b class="tooltip tooltip-top-right">
                                        <i class="fa fa-arrow-circle-down"></i> Recharge Discount</b> 
                                    </label>
                                </div>
                                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">                                    
                                    <label class="label">Seller Type</label>                                       
                                    <label class="input">  
                                         <asp:DropDownList ID="ddlseller" runat="server">
                                               <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                       <asp:ListItem Text="Distributor" Value="1"></asp:ListItem>
                                      <asp:ListItem Text="Retailer" Value="2"></asp:ListItem>
                                      </asp:DropDownList>
                                     
                                    </label>                                  
                                </div>                                   
                                 
                                 
                                 <div id="divActive" runat="server" visible="false" class="col-xs-12 col-sm-12 col-md-12 col-lg-12">                                    
                                    <label class="label">Is Tariff Group Active</label>                                       
                                    <label class="text"> 
                                        <asp:CheckBox ID="CheckBox1" runat="server" Checked="true" /> 
                                    </label>                                  
                                </div> 
                            </div>
                            <div class="row">
                            <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">                                    
                                                                                                       
                            </div>
                                                 
                        </div> 
                        </div>
                
                <footer class="boxBtnsubmit">
                        <asp:Button ID="btnBack" runat="server" Text="BACK" class="btn btn-primary" Visible="false"  OnClick="btnBack_Click1"   />
                        <asp:Button ID="btnReset" runat="server" Text="RESET" class="btn btn-primary" OnClick="btnReset_Click1"/>
                        <asp:Button ID="btnSave" runat="server" Text="SAVE" class="btn btn-primary" ValidationGroup="save" OnClientClick="javascript:return validate();" OnClick="btnSave_Click"  />
                        <asp:Button ID="btnUpdate" runat="server" Text="UPDATE" class="btn btn-primary" OnClientClick="javascript:return validate();" OnClick="btnUpdate_Click1"/>              
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
</asp:Content>
