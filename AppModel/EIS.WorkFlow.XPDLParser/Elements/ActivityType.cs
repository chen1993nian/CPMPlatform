using System;

namespace EIS.WorkFlow.XPDLParser.Elements
{
	public enum ActivityType
	{
		Start,
		End,
		Normal,
		Sign,
		Mail,
		Dll,
		And,
		Xor,
		Block,
		SubWorkflow,
		Auto
	}
}