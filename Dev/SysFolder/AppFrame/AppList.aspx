<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AppList.aspx.cs" Inherits="EIS.WebBase.SysFolder.AppFrame.AppList" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>[<%=TblNameCn%>] 默认查询</title>
    <meta http-equiv="Pragma" content="no-cache" />
    <!--<meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />-->
    <link rel="stylesheet" type="text/css" href="../../grid/css/flexigrid.css" />
    <link rel="stylesheet" type="text/css" href="../../Css/appList.css" />
    <link rel="stylesheet" type="text/css" href="../../Css/datePicker.css" />
    <link rel="stylesheet" type="text/css" href="../../Css/ymPrompt_vista/ymPrompt.css" />
    <script type="text/javascript" src="../../grid/lib/jquery/jquery.js"></script>
    <script type="text/javascript" src="../../grid/flexigrid.js"></script>
    <script type="text/javascript" src="../../DatePicker/WdatePicker.js"></script>
    <script type="text/javascript" src="../../js/ymPrompt.js"></script>
    <script type="text/javascript" src="../../js/lhgdialog.min.js"></script>
    <script type="text/javascript" src="../../js/jquery.notice.js"></script>
    <%=customScript%>
</head>
<body style="margin: 1px;" scroll="no">
    <form id="form1" runat="server">
        <div id="griddiv">
            <table id="flex1" style="display: none"></table>
        </div>
    </form>
