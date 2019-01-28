<%@ Page Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="Topup.aspx.cs" Inherits="ENK.Topup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--<link type="text/css" rel="stylesheet" href="http://code.jquery.com/ui/1.10.3/themes/smoothness/jquery-ui.css" />
       <script type="text/javascript" src="http://code.jquery.com/jquery-1.8.2.js"></script>
       <script type="text/javascript" src="http://code.jquery.com/ui/1.10.3/jquery-ui.js"></script>--%>
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
            position: fixed;
            background-color: White;
            z-index: 999;
        }
    </style>
    <script type="text/javascript">

        function Confirm() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Do you want to deduct Amount?")) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }

        function validate() {

            if (document.getElementById('<%=ddlDistributor.ClientID%>').selectedIndex == 0) {
                alert('Please Select Distributor');
                document.getElementById("<%=ddlDistributor.ClientID%>").focus();
                return false;
            }


            if (document.getElementById("<%=txtTopupAmount.ClientID%>").value == "") {

                var lbl = document.getElementById("<%=lblwarning.ClientID%>");
                lbl.innerText = "Topup Amount Can't be Blank";
                lbl.style.display = "block";
                // alert("pankaj");
                document.getElementById("<%=txtTopupAmount.ClientID%>").focus();

                return false;
            }
            return true;
        }
        function validateTopup() {

            if (document.getElementById("<%=txtTopupAmount.ClientID%>").value == "") {

                var lbl = document.getElementById("<%=lblwarning.ClientID%>");
                lbl.innerText = "Topup Amount Can't be Blank";
                lbl.style.display = "block";
                // alert("pankaj");
                document.getElementById("<%=txtTopupAmount.ClientID%>").focus();

                return false;
            }
            return true;
        }

        function onlynumericDecimal(evt, cnt) {

            var keycode;
            var textbox = cnt;
            if (window.event) {
                event = window.event;
            }

            // alert(textbox.value);
            var divError = document.getElementById("<%=lblwarning.ClientID%>");
            divError.innerText = "";
            divError.style.display = "none";


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
                        alert("Only one decimal allowed.");
                        return false;
                    }
                    else {

                        return true;
                    }

                }
                else {
                    alert("Only numbers allowed.");

                    return false; // disable key press
                }


            }

        }//end



    </script>

    <link href="css/plugins/chosen/chosen.css" rel="stylesheet" />
    <style>
        .chosen-container-single .chosen-single
        {
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

    <div id="main" role="main" style="margin-top: 0px!important;">

        <!-- RIBBON -->

        <!-- END RIBBON -->

        <!-- MAIN CONTENT -->
        <div id="content">
            <div class="row new-inerpage">
                <div class="col-xs-12 col-sm-5 col-md-5 col-lg-4">
                    <h1 class="page-title txt-color-blueDark">
                        <i class="fa fa-user fa-fw"></i>
                        Admin
							<span>> 
								Add Funds						</span></h1>
                </div>
                <div class="col-xs-12 col-sm-7 col-md-7 col-lg-8">
                    <ul id="sparks" class="">
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
                        <div class="jarviswidget jarviswidget-color-blueDark" id="wid-id-0" data-widget-colorbutton="false" data-widget-editbutton="false" data-widget-togglebutton="false" data-widget-deletebutton="false" data-widget-fullscreenbutton="false" data-widget-custombutton="false" data-widget-collapsed="false" data-widget-sortable="false">
                            <header>
                                <span class="widget-icon"><i class="fa fa-table"></i></span>
                                <h2>New Topup</h2>
                            </header>

                            <!-- widget edit box -->
                            <div class="jarviswidget-editbox">
                                <!-- This area used as dropdown edit box -->
                            </div>
                            <div id="login-form" class="smart-form client-form">
                                <div class="box-input">
                                    <div class="row">
                                        <div id="div1" runat="server">
                                            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                                <div id="divDistributor" runat="server" class="col-xs-12 col-sm-6 col-md-6 col-lg-6">
                                                    <div class="col-xs-12 col-sm-3 col-md-3 col-lg-3">
                                                        <label class="label">Distributor Name</label>
                                                    </div>
                                                    <div class="col-xs-12 col-sm-3 col-md-3 col-lg-6">
                                                        <asp:Label ID="lblDistributor" runat="server" Text=""></asp:Label>
                                                        <asp:HiddenField ID="hddnDistributorID" runat="server" />

                                                    </div>
                                                </div>
                                                <div id="divCompany" runat="server" class="col-xs-12 col-sm-6 col-md-6 col-lg-6">
                                                    <div class="col-xs-12 col-sm-3 col-md-3 col-lg-3">
                                                        <label class="label">Distributor Name</label>
                                                    </div>
                                                    <div class="col-xs-12 col-sm-3 col-md-3 col-lg-6">
                                                        <label class="input">
                                                            <asp:DropDownList class="chosen-select text-area" ID="ddlDistributor" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlDistributor_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </label>
                                                    </div>
                                                </div>
                                                <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">
                                                </div>
                                            </div>
                                            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                                <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">
                                                    <div class="col-xs-12 col-sm-3 col-md-3 col-lg-3">
                                                        <label class="label">Balance</label>
                                                    </div>
                                                    <div class="col-xs-12 col-sm-3 col-md-3 col-lg-6">
                                                        <asp:Label ID="lblBalanceAmount" runat="server" Text=""></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">
                                                  <b><asp:Label ID="lblTopupCommission" runat="server" Text="" style="color:red;" Visible="false"></asp:Label></b>
                                                </div>
                                            </div>
                                            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                                <div id="divProceed" runat="server" class="col-xs-12 col-sm-6 col-md-6 col-lg-6">
                                                    <div class="col-xs-12 col-sm-3 col-md-3 col-lg-3">
                                                        <label class="label">Topup Amount</label>
                                                    </div>
                                                    <div class="col-xs-12 col-sm-3 col-md-3 col-lg-8">
                                                        <asp:TextBox ID="txtTopupAmount" runat="server" title="Amount.." Height="30px" onkeypress="javascript:return onlynumericDecimal(event,this);"></asp:TextBox>
                                                        <asp:Button ID="btnRetry" runat="server" Text="Retry" Visible="false" class="btn btn-primary pul-left" OnClick="btnRetry_Click" /> 
                                                        <asp:Label ID="lblwarning" runat="server" Style="display: none;" ForeColor="Red" Text=""></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                                <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">
                                                    <div class="col-xs-12 col-sm-3 col-md-3 col-lg-3">
                                                        <label class="label">Reason (Optional)</label>
                                                    </div>
                                                    <div class="col-xs-12 col-sm-3 col-md-3 col-lg-6">
                                                        <asp:TextBox id="txtReason" runat="server" TextMode="MultiLine" ></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">
                                                </div>

                                                <div id="divpaypal" runat="server" visible="false" class="col-xs-12 col-sm-6 col-md-6 col-lg-6">
                                                    <%--<label class="label">You will be additionally 3% charged if you pay via PayPal</label>--%>
                                                    <div class="col-xs-12 col-sm-3 col-md-3 col-lg-3">
                                                        <label class="label">Charged Amount</label>
                                                    </div>
                                                    <div class="col-xs-12 col-sm-3 col-md-3 col-lg-6">
                                                        <asp:HiddenField ID="hddnFinalAmount" runat="server" />
                                                        <asp:Label ID="lblFinalAmount" runat="server" ForeColor="Red" Text=""></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">
                                                </div>
                                            </div>
                                            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                                <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">
                                                    <footer class="boxBtnsubmit" style="float: left;">

                                                        <asp:ImageButton ID="btnPaypal" runat="server" src='img/expresscheckout_buttons.png' class="btn btn-primary pul-left"  ClientIDMode="Static"  border='0' align='top' alt='Check out with PayPal' Visible="false" OnClientClick="javascript:return validateTopup();" OnClick="btnPaypal_Click" Style="padding: 0px!important; background-color: white!important; border-color: white!important;" />

                                                        <asp:Button ID="btndeduct" runat="server" Text="Deduct Amount" class="btn btn-primary pul-left"
                                                            OnClientClick="return confirm('Do you want to deduct Amount ?')" OnClick="btndeduct_Click" />

                                                        <asp:Button ID="Button1" runat="server" Text="Cancel" Visible="false" class="btn btn-primary pul-left" OnClick="Button1_Click" />
                                                        <asp:Button ID="btnCheckAmount" runat="server" Text="TOPUP"  ClientIDMode="Static" Visible="false" class="btn btn-primary pul-left" OnClick="btnCheckAmount_Click" />
                                                        <asp:Button ID="btnCompanyTopup" runat="server" Text="TOPUP"  ClientIDMode="Static" Visible="false" class="btn btn-primary pul-left" OnClick="btnCompanyTopup_Click" OnClientClick="javascript:return validate();" />
                                                        <%-- OnClientClick="javascript:shouldSubmit=true; return confirm('Are you sure?'); return validate(); " OnClick="btndeduct_Click"  />--%>
                                                    </footer>
                                                </div>
                                                <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">
                                                    
                                                    <span style="color:red;" ><strong>Note:-</strong> If in case, after paypal transaction you are not redirect to the <%= ConfigurationManager.AppSettings["T_Name"] %> account and see the updated amount, then call for assistance : <strong><%= ConfigurationManager.AppSettings["COMPANY_PHONE"] %></strong> & Email : <strong><%= ConfigurationManager.AppSettings["COMPANY_EMAIL"] %></strong></span> 
                                                </div>
                                            </div>

                                        </div>

                                    </div>
                                </div>
                            </div>
                        </div>
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
    <%--</div>--%>
    <!-- END MAIN PANEL -->
    </div>
    <div id="popupdiv" class="loading" title="Basic modal dialog" style="display: nonefont-family:Arial; font-size: 10pt; border: 2px solid #67CFF5; text-align: -webkit-center;">

        <b style="text-align: center!important;">Please Wait...........</b>
        <br />
        <br />
        <img src="img/loader.gif" alt="" style="text-align: center!important;" />
    </div>

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

