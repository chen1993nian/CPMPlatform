<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="InstanceImg.ascx.cs" Inherits="EIS.WebBase.SysFolder.WorkFlow.UserControl.InstanceImg" %>
<img id="wfpic" src="<%=AppRoot %>/Sysfolder/Workflow/GetInstanceImg.aspx?instanceId=<%=InstanceId %>&t=<%=System.DateTime.Now.Ticks%>" border="0" alt=""  usemap="#FlowMap"/>

<map id="FlowMap" name="FlowMap">
<%=hotPoint%>
</map>
