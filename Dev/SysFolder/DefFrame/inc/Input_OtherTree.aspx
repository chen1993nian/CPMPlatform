<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Input_OtherTree.aspx.cs" Inherits="EIS.Studio.SysFolder.DefFrame.inc.Input_OtherTree" %>

<!DOCTYPE html>

<HTML>
	<HEAD>
		<title>弹出其它树形界面</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="../../../../style.css" rel="stylesheet">
		<script language="javascript">
<!--
    function selectionMade(SouSelect, TarSelect) {
        with (SouSelect) {
            var cboField;
            var cbovalue;
            for (var loop_index = 0; loop_index < length; loop_index++) {
                if (options[loop_index].selected) {
                    cboField = options[loop_index].text;
                    cbovalue = options[loop_index].value;
                    SendValue(TarSelect, cboField, cbovalue);
                    SouSelect.remove(loop_index);
                }
            }
        }
    }

    function SendValue(TarObj, FieldTxt, Num) {
        var oOption = document.createElement("OPTION");
        oOption.text = FieldTxt;
        oOption.value = Num;
        TarObj.add(oOption);
    }
    function selRefTbl() {
        str = "<xml id=xmlSelField src='GetRefField.asp?tbl=" + document.all.refTbl.value + "'></xml>";
        document.all.divXml.innerHTML = str;
        document.all.selectField.innerHTML = "";
    }

    //-->
		</script>
		<script id="clientEventHandlersJS" language="javascript">
<!--

    function Confirm() {
        var sql = "";
        var control_col = "";

        sql = window.frm.strsql.value;

        for (var i = 0 ; i < window.frm.LB_FullField.length ; i++) {
            if (i != (window.frm.LB_FullField.length - 1)) {
                control_col += window.frm.LB_FullField.options(i).value;
                control_col += ",";
            }
            else {
                control_col += window.frm.LB_FullField.options(i).value;
            }
        }
        window.returnValue = sql + "|" + control_col;
        window.close();
    }

    function Cancel() {
        window.returnValue = "";
        window.close();
    }

    //-->
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout" language="javascript" >
		<form id="frm" method="post" runat="server">
			<TABLE id="Table2" cellSpacing="1" cellPadding="1" width="472" border="1" style="WIDTH: 472px; HEIGHT: 303px">
				<TR>
					<TD colSpan="3">
						<TABLE id="Table5" cellSpacing="1" cellPadding="1" style="WIDTH: 472px; HEIGHT: 67px">
							<TR>
								<TD colSpan="2"><STRONG>文件路径：</STRONG> <INPUT id="strsql" style="WIDTH: 376px; HEIGHT: 21px" type="text" size="57"></TD>
							</TR>
						</TABLE>
						<STRONG>请选择要回写的字段</STRONG></TD>
				</TR>
				<TR>
					<TD rowspan="2">
						<asp:ListBox id="LB_SelectField" runat="server" Width="224px" Height="216px" ondblclick="selectionMade(window.frm.LB_SelectField,window.frm.LB_FullField)"></asp:ListBox></TD>
					<TD><IMG class="btn" onclick="selectionMade(window.frm.LB_SelectField,window.frm.LB_FullField)"
							src="../../../../images/btn/btn_arrowhead1.gif" style="CURSOR: hand"></TD>
					<TD rowspan="2">
						<asp:ListBox id="LB_FullField" runat="server" Width="212px" Height="217px" ondblclick="selectionMade(window.frm.LB_FullField,window.frm.LB_SelectField)"></asp:ListBox></FONT></TD>
				</TR>
				<TR>
					<TD><IMG class="btn" onclick="selectionMade(window.frm.LB_FullField,window.frm.LB_SelectField)"
							src="../../../../images/btn/btn_arrowhead.gif" style="CURSOR: hand"></TD>
				</TR>
			</TABLE>
			<TABLE id="Table1" align="right" style="Z-INDEX: 101; LEFT: 328px; POSITION: absolute; TOP: 328px">
				<TR>
					<TD><IMG class="btn" onclick="Confirm()" src="../../../../images/btn/btn_affirm.gif">
						<IMG class="btN" onclick="Cancel()" src="../../../../images/btn/btn_cancel.gif">
					</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
