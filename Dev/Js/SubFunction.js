var _tmplRowId = "srkjdslABHSAS";
Date.prototype.format = function (fmt) {
    var o = {
        "M+": this.getMonth() + 1, //月份      
        "d+": this.getDate(), //日      
        "h+": this.getHours() % 12 == 0 ? 12 : this.getHours() % 12, //小时      
        "H+": this.getHours(), //小时      
        "m+": this.getMinutes(), //分      
        "s+": this.getSeconds(), //秒      
        "q+": Math.floor((this.getMonth() + 3) / 3), //季度      
        "S": this.getMilliseconds() //毫秒      
    };
    var week = { "0": "/u65e5", "1": "/u4e00", "2": "/u4e8c", "3": "/u4e09", "4": "/u56db", "5": "/u4e94", "6": "/u516d" };
    if (/(y+)/.test(fmt)) {
        fmt = fmt.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
    }
    if (/(E+)/.test(fmt)) {
        fmt = fmt.replace(RegExp.$1, ((RegExp.$1.length > 1) ? (RegExp.$1.length > 2 ? "/u661f/u671f" : "/u5468") : "") + week[this.getDay() + ""]);
    }
    for (var k in o) {
        if (new RegExp("(" + k + ")").test(fmt)) {
            fmt = fmt.replace(RegExp.$1, (RegExp.$1.length == 1) ? (o[k]) : (("00" + o[k]).substr(("" + o[k]).length)));
        }
    }
    return fmt;

}

var _class2type = {};
var _arrType = "Boolean Number String Function Array Date RegExp Object".split(" ");
jQuery.each(_arrType,
    function (i, name) {
        _class2type["[object " + name + "]"] = name.toLowerCase();
    });
var _core_toString = Object.prototype.toString;
jQuery.extend({
    type: function (obj) {
        return obj == null ?
            String(obj) :  // 如果传入的值为null或者是undefined直接调用String()方法 
            _class2type[_core_toString.call(obj)] || "object";
    }
});

