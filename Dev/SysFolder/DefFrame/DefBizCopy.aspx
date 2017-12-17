<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DefBizCopy.aspx.cs" Inherits="EIS.Studio.SysFolder.DefFrame.DefBizCopy"  ValidateRequest="false"%>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>业务复制</title>
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
    <table class='normaltbl center' style="width: 660px;" border="1" align="center">
    <caption>业务模型复制</caption>
    <thead>
        <tr>
            <th width="20">#</th>
            <th width="140">参考表名称</th>
            <th width="160">参考表中文名</th>
            <th width="40">类型</th>
            <th width="140">新建表名称</th>
            <th>新建表中文名</th>
        </tr>
    </thead>
    <tbody>
        <%=tblList %>
    </tbody>
    <tfoot>
        <tr>
            <td colspan="6"  style="height:40px;padding-right:15px;" align="right">
                <asp:Button ID="Button1" runat="server" CssClass="btn01" Text="  提  交  " onclick="Button1_Click" />
                <input type="button" id="btnClose" class="btn01" value="  关 闭  " />
            </td>
        </tr>
    </tfoot>
    </table>
    <div id="infoZone" style="width:660px;" class="center">
        <div class="tip" >
            操作提示：请在最后两列分别输入表名和中文名，主表名称不能为空（如果子表名为空，系统不会进行复制）。
        </div>
    </div>
    </form>
</body>
</html>
<script type="text/javascript">
    $(function () {
        $("#btnClose").click(function () {
            //frameElement.lhgDG.curWin.app_query();
            frameElement.lhgDG.cancel();
        });
        var _curClass = EIS.Studio.SysFolder.DefFrame.DefBizCopy;
        $("#Button1").click(function () {

            if ($("#mainName").val() == "") {
                alert("主表名不能为空");
                $("#mainName").focus();
                return false;
            }
            else if ($("#mainCn").val() == "") {
                alert("主表中文名不能为空");
                $("#mainCn").focus();
                return false;
            }
            else {
                var arr = [];
                var tarName = $("#mainName").val();
                arr.push("<%=tblName %>," + tarName + "," + $("#mainCn").val());
                $(".subName").each(function (i, n) {
                    if (this.value == "")
                        return false;
                    arr.push($(this).attr("srcTbl") + "," + this.value + "," + $("#" + this.id + "_cn").val());
                });
                var ret = _curClass.CopyTables(arr.join("|"));
                if (ret.error) {
                    $("#infoZone").prepend("<div class='tip info' >复制业务时出错：" + ret.error.Message + "</div>");

                    setTimeout(function () { $(".info").remove(); }, 10000);

                }
                else {
                    frameElement.lhgDG.curWin.app_query();
                    frameElement.lhgDG.cancel();
                    if (t == "1")
                        window.open("DefTableFrame.aspx?tblName=" + tarName, "_blank");
                    else
                        window.open("DefQueryFrame.aspx?tblName=" + tarName, "_blank");
                }
                return false;
            }
        });
    });
</script>
