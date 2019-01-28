<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="Purchase.aspx.cs" Inherits="ENK.Purchase" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link type="text/css" rel="stylesheet" href="http://code.jquery.com/ui/1.10.3/themes/smoothness/jquery-ui.css" />
    <script type="text/javascript" src="http://code.jquery.com/jquery-1.8.2.js"></script>
    <script type="text/javascript" src="http://code.jquery.com/ui/1.10.3/jquery-ui.js"></script>
    <script type="text/javascript">

        function validate() {
            if (document.getElementById('<%=ddlVendor.ClientID%>').selectedIndex == 0) {
                alert('Please Select Network');
                document.getElementById("<%=ddlVendor.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=txtPurchaseDate.ClientID%>").value == "") {
                alert("Purchase Date Can't be Blank");
                document.getElementById("<%=txtPurchaseDate.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=txtPurchaseNo.ClientID%>").value == "") {
                alert("Purchase Number Can't be Blank");
                document.getElementById("<%=txtPurchaseNo.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=txtInvoiceNo.ClientID%>").value == "") {
                alert("Invoice Number Can't be Blank");
                document.getElementById("<%=txtInvoiceNo.ClientID%>").focus();
                return false;
            }
            return true;
        }
        function validateNewRowForSim() {
            
            if (document.getElementById("<%=txtSIM.ClientID%>").value == "" || document.getElementById("<%=txtSIM.ClientID%>").value == "8919601") {
                alert("SIM Can't be Blank");
                document.getElementById("<%=txtSIM.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=txtPIN.ClientID%>").value == "") {    
                alert("PIN Can't be Blank");
                document.getElementById("<%=txtPIN.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=txtPUK.ClientID%>").value == "") {
                alert("PUK Can't be Blank");
                document.getElementById("<%=txtPUK.ClientID%>").focus();
                return false;
            }
            if (document.getElementById('<%=ddlSIMTypeForSIM.ClientID%>').selectedIndex == 0) {
                alert('Please Select SIM Type');
                document.getElementById("<%=ddlSIMTypeForSIM.ClientID%>").focus();
                return false;
            }
            return true;
        }

        function validateNewRowForMobileSim() {

            if (document.getElementById("<%=txtMobileforMobileSIM.ClientID%>").value == "") {
                alert("Mobile Can't be Blank");
                document.getElementById("<%=txtMobileforMobileSIM.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=txtSIMforMobileSIM.ClientID%>").value == "") {
                alert("SIM Can't be Blank");
                document.getElementById("<%=txtSIMforMobileSIM.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=txtPINforMobileSIM.ClientID%>").value == "") {      
                alert("PIN Can't be Blank");
                document.getElementById("<%=txtPINforMobileSIM.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=txtPUKforMobileSIM.ClientID%>").value == "") {
                alert("PUK Can't be Blank");
                document.getElementById("<%=txtPUKforMobileSIM.ClientID%>").focus();
                return false;
            }
            if (document.getElementById('<%=ddlSIMTypeforMobileSIM.ClientID%>').selectedIndex == 0) {
                alert('Please Select SIM Type');
                document.getElementById("<%=ddlSIMTypeforMobileSIM.ClientID%>").focus();
                return false;
            }
            return true;
        }

        function ValidateSimNumber() {

            var a = document.getElementById('ContentPlaceHolder1_txtSIM').value.length;
            a = Number(a);
            
            if (document.getElementById('<%=ddlVendor.ClientID%>').selectedIndex == 1) {
                if (a < 19) {
                    alert("Lyca Sim Number should not be less than 19 digits");
                    <%-- document.getElementById("<%=txtSIM.ClientID%>").focus();--%>
                     return false;
                }
                if (a > 19) {
                    alert("Lyca Sim Number should not be greater than 19 digits");
                     <%--document.getElementById("<%=txtSIM.ClientID%>").focus();--%>
                     return false;
                }
            }
              if (document.getElementById('<%=ddlVendor.ClientID%>').selectedIndex == 2) {
                if (a < 20) {
                    alert("Lyca Sim Number should not be less than 20 digits");
                     <%--document.getElementById("<%=txtSIM.ClientID%>").focus();--%>
                     return false;
                }
                if (a > 20) {
                    alert("Lyca Sim Number should not be greater than 20 digits");
                     <%--document.getElementById("<%=txtSIM.ClientID%>").focus();--%>
                     return false;

                }
            }
        }

   </script>

       
   <script type="text/javascript">
       function ShowProgress() {
           $("#popupdiv").dialog({
               title: "",
               width: 430,
               height: 250,
               modal: true,
               buttons: {
                   Close: function () {
                       $(this).dialog('close');
                   }
               }
           });
           $(".ui-dialog-buttonpane").hide();
           $(".ui-dialog-titlebar").hide();
           $(".ui-widget-overlay").css({
               /*background: "#0F0101 url(images/ui-bg_flat_0_aaaaaa_40x100.png) repeat-x",
                 opacity: .3*/
               background: rgb(99, 127, 239),
               opacity: .3,
               filter: Alpha(Opacity = 50)
           });
           return true;
       };

       $('form').live("submit", function () {
           ShowProgress();
       });



    </script>
     <style type="text/css">
    .modal
    {
       
        top: 0;
        left: 0;        
        z-index: 99;
        opacity: 0.8;
        filter: alpha(opacity=80);
        -moz-opacity: 0.8;
        min-height: 100%;
        width: 100%;
    }
    .loading
    {
        font-family: Arial;
        font-size: 10pt;
        border: 5px solid #67CFF5;
        width: 550px;
        height: 150px;
        display: none;
        position:fixed; 
        background-color: White;
        z-index: 999;
    }
