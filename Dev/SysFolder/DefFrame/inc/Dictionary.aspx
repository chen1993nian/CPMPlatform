<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dictionary.aspx.cs" Inherits="Studio.JZY.SysFolder.DefFrame.inc.Dictionary" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>无标题页</title>
    <link rel="stylesheet" type="text/css" href="../../../grid/css/flexigrid.css"/>
    <link rel="stylesheet" type="text/css" href="../../../Css/DefStyle.css"/>
    <script type="text/javascript" src="../../../grid/lib/jquery/jquery.js"></script>
    <script type="text/javascript" src="../../../grid/flexigrid.js"></script>
</head>
<body style="margin:2px">
    <form id="form1" runat="server">
    <div id="griddiv">
        <table id="flex1" style="display:none"></table>    
    </div>
    </form>
</body>
</html>
<script type="text/javascript">
<!--
    $("#flex1").flexigrid
	({
	    url: '../../../getdata.ashx',
	    params: [{ name: "queryid", value: "dictinfo" }
                , { name: "condition", value: "" }
	    ],
	    colModel: [
            { display: '序号', name: 'rowindex', width: 30, sortable: false, align: 'center' },
            { display: '字典名称', name: 'dictname', width: 100, sortable: true, align: 'left' },
            { display: '子项数', name: 'itemsnum', width: 80, sortable: false, align: 'left' },
            { display: '编辑子项', name: 'dictid', width: 80, sortable: false, align: 'center', renderer: editson },
            { display: '选择', name: 'dictname', width: 80, sortable: false, align: 'center', renderer: seldict }
	    ],
	    buttons: [
            { name: '字典维护', bclass: 'add', onpress: app_add },
            { separator: true },
            { name: '查询', bclass: 'view', onpress: app_query },
            { name: '清空', bclass: 'clear', onpress: app_reset }
	    ],
	    searchitems: [
            { display: '字典名称', name: 'dictname', type: 1 }
	    ],
	    sortname: "dictname",
	    sortorder: "asc",
	    usepager: true,
	    singleSelect: false,
	    useRp: true,
	    rp: 15,
	    multisel: false,
	    showTableToggleBtn: false,
	    resizable: false,
	    height: 355,
	    onError: showError
	});

    function editson(fldval, row) {
        var n = $("dictname", row).text();
        return "<a onclick=\"javascript:openCenter('../DefDictItems.aspx?dictid=" + fldval + "&dictname=" + escape(n) + "','_blank',600,400);\" href='javascript:'>编辑</a>";

    }
    function seldict(fldval, row) {
        var n = $("dictid", row).text();
        return "<a onclick=\"javascript:parent.frameElement.lhgDG.curWin.styleCallBack('<%=Request["key"] %>:<%=Request["titleName"] %>:" + fldval + "|" + n + "');parent.frameElement.lhgDG.cancel();\" href='javascript:'>确认</a>";
    }
    function showError(data) {
    }
    function app_add(cmd, grid) {
        openCenter("../DefDictFrame.aspx", "_blank", 860, 600);
        $("#flex1").flexReload();
    }
    function app_reset(cmd, grid) {
        $("#flex1").clearQueryForm();
    }
    function app_edit(cmd, grid) {
        if ($('.trSelected', grid).length > 0) {
            var editid = $('.trSelected', grid)[0].id.substr(3);
            openCenter("../DefDictEdit.aspx?editid=" + editid, "_blank", 260, 130);
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
