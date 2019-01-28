<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="DistributorView.aspx.cs" Inherits="ENK.TempleteViewMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<link type="text/css" rel="stylesheet" href="http://code.jquery.com/ui/1.10.3/themes/smoothness/jquery-ui.css" />
        <link type="text/css" rel="stylesheet" href="https://cdn.datatables.net/1.10.19/css/jquery.dataTables.min.css" />
     <link type="text/css" rel="stylesheet" href="https://cdn.datatables.net/buttons/1.5.2/css/buttons.dataTables.min.css" />
    <link type="text/css" rel="stylesheet" href="https://cdn.datatables.net/fixedcolumns/3.2.6/css/fixedColumns.dataTables.min.css" />
    <%--<script type="text/javascript" src="http://code.jquery.com/jquery-1.8.2.js"></script>--%>
    <script type="text/javascript" src="http://code.jquery.com/ui/1.10.3/jquery-ui.js"></script>
     <link href="css/plugins/chosen/chosen.css" rel="stylesheet" />
    <style>
        div.dt-buttons {
            position: relative;
            float: left;
            margin-bottom: -22px;
        }
        .chosen-container-single .chosen-single
        {
            position: relative;
            display: block;
            overflow: hidden;
            padding: 0 0 0 8px;
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
            line-height: 24px;
        }
        .jarviswidget {
            margin: 0 !important;
        }
        .smart-form section {
            margin-bottom:  0 !important;
        }
    </style>
    <script type="text/javascript">
        function ShowProgress() {
            $("#popupdiv").dialog({
                title: "WARNING",
                width: 650,
                height: 550,
                modal: false,
                buttons: {
                    Close: function () {
                        $(this).dialog('close');
                    }
                }
            });
            $(".ui-dialog-buttonpane").hide();
            /*$(".ui-dialog-titlebar").hide();*/
            $(".ui-widget-overlay").css({
                /*background: "#0F0101 url(images/ui-bg_flat_0_aaaaaa_40x100.png) repeat-x",
                  opacity: .3 */
                background: rgb(99, 127, 239),
                opacity: .3,
                filter: Alpha(Opacity = 50)
            });
            return true;
        };

        /*  $('form').live("submit", function () {
            ShowProgress();
        });*/



    </script>

    <style type="text/css">
       
        .modal {
            top: 0;
            left: 0;
            z-index: 99;
            opacity: 0.8;
            filter: alpha(opacity=80);
            -moz-opacity: 0.8;
            min-height: 100%;
            width: 100%;
        }

        .loading {
            font-family: Arial;
            font-size: 10pt;
            border: 5px solid #67CFF5;
            width: 550px;
            height: 150px;
            display: none;
            position: fixed;
            background-color: White;
            z-index: 999;
        }

        
    </style>
    
    <script type="text/javascript">
        debugger;
        function Search_Gridview(phrase) {
            var words = phrase.value.toLowerCase().split(' ');
            var table = document.getElementById("<%=rptDist.ClientID %>");


            var ele;
            for (var r = 1; r < table.rows.length; r++) {
                ele = table.rows[r].innerHTML.replace(/<[^>]+>/g, '');
                var displayStyle = 'none';
                for (var i = 0; i < words.length; i++) {
                    if (ele.toLowerCase().indexOf(words[i]) >= 0)
                        displayStyle = '';
                    else {
                        displayStyle = 'none';
                        break;
                    }
                }
                table.rows[r].style.display = displayStyle;
            }
        }
        function setTarget() {
            debugger;
            document.forms[0].target = "_blank";
        }
    </script>

    <script>         
          function isNumberKey(evt) {
              var charCode = (evt.which) ? evt.which : evt.keyCode;
              if (charCode != 46 && charCode > 31
                && (charCode < 48 || charCode > 57))
                  return false;

              return true;
          }
    </script>
    

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="main" role="main" style="margin-top: 0px!important;">

        <!-- RIBBON -->

        <!-- END RIBBON -->

        <!-- MAIN CONTENT -->
        <div id="content">
            

            <!-- widget grid -->
            <section id="widget-grid" class="">

                <!-- row -->
                <div class="row">
                    <!-- NEW WIDGET START -->
                    <article class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                        <!-- Widget ID (each widget will need unique ID)-->
                        <div class="jarviswidget jarviswidget-color-blueDark" id="wid-id-0" data-widget-colorbutton="false" data-widget-editbutton="false" data-widget-togglebutton="false" data-widget-deletebutton="false" data-widget-fullscreenbutton="false" data-widget-custombutton="false" data-widget-collapsed="false" data-widget-sortable="false">
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
                           

                            <!-- widget div-->
                            <div>

                               


                                <div id="login-form" class="smart-form client-form">



                                    <main class="cd-main-content">
                                    <div class="cd-tab-filter-wrapper">
                                        <div class="cd-tab-filter">
                                            <ul class="cd-filters">
                                               
                                                <li class="filter">
												<a href="#0" >
                                                    <h2 class="titl"><strong>Seller</strong></h2>
                                                </a>
												</li>

                                                 <li style="float: right; display: inherit; top: -60px; position: relative;">
                                                    <a href="AddDistributor.aspx" title="New" id="sparks" runat="server" style="color:blue;">
                                                        <span><i class="fa fa-file"></i></span> New
                                                    </a>
                                                </li>
                                            </ul>
                                            <!-- cd-filters -->
                                        </div>
                                        <!-- cd-tab-filter -->
                                    </div>
                                    <!-- cd-tab-filter-wrapper -->

                                    <section class="cd-gallery">

                                        <asp:TextBox ID="txtSearchgv" runat="server" placeholder="Search Text..." Class="form-control" Visible="false"
                                        onkeyup="Search_Gridview(this)"></asp:TextBox>

                                    <asp:HiddenField ID="hddnDistributorId" runat="server" />
                                    <div class="table-responsive">
                                        <asp:Repeater ID="RepeaterDistributorView" runat="server" OnItemCommand="RepeaterDistributorView_ItemCommand">
                                            <HeaderTemplate>
                                                <table class="table table-bordered" id="exampleTbl" >
                                                    <thead>
                                                        <tr>
                                                            <th>Code</th>
                                                            <th>Name</th>
                                                            <th>Phone</th>
                                                            <th>Type</th>
                                                            <th>Email</th>
                                                            <th style="display: none;">Address</th>
                                                            <th style="display: none;">City</th>
                                                            <th style="display: none;">State</th>
                                                            <th style="display: none;">Zip Code</th>
                                                            <th>Website</th>
                                                            <th>Tax ID</th>
                                                            <th>Account Balance</th>
                                                            <th>Parent Distributor</th>
                                                            <th>No. Of Blank Sims</th>
                                                            <th>No. Of Activations MTD</th>
                                                            <th>Created DateTime</th>
                                                            <th>Modified DateTime</th>
                                                            <th>Action</th>
                                                            <th>Reset Password</th>
                                                            <th>Tax Document</th>
                                                            <th>Reseller Certificate</th>
                                                            <th id="thhold" runat="server">Hold Account</th>
                                                            <th id="thtopup" runat="server" class="topuphead">TopUp Option</th>
                                                            <%--<th class="uk-width-2-10"><asp:Label runat="server" ID="tdInfoHeader" Visible="true">TopUp Option</asp:Label></th>--%>
                                                            <th style="display: none">Api Status</th>
                                                            <th>Ip Address</th>
                                                        </tr>
                                                        <tbody>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td>
                                                        <asp:Label runat="server" ID="Label1" Text='<%# Eval("distributorCode") %>' />
                                                        <asp:HiddenField ID="hdnDistributortId" runat="server" Value='<%# Bind("distributorID") %>' />
                                                        <asp:HiddenField ID="hdnParentId" runat="server" Value='<%# Bind("parent") %>' />
                                                        <asp:HiddenField ID="hdnClientTypeId" runat="server" Value='<%# Bind("companyType") %>' />
                                                        <asp:HiddenField ID="hiddeholdstatus" runat="server" Value='<%# Bind("Holdstatus") %>' />
                                                        <asp:HiddenField ID="hiddenDocFile" runat="server" Value='<%# Bind("Document") %>' />
                                                        <asp:HiddenField ID="hiddenCertifile" runat="server" Value='<%# Bind("Certificate") %>' />


                                                    </td>
                                                    <td>
                                                        <asp:Label runat="server" ID="distributorName" Text='<%# Eval("distributorName") %>' />
                                                    </td>
                                                    <td>
                                                        <asp:Label runat="server" ID="contactNo" Text='<%# Eval("contactNo") %>' />
                                                    </td>
                                                    <td>
                                                        <asp:Label runat="server" ID="CompanyTypeName" Text='<%# Eval("CompanyTypeName") %>' />
                                                    </td>
                                                    <td>
                                                        <asp:Label runat="server" ID="emailID" Text='<%# Eval("emailID") %>' />
                                                    </td>
                                                    <td style="display: none;">
                                                        <asp:Label runat="server" ID="address" Text='<%# Eval("address") %>' />
                                                    </td>
                                                    <td style="display: none;">
                                                        <asp:Label runat="server" ID="city" Text='<%# Eval("city") %>' />
                                                    </td>
                                                    <td style="display: none;">
                                                        <asp:Label runat="server" ID="state" Text='<%# Eval("state") %>' />
                                                    </td>
                                                    <td style="display: none;">
                                                        <asp:Label runat="server" ID="Label4" Text='<%# Eval("zip") %>' />
                                                    </td>
                                                    <td>
                                                        <asp:Label runat="server" ID="webSiteName" Text='<%# Eval("webSiteName") %>' />
                                                    </td>
                                                    <td>
                                                        <asp:Label runat="server" ID="Label2" Text='<%# Eval("EIN") %>' />
                                                    </td>
                                                    <td>
                                                        <asp:Label runat="server" ID="balanceAmount" Text='<%# Eval("balanceAmount") %>' />
                                                    </td>
                                                    <td>
                                                        <asp:Label runat="server" ID="parentDistributor" Text='<%# Eval("parentDistributor") %>' />
                                                    </td>

                                                    <td>
                                                        <asp:Label runat="server" ID="lblNoOfBlankSim" Text='<%# Eval("NoOfBlankSim") %>' />
                                                    </td>
                                                    <td>
                                                        <asp:Label runat="server" ID="lblNoOfActivation" Text='<%# Eval("NoOfActivation") %>' />
                                                    </td>
                                                    <td>
                                                        <asp:Label runat="server" ID="lblcreatedDT" Text='<%# Eval("CreatedDate") %>' />
                                                    </td>
                                                    <td>
                                                        <asp:Label runat="server" ID="lblmodifiedDT" Text='<%# Eval("ModifiedDate") %>' />
                                                    </td>
                                                    <td>
                                                        <asp:ImageButton ID="lnkView" runat="server" CausesValidation="false" CommandName="View" ToolTip="View Distributor" CommandArgument='<%#Eval("distributorID") %>' ImageUrl="~/img/1399984583_view.png" />
                                                        &nbsp;
                                                    <asp:ImageButton ID="lnkEdit" runat="server" CausesValidation="false" CommandName="RowEdit" ToolTip="Edit Distributor" CommandArgument='<%#Eval("distributorID") %>' ImageUrl="~/img/Pencil-icon.png" />
                                                        <asp:ImageButton ID="lnkDeactivate" class="pull-right" runat="server" CausesValidation="false" CommandName="RowDelete" ToolTip="Deactivate Distributor" CommandArgument='<%#Eval("distributorID") %>' ImageUrl="~/img/icon-close-round-16.png" OnClientClick="return confirm('Are you sure want to \n deactivate distributor ?');" />
                                                        &nbsp
                                                    </td>
                                                    <td>
                                                        <asp:LinkButton ID="lnkResetPass" runat="server" CausesValidation="false" OnClick="lnkResetPass_Click" ToolTip="Reset Password" CommandArgument='<%#Eval("distributorID") %>' ImageUrl="~/img/Pencil-icon.png"><img src="img/settings.png" style="width:45% !important;" /> </asp:LinkButton>

                                                    </td>
                                                    <td>
                                                        <%-- <asp:LinkButton ID="lnlViewTaxDocument" runat="server" CausesValidation="false" ToolTip="View Tax Document" CommandName="ViewDocument" >View Tax Document </asp:LinkButton>
                                                        --%>   <%--<a href="https://docs.google.com/viewerng/viewer?url=<%# Eval("Document")%>" title='View Tax Documnet' target='_blank' class="badge badge-danger" data-toggle="tooltip" runat="server" id="href"  ><i class="fa fa-eye"></i></a>--%>
                                                        <%--<asp:HyperLink ID="hpLinkView" runat="server" CssClass="badge badge-danger" data-toggle="tooltip" title="View Tax Document"  Target="_blank" CommandName="ViewDocument"><i class="fa fa-eye" style="color:white;"></i></asp:HyperLink>--%>
                                                        <asp:LinkButton ID="hpLinkView" runat="server" CssClass="badge badge-danger" data-toggle="tooltip" title="View Tax Document" OnClientClick="setTarget();" CommandName="ViewDocument"><i class="fa fa-eye" style="color:white;"></i></asp:LinkButton>

                                                        <%--<a id="ViewDocFile" runat="server" title='View Tax Documnet' target='_blank' class="badge badge-danger" data-toggle="tooltip" ><i class="fa fa-eye"></i> </a>--%>
                                                        <%-- <asp:LinkButton ID="lnkDownloadTaxDoc" runat="server" CausesValidation="false" ToolTip="Download Tax Document" CommandArgument='<%#Eval("distributorID") %>'>Download Tax Document </asp:LinkButton>--%>
                                                        <asp:HyperLink ID="hlFileName" runat="server" CssClass="badge badge-danger" data-toggle="tooltip" title="Download Tax Document" NavigateUrl='<%# Eval("Document")%>' Target="_blank" CommandArguement='<%# Eval("Document") %>' CommandName="Select"><i class="fa fa-download" style="color:white;"></i></asp:HyperLink>

                                                    </td>


                                                    <td>
                                                        <asp:LinkButton ID="LbtnCertificate" runat="server" CssClass="badge badge-danger" data-toggle="tooltip" title="View Tax Certificate" OnClientClick="setTarget();" CommandName="ViewCertificate"><i class="fa fa-eye" style="color:white;"></i></asp:LinkButton>
                                                        <asp:HyperLink ID="h2FileName" runat="server" CssClass="badge badge-danger" data-toggle="tooltip" title="Download Certificate" NavigateUrl='<%# Eval("Certificate")%>' Target="_blank" CommandArguement='<%# Eval("Certificate") %>' CommandName="Select"><i class="fa fa-download" style="color:white;"></i></asp:HyperLink>

                                                    </td>


                                                    <td>
                                                        <asp:Button ID="btnHoldStatus" runat="server" Text="Hold" CommandArgument='<%#Eval("distributorID") %>' OnClick="btnHoldStatus_Click" />
                                                    </td>

                                                    <td id="btntopuptd" runat="server" class="btn-popup topuphead">

                                                        <button id="ChangeStatus" type="button" class="btn btn-default btntop" />
                                                        TopUp</button>
                                                                                          
                                                    </td>

                                                    <td style="display: none">
                                                        <asp:ImageButton ID="btnApiStatus" runat="server" CausesValidation="false" CommandName="ApiStatus" ToolTip="Edit Api Status" CommandArgument='<%#Eval("distributorID") %>' ImageUrl="~/img/Pencil-icon.png" />

                                                    </td>
                                                    <td>
                                                        <asp:ImageButton ID="btnIpAddress" runat="server" CausesValidation="false" CommandName="IpAddress" ToolTip="Edit Ip Address" CommandArgument='<%#Eval("distributorID") %>' ImageUrl="~/img/Pencil-icon.png" />

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
                                    </section>
                                    <!-- cd-gallery -->

                                    <div class="cd-filter">
                                     
                                    

                                            

                                         <div class="cd-filter-block col-md-4 ">
                                                <div class="cd-filter-content">
                                               <%-- <label class="label">LEVEL 1</label>--%>
                                                     <asp:Label ID="LEVEL1" runat="server" class="label" Text="LEVEL 1"></asp:Label>
                                                <label class="input">
                                                    <asp:DropDownList ID="ddlLevel1" class="chosen-select text-area" runat="server" OnSelectedIndexChanged="ddlLevel1_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                </label>
                                            </div>
                                         </div>
                                            <div class="cd-filter-block col-md-4 ">
                                                <div class="cd-filter-content">
                                               <%-- <label class="label">LEVEL 2</label>--%>
                                                     <asp:Label ID="LEVEL2" runat="server" class="label" Text="LEVEL 2"></asp:Label>
                                                <label class="input">
                                                    <asp:DropDownList ID="ddlLevel2" runat="server" class="chosen-select text-area" OnSelectedIndexChanged="ddlLevel2_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                </label>

                                            </div>
                                            </div>
                                            <div class="cd-filter-block col-md-4 ">
                                                <div class="cd-filter-content">
                                               <%-- <label class="label">LEVEL 3</label>--%>
                                                     <asp:Label ID="LEVEL3" runat="server" class="label" Text="LEVEL 3"></asp:Label>
                                                <label class="input">
                                                    <asp:DropDownList ID="ddlLevel3" runat="server" class="chosen-select text-area" OnSelectedIndexChanged="ddlLevel3_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                </label>
                                            </div></div>

                                            <div class="cd-filter-block col-md-4 ">
                                                <div class="cd-filter-content">
                                                <%--<label class="label">LEVEL 4</label>--%>
                                                     <asp:Label ID="LEVEL4" runat="server" class="label" Text="LEVEL 4"></asp:Label>
                                                <label class="input">
                                                    <asp:DropDownList ID="ddlLevel4" runat="server" class="chosen-select text-area" OnSelectedIndexChanged="ddlLevel4_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                </label>

                                            </div></div>

                                        
                                            <div class="cd-filter-block col-md-4 ">
                                                <div class="cd-filter-content">
                                                <%--<label class="label">LEVEL 5</label>--%>
                                                     <asp:Label ID="LEVEL5" runat="server" class="label" Text="LEVEL 5"></asp:Label>
                                                <label class="input">
                                                    <asp:DropDownList ID="ddlLevel5" runat="server" class="chosen-select text-area" OnSelectedIndexChanged="ddlLevel5_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                </label>

                                            </div>
                                                </div>

                                          <div class="cd-filter-block col-md-4 ">
                                                <div class="cd-filter-content">
                                                <%--<label class="label">LEVEL 5</label>--%>
                                                     <asp:Label ID="lblSearch" runat="server" class="label" Text="Search"></asp:Label>
                                                <label class="input">
                                                    <asp:TextBox ID="txtSearch" runat="server" ></asp:TextBox>
                                                </label>

                                            </div>
                                                </div>
                                        <%--akash--%>
                                 
                                            <div class="col-md-3" style="display: none;">
                                                <label class="label">Select Modified/Created Date</label>
                                                <asp:DropDownList ID="ddlDate" runat="server" class="chosen-select text-area">

                                                    <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                                    <asp:ListItem Text="ModifiedDate" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="CreatedDate" Value="2"></asp:ListItem>
                                                </asp:DropDownList>
                                               <%-- <asp:Button ID="Button1" ClientIDMode="Static" runat="server" Text="Button" onclick="Button1_Click" onClientClick="return false" />--%>
                                            </div>
                                           

                                           <div class="clearfix"></div>
                                        <div class="row">
                                            <div class="cd-filter-block col-md-6 ">
                                                <div class="cd-filter-content">
                                                <label class="label">From Date<asp:Label ID="Label18" ForeColor="Red" runat="server" Text="*"></asp:Label></label>
                                                <label class="input">
                                                    <i class="icon-append fa fa-calendar"></i>
                                                    <asp:TextBox ID="txtFromDate" runat="server" title="Branch Name" class="datepicker" AutoCompleteType="Enabled" data-dateformat='dd-M-yy'></asp:TextBox>
                                                </label>
                                            </div>
                                           </div>
                                            <div class="cd-filter-block col-md-6 ">
                                                <div class="cd-filter-content">

                                                <label class="label">To Date<asp:Label ID="Label2" ForeColor="Red" runat="server" Text="*"></asp:Label></label>
                                                <label class="input">
                                                    <i class="icon-append fa fa-calendar"></i>
                                                    <asp:TextBox ID="txtToDate" runat="server" title="Branch Name" class="datepicker" AutoCompleteType="Enabled" data-dateformat='dd-M-yy'></asp:TextBox>
                                                </label>
                                            </div>
                                                </div>
                                             </div>
                                            <div class="cd-filter-block col-md-6 " style="display:none;">
                                                <div class="cd-filter-content">
                                                <label class="label">
                                                    <asp:Label ID="Label3" ForeColor="Red" runat="server" Text=""></asp:Label></label>
                                                <asp:Button ID="btnsearch" runat="server" Text="Search" CssClass="btn btn-sm btn-warning" Visible="false" />

                                            </div>
                                            </div>
                                       
                                      
                                            <div class="cd-filter-block col-md-6 ">
                                                <div class="cd-filter-content">
                                                <label class="label">Tax Document</label>
                                                <asp:DropDownList ID="ddlTaxDocument" runat="server" class="chosen-select text-area">
                                                    <asp:ListItem Text="All" Value="0"></asp:ListItem>
                                                    <asp:ListItem Text="Tax Document Uploaded" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="Not Uploaded" Value="2"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                           </div>
                                            <div class="cd-filter-block col-md-6 ">
                                                <div class="cd-filter-content">
                                                <label class="label">Reseller Certificate</label>
                                                <asp:DropDownList ID="ddlResellerCertificate" runat="server" class="chosen-select text-area">
                                                    <asp:ListItem Text="All" Value="0"></asp:ListItem>
                                                    <asp:ListItem Text="Certificate Uploaded" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="Not Uploaded" Value="2"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                           </div>
                                            <div class="cd-filter-block col-md-12 ">
                                                <div class="cd-filter-content pull-right">
                                                <asp:Button ID="btnGetReport" runat="server" class="btn btn-primary btn-sm" Text="Get Report" OnClick="btnGetReport_Click" />
                                            </div>
                                           </div>
                                            <div class="cd-filter-block col-md-6 ">
                                                <div class="cd-filter-content">
                                                <asp:Button ID="btnExportToExcel" runat="server" Text="Export To Excel" Visible="false" CssClass="btn btn-sm btn-warning" />
                                            </div>
                                           </div>
                                            <div class="cd-filter-block col-md-6 " style="display: none;">
                                                <div class="cd-filter-content">
                                                <asp:HiddenField ID="pageindex" runat="server" />
                                                <asp:Button ID="btnGetindexData" runat="server" Text="getIndexData" CssClass="btn btn-primary btn-sm" Style="Display: none;" />

                                            </div>
                                        </div>

                                        <a href="#0" class="cd-close">Close</a>
                                    </div>
                                    <!-- cd-filter -->

                                    <a href="#0" class="cd-filter-trigger">Filters</a>
                                </main>





                                    
                                </div>
                                <!-- widget content -->
                               
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
    <div id="popupdiv" class="loading" title="Basic modal dialog" style="display: nonefont-family:Arial; font-size: 10pt; border: 2px solid #67CFF5; text-align: -webkit-center;">

        <div class="widget-body">
            <div>
                <label style="font-weight: 600;" class="">Are you sure want to delete it's chid Distributor</label>

            </div>
            <div class="table-responsive" style="max-height: 300px; overflow-y: auto;">
                <asp:Repeater ID="rptDist" runat="server">
                    <HeaderTemplate>
                        <table class="table table-bordered" id="dataTables-example">
                            <thead>
                                <tr>
                                    <th>Distributor Name</th>
                                </tr>
                                <tbody>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td>
                                <asp:Label runat="server" ID="Label7" Text='<%# Eval("DistributorName") %>' />
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
            <footer class="boxBtnsubmit">
                <a>
                    <asp:LinkButton ID="linkDeactivate" class="btn btn-primary btn-sm" runat="server" Text="Yes" OnClick="linkDeactivate_Click" /></a>
                <a>
                    <asp:LinkButton ID="linkBack" class="btn btn-primary btn-sm" runat="server" Text="No" OnClick="linkBack_Click" /></a>




            </footer>

        </div>


    </div>
    <div style="display: none; z-index: 2; border: 1px solid rgba(37, 36, 36, 0.5); background: rgba(248, 248, 248, 0.9); position: fixed; top: 42vh; padding: 18px 15px; left: 44vw; width: 493px; border-radius: 6px"
        id="DivHold">


        <div class="col-md-12 text-left">
            <strong>Hold Reason</strong>
            <asp:TextBox ID="txtHoldReason" Style="width: 450px; height: 72px;" TextMode="MultiLine" runat="server"></asp:TextBox>
            <br />

        </div>
        <div class="col-md-12 text-right">
            <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-primary btn-sm" OnClick="btnSubmit_Click" Text="Submit" />
            <asp:Button ID="btnClose" runat="server" CssClass="btn btn-primary btn-sm" Text="Close" OnClientClick="resetfield();" />

        </div>

    </div>

    <script type="text/javascript">
        function HyperLinkClick() {
            debugger;

            document.getElementById('DivHold').style.display = "block";

        }
    </script>


    <div>
        <asp:HiddenField ID="HiddenField1" runat="server" Value="" ClientIDMode="Static" />
    </div>
    <div style="display: none; z-index: 2; border: 1px solid rgba(37, 36, 36, 0.5); background: #cccccc; position: fixed; top: 42vh; padding: 18px 15px; left: 30vw; width: 600px; border-radius: 6px"
        id="TopupOption">


        <div class="row">
            <div class="col-md-2 col-sm-2 form-group">
                <span>Min TopUp</span>
            </div>
            <div class="col-md-4 col-sm-4">
                <asp:TextBox ID="txtmintopup" CssClass="form-control" runat="server" onkeypress="return isNumberKey(event)" ClientIDMode="Static"></asp:TextBox>
            </div>
            <div class="col-md-2 col-sm-2 form-group">
                <span>Max TopUp</span>
            </div>
            <div class="col-md-4 col-sm-4">
                <asp:TextBox ID="txtmaxtopup" CssClass="form-control" runat="server" onkeypress="return isNumberKey(event)" ClientIDMode="Static"></asp:TextBox>
            </div>

        </div>

        <div class="row">

               <div class="col-md-2 col-sm-2 form-group">
                <span>Paypal Commission Type</span>
            </div>
            <div class="col-md-4 col-sm-4">
               <%-- <asp:TextBox ID="txtcommission" CssClass="form-control" runat="server"></asp:TextBox>--%>
                <asp:DropDownList ID="ddlPaypaltax" runat="server" Width="170px">
                    <asp:ListItem Text="Paypal Text Type" Value="" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="Percentage" Value="Percentage"></asp:ListItem>
                    <asp:ListItem Text="Fixed Amount" Value="Fixed Amount"></asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="col-md-2 col-sm-2 form-group">
                <span>Paypal Commission</span>
            </div>
            <div class="col-md-4 col-sm-4">
                <asp:TextBox ID="txtfixed" CssClass="form-control" onkeypress="return isNumberKey(event)" ClientIDMode="Static" runat="server"></asp:TextBox>
            </div>
         

        </div>


        <script type="text/javascript">
            function HyperLinkClicktopup() {
                debugger;

                document.getElementById('TopupOption').style.display = "block";

            }
        </script>


        <div class="col-md-12 text-right">
            <asp:Button ID="btnpopupsubmit" runat="server" CssClass="btn btn-primary btn-sm" OnClick="btnpopupsubmit_Click" Text="Submit" />
            <%-- <asp:Button ID="btnpopupcancel" runat="server" CssClass="btn btn-primary btn-sm" Text="Close" OnClientClick="resetfield();" />--%>
            <div class="btn btn-default btn-sm btncloses">Close</div>
        </div>

    </div>





    <script src="js/jquery-2.1.1.js"></script>
    <script src="js/plugin/chosen/chosen.jquery.js"></script>
   
    <!-- PAGE RELATED PLUGIN(S) -->
    <script src="js/plugin/jquery-form/jquery-form.min.js"></script>
         <script src="https://cdn.datatables.net/1.10.19/js/jquery.dataTables.min.js"></script>
      <script src="https://cdn.datatables.net/fixedcolumns/3.2.6/js/dataTables.fixedColumns.min.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.5.2/js/dataTables.buttons.min.js"></script>
