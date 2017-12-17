<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MailTrash.aspx.cs" Inherits="EIS.Web.Mail.MailTrash" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>我的垃圾箱</title>
    <meta http-equiv="Pragma" content="no-cache"/>
    <link rel="stylesheet" type="text/css" href="../grid/css/flexigrid.css"/>
    <link rel="stylesheet" type="text/css" href="../Css/defStyle.css"/>
    <link rel="stylesheet" type="text/css" href="../css/smartMenu.css" />
    <script type="text/javascript" src="../js/jquery-1.7.min.js" ></script>

    <script type="text/javascript" src="../DatePicker/WdatePicker.js"></script> 
    <script type="text/javascript" src="../grid/flexigrid.js"></script>
    <script type="text/javascript" src="../js/jquery.smartMenu.js"></script>
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
	    .smart_menu_box{width:80px;}
        .smart_menu_a{padding-left:16px;height:24px;}
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
    var _curCalss = EIS.Web.Mail.MailTrash;
    $("#flex1").flexigrid
			(
			{
			    url: '../getdata.ashx',
			    params: [{ name: "queryid", value: "mail_trash" }
			        , { name: "condition", value: "" }
			    ],
			    colModel: [
				{ display: '序号', name: 'recid', width: 30, sortable: false, align: 'center', renderer: tranchk },
				{ display: '发件人', name: 'sendername', width: 120, sortable: true, align: 'left' },
				{ display: '主题', name: 'subject', width: 320, sortable: false, align: 'left',renderer:rendSubject },
				{ display: '时间', name: 'createtime', width: 120, sortable: true, align: 'left' }
			    ],
			    buttons: [
				//{ name: '清空邮件', bclass: 'delete', onpress: app_clear },
				{ name: '彻底删除', bclass: 'delete', onpress: app_remove },
				{ name: '转移到', bclass: 'transmit', onpress: app_edit },
				{ separator: true },
				{ name: '查询', bclass: 'view', onpress: app_query },
				{ name: '清空', bclass: 'clear', onpress: app_reset }
			    ],
			    searchitems: [
			    { display: '发件人', name: 'sendername', type: 1 },
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
    function tranchk(recId, row) {
        var mailId=$("mailid",row).text();
        return "<input type='checkbox' onclick='selectchk(this);' class='selectchk' value='" + recId + "|"+mailId+"'/>";
    }
    function selectchk(obj) {
        if (obj.checked) {
            $(obj).closest("tr").addClass("trSelected");
        }
        else {
            $(obj).closest("tr").removeClass("trSelected");
        }
    }

    var menuFolder = [[<%=sbMenu %>]];
    function menuClick(folderId) {
        var arr = [];
        $('.selectchk:checked').each(function(){
            arr.push(this.value);
        });
        if(arr.length==0)
        {
            alert("请先选中要转移的邮件");
            return;
        }
        var ret  = _curCalss.MoveMail(arr,folderId);
        if (ret.error) {
            alert("转移邮件时出错:" + ret.error.Message);
        }
        else {
            $("#flex1").flexReload();
        }
    }
    jQuery(function () {
        jQuery(".transmit").smartMenu(menuFolder
        , { name: "contextMenu1"
            , triggerEvent: "click"
            ,offsetX: 0
            ,offsetY: 10
        });

    });
    function app_selall() {
        $(".selectchk").each(function () {
            this.checked = true;
            $(this).closest("tr").addClass("trSelected");
        })
    }
    function rendSubject(fldval, row) {
        var mailid = $("mailid", row).text();
        return "<a href='MailRead.aspx?folderid=trash&mailid=" + mailid + "' class='subject' target='_self'>" + fldval + "</a>";
    }
    function rendState(state, row)
    {
        var attach = $("attach", row).text()+"";
        var arrHtml = [];
        if(state == "0")
        {
            arrHtml.push("<div class='flag unread'></div>");
            if (attach == "0") {
                arrHtml.push("<div class='flag'></div>");
            }
            else {
                arrHtml.push("<div class='flag attach'></div>");         
            }
        }
        else if(state == "1")
        {
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
    function app_clear() { 
    
    }
    function app_reset(cmd, grid) {
        $("#flex1").clearQueryForm();
    }
    function app_edit(cmd, grid) {

    }
    function app_remove(cmd, grid) {
        var arr = [];
        $('.selectchk:checked').each(function(){
            arr.push(this.value);
        });
        if(arr.length==0)
        {
            alert("请先选中邮件");
            return;
        }
        if(!confirm("您确认删除选中的邮件吗"))
            return;
        var ret  = _curCalss.RemoveMail(arr);
        if (ret.error) {
            alert("彻底删除邮件时出错:" + ret.error.Message);
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



