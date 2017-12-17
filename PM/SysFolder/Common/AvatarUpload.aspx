<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AvatarUpload.aspx.cs" Inherits="EIS.Web.SysFolder.Common.AvatarUpload" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>上传文件</title>
    <link href="../../Css/AppStyle.css" rel="stylesheet" type="text/css" />
    <link href="../../Css/uploadify-3.2.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../../js/jquery-1.4.2.min.js"></script>
    <script type="text/javascript" src="../../js/jquery.uploadify-3.2.js"></script>

	<style type="text/css">

	a.hover {
	color: red;
    }
	body{margin:0px;padding:0px;}

    #fileQueue{text-align:left;}
    .fileitem {color:Gray;line-height:20px;}
    .fileitem a{text-decoration:none;}
	.fileitem .dellink{
		text-decoration:none;
	}
	.fileitem .dellink:hover{
		text-decoration: underline;
	}
	.hidden{display:none;}
	</style>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#uploadify").uploadify({
                'swf': '../../js/uploadify-3.2.swf',
                'uploader': '../../FancyUpload.axd',
                'formData': { appName: "<%=appName %>", appId: "<%=appId %>", folder: "<%=folder %>" },
                'buttonText': ' ' + escape("选择文件"),
                'buttonImage': '../../img/common/browser.png',
                'height': 24,
                'width': 78,
                'fileTypeExts': "<%=ext %>",
                'fileSizeLimit': "<%=limit %>",
                'folder': '<%=folder %>',
                //'queueID': 'fileQueue',
                'auto': true,
                'multi': false,
                'overrideEvents': ['onSelect', 'onUploadSuccess', 'onQueueComplete', 'onSelectError'],
                onSelect: function (file) {
                    //$("#fileQueue").removeClass("hidden");
                },
                onQueueComplete: function () {
                    //window.location.reload();
                    //$("#fileQueue").addClass("hidden");

                },
                onUploadSuccess: function (fileObj, response, data) {
                    var fileId = response;
                    $("#AvaterImg").attr("src", "FileDown.aspx?fileId=" + fileId);
                },
                //返回一个错误，选择文件的时候触发        
                onSelectError: function (file, errorCode, errorMsg) {
                    switch (errorCode) {
                        case -100:
                            alert("上传的文件数量已经超出系统限制的" + $('#uploadify').uploadify('settings', 'queueSizeLimit') + "个文件！");
                            break;
                        case -110:
                            alert("文件 [" + file.name + "] 大小超出系统限制的" + $('#uploadify').uploadify('settings', 'fileSizeLimit') + "大小！");
                            break;
                        case -120:
                            alert("文件 [" + file.name + "] 大小异常！");
                            break;
                        case -130:
                            alert("文件 [" + file.name + "] 类型不正确！");
                            break;
                    }
                },
                //检测FLASH失败调用        
                onFallback: function () {
                    alert("您未安装FLASH控件，无法上传图片！请安装FLASH控件后再试。");
                },
                onError: function (event, queueId, fileObj, errorObj) {
                    alert("文件【" + fileObj.name + "】上传错误");
                }
            });
            $("#AvaterImg").click(function () {
                var win = window.open("", "_blank"); win.document.write("<img src='" + this.src + "' />");

            });
        });

        function overLoad() {
            if (window.opener.store != undefined) {
                window.opener.store.reload();
                window.opener.store.commitChanges();
            }
            else {
                window.opener.location.href = window.opener.location.href;
            }
            window.close();
        }
        function delFile(fileId) {
            if (!confirm("你确认要删除这个文件吗"))
                return;
            var ret = EIS.Web.SysFolder.Common.FileListFrame.DeleteFile(fileId);
            if (ret.error) {
                alert(ret.error.Message);
            }
            else {
                window.location.reload();
            }
        }
	</script>
</head>
<body style="background:white;" >
    <form id="form1" runat="server">
    <div id="maindiv" style="padding:5px;overflow:auto;text-align:left;">
    <img id="AvaterImg" src="<%=filePath %>" alt="<%=imgAlt %>" style="<%=imgCss %>"/>
	<div style="<%=read%>;text-align:left;">
		<input type="file" name="uploadify" id="uploadify" /> 
		<input class="btn_sub_DelRow hidden" type="button" value="上传文件" onclick="javascript: $('#uploadify').uploadifyUpload()" />
		<input class="btn_sub_DelRow hidden" type="button" value="全部取消" onclick="javascript: $('#uploadify').uploadifyClearQueue()" />
    </div>
    </div>
    </form>
</body>
</html>