var _upKeyCode = 38;
var _downKeyCode = 40;
var _inputBg = {};
//平台初始化 var _sysModel=[{主表1},{主表2}];
$(function () {
    $("dl").KandyTabs({ trigger: "click" });
    jQuery("input.emptytip").emptyValue();
    jQuery("textarea.emptytip").emptyValue();

    jQuery(":text,.TextBoxInArea").live("focus", function () {
        _inputBg = { "backgroundColor": $(this).css("background-color"), "backgroundImage": $(this).css("background-image") };
        $(this).css({ "backgroundColor": "#fff280", "backgroundImage": "url()" });
    });

    jQuery(":text,.TextBoxInArea").live("blur", function () {
        $(this).css(_inputBg);
    });

    //主表字段映射关系
    _genFieldsMap();
    //SQLID|input07,input02|EmployeeName,CompanyName|companyId='{CompanyId}'|EmployeeName,LoginName,CompanyName
    jQuery(".EnterSearch").attr("autocomplete", "off").live("keyup", function () {
        _enterSearch(event.srcElement);
    });
    jQuery(".EnterSearch").live("dblclick", function () {
        _clickSearch(event.srcElement);
    });
    //dLink 字段联动，dLink的格式为：fieldId&fieldOdr|参考表1&字段1|参考表2&字段2
    var _dLinkEvent = {};
    jQuery(".dLink").each(function () {
        var dLink = $(this).attr("dLink");
        if (dLink.length > 0) {
            var arrLink = dLink.split("|");
            var fieldInfo = arrLink[0].split("&");
            for (var i = 1; i < arrLink.length; i++) {
                var fieldTbl = arrLink[i];
                var arr = fieldTbl.split("&");
                var eventFlag = fieldInfo[0] + "_" + arr[1];
                if (_dLinkEvent[eventFlag] != undefined)
                    continue;
                _dLinkEvent[eventFlag] = "1";

                $("." + arr[0] + "_" + arr[1]).live("change", { "arrLink": arrLink }, function (event) {
                    var pArr = [];
                    var rField = event.data.arrLink;
                    var fieldInfo = rField[0].split("&");
                    var fieldId = fieldInfo[0];
                    for (var n = 1; n < rField.length; n++) {
                        var ff = rField[n].split("&");
                        if (this.id.indexOf("input") > -1) {
                            pArr.push("@" + ff[1] + "=" + $("." + ff[0] + "_" + ff[1]).val());
                        }
                        else {
                            var subOdr = _getSubOdr(ff[0]);
                            var fieldOdr = _getSubFieldOdr(subOdr, ff[1]);
                            var idSeg = this.id.split("_");
                            var refId = idSeg[0] + "_" + idSeg[1] + "_" + fieldOdr;
                            pArr.push("@" + ff[1] + "=" + $("#" + refId).val());

                        }
                    }
                    var ret = _curClass.GetLinkData(fieldId, pArr.join("|"));
                    if (!ret.error) {
                        var dt = ret.value;
                        //开始装载下拉框
                        if (dt != null) {
                            var codeFld = dt.Columns[0].Name;
                            var dispFld = dt.Columns[1].Name;
                            //清空options
                            var targetCtl = null;
                            if (this.id.indexOf("input") > -1) {
                                targetCtl = document.getElementById("input0" + fieldInfo[1]);
                            }
                            else {
                                var idSeg = this.id.split("_");
                                var refId = idSeg[0] + "_" + idSeg[1] + "_" + fieldInfo[1];
                                targetCtl = document.getElementById(refId);

                            }
                            targetCtl.options.length = 1;
                            for (var j = 0; j < dt.Rows.length; j++) {
                                targetCtl.options.add(new Option(dt.Rows[j][dispFld], dt.Rows[j][codeFld]));
                            }
                        }
                    }
                });
            }
        }
    });

    //AutoSn 自动编号，AutoSn的格式为：fieldId&fieldOdr|参考表1&字段1|参考表2&字段2
    var _AutoSnEvent = {};
    jQuery(".autosn").each(function () {
        var dLink = $(this).attr("autosn");
        if (dLink == undefined)
            return;
        if (dLink.length > 0) {
            var arrLink = dLink.split("|");
            var fieldInfo = arrLink[0].split("&");
            for (var i = 1; i < arrLink.length; i++) {
                var fieldTbl = arrLink[i];
                var arr = fieldTbl.split("&");
                var eventFlag = fieldInfo[0] + "_" + arr[1];
                if (_AutoSnEvent[eventFlag] != undefined)
                    continue;
                _AutoSnEvent[eventFlag] = "1";

                $("." + arr[0] + "_" + arr[1]).live("change", { "arrLink": arrLink }, function (event) {
                    var pArr = [];
                    var rField = event.data.arrLink;
                    var fieldInfo = rField[0].split("&");
                    var fieldId = fieldInfo[0];
                    for (var n = 1; n < rField.length; n++) {
                        var ff = rField[n].split("&");
                        if (this.id.indexOf("input") > -1) {
                            pArr.push("@" + ff[1] + "=" + $("." + ff[0] + "_" + ff[1]).val());
                        }
                        else {
                            var subOdr = _getSubOdr(ff[0]);
                            var fieldOdr = _getSubFieldOdr(subOdr, ff[1]);
                            var idSeg = this.id.split("_");
                            var refId = idSeg[0] + "_" + idSeg[1] + "_" + fieldOdr;
                            pArr.push("@" + ff[1] + "=" + $("#" + refId).val());

                        }
                    }
                    var ret = _curClass.GetAutoSn(fieldId, pArr.join("|"));
                    if (!ret.error) {
                        //开始装载下拉框
                        if (ret.value != null) {
                            var targetCtl = null;
                            if (this.id.indexOf("input") > -1) {
                                targetCtl = document.getElementById("input0" + fieldInfo[1]);
                            }
                            else {
                                var idSeg = this.id.split("_");
                                var refId = idSeg[0] + "_" + idSeg[1] + "_" + fieldInfo[1];
                                targetCtl = document.getElementById(refId);
                            }

                            targetCtl.value = ret.value;

                        }
                    }
                });
            }
        }
    });

    KindEditor.create(".WebEditor", { uploadJson: '../../UploadImage.axd', filterMode: false, urlType: 'relative' });

    $(".subtbl input[type!=button]").live("keydown", function () {
        var ctlName = $(this).attr("name");
        var arrName = ctlName.split("_");
        if (arrName.length < 2) return;

        var subOdr = arrName[0].substr(7);
        var rowIndex = parseInt(arrName[1]);
        if (event.keyCode == _upKeyCode) {
            arrName[1] = rowIndex - 1;
            var targetName = arrName.join("_");
            var ctl = $("#" + targetName);
            if (ctl.length == 0) {
                while (rowIndex > 0) {
                    rowIndex = rowIndex - 1;
                    arrName[1] = rowIndex;
                    targetName = arrName.join("_");
                    var ctl = $("#" + targetName);
                    if (ctl.length > 0) {
                        ctl.focus().select();
                        break;
                    }
                }
            }
            else {
                ctl.focus().select();
            }
        }
        else if (event.keyCode == _downKeyCode) {
            arrName[1] = rowIndex + 1;
            var targetName = arrName.join("_");
            var ctl = $("#" + targetName);
            var maxRow = _sysModel[0].subtbls[subOdr].maxorder;
            //如果是最后一行，增加新行
            if (rowIndex == maxRow - 1) {
                _fnSubAdd(_sysModel[0].subtbls[subOdr].tablename);
            }
            if (ctl.length == 0) {
                while (rowIndex < maxRow) {
                    rowIndex = rowIndex + 1;
                    arrName[1] = rowIndex;
                    targetName = arrName.join("_");
                    var ctl = $("#" + targetName);
                    if (ctl.length > 0) {
                        ctl.focus().select();
                        break;
                    }
                }
            }
            else {
                ctl.focus().select();
            }
        }

    });

    if (!_xmlData) {
        alert("请检查浏览器设置，关闭【ActiveX筛选】功能，重新打开本页面。");
        return false;
    }
});

//032弹出窗口，键盘事件
function _enterSearch(ctl) {
    var arr = $(ctl).attr("display").split("|");
    var arrCId = arr[1].split(",");
    var arrQuery = arr[2].split(",");
    var scope = "";
    var v = $(ctl).val();
    if (arr[3].length > 0) {
        if (ctl.id.indexOf("SubTbl") == 0) {
            var segs = ctl.id.split("_");
            var subOdr = segs[0].substring(7);
            scope = _replacePara(arr[3], subOdr, segs[1]);
        }
        else {
            scope = _replacePara(arr[3]);
        }
    }
    if (v.length == 0 && event.keyCode == 13) {
        var cond = [];
        cond.push("queryid=" + arr[0]);
        cond.push("queryfield=" + arr[2]);
        cond.push("cid=" + arr[1]);
        cond.push("condition=" + escape(scope));
        _openPage("../AppFrame/AppOutSelect.aspx?" + cond.join("&"));
    }
    else if (event.keyCode == 13) {
        scope = scope.replace(/\[QUOTES\]/ig, "'");
        var dt = _curClass.GetQuery(arr[0], v, scope).value;
        if (dt.Rows.length == 1) {
            for (var i = 0; i < arrCId.length; i++) {
                var fvObject = dt.Rows[0][arrQuery[i]];
                var fv = fvObject.toString();
                if (jQuery.type(fvObject) == "date")
                    fv = fvObject.format("yyyy-MM-dd");
                document.getElementById(arrCId[i]).value = fv;
                $("#" + arrCId[i]).change();
            }
        }
        else {
            $(ctl).val("");
            var cond = [];
            cond.push("queryid=" + arr[0]);
            cond.push("queryfield=" + arr[2]);
            cond.push("cid=" + arr[1]);
            var initArr = [];
            var arrCond = arr[4].split(",");
            for (var i = 0; i < arrCond.length; i++) {
                initArr.push(arrCond[i] + " like [QUOTES]%" + v + "%[QUOTES]");
            }
            if (initArr.length > 0) {
                cond.push("initCond=" + escape(initArr.join(" or ")));
            }
            cond.push("condition=" + escape(scope));
            _openPage("../AppFrame/AppOutSelect.aspx?" + cond.join("&"));
        }
    }
    else {
        $(ctl).data("valid", 0);
    }
}

