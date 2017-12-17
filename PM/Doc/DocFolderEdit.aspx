<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DocFolderEdit.aspx.cs" Inherits="EIS.Web.Doc.DocFolderEdit" %>



<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>目录信息编辑</title>
    <meta http-equiv="Pragma" content="no-cache"/>
    <link rel="stylesheet" type="text/css" href="../Css/DefStyle.css" />
    <script src="../Js/jquery-1.7.min.js" type="text/javascript"></script>
    <script src="../Js/Tools.js" type="text/javascript"></script>
    <style type="text/css">
        input[type=submit],input[type=button]{padding:0px 10px;height:28px;}
        .maindiv{text-align:left;}
        label{cursor:pointer;}
        .data-table{
            border-collapse:collapse;
            border:1px solid #dae2e5;
            background-color:White;
            width:100%;
            top:0px;
            left:0px;
            font-size:12px;    
            }
        .data-table >thead>tr>th{
            background-color:#e5f1f6;
            color:#414141;
            text-align:center;
            font-size:12px;
            font-weight:normal;
            height:30px;
            border-right:1px solid #dae2e5;
            cursor:pointer;
            }
        .data-table >tbody>tr>td{
            text-align:center;
            vertical-align:middle;
            padding:5px 0px;
            border:1px solid #dae2e5;
            }
        a{color:Blue;text-decoration:none;}
        a:hover{text-decoration:underline;}
    
    </style>
     <script type="text/javascript">
         function afterSave() {
             if (window.opener) {
                 window.opener.changeName("<%=folderId %>", document.getElementById("TextBox2").value);
                window.opener.app_query();
            }
            window.close();
        }
        function afterNew() {
            if (window.opener) {
                window.opener.newFolder("<%=folderId %>", document.getElementById("TextBox2").value);
                window.opener.app_query();
            }
            window.close();
        }
        function afterSelUser(cId, arrId, arrName) {
            for (var i = 0; i < arrId.length; i++) {
                var index = addLimit({ "autoid": "", "objid": arrId[i], "objtype": "1", "limit": "1", "state": "new" });
                if (index > -1)
                    AddRow(arrId[i], arrName[i], index);
                //else
                //alert("列表中已经存在该用户，不能重复添加");
            }
        }
        function afterSelDept(cId, arrId, arrName) {
            for (var i = 0; i < arrId.length; i++) {
                var index = addLimit({ "autoid": "", "objid": arrId[i], "objtype": "2", "limit": "1", "state": "new" });
                if (index > -1)
                    AddRow(arrId[i], arrName[i], index);
                //else
                //alert("列表中已经存在该部门，不能重复添加");
            }
        }
        function afterSelRole(cId, arrId, arrName) {
            for (var i = 0; i < arrId.length; i++) {
                var index = addLimit({ "autoid": "", "objid": arrId[i], "objtype": "3", "limit": "1", "state": "new" });
                if (index > -1)
                    AddRow(arrId[i], arrName[i], index);
                //else
                //alert("列表中已经存在该角色，不能重复添加");
            }
        }
        //返回-1 代表有冲突，如果成功返回索引
        function addLimit(obj) {
            for (var i = 0; i < arrLimit.length; i++) {
                if (arrLimit[i].objid == obj.objid) {
                    if (arrLimit[i].state == "unkown") {
                        arrLimit[i].state = "new";
                        return i;
                    }
                    else if (arrLimit[i].state == "delete") {
                        arrLimit[i].state = "update";
                        return i;
                    }
                    else {
                        return -1;
                    }
                }
            }
            arrLimit.push(obj);
            return arrLimit.length - 1;
        }
        jQuery(function () {
            $(".btnAddUser").click(function () {
                openpage("../SysFolder/Common/UserTree.aspx?method=1&queryfield=empid,empname&cid=Hidden1,Hidden2&callback=afterSelUser");
            });
            $(".btnAddDept").click(function () {
                openpage("../SysFolder/Common/DeptTree.aspx?method=1&queryfield=deptid,deptname&cid=Hidden1,Hidden2&callback=afterSelDept");
            });
            $(".btnAddRole").click(function () {
                openpage("../SysFolder/Common/RoleTree.aspx?method=1&queryfield=roleid,rolename&cid=Hidden1,Hidden2&callback=afterSelRole");
            });
            $(".btnAddAll").click(function () {
                var index = addLimit({ "autoid": "", "objid": "everyone", "objtype": "0", "limit": "1", "state": "new" });
                if (index > -1)
                    AddRow("everyone", "全体员工", index);
            });
            $(".linkdel").live("click", function (e) {
                if (!confirm("确认删除本条设置吗"))
                    return;
                var i = $(this).attr("oindex");
                if (arrLimit[i].state == "new") {
                    arrLimit[i].state = "unkown"
                }
                else if (arrLimit[i].state == "update") {
                    arrLimit[i].state = "delete"
                }
                else if (arrLimit[i].state == "unchanged") {
                    arrLimit[i].state = "delete"
                }
                $(this).parents("tr:first").remove();
            });
            $(".selLimit").live("change", function (e) {

                var i = $(this).attr("oindex");
                if (arrLimit[i].state == "unchanged") {
                    arrLimit[i].state = "update"
                }
                arrLimit[i].limit = $(this).val();
            });
            //把权限设置序列化
            $("#Button2").click(function () {
                var strLimit = [];
                for (var i = 0; i < arrLimit.length; i++) {
                    strLimit.push(arrLimit[i].state + "|" + arrLimit[i].autoid + "|" + arrLimit[i].objid + "|" + arrLimit[i].objtype + "|" + arrLimit[i].limit);
                }
                $("#LimitData").val(strLimit.join("$"));
                return true;
            });
        });
        var arrLimit = [<%=arrLimit %>];
        function AddRow(objId, objName, maxRow) {

            var arrHtml = [];
            var newEle = $("<tr oindex='" + maxRow + "' align='center'><td></td><td></td><td></td></tr>");

            arrHtml.push("<select class='selLimit' oindex='" + maxRow + "' size='1'>"
            , " <option value='0'>无权限</option>"
            , " <option value='1' selected>可见</option>"
            , " <option value='2'>读取</option>"
            , "	<option value='3'>下载</option>"
            , "	<option value='6'>编辑</option>"
            , "	<option value='9'>全部</option>"
            , "	</select>");

            $("td:eq(0)", newEle).append(objName);
            $("td:eq(1)", newEle).append(arrHtml.join(""));
            $("td:eq(2)", newEle).append("<a class='linkdel' href='javascript:' oindex='" + maxRow + "'>删除</a>");
            $("#tblbody").append(newEle);
        }

     </script>
