<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DefDeptEdit.aspx.cs" Inherits="EIS.Web.SysFolder.Permission.DefDeptEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <title>部门信息</title>
    <meta http-equiv="Pragma" content="no-cache" />
    <link type="text/css" rel="stylesheet" href="../../Css/appInput.css" />
    <script type="text/javascript" src="../../js/jquery-1.7.min.js"></script>
    <script type="text/javascript" src="../../DatePicker/WdatePicker.js"></script>
    <script type="text/javascript" src="../../js/jquery.validate.min.js"></script>
    <script type="text/javascript" src="../../js/jquery.notice.js"></script>
    <script src="../../Js/jquery.cookie.js" type="text/javascript"></script>

    <style type="text/css">
        table{border-color:gray;}
        td{padding:5px;border-color:gray;height:25px;}
        body{background:white;}
        input[type=text]{width:220px;}
        .topnav button{
	        padding:1px;
	        margin:1px;
	        width: auto;
	        height:23px;
	        background-color:transparent;
	        border-width:0px;
            overflow:visible;
        }
        .topnav button img{
	        margin-right:2px;
            vertical-align:middle;
        }

        .imgbutton{
	        padding:1px 4px;
	        margin:1px;
	        width: auto;
            overflow:visible;
            cursor:pointer;
        }
        .imgbutton img{
	        margin-right:2px;
            vertical-align:middle;
        }
        
        a
        {
            text-decoration: none;
            outline-style: none;
        }
        .tabs
        {
            padding-bottom: 0px;
            list-style-type: none !important;
            margin: 0px 0px 10px;
            padding-left: 0px;
            padding-right: 0px;
            zoom: 1;
            padding-top: 0px;
        }
        .tabs:before
        {
            display: inline;
            content: "";
        }
        .tabs:after
        {
            display: inline;
            content: "";
        }
        .tabs:after
        {
            clear: both;
        }
        .tabs li
        {
            padding-left: 5px;
            float: left;
        }
        .tabs li a
        {
            display: block;
        }
        .tabs
        {
            border-bottom: #d0e1f0 1px solid;
            width: 100%;
            float: left;
        }
        .tabs li
        {
            position: relative;
            top: 1px;
        }
        .tabs li a
        {
            border-bottom: #d0e1f0 1px solid;
            border-left: #d0e1f0 1px solid;
            padding-bottom: 0px;
            line-height: 28px;
            padding-left: 15px;
            padding-right: 15px;
            background: #e3edf7;
            color: #666 !important;
            border-top: #d0e1f0 1px solid;
            margin-right: 2px;
            border-right: #d0e1f0 1px solid;
            padding-top: 0px;
            border-radius: 4px 4px 0 0;
            -webkit-border-radius: 4px 4px 0 0;
            -moz-border-radius: 4px 4px 0 0;
        }
        .tabs li a:hover
        {
            background-color: #fff;
            text-decoration: none;
        }
        .tabs li.selected a
        {
            border-bottom: transparent 1px solid;
            border-left: #81b0da 1px solid;
            background-color: #fff;
            color: #000;
            border-top: #81b0da 1px solid;
            font-weight: bold;
            border-right: #81b0da 1px solid;
            _border-bottom-color: #fff;
        }
        #tabControl{}

    </style>

    <script type="text/javascript">
        $(document).ready(function () {
            $(".Read").attr("readonly", true);

            $(".tabs>li").click(function () {
                var i = $(this).index();
                $("li.selected").removeClass("selected");
                jQuery.cookie("selTabDept", i);
                $(this).addClass("selected");
                $("#tabControl").children().hide();
                $("#tabControl").children(":eq(" + i + ")").show();

            });

            var selTab = jQuery.cookie("selTabDept");
            if (selTab) {
                $(".tabs>li").eq(selTab).click();
            }

            $("#btnSave").click(function () {
                var deptName = $("#txtName").val();
                window.parent.frames["left"].nodeNameChange(deptName);
                __doPostBack('LinkButton1', '');
            });
            $("#txtName").change(function () {
                //window.parent.frames["left"].nodeNameChange(this.value);
            });
            jQuery("#btnUpLeader").click(function () {
                openpage('../Common/PositionTree.aspx?method=1&queryfield=positionid,positionname&cid=UpLeaderId,UpLeader');
            });
            jQuery("#btnAdminCn").click(function () {
                openpage('../Common/UserTree.aspx?method=1&queryfield=empid,empname&cid=AdminId,AdminCn');
            });

            jQuery("#btnPic2").click(function () {
                openpage('../Common/UserTree.aspx?method=1&queryfield=empid,empname&cid=hidPicId2,txtPicName2');
            });

            jQuery("#btnUpLeader2").click(function () {
                openpage('../Common/UserTree.aspx?method=1&queryfield=empid,empname&cid=UpLeaderId2,UpLeader2');
            });

            jQuery("#btnDeptSfwCn").click(function () {
                openpage('../Common/UserTree.aspx?method=1&queryfield=empid,empname&cid=DeptSfwId,DeptSfwCn');
            });

        });
        function notFind() {
            alert("找不到对应的公司");
            window.location = "../../welcome.htm";
        }
        function returnList() {

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
            openCenter(url, "_blank", 600, 500);
        }
    </script>
