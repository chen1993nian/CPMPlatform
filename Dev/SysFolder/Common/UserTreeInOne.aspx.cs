using AjaxPro;
using EIS.AppBase;
using EIS.AppBase.Config;
using EIS.DataAccess;
using EIS.Permission.Access;
using EIS.Permission.Model;
using EIS.Permission.Service;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.Common;
using System.Runtime.CompilerServices;
using System.Text;
using System.Web.SessionState;
using System.Web.UI;

namespace EIS.WebBase.SysFolder.Common
{
	public partial class UserTreeInOne : PageBase
	{
		private List<Department> list_0;

		public string treedata = "";

		private StringBuilder stringBuilder_0 = new StringBuilder();

		public string selmethod = "";

        public string cid = "";

		public string queryfield = "";

		public StringBuilder sbGroup = new StringBuilder();

		public StringBuilder sbMostUse = new StringBuilder();

	

		[AjaxMethod]
		public string GetEmployeeAttr(string empId, string posId)
		{
			string str;
			StringBuilder stringBuilder = new StringBuilder();
			if (string.IsNullOrEmpty(posId))
			{
				posId = EmployeeService.GetDefaultPositionById(empId)._AutoID;
			}
			DeptEmployee deptEmployeeByPositionId = DeptEmployeeService.GetDeptEmployeeByPositionId(empId, posId);
			if (deptEmployeeByPositionId != null)
			{
				Employee model = (new _Employee()).GetModel(empId);
				stringBuilder.Append(string.Concat(model.EmployeeCode, "|"));
				stringBuilder.Append(string.Concat(model.EMail, "|"));
				stringBuilder.Append(string.Concat(model.Officephone, "|"));
				stringBuilder.Append(string.Concat(model.Cellphone, "|"));
				stringBuilder.Append(string.Concat(deptEmployeeByPositionId.PositionName, "|"));
				Department department = DepartmentService.GetModel(deptEmployeeByPositionId.DeptID);
				stringBuilder.Append(string.Concat(department.DeptCode, "|"));
				stringBuilder.Append(string.Concat(department.DeptName, "|"));
				Department topLevelDeptModel = (new _Department()).GetTopLevelDeptModel(department._AutoID);
				if (topLevelDeptModel == null)
				{
					stringBuilder.Append("|");
					stringBuilder.Append("|");
				}
				else
				{
					stringBuilder.Append(string.Concat(topLevelDeptModel.DeptCode, "|"));
					stringBuilder.Append(string.Concat(topLevelDeptModel.DeptName, "|"));
				}
				Department model1 = DepartmentService.GetModel(deptEmployeeByPositionId.CompanyID);
				if (model1 == null)
				{
					stringBuilder.Append("|");
					stringBuilder.Append("|");
				}
				else
				{
					stringBuilder.Append(string.Concat(model1.DeptCode, "|"));
					stringBuilder.Append(string.Concat(model1.DeptName, "|"));
				}
				str = stringBuilder.ToString();
			}
			else
			{
				str = "";
			}
			return str;
		}

		[AjaxMethod(HttpSessionStateRequirement.Read)]    // JustDecompile was unable to locate the assembly where attribute parameters types are defined. Generating parameters values is impossible.
		public string GetGroup(string groupId)
		{
			string str = string.Format("select cast(UserId as nvarchar(4000)) + '|' + cast(UserName as nvarchar(4000)) + '|' + cast(UserPosId as nvarchar(4000)) from T_E_Org_Group where _AutoId='{0}'", groupId);
			return SysDatabase.ExecuteScalar(str).ToString();
		}

