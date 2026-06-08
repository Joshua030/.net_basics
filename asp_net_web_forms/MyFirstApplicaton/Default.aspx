<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="MyFirstApplicaton.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">

            <div class="d-flex flex-column">
                <div class="search-bar">
   <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                
   <asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click" />
                </div>

                <div class="d-flex gap-2">
                       <asp:Button ID="BtnRedirect" runat="server"  Text="Response.Redirect" OnClick="BtnRedirect_Click" />
                  <asp:Button ID="BtnTransfer" runat="server" Text="Server.Transfer" OnClick="BtnTransfer_Click" />
                <asp:Button ID="CrossPagePostBack" runat="server" Text="Cross Page posting" PostBackUrl="~/Default.aspx" />
                </div>

            </div>
         
       

    </form>
</body>
</html>
