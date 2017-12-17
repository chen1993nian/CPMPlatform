<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DocFrame.aspx.cs" Inherits="Studio.JZY.Doc.DocFrame" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>文档管理</title>
    <link rel="stylesheet" type="text/css" href="../css/defstyle.css" />    
    <link rel="stylesheet" type="text/css" href="../css/tree.css" />
    <link rel="stylesheet" type="text/css" href="../css/doc.css" />
    <link rel="stylesheet" type="text/css" href="../css/smartMenu.css" />
    <link rel="stylesheet" type="text/css" href="../css/ymPrompt_green/ymPrompt.css"/>
    <link rel="stylesheet" type="text/css" href="../grid/css/flexigrid.css"/>
    <script type="text/javascript" src="../js/jquery-1.4.2.min.js" ></script>
    <script type="text/javascript" src="../DatePicker/WdatePicker.js"></script> 
    <script type="text/javascript" src="../js/jquery.tree.js" ></script>
    <script type="text/javascript" src="../grid/flexigrid.js"></script>
    <script type="text/javascript" src="../js/ymPrompt.js"></script>
    <script type="text/javascript" src="../js/jquery.smartMenu.js"></script>
     <style type="text/css">
     	#tree
	    {
	        border:#c3daf9 1px solid;
	        padding:5px;
	        margin:5px;
	        float:left;
	        width:200px;
	    }
	    #griddiv
	    {
            margin:1px 0px 0px 200px;
	    }
	    body{
	    	margin:0px;
	    	}
	    div.iDialog{
	    	    margin-left:30px;
	    	    margin-top:30px;
	    	}

     </style>    
	</head>
	<body scroll="no">
		<form id="Form1" method="post" runat="server">
        <div id="tree"> 
        </div>
        <div id="griddiv" >
            <table id="flex1"  style="display:none"></table>    
        </div>
    <script type="text/javascript">
        var treeObj = null;
        var userAgent = window.navigator.userAgent.toLowerCase();
        $.browser.msie8 = $.browser.msie && /msie 8\.0/i.test(userAgent);
        $.browser.msie7 = $.browser.msie && /msie 7\.0/i.test(userAgent);
        $.browser.msie6 = !$.browser.msie8 && !$.browser.msie7 && $.browser.msie && /msie 6\.0/i.test(userAgent);
        <%=treedata %>;
        function load() {
            var o = {
                showcheck: false,
                onnodeclick: function (item) {
                    loadData(item.id);
                },
                blankpath: "../Img/common/",
                cbiconpath: "../Img/common/"
            };
            o.data = treedata;
            treeObj = $("#tree").treeview(o);

        }
        if ($.browser.msie6) {
            load();
        }
        else {
            $(document).ready(load);
        }

        function newFolder(objid, objname)//增加子节点
        {
            var newNode = {
                id: "" + objid,
                text: objname,
                value: objid,
                showcheck: false,
                isexpand: true,
                checkstate: 0,
                hasChildren: false,
                ChildNodes: null,
                complete: true
            };
            treeObj.addNode(newNode);
        }
        function changeName(objid, objname)//修改名称
        {
            treeObj.changeText(objid, objname);
        }

    </script>

    <script type="text/javascript">
