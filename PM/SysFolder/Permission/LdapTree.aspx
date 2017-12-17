<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LdapTree.aspx.cs" Inherits="EIS.Web.SysFolder.Permission.LdapTree" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>组织机构选择</title>
     <link href="../../css/appstyle.css" rel="stylesheet" type="text/css" />    
     <link href="../../css/tree.css" rel="stylesheet" type="text/css" />
     <style type="text/css">
     	#tree
	    {
	        border:#c3daf9 1px solid;
	        padding:5px;
	        margin:5px;
	    }
     </style>    
	</head>
	<body >
		<form id="Form1" method="post" runat="server">
			<fieldset style="text-align:right;padding-right: 5px; padding-left: 10px; padding-bottom: 2px; padding-top: 2px; background-color: #f2f4fb">
                    &nbsp;
					<input class="defaultbtn"  id="btnconfirm" style="Z-INDEX: 102; LEFT: 72px; TOP: 8px; HEIGHT: 24px"
					type="button" value="确定返回"/> &nbsp;
                    <input class="defaultbtn" id="btndel" style="Z-INDEX: 103; LEFT: 152px; TOP: 8px; HEIGHT: 24px"
					onclick="window.close();" type="button" value="关闭窗口"/>
			</fieldset>
		<br />
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

                },
                blankpath: "../../Img/common/",
                cbiconpath: "../../Img/common/"
            };
            o.data = treedata;
            $("#tree").treeview(o);
            $("#btnconfirm").click(function (e) {

                var node = $("#tree").getCurItem();
                window.opener.document.getElementById("<%=Request["cid"] %>").value = node.value;
               window.close();
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
</html>
