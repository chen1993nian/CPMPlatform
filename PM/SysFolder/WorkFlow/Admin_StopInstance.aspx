<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Admin_StopInstance.aspx.cs" Inherits="EIS.Web.SysFolder.WorkFlow.Admin_StopInstance" %>

<!DOCTYPE html>


<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>终止任务</title>
    <link rel="stylesheet" type="text/css" href="../../Css/wfStyle.css" />
    <style type="text/css">
		input[type=submit],input[type=button]{padding:3px;}
        textarea{padding:3px;line-height:150%;}
        td{padding:5px;}
    </style>

    <script type="text/javascript">
        function success(t) {
            alert("任务成功终止!");
            if (window.opener)
                window.opener.app_query();
            window.close();
        }
    </script>
</head>
<body style="background:white;">
    <form id="form1" runat="server">
    <div id="maindiv" style="width:560px;text-align:left;">
    
     <table width="100%" border="0" align="center">
        <tr><td height="30"><h4 style="float:left;color:#4677bf"><span>任务名称：</span><%=curInstance.InstanceName%></h4></td></tr>
        <tr><td style="color:Gray;font-size:12px;">流程名称：<%=workflowName%></td></tr>
        <tr><td style="color:Gray;font-size:12px;">发起人：<%=curInstance.EmployeeName%>
        &nbsp;&nbsp;&nbsp;&nbsp;发起时间：<%=curInstance._CreateTime%>
        &nbsp;&nbsp;&nbsp;&nbsp;审批状态：<%=curInstance.InstanceState%>
        </td></tr>
    </table>    
        <table width="100%">
            <tbody>
                <tr>
                    <td>终止原因：</td>
                </tr>
                <tr>
                    
                    <td>
                        <asp:TextBox ID="txtReason" Rows="3" Width="536px"  TextMode="MultiLine" 
                            runat="server"></asp:TextBox>
                        <br />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                            ControlToValidate="txtReason" ErrorMessage="终止原因不能为空" Font-Bold="True" 
                            Font-Size="9pt" ForeColor="Red"></asp:RequiredFieldValidator>
						<div class="tip">
                            提示：请先确认后再提交，任务终止之后不能再恢复，您的操作将会记录在日志中
                        </div>
                    </td>
                </tr>
            </tbody>
            <tfoot>
                <tr>
                    <td  align="center">
                        <asp:Button ID="Button1" runat="server" Text=" 提 交 " onclick="Button1_Click" />&nbsp;
                        <input style="padding:3px;height:30px;" type="button" value=" 关 闭 " onclick="javascript: window.close();" />
                    </td>
                </tr>
            </tfoot>
        </table>
    </div>
    </form>
</body>
</html>
