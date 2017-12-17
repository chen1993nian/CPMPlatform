<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebOfficeProp.aspx.cs" Inherits="Studio.JZY.SysFolder.DefFrame.inc.WebOfficeProp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>WebOffice属性</title>
    <link href="../../../css/appstyle.css" rel="stylesheet"/>
    <script type="text/javascript" src="../../../js/jquery-1.7.min.js"></script>
    <script type="text/javascript">
        function doOK() {
            var arr = ["", "", "", ""];
            arr[0] = jQuery("#selstyle").val();
            arr[1] = jQuery("#selmb").val();
            arr[2] = jQuery("#txtmb").val();

            parent.frameElement.lhgDG.curWin.styleCallBack("<%=Request["key"] %>:<%=Request["titleName"] %>:" + arr.join("|"));
            parent.frameElement.lhgDG.cancel();
        }

        function doCancel() {
            parent.frameElement.lhgDG.cancel();
        }
        function selchange() {
            //document.all("txtStyle").value=event.srcElement.value;
        }

        jQuery(function () {
            if (getOpenerValue("stylecode") == "022") {
                var arr = getOpenerValue("styletxt").split("|");
                if (arr.length == 4) {
                    jQuery("#selstyle").val(arr[0]);
                    jQuery("#selmb").val(arr[1]);
                    jQuery("#txtmb").val(arr[2]);
                }
            }
        });
        function getOpenerValue(ctlName) {
            return parent.frameElement.lhgDG.curWin.document.getElementById(ctlName).value;
        }
    </script>
        <style type="text/css">
            td{font-size:10pt;padding:5px;}
            input.btn{padding:3px 8px;height:28px;}
            select{width:100px;}
        </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    		<br/>
		<table border="0" width="90%" align="center">
			<tr>
				<td width="100%"><b>请设置文档格式:</b></td>
			</tr>
			<tr>
				<td  align="left">
					<table  align="left" width="100%" border="0" cellspacing="4" cellpadding="0" style="border: #aaa 1px solid;"
						id="Table1">
						<tr>
							<td align="right" width="100">选择格式：</td>
							<td>
							<select id="selstyle" onchange="selchange()">
							    <option value="1">MsWord文档</option>
							    <option value="2">MsExecl文档</option>
							    <option value="3">微软幻灯片</option>
							    <option value="4">金山文档</option>
							    <option value="5">金山电子表格</option>
							</select>
							</td>
						</tr>
                        <tr>
                            <td align="right">模板选择：</td>
                            <td>
							    <select id="selmb">
							        <option value="0">不指定</option>
							        <option value="1">指定参数</option>
							        <option value="2">指定模板</option>
							    </select>
                                <input type="text" name="txtmb" id="txtmb" value="" />

                            </td>
                        </tr>
                        <tr>
                            <td align="right"></td>
                            <td>
                            </td>                            
                        </tr>
					</table>
				</td>
			</tr>
            <tr>
                <td align="right">					
                <input type="button" value="确认" class="btn" onclick="doOK()"/> 
				<input type="button" value="取消" class="btn" onclick="doCancel()"/></td>
            </tr>
		</table>
    </div>
    </form>
</body>
</html>
