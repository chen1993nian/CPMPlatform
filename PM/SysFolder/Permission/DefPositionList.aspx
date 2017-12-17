<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DefPositionList.aspx.cs" Inherits="EIS.Web.SysFolder.Permission.DefPositionList" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Pragma" content="no-cache" />
    <title>岗位列表</title>
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
    var curClass = EIS.Web.SysFolder.Permission.DefPositionList;
    var para = "";
    $(function () {
    });
    $("#flex1").flexigrid
			(
			{
			    url: '../../getxml.ashx',
			    params: [{ name: "queryid", value: "T_E_Org_Position" }
			        , { name: "cryptcond", value: "" }
			        , { name: "sindex", value: "" }
			        , { name: "condition", value: "<%=Request["condition"] %>" }
			    ],
			    colModel: [
			{ display: '序号', name: 'rowindex', width: 30, sortable: false, align: 'center' },
                 { display: '岗位名称', name: 'PositionName', width: 80, sortable: true, align: 'left', hide: false, renderer: fnEdit },
                 { display: '岗位编码', name: 'PositionCode', width: 60, sortable: true, align: 'left', hide: false, renderer: false },
                 { display: '公司名称', name: 'CompanyName', width: 80, sortable: true, align: 'left', hide: false, renderer: false },
                 { display: '部门名称', name: 'DeptName', width: 80, sortable: true, align: 'left', hide: false, renderer: false },
                 { display: '直接上级岗位', name: 'ParentPositionName', width: 80, sortable: true, align: 'left', hide: false, renderer: false },
                 { display: '岗位属性', name: 'PropName', width: 70, sortable: true, align: 'left', hide: false, renderer: false },
                 { display: '排序', name: 'OrderID', width: 30, sortable: true, align: 'left', hide: false, renderer: false },
                 { display: '操作', name: '_AutoID', width: 130, sortable: true, align: 'left', hide: false, renderer: addEmpRender }
			    ],
			    buttons: [
				{ name: '添加', bclass: 'add', onpress: app_add },
				{ name: '编辑', bclass: 'edit', onpress: app_edit },
				{ name: '删除', bclass: 'delete', onpress: app_delete },
				{ separator: true },
				{ name: '查询', bclass: 'view', onpress: app_query },
				{ name: '清空', bclass: 'clear', onpress: app_reset }

			    ],
			    searchitems: [
			    { display: '岗位编码', name: 'PositionCode', type: 1 },
                { display: '岗位名称', name: 'PositionName', type: 1 }
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
			    height: 'auto',
			    onError: showError,
			    preProcess: false,
			    onColResize: fnColResize
			}
	);
                function fnEdit(v, row) {
                    var pid = $(row).attr("id");
                    return "<a href='javascript:' onclick=\"RecEdit('" + pid + "');\" >" + v + "</a>"
                }
                function RecEdit(pid) {
                    var para = "PositionId=" + pid;
                    openCenter("DefPositionEdit.aspx?" + para, "_blank", 600, 400);
                }
                function addEmpRender(fldval, row) {
                    var empNum = $("EmpNum", row).text();
                    var positionId = fldval;
                    var arr = [];
                    arr.push("<a href=\"CompanyEmployeeEdit.aspx?positionId=" + positionId + "\" target='_self'>添加员工</a>");
                    arr.push("&nbsp;&nbsp;<a href=\"javascript:viewEmp('" + positionId + "');\">查看");
                    if (empNum != "0")
                        arr.push("（", empNum, "）");
                    arr.push("</a>");
                    return arr.join("");
                }

                function viewEmp(positionId) {
                    var url = "../../SysFolder/AppFrame/AppDefault.aspx?tblName=T_E_Org_DeptEmployee&condition=positionId=[QUOTES]" + positionId + "[QUOTES]&ext=600|300|000";
                    openCenter(url, "_blank", 700, 480);
                }

                function showError(data) {
                    alert("加载数据出错");
                }
                function app_add(cmd, grid) {
                    var para = "DeptID=<%=DeptID %>";
        openCenter("DefPositionEdit.aspx?" + para, "_blank", 600, 400);

    }
    function fnColResize(fieldname, width) {

    }
    function app_layout(cmd, grid) {
        //暂时有点儿问题，应该把fieldname 换成fieldid
        var fldlist = [];
        $('th', grid).each(function () {

            fldlist.push((this.fieldid || this.field) + "=" + ($(this).width() - 10) + "=" + $(this).css("display"));
        });
        var ret = curClass.saveLayout(fldlist, "T_E_Org_Position", "");
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
            para = "PositionId=" + editid;
            openCenter("DefPositionEdit.aspx?" + para, "_blank", 600, 400);
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
			                    var ret = curClass.RemovePosition(this.id.substr(3));
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
        var ret = window.showModalDialog("AppConditionDef.aspx?tblname=T_E_Org_Position&sindex=", "", "dialogHeight=500px;dialogWidth=400px;status=no;center=yes;resizable=yes;");
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