<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DefFunNodeDesktopImg.aspx.cs" Inherits="Studio.JZY.SysFolder.Permission.DefFunNodeDesktopImg" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>选择图标</title>
	<script type="text/javascript" src="../../js/jquery-1.7.js"></script>
    <script>
        $(document).ready(function () {
            getDesktopImgList();
            $(window).bind("scroll", MediaAsynLoading);
        });

        function SetFunNodeDesktopIcon() {
            $("#DesktopIconList .IconBox").click(function () {
                var obj_img = $(this).find("img");
                var filename = $(obj_img).attr("data-url");
                var c = window.opener.document.getElementById("input014");
                c.value = filename;
                window.close();
            });
        }

        function getDesktopImgList() {
            var clearCaches = new Date().getTime();
            var parass = "Action=GetData&rnd=" + clearCaches;
            var dataUrl = "DefFunNodeDesktopImg.aspx";
            $.ajax({
                type: "POST",
                url: dataUrl,
                data: parass,
                dataType: "json",
                success: function (data) {
                    if (data.length > 0) {
                        var c = window.opener.document.getElementById("input014");
                        var filename = c.value;
                        $.each(data, function (index, elem) {
                            var liStr = "<img src=\"\" data-url=\"" + elem.FileName + "\" title=\"" + elem.FileName + "\"  />";
                            var d = document.createElement('div');
                            d.innerHTML = liStr;
                            $(d).addClass("IconBox");
                            if (elem.FileName == filename) {
                                var dc = document.createElement('div');
                                dc.innerHTML = liStr;
                                $(dc).addClass("IconBox IconBoxSelect");
                                $('#DesktopIconList').prepend(dc);
                            }
                            $('#DesktopIconList').append(d);
                        });
                        MediaAsynLoading();
                        SetFunNodeDesktopIcon();
                    }
                }, error: function (req, status, error) {
                }
            });
        }


        function MediaAsynLoading() {
            var st = $(window).scrollTop(),
                sth = st + $(window).height();
            $("#DesktopIconList .IconBox").each(function () {
                post = $(this).offset().top;
                posb = post + $(this).height();
                if ((post > st && post < sth) || (posb > st && posb < sth)) {
                    var obj_img = $(this).find("img");
                    if (obj_img.length == 1) {
                        var photo_file = $(obj_img).attr("data-url");
                        var photo_src = $(obj_img).attr("src");
                        if (photo_src == "") {
                            var photo_url = "../../Theme/1/images/app_Icons/" + photo_file;
                            $(obj_img).attr("src", photo_url);
                        }
                    }
                }
            });
        }

    </script>
    <style>

        body {
            background-color: #0b6ea0;
            filter: progid:DXImageTransform.Microsoft.gradient(GradientType=0, startColorstr=#0b6ea0, endColorstr=#c7e9fa);
            -ms-filter: "progid:DXImageTransform.Microsoft.gradient (GradientType=0, startColorstr=#0b6ea0, endColorstr=#c7e9fa)";
            background: -o-linear-gradient(45deg, #0b6ea0, #c7e9fa);
            background: -moz-linear-gradient(45deg, #0b6ea0, #c7e9fa);
            background: -webkit-linear-gradient(45deg, #0b6ea0, #c7e9fa);
            background: -webkit-gradient(linear,0% 0%,100% 100%,from(#0b6ea0),to(#c7e9fa));
            cursor: default;
            font-size: 12px;
            height: 100%;
            display: block;
            overflow:scroll; 
            overflow-x:hidden; 
        }

        .IconBox {
            float: left;
            margin: 15px;
            height: 86px;
            width: 86px;
            line-height: 86px;
            cursor: pointer;
            position: relative;
            text-align: center;
            border: 1px solid #ccffcc;
            border-radius: 100px;
            border-bottom-left-radius: 100px;
            border-bottom-right-radius: 100px;
            border-top-left-radius: 100px;
            border-top-right-radius: 100px;
        }
            .IconBox img {
                max-height: 64px;
                max-width: 64px;
                position: static;
                top: -50%;
                left: -50%;
                vertical-align: middle;
            }

        .IconBoxSelect {
            background-color: red;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div style="display:block;position:relative;" id="DesktopIconList">
    </div>
    </form>
</body>
</html>
