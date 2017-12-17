<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DefFieldsQuery.aspx.cs" Inherits="EIS.Studio.SysFolder.DefFrame.DefFieldsQuery" ValidateRequest="false"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>查询条件定义</title>
    <meta http-equiv="Pragma" content="no-cache"/>
    <meta http-equiv="content-type" content="text/html; charset=utf-8"/>

    <link rel="stylesheet" type="text/css" href="../../css/DefFrame.css"/>
    <script type="text/javascript" src="../../js/jquery-1.7.min.js"></script>
    <script type="text/javascript" src="../../js/jquery.notice.js"></script>
    <script type="text/javascript" src="../../js/lhgdialog.min.js"></script>
    
    <style type="text/css">
    #maintbl
    {
	    border-collapse: collapse;
        margin-left:auto;
	    margin-right:auto;
	    font-size: 12px;
	    line-height:20px;
	    border:1px solid #a0a0a0;
	    color:#393939;
	    background:#ebf3ff;
	    width:900px;
    }
    td,th
    {
        border:1px solid #a0a0a0;
        padding:2px;
    }
    .textbox
    {
        border:1px solid #ddd;
        width:96%;
    }
    .focus{background:lightgreen;}
    .seldeftype
    {
        width:160px;
    }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="menubar">
  <div class="topnav">

        <span style="float:left;margin-left:10px">业务名称：<%=tblName %>&nbsp;&nbsp;
        </span>
        <span style="float:right;margin-right:10px">
            <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click" >保存</asp:LinkButton>
        </span>
        </div>
   </div>

		<div id="tabs-1">
        <br />
			<table id="maintbl" align="center" border="1" >
                <thead>
				<tr>
				    <th align="center" width="30" height="25">序号</th>
					<th align="center" width="100" height="25">字段名</th>
					<th align="center" width="100" >中文名</th>
					<th align="center" width="60">查询条件</th>
					<th align="center" width="80">匹配方式</th>
					<th align="center" width="160">查询默认值类型</th>
					<th align="center" width="90">查询默认值</th>
					<th align="center" >选择方式</th>
					<th align="center" width="40">选择</th>
					
				</tr>
                </thead>
                <tbody>
                <%=fieldHTML %>
                </tbody>
		</table>
        </div>

        <input type="hidden" id="stylecode" />
        <input type="hidden" id="stylename" />
        <input type="hidden" id="styletxt" />
        <input id="txtxml" type="hidden"  name="txtxml" value=''/>
    	<script type="text/javascript">
    	    var _upKeyCode = 38;
    	    var _downKeyCode = 40;
    	    var rowcount=<%=rowcount %>;
    	    var fldmodel=jQuery(jQuery.parseXML("<xml><root><%= fieldXML %></root></xml>"));
    	    $(function(){ //初始化
    	        $("#LinkButton1").click(chkform);
    	        $("input,select").change(updatexml);
    	        $("input[type!=button]").live("keydown",function () {
    	            var i = parseInt($(this).attr("oindex"));
    	            var ctlName = $(this).attr("name");
    	            if (event.keyCode == _upKeyCode) {
    	                //向上
    	                var ctl = $("#" + ctlName + (i - 1));
    	                if (ctl.length > 0) {
    	                    ctl[0].focus();
    	                    ctl[0].select();
    	                }
    	            }
    	            else if (event.keyCode == _downKeyCode) {
    	                //向下
    	                var ctl = $("#" + ctlName + (i + 1));
    	                if (ctl.length > 0) {
    	                    ctl[0].focus();
    	                    ctl[0].select();
    	                }
    	            }
    	        });
    	        $("td").live("click",function () {
    	            $("tr.focus").removeClass("focus");
    	            $(this).parent("tr").addClass("focus");
    	        });
    	        $("input[type!=button]").live("focusin",function () {
    	            $(this).toggleClass("focus", "");

    	            $("tr.focus").removeClass("focus");
    	            $(this).closest("tr").addClass("focus");
    	        });
    	        $("input[type!=button]").live("focusout",function () {
    	            $(this).toggleClass("focus", "");
    	            $("tr.focus").removeClass("focus");
    	            $(this).closest("tr").addClass("focus");
    	        });
    	    });
    	    function updatexml()
    	    {
    	        var i=$(this).attr("oindex");
    	        var xmlobj =fldmodel.find("td[oindex="+i+"]");
    	        var txtname= this.name;
    	        var txtvalue = $(this).val() || " ";

    	        if(this.type=="checkbox")
    	        {
    	            txtvalue=this.checked?"1":"0";
    	        }
    	        var fldstate = xmlobj.attr("state");
    	        if (fldstate == "unchanged") {
    	            jQuery(xmlobj).find(txtname).text(txtvalue);
    	            xmlobj.attr("state","changed");
    	        }
    	        else{
    	            jQuery(xmlobj).find(txtname).text(txtvalue);                    
    	        }
    	        //更新XML
		        
    	    }
    	    var regfldcn=/.+/;
    	    var regwidth=/^[1-9]+\d*$/;
    	    function chkform()  //判断字段长度输入是否规范
    	    {	
    	        var f=true;
    	        if(!f)
    	            return false;
    	        $("#txtxml").val(_serializeToString(fldmodel));
			
    	    }
    	    function _serializeToString(objXML) {
    	        if (window.XMLSerializer) {
    	            return (new XMLSerializer()).serializeToString(objXML.find("root")[0])
    	        }
    	        else {
    	            return objXML.find("root")[0].xml;
    	        }
    	    }
    	    var selIndex = "";
    	    function seldispstyle()
    	    {
    	        selIndex = $(event.srcElement).attr("oindex");
    	        $("#stylecode").val($("#txtstyle" + selIndex).val());
    	        $("#stylename").val($("#txtstylename" + selIndex).val());
    	        $("#styletxt").val($("#txtstyletxt" + selIndex).val());

    	        var xmlobj = fldmodel.find("td[oindex='" + selIndex + "']");
    	        var fieldid = xmlobj.attr("fieldid");
    	        var fieldname = xmlobj.find("txtname").text();

    	        var url = '<%=Page.ResolveUrl("~") %>SysFolder/DefFrame/DefQueryStyleFrame.aspx?tblname=<%=tblName %>&fieldid=' + fieldid + "&fieldname=" + fieldname;
            var dlg = new $.dialog({ title: '查询风格设置', page: url, maxBtn: false
            , btnBar: false, cover: true, lockScroll: true, width: 800, height: 600, bgcolor: 'gray'
                    
            });
            dlg.ShowDialog();

		    //_openCenter("DefQueryStyleFrame.aspx?tblname=<%=tblName %>&fieldid=" + fieldid + "&fieldname=" + fieldname, "_blank", 800,600);

		}
    	    function styleCallBack(ret) { 
    	        var arr = ret.split(":");
    	        var i=ret.indexOf(":");
    	        i = ret.indexOf(":", i);
    	        i = ret.indexOf(":", i+1);
    	        var txt = ret.substr(i + 1);
    	        $("#txtstyle" + selIndex).val(arr[0]).change();
    	        $("#txtstylename" + selIndex).val(arr[1]).change();
    	        $("#txtstyletxt" + selIndex).val(txt).change();
    	    }
    	    function _openCenter(url, name, width, height) {
    	        var str = "height=" + height + ",innerHeight=" + height + ",width=" + width + ",innerWidth=" + width;
    	        if (window.screen) {
    	            var ah = screen.availHeight - 30;
    	            var aw = screen.availWidth - 10;
    	            var xc = (aw - width) / 2;
    	            var yc = (ah - height) / 2;
    	            str += ",left=" + xc + ",screenX=" + xc + ",top=" + yc + ",screenY=" + yc;
    	            str += ",resizable=yes,scrollbars=yes,directories=no,status=no,toolbar=no,menubar=no,location=no";
    	        }
    	        return window.open(url, name, str);
    	    }	
		</script>
    </form>
    <br />
</body>
</html>
