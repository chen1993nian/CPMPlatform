<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="IdeaTemplate.aspx.cs" Inherits="EIS.Web.SysFolder.WorkFlow.WfIdeaTemplate" ValidateRequest="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>意见模板编辑</title>
    <link rel="stylesheet" type="text/css" href="../../css/DefStyle.css"/>
    <script type="text/javascript" src="../../js/jquery-1.4.2.min.js"></script>
    <style type="text/css">
	body {
        margin:0;
        padding:0;
        border:medium none;
        overflow:auto;
    }
    .txtname{
        width:390px;
    }
	.txtorder{width:35px;}
	#tblfield{
	    table-layout:fixed;
		border-collapse: collapse;
	    margin-left:auto;
		margin-right:auto;
		font-size: 12px;
		width:500px;
		line-height:20px;
		border:1px solid gray;
		background:#ebf3ff;
	}
	#tblfield th,#tblfield td{background:#ebf3ff;border:1px solid gray;padding-right:2px;}

    </style>
    <script type="text/javascript">

        function AddRow(n)
        {
            for(var i=0;i<n;i++)
            {
                var strhtml="";
                var newEle = $("<tr><td></td><td></td><td></td></tr>");


                $("td:eq(0)", newEle).append("<input class='textbox txtname'  type=\"text\" size=\"20\" oindex=" + rowcount + " name=\"txtname\">");
                $("td:eq(1)",newEle).append("<input class='textbox txtorder' type=\"text\" size=\"20\" oindex="+rowcount+" name=\"txtorder\">");
                $("td:eq(2)", newEle).append("<input type=\"button\" class='btnDel' value='删除' onclick=\"dropfield('" + rowcount + "');\">");
                newEle.attr("align", "center");
                $("#tblfield").append(newEle);

                fldmodel.append("<td oindex='"+rowcount+"'  txtname='' txtorder='' state='add'/>");
                
                rowcount++;
            }
            $("input[class=textbox]").change(function(){
                updatexml();
            });
        }
        function afterSave() {
            frameElement.lhgDG.cancel();
            frameElement.lhgDG.curWin.ReLoadIdea();
        }
	</script>
</head>
<body  scroll="auto">
    <form id="form1" runat="server">

    <div class="menubar">
        <div class="topnav">
        <span style="float:left;margin-left:10px">意见模板编辑：&nbsp;&nbsp;
        </span>
        <span style="float:right;margin-right:10px">
            <a class='linkbtn' href="javascript:" onclick="AddRow(1);">添加行</a>
			<em class="split">|</em>
			<asp:LinkButton ID="LinkButton1" CssClass="linkbtn" runat="server" OnClick="LinkButton1_Click" >保存</asp:LinkButton>
        </span>
        </div>
    </div>
    <div id="maindiv">
    <br />
		<table id="tblfield" align="center">
				<tr>
					<th align="center" width="400" height="25">意见模板</th>
					<th align="center" width="50">排序</th>
					<th align="center" width="50">删除</th>
				</tr>
                <%=fieldHTML %>

		</table>
    </div>
    <br />
    <input id="state" type="hidden"  name="state" value='<%=hidstate %>'/>
    <input id="txtxml" type="hidden"  name="txtxml" value=''/>
    <script type="text/javascript">
        var rowcount=<%=rowcount %>;
        var fldmodel=$("<tr><%=fieldXML %></tr>");
        $(function(){ //初始化
            $("#LinkButton1").click(chkform);
            $("input[type!=button]").live("change",updatexml);
            //$("#tblfield").tableDnD();

        });
        function updatexml()
        {
            //更新XML
            var xmlobj =fldmodel.find("td[oindex='"+$(this).attr("oindex")+"']");
            var txtname=this.name;
            var txtvalue=this.value||" ";
            var fldstate=xmlobj.attr("state");
            if(fldstate=="unchanged"){
                $(xmlobj).attr(txtname,txtvalue);
                $(xmlobj).attr("state","changed");
            }
            else if(fldstate=="changed"){
                $(xmlobj).attr(txtname,txtvalue);
            }
            else if(fldstate=="add"){
                $(xmlobj).attr(txtname,txtvalue);
            }
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
        var regfldname=/^[a-zA-Z]+[\w_]*$/;
        var regfldcn=/.+/;
        var regfldsize=[/^[1-9]+\d*$/];
        function chkform()  //判断字段长度输入是否规范
        {	
            var f=true;
            $("input.textbox").each(function(){
		    
                switch(this.name)
                {
                    case "txtorder":
                        if(!regfldsize[0].test($(this).val())){
                            alert("排序不符合规定");
                            $(this).select();
                            f=false;
                            return false;
                        }
                        break;
                }
            });	
            if(!f)
                return false;
            $("#txtxml").val(escape("<tr>"+fldmodel.html()+"</tr>"));
        }
		</script>
		
    </form>
</body>
</html>
