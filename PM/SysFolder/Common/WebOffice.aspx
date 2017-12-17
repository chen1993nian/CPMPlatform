<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebOffice.aspx.cs" Inherits="EIS.Web.SysFolder.Common.WebOffice" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>文档编辑</title>
    <meta content="IE=7" http-equiv="X-UA-Compatible" />
    <link href="StyleSheet.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript" src="ntko.js?v=2014"></script>
</head>
<script language="javascript" type="text/javascript">
    function saveoffice() {
        saveFileToUrl();
    }
    function onload(filename, newofficetype) {
        resizeView();
        window.onresize = resizeView;
        editoffice(filename, newofficetype);
    }
    function resizeView() {
        try {
            document.getElementById("TANGER_OCX").style.height = document.documentElement.clientHeight;
        } catch (e) { }
    }
    function newControlStyle() {
        var myobj = TANGER_OCX_OBJ;
        var crt = document.getElementById("hiddenControl").value;
        if (crt == 1) {
            for (var menuPos = 1; menuPos <= 4; menuPos++) {
                if (menuPos == 1) {
                    myobj.AddCustomMenu2(menuPos, "打印控制" + "(&" + menuPos + ")");
                    for (var submenuPos = 0; submenuPos < 4; submenuPos++) {
                        if (0 == submenuPos)//增加菜单项目
                            myobj.AddCustomMenuItem2(menuPos, submenuPos, -1, false, "允许打印", false);
                        if (1 == submenuPos)//增加菜单项目
                            myobj.AddCustomMenuItem2(menuPos, submenuPos, -1, false, "禁止打印", false);
                        if (2 == submenuPos)//增加菜单项目
                            myobj.AddCustomMenuItem2(menuPos, submenuPos, -1, false, "页面设置", false);
                        if (3 == submenuPos)//增加菜单项目
                            myobj.AddCustomMenuItem2(menuPos, submenuPos, -1, false, "打印预览", false);
                    }
                }
                if (menuPos == 2) {
                    myobj.AddCustomMenu2(menuPos, "印章管理" + "(&" + menuPos + ")");
                    var yzstr = document.getElementById("hiddenYz").value;
                    if (yzstr != "" && yzstr != null) {
                        var sp1 = yzstr.split("$");
                        if (sp1.length > 1) {
                            var count = sp1[0];
                            var str2 = sp1[1];
                            var sp2 = str2.split("|");
                            for (var submenuPos = 0; submenuPos < sp2.length; submenuPos++) {
                                var str3 = sp2[submenuPos];
                                var sp3 = str3.split(",");
                                myobj.AddCustomMenuItem2(menuPos, submenuPos, -1, false, sp3[0], false);

                            }
                        }
                    }
                    else {
                        myobj.AddCustomMenuItem2(menuPos, submenuPos, -1, false, "无", false);
                    }
                }
                if (menuPos == 3) {
                    myobj.AddCustomMenu2(menuPos, "套红模板" + "(&" + menuPos + ")");
                    for (var submenuPos = 0; submenuPos < 2; submenuPos++) {
                        if (0 == submenuPos)//增加菜单项目
                        {
                            myobj.AddCustomMenuItem2(menuPos, submenuPos, -1, true, "选择套红", false);
                            var thstr = document.getElementById("hiddenTh").value;

                            //			        //增加子菜单项目
                            if (thstr != "" && thstr != null) {
                                var sp1 = thstr.split("$");
                                if (sp1.length > 1) {
                                    var count = sp1[0];
                                    var str2 = sp1[1];
                                    var sp2 = str2.split("|");
                                    var newsubsubmenuPos = 0;
                                    for (var i = 0; i < sp2.length; i++) {
                                        var str3 = sp2[i];
                                        var sp3 = str3.split(",");
                                        myobj.AddCustomMenuItem2(menuPos, submenuPos, newsubsubmenuPos, false,
										sp3[0], false, menuPos * 100 * submenuPos * 20 + newsubsubmenuPos);
                                        newsubsubmenuPos++;
                                    }
                                }
                            }
                            else {
                                myobj.AddCustomMenuItem2(menuPos, submenuPos, newsubsubmenuPos, false,
			                    "无", false, menuPos * 100 * submenuPos * 20 + newsubsubmenuPos);
                            }
                        }
                        if (1 == submenuPos)//增加菜单项目
                        {
                            myobj.AddCustomMenuItem2(menuPos, submenuPos, -1, true, "选择模板", false);
                            var mbstr = document.getElementById("hiddenMb").value;

                            //增加子菜单项目
                            if (mbstr != "" && mbstr != null) {
                                var sp1 = mbstr.split("$");
                                if (sp1.length > 1) {
                                    var count = sp1[0];
                                    var str2 = sp1[1];
                                    var sp2 = str2.split("|");
                                    var newsubsubmenuPos = 0;
                                    for (var i = 0; i < sp2.length; i++) {
                                        var str3 = sp2[i];
                                        var sp3 = str3.split(",");
                                        myobj.AddCustomMenuItem2(menuPos, submenuPos, newsubsubmenuPos, false,
			                        sp3[0], false, menuPos * 100 * submenuPos * 20 + newsubsubmenuPos);
                                        newsubsubmenuPos++;
                                    }
                                }
                            }
                            else {
                                myobj.AddCustomMenuItem2(menuPos, submenuPos, newsubsubmenuPos, false,
			                    "无", false, menuPos * 100 * submenuPos * 20 + newsubsubmenuPos);
                            }
                        }

                    }
                }
                if (menuPos == 4) {
                    myobj.AddCustomMenu2(menuPos, "痕迹保留功能" + "(&" + menuPos + ")");
                    for (var submenuPos = 0; submenuPos < 2; submenuPos++) {

                        if (0 == submenuPos)//增加菜单项目
                            myobj.AddCustomMenuItem2(menuPos, submenuPos, -1, false, "显示痕迹", false);
                        if (1 == submenuPos)//增加菜单项目
                            myobj.AddCustomMenuItem2(menuPos, submenuPos, -1, false, "隐藏痕迹", false);
                    }
                }
            }
        }
        var mburl = document.getElementById("hiddenMbUrl").value;
        var mbload = document.getElementById("hiddenMbLoad").value;
        if (mburl != "" && mburl != null) {
            if (mbload != "1") {
                document.getElementById("hiddenMbLoad").value = 1;
                openTemplateFileFromUrl(mburl);
            }
        }
    }
