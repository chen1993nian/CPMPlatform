<%@ Page language="c#" Codebehind="RoleTree.aspx.cs" AutoEventWireup="false" Inherits="EIS.Web.SysFolder.Common.RoleTree" enableViewState="True"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
    <head id="Head1" runat="server">
    <title>选择角色</title>
     <link href="../../css/appstyle.css" rel="stylesheet" type="text/css" />    
     <link href="../../css/tree.css" rel="stylesheet" type="text/css" />
     <style type="text/css">
     	#tree
	    {
	        border:#c3daf9 1px solid;
	        background:#f9fafe;
	        padding:5px;
	        margin:5px;
	    }
	    fieldset{
	        text-align:right;
	        padding-right: 5px; 
	        padding-left: 10px; 
	        padding-bottom: 2px; 
	        padding-top: 2px; 
	        background: #f2f4fb url(../../img/common/group.png) no-repeat 10px center;
	        }
	    fieldset span{color:#3a6ea5;font-weight:bold;float:left;line-height:30px;padding-left:24px;}    
     </style>    
	</head>
	<body>
		<form id="Form1" method="post" runat="server">
		<fieldset style="">
                <span>请选择下面的角色</span>
				<input class="defaultbtn" id="btnconfirm" style="z-index: 102; left: 72px; top: 8px; height: 26px"
				type="button" value="确定返回"/> &nbsp;
				<input class="defaultbtn" id="btndel" style="z-index: 103; left: 152px; top: 8px; height: 26px"
				onclick="window.close();" type="button" value="关闭窗口"/>
		</fieldset>
        <div id="tree">
            
        </div>
        
    <script type="text/javascript" src="../../js/jquery-1.7.min.js"></script>
    <script src="../../js/jquery.tree.js" type="text/javascript"></script>
   
    <script type="text/javascript">
        Array.prototype.contains = function (element) {
            for (var i = 0; i < this.length; i++) {
                if (this[i] == element) {
                    return i;
                }
            }
            return -1;
        }
        $(window).resize(function () {
            $("#tree").height($(document.body).height() - 80);
        });
        $("#tree").height($(document.body).height() - 80);
        var userAgent = window.navigator.userAgent.toLowerCase();
        $.browser.msie8 = $.browser.msie && /msie 8\.0/i.test(userAgent);
        $.browser.msie7 = $.browser.msie && /msie 7\.0/i.test(userAgent);
        $.browser.msie6 = !$.browser.msie8 && !$.browser.msie7 && $.browser.msie && /msie 6\.0/i.test(userAgent);
        <%=treedata %>
        function load() {
            var o = {
                showcheck: true,
                onnodeclick: function (item) {


                },
                blankpath: "../../Img/common/",
                cbiconpath: "../../Img/common/",
                url: "../Common/TreeData.ashx?queryid=PositionByDeptId"
            };
            o.data = treedata;
            $("#tree").treeview(o);

            $("#btnconfirm").click(function (e) {
                //var s=$("#tree").getTSVs();
                var cid = "<%=cid %>";
                var bizfields = cid.split(",");
                var qryfields = "<%=queryfield %>".split(",");
              var idindex = qryfields.contains("roleid");
              var nameindex = qryfields.contains("rolename");

              var postionIds = [];
              var postionNames = [];
              var nodes = $("#tree").getTSNs();
              for (var i = 0; i < nodes.length; i++) {
                  if (nodes[i].id.length == "36") {
                      if (idindex > -1)
                          postionIds.push(nodes[i].id);
                      if (nameindex > -1)
                          postionNames.push(nodes[i].text);
                  }
              }

              if (idindex > -1)
                  window.opener.document.getElementById(bizfields[idindex]).value = postionIds;
              if (nameindex > -1)
                  window.opener.document.getElementById(bizfields[nameindex]).value = postionNames;

              if ('<%=Request["callback"] %>' != "") {
                   window.opener["<%=Request["callback"] %>"](bizfields, postionIds, postionNames);
               }
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