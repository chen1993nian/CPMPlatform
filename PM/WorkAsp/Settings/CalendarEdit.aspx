<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CalendarEdit.aspx.cs" Inherits="EIS.Web.WorkAsp.Settings.CalendarEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>工作日历信息</title>
    <link href="../../Css/calendar.css" rel="stylesheet" type="text/css" />
    <script src="../../Js/jquery-1.4.2.min.js" type="text/javascript"></script>
    <style type="text/css">
        td{height:26px;}
        input[type=button],input[type=submit]
        {
	        font-size:12px;
	        padding:3px;
        }
    </style>
        <script type="text/javascript">
            function afterSuccess() {
                window.opener.UpdateCalendar($("#txtName").val());
                window.close();
            }
            $(function () {

            });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="maindiv" style="padding:30px;">
        <table width="460" align="center">
            <tr>
                <td width="100">工作日历名称：</td>
                <td>
                    <asp:TextBox ID="txtName" Width="200" runat="server" />
                </td>
            </tr>
            <tr>
                <td>每天工作小时数：</td>
                <td>
                    <asp:TextBox ID="txtHours" Width="200" runat="server" />
                </td>
            </tr>
            <tr>
                <td>时区信息：</td>
                <td>
                    <asp:DropDownList Width="300" ID="selTimeZone" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>排序：</td>
                <td>
                    <asp:TextBox ID="txtOrder" Width="200" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <%=MsgInfo%>                
                </td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <br />
                    <asp:Button ID="Button1" runat="server" Text="  确 定  " onclick="Button1_Click" />&nbsp;
                    <input type="button" value="  取 消  " onclick="window.close();"/>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>

