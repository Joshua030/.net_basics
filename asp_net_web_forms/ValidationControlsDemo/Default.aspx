<%@ Page Language="C#" UnobtrusiveValidationMode="None" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ValidationControlsDemo.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div style="display:flex;flex-direction:column; gap:10px">
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
            <div>
     First Name:
     <asp:TextBox ID="TxtName" runat="server"></asp:TextBox>
            </div>
                       <div>
    Last Name:
    <asp:TextBox ID="TxtLastName" runat="server"></asp:TextBox>
           </div>
                    </div>
                   <div>
Education:

       </div>
       
            <asp:Button ID="BtnSubmit" runat="server" Text="Submit" />
             <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TxtLastName" ErrorMessage="Please provide last name"></asp:RequiredFieldValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TxtName" ErrorMessage="Please provide name"></asp:RequiredFieldValidator>
        </div>
    </form>
</body>
</html>
