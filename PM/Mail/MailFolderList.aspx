<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MailFolderList.aspx.cs" Inherits="EIS.Web.MailFolderList" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>自定义文件夹</title>
    <meta http-equiv="Pragma" content="no-cache"/>
    <link rel="stylesheet" type="text/css" href="../grid/css/flexigrid.css"/>
    <link rel="alternate stylesheet" type="text/css" href="../css/zxxboxv3.css" media="screen" />
    <link rel="stylesheet" type="text/css" href="../Css/DefStyle.css"/>
    <script type="text/javascript" src="../grid/lib/jquery/jquery.js"></script>
    <script type="text/javascript" src="../grid/flexigrid.js"></script>
    <script language="javascript" type="text/javascript" src="../js/jquery.zxxbox.3.0-min.js"></script>
    <style type="text/css">
        #folderForm{padding-left:30px;padding-top:20px;height:160px;display:none;}
        #folderForm label{display:block;float:left;width:80px;line-height:26px;}
        #folderForm p{height:26px;}
        input[type=button]{padding:2px;height:25px;}
    </style>
</head>
<body style="margin:2px">
    <form id="form1" runat="server">
    <div id="griddiv">
        <table id="flex1" style="display:none"></table>    
    </div>
    <div id="folderForm">
        <div>
            <p>
            <label>文件夹名称：</label>            
            <input id="folderName" class="textbox" type="text" value="" />
            </p>
            <p>
            <label>序号：</label>
            <input id="folderSn" class="textbox" type="text" value="" />
            </p>
        </div>
        <div style="text-align:left;padding-left:80px;height:50px;">
            <input type="button" id="btnOk" value=" 确定 " />&nbsp;&nbsp;&nbsp;&nbsp;
            <input type="button" id="btnCancel" value=" 取消 " />
        </div>
    </div>
    </form>
</body>
</html>
<script type="text/javascript">
<!--
    var editflag = "";
    $(function () {
        $("#btnOk").click(function () {
            var folderName = $("#folderName").val();
            var sn = $("#folderSn").val();
            if (folderName == "" || sn == "") {
                alert("文件夹名或者序号不能为空");
                return;
            }
            var ret = "";
            if (editflag) {
                ret = _curClass.ChangeFolder(editflag, folderName, sn);
            }
            else {
                ret = _curClass.NewFolder(folderName, sn);
            }
            if (ret.error) {
                alert(ret.error.Message);
            }
            else {
                app_query();
            }
            $.zxxbox.hide();
        });
        $("#btnCancel").click(function () {

            $.zxxbox.hide();
        });
        if ("<%=Request["newfolder"] %>") {
            app_add();
        }
    });
    var _curClass = EIS.Web.Mail.MailFolderList;
    var maiheight = document.documentElement.clientHeight;
    var otherpm = 150; //GridHead，toolbar，footer,gridmargin
    var gh = maiheight - otherpm;

    $("#flex1").flexigrid
    (
    {
        url: '../getdata.ashx',
        params: [{ name: "queryid", value: "mail_folder" }
			    , { name: "condition", value: "" }
        ],
        colModel: [
			{ display: '序号', name: 'rowindex', width: 30, sortable: false, align: 'center' },
			{ display: '文件夹名称', name: 'foldername', width: 180, sortable: true, align: 'left' },
			{ display: '排序', name: 'sn', width: 60, sortable: true, align: 'center' }
        ],
        buttons: [
			{ name: '添加', bclass: 'add', onpress: app_add },
			{ name: '修改', bclass: 'edit', onpress: app_edit },
			{ name: '删除', bclass: 'delete', onpress: app_delete },
			{ separator: true },
			{ name: '查询', bclass: 'view', onpress: app_query },
			{ name: '清空', bclass: 'clear', onpress: app_reset }
        ],
        searchitems: [
			{ display: '文件夹名称', name: 'foldername', type: 1 }
        ],
        sortname: "",
        sortorder: "",
        usepager: true,
        singleSelect: true,
        useRp: true,
        rp: 15,
        multisel: false,
        showTableToggleBtn: false,
        resizable: false,
        height: gh,
        onError: showError,
        preProcess: processData
    }
    );
    var gridData;
    function processData(data) {
        gridData = data;
        return data;
    }
    function showError(data) {
        //alert("加载数据出错");
    }
    function app_add(cmd, grid) {
        editflag = "";
        $("#folderForm").show();
        $.zxxbox("#folderForm", { title: "新建文件夹", width: 300, height: 400 });
    }
    function app_reset(cmd, grid) {
        $("#flex1").clearQueryForm();
    }
    function app_edit(cmd, grid) {
        if ($('.trSelected', grid).length > 0) {

            editflag = $('.trSelected', grid)[0].id.substr(3);
            var folderName = $("#" + editflag, gridData).find("foldername").text();
            var sn = $("#" + editflag, gridData).find("sn").text();
            $("#folderName").val(folderName);
            $("#folderSn").val(sn);
            $("#folderForm").show();
            $.zxxbox("#folderForm", { title: "修改文件夹", width: 300, height: 400 });
        }
        else {
            alert("请选中一条记录");
        }
    }
    function app_delete(cmd, grid) {
        if ($('.trSelected', grid).length > 0) {
            if (confirm('确定删除这' + $('.trSelected', grid).length + '条记录吗?')) {
                $('.trSelected', grid).each
                (
                    function () {
                        var ret = _curClass.DelFolder(this.id.substr(3));
                        if (ret.error) {
                            alert(ret.error.Message);
                        }
                    }
                );
                $("#flex1").flexReload();
            }
        }
        else {
            alert("请选中一条记录");
        }
    }
    function app_query() {
        $("#flex1").flexReload();
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

