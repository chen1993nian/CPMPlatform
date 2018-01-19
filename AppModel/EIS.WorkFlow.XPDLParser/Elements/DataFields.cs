using System;
using System.Collections;

namespace EIS.WorkFlow.XPDLParser.Elements
{
	public class DataFields : BaseElement
	{
		private IList ilist_0;

		public DataFields()
		{
		}

		public DataField GetDataFieldById(string Id)
		{
			DataField dataField;
			if (null == this.ilist_0)
			{
				this.GetDataFields();
			}
			IEnumerator enumerator = this.ilist_0.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					DataField current = (DataField)enumerator.Current;
					if (!current.GetId().Equals(Id))
					{
						continue;
					}
					dataField = current;
					return dataField;
				}
				throw new ObjectNotFound(string.Concat("DataField Not Found! Id=", Id));
			}
			finally
			{
				IDisposable disposable = enumerator as IDisposable;
				if (disposable != null)
				{
					disposable.Dispose();
				}
			}
			return dataField;
		}

		public IList GetDataFields()
		{
			IList children = base.GetChildren(this.ilist_0, out this.ilist_0, typeof(DataField), "DataField");
			return children;
		}
	}
}