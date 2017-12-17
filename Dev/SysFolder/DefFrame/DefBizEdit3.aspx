<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DefBizEdit3.aspx.cs" Inherits="EIS.Studio.SysFolder.DefFrame.DefBizEdit3"  ValidateRequest="false"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>查询业务编辑</title>
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
            window.open("DefQueryFrame.aspx?tblname=" + t, "_self");
        }
    </script>
    <style type="text/css">
        td{padding:5px;}
        #tb_sqlcmd,#tb_DetailSQL{
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
        <table style="width: 800px;" border="0" align="left">
            <tr>
                <td width="80">业务名称：
                </td>
                <td >
                    <asp:TextBox ID="tb_tablename" Width="200px" runat="server"  CssClass="TextBoxInChar"></asp:TextBox></td>

                <td width="80">中文名称：
                </td>
                <td>
                    <asp:TextBox ID="tb_tablenamecn" Width="200px"   runat="server" CssClass="TextBoxInChar"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>数据源：</td>
                <td>
                    <asp:DropDownList ID="DropDownList1" runat="server">
                    </asp:DropDownList>
                </td>
                    <td>
                    排序字段：
                    </td>
                    <td>
                    <asp:TextBox ID="txtOrder" Width="120px"   runat="server" CssClass="TextBoxInChar"></asp:TextBox>
                    &nbsp;
                        <asp:DropDownList ID="ddlOrder" runat="server">
                        <asp:ListItem Value="" Text=""></asp:ListItem>
                        <asp:ListItem Selected="True" Value="asc" Text="升序"></asp:ListItem>
                        <asp:ListItem Value="desc" Text="降序"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
            </tr>
            <tr>
                <td>列表语句：
                </td>
                <td colspan="3">
                <div class="codetd">

                <asp:TextBox ID="tb_sqlcmd" runat="server"  CssClass="TextBoxInArea"  Width="660px" Rows="10" TextMode="MultiLine"></asp:TextBox>
                </div>
                <br />
                <span style="color:Red;font-weight:bold;display:none;">注意：如果该查询用于弹出列表返回数据，则在返回的数据集中必需包含_AutoID列</span>
                </td>
            </tr>
            <tr>
                <td>
                单条语句：
                </td>
                <td colspan="3">
                <div class="codetd">

                <asp:TextBox ID="tb_DetailSQL" runat="server"  CssClass="TextBoxInArea" Rows="6" Width="660px" TextMode="MultiLine"></asp:TextBox>
                </div>
            
                </td>
            </tr>
            <tr>
                <td>每页记录数：
                <br /></td>
                <td colspan="3">
            <div class="codetd">
                <asp:TextBox ID="tb_PageRecCount" runat="server"  CssClass="TextBoxInChar" Rows="6" Width="100px"></asp:TextBox>
            </div>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
