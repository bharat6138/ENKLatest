<%@ Page Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="ActivatePreloadedSim.aspx.cs" Inherits="ENK.ActivatePreloadedSim" %>
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

            if (document.getElementById("<%=txtAmountPay.ClientID%>").value == "") {
                alert("Amount to Pay Can't be blank");
                document.getElementById("<%=txtAmountPay.ClientID%>").focus();
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
            <%--var email = document.getElementById("<%=txtEmail.ClientID%>");
            var filter = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;--%>

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

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- MAIN PANEL -->
    <div id="main" role="main" style="margin-top: 0px!important;">
        <asp:HiddenField ID="hddnActivation" runat="server" />
        <!-- RIBBON -->

        <!-- END RIBBON -->

        <!-- MAIN CONTENT -->
        <div id = "content" >
                <div class="col-xs-12 col-sm-5 col-md-5 col-lg-4">
                    <h1 class="page-title txt-color-blueDark">
                        <i class="fa fa-lg fa-fw fa-pencil-square-o"></i>
                        Sim Provisioning
                            <span>> 
								Activate  SIM						</span></h1>
                </div>
                <div class="col-xs-12 col-sm-7 col-md-7 col-lg-8">
                </div>
            </div>

            <!-- widget grid -->
            <section id = "widget-grid" class="">

                <!-- row -->
                <div class="row">
                    <!-- NEW WIDGET START -->
                    <article class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                        <!-- Widget ID(each widget will need unique ID)-->
                        <div class="jarviswidget jarviswidget-color-blueDark" id="wid-id-0" data-widget-colorbutton="false" data-widget-editbutton="false" data-widget-togglebutton="false" data-widget-deletebutton="false" data-widget-fullscreenbutton="false" data-widget-custombutton="false" data-widget-collapsed="false" data-widget-sortable="false">
                         
                            <header>
                                <span class="widget-icon"><i class="fa fa-table"></i></span>
                                <h2>New Activation</h2>
                            </header>

                            <div id = "DivValCHECK" runat= "server" class="smart-form client-form box-input">
                                <div class="row">
                                    <div class="form-group col-md-4">
                                        <label class="label">SIM Card Number</label>
                                        <label class="input">
                                            <asp:TextBox ID = "txtSIMNO" CssClass="form-control" title="Enter your SIM NO" runat="server"></asp:TextBox>
                                        </label>
                                    </div>
                                    <div class="form-group col-md-2">
                                            <asp:Button ID = "btnCHECKPreloadedSim" Class="btn btn-primary" runat="server" Text="Validate PreloadedSim" OnClick="btnCHECKPreloadedSim_Click" style="margin-top:28px;" />
                                       
                                    </div>
                                </div>
                                <br /><br />
                                <div class="row"></div>
                            </div>

                            <!-- widget div-->
                            <div id = "DivVal" runat="server">


                                <div id = "login-form" class="smart-form client-form">




                                    <div class="box-input">

                                        <div class="row">
                                            <asp:HiddenField ID = "hdnVendorID" runat="server" />
                                            <asp:HiddenField ID = "hdnPurchaseID" runat="server" />


                                            <asp:HiddenField ID = "hddnTariffType" runat="server" />
                                            <asp:HiddenField ID = "hddnTariffTypeID" runat="server" />
                                            <asp:HiddenField ID = "hddnTariffCode" runat="server" />
                                            <asp:HiddenField ID = "hddnLycaAmount" runat="server" />
                                            <asp:HiddenField ID = "hddnMonths" runat="server" />
                                            <asp:HiddenField ID = "hddnPrepaidSIM" runat="server" />
                                            <asp:HiddenField ID = "hddnTariffID" runat="server" />

                                            <asp:HiddenField ID = "hddnAddOn" runat="server" />
                                            <asp:HiddenField ID = "hddnInternational" runat="server" />


                                            <asp:HiddenField ID = "hddnDataAddOnValue" runat="server" />
                                            <asp:HiddenField ID = "hddnDataAddOnDiscountedAmount" runat="server" />
                                            <asp:HiddenField ID = "hddnDataAddOnDiscountPercent" runat="server" />

                                            <asp:HiddenField ID = "hddnInternationalCreditValue" runat="server" />
                                            <asp:HiddenField ID = "hddnInternationalCreditDiscountedAmount" runat="server" />
                                            <asp:HiddenField ID = "hddnInternationalCreditDiscountPercent" runat="server" />




                                        </div>
                                        <%-- <asp:Label ForeColor = "Green" cssClass="label pull-right" ID="Label1" runat="server" Text=" Download App from Google Play Store"></asp:Label>
                                   <asp:HyperLink id = "hyperlink1" Width="99px"   CssClass="label pull-right" ImageUrl="img/playstore.png" NavigateUrl="https://play.google.com/store/apps/details?id=com.virtuzoconsultancyservicespvtltd.ENK&hl=en"         ToolTip  ="Download Android App"
                                    Target="_blank" runat="server"/> --%>



                                        <div class="row">
                                            <div class="form-group col-md-6">
                                                <label class="label">SIM CARD</label>
                                                <label class="input">
                                                    <asp:TextBox ID = "txtSIMCARD" AutoPostBack="true" OnClick="" CssClass="form-control" title="Enter your SIM card number" runat="server"></asp:TextBox>
                                                    <b class="tooltip tooltip-top-right">
                                                        <i class="fa fa-arrow-circle-down"></i>Enter SIM card number</b>
                                                </label>
                                            </div>


                                            <div id = "div1" runat= "server" class="form-group col-md-6">
                                                <label class="label">Network</label>
                                                <label class="input">
                                                    <asp:DropDownList ID = "ddlNetwork" class="form-control chosen-select text-area" runat="server">
                                                    </asp:DropDownList>
                                                </label>
                                            </div>
                                        </div>

                                        <div id = "div34" class="row" runat="server">
                                            <div id = "divActivation" runat="server" class="form-group col-md-6">
                                                <label class="label">Tariff</label>
                                                <label class="input">
                                                    <asp:DropDownList ID = "ddlTariff" class="form-control chosen-select text-area" runat="server" AutoPostBack="true">
                                                        <%--OnSelectedIndexChanged="ddlTariff_SelectedIndexChanged"--%>
                                                    </asp:DropDownList>
                                                </label>
                                            </div>
                                            <div class="form-group col-md-6">
                                                <label class="label">
                                                    Month(s)<asp:Label ID = "lblMonth" ForeColor="Red" runat="server" Text=""></asp:Label>
                                                    <%--<asp:Label ID = "Label2" runat="server" Text=""  ForeColor="Red"></asp:Label>     --%>
                                                </label>
                                                <label class="input">
                                                    <asp:TextBox ID = "txtMonth" CssClass="form-control" Text="0" runat="server" ></asp:TextBox>
                                                </label>
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div id = "div2" runat="server" class="form-group col-md-6">
                                                <label class="label">Data Add On</label>
                                                <label class="input">
                                                    <asp:DropDownList ID = "ddlDataAddOn" class="form-control chosen-select text-area" runat="server" AutoPostBack="true">
                                                    </asp:DropDownList>
                                                </label>
                                            </div>
                                            <div id = "div3" runat="server" class="form-group col-md-6">
                                                <label class="label">International Credits</label>
                                                <label class="input">
                                                    <asp:DropDownList ID = "ddlInternationalCreadits" class="form-control chosen-select text-area" runat="server" AutoPostBack="true">
                                                    </asp:DropDownList>
                                                </label>
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div id = "divProduct" style="display: none" runat="server" class="form-group col-md-6 ">
                                                <label class="label">Product</label>
                                                <label class="input">
                                                    <asp:DropDownList ID = "ddlProduct" class="form-control chosen-select text-area" AutoPostBack="true" runat="server">
                                                    </asp:DropDownList>
                                                </label>
                                            </div>

                                        </div>

                                        <div class="row">
                                            <div id = "divRegulatry" runat="server">
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
                                                    <asp:TextBox ID = "txtAmountPay" CssClass="form-control" title="Enter your SIM card number" runat="server"></asp:TextBox>
                                                    <b class="tooltip tooltip-top-right">
                                                        <i class="fa fa-arrow-circle-down"></i>Amount To Pay</b>
                                                </label>
                                            </div>


                                            <%-- <div class="form-group col-md-6">   
                                    <label class="label">SIM CARD</label>
                                    <label class="input">   
                                    <asp:TextBox ID = "txtSIMCARD" CssClass="form-control"  title="Enter your SIM card number" runat="server"></asp:TextBox>                           
                                     <b class="tooltip tooltip-top-right">
                                                <i class="fa fa-arrow-circle-down"></i> Enter SIM card number</b>
                                    </label>
                                </div>                            --%>









                                            <div class="form-group col-md-6">
                                                <label class="label">ZIP Code</label>
                                                <label class="input">
                                                    <asp:TextBox ID = "txtZIPCode" CssClass="form-control" MaxLength="10" title="Enter your ZIP code" runat="server"></asp:TextBox>
                                                    <b class="tooltip tooltip-top-right">
                                                        <i class="fa fa-arrow-circle-down"></i>Enter ZIP Code</b>
                                                </label>
                                            </div>

                                        </div>

                                        <div class="row">
                                            <div class="form-group  col-md-6">
                                              <%--  <label class="label">Email</label>
                                                <label class="input">
                                                    <asp:TextBox ID = "txtEmail" CssClass="form-control" MaxLength="50" title="Enter Email" runat="server"></asp:TextBox>
                                                    <b class="tooltip tooltip-top-right">
                                                        <i class="fa fa-arrow-circle-down"></i>Enter Email</b>
                                                </label>--%>
                                            </div>

                                             <div class="btn-group  col-md-6 ">

                                                <label class="label"><font size="3"></font></label>

                                                <div id="ck-button">
                                                    <label>
                                                        <asp:CheckBox ID="chkActivePort" runat="server" AutoPostBack="true" OnCheckedChanged="chkActivePort_CheckedChanged" />
                                                        <span>Port In</span>
                                                    </label>
                                                </div>
                                                <%--   </label>--%>
                                            </div>

                                            <div id = "divCity" style= "display: none" runat= "server" class="form-group col-md-6 ">
                                                <label class="label">City</label>
                                                <label class="input">
                                                    <asp:TextBox ID = "txtCity" CssClass="form-control" MaxLength="50" title="Enter CITY" runat="server"></asp:TextBox>
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
                                            <div id = "divahannel" runat="server" class="form-group col-md-6">
                                                <label class="label">Channel ID</label>
                                                <label class="input">
                                                    <asp:TextBox ID = "txtChannelID" CssClass="form-control" placeholder="Channel ID" runat="server" Text=""></asp:TextBox>
                                                    <b class="tooltip tooltip-top-right">
                                                        <i class="fa fa-arrow-circle-down"></i>Enter Channel ID</b>
                                                </label>
                                            </div>
                                            <div id = "divLanguage" runat="server" class="form-group col-md-6 ">
                                                <label class="label">Language</label>
                                                <label class="input">
                                                    <asp:DropDownList ID = "ddlLanguage" class="text-area" runat="server">
                                                        <asp:ListItem Value = "1" Text="English">
                                                        </asp:ListItem>
                                                        <asp:ListItem Value = "2" Text="Spanish">
                                                        </asp:ListItem>
                                                    </asp:DropDownList>
                                                </label>
                                            </div>
                                        </div>
                                    </div>

                                </div>
                                <footer class="boxBtnsubmit">
                                    <asp:Button ID = "btnCompanyByAccount" class="btn btn-primary" runat="server" Visible="false" Text="Submit Using Account Balance" OnClick="btnCompanyByAccount_Click" OnClientClick="javascript:return validateControlsActivate();" />
                                    <asp:Button ID = "btnDistributorByAccount" class="btn btn-primary" runat="server" Visible="false" Text="Submit Using Account Balance" OnClick="btnDistributorByAccount_Click" OnClientClick="javascript:return validateControlsActivate();" />
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
