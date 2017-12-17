<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProjectDataLimitMainTop.aspx.cs" Inherits="EIS.WorkAsp.DataLimit.ProjectDataLimitMainTop" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../../style.css" type="text/css" rel="stylesheet"/>
</head>
<body>

<form id="form1" runat="server">
    <div>
        <table style="width:100%;">
        <tr>
            <td>
                <a href="RelationMain.aspx?RelationID=E1A26D93-64B8-4451-A946-79E8136AAE82" target="DataLimitContents">人员数据授权</a>
                <a href="RelationMain.aspx?RelationID=4CDBBB53-6747-489E-850F-4071FAD9742A" target="DataLimitContents">角色数据授权</a>
                <a href="RelationMain.aspx?RelationID=FD3AB4BD-4526-4E07-9232-DD21B3F53714" target="DataLimitContents">岗位数据授权</a>
                <a href="RelationMain.aspx?RelationID=291F4DE7-7A10-47A0-81CA-EFF13DB724F3" target="DataLimitContents">部门数据授权</a>
            </td>
            <td align="right">
                <asp:LinkButton ID="LinkButton1" runat="server" onclick="LinkButton1_Click" ToolTip="默认设置所有部门拥有查看本公司所有项目数据的权限">快速部门数据授权</asp:LinkButton>
            </td>
            </tr>
        </table>
    </div>
</form>
</body>
</html>
