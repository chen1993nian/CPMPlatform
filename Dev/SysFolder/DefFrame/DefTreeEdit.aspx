<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DefTreeEdit.aspx.cs" Inherits="EIS.Studio.SysFolder.DefFrame.DefTreeEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>树型定义</title>
    <meta http-equiv="Pragma" content="no-cache"/>
    <meta http-equiv="content-type" content="text/html; charset=utf-8"/>
    <link rel="stylesheet" type="text/css" href="../../css/AppStyle.css"/>
    <link rel="stylesheet" type="text/css" href="../../css/DefStyle.css"/>
    <script type="text/javascript" src="../../js/jquery-1.4.2.min.js"></script>
    <script type="text/javascript" src="../../js/jquery.validate.min.js"></script>
    <script type="text/javascript" src="../../js/jquery.notice.js"></script>
    <script type="text/javascript" src="../CodeMirror/js/codemirror.js" ></script>
    
    <script type="text/javascript">
        $(document).ready(function () {
            $(".Read").attr("readonly", true);
            var validator = $("#form1").validate({
                rules: {
                    txtTreeName: "required",
                    txtCatCode: "required"
                }
            });
            $("#LinkButton1").click(function () {
                return $("#form1").valid();
            });

            var editor1 = CodeMirror.fromTextArea('txtTreeSQL', {
                parserfile: "parsesql.js",
                stylesheet: "../CodeMirror/css/sqlcolors.css",
                path: "../CodeMirror/js/",
                height: 200,
                tabMode: "shift",
                autoMatchParens: true
            });
        });
        function toDefine(t) {
            window.open("DefQueryFrame.aspx?tblname=" + t, "_self");
        }
    </script>
    <style type="text/css">
        .CodeMirror-wrapping{border: #c0bbb4 1px solid;}
        td{padding:5px;}
       .CodeMirror-line-numbers {
        width: 2.2em;
        color: #aaa;
        background-color: #eee;
        text-align: right;
        padding-right: .3em;
        font-size: 10pt;
        font-family: monospace;
        padding-top: .4em;
      }
    </style>
</head>
<body >
    <form id="form1" runat="server">
    <div class="menubar">
        <div class="topnav">
            <ul>
                <li>
                    <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click">保存</asp:LinkButton></li>
                <li><a href="javascript:" onclick="window.close();">关闭</a> </li>
            </ul>
        </div>
    </div>
    <div class="maindiv">
        <table style="width: 700px;" border="0" align="center">
            <tr>
                <td width="80">树&nbsp;&nbsp;&nbsp;&nbsp;名：</td>
                <td>
                    <asp:TextBox ID="txtTreeName" Width="200"  CssClass="TextBoxInArea"  runat="server"></asp:TextBox>
                </td>
                <td>分类编码：</td>
                <td>
                    <asp:TextBox ID="txtCatCode" Width="200"   CssClass="TextBoxInArea"  runat="server"></asp:TextBox>
                
                </td>
            </tr>
            <tr>
                <td width="80">数据源：
                </td>
                <td>
                    <asp:DropDownList ID="dsList" runat="server">
                    </asp:DropDownList>
                </td>
                <td>数据形式：
                </td>
                <td >
                    <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Text="PID" Value="1" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="WBS" Value="2"></asp:ListItem>
                    </asp:RadioButtonList>
                 </td>


            </tr>
            <tr>
                <td width="80">根节点参数：</td>
                <td>
                    <asp:TextBox ID="rootPara" Width="200"  CssClass="TextBoxInArea"  runat="server"></asp:TextBox>
                </td>
                <td>根节点值：</td>
                <td>
                    <asp:TextBox ID="rootValue" Width="200"   CssClass="TextBoxInArea"  runat="server"></asp:TextBox>
                
                </td>
            </tr>
            <tr>
                <td>树型SQL：
                <br /></td>
                <td colspan="3">
                <asp:TextBox ID="txtTreeSQL" runat="server"  CssClass="TextBoxInArea"  Width="560px" TextMode="MultiLine"></asp:TextBox>
            
                </td>
            </tr>
            <tr>
                <td>JS脚本：
                <br /></td>
                <td colspan="3">
                <asp:TextBox ID="txtJs" runat="server"  CssClass="TextBoxInArea"  Width="560px" TextMode="MultiLine"></asp:TextBox>
            
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
