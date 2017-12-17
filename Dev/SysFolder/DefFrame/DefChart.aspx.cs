


using EIS.AppBase;
using EIS.DataAccess;
using EIS.DataModel.Access;
using EIS.DataModel.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace EIS.Studio.SysFolder.DefFrame
{
	public partial class DefChart : AdminPageBase
	{
		

		public string tblName = "";

		public string flashPath = "";

		public string w = "";

		public string h = "";

	

		protected void LinkButton1_Click(object sender, EventArgs e)
		{
			_AppChart __AppChart = new _AppChart();
			AppChart modelByQueryCode = __AppChart.GetModelByQueryCode(this.tblName);
			if (modelByQueryCode != null)
			{
				modelByQueryCode.ChartType = this.ddlType.SelectedValue;
				modelByQueryCode.ChartTitle = this.txtTitle.Text;
				modelByQueryCode.xAxisName = this.ddlxAxis.SelectedValue;
				modelByQueryCode.yAxisName = this.ddlyAxis.SelectedValue;
				modelByQueryCode.xAxisTitle = this.ddlxAxis.SelectedItem.Text;
				modelByQueryCode.yAxisTitle = this.ddlyAxis.SelectedItem.Text;
				modelByQueryCode.ShowValue = (this.chkDisp.Checked ? "1" : "0");
				modelByQueryCode.ChartWidth = this.txtWidth.Text;
				modelByQueryCode.ChartHeight = this.txtHeight.Text;
				__AppChart.Update(modelByQueryCode);
			}
			else
			{
				modelByQueryCode = new AppChart(base.UserInfo)
				{
					ChartType = this.ddlType.SelectedValue,
					ChartTitle = this.txtTitle.Text,
					xAxisName = this.ddlxAxis.SelectedValue,
					yAxisName = this.ddlyAxis.SelectedValue,
					xAxisTitle = this.ddlxAxis.Text,
					yAxisTitle = this.ddlyAxis.Text,
					ShowValue = (this.chkDisp.Checked ? "1" : "0"),
					ChartWidth = this.txtWidth.Text,
					ChartHeight = this.txtHeight.Text,
					QueryCode = this.tblName
				};
				__AppChart.Add(modelByQueryCode);
			}
			string str = string.Concat("select ChartPath from T_E_Sys_ChartConfig where _autoid='", modelByQueryCode.ChartType, "'");
			this.flashPath = SysDatabase.ExecuteScalar(str).ToString();
			this.w = modelByQueryCode.ChartWidth;
			this.h = modelByQueryCode.ChartHeight;
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			this.tblName = base.GetParaValue("tblName");
			if (!base.IsPostBack)
			{
				AppChart modelByQueryCode = (new _AppChart()).GetModelByQueryCode(this.tblName);
				DataTable dataTable = SysDatabase.ExecuteTable("select _autoId ,TypeName from T_E_Sys_ChartConfig ");
				this.ddlType.DataSource = dataTable;
				this.ddlType.DataTextField = "TypeName";
				this.ddlType.DataValueField = "_autoId";
				this.ddlType.DataBind();
				foreach (FieldInfo tableField in (new _FieldInfo()).GetTableFields(this.tblName))
				{
					ListItem listItem = new ListItem()
					{
						Text = tableField.FieldNameCn,
						Value = tableField.FieldName
					};
					this.ddlxAxis.Items.Add(listItem);
					ListItem listItem1 = new ListItem()
					{
						Text = tableField.FieldNameCn,
						Value = tableField.FieldName
					};
					this.ddlyAxis.Items.Add(listItem1);
				}
				if (modelByQueryCode != null)
				{
					this.txtTitle.Text = modelByQueryCode.ChartTitle;
					this.txtWidth.Text = modelByQueryCode.ChartWidth;
					this.txtHeight.Text = modelByQueryCode.ChartHeight;
					this.chkDisp.Checked = modelByQueryCode.ShowValue == "1";
					if (modelByQueryCode.xAxisName != "")
					{
						this.ddlxAxis.SelectedValue = modelByQueryCode.xAxisName;
					}
					if (modelByQueryCode.yAxisName != "")
					{
						this.ddlyAxis.SelectedValue = modelByQueryCode.yAxisName;
					}
					if (modelByQueryCode.ChartType != "")
					{
						this.ddlType.SelectedValue = modelByQueryCode.ChartType;
					}
					string str = string.Concat("select ChartPath from T_E_Sys_ChartConfig where _autoid='", modelByQueryCode.ChartType, "'");
					this.flashPath = SysDatabase.ExecuteScalar(str).ToString();
					this.w = modelByQueryCode.ChartWidth;
					this.h = modelByQueryCode.ChartHeight;
				}
			}
		}
	}
}