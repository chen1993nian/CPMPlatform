<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AppWorkFlowPrint.aspx.cs" Inherits="EIS.WebBase.SysFolder.AppFrame.AppWorkFlowPrint" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>查看流程相关信息</title>
    <meta http-equiv="Pragma" content="no-cache"/>
    <link type="text/css" rel="stylesheet" href="../../Css/appPrint.css" />
    <script type="text/javascript" src="../../js/jquery-1.4.2.min.js"></script>
    <%=customScript%>
    <style type="text/css" media="print"> 
	    .NoPrint{display:none;} 
	    .PageNext{page-break-after: always;}
	    #maindiv{height:100%;overflow:visible} 
    </style> 
    <script type="text/javascript">
        jQuery(function () {
            $(window).resize(function () {
                $("#maindiv").height($(document.body).height() - 75);
            });
            $("#maindiv").height($(document.body).height() - 75);
        });
        function appPrint() {
            document.getElementById("WebBrowser").ExecWB(7, 1);
        }
    </script>
    <script type="text/javascript" src="LodopFuncs.js"></script>
    <object id="LODOP_OB" classid="clsid:2105C259-1E0C-4534-8141-A753534CB4CA" width="0" height="0"> 
      <embed id="LODOP_EM" type="application/x-print-lodop" width="0" height="0" pluginspage="install_lodop.exe"></embed>
    </object>
</head>
<body>
    <form id="form1" runat="server">
    <object id="WebBrowser" classid="CLSID:8856F961-340A-11D0-A96B-00C04FD705A2" height="0" width="0" ></object> 
    <!-- 工具栏 -->
    <div class="menubar NoPrint">
        <div class="topnav">
            <ul>
                <li><a href="javascript:" onclick="appPrint();" >打印</a></li>
                <li><a href="javascript:" onclick="window.close();" >关闭</a> </li>
            </ul>
        </div>
    </div>
    
    <div id="maindiv" style="background:white;">
        <% =tblHTML%>
    </div>
    </form>
</body>
</html>
