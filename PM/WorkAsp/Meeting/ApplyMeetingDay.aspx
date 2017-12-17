<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ApplyMeetingDay.aspx.cs" Inherits="EIS.Web.WorkAsp.Meeting.ApplyMeetingDay" %>

 
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1">
    <title>会议室日程</title>
    <link href="../../Theme/Default/main.css" rel="stylesheet" type="text/css" />
    <link href="../../Theme/Default/dailog.css" rel="stylesheet" type="text/css" />
    <link href="../../Theme/Default/calendar.css" rel="stylesheet" type="text/css" /> 
    <script src="../../Js/jquery-1.7.min.js" type="text/javascript"></script>
    <script src="../../js/Calendar/Common.js" type="text/javascript"></script>    
    <script src="../../Js/DateExt.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../DatePicker/WdatePicker.js"></script>
    <script type="text/javascript" src="../../Js/lhgdialog.min.js?t=self"></script>

    <style type="text/css">
        body{font-family:"微软雅黑" ,"宋体", Consolas, Verdana;font-size:10pt;background:#f6f7f9;padding:10px;overflow-x:hidden;}
        .maintbl{border:1px solid #666;table-layout:fixed;border-collapse:collapse;background:white;}
        .maintbl tbody>tr>th{border:1px solid #666;padding:5px 5px 5px 4px;background-color:#e8eef7;color:#468;font-weight:normal;font-size:12px;}
        .maintbl tbody>tr>td{border:1px solid #666;padding:5px 5px 5px 4px;}
        
        .maintbl tbody>tr>th.hourtd{width:49px;padding:0px;border:1px solid #666;}
        .maintbl tbody>tr>td.hystd{padding-left:24px;background:url(../../img/common/home.png) no-repeat 5px center;}
        .hystd span{color:#367abb;text-decoration:none;font-size:11px;color:Gray;}
        .hystd a{color:#367abb;text-decoration:none;}
        .hystd a:hover{color:red;text-decoration:none;}
        .toolPanel{border:1px solid #99bbe8;padding:5px;margin:5px 0px;width:900px;background:#99bbe6 url(../../img/common/topbar.gif);}
        .label{vertical-align:middle;}
        input[type=checkbox]{vertical-align:middle;}
        .toolPanel input[type=button]{cursor:pointer;border:1px solid transparent;background-color:transparent;height:24px;padding-left:2px;padding-right:2px;}
        .toolPanel input.btnHover{cursor:pointer;background-color:#EDF1D5;border: #555 1px solid;}
        .Wdate{width:140px;}
        #bbit-cal-quickAddBTN{height:24px;}
        .maintbl tbody>tr>td.chiptd{padding:0px;}
        .colorbox{border:0px solid gray;float:left;height:16px;width:16px;margin:3px 5px;}
        .chipPanel{height:30px;background:#ffa;position:relative;left:0px;top:0px;}
        .chipPanel .item{position:absolute;height:28px;line-height:28px;background:#e67399;border:0px solid #fff;overflow:hidden;
                         color:White;font-size:10px;padding-left:2px;cursor:pointer;border-radius:3px;}
        .tip{
            overflow:auto;
	        overflow-x:hidden;
            margin-top:10px;
            margin-bottom:10px;
            border:dotted 1px orange;
            background:#F9FB91;
            text-align:left;
            padding:5px;
            width:900px;
        }
    </style>
</head>
<body>
    <div id="mainZone">
        <div class="toolPanel">
            <div style="float:left;">
                <span>日期：</span>
                <input type="text" id="txtDate" class='Wdate' readonly="readonly" />
                <input type="button" class="btnPrev" value="前一天" />
                <input type="button" class="btnNext" value="后一天" />
                <input type="button" class="btnLoad" value="刷新" />
                <a href="ApplyMeeting.aspx?view=week" style="color:Blue;" title="切换到周视图" target="_self">[周]</a>
                &nbsp;
                <a href="ApplyMeeting.aspx?view=month" style="color:Blue;" title="切换到月视图" target="_self">[月]</a>

            </div>

            <div style="width:450px;float:left;margin-left:10px;height:22px;line-height:22px;padding-left:10px;vertical-align:middle;">
                <span style="float:left;">图例:</span>
                <div class="colorbox" style="background:#e67399;"></div><span style="float:left;">申请中&nbsp;</span>
                <div class="colorbox" style="background:#4cb052;"></div><span style="float:left;">已批准&nbsp;</span>
                <div class="colorbox" style="background:#ff9933;"></div><span style="float:left;">进行中&nbsp;</span>
                <div class="colorbox" style="background:#999;"></div>
                <input type="checkbox" name="chkShowCancel" style="vertical-align:middle;" id="chkShowCancel"/>
                <label for="chkShowCancel" title="显示已经取消的会议" style="cursor:pointer;">已取消</label>
            </div>
            <div style="clear:both;"></div>
        </div>
        <div class="bodyZone">
            <table class="maintbl" border="1" cellpadding="0" cellspacing="0">
                <tbody>
                    <%=bodyHtml %>
                </tbody>
            </table>
            <div class="tip">使用说明：在会议室右侧黄色区域上按下鼠标，拖拽至会议结束时间，松开鼠标，然后在弹出小窗口中输入会议名称。
            </div>
        </div>
    </div>
</body>
</html>
<script type="text/javascript">
    function fnQuickAdd(param) {
        var arr = [];
        arr.push("HyName=", escape(param[0].value), "^0|");
        arr.push("StartTime=", escape(param[1].value), "^0|");
        arr.push("EndTime=", escape(param[2].value), "^0|");
        arr.push("HyAddr=", escape(_curHys), "^0");
        var url = "../../SysFolder/Workflow/SelectWorkFlow.aspx?tblName=T_OA_HY_Apply&cpro=" + arr.join("");
        _openCenter(url, "_blank", 1000, 700);

    }

    function Edit(data) {
        var what = $("#bbit-cal-what").val();
        var f = /^[^\$\<\>]+$/.test(what);
        if (!f) {
            alert("请输入会议名称");
            $("#bbit-cal-what").focus();
            return false;
        }
        var arr = [];
        arr.push("HyName=", encodeURIComponent(data[1]), "^0|");
        arr.push("StartTime=", encodeURIComponent(data[2]), "^0|");
        arr.push("EndTime=", encodeURIComponent(data[3]), "^0|");
        arr.push("HyAddr=", encodeURIComponent(_curHys), "^0|");
        arr.push("HyState=", encodeURIComponent('是'), "^0");
        var url = "../../SysFolder/AppFrame/AppInput.aspx?tblName=T_OA_HY_Apply&condition=&T_OA_HY_Applycpro=" + arr.join("");
        _openCenter(url, "_blank", 1000, 700);
    }
    function app_query() {
        loadEvents();
    }
    function View(hyId) {
        var url = "../../SysFolder/AppFrame/AppDetail.aspx?tblName=T_OA_HY_Apply&mainId=" + hyId + "&toolbar=false";
        var dlg = new $.dialog({
            title: '查看会议信息', maxBtn: false, page: url
                , btnBar: false, cover: true, lockScroll: true, width: 800, height: 460, bgcolor: 'black', cancelBtnTxt: '关闭',
            onCancel: function () {
            }
        });
        dlg.ShowDialog();
    }
</script>
<script type="text/javascript">
    var arrColors = ["#999", "#4cb052", "#e67399", "", "", "#ff9933", "#00cccc"];
    var arrHys = [<%=hysList %>];
    var sTime = 7, eTime = 21, hourSpan = (eTime - sTime + 1), chipHeight = 30;
    var options = {
        startTime: sTime,
        isloading: false,
        CanDrag: false,
        CanNew: true,
        view: "day",
        method: "POST",
        url: "GetMeeting.aspx",
        quickAddHandler: fnQuickAdd,
        EditCmdhandler: Edit,
        quickAddUrl: "CalendarEdit.aspx?act=add", //快速添加日程Post Url 地址
        quickUpdateUrl: "CalendarEdit.aspx?act=update",
        quickDeleteUrl: "CalendarEdit.aspx?act=delete" //快速删除日程的

    };
    var hourWidth = 50; //一个小时的宽度
    var halfWidth = 25; //半个小时的宽度
    var _dragData = {};
    var _dragging = false;
    var _dragTemp = {};
    var _dragEvent;
    var _date = "";
    var _curHys = "";
    function getDate() {
        var d = $("#txtDate").val();
        return d.split(" ")[0];
    }
    jQuery(function () {

        var d = "<%=ShowDate %>";//Date.today().toString("yyyy-M-d dddd");
        $("#txtDate").val(d);
        $("#txtDate").focus(function () {
            WdatePicker({ isShowClear: false, dateFmt: 'yyyy-M-d DD', maxDate: '<%=AheadDate %>', onpicked: loadEvents });
        }).click(function () {
            WdatePicker({ isShowClear: false, dateFmt: 'yyyy-M-d DD', maxDate: '<%=AheadDate %>', onpicked: loadEvents });
        });
        $(".btnLoad").click(function () {
            loadEvents();
        });
        $(".btnPrev").click(function () {
            var dstr = getDate();
            var d = Date.parse(dstr);
            $("#txtDate").val(d.addDays(-1).toString("yyyy-M-d dddd"));
            loadEvents();
        });
        $(".btnNext").click(function () {
            var dstr = getDate();
            var d = Date.parse(dstr);
            $("#txtDate").val(d.addDays(1).toString("yyyy-M-d dddd"));
            loadEvents();
        });
        $("#chkShowCancel").change(function () {
            loadEvents();
        });
        $(".toolPanel input").hover(function () {
            $(this).addClass("btnHover");
        }, function () {
            $(this).removeClass("btnHover");
        });

        _date = getDate();

        //生成框架
        var mainBody = $(".maintbl>tbody");
        var mainHtml = [];
        mainHtml.push("<tr><th style='width:150px;'>会议室名称</th>");
        for (var i = sTime; i <= eTime; i++) {
            mainHtml.push("<th class='hourtd'>", i, ":00</th>");
        }
        mainHtml.push("</tr>");

        for (var i = 0; i < arrHys.length; i++) {
            var arrInfo = ["会 议 室：" + arrHys[i].HysName + "〔" + arrHys[i].HysNum + "人〕 - " + arrHys[i].HysAddr
                , "预定时间：" + arrHys[i].StartTime + " - " + arrHys[i].EndTime
                , "备&nbsp;&nbsp;&nbsp;注：" + arrHys[i].Note
                , "点击名称转到会议室视图 >>"
            ];
            mainHtml.push("<tr><td class='hystd'><a href='ApplyMeeting.aspx?hysId=", arrHys[i].Id, "' title='", arrInfo.join("\r"), "' target='_self'>", arrHys[i].HysName + "&nbsp;<span>(" + arrHys[i].HysNum + "人)</span>", "</a></td>");
            mainHtml.push("<td class='chiptd' colspan='", hourSpan, "'><div class='chipPanel' title='" + arrHys[i].HysName + "' hysName='" + arrHys[i].HysName + "' id='chip" + i + "'></div></td>");
            mainHtml.push("</tr>");
        }

        mainBody.html(mainHtml.join(""));
        $(".chipPanel").mousedown(function (e) {

            var dstr = getDate();
            var d = Date.parse(dstr);
            if (Date.today().compareTo(d) > 0)
                return false;

            if (_dragging)
                return false;
            if ($(e.srcElement).hasClass("item")) {
                if (!options.CanDrag)
                    return false;
                //拖拽会议
                _dragData = {};
                _dragData.target = $(e.srcElement || e.target);
                _dragData.left = _dragData.target.offset().left;
                _dragData.sx = e.pageX;
                _dragData.sy = e.pageY;
                _dragData.tp = $(this).offset().left;
                _dragData.tw = $(this).width();
                _dragData.type = 2;
                _dragData.switchParent = false; //
                $(e.srcElement).css("cursor", "move");
            }
            else {
                if (!options.CanNew)
                    return false;
                //新建会议
                _dragData = {};
                _dragData.target = $(this); // e.srcElement || e.target;
                _dragData.sx = e.pageX;
                _dragData.sy = e.pageY;
                _dragData.ox = e.offsetX;
                _dragData.oy = e.offsetY;
                _dragData.type = 1;
            }
            _dragging = true;

            $('body').noSelect(true);
            return false;
        });

        //加载数据
        loadEvents();

        $(document)
		.mousemove(dragMove)
		.mouseup(dragEnd);
    });

    function loadEvents() {
        $(".chipPanel").empty();
        populate();
    }
    function GetHysIndex(hysName) {
        for (var i = 0; i < arrHys.length; i++) {
            if (arrHys[i].HysName == hysName) {
                return i;
            }
        }
        return -1;
    }
    function render(events) {
        for (var i = 0; i < events.length; i++) {
            var hys = events[i][9];
            var hysIndex = GetHysIndex(hys);
            if (hysIndex > -1) {

                var defStartTime = arrHys[hysIndex].StartTime;
                var defEndTime = arrHys[hysIndex].EndTime;

                var chip = $("#chip" + hysIndex);
                var tp = chip.offset().left;
                var hyName = events[i][1];
                var st = events[i][2];
                var et = events[i][3];

                var dstr = getDate();
                var curDate = Date.parse(dstr);

                var dStart = parseTime(st);
                var arrStart = dStart.toString("H:mm").split(":");
                if (curDate.clone().addHours(sTime).compareTo(dStart) >= 0) {
                    arrStart = [sTime, "00"];
                }

                var curDateEnd = Date.parse(dstr + " " + defEndTime);
                var dEnd = parseTime(et);

                //如果当前天的开始时间大于预定结束时间
                if (Date.parse(dstr + " " + defStartTime).compareTo(dEnd) > 0) {
                    continue;
                }
                var arrEnd = dEnd.toString("H:mm").split(":");
                if (curDateEnd.compareTo(dEnd) < 0) {
                    arrEnd = [eTime + 1, "00"];
                }

                //window.status = arrStart + arrEnd;
                var lpx = tp + gP(parseInt(arrStart[0]), parseInt(arrStart[1]));
                var rpx = tp + gP(parseInt(arrEnd[0]), parseInt(arrEnd[1])) - 1;
                var gh = gH(lpx, rpx, tp);
                var tempdata = GenDragTmpl(gh.sh, gh.sm, gh.eh, gh.em, gh.h);
                var newobj = $(tempdata).attr("title", "会 议：" + hyName + '\r时 间：' + arrStart.join(":") + " - " + arrEnd.join(":")
                    + "\r地 点：" + events[i][9] + "\r电 话：" + (events[i].length > 11 ? events[i][11] : ""));
                if (gh.h > 70) {
                    newobj.html(arrStart.join(":") + " - " + arrEnd.join(":"));
                }
                newobj.css("background-color", arrColors[events[i][7]]);
                chip.append(newobj);

            }
        }
    }

    function parseTime(str) {
        var reg = /Date\([0-9]+\)/gi;
        var matches = str.match(reg);
        if (matches != null) {
            var stime = eval("new " + matches[0]);
            return stime;
        }
    }

    //鼠标移动事件
    function dragMove(e) {
        if (!_dragging)
            return false;

        if (e.pageX < 0 || e.pageY < 0 || e.pageX > document.documentElement.clientWidth || e.pageY >= document.documentElement.clientHeight) {
            dragEnd(e);
            return false;
        }

        var d = _dragData;
        var px = e.pageX;

        if (d.type == 1) {

            var diffx = px - d.sx;
            if (diffx > 11 || diffx < -11 || d.dragobj) {
                if (diffx == 0) { diffx = halfWidth; }
                //求余
                var dx = diffx % halfWidth;
                if (dx != 0) {
                    diffx = dx > 0 ? diffx + halfWidth - dx : diffx - halfWidth - dx;
                    px = d.sx + diffx;
                }

                if (!d.tp) {
                    d.tp = d.target.offset().left;
                }
                //window.status = "diffx=" + diffx + ",d.ox =" + d.ox + ",width=" + d.target.width();

                if (diffx + d.ox > d.target.width() + halfWidth) {
                    //debugger;
                    px = d.tp + d.target.width() + 1;
                }
                else if (px < d.tp) {
                    px = d.tp;
                }

                var gh = gH(d.sx, px, d.tp);
                var ny = gP(gh.sh, gh.sm); //计算给定时间的起始时间left
                if (!d.dragobj) {
                    var tempdata = GenDragTmpl(gh.sh, gh.sm, gh.eh, gh.em, gh.h);
                    d.dragobj = $(tempdata);
                    d.target.append(d.dragobj);
                }
                else {
                    if (d.cgh.sh != gh.sh || d.cgh.eh != gh.eh || d.cgh.sm != gh.sm || d.cgh.em != gh.em) {
                        var txt = [pZero(gh.sh), pZero(gh.sm)].join(":");
                        if (gh.h >= 75) {
                            txt += " - " + [pZero(gh.eh), pZero(gh.em)].join(":");
                        }
                        d.dragobj.css({ "left": (ny - 1) + "px", "width": (gh.h - 1) + "px" }).html(txt);
                    }
                }
                d.cgh = gh;
            }
        }
        else if (d.type == 2) {
            //移动对象
            var diffx = px - d.sx;
            var diffy = e.pageY - d.sy;
            window.status = diffy;
            //如果是上下移动
            if ((diffy > chipHeight || diffy < 0 - chipHeight) && (diffx < halfWidth)) {
                var newP = $(e.target);
                if (newP.hasClass("chipPanel")) {
                    d.target.appendTo(newP);
                    d.switchParent = true;
                }
            }
            else {
                var maxLeft = d.tp + d.tw + 1 - d.target.width();
                if (diffx > 11 || diffx < -11) {
                    if (diffx == 0) { diffx = halfWidth; }
                    var dx = diffx % halfWidth;
                    if (dx != 0) {
                        diffx = dx > 0 ? diffx + halfWidth - dx : diffx - halfWidth - dx;
                        px = d.sx + diffx;
                    }
                    var left = d.left + diffx;
                    if (left > maxLeft) {
                        left = maxLeft + 1;
                    }
                    else if (left <= d.tp) {
                        left = d.tp;
                    }

                    var newRight = left + d.target.width() + 1;

                    var gh = gH(left, newRight, d.tp);
                    var ny = gP(gh.sh, gh.sm); //计算给定时间的起始时间left

                    var txt = [pZero(gh.sh), pZero(gh.sm)].join(":");
                    if (d.target.width() >= 70)
                        txt += " - " + [pZero(gh.eh), pZero(gh.em)].join(":");

                    d.target.css({ "left": (ny - 1) + "px" }).html(txt);

                    window.status = txt;

                }
            }
            return false;
        }
    }

    function dragEnd(e) {
        if (!_dragging)
            return false;
        $('body').noSelect(false);
        _dragging = false;
        var d = _dragData;
        if (d.type == 1) {

            var wrapid = new Date().getTime();
            if (!d.tp) {
                d.tp = d.target.offset().left;
            }
            if (!d.dragobj) {
                window.status = d.sx + hourWidth;
                var px = d.sx;
                var dx = (d.sx - d.tp) % hourWidth;
                if (dx != 0) {
                    px = d.sx - dx;
                }
                var gh = gH(px, px + hourWidth, d.tp);
                var ny = gP(gh.sh, gh.sm);

                var tempdata = GenDragTmpl(gh.sh, gh.sm, gh.eh, gh.em, gh.h);
                d.dragobj = $(tempdata);
                d.target.append(d.dragobj);

                d.cgh = gh;
            }
            var pos = d.dragobj.offset();
            pos.left = pos.left + 30;
            //d.dragobj.attr("tempid", wrapid);
            _date = getDate();
            var start = str2date(_date + " " + d.cgh.sh + ":" + d.cgh.sm);
            var end = str2date(_date + " " + d.cgh.eh + ":" + d.cgh.em);

            //var start = Date.parse(_date + " " + d.cgh.sh + ":" + d.cgh.sm);
            //var end = Date.parse(_date + " " + d.cgh.eh + ":" + d.cgh.em);
            _curHys = d.target.attr("hysName");
            _dragEvent = function () { d.dragobj.remove(); $("#bbit-cal-buddle").css("visibility", "hidden"); };
            quickAdd(start, end, false, pos);
        }
        else if (d.type == 2) {
            d.target.css("cursor", "pointer");
            if (d.switchParent) {
                window.status = "调剂成功！";
            }
            else {
                window.status = "调剂成功！";
            }
        }
    }

    var _dragTmpl = "<div class='item' style='top:${top};left:${left};width:${width};'>${html}</div>";
    //生成临时拖拽对象
    function GenDragTmpl(sh, sm, eh, em, h) {
        var offsetLeft = gP(sh, sm);
        var newTmpl = T(_dragTmpl, {
            starttime: [pZero(sh), pZero(sm)].join(":"),
            endtime: [pZero(eh), pZero(em)].join(":"),
            top: "1px",
            left: (offsetLeft - 1) + "px",
            width: (h) + "px",
            height: "100%",
            html: [pZero(sh), pZero(sm)].join(":")
        });
        return newTmpl;
    }

    function pZero(n) {
        return n < 10 ? "0" + n : "" + n;
    }

    //使用数据和模板生成最终样式
    function T(temp, arrData) {
        return temp.replace(/\$\{([\w]+)\}/g, function (s1, s2) { var s = arrData[s2]; if (typeof (s) != "undefined") { return s; } else { return s1; } });
    }

    //根据时间计算top
    function gP(h, m) {
        return (h - options.startTime) * hourWidth + parseInt(m / 60 * hourWidth);
    }

    //计算起始时间，结束时间级高度差（参数：y1起始top，y2修整后的top，pt事件对象的left）
    function gH(y1, y2, pt) {
        var sy1 = Math.min(y1, y2);
        var sy2 = Math.max(y1, y2);
        var t1 = (sy1 - pt) / hourWidth;
        var t2 = parseInt(t1);
        //t2 = t2 < 0 ? 0 : t2;
        var t3 = t1 - t2 >= 0.5 ? 30 : 0;

        var t4 = (sy2 - pt) / hourWidth;
        var t5 = parseInt(t4);

        //t5 = (t5 + sTime) >= eTime ? (eTime - sTime) : t5;
        var t6 = t4 - t5 >= 0.5 ? 30 : 0;

        return { sh: t2 + sTime, sm: t3, eh: t5 + sTime, em: t6, h: sy2 - sy1 };
    }
</script>

<script type="text/javascript">
    //发起ajax请求
    function populate() {
        if (options.isloading) {
            return true;
        }
        if (options.url && options.url != "") {
            options.isloading = true;
            if (options.onBeforeRequestData && $.isFunction(options.onBeforeRequestData)) {
                options.onBeforeRequestData(1, options);
            }
            var zone = new Date().getTimezoneOffset() / 60 * -1;

            var param = [
                { name: "showdate", value: getDate() },
                { name: "viewtype", value: options.view },
				 { name: "timezone", value: zone },
				 { name: "showCancel", value: $("#chkShowCancel").attr("checked") == "checked" }
            ];
            if (options.extParam) {
                for (var pi = 0; pi < options.extParam.length; pi++) {
                    param[param.length] = options.extParam[pi];
                }
            }

            $.ajax({
                type: options.method, //
                url: options.url,
                data: param,
                dataType: "json",
                //dataFilter: function (data, type) {return data.replace(/"\\\/(Date\([0-9-]+\))\\\/"/gi, "new $1"); },
                success: function (data) {
                    if (data != null && data.error != null) {
                        if (options.onRequestDataError) {
                            options.onRequestDataError(1, data);
                        }
                    }
                    else {
                        responseData(data, data.start, data.end);
                    }
                    if (options.onAfterRequestData && $.isFunction(options.onAfterRequestData)) {
                        options.onAfterRequestData(1);
                    }
                    options.isloading = false;
                },
                error: function (data) {
                    try {
                        debugger;
                        if (options.onRequestDataError) {
                            options.onRequestDataError(1, data);
                        } else {
                            alert("加载数据异常");
                        }
                        if (options.onAfterRequestData && $.isFunction(options.onAfterRequestData)) {
                            options.onAfterRequestData(1);
                        }
                        options.isloading = false;
                    } catch (e) { }
                }
            });
        }
        else {
            alert("没有指定数据加载地址");
        }
    }
    //处理返回数据
    function responseData(data, start, end) {
        var events;
        if (data.issort == false) {
            if (data.events && data.events.length > 0) {
                events = data.sort(function (l, r) { return l[2] > r[2] ? -1 : 1; });
            }
            else {
                events = [];
            }
        }
        else {
            events = data.events;
        }
        render(events);

    }
    function quickAdd(start, end, isallday, pos) {
        if ((!options.quickAddHandler && options.quickAddUrl == "") || options.readonly) {
            return;
        }
        var buddle = $("#bbit-cal-buddle");
        if (buddle.length == 0) {
            var temparr = [];
            temparr.push('<div id="bbit-cal-buddle" style="z-index: 180; width: 400px;visibility:hidden;" class="bubble">');
            temparr.push('<table class="bubble-table" cellSpacing="0" cellPadding="0"><tbody><tr><td class="bubble-cell-side"><div id="tl1" class="bubble-corner"><div class="bubble-sprite bubble-tl"></div></div>');
            temparr.push('<td class="bubble-cell-main"><div class="bubble-top"></div><td class="bubble-cell-side"><div id="tr1" class="bubble-corner"><div class="bubble-sprite bubble-tr"></div></div>  <tr><td class="bubble-mid" colSpan="3"><div style="overflow: hidden" id="bubbleContent1"><div><div></div><div class="cb-root">');
            temparr.push('<table class="cb-table" cellSpacing="0" cellPadding="0"><tbody><tr><th class="cb-key">');
            temparr.push('时  间:</th><td class=cb-value><div id="bbit-cal-buddle-timeshow"></div></td></tr><tr><th class="cb-key">');
            temparr.push('会 议:</th><td class="cb-value"><div class="textbox-fill-wrapper"><div class="textbox-fill-mid"><input id="bbit-cal-what" class="textbox-fill-input"/></div></div><div class="cb-example">');
            temparr.push('例如：每周一工作例会</div></td></tr><tr><th></th><td style="padding-top:5px">');
            temparr.push('<input id="bbit-cal-quickAddBTN" style="cursor:pointer" value="发起会议申请" type="button"/>&nbsp;&nbsp; <SPAN id="bbit-cal-editLink" class="lk"></SPAN>');
            temparr.push('</td></tr></tbody></table><input id="bbit-cal-start" type="hidden"/><input id="bbit-cal-end" type="hidden"/><input id="bbit-cal-allday" type="hidden"/>');
            temparr.push('</div></div></div><tr><td><div id="bl1" class="bubble-corner"><div class="bubble-sprite bubble-bl"></div></div><td><div class="bubble-bottom"></div><td><div id="br1" class="bubble-corner"><div class="bubble-sprite bubble-br"></div></div></tr></tbody></table><div id="bubbleClose1" class="bubble-closebutton"></div><div id="prong2" class="prong"><div class=bubble-sprite></div></div></div>');
            var tempquickAddHanler = temparr.join("");
            temparr = null;
            $(document.body).append(tempquickAddHanler);

            buddle = $("#bbit-cal-buddle");
            var calbutton = $("#bbit-cal-quickAddBTN");
            var lbtn = $("#bbit-cal-editLink");

            var closebtn = $("#bubbleClose1").click(function () {
                $("#bbit-cal-buddle").css("visibility", "hidden");
                realsedragevent();
            });
            calbutton.click(function (e) {
                if (options.isloading) {
                    return false;
                }
                options.isloading = true;
                var what = $("#bbit-cal-what").val();
                var datestart = $("#bbit-cal-start").val();
                var dateend = $("#bbit-cal-end").val();
                var allday = $("#bbit-cal-allday").val();
                var f = /^[^\$\<\>]+$/.test(what);
                if (!f) {
                    alert("请输入会议名称");
                    $("#bbit-cal-what").focus();
                    options.isloading = false;
                    return false;
                }
                var zone = new Date().getTimezoneOffset() / 60 * -1;
                var param = [{ "name": "CalendarTitle", value: what },
						{ "name": "CalendarStartTime", value: datestart },
						{ "name": "CalendarEndTime", value: dateend },
						{ "name": "IsAllDayEvent", value: allday },
						{ "name": "timezone", value: zone }];

                if (options.extParam) {
                    for (var pi = 0; pi < options.extParam.length; pi++) {
                        param[param.length] = options.extParam[pi];
                    }
                }

                if (options.quickAddHandler && $.isFunction(options.quickAddHandler)) {
                    options.quickAddHandler.call(this, param);
                    $("#bbit-cal-buddle").css("visibility", "hidden");
                    //realsedragevent();
                    options.isloading = false;
                }
                else {
                    $("#bbit-cal-buddle").css("visibility", "hidden");
                    var newdata = [];
                    var tId = -1;
                    options.onBeforeRequestData && options.onBeforeRequestData(2);
                    $.post(options.quickAddUrl, param, function (data) {
                        if (data) {
                            if (data.IsSuccess == true) {
                                options.isloading = false;
                                options.eventItems[tId][0] = data.Data;
                                options.eventItems[tId][8] = 1;
                                render();
                                options.onAfterRequestData && options.onAfterRequestData(2);
                            }
                            else {
                                options.onRequestDataError && options.onRequestDataError(2, data);
                                options.isloading = false;
                                options.onAfterRequestData && options.onAfterRequestData(2);
                            }

                        }

                    }, "json");

                    newdata.push(-1, what);
                    var sd = str2date(datestart);
                    var ed = str2date(dateend);
                    var diff = DateDiff("d", sd, ed);
                    newdata.push(sd, ed, allday == "1" ? 1 : 0, diff > 0 ? 1 : 0, 0);
                    newdata.push(-1, 0, "", ""); //主题,权限,参与人，
                    tId = Ind(newdata);
                    realsedragevent();
                    render();
                }
            });
            lbtn.click(function (e) {
                if (!options.EditCmdhandler) {
                    alert("EditCmdhandler未定义");
                }
                else {
                    if (options.EditCmdhandler && $.isFunction(options.EditCmdhandler)) {
                        options.EditCmdhandler.call(this, ['0', $("#bbit-cal-what").val(), $("#bbit-cal-start").val(), $("#bbit-cal-end").val(), $("#bbit-cal-allday").val()]);
                    }
                    $("#bbit-cal-buddle").css("visibility", "hidden");
                    realsedragevent();
                }
                return false;
            });
            buddle.mousedown(function (e) { return false });
        }

        var dateshow = CalDateShow(start, end, !isallday, true);
        var off = getbuddlepos(pos.left, pos.top);
        if (off.hide) {
            $("#prong2").hide()
        }
        else {
            $("#prong2").show()
        }
        $("#bbit-cal-buddle-timeshow").html(dateshow);
        var calwhat = $("#bbit-cal-what").val("");
        $("#bbit-cal-allday").val(isallday ? "1" : "0");
        $("#bbit-cal-start").val(dateFormat.call(start, "yyyy-M-d HH:mm"));
        $("#bbit-cal-end").val(dateFormat.call(end, "yyyy-M-d HH:mm"));
        buddle.css({ "visibility": "visible", left: off.left, top: off.top });
        calwhat.blur().focus(); //add 2010-01-26 blur() fixed chrome 
        $(document).one("mousedown", function () {
            $("#bbit-cal-buddle").css("visibility", "hidden");
            realsedragevent();
        });
        return false;
    }

    function realsedragevent() {
        if (_dragEvent) {
            _dragEvent();
            _dragEvent = null;
        }
    }

    function getbuddlepos(x, y) {
        var tleft = x - 110; //先计算如果是显示箭头
        var ttop = y - 217; //如果要箭头
        var maxLeft = document.documentElement.clientWidth;
        var maxTop = document.documentElement.clientHeight;
        var ishide = false;
        if (tleft <= 0 || ttop <= 0 || tleft + 400 > maxLeft) {
            tleft = x - 200 <= 0 ? 10 : x - 200;
            ttop = y - 159 <= 0 ? 10 : y - 159;
            if (tleft + 400 >= maxLeft) {
                tleft = maxLeft - 410;
            }
            if (ttop + 164 >= maxTop) {
                ttop = maxTop - 165;
            }
            ishide = true;
        }
        return { left: tleft, top: ttop, hide: ishide };
    }

    if ($.fn.noSelect == undefined) {
        $.fn.noSelect = function (p) { //no select plugin by me :-)
            if (p == null)
                prevent = true;
            else
                prevent = p;
            if (prevent) {
                return this.each(function () {
                    if ($.browser.msie || $.browser.safari) $(this).bind('selectstart', function () { return false; });
                    else if ($.browser.mozilla) {
                        $(this).css('MozUserSelect', 'none');
                        $('body').trigger('focus');
                    }
                    else if ($.browser.opera) $(this).bind('mousedown', function () { return false; });
                    else $(this).attr('unselectable', 'on');
                });

            } else {
                return this.each(function () {
                    if ($.browser.msie || $.browser.safari) $(this).unbind('selectstart');
                    else if ($.browser.mozilla) $(this).css('MozUserSelect', 'inherit');
                    else if ($.browser.opera) $(this).unbind('mousedown');
                    else $(this).removeAttr('unselectable', 'on');
                });

            }
        }; //end noSelect
    }
    __WDAY = ["周日", "周一", "周二", "周三", "周四", "周五", "周六"];
    __MonthName = ["一月", "二月", "三月", "四月", "五月", "六月", "七月", "八月", "九月", "十月", "十一月", "十二月"];
    if (!dateFormat || typeof (dateFormat) != "function") {
        var dateFormat = function (format) {
            var o = {
                "M+": this.getMonth() + 1,
                "d+": this.getDate(),
                "h+": this.getHours(),
                "H+": this.getHours(),
                "m+": this.getMinutes(),
                "s+": this.getSeconds(),
                "q+": Math.floor((this.getMonth() + 3) / 3),
                "w": "0123456".indexOf(this.getDay()),
                "W": __WDAY[this.getDay()],
                "L": __MonthName[this.getMonth()] //non-standard
            };
            if (/(y+)/.test(format)) {
                format = format.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
            }
            for (var k in o) {
                if (new RegExp("(" + k + ")").test(format))
                    format = format.replace(RegExp.$1, RegExp.$1.length == 1 ? o[k] : ("00" + o[k]).substr(("" + o[k]).length));
            }
            return format;
        };
    }

    if (!DateDiff || typeof (DateDiff) != "function") {
        var DateDiff = function (interval, d1, d2) {
            switch (interval) {
                case "d": //天
                case "w":
                    d1 = new Date(d1.getFullYear(), d1.getMonth(), d1.getDate());
                    d2 = new Date(d2.getFullYear(), d2.getMonth(), d2.getDate());
                    break;  //w
                case "h":
                    d1 = new Date(d1.getFullYear(), d1.getMonth(), d1.getDate(), d1.getHours());
                    d2 = new Date(d2.getFullYear(), d2.getMonth(), d2.getDate(), d2.getHours());
                    break; //h
                case "n":
                    d1 = new Date(d1.getFullYear(), d1.getMonth(), d1.getDate(), d1.getHours(), d1.getMinutes());
                    d2 = new Date(d2.getFullYear(), d2.getMonth(), d2.getDate(), d2.getHours(), d2.getMinutes());
                    break;
                case "s":
                    d1 = new Date(d1.getFullYear(), d1.getMonth(), d1.getDate(), d1.getHours(), d1.getMinutes(), d1.getSeconds());
                    d2 = new Date(d2.getFullYear(), d2.getMonth(), d2.getDate(), d2.getHours(), d2.getMinutes(), d2.getSeconds());
                    break;
            }
            var t1 = d1.getTime(), t2 = d2.getTime();
            var diff = NaN;
            switch (interval) {
                case "y": diff = d2.getFullYear() - d1.getFullYear(); break; //y
                case "m": diff = (d2.getFullYear() - d1.getFullYear()) * 12 + d2.getMonth() - d1.getMonth(); break;    //m
                case "d": diff = Math.floor(t2 / 86400000) - Math.floor(t1 / 86400000); break;
                case "w": diff = Math.floor((t2 + 345600000) / (604800000)) - Math.floor((t1 + 345600000) / (604800000)); break; //w
                case "h": diff = Math.floor(t2 / 3600000) - Math.floor(t1 / 3600000); break; //h
                case "n": diff = Math.floor(t2 / 60000) - Math.floor(t1 / 60000); break; //
                case "s": diff = Math.floor(t2 / 1000) - Math.floor(t1 / 1000); break; //s
                case "l": diff = t2 - t1; break;
            }
            return diff;

        }
    }

    function str2date(str) {

        var arr = str.split(" ");
        var arr2 = arr[0].split("-");
        var arr3 = arr[1].split(":");

        var y = arr2[0];
        var m = arr2[1].indexOf("0") == 0 ? arr2[1].substr(1, 1) : arr2[1];
        var d = arr2[2].indexOf("0") == 0 ? arr2[2].substr(1, 1) : arr2[2];
        var h = arr3[0].indexOf("0") == 0 ? arr3[0].substr(1, 1) : arr3[0];
        var n = arr3[1].indexOf("0") == 0 ? arr3[1].substr(1, 1) : arr3[1];
        return new Date(y, parseInt(m) - 1, d, h, n);
    }

    function fomartTimeShow(h) {
        return h < 10 ? "0" + h + ":00" : h + ":00";
    }
    function getymformat(date, comparedate, isshowtime, isshowweek, showcompare) {
        var showyear = isshowtime != undefined ? (date.getFullYear() != new Date().getFullYear()) : true;
        var showmonth = true;
        var showday = true;
        var showtime = isshowtime || false;
        var showweek = isshowweek || false;
        if (comparedate) {
            showyear = comparedate.getFullYear() != date.getFullYear();
            //showmonth = comparedate.getFullYear() != date.getFullYear() || date.getMonth() != comparedate.getMonth();
            if (comparedate.getFullYear() == date.getFullYear() &&
					date.getMonth() == comparedate.getMonth() &&
					date.getDate() == comparedate.getDate()
					) {
                showyear = showmonth = showday = showweek = false;
            }
        }

        var a = [];
        if (showyear) {
            a.push("yyyy年M月d日")
        } else if (showmonth) {
            a.push("M月d日")
        } else if (showday) {
            a.push("d日");
        }
        a.push(showweek ? " (W)" : "", showtime ? " HH:mm" : "");
        return a.join("");
    }
    function CalDateShow(startday, endday, isshowtime, isshowweek) {
        if (!endday) {
            return dateFormat.call(startday, getymformat(startday, null, isshowtime));
        } else {
            var strstart = dateFormat.call(startday, getymformat(startday, null, isshowtime, isshowweek));
            var strend = dateFormat.call(endday, getymformat(endday, startday, isshowtime, isshowweek));
            var join = (strend != "" ? " - " : "");
            return [strstart, strend].join(join);
        }
    }
</script>

