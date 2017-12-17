<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CheckBox_SQL.aspx.cs" Inherits="Studio.JZY.SysFolder.DefFrame.inc.CheckBox_SQL" %>


<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
	<head>
		<title>
			CheckBoxSQL语句
		</title>
		<link href="../../../css/appstyle.css" rel="stylesheet"/>
        <script type="text/javascript" src="../../../js/jquery-1.7.min.js"></script>
		<script type="text/javascript">
        <!--
    function Confirm() {
        var txtSQL = $("#rvalue").val();
        if (txtSQL == "") {
            alert("自定义值SQL语句不能为空！");
            return false;
        }
        else {
            var arrRet = [];
            arrRet.push($("#selLayout").val());
            arrRet.push($("input[name=raddir]:checked").val());
            arrRet.push($("#colNum").val());
            arrRet.push($("#showNum").attr("checked") == "checked" ? "1" : "0");
            parent.frameElement.lhgDG.curWin.styleCallBack("<%=Request["key"] %>:<%=Request["titleName"] %>:" + arrRet.join(",") + "`" + txtSQL);
				parent.frameElement.lhgDG.cancel();
            }

        }
        function Cancel() {
            parent.frameElement.lhgDG.cancel();
        }

        jQuery(function () {
            if (getOpenerValue("stylecode") == "<%=Request["key"] %>") {
                var pv = getOpenerValue("styletxt");
                if (pv.indexOf("`") > 0) {
                    var pvs = pv.split("`");
                    var arr = pvs[0].split(",");
                    $("#selLayout").val(arr[0]);
                    if (arr[1] == "1")
                        $("#raddir1").attr("checked", "checked");
                    else
                        $("#raddir2").attr("checked", "checked");
                    $("#colNum").val(arr[2]);

                    if (arr[3] == "1")
                        $("#showNum").attr("checked", "checked");
                    else
                        $("#showNum").removeAttr("checked");

                    jQuery("#rvalue").val(pvs[1]);
                }
                else {
                    jQuery("#rvalue").val(pv);
                }

            }
        });
           function getOpenerValue(ctlName) {
               return parent.frameElement.lhgDG.curWin.document.getElementById(ctlName).value;
           }
           //-->
		</script>
        <style type="text/css">
            td{font-size:10pt;padding:5px;}
            input.btn{padding:3px 8px;height:28px;}
			.subtitle{color:#dc143c;}
        </style>
	</head>
	<body >
        <br/><br/>
		<table id="Table2" width="90%" align="center" border="0">
			<tr>
				<td ><b style="color:Blue;font-weight:bold;">CheckBox显示：</b>自定义SQL语句</td>
			</tr>
			<tr>
				<td class="cnt" width="100%">
					<table id="Table1" style="border: #aaa 1px solid;"
						 width="100%" border="0">
						<tr>
							<td align="left" colspan="2" >
								<table>
									<tr>
										<td class='subtitle'>布局:</td>
										<td >
											<select id="selLayout">
												<option value="0">默认</option>
												<option value="1">表格</option>
											</select>
										</td>
										<td class='subtitle'>&nbsp;&nbsp;方向:</td>
										<td  align="left">
											<input type="radio" name='raddir' id='raddir1' checked value="1"><label for='raddir1'>水平</label>
											<input type="radio" name='raddir' id='raddir2' value="2"><label for='raddir2'>垂直</label>
										</td>
										<td class='subtitle'>&nbsp;&nbsp;列(行)数:</td>
										<td>
											<input type="text" id="colNum" value="" size="3"/>
										</td>
										<td>&nbsp;&nbsp;
											<input type="checkbox" id="showNum"/><label for='showNum' class='subtitle'>&nbsp;显示序号</label>
										</td>
									</tr>
								</table>
							</td>
							</tr><tr>
							<td colspan="2">
                                <textarea id="rvalue" rows="10" style="width:100%;"></textarea>
                            </td>
						</tr>
					</table>
                </td></tr>
                <tr><td style="font-size:10pt">
					1、自定义SQL语句示例：select 值字段 , 显示字段
				</td>
			</tr>
			<tr><td align="right">
			    <input type="button" value="确认" class="btn" onclick="Confirm()"/> 
                <input type="button" value="取消" class="btn" onclick="Cancel()"/>
			    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
			</td></tr>
		</table>
	</body>
</html>
