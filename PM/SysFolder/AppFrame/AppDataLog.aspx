<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AppDataLog.aspx.cs" Inherits="EIS.Web.SysFolder.AppFrame.AppDataLog" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>查看记录</title>
    <meta http-equiv="Pragma" content="no-cache"/>
    <link type="text/css" rel="stylesheet" href="../../Css/jquery-ui/lightness/jquery-ui-1.7.2.custom.css"  />
    <link type="text/css" rel="stylesheet" href="../../Css/appInput.css" />
    <link type="text/css" rel="stylesheet" href="../../Editor/skins/default.css" />
    <script type="text/javascript" src="../../js/jquery-1.7.min.js"></script>
    <script type="text/javascript" src="../../DatePicker/WdatePicker.js"></script>
    <script type="text/javascript" src="../../js/ui.core.js"></script>
    <script type="text/javascript" src="../../js/ui.tabs.js"></script>
    <script type="text/javascript" src="../../Editor/kindeditor.js"></script>
    <style type="text/css" media="print"> 
	    .NoPrint{display:none;} 
	    .PageNext{page-break-after: always;} 
    </style>
    <style type="text/css">
        .normaltbl>tbody>tr> td{height:25px;}
        .logtbl{width:700px;table-layout:fixed;border-collapse:collapse;margin-bottom:5px;}
        .logtbl th{padding:2px 1px 2px 5px;border:1px dotted gray;text-align:left;background:#e2e9ea none;color:#3a6ea5;}
        .logtbl td{padding:2px 1px 2px 5px;border:1px dotted gray;}
        .titlefld{width:160px;}
        .subtitle{line-height:30px;}
        .tblname,.autoid{color:Blue;font-weight:bold;height:20px;display:inline-block;padding:2px;}
        .logtbl td img{width:200px;height:160px;}
    </style> 
    <script type="text/javascript">
        jQuery(function () {
            $(window).resize(function () {
                $("#maindiv").height($(document.body).height() - 75);
            });
            $("#maindiv").height($(document.body).height() - 75);
        });
    </script>
</head>
<body scroll="auto">
    <form id="form1" runat="server">
    <!-- 工具栏 -->
    <div class="menubar NoPrint">
        <div class="topnav">
            <ul>
                <li><a href="javascript:" onclick="window.close();" >关闭</a> </li>
            </ul>
        </div>
    </div>
    
    <div id="maindiv" style="background:white;">
    <br />
        <table class="normaltbl" style="width:800px;" border="0" cellpadding="0" cellspacing="0">
        <caption>
            业务数据修改日志
        </caption>
            <tr>
                <td width="80">业务名称</td>
                <td><%=LogModel.AppName %></td>
                <td width="80">业务ID</td>
                <td><%=LogModel.AppID %></td>
            </tr>
            <tr>
                <td>操作人</td>
                <td><%=LogModel.UserName %></td>
                <td>操作时间</td>
                <td><%=LogModel.LogTime %></td>
            </tr>
            <tr>
                <td>操作类型</td>
                <td><%=LogModel.LogType %></td>
                <td>客户端IP</td>
                <td><%=LogModel.ComputeIP%></td>
            </tr>
            <tr>
                <td>信息摘要</td>
                <td colspan="3"><%=LogModel.Message %></td>
            </tr>
            <tr>
                <td>修改内容</td>
                <td colspan="3" style="padding:5px;background-color:White;">

                        <%=sbLog %>
                    
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
