<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DefBizEdit2.aspx.cs" Inherits="EIS.Studio.SysFolder.DefFrame.DefBizEdit2" ValidateRequest="false" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>业务子表编辑</title>
    <meta http-equiv="Pragma" content="no-cache"/>
    <meta http-equiv="content-type" content="text/html; charset=utf-8"/>
    <link rel="stylesheet" type="text/css" href="../../css/AppStyle.css"/>
    <link rel="stylesheet" type="text/css" href="../../css/DefStyle.css"/>
    <script type="text/javascript" src="../../js/jquery-1.4.2.min.js"></script>
    <script type="text/javascript" src="../../js/jquery.validate.min.js"></script>
    <script type="text/javascript" src="../../js/jquery.notice.js"></script>
    
    <script type="text/javascript">
        $(document).ready(function () {
            $(".Read").attr("readonly", true);
            var validator = $("#form1").validate({
                rules: {
                    tb_tablename: "required",
                    tb_tablenamecn: "required"
                }
            });
            $("#LinkButton1").click(function () {
                return $("#form1").valid();
            });
        });
        function toDefine(t) {
            try {
                $.noticeAdd({ text: '保存成功！', stay: false });
                frameElement.lhgDG.curWin.app_query();
                frameElement.lhgDG.cancel();
            }
            catch (e) {
                if (window.opener)
                    window.opener.app_query();
                window.close();
            }
        }
    </script>
    <style type="text/css">
        td{padding:5px;}
    </style>
</head>
<body >
    <form id="form1" runat="server">
    <div class="menubar">
        <div class="topnav">
            <ul>
                <li>
                    <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click">保存</asp:LinkButton></li>
                <li><a href="javascript:" onclick="toDefine();">关闭</a> </li>
            </ul>
        </div>
    </div>
    <div class="maindiv">
        <table style="width: 520px; height: 107px" border="0" align="center">
            <tr>
                <td width="100">业务名称：
                </td>
                <td >
                    <asp:TextBox ID="tb_tablename" Width="250px" runat="server"  CssClass="TextBoxInChar"></asp:TextBox></td>
            </tr>
            <tr>
                <td width="80">中文名称：
                </td>
                <td>
                    <asp:TextBox ID="tb_tablenamecn" Width="250px"   runat="server" CssClass="TextBoxInChar"></asp:TextBox></td>
            </tr>
            <tr>
                <td>父表名称：
                </td>
                <td>
                    <asp:TextBox ID="tb_parent" Width="250px"  runat="server"  CssClass="TextBoxInChar Read"></asp:TextBox>
                </td>

            </tr>
            <tr>
                <td>编辑风格：</td>
                <td>
                    <div style="background-color:White;width:250px;border:1px solid #ccc;padding:3px 0px;">
                    <asp:RadioButtonList ID="rblEditMode" runat="server" 
                        RepeatDirection="Horizontal">
                        <asp:ListItem Selected="True" Value="0"> 默认(嵌入式)</asp:ListItem>
                        <asp:ListItem Value="1"> 弹出式</asp:ListItem>
                    </asp:RadioButtonList>
                    </div>
                </td>
            </tr>
            <tr>
                <td>默认行数：</td>
                <td>
                    <asp:TextBox ID="tb_InitRows" Width="250px"  runat="server" Value="1" CssClass="TextBoxInChar"></asp:TextBox>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
