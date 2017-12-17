<%@ Page language="c#" Codebehind="AutoNumber.aspx.cs" AutoEventWireup="false" Inherits="EIS.Studio.SysFolder.DefFrame.AutoNumber" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
	<head>
		<title>自动编号</title>
		<meta name="vs_defaultClientScript" content="JavaScript"/>
        <script type="text/javascript" src="../../../js/jquery-1.7.min.js"></script>
		<link href="../../../css/appstyle.css" rel="stylesheet"/>
		<script type="text/javascript">
        function doOK()
        {	
            if ($("#head").val().indexOf("|")!=-1)
            {
	            alert ("编号头不能包含字符“|”");
	            return false;
            }
            //alertstr = "编号数字部分长度必须以数字表示!";
            if (isNaN($("#len").val()))
            {
	            alert("\"序号部分长度\"必须以数字表示！");
	            return false;
            }
            if (isNaN($("#init").val()))
            {
	            alert("序号部分起始值必须以数字表示！");
	            return false;
            }
            if ($("#end").val().indexOf("|")!=-1)
            {
	            alert ("编号尾不能包含字符“|”");
	            return false;
            }
            if ($("#len").val()=="")
            {
	            alert ("编号长度不能为空！");
	            return false;
            }
            if (Radio1.checked) //允许录入时修改自动编号
	            editFlag=1;
            else
	            editFlag=0;
            parent.frameElement.lhgDG.curWin.styleCallBack("<%=Request["key"] %>:<%=Request["titleName"] %>:"+head.value+"|"+len.value+'|'+init.value+'|'+end.value+'|'+editFlag);
            parent.frameElement.lhgDG.cancel();
        }

        function doCancel()
        {
            parent.frameElement.lhgDG.cancel();
        }
        jQuery(function(){
            if(getOpenerValue("stylecode")=="000")
            {
                var arr=getOpenerValue("styletxt").split("|");
                if(arr.length == 5)
                {
                    jQuery("#head").val(arr[0]);
                    jQuery("#len").val(arr[1]);
                    jQuery("#init").val(arr[2]);
                    jQuery("#end").val(arr[3]);
                    if(arr[4]=="1")
                        jQuery("#Radio1").attr("checked",true);
                    if(arr[4]=="0")
                        jQuery("#Radio2").attr("checked",true);
                }
            }
        });
        function getOpenerValue(ctlName) {
            return parent.frameElement.lhgDG.curWin.document.getElementById(ctlName).value;
        }
		</script>
		<style type="text/css">
		    td{font-size:12px}
		    input.btn{padding:3px 12px;height:28px;}
		    fieldset{border:1px solid gray;}
		    .mainZone{margin-left:auto;margin-right:auto;width:90%;}
		    
    .data-table{
        border-collapse:collapse;
        width:98%;
        font-size:12px;
        table-layout:fixed;
        background-color:#eee8aa;
        }
    .data-table >tbody>tr>td{
        vertical-align:middle;
        padding:3px;
        }
		</style>
	</head>
	<body >
		<br/>
        <div class="mainZone">
		<fieldset>
		<legend><b>请设置自动编号规则:</b></legend>
			<table width="100%" border="0" cellspacing="0" cellpadding="3">
				<tr>
					<td >编号头：</td>
					<td><input type='text' id="head" name='head' maxlength="100" style="width:300px;"/></td>
				</tr>
				<tr>
					<td>序号部分长度：</td>
					<td><input type='text' id="len" name='len' maxlength="2" style="width:40px;"/>
                    &nbsp;起始值：
					<input type='text' id="init" name='init' value='1' maxlength="5" style="width:40px;"/>
                    &nbsp;零补齐：
                        <input type="radio" name="edity" value="1" id="Radio1" checked="checked"/>
                        <label for="Radio1">允许</label> 
                        <input type="radio" name="edity" value="0" id="Radio2"/>
                        <label for="Radio2">不允许</label> 
                    </td>
				</tr>
				<tr>
					<td>编号尾：</td>
					<td><input type='text' id="end" name='end' value='' maxlength="10" style="width:300px;"/></td>
				</tr>
	    </table>

    </fieldset>
        <div style="text-align:right;padding:5px 5px 0px 5px;">
        		<input type="button" value="确认" class="btn" onclick="doOK()"/> 
			    <input type="button" value="取消" class="btn" onclick="doCancel()"/>
        </div>
        <div class="tip">
		    <b style="color:blue;">说明：</b>
            <br />1.&nbsp;&nbsp;年、月、日可以用{年}、{月}、{日}表示，{年2}表示两位表示的年份
            <br />2.&nbsp;&nbsp;编号头和编号尾可以引用Session，语法为：[!Session键值!]
            <br />3.&nbsp;&nbsp;编号头和编号尾可以引用表单中其它字段的值，语法为：{字段名称}
		    <br /><b style="color:blue;">示例：</b>
            <br />1.&nbsp;&nbsp;合同编号规则：年份 + 公司编码 + 3位序号
            <br />
                <table class="data-table" border="1">
                <tbody>
                    <tr>
                        <td width="120">编号头</td><td>{年}[!CompanyCode!]</td>
                    </tr>
                    <tr>
                        <td>序号部分长度</td><td>3</td>
                    </tr>
                    <tr>
                        <td>序号部分起始值</td><td>1</td>
                    </tr>
                    <tr>
                        <td>编号尾</td><td></td>
                    </tr>
                    <tr>
                        <td>是否补齐零</td><td>允许</td>
                    </tr>
                </tbody>

                </table>
            2.&nbsp;&nbsp;单项工程编号规则：项目编码 + 2位序号
                <table border="1" class="data-table">
                    <tr>
                        <td width="120">编号头</td><td>{ProjectCode}</td>
                    </tr>
                    <tr>
                        <td>序号部分长度</td><td>2</td>
                    </tr>
                    <tr>
                        <td>序号部分起始值</td><td>1</td>
                    </tr>
                    <tr>
                        <td>编号尾</td><td></td>
                    </tr>
                    <tr>
                        <td>是否补齐零</td><td>允许</td>
                    </tr>
                </table>
        </div>
    </div>
	</body>
</html>
