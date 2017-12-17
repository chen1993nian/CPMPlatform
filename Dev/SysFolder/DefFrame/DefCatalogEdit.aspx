<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DefCatalogEdit.aspx.cs" Inherits="EIS.Studio.SysFolder.DefFrame.DefCatalogEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>分类编辑</title>
    <meta http-equiv="Pragma" content="no-cache"/>
    <link rel="stylesheet" type="text/css" href="../../Css/DefStyle.css" />
    <script type="text/javascript" src="../../js/jquery.js"></script>
    <script type="text/javascript" src="../../js/jquery-1.4.2.min.js"></script>
    <script type="text/javascript" src="../../js/lhgdialog.min.js?s=default,chrome,"></script>
    <style type="text/css">
        input[type=submit]{padding:3px;}
        .btnHelp {
            padding:3px;
            width: 68px;
            background-color: #9cf78b;
            border: 1px solid gray;
        }
    </style>
    <script type="text/javascript">
        $(document).ready(function () {

            $(".btnHelp").click(function () {
                var url = $(this).attr("data-helpUrl");
                app_showHelp(url, "业务分类及字典定义帮助");
            });

        });


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

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table>
    <tr>
        <td height="25">分类编码：</td>
        <td>
            <asp:TextBox ID="TextBox1" runat="server" CssClass="textbox"></asp:TextBox></td>
    </tr>
        <tr>
        <td height="25">分类名称：</td>
        <td>
            <asp:TextBox ID="TextBox2" runat="server" CssClass="textbox"></asp:TextBox></td>
    </tr>
        <tr>
        <td height="25">同级排序：</td>
        <td>
            <asp:TextBox ID="TextBox3" runat="server" CssClass="textbox"></asp:TextBox></td>
    </tr>
        <tr>
        <td colspan="2" align="center">
        <br />
            <asp:Button ID="Button1" runat="server" Text="增加" Width="60px" OnClick="Button1_Click" />
            <asp:Button ID="Button2" runat="server" Text="保存" Width="60px" OnClick="Button2_Click" />
            <asp:Button ID="Button3" runat="server" Text="删除" Width="60px" OnClick="Button3_Click" />
            <input type="button" value="帮助" id="Button5" class="btnHelp" data-helpUrl="Help/HelpTabelCatalog.aspx" />
            </td>
    </tr>
    </table>
        <input id="openflag" type="hidden" runat="server" value="change" />
    </div>
    </form>
</body>
</html>
