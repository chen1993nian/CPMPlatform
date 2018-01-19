using EIS.DataAccess;
using EIS.WorkFlow.Access;
using EIS.WorkFlow.Model;
using System;
using System.Collections.Generic;
using System.Data.Common;

namespace EIS.WorkFlow.Service
{
	public class DefineService
	{
		public DefineService()
		{
		}

		public static List<Define> GetDefineListByCompanyId(string appName, string companyId)
		{
			_Define __Define = new _Define();
			string[] strArrays = new string[] { "Enabled='是' and AppNames='", appName, "' and (CompanyId='' or  CompanyId='", companyId, "')" };
			return __Define.GetModelList(string.Concat(strArrays), false);
		}

		public static string GetPrintStyleById(string workflowId)
		{
			string str = string.Concat("select isnull(PrintStyle,'') from T_E_WF_Define where _AutoId='", workflowId, "'");
			object obj = SysDatabase.ExecuteScalar(str);
			return ((obj == null ? false : obj != DBNull.Value) ? obj.ToString() : "");
		}

		public static Define GetWorkflowBasicModelById(string workflowId)
		{
			Define item;
			_Define __Define = new _Define();
			List<Define> modelList = __Define.GetModelList(string.Concat("_AutoID='", workflowId, "'"), false);
			if (modelList.Count > 0)
			{
				item = modelList[0];
			}
			else
			{
				item = null;
			}
			return item;
		}

		public static string GetWorkflowBizType(string workflowId, DbTransaction dbTran)
		{
			string str = string.Concat("select IsNull(BizType,'') from T_E_WF_Define where _autoId='", workflowId, "'");
			return SysDatabase.ExecuteScalar(str, dbTran).ToString();
		}

		public static Define GetWorkflowByCode(string workflowCode)
		{
			Define model;
			string str = string.Concat("select top 1 _AutoID from T_E_WF_Define where Enabled = '是' and WorkflowCode='", workflowCode, "'");
			object obj = SysDatabase.ExecuteScalar(str);
			if ((obj == null ? false : obj != DBNull.Value))
			{
				model = (new _Define()).GetModel(obj.ToString());
			}
			else
			{
				model = null;
			}
			return model;
		}

		public static string GetWorkflowCode(string workflowId)
		{
			string str = string.Concat("select WorkflowCode from T_E_WF_Define where _autoId='", workflowId, "'");
			return SysDatabase.ExecuteScalar(str).ToString();
		}

		public static List<Define> GetWorkflowDefineModeByCatCode(string catCode)
		{
			return (new _Define()).GetModelListByCatCode(catCode);
		}

		public static List<Define> GetWorkflowDefineModeByCatId(string catId)
		{
			_Define __Define = new _Define();
			Catalog model = (new _Catalog()).GetModel(catId);
			return __Define.GetModelListByCatCode(model.CatalogCode);
		}

		public static Define GetWorkflowDefineModelById(string workflowId)
		{
			_Define __Define = new _Define();
			Define define = new Define();
			return __Define.GetModel(workflowId);
		}

		public static string GetWorkflowName(string workflowId)
		{
			string str = string.Concat("select WorkflowName from T_E_WF_Define where _autoId='", workflowId, "'");
			return SysDatabase.ExecuteScalar(str).ToString();
		}
	}
}