<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DefTableRelationKey.aspx.cs" Inherits="EIS.Studio.SysFolder.DefFrame.DefTableRelationKey" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>主外键维护</title>
    <meta http-equiv="Pragma" content="no-cache"/>
    <link rel="stylesheet" type="text/css" href="../../css/DefStyle.css">
    <link rel="stylesheet" type="text/css" href="../../css/jquery.autocomplete.css">
    <script type="text/javascript" src="../../js/jquery-1.4.2.min.js"></script>
    <script type="text/javascript" src="../../js/jquery.notice.js"></script>
    <script type="text/javascript" src="../../js/jquery.autocomplete.js"></script>
    
    <style type="text/css">
	body {
        margin:0;
        padding:0;
        border:medium none;
        overflow:hidden;
    }
    #maintbl
    {
	    border-collapse: collapse;
        margin-left:auto;
	    margin-right:auto;
	    font-size: 12px;
	    line-height:20px;
	    border:#808080 1px solid;
	    color:#393939;
	    background:#ebf3ff;
	    width:1100px;
	    /*FFF9F2 FCFCFB*/
    }
    td
    {
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
<script type="text/javascript" language="javascript">

    function AddRow(n)
    {
        {
            var strhtml="";
            var newEle = $("<tr><td></td><td></td><td></td><td></td><td></td><td></td></tr>");
            $("td:eq(0)",newEle).append(rowcount);//序号
            $("td:eq(1)",newEle).append("<input id=\"MainTable\" class='textbox tblinput'  type=\"text\" size=\"20\" oindex="+rowcount+" name=\"MainTable\">");
            $("td:eq(2)",newEle).append("<input id=\"MainKey\"  class='textbox fieldinput'   type=\"text\" size=\"80\" oindex="+rowcount+" name=\"MainKey\">");
            $("td:eq(3)",newEle).append("<input id=\"SubTable\"  class='textbox tblinput'   type=\"text\" size=\"20\" oindex="+rowcount+" name=\"SubTable\">");
            $("td:eq(4)",newEle).append("<input id=\"SubKey\" class='textbox fieldinput' type=\"text\" size=\"20\" oindex="+rowcount+" name=\"SubKey\">");
            $("td:eq(5)",newEle).append("<input type=\"button\" value=\"删除\" onclick=\"dropfield('"+rowcount+"');\">");
        
            newEle.attr("align","center");
            $("#maintbl").append(newEle);
            $("input,select",newEle).change(updatexml);
            fldmodel.append("<td oindex='"+rowcount+"' MainTable='' MainKey='' SubTable='' SubKey='' state='add'/>");//处理XML
        
            rowcount++;
        }
    
    }
</script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="menubar">
  <div class="topnav">

        <span style="float:left;margin-left:10px">业务名称：<%=tblname %>&nbsp;&nbsp;
        </span>
        <span style="float:right;margin-right:10px">
            <a href="javascript:" onclick="AddRow(1);">添加行</a> &nbsp;
            <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click" >保存</asp:LinkButton>
        </span>
        </div>
   </div>

		<div id="tabs-1">
        <br />
			<table id="maintbl" align="center" border="1" >
				<tr>
				<td align="center" width="40" height="25">序号</td>
				    <td align="center" width="180" height="25">主键表名</td>
					<td align="center" width="180" height="25">主键字段</td>
					<td align="center" width="180" >外键表名</td>
					<td align="center" width="180">外键字段</td>
					<td align="center" width="40">删除</td>
					
				</tr>
                <%=fieldHTML %>

		</table>
        </div>


        <input id="txtxml" type="hidden"  name="txtxml" value=''/>
    	<script type="text/javascript" language="javascript">
    	    var rowcount=<%=rowcount %>;
    	    var fldmodel=$("<tr><%=fieldXML %></tr>");
    	    $(function(){ //初始化
    	        $("#LinkButton1").click(chkform);
    	        $("input,select").change(updatexml);
    	        $(".tblinput").live("focusin",function(){
    	            $(this).autocomplete({
    	                width:300
                        ,deferRequestBy:200
                        ,serviceUrl:"../common/AutoComplete.ashx"
                        ,"valuekey":"tablename"
                        ,params:{
                            "queryid":"tablelist",
                            "querykey":"tablename",
                            "condition":"1=1"
                        }
    	            });
    	        });
    	        $(".fieldinput").live("focusin",function(){

    	            var el=$(this);
    	            var tblname = el.parent().prev().find(":text").val();
    	            var autoobj=null;
    	            if(el.data("autoobj"))
    	            {
    	                autoobj=el.data("autoobj");
    	            }
    	            else
    	            {
    	                autoobj=el.autocomplete({
    	                    width:200
                        ,serviceUrl:"../common/AutoComplete.ashx"
                        ,"valuekey":"fieldname"
                        ,params:{
                            "queryid":"tablefields",
                            "querykey":"fieldname"
                        }
    	                });
    	                el.data("tblname",tblname);
    	                el.data("autoobj",autoobj);
    	            }
    	            if(autoobj.options)
    	                autoobj.options.params.condition="tablename='"+tblname+"'";
		        
    	            if(el.data("tblname") && tblname!=el.data("tblname"))
    	                autoobj.clearCache();		        
		        
    	        });
		    
		    
    	    });
    	    function updatexml()
    	    {
    	        var xmlobj =fldmodel.find("td[oindex='"+this.oindex+"']");
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
    	        $("input,select").each(function(){
		    
    	        });	
    	        if(!f)
    	            return false;
    	        $("#txtxml").val("<tr>"+fldmodel.html()+"</tr>");
			
    	    }	
		</script>
    </form>
</body>
</html>
