<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DocShareFrame.aspx.cs" Inherits="Studio.JZY.Doc.DocShareFrame" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>文件转发接收列表</title>
    <link rel="stylesheet" type="text/css" href="../css/defstyle.css" />    
    <link rel="stylesheet" type="text/css" href="../css/doc.css" />
    <link rel="stylesheet" type="text/css" href="../css/smartMenu.css" />
    <link rel="stylesheet" type="text/css" href="../css/ymPrompt_green/ymPrompt.css"/>
    <link rel="stylesheet" type="text/css" href="../grid/css/flexigrid.css"/>
    <script type="text/javascript" src="../js/jquery-1.7.min.js" ></script>
    <script type="text/javascript" src="../DatePicker/WdatePicker.js"></script> 
    <script type="text/javascript" src="../grid/flexigrid.js"></script>
    <script type="text/javascript" src="../js/ymPrompt.js"></script>
    <script type="text/javascript" src="../js/jquery.smartMenu.js"></script>
     <style type="text/css">
	    body{
	    	margin:0px;padding:0px;
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
            //window.parent.frames["left"].openFolder("<%=folderId %>");
        });
    </script>

    <script type="text/javascript">
<!--

    var maiheight = document.documentElement.clientHeight;
    var otherpm = 120;
    var gh = maiheight - otherpm;
    var gridData;
    $("#flex1").flexigrid
        (
        {
            url: '../getdata.ashx',
            params: [{ name: "queryid", value: "folder_list" }
                    , { name: "condition", value: "charindex('<%=EmpId %>',ShareUserId,0)>0" }
			    ],
			    colModel: [
				{ display: '序号', name: 'rowindex', width: 30, sortable: false, align: 'center' },
				{ display: '', name: 'share', width: 20, sortable: false, align: 'left', renderer: transtype },
				{ display: '目录名称', name: 'folderName', width: 300, sortable: false, align: 'left', renderer: transname },
				{ display: '共享人', name: 'ownerName', width: 70, sortable: false, align: 'left' },
                { display: '创建时间', name: 'createtime', width: 120, sortable: false, align: 'center' }
			    ],
			    buttons: [
				{ name: '查询', bclass: 'view', onpress: app_query },
				{ name: '清空', bclass: 'clear', onpress: app_reset },
				{ separator: true },
				{ name: '共享文档列表', bclass: 'fullpath' }
			    ],
			    searchitems: [
			    { display: '目录名称', name: 'folderName', type: 1 },
			    { display: '共享人', name: 'ownerName', type: 1 }
			    ],
			    sortname: "folderName",
			    sortorder: "asc",
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

			    function transtype(v, row, td) {
			        return "<div class='flag foldershare'></div>";
			    }
			    function processData(data) {
			        gridData = data;
			        return data;
			    }
			    function transname(fldval, row, td) {
			        var fileid = $("folderId", row).text();
			        $(td).attr("fileId", fileid);
			        var filetype = $("filetype", row).text();
			        if (filetype == "") {
			            $(td).addClass("tdfolder");
			            return "<a href=\"javascript:openFolder('" + fileid + "');\"  class='foldername' target='_self'>" + fldval + "</a>";
			        }
			        else {
			            $(td).addClass("tdfile");
			            return "<a href='../sysfolder/common/filedown.aspx?fileid=" + fileid + "' class='filename' target='_self'>" + fldval + "</a>";
			        }

			    }
			    function app_FolderEdit(cmd, grid) {
			        openCenter("DocFolderEdit.aspx?folderId=<%=folderId %>", "_blank", 400, 300);
        }
        function app_FolderLimit() {
            openCenter("DocFolderShare.aspx?folderId=<%=folderId %>", "_blank", 500, 340);
        }
        function tranchk(fldval, row, td) {
            var filetype = $("filetype", row).text();
            filetype = filetype.length > 0 ? "chkfile" : "chkfolder";
            return "<input type='checkbox' onclick='selectchk(this);' class='selectchk " + filetype + "' value='" + fldval + "'/>";
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
        function getSelectFiles() {
            var arr = [];
            $('.chkfile:checked').each(function () {
                arr.push(this.value);
            });
            return arr;
        }
        function getSelectFolders() {
            var arr = [];
            $('.chkfolder:checked').each(function () {
                arr.push(this.value);
            });
            return arr;
        }
        function app_paste() {
            var ret = _curClass.PasteSelect("<%=folderId %>");
            if (ret.error) {
                alert("粘贴文件时出错:" + ret.error.Message);
            }
            else {
                $("#flex1").flexReload();
            }

        }
        function app_send() {
            var arr = getSelectFiles();
            var arr2 = [];
            if (arr.length == 0) {
                alert("请先选择对象");
                return;
            }
            var ret = "";
            ret = _curClass.SaveSelect("1", arr, arr2);
            if (ret.error) {
                alert("保存选中的记录时出错:" + ret.error.Message);
            }
            else {
                openCenter("DocFolderSend.aspx", "_blank", 500, 340);
            }

        }
        function app_select(cmd, grid) {
            var arr = getSelectFiles();
            var arr2 = getSelectFolders();
            if (arr.length == 0 && arr2.length == 0) {
                alert("请先选择对象");
                return;
            }
            var ret = "";
            if (cmd == "复制") {
                ret = _curClass.SaveSelect("1", arr, arr2);
            }
            else if (cmd == "剪切") {
                ret = _curClass.SaveSelect("2", arr, arr2);
            }

            if (ret.error) {
                alert("保存选中的记录时出错:" + ret.error.Message);
            }
            else {
                alert("选择的文件已经【" + cmd + "】\r\n请到目标目录中选择【粘贴】操作");
            }
        }
        function app_delete(cmd, grid) {

            var arr = getSelectFiles();
            var arr2 = getSelectFolders();
            if (arr.length == 0 && arr2.length == 0) {
                alert("请先选择对象");
                return;
            }
            if (confirm('您确定删除选择的文件吗?')) {

                var ret = _curClass.DeleteFile(arr, arr2);
                if (ret.error) {
                    alert("删除操作时出错:" + ret.error.Message);
                }
                else {
                    $("#flex1").flexReload();
                }
            }
        }

        function upLoadFile() {
            ymPrompt.win({
                message: '../SysFolder/Common/FileUpload.aspx?folderId=<%=folderId %>',
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
                params: [{ name: "queryid", value: "doc_list" }, { name: "condition", value: "folderId='" + folderId + "'" }]
            });
            $("#flex1").flexReload();
        }

        function upFolder() {
            if ("<%=folderPId %>") {
                openFolder("<%=folderPId %>");
            }
        }

        function openFolder(folderId) {
            window.open("DocShareList.aspx?folderId=" + folderId + "&t=" + Math.random(), "_self");
        }

        var curNode;
        function addFolder() {
            openCenter("DocFolderEdit.aspx?folderPId=<%=folderId %>", "_blank", 400, 300);
        }

        function handler2(tp) {
            if (tp == 'ok') {
                var pId = curNode.id;
                var folderName = jQuery("#folderName").val();
                var ret = _curClass.NewFolder(folderName, pId);
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




