<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default4.aspx.cs" Inherits="MyFirstApplicaton.Default4" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 100%;
            background-color: #00FF00;
        }
        .auto-style2 {
            width: 100%;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    
        <table class="auto-style1">
            <tr>
                <td>
    
        <asp:ListBox ID="lsbLeft" runat="server">
            <asp:ListItem Value="1" >One</asp:ListItem>
            <asp:ListItem Value="2" >Two</asp:ListItem>
            <asp:ListItem Value="3" >Three</asp:ListItem>
            <asp:ListItem Value="4" >Four</asp:ListItem>
                    </asp:ListBox>
                </td>
                <td>
                    <table class="auto-style2">
                        <tr>
                            <td>
        <asp:Button ID="btnMoveToRight" runat="server" Text=" &gt; " OnClick="btnMoveToRight_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td>
        <asp:Button ID="btnMoveToLeft" runat="server" Text=" &lt; " OnClick="btnMoveToLeft_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
                <td>
                    <asp:ListBox ID="lsbRight" runat="server"></asp:ListBox>
                </td>
            </tr>
        </table>
    
    </form>
</body>
</html>
