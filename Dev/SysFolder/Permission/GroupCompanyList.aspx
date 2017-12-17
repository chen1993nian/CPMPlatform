<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GroupCompanyList.aspx.cs" Inherits="EIS.Studio.SysFolder.Permission.GroupCompanyList" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Pragma" content="no-cache" />
    <title>部门列表</title>
    <link rel="stylesheet" type="text/css" href="../../grid/css/flexigrid.css" />
    <link rel="stylesheet" type="text/css" href="../../Css/DefStyle.css" />
    <link rel="stylesheet" type="text/css" href="../../Css/ymPrompt_vista/ymPrompt.css" />
    
    <script type="text/javascript" src="../../grid/lib/jquery/jquery.js"></script>
    <script type="text/javascript" src="../../grid/flexigrid.js"></script>
    <script type="text/javascript" src="../../DatePicker/WdatePicker.js"></script> 
    <script type="text/javascript" src="../../js/ymPrompt.js"></script> 
    <script type="text/javascript" src="../../js/lhgdialog.min.js"></script>
</head>

<body style="margin:1px;" scroll="no">
    <form id="form1" runat="server">
    <div id="griddiv" >
        <table id="flex1" style="display:none"></table>    
    </div>

    </form>
</body>
</html>
<script type="text/javascript"> 
<!--
    var curClass = EIS.Studio.SysFolder.Permission.GroupCompanyList;
    var para = "";
    $(function () {
    });
    $("#flex1").flexigrid
			(
			{
			    url: '../../getxml.ashx',
			    params: [{ name: "queryid", value: "T_E_Org_Department" }
			        , { name: "cryptcond", value: "" }
			        , { name: "sindex", value: "" }
			        , { name: "condition", value: "DeptPWBS='<%=DeptPWBS %>' and TypeID in (select _AutoId from T_E_Org_DeptType where  TypeProp in (0,1,3))" }
			    ],
			    colModel: [
			    { display: '序号', name: 'rowindex', width: 30, sortable: false, align: 'center' },
                { display: '单位名称', name: 'DeptName', width: 174, sortable: true, align: 'left', hide: false, renderer: false },
                { display: '单位编码', name: 'DeptCode', width: 80, sortable: true, align: 'left', hide: false, renderer: false },
                { display: '单位简称', name: 'DeptAbbr', width: 80, sortable: true, align: 'left', hide: false, renderer: false },
                { display: '单位类型', name: 'TypeName', width: 100, sortable: true, align: 'left', hide: false, renderer: false },
                { display: '管理员账号', name: 'sa', width: 80, sortable: true, align: 'left', hide: false, renderer: false },
                //{ display: '单位状态', name: 'DeptState', width: 80, sortable: true, align: 'left', hide: false, renderer: false },
                { display: '排序', name: 'OrderID', width: 80, sortable: true, align: 'left', hide: false, renderer: false },
			    ],
			    buttons: [
				{ name: '添加单位', bclass: 'add', onpress: app_add },
				{ name: '编辑', bclass: 'edit', onpress: app_edit },
				{ name: '删除', bclass: 'delete', onpress: app_delete },
				{ separator: true },
				{ name: '查询', bclass: 'view', onpress: app_query },
				{ name: '清空', bclass: 'clear', onpress: app_reset },
			    { separator: true },
                { name: '帮助', bclass: 'help', onpress: app_help }

			    ],
			    searchitems: [
			    { display: '单位编码', name: 'DeptCode', type: 1 },
                { display: '单位名称', name: 'DeptName', type: 1 }
			    ],
			    sortname: "OrderID",
			    sortorder: "asc",
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
                    para = "para=" + curClass.CryptPara("DeptPWBS=<%=DeptPWBS %>").value;
        window.open("GroupCompanyEdit.aspx?" + para, "_self");
    }
    function fnColResize(fieldname, width) {

    }
    function app_layout(cmd, grid) {
        //暂时有点儿问题，应该把fieldname 换成fieldid
        var fldlist = [];
        $('th', grid).each(function () {

            fldlist.push((this.fieldid || this.field) + "=" + ($(this).width() - 10) + "=" + $(this).css("display"));
        });
        var ret = curClass.saveLayout(fldlist, "T_E_Org_Department", "");
        if (ret.error) {
            alert("保存出错：" + ret.error.Message);
        }
        else {
            alert("保存成功！");
        }
    }
    function app_reset(cmd, grid) {
        $("#flex1").clearQueryForm();
    }
    function app_edit(cmd, grid) {
        if ($('.trSelected', grid).length > 0) {
            var editid = $('.trSelected', grid)[0].id.substr(3);
            para = "para=" + curClass.CryptPara("DeptPWBS=<%=DeptPWBS %>&DeptId=" + editid).value;
            window.open("GroupCompanyEdit.aspx?" + para, "_self");
        }
        else {
            alert("请选中一条记录");
        }
    }
    function app_delete(cmd, grid) {
        //return;
        if ($('.trSelected', grid).length > 0) {
            if (confirm('确定删除这' + $('.trSelected', grid).length + '条记录吗?')) {
                $('.trSelected', grid).each
			            (
			                function () {
			                    var ret = curClass.DelRecord("T_E_Org_Department", this.id.substr(3));
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
    function app_setquery() {
        var ret = window.showModalDialog("AppConditionDef.aspx?tblname=T_E_Org_Department&sindex=", "", "dialogHeight=500px;dialogWidth=400px;status=no;center=yes;resizable=yes;");
        if (ret == "ok")
            window.location.reload();
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
        app_showHelp("./Help/HelpGroupCompanyList.aspx#a3", "分公司及下属单位信息设置帮助");
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