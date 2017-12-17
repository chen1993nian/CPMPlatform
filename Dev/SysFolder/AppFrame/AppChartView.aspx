<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AppChart.aspx.cs" Inherits="EIS.WebBase.SysFolder.AppFrame.AppChartView" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>业务编辑</title>
    <meta http-equiv="Pragma" content="no-cache"/>
    <meta http-equiv="content-type" content="text/html; charset=utf-8"/>
    <link rel="stylesheet" type="text/css" href="../../css/AppStyle.css"/>
    <script type="text/javascript" src="../../js/jquery-1.3.2.min.js"></script>
    <script type="text/javascript" src="../../js/FusionCharts.js"></script>
    
    <script type="text/javascript">
        $(document).ready(function () {
            $(".Read").attr("readonly", true);
       
            var w = $("#chartdiv").width();
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
</head>
<body >
    <form id="form1" runat="server">
	<br/>
    <div class="maindiv" style="padding:10px;text-align:center;">
        <div id="chartdiv" style="padding:10px;height:400px;overflow:auto;border:0px solid #aaa;">
        </div>
    </div>
    </form>
</body>
</html>
