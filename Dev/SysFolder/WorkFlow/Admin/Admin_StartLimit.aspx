<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Admin_StartLimit.aspx.cs" Inherits="EIS.WebBase.SysFolder.WorkFlow.Admin.Admin_StartLimit" %>

<!DOCTYPE html>


<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>发起权限设置</title>
    <link rel="stylesheet" type="text/css" href="../../../css/password.css" />
    <script type="text/javascript" src="../../../js/jquery-1.7.min.js"></script>
    <script type="text/javascript" src="../../../js/jquery.notice.js"></script>
    <script type="text/javascript" src="../../../js/tools.js"></script>
    <style type="text/css">
        .titleTd{font-size:12px;color:#FF8C00;text-align:left;font-weight:bold;}
        input[type=radio]{cursor:pointer;}
        label{cursor:pointer;}
    </style>

    <script type="text/javascript">
        function success() {
            $.noticeAdd({
                text: '保存成功！', stay: false, onClose: function () {
                    if ('<%=Request["open"] %>' != "")
                        window.close();
                    else
                        frameElement.lhgDG.cancel();
                }
            });
        }
        jQuery(function () {

            $("#btnClose").click(function () {
                if ('<%=Request["open"] %>' != "")
                    window.close();
                else
                    frameElement.lhgDG.cancel();
            });
            $("#Button1").click(function () {
                var sb = "";
                var st = "";

                if ($("#RadioButtonList1_1").attr("checked")) {
                    st = "指定组织级别：";
                    if ($("#RadioButtonList2_0").attr("checked")) {
                        sb = "'[!CompanyTypeCode!]'='GS'";
                        st += "公司";
                    }
                    else if ($("#RadioButtonList2_1").attr("checked")) {
                        sb = "'[!CompanyTypeCode!]'='FGS' or '[!OrgType!]'='ZGS'";
                        st += "分公司和子公司";
                    }
                    else if ($("#RadioButtonList2_2").attr("checked")) {
                        sb = "'[!OrgType!]'='XMB'";
                        st += "项目部";
                    }

                }
                else if ($("#RadioButtonList1_2").attr("checked")) {
                    var arrCode = $("#HiddenField1").val().split(",");
                    var arrSb = [];
                    for (var i = 0; i < arrCode.length; i++) {
                        if (arrCode[i].length > 0) {
                            arrSb.push("'[!DeptWbs!]' like '" + arrCode[i] + "%'");
                        }
                    }
                    sb = arrSb.join(" or ");

                    st = "指定组织范围：" + $("#TextBox1").val();

                }
                else if ($("#RadioButtonList1_3").attr("checked")) {
                    var arrCode = $("#HiddenField2").val();
                    if (arrCode.length > 0)
                        sb = "dbo.Org_IsEmployeeInRole('[!EmployeeId!]','" + arrCode + "')>0";

                    st = "指定角色：" + $("#TextBox2").val();
                }
                else if ($("#RadioButtonList1_4").attr("checked")) {
                    var arrCode = $("#HiddenField3").val();
                    if (arrCode.length > 0)
                        sb = "dbo.Org_IsEmployeeInPosition('[!EmployeeId!]','" + arrCode + "')>0";

                    st = "指定岗位：" + $("#TextBox3").val();
                }
                else {
                    st = "所有人都可以发起";
                }
                var arrId = "<%=Request["cid"] %>".split(",");
                window.opener.document.getElementById(arrId[0]).value = sb;
                window.opener.document.getElementById(arrId[1]).value = st;

                if ('<%=Request["open"] %>' != "")
                    window.close();
                else
                    frameElement.lhgDG.cancel();
                return false;
            });
            $("#TextBox1").attr("emptytip", "<请双击选择部门>").attr("readOnly", true).dblclick(function () {
                openpage('../../Common/DeptTree.aspx?method=1&queryfield=deptwbs,deptname&cid=HiddenField1,TextBox1');
            });
            $("#TextBox2").attr("emptytip", "<请双击选择角色>").attr("readOnly", true).dblclick(function () {
                openpage('../../Common/RoleTree.aspx?method=1&queryfield=roleid,rolename&cid=HiddenField2,TextBox2');
            });
            $("#TextBox3").attr("emptytip", "<请双击选择岗位>").attr("readOnly", true).dblclick(function () {
                openpage('../../Common/PositionTree.aspx?method=1&queryfield=positionid,positionname&cid=HiddenField3,TextBox3');
            });

            /*
            $("#TextBox00").attr("emptytip", "<请双击选择员工>").attr("readOnly", true).dblclick(function () {
                openCenter('../../Common/UserTree.aspx?method=1&queryfield=empid,empname&cid=HiddenField3,TextBox3', "_blank", 640, 500);
            });*/

            jQuery("textarea.emptytip").emptyValue();

            $("input[name=RadioButtonList1]").click(function () {
                $("input[name=RadioButtonList2]").attr("disabled", this.value != "1");
                $("#TextBox1").attr("disabled", this.value != "2");
                $("#TextBox2").attr("disabled", this.value != "3");
                $("#TextBox3").attr("disabled", this.value != "4");
            });

        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="maindiv">
        <table width="600">
            <tbody>
                <tr>
                    <td width="80" class="titleTd">发起权限类型</td>
                    <td align="left" valign="middle" height="30">
                        <asp:RadioButtonList ID="RadioButtonList1" RepeatDirection="Horizontal" runat="server">
                            <asp:ListItem Text=" 所有人都可以发起" Value="0" Selected="True"/>
                            <asp:ListItem Text=" 指定组织级别" Value="1"/>
                            <asp:ListItem Text=" 指定组织范围" Value="2"/>
                            <asp:ListItem Text=" 指定角色" Value="3"/>
                            <asp:ListItem Text=" 指定岗位" Value="4"/>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td class="titleTd">
                        指定组织级别
                    </td>
                    <td align="left" valign="middle" height="30">
                        <asp:RadioButtonList ID="RadioButtonList2" Enabled="false" RepeatDirection="Horizontal" runat="server">
                            <asp:ListItem Text=" 公司" Value="0" Selected="True"/>
                            <asp:ListItem Text=" 分公司和子公司" Value="1"/>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td class="titleTd">
                        指定组织范围
                    </td>
                    <td>
                        <asp:TextBox ID="TextBox1" CssClass="TextBoxInArea emptytip" Enabled="false" TextMode="MultiLine" Rows="4" runat="server"></asp:TextBox>
                        <asp:HiddenField ID="HiddenField1" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td class="titleTd">
                         指定角色
                    </td>
                    <td>
                        <asp:TextBox ID="TextBox2" CssClass="TextBoxInArea emptytip" Enabled="false" TextMode="MultiLine" Rows="4" runat="server"></asp:TextBox>
                        <asp:HiddenField ID="HiddenField2" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td class="titleTd">
                         指定岗位
                    </td>
                    <td>
                        <asp:TextBox ID="TextBox3" CssClass="TextBoxInArea emptytip" Enabled="false" TextMode="MultiLine" Rows="4" runat="server"></asp:TextBox>
                        <asp:HiddenField ID="HiddenField3" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td height="60" align="left">
                        <asp:Button ID="Button1" CssClass="btn01" runat="server" Text=" 保 存 " onclick="Button1_Click" />
                        &nbsp;
                        &nbsp;
                        <input id="btnClose" type="button"  class="btn01" value=" 关 闭 "/>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
    </form>
</body>
</html>