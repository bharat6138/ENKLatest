<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="TariffGroupN.aspx.cs" Inherits="ENK.TariffGroupN" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .table-responsive {
            height: 40vh;
        }

        .txt-box {
            width: 65px;
        }

        .form-control,
        .single-line {
            background-color: #FFFFFF;
            background-image: none;
            border: 1px solid #e5e6e7;
            border-radius: 1px;
            color: inherit;
            display: block;
            padding: 6px 12px;
            transition: border-color 0.15s ease-in-out 0s, box-shadow 0.15s ease-in-out 0s;
            width: 100%;
            font-size: 14px;
        }

            .form-control:focus,
            .single-line:focus {
                border-color: #b01116 !important;
            }

        .has-success .form-control {
            border-color: #b01116;
        }

        .has-warning .form-control {
            border-color: #f8ac59;
        }

        .has-error .form-control {
            border-color: #ed5565;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        function checkonlynumeric(evt) {
            var keycode;
            //alert('pankaj');
            if (evt.keyCode) //For IE
                keycode = evt.keyCode;
            else if (evt.Which)
                keycode = evt.Which;  // For FireFox
            else
                keycode = evt.charCode; // Other Browser

            if (keycode != 8) //if the key is the backspace key
            {
                if ((keycode >= 48 && keycode <= 57 && this.value.split(".").length > 1) || keycode == 45 || keycode == 9) { //if not a number
                    return true;  // enable key press
                }
                else {
                    alert("Only numbers allowed.");
                    return false; // disable key press
                }
            }
        }//end

        function ValidatenumericDecimal(evt, cnt) {
            var keycode;
            //alert(cnt.value);                
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
            //alert(keycode);
            if (keycode != 8) //if the key is the backspace key
            {
                if ((keycode >= 48 && keycode <= 57)) { //if not a number
                    // alert(keycode);
                    var tmp;
                    if (event.keyCode == 48) {
                        tmp = textbox.value + "0";
                        if (tmp.trim() == "0") {
                            alert("Enter Correct Quantity. Zero Not Allowed");
                            textbox.value = "1";
                            return false;
                        }
                    }
                    return true;  // enable key press
                }
                else {
                    alert("Only numbers allowed.");
                    return false; // disable key press
                }
            }
        }//end

        function onlynumericDecimal(evt, cnt) {

            var keycode;
            //alert(cnt.value);
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

                    //alert(tmp);
                    if (tmp.split(".").length > 2) {
                        textbox.value = tmp.trim().substr(0, tmp.length - 1);
                        alert("Only one decimal allowed.");
                        return false;
                    }
                    else {

                        return true;
                    }

                }
                else {
                    alert("Only numbers allowed.");
                    // alert(keycode);
                    return false; // disable key press
                }
                //alert(textbox.value.split(".").length);

            }

        }//end


    </script>
    <div id="main" role="main" style="margin-top: 0px!important;">
        <asp:ScriptManager ID="SC1" runat="server" EnableViewState="true">
        </asp:ScriptManager>

        <!-- RIBBON -->

        <!-- END RIBBON -->
        <div style="width: 97%; text-align: right;">
            <%--<asp:Button ID="btnBack" runat="server" CssClass="btn btn-primary" Text="Back" OnClick="btnBack_Click" />--%>
        </div>

        <!-- MAIN CONTENT -->
        <div id="content">



            <!-- row -->

            <div class="row">



                <!-- NEW WIDGET START -->
                <article class="col-sm-12 col-md-12 col-lg-12">





                    <!-- widget content -->
                    <div class="widget-body">

                        <div class="row">
                            <div class="form-group col-md-6">
                                <label class="control-label">Tariff Group</label>
                                <div class="inputGroupContainer">
                                    <div class="input-group">
                                        <asp:TextBox ID="txtGroup" runat="server" CssClass="form-control" />
                                        <span class="input-group-addon"><i class="fa  fa-cubes"></i></span>
                                    </div>
                                </div>
                            </div>




                            <div class="form-group col-md-6">
                                <label class="control-label">&nbsp;</label><br />
                                <%--<button class="btn btn-primary col-md-12" type="submit">
																<i class="fa fa-save"></i>
																SAVE
															</button>--%>
                                <asp:Button ID="btnSave" runat="server" Text="SAVE" CssClass="btn btn-primary col-md-12" OnClick="btnSave_Click" />
                            </div>


                        </div>






                        <hr class="simple" />
                        <ul id="myTab1" class="nav nav-tabs bordered">
                            <li class="active">
                                <a href="#s1" data-toggle="tab"><i class="glyphicon glyphicon-book"></i>General Plans</a>
                            </li>
                            <%--<li>
												<a href="#s2" data-toggle="tab"><i class="glyphicon glyphicon-book"></i> Family Plan</a>
											</li>
											
                            --%>
                        </ul>

                        <div id="myTabContent1" class="tab-content padding-10">
                            <div class="tab-pane fade in active" id="s1">
                                
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                     <ContentTemplate>
                                <div class="row">
                                    <div class="form-group col-md-3">
                                        <label class="control-label">Discount</label>
                                        <div class="inputGroupContainer">
                                            <div class="input-group">
                                                <asp:TextBox class="form-control"  runat="server" value="0" AutoPostBack="true" ID="txtComission" OnTextChanged="txtComission_Changed" onkeypress="javascript:return onlynumericDecimal(event,this);"></asp:TextBox>
                                                <span class="input-group-addon">%</span>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group col-md-3">
                                        <label class="control-label">Recharge Percentage</label>
                                        <div class="inputGroupContainer">
                                            <div class="input-group">
                                                <asp:TextBox class="form-control" runat="server" value="0" ID="txtRechageCom"  onkeypress="javascript:return onlynumericDecimal(event,this);"></asp:TextBox>
                                                 <span class="input-group-addon">%</span>
                                            </div>
                                        </div>
                                    </div>
                                  <%--  <div class="form-group col-md-6"></div>--%>

                                    
                                     <div class="form-group col-md-3">
                                        <label class="control-label">H2O Discount (%)</label>
                                        <div class="inputGroupContainer">
                                            <div class="input-group">
                                                <asp:TextBox class="form-control" runat="server" value="0" AutoPostBack="true" ID="txtH2OGeneralDiscount" OnTextChanged="txtH2OGeneralDiscount_TextChanged" onkeypress="javascript:return onlynumericDecimal(event,this);"></asp:TextBox>
                                                <span class="input-group-addon">%</span>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group col-md-3">
                                        <label class="control-label">H2O Recharge Discount (%)</label>
                                        <div class="inputGroupContainer">
                                            <div class="input-group">
                                                <asp:TextBox class="form-control" runat="server" value="0" ID="txtH2ORechargeDiscount"  onkeypress="javascript:return onlynumericDecimal(event,this);"></asp:TextBox>
                                                 <span class="input-group-addon">%</span>
                                            </div>
                                        </div>
                                    </div>


                                    <div class="form-group col-md-2">
                                        <label class="control-label">&nbsp;</label><br />
                                        <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="btn btn-primary col-md-12" OnClick="btnBack_Click1"/>
                                    </div>



                                </div>
                                         </ContentTemplate>
                                    </asp:UpdatePanel>

                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="table-responsive">

                                            <table class="table" style="display: none">
                                                <tr>
                                                    <td colspan="13" style="text-align: center; font-weight: bold" class="fa fa-building">Months</td>
                                                </tr>
                                                <thead>
                                                    <tr>
                                                        <th>Tarrif Name</th>
                                                        <th><i class="fa fa-building"></i>1</th>
                                                        <th><i class="fa fa-calendar"></i>2</th>
                                                        <th><i class="glyphicon glyphicon-send"></i>3</th>
                                                        <th><i class="glyphicon glyphicon-send"></i>4</th>
                                                        <th><i class="fa fa-building"></i>5</th>
                                                        <th><i class="fa fa-calendar"></i>6</th>
                                                        <th><i class="glyphicon glyphicon-send"></i>7</th>
                                                        <th><i class="glyphicon glyphicon-send"></i>8</th>
                                                        <th><i class="fa fa-building"></i>9</th>
                                                        <th><i class="fa fa-calendar"></i>10</th>
                                                        <th><i class="glyphicon glyphicon-send"></i>11</th>
                                                        <th><i class="glyphicon glyphicon-send"></i>12</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr>
                                                        <td>XYZ</td>
                                                        <td>
                                                            <input type="text" aria-controls="form-control" name="name" placeholder="Spiff" /><input type="text" aria-controls="form-control" name="name" placeholder="Monthly Commission" /></td>
                                                        <td>
                                                            <input type="text" aria-controls="form-control" name="name" placeholder="Spiff" /><input type="text" aria-controls="form-control" name="name" placeholder="Monthly Commission" /></td>
                                                        <td>
                                                            <input type="text" aria-controls="form-control" name="name" placeholder="Spiff" /><input type="text" aria-controls="form-control" name="name" placeholder="Monthly Commission" /></td>
                                                        <td>
                                                            <input type="text" aria-controls="form-control" name="name" placeholder="Spiff" /><input type="text" aria-controls="form-control" name="name" placeholder="Monthly Commission" /></td>
                                                        <td>
                                                            <input type="text" aria-controls="form-control" name="name" placeholder="Spiff" /><input type="text" aria-controls="form-control" name="name" placeholder="Monthly Commission" /></td>
                                                        <td>
                                                            <input type="text" aria-controls="form-control" name="name" placeholder="Spiff" /><input type="text" aria-controls="form-control" name="name" placeholder="Monthly Commission" /></td>
                                                        <td>
                                                            <input type="text" aria-controls="form-control" name="name" placeholder="Spiff" /><input type="text" aria-controls="form-control" name="name" placeholder="Monthly Commission" /></td>
                                                        <td>
                                                            <input type="text" aria-controls="form-control" name="name" placeholder="Spiff" /><input type="text" aria-controls="form-control" name="name" placeholder="Monthly Commission" /></td>

                                                        <td>
                                                            <input type="text" aria-controls="form-control" name="name" placeholder="Spiff" /><input type="text" aria-controls="form-control" name="name" placeholder="Monthly Commission" /></td>
                                                        <td>
                                                            <input type="text" aria-controls="form-control" name="name" placeholder="Spiff" /><input type="text" aria-controls="form-control" name="name" placeholder="Monthly Commission" /></td>
                                                        <td>
                                                            <input type="text" aria-controls="form-control" name="name" placeholder="Spiff" /><input type="text" aria-controls="form-control" name="name" placeholder="Monthly Commission" /></td>
                                                        <td>
                                                            <input type="text" aria-controls="form-control" name="name" placeholder="Spiff" /><input type="text" aria-controls="form-control" name="name" placeholder="Monthly Commission" /></td>

                                                    </tr>
                                                    <tr>
                                                        <td>ABC</td>
                                                        <td>
                                                            <input type="text" aria-controls="form-control" name="name" placeholder="Spiff" /><input type="text" aria-controls="form-control" name="name" placeholder="Monthly Commission" /></td>
                                                        <td>
                                                            <input type="text" aria-controls="form-control" name="name" placeholder="Spiff" /><input type="text" aria-controls="form-control" name="name" placeholder="Monthly Commission" /></td>
                                                        <td>
                                                            <input type="text" aria-controls="form-control" name="name" placeholder="Spiff" /><input type="text" aria-controls="form-control" name="name" placeholder="Monthly Commission" /></td>
                                                        <td>
                                                            <input type="text" aria-controls="form-control" name="name" placeholder="Spiff" /><input type="text" aria-controls="form-control" name="name" placeholder="Monthly Commission" /></td>
                                                        <td>
                                                            <input type="text" aria-controls="form-control" name="name" placeholder="Spiff" /><input type="text" aria-controls="form-control" name="name" placeholder="Monthly Commission" /></td>
                                                        <td>
                                                            <input type="text" aria-controls="form-control" name="name" placeholder="Spiff" /><input type="text" aria-controls="form-control" name="name" placeholder="Monthly Commission" /></td>
                                                        <td>
                                                            <input type="text" aria-controls="form-control" name="name" placeholder="Spiff" /><input type="text" aria-controls="form-control" name="name" placeholder="Monthly Commission" /></td>
                                                        <td>
                                                            <input type="text" aria-controls="form-control" name="name" placeholder="Spiff" /><input type="text" aria-controls="form-control" name="name" placeholder="Monthly Commission" /></td>

                                                        <td>
                                                            <input type="text" aria-controls="form-control" name="name" placeholder="Spiff" /><input type="text" aria-controls="form-control" name="name" placeholder="Monthly Commission" /></td>
                                                        <td>
                                                            <input type="text" aria-controls="form-control" name="name" placeholder="Spiff" /><input type="text" aria-controls="form-control" name="name" placeholder="Monthly Commission" /></td>
                                                        <td>
                                                            <input type="text" aria-controls="form-control" name="name" placeholder="Spiff" /><input type="text" aria-controls="form-control" name="name" placeholder="Monthly Commission" /></td>
                                                        <td>
                                                            <input type="text" aria-controls="form-control" name="name" placeholder="Spiff" /><input type="text" aria-controls="form-control" name="name" placeholder="Monthly Commission" /></td>
                                                    </tr>

                                                </tbody>
                                            </table>
                                            <asp:UpdatePanel ID="up1" runat="server">
                                                <ContentTemplate>

                                                    <div style="width: 100%; text-align: center; font-size: large; font-weight: bold;">Months</div>
                                                    <asp:GridView ID="gvTariff" GridLines="Horizontal" runat="server" Width="97%" CellPadding="15" CellSpacing="10" AutoGenerateColumns="false" OnRowDataBound="gvTariff_RowDataBound">
                                                        <RowStyle Height="65px" VerticalAlign="Top" />
                                                        <Columns>
                                                            <asp:BoundField HeaderText="Tariff Name" DataField="Description" />

                                                            <asp:TemplateField HeaderText="1" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtSP1" CssClass="txt-box" onKeyPress="javascript:return onlynumericDecimal(event,this);" runat="server" AutoPostBack="true" placeholder="0" OnTextChanged="txtSpiff1_TextChanged" Text='<%#Bind("Spiff1") %>'></asp:TextBox>
                                                                    <br />
                                                                    <asp:TextBox ID="txtSComm1" ReadOnly="true" CssClass="txt-box" runat="server" Text=""></asp:TextBox>
                                                                    <asp:HiddenField ID="hdnSpiff1" runat="server" Value='<%#Bind("Spiff1") %>' />
                                                                    <asp:HiddenField ID="hdnActive" runat="server" Value='<%#Bind("IsActive") %>' />
                                                                    <asp:HiddenField ID="hdnRental" runat="server" Value='<%#Bind("Rental") %>' />
                                                                    <asp:HiddenField ID="hdnTariffId" runat="server" Value='<%#Bind("TariffId") %>' />
                                                                    <asp:HiddenField ID="hdnFlag" runat="server" Value="0" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="2">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtSP2" runat="server" onKeyPress="javascript:return onlynumericDecimal(event,this);" CssClass="txt-box" AutoPostBack="true" placeholder="0" OnTextChanged="txtSpiff2_TextChanged" Text='<%#Bind("Spiff2") %>'></asp:TextBox>
                                                                    <br />
                                                                    <asp:TextBox ID="txtSComm2" ReadOnly="true" CssClass="txt-box" runat="server" Text=""></asp:TextBox>
                                                                    <asp:HiddenField ID="hdnSpiff2" runat="server" Value='<%#Bind("Spiff2") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="3">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtSP3" runat="server" onKeyPress="javascript:return onlynumericDecimal(event,this);" CssClass="txt-box" AutoPostBack="true" placeholder="0" OnTextChanged="txtSpiff3_TextChanged" Text='<%#Bind("Spiff3") %>'></asp:TextBox>
                                                                    <br />
                                                                    <asp:TextBox ID="txtSComm3" ReadOnly="true" CssClass="txt-box" runat="server" Text=""></asp:TextBox>
                                                                    <asp:HiddenField ID="hdnSpiff3" runat="server" Value='<%#Bind("Spiff1") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="4">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtSP4" CssClass="txt-box" onKeyPress="javascript:return onlynumericDecimal(event,this);" runat="server" AutoPostBack="true" placeholder="0" OnTextChanged="txtSpiff4_TextChanged" Text='<%#Bind("Spiff4") %>'></asp:TextBox>
                                                                    <br />
                                                                    <asp:TextBox ID="txtSComm4" ReadOnly="true" CssClass="txt-box" runat="server" Text=""></asp:TextBox>
                                                                    <asp:HiddenField ID="hdnSpiff4" runat="server" Value='<%#Bind("Spiff4") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="5">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtSP5" CssClass="txt-box" onKeyPress="javascript:return onlynumericDecimal(event,this);" runat="server" AutoPostBack="true" placeholder="0" OnTextChanged="txtSpiff5_TextChanged" Text='<%#Bind("Spiff5") %>'></asp:TextBox>
                                                                    <br />
                                                                    <asp:TextBox ID="txtSComm5" ReadOnly="true" CssClass="txt-box" runat="server" Text=""></asp:TextBox>
                                                                    <asp:HiddenField ID="hdnSpiff5" runat="server" Value='<%#Bind("Spiff5") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="6">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtSP6" onKeyPress="javascript:return onlynumericDecimal(event,this);" CssClass="txt-box" runat="server" AutoPostBack="true" placeholder="0" OnTextChanged="txtSpiff6_TextChanged" Text='<%#Bind("Spiff6") %>'></asp:TextBox>
                                                                    <br />
                                                                    <asp:TextBox ID="txtSComm6" ReadOnly="true" CssClass="txt-box" runat="server" Text=""></asp:TextBox>
                                                                    <asp:HiddenField ID="hdnSpiff6" runat="server" Value='<%#Bind("Spiff6") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="7">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtSP7" onKeyPress="javascript:return onlynumericDecimal(event,this);" CssClass="txt-box" runat="server" AutoPostBack="true" placeholder="0" OnTextChanged="txtSpiff7_TextChanged" Text='<%#Bind("Spiff7") %>'></asp:TextBox>
                                                                    <br />
                                                                    <asp:TextBox ID="txtSComm7" ReadOnly="true" CssClass="txt-box" runat="server" Text=""></asp:TextBox>
                                                                    <asp:HiddenField ID="hdnSpiff7" runat="server" Value='<%#Bind("Spiff7") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="8">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtSP8" CssClass="txt-box" onKeyPress="javascript:return onlynumericDecimal(event,this);" runat="server" AutoPostBack="true" placeholder="0" OnTextChanged="txtSpiff8_TextChanged" Text='<%#Bind("Spiff8") %>'></asp:TextBox>
                                                                    <br />
                                                                    <asp:TextBox ID="txtSComm8" ReadOnly="true" CssClass="txt-box" runat="server" Text=""></asp:TextBox>
                                                                    <asp:HiddenField ID="hdnSpiff8" runat="server" Value='<%#Bind("Spiff8") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="9">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtSP9" CssClass="txt-box" onKeyPress="javascript:return onlynumericDecimal(event,this);" runat="server" AutoPostBack="true" placeholder="0" OnTextChanged="txtSpiff9_TextChanged" Text='<%#Bind("Spiff9") %>'></asp:TextBox>
                                                                    <br />
                                                                    <asp:TextBox ID="txtSComm9" ReadOnly="true" CssClass="txt-box" runat="server" Text=""></asp:TextBox>
                                                                    <asp:HiddenField ID="hdnSpiff9" runat="server" Value='<%#Bind("Spiff9") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="10">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtSP10" CssClass="txt-box" onKeyPress="javascript:return onlynumericDecimal(event,this);" runat="server" AutoPostBack="true" placeholder="0" OnTextChanged="txtSpiff10_TextChanged" Text='<%#Bind("Spiff10") %>'></asp:TextBox>
                                                                    <br />
                                                                    <asp:TextBox ID="txtSComm10" ReadOnly="true" CssClass="txt-box" runat="server" Text=""></asp:TextBox>
                                                                    <asp:HiddenField ID="hdnSpiff10" runat="server" Value='<%#Bind("Spiff10") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="11">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtSP11" onKeyPress="javascript:return onlynumericDecimal(event,this);" CssClass="txt-box" runat="server" AutoPostBack="true" placeholder="0" OnTextChanged="txtSpiff11_TextChanged" Text='<%#Bind("Spiff11") %>'></asp:TextBox>
                                                                    <br />
                                                                    <asp:TextBox ID="txtSComm11" ReadOnly="true" CssClass="txt-box" runat="server" Text=""></asp:TextBox>
                                                                    <asp:HiddenField ID="hdnSpiff11" runat="server" Value='<%#Bind("Spiff11") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="12">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtSP12" onKeyPress="javascript:return onlynumericDecimal(event,this);" CssClass="txt-box" runat="server" AutoPostBack="true" placeholder="0" OnTextChanged="txtSpiff12_TextChanged" Text='<%#Bind("Spiff12") %>'></asp:TextBox>
                                                                    <br />
                                                                    <asp:TextBox ID="txtSComm12" ReadOnly="true" CssClass="txt-box" runat="server" Text=""></asp:TextBox>
                                                                    <asp:HiddenField ID="hdnSpiff12" runat="server" Value='<%#Bind("Spiff12") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                        </Columns>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Height="35px" />

                                                    </asp:GridView>
                                                    </div>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                                <div class="tab-pane fade" id="s2">
                                    <div class="row">
                                        <div class="form-group col-md-3">
                                            <label class="control-label">Comission</label>
                                            <div class="inputGroupContainer">
                                                <div class="input-group">
                                                    <input type="text" class="form-control" name="price" />
                                                    <span class="input-group-addon">%</span>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="table-responsive">

                                                <table class="table">
                                                    <thead>
                                                        <tr>
                                                            <th>Tarrif Group Name</th>
                                                            <th><i class="fa fa-building"></i>First Month</th>
                                                            <th><i class="fa fa-calendar"></i>Second Month</th>
                                                            <th><i class="glyphicon glyphicon-send"></i>Third Month</th>
                                                            <th><i class="glyphicon glyphicon-send"></i>Fourth Month</th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <tr>
                                                            <td>XYZ</td>
                                                            <td>
                                                                <input type="text" aria-controls="form-control" name="name" /><br />
                                                                <input type="text" aria-controls="form-control" name="name" /><br />
                                                                <input type="text" aria-controls="form-control" name="name" /><br />
                                                                <input type="text" aria-controls="form-control" name="name" /><br />
                                                                <input type="text" aria-controls="form-control" name="name" />
                                                            </td>
                                                            <td>
                                                                <input type="text" aria-controls="form-control" name="name" /><br />
                                                                <input type="text" aria-controls="form-control" name="name" /><br />
                                                                <input type="text" aria-controls="form-control" name="name" /><br />
                                                                <input type="text" aria-controls="form-control" name="name" /><br />
                                                                <input type="text" aria-controls="form-control" name="name" />
                                                            </td>
                                                            <td>
                                                                <input type="text" aria-controls="form-control" name="name" /><br />
                                                                <input type="text" aria-controls="form-control" name="name" /><br />
                                                                <input type="text" aria-controls="form-control" name="name" /><br />
                                                                <input type="text" aria-controls="form-control" name="name" /><br />
                                                                <input type="text" aria-controls="form-control" name="name" />
                                                            </td>
                                                            <td>
                                                                <input type="text" aria-controls="form-control" name="name" /><br />
                                                                <input type="text" aria-controls="form-control" name="name" /><br />
                                                                <input type="text" aria-controls="form-control" name="name" /><br />
                                                                <input type="text" aria-controls="form-control" name="name" /><br />
                                                                <input type="text" aria-controls="form-control" name="name" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>XYZ</td>
                                                            <td>
                                                                <input type="text" aria-controls="form-control" name="name" /><br />
                                                                <input type="text" aria-controls="form-control" name="name" /><br />
                                                                <input type="text" aria-controls="form-control" name="name" /><br />
                                                                <input type="text" aria-controls="form-control" name="name" /><br />
                                                                <input type="text" aria-controls="form-control" name="name" />
                                                            </td>
                                                            <td>
                                                                <input type="text" aria-controls="form-control" name="name" /><br />
                                                                <input type="text" aria-controls="form-control" name="name" /><br />
                                                                <input type="text" aria-controls="form-control" name="name" /><br />
                                                                <input type="text" aria-controls="form-control" name="name" /><br />
                                                                <input type="text" aria-controls="form-control" name="name" />
                                                            </td>
                                                            <td>
                                                                <input type="text" aria-controls="form-control" name="name" /><br />
                                                                <input type="text" aria-controls="form-control" name="name" /><br />
                                                                <input type="text" aria-controls="form-control" name="name" /><br />
                                                                <input type="text" aria-controls="form-control" name="name" /><br />
                                                                <input type="text" aria-controls="form-control" name="name" />
                                                            </td>
                                                            <td>
                                                                <input type="text" aria-controls="form-control" name="name" /><br />
                                                                <input type="text" aria-controls="form-control" name="name" /><br />
                                                                <input type="text" aria-controls="form-control" name="name" /><br />
                                                                <input type="text" aria-controls="form-control" name="name" /><br />
                                                                <input type="text" aria-controls="form-control" name="name" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>XYZ</td>
                                                            <td>
                                                                <input type="text" aria-controls="form-control" name="name" /><br />
                                                                <input type="text" aria-controls="form-control" name="name" /><br />
                                                                <input type="text" aria-controls="form-control" name="name" /><br />
                                                                <input type="text" aria-controls="form-control" name="name" /><br />
                                                                <input type="text" aria-controls="form-control" name="name" />
                                                            </td>
                                                            <td>
                                                                <input type="text" aria-controls="form-control" name="name" /><br />
                                                                <input type="text" aria-controls="form-control" name="name" /><br />
                                                                <input type="text" aria-controls="form-control" name="name" /><br />
                                                                <input type="text" aria-controls="form-control" name="name" /><br />
                                                                <input type="text" aria-controls="form-control" name="name" />
                                                            </td>
                                                            <td>
                                                                <input type="text" aria-controls="form-control" name="name" /><br />
                                                                <input type="text" aria-controls="form-control" name="name" /><br />
                                                                <input type="text" aria-controls="form-control" name="name" /><br />
                                                                <input type="text" aria-controls="form-control" name="name" /><br />
                                                                <input type="text" aria-controls="form-control" name="name" />
                                                            </td>
                                                            <td>
                                                                <input type="text" aria-controls="form-control" name="name" /><br />
                                                                <input type="text" aria-controls="form-control" name="name" /><br />
                                                                <input type="text" aria-controls="form-control" name="name" /><br />
                                                                <input type="text" aria-controls="form-control" name="name" /><br />
                                                                <input type="text" aria-controls="form-control" name="name" />
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>

                                            </div>
                                        </div>
                                    </div>
                                </div>

                            </div>

                        </div>
                        <!-- end widget content -->
                </article>
                <!-- WIDGET END -->


            </div>

            <!-- end row -->


        </div>
        <!-- END MAIN CONTENT -->

    </div>

</asp:Content>
