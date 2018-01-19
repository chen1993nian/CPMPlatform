using EIS.AppBase;
using System;

namespace EIS.WorkFlow.Model
{
	public enum UserTaskState
	{
		[EnumDescription("共享任务申请前")]
		ShareTaskForApply,
		[EnumDescription("待办")]
		ToDo,
		[EnumDescription("完成")]
		Finished,
		[EnumDescription(" 共享任务申请后（作废的任务）")]
		ShareTaskApplied
	}
}