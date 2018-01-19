using EIS.AppBase;
using EIS.DataAccess;
using EIS.Web.ModelLib.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;

namespace EIS.Web.ModelLib.Access
{
	public class _Calendar
	{
		private DbTransaction dbTransaction_0 = null;

		public _Calendar()
		{
		}

		public _Calendar(DbTransaction tran)
		{
			this.dbTransaction_0 = tran;
		}

		public int Add(Calendar model)
		{
			int num;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("Insert T_OA_RC_Calendar  (\r\n \t\t\t\t\t_AutoID,\r\n\t\t\t\t\t_UserName,\r\n\t\t\t\t\t_OrgCode,\r\n\t\t\t\t\t_CreateTime,\r\n\t\t\t\t\t_UpdateTime,\r\n\t\t\t\t\t_IsDel,\r\n\t\t\t\t\tSubject,\r\n\t\t\t\t\tLocation,\r\n\t\t\t\t\tDescription,\r\n\t\t\t\t\tStartTime,\r\n                    EndTime,\r\n                    IsAllDayEvent,\r\n                    Category,\r\n                    CategoryName,\r\n                    CalendarType,\r\n                    EmpId,\r\n                    EmpName\r\n\r\n\t\t\t) values(\r\n\t\t\t\t\t@_AutoID,\r\n\t\t\t\t\t@_UserName,\r\n\t\t\t\t\t@_OrgCode,\r\n\t\t\t\t\t@_CreateTime,\r\n\t\t\t\t\t@_UpdateTime,\r\n\t\t\t\t\t@_IsDel,\r\n\t\t\t\t\t@Subject,\r\n\t\t\t\t\t@Location,\r\n\t\t\t\t\t@Description,\r\n\t\t\t\t\t@StartTime,\r\n                    @EndTime,\r\n                    @IsAllDayEvent,\r\n                    @Category,\r\n                    @CategoryName,\r\n                    @CalendarType,\r\n                    @EmpId,\r\n                    @EmpName\r\n\t\t\t)");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "@_AutoID", DbType.String, model._AutoID);
			SysDatabase.AddInParameter(sqlStringCommand, "@_UserName", DbType.String, model._UserName);
			SysDatabase.AddInParameter(sqlStringCommand, "@_OrgCode", DbType.String, model._OrgCode);
			SysDatabase.AddInParameter(sqlStringCommand, "@_CreateTime", DbType.DateTime, model._CreateTime);
			SysDatabase.AddInParameter(sqlStringCommand, "@_UpdateTime", DbType.DateTime, model._UpdateTime);
			SysDatabase.AddInParameter(sqlStringCommand, "@_IsDel", DbType.Int32, model._IsDel);
			SysDatabase.AddInParameter(sqlStringCommand, "@Subject", DbType.String, model.Subject);
			SysDatabase.AddInParameter(sqlStringCommand, "@Location", DbType.String, model.Location);
			SysDatabase.AddInParameter(sqlStringCommand, "@Description", DbType.String, model.Description);
			SysDatabase.AddInParameter(sqlStringCommand, "@StartTime", DbType.DateTime, model.StartTime);
			SysDatabase.AddInParameter(sqlStringCommand, "@EndTime", DbType.DateTime, model.EndTime);
			SysDatabase.AddInParameter(sqlStringCommand, "@IsAllDayEvent", DbType.Int32, model.IsAllDayEvent);
			SysDatabase.AddInParameter(sqlStringCommand, "@Category", DbType.String, model.Category);
			SysDatabase.AddInParameter(sqlStringCommand, "@CategoryName", DbType.String, model.CategoryName);
			SysDatabase.AddInParameter(sqlStringCommand, "@CalendarType", DbType.Int32, model.CalendarType);
			SysDatabase.AddInParameter(sqlStringCommand, "@EmpId", DbType.String, model.EmpId);
			SysDatabase.AddInParameter(sqlStringCommand, "@EmpName", DbType.String, model.EmpName);
			num = (this.dbTransaction_0 == null ? SysDatabase.ExecuteNonQuery(sqlStringCommand) : SysDatabase.ExecuteNonQuery(sqlStringCommand, this.dbTransaction_0));
			return num;
		}

