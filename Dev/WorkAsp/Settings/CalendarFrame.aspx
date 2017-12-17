<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CalendarFrame.aspx.cs" Inherits="EIS.WebBase.WorkAsp.Settings.CalendarFrame1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>工作日历设置</title>
</head>
<frameset cols="260,*" frameborder="no">
	<frame name="calTree" id="left" src="CalendarTree.aspx">
	<frame name="calMain" id="main" src="../../Welcome.htm">
	<noframes>
	</noframes>
</frameset>

</html>
