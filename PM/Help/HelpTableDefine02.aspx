<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HelpTableDefine02.aspx.cs" Inherits="CEIM.WebHelp.Help.HelpTableDefine02" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>新建业务表单</title>
    <link rel="stylesheet" type="text/css" href="Imgs/HelpStyle1.css" />
    <script type="text/javascript" src="../js/jquery-1.7.js"></script>
    <script type="text/javascript" src="../js/jquery-1.8.0.min.js"></script>
    <script type="text/javascript" src="../js/layer/layer.js"></script>
    <script type="text/javascript" src="Imgs/helpmain.js"></script>
    <style>
        .YWDY02 {
            background-image: url(Imgs/Help_Table_YWDY_02.png);
            height: 463px;
            width: 775px;
        }

        .YWDY03 {
            background-image: url(Imgs/Help_Table_YWDY_03.png);
            height: 373px;
            width: 999px;
        }

        .YWDY10 {
            background-image: url(Imgs/Help_Table_YWDY_10.png);
            height: 373px;
            width: 1033px;
        }

        .YWDY11 {
            background-image: url(Imgs/Help_Table_YWDY_11.png);
            height: 415px;
            width: 770px;
        }

        .YWDY12 {
            background-image: url(Imgs/Help_Table_YWDY_12.png);
            height: 480px;
            width: 799px;
        }

        .YWDY13 {
            background-image: url(Imgs/Help_Table_YWDY_13.png);
            height: 342px;
            width: 796px;
        }

        .YWDY14 {
            background-image: url(Imgs/Help_Table_YWDY_14.png);
            height: 897px;
            width: 738px;
        }

        .YWDY15 {
            background-image: url(Imgs/Help_Table_YWDY_15.png);
            height: 377px;
            width: 920px;
        }

        .YWDY16 {
            background-image: url(Imgs/Help_Table_YWDY_16.png);
            height: 749px;
            width: 844px;
        }

        .YWDY17 {
            background-image: url(Imgs/Help_Table_YWDY_17.png);
            height: 526px;
            width: 717px;
        }

        .YWDY18 {
            background-image: url(Imgs/Help_Table_YWDY_18.png);
            height: 526px;
            width: 717px;
        }

        .YWDY19 {
            background-image: url(Imgs/Help_Table_YWDY_19.png);
            height: 426px;
            width: 717px;
        }

        .YWDY20 {
            background-image: url(Imgs/Help_Table_YWDY_20.png);
            height: 645px;
            width: 645px;
        }

        .FLDY01 {
            background-image: url(Imgs/Help_Table_FLDY_01.png);
            height: 357px;
            width: 918px;
        }

        .FLDY02 {
            background-image: url(Imgs/Help_Table_FLDY_02.png);
            height: 225px;
            width: 913px;
        }

        .FLDY03 {
            background-image: url(Imgs/Help_Table_FLDY_03.png);
            height: 290px;
            width: 916px;
        }

        .FLDY04 {
            background-image: url(Imgs/Help_Table_FLDY_04.png);
            height: 240px;
            width: 810px;
        }


        .ulTR {
            list-style-type: none;
            list-style: none;
            line-height: 24px;
            width: 752px;
            height: 24px;
            margin: 0px 0px 0px 0px;
        }

        .liFirstTD {
            border-top: 1px solid #33ddf5;
        }

        .liTD {
            float: left;
            border-bottom: 1px solid #33ddf5;
            border-left: 1px solid #33ddf5;
            line-height: 24px;
        }

        .liLastTD {
            border-right: 1px solid #33ddf5;
        }
        
        #abdsj table {
            border-collapse: collapse;
            border-top: 1px solid #33ddf5;
            border-right: 1px solid #33ddf5;
        }

        #abdsj table td {
            border-bottom: 1px solid #33ddf5;
            border-left: 1px solid #33ddf5;
            line-height: 24px;
        }
        #abdsj table th {
            border-bottom: 1px solid #33ddf5;
            border-left: 1px solid #33ddf5;
            line-height: 24px;
            text-align:center;
            background-color:#faf8f8;
        }

    </style>

