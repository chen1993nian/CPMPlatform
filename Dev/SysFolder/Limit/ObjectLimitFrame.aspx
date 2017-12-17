<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ObjectLimitFrame.aspx.cs" Inherits="EIS.Studio.SysFolder.Limit.ObjectLimitFrame" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>权限设置</title>
</head>
<frameset rows="40,*" frameborder="0">
	<frame name="limittop" id="limittop" src="ObjectLimitTop.aspx?funId=<%=Request["funId"] %>" noresize="noresize" />
	<frame name="limitmain" id="limitmain" src="../../Welcome.htm"/>
</frameset>
</html>
