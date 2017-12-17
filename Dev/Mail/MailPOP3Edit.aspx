<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MailPOP3Edit.aspx.cs" Inherits="EIS.Web.Mail.MailPOP3Edit" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>业务编辑</title>
    <meta http-equiv="Pragma" content="no-cache"/>
    
    <link rel="stylesheet" type="text/css" href="../css/AppStyle.css"/>
    <script type="text/javascript" src="../js/jquery-1.4.2.min.js"></script>
    <script type="text/javascript" src="../js/jquery.validate.min.js"></script>
    
    <script type="text/javascript">
        $(document).ready(function () {

            $(".Read").attr("readonly", true);

            var validator = $("#form1").validate({
                rules: {
                    tb_email: { required: true, email: true },
                    tb_pop3: "required",
                    tb_pop3Port: { required: true, number: true },
                    tb_smtp: "required",
                    tb_smtpPort: { required: true, number: true },
                    tb_account: "required",
                    tb_passwd: "required"
                }
            });
            $("#LinkButton1").click(function () {
                return $("#form1").valid();
            });
        });
    </script>
    <style type="text/css">
        table.tblmain td{
        	height:30px;
        	padding:2px;
        }
        table.defaultstyle caption{font-size:11pt;height:29px;border:1px solid #a0a0a0;line-height:29px;}        
        label{margin-left:5px;}
        a{text-decoration:none;}
        a:hover{color:Red;}
    </style>
</head>
<body  scroll="auto">
    <form id="form1" runat="server">
    <div class="menubar">
        <div class="topnav">
            <ul>
                <li>
                    <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click">保存</asp:LinkButton></li>
                <li><a href="javascript:" onclick="window.close();">关闭</a> </li>
            </ul>
        </div>
    </div>
    <div class="maindiv">
    <br />
        <table class="defaultstyle" style="width: 530px; height: 107px" border="1" align="center" cellpadding="10" cellspacing="0">
            <caption>
                电子邮箱设置
            </caption>
            <tr>
                <td width="180">电子邮件地址：</td>
                <td >
                    <asp:TextBox ID="tb_email" Width="180px" runat="server"  CssClass="TextBoxInChar"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td >接收服务器(POP3)：</td>
                <td >
                <table border="0" width="100%">
                <tr>
                <td width="150px"><asp:TextBox ID="tb_pop3" Width="180px" runat="server"  CssClass="TextBoxInChar"></asp:TextBox></td>
                <td width="50">端口：</td>
                <td><asp:TextBox ID="tb_pop3Port" Width="50px" runat="server"  CssClass="TextBoxInChar"></asp:TextBox></td>
                </tr>
                </table>
                &nbsp;<asp:CheckBox ID="chk_pop3SSL" Width="250px" runat="server" Text="此服务器要求安全连接(SSL) "/>
                </td>
            </tr>            
            <tr>
                <td >发送服务器(SMTP)：</td>
                <td >
                <table border="0" width="100%">
                    <tr>
                    <td width="150px">
                    <asp:TextBox ID="tb_smtp" Width="180px" runat="server"  CssClass="TextBoxInChar"></asp:TextBox>
                    </td>
                    <td width="50">端口：</td>
                    <td><asp:TextBox ID="tb_smtpPort" Width="50px" runat="server"  CssClass="TextBoxInChar"></asp:TextBox>
                    </td>
                    
                    </tr>
                    </table>
                    &nbsp;<asp:CheckBox ID="chk_smtpSSL" Width="250px" runat="server" Text="此服务器要求安全连接(SSL) "/>
                </td>
            </tr>            
            <tr>
                <td >登录账户：</td>
                <td >
                    <asp:TextBox ID="tb_account" Width="180px" runat="server"  CssClass="TextBoxInChar"></asp:TextBox>
                </td>
            </tr>            
            <tr>
                <td >登录密码：</td>
                <td >
                    <asp:TextBox ID="tb_passwd" Width="180px" runat="server"  
                        CssClass="TextBoxInChar" TextMode="Password"></asp:TextBox>
                </td>
            </tr>            
            <tr>
                <td >SMTP需要身份认证：</td>
                <td >
                    <asp:DropDownList ID="ddl_needAuthen" runat="server" Width="100px" Visible="True">
                        <asp:ListItem Selected="True" Value="1">是</asp:ListItem>
                        <asp:ListItem Value="0">否</asp:ListItem>
                    
                    </asp:DropDownList>
                </td>
            </tr>            
            <tr class="hidden">
                <td >自动收取外部邮件：</td>
                <td >
                    <asp:DropDownList ID="ddl_autoRec" runat="server" Width="100px" Visible="True">
                        <asp:ListItem Selected="True" Value="1">是</asp:ListItem>
                        <asp:ListItem Value="0">否</asp:ListItem>
                    
                    </asp:DropDownList>
                </td>
            </tr>            
            <tr>
                <td >默认邮箱：</td>
                <td >
                    <asp:CheckBox runat="server" ID="chk_default" Text="作为内部邮件外发默认邮箱（必须设置账户密码）" />
                </td>
            </tr>            
            <tr class="hidden">
                <td >收信删除：</td>
                <td >
                    <asp:CheckBox runat="server" ID="chk_delAfterRec" Text="收到邮件后从服务器上删除" />
                </td>
            </tr>            
            <tr class="hidden">
                <td >新邮件提醒：</td>
                <td >
                    <asp:CheckBox runat="server" ID="chk_alter" Text="收到新邮件后使用内部短信提醒" />
                </td>
            </tr>

        </table>
        <br />
    </div>
    </form>
</body>
</html>
