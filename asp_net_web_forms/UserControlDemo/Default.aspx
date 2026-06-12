<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="UserControlDemo.Default" %>

<%@ Register src="LoginUserControl.ascx" tagname="LoginUserControl" tagprefix="uc1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">S
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1>Welcome to my website</h1>
            <uc1:LoginUserControl ID="LoginUserControl1" runat="server" ReturnUrl="~/WelcomeAuthenticatedUser.aspx" />
        </div>
    </form>
</body>
</html>
