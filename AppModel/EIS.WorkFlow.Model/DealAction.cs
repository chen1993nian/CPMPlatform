using EIS.AppBase;
using System;

namespace EIS.WorkFlow.Model
{
	public enum DealAction
	{
		[EnumDescription("提交")]
		Submit,
		[EnumDescription("同意")]
		Agree,
		[EnumDescription("不同意")]
		Disagree,
		[EnumDescription("退回")]
		RollBack,
		[EnumDescription("直送")]
		DirectTo,
		[EnumDescription("委托")]
		DelegateTo
	}
}