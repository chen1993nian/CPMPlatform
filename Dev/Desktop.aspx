<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Desktop.aspx.cs" Inherits="EIS.Studio.Desktop" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>个人桌面</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
    <link rel="stylesheet" type="text/css" href="theme/1/style.css"/>
    <link rel="stylesheet" type="text/css" href="theme/1/portal_index.css"/>
    <script type="text/javascript" src="JS/Desktop/js_lang.js"></script>
    <script type="text/javascript" src="JS/jquery-1.8.0.min.js"></script>
    <script type="text/javascript" src="JS/Desktop/jquery.plugins.js.gz"></script>
    <script type="text/javascript" src="JS/Desktop/jquery-ui.custom.min.js.gz"></script>
    <script type="text/javascript" src="JS/Desktop/jquery.ui.draggable.min.js.gz"></script>
    <script type="text/javascript" src="JS/Desktop/jquery.ui.sortable.min.js.gz"></script>
    <script type="text/javascript" src="JS/Desktop/jquery.ux.borderlayout.js.gz"></script>
    <script type="text/javascript" src="JS/Desktop/jquery.ui.droppable.min.js.gz"></script>
    <script type="text/javascript" src="JS/Desktop/jquery.ux.slidebox.js.gz"></script>
    <script type="text/javascript" src="JS/Desktop/jquery.ux.simulatemouse.js.gz"></script>
    <script type="text/javascript" src="JS/Desktop/portal_index.js"></script>
    <script type="text/javascript">
        var bEmailPriv = true;
        var bSmsPriv = true;
        var bTabStyle = true;
        var bInitWeather = true;
        var weatherCity = "050102";
        var menuExpand = "24";
        var shortcutArray = [];
        var monInterval = { online: 120, sms: 30 };
        var ispirit = "";
        var statusTextScroll = 60;
        var show_ip = 0;
        var show_button = "0";
        var unit_name = '&nbsp;';
        var user_total_count = "6";
        

        var funcIdStr = '<%=funcIdStr %>';

        var func_array = [];
        <%=func_array %>
        var funcarray = func_array

        //-- 当前系统主题
        var ostheme = 1;
        var monInterval = 3;
        var moduleIdStr = '';
    </script>
</head>
<body style="cursor: default">
    <div id="control" style="width:100%">
        <table align="center">
            <tbody>
                <tr>
                    <td class="control-l">
                    </td>
                    <td class="control-c">
                    </td>
                    <td class="control-r">
                        <a id="openAppBox" title="设置" class="cfg" href="javascript: void(0)">
                        </a>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
    <div class="slidebox">
        <div id="trash" class="ui-droppable" style="display: none">
        </div>
        <div id="container" style="height: 548px; position: relative; margin-left: 0px; left: 0px;
            width: 1362px" >

        </div>
    </div>
    <div id="overlay" style="height: 921px; display: none">
    </div>
    <div class="background">
    </div>
</body>
</html>
