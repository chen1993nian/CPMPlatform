<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HelpFunNodeDefine.aspx.cs" Inherits="CEIM.WebHelp.Help.HelpFunNodeDefine" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link rel="stylesheet" type="text/css" href="Imgs/HelpStyle1.css" />
    <script type="text/javascript" src="../js/jquery-1.7.js"></script>
    <script type="text/javascript" src="../js/jquery-1.8.0.min.js"></script>
    <script type="text/javascript" src="../js/layer/layer.js"></script>
    <script type="text/javascript" src="Imgs/helpmain.js"></script>
    <style>
        .JDDY01 {
            background-image: url(Imgs/Help_FunNode_JDDY_01.png);
            height: 437px;
            width: 749px;
        }

        .JDDY02 {
            background-image: url(Imgs/Help_FunNode_JDDY_02.png);
            height: 437px;
            width: 749px;
        }
     
        .JDDY03 {
            background-image: url(Imgs/Help_FunNode_JDDY_03.png);
            height: 437px;
            width: 749px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="hlpMain">
            <div class="hlpContent">
                <div class="hlpContentItem" id="a1">
                    <div class="hlpContentItemTitle">产品节点（WebId）</div>
                    <div class="hlpContentItemContent">
                        <ol>
                            <li class="hlpImg JDDY01"></li>
                            <li class="hlpImg JDDY02"></li>
                        </ol>
                    </div>
                </div>
                <div class="hlpContentItem" id="a2">
                    <div class="hlpContentItemTitle">节点定义</div>
                    <div class="hlpContentItemContent">
                        <ol>
                            <li class="hlpImg JDDY03"></li>
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
