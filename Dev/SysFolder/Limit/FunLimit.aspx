<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FunLimit.aspx.cs" Inherits="EIS.Studio.SysFolder.Limit.FunLimit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>权限设置</title>
    <meta http-equiv="Content-Type" content="text/html;charset=utf-8" />
    <link type="text/css" rel="stylesheet" href="../../Css/appInput.css" />
    <script type="text/javascript" src="../../js/jquery-1.7.min.js"></script>
    <script src="../../DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script src="../../Js/jquery.cookie.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../js/tools.js"></script>
    <style type="text/css">
        body{background:white url(../../img/common/body_bg.gif) repeat-x;padding-top:20px ;}
        .normaltbl>tbody>tr>td{text-align:center;}
    </style>
    <script type="text/javascript">
        jQuery(function () {

            $("#btnSel").click(function () {
                _openCenter('../../SysFolder/Common/UserTree.aspx?method=1&queryfield=empid,empname&cid=HiddenField1,TextBox1', '_blank', 640, 500);
              
            });
        });
        function btnSel_onclick() {

        }
        //弹出窗口
        function _openCenter(url, name, width, height) {
            var str = "height=" + height + ",innerHeight=" + height + ",width=" + width + ",innerWidth=" + width;
            if (window.screen) {
                var ah = screen.availHeight - 30;
                var aw = screen.availWidth - 10;
                var xc = (aw - width) / 2;
                var yc = (ah - height) / 2;
                str += ",left=" + xc + ",screenX=" + xc + ",top=" + yc + ",screenY=" + yc;
                str += ",resizable=yes,scrollbars=yes,directories=no,status=no,toolbar=no,menubar=no,location=no";
            }
            return window.open(url, name, str);
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="maindiv" style="width:600px;">
        <table class="normaltbl">
        <caption>权限设置</caption>
            <tr>
                <td>
                    <asp:CheckBoxList ID="CheckBoxList1" runat="server" 
                        RepeatDirection="Horizontal">
                        <asp:ListItem Selected="True" Value="显示"> 显示</asp:ListItem>
                        <asp:ListItem Selected="True"> 新建</asp:ListItem>
                        <asp:ListItem Selected="True"> 修改</asp:ListItem>
                        <asp:ListItem Selected="True"> 删除</asp:ListItem>
                        <asp:ListItem Selected="True"> 条件</asp:ListItem>
                        <asp:ListItem Selected="True"> 布局</asp:ListItem>
                        <asp:ListItem Selected="True"> 导出</asp:ListItem>
                    </asp:CheckBoxList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="TextBox1" CssClass="TextBoxInArea" Rows="20" runat="server" TextMode="MultiLine"></asp:TextBox>
                    <asp:HiddenField ID="HiddenField1" runat="server" />
                </td>
            </tr>
            <tr>
                <td>1、请在上面输入工号（可以从Excel复制，也可以逗号分隔）&nbsp;2、选择人员</td>
            </tr>
            <tr>
            <td height="40">
                <input type="button" id="btnSel" value="选择人员" class="btn01"/>
                <asp:Button ID="Button1" CssClass="btn01" runat="server" Text=" 提 交 " 
                    onclick="Button1_Click" />
                <input type="button" id="Button2" value=" 关 闭 " class="btn01" onclick="javascript: window.close();"/>
            </td>
            </tr>

        </table>
    </div>
    </form>
</body>
</html>
