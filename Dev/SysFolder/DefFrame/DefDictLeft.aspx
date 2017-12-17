<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DefDictLeft.aspx.cs" Inherits="EIS.Studio.SysFolder.DefFrame.DefDictLeft" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>无标题页</title>
     <link href="../../css/appstyle.css" rel="stylesheet" type="text/css" />    
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
	</head>
	<body>
		<form id="Form1" method="post" runat="server">
        <div id="tree">
            
        </div>
        
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
                    var d = new Date();
                    window.parent.frames["main"].location = "DefDictList.aspx?nodewbs=" + item.id + "&time=" + d.getMilliseconds();

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
		</form>
	</body>
</HTML>
