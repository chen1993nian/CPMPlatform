<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DefTableRelation.aspx.cs" Inherits="EIS.Studio.SysFolder.DefFrame.DefTableRelation" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>创建表关系</title>
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
</head>
<body>
    <form id="form1" runat="server">
    <br />
    <table class='normaltbl center' style="width: 460px;" border="1" align="center">
    <caption>创建表关系</caption>
    <tbody>
        <tr>
            <td width="100">主表</td>
            <td>
                <input type="text" class="TextBoxInChar gray" readonly="readonly" name="mainName" id="mainName" value="<%=tblName %>" />
            </td>
        </tr>
        <tr>
            <td>子表</td>
            <td>
                <input type="text" class="TextBoxInChar" name="subName" id="subName"  value="" />                
            </td>
        </tr>
    </tbody>
    <tfoot>
        <tr>
            <td colspan="2"  style="height:40px;padding-right:15px;" align="right">
                <asp:Button ID="Button1" runat="server" CssClass="btn01" Text="  提  交  " onclick="Button1_Click" />
                <input type="button" id="btnClose" class="btn01" value="  关 闭  " />
            </td>
        </tr>
    </tfoot>
    </table>
    <div id="infoZone" style="width:660px;" class="center">
    </div>
    </form>
</body>
</html>
<script type="text/javascript">
    $(function () {
        $("#btnClose").click(function () {
            frameElement.lhgDG.cancel();
        });
        var _curClass = EIS.Studio.SysFolder.DefFrame.DefTableRelation;
        $("#Button1").click(function () {

            if ($("#subName").val() == "") {
                alert("子表名不能为空");
                $("#subName").focus();
                return false;
            }
            var ret = _curClass.AddRelation("<%=tblName %>", $("#subName").val());
            if (ret.error) {
                $("#infoZone").prepend("<div class='tip info' >创建关系时出错：" + ret.error.Message + "</div>");
                setTimeout(function () { $(".info").remove(); }, 10000);

            }
            else {
                frameElement.lhgDG.curWin.loadFields($("#subName").val());
                frameElement.lhgDG.cancel();
            }
            return false;
        });
    });
</script>
