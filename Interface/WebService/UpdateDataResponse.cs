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
	[MessageContract(WrapperName="UpdateDataResponse", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
	public class UpdateDataResponse
	{
		[MessageBodyMember(Namespace="http://tempuri.org/", Order=0)]
		public string UpdateDataResult;

		public UpdateDataResponse()
		{
		}

		public UpdateDataResponse(string UpdateDataResult)
		{
			this.UpdateDataResult = UpdateDataResult;
		}
	}
}