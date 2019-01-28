<%@ Page Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="SimDetails.aspx.cs" Inherits="ENK.SimDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
                        <i class="fa fa-home"></i>
                        Dashboard
							<span>> 
								Available Sim
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
                                <h2>Sim Details<asp:Label ID="lblCount" Style="font-size: 12px!important;" class="label" runat="server" Text=""></asp:Label></h2>
                            </header>


                            <!-- widget div-->
                            <div>

                                <!-- widget edit box -->
                                <div class="jarviswidget-editbox">
                                    <!-- This area used as dropdown edit box -->

                                </div>
                                <!-- end widget edit box -->

                                <!-- widget content -->
                                <div class="widget-body">
                                    <div class="table-responsive">
                                        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12" style="max-height: 300px; overflow-y: auto;">

                                            <asp:Repeater ID="rptBlankSim" runat="server">
                                                <HeaderTemplate>
                                                    <table class="table table-bordered" id="dataTables-example">
                                                        <thead>
                                                            <tr>

                                                                <th>SIM Serial No</th>
                                                                <th>Current Distributor</th>
                                                                <th>Transfer To</th>
                                                                <th>Inventory Status</th>
                                                            </tr>
                                                            <tbody>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <tr>
                                                        <td>
                                                            <asp:Label runat="server" ID="Label1" Text='<%# Eval("SIMSerialNumber") %>' />
                                                        </td>
                                                        <td>
                                                            <asp:Label runat="server" ID="Label2" Text='<%# Eval("Distributor") %>' />
                                                        </td>
                                                        <td>
                                                            <asp:Label runat="server" ID="Label6" Text='<%# Eval("TransferTo") %>' />
                                                        </td>
                                                        <td>
                                                            <asp:Label runat="server" ID="Label8" Text='<%# Eval("InventoryStatus") %>' />
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
</asp:Content>
