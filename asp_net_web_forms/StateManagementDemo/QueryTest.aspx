<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QueryTest.aspx.cs" Inherits="StateManagementDemo.QueryTest" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:HyperLink ID="HlinkSearch" NavigateUrl="~/WebForm1.aspx?search=demo" runat="server">Search</asp:HyperLink>
            <asp:TextBox ID="TxtSearch" runat="server"></asp:TextBox>
            <asp:Button runat="server" ID="BtnSearch" Text="Search" OnClick="BtnSearch_Click" />
            <asp:Button runat="server" ID="BtnTransfer" Text="Transfer" OnClick="BtnTransfer_Click" />
        </div>
    </form>
</body>
</html>
