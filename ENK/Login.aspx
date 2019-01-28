<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="ENK.Login" %>

<!DOCTYPE html>
<html lang="en-us" id="extr-page">
<head>
    <meta charset="utf-8">
    <title><%= ConfigurationManager.AppSettings["COMPANY_NAME"] %> </title>
    <meta name="description" content="">
    <meta name="author" content="">
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no">

   <!-- Basic Styles -->
   <%-- <link rel="stylesheet" type="text/css" media="screen" href="css/bootstrap.min - Copy.min.css" />--%>
    <link href="css/bootstrap1.min.css"  type="text/css" media="screen" rel="stylesheet" />
    <link rel="stylesheet" type="text/css" media="screen" href="css/font-awesome.min.css" />

    <!-- Ezeeway Styles : Please note (Ezeeway-production.css) was created using LESS variables -->
    <link rel="stylesheet" type="text/css" media="screen" href="css/ezeeway-production.min.css" />
    <link rel="stylesheet" type="text/css" media="screen" href="css/ezeeway-skins.min.css" />

    <!-- SmartAdmin Styles : Caution! DO NOT change the order -->
    <link rel="stylesheet" type="text/css" media="screen" href="css/smartadmin-production-plugins.min.css" />
    <link rel="stylesheet" type="text/css" media="screen" href="css/smartadmin-production.min.css" />
    <link rel="stylesheet" type="text/css" media="screen" href="css/smartadmin-skins.min.css" />

    <%-- Ezeeway RTL Support is under construction
			 This RTL CSS will be released in version 1.5
		<link rel="stylesheet" type="text/css" media="screen" href="css/Ezeeway-rtl.min.css"> --%>

    <%-- We recommend you use "your_style.css" to override Ezeeway
		     specific styles this will also ensure you retrain your customization with each Ezeeway update.
		<link rel="stylesheet" type="text/css" media="screen" href="css/your_style.css"> --%>

    <!-- Demo purpose only: goes with demo.js, you can delete this css when designing your own WebApp -->
    <link rel="stylesheet" type="text/css" media="screen" href="css/demo.min.css" />

    <style>
         
        .contct {
            -moz-border-radius: 2px;
            -webkit-border-radius: 2px;
            border-radius: 2px;
            cursor: default!important;
            display: inline-block;
            font-weight: 700;
            height: 30px;
            line-height: 24px;
            min-width: 30px;
            padding: 2px;
            text-align: center;
            text-decoration: none!important;
            -moz-user-select: none;
            -webkit-user-select: none;
            background-color: #f8f8f8;
            background-image: -webkit-gradient(linear,left top,left bottom,from(#f8f8f8),to(#f1f1f1));
            background-image: -webkit-linear-gradient(top,#f8f8f8,#f1f1f1);
            background-image: -moz-linear-gradient(top,#f8f8f8,#f1f1f1);
            background-image: -ms-linear-gradient(top,#f8f8f8,#f1f1f1);
            background-image: -o-linear-gradient(top,#f8f8f8,#f1f1f1);
            background-image: linear-gradient(top,#f8f8f8,#f1f1f1);
            border: 1px solid #bfbfbf;
            color: #6D6A69;
            font-size: 17px;
            margin: 0px 0 6px 0px;
        }
          .dropdown-menu {
               position: absolute;
   
    right: 30px !important;
    z-index: 1000;
    display: none;
    float: left;
    width: 36vw;
   
    padding: 4px 0;
    margin: 1px 0 0;
    list-style: none;
    font-size: 10px;
    text-align: left;
            }

                .dropdown-menu > li > a {
                    display: block;
                    padding: 3px 8px;
                    clear: both;
                    font-weight: 400;
                    line-height: 1.42857143;
                    color: #333;
                    white-space: nowrap;
                }
        body {
            margin: 0;
            padding: 0;
            min-height: 100%;
            direction: ltr;
        }

        #header {
            display: block;
            height: 49px;
            margin: 0;
            padding: 0 13px 0 0;
            background-color: #f3f3f3;
            background-image: -moz-linear-gradient(top,#f3f3f3,#e2e2e2);
            background-image: -webkit-gradient(linear,0 0,0 100%,from(#f3f3f3),to(#e2e2e2));
            background-image: -webkit-linear-gradient(top,#f3f3f3,#e2e2e2);
            background-image: -o-linear-gradient(top,#f3f3f3,#e2e2e2);
            background-image: linear-gradient(to bottom,#f3f3f3,#e2e2e2);
            background-repeat: repeat-x;
            position: relative;
            z-index: 905;
        }

        #extr-page #header {
            margin: 0;
            height: 71px;
            border-bottom: 1px solid #eee !important;
            overflow: visible;
            padding: 0 30px;
            border-width: 0;
            min-height: 28px;
            background: #f4f4f4 !important;
        }

        #logo {
            display: inline-block;
            margin-top: 13px;
            margin-left: 9px;
        }

        #logo-group *, .login-info, .login-info *, .minified .menu-item-parent {
            -webkit-box-sizing: content-box;
            -moz-box-sizing: content-box;
            box-sizing: content-box;
        }

        #logo-group > span {
            display: inline-block;
            /* height: 39px; */
            float: left;
        }

        #logo-group span {
            margin-top: 6px;
        }

        #extr-page #header #logo {
            margin-top: 14px;
        }

        #extr-page #main {
            margin-top: 0 !important;
        }

        #main {
            margin-left: 220px;
            padding: 0 0 52px;
            min-height: 500px;
        }

        #main {
            -webkit-transition: all .2s ease-out;
            transition: all .2s ease-out;
            background-color: #FFF;
        }

        #extr-page #main {
            padding-top: 20px;
        }

        #extr-page #main {
            background: #fff;
            margin: 0;
        }

        #content {
            padding: 10px 14px;
            position: relative;
        }

        #extr-page .container {
            border: none;
        }

        h1, h2, h3, h4 {
            margin: 0;
            font-family: "Open Sans",Arial,Helvetica,Sans-Serif;
            font-weight: 300;
        }

        h1 {
            letter-spacing: -1px;
            font-size: 24px;
            margin: 10px 0;
        }

        .txt-color-red {
            color: #a90329 !important;
        }

        #extr-page .login-header-big {
            font-weight: 400;
        }

        #extr-page .hero {
            width: 100%;
            float: left;
        }

        #extr-page .login-desc-box-l {
            min-height: 350px;
            width: 50%;
        }

        #extr-page h4.paragraph-header {
            color: #565656;
            font-size: 15px;
            font-weight: 400;
            line-height: 22px;
            margin-top: 15px;
            width: 270px;
        }

        #extr-page h5.about-heading {
            color: #565656;
            font-size: 15px;
            font-weight: 700;
            line-height: 24px;
            margin: 0 0 5px;
        }

        .well {
            background: #fbfbfb;
            border: 1px solid #ddd;
            box-shadow: 0 1px 1px #ececec;
            -webkit-box-shadow: 0 1px 1px #ececec;
            -moz-box-shadow: 0 1px 1px #ececec;
            position: relative;
        }

        .no-padding {
            padding: 0 !important;
        }

        .smart-form footer .btn {
            float: none !important;
        }

        .smart-form {
            margin: 0;
            outline: 0;
            color: #666;
            position: relative;
        }

            .smart-form *, .smart-form :after, .smart-form :before {
                margin: 0;
                padding: 0;
                box-sizing: content-box;
                -moz-box-sizing: content-box;
            }

            .smart-form header {
                display: block;
                padding: 8px 0;
                border-bottom: 1px dashed rgba(0,0,0,.2);
                background: #fff;
                font-size: 16px;
                font-weight: 300;
                color: #232323;
                margin: 10px 14px 0;
            }

        .client-form header {
            padding: 15px 13px;
            margin: 0;
            border-bottom-style: solid;
            border-bottom-color: rgba(0,0,0,.1);
            background: rgba(248,248,248,.9);
        }

        .smart-form footer {
            display: block;
            padding: 22px 14px 55px;
            border-top: 1px solid rgba(0,0,0,.1);
            background: rgba(248,248,248,.9);
        }

        .smart-form section {
            margin-bottom: 15px;
            position: relative;
        }

        .smart-form .label {
            display: block;
            margin-bottom: 6px;
            line-height: 19px;
            font-weight: 400;
            font-size: 13px;
            color: #333;
            text-align: left;
        }

        .smart-form .button, .smart-form .checkbox, .smart-form .input, .smart-form .radio, .smart-form .select, .smart-form .textarea, .smart-form .toggle {
            position: relative;
            display: block;
            font-weight: 400;
        }

        .smart-form .icon-append, .smart-form .icon-prepend {
            position: absolute;
            top: 5px;
            width: 22px;
            height: 22px;
            font-size: 14px;
            line-height: 22px;
            text-align: center;
        }

        .smart-form .icon-append {
            right: 5px;
            padding-left: 3px;
            border-left-width: 1px;
            border-left-style: solid;
        }

        .smart-form .checkbox i, .smart-form .icon-append, .smart-form .icon-prepend, .smart-form .input input, .smart-form .radio i, .smart-form .select select, .smart-form .textarea textarea, .smart-form .toggle i {
            border-color: #BDBDBD;
            transition: border-color .3s;
            -o-transition: border-color .3s;
            -ms-transition: border-color .3s;
            -moz-transition: border-color .3s;
            -webkit-transition: border-color .3s;
        }

        .smart-form .icon-append, .smart-form .icon-prepend {
            color: #A2A2A2;
        }

        .smart-form .input input, .smart-form .select select, .smart-form .textarea textarea {
            display: block;
            box-sizing: border-box;
            -moz-box-sizing: border-box;
            width: 100%;
            height: 32px;
            padding: 8px 10px;
            outline: 0;
            border-width: 1px;
            border-style: solid;
            border-radius: 0;
            background: #fff;
            font: 13px/16px 'Open Sans',Helvetica,Arial,sans-serif;
            color: #404040;
            appearance: normal;
            -moz-appearance: none;
            -webkit-appearance: none;
        }

        .smart-form .input .icon-append + input, .smart-form .textarea .icon-append + textarea {
            padding-right: 37px;
        }

        .note, .smart-form .note {
            margin-top: 12px;
            padding: 0 1px;
            font-size: 11px;
            line-height: 15px;
            color: #999;
        }

            .smart-form .note a {
                font-size: 13px;
            }

        .smart-form footer .btn {
            float: right;
            height: 31px;
            margin: 10px 0 0 5px;
            padding: 0 22px;
            font: 300 15px/29px 'Open Sans',Helvetica,Arial,sans-serif;
            cursor: pointer;
        }

        #content {
            padding: 0px !important;
        }
        .dropdown-menu {
            width: 46vw !important;
        }

            .dropdown-menu > li > a {
                font-size: 12px;
            }

        @media only screen and (max-width: 479px) and (min-width: 320px) {
            .display-image {
                width: 80px !important;
                padding-top: 8px;
                padding-left: 5px;
                    left: 73px !important;
            }
        }
    </style>
    <script type="text/javascript">
        function ErrorClose() {
            
            var divError = document.getElementById("lblwarning");
            divError.style.display = "none";

            var divReply = document.getElementById("btnReply");
            divReply.style.display = "none";
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

        function redirect() {
            //location.href = 'http://www.ENK.com/become-a-dealer.html';
            window.open('http://www.ENK.com/become-a-dealer.html');
            return false;
        }

        function validateResponse() {
          
            var response = document.getElementById("<%=txtHoldResponse.ClientID%>").value;
            if (/[^a-zA-Z0-9\-' '-\/]/.test(response)) {
                alert('Response can only contain alphanumeric characters, hyphens(-) and back slashs(\)');
                document.getElementById("<%=txtHoldResponse.ClientID%>").focus();
            }

        }
    </script>
      <script src="js/bootstrap/bootstrap.min.js"></script>
</head>

<body class="animated fadeInDown  desktop-detected pace-done" style="">
   
    <form id="form2" runat="server">


        <header id="header">

            <div id="logo-group">
                <img src="img/logologin.png" class="pull-left display-image" alt="" style="width: 100px; margin-top: 10px; max-height: 100px!important;">
                <img src="img/LycaMobile.png" class="pull-left display-image" alt="" style="width: 150px; width: 150px;
    margin-top: 7px;
    max-height: 100px!important;
    left: 155px;
    position: absolute;" hspace="20">
                        
              </div>
                <span class="ribbon-button-alignment pull-right">
                   <%-- <span class="project-selector dropdown-toggle" data-toggle="dropdown"><small style="color: blue;"> Contact Us </small><i class="fa fa-phone contct"></i></span>--%>

                    <!-- Suggestion: populate this list with fetch and push technique -->
                   <span class="project-selector dropdown-toggle" data-toggle="dropdown"><small style="color: #0061a0; font-size:14px;"><strong> Contact Us </strong> </small><i class="fa fa-phone contct"></i></span>
            
                    <!-- Suggestion: populate this list with fetch and push technique -->
                    <ul class="dropdown-menu">
                        <li>
                            <a href="javascript:void(0);"><strong>Customer service business hours -</strong> (Mon - Fri 9 AM - 8 PM) (SAT 10 AM - 6 PM) (SUN 10 AM - 3 PM)</a>
                        </li>
                        <li>
                            <a href="javascript:void(0);"><strong>Call Customer service -</strong>
                                <asp:Label ID="Label4" runat="server" Text=" +1 (213) 213 5880"></asp:Label></a>
                        </li>
                        <li>
                            <a href="javascript:void(0);"><strong>Email us -</strong>
                                <asp:Label ID="Label5" runat="server" Text=""><%= ConfigurationManager.AppSettings["COMPANY_EMAIL"] %></asp:Label></a>

                        </li>
                      

                        <%--<li class="divider"></li>
<li>
<a href="javascript:void(0);"><i class="fa fa-power-off"></i> Clear</a>
</li>--%>
                    </ul>
                </span>
            

        </header>

        <div id="main" role="main">

            <!-- MAIN CONTENT -->
            <div id="content" class="container">

                <div class="row">
                    <div class="col-xs-12 col-sm-12 col-md-7 col-lg-8 hidden-xs hidden-sm">


                        <%--<div class="col-xs-12 col-sm-12 col-md-12 col-lg1-12">

                            <img src="img/3.jpg" class="" alt="" style="margin-bottom: 0px!important; width: 100%;">
                        </div>
                        <div class="col-xs-12 col-sm-12 col-md-12 col-lg1-12">

                            <img src="img/6.jpg" class="" alt="" style="margin-top: 10px!important; width: 100%;">
                        </div>
                        <div class="col-xs-12 col-sm-12 col-md-12 col-lg1-12">

                            <img src="img/4.jpg" class="" alt="" style="margin-top: 10px!important; width: 100%;">
                        </div>--%>

                        <div class="col-xs-12 col-sm-12 col-md-12 col-lg1-12" id="DivImg1" runat="server" visible="false">
                            <asp:Image ID="Img1" runat="server" class="" alt="" Style="margin-top: 10px!important; width: 100%;" />
                        </div>

                        <div class="col-xs-12 col-sm-12 col-md-12 col-lg1-12" id="DivImg2" runat="server" visible="false">
                            <asp:Image ID="Img2" runat="server" class="" alt="" Style="margin-top: 10px!important; width: 100%;" />
                        </div>

                        <div class="col-xs-12 col-sm-12 col-md-12 col-lg1-12" id="DivImg3" runat="server" visible="false">
                            <asp:Image ID="Img3" runat="server" class="" alt="" Style="margin-top: 10px!important; width: 100%;" />
                        </div>

                    </div>
                   
                    <div class="col-xs-12 col-sm-12 col-md-5 col-lg-4">
                        <div class="well no-padding">
                            <div id="login-form" class="smart-form client-form">
                                <header>
                                    Sign In							
                                </header>


                                <feildset>
									<section style="padding: 14px 12px 0 12px!important;">
										<label class="label"> &nbsp;Username</label>
										<label class="input"> <i class="icon-append fa fa-user"></i>
											 
                                            <asp:TextBox ID="txtUserName" runat="server" onblur="javascript:ErrorClose();" TabIndex="1"></asp:TextBox>
											<b class="tooltip tooltip-top-right"><i class="fa fa-user txt-color-teal"></i> Please enter email address/username</b></label>
									</section>

									<section style="padding: 14px 12px 0 12px!important;">
										<label class="label"> &nbsp;Password</label>
										<label class="input"> <i class="icon-append fa fa-lock"></i>
											 
                                            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" onblur="javascript:ErrorClose();" TabIndex="2"></asp:TextBox>
											<b class="tooltip tooltip-top-right"><i class="fa fa-arrow-circle-down"></i> Enter your password</b> </label>
										<div class="note">
											<a href="ForgetPassword.aspx">Forgot password?</a>										</div>
									</section>
                                
									
                                
									
								 </feildset>
                                <footer style="text-align: right">
                                    <section>
                                        <asp:Label ID="lblwarning" runat="server" Text="Invalid UserID and Password" Style="display: none;" ForeColor="Red"></asp:Label>
                                    </section>

                                    <asp:Button ID="Button1" runat="server" class="btn btn-primary" Text="Sign in" TabIndex="3" OnClick="Button1_Click" OnClientClick="javascript:return ChkValidVal();" />


                                    <span style="float: left !important;">
                                        <asp:Button ID="btnReply" runat="server" class="btn btn-primary" Text="Reply to Admin" Style="display: none;" TabIndex="2" OnClick="btnReply_Click" />
                                    </span>

                                    <br />
                                    <a runat="server" href="https://www.lycamobile.us/en/activate-sim/" target="_blank" tabindex="6" class="btn btn-primary" style="float: right !important;">Activate Preloaded SIM</a>

                                </footer>
                            </div>
                        </div>
                        <div class="col-xs-12 col-sm-12 col-md-12 col-lg1-12">

                            <img src="img/7.jpg" class="" alt="" style="margin-top: 0px!important; width: 100%;">
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div style="display: none; z-index: 2; border: 1px solid rgba(0,0,0,.1); background: #fbfbfb; position: fixed; top: 44vh; padding: 18px 15px; left: 20vw; width: 493px; border-radius: 6px;"
            id="DivHold">


            <div class="col-md-12 text-left">
                <strong>Attach Document:</strong>
                <asp:FileUpload ID="FileUpload1" runat="server" />
            </div>
            <br />
            <div class="col-md-12 text-left">
                <strong>Response :</strong>
                <asp:TextBox ID="txtHoldResponse" Style="width: 100%; height: 62px;" TextMode="MultiLine" runat="server" onblur="validateResponse();"></asp:TextBox>
            </div>
            <br />
            <div class="col-md-12 text-right">
                <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-primary btn-sm" Text="Submit" OnClick="btnSubmit_Click" OnClientClick="javascript: return confirm('Are you sure to continue?');" />
                <asp:Button ID="btnClose" runat="server" CssClass="btn btn-primary btn-sm" Text="Close" OnClientClick="resetfield();" />
            </div>

        </div>
        <!--================================================== -->





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
        <script type="text/javascript">
            function HyperLinkClick() {
                debugger;

                document.getElementById('DivHold').style.display = "block";

            }
        </script>
    </form>
</body>
</html>
