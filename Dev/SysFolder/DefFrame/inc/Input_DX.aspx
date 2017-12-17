<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Input_DX.aspx.cs" Inherits="Studio.JZY.SysFolder.DefFrame.inc.Input_DX" %>
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
        parent.frameElement.lhgDG.curWin.styleCallBack("<%=Request["key"] %>:<%=Request["titleName"] %>:" + $("#rvalue").val());
            parent.frameElement.lhgDG.cancel();
        }

        function Cancel() {
            parent.frameElement.lhgDG.cancel();
        }
        jQuery(function () {
            if (getOpenerValue("stylecode") == '<%=Request["key"] %>') {
                var arr = getOpenerValue("styletxt");
                jQuery("#rvalue").val(arr);

            }

            $("#selField").dblclick(function () {

                $("#rvalue").val($(this).val());
            });
        });
           function getOpenerValue(ctlName) {
               return parent.frameElement.lhgDG.curWin.document.getElementById(ctlName).value;
           }
           //-->
		</script>
        <style type="text/css">
            td{font-size:10pt;padding:5px;}
            input.btn{padding:3px 8px;height:26px;}
        </style>
	</head>
	<body>
    <form id="form1" runat="server">
        <br/><br/>
		<table id="Table2" width="90%" align="center" border="0">
			<tr>
				<td class="cnt" width="100%">
					<table id="Table1" style="border:1px solid #ddd; " width="100%" border="0">
						<tr>
							<td align="left" height="30" style="color:Blue;font-weight:bold;">金额大写：</td>
							</tr>
                         <tr>
							<td >
                                <input id="rvalue" type="text" style="width: 98%; height: 21px" size="51"/>
                            </td>
						</tr>

                        <tr><td style="font-size:8pt:height:30px;">
                                提示：请双击选择要参考字段名称（对应的小写金额字段名称）

                                <div style="float:right;">
			                    <input type="button" value="确认" class="btn" onclick="Confirm()"/> 
                                <input type="button" value="取消" class="btn" onclick="Cancel()"/>
			                    &nbsp;&nbsp;
                                </div>
				            </td>
			            </tr>
                        <tr>
                            <td>
                                <select id="selField" title="双击字段"  size="16" style="width:300px;float:left;">
                                    <%=sbList %>
                                </select>
                            </td>
                        </tr>
					</table>
                </td>
                </tr>
		</table>
        </form>
	</body>
</html>
