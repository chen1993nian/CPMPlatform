<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AppQuery.aspx.cs" Inherits="EIS.WebBase.SysFolder.AppFrame.AppQuery" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>查询</title>
    <meta http-equiv="Pragma" content="no-cache"/>
    <link rel="stylesheet" type="text/css" href="../../grid/css/flexigrid.css"/>
    <link rel="stylesheet" type="text/css" href="../../Css/DefStyle.css"/>
    <link rel="stylesheet" type="text/css" href="../../Css/datePicker.css"/>
    <link rel="stylesheet" type="text/css" href="../../Css/ymPrompt_vista/ymPrompt.css"/>
    <script   type="text/javascript"  src="../../Js/jquery-1.7.min.js"></script>

    <script type="text/javascript" src="../../grid/flexigrid.js"></script>
    <script type="text/javascript" src="../../DatePicker/WdatePicker.js"></script> 
    <script type="text/javascript" src="../../js/ymPrompt.js"></script> 
    <script type="text/javascript" src="../../js/lhgdialog.min.js"></script>
    <script type="text/javascript" src="../../js/jquery.notice.js"></script>
    <%=customScript%>
</head>
<body style="margin:1px;" scroll="no">
    <form id="form1" runat="server">
    <div id="griddiv" >
        <table id="flex1" style="display:none"></table>    
    </div>
    </form>
</body>
</html>
<script type="text/javascript">
<!--
    var _curClass = EIS.WebBase.SysFolder.AppFrame.AppQuery;
    var para="";
    $(function(){
    });
    $("#flex1").flexigrid({
        url: '../getxml.ashx',
        initCond:"<%=InitCond %>",
			params:[{name:"queryid",value:"<%=tblname %>"}
                    ,{name:"cryptcond",value:""}
                    ,{name:"condition",value:"<%=base.GetParaValue("condition") %>"}
                    ,{name:"defaultvalue",value:"<%=base.GetParaValue("defaultvalue") %>"}
			],
			    colModel : [
                {display: '序号', name : 'rowindex', width : 30, sortable : false, align: 'center'},
                <%=colmodel %>
			],
			    buttons : [
                    {name: '查询', bclass: 'view', onpress : app_query},
                    {name: '清空', bclass: 'clear', onpress : app_reset},
                    {name: '查询定制', bclass: 'view', onpress : app_setquery, hidden : <%=condLimit %>},
				    {name: '保存布局', bclass: 'layout', onpress : app_layout, hidden : <%=layoutLimit %>},
				    {name: '导出', bclass: 'layout', onpress : app_export, hidden: <%=exportLimit %>},
                    {separator: true},
				    { name: '帮助', bclass: 'help', onpress: app_help }
				],
			    searchitems :[
			    <%=querymodel %>
			],
			    sortname: "<%=sortname %>",
			    sortorder: "<%=sortorder %>",
			    usepager: true,
			    singleSelect:false,
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
        function fnColResize(fieldname,width)
        {
			   
        }


        function app_help() {
            app_showHelp("./Help/HelpAppDefault.aspx?tblname=<%=tblname %>", "帮助");
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

        function app_export(cmd,grid)
        {
            var param=$("#flex1")[0].p;
            var query="&query="+param.condition+"&sortname="+param.sortname+"&sortorder="+param.sortorder;
            window.open("AppExport.aspx?<%=Request.QueryString %>"+query,"_blank");
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
                var ret = _curClass.saveLayout(fldlist,"<%=tblname %>","<%=sindex %>",sortdir);
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

            function app_setquery()
            {
                openCenter("AppConditionDef.aspx?tblname=<%=tblname %>&sindex=<%=sindex %>","_blank",400,500);
			}
			function app_query()
			{
			    $("#flex1").flexReload();
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
			        str += ",left=" + xc + ",screenX=" + xc + ",top=" + yc + ",screenY=" + yc;
			        str += ",resizable=yes,scrollbars=yes,directories=no,status=no,toolbar=no,menubar=no,location=no";
			    }
			    return window.open(url, name, str);
			}
			var _sIndex="<%=sindex %>";
    <%=listfn %>
    //-->
</script>