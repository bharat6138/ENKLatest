<%@ Page Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="Print.aspx.cs" Inherits="ENK.Print" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title><%= ConfigurationManager.AppSettings["COMPANY_NAME"] %> </title>

    <link rel="stylesheet" type="text/css" media="screen" href="css/demo.min.css" />
    <style>
        body {
            background: rgb(204,204,204);
        }

        page {
            background: white;
            display: block;
            margin: 0 auto;
            margin-bottom: 0.5cm;
            box-shadow: 0 0 0.5cm rgba(0,0,0,0.5);
        }

            page[size="A4"] {
                width: 16cm;
                height: 16cm;
                float: left;
                position: relative;
            }

                page[size="A4"][layout="portrait"] {
                    width: 16cm;
                    height: 14cm;
                    top: 58px;
                    position: relative;
                }

            page[size="A3"] {
                width: 29.7cm;
                height: 42cm;
            }

                page[size="A3"][layout="portrait"] {
                    width: 42cm;
                    height: 29.7cm;
                }

            page[size="A5"] {
                width: 14.8cm;
                height: 21cm;
            }

                page[size="A5"][layout="portrait"] {
                    width: 21cm;
                    height: 14.8cm;
                }

        .demo {
            position: absolute;
            top: 5px;
            right: 0;
            width: 160px;
            z-index: 902;
            background: #F1DA91;
            display: none;
        }

        @media print {
            body, page {
                margin: 0;
                box-shadow: 0;
            }
        }

        .auto-style1 {
            width: 24%;
        }

        td, th {
            padding: 5px;
        }
    </style>

    <link type="text/css" rel="stylesheet" href="http://code.jquery.com/ui/1.10.3/themes/smoothness/jquery-ui.css" />


    <script type="text/javascript" src="http://code.jquery.com/jquery-1.8.2.js"></script>
    <script type="text/javascript" src="http://code.jquery.com/ui/1.10.3/jquery-ui.js"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js"></script>
    <script type="text/javascript">

        function printDiv(divName) {
            var printContents = document.getElementById(divName).innerHTML;
            var originalContents = document.body.innerHTML;

            document.body.innerHTML = printContents;

            window.print();

            document.body.innerHTML = originalContents;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div id="main" role="main" style="margin-top: 0px!important;">
        <!-- RIBBON -->

        <!-- END RIBBON -->

        <!-- MAIN CONTENT -->
        <div id="content">
            <div class="row new-inerpage">
                <div class="col-md-8 pull-left">

                    <page size="A4" id="printableArea">
<div style="padding:15px;">
        <table width="100%" border="0" cellspacing="0" cellpadding="0">
  
  <tr>
    <td>&nbsp;</td>
    <td colspan="5" class="auto-style1"><img src="img/logologin.png" class="img-resposive" width="200px" alt=""></td>
    
  </tr>
  
   
  
  <tr>
    <td>&nbsp;</td>
    <td class="auto-style1">&nbsp;</td>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
  </tr>
  <tr>
    <td>&nbsp;</td>
    <td class="auto-style1">Retailer ID</td>
    <td><div align="center">-</div></td>
    <td colspan="2"><asp:Label ID="lblRetailerName" runat="server" Text="-"></asp:Label></td>
    <td>&nbsp;</td>
  </tr>
  <tr>
    <td>&nbsp;</td>
    <td class="auto-style1">Mobile No.</td>
    <td><div align="center">-</div></td>
    <td colspan="2"><asp:Label ID="lblMno" runat="server" Text="-"></asp:Label></td>
    <td>&nbsp;</td>
  </tr>
  <tr>
    <td>&nbsp;</td>
    <td class="auto-style1">Product </td>
    <td><div align="center">-</div></td>
    <td colspan="2"><asp:Label ID="lblProduct" runat="server" Text="-"></asp:Label><span class="auto-style1"></span></td>
    <td>&nbsp;</td>
  </tr>
  <tr>
    <td>&nbsp;</td>
    <td class="auto-style1">Refrence No.</td>
    <td><div align="center">-</div></td>
    <td colspan="2"><asp:Label ID="lblRefrenceNo" runat="server" Text="-"></asp:Label></td>
    <td>&nbsp;</td>
  </tr>
  <tr>
    <td>&nbsp;</td>
    <td class="auto-style1">Transaction Date &amp; Time</td>
    <td><div align="center">-</div></td>
    <td><asp:Label ID="lblTransactionDate" runat="server" Text="-"></asp:Label></td>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
  </tr>
  <tr>
    <td>&nbsp;</td>
    <td class="auto-style1">Regulatory Fees</td>
    <td><div align="center">-</div></td>
    <td><span class="auto-style1">$</span><asp:Label ID="lblRegulatoryFees" runat="server" Text=""></asp:Label></td>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
  </tr>
              <tr>
    <td>&nbsp;</td>
    <td class="auto-style1">International Credit AddOn</td>
    <td><div align="center">-</div></td>
    <td><span class="auto-style1">$</span><asp:Label ID="lblInternationalCreditAmount" runat="server" Text=""></asp:Label></td>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
  </tr>
  <tr>
    <td>&nbsp;</td>
    <td class="auto-style1">Total Sales Amount </td>
    <td><div align="center">-</div></td>
    <td><span class="auto-style1">$</span><asp:Label ID="lblSalesAmount" runat="server" Text=""></asp:Label></td>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
  </tr>
  <tr>
    <td>&nbsp;</td>
    <td class="auto-style1">&nbsp;</td>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
  </tr>
  <tr>
    <td>&nbsp;</td>
    <td class="auto-style1">&nbsp;</td>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
  </tr>
  <tr>
    <td>&nbsp;</td>
    <td colspan="4" class="auto-style1">Taxes and surcharges may apply and will be chraged by the retailer Seprate</td>
    <td>&nbsp;</td>
  </tr>
  <tr>
    <td>&nbsp;</td>
    <td colspan="4" class="auto-style1">NO REFUND or Exchange of Product given</td>
    <td>&nbsp;</td>
  </tr>
  <tr>
    <td>&nbsp;</td>
    <td colspan="4" class="auto-style1">See <%= ConfigurationManager.AppSettings["COMPANY_WEBSITE"] %> for terms and conditions.</td>
    <td>&nbsp;</td>
  </tr>
  <tr>
    <td>&nbsp;</td>
    <td class="auto-style1">&nbsp;</td>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
  </tr>
  <tr>
    <td>&nbsp;</td>
    <td class="auto-style1">&nbsp;</td>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
  </tr>
  <tr>
    <td>&nbsp;</td>
    <td class="auto-style1">&nbsp;</td>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
  </tr>
</table>
</div>

                </div>
                <div class="col-md-4" style="padding-top: 200px;">


                    <input type="button" class="btn btn-primary" onclick="printDiv('printableArea')" value="Print" />
                    <asp:Button ID="btnRedirect" runat="server" Text="" OnClick="btnRedirect_Click" class="btn btn-warning" />
                </div>
            </div>
        </div>
    </div>
    </page>
</asp:Content>
