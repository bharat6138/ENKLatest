<%@ Page Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="SimSwap.aspx.cs" Inherits="ENK.SimSwap" %>

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

    <%--<script>

        var minLength = 10;
        var maxLength = 10;
        $(document).ready(function () {
            $('#ContentPlaceHolder1_txtMSSIDN').on('keydown keyup change', function () {
                var char = $(this).val();
                var charLength = $(this).val().length;
                if (charLength < minLength) {
                    $('#mxno').text('Length is to short, minimum ' + minLength + ' required.');
                } else if (charLength > maxLength) {
                    $('#mxno').text('Length is not valid, maximum ' + maxLength + ' allowed.');
                    $(this).val(char.substring(0, maxLength));
                } else {
                    $('#mxno').text('Length is valid');
                }
            });
        });

    </script>--%>

    <script type="text/javascript">


        function ValidateSimNumber() {

            var a = document.getElementById('ContentPlaceHolder1_txtOldICCID').value.length;

            a = Number(a);
            if (a < 19) {
                alert("Sim Number should not be less than 19 digits");

            }
            if (a > 19) {
                alert("Sim Number should not be greater than 19 digits");

            }

        }
        function ValidateSimNumberNew() {

            var a = document.getElementById('ContentPlaceHolder1_txtNewICCID').value.length;

            a = Number(a);
            if (a < 19) {
                alert("Sim Number should not be less than 19 digits");

            }
            if (a > 19) {
                alert("Sim Number should not be greater than 19 digits");

            }

        }

        function ValMobNum() {

            var a = document.getElementById('ContentPlaceHolder1_txtMSSIDN').value.length;

            a = Number(a);
            if (a < 10) {
                alert("MSISDN should not be less than 10 digits");
                return false;

            }
            else if (a > 11) {
                alert("MSISDN should not be greater than 11 digits");
                return false;

            }

            else {
                return true;
            }
        }

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
                        Sim Swapping
							<%--<span> 
								PortIn Status						</span>--%></h1>
                </div>
            </div>










        </div>

        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">


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
                                <h2>Sim Swapping	</h2>
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
                                               <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">
                                                   <b><span style="color:white; font-size:20px; margin-left:12px; background-color:#023954;">Only for Lyca SIM</span></b>
                                                </div>
                                            </div>
                                        <div class="row">


                                            <div class="form-group col-xs-12 col-sm-12 col-md-12 col-lg-2">
                                                <div class="col-md-12">
                                                    <label class="control-label" style="padding-top: 10px;">MSISDN</label>

                                                    <asp:TextBox ID="txtMSSIDN" title="MSISDN" CssClass="form-control" runat="server" MaxLength="11"></asp:TextBox>
                                                    <b class="tooltip tooltip-top-right"><i class="fa fa-arrow-circle-down"></i>Only Numeric</b>
                                                </div>
                                            </div>

                                            <div class="form-group col-xs-12 col-sm-12 col-md-12 col-lg-4">
                                                <div class="col-md-12">
                                                    <label class="control-label" style="padding-top: 10px;">Old SIM Number</label>

                                                    <%--<asp:TextBox ID="txtOldICCID" title="MSISDN" CssClass="form-control" runat="server" MaxLength="19" onblur="ValidateSimNumber();"></asp:TextBox>--%>
                                                <asp:TextBox ID="txtOldICCID" title="MSISDN" CssClass="form-control" runat="server" MaxLength="19" ></asp:TextBox>
                                                        <b class="tooltip tooltip-top-right"><i class="fa fa-arrow-circle-down"></i>Only Numeric</b>
                                                </div>
                                            </div>


                                            <div class="form-group col-xs-12 col-sm-12 col-md-12 col-lg-4">
                                                <div class="col-md-12">
                                                    <label class="control-label" style="padding-top: 10px;">New SIM Number</label>

                                                   <%--<asp:TextBox ID="txtNewICCID" title="MSISDN" CssClass="form-control" runat="server" MaxLength="19" onblur="ValidateSimNumberNew();"></asp:TextBox>--%>
                                               <asp:TextBox ID="txtNewICCID" title="MSISDN" CssClass="form-control" runat="server" MaxLength="19" ></asp:TextBox>
                                                         <b class="tooltip tooltip-top-right"><i class="fa fa-arrow-circle-down"></i>Only Numeric</b>
                                                </div>
                                            </div>


                                            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-1" style="margin-top: 30px;">
                                                <asp:Button ID="btnGet" class="btn btn-primary btn-sm" runat="server" Text="Swap Sim" OnClick="btnGet_Click" />

                                            </div>

                                        </div>
                                        <div class="table-responsive">
                                            <asp:GridView ID="GrdSwappedSim" class="table table-bordered" runat="server">
                                            </asp:GridView>
                                        </div>




                                    </div>

                                    <!-- start footer -->
                                    <!-- end footer -->
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







        </div>

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
