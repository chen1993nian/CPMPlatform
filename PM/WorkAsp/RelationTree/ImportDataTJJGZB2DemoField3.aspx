<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ImportDataTJJGZB2DemoField3.aspx.cs" Inherits="EIS.Web.ImportDataTJJGZB.ImportDataTJJGZB2DemoField3" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css" href="../../grid/css/flexigrid.css">
    <link rel="stylesheet" type="text/css" href="../../Css/DefStyle.css">
    <link rel="stylesheet" type="text/css" href="../../Css/datePicker.css">
    <link rel="stylesheet" type="text/css" href="../../Css/ymPrompt_vista/ymPrompt.css">
    
    <script type="text/javascript" src="../../grid/lib/jquery/jquery.js"></script>
    <script type="text/javascript" src="../../grid/flexigrid.js"></script>
    <script type="text/javascript" src="../../js/jquery.datePicker-min.js"></script> 
    <script type="text/javascript" src="../../js/ymPrompt.js"></script> 
    <style type="text/css">
        html
        {
            color: #4d4d4d;
        }
        body
        {
            font: 12px tahoma, arial, 宋体;
            padding: 10px;
            margin: 0px;
        }
        .record-tit-link
        {
            line-height: 30px;
            width: 300px;
            vertical-align: middle;
        }
        .record-tit-link a
        {
            text-decoration: none;
            color: #08c;
        }
    </style>

    <script type="text/javascript">
        $("document").ready(function () {
            $('#flexme1').flexigrid({
                url: 'ImportDataTJJGZB2DemoField3XML.ashx',
                params: [{ name: "TableName", value: "<%=TableName %>" }
                    ,{ name: "DbName", value: "<%=DbName %>" }
                ],
                dataType: 'xml',
                colModel: [
                    <%=Field_Script %>
                ],
                sortname: "",
                sortorder: "",
                usepager: true,
                useRp: true,
                rp: 500,
                multisel: false,
                showTableToggleBtn: false,
                resizable: false,
                height: 360,
                preProcess: false
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="griddiv" >
        <table id="flexme1" style="display:none"></table>    
    </div>
    </form>
</body>
</html>
