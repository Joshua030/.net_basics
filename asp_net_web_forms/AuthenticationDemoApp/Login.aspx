<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="AuthenticationDemoApp.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label runat="server" ID="LblError" ForeColor="Red" />
            Login:
            <asp:TextBox ID="TxtLogin" runat="server"></asp:TextBox><br />
            Password:
            <asp:TextBox ID="TxtPassword" runat="server"></asp:TextBox><br />
            <asp:Button ID="BtnLogin" runat="server" Text="Login" OnClick="BtnLogin_Click" />
            <asp:CheckBox ID="ChkRememberMe" runat="server" Text="Remember Me" /><br />
        </div>
    </form>
</body>
</html>
