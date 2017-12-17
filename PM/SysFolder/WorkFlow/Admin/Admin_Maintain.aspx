<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Admin_Maintain.aspx.cs" Inherits="EIS.Web.SysFolder.WorkFlow.Admin.Admin_Maintain" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>流程任务维护</title>
    <meta http-equiv="Pragma" content="no-cache"/>
    <link type="text/css" rel="stylesheet" href="../../../Css/kandytabs.css"  />
    <link type="text/css" rel="stylesheet" href="../../../Css/wfstyle.css" />
    <script type="text/javascript" src="../../../js/jquery-1.7.min.js"></script>
    <script type="text/javascript" src="../../../js/kandytabs.pack.js"></script>
    <script type="text/javascript" src="../../../js/lhgdialog.min.js"></script>

    <style type="text/css"> 
	    dl{width:900px;text-align:left;}
	    dd .tabcont{height:500px;}
	    dt{cursor:hand;}
	    .normaltbl>tbody>tr> td{padding:3px;}
    </style> 
    <script type="text/javascript">
        jQuery(function () {
            $(window).resize(function () {
                $("#maindiv").height($(document.body).height() - 60);
            });
            $("#maindiv").height($(document.body).height() - 60);
            $("dl").KandyTabs();
            $("#wfpic").parent().css("overflow", "auto");
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <!-- 工具栏 -->
    <div class="menubar">
        <div class="topnav">
            <span style="color:#999;padding-left:50px;">流程名称：<%=workflowName%>&nbsp;V<%=workflowVer%> &nbsp;&nbsp;&nbsp;&nbsp;流程编号：<%=workflowCode%>
            </span>
            <span style="right:10px;display:inline;float:right;position:fixed;line-height:30px;top:0px;">
                <a class="linkbtn" href="javascript:" onclick="window.location.reload();" >刷新</a> 
                <em class="split">|</em>
                <a class="linkbtn" href="javascript:" onclick="window.close();" >关闭</a> 
            </span>
        </div>
    </div>
    
    <div id="maindiv" style="background:white;">
	    <div style="Text-align:left;width:900px;margin-left:auto;margin-right:auto;">
	        <table width="100%" border="0" align="center">
	        <tr><td height="30"><h4 style="float:left;color:#4677bf;font-size:12pt;"><span>任务名称：</span><%=curInstance.InstanceName%></h4></td></tr>
	        <tr><td style="color:Gray;font-size:12px;height:20px;">发起人：<%=curInstance.EmployeeName%>（<%=curInstance.CompanyName%>） 
	        &nbsp;&nbsp;&nbsp;&nbsp;发起时间：<%=curInstance._CreateTime%>
	        &nbsp;&nbsp;&nbsp;&nbsp;审批状态：<%=curInstance.InstanceState%>
	        </td></tr>
	    </table>

    </div>
    <dl style="margin-left:auto;margin-right:auto;">
        <dt>流程图</dt>
        <dd>
            <wf:InstanceImg id="FlowImg" runat="server"></wf:InstanceImg>
        </dd>
        <dt>审批意见</dt>
        <dd>
            <div class="wfdealinfo">
            <wf:UserDealInfo id="UserDealInfo" runat="server"></wf:UserDealInfo>
            </div>
        </dd>
        <dt>维护日志</dt>
        <dd>
            <wf:InstanceLog id="FlowLog" runat="server"></wf:InstanceLog>
        </dd>
    </dl>
    <div class="tip" style="width:900px;margin-left:auto;margin-right:auto;">
        提示：请点击流程图中的节点编辑详细信息。
    </div>

    </div>
    </form>
</body>
</html>
<script type="text/javascript">
    jQuery(function () {
        $("area").click(function () {
            var actId = $(this).attr("actId");
            var dlg = new jQuery.dialog({
                title: '任务维护', maxBtn: false, page: 'Admin_Maintain_Win.aspx?InstanceId=<%=curInstance.InstanceId%>&nodeId=' + actId
                    , btnBar: false, cover: true, lockScroll: true, width: 800, height: 600, bgcolor: 'black'
            });
            dlg.ShowDialog();
        });
    });
</script>

