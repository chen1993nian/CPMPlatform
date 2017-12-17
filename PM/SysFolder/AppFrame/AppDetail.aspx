<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AppDetail.aspx.cs" Inherits="EIS.Web.SysFolder.AppFrame.AppDetail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>查看记录</title>
    <meta http-equiv="Pragma" content="no-cache"/>
    <meta content="width=device-width, initial-scale=1.0, maximum-scale=2.0, user-scalable=1;" name="viewport" />
    <link type="text/css" rel="stylesheet" href="../../Css/newsStyle.css"  />
    <link type="text/css" rel="stylesheet" href="../../Css/kandytabs.css"  />
    <link type="text/css" rel="stylesheet" href="../../Css/appInput.css" />
    <link type="text/css" rel="stylesheet" href="../../Css/appDetailPrint.css" media="print" />
    <script type="text/javascript" src="../../js/jquery-1.7.min.js"></script>
    <script type="text/javascript" src="../../DatePicker/WdatePicker.js"></script>
    <script type="text/javascript" src="../../js/kandytabs.pack.js"></script>
    <script type="text/javascript" src="../../js/jquery.jqprint-0.3.js"></script>
    <%=customScript%>
    <style type="text/css">
        .ccomment 
        {
            width:760px;
            display: inline-block;
        }
        .kandyTabs {width:760px;}
        .kandyTabs .tabtitle{text-align:left;}
    </style> 
    <script type="text/javascript">
        var _curClass = new Object();
        jQuery(function () {
            _curClass = EIS.Web.SysFolder.AppFrame.AppInput;
            $("dl").KandyTabs();
            if ("<%=Request["toolbar"] %>" != "") {
                $(".menubar a").hide();
            }
            $(window).resize(function () {
                $("#maindiv").height($(document.body).height() - 75);
            });
            $("#maindiv").height($(document.body).height() - 75);

            if (!!frameElement) {
                if (!!frameElement.lhgDG) {
                    $(frameElement).css("width", "100%");
                }
            }

        });
        function appPrint() {
            $(".normaltbl").jqprint({
                importCSS: true,
                printContainer: true
            });
        }

        function _appClose() {
            if (!!frameElement) {
                if (!!frameElement.lhgDG)
                    frameElement.lhgDG.cancel();
                else
                    window.close();
            }
            else {
                window.close();
            }
        }

        function appWord() {
            var paraStr = "tblName=<%=tblName %>&sIndex=<%=sIndex%>&mainId=<%=mainId %>&condition=<%=condition %>";
            var url = "AppDetailWord.aspx?para=" + _curClass.CryptPara(paraStr).value;
            window.open(url,"_blank");
        }

        var _mainTblName = "<%=tblName %>";
        var _mainId = "<%=mainId %>";
    </script>
    <script type="text/javascript" src="AppDetail.js"></script>
</head>
<body scroll="auto">
    <form id="form1" runat="server">
    <!-- 工具栏 -->
    <div class="menubar NoPrint">
        <div class="topnav">
            <ul>
              <%=appWordBtn %>
              <li><a href="javascript:" onclick="appPrint();" >打印</a></li>
              <li><a href="javascript:" onclick="_appClose();" >关闭</a> </li>
            </ul>
        </div>
    </div>
    
    <div id="maindiv" style="background:white;">
        <div id="divAppTableHtml">
            <%=tblHTML%>
        </div>

    
        <div class="ccomment" style="display:none;">
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
    </form>

    <script type="text/javascript" language='javascript'>
        GetCommentListHTML(0);//显示评论列表
    </script>

</body>
</html>
