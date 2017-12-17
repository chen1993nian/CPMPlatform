<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DefDictItems.aspx.cs" Inherits="EIS.Studio.SysFolder.DefFrame.DefDictItems"  ValidateRequest="false"%>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>字典编辑</title>
    <link rel="stylesheet" type="text/css" href="../../css/DefStyle.css"/>
    <script type="text/javascript" src="../../js/jquery-1.4.2.min.js"></script>
    <style type="text/css">
	body {
        margin:0;
        padding:0;
        border:medium none;
        overflow:auto;
    }
    #tblfield
    {
	    border-collapse: collapse;
        margin-left:auto;
	    margin-right:auto;
	    font-size: 12px;
	    line-height:20px;
	    border:1px solid #a0a0a0;
	    color:#393939;
	    background:#ebf3ff;
	    width:500px;
    }
    #tblfield td,th
    {
        border:1px solid #a0a0a0;
        padding:2px;
    }
    .textbox
    {
        width:95%;
    }
    .red{color:Red;}
    .focus{background:lightgreen;}
    .tip{width:490px;margin-left:auto;margin-left:auto;}
    </style>
    <script type="text/javascript">

        function AddRow(n)
        {
            for(var i=0;i<n;i++)
            {
                var strhtml="";
                var newEle = $("<tr><td></td><td></td><td></td><td></td></tr>");

                $("td:eq(0)", newEle).append("<input id=\"txtname" + rowcount + "\" class='textbox'  type=\"text\" size=\"20\" oindex=" + rowcount + " name=\"txtname\">");
                $("td:eq(1)", newEle).append("<input id=\"txtcode" + rowcount + "\" class='textbox'  type=\"text\" oindex=" + rowcount + " name=\"txtcode\">");
                $("td:eq(2)", newEle).append("<input id=\"txtorder" + rowcount + "\" class='textbox' type=\"text\" size=\"20\" oindex=" + rowcount + " name=\"txtorder\">");
                $("td:eq(3)",newEle).append("<input type=\"button\" value=\"删除\" onclick=\"dropfield('"+rowcount+"');\">");
                newEle.attr("align","center");
                $("#tblfield").append(newEle);
                //$("input,select",newEle).change(updatexml);
                fldmodel.append("<td oindex='" + rowcount + "' txtcode='' txtname='' txtorder='' state='add'/>"); //处理XML
                $("#txtname" + rowcount).focus().select();
                rowcount++;
            }

        }
        function success() {
            //window.opener.app_query();
            //window.close();
            frameElement.lhgDG.curWin.app_query();
            frameElement.lhgDG.cancel();
        }
        function winClose() {
            //window.close();
            frameElement.lhgDG.cancel();
        }
    </script>
</head>
<body  scroll="auto">
    <form id="form1" runat="server">

    <div class="menubar">
        <div class="topnav">
        <span style="float:left;margin-left:10px">字典维护&nbsp;&nbsp;</span>

            <span style="right:10px;display:inline;float:right;position:fixed;line-height:30px;top:0px;">
                <a class='linkbtn' href="javascript:" onclick="AddRow(1);">添加行(Ctrl+Enter)</a>
				<em class="split">|</em>
                <asp:LinkButton CssClass="linkbtn" ID="LinkButton1" runat="server" OnClick="LinkButton1_Click" >保存</asp:LinkButton>
				<em class="split">|</em>
                <a class='linkbtn' href="javascript:" onclick="winClose();" >关闭</a>
            </span>
        </div>
    </div>
    <div id="maindiv">
    <br />
		<table class="tblfield" align="center"  border="0" style="width:500px;margin-bottom:10px;">
        <tbody>
            <tr>
                <td width="70">字典名称：</td>
                <td width="180">
                    <asp:TextBox ID="TextBox1" Width="170" CssClass="textbox" runat="server"></asp:TextBox>
                </td>
                <td width="70">字典编码：
                </td>
                <td>
                    <asp:TextBox ID="TextBox2" Width="170" CssClass="textbox" runat="server"></asp:TextBox>                
                </td>
            </tr>
        </tbody>
        </table>
        <div style="margin-left:auto;margin-right:auto;width:500px;line-height:22px;">
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1"  runat="server" 
        ErrorMessage="字典名称不能为空" ControlToValidate="TextBox1" Display="Dynamic" CssClass="red"></asp:RequiredFieldValidator>
        </div>
        <%=Tips %>
		<table id="tblfield" align="center"  border="1" bgcolor="#ebf3ff">
				<tr>
					<td align="center" width="120" height="25">显示值</td>
					<td align="center" width="150" >存储值</td>
					<td align="center" width="50">排序</td>
					<td align="center" width="40">删除</td>
				</tr>
                <%=fieldHTML %>

		</table>
    </div>
    <br />
    <input id="state" type="hidden"  name="state" value='<%=hidstate %>'/>
    <input id="txtxml" type="hidden"  name="txtxml" value=''/>
    <script type="text/javascript">
        var _upKeyCode = 38;
        var _downKeyCode = 40;
        var _insertKeyCode = 45;
        var rowcount=<%=rowcount %>;
            var fldmodel=$("<tr><%=fieldXML %></tr>");
        $(function(){ //初始化
            $("#LinkButton1").click(chkform);
            $(document).keydown(function(e){
                if(e.ctrlKey && event.keyCode == 13)
                {
                    AddRow(1);
                }
            });
            $("input[type!=button],select").live("change",updatexml);
            $("input[type!=button]").live("keydown",function(){
                var i = parseInt($(this).attr("oindex")) ;
                var ctlName = $(this).attr("name");
                if(event.keyCode == _upKeyCode)
                {
                    //向上
                    var ctl=$("#"+ctlName+(i-1));
                    if(ctl.length>0)
                        ctl.focus().select();
                }
                else if(event.keyCode == _downKeyCode)
                {
                    //向下
                    var ctl=$("#"+ctlName+(i+1));
                    if(ctl.length>0)
                        ctl.focus().select();
                }

            });

            $("input[type!=button]").live("focus",function(){
                $(".focus").change().removeClass("focus");
                $(this).addClass("focus");
            });
        });
        function updatexml()
        {
            var i=$(this).attr("oindex");
            var xmlobj =fldmodel.find("td[oindex='"+i+"']");
            var txtname=this.name;
            var txtvalue=this.value||" ";
            var fldstate=xmlobj.attr("state");
            if(fldstate=="unchanged"){

                xmlobj.attr(txtname,txtvalue);
                xmlobj.attr("state","changed");
            }
            else if(fldstate=="changed"){
                xmlobj.attr(txtname,txtvalue);
            }
            else if(fldstate=="add"){
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
            $("#txtxml").val("<tr>"+fldmodel.html()+"</tr>");
			
        }		
		</script>
		
    </form>
</body>
</html>
