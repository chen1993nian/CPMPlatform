<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DefDeptLimitLeft.aspx.cs" Inherits="EIS.Web.SysFolder.Permission.DefDeptLimitLeft" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>组织部门</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />    
	<link rel="stylesheet" href="../../css/appStyle.css?v=1" type="text/css"/>
	<link rel="stylesheet" href="../../css/zTreeStyle/zTreeStyle.css" type="text/css"/>
	<script type="text/javascript" src="../../js/jquery-1.4.2.min.js"></script>
	<script type="text/javascript" src="../../js/jquery.ztree.all-3.4.min.js"></script>

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
            <button id="btnFresh" type="button"><img alt="刷新" src="../../img/common/fresh.png" />刷新</button>
            <input type="text" style="width:80px;" class="search" id="txtSearch" />
            <button id="btnSearch" type="button"><img alt="查找" src="../../img/common/search.png" />查找</button>
        </div>
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
                      onClick:nodeClick
                  }
              };
              function nodeClick(e,treeId,node)
              {

                  var d=new Date();
                  window.parent.frames["main"].location = "DefDeptLimitEdit.aspx?roleId="+node.value+ "&rolename="+escape(node.name)+"&t="+d.getMilliseconds();

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
              var searchKey = "";
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
    </script>
		</form>
	</body>
</html>