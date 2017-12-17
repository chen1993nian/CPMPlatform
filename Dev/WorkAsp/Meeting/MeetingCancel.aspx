<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MeetingCancel.aspx.cs" Inherits="EIS.Web.WorkAsp.Meeting.MeetingCancel" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>取消会议</title>
    <link rel="stylesheet" type="text/css" href="../../Css/wfStyle.css" />
    <style type="text/css">
		input[type=submit],input[type=button]{padding:3px;}
        textarea{padding:3px;line-height:150%;}
        td{padding:5px;}
    </style>

    <script type="text/javascript">
        function success(t) {
            alert("会议取消成功!");
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
        <tr><td height="30"><h4 style="float:left;color:#4677bf"><span>会议名称：</span><%=_HyName%></h4></td></tr>
        <tr><td style="color:Gray;font-size:12px;">申请人：<%=_EmployeeName%>
        &nbsp;&nbsp;&nbsp;&nbsp;申请时间：<%=_CreateTime%>
        &nbsp;&nbsp;&nbsp;&nbsp;审批状态：<%=_InstanceState%>
        </td></tr>
    </table>    
        <table width="100%">
            <tbody>
                <tr>
                    <td>请在下面输入取消会议的原因：&nbsp;&nbsp;
                        <asp:CheckBox ID="CheckBox1" Checked="true" runat="server" Text="发短信通知申请人" />
                    </td>
                </tr>
                <tr>
                    
                    <td>
                        <asp:TextBox ID="txtReason" Rows="3" Width="536px"  TextMode="MultiLine" 
                            runat="server"></asp:TextBox>
                        <br />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                            ControlToValidate="txtReason" ErrorMessage="取消原因不能为空" Font-Bold="True" 
                            Font-Size="9pt" ForeColor="Red"></asp:RequiredFieldValidator>
						<div class="tip">
                            提示：请先确认后再提交，会议取消之后不能再恢复，您的操作将会记录在日志中
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
