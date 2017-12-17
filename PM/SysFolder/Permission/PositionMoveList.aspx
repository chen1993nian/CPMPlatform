<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PositionMoveList.aspx.cs" Inherits="EIS.Web.SysFolder.Permission.PositionMoveList" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>岗位调整</title>
    <meta http-equiv="Pragma" content="no-cache" />
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
    var AppDefault = EIS.Web.SysFolder.Permission.PositionMoveList;
    var para = "";
    $(function () {
    });
    $("#flex1").flexigrid
			(
			{
			    url: '../../getdata.ashx',
			    params: [{ name: "queryid", value: "employee_query" }
			        , { name: "cryptcond", value: "" }
			        , { name: "sindex", value: "" }
			        , { name: "condition", value: "" }
			    ],
			    colModel: [
			    { display: '序号', name: 'rowindex', width: 30, sortable: false, align: 'center' },
                { display: '员工姓名', name: 'EmployeeName', width: 80, sortable: true, align: 'left', hide: false, renderer: false },
                { display: '性别', name: 'Sex', width: 40, sortable: true, align: 'left', hide: false, renderer: false },
                { display: '部门', name: 'DeptName', width: 100, sortable: true, align: 'left', hide: false, renderer: false },
                { display: '岗位', name: 'PositionName', width: 100, sortable: true, align: 'left', hide: false, renderer: false },
                { display: '单位', name: 'CompanyName', width: 100, sortable: true, align: 'left', hide: false, renderer: false },
                { display: '办公电话', name: 'Officephone', width: 100, sortable: true, align: 'left', hide: false, renderer: false },
                { display: '手机', name: 'Cellphone', width: 100, sortable: true, align: 'left', hide: false, renderer: false },
                { display: '操作', name: 'EmployeeName', width: 80, sortable: true, align: 'left', hide: false, renderer: colMove }


			    ],
			    buttons: [
				{ name: '查询', bclass: 'view', onpress: app_query },
				{ name: '清空', bclass: 'clear', onpress: app_reset }

			    ],
			    searchitems: [
			    { display: '员工姓名', name: 'EmployeeName', type: 1 },
                { display: '部门', name: 'DeptName', type: 1 },
                { display: '单位', name: 'CompanyName', type: 1 },
                { display: '办公电话', name: 'Officephone', type: 1 }

			    ],
			    sortname: "",
			    sortorder: "",
			    usepager: true,
			    singleSelect: true,
			    useRp: true,
			    rp: 10,
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
    function colMove(fldval, row) {
        var empId = $("EmployeeId", row).text();
        return "<a href=\"javascript:roleSet('" + empId + "')\">调整岗位</a>";
    }
    function roleSet(empId) {
        var url = "PositionMoveEdit.aspx?employeeId=" + empId;
        openCenter(url, "_blank", 480, 300);
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