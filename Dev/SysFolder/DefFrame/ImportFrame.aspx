<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ImportFrame.aspx.cs" Inherits="EIS.Studio.SysFolder.DefFrame.ImportFrame" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>业务属性</title>
	<link rel="stylesheet" type="text/css" href="../../css/layout-default-latest.css" />
    <link href="../../css/tree.css" rel="stylesheet" type="text/css" />    
    <link rel="stylesheet" type="text/css" href="../../css/DefStyle.css"/>
	
	<style type="text/css">
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
    <script src="../../js/jquery.tree.js" type="text/javascript"></script>
 
	<script type="text/javascript">

	    var myLayout; // init global vars

	    $(document).ready(function () {
	        parent.myLayout.close("east");
	        myLayout = $('body').layout({
	            maskIframesOnResize: ".ui-layout-east,#mainframe2",
	            west__size: 160,
	            closable: true
	        });
	        var o = {
	            showcheck: false,
	            onnodeclick: menuClick,
	            blankpath: "../../Img/common/",
	            cbiconpath: "../../Img/common/"
	        };

        <%=treedata %>
	    o.data = treedata;
	    $("#menuTree").treeview(o);
	    $("#mainframe2")[0].src = "ImportTable.aspx";

	});
	function menuClick(item) {
	    var d = new Date();
	    //window.open(item.value,"mainframe2");
	}
	function getCatalog() {
	    var s = $("#menuTree").getCurItem();
	    if (s != null)
	        return s.id;
	    else
	        return "";
	}
	</script>
 
</head>
<body>
<form id="form1" runat="server">
<div class="ui-layout-east" style="display: none;">
<div id="menuTree" ></div>
</div>
<iframe id="mainframe2" name="mainframe2" class="ui-layout-center"  width="100%" height="100%" frameborder="0" scrolling="auto" src="../../welcome.htm"></iframe>
 </form>
  
</body>
</html> 
