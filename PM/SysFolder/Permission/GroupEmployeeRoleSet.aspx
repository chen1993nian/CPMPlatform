<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GroupEmployeeRoleSet.aspx.cs" Inherits="EIS.Web.SysFolder.Permission.GroupEmployeeRoleSet" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>设置角色</title>
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
	<body  >
		<form id="Form1" method="post" runat="server">
        	<fieldset style="padding: 5px; background-color: #f2f4fb">
				    <div style="float:left;width:260px;padding-top:4px;">员工姓名：<%=roleName %></div>&nbsp;&nbsp;
					<input class="defaultbtn" id="btnconfirm" style="Z-INDEX: 102; LEFT: 72px; TOP: 8px; HEIGHT: 24px"
					type="button" value="保存返回"/> &nbsp;
					<input class="defaultbtn" id="btndel" style="Z-INDEX: 103; LEFT: 152px; TOP: 8px; HEIGHT: 24px"
					onclick="window.close();" type="button" value="关闭窗口"/>
			</fieldset>

            <div id="tree"></div>



        
    <script src="../../js/jquery-1.4.2.min.js" type="text/javascript"></script>
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
                dataload: function (data) {
                    return;
                    for (var i = 0; i < data.length; i++) {
                        data[i].checkstate = 2;
                    }
                },
                onnodeclick: function (item) {

                },
                blankpath: "../../Img/common/",
                cbiconpath: "../../Img/common/"/*,
            url:"../Common/TreeData.ashx?queryid=EmployeeByDeptID"*/
            };
            o.data = treedata;
            $("#tree").treeview(o);

            $("#btnconfirm").click(function (e) {


                var emps = [];
                var nodes = $("#tree").getTSNs(true);
                for (var i = 0; i < nodes.length; i++) {
                    emps.push(nodes[i].value);
                }

                var r = EIS.Web.SysFolder.Permission.GroupEmployeeRoleSet.SaveRoleSet(emps.join(","), "<%=roleId %>");
              if (r.error) {
                  alert("保存出错:" + r.error.Message);
              }
              else {
                  alert("保存成功！");
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
