using EIS.AppBase;
using EIS.DataAccess;
using EIS.Permission.Access;
using EIS.Permission.Model;
using EIS.Permission.Service;
using EIS.WorkFlow.Access;
using EIS.WorkFlow.Model;
using EIS.WorkFlow.XPDLParser.Elements;
using NLog;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Runtime.CompilerServices;

namespace EIS.WorkFlow.Engine
{
	public class TaskPerformer2
	{
		private static Logger fileLogger;

		public Instance ins
		{
			get;
			set;
		}

		public UserContext uc
		{
			get;
			set;
		}

		static TaskPerformer2()
		{
			TaskPerformer2.fileLogger = LogManager.GetCurrentClassLogger();
		}

		public TaskPerformer2()
		{
		}

		public List<DeptEmployee> FromPositions(string userDefine)
		{
			List<DeptEmployee> deptEmployees;
			List<DeptEmployee> deptEmployees1 = new List<DeptEmployee>();
			char[] chrArray = new char[] { '|' };
			string[] strArrays = userDefine.Split(chrArray);
			if (strArrays[2].Length != 0)
			{
				string str = strArrays[2];
				chrArray = new char[] { ',' };
				string[] strArrays1 = str.Split(chrArray);
				for (int i = 0; i < (int)strArrays1.Length; i++)
				{
					foreach (DeptEmployee deptEmployeeByPositionId in DeptEmployeeService.GetDeptEmployeeByPositionId(strArrays1[i]))
					{
						deptEmployees1.Add(deptEmployeeByPositionId);
					}
				}
				deptEmployees = deptEmployees1;
			}
			else
			{
				deptEmployees = deptEmployees1;
			}
			return deptEmployees;
		}

		public List<DeptEmployee> FromRoles(string userDefine)
		{
			List<DeptEmployee> deptEmployees;
			List<DeptEmployee> deptEmployees1 = new List<DeptEmployee>();
			char[] chrArray = new char[] { '|' };
			string[] strArrays = userDefine.Split(chrArray);
			if (strArrays[2].Length != 0)
			{
				string str = strArrays[2];
				chrArray = new char[] { ',' };
				string[] strArrays1 = str.Split(chrArray);
				for (int i = 0; i < (int)strArrays1.Length; i++)
				{
					string str1 = strArrays1[i];
					foreach (RoleEmployee roleEmployeeList in RoleEmployeeService.GetRoleEmployeeList(str1))
					{
						DeptEmployee orignalDeptEmployee = DeptEmployeeService.GetOrignalDeptEmployee(roleEmployeeList.EmployeeID);
						if (orignalDeptEmployee != null)
						{
							deptEmployees1.Add(orignalDeptEmployee);
						}
					}
					foreach (Position positionByPropId in PositionService.GetPositionByPropId(str1))
					{
						foreach (DeptEmployee deptEmployeeByPositionId in DeptEmployeeService.GetDeptEmployeeByPositionId(positionByPropId._AutoID))
						{
							deptEmployees1.Add(deptEmployeeByPositionId);
						}
					}
				}
				deptEmployees = deptEmployees1;
			}
			else
			{
				deptEmployees = deptEmployees1;
			}
			return deptEmployees;
		}

