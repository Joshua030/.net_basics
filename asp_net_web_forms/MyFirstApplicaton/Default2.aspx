<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default2.aspx.cs" Inherits="MyFirstApplicaton.Default2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Label ID="lblDemo" runat="server" Text="This is demo text"></asp:Label>
        <asp:RadioButton ID="rbnRed" runat="server" GroupName="color" Text="Red" OnCheckedChanged="rbnColor_CheckedChanged"  />
        <asp:RadioButton ID="rbnGreen" runat="server"  GroupName="color" Text="Green" OnCheckedChanged="rbnColor_CheckedChanged" />
        <asp:RadioButton ID="rbnBlue" runat="server"  GroupName="color" Text="Blue" OnCheckedChanged="rbnColor_CheckedChanged" />
        <p>
            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Button" />
        </p>
        </form>
</body>
</html>
