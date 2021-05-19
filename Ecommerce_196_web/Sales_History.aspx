<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Sales_History.aspx.cs" Inherits="Default2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
<title>Sales History</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div style="margin-left: 30px; text-align:center">
        <asp:Label ID="Label1" runat="server" Text="History" Font-Size="XX-Large"></asp:Label>
    </div>
       <div  style="margin-left: 30px; margin-top: 20px;">
       
        <asp:TextBox ID="txtsearch" runat="server"></asp:TextBox>
        <asp:Button ID="btnsearch" runat="server" Text="Search" OnClick="btnsearch_Click"/>
    </div>
    <div style="margin-left: 30px; margin-top: 40px; margin-bottom: 40px" >
        <%--DataSourceID="SqlDataSource1"--%>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
            Width="1300px" CellPadding="3" ForeColor="Black" GridLines="Vertical" BackColor="White"
            BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" 
             AllowSorting="True" OnPageIndexChanging="GridView1_PageIndexChanging">
            <AlternatingRowStyle BackColor="#CCCCCC" />
            <Columns>
                  <asp:BoundField DataField="CustName" HeaderText="Customer Name" InsertVisible="False" ReadOnly="True"
                    SortExpression="custname">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="PhoneNo" HeaderText="Customer PhoneNo" SortExpression="PhoneNo">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>


                <asp:BoundField DataField="T_Id" HeaderText="T_Id" InsertVisible="False" ReadOnly="True"
                    SortExpression="T_Id">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                
              
               <%-- <asp:BoundField DataField="DateTime" HeaderText="DateTime" SortExpression="DateTime">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>--%>
                <asp:HyperLinkField DataNavigateUrlFields="T_Id" DataNavigateUrlFormatString="Transaction_Details.aspx?Action=edit&amp;T_Id={0}"
                    HeaderText="Action" Text="View">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:HyperLinkField>
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
        SelectCommand=" select  C.CustName,C.PhoneNo, T.T_Id,T.Datetime From Customer_Master C JOIN Transaction_Master T on C.C_Id=T.C_Id"></asp:SqlDataSource>
</asp:Content>