//032弹出窗口，点击事件
function _clickSearch(ctl) {
    var arr = $(ctl).attr("display").split("|");
    var arrCId = arr[1].split(",");
    var arrQuery = arr[2].split(",");
    var scope = "";
    var v = $(ctl).val();
    if (arr[3].length > 0) {
        if (ctl.id.indexOf("SubTbl") == 0) {
            var segs = ctl.id.split("_");
            var subOdr = segs[0].substring(7);
            scope = _replacePara(arr[3], subOdr, segs[1]);
        }
        else {
            scope = _replacePara(arr[3]);
        }
    }
    var cond = [];
    cond.push("queryid=" + arr[0]);
    cond.push("queryfield=" + arr[2]);
    cond.push("cid=" + arr[1]);
    cond.push("condition=" + escape(scope));
    _openPage("../AppFrame/AppOutSelect.aspx?" + cond.join("&"));
}

//032弹出窗口，点击事件回调函数
function _afterOutSelect(arrId, arrVal) {
    for (var i = 0; i < arrId.length; i++) {
        var ctlName = arrId[i];
        var ctlArr = $("input[name='" + ctlName + "']");
        if (ctlArr.length > 1) {
            var vals = arrVal[i].split(",");
            ctlArr.each(function (i, ctl) {
                if ($.inArray(ctl.value, vals) > -1)
                    $(ctl).attr("checked", "checked");
            });
        }
        else {
            jQuery("#" + ctlName).val(arrVal[i]).change();
        }
    }
}

//参数替换
function _replacePara(source,subOdr,rowNum) {
    var reg = /{([\w\._1-9]+)}/gi;
    var matches = source.match(reg);
    if (matches != null) {
        for (var i = 0; i < matches.length; i++) {
            var fldName = matches[i].substr(1, matches[i].length - 2);
            if (arguments.length == 3) {
                if (fldName.substring(0, 1) == ".") {
                    var subfld = fldName.substring(1);
                    var ix = _getSubFieldOdr(subOdr, subfld);
                    if (ix > -1) {
                        var subval = $("#SubTbl0" + subOdr + "_" + rowNum + "_" + ix).val();
                        source = source.replace(matches[i], subval);
                    }
                }
                else {
                    source = source.replace(matches[i], _sys.getValue(fldName));
                }
            }
            else {
                if (fldName.substr(0, 1) == ".") fldName = fldName.substr(1);
                source = source.replace(matches[i], _sys.getValue(fldName));
            }
        }
    }
    return source;
}

//主表字段映射关系
function _genFieldsMap() {
    if (!!_sysModel.fieldsMap) {
        return;
    }
    var fieldArr = _sysModel.fields;
    _sysModel.fieldsMap = {};
    for (var i = 0; i < fieldArr.length; i++) {
        _sysModel.fieldsMap[fieldArr[i].name] = fieldArr[i];
    }
}

//查询主表字段序号
function _getFieldOdr(fldName) {
    _genFieldsMap();
    if (_sysModel.fieldsMap[fldName]) {
        return _sysModel.fieldsMap[fldName].order;
    }
    else {
        return -1;
    }
}

//返回字段定义
function _getField(fldName) {
    if (_sysModel[0].fieldsMap[fldName]) {
        return _sysModel[0].fieldsMap[fldName];
    }
    else {
        alert("请求的字段定义不存在");
        return null;
    }
}

//返回控件对应的字段名称
function _getFieldName(ctl) {
    if (ctl.name.substr(0, 5) == "input")//主表
    {
        var tblIndex = ctl.name.substr(5, 1);
        var fieldArr = _sysModel[tblIndex].fields;
        for (var i = 0; i < fieldArr.length; i++) {
            if (fieldArr[i].order == ctl.name.substr(6))
                return fieldArr[i].name;
        }
        return "";
    }
    else {

    }
}

//返回主表指定字段名的控件值
function _getCtlByFieldName(fldName) {

    var fldOrder = _getFieldOdr(fldName);
    if (fldOrder == -1)
        return null;
    else {
        return jQuery("#input0" + fldOrder);

    }

}

//返回指定字段名的控件值，如果找到不控件不存在
function _getCtlValueByFieldName(fldName, replace, rowId) {
    replace = false;
    if (arguments.length < 3) {
        var fldOrder = _getFieldOdr(fldName);
        if (fldOrder == -1)
            return _tmplRowId;
        else {
            var ctlName = "input0" + fldOrder;
            var ctlArr = $("input[name='" + ctlName + "']");
            if (ctlArr.length > 1) {
                return _getGroupValueByName(ctlName).join(",");
            }
            else {
                if (document.getElementById(ctlName))
                    return jQuery("#" + ctlName).val();
            }

        }
    }
    else {
        var subOdr = rowId.substr(7, 1);
        var fldOrder = _getSubFieldOdr(subOdr, fldName);
        if (fldOrder == -1)
            return _tmplRowId;
        else {
            var ctlName = rowId + fldOrder;
            var ctlArr = $("input[name='" + ctlName + "']");
            if (ctlArr.length > 1) {
                return _getGroupValueByName(ctlName).join(",");
            }
            else {
                if (document.getElementById(ctlName)) {
                    return jQuery("#" + ctlName).val();
                }
            }
        }
    }

    return _tmplRowId;
}

