<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DefTableFrame.aspx.cs" Inherits="EIS.Studio.SysFolder.DefFrame.DefTableFrame" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>【<%=tblNameCn %>】业务定义</title>
    <meta http-equiv="X-UA-Compatible" content="IE=Edge" />    
    <link rel="stylesheet" type="text/css" href="../../css/layout-default-latest.css" />
    <link href="../../css/tree.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="../../css/DefStyle.css" />
    <style type="text/css">
        .ui-layout-north, .ui-layout-center, /* has content-div */ .ui-layout-west, /* has Accordion */ .ui-layout-east, /* has content-div ... */ .ui-layout-east .ui-layout-content
        {
            /* content-div has Accordion */
            padding: 0px;
            margin: 0px;
            overflow: hidden;
            background-color: #f5f5f5;
        }
        .ui-layout-mask
        {
            opacity: 0.2 !important;
            filter: alpha(opacity=20) !important;
            background-color: #666 !important;
        }
        #topCaption, #topCaption2
        {
            padding-left: 20px;
        }
        #menuTree
        {
            border: #c3daf9 1px solid;
            padding: 5px;
            margin: 5px;
        }
    </style>
    <script type="text/javascript" src="../../js/jquery-1.4.2.min.js"></script>
    <script type="text/javascript" src="../../js/jquery-ui-1.7.2.js"></script>
    <script type="text/javascript" src="../../js/jquery.layout.js"></script>
    <script type="text/javascript">

        var myLayout; // init global vars

        $(document).ready(function () {
            //if (parent.myLayout)
            //    parent.myLayout.close("west");
            myLayout = $('body').layout({
                maskIframesOnResize: ".ui-layout-west,#mainframe2",
                west__size: 160,
                closable: true
            });
            //var para = "para=" + EIS.Studio.SysFolder.DefFrame.DefTableFrame.CryptPara("tblname=<%=tblName %>&sindex=").value;
            var o = {
                showcheck: false,
                onnodeclick: menuClick,
                blankpath: "../../Img/common/",
                cbiconpath: "../../Img/common/"
            };
            if ("<%=t %>" == "1") {
                o.data = [
                {
                    id: "0", text: "功能定义", value: "", complete: true, isexpand: true, "hasChildren": true, "ChildNodes": [
                      { id: "0a", text: "基本信息", value: "DefBizEdit.aspx?tblname=<%=tblName %>&sindex=&t=<%=t %>", complete: true, isexpand: true },
                    { id: "00", text: "字段定义", value: "DefTableFields.aspx?tblname=<%=tblName %>&sindex=&t=<%=t %>", complete: true, isexpand: true },
                    { id: "01", text: "字段风格", value: "DefFieldsEditStyle.aspx?tblname=<%=tblName %>&sindex=&t=<%=t %>", complete: true, isexpand: true },
                    { id: "02", text: "列表属性", value: "DefFieldsAttr.aspx?tblname=<%=tblName %>&sindex=&t=<%=t %>", complete: true, isexpand: true },
                    //{ id: "0b", text: "查询条件", value: "DefFieldsQuery.aspx?tblname=<%=tblName %>&sindex=&t=<%=t %>", complete: true, isexpand: true },
                    { id: "03", text: "界面设计", value: "DefTableStyleList.aspx?tblname=<%=tblName %>&sindex=&t=<%=t %>", complete: true, isexpand: true },
                    { id: "05", text: "字段事件", value: "DefFieldsEvent.aspx?tblname=<%=tblName %>&sindex=&t=<%=t %>", complete: true, isexpand: true },
                    { id: "06", text: "SQL逻辑", value: "DefTableScriptList.aspx?tblName=T_E_Sys_TableScript&bizName=<%=tblName %>&t=<%=t %>", complete: true, isexpand: true },
                    { id: "07", text: "业务逻辑", value: "../AppFrame/AppDefault.aspx?tblname=T_E_Sys_TableDll&Condition=TableName=[QUOTES]<%=tblName %>[QUOTES]&cpro=TableName=<%=tblName %>^1&ext=700|400", complete: true, isexpand: true },
                    //{ id: "07", text: "主键外键", value: "DefTableRelationKey.aspx?tblname=<%=tblName %>&sindex=&t=<%=t %>", complete: true, isexpand: true },
                    { id: "04", text: "脚本编辑", value: "DefTableScript.aspx?tblname=<%=tblName %>&sindex=&t=1", complete: true, isexpand: true },
                    { id: "08", text: "业务文档", value: "DefBizDoc.aspx?tblname=<%=tblName %>&sindex=&t=1", complete: true, isexpand: true },
                    { id: "09", text: "子表定义", value: "DefBizList2.aspx?parent=<%=tblName %>", complete: true, isexpand: true }
                    //{ id: "10", text: "后台脚本", value: "DefTableScriptList.aspx?tblName=T_E_Sys_TableScript&bizName=<%=tblName %>&t=<%=t %>", complete: true, isexpand: true }
                ]
                }];

                var subarr = "<%=subtbl %>";

                var arr = subarr.split(",");

                //var inputpara = "para=" + EIS.Studio.SysFolder.DefFrame.DefTableFrame.CryptPara("tblname=<%=tblName %>&mainid=").value;
                var ylnode = {
                    id: "yw", text: "业务预览", value: "", complete: true, isexpand: true, "hasChildren": true, "ChildNodes": [
                    { id: "yw0", text: "列表界面", value: "../AppFrame/AppDefault.aspx?tblname=<%=tblName %>&t=<%=t %>&admin=1", complete: true, isexpand: true },
                { id: "yw1", text: "编辑界面", value: "../AppFrame/AppInput.aspx?tblname=<%=tblName %>", complete: true, isexpand: true }
                ]
                };
                o.data[o.data.length] = ylnode;
            }

            //$("#topCaption").text("");
            //加载菜单

            $("#menuTree").treeview(o);


        });
        function menuClick(item) {
            var d = new Date();
            if (item.id != "0" && item.value) {
                var arr_url = item.value.split('?');
                var url = arr_url[0] + "?para=" + _curClass.CryptPara(arr_url[1]).value;
                window.frames["mainframe2"].location = url + "&rnd=" + Math.random();
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="ui-layout-west" style="display: none;">
        <div id="menuTree">
        </div>
    </div>
    <iframe id="mainframe2" name="mainframe2" class="ui-layout-center" width="100%" height="100%"
        frameborder="0" scrolling="auto" src="DefBizEdit.aspx?tblName=<%=tblName %>"></iframe>
    </form>
    <script src="../../js/jquery.tree.js" type="text/javascript"></script>
    <script type="text/javascript">
        var _curClass = EIS.Studio.SysFolder.DefFrame.DefTableFrame;
    </script>
</body>
</html>
