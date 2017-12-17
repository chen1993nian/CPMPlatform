<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RelationRightLeft.aspx.cs" Inherits="EIS.WorkAsp.RelationTree.RelationRightLeft" validateRequest="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title><%=RelationName%></title>
    <script type="text/javascript">
        var WorkAreaShow = "1";
        var initCols = "";
        window.onload = init;
        function init() {
            initCols = rlr.cols;
            //alert(initCols);
        }
        function showhiddeWorkArea() {
            if (WorkAreaShow == "1") {
                WorkAreaShow = "0";
                //rlr.cols = "26,*";
                rlr.cols = "3%,*";
            }
            else {
                WorkAreaShow = "1";
                rlr.cols = initCols;
            }
        }

        function showWorkArea() {
            WorkAreaShow = "1";
            rlr.cols = initCols;
        }

        function hiddeWorkArea() {
            WorkAreaShow = "0";
            rlr.cols = "3%,*";
        }
    </script>
</head>

<frameset id="rlr" rows="<%=RelationUIWidth %>">
	<frame name="RelationContents" target="RelationMain" src="<%=url1 %>&dir=r">
	<frame name="RelationMain" src="<%=url2 %>">
	<noframes>
	<body>

	<p>此网页使用了框架，但您的浏览器不支持框架。</p>

	</body>
	</noframes>
</frameset>

</html>
