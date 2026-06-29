<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="AuthenticationDemoApp.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            Welcome
            <asp:Literal ID="LtlUsername" runat="server"></asp:Literal>
            to home page
            <br />
            <a href="Secured/Page1.aspx">Secured/Page1.aspx</a>
            <a href="Secured/Page2.aspx">Secured/Page2.aspx</a>
            <asp:LinkButton ID="BtnLogin" runat="server" OnClick="BtnLogin_Click">Login</asp:LinkButton>
        </div>
    </form>
</body>
</html>
