<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FlowMaintain_old.aspx.cs" Inherits="EIS.WebBase.SysFolder.WorkFlow.FlowMaintain_old" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>流程任务维护</title>
    <meta http-equiv="Pragma" content="no-cache"/>
    <link rel="stylesheet" type="text/css" href="../../grid/css/flexigrid.css"/>
    <link rel="stylesheet" type="text/css" href="../../Css/DefStyle.css"/>
    <link rel="stylesheet" type="text/css" href="../../Css/datePicker.css"/>
    
    <script type="text/javascript" src="../../grid/lib/jquery/jquery.js"></script>
    <script type="text/javascript" src="../../DatePicker/WdatePicker.js"></script> 
    <script type="text/javascript" src="../../grid/flexigrid.js"></script>
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
    var _curClass = EIS.WebBase.SysFolder.WorkFlow.FlowMyPart;
    var maiheight = document.documentElement.clientHeight;
    var otherpm = 150; //GridHead，toolbar，footer,gridmargin
    var gh = maiheight - otherpm;

    $("#flex1").flexigrid
    (
    {
        url: '../../getdata.ashx',
        params: [{ name: "queryid", value: "flowmypart" }
            , { name: "condition", value: "1=1" }
            , { name: "defaultvalue", value: "\"@employeeId\":\"<%=EmployeeID %>\"" }
        ],
        colModel: [
        { display: '序号', name: 'rowindex', width: 30, sortable: false, align: 'center' },
        { display: '任务名称', name: 'InstanceName', width: 200, sortable: true, align: 'left', renderer: transname },
        { display: '发起人', name: 'EmployeeName', width: 60, sortable: true, align: 'left' },
        { display: '部门名称', name: 'DeptName', width: 100, sortable: true, align: 'center' },
        { display: '发起时间', name: '_CreateTime', width: 100, sortable: true, align: 'left' },
        { display: '当前步骤', name: 'ActiveNode', width: 100, sortable: true, align: 'left' },
        { display: '当前处理人', name: 'ActiveEmployee', width: 100, sortable: true, align: 'left' },
        { display: '完成时间', name: 'FinishTime', width: 100, sortable: true, align: 'left' },
        { display: '任务状态', name: 'InstanceState', width: 60, sortable: true, align: 'left', renderer: getstate },
        { display: '处理', name: 'InstanceId', width: 140, sortable: false, align: 'center', renderer: transfld }
        ],
        buttons: [
        { name: '修改标题', bclass: 'edit', onpress: app_edit },
        { name: '终止任务', bclass: 'delete', onpress: app_stop },
        { separator: true },
        { name: '查询', bclass: 'view', onpress: app_query },
        { name: '清空', bclass: 'clear', onpress: app_reset }
        ],
        searchitems: [
        { display: '任务名称', name: 'InstanceName', type: 1 },
        { display: '流程名称', name: 'WorkflowName', type: 1 },
        { display: '发起公司', name: 'CompanyName', type: 1 },
        { display: '发起人', name: 'EmployeeName', type: 1 },
        { display: '发起时间', name: '_CreateTime', type: 4 },
        { display: '任务状态', name: 'InstanceState', type: 1, edit: 'select', data: '处理中|处理中,完成|完成,终止|终止,归档|归档,', defvalue: '' }
        ],
        sortname: "DealTime",
        sortorder: "desc",
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
        function transname(fldval, row) {
            var instanceId = $(row).attr("id");
            var htmlArr = [];
            htmlArr.push("<a href=\"javascript:dealTask('" + instanceId + "');\">" + fldval + "</a>&nbsp;");
            return htmlArr.join("");
        }
        function transfld(fldval, row, td) {
            var htmlArr = [];
            htmlArr.push("<a href=\"javascript:dealTask('" + fldval + "');\">维护</a>&nbsp;");
            htmlArr.push("<a class='normal' href=\"javascript:frechTask('" + fldval + "');\">取回</a>&nbsp;");
            htmlArr.push("<a class='normal' href=\"javascript:viewChart('" + fldval + "');\">流程图</a>&nbsp;");
            htmlArr.push("<a class='normal' target='_blank' href=\"../AppFrame/AppWorkflowPrint.aspx?InstanceId=" + fldval + "\">打印</a>");
            return htmlArr.join("");

        }


        function getstate(fldval, row) {
            if (fldval == "完成" || fldval == "处理中")
                return "<span style='color:green'>" + fldval + "</span>";
            else
                return "<span style='color:red'>" + fldval + "</span>";
        }
        function app_stop(cmd, grid) {

            var editid = $('.trSelected', grid)[0].id.substr(3);
            openCenter("Admin_StopInstance.aspx?instanceId=" + editid, "_blank", 600, 400);
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
                openCenter("Admin_UpdateTitle.aspx?instanceId=" + editid, "_blank", 600, 400);
            }
            else {
                alert("请选中一条记录");
            }
        }
        function app_delete(cmd, grid) {
            if ($('.trSelected', grid).length > 0) {
                if (confirm('确定删除这' + $('.trSelected', grid).length + '条记录吗?')) {
                    $('.trSelected', grid).each
                    (
                        function () {
                            var ret = EIS.WebBase.SysFolder.DefFrame.DefBizList.DelRecord(this.id.substr(3));
                            if (ret.error) {
                                alert(ret.error.Message);
                            }
                        }
                    );
                    $("#flex1").flexReload();
                }
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
        function dealTask(insId) {
            var url = "admin/Admin_Maintain.aspx?InstanceId=" + insId;
            openCenter(url, "_blank", 1000, 700);
        }
        function frechTask(insId) {
            var ret = _curClass.FetchTask(insId);
            if (ret.error) {
                alert(ret.error.Message);
            }
            else {
                alert("取回任务成功");
                $("#flex1").flexReload();
            }
        }
        function viewChart(instanceId) {
            var url = "InstanceChart.aspx?instanceId=" + instanceId;
            openCenter(url, "_blank", 1000, 700);
        }
        function openCenter(url, name, width, height) {
            var str = "height=" + height + ",innerHeight=" + height + ",width=" + width + ",innerWidth=" + width;
            if (window.screen) {
                var ah = screen.availHeight - 30;
                var aw = screen.availWidth - 10;
                var xc = (aw - width) / 2;
                var yc = (ah - height) / 2;
                if (yc < 0)
                    yc = 0;
                str += ",left=" + xc + ",screenX=" + xc + ",top=" + yc + ",screenY=" + yc;
                str += ",resizable=yes,scrollbars=yes,directories=no,status=no,toolbar=no,menubar=no,location=no";
            }
            return window.open(url, name, str);
        }
        //-->
</script>