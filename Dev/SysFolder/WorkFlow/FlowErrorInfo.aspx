<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FlowErrorInfo.aspx.cs" Inherits="EIS.WebBase.SysFolder.WorkFlow.FlowErrorInfo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>任务处理提示</title>
    <link href="../../Css/common.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        input {
            padding: 3px;
        }
        .ErrorPanel{font-size:12px;border:1px solid gray;height:200px;overflow:scroll;padding:5px 5px 5px 10px;}
        .appTbl{
	        border-collapse: collapse;
            border:1px solid gray;            
            }
         .appTbl td{border:1px solid gray;padding:2px 5px 2px 5px;}
    </style>
    <script type="text/javascript">
        function toggleMore() {
            var el = document.getElementById("moreTr");
            if (el.style.display == "none") {
                el.style.display = "";
            }
            else {
                el.style.display = "none";
            }
        }
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
                <h1>系统提示</h1>
            </div>
            <div class="content">
                <table width="90%" align="center">
                    <tr>
                        <td width="80"><img alt="提示" src="../../img/icon_64/icon64_info.png" /></td>
                        <td style="padding:20px;line-height:18px;">
                            <div class="TaskName"><h4 style="color:#4677bf"><%=curInstance.InstanceName%></h4></div>
                            <%=DealInfo%>
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td style="padding:10px 20px;">
                            <input type="button" value=" 返 回 " onclick="window.history.back();"/>&nbsp;
                            <input type="button" value=" 更 多 " onclick="toggleMore();"/>&nbsp;
                            <input type="button" value=" 关 闭 " onclick="window.close();"/>
                        </td>
                    </tr>
                    <tr id="moreTr" style="display:none;">
                        <td>&nbsp;</td>
                        <td style="padding:20px;line-height:18px;">
                            <%=moreInfo %>                            
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
