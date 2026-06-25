<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="StateManagementDemo.Home" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            This is Home Page
            <a href="secure/Secure1.aspx">secure/Secure1.aspx</a>
            <a href="secure/Secure2.aspx">secure/Secure2.aspx</a>
            <div class="mt-1">
                <asp:Label ID="LblLoginStatus" runat="server" Text=""></asp:Label>
                <asp:LinkButton ID="LnkLoging" runat="server" OnClick="LnkLoging_Click"> Login </asp:LinkButton>
            </div>
        </div>
    </form>
</body>
</html>
