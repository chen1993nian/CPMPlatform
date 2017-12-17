<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ModelExport.aspx.cs" Inherits="EIS.Studio.SysFolder.DefFrame.ModelExport" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>业务模型导出</title>
    <meta http-equiv="Pragma" content="no-cache"/>
    <meta http-equiv="Content-Type" content="text/html;charset=utf-8"/>
    <link rel="stylesheet" type="text/css" href="../../css/appStyle.css"/>
    <script type="text/javascript" src="../../js/jquery-1.4.2.min.js"></script>
    <style type="text/css">
        body{background-color:#f9fafe;}
        table.outerTbl
        {
            table-layout:fixed;
            border-collapse:collapse;
            border:0px solid gray;
            }
        table.innerTbl
        {
            border-collapse:collapse;
            border:1px solid gray;
            }
        .innerTbl th,.innerTbl td
        {
            border:1px solid gray;padding:2px;height:22px;background-color:White;
            }
        .innerTbl th{
            background:#eee url(../../img/toolbar.gif) repeat-x center center;
            height:26px;
            }
        table.outerTbl>tbody>tr>td{border:0px solid gray;padding:5px;}
        .center{text-align:center;}
        .model
        {
            padding:3px;
            background-color:#eee;
            margin-bottom:2px;
            }
         .tip{border:dotted 1px orange;background:#F9FB91;text-align:left;padding:5px;margin-top:10px;}
    </style>
    <script type="text/javascript">
        jQuery(function () {
            jQuery("#chkAll").click(function () {
                jQuery(".chkitem").attr("checked", this.checked);
            });
            jQuery("#btnExport").click(function () {

                var arr = [];
                $(".chkTbl:checked").each(function (i, o) {
                    var dict = $(".chkDict[value=" + this.value + "]").attr("checked");
                    var data = $(".chkData[value=" + this.value + "]").attr("checked");
                    arr.push(this.value + "|" + dict + "|" + data);
                });

                $("#tblName").val(arr.join(","));
                jQuery("#frmExport").submit();
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="maindiv" style="text-align:left;">
        <br /><br />
        <table width="600" class="outerTbl" align="center">
            <caption style="font-weight:bold;font-size:12pt;line-height:30px;">业务模型导出设置</caption>
            <tr>
                <td>
                    <table class="innerTbl" width="100%">
                        <colgroup>
                            <col width="40" align="center"/>
                            <col align="left"/>
                            <col width="80" align="center"/>
                            <col width="80" align="center"/>
                        </colgroup>
                        <thead>
                            <tr>
                                <th >选择</th>
                                <th >业务对象</th>
                                <th >导出关联对象</th>
                                <th >导出数据</th>
                            </tr>
                        </thead>
                        <tbody>
                        <%=tblHtml %>
                        
                        </tbody>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <%=TipMessage %>                    
                   <div style="padding:3px;">
                        <div style="line-height:30px;color:Green;">
                        <button  type="button" id="btnExport" class="imgbutton"><img alt="增加子节点" src="../../img/common/add.png" />开始导出&nbsp;</button>
                        </div>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    </form>
    <form style="display:none;" id="frmExport" action="ModelExportResponse.aspx" method="post" target="_blank">
        <input id="tblName" name="tblName" value=""/>
    </form>
</body>
</html>