</head>
<body>
    <form runat="server" id="form1">
    <!-- 工具栏 -->
    <div class="menubar">
        <div class="topnav" style="line-height:26px;text-align:right;padding-right:60px;">
            <asp:LinkButton ID="LinkButton2" runat="server" CssClass="hidden" onclick="LinkButton2_Click">保存后新增</asp:LinkButton>&nbsp;&nbsp;
            <button id="btnSave" class="imgbutton" type="button"><img alt="保存" src="../../img/common/ico5.gif" />保存</button>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:LinkButton ID="LinkButton1" runat="server" CssClass="hidden" onclick="LinkButton1_Click">保存</asp:LinkButton>
        </div>
    </div>
    
    <div  id="maindiv" style="width:700px;padding-top:5px;">
        <%=TipMessage %>
        <ul class="tabs">
            <li class="selected"><a href="#tabpage1">基本信息表</a></li>
            <%--<li><a href="#tabpage2">高级选项</a></li>--%>
        </ul>
        <div id="tabControl">
            <div >
            <table class='normaltbl' style="width:100%;" border="1"   align="center">
            <caption>部门信息表</caption>
            <tbody>
              <tr>
                <td  width="100">&nbsp;部门编码</td>
                <td  >
                    <asp:TextBox ID="txtCode" CssClass="TextBoxInChar" runat="server"></asp:TextBox>
                </td>
                <td   width="100">&nbsp;部门名称</td>
                <td  >
                    <asp:TextBox ID="txtName" CssClass="TextBoxInChar" runat="server"></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td>&nbsp;部门简称</td>
                <td >
                    <asp:TextBox ID="txtAbbr" CssClass="TextBoxInChar" runat="server"></asp:TextBox>
                </td>
                <td>&nbsp;部门全称</td>
                <td >
                    <asp:TextBox ID="txtFullName" CssClass="TextBoxInChar" runat="server"></asp:TextBox>
            
                </td>

           </tr>

            <tr>
                <td>&nbsp;部门状态</td>
                <td >
                    <asp:RadioButtonList ID="RadioState" runat="server" 
                        RepeatDirection="Horizontal" RepeatLayout="Flow" Width="193px">
                        <asp:ListItem>正常</asp:ListItem>
                        <asp:ListItem>停用</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
                        <td>&nbsp;部门级别</td>
                <td>
                    <asp:DropDownList ID="listType" runat="server">
                    </asp:DropDownList>
                </td>
            </tr> 
                <tr>             <td>&nbsp;部门WBS</td>
                <td >
                    <asp:TextBox ID="txtWbs" CssClass="TextBoxInChar Read" runat="server"></asp:TextBox>
                </td>
               
                             <td>&nbsp;排序</td>
                    <td>
                        <asp:TextBox ID="txtOrder" CssClass="TextBoxInChar" runat="server"></asp:TextBox>
                    </td>
            </tr> 
              <tr>
                <td>
                    &nbsp;部门负责人岗位</td>
                <td>
        
                  <asp:DropDownList ID="selPosition" runat="server">
                  </asp:DropDownList>
                 </td>
                             <td>
                    &nbsp;部门副负责人</td>
                    <td>
                    <asp:TextBox ID="txtPicName2" CssClass="TextBoxInChar Read" Width="180" runat="server"></asp:TextBox>
                    <a href="javascript:" id="btnPic2">【选择】</a>
                    <asp:HiddenField ID="hidPicId2" runat="server" />

                  </td>
                </tr>
                <tr>
                    <td>
                    &nbsp;部门分管领导</td>
                    <td>
                    <asp:TextBox ID="UpLeader2" CssClass="TextBoxInChar Read" Width="180" runat="server"></asp:TextBox>
                    <a href="javascript:" id="btnUpLeader2">【选择】</a>
                    <asp:HiddenField ID="UpLeaderId2" runat="server" />

                  </td>
                <td>&nbsp;分管领导岗位</td>
                <td >
                    <asp:TextBox ID="UpLeader" CssClass="TextBoxInChar Read" Width="180" runat="server"></asp:TextBox>
                    <a href="javascript:" id="btnUpLeader">【选择】</a>
                    <asp:HiddenField ID="UpLeaderId" runat="server" />

                </td>

                </tr>

                <tr>
                <td>&nbsp;部门管理员</td>
                <td >
                    <asp:TextBox ID="AdminCn" CssClass="TextBoxInChar Read" Width="180" runat="server"></asp:TextBox>
                    <a href="javascript:" id="btnAdminCn">【选择】</a>
                    <asp:HiddenField ID="AdminId" runat="server" />

                </td>
                <td>
                    &nbsp;部门公文收发员</td>
                <td>
                    <asp:TextBox ID="DeptSfwCn" CssClass="TextBoxInChar Read" Width="180" runat="server"></asp:TextBox>
                    <a href="javascript:" id="btnDeptSfwCn">【选择】</a>
                    <asp:HiddenField ID="DeptSfwId" runat="server" />
                  </td>
                </tr>
                <tr style="display:none">
                  
                    <td>&nbsp;工作日历</td>
                    <td>
                        <asp:DropDownList ID="selCalendar" runat="server">
                        </asp:DropDownList>
                    </td>
                

                     <td>&nbsp;部门属性</td>
                <td>
                    <asp:DropDownList ID="listProp" runat="server">
                    </asp:DropDownList>
                </td>
                </tr>
            </tbody>
            </table>
            </div>
            <div>
            <table>
                <tr>
                    <td></td>
                </tr>
            </table>
            </div>
    </div>
    </div>
    </form>
</body>
</html>