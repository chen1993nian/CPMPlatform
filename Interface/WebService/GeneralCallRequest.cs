using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.ServiceModel;
using System.Xml.Serialization;

namespace EIS.Interface.WebService
{
	[DebuggerStepThrough]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[GeneratedCode("System.ServiceModel", "4.0.0.0")]
	[MessageContract(WrapperName="GeneralCall", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
	public class GeneralCallRequest
	{
		[MessageBodyMember(Namespace="http://tempuri.org/", Order=0)]
		public string methodName;

		[MessageBodyMember(Namespace="http://tempuri.org/", Order=1)]
		[XmlElement(DataType="base64Binary")]
		public byte[] param;

		public GeneralCallRequest()
		{
		}

		public GeneralCallRequest(string methodName, byte[] param)
		{
			this.methodName = methodName;
			this.param = param;
		}
	}
}