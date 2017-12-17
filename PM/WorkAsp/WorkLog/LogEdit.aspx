<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LogEdit.aspx.cs" Inherits="EIS.Web.WorkAsp.WorkLog.LogEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>工作日志</title>
    <link type="text/css" rel="stylesheet" href="../../Css/appInput.css" />
    <link type="text/css" rel="stylesheet" href="../../Editor/skins/default.css" />
    <script type="text/javascript" src="../../js/jquery-1.7.min.js"></script>
    <script type="text/javascript" src="../../DatePicker/WdatePicker.js"></script>
    <script type="text/javascript" src="../../Editor/kindeditor.js"></script>
    <script type="text/javascript" src="../../js/jquery.notice.js"></script>
    <script type="text/javascript">
        function success() {
            $.noticeAdd({
                text: '保存成功！', stayTime: 500, onClose: function () {
                    window.open("LogRight.aspx?t=" + Math.random(), "_self");
                }
            });
        }
        jQuery(function () {
            $(".WebEditor").each(function () {
                KE.show({ id: this.id });
            });
            $(".Wdate").focus(function () {
                WdatePicker({ isShowClear: false, dateFmt: 'yyyy-MM-dd' });
            }).click(function () {
                WdatePicker({ isShowClear: false, dateFmt: 'yyyy-MM-dd' });
            });

            $("#LinkButton1,#LinkButton2").click(function () {
                KE.util.setData("txtEditor");
            });
        });

    </script>
</head>
<body>
    <form id="form1" runat="server">
<!-- 工具栏 -->
    <div class="menubar">
        <div class="topnav">
            <span style="right:10px;display:inline;float:right;position:fixed;line-height:30px;top:0px;">
            	<a class='linkbtn' href="LogRight.aspx" target="_self">返回列表</a>
				<em class="split">|</em>
                <asp:LinkButton ID="LinkButton2" CssClass="linkbtn" runat="server" 
                onclick="LinkButton2_Click">保存后新增</asp:LinkButton>
				<em class="split">|</em>
                <asp:LinkButton ID="LinkButton1" CssClass="linkbtn" runat="server" 
                onclick="LinkButton1_Click">保存</asp:LinkButton>
            </span>
        </div>
    </div>
    <div id="maindiv">

        <table class="normaltbl" border="1"><caption>工作日志</caption>
            <tbody>
            <tr>
            <td width="20%">&nbsp;员工姓名</td>
            <td width="30%">
                <asp:TextBox ID="txtEmpName" CssClass="TextBoxInChar" ReadOnly="true" runat="server"></asp:TextBox>
            </td>
            <td width="20%">&nbsp;日志日期</td>
            <td>
                <asp:TextBox ID="txtDate" CssClass="Wdate TextBoxInDate" runat="server"></asp:TextBox>
            </td>
            </tr>
            <tr>
            <td colspan="4">
                <asp:TextBox ID="txtEditor" CssClass="WebEditor" TextMode="MultiLine" style='display:none;width:100%;height:360px;' runat="server"></asp:TextBox>
            </td>
            </tr>
            </tbody>
        </table>
    </div>
    </form>
</body>
</html>
