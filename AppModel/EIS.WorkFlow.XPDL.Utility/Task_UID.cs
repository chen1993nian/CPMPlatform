using System;

namespace EIS.WorkFlow.XPDL.Utility
{
	[Serializable]
	public class Task_UID
	{
		private string actid = "";

		private string piid = "";

		private string taskid = "";

		public string ActID
		{
			get
			{
				return this.actid;
			}
			set
			{
				this.actid = value;
			}
		}

		public string PIID
		{
			get
			{
				return this.piid;
			}
			set
			{
				this.piid = value;
			}
		}

		public string TaskID
		{
			get
			{
				return this.taskid;
			}
			set
			{
				this.taskid = value;
			}
		}

		public Task_UID()
		{
		}

		public Task_UID(string piid, string actid, string taskid)
		{
			this.piid = piid;
			this.actid = actid;
			this.taskid = taskid;
		}

		public override bool Equals(object object_0)
		{
			bool flag;
			Task_UID object0 = object_0 as Task_UID;
			if (object0 == null)
			{
				flag = false;
			}
			else
			{
				flag = (!this.PIID.Equals(object0.PIID) || !this.ActID.Equals(object0.ActID) ? false : this.TaskID.Equals(object0.TaskID));
			}
			return flag;
		}

		public override int GetHashCode()
		{
			int hashCode = this.PIID.GetHashCode() * 3 + this.ActID.GetHashCode() * 13 + this.TaskID.GetHashCode() * 23;
			return hashCode;
		}
	}
}