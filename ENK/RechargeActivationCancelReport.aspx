<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MainMaster.Master" CodeBehind="RechargeActivationCancelReport.aspx.cs" Inherits="ENK.RechargeActivationCancelReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link type="text/css" rel="stylesheet" href="http://code.jquery.com/ui/1.10.3/themes/smoothness/jquery-ui.css" />
    
     <link type="text/css" rel="stylesheet" href="https://cdn.datatables.net/1.10.18/css/jquery.dataTables.min.css" />
    
     <link type="text/css" rel="stylesheet" href="https://cdn.datatables.net/buttons/1.5.2/css/buttons.dataTables.min.css" />

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
        div.dt-buttons {
    position: relative;
    float: left;
    top: 30px;
}
    </style>

</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="main" role="main" style="margin-top: 0px!important;">
        <!-- MAIN CONTENT -->
        <div id="content">

            <div class="row new-inerpage">
                <div class="col-xs-12 col-sm-5 col-md-5 col-lg-4">
                    <h1 class="page-title txt-color-blueDark">
                        <i class="fa fa-lg fa-fw fa-file-text"></i>
                        Report
							<span>> 
								Recharge/Activation Cancel Report					</span></h1>
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
                                <h2>
                                    <span>Recharge					</span>&nbsp;Mobile Report</h2>
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

                                            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-4">
                                                <label>From Date</label>

                            <label class="input">
                                <i class="icon-append fa fa-calendar"></i>
                                <asp:TextBox ID="txtDate" runat="server" AutoComplete="Off" title="Branch Name" class="datepicker" data-dateformat='dd-M-yy'></asp:TextBox>
                            </label>
                                            </div>

                                            
                                            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-4">
                                                <label>To Date</label>

                            <label class="input">
                                <i class="icon-append fa fa-calendar"></i>
                                <asp:TextBox ID="txtToDate" runat="server" AutoComplete="Off" title="Branch Name" class="datepicker" data-dateformat='dd-M-yy'></asp:TextBox>
                            </label>
                                            </div>


                                            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-4" style="padding-top:20px;">
                            <%-- <asp:Button ID="btnGetDetail" runat="server" Text="Get Details" CssClass="btn btn-default"  OnClick="btnGetDetail_Click" OnClientClick="javascript:return validate();" />--%>
                            <asp:Button ID="btnGetReport" runat="server" Text="Get Details" class="btn btn-primary btn-sm" OnClick="btnGetReport_Click" />
                        </div>

                                        </div>

                                        

                                        <div class="row" id="DivMobile" runat="server">
                                            <div class="col-md-12 table-responsive">
                                <asp:Repeater ID="RepeaterCancelReport" runat="server">
                                    <HeaderTemplate>
                                        <table class="table table-bordered" id="example">
                                            <thead>
                                                <tr>
                                                    <th>MSISDN</th>
                                                    <th>Plan Desription</th>
                                                    <th>Rental</th>
                                                    <th>CancelForMonth</th>
                                                    <th>Cancel Recharge/Activation</th>
                                                </tr>
                                                <tbody>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("MSISDN") %>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label2" runat="server" Text='<%# Eval("Description") %>'></asp:Label></td>
                                            <td>
                                                <asp:Label ID="Label3" runat="server" Text='<%# Eval("Rental") %>'></asp:Label></td>
                                            <td>
                                                <asp:Label ID="Label4" runat="server" Text='<%# Eval("CancelPlanForMonth") %>'></asp:Label>

                                            </td>
                                            <td>
                                                <asp:Label ID="Label5" runat="server" Text='<%# Eval("Canceltype") %>'></asp:Label>

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
            <!-- end widget grid -->
        </div>
        <!-- END MAIN CONTENT -->
        <script src="https://code.jquery.com/jquery-3.3.1.js"></script>

    <script src="https://cdn.datatables.net/1.10.18/js/jquery.dataTables.min.js"></script>
        <script src="https://cdn.datatables.net/buttons/1.5.2/js/dataTables.buttons.min.js"></script>
 <script src="https://cdn.datatables.net/buttons/1.5.2/js/buttons.flash.min.js"></script>
 <script src="https://cdnjs.cloudflare.com/ajax/libs/jszip/3.1.3/jszip.min.js"></script>
 <script src="https://cdnjs.cloudflare.com/ajax/libs/pdfmake/0.1.36/pdfmake.min.js"></script>
 <script src="https://cdnjs.cloudflare.com/ajax/libs/pdfmake/0.1.36/vfs_fonts.js"></script>
 <script src="https://cdn.datatables.net/buttons/1.5.2/js/buttons.html5.min.js"></script>
 <script src="https://cdn.datatables.net/buttons/1.5.2/js/buttons.print.min.js"></script>

 <script src="https://cdn.datatables.net/1.10.18/js/dataTables.bootstrap.min.js"></script>
    <script>var $j = jQuery.noConflict(true);</script>
<script>
    $(document).ready(function () {
        console.log($().jquery); // This prints v1.4.2
        console.log($j().jquery); // This prints v1.9.1
    });
  </script>


 
    <script>
        $j(document).ready(function () {
            $j('#example').DataTable({
                dom: 'Bfrtip',
                buttons: [
                    'excel'
                ]
            });
        });

    </script>
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
