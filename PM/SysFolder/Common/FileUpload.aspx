<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FileUpload.aspx.cs" Inherits="EIS.Web.SysFolder.Common.FileUpload" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>上传文件</title>
    <link href="../../Css/AppStyle.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../../js/mootools.js"></script>
	<script type="text/javascript" src="../../js/Fx.ProgressBar.js"></script>
	<script type="text/javascript" src="../../js/Swiff.Uploader.js"></script>
	<script type="text/javascript" src="../../js/FancyUpload3.Attach.js"></script>
	<style type="text/css">

	a.hover {
	color: red;
    }

    #demo-list {
	    padding: 0;
	    list-style: none;
	    margin: 0;
    }

    #demo-list .file-invalid {
	    cursor: pointer;
	    color: #514721;
	    padding-left: 48px;
	    line-height: 24px;
	    background: url(assets/error.png) no-repeat 24px 5px;
	    margin-bottom: 1px;
    }
    #demo-list .file-invalid span {
	    background-color: #fff6bf;
	    padding: 1px;
    }

    #demo-list .file {
	    line-height: 2em;
	    padding-left: 22px;
	    background: url(assets/attach.png) no-repeat 1px 50%;
    }

    #demo-list .file span,
    #demo-list .file a {
	    padding: 0 4px;
    }

    #demo-list .file .file-size {
	    color: #666;
    }

    #demo-list .file .file-error {
	    color: #8a1f11;
    }

    #demo-list .file .file-progress {
	    width: 125px;
	    height: 12px;
	    vertical-align: middle;
	    background-image: url(assets/progress-bar/progress.gif);
    }
    #demo-attach,#demo-attach-2{text-decoration:none;}
	</style>
	<script type="text/javascript">
	    window.addEvent('domready', function () {
	        var thumb = "";
	        var foldername = escape("");
	        var up = new FancyUpload3.Attach('demo-list', '#demo-attach, #demo-attach-2', {
	            path: '../../js/Swiff.Uploader.swf',
	            url: '../../FancyUpload.axd?appName=<%=AppName %>&appId=<%=AppId %>&folderId=<%=folderId %>',
	            fileSizeMax: 10 * 1024 * 1024,

	            verbose: true,

	            onSelectFail: function (files) {
	                files.each(function (file) {
	                    new Element('li', {
	                        'class': 'file-invalid',
	                        events: {
	                            click: function () {
	                                this.destroy();
	                            }
	                        }
	                    }).adopt(
					new Element('span', { html: file.validationErrorMessage || file.validationError })
				).inject(this.list, 'bottom');
	                }, this);
	            },

	            onFileSuccess: function (file) {
	                new Element('input', { type: 'checkbox', 'checked': true }).inject(file.ui.element, 'top');
	                file.ui.element.highlight('#e6efc2');
	            },

	            onFileError: function (file) {
	                file.ui.cancel.set('html', '重试').removeEvents().addEvent('click', function () {
	                    file.requeue();
	                    return false;
	                });

	                new Element('span', {
	                    html: file.errorMessage,
	                    'class': 'file-error'
	                }).inject(file.ui.cancel, 'after');
	            },

	            onFileRequeue: function (file) {
	                file.ui.element.getElement('.file-error').destroy();

	                file.ui.cancel.set('html', '取消').removeEvents().addEvent('click', function () {
	                    file.remove();
	                    return false;
	                });

	                this.start();
	            }

	        });

	    });

	</script>
</head>
<body class="bgbody">
    <form id="form1" runat="server">
        <div class="maindiv">
            <br />&nbsp;&nbsp;
            <a href="#" id="demo-attach">【上传文件】</a>
            <a href="#" id="demo-attach-2" style="display: none;">【继续上传】</a>	
            <label class="error">（每个文件最大10M）</label>
            <ul id="demo-list"></ul>
        </div>
    </form>
</body>
</html>
