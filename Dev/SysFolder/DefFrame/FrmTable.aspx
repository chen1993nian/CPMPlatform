<%@ Page Title="" Language="C#" MasterPageFile="~/SysFolder/DefFrame/FrmTable.Master" AutoEventWireup="true" CodeBehind="FrmTableInfo.aspx.cs" Inherits="EIS.Studio.SysFolder.DefFrame.FrmTableInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        label{font-weight:normal;cursor:pointer;}
        #txtOrder,#ddlOrder{display:inline;}
        #rblShow label, #rblArchive label, #rblDel label,#rblLog label{padding-left:5px;padding-right:10px;}
        .panel-body{background-color:rgb(249, 250, 254);}
    </style>
    <script type="text/javascript">
        jQuery(function () {
            $("ul.nav>li:eq(0)").addClass("active");
            $("#_btnSubmit").click(function () { $("#btnSubmit").click() });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="panel panel-default">
        <div class="panel-heading">
            <h3 class="panel-title">
            <span class="glyphicon glyphicon-th-large"></span>
            业务基本信息</h3>
        </div>
        <div class="panel-body">
            <div class="form-group  form-group-sm">
                <label for="txtTblName" class="col-sm-2 control-label">业务名称：</label>
                <div class="col-sm-3">
                    <asp:TextBox ID="txtTblName" runat="server"  CssClass="form-control input-sm" ClientIDMode="Static"></asp:TextBox>
                </div>
                <label for="txtNameCn" class="col-sm-2 control-label">中文名称：</label>
                <div class="col-sm-3">
                    <asp:TextBox ID="txtNameCn" runat="server"  CssClass="form-control input-sm" ClientIDMode="Static"></asp:TextBox>
                </div>
            </div>

            <!-- 第2行开始 -->
            <div class="form-group  form-group-sm">
                <label class="col-sm-2 control-label">审批状态：</label>
                <div class="col-sm-3">
                    <asp:RadioButtonList ID="rblShow" runat="server"  ClientIDMode="Static"
                        RepeatDirection="Horizontal">
                        <asp:ListItem Selected="True" Value="1">显示</asp:ListItem>
                        <asp:ListItem Value="0">隐藏</asp:ListItem>
                    </asp:RadioButtonList>

                </div>
                <label class="col-sm-2 control-label">归档状态：</label>
                <div class="col-sm-3">
                    <asp:RadioButtonList ID="rblArchive" runat="server"  ClientIDMode="Static"
                        RepeatDirection="Horizontal">
                        <asp:ListItem Selected="True" Value="1">显示</asp:ListItem>
                        <asp:ListItem Value="0">隐藏</asp:ListItem>
                    </asp:RadioButtonList>
                </div>
            </div>
            
            <!-- 第3行开始 -->
            <div class="form-group  form-group-sm">
                <label for="txtOrder" class="col-sm-2 control-label">排序字段：</label>
                <div class="col-sm-3">
                    <asp:TextBox ID="txtOrder" runat="server" Width="160"  CssClass="form-control input-sm" ClientIDMode="Static"></asp:TextBox>

                    <asp:DropDownList ID="ddlOrder" runat="server" Width="80" CssClass="form-control input-sm" ClientIDMode="Static">
                        <asp:ListItem Selected="True" Value="asc" Text="升序"></asp:ListItem>
                        <asp:ListItem Value="desc" Text="降序"></asp:ListItem>
                     </asp:DropDownList>
                </div>
                <label class="col-sm-2 control-label">删除模式：</label>
                <div class="col-sm-3">
                    <asp:RadioButtonList ID="rblDel" runat="server"  ClientIDMode="Static"
                        RepeatDirection="Horizontal">
                        <asp:ListItem Selected="True" Value="0">物理删除</asp:ListItem>
                        <asp:ListItem Value="1">逻辑删除</asp:ListItem>
                    </asp:RadioButtonList>
                </div>
            </div>

            <!-- 第4行开始 -->
            <div class="form-group  form-group-sm">
                <label class="col-sm-2 control-label">日志级别：</label>
                <div class="col-sm-3">
                    <asp:RadioButtonList ID="rblLog" runat="server"  ClientIDMode="Static"
                        RepeatDirection="Horizontal">
                        <asp:ListItem Selected="True" Value="0">关闭</asp:ListItem>
                        <asp:ListItem Value="1">简单</asp:ListItem>
                        <asp:ListItem Value="2">详细</asp:ListItem>
                    </asp:RadioButtonList>
                </div>
            </div>

            <!-- 第5行开始 -->
            <div class="form-group  form-group-sm">
                <label for="txtDataLog" class="col-sm-2 control-label">日志模板：</label>
                <div class="col-sm-8">
                    <asp:TextBox ID="txtDataLog" runat="server"  CssClass="form-control input-sm" ClientIDMode="Static"></asp:TextBox>
                    <div class="alert alert-info" style="margin-top:5px;margin-bottom:0px;padding:7px;" role="alert">
                    <span class="glyphicon glyphicon-info-sign"></span>
                    提示：日志模板中业务字段的写法为 {字段名称}</div>
                </div>
            </div>

            <!-- 第6行开始 -->
            <div class="form-group  form-group-md">
                <label for="txtDataLog" class="col-sm-2 control-label">列表语句：</label>
                <div class="col-sm-8">
                    <asp:TextBox ID="txtListSQL" runat="server" TextMode="MultiLine" Rows="3"  CssClass="form-control" ClientIDMode="Static"></asp:TextBox>
                </div>
                
            </div>

            <!-- 第7行开始 -->
            <div class="form-group  form-group-md">
                <label for="txtDataLog" class="col-sm-2 control-label">单条语句：</label>
                <div class="col-sm-8">
                    <asp:TextBox ID="txtDetailSQL" runat="server" TextMode="MultiLine" Rows="3"  CssClass="form-control" ClientIDMode="Static"></asp:TextBox>
                </div>
                
            </div>
            <div class="form-group form-group-sm">

            <div class="col-sm-offset-2 col-sm-6">
                <button type="button" id="_btnSubmit" class="btn btn-primary">
                    <span class="glyphicon glyphicon-ok-sign"></span> 提 交
                </button>

                <button type="button" id="_btnCopy" class="btn btn-success">
                    <span class="glyphicon glyphicon-plus-sign"></span> 复 制
                </button>
                <button type="button" id="_btnLock" class="btn btn-info">
                    <span class="glyphicon glyphicon-expand"></span> 锁 定
                </button>

                <asp:Button ID="btnSubmit" runat="server" ClientIDMode="Static" CssClass="hidden" onclick="btnSubmit_Click" Text=" 提 交 " />
                <asp:Button ID="btnCopy" runat="server" ClientIDMode="Static" CssClass="hidden" Text=" 复 制 " />
                <asp:Button ID="btnLock" runat="server" ClientIDMode="Static" CssClass="hidden" Text=" 锁 定 " />
            </div>
            </div>
        </div>
        </div>

    </div>

</asp:Content>
