<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MailPOP3.aspx.cs" Inherits="EIS.Web.Mail.MailPOP3" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>外部邮箱设置</title>
    <meta http-equiv="Pragma" content="no-cache"/>
    <link rel="stylesheet" type="text/css" href="../grid/css/flexigrid.css"/>
    <link rel="stylesheet" type="text/css" href="../Css/DefStyle.css"/>
    <script type="text/javascript" src="../grid/lib/jquery/jquery.js"></script>
    <script type="text/javascript" src="../grid/flexigrid.js"></script>
</head>
<body style="margin:2px">
    <form id="form1" runat="server">
    <div id="griddiv" name="griddiv">
        <table id="flex1" style="display:none"></table>    
    </div>
    </form>
</body>
</html>
<script type="text/javascript" language="JavaScript">
<!--

    var maiheight = document.documentElement.clientHeight;
    var otherpm = 150; //GridHead，toolbar，footer,gridmargin
    var gh = maiheight - otherpm;

    $("#flex1").flexigrid
    (
    {
        url: '../getdata.ashx',
        params: [{ name: "queryid", value: "mail_pop3" }
                , { name: "condition", value: "" }
        ],
        colModel: [
            { display: '序号', name: 'rowindex', width: 30, sortable: false, align: 'center' },
            { display: '电子邮件地址', name: 'emailadrr', width: 180, sortable: true, align: 'left' },
            { display: '接收服务器', name: 'pop3adrr', width: 120, sortable: true, align: 'left' },
            { display: '发送服务器', name: 'smtpadrr', width: 120, sortable: false, align: 'left' },
            { display: '登录账户', name: 'account', width: 160, sortable: true, align: 'left' }
        ],
        buttons: [
            { name: '添加', bclass: 'add', onpress: app_add },
            { name: '修改', bclass: 'edit', onpress: app_edit },
            { name: '删除', bclass: 'delete', onpress: app_delete },
            { separator: true },
            { name: '查询', bclass: 'view', onpress: app_query },
            { name: '清空', bclass: 'clear', onpress: app_reset }
            //{ separator: true },
            //{ name: '开始收信', bclass: 'add', onpress: app_start },
            //{ name: '停止收信', bclass: 'delete', onpress: app_stop }
        ],
        searchitems: [
            { display: '业务名称', name: 'tablename', type: 1 },
            { display: '中文名称', name: 'tablenamecn', type: 1 }
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
        onError: showError
    }
    );
    function app_start() {
        EIS.Web.Mail.MailPOP3.StartPop3().value;
    }
    function app_stop() {
        EIS.Web.Mail.MailPOP3.StopPop3().value;
    }

    function showError(data) {
        //alert("加载数据出错");
    }
    function app_add(cmd, grid) {
        openCenter("MailPOP3Edit.aspx", "_blank", 600, 500);
    }
    function app_reset(cmd, grid) {
        $("#flex1").clearQueryForm();
    }
    function app_edit(cmd, grid) {
        if ($('.trSelected', grid).length > 0) {
            var editid = $('.trSelected', grid)[0].id.substr(3);
            openCenter("MailPOP3Edit.aspx?editid=" + editid, "_blank", 600, 500);
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
                        var ret = EIS.Web.Mail.MailPOP3.DelRecord(this.id.substr(3));
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

