<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DefDictList.aspx.cs" Inherits="EIS.Studio.SysFolder.DefFrame.DefDictList" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>无标题页</title>
    <link rel="stylesheet" type="text/css" href="../../grid/css/flexigrid.css"/>
    <link rel="stylesheet" type="text/css" href="../../Css/DefStyle.css"/>
    <script type="text/javascript" src="../../grid/lib/jquery/jquery.js"></script>
    <script type="text/javascript" src="../../grid/flexigrid.js"></script>
    <script type="text/javascript" src="../../js/lhgdialog.min.js?s=default,chrome,"></script>
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
        params: [{ name: "queryid", value: "dictinfo" }
                , { name: "condition", value: "DictCat = '<%=NodeWbs %>'" }
			],
			colModel: [
				{ display: '序号', name: 'rowindex', width: 30, sortable: false, align: 'center' },
				{ display: '字典名称', name: 'dictname', width: 140, sortable: true, align: 'left' },
				{ display: '字典编码', name: 'dictcode', width: 140, sortable: true, align: 'left' },
				{ display: '子项数', name: 'itemsnum', width: 40, sortable: false, align: 'left' },
				{ display: '编辑子项', name: 'dictid', width: 80, sortable: false, align: 'center', renderer: editson }
			],
			buttons: [
				{ name: '添加字典', bclass: 'add', onpress: app_add },
				{ name: '删除', bclass: 'delete', onpress: app_delete },
				{ separator: true },
				{ name: '查询', bclass: 'view', onpress: app_query },
				{ name: '清空', bclass: 'clear', onpress: app_reset },
                { name: '导入', bclass: 'layout', onpress: app_import },
	    		{ separator: true },
				{ name: '帮助', bclass: 'help', onpress: app_help }
			],
			searchitems: [
			    { display: '字典名称', name: 'dictname', type: 1 },
			    { display: '字典编码', name: 'dictcode', type: 1 }
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
			height: 'auto',
			onError: showError
			}
			);

			function editson(fldval, row) {
			    var n = $("dictname", row).text()
			    return "<a onclick=\"javascript:itemEdit('" + fldval + "');\" href='javascript:'>编辑</a>";

			}
			function itemEdit(dictid) {
			    var dlg = new jQuery.dialog({
			        title: '数据字典维护', maxBtn: false, page: '<%=Page.ResolveUrl("~") %>SysFolder/DefFrame/DefDictItems.aspx?dictid=' + dictid
                , btnBar: false, cover: true, lockScroll: true, width: 600, height: 400, bgcolor: 'black'
			    });
			    dlg.ShowDialog();
			}
			function showError(data) {
			    //alert("加载数据出错");
			}
			function app_add(cmd, grid) {
			    var dlg = new jQuery.dialog({
			        title: '数据字典维护', maxBtn: false, page: '<%=Page.ResolveUrl("~") %>SysFolder/DefFrame/DefDictItems.aspx?cat=<%=NodeWbs %>'
                , btnBar: false, cover: true, lockScroll: true, width: 600, height: 400, bgcolor: 'black'
			    });
			    dlg.ShowDialog();
			    return;
			    openCenter("DefDictItems.aspx?cat=<%=NodeWbs %>", "_blank", 600, 400);
            }
            function app_import() {

                window.open("ModelImport.aspx?catCode=<%=NodeWbs %>", "_blank");
			}
			function app_reset(cmd, grid) {
			    $("#flex1").clearQueryForm();
			}
			function app_edit(cmd, grid) {
			    if ($('.trSelected', grid).length > 0) {
			        var editid = $('.trSelected', grid)[0].id.substr(3);
			        openCenter("DefDictEdit.aspx?editid=" + editid, "_blank", 260, 130);
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
			                    EIS.Studio.SysFolder.DefFrame.DefDictList.DelRecord(this.id.substr(3)).value;
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


			function app_help() {
			    app_showHelp("Help/HelpDictionary.aspx", "字典定义帮助");
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
