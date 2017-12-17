<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FlowToDo.aspx.cs" Inherits="EIS.Web.SysFolder.WorkFlow.FlowToDo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>待办任务</title>
    <meta http-equiv="Pragma" content="no-cache"/>
    <link rel="stylesheet" type="text/css" href="../../grid/css/flexigrid.css"/>
    <link rel="stylesheet" type="text/css" href="../../Css/DefStyle.css"/>
    <link rel="stylesheet" type="text/css" href="../../Css/datePicker.css"/>
    <link rel="stylesheet" type="text/css" href="../../css/ymPrompt_green/ymPrompt.css"/>
    
    <script type="text/javascript" src="../../grid/lib/jquery/jquery.js"></script>
    <script type="text/javascript" src="../../DatePicker/WdatePicker.js"></script> 
    <script type="text/javascript" src="../../grid/flexigrid.js"></script>
    <script type="text/javascript" src="../../js/ymPrompt.js"></script>
</head>
<body style="margin:2px">
    <form id="form1" runat="server">
        <div id="griddiv" name="griddiv">
        <table id="flex1" style="display:none"></table>    
        </div>
        <input type="hidden" value="" id="delegatePosId" />
        <input type="hidden" value="" id="delegateUserId" />
    </form>
</body>
</html>
<script type="text/javascript">
<!--
    var _curClass = EIS.Web.SysFolder.WorkFlow.FlowToDo;
    var maiheight = document.documentElement.clientHeight;
    var otherpm = 150; //GridHead，toolbar，footer,gridmargin
    var gh = maiheight - otherpm;

    $("#flex1").flexigrid
    (
    {
        url: '../../getdata.ashx',
        params: [{ name: "queryid", value: "flowtodo" }
                , { name: "condition", value: "" }
                , { name: "defaultvalue", value: "\"@employeeId\":\"<%=EmpId %>\"" }
			],
			    colModel: [
                    { display: '选择', name: 'utaskid', width: 30, sortable: false, align: 'center', renderer: chkCol },
                    { display: '任务名称', name: 'instancename', width: 200, sortable: true, align: 'left' },
                    { display: '流程名称', name: 'workflowname', width: 200, sortable: true, align: 'left' },
                    { display: '步骤名称', name: 'taskname', width: 100, sortable: true, align: 'center' },
                    { display: '发起人', name: 'createuser', width: 60, sortable: true, align: 'left' },
                    { display: '发起时间', name: 'createtime', width: 100, sortable: true, align: 'right' },
                    { display: '到达时间', name: 'arrivetime', width: 100, sortable: true, align: 'right' },
                    { display: '任务代理人', name: 'agentName', width: 80, sortable: true, align: 'right' },
                    { display: '处理', name: 'utaskid', width: 80, sortable: false, align: 'center', renderer: transfld }
			    ],
			    buttons: [

                    { name: '查询', bclass: 'view', onpress: app_query },
                    { name: '清空', bclass: 'clear', onpress: app_reset },
                    { separator: true },
                    { name: '修改标题', bclass: 'edit', onpress: app_edit },
                    { name: '终止任务', bclass: 'delete', onpress: app_stop },
                    { separator: true },
                    { name: '转交', bclass: 'arrow', onpress: app_transfer },
                    { name: '委托', bclass: 'arrow', onpress: app_delegate }
			    ],
			    searchitems: [
                    { display: '任务名称', name: 'instancename', type: 1 },
                    { display: '流程名称', name: 'workflowname', type: 1 },
                    { display: '发起公司', name: 'companyname', type: 1 },
                    { display: '发起人', name: 'createuser', type: 1 },
                    { display: '到达时间', name: 'arrivetime', type: 4 }
			    ],
			    sortname: "arrivetime",
			    sortorder: "desc",
			    usepager: true,
			    singleSelect: true,
			    useRp: true,
			    rp: 12,
			    multisel: false,
			    showTableToggleBtn: false,
			    resizable: false,
			    height: gh,
			    onError: showError,
			    preProcess: processData
			}
			);
                var dataList = null;
                function processData(data) {
                    dataList = data;
                    return data;
                }
                function transfld(fldval, row, td) {
                    var instanceId = $("instanceid", row).text();
                    var taskstate = $("taskstate", row).text();
                    if ("<%=EmpId %>" == "<%=EmployeeID %>") {
			        return "<a class='normal' href=\"javascript:dealTask('" + fldval + "');\">处理</a>";
			    }
			    else {
			        return "<a class='normal' href=\"javascript:viewTask('" + instanceId + "');\">查看</a>";
			    }


            }
            function chkCol(fldval, row) {
                return "<input type=checkbox class='chkcol' value='" + fldval + "'/>";
            }
            function showError(data) {
            }
            function app_reset(cmd, grid) {
                $("#flex1").clearQueryForm();
            }
            function app_edit(cmd, grid) {
                if ($('.trSelected', grid).length > 0) {
                    var editid = $('.trSelected', grid)[0].id.substr(3);
                    editid = $("#" + editid, dataList).find("instanceid").text();
                    openCenter("Admin_UpdateTitle.aspx?instanceId=" + editid, "_blank", 600, 400);
                }
                else {
                    alert("请选中一条记录");
                }
            }
            function app_stop(cmd, grid) {

                var editid = $('.trSelected', grid)[0].id.substr(3);
                editid = $("#" + editid, dataList).find("instanceid").text();
                openCenter("Admin_StopInstance.aspx?instanceId=" + editid, "_blank", 600, 400);
            }

            //转交
            function app_transfer(cmd, grid) {

                if (!jQuery(".chkcol:checked").val()) {
                    alert("请先选择任务");
                    return;
                }
                jQuery("#delegateUserId").val("");
                jQuery("#delegatePosId").val("");
                jQuery("#txtDelegate").val("");
                var arr = [];
                arr.push("<table><tr><td colspan='2'>")
                arr.push("你确认把选中的任务转交他人处理吗？", "<br/><br/>");
                arr.push("</td>")
                arr.push("<tr><td>")
                arr.push("任务接收人：")
                arr.push("</td><td>")
                arr.push("<input type=text value='' style='height:20px;width:80px;' class='textbox' id='txtDelegate'>");
                arr.push("&nbsp;<input type=button value='选择' onclick='selUser();'/>");
                arr.push("</td><tr>")
                ymPrompt.confirmInfo({
                    message: arr.join(""),
                    width: 360,
                    height: 200,
                    title: '任务转交',
                    maxBtn: false,
                    minBtn: false,
                    handler: transferTask
                });

            }

            function transferTask(cmd) {
                var empId = jQuery("#delegateUserId").val();
                var posId = jQuery("#delegatePosId").val();
                if (cmd == "ok" && empId != "") {
                    var list = [];
                    jQuery(".chkcol:checked").each(function () {
                        list.push(this.value);
                    });
                    var ret = _curClass.updateTaskOwner(list, empId, posId);
                    if (ret.error) {
                        alert("出错：" + ret.error.Message);
                    }
                    else {
                        alert("保存成功");
                        $("#flex1").flexReload();
                    }

                }
            }
            //委托
            function app_delegate(cmd, grid) {
                var arr = [];
                arr.push("<table><tr><td colspan='2'>")
                arr.push("你确认把选中的任务委托他人处理吗？", "<br/><br/>");
                arr.push("</td>")
                arr.push("<tr><td>")
                arr.push("任务接收人：")
                arr.push("</td><td>")
                arr.push("<input type=text value='' style='height:20px;width:80px;' class='textbox' id='txtDelegate'>");
                arr.push("&nbsp;<input type=button value='选择' onclick='selUser();'/>");
                arr.push("</td><tr>")
                ymPrompt.confirmInfo({
                    message: arr.join(""),
                    width: 360,
                    height: 200,
                    title: '任务委托',
                    maxBtn: false,
                    minBtn: false,
                    handler: delegateTask
                });

            }

            function selUser() {
                openCenter('../Common/UserTree.aspx?method=1&queryfield=empid,empname,posid&cid=delegateUserId,txtDelegate,delegatePosId', "_blank", 640, 500);
            }

            function delegateTask(cmd) {

                if (!jQuery(".chkcol:checked").val()) {
                    alert("请先选择任务");
                    return;
                }
                var empId = jQuery("#delegateUserId").val();
                if (cmd == "ok" && empId != "") {
                    var list = [];
                    jQuery(".chkcol:checked").each(function () {
                        list.push(this.value);
                    });
                    var ret = _curClass.updateTaskAgent(list, empId);
                    if (ret.error) {
                        alert("出错：" + ret.error.Message);
                    }
                    else {
                        alert("保存成功");
                        $("#flex1").flexReload();
                    }

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

            function viewTask(insId) {
                var url = "../AppFrame/AppWorkFlowInfo.aspx?InstanceId=" + insId;
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