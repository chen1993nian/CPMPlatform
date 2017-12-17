<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DefQueryFrame.aspx.cs" Inherits="EIS.Studio.SysFolder.DefFrame.DefQueryFrame" %>

<!DOCTYPE html>


<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>【<%=tblNameCn %>】业务定义</title>
    <meta http-equiv="X-UA-Compatible" content="IE=Edge" />    
	<link rel="stylesheet" type="text/css" href="../../css/layout-default-latest.css" />
    <link href="../../css/tree.css" rel="stylesheet" type="text/css" />    
	
	<style type="text/css">
    *{
        margin:0px;
        padding:0px;
    }
    html
    {
        scrollbar-shadow-color: #ffffff; 
        scrollbar-highlight-color: #ffffff; 
        scrollbar-face-color: #d9d9d9; 
        scrollbar-3dlight-color: #d9d9d9; 
        scrollbar-darkshadow-color: #d9d9d9; 
        scrollbar-track-color: #ffffff; 
        scrollbar-arrow-color: #ffffff
    }
	/* remove padding and scrolling from elements that contain an Accordion OR a content-div */
	.ui-layout-north,
	.ui-layout-center ,	/* has content-div */
	.ui-layout-west ,	/* has Accordion */
	.ui-layout-east ,	/* has content-div ... */
	.ui-layout-east .ui-layout-content { /* content-div has Accordion */
		padding: 0px;
		margin:0px;
		overflow: hidden;
		background-color:#f5f5f5
	}
	.ui-layout-mask {
		opacity: 0.2 !important;
		filter:	 alpha(opacity=20) !important;
		background-color: #666 !important;
	}
	#topCaption,#topCaption2
	{
	    padding-left:20px;
	}
	#menuTree
	{
	    border:#c3daf9 1px solid;
	    padding:5px;
	    margin:5px;
	}
	</style>
 
	<!-- REQUIRED scripts for layout widget -->
	<script type="text/javascript" src="../../js/jquery-1.4.2.min.js"></script>
	<script type="text/javascript" src="../../js/jquery-ui-1.7.2.js"></script>
	<script type="text/javascript" src="../../js/jquery.layout.js"></script>
 
	<script type="text/javascript">

	    var myLayout; // init global vars
	    $(document).ready(function () {
	        //parent.myLayout.close("west");
	        myLayout = $('body').layout({
	            maskIframesOnResize: ".ui-layout-west,#mainframe2",
	            west__size: 160,
	            closable: true
	        });

	        var o = {
	            showcheck: false,
	            onnodeclick: menuClick,
	            blankpath: "../../Img/common/",
	            cbiconpath: "../../Img/common/"
	        };

	        o.data = [
            {
                id: "0", text: "功能定义", value: "", complete: true, isexpand: true, "hasChildren": true, "ChildNodes": [
                   { id: "0a", text: "基本信息", value: "DefBizEdit3.aspx?tblname=<%=tblname %>" + "&t=3", complete: true, isexpand: true },
                { id: "01", text: "列表属性", value: "DefFieldsAttr.aspx?tblname=<%=tblname %>" + "&t=3", complete: true, isexpand: true },
                { id: "0b", text: "查询条件", value: "DefFieldsQuery.aspx?tblname=<%=tblname %>&sindex=&t=3", complete: true, isexpand: true },
                { id: "02", text: "界面设计", value: "DefQueryStyleList.aspx?tblname=<%=tblname %>" + "&t=3", complete: true, isexpand: true },
                { id: "09", text: "业务逻辑", value: "DefTableScriptList.aspx?para=<%=CryptStr2 %>&t=3", complete: true, isexpand: true },
                { id: "04", text: "脚本编辑", value: "DefTableScript.aspx?tblname=<%=tblname %>" + "&t=3", complete: true, isexpand: true },
                { id: "08", text: "子查询定义", value: "DefBizList3.aspx?parent=<%=tblname %>" + "&t=3", complete: true, isexpand: true },
                { id: "03", text: "列表预览", value: "../AppFrame/AppQuery.aspx?para=<%=CryptStr %>", complete: true, isexpand: true }
                //{ id: "05", text: "图表定义", value: "DefChart.aspx?tblname=<%=tblname %>" + "&t=1", complete: true, isexpand: true }
                //{ id: "06", text: "统计分析", value: "DefPivotGrid.aspx?tblname=<%=tblname %>" + "&t=1", complete: true, isexpand: true }
				//{ id: "07", text: "列表预览2", value: "../AppFrame/AppQuery2.aspx?para=<%=CryptStr %>", complete: true, isexpand: true }
            ]
            }
            ];


	    $("#menuTree").treeview(o);


	});
    function menuClick(item) {
        if (item.id != "0")
            window.frames["mainframe2"].location = item.value + "&r=" + Math.random();

    }
	</script>
 
</head>
<body>
<form id="form1" runat="server">
<div class="ui-layout-west" style="display: none;">
<div id="menuTree" ></div>
</div>
<iframe id="mainframe2" name="mainframe2" class="ui-layout-center" width="100%" height="100%" frameborder="0" scrolling="auto" src="DefBizEdit3.aspx?tblName=<%=tblname %>"></iframe>
 </form>
<script src="../../js/jquery.tree.js" type="text/javascript"></script>
  
</body>
</html> 
