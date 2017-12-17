<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CompanyEmployeeList.aspx.cs" Inherits="EIS.Web.SysFolder.Permission.CompanyEmployeeList" %>


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
    <style type="text/css">
        .flexigrid .red{color:Red;text-decoration:none;}
        .flexigrid .green{color:green;text-decoration:none;}
    </style> 
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
    var AppDefault = EIS.Web.SysFolder.Permission.CompanyEmployeeList;
    var para = "";
    $(function () {
    });
    $("#flex1").flexigrid
			(
			{
			    url: '../../getdata.ashx',
			    params: [{ name: "queryid", value: "_employee_info" }
			        , { name: "cryptcond", value: "" }
			        , { name: "sindex", value: "" }
			        , { name: "condition", value: "<%=Condition %>" }
			    ],
			    colModel: [
			    { display: '序号', name: 'rowindex', width: 30, sortable: false, align: 'center', renderer: renderSn },
                { display: '员工姓名', name: 'EmployeeName', width: 50, sortable: true, align: 'left', hide: false, renderer: renderName },
                { display: '公司名称', name: 'CompanyName', width: 60, sortable: true, align: 'left', hide: false, renderer: false },
                { display: '部门名称', name: 'DeptName', width: 60, sortable: true, align: 'left', hide: false, renderer: false },
                { display: '岗位名称', name: 'PositionName', width: 60, sortable: true, align: 'left', hide: false, renderer: false },
                { display: '登录帐号', name: 'LoginName', width: 60, sortable: true, align: 'left', hide: false, renderer: false },
                { display: '性别', name: 'Sex', width: 30, sortable: true, align: 'left', hide: false, renderer: false },
                { display: '手机号码', name: 'Cellphone', width: 80, sortable: true, align: 'left', hide: true, renderer: false },
                { display: '锁定', name: 'IsLocked', width: 30, sortable: true, align: 'left', hide: false, renderer: false },
                { display: '排序', name: 'DeOrderID', width: 30, sortable: true, align: 'left', hide: false, renderer: false },
                { display: '兼职', name: 'JzNum', width: 60, sortable: true, align: 'left', hide: false, renderer: fnJZ },
                { display: '操作', name: 'EmployeeName', width: 190, sortable: true, align: 'center', hide: false, renderer: opCol },
			    { display: '授权', name: 'EmployeeName', width: 190, sortable: true, align: 'center', hide: false, renderer: opColSq }

			    ],
			    buttons: [
				{ name: '新建员工', bclass: 'add', onpress: app_add },
				{ name: '批量导入', bclass: 'add', onpress: app_add2 },
				{ name: '编辑', bclass: 'edit', onpress: app_edit, hidden: true },
				{ name: '删除', bclass: 'delete', onpress: app_delete },
				{ name: '彻底删除', bclass: 'delete', onpress: app_delete2 },
				{ separator: true },
				{ name: '查询', bclass: 'view', onpress: app_query },
				{ name: '清空', bclass: 'clear', onpress: app_reset }
			    ],
			    searchitems: [{ display: '帐号', name: 'LoginName', type: 1 },
                { display: '员工姓名', name: 'EmployeeName', type: 1 },
                { display: '公司名称', name: 'CompanyName', type: 1 },
                { display: '岗位名称', name: 'PositionName', type: 1 }

			    ],
			    sortname: "CompanyName,DeptName,DeOrderID",
			    sortorder: "asc",
			    usepager: true,
			    singleSelect: true,
			    useRp: true,
			    rp: 15,
			    multisel: false,
			    showTableToggleBtn: false,
			    resizable: false,
			    height: 'auto',
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
                function app_add2(cmd, grid) {
                    window.open("OrgIO_EmpImport.aspx", "_blank");
                }
                function opCol(fldval, row) {
                    var empId = $("EmployeeId", row).text();
                    var arr = [];
                    arr.push("<a class='normal green' href=\"javascript:posSet('" + empId + "')\">调整岗位</a>&nbsp;");
                    arr.push("<a class='normal red' href=\"javascript:roleSet('" + empId + "')\">角色</a>&nbsp;");
                    if ("<%=base.LoginType %>" == "0")
                        arr.push("<a href='simLogin.aspx?empId=" + empId + "' target='_blank'>登录</a>");
                    return arr.join("");
                }
                function posSet(empId) {
                    var url = "PositionMoveEdit.aspx?employeeId=" + empId;
                    openCenter(url, "_blank", 480, 300);
                }
                function roleSet(empId) {
                    var url = "GroupEmployeeRoleSet.aspx?empId=" + empId;
                    openCenter(url, "_blank", 480, 500);
                }

                function renderSn(fldval, row) {
                    var empId = $("EmployeeId", row).text();
                    return "<a href='../Common/UserInfo.aspx?empId=" + empId + "' target='_blank'>" + fldval + "</a>";
                }
                function renderName(fldval, row, td) {
                    var deptId = $("RelationID", row).text();
                    var empId = $("EmployeeId", row).text();
                    $(td).attr("relationId", deptId);
                    return "<a href='CompanyEmployeeEdit.aspx?relationId=" + deptId + "' target='_self'>" + fldval + "</a>";
                }
                function fnJZ(fldval, row) {
                    var arr = [];
                    var empId = $("EmployeeId", row).text();
                    var empName = $("EmployeeName", row).text();
                    arr.push("<a href='javascript:' onclick=\"jzList('" + empId + "','" + empName + "');\" >兼职");
                    if (fldval != "0")
                        arr.push("（", fldval, "）");
                    arr.push("</a>");
                    return arr.join("");
                }
                function jzList(empId, empName) {
                    var url = "../../SysFolder/AppFrame/AppDefault.aspx?tblName=T_E_Org_DeptEmployee&cpro=EmployeeId="
                    + empId + "^1|EmployeeName=" + empName + "^1|CompanyId=^1|DeptEmployeeType=1^1&condition=DeptEmployeeType=1 and EmployeeId=[QUOTES]" + empId + "[QUOTES]&ext=600|300";
                    openCenter(url, "_blank", 700, 480);
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
                        var relationId = $('.trSelected>td:eq(1)', grid).attr("relationId");
                        window.open("CompanyEmployeeEdit.aspx?relationId=" + relationId, "_self");
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

                function app_delete2(cmd, grid) {
                    //return;
                    if ($('.trSelected', grid).length > 0) {
                        if (confirm('确定删除这' + $('.trSelected', grid).length + '条记录吗?')) {
                            $('.trSelected', grid).each
                                    (
                                        function () {
                                            var ret = AppDefault.RemoveEmployee(this.id.substr(3));
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

                function AutoOrg(fldval, row) {
                    //var ID = $("_AutoID", row).text();
                    var ID = $("EmployeeId", row).text();
                    var name = $("EmployeeName", row).text();
                    var names = $("PositionName", row).text();
                    var url = "../../WorkAsp/RelationTree/ProjectDataLimitProjectList.aspx?personid=" + ID + "&funid=";
                    return "<a href=\"" + url + "\");\" target=\"_blank\">工程项目</a>";
                }
                function AutoData(fldval, row) {
                    //  var ID = $("_AutoID", row).text();
                    var ID = $("EmployeeId", row).text();
                    var name = $("EmployeeName", row).text();
                    var names = $("PositionName", row).text();
                    var url = "../../sysfolder/permission/DefEmployeeLimitEdit.aspx?roleId=" + ID + "&rolename=" + name + " (" + names + ")&t=414&webId=";
                    url = encodeURI(url);
                    return "<a href=\"" + url + "\" target=\"_blank\">组织机构</a>";

                }
                function opColSq(fldval, row) {


                    var arr = [];
                    arr.push(AutoOrg(fldval, row));
                    arr.push(AutoData(fldval, row));

                    return arr.join("");
                }
                //-->
</script>