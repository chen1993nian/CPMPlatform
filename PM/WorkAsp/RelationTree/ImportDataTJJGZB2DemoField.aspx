<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ImportDataTJJGZB2DemoField.aspx.cs" Inherits="EIS.Web.ImportDataTJJGZB.ImportDataTJJGZB2DemoField" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>对应字段</title>
</head>
<frameset id="rlr" cols="20%,*,20%">
	<frame name="RelationContentsL" target="RelationMain" src="ImportDataTJJGZB2DemoField1.aspx?<%=Request.QueryString %>">
    <frameset id="rlr" rows="*,10%">
    	<frame name="RelationMain" src="<%=app_url %>">
	    <frame name="RelationMainTB" src="">
    </frameset>
	<frame name="RelationContentsR" target="RelationMain" src="ImportDataTJJGZB2DemoField2.aspx?<%=Request.QueryString %>">
	<noframes>
	<body>

	<p>此网页使用了框架，但您的浏览器不支持框架。</p>

	</body>
	</noframes>
</frameset>
</html>
