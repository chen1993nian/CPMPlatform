<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Input_Expression.aspx.cs" Inherits="EIS.Studio.SysFolder.DefFrame.inc.Input_Expression" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
	<head>
		<title>计算表达式</title>
		<link href="../../../css/appstyle.css" rel="stylesheet"/>
        <script type="text/javascript" src="../../../js/jquery-1.7.min.js"></script>
		<script type="text/javascript">
        <!--
       var t="<%=t %>";
       var tblName='<%=Request["tblname"]%>';
        function Confirm()
        {
	        if (jQuery("#rvalue").val() == "")
	        {
		        alert("自定义值不能为空！");
		        return false;
	        }
	        else
	        {
                var arr=["","",""];
                if(t=="1"){
                    var c = document.getElementById("RadioButtonList1_0").checked;
                    c = c ? "1":"2";
                    arr[0]=c;
                    if(c == "2"){
                        arr[1]=jQuery("#DropDownList1").val();
                    }
                    
                }
                else if(t=="2"){
                    arr[0]="3";
                }
                else{
                    parent.frameElement.lhgDG.cancel();
                }
                arr[2]=jQuery("#rvalue").val();
		        parent.frameElement.lhgDG.curWin.styleCallBack("<%=Request["key"] %>:计算表达式:"+arr.join("|"));
		        parent.frameElement.lhgDG.cancel();
	        }
	
        }

        function Cancel()
        {
           parent.frameElement.lhgDG.cancel();
        }
        jQuery(function(){
            var selTbl=tblName;
            if(getOpenerValue("stylecode")=='<%=Request["key"] %>')
            {
                //类型|子表名|表达式
                var arr = getOpenerValue("styletxt").split("|");
                if(arr.length==3)
                {
                    if(arr[0]=="1"){
                        document.getElementById("RadioButtonList1_0").checked = true;
                    }
                    else if (arr[0]=="2"){
                        document.getElementById("RadioButtonList1_1").checked = true;
                        jQuery("#DropDownList1").val(arr[1]);
                        selTbl=arr[1];
                    }
                    else{
                        jQuery("#trType").hide();
                    }
                    jQuery("#rvalue").val(arr[2]);
                }
                if(t=="2"){
                     jQuery("#trType").hide();                    
                }

            }
            else{
                if(t=="2"){
                     jQuery("#trType").hide();                    
                }
                else{
                     document.getElementById("RadioButtonList1_0").checked = true;
                }
            }
            //加载字段列表
            loadFields(selTbl);

            $(":radio").click(function(){
                if(this.value=="1"){
                    loadFields(tblName);
                }
                else{
                    var selTbl=$("#DropDownList1").val();
                    loadFields(selTbl);                
                }
            });
            $("#DropDownList1").change(function(){
                var selTbl=$(this).val();
                loadFields(selTbl);
            });
            $("input.op").click(function(){
                 $("#rvalue").insertAtCaret($(this).val()).focus();
            });
            $("input.rm").click(function(){
                 $("#rvalue").val("");
            });
            $("#selField").dblclick(function(){
                 $("#rvalue").insertAtCaret($(this).val()).focus();
            });

        });
        function getOpenerValue(ctlName) {
            return parent.frameElement.lhgDG.curWin.document.getElementById(ctlName).value;
        }
        function loadFields(t){
            var _curClass= EIS.Studio.SysFolder.DefFrame.inc.Input_Expression;
            var arrField=_curClass.GetFields(t).value;
            var arrHtml=[];
            for(var i=0;i<arrField.length;i++){
                var encn=arrField[i].split("|");
                arrHtml.push("<option value='{",encn[0],"}'>",encn[1]," [ ",encn[0]," ]</option>");
            }
            $("#selField").html(arrHtml.join(""));
        }
        $.fn.insertAtCaret = function (tagName) {
	        return this.each(function(){
		        if (document.selection) {
			        //IE support
			        this.focus();
			        sel = document.selection.createRange();
			        sel.text = tagName;
			        this.focus();
		        }else if (this.selectionStart || this.selectionStart == '0') {
			        startPos = this.selectionStart;
			        endPos = this.selectionEnd;
			        scrollTop = this.scrollTop;
			        this.value = this.value.substring(0, startPos) + tagName + this.value.substring(endPos,this.value.length);
			        this.focus();
			        this.selectionStart = startPos + tagName.length;
			        this.selectionEnd = startPos + tagName.length;
			        this.scrollTop = scrollTop;
		        } else {
			        this.value += tagName;
			        this.focus();
		        }
	        });
        };

        //-->
		</script>
        <style type="text/css">
            td{font-size:10pt;padding:5px;}
            input.btn{padding:3px 8px;height:26px;}
            option{line-height:120%;font-size:12px;}
            select{padding:2px;}
        </style>
	</head>
	<body>
        <br/>
    <form id="form1" runat="server">
		<table id="Table2" width="90%" align="center" border="0">
			<tr>
				<td class="cnt" width="100%">
					<table id="Table1" style="border:1px solid #ddd; " width="100%" border="0">
						<tr>
							<td align="left" height="30" style="color:Blue;font-weight:bold;">计算表达式：</td>
							</tr>
                        <tr id="trType">
							<td align="left" height="30" style="">
                                <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatLayout="Flow" RepeatDirection="Horizontal">
                                </asp:RadioButtonList>&nbsp;
                                <asp:DropDownList ID="DropDownList1" runat="server">
                                </asp:DropDownList>
                            </td>
							</tr>
                         <tr>
							<td >
                                <textarea id="rvalue" title="双击下面字段列表" rows="3" cols="70" style="color:green;font-size:10pt;font-weight:bold;line-height:150%;font-family:Tahoma,Helvetica,Arial,sans-serif,宋体;"></textarea>
                            </td>
						</tr>
                        <tr>
                            <td>
                                <input type="button" value="+"  class="btn op" title="加"/>
                                <input type="button" value="-"  class="btn op" title="减"/>
                                <input type="button" value="*"  class="btn op" title="乘"/>
                                <input type="button" value="/"  class="btn op" title="除"/>
                                <input type="button" value="("  class="btn op" title="左括号"/>
                                <input type="button" value=")"  class="btn op" title="右括号"/>
                                <input type="button" value="清空" class="btn rm" style="color:Red;"/> 
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <div style="float:right;">
                                <input type="button" value="确认" class="btn" onclick="Confirm()"/> 
                                <input type="button" value="取消" class="btn" onclick="Cancel()"/>
			                    </div>
                            </td>
                        </tr>
                        <tr>
                        <td>
                            <select id="selField" size="16" title="双击字段" style="width:200px;float:left;">
                            </select>

                                <div style="width:280px;float:right;font-size:10pt;height:30px;font-family:宋体;line-height:150%;">
                                    说明：<span style='color:Red;font-weight:bold;'>双击左边字段，格式为{字段名称}</span><br/>
                                    <ul style="list-style-type:decimal;padding-left:26px;padding-right:10px;">
                                        <li>主表字段表达式：<span style="color:green;font-weight:bold;">{货款}+{运费}</span></li>
                                        <li>主表合计子表字段(表达式)：首选选中对应的子表，然后只需填写子表表达式，如<span style="color:green;font-weight:bold;">{数量}*{单价}</span></li>
                                        <li>子表字段表达式：<span style="color:green;font-weight:bold;">{数量}*{单价}</span></li>
                                    </ul>
			                    </div>
                        </td>
			            </tr>
					</table>
                </td>
                </tr>

		</table>
    </form>

	</body>
</html>

