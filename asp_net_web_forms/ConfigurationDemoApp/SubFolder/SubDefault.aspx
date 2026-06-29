<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SubDefault.aspx.cs" Inherits="ConfigurationDemoApp.SubFolder.SubDefault" %>

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
   K3: <%=System.Configuration.ConfigurationManager.AppSettings["k3"] %>
        </div>
    </form>
</body>
</html>
