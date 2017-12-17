<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserDealInfo.ascx.cs" Inherits="EIS.WebBase.SysFolder.WorkFlow.UserControl.UserDealInfo" %>
<asp:GridView CssClass="dealInfoTbl" ID="GridView1" runat="server" AutoGenerateColumns="False" 
    CellPadding="3" Width="100%" onrowdatabound="GridView1_RowDataBound">
    <RowStyle Height="25" />
    <Columns>
        <asp:TemplateField HeaderText="序号">
            <ItemStyle Width="40" ForeColor="Red" />
             <ItemTemplate>
                <b><%# this.GridView1.PageIndex * this.GridView1.PageSize + this.GridView1.Rows.Count + 1%></b>
             </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField ItemStyle-Width="120"  HeaderText="步骤名称" DataField="TaskName"/>
        <asp:BoundField ItemStyle-Width="50"  HeaderText="处理人" DataField="EmployeeName" />
        <asp:BoundField ItemStyle-Width="40"  HeaderText="审批" DataField="DealAction" />
        <asp:BoundField HeaderText="处理意见" DataField="DealAdvice" />
        <asp:BoundField ItemStyle-Width="100" HeaderText="查看时间" DataField="ReadTime"  DataFormatString="{0:MM月dd日 HH:mm}"  />
        <asp:BoundField ItemStyle-Width="100" HeaderText="处理时间" DataField="DealTime"    DataFormatString="{0:MM月dd日 HH:mm}" />
        <asp:TemplateField HeaderText="是否查看过" Visible="false">
            <ItemStyle Width="80" ForeColor="Red" />
             <ItemTemplate>
                <%#Eval("IsRead2").ToString() == "是" ? "<span class='green'>是</span>" : "<span class='red'>否</span>"%>
             </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>