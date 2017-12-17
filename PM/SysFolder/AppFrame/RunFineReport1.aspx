<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RunFineReport1.aspx.cs" Inherits="EIS.Web.FineReport.SysFolder.AppFrame.RunFineReport1" %>

<!DOCTYPE html>


<html>
<head>
    <title>自动打印</title>
    <meta http-equiv="Content-Type" content="text/html; charset=GBK" />
    <script type="text/javascript" src="http://www.baidu.com:8080/WebReport/ReportServer?op=emb&resource=finereport.js"></script>
    <link rel="stylesheet" type="text/css" href="http://www.baidu.com:8080/WebReport/ReportServer?op=emb&resource=finereport.css" />
    <script type='text/javascript'>
        var printurl = "http://www.baidu.com:8080/WebReport/ReportServer";
        var reportlets = "[{reportlet: 'md%2fwz_rkd.cpt', mainid: '0cba5627-3083-4ea6-9f47-f56331969ddc'}, {reportlet: 'md%2fwz_rkd.cpt', mainid: '97a4ac56-203a-4573-83f6-7685cda9cc96'}]";
        var config = {
            url: printurl,
            isPopUp: false,
            data: {
                reportlets: reportlets
            }
        };

        function doURLFlashPrint() {
            FR.doURLFlashPrint(config);
        }
        function doAppletPrint() {
            FR.doURLAppletPrint(config);
        }
        function doURLPDFPrint() {
            FR.doURLPDFPrint(config);
        }

        window.onload = function () {
            //2秒钟后开始打印
            window.setTimeout(doAppletPrint, 2000);
        }

        function doPrint() {
            var url1 = "http://www.baidu.com:8080/WebReport/ReportServer?reportlet=md%2fwz_rkd.cpt&mainid=97a4ac56-203a-4573-83f6-7685cda9cc96";
            var config1 = { url: url1, isPopUp: true }
            FR.doURLAppletPrint(config1);
        }


    </script>
    <style>
        h2 {
            margin-left:30px;
        }
        h3 {
            margin-left:50px;
        }
        body {
            margin-top:30px;
        }
    </style>
</head>
<body>


    <h2>如果打印机没有接受到打印任务</h2>
    <h3>1、打印机未通电或未启动就绪</h3>
    <h3>2、打印插件被拦截，未开启</h3>
    <h3>3、设置本站点为受信任站点</h3>
    <h3>检查完毕后，请点击“打印”按钮。</h3>
    <p style="flex-align: center;margin-left:50px;">
        <input type="button" name="doprint" onclick="doURLFlashPrint()" value="打印(PDF)" />
        <input type="button" name="doprint" onclick="doAppletPrint()" value="打印(Applet)" />
        <input type="button" name="doprint" onclick="doURLPDFPrint()" value="打印(Flash)" />
        <input type="button" name="doprint" onclick="doPrint()" value="打印" />
    </p>

</body>
</html>
