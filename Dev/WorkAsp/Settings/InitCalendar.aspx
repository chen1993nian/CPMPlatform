<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InitCalendar.aspx.cs" Inherits="EIS.WebBase.WorkAsp.Settings.InitCalendar" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>初始化工作日</title>
    <link href="../../Css/calendar.css" rel="stylesheet" type="text/css" />
    <script src="../../Js/jquery-1.4.2.min.js" type="text/javascript"></script>
    <script src="../../DatePicker/WdatePicker.js" type="text/javascript"></script>
    <style type="text/css">
        td{height:26px;}
    input[type=button],input[type=submit]
    {
	    font-size:12px;
	    padding:3px;
    }
    .Wdate{width:80px;}
    </style>
        <script type="text/javascript">
            function afterSuccess() {
                window.opener.location.href = window.opener.location.href;
                window.close();
            }
            $(function () {

                $(".Wdate").focus(function () {
                    WdatePicker({ dateFmt: 'HH:mm' });
                }).click(function () {
                    WdatePicker({ dateFmt: 'HH:mm' });
                });
                ;
            });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="maindiv" style="padding:30px;">
        <%=MsgInfo%>
        <table width="360" align="center">
            <tr>
                <td>年 份：</td>
                <td>
                    <asp:DropDownList ID="DropDownList1" runat="server">
                    </asp:DropDownList>&nbsp;
                    如果存在本年数据，会覆盖掉
                </td>
            </tr>
            <tr>
                <td>上 午：</td>
                <td>
                    <asp:TextBox CssClass="Wdate" ID="TextBox1" runat="server"></asp:TextBox>&nbsp;至&nbsp;
                    <asp:TextBox CssClass="Wdate" ID="TextBox2" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>下 午：</td>
                <td>
                    <asp:TextBox CssClass="Wdate" ID="TextBox3" runat="server"></asp:TextBox>&nbsp;至&nbsp;
                    <asp:TextBox CssClass="Wdate" ID="TextBox4" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>工作日：</td>
                <td align="left">
                    <asp:CheckBoxList ID="CheckBoxList1" runat="server" RepeatColumns="5" 
                        RepeatDirection="Horizontal">
                        <asp:ListItem Value="1">周一</asp:ListItem>
                        <asp:ListItem Value="2">周二</asp:ListItem>
                        <asp:ListItem Value="3">周三</asp:ListItem>
                        <asp:ListItem Value="4">周四</asp:ListItem>
                        <asp:ListItem Value="5">周五</asp:ListItem>
                        <asp:ListItem Value="6">周六</asp:ListItem>
                        <asp:ListItem Value="0">周日</asp:ListItem>
                    </asp:CheckBoxList>
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
