<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="SimTariffMapping.aspx.cs" Inherits="ENK.SimTariffMapping" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">

        function validate() {
            if (document.getElementById('<%=ddlNetwork.ClientID%>').selectedIndex == 0) {
                alert('Please Select Network');
                document.getElementById("<%=ddlNetwork.ClientID%>").focus();
                return false;
            }
            if (document.getElementById('<%=ddlTariff.ClientID%>').selectedIndex == 0) {
                alert('Please Select Tariff Plan');
                document.getElementById("<%=ddlTariff.ClientID%>").focus();
                return false;
            }

            return true;
        }



        function validateUn() {
            if (document.getElementById('<%=ddlNetwork.ClientID%>').selectedIndex == 0) {
                alert('Please Select Network');
                document.getElementById("<%=ddlNetwork.ClientID%>").focus();
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

        <!-- RIBBON -->

        <!-- END RIBBON -->

        <!-- MAIN CONTENT -->
        <div id="content">
            <div class="row new-inerpage">
                <div class="col-xs-12 col-sm-5 col-md-5 col-lg-4">
                    <h1 class="page-title txt-color-blueDark">
                        <i class="fa fa-lg fa-fw fa-money"></i>
                        Inventory
							<span>> 
								Sim Plan Mapping						</span></h1>
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
                                <h2>Sim Plan Mapping</h2>
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
                                        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                            <label class="label">Network</label>
                                            <label class="input">

                                                <asp:DropDownList class="chosen-select text-area" ID="ddlNetwork" OnSelectedIndexChanged="ddlNetwork_SelectedIndexChanged" AutoPostBack="true" runat="server">
                                                </asp:DropDownList>




                                            </label>
                                        </div>
                                        <div class="row">
                                            <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">
                                                <label class="label">Plan</label>
                                                <label class="input">
                                                    <asp:DropDownList class="chosen-select text-area" ID="ddlTariff" runat="server" OnSelectedIndexChanged="ddlTariff_SelectedIndexChanged" AutoPostBack="true">
                                                    </asp:DropDownList>
                                                </label>
                                            </div>

                                            <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">
                                                <label class="label">Months</label>
                                                <label class="input">
                                                        <asp:DropDownList ID="ddlMonth" runat="server" class="form-control chosen-select text-area" >
                                                    <%--    <asp:ListItem Text="1" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="2" Value="2"></asp:ListItem>
                                                        <asp:ListItem Text="3" Value="3"></asp:ListItem>
                                                        <asp:ListItem Text="4" Value="4"></asp:ListItem>
                                                        <asp:ListItem Text="5" Value="5"></asp:ListItem>
                                                        <asp:ListItem Text="6" Value="6"></asp:ListItem>--%>
                                                    </asp:DropDownList>
                                                  <%--  <asp:TextBox ID="txtMonths" CssClass="form-control" MaxLength="10" title="Months" runat="server" AutoPostBack="true"></asp:TextBox>--%>
                                                    <b class="tooltip tooltip-top-right">
                                                        <i class="fa fa-arrow-circle-down"></i>Months Should be less than equal to 6</b>
                                                </label>
                                            </div>

                                            <%--                                <div class="col-xs-12 col-sm-4 col-md-4 col-lg-4"> 
                                <label class="label">PUK</label>
                                        <label class="input" >
                                <asp:TextBox ID="txtPUK"  CssClass="form-control" MaxLength="20" title="PUK" runat="server" AutoPostBack="true"></asp:TextBox>                                
                                            <b class="tooltip tooltip-top-right">
                                        <i class="fa fa-arrow-circle-down"></i>Enter PUK</b>                            
                                </label>
                                </div>--%>
                                        </div>

                                        <div class="row">
                                            <div class="col-xs-12 col-sm-3 col-md-3 col-lg-5">
                                                <asp:CheckBox ID="chkBulkUpload" OnCheckedChanged="chkBulkUpload_CheckedChanged" Visible="true" AutoPostBack="true" runat="server" Checked="false" />&nbsp 
                                    <asp:Label ID="lblBulkTransfer" runat="server" Text="Bulk Import"></asp:Label>
                                            </div>
                                            <div class="col-xs-12 col-sm-3 col-md-3 col-lg-7">
                                            </div>
                                        </div>

                                        <div class="row" id="BulkUpload" runat="server" visible="false">
                                            <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">
                                                <label class="label">Bulk Upload</label>
                                                <label class="input">
                                                    <%--<i class="icon-append fa fa-lock"></i>--%>
                                                    <%--<asp:TextBox ID="TextBox1" runat="server" title="Address 2"></asp:TextBox>--%>
                                                    <asp:FileUpload ID="fuBulkUpload" runat="server" />
                                                    <%--<b class="tooltip tooltip-top-right">
                                                    <i class="fa fa-lock txt-color-teal"></i> Enter PIN</b> --%>
                                                </label>
                                            </div>
                                            <div class="col-xs-12 col-sm-6 col-md-6 col-lg-4">
                                                <label class="label"></label>
                                                </br>
                                                    <asp:Button ID="btnUploadFile" runat="server" OnClick="btnUploadFile_Click" Text="Upload" Height="20px" Width="100px" />



                                            </div>
                                            <div class="col-xs-12 col-sm-6 col-md-6 col-lg-2 " style="float: right!important; color: #7E8184!important;">
                                                <label class="label">&nbsp;</label>

                                                <ul id="Ul1" style="float: right!important;">
                                                    <li id="liSim" runat="server" visible="true" class="sparks-info">
                                                        <a target="_blank" href="Format/BlankSIMPurchase.csv" style="float: right!important; color: #7E8184!important;" title="Blank SIM Bulk Upload Format">
                                                            <span><i class="fa fa-arrow-circle-o-down"></i></span>Bulk Format</a>

                                                    </li>
                                                    <li id="liMobile" runat="server" visible="false" class="sparks-info">
                                                        <a target="_blank" href="Format/MobileSIMPurchase.csv" style="float: right!important; color: #7E8184!important;" title="Mobile SIM Bulk Upload Format">
                                                            <span><i class="fa fa-arrow-circle-o-down"></i></span>Mobile SIM Format</a>
                                                    </li>

                                                </ul>
                                            </div>
                                        </div>





                                        <div class="row">
                                            <div class="col-xs-12 col-sm-3 col-md-3 col-lg-3">
                                                <label class="input">
                                                    <span>Sim Number</span>
                                                    <asp:TextBox ID="TxtSimNumb" title="SimNumb" runat="server" Text="8919601" MaxLength="18"></asp:TextBox>
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

                                            <div class="col-xs-12 col-sm-3 col-md-3 col-lg-3">
                                                <label class="input">
                                                    <label class="label">From Sim</label>
                                                    <asp:TextBox ID="txtFromSim" title="FromSim" runat="server" Text="8919601" MaxLength="18"></asp:TextBox>
                                                    <b class="tooltip tooltip-top-right">
                                                        <i class="fa fa-arrow-circle-down"></i>Only Numeric</b>
                                                </label>
                                            </div>

                                            <div class="col-xs-12 col-sm-1 col-md-1 col-lg-1">
                                                <label class="label"><b>TO</b></label>
                                                <label class="input">
                                                    <b class="tooltip tooltip-top-right">
                                                        <i class="fa fa-arrow-circle-down"></i>or</b>
                                                </label>
                                            </div>

                                            <div class="col-xs-12 col-sm-3 col-md-3 col-lg-3">

                                                <label class="input">
                                                    <label class="label">To Sim</label>
                                                    <asp:TextBox ID="txtTosim" title="Tosim" runat="server" Text="8919601" MaxLength="18"></asp:TextBox>
                                                    <b class="tooltip tooltip-top-right">
                                                        <i class="fa fa-arrow-circle-down"></i>Only Numeric</b>
                                                </label>
                                            </div>

                                            <div class="col-xs-12 col-sm-1 col-md-1 col-lg-1">
                                                <asp:Button ID="btnSearch" class="btn btn-primary" runat="server" Text="Search" OnClick="btnSearch_Click"/>                                                   
                                            </div>

                                        </div>


                                        <div class="row">
                                            <div class="col-xs-12 col-sm-10 col-md-10 col-lg-10"></div>
                                                <div class="col-xs-12 col-sm-2 col-md-2 col-lg-2">
                                                 <span><b>SIM COUNT :</b></span>
                                                <b> <asp:Label ID="lblCount" runat="server" Text=""></asp:Label></b>
                                             </div>
                                        </div>
                                        
                                        <div class="row" id="DivSIM" runat="server" visible="true">
                                            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                                <label class="label">&nbsp;</label>
                                                <%-- <div class="table-responsive" id="dd" runat="server" visible="false">
                                       <table class="table table-bordered" id="dataTables-example">
                                                <thead>
                                                    <tr>
                                                        <th style="width: 24px;"></th>
                                                        <th style="width: 101px;">Tariff Code</th>
                                                        <th style="width: 200px;">Tariff Description</th>
                                                        <th>Default Tariff</th>                                                         
                                                    </tr> 
                                                </thead>    
                                       </table>
                                       </div>--%>
                                                <div class="table-responsive" style="max-height: 237px; overflow-y: auto;">

                                                    <asp:Repeater ID="RepeaterTransferSIM" runat="server">
                                                        <HeaderTemplate>
                                                            <table class="table table-bordered" id="dataTables-example">
                                                                <thead>
                                                                    <tr>
                                                                        <th>
                                                                            <asp:CheckBox ID="chkAllSIM" OnCheckedChanged="chkAllSIM_OnCheckedChanged" AutoPostBack="true" runat="server" /></th>

                                                                        <th>Sim Number</th>
                                                                        <th>PUK</th>
                                                                        <th>Tariff Plan</th>
                                                                        <th>Months</th>

                                                                    </tr>
                                                                    <tbody>
                                                        </HeaderTemplate>

                                                        <ItemTemplate>
                                                            <tr>

                                                                <td>
                                                                    <asp:CheckBox ID="CheckBox2" runat="server" />
                                                                </td>

                                                                <td>
                                                                    <asp:Label runat="server" ID="Label7" Text='<%# Eval("SimNo") %>' />
                                                                    <asp:HiddenField ID="hdnSIMID" runat="server" Value='<%# Bind("SIMID") %>' />
                                                                </td>
                                                                <td>

                                                                    <asp:Label runat="server" ID="Label4" Text='<%# Eval("PUK") %>' />
                                                                </td>
                                                                <td>
                                                                    <asp:Label runat="server" ID="Label1" Text='<%# Eval("TariffCode") %>' />
                                                                </td>
                                                                <td>
                                                                    <asp:Label runat="server" ID="Label2" Text='<%# Eval("MONTHS") %>' />
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

                                    </div>

                                    <footer class="boxBtnsubmit">
                                        <asp:Button ID="btnUnMapped" runat="server" Text="UnMapping" OnClick="btnUnMapped_Click" OnClientClick="return confirm('Do you want to UnMapping Simnumber ?');" class="btn btn-primary" />
                                        <asp:Button ID="btnReset" runat="server" Text="Reset" OnClick="btnReset_Click" class="btn btn-primary" />
                                        <asp:Button ID="btnSubmit" runat="server" Text="Mapping" OnClick="btnSubmit_Click" OnClientClick="javascript:return validate();" class="btn btn-primary" />

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
    <div id="popupdiv" class="loading" title="Basic modal dialog" style="display: nonefont-family:Arial; font-size: 10pt; border: 2px solid #67CFF5; text-align: -webkit-center;">

        <b style="text-align: center!important;">Please Wait...........</b>
        <br />
        <br />
        <img src="img/loader.gif" alt="" style="text-align: center!important;" />
    </div>

    <script src="js/jquery-2.1.1.js"></script>
    <script src="js/plugin/chosen/chosen.jquery.js"></script>


    <script>
        $(document).ready(function () {
            $("#ContentPlaceHolder1_txtMonths").keydown(function (e) {
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

            //$('#ContentPlaceHolder1_TxtRefno').keypress(function (e) {
            //    var regex = new RegExp("^[0-9a-zA-Z]+$");
            //    var str = String.fromCharCode(!e.charCode ? e.which : e.charCode);
            //    if (regex.test(str)) {
            //        return true;
            //    }
            //    e.preventDefault();
            //    return false;
            //});
        });
    </script>


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

     <script>
         $(document).ready(function () {
             $("#ContentPlaceHolder1_TxtSimNumb").keydown(function (e) {
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


         $(document).ready(function () {
             var keyDown = false, ctrl = 17, vKey = 86, Vkey = 118;

             $("#ContentPlaceHolder1_TxtSimNumb").keydown(function (e) {
                 if (e.keyCode == ctrl) keyDown = true;
             }).keyup(function (e) {
                 if (e.keyCode == ctrl) keyDown = false;
             });

             $("#ContentPlaceHolder1_TxtSimNumb").on('keypress', function (e) {
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

             $("#ContentPlaceHolder1_txtFromSim").keydown(function (e) {
                 if (e.keyCode == ctrl) keyDown = true;
             }).keyup(function (e) {
                 if (e.keyCode == ctrl) keyDown = false;
             });

             $("#ContentPlaceHolder1_txtFromSim").on('keypress', function (e) {
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

             $("#ContentPlaceHolder1_txtTosim").keydown(function (e) {
                 if (e.keyCode == ctrl) keyDown = true;
             }).keyup(function (e) {
                 if (e.keyCode == ctrl) keyDown = false;
             });

             $("#ContentPlaceHolder1_txtTosim").on('keypress', function (e) {
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

</asp:Content>
