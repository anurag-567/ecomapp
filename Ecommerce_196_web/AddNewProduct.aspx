<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="AddNewProduct.aspx.cs" Inherits="Default2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <title>Add New Product</title>
    <style type="text/css">
        .style1 {
            width: 100%;
        }

        .style2 {
            width: 254px;
        }

        td {
            height: 40px;
        }

        .style5 {
            width: 330px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div style="margin-left: 100px; margin-bottom: 40px">
        <table class="style1" style="margin-left: 150px">
            <tr>
                <td class="style2">&nbsp;
                </td>
                <td class="style5">&nbsp;
                    
                    <asp:Label ID="Label1" runat="server" Text="Add New Product"
                        Font-Size="XX-Large"></asp:Label>
                </td>
                <td>&nbsp;
                </td>
            </tr>
            <tr>
                <td class="style2">
                    <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
                </td>
                <td class="style5">&nbsp;</td>
                <td>&nbsp;</td>
            </tr>

            <%--<div class="button1">--%>
            <%if (Request.QueryString["Action"] == "edit")
                { %>
            <tr>
                <td class="style2">&nbsp;
                </td>
                <td class="style5">&nbsp;
                    <asp:Image ID="Image1" runat="server" Height="200px" Width="200px" />
                </td>
                <td>&nbsp;
                </td>
            </tr>
            <%} %>
            <%--   </div>--%>
            <tr>
                <td class="style2">Category :</td>
                <td class="style5">&nbsp;
                    <asp:DropDownList ID="DropDownList1" runat="server" Height="30px" Width="260px">
                    </asp:DropDownList>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="style2">Product Name :
                </td>
                <td class="style5">&nbsp;
                    <asp:TextBox ID="txtProductName" runat="server" Height="30px" Width="260px"></asp:TextBox>
                </td>
                <td>&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtProductName"
                    ErrorMessage="Product Name Required" Display="Dynamic"></asp:RequiredFieldValidator>
                   <%-- <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtProductName" Display="Dynamic" ErrorMessage="Invalid Name" ValidationExpression="^[A-Z][a-zA-Z]*$"></asp:RegularExpressionValidator>--%>
                </td>
            </tr>
            <tr>
                <td class="style2">Image :
                </td>
                <td class="style5">&nbsp;
                    <asp:FileUpload ID="FileUpload1" runat="server" Height="30px" Width="260px" />
                </td>
                <td>

                    <%if (Request.QueryString["Action"] != "edit")
                        {%>
                    &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="FileUpload1"
                        ErrorMessage="Image Required" Display="Dynamic"></asp:RequiredFieldValidator>
                    <%} %>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="FileUpload1" ValidationExpression="([a-zA-Z0-9\s_\\.\-:])+(.png|.jpg|.gif|.PNG|.JPEG|.jpeg)$" runat="server" ErrorMessage="Only Images" Display="Dynamic"></asp:RegularExpressionValidator>
                </td>
                <td></td>
            </tr>
            <tr>
                <td class="style2">Cost : 
                </td>
                <td class="style5">&nbsp;
                    <asp:TextBox ID="txtCost" runat="server" Height="30px" Width="260px"></asp:TextBox>
                </td>
                <td>&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtCost"
                    ErrorMessage="Cost Required"></asp:RequiredFieldValidator>
                    <br />
                    &nbsp;<asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server"
                        ControlToValidate="txtCost" ErrorMessage="Enter Only number"
                        ValidationExpression="^[0-9]*$"></asp:RegularExpressionValidator>
                    <br />
                    &nbsp;</td>
            </tr>

            <tr>
                <td class="style2">&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>

            <tr>
                <td class="style2">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" Height="45px"
                        Width="120px" BackColor="Black" ForeColor="White" />
                </td>
                <td>
                    <%-- <div class="button1">--%>
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnDelete" runat="server" Text="Delete" OnClick="btnDelete_Click"
                        Height="45px" Width="120px" BackColor="Black" ForeColor="White"
                        Enabled="False" />
                    <%-- </div>--%>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnReset" runat="server" Text="Reset" CausesValidation="False" OnClick="btnReset_Click"
                        Height="45px" Width="120px" BackColor="Black" ForeColor="White" />
                </td>
                <td>
                    <%-- </div>--%>
                    <%-- </div>--%>
                    <asp:Button ID="btnBack" runat="server" BackColor="Black" CausesValidation="False" ForeColor="White" Height="45px" Text="Back" Width="120px" OnClick="btnBack_Click" />
                </td>
            </tr>
            <tr>
                <td class="style2">&nbsp;
                </td>
                <td class="style5">&nbsp;
                </td>
                <td>&nbsp;
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
