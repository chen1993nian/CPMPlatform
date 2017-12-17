<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DefQueryStyleFrame.aspx.cs" Inherits="EIS.Studio.SysFolder.DefFrame.DefQueryStyleFrame" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>查询风格设置【<%=Request["fieldname"] %>】</title>
</head>
<frameset cols="230,*" frameborder="no">
	<frame name="left" id="left" src="DefQueryStyleLeft.aspx?fieldid=<%=Request["fieldid"] %>&fieldname=<%=Request["fieldname"] %>&tblname=<%=Request["tblname"] %>">
	<frame name="main" id="main" src="../../Welcome.htm">
	<noframes>
	</noframes>
</frameset>
</html>
