<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="UserCart.aspx.cs" Inherits="Default2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script type="text/javascript">

        window.onload = window.history.forward(0);  //calling function on window onload

    </script>
    <title>User Cart</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div style="text-align: center; margin-top: 20px;">
        <asp:Label ID="Label2" runat="server" Text="User Cart" Font-Size="XX-Large"></asp:Label>
    </div>
    <div style="margin-left: 30px; margin-top: 40px; margin-bottom: 40px; text-align: center;">
        <asp:Label ID="Label1" runat="server" Text="Enter Customer Phone Number"></asp:Label>&nbsp;:</br></br>
        <asp:TextBox ID="txtPhoneNo" runat="server" Height="30px" Width="200px" MaxLength="10"></asp:TextBox>
        </br>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtPhoneNo"
            ErrorMessage="Please enter Phone Number"></asp:RequiredFieldValidator>
        </br>
        <asp:Button ID="btnSubmit" runat="server" Text="Submit" Height="35px" Width="200px"
            OnClick="btnSubmit_Click" />
    </div>
    </br>
    <div style="margin-left: 30px; text-align: center;">
        <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
    </div>
    <div style="margin-left: 30px; margin-top: 40px; margin-bottom: 40px">
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="1300px"
            CellPadding="3" ForeColor="Black" GridLines="Vertical" BackColor="White" BorderColor="#999999"
            BorderStyle="Solid" BorderWidth="1px" OnPageIndexChanging="GridView1_PageIndexChanging">
            <AlternatingRowStyle BackColor="#CCCCCC" />
            <Columns>
                <asp:BoundField DataField="Product_Id" HeaderText="Product Id" InsertVisible="False"
                    ReadOnly="True" SortExpression="Product_Id">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="ProductName" HeaderText="Product Name" SortExpression="ProductName">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="Quantity" HeaderText="Quantity" SortExpression="Quantity">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="Total_Cost" HeaderText="Total Cost" SortExpression="Total_Cost">
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
    <div style="margin-left: 30px; text-align: center">
        <asp:Label ID="lblTotalCost" runat="server" Text=""></asp:Label>
    </div>
    <div style="margin-left: 30px; margin-top: 40px; margin-bottom: 40px; text-align: center">
        <asp:Button ID="btnPrint" runat="server" Text="Pay By Cash" Height="36px" Width="128px"
            OnClick="btnPrint_Click" />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnPayByWallet" runat="server" Text="Pay By Wallet" Height="36px"
            Width="128px" OnClick="btnPayByWallet_Click" />
    </div>
</asp:Content>
