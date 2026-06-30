<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Page1.aspx.cs" Inherits="ProvidersDemoApp.Secured.Page1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            This is page 1<br />
            <a href="Page2.aspx">Page2.aspx</a><br />
            <a href="../Default.aspx">../Default.aspx</a><br />
            <a href="ChangePassword.aspx">ChangePassword.aspx</a><br />
        </div>
    </form>
</body>
</html>
