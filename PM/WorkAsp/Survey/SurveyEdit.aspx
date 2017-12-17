<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SurveyEdit.aspx.cs" Inherits="EIS.Web.WorkAsp.Survey.SurveyEdit" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>编辑问题信息</title>
    <meta http-equiv="Pragma" content="no-cache"/>
    <link rel="stylesheet" type="text/css" href="../../Css/Password.css" />
    <script type="text/javascript" src="../../js/jquery-1.7.min.js"></script>
    <script type="text/javascript" src="../../js/jquery.notice.js"></script>
    <script type="text/javascript" src="../../js/tools.js"></script>
    <style type="text/css">
        body{overflow:hidden;padding:20px;margin:0px;}
     	.maintbl
	    {
	        border:#c3daf9 1px solid;
	        background:white;
	        border-collapse:collapse;
	        width:500px;
	    }
	    .maintbl caption{
	        background:#f9fafe url(../../img/common/layout.png) no-repeat 10px center;
	        border:#c3daf9 1px solid;
	        padding-left:30px;
	        color:#38a0cd;
	        text-align:left;
	        font:bold 14px/36px "Microsoft YaHei",宋体;
	        }
	    .maintbl>tbody>tr>td{
	        border:1px solid #c3daf9;
	        }
	    .maintbl>tfoot{
	        border-top:#c3daf9 2px solid;
	        height:40px;	        
	        }
	    .titleTd{text-align:right;width:100px;font:normal 13px/30px "Microsoft YaHei",宋体;color:#444;}
        input[type=radio]{vertical-align:middle;cursor:pointer;}
        input[type=text]{padding:2px;line-height:22px;height:20px;font-size:12px;color:#444;border:1px solid #bbb;}
        textarea{padding:2px;line-height:20px;font-size:12px;color:#444;border:1px solid #bbb;}
        label{cursor:pointer;}
    </style>
        <script type="text/javascript">
            function addNode(info) {
                var arr = info.split("|");
                parent.frames['left'].addNode(arr);
            }
            function changeNode(info) {
                var arr = info.split("|");
                parent.frames['left'].changeNode(arr);
            }
            function removeNode(nodeId) {
                parent.frames['left'].removeNode(nodeId);
            }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div style="padding:30px;">
    <table class="maintbl">
    <caption>调查问题信息</caption>
    <tbody>
    <tr>
        <td height="25" class="titleTd">调查问题：</td>
        <td>
            <asp:TextBox ID="QTitle" runat="server" Width="400" CssClass="textbox"></asp:TextBox></td>
    </tr>
     <tr>
        <td height="25" class="titleTd">问题类型：</td>
        <td>
            <table width="100%">
                <tr>
                    <td width="200">            
                        <asp:RadioButtonList ID="QType" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Text="单选" Value="单选" Selected="True"/>
                            <asp:ListItem Text="多选" Value="多选"/>
                            <asp:ListItem Text="输入" Value="输入"/>
                        </asp:RadioButtonList>
                    </td>
                    <td>
                        <asp:CheckBox ID="CheckBox1" runat="server" Text="显示其它"/>
                    </td>
                </tr>
            </table>

        </td>
    </tr>
     <tr>
        <td height="25" class="titleTd">选项列表：</td>
        <td>
            <asp:TextBox ID="QSelect" runat="server" TextMode="MultiLine" Rows="6" Width="99%" CssClass="textbox"></asp:TextBox>
            <div style="padding:3px;line-height:25px;color:red;">
                每行代表一个选项
            </div>
            </td>
    </tr>
    <tr>
        <td height="25" class="titleTd">必 选 题：</td>
        <td>
            <asp:RadioButtonList ID="QMustSel" runat="server" RepeatDirection="Horizontal">
                <asp:ListItem Text="必选" Value="1"/>
                <asp:ListItem Text="非必选" Value="0" Selected="True"/>
            </asp:RadioButtonList>
        </td>
    </tr>
        <tr>
        <td height="25" class="titleTd">排 序：</td>
        <td>
            <asp:TextBox ID="QOrder" runat="server" CssClass="textbox"></asp:TextBox>
            <span style="padding:3px;line-height:25px;color:red;">在同一问卷中不能重复</span>
            </td>
    </tr>
    </tbody>
    <tfoot style="background-color:#f4f9ff;">
        <tr>
        <td>
        </td>
        <td>
        <br />
            <asp:Button ID="Button2" runat="server" Text="保 存" Width="80px" OnClick="Button2_Click" />
            <asp:Button ID="Button1" runat="server" Text="继续添加问题" Width="100px" OnClick="Button1_Click" />
            </td>
    </tr>
    </tfoot>
    </table>
    </div>
    </form>
</body>
</html>
