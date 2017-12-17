using AjaxPro;
using EIS.AppBase;
using EIS.AppCommon;
using EIS.DataAccess;
using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Text;
using System.Web.UI.HtmlControls;

namespace EIS.Web.WorkAsp.Survey
{
	public partial class SurveyLeft : PageBase
	{
		protected HtmlHead Head1;

		protected HtmlForm Form1;

		private DataTable dataTable_0;

		public string treedata = "";

		public string surveyId = "";

	

		[AjaxMethod]    // JustDecompile was unable to locate the assembly where attribute parameters types are defined. Generating parameters values is impossible.
		public string AddSonNode(string nodeName, string PID)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("Insert T_OA_Survey_Question (\r\n \t\t\t\t\t_AutoID,\r\n\t\t\t\t\t_UserName,\r\n\t\t\t\t\t_OrgCode,\r\n\t\t\t\t\t_CreateTime,\r\n\t\t\t\t\t_UpdateTime,\r\n\t\t\t\t\t_IsDel,\r\n                    _CompanyId,\r\n                    SurveyId,\r\n\t\t\t\t\tQTitle,\r\n\t\t\t\t\tQSelect,\r\n\t\t\t\t\tShowOther,\r\n\t\t\t\t\tQOrder,\r\n\t\t\t\t\tQType,\r\n\t\t\t\t\tQMustSel\r\n\r\n\t\t\t    ) values(\r\n\t\t\t\t\t    @_AutoID,\r\n\t\t\t\t\t    @_UserName,\r\n\t\t\t\t\t    @_OrgCode,\r\n\t\t\t\t\t    @_CreateTime,\r\n\t\t\t\t\t    @_UpdateTime,\r\n\t\t\t\t\t    @_IsDel,\r\n                        @_CompanyId,\r\n                        @SurveyId,\r\n\t\t\t\t\t    @QTitle,\r\n\t\t\t\t\t    @QSelect,\r\n\t\t\t\t\t    @ShowOther,\r\n\t\t\t\t\t    @QOrder,\r\n\t\t\t\t\t    @QType,\r\n\t\t\t\t\t    @QMustSel\r\n\t\t\t    )");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			UserContext userInfo = base.UserInfo;
			string str = Guid.NewGuid().ToString();
			SysDatabase.AddInParameter(sqlStringCommand, "@_AutoID", DbType.String, str);
			SysDatabase.AddInParameter(sqlStringCommand, "@_UserName", DbType.String, userInfo.EmployeeId);
			SysDatabase.AddInParameter(sqlStringCommand, "@_OrgCode", DbType.String, userInfo.DeptWbs);
			SysDatabase.AddInParameter(sqlStringCommand, "@_CreateTime", DbType.DateTime, DateTime.Now);
			SysDatabase.AddInParameter(sqlStringCommand, "@_UpdateTime", DbType.DateTime, DateTime.Now);
			SysDatabase.AddInParameter(sqlStringCommand, "@_IsDel", DbType.Int32, 0);
			SysDatabase.AddInParameter(sqlStringCommand, "@_CompanyId", DbType.String, userInfo.CompanyId);
			SysDatabase.AddInParameter(sqlStringCommand, "@SurveyId", DbType.String, PID);
			SysDatabase.AddInParameter(sqlStringCommand, "@QTitle", DbType.String, nodeName);
			SysDatabase.AddInParameter(sqlStringCommand, "@QSelect", DbType.String, "");
			SysDatabase.AddInParameter(sqlStringCommand, "@ShowOther", DbType.String, "否");
			SysDatabase.AddInParameter(sqlStringCommand, "@QType", DbType.String, "单选");
			SysDatabase.AddInParameter(sqlStringCommand, "@QMustSel", DbType.String, "0");
			SysDatabase.AddInParameter(sqlStringCommand, "@QOrder", DbType.Int32, this.method_0(PID));
			SysDatabase.ExecuteNonQuery(sqlStringCommand);
			return str;
		}

		private int method_0(string string_0)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(" select max(QOrder) ");
			stringBuilder.Append(" From T_OA_Survey_Question ");
			stringBuilder.Append(string.Concat(" where SurveyId='", string_0, "'"));
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			object obj = SysDatabase.ExecuteScalar(sqlStringCommand);
			return (obj == DBNull.Value ? 1 : Convert.ToInt32(obj) + 1);
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			AjaxPro.Utility.RegisterTypeForAjax(typeof(SurveyLeft));
			this.surveyId = base.GetParaValue("mainId");
			string str = string.Concat("select * from T_OA_Survey_Info where _AutoId='", this.surveyId, "'");
			this.dataTable_0 = SysDatabase.ExecuteTable(str);
			if (this.dataTable_0.Rows.Count > 0)
			{
				zTreeNode _zTreeNode = new zTreeNode()
				{
					id = "",
					name = this.dataTable_0.Rows[0]["SurTitle"].ToString(),
					icon = "../../img/common/home.png",
					@value = this.surveyId,
					open = true
				};
				str = string.Concat("select * from T_OA_Survey_Question where SurveyId='", this.surveyId, "'");
				this.dataTable_0 = SysDatabase.ExecuteTable(str);
				foreach (DataRow row in this.dataTable_0.Rows)
				{
					zTreeNode _zTreeNode1 = new zTreeNode()
					{
						id = row["_AutoID"].ToString(),
						name = string.Concat(row["QOrder"].ToString(), ".", row["QTitle"].ToString()),
						@value = ""
					};
					_zTreeNode.Add(_zTreeNode1);
				}
				this.treedata = _zTreeNode.ToJsonString(true);
			}
		}

		[AjaxMethod]    // JustDecompile was unable to locate the assembly where attribute parameters types are defined. Generating parameters values is impossible.
		public int RemoveNode(string nodeId)
		{
			return SysDatabase.ExecuteNonQuery(string.Format("delete T_OA_Survey_Question where _autoId='{0}'", nodeId));
		}
	}
}