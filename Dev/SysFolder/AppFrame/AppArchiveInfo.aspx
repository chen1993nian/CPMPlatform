<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AppArchiveInfo.aspx.cs" Inherits="EIS.WebBase.SysFolder.AppFrame.AppArchiveInfo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>查看档案相关信息</title>
    <meta http-equiv="Pragma" content="no-cache"/>
    <link type="text/css" rel="stylesheet" href="../../Css/wfstyle.css" />
    <script type="text/javascript" src="../../js/jquery-1.7.min.js"></script>
    <style type="text/css" media="print"> 
	    .NoPrint{display:none;} 
	    .PageNext{page-break-after: always;}
    </style> 
    <style type="text/css"> 
		body{background:white;}
	    dl{width:770px;text-align:left;}
	    dt{cursor:hand;}
		#maindiv{overflow:auto;}
	    .normaltbl>tbody>tr> td{padding:2px;}
	    a
        {
            text-decoration: none;
            outline-style: none;
        }
        .tabs
        {
            padding-bottom: 0px;
            list-style-type: none !important;
            margin: 0px 0px 20px;
            padding-left: 0px;
            padding-right: 0px;
            zoom: 1;
            padding-top: 0px;
        }
        .tabs:before
        {
            display: inline;
            content: "";
        }
        .tabs:after
        {
            display: inline;
            content: "";
        }
        .tabs:after
        {
            clear: both;
        }
        .tabs li
        {
            padding-left: 5px;
            float: left;
        }
        .tabs li a
        {
            display: block;
        }
        .tabs
        {
            border-bottom: #d0e1f0 1px solid;
            width: 100%;
            float: left;
			padding-left:5px;
        }
        .tabs li
        {
            position: relative;
            top: 1px;
        }
        .tabs li a
        {
            border-bottom: #d0e1f0 1px solid;
            border-left: #d0e1f0 1px solid;
            padding-bottom: 0px;
            line-height: 28px;
            padding-left: 15px;
            padding-right: 15px;
            background: #e3edf7;
            color: #666 !important;
            border-top: #d0e1f0 1px solid;
            margin-right: 2px;
            border-right: #d0e1f0 1px solid;
            padding-top: 0px;
            border-radius: 4px 4px 0 0;
            -webkit-border-radius: 4px 4px 0 0;
            -moz-border-radius: 4px 4px 0 0;
        }
        .tabs li a:hover
        {
            background-color: #fff;
            text-decoration: none;
        }
        .tabs li.selected a
        {
            border-bottom: transparent 1px solid;
            border-left: #81b0da 1px solid;
            background-color: #fff;
            color: #000;
            border-top: #81b0da 1px solid;
            font-weight: bold;
            border-right: #81b0da 1px solid;
            _border-bottom-color: #fff;
        }
    </style> 
    <script type="text/javascript">
        jQuery(function () {
            $(window).resize(function () {
                $("#maindiv").height($(document.body).height() - 45);
            });
            $("#maindiv").height($(document.body).height() - 45);

            $(".tabs>li").click(function () {
                var i = $(this).index();
                $("li.selected").removeClass("selected");
                $(this).addClass("selected");
                $("#tabControl").children().hide();
                $("#tabControl").children(":eq(" + i + ")").show();

            });
        });
        function appPrint() {
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <!-- 工具栏 -->
    <div class="menubar NoPrint">
        <div class="topnav">
            <ul>
                <li><a href="javascript:" onclick="window.close();" >关闭</a> </li>
            </ul>
        </div>
    </div>
    
    <div id="maindiv" style="background:white;text-align:center;">
        <div style="Text-align:left;width:780px;margin-left:auto;margin-right:auto;">
	        <table width="100%" border="0" align="center">
	        <tr><td height="30"><h4 style="float:left;color:#4677bf"><span>档案名称：</span><%=archiveModel.FwTitle%></h4></td></tr>
	    </table>
        <ul class="tabs" >
            <li class="selected"><a href="#tabpage1">档案信息</a></li>
            <li><a href="#tabpage2">表单信息</a></li>
        </ul>
	    </div>

		
        <div id="tabControl" style="width:780px;margin-left:auto;margin-right:auto;margin-top:10px;">
            <div>
                <% =daHTML%> 
            </div>
            <div class="hidden">
                <% =tblHTML%> 
            	<div class="wfdealinfo">
	            <wf:UserDealInfo id="UserDealInfo" runat="server"></wf:UserDealInfo>
	            </div>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
