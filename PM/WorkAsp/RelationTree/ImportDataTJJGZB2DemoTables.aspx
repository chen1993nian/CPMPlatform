<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ImportDataTJJGZB2DemoTables.aspx.cs" Inherits="EIS.Web.ImportDataTJJGZB.ImportDataTJJGZB2DemoTables" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" language="javascript">
        function setTable1(s,t) {
            document.getElementById("TextBox1").value = s;
            document.getElementById("TextBox3").value = t;
        }
        function setTable2(s,t) {
            document.getElementById("TextBox2").value = s;
            document.getElementById("TextBox4").value = t;
        }
        function setFieldRelation() {
            if (document.getElementById("TextBox1").value == "") {
                alert("请选择表单");
                return;
            }
            if (document.getElementById("TextBox2").value == "") {
                alert("请选择表单");
                return;
            }
            var para = "t1=" + document.getElementById("TextBox1").value + "&tc1=" + document.getElementById("TextBox3").value;
            para = para + "&t2=" + document.getElementById("TextBox2").value + "&tc2=" + document.getElementById("TextBox4").value;
            var url = "ImportDataTJJGZB2DemoField.aspx?" + para;
            window.open(url,"_blank");
        }
    </script></head>
<body>
    <form id="form1" runat="server">
    <div>
        <table>
            <tr>
                <td>高校模拟系统表单：</td>
            </tr>
            <tr>
                <td><asp:TextBox ID="TextBox1" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td><asp:TextBox ID="TextBox3" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td>&nbsp;&nbsp;</td>
            </tr>
            <tr>
                <td>建工总包系统表单：</td>
            </tr>
            <tr>
                <td><asp:TextBox ID="TextBox2" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td><asp:TextBox ID="TextBox4" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td><input id="Button1" type="button" value="对应表单字段" onclick="setFieldRelation();" /></td>
            </tr>
            <tr>
                <td>&nbsp;&nbsp;</td>
            </tr>
        </table>
        
        
    </div>
    </form>
</body>
</html>
