<%@ Page Title="C-Sharp Course Details" Language="C#" MasterPageFile="~/CoursesMasterPage.Master" AutoEventWireup="true" CodeBehind="CSharp.aspx.cs" Inherits="MasterPagesDemoApp.CSharp" %>

<asp:Content runat="server" ContentPlaceHolderID="Head">
    <script>
       console.log("C# course page loaded");
   </script>
</asp:Content>
<asp:Content ID="Content1" runat="server" contentplaceholderid="CpdCourseDetails">
    <p>
    C# course Details</p>
    <p>
        <asp:Button ID="BtnDemo" runat="server" OnClick="BtnDemo_Click" Text="Demo" />
    </p>
</asp:Content>
<asp:Content ID="Content2" runat="server" contentplaceholderid="CphStartDate">
    1st March
</asp:Content>


