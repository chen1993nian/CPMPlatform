<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Input_Path.aspx.cs" Inherits="Studio.JZY.SysFolder.DefFrame.inc.Input_Path" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
	<head>
		<title>
			<%=Request["titleName"].ToString()%>
		</title>
		<link href="../../../css/appstyle.css" rel="stylesheet"/>
        <script type="text/javascript" src="../../../js/jquery-1.7.min.js"></script>
		
		<script type="text/javascript">
        <!--
    function Confirm() {
        if (window.rvalue.value == "") {
            alert("自定义值不能为空！");
            return false;
        }
        else {
            parent.frameElement.lhgDG.curWin.styleCallBack("<%=Request["key"] %>:<%=Request["titleName"] %>:" + window.rvalue.value);
	            parent.frameElement.lhgDG.cancel();
	        }

        }

        function Cancel() {
            parent.frameElement.lhgDG.cancel();
        }
        jQuery(function () {
            if (getOpenerValue("stylecode") == "<%=Request["key"] %>") {
                var arr = getOpenerValue("styletxt");
                jQuery("#rvalue").val(arr);

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
        </style>
	</head>
	<body>
        <br/><br/>
		<table id="Table2" width="90%" align="center" border="0">
			<tr>
				<td class="cnt" width="100%">
					<table id="Table1" style="border:1px solid #ddd; " width="100%" border="0">
						<tr>
							<td align="left" height="30" style="color:Blue;font-weight:bold;"><%=Request["titleName"].ToString()%>：</td>
							</tr>
                         <tr>
							<td >
                                <input id="rvalue" type="text" style="width: 100%; height: 21px" size="51"/>


                            </td>
						</tr>

                        <tr><td style="font-size:8pt:height:30px;">
                                提示：请输入相对根目录的路径，如SysFolder/AppFrame/xxx.aspx
				            </td>
			            </tr>
					</table>
                </td>
                </tr>

			<tr><td align="right"><br/>
			<input type="button" value="确认" class="btn" onclick="Confirm()"/> 
            <input type="button" value="取消" class="btn" onclick="Cancel()"/>
			&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
			</td></tr>
		</table>
	</body>
</html>
