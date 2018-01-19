using System;

namespace EIS.Permission
{
	public enum LoginInfoType
	{
		Allowed,
		NotExist,
		WrongPwd,
		IsLocked
	}
}