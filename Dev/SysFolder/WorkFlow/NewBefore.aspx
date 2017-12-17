<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewBefore.aspx.cs" Inherits="EIS.WebBase.SysFolder.WorkFlow.NewBefore" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>流程填报须知</title>
    <link rel="stylesheet" href="../../css/newsStyle.css" />

    <style type="text/css">
        
        input,button{
            padding:3px;
            }
            select{font-size:14px;}
        .wfbottom{text-align:left;}
    </style>
</head>
<body scroll="auto">
    <form id="form1" runat="server">
    <div class="cbox">
        <table width="100%">
        <tr>
            <td>
                <div class="ctitle"><%=defModel.WorkflowName%>&nbsp;<b style="color:red">[填报要求]</b>
			<span>更新时间：<%=updateTime%></span>
		</div>

                <div class="ccontent" style="text-align:left;overflow:auto;font-size:14px;line-height:180%;padding-left:30px;">
                <%=Infos %>
                </div>
            </td>
        </tr>
	<tr><td>
        <div class="wfbottom" style="border-top:1px dotted gray;"><br/>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:CheckBox ID="CheckBox1" runat="server" Text="下次不再显示" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="Button1" runat="server" Text="发起流程" onclick="Button1_Click" />
        </div>
	</td></tr></table>
    </div>
    </form>
</body>
</html>