		public List<DeptEmployee> GetEmployeeByPositionProp(string userDefine)
		{
			_Position __Position;
			List<Position> modelListByPropNameInDept;
			Position position = null;
			DeptEmployee deptEmployeeByPositionId = null;
			List<DeptEmployee> deptEmployees;
			List<DeptEmployee> deptEmployees1 = new List<DeptEmployee>();
			_Role __Role = new _Role();
			string[] strArrays = userDefine.Split(new char[] { '|' });
			if (strArrays[2].Length != 0)
			{
				Role model = __Role.GetModel(strArrays[2]);
				if (model != null)
				{
					if (model.SearchScope == "部门")
					{
						__Position = new _Position();
						modelListByPropNameInDept = __Position.GetModelListByPropNameInDept(this.ins.DeptId, model._AutoID);
						foreach (Position position1 in modelListByPropNameInDept)
						{
							foreach (DeptEmployee deptEmployee in DeptEmployeeService.GetDeptEmployeeByPositionId(position1._AutoID))
							{
								deptEmployees1.Add(deptEmployee);
							}
						}
					}
                    else if ((model.SearchScope == "子公司") || (model.SearchScope == "分公司") || (model.SearchScope == "公司"))
					{
						__Position = new _Position();
						modelListByPropNameInDept = __Position.GetModelListByPropNameInCompany(this.ins.CompanyId, model._AutoID);
						foreach (Position position2 in modelListByPropNameInDept)
						{
							foreach (DeptEmployee deptEmployeeByPositionId1 in DeptEmployeeService.GetDeptEmployeeByPositionId(position2._AutoID))
							{
								deptEmployees1.Add(deptEmployeeByPositionId1);
							}
						}
					}
					else if (model.SearchScope == "事业部")
					{
                        Department parentOrgByDeptId = DepartmentService.GetParentOrgByDeptId(this.ins.DeptId, "事业部");
                        if (parentOrgByDeptId == null)
                        {
                            deptEmployees = deptEmployees1;
                            return deptEmployees;
                        }
                        __Position = new _Position();
                        modelListByPropNameInDept = __Position.GetModelListByPropNameInOrg(parentOrgByDeptId.DeptWBS, model._AutoID);
                        foreach (Position positionA in modelListByPropNameInDept)
                        {
                            foreach (DeptEmployee deptEmployeeByPositionIdA in DeptEmployeeService.GetDeptEmployeeByPositionId(positionA._AutoID))
                            {
                                deptEmployees1.Add(deptEmployeeByPositionIdA);
                            }
                        }
					}
					else
					{
                        deptEmployees = deptEmployees1;
                        return deptEmployees;
					}
					deptEmployees = deptEmployees1;
				}
				else
				{
					deptEmployees = deptEmployees1;
				}
			}
			else
			{
				deptEmployees = deptEmployees1;
			}
			return deptEmployees;
		}

		public DeptEmployee GetInitiator(DbTransaction dbTran)
		{
			DeptEmployee deptEmployee;
			DeptEmployee orignalDeptEmployee = DeptEmployeeService.GetDeptEmployee(this.ins._UserName, this.ins.DeptId);
			if (orignalDeptEmployee == null)
			{
				orignalDeptEmployee = DeptEmployeeService.GetOrignalDeptEmployee(this.ins._UserName);
				if (orignalDeptEmployee == null)
				{
					_UserTask __UserTask = new _UserTask(dbTran);
					UserTask startUserTask = __UserTask.GetStartUserTask(this.ins.InstanceId);
					List<DeptEmployee> deptEmployeeByPositionId = DeptEmployeeService.GetDeptEmployeeByPositionId(startUserTask.PositionId);
					if (deptEmployeeByPositionId.Count > 0)
					{
						orignalDeptEmployee = deptEmployeeByPositionId[0];
					}
				}
				deptEmployee = orignalDeptEmployee;
			}
			else
			{
				deptEmployee = orignalDeptEmployee;
			}
			return deptEmployee;
		}

