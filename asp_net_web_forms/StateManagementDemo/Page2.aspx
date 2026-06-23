<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Page2.aspx.cs" Inherits="StateManagementDemo.Page2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
                  Please enter your Qualification: <asp:TextBox ID="TxtQualification" runat="server"></asp:TextBox><br />
      Please enter your Country: <asp:TextBox ID="TxtCountry" runat="server"></asp:TextBox><br />
      <asp:Button ID="BtnNext" runat="server" Text="Finish" OnClick="BtnNext_Click" />
        </div>
    </form>
</body>
</html>
