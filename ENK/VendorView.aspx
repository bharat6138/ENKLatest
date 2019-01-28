<%@ Page Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="VendorView.aspx.cs" Inherits="ENK.VendorView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div id="main" role="main" style="margin-top: 0px!important;" >

			<!-- RIBBON -->
			 
			<!-- END RIBBON -->

			<!-- MAIN CONTENT -->
			<div id="content">
				<div class="row row-fullWidth">
					<div class="col-xs-5 col-sm-7 col-md-7 col-lg-4">
						<h1 class="page-title txt-color-blueDark">
							<i class="fa fa-user fa-fw"></i> 
								Admin
							<span>> 
								Network
							</span>
						</h1>
					</div>
					<div class="col-xs-7 col-sm-5 col-md-5 col-lg-8">
						<ul id="sparks" class="">
							<li class="sparks-info">
								<a href="Vendor.aspx" title="New">
                                	<span><i class="fa fa-file"></i></span> New
                               	</a>
							</li>
							<%--<li class="sparks-info">
								<a href="#" title="Edit">
                                	<span><i class="fa fa-backward"></i></span> Back
                              	</a>
							</li>--%>
							<%--<li class="sparks-info">
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
							<div class="jarviswidget jarviswidget-color-blueDark"   data-widget-colorbutton="false" data-widget-editbutton="false" data-widget-togglebutton="false" data-widget-deletebutton="false" data-widget-fullscreenbutton="false" data-widget-custombutton="false" data-widget-collapsed="false" data-widget-sortable="false">
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
									<span class="widget-icon"> <i class="fa fa-table"></i> </span>
									<h2>Network</h2>

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
										<div class="table-responsive" style="max-height: 300px; overflow-y: auto;">
											 <asp:Repeater ID="rptVendor" runat="server" OnItemCommand="rptVendor_ItemCommand" >
                                              <HeaderTemplate>
                                              <table class="table table-bordered" id="dataTables-example">
                                                <thead>
                                                    <tr>
                                                        <th>Code</th>
                                                        <th>Name</th>
                                                        <th>Email</th>
                                                        <th>Contact Person</th>
                                                        <th>Mobile No</th>
                                                        <th>Address</th>
                                                        <th>Action</th>
                                                         
                                                         
                                                    </tr> 
                                                <tbody>             
                                              </HeaderTemplate>
                                              <ItemTemplate> 
                                                <tr>
                                                    <td>
                                                    <asp:Label runat="server" ID="Label1" text='<%# Eval("VendorCode") %>' />
                                                    
                                                    <asp:HiddenField ID="hddnVendorID" runat="server" Value='<%# Bind("VendorID") %>' />
                                                    </td>  
                                                    <td>
                                                    <asp:Label runat="server" ID="Label8" text='<%# Eval("VendorName") %>' />
                                                    </td>
                                                    <td>
                                                    <asp:Label runat="server" ID="Label2" text='<%# Eval("VendorEmail") %>' />
                                                    </td>

                                                    <td>
                                                    <asp:Label runat="server" ID="Label3" text='<%# Eval("ContactPerson") %>' />
                                                    </td>

                                                    <td>
                                                    <asp:Label runat="server" ID="Label4" text='<%# Eval("VendorMobile") %>' />
                                                    </td>

                                                    <td>
                                                    <asp:Label runat="server" ID="Label5" text='<%# Eval("VendorAddress") %>' />
                                                    </td>
                                                     <td>
                                                    <asp:ImageButton id="lnkView" runat="server" CausesValidation="false" ToolTip="View Network" CommandName="View" CommandArgument='<%#Eval("VendorID") %>' ImageUrl="~/img/1399984583_view.png" /> &nbsp
                                                    <asp:ImageButton ID="lnkEdit"  runat="server" CausesValidation="false" ToolTip="Edit Network" CommandName="RowEdit" CommandArgument='<%#Eval("VendorID") %>' ImageUrl="~/img/Pencil-icon.png"  />&nbsp
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

