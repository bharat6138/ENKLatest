<%@ Page Language="C#"  MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="SimReplacement.aspx.cs" Inherits="ENK.SimReplacement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
            if (document.getElementById("<%=txtNewSIMNumber.ClientID%>").value == "") {
                alert("New SIM Number Can't be Blank");
                document.getElementById("<%=txtNewSIMNumber.ClientID%>").focus();
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
							<i class="fa fa-lg fa-fw fa-pencil-square-o"></i> 
								Sim Provisioning
							<span> >
								SIM Replacement						</span>						</h1>
					</div>
					<div class="col-xs-12 col-sm-7 col-md-7 col-lg-8">
						<%--<ul id="sparks" class="">
							<li class="sparks-info">
								<a href="newpage.html" title="Save">
                                	<span><i class="fa fa-floppy-o"></i></span> Save                              	</a>							</li>
							<li class="sparks-info">
								<a href="#" title="Save &amp; Close">
                                	<span><i class="fa fa-times"></i></span> Save &amp; Close                              	</a>                            </li>
							<li class="sparks-info">
								<a href="#" title="Save &amp; New">
                                	<span><i class="fa fa-folder"></i></span> Save &amp; New                              	</a>                            </li>
                            <li class="sparks-info">
								<a href="#" title="Close">
                                	<span><i class="fa fa-times"></i></span> Close                                </a>                            </li>
						</ul>--%>
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
									<h2>New SIM Replacement</h2>
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
                                <div class="col-xs-12 col-sm-5 col-md-5 col-lg-5">                                    
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
                            <div class="row">
                                <div class="col-xs-12 col-sm-5 col-md-5 col-lg-5">                                   
                                    <label class="label">New SIM Number<asp:Label ID="Label17" ForeColor="Red" runat="server" Text="*"></asp:Label></label>                                       
                                    <label class="input">  
                                         <asp:TextBox ID="txtNewSIMNumber" runat="server" title="Contact Person"></asp:TextBox>                      
                                        <b class="tooltip tooltip-top-right">
                                        <i class="fa fa-arrow-circle-down"></i> Enter New SIM Number</b> 
                                    </label>
                                </div>  
                                <div class="col-xs-12 col-sm-7 col-md-7 col-lg-7">                                  
                                       
                                </div>     
                            </div>   
                            
                        </div>
                
                <footer class="boxBtnsubmit">
                     <asp:Button ID="btnResetAll" runat="server" Text="Reset" OnClick="btnResetAll_Click" class="btn btn-primary"   />
                     <asp:Button ID="btnReplace" runat="server" Text="Replace" OnClick="btnReplace_Click" class="btn btn-primary" OnClientClick="javascript:return validate();"  />              
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


