<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LogItem.aspx.cs" Inherits="EIS.Web.WorkAsp.WorkLog.LogItem" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>工作日志</title>
    <link rel="stylesheet" type="text/css" href="../../css/appstyle.css" />    
	<script type="text/javascript" src="../../js/jquery-1.7.min.js"></script>
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
       .subbar{float:right;display:inline; width:60px;padding-top:3px;}
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
        
        .h_text { width:164px; height:17px; border:1px #ddd solid; vertical-align:middle; padding:2px; margin-left:5px;}
        #Submit1,.h_submit { width:80px; height:25px; background:url(../../img/common/bg.gif) 0 -94px no-repeat; border:0; vertical-align:middle; margin-left:5px; cursor:pointer;}
        .ccomment { margin-top:20px;border:1px #B3DCEE solid;width:98%;}
        .ccomment h3 {color:#333; border-bottom:1px #B3DCEE solid; background:#D8ECFA; height:28px; line-height:28px; padding-left:10px; font-weight:bold;font-size:10pt;}
        .c_list { margin:10px; border-bottom:1px #ddd dashed;}
        .c_list .c_writer { float:left; color:#999;}
        .c_list .c_time { float:right;color:#999;}
        .c_list p { clear:both; padding:5px 0px;}
        .cwrite { background:#F4F7FA; margin:10px; padding:10px 15px; border:1px #E8E8E8 solid;}
        .cwrite h4 { font-size:12px; font-weight:bold; color:#333; margin-bottom:5px;}
        .cwrite textarea { width:616px; border:1px #B7CAD1 solid; padding:2px;}
        .cwrite_btns { margin:10px 0 10px 0;}
        .cwrite_btns input { vertical-align:middle;}
        .cwrite_btns .h_submit {margin-right:20px;}
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="toolbar" style="">
        <div style="float:left;">
            &nbsp;
            <button id="btnFresh" type="button" title="刷新"><img alt="刷新" src="../../img/common/fresh.png" />刷新</button>
            &nbsp;
        </div>
        <div class="subbar">
            <a href="javascript:window.history.back();" class="on"> 返回 </a>&nbsp;&nbsp;
            <%=PreNextLink %>
        </div>
    </div>
    <div id="logbody">
        <%=sbLog %>
        <div class="ccomment">
        <h3>相关评论</h3>
            <%=sbComment%>
            <div class="cwrite">
                <h4>我来说两句</h4>
                <asp:TextBox ID="TextBox1" Rows="4" runat="server" TextMode="MultiLine"></asp:TextBox>
                <div class="cwrite_btns">
                    <asp:Button ID="Submit1" CssClass="h_submit" runat="server" Text="提 交" 
                        onclick="Submit1_Click" />
                    <input type="reset" value="清 空" class="h_submit" />&nbsp;&nbsp;&nbsp;&nbsp;
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

        $("#btnFresh").click(function () {
            window.location.reload();
        });
    });
</script>
