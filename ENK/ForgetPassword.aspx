<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ForgetPassword.aspx.cs" Inherits="ENK.ForgetPassword" %>

<!DOCTYPE html>

<html lang="en-us" id="extr-page">
	<head>
		<meta charset="utf-8">
			<title> ENK WIRELESS INC. </title>
		<meta name="description" content="">
		<meta name="author" content="">
		<meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no">
		
		<!-- #CSS Links -->
		<!-- Basic Styles -->
		<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css">
		<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css">

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
    border-bottom: 1px solid #eee!important;
    overflow: hidden;
    padding: 0 30px;
    border-width: 0;
    min-height: 28px;
    background: #f4f4f4!important;
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

#logo-group>span {
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
    margin-top: 0!important;
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
    color: #a90329!important;
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
    padding: 0!important;
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
.smart-form .input .icon-append+input, .smart-form .textarea .icon-append+textarea {
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
            function ErrorClose() {
                var divError = document.getElementById("lblwarning");
                divError.style.display = "none";
                return true;
            }

            function ChkValidVal() {

                var user_email = document.getElementById("txtUserID");
                //var user_password = document.getElementById("txtMobile");


                if (user_email.value.trim() == "") {
                    //alert("Please enter user name.");
                    var lbl = document.getElementById("lblwarning");
                    lbl.innerText = "Please enter user name.";
                    lbl.style.display = "block";
                    return false;
                }
                //if (user_password.value.trim() == "") {
                //    var lbl = document.getElementById("lblwarning");
                //    lbl.innerText = "Please enter Registered Mobile Number.";
                //    lbl.style.display = "block";
                //    return false;
                //}
                return true;
            }


    </script>
	</head>
	
	<body class="animated fadeInDown">
        <form id="form1" runat="server">
		<header id="header"> 
 
			<div id="logo-group">
				<img src="img/logologin.png" class="pull-left display-image" alt="" style="width:100px; margin-top: 10px;max-height: 100px!important;" >
                <img src="img/LycaMobile.png" class="pull-left display-image" alt="" style="width:150px; margin-top: 10px;max-height: 100px!important;" hspace ="20" >
			</div>
            
		</header>

		<div id="main" role="main">

			<!-- MAIN CONTENT -->
			<div id="content" class="container">

				<div class="row">
					<div class="col-xs-12 col-sm-12 col-md-7 col-lg-8 hidden-xs hidden-sm">
						  
                        
                        <div class="col-xs-12 col-sm-12 col-md-12 col-lg1-12">
                            
							<img src="img/3.jpg" class="" alt="" style="margin-bottom: 0px!important;width: 100%;">						
                            
						  </div>
                        <div class="col-xs-12 col-sm-12 col-md-12 col-lg1-12">
                            
							<img src="img/6.jpg" class="" alt="" style="margin-top: 10px!important;width: 100%;">						
                            
						  </div>
                        <div class="col-xs-12 col-sm-12 col-md-12 col-lg1-12">
                            
							<img src="img/4.jpg" class="" alt="" style="margin-top: 10px!important;width: 100%;">						
                            
						  </div>

					</div>
					<div class="col-xs-12 col-sm-12 col-md-5 col-lg-4">
						<div class="well no-padding">
							<div  id="login-form" class="smart-form client-form">
								<header>
									Forget Password								</header>

							 
									<fieldset>
									<section style="padding: 14px 12px 0 12px!important;">
										<label class="label"> &nbsp;Username</label>
										<label class="input"> 
											 
                                            <asp:TextBox ID="txtUserID" runat="server" MaxLength="100" onblur="javascript:ErrorClose();"></asp:TextBox>
											<b class="tooltip tooltip-top-right"><i class="fa fa-arrow-circle-down"></i> Please enter Username</b></label>
									</section>

									 <section style="padding: 14px 12px 0 12px!important;">
										<%--<label class="label"> &nbsp;Mobile Number</label>
										<label class="input">  
											 
                                            <asp:TextBox ID="txtMobile" runat="server"   MaxLength="20" onblur="javascript:ErrorClose();"></asp:TextBox>
											<b class="tooltip tooltip-top-right"><i class="fa fa-fw fa-arrow-circle-down"></i>Please Enter your registerd Mobile Number</b> </label>--%>
										<div class="note">
											<a href="Login.aspx">Signin</a>										</div>
									</section>
                                
									<section>
										<%--<label class="checkbox">
                                            <asp:CheckBox ID="CheckBox1" runat="server" class="checkbox"    />
											<input type="checkbox" name="remember" checked="">
											<i></i>Stay signed in</label>--%>
									</section>
								 </feildset>
								<footer>
                                    <section>
                                        <asp:Label ID="lblwarning" runat="server"   Text="Invalid UserID and Password" Style="display:none;" ForeColor="Red"></asp:Label>
                                        </section>
                                    <asp:Button ID="btnforgetPass" runat="server"  class="btn btn-primary" Text="Recover Password" OnClick="btnforgetPass_Click" OnClientClick="javascript:return ChkValidVal();" />
									 
								</footer>
							 </div>
						</div>
                        <div class="col-xs-12 col-sm-12 col-md-12 col-lg1-12">
                            
							<img src="img/7.jpg" class="" alt="" style="margin-top: 0px!important;width: 100%;">						
                            
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
