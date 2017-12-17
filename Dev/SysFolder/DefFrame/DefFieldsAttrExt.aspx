<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DefFieldsAttrExt.aspx.cs" Inherits="EIS.Studio.SysFolder.DefFrame.DefFieldsAttrExt" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>列表样式维护</title>
    <link rel="stylesheet" type="text/css" href="../../css/DefFrame.css">
    <script type="text/javascript" src="../../js/jquery-1.4.2.min.js"></script>
    <script type="text/javascript" src="../../js/jquery.notice.js"></script>
    
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
    #seldefvaluetype
    {
    width:100px;
    }
    </style>
        <script type="text/javascript">

            function AddRow(n)
            {
                for(var i=0;i<n;i++)
                {
                    var strhtml="";
                    var newEle = $("<tr><td></td><td align='left'></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td></tr>");

                    strhtml = "<select id=\"txtname" + rowcount + "\" size=\"1\" oindex=" + rowcount + "  name=\"txtname\">";
                    strhtml=strhtml+"   <option value='' selected></option>";
                    strhtml=strhtml+"   <%=fieldHTML2 %>";
        strhtml=strhtml+"	</select>";
        
        $("td:eq(0)",newEle).append(rowcount);//序号
        $("td:eq(1)",newEle).append(strhtml);//字段名称
        $("td:eq(2)", newEle).append("<input id=\"txtnamecn" + rowcount + "\" class='textbox'  type=\"text\" size=\"20\" oindex=" + rowcount + " name=\"txtnamecn\">");
        $("td:eq(3)", newEle).append("<input id=\"chklistdisp" + rowcount + "\"  checked  type=\"checkbox\" size=\"20\" oindex=" + rowcount + " name=\"chklistdisp\">");
        $("td:eq(4)", newEle).append("<input id=\"chkquerydisp" + rowcount + "\"   type=\"checkbox\" size=\"20\" oindex=" + rowcount + " name=\"chkquerydisp\">");
        $("td:eq(5)", newEle).append("<select id=\"selalign" + rowcount + "\" size='1' oindex=" + rowcount + "  name=\"selalign\">"
	            +"  <option value=\"1\" selected>左</option>"
	            +"  <option value=\"2\">中</option>"
	            +"	<option value=\"3\">右</option>"
	            +"	</select>" 
        );

        $("td:eq(6)", newEle).append("<input id=\"txtwidth" + rowcount + "\" class='textbox' type=\"text\" value='100' size=\"20\" oindex=" + rowcount + " name=\"txtwidth\">");
        $("td:eq(7)", newEle).append("<input id=\"txtrender" + rowcount + "\" class='textbox' type=\"text\" size=\"20\" oindex=" + rowcount + " name=\"txtrender\">");
        $("td:eq(8)", newEle).append("<input id=\"txtformatexp" + rowcount + "\" class='textbox' type=\"text\" size=\"20\" oindex=" + rowcount + " name=\"txtformatexp\">");
        $("td:eq(9)", newEle).append("<input id=\"txttotalexp" + rowcount + "\" class='textbox' type=\"text\" size=\"20\" oindex=" + rowcount + " name=\"txttotalexp\">");
        
        $("td:eq(10)",newEle).append("<input type=\"button\" value=\"删除\" onclick=\"dropfield('"+rowcount+"');\">");
        
        newEle.attr("align","center");
        $("#maintbl").append(newEle);
        $("input,select",newEle).change(updatexml);
        fldmodel.append("<td oindex='"+rowcount+"' txtname='' txtnamecn='' chklistdisp='1' chkquerydisp='0' selalign='1' txtwidth='100'  txtrender='' txtformatexp='' txttotalexp=''  state='add'/>");//处理XML
        
        rowcount++;
    }
    
}
</script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="menubar">
          <div class="topnav">

            <span style="float:left;margin-left:10px">业务名称：<%=tblName %>&nbsp;&nbsp;
            </span>
            <span style="float:right;padding-right:140px;">
                样式名称：<asp:DropDownList ID="DropDownList1" runat="server" Width="80px" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" AutoPostBack="True">
                    </asp:DropDownList>&nbsp;&nbsp;&nbsp;
                    <a href="javascript:" onclick="AddRow(1);">添加列</a>
                    <a href="javascript:" onclick="AddRow(5);">添加5列</a>
                
                <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click" >修改保存</asp:LinkButton>                
                    <asp:LinkButton ID="LinkButton2" runat="server" OnClick="LinkButton2_Click" >另存为新样式</asp:LinkButton>                
            </span>
        </div>
        </div>

        <br />
			<table id="maintbl" align="center" border="1">
            <thead>
				<tr>
				    <th align="center" width="40" height="25">序号</th>
					<th align="center" width="80" height="25">字段名</th>
					<th align="center" width="100" >中文名</th>
					<th align="center" width="70">列表显示</th>
					<th align="center" width="70">查询条件</th>
					<th align="center" width="50">列对齐</th>
					<th align="center" width="40">列宽</th>
					<th align="center" width="80">渲染函数名</th>
					<th align="center" width="80">格式</th>
					<th align="center" width="80">统计表达式</th>
					<th align="center" width="40">删除</th>
					
				</tr>
                </thead>
                <tbody>
                <%=fieldHTML %>
                </tbody>
		</table>
        <input id="txtxml" type="hidden"  name="txtxml" value=''/>
    	<script type="text/javascript">
    	    var _upKeyCode = 38;
    	    var _downKeyCode = 40;
    	    var rowcount=<%=rowcount %>;
    	    var fldmodel=$("<tr><%=fieldXML %></tr>");
    	    $(function(){ //初始化
    	        $("#LinkButton1").click(chkform);
    	        $("#LinkButton2").click(chkform);
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
    	        var txtvalue= $(this).val() || " ";
    	        if(this.type=="checkbox")
    	        {
    	            txtvalue=this.checked?"1":"0";
    	        } 
    	        var fldstate=xmlobj.attr("state");
    	        if(fldstate=="unchanged"){
    	            xmlobj.attr(txtname,txtvalue);
    	            xmlobj.attr("state","changed");
    	        }
    	        else{
    	            xmlobj.attr(txtname,txtvalue);                     
    	        }
    	        //更新XML
		        
    	    }
    	    function dropfield(oindex)
    	    {
    	        if(!confirm("您确定删除吗") )
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
    	    var regfldcn=/.+/;
    	    var regwidth=/^[1-9]+\d*$/;
    	    function chkform()  //判断字段长度输入是否规范
    	    {	
    	        var f=true;
    	        $("input.textbox,select").each(function(){
		    
    	            switch(this.name)
    	            {
    	                case "txtname":
    	                    if(!regfldcn.test($(this).val())){
    	                        alert("字段名不符合规定");
    	                        $(this).select();
    	                        $(this).focus();
    	                        f=false;
    	                        return false;
    	                    } 
    	                    break;
    	                case "txtnamecn":
    	                    if(!regfldcn.test($(this).val())){
    	                        alert("字段中文名不符合规定");
    	                        $(this).select();
    	                        f=false;
    	                        return false;
    	                    } 
    	                    break;
    	                case "txtwidth":
    	                    if(!regwidth.test($(this).val())){
    	                        alert("列宽不符合规定");
    	                        $(this).select();
    	                        f=false;
    	                        return false;
    	                    }
    	                    break;
    	            }
    	        });	
    	        if(!f)
    	            return false;
    	        $("#txtxml").val("<tr>"+fldmodel.html()+"</tr>");
			
    	    }	
		</script>
    </form>
</body>
</html>
