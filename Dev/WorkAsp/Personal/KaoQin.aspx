<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KaoQin.aspx.cs" Inherits="EIS.Web.WorkAsp.Personal.KaoQin" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>考勤登记</title>
    <link type="text/css" rel="stylesheet" href="../../Css/appInput.css" />
    <script type="text/javascript" src="../../js/jquery-1.3.2.min.js"></script>
    <script type="text/javascript" src="../../DatePicker/WdatePicker.js"></script>
    <script type="text/javascript" src="../../js/jquery.validate.min.js"></script>
    <script type="text/javascript" src="../../js/jquery.notice.js"></script>
    <style type="text/css">
        input{padding:3px;display:block;margin:3px;}
        input[type=button]{padding:3px;}
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div  id="maindiv">
        <br />
        <br />
        <br />
        <table>
            <tr>
                <td>
                    <asp:Button ID="Button1" runat="server" Text="上午上班登记" />
                    <asp:Button ID="Button2" runat="server" Text="上午下班登记" />
                    <asp:Button ID="Button3" runat="server" Text="下午上班登记" />
                    <asp:Button ID="Button4" runat="server" Text="下午下班登记" />
                </td>
            </tr>
            <tr>
                <td>
                    <input type="submit" value="  外出登记  " />
                    <input type="submit" value="  请假登记  " />
                    <input type="submit" value="  出差登记  " />
                    <input type="submit" value="  加班登记  " />
                    <input type="submit" value="  考勤统计  " />
                </td>

            </tr>
        </table>
    </div>
    </form>
</body>
</html>
