<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GroupEmployeeRoleList.aspx.cs" Inherits="EIS.Web.SysFolder.Permission.GroupEmployeeRoleList" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Pragma" content="no-cache" />
    <title>员工列表</title>
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
    var AppDefault = EIS.Web.SysFolder.Permission.GroupEmployeeRoleList;
    var para = "";
    $(function () {
    });
    $("#flex1").flexigrid
			(
			{
			    url: '../../getxml.ashx',
			    params: [{ name: "queryid", value: "T_E_Org_Employee" }
			        , { name: "cryptcond", value: "" }
			        , { name: "sindex", value: "" }
			        , { name: "condition", value: "<%=condition %>" }
			    ],
			    colModel: [
			    { display: '序号', name: 'rowindex', width: 30, sortable: false, align: 'center' },
                { display: '员工姓名', name: 'EmployeeName', width: 80, sortable: true, align: 'left', hide: false, renderer: empCol },
                { display: '登录账号', name: 'LoginName', width: 80, sortable: true, align: 'left', hide: false, renderer: false },
                { display: '部门名称', name: 'DeptName', width: 80, sortable: true, align: 'left', hide: false, renderer: false },
                { display: '性别', name: 'Sex', width: 31, sortable: true, align: 'left', hide: false, renderer: false },
                { display: '办公室电话', name: 'Officephone1', width: 80, sortable: true, align: 'left', hide: false, renderer: false },
                { display: '是否锁定', name: 'IsLocked', width: 80, sortable: true, align: 'left', hide: false, renderer: false },
                { display: '排序', name: 'OrderID', width: 31, sortable: true, align: 'left', hide: false, renderer: false },
                { display: '兼职', name: 'DeptEmployeeType', width: 32, sortable: true, align: 'left', hide: false, renderer: renderjz }
			    ],
			    buttons: [
				/*{ name: '新建员工', bclass: 'add', onpress: app_add },
				{ name: '编辑', bclass: 'edit', onpress: app_edit },
				{ name: '删除', bclass: 'delete', onpress: app_delete },*/
				{ separator: true },
				{ name: '查询', bclass: 'view', onpress: app_query },
				{ name: '清空', bclass: 'clear', onpress: app_reset }
			    ],
			    searchitems: [
                { display: '员工姓名', name: 'EmployeeName', type: 1 },
                { display: '登录账号', name: 'LoginName', type: 1 }
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
                    window.open("CompanyEmployeeEdit.aspx?deptId=<%=DeptID %>", "_self");
    }
    function empCol(fldval, row) {
        var empId = $("_AutoID", row).text();
        return "<a href=\"javascript:roleSet('" + empId + "')\">" + fldval + "</a>";
    }
    function roleSet(empId) {
        var url = "GroupEmployeeRoleSet.aspx?empId=" + empId;
        openCenter(url, "_blank", 480, 500);
    }
    function fnColResize(fieldname, width) {

    }
    function app_layout(cmd, grid) {
        //暂时有点儿问题，应该把fieldname 换成fieldid
        var fldlist = [];
        $('th', grid).each(function () {

            fldlist.push((this.fieldid || this.field) + "=" + ($(this).width() - 10) + "=" + $(this).css("display"));
        });
        var ret = AppDefault.saveLayout(fldlist, "T_E_Org_Employee", "");
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
            window.open("CompanyEmployeeEdit.aspx?deptId=<%=DeptID %>&EmployeeId=" + editid, "_self");
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
        var ret = window.showModalDialog("AppConditionDef.aspx?tblname=T_E_Org_Employee&sindex=", "", "dialogHeight=500px;dialogWidth=400px;status=no;center=yes;resizable=yes;");
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
    function renderjz(fldval, row) {
        if (fldval == "1")
            return "是";
        else
            return "否";
    }
    function addjz(fldval, row) {
        var empname = $("EmployeeName", row).text();
        return "<a href=\"javascript:addjzwin('" + fldval + "','" + empname + "');\">添加兼职</a>";
    }
    function addjzwin(empid, empname) {
        var p = "tblname=T_E_Org_DeptEmployee&T_E_Org_DeptEmployeecpro=EmployeeID=" + empid + "^1|EmployeeName=" + empname + "^1|DeptEmployeeType=1^1";
        var url = EIS.Web.SysFolder.AppFrame.AppDefault.CryptPara(p).value;
        url = "appInput.aspx?para=" + url;
        openCenter(url, "_blank", 600, 400);
    }
    //-->
</script>