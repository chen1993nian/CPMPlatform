<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DeptTree_Relation.aspx.cs" Inherits="EIS.Studio.SysFolder.DefFrame.inc.DeptTree_Relation" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>查询条件设置</title>
    <link rel="stylesheet" type="text/css" href="../../../Css/appStyle.css"/>
    <link type="text/css" href="../../../Css/jquery-ui/lightness/jquery-ui-1.7.2.custom.css" rel="stylesheet" />
	<script type="text/javascript" src="../../../js/jquery-1.8.0.min.js"></script>
	<script type="text/javascript" src="../../../js/jquery-ui-1.8.23.custom.min.js"></script>
	<style type="text/css">
	a{text-decoration:none;}
	a:hover{text-decoration:underline;}
	ul.connectedSortable, ul.connectedSortable2 
	{
	    list-style-type: none; 
	    margin: 0; 
	    padding: 0; 
	    background: #eee; 
	    padding: 3px; 
	    width: 114px;}
	.connectedSortable li, .connectedSortable2 li  
	{
	    margin: 0px 3px 2px 3px; 
	    padding: 2px; 
	    font-size: 1.0em; 
	    width: 100px;
	    cursor:default;
	    text-align:center;
		word-break:keep-all;/* 不换行 */
		white-space:nowrap;/* 不换行 */
		overflow:hidden;/* 内容超出宽度时隐藏超出部分的内容 */
        text-overflow:ellipsis;}
	    #tablesel,#querysel,#tablefld,#queryfld
	    {
	        min-height:25px;
            height:100%;
            _height:25px;
        }
        
      	 #maindiv  
	{
	    margin-left:auto; 
	    margin-right:auto; 
	    padding: 2px; 
		overflow:auto;
	}  
    table.frametbl
    {
	    border-collapse: collapse;
        margin-left:auto;
	    margin-right:auto;
	    font-size: 12px;
	    line-height:20px;
        width:510px;
	    border:#808080 1px solid;
	    color:#393939;
	    background:#FAF8F8;
	    table-layout:fixed;
    }
    table.frametbl td{padding:2px;}
	</style>
		<script type="text/javascript">
	$(function() {
		$("#tablefld, #tablesel").sortable({
			connectWith: '.connectedSortable'
			,placeholder: 'ui-state-highlight'
			,dropOnEmpty: true
		}).disableSelection();
		
		$("#queryfld, #querysel").sortable({
			connectWith: '.connectedSortable2'
			,placeholder: 'ui-state-highlight'
			,dropOnEmpty: true
		}).disableSelection();
		        $(window).resize(function () {
            $("#maindiv").height($(document.body).height() - 60);
        });
        $("#maindiv").height($(document.body).height() - 60);

        $("#tablefld>li").dblclick(function () {
            $(this).appendTo("#tablesel");
        });

        $("#queryfld>li").dblclick(function () {
            $(this).appendTo("#querysel");
        });

        $("#maindiv li").attr("title", function () { return this.innerText });
	});
	function save()
	{
	   var tablelist= $('#tablesel').sortable('toArray');
	   var querylist= $('#querysel').sortable('toArray');
        if(querylist.length != tablelist.length )
        { 
            alert("选择数据不相等");
            return false;
        }
        parent.frameElement.lhgDG.curWin.styleCallBack("<%=Request["key"] %>:<%=Request["titleName"] %>:|"+tablelist.join(",")+"|"+querylist.join(","));
        parent.frameElement.lhgDG.cancel();
	}
        function getOpenerValue(ctlName) {
            return parent.frameElement.lhgDG.curWin.document.getElementById(ctlName).value;
        }
	</script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="menubar">
    <ul>
    <li><a href="javascript:" onclick="save();">确认</a></li>
    <li><a href="javascript:" onclick="parent.frameElement.lhgDG.cancel();">关闭</a> </li>
    </ul></div>
    <div id="maindiv">
    <table align="center" class="frametbl" width="98%" border="1">
    <tr>
        <th align="center" height="30">表单字段</th>
        <th align="center">选中字段</th>
        <th align="center">选中字段</th>
        <th align="center">查询字段</th>
    </tr>
    <tr>
        <td width="25%" valign=top align="center">
            <ul id="tablefld" class="connectedSortable">
                <%=fieldlist1 %>
            </ul>
        </td>
        <td width="25%"  valign=top align="center">
            <ul id="tablesel" class="connectedSortable">
            <%=fieldlist1in %>
            </ul>
        </td>
        <td width="25%" valign=top align="center">
            <ul id="querysel" class="connectedSortable2">
            <%=fieldlist2in %>
            </ul>
        </td>
        <td  valign=top align="center">
            <ul id="queryfld" class="connectedSortable2">
                <%=fieldlist2 %>
            </ul>
        </td>
    </tr></table></div>
    </form>
</body>

</html>
