<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PositionMoveEdit.aspx.cs" Inherits=" EIS.Studio.SysFolder.Permission.PositionMoveEdit" %>
 
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <title>主岗信息调整</title>
    <meta http-equiv="Pragma" content="no-cache" />
    <link type="text/css" rel="stylesheet" href="../../Css/appInput.css" />
    <script type="text/javascript" src="../../js/jquery-1.7.min.js"></script>
    <script type="text/javascript" src="../../DatePicker/WdatePicker.js"></script>
    <script type="text/javascript" src="../../js/jquery.notice.js"></script>

    <style type="text/css">
        td{padding:5px;border-color:gray;}
        body{background:white;}
        input[type=text]{width:220px;}
        input[type=button]{padding:3px;}
    </style>

    <script type="text/javascript">
        $(document).ready(function () {
            $(".Read").attr("readonly", true);

            jQuery("#txtPositionName2").click(function () {
                _openPage('../Common/PositionTree.aspx?method=1&queryfield=positionid,positionname,deptid,deptname&cid=HiddenField1,txtPositionName2,HiddenField2,txtDeptName2'
                );
            }).emptyValue({
                empty: "<请点击选择岗位>",
                className: "gray"
            });

        });
        function notFind() {
            alert("找不到对应的公司");
            window.location = "../../welcome.htm";
        }

        //弹出窗口
        function _openCenter(url, name, width, height) {
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

        //系统弹出
        function _openPage(url) {
            _openCenter(url, "_blank", 400, 500);
        }
    </script>
</head>
<body>
    <form runat="server" id="form1">
    <!-- 工具栏 -->
    <div class="menubar">
        <div class="topnav">
                    <span style="float:right;">
                    <asp:LinkButton ID="LinkButton1" runat="server" onclick="LinkButton1_Click">保存</asp:LinkButton>&nbsp;&nbsp;
                    <a href="javascript:" onclick="window.close();" >关闭</a> &nbsp;&nbsp;
                    </span>
        </div>
    </div>
    
    <div  id="maindiv">
        <%=TipMessage %>
    <table class='normaltbl'  border="1"  style="width:400px;" align="center">
    <caption>主岗信息调整</caption>
    <tbody>
      <tr>
        <td  width="100">&nbsp;员工姓名</td>
        <td  >
            <asp:TextBox ID="txtEmpName" CssClass="TextBoxInChar" runat="server"></asp:TextBox>
        </td>
    </tr>

      <tr>
        <td>&nbsp;原岗位名称</td>
        <td >
            <asp:TextBox ID="txtPositionName" CssClass="TextBoxInChar" runat="server"></asp:TextBox>

        </td>
      </tr>
     <tr>
        <td>
            &nbsp;原部门名称</td>
        <td>
            <asp:TextBox ID="txtDeptName" CssClass="TextBoxInChar" runat="server"></asp:TextBox>
        </td>
     </tr>

           <tr>
        <td>&nbsp;<span class="green">新岗位名称</span></td>
        <td >
            <asp:TextBox ID="txtPositionName2" CssClass="TextBoxInChar Read" runat="server"></asp:TextBox>
            <asp:HiddenField ID="HiddenField1" runat="server" />
        </td>
      </tr>
     <tr>
        <td>
            &nbsp;<span class="green">新部门名称</span></td>
        <td>
            <asp:TextBox ID="txtDeptName2" CssClass="TextBoxInChar Read" runat="server"></asp:TextBox>
            <asp:HiddenField ID="HiddenField2" runat="server" />
        </td>
     </tr>
          <tr>
        <td>
            &nbsp;<span class="green">排序</span></td>
        <td>
            <asp:TextBox ID="txtOrder" CssClass="TextBoxInChar" runat="server"></asp:TextBox>
        </td>
     </tr>
    </tbody>
    </table>
    </div>
    </form>
</body>
</html>

