<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ControlProperties.aspx.cs" Inherits="MyFirstApplicaton.ControlProperties" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server" defaultbutton="BtnDelete">
        <asp:Panel ID="Panel1" runat="server">
              <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
  <asp:Button ID="BtnDelete" runat="server" Text="Button" OnClientClick="return confirm('Are You sure')" OnClick="BtnDelete_Click" />
        </asp:Panel>
        <asp:Panel ID="Panel2" runat="server" DefaultButton="BtnAlternButton">
    <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
    <asp:Button ID="BtnAlternButton" runat="server" Text="Button" OnClientClick="alert('You press altern button')" OnClick="BtnDelete_Click" />
        </asp:Panel>
    </form>
</body>
</html>
s