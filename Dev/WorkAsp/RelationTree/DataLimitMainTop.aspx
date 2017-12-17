<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DataLimitMainTop.aspx.cs" Inherits="Studio.JZY.WorkAsp.DataLimit.WorkAsp.RelationTree.DataLimitMainTop" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../style.css" type="text/css" rel="stylesheet"/>
</head>
<body>

<form id="form1" runat="server">
    <div>
        <table style="width:100%;">
        <tr>
            <td>
                <a href="RelationMain.aspx?RelationID=BB4D6FF4-9711-4357-A521-AF6B6613F38A" target="DataLimitContents">人员数据授权</a>
                <a href="RelationMain.aspx?RelationID=C2661DED-2FBB-47CF-8269-F63D5E3C5D58" target="DataLimitContents">角色数据授权</a>
                <a href="RelationMain.aspx?RelationID=1E5E6448-21D7-4D3F-A5CC-4E4B306FD687" target="DataLimitContents">岗位数据授权</a>
                <a href="RelationMain.aspx?RelationID=4FBE5F3B-949C-4D34-9BB9-E7F22E333564" target="DataLimitContents">部门数据授权</a>
            </td>
            <td align="right">
                <asp:LinkButton ID="LinkButton1" runat="server" onclick="LinkButton1_Click" ToolTip="默认设置所有部门拥有查看本公司所有数据的权限">快速部门数据授权</asp:LinkButton>
            </td>
            </tr>
        </table>
    </div>
</form>
</body>
</html>
