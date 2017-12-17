<%@ Page language="c#" Codebehind="SingleInput.aspx.cs" AutoEventWireup="false" Inherits="EIS.Studio.SysFolder.DefFrame.SingleInput" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
	<head>
		<title>
			Radio自定义值
		</title>
		<link href="../../../css/appstyle.css" rel="stylesheet"/>
        <script type="text/javascript" src="../../../js/jquery-1.7.min.js"></script>
		<script type="text/javascript">
        <!--
        function Confirm()
        {
			var txtSQL=$("#rvalue").val();
	        if (txtSQL == "")
	        {
		        alert("自定义值不能为空！");
		        return false;
	        }
	        else
	        {
				var arrRet=[];
				arrRet.push($("#selLayout").val());
				arrRet.push($("input[name=raddir]:checked").val());
				arrRet.push($("#colNum").val());
				arrRet.push($("#showNum").attr("checked")== "checked"?"1":"0");
		        parent.frameElement.lhgDG.curWin.styleCallBack("<%=Request["key"] %>:<%=Request["titleName"] %>:"+arrRet.join(",")+"`"+txtSQL);
		        parent.frameElement.lhgDG.cancel();
	        }
	
        }
        function Cancel()
        {
           parent.frameElement.lhgDG.cancel();
        }

        jQuery(function(){
            $("#selLayout").click(function() {
                var v=$(this).val();
                if(v == "0")
                    $("#raddir1,#raddir2,#colNum").val("").prop("disabled",true);
                else
                    $("#raddir1,#raddir2,#colNum").prop("disabled",false);

            });

            if(getOpenerValue("stylecode")=='<%=Request["key"] %>')
            {
                var pv=getOpenerValue("styletxt");
				if(pv.indexOf("`")>0)
				{
					var pvs=pv.split("`");
					var arr=pvs[0].split(",");
					$("#selLayout").val(arr[0]).click();
					if(arr[1]=="1")
						$("#raddir1").attr("checked","checked");
					else
						$("#raddir2").attr("checked","checked");
					$("#colNum").val(arr[2]);

					if(arr[3]=="1")
						$("#showNum").attr("checked", "checked");
					else
						$("#showNum").removeAttr("checked");

					jQuery("#rvalue").val(pvs[1]);
				}
				else{
					jQuery("#rvalue").val(pv);
				}

            }
            else {
                $("#selLayout").click();    
            }
        });
        function getOpenerValue(ctlName) {
            return parent.frameElement.lhgDG.curWin.document.getElementById(ctlName).value;
        }
        //-->
        </script>
        <style type="text/css">
            td{font-size:10pt;padding:5px;}
            input.btn{padding:3px 8px;height:28px;}
			.subtitle{color:#dc143c;}
			em{color:white;font-size:11px;padding:2px 3px;background-color:#1e90ff;border-radius:3px;font-style:normal;}
        </style>
	</head>
	<body >
        <br/><br/>
		<table id="Table2" width="90%" align="center" border="0">
			<tr>
				<td ><b style="color:Blue;font-weight:bold;">Radio显示：</b>自定义值</td>
			</tr>
			<tr>
				<td class="cnt" width="100%">
					<table id="Table1" style="border: #aaa 1px solid;"
						 width="100%" border="0">
						<tr>
							<td align="left" colspan="2" >
								<table>
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
											<input type="radio" name='raddir' id='raddir1' checked="checked" value="1" /><label for='raddir1'>水平</label>
											<input type="radio" name='raddir' id='raddir2' value="2" /><label for='raddir2'>垂直</label>
										</td>
										<td class='subtitle'>&nbsp;&nbsp;列(行)数:</td>
										<td>
											<input type="text" id="colNum" value="" size="3"/>
										</td>
										<td>&nbsp;&nbsp;
											<input type="checkbox" id="showNum"/><label for='showNum' class='subtitle'>&nbsp;显示序号</label>
										</td>
									</tr>
								</table>
							</td>
							</tr>
                         <tr>
							<td colspan="2">
                                <textarea id="rvalue" rows="10" style="width:100%;"></textarea>
                            </td>
						</tr>
					</table>
                </td></tr>
                <tr><td style="font-size:10pt;line-height:20px;">
                    <span style="color:blue;">分隔符：</span>一项占每行（用回车符分隔）；每项之间也可以使用逗号分隔<br />
					<span class='subtitle'>格式 1：</span>
                    显示1|值1<em>分隔符</em>
                    显示2|值2<em>分隔符</em>
                    显示3|值3<br />
					<span class='subtitle'>格式 2：</span>
                    显示1<em>分隔符</em>
                    显示2<em>分隔符</em>
                    显示3
				</td>
			</tr>
			<tr><td align="right">
			    <input type="button" value="确认" class="btn" onclick="Confirm()"/> 
                <input type="button" value="取消" class="btn" onclick="Cancel()"/>
			    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
			</td></tr>
		</table>
	</body>
</html>
