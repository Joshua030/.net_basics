<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="StateManagementDemo.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            Please Login Here...
            <asp:Label ID="LblError" runat="server" Text=""></asp:Label>
            Username:<asp:TextBox ID="TxtUsername" runat="server"></asp:TextBox><br />
            Password:<asp:TextBox ID="TxtPassword" runat="server"></asp:TextBox><br />
            <asp:CheckBox ID="ChkRemember" runat="server" Text="remeber my u/p o this  machine"/><br />
            <<asp:Button ID="BtnLogin" runat="server" Text="Login" OnClick="BtnLogin_Click" />
        </div>
    </form>
</body>
</html>
