<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FlowList.aspx.cs" Inherits="EIS.WebBase.SysFolder.WorkFlow.FlowList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>流程新建</title>
	<link href="../../css/appStyle.css" type="text/css" rel="stylesheet" />
    <script type="text/javascript" src="../../js/jquery-1.7.min.js"></script>

	<style type="text/css">
	    dl{
	        margin-bottom:20px;
	    }
		dt{
			margin-bottom:2px;
			cursor:pointer;
			font-size:11pt;
			padding-left:20px;
			background:url(../../img/Workflow/ico6.gif) no-repeat;
		}
		dd{
		    display:none;
			margin-left:20px;
			padding-left:18px;
			background:url(../../img/Workflow/ag.gif) no-repeat;
		}
		dd a{
			text-decoration:none;
			font-size:13px;
			color:gray;
			line-height:22px;
			padding-bottom:0px;
			border-bottom: solid gray 0px;
		}
		dd a:hover{
			text-decoration:none;
			color:blue;
			padding-bottom:1px;
			border-bottom: solid blue 1px;
		}
		dd span{
			margin-left:5px;
			color:red;
			font-weight:bold;
			font-size:14px;
			cursor:hand;
		}
		dd span.flowchart
		{
			color:Gray;
			font-weight:normal;
			font-size:12px;
			}
		.wrapper
		{
			margin:15px;
			border:solid gray 1px;
			padding:10px;
		}
		body
		{
			background:#fafafa;
			height:auto;
		}
		table{table-layout:fixed;}
	</style>
	<script type="text/javascript">
	    jQuery(function () {
	        jQuery("dt").attr("title", "点击标题查看流程");
	        jQuery("dl").each(function () {
	            var n = $("dd", this).length;
	            if (n > 0) {
	                $("dt", this).append("<span style='color:red;font-size:12px;'>&nbsp;&nbsp;(<b>" + n + "</b>)</span>");
	            }
	        });
	        jQuery("dt").click(function () {
	            var dl = $(this).parent();
	            $("dd", dl).toggle();
	        });
	    });
	    function openlist(tblname) {
	        window.open("../scsexec/UncryptNewEditDelRecord.aspx?tblname=" + tblname + "&SqlQueryCondition=isnull(workflowend,0)=0 and username=[QUOTES]{employeeid}[QUOTES]", "_blank");
	    }
	    function newflow(wfid, tblname) {
	       // window.open('Newflow.aspx?workflowid=' + wfid, '_blank', 'top=0,left=0,width=1000,height=700,resizable=yes');
	        //获得窗口的垂直位置 
	        var iHeight = window.screen.availHeight - 150;
	        //获得窗口的水平位置 
	        var iLeft = (window.screen.availWidth - 10 - 1000) / 2;
	        window.open("Newflow.aspx?workflowid=" + wfid, "_blank", "top=50%,left=" + iLeft, "width=1000,height=" + iHeight, "resizable=yes");
	    }
	    function openchart(wfid) {
	        window.open('FlowChart.aspx?workflowid=' + wfid, '_blank', 'top=0,left=0,width=1000,height=700,resizable=yes');
	    }
	</script>
</head>
<body scroll="auto">
    <form id="form1" runat="server">
    <div class="wrapper">
	<table width="100%">
		<tr>
			<td valign="top"><%=stringBuilder_0 %></td>
			<td valign="top"><%=stringBuilder_1 %></td>
			<td valign="top"><%=stringBuilder_2 %></td>
		</tr>
	</table>
    
    </div>
    </form>
</body>
</html>

