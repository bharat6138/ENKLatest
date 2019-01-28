<%@ Page Language="C#"  AutoEventWireup="true" CodeBehind="Userxyx.aspx.cs" Inherits="ENK.Userxyx" %>

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
        
        <link type="text/css" rel="stylesheet" href="Scripts1/jquery1.10.3-ui.css" />
        <script type="text/javascript" src="Scripts1/jquery-1.8.2.js"></script>
        <script type="text/javascript" src="Scripts1/jquery1.10.3-ui.js"></script>
        
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
               
              return false;
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
    
	</head>
	
	<body class="animated fadeInDown">
        <form id="form1" runat="server">
		<header id="header">
			<div id="logo-group">
				<span id="logo"><img src="img/logo.png" class="pull-right display-image" alt="" style="width:100px; max-height: 100px!important; margin-top:-27px!important"></span>			</div>
		</header>
     <div id="main" role="main"  style="margin-top: 0px!important;" >

			<!-- RIBBON -->
			 
			<!-- END RIBBON -->

			<!-- MAIN CONTENT -->
			<div id="content">
				<div class="row row-fullWidth">
					<div class="col-xs-5 col-sm-7 col-md-7 col-lg-4">
						<h1 class="page-title txt-color-blueDark">
							<i class="fa fa-user fa-fw"></i> 
								Admin
							<span>> 
								User List
							</span>
						</h1>
					</div>
					<div class="col-xs-7 col-sm-5 col-md-5 col-lg-8">
						<ul id="sparks" class="">
							<%--<li class="sparks-info">
								<a href="#" title="New">
                                	<span><i class="fa fa-file"></i></span> New
                               	</a>
							</li>--%>
							<%--<li class="sparks-info">
								<a href="#" title="Edit">
                                	<span><i class="fa fa-pencil-square-o"></i></span> Edit
                              	</a>
							</li>
							<li class="sparks-info">
								<a href="#" title="Trash">
                                	<span><i class="fa fa-trash-o"></i></span> Trash
                               	</a>
							</li>--%>
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
									<h2>User List</h2>

								</header>

								<!-- widget div-->
								<div>

									<!-- widget edit box -->
									<div class="jarviswidget-editbox">
										<!-- This area used as dropdown edit box -->

									</div>
									<!-- end widget edit box -->

									<!-- widget content -->
									<div class="widget-body"><%--style="max-height: 300px; overflow-y: auto;"--%>
										<div class="table-responsive" >										
											<%--style="overflow-y:scroll; height:300px;"  --%>
											<div style="overflow-y:scroll; height:300px;"  >
											 <asp:Repeater ID="RepeaterUserList" runat="server" >
                                              <HeaderTemplate>
                                              <table id="table123"   class="table table-bordered"  >
                                                <thead>
                                                    <tr>
                                                        <th>User ID</th>
                                                        <th>Pass</th>
                                                                                                       
                                                        
                                                    </tr>
                                                    <tbody>               
                                              </HeaderTemplate>
                                              <ItemTemplate>      
         
                                                <tr class="gradeA">
                                                    <td>
                                                    <asp:Label runat="server" ID="Label1" text='<%# Eval("Username") %>' /> 
                                                  
                                                                                           
                                                    </td>                    
                                                    <td>
                                                    <asp:Label runat="server" ID="Label2" text='<%# Eval("Pass") %>' />
                                                    </td>
                                                     
                                                     
                                                </tr>          
        
                                              </ItemTemplate>
    
                                              <FooterTemplate>
                                               </thead> 
                                               </table>
                                               </tbody>
                                              </FooterTemplate>
                                              </asp:Repeater>  
                                                 </div>
										</div>
                                         
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
          <div id="popupdiv" class="loading" title="Basic modal dialog" style="display:nonefont-family:Arial;font-size: 10pt; border: 2px solid #67CFF5;text-align: -webkit-center;">
        
      <b style="text-align:center!important;"> Please Wait...........</b>
        <br />
        <br />
        <img src="img/loader.gif" alt="" style="text-align:center!important;" />
    </div>
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