<script src="https://cdn.datatables.net/buttons/1.5.2/js/buttons.flash.min.js"></script>
<script src="https://cdnjs.cloudflare.com/ajax/libs/jszip/3.1.3/jszip.min.js"></script>
<script src="https://cdnjs.cloudflare.com/ajax/libs/pdfmake/0.1.36/pdfmake.min.js"></script>
<script src="https://cdnjs.cloudflare.com/ajax/libs/pdfmake/0.1.36/vfs_fonts.js"></script>
<script src="https://cdn.datatables.net/buttons/1.5.2/js/buttons.html5.min.js"></script>
<script src="https://cdn.datatables.net/buttons/1.5.2/js/buttons.print.min.js"></script>
    <script>var $j = jQuery.noConflict(true);</script>
  
   
    <script>



        $j(document).ready(function () {
            $j('#exampleTbl').DataTable({


                "scrollY": 340,
                "scrollX": true,
                "searching": true,
                "paging": false,
                "info": false,
                "autoWidth": true,
                dom: 'Bfrtip',
                buttons: [
        {
            extend: 'excel', text: 'Export to Excel'
        }
        ]
            });
        });

    </script>
    <script type="text/javascript">
        var sessionValue = '<%= Session["LoginID"] %>';
        if (sessionValue == '1') {       
            $j('.topuphead').css("display", "block");
        }
        else {

            $j('.topuphead').css("display", "none");
        }
    </script>

    <script type="text/javascript">
        $j(window).bind("load", function () {
            $j(".btntop").click(function () {

                var item = $(this).closest("tr").find("input[type=hidden]").val();
                var item1 = $("#HiddenField1").val(item);
                $j("#TopupOption").css("display", "block");
                debugger;
                $("[id*='Button1']").click();
            });
        });

        $j(document).ready(function () {
            $j(".btncloses").click(function () {
                $j("#TopupOption").css("display", "none");

            });
        });

    </script>


    <script>
        $(document).ready(function () {
            console.log($().jquery); // This prints v1.4.2
            console.log($j().jquery); // This prints v1.9.1
        });
    </script>
    

    <script src="js/plugin/chosen/chosen.jquery.js"></script>

    <!-- PAGE RELATED PLUGIN(S) -->
    <script src="js/plugin/jquery-form/jquery-form.min.js"></script>

    <%--<script src="https://code.jquery.com/jquery-3.3.1.js"></script>--%>




     <script type="text/javascript">

       
        var config = {
            '.chosen-select': {},
            '.chosen-select-deselect': { allow_single_deselect: true },
            '.chosen-select-no-single': { disable_search_threshold: 10 },
            '.chosen-select-no-results': { no_results_text: 'Oops, nothing found!' },
            '.chosen-select-width': { width: "95%" }
        }
        for (var selector in config) {
            $j(selector).chosen(config[selector]);
        }
    </script>

        <%--<script type="text/javascript">
            $(function () {
                debugger;
                $.ajax({
                    type: 'GET',
                    url: 'DistributorView.aspx/TopUpDetail',

                    success: function (msg) {
                        alert(msg);
                    }
                });

            });
    </script>--%>
</asp:Content>
