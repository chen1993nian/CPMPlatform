<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DefTableStyleList.aspx.cs" Inherits="EIS.Studio.SysFolder.DefFrame.DefTableStyleList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>编辑业务样式</title>
    <meta http-equiv="Pragma" content="no-cache" />
    <link rel="stylesheet" type="text/css" href="../../grid/css/flexigrid.css" />
    <link rel="stylesheet" type="text/css" href="../../Css/DefStyle.css" />
    <script src="../../Js/jquery-1.7.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../DatePicker/WdatePicker.js"></script>
    <script type="text/javascript" src="../../grid/flexigrid.js"></script>
    <script src="../../Js/Tools.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../js/jquery.notice.js"></script>
    <style type="text/css">
        .flexigrid .red{color:Red;text-decoration:none;}
        .flexigrid .green{color:green;text-decoration:none;}
        .styleName{border-width:0px;height:22px;line-height:22px;padding-left:3px;}
    </style>
</head>
<body style="margin: 2px">
    <form id="form1" runat="server">
    <div id="griddiv" name="griddiv">
        <table id="flex1" style="display: none">
        </table>
    </div>
    </form>
</body>
</html>
<script type="text/javascript">
<!--

    var maiheight = document.documentElement.clientHeight;
    var otherpm = 150; //GridHead，toolbar，footer,gridmargin
    var gh = maiheight - otherpm;
    var _curClass = EIS.Studio.SysFolder.DefFrame.DefTableStyleList;
    $("#flex1").flexigrid
			({
			    url: '../../getdata.ashx',
			    params: [{ name: "queryid", value: "tablestylelist" }
			        , { name: "condition", value: "TableName='<%=tblName %>'" }
                    , { name: "defaultvalue", value: "\"@tablename\":\"<%=tblName %>\"" }
			    ],
			    colModel: [
				{ display: '索引', name: 'styleindex', width: 60, sortable: true, align: 'center' },
				{ display: '样式名称', name: 'stylename', width: 200, sortable: true, align: 'left', renderer: fnName },
				{ display: '创建日期', name: 'createdate', width: 100, sortable: false, align: 'right' },
				{ display: '最后修改日期', name: 'updatetime', width: 100, sortable: false, align: 'right' },
				{ display: '编辑样式', name: 'styleindex', width: 240, sortable: false, align: 'center', renderer: sonTable }
			    ],
			    buttons: [
				{ name: '添加', bclass: 'add', onpress: app_add },
				{ name: '删除', bclass: 'delete', onpress: app_delete },
				{ separator: true },
				{ name: '查询', bclass: 'view', onpress: app_query },
				{ name: '清空', bclass: 'clear', onpress: app_reset }
			    ],
			    searchitems: [
			    { display: '样式名称', name: 'stylename', type: 1 },
			    { display: '创建时间', name: '_CreateTime', type: 4 }
			    ],
			    sortname: "",
			    sortorder: "",
			    usepager: true,
			    singleSelect: true,
			    useRp: true,
			    rp: 20,
			    multisel: false,
			    showTableToggleBtn: false,
			    resizable: false,
			    height: gh,
			    onError: showError
			}
			);
            function sonTable(fldval, row) {

                var arr = [];
                arr.push("<a class='normal red' href='DefTableUI2.aspx?tblName=<%=tblName %>&styleindex=" + fldval + "&t=1" + "'>编辑界面</a>");
        arr.push("<a class='normal green' href='DefTableUI3.aspx?tblName=<%=tblName %>&styleindex=" + fldval + "&t=2" + "'><b>手机界面</b></a>");
	    arr.push("<a class='normal red' href='DefTableUI4.aspx?tblName=<%=tblName %>&styleindex=" + fldval + "&t=3" + "'>打印界面</a>");
	    arr.push("<a class='normal green' href='DefTableUI2.aspx?tblName=<%=tblName %>&styleindex=" + fldval + "&t=4" + "'><b>查看界面</b></a>");

	    return arr.join("&nbsp;&nbsp;");
	}
	function fnName(v, row) {
	    var styleid = $(row).attr("id");
	    if (styleid == "")
	        return v
	    else
	        return "<input type='text' onchange='styleChange(this);' class='styleName' value='" + v + "' styleIdx='" + styleid + "'/>";
	}
	function styleChange(obj) {
	    var index = $(obj).attr("styleIdx");
	    var ret = _curClass.UpdateName(index, obj.value);
	    if (ret.error) {
	        alert("保存出错：" + ret.error.Message);
	    }
	    else {
	        $.noticeAdd({ text: '保存成功！', stay: false, stayTime: 200 });
	    }
	}
	function showError(data) {
	}
	function app_add(cmd, grid) {

	    var r = _curClass.AddStyle("<%=tblName %>");
        if (r.error) {
            alert(r.error.Message);
        }
        else {
            alert("新样式创建成功");
            $("#flex1").flexReload();
        }
        return;
        window.open("DefTableUI.aspx?tblName=<%=tblName %>", "_self");
    }
    function app_reset(cmd, grid) {
        $("#flex1").clearQueryForm();
    }

    function app_delete(cmd, grid) {
        if ($('.trSelected', grid).length > 0) {
            if (confirm('确定删除这' + $('.trSelected', grid).length + '条记录吗?')) {
                $('.trSelected', grid).each
			            (
			                function () {
			                    var ret = _curClass.DelRecord(this.id.substr(3));
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
    function app_setquery() {
        window.showModalDialog("AppConditionDef.aspx", "", "dialogHeight=600px;dialogWidth=800px;status=no;center=yes;resizable=yes;");
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
