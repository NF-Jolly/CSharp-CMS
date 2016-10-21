<%@ Page Title="" Language="C#" MasterPageFile="~/MP.master" AutoEventWireup="true" CodeFile="ContactInfo.aspx.cs" Inherits="ContactInfo" %>

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


    Add Phone Number: <br />
    <asp:DropDownList ID="ddType" runat="server">
        <asp:ListItem>Home</asp:ListItem>
        <asp:ListItem>Office</asp:ListItem>
        <asp:ListItem>Cell</asp:ListItem>
        <asp:ListItem>School</asp:ListItem>
        <asp:ListItem>Misc1</asp:ListItem>
        <asp:ListItem>Misc2</asp:ListItem>
        <asp:ListItem></asp:ListItem>
    </asp:DropDownList>
    <asp:TextBox ID="tbPhoneNo" runat="server" OnTextChanged="tbPhoneNo_TextChanged" AutoPostBack="True"></asp:TextBox>
    
&nbsp;<asp:SqlDataSource ID="SqlPhoneNos" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT * FROM [phoneNos] WHERE ([ID] = @ID)" DeleteCommand="DELETE FROM [phoneNos] WHERE [ID] = @ID AND [type] = @type" InsertCommand="INSERT INTO [phoneNos] ([ID], [type], [phoneNo]) VALUES (@ID, @type, @phoneNo)" UpdateCommand="UPDATE [phoneNos] SET [phoneNo] = @phoneNo WHERE [ID] = @ID AND [type] = @type">
        <DeleteParameters>
            <asp:Parameter Name="ID" Type="Int32" />
            <asp:Parameter Name="type" Type="String" />
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="ID" Type="Int32" />
            <asp:Parameter Name="type" Type="String" />
            <asp:Parameter Name="phoneNo" Type="String" />
        </InsertParameters>
        <SelectParameters>
            <asp:SessionParameter Name="ID" SessionField="contactId" Type="Int32" />
        </SelectParameters>
        <UpdateParameters>
            <asp:Parameter Name="phoneNo" Type="String" />
            <asp:Parameter Name="ID" Type="Int32" />
            <asp:Parameter Name="type" Type="String" />
        </UpdateParameters>
    </asp:SqlDataSource>
    <asp:GridView ID="gvPhoneNos" runat="server" AutoGenerateColumns="False" CellPadding="4" DataKeyNames="ID,type" DataSourceID="SqlPhoneNos" ForeColor="#333333" GridLines="None">
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        <Columns>
            <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
            <asp:BoundField DataField="ID" HeaderText="ID" ReadOnly="True" SortExpression="ID" Visible="False" />
            <asp:BoundField DataField="type" HeaderText="type" ReadOnly="True" SortExpression="type" />
            <asp:BoundField DataField="phoneNo" HeaderText="phoneNo" SortExpression="phoneNo" />
        </Columns>
        <EditRowStyle BackColor="#999999" />
        <FooterStyle BackColor="#5D7B9D" ForeColor="White" Font-Bold="True" />
        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
        <SortedAscendingCellStyle BackColor="#E9E7E2" />
        <SortedAscendingHeaderStyle BackColor="#506C8C" />
        <SortedDescendingCellStyle BackColor="#FFFDF8" />
        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
    </asp:GridView>
    <br />
    <asp:Label ID="phoneMsg" runat="server" Text=""></asp:Label>
    <br />


    ___________________________________________________________<br /><br />
