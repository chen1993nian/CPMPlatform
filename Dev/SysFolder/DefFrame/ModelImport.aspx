<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ModelImport.aspx.cs" Inherits="EIS.Studio.SysFolder.DefFrame.ModelImport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>业务模型导入</title>
    <meta http-equiv="Pragma" content="no-cache"/>
    <meta http-equiv="Content-Type" content="text/html;charset=utf-8"/>
    <link rel="stylesheet" type="text/css" href="../../css/defStyle.css"/>
    <script type="text/javascript" src="../../js/jquery-1.4.2.min.js"></script>
    <style type="text/css">
        body{background-color:white;overflow:auto;}
        table
        {
            table-layout:fixed;
            border-collapse:collapse;
            border:0px solid gray;
            width:580px;
            }
        td{border:0px solid gray;padding:5px;}
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
         input[type=submit]{padding:2px;color:Green;font-weight:bold;}
         input[type=file]{padding:2px;}
         .innerTbl .exist{color:black;background:#FF7F50;}
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
    <div id="maindiv" style="text-align:left;">
        <table width="600" align="center">
            <caption style="font-weight:bold;font-size:12pt;line-height:30px;">业务模型导入</caption>
            <tr>
                <td> 
                    <div style="line-height:28px;color:Green;border-bottom:1px solid gray;padding-bottom:10px;">请先选择导出的模型文件，然后点击下面的【上传】
                    <br />
                    <asp:FileUpload ID="FileUpload1" Width="300px" runat="server" />&nbsp;&nbsp;
                    <asp:Button ID="Button1" runat="server" Text=" 上 传 " onclick="Button1_Click" />&nbsp;&nbsp;
                    <asp:Button ID="Button2" runat="server" Text="开始导入" onclick="Button2_Click" />

                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <%=TipMessage %>
                    <div style="<%=view%>">
                        <table class="innerTbl">
                        <colgroup>
                            <col  align="left"/>
                            <col width="60" align="center"/>
                            <col width="80" align="center"/>
                            <col width="100" align="center"/>
                        </colgroup>
                        <thead>
                            <tr>
                                <th>&nbsp;&nbsp;对象名称</th>
                                <th >类型</th>
                                <th >模型定义</th>
                                <th >物理表</th>
                            </tr>
                        </thead>
                        <tbody>
                        <%=tblHtml %>
                        </tbody>
                        </table>

                    </div>
                    <div class="tip">说明：如果对应物理表已经存在记录，导入操作不会重建物理表，只会把新增字段更新到物理表</div>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
