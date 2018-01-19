using EIS.AppBase;
using EIS.DataAccess;
using EIS.DataModel.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;

namespace EIS.DataModel.Access
{
	public class _AppChart
	{
		private DbTransaction dbTransaction_0 = null;

		public _AppChart()
		{
		}

		public _AppChart(DbTransaction tran)
		{
			this.dbTransaction_0 = tran;
		}

		public int Add(AppChart model)
		{
			int num;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("Insert T_E_Sys_Chart (\r\n \t\t\t\t\t_AutoID,\r\n\t\t\t\t\t_UserName,\r\n\t\t\t\t\t_OrgCode,\r\n\t\t\t\t\t_CreateTime,\r\n\t\t\t\t\t_UpdateTime,\r\n\t\t\t\t\t_IsDel,\r\n\t\t\t\t\tQueryCode,\r\n\t\t\t\t\tChartTitle,\r\n\t\t\t\t\txAxisName,\r\n\t\t\t\t\tyAxisName,\r\n\t\t\t\t\txAxisTitle,\r\n\t\t\t\t\tyAxisTitle,\r\n\t\t\t\t\tChartType,\r\n\t\t\t\t\tChartWidth,\r\n\t\t\t\t\tChartHeight,\r\n\t\t\t\t\tShowValue\r\n\r\n\t\t\t) values(\r\n\t\t\t\t\t@_AutoID,\r\n\t\t\t\t\t@_UserName,\r\n\t\t\t\t\t@_OrgCode,\r\n\t\t\t\t\t@_CreateTime,\r\n\t\t\t\t\t@_UpdateTime,\r\n\t\t\t\t\t@_IsDel,\r\n\t\t\t\t\t@QueryCode,\r\n\t\t\t\t\t@ChartTitle,\r\n\t\t\t\t\t@xAxisName,\r\n\t\t\t\t\t@yAxisName,\r\n\t\t\t\t\t@xAxisTitle,\r\n\t\t\t\t\t@xAxisTitle,\r\n\t\t\t\t\t@ChartType,\r\n\t\t\t\t\t@ChartWidth,\r\n\t\t\t\t\t@ChartHeight,\r\n\t\t\t\t\t@ShowValue\r\n\t\t\t)");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "@_AutoID", DbType.String, model._AutoID);
			SysDatabase.AddInParameter(sqlStringCommand, "@_UserName", DbType.String, model._UserName);
			SysDatabase.AddInParameter(sqlStringCommand, "@_OrgCode", DbType.String, model._OrgCode);
			SysDatabase.AddInParameter(sqlStringCommand, "@_CreateTime", DbType.DateTime, model._CreateTime);
			SysDatabase.AddInParameter(sqlStringCommand, "@_UpdateTime", DbType.DateTime, model._UpdateTime);
			SysDatabase.AddInParameter(sqlStringCommand, "@_IsDel", DbType.Int32, model._IsDel);
			SysDatabase.AddInParameter(sqlStringCommand, "@QueryCode", DbType.String, model.QueryCode);
			SysDatabase.AddInParameter(sqlStringCommand, "@ChartTitle", DbType.String, model.ChartTitle);
			SysDatabase.AddInParameter(sqlStringCommand, "@xAxisName", DbType.String, model.xAxisName);
			SysDatabase.AddInParameter(sqlStringCommand, "@yAxisName", DbType.String, model.yAxisName);
			SysDatabase.AddInParameter(sqlStringCommand, "@xAxisTitle", DbType.String, model.xAxisTitle);
			SysDatabase.AddInParameter(sqlStringCommand, "@yAxisTitle", DbType.String, model.yAxisTitle);
			SysDatabase.AddInParameter(sqlStringCommand, "@ChartType", DbType.String, model.ChartType);
			SysDatabase.AddInParameter(sqlStringCommand, "@ChartWidth", DbType.String, model.ChartWidth);
			SysDatabase.AddInParameter(sqlStringCommand, "@ChartHeight", DbType.String, model.ChartHeight);
			SysDatabase.AddInParameter(sqlStringCommand, "@ShowValue", DbType.String, model.ShowValue);
			num = (this.dbTransaction_0 == null ? SysDatabase.ExecuteNonQuery(sqlStringCommand) : SysDatabase.ExecuteNonQuery(sqlStringCommand, this.dbTransaction_0));
			return num;
		}

