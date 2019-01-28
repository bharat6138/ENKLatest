<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="NotificationMaster.aspx.cs" Inherits="ENK.NotificationMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- MAIN PANEL -->
    <div id="main" role="main" style="margin-top: 0px!important;">


        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <!-- RIBBON -->

        <!-- END RIBBON -->

        <!-- MAIN CONTENT -->
        <div id="content">
            <div class="row new-inerpage">
                <div class="col-xs-12 col-sm-5 col-md-5 col-lg-4">
                    <h1 class="page-title txt-color-blueDark">
                        <i class="fa fa-user fa-fw"></i>
                        Notification
							<span></span></h1>
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

                            <header>
                                <span class="widget-icon"><i class="fa fa-table"></i></span>
                                <h2>Notification Received </h2>
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
                                            <div class="table-responsive">
                                            <asp:GridView ID="GridNotification" runat="server" AutoGenerateColumns="false" class="table table-hover" OnItemCommand="GridNotification_ItemCommand"  >                                                <Columns>
                                                    <asp:TemplateField HeaderText="S No" HeaderStyle-Width="10%">
                                                        <ItemTemplate>
                                                            <%# Container.DataItemIndex + 1 %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                      <asp:TemplateField HeaderText="Message" HeaderStyle-Width="50%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label1" runat="server" class="truncate" Text='<%# Eval("NotificationText") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                              <%--      <asp:BoundField HeaderText="Message" HeaderStyle-Width="50%" DataField="NotificationText"  />--%>
                                                    <asp:BoundField HeaderText="Sent Date"  HeaderStyle-Width="20%" DataField="CreatedDtTM" />
                                                   <asp:BoundField HeaderText="Sent By" HeaderStyle-Width="20%" DataField="CreatedBy" />
                                              <asp:TemplateField HeaderText="Action" HeaderStyle-Width="50%">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lblView" CommandName="View" CommandArgument='<%# Eval("ID") %>' runat="server" ToolTip="View" OnClick="lblView_Click" >View</asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                       </Columns>
                                            </asp:GridView>
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
            <!-- end widget grid -->
        </div>
        <!-- END MAIN CONTENT -->
    </div>
    <!-- END MAIN PANEL -->
</asp:Content>
