<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DealFlowAfter.aspx.cs" Inherits="EIS.Web.SysFolder.WorkFlow.DealFlowAfter" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>任务处理提示</title>
    <link href="../../Css/common.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        input{padding:3px;}
    </style>
    <script type="text/javascript">
        function afterClose() {
            if (window.opener != null) {
                if (typeof (window.opener["app_query"]) != "undefined") {
                    window.opener["app_query"]();
                }
                else {
                    window.opener.location.reload();
                }
            }
            window.close();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="Rpage" class="Rpage-main">
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
                            <%=DealInfo%>
                        </td>
                    </tr>
                    <tr>
                        <td style="padding:10px 20px;">
                            <input type="button" value=" 关 闭 " onclick="afterClose();"/>
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
