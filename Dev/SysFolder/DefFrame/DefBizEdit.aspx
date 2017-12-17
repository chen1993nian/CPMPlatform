<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DefBizEdit.aspx.cs" Inherits="EIS.Studio.SysFolder.DefFrame.DefBizEdit" ValidateRequest="false" %>


<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>业务编辑</title>
    <meta http-equiv="Pragma" content="no-cache"/>
    <meta http-equiv="content-type" content="text/html; charset=utf-8"/>
    <link rel="stylesheet" type="text/css" href="../../css/AppStyle.css"/>
    <script type="text/javascript" src="../../js/jquery-1.4.2.min.js"></script>
    <script type="text/javascript" src="../../js/jquery.validate.min.js"></script>
    <script type="text/javascript" src="../../js/jquery.notice.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            jQuery.validator.addMethod("tableName", function (value, element) {
                var tblRegex = new RegExp("^[a-zA-Z_]+[a-zA-Z0-9_]*$", "g");
                if (tblRegex.test(value)) {
                    return true;
                }
                return false;

            }, "不符合要求");
            $(".Read").attr("readonly", true);
            var validator = $("#form1").validate({
                rules: {
                    tb_tablename: { required: true, tableName: true },
                    tb_tablenamecn: "required",
                    tb_createdate: {
                        required: true,
                        dateISO: true
                    }
                }
            });
            $("#LinkButton1").click(function () {
                return $("#form1").valid();
            });

        });
        function toDefine(t) {
            window.open("DefTableFrame.aspx?tblname=" + t, "_self");
        }
    </script>
    <style type="text/css">
        #maindiv{text-align:left;}
        td{padding:4px;}
        #tb_tablename{ime-mode:disabled;}
        #rblLog td{padding:0px;}
        #tb_sqlcmd,#tb_DetailSQL,#txt_DataLog{
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
    <div class="maindiv" style="padding:15px 15px 15px 25px;">
        <table style="width: 800px; " border="0" align="left">
            <tr>
                <td width="80">业务名称：
                </td>
                <td width="320">
                    <asp:TextBox ID="tb_tablename" Width="180px" runat="server"  CssClass="TextBoxInChar"></asp:TextBox></td>

                <td width="80">中文名称：
                </td>
                <td>
                    <asp:TextBox ID="tb_tablenamecn" Width="180px"   runat="server" CssClass="TextBoxInChar"></asp:TextBox></td>
            </tr>
            <tr>
                <td>
                    审批状态：
                </td>
                <td>
                    <asp:RadioButtonList ID="rblShow" runat="server" 
                        RepeatDirection="Horizontal">
                        <asp:ListItem Selected="True" Value="1">显示</asp:ListItem>
                        <asp:ListItem Value="0">隐藏</asp:ListItem>
                    </asp:RadioButtonList>
                 </td>
                 <td>
                    归档状态：
                </td>
                <td>
                    <asp:RadioButtonList ID="rblArchive" runat="server" 
                        RepeatDirection="Horizontal">
                        <asp:ListItem Value="1">显示</asp:ListItem>
                        <asp:ListItem Selected="True" Value="0">隐藏</asp:ListItem>
                    </asp:RadioButtonList>
                </td>


            </tr>
            <tr>
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
                    <td>
                    删除模式：
                    </td>
                    <td>
                        <asp:RadioButtonList ID="rblDel" runat="server" 
                            RepeatDirection="Horizontal">
                            <asp:ListItem Selected="True" Value="0">物理删除</asp:ListItem>
                            <asp:ListItem Value="1">逻辑删除</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
            </tr>
            <tr>
                <td>
                    日志级别：
                </td>
                <td>
                    <asp:RadioButtonList ID="rblLog" runat="server" 
                        RepeatDirection="Horizontal">
                        <asp:ListItem Selected="True" Value="0">关闭</asp:ListItem>
                        <asp:ListItem Value="1">简单</asp:ListItem>
                        <asp:ListItem Value="2">详细</asp:ListItem>
                    </asp:RadioButtonList>
                    </td>
                <td>表单宽度：</td>
                <td>
                    <asp:TextBox ID="tb_Width" Width="120px"   runat="server" CssClass="TextBoxInChar"></asp:TextBox>
                    &nbsp;px&nbsp;或&nbsp;%                    
                </td>
            </tr>
            <tr>
                <td>日志模板：</td>
                <td colspan="3">
                    <asp:TextBox ID="txt_DataLog" runat="server"  CssClass="TextBoxInArea" Rows="2" Width="660px" TextMode="MultiLine"></asp:TextBox>
                    <div class="tip" style="margin:0px;clear:both;width:660px;">
                    <span style="color:red">模板说明：</span>
                    &nbsp;使用 {HtName} 来表示主表记录中字段名称为 HtName 的值。
                    </div>
                
                </td>
            </tr>
            <tr>
            <td>列表语句：
            <br /></td>
            <td colspan="3">
            <div class="codetd">
            <asp:TextBox ID="tb_sqlcmd" runat="server"  CssClass="TextBoxInArea" Rows="10" Width="660px" TextMode="MultiLine"></asp:TextBox>
            </div>
            </td>
            </tr>
            <tr>
                <td>单条语句：
                <br /></td>
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
