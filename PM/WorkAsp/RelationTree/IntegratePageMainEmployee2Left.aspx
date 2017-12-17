<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="IntegratePageMainEmployee2Left.aspx.cs" Inherits="EIS.WorkAsp.IntegratedPage.WorkAsp.RelationTree.IntegratePageMainEmployee2Left" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>综合业务节点树</title>
	<link rel="stylesheet" href="../../css/zTreeStyle/zTreeStyle.css" type="text/css">
	<script type="text/javascript" src="../../js/jquery-1.4.4.min.js"></script>
	<script type="text/javascript" src="../../js/jquery.ztree.core-3.4.js"></script>
	<script type="text/javascript" src="../../js/jquery.ztree.excheck-3.4.js"></script>
	<script type="text/javascript" src="../../js/jquery.ztree.exedit-3.4.js"></script>
    <style>
    /*大表格样式*/
table.normaltbl,table.redtbl,table.subtbl
{
    table-layout:fixed;
	border-collapse: collapse;
    margin-left:auto;
	margin-right:auto;
	font-size: 12px;
	width:90%;
	line-height:20px;
}

table.normaltbl
{
	border:#808080 1px solid;
	color:#393939;
	background:#FAF8F8;
	/*FFF9F2 FCFCFB*/
}
.normaltbl > tr > td , .normaltbl > tbody > tr > td , .subtbl > tbody > tr > td, .subtbl > thead > tr > td
{
	text-align:left;
	padding:1px 1px 1px 3px;
	border:1px #808080 solid;
}
    </style>
	<script type="text/javascript">
		<!--
	    var setting = {
			view: {
				selectedMulti: false
			},
	        data: {
				key: {
				    title: "t",
					wbs:"wbs",
					pwbs:"pwbs"
				},
	            simpleData: {
	                enable: true
	            }
			}
	    };

	    var zNodes = [<%=this.FunNodeZTree_Script %>
		];

	    jQuery(document).ready(function () {
	        jQuery.fn.zTree.init(jQuery("#treeDemo"), setting, zNodes);
            //alert( window.parent.initurl  ) ;
            document.all("lnkurl").href = window.parent.initurl ;
	    });
		//-->
	</script>

</head>
<body style="margin:0;" scroll="auto"  oncontextmenu="return(false);">
    <form id="form1" runat="server">
         <table  width="100%"  height="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    <table class="normaltbl">
                        <tr>
                            <td colspan="2" align="center">
                                <img src='' id="img_photo" runat="server" height="200" width="200" alt='' style=''/>
                            </td>
                        </tr>
                        <tr><td width="50px;">姓名</td><td>
                            <a href="" id="lnkurl" target="RelationMain"><asp:Label ID="Lbl_EmployeeName" runat="server" Text="Label"></asp:Label></a>
                            </td></tr>
                        <tr><td>编号</td><td>
                            <asp:Label ID="Lbl_EmployeeCode" runat="server" Text=""></asp:Label>
                            &nbsp;</td></tr>
                        <tr><td>单位名称</td><td>
                            <asp:Label ID="Lbl_CompName" runat="server" Text=""></asp:Label>
                            &nbsp;</td></tr>
                        <tr><td>性别</td><td>
                            <asp:Label ID="Lbl_Sex" runat="server" Text=""></asp:Label>
                            &nbsp;</td></tr>
                        <tr><td>工作部门</td><td>
                            <asp:Label ID="Lbl_DeptName" runat="server" Text=""></asp:Label>
                            &nbsp;</td></tr>
                        <tr><td>职务岗位</td><td>
                            <asp:Label ID="Lbl_PositionName" runat="server" Text=""></asp:Label>
                            &nbsp;</td></tr>
                    </table>
                </td>
            </tr>
			<tr>
				<td valign="top">
                    <ul id="treeDemo" class="ztree"></ul>
				</td>
			</tr>
        </table>            
    <asp:HiddenField ID="HiddenField1" runat="server" />
    <asp:HiddenField ID="HiddenField2" runat="server" />
    </form>
</body>
</html>
