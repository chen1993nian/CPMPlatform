<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DefDefaultValue.aspx.cs" Inherits="EIS.Studio.SysFolder.DefFrame.DefDefaultValue" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>默认值设置</title>
    <meta http-equiv="Content-Type" content="text/html;charset=utf-8" />
    <link type="text/css" rel="stylesheet" href="../../Css/appInput.css" />
    <script type="text/javascript" src="../../js/jquery-1.7.min.js"></script>
    <script src="../../DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script src="../../Js/jquery.cookie.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../js/tools.js"></script>
    <style type="text/css">
        body{background:white url(../../img/common/body_bg.gif) repeat-x;padding-top:20px ;}
        .normaltbl>tbody>tr>td{text-align:left;}
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <table class='normaltbl center' style="width: 460px;" border="1" align="center">
    <caption>默认值设置</caption>
    <tbody>
        <tr><td width="80">&nbsp;默认值类型</td>
        <td>
            <select id="selDefType" size="1" >
		        <option value="Custom"  title="自定义值">自定义值</option>
		        <option value="Date"  title="当前日期">当前日期</option>
		        <option value="DateTime"  title="当前日期时间">当前日期时间</option>
		        <option value="Session"  title="Session值类型">Session值类型</option>
		        <option value="LoginName"  title="登录用户账号">登录用户帐号</option>
		        <option value="EmployeeName"  title="登录用户中文名">登录用户中文名</option>
		        <option value="EmployeeID"  title="登录用户ID">登录用户ID</option>
		        <option value="DeptID"  title="登录用户所属部门ID">所属部门ID</option>
		        <option value="DeptCode"  title="登录用户所属部门编码">所属部门编码</option>
		        <option value="DeptName"  title="登录用户所属部门名称">所属部门名称</option>
		        <option value="DeptFullName"  title="登录用户所属部门全称">所属部门全称</option>
		        <option value="TopDeptID"  title="登录用户一级部门ID">一级部门ID</option>
		        <option value="TopDeptCode"  title="登录用户一级部门编码">一级部门编码</option>
		        <option value="TopDeptName"  title="登录用户一级部门名称">一级部门名称</option>
		        <option value="TopDeptFullName"  title="登录用户一级部门全称">一级部门全称</option>
		        <option value="CompanyID"  title="登录用户所属公司ID">所属公司ID</option>
		        <option value="CompanyCode"  title="登录用户所属公司编码">所属公司编码</option>
		        <option value="CompanyName"  title="登录用户所属公司名称">所属公司名称</option>
		        <option value="PositionID"  title="登录用户所属岗位ID">所属岗位ID</option>
		        <option value="PositionName"  title="登录用户所属岗位名称">所属岗位名称</option>
		        <option value="LoginIP"  title="登录用户IP">登录用户IP</option>
		        <option value="DbFunction"  title="数据库函数">数据库函数</option>
		        <option value="DbSQL"  title="自定义SQL">自定义SQL</option>
		        <option value="CurrentYear"  title="当前年">当前年</option>
		        <option value="CurrentMonth"  title="当前月">当前月</option>
		        <option value="CurrentDay"  title="当前日">当前日</option>
		    </select>
        </td>
        </tr>
        <tr><td>&nbsp;默认值</td>
        <td>
            <textarea rows="5" id="txtDefValue" class="TextBoxInArea"></textarea>
        </td></tr>
    </tbody>
    <tfoot>
        <tr>
            <td colspan="2"  style="height:40px;padding-left:85px;" align="left">
                <input type="button" id="Button1" class="btn01" value="  确 认  " />&nbsp;
                <input type="button" id="btnClose" class="btn01" value="  关 闭  " />
            </td>
        </tr>
    </tfoot>
    </table>
    </form>
</body>
</html>
<script type="text/javascript">
    $(function () {
        var ind = '<%=Request["oindex"] %>';
        var t = frameElement.lhgDG.curWin.jQuery("#txtdeftype" + ind).val();
        var v = frameElement.lhgDG.curWin.jQuery("#txtdefvalue" + ind).val();
        $("#selDefType").val(t);
        $("#txtDefValue").val(v);

        $("#Button1").click(function () {
            var a = $("#selDefType").val();
            var b = $("#selDefType").find(":selected").text();
            var c = $("#txtDefValue").val();
            frameElement.lhgDG.curWin["defaultCallBack"](a, b, c);
            frameElement.lhgDG.cancel();
        });
        $("#btnClose").click(function () {
            frameElement.lhgDG.cancel();
        });
    });
</script>