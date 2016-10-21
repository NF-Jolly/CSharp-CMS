<%@ Page Title="" Language="C#" MasterPageFile="~/MP.master" AutoEventWireup="true" CodeFile="Admin.aspx.cs" Inherits="Admin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Button ID="bCreateDB" runat="server" Text="CreateDB" OnClick="bCreateDB_Click" />

    <br />
    <br />
    <br />

    <asp:Button ID="bCreateTestData" runat="server" Text="Create Test Data" OnClick="bCreateTestData_Click" />
</asp:Content>

