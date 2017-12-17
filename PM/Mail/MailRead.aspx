<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MailRead.aspx.cs" Inherits="EIS.Web.Mail.MailRead" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>阅读邮件</title>
    <meta http-equiv="Pragma" content="no-cache" />
    <link href="../Css/Email.css" rel="stylesheet" type="text/css" />
    <link href="../Css/EmailQQ.css" rel="stylesheet" type="text/css" />
    <script src="../Js/jquery-1.4.2.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            var _curClass = EIS.Web.Mail.MailRead;
            $(".btn_goback").click(function () {
                returnBack();
            });
            $("#btnTransfer").click(function () {
                window.open("MailWrite.aspx?act=2&mailId=<%=MailId %>", "_self");
            });
            $("#btnReplay").click(function () {
                window.open("MailWrite.aspx?act=1&mailId=<%=MailId %>", "_self");
            });
            $("#quick_del").click(function () {
                delmail(1);
            });
            $("#quick_completelydel").click(function () {
                delmail(2);
            });
            function delmail(flag) {
                if (!confirm("您确认删除当前邮件吗")) {
                    return;
                }
                var ret = "";
                if (flag == 1) {
                    ret = _curClass.DeleteMail("<%=MailId %>");
                }
                else {
                    ret = _curClass.RemoveMail("<%=MailId %>");
                }
                if (ret.error) {
                    alert("删除邮件时出错：" + ret.error.Message);
                }
                else {
                    alert("邮件已经成功删除");
                    var p = $("#prevmail").attr("disabled");
                    var n = $("#nextmail").attr("disabled");
                    if (!p) {
                        $("#prevmail")[0].click();
                    }
                    else if (!n) {
                        $("#nextmail")[0].click();
                    }
                    else {
                        returnBack();
                    }
                }

            }

            function returnBack() {
                var folderId = "<%=FolderId %>";
                if (folderId == "send") {
                    window.open("MailSend.aspx", "_self");
                }
                else if (folderId == "trash") {
                    window.open("MailTrash.aspx", "_self");
                }
                else {
                    window.open("MailReceive.aspx?folderId=<%=FolderId%>", "_self");
                }
        }
        });
    </script>
</head>
<body style="background: #fff; color: #000; font-size: 12px; font-weight: normal;
    font-family: 'lucida Grande',Verdana; padding: 2px 7px 6px 4px; margin: 0;">
    <!--  工具栏  -->
    <div class="toolbg toolbgline toolheight">
        <div id="nextmail_top" class="qm_right">
            <%=NextPrevBtn.ToString() %>
        </div>
        <div class="nowrap qm_left">
            <input class="qm_btn wd1 btn_true nowrap btn_goback" value="«&nbsp;返回" type="button" />
            <input class="btn_sepline" type="button" />
            <input class="qm_btn wd1" value=" 回复 " type="button" name="btnReplay" id="btnReplay"/>
            <input class="qm_btn wd1" value=" 转发 " type="button" id="btnTransfer" />
            <input id="quick_del" class="qm_btn wd1" value="删除" type="button" name="del" />
            <input id="quick_completelydel" class="qm_btn wd2" value="彻底删除" type="button" name="perdel"/>&nbsp;
            <div style="margin: 2px 0px 0px 1px; float: left" id="moreoprcontainer" name="moreoprcontainer">
            </div>
        </div>
        <div class="clr">
        </div>
    </div>
    <!--  工具栏  -->
    <!--  邮件主体  -->
    <div class="readmailinfo">
        <span id="subjecttip"></span>
        <table style="height: 24px" border="0" cellspacing="0" cellpadding="0">
            <tbody>
                <tr>
                    <td style="padding-bottom: 2px; padding-left: 14px; padding-right: 8px; word-break: break-all;
                        padding-top: 9px" class="txt_left settingtable readmail_subject" height="24"
                        valign="center">
                        <div style="padding-bottom: 3px" class="qm_left">
                            <span id="subject" class="sub_title ">
                                <%=mailModel.Subject %></span>
                        </div>
                        <div class="clr">
                        </div>
                    </td>
                    <td style="text-align: right; padding-bottom: 0px; padding-left: 0px; padding-right: 12px;
                        padding-top: 5px" id="senderinfo2" class="f_size settingtable" width="1%" nowrap>
                        <div style="text-align: left; width: 45px">
                            &nbsp;
                        </div>
                    </td>
                </tr>
            </tbody>
        </table>
        <table border="0" cellspacing="0" cellpadding="0">
            <tbody>
                <tr>
                    <td style="padding-left: 14px" class="settingtable txt_left">
                        <span class="addrtitle">发件人：</span> 
                        <span>
                        <%=mailModel.SenderName%>
                        </span>
                    </td>
                    <td style="padding-right: 12px" class="settingtable" width="1%" nowrap>
                    </td>
                </tr>
            </tbody>
        </table>
        <table border="0" cellspacing="0" cellpadding="0">
            <tbody>
                <tr>
                    <td style="padding-bottom: 0px; line-height: 19px; padding-left: 14px; padding-right: 12px;
                        word-break: break-all; padding-top: 2px" class="settingtable txt_left" width="99%">
                        <span class="addrtitle">时&nbsp;&nbsp;&nbsp;间：</span>
                        <b class="tcolor"><%=mailModel._CreateTime.ToString("yyyy年MM月dd日 HH:mm (dddd)") %></b>
                    </td>
                </tr>
                <tr>
                    <td style="padding-bottom: 0px; line-height: 19px; padding-left: 14px; padding-right: 0px;
                        padding-top: 0px" class="settingtable txt_left">
                        <div>
                            <div style="position: absolute" class="addrtitle nowrap">
                                收件人：&nbsp;
                            </div>
                            <div style="padding-left: 48px; zoom: 1; font-size: 12px; overflow: hidden">
                                <%=mailModel.Receivers %>
                            </div>
                        </div>
                        <div style="margin-top: 1px">
                            <div style="position: absolute" class="addrtitle nowrap">
                                附&nbsp;&nbsp;&nbsp;件：
                            </div>
                            <div style="padding-left: 48px; height: 20px; font-size: 12px; overflow: hidden;">
                                <span class="pointer">
                                    <%=mailAttatches %>
                                </span>
                            </div>
                        </div>
                    </td>
                </tr>
            </tbody>
        </table>
        <div class="txt_left">
            <div id="spam" name="spam">
            </div>
        </div>
    </div>
    <!--  邮件主体  -->
    <form id="form1" runat="server">
    <div class="" id="contentDiv" style="padding-bottom: 0px; line-height: 170%; min-height: 100px;
        padding-left: 15px; padding-right: 15px; height: auto; font-size: 14px; overflow: visible;
        padding-top: 15px; _height: 100px;" onclick="">
        <div id="mailContentContainer" style="padding-bottom: 0px; min-height: auto; padding-left: 0px;
            padding-right: 0px; font-family: 'lucida Grande',Verdana; height: auto; font-size: 14px;
            margin-right: 170px; padding-top: 0px;">
            <%=BodyHtml %>
        </div>
    </div>
    </form>
</body>
</html>
