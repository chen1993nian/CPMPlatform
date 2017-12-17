<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChangePass.aspx.cs" Inherits="EIS.Web.ChangePass" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>修改密码</title>
    <meta http-equiv="Pragma" content="no-cache"/>
    <link rel="stylesheet" type="text/css" href="Css/appStyle.css" />
    <script language="javascript" type="text/javascript" src="js/jquery-1.4.2.min.js"></script>
    <script type="text/javascript" src="js/jquery.validate.min.js"></script>
    <style type="text/css">
        body{overflow:hidden;background:white;}
        input[type="button"],input[type=submit]
        {
            padding:3px 18px 3px 18px;
	        margin:1px 1px 1px 0px;
	        width: auto;
	        line-height:16px;
	        height:28px;
            color:#333;
            overflow:visible; 
        }
        table.defaultstyle{background:#eeeeee;border:1px solid #a0a0a0;}
        table.defaultstyle td{border:1px solid #a0a0a0;}
        table.defaultstyle caption{font-size:11pt;height:29px;border:1px solid #a0a0a0;line-height:29px;}
    </style>
    <script type="text/javascript">
        $(document).ready(function () {
            $(".Read").attr("readonly", true);
            var validator = $("#form1").validate({
                rules: {
                    txtOldPass: "required",
                    newPass: {
                        required: true
                    },
                    newPass2: {
                        required: true,
                        equalTo: "#newPass"
                    }
                }
            });
            $("#Button1").click(function () {
                return $("#form1").valid();
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="maindiv"><br /><br />
    <table class='defaultstyle' style="width:600px"  border="1"   align="center">
        <caption>修改密码</caption>
        <tbody>
          <tr>
            <td  width="30%">旧密码</td>
            <td  >
                <asp:TextBox ID="txtOldPass" TextMode="Password" Width="300px" CssClass="TextBoxInChar" runat="server"></asp:TextBox></td>
          </tr>
          <tr>
            <td  width="18%">新密码</td>
            <td ><asp:TextBox ID="newPass" TextMode="Password" Width="300px" CssClass="TextBoxInChar" runat="server"></asp:TextBox></td>
          </tr>
            <tr>
            <td  width="18%">确认密码</td>
            <td ><asp:TextBox ID="newPass2" TextMode="Password" Width="300px" CssClass="TextBoxInChar" runat="server"></asp:TextBox></td>
          </tr>
        </tbody>
    </table>
    <table align="center" style="width:600px">
    <tr>
        <td align="center">
        <br />
        <asp:Button ID="Button1" runat="server" Text="保 存" onclick="Button1_Click" />&nbsp;&nbsp;
        <input type="button" value="关 闭"  onclick="window.close();"/>
        </td>
    </tr>
    </table>
</div>
    </form>
</body>
</html>
