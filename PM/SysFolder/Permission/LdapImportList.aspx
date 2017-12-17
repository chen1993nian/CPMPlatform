<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LdapImportList.aspx.cs" Inherits="EIS.Web.SysFolder.Permission.LdapImportList" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>导入员工</title>
    <link href="../../css/defstyle.css" rel="stylesheet" type="text/css" />    
    <link href="../../css/tree.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="../../grid/css/flexigrid.css"/>
    <script src="../../js/jquery-1.4.2.min.js" type="text/javascript"></script>
    <script src="../../js/jquery.tree.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../grid/flexigrid.js"></script>
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
     </style>    
	</head>
	<body>
		<form id="Form1" method="post" runat="server">
        <div id="tree"> 
        </div>
        <div id="griddiv" >
            <table id="flex1"  style="display:none"></table>    
        </div>
        <input type=hidden id="posName" />
        <input type=hidden id="posId" />
    <script type="text/javascript">
        var userAgent = window.navigator.userAgent.toLowerCase();
        $.browser.msie8 = $.browser.msie && /msie 8\.0/i.test(userAgent);
        $.browser.msie7 = $.browser.msie && /msie 7\.0/i.test(userAgent);
        $.browser.msie6 = !$.browser.msie8 && !$.browser.msie7 && $.browser.msie && /msie 6\.0/i.test(userAgent);
        <%=treedata %>;
        function load() {
            var o = {
                showcheck: false,
                onnodeclick: function (item) {
                    $("#flex1").flexOptions({
                        url: 'LdapGetUserXml.aspx?t=' + Math.random(),
                        params: [{ name: "oupath", value: item.value }]
                    });
                    $("#flex1").flexReload();
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

    <script type="text/javascript">
<!--
        var _curClass = EIS.Web.SysFolder.Permission.LdapImportList;
    var maiheight = document.documentElement.clientHeight;
    var otherpm = 120; //GridHead，toolbar，footer,gridmargin
    var gh = maiheight - otherpm;

    $("#flex1").flexigrid
        (
        {
            url: 'LdapGetUserXml.aspx',
            params: [{ name: "oupath", value: "" }
            ],
            colModel: [
            { display: '序号', name: 'rowindex', width: 30, sortable: false, align: 'center', renderer: tranchk },
            { display: '账号', name: 'sAMAccountName', width: 100, sortable: false, align: 'left' },
            { display: '姓名', name: 'displayName', width: 100, sortable: false, align: 'left' },
            { display: '已经导入', name: 'exist', width: 100, sortable: false, align: 'center' },
            { display: '导入', name: 'objectGUID', width: 80, sortable: false, align: 'center', renderer: transfld }
            ],
            buttons: [
            { name: '全部选中', bclass: 'view', onpress: app_selall },
            { name: '批量导入', bclass: 'add', onpress: app_add },
            { separator: true }
            ],
            sortname: "",
            sortorder: "",
            usepager: true,
            singleSelect: false,
            autoload: false,
            useRp: true,
            rp: 10,
            multisel: true,
            showTableToggleBtn: false,
            resizable: false,
            height: gh,
            onError: showError
        }
    );
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
    function transfld(fldval, row) {
        return "<a class='normal' href=\"javascript:importUser('" + fldval + "')\" >导入</a>";
    }
    function setPos(posId, posName) {
        $("#posId").val(posId);
        $("#posName").val(posName);
        $("span.add").html("导入到【" + posName + "】");
    }
    function importUser(objectGUID) {

        var curNode = $("#tree").getCurItem();

        if ($("#posId").val() == "") {
            alert("请右边选择要导入的岗位");
            return;
        }
        var ret = _curClass.ImportUser($("#posId").val(), curNode.value, objectGUID);
        if (ret.error) {
            alert(ret.error.Message);
        }
        else {
            alert("导入成功！");
            $("#flex1").flexReload();
        }

    }
    function showError(data) {
        //alert("加载数据出错");
    }
    function app_reset(cmd, grid) {
        $("#flex1").clearQueryForm();
    }

    function app_add(cmd, grid) {
        if ($("#posId").val() == "") {
            alert("请右边选择要导入的岗位");
            return;
        }
        if ($('.trSelected', grid).length > 0) {
            if (confirm('确定导入这' + $('.trSelected', grid).length + '位员工吗?')) {

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
                    var ret = _curClass.ImportUser($("#posId").val(), curNode.value, keys.join(","));
                    if (ret.error) {
                        alert(ret.error.Message);
                    }
                    else {
                        alert("导入成功！");
                        $("#flex1").flexReload();
                    }
                }
            }
        }
        else {
            alert("请选中一条记录");
        }
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
