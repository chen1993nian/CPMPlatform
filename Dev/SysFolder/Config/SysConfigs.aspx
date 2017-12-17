<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" CodeBehind="SysConfigs.aspx.cs" Inherits="EIS.Studio.SysFolder.Config.SysConfigs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>系统配置</title>
    <meta http-equiv="Pragma" content="no-cache" />
    <meta http-equiv="Content-Type" content="text/html;charset=utf-8" />
    <link type="text/css" rel="stylesheet" href="../../Css/appInput.css" />
    <script type="text/javascript" src="../../js/jquery.js"></script>
    <script type="text/javascript" src="../../js/jquery-1.4.2.min.js"></script>
    <script type="text/javascript" src="../../js/lhgdialog.min.js?s=default,chrome,"></script>
    <script src="../../DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script src="../../Js/jquery.cookie.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../js/tools.js"></script>
    <style type="text/css">
        body {
            background-color: white;
            overflow: auto;
        }

        table {
            table-layout: fixed;
            border-collapse: collapse;
            border: 1px solid gray;
        }

        td {
            height: 25px;
        }

        .TextBoxInArea {
            width: 98%;
        }

        .model {
            padding: 3px;
            background-color: #eee;
            margin-bottom: 2px;
        }

        .tip {
            border: dotted 1px orange;
            background: #F9FB91;
            text-align: left;
            padding: 5px;
            margin-top: 10px;
        }

        input[type=submit] {
            padding: 3px;
            height: 28px;
        }

        a {
            text-decoration: none;
            outline-style: none;
        }

        .tabs {
            padding-bottom: 0px;
            list-style-type: none !important;
            margin: 0px 0px 10px;
            padding-left: 0px;
            padding-right: 0px;
            zoom: 1;
            padding-top: 0px;
        }

            .tabs:before {
                display: inline;
                content: "";
            }

            .tabs:after {
                display: inline;
                content: "";
            }

            .tabs:after {
                clear: both;
            }

            .tabs li {
                padding-left: 5px;
                float: left;
            }

                .tabs li a {
                    display: block;
                }

        .tabs {
            border-bottom: #d0e1f0 1px solid;
            width: 100%;
            float: left;
        }

            .tabs li {
                position: relative;
                top: 1px;
            }

                .tabs li a {
                    border-bottom: #d0e1f0 1px solid;
                    border-left: #d0e1f0 1px solid;
                    padding-bottom: 0px;
                    line-height: 28px;
                    padding-left: 15px;
                    padding-right: 15px;
                    background: #e3edf7;
                    color: #666 !important;
                    border-top: #d0e1f0 1px solid;
                    margin-right: 2px;
                    border-right: #d0e1f0 1px solid;
                    padding-top: 0px;
                    border-radius: 4px 4px 0 0;
                    -webkit-border-radius: 4px 4px 0 0;
                    -moz-border-radius: 4px 4px 0 0;
                }

                    .tabs li a:hover {
                        background-color: #fff;
                        text-decoration: none;
                    }

                .tabs li.selected a {
                    border-bottom: transparent 1px solid;
                    border-left: #81b0da 1px solid;
                    background-color: #fff;
                    color: #000;
                    border-top: #81b0da 1px solid;
                    font-weight: bold;
                    border-right: #81b0da 1px solid;
                    _border-bottom-color: #fff;
                }

        #tabControl {
        }

        input[type=radio] {
            vertical-align: middle;
            cursor: pointer;
        }

        label {
            cursor: pointer;
        }

        .Wdate {
            width: 65px;
            line-height: 20px;
            padding-left: 3px;
        }

        .btnHelp {
            width: 68px;
            background-color: #9cf78b;
            border: 1px solid gray;
        }
    </style>
    <script type="text/javascript">
        jQuery(function () {
            $(".Wdate").focus(function () {
                WdatePicker({ dateFmt: 'HH:mm' });
            }).click(function () {
                WdatePicker({ dateFmt: 'HH:mm' });
            });
            $(".tabs>li").click(function () {
                var i = $(this).index();
                $("li.selected").removeClass("selected");
                jQuery.cookie("selTab", i);
                $(this).addClass("selected");
                $("#tabControl").children().hide();
                $("#tabControl").children(":eq(" + i + ")").show();

            });

            $(".btnHelp").click(function () {
                var url = $(this).attr("data-helpUrl");
                app_showHelp(url, "系统配置帮助");
            });

            var selTab = jQuery.cookie("selTab");
            if (selTab) {
                $(".tabs>li").eq(selTab).click();
            }
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
    <form id="form1" runat="server">
        <div id="maindiv" style="text-align: center; width: 760px;">
            <ul class="tabs">
                <li class="selected"><a href="#tabpage1">基本信息配置</a></li>
                <li><a href="#tabpage2">流程信息配置</a></li>
                <li><a href="#tabpage2">系统邮件配置</a></li>
                <li><a href="#tabpage3">目录服务配置</a></li>
                <li><a href="#tabpage4">自定义脚本引用</a></li>
            </ul>
            <div id="tabControl">
                <div>
                    <table class='normaltbl' style="width: 760px;" border="1" align="center">
                        <caption>
                            基本信息配置</caption>
                        <tbody>
                            <tr>
                                <td width="140">&nbsp;企业注册编号：
                                </td>
                                <td>
                                    <asp:TextBox CssClass="TextBoxInChar" ID="basic_CompanyCode" runat="server"></asp:TextBox>
                                </td>
                                <td width="140">&nbsp;系统运行环境：
                                </td>
                                <td>
                                    <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Selected="True"> 正式环境 </asp:ListItem>
                                        <asp:ListItem Value="2"> 测试环境 </asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>

                            </tr>
                            <tr>
                                <td>&nbsp;员工默认桌面：
                                </td>
                                <td>
                                    <asp:RadioButtonList ID="basic_DeskTop" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1"> 文字桌面 </asp:ListItem>
                                        <asp:ListItem Value="2" Selected="True"> 图标桌面 </asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                <td>&nbsp;员工初始化密码：
                                </td>
                                <td>
                                    <asp:TextBox CssClass="TextBoxInChar" ID="basic_EmployeeDefaultPass" runat="server"></asp:TextBox>
                                </td>

                            </tr>
                            <tr>
                                <td>&nbsp;静态资源获取方式：
                                </td>
                                <td>
                                    <asp:RadioButtonList ID="basic_ResMethod" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Selected="True"> 云加速服务 </asp:ListItem>
                                        <asp:ListItem Value="2"> 本地服务器 </asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>


                                <td>&nbsp;管理员初始化密码：
                                </td>
                                <td>
                                    <asp:TextBox CssClass="TextBoxInChar" ID="basic_AdminDefaultPass" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>&nbsp;登录验证码：</td>
                                <td>
                                    <asp:RadioButtonList ID="basic_VerifyCode_Show" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="否" Selected="True"> 禁用 </asp:ListItem>
                                        <asp:ListItem Value="是"> 字母数字混合 </asp:ListItem>
                                        <asp:ListItem Value="数字"> 纯数字 </asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>

                                <td>&nbsp;首次登录后修改密码：
                                </td>
                                <td>
                                    <asp:RadioButtonList ID="basic_ModifyPassFirstLogin" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="是"> 是 </asp:ListItem>
                                        <asp:ListItem Value="否" Selected="True"> 否 </asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>

                            <tr>
                                <td>&nbsp;管理员&nbsp;-&nbsp;手机：
                                </td>
                                <td>
                                    <asp:TextBox CssClass="TextBoxInChar" ID="txtAdminTel" runat="server"></asp:TextBox>
                                </td>
                                <td>&nbsp;测试人员&nbsp;-&nbsp;手机：
                                </td>
                                <td>
                                    <asp:TextBox CssClass="TextBoxInChar" ID="txtTestTel" runat="server"></asp:TextBox>
                                </td>
                            </tr>

                            <tr>
                                <td>&nbsp;管理员&nbsp;-&nbsp;邮箱：
                                </td>
                                <td>
                                    <asp:TextBox CssClass="TextBoxInChar" ID="txtAdminEmail" runat="server"></asp:TextBox>
                                </td>
                                <td>&nbsp;测试人员&nbsp;-&nbsp;邮箱：
                                </td>
                                <td>
                                    <asp:TextBox CssClass="TextBoxInChar" ID="txtTestEmail" runat="server"></asp:TextBox>
                                </td>
                            </tr>


                            <tr>
                                <td>&nbsp;刷新在线间隔（秒）：
                                </td>
                                <td>
                                    <asp:TextBox CssClass="TextBoxInChar" ID="basic_OnlineRefreshInterval" runat="server"></asp:TextBox>
                                </td>
                                <td>&nbsp;每天工作小时数：
                                </td>
                                <td>
                                    <asp:TextBox CssClass="TextBoxInChar" ID="basic_HoursOneDay" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>&nbsp;工作时间：
                                </td>
                                <td colspan="3">上午:
                                <asp:TextBox CssClass="Wdate" ID="basic_StartTime1" runat="server"></asp:TextBox>至
                                <asp:TextBox CssClass="Wdate" ID="basic_EndTime1" runat="server"></asp:TextBox>
                                    &nbsp;&nbsp;下午:
                                <asp:TextBox CssClass="Wdate" ID="basic_StartTime2" runat="server"></asp:TextBox>至
                                <asp:TextBox CssClass="Wdate" ID="basic_EndTime2" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>&nbsp;系统维护模式：
                                </td>
                                <td colspan="3">
                                    <asp:RadioButtonList ID="RadioButtonList2" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1"> 开启 </asp:ListItem>
                                        <asp:ListItem Value="2" Selected="True"> 关闭 </asp:ListItem>
                                    </asp:RadioButtonList>
                                    &nbsp;&nbsp;
                                <span style="color: Red;">状态开启时，禁止用户使用系统，登录时会自动转至提示页面。</span>
                                </td>

                            </tr>
                            <tr>
                                <td>&nbsp;系统维护提示语：</td>
                                <td colspan="3">
                                    <asp:TextBox CssClass="TextBoxInArea" TextMode="MultiLine" Rows="4" ID="TextBox5" runat="server"></asp:TextBox>

                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <div style="line-height: 48px; margin: 0 auto;">
                        <asp:Button ID="btnBasic" runat="server" Text="保存设置" OnClick="btnBasic_Click" />
                        <input type="button" value="帮助" id="Button5" class="btnHelp" data-helpUrl="Help/HelpSysConfigs.aspx#a1" />
                    </div>
                </div>
                <div class="hidden">
                    <table class='normaltbl' style="width: 760px;" border="1" align="center">
                        <caption>流程信息配置</caption>
                        <tbody>
                            <tr>
                                <td width="140">&nbsp;决策节点提交按钮名称：
                                </td>
                                <td>
                                    <asp:TextBox CssClass="TextBoxInChar" Width="160" ID="wf_WF_SubmitText" runat="server"></asp:TextBox>
                                </td>
                                <td>&nbsp;流程会签否决按钮名称：
                                </td>
                                <td>
                                    <asp:TextBox CssClass="TextBoxInChar" ID="wf_WF_SignRejectText" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>&nbsp;是否启用超时自动处理：</td>
                                <td>
                                    <asp:RadioButtonList ID="wf_OverTimeCheck" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="是"> 是 </asp:ListItem>
                                        <asp:ListItem Value="否" Selected="True"> 否 </asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                <td>&nbsp;超时检查间隔(分钟)：
                                </td>
                                <td>
                                    <asp:TextBox CssClass="TextBoxInChar" ID="wf_OverTimeInterval" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>&nbsp;审批意见强制同意</td>
                                <td>
                                    <asp:RadioButtonList ID="wf_EnforceAgree" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="是"> 是 </asp:ListItem>
                                        <asp:ListItem Value="否" Selected="True"> 否 </asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                <td></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td>&nbsp;流程会签单打印样式：
                                </td>
                                <td colspan="3">
                                    <asp:TextBox CssClass="TextBoxInArea" Rows="5" TextMode="MultiLine" ID="wf_WF_SignPrintStyle" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="color: red;">【到达通知】标题：</td>
                                <td colspan="3">
                                    <asp:TextBox CssClass="TextBoxInChar" ID="wf_ArriveTitle" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="color: red;">【到达通知】内容：
                                </td>
                                <td colspan="3">
                                    <asp:TextBox CssClass="TextBoxInArea" Rows="3" TextMode="MultiLine" ID="wf_ArriveMsg" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="color: Green;">【提交通知】标题：</td>
                                <td colspan="3">
                                    <asp:TextBox CssClass="TextBoxInChar" ID="wf_SubmitTitle" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="color: Green;">【提交通知】内容：
                                </td>
                                <td colspan="3">
                                    <asp:TextBox CssClass="TextBoxInArea" Rows="3" TextMode="MultiLine" ID="wf_SubmitMsg" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="color: blue;">【回退通知】标题：</td>
                                <td colspan="3">
                                    <asp:TextBox CssClass="TextBoxInChar" ID="wf_BackTitle" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="color: blue;">【回退通知】内容：
                                </td>
                                <td colspan="3">
                                    <asp:TextBox CssClass="TextBoxInArea" Rows="3" TextMode="MultiLine" ID="wf_BackMsg" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <div style="line-height: 48px; margin: 0 auto;">
                        <asp:Button ID="Button1" runat="server" Text=" 保存设置 " OnClick="btnWF_Click" />
                        <input type="button" value="帮助" id="Button4" class="btnHelp" data-helpUrl="Help/HelpSysConfigs.aspx#a2" />
                    </div>
                </div>
                <div class="hidden">
                    <table class='normaltbl' style="width: 760px;" border="1" align="center">
                        <caption>
                            系统邮件配置</caption>
                        <tbody>
                            <tr>
                                <td width="140">&nbsp;发送服务器(SMTP)：
                                </td>
                                <td>
                                    <asp:TextBox CssClass="TextBoxInChar" ID="mail_Server" runat="server"></asp:TextBox>
                                </td>
                                <td width="140">&nbsp;端口(Port)：
                                </td>
                                <td>
                                    <asp:TextBox CssClass="TextBoxInChar" ID="mail_Port" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>&nbsp;使用加密连接（SSL）：
                                </td>
                                <td>
                                    <asp:RadioButtonList ID="mail_SSL" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="是"> 是 </asp:ListItem>
                                        <asp:ListItem Value="否" Selected="True"> 否 </asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                <td>&nbsp;发送方式：
                                </td>
                                <td>
                                    <asp:RadioButtonList ID="mail_Async" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="否" Selected="True"> 同步 </asp:ListItem>
                                        <asp:ListItem Value="是"> 异步 </asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                            <tr>
                                <td>&nbsp;登录帐户：
                                </td>
                                <td>
                                    <asp:TextBox CssClass="TextBoxInChar" ID="mail_Account" runat="server"></asp:TextBox>
                                </td>
                                <td>&nbsp;登录密码：
                                </td>
                                <td>
                                    <asp:TextBox CssClass="TextBoxInChar" ToolTip="保存后显示为空，如需修改请输入新密码！" ID="mail_PassWord" TextMode="Password" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>&nbsp;是否启用：
                                </td>
                                <td>
                                    <asp:RadioButtonList ID="mail_Enable" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="启用" Selected="True"> 启用 </asp:ListItem>
                                        <asp:ListItem Value="禁用"> 禁用 </asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                <td>&nbsp;帐户昵称：
                                </td>
                                <td>
                                    <asp:TextBox CssClass="TextBoxInChar" ID="mail_NiCheng" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>&nbsp;查看链接自动登录：
                                </td>
                                <td>
                                    <asp:RadioButtonList ID="basic_ReloginLinkInMail" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="是"> 是 </asp:ListItem>
                                        <asp:ListItem Value="否" Selected="True"> 否 </asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                <td>&nbsp;测试用接收邮箱：
                                </td>
                                <td>
                                    <asp:TextBox CssClass="TextBoxInChar" ID="mail_TestAccount" runat="server"></asp:TextBox>
                                </td>

                            </tr>

                            <tr>
                                <td>&nbsp;邮件内容后缀：
                                </td>
                                <td colspan="3">
                                    <asp:TextBox CssClass="TextBoxInArea" Rows="4" TextMode="MultiLine" ID="mail_Subffix" runat="server"></asp:TextBox>
                                </td>
                            </tr>

                        </tbody>
                    </table>
                    <div style="line-height: 48px; margin: 0 auto;">
                        <asp:Button ID="btnMail" runat="server" Text="保存设置" OnClick="btnMail_Click" />
                        <asp:Button ID="btnTest" runat="server" Text="发送测试邮件" OnClick="btnTest_Click" />
                        <input type="button" value="帮助" id="Button3" class="btnHelp" data-helpUrl="Help/HelpSysConfigs.aspx#a3" />
                    </div>
                </div>
                <div class="hidden">
                    <table class='normaltbl' style="width: 760px;" border="1" align="center">
                        <caption>
                            目录服务配置</caption>
                        <tbody>
                            <tr>
                                <td width="140">&nbsp;目录服务器地址：
                                </td>
                                <td>
                                    <asp:TextBox CssClass="TextBoxInChar" ID="ds_ServerIP" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>&nbsp;目录服务器端口：
                                </td>
                                <td>
                                    <asp:TextBox CssClass="TextBoxInChar" ID="ds_ServerPort" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>&nbsp;是否启用目录服务：
                                </td>
                                <td>
                                    <asp:RadioButtonList ID="ds_State" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="是"> 是 </asp:ListItem>
                                        <asp:ListItem Value="否" Selected="True"> 否 </asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                            <tr>
                                <td>&nbsp;目录根路径：
                                </td>
                                <td>
                                    <asp:TextBox CssClass="TextBoxInChar" ID="ds_Root" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>&nbsp;目录服务管理账号：
                                </td>
                                <td>
                                    <asp:TextBox CssClass="TextBoxInChar" ID="ds_Account" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>&nbsp;目录服务管理密码：
                                </td>
                                <td>
                                    <asp:TextBox CssClass="TextBoxInChar" ID="ds_Pass" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <div style="line-height: 48px; margin: 0 auto;">
                        <asp:Button ID="btnDs" runat="server" Text="保存设置" OnClick="btnDs_Click" />
                        <input type="button" value="帮助" id="Button2" class="btnHelp" data-helpUrl="Help/HelpSysConfigs.aspx#a4" />
                    </div>
                </div>
                <div class="hidden">
                    <table class='normaltbl' style="width: 760px;" border="1" align="center">
                        <caption>自定义脚本及样式表文件引用路径</caption>
                        <tbody>
                            <tr>
                                <td width="140">&nbsp;主页面脚本<br />
                                    &nbsp;<span style="color: Green;">DefaultMain.aspx</span>
                                </td>
                                <td>
                                    <asp:TextBox CssClass="TextBoxInArea" Rows="3" TextMode="MultiLine" ID="ref_DefaultMain" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>&nbsp;增删改列表页面<br />
                                    &nbsp;<span style="color: Green;">AppDefault.aspx</span>
                                </td>
                                <td>
                                    <asp:TextBox CssClass="TextBoxInArea" Width="500" ID="ref_AppDefault" Rows="3" TextMode="MultiLine" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>&nbsp;查询列表页面<br />
                                    &nbsp;<span style="color: Green;">AppQuery.aspx</span>
                                </td>
                                <td>
                                    <asp:TextBox CssClass="TextBoxInArea" Width="500" ID="ref_AppQuery" Rows="3" TextMode="MultiLine" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>&nbsp;平台编辑页面<br />
                                    &nbsp;<span style="color: Green;">AppInput.aspx</span>
                                </td>
                                <td>
                                    <asp:TextBox CssClass="TextBoxInArea" Rows="3" TextMode="MultiLine" ID="ref_AppInput" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>&nbsp;平台查看页面<br />
                                    &nbsp;<span style="color: Green;">AppDetail.aspx</span>
                                </td>
                                <td>
                                    <asp:TextBox CssClass="TextBoxInArea" Rows="3" TextMode="MultiLine" ID="ref_AppDetail" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>&nbsp;平台打印页面<br />
                                    &nbsp;<span style="color: Green;">AppPrint.aspx</span>
                                </td>
                                <td>
                                    <asp:TextBox CssClass="TextBoxInArea" Rows="3" TextMode="MultiLine" ID="ref_AppPrint" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="color: Red;">&nbsp;说明：可以在上面配置自定义脚本（script）或者样式表（css）文件的路径（从根路径算起）。
                                <br />
                                    &nbsp;示例：Js/MyScript.js、Css/CustomStyle.css（每行一个文件）
                                </td>
                            </tr>

                        </tbody>
                    </table>
                    <div style="line-height: 48px; margin: 0 auto;">
                        <asp:Button ID="btnRef" runat="server" Text=" 保存设置 " OnClick="btnRef_Click" />
                        <input type="button" value="帮助" id="btnHelp5" class="btnHelp" data-helpUrl="Help/HelpSysConfigs.aspx#a5" />
                    </div>
                </div>
            </div>
            <%=sbMsg %>
        </div>
    </form>
</body>
</html>
