<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserTree_Relation.aspx.cs" Inherits="EIS.Studio.SysFolder.DefFrame.inc.UserTree_Relation" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>查询条件设置</title>
    <link rel="stylesheet" type="text/css" href="../../../Css/appStyle.css"/>
    <link type="text/css" href="../../../Css/jquery-ui/lightness/jquery-ui-1.7.2.custom.css" rel="stylesheet" />
	<script type="text/javascript" src="../../../js/jquery-1.8.0.min.js"></script>
	<script type="text/javascript" src="../../../js/jquery-ui-1.8.23.custom.min.js"></script>
	<style type="text/css">
	body{overflow:hidden;}
	a{text-decoration:none;}
	a:hover{text-decoration:underline;}
	ul.connectedSortable, ul.connectedSortable2 
	{
	    list-style-type: none; 
	    margin: 0; 
	    padding: 0; 
	    background: #eee; 
	    padding: 3px; 
	    width: 118px;
	    }
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
        text-overflow:ellipsis;
	    }
	 #maindiv  
	{
	    margin-left:auto; 
	    margin-right:auto; 
	    padding: 2px; 
		overflow:auto;
	}
	#tablesel,#querysel,#tablefld,#queryfld
	{
	    min-height:25px;
        height:100%;
        _height:25px;
    }
        
    table.frametbl
    {
	    table-layout:fixed;
	    border-collapse: collapse;
        margin-left:auto;
	    margin-right:auto;
	    font-size: 12px;
	    line-height:20px;
        width:520x;
	    border:#808080 1px solid;
	    color:#393939;
	    background:#FAF8F8;
    }
    table.frametbl td{padding:2px;border:#808080 1px solid;}
    table.frametbl th{padding:2px;border:#808080 1px solid;}
	</style>
    <script type="text/javascript">
        function getOpenerValue(ctlName) {
            return window.parent.opener.document.getElementById(ctlName).value;
        }
        $(function () {

            if (getOpenerValue("stylecode") == "033") {
                var arr = getOpenerValue("styletxt").split("|");
                if (arr.length > 1) {
                    $("#chkSingleSel").attr("checked", arr[0] == "2");
                }
            }

            $("#tablefld, #tablesel").sortable({
                connectWith: '.connectedSortable'
			, placeholder: 'ui-state-highlight'
			, dropOnEmpty: true
            }).disableSelection();

            $("#queryfld, #querysel").sortable({
                connectWith: '.connectedSortable2'
			, placeholder: 'ui-state-highlight'
			, dropOnEmpty: true
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
        var singleSel = $("#chkSingleSel").attr("checked") == "checked"?"2":"1";
        parent.frameElement.lhgDG.curWin.styleCallBack("<%=key %>:弹出用户树:" + singleSel + "|" + tablelist.join(",") + "|" + querylist.join(","));
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
        <li><input id="chkSingleSel" type="checkbox" value="2" /><label for="chkSingleSel">&nbsp;单用户选择&nbsp;</label></li>
        <li><a href="javascript:" onclick="save();">确认</a></li>
        <li><a href="javascript:" onclick="parent.frameElement.lhgDG.cancel();">关闭</a> </li>
        </ul>
    </div>
    <div id="maindiv">
    <table align="center" class="frametbl">
    <tr>
        <th align="center" height="30">表单字段</th>
        <th align="center">选中字段</th>
        <th align="center">选中字段</th>
        <th align="center">查询字段</th>
    </tr>
    <tr>
        <td width="25%" valign="top" align="center">
            <ul id="tablefld" class="connectedSortable">
                <%=fieldlist1 %>
            </ul>
        </td>
        <td width="25%"  valign="top" align="center">
            <ul id="tablesel" class="connectedSortable">
            <%=fieldlist1in %>
            </ul>
        </td>
        <td width="25%" valign="top" align="center">
            <ul id="querysel" class="connectedSortable2">
            <%=fieldlist2in %>
            </ul>
        </td>
        <td  valign="top" align="center">
            <ul id="queryfld" class="connectedSortable2">
                <%=fieldlist2 %>
            </ul>
        </td>
    </tr></table>
    </div>
    </form>
</body>

</html>
