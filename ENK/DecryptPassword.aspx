<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DecryptPassword.aspx.cs" Inherits="ENK.DecryptPassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>DecryptPassword</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Label runat="server" ID="lblPassword" Text="Enter Encrypted Password: "></asp:Label>&nbsp;&nbsp;&nbsp;
        
        <asp:TextBox runat="server" ID="txtPassword"></asp:TextBox><br /><br />
        <asp:Button runat="server" ID="btnDecrypt" Text="Click To Decrypt" OnClick="btnDecrypt_Click" /><br /><br />
        <asp:Label runat="server" ID="lblDecryptPass" Text="Decrypted Password: "></asp:Label><asp:Label style="color:red" runat="server" ID="lblMessage"></asp:Label>
    </div>
    </form>
</body>
</html>
