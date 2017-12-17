<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FlowShareTo.aspx.cs" Inherits="EIS.WebBase.SysFolder.WorkFlow.FlowShareTo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>任务共享</title>
    <link rel="stylesheet" type="text/css" href="../../Css/wfStyle.css" />
    <script type="text/javascript" src="../../js/jquery-1.7.min.js"></script>

        <style type="text/css">
        #txtLeader{margin-top:3px;}
       .info{font-size:1.2em;font-weight:bolder;color:red;padding:10px 0px;line-height:1.5em;}
    </style>
        <script type="text/javascript">
            $(document).ready(function () {
                $(".Read").attr("readonly", true);

                jQuery("#btnLeader").click(function () {
                    openpage('../Common/UserTree.aspx?method=1&queryfield=empid,empname&cid=txtLeaderId,txtLeader');
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
    <div id="maindiv" style="width:500px;">
            <br />
            <h3 style="font-size:12pt;margin:5px;"><%=curInstance.InstanceName %></h3>
			&nbsp;
            <table class='normaltbl' style="width:500px;" border="1" cellpadding="3" align="center">
            <tr>
            <td  width="100">请选择共享人：</td>
            <td style="padding:5px;">
                <asp:TextBox ID="txtLeader" Width="280"  CssClass="TextBoxInChar" runat="server"></asp:TextBox>
				&nbsp;
                <input type="button" id="btnLeader" value="选择" />
                <asp:HiddenField ID="txtLeaderId" runat="server" />
            </td>
            </tr>
            </table>
        <br />
        <div style="text-align:left;position:relative;">
        	<h5 style="padding:5px;font-size:1em;">给共享人的附言：(以系统消息形式通知共享人)</h5>
           <asp:TextBox Rows="3" TextMode="MultiLine"  CssClass="TextBoxInArea"  Width="98%" ID="txtReason" runat="server"></asp:TextBox>

           <%=TipMsg%>
           <div style="clear:both;height:40px;line-height:40px;">
            <asp:Button ID="Button1" CssClass="clear" runat="server" Text="提 交" onclick="Button1_Click" />
			&nbsp;&nbsp;
            <button type="button" onclick="javascript:window.history.back();" >返回任务</button>
            
            <button type="button"  onclick="javascript:window.close();" >关闭窗口</button>
            </div>
        </div>

    </div>
    </form>
</body>
</html>

