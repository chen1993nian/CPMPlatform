<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QueryLimitSet.aspx.cs" Inherits="EIS.Web.SysFolder.WorkFlow.QueryLimitSet" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>查阅权限设置</title>
    <link rel="stylesheet" type="text/css" href="../../css/password.css" />
    <script type="text/javascript" src="../../js/jquery-1.7.min.js"></script>
    <script type="text/javascript" src="../../js/jquery.notice.js"></script>
    <script type="text/javascript" src="../../js/tools.js"></script>
    <style type="text/css">
        .titleTd{font-size:12px;color:#FF8C00;text-align:left;font-weight:bold;}
        
    </style>

    <script type="text/javascript">
        function success() {
            $.noticeAdd({
                text: '保存成功！', stay: false, onClose: function () {
                    if ('<%=Request["open"] %>' != "")
                    window.close();
                else
                    frameElement.lhgDG.cancel();
            }
            });
        }
        jQuery(function () {

            $("#btnClose").click(function () {
                if ('<%=Request["open"] %>' != "")
                    window.close();
                else
                    frameElement.lhgDG.cancel();
            });
            $("#TextBox1").attr("emptytip", "<请双击选择部门>").attr("readOnly", true).dblclick(function () {
                openpage('../Common/DeptTree.aspx?method=1&queryfield=deptid,deptname&cid=HiddenField1,TextBox1');
            });
            $("#TextBox2").attr("emptytip", "<请双击选择岗位>").attr("readOnly", true).dblclick(function () {
                openpage('../Common/PositionTree.aspx?method=1&queryfield=positionid,positionname&cid=HiddenField2,TextBox2');
            });
            $("#TextBox3").attr("emptytip", "<请双击选择员工>").attr("readOnly", true).dblclick(function () {
                openCenter('../Common/UserTree.aspx?method=1&queryfield=empid,empname&cid=HiddenField3,TextBox3', "_blank", 640, 500);
            });

            jQuery("textarea.emptytip").emptyValue();

        });
</script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="maindiv">
        <table width="700">
            <tbody>
                <tr>
                    <td width="80" class="titleTd"> 查阅权限</td>
                    <td align="left" valign="middle" height="40">
                        <asp:RadioButtonList ID="RadioButtonList1" RepeatDirection="Horizontal" runat="server">
                            <asp:ListItem Text=" 流程任务处理人" Value="0" Selected="True"/>
                            <asp:ListItem Text=" 所有人可以查阅" Value="1"/>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td class="titleTd">
                        按部门设置
                    </td>
                    <td>
                        <asp:TextBox ID="TextBox1" CssClass="TextBoxInArea emptytip" TextMode="MultiLine" Rows="4" runat="server"></asp:TextBox>
                        <asp:HiddenField ID="HiddenField1" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td class="titleTd">
                         按岗位设置
                    </td>
                    <td>
                        <asp:TextBox ID="TextBox2" CssClass="TextBoxInArea emptytip" TextMode="MultiLine" Rows="4" runat="server"></asp:TextBox>
                        <asp:HiddenField ID="HiddenField2" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td class="titleTd">
                        按人员设置
                    </td>
                    <td>
                        <asp:TextBox ID="TextBox3" CssClass="TextBoxInArea emptytip" TextMode="MultiLine" Rows="4" runat="server"></asp:TextBox>
                        <asp:HiddenField ID="HiddenField3" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td height="60">
                        <asp:Button ID="Button1" CssClass="btn01" runat="server" Text=" 保 存 " onclick="Button1_Click" />
                        &nbsp;
                        &nbsp;
                        <input id="btnClose" type="button"  class="btn01" value=" 关 闭 "/>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
    </form>
</body>
</html>
