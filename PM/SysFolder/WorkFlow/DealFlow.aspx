<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DealFlow.aspx.cs" Inherits="EIS.Web.SysFolder.WorkFlow.DealFlow" %>

<!DOCTYPE html>


<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title><%=curInstance.InstanceName %></title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
    <meta http-equiv="Pragma" content="no-cache" />
    <link type="text/css" rel="stylesheet" href="../../Css/kandytabs.css" />
    <link rel="stylesheet" type="text/css" href="../../Css/wfStyle.css" />
    <link type="text/css" rel="stylesheet" href="../../Editor/kindeditor-4.1.10/themes/default/default.css" />
    <script type="text/javascript" src="../../js/jquery-1.7.min.js"></script>
    <script type="text/javascript" src="../../DatePicker/WdatePicker.js"></script>
    <script type="text/javascript" src="../../js/kandytabs.pack.js"></script>
    <script type="text/javascript" src="../../js/lhgdialog.min.js"></script>
    <script type="text/javascript" src="../../js/jquery.notice.js"></script>
    <script type="text/javascript" src="../../Editor/kindeditor-4.1.10/kindeditor-min.js"></script>
    <script type="text/javascript" src="../../Editor/kindeditor-4.1.10/lang/zh_CN.js"></script>
    <script type="text/javascript" src="../../js/layer/layer.js"></script>
    <script src="../../Js/DateExt.js" type="text/javascript"></script>
    <%=customScript%>
    <style type="text/css">
        html, body {
            overflow: hidden;
            margin: 0px;
        }
    </style>
    <script>
        var _IsAllowSave = false;
        function CheckSaveRuler(){
            return(_IsAllowSave);
        }
    </script>