</head>
<body class="bgbody">
    <form id="form1" runat="server">
    <div class="maindiv">
    <table>
        <tr>
        <td height="30" width="80">文件夹名称：</td>
        <td>
            <asp:TextBox ID="TextBox2" runat="server" Width="160" CssClass="textbox"></asp:TextBox>
            <br />
            <br />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                ControlToValidate="TextBox2" Display="Dynamic" ErrorMessage="文件夹名称不能为空" 
                ForeColor="Red"></asp:RequiredFieldValidator>
            </td>
        <td height="30">&nbsp;&nbsp;同级排序：</td>
        <td>
            <asp:TextBox ID="TextBox3" runat="server" Width="160" CssClass="textbox"></asp:TextBox></td>
        </tr>
        <tr>
        <td>系统编号：</td>
        <td>
            <asp:TextBox ID="TextBox1" runat="server" Width="160" CssClass="textbox" ReadOnly="true"></asp:TextBox>            
        </td>
        <td height="30">&nbsp;&nbsp;权限继承：</td>
            <td>
            <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal">
                <asp:ListItem Value="1" Selected="True" Text="继承父级权限" />
                <asp:ListItem Value="0" Text="不继承" />
            </asp:RadioButtonList>
            </td>
    </tr>
        <tr>
            <td>权限设置：</td>
            <td colspan="3">
                <div style="height:200px;overflow:auto;background-color:White;border-bottom:1px dashed #ccc;">
                    <table id="" class="data-table" border="0" cellpadding="0" cellspacing="0">
                        <thead>
                         <tr>
                            <th>授权对象</th>
                            <th>权限</th>
                            <th>删除</th>
                        </tr>
                        </thead>
                        <tbody id="tblbody">
                            <%=sbList%>
                        </tbody>
                    </table>
                </div>

                <div style="padding:3px;">
                    <a href="javascript:" class="linkbtn btnAddAll">【添加全体员工】</a>
                    <a href="javascript:" class="linkbtn btnAddUser">【添加用户】</a>
                    <a href="javascript:" class="linkbtn btnAddDept">【添加部门】</a>
                    <a href="javascript:" class="linkbtn btnAddRole">【添加角色】</a>
                </div>
            </td>
        </tr>
        <tr>
        <td>
        </td>
        <td height="30" align="center" colspan="3">
        <br />
            <asp:Button AccessKey="s" ID="Button2" runat="server" Text=" 保存 " OnClick="Button2_Click" />
            &nbsp;
            <input accesskey="c" type="button" value=" 关闭 " onclick="window.close();"/>
            </td>
    </tr>
    </table>
        <input id="Hidden1" type="hidden" value="" />
        <input id="Hidden2" type="hidden" value="" />
        <input id="LimitData" type="hidden" runat="server" value="" />
    </div>
    </form>
</body>
</html>
