using System;

namespace EIS.AppBase
{
	public interface ISecurity
	{
		string EncryptPassword(string str);
	}
}