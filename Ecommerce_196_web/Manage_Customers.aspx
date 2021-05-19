<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="~/Manage_Customers.aspx.cs" Inherits="Default2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
<title>Manage Customers</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div style="margin-left: 30px; text-align: center">
        <asp:Label ID="Label1" runat="server" Text="Manage Customers" Font-Size="XX-Large"></asp:Label>
    </div>

    <div style="margin-left: 30px; margin-top: 40px">
        <a href=" " ></a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="lblMessgae" runat="server" Visible="True"></asp:Label>
    </div>
    <div>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="txtsearch" runat="server"></asp:TextBox>
        <asp:Button ID="btnsearch" runat="server" Text="Search" OnClick="btnsearch_Click" />
    </div>
    <div style="margin-left: 30px; margin-top: 40px; margin-bottom: 40px">
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
            Width="1300px" CellPadding="3" ForeColor="Black" GridLines="Vertical" BackColor="White"
            BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px"
            AllowSorting="True" OnPageIndexChanging="GridView1_PageIndexChanging">
            <AlternatingRowStyle BackColor="#CCCCCC" />
            <Columns>
                <asp:BoundField DataField="C_Id" HeaderText="Id" InsertVisible="False" ReadOnly="True"
                    SortExpression="C_Id">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="CustName" HeaderText="Name" SortExpression="CustName">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="Address" HeaderText="Address" SortExpression="Address">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="PhoneNo" HeaderText="PhoneNo" SortExpression="PhoneNo">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="EmailId" HeaderText="Email Id" SortExpression="EmailId">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
               <%-- <asp:BoundField DataField="Balance" HeaderText="Balance" SortExpression="Balance">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>--%>
               <%-- <asp:HyperLinkField DataNavigateUrlFields="C_Id" DataNavigateUrlFormatString="AddNewCustomer.aspx?Action=edit&amp;C_Id={0}"
                    HeaderText="Action" Text="Edit">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:HyperLinkField>--%>
            </Columns>
            <FooterStyle BackColor="#CCCCCC" />
            <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#F1F1F1" />
            <SortedAscendingHeaderStyle BackColor="#808080" />
            <SortedDescendingCellStyle BackColor="#CAC9C9" />
            <SortedDescendingHeaderStyle BackColor="#383838" />
        </asp:GridView>
    </div>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:constr %>"
        SelectCommand="SELECT * FROM [Customer_Master]"></asp:SqlDataSource>
</asp:Content>
