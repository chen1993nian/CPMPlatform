<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DefBizList.aspx.cs" Inherits="EIS.Studio.SysFolder.DefFrame.DefBizList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>业务对象列表</title>
    <meta http-equiv="Pragma" content="no-cache"/>
    <link rel="stylesheet" type="text/css" href="../../grid/css/flexigrid.css"/>
    <link rel="stylesheet" type="text/css" href="../../Css/DefStyle.css"/>
    <script type="text/javascript" src="../../grid/lib/jquery/jquery.js"></script>
    <script type="text/javascript" src="../../DatePicker/WdatePicker.js"></script> 
    <script type="text/javascript" src="../../js/jquery.notice.js"></script>
    <script type="text/javascript" src="../../grid/flexigrid.js"></script>
    <script type="text/javascript" src="../../js/lhgdialog.min.js?s=default,chrome,"></script>
</head>
<body style="margin:1px;">
    <form id="form1" runat="server">
    <div id="griddiv" >
        <table id="flex1" style="display:none"></table>    
    </div>
    </form>
    <form style="display:none;" id="frmExport" action="ModelExport.aspx" method="post" target="_blank">
        <input id="tblName" name="tblName" value=""/>
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
                , { name: "condition", value: "<%=condition %>" }
			],
			colModel: [
				{ display: '序号', name: 'tablename', width: 30, sortable: false, align: 'center', renderer: chkCol },
				{ display: '业务名称', name: 'tablename', width: 300, sortable: true, align: 'left', renderer: transName },
				{ display: '中文名称&nbsp;-（子表数）', name: 'tablenamecn', width: 180, sortable: true, align: 'left', renderer: transCn },
				{ display: '类型', name: 'tabletype', width: 30, sortable: false, align: 'center', renderer: transtype },
				{ display: '创建日期', name: '_CreateTime', width: 100, sortable: true, align: 'right' },
				{ display: '修改日期', name: '_UpdateTime', width: 100, sortable: true, align: 'right' },
				{ display: '操作', name: 'tablename', width: 80, sortable: false, align: 'center', renderer: transfld }
			],
			buttons: [
				{ name: '新建业务', bclass: 'add', onpress: app_add },
				{ name: '删除', bclass: 'delete', onpress: app_delete },
				{ separator: true },
				{ name: '查询', bclass: 'view', onpress: app_query },
				{ name: '清空', bclass: 'clear', onpress: app_reset },
				{ separator: true },
				{ name: '全选', bclass: 'layout', onpress: app_select },
				{ name: '导出', bclass: 'layout', onpress: app_export },
				{ name: '导入', bclass: 'layout', onpress: app_import },
	    		{ separator: true },
				{ name: '帮助', bclass: 'help', onpress: app_help }
			],
			searchitems: [
			    { display: '业务名称', name: 'tablename', type: 1 },
			    { display: '中文名称', name: 'tablenamecn', type: 1 },
			    { display: '创建时间', name: '_CreateTime', type: 4 }
			],
			sortname: "_UpdateTime",
			sortorder: "desc",
			usepager: true,
			singleSelect: true,
			useRp: true,
			rp: 12,
			multisel: false,
			showTableToggleBtn: false,
			resizable: false,
			height: "auto",
			onError: showError
			}
			);

			function transName(fldval, row) {
			    return "<a class='red' href=\"DefTableFrame.aspx?tblname=" + fldval + "\" target='_blank'>" + fldval + "</a>";
			}

			function transCn(fldval, row) {
			    var n = $("sonnum", row).text();
			    if (n == "0")
			        return fldval;
			    else
			        return fldval + "&nbsp;<b style='color:red;'>（" + n + "）</b>";
			}

			function transfld(fldval, row) {
			    var arr = [];
			    var t = $("tablenamecn", row).text();
			    arr.push("<a class='red' href=\"javascript:app_copy('" + fldval + "');\" >复制</a>");
			    arr.push("&nbsp;&nbsp;<a class='green' href=\"javascript:app_export('" + fldval + "');\" >导出</a>");
			    return arr.join("");
			}
			function chkCol(fldval, row) {
			    return "<input type=checkbox class='chkcol' value='" + fldval + "'/>";
			}
			function app_copy(t) {
			    var dlg = new jQuery.dialog({
			        title: '业务模型复制', maxBtn: false, page: 'SysFolder/DefFrame/DefBizCopy.aspx?tblName=' + t
                    , btnBar: false, cover: true, lockScroll: true, width: 800, height: 600, bgcolor: 'black'
			    });
			    dlg.ShowDialog();
			}
			function app_select() {
			    jQuery(".chkcol").attr("checked", true);
			}
			function app_export(t) {
			    var arr = [];
			    if (arguments.length == 2) {
			        jQuery(".chkcol:checked").each(function () {
			            arr.push(this.value);
			        });
			    }
			    else {
			        arr.push(t);
			    }
			    if (arr.length == 0) {
			        alert("请先选择要导出的业务"); return;
			    }
			    var dlg = new jQuery.dialog({
			        title: '业务模型导出', maxBtn: false, page: 'SysFolder/DefFrame/ModelExport.aspx?tblName=' + arr.join(",")
                    , btnBar: false, cover: true, lockScroll: true, width: 800, height: 600, bgcolor: 'black'
			    });
			    dlg.ShowDialog();
			}
			function app_import() {
			    var dlg = new jQuery.dialog({
			        title: '业务模型导入', maxBtn: false, page: 'SysFolder/DefFrame/ModelImport.aspx?catCode=<%=nodewbs %>'
                    , btnBar: false, cover: true, lockScroll: true, width: 800, height: 600, bgcolor: 'black'
			    });
			    dlg.ShowDialog();
			}
			function transtype(fldval, row) {
			    return fldval == "1" ? "主表" : "子表";
			}
			function sonTable(fldval, row) {
			    if (sonnum == "0")
			        return "<a class='normal' href='DefBizList2.aspx?parent=" + fldval + "' target='_blank'></a>";
			    else
			        return "<a class='normal' href='DefBizList2.aspx?parent=" + fldval + "' target='_blank'>（" + sonnum + "）</a>";
			}
			function showError(data) {
			}
			function app_add(cmd, grid) {
			    var url = "DefBizEdit.aspx?nodewbs=<%=nodewbs %>&parent=&t=1";
			    window.open(url, "_blank");
			    //window.top.addTab(Math.random(),"新建业务",url);
			}
			function app_reset(cmd, grid) {
			    $("#flex1").clearQueryForm();
			}
			function app_edit(cmd, grid) {
			    if ($('.trSelected', grid).length > 0) {
			        var editid = $('.trSelected', grid)[0].id.substr(3);
			        window.open("DefTableFrame.aspx?tblname=" + editid + "", "_blank");
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
			                    var ret = EIS.Studio.SysFolder.DefFrame.DefBizList.DelRecord(this.id.substr(3));
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

			function app_help() {
			    app_showHelp("Help/HelpTableDefine01.aspx", "业务表单定义帮助");
			}

			function app_showHelp(urlStr, titleStr) {
			    if ((titleStr == undefined) || (titleStr == "")) titleStr = "帮助";
			    var win = new $.dialog({
			        id: 'HelpWin'
                    , cover: true
                    , maxBtn: true
                    , minBtn: true
                    , btnBar: true
                    , lockScroll: false
                    , title: titleStr
                    , autoSize: false
                    , width: 1150
                    , height: 600
                    , resize: true
                    , bgcolor: 'black'
                    , iconTitle: false
                    , page: urlStr
			    });
			    win.ShowDialog();
			}
			//-->
</script>
