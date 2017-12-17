<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GovProjectDataLimitProjectList.aspx.cs" Inherits="EIS.WorkAsp.DataLimit.GovProjectDataLimitProjectList" %>

<!DOCTYPE html>

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
                url: 'GovProjectDataLimitProjectListXML.ashx',
                dataType: 'xml',
                params: [{ name: "DataLimit_Type", value: "<%=DataLimit_Type %>" }
                    , { name: "DataLimit_Value", value: "<%=DataLimit_Value %>" }
                ],
                colModel: [
			    { display: '选择', name: 'Project_Limit', width: 30, sortable: false, align: 'center', renderer: colLimit }
                , { display: '工程名称', name: 'projectname', width: 240, sortable: true, align: 'left', hide: false, renderer: false, fieldid: '7619fa25-9bcf-4220-8d16-b3d3a576ac57' }
                , { display: '工程编号', name: 'projectcode', width: 120, sortable: true, align: 'left', hide: false, renderer: false, fieldid: 'bb4c7057-e2b3-4688-b2bb-95cf5430457c' }
                , { display: '工程ID', name: 'projectid', width: 80, sortable: true, align: 'left', hide: true, renderer: false, fieldid: 'c31b6e61-d494-4661-b7c3-340b401ab601' }
                , { display: '工程简称', name: 'projectjc', width: 80, sortable: true, align: 'left', hide: false, renderer: false, fieldid: 'fdfd3e31-37ff-4aac-9642-8363c9ba3675' }
                , { display: '工程性质', name: 'PropertyName', width: 80, sortable: true, align: 'left', hide: false, renderer: false, fieldid: '96e48e58-4dfd-456a-b413-6b6220687e67' }
                , { display: '所属公司名称', name: 'CompanyName', width: 80, sortable: true, align: 'left', hide: false, renderer: false, fieldid: '4951b200-0fa4-4c81-a260-27777ada06fa' }
                , { display: '总包分包分类', name: 'ContractorTypeName', width: 80, sortable: true, align: 'left', hide: false, renderer: false, fieldid: 'ea86c5f1-fbe9-4cc7-bdb0-95a4dac2e65a' }
                , { display: '所属经济片区', name: 'RegionNameCN', width: 80, sortable: true, align: 'left', hide: false, renderer: false, fieldid: '4925b6c9-52db-43a3-830e-66efa7516975' }
                , { display: '工程类别', name: 'CategoryName', width: 80, sortable: true, align: 'left', hide: false, renderer: false, fieldid: '8b049206-8ded-4920-8e4f-e11d9ae50077' }
                , { display: '所属专业', name: 'ProfessionalName', width: 80, sortable: true, align: 'left', hide: false, renderer: false, fieldid: '514f9ef7-2df7-4d92-ad9b-366494eac969' }
                , { display: '投资性质', name: 'InvestmentPropName', width: 80, sortable: true, align: 'left', hide: false, renderer: false, fieldid: '7a82c9c5-4fcf-4875-ab3b-edaae360675b' }
                , { display: '结构类型', name: 'StructureTypeName', width: 80, sortable: true, align: 'left', hide: false, renderer: false, fieldid: '573e2fa3-89c5-459c-bc0a-392988a3f03a' }
                , { display: '层数', name: 'NumberFloors', width: 80, sortable: true, align: 'left', hide: false, renderer: false, fieldid: '0d298d09-02fc-45d2-92ce-967200938729' }
                , { display: '工程状态', name: 'ProjectStatusName', width: 80, sortable: true, align: 'left', hide: false, renderer: false, fieldid: '56950f12-86a8-45c1-829a-a5cee7843b19' }
                , { display: '招标类型', name: 'BiddingTypeName', width: 80, sortable: true, align: 'left', hide: false, renderer: false, fieldid: '69f9cac4-e1b7-4138-80d9-15f4cbb274f2' }
                ],
                sortname: "",
                sortorder: "",
                usepager: true,
                useRp: true,
                rp: 15,
                multisel: false,
                showTableToggleBtn: false,
                resizable: false,
                height: 300,
                searchitems: [
                    { display: '工程名称', name: 'a.ProjectName', type: 1, defvalue: '' }
                ],
                buttons: [
                    { name: '查询', bclass: 'view', onpress: app_query_prj }
                ],
                preProcess: false
            });
        });

        function colLimit(fldval, row) {
            var ProjectID = $("projectid", row).text();
            var projectname = $("projectname", row).text();
            return "<INPUT TYPE=\"checkbox\" " + fldval + "  onclick=\"ChangeProjectLimit('" + projectname + "','" + ProjectID + "',this)\">";
        }
        function ChangeProjectLimit(projectname, projectid, objchk) {
            var fldname = document.getElementById("HiddenField1").value;
            var fldvalue = document.getElementById("HiddenField2").value;
            var IsChecked = "0";
            if (objchk.checked) IsChecked = "1";
            var rest = EIS.WorkAsp.DataLimit.GovProjectDataLimitProjectList.SaveCheckProject(fldname, fldvalue, projectid, IsChecked).value;
            if (objchk.checked)
                window.status = "已授予该项目权限！" + projectname;
            else
                window.status = "已取消该项目权限！" + projectname;
        }

        function app_query_prj(cmd, grid) {
            $("#flexme1").flexReload();
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
