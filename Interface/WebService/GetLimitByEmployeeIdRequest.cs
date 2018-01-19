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
	[MessageContract(WrapperName="GetLimitByEmployeeId", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
	public class GetLimitByEmployeeIdRequest
	{
		[MessageHeader(Namespace="http://tempuri.org/")]
		public EIS.Interface.WebService.ServiceCredential ServiceCredential;

		[MessageBodyMember(Namespace="http://tempuri.org/", Order=0)]
		public string EmployeeId;

		[MessageBodyMember(Namespace="http://tempuri.org/", Order=1)]
		public string pwbs;

		[MessageBodyMember(Namespace="http://tempuri.org/", Order=2)]
		public string webId;

		public GetLimitByEmployeeIdRequest()
		{
		}

		public GetLimitByEmployeeIdRequest(EIS.Interface.WebService.ServiceCredential ServiceCredential, string EmployeeId, string pwbs, string webId)
		{
			this.ServiceCredential = ServiceCredential;
			this.EmployeeId = EmployeeId;
			this.pwbs = pwbs;
			this.webId = webId;
		}
	}
}