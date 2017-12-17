<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DefCatalog.aspx.cs" Inherits="EIS.Studio.SysFolder.DefFrame.DefCatalog" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>分类目录</title>
    <meta http-equiv="Pragma" content="no-cache"/>
    <link rel="stylesheet" type="text/css" href="../../Css/appstyle.css"/>
    <link href="../../css/tree.css" rel="stylesheet" type="text/css" />
     <style type="text/css">
     	#tree
	    {
	        border:#c3daf9 1px solid;
	        padding:5px;
	        margin:5px;
	        text-align:left;
	        background:#f9fafe;
	    }
     </style> 
	<script type="text/javascript">
    <!--

    function addNode(objid, objname)//增加子级结点
    {
        var newNode = {
            id: "" + objid,
            text: objname,
            value: objid,
            showcheck: false,
            isexpand: true,
            checkstate: 0,
            hasChildren: false,
            ChildNodes: null,
            complete: true
        };
        jQuery("#tree").addNode(newNode);
    }
    function delNode() {
        $("#tree").delNode();
        parent.frames["main"].location.href = "../../welcome.htm";
        return;
        if (window.confirm("确认删除吗？")) {
            var nodeCode = $("#tree").getCurItem().id;
            var ret = EIS.Studio.SysFolder.DefFrame.DefCatalog.delnode(nodeCode);
            if (ret.error) {
                alert(ret.error.Message);
            }
            else {
                $("#tree").delNode();
            }
        }
    }
    function changeNode(objid, objname)//增加子级结点
    {
        jQuery("#tree").changeText(objid, objname);
    }
    //-->
		</script>
	</head>
	<body >
		<form id="Form1" method="post" runat="server">
			<div id="tree"> </div>
		</form>
	<script src="../../js/jquery-1.4.2.min.js" type="text/javascript"></script>
    <script src="../../js/jquery.tree.js" type="text/javascript"></script>
   
    <script type="text/javascript">
        var userAgent = window.navigator.userAgent.toLowerCase();
        $.browser.msie8 = $.browser.msie && /msie 8\.0/i.test(userAgent);
        $.browser.msie7 = $.browser.msie && /msie 7\.0/i.test(userAgent);
        $.browser.msie6 = !$.browser.msie8 && !$.browser.msie7 && $.browser.msie && /msie 6\.0/i.test(userAgent);
        <%=treedata %>
        function load() {
            var o = {
                showcheck: false,
                onnodeclick: function (item) {

                    if (item.parent)
                        $("#btndel").attr("disabled", false);
                    else
                        $("#btndel").attr("disabled", true);

                    var d = new Date();
                    window.parent.frames["main"].location = "DefCatalogEdit.aspx?nodewbs=" + item.id + "&time=" + d.getMilliseconds();

                },
                blankpath: "../../Img/common/",
                cbiconpath: "../../Img/common/"
            };
            o.data = treedata;
            $("#tree").treeview(o);
            $("#showchecked").click(function (e) {
                var s = $("#tree").getTSVs();
                if (s != null)
                    alert(s.join(","));
                else
                    alert("NULL");
            });
            $("#showcurrent").click(function (e) {
                var s = $("#tree").getTCT();
                if (s != null)
                    alert(s.text);
                else
                    alert("NULL");
            });
            $("#reflashshanghai").click(function (e) {
                $("#tree").reflash("9"); //9 为节点的ID
            });

        }
        if ($.browser.msie6) {
            load();
        }
        else {
            $(document).ready(load);
        }


    </script>

	</body>
</html>
