<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AppReportRun.aspx.cs" Inherits="EIS.Web.SysFolder.AppFrame.AppReportRun" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
	<script type="text/javascript" src="../../js/jquery-1.8.0.min.js"></script>
    <script>
        jQuery(function () {
            setTimeout(function () {
                var url = $("#HiddenField1").val();
                window.open(url, "_self");
            }, 1000);
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <img src="../../Img/common/loading2.gif" />
        <asp:HiddenField ID="HiddenField1" runat="server" />
    </div>
    </form>
</body>
</html>
