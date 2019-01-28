<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="IpMapping.aspx.cs" Inherits="ENK.IpMapping" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link type="text/css" rel="stylesheet" href="http://code.jquery.com/ui/1.10.3/themes/smoothness/jquery-ui.css" />
    <script type="text/javascript" src="http://code.jquery.com/jquery-1.8.2.js"></script>
    <script type="text/javascript" src="http://code.jquery.com/ui/1.10.3/jquery-ui.js"></script>

</asp:Content>

<asp:Content ID="content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="main" role="main" style="margin-top: 0px!important;">

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
                            <header>
                                <span class="widget-icon"><i class="fa fa-table"></i></span>
                                <h2>SELLER IP MAPPING</h2>
                            </header>
                            <div>
                                <div class="box-input">
                                    <div class="row">
                                        <div class="col-md-4 form-group">
                                            <asp:CheckBox ID="chkMultipleIP" runat="server" Visible="false" />
                                            <div id="divMultIP" runat="server">
                                                <label>MAP IP ADDRESS</label>
                                                <asp:TextBox ID="txtMultiIPAdress" runat="server" CssClass="form-control"  Style="width: 74%;"></asp:TextBox>
                                                <br />
                                                 <div class="table-responsive">
                                                <asp:GridView ID="grdIPAddlist" runat="server" AutoGenerateColumns="false" Visible="true" CssClass="table table-bordered" OnRowDeleting="grdIPAddlist_RowDeleting">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="IPAddress">
                                                            <ItemTemplate>
                                                                 <asp:Label ID="lblID" runat="server" Text='<%# Bind("ID") %>' Visible="false"></asp:Label>
                                                                <asp:Label ID="lblIpadddr" runat="server" Text='<%# Bind("IP") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Action">
                                                            <ItemTemplate>
                                                                <%-- <asp:LinkButton ID="lbldelete" runat="server">Delete</asp:LinkButton>--%>
                                                                <asp:LinkButton ID="lnkdelete" runat="server" CommandName="Delete">Delete</asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>


                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                               

                                            </div>
                                        </div>

                                        <div class="col-md-4 form-group">
                                             <div class="form-group" style="padding-left: 0px;">
                                                    <asp:Button ID="btnAddIpAddr" runat="server" Text="Add IP Address" Style="height: 31px; margin: 19px 0 0 5px; float:left; width: 34%; padding: 0 7px; cursor: pointer;" CssClass="btn btn-primary" Visible="false" OnClick="btnAddIpAddr_Click" />


                                                    <asp:Button ID="btnIpSave" runat="server" Text="MAPPING" CssClass="btn btn-primary" OnClick="btnIpSave_Click"
                                                        Style="height: 31px; margin: 21px 0 0 -36px; padding: 0 7px;  width: 74%; cursor: pointer;" ValidationGroup="ENK1" />
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


    <script>
        $(document).ready(function () {
            var pattern = /\b(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\b/;
            x = 46;
            //$('input[type="text"]').keypress(function (e) {
            $('$txtMultiIPAdress').keypress(function (e) {


                if (e.which != 8 && e.which != 0 && e.which != x && (e.which < 48 || e.which > 57)) {
                    console.log(e.which);
                    return false;
                }
            }).keyup(function () {
                var this1 = $(this);
                if (!pattern.test(this1.val())) {
                    $('#validate_ip').text('Not Valid IP');
                    while (this1.val().indexOf("..") !== -1) {
                        this1.val(this1.val().replace('..', '.'));
                    }
                    x = 46;
                } else {
                    x = 0;
                    var lastChar = this1.val().substr(this1.val().length - 1);
                    if (lastChar == '.') {
                        this1.val(this1.val().slice(0, -1));
                    }
                    var ip = this1.val().split('.');
                    if (ip.length == 4) {
                        $('#validate_ip').text('Valid IP');
                    }
                }
            });
        });
    </script>

</asp:Content>

