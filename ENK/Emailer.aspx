<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Emailer.aspx.cs" Inherits="ENK.Emailer" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div class="row">     
        <asp:DropDownList runat="server" ID="ddlClient">
            <asp:ListItem Text="EAHA" Value="3"></asp:ListItem>
            <asp:ListItem Text="BNK" Value="2"></asp:ListItem>
            <asp:ListItem Text="ENK" Value="1"></asp:ListItem>
        </asp:DropDownList>   &nbsp; &nbsp; &nbsp; &nbsp;
        <asp:Button ID="btnSend" runat="server" Text="Send" OnClick="btnSend_Click"  />
    </div>
    </form>
</body>
</html>
