<%@ Page Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="CommissionReport.aspx.cs" Inherits="ENK.CommissionReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link type="text/css" rel="stylesheet" href="http://code.jquery.com/ui/1.10.3/themes/smoothness/jquery-ui.css" />
    <script type="text/javascript" src="http://code.jquery.com/jquery-1.8.2.js"></script>
    <script type="text/javascript" src="http://code.jquery.com/ui/1.10.3/jquery-ui.js"></script>
    <style>
        .ui-datepicker-calendar {
            display: none !important;
        }

        .ui-datepicker select.ui-datepicker-month, .ui-datepicker select.ui-datepicker-year {
            width: 38% !important;
            font-size: 10px;
        }

        .ui-state-hover, .ui-widget-content .ui-state-hover, .ui-widget-header .ui-state-hover, .ui-state-focus, .ui-widget-content .ui-state-focus, .ui-widget-header .ui-state-focus {
            border: 0px solid #999999 !important;
            background: none !important;
            font-weight: bold;
            color: #212121;
        }

        .ui-datepicker .ui-datepicker-prev span {
            display: block;
            position: absolute;
            left: 21%;
            margin-left: -8px;
            top: 50%;
            width: 50px;
            margin-top: -8px;
            cursor: pointer;
        }

        .ui-datepicker .ui-datepicker-next span {
            display: block;
            position: absolute;
            left: -35% !important;
            margin-left: -14px;
            top: 50%;
            width: 50px;
            margin-top: -8px;
            cursor: pointer;
        }
    </style>
    <div id="main" role="main" style="margin-top: 0px!important;">

        <!-- RIBBON -->

        <!-- END RIBBON -->

        <!-- MAIN CONTENT -->
        <div id="content">
            <div class="row row-fullWidth">
                <div class="col-xs-5 col-sm-7 col-md-7 col-lg-4">
                    <h1 class="page-title txt-color-blueDark">
                        <i class="fa fa-home"></i>
                        Dashboard
							<span>> 
								Commission Report
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

                        <div class="jarviswidget jarviswidget-color-blueDark" id="Div1" data-widget-colorbutton="false" data-widget-editbutton="false" data-widget-togglebutton="false" data-widget-deletebutton="false" data-widget-fullscreenbutton="false" data-widget-custombutton="false" data-widget-collapsed="false" data-widget-sortable="false">
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
                                <h2>Manual Commission Posted For Month
                                    <asp:Label runat="server" ID="lblMonth" Text="" />
                                    - <strong>
                                        <asp:Label runat="server" ID="lblAmount" Text="0" /></strong> </h2>

                            </header>

                            <!-- widget div-->
                            <div>

                                <!-- widget edit box -->
                                <div class="jarviswidget-editbox">
                                    <!-- This area used as dropdown edit box -->

                                </div>
                                <!-- end widget edit box -->

                                <!-- widget content -->
                                <div class="smart-form client-form">

                                    <div class="box-input">
                                        <div class="row">

                                            <div class="col-md-3">

                                                <label class="label"><b>Month</b></label>
                                                <label class="input">
                                                    <i class="icon-append fa fa-calendar"></i>
                                                    <asp:TextBox ID="txtMonthYr" runat="server"  title="Month Year" class="datepicker" data-dateformat='MM/YY' ></asp:TextBox>
                                                </label>
                                            </div>

                                            <div class="col-md-2" style="margin-top: 24px;">

                                                <asp:Button ID="btnSearch" runat="server" Text="Search" class="col-md-9 btn btn-primary btn-sm" OnClick="btnSearch_Click" />

                                            </div>
                                            <div class="col-md-2" style="margin-top: 24px;">

                                                <asp:Button ID="btnExportToExcel" runat="server" Text="Export To Excel" OnClick="btnExportToExcel_Click" class="col-md-9 btn btn-primary btn-sm" />

                                            </div>

                                        </div>



                                        <div class="table-responsive">
                                            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12" style="max-height: 300px; overflow-y: auto;">
                                                <asp:GridView ID="grdCommission" runat="server" class="table table-bordered"></asp:GridView>

                                                <asp:Repeater ID="rptCommission" runat="server">
                                                    <HeaderTemplate>
                                                        <table class="table table-bordered" id="dataTables-example" style="display: none;">
                                                            <thead>
                                                                <tr>

                                                                    <th>MSSIDN</th>
                                                                    <th>Sim Number</th>
                                                                    <th>Tariff Code</th>
                                                                    <th>Commission Amount</th>
                                                                    <th>Remarks</th>
                                                                    <th>Month</th>
                                                                    <th>Distributor Level</th>
                                                                    <%--<th>Year</th>--%>
                                                                </tr>
                                                                <tbody>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <tr>


                                                            <td>
                                                                <asp:Label runat="server" ID="Label1" Text='<%# Eval("MSSIDN") %>' />
                                                            </td>
                                                            <td>
                                                                <asp:Label runat="server" ID="Label2" Text='<%# Eval("SimNo") %>' />
                                                            </td>
                                                            <td>
                                                                <asp:Label runat="server" ID="Label6" Text='<%# Eval("TariffCode") %>' />
                                                            </td>
                                                            <td>
                                                                <asp:Label runat="server" ID="Label8" Text='<%# Eval("CommissionAmount") %>' />
                                                            </td>
                                                            <td>
                                                                <asp:Label runat="server" ID="Label3" Text='<%# Eval("Remarks") %>' />

                                                            </td>
                                                            <td>
                                                                <asp:Label runat="server" ID="Label4" Text='<%# Eval("commissionprocessmonth") %>' />

                                                            </td>
                                                            <td>
                                                                <asp:Label runat="server" ID="Label5" Text='<%# Eval("DistributorLevel") %>' />
                                                            </td>
                                                            <%--<td>
                                                            <asp:Label runat="server" ID="Label5" Text='<%# Eval("Year") %>' />

                                                        </td>--%>
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
            <script src="js/jquery-2.1.1.js"></script>
            <script src="js/plugin/chosen/chosen.jquery.js"></script>

            <!-- PAGE RELATED PLUGIN(S) -->
            <script src="js/plugin/jquery-form/jquery-form.min.js"></script>
            <script>
                $(function () {
                    $('.datepicker').datepicker({
                        changeMonth: true,
                        changeYear: true,
                        showButtonPanel: true,
                        dateFormat: 'mm/yy',
                        onClose: function (dateText, inst) {
                            $(this).datepicker('setDate', new Date(inst.selectedYear, inst.selectedMonth, 1));
                        }
                    });
                });
            </script>
            <script>
                $('datepicker').keydown(function (e) {
                    alert("ABC");
                    e.preventDefault();
                    return false;
                });
            </script>
            <!-- end widget grid -->
        </div>
        <!-- END MAIN CONTENT -->
    </div>
</asp:Content>
