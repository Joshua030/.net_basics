<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="StateManagementDemo.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div>
                <p>Instance Counter: </p>
  <asp:Label ID="LblCounter" runat="server" Text=""></asp:Label>
            </div>
            <div>
                <p>Shared Counter: </p>
  <asp:Label ID="LblSharedCounter" runat="server" Text=""></asp:Label>
            </div>
            <asp:Button ID="BtnNew" runat="server" Text="New" OnClick="BtnNew_Click" />
            <asp:Button ID="BtnEdit" runat="server" Text="Edit" OnClick="BtnEdit_Click" />
            <asp:Button ID="BtnSave" runat="server" Text="Save" OnClick="BtnSave_Click" Visible="False" />
        </div>
    </form>
</body>
</html>