//返回多附件字段上传的文件名
function _getFileNames(fldName) {
    var fldOdr = _getFieldOdr(fldName);
    try {
        return window.frames["frm_" + fldOdr].getFileNames();
    } catch (e) { }

    return null;
}

//设置主表的字段值
function _setFieldValue(fldName, val, repalce, rowId) {
    repalce = false;
    if (arguments.length < 4) {
        var fldOrder = _getFieldOdr(fldName);
        if (fldOrder == -1)
            return;
        else {
            var ctlName = "input0" + fldOrder;
            var ctlArr = $("input[name='" + ctlName + "']");
            if (ctlArr.length > 1) {
                var vals = val.split(",");
                ctlArr.each(function (i, ctl) {
                    if ($.inArray(ctl.value, vals) > -1)
                        $(ctl).attr("checked", "checked");
                    else
                        $(ctl).removeAttr("checked");
                });
            }
            else {
                jQuery("#" + ctlName).val(val);
            }
        }
    }
    else {
        var subOdr = rowId.substr(7, 1);
        var fldOrder = _getSubFieldOdr(subOdr, fldName);

        var ctlName = rowId + fldOrder;
        var ctlArr = $("input[name='" + ctlName + "']");
        if (ctlArr.length > 1) {
            var vals = val.split(",");
            ctlArr.each(function (i, ctl) {
                if ($.inArray(ctl.value, vals) > -1)
                    $(ctl).attr("checked", "checked");
                else
                    $(ctl).removeAttr("checked");
            });
        }
        else {
            jQuery("#" + ctlName).val(val);
        }
    }
}

//设置控件的值
function _setCtlValue(ctlName, val) {
    var ctlArr = $("input[name='" + ctlName + "']");
    if (ctlArr.length > 1) {
        var vals = val.split(",");
        ctlArr.each(function (i, ctl) {
            if ($.inArray(ctl.value, vals)>-1)
                $(ctl).attr("checked", "checked");
        });
    }
    else {
        jQuery("#" + ctlName).val(val);
    }
}

//平台对象
var _sys = { "getValue": _getCtlValueByFieldName, "getField": _getField, "setValue": _setFieldValue, "setCtlValue": _setCtlValue };

//返回Radio或者CheckBox的值（数组）
function _getGroupValueByName(ctlName) {
    var ctlArr = document.getElementsByName(ctlName);
    var ctlval = [];
    for (var i = 0; i < ctlArr.length; i++) {
        if (ctlArr[i].checked)
            ctlval.push(ctlArr[i].value);
    }
    return ctlval;
}

//保存数据
function _sysSave() {

    //保存HTML编辑器
    KindEditor.sync('.WebEditor');

    var checkFlag = true;
    if (typeof (_sysBeforeSave) == "function") {
        //自定义验证，通过返回true
        checkFlag = _sysBeforeSave();
    }
    if (!checkFlag)
        return false;

    //验证同步数据
    if (!_formSynchData())
        return false;

    if (checkFlag) {
        var ret = _curClass.saveData(_tblName, _mainId, _serializeToString(_xmlData));
        if (ret.error) {
            alert(ret.error.Message);
            return false;
        }
        else {
            //保存成功标志
            var saveFlag = true;
            if (typeof (_sysAfterAdd) == "function" && _isNew) {
                //新增保存通过后                        
                saveFlag = _sysAfterAdd();
            }
            else if (typeof (_sysAfterEdit) == "function" && !_isNew) {
                //修改保存通过后
                saveFlag = _sysAfterEdit();
            }

            return saveFlag;

        }
    }
    else {
        return false;
    }
}

//仅验证表单，外部使用
function _formValidate() {

    for (var mainOrd = 0; mainOrd < _sysModel.length; mainOrd++) {
        var curtbl = _sysModel[mainOrd];
        //第一步验证数据
        var fieldsCount = curtbl.fields.length;
        for (var i = 0; i < fieldsCount; i++) {
            var el = curtbl.fields[i];
            if (_validateCtl(el) == false)
                return false;
        }
        for (var j = 0; j < curtbl.subtbls.length; j++) {
            //行数
            var subTbl = curtbl.subtbls[j];
            var rowCount = subTbl.maxorder;
            var fldCount = subTbl.fields.length;
            for (var n = 0; n < rowCount; n++) {
                var rowId = "SubTbl" + mainOrd + j + "_" + n + "_";
                if (document.getElementById(rowId) == null) continue;
                for (var i = 0; i < fldCount; i++) {
                    var el = subTbl.fields[i];
                    if (_validateCtl(el, rowId) == false)
                        return false;

                }
            }
        }
    }
    return true;
}

//验证表单，同步表单数据
function _formSynchData() {
    var mainOrd = "0";
    var curtbl = _sysModel;
    var rowNode = jQuery("row:first", _xmlData);
    for (var i = 0; i < curtbl.fields.length; i++) {
        var el = curtbl.fields[i];
        var ctlName = "input" + mainOrd + el.order;
        var ctl = $("#" + ctlName);

        if (_validateCtl(el) == false)
            return false;

        if (ctl.length == 1)//如果控件存在
        {
            _synchData(rowNode, el, ctl.val());
        }
        else {
            //针对Radio和CheckBox的情况做处理，只有控件存在的情况下才更新
            var ctlArr = document.getElementsByName(ctlName);
            if (ctlArr.length > 0) {
                if (el.dispstyle.substr(0, 2) == "04") {
                    var ctlval = _getGroupValueByName(ctlName);
                    _synchData(rowNode, el, ctlval.join(","));
                }
                else if (el.dispstyle.substr(0, 2) == "05") {
                    var ctlval = _getGroupValueByName(ctlName);
                    _synchData(rowNode, el, ctlval.join(","));

                }
            }
        }

    }

    return true;
}

