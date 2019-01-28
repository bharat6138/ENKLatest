<%@ Page Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="User.aspx.cs" Inherits="ENK.User" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link type="text/css" rel="stylesheet" href="http://code.jquery.com/ui/1.10.3/themes/smoothness/jquery-ui.css" />
    <script type="text/javascript" src="http://code.jquery.com/jquery-1.8.2.js"></script>
    <script type="text/javascript" src="http://code.jquery.com/ui/1.10.3/jquery-ui.js"></script>

    <script type="text/javascript">
        function validate() {
            debugger;
            if (document.getElementById('<%=ddlDistributor.ClientID%>').selectedIndex == 0) {
                alert('Please Select Distributor');
                document.getElementById("<%=ddlDistributor.ClientID%>").focus();
                return false;
            }

            if (document.getElementById("<%=txtUserFName.ClientID%>").value == "") {
                alert("First Name Can't be Blank");
                document.getElementById("<%=txtUserFName.ClientID%>").focus();
                return false;
            }

            if (document.getElementById("<%=txtUserId.ClientID%>").value == "") {
                alert("User ID Can't be Blank");
                document.getElementById("<%=txtUserId.ClientID%>").focus();
                return false;
            }

            if (document.getElementById("<%=txtEmail.ClientID%>").value == "") {
                alert("Email Can't be Blank");
                document.getElementById("<%=txtEmail.ClientID%>").focus();
                return false;
            }

            var email = document.getElementById("<%=txtEmail.ClientID%>");
            var filter = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;

            if (!filter.test(email.value)) {
                alert('Please Enter a valid email address');
                email.focus;
                return false;
            }

            if (document.getElementById("<%=txtPassWord.ClientID%>").value  == "") {
                alert("Password Can't be Blank");
                document.getElementById("<%=txtPassWord.ClientID%>").focus();
                return false;
            }
             if (document.getElementById("<%=txtConfirmPass.ClientID%>").value == "") {
                 alert("Confirm Password Can't be Blank");
                 document.getElementById("<%=txtConfirmPass.ClientID%>").focus();
                 return false;
             }
             if (document.getElementById("<%=txtMobile.ClientID%>").value == "") {
                 alert("Mobile Number Can't be Blank");
                 document.getElementById("<%=txtMobile.ClientID%>").focus();
                 return false;
             }    
             if (document.getElementById("<%=txtActiveFrom.ClientID%>").value == "") {
                 alert("Active From Date Can't be Blank");
                 document.getElementById("<%=txtActiveFrom.ClientID%>").focus();
                 return false;
             }
             if (document.getElementById("<%=txtActiveTo.ClientID%>").value == "") {
                 alert("Active To Date Can't be Blank");
                 document.getElementById("<%=txtActiveTo.ClientID%>").focus();
                 return false;
             }
             
            return true;
        }
        

        function ErrorClose1() {
            var lbl = document.getElementById("<%=lblMessage.ClientID%>");
            lbl.style.display = "none";
            return true;
        }

        function ErrorClose() {
            var lbl = document.getElementById("<%=lblmsg.ClientID%>");
             lbl.style.display = "none";
             return true;
         }

        function matchpassword() {
             
            var password = document.getElementById("<%=txtPassWord.ClientID%>").value;
            var con_password = document.getElementById("<%=txtConfirmPass.ClientID%>").value;
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
                 document.getElementById("<%=txtPassWord.ClientID%>").focus();
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
        function validateUpdate() {
            debugger;
           
            if (document.getElementById('<%=ddlDistributor.ClientID%>').selectedIndex == 0) {
                alert('Please Select Distributor');
                document.getElementById("<%=ddlDistributor.ClientID%>").focus();
                return false;
            }

            if (document.getElementById("<%=txtUserFName.ClientID%>").value == "") {
                alert("First Name Can't be Blank");
                document.getElementById("<%=txtUserFName.ClientID%>").focus();
                return false;
            }

            if (document.getElementById("<%=txtUserId.ClientID%>").value == "") {
                alert("User ID Can't be Blank");
                document.getElementById("<%=txtUserId.ClientID%>").focus();
                return false;
            }

            if (document.getElementById("<%=txtEmail.ClientID%>").value == "") {
                alert("Email Can't be Blank");
                document.getElementById("<%=txtEmail.ClientID%>").focus();
                return false;
            }

            var email = document.getElementById("<%=txtEmail.ClientID%>");
            var filter = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;

            if (!filter.test(email.value)) {
                alert('Please Enter a valid email address');
                email.focus;
                return false;
            }

            if (document.getElementById("<%=hdnDistributor.ClientID%>").value == "1") {
                if (document.getElementById("<%=txtPassWord.ClientID%>").value == "") {
                    alert("Password Can't be Blank");
                    document.getElementById("<%=txtPassWord.ClientID%>").focus();
                    return false;
                }
                if (document.getElementById("<%=txtConfirmPass.ClientID%>").value == "") {
                    alert("Confirm Password Can't be Blank");
                    document.getElementById("<%=txtConfirmPass.ClientID%>").focus();
                    return false;
                }
            }
             if (document.getElementById("<%=txtMobile.ClientID%>").value == "") {
                alert("Mobile Number Can't be Blank");
                document.getElementById("<%=txtMobile.ClientID%>").focus();
                 return false;
             }
             if (document.getElementById("<%=txtActiveFrom.ClientID%>").value == "") {
                alert("Active From Date Can't be Blank");
                document.getElementById("<%=txtActiveFrom.ClientID%>").focus();
                 return false;
             }
             if (document.getElementById("<%=txtActiveTo.ClientID%>").value == "") {
                alert("Active To Date Can't be Blank");
                document.getElementById("<%=txtActiveTo.ClientID%>").focus();
                 return false;
             }

             return true;
         }


         function ErrorClose1() {
             var lbl = document.getElementById("<%=lblMessage.ClientID%>");
            lbl.style.display = "none";
            return true;
        }

        function ErrorClose() {
            var lbl = document.getElementById("<%=lblmsg.ClientID%>");
            lbl.style.display = "none";
            return true;
        }

        function matchpassword() {

            var password = document.getElementById("<%=txtPassWord.ClientID%>").value;
            var con_password = document.getElementById("<%=txtConfirmPass.ClientID%>").value;
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
                 document.getElementById("<%=txtPassWord.ClientID%>").focus();
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
             
            
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
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
								New User						</span>						</h1>
					</div>
					<div class="col-xs-12 col-sm-7 col-md-7 col-lg-8">
						<ul id="sparks"  class="">
							<li  id="liUpdate" runat="server" style="border-left: 0px!important;" class="sparks-info">
                             
								<asp:Button ID="btnUpdateUp" runat="server" Text="UPDATE" Style="float: right; height: 31px;margin: 10px 0 0 5px;padding: 0 22px; font: 300 15px/29px 'Open Sans',Helvetica,Arial,sans-serif; cursor: pointer;" class="btn btn-primary" Visible="true" OnClick="btnUpdateUp_Click" OnClientClick="javascript:return validate();" /> 
                              
							</li>
							 <li style="border-left: 0px!important;" id="liSave" runat="server" visible="false" class="sparks-info">
                                <asp:Button ID="btnSaveUp" runat="server" Style="float: right; height: 31px;margin: 10px 0 0 5px;padding: 0 22px; font: 300 15px/29px 'Open Sans',Helvetica,Arial,sans-serif; cursor: pointer;" Text="SAVE" class="btn btn-primary" Visible="true" OnClick="btnSaveUp_Click" OnClientClick="javascript:return validate();" /> 
  
							</li>	
                            <li style="border-left: 0px!important;" id="liReset" runat="server" visible="false" class="sparks-info">
                                <asp:Button ID="btnResetUp" runat="server" Style="float: right; height: 31px;margin: 10px 0 0 5px;padding: 0 22px; font: 300 15px/29px 'Open Sans',Helvetica,Arial,sans-serif; cursor: pointer;" Text="RESET" class="btn btn-primary" Visible="true" OnClick="btnResetUp_Click" /> 
  
							</li>
                            <li style="border-left: 0px!important;" id="liBack" runat="server" class="sparks-info">
								 
                                <asp:Button ID="btnBackUp" runat="server" Text="BACK" Style="float: right; height: 31px;margin: 10px 0 0 5px;padding: 0 22px; font: 300 15px/29px 'Open Sans',Helvetica,Arial,sans-serif; cursor: pointer;" class="btn btn-primary" Visible="true" OnClick="btnBackUp_Click"  /> 
                                    
							</li>
                            
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
									<h2>New User</h2>
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
                                    <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6"> 
                                         <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                              <asp:HiddenField ID="hddnUserID" runat="server" />    
                                             <asp:HiddenField ID="hddnMode" runat="server" />                             
                                        <label class="label">User First Name</label>                                       
                                        <label class="input">  
                                        <asp:TextBox ID="txtUserFName" runat="server" title="User Name...."></asp:TextBox> 
                                       
                                             <b class="tooltip tooltip-top-right">
                                        <i class="fa fa-arrow-circle-down"></i> Enter User First Name</b>
                                       </label>                                    
                                       </div>
                                       <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">                                   
                                            <label class="label">User Last Name</label>                                       
                                            <label class="input">  
                                                <asp:TextBox ID="txtUserLName" runat="server" title="User Name...."></asp:TextBox> 
                                                     <b class="tooltip tooltip-top-right">
                                                <i class="fa fa-arrow-circle-down"></i> Enter Last User Name</b>
                                            </label>                                    
                                       </div>
                                       
                                                 <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">                                   
                                                 <label class="label">User ID</label>                                
                                                 <label class="input">  
                                                 <asp:TextBox ID="txtUserId" runat="server" AutoPostBack="false" title="User ID....." onkeypress="javascript:return ErrorClose1();" MaxLength="100"></asp:TextBox>                          
                                                     <asp:Label ID="lblMessage" runat="server" ForeColor="Red" Text=""></asp:Label> 
                                                     <asp:HiddenField ID="hddnMatch" runat="server" /> 
                                                 <b class="tooltip tooltip-top-right">
                                                 <i class="fa fa-arrow-circle-down"></i> Enter User ID</b> 
                                                 </label>
                                                 </div>
                                        
                                         <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">                        
                                    <label class="label">Password</label>                                       
                                    <label class="input">  
                                      <asp:TextBox ID="txtPassWord" runat="server" title="Password...."></asp:TextBox>                                    
                                        <asp:HiddenField ID="hdnPassword" runat="server" />
                                        <asp:HiddenField ID="hdnDistributor" runat="server" />
                                         <b class="tooltip tooltip-top-right">
                                        <i class="fa fa-arrow-circle-down"></i> Enter Password</b> 
                                       
                                    </label>                                  
                                </div>
                                         <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">                                   
                                    <label class="label">Confirm Password</label>                                       
                                    <label class="input">  
                                       <asp:TextBox ID="txtConfirmPass" runat="server" title="Password...."  onkeypress="javascript:return ErrorClose();"  onblur="javascript:return matchpassword();"></asp:TextBox> 
                                        
                                        <b class="tooltip tooltip-top-right">
                                        <i class="fa fa-arrow-circle-down"></i> Enter Confirm Password</b> 
                                        <asp:Label ID="lblmsg" runat="server" Text="Password not match" ForeColor="Red"  Style="display:none;"></asp:Label>
                                        
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
                                    </div>
                                    <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6"> 
                                         <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">                          
                                    <label class="label">Distributor</label>                                       
                                    <label class="input"> 
                                        <asp:HiddenField ID="hddnRole" runat="server" /> 
                                        <asp:DropDownList  class="chosen-select text-area" ID="ddlDistributor" runat="server">
                                              
                                              
                                        </asp:DropDownList>          
                                    </label>                                  
                                </div>
                                         <%--<div  class="col-xs-12 col-sm-12 col-md-12 col-lg-12">                                  
                                    <label class="label">User Role</label>                                       
                                    <label class="input">  
                                        <asp:DropDownList  class="text-area" ID="ddlRole" runat="server">
                                            
                                              
                                        </asp:DropDownList>          
                                    </label>                                  
                                </div>--%>
                                      
                                         <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">                                    
                                    <label class="label">Mobile Number</label>                                       
                                    <label class="input"> 
                                        <asp:TextBox ID="txtMobile" runat="server" title="Mobile Number" onkeypress="javascript:return onlynumeric(event,this);"></asp:TextBox>
                                       
                                        <b class="tooltip tooltip-top-right">
                                        <i class="fa fa-arrow-circle-down"></i> Enter Mobile Number</b>                                   

                                    </label>                                  
                                </div>
                                         <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">                                
                                    <label class="label">Active From</label>     <%--(dd/mm/yyyy) --%>                                 
                                    <label class="input">  
                                        <asp:TextBox ID="txtActiveFrom" runat="server" TextMode="Date"></asp:TextBox>
                                       
                                        <b class="tooltip tooltip-top-right">
                                        <i class="fa fa-arrow-circle-down"></i> Enter Active From Date</b>                                   

                                    </label>                                  
                                </div>
                                         <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">                                    
                                           <label class="label">Active To</label>                                       
                                           <label class="input">  
                                           <asp:TextBox ID="txtActiveTo" runat="server" TextMode="Date" ></asp:TextBox>
                                               <b class="tooltip tooltip-top-right">
                                        <i class="fa fa-arrow-circle-down"></i> Enter Active To Date</b>
                                           </label>                                  
                                         </div>
                                         <div id="divActive" runat="server" visible="false" class="col-xs-12 col-sm-12 col-md-12 col-lg-12">                                    
                                             <label class="label">Is User Active</label>                                       
                                             <label class="text"> 
                                             <asp:CheckBox ID="CheckBox1" runat="server" Checked="true" /> 
                                            </label>                                  
                                         </div>
                                  </div>
                                </div>               
                            </div>      

                             <div class="row">
                                <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">                                    
                                                                                                       
                                </div>
                                                 
                            </div>     
                        
                        </div>
                
                <footer class="boxBtnsubmit">
                        <asp:Button ID="btnBack" runat="server" Text="BACK" class="btn btn-primary" Visible="false" OnClick="btnBack_Click"    />
                        <asp:Button ID="btnReset" runat="server" Text="RESET" class="btn btn-primary" OnClick="btnReset_Click"/>
                          
                        <asp:Button ID="btnSave" runat="server" Text="SAVE" class="btn btn-primary" ValidationGroup="save" OnClientClick="javascript:return validate();" OnClick="btnSave_Click" />
                        <asp:Button ID="btnUpdate" runat="server" Text="UPDATE" class="btn btn-primary" OnClientClick="javascript:return validateUpdate();" OnClick="btnUpdate_Click"/>              
                           
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