		[AjaxMethod(HttpSessionStateRequirement.Read)]    // JustDecompile was unable to locate the assembly where attribute parameters types are defined. Generating parameters values is impossible.
		public string GetOnline()
		{
			string itemValue = SysConfig.GetConfig("OnlineRefreshInterval").ItemValue;
			int num = (string.IsNullOrEmpty(itemValue) ? 120 : Convert.ToInt32(itemValue));
			string str = string.Format("select d.employeeId,e.employeeName,d.deptname,d.positionname,d.positionId \r\n                from T_E_Org_Employee e inner join T_E_Org_DeptEmployee d on e._AutoID = d.EmployeeID \r\n                where e._IsDel=0  and d._IsDel=0 and d.DeptEmployeeType=0 and datediff(s, e.RefreshTime, getdate()) < {0}", (num < 30 ? 30 : num));
			DataTable dataTable = SysDatabase.ExecuteTable(str);
			string fieldValueList = EIS.AppBase.Utility.GetFieldValueList(dataTable, "employeeId");
            string fieldValueList1 = EIS.AppBase.Utility.GetFieldValueList(dataTable, "employeeName");
            string str1 = EIS.AppBase.Utility.GetFieldValueList(dataTable, "positionId");
			string[] strArrays = new string[] { fieldValueList, "|", fieldValueList1, "|", str1 };
			return string.Concat(strArrays);
		}

		[AjaxMethod(HttpSessionStateRequirement.Read)]    // JustDecompile was unable to locate the assembly where attribute parameters types are defined. Generating parameters values is impossible.
		public string GetPosition(string propName)
		{
			DataTable dataTable = SysDatabase.ExecuteTable(string.Format("select de.employeeId,e.employeeName,de.positionId from T_E_Org_DeptEmployee de inner join T_E_Org_Employee e\r\n                on de.employeeId=e._autoId\r\n                where de._isDel=0 and e._isdel=0 and e.islocked='否' and de.positionId in (select _autoId from T_E_Org_Position where PropName='{0}')", propName));
            string fieldValueList = EIS.AppBase.Utility.GetFieldValueList(dataTable, "employeeId");
            string str = EIS.AppBase.Utility.GetFieldValueList(dataTable, "employeeName");
            string fieldValueList1 = EIS.AppBase.Utility.GetFieldValueList(dataTable, "positionId");
			string[] strArrays = new string[] { fieldValueList, "|", str, "|", fieldValueList1 };
			return string.Concat(strArrays);
		}

		[AjaxMethod]
		public string GetTitle(string empId, string posId)
		{
			string str;
			DeptEmployee deptEmployeeByPositionId = DeptEmployeeService.GetDeptEmployeeByPositionId(empId, posId);
			if (deptEmployeeByPositionId != null)
			{
				Department model = DepartmentService.GetModel(deptEmployeeByPositionId.CompanyID);
				string employeeAttrById = EmployeeService.GetEmployeeAttrById(deptEmployeeByPositionId.EmployeeID, "EmployeeCode");
				string str1 = (deptEmployeeByPositionId.PositionName == "未知" ? "" : string.Concat("-", deptEmployeeByPositionId.PositionName));
				employeeAttrById = (employeeAttrById == "" ? "" : string.Concat("(", employeeAttrById, ")"));
				str = string.Concat(model.DeptAbbr, "-", deptEmployeeByPositionId.DeptName, employeeAttrById);
			}
			else
			{
				str = "";
			}
			return str;
		}

		private void method_0(TreeItem treeItem_0)
		{
			
			List<Department> departments = this.list_0.FindAll((Department department_0) => (department_0.DeptPWBS != treeItem_0.id ? false : department_0.DeptState != "停用"));
			if (departments.Count > 0)
			{
				foreach (Department department in departments)
				{
					TreeItem treeItem = new TreeItem()
					{
						text = department.DeptName,
						id = department.DeptWBS,
						@value = department._AutoID,
						complete = false,
						hasChildren = true
					};
					treeItem_0.Add(treeItem);
					this.method_0(treeItem);
				}
			}
		}

		private void method_1()
		{
			base.Load += new EventHandler(this.UserTreeInOne_Load);
		}

