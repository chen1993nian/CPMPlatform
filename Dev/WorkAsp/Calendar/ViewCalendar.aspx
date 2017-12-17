<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewCalendar.aspx.cs" Inherits="EIS.Web.WorkAsp.Calendar.ViewCalendar" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
 
<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <meta http-equiv="X-UA-Compatible" content="IE=7" />
    <title>查看日程详细</title>
    <link href="../../Theme/Default/main.css" rel="stylesheet" type="text/css" />  
    <script src="../../js/Calendar/jquery.min.js" type="text/javascript"></script>
    <script src="../../js/Calendar/Common.js" type="text/javascript"></script>   
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
     .hidden{display:none;}
     </style>
</head>
<body>
    <div>
      <div class="toolBotton">
            
            <a id="Closebtn" class="imgbtn"  style="float:right;" href="javascript:void(0);">
                <span class="Close" title="关闭窗口" >关闭</span>
            </a>
        </div>        
         <div style="clear: both">
         </div>
        <div class="infocontainer">
            <form runat="server" class="fform" id="fmEdit">
                <div class="row">
                    <span style="color:Red;">
                        主题：
                    </span>
                    <asp:TextBox class="required safe" Width="500" ID="Subject" MaxLength="200" runat="server"></asp:TextBox>
                    <asp:HiddenField ID="colorvalue" runat="server" />
                    
                </div>
                <div class="row">
                    <span style="color:Red;">
                        类型：
                    </span>
                    <asp:RadioButtonList ID="rdCategory" runat="server" RepeatLayout="Table" RepeatDirection="Horizontal">
                    </asp:RadioButtonList>
                </div>
                 <div class="row">
                    <span style="color:Red;">
                       时间：
                    </span>
                    <div>
                    <asp:TextBox class="required safe Wdate" ID="stpartdate" MaxLength="200" runat="server"></asp:TextBox>
                    <asp:TextBox class="required safe Wdate" ID="etpartdate" MaxLength="200" runat="server"></asp:TextBox>
                      <asp:CheckBox ID="IsAllDayEvent" runat="server" />
                        全天日程
                    </div>
                </div>
                 <div class="row">
                    <span style="color:Red;">
                        地点：
                    </span>
                    <asp:TextBox class="required safe" ID="Location" Width="500" MaxLength="200" runat="server"></asp:TextBox>

                </div>
                 <div class="row">
                    <span style="color:Red;">
                        备注：
                    </span>
                    <asp:TextBox TextMode="MultiLine"  cols="100" rows="4" Width="500"  class="required safe" ID="Description" MaxLength="200" runat="server"></asp:TextBox>

                </div>
                <input id="timezone" name="timezone" type="hidden" value="" />
                <br/>
                <br/>
           </form>
 
         </div>


    <script type="text/javascript">
        $("#Closebtn").click(function () { CloseModelWindow(); });

    </script>
    </div>
</body>
</html>
 

