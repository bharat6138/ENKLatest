<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MainMaster.Master" CodeBehind="PaymentDetail.aspx.cs" Inherits="ENK.PaymentDetail" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link type="text/css" rel="stylesheet" href="http://code.jquery.com/ui/1.10.3/themes/smoothness/jquery-ui.css" />
    <script type="text/javascript" src="http://code.jquery.com/jquery-1.8.2.js"></script>
    <script type="text/javascript" src="http://code.jquery.com/ui/1.10.3/jquery-ui.js"></script>


    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" />
    <%--<script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>--%>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>

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

    <script type="text/javascript">

        function validate() {
            if (document.getElementById('<%=ddlDistributor.ClientID%>').selectedIndex == 0) {
                alert('Please Select Distributor');
                document.getElementById("<%=ddlDistributor.ClientID%>").focus();
                return false;
            }
            return true;
        }
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
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </asp:ToolkitScriptManager>
        <!-- RIBBON -->

        <!-- END RIBBON -->

        <!-- MAIN CONTENT -->
        <div id="content">
            <div class="row new-inerpage">
                <div class="col-xs-12 col-sm-5 col-md-5 col-lg-4">
                    <h1 class="page-title txt-color-blueDark">
                        <i class="fa fa-lg fa-fw fa-file-text"></i>
                        Report
							<span>> 
								Topup Report					</span></h1>
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
                                <h2>Topup Report</h2>
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
                                                <label class="label">Distributor</label>
                                                <label class="input">
                                                    <asp:DropDownList class="chosen-select text-area" ID="ddlDistributor" runat="server" AutoPostBack="false">
                                                        <%--<asp:ListItem Value="1" Text="-Select-"></asp:ListItem>
                                            <asp:ListItem Value="1" Text="Mobile SIM"></asp:ListItem>
                                            <asp:ListItem Value="2" Text="Blank SIM"></asp:ListItem>--%>
                                                    </asp:DropDownList>
                                                </label>
                                            </div>

                                            <div id="divFrmDate" runat="server" visible="true" class="col-xs-12 col-sm-12 col-md-12 col-lg-3">
                                                <label class="label">From Date<asp:Label ID="Label18" ForeColor="Red" runat="server" Text="*"></asp:Label></label>
                                                <label class="input">
                                                    <asp:TextBox ID="txtFromDate" runat="server" title="Branch Name"></asp:TextBox>
                                                </label>
                                                <asp:ImageButton ID="ImageButton1" CausesValidation="false" ImageUrl="~/img/Calendar.png" runat="server" class="img-responsive text-right" align="right" Style="margin-top: 0px;" />
                                                <asp:CalendarExtender ID="CalendarExtender1" PopupButtonID="ImageButton1" runat="server" TargetControlID="txtFromDate" Format="dd-MMM-yyyy">
                                                </asp:CalendarExtender>




                                            </div>
                                            <div id="divToDate" runat="server" visible="true" class="col-xs-12 col-sm-12 col-md-12 col-lg-3">
                                                <label class="label">To Date<asp:Label ID="Label2" ForeColor="Red" runat="server" Text="*"></asp:Label></label>
                                                <label class="input">
                                                    <asp:TextBox ID="txtToDate" runat="server" title="Branch Name"></asp:TextBox>
                                                </label>
                                                <asp:ImageButton ID="ImageButton2" CausesValidation="false" ImageUrl="~/img/Calendar.png" runat="server" class="img-responsive text-right" align="right" Style="margin-top: 0px;" />
                                                <asp:CalendarExtender ID="CalendarExtender2" PopupButtonID="ImageButton2" runat="server" TargetControlID="txtToDate" Format="dd-MMM-yyyy">
                                                </asp:CalendarExtender>
                                            </div>

                                            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-2">
                                                <label class="label">&nbsp;</label>
                                                <asp:Button ID="btnGet" class="btn btn-primary" runat="server" Text="Get Report" Style="float: right; height: 31px; margin: 10px 0 0 5px; padding: 0 44px; margin-top: 0px; font: 300 15px/29px 'Open Sans',Helvetica,Arial,sans-serif; cursor: pointer;" OnClick="btnGet_Click" />

                                            </div>

                                        </div>

                                        <div class="row">
                                            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-10"></div>


                                            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-2">
                                                <label class="input">&nbsp; </label>
                                                <asp:Button ID="btnExportToExcel" runat="server" Text="Export To Excel" 
                                                    Style="float: right; height: 31px; margin: 10px 0 0 5px; padding: 0 44px; margin-top: 0px; font: 300 15px/29px 'Open Sans',Helvetica,Arial,sans-serif; cursor: pointer;" 
                                                    OnClick="btnExportToExcel_Click" class="btn btn-primary" />
                                                 <asp:Label runat="server" ID="lblh" Text="" visible="false"/>
                                            </div>

                                        </div>




                                        <div class="row" id="DivMobile" runat="server">
                                            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                                <label class="label">&nbsp;</label>
                                                <%--<asp:GridView ID="gridexcel" runat="server" Style="display:none"></asp:GridView>--%>

                                                <div class="table-responsive" style="max-height: 237px; overflow-y: auto;">

                                                    <asp:Repeater ID="RepeaterTransfer" runat="server" OnItemCommand="RepeaterTransfer_ItemCommand">
                                                        <HeaderTemplate>
                                                            <table class="table table-bordered" id="dataTables-example">
                                                                <thead>
                                                                    <tr>
                                                                        <th>TransactionId</th>
                                                                        <th>Amount</th>
                                                                        <th>Transaction Status</th>
                                                                        <th>Distributor Name</th>
                                                                        <th>Email</th>
                                                                        <th>Payment Date</th>
                                                                        <th>Payment Mode</th>
                                                                        <%-------%>
                                                                         <th>Remark</th>
                                                                          <%-------%>
                                                                        <th>Action</th>

                                                                    </tr>
                                                                    <tbody>
                                                        </HeaderTemplate>

                                                        <ItemTemplate>
                                                            <tr>

                                                                <td>
                                                                    <asp:Label runat="server" ID="Label9" Text='<%# Eval("TxnId") %>' />
                                                                </td>
                                                                <td>
                                                                    <asp:Label runat="server" ID="lblChargedAmount" Text='<%# Eval("ChargedAmount") %>' />
                                                                </td>
                                                                <td>
                                                                    <asp:Label runat="server" ID="lblTransactionStatus" class="statusss" Text='<%# Eval("TransactionStatus") %>' />
                                                                </td>
                                                                <td>
                                                                    <asp:Label runat="server" ID="Label11" Text='<%# Eval("DistributorName") %>' />
                                                                </td>
                                                                  <td>
                                                                    <asp:Label runat="server" ID="Label3" Text='<%# Eval("Email") %>' />
                                                                </td>
                                                                <td>
                                                                    <asp:Label runat="server" ID="Label1" Text='<%# Eval("PaymentDate") %>' />
                                                                </td>
                                                                <td>
                                                                    <asp:Label runat="server" ID="Label7" Text='<%# Eval("Value") %>' />
                                                                </td>
                                                                 <td>
                                                                    <asp:Label runat="server" ID="Label5" Text='<%# Eval("StatusRemarks") %>' />
                                                                </td>
                                                                <td style="display: none;">
                                                                    <asp:LinkButton ID="lnkChangeStatus" CommandName="ChangeStatus"  ToolTip="Change Status" CommandArgument='<%#Eval("paymentId") %>' runat="server" >Change Status</asp:LinkButton>
                                                                </td>
                                                                <td runat="server">
                                                                    <button id="ChangeStatus" type="button" style="display: none" class="btn btn-default stts">Change Status</button>
                                                                </td>
                                                                
                                                                <td>
                                                                    <asp:HiddenField id="hdbget" runat="server" value='<%# Eval("paymentId") %>' />
                                                                  
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
                                    <div>  <asp:HiddenField ID="HiddenField1" runat="server" value=""/>  </div>
                                    <div id="myModal" role="dialog" class="col-xs-12 col-sm-12 col-md-12 col-lg-2" style="margin-top: -40%; margin-left: 15%; display: none;">
                                        <div class="modal-dialog" style="width:450px">
                                            <div class="modal-content">
                                                <div id="dvUpdateStatus">
                                                    <div class="modal-header">
                                                        <h4 class="modal-title">Update Status</h4>
                                                    </div>
                                                    <div class="modal-body">
                                                         <%--add by akash starts--%>
                                                        <label class="label">Remark<asp:Label ID="Label4" ForeColor="Red" runat="server" Text="*"></asp:Label></label>
                                                        <asp:TextBox ID="txtStatusRemark" runat="server" Text="" Style="padding: 5px 0 5px 0px;
    margin-right: 10px;
    border: 1px solid #337ab6;
    border-radius: 5px;"></asp:TextBox>
                                                            <%--add by akash end--%>

                                                        <asp:Button ID="btnFail" runat="server" Text="Fail" OnClick="btnFail_Click" class="btn btn-primary" />
                                                        <asp:Button ID="btnSuccess" runat="server" Text="Success" OnClick="btnSuccess_Click" class="btn btn-primary" />
                                                         

                                                    </div>
                                                    <div class="modal-footer">
                                                        <button id="closebtn" type="button" class="btn btn-default">Close</button>
                                                    </div>
                                                </div>
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
    </div>






    <!-- END MAIN PANEL -->
    <div id="popupdiv" runat="server" class="loading" title="Basic modal dialog" style="display: nonefont-family:Arial; font-size: 10pt; border: 2px solid #67CFF5; text-align: -webkit-center;">

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


    <script type="text/javascript">
        $(document).ready(function () {
            $('#dataTables-example').find("span.statusss").each(function (i) {
                if ($(this).text() == "Pending") {
                    $(this).closest('tr').find('button').css({ 'display': "block" });
                }
            });

            $(".stts").click(function () {
                debugger;
                var item = $(this).closest("tr").find("input[type=hidden]").val();
                var item1 = $("#ContentPlaceHolder1_HiddenField1").val(item);
                //alert($("#ContentPlaceHolder1_HiddenField1").val());
                $("#myModal").show();
            });

            $("#closebtn").click(function () {
                $("#myModal").hide();
            });

        });
    </script>

</asp:Content>

