using EIS.AppBase;
using EIS.DataAccess;
using EIS.Permission.Access;
using EIS.Permission.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace EIS.Permission.Service
{
	public class PositionService
	{
		public PositionService()
		{
		}

		public static int GetEmployeeCountByPositionId(string positionId)
		{
			int num;
			string str = string.Concat("select count(*) from T_E_Org_DeptEmployee where PositionId='", positionId, "'");
			object obj = SysDatabase.ExecuteScalar(str);
			num = (obj == DBNull.Value ? 0 : Convert.ToInt32(obj));
			return num;
		}

		public static string GetJsonPostionByDeptId(string deptId)
		{
			List<Position> modelListByDeptId = (new _Position()).GetModelListByDeptId(deptId);
			ArrayList arrayLists = new ArrayList();
			foreach (Position position in modelListByDeptId)
			{
				TreeItem treeItem = new TreeItem()
				{
					id = position._AutoID,
					text = position.PositionName,
					@value = position.DeptID
				};
				arrayLists.Add(treeItem);
			}
			return (new JavaScriptSerializer()).Serialize(arrayLists);
		}

		public static List<Position> GetPositionByDeptId(string deptId)
		{
			return (new _Position()).GetModelListByDeptId(deptId);
		}

		public static Position GetPositionById(string positionId)
		{
			return (new _Position()).GetModel(positionId);
		}

		public static List<Position> GetPositionByPropId(string propId)
		{
			return (new _Position()).GetModelListByPropId(propId);
		}

		public static bool RemovePosition(string positionId)
		{
			_Position __Position = new _Position();
			__Position.GetModel(positionId);
			if ((new _DeptEmployee()).GetEmployeeCountByPositionId(positionId) > 0)
			{
				throw new Exception("该岗位下面存在员工，不能删除");
			}
			return __Position.Delete(positionId) > 0;
		}
	}
}