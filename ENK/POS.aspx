<%@ Page Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="POS.aspx.cs" Inherits="ENK.POS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <link type="text/css" rel="stylesheet" href="http://code.jquery.com/ui/1.10.3/themes/smoothness/jquery-ui.css" />
     <script type="text/javascript" src="http://code.jquery.com/jquery-1.8.2.js"></script>
     <script type="text/javascript" src="http://code.jquery.com/ui/1.10.3/jquery-ui.js"></script>

    <script type="text/javascript">
        function validate() {

            if (document.getElementById("<%=txtCurrentSIMNumber.ClientID%>").value == "") {
            alert("Current SIM Number Can't be Blank");
            document.getElementById("<%=txtCurrentSIMNumber.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=txtCurrentMobileNumber.ClientID%>").value == "") {
            alert("Current Mobile Number Can't be Blank");
            document.getElementById("<%=txtCurrentMobileNumber.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=txtCustomerName.ClientID%>").value == "") {
            alert("Customer Name Can't be Blank");
            document.getElementById("<%=txtCustomerName.ClientID%>").focus();
                return false;
            }

            var email = document.getElementById("<%=txtEmail.ClientID%>");
            var filter = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;

            if (!filter.test(email.value)) {
                alert('Please Enter a valid email address');
                email.focus;
                return false;
            }
            return true;
        }
        function onlynumeric(evt, cnt) {

            var keycode;
            var textbox = cnt;
            if (window.event) {
                event = window.event;
            }
            //alert(textbox.value);
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

                if (keycode >= 48 && keycode <= 57) {
                    //alert(textbox.value);


                }
                else {
                    alert("Only numbers allowed.");

                    return false; // disable key press
                }


            }

        }

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
							<i class="fa fa-lg fa-fw fa-sort-alpha-asc"></i> 
								POS
							<span> >
								Point Of Sale						</span>						</h1>
					</div>
					<div class="col-xs-12 col-sm-6 col-md-6 col-lg-8">     
                         <footer class="boxBtnsubmit">                    
						<ul id="sparks"  class="">
							 <li style="border-left: 0px!important;" id="liSave" runat="server" visible="true" class="sparks-info">
                                <asp:Button ID="btnSaveUp" runat="server" Style="float: right; height: 31px;margin: 10px 0 0 5px;padding: 0 22px; font: 300 15px/29px 'Open Sans',Helvetica,Arial,sans-serif; cursor: pointer;" Text="SAVE" class="btn btn-primary" Visible="true" OnClick="btnSaveUp_Click" OnClientClick="javascript:return validate();" /> 
  
							</li>	
                            <li style="border-left: 0px!important;" id="liReset" runat="server" visible="true" class="sparks-info">
                                <asp:Button ID="btnResetUp" runat="server" Style="float: right; height: 31px;margin: 10px 0 0 5px;padding: 0 22px; font: 300 15px/29px 'Open Sans',Helvetica,Arial,sans-serif; cursor: pointer;" Text="RESET" class="btn btn-primary" Visible="true" OnClick="btnResetUp_Click" /> 
  
							</li>
                            				 
                            <li style="border-left: 0px!important; float: initial;" id="liBack" runat="server" class="sparks-info">								 
                               <asp:Button ID="btnBackUp" runat="server" Text="BACK" Style="float: right; height: 31px;margin: 10px 0 0 5px;padding: 0 22px; font: 300 15px/29px 'Open Sans',Helvetica,Arial,sans-serif; cursor: pointer;" class="btn btn-primary" Visible="true" OnClick="btnBackUp_Click"  />
							</li>                            
						</ul>
                            </footer>
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
									<h2>Point Of Sale</h2>
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
                                   <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12" style="border: 1px solid #284497;">
                                <div class="col-xs-12 col-sm-6 col-md-6 col-lg-5">                                    
                                    <label class="label">Current SIM Number<asp:Label ID="Label19" ForeColor="Red" runat="server" Text="*"></asp:Label></label>                                       
                                    <label class="input">  
                                        <asp:TextBox ID="txtCurrentSIMNumber" runat="server" title="Branch Name" AutoPostBack="false"></asp:TextBox> 
                                             <b class="tooltip tooltip-top-right">
                                        <i class="fa fa-arrow-circle-down"></i> Enter Current SIM Number</b>  
                                    </label>                  
                                    <asp:HiddenField runat="server" ID="hdnCurrentSIMID" />                
                                </div>
                                <div class="col-xs-12 col-sm-1 col-md-1 col-lg-1">
                                    <label class="label">(OR)</label>
                                    </div>
                                <div class="col-xs-12 col-sm-5 col-md-5 col-lg-5">                                   
                                    <label class="label">Current Mobile Number<asp:Label ID="Label18" ForeColor="Red" runat="server" Text="*"></asp:Label></label>                                       
                                    <label class="input">  
                                        <asp:TextBox ID="txtCurrentMobileNumber" runat="server" title="Branch Name" AutoPostBack="false" ></asp:TextBox> 
                                             <b class="tooltip tooltip-top-right">
                                        <i class="fa fa-arrow-circle-down"></i> Enter Current Mobile Number</b>                                   

                                    </label>                                   
                                     <asp:HiddenField runat="server" ID="hdnMSISDN_SIM_ID" /> 
                                </div>    
                                <div class="col-xs-12 col-sm-1 col-md-1 col-lg-1">
                                    <label class="label">&nbsp;</label>
                                    <asp:Button ID="btnVerify" runat="server" Text="Verify" OnClick="btnVerify_Click" width="60px" Height="25px" />
                                    </div>  
                                    </div>     
                                </div>         
                            </div>   
                            <div class="row">
                                <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6"> 
                                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">                                   
                                    <label class="label">Customer Name<asp:Label ID="Label17" ForeColor="Red" runat="server" Text="*"></asp:Label></label>                                       
                                    <label class="input">  
                                         <asp:TextBox ID="txtCustomerName" runat="server" title="Customer Name"></asp:TextBox>                      
                                        <b class="tooltip tooltip-top-right">
                                        <i class="fa fa-arrow-circle-down"></i> Enter Customer Name</b> 
                                    </label>
                                </div>  
                                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">                                   
                                    <label class="label">Email</label>                                       
                                    <label class="input">  
                                         <asp:TextBox ID="txtEmail" runat="server" title="Email"></asp:TextBox>                      
                                        <b class="tooltip tooltip-top-right">
                                        <i class="fa fa-arrow-circle-down"></i> Enter Email</b> 
                                    </label>
                                </div>  
                                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">                                   
                                    <label class="label">Address</label>                                       
                                    <label class="input">  
                                         <asp:TextBox ID="txtAddress" runat="server" title="Address"></asp:TextBox>                      
                                        <b class="tooltip tooltip-top-right">
                                        <i class="fa fa-arrow-circle-down"></i> Enter Address</b> 
                                    </label>
                                </div>  
                                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">                                   
                                    <label class="label">Alternate Number </label>                                       
                                    <label class="input">  
                                         <asp:TextBox ID="txtAlternateNumber" runat="server" title="Alternate Number" onkeypress="javascript:return onlynumeric(event,this);"></asp:TextBox>                      
                                        <b class="tooltip tooltip-top-right">
                                        <i class="fa fa-arrow-circle-down"></i> Enter Alternate Number</b> 
                                    </label>
                                </div>  
                                </div>
                                 <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">   
                                 </div>  
                            </div>   
                            
                        </div>
                
                <footer class="boxBtnsubmit">
                      <asp:Button ID="btnBack" runat="server" Text="BACK" class="btn btn-primary" Visible="true" OnClick="btnBack_Click"    />
                     <asp:Button ID="btnReset" runat="server" Text="RESET" OnClick="btnReset_Click" class="btn btn-primary"   />
                     <asp:Button ID="btnSave" runat="server" Text="SAVE" OnClick="btnSave_Click" class="btn btn-primary" OnClientClick="javascript:return validate();"  />              
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
