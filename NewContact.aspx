<%@ Page Title="" Language="C#" MasterPageFile="~/MP.master" AutoEventWireup="true" CodeFile="NewContact.aspx.cs" Inherits="NewContact" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   
    <asp:Label ID="NewContactMsg" runat="server" AutoPostBack="True" Text="Enter name, DOB and relationship"></asp:Label>
    <br />
    <br />

     Last Name: <asp:TextBox ID="tbLname" runat="server" OnTextChanged="tbLname_TextChanged" AutoPostBack="True" ToolTip="Enter the name of a new contact -OR- the first 3 characters of an existing contact then press the tab key"  ></asp:TextBox>
        <br /><br />
     First Name: <asp:TextBox ID="tbFname" runat="server"></asp:TextBox>
         <br /><br />

    
    <asp:ScriptManager runat="server" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>

    <asp:ListBox ID="lbContacts" runat="server" AutoPostBack="True" Height="100px" Width="300" OnSelectedIndexChanged="lbContacts_SelectedIndexChanged" ToolTip="To modify and existing contact -OR- Schedule a meeting click on the contacts name"></asp:ListBox>

    </ContentTemplate>
        </asp:UpdatePanel>

    
    Birth Date: <asp:TextBox ID="tbDOB" runat="server" type="date" />
     

    <br />
    Relationship:&nbsp;
    <asp:TextBox ID="tbRelationship" runat="server"></asp:TextBox>
    <br /><br />
    <asp:Button ID="bSaveContact" runat="server" Text="Save Contact" OnClick="bSaveContact_Click" />
    <asp:Label ID="lStatus" runat="server" Text=""></asp:Label>
     <br /><br />
    <asp:Button ID="bMore" runat="server" Text="More Information" OnClick="bMore_Click" Visible="False" ToolTip="Click here enter phone numbers and addresses " />
    <br />
    <br />
    <asp:Button ID="bEvents" runat="server" Text="Schedule Event" OnClick="bEvents_Click" Visible="False" ToolTip="Click here to schedule an event with this contact" />
    <br />
    <br />
    <asp:Button ID="bDelete" runat="server" Text="Delete Contact" OnClick="bDelete_Click" Visible="False" />
</asp:Content>

