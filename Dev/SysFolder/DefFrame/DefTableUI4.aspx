<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DefTableUI4.aspx.cs" Inherits="EIS.Studio.SysFolder.DefFrame.DefTableUI4" ValidateRequest="false"  %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>打印模板编辑</title>
    <meta http-equiv="Pragma" content="no-cache"/>
    <meta http-equiv="Content-Type" content="text/html;charset=utf-8"/>
    <link rel="stylesheet" type="text/css" href="../../css/DefStyle.css"/>
    <script type="text/javascript" src="../../js/jquery-1.4.2.min.js"></script>
    <script type="text/javascript" src="../../js/jquery.cookie.js"></script>
    <script type="text/javascript" src="../tiny_mce/jquery.tinymce.js"></script>

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
	.li-table{background:url(../../img/toolbar/bg.gif) repeat-x;
	        display:block;
	        height:26px;
	        line-height:26px;
	        padding-left:5px;
	        margin:5px 0px;
	        border:1px solid lightblue;
	        }
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
    
    .middlebar{
        text-align:left;
        background-color:rgb(229, 241, 246);
        border:3px solid #eee;
        margin:5px 0px;
        padding:2px 3px;
        width:900px;
        }
    .middlebar a.linkbtn{padding:0px 5px;}
    .middlebar a.linkbtn:hover{
        background:#3e88c7;
        color:White;
        border-radius:2px;
        padding:0px 5px;
        text-decoration:none;
        }
        
    a{text-decoration:none;color:blue;}
    a:hover{text-decoration:none;color:Red;}
    .curstyle{color:red;}
	</style>
	<script type="text/javascript">
	    var editor = null;
	    var _dragData = "";
	    $(function () {
	        $(".li-field").click(function () {
	            $('#TextBox1').tinymce().execCommand('mceInsertContent', false, $(this).attr("ref")); return false;
	            //editor_a.execCommand('insertHtml', $(this).attr("ref"));
	        }).bind("drag", function (e) {
	            var ref = $(e.srcElement).attr("ref");
	            _dragData = ref;
	        });
	        $(".middlebar>.linkbtn").click(function () {
	            $('#TextBox1').tinymce().execCommand('mceInsertContent', false, $(this).attr("rel")); return false;
	        }).bind("drag", function (e) {
	            var ref = $(e.srcElement).attr("rel");
	            _dragData = ref;
	        });
	        $(".li-table").click(function () {
	            var titleRef = [];
	            var fieldRef = [];
	            var subTbl = $(this).attr("ref")
	            titleRef.push("<p>&nbsp;</p>");
	            titleRef.push("<table id=\"" + subTbl + "\" class=\"subtbl\" border=\"1\">");
	            titleRef.push("<thead><tr>");
	            fieldRef.push("<tbody><tr>");
	            $(this).siblings(".fieldItem").each(function () {
	                var arr = $(this).attr("ref").split("|");
	                titleRef.push("<td>&nbsp;", arr[1], "</td>");
	                fieldRef.push("<td>{", arr[0], "}</td>");
	            });

	            titleRef.push("</tr></thead>");
	            fieldRef.push("</tr></tbody>");
	            fieldRef.push("</table>");
	            var htmlRef = titleRef.concat(fieldRef);

	            $('#TextBox1').tinymce().execCommand('mceInsertContent', false, htmlRef.join("")); return false;
	        }).bind("drag", function (e) {
	            var titleRef = [];
	            var fieldRef = [];
	            var subTbl = $(this).attr("ref")
	            titleRef.push("<p>&nbsp;</p>");
	            titleRef.push("<table id=\"" + subTbl + "\" class=\"subtbl\" border=\"1\">");
	            titleRef.push("<thead><tr>");
	            fieldRef.push("<tbody><tr>");
	            $(this).siblings(".fieldItem").each(function () {
	                var arr = $(this).attr("ref").split("|");
	                titleRef.push("<td>&nbsp;", arr[1], "</td>");
	                fieldRef.push("<td>{", arr[0], "}</td>");
	            });

	            titleRef.push("</tr></thead>");
	            fieldRef.push("</tr></tbody>");
	            fieldRef.push("</table>");
	            var htmlRef = titleRef.concat(fieldRef);

	            _dragData = htmlRef.join("");
	        });
	        $("#btnField").click(function () {
	            $("#fieldtd").toggle();
	        });
	        $("#LinkButton1x").click(function () {
	            var c = $('#TextBox1').html();
	            $("#TextBox1").val(c);
	        });

	        $('textarea.tinymce').tinymce({
	            language: 'cn',
	            script_url: '../tiny_mce/tiny_mce.js',
	            theme: "advanced",
	            plugins: "autolink,lists,pagebreak,style,table,save,advhr,advimage,advlink,inlinepopups,preview,searchreplace,contextmenu,paste,directionality,fullscreen,noneditable,nonbreaking,xhtmlxtras,template,advlist",
	            visual_table_class: "printTbl",
	            // Theme options
	            theme_advanced_buttons1: "code,undo,redo,|,cut,copy,paste,pastetext,pasteword,|,forecolor,backcolor,|,bold,italic,underline,strikethrough,|,justifyleft,justifycenter,justifyright,styleselect,formatselect,fontselect,fontsizeselect",
	            theme_advanced_buttons2: "tablecontrols,|search,replace,|,bullist,numlist,|,link,unlink,image,|,removeformat,visualaid,|,sub,sup,|,charmap,|,fullscreen,attribs,template",
	            theme_advanced_toolbar_location: "top",
	            theme_advanced_toolbar_align: "left",
	            theme_advanced_statusbar_location: "bottom",
	            theme_advanced_resizing: true,
	            theme_advanced_path: false,
	            theme_advanced_fonts: "宋体,黑体," +
                    "Arial=arial,helvetica,sans-serif;" +
                    "Arial Black=arial black,avant garde;" +
                    "Courier New=courier new,courier;",
	            // Example content CSS (should be your site CSS)
	            content_css: "../../css/editorPrint.css",

	            extended_valid_elements: "pre[name|class],style",
	            valid_children: "+body[style],+body[link]",
	            width: "900px",
	            height: "540px",
	            // Drop lists for link/image/media/template dialogs
	            template_external_list_url: "tmpl/template_list.js",
	            external_link_list_url: "lists/link_list.js",
	            external_image_list_url: "lists/image_list.js",
	            media_external_list_url: "lists/media_list.js",

	            // Replace values for the template plugin
	            template_replace_values: {
	                username: "Some User",
	                staffid: "991234"
	            },
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
        业务名称：<%=tblName %>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;切换界面：
            <asp:DropDownList ID="DropDownList1" runat="server">
            </asp:DropDownList>
        &nbsp;&nbsp;界面名称：<asp:TextBox ID="txtStyleName" CssClass="textbox"  runat="server" width="100px"></asp:TextBox>
        &nbsp;
            <a href="DefTableUI2.aspx?tblName=<%=tblName %>&styleindex=<%=styleIndex%>&t=1" target="_self">[编辑界面]</a> 
            <a href="DefTableUI3.aspx?tblName=<%=tblName %>&styleindex=<%=styleIndex%>&t=2" target="_self">[手机界面]</a> 
            <a class="curstyle" href="DefTableUI4.aspx?tblName=<%=tblName %>&styleindex=<%=styleIndex%>&t=3" target="_self">[打印界面]</a> 
            <a href="DefTableUI2.aspx?tblName=<%=tblName %>&styleindex=<%=styleIndex%>&t=4" target="_self">[查看界面]</a> 

        </span>
        <span style="float:right;margin-right:10px">
            <a href="DefTableStyleList.aspx?tblName=<%=tblName %>" target="_self">返回列表</a>
            <a href="DefTableUI.aspx?tblName=<%=tblName %>&styleindex=<%=styleIndex%>&t=<%=t%>" target="_self">HTML界面</a> 
            <a href="javascript:" id="btnField">字段参考</a> 
            <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click" >保存</asp:LinkButton>
            <a href="javascript:" onclick="window.close();">关闭</a> 
            
        
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
            <div class="middlebar">
                <a class="linkbtn" rel="{Advice:N}" title="{Advice:N:参数}，参数可选，其值 all 、last，all代表显示所有意见，last代表显示最近一次的意见" href="javascript:" >处理意见</a> 
                    <em class="split">|</em>
                <a class="linkbtn" rel="{Employee:N}" title="处理人姓名, {Employee:N}" href="javascript:" >处理人姓名</a> 
                    <em class="split">|</em>
                <a class="linkbtn" rel="{Signature:N}" title="处理人签名，{Signature:N}" href="javascript:" >处理人签名</a> 
                    <em class="split">|</em>
                <a class="linkbtn" rel="{Time:N:日期格式}" title="{Time:N:日期格式}，日期格式可选，默认为yyyy-MM-dd HH:mm" href="javascript:" >处理时间</a> 
                    <em class="split">|</em>
                <a class="linkbtn" rel="{Sign:N}" title="{Sign:N}，会签节点" href="javascript:" >会签节点</a> 
                    <em class="split">|</em>
                <a class="linkbtn" rel="{Position:N}" title="{Position:N}，处理人岗位" href="javascript:" >处理人岗位</a> 
                    <em class="split">|</em>
                <a class="linkbtn" rel="{DeptName:N}" title="{DeptName:N}，处理人所属部门" href="javascript:" >处理人所属部门</a> 
                    <em class="split">|</em>
                <a class="linkbtn" rel="{CompanyName:N}" title="{CompanyName:N}，处理人所属公司" href="javascript:" >处理人所属公司</a>
                    <em class="split">|</em>
                <a class="linkbtn" rel="{AdviceList:}" title="{AdviceList:N}，意见列表" href="javascript:" >意见列表</a>
                    <em class="split">|</em>
                <span><em style="color:Red;font-weight:bold;font-style:normal;">N</em> 为流程步骤编号</span> 
            </div>
        </td>
     </tr>
  </table>
  </div>



    </form>
</body>
</html>