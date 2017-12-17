<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WorkflowImg.ascx.cs" Inherits="EIS.WebBase.SysFolder.WorkFlow.UserControl.WorkflowImg" %>
<img id="wfpic" src="<%=AppRoot %>/Sysfolder/Workflow/GetWorkflowImg.aspx?workflowId=<%=workflowId %>&t=<%=System.DateTime.Now.Ticks%>" border="0" alt=""  usemap="#FlowMap"/>
<map id="FlowMap" name="FlowMap">
<%=hotPoint%>
</map>
