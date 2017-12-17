<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="EIS.Web.Default" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
    <meta name="viewport" content="width=device-width,minimum-scale=1,maximum-scale=1,initial-scale=no" />
    <meta content="telephone=no" name="format-detection" />
    <link rel="shortcut icon" type="image/x-icon" href="./Img/desktop/favicon.ico" media="screen" />
    <script type="text/javascript" src="./js/jquery-1.7.js"></script>
    <title><%=LoginTitle %></title>
    <style type="text/css">
        body, h1, h2, h3, h4, h5, h6, hr, p, blockquote, dl, dt, dd, ul, ol, li, pre, form, fieldset, legend, button, input, textarea, th, td {
            margin: 0;
            padding: 0;
        }

        body, button, input, select, textarea {
            font-family: 'Open Sans',sans-serif !important;
            background: #ffffff;
            font-size: 100%;
        }
        /* 字体12像素 行高 1.5em 字体 tahoma,arial*/

        h1, h2, h3, h4, h5, h6 {
            font-size: 100%;
        }

        address, cite, dfn, em, var {
            font-style: normal;
        }

        code, kbd, pre, samp {
            font-family: couriernew,courier,monospacode;
        }

        small {
            font-size: 12px;
        }

        ul, ol {
            list-style: none;
        }

        a {
            text-decoration: none;
        }

            a:hover {
                text-decoration: underline;
            }

        sup {
            vertical-align: text-top;
        }

        sub {
            vertical-align: text-bottom;
        }

        legend {
            color: #000;
        }

        fieldset, img {
            border: 0;
        }

        button, input, select, textarea {
            font-size: 100%;
        }

        table {
            border-collapse: collapse;
            border-spacing: 0;
        }

        input[type=text], input[type=password] {
            border: 1px solid gray;
            padding: 5px;
            font-family: arial,"宋体";
            height: 16px;
            font-size: 14px;
            width: 195px;
            color: #444;
            font-weight: bolder;
        }

        #Button1, #Button2 {
            font-family: simsun,sans-serif;
            font-size: 14px;
            padding: 0px;
            width: 90px;
            color: #ffffff;
            background-color: #69b2dc;
            font-weight: bold;
            cursor: pointer;
            border-width: 0px;
            height: 32px;
            width: 206px;
        }

        .homeLogo {
            position: relative;
            width: 949px;
            height: 60px;
            background-image: url(./img/Handlers/getCorpLoginBgLogo.ashx);
            background-repeat: no-repeat;
        }

        .homeBg {
            width: 949px;
            height: 430px;
            background-image: url(./img/Handlers/getCorpLoginBg.ashx);
            background-repeat: no-repeat;
        }

        .loginBox {
            position: fixed;
            width: <%=bgWidth%>;
            height: <%=bgHeight%>;
            left: 50%;
            top: 50%;
            z-index: 11;
            /*设定这个div的margin-top的负值为自身的高度的一半,margin-left的值也是自身的宽度的一半的负值.*/
            /*宽为400,那么margin-top为-200px*/
            /*高为200那么margin-left为-100px;*/
            margin: <%=box_mtop%> 0 0 <%=box_mleft%>;
        }

        .loginInfo {
            width: 320px;
            position: relative;
            top: <%=login_mtop%>;
            left: <%=login_mright%>;
            color: <%=login_Color%>;
        }
    </style>

    <script type="text/javascript" src="./js/jquery-1.7.js"></script>

</head>
<body class="login-bg">
    <form id="form1" runat="server">

        <div class="loginBox">
            <div class="homeLogo"></div>
            <div class="homeBg">

                <div id="wrapper" class="loginInfo">
                    <div class="formcenter">
                        <table>
                            <tbody>
                                <tr style="height: 32px;">
                                    <td>
                                        <label style="font-weight: bolder;" for="txtUser">帐&nbsp;&nbsp;&nbsp;&nbsp;号：</label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtUser" runat="server" CssClass="login_input"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr style="height: 32px;">
                                    <td>
                                        <label style="font-weight: bolder;" for="txtPwd">密&nbsp;&nbsp;&nbsp;&nbsp;码：</label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtPwd" runat="server" CssClass="login_input" TextMode="Password"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr style="<%=verifyCss%>; height: 32px;">
                                    <td></td>
                                    <td>
                                        <img src="VerifyCode.axd" onclick="javascript:this.src='VerifyCode.axd?t='+Math.random();" title="不区分大小写，点击刷新" style="width: 208px; height: 32px; cursor: pointer;" alt="验证码" /></td>
                                </tr>
                                <tr style="<%=verifyCss%>; height: 32px;">
                                    <td>
                                        <label style="font-weight: bolder;" for="txtCode">验证码：</label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtCode" runat="server" CssClass="login_input"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr style="height: 32px;">
                                    <td></td>
                                    <td>
                                        <asp:Button ID="Button1" runat="server" Text="登 录" OnClick="Button1_Click" />
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                        <div style="padding-left: 64px;">
                            <asp:Button ID="Button2" Visible="false" runat="server" Text="域用户登录" OnClick="Button2_Click" />
                        </div>
                        <p style="padding-left: 70px; height: 32px;">
                            <asp:CheckBox ID="CheckBox1" runat="server" Text="记住用户名" Checked="True" />
                            <asp:CheckBox ID="CheckBox2" runat="server" Text="自动登录" />
                        </p>
                        <p style="padding-left: 60px; display: none; height: 32px;">
                            <a class='forgetlink' href="Unsafe/Pass_Recover.aspx" target="_self">忘记密码?</a>
                        </p>
                        <p>
                            <%=TipMsg %>
                        </p>
                    </div>
                </div>


            </div>
        </div>
        <script type="text/javascript">
            $(document).ready(function () {
                $("#txtUser").focus();
                /*
                var wc = $(window).width() / 2;
                var wl = 960 - wc;
                $(".homeLogo").css({ left: wl });
                */
            });
        </script>
    </form>
</body>

</html>
