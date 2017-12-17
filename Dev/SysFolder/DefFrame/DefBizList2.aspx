<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DefBizList2.aspx.cs" Inherits="EIS.Studio.SysFolder.DefFrame.DefBizList2" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>业务对象列表</title>
    <meta http-equiv="Pragma" content="no-cache"/>
    <link rel="stylesheet" type="text/css" href="../../Css/DefStyle.css"/>
    <link rel="stylesheet" type="text/css" href="../../grid/css/flexigrid.css"/>
    <script type="text/javascript" src="../../grid/lib/jquery/jquery.js"></script>
    <script type="text/javascript" src="../../DatePicker/WdatePicker.js"></script> 
    <script type="text/javascript" src="../../js/lhgdialog.min.js"></script>    
    <script type="text/javascript" src="../../grid/flexigrid.js"></script>
    <style type="text/css">
        .flexigrid .red{color:Red;text-decoration:none;}
        .flexigrid .green{color:green;text-decoration:none;}
    </style>
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
				{ display: '序号', name: 'rowindex', width: 30, sortable: false, align: 'center', renderer: tranIndex },
				{ display: '业务名称', name: 'tablename', width: 160, sortable: true, align: 'left', renderer: tranName },
				{ display: '中文名称', name: 'tablenamecn', width: 120, sortable: true, align: 'left' },
				{ display: '类型', name: 'tabletype', width: 30, sortable: false, align: 'center', renderer: transtype },
				{ display: '创建日期', name: '_CreateTime', width: 100, sortable: true, align: 'right' },
				{ display: '业务定义', name: 'tablename', width: 200, sortable: false, align: 'center', renderer: transfld }
			],
			buttons: [
				{ name: '添加', bclass: 'add', onpress: app_add },
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
			sortname: "_CreateTime",
			sortorder: "asc",
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

			function tranIndex(v, row) {
			    return parseInt(v) - 1;
			}

			function tranName(fldval, row) {
			    return "<a class='normal' href=\"javascript:edit('" + fldval + "');\" target='_self'>" + fldval + "</a>";

			}
			function edit(tName) {
			    var url = "DefBizEdit2.aspx?tblname=" + tName;
			    var dlg = new $.dialog({
			        title: '子表信息', page: url
                        , btnBar: false, cover: true, lockScroll: true, width: 500, height: 340, bgcolor: 'gray'

			    });
			    dlg.ShowDialog();
			}
			function transfld(fldval, row) {
			    var arr = [];
			    var t = $("tablenamecn", row).text();
			    arr.push("<a class='normal red' href=\"DefTableFields.aspx?tblname=" + fldval + "&t=2\"  target='_self'>字段定义</a>");
			    arr.push("&nbsp;<a class='normal green' href=\"DefFieldsEditStyle.aspx?tblname=" + fldval + "&t=2\" target='_self'>编辑风格</a>");
			    arr.push("&nbsp;<a class='normal' href=\"DefFieldsEvent.aspx?tblname=" + fldval + "&t=2\" target='_self'>列表属性</a>");
			 
			    return arr.join("");
			}
			function transtype(fldval, row) {
			    return fldval == "1" ? "主表" : "子表";
			}
			function sonTable(fldval, row) {
			    var cat = $("tablecat", row).text();
			    return "";
			}
			function showError(data) {
			}
			function app_add(cmd, grid) {

			    var url = "DefBizEdit2.aspx?parent=<%=parent %>&tblname=";
			    var dlg = new $.dialog({
			        title: '子表信息', page: url
                    , btnBar: false, cover: true, lockScroll: true, width: 500, height: 340, bgcolor: 'gray'

			    });
			    dlg.ShowDialog();
			}
			function app_reset(cmd, grid) {
			    $("#flex1").clearQueryForm();
			}
			function app_edit(cmd, grid) {
			    if ($('.trSelected', grid).length > 0) {
			        var editid = $('.trSelected', grid)[0].id.substr(3);
			        var url = "DefBizEdit2.aspx?tblname=" + editid;
			        var dlg = new $.dialog({
			            title: '子表信息', page: url
                        , btnBar: false, cover: true, lockScroll: true, width: 500, height: 340, bgcolor: 'gray'

			        });
			        dlg.ShowDialog();

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
			                    var ret = EIS.Studio.SysFolder.DefFrame.DefBizList2.DelRecord(this.id.substr(3));
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
