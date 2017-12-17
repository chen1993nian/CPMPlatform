<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FusionChart.aspx.cs" Inherits="Studio.JZY.PlugIn.FusionChart" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>业务编辑</title>
    <meta http-equiv="Pragma" content="no-cache"/>
    <meta http-equiv="content-type" content="text/html; charset=utf-8"/>
    <link rel="stylesheet" type="text/css" href="../css/AppStyle.css"/>
    <script type="text/javascript" src="../js/jquery-1.4.2.min.js"></script>
	<script type="text/javascript" src="../DatePicker/WdatePicker.js"></script>
	<script type="text/javascript" src="../js/DateExt.js"></script>
    <script type="text/javascript" src="../js/FusionCharts.js"></script>
    
    <script type="text/javascript">
        $(document).ready(function () {
            $(".Read").attr("readonly", true);
            var arr="<%=chartJson %>".split("$");
            var html=["图形类型：<select class='chartType'>"];
            for (var i = 0; i < arr.length; i++) {
                var item=arr[i].split("|");
                if(arr[i].length>0)
                {
                    if("<%=chartSWF%>"==item[0])
				        html.push("<option value='"+arr[i]+"' selected>"+item[1]+"</option>");
				    else
				        html.push("<option value='"+arr[i]+"' >"+item[1]+"</option>");
				}
            }
            html.push("</select>");
            $("#chartOption").prepend(html.join(""));

            $(".chartType").change(function(){
                chartRefresh();
            });
			
            genCondition();
            chartRefresh();
            $(".curweek").click(function(){
                $(".linksel").removeClass("linksel");
                $(this).addClass("linksel");
                var today=new Date();
                var fld=$(this).attr("fldname");
                $("[name="+fld+"]").eq(0).val(today.clone().addDays(1-today.getDay()).toString("yyyy-MM-dd"));
                $("[name="+fld+"]").eq(1).val(today.addDays(7-today.getDay()).toString("yyyy-MM-dd"));
            });
            $(".curmonth").click(function(){
                $(".linksel").removeClass("linksel");
                $(this).addClass("linksel");
                var today=new Date();
                var fld=$(this).attr("fldname");
                $("[name="+fld+"]").eq(0).val(today.clone().moveToFirstDayOfMonth().toString("yyyy-MM-dd"));
                $("[name="+fld+"]").eq(1).val(today.moveToLastDayOfMonth().toString("yyyy-MM-dd"));
            });
            $(".lastmonth").click(function(){
                $(".linksel").removeClass("linksel");
                $(this).addClass("linksel");
                var today=new Date();
                var fld=$(this).attr("fldname");
                $("[name="+fld+"]").eq(0).val(today.clone().addMonths(-1).toString("yyyy-MM-dd"));
                $("[name="+fld+"]").eq(1).val(today.toString("yyyy-MM-dd"));
            });
            $(".last3month").click(function(){
                $(".linksel").removeClass("linksel");
                $(this).addClass("linksel");
                var today=new Date();
                var fld=$(this).attr("fldname");
                $("[name="+fld+"]").eq(0).val(today.clone().addMonths(-3).toString("yyyy-MM-dd"));
                $("[name="+fld+"]").eq(1).val(today.toString("yyyy-MM-dd"));
            });
            $(".linkclear").click(function(){
                var today=new Date();
                var fld=$(this).attr("fldname");
                $("[name="+fld+"]").eq(0).val('');
                $("[name="+fld+"]").eq(1).val('');
            });


        });

        function genCondition(){
            var sopt=[];
            var q="<%=query%>";
		var arrQ=q.split("$");
		for(var i=0 ;i<arrQ.length;i++){
		    if(arrQ[i].length==0)
		        continue;
		    var define=arrQ[i].split("|");
		    if (define[2] == 2 || define[2] == 3) //数字
		    {
		        sopt.push("<div class='queryfielddiv'><span class='querydisp'>", define[1], "：</span>");
		        sopt.push("<input type='text' class='textbox' size='10' name='", define[0], "'>");
		        sopt.push(" <span class='querydisp2'>&nbsp;到&nbsp;</span><input type='text' class='textbox' size='10' name='", define[0], "'></div>");

		    }
		    else if (define[2] == 4) //日期
		    {
		        var arrDate = ["", ""];
		        if (define[3]) {
		            var today=new Date();
		            switch(define[3].toLowerCase())
		            {
		                case "curweek":
		                    arrDate[0] = today.clone().addDays(1-today.getDay()).toString("yyyy-MM-dd");
		                    arrDate[1] = today.addDays(7-today.getDay()).toString("yyyy-MM-dd");
		                    break;
		                case "curmonth":
		                    arrDate[0] = today.clone().moveToFirstDayOfMonth().toString("yyyy-MM-dd");
		                    arrDate[1] = today.moveToLastDayOfMonth().toString("yyyy-MM-dd");
		                    break;
		                case "lastmonth":
		                    arrDate[0] = today.clone().addMonths(-1).toString("yyyy-MM-dd");
		                    arrDate[1] = today.toString("yyyy-MM-dd");
		                    break;
		                case "last3month":
		                    arrDate[0] = today.clone().addMonths(-3).toString("yyyy-MM-dd");
		                    arrDate[1] = today.toString("yyyy-MM-dd");
		                    break;
		            }
		        }
		        sopt.push("<div class='queryfielddiv'><span class='querydisp'>" + define[1] + "：</span>"
					, "<input type='text' class='textbox date Wdate' size='8' name='" + define[0] + "' value='" + arrDate[0] + "'/>"
					, "<span class='querydisp2'>&nbsp;到&nbsp;</span><input type='text' class='textbox date Wdate' size='8' name='" + define[0] + "' value='" + arrDate[1] + "' />");
				
		        sopt.push("&nbsp;<a class='curweek' href='javascript:' fldname='"+define[0]+"' >本周</a>");
		        sopt.push("<a class='curmonth' href='javascript:' fldname='"+define[0]+"' >本月</a>");
		        sopt.push("<a class='lastmonth' href='javascript:' fldname='"+define[0]+"' >最近一月</a>");
		        sopt.push("<a class='last3month' href='javascript:' fldname='"+define[0]+"' >最近三月</a>");
		        sopt.push("<a class='linkclear' href='javascript:' fldname='"+define[0]+"' >清空</a>");
		        sopt.push("</div>");

		    }
		    else{
		        sopt.push("<div class='queryfielddiv'><span class='querydisp'>", define[1], "：</span>"
				, "<input type='text' class='textbox' size='10' name='", define[0], "'></div>");
		    }
		}
		if(sopt.length>-1){
		    sopt.push("&nbsp;<input type=button value='查 询' id='btnQuery' onclick='query();'>");
		}
		$(".condiv").append(sopt.join(""));
		$(".date").click(function () { WdatePicker({ isShowClear: true, readOnly: false }); }).width(90);
    }

    var strWhere="";
    function query(){
        chartRefresh();
                
    }
    function chartRefresh(){

        //生成查询条件
        var arrcond = [];
        var paraCondition="<%=base.GetParaValue("condition")%>";
		if(paraCondition.length)
		    arrcond.push(paraCondition);
		var q="<%=query%>";
		var arrQ=q.split("$");

		for (var i = 0; i < arrQ.length; i++) {
		    if(arrQ[i].length==0)
		        continue;
		    var define=arrQ[i].split("|");
		    var ctlname = define[0];
		    var ctlarr = $("[name=" + ctlname + "]");
		    var r1 = /^[-+]?\d+\.?\d*$/;
		    var r2 = /^\d{4}-\d{1,2}-\d{1,2}$/;
		    if (define[2] == 2 || define[2] == 3) //数字
		    {
		        if (ctlarr.eq(0).val() != "" && ctlarr.eq(1).val() != "") {
		            if (!r1.test(ctlarr.eq(0).val())) {
		                alert(sitems[s].display + "输入不符合要求");
		                ctlarr.eq(0).select();
		                return false;
		            }
		            if (!r1.test(ctlarr.eq(1).val())) {
		                alert(sitems[s].display + "输入不符合要求");
		                ctlarr.eq(1).select();
		                return true;
		            }
		            arrcond.push(ctlname + " between " + ctlarr.eq(0).val() + " and " + ctlarr.eq(1).val() + " ");
		        }
		        else if (ctlarr.eq(0).val() == "" && ctlarr.eq(1).val() != "") {
		            if (!r1.test(ctlarr.eq(1).val())) {
		                alert(sitems[s].display + "输入不符合要求");
		                ctlarr.eq(1).select();
		                return true;
		            }
		            arrcond.push(ctlname + " <= " + ctlarr.eq(1).val() + " ");
		        }
		        else if (ctlarr.eq(0).val() != "" && ctlarr.eq(1).val() == "") {
		            if (!r1.test(ctlarr.eq(0).val())) {
		                alert(sitems[s].display + "输入不符合要求");
		                ctlarr.eq(0).select();
		                return true;
		            }
		            arrcond.push(ctlname + " >= " + ctlarr.eq(0).val() + " ");
		        }
		    }
		    else if (define[2] == 4) //日期
		    {
		        if (ctlarr.eq(0).val() != "" && ctlarr.eq(1).val() != "") {
		            if (!r2.test(ctlarr.eq(0).val())) {
		                alert(sitems[s].display + "输入不符合要求");
		                ctlarr.eq(0).select();
		                return false;
		            }
		            if (!r2.test(ctlarr.eq(1).val())) {
		                alert(sitems[s].display + "输入不符合要求");
		                ctlarr.eq(1).select();
		                return true;
		            }
		            arrcond.push(ctlname + " between '" + ctlarr.eq(0).val() + "' and '" + ctlarr.eq(1).val() + "' ");
		        }
		        else if (ctlarr.eq(0).val() == "" && ctlarr.eq(1).val() != "") {
		            if (!r2.test(ctlarr.eq(1).val())) {
		                alert(sitems[s].display + "输入不符合要求");
		                ctlarr.eq(1).select();
		                return false;
		            }
		            arrcond.push(ctlname + " <= '" + ctlarr.eq(1).val() + "' ");
		        }
		        else if (ctlarr.eq(0).val() != "" && ctlarr.eq(1).val() == "") {
		            if (!r2.test(ctlarr.eq(0).val())) {
		                alert(sitems[s].display + "输入不符合要求");
		                ctlarr.eq(0).select();
		                return false;
		            }
		            arrcond.push(ctlname + " >= '" + ctlarr.eq(0).val() + "' ");
		        }
		    }
		    else if (define[2] == 1 || define[2] == 5) //文本
		    {

		        if (ctlarr.eq(0).val() != "")
		            arrcond.push(ctlname + " like [QUOTES]%" + $.trim(ctlarr.eq(0).val()) + "%[QUOTES] ");
				
		    }


		}

		strWhere = encodeURI(arrcond.join(" and "));



		var w = $("#chartdiv").width();
		if ("<%=w%>" != "")
		    w=<%=w%>;
        var h = $("#chartdiv").height();
        if ("<%=h%>" != "")
		    h=<%=h%>;


        var arr=$(".chartType").val().split("|");
        var chart = new FusionCharts(arr[2], "ChartId", w, h, "0", "0");
        var arrUrl=["../GetChartData.ashx?chartId=<%=chartId %>"];
		arrUrl.push("showValues=" + (document.getElementById("chkShowvalue").checked?"1":"0"));
		arrUrl.push("condition="+strWhere);
		chart.setDataURL(arrUrl.join("%26"));
		chart.render("chartdiv");
    }
    </script>
	<style>
	.condiv{padding:10px;background:#fafafa;border:1px solid gray;display:block;position:relative;height:30px;}
	body{background:white;}
	a{color:#005aa0;text-decoration:none;padding:3px;line-height:18px;height:18px;display:inline-block;}
	a:hover{color:white;background:#4598d2;}
	.linksel{color:white;background:#4598d2;}
	#chartOption , .queryfielddiv{display:block;float:left;position:relative;margin:5px;}
	</style>
</head>
<body>
    
    <br />
    <div class="maindiv" style="width:1000px;margin-left:auto;margin-right:auto;">
		<form id="form2">
        <div class="condiv">
			<div id="chartOption">
			&nbsp;&nbsp;&nbsp;&nbsp;<input type="checkbox" id='chkShowvalue'/>&nbsp;<label for="chkShowvalue">显示值</label>&nbsp;&nbsp;&nbsp;&nbsp;
			</div>
		</div>
		</form>
		<br/>
        <div id="chartdiv" style="overflow:auto;">
        </div>
    </div>
	<form id="form1" runat="server">
    </form>
</body>
</html>
