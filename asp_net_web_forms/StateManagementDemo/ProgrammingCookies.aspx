<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProgrammingCookies.aspx.cs" Inherits="StateManagementDemo.ProgrammingCookies" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            Name:
            <asp:TextBox ID="txtKey" runat="server"></asp:TextBox>
            <br />

            Value:
            <asp:TextBox ID="txtValue" runat="server"></asp:TextBox><br />

            <asp:CheckBox ID="chkPersistance" runat="server" Text="Is Persistance" /><br />

            <asp:CheckBox ID="chkSecured" runat="server" Text="Is Secured" /><br />

            <asp:Button ID="btnSetCookie" runat="server" OnClick="btnSetCookie_Click" Text="Set Cookie" />

            <asp:Button ID="btnGetCookie" runat="server" OnClick="btnGetCookie_Click" Text="Get Cookie" style="height: 26px" />

            <asp:Button ID="btnDestroyCookie" runat="server" OnClick="btnDestroyCookie_Click" Text="Destroy Cookie" />

            <asp:Button ID="btnGetAllCookies" runat="server" OnClick="btnGetAllCookies_Click" Text="Get All Cookies" />
            <br />
            <asp:Label ID="lblValue" runat="server" /></asp:Label>
        </div>
    </form>
</body>
</html>
