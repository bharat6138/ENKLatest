<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MainMaster.Master" CodeBehind="RechargeActivationCancellation.aspx.cs" Inherits="ENK.RechargeActivationCancellation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link type="text/css" rel="stylesheet" href="http://code.jquery.com/ui/1.10.3/themes/smoothness/jquery-ui.css" />
    <script type="text/javascript" src="http://code.jquery.com/jquery-1.8.2.js"></script>
    <script type="text/javascript" src="http://code.jquery.com/ui/1.10.3/jquery-ui.js"></script>

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
    <script>
        function validate() {
            if (document.getElementById("<%=txtMSISDN.ClientID%>").value.length < 10) {
                alert("MSISDN can't be less than 10 digit");
                return false;
            }

            return true;
        }

        
    </script>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="main" role="main" style="margin-top: 0px!important;">
        <div id="content">
            <div class="row row-fullWidth">
                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                    <h1 class="page-title txt-color-blueDark">
                        <i class="fa fa-lg fa-fw fa-pencil-square-o"></i>

                        <span>Recharge/Activation Cancellation
                        </span>
                    </h1>
                    <div class="row">

                        <div class="col-md-3">
                            <label>Select Recharge/Activation</label>
                            <asp:DropDownList ID="ddlMSISDN" class="form-control chosen-select text-area" runat="server" OnSelectedIndexChanged="ddlMSISDN_SelectedIndexChanged" AutoPostBack="true">
                                <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                <asp:ListItem Text="Recharge" Value="1"></asp:ListItem>
                                <asp:ListItem Text="Activation" Value="2"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-3">
                            <label>MSISDN</label>

                            <asp:TextBox ID="txtMSISDN" runat="server" CssClass="form-control" MaxLength="11"></asp:TextBox>
                        </div>
                        <div id="divSerialNumber" runat="server" visible="false">
                            <div class="col-md-1" style="padding-top: 23px;">
                                <label><b>OR</b></label>
                            </div>
                            <div class="col-md-3">

                                <label>Sim Card</label>

                                <asp:TextBox ID="txtSerialNumber" runat="server" CssClass="form-control" MaxLength="19"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-2" style="padding-top: 23px;">
                            <%-- <asp:Button ID="btnGetDetail" runat="server" Text="Get Details" CssClass="btn btn-default"  OnClick="btnGetDetail_Click" OnClientClick="javascript:return validate();" />--%>
                            <asp:Button ID="btnGetDetail" runat="server" Text="Get Details" CssClass="btn btn-default" OnClick="btnGetDetail_Click" />
                        </div>
                        <br />
                        <br />
                        <div class="col-md-12 table-responsive">
                            <asp:GridView ID="grdMSISDN" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered">
                                <Columns>
                                    <asp:TemplateField HeaderText="MSISDN">
                                        <ItemTemplate>
                                            <asp:Label ID="lblMSISDN" runat="server" Text='<%# Eval("MSISDN") %>'></asp:Label>

                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Recharge/Activation DateTime">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDate" runat="server" Text='<%# Eval("Date") %>'></asp:Label>

                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Response">
                                        <ItemTemplate>
                                            <asp:Label ID="lblResponse" runat="server" Text='<%# Eval("Response") %>'></asp:Label>

                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Plan">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPlan" runat="server" Text='<%# Eval("Description") %>'></asp:Label>

                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Month">
                                        <ItemTemplate>
                                            <asp:Label ID="lblvalidity" runat="server" Text='<%# Eval("ValMonth") %>'></asp:Label>

                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Cancel For Month">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtcancelMonth" runat="server" Text='<%# Eval("ValMonth") %>'></asp:TextBox>

                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Action">
                                        <ItemTemplate>
                                            <asp:HiddenField ID="hddnID" runat="server" Value='<%# Eval("ID") %>' />
                                            <asp:HiddenField ID="hddnMSISDN" runat="server" Value='<%# Eval("MSISDN") %>' />
                                            <asp:HiddenField ID="hddnTariffID" runat="server" Value='<%# Eval("TariffID") %>' />
                                            <asp:HiddenField ID="hddnTariffCode" runat="server" Value='<%# Eval("TariffCode") %>' />
                                            <asp:HiddenField ID="hddnTariffDesc" runat="server" Value='<%# Eval("Description") %>' />
                                            <asp:HiddenField ID="hddnRental" runat="server" Value='<%# Eval("Rental") %>' />
                                            <asp:HiddenField ID="hddnRegFee" runat="server" Value='<%# Eval("RegulatoryFee") %>' />
                                            <asp:HiddenField ID="hddnmonth" runat="server" Value='<%# Eval("ValMonth") %>' />
                                            <asp:LinkButton ID="lnkCancel" runat="server" OnClick="lnkCancel_Click" OnClientClick="javascript : return confirm('Are you sure to continue?') ;">Cancel & Refund</asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
        </div>

    </div>
    <script src="js/jquery-2.1.1.js"></script>
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
    <script>
        function CheckContinue() {
            var text;
            var res = confirm("Are you sure to continue?");
            if (res == true) {
                //   validateControlsActivate();
                if (CheckMonth() == true) {
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
