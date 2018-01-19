using System;

namespace EIS.AppBase
{
	public interface IRemoteCall
	{
		byte[] GeneralCall(string methodName, params byte[] param);
	}
}