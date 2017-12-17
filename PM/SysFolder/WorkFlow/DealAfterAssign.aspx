<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DealAfterAssign.aspx.cs" Inherits="EIS.Web.SysFolder.WorkFlow.DealAfterAssign" %>

<!DOCTYPE html>


<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>任务处理提示</title>
    <link href="../../Css/common.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        button,input{padding:3px;}
        li{line-height:20px;}
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div id="Rpage" class="Rpage-main" style="width:800px;">
        <div id="Rheader">
        </div>
        <div id="Rbody">
            <div class="title">
                <b class="crl"></b><b class="crr"></b>
                <h1>系统提示</h1>
            </div>
            <div class="content">
                <table width="90%" align="center">

                    <tr>
                        <td rowspan="2" width="80"><img alt="提示" src="../../img/icon_64/icon64_info.png" /></td>
                        <td style="padding:20px;line-height:18px;">
                            <div class="TaskName"><h4 style="color:#4677bf"><%=curInstance.InstanceName%></h4></div>
                            该步骤还有【<%=empNameList%>】未处理完成
                            <ul style="list-style-type:decimal;">
                                <li>您可以等待他们处理完成之后再进行提交</li>
                                <li>也可以点击【继续提交】，审批流程将流转到下一步，未处理任务会被取消</li>
                            </ul>
                        </td>
                    </tr>
                    <tr>
                        <td style="padding:10px 20px;">
                        <asp:Button ID="Button1" runat="server" Text="继续提交" onclick="Button1_Click" />
                        <input type="button" value=" 返 回 " onclick="window.history.back();"/>
                        <input type="button" value=" 关 闭 " onclick="window.close();"/>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="bottom">
                <b class="crl"></b><b class="crr"></b>
            </div>
        </div>
    </div>

    </form>
</body>
</html>
