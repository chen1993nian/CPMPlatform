<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ModifyCalendar.aspx.cs" Inherits="EIS.Web.WorkAsp.Calendar.ModifyCalendar" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
 
<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <meta http-equiv="X-UA-Compatible" content="IE=7" />
    <title>修改日程详细</title>
    <link href="../../Theme/Default/main.css" rel="stylesheet" type="text/css" />  
    <link href="../../Theme/Default/dp.css" rel="stylesheet" />
    <link href="../../Theme/Default/dropdown.css" rel="stylesheet" />
    <link href="../../Theme/Default/colorselect.css" rel="stylesheet" />
    <script src="../../js/Calendar/jquery.min.js" type="text/javascript"></script>
    <script src="../../js/Calendar/Common.js" type="text/javascript"></script>   
    <script src="../../js/Calendar/jquery.form.js" type="text/javascript"></script>
    <script src="../../js/Calendar/jquery.validate.js" type="text/javascript"></script>
    <script src="../../js/Calendar/jquery.dropdown.js" type="text/javascript"></script>
    <script src="../../js/Calendar/jquery.colorselect.js" type="text/javascript"></script>
    <script src="../../DatePicker/WdatePicker.js" type="text/javascript" ></script>
     <style type="text/css">
     .calpick
     {
        width:25px;
        border:none;
        cursor:pointer;
        background:url("../../Theme/Default/images/icons/cal.gif") no-repeat center 2px;
        margin-left:-22px;
     }
     #divleftcalendarcolor{width:16px;}
     #calendarcolor{width:24px;}
     .error{color:Red;    
            border:dotted 1px orange;
            background:#F9FB91;}
     .hidden{display:none;}
     .imgbtn{float:right;}
     </style>
     <script type="text/javascript">
         function success() {
             var data <%=msg %>;
            if (data.IsSuccess) {
                CloseModelWindow(null, true);
            }
            else {
                alert(data.Msg);
            }
        }
     </script>
</head>
<body>
    <div>
      <div class="toolBotton">
            <a id="Closebtn" class="imgbtn" href="javascript:void(0);">
                <span class="Close" title="关闭窗口" >关闭</span>
               <a id="Deletebtn" class="imgbtn" href="javascript:void(0);">
                    <span class="Delete" title="取消该日程">删除</span>
                </a>

           <a id="Savebtn" class="imgbtn" href="javascript:void(0);">
                <span class="Save"  title="保存">保存</span>
           </a>
           

            

            </a>
        </div>        
         <div style="clear: both">
         </div>
        <div class="infocontainer" style="padding-left:20px;">
            <form runat="server" class="fform" id="fmEdit">
                <asp:Button CssClass="hidden" ID="Button1" runat="server" Text="Button" 
                    OnClick="Button1_Click" />

                <div class="row">
                    <span style="color:Red;">
                        主题：
                    </span>
                    <asp:TextBox class="required safe" Width="500" ID="Subject" MaxLength="200" runat="server"></asp:TextBox>
                    
                </div>
                <div class="row">
                    <span style="color:Red;">
                        类型：
                    </span>
                    <asp:RadioButtonList ID="rdCategory" runat="server" RepeatLayout="Table" RepeatDirection="Horizontal">
                    </asp:RadioButtonList>
                    <asp:HiddenField ID="colorvalue" runat="server" />                    
                </div>
                 <div class="row">
                    <span style="color:Red;">
                       时间：
                    </span>
                    <div>
                    <asp:TextBox class="required safe Wdate" ID="stpartdate" MaxLength="200" runat="server"></asp:TextBox>
                    <asp:TextBox class="required safe Wdate" ID="etpartdate" MaxLength="200" runat="server"></asp:TextBox>
                      <asp:CheckBox ID="IsAllDayEvent" runat="server" /><label for="IsAllDayEvent" style="display:inline;cursor:hand;">全天日程</label>
                    </div>
                </div>
                 <div class="row">
                    <span>
                        地点：
                    </span>
                    <asp:TextBox class="required safe" ID="Location" Width="500" MaxLength="200" runat="server"></asp:TextBox>

                </div>
                 <div class="row">
                    <span>
                        备注：
                    </span>
                    <asp:TextBox TextMode="MultiLine"  cols="100" rows="4" Width="500"  class="required safe" ID="Description" MaxLength="200" runat="server"></asp:TextBox>

                </div>
                <input id="timezone" name="timezone" type="hidden" value="" />
                <br/>
                <div class="row error" style="display:none;">
                
                </div>
                <br/>
           </form>
 
         </div>


    <script type="text/javascript">
        $(function () {
            var arrT = [];
            var tt = "{0}:{1}";
            for (var i = 0; i < 24; i++) {
                arrT.push({ text: StrFormat(tt, [i >= 10 ? i : "0" + i, "00"]) }, { text: StrFormat(tt, [i >= 10 ? i : "0" + i, "30"]) });
            }
            $("#timezone").val(new Date().getTimezoneOffset() / 60 * -1);

            var check = $("#IsAllDayEvent").click(function (e) {
                if (this.checked) {
                    var v = $("#stpartdate").val();
                    $("#stpartdate").val(v.split(' ')[0]);
                    $("#etpartdate").val("").hide();
                }
                else {
                    var v = $("#stpartdate").val();
                    $("#stpartdate").val(v.split(' ')[0] + " 00:00");
                    $("#etpartdate").show();
                }
            });
            if (check[0].checked) {
                $("#etpartdate").val("").hide();
            }

            $("#Savebtn").click(function () {
                var error = "";
                if ($("#Subject").val() == "") {
                    error = "主题不能为空！";
                }
                else if ($("#stpartdate").val() == "") {
                    error = "开始时间不能为空！";
                }
                else {
                    if (!check[0].checked && $("#etpartdate").val() == "") {
                        error = "结束时间不能为空！";
                    }
                }
                if (error == "") {
                    $("#Button1").click();
                }
                else {

                    $(".error").show().html(error);
                }

            });
            $("#Closebtn").click(function () { CloseModelWindow(); });
            $("#Deletebtn").click(function () {
                if (confirm("你确定要取消该日程吗？")) {
                    var param = [{ "name": "calendarId", value: 5 }];
                    $.post("CalendarEdit.aspx?act=delete",
                        param,
                        function (data) {
                            if (data.IsSuccess) {
                                CloseModelWindow(null, true);
                            }
                            else {
                                $(".error").show().html("操作失败：\r\n" + data.Msg);
                            }
                        }
                    , "json");
                }
            });

            $("#stpartdate,#etpartdate").attr("readonly", true).focus(function () {
                if (check[0].checked) {
                    WdatePicker({ isShowClear: true, dateFmt: 'yyyy-MM-dd' });
                }
                else {
                    WdatePicker({ isShowClear: true, dateFmt: 'yyyy-MM-dd HH:mm' });
                }
            });

            var cv = $("#colorvalue").val();
            if (cv == "") {
                cv = "-1";
            }

            //$("#calendarcolor").colorselect({ title: "颜色分类", index: cv, hiddenid: "colorvalue" });

        });


    </script>
    </div>
</body>
</html>
 

