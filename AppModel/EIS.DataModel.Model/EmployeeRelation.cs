using EIS.AppBase;
using System;
using System.Runtime.CompilerServices;

namespace EIS.DataModel.Model
{
	[Serializable]
	public class EmployeeRelation : AppModelBase
	{
		public string EmployeeId
		{
			get;
			set;
		}

		public string EmployeeName
		{
			get;
			set;
		}

		public string LeaderId
		{
			get;
			set;
		}

		public string LeaderName
		{
			get;
			set;
		}

		public string Relation
		{
			get;
			set;
		}

		public string State
		{
			get;
			set;
		}

		public EmployeeRelation()
		{
		}

		public EmployeeRelation(UserContext user)
		{
			base._AutoID = Guid.NewGuid().ToString();
			base._UserName = user.EmployeeId;
			base._OrgCode = user.DeptWbs;
			base._CreateTime = DateTime.Now;
			base._UpdateTime = DateTime.Now;
			base._IsDel = 0;
		}
	}
}