<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebLimitWebList.aspx.cs" Inherits="EIS.WorkAsp.RelationTree.WebLimitWebList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
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
                url: 'WebLimitWebListXML.ashx',
                params: [{ name: "DataLimit_Type", value: "<%=DataLimit_Type %>" }
                    , { name: "DataLimit_Value", value: "<%=DataLimit_Value %>" }
                ],
                dataType: 'xml',
                colModel: [
			    { display: '选择', name: 'Web_Limit', width: 30, sortable: false, align: 'center', renderer: colLimit }
                , { display: '产品编号', name: 'WebId', width: 80, sortable: true, align: 'left', hide: false, renderer: false, fieldid: '7619fa25-9bcf-4220-8d16-b3d3a576ac57' }
                , { display: '产品名称', name: 'WebName', width: 180, sortable: true, align: 'left', hide: false, renderer: false, fieldid: 'bb4c7057-e2b3-4688-b2bb-95cf5430457c' }
                ],
                sortname: "",
                sortorder: "",
                usepager: true,
                useRp: true,
                rp: 15,
                multisel: false,
                showTableToggleBtn: false,
                resizable: false,
                height: 360,
                preProcess: false
            });
        });

        function colLimit(fldval, row) {
            var ProjectID = row.selectNodes("WebId")[0].text;
            var projectname = row.selectNodes("WebName")[0].text;
            return "<INPUT TYPE=\"checkbox\" " + fldval + "  onclick=\"ChangeProjectLimit('" + projectname + "','" + ProjectID + "',this)\">";
        }
        function ChangeProjectLimit(projectname, projectid, objchk) {
            var fldname = document.getElementById("HiddenField1").value;
            var fldvalue = document.getElementById("HiddenField2").value;
            var IsChecked = "0";
            if (objchk.checked) IsChecked = "1";
            var rest = EIS.WorkAsp.RelationTree.WebLimitWebList.SaveCheckProject(fldname, fldvalue, projectid, projectname, IsChecked).value;
            if (objchk.checked)
                window.status = "已授予该产品权限！" + projectname;
            else
                window.status = "已取消该产品权限！" + projectname;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="griddiv" >
        <table id="flexme1" style="display:none"></table>
    </div>
    <asp:HiddenField ID="HiddenField1" runat="server" />
    <asp:HiddenField ID="HiddenField2" runat="server" />
    </form>
</body>
</html>
