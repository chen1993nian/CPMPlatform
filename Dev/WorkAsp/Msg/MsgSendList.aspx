<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MsgSendList.aspx.cs" Inherits="EIS.Web.WorkAsp.Msg.MsgSendList" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
 
<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <title>发送消息列表 </title>
    <meta http-equiv="Pragma" content="no-cache" />
    <!--<meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />-->
    <link rel="stylesheet" type="text/css" href="../../grid/css/flexigrid2.css" />
	<link rel="stylesheet" type="text/css" href="../../Css/appList.css"/>
    <link rel="stylesheet" type="text/css" href="../../Css/datePicker.css" />
 
    <script type="text/javascript" src="../../grid/lib/jquery/jquery.js"></script>
    <script type="text/javascript" src="../../grid/flexigrid2.js"></script>
    <script type="text/javascript" src="../../js/jquery.datePicker-min.js"></script> 
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
    var curClass = EIS.Web.WorkAsp.Msg.MsgSendList;
    var para = "";
    $(function () {
    });
    $("#flex1").flexigrid
			(
			{
			    url: '../getdata.ashx',
			    params: [{ name: "queryid", value: "msgsendlist" }
			        , { name: "cryptcond", value: "" }
			        , { name: "sindex", value: "" }
			        , { name: "condition", value: "" }
                    , { name: "defaultvalue", value: "\"@employeeId\":<%=base.EmployeeID %>" }
			    ],
			    colModel: [
				{ display: '序号', name: 'rowindex', width: 30, sortable: false, align: 'center', renderer: false },
                { display: '', name: 'isread', width: 20, sortable: true, align: 'left', hide: false, renderer: readCol },
                { display: '消息内容', name: 'content', width: 320, sortable: true, align: 'left', hide: false, renderer: viewCol },
                { display: '接收人', name: 'recnames', width: 200, sortable: true, align: 'left', hide: false, renderer: false },
                { display: '发送时间', name: 'sendtime', width: 140, sortable: true, align: 'left', hide: false, renderer: false },
                { display: '操作', name: 'msgid', width: 40, sortable: true, align: 'center', hide: false, renderer: delCol }
			    ],
			    buttons: [
				{ name: '查询', bclass: 'view', onpress: app_query },
				{ name: '清空', bclass: 'clear', onpress: app_reset }
			    ],
			    searchitems: [
                { display: '消息内容', name: 'content', type: 1 },
                { display: '接收人', name: 'recnames', type: 1 },
                { display: '发送时间', name: 'sendtime', type: 4 }
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
			    height: 360,
			    onError: showError,
			    preProcess: false,
			    onColResize: fnColResize
			}
			);
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
                function viewMsg(msgId) {
                    openCenter("msgRead.aspx?msgId=" + msgId, "_blank", 560, 360);
                }
                function fnColResize(fieldname, width) {

                }
                function delCol(fldval, row) {
                    return "<a href='javascript:delRec(&quot;" + fldval + "&quot;);'>删除</a>";
                }

                function delRec(msgId) {
                    if (confirm('确定删除这条记录吗?')) {
                        var ret = curClass.DeleteMessage(msgId);
                        if (ret.error) {
                            alert("删除出错：" + ret.error.Message);
                        }
                        else {
                            alert("删除成功！");
                            $("#flex1").flexReload();
                        }
                    }
                }
                function readCol(fldval, row) {
                    var recNum = $("recids", row).text().split(",").length;
                    if (fldval == "1" && recNum == 1) {
                        return "<div class='flag read'></div>";
                    }
                    else if (fldval == "0" && recNum == 1) {
                        return "<div class='flag unread'></div>";
                    }
                    else {
                        return "";
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
