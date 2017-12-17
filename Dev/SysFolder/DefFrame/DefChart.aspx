<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DefChart.aspx.cs" Inherits="EIS.Studio.SysFolder.DefFrame.DefChart" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>业务编辑</title>
    <meta http-equiv="Pragma" content="no-cache"/>
    <meta http-equiv="content-type" content="text/html; charset=utf-8"/>
    <link rel="stylesheet" type="text/css" href="../../css/AppStyle.css"/>
    <link rel="stylesheet" type="text/css" href="../../css/DefStyle.css"/>
    <script type="text/javascript" src="../../js/jquery-1.4.2.min.js"></script>
    <script type="text/javascript" src="../../js/jquery.validate.min.js"></script>
    <script type="text/javascript" src="../../js/jquery.notice.js"></script>
    <script type="text/javascript" src="../../js/FusionCharts.js"></script>
    
    <script type="text/javascript">
        $(document).ready(function () {
            $(".Read").attr("readonly", true);
            var validator = $("#form1").validate({
                rules: {
                }
            });
            $("#LinkButton1").click(function () {
                return $("#form1").valid();
            });
            var w = $("#chartdiv").width();
            var h = $("#chartdiv").height();
            if ("<%=w%>" != "")
            w=<%=w%>;
            var h = $("#chartdiv").height();
            if ("<%=h%>" != "")
                h=<%=h%>;
            var chart = new FusionCharts("../../<%=flashPath %>", "ChartId", w, h, "0", "0");
            chart.setDataURL("../../GetChartData.ashx?chartCode=<%=tblName %>");
            chart.render("chartdiv");
        });


    </script>
    <style type="text/css">
        td{padding:5px;}
    </style>
</head>
<body >
    <form id="form1" runat="server">
    <div class="menubar">
        <div class="topnav">
            <ul>
                <li>
                    <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click">保存</asp:LinkButton></li>
                <li><a href="javascript:" onclick="window.close();">关闭</a> </li>
            </ul>
        </div>
    </div>
    <div class="maindiv">
         <table style="width: 860px;" border="0" align="center">
            <tr>
                <td width="80">图表标题：
                </td>
                <td width="160">
                    <asp:TextBox ID="txtTitle" Width="150px" runat="server"  CssClass="TextBoxInChar"></asp:TextBox></td>

                <td width="80">图表类型：
                </td>
                <td>&nbsp;<asp:DropDownList ID="ddlType" runat="server" >
                    </asp:DropDownList>
                    </td>

                <td width="80"> X轴列：</td>
                <td>
                <asp:DropDownList ID="ddlxAxis" runat="server" >
                    <asp:ListItem Text="" />
                    </asp:DropDownList>
                    
                    </td>
                <td width="80">
                    Y轴列：
                </td>
                <td>
                <asp:DropDownList ID="ddlyAxis" runat="server" >
                    <asp:ListItem Text="" />
                    </asp:DropDownList>                
                </td>
            </tr>
            <tr>
                <td> 宽度：</td>
                <td>
                    <asp:TextBox ID="txtWidth" Width="150px"  runat="server"  CssClass="TextBoxInChar"></asp:TextBox>
                    </td>

                <td>
                    高度：
                </td>
                <td><asp:TextBox ID="txtHeight" Width="150px"   runat="server" CssClass="TextBoxInChar"></asp:TextBox> </td>

                <td> 显示值：</td>
                <td>
                    <asp:CheckBox ID="chkDisp" Text="显示" runat="server" />

                    </td>

                <td>
                </td>
                <td></td>
            </tr>
        </table>
        <div id="chartdiv" style="height:400px;background:white;overflow:auto;border:2px solid green;">
        </div>
    </div>
    </form>
</body>
</html>
