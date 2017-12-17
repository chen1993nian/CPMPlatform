<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DefTableScriptList.aspx.cs" Inherits="EIS.WebBase.SysFolder.AppFrame.DefTableScriptList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>默认查询</title>
    <meta http-equiv="Pragma" content="no-cache"/>
    <!--<meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />-->
    <link rel="stylesheet" type="text/css" href="../../grid/css/flexigrid.css"/>
    <link rel="stylesheet" type="text/css" href="../../Css/DefStyle.css"/>
    <script type="text/javascript" src="../../grid/lib/jquery/jquery.js"></script>
    <script type="text/javascript" src="../../grid/flexigrid.js"></script>
    <script type="text/javascript" src="../../DatePicker/WdatePicker.js"></script> 

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
    var _curClass =EIS.WebBase.SysFolder.AppFrame.DefTableScriptList;
    var para="";
    $(function(){
        $(".contentDiv").live("dblclick",function(){
            clearSelection();
            if($(".edit").length == 0)
                return;
            var editid=$(this).parents("tr")[0].id.substr(3);
            if(editid){
                para="para="+_curClass.CryptPara("<%=para %>&condition=_autoid='"+editid+"'").value;
                openCenter("../AppFrame/AppInput.aspx?"+para,"_blank",<%=ww %>,<%=wh %>);
                }

        });
    });
    $("#flex1").flexigrid
	({
	    url: '../../getxml.ashx',
	    params:[{name:"queryid",value:"T_E_Sys_TableScript"}
			    ,{name:"cryptcond",value:""}
			    ,{name:"sindex",value:"<%=sindex %>"}
			    ,{name:"condition",value:"TableName='<%=bizName%>'"}
	    ],
	    colModel : [
	    {display: '序号', name : 'rowindex', width : 30, sortable : false, align: 'center'},
		{display: '业务名称', name : 'TableName', width : 120, sortable : true, align: 'left',hide:false,renderer:false,fieldid:'846bfba7-551d-4194-bbc3-2819236fdcd7'},
		{display: '编码', name : 'ScriptCode', width : 220, sortable : true, align: 'left',hide:false,renderer:false,fieldid:'0b1dd4d7-d21d-4176-9dd7-0ba1e9a2772e'},
		{display: '事件', name : 'ScriptEvent', width : 60, sortable : true, align: 'left',hide:false,renderer:false,fieldid:'0b1dd4d7-d21d-4176-9dd7-0ba1e9a2772e'},
		{display: '有效', name : 'Enable', width : 40, sortable : true, align: 'left',hide:false,renderer:false,fieldid:'0b1dd4d7-d21d-4176-9dd7-0ba1e9a2772e'},
		{display: '说明', name : 'ScriptNote', width : 218, sortable : true, align: 'left',hide:false,renderer:false,fieldid:'e5509389-9fba-49bb-bb7e-5e621742654f'}
	    ],
	    buttons : [
		    {name: '添加', bclass: 'add', onpress : app_add},
		    {name: '编辑', bclass: 'edit', onpress : app_edit},
		    {name: '删除', bclass: 'delete', onpress : app_delete},
		    {separator: true},
		    {name: '查询', bclass: 'view', onpress : app_query},
		    {name: '清空', bclass: 'clear', onpress : app_reset}
	    ],
	    searchitems :[
		    {display: '业务名称', name : 'TableName', type: 1},{display: '编码', name : 'ScriptCode', type: 1}
	    ],
	    sortname: "",
	    sortorder: "",
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
        alert("加载数据出错");
    }
    function app_add(cmd,grid)
    {
        para="para="+_curClass.CryptPara("TblName=T_E_Sys_TableScript&T_E_Sys_TableScriptcpro=TableName=<%=bizName%>^1&sindex=<%=sindex %>&condition=").value;
	    openCenter("../AppFrame/AppInput.aspx?"+para,"_blank",800,600);
	}
	function fnColResize(fieldname,width)
	{
			   
	}
	function wfStateRender(val,row)
	{
	    var recid=$("_AutoID",row).text();
	    var arr=[];
	    switch( val)
	    {
	        case "":
	        case "未发起":
	            arr.push("未发起","&nbsp;<a class='tdbtn' href=\"javascript:app_startwf('",recid,"');\">【发起】</a>");
	            break;
	        default:
	            arr.push(val,"&nbsp;<a class='tdbtn' href=\"javascript:app_wfinfo('",recid,"')\">【查看】</a>");
	            break;
	    }

	    return arr.join("");
	}
	function app_layout(cmd,grid)
	{
	    //暂时有点儿问题，应该把fieldname 换成fieldid
	    var fldlist=[];
	    $('th',grid).each(function(){
			    
	        fldlist.push((this.fieldid||this.field)+"="+($(this).width()-10)+"="+$(this).css("display"));
	    });
	    var ret=_curClass.saveLayout(fldlist,"T_E_Sys_TableScript","<%=sindex %>");
		if(ret.error)
		{
		    alert("保存出错："+ret.error.Message);
		}
		else
		{
		    alert("保存成功！");
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
            para="para="+_curClass.CryptPara("TblName=T_E_Sys_TableScript&sindex=<%=sindex %>&condition=_autoid='"+editid+"'").value;
			openCenter("../AppFrame/AppInput.aspx?"+para,"_blank",800,600);
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
                $('.trSelected',grid).each
			    (
			        function()
			        {
			            var ret=_curClass.DelRecord("T_E_Sys_TableScript",this.id.substr(3));
			            if(ret.error)
			            {
			                alert("删除出错："+ret.error.Message);
			            }
			            else
			            {
			                alert("删除成功！");
			                $("#flex1").flexReload();
			            }
			        }
			    );
				         
            }
        }
        else
        {
            alert("请选中一条记录");
        }
    }
    function addCallBack()
    {
        $("#flex1").flexReload();
    }
    function app_setquery()
    {
        var ret=window.showModalDialog("../AppFrame/AppConditionDef.aspx?tblname=T_E_Sys_TableScript&sindex=<%=sindex %>","","dialogHeight=500px;dialogWidth=400px;status=no;center=yes;resizable=yes;");
	    if(ret=="ok")
	        window.location.reload();
	}
	function app_query()
	{
	    $("#flex1").flexReload();
	}
	function clearSelection() {
	    if(document.selection && document.selection.empty) {document.selection.empty();}else if(window.getSelection) {window.getSelection().removeAllRanges();}
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
    <%=listfn %>
    //-->
</script>