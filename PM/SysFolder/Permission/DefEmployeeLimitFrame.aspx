<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DefEmployeeLimitFrame.aspx.cs" Inherits="EIS.Web.SysFolder.Permission.DefEmployeeLimitFrame" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>组织部门定义</title>
</head>
<frameset cols="300,*" frameborder="no">
	<frame name="left" id="left" src="DefEmployeeLimitLeft.aspx?webId=<%=base.GetParaValue("webId") %>">
	<frame name="main" id="main" src="../../Welcome.htm">
	<noframes>
	</noframes>
</frameset>
</html>
