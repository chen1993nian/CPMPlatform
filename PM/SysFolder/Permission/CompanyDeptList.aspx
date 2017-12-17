<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CompanyDeptList.aspx.cs" Inherits="EIS.Web.SysFolder.Permission.CompanyDeptList" %>


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
    var curClass = EIS.Web.SysFolder.Permission.CompanyDeptList;
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
			        , { name: "condition", value: "DeptPWBS='<%=DeptPWBS %>'" }
			    ],
			    colModel: [
			{ display: '序号', name: 'rowindex', width: 30, sortable: false, align: 'center' },
                { display: '部门编码', name: 'DeptCode', width: 80, sortable: true, align: 'left', hide: false, renderer: false, fieldid: '059333b8-4b2f-44f1-938a-23cc273415db' },
                 { display: '部门名称', name: 'DeptName', width: 174, sortable: true, align: 'left', hide: false, renderer: false, fieldid: '7a2484be-fafd-4a8a-9122-22e9840a176f' },
                 { display: '部门类型', name: 'TypeName', width: 100, sortable: true, align: 'left', hide: false, renderer: false, fieldid: '50ca0d25-06b7-439e-8f28-38a6b2d6d858' },
                 { display: '部门负责人岗位', name: 'PicPosition', width: 80, sortable: true, align: 'left', hide: false, renderer: false, fieldid: 'c3ad66ed-aade-423e-8e26-839c51dead57' },
                 { display: '部门状态', name: 'DeptState', width: 80, sortable: true, align: 'left', hide: false, renderer: false, fieldid: '008ca636-ab0c-4271-aeb6-d5743f6b82a6' },
                 { display: '排序', name: 'OrderID', width: 80, sortable: true, align: 'left', hide: false, renderer: false, fieldid: '76ba2d74-00cc-4dcc-b901-fdbfc3130286' }
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
                    window.open("DefDeptEdit.aspx?DeptPWBS=<%=DeptPWBS %>", "_self");
        return;
        var newCode = curClass.GetNewDeptWbs("<%=DeptPWBS %>").value;
        para = "para=" + curClass.CryptPara("tblname=T_E_Org_Department&T_E_Org_Departmentcpro=DeptPWBS=<%=DeptPWBS %>^1|CompanyId=<%=CompanyId %>^1|DeptWBS=" + newCode + "^1&sindex=&condition=").value;
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