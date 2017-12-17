<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="Studio.JZY.Home" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>桌面</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <script language="javascript" type="text/javascript" src="js/jquery-1.4.2.min.js"></script>
    <style type="text/css">
    	.qp_counter {
			margin:  10px;
		}
		.qc_pager a
		{
			text-decoration:none;
		}
		.qp_disabled {
			color: #888;
		}

    </style> 
              
    <script type="text/javascript">
        jQuery.fn.quickpaginate = function (settings) {

            settings = jQuery.extend({
                perpage: 5,
                pager: null,
                showcounter: true,
                prev: "qp_next",
                next: "qp_prev",
                pagenumber: "qp_pagenumber",
                totalnumber: "qp_totalnumber",
                counter: "qp_counter"

            }, settings);
            var cm;
            var total;
            var last = false;
            var first = true;
            var items = jQuery(this);
            var nextbut;
            var prevbut;
            var init = function () {
                items.show();

                total = items.size();

                if (items.size() > settings.perpage) {
                    items.filter(":gt(" + (settings.perpage - 1) + ")").hide();

                    cm = settings.perpage;

                    setNav();
                }
            };
            var goNext = function () {
                if (!last) {
                    var nm = cm + settings.perpage;
                    items.hide();

                    items.slice(cm, nm).show();
                    cm = nm;

                    if (cm >= total) {
                        last = true;
                        nextbut.addClass("qp_disabled");
                    }

                    if (settings.showcounter) settings.pager.find("." + settings.pagenumber).text(cm / settings.perpage);

                    prevbut.removeClass("qp_disabled");
                    first = false;
                }
            };

            var goPrev = function () {
                if (!first) {
                    var nm = cm - settings.perpage;
                    items.hide();

                    items.slice((nm - settings.perpage), nm).show();
                    cm = nm;

                    if (cm == settings.perpage) {
                        first = true;
                        prevbut.addClass("qp_disabled");
                    }
                    if (settings.showcounter) settings.pager.find("." + settings.pagenumber).text(cm / settings.perpage);

                    nextbut.removeClass("qp_disabled");
                    last = false;
                }
            };

            var setNav = function () {
                if (settings.pager === null) {
                    settings.pager = jQuery('<div class="qc_pager"></div>');
                    items.eq(items.size() - 1).after(settings.pager);
                }

                var pagerNav = $('<a class="' + settings.prev + '" href="#">&laquo; 上页</a><a class="' + settings.next + '" href="#">下页 &raquo;</a>');

                jQuery(settings.pager).append(pagerNav);

                if (settings.showcounter) {
                    var counter = '<span class="' + settings.counter + '"><span class="' + settings.pagenumber + '"></span> / <span class="' + settings.totalnumber + '"></span></span>';

                    settings.pager.find("." + settings.prev).after(counter);

                    settings.pager.find("." + settings.pagenumber).text(1);
                    settings.pager.find("." + settings.totalnumber).text(Math.ceil(total / settings.perpage));
                }
                nextbut = settings.pager.find("." + settings.next);
                prevbut = settings.pager.find("." + settings.prev);
                prevbut.addClass("qp_disabled");
                nextbut.click(function () {
                    goNext();
                    return false;
                });
                prevbut.click(function () {
                    goPrev();
                    return false;
                });
            };
            init();
        };
        $(function () {
            $(".labelZone .bizlabel").click(function () {
                $(".bizon").removeClass("bizon");
                $(this).addClass("bizon");
                var biz = $(this).attr("biz");
                if (biz == "") {
                    $("#todolist .item").show();
                }
                else {
                    $("#todolist .item").hide();
                    $("." + biz).show();
                }
            });
            $("#todoList li").quickpaginate({ perpage: 5 });
            $("#newsList li").quickpaginate({ perpage: 5 });
            $("#mailList li").quickpaginate({ perpage: 5 });
            return;
            $(".wrapper").each(function () {
                var img;
                var len = $(".item", this).length;
                if (len > 0) {
                    $(".itemnum", this).html("（" + len + "）");
                }

            });
        });
        function viewMsg(msgId) {
            openCenter("Workasp/Msg/msgRead.aspx?msgId=" + msgId, "_blank", 660, 400);
        }
        function openCenter(url, name, width, height) {
            var str = "height=" + height + ",innerHeight=" + height + ",width=" + width + ",innerWidth=" + width;
            if (window.screen) {
                var ah = screen.availHeight - 30;
                var aw = screen.availWidth - 10;
                var xc = (aw - width) / 2;
                var yc = (ah - height) / 2;
                str += ",left=" + xc + ",screenX=" + xc + ",top=" + yc + ",screenY=" + yc;
                str += ",resizable=yes,scrollbars=yes,directories=no,status=no,toolbar=no,menubar=no,location=no";
            }
            return window.open(url, name, str);
        }

    </script>
    <style type="text/css">
        body{
            font:12px 宋体,sans-serif;
        }
        #mainframe{
            padding:0px 10px;
            width:98%;
            overflow:hidden;
        }
        .wrapper{
            border:#c7d8ea 1px solid;
            padding:0px;
            margin-bottom:8px;
            }
        .caption{
            font:12px arial,宋体,sans-serif ;
            font-weight:bold;
            border-bottom:1px solid #c7d8ea; 
            color:#3a6ea5;
            background: url(img/common/x_bg.png) repeat-x left -42px; 
            height:26px; 
            line-height:28px;
            padding:0px 10px;
         }
         .caption a{
             color:#3a6ea5;
             }
        .content{ 
            padding:5px;
            display:block;
            }
        .item{
		    display:block;
		    line-height:22px;  
		}
        .itemnum{
			color:Red;
			font-weight:bold;
		}

		.hidden{display:none;}
		.gray{color:Gray;}
		.lineperson{margin:0px 10px;}
        a{
            color:#444;
            text-decoration:none;
        }
        a:hover{
            color:#444;
            text-decoration:underline;
        }
        .labelZone{padding:2px 5px;margin-top:3px;border-bottom:1px solid #eee;}
        .split{color:#ddd;padding:0px 3px;}
        .bizlabel{background-color:#fff;color:#004499;padding:2px 3px;cursor:hand;height:18px;line-height:18px;display:inline-block;}
        .labelZone em{color:Red;font-style:normal;}
        .bizon{background-color:#3a6ea5;color:White;}
        .bizon em{color:yellow;font-style:normal;}
    </style>
</head>
<body>
<div id="mainframe" >
    <table width="100%" align="center">
        <tr>
        <td valign="top" width="60%">
             <div class="wrapper" >
	            <div class="caption">
                <a href="SysFolder/Workflow/FlowToDo.aspx" target="_self">待办事项</a><span class="itemnum"><%=iToDo %></span>
                <%=sbLeaderToDo %>
                </div>
                <div class="labelZone">
                    <%=sbLabel %>
                </div>
	            <div class="content" id="todolist">
                    <%=sbToDo %>
	            </div>
            </div>
            <div class="wrapper"  >
	            <div class="caption">
                <a href="SysFolder/AppFrame/AppQuery.aspx?tblName=Q_OA_News" target="_self">新闻中心</a>
                <span class="itemnum"><%=iNews %></span></div>
	            <div class="content" id="newsList">
                    <%=sbNews %>
	            </div>
            </div>
            <div class="wrapper"  >
	            <div class="caption">
                <a href="SysFolder/AppFrame/AppQuery.aspx?tblName=Q_OA_Note" target="_self">通知公告</a>
                <span class="itemnum"><%=iNote %></span></div>
	            <div class="content" id="notelist">
                    <%=sbNote %>
	            </div>

            </div>

            <div class="wrapper"  >
	            <div class="caption">
                <a href="Mail/MailFrame.aspx" target="_self">未读邮件</a>
                <span class="itemnum"><%=iMail %></span></div>
	            <div class="content" id="Div1">
                    <%=sbMail%>
	            </div>
            </div>
        
        </td>
        <td width="10">&nbsp;</td>
        <td valign=top>
            <div class="wrapper" >
	        <div class="caption"><a href="Workasp/msg/MsgFrame.aspx" target="_self">消息提示</a><span class="itemnum"><%=iMsg %></span></div>
	        <div class="content" id="msglist">
                <%=sbMsg %>
	        </div>
            </div>

            <div class="wrapper" >
	            <div class="caption">日程安排<span class="itemnum"><%=iSchedule %></span></div>
	            <div class="content" id="calendarlist">
                    <%=sbCalendar %>
	            </div>
            </div>
 
            <div class="wrapper" >
	            <div class="caption">我的会议<span class="itemnum"><%=iMeeting %></span></div>
	            <div class="content">
                    <%=sbMeeting %>
	            </div>
            </div>
        </td>
        </tr>
    </table>

</div>
</body></html>
