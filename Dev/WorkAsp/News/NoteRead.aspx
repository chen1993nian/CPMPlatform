<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NoteRead.aspx.cs" Inherits="EIS.Web.WorkAsp.News.NoteRead" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>通知公告详细信息</title>
    <link rel="stylesheet" href="../../css/newsStyle.css" />
    <script type="text/javascript" src="../../js/jquery-1.7.min.js"></script>
     <script type="text/javascript">
         $(function () {
             $("#viewlink").click(function () {
                 $(".readdiv").toggle();
             });
         });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="cbox">
        <table>
        <tr>
            <td>
                <div class="ctitle"><%=newstitle %></div>

                <div class="ccontent">
                <%=newscontent %>
                </div>
            </td>
        </tr>
        <tr>
            <td>
            <br/>
                <div style="border-top:1px #999 dashed;padding-top:20px;">
                <span>相关附件：</span><br/>
                <%=fjlist %>
                </div>
	    </td>
	</tr><tr><td>
                <div style="border-top:1px #999 dashed;padding-top:20px;clear:both;">
                    <span class="label">接收情况：</span><span><%=total %></span>&nbsp;&nbsp;<a id='viewlink' href="javascript:"><b>[查看详情]</b></a>
		     <div class="readdiv" style="display:none;">
                    <div class="green"><%=ReadedList %></div>
                    <div class="red"><%=UnReadList%></div>
                     </div>
                </div>
		</td></tr>
	<tr><td>
                <div class="ccomment">
                <h3>相关评论</h3>

                <%=commentlist %>
                    <div class="cwrite">
                        <h4>我来说两句</h4>
                        <asp:TextBox ID="TextBox1" runat="server" TextMode="MultiLine"></asp:TextBox>
                        <div class="cwrite_btns">
                        <input type="submit" value="提 交" class="h_submit" id="Submit1" onserverclick="Submit1_ServerClick" runat="server"/>
                        <input type="reset" value="清 空" class="h_submit" />&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:TextBox ID="TextBox2" runat="server" style="display:none"></asp:TextBox>&nbsp;&nbsp;</div>
                    </div>
                </div>
            </td>
        </tr>
        </table>
    </div>
    </form>
</body>
</html>
