<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HelpIndex.aspx.cs" Inherits="CEIM.WebHelp.Help.HelpIndex" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>帮助查询</title>
    <script src="../Js/jquery-1.7.js"></script>
    <script type="text/javascript" src="../js/layer/layer.js"></script>
    <script type="text/javascript" src="../js/lhgdialog.min.js"></script>
    <script>
        function app_table_def()
        {
            app_help("HelpTableDefine01.aspx", "业务表单定义帮助");
        }
        function app_query_def() {
            app_help("HelpQueryDefine01.aspx", "查询定义帮助");
        }

        function app_showHelp(urlStr, titleStr) {
            if ((titleStr == undefined) || (titleStr == "")) titleStr = "帮助";
            var win = new $.dialog({
                id: 'HelpWin'
                , cover: true
                , maxBtn: true
                , minBtn: true
                , btnBar: true
                , lockScroll: false
                , title: titleStr
                , autoSize: false
                , width: 1150
                , height: 600
                , resize: true
                , bgcolor: 'black'
                , iconTitle: false
                , page: urlStr
            });
            win.ShowDialog();
        }


        $(document).ready(function () {
            $("#btnCreateNewTable").click(app_table_def);
            $("#btnCreateQuery").click(app_query_def);
            setTimeout(app_query_def(), 3000);
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <input type="button" value="业务表单定义帮助" id="btnCreateNewTable" />
        <input type="button" value="查询定义帮助" id="btnCreateQuery" />
        <asp:HiddenField ID="hidHelpIsLocal" runat="server" Value="1" />
        <asp:HiddenField ID="hidHelpWeb" runat="server" Value="" />
    </div>
    </form>
</body>
</html>
