<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditCalendar.aspx.cs" Inherits="EIS.WebBase.WorkAsp.Settings.EditCalendar" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>工作日信息</title>
    <link href="../../Css/calendar.css" rel="stylesheet" type="text/css" />
    <script src="../../Js/jquery-1.7.min.js" type="text/javascript"></script>
    <script src="../../DatePicker/WdatePicker.js" type="text/javascript"></script>
    <style type="text/css">
        td{height:26px;}
        input[type=button],input[type=submit]
        {
	        font-size:12px;
	        padding:3px;
        }
        .Wdate{width:80px;}
        a{text-decoration:none;color:Blue;}
        
    </style>
        <script type="text/javascript">
            function afterSuccess() {
                window.opener.loadEvents();
                window.close();
            }
            $(function () {
                dtClick();
                $("#RadioButtonList1").click(dtClick);
                $(".Wdate").focus(function () {
                    WdatePicker({ dateFmt: 'HH:mm' });
                }).click(function () {
                    WdatePicker({ dateFmt: 'HH:mm' });
                });

            });
            function dtClick() {
                var isWorkDay = $("#RadioButtonList1_0").prop("checked");
                var isJR = $("#RadioButtonList1_2").prop("checked");
                if (isWorkDay) {
                    $("#trsw").show();
                    $("#trxw").show();
                    $("#trjr").hide();
                }
                else if (isJR) {
                    $("#trjr").show();
                    $("#trsw").hide();
                    $("#trxw").hide();
                }
                else {
                    $("#trjr").hide();
                    $("#trsw").hide();
                    $("#trxw").hide();
                }
            }
            var times = "<%=times %>".split("|");
            function defone() {
                $("#TextBox1").val(times[0]);
                $("#TextBox2").val(times[1]);
            }
            function deftwo() {
                $("#TextBox3").val(times[2]);
                $("#TextBox4").val(times[3]);
            }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="maindiv" style="padding:30px;">
        <%=MsgInfo%>
        <table width="360" align="center">
            <tr>
                <td>设置日期：</td>
                <td>
                    <asp:TextBox ID="EditDate" runat="server" ReadOnly="true" />
                </td>
            </tr>
            <tr>
                <td>类 型：</td>
                <td align="left">
                    <asp:RadioButtonList ID="RadioButtonList1" runat="server" 
                        RepeatDirection="Horizontal">
                        <asp:ListItem Value="工作日">工作日</asp:ListItem>
                        <asp:ListItem Value="周末">周末</asp:ListItem>
                        <asp:ListItem Value="节假日">节假日</asp:ListItem>
                        <asp:ListItem Value="调休假">调休假</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr id="trjr">
                <td>节日名称：</td>
                <td>
                    <asp:TextBox ID="HolidayName" runat="server" />
                </td>
            </tr>
            <tr id="trsw">
                <td>上 午：</td>
                <td>
                    <asp:TextBox CssClass="Wdate" ID="TextBox1" runat="server"></asp:TextBox>&nbsp;至&nbsp;
                    <asp:TextBox CssClass="Wdate" ID="TextBox2" runat="server"></asp:TextBox>&nbsp;
                    <a href="javascript:defone();">默认</a>
                </td>
            </tr>
            <tr id="trxw">
                <td>下 午：</td>
                <td>
                    <asp:TextBox CssClass="Wdate" ID="TextBox3" runat="server"></asp:TextBox>&nbsp;至&nbsp;
                    <asp:TextBox CssClass="Wdate" ID="TextBox4" runat="server"></asp:TextBox>&nbsp;
                    <a href="javascript:deftwo();">默认</a>
                </td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <br />
                    <asp:Button ID="Button1" runat="server" Text="  确 定  " onclick="Button1_Click" />&nbsp;
                    <input type="button" value="  取 消  " onclick="window.close();"/>
                    <asp:Button ID="Button2" Visible="false" runat="server" Text="计 算" onclick="Button2_Click" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
