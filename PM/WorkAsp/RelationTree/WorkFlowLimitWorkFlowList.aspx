<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WorkFlowLimitWorkFlowList.aspx.cs" Inherits="EIS.WorkAsp.RelationTree.WorkFlowLimitWorkFlowList" %>

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
                url: 'WorkFlowLimitWorkFlowListXML.ashx',
                params: [{ name: "DataLimit_Type", value: "<%=DataLimit_Type %>" }
                    , { name: "DataLimit_Value", value: "<%=DataLimit_Value %>" }
                ],
                dataType: 'xml',
                colModel: [
			    { display: '选择', name: 'Workflow_Limit', width: 30, sortable: false, align: 'center', renderer: colLimit }
                , { display: '流程名称', name: 'WorkflowName', width: 280, sortable: true, align: 'left', hide: false, renderer: showTreeRow, fieldid: 'bb4c7057-e2b3-4688-b2bb-95cf5430457c' }
                , { display: '流程ID', name: 'WorkflowId', width: 240, sortable: true, align: 'left', hide: false, renderer: false, fieldid: '7619fa25-9bcf-4220-8d16-b3d3a576ac57' }
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

        function showTreeRow(fldval, row) {
            var str_img = row.selectNodes("img")[0].text;
            str_img = str_img.replace(/<w>/g, "<img src='../../Img/treeimages/white.gif'>");
            str_img = str_img.replace(/<i>/g, "<img src='../../Img/treeimages/i.gif'>");
            str_img = str_img.replace(/<Tp>/g, "<img src='../../Img/treeimages/Tminus.gif'><img src='../../Img/treeimages/dept.gif'>");
            str_img = str_img.replace(/<Lp>/g, "<img src='../../Img/treeimages/Lminus.gif'><img src='../../Img/treeimages/dept.gif'>");
            str_img = str_img.replace(/<T>/g, "<img src='../../Img/treeimages/T.gif'><img src='../../Img/treeimages/CostAccount.gif'>");
            str_img = str_img.replace(/<L>/g, "<img src='../../Img/treeimages/L.gif'><img src='../../Img/treeimages/CostAccount.gif'>");
            str_img = str_img + fldval;
            return (str_img);
        }

        function colLimit(fldval, row) {
            var ProjectID = row.selectNodes("WorkflowId")[0].text;
            var projectname = row.selectNodes("WorkflowName")[0].text;
            return "<INPUT TYPE=\"checkbox\" " + fldval + "  onclick=\"ChangeProjectLimit('" + projectname + "','" + ProjectID + "',this)\">";
        }
        function ChangeProjectLimit(projectname, projectid, objchk) {
            var fldname = document.getElementById("HiddenField1").value;
            var fldvalue = document.getElementById("HiddenField2").value;
            var IsChecked = "0";
            if (objchk.checked) IsChecked = "1";
            var rest = EIS.WorkAsp.RelationTree.WorkFlowLimitWorkFlowList.SaveCheckProject(fldname, fldvalue, projectid, projectname, IsChecked).value;
            if (objchk.checked)
                window.status = "已授予该流程权限！" + projectname;
            else
                window.status = "已取消该流程权限！" + projectname;
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
