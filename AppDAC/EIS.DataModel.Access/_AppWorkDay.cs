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
	public class _AppWorkDay
	{
		private DbTransaction dbTransaction_0 = null;

		public _AppWorkDay()
		{
		}

		public _AppWorkDay(DbTransaction tran)
		{
			this.dbTransaction_0 = tran;
		}

		public int Add(AppWorkDay model)
		{
			int num;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("Insert T_E_App_WorkDay (\r\n \t\t\t\t\t_AutoID,\r\n\t\t\t\t\t_UserName,\r\n\t\t\t\t\t_OrgCode,\r\n\t\t\t\t\t_CreateTime,\r\n\t\t\t\t\t_UpdateTime,\r\n\t\t\t\t\t_IsDel,\r\n\t\t\t\t\tCompanyId,\r\n\t\t\t\t\tWorkDate,\r\n\t\t\t\t\tIsWorkDay,\r\n\t\t\t\t\tDayType,\r\n\t\t\t\t\tHoliday,\r\n\t\t\t\t\tStartTime,\r\n\t\t\t\t\tEndTime\r\n\t\t\t) values(\r\n\t\t\t\t\t@_AutoID,\r\n\t\t\t\t\t@_UserName,\r\n\t\t\t\t\t@_OrgCode,\r\n\t\t\t\t\t@_CreateTime,\r\n\t\t\t\t\t@_UpdateTime,\r\n\t\t\t\t\t@_IsDel,\r\n\t\t\t\t\t@CompanyId,\r\n\t\t\t\t\t@WorkDate,\r\n\t\t\t\t\t@IsWorkDay,\r\n\t\t\t\t\t@DayType,\r\n\t\t\t\t\t@Holiday,\r\n\t\t\t\t\t@StartTime,\r\n\t\t\t\t\t@EndTime\r\n\t\t\t)");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "@_AutoID", DbType.String, model._AutoID);
			SysDatabase.AddInParameter(sqlStringCommand, "@_UserName", DbType.String, model._UserName);
			SysDatabase.AddInParameter(sqlStringCommand, "@_OrgCode", DbType.String, model._OrgCode);
			SysDatabase.AddInParameter(sqlStringCommand, "@_CreateTime", DbType.DateTime, model._CreateTime);
			SysDatabase.AddInParameter(sqlStringCommand, "@_UpdateTime", DbType.DateTime, model._UpdateTime);
			SysDatabase.AddInParameter(sqlStringCommand, "@_IsDel", DbType.Int32, model._IsDel);
			SysDatabase.AddInParameter(sqlStringCommand, "@CompanyId", DbType.String, model.CompanyId);
			SysDatabase.AddInParameter(sqlStringCommand, "@WorkDate", DbType.DateTime, model.WorkDate);
			SysDatabase.AddInParameter(sqlStringCommand, "@IsWorkDay", DbType.String, model.IsWorkDay);
			SysDatabase.AddInParameter(sqlStringCommand, "@DayType", DbType.String, model.DayType);
			SysDatabase.AddInParameter(sqlStringCommand, "@Holiday", DbType.String, model.Holiday);
			SysDatabase.AddInParameter(sqlStringCommand, "@StartTime", DbType.DateTime, model.StartTime);
			SysDatabase.AddInParameter(sqlStringCommand, "@EndTime", DbType.DateTime, model.EndTime);
			num = (this.dbTransaction_0 == null ? SysDatabase.ExecuteNonQuery(sqlStringCommand) : SysDatabase.ExecuteNonQuery(sqlStringCommand, this.dbTransaction_0));
			return num;
		}

		public int Delete(string msgId)
		{
			int num;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("update T_E_App_WorkDay set \r\n\t\t\t\t\t_IsDel=@_IsDel\r\n\t\t\t\t\twhere _AutoID=@_AutoID\r\n\t\t\t ");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "@_AutoID", DbType.String, msgId);
			SysDatabase.AddInParameter(sqlStringCommand, "@_IsDel", DbType.Int32, 1);
			num = (this.dbTransaction_0 == null ? SysDatabase.ExecuteNonQuery(sqlStringCommand) : SysDatabase.ExecuteNonQuery(sqlStringCommand, this.dbTransaction_0));
			return num;
		}

		public DataTable GetList(string strWhere)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select *  FROM T_E_App_WorkDay ");
			if (strWhere.Trim() != "")
			{
				stringBuilder.Append(string.Concat(" where ", strWhere));
			}
			return SysDatabase.ExecuteTable(stringBuilder.ToString());
		}

		public AppWorkDay GetModel(string string_0)
		{
			AppWorkDay model;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select  top 1 * from T_E_App_WorkDay ");
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

		public AppWorkDay GetModel(DataRow dataRow_0)
		{
			AppWorkDay appWorkDay = new AppWorkDay()
			{
				_AutoID = dataRow_0["_AutoID"].ToString(),
				_UserName = dataRow_0["_UserName"].ToString(),
				_OrgCode = dataRow_0["_OrgCode"].ToString()
			};
			if (dataRow_0["_CreateTime"].ToString() != "")
			{
				appWorkDay._CreateTime = DateTime.Parse(dataRow_0["_CreateTime"].ToString());
			}
			if (dataRow_0["_UpdateTime"].ToString() != "")
			{
				appWorkDay._UpdateTime = DateTime.Parse(dataRow_0["_UpdateTime"].ToString());
			}
			if (dataRow_0["_IsDel"].ToString() != "")
			{
				appWorkDay._IsDel = int.Parse(dataRow_0["_IsDel"].ToString());
			}
			appWorkDay.CompanyId = dataRow_0["CompanyId"].ToString();
			appWorkDay.IsWorkDay = dataRow_0["IsWorkDay"].ToString();
			appWorkDay.Holiday = dataRow_0["Holiday"].ToString();
			appWorkDay.DayType = dataRow_0["DayType"].ToString();
			if (dataRow_0["WorkDate"].ToString() != "")
			{
				appWorkDay.WorkDate = DateTime.Parse(dataRow_0["WorkDate"].ToString());
			}
			if (dataRow_0["StartTime"].ToString() != "")
			{
				appWorkDay.StartTime = DateTime.Parse(dataRow_0["StartTime"].ToString());
			}
			if (dataRow_0["EndTime"].ToString() != "")
			{
				appWorkDay.EndTime = DateTime.Parse(dataRow_0["EndTime"].ToString());
			}
			return appWorkDay;
		}

		public List<AppWorkDay> GetModelList(string strWhere)
		{
			StringBuilder stringBuilder = new StringBuilder();
			List<AppWorkDay> appWorkDays = new List<AppWorkDay>();
			stringBuilder.Append("select *  From T_E_App_WorkDay ");
			if (strWhere.Trim() != "")
			{
				stringBuilder.Append(string.Concat(" where ", strWhere));
			}
			foreach (DataRow row in SysDatabase.ExecuteTable(stringBuilder.ToString()).Rows)
			{
				appWorkDays.Add(this.GetModel(row));
			}
			return appWorkDays;
		}

		public List<AppWorkDay> GetModelsByDate(DateTime date)
		{
			return this.GetModelsByDate(date, "");
		}

		public List<AppWorkDay> GetModelsByDate(DateTime date, string calendarId)
		{
			List<AppWorkDay> appWorkDays = new List<AppWorkDay>();
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select * from T_E_App_WorkDay ");
			stringBuilder.Append(" where year(WorkDate)=year(@WorkDate) and month(WorkDate)=month(@WorkDate) and day(WorkDate)=day(@WorkDate) and isnull(CompanyId,'')=@calendarId order by startTime");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "@WorkDate", DbType.Date, date);
			SysDatabase.AddInParameter(sqlStringCommand, "@calendarId", DbType.String, calendarId);
			foreach (DataRow row in SysDatabase.ExecuteTable(sqlStringCommand).Rows)
			{
				appWorkDays.Add(this.GetModel(row));
			}
			return appWorkDays;
		}

		public AppWorkDay GetNextWorkDay(DateTime startTime, string calendarId)
		{
			AppWorkDay model;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select top 1 * from T_E_App_WorkDay where IsWorkDay='是' and isnull(CompanyId,'')=@calendarId and StartTime>=@startTime order by StartTime");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "@startTime", DbType.DateTime, startTime);
			SysDatabase.AddInParameter(sqlStringCommand, "@calendarId", DbType.String, calendarId);
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

		public AppWorkDay GetWorkDayByTime(DateTime time, string calendarId)
		{
			AppWorkDay model;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select top 1 * from T_E_App_WorkDay ");
			stringBuilder.Append(" where @WorkDate between StartTime and EndTime and IsWorkDay='是' and isnull(CompanyId,'')=@calendarId order by startTime");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "@WorkDate", DbType.Date, time);
			SysDatabase.AddInParameter(sqlStringCommand, "@calendarId", DbType.String, calendarId);
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

		public int Remove(string string_0)
		{
			int num;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("delete from T_E_App_WorkDay ");
			stringBuilder.Append(" where _AutoID=@_AutoID ;");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "@_AutoID", DbType.String, string_0);
			num = (this.dbTransaction_0 == null ? SysDatabase.ExecuteNonQuery(sqlStringCommand) : SysDatabase.ExecuteNonQuery(sqlStringCommand, this.dbTransaction_0));
			return num;
		}

		public int RemoveByCondition(string condition)
		{
			int num;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("delete from T_E_App_WorkDay ");
			stringBuilder.AppendFormat(" where {0}", condition);
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			num = (this.dbTransaction_0 == null ? SysDatabase.ExecuteNonQuery(sqlStringCommand) : SysDatabase.ExecuteNonQuery(sqlStringCommand, this.dbTransaction_0));
			return num;
		}

		public int Update(AppWorkDay model)
		{
			int num;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("update T_E_App_WorkDay set \r\n\t\t\t\t\t_UpdateTime=@_UpdateTime,\r\n\t\t\t\t\t_IsDel=@_IsDel,\r\n\r\n\t\t\t\t\tCompanyId=@CompanyId,\r\n\t\t\t\t\tWorkDate=@WorkDate,\r\n\t\t\t\t\tIsWorkDay=@IsWorkDay,\r\n\t\t\t\t\tDayType=@DayType,\r\n\t\t\t\t\tHoliday=@Holiday,\r\n\t\t\t\t\tStartTime=@StartTime,\r\n\t\t\t\t\tEndTime=@EndTime\r\n\r\n\t\t\t\t\twhere _AutoID=@_AutoID\r\n\t\t\t ");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "@_AutoID", DbType.String, model._AutoID);
			SysDatabase.AddInParameter(sqlStringCommand, "@_UpdateTime", DbType.DateTime, model._UpdateTime);
			SysDatabase.AddInParameter(sqlStringCommand, "@_IsDel", DbType.Int32, model._IsDel);
			SysDatabase.AddInParameter(sqlStringCommand, "@CompanyId", DbType.String, model.CompanyId);
			SysDatabase.AddInParameter(sqlStringCommand, "@WorkDate", DbType.DateTime, model.WorkDate);
			SysDatabase.AddInParameter(sqlStringCommand, "@IsWorkDay", DbType.String, model.IsWorkDay);
			SysDatabase.AddInParameter(sqlStringCommand, "@DayType", DbType.String, model.DayType);
			SysDatabase.AddInParameter(sqlStringCommand, "@Holiday", DbType.String, model.Holiday);
			SysDatabase.AddInParameter(sqlStringCommand, "@StartTime", DbType.DateTime, model.StartTime);
			SysDatabase.AddInParameter(sqlStringCommand, "@EndTime", DbType.DateTime, model.EndTime);
			num = (this.dbTransaction_0 == null ? SysDatabase.ExecuteNonQuery(sqlStringCommand) : SysDatabase.ExecuteNonQuery(sqlStringCommand, this.dbTransaction_0));
			return num;
		}
	}
}