</head>
<body>
    <form id="form1" runat="server">
        <div class="hlpMain">
            <div class="hlpContent">
                <div class="hlpContentItem" id="a1">
                    <div class="hlpContentItemTitle">新建业务表单</div>
                    <div class="hlpContentItemContent">
                        <div class="hlpImg YWDY02"></div>
                    </div>
                </div>
                <div class="hlpContentItem" id="a2">
                    <div class="hlpContentItemTitle">修改业务表单</div>
                    <div class="hlpContentItemContent">
                        <div class="hlpImg YWDY03"></div>
                    </div>
                </div>
                <div class="hlpContentItem">
                    <div class="hlpContentItemTitle">操作步骤说明</div>
                    <div class="hlpContentItemContent">
                        <ol>
                            <li>基本信息。</li>
                            <li>字段定义。</li>
                            <li>字段风格。</li>
                            <li>列表属性。</li>
                            <li>界面设计。</li>
                            <li>表单事件。</li>
                            <li>字段事件。</li>
                            <li>SQL逻辑。</li>
                            <li>业务逻辑。</li>
                            <li>脚本编辑。</li>
                            <li>业务文档。</li>
                            <li>子表定义。</li>
                        </ol>
                    </div>
                </div>
                <div class="hlpContentItem" id="a3">
                    <div class="hlpContentItemTitle">基本信息</div>
                    <div class="hlpContentItemContent">
                        1、录入表单英文名称。<br />
                        2、录入表单中文名称。<br />
                        3、点击【保存】。<br />
                        4、保存后，自动进入修改表单界面。<br />
                        5、列表语句：select * from T_SG_Contract_LaborSubInfo where |^condition^| |^sortdir^|<br />
                        <div class="pl2">
                            平台站位字符：|^condition^|，对应表单应用页面的参数：condition=projectcode='HT123'。<br />
                            平台站位字符：|^sortdir^|，对应表单应用页面的参数：Sortorder=desc&Sortname=_createtime。<br />
                            自定义站位字符，例如：@year，对应表单应用页面的参数：Defaultvalue=@year=2017。<br />
                            列表语句的作用是，研发人员可以对数据进行优化查询。
                        </div>
                        6、单条语句：select * from T_SG_Contract_LaborSubInfo where |^condition^|<br />
                        <div class="pl2">
                            同上。<br />
                            单条语句的作用是，研发人员可以对数据进行优化查询。
                        </div>
                        7、当日志级别选择非关闭状态是，可以填写日志模板。<br />
                        <div class="pl2">
                            例如：{年}年{月}月{日}日，[!EmployeeName!]修改了合同：{htname}。<br />
                            系统日志记录的结果是：2017年1月1日，米军喜修改了合同：#1号楼过道脚手架安装工程<br />
                        </div>

                    </div>
                </div>
                <div class="hlpContentItem" id="a4">
                    <div class="hlpContentItemTitle">平台字段</div>
                    <div class="hlpContentItemContent">
                        1、新建表，点击【保存】后，平台会自动为表单增加以下字段，都是以下画线（_）开头<br />
                        <ul class="ulTR" id="tbldef_cyzd">
                            <li class="liTD wh50 liFirstTD">序号</li>
                            <li class="liTD wh150 liFirstTD">字段名</li>
                            <li class="liTD wh150 liFirstTD">类型</li>
                            <li class="liTD wh100 liFirstTD">范围</li>
                            <li class="liTD wh250 liFirstTD liLastTD">说明</li>
                        </ul>
                        <ul class="ulTR">
                            <li class="liTD wh50">1</li>
                            <li class="liTD wh150">_AutoID</li>
                            <li class="liTD wh150">varchar(50)</li>
                            <li class="liTD wh100">主表、子表</li>
                            <li class="liTD wh250 liLastTD">记录主键，自动生成 guid</li>
                        </ul>
                        <ul class="ulTR">
                            <li class="liTD wh50">2</li>
                            <li class="liTD wh150">_UserName</li>
                            <li class="liTD wh150">varchar(50)</li>
                            <li class="liTD wh100">主表、子表</li>
                            <li class="liTD wh250 liLastTD">记录创建人的ID</li>
                        </ul>
                        <ul class="ulTR">
                            <li class="liTD wh50">3</li>
                            <li class="liTD wh150">_OrgCode</li>
                            <li class="liTD wh150">varchar(50)</li>
                            <li class="liTD wh100">主表、子表</li>
                            <li class="liTD wh250 liLastTD">记录创建人所属部门WBS</li>
                        </ul>
                        <ul class="ulTR">
                            <li class="liTD wh50">4</li>
                            <li class="liTD wh150">_CreateTime</li>
                            <li class="liTD wh150">varchar(50)</li>
                            <li class="liTD wh100">主表、子表</li>
                            <li class="liTD wh250 liLastTD">记录创建时间</li>
                        </ul>
                        <ul class="ulTR">
                            <li class="liTD wh50">5</li>
                            <li class="liTD wh150">_UpdateTime</li>
                            <li class="liTD wh150">varchar(50)</li>
                            <li class="liTD wh100">主表、子表</li>
                            <li class="liTD wh250 liLastTD">记录最后修改时间</li>
                        </ul>
                        <ul class="ulTR">
                            <li class="liTD wh50">6</li>
                            <li class="liTD wh150">_IsDel</li>
                            <li class="liTD wh150">int</li>
                            <li class="liTD wh100">主表、子表</li>
                            <li class="liTD wh250 liLastTD">已删除标志位:0|1;未删除|已删除</li>
                        </ul>
                        <ul class="ulTR">
                            <li class="liTD wh50">7</li>
                            <li class="liTD wh150">_CompanyID</li>
                            <li class="liTD wh150">varchar(50)</li>
                            <li class="liTD wh100">主表</li>
                            <li class="liTD wh250 liLastTD">记录创建人所属公司ID</li>
                        </ul>
                        <ul class="ulTR">
                            <li class="liTD wh50">8</li>
                            <li class="liTD wh150">_WFState</li>
                            <li class="liTD wh150">varchar(50)</li>
                            <li class="liTD wh100">主表</li>
                            <li class="liTD wh250 liLastTD">流程审批状态字段:Null|处理中|完成</li>
                        </ul>
                        <ul class="ulTR">
                            <li class="liTD wh50">8</li>
                            <li class="liTD wh150">_GDState</li>
                            <li class="liTD wh150">varchar(50)</li>
                            <li class="liTD wh100">主表</li>
                            <li class="liTD wh250 liLastTD">归档状态字段</li>
                        </ul>
                        <ul class="ulTR">
                            <li class="liTD wh50">9</li>
                            <li class="liTD wh150">_WFCurNode</li>
                            <li class="liTD wh150">varchar(200)</li>
                            <li class="liTD wh100">主表</li>
                            <li class="liTD wh250 liLastTD">流程当前审批节点</li>
                        </ul>
                        <ul class="ulTR">
                            <li class="liTD wh50">10</li>
                            <li class="liTD wh150">_WFCurUser</li>
                            <li class="liTD wh150">varchar(200)</li>
                            <li class="liTD wh100">主表</li>
                            <li class="liTD wh250 liLastTD">流程当前审批人</li>
                        </ul>
                        <ul class="ulTR">
                            <li class="liTD wh50">11</li>
                            <li class="liTD wh150">_MainID</li>
                            <li class="liTD wh150">varchar(50)</li>
                            <li class="liTD wh100">子表</li>
                            <li class="liTD wh250 liLastTD">主表记录ID</li>
                        </ul>
                        <ul class="ulTR">
                            <li class="liTD wh50">12</li>
                            <li class="liTD wh150">_MainTbl</li>
                            <li class="liTD wh150">varchar(100)</li>
                            <li class="liTD wh100">子表</li>
                            <li class="liTD wh250 liLastTD">主表名称</li>
                        </ul>
                    </div>
                </div>

                <br />
                <div class="hlpContentItem" id="a5">
                    <div class="hlpContentItemTitle">字段定义</div>
                    <div class="hlpContentItemContent">
                        <div class="hlpImg FLDY01"></div>
                        1、字段类型：<br />
                        <div class="pl2">
                            1字符<br />
                            2整数<br />
                            3数值<br />
                            4日期<br />
                            5大文本<br />
                        </div>
                        2、选择【唯一】指定一个关键字段。<br />
                    </div>
                </div>
                <div class="hlpContentItem" id="a6">
                    <div class="hlpContentItemTitle">字段风格</div>
                    <div class="hlpContentItemContent">
                        <div class="hlpImg FLDY02"></div>
                    </div>
                </div>
                <div class="hlpContentItem" id="a7">
                    <div class="hlpContentItemTitle">列表属性</div>
                    <div class="hlpContentItemContent">
                        <ol>
                            <li>列表字段属性<div class="hlpImg FLDY03"></div></li>
                            <li>列表效果<div class="hlpImg FLDY04"></div></li>
                        </ol>
                    </div>
                </div>
                <div class="hlpContentItem" id="a8">
                    <div class="hlpContentItemTitle">界面设计</div>
                    <div class="hlpContentItemContent">
                        <ol>
                            <li>界面设计<div class="hlpImg YWDY10"></div></li>
                            <li>录入效果<div class="hlpImg YWDY11"></div></li>
                        </ol>
                    </div>
                </div>
                <div class="hlpContentItem" id="abdsj">
                    <div class="hlpContentItemTitle">表单事件</div>
                    <div class="hlpContentItemContent">
                        <table>
                            <tr>
                                <th>函数名</th>
                                <th>返回值</th>
                                <th>触发事件</th>
                                <th>一般用途</th>
                            </tr>
                            <tr>
                                <td>_sysBeforeSave()</td>
                                <td>Bool</td>
                                <td>新建、修改保存前</td>
                                <td>自定义验证函数</td>
                            </tr>
                            <tr>
                                <td>_sysAfterAdd()</td>
                                <td>Bool</td>
                                <td>新建保存后</td>
                                <td>调用后台业务逻辑</td>
                            </tr>
                            <tr>
                                <td>_sysAfterEdit()</td>
                                <td>Bool</td>
                                <td>修改保存后</td>
                                <td>调用后台业务逻辑</td>
                            </tr>
                            <tr>
                                <td>_子表名_AfterAdd</td>
                                <td></td>
                                <td>子表添加行后</td>
                                <td></td>
                            </tr>
                            <tr>
                                <td>_子表名_BeforeDel</td>
                                <td>Bool</td>
                                <td>子表删除行前</td>
                                <td></td>
                            </tr>
                            <tr>
                                <td>_子表名_AfterDel</td>
                                <td></td>
                                <td>子表删除行后</td>
                                <td></td>
                            </tr>
                        </table>
                    </div>
                </div>
                <div class="hlpContentItem" id="a9">
                    <div class="hlpContentItemTitle">字段事件</div>
                    <div class="hlpContentItemContent">
                        <ol>
                            <li>省份城市地区级联下拉，界面效果<div class="hlpImg YWDY12"></div></li>
                            <li>事件定义<div class="hlpImg YWDY13"></div></li>
                            <li>事件脚本<div class="hlpImg YWDY14"></div></li>
                        </ol>
                    </div>
                </div>
                <div class="hlpContentItem" id="a10">
                    <div class="hlpContentItemTitle">SQL逻辑</div>
                    <div class="hlpContentItemContent">
                        <ol>
                            <li>复制上年度保险基数，界面效果<div class="hlpImg YWDY15"></div></li>
                            <li>定义执行按钮<div class="hlpImg YWDY16"></div></li>
                            <li>定义执行SQL逻辑<div class="hlpImg YWDY17"></div></li>
                        </ol>
                    </div>
                </div>
                <div class="hlpContentItem" id="a11">
                    <div class="hlpContentItemTitle">业务逻辑</div>
                    <div class="hlpContentItemContent">
                        <ol>
                            <li>通过微信发送新闻提醒，界面效果<div class="hlpImg YWDY18"></div></li>
                            <li>定义业务逻辑<div class="hlpImg YWDY19"></div></li>
                            <li>定义业务逻辑<div class="hlpImg YWDY20"></div></li>
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
