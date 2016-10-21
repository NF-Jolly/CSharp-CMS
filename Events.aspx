<%@ Page Title="" Language="C#" MasterPageFile="~/MP.master" AutoEventWireup="true" CodeFile="Events.aspx.cs" Inherits="Events" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   
        <br />
    <asp:Label ID="lErrMsg" runat="server" Text=" " ForeColor="#FF3300"></asp:Label>
    <br />
    <asp:Label ID="lName" runat="server" Text="Label"></asp:Label>
&nbsp;
    <asp:Label ID="lRelationship" runat="server" Text="Label"></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Button ID="bBack" runat="server" Text="Back" OnClick="bBack_Click" />
    <br />
    <br />


    <asp:Table ID="Table1" runat="server" Height="272px">
    <asp:TableRow><asp:TableCell>
    Date:</asp:TableCell><asp:TableCell><asp:TextBox ID="tbDate" runat="server" type="date"></asp:TextBox>
        </asp:TableCell></asp:TableRow><asp:TableRow><asp:TableCell>
    Start Time:</asp:TableCell><asp:TableCell><asp:TextBox ID="tbStartTime" runat="server" type="Time"></asp:TextBox>
                </asp:TableCell></asp:TableRow><asp:TableRow><asp:TableCell>
    Stop Time: </asp:TableCell><asp:TableCell><asp:TextBox ID="tbEndTime" runat="server" type="Time"></asp:TextBox>
                </asp:TableCell></asp:TableRow><asp:TableRow><asp:TableCell>
    Subject:</asp:TableCell><asp:TableCell><asp:TextBox ID="tbSubject" runat="server"></asp:TextBox>
                 </asp:TableCell></asp:TableRow><asp:TableRow><asp:TableCell>
    Location:</asp:TableCell><asp:TableCell><asp:TextBox ID="tbLocation" runat="server"></asp:TextBox>
                 </asp:TableCell></asp:TableRow><asp:TableRow><asp:TableCell>
   Description:</asp:TableCell><asp:TableCell>
        <textarea id="taDescription" cols="30" rows="5" runat="server"></textarea>
        </asp:TableCell></asp:TableRow>
        <asp:TableRow><asp:TableCell>
        </asp:TableCell><asp:TableCell>
              <asp:Button ID="bSave" runat="server" Text="Save Event" OnClick="bSave_Click" />
        </asp:TableCell></asp:TableRow>

        <asp:TableRow><asp:TableCell>
        </asp:TableCell><asp:TableCell>
        <asp:Button ID="bDelete"  runat="server" Text="Delete" OnClick="bDelete_Click" Visible="False" />

                </asp:TableCell></asp:TableRow>
    </asp:Table>


    <br />
      
</asp:Content>