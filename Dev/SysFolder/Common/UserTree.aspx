<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserTree.aspx.cs" Inherits="EIS.WebBase.SysFolder.Common.UserTree" %>



<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>选择用户</title>
	<meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
    <link rel="stylesheet" type="text/css" href="../../css/appstyle.css"/>    
    <link rel="stylesheet" type="text/css" href="../../css/tree.css"/>
     <style type="text/css">
     	#tree
	    {
	        border:#c3daf9 1px solid;
	        padding:5px;
	        margin:5px;
			height:300px;
			overflow:auto;
	    }
		.centerZone
		{
			border:#c3daf9 1px solid;
	        padding:5px;
	        margin-top:5px;
			height:300px;
			overflow:auto;
			background:white;
		}
		.radioSpan label{cursor:hand;}
		.mostPanel,.groupPanel1,.groupPanel2{
		    display:block;
		    width:180px;
		    float:left;
		    word-break:keep-all;
		    white-space:nowrap;
		    overflow:hidden;
		    text-overflow:ellipsis;
			padding:0px;
			margin:0px;
			height:24px;
			line-height:24px;
		}
		.mostUse,.searchZone{margin:8px;clear:both;}
		label{cursor:pointer;}
		label.mysel{color:Red;}
		.searchZone label{color:blue;padding-left:1px;}
		#searchInfo{font-weight:bold;color:green;}
		.item{padding:2px;cursor:hand;text-decoration:none;color:black;}
		.item:hover{color:red;}
		.mainPanel{clear:both;}
		#txtSearch,#newGroup{border:1px solid gray;padding:3px;}
		#btnSearch,#btnconfirm,#btnclose{
		    padding:3px 8px 3px 8px;
			margin:1px 0px 1px 0px;
			width: auto;
			line-height:14px;
			height:26px;
		    overflow:visible; 
		}
		.newGroupPanel{display:none;padding:20px;}
		
     </style>    
	</head>
	<body >
		<form id="Form1" method="post" runat="server">
            <div style="padding:6px;">
				<div style="padding:3px;margin:0px 5px;height:26px;border:1px solid gray;background-color:#f2f4fb;">
					<span  class="radioSpan"  style="float:left;">&nbsp;&nbsp;&nbsp;类型:
						<input type="radio" name="selType" id="selType0"  value="0" checked="checked" /><label for="selType0">常用联系人</label>
						<input type="radio" name="selType" id="selType1"  value="1" /><label for="selType1">组织机构树</label>
						<input type="radio" name="selType" id="selType2"  value="2" /><label for="selType2">用户组</label>
					</span>
					<span style="float:right;">
						<input class="defaultbtn" id="btnconfirm" style=" left: 72px; top: 8px; "
						type="button" value="确 定"/>
						<input class="defaultbtn" id="btnclose" style=" left: 152px; top: 8px;"
						onclick="window.close();" type="button" value="关 闭"/>
					</span>
				</div>

				<div class="mainPanel">
					<div class="centerZone">
						<div style="border-bottom:1px solid #eee;padding:3px;vertical-align:middle;line-height:30px;height:30px;">
							搜索：<input type="text" id="txtSearch"/>
							<input type="button" value="查 询" id="btnSearch"/>
							<span id="searchInfo"></span>
						</div>
						<div class="searchZone"></div>
						<div class="mostUse">
							<%=sbMostUse%>
						</div>
					</div>
					<div id="tree" class="centerZone hidden">
					</div>
					<div class="centerZone hidden" style="padding:10px;"><%=sbGroup%></div>
				</div>

				<table width="100%" border="0">
					<tr>
					<td height="25">
					<span style="font-size:12px;font-weight:bold;">已选择人员：</span>
					<a href="javascript:" id='linkOnline'>所有在线用户</a>
					<a href="javascript:" id='linkGroupSave'>保存为组</a>
					<a href="javascript:" id='linkClear'>清空</a>
					</td>
					<td style="color:green;text-align:right;">双击可以删除下面选中的人员</td>
					</tr>
				</table>

				<div id="selPanel" style="border:1px solid gray;background-color:white;padding:2px;margin:3px;height:60px;overflow:auto;">
				
				</div>
        </div>
        <div class="newGroupPanel">
            <div>
                <label for="newGroup">分组名称：</label>
                <input type="text" id="newGroup" />
            </div>
            <div style="margin:15px 0px 0px 10px;text-align:center;height:30px;">
                <button style="width:60px;" id="btnGroupYes">  确 定  </button>&nbsp;&nbsp;
                <button style="width:60px;" id="btnGroupNo">  取 消  </button>
            </div>
        </div>
		</form>
	</body>
</html>