//序列化
function _serializeToString(objXML) {
    if (window.XMLSerializer) {
        return (new XMLSerializer()).serializeToString(objXML.find("root")[0])
    }
    else {
        return objXML.find("root")[0].xml;
    }
}

//同步数据到xml
function _synchData(rowNode, el, val) {
    //修改值
    var xmlEl = rowNode.find(el.name);
    if (xmlEl.text() != val) {
        xmlEl.text(val);
        if (rowNode.attr("state") == "Unchanged")
            rowNode.attr("state", "Modified");
    }
}

//验证控件
function _validateCtl(el, rowId) {
    var ctl = null;
    //主表字段
    if (arguments.length == 1) {
        var ctlName = "input0" + el.order;
        ctl = $("#" + ctlName);
        if (ctl.length == 0) {
            var ctlArr = document.getElementsByName(ctlName);
            //针对Radio和CheckBox的情况做处理，只判断 必填
            var valArr = _getGroupValueByName(ctlName);
            if (ctlArr.length > 0) {
                if (valArr.length == 0 && el.empty == "1" && _saveAction == "1") {
                    alert(el.namecn + "不能为空");
                    return false;
                }
                else {
                    return true;
                }
            }
            else {
                return true;
            }
        }
        else {
            if (el.empty == "1" && _saveAction == "1") {
                //针对附件校验
                if (el.dispstyle == "023") {
                    try {
                        var has = window.frames["frm_" + el.order].hasFiles();
                        if (!has) {
                            alert("请上传附件〔" + el.namecn + "〕");
                            return false;
                        }

                    } catch (e) {}
                }
            }
        }

    }
    else {
        //子表的情况1
        var ctlName = rowId + el.order;
        ctl = $("#" + ctlName);
        if (ctl.length == 0) {
            var ctlArr = document.getElementsByName(ctlName);
            //针对Radio和CheckBox的情况做处理，只判断 必填
            var valArr = _getGroupValueByName(ctlName);
            if (ctlArr.length > 0) {
                if (valArr.length == 0 && el.empty == "1" && _saveAction == "1") {
                    alert(el.namecn + "不能为空");
                    return false;
                }
                else {
                    return true;
                }
            }
            else {
                return true;
            }
        }
    }

    var val = ctl.val(); if (val == null) val = "";
    var maxlen = el.length.split(",")[0];
    if (el.empty == "1" && val.length == 0 && _saveAction == "1")//不能为空
    {
        alert(el.namecn + "不能为空");
        try {
            if (el.type != '4') ctl.focus();
            ctl.select();
        } catch (e) { }
        return false;
    }
    if (el.empty == "0" && val.length == 0)//可以为空，且内容为空
    {
        return true;
    }
    if (val.length > maxlen && el.type == 1) {
        alert(el.namecn + "的长度不能大于" + maxlen);
        try { ctl.focus(); ctl.select(); } catch (e) { }
        return false;
    }

    var regfld = [/^[-]?([\d,])*$/, /^[-]?\d+([\d,])*\.?\d*$/, /^\d{4}-\d{1,2}-\d{1,2}$/];
    switch (el.type) {
        case '2':
            if (!regfld[0].test(val))//不能为空
            {
                alert(el.namecn + "必须输入整数");
                ctl.focus();
                ctl.select();
                return false;
            }
            break;
        case '3':
            if (!regfld[1].test(val))//不能为空
            {
                alert(el.namecn + "输入不符合要求");
                ctl.focus();
                ctl.select();
                return false;
            }
            var dotlen = el.length.split(",")[1];
            var m = parseFloat("9999999999999999999999".substring(0, maxlen - dotlen) + "." + "999999".substring(0, dotlen));
            var v = parseFloat(val.replace(/[,]/g, ""));
            if (v > m) {
                alert(el.namecn + "的值不能大于" + m);
                ctl.focus();
                ctl.select();
                return false;
            }

            break;
        case '4':
            /*
            if(!regfld[2].test(val))//不能为空
            {
            alert(el.namecn+"必须输入合法的日期格式");
            ctl.focus();
            ctl.select();
            return false;
            }*/
            break;
        default:
            return true;
    }
    return true;
}

//生成数据行
function _createXml(subOdr, rowId) {
    var retxml = _xmlData[0].createElement("row");
    var attID = _xmlData[0].createAttribute("id");
    attID.nodeValue = rowId;

    var attst = _xmlData[0].createAttribute("state");
    attst.nodeValue = "Detached";

    var flds = _sysModel[0].subtbls[subOdr].fields;
    for (var i = 0; i < flds.length; i++) {
        var fldnode = _xmlData[0].createElement(flds[i].name);
        fldnode.appendChild(_xmlData[0].createCDATASection(""));
        retxml.appendChild(fldnode);

    }
    retxml.attributes.setNamedItem(attID);
    retxml.attributes.setNamedItem(attst);
    return retxml;

}

//返回子表的序号
function _getSubOdr(subName) {
    return _rowId.substr(7,1);
    alert("子表[" + subName + "]不存在，请检查确认");
    return -1;
}

