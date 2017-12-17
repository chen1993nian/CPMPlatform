<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MsgSend.aspx.cs" Inherits="EIS.Web.WorkAsp.Msg.MsgSend" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>发送消息</title>
    <link href="../../Css/appStyle.css" rel="stylesheet" type="text/css" />
    <script src="../../Js/jquery-1.7.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../js/jquery.validate.min.js"></script>
    <script type="text/javascript" src="../../js/jquery.notice.js"></script>
    <script src="../../Js/Tools.js" type="text/javascript"></script>
    <script type="text/javascript">
        var afterSendFlag = 0;
        $(document).ready(function () {
            if (afterSendFlag) {
                window.parent.location.href = 'MsgFrame.aspx';
            }
            jQuery("#txtRecNames").attr("readonly", "readonly").click(function () {
                openCenter('../../SysFolder/Common/UserTree.aspx?method=1&queryfield=empid,empname&cid=txtRecIds,txtRecNames'
                , '_blank', 640, 500);
            }).emptyValue({
                empty: "<请点击选择消息接收人>",
                className: "gray"
            });

            jQuery("#txtRecNames").focus(function () {
                this.blur();
            });
            jQuery("#btnReturn").click(function () {
                window.open("MsgFrame.aspx", "_parent");
            });
            jQuery("#Button1").click(function () {

                if ($("#txtRecNames").val() == "") {
                    $("#RequiredFieldValidator2").show();
                    return false;
                }
            });
        });
        function afterSend() {
            $.noticeAdd({ text: '消息发送成功！', stay: false });
            afterSendFlag = 1;

        }
        function sendError() {
            $.noticeAdd({ text: '消息发送失败！', stay: false });
        }
    </script>
    <style type="text/css">
        body{background:#f6f7f9;}
        table{margin-left:auto;margin-right:auto;}
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div id="maindiv">

    
<table class="TableTop" width="600" align="center">
<tbody>
    <tr>
        <td class="left"></td>
        <td class="center subject">发送消息 </td>
        <td class="right"></td>
    </tr>
</tbody>
</table>
<table border="1" style="width:600px;" class="TableBlock no-top-border" align="center">
    <tr>
        <td width="100">&nbsp;接收人</td>
        <td align="left">
            <asp:TextBox ID="txtRecNames" TextMode="MultiLine" Rows="3"   CssClass="TextBoxInArea" runat="server"></asp:TextBox>
            <asp:HiddenField ID="txtRecIds" runat="server" />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ForeColor="red" 
                ControlToValidate="txtRecNames" runat="server" ErrorMessage="请选择消息接收人" 
                Display="Dynamic"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td>&nbsp;内 容</td>
        <td align="left">
            <asp:TextBox ID="txtContent" TextMode="MultiLine" Rows="5"   CssClass="TextBoxInArea" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ForeColor="red" ControlToValidate="txtContent" runat="server" ErrorMessage="消息内容不能为空"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td>&nbsp;附 件</td>
        <td align="left">
            <iframe frameborder="0" width="480" height="100" src="../../SysFolder/Common/FileListFrame.aspx?appName=T_E_App_MsgInfo&appId=<%=appId %>&read=0"></iframe>
        </td>
        
    </tr>
    <tr class="TableFooter">
        <td colspan="2">
                    <asp:Button ID="Button1" runat="server" Text="发送消息" onclick="Button1_Click" />
                    <input id="btnReturn" type="button" value="返回列表" />
        </td>
    </tr>
</table>

        
        
    </div>
    </form>
</body>
</html>
