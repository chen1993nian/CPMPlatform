<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DealFlowByGroup.aspx.cs" Inherits="EIS.WebBase.SysFolder.WorkFlow.DealFlowByGroup" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>我的待办</title>
    <meta http-equiv="Pragma" content="no-cache" />
    <meta http-equiv="Content-Type" content="text/html;charset=utf-8" />
    <script type="text/javascript" src="../../js/jquery-1.4.2.min.js"></script>
    <script type="text/javascript" src="../../Js/jquery.cookie.js"></script>
    <style type="text/css">
        body
        {
            background-color: white;
            overflow:auto;
            font-family:Tahoma,Helvetica,Arial,sans-serif;
            font-size:12px;

        }
        #maindiv{}
        table
        {
            table-layout: fixed;
            border-collapse: collapse;
            border: 1px solid gray;
        }
        td
        {
            border: 1px solid gray;
            padding: 5px;
            height:25px;
        }
        .model
        {
            padding: 3px;
            background-color: #eee;
            margin-bottom: 2px;
        }
        .tip
        {
            border: dotted 1px orange;
            background: #F9FB91;
            text-align: left;
            padding: 5px;
            margin-top: 10px;
        }
        input[type=checkbox]
        {
            cursor:pointer;
        }
        input[type=submit]
        {
            padding: 3px;
            height: 28px;
        }
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
            padding-left: 15px;
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
        .tabs li em{color:red;font-style:normal;}
        .tabPage{padding:5px 3px;}
        input[type=radio]{vertical-align:middle;cursor:pointer;}
        input[type=submit]{vertical-align:middle;cursor:pointer;}

        .data-table{
            border-collapse:collapse;
            border:1px solid #dae2e5;
            width:100%;
            font-size:12px;    
            }
        .data-table >thead>tr>th{
            background-color:#e5f1f6;
            color:#414141;
            padding:10 5px;
            font-size:12px;
            font-weight:normal;
            height:30px;
            border-right:1px solid #dae2e5;
            cursor:pointer;
            }
        .th_asc{
            background:url(../img/icons/uh.png) no-repeat center top;
            }
        .th_desc{
            background:url(../img/icons/dh.png) no-repeat center top;
            }
        .data-table >tbody>tr>td{
            text-align:center;
            vertical-align:middle;
            padding:5px 0px;
            border:1px solid #dae2e5;
            background-color:#fffff0;
            }
        a{color:Blue;text-decoration:none;}
        a:hover{text-decoration:underline;}
        .imgbtn{background:url(../../img/common/big_btn_b.png) no-repeat;
                height:26px;line-height:26px;
                width:86px;
                border:0px solid white;
                cursor:pointer;
                color:Green;}
        .data-table >tbody>tr>td.taskLink{text-align:left;padding-left:10px;}
        .data-table >tbody>tr>td a.duban{color:Red;}
        #btnBatch{display:inline-block;}
        .bottombar{padding:10px 0px;}
        .bottombar input[type=button]{cursor:pointer;}
    </style>
    <script type="text/javascript">
        jQuery(function () {
            $(".taskchk").attr("title", "选中之后可以批量审批");
            $(".chkall").click(function () {
                $(".taskchk").attr("checked", $(this).attr("checked"));
            });
            $(".tabs>li").click(function () {
                var i = $(this).index();
                $("li.selected").removeClass("selected");
                jQuery.cookie("selTab", i);
                $(this).addClass("selected");
                $("#tabControl").children().hide();
                $("#tabControl").children(":eq(" + i + ")").show();

            });

            var selTab = jQuery.cookie("selTab");
            if (selTab) {
                $(".tabs>li").eq(selTab).click();
            }
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="maindiv" style="text-align: left; width: 860px;">
        <ul class="tabs">
            <li class="selected"><a href="#tabpage1"><%=WFName%></a></li>
        </ul>
        <div id="tabControl">
            <div class="tabPage">
                <table class="data-table" border="0" cellpadding="0" cellspacing="0">
                <thead>
                 <tr>
                    <th width="40" align="center"><input type="checkbox" title="全选" id="chkall" class="chkall" value="全选" /></th>
                    <th align="left">&nbsp;&nbsp;任务标题</th>
                    <th width="100" align="center">步骤名称</th>
                    <th width="60" align="center">发起人</th>
                    <th width="120" align="center">到达时间</th>
                </tr>
                </thead>
                <tbody>
                   <%=sbToDo%> 
                </tbody>
                </table>
                <div class="bottombar">
                    <asp:Button ID="btnBatch" runat="server" Text="  批量审批  " onclick="btnBatch_Click" />
                </div>
            </div>
        </div>
        <%=sbMsg %>
    </div>
    </form>
</body>
</html>
