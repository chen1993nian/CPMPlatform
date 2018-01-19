using EIS.DataAccess;
using EIS.WorkFlow.Access;
using EIS.WorkFlow.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace EIS.WorkFlow.Service
{
	public class CatalogService
	{
		public CatalogService()
		{
		}

		public static List<Catalog> GetAllCatalog()
		{
			return (new _Catalog()).GetModelList("");
		}

		public static List<Catalog> GetCatalogByCatCode(string catCode)
		{
			_Catalog __Catalog = new _Catalog();
			List<Catalog> modelList = __Catalog.GetModelList(string.Concat("CatalogCode like '", catCode, "%'"));
			return modelList;
		}

		public static string GetCatalogNameByCode(string catCode)
		{
			string str = string.Concat("select CatalogName from T_E_WF_Catalog where CatalogCode ='", catCode, "'");
			object obj = SysDatabase.ExecuteScalar(str);
			return ((obj == DBNull.Value ? true : obj == null) ? "" : obj.ToString());
		}

		public static int GetSonCountByCatCode(string catCode)
		{
			return (new _Catalog()).GetSonCountByCode(catCode);
		}

		public static Catalog GetTopCatalogByCompanyId(string companyId)
		{
			Catalog item;
			_Catalog __Catalog = new _Catalog();
			List<Catalog> modelList = __Catalog.GetModelList(string.Concat("orgId='", companyId, "'"));
			if (modelList.Count <= 0)
			{
				item = null;
			}
			else
			{
				item = modelList[0];
			}
			return item;
		}

		public static int GetWorkflowCountByCatCode(string catCode)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select  count(*) from T_E_WF_Define ");
			stringBuilder.Append(string.Concat(" where CatalogCode = '", catCode, "' "));
			object obj = SysDatabase.ExecuteScalar(stringBuilder.ToString());
			return Convert.ToInt32(obj);
		}
	}
}