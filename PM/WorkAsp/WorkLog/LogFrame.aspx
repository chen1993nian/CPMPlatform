<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LogFrame.aspx.cs" Inherits="EIS.Web.WorkAsp.WorkLog.LogFrame" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Frameset//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-frameset.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>工作日志</title>
</head>
<frameset cols="260,*" frameborder="no">
	<frame name="logleft" id="logleft" src="LogLeft.aspx"/>
	<frame name="logmain" id="logmain" src="LogRight.aspx"/>
</frameset>
</html>

