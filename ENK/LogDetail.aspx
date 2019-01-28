<%@ Page Language="C#"  MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="LogDetail.aspx.cs" Inherits="ENK.LogDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
      <script type="text/javascript">
          function validate1() {
               
              var div = document.getElementById('divLog1');
              div.style.display = 'block';
              
              var div2 = document.getElementById('divLog2');
              div2.style.display = 'None';

              var div3 = document.getElementById('divLog3');
              div3.style.display = 'None';
              
             return false;
          }
          function validate2() {
              var div = document.getElementById('divLog1');
              div.style.display = 'None';

              var div2 = document.getElementById('divLog2');
              div2.style.display = 'block';

              var div3 = document.getElementById('divLog3');
              div3.style.display = 'None';
              
              return false;
          }
          function validate3() {
              var div = document.getElementById('divLog1');
              div.style.display = 'None';

              var div2 = document.getElementById('divLog2');
              div2.style.display = 'None';

              var div3 = document.getElementById('divLog3');
              div3.style.display = 'block';
              return false;
          }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- MAIN PANEL -->
		<div id="main" role="main"  style="margin-top: 0px!important;" >

			<!-- RIBBON -->
			 
			<!-- END RIBBON -->

			<!-- MAIN CONTENT -->
			<div id="content">
				<div class="row new-inerpage">
					<div class="col-xs-12 col-sm-5 col-md-5 col-lg-4">
						<h1 class="page-title txt-color-blueDark">
							<i class="fa fa-leaf fa-fw"></i> 
								Admin
							<span>> 
								Log Detail						</span>						</h1>
					</div>
					<div class="col-xs-12 col-sm-7 col-md-7 col-lg-8">
						 <ul id="sparks" class="">
							<li class="sparks-info">
								 <asp:LinkButton ID="linkLog1" runat="server" onclick="linkLog1_Click" >Log 1</asp:LinkButton>
                                <span><i class="fa fa-file"></i></span>
							</li>
							<li class="sparks-info">
                                <asp:LinkButton ID="linkLog2" runat="server" OnClick="linkLog2_Click" >Log 2</asp:LinkButton>
								 <span><i class="fa fa-file"></i></span>
                                	 
                              	 
							</li>
							<li class="sparks-info">
								 <asp:LinkButton ID="linkLog3" runat="server" OnClick="linkLog3_Click" >Log 3</asp:LinkButton>
                                <span><i class="fa fa-file"></i></span>
							</li>
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
							<div id="divLog1"  runat="server"  class="jarviswidget jarviswidget-color-blueDark"   data-widget-colorbutton="false" data-widget-editbutton="false" data-widget-togglebutton="false" data-widget-deletebutton="false" data-widget-fullscreenbutton="false" data-widget-custombutton="false" data-widget-collapsed="false" data-widget-sortable="false">
							 
								<header>
									<span class="widget-icon"> <i class="fa fa-table"></i> </span>
									<h2>Log 1 Detail</h2>
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
                                                <asp:TextBox ID="TextBox1" class="col-xs-12 col-sm-12 col-md-12 col-lg-12" runat="server" TextMode="MultiLine"  Style="margin: 0px; height: 271px;"  ></asp:TextBox>
                                            </label>                                  
                                        </div>  
                                            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                            <footer class="boxBtnsubmit">
                                            <asp:Button ID="btnLog1" runat="server" class="btn btn-primary" Text="LOG 1" OnClick="btnLog1_Click"   /> 
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
                            <div id="divLog2" runat="server"  class="jarviswidget jarviswidget-color-blueDark"    data-widget-colorbutton="false" data-widget-editbutton="false" data-widget-togglebutton="false" data-widget-deletebutton="false" data-widget-fullscreenbutton="false" data-widget-custombutton="false" data-widget-collapsed="false" data-widget-sortable="false">
							 
								<header>
									<span class="widget-icon"> <i class="fa fa-table"></i> </span>
									<h2>Log 2 Detail</h2>
								</header>

								<!-- widget div-->
								<div>
									<!-- widget edit box -->
									<div class="jarviswidget-editbox">
										<!-- This area used as dropdown edit box -->
									</div>
									<!-- end widget edit box -->

									<!-- widget content -->
                                    <div id="Div2" class="smart-form client-form">
                    
                                    <div class="box-input">        
                                    <div class="row">                               
                                        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12"> 
                                        <label class="input"> 
                                            <asp:TextBox ID="TextBox2" class="col-xs-12 col-sm-12 col-md-12 col-lg-12" runat="server" Style="margin: 0px; height: 271px; "  TextMode="MultiLine"></asp:TextBox>
                                        </label>                                  
                                        </div>  
                                        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                        <footer class="boxBtnsubmit">
                                        <asp:Button ID="btnLog2" runat="server" class="btn btn-primary" Text="LOG 2" OnClick="btnLog2_Click"   /> 
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
                            <div id="divLog3" runat="server" class="jarviswidget jarviswidget-color-blueDark"    data-widget-colorbutton="false" data-widget-editbutton="false" data-widget-togglebutton="false" data-widget-deletebutton="false" data-widget-fullscreenbutton="false" data-widget-custombutton="false" data-widget-collapsed="false" data-widget-sortable="false">
							 
								<header>
									<span class="widget-icon"> <i class="fa fa-table"></i> </span>
									<h2>Log 3 Detail</h2>
								</header>

								<!-- widget div-->
								<div>
									<!-- widget edit box -->
									<div class="jarviswidget-editbox">
										<!-- This area used as dropdown edit box -->
									</div>
									<!-- end widget edit box -->

									<!-- widget content -->
                                    <div id="Div4" class="smart-form client-form">
                    
                                    <div class="box-input">        
                                    <div class="row">                               
                                        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12"> 
                                        <label class="input"> 
                                            <asp:TextBox ID="TextBox3" runat="server" class="col-xs-12 col-sm-12 col-md-12 col-lg-12" Style="margin: 0px; height: 271px; "   TextMode="MultiLine"></asp:TextBox>
                                        </label>                                  
                                        </div>  
                                        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                        <footer class="boxBtnsubmit">
                                        <asp:Button ID="btnLog3" runat="server" class="btn btn-primary" Text="LOG 3" OnClick="btnLog3_Click"   /> 
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
