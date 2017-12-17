<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ImportDataTJJGZB2DemoStart.aspx.cs" Inherits="EIS.Web.ImportDataTJJGZB.WorkAsp.RelationTree.ImportDataTJJGZB2DemoStart" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>对应字段</title>
</head>
<frameset id="rlr" cols="20%,*">
	<frame name="RelationContentsL" target="ViewDataWin1" src="ImportDataTJJGZB2DemoStart1.aspx?<%=Request.QueryString %>">
    <frameset id="rlr" rows="50%,*">
    	<frame name="ViewDataWin1" src="">
	    <frame name="ViewDataWin2" src="">
    </frameset>
	<noframes>
	<body>

	<p>此网页使用了框架，但您的浏览器不支持框架。</p>

	</body>
	</noframes>
</frameset>
</html>
