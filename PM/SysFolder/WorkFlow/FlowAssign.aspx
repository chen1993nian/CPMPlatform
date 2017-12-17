<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FlowAssign.aspx.cs" Inherits="EIS.Web.SysFolder.WorkFlow.FlowAssign" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>任务加签</title>
    <link rel="stylesheet" type="text/css" href="../../Css/wfStyle.css" />
    <script type="text/javascript" src="../../js/jquery-1.7.min.js"></script>

        <style type="text/css">
        
        input[type=submit],input[type=button],button{
            padding:3px;
            }
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
    <div id="maindiv" style="width:440px;">
            <br />
            <div style="font-size:12pt;padding:10px;font-weight:bold;text-align:center;"><%=curInstance.InstanceName %></div>
            <table class='normaltbl' style="width:100%;" border="1" cellpadding="3" align="center">
            <tr>
            <td  width="100">请选择加签人：</td>
            <td style="padding:5px;">
                <asp:TextBox ID="txtLeader" Width="260"  CssClass="TextBoxInChar" runat="server"></asp:TextBox>
                <input type="button" id="btnLeader" value="选 择" />
                <asp:HiddenField ID="txtLeaderId" runat="server" />
            </td>
            </tr>
            </table>
        <br />
		<div style="padding:5px;text-align:left;">
			<span style="font-weight:bold;">写给加签人的附言</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue">通知方式:</span>
			<asp:CheckBox ID="CheckBox1" runat="server" Checked="true"  Text="系统消息"  Enabled="false" />
			<asp:CheckBox ID="CheckBox2" runat="server" Checked="false" Text="电子邮件" />
		</div>
        <div style="text-align:left;">
           <asp:TextBox Rows="3" TextMode="MultiLine"  CssClass="TextBoxInArea"  Width="98%" ID="txtReason" runat="server"></asp:TextBox>

           <%=TipMsg%>
           <div style="padding:5px;">
            <asp:Button ID="Button1" CssClass="clear" runat="server" Text=" 提 交 " onclick="Button1_Click" />
            &nbsp;
            <button value="关 闭" onclick="javascript:window.close();" >&nbsp;关 闭&nbsp;</button>
            </div>
        </div>

    </div>
    </form>
</body>
</html>

