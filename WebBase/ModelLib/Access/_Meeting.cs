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
	public class _Meeting
	{
		private DbTransaction dbTransaction_0 = null;

		public _Meeting()
		{
		}

		public _Meeting(DbTransaction tran)
		{
			this.dbTransaction_0 = tran;
		}

		public DataTable GetList(string strWhere)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select *  FROM T_OA_HY_Apply  ");
			if (strWhere.Trim() != "")
			{
				stringBuilder.Append(string.Concat(" where ", strWhere));
			}
			stringBuilder.Append(" order by StartTime ");
			return SysDatabase.ExecuteTable(stringBuilder.ToString());
		}

		public Meeting GetModel(string string_0)
		{
			Meeting model;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select  top 1 * from T_OA_HY_Apply  ");
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

		public Meeting GetModel(DataRow dataRow_0)
		{
			Meeting meeting = new Meeting()
			{
				_AutoID = dataRow_0["_AutoID"].ToString(),
				_UserName = dataRow_0["_UserName"].ToString(),
				_OrgCode = dataRow_0["_OrgCode"].ToString()
			};
			if (dataRow_0["_CreateTime"].ToString() != "")
			{
				meeting._CreateTime = DateTime.Parse(dataRow_0["_CreateTime"].ToString());
			}
			if (dataRow_0["_UpdateTime"].ToString() != "")
			{
				meeting._UpdateTime = DateTime.Parse(dataRow_0["_UpdateTime"].ToString());
			}
			if (dataRow_0["_IsDel"].ToString() != "")
			{
				meeting._IsDel = int.Parse(dataRow_0["_IsDel"].ToString());
			}
			meeting.HyName = dataRow_0["HyName"].ToString();
			meeting.HyAddr = dataRow_0["HyAddr"].ToString();
			meeting.HyDept = dataRow_0["HyDept"].ToString();
			meeting.HyJbr = dataRow_0["HyJbr"].ToString();
			meeting.JbrTel = dataRow_0["JbrTel"].ToString();
			if (dataRow_0.Table.Columns.Contains("OfficeTel"))
			{
				string str = dataRow_0["OfficeTel"].ToString();
				if (str.Length > 0)
				{
					if (meeting.JbrTel.Length <= 0)
					{
						meeting.JbrTel = str;
					}
					else
					{
						Meeting meeting1 = meeting;
						meeting1.JbrTel = string.Concat(meeting1.JbrTel, " / ", str);
					}
				}
			}
			meeting.HyState = dataRow_0["HyState"].ToString();
			meeting._WFState = dataRow_0["_WFState"].ToString();
			if (dataRow_0["StartTime"].ToString() != "")
			{
				meeting.StartTime = DateTime.Parse(dataRow_0["StartTime"].ToString());
			}
			if (dataRow_0["EndTime"].ToString() != "")
			{
				meeting.EndTime = DateTime.Parse(dataRow_0["EndTime"].ToString());
			}
			meeting.IsAllDayEvent = 0;
			return meeting;
		}

		public List<Meeting> GetModelList(string strWhere)
		{
			StringBuilder stringBuilder = new StringBuilder();
			List<Meeting> meetings = new List<Meeting>();
			stringBuilder.Append("select *  FROM T_OA_HY_Apply  ");
			if (strWhere.Trim() != "")
			{
				stringBuilder.Append(string.Concat(" where ", strWhere));
			}
			stringBuilder.Append(" order by StartTime ");
			foreach (DataRow row in SysDatabase.ExecuteTable(stringBuilder.ToString()).Rows)
			{
				meetings.Add(this.GetModel(row));
			}
			return meetings;
		}
	}
}