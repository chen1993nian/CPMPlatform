<%@ Page language="c#" Codebehind="UserTreeInOne.aspx.cs" AutoEventWireup="false" Inherits="EIS.WebBase.SysFolder.Common.UserTreeInOne" enableViewState="True"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>选择用户</title>
     <link href="../../css/appstyle.css" rel="stylesheet" type="text/css" />    
     <link href="../../css/tree.css" rel="stylesheet" type="text/css" />
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
	        margin:5px;
			height:300px;
			overflow:auto;
			background:white;
		}
		.radioSpan label{cursor:hand;}
		.groupPanel1,.groupPanel2{width:200px;float:left;}
		.mostPanel{width:100px;float:left;}
		.mostUse,.searchZone{padding:5px;clear:both;}
		.searchZone label{color:blue;}
		.mostUse{margin-top:5px;}
		#searchInfo{font-weight:bold;color:green;}
		.item{padding:2px;cursor:hand;text-decoration:none;color:black;}
		.mainPanel{clear:both;}
		#txtSearch{border:1px solid gray;padding:3px;}
     </style>    
	</head>
	<body >
		<form id="Form1" method="post" runat="server">
            <div style="padding:6px;">
				<div style="padding:3px;margin:0px 5px;height:26px;border:1px solid gray;background-color:#f2f4fb;">
					<span  class="radioSpan"  style="float:left;">&nbsp;&nbsp;&nbsp;类型:
						<input type="radio" name="selType" id="selType0"  value="0" checked="checked" /><label for="selType0">常用联系人</label>
						<input type="radio" name="selType" id="selType1"  value="1" /><label for="selType1">本公司</label>
						<input type="radio" name="selType" id="selType2"  value="2" /><label for="selType2">用户组</label>
					</span>
					<span style="float:right;">
						<input class="defaultbtn" id="btnconfirm" style=" left: 72px; top: 8px; height: 24px"
						type="button" value="确 定"/>
						<input class="defaultbtn" id="btndel" style=" left: 152px; top: 8px; height: 24px"
						onclick="window.close();" type="button" value="关 闭"/>
					</span>
				</div>

				<div class="mainPanel">
					<div class="centerZone">
						<div style="border-bottom:1px solid #eee;padding:3px;">
							搜索：<input type=text id="txtSearch"/>
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
					<div class="centerZone hidden"><%=sbGroup%></div>
				</div>

				<div style="height:20px;margin:3px 0px;padding:3px;">
					<span style="font-size:12px;font-weight:bold;">已选择人员：</span>
					<a href="#" id='linkOnline'>所有在线用户</a>
					<a href="#">保存为组</a>
					<a href="#" id='linkClear'>清空</a>
				</div>

				<div id="selPanel" style="border:1px solid gray;background-color:white;padding:2px;margin:3px;height:60px;overflow:auto;">
				
				</div>
        </div>
		</form>
	</body>
</html>

    <script src="../../js/jquery-1.3.2.min.js" type="text/javascript"></script>
    <script src="../../js/jquery.tree.js" type="text/javascript"></script>
   
    <script type="text/javascript">
        var _curClass = EIS.WebBase.SysFolder.Common.UserTreeInOne;
        Array.prototype.contains = function (element) {
            for (var i = 0; i < this.length; i++) {
                if (this[i] == element) {
                    return i;
                }
            }
            return -1;
        }
        function addEmployeeList(idcn) {
            var arr = idcn.split("|");
            var idList = arr[0].split(",");
            var cnList = arr[1].split(",");

            for (var i = 0; i < idList.length; i++) {
                addEmployee(idList[i], cnList[i]);
            }
        }
        function addEmployee(id, text) {
            var k = $("#selPanel").find("a[v='" + id + "']");
            if (k.length > 0) return;
            $("<a title='双击删除' href='#' class='item' v='" + id + "'>" + text + ",</a>").dblclick(
			function () {
			    $(this).remove();
			}).appendTo("#selPanel");
        }
        function removeEmployeeList(idcn) {
            var arr = idcn.split("|");
            var idList = arr[0].split(",");
            var cnList = arr[1].split(",");

            for (var i = 0; i < idList.length; i++) {
                var k = $("#selPanel").find("a[v='" + idList[i] + "']").remove();

            }
        }
        var userAgent = window.navigator.userAgent.toLowerCase();
        $.browser.msie8 = $.browser.msie && /msie 8\.0/i.test(userAgent);
        $.browser.msie7 = $.browser.msie && /msie 7\.0/i.test(userAgent);
        $.browser.msie6 = !$.browser.msie8 && !$.browser.msie7 && $.browser.msie && /msie 6\.0/i.test(userAgent);
        <%=treedata %>
        function load() {
            $(".centerZone .chkmost").live("click", function () {
                var emp = $(this).next("label").text();
                if (this.checked) {
                    addEmployee(this.value, emp);
                }
                else {
                    var k = $("#selPanel").find("a[v='" + this.value + "']").remove();
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
                    $("#searchInfo").html("&nbsp;共查找到" + idList.length + "个符合条件的员工");
                    for (var i = 0; i < idList.length; i++) {
                        var arr2 = ["<div class='mostPanel' ><input type='checkbox' class='chkmost' value='", idList[i], "' id='mostPanel", idList[i]
						, "'/><label for='mostPanel", idList[i], "'>", cnList[i], "</label>"];
                        $(arr2.join("")).appendTo(".searchZone");

                    }
                }
            });
            $("#linkClear").click(function () {
                $("#selPanel").empty();
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

            $(".radioSpan input").click(function () {
                var i = event.srcElement.value;
                $(".mainPanel>div").hide();
                var k = $(".mainPanel div:eq(" + i + ")").html();
                $(".mainPanel>div:eq(" + i + ")").show();
            });

            var o = {
                showcheck: true,
                onnodeclick: function (item) {

                },
                aftercheck: function (item) {
                    if (item.id.length == 36) {
                        if (item.checkstate == 1) {
                            addEmployee(item.id, item.text);
                        }
                        else {
                            var k = $("#selPanel").find("a[v='" + item.id + "']").remove();
                        }
                    }
                },
                blankpath: "../../Img/common/",
                cbiconpath: "../../Img/common/",
                url: "../Common/TreeData.ashx?queryid=EmployeeByDeptID"
            };
            o.data = treedata;
            $("#tree").treeview(o);

            $("#btnconfirm").click(function (e) {
                //var s=$("#tree").getTSVs();
                var cid = "<%=cid %>";
                var bizfields = cid.split(",");
                var qryfields = "<%=queryfield %>".split(",");
              var idindex = qryfields.contains("empid");
              var nameindex = qryfields.contains("empname")
              var emps = [];
              var ids = [];
              $("#selPanel").find(".item").each(function () {
                  ids.push(this.v);
                  var t = $(this).text();
                  emps.push(t.substr(0, t.length - 1));
              });
              if (idindex > -1) {
                  window.opener.document.getElementById(bizfields[idindex]).value = ids;
              }
              if (nameindex > -1) {
                  window.opener.document.getElementById(bizfields[nameindex]).value = emps;
              }
              if ('<%=Request["callback"] %>' != "") {
                   window.opener["<%=Request["callback"] %>"](bizfields);
               }

                window.close();


            });

       }
       if ($.browser.msie6) {
           load();
       }
       else {
           $(document).ready(load);
       }
    </script>
