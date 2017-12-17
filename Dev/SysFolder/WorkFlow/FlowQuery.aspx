<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FlowQuery.aspx.cs" Inherits="EIS.WebBase.SysFolder.WorkFlow.FlowQuery" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>流程查询</title>
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

    var maiheight = document.documentElement.clientHeight;
    var otherpm = 150; //GridHead，toolbar，footer,gridmargin
    var gh = maiheight - otherpm;

    $("#flex1").flexigrid
			(
			{
			    url: '../../getdata.ashx',
			    params: [{ name: "queryid", value: "flow_admin_ondeal" }
			        , { name: "condition", value: "<%=base.GetParaValue("condition") %>" }
                    , { name: "defaultvalue", value: "" }
			    ],
			    colModel: [
				{ display: '序号', name: 'rowindex', width: 30, sortable: false, align: 'center' },
				{ display: '任务名称', name: 'InstanceName', width: 260, sortable: true, align: 'left', renderer: transfld },
				{ display: '发起人', name: 'EmployeeName', width: 40, sortable: true, align: 'left' },
				{ display: '部门名称', name: 'DeptName', width: 60, sortable: false, align: 'center' },
				{ display: '发起时间', name: '_CreateTime', width: 100, sortable: true, align: 'left' },
				{ display: '当前步骤', name: 'ActiveNode', width: 100, sortable: true, align: 'left' },
				{ display: '当前处理人', name: 'ActiveEmployee', width: 80, sortable: true, align: 'left' },
				{ display: '状态', name: 'InstanceState', width: 40, sortable: true, align: 'left', renderer: getstate },
				{ display: '流程名称', name: 'workflowname', width: 160, sortable: true, align: 'left' }
			    ],
			    buttons: [
				{ name: '查询', bclass: 'view', onpress: app_query },
				{ name: '清空', bclass: 'clear', onpress: app_reset }
			    ],
			    searchitems: [
			    { display: '任务名称', name: 'InstanceName', type: 1 },
			    { display: '发起人', name: 'EmployeeName', type: 1 },
			    { display: '发起时间', name: '_CreateTime', type: 4 },
                { display: '当前步骤', name: 'ActiveNode', type: 1 },
                { display: '当前处理人', name: 'ActiveEmployee', type: 1 }
			    ],
			    sortname: "_CreateTime",
			    sortorder: "desc",
			    usepager: true,
			    singleSelect: true,
			    useRp: true,
			    rp: 10,
			    multisel: false,
			    showTableToggleBtn: false,
			    resizable: false,
			    height: gh,
			    onError: showError
			}
			);
			    function transfld(fldval, row) {
			        var arr = [];
			        var insId = $("InstanceId", row).text();
			        arr.push("<a class='normal' href=\"javascript:dealTask('" + insId + "');\">" + fldval + "</a>&nbsp;");
			        return arr.join("");
			    }

			    function getstate(fldval, row) {
			        if (fldval == "完成" || fldval == "处理中")
			            return "<span style='color:green'>" + fldval + "</span>";
			        else
			            return "<span style='color:red'>" + fldval + "</span>";
			    }

			    function showError(data) {
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

			    function app_stop(cmd, grid) {
			        var editid = $('.trSelected', grid)[0].id.substr(3);
			        openCenter("Admin_StopInstance.aspx?instanceId=" + editid, "_blank", 600, 400);
			    }

			    function app_query() {
			        $("#flex1").flexReload();
			    }
			    function dealTask(insId) {
			        var url = "../AppFrame/AppWorkFlowInfo.aspx?InstanceId=" + insId;
			        openCenter(url, "_blank", 1000, 700);
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
			            if (yc < 0) yc = 0;
			            str += ",left=" + xc + ",screenX=" + xc + ",top=" + yc + ",screenY=" + yc;
			            str += ",resizable=yes,scrollbars=yes,directories=no,status=no,toolbar=no,menubar=no,location=no";
			        }
			        return window.open(url, name, str);
			    }
			    //-->
</script>
