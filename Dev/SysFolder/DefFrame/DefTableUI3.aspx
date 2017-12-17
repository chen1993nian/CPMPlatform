<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DefTableUI3.aspx.cs" Inherits="EIS.Studio.SysFolder.DefFrame.DefTableUI3" ValidateRequest="false"%>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>手机模板编辑</title>
    <meta http-equiv="Pragma" content="no-cache"/>
    <meta http-equiv="Content-Type" content="text/html;charset=utf-8"/>
    <link rel="stylesheet" type="text/css" href="../../css/DefStyle.css"/>
    <script type="text/javascript" src="../../js/jquery-1.4.2.min.js"></script>
    <script type="text/javascript" src="../../js/jquery.cookie.js"></script>
    <script type="text/javascript" src="../tiny_mce/tiny_mce.js"></script>

    <style type="text/css">
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
	.li-field , .fieldattr{
	    text-align:left; 
	    color:teal;
	    font-family:Tahoma,Helvetica,Arial,sans-serif;
	    margin:1px; 
	    padding: 1px 0px 0px 1px;
	    display:inline-block;
	    font-size:9pt;
        cursor:hand;
	}
	.titleZone{
	    background:url(../../img/toolbar/bg.gif) repeat-x;
	    display:block;
        height:26px;
        line-height:26px;
        padding-left:5px;
        margin:1px;
        border:1px solid lightblue;
        cursor:pointer;
	    }
	.titleZone a{line-height:24px;padding:1px 0px 1px 20px;
	             background:transparent url(../../img/common/site.png) no-repeat}
	html,body {
        margin:0;
        padding:0;
        border:medium none;
        overflow:hidden;
        height:100%;
    }
    #form1{height:100%;}
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
    .mceEditor{display:block;width:900px;}
    .hidden{display:none;}
    
    a{text-decoration:none;color:blue;}
    a:hover{text-decoration:none;color:Red;}
    .curstyle{color:red;}
	</style>
	<script type="text/javascript">
	    var editor = null;
	    var _dragData = "";
	    $(function () {
	        $(".li-field").click(function () {
	            editor.execCommand('mceInsertContent', false, $(this).attr("ref")); return false;
	        }).bind("drag", function (e) {
	            var ref = $(e.srcElement).attr("ref");
	            _dragData = ref;
	        });
	        $("#btnField").click(function () {
	            $("#fieldtd").toggle();
	        });
	        var _curClass = EIS.Studio.SysFolder.DefFrame.DefTableUI3;

	        function SaveData(showMsg) {
	            var ed = tinyMCE.getInstanceById("TextBox1");
	            var ret = _curClass.SaveData("<%=tblName %>", "<%=styleIndex %>", "<%=t %>", editor.getContent());
	            if (ret.error) {
	                alert("保存出错：" + ret.error.Message);
	            }
	            else {
	                if (showMsg)
	                    $.noticeAdd({ text: '保存成功！', stay: false, stayTime: 200 });
	            }
            }

	        $("#LinkButton1").click(function () {
	            SaveData(true);
	            return false;
	        });

	        $(".titleZone").attr("title", "展开/收缩").click(function () {
	            $(this).next().toggle();
	        });
	        tinyMCE.init({
	            language: 'cn',
	            mode: "exact",
	            elements: "TextBox1",
	            theme: "advanced",
	            verify_html: false,
	            plugins: "autolink,lists,pagebreak,style,table,save,advhr,advimage,advlink,inlinepopups,preview,searchreplace,contextmenu,paste,directionality,fullscreen,noneditable,nonbreaking,xhtmlxtras,template,advlist",

	            // Theme options
	            theme_advanced_buttons1: "code,undo,redo,|,cut,copy,paste,pastetext,pasteword,|,forecolor,backcolor,|,bold,italic,underline,strikethrough,|,justifyleft,justifycenter,justifyright,styleselect,formatselect,fontselect,fontsizeselect",
	            theme_advanced_buttons2: "tablecontrols,|search,replace,|,bullist,numlist,|,link,unlink,image,|,removeformat,visualaid,|,sub,sup,|,charmap,|,fullscreen,attribs,template",
	            theme_advanced_toolbar_location: "top",
	            theme_advanced_toolbar_align: "left",
	            theme_advanced_statusbar_location: "bottom",
	            theme_advanced_resizing: true,
	            theme_advanced_path: false,
	            theme_advanced_fonts: "宋体," +
                    "Arial=arial,helvetica,sans-serif;" +
                    "Arial Black=arial black,avant garde;" +
                    "Courier New=courier new,courier;",
	            content_css: "../../css/editorMobile.css",

	            extended_valid_elements: "pre[name|class],style",
	            valid_children: "+body[style],+body[link]",

	            width: "900px",
	            height: "540px",
	            template_external_list_url: "tmpl/template_list.js",
	            external_link_list_url: "lists/link_list.js",
	            external_image_list_url: "lists/image_list.js",
	            media_external_list_url: "lists/media_list.js",

	            template_replace_values: { username: "Some User", staffid: "991234" },
	            oninit: function () { editor = tinyMCE.getInstanceById("TextBox1"); },
	            setupcontent_callback: function (editor_id, b, doc) {
	                b.ondrop = function () {
	                    var ed = tinyMCE.getInstanceById(editor_id);
	                    ed.execCommand('mceInsertContent', false, _dragData);
	                    return false;
	                }

	            }
	        });
	    });
	</script>
</head>
<body style="height:100%;">
    <form id="form1" runat="server">

    <div class="menubar">
      <div class="topnav">

        <span style="float:left;margin-left:10px">
        业务名称：<asp:DropDownList ID="ddlTblName" runat="server" Width="120">
            </asp:DropDownList>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        切换界面：
            <asp:DropDownList ID="DropDownList1" runat="server">
            </asp:DropDownList>
        &nbsp;&nbsp;
        &nbsp;
            <a class="styleLink" href="DefTableUI2.aspx?tblName=<%=tblName %>&styleindex=<%=styleIndex%>&t=1" target="_self">[编辑界面]</a> 
            <a class="curstyle" href="DefTableUI3.aspx?tblName=<%=tblName %>&styleindex=<%=styleIndex%>&t=2" target="_self">[手机界面]</a> 
            <a class="styleLink" href="DefTableUI4.aspx?tblName=<%=tblName %>&styleindex=<%=styleIndex%>&t=3" target="_self">[打印界面]</a> 
            <a class="styleLink" href="DefTableUI2.aspx?tblName=<%=tblName %>&styleindex=<%=styleIndex%>&t=4" target="_self">[查看界面]</a> 

        </span>
        <span style="float:right;margin-right:10px">
            <a href="DefTableStyleList.aspx?tblName=<%=tblName %>" target="_self">返回列表</a>
            <a href="DefTableUI.aspx?tblName=<%=tblName %>&styleindex=<%=styleIndex%>&t=<%=t%>" target="_self">HTML界面</a> 
            <a href="javascript:" id="btnField">字段参考</a> 
            <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click" >保存</asp:LinkButton>
            
        
        </span>
    </div>
    </div>
    <div style="text-align:left;height:100%">
   <table width="100%" height="100%" align="center">
    <tr>
        <td align="left" width="200" valign="top" id="fieldtd">
            <div id="fieldlist">
            <%=fieldlist1.ToString()%>
            </div>
        </td>
        <td align="center" valign="top">
            <asp:TextBox ID="TextBox1" Width="96%" CssClass="hidden tinymce" runat="server"  TextMode="MultiLine"></asp:TextBox>
        </td>
     </tr>
  </table>
  </div>



    </form>
</body>
</html>