<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="EIS.Studio.Error" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>程序异常</title>
    <link href="<%=Page.ResolveUrl("~/Css/common.css")%>" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript" src="<%=Page.ResolveUrl("~/js/jquery-1.4.2.min.js")%>"></script>
    <style type="text/css">
        input[type=button],input[type=submit]
        {
            padding:3px 18px 3px 18px;
	        margin:1px 1px 1px 0px;
	        width: auto;
	        line-height:16px;
	        height:28px;
            color:#333;
            overflow:visible; 
        }
        .detail{
            line-height:150%;
            width:560px;
            overflow:scroll;
            border:1px dotted gray;
            background-color:#F9FB91;
            padding:5px;
            display:none;
        }
    </style>
    <script type="text/javascript">
        jQuery(function () {
            jQuery("#btnDetail").click(function () {
                jQuery(".detail").toggle();
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="Rpage" class="Rpage-main">
        <div id="Rheader">
        </div>
        <div id="Rbody">
            <div class="title">
                <b class="crl"></b><b class="crr"></b>
                <h1>程序异常</h1>
            </div>
            <div class="content">
                <table width="90%" align="center">

                    <tr>
                        <td rowspan="2" width="80"><div class="icon64_error"></div></td>
                        <td style="padding:20px;line-height:20px;">
                            <%=ErrorMsg %>
                        </td>
                    </tr>
                    <tr>
                        <td style="padding:10px 20px;">
                            <input type="button" id="btnDetail" value="详细信息"/>
                            <input type="button" value="关 闭" onclick="_appClose();" />
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>
                            <div class="detail">
                            <%=DetailMsg%>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="bottom">
                <b class="crl"></b><b class="crr"></b>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
<script type="text/javascript">
    function _appClose() {
        if (!!frameElement) {
            if (!!frameElement.lhgDG)
                frameElement.lhgDG.cancel();
            else
                window.close();
        }
        else {
            window.close();
        }
    }

</script>