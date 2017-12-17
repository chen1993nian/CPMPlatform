<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RelationDeptRoleTree.aspx.cs" Inherits="EIS.WorkAsp.RelationTree.RelationDeptRoleTree" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>部门角色树</title>
	<link rel="stylesheet" href="../../css/zTreeStyle/zTreeStyle.css" type="text/css">
	<script type="text/javascript" src="../../js/jquery-1.4.4.min.js"></script>
	<script type="text/javascript" src="../../js/jquery.ztree.core-3.4.js"></script>
	<script type="text/javascript" src="../../js/jquery.ztree.excheck-3.4.js"></script>
	<script type="text/javascript" src="../../js/jquery.ztree.exedit-3.4.js"></script>
    <script type="text/javascript">
        var setting = {
			view: {
				showIcon: true,
                fontCss: getFont
			},
            check: {
				enable: false
			},
			data: {
				simpleData: {
					enable: true
				}
			},
            async: {
				enable: true,
				url:"../asyncData/getNodes.php",
				autoParam:["id", "name=n"],
				otherParam:{"otherParam":"zTreeAsyncTest"},
				dataFilter: filter
			},
			callback: {
				beforeClick: beforeClick,
				beforeAsync: beforeAsync,
                beforeDrag: beforeDrag,
				beforeDrop: beforeDrop
			}
		};
        
        function getFont(treeId, node) {
			return node.font ? node.font : {};
		}
        function beforeClick(treeId, treeNode) {
			if (!treeNode.isParent) {
				//alert("请选择父节点");
				return false;
			} else {
				return true;
			}
		}
		function beforeAsync(treeId, treeNode) {
			return treeNode ? treeNode.level < 5 : true;
		}
        function beforeDrag(treeId, treeNodes) {
			for (var i=0,l=treeNodes.length; i<l; i++) {
				if (treeNodes[i].drag === false) {
					return false;
				}
			}
			return true;
		}
		function beforeDrop(treeId, treeNodes, targetNode, moveType) {
			return targetNode ? targetNode.drop !== false : true;
		}

        function filter(treeId, parentNode, childNodes) {
			if (!childNodes) return null;
			for (var i=0, l=childNodes.length; i<l; i++) {
				childNodes[i].name = childNodes[i].name.replace(/\.n/g, '.');
			}
			return childNodes;
		}



        var zNodes = [<%=FunNodeZTree_Script %>];

        $(document).ready(function () {
            $.fn.zTree.init($("#zTree"), setting, zNodes);
        });
	</script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="zTreeDemoBackground left">
		<ul id="zTree" class="ztree"></ul>
	</div>
    <asp:HiddenField ID="hidd_target" runat="server" Value="RelationMain" />
    </form>
</body>
</html>
