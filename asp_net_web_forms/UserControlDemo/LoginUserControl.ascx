<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LoginUserControl.ascx.cs" Inherits="UserControlDemo.LoginUserControl" %>

<table>
    <tr>
        <td>Username</td>
        <td>
            <asp:TextBox ID="TxtUsername" runat="server"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="auto-style3">Password</td>
        <td class="auto-style3">
            <asp:TextBox ID="TxtPassword" runat="server" TextMode="Password"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="auto-style2">
            <asp:Button ID="BtnLogin" runat="server" Text="Login" OnClick="BtnLogin_Click" />
        </td>
        <td class="auto-style2">&nbsp;</td>
    </tr>
    <tr>
        <td class="auto-style2" colspan="2">
            <asp:Label ID="LblMessage" runat="server"></asp:Label>
        </td>
    </tr>
</table>

