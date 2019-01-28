 

<%@ Page Language="C#"  MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="NetworkReplace.aspx.cs" Inherits="ENK.NetworkReplace"%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function validate() {

            if (document.getElementById("<%=txtSIMNumber.ClientID%>").value == "") {
            alert("Current SIM Number Can't be Blank");
            document.getElementById("<%=txtSIMNumber.ClientID%>").focus();
                return false;
            }
           
            return true;
        }
        function validateNewRowForSim() {
            if (document.getElementById("<%=txtSIMNumber.ClientID%>").value == "") {
                alert("Current SIM Number Can't be Blank");
                document.getElementById("<%=txtSIMNumber.ClientID%>").focus();
            return false;
        }
            return true;
        }
        function ConfirmOnDelete() {

            if (document.getElementById('<%=ddlNetwork.ClientID%>').selectedIndex == 0) {
                alert('Please Select Network');
                document.getElementById("<%=ddlNetwork.ClientID%>").focus();
                    return false;
                }

            if (confirm("Are you sure change Network ?") == true) {
               
                return true;
            }
            else
                return false;
        }
        </script>
    <link type="text/css" rel="stylesheet" href="http://code.jquery.com/ui/1.10.3/themes/smoothness/jquery-ui.css" />
<script type="text/javascript" src="http://code.jquery.com/jquery-1.8.2.js"></script>
<script type="text/javascript" src="http://code.jquery.com/ui/1.10.3/jquery-ui.js"></script>
    <script type="text/javascript">
        function ShowProgress() {
            $("#popupdiv").dialog({
                title: "",
                width: 430,
                height: 250,
                modal: false,
                buttons: {
                    Close: function () {
                        $(this).dialog('close');
                    }
                }
            });
            $(".ui-dialog-buttonpane").hide();
            $(".ui-dialog-titlebar").hide();
            $(".ui-widget-overlay").css({
                background: "#0F0101 url(images/ui-bg_flat_0_aaaaaa_40x100.png) repeat-x",
                opacity: .3
                /*background: "#454545",
                opacity: ".9 !important",
                filter: "Alpha(Opacity=50)"*/
            });
            return true;
        };

        $('form').live("submit", function () {
            ShowProgress();
        });



