
<%@ Page Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="Reacharge.aspx.cs" Inherits="ENK.Reacharge" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     
  
   
    <script type="text/javascript">

        function validateControlsActivate() {
            debugger;
           
            
            if (document.getElementById('<%=ddlNetwork.ClientID%>').selectedIndex == 0) {
               // alert('Please Select  Network');
                $.alertable.alert('Please Select  Network!').always(function () {
                    console.log('Alert dismissed');
                    document.getElementById("<%=ddlNetwork.ClientID%>").focus();
                });
              
                return false;
            }

            if (document.getElementById('<%=ddlProduct.ClientID%>').selectedIndex == 0) {
                // alert('Please Select Tariff / Product');
                $.alertable.alert('Please Select Tariff / Product').always(function () {
                    console.log('Alert dismissed');
                    document.getElementById("<%=ddlProduct.ClientID%>").focus();
                });
               
                return false;
            }

            if (document.getElementById("<%=txtSIMCARD.ClientID%>").value == "") {
                //alert("Mobile Number Can't be Blank");
                $.alertable.alert('Mobile Number Can not be Blank').always(function () {
                    console.log('Alert dismissed');
                    document.getElementById("<%=txtSIMCARD.ClientID%>").focus();
                });
                
                return false;
            }

            //"Ultra Mobile"=17
            var Network = document.getElementById('<%=ddlNetwork.ClientID%>');
            if (Network.value == "17") {
                var str = document.getElementById("<%=txtSIMCARD.ClientID%>").value
                if (str.length > 10) {
                    // alert("Sorry ! Mobile Number Can't be  maximum 10 digit.");
                    $.alertable.alert('Sorry ! Mobile Number Can not be  maximum 10 digit.').always(function () {
                        console.log('Alert dismissed');
                        document.getElementById("<%=txtSIMCARD.ClientID%>").focus();
                    });
                   
                    return false;
                }

            }




            if (document.getElementById("<%=txtConfirmSIMCARD.ClientID%>").value == "") {
                // alert("Confirm Mobile Number Can't be Blank");
                $.alertable.alert('Confirm Mobile Number Can not be Blank').always(function () {
                    console.log('Alert dismissed');
                    document.getElementById("<%=txtConfirmSIMCARD.ClientID%>").focus();
                });
             
                return false;
            }

          

            if (document.getElementById("<%=txtAmt.ClientID%>").value == "") {
                //alert("Amount Can't be Blank");
                $.alertable.alert('Amount Can not be Blank').always(function () {
                    console.log('Alert dismissed');
                    document.getElementById("<%=txtAmt.ClientID%>").focus();
                });

                return false;
            }
            
            if (document.getElementById("<%=txtAmountPay.ClientID%>").value == "") {
                //alert("Amount Can't be Blank");
                $.alertable.alert('Amount Can not be Blank').always(function () {
                    console.log('Alert dismissed');
                    document.getElementById("<%=txtAmountPay.ClientID%>").focus();
                });
               
                return false;
            }
            if (document.getElementById("<%=txtAmountPay.ClientID%>").value == "0") {
                //alert("Amount Can't be Zero");
                $.alertable.alert('Amount Can not be Zero').always(function () {
                    console.log('Alert dismissed');
                    document.getElementById("<%=txtAmountPay.ClientID%>").focus();
                });
               
                return false;
            }
            

            // fOR AT&T Plan
            if (Network.value == "18") {
                debugger;

                     var Product = document.getElementById('<%=ddlProduct.ClientID%>').value;
                     var str = document.getElementById("<%=txtAmt.ClientID%>").value

                     if (Product == "8816130") {

                         if (parseFloat(str) < 30 || parseFloat(str) > 30) {

                             $.alertable.alert('Sorry ! Recharge Amount Can not be  greter than or less than 30$').always(function () {
                                 console.log('Alert dismissed');
                                 document.getElementById("<%=txtAmt.ClientID%>").focus();
                             });

                             return false;
                         }
                     }

                     else if (Product == "8815999") { 
                         if (parseFloat(str) < 10 || parseFloat(str) > 14.99) {
                             $.alertable.alert('Sorry ! Recharge Amount must be enter between 10 And 14.99$ in this Plan').always(function () {
                                 console.log('Alert dismissed');
                                 document.getElementById("<%=txtAmt.ClientID%>").focus();
                             });

                             return false;
                         }
                     }
                     else if (Product == "8816000") {

                         if (parseFloat(str) < 15 || parseFloat(str) > 450) {
                             //alert("Sorry ! Mobile Number Can't be maximum 10 digit");
                             $.alertable.alert('Sorry ! Recharge Amount must be enter between 15 And 450$ in this Plan').always(function () {
                                 console.log('Alert dismissed');
                                 document.getElementById("<%=txtAmt.ClientID%>").focus();
                             });

                             return false;
                         }
                     }




         }
  
           

            var SIMCARD = document.getElementById("<%= txtSIMCARD.ClientID%>");
           var confirmSIMCARD = document.getElementById("<%=txtConfirmSIMCARD.ClientID%>");

            if (SIMCARD.value != confirmSIMCARD.value) {
               // alert("Sorry ! Mobile Number  do not match.");
                $.alertable.alert('Sorry ! Mobile Number  do not match.').always(function () {
                    console.log('Alert dismissed');
                    document.getElementById("<%=txtConfirmSIMCARD.ClientID%>").focus();
                    
                });

               
                return false;
            }


          


            return true;
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
                  //  alert("Only numbers allowed.");
                    $.alertable.alert('Only numbers allowed.').always(function () {
                        console.log('Alert dismissed');
                       

                      });

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
                       // alert("Only one decimal allowed.");
                        $.alertable.alert('Only one decimal allowed.').always(function () {
                            console.log('Alert dismissed');


                        });
                        return false;
                    }
                    else {

                        return true;
                    }

                }
                else {
                   /// alert("Only numbers allowed.");
                    $.alertable.alert('Only numbers allowed.').always(function () {
                        console.log('Alert dismissed');


                    });
                    return false; // disable key press
                }
            }

        }

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

        .Img {
            max-width: 18%;
            padding-left: 15px;
            margin-left: 474px;
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

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- MAIN PANEL -->
		<div id="main" role="main" style="margin-top: 0px!important;" >
             <asp:HiddenField ID="hddnActivation" runat="server" />
			<!-- RIBBON -->
			 
			<!-- END RIBBON -->

			<!-- MAIN CONTENT -->
			<div id="content">
				<div class="row new-inerpage">
					<div class="col-xs-12 col-sm-5 col-md-5 col-lg-4" style="display:none">
						<h1 class="page-title txt-color-blueDark">
							<i class="fa fa-lg fa-fw fa-pencil-square-o"></i> 
								Sim Provisioning
							<span>> 
								Recharge						</span>						</h1>
					</div>
					<div class="col-xs-12 col-sm-7 col-md-7 col-lg-8"  >
                          <asp:Label ID="lblmessage" Visible="false"  ForeColor="Green" runat="server" Text=""></asp:Label> <br />
                         <%-- <asp:Label ID="lblNote"  Visible="false" ForeColor="Red" runat="server" Text="Note :- If you want to recharge again  please wait 5 min.  "></asp:Label> --%>
                      
					</div>
				</div>

				<!-- widget grid -->
				<section id="widget-grid" class="">

					<!-- row -->
					<div class="row">
						<!-- NEW WIDGET START -->
                        <article class="col-xs-3 col-sm-3 col-md-3 col-lg-3">
                            &nbsp;
                            </article>

						<article class="col-xs-6 col-sm-6 col-md-6 col-lg-6">
							<!-- Widget ID (each widget will need unique ID)-->
							<div class="jarviswidget jarviswidget-color-blueDark"   data-widget-colorbutton="false" data-widget-editbutton="false" data-widget-togglebutton="false" data-widget-deletebutton="false" data-widget-fullscreenbutton="false" data-widget-custombutton="false" data-widget-collapsed="false" data-widget-sortable="false">
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
									<h2>New Recharge</h2>
								</header>

								<!-- widget div-->
								<div>
									<!-- widget edit box -->
									<%--<div class="jarviswidget-editbox">--%>
										<!-- This area used as dropdown edit box -->
									<%--</div>--%>
									<!-- end widget edit box -->

									<!-- widget content -->
                                    <%--   <asp:Label ForeColor="Green" cssClass="label pull-right" ID="Label1" runat="server" Text="Download App from Google Play Store"></asp:Label>
                                    <asp:HyperLink id="hyperlink1" Width="99px"  CssClass="label pull-right"   ImageUrl="img/playstore.png" NavigateUrl="https://play.google.com/store/apps/details?id=com.virtuzoconsultancyservicespvtltd.ENK&hl=en"         ToolTip  ="Download Android App"
                                    Target="_blank" runat="server"/>--%>
                                   <div  class="clearfix"></div>
                                         
                                   
                    <div id="login-form" class="smart-form client-form">
                        <div class="box-input">
                                                     <asp:HiddenField ID="hddnInvoice" runat="server" />                         
                             <div class="row">
                                 
                                 

                                  <div id="div1" runat="server" class="form-group col-md-12">                                  
                                <label class="label">Network</label>                                       
                                <label class="input"> 
                                <asp:DropDownList ID="ddlNetwork" class="form-control chosen-select text-area" runat="server" OnSelectedIndexChanged="ddlNetwork_SelectedIndexChanged"  AutoPostBack="true" >
                                     
                                </asp:DropDownList>                            
                                </label>                                  
                                </div>
                                  </div>
                                  <div class="row">
                                      <div id="divProduct"  runat="server"  class="form-group col-md-12 ">                                  
                                          <asp:Label ID="lblProduct" runat="server" Text="Tariff"></asp:Label>                         
                                <label class="input"> 
                                <asp:DropDownList ID="ddlProduct"  class="form-control chosen-select text-area"  runat="server"  OnSelectedIndexChanged="ddlProduct_SelectedIndexChanged" AutoPostBack ="true" >
                                     
                                </asp:DropDownList>                            
                                </label>                                  
                                </div>


                                         <div class="form-group col-md-12">   
                                    <label class="label">Mobile Number <asp:Label ID="Label2" ForeColor="Green" runat="server" Text="Ex-0123456789"></asp:Label></label>
                                    <label class="input">   
                                    <asp:TextBox ID="txtSIMCARD" CssClass="form-control" MaxLength="10"  title="Enter your Mobile  number" runat="server" onkeypress="javascript:return onlynumeric(event,this);"></asp:TextBox>                           
                                     <b class="tooltip tooltip-top-right">
                                                <i class="fa fa-arrow-circle-down"></i> Enter Mobile Number</b>    
                                    </label>
                                </div>
                                      
                                   <div class="form-group col-md-12">   
                                    <label class="label">Confirm Mobile Number</label>
                                    <label class="input">   
                                    <asp:TextBox ID="txtConfirmSIMCARD" CssClass="form-control" MaxLength="10"  title="Enter  Confirm Mobile  number" runat="server" onkeypress="javascript:return onlynumeric(event,this);" ></asp:TextBox>                           
                                     <b class="tooltip tooltip-top-right">
                                                <i class="fa fa-arrow-circle-down"></i> Enter Confirm Mobile Number</b>    
                                    </label>
                                </div>  


                                      <div id="divMonths" runat="server" >
                                  <div class="form-group col-md-12" >   
                                    <label class="label">Month(s)<asp:Label ID="LblMonths" runat="server" forecolor="Red"></asp:Label>
                                        
                                    </label>
                                    <label class="input">   
                                    <asp:TextBox ID="txtMonths"   AutoPostBack="true"  runat="server" MaxLength="6"></asp:TextBox>                           
                                     <b class="tooltip tooltip-top-right">
                                                <i class="fa fa-arrow-circle-down"></i>  </b>    
                                    </label>
                                </div>


                               <div id="divRegulatry" runat="server" >
                                  <div class="form-group col-md-12" >   
                                    <label class="label">Plan Amount<asp:Label ID="lblRechargePercentage" runat="server" forecolor="Red"></asp:Label>
                                        
                                    </label>
                                    <label class="input">   
                                    <asp:TextBox ID="txtAmt"  OnTextChanged="txtAmt_TextChanged"  AutoPostBack="true"  runat="server" MaxLength="6" onkeypress="javascript:return onlynumericDecimal(event,this);"></asp:TextBox>                           
                                     <b class="tooltip tooltip-top-right">
                                                <i class="fa fa-arrow-circle-down"></i>  </b>    
                                    </label>
                                </div> 
                               <div class="form-group col-md-12">   
                                    <label class="label">Regulatory fee<asp:Label  ID="Label13" ForeColor="Red" runat="server" Text=""></asp:Label>
                                           <asp:Label ID="lblRegulatory" runat="server" Text=""  ForeColor="Red"></asp:Label>     
                                    </label>
                                    <label class="input">   
                                    <asp:TextBox ID="txtRegulatry"  Readonly="true" Text="0"  runat="server" MaxLength="5" onkeypress="javascript:return onlynumeric(event,this);"></asp:TextBox>                           
                                     <b class="tooltip tooltip-top-right">
                                                <i class="fa fa-arrow-circle-down"></i> </b>    
                                    </label>
                                </div> 
                                </div>



                                   <div class=" form-group col-md-12">   
                                    <label class="label">Total Amount To Pay</label>

                                    <label class="input">   
                                    <asp:TextBox ID="txtAmountPay" CssClass="form-control"  title="Enter your SIM card number" runat="server"></asp:TextBox>                           
                                     <b class="tooltip tooltip-top-right">
                                                <i class="fa fa-arrow-circle-down"></i> Amount To Pay</b>    
                                    </label>
                                </div> 
                                 

                                    
                                                                  
                              <div class="form-group col-md-12" id="divrecharge" runat="server" style="display:none">                          
                                    <label class="label">ZIP Code</label>
                                    <label class="input">  
                                    <asp:TextBox ID="txtZIPCode" CssClass="form-control"  MaxLength="10" title="Enter your ZIP code" runat="server"></asp:TextBox>                              
                                    <b class="tooltip tooltip-top-right">
                                    <i class="fa fa-arrow-circle-down"></i> Enter ZIP Code</b>    
                                    </label>
                                </div>
                          
                                    <div  id="divCity" style="display:none" runat="server" class="form-group col-md-12 ">                          
                                    <label class="label">City</label>
                                    <label class="input">  
                                    <asp:TextBox ID="txtCity" CssClass="form-control"  MaxLength="50" title="Enter CITY" runat="server"></asp:TextBox>                              
                                    <b class="tooltip tooltip-top-right">
                                    <i class="fa fa-arrow-circle-down"></i> Enter CITY</b>    
                                    </label>
                                </div>
                               


                            <div style="display:none" >
                          
                                  <div class="form-group  col-md-6">                          
                                    <label class="label">Email</label>
                                    <label class="input">  
                                    <asp:TextBox ID="txtEmail" CssClass="form-control"  MaxLength="50" title="Enter Email" runat="server"></asp:TextBox>                              
                                    <b class="tooltip tooltip-top-right">
                                    <i class="fa fa-arrow-circle-down"></i> Enter Email</b>    
                                    </label>
                                </div>

                                  

                                <div id="divahannel" runat="server" class="form-group col-md-6">             
                                    <label class="label">Channel ID</label>
                                    <label class="input">  
                                    <asp:TextBox ID="txtChannelID"  CssClass="form-control" placeholder="Channel ID" runat="server" Text=""></asp:TextBox>                               
                                     <b class="tooltip tooltip-top-right">
                                     <i class="fa fa-arrow-circle-down"></i> Enter Channel ID</b>    
                                    </label>
                                </div>                             
                                <div id="divLanguage" runat="server" class="form-group col-md-6 ">   
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
                        <footer class="boxBtnsubmit">
                             <asp:Label ID="lblCashback" style="padding-right: 10%;"  ForeColor="Green" runat="server" Text=""></asp:Label>
                            <asp:Button ID="btnCompanyByAccount" class="btn btn-primary sccs"  runat="server"   Text="Recharge" OnClick="btnCompanyByAccount_Click" OnClientClick="javascript:return validateControlsActivate();" />
                            
                        </footer>
                  </div>
									<!-- end widget content -->
								</div>
								<!-- end widget div -->
							
							<!-- end widget -->
						</article>
						<!-- WIDGET END -->
                         <article class="col-xs-3 col-sm-3 col-md-3 col-lg-3">
                            &nbsp;
                            </article>
                        </div>
					

					<!-- end row -->
				</section>
                </div>
				<!-- end widget grid -->
			</div>
			<!-- END MAIN CONTENT -->
		
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

        //$('.sccs').on('click', function () {
        //    $.alertable.confirm('You sure?').then(function () {
        //        console.log('Confirmation submitted');
        //        return true;
        //    }, function () {
        //        console.log('Confirmation canceled');
        //    });
           
        //});

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
   
</asp:Content>
