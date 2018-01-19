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
	[MessageContract(WrapperName="QueryData", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
	public class QueryDataRequest
	{
		[MessageHeader(Namespace="http://tempuri.org/")]
		public EIS.Interface.WebService.ServiceCredential ServiceCredential;

		[MessageBodyMember(Namespace="http://tempuri.org/", Order=0)]
		public string queryCode;

		[MessageBodyMember(Namespace="http://tempuri.org/", Order=1)]
		public string condStr;

		[MessageBodyMember(Namespace="http://tempuri.org/", Order=2)]
		public string replacePara;

		public QueryDataRequest()
		{
		}

		public QueryDataRequest(EIS.Interface.WebService.ServiceCredential ServiceCredential, string queryCode, string condStr, string replacePara)
		{
			this.ServiceCredential = ServiceCredential;
			this.queryCode = queryCode;
			this.condStr = condStr;
			this.replacePara = replacePara;
		}
	}
}