<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MailSend.aspx.cs" Inherits="EIS.Web.Mail.MailSend" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>我的发件箱</title>
    <meta http-equiv="Pragma" content="no-cache"/>
    <link rel="stylesheet" type="text/css" href="../grid/css/flexigrid.css"/>

    <link rel="stylesheet" type="text/css" href="../Css/defStyle.css"/>

    <script type="text/javascript" src="../grid/lib/jquery/jquery.js"></script>
    <script type="text/javascript" src="../DatePicker/WdatePicker.js"></script> 

    <script type="text/javascript" src="../grid/flexigrid.js"></script>
    <style type="text/css">
        a {
	        outline-style: none; cursor: pointer; text-decoration: none
        }
        a:hover {
	        text-decoration: underline
        }
        .subject{color:#2131a1;}
        .flag{width:16px;margin:3px 2px 0px 0px;height:14px;float:left;overflow:hidden;}
        .unread{background:url(../img/email/mail03423d.png) no-repeat -48px 0px}
        .read{background:url(../img/email/mail03423d.png) no-repeat -48px -16px}
        .attach{background:url(../img/email/mail03423d.png) no-repeat -65px 2px}
		.flexigrid div.fbutton .transmit
	    {
		    background: url(../img/email/export.png) no-repeat center left transparent;
	    }
	    .flexigrid div.fbutton .transfer
	    {
		    background: url(../img/common/folder.gif) no-repeat center left transparent;
	    }
    </style>
</head>
<body style="margin:2px">
    <form id="form1" runat="server">
    <div id="griddiv">
        <table id="flex1" style="display:none"></table>    
    </div>
    </form>
</body>
</html>
<script type="text/javascript">
<!--

    var maiheight = document.documentElement.clientHeight;
    var otherpm = 150; //GridHead，toolbar，footer,gridmargin
    var gh = maiheight - otherpm;
    var _curCalss = EIS.Web.Mail.MailSend;
    $("#flex1").flexigrid
			(
			{
			    url: '../getdata.ashx',
			    params: [{ name: "queryid", value: "mail_send" }
			        , { name: "condition", value: "" }
			    ],
			    colModel: [
				{ display: '序号', name: 'mailid', width: 30, sortable: false, align: 'center', renderer: tranchk },
				{ display: '主题', name: 'subject', width: 320, sortable: false, align: 'left', renderer: rendSubject },
				{ display: '收件人', name: 'receivers', width: 120, sortable: true, align: 'left' },
				{ display: '发送时间', name: 'createtime', width: 120, sortable: true, align: 'left' }
			    ],
			    buttons: [
				{ name: '删除', bclass: 'delete', onpress: app_delete },
				{ name: '彻底删除', bclass: 'delete', onpress: app_remove },
				{ separator: true },
				{ name: '查询', bclass: 'view', onpress: app_query },
				{ name: '清空', bclass: 'clear', onpress: app_reset },
				{ separator: true }
			    ],
			    searchitems: [
			    { display: '收件人', name: 'receivers', type: 1 },
			    { display: '主题', name: 'subject', type: 1 },
                { display: '时间', name: 'createtime', type: 4 }
			    ],
			    sortname: "",
			    sortorder: "",
			    usepager: true,
			    singleSelect: false,
			    useRp: true,
			    rp: 12,
			    multisel: true,
			    showTableToggleBtn: true,
			    resizable: false,
			    height: gh,
			    onError: showError
			}
			);

    function tranchk(mailid, row) {
        return "<input type='checkbox' onclick='selectchk(this);' class='selectchk' value='" + mailid + "'/>";
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
    function rendSubject(fldval, row) {
        var mailid = $("mailid", row).text();
        return "<a href='MailRead.aspx?folderid=send&mailid=" + mailid + "' class='subject' target='_self'>" + fldval + "</a>";
    }
    function rendState(state, row) {
        var attach = $("attach", row).text() + "";
        var arrHtml = [];
        if (state == "0") {
            arrHtml.push("<div class='flag unread'></div>");
            if (attach == "0") {
                arrHtml.push("<div class='flag'></div>");
            }
            else {
                arrHtml.push("<div class='flag attach'></div>");
            }
        }
        else if (state == "1") {
            arrHtml.push("<div class='flag read'></div>");
            if (attach == "0") {
                arrHtml.push("<div class='flag'></div>");
            }
            else {
                arrHtml.push("<div class='flag attach'></div>");
            }
        }
        return arrHtml.join("");

    }
    function showError(data) {
        //alert("加载数据出错");
    }
    function app_reset(cmd, grid) {
        $("#flex1").clearQueryForm();
    }
    function app_remove(cmd, grid) {
        var arr = [];
        $('.selectchk:checked').each(function () {
            arr.push(this.value);
        });
        if (arr.length == 0) {
            alert("请先选中邮件");
            return;
        }
        if (!confirm("您确认删除选中的邮件吗"))
            return;
        var ret = _curCalss.RemoveMail(arr);
        if (ret.error) {
            alert("彻底删除邮件时出错:" + ret.error.Message);
        }
        else {
            $("#flex1").flexReload();
        }
    }
    function app_delete(cmd, grid) {
        var arr = [];
        $('.selectchk:checked').each(function () {
            arr.push(this.value);
        });
        if (arr.length == 0) {
            alert("请先选中邮件");
            return;
        }
        if (!confirm("您确认删除选中的邮件吗"))
            return;
        var ret = _curCalss.DeleteMail(arr);
        if (ret.error) {
            alert("删除邮件时出错:" + ret.error.Message);
        }
        else {
            $("#flex1").flexReload();
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



