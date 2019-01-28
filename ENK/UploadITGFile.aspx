<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UploadITGFile.aspx.cs" Inherits="ENK.UploadITGFile" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Commission Processing</title>

    <!-- Basic Styles -->
    <link rel="stylesheet" type="text/css" media="screen" href="css/bootstrap.min.css" />
    <link rel="stylesheet" type="text/css" media="screen" href="css/font-awesome.min.css" />

    <!-- GOOGLE FONT -->
    <link rel="stylesheet" href="http://fonts.googleapis.com/css?family=Open+Sans:400italic,700italic,300,400,700" />

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div id="content">
                <div style="margin-left:20px;">
                    <center><h3>Process Manual Commission</h3></center>
                </div>
                
                <div style="margin-left:20px;">
                    <label runat="server" id="lblStatus" style="color:red;"></label>
                </div>

                <div style="margin-left:20px;">
                    <table id="tblProcess" class="table-bordered" style="width:80%;margin-top: 10px;margin-bottom:30px;">
                        <tr style="height:40px;background-color:#ffcd41;">
                            <th style="width:50px;">Step No.</th>
                            <th style="width:300px;">Process Description</th>
                            <th style="width:300px;">Action</th>
                        </tr>

                        <tr id="tr1" runat="server" style="height:40px;">
                            <td>1</td>
                            <td>Upload Files</td>
                            <td>
                                <div class="col-lg-12">
                                    <div class="col-lg-6">
                                        <asp:FileUpload ID="FileUpload1" runat="server" />
                                    </div>
                                    <div class="col-lg-3">
                                        <asp:Button ID="btnUpload" runat="server" Text="Upload File" OnClick="btnUpload_Click" Class="btn btn-success" />
                                    </div>
                                </div>                                
                            </td>
                        </tr>

                        <tr id="tr2" runat="server" style="height:40px;">
                            <td>2</td>
                            <td>Transfer Data to ITGReport</td>
                            <td>
                                <div class="col-lg-12">
                                    <asp:Button ID="btnPostToITGReport" runat="server" Text="Post To ITG Report" OnClick="btnPostToITGReport_Click" Class="btn btn-success" Visible="false" />
                                </div>                                
                            </td>
                        </tr>

                        <tr id="tr3" runat="server" style="height:40px;">
                            <td>3</td>
                            <td>Remarks - Inventory not exists</td>
                            <td>
                                <div class="col-lg-12">
                                    <asp:Button ID="btnInventoryNotExists" runat="server" Text="Set Remarks" OnClick="btnInventoryNotExists_Click" Class="btn btn-success" Visible="false" />
                                </div>                                
                            </td>
                        </tr>

                        <tr  id="tr4" runat="server" style="height:40px;">
                            <td>4</td>
                            <td>Remarks - Check for preloaded SIMs</td>
                            <td>
                                <div class="col-lg-12">
                                    <asp:Button ID="btnPreloadedSIM" runat="server" Text="Set Remarks" OnClick="btnPreloadedSIM_Click" Class="btn btn-success" Visible="false" />
                                </div>                                
                            </td>
                        </tr>

                        <tr id="tr5" runat="server" style="height:40px;">
                            <td>5</td>
                            <td>Remarks - Check Topup Sequence Greater Than 3</td>
                            <td>
                                <div class="col-lg-12">
                                    <asp:Button ID="btnCheckTopupSequence" runat="server" Text="Set Remarks" OnClick="btnCheckTopupSequence_Click" Class="btn btn-success" Visible="false" />
                                </div>                                
                            </td>
                        </tr>

                        <tr  id="tr6" runat="server" style="height:40px;">
                            <td>6</td>
                            <td>Remarks - Check if sequence is more than the preloaded months</td>
                            <td>
                                <div class="col-lg-12">
                                    <asp:Button ID="Button3" runat="server" Text="Set Remarks" OnClick="Button3_Click" Class="btn btn-success" Visible="false"  />
                                </div>                                
                            </td>
                        </tr>

                        <tr  id="tr7" runat="server" style="height:40px;">
                            <td>7</td>
                            <td>Remarks - Activation Entry to be Posted</td>
                            <td>
                                <div class="col-lg-12">
                                    <asp:Button ID="Button4" runat="server" Text="Set Remarks" OnClick="Button4_Click" Class="btn btn-success" Visible="false" />
                                </div>                                
                            </td>
                        </tr>

                        <tr  id="tr8" runat="server" style="height:40px;">
                            <td>8</td>
                            <td>Remarks - Activation record already posted with Month=1</td>
                            <td>
                                <div class="col-lg-12">
                                    <asp:Button ID="Button5" runat="server" Text="Set Remarks" OnClick="Button5_Click" Class="btn btn-success" Visible="false" />
                                </div>                                
                            </td>
                        </tr>

                        <tr id="tr9" runat="server" style="height:40px;">
                            <td>9</td>
                            <td>Remarks - Activation record already posted with Month=2</td>
                            <td>
                                <div class="col-lg-12">
                                    <asp:Button ID="Button6" runat="server" Text="Set Remarks" OnClick="Button6_Click" Class="btn btn-success" Visible="false" />
                                </div>                                
                            </td>
                        </tr>

                        <tr id="tr10" runat="server" style="height:40px;">
                            <td>10</td>
                            <td>Remarks - Check if the first activation exists in our system for 3 month</td>
                            <td>
                                <div class="col-lg-12">
                                    <asp:Button ID="Button7" runat="server" Text="Set Remarks" OnClick="Button7_Click" Class="btn btn-success" Visible="false"  />
                                </div>                                
                            </td>
                        </tr>

                        <tr id="tr11" runat="server" style="height:40px;">
                            <td>11</td>
                            <td>Remarks - PAYG activated and recharged outside our portal</td>
                            <td>
                                <div class="col-lg-12">
                                    <asp:Button ID="Button8" runat="server" Text="Set Remarks" OnClick="Button8_Click" Class="btn btn-success" Visible="false" />
                                </div>                                
                            </td>
                        </tr>

                        <tr id="tr12" runat="server" style="height:40px;">
                            <td>12</td>
                            <td>Remarks - Activated from our portal and recharged outside our portal</td>
                            <td>
                                <div class="col-lg-12">
                                    <asp:Button ID="Button9" runat="server" Text="Set Remarks" OnClick="Button9_Click" Class="btn btn-success" Visible="false" />
                                </div>                                
                            </td>
                        </tr>

                        <tr id="tr13" runat="server" style="height:40px;">
                            <td>13</td>
                            <td>Remarks - Activated and recharged outside our portal</td>
                            <td>
                                <div class="col-lg-12">
                                    <asp:Button ID="Button10" runat="server" Text="Set Remarks" OnClick="Button10_Click" Class="btn btn-success" Visible="false" />
                                </div>                                
                            </td>
                        </tr>

                        <tr id="tr14" runat="server" style="height:40px;">
                            <td>14</td>
                            <td>Remarks - Activation from Portal</td>
                            <td>
                                <div class="col-lg-12">
                                    <asp:Button ID="Button1" runat="server" Text="Set Remarks" OnClick="Button1_Click" Class="btn btn-success" Visible="false" />
                                </div>                                
                            </td>
                        </tr>

                        <tr id="tr15" runat="server" style="height:40px;">
                            <td>15</td>
                            <td>Remarks - Activation from Outside</td>
                            <td>
                                <div class="col-lg-12">
                                    <asp:Button ID="Button2" runat="server" Text="Set Remarks" OnClick="Button2_Click" Class="btn btn-success" Visible="false" />
                                </div>                                
                            </td>
                        </tr>

                        <tr id="tr16" runat="server" style="height:40px;">
                            <td>16</td>
                            <td>Remarks - PAYG Activation from portal, recharge from system</td>
                            <td>
                                <div class="col-lg-12">
                                    <asp:Button ID="Button11" runat="server" Text="Set Remarks" OnClick="Button11_Click" Class="btn btn-success" Visible="false" />
                                </div>                                
                            </td>
                        </tr>

                        <tr id="tr17" runat="server" style="height:40px;">
                            <td>17</td>
                            <td>Remarks - Activation from portal, recharge from system</td>
                            <td>
                                <div class="col-lg-12">
                                    <asp:Button ID="Button12" runat="server" Text="Set Remarks" OnClick="Button12_Click" Class="btn btn-success" Visible="false" />
                                </div>                                
                            </td>
                        </tr>

                        <tr id="tr18" runat="server" style="height:40px;">
                            <td>18</td>
                            <td>Remarks - Activation done outside the portal, recharge from system</td>
                            <td>
                                <div class="col-lg-12">
                                    <asp:Button ID="Button13" runat="server" Text="Set Remarks" OnClick="Button13_Click" Class="btn btn-success" Visible="false" />
                                </div>                                
                            </td>
                        </tr>

                        <tr id="tr19" runat="server" style="height:40px;">
                            <td>19</td>
                            <td>Post To Process Commission Table</td>
                            <td>
                                <div class="col-lg-12">
                                    <asp:Button ID="btnPostToProcessCommissionTable" runat="server" Text="Post To Process Commission Table" OnClick="btnPostToProcessCommissionTable_Click" Class="btn btn-success" Visible="false" />
                                </div>                                
                            </td>
                        </tr>

                        <tr id="tr20" runat="server" style="height:40px;">
                            <td>20</td>
                            <td>Process Commission</td>
                            <td>
                                <div class="col-lg-12">
                                    <asp:Button ID="btnProcessCommission" runat="server" Text="Process Commission" OnClick="btnProcessCommission_Click" Class="btn btn-success" Visible="false" />
                                </div>                                
                            </td>
                        </tr>

                        <tr id="tr21" runat="server" style="height:40px;">
                            <td>21</td>
                            <td>Post To AfterCommissionProcess</td>
                            <td>
                                <div class="col-lg-12">
                                    <asp:Button ID="btnPostToAfterCommissionProcess" runat="server" Text="Post To AfterCommissionProcess" OnClick="btnPostToAfterCommissionProcess_Click" Class="btn btn-success" Visible="false" />
                                </div>                                
                            </td>
                        </tr>

                        <tr id="tr22" runat="server" style="height:40px;">
                            <td>22</td>
                            <td>TopUpManualCommission_June</td>
                            <td>
                                <div class="col-lg-12">
                                    <asp:Button ID="btnTopUpManualCommission_June" runat="server" Text="TopUpManualCommission_June" OnClick="btnTopUpManualCommission_June_Click" Class="btn btn-success" Visible="false" />
                                </div>                                
                            </td>
                        </tr>

                        <tr id="tr23" runat="server" style="height:40px;">
                            <td>23</td>
                            <td>InsertToSIMActivation_AfterCommissionProcess</td>
                            <td>
                                <div class="col-lg-12">
                                    <asp:Button ID="btnInsertToSIMActivation" runat="server" Text="InsertToSIMActivation_AfterCommissionProcess" OnClick="btnInsertToSIMActivation_Click" Class="btn btn-success" Visible="false" />
                                </div>                                
                            </td>
                        </tr>

                        <tr style="height:40px;">
                            <td colspan="3"></td>
                        </tr>
                    </table>                    
                </div>
            </div>
        </div>
    </form>

    <!-- Link to Google CDN's jQuery + jQueryUI; fall back to local -->
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/2.0.2/jquery.min.js"></script>
    <script>
        if (!window.jQuery) {
            document.write('<script src="js/libs/jquery-2.0.2.min.js"><\/script>');
        }
    </script>

    <script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.10.3/jquery-ui.min.js"></script>
    <script>
        if (!window.jQuery.ui) {
            document.write('<script src="js/libs/jquery-ui-1.10.3.min.js"><\/script>');
        }
    </script>

    <!-- IMPORTANT: APP CONFIG -->
    <script src="js/app.config.js"></script>

    <!-- JS TOUCH : include this plugin for mobile drag / drop touch events-->
    <script src="js/plugin/jquery-touch/jquery.ui.touch-punch.min.js"></script>

    <!-- BOOTSTRAP JS -->
    <script src="js/bootstrap/bootstrap.min.js"></script>

</body>
</html>
