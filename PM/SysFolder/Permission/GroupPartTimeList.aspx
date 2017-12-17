﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GroupPartTimeList.aspx.cs" Inherits="EIS.Web.SysFolder.Permission.GroupPartTimeList" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Pragma" content="no-cache" />
    <title>兼职管理</title>
    <link rel="stylesheet" type="text/css" href="../../grid/css/flexigrid.css" />
    <link rel="stylesheet" type="text/css" href="../../Css/DefStyle.css" />
    <link rel="stylesheet" type="text/css" href="../../Css/ymPrompt_vista/ymPrompt.css" />
    
    <script type="text/javascript" src="../../grid/lib/jquery/jquery.js"></script>
    <script type="text/javascript" src="../../grid/flexigrid.js"></script>
    <script type="text/javascript" src="../../DatePicker/WdatePicker.js"></script> 
    <script type="text/javascript" src="../../js/ymPrompt.js"></script> 
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
    var AppDefault = EIS.Studio.SysFolder.Permission.GroupPartTimeList;
    var para = "";
    $(function () {
    });
    $("#flex1").flexigrid
    (
    {
        url: '../../getxml.ashx',
        params: [{ name: "queryid", value: "T_E_Org_DeptEmployee" }
                , { name: "cryptcond", value: "" }
                , { name: "sindex", value: "" }
                , { name: "condition", value: "DeptEmployeeType=1" }
        ],
        colModel: [
              { display: '序号', name: 'rowindex', width: 30, sortable: false, align: 'center' }
            , { display: '姓名', name: 'EmployeeName', width: 80, sortable: true, align: 'left', hide: false, renderer: false }
            , { display: '兼职单位', name: 'companyName', width: 80, sortable: true, align: 'left', hide: false, renderer: false }
            , { display: '兼职部门名称', name: 'DeptName', width: 80, sortable: true, align: 'left', hide: false, renderer: false }
            , { display: '兼职岗位名称', name: 'PositionName', width: 100, sortable: true, align: 'left', hide: false, renderer: false }
            , { display: '原单位', name: 'companyName2', width: 80, sortable: true, align: 'left', hide: false, renderer: false }
            , { display: '原部门', name: 'DeptName2', width: 69, sortable: true, align: 'left', hide: false, renderer: false }
            , { display: '原岗位', name: 'PositionName2', width: 100, sortable: true, align: 'left', hide: false, renderer: false }
            , { display: '排序', name: 'OrderID', width: 40, sortable: true, align: 'left', hide: false, renderer: false }

        ],
        buttons: [
            { name: '添加', bclass: 'add', onpress: app_add },
            { name: '编辑', bclass: 'edit', onpress: app_edit },
            { name: '删除', bclass: 'delete', onpress: app_delete },
            { separator: true },
            { name: '查询', bclass: 'view', onpress: app_query },
            { name: '清空', bclass: 'clear', onpress: app_reset }
        ],
        searchitems: [{ display: '姓名', name: 'EmployeeName', type: 1 }
        , { display: '兼职部门', name: 'DeptName', type: 1 }
        , { display: '兼职岗位', name: 'PositionName', type: 1 }

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
        para = "para=" + AppDefault.CryptPara("tblname=T_E_Org_DeptEmployee&T_E_Org_DeptEmployeecpro=CompanyId=<%= CompanyId%>^1|DeptEmployeeType=1^1&sindex=&condition=").value;
			    openCenter("../AppFrame/AppInput.aspx?" + para, "_blank", 800, 600);
			}
			function fnColResize(fieldname, width) {

			}

			function app_reset(cmd, grid) {
			    $("#flex1").clearQueryForm();
			}
			function app_edit(cmd, grid) {
			    if ($('.trSelected', grid).length > 0) {
			        var editid = $('.trSelected', grid)[0].id.substr(3);
			        var curClass = AppDefault;
			        para = "para=" + curClass.CryptPara("tblname=T_E_Org_DeptEmployee&sindex=&condition=_autoid='" + editid + "'").value;
			        openCenter("../AppFrame/AppInput.aspx?" + para, "_blank", 800, 600);
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
			function app_setquery() {
			    var ret = window.showModalDialog("AppConditionDef.aspx?tblname=T_E_Org_DeptEmployee&sindex=", "", "dialogHeight=500px;dialogWidth=400px;status=no;center=yes;resizable=yes;");
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

			//-->
</script>