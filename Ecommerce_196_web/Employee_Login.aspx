<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Employee_Login.aspx.cs" Inherits="Employee_Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Free Adidas Website Template | Login :: w3layouts</title>
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
</head>
<body>
    <div style="margin-left:30px">
        <img src="images/ShoppingImage.jpg" alt="~/" height="350px" width="1300px" />
    </div>
    <div class="login">
        <div class="wrap">
            <div class="col_1_of_login span_1_of_login">
                <div class="login-title">
                    <h4 class="title">
                        Employee Login</h4>
                    <div class="comments-area">
                        <form id="Form1" action="" runat="server"> 
                        <p>
                            <label>
                                Email Id</label>
                            <asp:TextBox ID="txtEmailId" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                ErrorMessage="Email Id Required" ControlToValidate="txtEmailId"></asp:RequiredFieldValidator>
                        </p>
                        
                        <p>
                            <label>
                                Password</label>
                            <span></span>
                           <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox>
                           <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                ErrorMessage="Password Required" ControlToValidate="txtPassword"></asp:RequiredFieldValidator>
                        </p>
                        <p>
                            <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
                        </p>
                        
                        <p>
                            <asp:Button ID="btnLogin" runat="server" Text="Login" 
                                onclick="btnLogin_Click" />
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

