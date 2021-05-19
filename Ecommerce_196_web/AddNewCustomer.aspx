<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="AddNewCustomer.aspx.cs" Inherits="Default2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <title>Add New Customer</title>
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .style2
        {
            width: 254px;
        }
        
        td
        {
            height: 40px;
        }
        .style5
        {
            width: 330px;
        }
        .style6
        {
            width: 254px;
            height: 48px;
        }
        .style7
        {
            width: 330px;
            height: 48px;
        }
        .style8
        {
            height: 48px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div style="margin-left: 100px; margin-bottom: 40px">
        <table class="style1" style="margin-left:150px">
            <tr>
                <td class="style2">
                    &nbsp;
                </td>
                <td class="style5">
                    &nbsp;
                    <asp:Label ID="Label1" runat="server" Text="Add New Customer" 
                        Font-Size="XX-Large"></asp:Label>
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="style2">
                    &nbsp;
                    <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
                </td>
                <td class="style5">
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="style2">
                    Customer Name :
                </td>
                <td class="style5">
                    &nbsp;
                    <asp:TextBox ID="txtCustName" runat="server" Height="30px" Width="260px"></asp:TextBox>
                </td>
                <td>
                    &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtCustName"
                        ErrorMessage="Customer Name Required" Display="Dynamic"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtCustName" Display="Dynamic" ErrorMessage="Invalid Name" ValidationExpression="^[a-zA-Z\s]+$"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td class="style2">
                    Address :
                </td>
                <td class="style5">
                    &nbsp;
                    <asp:TextBox ID="txtAddress" runat="server" Height="67px" Width="260px" TextMode="MultiLine"></asp:TextBox>
                </td>
                <td>
                    &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtAddress"
                        ErrorMessage="Address Required"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="style2">
                    Phone Number :
                </td>
                <td class="style5">
                    &nbsp;
                    <asp:TextBox ID="txtPhoneNo" runat="server" Height="30px" Width="260px" 
                        MaxLength="10"></asp:TextBox>
                </td>
                <td>
                    &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtPhoneNo"
                        ErrorMessage="Phone Number Required"></asp:RequiredFieldValidator>
                    &nbsp;<br />
                    &nbsp;<asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                        ControlToValidate="txtPhoneNo" ErrorMessage="Invalid Phone Number" ValidationExpression="\d{10}"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td class="style6">
                    Email Id :
                </td>
                <td class="style7">
                    &nbsp;
                    <asp:TextBox ID="txtEmailId" runat="server" Height="30px" Width="260px"></asp:TextBox>
                </td>
                <td class="style8">
                    &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtEmailId"
                        ErrorMessage="Email Id Required"></asp:RequiredFieldValidator>
                    <br />
                    &nbsp;<asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server"
                        ControlToValidate="txtEmailId" ErrorMessage="Invalid Email Id" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td class="style2">
                    Balance :
                </td>
                <td class="style5">
                    &nbsp;
                    <asp:TextBox ID="txtBalance" runat="server" Height="30px" Width="260px"></asp:TextBox>
                </td>
                <td>
                    &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtBalance"
                        ErrorMessage="Balance Required"></asp:RequiredFieldValidator>
                    <br />
                    &nbsp;<asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server"
                        ControlToValidate="txtBalance" ErrorMessage="Enter Only number" ValidationExpression="^[0-9]*$"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td class="style2">
                    &nbsp;</td>
                <td>
                     &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style2">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" 
                         Height="45px" Width="120px" BackColor="Black" ForeColor="White"/>
                </td>
                <td>
                     <%--<div class="button1">--%>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnDelete" runat="server" Text="Delete" 
                         OnClick="btnDelete_Click" Height="45px" Width="120px" BackColor="Black" 
                         ForeColor="White" Enabled="False"/>
                   <%--   </div>--%>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnReset" runat="server" Text="Reset" CausesValidation="False" OnClick="btnReset_Click" Height="45px" Width="120px" BackColor="Black" ForeColor="White"/>
                </td>
                <td>
                   <%-- <div class="button1">--%>
                   <%-- </div>--%>
                    <asp:Button ID="btnBack" runat="server" BackColor="Black" CausesValidation="False" ForeColor="White" Height="45px"  Text="Back" Width="120px" OnClick="btnBack_Click" />
                </td>
            </tr>
            <tr>
                <td class="style2">
                    &nbsp;
                </td>
                <td class="style5">
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
