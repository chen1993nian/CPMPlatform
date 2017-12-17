
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AppOutSelect.aspx.cs" Inherits="EIS.WebBase.SysFolder.AppFrame.AppOutSelect" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>请选择</title>
    <link rel="stylesheet" type="text/css" href="../../grid/css/flexigrid.css" />
    <link rel="stylesheet" type="text/css" href="../../Css/DefStyle.css" />
    <link rel="stylesheet" type="text/css" href="../../Css/datePicker.css" />
    <script type="text/javascript" src="../../js/jquery-1.7.min.js"></script>
    <script type="text/javascript" src="../../grid/flexigrid.js"></script>
    <script type="text/javascript" src="../../DatePicker/WdatePicker.js"></script> 
    <%=customScript%>
    <style type="text/css">
    .selradio{
        height:20px;
        margin:0px;
    }
    </style>
</head>
<body scroll="no" style="overflow:hidden;margin:1px 1px 1px 2px">
    <form id="form1" runat="server">
    <div id="griddiv" name="griddiv">
        <table id="flex1" style="display:none"></table>    
    </div>
    </form>
</body>
</html>
<script type="text/javascript">
<!--
    var _curClass = EIS.WebBase.SysFolder.AppFrame.AppOutSelect;
    var multi = '<%=base.GetParaValue("multi") %>' != "";
    var retctl = "<%=base.GetParaValue("cid") %>".split(",");
    var qList = "<%=base.GetParaValue("queryfield") %>".split(",");
    var multiRow = [];
    jQuery(function(){
        $("#flex1").flexigrid
        ({
            url: '../getxml.ashx',
            initCond: "<%=InitCond %>",
            params: [{ name: "queryid", value: "<%=tblName%>" }
                    , { name: "cryptcond", value: "" }
                    , { name: "condition", value: "<%=base.GetParaValue("condition") %>" }
                        , { name: "defaultvalue", value: "<%=base.GetParaValue("defaultvalue") %>" }
		        ],
            colModel: [
                { display: '选择', name: 'rowindex', width: 30, sortable: false, align: 'center', renderer: tranSel },
                    <%=colmodel %>
		        ],
            buttons: [
                { name: '查询', bclass: 'view', onpress: app_query },
                { name: '查询定制', bclass: 'view', onpress: app_setquery },
                { name: '清空', bclass: 'clear', onpress: app_reset },
                { name: '保存布局', bclass: 'layout', onpress: app_layout },
                { name: '确认选择', bclass: 'check', onpress: app_confirm }
            ],
            searchitems: [
			        <%=querymodel %>
		        ],
            sortname: "<%=sortname %>",
            sortorder: "<%=sortorder %>",
            usepager: true,
            singleSelect: true,
            useRp: true,
            rp: <%=PageRecCount%>,
		            rpOptions : <%=PageRecOptions%>,
        showTableToggleBtn: false,
        resizable: false,
        height: 'auto',
        onError: showError,
        preProcess: getdata,
        onColResize: fnColResize,
        onRowSelect: fnRowSelect,
        onSuccess: fnLoad
    });
        });
        function fnRowSelect(row, selected) {
            var rowId = row.attr("id").substr(3);

            var dr = $("row[id='" + rowId + "']", xmldata);
            var valArr = [];
            $.each(qList, function (i, v) {
                valArr.push(dr.find(v).text());
            });

            if (multi) {
                $(".selcheck", row).prop("checked", selected);
                if (selected)
                    addChecked(rowId, valArr);
                else
                    delChecked(rowId);
            }
            else {
                $(".selradio", row).prop("checked", selected);
                if (selected) fnRet(rowId);
            }
        }

        function hasChecked(key) {
            for (var i = 0; i < multiRow.length; i++) {
                if (multiRow[i].id == key)
                    return "checked";
            }
            return "";
        }

        function addChecked(key, valArr) {
            for (var i = 0; i < multiRow.length; i++) {
                if (multiRow[i].id == key)
                    return;
            }
            multiRow.push({ "id": key, "data": valArr });
        }

        function delChecked(key, valArr) {
            for (var i = 0; i < multiRow.length; i++) {
                if (multiRow[i].id == key) {
                    multiRow.splice(i, 1);
                    return;
                }
            }
        }

        function showError(data) { }
        var xmldata;
        function getdata(data) {
            return xmldata = jQuery(data);
        }
        function fnColResize(fieldname, width) {

        }

        function tranSel(fldval, row, td) {
            var key = $(row).attr("id");
            if (multi) {
                var chk = hasChecked(key);
                if (chk != "") {
                    $(td).parent().addClass("trSelected");
                }
                return "<input type='checkbox' " + hasChecked(key) + " class='selcheck' name='selcheck' key='" + key + "' onclick='setvalue()' />";
            }
            else {
                return "<input type='radio' " + hasChecked(key) + " class='selradio' name='selradio' key='" + key + "' onclick='retvalue()'/>";
            }
        }
        function fnLoad() {

        }
        //确认返回
        function app_confirm() {
            return;
            if (typeof (window.opener["_afterMultiSelect"]) != "undefined") {
                window.opener["_afterMultiSelect"](retctl, multiRow, "<%=tblName%>");
		        window.close();
                
		    }
		    else {
		        $.each(qList, function (i, v) {
		            var c = window.opener.document.getElementById(retctl[i]);
		            if (c != null) {
		                c.value = valArr[i];
		                try { $(c).change(); } catch (e) { }
		            }
		        });
		    }
            window.close();
        }

        function setvalue() {
            var srcEl = $(event.srcElement);
            var key = srcEl.attr("key");
            var dr = $("row[id='" + key + "']", xmldata);
            var valArr = [];
            $.each(qList, function (i, v) {
                valArr.push(dr.find(v).text());
            });
            var checked = srcEl.prop("checked");
            if (multi) {
                if (checked) {
                    $("#row" + key).addClass("trSelected");
                    addChecked(key, valArr);
                }
                else {
                    $("#row" + key).removeClass("trSelected");
                    delChecked(key);
                }
            }
        }
        function retvalue() {
            var srcEl = $(event.srcElement);
            var key = srcEl.attr("key");
            fnRet(key);
        }
        function fnRet(key) {
            var dr = $("row[id='" + key + "']", xmldata);
            var valArr = [];
            $.each(qList, function (i, v) {
                valArr.push(dr.find(v).text());
            });

            if (typeof (window.opener["_afterOutSelect"]) != "undefined") {
                window.opener["_afterOutSelect"](retctl, valArr, "<%=tblName%>");
            window.close();
        }
        else {
            $.each(qList, function (i, v) {
                var c = window.opener.document.getElementById(retctl[i]);
                if (c != null) {
                    c.value = valArr[i];
                    try {
                        $(c).change();
                    } catch (e) {

                    }
                }
            });
        }
        window.close();
    }
    function app_layout(cmd, grid) {
        //暂时有点儿问题，应该把fieldname 换成fieldid
        var fldlist = [];
        $('th', grid).each(function () {
            var fieldId = $(this).attr("fieldid");
            if (fieldId)
                fldlist.push(fieldId + "=" + ($(this).width() - 10) + "=" + $(this).css("display"));
        });
        var param = $("#flex1")[0].p;
        var sortdir = "";
        if (param.sortname.length > 0) {
            sortdir = (param.sortname + " " + param.sortorder);
        }
        var ret = EIS.Web.SysFolder.AppFrame.AppOutSelect.saveLayout(fldlist, "<%=tblName %>", sortdir);
        if (ret.error) {
            alert("保存出错：" + ret.error.Message);
        }
        else {
            alert("保存成功！");
        }
    }
    function app_reset(cmd, grid) {
        $("#flex1").clearQueryForm();
    }

    function addcallback() {
        $("#flex1").flexReload();
    }
    function app_setquery() {
        var paraStr = "tblName=<%=tblName %>";
            var para = _curClass.CryptPara(paraStr).value;
            openCenter("AppConditionDef.aspx?para=" + para, "_blank", 400, 500);
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
<script type="text/javascript">
    <%=listfn %>
</script>
