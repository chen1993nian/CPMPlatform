<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DocFolderSend.aspx.cs" Inherits="Studio.JZY.Doc.DocFolderSend" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>文件夹转发</title>
    <link rel="stylesheet" type="text/css" href="../Css/DefStyle.css" />
    <script type="text/javascript" src="../js/jquery-1.4.2.min.js" ></script>
    <script type="text/javascript" src="../js/jquery.notice.js"></script>
    <script type="text/javascript" src="../js/Tools.js"></script>
    <style type="text/css">
        input[type=submit],input[type=button]{padding:0px 10px;height:28px;}
        .btnsel{border-width:0px;background-color:#ccc;}
        #txtPath{border-width:1px;color:blue;font-weight:bold;background-color:#eee;height:20px;line-height:20px;padding:3px;}
        .maindiv{text-align:left;}
    </style>
    <script type="text/javascript">
        jQuery(function () {
            $("#txtShare").attr("readOnly", true);

            jQuery("#btnSel").click(function () {
                openCenter('../SysFolder/Common/UserTree.aspx?method=1&queryfield=empid,empname&cid=hidShareId,txtShare', '_blank', 640, 500);
            });
            jQuery("#btnCls").click(function () {
                jQuery("#txtShare,#hidShareId").val("");
            });
        });
        function afterSave() {
            $.noticeAdd({ text: '转发成功！', stay: false });
            if (window.opener) {
                window.opener.app_query();
            }
            window.close();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="maindiv">
    <table>
    <tr>
        <td height="25">选择接收人：</td>
        <td>
            <asp:TextBox ID="txtShare" runat="server" Width="320" CssClass="TextArea" TextMode="MultiLine" Rows="6"></asp:TextBox>
            <input id="hidShareId" type="hidden" runat="server" value="" />
            <div style="clear:both;padding:2px;">
            <input id="btnSel" class="btnsel" type="button" value="选择" />
            <input id="btnCls" class="btnsel" type="button" value="清空" />
            </div>
        </td>
    </tr>
        <tr>
        <td colspan="2" align="center">
        <br />
            <asp:Button ID="Button1" runat="server" Text="转发并消息通知"  OnClick="Button1_Click" />
            &nbsp;
            <input type="button" value="关闭" onclick="window.close();"/>
            </td>
    </tr>
    </table>
    
    </div>
    </form>
</body>
</html>
