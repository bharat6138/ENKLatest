<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="GlobalRecharge.aspx.cs" Inherits="ENK.GlobalRecharge" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta http-equiv="Cache-Control" content="no-cache" />
    <link type="text/css" rel="stylesheet" href="http://code.jquery.com/ui/1.10.3/themes/smoothness/jquery-ui.css" />
    <%-- <script type="text/javascript" src="http://code.jquery.com/jquery-1.8.2.js"></script>--%>
    <script type="text/javascript" src="http://code.jquery.com/ui/1.10.3/jquery-ui.js"></script>
    <script type="text/javascript">
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



    <style type="text/css">
        table, th, td {
            border: 1px solid black;
            border-collapse: collapse;
        }
        .loader {
  margin: 5em auto;
}
       #loading {
 position: fixed;
 z-index: 999;
 height: 2em;
 width: 2em;
 overflow: show;
 margin: auto;
 top: 0;
 left: 0;
 bottom: 0;
 right: 0;
}

/* Transparent Overlay */
#loading:before {
 content: '';
 display: block;
 position: fixed;
 top: 0;
 left: 0;
 width: 100%;
 height: 100%;
 background-color: rgba(0,0,0,0.3);
}

.loader--audioWave {
  width: 3em;
  height: 2em;
  background: linear-gradient(#9b59b6, #9b59b6) 0 50%, linear-gradient(#9b59b6, #9b59b6) 0.625em 50%, linear-gradient(#9b59b6, #9b59b6) 1.25em 50%, linear-gradient(#9b59b6, #9b59b6) 1.875em 50%, linear-gradient(#9b59b6, #9b59b6) 2.5em 50%;
  background-repeat: no-repeat;
  background-size: 0.5em 0.25em, 0.5em 0.25em, 0.5em 0.25em, 0.5em 0.25em, 0.5em 0.25em;
  animation: audioWave 1.5s linear infinite;
}
@keyframes audioWave {
  25% {
    background: linear-gradient(#3498db, #3498db) 0 50%, linear-gradient(#9b59b6, #9b59b6) 0.625em 50%, linear-gradient(#9b59b6, #9b59b6) 1.25em 50%, linear-gradient(#9b59b6, #9b59b6) 1.875em 50%, linear-gradient(#9b59b6, #9b59b6) 2.5em 50%;
    background-repeat: no-repeat;
    background-size: 0.5em 2em, 0.5em 0.25em, 0.5em 0.25em, 0.5em 0.25em, 0.5em 0.25em;
  }
  37.5% {
    background: linear-gradient(#9b59b6, #9b59b6) 0 50%, linear-gradient(#3498db, #3498db) 0.625em 50%, linear-gradient(#9b59b6, #9b59b6) 1.25em 50%, linear-gradient(#9b59b6, #9b59b6) 1.875em 50%, linear-gradient(#9b59b6, #9b59b6) 2.5em 50%;
    background-repeat: no-repeat;
    background-size: 0.5em 0.25em, 0.5em 2em, 0.5em 0.25em, 0.5em 0.25em, 0.5em 0.25em;
  }
  50% {
    background: linear-gradient(#9b59b6, #9b59b6) 0 50%, linear-gradient(#9b59b6, #9b59b6) 0.625em 50%, linear-gradient(#3498db, #3498db) 1.25em 50%, linear-gradient(#9b59b6, #9b59b6) 1.875em 50%, linear-gradient(#9b59b6, #9b59b6) 2.5em 50%;
    background-repeat: no-repeat;
    background-size: 0.5em 0.25em, 0.5em 0.25em, 0.5em 2em, 0.5em 0.25em, 0.5em 0.25em;
  }
  62.5% {
    background: linear-gradient(#9b59b6, #9b59b6) 0 50%, linear-gradient(#9b59b6, #9b59b6) 0.625em 50%, linear-gradient(#9b59b6, #9b59b6) 1.25em 50%, linear-gradient(#3498db, #3498db) 1.875em 50%, linear-gradient(#9b59b6, #9b59b6) 2.5em 50%;
    background-repeat: no-repeat;
    background-size: 0.5em 0.25em, 0.5em 0.25em, 0.5em 0.25em, 0.5em 2em, 0.5em 0.25em;
  }
  75% {
    background: linear-gradient(#9b59b6, #9b59b6) 0 50%, linear-gradient(#9b59b6, #9b59b6) 0.625em 50%, linear-gradient(#9b59b6, #9b59b6) 1.25em 50%, linear-gradient(#9b59b6, #9b59b6) 1.875em 50%, linear-gradient(#3498db, #3498db) 2.5em 50%;
    background-repeat: no-repeat;
    background-size: 0.5em 0.25em, 0.5em 0.25em, 0.5em 0.25em, 0.5em 0.25em, 0.5em 2em;
  }
}
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- MAIN PANEL -->
    <div id="main" role="main" style="margin-top: 0px!important;">
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </asp:ToolkitScriptManager>
        <!-- RIBBON -->

        <!-- END RIBBON -->

        <!-- MAIN CONTENT -->
        <div id="content">
            <div class="row new-inerpage">
                <div class="col-xs-12 col-sm-5 col-md-5 col-lg-4">
                    <h1 class="page-title txt-color-blueDark">
                        <i class="fa fa-lg fa-fw fa-file-text"></i>
                        Welcome	
                    </h1>
                </div>
                <div class="col-xs-12 col-sm-7 col-md-7 col-lg-8">
                </div>
            </div>

            <!-- widget grid -->
            <section id="widget-grid" class="">

                <!-- row -->
                <div class="row">
                    <article class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                        <div class="jarviswidget jarviswidget-color-blueDark" id="wid-id-0" data-widget-colorbutton="false" data-widget-editbutton="false" data-widget-togglebutton="false" data-widget-deletebutton="false" data-widget-fullscreenbutton="false" data-widget-custombutton="false" data-widget-collapsed="false" data-widget-sortable="false">

                            <header>
                                <span class="widget-icon"><i class="fa fa-table"></i></span>
                                <h2>GLOBAL RECHARGE</h2>
                            </header>
                            <div>
                                <div id="login-form" class="smart-form client-form">

                                    <div class="box-input">

                                        <div class="row">
                                            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-4">
                                                <label class="label">Mobile Number</label>
                                                <label class="input">
                                                    <asp:TextBox ID="txtMobileNumber" runat="server" ClientIDMode="static" CssClass="form-control"></asp:TextBox>

                                                </label>
                                            </div>

                                            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-4">
                                                <label class="label">Country</label>
                                                <label class="input">


                                                    <asp:DropDownList class="chosen-select text-area" ID="ddlCountry" runat="server" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged" AutoPostBack="true">
                                                    </asp:DropDownList>
                                                </label>
                                            </div>
                                            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-4">
                                                <label class="label">Operator</label>
                                                <label class="input">
                                                    <asp:DropDownList class="chosen-select text-area" ID="ddlOperator" runat="server" ClientIDMode="static" AutoPostBack="false">
                                                    </asp:DropDownList>
                                                </label>
                                            </div>

                                        </div>
                                        <div id ="loading" style="display:none;"><div class='loader loader--audioWave'></div></div>
                                                
                                        <div class="row" id="DivPlan" style="display: none;">
                                            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                                
                                                <label class="label">Plan</label>
                                                <table id="tbDetails" class="table-bordered table table-responsive" style="width: 100%">
                                                    <thead style="font-weight: bold;">
                                                        <tr>
                                                            <th>Plan</th>
                                                            <th>Min Recharge Value (USD)</th>
                                                            <th>Max Recharge Value (USD)</th>
                                                            <th>Choose Plan</th>

                                                        </tr>
                                                    </thead>
                                                    <tbody></tbody>
                                                </table>
                                            </div>
                                        </div>

                                        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                           <input type="button" id="btnSubmit" value="Submit" class="btn btn-primary pull-right" onclick="DingRecharge()"/>
                                             <br />
                                        </div>
                                        <asp:HiddenField ID="hdnfldSKU" ClientIDMode="Static" runat="server" />
                                        <asp:HiddenField ID="hdnfldDIFF" ClientIDMode="Static" runat="server" />
                                        <asp:HiddenField ID="hdnfldMaxAmount" ClientIDMode="Static" runat="server" />
                                        <asp:HiddenField ID="hdnfldRequest" ClientIDMode="Static" runat="server" />
                                        <asp:HiddenField ID="hdnfldResponse" ClientIDMode="Static" runat="server" />
                                        <asp:HiddenField ID="HiddenField1" ClientIDMode="Static" runat="server" />

                                    </div>


                                    <div id="myModal" role="dialog" class="col-xs-6 col-sm-6 col-md-6" style="margin-top: -20%; margin-left: 28%; display: none;">
                                        <div class="modal-dialog">
                                            <div class="modal-content" style="width: 255px;">
                                                <div id="dvGetMsisdn">
                                                    <div class="modal-header">
                                                        <h4 class="modal-title">Enter Amount (USD)</h4>
                                                    </div>
                                                    <div class="modal-body">
                                                        <div class="row">
                                                            <div class="col-md-12">
                                                                <asp:TextBox ID="txtAmount" runat="server" ClientIDMode="Static" CssClass="form-control" Style="width: 200px; float: left; height: 20px; margin-right: 10px;"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="modal-footer">
                                                        <button id="closebtn" type="button" class="btn btn-default">Close</button>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <!-- start footer -->
                                <!-- end footer -->
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

    <!-- END MAIN PANEL -->
    <div id="popupdiv" class="loading" title="Basic modal dialog" style="display: nonefont-family:Arial; font-size: 10pt; border: 2px solid #67CFF5; text-align: -webkit-center;">

        <b style="text-align: center!important;">Please Wait...........</b>
        <br />
        <br />
        <img src="img/loader.gif" alt="" style="text-align: center!important;" />
    </div>

    <%--    <script src="js/jquery-2.1.1.js"></script>--%>
    <script src="js/plugin/chosen/chosen.jquery.js"></script>

    <!-- PAGE RELATED PLUGIN(S) -->
    <script src="js/plugin/jquery-form/jquery-form.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>

    <script>
        $(document).ready(function () {
            $j('#btnSubmit').attr("disabled", "disabled");
            var keyDown = false, ctrl = 17, vKey = 86, Vkey = 118;
            $j("#txtMobileNumber").keydown(function (e) {
                if (e.keyCode == ctrl) keyDown = true;
            }).keyup(function (e) {
                if (e.keyCode == ctrl) keyDown = false;
            });

            $("#txtMobileNumber").on('keypress', function (e) {
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

    <script>
        var $j = jQuery.noConflict();
        $j(document).ready(function () {
            $j('#ddlOperator').val('0');
            $j("#hdnfldSKU").val('');
            $j("#hdnfldMaxAmount").val('');
            $j("#hdnfldDIFF").val('');
            $j("#txtAmount").val('');

            $j('#ddlOperator').on('change', function () {
                $j('#loading').show();
                var pCODE = $j('#ddlOperator').val();
                var settings = {
                    "async": true,
                    "crossDomain": true,
                    "url": "https://api.dingconnect.com/api/V1/GetProducts?providerCodes=" + pCODE,
                    "method": "GET",
                    "headers": {
                        "api_key": "Bk4kbCu53Ub6ZO3xN2vaHU",
                        "cache-control": "no-cache",
                        "postman-token": "d37a590b-7819-b83d-572d-9e9f75c52ae6"
                    }
                }
                $j.ajax(settings).done(function (response) {
                    $j("#tbDetails td").detach();
                    for (var i = 0; i < response.Items.length; i++) {
                        $j("#DivPlan").css("display", "block");
                        $j("#tbDetails").append("<tr><td>" + response.Items[i].DefaultDisplayText + "</td><td style='display: none;'>" + response.Items[i].SkuCode + "</td><td style='display: none;'>" + response.Items[i].ProviderCode + "</td><td>" + response.Items[i].Minimum.SendValue + "</td><td>" + response.Items[i].Maximum.SendValue + "</td><td><div class='radio_item'>  <input type='radio' class= 'rdbtn' name = 'same' id='" + response.Items[i].Maximum.SendValue + "'  value='" + response.Items[i].SkuCode + "'  text= '" + response.Items[i].Minimum.SendValue + "' onclick = 'SelectPlan(this.value, this.id, this);'></div></td></tr>");
                        $j('#loading').hide();
                    }
                });
            });
        });
    </script>

    <script>
        var $j = jQuery.noConflict();
        function SelectPlan(SkuCode, MaxAmount, elm) {
            if ($j("#txtMobileNumber").val() == "") {
                alert('Please enter Mobile No.');
                return;
            }
            $j('#btnSubmit').removeAttr("disabled");
            var MinAmount = elm.getAttribute('text');
            var difference = (parseFloat(MaxAmount) - parseFloat(MinAmount));
            $j("#hdnfldSKU").val(SkuCode);
            $j("#hdnfldMaxAmount").val(MaxAmount);
            $j("#hdnfldDIFF").val(difference);

            var MaxAmount = $j("#hdnfldMaxAmount").val();
            $j("#txtAmount").val(MaxAmount);
            if ($j("#hdnfldDIFF").val() != "0") {
                $j("#myModal").show();
            }
            $j("#closebtn").on('click', function () {
                $j("#myModal").hide();
            });
        }
    </script>

    <script>
        var $j = jQuery.noConflict();
        function DingRecharge() {
            $j('#loading').show();
            var req1 = "";
            var SkuCode = $j("#hdnfldSKU").val();
            var MobileNumber = $j("#txtMobileNumber").val();

            if ($j("#hdnfldDIFF").val() != "0") {
                var MaxAmount = $j("#txtAmount").val();
            }
            else {
                var MaxAmount = $j("#hdnfldMaxAmount").val();
            }
            req1 = { "SkuCode": SkuCode, "SendValue": MaxAmount, "SendCurrencyIso": "USD", "AccountNumber": MobileNumber, "DistributorRef": "1234567", "ValidateOnly": false };
            req = JSON.stringify(req1);

            $j.ajax({
                "async": true,
                "crossDomain": true,
                "url": "https://api.dingconnect.com/api/V1/SendTransfer",
                "method": "POST",
                "headers": {
                    "api_key": "Bk4kbCu53Ub6ZO3xN2vaHU",
                    "content-type": "application/json",
                    "cache-control": "no-cache"
                },
                "processData": false,
                "data": req,

                success: function (data, status, jqXHR) {
                    if (jqXHR.status == 200) {
                        // alert("Recharge got successfully done");
                        //var resstring = JSON.stringify(jqXHR);
                        // window.prompt("Copy to clipboard: Ctrl+C, Enter", resstring); 
                    }                  
                    SendResponse(req1, jqXHR);
                },
                error: function (jqXHR, status, err) {
                    //var resstring = JSON.stringify(jqXHR);
                    //window.prompt("Copy to clipboard: Ctrl+C, Enter", resstring);
                    getErrorMessage(jqXHR, err);
                },
                complete: function (jqXHR, status) {
                    //var resstring = JSON.stringify(jqXHR);
                    //window.prompt("Copy to clipboard: Ctrl+C, Enter", resstring);

                    $j('#loading').hide();
                }
            })

            ////DING GetErrorCodeDescriptions
            function getErrorMessage(jqXHR, exception) {
                if (jqXHR.status === 0) {
                    alert('Recharge got failed From Server, Please try Again');
                } else if (jqXHR.status == 400) {
                    alert('Mobile Number does not match to Operator');
                } else if (jqXHR.status == 401) {
                    alert('Mobile Number does not match to Operator');
                } else if (jqXHR.status == 429) {
                    alert('The API call failed because of some transient error. The request can be retried later. You can retry but should contact Ding if the error persists.');
                } else if (jqXHR.status == 503) {
                    alert('The API call failed because of some transient error. The request can be retried later. You can retry but should contact Ding if the error persists.');
                } else if (jqXHR.status == 500) {
                    alert('Internal Server Error');
                } else if (exception === 'parsererror') {
                    alert('Requested JSON parse failed.');
                } else if (exception === 'timeout') {
                    alert('Time out error.');
                } else if (exception === 'abort') {
                    alert('Ajax request aborted.');
                } else {
                    alert('Recharge got failed, Please try Again');
                }

            }
        }
    </script>

    <script>
        var $j = jQuery.noConflict();
        function SendResponse(req, res) {
            var OP = $j('#ddlOperator').val();
            $j.ajax({
                type: "POST",
                url: "GlobalRecharge.aspx/GetRechargeDetails",
                data: '{Resp: "' + res.responseJSON.TransferRecord.TransferId.TransferRef + '|' + res.responseJSON.TransferRecord.Price.ReceiveValue + '|' + res.responseJSON.TransferRecord.Price.SendValue + '|' + res.responseJSON.TransferRecord.StartedUtc + '|' + res.responseJSON.TransferRecord.CompletedUtc + '|' + res.responseJSON.TransferRecord.ProcessingState + '|' + res.responseJSON.ResultCode + '|' + req.SkuCode + '|' + req.SendValue + '|' + req.SendCurrencyIso + '|' + req.AccountNumber + '|' + req.DistributorRef + '|' + OP + '" }',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: OnSuccess,
                failure: function (response) {
                    alert(response.d);
                }
            });
        }
        function OnSuccess(response) {
            window.location = response.d;
        }
    </script>

    <script type="text/javascript">
        $(document).ready(function () {
            $('#loading')
                .hide()
                .ajaxStart(function () {
                $(this).show();
            })
                .ajaxStop(function () {
                $(this).hide();
            });
        }); </script>
</asp:Content>

