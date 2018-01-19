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
	[MessageContract(WrapperName="GeneralCallResponse", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
	public class GeneralCallResponse
	{
		[MessageBodyMember(Namespace="http://tempuri.org/", Order=0)]
		[XmlElement(DataType="base64Binary")]
		public byte[] GeneralCallResult;

		public GeneralCallResponse()
		{
		}

		public GeneralCallResponse(byte[] GeneralCallResult)
		{
			this.GeneralCallResult = GeneralCallResult;
		}
	}
}