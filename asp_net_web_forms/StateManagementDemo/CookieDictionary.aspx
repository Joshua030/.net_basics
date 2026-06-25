<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CookieDictionary.aspx.cs" Inherits="StateManagementDemo.CookieDictionary" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            Cookie dictionary Name:
            <asp:TextBox ID="TxtDictionaryName" runat="server"></asp:TextBox><br />
            SubKey:
            <asp:TextBox ID="TxtSubKey" runat="server"></asp:TextBox><br />
            Value: 
            <asp:TextBox ID="TxtValue" runat="server"></asp:TextBox><br />
            <asp:Button ID="BtnAddToDictionary" runat="server"  Text="Add to Dictionary" OnClick="BtnAddToDictionary_Click" />
            <asp:Button ID="BtnShowDictionary" runat="server"  Text="Show Dictionary" OnClick="BtnShowDictionary_Click" />
            <br />
            <asp:Label ID="LblValue" runat="server"></asp:Label>
        </div>
    </form>
</body>
</html>
