<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DocLimitTop.aspx.cs" Inherits="Studio.JZY.Doc.Limit.DocLimitTop" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>功能节点授权</title>
	<script type="text/javascript" src="../../js/jquery-1.4.2.min.js"></script>
    <style type="text/css">
        html{         
            margin:0px;
            padding:0px;}
        body{
             height:38px;
             margin:0px;
             padding:0px;
             background:#f9fafe;
             font-size:12px;
             border-bottom:2px solid #ddd;}
        a{text-decoration:none;line-height:30px;}
        a:hover{background-color:#eee;color:red;}
    </style>
    <script type="text/javascript">
        jQuery(function () {
            var funId = "<%=funId %>";
            //window.parent.frames["limitmain"].open("FunLimitDept.aspx?funId=<%=funId %>", "limitmain");
            jQuery("input[name=limitType]").click(function () {
                var t = getLimitType();
                if (funId == "") {
                    alert("请先选择功能节点");
                    return;
                }
                if (t == "1") {
                    window.open("FunLimitDept.aspx?funId=" + funId + "&funName=", "limitmain");
                }
                else if (t == "2") {
                    window.open("FunLimitPosition.aspx?funId=" + funId + "&funName=", "limitmain");
                }
                else if (t == "3") {
                    window.open("FunLimitEmployee.aspx?funId=" + funId + "&funName=", "limitmain");
                }
                else if (t == "4") {
                    window.open("FunLimitRole.aspx?funId=" + funId + "&funName=", "limitmain");
                }
                else if (t == "5") {
                    window.open("FunLimitExclude.aspx?funId=" + funId + "&funName=", "limitmain");
                }
                else if (t == "0") {
                    window.open("FunLimitQuery.aspx?funId=" + funId + "&funName=", "limitmain");
                }
            });
            $("#btnReturn").click(function () {
                window.open("../DocList.aspx?folderId=<%=funId %>", "_parent");

            });
        });
        function getLimitType() {
            return jQuery("input[name=limitType]:checked").val();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <table width="100%" border="0">
            <tr>
                <td style="width:400px;height:30px;">&nbsp;&nbsp;&nbsp;&nbsp;
                    <input type="radio" value="0" checked="checked" name="limitType" id="limitType0"/><label for="limitType0" style="color:red;">权限查询</label>                
                    <input type="radio" value="1" name="limitType" id="limitType1"/><label for="limitType1">部门权限</label>
                    <input type="radio" value="2" name="limitType" id="limitType2"/><label for="limitType2">岗位权限</label>
                    <input type="radio" value="3" name="limitType" id="limitType3"/><label for="limitType3">员工权限</label>
                    <input type="radio" value="4" name="limitType" id="limitType4"/><label for="limitType4">角色权限</label>
                </td>
                <td align="left" style="color:Blue;font-family:Cordia New 宋体;">
                    授权对象：<%=funName %>
                </td>
                <td width="100">
                    <input id="btnReturn" type="button" value="返回列表" style="height:26px;padding:3px;border-width:0px;background-color:lightblue;"/>
                </td>
            </tr>
        </table>
        <input type="hidden" id="funId" value="" />
    </form>
</body>
</html>
