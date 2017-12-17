<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DefBizDoc.aspx.cs" Inherits="EIS.Studio.SysFolder.DefFrame.DefBizDoc"  ValidateRequest="false"%>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>业务文档</title>
    <meta http-equiv="Pragma" content="no-cache"/>
    
    <link rel="stylesheet" type="text/css" href="../../css/DefStyle.css"/>
    <link type="text/css" rel="stylesheet" href="../../Editor/kindeditor-4.1.10/themes/default/default.css" />
    
	<link type="text/css" href="../../Css/jquery-ui/lightness/jquery-ui-1.7.2.custom.css" rel="stylesheet" />
    <script type="text/javascript" src="../../js/jquery-1.7.min.js"></script>
    <script type="text/javascript" src="../../Js/jquery.cookie.js"></script>
    <script type="text/javascript" src="../../js/ui.core.js"></script>
    <script type="text/javascript" src="../../js/ui.tabs.js"></script>
    <script type="text/javascript" src="../../js/jquery.notice.js"></script>
    <script type="text/javascript" src="../../Editor/kindeditor-4.1.10/kindeditor-min.js"></script>
    <script type="text/javascript" src="../../Editor/kindeditor-4.1.10/lang/zh_CN.js"></script>
    
    <style type="text/css">
        html,body {
	        height:100%;
        }
        body {
	        margin: 0px;
	        padding: 0px;
            overflow: hidden;
        }
         a {
            text-decoration: none;
        }
        a:hover {
            text-decoration: underline;
        }
        .codetd{width:100%;display:block;position:relative;}
        .WebEditor{display:none;}
	</style>
	<script type="text/javascript">
	    $(function () {
	        var h = $(document.body).height() - 100;
	        var w = $(document.body).width() - 40;
	        KindEditor.create(".WebEditor", { uploadJson: '../../UploadImage.axd', height: (h - 100) + 'px' });
	        $("#tabs").height(h);
	        $("#tabs").tabs({ cookie: true });

	    });
	    function chkform() {
	        //保存HTML编辑器
	        KindEditor.sync('.WebEditor');
	    }
	</script>
</head>
<body>
    <form id="form1" runat="server">

    <div class="menubar">
      <div class="topnav">

        <span style="float:left;margin-left:10px">业务名称：<%=tblname %>&nbsp;&nbsp;   </span>
        <span style="float: right; margin-right: 10px">
        <asp:LinkButton ID="LinkButton1" runat="server" OnClientClick="chkform();" OnClick="LinkButton1_Click" >保存</asp:LinkButton>
        <a href="javascript:" onclick="window.close();">关闭</a>
                        &nbsp;&nbsp;&nbsp;&nbsp; 
            </span>
        </div>
    </div>

<div id="tabs">
	<ul>
		<li><a href="#tabs-1">填报说明</a></li>
		<li><a href="#tabs-2">设计文档</a></li>
	</ul>
    <div id="tabs-1">
        <div class="codetd">
            <asp:TextBox ID="TextBox1" CssClass="WebEditor" runat="server" height="400px" TextMode="MultiLine" Width="98%"></asp:TextBox>
        </div>
    </div>
    <div id="tabs-2">
        <div class="codetd">
            <asp:TextBox ID="TextBox2" CssClass="WebEditor"  runat="server" height="400px" TextMode="MultiLine" Width="98%"></asp:TextBox>
        </div>
    </div>
</div>
    </form>
</body>
</html>
