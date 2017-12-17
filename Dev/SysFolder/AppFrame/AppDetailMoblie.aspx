<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AppDetailMoblie.aspx.cs" Inherits="EIS.WebBase.SysFolder.AppFrame.AppDetailMoblie" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>查看记录</title>
    <meta content="width=device-width, initial-scale=1.0, maximum-scale=2.0, user-scalable=1;" name="viewport" />
    <meta http-equiv="Pragma" content="no-cache"/>
    <link type="text/css" rel="stylesheet" href="../../Css/newsStyle.css"  />
    <link type="text/css" rel="stylesheet" href="../../Css/kandytabs.css"  />
    <link type="text/css" rel="stylesheet" href="../../Css/appInput.css" />
    <link type="text/css" rel="stylesheet" href="../../Editor/skins/default.css" />
    <script type="text/javascript" src="../../js/jquery-1.7.min.js"></script>
    <script type="text/javascript" src="../../DatePicker/WdatePicker.js"></script>
    <script type="text/javascript" src="../../js/kandytabs.pack.js"></script>
    <script type="text/javascript" src="../../Editor/kindeditor.js"></script>
    <style type="text/css" media="print"> 
	    .NoPrint{display:none;} 
	    .PageNext{page-break-after: always;} 
    </style>
    <style type="text/css">
        .kandyTabs{width:760px;}
        .kandyTabs .tabtitle{text-align:left;}
#maindiv
{
margin-top: 0px;
padding: 0px 0px 20px 0px;
}
table.normaltbl, table.redtbl, table.subtbl {
width: 95%;
}	
.cwrite textarea {
width: 90%;
}

.ccomment {
margin-top: 20px;
margin-right: 5px;
margin-left: 5px;
}

.menubar {

display: none;

}



    </style> 
    <script type="text/javascript">
        jQuery(function () {
            $("dl").KandyTabs();
            $(window).resize(function () {
                $("#maindiv").height($(document.body).height() - 75);
            });
            $("#maindiv").height($(document.body).height() - 75);
        });
        function appPrint() {
            document.getElementById("WebBrowser").ExecWB(7, 1);
        }


        var _mainTblName = "<%=tblName %>";
        var _mainId = "<%=mainId %>";

    </script>
    <script type="text/javascript" src="AppDetail.js"></script>

 

</head>
<body scroll="auto">
    <form id="form1" runat="server">
    <OBJECT id="WebBrowser" classid="CLSID:8856F961-340A-11D0-A96B-00C04FD705A2" height="0" width="0" ></OBJECT> 
    <!-- 工具栏 -->
    <div class="menubar NoPrint">
        <div class="topnav">
            <ul>
                <li><a href="javascript:" onclick="appPrint();" >打印</a></li>
                <li><a href="javascript:" onclick="window.close();" >关闭</a> </li>
            </ul>
        </div>
    </div>
    
    <div id="maindiv" style="background:white;">
    <br />
    <% =tblHTML%>
    
        <div class="ccomment">
            <h3>相关评论</h3>
            <div id="div_commentlist">
            </div>
            <div class="cwrite">
                <h4>我来说两句</h4>
                <textarea  id="txtCommentContent" ></textarea>
                <div class="cwrite_btns">
                <input type="button" value="提 交" class="h_submit" id="savecomment" onclick="GetCommentListHTML(1);" />
                <input type="reset" value="清 空" class="h_submit" />
                </div>
            </div>
        </div>
    </div>



    <script type="text/javascript" language='javascript'>
        GetCommentListHTML(0);//显示评论列表
    </script>

    </form>
</body>
</html>
