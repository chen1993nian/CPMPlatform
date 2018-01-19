using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.ServiceModel;

namespace EIS.Interface.WebService
{
	[DebuggerStepThrough]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[GeneratedCode("System.ServiceModel", "4.0.0.0")]
	[MessageContract(WrapperName="ExecuteNonQueryResponse", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
	public class ExecuteNonQueryResponse
	{
		[MessageBodyMember(Namespace="http://tempuri.org/", Order=0)]
		public int ExecuteNonQueryResult;

		public ExecuteNonQueryResponse()
		{
		}

		public ExecuteNonQueryResponse(int ExecuteNonQueryResult)
		{
			this.ExecuteNonQueryResult = ExecuteNonQueryResult;
		}
	}
}