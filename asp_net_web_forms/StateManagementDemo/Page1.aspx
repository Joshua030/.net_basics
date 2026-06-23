<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Page1.aspx.cs" Inherits="StateManagementDemo.Page1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            Please enter your Name: <asp:TextBox ID="TxtName" runat="server"></asp:TextBox><br />
            Please enter your Age: <asp:TextBox ID="TxtAge" runat="server"></asp:TextBox><br />
            <asp:Button ID="BtnNext" runat="server" Text="Next..." OnClick="BtnNext_Click" />
        </div>
    </form>
</body>
</html>
