using EIS.DataModel.Access;
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Data;
using System.Data.Common;

namespace EIS.DataModel.Service
{
	public class EmployeeRelationService
	{
		public EmployeeRelationService()
		{
		}

		public static StringCollection GetAllMyLeader(string employeeId)
		{
			StringCollection stringCollections = new StringCollection();
			_EmployeeRelation __EmployeeRelation = new _EmployeeRelation();
			DataTable list = __EmployeeRelation.GetList(string.Concat("EmployeeId='", employeeId, "' and (relation='0' or relation='1') and _IsDel=0"));
			foreach (DataRow row in list.Rows)
			{
				stringCollections.Add(string.Concat(row["LeaderId"], "|", row["LeaderName"]));
			}
			return stringCollections;
		}

		public static StringCollection GetMyColleague(string employeeId, DbTransaction dbTran)
		{
			StringCollection stringCollections = new StringCollection();
			_EmployeeRelation __EmployeeRelation = new _EmployeeRelation(dbTran);
			DataTable list = __EmployeeRelation.GetList(string.Concat("EmployeeId='", employeeId, "' and (relation='2') and _IsDel=0"));
			foreach (DataRow row in list.Rows)
			{
				stringCollections.Add(string.Concat(row["LeaderId"], "|", row["LeaderName"]));
			}
			return stringCollections;
		}

		public static StringCollection GetMyHelper(string employeeId, DbTransaction dbTran)
		{
			StringCollection stringCollections = new StringCollection();
			_EmployeeRelation __EmployeeRelation = new _EmployeeRelation(dbTran);
			DataTable list = __EmployeeRelation.GetList(string.Concat("LeaderId='", employeeId, "' and (relation='1') and _IsDel=0"));
			foreach (DataRow row in list.Rows)
			{
				stringCollections.Add(string.Concat(row["EmployeeId"], "|", row["EmployeeName"]));
			}
			return stringCollections;
		}

		public static StringCollection GetMyLeader(string employeeId)
		{
			return EmployeeRelationService.GetMyLeader(employeeId, null);
		}

		public static StringCollection GetMyLeader(string employeeId, DbTransaction dbTran)
		{
			StringCollection stringCollections = new StringCollection();
			_EmployeeRelation __EmployeeRelation = new _EmployeeRelation(dbTran);
			DataTable list = __EmployeeRelation.GetList(string.Concat("EmployeeId='", employeeId, "' and (relation='0') and _IsDel=0"));
			foreach (DataRow row in list.Rows)
			{
				stringCollections.Add(string.Concat(row["LeaderId"], "|", row["LeaderName"]));
			}
			return stringCollections;
		}

		public static StringCollection GetMyLeader2(string employeeId)
		{
			StringCollection stringCollections = new StringCollection();
			_EmployeeRelation __EmployeeRelation = new _EmployeeRelation();
			DataTable list = __EmployeeRelation.GetList(string.Concat("EmployeeId='", employeeId, "' and (relation='1') and _IsDel=0"));
			foreach (DataRow row in list.Rows)
			{
				stringCollections.Add(string.Concat(row["LeaderId"], "|", row["LeaderName"]));
			}
			return stringCollections;
		}

		public static StringCollection GetMyUnderling(string employeeId, DbTransaction dbTran)
		{
			StringCollection stringCollections = new StringCollection();
			_EmployeeRelation __EmployeeRelation = new _EmployeeRelation(dbTran);
			DataTable list = __EmployeeRelation.GetList(string.Concat("LeaderId='", employeeId, "' and (relation='0') and _IsDel=0"));
			foreach (DataRow row in list.Rows)
			{
				stringCollections.Add(string.Concat(row["EmployeeId"], "|", row["EmployeeName"]));
			}
			return stringCollections;
		}
	}
}