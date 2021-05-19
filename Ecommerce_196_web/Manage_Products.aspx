<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Manage_Products.aspx.cs" Inherits="Default2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
<title>Manage Products</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div style="margin-left: 30px;text-align:center">
        <asp:Label ID="Label1" runat="server" Text="Manage Products" Font-Size="XX-Large"></asp:Label>
    </div>
    <div style="margin-left: 30px; margin-top: 40px;">
        <a href="AddNewProduct.aspx">Add New Products</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="lblMessgae" runat="server"></asp:Label>
    </div>
    <div style="margin-left: 30px; margin-top: 40px;">
        <asp:DropDownList ID="DropDownList1" runat="server" Height="30px" Width="200px" AutoPostBack="True"
            OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
        </asp:DropDownList>

    </div>

     <div  style="margin-left: 30px; margin-top: 20px;">
       
        <asp:TextBox ID="txtsearch" runat="server"></asp:TextBox>
        <asp:Button ID="btnsearch" runat="server" Text="Search" OnClick="btnsearch_Click"  />
    </div>
   <%-- DataSourceID="SqlDataSource1"--%>
    <div style="margin-left: 30px; margin-top: 40px; margin-bottom: 40px">
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
            Width="1300px" CellPadding="3" ForeColor="Black" GridLines="Vertical" BackColor="White"
            BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px"
            AllowSorting="True" OnPageIndexChanging="GridView1_PageIndexChanging" OnRowCommand="GridView1_RowCommand">
            <AlternatingRowStyle BackColor="#CCCCCC" />
            <Columns>
                <asp:BoundField DataField="Product_Id" HeaderText="Id" InsertVisible="False" ReadOnly="True"
                    SortExpression="Product_Id">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:BoundField>
                <asp:BoundField DataField="ProductName" HeaderText="Name" SortExpression="ProductName">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:BoundField>
                <asp:BoundField DataField="Cost" HeaderText="Cost" SortExpression="Cost">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:BoundField>
                <%--<asp:ImageField HeaderText="Photo" DataImageUrlField="ImagePath" DataImageUrlFormatString="~/{0}"
                    ControlStyle-Height="150px" ControlStyle-Width="150px">
                    <ControlStyle Height="150px" Width="150px"></ControlStyle>
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:ImageField>--%>
               <asp:TemplateField HeaderText="Photo">
                        <ItemTemplate>
                            <asp:LinkButton ID="linkbtnimage" runat="server" CommandName="Image" CommandArgument='<%#Eval("Product_Id")%>'>View Product</asp:LinkButton>
                        </ItemTemplate>
                   <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    </asp:TemplateField>


                <asp:HyperLinkField DataNavigateUrlFields="Product_Id" DataNavigateUrlFormatString="AddNewProduct.aspx?Action=edit&amp;Product_Id={0}"
                    HeaderText="Action" Text="Edit">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
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
        SelectCommand="SELECT * FROM [Product_Master] where  Cat_Id='1'"></asp:SqlDataSource>
</asp:Content>
