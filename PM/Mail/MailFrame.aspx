<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MailFrame.aspx.cs" Inherits="EIS.Web.Mail.MailFrame" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>电子邮件</title>
    <meta http-equiv="Pragma" content="no-cache" />
    <script type="text/javascript" src="../js/jquery-1.4.2.min.js"></script>
    <style type="text/css">
        *{margin:0px;padding:0px;}
        html{height:100%;width:100%;}
        body{color:#222;line-height:1.6666;font-size:12px;font-family:"Microsoft Yahei",verdana;height:100%;width:100%;overflow:hidden;}
        .leftzone{background:white;width:220px;border:3px solid #608eb7;float:left;
                  height:100%;
                  border-collapse:collapse;
                  }

        .lefttitle{background:#222 url(../img/email/leftmenu-title-bg.png) repeat-x;color:White;height:26px;line-height:26px; font-weight:bold;}
        .dvNavTop{height:40px;background-color:#95bcda;color:White;padding:8px;}
        .qf{background:#95bcda url(../img/email/skin_163blue.png) no-repeat 0px 0px;
            height:40px;
            border:1px solid #95bcda;
            width:200px;
            position:relative;
            }
        .KS{display:block;float:left;width:30px;position:relative;}
        .sx{background:transparent url(../img/email/skin_163blue.png) no-repeat 8px -184px;height:30px;}
        .xx{background:transparent url(../img/email/skin_163blue.png) no-repeat -57px -184px;height:30px;}
        .kT{display:block;float:left;width:60px;color: rgb(60, 102, 146); font-size:14px;font-weight:bold;
            text-decoration:none;
            position:relative;
            line-height:30px;
            }
        .kT:hover{color:red;}
        ul{list-style-type:none;list-style-image:none;list-style-position:outside;margin:5px 0px;}
        li{font-family:Myriad, Helvetica, Tahoma, Arial, clean, sans-serif;
            padding-left:23px;color:#1e5494;line-height:23px;
            cursor:hand;
            }
        .li_hover{background-color:#bfd1e2;color:blue;}
        .li_selected{background-color:#95bcda;color:red;}
        .sb_Line{border-top:1px solid #bfd1e2;height:2px;}
        .icoright{float:left;line-height:24px;}
        td{padding:0px;}
        a{text-decoration:none;color:Blue;}
    </style>
    <script type="text/javascript">
        jQuery(function () {
            jQuery("li").hover(function () {
                $(this).addClass("li_hover");
            }, function () {
                $(this).removeClass("li_hover");
            });
            $(".menu").click(function () {
                $(".li_selected").removeClass("li_selected");
                $(this).addClass("li_selected");
                var url = $(this).attr("url");
                if (url) {
                    document.getElementById("mainframe2").src = url;
                }
            });

            $("#mainframe2").width($(document.body).width() - 226);
            $("#mainframe2").height($(document.body).height() - 5);

        });
    </script>
</head>
<body>
<table class="leftzone" border="0">
<tr>
    <td valign="top"  width="220">
        <div class="lefttitle">
        <img src="../img/email/mail.png" alt="电子邮件" style="margin-left:10px;vertical-align:middle;" />
        &nbsp;电子邮件
        </div>
        <div class="dvNavTop">
        <div class="qf">
            <div class="KS sx">&nbsp;&nbsp;</div>
            <a hideFocus="true" class="kT" href="MailReceive.aspx?folderId=0" target="mainframe2">收 信</a>
            <div class="KS xx">&nbsp;&nbsp;</div>
            <a hideFocus="true" class="kT" href="MailWrite.aspx" target="mainframe2">写 信</a>
        </div>
        </div>
        <ul class="menuEmail">
            <li title="写邮件" url="MailWrite.aspx" class='menu'>写邮件</li>
            <li title="收件箱" url="MailReceive.aspx?folderId=0" class='menu li_selected'>收件箱</li>
            <li title="草稿箱" url="MailDraft.aspx" class='menu'>草稿箱</li>
            <li title="已发送" url="MailSend.aspx" class='menu'>已发送</li>
            <li title="垃圾箱" url="MailTrash.aspx" class='menu'>回收站</li>
            <!--<li title="邮箱设置" url="MailPOP3.aspx" class='menu'>邮箱设置</li>-->
        </ul>
        <div class="sb_Line">
        </div>
    <ul class="menuOther">
        <li class='menu' style="padding-left:5px;" url="MailFolder.aspx">
        <span class="icoright" title="展开">
            <img src="../img/common/Dark_Blue_Arrow.gif" style="vertical-align:middle;margin-top:3px;margin-right:3px;" alt="" />
        </span>
        <span class="otherfolder">其它文件夹</span>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <span title="新建文件夹" class="tnew">
        <a href="MailFolder.aspx?newfolder=1" target="mainframe2">[ 新建 ]</a>
        </span>
        </li>
        <%=sbFolder %>
    </ul>
    </td>
</tr>
</table>
<iframe id="mainframe2" name="mainframe2" width="100%" height="100%" frameborder="0" scrolling="auto" src="mailReceive.aspx?folderId=0"></iframe>
</body>
</html>
