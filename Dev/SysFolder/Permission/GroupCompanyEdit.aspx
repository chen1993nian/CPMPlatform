<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GroupCompanyEdit.aspx.cs" Inherits="EIS.Studio.SysFolder.Permission.GroupCompanyEdit" %>

 
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <title>集团信息维护</title>
    <meta http-equiv="Pragma" content="no-cache" />
    <link type="text/css" rel="stylesheet" href="../../Css/appInput.css" />
    <script type="text/javascript" src="../../js/jquery-1.4.2.min.js"></script>
    <script type="text/javascript" src="../../DatePicker/WdatePicker.js"></script>
    <script type="text/javascript" src="../../js/jquery.validate.min.js"></script>
    <script type="text/javascript" src="../../js/jquery.notice.js"></script>

    <style type="text/css">
        table{border-color:gray;width:500px;}
        td{padding:5px;border-color:gray;}
        caption{font-size:14px;font-weight:bold;height:30px;border-width:0px;}
        body{background:white;}
        input[type=text]{width:380px;}
    </style>

    <script type="text/javascript">
        $(document).ready(function () {
            $(".Read").attr("readonly", true);
            var validator = $("#form1").validate({
                rules: {
                    txtDeptName: "required",
                    txtDeptAbbr: "required",
                    txtOrder: "required"
                }
            });
            $("#LinkButton1,#LinkButton2").click(function () {
                return $("#form1").valid();
            });
            $("#btnPath").click(function () {

                openpage("LdapTree.aspx?cid=txtPath");
            });
            $("#btnParent").click(function () {
                openpage('../Common/DeptTree2.aspx?method=1&queryfield=deptwbs,deptname&cid=txtParentId,txtParentName');
            });
        });
        function returnList() {
            window.open("GroupCompanyList.aspx?DeptPWBS=<%=pwbs %>", "_self");
        }

        function openCenter(url, name, width, height) {
            var str = "height=" + height + ",innerHeight=" + height + ",width=" + width + ",innerWidth=" + width;
            if (window.screen) {
                var ah = screen.availHeight - 30;
                var aw = screen.availWidth - 10;
                var xc = (aw - width) / 2;
                var yc = (ah - height) / 2;
                str += ",left=" + xc + ",screenX=" + xc + ",top=" + yc + ",screenY=" + yc;
                str += ",resizable=yes,scrollbars=yes,directories=no,status=no,toolbar=no,menubar=no,location=no";
            }
            return window.open(url, name, str);
        }

        function openpage(url) {
            openCenter(url, "_blank", 400, 500);
        }
    </script>
</head>
<body>
    <form runat="server" id="form1">
 
    <!-- 工具栏 -->
    <div class="menubar">
        <div class="topnav">
            <ul>
                <li>
                    <asp:LinkButton ID="LinkButton2" runat="server" onclick="LinkButton2_Click">保存后新增</asp:LinkButton>
                </li>
                <li>
                    <asp:LinkButton ID="LinkButton1" runat="server" onclick="LinkButton1_Click">保存</asp:LinkButton>
                </li>
                <li>
                    <a href="javascript:" onclick="returnList()" >返回列表</a> 
                </li>
            </ul>
        </div>
    </div>
    
    <div >
    <%=TipMessage %>
    <table  class="normaltbl" style="width:660px;margin-top:10px;"  border="1"   align="center">
    <caption>单位信息</caption>
    <tbody>
      <tr>
        <td  width="120">&nbsp;单位名称</td>
        <td  >
            <asp:TextBox ID="txtDeptName" CssClass="TextBoxInChar" runat="server"></asp:TextBox>
            &nbsp;<span class='RequiredStar'>*</span>
        </td>
    </tr>
    <tr>
        <td >&nbsp;单位编码</td>
        <td >
            <asp:TextBox ID="txtDeptCode" CssClass="TextBoxInChar" runat="server"></asp:TextBox>
         </td>
      </tr>
      <tr>
        <td>&nbsp;单位简称</td>
        <td>
            <asp:TextBox ID="txtDeptAbbr" CssClass="TextBoxInChar" runat="server"></asp:TextBox>
            &nbsp;<span class='RequiredStar'>*</span>
        </td>
        </tr>
              <tr>
        <td>&nbsp;单位类型</td>
        <td>
            <asp:DropDownList ID="DropDownList1" runat="server">
            </asp:DropDownList>
            &nbsp;<span class='RequiredStar'>*</span>
        </td>
        </tr>
              <tr>
        <td>&nbsp;同级排序</td>
        <td>
            <asp:TextBox ID="txtOrder" CssClass="TextBoxInChar" runat="server"></asp:TextBox>
            &nbsp;<span class='RequiredStar'>*</span>
        </td>
        </tr>
        <tr>
        <td>&nbsp;单位管理员账号</td>
        <td>
            <asp:TextBox ID="txtDeptSa" CssClass="TextBoxInChar" runat="server"></asp:TextBox>
        </td>
      </tr>
      <tr>
        <td>&nbsp;上级单位</td>
        <td>
            <asp:TextBox ID="txtParentName" CssClass="TextBoxInChar Read" runat="server"></asp:TextBox>
            <asp:HiddenField ID="txtParentId" runat="server" />
            <input id="btnParent" type="button" value="选择"/>
        </td>
        </tr> 
    <tr style="display:none;">
        <td>&nbsp;目录服务路径</td>
        <td>
            <asp:TextBox ID="txtPath" CssClass="TextBoxInChar" runat="server"></asp:TextBox>
            <input id="btnPath" type="button" value="选择"/>
        </td>
      </tr> 
      <tr>
        <td>&nbsp;单位描述</td>
        <td>
        <asp:TextBox ID="txtDeptNote" CssClass="TextBoxInArea" runat="server" Rows="6" TextMode="MultiLine"></asp:TextBox>
        </td>
      </tr>



    </tbody>
    </table>
    </div>
    </form>
</body>
</html>

