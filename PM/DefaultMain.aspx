<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DefaultMain.aspx.cs" Inherits="EIS.Web.DefaultMain" %>

<!DOCTYPE html>


<html xmlns="http://www.w3.org/1999/xhtml" class="off">


<head id="Head1" runat="server">
    <meta name="viewport" content="width=device-width,minimum-scale=1,maximum-scale=1,initial-scale=no" />
    <meta content="telephone=no" name="format-detection" />
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
<title><%=MainTitle %></title>
<link rel="shortcut icon" type="image/x-icon" href="./Img/desktop/favicon.ico" media="screen" />
<link rel="alternate stylesheet" type="text/css" href="css/zxxboxv3.css" media="screen" />
<link href="Theme/Default/css/reset.css" rel="stylesheet" type="text/css" />
<link href="Theme/Default/css/zh-cn-system.css" rel="stylesheet" type="text/css" />
<script language="javascript" type="text/javascript" src="js/jquery-1.7.min.js"></script>
<script language="javascript" type="text/javascript" src="Theme/Default/js/styleswitch.js"></script>
<link rel="alternate stylesheet" type="text/css" href="Theme/Default/css/style/zh-cn-styles1.css" title="styles1" media="screen" />
<link rel="alternate stylesheet" type="text/css" href="Theme/Default/css/style/zh-cn-styles2.css" title="styles2" media="screen" />
<link rel="stylesheet" type="text/css" href="Theme/Default/css/style/zh-cn-styles3.css" title="styles3" media="screen" />
<link rel="alternate stylesheet" type="text/css" href="Theme/Default/css/style/zh-cn-styles4.css" title="styles4" media="screen" />

<link rel="stylesheet" type="text/css" href="css/smartMenu.css" />
<script language="javascript" type="text/javascript" src="js/jquery.zxxbox.3.0-min.js"></script>
<script type="text/javascript">
    var pc_hash = 'QGXBYD'
</script>
<style type="text/css"> 
.objbody{overflow:hidden;}
.switchpos{color:White;text-decoration:none;text-align:center;font-weight:bolder;}
.switchpos:hover{color:blue;text-decoration:none;background:white;}
    .f14 a>div {
        overflow:hidden;
        word-break:keep-all;
        white-space:nowrap;
        text-overflow:ellipsis;
        width:100px;
    }
    .f14 .NoLeaf {
        font-weight:bold;
    }
</style>
</head>
<body scroll="no" class="objbody">
 <form id="form1" runat="server">
 </form>

<div class="header">
	<div class="logo lf">
        <a href="#" ><span class="invisible">OA协同办公系统</span></a>
    </div>
    <div class="rt-col">
    	<div class="tab_style white cut_line text-r">
        <a href="javascript:;" onclick="show_changepwd()"><img src="Theme/Default/images/u_zx1.gif"/> 修改密码</a><span>|</span>
        <a href="desktop.aspx?pwbs=switch" target="rightMain">切换系统</a><span>|</span>
        <a href="javascript:show_SubmitQuestiion();" >问题反馈</a><span>|</span>
        <a href="javascript:show_HelpDoc();" >帮助？</a>
        <ul id="Skin">
		    <li class="s1 styleswitch" rel="styles1"></li>
		    <li class="s2 styleswitch" rel="styles2"></li>
		    <li class="s3 styleswitch" rel="styles3"></li>
            <li class="s4 styleswitch" rel="styles4"></li>
	    </ul>
        </div>
    </div>
    <div class="col-auto">
    	<div class="log white cut_line">您好！<%=base.EmployeeName %> [<%=PositionName %> 
        <a class="switchpos" title="切换岗位" href="javascript:;">&nbsp;▼&nbsp;</a>]
        <span>|</span><a href="Default.aspx?logout=1">[退出]</a>
    	</div>
        <div class="topMenuPanel" style="float:left;top:10px;position:relative;overflow:hidden;">
            <ul class="nav white" id="top_menu" style="left:0px;float:left;bottom:-1px;">
                <%=mainMenu %>
            </ul>
            <div style="clear: both"></div>
        </div>
        <div id="horScroll" style="width:70px;border:0px solid red;float:left;position:relative;top:13px;cursor:hand;text-align:center;">
            <a id="btnleft" title="向左" style="display:inline-block;width:27px;height:28px;outline-width:medium;outline-style:none;outline-color:invert;background:transparent url(img/common/scroll.png);"></a>
            &nbsp;
            <a id="btnright" title="向右" style="display:inline-block;width:27px;height:28px;outline-width:medium;outline-style:none;outline-color:invert;background:transparent url(img/common/scroll.png) top right;"></a>
        </div>
        
    </div>
