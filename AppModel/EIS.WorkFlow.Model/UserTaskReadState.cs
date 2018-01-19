using EIS.AppBase;
using System;

namespace EIS.WorkFlow.Model
{
	public enum UserTaskReadState
	{
		[EnumDescription("未读")]
		UnRead,
		[EnumDescription("已读")]
		Read
	}
}