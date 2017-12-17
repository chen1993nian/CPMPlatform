<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewsRead.aspx.cs" Inherits="EIS.Web.WorkAsp.News.NewsRead" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title><%=newsTitle%></title>
    <link rel="stylesheet" href="../../css/newsStyle.css" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="cbox">
        <table>
        <tr>
            <td>
                <div class="ctitle"><%=newsHeader%></div>

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
