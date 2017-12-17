<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DefFieldsEvent.aspx.cs" Inherits="EIS.Studio.SysFolder.DefFrame.DefFieldsEvent" %>


<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>字段事件编辑</title>
    <meta http-equiv="Pragma" content="no-cache"/>
    <link rel="stylesheet" type="text/css" href="../../css/DefStyle.css"/>
    <script type="text/javascript" src="../../js/jquery-1.4.2.min.js"></script>
    <link href="../CodeMirror/lib/codemirror.css" rel="stylesheet" type="text/css" />
    <script src="../CodeMirror/lib/codemirror.js" type="text/javascript"></script>
    <script src="../CodeMirror/addon/format/formatting.js" type="text/javascript"></script>
    <script src="../CodeMirror/mode/javascript/javascript.js" type="text/javascript"></script>

    <style type="text/css">
	#fieldlist{
	    list-style-type: none; 
	    float: left; 
	    margin:0px 10px 0px 0px; 
	    background: aliceblue; 
	    padding: 2px; 
	    width: 96%; 
	    border:1px dashed teal;
	    text-align:left;
	    overflow:auto;
	    line-height:120%;
	}
	.li-field{
	    text-align:left; 
	    color:teal;
	    font-family:Tahoma,Helvetica,Arial,sans-serif;
	    margin: 0 5px 5px 2px; 
	    padding: 2px 0px 0px 2px; 
	    font-size:9pt;
        cursor:hand;
	}
	</style>
	
    <style type="text/css">
	html,body {
        margin:0;
        padding:0;
        border:medium none;
        overflow:hidden;
        height:100%;
        width:100%;
    }
    .textbox
    {
        width:95%;
    }
    .codetd{
     border:solid 1px #ccc;
     padding:0px;
     }
     .CodeMirror-wrapping{border:1px solid  #ddd;background-color:white;}
	</style>
	<script type="text/javascript">
	    var editor1;
	    $(function () {
	        $(".li-field").click(function () {

	            //editor1.replaceSelection($(this).attr("ref"));
	        });

	        var h = $(document).height() - 100;

	        editor1 = CodeMirror.fromTextArea(document.getElementById("TextBox1"), {
	            mode: "javascript",
	            tabMode: "shift",
	            lineNumbers: true,
	            autoMatchParens: true
	        });
	        editor1.setSize("100%", h - 60);
	    });
	</script>
</head>
<body>
    <form id="form1" runat="server">

    <div class="menubar">
      <div class="topnav">

        <div style="float:left;margin-left:10px;width:240px;">业务名称：<%=tblname %>&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; </div>
        <table align="left" style="line-height:25px;">
        <tr>
        <td>字段名称：
            <asp:DropDownList ID="DropDownList2" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList2_SelectedIndexChanged">
            </asp:DropDownList>
        </td>
        <td>事件名称：
            <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
            </asp:DropDownList>
        </td>
        <td>&nbsp;
        <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click" >保存</asp:LinkButton>
        </td>
        <td>&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
        </tr>
        </table>
        </div>
    </div>


    <table width="98%" align="center" cellpadding="5">
    <tr>
        <td align="left" width="200" valign="top">
        <div id="fieldlist">
        <%=fieldlist1.ToString()%>
        </div>
        </td>
        <td valign="top">
            <div class="codetd">
            <asp:TextBox ID="TextBox1" runat="server" height="400px" TextMode="MultiLine" Width="96%"></asp:TextBox>
            </div>
        </td>
    </tr>
    </table>


    </form>
</body>
</html>