//设置子表自动序号列
function _setSubAutoSn(subOdr) {
    var snFieldOdr = _sysModel[0].subtbls[subOdr].snfield;
    if (typeof (snFieldOdr) == "undefined") {
        _sysModel[0].subtbls[subOdr].snfield = "";
        snFieldOdr = "";
        for (var i = 0; i < _sysModel[0].subtbls[subOdr].fields.length; i++) {
            var field = _sysModel[0].subtbls[subOdr].fields[i];
            if (field.dispstyle == "003") {
                _sysModel[0].subtbls[subOdr].snfield = field.order;
                snFieldOdr = field.order;
                break;
            }
        }
    }
    if (snFieldOdr) {
        var maxOdr = _sysModel[0].subtbls[subOdr].maxorder;
        var sn = 1;
        for (var i = 0; i <= maxOdr; i++) {
            var rowId = "SubTbl0" + subOdr + "_" + i + "_" + snFieldOdr;
            if ($("#" + rowId).length > 0) {
                $("#" + rowId).val(sn++);
            }
        }
    }
}

//增加子表数据行
function _fnSubAdd(subName) {

    //处理界面
    var subOdr = _getSubOdr(subName);
    if (subOdr == -1)
        return;
    var newOdr = _sysModel[0].subtbls[subOdr].maxorder++;
    if (typeof (window["_" + subName + "_BeforeAdd"]) == "function") {
        var canAdd = window["_" + subName + "_BeforeAdd"](newOdr);
        if (!canAdd)
            return -1;
    }

    var newhtml = $("#SubTbl0" + subOdr + "_srkjdslABHSAS_").parent().html().replace(/_srkjdslABHSAS_/g, "_" + newOdr + "_");
    var arrField = _sysModel[0].subtbls[subOdr].fields;
    var rowData = {};
    for (var i = 0; i < arrField.length; i++) {
        if (arrField[i].dispstyle == "023") {
            var fldodr = arrField[i].order;
            var newId = _newGuid();
            rowData[arrField[i].name] = newId;
            newhtml = newhtml.replace("{" + arrField[i].name + "}", newId);
        }
    }
    
    $("#SubTbl0" + subOdr).append(newhtml);
    //处理XML数据
    var prefix = "SubTbl0" + subOdr + "_" + newOdr + "_";
    var newNode = _createXml(subOdr, prefix);
    $("Table[TableName='" + subName + "']:first", _xmlData).append(newNode);
    if (typeof (window["_" + subName + "_AfterAdd"]) == "function") {
        window["_" + subName + "_AfterAdd"](newOdr);
    }
    if (window[subName + "_RowChange"])
        window[subName + "_RowChange"].trigger("rowchange");
    _setSubAutoSn(subOdr);
    //子表附件设置
    for (var i = 0; i < arrField.length; i++) {
        if (arrField[i].dispstyle == "023") {
            var fldodr = arrField[i].order;
            var obj = $("#" + prefix + fldodr);
            if (obj.length == 1) {
                var newId = rowData[arrField[i].name];
                obj.val(newId);       
            }
        }
    }
    jQuery("input.emptytip", "#" + prefix).emptyValue();
    return newOdr;
}

//删除子表数据行，无提示
function _fnSubDel(subName, rowId) {
    var subOdr = _getSubOdr(subName);
    if (subOdr == -1)
        return;
    if ($("#" + rowId).length == 0)
        return;

    if (typeof (window["_" + subName + "_BeforeDel"]) == "function") {
        var flag = window["_" + subName + "_BeforeDel"](rowId);
        if (!flag) {
            return false;
        }
    }
    $("#" + rowId).remove();
    if ($("#" + rowId, _xmlData).attr("state") == "Detached")//如果是新记录，
    {
        $("Table[TableName='" + subName + "']:first", _xmlData)[0].removeChild($("#" + rowId, _xmlData)[0]);
    }
    else {
        $("#" + rowId, _xmlData).attr("state", "Deleted");
    }
    if (typeof (window["_" + subName + "_AfterDel"]) == "function") {
        window["_" + subName + "_AfterDel"](rowId);
    }
    if (window[subName + "_RowChange"])
        window[subName + "_RowChange"].trigger("rowchange");
    _setSubAutoSn(subOdr);
    return true;
}

//删除子表数据行，带提示
function _fnSubDelConfirm(subName, rowId) {
    if (!confirm('确定删除这条记录吗?'))
        return;
    _fnSubDel(subName, rowId);
}


