<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CheckBox_Dict.aspx.cs" Inherits="EIS.Studio.SysFolder.DefFrame.inc.CheckBox_Dict" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
		<title>Radio显示：字典数据源</title>
        <link rel="stylesheet" type="text/css" href="../../../grid/css/flexigrid.css"/>
        <script type="text/javascript" src="../../../js/jquery-1.7.min.js"></script>
        <script type="text/javascript" src="../../../grid/flexigrid.js"></script>
		<script type="text/javascript">
        <!--
        function Confirm()
        {
			var txtDict=$("#rvalue").val();
	        if (txtDict == "")
	        {
		        alert("请选择字典！");
		        return false;
	        }
	        else
	        {
				var arrRet=[];
				arrRet.push($("#selLayout").val());
				arrRet.push($("input[name=raddir]:checked").val());
				arrRet.push($("#colNum").val());
				arrRet.push($("#showNum").attr("checked")== "checked"?"1":"0");

		        parent.frameElement.lhgDG.curWin.styleCallBack("<%=Request["key"] %>:<%=Request["titleName"] %>:"
                    + $("#rvalue").val()+"|"+$("#rvalue").attr("dictId")+"|"+arrRet.join(","));
		        parent.frameElement.lhgDG.cancel();
	        }
	
        }
        function Cancel()
        {
           parent.frameElement.lhgDG.cancel();
        }

        jQuery(function(){
            if(getOpenerValue("stylecode")=="<%=Request["key"] %>")
            {
                var pv=getOpenerValue("styletxt");
			    var pvs=pv.split("|");
				if(pvs.length>2)
				{
					var arr=pvs[2].split(",");
					$("#selLayout").val(arr[0]);
					if(arr[1]=="1")
						$("#raddir1").attr("checked","checked");
					else
						$("#raddir2").attr("checked","checked");
					$("#colNum").val(arr[2]);

					if(arr[3]=="1")
						$("#showNum").attr("checked", "checked");
					else
						$("#showNum").removeAttr("checked");

					jQuery("#rvalue").val(pvs[0]).attr("dictId",pvs[1]);
				}
				else{
					jQuery("#rvalue").val(pvs[0]).attr("dictId",pvs[1]);
				}

            }
        });
    function getOpenerValue(ctlName) {
        return parent.frameElement.lhgDG.curWin.document.getElementById(ctlName).value;
    }
        //-->
		</script>
        <style type="text/css">
            *{
            margin:0;
            }
            html{
                height:100%;
            }
            body{
                font-weight:normal;
                font-style:normal;
                font-family:Tahoma,Helvetica,Arial,sans-serif;
                font-size:12px;
                background-color:#f9fafe;
                height:100%;

            }

            #Table1 td{font-size:10pt;padding:5px;}
            input.btn{padding:3px 8px;height:28px;}
			.subtitle{color:#dc143c;}
        </style>
</head>
<body>
    <form id="form1" runat="server">
        
		<table id="Table1" 
				width="96%" border="0">
            <tr>
				<td ><b style="color:Blue;font-weight:bold;">Radio显示：</b>字典数据源</td>
                <td align="right" >
                    <input type="button" value=" 确 认 " class="btn" onclick="Confirm()"/> 
                    <input type="button" value=" 取 消 " class="btn" onclick="Cancel()"/>
                </td>
			</tr>
			<tr>
				<td align="left" colspan="2" >
					<table width="100%" style="border: #aaa 1px solid;">
						<tr>
							<td class='subtitle'>布局:</td>
							<td >
								<select id="selLayout">
									<option value="0">默认</option>
									<option value="1">表格</option>
								</select>
							</td>
							<td class='subtitle'>&nbsp;&nbsp;方向:</td>
							<td  align="left">
								<input type="radio" name='raddir' id='raddir1' checked="checked" value="1"/><label for='raddir1'>水平</label>
								<input type="radio" name='raddir' id='raddir2' value="2"/><label for='raddir2'>垂直</label>
							</td>
							<td class='subtitle'>&nbsp;&nbsp;列(行)数:</td>
							<td>
								<input type="text" id="colNum" value="" size="3"/>
							</td>
							<td>&nbsp;&nbsp;
								<input type="checkbox" id="showNum"/><label for='showNum' class='subtitle'>&nbsp;显示序号</label>
							</td>
						</tr>
                        <tr>
                            <td class="subtitle">
                                字典:
                            </td>
							<td align="left" colspan="6">
                                <input id="rvalue" readonly="readonly" style="width:200px;color:Gray;" />
                                <span style="color:Gray;">（只读，只能从选择上面列表中的字典）</span>
                            </td>
                        </tr>
					</table>
				</td>
				</tr>
		</table>
        <div id="griddiv">
        <table id="flex1" style="display:none"></table>    
        </div>
    </form>

</body>
</html>
<script type="text/javascript">
<!--
	$("#flex1").flexigrid
	({
	url: '../../../getdata.ashx',
	params:[{name:"queryid",value:"dictinfo"}
			,{name:"condition",value:""}
			],
	colModel : [
		{display: '序号', name : 'rowindex', width : 30, sortable : false, align: 'center'},
		{display: '字典名称', name : 'dictname', width : 100, sortable : true, align: 'left'},
		{display: '子项数', name : 'itemsnum', width : 80, sortable : false, align: 'left'},
		{display: '编辑子项', name : 'dictid', width : 80, sortable : false, align: 'center',renderer:editson},
		{display: '选择', name : 'dictname', width : 80, sortable : false, align: 'center',renderer:seldict}
		],
	buttons : [
		{name: '字典维护', bclass: 'add', onpress : app_add},
		{separator: true},
		{name: '查询', bclass: 'view', onpress : app_query},
		{name: '清空', bclass: 'clear', onpress : app_reset}
		],
	searchitems :[
		{display: '字典名称', name : 'dictname',type:1}
		],
		sortname: "dictname",
		sortorder: "asc",
		usepager: true,
		singleSelect:false,
		useRp: true,
		rp: 10,
		multisel:false,
		showTableToggleBtn: false,
		resizable:false,
        height:170,
		onError:showError
	});

	function editson(fldval,row)
	{
        var n=$("dictname",row).text();
		return "<a onclick=\"javascript:openCenter('../DefDictItems.aspx?dictid="+fldval+"&dictname="+escape(n)+"','_blank',600,400);\" href='javascript:'>编辑</a>";

	}
	function seldict(fldval,row)
	{
        var n=$("dictid",row).text();
        var c=$("dictname",row).text();
		return "<a onclick=\"javascript:setDict('"+n+"','"+c+"');\" href='javascript:'>选择</a>";			
	}
    function setDict(dictId,dictName){
        $("#rvalue").val(dictName).attr("dictId",dictId);
    }
	function showError(data)
	{
	}
	function app_add(cmd,grid)
	{
		openCenter("../DefDictFrame.aspx","_blank",860,600);
		$("#flex1").flexReload();
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
			openCenter("../DefDictEdit.aspx?editid="+editid,"_blank",260,130);
		}
		else
		{
			alert("请选中一条记录");
		}
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
//-->
</script>
