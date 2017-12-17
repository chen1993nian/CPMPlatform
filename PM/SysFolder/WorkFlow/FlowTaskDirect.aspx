<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FlowTaskDirect.aspx.cs" Inherits="EIS.Web.SysFolder.WorkFlow.FlowTaskDirect" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>任务直送</title>
    <link rel="stylesheet" type="text/css" href="../../Css/wfStyle.css" />
    <style type="text/css">
    input[type=submit],button{
        padding:3px 8px;
        }
    .tip{border:dotted 1px orange;background:#F9FB91;text-align:left;padding:5px;margin-top:10px;}
    td{height:26px;}
    </style>
</head>
<body>
    
    <form id="form1" runat="server">
    <div id="maindiv" style="width:500px;">
        <br />
        <h5 style="padding:5px;text-align:left;">请选择直送步骤：</h5>
        <table  class="normaltbl" border="1" align="center" style="width:100%">
            <colgroup>
                <col width="50" align="center"/>
                <col width="180" align="left"/>
                <col />
            </colgroup>
            <tr>
            <th >序号</th>
            <th >步骤名称</th>
            <th>处理人</th>
            </tr>
            <%=sbHtml %>
        </table>
        <div class="<%=tipClass%>">
        提示：该步骤还有【<%=empNameList%>】未处理完成，点击【确定】未处理任务会被取消
        </div>
        <br/>
        <asp:TextBox Rows="3" TextMode="MultiLine"  CssClass="TextBoxInArea" ID="txtReason" runat="server"></asp:TextBox>
        <br />
        <asp:Button ID="Button1" runat="server" Text="确 定" onclick="Button1_Click" />
                    &nbsp;
            <button value="关 闭" onclick="javascript:window.close();" >关 闭</button>
    </div>
    </form>
</body>
</html>
