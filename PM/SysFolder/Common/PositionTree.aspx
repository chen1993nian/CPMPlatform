<%@ Page language="c#" Codebehind="PositionTree.aspx.cs" AutoEventWireup="false" Inherits="EIS.Web.SysFolder.Common.PositionTree" enableViewState="True"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>选择用户</title>
     <link href="../../css/appstyle.css" rel="stylesheet" type="text/css" />    
     <link href="../../css/tree.css" rel="stylesheet" type="text/css" />
     <style>
     	#tree
	    {
	        border:#c3daf9 1px solid;
	        padding:5px;
	        margin:5px;
	    }
     </style>    
	</HEAD>
	<body  MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<fieldset style="PADDING-RIGHT: 5px; PADDING-LEFT: 10px; PADDING-BOTTOM: 2px; PADDING-TOP: 2px; BACKGROUND-COLOR: #f2f4fb">
				    <INPUT class="defaultbtn" style="Z-INDEX: 102; LEFT: 72px; TOP: 8px" onclick="explevel(4);" type="button" value="全部展开" /> &nbsp;
					
					<INPUT class="defaultbtn" id="btnconfirm" style="Z-INDEX: 102; LEFT: 72px; TOP: 8px; HEIGHT: 24px" type="button" value="确定返回" /> &nbsp;
					
					<INPUT class="defaultbtn" id="btndel" style="Z-INDEX: 103; LEFT: 152px; TOP: 8px; HEIGHT: 24px" onclick="window.close();" type="button" value="关闭窗口" />
					
			</fieldset>
		<br />
        <div id="tree">
            
        </div>
        
    <script src="../../js/jquery-1.3.2.min.js" type="text/javascript"></script>
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
              var idindex = qryfields.contains("positionid");
              var nameindex = qryfields.contains("positionname");
              var didindex = qryfields.contains("deptid");
              var dnameindex = qryfields.contains("deptname");

              var postionIds = [];
              var postionNames = [];
              var deptIds = [];
              var deptNames = [];
              var nodes = $("#tree").getTSNs();
              for (var i = 0; i < nodes.length; i++) {
                  if (nodes[i].id.length == "36") {
                      if (idindex > -1)
                          postionIds.push(nodes[i].id);
                      if (nameindex > -1)
                          postionNames.push(nodes[i].text);
                      if (didindex > -1)
                          deptIds.push(nodes[i].value);
                      if (dnameindex > -1)
                          deptNames.push(nodes[i].parent.text);
                  }
              }

              if (idindex > -1)
                  window.opener.document.getElementById(bizfields[idindex]).value = postionIds;
              if (nameindex > -1)
                  window.opener.document.getElementById(bizfields[nameindex]).value = postionNames;
              if (didindex > -1)
                  window.opener.document.getElementById(bizfields[didindex]).value = deptIds;
              if (dnameindex > -1)
                  window.opener.document.getElementById(bizfields[dnameindex]).value = deptNames;
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
</HTML>