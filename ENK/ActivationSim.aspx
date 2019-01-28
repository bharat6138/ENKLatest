<%@ Page Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="ActivationSim.aspx.cs" Inherits="ENK.ActivationForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link type="text/css" rel="stylesheet" href="http://code.jquery.com/ui/1.10.3/themes/smoothness/jquery-ui.css" />

    <style>
        #ck-button {
            margin: 4px;
            background-color: #EFEFEF;
            border-radius: 4px;
            border: 1px solid #D0D0D0;
            overflow: auto;
            float: left;
            margin-top: 19px;
        }

            #ck-button label {
                float: left;
                width: 5em;
            }

                #ck-button label span {
                    text-align: center;
                    padding: 7px 9px;
                    display: block;
                }

                #ck-button label #ContentPlaceHolder1_chkActivePort {
                    position: absolute;
                    top: 32px;
                    z-index: -1;
                }

            #ck-button #ContentPlaceHolder1_chkActivePort:checked + span {
                background-color: #911;
                color: #fff;
            }

        .ui-dialog {
            z-index: 9999999;
            width: 50vw !important;
        }
    </style>
    <script type="text/javascript" src="http://code.jquery.com/jquery-1.8.2.js"></script>
    <script type="text/javascript" src="http://code.jquery.com/ui/1.10.3/jquery-ui.js"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>     
    <script type="text/javascript">
        function validateControlsActivate() {
            if (document.getElementById('<%=ddlNetwork.ClientID%>').selectedIndex == 0) {
                alert('Please Select  Network');
                document.getElementById("<%=ddlNetwork.ClientID%>").focus();
                return false;
            }
            // if (document.getElementById('<%=ddlTariff.ClientID%>').selectedIndex == 0) {
            //  alert('Please Select Tariff');
            //  document.getElementById("<%=ddlTariff.ClientID%>").focus();
            // return false;
            // } 
            // if (document.getElementById('<%=ddlProduct.ClientID%>').selectedIndex == 0) {
            //  alert('Please Select Product');
            // document.getElementById("<%=ddlProduct.ClientID%>").focus();
            //  return false;
            // }

            if (document.getElementById("<%=txtAmountPay.ClientID%>").value == "") {
                alert("Amount to Pay Can't be blank");
                document.getElementById("<%=txtAmountPay.ClientID%>").focus();
                return false;
            }
            //if (document.getElementById("<%=txtAmountPay.ClientID%>").value == "0") {
            //    alert("Amount to Pay Can't be blank");
            //    document.getElementById("<%=txtAmountPay.ClientID%>").focus();
            //    return false;
            //}
            if (document.getElementById("<%=txtSIMCARD.ClientID%>").value == "") {
                alert("SIM CARD Can't be Blank");
                document.getElementById("<%=txtSIMCARD.ClientID%>").focus();
                return false;
            }
            else if (document.getElementById("<%=txtZIPCode.ClientID%>").value == "") {
                   alert("ZIP Code Can't be Blank");
                  document.getElementById("<%=txtZIPCode.ClientID%>").focus();
                    return false;
                 }
           // add by akash
          <%-- // if (document.getElementById('<%=ddlNetwork.ClientID%>').selectedIndex == 2) {  
               // if (document.getElementById("<%=txtZIPCode.ClientID%>").value == "") {
                 //   alert("Area Code Can't be Blank");
                  //  document.getElementById("<%=txtZIPCode.ClientID%>").focus();
                  //  return false;
               // }               
               // if (document.getElementById('<%=ddlCountry.ClientID%>').selectedIndex == 0) {
                //    alert("Please Select  Country");
                 //   document.getElementById("<%=ddlCountry.ClientID%>").focus();
                 //   return false;
              //  }
                
          //  }
          //  else if (document.getElementById("<%=txtZIPCode.ClientID%>").value == "") {
            //    alert("ZIP Code Can't be Blank");
             //   document.getElementById("<%=txtZIPCode.ClientID%>").focus();
           //     return false;
            //  }--%>

            // add by akash

            <%--if (document.getElementById("<%=txtEmail.ClientID%>").value == "") {
                alert("txtEmail Can't be Blank");
                document.getElementById("<%=txtEmail.ClientID%>").focus();
                return false;
            }--%>
            var email = document.getElementById("<%=txtEmail.ClientID%>");
            var filter = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;

            //if (!filter.test(email.value)) {
            //    alert('Please Enter a valid email address');
            //    email.focus;
            //    return false;
            //}
            return true;
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

    <script>
      


        $(document).ready(function () {
            var keyDown = false, ctrl = 17, vKey = 86, Vkey = 118;

            $("#ContentPlaceHolder1_txtPHONETOPORT").keydown(function (e) {
                if (e.keyCode == ctrl) keyDown = true;
            }).keyup(function (e) {
                if (e.keyCode == ctrl) keyDown = false;
            });

            $("#ContentPlaceHolder1_txtPHONETOPORT").on('keypress', function (e) {
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

            $("#ContentPlaceHolder1_txtACCOUNT").keydown(function (e) {
                if (e.keyCode == ctrl) keyDown = true;
            }).keyup(function (e) {
                if (e.keyCode == ctrl) keyDown = false;
            });

            $("#ContentPlaceHolder1_txtACCOUNT").on('keypress', function (e) {
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

            $("#ContentPlaceHolder1_txtPIN").keydown(function (e) {
                if (e.keyCode == ctrl) keyDown = true;
            }).keyup(function (e) {
                if (e.keyCode == ctrl) keyDown = false;
            });

            $("#ContentPlaceHolder1_txtPIN").on('keypress', function (e) {
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
            $("#ContentPlaceHolder1_txtMonth").keydown(function (e) {
                // Allow: backspace, delete, tab, escape, enter and .
                if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 110]) !== -1 ||
                    // Allow: Ctrl+A, Command+A
                    (e.keyCode === 65 && (e.ctrlKey === true || e.metaKey === true)) ||
                    // Allow: home, end, left, right, down, up
                    (e.keyCode >= 35 && e.keyCode <= 40)) {
                    // let it happen, don't do anything
                    return;
                }
                // Ensure that it is a number and stop the keypress
                if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
                    e.preventDefault();
                }
            });
        });
    </script>


    <script>
        $(document).ready(function () {
            $("#ContentPlaceHolder1_txtMonth").keyup(function () {
                if (("#ContentPlaceHolder1_txtMonth").val() > 6) {
                    alert("check");
                    //    return false;
                }
            });
        });
    </script>--%>



    <%--    <script>
        $(document).ready(function () {
            $("#ContentPlaceHolder1_DivPort").hide();
            $("#btnPortIn").click(function () {
                $("#ContentPlaceHolder1_DivPort").slideToggle("slow");
            });


            var $checkbox = $(this).find(':checkbox');
            $checkbox.attr('checked', !$checkbox.is(':checked'));
//            $("#ContentPlaceHolder1_btnPortIn").toggle(function () {
//                $("#ContentPlaceHolder1_btnPortIn").text("Port In Active");
//}, function () {
//    $("#ContentPlaceHolder1_btnPortIn").text("Port In");
//});
        });
</script>--%>

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

    <link href="css/plugins/chosen/chosen.css" rel="stylesheet" />
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

    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js"></script>

    <%--<script type="text/javascript">

    function printDiv(divName) {
        var printContents = document.getElementById(divName).innerHTML;
        var originalContents = document.body.innerHTML;

        document.body.innerHTML = printContents;

        window.print();

        document.body.innerHTML = originalContents;
    }


    $("[id*=btnModalPopup]").live("click", function () {
        $("#modal_dialog").dialog({
            buttons: {
                Close: function () {
                    $(this).dialog('close');
                }
            },
            modal: true
        });
        return false;
    });

   
</script>--%>
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
								Activate SIM						</span></h1>

                    <%--                    <div id="modal_dialog" style="display: none" runat="server">
    
                        <div id="printableArea">
 <page size="A4">
<div style="padding:15px;">
        <table width="100%" border="0" cellspacing="0" cellpadding="0">
  <tr>
    <td width="4%">&nbsp;</td>
    <td width="22%" class="auto-style1">&nbsp;</td>
    <td width="6%">&nbsp;</td>
    <td width="30%">&nbsp;</td>
    <td width="34%">&nbsp;</td>
    <td width="4%">&nbsp;</td>
  </tr>
          
  <tr>
    <td>&nbsp;</td>
    <td rowspan="3" class="auto-style1"><img src="img/logologin.png" class="img-resposive" alt="" height="auto"></td>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
  </tr>
  <tr>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
  </tr>
  <tr>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
  </tr>
  <tr>
    <td>&nbsp;</td>
    <td class="auto-style1">&nbsp;</td>
    <td>&nbsp;</td>
      <td></td>
    <td>&nbsp;</td>
  </tr>
  <tr>
    <td>&nbsp;</td>
    <td class="auto-style1">&nbsp;</td>
    <td>&nbsp;</td>
      <td></td>
    <td>&nbsp;</td>
  </tr>
  <tr>
    <td>&nbsp;</td>
    <td class="auto-style1">&nbsp;</td>
    <td>&nbsp;</td>
      <td></td>
    <td>&nbsp;</td>
  </tr>
  <tr>
    <td>&nbsp;</td>
    <td class="auto-style1">&nbsp;</td>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
  </tr>
  <tr>
    <td>&nbsp;</td>
    <td class="auto-style1">&nbsp;</td>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
  </tr>
  <tr>
    <td>&nbsp;</td>
    <td class="auto-style1">Retailer ID</td>
    <td><div align="center">-</div></td>
    <td colspan="2"><asp:Label ID="lblRetailerName" runat="server" Text="-"></asp:Label></td>
    <td>&nbsp;</td>
  </tr>
  <tr>
    <td>&nbsp;</td>
    <td class="auto-style1">Mobile No.</td>
    <td><div align="center">-</div></td>
    <td colspan="2"><asp:Label ID="lblMno" runat="server" Text="-"></asp:Label></td>
    <td>&nbsp;</td>
  </tr>
  <tr>
    <td>&nbsp;</td>
    <td class="auto-style1">Product </td>
    <td><div align="center">-</div></td>
    <td colspan="2"><asp:Label ID="lblProduct" runat="server" Text="-"></asp:Label><span class="auto-style1"></span></td>
    <td>&nbsp;</td>
  </tr>
  <tr>
    <td>&nbsp;</td>
    <td class="auto-style1">Refrence No.</td>
    <td><div align="center">-</div></td>
    <td colspan="2"><asp:Label ID="lblRefrenceNo" runat="server" Text="-"></asp:Label></td>
    <td>&nbsp;</td>
  </tr>
  <tr>
    <td>&nbsp;</td>
    <td class="auto-style1">Transaction Date &amp; Time</td>
    <td><div align="center">-</div></td>
    <td><asp:Label ID="lblTransactionDate" runat="server" Text="-"></asp:Label></td>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
  </tr>
  <tr>
    <td>&nbsp;</td>
    <td class="auto-style1">Regulatory Fees</td>
    <td><div align="center">-</div></td>
    <td><span class="auto-style1">$</span><asp:Label ID="lblRegulatoryFees" runat="server" Text=""></asp:Label></td>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
  </tr>
  <tr>
    <td>&nbsp;</td>
    <td class="auto-style1">Total Sales Amount </td>
    <td><div align="center">-</div></td>
    <td><span class="auto-style1">$</span><asp:Label ID="lblSalesAmount" runat="server" Text=""></asp:Label></td>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
  </tr>
  <tr>
    <td>&nbsp;</td>
    <td class="auto-style1">&nbsp;</td>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
  </tr>
  <tr>
    <td>&nbsp;</td>
    <td class="auto-style1">&nbsp;</td>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
  </tr>
  <tr>
    <td>&nbsp;</td>
    <td colspan="4" class="auto-style1">Taxes and surcharges may apply and will be chraged by the retailer Seprate</td>
    <td>&nbsp;</td>
  </tr>
  <tr>
    <td>&nbsp;</td>
    <td colspan="4" class="auto-style1">NO REFUND or Exchange of Product given</td>
    <td>&nbsp;</td>
  </tr>
  <tr>
    <td>&nbsp;</td>
    <td colspan="4" class="auto-style1">See enkwirelessinc.com for terms and conditions.</td>
    <td>&nbsp;</td>
  </tr>
  <tr>
    <td>&nbsp;</td>
    <td class="auto-style1">&nbsp;</td>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
  </tr>
  <tr>
    <td>&nbsp;</td>
    <td class="auto-style1">&nbsp;</td>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
  </tr>
  <tr>
    <td>&nbsp;</td>
    <td class="auto-style1">&nbsp;</td>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
  </tr>
</table>
</div>
    </page>
</div>
                        <input type="button" onclick="printDiv('printableArea')" value="print a div!" />

</div>
<asp:Button ID="btnModalPopup" runat="server" Visible="false"   />--%>
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
                                <h2>New Activation</h2>
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
                                            <asp:HiddenField ID="hdnVendorID" runat="server" />
                                            <asp:HiddenField ID="hdnPurchaseID" runat="server" />


                                            <asp:HiddenField ID="hddnTariffType" runat="server" />
                                            <asp:HiddenField ID="hddnTariffTypeID" runat="server" />
                                            <asp:HiddenField ID="hddnTariffCode" runat="server" />
                                            <asp:HiddenField ID="hddnLycaAmount" runat="server" />
                                            <asp:HiddenField ID="hddnMonths" runat="server" />
                                            <asp:HiddenField ID="hddnPrepaidSIM" runat="server" />


                                            <asp:HiddenField ID="hddnAddOn" runat="server" />
                                            <asp:HiddenField ID="hddnInternational" runat="server" />


                                            <asp:HiddenField ID="hddnDataAddOnValue" runat="server" />
                                            <asp:HiddenField ID="hddnDataAddOnDiscountedAmount" runat="server" />
                                            <asp:HiddenField ID="hddnDataAddOnDiscountPercent" runat="server" />

                                            <asp:HiddenField ID="hddnInternationalCreditValue" runat="server" />
                                            <asp:HiddenField ID="hddnInternationalCreditDiscountedAmount" runat="server" />
                                            <asp:HiddenField ID="hddnInternationalCreditDiscountPercent" runat="server" />




                                        </div>
                                        <%-- <asp:Label ForeColor="Green" cssClass="label pull-right" ID="Label1" runat="server" Text=" Download App from Google Play Store"></asp:Label>
                                   <asp:HyperLink id="hyperlink1" Width="99px"   CssClass="label pull-right" ImageUrl="img/playstore.png" NavigateUrl="https://play.google.com/store/apps/details?id=com.virtuzoconsultancyservicespvtltd.ENK&hl=en"         ToolTip  ="Download Android App"
                                    Target="_blank" runat="server"/> --%>
                                        <div class="row">
                                            <div class="form-group col-md-6">
                                                <label class="label">SIM CARD</label>
                                                <label class="input">
                                                    <asp:TextBox ID="txtSIMCARD" AutoPostBack="true" OnClick=""  OnTextChanged="txtSIMCARD_TextChanged" CssClass="form-control" title="Enter your SIM card number" runat="server"></asp:TextBox>
                                                    <b class="tooltip tooltip-top-right">
                                                        <i class="fa fa-arrow-circle-down"></i>Enter SIM card number</b>
                                                </label>
                                            </div>


                                            <div id="div1" runat="server" class="form-group col-md-6">
                                                <label class="label">Network</label>
                                                <label class="input">
                                                    <asp:DropDownList ID="ddlNetwork" class="form-control chosen-select text-area" runat="server" OnSelectedIndexChanged="ddlNetwork_SelectedIndexChanged" AutoPostBack="true">
                                                    </asp:DropDownList>
                                                </label>
                                            </div>






                                            <%-- ankit--%>
                                        </div>
                                        <div class="row">
                                            <div id="divActivation" runat="server" class="form-group col-md-6">
                                                <label class="label">Plan</label>
                                                <label class="input">
                                                    <asp:DropDownList ID="ddlTariff" class="form-control chosen-select text-area" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlTariff_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </label>
                                            </div>
                                            <div class="form-group col-md-6">
                                                <label class="label">
                                                    Month(s)<asp:Label ID="lblMonth" ForeColor="Red" runat="server" Text=""></asp:Label>
                                                    <%--<asp:Label ID="Label2" runat="server" Text=""  ForeColor="Red"></asp:Label>     --%>
                                                </label>
                                                <label class="input">
                                                    <asp:DropDownList ID="ddlMonth" runat="server" class="form-control chosen-select text-area" AutoPostBack="true" OnSelectedIndexChanged="ddlMonth_SelectedIndexChanged">
                                                        <asp:ListItem Text="1" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="2" Value="2"></asp:ListItem>
                                                        <asp:ListItem Text="3" Value="3"></asp:ListItem>
                                                        <asp:ListItem Text="4" Value="4"></asp:ListItem>
                                                        <asp:ListItem Text="5" Value="5"></asp:ListItem>
                                                        <asp:ListItem Text="6" Value="6"></asp:ListItem>
                                                    </asp:DropDownList>
                                                    <%--<asp:TextBox ID="txtMonth" CssClass="form-control" Text="1" runat="server" MaxLength="5" onkeypress="javascript:return onlynumeric(event,this)" AutoPostBack="true" OnTextChanged="txtMonth_TextChanged"></asp:TextBox>--%>
                                                    <b class="tooltip tooltip-top-right">
                                                        <i class="fa fa-arrow-circle-down"></i></b>
                                                </label>
                                            </div>
                                        </div>



                                        <div class="row">
                                            <div id="div2" runat="server" class="form-group col-md-6" visible="false">
                                                <label class="label">Data Add On</label>
                                                <label class="input">
                                                    <asp:DropDownList ID="ddlDataAddOn" class="form-control chosen-select text-area" Enabled="false" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlDataAddOn_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </label>
                                            </div>
                                            <div id="div3" runat="server" class="form-group col-md-6" visible="false">
                                                <label class="label">International Credits</label>
                                                <label class="input">
                                                    <asp:DropDownList ID="ddlInternationalCreadits" class="form-control chosen-select text-area" Enabled="false" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlInternationalCreadits_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </label>
                                            </div>
                                        </div>



                                        <div class="row">
                                            <div id="divProduct" style="display: none" runat="server" class="form-group col-md-6 ">
                                                <label class="label">Product</label>
                                                <label class="input">
                                                    <asp:DropDownList ID="ddlProduct" class="form-control chosen-select text-area" OnSelectedIndexChanged="ddlProduct_SelectedIndexChanged" AutoPostBack="true" runat="server">
                                                    </asp:DropDownList>
                                                </label>
                                            </div>

                                        </div>
                                        <div class="row">
                                            <div id="divRegulatry" runat="server">
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

                                            <div class=" form-group col-md-6">
                                                <label class="label">Total Amount To Pay</label>

                                                <label class="input">
                                                    <asp:TextBox ID="txtAmountPay" CssClass="form-control" title="Enter your SIM card number" runat="server"></asp:TextBox>
                                                    <b class="tooltip tooltip-top-right">
                                                        <i class="fa fa-arrow-circle-down"></i>Amount To Pay</b>
                                                </label>
                                            </div>


                                            <%-- <div class="form-group col-md-6">   
                                    <label class="label">SIM CARD</label>
                                    <label class="input">   
                                    <asp:TextBox ID="txtSIMCARD" CssClass="form-control"  title="Enter your SIM card number" runat="server"></asp:TextBox>                           
                                     <b class="tooltip tooltip-top-right">
                                                <i class="fa fa-arrow-circle-down"></i> Enter SIM card number</b>    
                                    </label>
                                </div>                            --%>



                                            <div class="form-group col-md-6">
                                                <label class="label" id="lblZipcode" runat="server">ZIP Code<span style="color: red;">*</span></label>
                                                <label class="input">
                                                    <asp:TextBox ID="txtZIPCode" CssClass="form-control" MaxLength="10" title="Enter your ZIP code" runat="server"></asp:TextBox>
                                                    <b class="tooltip tooltip-top-right">
                                                        <i class="fa fa-arrow-circle-down"></i>Enter ZIP Code</b>
                                                </label>
                                            </div>

                                        </div>

                                        <%--add by akash starts--%>
                                       <%-- <div class="row" id="DivCountry" runat="server" visible="false">
                                             <div class="form-group col-md-6">
                                                <label class="label">Country<span style="color: red;"></span></label>
                                                <label class="input">
                                                  <asp:DropDownList ID="ddlCountry" class="form-control chosen-select text-area" ClientIDMode="Static" runat="server">
                                                      <%--<asp:ListItem Value="0" Text="select"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </label>
                                            </div>
                                        </div>--%>
                                        <%--add by akash ends--%>

                                        <div class="row">
                                            <div class="form-group  col-md-6">
                                                <%--<label class="label" >Email</label>--%>
                                                <label class="input">
                                                    <asp:TextBox ID="txtEmail" CssClass="form-control" Visible="false" MaxLength="50" title="Enter Email" runat="server"></asp:TextBox>
                                                    <b class="tooltip tooltip-top-right">
                                                        <i class="fa fa-arrow-circle-down"></i>Enter Email</b>
                                                </label>
                                            </div>

                                            <div class="btn-group  col-md-6 ">

                                                <label class="label"><font size="3"></font></label>
                                                <%-- <input type="button" id="btnPortIn" value="Port In" class="btn btn-primary" />--%>
                                                <%-- <asp:Button ID="btnPortIn" runat="server" autopostback="true" class="btn btn-primary" text ="PortIn"/>     --%>

                                                <div id="ck-button">
                                                    <label>
                                                        <asp:CheckBox ID="chkActivePort" runat="server" AutoPostBack="true" OnCheckedChanged="chkActivePort_CheckedChanged" />
                                                        <span>Port In</span>
                                                    </label>
                                                </div>
                                                <%--   </label>--%>
                                            </div>

                                            <div id="divCity" style="display: none" runat="server" class="form-group col-md-6 ">
                                                <label class="label">City</label>
                                                <label class="input">
                                                    <asp:TextBox ID="txtCity" CssClass="form-control" MaxLength="50" title="Enter CITY" runat="server"></asp:TextBox>
                                                    <b class="tooltip tooltip-top-right">
                                                        <i class="fa fa-arrow-circle-down"></i>Enter CITY</b>
                                                </label>
                                            </div>
                                        </div>


                                        <%-- /////////////////////--%>
                                        <div id="DivPort" runat="server" visible="false">

                                            <div class="row">


                                                <div class="form-group col-md-6">
                                                    <label class="label">PHONE # TO PORT</label>
                                                    <label class="input">
                                                        <asp:TextBox ID="txtPHONETOPORT" CssClass="form-control" MaxLength="10" title="Enter your PHONE # TO PORT" runat="server"></asp:TextBox>
                                                        <b class="tooltip tooltip-top-right">
                                                            <i class="fa fa-arrow-circle-down"></i>Enter PHONE # TO PORT</b>
                                                    </label>
                                                </div>

                                                <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">
                                                    <label class="label">ACCOUNT #</label>
                                                    <label class="input">
                                                        <asp:TextBox ID="txtACCOUNT" CssClass="form-control" MaxLength="100" title="Enter your ACCOUNT #" runat="server"></asp:TextBox>
                                                        <b class="tooltip tooltip-top-right">
                                                            <i class="fa fa-arrow-circle-down"></i>Enter ACCOUNT #</b>
                                                    </label>
                                                </div>


                                                <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">
                                                    <label class="label">PIN</label>
                                                    <label class="input">
                                                        <asp:TextBox ID="txtPIN" CssClass="form-control" MaxLength="10" title="Account Password/Pin" runat="server"></asp:TextBox>
                                                        <b class="tooltip tooltip-top-right">
                                                            <i class="fa fa-arrow-circle-down"></i>Enter PIN</b>
                                                    </label>
                                                </div>
                                            </div>
                                        </div>
                                        <%--////////////////////--%>

                                        <div class="row">
                                            <div id="divahannel" runat="server" class="form-group col-md-6">
                                                <label class="label">Channel ID</label>
                                                <label class="input">
                                                    <asp:TextBox ID="txtChannelID" CssClass="form-control" placeholder="Channel ID" runat="server" Text=""></asp:TextBox>
                                                    <b class="tooltip tooltip-top-right">
                                                        <i class="fa fa-arrow-circle-down"></i>Enter Channel ID</b>
                                                </label>
                                            </div>
                                            <div id="divLanguage" runat="server" class="form-group col-md-6 ">
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
                                        </div>
                                    </div>

                                </div>
                                <footer class="boxBtnsubmit">
                                    <div class="col-md-9 text-left">
                                        <div style="font-size: 12px;">
                                            *While doing activation if amount not debited from the balance then it will be debited during our reconciliation process over night 
                                <br>
                                            *If activation get failed please try sim card again for activation and will get a successful message with a current phone number on the SIM
                                <br>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <asp:Button ID="btnCompanyByAccount" class="btn btn-primary" runat="server" Visible="false" Text="Submit Using Account Balance" OnClick="btnCompanyByAccount_Click" OnClientClick="javascript:return CheckContinue();" />
                                        <asp:Button ID="btnDistributorByAccount" class="btn btn-primary" runat="server" Visible="false" Text="Submit Using Account Balance" OnClick="btnDistributorByAccount_Click" OnClientClick="javascript:return CheckContinue();" />
                                        <asp:ImageButton ID="imgByPaypal" runat="server" src='img/expresscheckout_buttons.png' class="btn btn-primary pul-right" border='0' align='top' alt='Check out with PayPal' Visible="false" OnClientClick="javascript:return CheckContinue();" OnClick="imgByPaypal_Click" Style="padding: 0px!important; background-color: white!important; border-color: white!important; height: 35px;" />
                                        <asp:Label ID="lblPaypalMsg" runat="server" Visible="false" Text="Extra 3% will be charge if you pay with payPal" ForeColor="Red"></asp:Label>
                                        <asp:Button ID="btnUpdateMSISDN" runat="server" Visible="False" Text="Button" OnClick="btnUpdateMSISDN_Click" />
                                    </div>
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





    <!-- Modal -->

    <%--//////////////--%>









    <script src="js/jquery-2.1.1.js"></script>
    <script src="js/plugin/chosen/chosen.jquery.js"></script>
    <script type="text/javascript">
        document.onkeydown = function (e) {
            var key;
            if (window.event) {
                key = event.keyCode
            }
            else {
                var unicode = e.keyCode ? e.keyCode : e.charCode
                key = unicode
            }

            switch (key) {//event.keyCode
                case 116: //F5 button
                    event.returnValue = false;
                    key = 0; //event.keyCode = 0;
                    return false;
                case 82: //R button
                    if (event.ctrlKey) {
                        event.returnValue = false;
                        key = 0; //event.keyCode = 0;
                        return false;
                    }
                case 91: // ctrl + R Button
                    event.returnValue = false;
                    key = 0;
                    return false;
            }
        }


        $(document).ready(function () {
            //Disable cut copy paste
            //$('body').bind('cut copy paste', function (e) {
            //    e.preventDefault();
            //});

            //Disable mouse right click
            $("body").on("contextmenu", function (e) {
                return false;
            });
        });

    </script>


    <script>

        history.pushState({ page: 1 }, "Title 1", "#no-back");
        window.onhashchange = function (event) {
            window.location.hash = "no-back";
        };

        $(document).ready(function () {
            function disableBack() { window.history.forward() }

            window.onload = disableBack();
            window.onpageshow = function (evt) { if (evt.persisted) disableBack() }
        });


        function detectRefresh() {
            try {
                if (window.opener.title == undefined) {
                    isRefresh = true;
                    document.write('Window was refreshed!');
                }
            }
            catch (err) {
                isRefresh = false;
                document.write('Window was closed!');
            }
        }

    </script>
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
    <script>
        function CheckContinue() {
            var text;
                   
            var res = confirm("Are you sure to continue?");
            if (res == true) {
                //   validateControlsActivate();
                if (validateControlsActivate() == true) {
                    return true;
                }
                else {
                    return false;
                }
            }
            else {
                return false;
            }
        }

    </script>
  
</asp:Content>

