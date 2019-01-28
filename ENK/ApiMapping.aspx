<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="ApiMapping.aspx.cs" Inherits="ENK.ApiMapping" %>

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
                                <h2>SELLER API MAPPING</h2>
                            </header>
                            <div>

        <div class="row" >
            <div class="col-md-12 form-group">

                <label>API Status</label>
                <div class="chckbx">
                    <asp:CheckBox ID="chkOnOff" runat="server" />
                </div>
            </div>
            <div class="col-md-4 form-group">
                <label>Client Code</label>
                <asp:TextBox ID="txtClientCode" runat="server" CssClass="form-control"></asp:TextBox>

                <asp:RequiredFieldValidator runat="server" ID="reqName" controltovalidate="txtClientCode" errormessage="Please Enter Client Code!" ValidationGroup="ENK1" ForeColor="Red" />
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtClientCode" ErrorMessage="Please Enter Only Numbers" ForeColor="Red" ValidationGroup="ENK1" ValidationExpression="^[0-9]*$"></asp:RegularExpressionValidator>

                <br />
                <div class="table-responsive" id="div1" visible="true">
                    <asp:GridView ID="grdAPIMapping" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered">
                        <columns>
                                                                        <asp:TemplateField HeaderText="API" HeaderStyle-BackColor="Gray" HeaderStyle-ForeColor="white">
                                                                            <ItemTemplate>
                                                                                <asp:CheckBox ID="chkAPI" runat="server" AutoPostBack="true" />
                                                                                <asp:HiddenField ID="hddnApiID" runat="server" Value='<%# Bind("Id") %>' />
                                                                                <asp:Label ID="lblApitype" runat="server" Text='<%# Bind("ApiName") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </columns>
                    </asp:GridView>
                </div>
            </div>
            <div class="col-md-8 form-group"></div>
        </div>
        <div class="col-md-2 form-group">


            <asp:Button ID="btnApiSave" runat="server" Text="MAPPING" CssClass="btn btn-primary" OnClick="btnApiSave_Click" Style="height: 31px; margin: 97px 0 0 5px; padding: 0 7px; float: right; width: 94%; cursor: pointer;" ValidationGroup="ENK1" />
        </div>
    </div>
                                </article>
                    </div>
                </section>
            </div>
          </div>
</asp:Content>
