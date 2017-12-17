<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PersonalInfo.aspx.cs" Inherits="EIS.Web.WorkAsp.Personal.PersonalInfo" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <title>员工信息</title>
    <meta http-equiv="Pragma" content="no-cache" />
    <link type="text/css" rel="stylesheet" href="../../Css/appInput.css" />
    <script type="text/javascript" src="../../js/jquery-1.4.2.min.js"></script>
    <script type="text/javascript" src="../../DatePicker/WdatePicker.js"></script>
    <script type="text/javascript" src="../../js/jquery.validate.min.js"></script>
    <script type="text/javascript" src="../../js/jquery.notice.js"></script>

    <style type="text/css">
        table{border-color:gray;width:600px;}
        td{padding:5px;border-color:gray;}
        body{background:white;overflow:auto;}
        input[type=text]{width:220px;}
        #LinkButton1{text-decoration:none;display:block;margin-right:30px;padding-left:48px;background:url(../../img/common/ico5.gif) no-repeat center center;}
        #LinkButton1:hover{text-decoration:none;display:block;color:red;}
        input[type=checkbox]{vertical-align:middle;}
    </style>

    <script type="text/javascript">
        $(document).ready(function () {
            $(".Read").attr("readonly", true);
            $(".Wdate").focus(function () {
                WdatePicker({ isShowClear: false, dateFmt: 'yyyy-MM-dd' });
            });
            var validator = $("#form1").validate({
                rules: {
                    txtEmpName: "required",
                    txtOrder: "required",
                    txtLoginName: "required"
                }
            });
            $("#LinkButton1,#LinkButton2").click(function () {
                return $("#form1").valid();
            });

        });
    </script>
</head>
<body>
    <form runat="server" id="form1">
    <!-- 工具栏 -->
    <div class="menubar">
        <div class="topnav">
            <span style="float:right;">
            <asp:LinkButton ID="LinkButton1" runat="server" onclick="LinkButton1_Click">保存</asp:LinkButton>&nbsp;&nbsp;
            </span>
        </div>
    </div>
    
    <div  id="maindiv">
        <%=TipMessage %>
    <table class='normaltbl'  border="1"   align="center">
    <caption>员工基本信息</caption>
    <tbody>
      <tr>
        <td  width="100">&nbsp;员工姓名</td>
        <td>
            <asp:TextBox ID="txtEmpName" ReadOnly="true" CssClass="TextBoxInChar" runat="server"></asp:TextBox>
        </td>
        <td   width="100">&nbsp;员工编号</td>
        <td  >
            <asp:TextBox ID="txtEmpCode" ReadOnly="true" CssClass="TextBoxInChar" runat="server"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td >&nbsp;性别</td>
        <td class="style1" >
            <asp:RadioButtonList ID="selSex" runat="server" 
                RepeatDirection="Horizontal" RepeatLayout="Flow" Width="193px">
                <asp:ListItem>男</asp:ListItem>
                <asp:ListItem>女</asp:ListItem>
            </asp:RadioButtonList>
         </td>
        <td >&nbsp;出生日期
            &nbsp;</td>
        <td >
            <asp:TextBox ID="txtBirthDay" CssClass="Wdate TextBoxInDate" runat="server" 
                MaxLength="10" ToolTip="只能输入如yyyy-mm-dd格式的日期字符串"></asp:TextBox>
        </td>
      </tr>
              <tr>
        <td>&nbsp;电子邮件</td>
        <td >
            <asp:TextBox ID="txtEmail" CssClass="TextBoxInChar" runat="server"></asp:TextBox>
        </td>
        <td>
            &nbsp;移动电话</td>
        <td>
            <asp:TextBox ID="txtMobile" Width="120" CssClass="TextBoxInChar" runat="server"></asp:TextBox>&nbsp;
            <asp:CheckBox Text=" 在通讯录中隐藏" ID="chkHideMobile" runat="server" />
                  </td>
        </tr>
        <tr>
            <td>&nbsp;家庭电话</td>
            <td >
                <asp:TextBox ID="txtHomePhone" CssClass="TextBoxInChar" runat="server"></asp:TextBox>
            </td>
            <td>
                &nbsp;办公电话</td>
            <td>
            <asp:TextBox ID="txtOfficePhone" CssClass="TextBoxInChar" runat="server"></asp:TextBox>
          </td>
        </tr>
        <tr>
            <td>&nbsp;邮寄地址</td>
            <td >
                <asp:TextBox ID="txtOfficeAddress"  CssClass="TextBoxInChar" runat="server"></asp:TextBox>
            </td>
            <td>&nbsp;邮 编</td>
            <td >
                <asp:TextBox ID="txtPostCode"  CssClass="TextBoxInChar" runat="server"></asp:TextBox>
            </td>
        </tr>
            <tr>
            <td>&nbsp;身份证号</td>
            <td >
                <asp:TextBox ID="txtIdCard"  CssClass="TextBoxInChar" runat="server"></asp:TextBox>
            </td>
            <td></td>
            <td >
                
            </td>
        </tr>
        <tr>
            <td>&nbsp;默认岗位</td>
            <td colspan="3">
                <asp:RadioButtonList ID="PositionList" runat="server">
                </asp:RadioButtonList> 
            </td>
        </tr>
        <tr>
            <td>&nbsp;个人签名</td>
            <td colspan="3" style="padding:2px">
                <table>
                    <tr>
                        <td width="100">
                            <img style="width:100px;height:50px;" alt="个人签名" src="../../SysFolder/Common/SignImage.aspx?uId=<%=EditEmployeeId %>&rnd=<%=DateTime.Now.Ticks %>" /></td>
                        <td>                
                            <asp:FileUpload ID="FileUpload1" runat="server" />&nbsp;&nbsp;<span class="red">大小要求100*50</span><br />
                            <span style="line-height:28px;">在审批打印时使用，更新签名时请先选择签名文件，然后点击右上角的【保存】</span>
                        </td>
                    </tr>
                </table>
                
            </td>
        </tr>
        <tr>
            <td>&nbsp;我的头像</td>
            <td colspan="3" style="padding:2px">
                <table>
                    <tr>
                        <td width="100">
                            <div style="padding:2px;background:white;border:1px solid #ccc;width:80px;height:104px;text-align:center;">
                                <img style="width:78px;height:100px;" alt="我的头像" src="<%=photoPath %>" />
                            </div></td>
                        <td>                
                            <asp:FileUpload ID="FileUpload2" runat="server" />&nbsp;&nbsp;<span class="red">要求长宽比为4:3</span><br />
                            <span style="line-height:28px;">先选择头像照片，然后点击右上角的【保存】</span>
                        </td>
                    </tr>
                </table>
                
            </td>
        </tr>
    </tbody>
    </table>
    <table  border="0" style="width:760px;"  align="center">
        <tr>
            <td height="40" align="left">
                <span class="green"><b>
                最后一次登录时间：<%=Session["LastLoginTime"]%>&nbsp;&nbsp;&nbsp;&nbsp;
                累计登录次数：<%=Session["LoginCount"]%> 次             
                </b>
                </span>

            </td>
        </tr>
    </table>
    </div>
    </form>
</body>
</html>