		public List<DeptEmployee> GetPositionLeader(string userDefine, Activity actObj, DbTransaction dbTran)
		{
			List<DeptEmployee> deptEmployees;
			List<DeptEmployee> deptEmployees1 = new List<DeptEmployee>();
			string[] strArrays = userDefine.Split(new char[] { '|' });
			if (strArrays[2].Length != 0)
			{
				string str = strArrays[2];
				_Position __Position = new _Position();
				string str1 = string.Format("select ownerId , PositionId ,TaskState,deptId from T_E_WF_UserTask where IsNull(IsAssign,'0')='0' and taskId in \r\n                (select top 1 _autoId from T_E_WF_Task where InstanceId='{0}' and ActivityId='{1}' order by ArriveTime desc)", this.ins.InstanceId, str);
				DataTable dataTable = new DataTable();
				dataTable = (dbTran != null ? SysDatabase.ExecuteTable(str1, dbTran) : SysDatabase.ExecuteTable(str1));
				if (dataTable.Rows.Count != 0)
				{
					foreach (DataRow row in dataTable.Rows)
					{
						string str2 = row["PositionId"].ToString();
						row["ownerId"].ToString();
						Position model = __Position.GetModel(str2);
						if (model != null)
						{
							if (!string.IsNullOrEmpty(model.ParentPositionId))
							{
								foreach (DeptEmployee deptEmployeeByPositionId in DeptEmployeeService.GetDeptEmployeeByPositionId(model.ParentPositionId))
								{
									deptEmployees1.Add(deptEmployeeByPositionId);
								}
							}
						}
					}
					deptEmployees = deptEmployees1;
				}
				else
				{
					deptEmployees = deptEmployees1;
				}
			}
			else
			{
				deptEmployees = deptEmployees1;
			}
			return deptEmployees;
		}

		public List<DeptEmployee> GetStartDeptLeader(string userDefine)
		{
			List<DeptEmployee> deptEmployees;
			List<DeptEmployee> deptEmployees1 = new List<DeptEmployee>();
			Department model = DepartmentService.GetModel(this.ins.DeptId);
			deptEmployees = (!string.IsNullOrEmpty(model.PicPositionId) ? DeptEmployeeService.GetDeptEmployeeByPositionId(model.PicPositionId) : deptEmployees1);
			return deptEmployees;
		}

		public List<DeptEmployee> GetTaskDealUser(string userDefine, Activity actObj, DbTransaction dbTran)
		{
			List<DeptEmployee> deptEmployees;
			List<DeptEmployee> deptEmployeeByPositionId = new List<DeptEmployee>();
			string[] strArrays = userDefine.Split(new char[] { '|' });
			if (strArrays[2].Length != 0)
			{
				string str = strArrays[2];
				_Position __Position = new _Position();
				string str1 = string.Format("select ownerId ,dealUser, PositionId ,TaskState,deptId from T_E_WF_UserTask where IsNull(IsAssign,'0')='0' and taskId in \r\n                (select top 1 _autoId from T_E_WF_Task where InstanceId='{0}' and ActivityId='{1}' order by ArriveTime desc)", this.ins.InstanceId, str);
				DataTable dataTable = new DataTable();
				dataTable = (dbTran != null ? SysDatabase.ExecuteTable(str1, dbTran) : SysDatabase.ExecuteTable(str1));
				if (dataTable.Rows.Count != 0)
				{
					foreach (DataRow row in dataTable.Rows)
					{
						string str2 = row["PositionId"].ToString();
						string str3 = row["ownerId"].ToString();
						string str4 = row["dealUser"].ToString();
						if (str4 == "")
						{
							str4 = str3;
						}
						DeptEmployee deptEmployee = DeptEmployeeService.GetDeptEmployeeByPositionId(str4, str2);
						if ((deptEmployee == null ? true : !EmployeeService.CheckedValid(deptEmployee.EmployeeID)))
						{
							deptEmployeeByPositionId = DeptEmployeeService.GetDeptEmployeeByPositionId(str2);
						}
						else
						{
							deptEmployeeByPositionId.Add(deptEmployee);
						}
					}
					deptEmployees = deptEmployeeByPositionId;
				}
				else
				{
					deptEmployees = deptEmployeeByPositionId;
				}
			}
			else
			{
				deptEmployees = deptEmployeeByPositionId;
			}
			return deptEmployees;
		}

