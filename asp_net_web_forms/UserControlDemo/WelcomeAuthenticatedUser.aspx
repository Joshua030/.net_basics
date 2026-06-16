<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WelcomeAuthenticatedUser.aspx.cs" Inherits="UserControlDemo.WelcomeAuthenticatedUser" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1>Welcome,
                <asp:Label ID="LblUsername" runat="server"></asp:Label>!</h1>
         <p><%=Request.QueryString["username"] %></p>
        </div>
    </form>
</body>
</html>
