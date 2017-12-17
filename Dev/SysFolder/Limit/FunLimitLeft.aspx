<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FunLimitLeft.aspx.cs" Inherits="EIS.Studio.SysFolder.Limit.FunLimitLeft" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>功能节点授权</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />    
	<link rel="stylesheet" href="../../css/appStyle.css?v=1" type="text/css"/>
	<link rel="stylesheet" href="../../css/zTreeStyle/zTreeStyle.css" type="text/css"/>
	<script type="text/javascript" src="../../js/jquery-1.4.2.min.js"></script>
	<script type="text/javascript" src="../../js/jquery.ztree.all-3.4.min.js"></script>
     <style type="text/css">
         body{
             overflow:hidden;
             background:#f9fafe;
	         border-right: 2px solid #aaa;
        }
     	#tree{
	        overflow:auto;
	        padding:2px;
	        margin:1px;
	    }
     </style>    
	</head>
	<body>
		<form id="Form1" method="post" runat="server">
        <ul id="tree"  class="ztree"> 
        </ul>
    <script type="text/javascript">
        var setting = { 
            edit: {
                drag:{
                    autoExpandTrigger: true,
                    isCopy:true,
                    isMove:true
                },
                enable: false,
                showRenameBtn:false,
                showRemoveBtn:false,
                editNameSelectAll: false
            },
            callback:{
                onClick:nodeClick,
                beforeRemove : beforeNodeRemove,
                onRemove : afterNodeRemove
            }
        };
        function nodeClick(e,treeId,node)
        {
            var t = window.parent.frames["limittop"].getLimitType();
            if(t=="1"){
                window.open("FunLimitDept.aspx?funId="+node.value+"&funName="+escape(node.name),"main");
            }
            else if(t=="2"){
                window.open("FunLimitPosition.aspx?funId="+node.value+"&funName="+escape(node.name),"main");
            }
            else if(t=="3"){
                window.open("FunLimitEmployee.aspx?funId="+node.value+"&funName="+escape(node.name),"main");
            }
            else if(t=="4"){
                window.open("FunLimitRole.aspx?funId="+node.value+"&funName="+escape(node.name),"main");
            }
            else if(t=="5"){
                window.open("FunLimitExclude.aspx?funId="+node.value+"&funName="+escape(node.name),"main");
            }
            else if(t=="0"){
                window.open("FunLimitQuery.aspx?funId="+node.value+"&funName="+escape(node.name),"main");            
            }
            window.parent.frames["limittop"].document.getElementById("funId").value = node.value;
        }
        function beforeNodeRemove(treeId,node)
        {
            if(!confirm("确认删除吗"))
                return false;
            if(node.children)
            {
                if(node.children.length>0)
                {
                    alert("存在子节点，不能删除");
                    return false;
                }
            }
        }
        function afterNodeRemove(e,treeId,node)
        {
            return;
            var afterNode = node.getPreNode();
            if(afterNode == null)
                afterNode = node.getParentNode();
            if(afterNode != null)
                zTree.selectNode(afterNode);
        }
        var zTree;
        var zNodes =<%=treedata %>;
        $(function () {
            var h=$(document).height();
            $("#tree").height(h-10);
            $(window).resize(function(){
                var h=$(document).height();
                $("#tree").height(h-10);
            });
            $.fn.zTree.init($("#tree"), setting, zNodes);
            zTree = $.fn.zTree.getZTreeObj("tree");
            var root =zTree.getNodeByTId("tree_1");
            $("#btnExpand").toggle(function(){
                $.each(root.children,
                function(i,n){
                    zTree.expandNode(n,true,true,false);
                });
            },function(){
                $.each(root.children,
                function(i,n){
                    zTree.expandNode(n,false,true,false);
                });
            });
        });
    </script>
		</form>
	</body>
</html>
