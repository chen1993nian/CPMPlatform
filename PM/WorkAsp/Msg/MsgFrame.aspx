<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MsgFrame.aspx.cs" Inherits="EIS.Web.WorkAsp.Msg.MsgFrame" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>我的消息</title>
    <link href="../../Css/common.css" rel="stylesheet" type="text/css" />
    <script src="../../Js/jquery-1.7.min.js" type="text/javascript"></script>

</head>
<body>
    <form id="form1" runat="server">
    <div id="navPanel">
	    <div id="navMenu">
		    <a class="active" href="MsgRecList.aspx" target="msgmain"><span>消息接收列表</span></a>
		    <a id="sendListLink" href="MsgSendList.aspx" target="msgmain"><span>消息发送列表</span></a>
		    <a href="MsgSend.aspx" target="msgmain"><span>＋发送消息</span></a>
	    </div>
    </div>
    <iframe width="100%" height="400px" frameborder="0" id="msgmain" name="msgmain" src="MsgRecList.aspx"></iframe>
    </form>
</body>
</html>

    <script type="text/javascript">
        $("#msgmain").height($(document).height() - 40);

        jQuery(function () {
            jQuery(window).resize(function () {
                $("#navMenu a").click(function () {
                    $("#navMenu a.active").removeClass("active");
                    $(this).addClass("active");
                });
            });
        });
    </script>
