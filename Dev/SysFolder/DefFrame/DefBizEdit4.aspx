<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DefBizEdit4.aspx.cs" Inherits="EIS.Studio.SysFolder.DefFrame.DefBizEdit4"  ValidateRequest="false"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>子查询业务编辑</title>
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
            window.close();
        }
    </script>
    <style type="text/css">
        td{padding:5px;}
        #tb_sqlcmd{
            padding:5px;
            line-height:150%;
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
        <table style="width: 600px;" border="0" align="center">
            <tr>
                <td width="80">查询编码：
                </td>
                <td >
                    <asp:TextBox ID="tb_tablename" Width="160px" runat="server"  CssClass="TextBoxInChar"></asp:TextBox></td>

                <td width="80">中文名称：
                </td>
                <td>
                    <asp:TextBox ID="tb_tablenamecn" Width="160px"   runat="server" CssClass="TextBoxInChar"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>数据源：</td>
                <td>
                    <asp:DropDownList ID="DropDownList1" runat="server">
                    </asp:DropDownList>
                </td>
                <td>父查询名称：</td>
                <td>
                    <asp:TextBox ID="tb_parent" Width="160px"  runat="server"  CssClass="TextBoxInChar Read"></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td>列表语句：
                </td>
                <td colspan="3">
                <asp:TextBox ID="tb_sqlcmd" runat="server"  CssClass="TextBoxInArea" Rows="10"  Width="465px" TextMode="MultiLine"></asp:TextBox>
                <div style="clear:both;width:465px;float:left;" class="tip">父查询记录ID字段的代替符为@_MainID</div>
                </td>
            </tr>
            <tr>
                <td colspan="4"></td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