</style>
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
							<i class="fa fa-lg fa-fw fa-money"></i> 
								Inventory
							<span>> 
								Purchase						</span>						</h1>
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
									<h2>Purchase</h2>
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
                                    <label class="label">Network<asp:Label ID="Label19" ForeColor="Red" runat="server" Text="*"></asp:Label></label>                                       
                                    <label class="input">  
                                        <asp:DropDownList  class="text-area" ID="ddlVendor" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlVendor_SelectedIndexChanged">
                                        </asp:DropDownList> 
                                    </label>                                  
                                </div>
                                 <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">                                   
                                    <label class="label">Purchase Date<asp:Label ID="Label18" ForeColor="Red" runat="server" Text="*"></asp:Label></label>                                       
                                    <label class="input">  
                                        <asp:TextBox ID="txtPurchaseDate" runat="server" title="Branch Name" TextMode="Date"></asp:TextBox> 
                                             <b class="tooltip tooltip-top-right">
                                        <i class="fa fa-arrow-circle-down"></i> Enter Purchase Date</b>                                   

                                    </label>                                    
                                </div>         
                                                 
                            </div>   
                            <div class="row">
                                <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">                                   
                                    <label class="label">Purchase No.<asp:Label ID="Label17" ForeColor="Red" runat="server" Text="*"></asp:Label></label>                                       
                                    <label class="input">  
                                         <asp:TextBox ID="txtPurchaseNo" runat="server" title="Contact Person"></asp:TextBox>                      
                                        <b class="tooltip tooltip-top-right">
                                        <i class="fa fa-arrow-circle-down"></i> Enter Purchase No.</b> 
                                    </label>
                                </div>  
                                <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">                                  
                                    <label class="label">Invoice No.<asp:Label ID="Label16" ForeColor="Red" runat="server" Text="*"></asp:Label></label>                                       
                                    <label class="input">  
                                      <asp:TextBox ID="txtInvoiceNo" runat="server" title="Contact Number"></asp:TextBox>                                    
                                        <b class="tooltip tooltip-top-right">
                                        <i class="fa fa-arrow-circle-down"></i> Enter Invoice No.</b> 
                                    </label>                                  
                                </div>     
                            </div>   
                            <div class="row">
                                
                                <div class="col-xs-12 col-sm-3 col-md-3 col-lg-2">
                                    <asp:CheckBox ID="chkBulkUpload" OnCheckedChanged="chkBulkUpload_CheckedChanged" AutoPostBack="true" runat="server" />&nbsp Bulk Upload
                                            
                                </div>
                                <div class="col-xs-12 col-sm-3 col-md-3 col-lg-2">
                                    <asp:RadioButton ID="rbSIMPurchase" GroupName="Purchase" runat="server" Visible="false" Checked="true" OnCheckedChanged="rbSIMPurchase_CheckedChanged" AutoPostBack="true"  />&nbsp <%--Blank SIM Purchase--%>
                                </div>
                                
                                <div class="col-xs-12 col-sm-3 col-md-3 col-lg-2">
                                    <%--<asp:CheckBox ID="chkBlankSIMPurchase" OnCheckedChanged="chkBlankSIMPurchase_CheckedChanged" AutoPostBack="true" runat="server" />--%>
                                    <asp:RadioButton ID="rbMobileSIMPurchase" GroupName="Purchase" Visible="false"  runat="server" OnCheckedChanged="rbMobileSIMPurchase_CheckedChanged" AutoPostBack="true" Checked="false" />&nbsp <%--Mobile SIM Purchase  --%>      
                                    
                                            
                                </div>
                                <div class="col-xs-12 col-sm-3 col-md-3 col-lg-6">
                                   <%-- <label class="label">Format</label>--%>
                                    <%--<ul id="sparks" style="float: left!important;">
							        <li class="sparks-info">
								        <a target="_blank" href="Format/BlankSIMPurchase.csv" title="BlankSIMPurchase"> 
                                	   <span><i class="fa fa-arrow-circle-o-down"></i></span> Blank SIM Format</a>
								        
							        </li>
                                        <li class="sparks-info">
								        <a target="_blank" href="Format/MobileSIMPurchase.csv" title="MobileSIMPurchase"> 
                                	   <span><i class="fa fa-arrow-circle-o-down"></i></span> Mobile SIM Format</a>
							        </li>
							 
						        </ul>--%>
                               </div>
                            </div>
                            <div class="row" id="DivSIM" runat="server" visible="true">
                                
                                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                    <div class="row">
                                        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                        <br />
                                            
                                            <label class="label" style="color:black;">SIM Purchase</label> 
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                        <hr />
                                        </div>
                                    </div>
                                    <div class="row" >
                                        <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">                                   
                                            <label class="label">SIM #<asp:Label ID="Label15" ForeColor="Red" runat="server" Text="*"></asp:Label></label>                                       
                                            <label class="input">  
                                               <asp:TextBox ID="txtSIM" runat="server" title="Branch Type" Text="8919601" MaxLength="19" onblur="ValidateSimNumber();"></asp:TextBox>                                      
                                                <b class="tooltip tooltip-top-right">
                                                <i class="fa fa-arrow-circle-down"></i> Enter SIM #*</b> 
                                            </label>                                    
                                        </div>                             
                             
                                        <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">                                    
                                                                 
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">                                    
                                            <label class="label">PIN<asp:Label ID="Label14" ForeColor="Red" runat="server" Text="*"></asp:Label></label>                                       
                                            <label class="input"> 
                                                <asp:TextBox ID="txtPIN" runat="server" title="Address 2"></asp:TextBox> 
                                       
                                                <b class="tooltip tooltip-top-right">
                                                <i class="fa fa-arrow-circle-down"></i> Enter PIN</b>                                   

                                            </label>                                  
                                        </div>
                                        <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">                                    
                                            <label class="label">PUK<asp:Label ID="Label13" ForeColor="Red" runat="server" Text="*"></asp:Label></label>                                       
                                            <label class="input">  
                                                <asp:TextBox ID="txtPUK" runat="server" title="Street"></asp:TextBox>
                                       
                                                <b class="tooltip tooltip-top-right">
                                                <i class="fa fa-arrow-circle-down"></i> Enter PUK</b>                                   

                                            </label>                                  
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">                                    
                                            <label class="label">SIM Type<asp:Label ID="Label12" ForeColor="Red" runat="server" Text="*"></asp:Label></label>                                       
                                            <label class="input">  
                                                <asp:DropDownList  class="text-area" ID="ddlSIMTypeForSIM" runat="server">
                                                    <%--<asp:ListItem Value="0" Text="-Select SIM Type-"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="Normal"></asp:ListItem>
                                                    <asp:ListItem Value="2" Text="Nano"></asp:ListItem>
                                                    <asp:ListItem Value="3" Text="Micro"></asp:ListItem>--%>
                                                </asp:DropDownList> 
                                            </label>                                     
                                        </div>
                                        <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">
                                            <label class="label"></label>                                       
                                            </br>
                                                <asp:Button ID="btnADDNewRowForSIM" runat="server" Text="ADD New Row" OnClick="btnADDNewRowForSIM_Click" OnClientClick="javascript:return validateNewRowForSim();" height="30px" Width="150px"  />                               
                                        </div>
                                    </div>   
                                    
                                   
                                        
                                     
                                </div>
                               
                                 
                                 
                                                            
                            </div>      
                            
                            <div class="row" id="DivBulkUpload" runat="server" visible="false">
                                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                    <div class="row">
                                            <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">                                    
                                                <label class="label">Bulk Upload</label>                                       
                                                <label class="input"> <%--<i class="icon-append fa fa-lock"></i>--%>
                                                    <%--<asp:TextBox ID="TextBox1" runat="server" title="Address 2"></asp:TextBox>--%>
                                           <asp:FileUpload ID="fuBulkUpload" runat="server" />
                                                    <%--<b class="tooltip tooltip-top-right">
                                                    <i class="fa fa-lock txt-color-teal"></i> Enter PIN</b> --%>                                  

                                                </label>                     
                                            </div>
                                            <div class="col-xs-12 col-sm-6 col-md-6 col-lg-4">
                                                <label class="label"></label>                                       
                                                </br>
                                                    <asp:Button ID="btnUploadFile" runat="server" OnClick="btnUploadFile_Click" Text="Upload" height="20px" Width="100px"  />                               

                                             
                                            
                                            </div>
                                        <div class="col-xs-12 col-sm-6 col-md-6 col-lg-2 " style="float: right!important; color: #7E8184!important;">
                                            <label class="label">&nbsp;</label>
                                         
                                            <ul id="Ul1" style="float: right!important;">
							                <li id="liSim" runat="server" visible="false" class="sparks-info">
								                <a target="_blank" href="Format/BlankSIMPurchase.csv" style="float: right!important; color: #7E8184!important;" title="Blank SIM Bulk Upload Format"> 
                                	            <span><i class="fa fa-arrow-circle-o-down"></i></span> Blank SIM Format</a>
								        
							                </li>
                                                <li id="liMobile" runat="server" visible="false" class="sparks-info">
								                <a target="_blank" href="Format/MobileSIMPurchase.csv" style="float: right!important; color: #7E8184!important;" title="Mobile SIM Bulk Upload Format"> 
                                	            <span><i class="fa fa-arrow-circle-o-down"></i></span> Mobile SIM Format</a>
							                </li>
							 
						                    </ul>
                                       </div>
                            </div>
                                </div>
                            </div>
                            <div class="row" id="DivSIMRepeater" runat="server" visible="true">
                                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                     <div class="row">
                                        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                        <div class="table-responsive" style="max-height: 300px; overflow-y: auto;">										
											
                                            <asp:Repeater ID="RepeaterSIMPurchase" runat="server" OnItemCommand="RepeaterSIMPurchase_ItemCommand">
                                         <HeaderTemplate >
                                              <table class="table table-bordered"  id="dataTables-example">
                                                <thead>
                                                    <tr>
                                                        <th>Sim Number</th>
                                                        <th>PIN</th>
                                                        <th>PUK</th>
                                                        <th>SIM Type</th>
                                                        <th>Action</th>
                                                                                                                
                                                    </tr> 
                                                <tbody>             
                                              </HeaderTemplate>
                                       
                                              <ItemTemplate> 
                                                <tr>
                                                    <td>
                                                    <asp:Label runat="server" ID="Label7" text='<%# Eval("SimNo") %>' />
                                                        
                                                    </td> 
                                                    <td>
                                                    <asp:Label runat="server" ID="Label2" text='<%# Eval("PIN") %>' />
                                                        
                                                    </td> 
                                                    <td>
                                                    <asp:Label runat="server" ID="Label3" text='<%# Eval("PUK") %>' />
                                                        
                                                    </td> 
                                                    <td>
                                                    <asp:Label runat="server" ID="Label4" text='<%# Eval("SIMType") %>' />
                                                    <asp:HiddenField ID="hdnSIMTypeID" runat="server" Value='<%# Bind("SIMTypeID") %>' />
                                                        
                                                    </td> 
                                                    <td>
                                                        <asp:LinkButton ID="lbtnRemove" runat="server" CommandName="Delete" >Remove</asp:LinkButton>
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
                                </div>
                            </div>
                            <div class="row" id="DivMobile" runat="server" visible="false">
                                 <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                    <div class="row">
                                        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                        <br />
                                            <label class="label" style="color:black;">Mobile Purchase</label> 
                                            
                                        </div>
                                        
                                    </div>
                                    <div class="row">
                                        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                        <hr />
                                        </div>
                                    </div>
                                     <div class="row" >
                                        <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">                                   
                                            <label class="label">Mobile #<asp:Label ID="Label6" ForeColor="Red" runat="server" Text="*"></asp:Label></label>                                       
                                            <label class="input">  
                                               <asp:TextBox ID="txtMobileforMobileSIM" runat="server" title="Branch Type"></asp:TextBox>                                      
                                                <b class="tooltip tooltip-top-right">
                                                <i class="fa fa-arrow-circle-down"></i> Enter Mobile #*</b> 
                                            </label>                                    
                                        </div>                             
                             
                                        <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">                                 
                                            <label class="label">SIM #<asp:Label ID="Label8" ForeColor="Red" runat="server" Text="*"></asp:Label></label>                                       
                                            <label class="input">  
                                               <asp:TextBox ID="txtSIMforMobileSIM" runat="server" title="Branch Type"></asp:TextBox>                                      
                                                <b class="tooltip tooltip-top-right">
                                                <i class="fa fa-arrow-circle-down"></i> Enter SIM #*</b> 
                                            </label>            
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">                                   
                                            <label class="label">PIN<asp:Label ID="Label9" ForeColor="Red" runat="server" Text="*"></asp:Label></label>                                       
                                            <label class="input"> 
                                                <asp:TextBox ID="txtPINforMobileSIM" runat="server" title="Address 2"></asp:TextBox>
                                       
                                                <b class="tooltip tooltip-top-right">
                                                <i class="fa fa-arrow-circle-down"></i> Enter PIN</b>                                   

                                            </label>                                  
                                        </div>
                                        <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">                                    
                                            <label class="label">PUK<asp:Label ID="Label10" ForeColor="Red" runat="server" Text="*"></asp:Label></label>                                       
                                            <label class="input">  
                                                <asp:TextBox ID="txtPUKforMobileSIM" runat="server" title="Street"></asp:TextBox>
                                       
                                                <b class="tooltip tooltip-top-right">
                                                <i class="fa fa-arrow-circle-down"></i> Enter PUK</b>                                   

                                            </label>                                  
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">                                  
                                            <label class="label">SIM Type<asp:Label ID="Label11" ForeColor="Red" runat="server" Text="*"></asp:Label></label>                                       
                                            <label class="input">  
                                                <asp:DropDownList  class="text-area" ID="ddlSIMTypeforMobileSIM" runat="server">
                                                    <%--<asp:ListItem Value="0" Text="-Select SIM Type-"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="Normal"></asp:ListItem>
                                                    <asp:ListItem Value="2" Text="Nano"></asp:ListItem>
                                                    <asp:ListItem Value="3" Text="Micro"></asp:ListItem>--%>
                                                </asp:DropDownList> 
                                            </label>                                 
                                        </div>
                                        <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">
                                            <label class="label"></label>                                       
                                            </br>
                                                <asp:Button ID="btnADDNewRowforMobileSIM" runat="server" Text="ADD New Row" OnClick="btnADDNewRowforMobileSIM_Click" OnClientClick="javascript:return validateNewRowForMobileSim();" height="30px" Width="150px"  />                               

                                             
                                            
                                        </div>
                                        

                                    </div> 
                                    
                                    
                                </div>
                            </div>
                            <div class="row" id="DivMobileRepeater" runat="server" visible="false">
                                 <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                     <div class="row">
                                        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                        <div class="table-responsive" style="max-height: 300px; overflow-y: auto;">										
											
                                            <asp:Repeater ID="RepeaterMobileSIMPurchase" runat="server" OnItemCommand="RepeaterMobileSIMPurchase_ItemCommand">
                                         <HeaderTemplate >
                                              <table class="table table-bordered"  id="dataTables-example">
                                                <thead>
                                                    <tr>
                                                        <th>Mobile Number</th>
                                                        <th>SIM Number</th>
                                                        <th>PIN</th>
                                                        <th>PUK</th>
                                                        <th>SIM Type</th>
                                                        <th>Action</th>
                                                                                                                
                                                    </tr> 
                                                <tbody>             
                                              </HeaderTemplate>
                                       
                                              <ItemTemplate> 
                                                <tr>
                                                    <td>
                                                    <asp:Label runat="server" ID="Label5" text='<%# Eval("MobileNo") %>' />
                                                                                                      
                                                    </td>  
                                                    <td>
                                                    <asp:Label runat="server" ID="Label1" text='<%# Eval("SIMNo") %>' />
                                                                                                      
                                                    </td>  
                                                    
                                                    <td>
                                                    <asp:Label runat="server" ID="Label2" text='<%# Eval("PIN") %>' />
                                                        
                                                    </td> 
                                                    <td>
                                                    <asp:Label runat="server" ID="Label3" text='<%# Eval("PUK") %>' />
                                                        
                                                    </td> 
                                                    <td>
                                                    <asp:Label runat="server" ID="Label4" text='<%# Eval("SIMType") %>' />
                                                    <asp:HiddenField ID="hdnSIMTypeID" runat="server" Value='<%# Bind("SIMTypeID") %>' />
                                                        
                                                    </td> 
                                                    <td>
                                                        <asp:LinkButton ID="lbtnRemove" runat="server" CommandName="Delete">Remove</asp:LinkButton>
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
                                 </div>
                            </div>

                             <%--<div class="row">
                                <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">                                    
                                                                                                       
                                </div>
                                                 
                            </div>  --%>   
                            
                        </div>
                
                <footer class="boxBtnsubmit">
                     <asp:Button ID="btnResetAll" runat="server" Text="RESET" OnClick="btnResetAll_Click" class="btn btn-primary"   />
                     <asp:Button ID="btnSavePurchase" runat="server" Text="SAVE" OnClick="btnSavePurchase_Click"  class="btn btn-primary" OnClientClick="javascript:return validate();"  />              
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
    <div id="popupdiv" class="loading" title="Basic modal dialog" style="display:nonefont-family:Arial;font-size: 10pt; border: 2px solid #67CFF5;text-align: -webkit-center;">
        
      <b style="text-align:center!important;"> Please Wait...........</b>
        <br />
        <br />
        <img src="img/loader.gif" alt="" style="text-align:center!important;" />
    </div>

    <script src="js/jquery-2.1.1.js"></script>
    <script src="js/plugin/chosen/chosen.jquery.js"></script>
     <script type="text/javascript">
     
        $(document).ready(function () {
            var keyDown = false, ctrl = 17, vKey = 86, Vkey = 118;

            $("#ContentPlaceHolder1_txtSIM").keydown(function (e) {
                if (e.keyCode == ctrl) keyDown = true;
            }).keyup(function (e) {
                if (e.keyCode == ctrl) keyDown = false;
            });

            $("#ContentPlaceHolder1_txtSIM").on('keypress', function (e) {
                if (!e) var e = window.event;
                if (e.keyCode > 0 && e.which == 0) return true;
                if (e.keyCode) code = e.keyCode;
                else if (e.which) code = e.which;
                var character = String.fromCharCode(code);
                if (character == '\b' || character == ' ' || character == '\t') return true;
                if (keyDown && (code == vKey || code == Vkey)) return (character);
                else return (/[0-9]$/.test(character));
            }).on('focusout', function (e) {
                var $this = $(this);
                $this.val($this.val().replace(/[^0-9]/g, ''));
            }).on('paste', function (e) {
                var $this = $(this);
                setTimeout(function () {
                    $this.val($this.val().replace(/[^0-9]/g, ''));
                }, 5);
            });
        });


       
            </script>

    

</asp:Content>