		public int Delete(string msgId)
		{
			int num;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("update T_E_Sys_Chart set \r\n\t\t\t\t\t_IsDel=@_IsDel\r\n\t\t\t\t\twhere _AutoID=@_AutoID\r\n\t\t\t ");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "@_AutoID", DbType.String, msgId);
			SysDatabase.AddInParameter(sqlStringCommand, "@_IsDel", DbType.Int32, 1);
			num = (this.dbTransaction_0 == null ? SysDatabase.ExecuteNonQuery(sqlStringCommand) : SysDatabase.ExecuteNonQuery(sqlStringCommand, this.dbTransaction_0));
			return num;
		}

		public DataTable GetList(string strWhere)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select *  FROM T_E_Sys_Chart ");
			if (strWhere.Trim() != "")
			{
				stringBuilder.Append(string.Concat(" where ", strWhere));
			}
			return SysDatabase.ExecuteTable(stringBuilder.ToString());
		}

		public AppChart GetModel(string string_0)
		{
			AppChart model;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select  top 1 * from T_E_Sys_Chart ");
			stringBuilder.Append(" where _AutoID=@_AutoID ");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "@_AutoID", DbType.String, string_0);
			DataTable dataTable = SysDatabase.ExecuteTable(sqlStringCommand);
			if (dataTable.Rows.Count <= 0)
			{
				model = null;
			}
			else
			{
				model = this.GetModel(dataTable.Rows[0]);
			}
			return model;
		}

		public AppChart GetModel(DataRow dataRow_0)
		{
			AppChart appChart = new AppChart()
			{
				_AutoID = dataRow_0["_AutoID"].ToString(),
				_UserName = dataRow_0["_UserName"].ToString(),
				_OrgCode = dataRow_0["_OrgCode"].ToString()
			};
			if (dataRow_0["_CreateTime"].ToString() != "")
			{
				appChart._CreateTime = DateTime.Parse(dataRow_0["_CreateTime"].ToString());
			}
			if (dataRow_0["_UpdateTime"].ToString() != "")
			{
				appChart._UpdateTime = DateTime.Parse(dataRow_0["_UpdateTime"].ToString());
			}
			if (dataRow_0["_IsDel"].ToString() != "")
			{
				appChart._IsDel = int.Parse(dataRow_0["_IsDel"].ToString());
			}
			appChart.QueryCode = dataRow_0["QueryCode"].ToString();
			appChart.ChartTitle = dataRow_0["ChartTitle"].ToString();
			appChart.xAxisName = dataRow_0["xAxisName"].ToString();
			appChart.yAxisName = dataRow_0["yAxisName"].ToString();
			appChart.xAxisTitle = dataRow_0["xAxisTitle"].ToString();
			appChart.yAxisTitle = dataRow_0["yAxisTitle"].ToString();
			appChart.ChartType = dataRow_0["ChartType"].ToString();
			appChart.ChartWidth = dataRow_0["ChartWidth"].ToString();
			appChart.ChartHeight = dataRow_0["ChartHeight"].ToString();
			appChart.ShowValue = dataRow_0["ShowValue"].ToString();
			return appChart;
		}

		public AppChart GetModelByQueryCode(string queryCode)
		{
			AppChart model;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select  top 1 * from T_E_Sys_Chart ");
			stringBuilder.Append(" where queryCode=@queryCode ");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "@queryCode", DbType.String, queryCode);
			DataTable dataTable = SysDatabase.ExecuteTable(sqlStringCommand);
			if (dataTable.Rows.Count <= 0)
			{
				model = null;
			}
			else
			{
				model = this.GetModel(dataTable.Rows[0]);
			}
			return model;
		}

		public IList<AppChart> GetModelList(string strWhere)
		{
			StringBuilder stringBuilder = new StringBuilder();
			List<AppChart> appCharts = new List<AppChart>();
			stringBuilder.Append("select *  FROM T_E_Sys_Chart ");
			if (strWhere.Trim() != "")
			{
				stringBuilder.Append(string.Concat(" where ", strWhere));
			}
			foreach (DataRow row in SysDatabase.ExecuteTable(stringBuilder.ToString()).Rows)
			{
				appCharts.Add(this.GetModel(row));
			}
			return appCharts;
		}

		public int Remove(string string_0)
		{
			int num;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("delete from T_E_Sys_Chart ");
			stringBuilder.Append(" where _AutoID=@_AutoID ;");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "@_AutoID", DbType.String, string_0);
			num = (this.dbTransaction_0 == null ? SysDatabase.ExecuteNonQuery(sqlStringCommand) : SysDatabase.ExecuteNonQuery(sqlStringCommand, this.dbTransaction_0));
			return num;
		}

		public int Update(AppChart model)
		{
			int num;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("update T_E_Sys_Chart set \r\n\t\t\t\t\t_UpdateTime=@_UpdateTime,\r\n\t\t\t\t\t_IsDel=@_IsDel,\r\n\r\n\t\t\t\t\tQueryCode =@QueryCode,\r\n\t\t\t\t\tChartTitle=@ChartTitle,\r\n\t\t\t\t\txAxisName=@xAxisName,\r\n\t\t\t\t\tyAxisName=@yAxisName,\r\n\t\t\t\t\txAxisTitle=@xAxisTitle,\r\n\t\t\t\t\tyAxisTitle=@yAxisTitle,\r\n\t\t\t\t\tChartType=@ChartType,\r\n\t\t\t\t\tChartWidth=@ChartWidth,\r\n\t\t\t\t\tChartHeight=@ChartHeight,\r\n\t\t\t\t\tShowValue=@ShowValue\r\n\r\n\t\t\t\t\twhere _AutoID=@_AutoID\r\n\t\t\t ");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "@_AutoID", DbType.String, model._AutoID);
			SysDatabase.AddInParameter(sqlStringCommand, "@_UpdateTime", DbType.DateTime, model._UpdateTime);
			SysDatabase.AddInParameter(sqlStringCommand, "@_IsDel", DbType.Int32, model._IsDel);
			SysDatabase.AddInParameter(sqlStringCommand, "@QueryCode", DbType.String, model.QueryCode);
			SysDatabase.AddInParameter(sqlStringCommand, "@ChartTitle", DbType.String, model.ChartTitle);
			SysDatabase.AddInParameter(sqlStringCommand, "@xAxisName", DbType.String, model.xAxisName);
			SysDatabase.AddInParameter(sqlStringCommand, "@yAxisName", DbType.String, model.yAxisName);
			SysDatabase.AddInParameter(sqlStringCommand, "@xAxisTitle", DbType.String, model.xAxisTitle);
			SysDatabase.AddInParameter(sqlStringCommand, "@yAxisTitle", DbType.String, model.yAxisTitle);
			SysDatabase.AddInParameter(sqlStringCommand, "@ChartType", DbType.String, model.ChartType);
			SysDatabase.AddInParameter(sqlStringCommand, "@ChartWidth", DbType.String, model.ChartWidth);
			SysDatabase.AddInParameter(sqlStringCommand, "@ChartHeight", DbType.String, model.ChartHeight);
			SysDatabase.AddInParameter(sqlStringCommand, "@ShowValue", DbType.String, model.ShowValue);
			num = (this.dbTransaction_0 == null ? SysDatabase.ExecuteNonQuery(sqlStringCommand) : SysDatabase.ExecuteNonQuery(sqlStringCommand, this.dbTransaction_0));
			return num;
		}
	}
}