<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AppInfo.aspx.cs" Inherits="EIS.Web.SysFolder.AppFrame.AppInfo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>处理提示</title>
    <link href="../../Css/common.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        input {
            padding: 3px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div id="Rpage" class="Rpage-main">
        <div id="Rheader">
        </div>
        <div id="Rbody">
            <div class="title">
                <b class="crl"></b><b class="crr"></b>
                <h1>系统提示</h1>
            </div>
            <div class="content">
                <table width="90%" align="center">

                    <tr>
                        <td rowspan="2" width="80"><img alt="提示" src="../../img/icon_64/<%=ImgName %>" /></td>
                        <td style="padding:20px;line-height:20px;">
                            <%=DealInfo.ToString() %>
                        </td>
                    </tr>
                    <tr>
                        <td style="padding:10px 20px;">
                            <input type="button" value=" 关 闭 " onclick="_appClose();" />
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
