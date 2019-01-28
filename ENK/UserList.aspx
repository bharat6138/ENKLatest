<%@ Page Language="C#"  MasterPageFile="~/MainMaster.Master"  AutoEventWireup="true" CodeBehind="UserList.aspx.cs" Inherits="ENK.UserList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
      <link href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" rel="stylesheet" />
    <link href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" rel="stylesheet" />
        <link href="https://cdn.datatables.net/1.10.13/css/dataTables.bootstrap.min.css" rel="stylesheet" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        
     <div id="main" role="main"  style="margin-top: 0px!important;" >

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
								User List
							</span>
						</h1>
					</div>

                     <div class="col-xs-5 col-sm-7 col-md-7 col-lg-3">
                        <asp:TextBox ID="txtSearch"  class="form-control" runat="server" placeholder="Search User Name"></asp:TextBox>
                        <asp:TextBox ID="txtUasername" style="display:none" class="form-control" runat="server" placeholder="User name"></asp:TextBox>
                      
                    </div>
                    <div class="col-xs-5 col-sm-7 col-md-7 col-lg-3">
                          <asp:TextBox ID="txtEmail"  class="form-control" runat="server" placeholder="Search email"></asp:TextBox>
                       
                    </div>
                     <div class="col-xs-5 col-sm-7 col-md-7 col-lg-2">
                          <asp:Button ID="btnsearch" runat="server" Text="Search" OnClick="btnsearch_Click" /> 
                    </div>
					<div class="col-xs-7 col-sm-5 col-md-5 col-lg-8">
						<ul id="sparks" class="">
							<li class="sparks-info">
								<a href="User.aspx" title="New">
                                	<span><i class="fa fa-file"></i></span> New
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
									<h2>User List</h2>

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
											
											 <asp:Repeater ID="RepeaterUserList" runat="server" OnItemCommand="RepeaterUserList_ItemCommand">
                                              <HeaderTemplate>
                                              <table class="table table-bordered" id="example">
                                                <thead>
                                                    <tr>
                                                        <th>User ID</th>
                                                         <th>User Name</th>
                                                         
                                                         <th>Email ID</th>
                                                      
                                                        <th>Distributor Name</th>
                                                        <th>Action</th>                                                       
                                                         <th>Reset Password</th>
                                                    </tr>
                                                    </thead>
                                                    <tbody>               
                                              </HeaderTemplate>
                                              <ItemTemplate>      
         
                                                <tr class="gradeA">
                                                    <td>
                                                    <asp:Label runat="server" ID="Label1" text='<%# Eval("UserName") %>' /> 
                                                   <asp:HiddenField ID="hdnDistributorId" runat="server" Value='<%# Bind("DistributorID") %>' />
                                                    <asp:HiddenField ID="hdnUserID" runat="server" Value='<%# Bind("ID") %>' />                                                 
                                                    </td>                    
                                                    <td>
                                                   <asp:Label runat="server" ID="Label2" text='<%# Eval("Fname")+""+Eval("Lname") %>' />
                                                    </td>
                                                     
                                                    <td>
                                                    <asp:Label runat="server" ID="Label3" text='<%# Eval("EmailID") %>' />
                                                    </td>
                                                     <td>
                                                    <asp:Label runat="server" ID="Label4" text='<%# Eval("Name") %>' />
                                                    </td>
                                                     <td>
                                                    <asp:ImageButton id="lnkView" runat="server" CausesValidation="false" ToolTip="View User" CommandName="View" CommandArgument='<%#Eval("ID") %>' ImageUrl="~/img/1399984583_view.png" /> &nbsp
                                                    <asp:ImageButton ID="lnkEdit"  runat="server" CausesValidation="false" ToolTip="Edit User" CommandName="RowEdit" CommandArgument='<%#Eval("ID") %>' ImageUrl="~/img/Pencil-icon.png"  />&nbsp
                                                    </td>
                                                      <td>

                                                        <asp:LinkButton ID="lnkResetPass" runat="server" CausesValidation="false"  Onclick="lnkResetPass_Click" ToolTip="Reset Password" CommandArgument='<%#Eval("distributorID") %>' ImageUrl="~/img/Pencil-icon.png" ><img src="img/settings.png" /> </asp:LinkButton>
                                                        

                                                    </td>
                                                </tr>          
        
                                              </ItemTemplate>
    
                                              <FooterTemplate>
                                                
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


   
    <script src="https://code.jquery.com/jquery-3.3.1.js"></script>
    <script src="https://cdn.datatables.net/1.10.19/js/jquery.dataTables.min.js"></script>
    <script src="https://cdn.datatables.net/1.10.18/js/dataTables.bootstrap.min.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.5.2/js/dataTables.buttons.min.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.5.2/js/buttons.flash.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jszip/3.1.3/jszip.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/pdfmake/0.1.36/pdfmake.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/pdfmake/0.1.36/vfs_fonts.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.5.2/js/buttons.html5.min.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.5.2/js/buttons.print.min.js"></script>
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
               

                "searching": true
            });


        });

    </script>

</asp:Content>

