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
	[MessageContract(WrapperName="ExecuteScalarResponse", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
	public class ExecuteScalarResponse
	{
		[MessageBodyMember(Namespace="http://tempuri.org/", Order=0)]
		public object ExecuteScalarResult;

		public ExecuteScalarResponse()
		{
		}

		public ExecuteScalarResponse(object ExecuteScalarResult)
		{
			this.ExecuteScalarResult = ExecuteScalarResult;
		}
	}
}