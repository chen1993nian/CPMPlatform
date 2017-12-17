<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrgIO_DeptImport.aspx.cs" Inherits=" EIS.Web.SysFolder.Permission.OrgIO_DeptImport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>组织机构导入</title>
    <meta http-equiv="Pragma" content="no-cache"/>
    <meta http-equiv="Content-Type" content="text/html;charset=utf-8"/>
    <link rel="stylesheet" type="text/css" href="../../css/defStyle.css"/>
    <script type="text/javascript" src="../../js/jquery-1.4.2.min.js"></script>
    <style type="text/css">
        body{background-color:#f9fafe;overflow:auto;}
        #maindiv table
        {
            table-layout:fixed;
            border-collapse:collapse;
            border:0px solid gray;
            width:660px;
            }
        #maindiv td{border:0px solid gray;padding:5px;}
        table.innerTbl
        {
            border-collapse:collapse;
            border:1px solid gray;
            }
        .innerTbl th,.innerTbl td
        {
            border:1px solid gray;padding:2px;
            }
        .innerTbl th{
            background:#eee url(../../img/toolbar.gif) repeat-x center center;
            height:26px;
            }
        .model
        {
            padding:3px;
            background-color:#eee;
            margin-bottom:2px;
            }
         .tip{border:dotted 1px orange;background:#F9FB91;text-align:left;padding:5px;margin-top:10px;}
         input[type=submit]{padding:2px 5px;font-weight:bold;height:30px;border:1px solid gray;color:#444;}
         label{cursor:pointer;font-size:11pt;line-height:180%;color:Red;}
         .innerTbl .exist{color:black;background:#FF7F50;}
         .step{font:11pt/180% 宋体;color:Red;}
         a{color:Blue;}
         #errorInfo{background:#f9fb91 url(../../img/info.gif) no-repeat 5px 5px;padding-left:30px;line-height:20px;}
    </style>
    <script type="text/javascript">
        jQuery(function () {
            jQuery("#chkAll").click(function () {
                jQuery(".chkitem").attr("checked", this.checked);
            });
            jQuery(".chkitem").attr("title", "导入");
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="maindiv" style="text-align:center;padding-top:40px;margin-left:auto;margin-right:auto;">
        <table align="center" style="text-align:left;">
            <caption style="font:bold 12pt/200% 'Microsoft YaHei';border-bottom:2px solid #3598db;padding-bottom:10px;">组织机构导入</caption>
            <tr>
                <td style="width:60px;" valign="top">
                    <span class="step">步骤1、</span>
                </td>
                <td style="line-height:180%;font-size:11pt;"> 
                    下载组织机构模板文件，按照模板内要求，整理组织机构数据。点击后面链接下载模板
                    <a href="tmpl/01.组织机构.xls" target="_blank">【组织机构.xls】</a>


                </td>
            </tr>
            <tr>
                <td  valign="top">
                    <span class="step">步骤2、</span>
                </td>
                <td style="line-height:180%;font-size:11pt;">点击下面的【选择文件】，上传整理好的数据文件（目前只接收.xls格式，高版本的Excel文件选择另存为.xls）
                    <div style="line-height:28px;color:Green;border:1px solid #ddd;padding-bottom:10px;">
                    <iframe id="multiattach_12" frameborder="0" scrolling="auto"  
                    src="../Common/FileListFrame.aspx?set=*.xls||0&appName=OrgIO_DeptImport&appId=OrgIO_DeptImport&read=0&fromDoc=0" width="100%" height="80px">
                    </iframe>
                    </div>
                </td>
            </tr>
            <tr>
                <td  valign="top">
                    <span class="step">步骤3、</span>
                </td>
                <td>
                <asp:Button ID="Button1" runat="server" Text="开始导入" onclick="Button1_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:CheckBox ID="CheckBox1" runat="server" Text="导入之前清空所有组织机构数据（部门、人员、岗位）" />
                    <div class="tip">说明：如果需要重新导入，请选择【导入之前清空所有组织机构数据】，否则不要勾选。</div>
                    <div>
                    <asp:TextBox ID="TextBox1" Width="100%" Height="80" runat="server" TextMode="MultiLine"></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <%=TipMessage %>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