</body>
</html>
<script type="text/javascript">
<!--
    var _curClass =EIS.WebBase.SysFolder.AppFrame.AppList;
    var para="";
    $(function(){
        $(".contentDiv").live("dblclick",function(){
            clearSelection();
            if($(".edit").length == 0)
                return;
            var editid=$(this).parents("tr")[0].id.substr(3);
            if(editid){
                para="para="+_curClass.CryptPara("<%=para %>&condition=_autoid='"+editid+"'").value;
                openCenter("AppInput.aspx?"+para,"_blank",<%=ww %>,<%=wh %>);
                }

        });
    });
    $("#flex1").flexigrid({
        url: '../getxml.ashx',
        initCond:"<%=InitCond %>",
        params:[{name:"queryid",value:"<%=tblname %>"}
                    ,{name:"cryptcond",value:""}
                    ,{name:"sindex",value:"<%=sindex %>"}
			        ,{name:"condition",value:"<%= condition %>"}
                    ,{name:"defaultvalue",value:"<%=base.GetParaValue("defaultvalue") %>"}
        ],
        colModel : [
        {display: '序号', name : 'rowindex', width : 30, sortable : false, align: 'center',renderer:colIndex},
                <%=colmodel %>
        ],
        buttons : [
            {name: '<%=addBtnText %>', bclass: 'add', onpress : app_add , hidden : <%=addLimit %>},
			{name: '编辑', bclass: 'edit', onpress : app_edit , hidden : <%=editLimit %>},
			{name: '删除', bclass: 'delete', onpress : app_delete , hidden : <%=delLimit %>},
			{separator: true},
			{name: '查询', bclass: 'view', onpress : app_query},
			{name: '清空', bclass: 'clear', onpress : app_reset},
            {name: '查询定制', bclass: 'setting', onpress : app_setquery, hidden : <%=condLimit %>},
            {name: '保存布局', bclass: 'layout', onpress : app_layout, hidden : <%=layoutLimit %>},
            {separator: true},
            {name: '导出', bclass: 'excel', title:'', onpress : app_export , hidden: <%=exportLimit %>},
            {name: '导入', bclass: 'excel', title:'从EXCEL文件导入数据', onpress : app_import , hidden: <%=exportLimit %>},
            {name: '归档', bclass: 'folder', onpress : app_archive , hidden: <%=gdLimit %>},
            {separator: true},
            { name: '帮助', bclass: 'help', onpress: app_help }
        ],
        searchitems :[
			    <%=querymodel %>
        ],
        sortname: "<%=sortname %>",
        sortorder: "<%=sortorder %>",
        usepager: true,
        singleSelect:true,
        useRp: true,
        rp: 15,
        multisel:false,
        showTableToggleBtn: false,
        resizable:false,
        height: 'auto',
        onError:showError,
        preProcess:<%=preProcess %>,
        onColResize:fnColResize
    });

    function showError(data)
    {
        alert(data.responseText);
    }
    function app_add(cmd,grid)
    {
        var url = "";
        if("<%=addAction %>"=="2"){
            if("<%=workflowCode %>"=="")
                url = "../workflow/SelectWorkFlow.aspx?para=<%=cryptPara %>";
            else
                url = "../workflow/NewFlow.aspx?para=<%=cryptPara %>";
        }
        else{
            url = "AppInput.aspx?para=<%=cryptPara %>"
        }
        openCenter(url+"&t="+Math.random(),"_blank",<%=ww %>,<%=wh %>);
    }
    function fnColResize(fieldname,width)
    {
			   
    }
    function gdStateRender(val,row)
    {
        var recid=$("_AutoID",row).text();
        var arr=[];
        if(recid)
        {
            switch( val)
            {
                case "":
                case "待归档":
                    arr.push("<a class='tdbtn' title='点击归档' href=\"javascript:app_startgd('",recid,"');\">【待归档】</a>");
                    break;
                default:
                    arr.push("<a class='tdbtn green' href=\"javascript:app_gdinfo('",recid,"')\">【",val,"】</a>");
                    break;
            }
        }

        return arr.join("");
    }
    function app_startgd(appId){
        openCenter("../../Doc/DaEdit.aspx?appName=<%=tblname %>&appId="+appId,"_blank",1000,700);
    }
    function app_archive(cmd,grid){
        if($('.trSelected',grid).length>0)
        {
            var editid=$('.trSelected',grid)[0].id.substr(3);
            openCenter("../../Doc/DaEdit.aspx?appName=<%=tblname %>&appId="+editid,"_blank",1000,700);
        }
        else
        {
            alert("请选中一条记录");
        }
    }
    function app_gdinfo(appId){
            
    }
    function wfStateRender(val,row)
    {
        var recid=$("_AutoID",row).text();
        var arr=[];
        if(recid)
        {
            switch( val)
            {
                case "":
                case "未发起":
                    arr.push("未发起","<a class='tdbtn' href=\"javascript:app_startwf('",recid,"');\">【发起】</a>");
                    break;
                default:
                    arr.push(val,"<a class='tdbtn' href=\"javascript:app_wfinfo('",recid,"')\">【查看】</a>");
                    break;
            }
        }

        return arr.join("");
    }
    function app_startwf(appId)
    {
        var url = "";
        if("<%=workflowCode %>" == "")
            url = "../workflow/SelectWorkFlow.aspx?tblName=<%=tblname %>&mainId="+appId;
        else
            url = "../workflow/SelectWorkFlow.aspx?workflowCode=<%=workflowCode %>&mainId="+appId;

        openCenter(url,"_blank",1000,700);
    }
    function app_export(cmd,grid)
    {
        var param=$("#flex1")[0].p;
        var query="&query="+param.condition+"&sortname="+param.sortname+"&sortorder="+param.sortorder;
        window.open("AppExport.aspx?<%=Request.QueryString %>"+query,"_blank");
    }
    function app_import(cmd,grid)
    {
        var param=$("#flex1")[0].p;
        var url = "<%=Page.ResolveUrl("~") %>SysFolder/AppFrame/AppImport.aspx?tblName=<%=tblname %>";
        var dlg = new jQuery.dialog({ title: '数据导入', maxBtn: true, page: url
            , btnBar: true, cover: true, lockScroll: true, width: 900, height: 600, bgcolor: 'black',cancelBtnTxt:'关闭'
        });
        dlg.ShowDialog();
    }
    function app_layout(cmd,grid)
    {
        //暂时有点儿问题，应该把fieldname 换成fieldid
        var fldlist=[];
        $('th',grid).each(function(){
            var fieldId=$(this).attr("fieldid");
            if(fieldId)
                fldlist.push(fieldId+"="+($(this).width()-10)+"="+$(this).css("display"));
        });
        var param=$("#flex1")[0].p;
        var sortdir="";
        if(param.sortname.length>0){
            sortdir =(param.sortname+" "+param.sortorder);
        }
        var ret=_curClass.saveLayout(fldlist,"<%=tblname %>","<%=sindex %>",sortdir);
        if(ret.error)
        {
            alert("保存出错："+ret.error.Message);
        }
        else
        {
            $.noticeAdd({ text: '保存成功！'});
        }
    }
    function app_reset(cmd,grid)
    {
        $("#flex1").clearQueryForm();
    }
    function app_edit(cmd,grid)
    {
        if($('.trSelected',grid).length>0)
        {
            var editid=$('.trSelected',grid)[0].id.substr(3);
            if(editid){
                para="para="+_curClass.CryptPara("<%=para %>&condition=_autoid='"+editid+"'").value;
                openCenter("AppInput.aspx?"+para,"_blank",<%=ww %>,<%=wh %>);
            }
        }
        else
        {
            alert("请选中一条记录");
        }
    }

    function app_delete(cmd,grid)
    {
        if($('.trSelected',grid).length>0)
        {
            if(confirm('确定删除这' + $('.trSelected',grid).length + '条记录吗?'))
            {
                $('.trSelected',grid).each(function(){
                        var editid=this.id.substr(3);
                        if(editid){
                            var ret=_curClass.DelRecord("<%=tblname %>",editid);
                            if(ret.error)
                            {
                                alert("删除出错："+ret.error.Message);
                            }
                            else
                            {
                                $.noticeAdd({ text: '删除成功！'});
                                $("#flex1").flexReload();
                            }
                       }
                    });
				         
            }
        }
        else
        {
            alert("请选中一条记录");
        }
    }
