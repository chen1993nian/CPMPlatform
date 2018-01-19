using EIS.AppBase;
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace EIS.Interface.WebService
{
	[DebuggerStepThrough]
	[GeneratedCode("System.ServiceModel", "4.0.0.0")]
	public class DataServiceSoapClient : ClientBase<DataServiceSoap>, DataServiceSoap, IRemoteCall
	{
		public DataServiceSoapClient()
		{
		}

		public DataServiceSoapClient(string endpointConfigurationName) : base(endpointConfigurationName)
		{
		}

		public DataServiceSoapClient(string endpointConfigurationName, string remoteAddress) : base(endpointConfigurationName, remoteAddress)
		{
		}

		public DataServiceSoapClient(string endpointConfigurationName, EndpointAddress remoteAddress) : base(endpointConfigurationName, remoteAddress)
		{
		}

		public DataServiceSoapClient(Binding binding, EndpointAddress remoteAddress) : base(binding, remoteAddress)
		{
		}

		[EditorBrowsable(EditorBrowsableState.Advanced)]
		ExecuteDataSetResponse EIS.Interface.WebService.DataServiceSoap.ExecuteDataSet(ExecuteDataSetRequest request)
		{
			return base.Channel.ExecuteDataSet(request);
		}

		[EditorBrowsable(EditorBrowsableState.Advanced)]
		ExecuteNonQueryResponse EIS.Interface.WebService.DataServiceSoap.ExecuteNonQuery(ExecuteNonQueryRequest request)
		{
			return base.Channel.ExecuteNonQuery(request);
		}

		[EditorBrowsable(EditorBrowsableState.Advanced)]
		ExecuteScalarResponse EIS.Interface.WebService.DataServiceSoap.ExecuteScalar(ExecuteScalarRequest request)
		{
			return base.Channel.ExecuteScalar(request);
		}

		[EditorBrowsable(EditorBrowsableState.Advanced)]
		GeneralCallResponse EIS.Interface.WebService.DataServiceSoap.GeneralCall(GeneralCallRequest request)
		{
			return base.Channel.GeneralCall(request);
		}

		[EditorBrowsable(EditorBrowsableState.Advanced)]
		GetLimitByEmployeeIdResponse EIS.Interface.WebService.DataServiceSoap.GetLimitByEmployeeId(GetLimitByEmployeeIdRequest request)
		{
			return base.Channel.GetLimitByEmployeeId(request);
		}

		[EditorBrowsable(EditorBrowsableState.Advanced)]
		QueryDataResponse EIS.Interface.WebService.DataServiceSoap.QueryData(QueryDataRequest request)
		{
			return base.Channel.QueryData(request);
		}

		[EditorBrowsable(EditorBrowsableState.Advanced)]
		UpdateDataResponse EIS.Interface.WebService.DataServiceSoap.UpdateData(UpdateDataRequest request)
		{
			return base.Channel.UpdateData(request);
		}

		public DataSet ExecuteDataSet(EIS.Interface.WebService.ServiceCredential ServiceCredential, string strcmd)
		{
			ExecuteDataSetRequest executeDataSetRequest = new ExecuteDataSetRequest()
			{
				ServiceCredential = ServiceCredential,
				strcmd = strcmd
			};
			return ((DataServiceSoap)this).ExecuteDataSet(executeDataSetRequest).ExecuteDataSetResult;
		}

		public int ExecuteNonQuery(EIS.Interface.WebService.ServiceCredential ServiceCredential, string strcmd)
		{
			ExecuteNonQueryRequest executeNonQueryRequest = new ExecuteNonQueryRequest()
			{
				ServiceCredential = ServiceCredential,
				strcmd = strcmd
			};
			return ((DataServiceSoap)this).ExecuteNonQuery(executeNonQueryRequest).ExecuteNonQueryResult;
		}

		public object ExecuteScalar(EIS.Interface.WebService.ServiceCredential ServiceCredential, string strcmd)
		{
			ExecuteScalarRequest executeScalarRequest = new ExecuteScalarRequest()
			{
				ServiceCredential = ServiceCredential,
				strcmd = strcmd
			};
			return ((DataServiceSoap)this).ExecuteScalar(executeScalarRequest).ExecuteScalarResult;
		}

		public byte[] GeneralCall(string methodName, byte[] param)
		{
			GeneralCallRequest generalCallRequest = new GeneralCallRequest()
			{
				methodName = methodName,
				param = param
			};
			return ((DataServiceSoap)this).GeneralCall(generalCallRequest).GeneralCallResult;
		}

		public DataTable GetLimitByEmployeeId(EIS.Interface.WebService.ServiceCredential ServiceCredential, string EmployeeId, string pwbs, string webId)
		{
			GetLimitByEmployeeIdRequest getLimitByEmployeeIdRequest = new GetLimitByEmployeeIdRequest()
			{
				ServiceCredential = ServiceCredential,
				EmployeeId = EmployeeId,
				pwbs = pwbs,
				webId = webId
			};
			return ((DataServiceSoap)this).GetLimitByEmployeeId(getLimitByEmployeeIdRequest).GetLimitByEmployeeIdResult;
		}

		public string HelloWorld()
		{
			return base.Channel.HelloWorld();
		}

		public string LoginCheck(string userName, string loginPass)
		{
			return base.Channel.LoginCheck(userName, loginPass);
		}

		public string NewInstanceByLoginName(string workflowCode, string instanceName, string appId, string loginName)
		{
			return base.Channel.NewInstanceByLoginName(workflowCode, instanceName, appId, loginName);
		}

		public string NewWorkFlowInstance(string workflowCode, string instanceName, string appId, string employeeId, string positionId)
		{
			string str = base.Channel.NewWorkFlowInstance(workflowCode, instanceName, appId, employeeId, positionId);
			return str;
		}

		public DataSet QueryData(EIS.Interface.WebService.ServiceCredential ServiceCredential, string queryCode, string condStr, string replacePara)
		{
			QueryDataRequest queryDataRequest = new QueryDataRequest()
			{
				ServiceCredential = ServiceCredential,
				queryCode = queryCode,
				condStr = condStr,
				replacePara = replacePara
			};
			return ((DataServiceSoap)this).QueryData(queryDataRequest).QueryDataResult;
		}

		public string StartWorkflow(string workflowCode, string instanceName, string appId, string employeeId, string positionId)
		{
			string str = base.Channel.StartWorkflow(workflowCode, instanceName, appId, employeeId, positionId);
			return str;
		}

		public string StartWorkflowByLoginName(string workflowCode, string instanceName, string appId, string loginName)
		{
			return base.Channel.StartWorkflowByLoginName(workflowCode, instanceName, appId, loginName);
		}

		public string UpdateData(EIS.Interface.WebService.ServiceCredential ServiceCredential, DataTable data, string bizLogicList)
		{
			UpdateDataRequest updateDataRequest = new UpdateDataRequest()
			{
				ServiceCredential = ServiceCredential,
				data = data,
				bizLogicList = bizLogicList
			};
			return ((DataServiceSoap)this).UpdateData(updateDataRequest).UpdateDataResult;
		}

		public string UserCheck(string userName, string loginPass)
		{
			return base.Channel.UserCheck(userName, loginPass);
		}
	}
}