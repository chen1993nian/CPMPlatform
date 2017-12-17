<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ImportTable.aspx.cs" Inherits="EIS.Studio.SysFolder.DefFrame.ImportTable" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>导入业务对象</title>
    <meta http-equiv="Pragma" content="no-cache"/>
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

    var maiheight = document.documentElement.clientHeight;
    var otherpm = 150; //GridHead，toolbar，footer,gridmargin
    var gh = maiheight - otherpm;

    $("#flex1").flexigrid
    (
    {
        url: '../../getdata.ashx',
        params: [{ name: "queryid", value: "phytableinfo" }
                , { name: "condition", value: "" }
        ],
        colModel: [
            { display: '序号', name: 'rowindex', width: 30, sortable: false, align: 'center' },
            { display: '物理表名', name: 'TableName', width: 160, sortable: true, align: 'left' },
            { display: '导入', name: 'TableName', width: 80, sortable: false, align: 'center', renderer: transfld }
        ],
        buttons: [
            { name: '导入', bclass: 'add', onpress: app_add },
            { separator: true },
            { name: '查询', bclass: 'view', onpress: app_query },
            { name: '清空', bclass: 'clear', onpress: app_reset }
        ],
        searchitems: [
            { display: '物理表名', name: '[Name]', type: 1 }
        ],
        sortname: "",
        sortorder: "",
        usepager: true,
        singleSelect: false,
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
        var t = $("tabletype", row).text();
        return "<a class='normal' href=\"javascript:importTable('" + fldval + "')\" >导入</a>";
    }
    function importTable(tblName) {
        var cat = parent.getCatalog();
        if (cat) {

            var ret = EIS.Studio.SysFolder.DefFrame.ImportTable.ImportPhyTable(tblName, cat);
            if (ret.error) {
                alert(ret.error.Message);
            }
            else {
                alert("导入成功！");
            }
        }
        else {
            alert("请选择右边的分类");
        }
    }
    function showError(data) {
        //alert("加载数据出错");
    }
    function app_reset(cmd, grid) {
        $("#flex1").clearQueryForm();
    }

    function app_add(cmd, grid) {
        if ($('.trSelected', grid).length > 0) {
            if (confirm('确定导入这' + $('.trSelected', grid).length + '条记录吗?')) {
                var cat = parent.getCatalog();
                if (cat) {
                    $('.trSelected', grid).each
                    (
                        function () {
                            var tblName = this.id.substr(3);
                            var ret = EIS.Studio.SysFolder.DefFrame.ImportTable.ImportPhyTable(tblName, cat);
                            if (ret.error) {
                                alert(ret.error.Message);
                                return;
                            }

                        }
                    );

                }
                else {
                    alert("请选择右边的分类");
                }
                alert("导入成功！");
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
