<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SurveyFrame.aspx.cs" Inherits="EIS.Web.WorkAsp.Survey.SurveyFrame" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>调查问卷题目设置</title>
</head>
<frameset cols="400,*" frameborder="no">
	<frame name="left" id="left" src="SurveyLeft.aspx?mainId=<%=Request["editid"] %>">
	<frame name="main" id="main" src="../../Welcome.htm">
	<noframes>
	</noframes>
</frameset>
</html>