        /// <summary>
        /// 获取实际处理的的人员。
        /// </summary>
        /// <param name="userDefine"></param>
        /// <param name="actObj"></param>
        /// <param name="dbTran"></param>
        /// <param name="IsAllUser"></param>
        /// <returns></returns>
        public List<DeptEmployee> GetTaskDealUser(string userDefine, Activity actObj, DbTransaction dbTran,Boolean IsAllUser)
        {
            List<DeptEmployee> deptEmployees;
            List<DeptEmployee> deptEmployeeByPositionId = new List<DeptEmployee>();
            string[] strArrays = userDefine.Split(new char[] { '|' });
            if (strArrays[2].Length != 0)
            {
                string str = strArrays[2];
                _Position __Position = new _Position();
                string str1 = string.Format("select ownerId ,dealUser, PositionId ,TaskState,deptId from T_E_WF_UserTask where IsNull(IsAssign,'0')='0' and taskId in \r\n                (select top 1 _autoId from T_E_WF_Task where InstanceId='{0}' and ActivityId='{1}' order by ArriveTime desc)", this.ins.InstanceId, str);
                DataTable dataTable = new DataTable();
                dataTable = (dbTran != null ? SysDatabase.ExecuteTable(str1, dbTran) : SysDatabase.ExecuteTable(str1));
                if (dataTable.Rows.Count != 0)
                {
                    foreach (DataRow row in dataTable.Rows)
                    {
                        string str2 = row["PositionId"].ToString();
                        string str3 = row["ownerId"].ToString();
                        string str4 = row["dealUser"].ToString();
                        if (str4 == "")
                        {
                            if (IsAllUser)
                            {
                                str4 = str3;
                            }
                            else
                            {
                                continue;
                            }
                        }
                        DeptEmployee deptEmployee = DeptEmployeeService.GetDeptEmployeeByPositionId(str4, str2);
                        if ((deptEmployee == null ? true : !EmployeeService.CheckedValid(deptEmployee.EmployeeID)))
                        {
                            deptEmployeeByPositionId = DeptEmployeeService.GetDeptEmployeeByPositionId(str2);
                        }
                        else
                        {
                            deptEmployeeByPositionId.Add(deptEmployee);
                        }
                    }
                    deptEmployees = deptEmployeeByPositionId;
                }
                else
                {
                    deptEmployees = deptEmployeeByPositionId;
                }
            }
            else
            {
                deptEmployees = deptEmployeeByPositionId;
            }
            return deptEmployees;
        }

		public List<DeptEmployee> GetTaskDeptLeader(string userDefine, Activity actObj, DbTransaction dbTran)
		{
			List<DeptEmployee> deptEmployees;
			List<DeptEmployee> deptEmployees1 = new List<DeptEmployee>();
			string[] strArrays = userDefine.Split(new char[] { '|' });
			if (strArrays[2].Length != 0)
			{
				string str = strArrays[2];
				_Position __Position = new _Position();
				string str1 = string.Format("select ownerId , PositionId ,deptId,TaskState from T_E_WF_UserTask where IsNull(IsAssign,'0')='0' and taskId in \r\n                (select top 1 _autoId from T_E_WF_Task where InstanceId='{0}' and ActivityId='{1}' order by ArriveTime desc)", this.ins.InstanceId, str);
				DataTable dataTable = new DataTable();
				dataTable = (dbTran != null ? SysDatabase.ExecuteTable(str1, dbTran) : SysDatabase.ExecuteTable(str1));
				if (dataTable.Rows.Count != 0)
				{
					foreach (DataRow row in dataTable.Rows)
					{
						row["PositionId"].ToString();
						row["ownerId"].ToString();
						string str2 = row["deptId"].ToString();
						Department model = DepartmentService.GetModel(str2);
						if (!string.IsNullOrEmpty(model.PicPositionId))
						{
							foreach (DeptEmployee deptEmployeeByPositionId in DeptEmployeeService.GetDeptEmployeeByPositionId(model.PicPositionId))
							{
								deptEmployees1.Add(deptEmployeeByPositionId);
							}
						}
						else
						{
							deptEmployees = deptEmployees1;
							return deptEmployees;
						}
					}
					deptEmployees = deptEmployees1;
				}
				else
				{
					deptEmployees = deptEmployees1;
				}
			}
			else
			{
				deptEmployees = deptEmployees1;
			}
			return deptEmployees;
		}