<%--<script type="text/javascript" src="../../js/jquery-1.7.min.js"></script>--%>
  <script src="../../js/jquery-1.3.2.min.js" type="text/javascript"></script>
<script type="text/javascript" src="../../js/jquery.tree.js"></script>
<script type="text/javascript" src="../../js/jquery.zxxbox.3.0-min.js"></script>
   
    <script type="text/javascript">
        var _curClass = EIS.WebBase.SysFolder.Common.UserTree;
        $("#btnGroupYes").click(function () {
            var newName = $.trim($("#newGroup").val());
            if (newName == "") {
                alert("分组名称不能为空！");
            }
            else {
                var arrName = [];
                var arrId = [];
                var arrPos = [];
                $("#selPanel").find(".item").each(function () {
                    var arr = $(this).attr("v").split("_");
                    arrId.push(arr[0]);
                    arrPos.push(arr[1]);
                    var t = $(this).text();
                    arrName.push(t.substr(0, t.length - 1));
                });
                var ret = _curClass.NewGroup(newName, arrId, arrName, arrPos);
                if (ret.error) {
                    alert("保存出错：" + ret.error.Message);
                }
                else {
                    alert("保存成功！");
                    $.zxxbox.hide();
                }
            }
        });
        $("#btnGroupNo").click(function () {
            $.zxxbox.hide();
        });
        function addEmployeeList(idcn) {
            var arr = idcn.split("|");
            var idList = arr[0].split(",");
            var cnList = arr[1].split(",");

            var posList = arr[2].split(",");
            for (var i = 0; i < idList.length; i++) {
                var posId = "0"
                if (posList.length > i) {
                    posId = posList[i];
                }
                var ctlId = idList[i] + "_" + posId;
                addEmployee(ctlId, cnList[i]);
            }
        }
        function addEmployee(id, text) {
            if (posIndex > -1) {
                var k = $("#selPanel").find("a[v='" + id + "']");
                if (k.length > 0) return;
            }
            else {
                var empId = id.split("_")[0];
                var k = $("#selPanel").find("a[v^='" + empId + "']");
                if (k.length > 0) return;
            }
            $("<a title='双击删除' href='#' class='item'  v='" + id + "'>" + text + ",</a>").dblclick(
			function () {
			    $(this).remove();
			}).appendTo("#selPanel");

            if (method == "2") {
                fnReturn();
            }

        }
        function removeEmployeeList(idcn) {
            var arr = idcn.split("|");
            var idList = arr[0].split(",");
            var cnList = arr[1].split(",");
            var posList = arr[2].split(",");
            for (var i = 0; i < idList.length; i++) {
                var ctlId = idList[i] + "_" + posList[i];
                var k = $("#selPanel").find("a[v='" + ctlId + "']").remove();

            }
        }

        $(".centerZone .chkmost").live("click", function () {
            var emp = $(this).next("label").text().split(" ")[0];
            if (this.checked) {
                addEmployee(this.value, emp);
            }
            else {
                var k = $("#selPanel").find("a[v='" + this.value + "']").remove();
            }
        });

        $(".mostPanel").live("mouseover", function () {
            if (!this.title) {
                var ctlVal = $("input.chkmost", this).val();
                var pArr = ctlVal.split("_");
                var msg = _curClass.GetTitle(pArr[0], pArr[1]).value;
                this.title = msg;
            }
        });
        $("#txtSearch").keydown(function () {

            if (event.keyCode == 13) {
                $("#btnSearch").click();
                return false;
            }
        });
        $("#btnSearch").click(function () {
            var v = $("#txtSearch").val();
            if (!v) {
                alert("查询信息不能为空");
                return;
            }
            var ret = _curClass.Search(v);
            if (ret.error) {
                alert(ret.error.Message);
            }
            else {
                $(".searchZone").empty();
                var arr = ret.value.split("|");
                if (arr[0].length == 0) {
                    $("#searchInfo").html("&nbsp;没有查找到符合条件的员工");
                    return;
                }
                var idList = arr[0].split(",");
                var cnList = arr[1].split(",");
                var posList = arr[2].split(",");
                $("#searchInfo").html("&nbsp;共查找到" + idList.length + "个符合条件的员工");
                for (var i = 0; i < idList.length; i++) {
                    var cltId = idList[i] + '_' + posList[i];
                    var arr2 = ["<div class='mostPanel' title='' ><input type='checkbox' class='chkmost' value='", cltId
					, "' id='mostPanel", cltId
					, "'/><label for='mostPanel", cltId, "'>"
					, cnList[i]
					, "</label>"];
                    $(arr2.join("")).appendTo(".searchZone");

                }
            }
        });
        $("#linkClear").click(function () {
            $("#selPanel").empty();
        });
        $("#linkGroupSave").click(function () {
            $.zxxbox($(".newGroupPanel"), { title: "新建分组", height: 260, width: 300 });
        });
        $("#linkOnline").click(function () {
            var v = this.value;
            var ret = _curClass.GetOnline();
            if (ret.error) {
                alert(ret.error.Message);
            }
            else {
                addEmployeeList(ret.value);
            }
        });
        //选择公共组
        $(".groupPanel1 .chkgroup").click(function () {
            var v = this.value;
            var ret = _curClass.GetGroup(v);
            if (ret.error) {
                alert(ret.error.Message);
            }
            else {
                if (this.checked)
                    addEmployeeList(ret.value);
                else
                    removeEmployeeList(ret.value);
            }
        });
        //选择个人组
        $(".groupPanel2 .chkgroup").click(function () {
            var v = this.value;
            var ret = _curClass.GetPosition(v);
            if (ret.error) {
                alert(ret.error.Message);
            }
            else {
                if (this.checked)
                    addEmployeeList(ret.value);
                else
                    removeEmployeeList(ret.value);
            }
        });
        //切换选择方式
        $(".radioSpan input").click(function () {
            var i = event.srcElement.value;
            $(".mainPanel>div").hide();
            var k = $(".mainPanel div:eq(" + i + ")").html();
            $(".mainPanel>div:eq(" + i + ")").show();
        });
        $("#btnconfirm").click(function (e) {

            fnReturn();
        });

        function fnReturn() {
            var arrName = [];
            var arrId = [];
            var arrPos = [];
            $("#selPanel").find(".item").each(function () {
                var arr = $(this).attr("v").split("_");
                arrId.push(arr[0]);
                arrPos.push(arr[1]);
                var t = $(this).text();
                arrName.push(t.substr(0, t.length - 1));
            });
            if (idIndex > -1) {
                window.opener.document.getElementById(bizfields[idIndex]).value = arrId;
            }
            if (nameIndex > -1) {
                window.opener.document.getElementById(bizfields[nameIndex]).value = arrName;
            }
            if (posIndex > -1) {
                window.opener.document.getElementById(bizfields[posIndex]).value = arrPos;
            }
            if ('<%=Request["callback"] %>' != "") {
                window.opener["<%=Request["callback"] %>"](bizfields, arrId, arrName);
            }
            window.close();
        }

        var bizfields = "<%=cid %>".split(",");
        var qryfields = "<%=queryfield %>".split(",");
        var idIndex = jQuery.inArray("empid", qryfields);
        var nameIndex = jQuery.inArray("empname", qryfields);
        var posIndex = jQuery.inArray("posid", qryfields);
        var method = "<%=Request["method"]%>";
        var userAgent = window.navigator.userAgent.toLowerCase();
        $.browser.msie8 = $.browser.msie && /msie 8\.0/i.test(userAgent);
        $.browser.msie7 = $.browser.msie && /msie 7\.0/i.test(userAgent);
        $.browser.msie6 = !$.browser.msie8 && !$.browser.msie7 && $.browser.msie && /msie 6\.0/i.test(userAgent);
        <%=treedata %>
        function load() {
            var o = {
                showcheck: true,
                onnodeclick: function (item) {
                },
                aftercheck: function (item) {
                    if (item.id.length == 36) {
                        if (item.checkstate == 1) {
                            var empName = item.text.split(" ")[0];
                            var ctlId = item.id + "_" + item.value;
                            addEmployee(ctlId, empName);
                        }
                        else {
                            var ctlId = item.id + "_" + item.value;
                            var k = $("#selPanel").find("a[v='" + ctlId + "']").remove();
                        }
                    }
                },
                blankpath: "../../Img/common/",
                cbiconpath: "../../Img/common/",
                url: "../Common/TreeData.ashx?queryid=DeptAndEmployeeByDeptID"
            };
            o.data = treedata;
            $("#tree").treeview(o);

            //加载老数据
            if (idIndex > -1 && '<%=Request["callback"] %>' == "" && (method == "1" || method == "")) {
                var idList = window.opener.document.getElementById(bizfields[idIndex]).value;
                if (idList.length > 0) {
                    var nameList = "";
                    var posList = "";
                    nameList = window.opener.document.getElementById(bizfields[nameIndex]).value;
                    if (posIndex > -1) {
                        posList = window.opener.document.getElementById(bizfields[posIndex]).value;
                    }

                    addEmployeeList(idList + "|" + nameList + "|" + posList);
                }
            }
        }
        if ($.browser.msie6) {
            load();
        }
        else {
            $(document).ready(load);
        }
    </script>
