<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HelpSysConfigs.aspx.cs" Inherits="CEIM.WebHelp.Help.HelpSysConfigs" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link rel="stylesheet" type="text/css" href="Imgs/HelpStyle1.css" />
    <script type="text/javascript" src="../js/jquery-1.7.js"></script>
    <script type="text/javascript" src="../js/jquery-1.8.0.min.js"></script>
    <script type="text/javascript" src="../js/layer/layer.js"></script>
    <script type="text/javascript" src="Imgs/helpmain.js"></script>
    <title></title>
    <style>
        .SysConfig01 {
            background-image: url(Imgs/Help_SysConfig_01.png);
            height: 360px;
            width: 735px;
        }

        .SysConfig02 {
            background-image: url(Imgs/Help_SysConfig_02.png);
            height: 500px;
            width: 735px;
        }

        .SysConfig03 {
            background-image: url(Imgs/Help_SysConfig_03.png);
            height: 251px;
            width: 735px;
        }

        .SysConfig04 {
            background-image: url(Imgs/Help_SysConfig_04.png);
            height: 251px;
            width: 735px;
        }
        .SysConfig05 {
            background-image: url(Imgs/Help_SysConfig_05.png);
            height: 418px;
            width: 735px;
        }
        .SysConfig06 {
            background-image: url(Imgs/Help_SysConfig_06.png);
            height: 300px;
            width: 771px;
        }
        .SysConfig07 {
            background-image: url(Imgs/Help_SysConfig_07.png);
            height: 300px;
            width: 771px;
        }
        .SysConfig0701 {
            background-image: url(Imgs/Help_SysConfig_0701.png);
            height: 300px;
            width: 771px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="hlpMain">
            <div class="hlpContent">
                <div class="hlpContentItem" id="a1">
                    <div class="hlpContentItemTitle">基本信息配置</div>
                    <div class="hlpContentItemContent">
                        <ol>
                            <li class="hlpImg SysConfig01"></li>
                        </ol>
                    </div>
                </div>
                <div class="hlpContentItem" id="a2">
                    <div class="hlpContentItemTitle">流程信息配置</div>
                    <div class="hlpContentItemContent">
                        <ol>
                            <li class="hlpImg SysConfig02"></li>
                        </ol>
                    </div>
                </div>
                <div class="hlpContentItem" id="a3">
                    <div class="hlpContentItemTitle">系统邮件配置</div>
                    <div class="hlpContentItemContent">
                        <ol>
                            <li class="hlpImg SysConfig03"></li>
                        </ol>
                    </div>
                </div>
                <div class="hlpContentItem" id="a4">
                    <div class="hlpContentItemTitle">目录服务配置</div>
                    <div class="hlpContentItemContent">
                        <ol>
                            <li class="hlpImg SysConfig04"></li>
                        </ol>
                    </div>
                </div>
                <div class="hlpContentItem" id="a5">
                    <div class="hlpContentItemTitle">自定义脚本引用</div>
                    <div class="hlpContentItemContent">
                        <ol>
                            <li class="hlpImg SysConfig05"></li>
                        </ol>
                    </div>
                </div>
                <div class="hlpContentItem" id="a6">
                    <div class="hlpContentItemTitle">附件目录配置</div>
                    <div class="hlpContentItemContent">
                        <ol>
                            <li class="hlpImg SysConfig06"></li>
                        </ol>
                    </div>
                </div>
                <div class="hlpContentItem" id="a7">
                    <div class="hlpContentItemTitle">登录背景图</div>
                    <div class="hlpContentItemContent">
                        <ol>
                            <li class="hlpImg SysConfig07"></li>
                            <li class="hlpImg SysConfig0701"></li>
                        </ol>
                    </div>
                </div>
                <div class="hlpContentItem" id="a8">
                    <div class="hlpContentItemTitle">企业LOGO</div>
                    <div class="hlpContentItemContent">
                        <ol>
                            <li class="hlpImg SysConfig07"></li>
                            <li class="hlpImg SysConfig0701"></li>
                        </ol>
                    </div>
                </div>
                <div class="hlpContentItem" id="a9">
                    <div class="hlpContentItemTitle">系统配置项</div>
                    <div class="hlpContentItemContent">
                        <ol>
                            <li class="hlpImg SysConfig07"></li>
                            <li class="hlpImg SysConfig0701"></li>
                        </ol>
                    </div>
                </div>
                <div class="hlpContentItem" id="a10">
                    <div class="hlpContentItemTitle">管理员账号</div>
                    <div class="hlpContentItemContent">
                        <ol>
                            <li class="hlpImg SysConfig07"></li>
                            <li class="hlpImg SysConfig0701"></li>
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
