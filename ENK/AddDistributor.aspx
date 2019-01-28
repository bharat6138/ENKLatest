<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="AddDistributor.aspx.cs" Inherits="ENK.AddDistributor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link type="text/css" rel="stylesheet" href="http://code.jquery.com/ui/1.10.3/themes/smoothness/jquery-ui.css" />
    <script type="text/javascript" src="http://code.jquery.com/jquery-1.8.2.js"></script>
    <script type="text/javascript" src="http://code.jquery.com/ui/1.10.3/jquery-ui.js"></script>
    <style>
        .field-icon {
            float: right;
            margin-left: -25px;
            margin-top: -25px;
            position: relative;
            z-index: 2;
        }

        .container {
            padding-top: 50px;
            margin: auto;
        }
    </style>
    <script type="text/javascript">

        function validate() {

            if (document.getElementById("<%=txtDistributorName.ClientID%>").value == "") {
                alert("Distributor Name Can't be Blank");
                document.getElementById("<%=txtDistributorName.ClientID%>").focus();
                return false;
            }



            if (document.getElementById("<%=txtContactPerson.ClientID%>").value == "") {
                alert("Contact Person Can't be Blank");
                document.getElementById("<%=txtContactPerson.ClientID%>").focus();
                return false;
            }



            if (document.getElementById("<%=txtContactNo.ClientID%>").value == "") {
                alert("Contact No Can't be Blank");
                document.getElementById("<%=txtContactNo.ClientID%>").focus();
                return false;
            }

            if (document.getElementById("<%=txtEmailID.ClientID%>").value == "") {
                alert("Email Can't be Blank");
                document.getElementById("<%=txtEmailID.ClientID%>").focus();
                return false;
            }

            var email = document.getElementById("<%=txtEmailID.ClientID%>");
            var filter = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;

            if (!filter.test(email.value)) {
                alert('Please Enter a valid email address');
                email.focus;
                return false;
            }

            if (document.getElementById("<%=txtaddress.ClientID%>").value == "") {
                alert("Address Can't be Blank");
                document.getElementById("<%=txtaddress.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=txtCity.ClientID%>").value == "") {
                alert("City Name Can't be Blank");
                document.getElementById("<%=txtCity.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=txtState.ClientID%>").value == "") {
                alert("State Name Can't be Blank");
                document.getElementById("<%=txtState.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=txtzip.ClientID%>").value == "") {
                alert("Zip Code Can't be Blank");
                document.getElementById("<%=txtzip.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=ddlCountry.ClientID%>").selectedIndex == 0) {
                alert("Please select Country");
                document.getElementById("<%=ddlCountry.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=ddlTarrifGroup.ClientID%>").selectedIndex == 0) {
                alert("Please select TarrifGroup");
                document.getElementById("<%=ddlTarrifGroup.ClientID%>").focus();
                return false;
            }

            var taxcheck = '<%= ConfigurationManager.AppSettings["TAX_Mandatory"].ToString() %>';
            if (taxcheck == "1") {
                if (document.getElementById("<%=txtEin.ClientID%>").value == "") {
                    alert("Please insert TaxId");
                    document.getElementById("<%=txtEin.ClientID%>").focus();
                    return false;
                }
            }

            var taxdoccheck = '<%= ConfigurationManager.AppSettings["TAX_Doc_Mandatory"].ToString() %>';
            if (taxdoccheck == "1") {
                if (document.getElementById("<%=fileUploadDocument.ClientID%>").files.length == 0) {
                    alert("Please attach Tax Document file");
                    return false;
                }
            }


            if (document.getElementById("<%=txtUserID.ClientID%>").value == "") {
                alert("Please insert UserID");
                document.getElementById("<%=ddlTarrifGroup.ClientID%>").focus();
                return false;
            }




            var prodDis = Math.floor(document.getElementById('<%=txtLycaPerRecharge.ClientID%>').value);
            if (prodDis >= 100) {
                alert("Lyca Tariff % Recharge can not exceed to 100 percent");
                document.getElementById("<%=txtLycaPerRecharge.ClientID%>").focus();
                return false;
            }

            var prodDis = Math.floor(document.getElementById('<%=txth20PerRecharge.ClientID%>').value);
            if (prodDis >= 100) {
                alert("H20 Tariff % Recharge can not exceed to 100 percent");
                document.getElementById("<%=txth20PerRecharge.ClientID%>").focus();
                return false;
            }

            var prodDis = Math.floor(document.getElementById('<%=txtEasyGoPerRecharge.ClientID%>').value);
            if (prodDis >= 100) {
                alert("EasyGo Tariff % Recharge can not exceed to 100 percent");
                document.getElementById("<%=txtEasyGoPerRecharge.ClientID%>").focus();
                return false;
            }

            return true;
        }

        function JScriptConfirmationUpdatePopupBox() {
            alert("Distributor Updated Successfully");
            window.location.href = "DistributorView.aspx";
        }


        function JScriptConfirmationPopupBox() {
            alert("Distributor Added Successfully");
            window.location.href = "DistributorView.aspx";
        }


        function onlynumericDecimal(evt, cnt) {

            var keycode;
            var textbox = cnt;
            if (window.event) {
                event = window.event;
            }

            if (event.keyCode) //For IE
            {
                keycode = event.keyCode;

            }
            else if (evt.Which) {
                keycode = event.Which;  // For FireFox

            }
            else {
                keycode = event.charCode; // Other Browser

            }

            if (keycode != 8) //if the key is the backspace key
            {

                if ((keycode >= 48 && keycode <= 57) || keycode == 46) {
                    //alert(textbox.value);
                    var tmp;
                    if (event.keyCode == 46) {
                        tmp = textbox.value + ".";
                    }
                    if (tmp.split(".").length > 2) {
                        textbox.value = tmp.trim().substr(0, tmp.length - 1);
                        //alert("Only one decimal allowed.");
                        //return false;
                    }
                    else {

                        //return true;
                    }

                }
                else {
                    alert("Only numbers allowed.");

                    return false; // disable key press
                }


            }

        }//end

        function onlynumeric(evt, cnt) {

            var keycode;
            var textbox = cnt;
            if (window.event) {
                event = window.event;
            }
            //alert(textbox.value);
            if (event.keyCode) //For IE
            {
                keycode = event.keyCode;

            }
            else if (evt.Which) {
                keycode = event.Which;  // For FireFox

            }
            else {
                keycode = event.charCode; // Other Browser

            }

            if (keycode != 8) //if the key is the backspace key
            {

                if (keycode >= 48 && keycode <= 57) {
                    //alert(textbox.value);


                }
                else {
                    alert("Only numbers allowed.");

                    return false; // disable key press
                }


            }

        }//end
        function checkEmail() {

            var email = document.getElementById('txtEmailID');
            var filter = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;

            if (!filter.test(email.value)) {
                alert('Please Enter a valid email address');
                email.focus;
                return false;
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

    <link href="css/plugins/chosen/chosen.css" rel="stylesheet" />
    <style>
        .chosen-container-single .chosen-single {
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
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div id="main" role="main" style="margin-top: 0px!important;">

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
							New	Seller					</span></h1>
                </div>
                <div class="col-xs-12 col-sm-6 col-md-6 col-lg-8">
                    <footer class="boxBtnsubmit">
                        <ul id="sparks" class="">
                            <li style="border-left: 0px!important;" id="liUpdate" runat="server" class="sparks-info">
                                <asp:Button ID="btnUpdateUp" runat="server" Style="float: right; height: 31px; margin: 10px 0 0 5px; padding: 0 22px; font: 300 15px/29px 'Open Sans',Helvetica,Arial,sans-serif; cursor: pointer;" Text="UPDATE" class="btn btn-primary" Visible="true" OnClick="btnUpdateUp_Click" OnClientClick="javascript:return validate();" />

                            </li>
                            <li style="border-left: 0px!important;" id="liSave" runat="server" visible="false" class="sparks-info">
                                <asp:Button ID="btnSaveUp" runat="server" Style="float: right; height: 31px; margin: 10px 0 0 5px; padding: 0 22px; font: 300 15px/29px 'Open Sans',Helvetica,Arial,sans-serif; cursor: pointer;" Text="SAVE" class="btn btn-primary" Visible="true" OnClick="btnSaveUp_Click" OnClientClick="javascript:return validate();" />

                            </li>
                            <li style="border-left: 0px!important;" id="liReset" runat="server" visible="false" class="sparks-info">
                                <asp:Button ID="btnResetUp" runat="server" Style="float: right; height: 31px; margin: 10px 0 0 5px; padding: 0 22px; font: 300 15px/29px 'Open Sans',Helvetica,Arial,sans-serif; cursor: pointer;" Text="RESET" class="btn btn-primary" Visible="true" OnClick="btnResetUp_Click" />

                            </li>
                            <li style="border-left: 0px!important;" id="liBack" runat="server" class="sparks-info">
                                <asp:Button ID="btnBackUp" runat="server" Text="BACK" Style="float: right; height: 31px; margin: 10px 0 0 5px; padding: 0 22px; font: 300 15px/29px 'Open Sans',Helvetica,Arial,sans-serif; cursor: pointer;" class="btn btn-primary" Visible="true" OnClick="btnBackUp_Click" />
                            </li>
                        </ul>
                    </footer>
                </div>
            </div>

            <!-- widget grid -->
            <section id="widget-grid" class="">

                <!-- row -->
                <div class="row">
                    <!-- NEW WIDGET START -->
                    <article class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                        <!-- Widget ID (each widget will need unique ID)-->
                        <div class="jarviswidget jarviswidget-color-blueDark" id="wid-id-0" data-widget-colorbutton="false" data-widget-editbutton="false" data-widget-togglebutton="false" data-widget-deletebutton="false" data-widget-fullscreenbutton="false" data-widget-custombutton="false" data-widget-collapsed="false" data-widget-sortable="false">
                            <header>
                                <span class="widget-icon"><i class="fa fa-table"></i></span>
                                <h2>New Seller</h2>
                            </header>
                            <div>
                                <!-- widget edit box -->
                                <div class="jarviswidget-editbox">
                                    <!-- This area used as dropfa-arrow-circle-down edit box -->

                                </div>
                                <div class="tabs-container">
                                    <div id="login-form" class="smart-form client-form">

                                        <ul class="nav nav-tabs" id="myTab" style="background-color: gray;">
                                            <li id="cert" class="active"><a data-toggle="tab" href="#tab-1">Basic</a></li>
                                            <li style="display: none" id="RationSurveytab" class=""><a data-toggle="tab" href="#tab-2">Lyca Tariff</a></li>
                                            <li style="display: none" id="Photostab" class=""><a data-toggle="tab" href="#tab-3">H2O Tariff</a></li>
                                            <li style="display: none" id="completetab" class=""><a data-toggle="tab" href="#tab-4">EasyGo Tariff</a></li>
                                            <li style="display: none" id="Ultra" class=""><a data-toggle="tab" href="#tab-5">Ultra Tariff</a></li>
                                            <li style="display: none" id="ATT" class=""><a data-toggle="tab" href="#tab-6">AT&T Tariff</a></li>

                                        </ul>


                                        <div class="tab-content">
                                            <div id="tab-1" class="tab-pane active">



                                                <div class="box-input">
                                                    <div class="row">
                                                        <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">
                                                            <div id="divCode" runat="server" visible="false" class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                                                <label class="label">Seller Code</label>
                                                                <label class="input">
                                                                    <asp:TextBox ID="txtCode" runat="server" title="Distributor Code"></asp:TextBox>
                                                                    <asp:HiddenField ID="hddDistributorID" runat="server" />
                                                                    <asp:HiddenField ID="hddnDistributorType" runat="server" />
                                                                    <asp:HiddenField ID="hddnParent" runat="server" />
                                                                    <b class="tooltip tooltip-top-right">
                                                                        <i class="fa fa-arrow-circle-down txt-color-red"></i>Enter Seller Code</b>

                                                                </label>
                                                            </div>
                                                            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                                                <label class="label">Seller Name</label>
                                                                <label class="input">
                                                                    <asp:TextBox ID="txtDistributorName" runat="server" title="Chainage Code"></asp:TextBox>

                                                                    <b class="tooltip tooltip-top-right">
                                                                        <i class="fa fa-arrow-circle-down"></i>Enter Seller Name</b>

                                                                </label>
                                                            </div>
                                                            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                                                <label class="label">Contact Person</label>
                                                                <label class="input">
                                                                    <asp:TextBox ID="txtContactPerson" runat="server" title="Contact Person"></asp:TextBox>

                                                                    <b class="tooltip tooltip-top-right">
                                                                        <i class="fa fa-arrow-circle-down"></i>Enter Contact Person</b>

                                                                </label>
                                                            </div>
                                                            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                                                <label class="label">Website</label>
                                                                <label class="input">
                                                                    <asp:TextBox ID="txtWebSiteName" runat="server" title="WebSite Name"></asp:TextBox>

                                                                    <b class="tooltip tooltip-top-right">
                                                                        <i class="fa fa-arrow-circle-down"></i>Enter WebSite Name</b>

                                                                </label>
                                                            </div>


                                                        </div>


                                                        <%--<div class="col-xs-12 col-sm-6 col-md-6 col-lg-6" >
                                   <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12"> 
                                       <label class="label">Tariff</label> 
                                     
                                       <div class="table-responsive" style="max-height: 237px; overflow-y: auto;" >
                                       
                                        <asp:Repeater ID="RepeaterTariff" runat="server">
                                         <HeaderTemplate >
                                              <table class="table table-bordered"  id="dataTables-example">
                                                <thead>
                                                    <tr>
                                                        <th> <asp:CheckBox ID="chkAll" OnCheckedChanged="chkAll_OnCheckedChanged" AutoPostBack="true" runat="server" /></th>
                                                        <th>Code</th>
                                                        <th>Description</th>
                                                        <th>Dealer Cost</th>
                                                        <th>Default</th>                                                         
                                                    </tr> 
                                                <tbody>             
                                              </HeaderTemplate>
                                       
                                              <ItemTemplate> 
                                                <tr>
                                                     
                                                    <td>
                                                        <asp:CheckBox ID="CheckBox2" runat="server" />
                                                    </td>
                                                    <td>
                                                    <asp:Label runat="server" ID="Label1" text='<%# Eval("TariffCode") %>' />
                                                    <asp:HiddenField ID="hdnTariffId" runat="server" Value='<%# Bind("ID") %>' />                                                    
                                                    </td>  
                                                    <td>
                                                    <asp:Label runat="server" ID="Label7" text='<%# Eval("Description") %>' />
                                                    </td>
                                                     <td>
                                                    <asp:TextBox ID="txtRental" runat="server" text='<%# Eval("Rental") %>'></asp:TextBox>
                                                     
                                                    </td> 
                                                    <td>
                                                        <asp:RadioButton ID="RadioButton1" runat="server" OnCheckedChanged="RadioButton1_OnCheckedChanged" AutoPostBack="true"   />
                                                      
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
                                </div>--%>
                                                        <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">
                                                            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">

                                                                <label class="label">Address</label>
                                                                <label class="input">
                                                                    <asp:TextBox ID="txtaddress" runat="server" title="Address"></asp:TextBox>

                                                                    <b class="tooltip tooltip-top-right">
                                                                        <i class="fa fa-arrow-circle-down"></i>Enter Address</b>

                                                                </label>
                                                            </div>

                                                            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                                                <label class="label">City</label>
                                                                <label class="input">
                                                                    <asp:TextBox ID="txtCity" runat="server" title="City"></asp:TextBox>

                                                                    <b class="tooltip tooltip-top-right">
                                                                        <i class="fa fa-arrow-circle-down"></i>Enter City</b>

                                                                </label>
                                                            </div>
                                                            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                                                <label class="label">Zip</label>
                                                                <label class="input">
                                                                    <asp:TextBox ID="txtzip" runat="server" title="Zip"  onkeypress="javascript:return onlynumeric(event,this);"></asp:TextBox>

                                                                    <b class="tooltip tooltip-top-right">
                                                                        <i class="fa fa-arrow-circle-down"></i>Enter Zip</b>

                                                                </label>
                                                            </div>
                                                            <div id="divAcntBalance" runat="server" visible="true" class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                                                <label class="label">Account Balance</label>
                                                                <label class="input">
                                                                    <asp:TextBox ID="txtAccountBalance" runat="server" title="Account Balance" onkeypress="javascript:return onlynumeric(event,this);"></asp:TextBox>

                                                                    <b class="tooltip tooltip-top-right">
                                                                        <i class="fa fa-arrow-circle-down"></i>Enter Account Balance</b>

                                                                </label>
                                                            </div>

                                                        </div>

                                                    </div>
                                                    <div class="row">
                                                        <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">
                                                            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                                                <label class="label">Contact No.</label>
                                                                <label class="input">
                                                                    <asp:TextBox ID="txtContactNo" runat="server" title="Contact No." onkeypress="javascript:return onlynumeric(event,this);"></asp:TextBox>

                                                                    <b class="tooltip tooltip-top-right">
                                                                        <i class="fa fa-arrow-circle-down"></i>Enter Contact No.</b>

                                                                </label>
                                                            </div>
                                                            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                                                <label class="label">Email ID</label>
                                                                <label class="input">
                                                                    <asp:TextBox ID="txtEmailID" runat="server" title="Email ID"></asp:TextBox>

                                                                    <b class="tooltip tooltip-top-right">
                                                                        <i class="fa fa-arrow-circle-down"></i>Enter Email ID</b>

                                                                </label>
                                                            </div>

                                                            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                                                <label class="label">Tax ID<span id="spanTaxID" runat="server" visible="false" style="color: red">*</span></label>
                                                                <label class="input">
                                                                    <asp:TextBox ID="txtEin" runat="server" title="Email ID"></asp:TextBox>

                                                                    <b class="tooltip tooltip-top-right">
                                                                        <i class="fa fa-arrow-circle-down"></i>Enter Tax ID</b>

                                                                </label>
                                                            </div>
                                                            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12" style="display: none">
                                                                <label class="label">Social Security Number</label>
                                                                <label class="input">
                                                                    <asp:TextBox ID="txtSSN" runat="server" title="Email ID"></asp:TextBox>

                                                                    <b class="tooltip tooltip-top-right">
                                                                        <i class="fa fa-arrow-circle-down"></i>Enter Social Security Number</b>

                                                                </label>
                                                            </div>

                                                            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                                                <div style="margin-bottom: 2%;">
                                                                    <label class="label">User ID</label>
                                                                    <label class="input">
                                                                        <asp:TextBox ID="txtUserID" runat="server" title="User ID"></asp:TextBox>

                                                                        <b class="tooltip tooltip-top-right">
                                                                            <i class="fa fa-arrow-circle-down"></i>User ID</b>

                                                                    </label>
                                                                </div>
                                                                <div>
                                                                    <label class="label">Plan Group<span style="color: red">*</span></label>
                                                                    <label class="input">
                                                                        <asp:DropDownList class="chosen-select text-area" ID="ddlTarrifGroup" runat="server">
                                                                        </asp:DropDownList>

                                                                    </label>
                                                                </div>
                                                                <div style="margin-top:10px;">
                                                                            <label class="label">Reseller Certificate <span style="color: red; font-size: 11px;"><strong>( pdf/doc/docx/jpg/jpeg/png )</strong></span><span id="span1" visible="false" runat="server" style="color: red">*</span></label>
                                                                            <label class="input">
                                                                                <asp:FileUpload ID="fileUploadCertificate" CssClass="form-control" runat="server" />
                                                                            </label>
                                                                            </div>
                        </div>


                        <div id="divPan" runat="server" visible="false" class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                                                <label class="label">Pan Number</label>
                                                                <label class="input">
                                                                    <asp:TextBox ID="txtPan" runat="server" title="Email ID"></asp:TextBox>

                                                                    <b class="tooltip tooltip-top-right">
                                                                        <i class="fa fa-arrow-circle-down"></i>Enter Pan Number</b>

                                                                </label>
                                                            </div>


                                                            <div id="divActive" runat="server" visible="false" class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                                                <label class="label">Is Distributor Active</label>
                                                                <label class="text">
                                                                    <asp:CheckBox ID="CheckBox1" runat="server" Checked="true" />
                                                                </label>
                                                            </div>


                                                        </div>
                                                        <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">
                                                            <%--<div class="col-xs-12 col-sm-12 col-md-12 col-lg-12"> 
                                    <label class="label">Address</label>                                       
                                    <label class="input">  
                                        <asp:TextBox ID="txtaddress" runat="server" title="Address"></asp:TextBox>
                                         
                                        <b class="tooltip tooltip-top-right">
                                        <i class="fa fa-arrow-circle-down"></i> Enter Address</b>                                    

                                    </label>
                                </div> 
                                
                                 <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12"> 
                                    <label class="label">City</label>                                       
                                    <label class="input">  
                                        <asp:TextBox ID="txtCity" runat="server" title="City"></asp:TextBox>
                                         
                                        <b class="tooltip tooltip-top-right">
                                        <i class="fa fa-arrow-circle-down"></i> Enter City</b>                                    

                                    </label>
                                </div>  
                                 <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12"> 
                                    <label class="label">Zip</label>                                       
                                    <label class="input">  
                                        <asp:TextBox ID="txtzip" runat="server" title="Zip" onkeypress="javascript:return onlynumeric(event,this);"></asp:TextBox>
                                         
                                        <b class="tooltip tooltip-top-right">
                                        <i class="fa fa-arrow-circle-down"></i> Enter Zip</b>                                    

                                    </label>
                                </div>--%>
                                                            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                                                <label class="label">State</label>
                                                                <label class="input">
                                                                    <asp:TextBox ID="txtState" runat="server" title="State"></asp:TextBox>

                                                                    <b class="tooltip tooltip-top-right">
                                                                        <i class="fa fa-arrow-circle-down"></i>Enter State</b>

                                                                </label>
                                                            </div>
                                                            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                                                <label class="label">Country</label>
                                                                <label class="input">
                                                                    <asp:DropDownList class="chosen-select text-area" ID="ddlCountry" runat="server">
                                                                    </asp:DropDownList>

                                                                </label>
                                                            </div>
                                                            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">

                                                                <div id="divfileDoc" runat="server" style="margin-bottom: -1.5%;">
                                                                    <label class="label">Tax Document <span style="color: red; font-size: 11px;"><strong>( pdf/doc/docx/jpg/jpeg/png )</strong></span><span id="spanMDoc" visible="false" runat="server" style="color: red">*</span></label>
                                                                    <label class="input">
                                                                        <asp:FileUpload ID="fileUploadDocument" CssClass="form-control" runat="server" />
                                                                    </label>
                                                                    <br />
                                                                </div>



                                                                <div style="margin-bottom: 2%;">
                                                                    <label class="label">Password</label>
                                                                    <label class="input">
                                                                        <asp:TextBox ID="txtPassword" runat="server" type="password" title="Password"></asp:TextBox>
                                                                        <span toggle="#txtPassword" class="fa fa-fw fa-eye field-icon toggle-password"></span>
                                                                        <b class="tooltip tooltip-top-right">
                                                                            <i class="fa fa-arrow-circle-down"></i>Enter Password</b>

                                                                    </label>
                                                                </div>





                                                                <div class="row">
                                                                    <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12" style="margin: 9px 0px;">
                                                                        <div class="col-xs-12 col-sm-4 col-md-1 col-lg-1">
                                                                            <asp:CheckBox ID="ChkSeller" runat="server" AutoPostBack="true" />

                                                                        </div>
                                                                        <div class="col-xs-12 col-sm-4 col-md-4 col-lg-4">

                                                                            <label><b>Seller Creation Rights</b></label>



                                                                        </div>

                                                                        <div class="col-xs-12 col-sm-4 col-md-1 col-lg-1">
                                                                            <asp:CheckBox ID="Chktariffgrp" runat="server" AutoPostBack="true" />

                                                                        </div>
                                                                        <div class="col-xs-12 col-sm-5 col-md-5 col-lg-5">

                                                                            <label><b>Plan Group Creation Rights</b></label>



                                                                        </div>


                                                                        <%--                                                                <div class="col-xs-12 col-sm-1 col-md-1 col-lg-1">
                                                                    
                                                                </div>--%>
                                                                    </div>
                                                                </div>

                                                               
                                                                <footer class="boxBtnsubmit">
                                                                    <asp:Button ID="btnBack" runat="server" Text="BACK" class="btn btn-primary" Visible="false" OnClick="btnBack_Click" />
                                                                    <asp:Button ID="btnReset" runat="server" Text="RESET" class="btn btn-primary" OnClick="btnReset_Click" />
                                                                    <asp:Button ID="btnSave" runat="server" Text="SAVE" class="btn btn-primary" OnClick="btnSave_Click" OnClientClick="javascript:return CheckContinue();" />
                                                                    <asp:Button ID="btnUpdate" runat="server" Text="UPDATE" class="btn btn-primary" OnClientClick="javascript:return validate();" OnClick="btnUpdate_Click" />

                                                                </footer>
                                                            </div>
                                                            <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">
                                                            </div>


                                                        </div>

                                                    </div>
                                                    <div id="tab-2" class="tab-pane" style="display: none;">
                                                        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">



                                                            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">

                                                                <label class="label">Plan</label>

                                                                <div class="table-responsive" style="max-height: 300px; overflow-y: auto;">

                                                                    <asp:Repeater ID="RepeaterTariff" runat="server">
                                                                        <HeaderTemplate>
                                                                            <table class="table table-bordered" id="dataTables-example">
                                                                                <thead>
                                                                                    <tr>
                                                                                        <th>
                                                                                            <asp:CheckBox ID="chkAll" OnCheckedChanged="chkAll_OnCheckedChanged" AutoPostBack="true" runat="server" /></th>
                                                                                        <th>Code</th>
                                                                                        <th>Description</th>
                                                                                        <th>Dealer Cost</th>
                                                                                        <th>Default</th>
                                                                                    </tr>
                                                                                    <tbody>
                                                                        </HeaderTemplate>

                                                                        <ItemTemplate>
                                                                            <tr>

                                                                                <td>
                                                                                    <asp:CheckBox ID="CheckBox2" runat="server" />
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label runat="server" ID="Label1" Text='<%# Eval("TariffCode") %>' />
                                                                                    <asp:HiddenField ID="hdnTariffId" runat="server" Value='<%# Bind("ID") %>' />
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label runat="server" ID="Label7" Text='<%# Eval("Description") %>' />
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtRental" runat="server" Text='<%# Eval("Rental") %>'></asp:TextBox>

                                                                                </td>
                                                                                <td>
                                                                                    <asp:RadioButton ID="RadioButton1" runat="server" OnCheckedChanged="RadioButton1_OnCheckedChanged" AutoPostBack="true" />

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

                                                            <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">
                                                                <div class="col-md-3 col-md-push-1">
                                                                    <label class="label" style="font-weight: bold">Lyca % Recharge</label>
                                                                    <label class="input">
                                                                        <asp:TextBox ID="txtLycaPerRecharge" MaxLength="4" onkeypress="javascript:return onlynumericDecimal(event,this);" runat="server" title="Lyca % Recharge"></asp:TextBox>

                                                                        <b class="tooltip tooltip-top-right">
                                                                            <i class="fa fa-arrow-circle-down"></i>Lyca % Recharge</b>

                                                                    </label>
                                                                </div>

                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div id="tab-3" class="tab-pane" style="display: none;">
                                                        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                                            <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">
                                                                <label class="label">Plan</label>

                                                                <div class="table-responsive" style="max-height: 330px; overflow-y: auto;">

                                                                    <asp:Repeater ID="rptH2O" runat="server">
                                                                        <HeaderTemplate>
                                                                            <table class="table table-bordered" id="dataTables-example">
                                                                                <thead>
                                                                                    <tr>
                                                                                        <th>
                                                                                            <asp:CheckBox ID="chkAll" OnCheckedChanged="chkAllH2O_OnCheckedChanged" AutoPostBack="true" runat="server" /></th>
                                                                                        <th>Code</th>
                                                                                        <th>Description</th>
                                                                                        <th>Dealer Cost</th>
                                                                                        <th>Default</th>
                                                                                    </tr>
                                                                                    <tbody>
                                                                        </HeaderTemplate>

                                                                        <ItemTemplate>
                                                                            <tr>

                                                                                <td>
                                                                                    <asp:CheckBox ID="CheckBox2" runat="server" />
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label runat="server" ID="Label1" Text='<%# Eval("TariffCode") %>' />
                                                                                    <asp:HiddenField ID="hdnTariffId" runat="server" Value='<%# Bind("ID") %>' />
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label runat="server" ID="Label7" Text='<%# Eval("Description") %>' />
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtRental" runat="server" Text='<%# Eval("Rental") %>'></asp:TextBox>

                                                                                </td>
                                                                                <td>
                                                                                    <asp:RadioButton ID="RadioButton1" runat="server" OnCheckedChanged="rdoAllH2O_OnCheckedChanged" AutoPostBack="true" />

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
                                                            <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">
                                                                <div class="col-md-3 col-md-push-1">
                                                                    <label class="label" style="font-weight: bold">H2O % Recharge</label>
                                                                    <label class="input">
                                                                        <asp:TextBox ID="txth20PerRecharge" MaxLength="4" onkeypress="javascript:return onlynumericDecimal(event,this);" runat="server" title="H2O % Recharge"></asp:TextBox>

                                                                        <b class="tooltip tooltip-top-right">
                                                                            <i class="fa fa-arrow-circle-down"></i>H2O % Recharge</b>

                                                                    </label>

                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div id="tab-4" class="tab-pane" style="display: none;">
                                                        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                                            <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">

                                                                <label class="label">Plan</label>

                                                                <div class="table-responsive" style="max-height: 330px; overflow-y: auto;">

                                                                    <asp:Repeater ID="rptEasyGo" runat="server">
                                                                        <HeaderTemplate>
                                                                            <table class="table table-bordered" id="dataTables-example">
                                                                                <thead>
                                                                                    <tr>
                                                                                        <th>
                                                                                            <asp:CheckBox ID="chkAll" OnCheckedChanged="chkAllEastGo_OnCheckedChanged" AutoPostBack="true" runat="server" /></th>
                                                                                        <th>Code</th>
                                                                                        <th>Description</th>
                                                                                        <th>Dealer Cost</th>
                                                                                        <th>Default</th>
                                                                                    </tr>
                                                                                    <tbody>
                                                                        </HeaderTemplate>

                                                                        <ItemTemplate>
                                                                            <tr>

                                                                                <td>
                                                                                    <asp:CheckBox ID="CheckBox2" runat="server" />
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label runat="server" ID="Label1" Text='<%# Eval("TariffCode") %>' />
                                                                                    <asp:HiddenField ID="hdnTariffId" runat="server" Value='<%# Bind("ID") %>' />
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label runat="server" ID="Label7" Text='<%# Eval("Description") %>' />
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtRental" runat="server" Text='<%# Eval("Rental") %>'></asp:TextBox>

                                                                                </td>
                                                                                <td>
                                                                                    <asp:RadioButton ID="RadioButton1" runat="server" OnCheckedChanged="rdoAllEasyGo_OnCheckedChanged" AutoPostBack="true" />

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
                                                            <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">
                                                                <div class="col-md-3 col-md-push-1">
                                                                    <label class="label" style="font-weight: bold">EasyGo % Recharge</label>
                                                                    <label class="input">
                                                                        <asp:TextBox ID="txtEasyGoPerRecharge" MaxLength="4" onkeypress="javascript:return onlynumericDecimal(event,this);" runat="server" title="EasyGo % Recharge"></asp:TextBox>

                                                                        <b class="tooltip tooltip-top-right">
                                                                            <i class="fa fa-arrow-circle-down"></i>EasyGo % Recharge</b>

                                                                    </label>

                                                                </div>
                                                            </div>
                                                        </div>

                                                    </div>
                                                    <div id="tab-5" class="tab-pane" style="display: none;">
                                                        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                                            <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">

                                                                <label class="label">Plan</label>

                                                                <div class="table-responsive" style="max-height: 330px; overflow-y: auto;">

                                                                    <asp:Repeater ID="rptUltraMobile" runat="server">
                                                                        <HeaderTemplate>
                                                                            <table class="table table-bordered" id="dataTables-example">
                                                                                <thead>
                                                                                    <tr>
                                                                                        <th>
                                                                                            <asp:CheckBox ID="chkAll" OnCheckedChanged="chkAllUltraMobile_OnCheckedChanged" AutoPostBack="true" runat="server" /></th>
                                                                                        <th>Code</th>
                                                                                        <th>Description</th>
                                                                                        <th>Dealer Cost</th>
                                                                                        <th>Default</th>
                                                                                    </tr>
                                                                                    <tbody>
                                                                        </HeaderTemplate>

                                                                        <ItemTemplate>
                                                                            <tr>

                                                                                <td>
                                                                                    <asp:CheckBox ID="CheckBox2" runat="server" />
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label runat="server" ID="Label1" Text='<%# Eval("TariffCode") %>' />
                                                                                    <asp:HiddenField ID="hdnTariffId" runat="server" Value='<%# Bind("ID") %>' />
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label runat="server" ID="Label7" Text='<%# Eval("Description") %>' />
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtRental" runat="server" Text='<%# Eval("Rental") %>'></asp:TextBox>

                                                                                </td>
                                                                                <td>
                                                                                    <asp:RadioButton ID="RadioButton1" runat="server" OnCheckedChanged="rdoAllUltraMobile_OnCheckedChanged" AutoPostBack="true" />

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
                                                            <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">
                                                                <div class="col-md-3 col-md-push-1">
                                                                    <label class="label" style="font-weight: bold">Ultra Mobile % Recharge</label>
                                                                    <label class="input">
                                                                        <asp:TextBox ID="txtUltraMobileReCharge" MaxLength="4" onkeypress="javascript:return onlynumericDecimal(event,this);" runat="server" title="Ultra Mobile % Recharge"></asp:TextBox>

                                                                        <b class="tooltip tooltip-top-right">
                                                                            <i class="fa fa-arrow-circle-down"></i>Ultra Mobile % Recharge</b>

                                                                    </label>

                                                                </div>
                                                            </div>
                                                        </div>

                                                    </div>
                                                    <div id="tab-6" class="tab-pane" style="display: none;">
                                                        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                                            <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">

                                                                <label class="label">Plan</label>

                                                                <div class="table-responsive" style="max-height: 330px; overflow-y: auto;">

                                                                    <asp:Repeater ID="rptATT" runat="server">
                                                                        <HeaderTemplate>
                                                                            <table class="table table-bordered" id="dataTables-example">
                                                                                <thead>
                                                                                    <tr>
                                                                                        <th>
                                                                                            <asp:CheckBox ID="chkAll" OnCheckedChanged="chkAllATT_OnCheckedChanged" AutoPostBack="true" runat="server" /></th>
                                                                                        <th>Code</th>
                                                                                        <th>Description</th>
                                                                                        <th>Dealer Cost</th>
                                                                                        <th>Default</th>
                                                                                    </tr>
                                                                                    <tbody>
                                                                        </HeaderTemplate>

                                                                        <ItemTemplate>
                                                                            <tr>

                                                                                <td>
                                                                                    <asp:CheckBox ID="CheckBox2" runat="server" />
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label runat="server" ID="Label1" Text='<%# Eval("TariffCode") %>' />
                                                                                    <asp:HiddenField ID="hdnTariffId" runat="server" Value='<%# Bind("ID") %>' />
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label runat="server" ID="Label7" Text='<%# Eval("Description") %>' />
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtRental" runat="server" Text='<%# Eval("Rental") %>'></asp:TextBox>

                                                                                </td>
                                                                                <td>
                                                                                    <asp:RadioButton ID="RadioButton1" runat="server" OnCheckedChanged="rdoAllATT_OnCheckedChanged" AutoPostBack="true" />

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
                                                            <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">
                                                                <div class="col-md-3 col-md-push-1">
                                                                    <label class="label" style="font-weight: bold">AT&T % Recharge</label>
                                                                    <label class="input">
                                                                        <asp:TextBox ID="txtATTRecharge" MaxLength="4" onkeypress="javascript:return onlynumericDecimal(event,this);" runat="server" title="Ultra Mobile % Recharge"></asp:TextBox>

                                                                        <b class="tooltip tooltip-top-right">
                                                                            <i class="fa fa-arrow-circle-down"></i>AT&T % Recharge</b>

                                                                    </label>

                                                                </div>
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


        <div id="popupdiv" class="loading" title="Basic modal dialog" style="display: nonefont-family:Arial; font-size: 10pt; border: 2px solid #67CFF5; text-align: -webkit-center;">

            <b style="text-align: center!important;">Please Wait...........</b>
            <br />
            <br />
            <img src="img/loader.gif" alt="" style="text-align: center!important;" />
        </div>

        <script src="js/jquery-2.1.1.js"></script>
        <script src="js/plugin/chosen/chosen.jquery.js"></script>

        <script type="text/javascript">



            var config = {
                '.chosen-select': {},
                '.chosen-select-deselect': { allow_single_deselect: true },
                '.chosen-select-no-single': { disable_search_threshold: 10 },
                '.chosen-select-no-results': { no_results_text: 'Oops, nothing found!' },
                '.chosen-select-width': { width: "95%" }
            }
            for (var selector in config) {
                $(selector).chosen(config[selector]);
            }
        </script>

        <script type="text/javascript">


            $(document).ready(function () {

                $('a[data-toggle="tab"]').on('show.bs.tab', function (e) {
                    localStorage.setItem('activeTab', $(e.target).attr('href'));
                });
                var activeTab = localStorage.getItem('activeTab');

                if (activeTab) {
                    $('#myTab a[href="' + activeTab + '"]').tab('show');
                }


            });



        </script>
        <script>
            function CheckContinue() {
                var text;
                var res = confirm("Are you sure to continue?");
                if (res == true) {
                    //   validateControlsActivate();
                    if (validate() == true) {
                        return true;
                    }
                    else {
                        return false;
                    }
                }
                else {
                    return false;
                }
            }

        </script>


        <script>

            $(document).ready(function () {
                $(".toggle-password").click(function () {
                    $(this).toggleClass("fa-eye fa-eye-slash");
                    var input = $("#ContentPlaceHolder1_txtPassword");
                    if (input.attr("type") == "password") {

                        input.attr("type", "text");
                    } else {
                        input.attr("type", "password");
                    }
                });
            });
        </script>

    </div>


</asp:Content>
