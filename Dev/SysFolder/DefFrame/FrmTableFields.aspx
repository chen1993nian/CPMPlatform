<%@ Page Title="" Language="C#" MasterPageFile="~/SysFolder/DefFrame/FrmTable.Master" AutoEventWireup="true" CodeBehind="FrmTableFields.aspx.cs" Inherits="EIS.Studio.SysFolder.DefFrame.FrmTableFields" ValidateRequest="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .panel-body{background-color:rgb(249, 250, 254);}
        .input-sm{padding:2px 2px 2px 3px;}
        option{height:18px;}
        #maintbl
        {
	        border-collapse: collapse;
            margin-left:auto;
	        margin-right:auto;
	        font-size: 12px;
	        line-height:20px;
	        border:1px solid #ddd;
	        border-radius:4px;
	        -webkit-border-radius:4px;
	        color:#393939;
	        width:100%;
        }
        th
        {
            border:1px solid #ddd;
            padding:5px 5px 5px 10px;
            font-size:13px;
            color:#333;
            background-color:#E9F3F8;
        }
         td
        {
            border:1px solid #ddd;
            padding:2px;
        }
        .textbox{
            margin:1px;
        }
        .focus{background:lightgreen;}
        .txtname{width:120px;}
        .txtnamecn{width:160px;}
        .txtsize{width:50px;}
        #tblfield
        {
            border-collapse:collapse;
        }
        #errorInfo{width:790px;line-height:200%;background:#f9fb91 url(../../img/common/error.png) no-repeat;padding-left:40px;}
    </style>

    <script type="text/javascript">
        jQuery(function () {
            $("ul.nav>li:eq(1)").addClass("active");
        });
        function AddRow(n) {
            for (var i = 0; i < n; i++) {
                var strhtml = "";
                var deftypehtml = "";
                var newEle = $("<tr><td></td><td></td><td></td><td></td><td></td><td></td><td></td></tr>");

                strhtml = "<select id=\"seltype" + rowcount + "\" size=\"1\" class='form-control input-sm' oindex='" + rowcount + "' onchange='javascript:checkit(this)' name=\"seltype\">";
                strhtml = strhtml + "   <option value=\"1\" selected>字符</option>";
                strhtml = strhtml + "   <option value=\"2\">整数</option>";
                strhtml = strhtml + "	<option value=\"3\">数值</option>";
                strhtml = strhtml + "	<option value=\"4\">日期</option>";
                strhtml = strhtml + "	<option value=\"5\">大文本</option>";
                strhtml = strhtml + "	</select>";

                $("td:eq(0)", newEle).append("<input id=\"txtname" + rowcount + "\" class='form-control input-sm txtname'  type=\"text\" oindex='" + rowcount + "' name=\"txtname\">");
                $("td:eq(1)", newEle).append("<input id=\"txtnamecn" + rowcount + "\" class='form-control input-sm txtnamecn'  type=\"text\" oindex='" + rowcount + "' name=\"txtnamecn\">");
                $("td:eq(2)", newEle).append(strhtml);
                $("td:eq(3)", newEle).append("<input id=\"txtsize" + rowcount + "\" class='form-control input-sm txtsize' type=\"text\"  value='50' oindex='" + rowcount + "' name=\"txtsize\">");
                $("td:eq(4)", newEle).append("<input id=\"unique" + rowcount + "\"  type=\"checkbox\" oindex='" + rowcount + "' name=\"unique\">");

                $("td:eq(5)", newEle).append("<input id=\"txtnote" + rowcount + "\" class='form-control input-sm txtnote' type=\"text\"  oindex='" + rowcount + "' name=\"txtnote\">");

                $("td:eq(6)", newEle).append("<input type=\"button\" class='btn btn-link' value=\"删除\" onclick=\"dropfield('" + rowcount + "');\">");

                newEle.attr("align", "center");
                $("#maintbl").append(newEle);
                fldmodel.append("<td oindex='" + rowcount + "' txtname='' txtnamecn='' seltype='1' txtsize='50' unique='0' txtnote='' state='add'/>"); //处理XML
                $("#txtname" + rowcount).focus();
                rowcount++;
            }

        }

        function checkit(obj) {
            var i = $(obj).attr("oindex");
            if (obj.value != "1" && obj.value != "3") {
                $("#txtsize" + i).val("").change();
                $("#txtsize" + i).attr("readonly", "readonly");
            }
            else {
                $("#txtsize" + i).removeAttr("readonly");
            }
        }

	</script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="panel panel-default">
            <div class="panel-heading">
                <h3 class="panel-title">
                    <span class="glyphicon glyphicon-list"></span>
                    字段定义&nbsp;<span style="color:#428bca;font-size:12px;">【<%=tblName %>】</span>
                    
                    </h3>
            </div>
            <div class="panel-body">
            <div class="col-sm-12">
                <%=OpInfo %>
		        <table id="maintbl" align="center"  border="1">
		            <thead>
				    <tr>
					    <th align="center" width="120" height="25">字段名</th>
					    <th align="center" width="160" >字段中文名</th>
					    <th align="center" width="70">类型</th>
					    <th align="center" width="60">长度</th>
					    <th align="center" width="50">唯一</th>
					    <th align="center" >字段描述</th>
					    <th align="center" width="60">&nbsp;删除</th>
				    </tr>
				    </thead>
				    <tbody>
                    <%=fieldHTML %>
                    </tbody>
		        </table>
                <input id="state" type="hidden"  name="state" value='<%=hidstate %>'/>
                <input id="txtxml" type="hidden"  name="txtxml" value=''/>
            </div>

            <div class="col-sm-12">
                <div class="alert alert-info" style="margin-top:5px;" role="alert">提示：本页面支持键盘操作，↓（向下移动）、↑（向上移动）、Ctrl+Enter（添加字段）、长度字段内Enter（跳转到下一行，如果是最后一行会添加新字段）</div> 
            </div>
            <div class="col-sm-12">
                <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-primary hidden" 
                    Text=" 提交 " onclick="btnSubmit_Click" ClientIDMode="Static" />
                <button type="button" id="Button1" class="btn btn-primary">
                    <span class="glyphicon glyphicon-ok-sign"></span> 提交
                </button>

                <button type="button" id="btnAdd1" class="btn btn-success">
                    <span class="glyphicon glyphicon-plus"></span>添加1行
                </button>
                <button type="button" id="btnAdd5" class="btn btn-success">
                    <span class="glyphicon glyphicon-plus"></span>添加5行
                </button>
            </div>

            </div>
        </div>
     </div>

     <script type="text/javascript">

         var _upKeyCode = 38;
         var _downKeyCode = 40;
         var rowcount=<%=rowcount %>;
        var fldmodel=$("<tr><%=fieldXML %></tr>");

         function pasteList(e){
             var c=clipboardData.getData("Text");
             var lines=c.split("\r\n");
             if(lines[lines.length-1]=="")
                 lines.pop();
             if(lines.length>1){
                 var index = parseInt($(e.srcElement).attr("oindex"));
                 for(var i=0;i<lines.length;i++){
                     if(lines[i].length == 0)
                         continue;
                     var attrs = lines[i].split("\t");
                     if($(e.srcElement).hasClass("txtname")){
                         if(attrs.length==1){
                             $("#txtname"+index).val(attrs[0]).change();
                         }
                         else if(attrs.length == 2){
                             $("#txtname"+index).val(attrs[0]).change();
                             $("#txtnamecn"+index).val(attrs[1]).change(); 
                         }
                         else if(attrs.length == 3){
                             $("#txtname"+index).val(attrs[0]).change();
                             $("#txtnamecn"+index).val(attrs[1]).change();
                             selectType("seltype"+index,attrs[2]);
                         }
                         else if(attrs.length == 4){
                             $("#txtname"+index).val(attrs[0]).change();
                             $("#txtnamecn"+index).val(attrs[1]).change();
                             selectType("seltype"+index,attrs[2]);
                             $("#txtsize"+index).val(attrs[3]).change();
                         }
                     }

                     index=index+1;
                     if(i < lines.length-1 && lines[i+1].length > 0){
                         var ctls=$("#txtname"+index);
                         if(ctls.length==0){
                             AddRow(1);
                         }
                     }

                 }

                 return false;
             }
             else{
                 return true;
             }
         }

         function selectType(ctlId,v){
             var ctls=$("#"+ctlId);
             if(ctls.length > 0){
                 switch (v) {
                     case "字符":
                         v="1";
                         break;
                     case "整数":
                         v="2";
                         break;
                     case "数值":
                         v="3";
                         break;
                     case "日期":
                         v="4";
                         break;
                     case "大文本":
                         v="5";
                         break;
                     default:
                         break;
                 }
                 ctls.val(v).change();

             }
         }

         $(function(){ //初始化
             $("#Button1").click(function(){
                 $("#btnSubmit").click()
             });
             $("#btnSubmit").click(chkform);
             $("#btnAdd1").click(function(){AddRow(1);});
             $("#btnAdd5").click(function(){AddRow(5);});

             $(document).keydown(function(e){
                 if(e.ctrlKey && event.keyCode == 13)
                 {
                     AddRow(1);
                 }
             });
             $("input[type!=button],select").live("change",updatexml);
             $("input[type!=button]").live("keydown",function(e){
                 var i = parseInt($(this).attr("oindex")) ;
                 var ctlName = $(this).attr("name");
                 if(event.keyCode == _upKeyCode)
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
                 else if(event.keyCode == _downKeyCode)
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
                 else if(event.keyCode == 13 && ctlName == "txtsize")
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
                 else if((e.ctrlKey && (e.which == 86 || e.which==118) ) && ctlName=="txtname"){
                     return pasteList(e);
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
            var xmlobj =fldmodel.find("td[oindex="+i+"]");
            var txtname=this.name;
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
        var regfldsize=[/^[1-9]+\d*$/,/^$/,/^[1-9]+\d*,[1-9]+\d*$/,/^$/,/^$/,/^$/];
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

    }		
    </script>
</asp:Content>
