<%@ Page language="c#" Codebehind="DeptTree.aspx.cs" AutoEventWireup="false" Inherits="EIS.WebBase.SysFolder.Common.DeptTree" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>组织机构选择</title>
    <link href="../../css/appstyle.css" rel="stylesheet" type="text/css" />    
    <link href="../../css/tree.css" rel="stylesheet" type="text/css" />
	<link rel="stylesheet" href="../../css/zTreeStyle/zTreeStyle.css" type="text/css"/>
    <script type="text/javascript" src="../../js/jquery-1.7.min.js"></script>
    <script type="text/javascript" src="../../js/jquery.tree.js"></script>
	<script type="text/javascript" src="../../js/jquery.ztree.all-3.4.min.js"></script>

     <style type="text/css">
         html{overflow:auto;}
         body{overflow:auto;}
     	#tree,#dTree
	    {
	        border:#c3daf9 1px solid;
	        padding:5px;
	        margin:5px;
	        overflow:auto;
	        background-color: rgb(249, 250, 254);
	    }
	    #dTree{display:none;}
	    fieldset{
	        text-align:right;
	        padding-right: 5px; 
	        padding-left: 10px; 
	        padding-bottom: 2px; 
	        padding-top: 2px; 
	        background: #f2f4fb url(../../img/common/site.png) no-repeat 10px center;
	        }
	    fieldset span{color:#3a6ea5;font-weight:bold;float:left;line-height:30px;padding-left:30px;} 
	    input[type=radio]{
	        vertical-align:middle;
	        }
	    label{cursor:pointer;padding-left:5px;}
	    .centerZone
		{
			border:#c3daf9 1px solid;
	        padding:5px;
	        margin-top:5px;
			height:430px;
			overflow:hidden;
			background:white;
		}
     </style>    
	</head>
	<body >
		<form id="Form1" method="post" runat="server">
        <fieldset >
            <span class="radioSpan">
                <input type="radio" name="selType" id="selType0"  value="0" checked="checked" /><label for="selType0">默认视图</label>&nbsp;&nbsp;&nbsp;&nbsp;
			    <input type="radio" name="selType" id="selType1"  value="1" /><label for="selType1">按部门属性</label>
            </span>

            <input type="button" class="defaultbtn"  id="btnConfirm" value="&nbsp;确定返回&nbsp;"/> &nbsp;
            <input type="button" class="defaultbtn" id="btnClose" onclick="window.close();"  value="&nbsp;关闭窗口&nbsp;"/>
		</fieldset>
        <div class="centerZone">
            <div id="tree">
            </div>
            <ul id="dTree" class="ztree">
            </ul>
        </div>
		</form>
   
    <script type="text/javascript">
        var setting = { 
            check: {
                enable: true
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
            dTree.checkNode(node,null,false);
        }
        var dTree;
        jQuery(function () {
            $(window).resize(function () {
                $("#tree,#dTree").height($(document.body).height() - 100);
            });
            $("#tree,#dTree").height($(document.body).height() - 100);

            //切换选择方式
            $(".radioSpan input").click(function(){
                var i=event.srcElement.value;
                $(".centerZone").children().hide();
                $(".centerZone").children(":eq("+i+")").show();
            });

            var zNodes =<%=deptTree %>;
		    $.fn.zTree.init($("#dTree"), setting, zNodes);
		    dTree = $.fn.zTree.getZTreeObj("dTree");
		    var root =dTree.getNodeByTId("tree_1");

            //确定事件
		    $("#btnConfirm").click(function(e){               
		        var names=[];
		        var fnames=[];
		        var ids=[];
		        var wbss=[]; 
		        var codes=[]; 
		        if($("#tree").is(":visible")){
		            var nodes = $("#tree").getTSNs();
		            for(var i=0;i<nodes.length;i++)
		            {
		                var vs=nodes[i].value.split("|");
		                if(index_id>-1)
		                    ids.push(vs[0]);
		                if(index_name>-1)
		                    names.push(nodes[i].text);
		                if(index_wbs>-1)
		                    wbss.push(nodes[i].id);
		                if(index_full>-1)
		                    fnames.push(vs[2]);
		                if(index_code>-1)
		                    codes.push(vs[1]);
		            }
		        }
		        else{
		            var nodes = dTree.getCheckedNodes(true);
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
		                if(index_full>-1)
		                    fnames.push(vs[2]);
		            }
		        }

		        if(index_id>-1){
		            var pctl=window.opener.document.getElementById(bizfields[index_id]);
		            if(pctl){
		                pctl.value=ids;
		                try { window.opener.$(pctl).change();} catch (e) {}
		            }
		        }
		        if(index_name>-1){
		            var pctl=window.opener.document.getElementById(bizfields[index_name]);
		            if(pctl){
		                pctl.value= names;
		                try { window.opener.$(pctl).change();} catch (e) {}
		            }
		        }
		        if(index_wbs>-1){
               
		            var pctl=window.opener.document.getElementById(bizfields[index_wbs]);
		            if(pctl){
		                pctl.value= wbss;
		                try { window.opener.$(pctl).change();} catch (e) {}
		            }
		        }
		        if(index_code>-1){
		            var pctl=window.opener.document.getElementById(bizfields[index_code]);
		            if(pctl){
		                pctl.value= codes;
		                try { window.opener.$(pctl).change();} catch (e) {}
		            }
		        }
		        if(index_full>-1){
		            var pctl=window.opener.document.getElementById(bizfields[index_full]);
		            if(pctl){
		                pctl.value= fnames;
		                try { window.opener.$(pctl).change();} catch (e) {}
		            }
		        }

		        if('<%=Request["callback"] %>'!="")
               {
                   window.opener["<%=Request["callback"] %>"](bizfields,ids,names);
               }       
                window.close();
            });
        });
       var userAgent = window.navigator.userAgent.toLowerCase();
       $.browser.msie8 = $.browser.msie && /msie 8\.0/i.test(userAgent);
       $.browser.msie7 = $.browser.msie && /msie 7\.0/i.test(userAgent);
       $.browser.msie6 = !$.browser.msie8 && !$.browser.msie7 && $.browser.msie && /msie 6\.0/i.test(userAgent);
       var cid="<%=cid %>";
        var bizfields=cid.split(",");
        var qryfields="<%=queryfield %>".split(",");
              
        var index_id = jQuery.inArray("deptid",qryfields);
        var index_code = jQuery.inArray("deptcode",qryfields);
        var index_name = jQuery.inArray("deptname",qryfields);
        var index_full = jQuery.inArray("fullname",qryfields);
        var index_wbs = jQuery.inArray("deptwbs",qryfields);

        <%=treeData %>
        function load() {        
            var o = { showcheck: true,
                onnodeclick:function(item){
                }, 
                blankpath:"../../Img/common/",
                cbiconpath:"../../Img/common/"
            };
            o.data = treedata;                  
            $("#tree").treeview(o);            

        }   
        if( $.browser.msie6)
        {
            load();
        }
        else{
            $(document).ready(load);
        }
    </script>
	</body>
</html>
