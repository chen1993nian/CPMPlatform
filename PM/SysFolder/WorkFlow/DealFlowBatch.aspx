<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DealFlowBatch.aspx.cs" Inherits="EIS.Web.SysFolder.WorkFlow.DealFlowBatch" %>

<!DOCTYPE html>


<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>批量审批</title>
    <link rel="stylesheet" type="text/css" href="../../Css/wfStyle.css" />
        <style type="text/css">
		input[type=submit],input[type=button]{
           padding:3px;
        }
        td{   
            padding:5px;
        }
    </style>
	<script type="text/javascript">
	    function success(t) {
	        if (window.opener)
	            window.opener.app_query();
	        window.close();
	    }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="maindiv" style="width:500px;text-align:left;">
        <table width="100%">
            <tbody>
				<tr><td>请输入审批意见：</td></tr>
                <tr>
                    <td>
                        <asp:TextBox ID="txtNewTitle" Rows="3" Width="500"  TextMode="MultiLine" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>
                        <div class="tip">
                            提示：点击批准（通过）意见默认为同意，点击退回必须输入审批意见！
                        </div>
                    </td>
                </tr>
            </tbody>
            <tfoot>
                <tr>
                    <td align="center">
                        <asp:Button ID="btnSubmit" runat="server" Text=" 批 准 " onclick="btnSubmit_Click" />&nbsp;
                        <asp:Button ID="btnBack" runat="server" Text=" 退 回 " onclick="btnBack_Click" />&nbsp;
                        <input style="padding:3px;height:30px;" type="button" value=" 关 闭 " onclick="javascript: window.close();" />
                    </td>
                </tr>
            </tfoot>
        </table>
    </div>
    </form>
</body>
</html>