</script>

    
    <link href="css/plugins/chosen/chosen.css" rel="stylesheet"/>
    <style>
       .chosen-container-single .chosen-single {
    position: relative;
    display: block;
    overflow: hidden;
    padding: 0 0 0 2px;
    height: 34px;
    border: 1px solid #aaa;
    border-radius: 5px;
    background-color: #fff;
    background: -webkit-gradient(linear, 50% 0%, 50% 100%, color-stop(20%, #ffffff), color-stop(50%, #f6f6f6), color-stop(52%, #eeeeee), color-stop(100%, #f4f4f4));
    background: -webkit-linear-gradient(top, #ffffff 20%, #f6f6f6 50%, #eeeeee 52%, #f4f4f4 100%);
    background: -moz-linear-gradient(top, #ffffff 20%, #f6f6f6 50%, #eeeeee 52%, #f4f4f4 100%);
    background: -o-linear-gradient(top, #ffffff 20%, #f6f6f6 50%, #eeeeee 52%, #f4f4f4 100%);
    background: linear-gradient(top, #ffffff 20%, #f6f6f6 50%, #eeeeee 52%, #f4f4f4 100%);
    background-clip: padding-box;
    box-shadow: 0 0 3px white inset, 0 1px 1px rgba(0, 0, 0, 0.1);
    color: #444;
    text-decoration: none;
    white-space: nowrap;
}
    </style>
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
							<i class="fa fa-lg fa-fw fa-pencil-square-o"></i> 
								Change Network  
							<span> >
								Change Network  						</span>						</h1>
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
									<h2>Change Network</h2>
								</header>

								<!-- widget div-->
								<div>
									<!-- widget edit box -->
									<div class="jarviswidget-editbox">
										<!-- This area used as dropdown edit box -->
									</div>
									<!-- end widget edit box -->

									<!-- widget content -->
                                    <div id="login-form"  class="smart-form client-form">
                    
                        <div class="box-input"  >        
                                     
                            <div class="row">
                                 <asp:HiddenField ID="hdnPurchaseID" runat="server" />
                                <div  class="col-md-8">
                                  <div id="div1" runat="server" class="col-md-12">                                  
                                <label class="label">Network</label>                                       
                                <label class="input"> 
                                <asp:DropDownList ID="ddlNetwork" class="form-control chosen-select text-area" runat="server" >
                                     
                                </asp:DropDownList>                            
                                </label>                                  
                                </div>
                                <div id="DivMobile" runat="server" class="col-md-7">                                    
                                    <label class="label">Current SIM Number<asp:Label ID="Label19" ForeColor="Red" runat="server" Text="*"></asp:Label></label>                                       
                                    <label class="input">  
                                        <asp:TextBox ID="txtSIMNumber" runat="server"  OnTextChanged="txtSIMNumber_TextChanged"  title="Branch Name" AutoPostBack="false"></asp:TextBox> 
                                             <b class="tooltip tooltip-top-right">
                                        <i class="fa fa-arrow-circle-down"></i> Enter Current SIM Number</b>  
                                    </label>                  
                                             
                                </div>

                                    <div  id="DivBulkUpload" runat="server" visible="false">
                              
                                            <div class="col-md-5" >                                    
                                                <label class="label">Bulk Upload</label>                                       
                                                <label class="input"> <%--<i class="icon-append fa fa-lock"></i>--%>
                                                    <%--<asp:TextBox ID="TextBox1" runat="server" title="Address 2"></asp:TextBox>--%>
                                           <asp:FileUpload ID="fuBulkUpload" runat="server" />
                                                    <%--<b class="tooltip tooltip-top-right">
                                                    <i class="fa fa-lock txt-color-teal"></i> Enter PIN</b> --%>                                  

                                                </label>                     
                                            </div>
                                            <div class="col-md-2" style="margin-top: 10px;">
                                                                                   
                                                </br>
                                                    <asp:Button ID="btnUploadFile" runat="server" class="btn btn-primary" OnClick="btnUploadFile_Click" Text="Upload"  height="29px" width="71px" />                               

                                             
                                            
                                            </div>
                                        <div class="col-md-3" >
                                            <br />
                                            <div id="liSim" runat="server" visible="false" class="sparks-info">
								                <a target="_blank" href="Format/BlankSIMNetworkChange.csv" style="float: right!important; color: #7E8184!important;" title="Blank SIM Bulk Upload Format"> 
                                	            <span><i class="fa fa-arrow-circle-o-down"></i></span> Blank SIM Format</a>
								        
							              </div>
                                       </div>
                                        
                           
                            </div>

                                       <div class="col-md-3">
                                         <br />
                                                <asp:Button ID="btnADDNewRowForSIM" runat="server"  class="btn btn-primary" Text="ADD New Sim" OnClick="btnADDNewRowForSIM_Click" OnClientClick="javascript:return validateNewRowForSim();" height="30px" Width="90px"  />                               
                                      
                                               </div>
                                      <div class="col-md-2">
                                          <br />
                                    <asp:CheckBox ID="chkBulkUpload" OnCheckedChanged="chkBulkUpload_CheckedChanged" AutoPostBack="true" runat="server" />&nbsp Bulk Upload
                                            
                                </div>
                                    </div>
                                <div  class="col-md-4">
                                   
                               
                               

                                    </div>
                                
                                    
                                    <div class="col-md-12">
											
                               
                                        <div class="table-responsive" style="max-height: 300px; overflow-y: auto;">										
											<asp:Repeater ID="rptErrot" runat="server"  Visible="false" >
                                            <HeaderTemplate >
                                              <table class="table table-bordered"  id="dataTables-example">
                                                <thead>
                                                    <tr>
                                                        <th>SIM Number</th>
                                                      <th>Error</th>
                                                    </tr>
                                                     
                                                <tbody>             
                                              </HeaderTemplate>
                                       
                                              <ItemTemplate> 
                                                <tr>
                                                    <td>
                                                    <asp:Label runat="server" ID="Label1" text='<%# Eval("SimNo") %>' />
                                                                                                      
                                                    </td>  
                                                    <td >
                                                        <span style="background-color:black; padding:2px;color:#fff;">
                                                    <asp:Label runat="server" ID="Label2" text='<%# Eval("Remark") %>' />    </td> 
                                                     </span>
                                                </tr>          
                                              
                                              </ItemTemplate>  
                                              <FooterTemplate>
                                               </thead> 
                                               </table>
                                               </tbody>
                                               
                                              </FooterTemplate>
                                    </asp:Repeater>
                                            <asp:Repeater ID="RepeaterMobileSIMPurchase" runat="server" OnItemCommand="RepeaterMobileSIMPurchase_ItemCommand">
                                            <HeaderTemplate >
                                              <table class="table table-bordered"  id="dataTables-example">
                                                <thead>
                                                    <tr>
                                                        <th>SIM Number</th>
                                                      <th>Action</th>
                                                    </tr>
                                                     
                                                <tbody>             
                                              </HeaderTemplate>
                                       
                                              <ItemTemplate> 
                                                <tr>
                                                    <td>
                                                    <asp:Label runat="server" ID="Label1" text='<%# Eval("SIMNo") %>' />
                                                                                                      
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
                
                <footer class="boxBtnsubmit">
                 
                     <asp:Button ID="btnReplace" runat="server" Text="Update" OnClick="btnReplace_Click" class="btn btn-primary"  OnClientClick="return ConfirmOnDelete();"  /> 
                       <asp:Button ID="btnResetAll" runat="server" Text="Reset" OnClick="btnResetAll_Click" class="btn btn-primary"   />             
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



        var config = {
            '.chosen-select': {},
            '.chosen-select-deselect': { allow_single_deselect: true },
            '.chosen-select-no-single': { disable_search_threshold: 10 },
            '.chosen-select-no-results': { no_results_text: 'Oops, nothing found!' },
            '.chosen-select-width': { width: "100%" }
        }
        for (var selector in config) {
            $(selector).chosen(config[selector]);
        }
    </script>
</asp:Content>


