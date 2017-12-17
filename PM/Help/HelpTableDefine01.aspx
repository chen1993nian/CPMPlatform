<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HelpTableDefine01.aspx.cs" Inherits="CEIM.WebHelp.Help.HelpTableDefine01" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>表单定义列表</title>
    <link rel="stylesheet" type="text/css" href="Imgs/HelpStyle1.css" />
    <script type="text/javascript" src="../js/jquery-1.7.js"></script>
    <script type="text/javascript" src="../js/jquery-1.8.0.min.js"></script>
    <script type="text/javascript" src="../js/layer/layer.js"></script>
    <script type="text/javascript" src="Imgs/helpmain.js"></script>
    <style>
        .YWDY01 {
            background-image: url(Imgs/Help_Table_YWDY_01.png);
            height: 437px;
            width: 749px;
        }

        .YWDY0405 {
            background-image: url(Imgs/Help_Table_YWDY_0405.png);
            height: 237px;
            width: 598px;
        }

        .YWDY06 {
            background-image: url(Imgs/Help_Table_YWDY_06.png);
            height: 168px;
            width: 714px;
        }

        .btnNew {
            background-image: url(Imgs/Help_Table_YWDY_01.png);
            background-repeat: no-repeat;
            background-position: -5px -1px;
            height: 20px;
            width: 80px;
            padding: 5px 42px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="hlpMain">
            <div class="hlpContent">
                <div class="hlpContentItem">
                    <div class="hlpContentItemTitle">业务表单定义</div>
                    <div class="hlpContentItemContent">
                        <div class="hlpImg YWDY01"></div>
                    </div>
                </div>
                <div class="hlpContentItem">
                    <div class="hlpContentItemTitle">操作说明</div>
                    <div class="hlpContentItemContent">
                        <ol>
                            <li>工具条：【新建业务】、【删除】、【查询】、【清空】、【全选】、【导出】、【导入】。</li>
                            <li>业务表单列表，默认按修改日期排序。</li>
                            <li>【查询】：依据业务名称和中文名称查询业务表单。</li>
                            <li>点击工具条上的【导出】按钮，一次导出一张或多张表单。</li>
                            <li>点击列表上的【导出】按钮，一次导出一张表单。</li>
                            <li>点击列表上的【业务名称】，进入编辑表单页面。</li>
                            <li>点击列表上的【复制】，进入复制表单页面。</li>
                            <li>点击工具条上的【全选】，选择所有的表单。</li>
                            <li>点击工具条上的【删除】，删除所有选择的表单，如果表单存在数据，则不能删除。</li>
                        </ol>
                    </div>
                </div>
                <div class="hlpContentItem">
                    <div class="hlpContentItemTitle">查询业务表单</div>
                    <div class="hlpContentItemContent">
                        <ol>
                            <li>按表单英文名称查询。</li>
                            <li>按表单中文说明查询。</li>
                            <li>按创建时间查询。</li>
                        </ol>
                    </div>
                </div>
                <div class="hlpContentItem">
                    <div class="hlpContentItemTitle">新建/修改业务表单</div>
                    <div class="hlpContentItemContent">
                        <ol>
                            <li>先选择左边的表单分类，然后点击工具条上的<a class="btnNew"></a>按钮，弹出新建业务表单界面。</li>
                            <li><a href="HelpTableDefine02.aspx#a4" target="_self">平台字段。</a></li>
                            <li><a href="HelpTableDefine02.aspx#a7" target="_self">列表属性(渲染函数、格式和合计)。</a></li>
                            <li><a href="HelpTableDefine02.aspx#a9" target="_self">字段事件(省份城市地区级联下拉)。</a></li>
                            <li><a href="HelpTableDefine02.aspx#a10" target="_self">SQL逻辑(复制上年度保险基数)。</a></li>
                            <li><a href="HelpTableDefine02.aspx#a11" target="_self">业务逻辑(通过微信发送新闻提醒)。</a></li>
                            <li><a href="HelpTableDefine02.aspx" target="_self">详细说明点击这里。</a></li>
                        </ol>
                    </div>
                </div>
                <div class="hlpContentItem">
                    <div class="hlpContentItemTitle">删除业务表单</div>
                    <div class="hlpContentItemContent">
                        <ol>
                            <li>点击业务列表，选中一行，然后点击工具条上的【删除】，删除选择的表单。</li>
                            <li>如果表单存在数据，则不能删除。</li>
                        </ol>
                    </div>
                </div>
                <div class="hlpContentItem">
                    <div class="hlpContentItemTitle">导出业务表单</div>
                    <div class="hlpContentItemContent">
                        <ol>
                            <li>选中需要导出的业务表单，单张或多张表单。</li>
                            <li>点击工具条上的【导出】按钮，弹出表单导出下载界面。</li>
                            <li class="hlpImg YWDY0405"></li>
                            <li><a href="HelpTableDefine03.aspx" target="_self">详细说明点击这里</a>。</li>
                        </ol>
                    </div>
                </div>
                <div class="hlpContentItem">
                    <div class="hlpContentItemTitle">导入业务表单</div>
                    <div class="hlpContentItemContent">
                        <ol>
                            <li>点击工具条上的【导入】按钮，弹出表单导入界面。</li>
                            <li><a href="HelpTableDefine03.aspx#a2" target="_self">详细说明点击这里</a>。</li>
                        </ol>
                    </div>
                </div>
                <div class="hlpContentItem">
                    <div class="hlpContentItemTitle">复制业务表单</div>
                    <div class="hlpContentItemContent">
                        <ol>
                            <li>点击列表上的【复制】，进入复制表单页面。</li>
                            <li class="hlpImg YWDY06"></li>
                            <li><a href="HelpTableDefine03.aspx#a3" target="_self">详细说明点击这里</a>。</li>
                        </ol>
                    </div>
                </div>
                <div class="hlpContentItem">
                    <div class="hlpContentItemTitle">业务表单的应用</div>
                    <div class="hlpContentItemContent">
                        <ol>
                            <li><a href="HelpTableDefine04.aspx#a1" target="_self">列表页面。</a></li>
                            <li><a href="HelpTableDefine04.aspx#a2" target="_self">录入页面。</a></li>
                            <li><a href="HelpTableDefine04.aspx#a3" target="_self">详细页面。</a></li>
                            <li><a href="HelpTableDefine04.aspx#a4" target="_self">流程发起页面。</a></li>
                            <li><a href="HelpTableDefine04.aspx#a5" target="_self">查询列表页面。</a></li>
                        </ol>
                    </div>
                </div>
                <div class="hlpContentItem">
                    <div class="hlpContentItemTitle">业务表单的命名规则</div>
                    <div class="hlpContentItemContent">
                        <ol>
                            <li>命名规则：T_[系统]_[子模块]_表单名称。</li>
                            <li>[系统]，例如：综合办公系统OA，人力资源系统HR，项目管理系统PM，客户关系系统CRM，供应链系统SCM。</li>
                            <li>[系统]，例如：房地产项目管理系统BPM，施工项目管理系统CPM，设计项目管理系统DPM。</li>
                            <li>[子模块]，例如：项目立项Basic，招投标管理Bid，进度计划Plan，预算管理Budget。</li>
                            <li>[子模块]，例如：合同管理Contract，物资管理Material，质量管理Quality，安全管理Safe。</li>
                            <li>[子模块]，例如：成本管理Cost，设备管理Environment，环境管理Environment。</li>
                            <li>例如表单名称：项目管理系统中的合同模块中的专业分包合同信息表T_PM_Contract_ProfessionalSubInfo。</li>
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
