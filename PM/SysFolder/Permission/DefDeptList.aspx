<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DefDeptList.aspx.cs" Inherits="EIS.Web.SysFolder.Permission.DefDeptList" %>

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
    var curClass = EIS.Web.SysFolder.Permission.DefDeptList;
    var para = "";
    $(function () {
    });
    $("#flex1").flexigrid
			(
			{
			    url: '../../getdata.ashx',
			    params: [{ name: "queryid", value: "Org_Department" }
			        , { name: "cryptcond", value: "" }
			        , { name: "sindex", value: "" }
			        , { name: "condition", value: "<%=Request["condition"] %> and typeId='7C2F6B38-EDE8-4EB4-B667-6EABE32A1EEF'" }
			    ],
			    colModel: [
			{ display: '序号', name: 'rowindex', width: 30, sortable: false, align: 'center' },
                 //{ display: '部门编码', name: 'DeptCode', width: 60, sortable: true, align: 'left', hide: false, renderer: false},
                 { display: '部门名称', name: 'DeptName', width: 80, sortable: true, align: 'left', hide: false, renderer: false },
                 { display: '公司名称', name: 'CompanyName', width: 200, sortable: true, align: 'left', hide: false, renderer: false },
                 //{ display: '部门类型', name: 'TypeName', width: 60, sortable: true, align: 'left', hide: false, renderer: false },
                 { display: '部门负责人岗位', name: 'PicPosition', width: 80, sortable: true, align: 'left', hide: false, renderer: false },
                 { display: '分管领导岗位', name: 'UpPosition', width: 80, sortable: true, align: 'left', hide: false, renderer: false },
                 { display: '部门状态', name: 'DeptState', width: 60, sortable: true, align: 'left', hide: false, renderer: false },
                 { display: '排序', name: 'OrderID', width: 40, sortable: true, align: 'left', hide: false, renderer: false }
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
			    { display: '部门编码', name: 'DeptCode', type: 1 },
                { display: '部门名称', name: 'DeptName', type: 1 }
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

                    window.open("DefDeptEdit.aspx?DeptPWBS=<%=DeptPWBS %>", "_self");
        return;
        var newCode = curClass.GetNewDeptWbs("<%=DeptPWBS %>").value;
        para = "para=" + curClass.CryptPara("tblname=T_E_Org_Department&T_E_Org_Departmentcpro=DeptPWBS=<%=DeptPWBS %>^1|DeptWBS=" + newCode + "^1&sindex=&condition=").value;
        openCenter("../AppFrame/AppInput.aspx?" + para, "_blank", 800, 600);
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

            window.open("DefDeptEdit.aspx?DeptId=" + editid, "_self");
            return;
            para = "para=" + curClass.CryptPara("tblname=T_E_Org_Department&sindex=&condition=_autoid='" + editid + "'").value;
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
			                    var ret = curClass.RemoveDept(this.id.substr(3));
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