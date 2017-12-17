<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DefPivotGrid.aspx.cs" Inherits="EIS.Studio.SysFolder.DefFrame.DefPivotGrid" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>多维分析报表定义</title>
    <link rel="stylesheet" type="text/css" href="../../Css/appStyle.css" />
    <link type="text/css" href="../../Css/jquery-ui/lightness/jquery-ui-1.7.2.custom.css" rel="stylesheet" />
	<script type="text/javascript" src="../../../js/jquery-1.8.0.min.js"></script>
	<script type="text/javascript" src="../../../js/jquery-ui-1.8.23.custom.min.js"></script>
    <style type="text/css">
        ul.connectedSortable, ul.connectedSortable2 {
            list-style-type: none;
            margin: 0;
            padding: 0;
            background: #eee;
            padding: 3px;
            width: 120px;
        }
        .connectedSortable li, .connectedSortable2 li {
            margin: 0 5px 2px 5px;
            padding: 2px;
            font-size: 1.0em;
            width: 100px;
            text-align: center;
        }
            table.frametbl
    {
	    border-collapse: collapse;
        margin-left:auto;
	    margin-right:auto;
	    font-size: 12px;
	    line-height:20px;

	    border:#808080 1px solid;
	    color:#393939;
	    background:#FAF8F8;
    }
    table.frametbl td{padding:5px;}
    </style>
    <script type="text/javascript">
        $(function () {
            $("#tablefld, #a1,#a2,#a3,#a4").sortable({
                connectWith: '.connectedSortable'
                , placeholder: 'ui-state-highlight'
                , dropOnEmpty: true
            }).disableSelection();

        });
        function save() {
            var a1 = $('#a1').sortable('toArray');
            var a2 = $('#a2').sortable('toArray');
            var a3 = $('#a3').sortable('toArray');
            var a4 = $('#a4').sortable('toArray');
            var _curClass = EIS.Studio.SysFolder.DefFrame.DefPivotGrid;
            var ret = _curClass.saveQuery("<%=tblName %>", a1.join(","), a2.join(","), a3.join(","), a4.join(","));
	    if (ret.error) {
	        alert("保存出错：" + ret.error.Message);
	    }
	    else {
	        alert("保存成功！");

	    }

    }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div class="menubar">
        <div class="topnav">
            <ul>
                <li><a href="javascript:" onclick="">预览</a> </li>
                <li><a href="javascript:" onclick="save();">保存</a>&nbsp;&nbsp;&nbsp;&nbsp;</li>
            </ul>
            </div>
        </div>
        <br />
        <table align="center" class="frametbl" width="96%" border="1">
            <tr>
                <th align="center" height="30">
                    表单字段
                </th>
                <th align="center">
                    过滤字段
                </th>
                <th align="center">
                    行字段
                </th>
                <th align="center">
                    列字段
                </th>
                <th align="center">
                    数据字段
                </th>
            </tr>
            <tr>
                <td width="25%" valign="top" align="center">
                    <ul id="tablefld" class="connectedSortable">
                        <%=sbFieldList %>
                    </ul>
                </td>
                <td width="25%" valign="top" align="center">
                    <ul id="a1" class="connectedSortable">
                        <%=sbFilterArea %>
                    </ul>
                </td>
                <td width="25%" valign="top" align="center">
                    <ul id="a2" class="connectedSortable">
                        <%=sbRowArea%>
                    </ul>
                </td>
                <td valign="top" align="center">
                    <ul id="a3" class="connectedSortable">
                        <%=sbColumnArea%>
                    </ul>
                </td>
                <td valign="top" align="center">
                    <ul id="a4" class="connectedSortable">
                        <%=sbDataArea%>
                    </ul>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
