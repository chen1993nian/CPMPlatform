using EIS.AppBase;
using System;

namespace EIS.WorkFlow.Model
{
	public enum InstanceState
	{
		[EnumDescription("未发起")]
		Ready,
		[EnumDescription("处理中")]
		Processing,
		[EnumDescription("挂起")]
		Suspended,
		[EnumDescription("终止")]
		Stoped,
		[EnumDescription("完成")]
		Finished,
		[EnumDescription("归档")]
		Archived
	}
}