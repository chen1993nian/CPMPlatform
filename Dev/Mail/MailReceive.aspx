<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MailReceive.aspx.cs" Inherits="EIS.Web.Mail.MailReceive" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>我的收件箱</title>
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
	        outline-style: none; text-decoration: none;
        }
        a:hover {
	        text-decoration: underline;
        }
        .subject{color:#2131a1;}
        .flag{width:16px;margin:3px 2px 0px 0px;height:14px;float:left;overflow:hidden;padding:0px;}
        .unread{background:url(../img/email/mail03423d.png) no-repeat -48px 0px}
        .read{background:url(../img/email/mail03423d.png) no-repeat -48px -16px}
        .attach{background:url(../img/email/mail03423d.png) no-repeat -65px 2px}
		.flexigrid div.fbutton .transmit
	    {
		    background: url(../img/email/export.png) no-repeat center left transparent;
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
    var _curCalss = EIS.Web.Mail.MailReceive;
    $("#flex1").flexigrid
			(
			{
			    url: '../getdata.ashx',
			    params: [{ name: "queryid", value: "mail_receive" }
			        , { name: "condition", value: "folderId='<%=folderId %>'" }
			    ],
			    colModel: [
				{ display: '序号', name: 'autoid', width: 30, sortable: false, align: 'center', renderer: tranchk },
				{ display: '状态', name: 'state', width: 34, sortable: true, align: 'left', renderer: rendState },
				{ display: '发件人', name: 'sendername', width: 120, sortable: true, align: 'left' },
				{ display: '主题', name: 'subject', width: 320, sortable: true, align: 'left',renderer:rendSubject },
				{ display: '时间', name: 'sendtime', width: 120, sortable: true, align: 'left' },
                { display: '操作', name: 'autoid', width: 80, sortable: false, align: 'left', renderer: fnTransfer }
			    ],
			    buttons: [
				{ name: '删除', bclass: 'delete', onpress: app_delete },
				{ name: '彻底删除', bclass: 'delete', onpress: app_remove },
				{ name: '转移到', bclass: 'arrow', onpress: app_edit },
				{ separator: true },
				{ name: '查询', bclass: 'view', onpress: app_query },
				{ name: '清空', bclass: 'clear', onpress: app_reset }
				//{ separator: true },
				//{ name: '开始收信', bclass: 'add', onpress: app_start },
				//{ name: '停止收信', bclass: 'delete', onpress: app_stop }
			    ],
			    searchitems: [
			    { display: '发件人', name: 'sendername', type: 1 },
			    { display: '主题', name: 'subject', type: 1 },
                { display: '时间', name: 'sendtime', type: 4 }
			    ],
			    sortname: "sendtime",
			    sortorder: "desc",
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
			    function app_start() {
			        //EIS.Web.Mail.MailPOP3.StartPop3().value;
			    }
			    function app_stop() {
			        //EIS.Web.Mail.MailPOP3.StopPop3().value;
			    }
			    function fnTransfer(v,row) {
			        return "<a href='MailWrite.aspx?act=2&recId="+v+"'>&nbsp;转发</a><a href='MailWrite.aspx?act=1&recId="+v+"'>&nbsp;回复</a>";
			    }
			    function tranchk(v, row) {
			        return "<input type='checkbox' onclick='selectchk(this);' class='selectchk' value='" + v + "'/>";
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
			        return "<a href=\"javascript:readMail('" + mailid + "')\" class='subject' target='_self'>" + fldval + "</a>";
			    }
			    function readMail(mailId){
			        var param=$("#flex1")[0].p;
			        var sortdir="";
			        if(param.sortname.length>0){
			            sortdir =(param.sortname+" "+param.sortorder);
			        }
			        var url="MailRead.aspx?folderid=<%=folderId %>&mailId=" + mailId + "&sortdir="+sortdir;
        window.open(url,"_self");
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
        jQuery(".arrow").smartMenu(menuFolder
        , { name: "contextMenu1"
            , beforeShow: function () {
                //trId = $(this).attr("fileId");
            }
            , triggerEvent: "click"
            ,offsetX: 0
            ,offsetY: 10
        });

    });
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
    function app_add(cmd, grid) {
        openCenter("MailPOP3Edit.aspx", "_blank", 600, 400);
    }
    function app_reset(cmd, grid) {
        $("#flex1").clearQueryForm();
    }
    function app_edit(cmd, grid) {
        if ($('.trSelected', grid).length > 0) {
            var editid = $('.trSelected', grid)[0].id.substr(3);
            openCenter("MailPOP3Edit.aspx?editid=" + editid, "_blank", 600, 400);
        }
        else {
            alert("请选中一条记录");
        }
    }
    function app_delete(cmd, grid) {
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
        var ret  = _curCalss.DeleteMail(arr);
        if (ret.error) {
            alert("删除邮件时出错:" + ret.error.Message);
        }
        else {
            $("#flex1").flexReload();
        }
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



