<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MsgRecList.aspx.cs" Inherits="EIS.Web.WorkAsp.Msg.MsgRecList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
 
<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <title>接收消息列表 </title>
    <meta http-equiv="Pragma" content="no-cache" />
    <!--<meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />-->
    <link rel="stylesheet" type="text/css" href="../../grid/css/flexigrid2.css" />
	<link rel="stylesheet" type="text/css" href="../../Css/appList.css"/>
    <link rel="stylesheet" type="text/css" href="../../Css/datePicker.css" />
    <script type="text/javascript" src="../../grid/lib/jquery/jquery.js"></script>
    <script type="text/javascript" src="../../grid/flexigrid2.js"></script>
    <script type="text/javascript" src="../../DatePicker/WdatePicker.js"></script> 
    <style type="text/css">
        html{color:#4d4d4d;
        }
        body{
             font:12px tahoma, arial, 宋体;
             padding:0px;
             margin:0px;
         }
         .record-tit-link
         {
            line-height:30px;
            width:300px;
            vertical-align:middle;
         }
         .record-tit-link a{
            text-decoration:none;        
            color:#08c;
          }
          .msgtitle{text-decoration:none;color:blue; }
          .msgtitle:hover{text-decoration:underline; }
        .flag{width:16px;margin:3px 2px 0px 0px;height:14px;float:left;overflow:hidden;padding:0px;}
        .unread{background:url(../../img/email/mail03423d.png) no-repeat -48px 0px}
        .read{background:url(../../img/email/mail03423d.png) no-repeat -48px -16px}
    </style>
</head>
<body scroll="no">
    <form runat="server" id="form1">
    <div id="griddiv" >
        <table id="flex1" style="display:none"></table>    
    </div>
    </form>
</body>
</html>
<script type="text/javascript"> 
<!--
    var curClass = EIS.Web.WorkAsp.Msg.MsgRecList;
    var para = "";
    $(function () {
    });
    $("#flex1").flexigrid
			(
			{
			    url: '../getdata.ashx',
			    params: [{ name: "queryid", value: "msgreclist" }
			        , { name: "cryptcond", value: "" }
			        , { name: "sindex", value: "" }
			        , { name: "condition", value: "" }
                    , { name: "defaultvalue", value: "\"@employeeId\":<%=base.EmployeeID %>" }
			    ],
			    colModel: [
				{ display: '序号', name: 'rowindex', width: 30, sortable: false, align: 'center', renderer: false },
                { display: '', name: 'isread', width: 20, sortable: true, align: 'left', hide: false, renderer: readCol },
                { display: '消息内容', name: 'content', width: 280, sortable: true, align: 'left', hide: false, renderer: viewCol },
                { display: '发送人', name: 'sender', width: 80, sortable: true, align: 'left', hide: false, renderer: false },
                { display: '发送时间', name: 'sendtime', width: 180, sortable: true, align: 'left', hide: false, renderer: false },
                { display: '操作', name: 'msgid', width: 40, sortable: true, align: 'center', hide: false, renderer: delCol }
			    ],
			    buttons: [
				{ name: '全部已阅', bclass: 'check', onpress: app_read },
				{ name: '删除全部', bclass: 'delete', onpress: app_delAll },
				{ name: '删除已阅', bclass: 'delete', onpress: app_delAllRead },
				{ separator: true },
				{ name: '查询', bclass: 'view', onpress: app_query },
				{ name: '清空条件', bclass: 'clear', onpress: app_reset }
			    ],
			    searchitems: [
                { display: '消息内容', name: 'content', type: 1 },
                { display: '发送人', name: 'sender', type: 1 },
                { display: '发送时间', name: 'sendtime', type: 4 }
			    ],
			    sortname: "sendtime",
			    sortorder: "desc",
			    usepager: true,
			    singleSelect: true,
			    useRp: true,
			    rp: 15,
			    multisel: false,
			    showTableToggleBtn: false,
			    resizable: false,
			    height: 360,
			    onError: showError,
			    preProcess: false,
			    onColResize: fnColResize
			}
			);

                function app_read() {
                    var ret = curClass.ReadAll();
                    if (ret.error)
                        alert("操作出错：" + ret.error.Message);
                    else
                        $("#flex1").flexReload();
                }
                function app_delAllRead() {
                    if (!confirm('确定删除所有已阅消息吗?'))
                        return;
                    var ret = curClass.DeleteAllReaded();
                    if (ret.error)
                        alert("操作出错：" + ret.error.Message);
                    else
                        $("#flex1").flexReload();
                }

                function app_delAll() {
                    if (!confirm('确定删除所有消息吗?'))
                        return;
                    var ret = curClass.DeleteAll();
                    if (ret.error)
                        alert("操作出错：" + ret.error.Message);
                    else
                        $("#flex1").flexReload();
                }

                function showError(data) {
                    alert("加载数据出错");
                }

                function viewCol(fldval, row) {
                    var msgId = $("msgid", row).text();
                    var arr = [];
                    arr.push("<a title='点击查看详情' class='msgtitle' href='javascript:viewMsg(&quot;" + msgId + "&quot;)'>");
                    arr.push(fldval);
                    arr.push("</a>");
                    return arr.join("")
                }
                function delCol(fldval, row) {
                    return "<a href='javascript:delRec(&quot;" + fldval + "&quot;);'>删除</a>";
                }

                function delRec(msgId) {
                    if (confirm('确定删除这条记录吗?')) {
                        var ret = curClass.DeleteReadRec(msgId);
                        if (ret.error) {
                            alert("删除出错：" + ret.error.Message);
                        }
                        else {
                            alert("删除成功！");
                            $("#flex1").flexReload();
                        }
                    }
                }
                function viewMsg(msgId) {
                    openCenter("msgRead.aspx?msgId=" + msgId, "_blank", 660, 400);
                }
                function fnColResize(fieldname, width) {

                }
                function readCol(fldval, row) {
                    if (fldval == "1") {
                        return "<div class='flag read'></div>";
                    }
                    else {
                        return "<div class='flag unread'></div>";
                    }
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
                                            var ret = AppDefault.DelRecord("T_OA_News", this.id.substr(3));
                                            if (ret.error) {
                                                alert("删除出错：" + ret.error.Message);
                                            }
                                            else {
                                                alert("删除成功！");
                                                $("#flex1").flexReload();
                                            }
                                        }
                                    );

                        }
                    }
                    else {
                        alert("请选中一条记录");
                    }
                }
                function colIndex(fldval, row) {
                    var autoid = $(row).attr("id");
                    return "<a href=\"../../SysFolder/AppFrame/AppDetail.aspx?tblName=T_OA_News&condition=_autoid='" + autoid + "'\" target='_blank'>" + fldval + "</a>";
                }
                function app_view() {

                }
                function app_wfinfo(mainId) {
                    var url = "AppWorkFlowInfo.aspx?tblName=T_OA_News&mainId=" + mainId;
                    openCenter(url, "_blank", 1000, 700);
                }
                function addCallBack() {
                    $("#flex1").flexReload();
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
 

