<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MainMaster.Master" CodeBehind="PortinStatus.aspx.cs" Inherits="ENK.PortinStatus" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

   <script type = "text/javascript">
       function printpanel() {

           var panel = document.getElementById("divprint");
           var printWindow = window.open('', '', 'height=400,width=800');
           printWindow.document.write('<html><head><title>DIV Contents</title>');
           printWindow.document.write('</head><body >');
           printWindow.document.write(panel.innerHTML);
           printWindow.document.write('</body></html>');
           printWindow.document.close();
           setTimeout(function () {
               printWindow.print();



           }, 500);
           return false;
       }
    </script> 

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div id="main" role="main" style="margin-top: 0px!important;" >

			<!-- RIBBON -->
			 
			<!-- END RIBBON -->

			<!-- MAIN CONTENT -->
			<div id="content">
				<div class="row new-inerpage">
					<div class="col-xs-12 col-sm-5 col-md-5 col-lg-4">
						<h1 class="page-title txt-color-blueDark">
							<i class="fa fa-lg fa-fw fa-pencil-square-o"></i> 
								Sim Provisioning
							<span>> 
								PortIn Status						</span>						</h1>
					</div>
					<div class="col-xs-12 col-sm-7 col-md-7 col-lg-8">
						<ul id="sparks" class="">
							 
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
								<header>
									<span class="widget-icon"> <i class="fa fa-table"></i> </span>
									<h2>PortIn Status</h2>
								</header>
								 
									<!-- widget edit box -->
									<div class="jarviswidget-editbox">
										<!-- This area used as dropdown edit box -->
									</div>
                                   <div id="login-form" class="smart-form client-form">                     
                                        <div class="box-input">  
                                            <div class="row"> 
                                                <div id="div1" runat="server">
                                                 <div id="divprint"  class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                                    <div id="divDistributor" style="border: 2px solid rgb(109, 126, 247);;" runat="server" class="col-xs-12 col-sm-6 col-md-6 col-lg-6"> 
                                                        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">                                  
                                                            <asp:Label ID="lblMessage" runat="server" Text="" Style="color: red;font-size: larger;"></asp:Label>
                                                        </div>
                                                         

                                                        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-4">
                                                           <asp:Label ID="lbsBalanceText" runat="server" Text="Account Balance " Style="color: red;font-size: initial;" ></asp:Label>
                                                           </div>
                                                        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-6"> 
                                                            <asp:Label ID="lblBalance" runat="server" Text="" Font-Bold="true" Style="color: red;font-size: larger;" ></asp:Label>
  
                                                        </div>
                                                        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-2">
                                                        </div>
                                                        <div id="divPaymentDetail" visible="false" runat="server">
                                                             <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                                                 <asp:Label ID="Label1" runat="server" Style="text-transform: uppercase; font-weight: 600; color: black;" Text="Tranaction Detail"></asp:Label>
                                                            </div>
                                                            <div class="col-xs-12 col-sm-3 col-md-3 col-lg-12">
                                                                <asp:Label ID="lblTransactionAmount" runat="server" Style="text-transform: uppercase;  color: black;"  Text=""></asp:Label>
                                                            </div>
                                                         
                                                            
                                                            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                                                 <asp:Label ID="lblTransactionDate" runat="server" Style="text-transform: uppercase; color: black;" Text=""></asp:Label>
                                                            </div>
                                                         
                                                            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                                                 <asp:Label ID="lblPayerName" runat="server" Style="text-transform: uppercase;   color: black;" Text=""></asp:Label>
                                                            </div>
                                                        
                                                            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                                                 <asp:Label ID="lblAddress" runat="server" Style="text-transform: uppercase;   color: black;" Text=""></asp:Label>
                                                            </div>
                                                            
                                                        </div>
                                                        <div id="divActivationDetail" visible="false" runat="server">
                                                            <div class="col-xs-12 col-sm-3 col-md-3 col-lg-12">
                                                                <asp:Label ID="lblSimnumber" runat="server" Style="text-transform: uppercase;"  Text=""></asp:Label>
                                                            </div>
                                                         
                                                            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                                                 <asp:Label ID="lblmsisdn" runat="server" Style="text-transform: uppercase;  color: rgb(24, 19, 224); font-weight: 600;" Text=""></asp:Label>
  
                                                            </div>
                                                         
                                                            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                                                 <asp:Label ID="lblemail" runat="server" Style="text-transform: uppercase; color: rgb(24, 19, 224); font-weight: 600;" Text=""></asp:Label>
                                                            </div>
                                                        
                                                             
                                                            
                                                        </div>
                                                    </div>
                                                    
                                                    <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">                                  
                                                        <asp:Label ID="lblresponse"  runat="server" Visible="false" Text=""></asp:Label>
                                            
                                                    </div>
                                                 </div> 
                                                 <div id="divBtn" runat="server" class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                                    <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6"> 
                                                        <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">
                                                            
                                                            <asp:Button ID="btnPrint" runat="server" Text="Print" class="btn btn-primary" Style="height: 31px;margin: 10px 0 0 5px;padding: 0 22px; font: 300 15px/29px 'Open Sans',Helvetica,Arial,sans-serif; cursor: pointer;" onclientclick="javascript:return printpanel()"/>   
                                                            
                                                        </div>
                                                        <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6"> 
                                                            <asp:Button ID="btnCancel" runat="server" Text="Back" class="btn btn-primary" Style="height: 31px;margin: 10px 0 0 5px;padding: 0 22px; font: 300 15px/29px 'Open Sans',Helvetica,Arial,sans-serif; cursor: pointer;" OnClick="btnCancel_Click"/> 
                                                        </div>
                                                        
                                                    </div>
                                                    <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6"> 

                                                    </div>
                                                </div>
                                                    <div id="divlink" runat="server" visible="false" class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                                    <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6"> 
                                                        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                                             
                                                            <asp:HyperLink ID="HyperLink1" NavigateUrl="~/ActivationPortin.aspx" Class="pull-right" runat="server">Back to PortIn</asp:HyperLink>
                                                        </div>
                                                        
                                                    </div>
                                                    <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6"> 

                                                    </div>
                                                </div>
                                         
                                                </div>
                                        
                                            </div>  
                                        </div>
                                   </div>
                                 
                             </div>
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
		<%--</div>--%>
		<!-- END MAIN PANEL -->
    </div>
</asp:Content>
