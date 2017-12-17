<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HelpTableDefine03.aspx.cs" Inherits="CEIM.WebHelp.Help.HelpTableDefine03" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>导入导出业务表单</title>
    <link rel="stylesheet" type="text/css" href="Imgs/HelpStyle1.css" />
    <script type="text/javascript" src="../js/jquery-1.7.js"></script>
    <script type="text/javascript" src="../js/jquery-1.8.0.min.js"></script>
    <script type="text/javascript" src="../js/layer/layer.js"></script>
    <script type="text/javascript" src="Imgs/helpmain.js"></script>
    <style>
        .YWDY04 {
            background-image: url(Imgs/Help_Table_YWDY_04.png);
            height: 450px;
            width: 602px;
        }

        .YWDY05 {
            background-image: url(Imgs/Help_Table_YWDY_05.png);
            height: 510px;
            width: 652px;
        }


        .YWDY07 {
            background-image: url(Imgs/Help_Table_YWDY_07.png);
            height: 176px;
            width: 672px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="hlpMain">
            <div class="hlpContent">
                <div class="hlpContentItem">
                    <div class="hlpContentItemTitle">业务表单导出</div>
                    <div class="hlpContentItemContent">
                        <div class="hlpImg YWDY04"></div>
                    </div>
                </div>
                <div class="hlpContentItem">
                    <div class="hlpContentItemTitle">导出表单操作步骤</div>
                    <div class="hlpContentItemContent">
                        1、点击【开始导出 】。<br />
                    </div>
                </div>
                <div class="hlpContentItem" id="a2">
                    <div class="hlpContentItemTitle">业务表单导入</div>
                    <div class="hlpContentItemContent">
                        <div class="hlpImg YWDY05"></div>
                    </div>
                </div>
                <div class="hlpContentItem">
                    <div class="hlpContentItemTitle">导入表单操作步骤</div>
                    <div class="hlpContentItemContent">
                        1、选择要导入的模型文件(*.xml)。<br />
                        2、点击【上传】。<br />
                        3、选择要导入的表单。<br />
                        4、点击【开始导入】。<br />
                    </div>
                </div>
                <div class="hlpContentItem" id="a3">
                    <div class="hlpContentItemTitle">复制业务表单</div>
                    <div class="hlpContentItemContent">
                        <div class="hlpImg YWDY07"></div>
                    </div>
                </div>
                <div class="hlpContentItem">
                    <div class="hlpContentItemTitle">复制表单操作步骤</div>
                    <div class="hlpContentItemContent">
                        1、在最后两列分别输入表名和中文名，主表名称不能为空，如果表名为空，系统不会进行复制。<br />
                        2、在最后两列分别输入表名和中文名，子表名称不能为空，如果子表名为空，系统不会进行复制。<br />
                        3、主表名称和子表名称不能重复，如果重复，系统不会进行复制。<br />
                        4、输入完毕后，点击【提交】进行复制。<br />
                        5、点击【关闭】取消复制。<br />
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
