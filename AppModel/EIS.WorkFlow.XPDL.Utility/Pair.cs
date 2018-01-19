using System;

namespace EIS.WorkFlow.XPDL.Utility
{
	[Serializable]
	public sealed class Pair
	{
		private object first;

		private object second;

		public object First
		{
			get
			{
				return this.first;
			}
		}

		public object Second
		{
			get
			{
				return this.second;
			}
		}

		public Pair(object first, object second)
		{
			this.first = first;
			this.second = second;
		}

		public override bool Equals(object object_0)
		{
			bool flag;
			Pair object0 = object_0 as Pair;
			if (object0 != null)
			{
				flag = (!object0.First.Equals(this.First) ? false : object0.Second.Equals(this.Second));
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		public override int GetHashCode()
		{
			int hashCode = this.First.GetHashCode() * 7 + this.Second.GetHashCode();
			return hashCode;
		}
	}
}