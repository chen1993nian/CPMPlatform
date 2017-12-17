<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FlowRelation.aspx.cs" Inherits="EIS.WebBase.SysFolder.WorkFlow.FlowRelation" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>参考审批选择</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
    <meta http-equiv="Pragma" content="no-cache"/>
    <link type="text/css" rel="stylesheet" href="../../Css/kandytabs.css"  />
    <link rel="stylesheet" type="text/css" href="../../Css/wfStyle.css" />
    <script type="text/javascript" src="../../js/jquery-1.7.min.js"></script>
    <script type="text/javascript" src="../../DatePicker/WdatePicker.js"></script>
    <script type="text/javascript" src="../../js/kandytabs.pack.js"></script>
    <style type="text/css">
        html,body{overflow:hidden;margin:0px;background:white;}
        .selpanel{border:0px solid #eee;width:780px;margin-top:5px;height:130px;overflow:hidden;}
        .box-frame,box-inner{margin:0px;}
        .box-frame{height:0px;line-height:0px;font-size:0px;position:relative;}
        .box-frame div{height:4px;width:4px;position:absolute;overflow:hidden;
                       background-image:url(../../img/common/corner_gray.png);
                       line-height:0px;
                       font-size:0px;
                       }
        .box-frame .tl , .box-frame .tr{bottom:-4px;}
        .box-frame .tl , .box-frame .bl{left:0px;}
        .box-frame .tr , .box-frame .br{right:0px;}
        .box-frame .bl , .box-frame .br{top:-4px;}
        
        .box-frame .tl{background-position:0px 0px;}
        .box-frame .tr{background-position:4px 0px;}
        .box-frame .bl{background-position:0px -4px;}
        .box-frame .br{background-position:-4px -4px;}
        
        .box-inner{border:1px solid #ddd;height:120px;}
        .box-inner .header{background:rgb(243, 243, 243);height:25px;}
        .box-inner .header .column{float:left;line-height:25px;text-align:center;}
        .box-inner .header .leftborder{border-left:1px solid #ddd;}
        .list {height:90px;overflow:auto;}
        .list .selItem{height:25px;line-height:25px;border-top:1px solid #eee;}
        .list .itemName{width:460px;overflow:hidden;float:left;text-align:left;	white-space: nowrap;text-overflow:ellipsis;}
        .list .itemEdit{width:240px;overflow:hidden;float:left;text-align:left;}
        .list .itemEdit input{width:330px;border:1px solid #ccc;padding:2px;background:#fff280;}
        .list .itemDel{overflow:hidden;float:left;text-align:center;width:60px;}
        .list .itemDel a{color:Blue;text-decoration:none;}
    </style>
    <script type="text/javascript">
        jQuery(function () {
            $(window).resize(function () {
                $("#maindiv").height($(document.body).height() - 35);
            });
            $("#maindiv").height($(document.body).height() - 35);
            $(".tabs>li").click(function () {
                var i = $(this).index();
                if (i > 0) {
                    var f = $(this).data("load");
                    if (!f) {
                        $(this).data("load", "1");
                        if (i == 1)
                            $("#frame" + i)[0].src = "FlowRelation_Query.aspx?condition=DeptId=[QUOTES][!DeptId!][QUOTES]";
                        else if (i == 2)
                            $("#frame" + i)[0].src = "FlowRelation_Query.aspx?condition=CompanyId=[QUOTES][!CompanyId!][QUOTES]";
                    }
                }
                $("li.selected").removeClass("selected");
                $(this).addClass("selected");
                $("#tabControl").children().hide();
                $("#tabControl").children(":eq(" + i + ")").show();

            });
            $(".linkclose").click(function () {
                window.close();
            });
            $(".linkconfirm").click(function () {
                var selArr = [];
                $(".itemEdit input").each(function (i, o) {
                    var insId = $(this).attr("insId");
                    var insName = $(this).attr("insName");
                    var insNote = $(this).val();
                    selArr.push(insId + "|" + insName + "|" + insNote);
                });
                window.opener.referSelect(selArr);
                window.close();
            });
            $(".btnDel").live("click", function () {
                if (confirm("您确认删除吗?")) {
                    $(this).parent().parent().remove();
                }
            });
        });
        function selTask(insId, insName) {
            var arr = [];
            arr.push("<div class='selItem'>");
            arr.push("<div class='itemName'>&nbsp;&nbsp;", insName, "</div>")
            arr.push("<div class='itemEdit'><input insId='", insId, "' insName='", insName, "' type=text value=\"\" /></div>")
            arr.push("<div class='itemDel'><a class='btnDel' href='javascript:'>删除</a></div>")
            arr.push("</div>");
            $(".list").append(arr.join(""));
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="menubar">
        <div class="topnav">
			<span style="right:10px;display:inline;float:right;position:fixed;line-height:30px;top:0px;">
			<a class="linkbtn linkconfirm"  href="javascript:" >确定</a>
            <em class="split">|</em>
			<a class="linkbtn linkclose"  href="javascript:">关闭</a>
            </span>
        </div>
    </div>
    
    <div id="maindiv">
        <ul class="tabs" style="width:96%;margin-bottom:0px;">
            <li class="selected"><a href="#tabpage1" >我发起的</a></li>
            <li><a href="#tabpage2" >本部门发起的</a></li>
            <li><a href="#tabpage3" >本公司发起的</a></li>
        </ul>
        <div id="tabControl" style="height:390px;width:780px;margin-left:auto;margin-right:auto;margin-top:0px;">
            <div>
                <iframe src="FlowRelation_Query.aspx?condition=_UserName=[QUOTES][!EmployeeId!][QUOTES]" width="100%" height="100%" frameborder="0"></iframe>
            </div>
            <div class="hidden">
                <iframe id="frame1" src="" width="100%" height="100%" frameborder="0"></iframe>
            </div>
            <div class="hidden">
                <iframe id="frame2" src="" width="100%" height="100%" frameborder="0"></iframe>
            </div>
        </div>
        <div class="selpanel">
            <div class="box-frame">
                <div class="tl"></div>
                <div class="tr"></div>
            </div>
            <div class="box-inner">
                <div class="header">
                    <div class="column" style="width:460px;">任务名称</div>
                    <div class="column leftborder" style="width:240px;">描述</div>
                    <div class="column leftborder" style="width:60px;">操作</div>
                </div>
                <div class="list">
                    
                </div>
            </div>
            <div class="box-frame">
                <div class="bl"></div>
                <div class="br"></div>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
