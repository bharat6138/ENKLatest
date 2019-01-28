<%@ Page Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="Resetpassword.aspx.cs" Inherits="ENK.Resetpassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link type="text/css" rel="stylesheet" href="http://code.jquery.com/ui/1.10.3/themes/smoothness/jquery-ui.css" />
   <script type="text/javascript" src="http://code.jquery.com/jquery-1.8.2.js"></script>
   <script type="text/javascript" src="http://code.jquery.com/ui/1.10.3/jquery-ui.js"></script>
    <script type="text/javascript">

        function validate() {
            if (document.getElementById("<%=txtUserID.ClientID%>").value == "") {
                alert("UserID Can't be Blank");
                document.getElementById("<%=txtUserID.ClientID%>").focus();
                return false;
            }

            if (document.getElementById("<%=txtOldPass.ClientID%>").value == "") {
                alert("Old Password Can't be Blank");
                document.getElementById("<%=txtOldPass.ClientID%>").focus();
                return false;
            }
            

            if (document.getElementById("<%=txtOldPass.ClientID%>").value == "") {
                alert("Contact Person Can't be Blank");
                document.getElementById("<%=txtOldPass.ClientID%>").focus();
                return false;
            }

            if (document.getElementById("<%=txtNewPassword.ClientID%>").value == "") {
                alert("New Password Can't be Blank");
                document.getElementById("<%=txtNewPassword.ClientID%>").focus();
                return false;
            }

            if (document.getElementById("<%=txtConfirmNewPassword.ClientID%>").value == "") {
                alert("Confirm New Password Can't be Blank");
                document.getElementById("<%=txtConfirmNewPassword.ClientID%>").focus();
                return false;
            }

            return true;
        }

        function ErrorClose() {
            var lbl = document.getElementById("<%=lblmsg.ClientID%>");
            lbl.style.display = "none";
            return true;
        }

        function matchpassword() {

            var password = document.getElementById("<%=txtNewPassword.ClientID%>").value;
             var con_password = document.getElementById("<%=txtConfirmNewPassword.ClientID%>").value;
             //alert("Please1 enter user name.");
             //alert(password);

             //alert(con_password );
             if (password.trim() == con_password.trim()) {
                 var lbl = document.getElementById("<%=lblmsg.ClientID%>");
                 lbl.style.display = "none";
             }
             else {
                 //alert("Password Not Match");  

                 var lbl = document.getElementById("<%=lblmsg.ClientID%>");
                 lbl.style.display = "block";
                 document.getElementById("<%=txtNewPassword.ClientID%>").focus();
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
							<i class="fa fa-lg fa-fw fa-wrench"></i> 
								Settings
							<span>> 
								Reset Password						</span>						</h1>
					</div>
					<div class="col-xs-12 col-sm-7 col-md-7 col-lg-8">
						<footer class="boxBtnsubmit">                    
						<ul id="sparks"  class="">
							 
                             <li style="border-left: 0px!important;" id="liSave" runat="server" visible="true" class="sparks-info">
                                <asp:Button ID="btnSaveUp" runat="server" Style="float: right; height: 31px;margin: 10px 0 0 5px;padding: 0 22px; font: 300 15px/29px 'Open Sans',Helvetica,Arial,sans-serif; cursor: pointer;" Text="RESET PASSWORD" class="btn btn-primary" Visible="true" OnClick="btnSaveUp_Click" OnClientClick="javascript:return validate();" /> 
  
							</li>	
                            <li style="border-left: 0px!important;" id="liReset" runat="server" visible="true" class="sparks-info">
                                <asp:Button ID="btnResetUp" runat="server" Style="float: right; height: 31px;margin: 10px 0 0 5px;padding: 0 22px; font: 300 15px/29px 'Open Sans',Helvetica,Arial,sans-serif; cursor: pointer;" Text="CLEAR" class="btn btn-primary" Visible="true" OnClick="btnResetUp_Click" /> 
  
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
									<h2>Reset Password</h2>
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
                                    <div visible="false" class="col-xs-12 col-sm-6 col-md-6 col-lg-6"> 
                                      
                                        <div id="divVendorCode" runat="server"  class="col-xs-12 col-sm-12 col-md-12 col-lg-12">                                                                               
                                        <label class="label">Userid</label>                                       
                                        <label class="input">  
                                        <asp:TextBox ID="txtUserID" runat="server" title="Vendor Code...."></asp:TextBox>
                                             <b class="tooltip tooltip-top-right">
                                        <i class="fa fa-arrow-circle-down"></i> Enter Vendor Name</b>
                                       </label>                                    
                                       </div>
                                        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">                                                                               
                                        <label class="label">Old Password</label>                                       
                                        <label class="input">  
                                        <asp:TextBox ID="txtOldPass" runat="server" title="Old Password...."></asp:TextBox>
                                             <b class="tooltip tooltip-top-right">
                                        <i class="fa fa-arrow-circle-down"></i> Enter Old Password</b>
                                       </label>                                    
                                       </div>
                                        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">                                                                               
                                        <label class="label">New Password</label>                                       
                                        <label class="input">  
                                        <asp:TextBox ID="txtNewPassword" runat="server" title="New Password...." onkeypress="javascript:return ErrorClose();"></asp:TextBox> 
                                       
                                                <b class="tooltip tooltip-top-right">
                                        <i class="fa fa-arrow-circle-down"></i> Enter New Password</b>
                                        </label>                                    
                                        </div>
                                        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">                                                                               
                                        <label class="label">Confirm New Password</label>                                       
                                        <label class="input">  
                                        <asp:TextBox ID="txtConfirmNewPassword" runat="server" title="Confirm New Password...."  onkeypress="javascript:return ErrorClose();" onblur="javascript:return matchpassword();"></asp:TextBox> 
                                       
                                                <b class="tooltip tooltip-top-right">
                                        <i class="fa fa-arrow-circle-down"></i> Enter Confirm New Password</b>
                                            <asp:Label ID="lblmsg" runat="server" Text="Password not match" ForeColor="Red"  Style="display:none;"></asp:Label>
                                        </label>                                    
                                        </div>
                                         
                                         
                                    </div>
                                    <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6"> 
                                         
                                         
                                    </div>
                                </div>               
                            </div>      

                                
                        
                        </div>
                
                <footer class="boxBtnsubmit">
                        
                        <asp:Button ID="btnReset" runat="server" Text="RESET" class="btn btn-primary" Visible="false" OnClick="btnReset_Click"/>
                        <asp:Button ID="btnSave" runat="server" Text="RESET PASSWORD" class="btn btn-primary"   OnClientClick="javascript:return validate();" OnClick="btnSave_Click" />
                         
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

