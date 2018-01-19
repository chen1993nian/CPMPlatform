using System;
using System.Runtime.CompilerServices;

namespace EIS.WorkFlow.Model
{
	[Serializable]
	public class Task
	{
		public readonly static string F__AutoID;

		public readonly static string F__UserName;

		public readonly static string F__OrgCode;

		public readonly static string F__CreateTime;

		public readonly static string F__UpdateTime;

		public readonly static string F__IsDel;

		public readonly static string F_InstanceId;

		public readonly static string F_TaskName;

		public readonly static string F_TaskType;

		public readonly static string F_DefineType;

		public readonly static string F_ActivityId;

		public readonly static string F_ArriveTime;

		public readonly static string F_FromTaskId;

		public readonly static string F_IsManualTask;

		public readonly static string F_CanHangUp;

		public readonly static string F_CanStop;

		public readonly static string F_CanRollBack;

		public readonly static string F_CanRelegateTo;

		public readonly static string F_CanAssign;

		public readonly static string F_CanPublic;

		public readonly static string F_CanJump;

		public readonly static string F_CanDelete;

		public readonly static string F_CanCancel;

		public readonly static string F_CanAttachment;

		public readonly static string F_TaskState;

		public DateTime _CreateTime
		{
			get;
			set;
		}

		public int _IsDel
		{
			get;
			set;
		}

		public string _OrgCode
		{
			get;
			set;
		}

		public DateTime _UpdateTime
		{
			get;
			set;
		}

		public string _UserName
		{
			get;
			set;
		}

		public string ActivityId
		{
			get;
			set;
		}

		public DateTime ArriveTime
		{
			get;
			set;
		}

		public string CanAssign
		{
			get;
			set;
		}

		public string CanAttachment
		{
			get;
			set;
		}

		public string CanBatch
		{
			get;
			set;
		}

		public string CanDelegateTo
		{
			get;
			set;
		}

		public string CanFetch
		{
			get;
			set;
		}

		public string CanHangUp
		{
			get;
			set;
		}

		public string CanJump
		{
			get;
			set;
		}

		public string CanPublic
		{
			get;
			set;
		}

		public string CanReturn
		{
			get;
			set;
		}

		public string CanRollBack
		{
			get;
			set;
		}

		public string CanSelPath
		{
			get;
			set;
		}

		public string CanStop
		{
			get;
			set;
		}

		public DateTime? Deadline
		{
			get;
			set;
		}

		public string DealStrategy
		{
			get;
			set;
		}

		public string DefineType
		{
			get;
			set;
		}

		public string FromTaskId
		{
			get;
			set;
		}

		public string HideAdviceOnDeal
		{
			get;
			set;
		}

		public string InstanceId
		{
			get;
			set;
		}

		public string IsManualTask
		{
			get;
			set;
		}

		public string MainPerformer
		{
			get;
			set;
		}

		public string NodeCode
		{
			get;
			set;
		}

		public string NodeStyle
		{
			get;
			set;
		}

		public string OverTimeAction
		{
			get;
			set;
		}

		public int OverTimeAlert
		{
			get;
			set;
		}

		public DateTime? OverTimeAlertFirst
		{
			get;
			set;
		}

		public int OverTimeAlertInterval
		{
			get;
			set;
		}

		public int OverTimeAlertRepeat
		{
			get;
			set;
		}

		public string OverTimeCalendarId
		{
			get;
			set;
		}

		public string TaskId
		{
			get;
			set;
		}

		public string TaskName
		{
			get;
			set;
		}

		public string TaskState
		{
			get;
			set;
		}

		public string TaskType
		{
			get;
			set;
		}

		static Task()
		{
			Task.F__AutoID = "_AutoID";
			Task.F__UserName = "_UserName";
			Task.F__OrgCode = "_OrgCode";
			Task.F__CreateTime = "_CreateTime";
			Task.F__UpdateTime = "_UpdateTime";
			Task.F__IsDel = "_IsDel";
			Task.F_InstanceId = "InstanceId";
			Task.F_TaskName = "TaskName";
			Task.F_TaskType = "TaskType";
			Task.F_DefineType = "DefineType";
			Task.F_ActivityId = "ActivityId";
			Task.F_ArriveTime = "ArriveTime";
			Task.F_FromTaskId = "FromTaskId";
			Task.F_IsManualTask = "IsManualTask";
			Task.F_CanHangUp = "CanHangUp";
			Task.F_CanStop = "CanStop";
			Task.F_CanRollBack = "CanRollBack";
			Task.F_CanRelegateTo = "CanDelegateTo";
			Task.F_CanAssign = "CanAssign";
			Task.F_CanPublic = "CanPublic";
			Task.F_CanJump = "CanJump";
			Task.F_CanDelete = "CanDelete";
			Task.F_CanCancel = "CanReturn";
			Task.F_CanAttachment = "CanAttachment";
			Task.F_TaskState = "TaskState";
		}

		public Task()
		{
		}
	}
}