<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="approvals.aspx.cs" Inherits="ENK.approvals" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head  runat="server" >
  
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
          function validateControlsActivate() {
              debugger;


             

              if (document.getElementById("<%=txtpass.ClientID%>").value == "") {
                  alert("Password Can't be Blank");
                  document.getElementById("<%=txtpass.ClientID%>").focus();
                return false;
            }
             

            if (document.getElementById("<%=txtConfirmpass.ClientID%>").value == "") {
                alert("Confirm pass Can't be Blank");
                document.getElementById("<%=txtConfirmpass.ClientID%>").focus();
                return false;
            }


            var SIMCARD = document.getElementById("<%= txtpass.ClientID%>");
            var confirmSIMCARD = document.getElementById("<%=txtConfirmpass.ClientID%>");

            if (SIMCARD.value != confirmSIMCARD.value) {
                alert("Sorry ! Password  do not match.");
                document.getElementById("<%=txtConfirmpass.ClientID%>").focus();
                return false;
            }





            return true;
        }


    </script>
</head>
<body>
    <form id="form1" runat="server">
 
      
                                <div runat="server" id="divPassword" >
                                  

        <div id="main" role="main" >

			<!-- MAIN CONTENT -->
			<div id="content" class="container">
                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">   
                                   <h5> <label class="label  pull-right" style="color:#208ac4!important;font-size: large!important;">Reset Password</label></h5>
                     </div>
				<div class="row">
					 
                     
					<div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
						<div class="well no-padding">
							<div  id="login-form" class="smart-form client-form">
								 <div class="box-input">                                   
                            
                             <div class="row">
                                   
                                   
                                 <asp:HiddenField ID="hddnEmailid" runat="server" />
                                
                                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-6">   
                                    <label class="label">Password </label>
                                    <label class="input">   
                                    <asp:TextBox ID="txtpass"  title="Enter your SIM card number" TextMode="Password" runat="server" MaxLength="12" ></asp:TextBox>                           
                                     <b class="tooltip tooltip-top-right">
                                                <i class="fa fa-arrow-circle-down"></i> Password</b>    
                                    </label>
                                </div>  
                                   </div>    
                                   
                                   <div class="row">                         
                                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-6">                          
                                    <label class="label">Confirm Password</label>
                                    <label class="input">  
                                    <asp:TextBox ID="txtConfirmpass"  TextMode="Password"   title="Enter your ZIP code" runat="server"></asp:TextBox>                              
                                    <b class="tooltip tooltip-top-right">
                                    <i class="fa fa-arrow-circle-down"></i>Confirm password</b>    
                                    </label>
                                </div>
                                
                                       
                             </div>                              
                         </div>
                      
                           </div>
                            
                             
							 </div>
                            <footer class="boxBtnsubmit">
                               <asp:Button ID="Button1" runat="server" Text="Reset" class="btn btn-primary"  style="  float: left!important;"  OnClick="btnReasonforCancelled_Click"  OnClientClick="javascript:return validateControlsActivate();" />
                      
                           </footer>
						</div>
					</div>
				</div>
			</div>

         </div>

    </form>
</body>
</html>
