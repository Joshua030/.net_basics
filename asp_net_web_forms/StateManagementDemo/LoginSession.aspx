<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginSession.aspx.cs" Inherits="StateManagementDemo.LoginSession" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="LblError" runat="server" Text=""></asp:Label><br />
            Username: <asp:TextBox ID="TxtUsername" runat="server"></asp:TextBox><br />
            Password: <asp:TextBox ID="TxtPassword" runat="server"></asp:TextBox><br />
            <asp:Button ID="BtnLogin" runat="server" Text="Login" OnClick="BtnLogin_Click" />
        </div>
    </form>
</body>
</html>
