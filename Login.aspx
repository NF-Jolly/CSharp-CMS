<%@ Page Title="" Language="C#" MasterPageFile="~/MP.master" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


    <asp:TextBox ID="tbUser" runat="server"></asp:TextBox>
    <br />
    <br />

    <asp:TextBox ID="tbPw1" TextMode ="Password" runat="server"></asp:TextBox>
    <br />
    <br />

    <asp:TextBox ID="tbPw2" runat="server"></asp:TextBox>
    <br />
    <br />

    <asp:Button ID="bLogin" runat="server" Text="Code in please" OnClick="bLogin_Click" />




</asp:Content>

