<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DefFunNodeLeft.aspx.cs" Inherits="EIS.Studio.SysFolder.Permission.DefFunNodeLeft" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>组织部门</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />    
	<link rel="stylesheet" href="../../css/appStyle.css?v=1" type="text/css"/>
	<link rel="stylesheet" href="../../css/zTreeStyle/zTreeStyle.css" type="text/css"/>
	<script type="text/javascript" src="../../js/jquery-1.4.2.min.js"></script>
	<script type="text/javascript" src="../../js/jquery.ztree.all-3.4.min.js"></script>
	<script type="text/javascript" src="../../js/jquery.ztree.exedit-3.4.min"></script>
    
     <style type="text/css">
         body{overflow:hidden;}
     	#tree{
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
        <div class="toolbar">
            <button id="btnExpand" type="button"><img alt="全部展开" src="../../img/common/ico6.gif" />全部展开</button>
            <button id="btnAdd" type="button"><img alt="增加子节点" src="../../img/common/add.png" />增加子节点</button>
            <button id="btnDel" type="button"><img alt="删除" src="../../img/common/delete.png" />删除</button>
            <button id="btnLimit" type="button"><img alt="权限" id="imgLimit" src="../../img/common/checkbox_0.gif" />权限</button>
        </div>
        <ul id="tree"  class="ztree"> 
        </ul>
    <script type="text/javascript">
        var _curClass = EIS.Studio.SysFolder.Permission.DefFunNodeLeft;
        var setting = { 
            edit: {
                drag:{
                    autoExpandTrigger: true,
                    isCopy:true,
                    isMove:true
                },
                enable: true,
                showRenameBtn:false,
                showRemoveBtn:true,
                removeTitle:"删除",
                editNameSelectAll: false
            },
            check:{enable:true},
            callback:{
                onClick:nodeClick,
                onRemove : afterNodeRemove
            }
        };
        function nodeClick(e,treeId,node)
        {
            if(!chkLimit)
            {
                var paraStr = "tblName=T_E_Sys_FunNode&condition=_AutoID='" + node.value + "'";
                var para = _curClass.CryptPara(paraStr).value;
                window.open("../../SysFolder/AppFrame/AppInput.aspx?para=" + para, "DefFunMain");
            }
            else
            {
                window.open("../Limit/ObjectLimitFrame.aspx?funId=" + node.value, "DefFunMain");
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
        <%=treedata %>;
        $(function () {
            var h=$(document).height();
            $("#tree").height(h-100);
            $(window).resize(function(){
                var h=$(document).height();
                $("#tree").height(h-100);
            });
            $.fn.zTree.init($("#tree"), setting, zNodes);
            zTree = $.fn.zTree.getZTreeObj("tree");
            var root =zTree.getNodeByTId("tree_1");
            $("#btnAdd").click(function(){
                var nodes = zTree.getSelectedNodes();
                if(nodes.length == 0)
                {
                    alert("请选择父节点");
                }
                else
                {
                    var nodeName="";
                    if(nodes[0].children)
                    {
                        nodeName="子节点"+nodes[0].children.length;
                    }
                    else
                    {
                        nodeName="子节点1";
                    }

                    var ret = _curClass.AddSonNode(nodeName,nodes[0].id,"<%=webId %>");
                    if(ret.error)
                    {
                        alert("添加子节点时出错："+ret.error.Message);
                    }
                    else
                    {
                        var retArr=ret.value.split("|");
                        var son={id:retArr[1],name:nodeName,value:retArr[0]};
                        zTree.addNodes(nodes[0],son);
                    }
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

                        if(!confirm("确认删除【"+n.name+"】吗"))
                            return false;
                        if(n.children)
                        {
                            if(n.children.length>0)
                            {
                                alert("存在子节点，不能删除");
                                return false;
                            }
                        }

                        var ret = _curClass.RemoveNode(n.value).value;
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

            $("#btnLimit").toggle(function(){
                chkLimit = 1;
                $("#imgLimit").attr("src","../../img/common/checkbox_1.gif");
            },function(){
                chkLimit = 0;
                $("#imgLimit").attr("src","../../img/common/checkbox_0.gif");            
            });
        });
        var chkLimit=0;
    </script>
		</form>
	</body>
</html>
