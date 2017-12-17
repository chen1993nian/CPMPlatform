<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DateDropDown.aspx.cs" Inherits="EIS.Studio.SysFolder.DefFrame.DateDropDown" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
	<head>
		<title>日期时间格式设置</title>
		<meta http-equiv="Pragma" content="no-cache"/>
		<meta name="vs_defaultClientScript" content="JavaScript"/>
        <script type="text/javascript" src="../../../js/jquery-1.7.min.js"></script>
		<link href="../../../css/appstyle.css" rel="stylesheet"/>
		<script type="text/javascript">
        function doOK()
        {	
            var arr=["","","",""];
            if (form1.txtStyle.value == "")
            {
	            alert ("自定义格式不能为空");
	            return false;
            }
            arr[0] = form1.txtStyle.value;
            if (form1.txtMin.value != "")
            {
                arr[1] = form1.txtMin.value;
            }
            if (form1.txtMax.value != "")
            {
                arr[2] = form1.txtMax.value;
            }
            arr[3] = $("#txtDis").attr("checked")=="checked"?"1":"0";
            parent.frameElement.lhgDG.curWin.styleCallBack("<%=Request["key"] %>:<%=Request["titleName"] %>:"+arr.join("|"));
            parent.frameElement.lhgDG.cancel();
        }

        function doCancel()
        {
           parent.frameElement.lhgDG.cancel();
        }
        function selchange()
        {
            document.getElementById("txtStyle").value=event.srcElement.value;
        }
        jQuery(function(){
            if(getOpenerValue("stylecode")=="001")
            {
                var arr=getOpenerValue("styletxt").split("|");
                if(arr.length == 4)
                {
                    jQuery("#txtStyle").val(arr[0]);
                    jQuery("#txtMin").val(arr[1]);
                    jQuery("#txtMax").val(arr[2]);
                    jQuery("#txtDis").attr("checked",arr[3]=="1");
                }
            }
        });
        function getOpenerValue(ctlName) {
            return parent.frameElement.lhgDG.curWin.document.getElementById(ctlName).value;
        }
		</script>
		<style type="text/css">
		td{font-size:12px;height:28px}
		input.btn{padding:3px 8px;height:28px;}
		</style>
	</head>
	<body >
	    <form id="form1" runat="server">
		<br/>
		<table border="0" width="90%" align="center">
			<tr>
				<td width="100%"><b>请设置日期时间格式:</b></td>
			</tr>
			<tr>
				<td width="100%">
					<table width="100%" border="0" cellspacing="4" cellpadding="0" style="BORDER-RIGHT:#000000 1px solid; BORDER-TOP:#000000 1px solid; BORDER-LEFT:#000000 1px solid; BORDER-BOTTOM:#000000 1px solid"
						id="Table1">
						<tr>
							<td align="right">选择格式：</td>
							<td>
							<select id="selstyle" onchange="selchange()">
							    <option value="yyyy-MM-dd">2008-10-08</option>
							    <option value="yyyy年MM月">2008年10月</option>
							    <option value="HH:mm:ss">11:29:15</option>
							    <option value="yyyy-MM-dd HH:mm">2008-10-08 08:08</option>
							    <option value="yyyy-MM-dd HH:mm:ss">2008-10-08 08:08:08</option>
							</select>
							</td>
						</tr>
						<tr>
							<td align="right">自定义格式：</td>
							<td><input type='text' id="txtStyle" name='txtStyle' maxlength="20" value="yyyy-MM-dd"/></td>
						</tr>
						<tr>
							<td align="right">最小值：</td>
							<td><input type='text' id="txtMin" name='txtMin' maxlength="20"/></td>
						</tr>
						<tr>
							<td align="right">最大值：</td>
							<td><input type='text' id="txtMax" name='txtMax' maxlength="20"/></td>
						</tr>
						<tr>
							<td align="right"></td>
							<td>
                            <input type='checkbox' id="txtDis" name='txtDis' />
                            <label for="txtDis">双月日历显示</label>
                            </td>
						</tr>
					</table>
				</td>
			</tr>
			<tr>
				<td style="font-size:13px"><br/>自定义格式：<a href="../../../DatePicker/DateDemo.mht" target="_blank">查看说明</a></td>
			</tr>
            <tr>
				<td align="right">
					<input type="button" value="确认" class="btn" onclick="doOK()"/> 
					<input type="button" value="取消" class="btn" onclick="doCancel()"/>
				</td>
			</tr>
		</table>
    </form>
		
	</body>
</html>
