<%@ Page Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="SuscriberLog.aspx.cs" Inherits="ENK.SuscriberLog" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- MAIN PANEL -->
		<div id="main" role="main" style="margin-top: 0px!important;">

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
							Subscriber Log Detail						</span>						</h1>
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
							<div class="jarviswidget jarviswidget-color-blueDark" id="wid-id-0"  data-widget-colorbutton="false" data-widget-editbutton="false" data-widget-togglebutton="false" data-widget-deletebutton="false" data-widget-fullscreenbutton="false" data-widget-custombutton="false" data-widget-collapsed="false" data-widget-sortable="false">
							 
								<header>
									<span class="widget-icon"> <i class="fa fa-table"></i> </span>
									<h2>Log Detail</h2>
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
                                            <label class="input"> 
                                                <asp:TextBox ID="TextBox1" runat="server" Style="margin: 0px; height: 271px; width: 1081px;" Enabled="false" TextMode="MultiLine"></asp:TextBox>
                                            </label>                                  
                                        </div>  
                                            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                            <footer class="boxBtnsubmit">
                                            <asp:Button ID="btnLog" runat="server" class="btn btn-primary" Text="LOG" OnClick="btnLog_Click"  /> 
                                            </footer> 
                                            </div>                        
                                    </div>        
                                
                                    </div>
                
                <footer class="boxBtnsubmit">
                               
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
