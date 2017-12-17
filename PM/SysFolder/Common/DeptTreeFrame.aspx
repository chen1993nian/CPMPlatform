<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DeptTreeFrame.aspx.cs" Inherits="EIS.Web.SysFolder.Common.DeptTreeFrame" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>组织机构选择</title>
    <meta http-equiv="Pragma" content="no-cache"/>
    <link rel="stylesheet" type="text/css" href="../../css/zTreeStyle/zTreeStyle.css"/>
    <script type="text/javascript" src="../../js/jquery-1.7.min.js"></script>
	<script type="text/javascript" src="../../js/jquery.ztree.all-3.4.min.js"></script>
    <style type="text/css">
        html{height:100%;}
        body{margin:0px;padding:0px;overflow-y:scroll;overflow-x:auto;background-color: rgb(249, 250, 254);height:100%;}
        #dTree
	    {
	        border:#888 0px solid;
	        padding:5px;
	        overflow:visible;
            width:auto;
	        background-color: rgb(249, 250, 254);
            z-index:1000;
	    }
    </style>
</head>
<body>
    <ul id="dTree" class="ztree">
    </ul>
</body>
</html>
<script type="text/javascript">
    var ctlName="DeptName";
    var setting = { 
        check: {
            enable: false
        },
        edit: {
            drag:false,
            enable: false,
            showRenameBtn:false,
            showRemoveBtn:false,
            editNameSelectAll: false
        },
        callback:{
            onClick:zNodeClick
        },
        view:{showLine:false}
    };
    function zNodeClick(e,treeId,node,flag){
        fnReturn();
        window.parent.$.zbox.hide(true);
    }
    function fnReturn(){
        var cid="<%=base.GetParaValue("cid") %>";
        var method="<%=base.GetParaValue("method") %>";
        var bizfields=cid.split(",");
        var qryfields="<%=base.GetParaValue("queryfield") %>".split(",");
        
        var names=[],ids=[],wbss=[],codes=[]; 

        var index_id = jQuery.inArray("deptid",qryfields);
        var index_code = jQuery.inArray("deptcode",qryfields);
        var index_name = jQuery.inArray("deptname",qryfields);
        var index_wbs = jQuery.inArray("deptwbs",qryfields);


        var nodes;
        if(method == "2"){
            nodes = dTreeObj.getCheckedNodes(true);
        }
        else{
            nodes =dTreeObj.getSelectedNodes();
        }

        for(var i=0;i<nodes.length;i++)
        {
            if(nodes[i].value=="")
                continue;

            var vs=nodes[i].value.split("|");
            if(index_id>-1)
                ids.push(vs[0]);
            if(index_name>-1)
                names.push(nodes[i].name);
            if(index_wbs>-1)
                wbss.push(nodes[i].id);
            if(index_code>-1)
                codes.push(vs[1]);
        }

        if(index_id>-1)
            window.parent.document.getElementById(bizfields[index_id]).value=ids;
        if(index_name>-1)
            window.parent.document.getElementById(bizfields[index_name]).value=names;
        if(index_wbs>-1)
            window.parent.document.getElementById(bizfields[index_wbs]).value=wbss;
        if(index_code>-1)
            window.parent.document.getElementById(bizfields[index_code]).value=codes;
    }
    var zNodes =<%=deptTree %>;
    $.fn.zTree.init($("#dTree"), setting, zNodes);

    var dTreeObj = $.fn.zTree.getZTreeObj("dTree");
    var root =dTreeObj.getNodeByTId("dTree_1");
    jQuery(function(){
        $("input[name="+ctlName+"]").live("click",function(){
            var cityOffset = $(this).offset();
            $("#dTree").css({left:cityOffset.left + "px", top:(cityOffset.top + $(this).height()+6) + "px"}).show();
            $("body").bind("mousedown", onBodyDown);

        });
    });
    function hideMenu() {
        $("#dTree").hide();
        $("body").unbind("mousedown", onBodyDown);
    }
    function onBodyDown(event) {
        if (!(event.target.id == "menuBtn" || event.target.id == "dTree" || $(event.target).parents("#dTree").length>0)) {
            hideMenu();
        }
    }
</script>
