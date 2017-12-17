<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DefTableUI.aspx.cs" Inherits="EIS.Studio.SysFolder.DefFrame.DefTableUI" ValidateRequest="false" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>模板编辑</title>
    <meta http-equiv="X-UA-Compatible" content="IE=Edge" />
    <meta http-equiv="Pragma" content="no-cache"/>
    <link rel="stylesheet" type="text/css" href="../../css/DefStyle.css"/>

    <script type="text/javascript" src="../../js/jquery-1.4.2.min.js"></script>
    <script type="text/javascript" src="../../js/lhgdialog.min.js"></script>

    <link href="../CodeMirror/lib/codemirror.css" rel="stylesheet" type="text/css" />
    <link href="../CodeMirror/addon/fold/foldgutter.css" rel="stylesheet" type="text/css" />
    <link href="../CodeMirror/addon/display/fullscreen.css" rel="stylesheet" >
    <script src="../CodeMirror/lib/codemirror.js" type="text/javascript"></script>
    <script src="../CodeMirror/addon/edit/closetag.js" type="text/javascript"></script>
    <script src="../CodeMirror/addon/edit/matchtags.js" type="text/javascript"></script>

    <script src="../CodeMirror/addon/fold/foldcode.js"></script>
    <script src="../CodeMirror/addon/fold/foldgutter.js"></script>
    <script src="../CodeMirror/addon/fold/brace-fold.js"></script>
    <script src="../CodeMirror/addon/fold/xml-fold.js"></script>
    <script src="../CodeMirror/addon/display/fullscreen.js"></script>
    <script src="../CodeMirror/mode/xml/xml.js" type="text/javascript"></script>
    <script src="../CodeMirror/mode/javascript/javascript.js" type="text/javascript"></script>
    <script src="../CodeMirror/mode/css/css.js" type="text/javascript"></script>
    <script src="../CodeMirror/mode/htmlmixed/htmlmixed.js" type="text/javascript"></script>
    <style type="text/css">
    a{text-decoration:none;}
	a:hover{text-decoration:underline;}
    #fieldlist{
	    list-style-type: none; 
	    float: left; 
	    background: aliceblue; 
	    padding: 2px; 
	    width: 95%; 
	    height:530px;
	    border:1px dashed teal;
	    text-align:left;
	    overflow:auto;
	    
	}
	.li-field{
	    text-align:left; 
	    color:teal;
	    font-family:Tahoma,Helvetica,Arial,sans-serif;
	    margin:1px; 
	    padding: 1px 0px 0px 1px;
	    display:inline-block;
	    font-size:9pt;
        cursor:hand;
	}
	.codetd{
         border:solid 1px gray;
         padding-right:10px;
         text-align:left;
     }
	body {
        margin:0;
        padding:0;
        border:medium none;
        overflow:hidden;
    }
    .textbox
    {
        width:95%;
    }
    #fieldtd
    {
    	padding:5px;
    	}
    #txtStyleName
    {
    	vertical-align:middle;
    	float:none;
    }
    a{text-decoration:none;color:blue;}
    a:hover{text-decoration:none;color:Red;}
    .curstyle{color:red;}
	</style>
	<script type="text/javascript">
	    var editor1;
	    var _curClass = new Object();
	    $(function () {
	        _curClass = EIS.Studio.SysFolder.DefFrame.DefTableUI;
	        var t = parseInt("<%=t %>") - 1;
	        $(".styleLink:eq(" + t + ")").addClass("curstyle");

	        //界面预览
	        $("#btnPreview").click(function () {
	            var sindex = "<%=styleIndex %>";
	            var para = "tblName=<%=tblName %>&sindex=" + (sindex == "0" ? "" : sindex);
	            para = _curClass.CryptPara(para).value;
	            var dlg = new $.dialog({
	                title: '界面预览', page: '../AppFrame/AppInput.aspx?para=' + para
                    , btnBar: true, cover: true, lockScroll: true, width: 900, height: 600, bgcolor: 'gray', cancelBtnTxt: '关闭',
	                onCancel: function () {
	                }
	            });
	            dlg.ShowDialog();
	        });

	        $("#DropDownList1").change(function () {
	            var i = $(this).val();
	            var para = "tblName=<%=tblName %>&styleindex=" + i + "&t=<%=t%>";
	            para = _curClass.CryptPara(para).value;
	            window.open("DefTableUI.aspx?para=" + para, "_self");
	            return false;
	        });

	        $(".li-field").click(function () {
	            editor1.replaceSelection($(this).attr("ref"), "");
	        });
	        $("#btnField").click(function () {
	            $("#fieldtd").toggle();
	        });
	        var h = $(document).height() - 100;
	        $("#codetd").height(h);

	        editor1 = CodeMirror.fromTextArea(document.getElementById("TextBox1"), {
	            mode: "text/html",
	            lineWrapping: true,
	            tabMode: "shift",
	            lineNumbers: true,
	            autoMatchParens: true,
	            autoCloseTags: true,

	            readOnly: false,
	            cursorHeight: 1,
	            foldGutter: true,
	            gutters: ["CodeMirror-linenumbers", "CodeMirror-foldgutter"],
	            matchTags: { bothTags: true },
	            extraKeys: {
	                "Ctrl-J": "toMatchingTag"
                            , "F11": F11
                            , "Esc": function (cm) { if (cm.getOption("fullScreen")) { $(".menubar").show(); cm.setOption("fullScreen", false); } }
	            }
	        });

	        editor1.setSize("100%", "100%");

	        $("#btnFormat").click(function () {
	            CodeMirror.commands["selectAll"](editor1);
	            var range = getSelectedRange();
	            editor1.autoFormatRange(range.from, range.to);
	        });

	        $("#A1").click(function () {
	            F11(editor1);
	        });
	    });
        function F11(cm) { if (cm.getOption("fullScreen")) { $(".menubar").show(); } else { $(".menubar").hide(); } cm.setOption("fullScreen", !cm.getOption("fullScreen")); }
        function getSelectedRange() {
            return { from: editor1.getCursor(true), to: editor1.getCursor(false) };
        }
	</script>
