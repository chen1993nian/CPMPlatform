<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InstanceChart.aspx.cs" Inherits="EIS.WebBase.SysFolder.WorkFlow.InstanceChart" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>流程图</title>
    <style type="text/css">
    Table.NodeTipTable{border: #ff3366 2px solid;border-collapse: separate}
    Table.NodeTipTable TD{background-color: #ffffff;padding-left: 2px;padding-right: 2px;white-space:nowrap;color: red;}
    Table.NodeTipTable TR.head TD{background-color: #ffff99;color: black}
    Table.NodeTipTable TR.processing TD{color: green;}
    </style>
    <script type="text/javascript">
        var g_TipContainer = null;
        function ShowNodeTip(nodeName) {
            if (arrNodeToolTip == null)
                return;
            if (g_TipContainer != null)
                return;

            var tip = null;
            for (var i = 0; i < arrNodeToolTip.length; i++) {
                if (arrNodeToolTip[i][0] == nodeName) {
                    tip = arrNodeToolTip[i][1];
                    break;
                }
            }

            if (tip == null)
                return;

            var x = event.clientX + document.documentElement.scrollLeft - event.offsetX + 32;
            var y = event.clientY + document.documentElement.scrollTop - event.offsetY + 32;

            g_TipContainer = _nodeTipDiv;
            g_TipContainer.style.display = '';
            g_TipContainer.style.pixelLeft = x;
            g_TipContainer.style.pixelTop = y;

            g_TipContainer.innerHTML = tip;
        }

        function HideTip() {
            if (g_TipContainer != null) {
                g_TipContainer.style.display = 'none';
                g_TipContainer = null;
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <table width="90%" border="0" align="center">
        <tr><td height="30"><h4 style="float:left;color:#4677bf"><span>任务名称：</span><%=defModel.InstanceName%></h4></td></tr>
        <tr><td style="color:Gray;font-size:12px;">流程名称：<%=workflowName%></td></tr>
        <tr><td style="color:Gray;font-size:12px;">发起人：<%=defModel.EmployeeName%>（<%=defModel.CompanyName%>） 
        &nbsp;&nbsp;&nbsp;&nbsp;发起时间：<%=defModel._CreateTime%>
        &nbsp;&nbsp;&nbsp;&nbsp;处理状态：<%=defModel.InstanceState%>
        </td></tr>
    </table>
    <div id="_dp" style="border:blue 1px solid;width:<%=MaxWidth%>px;height:<%=MaxHeight%>px;">
        <wf:InstanceImg id="FlowImg" runat="server"></wf:InstanceImg>
    </div>
    <div id="_nodeTipDiv" style="position:absolute; background-color:#cccccc; border-width:1px; border-color:Gray;">
    </div>

    <script type="text/javascript">
        var arrNodeToolTip = null;
    </script>
    </form>
</body>
</html>
