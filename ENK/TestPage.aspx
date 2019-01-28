<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestPage.aspx.cs" Inherits="ENK.TestPage" %>

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

		 

		<!-- Demo purpose only: goes with demo.js, you can delete this css when designing your own WebApp -->
		<link rel="stylesheet" type="text/css" media="screen" href="css/demo.min.css">

		<!-- #FAVICONS -->
		<%--<link rel="shortcut icon" href="img/favicon/favicon1.png" type="image/x-icon">
		<link rel="icon" href="img/favicon/favicon1.png" type="image/x-icon">--%>

		 
		<link rel="stylesheet" href="http://fonts.googleapis.com/css?family=Open+Sans:400italic,700italic,300,400,700">

		 
		 
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
             function getWidth() {
                 if (self.innerHeight) {

                     alert('1-'+self.innerWidth);
                     return false;
                 }

                 if (document.documentElement && document.documentElement.clientHeight) {
                     alert('2-' + document.documentElement.clientWidth)
                     return false;
                 }

                 if (document.body) {
                     alert('3-' + document.body.clientWidth);
                     return false;
                 }
             }

             function getHeight() {
                 if (self.innerHeight) {
                     alert('1-'+self.innerHeight)
                     return false;
                 }

                 if (document.documentElement && document.documentElement.clientHeight) {
                     alert('2' + document.documentElement.clientHeight);
                     return false;
                 }

                 if (document.body) {
                     alert('3' + document.body.clientHeight);
                     return false;
                 }
             }



    </script>
      
   

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
                                   <h5> <label class="label  pull-right" style="color:#208ac4!important;font-size: large!important;"></label></h5>
                     </div>
				<div class="row">
					<div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
						<div class="well no-padding">
							<div  id="login-form" class="smart-form client-form">
								 <div class="box-input">                                   
                                   <div class="row">
                                 <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">  
                                    <div class="col-xs-12 col-sm-12 col-md-12 col-lg-6">   
                                    
                                    </div>
                                    
                                 </div>
                            </div>
                                                           
                                 </div>
                      
                           </div>
                            
                             
							 </div>
                            <footer class="boxBtnsubmit">
                               <asp:Button ID="btnSubscriber" class="btn btn-primary"  style="float: right!important;" runat="server" Text="Submit Width"  OnClientClick="javascript:return getWidth();" />

                                <asp:Button ID="Button1" class="btn btn-primary"  style="float: left!important;" runat="server" Text="Submit Height"  OnClientClick="javascript:return getHeight();" />

                      
                           </footer>
						</div>
					</div>
				</div>
			</div>
		 
        </form>
	</body>
</html>

