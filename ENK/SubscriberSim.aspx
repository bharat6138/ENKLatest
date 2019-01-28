<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SubscriberSim.aspx.cs" Inherits="ENK.SubscriberSim" %>

<!DOCTYPE html>

<html lang="en-us" id="extr-page">
	<head>
		<meta charset="utf-8">
		<title> <%= ConfigurationManager.AppSettings["COMPANY_NAME"] %></title>
		<meta name="description" content="">
		<meta name="author" content="">
		<meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no">
		
		<!-- #CSS Links -->
		<!-- Basic Styles -->
		<link rel="stylesheet" type="text/css" media="screen" href="css/bootstrap.min.css">
		<link rel="stylesheet" type="text/css" media="screen" href="css/font-awesome.min.css">

		<!-- Ezeeway Styles : Please note (Ezeeway-production.css) was created using LESS variables -->
		<link rel="stylesheet" type="text/css" media="screen" href="css/ezeeway-production.min.css">
		<link rel="stylesheet" type="text/css" media="screen" href="css/ezeeway-skins.min.css">

		<!-- Ezeeway RTL Support is under construction
			 This RTL CSS will be released in version 1.5
		<link rel="stylesheet" type="text/css" media="screen" href="css/Ezeeway-rtl.min.css"> -->

		<!-- We recommend you use "your_style.css" to override Ezeeway
		     specific styles this will also ensure you retrain your customization with each Ezeeway update.
		<link rel="stylesheet" type="text/css" media="screen" href="css/your_style.css"> -->

		<!-- Demo purpose only: goes with demo.js, you can delete this css when designing your own WebApp -->
		<link rel="stylesheet" type="text/css" media="screen" href="css/demo.min.css">

		<!-- #FAVICONS -->
		<link rel="shortcut icon" href="img/favicon/favicon1.png" type="image/x-icon">
		<link rel="icon" href="img/favicon/favicon1.png" type="image/x-icon">

		<!-- #GOOGLE FONT -->
		<link rel="stylesheet" href="http://fonts.googleapis.com/css?family=Open+Sans:400italic,700italic,300,400,700">

		<!-- #APP SCREEN / ICONS -->
		<!-- Specifying a Webpage Icon for Web Clip 
			 Ref: https://developer.apple.com/library/ios/documentation/AppleApplications/Reference/SafariWebContent/ConfiguringWebApplications/ConfiguringWebApplications.html -->
		<link rel="apple-touch-icon" href="img/splash/sptouch-icon-iphone.png">
		<link rel="apple-touch-icon" sizes="76x76" href="img/splash/touch-icon-ipad.png">
		<link rel="apple-touch-icon" sizes="120x120" href="img/splash/touch-icon-iphone-retina.png">
		<link rel="apple-touch-icon" sizes="152x152" href="img/splash/touch-icon-ipad-retina.png">
		
		<!-- iOS web-app metas : hides Safari UI Components and Changes Status Bar Appearance -->
		<meta name="apple-mobile-web-app-capable" content="yes">
		<meta name="apple-mobile-web-app-status-bar-style" content="black">
		
		<!-- Startup image for web apps -->
		<link rel="apple-touch-startup-image" href="img/splash/ipad-landscape.png" media="screen and (min-device-width: 481px) and (max-device-width: 1024px) and (orientation:landscape)">
		<link rel="apple-touch-startup-image" href="img/splash/ipad-portrait.png" media="screen and (min-device-width: 481px) and (max-device-width: 1024px) and (orientation:portrait)">
		<link rel="apple-touch-startup-image" href="img/splash/iphone.png" media="screen and (max-device-width: 320px)">
        <script type="text/javascript">
            function ErrorClose() {
                var divError = document.getElementById("lblwarning");
                divError.style.display = "none";
                return false;
            }

            function ChkValidVal() {

                var user_email = document.getElementById("txtUserName");
                var user_password = document.getElementById("txtPassword");


                if (user_email.value.trim() == "") {
                    //alert("Please enter user name.");
                    var lbl = document.getElementById("lblwarning");
                    lbl.innerText = "Please enter user name.";
                    lbl.style.display = "block";
                    return false;
                }
                if (user_password.value.trim() == "") {
                    var lbl = document.getElementById("lblwarning");
                    lbl.innerText = "Please enter password.";
                    lbl.style.display = "block";
                    return false;
                }
                return true;
            }


    </script>
         <script type="text/javascript">
             function validateControlsActivate() {

                 if (document.getElementById("<%=txtSIMCARD.ClientID%>").value == "") {
                     alert("SIM CARD Can't be Blank");
                     document.getElementById("<%=txtSIMCARD.ClientID%>").focus();
                     return false;
                 }
                 var str= document.getElementById("<%=txtSIMCARD.ClientID%>").value
                 if (str.length <19) {
                     alert("Please fill correct SIM CARD number");
                     document.getElementById("<%=txtSIMCARD.ClientID%>").focus();
                     return false;
                 }
                 var str = "Hello World!";
                 var n = str.length;
                 if (document.getElementById("<%=txtZIPCode.ClientID%>").value == "") {
                     alert("ZIPCode Can't be Blank");
                     document.getElementById("<%=txtZIPCode.ClientID%>").focus(); txtAlternateNumber
                     return false;
                 }
                 if (document.getElementById("<%=txtCustomerName.ClientID%>").value == "") {
                     alert("CustomerName Can't be Blank");
                     document.getElementById("<%=txtCustomerName.ClientID%>").focus();
                     return false;
                 }
                 if (document.getElementById("<%=txtEmail.ClientID%>").value == "") {
                     alert("Email Can't be Blank");
                     document.getElementById("<%=txtEmail.ClientID%>").focus();
                     return false;
                 }

                 if (document.getElementById("<%=txtAddress.ClientID%>").value == "") {
                     alert("Address Can't be Blank");
                     document.getElementById("<%=txtAddress.ClientID%>").focus();
                     return false;
                 }
                 if (document.getElementById("<%=txtAlternateNumber.ClientID%>").value == "") {
                     alert("AlternateNumber Can't be Blank");
                     document.getElementById("<%=txtAlternateNumber.ClientID%>").focus();
                     return false;
                 }
                 return true;
             }

              
         
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

    <script type="text/javascript">
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

	</head>
	
	<body class="animated fadeInDown">
        <form id="form1" runat="server">
		<header id="header">
			<div id="logo-group">
				<span id="logo"><img src="img/logo.png" class="pull-right display-image" alt="" style="width:100px; max-height: 100px!important; margin-top:-27px!important"></span>			</div>
		</header>

		<div id="main" role="main">

			<!-- MAIN CONTENT -->
			<div id="content" class="container">
                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">   
                                   <h5> <label class="label  pull-right" style="color:#208ac4!important;font-size: large!important;">ACTIVATE SIM</label></h5>
                     </div>
				<div class="row">
					<%--<div class="col-xs-12 col-sm-12 col-md-5 col-lg-4 hidden-xs hidden-sm">
						<h1 class="txt-color-red login-header-big">AH Prepaid Inc</h1>
						<div class="hero" style="height: 150px!important;">
							<div class="pull-left login-desc-box-l">
								<h4 class="paragraph-header">SIM ACTIVATION</h4>
								<%--<div class="login-app-icons">
									<a href="javascript:void(0);" class="btn btn-danger btn-sm">Frontend Template</a>
									<a href="javascript:void(0);" class="btn btn-danger btn-sm">Find out more</a>								</div> 
							</div>
							
					   </div>

						<div class="row">
							<div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
								<h5 class="about-heading">About Ah Prepaid Inc - Are you up to date?</h5>
								<p>
									AH Prepaid Wholesale USA Preloaded Prepaid Sim Cards And Unlocked GSM Phones
                                    Our Company Established in 2011, A & H Prepaid Solution is a professional Wholesaller of 
                                    Prepaid Sim Cards in USA. Our main Networks include Lyca Mobile, Simple Mobile, 
                                    H2O Wireless,Red Pocket,Ultra Mobile and Net10.</p>
							</div>
							 <div class="col-xs-12 col-sm-12 col-md-6 col-lg-6">
								<h5 class="about-heading">Not just your average template!</h5>
								<p>
									Et harum quidem rerum facilis est et expedita distinctio. Nam libero tempore, cum soluta nobis est eligendi voluptatem accusantium!								</p>
							</div> 
						</div>
					</div>--%>
                       
					<div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
						<div class="well no-padding">
							<div  id="login-form" class="smart-form client-form">
								 <div class="box-input">
                                      <asp:Label ForeColor="Green" cssClass="label pull-right" ID="Label1" runat="server" Text="Download Subscriber App from Google Play Store"></asp:Label>
                          
                                            <%--<asp:HyperLink id="hyperlink1"  CssClass="label pull-right"   width="99px" ImageUrl="img/playstore.png" NavigateUrl="<%= ConfigurationManager.AppSettings["COMPANY_SUBSCRIBERAPPURL"] %>"          ToolTip  ="Download Subscriber  Android App"
                                     Target="_blank" runat="server"/>    --%>                      
                            <div class="row">
                                 <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">  
                                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-6">   
                                   <h5> <label class="label">Dear Subscriber</label></h5>
                                    </div>
                                    
                                </div>
                                </div>
                                     
                             <div class="row">
                                  <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">  
                                  <asp:HiddenField ID="hddnTariffType" runat="server" />
                                 <asp:HiddenField ID="hddnTariffTypeID" runat="server" />
                                 <asp:HiddenField ID="hddnTariffCode" runat="server" />
                                  <asp:HiddenField ID="hddnTariffAmount" runat="server" />
                                 <asp:HiddenField ID="hddnTariffID" runat="server" />
                                      <asp:HiddenField ID="hddnMonths" runat="server" />
                                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-6">   
                                    <label class="label">SIM CARD</label>
                                    <label class="input">   
                                    <asp:TextBox ID="txtSIMCARD"  title="Enter your SIM card number" runat="server" MaxLength="19" onkeypress="javascript:return onlynumeric(event,this);"></asp:TextBox>                           
                                     <b class="tooltip tooltip-top-right">
                                                <i class="fa fa-arrow-circle-down"></i> Enter SIM card number</b>    
                                    </label>
                                </div>  
                                      
                                             <div class="col-xs-12 col-sm-12 col-md-12 col-lg-6">   
                                    <label class="label">Regulatory fee<asp:Label  ID="Label13" ForeColor="Red" runat="server" Text=""></asp:Label>
                                           <asp:Label ID="lblRegulatory" runat="server" Text=""  ForeColor="Red"></asp:Label>     
                                    </label>
                                    <label class="input">   
                                    <asp:TextBox ID="txtRegulatry"  Readonly="true" Text="0"  runat="server" MaxLength="5" onkeypress="javascript:return onlynumeric(event,this);"></asp:TextBox>                           
                                     <b class="tooltip tooltip-top-right">
                                                <i class="fa fa-arrow-circle-down"></i> </b>    
                                    </label>
                                </div>                           
                                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-6">                          
                                    <label class="label">ZIP Code</label>
                                    <label class="input">  
                                    <asp:TextBox ID="txtZIPCode"   title="Enter your ZIP code" runat="server"></asp:TextBox>                              
                                    <b class="tooltip tooltip-top-right">
                                    <i class="fa fa-arrow-circle-down"></i> Enter ZIP Code</b>    
                                    </label>
                                </div>
                                 <div class="col-xs-12 col-sm-12 col-md-12 col-lg-6">                                   
                                    <label class="label">Customer Name<asp:Label ID="Label17" ForeColor="Red" runat="server" Text="*"></asp:Label></label>                                       
                                    <label class="input">  
                                         <asp:TextBox ID="txtCustomerName" runat="server" title="Customer Name"></asp:TextBox>                      
                                        <b class="tooltip tooltip-top-right">
                                        <i class="fa fa-arrow-circle-down"></i> Enter Customer Name</b> 
                                    </label>
                                </div>  
                                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-6">                                   
                                    <label class="label">Email</label>                                       
                                    <label class="input">  
                                         <asp:TextBox ID="txtEmail" runat="server" title="Email"></asp:TextBox>                      
                                        <b class="tooltip tooltip-top-right">
                                        <i class="fa fa-arrow-circle-down"></i> Enter Email</b> 
                                    </label>
                                </div>  
                                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-6">                                   
                                    <label class="label">Address</label>                                       
                                    <label class="input">  
                                         <asp:TextBox ID="txtAddress" runat="server" title="Address"></asp:TextBox>                      
                                        <b class="tooltip tooltip-top-right">
                                        <i class="fa fa-arrow-circle-down"></i> Enter Address</b> 
                                    </label>
                                </div>  
                                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-6">                                   
                                    <label class="label">Alternate Number </label>                                       
                                    <label class="input">  
                                         <asp:TextBox ID="txtAlternateNumber" runat="server" title="Alternate Number" onkeypress="javascript:return onlynumeric(event,this);"></asp:TextBox>                      
                                        <b class="tooltip tooltip-top-right">
                                        <i class="fa fa-arrow-circle-down"></i> Enter Alternate Number</b> 
                                    </label>
                                </div> 
                                <div id="divahannel" runat="server" class="col-xs-12 col-sm-6 col-md-6 col-lg-6">             
                                    <label class="label">Channel ID</label>
                                    <label class="input">  
                                    <asp:TextBox ID="txtChannelID"  placeholder="Channel ID" runat="server"></asp:TextBox>
                               
                                     <b class="tooltip tooltip-top-right">
                                                <i class="fa fa-arrow-circle-down"></i> Enter Channel ID</b>    
                                    </label>
                                </div>                             
                                <div id="divLanguage" runat="server" class="col-xs-12 col-sm-6 col-md-6 col-lg-6">   
                                <label class="label">Language</label>
                                <label class="input">  
                                <asp:DropDownList ID="ddlLanguage"  class="text-area" runat="server">
                                    <asp:ListItem Value="1" Text="English">
                                    </asp:ListItem>
                                    <asp:ListItem Value="2" Text="Spanish">
                                    </asp:ListItem>
                                </asp:DropDownList>
                                </label>
                                </div>
                                      </div>
                             </div>                              
                         </div>
                      
                           </div>
                            
                             
							 </div>
                            <footer class="boxBtnsubmit">
                               <asp:Button ID="btnSubscriber" class="btn btn-primary"  style="  float: right!important;" runat="server" Text="Activate Now"  OnClientClick="javascript:return validateControlsActivate();" OnClick="btnSubscriber_Click" />

                      
                           </footer>
						</div>
					</div>
				</div>
			</div>
		</div>

            <div id="popupdiv" class="loading" title="Basic modal dialog" style="display:nonefont-family:Arial;font-size: 10pt; border: 2px solid #67CFF5;text-align: -webkit-center;">
        
      <b style="text-align:center!important;"> Please Wait...........</b>
        <br />
        <br />
        <img src="img/loader.gif" alt="" style="text-align:center!important;" />
    </div>


		<!--================================================== -->	

		<!-- PACE LOADER - turn this on if you want ajax loading to show (caution: uses lots of memory on iDevices)-->
		<script src="js/plugin/pace/pace.min.js"></script>

	    <!-- Link to Google CDN's jQuery + jQueryUI; fall back to local -->
	    <script src="//ajax.googleapis.com/ajax/libs/jquery/2.0.2/jquery.min.js"></script>
		<script> if (!window.jQuery) { document.write('<script src="js/libs/jquery-2.0.2.min.js"><\/script>'); } </script>

	    <script src="//ajax.googleapis.com/ajax/libs/jqueryui/1.10.3/jquery-ui.min.js"></script>
		<script> if (!window.jQuery.ui) { document.write('<script src="js/libs/jquery-ui-1.10.3.min.js"><\/script>'); } </script>

		<!-- JS TOUCH : include this plugin for mobile drag / drop touch events 		
		<script src="js/plugin/jquery-touch/jquery.ui.touch-punch.min.js"></script> -->

		<!-- BOOTSTRAP JS -->		
		<script src="js/bootstrap/bootstrap.min.js"></script>

		<!-- JQUERY VALIDATE -->
		<script src="js/plugin/jquery-validate/jquery.validate.min.js"></script>
		
		<!-- JQUERY MASKED INPUT -->
		<script src="js/plugin/masked-input/jquery.maskedinput.min.js"></script>
		
		<!--[if IE 8]>
			
			<h1>Your browser is out of date, please update your browser by going to www.microsoft.com/download</h1>
			
		<![endif]-->

		<!-- MAIN APP JS FILE -->
		<script src="js/app.min.js"></script>

		<script type="text/javascript">
		    runAllForms();

		    $(function () {
		        // Validation
		        $("#login-form").validate({
		            // Rules for form validation
		            rules: {
		                email: {
		                    required: true,
		                    email: true
		                },
		                password: {
		                    required: true,
		                    minlength: 3,
		                    maxlength: 20
		                }
		            },

		            // Messages for form validation
		            messages: {
		                email: {
		                    required: 'Please enter your email address',
		                    email: 'Please enter a VALID email address'
		                },
		                password: {
		                    required: 'Please enter your password'
		                }
		            },

		            // Do not change code below
		            errorPlacement: function (error, element) {
		                error.insertAfter(element.parent());
		            }
		        });
		    });
		</script>
            </form>
	</body>
</html>
