<%@ Page Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="ActivationPortin.aspx.cs" Inherits="ENK.ActivationPortin" %>
  
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link type="text/css" rel="stylesheet" href="http://code.jquery.com/ui/1.10.3/themes/smoothness/jquery-ui.css" />
    <script type="text/javascript" src="http://code.jquery.com/jquery-1.8.2.js"></script>
    <script type="text/javascript" src="http://code.jquery.com/ui/1.10.3/jquery-ui.js"></script>    
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
    <style>
        .modal-backdrop.in {
    filter: alpha(opacity=50);
    opacity: .5;
    display: none;
}
    </style>

    <script type="text/javascript">

        function validateControlsActivatePort() {

            if (document.getElementById('<%=ddlNetwork.ClientID%>').selectedIndex == 0) {
                alert('Please Select  Network');
                document.getElementById("<%=ddlNetwork.ClientID%>").focus();
                return false;
            }

            if (document.getElementById("<%=txtSIMCARD.ClientID%>").value == "") {
                alert("SIM CARD Can't be Blank");
                document.getElementById("<%=txtSIMCARD.ClientID%>").focus();
                return false;
            }

            if (document.getElementById("<%=txtZIPCode.ClientID%>").value == "") {
                alert("ZIPCode Can't be Blank");
                document.getElementById("<%=txtZIPCode.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=txtPHONETOPORT.ClientID%>").value == "") {
                alert("PHONE TO PORT Can't be Blank");
                document.getElementById("<%=txtPHONETOPORT.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=txtACCOUNT.ClientID%>").value == "") {
                alert("ACCOUNT Can't be Blank");
                document.getElementById("<%=txtACCOUNT.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=txtPIN.ClientID%>").value == "") {
                alert("PIN Can't be Blank");
                document.getElementById("<%=txtPIN.ClientID%>").focus();
                return false;
            }

            if (document.getElementById("<%=txtEmailAddress.ClientID%>").value == "") {
                alert("Email Can't be Blank");
                document.getElementById("<%=txtEmailAddress.ClientID%>").focus();
                return false;
            }
            var email = document.getElementById("<%=txtEmailAddress.ClientID%>");
            var filter = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;

            if (!filter.test(email.value)) {
                alert('Please Enter a valid email address');
                email.focus;
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
        .modal {
            top: 0;
            left: 0;
            z-index: 99;
            opacity: 0.8;
            filter: alpha(opacity=80);
            -moz-opacity: 0.8;
            min-height: 100%;
            width: 100%;
        }

        .loading {
            font-family: Arial;
            font-size: 10pt;
            border: 5px solid #67CFF5;
            width: 550px;
            height: 150px;
            display: none;
            position: fixed;
            background-color: White;
            z-index: 999;
        }
    </style>
    <script>
       

        $(document).ready(function () {
            var keyDown = false, ctrl = 17, vKey = 86, Vkey = 118;

            $("#ContentPlaceHolder1_TxtMSISDN").keydown(function (e) {
                if (e.keyCode == ctrl) keyDown = true;
            }).keyup(function (e) {
                if (e.keyCode == ctrl) keyDown = false;
            });

            $("#ContentPlaceHolder1_TxtMSISDN").on('keypress', function (e) {
                if (!e) var e = window.event;
                if (e.keyCode > 0 && e.which == 0) return true;
                if (e.keyCode) code = e.keyCode;
                else if (e.which) code = e.which;
                var character = String.fromCharCode(code);
                if (character == '\b' || character == ' ' || character == '\t') return true;
                if (keyDown && (code == vKey || code == Vkey)) return (character);
                else return (/[0-9]$/.test(character));
            }).on('focusout', function (e) {
                var $this = $(this);
                $this.val($this.val().replace(/[^0-9]/g, ''));
            }).on('paste', function (e) {
                var $this = $(this);
                setTimeout(function () {
                    $this.val($this.val().replace(/[^0-9]/g, ''));
                }, 5);
            });
        });
       
</script>

     

    <script>
        $(document).ready(function () {
            //$("#ContentPlaceHolder1_TxtRefno").keydown(function (e) {
            //    // Allow: backspace, delete, tab, escape, enter and .
            //    if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 110]) !== -1 ||
            //        // Allow: Ctrl+A, Command+A
            //        (e.keyCode === 65 && (e.ctrlKey === true || e.metaKey === true)) ||
            //        // Allow: home, end, left, right, down, up
            //        (e.keyCode >= 35 && e.keyCode <= 40)) {
            //        // let it happen, don't do anything
            //        return;
            //    }
            //    // Ensure that it is a number and stop the keypress
            //    if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
            //        e.preventDefault();
            //    }
            //});

            $('#ContentPlaceHolder1_TxtRefno').keypress(function (e) {
                var regex = new RegExp("^[0-9a-zA-Z]+$");
                var str = String.fromCharCode(!e.charCode ? e.which : e.charCode);
                if (regex.test(str)) {
                    return true;
                }
                e.preventDefault();
                return false;
            });
        });
</script>


      <script>

          $(document).ready(function () {
              var keyDown = false, ctrl = 17, vKey = 86, Vkey = 118;

              $("#ContentPlaceHolder1_txtAccountNumber").keydown(function (e) {
                  if (e.keyCode == ctrl) keyDown = true;
              }).keyup(function (e) {
                  if (e.keyCode == ctrl) keyDown = false;
              });

              $("#ContentPlaceHolder1_txtAccountNumber").on('keypress', function (e) {
                  if (!e) var e = window.event;
                  if (e.keyCode > 0 && e.which == 0) return true;
                  if (e.keyCode) code = e.keyCode;
                  else if (e.which) code = e.which;
                  var character = String.fromCharCode(code);
                  if (character == '\b' || character == ' ' || character == '\t') return true;
                  if (keyDown && (code == vKey || code == Vkey)) return (character);
                  else return (/[0-9]$/.test(character));
              }).on('focusout', function (e) {
                  var $this = $(this);
                  $this.val($this.val().replace(/[^0-9]/g, ''));
              }).on('paste', function (e) {
                  var $this = $(this);
                  setTimeout(function () {
                      $this.val($this.val().replace(/[^0-9]/g, ''));
                  }, 5);
              });
          });


         
</script>

      <script>
          $(document).ready(function () {
              var keyDown = false, ctrl = 17, vKey = 86, Vkey = 118;

              $("#ContentPlaceHolder1_txtAccountNumber").keydown(function (e) {
                  if (e.keyCode == ctrl) keyDown = true;
              }).keyup(function (e) {
                  if (e.keyCode == ctrl) keyDown = false;
              });

              $("#ContentPlaceHolder1_txtAccountNumber").on('keypress', function (e) {
                  if (!e) var e = window.event;
                  if (e.keyCode > 0 && e.which == 0) return true;
                  if (e.keyCode) code = e.keyCode;
                  else if (e.which) code = e.which;
                  var character = String.fromCharCode(code);
                  if (character == '\b' || character == ' ' || character == '\t') return true;
                  if (keyDown && (code == vKey || code == Vkey)) return (character);
                  else return (/[0-9]$/.test(character));
              }).on('focusout', function (e) {
                  var $this = $(this);
                  $this.val($this.val().replace(/[^0-9]/g, ''));
              }).on('paste', function (e) {
                  var $this = $(this);
                  setTimeout(function () {
                      $this.val($this.val().replace(/[^0-9]/g, ''));
                  }, 5);
              });
          });


          
</script>

      <script>
          $(document).ready(function () {
              var keyDown = false, ctrl = 17, vKey = 86, Vkey = 118;

              $("#ContentPlaceHolder1_txtZip").keydown(function (e) {
                  if (e.keyCode == ctrl) keyDown = true;
              }).keyup(function (e) {
                  if (e.keyCode == ctrl) keyDown = false;
              });

              $("#ContentPlaceHolder1_txtZip").on('keypress', function (e) {
                  if (!e) var e = window.event;
                  if (e.keyCode > 0 && e.which == 0) return true;
                  if (e.keyCode) code = e.keyCode;
                  else if (e.which) code = e.which;
                  var character = String.fromCharCode(code);
                  if (character == '\b' || character == ' ' || character == '\t') return true;
                  if (keyDown && (code == vKey || code == Vkey)) return (character);
                  else return (/[0-9]$/.test(character));
              }).on('focusout', function (e) {
                  var $this = $(this);
                  $this.val($this.val().replace(/[^0-9]/g, ''));
              }).on('paste', function (e) {
                  var $this = $(this);
                  setTimeout(function () {
                      $this.val($this.val().replace(/[^0-9]/g, ''));
                  }, 5);
              });
          });


          
</script>






    <%--<script>
        $(document).ready(function () {
            $("#ContentPlaceHolder1_RdbtnMSISDN").click(function () {
                if ($(this).is(':checked')) {
                   
                    $('#ContentPlaceHolder1_TxtMSISDN').focus();
                    alert("check")
                }
            });
        });
    </script>--%>

    <link href="css/plugins/chosen/chosen.css" rel="stylesheet" />
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
    <div id="main" role="main" style="margin-top: 0px!important;">
        <asp:HiddenField ID="hddnActivation" runat="server" />
        <!-- RIBBON -->

        <!-- END RIBBON -->

        <!-- MAIN CONTENT -->
        <div id="content">
            <div class="row new-inerpage">
                <div class="col-xs-12 col-sm-5 col-md-5 col-lg-4">
                    <h1 class="page-title txt-color-blueDark">
                        <i class="fa fa-lg fa-fw fa-pencil-square-o"></i>
                        Sim Provisioning
							<span>> 
								PortIn Status						</span></h1>
                </div>
                <div class="col-xs-12 col-sm-7 col-md-7 col-lg-8">
                </div>
            </div>

            <!-- widget grid -->
            <section id="widget-grid" class="">

                <!-- row -->
                <div class="row">
                    <!-- NEW WIDGET START -->
                    <article class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                        <!-- Widget ID (each widget will need unique ID)-->
                        <div class="jarviswidget jarviswidget-color-blueDark" id="wid-id-0" data-widget-colorbutton="false" data-widget-editbutton="false" data-widget-togglebutton="false" data-widget-deletebutton="false" data-widget-fullscreenbutton="false" data-widget-custombutton="false" data-widget-collapsed="false" data-widget-sortable="false">
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
                                <span class="widget-icon"><i class="fa fa-table"></i></span>
                                <h2>Get PortIn Status</h2>
                            </header>

                            <!-- widget div-->
                            <div>
                                <!-- widget edit box -->
                                <div class="jarviswidget-editbox">
                                    <!-- This area used as dropdown edit box -->
                                </div>
                                <!-- end widget edit box -->

                                <!-- widget content -->
                                <%--<asp:Label ForeColor="Green" CssClass="label pull-right" ID="Label1" runat="server" Text="Download App from Google Play Store"></asp:Label>
                                <asp:HyperLink ID="hyperlink1" Width="99px" CssClass="label pull-right" ImageUrl="img/playstore.png" NavigateUrl="https://play.google.com/store/apps/details?id=com.virtuzoconsultancyservicespvtltd.ENK&hl=en" ToolTip="Download Android App"
                                    Target="_blank" runat="server" />--%>
                                <div class="clearfix"></div>

                                <div id="ActivationPort" runat="server" autoposback="true">
                                <div id="login-form" class="smart-form client-form">
                                    <div class="box-input">
                                        <div class="row">
                                               <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">
                                                   <b><span style="color:white; font-size:20px;  background-color:#023954;">Only for Port In to Lyca</span></b>
                                                </div>
                                            </div>
                                        <div class="row">

                                             <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">     
                                             <%--<asp:RadioButton ID="RdbtnREFNO" runat="server" AutoPostBack="true" value="REFERENCENO."  OnCheckedChanged="RdbtnREFNO_CheckedChanged"/>                         
                                           --%> <span>Reference Number</span>
                                            </div>
                               

                                             <div class="col-xs-12 col-sm-1 col-md-1 col-lg-1">
                                                 <label class="label">MSISDN</label> 
                                                 </div> 

                                             <div class="col-xs-12 col-sm-5 col-md-5 col-lg-5">     
                                             <%--<asp:RadioButton ID="RdbtnMSISDN" runat="server"  AutoPostBack="true" value="MSISDN" OnCheckedChanged="RdbtnMSISDN_CheckedChanged"/>                         
                                            <span>MSISDN</span>--%>
                                             </div>

                                            <%--<div class="col-xs-12 col-sm-1 col-md-1 col-lg-1">
                                                <label class="label">ICCID</label>
                                            </div>--%>
                                           <%-- <div class="col-xs-12 col-sm-4 col-md-4 col-lg-4 ">     
                                             <asp:RadioButton ID="RdbtnICCID" runat="server" AutoPostBack="true" value="ICCID" OnCheckedChanged="RdbtnICCID_CheckedChanged"/>                         
                                            <span>ICCID</span>
                                            </div>--%>




                                            <%--<div class="col-xs-12 col-sm-1 col-md-1 col-lg-1">
                                                <label class="label">REFERENCE NO.</label>
                                            </div>--%>
                                           
                                        </div>

                                        <div class="row">
                                            <div class="col-xs-12 col-sm-5 col-md-5 col-lg-5">
                                             <label class="input">
                                                    <asp:TextBox ID="TxtRefno" title="Refno" runat="server"></asp:TextBox>
                                                    <b class="tooltip tooltip-top-right">
                                                        <i class="fa fa-arrow-circle-down"></i>Only Numeric</b>
                                                </label>
                                            </div>

                                            <div class="col-xs-12 col-sm-1 col-md-1 col-lg-1">
                                                <label class="label"><b>OR</b></label>
                                                <label class="input">
                                                    <b class="tooltip tooltip-top-right">
                                                        <i class="fa fa-arrow-circle-down"></i>or</b>
                                                </label>
                                            </div>

                                            <div class="col-xs-12 col-sm-5 col-md-5 col-lg-5">
                                                
                                                 <label class="input">
                                                    <asp:TextBox ID="TxtMSISDN" title="MSISDN" runat="server"></asp:TextBox>
                                                    <b class="tooltip tooltip-top-right">
                                                        <i class="fa fa-arrow-circle-down"></i>Only Numeric</b>
                                                </label>
                                                <%--<label class="input">
                                                   <asp:TextBox ID="TxtICCID" title="ICCID" runat="server"></asp:TextBox>
                                                    <b class="tooltip tooltip-top-right">
                                                        <i class="fa fa-arrow-circle-down"></i>Only Numeric</b>
                                                </label>--%>
                                            </div>
                                            
                                            
                                            
                                            
                                            <div class="col-xs-12 col-sm-1 col-md-1 col-lg-1">

                                                <asp:Button ID="btnSearch" class="btn btn-primary" runat="server" Text="Search" OnClick="btnSearch_Click" />
                                            
                                               <%-- <label class="label"><b>OR</b></label>
                                                <label class="input">
                                                    <b class="tooltip tooltip-top-right">
                                                        <i class="fa fa-arrow-circle-down"></i>or</b>
                                                </label>--%>
                                            </div>

                                            <div class="col-xs-12 col-sm-3 col-md-3 col-lg-3">
                                                  
                                            </div>

                                            <div class="col-xs-12 col-sm-1 col-md-1 col-lg-1">

                                                </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                                <div class="table-responsive" >
                                                    <%--<asp:Label ID="Lbltxn" runat="server" Text=""></asp:Label>--%>
                                                    <asp:GridView ID="GridPlanDetails" class="table table-bordered" runat="server">
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-xs-12 col-sm-4 col-md-4 col-lg-4"></div>
                                             <div class="col-xs-12 col-sm-2 col-md-2 col-lg-2">
                                                  <!-- Trigger the modal with a button -->
                                           <asp:Button ID="BtnModify" class="btn btn-primary" runat="server" Text="Modify Request" onclick="BtnModify_Click"/>

                                                 <%-- <button type="button"  class="btn btn-primary" data-toggle="modal" data-target="#myModal">Modify Request</button>
                                             --%>
                                             </div>
                                            
                                            <div class="col-xs-12 col-sm-3 col-md-3 col-lg-3">
                                                 <asp:Button ID="BtnCancelportin" class="btn btn-primary" runat="server"  Text="Cancel Portin" OnClick="BtnCancelportin_Click"/>
                                    </div>
                                            <div class="col-xs-12 col-sm-3 col-md-3 col-lg-3"></div>
                                            </div>



                                        <div class="row" >
                                            <asp:HiddenField ID="hddnTariffType" runat="server" />
                                            <asp:HiddenField ID="hddnTariffTypeID" runat="server" />
                                            <asp:HiddenField ID="hddnTariffCode" runat="server" />
                                            <asp:HiddenField ID="hddnLycaAmount" runat="server" />
                                            <asp:HiddenField ID="hddnMonths" runat="server" />



                                            <div id="div1" runat="server" visible="false" class="form-group col-md-6">
                                                <label class="label">Network</label>
                                                <label class="input">
                                                    <asp:DropDownList ID="ddlNetwork" class="form-control chosen-select text-area" runat="server" OnSelectedIndexChanged="ddlNetwork_SelectedIndexChanged" AutoPostBack="true">
                                                    </asp:DropDownList>
                                                </label>
                                            </div>



                                            <div id="divPortIn" runat="server" visible="false" class="col-xs-12 col-sm-6 col-md-6 col-lg-6">
                                                <label class="label">Tariff</label>
                                                <label class="input">
                                                    <asp:DropDownList ID="ddlTariff" class="chosen-select text-area" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlTariff_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </label>
                                            </div>

                                            <div id="divRegulatry" visible="false" runat="server">
                                                <div class="form-group col-md-6">
                                                    <label class="label">
                                                        Plan Amount<asp:Label ID="lblRechargePercentage" runat="server" ForeColor="Red"></asp:Label>

                                                    </label>
                                                    <label class="input">
                                                        <asp:TextBox ID="txtAmt" ReadOnly="true" runat="server" MaxLength="5" onkeypress="javascript:return onlynumeric(event,this);"></asp:TextBox>
                                                        <b class="tooltip tooltip-top-right">
                                                            <i class="fa fa-arrow-circle-down"></i></b>
                                                    </label>
                                                </div>
                                                <div class="form-group col-md-6">
                                                    <label class="label">
                                                        Regulatory fee<asp:Label ID="Label13" ForeColor="Red" runat="server" Text=""></asp:Label>
                                                        <asp:Label ID="lblRegulatory" runat="server" Text="" ForeColor="Red"></asp:Label>
                                                    </label>
                                                    <label class="input">
                                                        <asp:TextBox ID="txtRegulatry" ReadOnly="true" Text="0" runat="server" MaxLength="5" onkeypress="javascript:return onlynumeric(event,this);"></asp:TextBox>
                                                        <b class="tooltip tooltip-top-right">
                                                            <i class="fa fa-arrow-circle-down"></i></b>
                                                    </label>
                                                </div>
                                            </div>

                                            <div id="div4t" visible="false" runat="server">
                                            <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6" visible="false">
                                                <label class="label">Total Amount To Pay</label>
                                                <label class="input">
                                                    <asp:TextBox ID="txtAmountPay" title="Enter your SIM card number" runat="server"></asp:TextBox>
                                                    <b class="tooltip tooltip-top-right">
                                                        <i class="fa fa-arrow-circle-down"></i>Amount To Pay</b>
                                                </label>
                                            </div>

                                            <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6" visible="false">
                                                <label class="label">SIM CARD</label>
                                                <label class="input">
                                                    <asp:TextBox ID="txtSIMCARD" title="Enter your SIM card number" runat="server"></asp:TextBox>
                                                    <b class="tooltip tooltip-top-right">
                                                        <i class="fa fa-arrow-circle-down"></i>Enter SIM card number</b>
                                                </label>
                                            </div>

                                            <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6" visible="false">
                                                <label class="label">ZIP Code</label>
                                                <label class="input">
                                                    <asp:TextBox ID="txtZIPCode" title="Enter your ZIP code" runat="server"></asp:TextBox>
                                                    <b class="tooltip tooltip-top-right">
                                                        <i class="fa fa-arrow-circle-down"></i>Enter ZIP Code</b>
                                                </label>
                                            </div>
                                            </div>
                                            <div id="divProduct" style="display: none" runat="server" visible="false" class="form-group col-md-6 ">
                                                <label class="label">Tariff</label>
                                                <label class="input">
                                                    <asp:DropDownList ID="ddlProduct" class="form-control chosen-select text-area" OnSelectedIndexChanged="ddlProduct_SelectedIndexChanged" AutoPostBack="true" runat="server">
                                                    </asp:DropDownList>
                                                </label>
                                            </div>
                                            <div id="divahannel" runat="server" visible="false" class="col-xs-12 col-sm-6 col-md-6 col-lg-6">
                                                <label class="label">Channel ID</label>
                                                <label class="input">
                                                    <asp:TextBox ID="txtChannelID" placeholder="Channel ID" runat="server" Text=""></asp:TextBox>
                                                    <b class="tooltip tooltip-top-right">
                                                        <i class="fa fa-arrow-circle-down"></i>Enter Channel ID</b>
                                                </label>
                                            </div>
                                            <div id="divLanguage" runat="server" visible="false" class="col-xs-12 col-sm-6 col-md-6 col-lg-6">
                                                <label class="label">Language</label>
                                                <label class="input">
                                                    <asp:DropDownList ID="ddlLanguage" class="text-area" runat="server">
                                                        <asp:ListItem Value="1" Text="English">
                                                        </asp:ListItem>
                                                        <asp:ListItem Value="2" Text="Spanish">
                                                        </asp:ListItem>
                                                    </asp:DropDownList>
                                                </label>
                                            </div>

                                            <div id="DivPort" runat="server" visible="false" class="box-input">


                                                <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">
                                                    <label class="label">PHONE # TO PORT</label>
                                                    <label class="input">
                                                        <asp:TextBox ID="txtPHONETOPORT" title="Enter your PHONE # TO PORT" runat="server"></asp:TextBox>
                                                        <b class="tooltip tooltip-top-right">
                                                            <i class="fa fa-arrow-circle-down"></i>Enter PHONE # TO PORT</b>
                                                    </label>
                                                </div>
                                                <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">
                                                    <label class="label">ACCOUNT #</label>
                                                    <label class="input">
                                                        <asp:TextBox ID="txtACCOUNT" class="form-control" title="Enter your ACCOUNT #" runat="server"></asp:TextBox>
                                                        <b class="tooltip tooltip-top-right">
                                                            <i class="fa fa-arrow-circle-down"></i>Enter ACCOUNT #</b>
                                                    </label>
                                                </div>
                                                <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">
                                                    <label class="label">PIN</label>
                                                    <label class="input">
                                                        <asp:TextBox ID="txtPIN" title="Account Password/Pin" runat="server"></asp:TextBox>
                                                        <b class="tooltip tooltip-top-right">
                                                            <i class="fa fa-arrow-circle-down"></i>Enter PIN</b>
                                                    </label>
                                                </div>
                                                <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">
                                                    <label class="label">Email Address</label>
                                                    <label class="input">
                                                        <asp:TextBox ID="txtEmailAddress" title="Enter your Email Address" runat="server"></asp:TextBox>
                                                        <b class="tooltip tooltip-top-right">
                                                            <i class="fa fa-arrow-circle-down"></i>Enter Email Address</b>
                                                    </label>
                                                </div>


                                                <div id="divH20" style="display: none" runat="server" class="box-input" visible="true">


                                                    <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">
                                                        <label class="label">Current Service Provider Name</label>
                                                        <label class="input">
                                                            <asp:TextBox ID="txtServiceProvider" title="Enter Current Service Provider Name " runat="server"></asp:TextBox>
                                                            <b class="tooltip tooltip-top-right">
                                                                <i class="fa fa-arrow-circle-down"></i>Enter Current Service Provider Name </b>
                                                        </label>
                                                    </div>

                                                    <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">
                                                        <label class="label">State</label>
                                                        <label class="input">
                                                            <asp:TextBox ID="txtState" title="Enter your state" runat="server"></asp:TextBox>
                                                            <b class="tooltip tooltip-top-right">
                                                                <i class="fa fa-arrow-circle-down"></i>Enter state</b>
                                                        </label>
                                                    </div>
                                                    <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">
                                                        <label class="label">City</label>
                                                        <label class="input">
                                                            <asp:TextBox ID="txtCity" title="Enter your city" runat="server"></asp:TextBox>
                                                            <b class="tooltip tooltip-top-right">
                                                                <i class="fa fa-arrow-circle-down"></i>Enter City</b>
                                                        </label>
                                                    </div>

                                                    <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">
                                                        <label class="label">Customer 1st and Last Name</label>
                                                        <label class="input">
                                                            <asp:TextBox ID="txtLastname" title="Enter Customer 1st and Last Name" runat="server"></asp:TextBox>
                                                            <b class="tooltip tooltip-top-right">
                                                                <i class="fa fa-arrow-circle-down"></i>Enter Customer 1st and Last Name</b>
                                                        </label>
                                                    </div>



                                                    <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">
                                                        <label class="label">Address</label>
                                                        <label class="input">
                                                            <asp:TextBox ID="txtaddress" title="Enter Address" runat="server"></asp:TextBox>
                                                            <b class="tooltip tooltip-top-right">
                                                                <i class="fa fa-arrow-circle-down"></i>Enter Address</b>
                                                        </label>
                                                    </div>
                                                </div>

                                            </div>
                                        </div>

                                 <!-- Modal -->
  



                                    </div>
                                </div>
                                <div id="divfooter" runat="server" visible="false" class="form-group col-md-6">
                                <footer class="boxBtnsubmit" >
                                    <asp:Button ID="btnCompanyByAccount" class="btn btn-primary" runat="server" Visible="false" Text="Activate Using Account Balance" OnClick="btnCompanyByAccount_Click" OnClientClick="javascript:return validateControlsActivatePort();" />
                                    <asp:Button ID="btnDistributorByAccount" class="btn btn-primary" runat="server" Visible="false" Text="Activate Using Account Balance" OnClick="btnDistributorByAccount_Click" OnClientClick="javascript:return validateControlsActivatePort();" />
                                    <asp:ImageButton ID="imgByPaypal" runat="server" src='img/expresscheckout_buttons.png' class="btn btn-primary pul-right" border='0' align='top' alt='Check out with PayPal' Visible="false" OnClientClick="javascript:return validateControlsActivatePort();" OnClick="imgByPaypal_Click" Style="padding: 0px!important; background-color: white!important; border-color: white!important; height: 35px;" />
                                    <asp:Label ID="lblPaypalMsg" runat="server" Visible="false" Text="Extra 3% will be charge if you pay with payPal" ForeColor="Red"></asp:Label>
                                </footer>
                                    </div>
                                    </div>

                                <div id="ActivationPort1" runat="server" autoposback="true">
                                    
                                             <div class="row">
                                             <div class="col-xs-12 col-md-3 form-group">     
                                             <span>Account Number</span>
                                             <asp:TextBox ID="txtAccountNumber" CssClass="form-control" runat="server"></asp:TextBox>
                                            
                                            </div>
                               
                                             <div class="col-xs-12 col-md-3 form-group">   
                                                 <span>Pin Number</span>
                                                      
                                                <asp:TextBox ID="txtPinNumber"  CssClass="form-control"  runat="server"></asp:TextBox>
                                                 </div> 
                                                 
                                                <div class="col-xs-12 col-md-3 form-group">    
                                                 <span>Zip Code</span>    
                                                     
                                                <asp:TextBox ID="txtZip"  CssClass="form-control"  runat="server"></asp:TextBox>
                                                 </div>

                                                 
                                            
                                        <div class="col-xs-12 col-md-3 form-group"> 
                                            <br />
                                                <asp:Button ID="BtnSubmit" runat="server" CssClass="btn btn-info"  Text="Submit" OnClick="BtnSubmit_Click" />
                                </div>
                                        </div>
                                          
                                   
                            </div>
                        </div>
                        <!-- end widget content -->
                </div>
                <!-- end widget div -->
        </div>
        <!-- end widget -->
        
						<!-- WIDGET END -->
    </div>

    <!-- end row -->
  
				<!-- end widget grid -->
    </div>
			<!-- END MAIN CONTENT -->
    </div>
		<!-- END MAIN PANEL -->





    <div class="modal fade" id="myModal" runat="server" role="dialog">
    <div class="modal-dialog">
    
      <!-- Modal content-->
      <div class="modal-content">
        <div class="modal-header">
          <button type="button" class="close" data-dismiss="modal">&times;</button>
          <h4 class="modal-title">Modal Header</h4>
        </div>
        <div class="modal-body">
<%--            <asp:TextBox ID="txtAccountNumber" runat="server"></asp:TextBox>
            <asp:TextBox ID="txtPinNumber" runat="server"></asp:TextBox>
            <asp:TextBox ID="txtZip" runat="server"></asp:TextBox>--%>
          
        </div>
        <div class="modal-footer">
          <%--<button type="button" class="btn btn-default" data-dismiss="modal">Close</button>--%>
            
        </div>
      </div>
      
    </div>
  </div>




   <%-- <div id="popupdiv" class="loading" title="Basic modal dialog" style="display: nonefont-family:Arial; font-size: 10pt; border: 2px solid #67CFF5; text-align: -webkit-center;">

        <b style="text-align: center!important;">Please Wait...........</b>
        <br />
        <br />
        <img src="img/loader.gif" alt="" style="text-align: center!important;" />
    </div>--%>

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
