<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MeetingInfo.aspx.cs" Inherits="EIS.Web.WorkAsp.Meeting.MeetingInfo" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1">
<title>查看记录</title>
    <meta http-equiv="Pragma" content="no-cache" />
    <link type="text/css" rel="stylesheet" href="../../Css/jquery-ui/lightness/jquery-ui-1.7.2.custom.css" />
    <link type="text/css" rel="stylesheet" href="../../Css/appInput.css" />
    <script type="text/javascript" src="../../js/jquery-1.7.min.js"></script>
    <script type="text/javascript" src="../../DatePicker/WdatePicker.js"></script>
    <script type="text/javascript" src="../../js/ui.core.js"></script>
    <script type="text/javascript" src="../../js/ui.tabs.js"></script>
    <style type="text/css" media="print"> 
	    .NoPrint{display:none;} 
	    .PageNext{page-break-after: always;} 
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
</head>
<body>
    <form id="form1" runat="server">
    <OBJECT id="WebBrowser" classid="CLSID:8856F961-340A-11D0-A96B-00C04FD705A2" height="0" width="0" ></OBJECT> 
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
    <br />
    <table class='normaltbl'   border="1"   align="center">
        <caption align="middle" height="25" >会议信息</caption>
          <tr>
            <td  width="18%">会议名称</td>
            <td  width="32%"><%=hyInfo["HyName"].ToString()%></td>
            <td  width="18%">会议地点</td>
            <td ><%=hyInfo["HyAddr"].ToString()%></td>
          </tr>
          <tr>
            <td>联系人</td>
            <td><%=hyInfo["HyJbr"].ToString()%></td>
            <td>联系方式</td>
            <td><%=hyInfo["JbrTel"].ToString()%></td>
          </tr>
          <tr>
            <td>会议开始时间</td>
            <td><%=hyInfo["StartTime"].ToString()%></td>
            <td>会议结束时间</td>
            <td><%=hyInfo["EndTime"].ToString()%></td>
 
          </tr>
            <tr>
            <td>参会人员</td>
            <td colspan="3">
            <%=hyInfo["HyRy"].ToString()%>
            </td>
 
          </tr>
            <tr>
            <td>会议内容</td>
            <td colspan="3">
            <%=hyInfo["Note"].ToString()%>
            </td>
 
          </tr>
         </table>
    </div>

    </form>
</body>
</html>
