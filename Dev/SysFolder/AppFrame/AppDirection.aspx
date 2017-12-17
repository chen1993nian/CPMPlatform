<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AppDirection.aspx.cs" Inherits="EIS.WebBase.SysFolder.AppFrame.AppDirection" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>填报说明</title>
    <link rel="stylesheet" href="../../css/newsStyle.css" />

</head>
<body>
    <form id="form1" runat="server">
    <div class="cbox">
        <table width="100%">
        <tr>
            <td>
                <div class="ctitle"><%=TblNameCn%>&nbsp;<b style="color:red">[填报要求]</b>
			    <span>更新时间：<%=UpdateTime%></span>
		        </div>

                <div class="ccontent" style="text-align:left;overflow:auto;font-size:14px;line-height:180%;padding-left:30px;">
                <%=Infos %>
                </div>
            </td>
        </tr>
        </table>
    </div>
    </form>
</body>
</html>
