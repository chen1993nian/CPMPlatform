<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="InstanceLog.ascx.cs" Inherits="EIS.Web.SysFolder.WorkFlow.UserControl.InstanceLog" %>
<asp:GridView CssClass="dealInfoTbl" ID="LogGridView1" runat="server" AutoGenerateColumns="False" 
    CellPadding="3" Width="100%" onrowdatabound="GridView1_RowDataBound">
    <RowStyle Height="25" />
    <Columns>
        <asp:TemplateField HeaderText="序号" HeaderStyle-HorizontalAlign="Center">
            <ItemStyle Width="40" ForeColor="Red" HorizontalAlign="Center" />
             <ItemTemplate>
                <b><%# this.LogGridView1.PageIndex * this.LogGridView1.PageSize + this.LogGridView1.Rows.Count + 1%></b>
             </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField ItemStyle-Width="50"  HeaderText="操作人" DataField="EmpName" />
        <asp:BoundField ItemStyle-Width="100" HeaderText="操作时间" DataField="LogTime"    DataFormatString="{0:MM月dd日 HH:mm}" />
        <asp:BoundField HeaderText="处理意见" DataField="LogContent" />
    </Columns>
</asp:GridView>