Add Address:&nbsp;
    <asp:DropDownList ID="ddAddress" runat="server">
        <asp:ListItem>Home</asp:ListItem>
        <asp:ListItem>School</asp:ListItem>
        <asp:ListItem>Parents</asp:ListItem>
        <asp:ListItem>Office</asp:ListItem>
        <asp:ListItem>Miisc1</asp:ListItem>
        <asp:ListItem>Misc2</asp:ListItem>
        <asp:ListItem></asp:ListItem>
    </asp:DropDownList><br />
    Mail Stop:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <asp:TextBox ID="tbMailStop" runat="server" ToolTip="Optional" MaxLength="50"></asp:TextBox><br />
    Street Adds:&nbsp;&nbsp;&nbsp; <asp:TextBox ID="tbStreet" runat="server" MaxLength="50"></asp:TextBox><br />
    City St Zip:&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:TextBox ID="tbCity" runat="server" MaxLength="50"></asp:TextBox>
    &nbsp;<asp:TextBox ID="tbSt" runat="server" MaxLength="2" Width="20px"></asp:TextBox>
    &nbsp;<asp:TextBox ID="tbZip" runat="server" OnTextChanged="tbZip_TextChanged" ToolTip="Once you enter the zip code and press tab the address will be stored" AutoPostBack="True" MaxLength="10"></asp:TextBox>
    <br />
    <br />


    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" DeleteCommand="DELETE FROM [addresses] WHERE [ID] = @ID AND [type] = @type" InsertCommand="INSERT INTO [addresses] ([ID], [type], [MailStop], [streetAddress], [City], [St], [zip]) VALUES (@ID, @type, @MailStop, @streetAddress, @City, @St, @zip)" SelectCommand="SELECT * FROM [addresses] WHERE ([ID] = @ID)" UpdateCommand="UPDATE [addresses] SET [MailStop] = @MailStop, [streetAddress] = @streetAddress, [City] = @City, [St] = @St, [zip] = @zip WHERE [ID] = @ID AND [type] = @type">
        <DeleteParameters>
            <asp:Parameter Name="ID" Type="Int32" />
            <asp:Parameter Name="type" Type="String" />
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="ID" Type="Int32" />
            <asp:Parameter Name="type" Type="String" />
            <asp:Parameter Name="MailStop" Type="String" />
            <asp:Parameter Name="streetAddress" Type="String" />
            <asp:Parameter Name="City" Type="String" />
            <asp:Parameter Name="St" Type="String" />
            <asp:Parameter Name="zip" Type="String" />
        </InsertParameters>
        <SelectParameters>
            <asp:SessionParameter Name="ID" SessionField="contactId" Type="Int32" />
        </SelectParameters>
        <UpdateParameters>
            <asp:Parameter Name="MailStop" Type="String" />
            <asp:Parameter Name="streetAddress" Type="String" />
            <asp:Parameter Name="City" Type="String" />
            <asp:Parameter Name="St" Type="String" />
            <asp:Parameter Name="zip" Type="String" />
            <asp:Parameter Name="ID" Type="Int32" />
            <asp:Parameter Name="type" Type="String" />
        </UpdateParameters>
    </asp:SqlDataSource>
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="ID,type" DataSourceID="SqlDataSource1" CellPadding="4" ForeColor="#333333" GridLines="None">
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        <Columns>
            <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
            <asp:BoundField DataField="ID" HeaderText="ID" ReadOnly="True" SortExpression="ID" Visible="False" />
            <asp:BoundField DataField="type" HeaderText="type" ReadOnly="True" SortExpression="type" />
            <asp:BoundField DataField="MailStop" HeaderText="MailStop" SortExpression="MailStop" />
            <asp:BoundField DataField="streetAddress" HeaderText="streetAddress" SortExpression="streetAddress" />
            <asp:BoundField DataField="City" HeaderText="City" SortExpression="City" />
            <asp:BoundField DataField="St" HeaderText="St" SortExpression="St" />
            <asp:BoundField DataField="zip" HeaderText="zip" SortExpression="zip" />
        </Columns>
        <EditRowStyle BackColor="#999999" />
        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
        <SortedAscendingCellStyle BackColor="#E9E7E2" />
        <SortedAscendingHeaderStyle BackColor="#506C8C" />
        <SortedDescendingCellStyle BackColor="#FFFDF8" />
        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
    </asp:GridView>


    <br />
    <br />


</asp:Content>

