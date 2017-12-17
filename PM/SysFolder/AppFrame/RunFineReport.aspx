<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RunFineReport.aspx.cs" Inherits="EIS.Web.FineReport.SysFolder.AppFrame.RunFineReport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>执行报表</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:HiddenField ID="FineReportServer" runat="server" Value="http://www.baidu.com:8080/WebReport/ReportServer" />
    </div>
    </form>
</body>
</html>
