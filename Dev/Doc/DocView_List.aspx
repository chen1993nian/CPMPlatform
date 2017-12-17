<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DocView_List.aspx.cs" Inherits="Studio.JZY.Doc.DocView_List" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>文档管理</title>
    <link rel="stylesheet" type="text/css" href="../css/defstyle.css" />    
    <link rel="stylesheet" type="text/css" href="../css/doc.css" />
    <link rel="stylesheet" type="text/css" href="../css/smartMenu.css" />
    <link rel="stylesheet" type="text/css" href="../css/ymPrompt_green/ymPrompt.css"/>
    <link rel="stylesheet" type="text/css" href="../grid/css/flexigrid.css"/>
    <script type="text/javascript" src="../Js/jquery-1.7.min.js"></script>
    <script type="text/javascript" src="../DatePicker/WdatePicker.js"></script> 
    <script type="text/javascript" src="../grid/flexigrid.js"></script>
    <script type="text/javascript" src="../js/ymPrompt.js"></script>
    <script type="text/javascript" src="../js/jquery.smartMenu.js"></script>
     <style type="text/css">
         html{height:100%;}
	    body{
	    	margin:0px;padding:0px;
	    	height:100%;
	    	}
	    div.iDialog{
	    	    margin-left:30px;
	    	    margin-top:30px;
	    	}
	    .fullpath{text-align:left; color:Blue;}
        .smart_menu_box{width:80px;}
        .smart_menu_a{padding-left:10px;}
     </style>    
	</head>
	<body>
		<form id="Form1" method="post" runat="server">
        <div id="griddiv" >
            <table id="flex1"  style="display:none"></table>    
        </div>
    <script type="text/javascript">

        function changeName(objid, objname)//修改名称
        {
            window.parent.frames["left"].changeName(objid, objname);
        }
        function newFolder(objid, objname) {
            window.parent.frames["left"].newFolder([objid, objname, ""]);
        }
        jQuery(function () {
            try {
                window.parent.frames["left"].openFolder("<%=folderId %>");
            } catch (e) {

            }
        });
    </script>

    <script type="text/javascript">
<!--

    var AppDefault = Studio.JZY.Doc.DocList;
    var maiheight = jQuery(window).height();
    var otherpm = 120;
    var gh = maiheight - otherpm;
    var gridData;
    $("#flex1").flexigrid
    (
    {
        url: '../getdata.ashx',
        params: [{ name: "queryid", value: "doc_list" }
                , { name: "condition", value: "folderId='<%=folderId %>'" }
			    ],
			    colModel: [
				{ display: '序号', name: 'rowindex', width: 30, sortable: false, align: 'center', renderer: tranchk },
				{ display: '', name: 'filetype', width: 20, sortable: false, align: 'left', renderer: transtype },
				{ display: '文件名', name: 'filename', width: 300, sortable: false, align: 'left', renderer: transname },
				{ display: '所有者', name: 'owner', width: 70, sortable: false, align: 'left' },
                { display: '类型', name: 'filetype', width: 60, sortable: false, align: 'center' },
                { display: '大小', name: 'filesize', width: 60, sortable: false, align: 'right', renderer: transize },
                { display: '创建时间', name: 'createtime', width: 120, sortable: false, align: 'right' }
			    ],
			    buttons: [
				{ name: '返回上级', bclass: 'folderback', onpress: upFolder },
			    /*
			    { name: '新建文件夹', bclass: 'newfolder', onpress: addFolder },
			    { name: '属性', bclass: 'folderedit', onpress: app_FolderEdit, hidden: ("<%=folderId %>" == "0" ? 1 : 0) },
			    { name: '权限', bclass: 'limit', onpress: app_FolderLimit },
			    { separator: true },
			    { name: '上传文件', bclass: 'newfile', onpress: upLoadFile },
			    { name: '移动', bclass: 'move', onpress: app_selall },
			    { name: '复制', bclass: 'copy', onpress: app_selall },
			    { name: '粘贴', bclass: 'paste', onpress: app_selall },
			    { name: '删除', bclass: 'delete', onpress: app_delete },
			    */
			    {separator: true },
            { name: '查询', bclass: 'view', onpress: app_query },
            { name: '清空', bclass: 'clear', onpress: app_reset },
            { separator: true },
            { name: '路径：<%=fullPath %>', bclass: 'fullpath' }
        ],
        searchitems: [
        { display: '文件名', name: 'filename', type: 1 },
        { display: '上传人', name: 'subject', type: 1 },
        { display: '上传时间', name: '_createtime', type: 4 }
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
        function transtype(fldval, row, td) {
            if (fldval == "") {
                $(td).attr("flag", "folder");
                return "<div class='flag folder'></div>";
            }
            else {
                $(td).attr("flag", "file");
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
                return "<a href=\"javascript:openFolder('" + fileid + "');\"  class='foldername'>" + fldval + "</a>";
            else
                return "<a href='../sysfolder/common/filedown.aspx?fileid=" + fileid + "' class='filename' target='_blank'>" + fldval + "</a>";

        }
        function app_FolderEdit(cmd, grid) {
            openCenter("DocFolderEdit.aspx?folderId=<%=folderId %>", "_blank", 400, 300);
            //window.open("limit/DocLimitFrame.aspx?funId=<%=folderId %>", "_self");
        }
        function app_FolderLimit() {
            window.open("limit/DocLimitFrame.aspx?funId=<%=folderId %>", "_self");

            //openCenter("DocFolderShare.aspx?folderId=<%=folderId %>", "_blank", 500, 340);            

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
            ymPrompt.win({ message: '../SysFolder/Common/FileUpload.aspx?folderId=<%=folderId %>',
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

        function loadData(folderId) {

            $("#flex1").flexOptions({
                url: '../getdata.ashx?t=' + Math.random(),
                params: [{ name: "queryid", value: "doc_list" }, { name: "condition", value: "folderId='" + folderId + "'"}]
            });
            $("#flex1").flexReload();
        }

        function upFolder() {
            if ("<%=folderPId %>") {
                openFolder("<%=folderPId %>");
            }
        }

        function openFolder(folderId) {
            window.open("DocList.aspx?folderId=" + folderId + "&treeId=<%=treeId %>&time=" + Math.random(), "_self");
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
            openCenter("DocFolderEdit.aspx?folderPId=<%=folderId %>", "_blank", 400, 300);
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
        var trId = "";
        var flag = "";
        function initMenu() {
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
