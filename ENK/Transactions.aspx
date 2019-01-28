<%@ Page Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="Transactions.aspx.cs" Inherits="ENK.Transactions" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link type="text/css" rel="stylesheet" href="http://code.jquery.com/ui/1.10.3/themes/smoothness/jquery-ui.css" />
    <script type="text/javascript" src="http://code.jquery.com/jquery-1.8.2.js"></script>
    <script type="text/javascript" src="http://code.jquery.com/ui/1.10.3/jquery-ui.js"></script>
    <script type="text/javascript">
                    

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
        <div id="content">
            <div class="row new-inerpage">
                <div class="col-xs-12 col-sm-5 col-md-5 col-lg-4">
                    <h1 class="page-title txt-color-blueDark">
                        <i class="fa fa-lg fa-fw fa-file-text"></i>
                        Report
							<span>> 
								Transaction Report					</span></h1>
                </div>
                <div class="col-xs-12 col-sm-7 col-md-7 col-lg-8">
                </div>
            </div>


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
                                <h2>Transaction Report</h2>
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
                                    <%--  --%>
                                    <div class="box-input">


                                        <div class="row">
                                            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-2">
                                                <label class="label">From Date<asp:Label ID="Label18" ForeColor="Red" runat="server" Text="*"></asp:Label></label>
                                                <label class="input">
                                                    <i class="icon-append fa fa-calendar"></i>
                                                    <asp:TextBox ID="txtFromDate" runat="server" title="Branch Name" AutoCompleteType="Disabled" class="datepicker" data-dateformat='dd-M-yy'></asp:TextBox>
                                                </label>
                                            </div>

                                            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-2">
                                                <label class="label">To Date<asp:Label ID="Label2" ForeColor="Red" AutoCompleteType="Disabled" runat="server" Text="*"></asp:Label></label>
                                                <label class="input">
                                                    <i class="icon-append fa fa-calendar"></i>
                                                    <asp:TextBox ID="txtToDate" runat="server" title="Branch Name" class="datepicker" data-dateformat='dd-M-yy'></asp:TextBox>
                                                </label>

                                            </div>
                                            <div class="col-xs-12 col-sm-5 col-md-12 col-lg-3">
                                                <label class="label">&nbsp;</label>
                                                <asp:Button ID="btnGet" runat="server" class="btn btn-primary btn-sm" Text="Get Report" OnClick="btnGet_Click" />
                                                <asp:Button ID="btnExportToExcel" runat="server" class="btn btn-primary btn-sm" Text="Export to Excel" OnClick="btnExportToExcel_Click" />

                                            </div>

                                        </div>

                                        <div class="row">

                                            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                                <div class="table-responsive" style="max-height: 280px; overflow-y: auto;">

                                                    <asp:Repeater ID="RepeaterData" runat="server">
                                                        <HeaderTemplate>
                                                            <table class="table table-bordered" id="dataTables-example">
                                                                <thead>
                                                                    <tr>
                                                                        <th>ActivationID</th>
                                                                        <th>Txn type</th>
                                                                        <th>TransactionID</th>
                                                                        <th>MSISDN</th>
                                                                        <th>ICCID</th>
                                                                        <th>Activation Date</th>
                                                                        <th>Bundle Month</th>
                                                                        <th>DistributorID</th>
                                                                        <th>Bundle</th>
                                                                        <th>BundleCount</th>
                                                                    </tr>
                                                                    <tbody>
                                                        </HeaderTemplate>

                                                        <ItemTemplate>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label runat="server" ID="Label1" Text='<%# Eval("activationid") %>' />
                                                                </td>
                                                                <td>
                                                                    <asp:Label runat="server" ID="Label3" Text='<%# Eval("TxnType") %>' />
                                                                </td>
                                                                <td>
                                                                    <asp:Label runat="server" ID="Label4" Text='<%# Eval("transactionid") %>' />
                                                                </td>
                                                                <td>
                                                                    <asp:Label runat="server" ID="Label5" Text='<%# Eval("MSISDN") %>' />
                                                                </td>
                                                                <td>
                                                                    <asp:Label runat="server" ID="Label6" Text='<%# Eval("iccid") %>' />
                                                                </td>
                                                                <td>
                                                                    <asp:Label runat="server" ID="Label11" Text='<%# Eval("bundlemonth") %>' />
                                                                </td>
                                                                <td>
                                                                    <asp:Label runat="server" ID="Label7" Text='<%# Eval("activationdate") %>' />
                                                                </td>
                                                                <td>
                                                                    <asp:Label runat="server" ID="Label8" Text='<%# Eval("distributorid") %>' />
                                                                </td>
                                                                <td>
                                                                    <asp:Label runat="server" ID="Label9" Text='<%# Eval("bundle") %>' />
                                                                </td>
                                                                <td>
                                                                    <asp:Label runat="server" ID="Label10" Text='<%# Eval("bundlecount") %>' />
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
                                </div>
                            </div>
                        </div>
                    </article>

                </div>
            </section>
        </div>
    </div>
</asp:Content>
