<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DefTableFields.aspx.cs"  ValidateRequest="false" Inherits="EIS.Studio.SysFolder.DefFrame.DefTableFields" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>字段编辑</title>
    <meta http-equiv="Pragma" content="no-cache"/>
    <link rel="stylesheet" type="text/css" href="../../css/defFrame.css"/>
    <script type="text/javascript" src="../../js/jquery-1.8.0.min.js"></script>
    <script type="text/javascript" src="../../js/jquery.notice.js"></script>
    <script type="text/javascript" src="../../js/jquery.validate.min.js"></script>
    <script type="text/javascript" src="../../js/jquery.zclip.min.js"></script>
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
        .textbox{
            margin:1px;
            border:0px solid #ddd;
        }
        .focus{background:lightgreen;}
        .txtname{width:120px;}
        .txtnamecn{width:160px;}
        .txtsize{width:40px;}
        .txtnote{width:380px;}
        select{border:1px solid #cfcfcf;height:22px;}
        input[type=button]{
            width:auto;
            padding:2px 3px;
            border:1px solid #ddd;
            cursor:pointer;
        }
        #tblfield
        {
            border-collapse:collapse;
        }
        #errorInfo{width:900px;line-height:200%;background:#f9fb91 url(../../img/common/error.png) no-repeat;padding-left:40px;}
    </style>
    <script type="text/javascript">
        function AddRow(n)
        {
            for(var i=0;i<n;i++)
            {
                var strhtml="";
                var deftypehtml="";
                var newEle = $("<tr><td></td><td></td><td></td><td></td><td></td><td></td><td></td></tr>");

                strhtml = "<select id=\"seltype" + rowcount + "\" size=\"1\" oindex='" + rowcount + "' onchange='javascript:checkit(this)' name=\"seltype\">";
                strhtml=strhtml+"   <option value=\"1\" selected>字符</option>";
                strhtml=strhtml+"   <option value=\"2\">整数</option>";
                strhtml=strhtml+"	<option value=\"3\">数值</option>";
                strhtml=strhtml+"	<option value=\"4\">日期</option>";
                strhtml=strhtml+"	<option value=\"5\">大文本</option>";
                strhtml=strhtml+"	</select>";

                $("td:eq(0)", newEle).append("<input id=\"txtname" + rowcount + "\" class='textbox txtname'  type=\"text\" oindex='" + rowcount + "' name=\"txtname\">");
                $("td:eq(1)", newEle).append("<input id=\"txtnamecn" + rowcount + "\" class='textbox txtnamecn'  type=\"text\" oindex='" + rowcount + "' name=\"txtnamecn\">");
                $("td:eq(2)",newEle).append(strhtml);
                $("td:eq(3)", newEle).append("<input id=\"txtsize" + rowcount + "\" class='textbox txtsize' type=\"text\"  value='50' oindex='" + rowcount + "' name=\"txtsize\">");
                $("td:eq(4)", newEle).append("<input id=\"unique" + rowcount + "\"  type=\"checkbox\" oindex='" + rowcount + "' name=\"unique\">");
        
                $("td:eq(5)", newEle).append("<input id=\"txtnote" + rowcount + "\" class='textbox txtnote' type=\"text\"  oindex='" + rowcount + "' name=\"txtnote\">");
        
                $("td:eq(6)",newEle).append("<input type=\"button\" value=\"删除\" onclick=\"dropfield('"+rowcount+"');\">");
                newEle.attr("align","center");
                $("#maintbl").append(newEle);

                fldmodel.append("<td oindex='" + rowcount + "' txtname='' txtnamecn='' seltype='1' txtsize='50' unique='0' txtnote=''  state='add'/>"); //处理XML
                if(i==0)
                    $("#txtname" + rowcount).focus();
                rowcount++;
            }
            $('html, body').animate({ scrollTop: $(document).height() }, 1000);
            $("#copybtn").trigger("reposition");
    
        }

        function checkit(obj) {
            var i = $(obj).attr("oindex");
            if (obj.value!="1" && obj.value!="3")
            {
                $("#txtsize" + i).val("").change();
                $("#txtsize" + i).attr("readonly", "readonly");
            }
            else
            {
                $("#txtsize" + i).removeAttr("readonly");	
            }
        }

	</script>
</head>
<body >
    <form id="form1" runat="server">

     <div class="menubar">
        <div class="topnav">
     
        <span style="float:left;margin-left:10px">业务名称：<%=tblName %>&nbsp;&nbsp;</span>
         <span style="float:right;margin-right:10px">
            <a class='linkbtn' href="javascript:" onclick="AddRow(1);">添加1行(Ctrl+Enter)</a>
            <em class="split">|</em>
            <a class='linkbtn' href="javascript:" onclick="AddRow(5);">添加5行</a>
            <em class="split">|</em>
            <asp:LinkButton ID="LinkButton1" CssClass="linkbtn" runat="server" OnClick="LinkButton1_Click" >保存</asp:LinkButton>

        </span>
        </div>
    </div>
    <div class="center" style="width:890px;">
        <div class="tipZone">
        <%=OpInfo %>
        </div>
        <br />
		<table id="maintbl" align="center"  border="1">
		    <thead>
		    <tr>
			    <th align="center" width="120" height="25">字段名</th>
			    <th align="center" width="160" >字段中文名</th>
			    <th align="center" width="40">类型</th>
			    <th align="center" width="40">长度</th>
			    <th align="center" width="40">唯一</th>
			    <th align="center" >字段说明</th>
			    <th align="center" width="40">删除</th>
		    </tr>
		    </thead>
		    <tbody>
            <%=fieldHTML %>
            </tbody>
		</table>
        <div class="tip" >键盘操作：↓（向下移动）、↑（向上移动）、Ctrl+Enter（添加字段）、字段长度内Enter（跳转到下一行，如果是最后一行会添加新字段）</div>
        <div style="text-align:left;">
            <a href="javascript:" class="quickbtn" id="savebtn">&nbsp;&nbsp;保 存&nbsp;&nbsp;</a>
            <a href="javascript:" class="quickbtn" onclick="AddRow(1);">添加行</a>
            <a href="javascript:" class="quickbtn" onclick="AddRow(5);">添加5行</a>
            <a href="javascript:" class="quickbtn" id="copybtn">复制字段列表</a>
            <a href="javascript:" class="quickbtn" onclick="pasteRows();">粘贴字段列表</a>
        </div>
        <br />
    </div>
    <input id="state" type="hidden"  name="state" value='<%=hidstate %>'/>
    <input id="txtxml" type="hidden"  name="txtxml" value=''/>
    <script type="text/javascript">
        var _upKeyCode = 38;
        var _downKeyCode = 40;
        var rowcount=<%=rowcount %>;
        var fldmodel=$("<tr><%=fieldXML %></tr>");
        var fieldLen = { '1': '50', '2': ' ', '3': '10,2', '4': ' ', '5': ' ' };
        var clip=null;
        $(function(){ //初始化
            $("#LinkButton1").click(chkform);
            $("#savebtn").click(function(){
                if(chkform()) __doPostBack("LinkButton1","");
            });
            $(document).keydown(function(e){
                if(e.ctrlKey && event.keyCode == 13)
                {
                    AddRow(1);
                }
            });
            $("#copybtn").zclip({
                path: "../../js/ZeroClipboard.swf",
                copy: function(){ return getFieldList();},
                afterCopy:function(){ $.noticeAdd({ text: '字段复制成功！', stay: false, width: 400 });}
            });

            //双击修改字段名
            $("input.txtname").dblclick(function(e){
                if(e.ctrlKey){
                    $(this).prop("readonly",false);
                }
            });
            $("input[type!=button],select").live("change",updatexml);
            $("input[type!=button]").live("keydown",function(e){
                var i = parseInt($(this).attr("oindex")) ;
                var ctlName = $(this).attr("name");
                if(e.keyCode == _upKeyCode)
                {
                    //向上
                    var ctl=$("#"+ctlName+(i-1));
                    if(ctl.length>0){
                        ctl.focus().select();
                    }
                    else{
                        var itd = $(this).parent().index();
                        var pNode = $(this).parent().parent().prev();
                        if( pNode.length == 0 )
                            return;
                        ctl = $("td:eq("+itd+")>input",pNode);
                        ctl.focus().select();
                    }
                }
                else if(e.keyCode == _downKeyCode)
                {
                    var ctl=$("#"+ctlName+(i+1));
                    if(ctl.length>0)
                    {
                        ctl.focus().select();
                    }
                    else
                    {
                        var itd = $(this).parent().index();
                        var pNode = $(this).parent().parent().next();
                        if( pNode.length == 0 )
                            return;
                        ctl = $("td:eq("+itd+")>input",pNode);
                        ctl.focus().select();
                    }

                }
                else if(e.keyCode == 13 && ctlName == "txtsize")
                {
                    var ctl=$("#txtname"+(i+1));
                    if(ctl.length>0)
                    {
                        ctl.focus().select();
                    }
                    else
                    {
                        var itd = $(this).parent().index();
                        var pNode = $(this).parent().parent().next();
                        if( pNode.length == 0 )
                        {
                            AddRow(1);
                        }
                        else
                        {
                            ctl = $("td:eq(0)>input",pNode);
                            ctl.focus().select();
                        }
                    }
                }

            });

            $("input[type!=button]").live("focus",function(){
                $(".focus").change().removeClass("focus");
                $(this).addClass("focus");
            });

            if("<%=hidstate %>"=="0")
                AddRow(10); 
    		    
    	});
        function updatexml()
        {
            var i = $(this).attr("oindex");
            var xmlobj = fldmodel.find("td[oindex="+i+"]");
            var txtname = this.name;
            if(this.name == "seltype"){
                $("#txtsize"+i).val(fieldLen[this.value]);
                xmlobj.attr("txtsize",fieldLen[this.value]);
            }

            var txtvalue=this.value||" ";
            if(this.type=="checkbox")
            {
                txtvalue=this.checked?"1":"0";
            }
            var fldstate=xmlobj.attr("state");
            if(fldstate=="unchanged"){
                if(!$(this).attr("readonly"))
                {
                    xmlobj.attr(txtname,txtvalue);
                    xmlobj.attr("state","changed");
                } 
            }
            else if(fldstate=="changed"){
                if(!$(this).attr("readonly"))
                {
                    xmlobj.attr(txtname,txtvalue);
                }
            }
            else if(fldstate=="add"){
                xmlobj.attr(txtname,txtvalue);
            }
            //更新XML
		        
        }
        function dropfield(oindex)
        {
            if(!confirm("您确定删除该字段吗") )
                return false;
            var xmlobj =fldmodel.find("td[oindex='"+oindex+"']:first");

            var fldstate=(xmlobj.attr("state"));
            if(fldstate!="add"){
                xmlobj.attr({"state":"deleted"});
            }
            else
            {
                xmlobj.remove();
            }
            $(event.srcElement).parents("tr:first").remove();
                    
	        
        }
        var saveFlag = 0;
        var regfldname=/^[a-zA-Z_]+[\w_]*$/;
        var regfldcn=/.+/;
        var regfldsize=[/^[1-9]+\d*$/,/^\s*$/,/^[1-9]+\d*,[1-9]+\d*$/,/^\s*$/,/^\s*$/,/^\s*$/];
        function chkform()  //判断字段长度输入是否规范
        {	

            if (saveFlag == 1) {return false;}

            var flag = true;
            $("input.textbox,select").each(function(){
                var i = $(this).attr("oindex") ;
                var f = $("#txtname"+i).val();
                var t = parseInt($("#seltype"+i).val());
                var v = $(this).val();
                var isNew = "<%=hidstate %>" == "0";
                switch(this.name)
                {
                    case "txtname":
                        if(f!="" && isNew && !regfldname.test(v)){
                            alert(v+"字段名称不符合规定");
                            $(this).select();
                            flag = false;
                            return false;
                        }    
                        break;
                    case "txtnamecn":
                        if(f!="" && !regfldcn.test(v)){
                            alert(f+"字段中文名不符合规定");
                            $(this).select();
                            flag = false;
                            return false;
                        } 
                        break;
                    case "seltype":
                        break;
                    case "txtsize":
                        if(f!="" && !regfldsize[t-1].test(v)){
                            alert(f+"字段长度不符合规定");
                            $(this).select();
                            flag = false;
                            return false;
                        }
                        break;
                }
		    });	
            if(!flag)
                return false;
            $("#txtxml").val("<tr>"+fldmodel.html()+"</tr>");
		    //提交之后置1
            saveFlag = 1;
            return true;

        }		
    </script>
    <script type="text/javascript">
        var mapLen = { '字符': '50', '整数': '', '数值': '10,2', '日期': '', '大文本': '' };
        function getFieldList() { 
            var arrField = [];
            var tMap = {'1':'字符','2':'整数','3':'数值','4':'日期','5':'大文本'};
            for (var i = 0; i < rowcount; i++) {
                if ($("#txtname" + i).length > 0) {
                    var arr = [];
                    arr.push($("#txtname" + i).val());
                    arr.push($("#txtnamecn" + i).val());
                    var ft = $("#seltype" + i).val()
                    arr.push(tMap[ft]);
                    arr.push($("#txtsize" + i).val());
                    arrField.push(arr.join("\t"));
                }
            }
            var fieldList = arrField.join("\r\n");
            return fieldList;
        }

        function copyRows() {
            try {
                var fieldList = getFieldList();
                if ($.browser.msie) {
                    if (clipboardData.setData("Text", fieldList)) {
                        $.noticeAdd({ text: '字段复制成功！', stay: false, width: 400 });
                    }
                    else {
                        $(".tipZone").prepend("<div class='tip'>复制字段时出错，请把本站点加到信任站点</div>");
                        setTimeout(function () { $(".tipZone").html(""); }, 10000);
                    }
                }
                else {
                    clip.setText(fieldList);
                }
            }
            catch (e) {
                $(".tipZone").prepend("<div class='tip'>复制字段时出错，请把本站点加到信任站点</div>");
                setTimeout(function () { $(".tipZone").html(""); }, 10000);
            }
        }
        function pasteRows() {
            var url = "DefTableFieldsPop.htm";
            var dlg = new $.dialog({ title: '粘贴字段列表', page: url
                , btnBar: false, cover: true, lockScroll: true, width: 800, height: 500, bgcolor: 'gray'

            });
            dlg.ShowDialog();
        }

        function pasteList(c) {
            //var c = clipboardData.getData("Text");
            var lines = c.split("\n");
            if (lines[lines.length - 1] == "")
                lines.pop();
            if (lines.length > 1) {
                for (var i = 0; i < lines.length; i++) {
                    if (lines[i].length == 0)
                        continue;
                    index = rowcount;
                    AddRow(1);
                    var attrs = lines[i].split("\t");
                    if (attrs.length == 1) {
                        $("#txtname" + index).val(attrs[0]).change();
                    }
                    else if (attrs.length == 2) {
                        $("#txtname" + index).val(attrs[0]).change();
                        $("#txtnamecn" + index).val(attrs[1]).change();
                    }
                    else if (attrs.length == 3) {
                        $("#txtname" + index).val(attrs[0]).change();
                        $("#txtnamecn" + index).val(attrs[1]).change();
                        selectType("seltype" + index, attrs[2]);
                    }
                    else if (attrs.length == 4) {
                        $("#txtname" + index).val(attrs[0]).change();
                        $("#txtnamecn" + index).val(attrs[1]).change();
                        selectType("seltype" + index, attrs[2]);
                        $("#txtsize" + index).val(attrs[3]).change();
                    }

                }
                $("#copybtn").trigger("reposition");

            }
        }

        function selectType(ctlId, v) {
            var ctls = $("#" + ctlId);
            var tMap = { '字符': '1', '整数': '2', '数值': '3', '日期': '4', '大文本': '5' };
            if (ctls.length > 0)
                ctls.val(tMap[v]).change();
        }
    </script>
    </form>
</body>
</html>
