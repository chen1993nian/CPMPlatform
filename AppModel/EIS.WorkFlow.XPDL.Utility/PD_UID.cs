using System;

namespace EIS.WorkFlow.XPDL.Utility
{
	[Serializable]
	public class PD_UID
	{
		private string m_sFilePath = "";

		private string m_sProcessID = "";

		public string PackageID
		{
			get
			{
				return this.m_sFilePath;
			}
			set
			{
				this.m_sFilePath = value;
			}
		}

		public string ProcessID
		{
			get
			{
				return this.m_sProcessID;
			}
			set
			{
				this.m_sProcessID = value;
			}
		}

		public PD_UID()
		{
		}

		public PD_UID(string packageID, string processID)
		{
			this.m_sFilePath = packageID;
			this.m_sProcessID = processID;
		}
	}
}