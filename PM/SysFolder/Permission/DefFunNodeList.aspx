<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DefFunNodeList.aspx.cs" Inherits="EIS.Web.SysFolder.Permission.DefFunNodeList" %>

 
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
 
<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <title>功能结点维护</title>
    <meta http-equiv="Pragma" content="no-cache" />
    <link rel="stylesheet" type="text/css" href="../../grid/css/flexigrid.css" />
    <link rel="stylesheet" type="text/css" href="../../Css/DefStyle.css" />
    <link rel="stylesheet" type="text/css" href="../../Css/ymPrompt_vista/ymPrompt.css" />
    
    <script type="text/javascript" src="../../grid/lib/jquery/jquery.js"></script>
    <script type="text/javascript" src="../../grid/flexigrid.js"></script>
    <script type="text/javascript" src="../../DatePicker/WdatePicker.js"></script> 
    <script type="text/javascript" src="../../js/ymPrompt.js"></script> 
    <style type="text/css">
        a{text-decoration:none;}
    </style>
</head>
<body style="margin:1px;" scroll="no">
    <form runat="server" id="form1">
 
    <div id="griddiv" >
        <table id="flex1" style="display:none"></table>    
    </div>
    </form>
</body>
</html>
<script type="text/javascript"> 
<!--
    var AppDefault = EIS.Web.SysFolder.Permission.DefFunNodeList;
    var para = "";
    $(function () {
    });
    $("#flex1").flexigrid
			(
			{
			    url: '../../getxml.ashx',
			    params: [{ name: "queryid", value: "T_E_Sys_FunNode" }
			        , { name: "cryptcond", value: "" }
			        , { name: "sindex", value: "" }
			        , { name: "condition", value: "webId='<%=webId %>' and funpwbs='<%=funpwbs %>'" }
			    ],
			    colModel: [
			      { display: '序号', name: 'rowindex', width: 30, sortable: false, align: 'center' }
                , { display: '父级编码', name: 'FunPWBS', width: 80, sortable: true, align: 'left', hide: true, renderer: false, fieldid: '39655b87-259a-4da6-9936-3ce495f9e2f3' }
                , { display: '功能名称', name: 'FunName', width: 80, sortable: true, align: 'left', hide: false, renderer: fnFunName, fieldid: 'ec385479-b115-4d1b-ad72-0b54ea23b909' }
                , { display: '编码', name: 'FunWBS', width: 80, sortable: true, align: 'left', hide: false, renderer: false, fieldid: 'a9610b65-b941-4566-91c6-c2c60b81a153' }
                , { display: '打开方式', name: 'DispStyle', width: 80, sortable: true, align: 'left', hide: false, renderer: false, fieldid: 'd191e103-bfcc-4577-a691-15bfb5225900' }
                , { display: '是否显示', name: 'DispState', width: 58, sortable: true, align: 'left', hide: false, renderer: false, fieldid: '97789261-0167-486c-b062-8d237931133b' }
                , { display: '排序', name: 'OrderId', width: 80, sortable: true, align: 'left', hide: false, renderer: false, fieldid: 'ceeabf09-f360-44ab-8abd-50dbd51c55be' }
                , { display: '大栏目', name: 'WebID', width: 80, sortable: true, align: 'left', hide: true, renderer: false, fieldid: '6775fbaa-cd82-477b-b802-eea7b333690e' }
                , { display: '连接类型', name: 'LinkType', width: 80, sortable: true, align: 'left', hide: true, renderer: false, fieldid: 'afef8c3c-c439-4f9e-8d6d-2646ad405c76' }
			    ],
			    buttons: [
				{ name: '添加子节点', bclass: 'add', onpress: app_add },
				{ name: '编辑', bclass: 'edit', onpress: app_edit },
				{ name: '删除', bclass: 'delete', onpress: app_delete },
				{ separator: true },
				{ name: '查询', bclass: 'view', onpress: app_query },
				{ name: '清空', bclass: 'clear', onpress: app_reset }

			    ],
			    searchitems: [
			    { display: '功能名称', name: 'FunName', type: 1 }, { display: '编码', name: 'FunWBS', type: 1 }
			    ],
			    sortname: "",
			    sortorder: "",
			    usepager: true,
			    singleSelect: true,
			    useRp: true,
			    rp: 15,
			    multisel: false,
			    showTableToggleBtn: false,
			    resizable: false,
			    height: 360,
			    onError: showError,
			    preProcess: false,
			    onColResize: fnColResize
			}
			);
                function showError(data) {
                    alert("加载数据出错");
                }
                function app_add(cmd, grid) {
                    var newCode = AppDefault.GetNewWbs('<%=funpwbs %>', '<%=webId %>').value;
        para = "para=" + AppDefault.CryptPara("tblname=T_E_Sys_FunNode&T_E_Sys_FunNodecpro=FunPWBS=<%=funpwbs %>^1|FunWBS=" + newCode + "^0|WebID=<%=webId %>^1&condition=").value;
        openCenter("../AppFrame/AppInput.aspx?" + para, "_self", 800, 600);
    }
    function fnColResize(fieldname, width) {

    }
    function fnFunName(fldval, row) {
        var recId = $(row).attr("id");
        return "<a href='javascript:editRec(&#39;" + recId + "&#39;)' >" + fldval + "</a>";
    }
    function editRec(editid) {

        var curClass = AppDefault;
        para = "para=" + curClass.CryptPara("tblname=T_E_Sys_FunNode&sindex=&condition=_autoid='" + editid + "'").value;
        openCenter("../AppFrame/AppInput.aspx?" + para, "_self", 800, 600);
    }
    function app_reset(cmd, grid) {
        $("#flex1").clearQueryForm();
    }
    function app_edit(cmd, grid) {
        if ($('.trSelected', grid).length > 0) {
            var editid = $('.trSelected', grid)[0].id.substr(3);
            var curClass = AppDefault;
            para = "para=" + curClass.CryptPara("tblname=T_E_Sys_FunNode&sindex=&condition=_autoid='" + editid + "'").value;
            openCenter("../AppFrame/AppInput.aspx?" + para, "_self", 800, 600);
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
			                    var ret = AppDefault.DelRecord(this.id.substr(3));
			                    if (ret.error) {
			                        alert("删除出错：" + ret.error.Message);
			                    }
			                    else {
			                        alert("删除成功！");
			                        $("#flex1").flexReload();
			                    }
			                }
			            );

            }
        }
        else {
            alert("请选中一条记录");
        }
    }
    function addCallBack() {
        $("#flex1").flexReload();
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

