<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MailAfterSend.aspx.cs" Inherits="EIS.Web.Mail.MailAfterSend" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>邮件发送成功</title>
    <style type="text/css">
        input{padding:3px;}
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <br /><br /><br /><br />
        <table width="300" border="0" align="center">
        <tr>
        <td width="60"><img alt="成功" src="../img/check.png" /></td>
        <td height="30"><h3 style="float:left;color:#4677bf"><%=msgInfo%></h3></td></tr>
        <tr>
            <td colspan="2" align="center">
                <br />
                <input type="button" value="返回列表" onclick="window.location = 'MailReceive.aspx?folderid=0';" />
                <input type="button" value="再写一封" onclick="window.location = 'MailWrite.aspx';" />

            </td>
        </tr>
        </table>

    </div>
    </form>
</body>
</html>
