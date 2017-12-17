<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DefDeptLeft.aspx.cs" Inherits="EIS.Web.SysFolder.Permission.DefDeptLeft" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>组织部门</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />    
	<link rel="stylesheet" href="../../css/appStyle.css?v=1" type="text/css"/>
	<link rel="stylesheet" href="../../css/zTreeStyle/zTreeStyle.css" type="text/css"/>
	<script type="text/javascript" src="../../js/jquery-1.4.2.min.js"></script>
	<script type="text/javascript" src="../../Js/jquery.ztree.all-3.4.min.js"></script>
 
     <style type="text/css">
         body{overflow:hidden;}
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
            <button id="btnExpand" type="button"><img alt="全部展开" src="../../img/common/ico6.gif" />全部展开</button>
            <button id="btnAdd" type="button"><img alt="增加部门" src="../../img/common/add.png" />增加部门</button>
            <button id="btnDel" type="button"><img alt="删除" src="../../img/common/delete.png" />删除</button>
            <button id="btnFresh" type="button"><img alt="刷新" src="../../img/common/fresh.png" />刷新</button>
        </div>
        <ul id="tree"  class="ztree"> 
        </ul>
        <div class="hidden">
        	<input class="defaultbtn" id="chkshow"  type="checkbox" />
            <label for="chkshow">显示所有子部门</label> &nbsp;
        </div>
       <script type="text/javascript">
           var _curClass = EIS.Web.SysFolder.Permission.DefDeptLeft;
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
                   //beforeRemove : beforeNodeRemove,
                   onRemove : afterNodeRemove
               }
           };
           function nodeNameChange(name)
           {
               var nodes = zTree.getSelectedNodes();
               if(nodes.length>0)
               {
                   nodes[0].name=name;
                   zTree.updateNode(nodes[0]);
               }

           }
           function nodeClick(e,treeId,node)
           {
               window.parent.frames["main"].location.href="DefDeptEdit.aspx?DeptId="+node.value; 
               //window.open("DefDeptEdit.aspx?DeptId="+node.value, "main");
               return;
               if($("#chkshow").attr("checked"))
               {
                   window.parent.frames["main"].location= "DefDeptList.aspx?DeptPWBS="+item.id+"&condition="+escape("DeptWBS like '"+item.id+"%'") ;                   
               }
               else
               {
                   window.parent.frames["main"].location= "DefDeptList.aspx?DeptPWBS="+item.id+"&condition="+escape("DeptPWBS='"+item.id+"'") ;
               }
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
          <%=treedata %>;
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

                    var ret = _curClass.AddSonDept(nodeName,nodes[0].id);
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
                    if(!confirm("确认删除吗？"))
                        return false;

                    $.each(nodes , function(i,n){
                        var ret = _curClass.RemoveDept(n.value);
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
            $("#btnFresh").click(function(){
                window.location.reload();
            });
        });
    </script>
		</form>
	</body>
</html>