//弹出窗口
function _openCenter(url, name, width, height) {
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

//系统弹出
function _openPage(url) {
    _openCenter(url, "_blank", 640, 500);
}

//生成GUID
function _newGuid() {
    var guid = "";
    for (var i = 1; i <= 32; i++) {
        var n = Math.floor(Math.random() * 16.0).toString(16);
        guid += n;
        if ((i == 8) || (i == 12) || (i == 16) || (i == 20))
            guid += "-";
    }
    return guid;
}

//传入控件ID得到数值
function _getNumById(ctlId) {
    if (document.getElementById(ctlId) && document.getElementById(ctlId).value) {
        var v = document.getElementById(ctlId).value.replace(/,/g, "");
        return parseFloat(isNaN(v) ? 0 : v);
    }
    else
        return 0;
}

//合计控件值
function _sumById() {
    var len = arguments.length;
    var s = 0;
    for (var i = 0; i < len; i++) {
        s += _getNumById(arguments[i])
    }
    return s;
}

//多个数相乘
function _multiById() {
    var len = arguments.length;
    var s = _getNumById(arguments[0]);
    for (var i = 1; i < len; i++) {
        s = s * _getNumById(arguments[i]);
    }
    return s;

}

//返回子表字段序号
function _getSubFieldOdr(subOdr, fieldName) {

    for (var i = 0; i < _sysModel.fields.length; i++) {
        var el = _sysModel.fields[i];
        if (el.name == fieldName)
            return el.order;
    }

    return -1;
}

//合计子表的字段
function _sumSubField(subName, fieldName, fnFilter) {

    var s = 0;
    var subOdr = _getSubOdr(subName);
    var maxOdr = _sysModel[0].subtbls[subOdr].maxorder;
    if (arguments.length == 3) {
        var fieldOdr = _getSubFieldOdr(subOdr, fieldName);
        if (jQuery.type(fnFilter) == "function") {
            for (var i = 0; i <= maxOdr; i++) {
                if (fnFilter("SubTbl0" + subOdr + "_" + i + "_")) {
                    s += _getNumById("SubTbl0" + subOdr + "_" + i + "_" + fieldOdr);
                }
            }
        }
        else {
            for (var i = 0; i <= maxOdr; i++) {
                s += _getNumById("SubTbl0" + subOdr + "_" + i + "_" + fieldOdr);
            }
        }
    }
    else if (arguments.length == 2) {
        var reg = /{([\w_1-9]+)}/gi;
        var matches = fieldName.match(reg);

        for (var i = 0; i <= maxOdr; i++) {
            var exp = fieldName;
            if (matches != null) {
                for (var j = 0; j < matches.length; j++) {
                    var fldName = matches[j].substr(1, matches[j].length - 2);
                    var fieldOdr = _getSubFieldOdr(subOdr, fldName);

                    var v = _getNumById("SubTbl0" + subOdr + "_" + i + "_" + fieldOdr)
                    var r = new RegExp(matches[j]);
                    exp = exp.replace(matches[j], v);
                }
                s += eval(exp);
            }
        }
    }
    return _fixNum(s);
}

//如果结果为正数就返回整数，否则返回包含2位小数的float
function _fixNum(fval) {
    var str = fval.toString();
    if (parseInt(str) == fval)
        return fval;
    else
        return fval.toFixed(2);
}

//相乘并合计子表字段
function _multiSubField(subName, fieldName1, fieldName2) {

    var subOdr = _getSubOdr(subName);
    var maxOdr = _sysModel[0].subtbls[subOdr].maxorder;
    var fieldOdr1 = _getSubFieldOdr(subOdr, fieldName1);
    var fieldOdr2 = _getSubFieldOdr(subOdr, fieldName2);
    var s = 0;
    for (var i = 0; i <= maxOdr; i++) {
        var prefix = "SubTbl0" + subOdr + "_" + i + "_";
        s += _multiById(prefix + fieldOdr1, prefix + fieldOdr2);
    }
    return _fixNum(s);
}

//计算表表某行的表达式
function _computeExp(exp) {
    var reg = /{([\w_1-9]+)}/gi;
    var matches = exp.match(reg);
    if (matches != null) {
        for (var j = 0; j < matches.length; j++) {
            var fldName = matches[j].substr(1, matches[j].length - 2);
            var fldOrder = _getFieldOdr(fldName);
            var ctlName = "input0" + fldOrder;
            var v = _getNumById(ctlName);
            var r = new RegExp(matches[j]);
            exp = exp.replace(matches[j], v);
        }
        return _fixNum(eval(exp));
    }
    return 0;
}

//计算子表某行的表达式
function _computeSubExp(subName, subField, rowOdr, exp) {
    var subOdr = _getSubOdr(subName);
    var tOdr = _getSubFieldOdr(subOdr, subField);

    var reg = /{([\w_1-9]+)}/gi;
    var matches = exp.match(reg);
    if (matches != null) {
        for (var j = 0; j < matches.length; j++) {
            var fldName = matches[j].substr(1, matches[j].length - 2);
            var fieldOdr = _getSubFieldOdr(subOdr, fldName);

            var v = _getNumById("SubTbl0" + subOdr + "_" + rowOdr + "_" + fieldOdr);
            var r = new RegExp(matches[j]);
            exp = exp.replace(matches[j], v);
        }
        jQuery("#SubTbl0" + subOdr + "_" + rowOdr + "_" + tOdr).val(_fixNum(eval(exp))).change();
    }
}

//金额小写转大写
function _a2c(Num) {
    Num = Num + ""
    Num = Num.replace(/,/g, "").replace(/ /g, "").replace("￥", "")
    if (isNaN(Num)) {
        alert("请检查小写金额是否正确");
        return;
    }
    part = String(Num).split(".");
    newchar = "";
    for (i = part[0].length - 1; i >= 0; i--) {
        if (part[0].length > 10) { alert("位数过大，无法计算"); return ""; }
        tmpnewchar = ""
        perchar = part[0].charAt(i);
        switch (perchar) {
            case "0": tmpnewchar = "零" + tmpnewchar; break;
            case "1": tmpnewchar = "壹" + tmpnewchar; break;
            case "2": tmpnewchar = "贰" + tmpnewchar; break;
            case "3": tmpnewchar = "叁" + tmpnewchar; break;
            case "4": tmpnewchar = "肆" + tmpnewchar; break;
            case "5": tmpnewchar = "伍" + tmpnewchar; break;
            case "6": tmpnewchar = "陆" + tmpnewchar; break;
            case "7": tmpnewchar = "柒" + tmpnewchar; break;
            case "8": tmpnewchar = "捌" + tmpnewchar; break;
            case "9": tmpnewchar = "玖" + tmpnewchar; break;
        }
        switch (part[0].length - i - 1) {
            case 0: tmpnewchar = tmpnewchar + "元"; break;
            case 1: if (perchar != 0) tmpnewchar = tmpnewchar + "拾"; break;
            case 2: if (perchar != 0) tmpnewchar = tmpnewchar + "佰"; break;
            case 3: if (perchar != 0) tmpnewchar = tmpnewchar + "仟"; break;
            case 4: tmpnewchar = tmpnewchar + "万"; break;
            case 5: if (perchar != 0) tmpnewchar = tmpnewchar + "拾"; break;
            case 6: if (perchar != 0) tmpnewchar = tmpnewchar + "佰"; break;
            case 7: if (perchar != 0) tmpnewchar = tmpnewchar + "仟"; break;
            case 8: tmpnewchar = tmpnewchar + "亿"; break;
            case 9: tmpnewchar = tmpnewchar + "拾"; break;
        }
        newchar = tmpnewchar + newchar;
    }
    if (Num.indexOf(".") != -1) {
        if (part[1].length > 2) {
            part[1] = part[1].substr(0, 2)
        }
        for (i = 0; i < part[1].length; i++) {
            tmpnewchar = ""
            perchar = part[1].charAt(i)
            switch (perchar) {
                case "0": tmpnewchar = "零" + tmpnewchar; break;
                case "1": tmpnewchar = "壹" + tmpnewchar; break;
                case "2": tmpnewchar = "贰" + tmpnewchar; break;
                case "3": tmpnewchar = "叁" + tmpnewchar; break;
                case "4": tmpnewchar = "肆" + tmpnewchar; break;
                case "5": tmpnewchar = "伍" + tmpnewchar; break;
                case "6": tmpnewchar = "陆" + tmpnewchar; break;
                case "7": tmpnewchar = "柒" + tmpnewchar; break;
                case "8": tmpnewchar = "捌" + tmpnewchar; break;
                case "9": tmpnewchar = "玖" + tmpnewchar; break;
            }
            if (i == 0) tmpnewchar = tmpnewchar + "角";
            if (i == 1) tmpnewchar = tmpnewchar + "分";
            newchar = newchar + tmpnewchar;
        }
    }
    while (newchar.search("零零") != -1)
        newchar = newchar.replace("零零", "零");
    newchar = newchar.replace("零亿", "亿");
    newchar = newchar.replace("亿万", "亿");
    newchar = newchar.replace("零万", "万");
    newchar = newchar.replace("零元", "元");
    newchar = newchar.replace("零角", "");
    newchar = newchar.replace("零分", "");
    if (newchar.charAt(newchar.length - 1) == "元" || newchar.charAt(newchar.length - 1) == "角")
        newchar = newchar + "整"
    return newchar;
}

/*
* _formatMoney(s,type)
* 功能：金额按千位逗号分割
* 参数：s，需要格式化的金额数值.
* 参数：type,判断格式化后的金额是否需要小数位.
* 返回：返回格式化后的数值字符串.
*/

function _formatMoney(s, type) {
    s = s.toString().replace(/,/g, "").replace(/ /g, "").replace("￥", "");
    if (/[^0-9\.]/.test(s)) return "0";
    if (s == null || s == "") return "0";
    s = s.toString().replace(/^(\d*)$/, "$1.");
    s = (s + "00").replace(/(\d*\.\d\d)\d*/, "$1");
    s = s.replace(".", ",");
    var re = /(\d)(\d{3},)/;
    while (re.test(s))
        s = s.replace(re, "$1,$2");
    s = s.replace(/,(\d\d)$/, ".$1");
    if (type == 0) {// 不带小数位(默认是有小数位)
        var a = s.split(".");
        if (a[1] == "00") {
            s = a[0];
        }
    }
    return s;
}

// 针对my97日历控件，把 onpicked 转化为change事件
function _datePicked(obj) { $(obj.srcEl).change(); }

//使用dt数据行填充select对象
function _fillSelect(ctlId, dt, fldTxt, fldVal, ResFirstOp) {
    if (!!dt) {
        var ctlSel = $("#" + ctlId);
        if (ctlSel.length == 1) {
            if (ResFirstOp)
                $("option:gt(0)", ctlSel).remove();
            else
                ctlSel.empty();
            for (var i = 0; i < dt.Rows.length; i++) {
                ctlSel.append("<option value='" + dt.Rows[i][fldVal] + "'>" + dt.Rows[i][fldTxt] + "</option>");
            }
        }
    }
}

(function ($) {
    $.fn.limitTextarea = function (opts) {
        var defaults = {
            maxNumber: 140, //允许输入的最大字数
            position: 'top', //提示文字的位置，top：文本框上方，bottom：文本框下方
            onOk: function () { }, //输入后，字数未超出时调用的函数
            onOver: function () { } //输入后，字数超出时调用的函数   
        }
        var option = $.extend(defaults, opts);
        this.each(function () {
            var _this = $(this);
            var info = '<div id="info">还可以输入<b><i>&nbsp;' + (option.maxNumber - _this.val().length) + '</i></b>&nbsp;&nbsp;字</div>';
            var fn = function () {
                var extraNumber = option.maxNumber - _this.val().length;
                var $info = $('#info');
                if (extraNumber >= 0) {
                    $info.html('还可以输入<b><i>&nbsp;' + extraNumber + '</i></b>&nbsp;&nbsp;字');
                    option.onOk();
                }
                else {
                    $info.html('已经超出<b style="color:red;"><i>&nbsp;' + (-extraNumber) + '</i></b>&nbsp;&nbsp;字');
                    option.onOver();
                }
            };
            switch (option.position) {
                case 'top':
                    _this.before(info);
                    break;
                case 'bottom':
                default:
                    _this.after(info);
            }
            //绑定输入事件监听器
            if (window.addEventListener) { //先执行W3C
                _this.get(0).addEventListener("input", fn, false);
            } else {
                _this.get(0).attachEvent("onpropertychange", fn);
            }
            _this.bind("keydown",function () {
                var key = window.event.keyCode;
                (key == 8 || key == 46) && fn(); //处理回退与删除
            });
            try {
                _this.get(0).attachEvent("oncut", fn); //处理粘贴

            } catch (e) {}
        });
    }
})(jQuery)