		public List<DeptEmployee> GetTaskDeptUpLeader(string userDefine, Activity actObj, DbTransaction dbTran)
		{
			List<DeptEmployee> deptEmployees;
			List<DeptEmployee> deptEmployees1 = new List<DeptEmployee>();
			string[] strArrays = userDefine.Split(new char[] { '|' });
			if (strArrays[2].Length != 0)
			{
				string str = strArrays[2];
				_Position __Position = new _Position();
				string str1 = string.Format("select ownerId ,PositionId ,deptId,TaskState from T_E_WF_UserTask where IsNull(IsAssign,'0')='0' and taskId in \r\n                (select top 1 _autoId from T_E_WF_Task where InstanceId='{0}' and ActivityId='{1}' order by ArriveTime desc)", this.ins.InstanceId, str);
				DataTable dataTable = new DataTable();
				dataTable = (dbTran != null ? SysDatabase.ExecuteTable(str1, dbTran) : SysDatabase.ExecuteTable(str1));
				if (dataTable.Rows.Count != 0)
				{
					TaskPerformer2.fileLogger.Trace<int, string>("GetTaskDeptUpLeader:返回{0},SQL:{1}", dataTable.Rows.Count, str1);
					foreach (DataRow row in dataTable.Rows)
					{
						row["PositionId"].ToString();
						row["ownerId"].ToString();
						string str2 = row["deptId"].ToString();
						Department model = DepartmentService.GetModel(str2);
						TaskPerformer2.fileLogger.Trace<string, string>("GetTaskDeptUpLeader:UpPositionId={0}，deptId={1}", model.UpPositionId, str2);
						if (!string.IsNullOrEmpty(model.UpPositionId))
						{
							List<DeptEmployee> deptEmployeeByPositionId = DeptEmployeeService.GetDeptEmployeeByPositionId(model.UpPositionId);
							TaskPerformer2.fileLogger.Trace("GetTaskDeptUpLeader:岗位人员={0}", deptEmployeeByPositionId.Count);
							foreach (DeptEmployee deptEmployee in deptEmployeeByPositionId)
							{
								deptEmployees1.Add(deptEmployee);
							}
						}
					}
					deptEmployees = deptEmployees1;
				}
				else
				{
					deptEmployees = deptEmployees1;
				}
			}
			else
			{
				deptEmployees = deptEmployees1;
			}
			return deptEmployees;
		}

		public List<DeptEmployee> GetTopLevelDeptLeader(string userDefine, DbTransaction dbTran)
		{
			List<DeptEmployee> deptEmployees;
			List<DeptEmployee> deptEmployees1 = new List<DeptEmployee>();
			_Department __Department = new _Department(dbTran);
			Department topLevelDeptModel = __Department.GetTopLevelDeptModel(this.ins.DeptId);
			if (topLevelDeptModel == null)
			{
				deptEmployees = deptEmployees1;
			}
			else
			{
				deptEmployees = (!string.IsNullOrEmpty(topLevelDeptModel.PicPositionId) ? DeptEmployeeService.GetDeptEmployeeByPositionId(topLevelDeptModel.PicPositionId) : deptEmployees1);
			}
			return deptEmployees;
		}

		public List<DeptEmployee> GetUpLevelDeptLeader(string userDefine, DbTransaction dbTran)
		{
			List<DeptEmployee> deptEmployees;
			List<DeptEmployee> deptEmployees1 = new List<DeptEmployee>();
			_Department __Department = new _Department(dbTran);
			Department parentDeptModel = __Department.GetParentDeptModel(this.ins.DeptId);
			deptEmployees = (!string.IsNullOrEmpty(parentDeptModel.PicPositionId) ? DeptEmployeeService.GetDeptEmployeeByPositionId(parentDeptModel.PicPositionId) : deptEmployees1);
			return deptEmployees;
		}
	}
}