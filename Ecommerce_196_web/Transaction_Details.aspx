<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Transaction_Details.aspx.cs" Inherits="Default2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
<title>Transaction Details</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div style="margin-left: 30px; margin-top: 40px; text-align: center">
        <asp:Label ID="Label1" runat="server" Text="History" Font-Size="XX-Large"></asp:Label>
    </div>
    <div style="margin-left: 30px; margin-top: 40px;">
        <asp:Button ID="btnsendinvoice" runat="server" Text="Send Invoice" OnClick="btnsendinvoice_Click" />
    </div>
    <div style="margin-left: 30px; margin-top: 40px; margin-bottom: 40px">
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1"
            Width="1300px" CellPadding="3" ForeColor="Black" GridLines="Vertical" BackColor="White"
            BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" 
            AllowPaging="True" AllowSorting="True">
            <AlternatingRowStyle BackColor="#CCCCCC" />
            <Columns>
                <asp:BoundField DataField="ProductName" HeaderText="Product Name" InsertVisible="False"
                    ReadOnly="True" SortExpression="ProductName">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="Quantity" HeaderText="Quantity" SortExpression="Quantity">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="Total_Cost" HeaderText="Total_Cost" SortExpression="Total_Cost">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
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
        SelectCommand="select T.T_Id,T.Datetime,C.PhoneNo,E.EmailId From Customer_Master C JOIN Transaction_Master T on C.C_Id=T.C_Id JOIN Employee_Master E on E.Emp_Id=T.Emp_Id">
    </asp:SqlDataSource>
</asp:Content>
