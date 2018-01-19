using EIS.WorkFlow.XPDL.Utility;
using System;
using System.Collections;

namespace EIS.WorkFlow.XPDLParser.Elements
{
	public class Deadline : BaseElement
	{
		private Node node_0 = NodeFactory.CreateNode();

		private Node node_1 = NodeFactory.CreateNode();

		private string string_0;

		private static Hashtable hashtable_0;

		public Deadline()
		{
			if (Deadline.hashtable_0 == null)
			{
				Deadline.hashtable_0 = new Hashtable()
				{
					{ "ASYNCHR", DeadlineExecution.ASYNCHR },
					{ "SYNCHR", DeadlineExecution.SYNCHR }
				};
			}
		}

		public Node GetDeadlineCondition()
		{
			if (this.node_0.IsEmpty())
			{
				this.method_0();
			}
			return this.node_0;
		}

		public DeadlineExecution GetDeadlineExecution()
		{
			if (!this.IsExecutionSpecified())
			{
				throw new ObjectNotFound("DeadlineExecution is not specified");
			}
			return (DeadlineExecution)Deadline.hashtable_0[this.string_0];
		}

		public Node GetExceptionName()
		{
			if (this.node_1.IsEmpty())
			{
				this.method_0();
			}
			return this.node_1;
		}

		public bool IsExecutionSpecified()
		{
			return base.GetAttribute(this.string_0, out this.string_0, "Execution") != "";
		}

		private void method_0()
		{
			IList lists = null;
			base.GetChildren(lists, out lists);
			foreach (Node node in lists)
			{
				if (!node.GetName().Equals("DeadlineCondition"))
				{
					if (!node.GetName().Equals("ExceptionName"))
					{
						continue;
					}
					this.node_1 = node;
				}
				else
				{
					this.node_0 = node;
				}
			}
		}
	}
}