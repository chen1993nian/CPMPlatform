<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AppDirectionList.aspx.cs" Inherits="EIS.Web.SysFolder.AppFrame.AppDirectionList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>设计说明</title>
    <link rel="stylesheet" href="../../css/newsStyle.css" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="cbox">
        <table width="100%">
        <tr>
            <td>
                <div class="ctitle"><%=TblNameCn%>&nbsp;<b style="color:red">[设计说明]</b>
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
