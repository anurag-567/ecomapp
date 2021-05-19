<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrintBill.aspx.cs" Inherits="PrintBill" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    
    <title>Print Bill</title>
    <style type="text/css">
        .style1
        {
            width: 39%;
        }
        .style2
        {
            width: 345px;
        }
        .style3
        {
            width: 337px;
        }
        .style4
        {
            width: 160px;
        }
        .style5
        {
            width: 121px;
        }
        .style6
        {
            height: 59px;
        }
        .style7
        {
            height: 58px;
        }
        .style8
        {
            width: 160px;
            height: 60px;
        }
        .style9
        {
            width: 121px;
            height: 60px;
        }
        .style10
        {
            height: 60px;
        }
        .style11
        {
            width: 160px;
            height: 15px;
        }
        .style12
        {
            width: 121px;
            height: 15px;
        }
        .style13
        {
            height: 15px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table class="style1" style="border: 10px solid #04B486">
            <tr align="center">
                <td colspan="3" style="border-bottom: 5px solid #000000" class="style6">
                    Shopping Using NFC
                </td>
            </tr>
            <tr align="center">
                <td colspan="3" class="style7" style="border-bottom: 5px solid #000000">
                    <asp:Label ID="lblPhoneNo" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style8" style="border-right: 5px solid #000000; border-bottom: 5px solid #000000;
                    text-align: center">
                    &nbsp; Product Name
                </td>
                <td class="style9" style="border-right: 5px solid #000000; border-bottom: 5px solid #000000;
                    text-align: center">
                    &nbsp; Quantity
                </td>
                <td class="style10" style="border-bottom: 5px solid #000000; text-align: center">
                    &nbsp; Cost
                </td>
            </tr>
            <asp:Repeater ID="Repeater1" runat="server">
                <HeaderTemplate>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td class="style2" style="border-right: 5px solid #000000; border-bottom: 5px solid #000000;
                            text-align: center">
                            &nbsp;
                            <asp:Label ID="lblProductName" runat="server" Text='<%#Eval("ProductName") %>'></asp:Label>
                        </td>
                        <td class="style3" style="border-right: 5px solid #000000; border-bottom: 5px solid #000000;
                            text-align: center">
                            &nbsp;
                            <asp:Label ID="lblQuantity" runat="server" Text='<%#Eval("Quantity") %>'></asp:Label>
                        </td>
                        <td style="border-bottom: 5px solid #000000; text-align: center">
                            &nbsp;
                            <asp:Label ID="lblCost" runat="server" Text='<%#Eval("Total_Cost") %>'></asp:Label>
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                </FooterTemplate>
            </asp:Repeater>
            <tr>
                <td class="style11">
                </td>
                <td class="style12">
                </td>
                <td class="style13">
                </td>
            </tr>
            <tr>
                <td class="style4">
                    &nbsp;
                    <asp:Label ID="lblMode" runat="server" Text=""></asp:Label>
                </td>
                <td class="style5" align="right">
                    &nbsp;
                    <asp:Label ID="Label1" runat="server" Text="Total Cost: "></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblTotalCost" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style4">
                </td>
                <td class="style5">
                    &nbsp;
                    <asp:Button ID="btnBack" runat="server" Text="Back" OnClick="btnBack_Click" Width="62px" />
                </td>
                <td>
                    <asp:Button ID="btnPrint" runat="server" Text="Print" Height="31px" Width="62px"
                        OnClientClick="javascript:window.print();" OnClick="btnPrint_Click" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
