<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DefBizList3.aspx.cs" Inherits="EIS.Studio.SysFolder.DefFrame.DefBizList3" ValidateRequest="false" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>业务对象列表</title>
    <meta http-equiv="Pragma" content="no-cache"/>
    <link rel="stylesheet" type="text/css" href="../../grid/css/flexigrid.css"/>
    <link rel="stylesheet" type="text/css" href="../../Css/DefStyle.css"/>
    <script type="text/javascript" src="../../grid/lib/jquery/jquery.js"></script>
    <script type="text/javascript" src="../../DatePicker/WdatePicker.js"></script> 
    
    <script type="text/javascript" src="../../grid/flexigrid.js"></script>
</head>
<body style="margin:2px">
    <form id="form1" runat="server">
    <div id="griddiv" >
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
        params: [{ name: "queryid", value: "tableinfo" }
                , { name: "condition", value: "<%=othercond %>" }
			],
			colModel: [
				{ display: '序号', name: 'rowindex', width: 30, sortable: false, align: 'center' },
				{ display: '业务名称', name: 'tablename', width: 160, sortable: true, align: 'left' },
				{ display: '中文名称', name: 'tablenamecn', width: 120, sortable: true, align: 'left' },
				{ display: '创建日期', name: '_CreateTime', width: 100, sortable: true, align: 'right' }
			],
			buttons: [
				{ name: '添加', bclass: 'add', onpress: app_add },
				{ name: '修改', bclass: 'edit', onpress: app_edit },
				{ name: '删除', bclass: 'delete', onpress: app_delete },
				{ separator: true },
				{ name: '查询', bclass: 'view', onpress: app_query },
				{ name: '清空', bclass: 'clear', onpress: app_reset }
			],
			searchitems: [
			    { display: '业务名称', name: 'tablename', type: 1 },
			    { display: '中文名称', name: 'tablenamecn', type: 1 },
			    { display: '创建时间', name: '_CreateTime', type: 4 }
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
			function transstyle(fldval, row) {
			    var t = $("tabletype", row).text();
			    if (t == "2") //子表
			        return "";
			    else
			        return "<a class='normal' href='../DefFrame/DefTableStyle.aspx?tblname=" + fldval + "' target='_self'>样式</a>";

			}
			function transfld(fldval, row) {
			    var arr = [];
			    var t = $("tablenamecn", row).text();
			    arr.push("<a class='normal' href=\"DefTableFrame2.aspx?tblname=" + fldval + "\"  target='_blank'>业务定义</a>");
			    //arr.push("&nbsp;<a class='normal' href=\"DefTableFrame.aspx?tblname=" + fldval + "\" target='_blank'>弹出</a>");
			    return arr.join("");
			}

			function sonTable(fldval, row) {
			    var cat = $("tablecat", row).text();
			    return "";
			}
			function showError(data) {
			}
			function app_add(cmd, grid) {
			    var url = "DefBizEdit4.aspx?parent=<%=parent %>&tblname=";

			    openCenter(url, "_blank", 660, 400);
			}
			function app_reset(cmd, grid) {
			    $("#flex1").clearQueryForm();
			}
			function app_edit(cmd, grid) {
			    if ($('.trSelected', grid).length > 0) {
			        var editid = $('.trSelected', grid)[0].id.substr(3);
			        var url = "DefBizEdit4.aspx?tblname=" + editid;
			        openCenter(url, "_blank", 660, 400);
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
			                    var ret = EIS.Studio.SysFolder.DefFrame.DefBizList3.DelRecord(this.id.substr(3));
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
