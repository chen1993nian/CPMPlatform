<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HelpIndex.aspx.cs" Inherits="CEIM.WebHelp.Help.HelpIndex" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>帮助查询</title>
    <link rel="stylesheet" type="text/css" href="Imgs/HelpStyle1.css" />
    <script type="text/javascript" src="../js/jquery-1.7.js"></script>
    <script type="text/javascript" src="../js/jquery-1.8.0.min.js"></script>
    <script type="text/javascript" src="../js/layer/layer.js"></script>
    <script type="text/javascript" src="Imgs/helpmain.js"></script>
    <script>
      
    </script>
    <style>

        .PTGS01 {
            background-image: url(Imgs/Help_Index_01.png);
            height: 360px;
            width: 954px;
        }

        .PTGS02 {
            background-image: url(Imgs/Help_Index_02.png);
            height: 360px;
            width: 954px;
        }

        .PTGS03 {
            background-image: url(Imgs/Help_Index_03.png);
            height: 360px;
            width: 954px;
        }
        .PTGS04 {
            background-image: url(Imgs/Help_Index_04.png);
            height: 360px;
            width: 954px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="hlpMain">
            <div class="hlpContent">
                <div class="hlpContentItem">
                    <div class="hlpContentItemTitle">平台概述</div>
                    <div class="hlpContentItemContent">
                        广义上，X-EIM.net是一套完整的企业信息化建设PaaS平台，企业敏捷开发AD平台，业务流程管理BPM平台。
                    </div>
                </div>
                <div class="hlpContentItem">
                    <div class="hlpContentItemTitle">30分钟完成，按企业格式要求定制表单</div>
                    <div class="hlpContentItemContent">
                        <ol>
                            <li class="hlpImg PTGS01"></li>
                        </ol>
                    </div>
                </div>
                <div class="hlpContentItem">
                    <div class="hlpContentItemTitle">30分钟完成，按企业实际定制审批流程</div>
                    <div class="hlpContentItemContent">
                        <ol>
                            <li class="hlpImg PTGS02"></li>
                        </ol>
                    </div>
                </div>
                <div class="hlpContentItem">
                    <div class="hlpContentItemTitle">流程审批入口</div>
                    <div class="hlpContentItemContent">
                        <ol>
                            <li>桌面审批：
                                <div class="hlpImg PTGS03"></div>
                            </li>
                            <li>微信移动审批：
                                <div class="hlpImg PTGS04"></div>
                            </li>
                        </ol>
                    </div>
                </div>
            </div>
            <div style="" class="hlpCatalog">
                <div class="hlpCatalogList">
                </div>
            </div>
        </div>
    </form>
</body>
</html>
