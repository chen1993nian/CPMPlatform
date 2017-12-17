<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DefEmployeeLimitLeft.aspx.cs" Inherits="EIS.Web.SysFolder.Permission.DefEmployeeLimitLeft" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>员工查询</title>
    <link rel="stylesheet" type="text/css" href="../../grid/css/flexigrid.css"/>
    <link rel="stylesheet" type="text/css" href="../../Css/DefStyle.css"/>
    <script type="text/javascript" src="../../grid/lib/jquery/jquery.js"></script>
    <script type="text/javascript" src="../../grid/flexigrid.js"></script>
    <style type="text/css">
        .red{color:red;text-decoration:none;font-weight:normal;border:0px solid #eee;padding:2px;}
        .red:hover{color:white;text-decoration:none;background-color:#aaa;border:0px solid gray;}
        .green{color:green;text-decoration:none;font-weight:normal;border:0px solid #eee;padding:2px;}
        .green:hover{color:white;text-decoration:none;background-color:#aaa;border:0px solid gray;}
        
    </style>
</head>
<body style="margin:1px">
    <form id="form1" runat="server">
    <div id="griddiv" name="griddiv">
        <table id="flex1" style="display:none"></table>    
    </div>
    </form>
</body>
</html>
<script type="text/javascript">
<!--
    $("#flex1").flexigrid
	({
	    url: '../../getdata.ashx?ds=org_data',
	    params: [{ name: "queryid", value: "Org_EmpLimit" }
			    , { name: "condition", value: "" }
	    ],
	    colModel: [
			{ display: '序号', name: 'rowindex', width: 30, sortable: false, align: 'center' },
			{ display: '姓名-工号', name: 'EmployeeName', width: 100, sortable: true, align: 'left', renderer: transName },
			{ display: '部门', name: 'DeptName', width: 120, sortable: false, align: 'center' }
	    ],
	    buttons: [
			{ name: '查询', bclass: 'view', onpress: app_query },
			{ name: '清空', bclass: 'clear', onpress: app_reset }

	    ],
	    searchitems: [{ display: '工号 / 姓名', name: 'EmployeeName', type: 1 }],
	    sortname: "",
	    sortorder: "",
	    usepager: true,
	    singleSelect: false,
	    onToggleCol: false,
	    useRp: false,
	    rp: 15,
	    multisel: false,
	    showToggleBtn: false,
	    resizable: false,
	    height: 'auto'
	}
	);

    function transName(v, row) {
        var empId = $(row).attr("id");
        var empName = $("EmployeeName", row).text();
        return "<a href=\"DefEmployeeLimitEdit.aspx?roleId=" + empId + "&rolename=" + empName + "\" target='main'>" + v + "</a>";
    }
    function app_select() {
        jQuery(".chkcol").attr("checked", true);
    }

    function app_reset(cmd, grid) {
        $("#flex1").clearQueryForm();
    }

    function app_query() {
        $("#flex1").flexReload();
    }
    //-->
</script>
