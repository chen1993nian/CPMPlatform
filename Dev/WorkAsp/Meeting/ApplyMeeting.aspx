<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ApplyMeeting.aspx.cs" Inherits="EIS.Web.WorkAsp.Meeting.ApplyMeeting" %>


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
            <div class="cHead"><div class="ftitle">会议室预定情况</div>
            <div id="loadingpannel" class="ptogtitle loadicon" style="display: none;">正在加载数据...</div>
             <div id="errorpannel" class="ptogtitle loaderror" style="display: none;">非常抱歉，无法加载您的活动，请稍后再试</div>
            </div>          
            
            <div id="caltoolbar" class="ctoolbar" style="height:26px;padding-top:4px;">

             <div id="showtodaybtn" class="fbutton">
                <div><span title='点击返回当前日程 ' class="showtoday">
                今天</span></div>
            </div>
              <div class="btnseparator"></div>
              <div id="showdaybtn" class="fbutton">
                <div><span title='日' class="showdayview">日</span></div>
            </div>
              <div  id="showweekbtn" class="fbutton">
                <div><span title='周' class="showweekview">周</span></div>
            </div>
              <div  id="showmonthbtn" class="fbutton">
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
    <script src="../../Js/DateExt.js" type="text/javascript"></script>
    <script type="text/javascript">
        var arrHys = [<%=hysList %>];

        function getHys(hysName) {
            for (var i = 0; i < arrHys.length; i++) {
                if (arrHys[i].HysName == hysName) {
                    return arrHys[i];
                }
            }
            return null;
        }

        if ("<%=view %>" == "week") {
            $("#showweekbtn").addClass("fcurrent");
        }
        else if ("<%=view %>" == "month") {
            $("#showmonthbtn").addClass("fcurrent");
        }
        var i18n = $.extend({}, i18n || {}, {
            xgcalendar: {
                dateformat: {
                    "fulldaykey": "yyyyMMdd",
                    "fulldayshow": "yyyy年M月d日",
                    "fulldayvalue": "yyyy-M-d",
                    "Md": "M/d (W)",
                    "Md3": "M月d日",
                    "separator": "-",
                    "year_index": 0,
                    "month_index": 1,
                    "day_index": 2,
                    "day": "d日",
                    "sun": "周日",
                    "mon": "周一",
                    "tue": "周二",
                    "wed": "周三",
                    "thu": "周四",
                    "fri": "周五",
                    "sat": "周六",
                    "jan": "一月",
                    "feb": "二月",
                    "mar": "三月",
                    "apr": "四月",
                    "may": "五月",
                    "jun": "六月",
                    "jul": "七月",
                    "aug": "八月",
                    "sep": "九月",
                    "oct": "十月",
                    "nov": "十一月",
                    "dec": "十二月"
                },
                "no_implemented": "没有实现",
                "to_date_view": "点击转到该日期的日视图",
                "i_undefined": "未设置",
                "allday_event": "全天会议",
                "repeat_event": "跨天会议",
                "time": "时  间",
                "event": "事  件",
                "location": "地  点",
                "participant": "参与人",
                "get_data_exception": "获取数据发生异常",
                "new_event": "新会议",
                "confirm_delete_event": "确定删除该会议吗？",
                "confrim_delete_event_or_all": "删除此序列还是单个事件？\r\n点击[确定]删除事件,点击[取消]删除序列",
                "data_format_error": "数据格式错误！",
                "invalid_title": "会议标题不能为空",
                "view_no_ready": "视图未准备就绪",
                "example": "例如：每周一工作例会",
                "content": "会 议",
                "create_event": "发起会议申请",
                "update_detail": "",
                "click_to_detail": "点击查看详细",
                "i_delete": "删除",
                "day_plural": "天",
                "others": "另外",
                "item": "个"
            }
        });
        </script>
    <script type="text/javascript">

        $(document).ready(function () {
            //[id,title,start,end，全天日程，跨日日程,循环日程,theme,'','']          
            var view = "<%=view %>";

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
                readonly: false,
                url: "GetMeeting.aspx",
                quickAddUrl: "MeetingSave.aspx?act=add", //快速添加日程Post Url 地址
                quickUpdateUrl: "MeetingSave.aspx?act=update",
                quickDeleteUrl: "MeetingSave.aspx?act=delete", //快速删除日程的
                quickAddHandler: fnQuickAdd,
                onBeforeDrag: fnBeforeDrag,
                onWeekOrMonthToDay: fnToDayView
            };
            function fnToDayView(day) {
                window.location.href = "ApplyMeetingDay.aspx?date=" + day;
            }
            function fnBeforeDrag(type, date) {
                var aheadDate = "<%=AheadDate %>";
                if (aheadDate == "") {
                    return true;
                }
                var alertMsg = "选定日期不可预定，最多可以预定到 " + aheadDate;
                if (!date)
                    return true;
                var arrd1 = date.split("-");
                var arrd2 = aheadDate.split("-");
                var d1 = new Date(arrd1[0], arrd1[1], arrd1[2]);
                var d2 = new Date(arrd2[0], arrd2[1], arrd2[2]);

                if (type == 1) {
                    if (DateDiff("d", d1, d2) > 0) {
                        return true;
                    }
                    else {
                        alert(alertMsg);
                        return false;
                    }
                }
                else if (type == 2) {
                    if (DateDiff("d", d1, d2) > 0) {
                        return true;
                    }
                    else {
                        alert(alertMsg);
                        return false;
                    }
                }
                else if (type == 3) {
                    if (DateDiff("d", d1, d2) > 0) {
                        return true;
                    }
                    else {
                        alert(alertMsg);
                        return false;
                    }
                }
                else {
                    return true;
                }

            }
            function fnQuickAdd(param) {

                var arr = [];
                var hysName = $("#DropDownList1").val();
                arr.push("HyName=", escape(param[0].value), "^0|");
                var startTime = param[1].value;
                var endTime = param[2].value;
                if (param[3].value == "1") {
                    startTime = startTime.split(" ")[0] + " " + getHys(hysName).StartTime;
                    endTime = endTime.split(" ")[0] + " " + getHys(hysName).EndTime;
                }
                arr.push("StartTime=", encodeURIComponent(startTime), "^0|");
                arr.push("EndTime=", encodeURIComponent(endTime), "^0|");

                arr.push("HyAddr=", escape(hysName), "^0");
                var url = "../../SysFolder/Workflow/SelectWorkFlow.aspx?tblName=T_OA_HY_Apply&cpro=" + arr.join("");
                _openCenter(url, "_blank", 1000, 700);

            }
            function fnQuickUpdate() {

            }
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
                var arr = [];
                var hysName = $("#DropDownList1").val();
                arr.push("HyName=", encodeURIComponent(data[1]), "^0|");
                var startTime = data[2];
                var endTime = data[3];
                if (data[4] == "1") {
                    startTime = startTime.split(" ")[0] + " " + getHys(hysName).StartTime;
                    endTime = endTime.split(" ")[0] + " " + getHys(hysName).EndTime;
                }
                arr.push("StartTime=", encodeURIComponent(startTime), "^0|");
                arr.push("EndTime=", encodeURIComponent(endTime), "^0|");
                arr.push("HyAddr=", encodeURIComponent(hysName), "^0|");
                arr.push("HyState=", encodeURIComponent('是'), "^0");
                var url = "../../SysFolder/AppFrame/AppInput.aspx?tblName=T_OA_HY_Apply&condition=&T_OA_HY_Applycpro=" + arr.join("");
                _openCenter(url, "_blank", 1000, 700);
            }

            function View(data) {
                var vurl = "../../SysFolder/AppFrame/AppDetail.aspx?tblName=T_OA_HY_Apply&mainId={0}&toolbar=false";
                if (data) {
                    var url = StrFormat(vurl, data);
                    OpenModelWindow(url, { width: 800, height: 460, caption: "查看日程" });
                }
            }

            function app_query() {
                $("#gridcontainer").BCalReload();
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
                fnToDayView("");
                return;
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


