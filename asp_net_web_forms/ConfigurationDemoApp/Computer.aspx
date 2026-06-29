<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Computer.aspx.cs" Inherits="ConfigurationDemoApp.Computer" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            Id = <%=Request.QueryString["id"] %>
        </div>
    </form>
</body>
</html>
