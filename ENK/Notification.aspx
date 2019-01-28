<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="Notification.aspx.cs" Inherits="ENK.Notification" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- MAIN PANEL -->
		<div id="main" role="main" style="margin-top: 0px!important;" >
             
            
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
			<!-- RIBBON -->
			 
			<!-- END RIBBON -->

			<!-- MAIN CONTENT -->
			<div id="content">
				<div class="row new-inerpage">
					<%--<div class="col-xs-12 col-sm-5 col-md-5 col-lg-4">
						<h1 class="page-title txt-color-blueDark">
							<i class="fa fa-user fa-fw"></i> 
								Notification
							<span>					</span>						</h1>
					</div>--%>
					<%--<div class="col-xs-12 col-sm-7 col-md-7 col-lg-8">
						<ul id="sparks"  class="">
							 <li style="border-left: 0px!important;" id="liSave" runat="server" class="sparks-info">
                                <asp:Button ID="btnSaveUp" runat="server" Style="float: right; height: 31px;margin: 10px 0 0 5px;padding: 0 22px; font: 300 15px/29px 'Open Sans',Helvetica,Arial,sans-serif; cursor: pointer;" Text="SEND" class="btn btn-primary" Visible="true" OnClick="btnSaveUp_Click" OnClientClick="javascript:return validate();" /> 
  
							</li>	
                            <li style="border-left: 0px!important;" id="liReset" runat="server" class="sparks-info">
                                <asp:Button ID="btnResetUp" runat="server" Style="float: right; height: 31px;margin: 10px 0 0 5px;padding: 0 22px; font: 300 15px/29px 'Open Sans',Helvetica,Arial,sans-serif; cursor: pointer;" Text="RESET" class="btn btn-primary" Visible="true" OnClick="btnResetUp_Click" /> 
  
							</li>
                            <li style="border-left: 0px!important;" id="liBack" runat="server" class="sparks-info">
								 
                                <asp:Button ID="btnBackUp" runat="server" Text="BACK" Style="float: right; height: 31px;margin: 10px 0 0 5px;padding: 0 22px; font: 300 15px/29px 'Open Sans',Helvetica,Arial,sans-serif; cursor: pointer;" class="btn btn-primary" Visible="true" OnClick="btnBackUp_Click"  /> 
                                    
							</li>
                            
						</ul>
					</div>--%>
				</div>

				<!-- widget grid -->
				<section id="widget-grid" class="">

					<!-- row -->
					<div class="row">
						<!-- NEW WIDGET START -->
						<article class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
							<!-- Widget ID (each widget will need unique ID)-->
							<div class="jarviswidget jarviswidget-color-blueDark" id="wid-id-0"  data-widget-colorbutton="false" data-widget-editbutton="false" data-widget-togglebutton="false" data-widget-deletebutton="false" data-widget-fullscreenbutton="false" data-widget-custombutton="false" data-widget-collapsed="false" data-widget-sortable="false">
								
								<header>
									<span class="widget-icon"> <i class="fa fa-table"></i> </span>
									<h2>Notification <asp:Label ID="Label1" runat="server" Text=""></asp:Label> </h2
                  >
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
                                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12"> 
                                       
                                       <div class="table-responsive" style="max-height: 237px; overflow-y: auto;" >
                                       
                                        <asp:Repeater ID="RepeaterDistributor" runat="server">
                                         <HeaderTemplate >
                                              <table class="table table-bordered"  id="dataTables-example">
                                                <thead>
                                                    <tr>
                                                        <th> <asp:CheckBox ID="chkAll" OnCheckedChanged="chkAll_CheckedChanged" AutoPostBack="true" runat="server" /></th>
                                                        <th>Distributor Name</th>                                     
                                                    </tr> 
                                                <tbody>             
                                              </HeaderTemplate>
                                       
                                              <ItemTemplate> 
                                                <tr>
                                                     
                                                    <td>
                                                        <asp:CheckBox ID="CheckBox2" runat="server" />
                                                    </td>
                                                    <td>
                                                    <asp:Label runat="server" ID="distributorName" text='<%# Eval("distributorName") %>' />
                                                        <asp:HiddenField ID="hnddistributorID" runat="server" Value='<%# Eval("distributorID") %>' />
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
                           <div class="row">
                                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12"> 
                                   <asp:Label ID="lblTextMessage" class="label" runat="server" Text="Text Message"></asp:Label>
                                        <%--<label class="label">Text Message</label>--%>
                                        <label class="input">
                                        <asp:TextBox ID="txtTextString" class="text-area" runat="server" TextMode="MultiLine" Height="140px"></asp:TextBox>    
                                        </label>   
                                    
                                        <asp:Label ID="lblUpload" class="label" runat="server" Text="Upload" Visible="false"></asp:Label>
                                        <label class="input">
                                            <asp:FileUpload ID="FileImageUpload" Visible="false" runat="server" />
                                        </label>
                                 </div>
                           </div>
                        </div>
                
                <footer class="boxBtnsubmit">
                        <asp:Button ID="btnBack" runat="server" Text="BACK" class="btn btn-primary" OnClick="btnBack_Click"    />
                        <asp:Button ID="btnReset" runat="server" Text="RESET" class="btn btn-primary" OnClick="btnReset_Click"/>
                          
                        <asp:Button ID="btnSave" runat="server" Text="SEND" class="btn btn-primary" ValidationGroup="save" OnClientClick="javascript:return validate();" OnClick="btnSave_Click" />
                           
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
