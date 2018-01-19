using EIS.AppBase;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Data;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace EIS.Interface.WebService
{
	public class WebDB
	{
		public static CompilerResults DbRef;

		private static DataServiceSoapClient dataServiceSoapClient_0;

		private static RemoteCaller remoteCaller_0;

		private static EIS.Interface.WebService.ServiceCredential serviceCredential_0;

		private static EIS.Interface.WebService.ServiceCredential MyCredential
		{
			get
			{
				return WebDB.serviceCredential_0;
			}
		}

		public WebDB()
		{
		}

		public static void AddInParameter(SmartCommand command, string name, DbType dbType, object value)
		{
			object obj;
			CommandParameter commandParameter = new CommandParameter()
			{
				ParameterName = name,
				DbType = dbType
			};
			CommandParameter commandParameter1 = commandParameter;
			if (value == null)
			{
				obj = DBNull.Value;
			}
			else
			{
				obj = value;
			}
			commandParameter1.Value = obj;
			command.Parameters.Add(commandParameter);
		}

		public static DataSet ExecuteDataSet(string strSQL)
		{
			return WebDB.dataServiceSoapClient_0.ExecuteDataSet(WebDB.MyCredential, strSQL);
		}

		public static DataSet ExecuteDataSet(SmartCommand command)
		{
			return WebDB.remoteCaller_0.Call<DataSet>("ExecuteDataSet2", command);
		}

		public static void ExecuteNonQuery(string strSQL)
		{
			WebDB.dataServiceSoapClient_0.ExecuteNonQuery(WebDB.MyCredential, strSQL);
		}

		public static int ExecuteNonQuery(SmartCommand command)
		{
			return WebDB.remoteCaller_0.Call<int>("ExecuteNonQuery2", command);
		}

		public static string ExecuteScalar(string strSQL)
		{
			object obj = WebDB.dataServiceSoapClient_0.ExecuteScalar(WebDB.MyCredential, strSQL);
			return ((obj == null ? false : obj != DBNull.Value) ? obj.ToString() : "");
		}

		public static string ExecuteScalar(SmartCommand command)
		{
			object obj = WebDB.remoteCaller_0.Call<object>("ExecuteScalar2", command);
			return ((obj == null ? false : obj != DBNull.Value) ? obj.ToString() : "");
		}

		public static void GetDataService(string string_0)
		{
			BasicHttpBinding basicHttpBinding = new BasicHttpBinding();
            basicHttpBinding.MaxReceivedMessageSize = (long)0x7fffffff;
            basicHttpBinding.MaxBufferPoolSize = (long)0x7fffffff;
            basicHttpBinding.MaxBufferSize = 0x7fffffff;
			EndpointAddress endpointAddress = new EndpointAddress(new Uri(string_0), new AddressHeader[0]);
			WebDB.dataServiceSoapClient_0 = new DataServiceSoapClient(basicHttpBinding, endpointAddress);
			WebDB.remoteCaller_0 = new RemoteCaller(WebDB.dataServiceSoapClient_0);
		}

		public static DataTable GetLimitByEmployeeId(string EmployeeId, string pwbs, string webId)
		{
			DataTable limitByEmployeeId = WebDB.dataServiceSoapClient_0.GetLimitByEmployeeId(WebDB.MyCredential, EmployeeId, pwbs, webId);
			return limitByEmployeeId;
		}

		public static SmartCommand GetSqlStringCommand(string commandText)
		{
			if (string.IsNullOrEmpty(commandText))
			{
				throw new ArgumentException("查询命令不能为空");
			}
			return new SmartCommand()
			{
				CommandType = CommandType.Text,
				CommandText = commandText
			};
		}

		public static string HelloWorld()
		{
			return WebDB.dataServiceSoapClient_0.HelloWorld();
		}

		public static void InitCredential(string loginName, string loginPass)
		{
			if (WebDB.serviceCredential_0 != null)
			{
				WebDB.serviceCredential_0.UserName = loginName;
				WebDB.serviceCredential_0.PassWord = loginPass;
			}
			else
			{
				WebDB.serviceCredential_0 = new EIS.Interface.WebService.ServiceCredential()
				{
					UserName = loginName,
					PassWord = loginPass
				};
			}
		}

		public static string LoginChk(string user, string string_0)
		{
			return WebDB.dataServiceSoapClient_0.UserCheck(user, string_0);
		}

		public static DataSet QueryData(string queryCode, string condStr, string paraList)
		{
			DataSet dataSet = WebDB.dataServiceSoapClient_0.QueryData(WebDB.MyCredential, queryCode, condStr, paraList);
			return dataSet;
		}

		public static string UpdateData(DataTable data, string bizLogicList)
		{
			return WebDB.dataServiceSoapClient_0.UpdateData(WebDB.MyCredential, data, bizLogicList);
		}
	}
}