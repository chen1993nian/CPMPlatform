<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="IntegratedPageMain.aspx.cs"
    Inherits="EIS.WorkAsp.IntegratedPage.IntegratedPageMain" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>综合页面</title>
</head>
<frameset rows="40,*">
	<frame name="RelationBanner" scrolling="no" target="RelationContents" src="IntegratedPageMainTop.aspx?<%=Request.QueryString%>">
	<frameset cols="250,*">
		<frame name="RelationContents" target="RelationMain" src="IntegratedPageMainLeft.aspx?<%=Request.QueryString%>">
		<frame name="RelationMain" src="<%=initUrl %>">
	</frameset>
	<noframes>
	<body>

	<p>此网页使用了框架，但您的浏览器不支持框架。</p>

	</body>
	</noframes>
</frameset>
</html>
