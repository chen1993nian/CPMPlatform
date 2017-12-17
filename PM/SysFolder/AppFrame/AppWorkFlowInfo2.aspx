<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AppWorkFlowInfo2.aspx.cs" Inherits="EIS.Web.SysFolder.AppFrame.AppWorkFlowInfo2" %>


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
    </style> 
    <script type="text/javascript">
        jQuery(function () {
            $(window).resize(function () {
                $("#maindiv").height($(document.body).height() - 60);
            });
            $("#maindiv").height($(document.body).height() - 60);
            $("dl").KandyTabs();

            if ('<%=Request["print"] %>') {
                if ($.browser.msie)
                    document.getElementById("WebBrowser").ExecWB(7, 1);
            }
        });
        function appPrint() {
            if (window.top != window.self) {
                window.open("AppWorkFlowInfo2.aspx?InstanceId=<%=InstanceId %>&print=1", "", "width=900,height=600");
            }
            else {
                document.getElementById("WebBrowser").ExecWB(7, 1);
            }
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
        <tr><td height="30"><h4 style="float:left;color:#4677bf;font-size:12pt;"><span>任务名称：</span><%=curInstance.InstanceName%></h4></td>
		<td rowspan="3" width="95">
			<img style="border:2px solid #eee;display:none;" alt="档案信息二维码" src="../../QRCode.axd" />
		</td>
		</tr>
        <tr><td style="color:Gray;font-size:12px;">流程名称：<%=workflowName%></td></tr>
        <tr><td style="color:Gray;font-size:12px;">发起人：<%=curInstance.EmployeeName%>（<%=curInstance.CompanyName%>） 
        &nbsp;&nbsp;&nbsp;&nbsp;发起时间：<%=curInstance._CreateTime%>
        &nbsp;&nbsp;&nbsp;&nbsp;审批状态：<%=curInstance.InstanceState%>
        </td></tr>
    </table>

    </div>

            <% =tblHTML%>     
            <div class="wfdealinfo">
            <wf:UserDealInfo id="UserDealInfo" runat="server"></wf:UserDealInfo>
            </div>

    </div>
    </form>
</body>
</html>
