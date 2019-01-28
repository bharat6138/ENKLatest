<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="TestAPI.aspx.cs" Inherits="ENK.TestAPI" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="main" role="main" style="margin-top: 0px!important;" >
            <%-- <asp:HiddenField ID="hddnActivation" runat="server" />--%>
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
								Activate SIM						</span>						</h1>
					</div>
					<div class="col-xs-12 col-sm-7 col-md-7 col-lg-8">
                        
					</div>
				</div>

				<!-- widget grid -->
				<section id="widget-grid" class="">

					<!-- row -->
					<div class="row">
                        <asp:Label ID="lblAPIResponse" runat="server" Text="Label"></asp:Label>
                        <asp:Button ID="btnAPI" runat="server" Text="API" OnClick="btnAPI_Click" />
                        </div>
                    </div>
        </div>
    </s
</asp:Content>