		[AjaxMethod(HttpSessionStateRequirement.Read)]    // JustDecompile was unable to locate the assembly where attribute parameters types are defined. Generating parameters values is impossible.
		public void NewGroup(string groupName, string[] arrId, string[] arrName, string[] arrPos)
		{
			if (int.Parse(SysDatabase.ExecuteScalar(string.Format("select count(*) from T_E_Org_Group where EmployeeId='{0}' and GroupName='{1}'", base.EmployeeID, groupName)).ToString()) > 0)
			{
				throw new Exception("相同的分组名称已经存在，请修改分组名称");
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("Insert T_E_Org_Group (\r\n \t\t\t\t\t_AutoID,\r\n\t\t\t\t\t_UserName,\r\n\t\t\t\t\t_OrgCode,\r\n\t\t\t\t\t_CreateTime,\r\n\t\t\t\t\t_UpdateTime,\r\n\t\t\t\t\t_IsDel,\r\n                    CompanyId,\r\n\t\t\t\t\tEmployeeId,\r\n                    EmployeeName,\r\n\t\t\t\t\tGroupType,\r\n\t\t\t\t\tGroupName,\r\n\t\t\t\t\tUserId,\r\n\t\t\t\t\tUserName,\r\n\t\t\t\t\tUserPosId,\r\n\t\t\t\t\tOrderId\r\n\r\n\t\t\t) values(\r\n\t\t\t\t\t@_AutoID,\r\n\t\t\t\t\t@_UserName,\r\n\t\t\t\t\t@_OrgCode,\r\n\t\t\t\t\t@_CreateTime,\r\n\t\t\t\t\t@_UpdateTime,\r\n\t\t\t\t\t@_IsDel,\r\n                    @CompanyId,\r\n\t\t\t\t\t@EmployeeId,\r\n                    @EmployeeName,\r\n\t\t\t\t\t@GroupType,\r\n\t\t\t\t\t@GroupName,\r\n\t\t\t\t\t@UserId,\r\n\t\t\t\t\t@UserName,\r\n\t\t\t\t\t@UserPosId,\r\n\t\t\t\t\t@OrderId\r\n\t\t\t)");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			UserContext userInfo = base.UserInfo;
			string str = Guid.NewGuid().ToString();
			SysDatabase.AddInParameter(sqlStringCommand, "@_AutoID", DbType.String, str);
			SysDatabase.AddInParameter(sqlStringCommand, "@_UserName", DbType.String, userInfo.EmployeeId);
			SysDatabase.AddInParameter(sqlStringCommand, "@_OrgCode", DbType.String, userInfo.DeptWbs);
			SysDatabase.AddInParameter(sqlStringCommand, "@_CreateTime", DbType.DateTime, DateTime.Now);
			SysDatabase.AddInParameter(sqlStringCommand, "@_UpdateTime", DbType.DateTime, DateTime.Now);
			SysDatabase.AddInParameter(sqlStringCommand, "@_IsDel", DbType.Int32, 0);
			SysDatabase.AddInParameter(sqlStringCommand, "@CompanyId", DbType.String, base.CompanyId);
			SysDatabase.AddInParameter(sqlStringCommand, "@EmployeeId", DbType.String, base.EmployeeID);
			SysDatabase.AddInParameter(sqlStringCommand, "@EmployeeName", DbType.String, base.EmployeeName);
			SysDatabase.AddInParameter(sqlStringCommand, "@GroupType", DbType.String, "");
			SysDatabase.AddInParameter(sqlStringCommand, "@GroupName", DbType.String, groupName);
            SysDatabase.AddInParameter(sqlStringCommand, "@UserId", DbType.String, EIS.AppBase.Utility.GetJoinString(arrId));
            SysDatabase.AddInParameter(sqlStringCommand, "@UserName", DbType.String, EIS.AppBase.Utility.GetJoinString(arrName));
            SysDatabase.AddInParameter(sqlStringCommand, "@UserPosId", DbType.String, EIS.AppBase.Utility.GetJoinString(arrPos));
			SysDatabase.AddInParameter(sqlStringCommand, "@OrderId", DbType.Int32, 0);
			SysDatabase.ExecuteNonQuery(sqlStringCommand);
		}

		protected override void OnInit(EventArgs eventArgs_0)
		{
			this.method_1();
			base.OnInit(eventArgs_0);
		}

		[AjaxMethod(HttpSessionStateRequirement.Read)]    // JustDecompile was unable to locate the assembly where attribute parameters types are defined. Generating parameters values is impossible.
		public string Search(string string_1)
		{
			DataTable dataTable = SysDatabase.ExecuteTable(string.Format("select e._autoId employeeId,e.employeeName \r\n                + ' (' + de.deptName + case de.positionName when '未知' then '' else '-'+de.positionName end +')' as employeeName,\r\n                de.positionId,de.companyId from T_E_Org_DeptEmployee de inner join T_E_Org_Employee e \r\n                on de.employeeId=e._autoId inner join T_E_Org_Department d on de.companyId=d._autoId \r\n                where de._isdel=0 and e.islocked='否' and ( e.loginName like '%{0}%' or e.employeeName like '%{0}%') ", string_1));
            string fieldValueList = EIS.AppBase.Utility.GetFieldValueList(dataTable, "employeeId");
            string str = EIS.AppBase.Utility.GetFieldValueList(dataTable, "employeeName");
            string fieldValueList1 = EIS.AppBase.Utility.GetFieldValueList(dataTable, "positionId");
			string[] strArrays = new string[] { fieldValueList, "|", str, "|", fieldValueList1 };
			return string.Concat(strArrays);
		}

		private void UserTreeInOne_Load(object sender, EventArgs e)
		{
			object[] deptAbbr;
			DataRow row = null;
			this.selmethod = base.GetParaValue("method");
            this.cid = base.GetParaValue("cid");
			this.queryfield = base.GetParaValue("queryfield");
			AjaxPro.Utility.RegisterTypeForAjax(typeof(UserTreeInOne));
			this.Session["CompanyWbs"].ToString();
			Department topDept = DepartmentService.GetTopDept();
			this.list_0 = (new _Department()).GetSonDeptByWbs(topDept.DeptWBS);
			TreeItem treeItem = new TreeItem()
			{
				id = topDept.DeptWBS,
				text = topDept.DeptName,
				@value = topDept._AutoID,
				isexpand = true
			};
			this.method_0(treeItem);
			this.treedata = treeItem.ToJsonString();
			StringCollection stringCollections = new StringCollection();
			string str = string.Format("select * from T_E_Org_Group where EmployeeId='{0}' and GroupType='mostuse'", base.EmployeeID);
			DataTable dataTable = SysDatabase.ExecuteTable(str);
			if (dataTable.Rows.Count > 0)
			{
				string str1 = dataTable.Rows[0]["userId"].ToString();
				string str2 = dataTable.Rows[0]["userName"].ToString();
				string str3 = dataTable.Rows[0]["userPosId"].ToString();
				if ((str1.Length <= 0 ? false : str2.Length > 0))
				{
					char[] chrArray = new char[] { ',' };
					string[] strArrays = str1.Split(chrArray);
					chrArray = new char[] { ',' };
					str2.Split(chrArray);
					chrArray = new char[] { ',' };
					string[] strArrays1 = str3.Split(chrArray);
					for (int i = 0; i < (int)strArrays.Length; i++)
					{
						if ((stringCollections.Contains(strArrays[i]) ? false : EmployeeService.CheckedValid(strArrays[i])))
						{
							DeptEmployee deptEmployeeByPositionId = DeptEmployeeService.GetDeptEmployeeByPositionId(strArrays[i], strArrays1[i]);
							if (deptEmployeeByPositionId != null && deptEmployeeByPositionId._IsDel != 1)
							{
								Department model = DepartmentService.GetModel(deptEmployeeByPositionId.CompanyID);
								stringCollections.Add(string.Concat(strArrays[i], "-", strArrays1[i]));
								StringBuilder stringBuilder = this.sbMostUse;
								deptAbbr = new object[] { deptEmployeeByPositionId.EmployeeName.Replace(" ", ""), string.Concat(strArrays[i], "_", strArrays1[i]), strArrays1[i], null, null, null };
								deptAbbr[3] = (deptEmployeeByPositionId.PositionName == "未知" ? "" : string.Concat("-", deptEmployeeByPositionId.PositionName));
								deptAbbr[4] = model.DeptAbbr;
								deptAbbr[5] = deptEmployeeByPositionId.DeptName;
								stringBuilder.AppendFormat("<div class='mostPanel' title='{4}-{5}{3}' ><input type='checkbox' class='chkmost' value='{1}' id='mostPanel{1}'/>\r\n                                <label for='mostPanel{1}' class='mysel'>{0} ({5}{3})</label></div>", deptAbbr);
							}
						}
					}
				}
			}
			string str4 = this.Session["deptId"].ToString();
			dataTable = SysDatabase.ExecuteTable(string.Format("select d.employeeId ,d.employeeName,d.PositionId,d.PositionName,e.EmployeeCode from \r\n                    t_e_org_deptemployee d inner join t_e_org_employee e on d.employeeId=e._autoId where d._isDel=0 and d.deptId='{0}'\r\n                    order by d.OrderID,d.employeeName", str4));
			foreach (DataRow rowA in dataTable.Rows)
			{
                string str5 = rowA["employeeId"].ToString();
                string str6 = rowA["PositionId"].ToString();
				if ((stringCollections.Contains(str5) ? true : !EmployeeService.CheckedValid(str5)))
				{
					continue;
				}
				stringCollections.Add(string.Concat(str5, "-", str6));
				string str7 = row["PositionName"].ToString();
				string str8 = row["EmployeeCode"].ToString();
				StringBuilder stringBuilder1 = this.sbMostUse;
				deptAbbr = new object[] { row["employeeName"].ToString().Replace(" ", ""), string.Concat(str5, "_", str6), str6, null, null, null, null };
				deptAbbr[3] = (str7 == "未知" ? "" : string.Concat("-", str7));
				deptAbbr[4] = this.Session["CompanyAbbr"].ToString();
				deptAbbr[5] = this.Session["DeptName"].ToString();
				deptAbbr[6] = (str8 == "" ? "" : string.Concat("(", str8, ")"));
				stringBuilder1.AppendFormat("<div class='mostPanel' title='{4}-{5}{6}'><input type='checkbox' class='chkmost' value='{1}' id='mostPanel{1}'/>\r\n                        <label for='mostPanel{1}'>{0} ({5}{3})</label></div>", deptAbbr);
			}
			str = string.Format("select * from T_E_Org_Group where GroupType='公共' or EmployeeId='{0}' order by GroupType", base.EmployeeID);
			dataTable = SysDatabase.ExecuteTable(str);
			foreach (DataRow dataRow in dataTable.Rows)
			{
				this.sbGroup.AppendFormat("<div class='groupPanel1' ><input type='checkbox' class='chkgroup' value='{1}' id='chkgroup{1}'/> <label for='chkgroup{1}'>{0}</label></div>", dataRow["GroupName"], dataRow["_AutoId"]);
			}
			str = string.Format("select RoleName from T_E_Org_Role where roleType='岗位属性'  order by OrderId", new object[0]);
			dataTable = SysDatabase.ExecuteTable(str);
			foreach (DataRow row1 in dataTable.Rows)
			{
				this.sbGroup.AppendFormat("<div class='groupPanel2' ><input type='checkbox' class='chkgroup' value='{1}' id='chkgroup{1}'/> <label for='chkgroup{1}'>{0}</label></div>", row1["RoleName"], row1["RoleName"]);
			}
		}
	}
}