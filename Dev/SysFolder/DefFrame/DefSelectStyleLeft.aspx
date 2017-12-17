<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DefSelectStyleLeft.aspx.cs" Inherits="EIS.Studio.SysFolder.DefFrame.DefSelectStyleLeft" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>显示风格设置</title>
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
        var _curClass = EIS.Studio.SysFolder.DefFrame.DefSelectStyleLeft;

        var userAgent = window.navigator.userAgent.toLowerCase();
        $.browser.msie8 = $.browser.msie && /msie 8\.0/i.test(userAgent);
        $.browser.msie7 = $.browser.msie && /msie 7\.0/i.test(userAgent);
        $.browser.msie6 = !$.browser.msie8 && !$.browser.msie7 && $.browser.msie && /msie 6\.0/i.test(userAgent);
        var treedata = [
        { id: "", text: "默认样式", value: "=:默认样式:" },
        {
            id: "00", text: "单行文本框", value: "", "isexpand": true, "checkstate": 0, "complete": true, "hasChildren": true, "ChildNodes": [
               { id: "000", text: "自动编号", value: "inc/AutoNumber.aspx?titleName=自动编号", "isexpand": true, "checkstate": 0, "complete": true, "hasChildren": false, "ChildNodes": null },
               { id: "001", text: "日期时间", value: "inc/DateDropDown.aspx?titleName=日期时间格式", "isexpand": true, "checkstate": 0, "complete": true, "hasChildren": false, "ChildNodes": null },
               { id: "002", text: "密码显示", value: "=002:密码显示:", "isexpand": true, "checkstate": 0, "complete": true, "hasChildren": false, "ChildNodes": null },
               { id: "003", text: "子表序号", value: "=003:子表序号:", "isexpand": true, "checkstate": 0, "complete": true, "hasChildren": false, "ChildNodes": null },
               { id: "004", text: "计算表达式", value: "inc/Input_Expression.aspx?titleName=计算表达式&fieldid=<%=fieldId %>", "isexpand": true, "checkstate": 0, "complete": true, "hasChildren": false, "ChildNodes": null },
            { id: "005", text: "金额大写", value: "inc/Input_DX.aspx?titleName=金额大写", "isexpand": true, "checkstate": 0, "complete": true, "hasChildren": false, "ChildNodes": null }
        ]
        },
        {
            id: "01", text: "下拉列表框", value: "", "isexpand": true, "checkstate": 0, "complete": true, "hasChildren": true, "ChildNodes": [
               { id: "010", text: "年份下拉", value: "=010:年份下拉:", "isexpand": true, "checkstate": 0, "complete": true, "hasChildren": false, "ChildNodes": null },
               { id: "011", text: "月份下拉", value: "=011:月份下拉:", "isexpand": true, "checkstate": 0, "complete": true, "hasChildren": false, "ChildNodes": null },
               { id: "012", text: "字典库", value: "inc/Dictionary.aspx?titleName=下拉字典库", "isexpand": true, "checkstate": 0, "complete": true, "hasChildren": false, "ChildNodes": null },
               { id: "013", text: "自定义值", value: "inc/SingleInput.aspx?titleName=自定义值下拉", "isexpand": true, "checkstate": 0, "complete": true, "hasChildren": false, "ChildNodes": null },
               { id: "014", text: "SQL语句", value: "inc/DropListInput.aspx?titleName=SQL语句下拉", "isexpand": true, "checkstate": 0, "complete": true, "hasChildren": false, "ChildNodes": null }
               //{id : "015" , text : "下拉树型",value : "inc/TreeList.aspx?titleName=下拉树型","isexpand":true,"checkstate":0,"complete":true,"hasChildren":false,"ChildNodes":null}
            ]
        },
        {
            id: "02", text: "多行文本框", value: "", "isexpand": true, "checkstate": 0, "complete": true, "hasChildren": true, "ChildNodes": [
               { id: "020", text: "普通文本框", value: "inc/Input_TextArea.aspx?titleName=普通多行文本框", "isexpand": true, "checkstate": 0, "complete": true, "hasChildren": false, "ChildNodes": null },
               { id: "021", text: "HTML编辑器", value: "inc/Input_TextArea.aspx?titleName=HTML编辑器", "isexpand": true, "checkstate": 0, "complete": true, "hasChildren": false, "ChildNodes": null },
               { id: "022", text: "WebOffice", value: "inc/WebOfficeProp.aspx?titleName=WebOffice", "isexpand": true, "checkstate": 0, "complete": true, "hasChildren": false, "ChildNodes": null },
               { id: "023", text: "多附件上传", value: "inc/MultiAttachProp.aspx?titleName=多附件上传", "isexpand": true, "checkstate": 0, "complete": true, "hasChildren": false, "ChildNodes": null },
               { id: "024", text: "单图片上传", value: "inc/SingleImageProp.aspx?titleName=单图片上传", "isexpand": true, "checkstate": 0, "complete": true, "hasChildren": false, "ChildNodes": null }
            ]
        },
        {
            id: "03", text: "弹出页面显示", value: "", "isexpand": true, "checkstate": 0, "complete": true, "hasChildren": true, "ChildNodes": [
               { id: "030", text: "自定义页面", value: "inc/Input_Path.aspx?titleName=用户自定义页面路径", "isexpand": true, "checkstate": 0, "complete": true, "hasChildren": false, "ChildNodes": null },
               { id: "032", text: "弹出查询列表", value: "inc/Input_OutList_Relation.aspx?titleName=弹出查询列表&fieldid=<%=fieldId %>", "isexpand": true, "checkstate": 0, "complete": true, "hasChildren": false, "ChildNodes": null },
            //{id : "031" , text : "弹出树型字典",value : "","isexpand":true,"checkstate":0,"complete":true,"hasChildren":false,"ChildNodes":null},
            { id: "033", text: "弹出用户树", value: "inc/UserTree_Relation.aspx?titleName=弹出用户树&fieldid=<%=fieldId %>", "isexpand": true, "checkstate": 0, "complete": true, "hasChildren": false, "ChildNodes": null },
            { id: "034", text: "弹出岗位树", value: "inc/PositionTree_Relation.aspx?titleName=弹出岗位树&fieldid=<%=fieldId %>", "isexpand": true, "checkstate": 0, "complete": true, "hasChildren": false, "ChildNodes": null },
            { id: "035", text: "弹出组织机构", value: "inc/DeptTree_Relation.aspx?titleName=弹出组织机构&fieldid=<%=fieldId %>", "isexpand": true, "checkstate": 0, "complete": true, "hasChildren": false, "ChildNodes": null },
            { id: "036", text: "弹出组织机构-单选", value: "inc/DeptTree_Relation.aspx?titleName=弹出组织机构-单选&fieldid=<%=fieldId %>", "isexpand": true, "checkstate": 0, "complete": true, "hasChildren": false, "ChildNodes": null }

        ]
        },
        {
            id: "04", text: "Radio显示", value: "", "isexpand": true, "checkstate": 0, "complete": true, "hasChildren": true, "ChildNodes": [
              { id: "040", text: "字典库", value: "inc/Radio_Dict.aspx?titleName=Radio字典库", "isexpand": true, "checkstate": 0, "complete": true, "hasChildren": false, "ChildNodes": null },
              { id: "041", text: "自定义值", value: "inc/Radio_Vals.aspx?titleName=Radio自定义值", "isexpand": true, "checkstate": 0, "complete": true, "hasChildren": false, "ChildNodes": null },
              { id: "042", text: "SQL语句", value: "inc/Radio_SQL.aspx?titleName=RadioSQL语句", "isexpand": true, "checkstate": 0, "complete": true, "hasChildren": false, "ChildNodes": null }
            ]
        },
        {
            id: "05", text: "CheckBox显示", value: "", "isexpand": true, "checkstate": 0, "complete": true, "hasChildren": true, "ChildNodes": [
              { id: "050", text: "字典库", value: "inc/CheckBox_Dict.aspx?titleName=CheckBox字典库", "isexpand": true, "checkstate": 0, "complete": true, "hasChildren": false, "ChildNodes": null },
              { id: "051", text: "自定义值", value: "inc/CheckBox_Vals.aspx?titleName=CheckBox自定义值", "isexpand": true, "checkstate": 0, "complete": true, "hasChildren": false, "ChildNodes": null },
              { id: "052", text: "SQL语句", value: "inc/CheckBox_SQL.aspx?titleName=CheckBoxSQL语句", "isexpand": true, "checkstate": 0, "complete": true, "hasChildren": false, "ChildNodes": null }
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

                        //parent.frameElement.lhgDG.curWin.styleCallBack(item.value.substr(1));
                        //window.parent.close();
                    }
                    else {
                        //window.open(item.value + "&time=" + d.getMilliseconds() + "&key=" + item.id + "&tblname=<%=tblName %>", "main");
                        var para = "tblname=<%=tblName %>";
                        para = _curClass.CryptPara(para).value;
                        window.parent.frames["main"].location = item.value + "&time=" + d.getMilliseconds() + "&key=" + item.id + "&para=" + para;
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

