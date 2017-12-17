<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ImportDataTJJGZB2DemoStart1.aspx.cs" Inherits="EIS.Web.ImportDataTJJGZB.WorkAsp.RelationTree.ImportDataTJJGZB2DemoStart1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script language="javascript" type="text/javascript">
        function OpenJgzbDataView() {
            var url = "ImportDataTJJGZB2DemoField3.aspx?TableName=" + document.getElementById("TextBox2").value + "&dbname=tjjgzbdb20140106";
            window.open(url, "ViewDataWin2");
        }

        function OpenDataView() {
            var url = "ImportDataTJJGZB2DemoField3.aspx?TableName=" + document.getElementById("TextBox1").value;
            window.open(url, "ViewDataWin1");
        }
    </script>

</head>
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
                <td><asp:Label ID="Label1" runat="server" Text="准备就绪，点击开始导入！" ForeColor="Red"></asp:Label></td>
            </tr>
            <tr>
                <td><asp:Button ID="Button1" runat="server" Text="开始导入" onclick="Button1_Click" /></td>
            </tr>
            <tr>
                <td><input id="Button2" type="button" value="预览建工总包数据" onclick="OpenJgzbDataView();" /></td>
            </tr>
            <tr>
                <td><input id="Button3" type="button" value="预览导入结果" onclick="OpenDataView();" /></td>
            </tr>
        </table>        
    </div>
    <asp:HiddenField ID="hidDbName" runat="server" Value="tjjgzbdb20140106" />
    </form>
</body>
</html>
