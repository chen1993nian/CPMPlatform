<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AppSub.aspx.cs" Inherits="EIS.WebBase.SysFolder.AppFrame.AppSub" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>编辑记录</title>
    <meta http-equiv="Pragma" content="no-cache"/>
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
    <link type="text/css" rel="stylesheet" href="../../Css/kandytabs.css"  />
    <link type="text/css" rel="stylesheet" href="../../Css/appInput.css" />
    <link type="text/css" rel="stylesheet" href="../../Editor/kindeditor-4.1.10/themes/default/default.css" />
    <script type="text/javascript" src="../../js/jquery-1.7.min.js"></script>
    <script type="text/javascript" src="../../DatePicker/WdatePicker.js"></script>
    <script type="text/javascript" src="../../js/lhgdialog.min.js"></script>
    <script type="text/javascript" src="../../js/kandytabs.pack.js"></script>
    <script type="text/javascript" src="../../Editor/kindeditor-4.1.10/kindeditor-min.js"></script>
    <script type="text/javascript" src="../../Editor/kindeditor-4.1.10/lang/zh_CN.js"></script>
    <script type="text/javascript" src="../../js/jquery.notice.js"></script>
    <script type="text/javascript" src="../../js/jquery.smartMenu.js"></script>
    <script src="../../Js/DateExt.js" type="text/javascript"></script>
    <%=customScript%>
    <style type="text/css">
        .btbar{padding:10px;}
        body{overflow:auto;}
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div id="maindiv">
        <%=tblHTML%>
        <div class="btbar">
            <input type="button" class="btnSave btn01" value="保 存" />
            <input type="button" class="btnAdd btn01" value="添 加" />
            <input type="button" class="btnCopy btn01" value="复 制" />
            <input type="button" class="btnClose btn01" value="关 闭" />
        </div>
    </div>
    </form>
</body>
</html>
<script type="text/javascript">
    var _isNew = <%=isNew %>;
    var _mainTblName = "<%=mainTblName %>";
    var _tblName = "<%=tblName %>";
    var _rowId="<%=rowId %>";
    var _subId="<%=subId %>";
    var _mainId = "<%=mainId %>";
    var _sIndex = "<%=sIndex %>";
    var _saveAction = "1";
    var _workflowCode ="";
    var _nodeCode ="";
    var _curClass =EIS.WebBase.SysFolder.AppFrame.AppSub;
    var _xmlData =jQuery(jQuery.parseXML('<xml><%= xmlData %></xml>'));
    <%=this.sbModel.ToString() %>;
</script>
<script type="text/javascript">
    $(function () {
        if (_isNew) $(".btnAdd,.btnCopy,").prop("disabled", true);
        $(".btnSave").click(function () {
            var pwin = frameElement.lhgDG.curWin;
            //平台保存函数
            if (_sysSave()) {
                $.noticeAdd({ text: '保存成功！', stayTime: 500, onClose: function () {
                    if (_isNew)
                        pwin["_fnPopAddAfter"](_tblName, _rowId);

                    var _psys = pwin["_sys"];
                    var fieldArr = _sysModel.fields;
                    for (var i = 0; i < fieldArr.length; i++) {
                        var v = _sys.getValue(fieldArr[i].name, false);
                        if (_tmplRowId != v)
                            _psys.setValue(fieldArr[i].name, v, true, _rowId);
                    }

                    if (_isNew)
                        pwin._setValueXml("_AutoID", _subId, _rowId);

                    frameElement.lhgDG.cancel();
                }
                });

            }
        });

        $(".btnAdd").click(function () {
            if (_isNew) return;
            var _fnNewId = frameElement.lhgDG.curWin["_fnNewRowId"];
            var newId = _fnNewId(_tblName);
            window.location.href = "AppSub.aspx?tblName=" + _tblName + "&rowId=" + newId + "&subId=&mainId=" + _mainId + "&sindex=" + _sIndex;
        });

        $(".btnCopy").click(function () {
            if (_isNew) return;
            var _fnNewId = frameElement.lhgDG.curWin["_fnNewRowId"];
            var newId = _fnNewId(_tblName);
            window.location.href = "AppSub.aspx?tblName=" + _tblName + "&rowId=" + newId + "&subId=&mainId=" + _mainId + "&sindex=" + _sIndex + "&copyId=" + _subId;
        });

        $(".btnClose").click(function () {
            frameElement.lhgDG.cancel();
            //window.close();
        });
    });
</script>
<script src="../../Js/SubFunction.js" type="text/javascript"></script>
<script type="text/javascript">
    <%=editScriptBlock %>    
</script>