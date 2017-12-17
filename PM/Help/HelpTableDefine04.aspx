<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HelpTableDefine04.aspx.cs" Inherits="CEIM.WebHelp.Help.HelpTableDefine04" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>业务表单的应用</title>
    <link rel="stylesheet" type="text/css" href="Imgs/HelpStyle1.css" />
    <script type="text/javascript" src="../js/jquery-1.7.js"></script>
    <script type="text/javascript" src="../js/jquery-1.8.0.min.js"></script>
    <script type="text/javascript" src="../js/layer/layer.js"></script>
    <script type="text/javascript" src="Imgs/helpmain.js"></script>
    <style>
        .BDYY401 {
            background-image: url(Imgs/Help_Table_BDYY401.png);
            height: 442px;
            width: 797px;
        }

        .BDYY402 {
            background-image: url(Imgs/Help_Table_BDYY402.png);
            height: 633px;
            width: 733px;
        }

        .BDYY403 {
            background-image: url(Imgs/Help_Table_BDYY403.png);
            height: 633px;
            width: 730px;
        }

        .BDYY404 {
            background-image: url(Imgs/Help_Table_BDYY404.png);
            height: 508px;
            width: 731px;
        }

        .BDYY405 {
            background-image: url(Imgs/Help_Table_BDYY405.png);
            height: 942px;
            width: 785px;
        }

        .BDYY406 {
            background-image: url(Imgs/Help_Table_BDYY406.png);
            height: 422px;
            width: 801px;
        }

        .BDYY407 {
            background-image: url(Imgs/Help_Table_BDYY407.png);
            height: 422px;
            width: 801px;
        }

    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="hlpMain">
            <div class="hlpContent">
                <div class="hlpContentItem" id="a1">
                    <div class="hlpContentItemTitle">列表页面</div>
                    <div class="hlpContentItemContent">
                        <div class="hlpImg BDYY401"></div>
                    </div>
                </div>
                <div class="hlpContentItem">
                    <div class="hlpContentItemTitle">页面参数说明</div>
                    <div class="hlpContentItemContent">
                        1、页面链接：SysFolder/AppFrame/AppDefault.aspx。<br />
                        2、例如：SysFolder/AppFrame/AppDefault.aspx?
                        <b>tblName</b>=T_SG_Contract_LaborSubInfo
                        &<b>cpro</b>=projectname=建筑云小区^1|projectcode=JZY20170101003^1|projectid=6D037AF8-FF09-4FA7-8A57-6549105C0BA0^1
                        &<b>Condition</b>=projectid='6D037AF8-FF09-4FA7-8A57-6549105C0BA0' and compid='6D09F6B6-517E-4CCF-9A80-4FD3E5C3E423'
                        &<b>Sindex</b>=1&<b>Defaultvalue</b>=@year=2017
                        &<b>Replacevalue</b>=@year=2017<br />
                        3、参数说明：<br />
                        <div class="pl2">
                            3.1、tblName = 表名<br />
                            3.2、Condition  = 条件<br />
                            <div class="pl2">
                                条件格式：[字段名称1]='[值1]' and [字段名称2]='[!Session值名称!]'<br />
                            </div>
                            3.3、Sindex = 样式索引<br />
                            3.4、cpro = 默认值参数<br />
                            <div class="pl2">
                                参数格式：[字段名称1]=[值1]^0|[字段名称2]=[值2]^1|[字段名称3]=[值3]^2|[字段名称3]=[值]^1<br />
                                参数格式：[字段名称4]=[!Session值名称1!]^1|[字段名称5]=[!Session值名称2!]^1<br />
                                0：隐藏；1：只读；2：录入。<br />
                            </div>
                            3.5、Defaultvalue  = SQL替换<br />
                            <div class="pl2">
                                参数格式：@[参数名称1]=[值1]|@[参数名称2]=[值2]|[参数名称3]=[值3]<br />
                                参数格式：@[参数名称4]=[!Session值名称1!]<br />
                            </div>
                            3.6、Replacevalue  = 内容替<br />
                            <div class="pl2">
                                参数格式：@[参数名称1]=[值1]|@[参数名称2]=[值2]|[参数名称3]=[值3]<br />
                                参数格式：@[参数名称4]=[!Session值名称1!]<br />
                            </div>
                        </div>
                        4、列表页面，所有参数必须加密。上述例子加密后：<br />
                        SysFolder/AppFrame/AppList.aspx?para=NJH5J2XanzmCdpueo7POF7DvsZcs9g4ww...<br />
                    </div>
                </div>
                <div class="hlpContentItem">
                    <div class="hlpContentItemTitle">页面开发说明</div>
                    <div class="hlpContentItemContent">
                        <ol>
                            <li><a href="HelpTableDefine05.aspx#a1" target="_self">列表页面对象：_curClass</a></li>
                            <li><a href="HelpTableDefine05.aspx#a1" target="_self">表格对象：$("#flex1")</a></li>
                            <li><a href="HelpTableDefine05.aspx#a1" target="_self">表格对象（flexigrid）常用方法。</a></li>
                            <li><a href="HelpTableDefine05.aspx#a1" target="_self">列表页面工具条。</a></li>
                            <li><a href="HelpTableDefine05.aspx#a1" target="_self">当前选择的行对象：$('.trSelected',grid)</a></li>
                            <li><a href="HelpTableDefine05.aspx#a1" target="_self">渲染函数中的行对象：row。</a></li>
                            <li><a href="HelpTableDefine05.aspx#a1" target="_self">flexigrid开发举例。</a></li>
                        </ol>
                    </div>
                </div>
                <div class="hlpContentItem">
                    <div class="hlpContentItemTitle">页面应用举例</div>
                    <div class="hlpContentItemContent">
                        <ol>
                            <li><a href="HelpTableDefine05.aspx#a1" target="_self">依据条件过滤列表数据。</a></li>
                            <li><a href="HelpTableDefine05.aspx#a2" target="_self">依据参数过滤列表数据。</a></li>
                            <li><a href="HelpTableDefine05.aspx#a3" target="_self">替换列表样式中的内容。</a></li>
                            <li><a href="HelpTableDefine05.aspx#a4" target="_self">在工具条上添加自定义按钮。</a></li>
                            <li><a href="HelpTableDefine05.aspx#a5" target="_self">在列表中添加单选、复选列、按钮。</a></li>
                            <li><a href="HelpTableDefine05.aspx#a6" target="_self">隐藏归档状态列、流程状态列。</a></li>
                            <li><a href="HelpTableDefine05.aspx#a7" target="_self">指定排序字段。</a></li>
                            <li><a href="HelpTableDefine05.aspx#a8" target="_self">显示汇总合计行。</a></li>
                            <li><a href="HelpTableDefine05.aspx#a9" target="_self">数字01转化汉字男女显示。</a></li>
                            <li><a href="HelpTableDefine05.aspx#a10" target="_self">渲染函数、渲染日期格式。</a></li>
                        </ol>
                    </div>
                </div>
                <div class="hlpContentItem" id="a2">
                    <div class="hlpContentItemTitle">录入编辑页面</div>
                    <div class="hlpContentItemContent">
                        <div class="hlpImg BDYY402"></div>
                    </div>
                </div>
                <div class="hlpContentItem">
                    <div class="hlpContentItemTitle">页面参数说明</div>
                    <div class="hlpContentItemContent">
                        1、页面链接：SysFolder/AppFrame/AppInput.aspx。<br />
                        2、例如：SysFolder/AppFrame/AppInput.aspx?
                        <b>tblName</b>=T_SG_Contract_LaborSubInfo
                        &<b>cpro</b>=projectname=建筑云小区^1|projectcode=JZY20170101003^1|projectid=6D037AF8-FF09-4FA7-8A57-6549105C0BA0^1
                        &<b>Condition</b>=_AutoID='6D09F6B6-517E-4CCF-9A80-4FD3E5C3E423'
                        &<b>Sindex</b>=1&<b>Defaultvalue</b>=@year=2017
                        &<b>Replacevalue</b>=@year=2017<br />
                        3、参数说明：<br />
                        <div class="pl2">
                            3.1、tblName = 表名<br />
                            3.2、Condition  = 条件<br />
                            <div class="pl2">
                                条件格式：[字段名称1]='[值1]' and [字段名称2]='[!Session值名称!]'<br />
                            </div>
                            3.3、Sindex = 样式索引<br />
                            3.4、cpro = 默认值参数<br />
                            <div class="pl2">
                                参数格式：[字段名称1]=[值1]^0|[字段名称2]=[值2]^1|[字段名称3]=[值3]^2|[字段名称3]=[值]^1<br />
                                参数格式：[字段名称4]=[!Session值名称1!]^1|[字段名称5]=[!Session值名称2!]^1<br />
                                0：隐藏；1：只读；2：录入。<br />
                            </div>
                            3.5、Defaultvalue  = SQL替换<br />
                            <div class="pl2">
                                参数格式：@[参数名称1]=[值1]|@[参数名称2]=[值2]|[参数名称3]=[值3]<br />
                                参数格式：@[参数名称4]=[!Session值名称1!]<br />
                            </div>
                            3.6、Replacevalue  = 内容替<br />
                            <div class="pl2">
                                参数格式：@[参数名称1]=[值1]|@[参数名称2]=[值2]|[参数名称3]=[值3]<br />
                                参数格式：@[参数名称4]=[!Session值名称1!]<br />
                            </div>
                        </div>
                        4、录入编辑页面，所有参数必须加密。<br />
                    </div>
                </div>
                <div class="hlpContentItem">
                    <div class="hlpContentItemTitle">页面开发说明</div>
                    <div class="hlpContentItemContent">
                        <ol>
                            <li><a href="HelpTableDefine06.aspx#a1" target="_self">页面对象：_curClass。</a></li>
                            <li><a href="HelpTableDefine06.aspx#a2" target="_self">页面全局变量。</a></li>
                            <li><a href="HelpTableDefine06.aspx#a3" target="_self">页面脚本事件。</a></li>
                            <li><a href="HelpTableDefine06.aspx#a4" target="_self">页面脚本函数。</a></li>
                        </ol>
                    </div>
                </div>
                <div class="hlpContentItem">
                    <div class="hlpContentItemTitle">页面应用举例</div>
                    <div class="hlpContentItemContent">
                        <ol>
                            <li><a href="HelpTableDefine06.aspx#a5" target="_self">数量单价金额合计大小写。</a></li>
                            <li><a href="HelpTableDefine06.aspx#a6" target="_self">选择材料类别自动添加材料明细。</a></li>
                            <li><a href="HelpTableDefine06.aspx#a7" target="_self">省份城市地区级联下拉选择。</a></li>
                            <li><a href="HelpTableDefine06.aspx#a8" target="_self">设置字段默认值和只读属性。</a></li>
                            <li><a href="HelpTableDefine06.aspx#a9" target="_self">替换录入样式中的内容。</a></li>
                            <li><a href="HelpTableDefine06.aspx#a10" target="_self">指定条件，并进入编辑页面。</a></li>
                            <li><a href="HelpTableDefine06.aspx#a11" target="_self">大文本编辑页面。</a></li>
                            <li><a href="HelpTableDefine06.aspx#a12" target="_self">HTML编辑页面。</a></li>
                            <li><a href="HelpTableDefine06.aspx#a13" target="_self">多附件上传页面。</a></li>
                            <li><a href="HelpTableDefine06.aspx#a14" target="_self">Word文档编辑页面。</a></li>
                            <li><a href="HelpTableDefine06.aspx#a15" target="_self">加载指定的Word文档，进入编辑页面。</a></li>
                        </ol>
                    </div>
                </div>
                <div class="hlpContentItem" id="a3">
                    <div class="hlpContentItemTitle">详细页面</div>
                    <div class="hlpContentItemContent">
                        <div class="hlpImg BDYY404"></div>
                    </div>
                </div>
                <div class="hlpContentItem">
                    <div class="hlpContentItemTitle">页面参数说明</div>
                    <div class="hlpContentItemContent">
                        1、页面链接：SysFolder/AppFrame/AppDetail.aspx。<br />
                        2、例如：SysFolder/AppFrame/AppDetail.aspx?
                        <b>tblName</b>=T_SG_Contract_LaborSubInfo
                        &<b>Condition</b>=_AutoID='6D09F6B6-517E-4CCF-9A80-4FD3E5C3E423'
                        &<b>Sindex</b>=1&<b>Defaultvalue</b>=@year=2017
                        &<b>Replacevalue</b>=@year=2017<br />
                        3、参数说明：<br />
                        <div class="pl2">
                            3.1、tblName = 表名<br />
                            3.2、Condition  = 条件<br />
                            <div class="pl2">
                                条件格式：[字段名称1]='[值1]' and [字段名称2]='[!Session值名称!]'<br />
                            </div>
                            3.3、Sindex = 样式索引<br />
                            3.4、Defaultvalue  = SQL替换<br />
                            <div class="pl2">
                                参数格式：@[参数名称1]=[值1]|@[参数名称2]=[值2]|[参数名称3]=[值3]<br />
                                参数格式：@[参数名称4]=[!Session值名称1!]<br />
                            </div>
                            3.5、Replacevalue  = 内容替<br />
                            <div class="pl2">
                                参数格式：@[参数名称1]=[值1]|@[参数名称2]=[值2]|[参数名称3]=[值3]<br />
                                参数格式：@[参数名称4]=[!Session值名称1!]<br />
                            </div>
                        </div>
                        4、详细页面，所有参数必须加密。<br />
                    </div>
                </div>
                <div class="hlpContentItem">
                    <div class="hlpContentItemTitle">页面参数说明</div>
                    <div class="hlpContentItemContent">
                        1、页面全局变量
                        <ol>
                            <li>获取当前表单名称：_mainTblName</li>
                            <li>获取当前记录_AutoID：_mainId</li>
                        </ol>
                    </div>
                </div>
                <div class="hlpContentItem">
                    <div class="hlpContentItemTitle">页面开发说明</div>
                    <div class="hlpContentItemContent">
                        <ol>
                            <li>建设中...</li>
                            <li>建设中...</li>
                            <li>建设中...</li>
                            <li>建设中...</li>
                            <li>建设中...</li>
                        </ol>
                    </div>
                </div>
                <div class="hlpContentItem">
                    <div class="hlpContentItemTitle">页面应用举例</div>
                    <div class="hlpContentItemContent">
                        <ol>
                            <li>建设中...</li>
                            <li>建设中...</li>
                            <li>建设中...</li>
                            <li>建设中...</li>
                            <li>建设中...</li>
                        </ol>
                    </div>
                </div>

                <div class="hlpContentItem" id="a4">
                    <div class="hlpContentItemTitle">流程发起页面</div>
                    <div class="hlpContentItemContent">
                        <div class="hlpImg BDYY405"></div>
                    </div>
                </div>
                <div class="hlpContentItem">
                    <div class="hlpContentItemTitle">页面参数说明</div>
                    <div class="hlpContentItemContent">
                        1、页面链接：SysFolder/AppFrame/AppWorkFlowInfo.aspx。<br />
                        2、例如：SysFolder/AppFrame/AppWorkFlowInfo.aspx?
                        <b>tblName</b>=T_SG_Contract_LaborSubInfo
                        &<b>mainId</b>=6D037AF8-FF09-4FA7-8A57-6549105C0BA0
                        &<b>InstanceId</b>=B13C63A0-C598-44F4-ADEF-FDC395EE0676
                        3、参数说明：<br />
                        <div class="pl2">
                            3.1、tblName = 表名<br />
                            3.2、mainId  = 记录_AutoID<br />
                            3.3、InstanceId  = 流程实例ID<br />
                        </div>
                        4、流程发起页面，所有参数必须加密。上述例子加密后：<br />
                    </div>
                </div>
                <div class="hlpContentItem">
                    <div class="hlpContentItemTitle">页面开发说明</div>
                    <div class="hlpContentItemContent">
                        <ol>
                            <li>建设中...</li>
                            <li>建设中...</li>
                            <li>建设中...</li>
                            <li>建设中...</li>
                            <li>建设中...</li>
                        </ol>
                    </div>
                </div>
                <div class="hlpContentItem">
                    <div class="hlpContentItemTitle">页面应用举例</div>
                    <div class="hlpContentItemContent">
                        <ol>
                            <li>建设中...</li>
                            <li>建设中...</li>
                            <li>建设中...</li>
                            <li>建设中...</li>
                            <li>建设中...</li>
                        </ol>
                    </div>
                </div>
                <div class="hlpContentItem" id="a5">
                    <div class="hlpContentItemTitle">查询列表页面</div>
                    <div class="hlpContentItemContent">
                        <div class="hlpImg BDYY407"></div>
                    </div>
                </div>
                <div class="hlpContentItem">
                    <div class="hlpContentItemTitle">页面参数说明</div>
                    <div class="hlpContentItemContent">
                        1、列表页面：SysFolder/AppFrame/AppQuery.aspx。<br />
                        2、例如：SysFolder/AppFrame/AppQuery.aspx?
                        <b>tblName</b>=T_SG_Contract_LaborSubInfo
                        &<b>Condition</b>=projectid='6D037AF8-FF09-4FA7-8A57-6549105C0BA0' and compid='6D09F6B6-517E-4CCF-9A80-4FD3E5C3E423'
                        &<b>Sindex</b>=1&<b>Defaultvalue</b>=@year=2017
                        &<b>Replacevalue</b>=@year=2017<br />
                        3、参数说明：<br />
                        <div class="pl2">
                            3.1、tblName = 表名<br />
                            3.2、Condition  = 条件<br />
                            <div class="pl2">
                                条件格式：[字段名称1]='[值1]' and [字段名称2]='[!Session值名称!]'<br />
                            </div>
                            3.3、Sindex = 样式索引<br />
                            3.4、Defaultvalue  = SQL替换<br />
                            <div class="pl2">
                                参数格式：@[参数名称1]=[值1]|@[参数名称2]=[值2]|[参数名称3]=[值3]<br />
                                参数格式：@[参数名称4]=[!Session值名称1!]<br />
                            </div>
                            3.5、Replacevalue  = 内容替<br />
                            <div class="pl2">
                                参数格式：@[参数名称1]=[值1]|@[参数名称2]=[值2]|[参数名称3]=[值3]<br />
                                参数格式：@[参数名称4]=[!Session值名称1!]<br />
                            </div>
                        </div>
                        4、查询列表页面，所有参数必须加密。<br />
                    </div>
                </div>
                <div class="hlpContentItem">
                    <div class="hlpContentItemTitle">页面开发说明</div>
                    <div class="hlpContentItemContent">
                        <ol>
                            <li><a href="HelpTableDefine05.aspx#a1" target="_self">页面对象：_curClass</a></li>
                            <li><a href="HelpTableDefine05.aspx#a1" target="_self">表格对象：$("#flex1")</a></li>
                            <li><a href="HelpTableDefine05.aspx#a1" target="_self">表格对象（flexigrid）常用方法。</a></li>
                            <li><a href="HelpTableDefine05.aspx#a1" target="_self">列表页面工具条。</a></li>
                            <li><a href="HelpTableDefine05.aspx#a1" target="_self">当前选择的行对象：$('.trSelected',grid)</a></li>
                            <li><a href="HelpTableDefine05.aspx#a1" target="_self">渲染函数中的行对象：row。</a></li>
                        </ol>
                    </div>
                </div>
                <div class="hlpContentItem">
                    <div class="hlpContentItemTitle">页面应用举例</div>
                    <div class="hlpContentItemContent">
                        <ol>
                            <li><a href="HelpTableDefine05.aspx#a1" target="_self">依据条件过滤列表数据。</a></li>
                            <li><a href="HelpTableDefine05.aspx#a2" target="_self">依据参数过滤列表数据。</a></li>
                            <li><a href="HelpTableDefine05.aspx#a3" target="_self">替换列表样式中的内容。</a></li>
                            <li><a href="HelpTableDefine05.aspx#a4" target="_self">添加按钮在工具条。</a></li>
                            <li><a href="HelpTableDefine05.aspx#a5" target="_self">在列表中添加单选、复选列、按钮。</a></li>
                            <li><a href="HelpTableDefine05.aspx#a7" target="_self">指定排序字段。</a></li>
                            <li><a href="HelpTableDefine05.aspx#a8" target="_self">显示汇总合计行。</a></li>
                            <li><a href="HelpTableDefine05.aspx#a9" target="_self">渲染函数、渲染日期格式。</a></li>
                            <li><a href="HelpTableDefine05.aspx#a10" target="_self">数字01转化汉字男女显示。</a></li>
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
