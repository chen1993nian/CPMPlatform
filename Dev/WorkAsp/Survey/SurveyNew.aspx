<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SurveyNew.aspx.cs" Inherits="EIS.Web.WorkAsp.Survey.SurveyNew" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>问卷基本信息设置</title>
    <link rel="stylesheet" type="text/css" href="../../Css/Password.css" />
    <script type="text/javascript" src="../../js/jquery-1.7.min.js"></script>
    <script type="text/javascript" src="../../js/jquery.notice.js"></script>
    <script type="text/javascript" src="../../DatePicker/WdatePicker.js"></script>
    <script type="text/javascript" src="../../js/tools.js"></script>
    <style type="text/css">
        body{overflow:hidden;padding:20px;margin:0px;}
     	.maintbl
	    {
	        border:#c3daf9 1px solid;
	        background:white;
	        border-collapse:collapse;
	        width:700px;
	        table-layout:fixed;
	    }
	    .maintbl caption{
	        background:#f9fafe url(../../img/common/layout.png) no-repeat 10px center;
	        border:#c3daf9 1px solid;
	        padding-left:30px;
	        color:#38a0cd;
	        text-align:left;
	        font:bold 14px/36px "Microsoft YaHei",宋体;
	        }
	    .maintbl>tbody>tr>td{
	        border:1px solid #c3daf9;
	        }
	    .maintbl>tfoot{
	        border-top:#c3daf9 2px solid;
	        height:40px;	        
	        }
	    .titleTd{text-align:right;width:80px;font:normal 13px/30px "Microsoft YaHei",宋体;color:#444;}
        input[type=radio]{vertical-align:middle;cursor:pointer;}
        input[type=text]{padding:2px;line-height:22px;height:20px;font-size:12px;color:#444;border:1px solid #bbb;}
        textarea{padding:2px;line-height:22px;font-size:12px;color:#444;border:1px solid #bbb;}
        label{cursor:pointer;}
    </style>
    <script type="text/javascript">
        function success() {
            $.noticeAdd({
                text: '保存成功！', stay: false, onClose: function () {
                    frameElement.lhgDG.cancel();
                }
            });
        }
        jQuery(function () {

            $("#btnClose").click(function () {
                frameElement.lhgDG.cancel();
            });
            $("#TextBox5").attr("emptytip", "<请双击选择部门>").attr("readOnly", true).dblclick(function () {
                openpage('../../SysFolder/Common/DeptTree.aspx?method=1&queryfield=deptid,deptname&cid=HiddenField1,TextBox5');
            });

            $("#Button1").click(function () {
                if ($("#TextBox5").val() == "") {
                    $("#TextBox5").val("");
                }
                if ($("#txtStart").val() == "") {
                    alert("生效日期不能为空");
                    return false;
                }
                if ($("#txtEnd").val() == "") {
                    alert("截止日期不能为空");
                    return false;
                }
                return true;
            });
            $(".Wdate").focus(function () {
                WdatePicker({ isShowClear: true, dateFmt: 'yyyy-MM-dd HH:mm' });
            });
            jQuery("textarea.emptytip").emptyValue();

            $("#RadioButtonList1_0").click(function () {
                $("#TextBox5").val("").attr("disabled", true);;
                $("#HiddenField1").val("");
            });
            $("#RadioButtonList1_1").click(function () {
                $("#TextBox5").attr("disabled", false);;
            });
        });
</script>
</head>
<body>
    <form id="form1" runat="server">
    <div style="padding:30px;">
    <table class="maintbl">
    <caption>问卷基本信息设置</caption>
    <tbody>
    <tr>
        <td height="25" class="titleTd"><span class="RequiredStar">*&nbsp;</span>标 题：</td>
        <td style="width:240px;">
            <asp:TextBox ID="TextBox1" runat="server" Width="230" CssClass=""></asp:TextBox></td>
        <td height="25" class="titleTd">编 号：</td>
        <td >
            <asp:TextBox ID="TextBox2" ToolTip="只读，自动生成" ReadOnly="true" runat="server" Width="200" CssClass=""></asp:TextBox></td>
    </tr>

        <tr>
        <td height="25" class="titleTd">调查范围：</td>
        <td>
            <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal">
                <asp:ListItem Text="公开（所有人）" Selected="True" Value="1"/>
                <asp:ListItem Text="指定部门"  Value="2"/>
            </asp:RadioButtonList>
            </td>
        <td height="25" class="titleTd">排 序：</td>
        <td>
            <asp:TextBox ID="TextBox4" ToolTip="按序号倒序排列，序号越大越靠前" runat="server" Width="200" CssClass=""></asp:TextBox>
            </td>
    </tr>
     <tr>
        <td height="25" class="titleTd">调查对象：</td>
        <td colspan="3">
            <asp:TextBox ID="TextBox5" runat="server" TextMode="MultiLine" Width="99%" Rows="5" CssClass="emptytip"></asp:TextBox>
            <asp:HiddenField ID="HiddenField1" runat="server" />
            </td>
    </tr>
            <tr>
        <td height="25" class="titleTd">描 述：</td>
        <td colspan="3">
            <asp:TextBox ID="TextBox6" runat="server" TextMode="MultiLine" Width="99%" Rows="5" CssClass=""></asp:TextBox></td>
    </tr>
            <tr>
        <td height="25" class="titleTd"><span class="RequiredStar">*&nbsp;</span>是否生效：</td>
        <td colspan="3">
            <asp:RadioButtonList ID="RadioButtonList2" runat="server" RepeatDirection="Horizontal">
                <asp:ListItem Text="有效" Selected="True" Value="是"/>
                <asp:ListItem Text="无效"  Value="否"/>
            </asp:RadioButtonList>
            </td>
    </tr>

        <tr>
        <td height="25" class="titleTd"><span class="RequiredStar">*&nbsp;</span>生效日期：</td>
        <td >
            <asp:TextBox ID="txtStart" runat="server" Width="200" CssClass="Wdate"></asp:TextBox></td>
        <td height="25" class="titleTd"><span class="RequiredStar">*&nbsp;</span>截止日期：</td>
        <td>
            <asp:TextBox ID="txtEnd" runat="server" Width="200" CssClass="Wdate"></asp:TextBox></td>
    </tr>
        <tr>
        <td colspan="4" align="center">
        <br />
            <asp:Button ID="Button1" runat="server" CssClass="btn01" Text="保 存" Width="80px" OnClick="Button1_Click" />
            <asp:Button ID="Button2" runat="server" CssClass="btn01" Text="创建问卷题目" Width="100px" OnClick="Button2_Click" />
        </td>
    </tr>
    </tbody>
    </table>
    </div>
    </form>
</body>
</html>
