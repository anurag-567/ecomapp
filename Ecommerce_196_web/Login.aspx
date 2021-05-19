<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Admin_Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Login</title>
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="css/style.css" rel="stylesheet" type="text/css" media="all" />
    <link href='http://fonts.googleapis.com/css?family=Open+Sans:400,300,600,700,800'
        rel='stylesheet' type='text/css'>
    <script type="text/javascript" src="js/jquery.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $(".dropdown img.flag").addClass("flagvisibility");

            $(".dropdown dt a").click(function () {
                $(".dropdown dd ul").toggle();
            });

            $(".dropdown dd ul li a").click(function () {
                var text = $(this).html();
                $(".dropdown dt a span").html(text);
                $(".dropdown dd ul").hide();
                $("#result").html("Selected value is: " + getSelectedValue("sample"));
            });

            function getSelectedValue(id) {
                return $("#" + id).find("dt a span.value").html();
            }

            $(document).bind('click', function (e) {
                var $clicked = $(e.target);
                if (!$clicked.parents().hasClass("dropdown"))
                    $(".dropdown dd ul").hide();
            });


            $("#flagSwitcher").click(function () {
                $(".dropdown img.flag").toggleClass("flagvisibility");
            });
        });
    </script>
    <!-- start menu -->
    <link href="css/megamenu.css" rel="stylesheet" type="text/css" media="all" />
    <script type="text/javascript" src="js/megamenu.js"></script>
    <script>        $(document).ready(function () { $(".megamenu").megamenu(); });</script>
    <!-- end menu -->
    <!-- top scrolling -->
    <script type="text/javascript" src="js/move-top.js"></script>
    <script type="text/javascript" src="js/easing.js"></script>
    <script type="text/javascript">
        jQuery(document).ready(function ($) {
            $(".scroll").click(function (event) {
                event.preventDefault();
                $('html,body').animate({ scrollTop: $(this.hash).offset().top }, 1200);
            });
        });
    </script>
    <style>
        .comments-area{

  -webkit-box-shadow: 3px 3px 5px 6px #ccc;  /* Safari 3-4, iOS 4.0.2 - 4.2, Android 2.3+ */
  -moz-box-shadow:    3px 3px 5px 6px #ccc;  /* Firefox 3.5 - 3.6 */
  box-shadow:         3px 3px 5px 6px #ccc;  /* Opera 10.5, IE 9, Firefox 4+, Chrome 6+, iOS 5 */
        }

        .btn{
            width:90%;
            margin-right:20px
        }

        .txt{
            margin-left:10px;
            margin-right:10px
        }
    </style>
</head>
<body>
    <div style="margin-left: 30px">
        
       <%-- <img src="images/ShoppingImage.jpg" alt="~/" height="350px" width="1300px" />--%>
    </div>
    <div class="login" style="background:url('images/back_.jpg') no-repeat;background-size:cover;height:100vh">
        <%-- <div class="wrap">--%>
        <div style="margin-left:500px;">
            <div class="col_1_of_login span_1_of_login">
                 <p style="font-size:xx-large;text-align:center;color:black"> E-SHETKARI</p>
                <br /><br /><br /><br />
                <div class="login-title" style="background-color:white">

                    <div class="comments-area" style="height:320px;text-align:center">
                        <form action="" runat="server">
                        <div>
                            <asp:Label ID="lblLogout" runat="server" Text=""></asp:Label>
                        </div>
                        <p>
                           
                            <asp:DropDownList ID="DropDownList1" runat="server" Height="30px" Width="150px" Visible="false">
                                <asp:ListItem>Admin</asp:ListItem>
                               <%-- <asp:ListItem>Employee</asp:ListItem>--%>
                            </asp:DropDownList>
                        </p>
                        <p>
                            <label>
                                Username</label>
                            <span></span>
                            <asp:TextBox ID="txtUsername" runat="server" Width="350px" CssClass="txt"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Username Required"
                                ControlToValidate="txtUsername"></asp:RequiredFieldValidator>
                        </p>
                        <p>
                            <label>
                                Password</label>
                            <span></span>
                            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" Width="350px" CssClass="txt"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Password Required"
                                ControlToValidate="txtPassword"></asp:RequiredFieldValidator>
                        </p>
                       
                        <p>
                            <asp:Button ID="btnLogin" runat="server" Text="Login" OnClick="btnLogin_Click" CssClass="btn" />
                        </p>
                        </form>
                    </div>
                </div>
            </div>
            <div class="clear">
            </div>
        </div>
    </div>
</body>
</html>
