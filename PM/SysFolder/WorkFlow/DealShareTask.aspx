<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DealShareTask.aspx.cs" Inherits="EIS.Web.SysFolder.WorkFlow.DealShareTask" %>

<!DOCTYPE html>


<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>任务处理提示</title>
    <style type="text/css">
        input{padding:3px;}
        .hidden{display:none;}
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <table width="80%" align="center">
        <tr>
            <td>    
            <div class="TaskName"><h4 style="color:#4677bf">任务名称：<%=curInstance.InstanceName%></h4></div>
            <br />
            </td>
        </tr>
        <tr>
            <td><%=DealInfo %></td>
        </tr>
        <tr>
            <td><br />
                <input type="button" value=" 返 回 " onclick="window.history.back();"/>
                <asp:Button ID="btnApplyTask"  runat="server" Text="申请任务" 
                    onclick="btnApplyTask_Click" />
                <asp:Button ID="btnShareTask"  runat="server" Text="退还任务" 
                    onclick="btnShareTask_Click" />
                <input type="button" value=" 关 闭 " onclick="window.close();"/>
            </td>
        </tr>
    </table>

    </form>
</body>
</html>
