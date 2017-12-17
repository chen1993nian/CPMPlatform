<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DefTableScript.aspx.cs" Inherits="EIS.Studio.SysFolder.DefFrame.DefTableScript" %>


<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>脚本编辑</title>
    <meta http-equiv="X-UA-Compatible" content="IE=Edge" />    
    <meta http-equiv="Pragma" content="no-cache" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link rel="stylesheet" type="text/css" href="../../css/DefStyle.css" />
    <link type="text/css" href="../../Css/jquery-ui/lightness/jquery-ui-1.7.2.custom.css" rel="stylesheet" />
    <script type="text/javascript" src="../../js/jquery-1.4.2.min.js"></script>
    <script type="text/javascript" src="../../js/jquery.notice.js"></script>
    <script type="text/javascript" src="../../js/lhgdialog.min.js"></script>

    <script type="text/javascript" src="../../Js/jquery.cookie.js"></script>
    <script type="text/javascript" src="../../js/ui.core.js"></script>
    <script type="text/javascript" src="../../js/ui.tabs.js"></script>
    <link href="../CodeMirror/lib/codemirror.css" rel="stylesheet" type="text/css" />
    <link href="../CodeMirror/addon/hint/show-hint.css" rel="stylesheet" type="text/css" />
    <link href="../CodeMirror/addon/fold/foldgutter.css" rel="stylesheet" type="text/css" />

    <script src="../CodeMirror/lib/codemirror.js" type="text/javascript"></script>
    
    <script src="../CodeMirror/addon/fold/foldcode.js"></script>
    <script src="../CodeMirror/addon/fold/foldgutter.js"></script>
    <script src="../CodeMirror/addon/fold/brace-fold.js"></script>
    <script src="../CodeMirror/addon/fold/xml-fold.js"></script>

    <script src="../CodeMirror/addon/selection/active-line.js"></script>

    <script src="../CodeMirror/addon/hint/show-hint.js"></script>
    <script src="../CodeMirror/addon/hint/javascript-hint.js"></script>

    <script src="../CodeMirror/mode/javascript/javascript.js" type="text/javascript"></script>
    <style type="text/css">
        html,body {
	        height:100%;
        }
        body {
	        margin: 0px;
	        padding: 0px;
            overflow: hidden;
        }
        .topnav a{background-color:transparent;line-height:20px;}
        .textbox {
            width: 95%;
        }
        .codetd {
            height:100%;
            padding:0px;
            border: solid 1px #bbb;
        }
        #tabs{	
	        height:auto!important; /*for ie6 bug and ie7+,moz,webkit 正确*/
	        height:100%; /*修复IE6,all browser*/
	        min-height:100%; /*for ie6 bug and ie7+,moz,webkit 正确*/
	        margin:auto;
	        padding:0px;
        }
        a:hover{text-decoration:none;color:Red;}
    </style>
    <script type="text/javascript">
        var editor1 = null, editor2 = null, editor3 = null, $tabs = null;
        $(function () {
            var _curClass = EIS.Studio.SysFolder.DefFrame.DefTableScript;

            $("#LinkButton1").click(function () {
                SaveData(true);
                return false;
            });
            $("#ddlTblName").change(function () {
                var tbl = $(this).val();
                window.open("DefTableScript.aspx?tblName=" + tbl, "_self");
                return false;
            });
            function SaveData(showMsg) {
                var doc2 = editor2 ? editor2.doc.getValue() : $("#TextBox2").val();
                var doc3 = editor3 ? editor3.doc.getValue() : $("#TextBox3").val();
                var ret = _curClass.SaveScript("<%=tblName %>", editor1.doc.getValue(), doc2, doc3);
                if (ret.error) {
                    alert("保存出错：" + ret.error.Message);
                }
                else {
                    if (showMsg)
                        $.noticeAdd({ text: '保存成功！', stay: false, stayTime: 200 });
                }
            }

            //界面预览
            $("#btnPreview").click(function () {
                Preview("");
            });

            function Preview(si) {
                SaveData(false);
                var selected = $tabs.tabs('option', 'selected');
                var url = "";
                if (selected == 1)
                    url = '../AppFrame/AppDefault.aspx?tblName=<%=tblName %>&sindex=' + si;
                if (selected == 0)
                    url = '../AppFrame/AppInput.aspx?tblName=<%=tblName %>&sindex=' + si;
                var dlg = new $.dialog({
                    title: '界面预览', page: url
                    , btnBar: true, cover: true, lockScroll: true, width: 1000, height: 600, bgcolor: 'gray', cancelBtnTxt: '关闭',
                    dgOnLoad: function () {
                        this.addBtn("btnFresh", "刷新", function () { window.parent.frames["lhgfrm_lhgdgId"].location.reload(); }, "left");
                    }

                });
                dlg.ShowDialog();
            }

            var h = $(document).height() - 100;

            editor1 = CodeMirror.fromTextArea(document.getElementById("TextBox1"), {
                mode: { name: "javascript", globalVars: true },
                tabMode: "shift",
                lineNumbers: true,
                styleActiveLine: true,
                autoMatchParens: true,
                foldGutter: true,
                gutters: ["CodeMirror-linenumbers", "CodeMirror-foldgutter"],
                extraKeys: {
                    "Ctrl-S": function (cm) { SaveData(true); }
                    , "F12": function (cm) { Preview(""); }
                    , "Ctrl-1": function (cm) { Preview("1"); return false; }
                    , "Ctrl-2": function (cm) { Preview("2"); }
                    , "Ctrl-3": function (cm) { Preview("3"); }
                    , "Ctrl-4": function (cm) { Preview("4"); }
                    , "Ctrl-5": function (cm) { Preview("5"); }
                    , "Ctrl-6": function (cm) { Preview("6"); }
                    , "Ctrl-J": function (cm) { editor1.showHint(); }
                }
            });
            editor1.setSize("100%", h - 60);
            /*
            editor1.on('change', function () {
                editor1.showHint();  //满足自动触发自动联想功能
            });
            */
            $("#tabs").height(h);
            $tabs = $("#tabs").tabs({
                cookie: true, show: function (i, o) {
                    if (editor2 == null && o.index == 1) {
                        editor2 = CodeMirror.fromTextArea(document.getElementById("TextBox2"), {
                            mode: "javascript",
                            tabMode: "shift",
                            lineNumbers: true,
                            styleActiveLine: true,
                            autoMatchParens: true,
                            foldGutter: true,
                            gutters: ["CodeMirror-linenumbers", "CodeMirror-foldgutter"],
                            extraKeys: {
                                "Ctrl-S": function (cm) { SaveData(true); }
                                , "F12": function (cm) { Preview(""); }
                                , "Ctrl-1": function (cm) { Preview("1"); return false; }
                                , "Ctrl-2": function (cm) { Preview("2"); }
                                , "Ctrl-3": function (cm) { Preview("3"); }
                                , "Ctrl-4": function (cm) { Preview("4"); }
                                , "Ctrl-5": function (cm) { Preview("5"); }
                                , "Ctrl-6": function (cm) { Preview("6"); }
                                , "Ctrl-J": function (cm) { editor2.showHint(); }
                            }
                        });
                        editor2.setSize("100%", h - 60);
                    }
                    if (editor3 == null && o.index == 2) {
                        editor3 = CodeMirror.fromTextArea(document.getElementById("TextBox3"), {
                            mode: "javascript",
                            tabMode: "shift",
                            lineNumbers: true,
                            styleActiveLine: true,
                            autoMatchParens: true,
                            foldGutter: true,
                            gutters: ["CodeMirror-linenumbers", "CodeMirror-foldgutter"]
                        });
                        editor3.setSize("100%", h - 60);
                    }
                }
            });
        });

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="menubar">
        <div class="topnav">
            <span style="float: left; margin-left: 10px">业务名称：
            <asp:DropDownList ID="ddlTblName" runat="server" >
            </asp:DropDownList>
            &nbsp;&nbsp; </span>
            <span style="float: right; margin-right: 10px">
                <a href="javascript:" onclick="window.location.reload();">【重新加载】</a>
                <a title="保存预览(F12)，其它样式：Ctrl-1、Ctrl-2 …" href="javascript:" id="btnPreview">【保存预览】</a>
                <asp:LinkButton ID="LinkButton1" ToolTip="保存(Ctrl-S)" runat="server" OnClick="LinkButton1_Click">【保存】</asp:LinkButton>
                &nbsp;&nbsp;&nbsp;&nbsp; 
            </span>
        </div>
    </div>
    <div id="tabs">
        <ul>
            <li><a href="#tabs-1">编辑界面脚本</a></li>
            <li><a href="#tabs-2">列表脚本块</a></li>
            <li><a href="#tabs-3">列表数据预处理</a></li>
        </ul>
        <div id="tabs-1">
            <div class="codetd">
                <asp:TextBox ID="TextBox1" runat="server" TextMode="MultiLine" Width="98%"></asp:TextBox>
            </div>
        </div>
        <div id="tabs-2">
            <div class="codetd">
                <asp:TextBox ID="TextBox2" runat="server" TextMode="MultiLine" Width="98%"></asp:TextBox>
            </div>
        </div>
        <div id="tabs-3">
            <div class="codetd">
                <asp:TextBox ID="TextBox3" runat="server" TextMode="MultiLine" Width="98%"></asp:TextBox>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
