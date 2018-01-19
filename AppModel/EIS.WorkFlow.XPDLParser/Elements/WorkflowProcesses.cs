using System;
using System.Collections;

namespace EIS.WorkFlow.XPDLParser.Elements
{
	public class WorkflowProcesses : BaseElement
	{
		private IList ilist_0;

		public WorkflowProcesses()
		{
		}

		public WorkflowProcess GetWorkflowProcessById(string Id)
		{
			WorkflowProcess workflowProcess;
			if (null == this.ilist_0)
			{
				this.GetWorkflowProcesses();
			}
			IEnumerator enumerator = this.ilist_0.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					WorkflowProcess current = (WorkflowProcess)enumerator.Current;
					if (!current.GetId().Equals(Id))
					{
						continue;
					}
					workflowProcess = current;
					return workflowProcess;
				}
				throw new ObjectNotFound(string.Concat("没有找到 Id为：", Id, "的WorkflowProcess节！"));
			}
			finally
			{
				IDisposable disposable = enumerator as IDisposable;
				if (disposable != null)
				{
					disposable.Dispose();
				}
			}
			return workflowProcess;
		}

		public IList GetWorkflowProcesses()
		{
			IList children = base.GetChildren(this.ilist_0, out this.ilist_0, typeof(WorkflowProcess), "WorkflowProcess");
			return children;
		}
	}
}