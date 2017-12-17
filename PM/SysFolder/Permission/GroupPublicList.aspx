<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GroupPublicList.aspx.cs" Inherits="EIS.Web.SysFolder.Permission.GroupPublicList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Pragma" content="no-cache" />
    <title>公共员工分组列表</title>
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
<!--
    var curClass = EIS.Web.SysFolder.Permission.GroupPublicList;
    var para = "";
    $(function () {
    });
    $("#flex1").flexigrid
			(
			{
			    url: '../../getxml.ashx',
			    params: [{ name: "queryid", value: "T_E_Org_Group" }
			        , { name: "cryptcond", value: "" }
			        , { name: "sindex", value: "" }
			        , { name: "condition", value: "companyId='' and groupType='公共'" }
			    ],
			    colModel: [
	        { display: '序号', name: 'rowindex', width: 30, sortable: false, align: 'center' },
            { display: '分组名称', name: 'GroupName', width: 120, sortable: true, align: 'left', hide: false, renderer: false },
            { display: '分组成员', name: 'UserName', width: 300, sortable: true, align: 'left', hide: false, renderer: false },
            { display: '排序', name: 'OrderId', width: 40, sortable: true, align: 'left', hide: false, renderer: false }

			    ],
			    buttons: [
				{ name: '新建分组', bclass: 'add', onpress: app_add },
				{ name: '编辑', bclass: 'edit', onpress: app_edit },
				{ name: '删除', bclass: 'delete', onpress: app_delete },
				{ separator: true },
				{ name: '查询', bclass: 'view', onpress: app_query },
				{ name: '清空', bclass: 'clear', onpress: app_reset }

			    ],
			    searchitems: [
			    { display: '分组名称', name: 'GroupName', type: 1 },
                { display: '分组成员', name: 'UserName', type: 1 }
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
        openCenter("../AppFrame/AppInput.aspx?tblName=T_E_Org_Group&T_E_Org_Groupcpro=GroupType=公共^1", "_blank", 600, 400);
    }
    function setEmpCol(fldval, row) {
        return "<a href=\"javascript:setEmp('" + fldval + "')\" >设置</a>";
    }
    function setEmp(roleId) {
        openCenter("CompanyRoleSet.aspx?roleId=" + roleId, "_blank", 460, 500);
    }

    function fnColResize(fieldname, width) {

    }
    function app_layout(cmd, grid) {
        //暂时有点儿问题，应该把fieldname 换成fieldid
        var fldlist = [];
        $('th', grid).each(function () {

            fldlist.push((this.fieldid || this.field) + "=" + ($(this).width() - 10) + "=" + $(this).css("display"));
        });
        var ret = curClass.saveLayout(fldlist, "T_E_Org_Group", "");
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
            openCenter("../AppFrame/AppInput.aspx?tblName=T_E_Org_Group&condition=_autoid='" + editid + "'", "_blank", 600, 400);
        }
        else {
            alert("请选中一条记录");
        }
    }
    function app_delete(cmd, grid) {
        return;
        if ($('.trSelected', grid).length > 0) {
            if (confirm('确定删除这' + $('.trSelected', grid).length + '条记录吗?')) {
                $('.trSelected', grid).each
			            (
			                function () {
			                    var ret = curClass.DelRecord("T_E_Org_Group", this.id.substr(3));
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

    //-->
</script>