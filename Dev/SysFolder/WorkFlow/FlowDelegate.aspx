<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FlowDelegate.aspx.cs" Inherits="EIS.WebBase.SysFolder.WorkFlow.FlowDelegate" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>任务委托</title>
    <link rel="stylesheet" type="text/css" href="../../Css/wfStyle.css" />
    <script type="text/javascript" src="../../js/jquery-1.7.min.js"></script>

        <style type="text/css">
        
        input[type=submit],button{
            padding:3px;
            }
       .info{font-size:1.2em;font-weight:bolder;color:red;padding:10px 0px;line-height:1.5em;}       
    </style>
    <script type="text/javascript">
        $(document).ready(function () {
            $(".Read").attr("readonly", true);

            jQuery("#btnLeader").click(function () {
                openpage('../Common/UserTree.aspx?method=2&queryfield=empid,empname&cid=txtLeaderId,txtLeader');
            });
        });
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

        function openpage(url) {
            openCenter(url, "_blank", 640, 500);
        }
    </script>  
</head>
<body>
    <form id="form1" runat="server">
    <div id="maindiv" style="width:440px;">
        <br />
        <div>
            <table class='normaltbl' style="width:440px;" border="1" cellpadding="3" align="center">
            <tr>
            <td  width="100">请选择代理人：</td>
            <td  >
                <asp:TextBox ID="txtLeader" Width="260" CssClass="TextBoxInChar Read" runat="server"></asp:TextBox>
                &nbsp;
                <input type="button" id="btnLeader" value="选择" />
                <asp:HiddenField ID="txtLeaderId" runat="server" />
            </td>
            </tr>
            </table>
        </div>
        <br />
        <div style="text-align:left;">
        <h5 style="padding:5px;font-size:12px;">附言&nbsp;&nbsp;<span style="color:Red;font-weight:normal;">(附言会以系统消息形式提醒他)</span></h5>
           <asp:TextBox Rows="3" TextMode="MultiLine"  CssClass="TextBoxInArea"  Width="98%" ID="txtReason" runat="server"></asp:TextBox>
           <%=TipMsg%>
           <div style="padding:5px;">
            <asp:Button ID="Button1" CssClass="clear" runat="server" Text=" 提 交 " onclick="Button1_Click" />
            &nbsp;
            <button value=" 关 闭 " onclick="javascript:window.close();" >&nbsp;关 闭&nbsp;</button>
            </div>
        </div>

    </div>
    </form>
</body>
</html>
