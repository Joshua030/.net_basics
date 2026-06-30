<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ProvidersDemoApp.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <a href="Secured/Page1.aspx">Secured/Page1.aspx</a><br />
            <a href="Secured/Page2.aspx">Secured/Page2.aspx</a><br />
            <br />
            <asp:LoginView ID="LoginView1" runat="server">
                <AnonymousTemplate>
                    Please
                    <asp:LoginStatus ID="LoginStatus1" runat="server" />
                </AnonymousTemplate>
                <LoggedInTemplate>
                    welcome
                    <asp:LoginName ID="LoginName1" runat="server" />
                    <asp:LoginStatus ID="LoginStatus1" runat="server" />
                </LoggedInTemplate>
            </asp:LoginView>

            <asp:Panel ID="RolesPanel" runat="server" Visible="false">
                <hr />
                <h3>Roles for <asp:Literal ID="LitUser" runat="server" /></h3>
                Current roles: <asp:Literal ID="LitRoles" runat="server" /><br /><br />
                <asp:DropDownList ID="DdlRoles" runat="server">
                    <asp:ListItem>r1</asp:ListItem>
                    <asp:ListItem>r2</asp:ListItem>
                    <asp:ListItem>r3</asp:ListItem>
                </asp:DropDownList>
                <asp:Button ID="BtnAdd" runat="server" Text="Add me to role" OnClick="BtnAdd_Click" />
                <asp:Button ID="BtnRemove" runat="server" Text="Remove me from role" OnClick="BtnRemove_Click" />
            </asp:Panel>
        </div>
    </form>
</body>
</html>
