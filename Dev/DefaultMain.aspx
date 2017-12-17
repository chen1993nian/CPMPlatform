<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DefaultMain.aspx.cs" Inherits="EIS.Studio.DefaultMain" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>开发平台主页</title>
    <meta http-equiv="Expires" content="0" />
    <meta http-equiv="Cache-Control" content="no-cache" />
    <meta http-equiv="Pragma" content="no-cache" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link rel="shortcut icon" type="image/x-icon" href="./Img/desktop/favicon.ico" media="screen" />
    <link rel="stylesheet" type="text/css" href="css/accordion.css" />
    <link rel="stylesheet" type="text/css" href="css/layout-default-latest.css" />
    <link href="css/TabPanel.css" rel="stylesheet" type="text/css" />
    <link href="css/tree.css" rel="stylesheet" type="text/css" />

    <style type="text/css">
        * {
            margin: 0px;
            padding: 0px;
        }

        .ui-layout-north,
        .ui-layout-center, /* has content-div */
        .ui-layout-west, /* has Accordion */
        .ui-layout-east, /* has content-div ... */
        .ui-layout-east .ui-layout-content { /* content-div has Accordion */
            padding: 0px;
            margin: 0px;
            overflow: hidden;
            background-color: #f5f5f5;
        }

        .ui-layout-mask {
            opacity: 0.2 !important;
            filter: alpha(opacity=20) !important;
            background-color: #666 !important;
        }

        #accordion1 > a {
            padding-left: 20px;
            font-size: 10pt;
        }

    </style>
    <style type="text/css">
        .winHelp, .logout, .changePWD, .relogin, .config {
            float:left;
            height: 30px;
            color: #ffffff;
            font-size: 12px;
            line-height:30px;
            text-decoration:none;
            text-align:center;
        }

        .winHelp {
    		background: url(./Img/common/help.png) no-repeat center left transparent;
            width: 64px;
        }

            .winHelp:hover {
                background-color:#59b7f5;
            }

        .logout {
    		background: url(./Img/common/home.png) no-repeat center left transparent;
            width: 64px;
        }

            .logout:hover {
                background-color:#59b7f5;
            }

        .changePWD {
    		background: url(./Img/common/edit.png) no-repeat center left transparent;
            width: 96px;
        }

            .changePWD:hover {
                background-color:#59b7f5;
            }

        .relogin {
    		background: url(./Img/common/group.png) no-repeat center left transparent;
            width: 96px;
        }

            .relogin:hover {
                background-color:#59b7f5;
            }

        .config {
    		background: url(./Img/common/gear.png) no-repeat center left transparent;
            width: 72px;
        }

            .config:hover {
                background-color:#59b7f5;
            }
        .logoPhoto {
            float:left;
            background: url(img/desktop/logo.png);
            background-size:contain;
            background-repeat:no-repeat;
            background-position:center left;
            background-position-x:left;
            background-position-y:center;
            height: 48px;
            width: 48px;
            border-radius:48px;
        }
        .logoText {
            float:left;
            height: 48px;
            width: 400px;
            margin: 0px 0px 0px 10px;
            line-height: 48px;
            font-family: 黑体, helvetica, arial, sans-serif;
            font-size: 36px;
            font-weight: bold;
            color: #ffffff;
            text-align:left;
        }

        .headGradientBack {
            background-color:#0b6ea0;
            filter: progid:DXImageTransform.Microsoft.gradient(GradientType=0, startColorstr=#0b6ea0, endColorstr=#c7e9fa);/*IE<9>*/
            -ms-filter: "progid:DXImageTransform.Microsoft.gradient (GradientType=0, startColorstr=#0b6ea0, endColorstr=#c7e9fa)";/*IE8+*/
            background:-moz-linear-gradient(left,#0b6ea0,#c7e9fa);/*Mozilla*/
            background:-webkit-gradient(linear,0 50%,100% 50%,from(#0b6ea0),to(#c7e9fa));/*Old gradient for webkit*/
            background:-webkit-linear-gradient(left,#0b6ea0,#c7e9fa);/*new gradient for Webkit*/
            background:-o-linear-gradient(left,#0b6ea0,#c7e9fa); /*Opera11*/
        }


        #HeadTable {
            min-width:1000px;
            width:100%;
        }
    </style>
    <!-- REQUIRED scripts for layout widget -->
    <script type="text/javascript" src="js/jquery.js"></script>
    <script type="text/javascript" src="js/jquery-ui-1.7.2.js"></script>
    <script type="text/javascript" src="js/jquery.layout.js"></script>
    <script type="text/javascript" src="js/TabPanel.js"></script>
    <script type="text/javascript" src="js/Fader.js"></script>
    <script type="text/javascript" src="js/jquery.cookie.js"></script>
    <script type="text/javascript" src="js/lhgdialog.min.js?s=default,chrome,"></script>
    <script type="text/javascript">

        var myLayout; // init global vars
        var tabpanel;

        function closeAllPanel() {
            $.each('north,south,west,east'.split(','), function () { myLayout.close(this); });
        }
        function openAllPanel() {
            $.each('north,south,west,east'.split(','), function () { myLayout.open(this); });
        }
        function toggleAllPanel() {
            $.each('north,south,west,east'.split(','), function () { myLayout.toggle(this); });
        }
        function toggleLeftPanel() {
            myLayout.toggle("west");
        }
        $(function () {

            myLayout = $('body').layout({
                spacing_open: 5,
                maskIframesOnResize: true
                , west__onresize: function () { $("#accordion1").accordion("resize"); }
                , center__onresize: function () { tabpanel.resize(); }
                , north__resizable: false
                , west__toggler: false
            });

            var wbs = $("#accordion1>a:first").attr("wbs");
            $("#accordion1").accordion({
                fillSpace: true,
                change: function (event, ui) {
                    wbs = ui.newHeader.attr("wbs");

                    $.ajax({
                        type: "post",
                        url: "treenode.aspx",
                        data: { parentnode: wbs },
                        async: true,
                        dataType: "json",
                        success: function (data) {
                            o.data = data;
                            $("#menuTree_" + wbs).treeview(o);
                        },
                        error: function (e) { window.location = "default.aspx?logout=1"; }
                    });

                }
            });
            var o = {
                showcheck: false,
                onnodeclick: menuClick,
                theme: "bbit-tree-no-lines",
                blankpath: "Img/common/",
                cbiconpath: "Img/common/"
            };
            //加载菜单
            $.ajax({
                type: "post",
                url: "treenode.aspx",
                data: { parentnode: wbs },
                async: true,
                dataType: "json",
                success: function (data) {
                    o.data = data;
                    $("#menuTree_" + wbs).treeview(o);
                },
                error: function (e) { window.location = "default.aspx?logout=1"; }
            });

            //增加TabPanel
            tabpanel = new TabPanel({
                renderTo: 'tab',
                active: 0,
                maxLength: 25,
                items: [{
                    title: '首页',
                    id: 0,
                    html: '<iframe src="Desktop.aspx" width="100%" height="100%" frameborder="0"></iframe>',
                    closable: false
                }]
            });
        });
        function menuClick(item) {
            var d = new Date();
            if (item.value)
                tabpanel.addTab({ id: item.id, title: item.text, html: '<iframe src="' + item.value + '" width="100%" height="100%" frameborder="0"></iframe>' });
        }
        function addTab(tabId, tabTitle, tabUrl) {
            tabpanel.addTab({ id: tabId, title: tabTitle, html: '<iframe src="' + tabUrl + '" width="100%" height="100%" frameborder="0"></iframe>' });
        }
        document.execCommand("BackgroundImageCache", false, true);

        function moveright() {
            $("#mainMenu").animate({ left: '-=180px' }, 300);
        }
        function moveleft() {
            if ($("#mainMenu").css("posLeft") >= 0)
                return;
            $("#mainMenu").animate({ left: '+=180px' }, 300);
        }
        /**/
        $(function () {
            //LayoutMenu.style.width = window.screen.availWidth - 270;
            var photoid = $('.logoPhoto').attr("data-photoid");
            if (photoid != "") {
                var imgUrl = "url(./SysFolder/Common/FileDown.ashx?FileID=" + photoid + ")";
                $('.logoPhoto').css({ "background-image": imgUrl });
            }

            $(".changePWD").dialog({
                title: '修改密码', maxBtn: false, page: 'ChangePass.aspx'
                , btnBar: false, cover: true, lockScroll: true, width: 700, height: 400, bgcolor: 'black'
            });

            $(".winHelp").click(function () {
                var url = $(this).attr("data-hlpUrl");
                app_showHelp(url, "帮助");
            });

        });


        function app_showHelp(urlStr, titleStr) {
            if ((titleStr == undefined) || (titleStr == "")) titleStr = "帮助";
            var win = new $.dialog({
                id: 'HelpWin'
                , cover: true
                , maxBtn: true
                , minBtn: true
                , btnBar: true
                , lockScroll: false
                , title: titleStr
                , autoSize: false
                , width: 1150
                , height: 600
                , resize: true
                , bgcolor: 'black'
                , iconTitle: false
                , page: urlStr
            });
            win.ShowDialog();
        }
    </script>
</head>
<body>

    <div class="ui-layout-north">
        <div id="HeadTable">
            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="headGradientBack" id="dd">
                <tr>
                    <td style="width: 40%;" rowspan="2">
                        <div class="logoPhoto" data-photoid="<%=base.PhotoId %>" ></div>
                        <div class="logoText"><%=base.EmployeeName %></div>
                    </td>
                    <td style="width:20%" rowspan="2" align="right">&nbsp;</td>
                    <td align="right" valign="top">
                        <div style="width:100%;">
                                <a class="winHelp" data-hlpUrl="Help/HelpIndex.aspx" href="#">帮助</a>
                                <a class="logout" href="Default.aspx?logout=1">注销</a>
                                <a class="changePWD" href="javascript:">修改密码</a>
                                <a class="relogin" href="Default.aspx?logout=1">重新登录</a>
                                <a class="config" href="#">个性化</a>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
    </div>

    <div class="ui-layout-west" style="display: none;">
        <div id="accordion1" class="basic">
            <%=leftMenu %>
        </div>
    </div>
    <div class="ui-layout-center" id="mainframe">
        <div id="tab" style="width: 100%; height: 100%"></div>
    </div>
    <script src="js/jquery.tree.js" type="text/javascript"></script>

</body>
</html>