<!--

    var AppDefault = Studio.JZY.Doc.DocFrame;
    var maiheight = document.documentElement.clientHeight;
    var otherpm = 120; //GridHead，toolbar，footer,gridmargin
    var gh = maiheight - otherpm;
    var gridData;
    $("#flex1").flexigrid
        (
        {
            url: '../getdata.ashx',
            params: [{ name: "queryid", value: "doc_list" }
                    , { name: "condition", value: "folderId='<%=treeId %>'" }
			    ],
			    colModel: [
				{ display: '序号', name: 'rowindex', width: 30, sortable: false, align: 'center', renderer: tranchk },
				{ display: '', name: 'filetype', width: 20, sortable: false, align: 'left', renderer: transtype },
				{ display: '档案目录名称', name: 'filename', width: 300, sortable: false, align: 'left', renderer: transname },
                { display: '创建时间', name: 'createtime', width: 120, sortable: false, align: 'right' }
			    ],
			    buttons: [
				{ name: '返回上级', bclass: 'folderback', onpress: upFolder },
                { separator: true },
				{ name: '添加子分类', bclass: 'newfolder', onpress: addFolder },
				{ name: '删除', bclass: 'delete', onpress: app_delete },
				{ separator: true },
				{ name: '查询', bclass: 'view', onpress: app_query },
				{ name: '清空', bclass: 'clear', onpress: app_reset },
				{ separator: true }
			    ],
			    searchitems: [
			    { display: '档案目录名称', name: 'filename', type: 1 },
                { display: '创建时间', name: 'createtime', type: 4 }
			    ],
			    sortname: "",
			    sortorder: "",
			    usepager: true,
			    singleSelect: false,
			    autoload: true,
			    useRp: true,
			    rp: 10,
			    multisel: true,
			    showTableToggleBtn: false,
			    resizable: false,
			    height: gh,
			    onError: showError,
			    onSuccess: initMenu,
			    preProcess: processData
			}
		);
			    var g = 1024 * 1024 * 1024;
			    var m = 1024 * 1024;
			    var k = 1024;
			    function transize(fldval, row) {
			        if (parseInt(fldval) > g) {
			            return (parseInt(fldval) / g).toFixed(2) + " G";
			        }
			        else if (parseInt(fldval) > m) {
			            return (parseInt(fldval) / m).toFixed(2) + " M";
			        }
			        else if (parseInt(fldval) > k) {
			            return (parseInt(fldval) / k).toFixed(2) + " K";
			        }
			        else if (fldval != "0") {
			            return fldval + " B";
			        }
			        else {
			            return "";
			        }
			    }
			    function transtype(fldval, row) {

			        if (fldval == "") {
			            return "<div class='flag folder'></div>";
			        }
			        else {
			            return "<div class='flag " + fldval.substring(1) + "'></div>";
			        }
			    }

			    function processData(data) {
			        gridData = data;
			        return data;
			    }

			    function transname(fldval, row) {
			        var fileid = $("fileid", row).text();
			        var filetype = $("filetype", row).text();
			        if (filetype == "")
			            return "<a href=\"javascript:openFolder('" + fileid + "');\"  class='foldername' target='_self'>" + fldval + "</a>";
			        else
			            return "<a href='../sysfolder/common/filedown.aspx?fileid=" + fileid + "' class='filename' target='_self'>" + fldval + "</a>";

			    }

			    function app_delete(cmd, grid) {
			        if ($('.trSelected', grid).length > 0) {
			            var arr = [];
			            var arr2 = [];
			            if (confirm('确定删除这' + $('.trSelected', grid).length + '条记录吗?')) {
			                $('.trSelected', grid).each
                                (
                                    function () {
                                        var id = this.id.substr(3);
                                        var row = $(gridData).find("#" + id);
                                        var filetype = $("filetype", row).text();
                                        if (filetype == "") {
                                            arr2.push(id);
                                        }
                                        else {
                                            arr.push(id);
                                        }
                                    }
                                );
			                var ret = AppDefault.DeleteFile(arr, arr2);
			                if (ret.error) {
			                    alert("删除记录时出错:" + ret.error.Message);
			                }
			                else {
			                    $("#flex1").flexReload();
			                }
			            }
			        }
			        else {
			            alert("请选中一条记录");
			        }
			    }

			    function upLoadFile() {
			        var item = treeObj.getCurItem();
			        if (item) {
			            ymPrompt.win({
			                message: '../SysFolder/Common/FileUpload.aspx?folderId=' + item.id,
			                width: 500,
			                height: 300,
			                title: '上传附件',
			                handler: function () {
			                    $("#flex1").flexReload();
			                },
			                maxBtn: true,
			                minBtn: true,
			                iframe: true
			            });
			        }
			    }

			    function loadData(folderId) {

			        $("#flex1").flexOptions({
			            url: '../getdata.ashx?t=' + Math.random(),
			            params: [{ name: "queryid", value: "doc_list" }, { name: "condition", value: "folderId='" + folderId + "'" }]
			        });
			        $("#flex1").flexReload();
			    }

			    function upFolder() {
			        if (treeObj.getCurItem().parent) {
			            var p = treeObj.getCurItem().parent;
			            treeObj.expandNode(p.id);
			            //loadData(p.id);
			        }
			    }

			    function openFolder(folderId) {
			        treeObj.expandNode(folderId);
			        //loadData(folderId);
			    }

			    function tranchk(fldval, row) {
			        return "<input type='checkbox' onclick='selectchk(this);' class='selectchk' value='" + fldval + "'/>";
			    }

			    function selectchk(obj) {
			        if (obj.checked) {
			            $(obj).closest("tr").addClass("trSelected");
			        }
			        else {
			            $(obj).closest("tr").removeClass("trSelected");
			        }
			    }

			    function app_selall() {
			        $(".selectchk").each(function () {
			            this.checked = true;
			            $(this).closest("tr").addClass("trSelected");
			        })
			    }

			    var curNode;
			    function addFolder() {
			        if (!jQuery("#tree").getCurItem()) {
			            alert("请先选择一个分类");
			            return;
			        }
			        curNode = jQuery("#tree").getCurItem();
			        var html = [];
			        html.push("<div class='iDialog'>");
			        html.push("<label for='folderName'>分类名称：</label>");
			        html.push("<input id='folderName' type=text style='float:none;' class='textbox'/>");
			        html.push("<br/><br/>");
			        html.push("<label for='folderOrder'>排 序：&nbsp;&nbsp;&nbsp;</label>");
			        html.push("<input id='folderOrder' type=text style='float:none;' class='textbox'/>");
			        html.push("</div>");
			        ymPrompt.win({
			            title: "创建[" + curNode.text + "]子级分类",
			            message: html.join(""),
			            showShadow: true,
			            winPos: [300, 100],
			            width: 300,
			            height: 200,
			            btn: [['确定', 'ok'], ['取消', 'cancel']],
			            handler: handler2
			        });
			    }

			    function handler2(tp) {
			        if (tp == 'ok') {

			            var pId = curNode.id;
			            var folderName = jQuery("#folderName").val();
			            var ret = AppDefault.NewFolder(folderName, pId);
			            if (ret.error) {
			                alert("新建文件夹出错");
			            }
			            else {
			                newFolder(ret.value, folderName);
			            }
			        }
			    }

			    function showError(data) {
			        //alert("加载数据出错");
			    }

			    function app_reset(cmd, grid) {
			        $("#flex1").clearQueryForm();
			    }

			    var contextMenu = [[/*{
            text: "下载",
            func: function () {

            }
        }
            , { text: "重命名",
                func: function () {

                }
            }
        , */{
            text: "编辑",
            func: function () {


                return;
                openCenter("../SysFolder/Common/WebOffice.aspx?fileId=" + trId, "_blank", 800, 600);
            }
        }
			    ]];
			    var trId = "";
			    function initMenu() {
			        jQuery("#flex1 tr").smartMenu(contextMenu

                    , {
                        name: "contextMenu"
                        , beforeShow: function () {
                            trId = $(event.srcElement).closest("tr")[0].id.substr(3);
                        }
                    }

                    );
			    }

			    function app_add(cmd, grid) {
			        if ($('.trSelected', grid).length > 0) {
			            if (confirm('确定导入这' + $('.trSelected', grid).length + '条记录吗?')) {

			                var curNode = $("#tree").getCurItem();
			                var keys = [];
			                $('.trSelected', grid).each
                            (
                                function () {
                                    var tblName = this.id.substr(3);
                                    keys.push(tblName);

                                }
                            );
			                if (keys.length > 0) {

			                }
			            }
			        }
			        else {
			            alert("请选中一条记录");
			        }
			    }

			    function app_query() {
			        $("#flex1").flexReload();
			    }

			    function app_reset(cmd, grid) {
			        $("#flex1").clearQueryForm();
			    }

			    function openCenter(url, name, width, height) {
			        var str = "height=" + height + ",innerHeight=" + height + ",width=" + width + ",innerWidth=" + width;
			        if (window.screen) {
			            var ah = screen.availHeight - 30;
			            var aw = screen.availWidth - 10;
			            var xc = (aw - width) / 2;
			            var yc = (ah - height) / 2;
			            str += ",left=" + xc + ",screenX=" + xc + ",top=" + yc + ",screenY=" + yc;
			            str += ",resizable=yes,scrollbars=yes,directories=no,status=no,toolbar=no,menubar=no,location=no";
			        }
			        return window.open(url, name, str);
			    }
			    //-->
</script>
		</form>
	</body>
</html>

