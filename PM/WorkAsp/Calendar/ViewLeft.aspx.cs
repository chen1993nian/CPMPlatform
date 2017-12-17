using AjaxPro;
using EIS.AppBase;
using EIS.AppCommon;
using EIS.DataAccess;
using EIS.DataModel.Model;
using EIS.Permission.Service;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Web.UI.HtmlControls;

namespace EIS.Web.WorkAsp.Calendar
{
	public partial class ViewLeft : PageBase
	{
		public string treedata = "";

		public string treeId = "";

		private List<FolderInfo> list_0 = new List<FolderInfo>();

	

		private void method_0(zTreeNode zTreeNode_0, string string_0)
		{
			StringCollection stringCollections = new StringCollection();
			DataTable dataTable = SysDatabase.ExecuteTable(string.Format("select UserId,UserName from T_E_Org_Group where _AutoId='{0}'", string_0));
			if (dataTable.Rows.Count > 0)
			{
				string str = dataTable.Rows[0]["UserId"].ToString();
				string str1 = dataTable.Rows[0]["UserName"].ToString();
				char[] chrArray = new char[] { ',' };
				string[] strArrays = str1.Split(chrArray);
				chrArray = new char[] { ',' };
				string[] strArrays1 = str.Split(chrArray);
				for (int i = 0; i < (int)strArrays1.Length; i++)
				{
					zTreeNode _zTreeNode = new zTreeNode()
					{
						name = strArrays[i],
						id = strArrays1[i],
						@value = "1"
					};
					if ((stringCollections.Contains(_zTreeNode.id) ? false : EmployeeService.CheckedValid(_zTreeNode.id)))
					{
						zTreeNode_0.Add(_zTreeNode);
						stringCollections.Add(_zTreeNode.id);
					}
				}
			}
		}

		private void method_1(zTreeNode zTreeNode_0, string string_0)
		{
			StringCollection stringCollections = new StringCollection();
			foreach (DataRow row in SysDatabase.ExecuteTable(string.Format("select de.employeeId,e.employeeName,de.positionId from T_E_Org_DeptEmployee de inner join T_E_Org_Employee e\r\n                on de.employeeId=e._autoId\r\n                where de._isDel=0 and e._isdel=0 and e.islocked='否' and de.positionId in (select _autoId from T_E_Org_Position where PropName='{0}')", string_0)).Rows)
			{
				zTreeNode _zTreeNode = new zTreeNode()
				{
					name = row["employeeName"].ToString(),
					id = row["employeeId"].ToString(),
					@value = "1"
				};
				if ((stringCollections.Contains(_zTreeNode.id) ? true : !EmployeeService.CheckedValid(_zTreeNode.id)))
				{
					continue;
				}
				zTreeNode_0.Add(_zTreeNode);
				stringCollections.Add(_zTreeNode.id);
			}
		}

		[AjaxMethod]    // JustDecompile was unable to locate the assembly where attribute parameters types are defined. Generating parameters values is impossible.
		public string NewFolder(string folderName, string folderPId)
		{
			return "";
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			DataRow row = null;
			zTreeNode _zTreeNode;
			AjaxPro.Utility.RegisterTypeForAjax(typeof(ViewLeft));
			zTreeNode _zTreeNode1 = new zTreeNode();
			if (this.treeId == "")
			{
				_zTreeNode1.id = "0";
				_zTreeNode1.name = "联系人分组";
				_zTreeNode1.@value = "0";
				_zTreeNode1.open = true;
			}
			string str = string.Format("select * from T_E_Org_Group where GroupType='公共' or EmployeeId='{0}' order by GroupType", base.EmployeeID);
			foreach (DataRow rowa in SysDatabase.ExecuteTable(str).Rows)
			{
				_zTreeNode = new zTreeNode()
				{
                    name = rowa["GroupName"].ToString(),
                    id = rowa["_AutoId"].ToString(),
					@value = "0"
				};
				_zTreeNode1.Add(_zTreeNode);
				this.method_0(_zTreeNode, _zTreeNode.id);
			}
			str = string.Format("select RoleName from T_E_Org_Role where roleType='岗位属性'  order by OrderId", new object[0]);
			foreach (DataRow dataRow in SysDatabase.ExecuteTable(str).Rows)
			{
				_zTreeNode = new zTreeNode()
				{
					name = dataRow["RoleName"].ToString(),
					id = dataRow["RoleName"].ToString(),
					@value = "0"
				};
				_zTreeNode1.Add(_zTreeNode);
				this.method_1(_zTreeNode, _zTreeNode.name);
			}
			this.treedata = _zTreeNode1.ToJsonString(true);
		}
	}
}