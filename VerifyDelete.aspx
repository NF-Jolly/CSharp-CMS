<%@ Page Title="" Language="C#" MasterPageFile="~/MP.master" AutoEventWireup="true" CodeFile="VerifyDelete.aspx.cs" Inherits="VerifyDelete" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
Please verify you wish to delete this information:<br />
    <asp:Label ID="lDeleteMsg" runat="server" Text=""></asp:Label>
    <br /><br />
    <asp:Button ID="bDelete" runat="server" Text="Delete" OnClick="bDelete_Click" />
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Button ID="bCancel" runat="server" Text="Nevermind" OnClick="bCancel_Click" />

</asp:Content>

