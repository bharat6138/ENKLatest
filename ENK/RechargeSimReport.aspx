<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="RechargeSimReport.aspx.cs" Inherits="ENK.RechargeSimReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
  <link type="text/css" rel="stylesheet" href="http://code.jquery.com/ui/1.10.3/themes/smoothness/jquery-ui.css" />
        <link type="text/css" rel="stylesheet" href="https://cdn.datatables.net/1.10.19/css/jquery.dataTables.min.css" />
    <link type="text/css" rel="stylesheet" href="https://cdn.datatables.net/fixedheader/3.1.5/css/fixedHeader.dataTables.min.css" />
    <%--<script type="text/javascript" src="http://code.jquery.com/jquery-1.8.2.js"></script>--%>
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
        .jarviswidget {
            margin: 0 !important;
        }
        .smart-form section {
            margin-bottom:  0 !important;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- MAIN PANEL -->
    <div id="main" role="main" style="margin-top: 0px!important;">
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </asp:ToolkitScriptManager>
        <!-- RIBBON -->

        <!-- END RIBBON -->

        <!-- MAIN CONTENT -->
        <div id="content">

           

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
                          

                            <!-- widget div-->
                            <div>
                                <!-- widget edit box -->
                                <div class="jarviswidget-editbox">
                                    <!-- This area used as dropdown edit box -->
                                </div>
                                <!-- end widget edit box -->

                                <!-- widget content -->
                                <div id="login-form" class="smart-form client-form">
                                    <main class="cd-main-content">
                                    <div class="cd-tab-filter-wrapper">
                                        <div class="cd-tab-filter">
                                            <ul class="cd-filters">
                                               
                                                <li class="filter"><a href="#0" >
                                                    <h2 class="titl"><strong>Recharge SIM Report</strong></h2>
                                                </a></li>

                                             
                                            </ul>
                                            <!-- cd-filters -->
                                        </div>
                                        <!-- cd-tab-filter -->
                                    </div>
                                    <!-- cd-tab-filter-wrapper -->

                                    <section class="cd-gallery">
                                        <div class="table-responsive" >
                                                    <asp:GridView ID="grdTransfer" runat="server" ClientIDMode="Static" class="table table-bordered" OnPreRender="GridView_PreRender">  </asp:GridView>

                                                    <asp:Repeater ID="RepeaterTransfer" runat="server">
                                                        <HeaderTemplate>
                                                            <table class="table table-bordered" id="dataTables-example" style="display:none;">
                                                                <thead>
                                                                    <tr>
                                                                        <th>TxnID</th>
                                                                        <th>Channel Reference</th>
                                                                        <th>MSISDN</th>
                                                                        <th>Top Amount</th>
                                                                        <th>No Of Month</th>
                                                                        <th>Bundle</th>
                                                                        <th>Bundle Amount</th>
                                                                        <th>Requested DateTime</th>
                                                                        <th>Processed DateTime</th>
                                                                    </tr>
                                                                    <tbody>
                                                        </HeaderTemplate>


                                                        <ItemTemplate>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label runat="server" ID="Label81" Text='<%# Eval("transactionid") %>' />
                                                                </td>
                                                                <td>
                                                                    <asp:Label runat="server" ID="Label161" Text='<%# Eval("ChannelReference") %>' />
                                                                </td>
                                                                <td>
                                                                    <asp:Label runat="server" ID="Label91" Text='<%# Eval("MSISDN") %>' />
                                                                </td>
                                                                <td>
                                                                    <asp:Label runat="server" ID="Label191" Text='<%# Eval("TopAmount") %>' />
                                                                </td>
                                                                <td>
                                                                    <asp:Label runat="server" ID="Label201" Text='<%# Eval("NoOfMonth") %>' />
                                                                </td>
                                                                <td>
                                                                    <asp:Label runat="server" ID="Label71" Text='<%# Eval("Bundle") %>' />
                                                                </td>
                                                                <td>
                                                                    <asp:Label runat="server" ID="Label31" Text='<%# Eval("BundleAmount") %>' />
                                                                </td>
                                                                <td>
                                                                    <asp:Label runat="server" ID="Label41" Text='<%# Eval("RequestedDateTime") %>' />
                                                                </td>
                                                                <td>
                                                                    <asp:Label runat="server" ID="Label61" Text='<%# Eval("ProcessedDateTime") %>' />
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>

                                                        <FooterTemplate>
                                                            </thead> 
                                               </table>
                                               </tbody>
                                               
                                                        </FooterTemplate>
                                                    </asp:Repeater>

                                                    <asp:Repeater ID="RepeaterTransfer1" runat="server">
                                                        <HeaderTemplate>
                                                            <table class="table table-bordered" id="dataTables-example" style="display:none;">
                                                                <thead>
                                                                    <tr>
                                                                            <th>Recharge Status</th>
                                                                            <th>Rechage By</th>
                                                                            <th>PlanCode</th>
                                                                            <th>Plan</th>
                                                                            <th>Plan Amount</th>
                                                                            <th>NetWork</th>
                                                                            <th>Distributor</th>
                                                                            <th>Mobile Number</th>
                                                                            <th>Amount</th>
                                                                            <th>Tax</th>
                                                                            <th>W/Tax</th>
                                                                            <th>State</th>
                                                                            <th>ZipCode</th>
                                                                            <th>TxnID</th>
                                                                            <th>Pin Number</th>
                                                                            <th>Recharge Date</th>
                                                                            <th>Regulatory</th>
                                                                      
                                                                              </tr>
                                                                    <tbody>
                                                        </HeaderTemplate>


                                                        <ItemTemplate>
                                                            <tr>
                                                                    <td>
                                                                        <asp:Label runat="server" ID="Label8" Text='<%# Eval("RechargeStatus") %>' />
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label runat="server" ID="Label16" Text='<%# Eval("RechargeVia") %>' />
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label runat="server" ID="Label9" Text='<%# Eval("TariffCode") %>' />
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label runat="server" ID="Label19" Text='<%# Eval("TariffPlan") %>' />
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label runat="server" ID="Label20" Text='<%# Eval("PlanAmount") %>' />
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label runat="server" ID="Label7" Text='<%# Eval("VendorName") %>' />
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label runat="server" ID="Label3" Text='<%# Eval("distributor") %>' />
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label runat="server" ID="Label4" Text='<%# Eval("SimNumber") %>' />
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label runat="server" ID="Label6" Text='<%# Eval("Amount") %>' />
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label runat="server" ID="Label14" Text='<%# Eval("ChargeAmount") %>' />
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label runat="server" ID="Label15" Text='<%# Eval("TotalAmount") %>' />
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label runat="server" ID="Label10" Text='<%# Eval("City") %>' />
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label runat="server" ID="Label11" Text='<%# Eval("ZipCode") %>' />
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label runat="server" ID="Label12" Text='<%# Eval("TxnId") %>' />
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label runat="server" ID="Label5" Text='<%# Eval("PinNumber") %>' />
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label runat="server" ID="Label13" Text='<%# Eval("RechargeDate") %>' />
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label runat="server" ID="Label17" Text='<%# Eval("Regulatery") %>' />
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
                                     
                                    </section>
                                    <!-- cd-gallery -->

                                    <div class="cd-filter">
                                     
                                    

                                            <div class="cd-filter-block col-md-4 ">
                                                <div class="cd-filter-content">
                                                      <%--<label class="label">LEVEL 1</label>--%>
                                                    <asp:Label ID="LEVEL1" runat="server" class="label" Text="LEVEL 1"></asp:Label>
                                                        <label class="input">
                                                            <asp:DropDownList ID="ddlLevel1" class="chosen-select text-area" runat="server" OnSelectedIndexChanged="ddlLevel1_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                        </label>
                                                </div>
                                            </div>
                                        
                                          <div class="cd-filter-block col-md-4 ">
                                                <div class="cd-filter-content">
                                               <%-- <label class="label">LEVEL 2</label>--%>
                                                    <asp:Label ID="LEVEL2" runat="server" class="label" Text="LEVEL 2"></asp:Label>
                                                <label class="input">
                                                    <asp:DropDownList ID="ddlLevel2" runat="server" class="chosen-select text-area" OnSelectedIndexChanged="ddlLevel2_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                </label>
                                            </div></div>

                                           <div class="cd-filter-block col-md-4 ">
                                                <div class="cd-filter-content">
                                              <%--  <label class="label">LEVEL 3</label>--%>
                                                    <asp:Label ID="LEVEL3" runat="server" class="label" Text="LEVEL 3"></asp:Label>
                                                <label class="input">
                                                    <asp:DropDownList ID="ddlLevel3" runat="server" class="chosen-select text-area" OnSelectedIndexChanged="ddlLevel3_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                </label>
                                            </div> </div>
                                            
                                          
                                            <div class="cd-filter-block col-md-4 ">
                                                <div class="cd-filter-content">
                                                <%--<label class="label">LEVEL 4</label>--%>
                                                    <asp:Label ID="LEVEL4" runat="server" class="label" Text="LEVEL 4"></asp:Label>
                                                <label class="input">
                                                    <asp:DropDownList ID="ddlLevel4" runat="server" class="chosen-select text-area" OnSelectedIndexChanged="ddlLevel4_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                </label>

                                            </div> </div>

                                          
                                            <div class="cd-filter-block col-md-4 ">
                                                <div class="cd-filter-content">
                                                <%--<label class="label">LEVEL 5</label>--%>
                                                     <asp:Label ID="LEVEL5" runat="server" class="label" Text="LEVEL 5"></asp:Label>
                                                <label class="input">
                                                    <asp:DropDownList ID="ddlLevel5" runat="server" class="chosen-select text-area" OnSelectedIndexChanged="ddlLevel5_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                </label>

                                            </div> </div>

                                                <div class="cd-filter-block col-md-4 ">
                                                <div class="cd-filter-content">
                                                <label class="label">Network</label>
                                                <label class="input">

                                                    <asp:DropDownList class="chosen-select text-area" ID="ddlNetwork" runat="server">
                                                    </asp:DropDownList>

                                                </label>
                                            </div> </div>
                                              <div class="clearfix"></div>
                                         <hr /><br />
                                        <div class="row">
                                            <div class="cd-filter-block col-md-6 ">
                                                <div class="cd-filter-content">
                                                <label class="label">From Date<asp:Label ID="Label18" ForeColor="Red" runat="server" Text="*"></asp:Label></label>
                                                <label class="input">
                                                    <i class="icon-append fa fa-calendar"></i>
                                                    <asp:TextBox ID="txtFromDate" runat="server" title="Branch Name" class="datepicker" data-dateformat='dd-M-yy'></asp:TextBox>
                                                </label>
                                            </div> </div>
                                           
                                            <div class="cd-filter-block col-md-6 ">
                                                <div class="cd-filter-content">
                                                <label class="label">To Date<asp:Label ID="Label2" ForeColor="Red" runat="server" Text="*"></asp:Label></label>
                                                <label class="input">
                                                    <i class="icon-append fa fa-calendar"></i>
                                                    <asp:TextBox ID="txtToDate" runat="server" class="datepicker" data-dateformat='dd-M-yy' title="Branch Name"></asp:TextBox>
                                                </label>
                                               
                                            </div> </div>
                                            </div>
                                         
                                            <div class="cd-filter-block col-md-12">
                                                <div class="cd-filter-content pull-right">


                                                <asp:Button ID="btnGet" runat="server" class="btn btn-primary btn-sm" Text="Get Report" OnClick="btnGet_Click" OnClientClick="javascript:return ShowProgress();" />
                                                <asp:Button ID="btnExportToExcel" runat="server" Text="Export To Excel" OnClick="btnExportToExcel_Click" class="btn btn-primary btn-sm" />
                                            </div> </div>

                                        <a href="#0" class="cd-close">Close</a>
                                    </div>
                                    <!-- cd-filter -->

                                    <a href="#0" class="cd-filter-trigger">Filters</a>
                                </main>











                                    

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
   
    <!-- PAGE RELATED PLUGIN(S) -->
    <script src="js/plugin/jquery-form/jquery-form.min.js"></script>
         <script src="https://cdn.datatables.net/1.10.19/js/jquery.dataTables.min.js"></script>
    <script>var $j = jQuery.noConflict(true);</script>
  
   
    <script>
         $j(document).ready(function() {
            $j('#grdTransfer').DataTable( {
                "scrollY": 340,
                "scrollX": true,
                "searching": true,
                "paging": false,
                "info": false,
                 "autoWidth": true
    } );
        } );

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
            $j(selector).chosen(config[selector]);
        }
    </script>

</asp:Content>

