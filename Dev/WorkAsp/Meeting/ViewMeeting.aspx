<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewMeeting.aspx.cs" Inherits="EIS.Web.WorkAsp.Meeting.ViewMeeting" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1">
    <title>会议室日程</title>
    <link href="../../Theme/Default/main.css" rel="stylesheet" type="text/css" />
    <link href="../../Theme/Default/dailog.css" rel="stylesheet" type="text/css" />
    <link href="../../Theme/Default/calendar.css" rel="stylesheet" type="text/css" /> 
    <link href="../../Theme/Default/dp.css" rel="stylesheet" type="text/css" />   
    <link href="../../Theme/Default/alert.css" rel="stylesheet" type="text/css" />     
    <link href="../../Theme/Shared/blackbird.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        #bbit-cal-quickAddBTN{margin-left:45px;}
        .colorbox{border:0px solid gray;float:left;height:16px;width:16px;margin:3px 5px;}
    </style>
</head>
<body scroll="no">
    <form id="form1" runat="server">
    <div>
      <div id="calhead" style="padding-left:1px;padding-right:1px;">          
            <div class="cHead"><div class="ftitle">会议室日程</div>
            <div id="loadingpannel" class="ptogtitle loadicon" style="display: none;">正在加载数据...</div>
             <div id="errorpannel" class="ptogtitle loaderror" style="display: none;">非常抱歉，无法加载您的活动，请稍后再试</div>
            </div>          
            
            <div id="caltoolbar" class="ctoolbar">

            <div class="btnseparator"></div>
             <div id="showtodaybtn" class="fbutton">
                <div><span title='点击返回当前日程 ' class="showtoday">
                今天</span></div>
            </div>
              <div class="btnseparator"></div>
              <div  id="showweekbtn" class="fbutton">
                <div><span title='周' class="showweekview">周</span></div>
            </div>
              <div  id="showmonthbtn" class="fbutton fcurrent">
                <div><span title='月' class="showmonthview">月</span></div>
            </div>
            <div class="btnseparator"></div>
              <div  id="showreflashbtn" class="fbutton">
                <div><span title='刷新' class="showdayflash">刷新</span></div>
                </div>
             <div class="btnseparator"></div>
            <div id="sfprevbtn" title="上一个"  class="fbutton">
              <span class="fprev"></span>
            </div>
            <div id="sfnextbtn" title="下一个" class="fbutton">
                <span class="fnext"></span>
            </div>
            <div class="fshowdatep fbutton">
                    <div>
                        <input type="hidden" name="txtshow" id="hdtxtshow" />
                        <span id="txtdatetimeshow">Loading</span>
                    </div>
            </div>
            <div class="btnseparator"></div>
            <div style="float:left;">
                &nbsp;选择会议室：
                <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="False" >
                </asp:DropDownList>&nbsp;
            </div>

            <div class="btnseparator"></div>
            <div style="width:400px;float:left;margin-left:10px;height:22px;line-height:22px;padding-left:10px;vertical-align:middle;">
                <span style="float:left;">图例说明：</span>
                <div class="colorbox" style="background:#e67399;"></div><span style="float:left;">申请中</span>
                <div class="colorbox" style="background:#4cb052;"></div><span style="float:left;">已批准</span>
                <div class="colorbox" style="background:#ff9933;"></div><span style="float:left;">进行中</span>
                <div class="colorbox" style="background:#999;"></div>
                <input type="checkbox" name="chkShowCancel" style="vertical-align:middle;" id="chkShowCancel"/>
                <label for="chkShowCancel" title="显示已经取消的会议" style="cursor:pointer;">已取消</label>
            </div>
            <div class="clear"></div>
            </div>
      </div>
      <div style="padding:1px;">
        <div class="t1 chromeColor">
            &nbsp;</div>
        <div class="t2 chromeColor">
            &nbsp;</div>
        <div id="dvCalMain" class="calmain printborder">
            <div id="gridcontainer" style="overflow-y: visible;">
            </div>
        </div>
        <div class="t2 chromeColor">
            &nbsp;</div>
        <div class="t1 chromeColor">
            &nbsp;
        </div>   
        </div>
     
  </div>
    <script src="../../js/Calendar/jquery.min.js" type="text/javascript"></script>  
    <script src="../../js/Calendar/Common.js" type="text/javascript"></script>    
    <script src="../../js/blackbird.js" type="text/javascript"></script> 
    <script src="../../js/Calendar/datepicker_lang_zh_CN.js" type="text/javascript"></script>     
    <script src="../../js/Calendar/jquery.datepicker.js" type="text/javascript"></script>
    <script src="../../js/Calendar/jquery.alert.js" type="text/javascript"></script>    
    <script src="../../js/Calendar/jquery.ifrmdailog.js" defer="defer" type="text/javascript"></script>
    <script src="../../js/Calendar/xgcalendar_lang_zh_CN.js" type="text/javascript"></script>  
    <script src="../../js/Calendar/jquery.meeting.js" type="text/javascript"></script>   
    <script type="text/javascript">
        $(document).ready(function () {
            //[id,title,start,end，全天日程，跨日日程,循环日程,theme,'','']          
            var view = "month";

            var op = {
                view: view,
                theme: 0,
                autoload: true,
                showday: new Date(),
                EditCmdhandler: Edit,
                DeleteCmdhandler: Delete,
                ViewCmdhandler: View,
                onWeekOrMonthToDay: wtd,
                onBeforeRequestData: cal_beforerequest,
                onAfterRequestData: cal_afterrequest,
                onRequestDataError: cal_onerror,
                onAfterRender: cal_afterrender,
                readonly: true,
                url: "GetMeeting.aspx",
                quickAddUrl: "CalendarEdit.aspx?act=add", //快速添加日程Post Url 地址
                quickUpdateUrl: "CalendarEdit.aspx?act=update",
                quickDeleteUrl: "CalendarEdit.aspx?act=delete"//快速删除日程的              
            };
            var $dv = $("#calhead");
            var _MH = document.documentElement.clientHeight;
            var dvH = $dv.height() + 2;
            op.height = _MH - dvH;
            //op.eventItems = __CURRENTDATA;
            var p = $("#gridcontainer").bcalendar(op).BcalGetOp();

            $("#caltoolbar").noSelect();
            $("#DropDownList1").change(function () {

                $("#gridcontainer").BCalReload();
            });

            $("#hdtxtshow").datepicker({
                picker: "#txtdatetimeshow", showtarget: $("#txtdatetimeshow"),
                onReturn: function (r) {
                    var p = $("#gridcontainer").BCalGoToday(r).BcalGetOp();
                    if (p && p.datestrshow) {
                        $("#txtdatetimeshow").text(p.datestrshow);
                    }
                }
            });
            function cal_afterrender(option) {
                if (option && option.datestrshow) {
                    $("#txtdatetimeshow").text(option.datestrshow);
                }
            }
            $("#chkShowCancel").click(function () {
                $("#gridcontainer").BCalReload();
            });
            function cal_beforerequest(type, option) {
                var chkCancel = $("#chkShowCancel").attr("checked");
                option.extParam = [{ name: "hysName", value: $("#DropDownList1").val() }, { name: "showCancel", value: chkCancel }];
                var t = "正在加载数据...";
                switch (type) {
                    case 1:
                        t = "正在加载数据...";
                        break;
                    case 2:
                    case 3:
                    case 4:
                        t = "正在处理请求...";
                        break;
                }
                $("#errorpannel").hide();
                $("#loadingpannel").html(t).show();
            }
            function cal_afterrequest(type) {
                switch (type) {
                    case 1:
                        $("#loadingpannel").hide();
                        break;
                    case 2:
                    case 3:
                    case 4:
                        $("#loadingpannel").html("操作成功!");
                        window.setTimeout(function () { $("#loadingpannel").hide(); }, 2000);

                        break;
                }

            }
            function cal_onerror(type, data) {
                $("#errorpannel").show();
            }


            function Edit(data) {
                var eurl = "ModifyCalendar.aspx?Id={0}&start={2}&end={3}&isallday={4}&title={1}";
                if (data) {
                    var url = StrFormat(eurl, data);
                    OpenModelWindow(url, {
                        width: 600, height: 400, caption: "管理日程", onclose: function () {
                            $("#gridcontainer").BCalReload();
                        }
                    });
                }
            }

            function View(data) {
                var vurl = "../../SysFolder/AppFrame/AppDetail.aspx?tblName=T_OA_HY_Apply&condition=_autoid='{0}'";
                if (data) {
                    var url = StrFormat(vurl, data);
                    OpenModelWindow(url, { width: 800, height: 400, caption: "查看日程" });
                }
            }

            function Delete(data, callback) {

                $.alerts.okButton = "确定";
                $.alerts.cancelButton = "取消";
                hiConfirm("是否要删除该日程?", '确认', function (r) { r && callback(0); });
            }

            function wtd(p) {
                if (p && p.datestrshow) {
                    $("#txtdatetimeshow").text(p.datestrshow);
                }
                $("#caltoolbar div.fcurrent").each(function () {
                    $(this).removeClass("fcurrent");
                })
                $("#showdaybtn").addClass("fcurrent");
            }
            //显示日视图
            $("#showdaybtn").click(function (e) {
                //document.location.href="#day";
                $("#caltoolbar div.fcurrent").each(function () {
                    $(this).removeClass("fcurrent");
                })
                $(this).addClass("fcurrent");
                var p = $("#gridcontainer").BCalSwtichview("day").BcalGetOp();

            });
            //显示周视图
            $("#showweekbtn").click(function (e) {
                //document.location.href="#week";
                $("#caltoolbar div.fcurrent").each(function () {
                    $(this).removeClass("fcurrent");
                })
                $(this).addClass("fcurrent");
                var p = $("#gridcontainer").BCalSwtichview("week").BcalGetOp();


            });
            //显示月视图
            $("#showmonthbtn").click(function (e) {
                //document.location.href="#month";
                $("#caltoolbar div.fcurrent").each(function () {
                    $(this).removeClass("fcurrent");
                })
                $(this).addClass("fcurrent");
                var p = $("#gridcontainer").BCalSwtichview("month").BcalGetOp();

            });
            //重新加载
            $("#showreflashbtn").click(function (e) {
                $("#gridcontainer").BCalReload();
            });

            //点击新增日程
            $("#faddbtn").click(function (e) {
                var url = "ModifyCalendar.aspx?Id=0&start=&end=&isallday=&title=新增日程";
                OpenModelWindow(url, { width: 600, height: 400, caption: "新增日程" });
            });
            //点击回到今天
            $("#showtodaybtn").click(function (e) {
                var p = $("#gridcontainer").BCalGoToday().BcalGetOp();



            });
            //上一个
            $("#sfprevbtn").click(function (e) {
                var p = $("#gridcontainer").BCalPrev().BcalGetOp();


            });
            //下一个
            $("#sfnextbtn").click(function (e) {
                var p = $("#gridcontainer").BCalNext().BcalGetOp();
            });

        });
    </script>
    </form>
</body>
</html>


