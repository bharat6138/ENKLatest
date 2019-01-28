

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SubscriberRecharge.aspx.cs" Inherits="ENK.SubscriberRecharge" %>

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
        <link rel='stylesheet prefetch' href='https://cdn.rawgit.com/claviska/jquery-alertable/master/jquery.alertable.css'/>

        




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
                 
                


                 if (document.getElementById('<%=ddlNetwork.ClientID%>').selectedIndex == 0) {
                     //alert('Please Select  Network');
                     $.alertable.alert('Please Select  Network!').always(function () {
                         console.log('Alert dismissed');
                         document.getElementById("<%=ddlNetwork.ClientID%>").focus();
                    });
                     return false;
                 }

                 if (document.getElementById('<%=ddlProduct.ClientID%>').selectedIndex == 0) {
                     //alert('Please Select  Network');
                     $.alertable.alert('Please Select Tariff / Product').always(function () {
                         console.log('Alert dismissed');
                         document.getElementById("<%=ddlProduct.ClientID%>").focus();
                     });
                     return false;
                 }

                  

                 if (document.getElementById("<%=txtMobileNo.ClientID%>").value == "") {
                     //alert("Mobile Number Can't be Blank");
                     $.alertable.alert('Mobile Number Can not be Blank').always(function () {
                         console.log('Alert dismissed');
                         document.getElementById("<%=txtMobileNo.ClientID%>").focus();
                });
                     return false;
                 }


                 //"Ultra Mobile"=17
                 var Network = document.getElementById('<%=ddlNetwork.ClientID%>');
                 debugger;
                 if (Network.value == "17") {
                     var str = document.getElementById("<%=txtMobileNo.ClientID%>").value
                     if (str.length > 10) {
                         //alert("Sorry ! Mobile Number Can't be maximum 10 digit");
                         $.alertable.alert('Sorry ! Mobile Number Can not be  maximum 10 digit.').always(function () {
                             console.log('Alert dismissed');
                             document.getElementById("<%=txtMobileNo.ClientID%>").focus();
                          });
                         
                         return false;
                     }

                 }
                 if (document.getElementById("<%=txtplanAmount.ClientID%>").value == "") {
                     // alert("Amount Can't be Blank");
                     $.alertable.alert('Amount Can not be Blank').always(function () {
                         console.log('Alert dismissed');
                         document.getElementById("<%=txtplanAmount.ClientID%>").focus();
                     });
                     return false;
                 }


                 // fOR AT&T Plan
                 if (Network.value == "18") {
                     debugger;
                     
                     var Product = document.getElementById('<%=ddlProduct.ClientID%>').value;
                     var str = document.getElementById("<%=txtplanAmount.ClientID%>").value
                    
                     if (Product == "8816130") {

                         if (parseFloat(str) < 30 || parseFloat(str) > 30) {
                            
                             $.alertable.alert('Sorry ! Recharge Amount Can not be  greter than or less than 30$').always(function () {
                                 console.log('Alert dismissed');
                                 document.getElementById("<%=txtplanAmount.ClientID%>").focus();
                             });

                             return false;
                         }
                     }
                 
                     else if (Product == "8815999") {
                         
                         //x >= 0.001 && x <= 0.009
                         if (parseFloat(str)<10 || parseFloat(str) >14.99) {
                             $.alertable.alert('Sorry ! Recharge Amount must be enter between 10 And 14.99$ in this Plan').always(function () {
                                 console.log('Alert dismissed');
                                 document.getElementById("<%=txtplanAmount.ClientID%>").focus();
                             });

                             return false;
                         }
                     }
                     else if (Product == "8816000") {
                          
                             if (parseFloat(str)<15 || parseFloat(str) >450) {
                             //alert("Sorry ! Mobile Number Can't be maximum 10 digit");
                             $.alertable.alert('Sorry ! Recharge Amount must be enter between 15 And 450$ in this Plan').always(function () {
                                 console.log('Alert dismissed');
                                 document.getElementById("<%=txtplanAmount.ClientID%>").focus();
                             });

                             return false;
                         }
                     }




                 }




                 if (document.getElementById("<%=txtConfirmSIMCARD.ClientID%>").value == "") {
                     $.alertable.alert('Confirm Mobile Number Can not be Blank').always(function () {
                         console.log('Alert dismissed');
                         document.getElementById("<%=txtConfirmSIMCARD.ClientID%>").focus();
                     });
                     return false;
                 }



                 if (document.getElementById('<%=ddlState.ClientID%>').selectedIndex == 0) {
                     //alert('Please Select Your State');
                     $.alertable.alert('Please Select Your State').always(function () {
                         console.log('Alert dismissed');
                         document.getElementById("<%=ddlState.ClientID%>").focus();
 });
                     return false;
                 }
                 if (document.getElementById("<%=txtzipcode.ClientID%>").value == "") {
                    // alert("Zip Code Can't be Blank");
                     $.alertable.alert('Zip Code Can not be Blank').always(function () {
                         console.log('Alert dismissed');
                     document.getElementById("<%=txtzipcode.ClientID%>").focus();
                     });
                     return false;
                 }

                

                 if (document.getElementById("<%=txtEmail.ClientID%>").value == "") {
                     // alert("EmailID Can't be Blank");
                     $.alertable.alert('EmailID Can not be Blank').always(function () {
                         console.log('Alert dismissed');
                         document.getElementById("<%=txtEmail.ClientID%>").focus();
                       });
                     
                     return false;
                 }


                 var email = document.getElementById("<%=txtEmail.ClientID%>");
                 var filter = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;

                 if (!filter.test(email.value)) {
                    // alert('Please Enter a valid EmailID address');

                     $.alertable.alert('Please Enter a valid EmailID address').always(function () {
                         console.log('Alert dismissed');
                         document.getElementById("<%=txtEmail.ClientID%>").focus();
                      });
                     email.focus;
                     return false;
                 }







                 //  var SIMCARD = document.getElementById("<%= txtMobileNo.ClientID%>");
                 // var confirmSIMCARD = document.getElementById("<%=txtConfirmSIMCARD.ClientID%>");

                 var SIMCARD = document.getElementById("txtMobileNo").value;
                 var confirmSIMCARD = document.getElementById("txtConfirmSIMCARD").value;
                 if (SIMCARD != confirmSIMCARD) {
                     //alert("Sorry ! Mobile Number  do not match.");

                     $.alertable.alert('Sorry ! Mobile Number  do not match.').always(function () {
                         console.log('Alert dismissed');
                         document.getElementById("<%=txtConfirmSIMCARD.ClientID%>").focus();
                     });
                    
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


         <link href="css/plugins/chosen/chosen.css" rel="stylesheet"/>
    <style>
       .chosen-container-single .chosen-single {
    position: relative;
    display: block;
    overflow: hidden;
    padding: 0 0 0 2px;
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

        }

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
                   
                                   <h5> <label class="label  pull-right" style="color:#208ac4!important;font-size: large!important;">Recharge</label></h5>
                     <asp:Label ForeColor="Green" cssClass="label pull-right" ID="Label12" runat="server" Text="Download Subscriber App from Google Play Store"></asp:Label>
                          
                    <asp:HyperLink id="hyperlink2"   width="99px" ImageUrl="img/playstore.png" NavigateUrl="<%= ConfigurationManager.AppSettings["COMPANY_SUBSCRIBERAPPURL"] %>"          ToolTip  ="Download Subscriber  Android App"
                                     Target="_blank" runat="server"/>   
                    <label class="col-xs-12 col-sm-12 col-md-12 col-lg-12" style="color:black;FONT-SIZE: 13PX;">If your online recharge don’t go through. Please don’t retry on other website. Please send us email info@ENK.com or call us&nbsp; 209 297-3200.&nbsp;&nbsp; Text us:&nbsp; 209 890 8006</label>

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

                                   



                                        <div id="div1" runat="server" class="col-xs-12 col-sm-12 col-md-12 col-lg-6">                                  
                                <label class="label">Network <asp:Label ID="Label1" ForeColor="Red" runat="server" Text="*"></asp:Label>      </label>                                       
                                <label class="input"> 
                                <asp:DropDownList ID="ddlNetwork" class="form-control chosen-select text-area" runat="server" OnSelectedIndexChanged="ddlNetwork_SelectedIndexChanged"  AutoPostBack="true" >
                                     
                                </asp:DropDownList>                            
                                </label>                                  
                                </div>



                                      <div id="divProduct"  runat="server" class="col-xs-12 col-sm-12 col-md-12 col-lg-6">                                  
                                           
                                          <label class="label">Tariff <asp:Label ID="Label10" ForeColor="Red" runat="server" Text="*"></asp:Label>      </label>  
                                         
                                           
                                          
                                                                 
                                <label class="input"> 
                                <asp:DropDownList ID="ddlProduct"  class="form-control chosen-select text-area"  runat="server"  OnSelectedIndexChanged="ddlProduct_SelectedIndexChanged" AutoPostBack ="true" >
                                     
                                </asp:DropDownList>                            
                                </label>                                  
                                </div>

                                     <div class="clearfix"></div>
                                      
                                     
                                       <div class="col-md-6 col-xs-12" >   
                                    <label class="label">Mobile Number<asp:Label ID="Label7" ForeColor="Red" runat="server" Text="*"></asp:Label>  <asp:Label ID="Label2" ForeColor="Green" runat="server" Text="Ex-0123456789"></asp:Label> </label>
                                    <label class="input">   
                                    <asp:TextBox ID="txtMobileNo"  title="Enter your Mobile Number" runat="server"  MaxLength="10" onkeypress="javascript:return onlynumeric(event,this);"></asp:TextBox>                           
                                     <b class="tooltip tooltip-top-right">
                                                <i class="fa fa-arrow-circle-down"></i> Enter Mobile Number</b>    
                                    </label>
                                </div> 

                                      <div class="col-md-6 col-xs-12" >   
                                    <label class="label">Confirm Mobile Number<asp:Label ID="Label6" ForeColor="Red" runat="server" Text="*"></asp:Label></label>
                                    <label class="input">   
                                    <asp:TextBox ID="txtConfirmSIMCARD"  title="Enter your Mobile Number" runat="server" MaxLength="10" onkeypress="javascript:return onlynumeric(event,this);"></asp:TextBox>                           
                                     <b class="tooltip tooltip-top-right">
                                                <i class="fa fa-arrow-circle-down"></i> Enter Confirm Mobile Number</b>    
                                    </label>
                                </div> 


                                         <div id="div2" runat="server" class="col-md-6 col-xs-12">                                  
                                <label class="label">State <asp:Label ID="Label4" ForeColor="Red" runat="server" Text="*"></asp:Label>      </label>                                       
                                <label class="input"> 
                                <asp:DropDownList ID="ddlState" class="form-control chosen-select text-area" runat="server" OnSelectedIndexChanged="ddlSate_SelectedIndexChanged"  AutoPostBack="true" >
                                     
                                </asp:DropDownList>                            
                                </label>                                  
                                </div> 
                                    
                                       <div class="col-md-6 col-xs-12" >   
                                    <label class="label">Zip Code<asp:Label ID="Label5" ForeColor="Red" runat="server" Text="*"></asp:Label></label>
                                    <label class="input">   
                                    <asp:TextBox ID="txtzipcode"  title="Enter your zip Code" runat="server" MaxLength="7" onkeypress="javascript:return onlynumeric(event,this);"></asp:TextBox>                           
                                     <b class="tooltip tooltip-top-right">
                                                <i class="fa fa-arrow-circle-down"></i> Enter your zip Code</b>    
                                    </label>
                                </div> 
                                        <div class="clearfix"></div>
                                       <div class="col-md-6 col-xs-12">   
                                    <label class="label">Plan Amount<asp:Label  ID="Label8" ForeColor="Red" runat="server" Text=""></asp:Label>
                                         
                                    </label>
                                    <label class="input">   
                                    <asp:TextBox ID="txtplanAmount"  AutoPostBack="true" OnTextChanged="txtplanAmount_TextChanged"   runat="server" MaxLength="7" onkeypress="javascript:return onlynumericDecimal(event,this);"></asp:TextBox>                           
                                     <b class="tooltip tooltip-top-right">
                                                <i class="fa fa-arrow-circle-down"></i>  </b>    
                                    </label>
                                </div> 
                                       <div class="col-md-6 col-xs-12">   
                                    <label class="label">State Tax<asp:Label  ID="Label11" ForeColor="Red" runat="server" Text=""></asp:Label>
                                         <asp:Label ID="lblSurCharge" runat="server" Text=""  ForeColor="Red"></asp:Label>  
                                    </label>
                                    <label class="input">   
                                    <asp:TextBox ID="txtStateTax"  Readonly="true"   runat="server" MaxLength="5" onkeypress="javascript:return onlynumeric(event,this);"></asp:TextBox>                           
                                     <b class="tooltip tooltip-top-right">
                                                <i class="fa fa-arrow-circle-down"></i>  </b>    
                                    </label>
                                </div> 
                                       <div class="col-md-6 col-xs-12">   
                                    <label class="label">Regulatory fee<asp:Label  ID="Label13" ForeColor="Red" runat="server" Text=""></asp:Label>
                                           <asp:Label ID="lblRegulatory" runat="server" Text=""  ForeColor="Red"></asp:Label>     
                                    </label>
                                    <label class="input">   
                                    <asp:TextBox ID="txtRerulatry"  Readonly="true"  runat="server" MaxLength="5" onkeypress="javascript:return onlynumeric(event,this);"></asp:TextBox>                           
                                     <b class="tooltip tooltip-top-right">
                                                <i class="fa fa-arrow-circle-down"></i> </b>    
                                    </label>
                                </div> 
                                       <div class="col-md-6 col-xs-12">   
                                    <label class="label">Total Amount to Pay<asp:Label  ID="Label9" ForeColor="Red" runat="server" Text="*"></asp:Label>
                                        
                                    </label>
                                    <label class="input">   
                                    <asp:TextBox ID="txtAmount"  Readonly="true"  runat="server" MaxLength="5" onkeypress="javascript:return onlynumeric(event,this);"></asp:TextBox>                           
                                     <b class="tooltip tooltip-top-right">
                                                <i class="fa fa-arrow-circle-down"></i>  </b>    
                                    </label>
                                </div> 
                                      
                                      
                                      
                                      <div class="col-md-6 col-xs-12">                                   
                                    <label class="label">EmailID<asp:Label ID="Label3" ForeColor="Red" runat="server" Text="*"></asp:Label></label>                                       
                                    <label class="input">  
                                         <asp:TextBox ID="txtEmail" runat="server" placeholder="abc@gmail.com"  title="Email"></asp:TextBox>                      
                                        <b class="tooltip tooltip-top-right">
                                        <i class="fa fa-arrow-circle-down"></i> Enter Email</b> 
                                    </label>
                                </div> 
                                       
                                          
                                       </div>
                             </div>                              
                         </div>
                      
                           </div>
                            
                             
							 </div>
                            <footer class="boxBtnsubmit">
                               <asp:Button ID="btnSubscriber" Visible="false" class="btn btn-primary"  style="  float: right!important;" runat="server" Text="Recharge Now"  OnClientClick="javascript:return validateControlsActivate();" OnClick="btnSubscriber_Click" />

                             <asp:ImageButton ID="btnPaypal" runat="server" src='img/BuyNow.JPG' class="btn btn-primary pul-left" border='0' align='top' alt='Check out with PayPal'     OnClientClick="javascript:return validateControlsActivate();" OnClick="btnPaypal_Click" Style="padding: 0px!important; background-color: white!important; width: 165px; margin-top: -25px; border-color: white!important;" />
 
 
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
            
     <script src="js/jquery-2.1.1.js"></script>
    <script src="js/plugin/chosen/chosen.jquery.js"></script> 
  <script src='https://cdnjs.cloudflare.com/ajax/libs/velocity/1.2.3/velocity.min.js'></script>
<script src='https://cdnjs.cloudflare.com/ajax/libs/velocity/1.2.3/velocity.ui.min.js'></script>
<script src='https://rawgit.com/claviska/jquery-alertable/master/jquery.alertable.min.js'></script>
    <script type="text/javascript">



        var config = {
            '.chosen-select': {},
            '.chosen-select-deselect': { allow_single_deselect: true },
            '.chosen-select-no-single': { disable_search_threshold: 10 },
            '.chosen-select-no-results': { no_results_text: 'Oops, nothing found!' },
            '.chosen-select-width': { width: "100%" }
        }
        for (var selector in config) {
            $(selector).chosen(config[selector]);
        }
    </script>

            </form>
	</body>
</html>
