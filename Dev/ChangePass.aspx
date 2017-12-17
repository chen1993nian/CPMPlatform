<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChangePass.aspx.cs" Inherits="EIS.Studio.ChangePass" %>

<!DOCTYPE html>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>修改密码</title>
    <meta http-equiv="Pragma" content="no-cache"/>
    <link rel="stylesheet" type="text/css" href="Css/password.css" />
    <script language="javascript" type="text/javascript" src="js/jquery-1.4.2.min.js"></script>
    <script type="text/javascript" src="js/jquery.validate.min.js"></script>
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
        function success() {
            //alert("密码修改成功！");
            frameElement.lhgDG.cancel();

        }
        function winClose() {
            //window.close();
            frameElement.lhgDG.cancel();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="maindiv"><br /><br /><br />
    <table class='defaultstyle' style="width:600px"  border="1"   align="center">
        <caption>修改密码</caption>
        <tbody>
          <tr>
            <td  width="160">旧密码</td>
            <td  >
                <asp:TextBox ID="txtOldPass" TextMode="Password" Width="300px" CssClass="TextBoxInChar" runat="server"></asp:TextBox></td>
          </tr>
          <tr>
            <td  >新密码</td>
            <td ><asp:TextBox ID="newPass" TextMode="Password" Width="300px" CssClass="TextBoxInChar" runat="server"></asp:TextBox></td>
          </tr>
            <tr>
            <td  >确认密码</td>
            <td ><asp:TextBox ID="newPass2" TextMode="Password" Width="300px" CssClass="TextBoxInChar" runat="server"></asp:TextBox></td>
          </tr>
        </tbody>
    </table>
    <table align="center" style="width:600px">
    <tr>
        <td align="center"><br />
        <asp:Button ID="Button1" runat="server" Text="保 存" onclick="Button1_Click" />&nbsp;&nbsp;
        <input type="button" value="关 闭"  onclick="winClose();"/>
        </td>
    </tr>
    </table>
</div>
    </form>
</body>
</html>

