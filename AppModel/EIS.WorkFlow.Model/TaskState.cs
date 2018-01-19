using EIS.AppBase;
using System;

namespace EIS.WorkFlow.Model
{
	public enum TaskState
	{
		[EnumDescription("未处理")]
		NotStart,
		[EnumDescription("处理中")]
		Processing,
		[EnumDescription("完成")]
		Finished
	}
}