<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="paypal.aspx.cs" Inherits="ENK.paypal" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server" action='expresscheckout.aspx' METHOD='POST'>
    <div>
     
        <input type='image' name='submit' src='https://www.paypal.com/en_US/i/btn/btn_xpressCheckout.gif' border='0' align='top' alt='Check out with PayPal'/>
 
    </div>
    </form>
</body>
</html>
