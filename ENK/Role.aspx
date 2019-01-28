<%@ Page Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="Role.aspx.cs" Inherits="ENK.Role" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- MAIN PANEL -->
		<div id="main" role="main" style="margin-top: 0px!important;" >

			<!-- RIBBON -->
			 
			<!-- END RIBBON -->

			<!-- MAIN CONTENT -->
			<div id="content">
				<div class="row new-inerpage">
					<div class="col-xs-12 col-sm-5 col-md-5 col-lg-4">
						<h1 class="page-title txt-color-blueDark">
							<i class="fa fa-user fa-fw"></i> 
								Admin
							<span>> 
								New Role						</span>						</h1>
					</div>
					<div class="col-xs-12 col-sm-7 col-md-7 col-lg-8">
						<%--<ul id="sparks" class="">
							<li class="sparks-info">
								<a href="newpage.html" title="Save">
                                	<span><i class="fa fa-floppy-o"></i></span> Save                              	</a>							</li>
							<li class="sparks-info">
								<a href="#" title="Save &amp; Close">
                                	<span><i class="fa fa-times"></i></span> Save &amp; Close                              	</a>                            </li>
							<li class="sparks-info">
								<a href="#" title="Save &amp; New">
                                	<span><i class="fa fa-folder"></i></span> Save &amp; New                              	</a>                            </li>
                            <li class="sparks-info">
								<a href="#" title="Close">
                                	<span><i class="fa fa-times"></i></span> Close                                </a>                            </li>
						</ul>--%>
					</div>
				</div>

				<!-- widget grid -->
				<section id="widget-grid" class="">

					<!-- row -->
					<div class="row">
						<!-- NEW WIDGET START -->
						<article class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
							<!-- Widget ID (each widget will need unique ID)-->
							<div class="jarviswidget jarviswidget-color-blueDark" id="wid-id-0"  data-widget-colorbutton="false" data-widget-editbutton="false" data-widget-togglebutton="false" data-widget-deletebutton="false" data-widget-fullscreenbutton="false" data-widget-custombutton="false" data-widget-collapsed="false" data-widget-sortable="false">
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
									<h2>New Role</h2>
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
                                <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">                                   
                                    <label class="label">Role Name</label>                                       
                                    <label class="input">  
                                        <asp:HiddenField ID="hddnRoleID" runat="server" />
                                        <asp:TextBox ID="txtRole" runat="server" title="User Name...."></asp:TextBox> 
                                             <b class="tooltip tooltip-top-right">
                                        <i class="fa fa-lock txt-color-teal"></i> Enter Role Name</b>                                   

                                    </label>                                    
                                </div>    
                                <div id="divActive" runat="server" visible="false" class="col-xs-12 col-sm-6 col-md-6 col-lg-6">                                    
                                    <label class="label">IsUserActive</label>                                       
                                        <label class="text"> 
                                        <asp:CheckBox ID="CheckBox1" runat="server" Checked="true" /> 
                                        </label>                                  
                                </div>         
                            </div> 
                            <div class="row">
                                <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">                                    
                                                                                                       
                                </div>          
                            </div>  
                            
                           <div class="table-responsive" style="max-height: 300px; overflow-y: auto; overflow-x:hidden">										
								 
						   <asp:Repeater ID="RepeaterScreen" runat="server" >
                                    <HeaderTemplate>
                                    <table class="table table-bordered" id="dataTables-example">
                                    <thead>
                                        <tr>
                                            <th>SNo</th>
                                            <th></th>
                                            <th>Screen Name</th>
                                            <th>Add/Edit</th>
                                            <th>View</th>                                              
                                            <th>Delete</th>                                  
                                        </tr>                                                  
                                        <tbody>           
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                    <tr>
                                           <td> 
                                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("sr") %>'></asp:Label> 
                                           </td>
                                           <td> 
                                            <asp:CheckBox ID="chkRoles" AutoPostBack="false" runat="server" />
                                           </td> 
                                           <td> 
                                            <asp:Label ID="Label2" runat="server" Text='<%# Eval("ScreenName") %>'></asp:Label> 
                                            <asp:HiddenField ID="hddnScreenID" runat="server" Value='<%# Eval("ScreenId") %>' />
                                           </td> 
                                           <td> 
                                           <asp:CheckBox ID="CheckBox1" runat="server" AutoPostBack="false"/>
                                           </td>
                                           <td> 
                                           <asp:CheckBox ID="chkView" runat="server" AutoPostBack="false"/>
                                           </td>
                                           <td> 
                                            <asp:CheckBox ID="chkEdit" runat="server" AutoPostBack="false" />
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
                
                       <footer class="boxBtnsubmit">
                                        <asp:Button ID="btnBack" runat="server" Text="BACK" class="btn btn-primary" Visible="false"  OnClick="btnBack_Click"  />
                                        <asp:Button ID="btnReset" runat="server" Text="RESET" class="btn btn-primary" OnClick="btnReset_Click" />
                                        <asp:Button ID="btnSave" runat="server" Text="SAVE" class="btn btn-primary" OnClick="btnSave_Click"  />
                                        <asp:Button ID="btnUpdate" runat="server" Text="UPDATE" class="btn btn-primary" Visible="false"  OnClick="btnUpdate_Click" />
                                         
                      </footer>
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