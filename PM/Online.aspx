<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Online.aspx.cs" Inherits="EIS.Web.Online" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>在线人数</title>
    <meta http-equiv="Pragma" content="no-cache"/>
    <link rel="stylesheet" type="text/css" href="grid/css/flexigrid.css"/>
    <link rel="stylesheet" type="text/css" href="Css/DefStyle.css"/>
    <link rel="stylesheet" type="text/css" href="Css/datePicker.css"/>
    
    <script type="text/javascript" src="grid/lib/jquery/jquery.js"></script>
    <script type="text/javascript" src="js/jquery.datePicker-min.js"></script> 
    <script type="text/javascript" src="grid/flexigrid.js"></script>
</head>
<body style="margin:2px">
    <form id="form1" runat="server">
        <div id="griddiv" name="griddiv">
        <table id="flex1" style="display:none"></table>    
    </div>
    </form>
</body>
</html>
<script type="text/javascript">
<!--

    var maiheight = document.documentElement.clientHeight;
    var otherpm = 150; //GridHead，toolbar，footer,gridmargin
    var gh = maiheight - otherpm;

    $("#flex1").flexigrid
    (
    {
        url: 'getdata.ashx',
        params: [{ name: "queryid", value: "online" }
            , { name: "condition", value: "" }
            , { name: "defaultvalue", value: "\"@interval\":<%=refreshInterval %>" }
        ],
        colModel: [
        { display: '序号', name: 'rowindex', width: 30, sortable: false, align: 'center' },
        { display: '员工编号', name: 'employeecode', width: 80, sortable: true, align: 'left' },
        { display: '员工姓名', name: 'employeename', width: 60, sortable: false, align: 'center' },
        { display: '单位名称', name: 'companyname', width: 180, sortable: true, align: 'left' },
        { display: '部门名称', name: 'deptname', width: 80, sortable: true, align: 'left' },
        { display: '登录时间', name: 'lastlogintime', width: 100, sortable: true, align: 'right' },
        { display: '操作', name: 'employeeid', width: 80, sortable: false, align: 'center', renderer: transfld }
        ],
        buttons: [
        { name: '查询', bclass: 'view', onpress: app_query },
        { name: '清空', bclass: 'clear', onpress: app_reset }
        ],
        searchitems: [
        { display: '单位名称', name: 'companyname', type: 1 },
        { display: '部门', name: 'deptname', type: 1 },
        { display: '姓名', name: 'employeename', type: 1 }
        ],
        sortname: "",
        sortorder: "",
        usepager: true,
        singleSelect: true,
        useRp: true,
        rp: 12,
        multisel: false,
        showTableToggleBtn: false,
        resizable: false,
        height: gh,
        onError: showError
    }
			);
        function transfld(fldval, row) {
            return "";


        }
        function transtype(fldval, row) {
            return fldval == "1" ? "主表" : "子表";
        }
        function sonTable(fldval, row) {
            var cat = $("tablecat", row).text();
            if ($("tabletype", row).text() == "1")
                return "<a class='normal' href='DefBizList.aspx?nodewbs=" + cat + "&parent=" + fldval + "&t=2" + "'>子表</a>";
            else
                return "";
        }
        function showError(data) {
        }
        function app_add(cmd, grid) {
            openCenter("DefBizEdit.aspx?nodewbs=", "_blank", 600, 400);
        }
        function app_reset(cmd, grid) {
            $("#flex1").clearQueryForm();
        }
        function app_edit(cmd, grid) {
            if ($('.trSelected', grid).length > 0) {
                var editid = $('.trSelected', grid)[0].id.substr(3);
                openCenter("DefBizEdit.aspx?editid=" + editid + "&nodewbs=", "_blank", 600, 400);
            }
            else {
                alert("请选中一条记录");
            }
        }
        function app_query() {
            $("#flex1").flexReload();
        }
        function app_setquery() {
            window.showModalDialog("AppConditionDef.aspx", "", "dialogHeight=600px;dialogWidth=800px;status=no;center=yes;resizable=yes;");
        }
        function dealTask(taskId) {
            var url = "DealFlow.aspx?taskId=" + taskId;
            openCenter(url, "_blank", 1000, 700);
        }
        function openCenter(url, name, width, height) {
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
        //-->
</script>