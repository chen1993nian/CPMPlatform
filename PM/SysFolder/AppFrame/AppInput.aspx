<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AppInput.aspx.cs" ValidateRequest="false" Inherits="EIS.Web.SysFolder.AppFrame.AppInput" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>[<%=TblNameCn%>] 编辑记录</title>
    <meta http-equiv="Pragma" content="no-cache"/>
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
    <link type="text/css" rel="stylesheet" href="../../Css/kandytabs.css"  />
    <link type="text/css" rel="stylesheet" href="../../Css/appInput.css" />
    <link type="text/css" rel="stylesheet" href="../../Editor/kindeditor-4.1.10/themes/default/default.css" />
    <script type="text/javascript" src="../../js/jquery-1.7.min.js"></script>
    <script type="text/javascript" src="../../DatePicker/WdatePicker.js"></script>
    <script type="text/javascript" src="../../js/lhgdialog.min.js"></script>
    <script type="text/javascript" src="../../js/kandytabs.pack.js"></script>
    <script type="text/javascript" src="../../Editor/kindeditor-4.1.10/kindeditor-min.js"></script>
    <script type="text/javascript" src="../../Editor/kindeditor-4.1.10/lang/zh_CN.js"></script>
    <script type="text/javascript" src="../../js/jquery.notice.js"></script>
    <script type="text/javascript" src="../../js/jquery.smartMenu.js"></script>
    <script type="text/javascript" src="../../js/layer/layer.js"></script>
    <script src="../../Js/DateExt.js" type="text/javascript"></script>
    <%=customScript%>
</head>
<body>
    <form id="form1" runat="server">
    <!-- 工具栏 -->
    <div class="menubar">
        <div class="topnav">
            <span style="right:10px;display:inline;float:right;position:fixed;line-height:30px;top:0px;">
                <a class='linkbtn' href="javascript:" onclick="_appSaveAdd();" id="btnSaveAdd" name="btnSaveAdd">保存后新增</a>
				<em class="split">|</em>
                <a class='linkbtn' href="javascript:" onclick="_appSave();"  id="btnSave" name="btnSave">保存</a>
                <em class="split" style="<%=startWF %>">|</em>
                <a class='linkbtn' style="<%=startWF %>" href="javascript:" onclick="_appStartWF();"  id="btnStartWF" name="btnStartWF">发起流程</a>
				<em class="split">|</em>
                <a class='linkbtn' style="" href="javascript:" onclick="_appDirection();"  id="A1" name="btnDirection">填报说明</a>
				<em class="split">|</em>
                <a class='linkbtn' href="javascript:" onclick="_appClose();" id="btnClose" name="btnClose">关闭</a>
            </span>
        </div>
    </div>
    
    <div id="maindiv">
    <% =tblHTML%>
    </div>
    </form>
</body>
</html>
    <script type="text/javascript">

        jQuery(function(){
            if(!_isNew)
            {
                jQuery("#btnSaveAdd").prev().andSelf().hide();
            }
            $(window).resize(function(){
                $("#maindiv").height($(document.body).height()-75);
            });
            $("#maindiv").height($(document.body).height()-75);
        });

        function SaveSuccess(){
            $.noticeAdd({ text: '保存成功！',stayTime:500,onClose:function(){
                if(!!frameElement){
                    if(!!frameElement.lhgDG){
                        frameElement.lhgDG.curWin.app_query("<%=tblName %>");
                        frameElement.lhgDG.cancel();
                    }
                    else{
                        try { window.opener.app_query("<%=tblName %>"); } catch (e) {}
                        window.close();
                    }
                }
                else{
                    try { window.opener.app_query("<%=tblName %>"); } catch (e) {}
                    window.close();
                }
            }});
        }

        //保存数据
        function _appSave()
        {   
            //平台保存函数
            var ret = _sysSave();
            if (ret.error){
                if (ret.error.Type == "EIS.Web.SysFolder.AppFrame.AppInputAlertException"){
                    //警告性错误，提示后继续关闭
                    layer.alert(ret.error.Message, 
                        { title:"提醒", icon: 5 , closeBtn: 0, btn: ['关闭'] }, 
                        function(){
                            SaveSuccess();
                        }
                    );
                    return false;
                }
                else {
                    //数据保存错误
                    layer.alert(ret.error.Message, { icon: 5 });
                    return false;
                }
            }
            else if(ret)
            { 
                SaveSuccess();
            }
        }

        function app_query(tName){
            var listHtml = _curClass.GetListHtml("<%=tblName %>",tName,_mainId).value;
            $("#"+tName).before(listHtml).remove();
        }

        //保存数据,发起流程
        function _appStartWF()
        {   
            //平台保存函数
            if(_sysSave())
            { 
                var url = "";
                if("<%=workflowCode %>" == "")
                {
                    url = "tblName=<%=tblName %>&mainId=<%=mainId %>";
                    url = "../Workflow/SelectWorkFlow.aspx?para="+_curClass.CryptPara(url).value;
                }
                else
                {
                    url = "workflowCode=<%=workflowCode %>&mainId=<%=mainId %>";
                    url = "../workflow/NewFlow.aspx?para="+_curClass.CryptPara(url).value;
                }
                window.location.href=url;
            }
        }

        //查询填报说明
        function _appDirection() {
            var paraStr = "tblName=<%=tblName %>";
            var url = 'AppDirection.aspx?para='+_curClass.CryptPara(paraStr).value;
            var dlg = new $.dialog({ title: '[<%=TblNameCn%>] 填报说明', page: url
                , btnBar: true, cover: true, lockScroll: true, width: 800, height: 600, bgcolor: 'gray', cancelBtnTxt: '关闭',
                onCancel: function () {
                }
            });
            dlg.ShowDialog();
        }

        //保存数据，新增
        function _appSaveAdd()
        {   
            //平台保存函数
            if(_sysSave())
            {
                try { window.opener.app_query();} catch (e) {}
                window.location.href = window.location.href;
            }
        }
        //返回列表
        function _appBack(){
            window.history.back();
        }
        //关闭窗口
        function _appClose(){
            if(!!frameElement){
                if(!!frameElement.lhgDG)
                    frameElement.lhgDG.cancel();
                else
                    window.close();
            }
            else{
                window.close();
            }
        }
        var _curClass =EIS.Web.SysFolder.AppFrame.AppInput;
        var _isNew = <%=isNew %>;
        var _mainTblName = "<%=tblName %>";
        var _mainId = "<%=mainId %>";
        var _sIndex = "<%=sIndex %>";
        var _saveAction = "1";
        var _workflowCode ="";
        var _nodeCode ="";
        <%=this.sbmodel.ToString() %>;
        var _xmlData =jQuery(jQuery.parseXML('<xml><%= xmlData %></xml>'));
    </script>
    <script src="../../Js/SysFunction.js?v=<%=SysFuncEtag %>" type="text/javascript"></script>
    <script type="text/javascript">
        <%=editScriptBlock %>    
    </script>