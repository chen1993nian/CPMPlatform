<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DaEdit.aspx.cs" Inherits="Studio.JZY.Doc.DaEdit" %>

<!DOCTYPE html>


<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>归档</title>
    <link href="../ligerui/lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" />
    <script src="../ligerui/lib/jquery/jquery-1.5.2.min.js" type="text/javascript"></script>
    <script src="../ligerui/lib/ligerUI/js/core/base.js" type="text/javascript"></script>
    <script src="../ligerui/lib/ligerUI/js/plugins/ligerLayout.js" type="text/javascript"></script>
    <script src="../ligerui/lib/ligerUI/js/plugins/ligerDialog.js" type="text/javascript"></script>
    <script src="../ligerui/lib/ligerUI/js/plugins/ligerResizable.js" type="text/javascript"></script>
    <script src="../ligerui/lib/ligerUI/js/plugins/ligerAccordion.js" type="text/javascript"></script>

    <style type="text/css">
        body {
            padding: 1px;
            margin: 0;
        }

        #layout1 {
            width: 100%;
            margin: 2;
            padding: 0;
        }
        li {
            padding-top:5px;
            padding-bottom:5px;
            margin-left:10px;
        }
    </style>
    <script type="text/javascript" src="DaEdit.js"></script>
    <script type="text/javascript">
  
    </script>


</head>
<body>
    <form id="form1" runat="server">
        <div id="layout1">
            <div position="center" title="业务明细">
                <iframe frameborder="0" height="350px;" width="100%" name="frm1" id="frm1" src="" />
                </iframe>
            </div>
            <div position="right" title="编制档案信息">
                <div id="accordion1">
                    <div title="基础信息">
                        <ul>
                            <li>文&nbsp;&nbsp;&nbsp;&nbsp;号：<asp:TextBox ID="txtWH" runat="server"></asp:TextBox></li>
                            <li>题&nbsp;&nbsp;&nbsp;&nbsp;名：<asp:TextBox ID="txtBT" runat="server"></asp:TextBox></li>
                            <li>副&nbsp;题&nbsp;名：<asp:TextBox ID="txtFBT" runat="server"></asp:TextBox></li>
                            <li>年&nbsp;&nbsp;&nbsp;&nbsp;度：<asp:TextBox ID="txtND" runat="server"></asp:TextBox></li>
                            <li>机&nbsp;构&nbsp;号：<asp:TextBox ID="txtJGH" runat="server"></asp:TextBox></li>
                            <li>责&nbsp;任&nbsp;人：<asp:TextBox ID="txtZRR" runat="server" ReadOnly="true"></asp:TextBox></li>
                            <li>部门名称：<asp:TextBox ID="txtBMMC" runat="server" ReadOnly="true"></asp:TextBox></li>
                            <li>公司名称：<asp:TextBox ID="txtGSMC" runat="server" ReadOnly="true"></asp:TextBox></li>
                        </ul>
                    </div>
                    <div title="存放位置">
                        <ul>
                            <li>存储位置：<asp:TextBox ID="txtPos" runat="server"></asp:TextBox><asp:HiddenField ID="hidPos" runat="server" /></li>
                            <li>自定义分类：<asp:TextBox ID="txtCtl" runat="server"></asp:TextBox><asp:HiddenField ID="hidCtl" runat="server" /></li>
                            <li>国标分类：<asp:TextBox ID="txtGB" runat="server"></asp:TextBox><asp:HiddenField ID="hidGB" runat="server" /></li>
                            <li>重大项目分类：<asp:TextBox ID="txtZD" runat="server"></asp:TextBox><asp:HiddenField ID="hidZD" runat="server" /></li>
                            <li>项目WBS分类：<asp:TextBox ID="txtWBS" runat="server"></asp:TextBox><asp:HiddenField ID="hidWBS" runat="server" /></li>
                            <li>安全资料分类：<asp:TextBox ID="txtAQ" runat="server"></asp:TextBox><asp:HiddenField ID="hidAQ" runat="server" /></li>
                            <li>质量资料分类：<asp:TextBox ID="txtZL" runat="server"></asp:TextBox><asp:HiddenField ID="hidZL" runat="server" /></li>
                            <li>技术资料分类：<asp:TextBox ID="txtJS" runat="server"></asp:TextBox><asp:HiddenField ID="hidJS" runat="server" /></li>
                            <li>所属项目：<asp:TextBox ID="txtProjectName" runat="server"></asp:TextBox><asp:HiddenField ID="hidProjectID" runat="server" /><asp:HiddenField ID="hidProjectCode" runat="server" /></li>
                        </ul>
                    </div>
                    <div title="备注说明" style="padding: 10px">
                        <ul>
                            <li>关键字：</li>
                            <li><asp:TextBox ID="txtKeyWord" runat="server" TextMode="MultiLine" Rows="5" Width="300"></asp:TextBox></li>
                            <li>备注：</li>
                            <li><asp:TextBox ID="txtReMark" runat="server" TextMode="MultiLine" Rows="5" Width="300"></asp:TextBox></li>
                        </ul>
                    </div>
                </div>
            </div>
            <div position="bottom">
                <div style="text-align: right;">
                    <input type="button" value="保存" style="width: 180px;" onclick="gdSave();" />&nbsp;&nbsp;
                </div>
            </div>
        </div>
        <asp:HiddenField ID="hidTableName" runat="server" />
        <asp:HiddenField ID="hidAppID" runat="server" />
        <asp:HiddenField ID="hidInstanceID" runat="server" />
        <asp:HiddenField ID="hidWorkflowID" runat="server" />
    </form>
</body>
</html>
