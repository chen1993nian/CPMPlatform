<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DefTreeList.aspx.cs" Inherits="EIS.Studio.SysFolder.DefFrame.DefTreeList" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>树型页面列表</title>
    <link rel="stylesheet" type="text/css" href="../../grid/css/flexigrid.css"/>
    <link rel="stylesheet" type="text/css" href="../../Css/DefStyle.css"/>
    <script type="text/javascript" src="../../grid/lib/jquery/jquery.js"></script>
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
    $("#flex1").flexigrid
    (
    {
        url: '../../getdata.ashx',
        params: [{ name: "queryid", value: "treeinfo" }
			    , { name: "condition", value: "CatCode like '<%=Request["nodewbs"] %>%'" }
		],
		colModel: [
			{ display: '序号', name: 'rowindex', width: 30, sortable: false, align: 'center' },
			{ display: '树型名称', name: 'treename', width: 200, sortable: true, align: 'left' },
			{ display: '数据源名称', name: 'Connection', width: 160, sortable: true, align: 'left' },
			{ display: '创建日期', name: 'createdate', width: 100, sortable: true, align: 'right' },
			{ display: '属性定义', name: 'treeid', width: 80, sortable: false, align: 'center', renderer: transattr }
		],
		buttons: [
			{ name: '新建树', bclass: 'add', onpress: app_add },
			//{name: '修改', bclass: 'edit', onpress : app_edit},
			{ name: '删除树', bclass: 'delete', onpress: app_delete },
			{ separator: true },
			{ name: '查询', bclass: 'view', onpress: app_query },
			{ name: '清空', bclass: 'clear', onpress: app_reset }
		],
		searchitems: [
			{ display: '树型名称', name: 'treename', type: 1 },
			{ display: '创建日期', name: '_createdate', type: 4 }
		],
		sortname: "",
		sortorder: "",
		usepager: true,
		singleSelect: false,
		useRp: true,
		rp: 15,
		multisel: false,
		showTableToggleBtn: false,
		resizable: false,
		height: 355,
		onError: showError
		}
		);
		function transattr(fldval, row) {
		    var n = $("treename", row).text();
		    var arr = [];
		    arr.push("<a class='normal' href=\"javascript:window.top.addTab('" + fldval + "','" + n + "','SysFolder/DefFrame/DefTreeEdit.aspx?treeId=" + fldval + "');\">业务定义</a>");
		    arr.push("&nbsp;<a class='normal' href=\"DefTreeEdit.aspx?treeId=" + fldval + "\" target='_blank'>弹出</a>");
		    return arr.join("");
		}

		function showError(data) {
		    //alert("加载数据出错");
		}
		function app_add(cmd, grid) {
		    openCenter("DefTreeEdit.aspx?nodewbs=<%=Request["nodewbs"] %>", "_blank", 800, 500);
		}
		function app_reset(cmd, grid) {
		    $("#flex1").clearQueryForm();
		}
		function app_edit(cmd, grid) {
		    if ($('.trSelected', grid).length > 0) {
		        var editid = $('.trSelected', grid)[0].id.substr(3);
		        openCenter("DefTreeEdit.aspx?treeId=" + editid, "_blank", 800, 500);
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
			                EIS.Studio.SysFolder.DefFrame.DefTreeList.DelRecord(this.id.substr(3)).value;
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
