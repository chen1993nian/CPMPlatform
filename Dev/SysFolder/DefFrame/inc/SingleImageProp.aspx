<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SingleImageProp.aspx.cs" Inherits="Studio.JZY.SysFolder.DefFrame.inc.SingleImageProp" %>


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
            arr[2] = document.getElementById("txtAlt").value;
            arr[3] = document.getElementById("txtCss").value;
            if (document.getElementById("radioPath1").checked) {
                arr[4] = "0";
            }
            else {
                arr[4] = "1";
            }
            arr[5] = document.getElementById("txtEmpty").value;

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
            if (getOpenerValue("stylecode") == "024") {
                var arr = getOpenerValue("styletxt").split("|");
                if (arr.length > 4) {
                    jQuery("#fileExt").val(arr[0]);
                    jQuery("#fileSize").val(arr[1]);
                    jQuery("#txtAlt").val(arr[2]);
                    jQuery("#txtCss").val(arr[3]);

                    if (arr[4] == "1")
                        jQuery("#radioPath2").attr("checked", true);
                    if (arr[4] == "0")
                        jQuery("#radioPath1").attr("checked", true);
                }
                if (arr.length > 5)
                    jQuery("#txtEmpty").val(arr[5]);
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
				<td width="100%"><b>单个图片上传设置:</b></td>
			</tr>
			<tr>
				<td width="100%">
					<table width="100%" border="1" id="Table1">
                    <tbody>
						<tr>
							<td align="left" width="100">选择格式：</td>
							<td>
                                <input type="text" name="fileExt" id="fileExt" />
                                <div style='line-height:24px;'> 支持上传文件类型（格式：*.jpg;*.png），空代表所有格式</div>
							</td>
						</tr>
                        <tr>
							<td align="left">单个文件最大：</td>
							<td>
                                <input type="text" id="fileSize"/>&nbsp;单位(M)，空代表不限制
							</td>
						</tr>
                        <tr>
							<td align="left">替换文本属性：</td>
							<td>
                                <input type="text" style="width:300px;" id="txtAlt"/>
							</td>
						</tr>
                        <tr>
							<td align="left">空白图片路径：</td>
							<td>
                                <input type="text" style="width:300px;" id="txtEmpty"/>
                                <div style='line-height:24px;'> 路径从根目录开始计算，示例：img/bbs/head_80.jpg</div>
							</td>
						</tr>
                        <tr>
							<td align="left">图片Css：</td>
							<td>
                                <input type="text" style="width:300px;" id="txtCss"/>
							</td>
						</tr>
                        <tr>
							<td align="left">存储路径：</td>
							<td>
                                <input type="radio" name="radioPath" id="radioPath1" value="0" checked="checked" /><label for="radioPath1">&nbsp;默认路径&nbsp;</label>
                                <input type="radio" name="radioPath" id="radioPath2" value="1" /><label for="radioPath2">&nbsp;单独路径&nbsp;</label>
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
