<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SelectUser.aspx.cs" Inherits="EIS.Web.SysFolder.WorkFlow.SelectUser" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>选择处理人</title>
    <meta http-equiv="Pragma" content="no-cache"/>
    <link rel="stylesheet" type="text/css" href="../../Css/AppStyle.css" />
    <link rel="stylesheet" href="../../Editor/skins/default.css" />
    <script type="text/javascript" src="../../js/jquery-1.4.2.min.js"></script>
    <script type="text/javascript" src="../../DatePicker/WdatePicker.js"></script>
    <script type="text/javascript" src="../../js/ui.core.js"></script>
    <script type="text/javascript" src="../../js/ui.tabs.js"></script>

    <style type="text/css">
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <!-- 工具栏 -->
    <div class="menubar">
        <div class="topnav">
            <ul>
                <li><a class="linkbtn btnsave"  href="javascript:" onclick="sysSave();" >保存</a></li>
                <li><asp:LinkButton CssClass="linkbtn btnsubmit" ID="LinkButton1" runat="server" 
                        onclick="LinkButton1_Click">提交</asp:LinkButton></li>
                <li><a class="linkbtn"  href="javascript:" onclick="window.close();" >关闭</a> </li>
            </ul>
        </div>
    </div>
    
    <div id="maindiv">
    选择处理人
    </div>
    </form>
</body>
</html>
