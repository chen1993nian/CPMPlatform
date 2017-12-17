<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DeptTree2.aspx.cs" Inherits="Studio.JZY.SysFolder.Common.DeptTree2" %>

<!DOCTYPE html>
  

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>组织机构选择</title>
     <link href="../../css/appstyle.css" rel="stylesheet" type="text/css" />    
     <link href="../../css/tree.css" rel="stylesheet" type="text/css" />
     <style type="text/css">
        body{overflow:hidden;}
     	#tree
	    {
	        border:#c3daf9 1px solid;
	        padding:5px;
	        margin:5px;
	        overflow:auto;
	        background-color: rgb(249, 250, 254);
	        height:440px;
	    }
	    fieldset input{padding:3px;}
	    fieldset{
	        text-align:right;
	        padding-right: 5px; 
	        padding-left: 10px; 
	        padding-bottom: 2px; 
	        padding-top: 2px; 
	        background: #f2f4fb url(../../img/common/site.png) no-repeat 10px center;
	        }
	    fieldset span{color:#3a6ea5;font-weight:bold;float:left;line-height:30px;padding-left:30px;} 
     </style>    
	</head>
	<body >
		<form id="Form1" method="post" runat="server">
        <fieldset style="margin:5px;padding:3px;background-color: #f2f4fb;">
            <span>请选择下面的部门</span>
            <input type="button" class="defaultbtn"  id="btnconfirm" value=" 确定返回 "/> &nbsp;
            <input type="button" class="defaultbtn" id="btndel" onclick="window.close();"  value=" 关闭窗口 "/>
            
		</fieldset>
        <div id="tree">
        </div>

    <script src="../../js/jquery-1.7.min.js" type="text/javascript"></script>
    <script src="../../js/jquery.tree.js" type="text/javascript"></script>
   
    <script type="text/javascript">
        jQuery(function () {
            $(window).resize(function () {
                $("#tree").height($(document.body).height() - 100);
            });
            $("#tree").height($(document.body).height() - 100);
            $("#btnconfirm").click(function (e) {

                var cid = "<%=cid %>";
                    var bizfields = cid.split(",");
                    var qryfields = "<%=queryfield %>".split(",");

                var index_id = jQuery.inArray("deptid", qryfields);
                    //   alert("index_id" + index_id);
                var index_code = jQuery.inArray("deptcode", qryfields);
                    //    alert("index_code" + index_code);
                var index_name = jQuery.inArray("deptname", qryfields);
                    //   alert("index_name" + index_name);
                var index_wbs = jQuery.inArray("deptwbs", qryfields);
                    //  alert("index_wbs" + index_wbs);

                var names = [];
                var ids = [];
                var wbss = [];
                var codes = [];
                var nodes = $("#tree").getCurItem();

                if (nodes) {
                    var vs = nodes.value.split("|");
                    if (index_id > -1) {
                        //        alert("id");
                        ids.push(vs[0]);
                    }
                    if (index_name > -1) {
                        //        alert("name");
                        names.push(nodes.text);
                    }
                    if (index_wbs > -1) {
                        //         alert("wbs");
                        wbss.push(nodes.id);
                    }
                    if (index_code > -1) {
                        //        alert("code");
                        codes.push(vs[1]);
                    }
                }

                if (index_id > -1) {
                    var pctl = window.opener.document.getElementById(bizfields[index_id]);
                    if (pctl) {
                        pctl.value = ids;
                        try { window.opener.$(pctl).change(); } catch (e) { }
                    }
                }
                if (index_name > -1) {
                    var pctl = window.opener.document.getElementById(bizfields[index_name]);
                    if (pctl) {
                        pctl.value = names;
                        try { window.opener.$(pctl).change(); } catch (e) { }
                    }
                }
                if (index_wbs > -1) {

                    var pctl = window.opener.document.getElementById(bizfields[index_wbs]);
                    if (pctl) {
                        pctl.value = wbss;
                        try { window.opener.$(pctl).change(); } catch (e) { }
                    }
                }
                if (index_code > -1) {
                    var pctl = window.opener.document.getElementById(bizfields[index_code]);
                    if (pctl) {
                        pctl.value = codes;
                        try { window.opener.$(pctl).change(); } catch (e) { }
                    }
                }
                window.close();
                });

        });
        var userAgent = window.navigator.userAgent.toLowerCase();
        $.browser.msie8 = $.browser.msie && /msie 8\.0/i.test(userAgent);
        $.browser.msie7 = $.browser.msie && /msie 7\.0/i.test(userAgent);
        $.browser.msie6 = !$.browser.msie8 && !$.browser.msie7 && $.browser.msie && /msie 6\.0/i.test(userAgent);
        <%=treeData %>
        function load() {
            var o = {
                showcheck: false,
                onnodedblclick: function (item) {
                    $("#btnconfirm").click();
                },
                blankpath: "../../Img/common/",
                cbiconpath: "../../Img/common/"
            };
            o.data = treedata;
            $("#tree").treeview(o);
        

        }
        if ($.browser.msie6) {
            load();
        }
        else {
            $(document).ready(load);
        }
    </script>
		</form>
	</body>
</html>

