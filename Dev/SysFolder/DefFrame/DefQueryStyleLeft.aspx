<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DefQueryStyleLeft.aspx.cs" Inherits="EIS.Studio.SysFolder.DefFrame.DefQueryStyleLeft" %>


<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>查询风格设置</title>
    <meta http-equiv="Pragma" content="no-cache"/>
     <link href="../../css/appstyle.css" rel="stylesheet" type="text/css" />    
     <link href="../../css/tree.css" rel="stylesheet" type="text/css" />
     <style type="text/css">
        body{overflow:hidden;}
     </style>    
	</head>
	<body >
		<form id="Form1" method="post" runat="server">
        <div style="border: #c3daf9 1px solid; width:200px; height: 560px; overflow: auto;margin:10px 0px 0 10px;background-color:White">
            <div id="tree">
            </div>
        </div>
    <script src="../../js/jquery-1.4.2.min.js" type="text/javascript"></script>
    <script src="../../js/jquery.tree.js" type="text/javascript"></script>
   
    <script type="text/javascript">
        var userAgent = window.navigator.userAgent.toLowerCase();
        $.browser.msie8 = $.browser.msie && /msie 8\.0/i.test(userAgent);
        $.browser.msie7 = $.browser.msie && /msie 7\.0/i.test(userAgent);
        $.browser.msie6 = !$.browser.msie8 && !$.browser.msie7 && $.browser.msie && /msie 6\.0/i.test(userAgent);
        var treedata = [
        { id: "", text: "默认样式", value: "=:默认样式:" },
        {
            id: "01", text: "下拉列表框", value: "", "isexpand": true, "checkstate": 0, "complete": true, "hasChildren": true, "ChildNodes": [
              { id: "001", text: "日期时间", value: "inc/DateDropDown.aspx?titleName=日期时间格式", "isexpand": true, "checkstate": 0, "complete": true, "hasChildren": false, "ChildNodes": null },
              { id: "010", text: "年份下拉", value: "=010:年份下拉:", "isexpand": true, "checkstate": 0, "complete": true, "hasChildren": false, "ChildNodes": null },
              { id: "011", text: "月份下拉", value: "=011:月份下拉:", "isexpand": true, "checkstate": 0, "complete": true, "hasChildren": false, "ChildNodes": null },
              { id: "012", text: "字典库", value: "inc/Dictionary.aspx?titleName=下拉字典库", "isexpand": true, "checkstate": 0, "complete": true, "hasChildren": false, "ChildNodes": null },
              { id: "013", text: "自定义值", value: "inc/SingleInput.aspx?titleName=自定义值下拉", "isexpand": true, "checkstate": 0, "complete": true, "hasChildren": false, "ChildNodes": null },
              { id: "014", text: "SQL语句", value: "inc/SingleInput.aspx?titleName=SQL语句下拉", "isexpand": true, "checkstate": 0, "complete": true, "hasChildren": false, "ChildNodes": null }
            ]
        },
        {
            id: "04", text: "Radio显示", value: "", "isexpand": true, "checkstate": 0, "complete": true, "hasChildren": true, "ChildNodes": [
              { id: "040", text: "字典库", value: "inc/Dictionary.aspx?titleName=Radio字典库", "isexpand": true, "checkstate": 0, "complete": true, "hasChildren": false, "ChildNodes": null },
              { id: "041", text: "自定义值", value: "inc/SingleInput.aspx?titleName=Radio自定义值", "isexpand": true, "checkstate": 0, "complete": true, "hasChildren": false, "ChildNodes": null },
              { id: "042", text: "SQL语句", value: "inc/SingleInput.aspx?titleName=RadioSQL语句", "isexpand": true, "checkstate": 0, "complete": true, "hasChildren": false, "ChildNodes": null }
            ]
        }
        ];
        function load() {
            var o = {
                showcheck: false,
                onnodeclick: function (item) {
                    var d = new Date();
                    if (item.value) {
                        if (item.value.substr(0, 1) == "=") {
                            parent.frameElement.lhgDG.curWin["styleCallBack"](item.value.substr(1));
                            parent.frameElement.lhgDG.cancel();

                            //window.parent.opener.styleCallBack(item.value.substr(1));
                            //window.parent.close();
                        }
                        else {
                            window.parent.frames["main"].location = item.value + "&time=" + d.getMilliseconds() + "&key=" + item.id + "&tblname=<%=tblName %>"
                        }
                    }

                },
                blankpath: "../../Img/common/",
                cbiconpath: "../../Img/common/"
            };
            o.data = treedata;
            $("#tree").treeview(o);
            var code = getOpenerValue("stylecode");
            var codeArr = ["002", "003", "010", "011"];
            if (code != "" && $.inArray(code, codeArr) == -1) {
                $("#tree_" + code).click();
            }


        }
        if ($.browser.msie6) {
            load();
        }
        else {
            $(document).ready(load);
        }
        function getOpenerValue(ctlName) {
            return parent.frameElement.lhgDG.curWin.document.getElementById(ctlName).value;
        }
    </script>
		</form>
	</body>
</html>
