<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MsgRead.aspx.cs" Inherits="EIS.Web.WorkAsp.Msg.MsgRead" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>消息详情</title>
    <link href="../../Css/appInput.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
    td{
        padding:7px;
        text-align:left;
        line-height:20px;
    }
    .linkbtn{
        font-size:13px;
        font-weight:bold;
        border:1px solid white;
        line-height:20px;
        text-decoration:none;
        }
    .linkbtn:hover{
        border:1px solid blue;
        background-color:#ccc;
        }
        body{
            background-color:#f5f5f5;overflow:auto;
        }
        .label{font-weight:bold;color:darkcyan}
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div id="maindiv">
        <table width="560" style="border-collapse:collapse;border:1px solid gray;margin-left:auto;margin-right:auto;" align="center" border="1">
            <tr>
                <td><span class="label">发送人：</span><%=model.Sender %> &nbsp;
                <span class="label">发送时间：</span><%=model.SendTime %></td>
            </tr>
            <tr>
                <td >
                    <%=model.Content.Replace("\r\n","<br/>") %> &nbsp;
                    <%=msgUrl %>                  
                </td>
            </tr>
            <tr>
                <td ><span class="label">接收人：</span><span><%=total %></span>
                <span class="green"><%=ReadedList %></span>
                <span class="red"><%=UnReadList%></span>
                </td>
            </tr>
            <tr>
                <td >
                <span class="label">附件：</span>
                <%=fileList %>
                <!--
                <iframe frameborder="0" width="500" height="40" src="../../SysFolder/Common/FileListFrame.aspx?appName=T_E_App_MsgInfo&appId=<%=msgId %>&read=1"></iframe>
                -->
                </td>
            </tr>
            <tr>
                <td style="text-align:right">
                    <a class="linkbtn" href="MsgSend.aspx?act=1&msgId=<%=msgId %>" target="_self">[回复]</a>&nbsp;
                    <a class="linkbtn"  href="MsgSend.aspx?act=2&msgId=<%=msgId %>" target="_self">[转发]</a>&nbsp;
                    <a class="linkbtn"  href="javascript:window.close();" target="_self">[关闭]</a>
                    &nbsp;&nbsp;&nbsp;&nbsp;
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
