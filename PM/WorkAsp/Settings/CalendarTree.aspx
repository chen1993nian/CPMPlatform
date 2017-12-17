<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CalendarTree.aspx.cs" Inherits="EIS.Web.WorkAsp.Settings.CalendarTree" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>日历列表</title>
    <link rel="stylesheet" type="text/css" href="../../css/appstyle.css" />    
    <link rel="stylesheet" type="text/css" href="../../css/tree.css" />
    <link rel="stylesheet" type="text/css" href="../../css/doc.css" />
	<link rel="stylesheet" href="../../css/zTreeStyle/zTreeStyle.css" type="text/css"/>
	<script type="text/javascript" src="../../js/jquery-1.4.2.min.js"></script>
	<script type="text/javascript" src="../../js/jquery.ztree.all-3.4.min.js"></script>
     <style type="text/css">
         body{overflow:hidden;padding:0px;margin:0px;}
     	#tree
	    {
	        border:#c3daf9 1px solid;
	        background:#f9fafe;
	        overflow:auto;
	        padding:5px;
	        margin:5px;
	    }
     </style>    
	</head>
	<body>
		<form id="Form1" method="post" runat="server">
         <div class="toolbar" style="">
            <button id="btnAdd" type="button"><img alt="新建日历" src="../../img/common/add.png" />新建日历</button>
            <button id="btnDel" type="button"><img alt="删除日历" src="../../img/common/delete.png" />删除</button>
            <button id="btnFresh" type="button"><img alt="刷新" src="../../img/common/fresh.png" />刷新</button>
        </div>
        <ul id="tree"  class="ztree"> 
        </ul>
		</form>
	</body>
</html>
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
                onClick:nodeClick
            },
            view:{showLine:false}
        };
        function nodeClick(e,treeId,node)
        {
            if(node.id!="root"){
                window.open("SetCalendar.aspx?nodeId=" + node.id + "&time=" + Math.random(), "main");
            }
        }
        function expandToRoot(node)
        {
            if(node.getParentNode())
            {
                var pNode =node.getParentNode();
                zTree.expandNode(pNode,true,false,true);
                expandToRoot(pNode);
            }
        }
        var searchKey="";
        var searchOrder = 0;
        var searchNodes =[];
        function searchNode()
        {
            if(!$("#txtSearch").val())
            {
                alert("请输入查询内容");
                return;
            }
            if(searchKey != $("#txtSearch").val())
            {
                //新的查询
                searchKey = $("#txtSearch").val();
                searchNodes = zTree.getNodesByParamFuzzy("name", searchKey);
                searchOrder = 0;
            }
            if(searchOrder<searchNodes.length)
            {
                if(searchNodes.length>0)
                {
                    zTree.selectNode(searchNodes[searchOrder]);
                    expandToRoot(searchNodes[searchOrder]);
                    searchOrder++;
                }
            }
            else
            {
                alert("已经查找到头");
            }
        }
        var zTree;
        var zNodes =<%=treedata %>;
        $(function () {
            var h=$(document).height();
            $("#tree").height(h-80);
            $(window).resize(function(){
                var h=$(document).height();
                $("#tree").height(h-80);
            });
            $.fn.zTree.init($("#tree"), setting, zNodes);
            zTree = $.fn.zTree.getZTreeObj("tree");
            var root =zTree.getNodeByTId("tree_1");
            $("#btnAdd").click(function(){
                var nodeName="";
                if(root.children)
                {
                    nodeName="子节点" + (root.children.length+1);
                }
                else
                {
                    nodeName="子节点1";
                }

                var ret = _curClass.AddSonNode(nodeName,root.id);
                if(ret.error)
                {
                    alert("添加日历时出错："+ret.error.Message);
                }
                else
                {
                    var retArr=ret.value;
                    var son={id:retArr,name:nodeName,value:"",icon:"../../img/common/calendar.gif"};
                    zTree.addNodes(root,son);
                    var newNode = zTree.getNodesByParam("id", retArr);
                    $("#"+newNode[0].tId+"_a").click();
                }
            });
            $("#btnDel").click(function(){
                var nodes = zTree.getSelectedNodes();
                if(nodes.length == 0)
                {
                    alert("请选择要删除的节点");
                }
                else
                {

                    $.each(nodes , function(i,n){
                        if(!confirm("确认删除选中日历吗？"))
                            return;
                        var ret = _curClass.RemoveNode(n.id).value;
                        if(ret.error)
                        {
                            alert("删除节点时出错："+ret.error.Message);
                        }
                        else
                        {
                            zTree.removeNode(n,true);
                        }
                    });
                     
                }
            });
            $("#txtSearch").keydown(function(){
                if(event.keyCode == 13)
                {
                    searchNode();
                    return false;
                }
            });
            $("#btnSearch").click(function(){
                searchNode();
            });

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
            $("#btnFresh").click(function(){
                window.location.reload();
            });
        });
        var _curClass = EIS.Web.WorkAsp.Settings.CalendarTree;
        function addNode(retArr)//增加子节点
        {
            var nodes = zTree.getSelectedNodes();
            if (nodes.length == 0) {
                alert("请选择父节点");
            }
            else {
                var son = { id: retArr[0], name: retArr[1], value: retArr[2] };
                zTree.addNodes(nodes[0], son);
            }
        }
        function changeNode(retArr)//修改名称
        {
            var objId = retArr[0];
            var objName = retArr[1];
            var searchNodes = zTree.getNodesByParamFuzzy("id", objId);
            if (searchNodes.length > 0) {
                searchNodes[0].name = objName;
                zTree.updateNode(searchNodes[0]);
            }
        }
        function removeNode(nodeId) {
            var searchNodes = zTree.getNodesByParamFuzzy("id", objid);
            if (searchNodes.length > 0) {
                searchNodes[0].name = objname;
                zTree.removeNode(searchNodes[0], true);
            }
        }
        function upFolder() {
            if (treeObj.getCurItem().parent) {
                var p = treeObj.getCurItem().parent;
                treeObj.expandNode(p.id);
            }
        }
        function openFolder(folderId) {
            var searchNodes = zTree.getNodesByParamFuzzy("id", folderId);
            if (searchNodes.length > 0) {
                zTree.selectNode(searchNodes[0]);
                expandToRoot(searchNodes[0]);
            }
        }
        //-->
    </script>