		public int Delete(string string_0)
		{
			int num;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("delete from T_OA_RC_Calendar  ");
			stringBuilder.Append(" where _AutoID=@_AutoID ;");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "@_AutoID", DbType.String, string_0);
			num = (this.dbTransaction_0 == null ? SysDatabase.ExecuteNonQuery(sqlStringCommand) : SysDatabase.ExecuteNonQuery(sqlStringCommand, this.dbTransaction_0));
			return num;
		}

		public DataTable GetList(string strWhere)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select *  From T_OA_RC_Calendar  ");
			if (strWhere.Trim() != "")
			{
				stringBuilder.Append(string.Concat(" where ", strWhere));
			}
			stringBuilder.Append(" order by StartTime ");
			return SysDatabase.ExecuteTable(stringBuilder.ToString());
		}

		public Calendar GetModel(string string_0)
		{
			Calendar model;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select  top 1 * from T_OA_RC_Calendar  ");
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

		public Calendar GetModel(DataRow dataRow_0)
		{
			Calendar calendar = new Calendar()
			{
				_AutoID = dataRow_0["_AutoID"].ToString(),
				_UserName = dataRow_0["_UserName"].ToString(),
				_OrgCode = dataRow_0["_OrgCode"].ToString()
			};
			if (dataRow_0["_CreateTime"].ToString() != "")
			{
				calendar._CreateTime = DateTime.Parse(dataRow_0["_CreateTime"].ToString());
			}
			if (dataRow_0["_UpdateTime"].ToString() != "")
			{
				calendar._UpdateTime = DateTime.Parse(dataRow_0["_UpdateTime"].ToString());
			}
			if (dataRow_0["_IsDel"].ToString() != "")
			{
				calendar._IsDel = int.Parse(dataRow_0["_IsDel"].ToString());
			}
			calendar.Subject = dataRow_0["Subject"].ToString();
			calendar.Location = dataRow_0["Location"].ToString();
			calendar.Description = dataRow_0["Description"].ToString();
			calendar.Category = dataRow_0["Category"].ToString();
			calendar.CategoryName = dataRow_0["CategoryName"].ToString();
			calendar.EmpId = dataRow_0["EmpId"].ToString();
			calendar.EmpName = dataRow_0["EmpName"].ToString();
			if (dataRow_0["StartTime"].ToString() != "")
			{
				calendar.StartTime = DateTime.Parse(dataRow_0["StartTime"].ToString());
			}
			if (dataRow_0["EndTime"].ToString() != "")
			{
				calendar.EndTime = DateTime.Parse(dataRow_0["EndTime"].ToString());
			}
			if (dataRow_0["IsAllDayEvent"].ToString() != "")
			{
				calendar.IsAllDayEvent = int.Parse(dataRow_0["IsAllDayEvent"].ToString());
			}
			if (dataRow_0["CalendarType"].ToString() != "")
			{
				calendar.CalendarType = int.Parse(dataRow_0["CalendarType"].ToString());
			}
			return calendar;
		}

		public List<Calendar> GetModelList(string strWhere)
		{
			StringBuilder stringBuilder = new StringBuilder();
			List<Calendar> calendars = new List<Calendar>();
			stringBuilder.Append("select *  From T_OA_RC_Calendar  ");
			if (strWhere.Trim() != "")
			{
				stringBuilder.Append(string.Concat(" where ", strWhere));
			}
			stringBuilder.Append(" order by StartTime ");
			foreach (DataRow row in SysDatabase.ExecuteTable(stringBuilder.ToString()).Rows)
			{
				calendars.Add(this.GetModel(row));
			}
			return calendars;
		}

		public int Update(Calendar model)
		{
			int num;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("update T_OA_RC_Calendar  set \r\n\t\t\t\t\t_UpdateTime=@_UpdateTime,\r\n\t\t\t\t\t_IsDel=@_IsDel,\r\n\r\n\t\t\t\t\tSubject = @Subject,\r\n\t\t\t\t\tLocation = @Location,\r\n\t\t\t\t\tDescription = @Description,\r\n\t\t\t\t\tStartTime = @StartTime,\r\n                    EndTime = @EndTime,\r\n                    IsAllDayEvent = @IsAllDayEvent,\r\n                    Category = @Category,\r\n                    CategoryName = @CategoryName,\r\n                    CalendarType = @CalendarType,\r\n                    EmpId = @EmpId,\r\n                    EmpName = @EmpName\r\n\r\n\t\t\t\t\twhere _AutoID=@_AutoID\r\n\t\t\t ");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "@_AutoID", DbType.String, model._AutoID);
			SysDatabase.AddInParameter(sqlStringCommand, "@_UpdateTime", DbType.DateTime, model._UpdateTime);
			SysDatabase.AddInParameter(sqlStringCommand, "@_IsDel", DbType.Int32, model._IsDel);
			SysDatabase.AddInParameter(sqlStringCommand, "@Subject", DbType.String, model.Subject);
			SysDatabase.AddInParameter(sqlStringCommand, "@Location", DbType.String, model.Location);
			SysDatabase.AddInParameter(sqlStringCommand, "@Description", DbType.String, model.Description);
			SysDatabase.AddInParameter(sqlStringCommand, "@StartTime", DbType.DateTime, model.StartTime);
			SysDatabase.AddInParameter(sqlStringCommand, "@EndTime", DbType.DateTime, model.EndTime);
			SysDatabase.AddInParameter(sqlStringCommand, "@IsAllDayEvent", DbType.Int32, model.IsAllDayEvent);
			SysDatabase.AddInParameter(sqlStringCommand, "@Category", DbType.String, model.Category);
			SysDatabase.AddInParameter(sqlStringCommand, "@CategoryName", DbType.String, model.CategoryName);
			SysDatabase.AddInParameter(sqlStringCommand, "@CalendarType", DbType.Int32, model.CalendarType);
			SysDatabase.AddInParameter(sqlStringCommand, "@EmpId", DbType.String, model.EmpId);
			SysDatabase.AddInParameter(sqlStringCommand, "@EmpName", DbType.String, model.EmpName);
			num = (this.dbTransaction_0 == null ? SysDatabase.ExecuteNonQuery(sqlStringCommand) : SysDatabase.ExecuteNonQuery(sqlStringCommand, this.dbTransaction_0));
			return num;
		}
	}
}