</script>
<body onload='onload("<%=filename %>","<%=newofficetype %>");' onunload ="onPageClose();" scroll="yes">
    <form id="form1" action="WeOfficeSave.aspx" enctype="multipart/form-data">
    <div id="editmain" style="display:none;">       
    <div id="officecontrol">
        <input type="hidden" value="<%=fileid %>" name="fileid" id="fileid"/>
        <input type="hidden" value="<%=foldername %>" name="foldername" id="foldername"/>
        <input type="hidden" id="hiddenTh" runat="server" />
        <input type="hidden" id="hiddenYz" runat="server" />
        <input type="hidden" id="hiddenYzfl" runat="server" />
        <input type="hidden" id="hiddenMb" runat="server" />
        <input type="hidden" id="hiddenReadonly" runat="server" />
        <input type="hidden" id="hiddenFullScrn" runat="server" />
        <input type="hidden" id="hiddenPrint" runat="server" />
        <input type="hidden" id="hiddenMark" runat="server" />
        <input type="hidden" id="hiddenCopy" runat="server" />
        <input type="hidden" id="hiddenFilenew" runat="server" />
        <input type="hidden" id="hiddenSaveas" runat="server" />
        <input type="hidden" id="hiddenControl" runat="server" />
        <input type="hidden" id="hiddenMbUrl" runat="server" />
        <input type="hidden" id="hiddenMbLoad" runat="server" />
        <input type="hidden" id="hiddenUserName" value="<%=base.EmployeeName %>" />
        <input type="hidden" value="<%=filename %>" name="filename" />
        <input type="hidden" value="" id="jstxt" runat="server"/>
        <!--控件事件代码开始-->
        <script type="text/javascript"  for="TANGER_OCX" event="OnFileCommand(cmd,canceled);">
            alert(cmd);
            CancelLastCommand = true;
        </script>
        <script type="text/javascript" for="TANGER_OCX" event="OnDocumentClosed();">
            setFileOpenedOrClosed(false);
        </script>
        <script type="text/javascript" for="TANGER_OCX" event="OnDocumentOpened(TANGER_OCX_str,TANGER_OCX_obj);">
            setFileOpenedOrClosed(true);//设置文档状态值
            newControlStyle();//插入控件样式自定义菜单

            with (TANGER_OCX_OBJ.ActiveDocument)//设置修订显示模式
            {

                TANGER_OCX_OBJ.ActiveDocument.CommandBars("Reviewing").Enabled = false;
                TANGER_OCX_OBJ.ActiveDocument.CommandBars("Track Changes").Enabled = false;

            }

            with (TANGER_OCX_OBJ.ActiveDocument.Application)//设置文档所有者名称
            {
                UserName = document.getElementById("hiddenUserName").value;
            }

            var mark = document.getElementById("hiddenMark").value;
            if (mark == 0) {
                SetReviewMode(false);//设置文档不在痕迹模式下编辑
                setShowRevisions(false);
            }
            else {
                SetReviewMode(true);//设置文档在痕迹模式下编辑
                setShowRevisions(true);
            }

            var saveas = document.getElementById("hiddenSaveas").value;
            if (saveas == 1) {
                setFileSaveAs(true);
            }
            else {
                setFileSaveAs(false);
            }

            var filenew = document.getElementById("hiddenFilenew").value;
            if (filenew == 1) {
                setFileNew(true);
            }
            else {
                setFileNew(false);
            }

            var copy = document.getElementById("hiddenCopy").value;
            if (copy == 1) {
                setIsNoCopy(false);
            }
            else {
                setIsNoCopy(true);
            }
            var readonly = document.getElementById("hiddenReadonly").value;
            if (readonly == 0) {
                TANGER_OCX_OBJ.SetReadOnly(false);
            }
            else {
                TANGER_OCX_OBJ.IsShowToolMenu = "-1";
                TANGER_OCX_OBJ.Toolbars = "0";
                //TANGER_OCX_OBJ.Menubar="0";
                TANGER_OCX_OBJ.SetReadOnly(true);
            }

            var print = document.getElementById("hiddenPrint").value;
            if (print == 1) {
                setFilePrint(true);
            }
            else {
                setFilePrint(false);
            }

            var fullscrn = document.getElementById("hiddenFullScrn").value;
            if (fullscrn == 1) {
                TANGER_OCX_OBJ.FullScreenMode = true;
            }
            else {
                TANGER_OCX_OBJ.FullScreenMode = false;
            }

            //saved属性用来判断文档是否被修改过,文档打开的时候设置成ture,当文档被修改,自动被设置为false,该属性由office提供.	
            TANGER_OCX_OBJ.ActiveDocument.Saved = true;

            if (2 == TANGER_OCX_OBJ.DocType) {
                try {
                    TANGER_OCX_OBJ.ActiveDocument.Application.ActiveWorkbook.Saved = true;
                } catch (e) {
                    alert("错误：" + err.number + ":" + err.description);
                }
            }
        </script>
        <script  type="text/javascript"  for="TANGER_OCX" event="BeforeOriginalMenuCommand(TANGER_OCX_str,TANGER_OCX_obj);">
            alert("BeforeOriginalMenuCommand事件被触发");
        </script>
        <script type="text/javascript" for="TANGER_OCX" event="OnFileCommand(TANGER_OCX_str,TANGER_OCX_obj);">
            var readonly = document.getElementById("hiddenReadonly").value;
            if (TANGER_OCX_str == 3) {
                if (readonly == 1) {
                    alert("文档属性为只读,不能保存!");
                }
                else {
                    saveoffice();
                }
                CancelLastCommand = true;
            }
        </script>
        <script type="text/javascript" for="TANGER_OCX" event="OnCustomMenuCmd2(menuPos,submenuPos,subsubmenuPos,menuCaption,menuID)">
            if ("全网页查看" == menuCaption) objside();
            if ("恢复原大小" == menuCaption) objside2();
            if ("允许打印" == menuCaption) setFilePrint(true);
            if ("禁止打印" == menuCaption) setFilePrint(false);
            if ("页面设置" == menuCaption) TANGER_OCX_OBJ.showDialog(5);
            if ("打印预览" == menuCaption) TANGER_OCX_OBJ.PrintPreview();

            if ("禁止编辑" == menuCaption) TANGER_OCX_OBJ.SetReadOnly(true);
            if ("允许编辑" == menuCaption) TANGER_OCX_OBJ.SetReadOnly(false);
            var tblname = document.getElementById("FolderName").value;
            //			 debugger;      


            var yzstr = document.getElementById("hiddenYz").value;

            if (yzstr != "" && yzstr != null) {
                var sp1 = yzstr.split("$");
                var count = sp1[0];
                var str2 = sp1[1];
                var sp2 = str2.split("|");
                var newsubsubmenuPos = 0;
                for (var i = 0; i < sp2.length; i++) {

                    var str3 = sp2[i];
                    var sp3 = str3.split(",");
                    if (sp3[0] == menuCaption) {
                        addServerSign(sp3[1]);

                    }
                }
            }
            var thstr = document.getElementById("hiddenTh").value;
            if (thstr != "" && thstr != null) {
                var sp1 = thstr.split("$");
                var count = sp1[0];
                var str2 = sp1[1];
                var sp2 = str2.split("|");
                var newsubsubmenuPos = 0;
                for (var i = 0; i < sp2.length; i++) {
                    var str3 = sp2[i];
                    var sp3 = str3.split(",");
                    if (sp3[0] == menuCaption) {

                        //var text = TANGER_OCX_OBJ.ActiveDocument.Content.text;
                        var curSel = TANGER_OCX_OBJ.ActiveDocument.Application.Selection;
                        //TANGER_OCX_SetMarkModify(false);
                        curSel.WholeStory();
                        curSel.Cut();

                        try {
                            //TANGER_OCX_OBJ.CreateNew("word.document");
                            TANGER_OCX_OBJ.ActiveDocument.Content.Text = "";
                        }
                        catch (e) {
                            alert(e);
                        };
                        //传入fileId
                        openTemplateFileFromUrl(sp3[1], false);
                        //加载表单对应书签值
                        var mark = document.getElementById("jstxt").value;
                        if (mark != "" && mark != null) {
                            var m = mark.split("|");
                            for (var i = 0; i < m.length; i++) {
                                var m1 = m[i];
                                if (m1 != "" && m1 != null) {
                                    //setBookMark(m1);
                                    setBookMarkValues(m1);
                                }
                            }
                        }

                        try {
                            var bkmkObj = TANGER_OCX_OBJ.ActiveDocument.BookMarks("正文");
                            var saverange = bkmkObj.Range;
                            saverange.Paste();
                            TANGER_OCX_OBJ.SetBookmarkValue("正文", saverange);
                            //TANGER_OCX_SetMarkModify(true);		
                            //var BK = TANGER_OCX_OBJ.ActiveDocument.Bookmarks; 
                            //if (BK.Exists("正文")) {
                            //TANGER_OCX_OBJ.SetBookmarkValue("正文", text);
                            //}
                        }
                        catch (e) {
                            //alert("Word 模板中不存在名称为：\"" + markname +"\"的书签！");
                        }
                    }
                }
            }

            var mbstr = document.getElementById("hiddenMb").value;
            if (mbstr != "" && mbstr != null) {
                var sp1 = mbstr.split("$");
                if (sp1.length > 1) {
                    var count = sp1[0];
                    var str2 = sp1[1];
                    var sp2 = str2.split("|");
                    var newsubsubmenuPos = 0;
                    for (var i = 0; i < sp2.length; i++) {
                        var str3 = sp2[i];
                        var sp3 = str3.split(",");
                        if (sp3[0] == menuCaption) openTemplateFileFromUrl(sp3[1]);
                    }
                }
            }



            if ("保留痕迹" == menuCaption) SetReviewMode(true);
            if ("取消留痕" == menuCaption) SetReviewMode(false);
            if ("显示痕迹" == menuCaption) setShowRevisions(true);
            if ("隐藏痕迹" == menuCaption) setShowRevisions(false);
	    </script>
	    <script type="text/javascript">
	        function setBookMark(mark) {
	            var m1 = mark.split("$");
	            var markname = m1[0];
	            var tblname = m1[1];
	            var fieldName = m1[2];

	            var inputValue = window.parent._getCtlValueByFieldName(fieldName);

	            try {
	                var bkmkObj = TANGER_OCX_OBJ.ActiveDocument.BookMarks(markname);
	                var saverange = bkmkObj.Range
	                saverange.Text = inputValue;
	                TANGER_OCX_OBJ.ActiveDocument.Bookmarks.Add(markname, saverange);
	            }
	            catch (e) {
	                //alert("Word 模板中不存在名称为：\"" + markname +"\"的书签！");
	            }

	        }

	        function setBookMarkValues(mark) {
	            if (TANGER_OCX_OBJ) {
	                var m1 = mark.split("$");
	                var markname = m1[0];
	                var tblname = m1[1];
	                var fieldName = m1[2];
	                var inputValue = "";
	                if (!!window.parent["_sys"]) {
	                    var _sys = window.parent._sys;
	                    var field = _sys.getField(fieldName);
	                    if (field.dispstyle == "023") {
	                        var fileNames = window.parent._getFileNames(fieldName);
	                        var arr = [];
	                        for (var i = 1; i <= fileNames.length; i++) {
	                            arr.push(i + "." + fileNames[i - 1]);
	                        }
	                        inputValue = arr.join("\r\n");
	                    }
	                    else {
	                        inputValue = _sys.getValue(fieldName);
	                    }
	                }
	                else {
	                    inputValue = window.parent._getCtlValueByFieldName(fieldName);
	                }
	                //alert(markname+"="+inputValue);
	                //获取书签集合对象
	                var BK = TANGER_OCX_OBJ.ActiveDocument.Bookmarks;
	                if (BK.Exists(markname)) {
	                    TANGER_OCX_OBJ.SetBookmarkValue(markname, inputValue);
	                }
	            }
	        }
        </script>
        <script type="text/javascript" for="TANGER_OCX" event="AfterPublishAsPDFToURL(result,code);">
            result = trim(result);
            if (result == "succeed")
            { window.close(); }
        </script>
        <!--控件事件代码结束-->
    </div>
    </div>
    <div>
        <object id="TANGER_OCX" classid="clsid:C9BC4DFF-4248-4a3c-8A49-63A7D317F404"    
            codebase="officecontrol.cab#version=5,0,1,0" width="100%" height="800px;">   
            <param name="BorderStyle" value="1"/>   
            <param name="BorderColor" value="14402205"/>   
            <param name="TitlebarColor" value="15658734"/>   
            <param name="TitlebarTextColor" value="0"/>   
            <param name="Caption" value="文档编辑"/>   
            <param name="IsUseUTF8URL" value="-1"/>   
            <param name="IsUseUTF8DATA" value="-1"/>
            <param name="Titlebar" value="0"/>
            <!--
            <param name="IsShowToolMenu" value="-1"/>
            <param name="Toolbars" value="0"/>
            <param name="Menubar" value="0"/>
            -->
            <param name="MaxUploadSize" value="10000000"/>   
            <param name="MenubarColor" value="14402205"/>   
            <param name="MenuButtonColor" value="16180947"/>   
            <param name="MenuBarStyle" value="3"/>   
            <param name="MenuButtonStyle" value="7"/>   
            <param name="ProductCaption" value="浪潮办公自动化系统"/> 
            <param name="ProductKey" value="B8DF5870DF8F2D736F6B256FC1E380F8F1E16815"/>  
            <param name="MakerCaption" value="浪潮集团山东通用软件有限公司"/>  
            <param name="MakerKey" value="0575AB35368BDFD7F0B9F945361657F7674632CB"/>  
            <span style="color:red">不能装载文档控件。请在检查浏览器的选项中检查浏览器的安全设置。</span>   
        </object>
    </div> 
    </form>
</body>
</html>

