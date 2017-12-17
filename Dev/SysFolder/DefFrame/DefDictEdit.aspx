<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DefDictEdit.aspx.cs" Inherits="EIS.Studio.SysFolder.DefFrame.DefDictEdit" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>字典编辑</title>
    <link rel="stylesheet" type="text/css" href="../../css/AppStyle.css">
    <script type="text/javascript" src="../../js/jquery-1.4.2.min.js"></script>
    <script type="text/javascript" src="../../js/jquery.validate.min.js"></script>
    
    <script language="javascript">
        $(document).ready(function () {
            $(".Read").attr("readonly", true);
            var validator = $("#form1").validate({
                rules: {
                    tb_tablename: "required",
                }
            });
            $("#LinkButton1").click(function () {
                return $("#form1").valid();
            });
        });
    </script>
</head>
<body  scroll=auto>
    <form id="form1" runat="server">
    <DIV class="menubar">
    <UL>
    <LI>
        <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click">保存</asp:LinkButton></LI>
    <LI><A href="javascript:" onclick="window.close();">关闭</A> </LI>
    </UL></DIV>
    <div class="maindiv">
    <br />
        <table  border=0 align=center>
            <tr>
                <td width="80">字典名称：
                </td>
                <td width="160">
                    <asp:TextBox ID="tb_tablename" Width="150px" runat="server"  CssClass="TextBoxInChar"></asp:TextBox></td>

            </tr>
        </table>
    </div>
    </form>
</body>
</html>
