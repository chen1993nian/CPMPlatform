<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SetCalendar.aspx.cs" Inherits="EIS.WebBase.WorkAsp.Settings.SetCalendar" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>工作日定义</title>
    <link href="../../Css/calendar.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../../js/jquery.js"></script>
    <script type="text/javascript" src="../../js/jquery-1.4.2.min.js"></script>
    <script type="text/javascript" src="../../js/lhgdialog.min.js?s=default,chrome,"></script>
    <script src="../../DatePicker/WdatePicker.js" type="text/javascript"></script>
    <style type="text/css">
        input[type=button],input[type=submit]{font-size:12px;padding:3px;}
        th,td{border-color:Gray;}
        .Wdate{width:120px;}
        .tip{line-height:18px;color:#666;margin-top:0px;margin-bottom:0px;border-width:0px;background-color:transparent;}
        .holiday{text-align:center;font-size:14px;font-weight:bold;line-height:3;color:#ff4500;}
        .hidden{display:none;}
		td a{text-decoration:none;font-weight:bold;font-size:14px;font-family:"微软雅黑" , "宋体" , Consolas, Verdana;}
		td a:hover{color:red;text-decoration:underline;}
        .btnHelp {
            width: 68px;
            background-color: #9cf78b;
            border: 1px solid gray;
        }
    </style>
    <script type="text/javascript">
        function UpdateCalendar(calName) {
            var arr = ["<%=calendarId %>", calName];
            window.parent.frames["left"].changeNode(arr);
        }
        function InitWork() {
            var strLink;
            strLink = "InitCalendar.aspx?calendarId=<%=calendarId %>";
            window.open(strLink, "newWin", "toolbar=no,resizable=yes,scrollbars=yes,width=460,height=320,top=200,left=400");
        }
        function editCalendar() {
            var strLink;
            if ("<%=calendarId %>" == "")
                return;
            strLink = "CalendarEdit.aspx?calendarId=<%=calendarId %>";
            window.open(strLink, "newWin", "toolbar=no,resizable=yes,scrollbars=yes,width=560,height=320,top=200,left=400");
        }
        function updateDay(calId, day) {

            var strLink = "EditCalendar.aspx?calId=" + calId + "&day=" + day;
            window.open(strLink, "newWin", "toolbar=no,resizable=yes,scrollbars=yes,width=460,height=300,top=200,left=400");
        }
        $(function () {
            if ("<%=calendarId %>" == "")
                $(".btnEdit").hide();
            $(".Wdate").focus(function () {
                WdatePicker({ dateFmt: 'yyyy年MM月', onpicked: loadEvents });
            }).click(function () {
                WdatePicker({ dateFmt: 'yyyy年MM月', onpicked: loadEvents });
            });

            $(".btnHelp").click(function () {
                var url = $(this).attr("data-helpUrl");
                app_showHelp(url, "工作日历帮助");
            });
        });
            function loadEvents() {
                $("#Button1").click();
            }



            function app_showHelp(urlStr, titleStr) {
                if ((titleStr == undefined) || (titleStr == "")) titleStr = "帮助";
                var win = new $.dialog({
                    id: 'HelpWin'
                    , cover: true
                    , maxBtn: true
                    , minBtn: true
                    , btnBar: true
                    , lockScroll: false
                    , title: titleStr
                    , autoSize: false
                    , width: 1150
                    , height: 600
                    , resize: true
                    , bgcolor: 'black'
                    , iconTitle: false
                    , page: urlStr
                });
                win.ShowDialog();
            }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="maindiv">
        <table width="840" align="center">
            <tr>
                <td>
                		<table border="0" cellspacing="0" cellpadding="0" width="100%" height="30">
							<tr>
								<td width="80">&nbsp;选择月份：</td>
								<td width="140">
                                    <asp:TextBox CssClass="Wdate" ID="txtQueryDate" runat="server"></asp:TextBox></td>
								<td >
                                    <asp:Button ID="Button1" CssClass="hidden" runat="server" Text="查询工作日" onclick="Button1_Click" />
                                    &nbsp;
                                    <input class="input_btn btnEdit" onclick="editCalendar();" value="编辑日历信息" type="button"/>
                                    &nbsp;
                                    <input class="input_btn" onclick="InitWork();" value="批量设置工作日" type="button"/>
                                    &nbsp;
                                    <input type="button" value="帮助" id="btnHelp" class="btnHelp" data-helpUrl="Help/HelpSetCalendar.aspx" />
                                    </td>
								<td width="160">&nbsp;
                                
                                    <img style="vertical-align:middle" border="0" src="../../img/common/reddot.gif" />休息日&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
									<img style="vertical-align:middle" border="0" src="../../img/common/greendot.gif" />工作日
                                </td>

							</tr>
						</table>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:calendar id="Calendar1" runat="server" BorderWidth="1px" BorderColor="gray" ShowGridLines="True"
							Font-Size="Small" BackColor="LightBlue" BorderStyle="solid" NextPrevFormat="ShortMonth" 
                        ForeColor="highlight" Font-Names="Verdana"
							OnDayRender="Calendar1_DayRender" CellPadding="5"  Width="100%" 
                        ondatabinding="Calendar1_DataBinding" oninit="Calendar1_Init" 
                        onvisiblemonthchanged="Calendar1_VisibleMonthChanged">
							<TodayDayStyle ForeColor="Red" BackColor="#FFFF99"></TodayDayStyle>
							<SelectorStyle Font-Size="Small" Font-Names="宋体" ForeColor="Blue"></SelectorStyle>
							<DayStyle Font-Size="12px" Font-Names="宋体" HorizontalAlign="Left" 
                                VerticalAlign="Top" BackColor="#FFFBF7" Height="50px"></DayStyle>
							<NextPrevStyle Font-Size="12px" Font-Names="宋体" Font-Bold="True" ForeColor="#666666"></NextPrevStyle>
							<DayHeaderStyle Font-Size="12px" Font-Bold="True" Height="20px" 
                                ForeColor="#333333" CssClass="rl-week"
								BackColor="#33CCCC"></DayHeaderStyle>
							<SelectedDayStyle ForeColor="White" BackColor="#009999"></SelectedDayStyle>
							<TitleStyle Font-Size="14px" Font-Names="微软雅黑,宋体" Font-Bold="True" Height="30px" ForeColor="#666666"
								BackColor="Transparent"></TitleStyle>
							<OtherMonthDayStyle CssClass="rl-blank"></OtherMonthDayStyle>
						    <WeekendDayStyle BackColor="#EDEDF3" />
						</asp:calendar>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
