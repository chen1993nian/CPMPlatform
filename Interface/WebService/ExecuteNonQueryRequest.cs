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
	[MessageContract(WrapperName="ExecuteNonQuery", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
	public class ExecuteNonQueryRequest
	{
		[MessageHeader(Namespace="http://tempuri.org/")]
		public EIS.Interface.WebService.ServiceCredential ServiceCredential;

		[MessageBodyMember(Namespace="http://tempuri.org/", Order=0)]
		public string strcmd;

		public ExecuteNonQueryRequest()
		{
		}

		public ExecuteNonQueryRequest(EIS.Interface.WebService.ServiceCredential ServiceCredential, string strcmd)
		{
			this.ServiceCredential = ServiceCredential;
			this.strcmd = strcmd;
		}
	}
}