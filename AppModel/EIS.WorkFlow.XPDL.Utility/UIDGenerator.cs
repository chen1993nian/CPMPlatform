using System;

namespace EIS.WorkFlow.XPDL.Utility
{
	public class UIDGenerator
	{
		private static long long_0;

		static UIDGenerator()
		{
			UIDGenerator.long_0 = DateTime.Now.Ticks;
		}

		public static string GenerateID()
		{
			string str;
			lock (typeof(UIDGenerator))
			{
				long long0 = UIDGenerator.long_0;
				UIDGenerator.long_0 = long0 + (long)1;
				str = long0.ToString();
			}
			return str;
		}
	}
}