</head>
<body>
    <form id="form1" runat="server">

    <div class="menubar">
      <div class="topnav">

    <table width="100%" border="0">
        <tr>
            <td style="width:260px;">
            业务名称：<%=tblName %>
            </td>
            <td>
                切换界面：
                <asp:DropDownList ID="DropDownList1" runat="server">
                </asp:DropDownList>
                &nbsp;&nbsp;界面名称：<asp:TextBox ID="txtStyleName" CssClass="textbox"  runat="server" width="100px"></asp:TextBox>&nbsp;
                <a class="styleLink" href="DefTableUI.aspx?tblName=<%=tblName %>&styleindex=<%=styleIndex%>&t=1" target="_self">[编辑界面]</a> 
                <a class="styleLink" href="DefTableUI.aspx?tblName=<%=tblName %>&styleindex=<%=styleIndex%>&t=2" target="_self">[手机界面]</a> 
                <a class="styleLink" href="DefTableUI.aspx?tblName=<%=tblName %>&styleindex=<%=styleIndex%>&t=3" target="_self">[打印界面]</a> 
                <a class="styleLink" href="DefTableUI.aspx?tblName=<%=tblName %>&styleindex=<%=styleIndex%>&t=4" target="_self">[查看界面]</a> 
            </td>
            <td></td>
            <td >
                <a href="DefTableStyleList.aspx?tblName=<%=tblName %>" target="_self">返回列表</a>
                <a href="DefTableUI2.aspx?tblName=<%=tblName %>&styleindex=<%=styleIndex%>&t=<%=t%>" target="_self">编辑器界面</a>
                <a href="javascript:" id="A1">全屏切换(F11)</a>
                <a href="javascript:" id="btnPreview">界面预览</a>
                <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click" >保存</asp:LinkButton>
            </td>
        </tr>
    </table>



            
        </div>
    </div>
   <table width="100%" align="center">
    <tr>
        <td align="left" width="200" valign="top" id="fieldtd">
        
        <div id="fieldlist">
        <%=fieldlist1.ToString()%>
        </div>
        </td>
        <td align="center" style="padding:10px;" valign="top">
            <div id="codetd" class="codetd">
            <asp:TextBox ID="TextBox1" runat="server"  TextMode="MultiLine" Width="99%"></asp:TextBox>
            </div>
        </td>
     </tr>
  </table>

    </form>
</body>
</html>