</div>

<div id="content">
	<div class="col-left left_menu">
    	<div id="Scroll"><div id="leftMain"></div></div>
        <a href="javascript:;" id="openClose" style="outline-style: none; outline-color: invert; outline-width: medium;" hideFocus="hidefocus" class="open" title="展开与关闭"><span class="hidden">展开</span></a>
    </div>
    <div class="col-auto mr8">
        <div class="crumbs">
            <div class="shortcut cu-span">
	            <a href="javascript:" onclick="show_online()">共有<span style="color:red; font-size:14px; font-weight:bold;" id="user_count"></span>人在线</a>&nbsp;&nbsp;&nbsp;&nbsp;
                |<a href="javascript:show_todo();" hidefocus='true' style='outline:none;'><span id="todo_count"></span> 待办(<span style="color:red; font-size:14px; font-weight:bold;" id="todo_num"></span>)</a> 
                |<a href="javascript:show_msg();" hidefocus='true' style='outline:none;'><span id="sms_count"></span> 短消息(<span style="color:red; font-size:14px; font-weight:bold;" id="sms_num"></span>)</a> 
	        </div>
            当前位置：<span id="current_pos">桌面</span>
        </div>
    	<div class="col-1">
        	<div class="content" style="position:relative; overflow:hidden;width:100%;">
                <iframe name="rightMain" id="rightMain" frameborder="0" src="home.aspx" scrolling="auto" style="border:none; margin-bottom:0px" width="100%" height="auto" allowtransparency="true"></iframe>
                <div class="fav-nav">
					<div id="panellist">
					</div>

                    <div class="fav-help">
					<div id="status_text">
						</div>
					</div>
				</div>
        	</div>
        </div>
    </div>
</div>
<div class="smart_menu_box">
    <div class="smart_menu_body">
        <ul class="smart_menu_ul">
            <%=sbPosList %>
        </ul>
    </div>
