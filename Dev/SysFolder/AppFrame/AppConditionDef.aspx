<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AppConditionDef.aspx.cs" Inherits="EIS.WebBase.SysFolder.AppFrame.AppConditionDef" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>查询条件设置</title>
    <meta http-equiv="Pragma" content="no-cache"/>
    <link rel="stylesheet" type="text/css" href="../../Css/DefStyle.css"/>
    <link type="text/css" href="../../Css/jquery-ui/lightness/jquery-ui-1.7.2.custom.css" rel="stylesheet" />
	<script type="text/javascript" src="../../js/jquery-1.8.0.min.js"></script>
	<script type="text/javascript" src="../../js/jquery-ui-1.8.23.custom.min.js"></script>
	<style type="text/css">
	#sortable1, #sortable2 {list-style-type: none; margin: 0; padding: 0; float: left; margin-right: 10px; background: #eee; padding: 2px; width: 143px;}
	#sortable1 li, #sortable2 li { margin: 0 5px 2px 5px; padding: 4px; font-size: 1.0em; width: 140px;text-align:center}
	table.frametbl
    {
	    border-collapse: collapse;
        margin-left:auto;
	    margin-right:auto;
	    font-size: 12px;
	    line-height:20px;

	    border:#808080 1px solid;
	    color:#393939;
	    background:#FAF8F8;
    }
    table.frametbl td{padding:2px;border:#808080 1px solid;}
	#maindiv{overflow:auto;}
	</style>
		<script type="text/javascript">
		    $(function () {
		        $(window).resize(function () {
		            $("#maindiv").height($(document.body).height() - 45);
		        });
		        $("#maindiv").height($(document.body).height() - 45);
		        $("#sortable1, #sortable2").sortable({
		            connectWith: '.connectedSortable'
                    , placeholder: 'ui-state-highlight'
                    , dropOnEmpty: true
		        }).disableSelection();

		    });
		    function save() {
		        var querylist = $('#sortable2').sortable('toArray');
		        if (querylist.length == 0) return false;
		        var ret = EIS.WebBase.SysFolder.AppFrame.AppConditionDef.saveQuery(querylist, "<%=tblName %>", "<%=sindex %>");
        if (ret.error) {
            alert("保存出错：" + ret.error.Message);
        }
        else {
            alert("保存成功！");
            window.opener.location.reload();
            window.close();

        }
    }
	</script>
</head>
<body style="padding:0px;margin:0px;height:100%;">
    <form id="form1" runat="server">
        <div class="menubar">
		<div class="topnav">
            <span style="right:10px;display:inline;float:right;position:fixed;line-height:30px;top:0px;">
                <a class='linkbtn' href="javascript:" onclick="save();" >保存</a>
				<em class="split">|</em>
                <a class='linkbtn' href="javascript:" onclick="window.close();" >关闭</a>
            </span>
        </div>
    </div>
    <div id="maindiv">
    <table align="center" class="frametbl" width="94%" border="1">
    <tr><th align="center" height="30">列表字段</th><th align="center">查询字段</th></tr>
    <tr><td width="50%" valign=top align="center">


<ul id="sortable1" class="connectedSortable">
    <%=fieldlist1 %>
</ul>
</td>
<td  valign=top align="center">
<ul id="sortable2" class="connectedSortable">
    <%=fieldlist2 %>
</ul>
</td>
</tr></table>
</div>
    </form>
</body>

</html>
