<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProjectDataLimitMain.aspx.cs" Inherits="EIS.WorkAsp.DataLimit.ProjectDataLimitMain" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>协同管理授权</title>
<script  type="text/javascript" language="javascript">
    //LimitType = 0人员权限；1角色权限；2岗位权限；3部门权限；
    </script>
</head>
<frameset rows="44,*">
	<frame name="DataLimitBanner" scrolling="no" target="DataLimitContents" src="ProjectDataLimitMainTop.aspx">
	<frameset>
		<frame name="DataLimitContents" target="deptLimitMain" src="<%=LimitUrl %>">
	</frameset>
	<noframes>
	<body>

	<p>此网页使用了框架，但您的浏览器不支持框架。</p>

	</body>
	</noframes>
</frameset>
</html>
