<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MultiAttachProp.aspx.cs" Inherits="Studio.JZY.SysFolder.DefFrame.inc.MultiAttachProp" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>附件上传属性</title>
    <link href="../../../css/appstyle.css" rel="stylesheet"/>
    <script type="text/javascript" src="../../../js/jquery-1.7.min.js"></script>
    <script type="text/javascript">
        function doOK() {
            var arr = ["", "", "", "", ""];
            arr[0] = document.getElementById("fileExt").value;
            arr[1] = document.getElementById("fileSize").value;
            if (document.getElementById("radioMulti1").checked) {
                arr[2] = "1";
            }
            else {
                arr[2] = "0";
            }

            if (document.getElementById("radioPath1").checked) {
                arr[3] = "0";
            }
            else {
                arr[3] = "1";
            }

            if (document.getElementById("radioArchive1").checked) {
                arr[4] = "1";
            }
            else {
                arr[4] = "0";
            }

            parent.frameElement.lhgDG.curWin.styleCallBack("<%=Request["key"] %>:<%=Request["titleName"] %>:" + arr.join("|"));
            parent.frameElement.lhgDG.cancel();
        }

        function doCancel() {

            parent.frameElement.lhgDG.cancel();
        }
        function selchange() {
            //document.all("txtStyle").value=event.srcElement.value;
        }
        var arrStyle = ["013", "014", "041", "042", "051", "052"];
        jQuery(function () {
            if (getOpenerValue("stylecode") == "023") {
                var arr = getOpenerValue("styletxt").split("|");
                if (arr.length == 4) {
                    jQuery("#fileExt").val(arr[0]);
                    jQuery("#fileSize").val(arr[1]);
                    if (arr[2] == "1")
                        jQuery("#radioMulti1").attr("checked", true);
                    if (arr[2] == "0")
                        jQuery("#radioMulti2").attr("checked", true);

                    if (arr[3] == "1")
                        jQuery("#radioPath2").attr("checked", true);
                    if (arr[3] == "0")
                        jQuery("#radioPath1").attr("checked", true);

                    jQuery("#radioArchive1").attr("checked", true);
                }
                else if (arr.length == 5) {
                    if (arr[4] == "1")
                        jQuery("#radioArchive1").attr("checked", true);
                    if (arr[4] == "0")
                        jQuery("#radioArchive2").attr("checked", true);
                }
            }
        });
        function getOpenerValue(ctlName) {
            return parent.frameElement.lhgDG.curWin.document.getElementById(ctlName).value;
        }
    </script>

    <style type="text/css">
        #Table1
        {
            border-collapse:collapse;
            border:#888 1px solid;
            }
        #Table1>tbody>tr> td
        {
            border:#888 1px solid;
            padding:3px;
            }
         #fileExt
         {
             font-size:bold;
             width:300px;
             font-family:Tahoma,Helvetica,Arial,sans-serif;
             }
        input.btn{padding:3px 8px;height:28px;}
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    		<br/>
		<table border="0" width="90%" align="center">
			<tr>
				<td width="100%"><b>上传文件设置:</b></td>
			</tr>
			<tr>
				<td width="100%">
					<table width="100%" border="1" id="Table1">
                    <tbody>
						<tr>
							<td align="left" width="100">选择格式：</td>
							<td>
                                <input type="text" name="fileExt" id="fileExt" /><br />
                                支持上传文件类型（格式：*.jpg;*.png），空代表所有格式
							</td>
						</tr>
                        <tr>
							<td align="left">单个文件最大：</td>
							<td>
                                <input type="text" id="fileSize"/>&nbsp;单位(M)，空代表不限制
							</td>
						</tr>
                        <tr>
							<td align="left">是否多文件上传：</td>
							<td>
                                <input type="radio" name="radioMulti" id="radioMulti1" value="1" checked="checked" /><label for="radioMulti1">&nbsp;是&nbsp;</label>
                                <input type="radio" name="radioMulti" id="radioMulti2" value="0" /><label for="radioMulti2">&nbsp;否&nbsp;</label>
							</td>
						</tr>
                        <tr>
							<td align="left">存储路径：</td>
							<td>
                                <input type="radio" name="radioPath" id="radioPath1" value="0" checked="checked" /><label for="radioPath1">&nbsp;默认路径&nbsp;</label>
                                <input type="radio" name="radioPath" id="radioPath2" value="1" /><label for="radioPath2">&nbsp;单独路径&nbsp;</label>
							</td>
						</tr>
                        <tr>
							<td align="left" class="red">归档附件字段：</td>
							<td>
                                <input type="radio" name="radioArchive" id="radioArchive1" value="1" checked="checked" /><label for="radioArchive1">&nbsp;是&nbsp;</label>
                                <input type="radio" name="radioArchive" id="radioArchive2" value="0" /><label for="radioArchive2">&nbsp;否&nbsp;</label>
							</td>
						</tr>
                        </tbody>
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
