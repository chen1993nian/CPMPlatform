<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DataLimitDeptTree.aspx.cs" Inherits="Studio.JZY.WorkAsp.DataLimit.DataLimitDeptTree" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>部门树</title>
    <link href="../../style.css" type="text/css" rel="stylesheet"/>

	<link rel="stylesheet" href="../../css/zTreeStyle/zTreeStyle.css" type="text/css">
	<script type="text/javascript" src="../../js/jquery-1.4.4.min.js"></script>
	<script type="text/javascript" src="../../js/jquery.ztree.core-3.4.js"></script>
	<script type="text/javascript" src="../../js/jquery.ztree.excheck-3.4.js"></script>
	<script type="text/javascript" src="../../js/jquery.ztree.exedit-3.4.js"></script>


    <script type="text/javascript" language="javascript">
        //点击节点
        function SaveCheck() {
            var arr = [];
            var zTree = jQuery.fn.zTree.getZTreeObj("treeDemo");
            var nodes_chk = zTree.getCheckedNodes(true);
            for (var i = 0, l = nodes_chk.length; i < l; i++) {
                arr.push("'" + nodes_chk[i].DeptID + "'");
            }
            var rest = EIS.WorkAsp.DataLimit.DataLimitDeptTree.SaveCheckDept(arr.join(","), document.getElementById("HiddenField1").value, document.getElementById("HiddenField2").value).value;
            alert("保存完毕！");
        }
    </script>

	<script type="text/javascript">
		<!--
	    var setting = {
			view: {
				selectedMulti: false
			},
			check: {
				enable: true,
                chkboxType: { "Y" : "s", "N" : "ps" }
			},
	        data: {
				key: {
				    title: "t",
					DeptID:"DeptID",
					DeptCode:"DeptCode",
					DeptWBS:"DeptWBS",
                    DeptPWBS:"DeptPWBS"
				},
	            simpleData: {
	                enable: true
	            }
			}
	    };

	    var zNodes = [<%=this.FunNodeZTree_Script %>
		];

	    jQuery(document).ready(function () {
	        jQuery.fn.zTree.init(jQuery("#treeDemo"), setting, zNodes);
	    });
		//-->
	</script>

</head>
<body style="margin:0;" scroll="auto"  oncontextmenu="return(false);">
    <form id="form1" runat="server">
         <table  width="100%"  height="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    <input type="button" name="Button1" value="保存" id="Button1" class="btn_sub" onclick="SaveCheck();" />
                </td>
            </tr>
			<tr>
				<td valign="top">
                    <ul id="treeDemo" class="ztree"></ul>
				</td>
			</tr>
        </table>            
    <asp:HiddenField ID="HiddenField1" runat="server" />
    <asp:HiddenField ID="HiddenField2" runat="server" />
    </form>
</body>
</html>
