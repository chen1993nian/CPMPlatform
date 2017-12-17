<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MyFolderEdit.aspx.cs" Inherits="EIS.Web.Doc.MyFolderEdit" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>目录信息编辑</title>
    <meta http-equiv="Pragma" content="no-cache"/>
    <link rel="stylesheet" type="text/css" href="../Css/DefStyle.css" />
    <style type="text/css">
        input[type=submit],input[type=button]{padding:0px 10px;height:28px;}
        .maindiv{text-align:left;}
    </style>
     <script type="text/javascript">
         function afterSave() {
             if (window.opener) {
                 window.opener.changeName("<%=folderId %>", document.getElementById("TextBox2").value);
                window.opener.app_query();
            }
            window.close();
        }
        function afterNew() {
            if (window.opener) {
                window.opener.newFolder("<%=folderId %>", document.getElementById("TextBox2").value);
                window.opener.app_query();
            }
            window.close();
        }
    </script>
</head>
<body class="bgbody">
    <form id="form1" runat="server">
    <div class="maindiv">
    <table>
        <tr>
        <td height="30" width="80">文件夹名称：</td>
        <td>
            <asp:TextBox ID="TextBox2" runat="server" Width="200" CssClass="textbox"></asp:TextBox>
            <br />
            <br />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                ControlToValidate="TextBox2" Display="Dynamic" ErrorMessage="文件夹名称不能为空" 
                ForeColor="Red"></asp:RequiredFieldValidator>
            </td>
    </tr>
        <tr>
        <td height="30">同级排序：</td>
        <td>
            <asp:TextBox ID="TextBox3" runat="server" Width="200" CssClass="textbox"></asp:TextBox></td>
    </tr>
        <tr>
        <td>
        </td>
        <td height="30" align="left">
        <br />
            <asp:Button AccessKey="s" ID="Button2" runat="server" Text="保存" OnClick="Button2_Click" />
            &nbsp;
            <input accesskey="c" type="button" value="关闭" onclick="window.close();"/>
            </td>
    </tr>
    </table>
        <input id="openflag" type="hidden" runat="server" value="" />
    </div>
    </form>
</body>
</html>
