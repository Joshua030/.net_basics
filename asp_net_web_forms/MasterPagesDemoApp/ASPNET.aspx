<%@ Page Title="ASP.NET Course Details" Language="C#" MasterPageFile="~/CoursesMasterPage.Master" AutoEventWireup="true" CodeBehind="ASPNET.aspx.cs" Inherits="MasterPagesDemoApp.ASPNET" %>
<%@ MasterType VirtualPath="~/CoursesMasterPage.Master" %>

<asp:Content runat="server" ContentPlaceHolderID="CpdCourseDetails">
    <p>
        ASP.NET course Details
    </p>
    <p>
        <img alt="Nature" class="auto-style5" src="Admin/Images/nature.jpg" /></p> 
    </asp:Content>

<asp:content runat="server" contentplaceholderid="CphStartDate">
    <p>
  15th March
    </p>
  <asp:Button ID="BtnDemo" runat="server" OnClick="BtnDemo_Click" Text="Demo" />
    </asp:content>
<asp:Content ID="Content1" runat="server" contentplaceholderid="head">
    <style type="text/css">
        .auto-style5 {
            width: 320px;
            height: 528px;
        }
    </style>
</asp:Content>

