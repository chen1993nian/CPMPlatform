<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AppWorkFlowInfo.aspx.cs" Inherits="EIS.WebBase.SysFolder.AppFrame.AppWorkFlowInfo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>查看流程相关信息</title>
    <meta http-equiv="Pragma" content="no-cache"/>
    <link type="text/css" rel="stylesheet" href="../../Css/kandytabs.css"  />
    <link type="text/css" rel="stylesheet" href="../../Css/wfstyle.css" />
    <script type="text/javascript" src="../../js/jquery-1.7.min.js"></script>
    <script type="text/javascript" src="../../js/kandytabs.pack.js"></script>
    <%=customScript%>
    <style type="text/css" media="print"> 
	    .NoPrint{display:none;} 
	    .PageNext{page-break-after: always;}
	    #maindiv{height:100%;overflow:visible}
    </style> 
    <style type="text/css"> 
	    dl{width:770px;text-align:left;}
	    dt{cursor:hand;}
	    .normaltbl>tbody>tr> td{padding:3px;}
	    .refItem{line-height:25px;height:25px;padding-left:3px;}
	    .refLink{text-decoration:none;}
	    .refLink:hover{text-decoration:underline;color:red;}
    </style> 
    <script type="text/javascript">
        jQuery(function () {
            $(window).resize(function () {
                $("#maindiv").height($(document.body).height() - 60);
            });
            $("#maindiv").height($(document.body).height() - 60);
            $("dl").KandyTabs({ trigger: "click" });
            $("#wfpic").parent().css("overflow", "auto");
        });
        function appPrint() {
            document.getElementById("WebBrowser").ExecWB(7, 1);
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <object id="WebBrowser" classid="CLSID:8856F961-340A-11D0-A96B-00C04FD705A2" height="0" width="0" ></object> 
    <!-- 工具栏 -->
    <div class="menubar NoPrint">
        <div class="topnav">
            <span style="right:10px;display:inline;float:right;position:fixed;line-height:30px;top:0px;">
                <a class="linkbtn" href="javascript:" onclick="appPrint();" >打印</a>
                <em class="split">|</em>
                <a class="linkbtn" href="javascript:" onclick="window.close();" >关闭</a> 
            </span>
        </div>
    </div>
    
    <div id="maindiv" style="background:white;">
	    <div style="Text-align:left;width:770px;margin-left:auto;margin-right:auto;">
	        <table width="100%" border="0" align="center">
	        <tr><td height="30"><h4 style="float:left;color:#4677bf;font-size:11pt;"><span>任务名称：</span><%=curInstance.InstanceName%></h4></td></tr>
	        <tr><td style="color:Gray;font-size:12px;">流程名称：<%=workflowName%>&nbsp;V<%=workflowVer%> &nbsp;&nbsp;&nbsp;&nbsp;流程编号：<%=workflowCode%></td></tr>
	        <tr><td style="color:Gray;font-size:12px;">发起人：<%=curInstance.EmployeeName%>（<%=curInstance.CompanyName%>） 
	        &nbsp;&nbsp;&nbsp;&nbsp;发起时间：<%=curInstance._CreateTime%>
	        &nbsp;&nbsp;&nbsp;&nbsp;审批状态：<%=curInstance.InstanceState%>
	        </td></tr>
	    </table>

    </div>
    <dl style="margin-left:auto;margin-right:auto;">
        <dt>表单信息</dt>
        <dd>
            <%=tblHTML%>    
        </dd>
        <dt>流程图</dt>
        <dd>
        <wf:InstanceImg id="FlowImg" runat="server"></wf:InstanceImg>
        </dd>
        <dt>参考流程</dt>
        <dd>
        <%=GetInstanceRefers()%> 
        </dd>
        <dt>维护日志</dt>
        <dd>
        <wf:InstanceLog id="FlowLog" runat="server"></wf:InstanceLog>
        </dd>
    </dl>
    <div class="wfdealinfo">
    <wf:UserDealInfo id="UserDealInfo" runat="server"></wf:UserDealInfo>
    </div>


    </div>
    </form>
</body>
</html>
