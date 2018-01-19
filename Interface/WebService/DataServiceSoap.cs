using System;
using System.CodeDom.Compiler;
using System.ServiceModel;

namespace EIS.Interface.WebService
{
	[GeneratedCode("System.ServiceModel", "4.0.0.0")]
	[ServiceContract(ConfigurationName="DataServiceRef.DataServiceSoap")]
	public interface DataServiceSoap
	{
		[OperationContract(Action="http://tempuri.org/ExecuteDataSet", ReplyAction="*")]
		[XmlSerializerFormat(SupportFaults=true)]
		ExecuteDataSetResponse ExecuteDataSet(ExecuteDataSetRequest request);

		[OperationContract(Action="http://tempuri.org/ExecuteNonQuery", ReplyAction="*")]
		[XmlSerializerFormat(SupportFaults=true)]
		ExecuteNonQueryResponse ExecuteNonQuery(ExecuteNonQueryRequest request);

		[OperationContract(Action="http://tempuri.org/ExecuteScalar", ReplyAction="*")]
		[XmlSerializerFormat(SupportFaults=true)]
		ExecuteScalarResponse ExecuteScalar(ExecuteScalarRequest request);

		[OperationContract(Action="http://tempuri.org/GeneralCall", ReplyAction="*")]
		[XmlSerializerFormat(SupportFaults=true)]
		GeneralCallResponse GeneralCall(GeneralCallRequest request);

		[OperationContract(Action="http://tempuri.org/GetLimitByEmployeeId", ReplyAction="*")]
		[XmlSerializerFormat(SupportFaults=true)]
		GetLimitByEmployeeIdResponse GetLimitByEmployeeId(GetLimitByEmployeeIdRequest request);

		[OperationContract(Action="http://tempuri.org/HelloWorld", ReplyAction="*")]
		[XmlSerializerFormat(SupportFaults=true)]
		string HelloWorld();

		[OperationContract(Action="http://tempuri.org/LoginCheck", ReplyAction="*")]
		[XmlSerializerFormat(SupportFaults=true)]
		string LoginCheck(string userName, string loginPass);

		[OperationContract(Action="http://tempuri.org/NewInstanceByLoginName", ReplyAction="*")]
		[XmlSerializerFormat(SupportFaults=true)]
		string NewInstanceByLoginName(string workflowCode, string instanceName, string appId, string loginName);

		[OperationContract(Action="http://tempuri.org/NewWorkFlowInstance", ReplyAction="*")]
		[XmlSerializerFormat(SupportFaults=true)]
		string NewWorkFlowInstance(string workflowCode, string instanceName, string appId, string employeeId, string positionId);

		[OperationContract(Action="http://tempuri.org/QueryData", ReplyAction="*")]
		[XmlSerializerFormat(SupportFaults=true)]
		QueryDataResponse QueryData(QueryDataRequest request);

		[OperationContract(Action="http://tempuri.org/StartWorkflow", ReplyAction="*")]
		[XmlSerializerFormat(SupportFaults=true)]
		string StartWorkflow(string workflowCode, string instanceName, string appId, string employeeId, string positionId);

		[OperationContract(Action="http://tempuri.org/StartWorkflowByLoginName", ReplyAction="*")]
		[XmlSerializerFormat(SupportFaults=true)]
		string StartWorkflowByLoginName(string workflowCode, string instanceName, string appId, string loginName);

		[OperationContract(Action="http://tempuri.org/UpdateData", ReplyAction="*")]
		[XmlSerializerFormat(SupportFaults=true)]
		UpdateDataResponse UpdateData(UpdateDataRequest request);

		[OperationContract(Action="http://tempuri.org/UserCheck", ReplyAction="*")]
		[XmlSerializerFormat(SupportFaults=true)]
		string UserCheck(string userName, string loginPass);
	}
}