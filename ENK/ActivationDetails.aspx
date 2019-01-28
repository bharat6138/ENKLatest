<%@ Page Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="ActivationDetails.aspx.cs" Inherits="ENK.ActivationDetails" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" media="screen" href="css/ezeeway-production.min.css" />
    <link type="text/css" rel="stylesheet" href="http://code.jquery.com/ui/1.10.3/themes/smoothness/jquery-ui.css" />
    <script type="text/javascript" src="http://code.jquery.com/jquery-1.8.2.js"></script>
    <script type="text/javascript" src="http://code.jquery.com/ui/1.10.3/jquery-ui.js"></script>
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
                background: "#0F0101 url(images/ui-bg_flat_0_aaaaaa_40x100.png) repeat-x",
                opacity: .3
                /*background: "#454545",
                opacity: ".9 !important",
                filter: "Alpha(Opacity=50)"*/
            });
            return true;
        };

        function HideProgress() {
            setTimeout(function () {
                var modal = $('<div />');
                modal.addClass("modal");
                $('body').append(modal);
                var loading = $(".loading");

                loading.hide();
                var top = Math.max($(window).height() / 2 - loading[0].offsetHeight / 2, 0);
                var left = Math.max($(window).width() / 2 - loading[0].offsetWidth / 2, 0);
                loading.css({ top: top, left: left });
            }, 200);
        }
        //$('form').live("submit", function () {
        //    ShowProgress();
        //});



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
    <div id="main" role="main" style="margin-top: 0px!important;">

        <!-- RIBBON -->

        <!-- END RIBBON -->

        <!-- MAIN CONTENT -->
        <div id="content">
            <div class="row row-fullWidth">
                <div class="col-xs-5 col-sm-7 col-md-7 col-lg-4">
                    <h1 class="page-title txt-color-blueDark">
                        <i class="fa fa-home fa-fw"></i>
                        Dashboard
							<span>> 
								Activation Detail
                            </span>
                    </h1>
                </div>
                <div class="col-xs-7 col-sm-5 col-md-5 col-lg-8">
                    <ul id="sparks" class="">
                        <li class="sparks-info">
                            <a href="Dashboard.aspx" title="Back">
                                <span><i class="fa fa-home"></i></span>back
                            </a>
                        </li>
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
                                <h2>Activation Detail
                                </h2>
                            </header>

                            <!-- widget div-->
                            <div>

                                <!-- widget edit box -->
                                <div class="jarviswidget-editbox">
                                    <!-- This area used as dropdown edit box -->

                                </div>
                                <!-- end widget edit box -->
                                 <div id="login-form" class="smart-form client-form">

                                    <div class="box-input">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <strong class="badge badge-danger" style="font-size: 21px; background-color: #41b27c !important; border-radius: 100px !important; padding: 5px 16px !important;">
                                                <asp:Label runat="server" ID="lblMonth" Text="" />
                                                <asp:Label runat="server" ID="lblNumber" Text="" /></strong>
                                        </div>
                                    </div>
                                    
                                    <div class="row">

                                        <div class="col-md-3" >
                                            <label class="label">Month:</label>
                                            <label class="input">

                                                <asp:DropDownList ID="ddlMonth" class="form-control chosen-select text-area" runat="server">
                                                    <asp:ListItem Text="---Select Month---" Value="0"></asp:ListItem>
                                                    <asp:ListItem Text="Sep-2018" Value="9"></asp:ListItem>
                                                    <asp:ListItem Text="Aug-2018" Value="8"></asp:ListItem>
                                                    <asp:ListItem Text="Jul-2018" Value="7"></asp:ListItem>
                                                    <asp:ListItem Text="Jun-2018" Value="6"></asp:ListItem>
                                                    <asp:ListItem Text="May-2018" Value="5"></asp:ListItem>
                                                    <asp:ListItem Text="APR-2018" Value="4"></asp:ListItem>
                                                    <asp:ListItem Text="OCT-2018" Value="10"></asp:ListItem>
                                                    <asp:ListItem Text="Nov-2018" Value="11"></asp:ListItem>
                                                    <asp:ListItem Text="Dec-2018" Value="12"></asp:ListItem>
                                                </asp:DropDownList>
                                                
                                            </label>
                                        </div>


                                        <div class="col-md-4">
                                            <label class="label">From Date<asp:Label ID="Label18" ForeColor="Red" runat="server" Text="*"></asp:Label></label>
                                            <label class="input">
                                                <i class="icon-append fa fa-calendar"></i>
                                                <asp:TextBox ID="txtFromDate" runat="server" title="Branch Name" class="datepicker" data-dateformat='mm/d/yy'></asp:TextBox>
                                            </label>
                                        </div>
                                        <div class="col-md-3">
                                            <label class="label">To Date<asp:Label ID="Label2" ForeColor="Red" runat="server" Text="*"></asp:Label></label>
                                            <label class="input">
                                                <i class="icon-append fa fa-calendar"></i>
                                                <asp:TextBox ID="txtToDate" runat="server" class="datepicker" data-dateformat='mm/d/yy' title="Branch Name"></asp:TextBox>
                                            </label>

                                        </div>
                                        <div class="col-md-2">
                                            <label class="input">
                                                <asp:Button ID="btnSearch" runat="server" Text="Search" style="margin-top:23px" class="btn btn-primary" OnClick="btnSearch_Click" />
                                             
                                            </label>
                                        </div>


                                    </div>
                                    <div class="table-responsive" style="max-height: 300px; overflow-y: auto;">
                                        <asp:Repeater ID="rptActivation" runat="server">
                                            <HeaderTemplate>
                                                <table class="table table-bordered" id="dataTables-example">
                                                    <thead>
                                                        <tr>
                                                            <th>Mobile No</th>
                                                            <th>SIM Serial No</th>
                                                            <th>Tariff Code</th>
                                                            <th>Distributor</th>
                                                            <th>Activation Date</th>


                                                        </tr>
                                                        <tbody>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td>
                                                        <asp:Label runat="server" ID="Label1" Text='<%# Eval("MSISDN") %>' />
                                                    </td>
                                                    <td>
                                                        <asp:Label runat="server" ID="Label7" Text='<%# Eval("SIMSerialNumber") %>' />
                                                    </td>
                                                    <td>
                                                        <asp:Label runat="server" ID="Label4" Text='<%# Eval("TariffCode") %>' />
                                                    </td>
                                                    <td>
                                                        <asp:Label runat="server" ID="Label3" Text='<%# Eval("Distributor") %>' />
                                                    </td>
                                                    <td>
                                                        <asp:Label runat="server" ID="Label2" Text='<%# Eval("ActivationDate") %>' />
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
                                <!-- end widget content -->
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
    </div>
    
    <script src="js/jquery-2.1.1.js"></script>
    <script src="js/plugin/chosen/chosen.jquery.js"></script>

    <!-- PAGE RELATED PLUGIN(S) -->
    <script src="js/plugin/jquery-form/jquery-form.min.js"></script>
    
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
