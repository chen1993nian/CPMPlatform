<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FileList.aspx.cs" Inherits="EIS.Web.SysFolder.Common.FileList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>文件下载</title>
    <meta http-equiv="Pragma" content="no-cache"/>
    <link rel="stylesheet" type="text/css" href="../../Css/AppStyle.css" />
    <link rel="stylesheet" type="text/css" href="../../Css/ymPrompt_green/ymPrompt.css" />
    <script language="javascript" type="text/javascript" src="../../js/jquery-1.3.2.min.js"></script>
    <script type="text/javascript" src="../../js/ymPrompt.js"></script>
    <script type="text/javascript">
        jQuery(function (jQuery) {
            ymPrompt.setDefaultCfg({ showShadow: true })
            $("#btnAdd").click(function () {

                ymPrompt.win({ message: 'FileUpload.aspx?para=<%=Request["para"] %>', width: 500, height: 300, title: '上传附件', handler: reload, maxBtn: true, minBtn: true, iframe: true })
            });
        });
        function reload() {
            window.location.reload();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <!-- 工具栏 -->
    <div class="menubar">
        <div class="topnav">
            <ul>
                <li><a href="javascript:" id="btnAdd" hidefocus="true">添加</a></li>
                <li><a href="javascript:" onclick="window.close();" hidefocus="true">关闭</a> </li>
            </ul>
        </div>
    </div>
    <div id="maindiv">
    <br />
    <table class="defaultstyle"   border="1"   align="center">
    <tr align="center">
        <th width="50px">序号</th>
        <th>文件名</th>
        <th width="100px">大小</th>
        <th width="100px">创建日期</th>
        <th width="80px">创建人</th>
        <th width="50px">下载</th>
    </tr>
    <%=fileList %>
    </table>
    </div>
    </form>
</body>
</html>
