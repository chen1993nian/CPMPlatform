<%@ Page Language="C#" Async="true" AutoEventWireup="true" CodeBehind="FlowTaskRollBack.aspx.cs" Inherits="EIS.WebBase.SysFolder.WorkFlow.FlowTaskRollBack" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>任务回退</title>
    <link rel="stylesheet" type="text/css" href="../../Css/wfStyle.css" />
        <style type="text/css">
        input[type=submit],button{
            padding:3px;
            }
       
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div id="maindiv" style="width:500px;">
        <br />
        <div style="font-weight:bold;padding:5px;text-align:left;">请选择回退步骤：</div>
        <table  class="normaltbl" border="1" align="center" style="width:100%">
            <tr>
            <th width="40">序号</th>
            <th width="100">步骤名称</th>
            <th>处理人</th>
            </tr>
            <%=sbHtml %>
        </table>
		<div class="<%=tipClass%>">
        提示：该步骤还有【<%=empNameList%>】未处理完成，点击【提交】未处理任务会被取消
        </div>
        <div style="text-align:left;">
        <div style="font-weight:bold;padding:5px;text-align:left;">回退原因：</div>
           <asp:TextBox Rows="3" TextMode="MultiLine"  CssClass="TextBoxInArea"  Width="98%" ID="txtReason" runat="server"></asp:TextBox>
           <div style="padding:5px;">
            <asp:Button ID="Button1" CssClass="clear" runat="server" Text="提 交" onclick="Button1_Click" />
            &nbsp;
            <button value="关 闭" onclick="javascript:window.close();" >关 闭</button>
            </div>
        </div>

    </div>
    </form>
</body>
</html>
