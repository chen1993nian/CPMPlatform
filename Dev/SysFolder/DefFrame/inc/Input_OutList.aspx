<%@ Page language="c#" Codebehind="Input_OutList.aspx.cs" AutoEventWireup="false" Inherits="EIS.Studio.SysFolder.DefFrame.Input_OutList" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>弹出列表</title>
    <link rel="stylesheet" type="text/css" href="../../../grid/css/flexigrid.css"/>
    <link rel="stylesheet" type="text/css" href="../../../Css/DefStyle.css"/>
    <script type="text/javascript" src="../../../grid/lib/jquery/jquery.js"></script>
    <script type="text/javascript" src="../../../grid/flexigrid.js"></script>
</head>
<body style="margin:0px">
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
	(
	{
	url: '../../../getdata.ashx',
	params:[{name:"queryid",value:"tableinfo"}
			,{name:"condition",value:"(TableType=1 or TableType=3)"}
			],
	colModel : [
		{display: '序号', name : 'rowindex', width : 30, sortable : false, align: 'center'},
		{display: '业务名称', name : 'tablename', width : 100, sortable : true, align: 'left'},
		{display: '中文名称', name : 'tablenamecn', width : 100, sortable : true, align: 'left'},
		{display: '创建日期', name : '_CreateTime', width : 55, sortable : true, align: 'right'},
		{display: '设置关系', name : 'tablename', width : 80, sortable : false, align: 'center',renderer:transattr}
		],
	buttons : [
		{name: '查询', bclass: 'view', onpress : app_query},
		{name: '清空', bclass: 'clear', onpress : app_reset}
		],
	searchitems :[
		{display: '业务名称', name : 'tablename',type:1},
		{display: '中文名称', name : 'tablenamecn',type:1}
		],
	sortname: "",
	sortorder: "",
	usepager: true,
	singleSelect:false,
	useRp: true,
	rp: 15,
	multisel:false,
	showTableToggleBtn: false,
	resizable:false,
    height:380,
	onError:showError
	}
	);

	function transattr(fldval,row)
	{
		var t=$("tableid",row).text();
		return "<a class='normal' href='input_outlist_relation.aspx?key=<%=Request["key"] %>&fieldid=<%=Request["fieldid"] %>&queryid="+t+"' target='_self'>维护</a>";
	}

	function showError(data)
	{
		//alert("加载数据出错");
	}
	function app_reset(cmd,grid)
	{
        $("#flex1").clearQueryForm();
	}

	function app_query()
	{
		$("#flex1").flexReload();
	}
//-->
</script>
