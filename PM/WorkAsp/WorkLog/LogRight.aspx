<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LogRight.aspx.cs" Inherits="EIS.Web.WorkAsp.WorkLog.LogRight" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>工作日志</title>
    <meta http-equiv="pragma" content="no-cache" /> 
    <meta http-equiv="cache-control" content="no-cache" /> 
    <meta http-equiv="expires" content="0" /> 
    <link href="../../Css/common.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="../../css/appstyle.css" />    
	<script type="text/javascript" src="../../js/jquery-1.7.min.js"></script>
    <script type="text/javascript" src="../../js/jquery.notice.js"></script>
    <style type="text/css">
        html{height:100%;}
        body{background:#f9fafe;padding:0px;margin:0px;overflow:hidden;height:100%;}
        .toolbar{border-bottom:1px solid #3a6ea5;}
        .toolbar a{
            display:inline-block;
            padding:3px 8px;
            margin:0px 1px;
            font-size:12px;
            text-decoration:none;
            color:Black;
            font-family:微软雅黑,黑体,宋体;
            }
       .toolbar a:hover,.toolbar a.on{
            color:white;
            font-weight:bold;
            background:#3a6ea5;
           }
       .subbar{float:right;display:inline; width:360px;padding-top:3px;}
       .toolbar a.subtitle{
            color:#3a6ea5;
            font-weight:bold;
           }
       .toolbar a.subtitle:hover{
            color:#3a6ea5;
            font-weight:bold;
            background-color:transparent;
           }
       #logbody {padding:20px;overflow:auto;}
       .logItem{margin-bottom:10px;}
       
       .itemHeader{height:22px;border-bottom:1px solid #3a6ea5;}
       .headerRight{float:right;}
       .headerLeft{float:left;}
       .itemHeader a{
            display:inline-block;
            padding:3px 8px;
            margin:0px 0px;
            font-size:12px;
            text-decoration:none;           
           }
       .itemHeader a.on,.itemHeader a:hover{
            font-weight:bold;
            color:white;
            background:#3a6ea5;
            font-family:微软雅黑,黑体,宋体;
           }
       .itemBody{
           clear:both;
           padding:10px;
           background:white;
           font-size:10pt;
           line-height:200%;
           font-family:微软雅黑,黑体,宋体;
        }
        .itemBody ul li{margin-left:10px;}
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="toolbar" style="">
        <div style="float:left;">
            <a href="javascript:" class="subtitle">[ 我的工作日志 ]</a>
            &nbsp;
            <button id="btnNew" type="button" title="填写工作日志"><img alt="填写工作日志" src="../../img/common/add.png" />填写工作日志</button>
            &nbsp;
            <button id="btnFresh" type="button" title="刷新"><img alt="刷新" src="../../img/common/fresh.png" />刷新</button>
            &nbsp;
        </div>
        <div class="subbar">
            <a href="javascript:" class="on"><%=startDate.ToString("yyyy年MM月dd日 dddd")%> </a>&nbsp;&nbsp;
            <%=PreNextLink %>
        </div>
    </div>
    <div id="logbody">
        <%=sbLog %>
        <div id="Rpage" class="Rpage-main" style="width:500px;<%=emptyStyle%>">
        <div id="Rheader"></div>
        <div id="Rbody">
            <div class="title">
                <b class="crl"></b><b class="crr"></b>
                <h1>系统提示</h1>
            </div>
            <div class="content" style="height:100px;padding-top:10px;">
                <table width="90%" align="center">

                    <tr>
                        <td rowspan="2" width="80"><img alt="提示" src="../../img/icon_64/icon64_info.png" /></td>
                        <td style="padding:20px;line-height:20px;">
                            <%=emptyInfo %>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="bottom">
                <b class="crl"></b><b class="crr"></b>
            </div>
        </div>
    </div>
    </div>

    </form>
</body>
</html>
<script type="text/javascript">

    $(function () {
        var h = $("body").height();
        $("#logbody").height(parseInt(h) - 60);
        $("#btnNew").click(function () {
            window.open("LogEdit.aspx", "_self");
        });

        $("#btnFresh").click(function () {
            window.location.reload();
        });
        $(".editLink").click(function () {
            var logId = $(this).attr("logId");
            window.open("LogEdit.aspx?mainId=" + logId, "_self");
        });
        $(".removeLink").click(function () {
            var logId = $(this).attr("logId");
            var _curClass = EIS.Web.WorkAsp.WorkLog.LogRight;
            var ret = _curClass.RemoveLog(logId);
            if (ret.error) {
                alert("删除出错：" + ret.error.Message);
            }
            else {
                $.noticeAdd({ text: '成功删除！' });
                $(this).closest(".logItem").remove();
            }
        });
    });
</script>
