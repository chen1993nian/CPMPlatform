<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ImportDataTJJGZB2Demo.aspx.cs" Inherits="EIS.Web.ImportDataTJJGZB.ImportDataTJJGZB2Demo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>选择表单</title>
    <script type="text/javascript" language="javascript">
        function setTable1(s,t) {
            RelationMain.setTable1(s,t);
        }
        function setTable2(s,t) {
            RelationMain.setTable2(s,t);
        }
    </script>
</head>
<frameset id="rlr" cols="40%,*,40%">
	<frame name="RelationContentsL" target="RelationMain" src="../../SysFolder/AppFrame/AppQuery.aspx?tblname=Q_Demo_TableInfo">
	<frame name="RelationMain" src="ImportDataTJJGZB2DemoTables.aspx">
	<frame name="RelationContentsR" target="RelationMain" src="../../SysFolder/AppFrame/AppQuery.aspx?tblname=Q_TJJGZB_TableInfo">
	<noframes>
	<body>

	<p>此网页使用了框架，但您的浏览器不支持框架。</p>

	</body>
	</noframes>
</frameset>
</html>