function colIndex(fldval,row)
{
    var autoid=$(row).attr("id");
    return "<a href=\"AppDetail.aspx?tblName=<%=tblname %>&sindex=<%=sindex %>&condition=_autoid='"+autoid+"'\" target='_blank'>"+fldval+"</a>";
}
function app_view()
{
                
}
function app_wfinfo(mainId)
{
    var url = "AppWorkFlowInfo.aspx?tblName=<%=tblname %>&mainId=" + mainId;
    openCenter(url,"_blank",1000,700);
}

function app_setquery()
{
    openCenter("AppConditionDef.aspx?tblname=<%=tblname %>&sindex=<%=sindex %>","_blank",400,500);
}

function app_query()
{
    $("#flex1").flexReload();
}
function clearSelection() {
    if(document.selection && document.selection.empty) {document.selection.empty();}else if(window.getSelection) {window.getSelection().removeAllRanges();}
}

function app_help() {
    app_showHelp("./Help/HelpAppDefault.aspx?tblname=<%=tblname %>", "<%=TblNameCn%>帮助");
}

function app_showHelp(urlStr, titleStr) {
    if ((titleStr == undefined) || (titleStr == "")) titleStr = "帮助";
    var win = new $.dialog({
        id: 'HelpWin'
        , cover: true
        , maxBtn: true
        , minBtn: true
        , btnBar: true
        , lockScroll: false
        , title: titleStr
        , autoSize: false
        , width: 1150
        , height: 600
        , resize: true
        , bgcolor: 'black'
        , iconTitle: false
        , page: urlStr
    });
    win.ShowDialog();
}

function openCenter(url,name,width,height)
{
    var str = "height=" + height + ",innerHeight=" + height + ",width=" + width + ",innerWidth=" + width;
    if (window.screen)
    {
        var ah = screen.availHeight - 30;
        var aw = screen.availWidth - 10;
        var xc = (aw - width) / 2;
        var yc = (ah - height) / 2;
        if (yc < 0)
            yc = 0;
        str += ",left=" + xc + ",screenX=" + xc + ",top=" + yc + ",screenY=" + yc;
        str += ",resizable=yes,scrollbars=yes,directories=no,status=no,toolbar=no,menubar=no,location=no";
    }
    return window.open(url, name, str);
}
var _sIndex="<%=sindex %>";
    <%=listfn %>
    //-->
</script>