</head>
<body>
    <!-- 工具栏 -->
    <div class="menubar">
        <div class="topnav">
            <span style="padding-left: 10px;">当前处理步骤：<%=curTask.TaskName %></span>
            <span style="right: 10px; display: inline; float: right; position: fixed; line-height: 30px; top: 0px;">
                <a class="linkbtn linknote" href="NewBefore.aspx?view=1&workflowid=<%=curInstance.WorkflowId %>" target="_blank">审批须知</a>
                <em class="split">|</em>
                <a class="linkbtn linkchart" href="InstanceChart.aspx?instanceId=<%=curInstance.InstanceId%>" target="_blank">查看流程图</a>
                <em class="split">|</em>
                <a class="linkbtn linkprint" href="../AppFrame/AppWorkFlowPrint.aspx?instanceId=<%=curInstance.InstanceId%>" target="_blank">打印</a>
                <em class="split">|</em>
                <a class="linkbtn toggleTable" href="javascript:">隐藏表单</a>
                <em class="split">|</em>
                <a class="linkbtn linkclose" href="javascript:">关闭</a>
            </span>


        </div>
    </div>

    <div id="maindiv">

        <div class="wfpage">

            <div style="width: 760px; margin-left: auto; margin-right: auto;">
                <div style="font-family: 微软雅黑,黑体; font-weight: bold; font-size: 12pt; line-height: 200%;">
                    <%=curInstance.InstanceName%>
                    <span style="color: red"><%=importance %></span>
                </div>
                <div style="color: #999; text-align: center;">
                    流程名称：<%=wokflowName%> &nbsp;
			发起人：<%=curInstance.EmployeeName%>（<%=curInstance._CreateTime.ToString("yyyy-MM-dd HH:mm")%>）
                </div>
            </div>

            <br />
            <div id="tblHTML">
                <% =tblHTML%>
                <div class="relationpanel">
                    <ul class="tabs">
                        <li class="selected"><a href="#tabpage1">
                            <button id="btnRelation" type="button" title="添加参考流程" style="margin: 0px; border-width: 0px; height: 20px; padding: 2px; background: transparent; cursor: hand;">
                                <img alt="添加参考流程" style="vertical-align: middle; cursor: hand;" src="../../img/common/add_small.png" />添加参考流程
                            </button>
                        </a>
                        </li>
                    </ul>
                    <div id="tabControl" style="width: 780px; margin-left: auto; margin-right: auto; text-align: left;">
                        <div class="refList">
                            <%=GetInstanceRefers() %>
                        </div>
                    </div>
                </div>
            </div>
            <form id="form1" runat="server" onsubmit="return(CheckSaveRuler());">

                <div class="wfdealinfo">
                    <wf:UserDealInfo ID="UserDealInfo" runat="server"></wf:UserDealInfo>
                </div>

                <div class="wfdealinfo" style="text-align: left;">
                    <table width="100%">
                        <tr class="DealFlowTextArea">
                            <td height="20">
                                <table width="100%">
                                    <tr>
                                        <td>
                                            <h5 style="font-size: 10pt;">处理意见：</h5>
                                        </td>
                                        <td width="530" id="tdTmpl"><%=SuggestTmpl %></td>
                                        <td style="background: #eeede9; padding: 2px 5px; text-align: right;">
                                            <a href="javascript:;" class="linkbtn linkidea" title="编辑意见模板">意见模板</a>
                                            <a href="javascript:;" class="linkbtn linkattach" title="添加意见附件">意见附件</a>
                                        </td>
                                    </tr>
                                </table>

                            </td>
                        </tr>
                        <tr class="DealFlowTextArea">
                            <td>
                                <asp:TextBox CssClass="TextBoxInArea" TextMode="MultiLine" Rows="3" ID="txtRemark" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="padding: 10px 0px;">
                                <span style="font-weight: bold; font-size: 10pt; float: left; margin-top: 4px;">下一步处理人：</span>
                                <div style="float: left;" class="nextUserZone">
                                    <%=NextActivityList%>
                                </div>
                            </td>
                        </tr>
                        <tr style="<%=stepinfo%>">
                            <td style="padding: 10px 0px;">
                                <div style="font-weight: bold; font-size: 10pt; float: left;">本步骤处理情况：</div>
                                <div style="float: left;">
                                    <%=CurStepInfo%>
                                </div>
                            </td>
                        </tr>
                    </table>
                    <div class="authPanel <%=ReAuthClass %>" style="margin: 2px; line-height: 30px;">
                        <span style="font-weight: bold; font-size: 10pt; float: left; margin-top: 5px; margin-right: 6px;">二次认证方式：</span>
                        <select id="selReAuthType" style="height: 24px;">
                            <option value="1">登录密码</option>
                            <option value="2">短信验证码</option>
                        </select>&nbsp;&nbsp;
                <label id="labelPass">密码：</label><input id="reAuthPass" type="text" title="输入后回车即可验证" style="border: 1px solid #bbb; padding: 2px; width: 100px;" />
                        <input id="btnFetchCode" type="button" value="获取验证码" class="hidden" style="height: 26px; background: #eee none;" />
                        <span id="checkInfo" style="vertical-align: middle; font-weight: bold; font-size: 10pt; color: Green">输入后回车即可验证</span>
                    </div>
                    <%=DealInfo %>
                    <div style="margin-top: 15px; border: 1px solid #ddd; padding: 2px; text-align: right; height: 30px; line-height: 30px; background: #fbfbfb;">
                        <span style="float: left;">
                            <asp:Button ID="btnSubmit" CssClass="btn01" runat="server" Text="提 交" OnClientClick="return(btnSubmitClick());" OnClick="btnSubmit_Click" />
                            <asp:Button ID="btnAgree" CssClass="btn01" runat="server" Text="同 意" OnClientClick="return(btnAgreeClick());" OnClick="btnAgree_Click" />
                            <asp:Button ID="btnReject" CssClass="btn01" runat="server" Text="不同意" OnClientClick="return(btnRejectClick());" OnClick="btnReject_Click" />
                            <asp:Button ID="btnShareTask" CssClass="btn01" runat="server" Text="退 还" OnClick="btnShareTask_Click" />
                            <button type="button" id="btnRollBack" class="btn01 <%=RollBackClass %>">退 回</button>
                            <button type="button" id="btnDirect" class="btn01 <%=DirectClass %>">直 送</button>
                            <button type="button" id="btnAssign" class="btn01 <%=AssignClass %>">加 签</button>
                            <button type="button" id="btnRelegate" class="btn01 <%=RelegateClass %>">委 托</button>
                            <asp:Button ID="btnHangUp" CssClass="btn01" runat="server" Text="挂 起" OnClick="btnHangUp_Click" />
                            <asp:Button ID="btnResume" CssClass="btn01" runat="server" Text="恢 复" OnClick="btnResume_Click" />
                            <asp:Button ID="btnStop" CssClass="btn01" runat="server" Text="终 止" OnClick="btnStop_Click" />
                            <button type="button" id="btnShareTo" class="btn01" onclick="javascript:wf_share();">内部传阅</button>
                            <button type="button" id="btnLimit" class="btn01 <%=LimitClass %>" onclick="javascript:wf_limit();">公开范围</button>
                            <button type="button" id="btnTempSave" class="btn01" onclick="_appSave();">暂 存</button>
                        </span>
                        <span style="font: bold 10pt/26px; color: #666">请选择岗位：</span><asp:DropDownList ID="PositionList" runat="server" Width="260" AutoPostBack="True"
                            OnSelectedIndexChanged="PositionList_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                </div>

            </form>

        </div>

    </div>

