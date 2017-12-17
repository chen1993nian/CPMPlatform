<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RelationSupplierTypeClassifyTree.aspx.cs" Inherits="EIS.WorkAsp.RelationTree.RelationSupplierTypeClassifyTree" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>材料供应商类别树</title>
    <link href="../../style.css" type="text/css" rel="stylesheet"/>

	<link rel="stylesheet" href="../../css/zTreeStyle/zTreeStyle.css" type="text/css">
	<script type="text/javascript" src="../../js/jquery-1.4.4.min.js"></script>
	<script type="text/javascript" src="../../js/jquery.ztree.core-3.4.js"></script>
	<script type="text/javascript" src="../../js/jquery.ztree.excheck-3.4.js"></script>
	<script type="text/javascript" src="../../js/jquery.ztree.exedit-3.4.js"></script>

	<script type="text/javascript">
		<!--
	    var setting = {
			view: {
				selectedMulti: false
			},
	        data: {
				key: {
				    title: "t",
					wbs:"wbs",
					pwbs:"pwbs"
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
