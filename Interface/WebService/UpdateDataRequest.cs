using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.ServiceModel;

namespace EIS.Interface.WebService
{
	[DebuggerStepThrough]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[GeneratedCode("System.ServiceModel", "4.0.0.0")]
	[MessageContract(WrapperName="UpdateData", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
	public class UpdateDataRequest
	{
		[MessageHeader(Namespace="http://tempuri.org/")]
		public EIS.Interface.WebService.ServiceCredential ServiceCredential;

		[MessageBodyMember(Namespace="http://tempuri.org/", Order=0)]
		public DataTable data;

		[MessageBodyMember(Namespace="http://tempuri.org/", Order=1)]
		public string bizLogicList;

		public UpdateDataRequest()
		{
		}

		public UpdateDataRequest(EIS.Interface.WebService.ServiceCredential ServiceCredential, DataTable data, string bizLogicList)
		{
			this.ServiceCredential = ServiceCredential;
			this.data = data;
			this.bizLogicList = bizLogicList;
		}
	}
}