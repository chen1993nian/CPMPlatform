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
	[MessageContract(WrapperName="QueryDataResponse", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
	public class QueryDataResponse
	{
		[MessageBodyMember(Namespace="http://tempuri.org/", Order=0)]
		public DataSet QueryDataResult;

		public QueryDataResponse()
		{
		}

		public QueryDataResponse(DataSet QueryDataResult)
		{
			this.QueryDataResult = QueryDataResult;
		}
	}
}