</body>
</html>
<script type="text/javascript">

        //告知
        function wf_share()
        {
            var reason=document.getElementById("txtRemark").value;
            window.open("FlowShareTo.aspx?taskId=<%=taskId %>&reason="+escape(reason),"_self");
        }
        //保存
        function _appSave()
        {
            _saveAction = "2";
            var ret = _sysSave();
            if (ret.error){
                if (ret.error.Type == "EIS.Web.SysFolder.AppFrame.AppInputAlertException"){
                    //警告性错误，提示后继续关闭
                    layer.alert(ret.error.Message, 
                        { title:"提醒", icon: 5 , closeBtn: 0, btn: ['关闭'] }, 
                        function(){
                            _IsAllowSave = true;
                            return true;
                        }
                    );
                    return true;
                }
                else {
                    //数据保存错误
                    _IsAllowSave = false;
                    layer.alert(ret.error.Message, { icon: 5 });
                    return false;
                }
            }
            else if(ret)
            {
                _IsAllowSave = true;
                alert("保存成功!");
                window.close();
            }
        }
        var tempUser="",tempPos="";
        function seluser(nodeCode)
        {
            var pid="pid"+nodeCode;
            var pname="pname"+nodeCode;
            var ppos="ppos"+nodeCode;

            tempUser=$("#"+pid).val();
            tempPos=$("#"+ppos).val();

            var url="../Common/UserTree.aspx?method=1&callback=onUserChange&queryfield=empid,empname,posid&cid="+pid+","+pname+","+ppos;
            _openCenter(url,"_blank",640,500);
        }
        function onUserChange(cidArr)
        {
            var newVal = $("#"+cidArr[0]).val();
            var newPos = $("#"+cidArr[2]).val();
            if(newVal.length == 0)
            {
                $("#"+cidArr[0]).val(tempUser);
                $("#"+cidArr[2]).val(tempPos);
                return;
            }
            //如果选择了处理人
            if(tempUser.length>0)
            {
                newVal=tempUser+","+newVal;
                newPos=tempPos+","+newPos;
            }
            $("#"+cidArr[0]).val(newVal);
            $("#"+cidArr[2]).val(newPos);

            var nodeId=cidArr[0].substring(3);
            var arr=[];
            var names=$("#"+cidArr[1]).val().split(",");
            for(var i=0;i<names.length;i++)
            {
                arr.push("<span class='performer' title='点击删除' actId='"+nodeId+"'>"+names[i]+"<img  src='../../img/common/close.png'></span>");
            }
            $("#userPanel"+nodeId).append(arr.join(""));
            var arrFlag = $("#tranact"+nodeId).val().split("|");
            arrFlag[2]="1";
            $("#tranact"+nodeId).val(arrFlag.join("|"));
        }
        function ReLoadIdea(){
            var ideaList=_curClass.GetSuggestTmpl().value;
            var arrHtml=[];
            if(ideaList.length>0){
                var arrIdea=ideaList.split("|");
                for (var i = 0; i < arrIdea.length; i++) {
                    arrHtml.push("<a class='tmplitem' href='javascript:'>"+arrIdea[i]+"</a>");    
                }
            }
            $("#tdTmpl").html(arrHtml.join(""));
        }


        function btnSubmitClick(){
            if(!confirm("您确定选择【提 交】吗？"))
                return false;
            _saveAction = "1";
            if(_saveBeforeSubmit)
            {
                var ret = _sysSave();
                if (ret.error){
                    if (ret.error.Type == "EIS.Web.SysFolder.AppFrame.AppInputAlertException"){
                        _IsAllowSave = true;
                        //警告性错误，提示后继续
                        alert(ret.error.Message);
                        return true;
                    }
                    else {
                        //数据保存错误
                        _IsAllowSave = false;
                        layer.alert(ret.error.Message, { icon: 5 });
                        return false;
                    }
                }
                _IsAllowSave = ret;
                return(_IsAllowSave);
            }
            else
                return true;
        };

        function btnAgreeClick(){
            if(!confirm("您确定选择【同 意】吗？"))
                return false;
            _saveAction = "1";
            if(_saveBeforeSubmit)
            {
                var ret = _sysSave();
                if (ret.error){
                    if (ret.error.Type == "EIS.Web.SysFolder.AppFrame.AppInputAlertException"){
                        _IsAllowSave = true;
                        //警告性错误，提示后继续
                        alert(ret.error.Message);
                        return true;
                    }
                    else {
                        //数据保存错误
                        _IsAllowSave = false;
                        layer.alert(ret.error.Message, { icon: 5 });
                        return false;
                    }
                }
                _IsAllowSave = ret;
                return(_IsAllowSave);
            }
            else
                return true;
        };

        function btnRejectClick(){
            if(!confirm("您确定选择【不同意】吗？"))
                return false;
            var reason=$("#txtRemark").val();
            if(reason == "")
            {
                layer.alert("【不同意】操作必须填写意见", {icon: 5});
                return false;
            }
            _saveAction = "r1";
            var ret = _sysSave();
            if (ret.error){
                alert(ret.error.Message);
            }
            //不管校验是否通过，都允许不同意退回。
            _IsAllowSave = true;
            return(_IsAllowSave);
        };

       

    jQuery(function($){
        if(!_saveBeforeSubmit)
            $("#btnTempSave").hide();
        if("<%=ReAuthClass %>"==""){
                $("#btnSubmit,#btnAgree,#btnReject,#btnRollBack,#btnDirect").attr("disabled",true);
            }
            $(window).resize(function(){
                $(".wfpage").height($(document.body).height()-60);
            });
            $(".wfpage").height($(document.body).height()-60);

            $(".btn01").hover(function(){
                this.className="btn02";
            },function(){
                this.className="btn01";
            });
            $("#btnRelation").click(function(){
                _openCenter("flowRelation.aspx","_blank",800,600);
            });
            $(".performer").live("click",function(){
                var actId = $(this).attr("actId");
                var index = $(this).index();
                var arr = $("#pid"+actId).val().split(",");
                var arr2 = $("#ppos"+actId).val().split(",");
                arr.splice(index,1);
                arr2.splice(index,1);
                $("#pid"+actId).val(arr.join(","));
                $("#ppos"+actId).val(arr2.join(","));
                $(this).remove();
                //更新处理人标志
                var arrFlag = $("#tranact"+actId).val().split("|");
                arrFlag[2]="1";
                $("#tranact"+actId).val(arrFlag.join("|"));

            });

            //回退
            $("#btnRollBack").click(function(){
                if(!confirm("您确定选择【"+event.srcElement.innerText+"】吗？"))
                    return;
                var reason=$("#txtRemark").val();
                if(reason=="")
                {
                    layer.alert("【退回】操作必须填写意见", {icon: 5});
                    return;
                }
                _saveAction = "b1";
                var ret = _sysSave();
                if (ret.error){
                    alert(ret.error.Message);
                }
                //不管校验是否通过，都允许退回。
                _IsAllowSave = true;
                if(_IsAllowSave)
                {
                    window.open("FlowTaskRollBack.aspx?taskId=<%=taskId %>&reason="+escape(reason),"_self");
                }
            });

            //直送
            $("#btnDirect").click(function(){
                //_openCenter("FlowTaskDirect.aspx?taskId=<%=taskId %>","_blank",500,500);
                var reason=document.getElementById("txtRemark").value;

                if(!confirm("您确定选择【直送】吗？"))
                    return;
                if(reason=="")
                {
                    layer.alert("【直送】操作必须填写意见", {icon: 5});
                    return;
                }

                window.open("FlowTaskDirect.aspx?taskId=<%=taskId %>&reason="+escape(reason),"_self");
            });
            //加签
            $("#btnAssign").click(function(){
                var reason=document.getElementById("txtRemark").value;
                window.open("FlowAssign.aspx?taskId=<%=taskId %>&reason="+escape(reason),"_self");
            });
            //委托
            $("#btnRelegate").click(function(){
                var reason=document.getElementById("txtRemark").value;
                window.open("FlowDelegate.aspx?taskId=<%=taskId %>&reason="+escape(reason),"_self");
            });
            //同意时不能填写处理意见，不同意一定要填写意见
            $("#txtRemark").change(function(){
                return;
                if(this.value)
                {
                    $("#btnSubmit").attr("disabled","true");
                    $("#btnReject").removeAttr("disabled","false");
                }
                else
                {
                    $("#btnSubmit").removeAttr("disabled");
                    $("#btnReject").attr("disabled","true");
                }
            });
            $(".toggleTable").click(function(){
                $("#tblHTML").toggle("slow");
            });
            $(".tmplitem").live("click",function(){
                $("#txtRemark").val(this.innerText);
            });
            $("#txtDealline").focus(function(){
                WdatePicker({isShowClear:false});
            });
            $(".linkclose").click(function(){
                window.close();
            });
            $(".linkidea").dialog({ title: '意见模板编辑', maxBtn: false, page: 'IdeaTemplate.aspx'
                , btnBar: false, cover: false, lockScroll: true, width: 600, height: 400, bgcolor: 'black'
            });
            $(".linkattach").click(function(){
                if($("#ideaAttach").length==0){
                    $("#txtRemark").before("<iframe id='ideaAttach' frameborder='0' scrolling='auto'  src='../Common/FileListFrame.aspx?appName=<%=tblName%>&appId=<%=uTaskId%>&read=0' width='100%' height='100'></iframe>")
                }
                else{
                    $("#ideaAttach").toggle();
                }
            });

            $("#selReAuthType").change(function(){
                var v=$("#selReAuthType").val();
                if(v=="1"){
                    $("#btnFetchCode").hide();
                    $("#labelPass").html("密码：");
                }else{
                    $("#labelPass").html("验证码：");
                    $("#btnFetchCode").show();                
                }
                $("#checkInfo").html("");
            });
            $("#btnFetchCode").click(function(){
                //获取验证码
                var ret =_curClass.SendSms();
                if(ret.error){
                    $("#checkInfo").html(ret.error.Message).css("color","red");
                }else{
                    $("#checkInfo").html("验证码已经成功发送，请注意查看").css("color","green");
                }
            });
            $("#reAuthPass").change(function(){
                //开始验证
                var t=$("#selReAuthType").val();
                var ret =_curClass.ReAuth(t,this.value);
                if(ret.error){
                    $("#checkInfo").html("验证时出错："+ret.error.Message).css("color","red");
                }
                else{
                    if(ret.value=="1"){
                        //alert("验证码正确");
                        $(".authPanel").slideUp();
                        $("#btnSubmit,#btnAgree,#btnReject,#btnRollBack,#btnDirect").attr("disabled",false);                    
                    }
                    else{
                        $("#checkInfo").html("验证码不正确").css("color","red");
                    }
                }
            }).keydown(function(event){
                if(event.keyCode == 13){
                    $(this).change();
                    return false;
                }
            });

            $("#txtRemark").limitTextarea({maxNumber:1000,position:"bottom"});

            //设置查阅权限
            $("#btnLimit").dialog({ title: '设置公开范围', maxBtn: false, page: 'QueryLimitSet.aspx?insId=<%=curInstance.InstanceId%>'
                , btnBar: false, cover: true, lockScroll: true, width: 800, height: 500, bgcolor: 'black'
            });

            $(".refDel").live("click",function(){
                if(confirm("确认删除本条参考流程吗?")){
                    $(this).parent().remove();
                }
            });

        });
        //选择参考流程后
        function referSelect(selArr){
            var referArr=[];
            for(var i=0;i<selArr.length;i++){
                var pArr=selArr[i].split("|");
                var bt="⊙&nbsp;"+pArr[1];
                if(pArr[2].length>0)
                    bt = bt + "（"+pArr[2]+"）";
                referArr.push("<div class='refItem'><a class='refLink' href='../AppFrame/AppWorkFlowInfo.aspx?instanceId=" ,pArr[0],"' target='_blank'>" ,bt,"</a>");
                referArr.push("<input type='hidden' name='wfRefer' value='",selArr[i],"' />");
                referArr.push("&nbsp;<span class='refDel' style='color:gray;cursor:hand;' title='删除参考'>[删除]</span></div>");
            }
            $(".refList").append(referArr.join(""));
        }

        function app_query(tName){
            var listHtml = _curClass.GetListHtml("<%=tblName %>",tName,_mainId).value;
            $("#"+tName).before(listHtml).remove();
        }

        var _curClass =EIS.Web.SysFolder.WorkFlow.DealFlow;
        var _isNew = false;
        var _saveBeforeSubmit=<%=SaveBeforeSubmit %>;
        var _mainTblName = "<%=tblName %>";
        var _mainId = "<%=MainId %>";
        var _sIndex = "<%=sIndex %>";
        var _saveAction = "1";
        var _workflowCode ="<%=workflowCode %>";
        var _nodeCode ="<%=nodeCode %>";
        <%=this.sbmodel.ToString() %>
        var _xmlData =jQuery(jQuery.parseXML('<xml><%=xmlData %></xml>'));
</script>
<script src="../../Js/SysFunction.js?v=<%=SysFuncEtag %>" type="text/javascript"></script>
<script type="text/javascript">
    <%=editScriptBlock %>    
</script>