</div>
<div class="scroll">
<a href="javascript:;" class="per" title="使用鼠标滚轴滚动侧栏" onclick="menuScroll(1);"></a>
<a href="javascript:;" class="next" title="使用鼠标滚轴滚动侧栏" onclick="menuScroll(2);"></a>
</div>
<div id="sms_sound"></div>
<script type="text/javascript">
    
    var nav = $(".topMenuPanel");
    var navul = $("#top_menu");
    var list  = $("#top_menu li");
    var scroll = $("#horScroll");
    var btn = scroll.find("a");
    var _width = 0;
    var _total = 0;
    var _totaltmp = 0;
    var timer=null;
    for (var i = 0; i < list.length; i++) {
        _total += list[i].offsetWidth + 4 ;
    }
    nav.width(_width);
    navul.width(_total);
    _totaltmp = _total = _total - _width;

    function stopMove(){
        clearInterval(timer);
        if (_totaltmp <= 0)
            btn[1].style.visibility = "hidden";
        if (_totaltmp >= _total)
            btn[0].style.visibility = "hidden";
    }
    stopMove();
    jQuery(function(){
        <%=sbFlash %>
        var mwidth = $(".smart_menu_box").width();
        $(".switchpos").click(function(e){
            
            var pos =$(this).position();
            var mtop = pos.top + $(this).height();
            var mleft = pos.left-mwidth+20;
            $(".smart_menu_box").css({
                display: "block",
                left: mleft ,
                top: mtop
            });
            $(".smart_menu_a").width(mwidth);
            return false;
        });
        $(".smart_menu_box").click(function(e){
            $(this).hide();
            //return false;
        });

        $(document).click(function(){
            $(".smart_menu_box").hide();
        });
        $("#btnright").hover(function(){
            var menuObj = document.getElementById("top_menu");
            if (_totaltmp <= 0) return;
            btn[0].style.visibility = "visible";
            timer = setInterval(function() {
                _totaltmp -= 10;
                if (isNaN(menuObj.style.pixelLeft)) menuObj.style.pixelLeft = 0;
                menuObj.style.pixelLeft -= 10;
                menuObj.style.left = menuObj.style.pixelLeft + "px";
                if (_totaltmp <= 0) {
                    stopMove(false);
                }
            }, 50);
        },function(){
            stopMove(false);
        });

        $("#btnleft").hover(function(){
            var menuObj = document.getElementById("top_menu");
            if (_totaltmp >= _total) return;
            btn[1].style.visibility = "visible";
            timer = setInterval(function() {
                _totaltmp += 10;
                if (isNaN(menuObj.style.pixelLeft)) menuObj.style.pixelLeft = 0;
                menuObj.style.pixelLeft += 10;
                menuObj.style.left = menuObj.style.pixelLeft + "px";
                if (_totaltmp >= _total) {
                    stopMove(true);
                }
            }, 50);
        },stopMove);

        var curWin_With = $(window).width();
        if (curWin_With > 1024) $(".topMenuPanel").css({"width":"900px"});
        if ((curWin_With <= 1024) && (curWin_With > 800)) $(".topMenuPanel").css({"width":"800px"});
        if (curWin_With <= 800) $(".topMenuPanel").css({"width":"600px"});

    });
        if (!Array.prototype.map)
            Array.prototype.map = function (fn, scope) {
                var result = [], ri = 0;
                for (var i = 0, n = this.length; i < n; i++) {
                    if (i in this) {
                        result[ri++] = fn.call(scope, this[i], i, this);
                    }
                }
                return result;
            };

        var getWindowSize = function () {
            return ["Height", "Width"].map(function (name) {
                return window["inner" + name] ||
        document.compatMode === "CSS1Compat" && document.documentElement["client" + name] || document.body["client" + name]
            });
        }
        window.onload = function () {
            if (! +"\v1" && !document.querySelector) { // for IE6 IE7
                document.body.onresize = resize;
            } else {
                window.onresize = resize;
            }
            function resize() {
                wSize();
                return false;
            }
        }
        function wSize() {
            //这是一字符串
            var str = getWindowSize();
            var strs = []; //定义一数组
            strs = str.toString().split(","); //字符分割
            var heights = strs[0] - 115, Body = $('body'); $('#rightMain').height(heights);
            //iframe.height = strs[0]-46;
            if (strs[1] < 980) {
                $('.header').css('width', 980 + 'px');
                $('#content').css('width', 980 + 'px');
                Body.attr('scroll', '');
                Body.removeClass('objbody');
            } else {
                $('.header').css('width', 'auto');
                $('#content').css('width', 'auto');
                Body.attr('scroll', 'no');
                Body.addClass('objbody');
            }

            var openClose = $("#rightMain").height();

            //$('#center_frame').height(openClose -50);
            $("#openClose").height(openClose + 30);
            $("#Scroll").height(openClose - 20);
            windowW();
        }
        wSize();
        function windowW() {
            if ($('#Scroll').height() < $("#leftMain").height()) {
                $(".scroll").show();
            } else {
                $(".scroll").hide();
            }
        }
        windowW();
        //站点下拉菜单
        $(function () {
            var offset = $(".tab_web").offset();
            $(".tab_web").mouseover(function () {
                $(".tab-web-panel").css({ "left": +offset.left + 4, "top": +offset.top + $('.tab_web').height() + 2 });
                $(".tab-web-panel").show();
            });
            $(".tab_web span").mouseout(function () { hidden_site_list_1() });
            $(".tab-web-panel").mouseover(function () { clearh(); $('.tab_web a').addClass('on') }).mouseout(function () { hidden_site_list_1(); $('.tab_web a').removeClass('on') });
            $("#leftMain").bind("mousewheel",function(){
                if (event.wheelDelta >= 120)
                    menuScroll(1);
                else if (event.wheelDelta <= -120)
                    menuScroll(2);  
            });
            //默认载入左侧菜单
            var firstMenu =$(".top_menu:eq(0)");
            var menuName=firstMenu.text();
            var menuId="";
            if(firstMenu != null)
            {
                if(typeof(firstMenu.attr("id"))!="undefined")
                {
                    menuId=firstMenu.attr("id").substring(2);
                }
            }
       
            $.ajax({
                type: "post",
                url: "TreeNode.aspx",
                data: { parentnode: menuId},
                async: true,
                dataType: "json",
                success: function (data) {
                    createMenu(data,menuName);
                    wSize();
                },
                error: function (e) {
                    // alert(e.Mesages);
              
                    alert("会话超时，请重新登录");
                    //    window.location = "default.aspx?logout=1";
                }
            });
        })

        //隐藏站点下拉。
        var s = 0;
        var h;
        function hidden_site_list() {
            s++;
            if (s >= 3) {
                $('.tab-web-panel').hide();
                clearInterval(h);
                s = 0;
            }
        }
        function clearh() {
            if (h) clearInterval(h);
        }
        function hidden_site_list_1() {
            h = setInterval("hidden_site_list()", 1);
        }

        //左侧开关
        $("#openClose").click(function () {
            if ($(this).data('clicknum') == 1) {
                $("html").removeClass("on");
                $(".left_menu").removeClass("left_menu_on");
                $(this).removeClass("close");
                $(this).data('clicknum', 0);
                $(".scroll").show();
            } else {
                $(".left_menu").addClass("left_menu_on");
                $(this).addClass("close");
                $("html").addClass("on");
                $(this).data('clicknum', 1);
                $(".scroll").hide();
            }
            return false;
        });
        var mainMenu="";
        function _M(menuid, targetUrl) {
            $("#menuid").val(menuid);
            $("#bigid").val(menuid);
            $("#paneladd").html('<a class="panel-add" href="javascript:add_panel();"><em>添加</em></a>');
            /*
            $("#leftMain").load("inc/lmenu.php?menuid=" + menuid, { limit: 25 }, function () {
                windowW();
            });*/
            mainMenu=$("#_M"+menuid).text();
            //加载菜单
            $.ajax({
                type: "post",
                url: "TreeNode.aspx",
                data: { parentnode: menuid },
                async: true,
                dataType: "json",
                success: function (data) {
                    createMenu(data,mainMenu);
                    wSize();
                },
                error: function (e) {
              
                    alert("会话超时，请重新登录");
                    //   window.location = "default.aspx?logout=1";
                    window.parent.location = "default.aspx?logout=1";
                }
            });
            if(targetUrl == "")
                targetUrl="DeskTop.aspx?pwbs="+menuid;
            $("#rightMain").attr('src', targetUrl);
            $('.top_menu').removeClass("on");
            $('#_M' + menuid).addClass("on");
            /*
            $.get("inc/tmenu.php?table=menu&name=menuname&menuid=" + menuid, function (data) {
                $("#current_pos").html(data);
            });*/
            $("#current_pos").html(""+mainMenu + '<span id="current_pos_attr"></span>');

            //当点击顶部菜单后，隐藏中间的框架
            //$('#display_center_id').css('display', 'none');
            //显示左侧菜单，当点击顶部时，展开左侧
            $(".left_menu").removeClass("left_menu_on");
            $("#openClose").removeClass("close");
            $("html").removeClass("on");
            $("#openClose").data('clicknum', 0);
            $("#current_pos").data('clicknum', 1);
        }
        function createMenu(data,mainMenu)
        {
            var children=data[0].ChildNodes;
            var menuHtml=[];
            if(!children)
                return;
            for(var i=0;i<children.length;i++)
            {

                var pmenu=children[i];
                if(pmenu.hasChildren)
                {
                    if (pmenu.isexpand){
                        menuHtml.push("<h3 class='f14'><span class='switchs cu on' title='展开与收缩'></span><a><div class='NoLeaf' title='",pmenu.text,"'>",pmenu.text,"</div></a></h3>");
                        menuHtml.push("<ul>");
                    }
                    else
                    {
                        menuHtml.push("<h3 class='f14'><span class='switchs cu' title='展开与收缩'></span><a><div class='NoLeaf' title='",pmenu.text,"'>",pmenu.text,"</div></a></h3>");
                        menuHtml.push("<ul style='display:none;'>");
                    }
                    for(var j=0;j<pmenu.ChildNodes.length;j++)
                    {
                        var smenu=pmenu.ChildNodes[j];
                        var path=[mainMenu,pmenu.text,smenu.text];
                        var url=smenu.value;
                        menuHtml.push("<li id='_MP",smenu.id,"' title='",smenu.text,"' class='sub_menu'><a href=\"javascript:_MP('",smenu.id,"','",url,"','",path.join(" > "),"');\" hidefocus='true' style='outline:none;' title='",smenu.text,"'><div>",smenu.text,"</div></a></li>");
                    }
                    menuHtml.push("</ul>");
                }
                else
                {
                    var path=[mainMenu,pmenu.text];
                    if(pmenu.value)
                    {
                        var url=pmenu.value;
                        menuHtml.push("<h3 class='f14'><span class='cu' title='点击操作'></span><a href=\"javascript:_MP('",pmenu.id,"','",url,"','",path.join(" > "),"');\" hidefocus='true' style='outline:none;' title='",pmenu.text,"'><div>",pmenu.text,"</div></a></h3>");
                    }
                    else
                    {
                        menuHtml.push("<h3 class='f14'><span class='cu' title='点击操作'></span><a><div>",pmenu.text,"</div></a></h3>");
                    }
                
                }
        
            }
            $("#leftMain").html(menuHtml.join(""));
            $("h3").each(function(i){
                $(this).bind("mousewheel",
                    function(){
                        if (event.wheelDelta >= 120)
                            menuScroll(1);
                        else if (event.wheelDelta <= -120)
                            menuScroll(2);  
                    })
            });

            $(".switchs").each(function(i){
                var ul = $(this).parent().next();
                $(this).click(
                function(){
                    if(ul.is(':visible')){
                        ul.hide();
                        $(this).removeClass('on');
                    }else{
                        ul.show();
                        $(this).addClass('on');
                    }
                })
            });
        }
        function _MP(menuid, targetUrl , path) {

            $("#menuid").val(menuid);
            $("#paneladd").html('<a class="panel-add" href="javascript:add_panel();"><em>添加</em></a>');
        
            if(targetUrl!="")
                $("#rightMain").attr('src', targetUrl);

            $('.sub_menu').removeClass("on fb blue");
            $('#_MP' + menuid).addClass("on fb blue");
            $("#current_pos").html(path + '<span id="current_pos_attr"></span>');
            $("#current_pos").data('clicknum', 1);
        }
        function menuScroll(num) {
            var Scroll = document.getElementById('Scroll');
            if (num == 1) {
                Scroll.scrollTop = Scroll.scrollTop - 60;
            } else {
                Scroll.scrollTop = Scroll.scrollTop + 60;
            }
        }

        function show_msg() {

            _MP("viewmsg","Workasp/msg/msgFrame.aspx","首页 > 系统消息");
        }

        function show_online() {
            mytop = (screen.availHeight - 370) / 2;
            myleft = (screen.availWidth - 312) / 1;
            _MP("userback","Online.aspx","首页 > 在线人数");
        }

        function show_changepwd()
        {
            _MP("userback","ChangePass.aspx","首页 > 修改密码");
        }

        function show_todo() {
            mytop = (screen.availHeight - 370) / 2;
            myleft = (screen.availWidth - 312) / 1;
            _MP("todo","Sysfolder/workflow/flowtodo.aspx","首页 > 我的待办")
        }

        function show_SubmitQuestiion()
        {
            _MP("userback","./sysfolder/workflow/SelectWorkFlow.aspx?tblName=T_OA_SubmitQuestiion","首页 > 问题反馈");
        }

        function show_HelpDoc()
        {
            _MP("userback","./Doc/DocView_Frame.aspx","首页 > 帮助");
        }

        function show_SubmitQuestiion111()
        {
            _MP("userback","http://wsq.qq.com/reflow/259128221","首页 > 问题反馈");
        }

        //刷新在线人数
        var maxtest = 0;
        var msgFlag = "";
        function updateol()
        {
            //在线人数|待办任务数|未读消息数量,上次读取数量
            var i = EIS.Web.DefaultMain.OnlineFlash();
            //var i = EIS.Web.DefaultMain.OnlineFlash().val();
            if (i.error || i.value==null)
            {
                if (maxtest >= 3) {
                    alert("会话超时，请重新登录");
                    //  window.location = "default.aspx?logout=1";
                    window.parent.location = "default.aspx?logout=1";
                }
                else {
                    maxtest++;
                    window.setTimeout("updateol()", 1000 * <%=refreshInterval %>); 
                }

            }
            else {
                var vals=i.value.split("|");
                $("#user_count").html(vals[0]);
                $("#todo_num").html(vals[1]);
                if(msgFlag != vals[2])
                {
                    var arr = vals[2].split(",");                
                    if (arr[0] != arr[1]) {
                        //jQuery('#shortcut').click();
                        $('#sms_count').html('<img src="Theme/Default/images/xin.gif">');

                        //消息提示,设定标题提示
                        var newSmsSoundHtml =[ "<object id='sms_sound' classid='clsid:D27CDB6E-AE6D-11cf-96B8-444553540000' codebase='Theme/Default/swf/swflash.cab' width='0' height='0'>"
                        ,"<param name='movie' value='Theme/Default/swf/9.swf?t=",Math.random(),"'><param name=quality value=high>"
                        ,"<embed id='sms_sound' src='Theme/Default/swf/9.swf?t=",Math.random()
                        ,"' width='0' height='0' quality='autohigh' wmode='opaque' type='application/x-shockwave-flash' "
                        ,"plugspace='http://www.macromedia.com/shockwave/download/index.cgi?P1_Prod_Version=ShockwaveFlash'></embed></object>"];

                        $('#sms_sound').html(newSmsSoundHtml.join(""));
                        setTimeout(function(){
                            $('#sms_sound').html("");
                        },10000);
                        $("#sms_num").html(arr[0]);
                        if(msgFlag!="")
                        {
                            $.zxxbox.ask("您有新的系统消息，请注意查收", function(){  $.zxxbox.hide(); }, null, { title: "系统提示" }); 
                            //window.frames["rightMain"].location.reload();
                        }
                    } else {
                      $("#sms_num").html(arr[0]);
                        $('#sms_count').html('  ');
                    }

                    msgFlag = vals[2];

                }


                window.setTimeout("updateol()", 1000 * <%=refreshInterval %>); 
                maxtest = 0;
            }

    }
        updateol();
</script>
</body>
</html>