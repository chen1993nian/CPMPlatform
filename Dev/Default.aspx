<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="EIS.Studio.Default" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>登录</title>
    <link rel="stylesheet" type="text/css" href="css/loginDev.css" />
    <script type="text/javascript" src="./js/jquery-1.7.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $(".VerifyCode").click(function () {
                var clearCaches = new Date().getTime();
                var url = "getVerifyCodeImage.ashx?t=" + clearCaches;
                $(".VerifyCode").prop("src", url);
            });

            $("#txtUser").focus();
        });

    </script>
</head>
<body>
    <form action="Default.aspx" id="form1" runat="server">

        <div id="wrapper">
            <div class="formcenter">
                <table>
                    <tr>
                        <td>
                            <label style="font-weight: bolder;" for="txtUser">帐 号：</label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtUser" runat="server" CssClass="login_input"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label style="font-weight: bolder;" for="txtPwd">密 码：</label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtPwd" runat="server" CssClass="login_input" TextMode="Password"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            <img src="./Img/desktop/verifyCode.png" class="VerifyCode" id="VerifyCode" alt="点击获取验证码" />
                        </td>
                    </tr>
                   <tr>
                        <td>
                            <label style="font-weight: bolder;" for="txtCode">验证码：</label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtCode" runat="server" CssClass="login_input"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            <asp:Button ID="Button1" Width="208px" runat="server" Text="登 录" Font-Size="11px" Height="30px" OnClick="Button1_Click" />
                            <asp:Button ID="Button2" Visible="false" Width="90px" runat="server" Text="域用户登录" Font-Size="11px" Height="30px" OnClick="Button2_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            <asp:CheckBox ID="CheckBox1" runat="server" Text="记住用户名" Checked="True" />
                            <asp:CheckBox ID="CheckBox2" runat="server" Text="自动登录" />
                        </td>
                    </tr>
                </table>
                <div>
                    <%=TipMsg %>
                </div>
            </div>
        </div>
    </form>

</body>

</html>

