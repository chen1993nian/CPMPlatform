<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FileListFrame.aspx.cs" Inherits="EIS.WebBase.SysFolder.Common.FileListFrame" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>上传文件</title>
    <link href="../../Css/AppStyle.css" rel="stylesheet" type="text/css" />
    <link href="../../Css/uploadify-3.2.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../../js/jquery-1.4.2.min.js"></script>
    <script type="text/javascript" src="../../js/jquery.uploadify-3.2.js"></script>
    <script type="text/javascript" src="../../js/lhgdialog.min.js"></script>

	<style type="text/css">

	a.hover {
	color: red;
    }
	body{margin:0px;padding:0px;}
    .oldfilecontent
    {
        padding-left:0px;
        text-align:left;
        }
    #fileQueue{text-align:left;}
    .fileitem {color:Gray;line-height:20px;}
    .fileitem a{text-decoration:none;}
    .filelink{
        padding:0px 0px 0px 18px;
        background:transparent url(../../img/email/compose104472.png) no-repeat 0px 0px ;
        }
	.fileitem .dellink{
		text-decoration:none;
	}
	.fileitem .dellink:hover{
		text-decoration: underline;
	}
	.hidden{display:none;}
	.flashbtn{position:absolute;left:0px;top:0px;}
	.uploadify-queue{margin-bottom:3px;}
	#fromMyDoc{position:absolute;left:80px;cursor:pointer; width:100px;height:21px;line-height:140%;color:#0068B7;padding:1px 5px;z-index:99;border:1px solid #AABED3;}
	#fromJZYWX{position:absolute;left:185px;cursor:pointer; width:100px;height:21px;line-height:140%;color:#0068B7;padding:1px 5px;z-index:99;border:1px solid #AABED3;}
	</style>
    <script type="text/javascript">
        $(document).ready(function () {
            
            var auth = "<% = Request.Cookies[FormsAuthentication.FormsCookieName]==null ? string.Empty : Request.Cookies[FormsAuthentication.FormsCookieName].Value %>";
            var ASPSESSID = "<%= Session.SessionID %>";

            $("#uploadify").uploadify({
                'swf': '../../js/uploadify-3.2.swf',
                'uploader': '../../FancyUpload.axd',
                'formData': {appName:"<%=appName %>",appId:"<%=appId %>",folder:"<%=folder %>",'ASPSESSID': ASPSESSID, 'AUTHID': auth},
                'buttonText': ' ' + escape("选择文件"),
                'buttonImage': '../../img/common/browser.png',
                'buttonClass': 'flashbtn',
                'height': 24,
                'width': 78,
                'fileTypeExts':"<%=ext %>",
                'fileSizeLimit': "<%=limit %>",
                'folder': '<%=folder %>',
                //'queueID': 'fileQueue',
                'auto': true,
                'multi': <%=multi %>,
                'overrideEvents':['onUploadSuccess','onQueueComplete','onSelectError'],
                onSelect:function(file){
                    //$("#fileQueue").removeClass("hidden");
                },
                onQueueComplete:function(){
                    //window.location.reload();
                    //$("#fileQueue").addClass("hidden");

                },
                onUploadSuccess:function(fileObj,response, data) {
                    var fileId=response;
                    var arr=[];
                    arr.push("<div class='fileitem'><a class='filelink' href='FileDown.aspx?fileId=");
                    arr.push(fileId);
                    arr.push("' target='_blank'>");
                    arr.push(fileObj.name);
                    arr.push("</a>&nbsp;<a class='dellink' href=\"javascript:delFile('");
                    arr.push(fileId);
                    arr.push("')\">[删除]</a></div>");
                    $(".oldfilecontent").append(arr.join(""));
                },
                //返回一个错误，选择文件的时候触发        
                onSelectError:function(file, errorCode, errorMsg){            
                    switch(errorCode) {                
                        case -100:                    
                            alert("上传的文件数量已经超出系统限制的"+$('#uploadify').uploadify('settings','queueSizeLimit')+"个文件！");                    
                            break;                
                        case -110:                    
                            alert("文件 ["+file.name+"] 大小超出系统限制的"+$('#uploadify').uploadify('settings','fileSizeLimit')+"大小！");                    
                            break;                
                        case -120:                    
                            alert("文件 ["+file.name+"] 大小异常！");                    
                            break;                
                        case -130:                    
                            alert("文件 ["+file.name+"] 类型不正确！");                    
                            break;           
                    }
                },        
                //检测FLASH失败调用        
                onFallback:function(){            
                    alert("您未安装FLASH控件，无法上传图片！请安装FLASH控件后再试。");        
                },
                onError: function (event, queueId, fileObj, errorObj) {
                    alert("文件【"+fileObj.name + "】上传错误");
                }
            });
            jQuery("#uploadify-button").css({"background-repeat":"no-repeat"});
            jQuery("#uploadify").css({width:"100%","margin-bottom":"3px"});
            jQuery("#fromMyDoc").appendTo("#uploadify").click(function(){
                var dlg = new $.dialog({ title: '从我的文档选择文件', page: '../../Doc/SelMyDocFrame.aspx'
                    , btnBar: true, cover: true, lockScroll: true, width: 800, height: 500, bgcolor: 'gray', cancelBtnTxt: '关闭',
                    onCancel: function () {
                    }
                });
                dlg.ShowDialog();
            });
            jQuery("#fromJZYWX").appendTo("#uploadify").click(function(){
                var dlgWX = new $.dialog({ title: '微信拍照上传', page: '../../WorkAsp/WXScene/WX_BoundAffixFile.aspx?AppId=<%=appId %>&TableName=<%=appName %>'
                    , btnBar: true, cover: true, lockScroll: true, width: 450, height: 540 , bgcolor: 'gray', cancelBtnTxt: '关闭',
                    onCancel: function () {
                    }
                });
                dlgWX.ShowDialog();
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
            var ret = EIS.WebBase.SysFolder.Common.FileListFrame.DeleteFile(fileId);
            if (ret.error) {
                alert(ret.error.Message);
            }
            else {
                window.location.reload();
            }
        }
        //用于从我的文档插入文件
        function copyFiles(arrFileId) {
            var ret = EIS.WebBase.SysFolder.Common.FileListFrame.CopyFiles(arrFileId.join(","),"<%=appId %>","<%=appName %>");
            if (ret.error) {
                alert(ret.error.Message);
            }
            else {
                var arr=[];
                var arrFile = ret.value.split(",");
                for (var i = 0; i < arrFile.length; i++) {
                    var arr2=arrFile[i];
                    var fileId=arr2.split("|")[0];
                    var fileName=arr2.split("|")[1];

                    arr.push("<div class='fileitem'><a class='filelink' href='FileDown.aspx?fileId=");
                    arr.push(fileId);
                    arr.push("' target='_blank'>");
                    arr.push(fileName);
                    arr.push("</a>&nbsp;<a class='dellink' href=\"javascript:delFile('");
                    arr.push(fileId);
                    arr.push("')\">[删除]</a></div>");
                }

                $(".oldfilecontent").append(arr.join(""));
            }
        }
        //用于必填验证
        function hasFiles(){
            return $(".filelink").length > 0 ? true : false;
        }
        //返回已经上传的附件名称数组
        function getFileNames() {
            var arr=[];
            $(".filelink").each(function(){
                arr.push($(this).text());
            });
            return arr;
        }
    </script>
</head>
<body style="background:white;" >
    <form id="form1" runat="server">
    <div id="maindiv" style="padding:5px;overflow:auto;">
	<div style="<%=read%>;text-align:left;">
		<input type="file" name="uploadify" id="uploadify" /> 
	    <input class="btn_sub" id="fromMyDoc" type="button" value="从我的文档选择" />
	    <input class="btn_sub" id="fromJZYWX" type="button" value="微信拍照上传" />
		<input class="btn_sub_DelRow hidden" type="button" value="上传文件" onclick="javascript:$('#uploadify').uploadifyUpload()" />
		<input class="btn_sub_DelRow hidden" type="button" value="全部取消" onclick="javascript:$('#uploadify').uploadifyClearQueue()" />
    </div>
    <div id="fileQueue" class="hidden"></div>
    <div class="oldfilecontent">
        <%=fileList %>
    </div>
    </div>
    </form>
</body>
</html>
