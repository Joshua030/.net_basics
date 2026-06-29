<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" CodeBehind="Default.aspx.cs" Inherits="ConfigurationDemoApp.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
           <p>Probando configuraciones Globales</p>
           K1: <%=System.Configuration.ConfigurationManager.AppSettings["k1"] %>
           K2: <%=System.Configuration.ConfigurationManager.AppSettings["k2"] %>
        </div>
        <asp:Button ID="BtnError" runat="server" Text="Throw Error" OnClick="BtnError_Click" />
      <a href="Monitor.aspx">Monitor</a>
      <a href="Mouse.aspx">Mouse</a>
      <a href="Keyboard.aspx">Keyboard</a>
    </form>
</body>
</html>
