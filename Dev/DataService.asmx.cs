using EIS.AppBase;
using EIS.AppBase.Config;
using EIS.DataAccess;
using EIS.DataModel.Access;
using EIS.DataModel.Model;
using EIS.DataModel.Service;
using EIS.Permission;
using EIS.Permission.Access;
using EIS.Permission.Model;
using EIS.Permission.Service;
using EIS.WorkFlow.Access;
using EIS.WorkFlow.Engine;
using EIS.WorkFlow.Model;
using EIS.WorkFlow.Service;
using EIS.WorkFlow.XPDLParser;
using EIS.WorkFlow.XPDLParser.Elements;
using NLog;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml;
namespace EIS.WebBase
{
    [ScriptService]
    [ToolboxItem(false)]
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class DataService : System.Web.Services.WebService, IRemoteCall
    {
        private static Logger logger_0;

        public ServiceCredential myCredential;

        static DataService()
        {
            DataService.logger_0 = LogManager.GetCurrentClassLogger();
        }



        [SoapHeader("myCredential", Direction = SoapHeaderDirection.In)]
        [WebMethod(Description = "返回一个数据集", EnableSession = true)]
        public DataSet ExecuteDataSet(string strcmd)
        {
            if (!this.method_6())
            {
                throw new Exception("验证信息不正确，无权访问");
            }
            return SysDatabase.ExecuteDataSet(strcmd);
        }

        [SoapHeader("myCredential", Direction = SoapHeaderDirection.In)]
        [WebMethod(Description = "执行一个查询语句", EnableSession = true)]
        public int ExecuteNonQuery(string strcmd)
        {
            if (!this.method_6())
            {
                throw new Exception("验证信息不正确，无权访问");
            }
            return SysDatabase.ExecuteNonQuery(strcmd);
        }

        [SoapHeader("myCredential", Direction = SoapHeaderDirection.In)]
        [WebMethod(Description = "执行一个查询语句,返回一个单值", EnableSession = true)]
        public object ExecuteScalar(string strcmd)
        {
            if (!this.method_6())
            {
                throw new Exception("验证信息不正确，无权访问");
            }
            return SysDatabase.ExecuteScalar(strcmd);
        }

        [SoapHeader("myCredential", Direction = SoapHeaderDirection.In)]
        [WebMethod(Description = "WebService统一访问接口", EnableSession = true)]
        public byte[] GeneralCall(string methodName, params byte[] param)
        {
            if (!this.method_6())
            {
                throw new Exception("验证信息不正确，无权访问");
            }
            byte[] binary;
            SmartCommand obj = EIS.AppBase.Utility.DeserializeToObject<SmartCommand>(param);
            string lower = methodName.ToLower();
            if (lower == null)
            {
                binary = null;
                return binary;
            }
            else if (lower == "executedataset2")
            {
                binary = EIS.AppBase.Utility.SerializeToBinary(this.method_1(obj));
            }
            else if (lower == "executenonquery2")
            {
                binary = EIS.AppBase.Utility.SerializeToBinary(this.method_0(obj));
            }
            else if (lower == "executescalar2")
            {
                binary = EIS.AppBase.Utility.SerializeToBinary(this.method_2(obj));
            }
            else if (lower == "wf_getinstancexpdl")
            {
                binary = EIS.AppBase.Utility.SerializeToBinary(this.method_3(obj));
            }
            else if (lower == "wf_updateinstancexpdl")
            {
                this.method_4(obj);
                binary = null;
                return binary;
            }
            else
            {
                if (lower != "wf_syncactivityattr")
                {
                    binary = null;
                    return binary;
                }
                binary = EIS.AppBase.Utility.SerializeToBinary(this.method_5(obj));
            }
            return binary;
        }

        public Activity GetActivityById(string xpdl, string actId)
        {
            Activity item = null;
            Package packageFromText = XpdlModel.GetPackageFromText(xpdl);
            IList activities = packageFromText.GetWorkflowProcesses().GetWorkflowProcessById("1").GetActivities().GetActivities();
            for (int i = 0; i < activities.Count; i++)
            {
                item = (Activity)activities[i];
                if (item.GetId() == actId)
                {
                    return item;
                }
            }
            throw new Exception("没有找到相应的节点");
        }

        [SoapHeader("myCredential", Direction = SoapHeaderDirection.In)]
        [WebMethod(Description = "权限查询", EnableSession = true)]
        public DataTable GetLimitByEmployeeId(string EmployeeId, string pwbs, string webId)
        {
            if (!this.method_6())
            {
                throw new Exception("验证信息不正确，无权访问");
            }
            return EIS.Permission.Utility.GetAllLimitDataByEmployeeId(EmployeeId, pwbs, webId);
        }

        [WebMethod(Description = "WebService测试方法", EnableSession = true)]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod(Description = "普通用户登录验证", EnableSession = true)]
        public string LoginCheck(string userName, string loginPass)
        {
            string str = "";
            switch (EIS.Permission.Utility.LoginChk(userName, loginPass))
            {
                case LoginInfoType.Allowed:
                    {
                        str = "";
                        break;
                    }
                case LoginInfoType.NotExist:
                    {
                        str = "用户不存在";
                        break;
                    }
                case LoginInfoType.WrongPwd:
                    {
                        str = "密码不正确";
                        break;
                    }
                case LoginInfoType.IsLocked:
                    {
                        str = "帐户被锁定";
                        break;
                    }
            }
            return str;
        }

        [WebMethod(Description = "普通用户登录验证", EnableSession = true)]
        public string LoginCheckByKey(string loginKey)
        {
            string str = "";
            string str1 = Security.Decrypt(base.Server.UrlDecode(loginKey));
            
            string urlPara = Security.GetUrlPara(str1, "u");
            string urlPara1 = Security.GetUrlPara(str1, "p");
            Security.GetUrlPara(str1, "t");
            switch (EIS.Permission.Utility.LoginChk(urlPara, urlPara1))
            {
                case LoginInfoType.Allowed:
                    {
                        str = "";
                        break;
                    }
                case LoginInfoType.NotExist:
                    {
                        str = "用户不存在";
                        break;
                    }
                case LoginInfoType.WrongPwd:
                    {
                        str = "密码不正确";
                        break;
                    }
                case LoginInfoType.IsLocked:
                    {
                        str = "帐户被锁定";
                        break;
                    }
            }
            return str;
        }

        [WebMethod(Description = "普通用户登录验证", EnableSession = true)]
        public string LoginCheckByRSA(string checkString)
        {
            string str = "";
            switch (EIS.Permission.Utility.LoginChk("", ""))
            {
                case LoginInfoType.Allowed:
                    {
                        str = "";
                        break;
                    }
                case LoginInfoType.NotExist:
                    {
                        str = "用户不存在";
                        break;
                    }
                case LoginInfoType.WrongPwd:
                    {
                        str = "密码不正确";
                        break;
                    }
                case LoginInfoType.IsLocked:
                    {
                        str = "帐户被锁定";
                        break;
                    }
            }
            return str;
        }

        private int method_0(SmartCommand smartCommand_0)
        {
            DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(smartCommand_0.CommandText);
            foreach (CommandParameter parameter in smartCommand_0.Parameters)
            {
                SysDatabase.AddInParameter(sqlStringCommand, parameter.ParameterName, parameter.DbType, parameter.Value);
            }
            return SysDatabase.ExecuteNonQuery(sqlStringCommand);
        }

        private DataSet method_1(SmartCommand smartCommand_0)
        {
            DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(smartCommand_0.CommandText);
            foreach (CommandParameter parameter in smartCommand_0.Parameters)
            {
                SysDatabase.AddInParameter(sqlStringCommand, parameter.ParameterName, parameter.DbType, parameter.Value);
            }
            return SysDatabase.ExecuteDataSet(sqlStringCommand);
        }

        private object method_2(SmartCommand smartCommand_0)
        {
            DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(smartCommand_0.CommandText);
            foreach (CommandParameter parameter in smartCommand_0.Parameters)
            {
                SysDatabase.AddInParameter(sqlStringCommand, parameter.ParameterName, parameter.DbType, parameter.Value);
            }
            return SysDatabase.ExecuteScalar(sqlStringCommand);
        }

        private string method_3(SmartCommand smartCommand_0)
        {
            _Instance __Instance = new _Instance();
            Instance instance = new Instance();
            string str = smartCommand_0.Parameters.Find((CommandParameter commandParameter_0) => commandParameter_0.ParameterName == "_AutoID").Value.ToString();
            return __Instance.GetModelWithXPDL(str).XPDL;
        }

        private void method_4(SmartCommand smartCommand_0)
        {
            _Instance __Instance = new _Instance();
            Instance instance = new Instance()
            {
                InstanceId = smartCommand_0.Parameters.Find((CommandParameter commandParameter_0) => commandParameter_0.ParameterName == "_AutoID").Value.ToString(),
                InstanceName = smartCommand_0.Parameters.Find((CommandParameter commandParameter_0) => commandParameter_0.ParameterName == "InstanceName").Value.ToString(),
                XPDLPath = smartCommand_0.Parameters.Find((CommandParameter commandParameter_0) => commandParameter_0.ParameterName == "XPDLPath").Value.ToString(),
                XPDL = smartCommand_0.Parameters.Find((CommandParameter commandParameter_0) => commandParameter_0.ParameterName == "XPDL").Value.ToString()
            };
            __Instance.UpdateXPDL(instance);
        }

        private string method_5(SmartCommand smartCommand_0)
        {
            XmlNode xmlNodes;
            XmlDocument xmlDocument;
            XmlNode xmlNodes1;
            CommandParameter commandParameter = smartCommand_0.Parameters.Find((CommandParameter commandParameter_0) => commandParameter_0.ParameterName == "WorkflowId");
            CommandParameter commandParameter1 = smartCommand_0.Parameters.Find((CommandParameter commandParameter_0) => commandParameter_0.ParameterName == "NodeId");
            CommandParameter commandParameter2 = smartCommand_0.Parameters.Find((CommandParameter commandParameter_0) => commandParameter_0.ParameterName == "NodeXml");
            CommandParameter commandParameter3 = smartCommand_0.Parameters.Find((CommandParameter commandParameter_0) => commandParameter_0.ParameterName == "NodeType");
            string str = commandParameter3.Value.ToString();
            if (commandParameter == null)
            {
                throw new Exception("缺少 WorkflowId 参数");
            }
            if (commandParameter1 == null)
            {
                throw new Exception("缺少 NodeId 参数");
            }
            if (commandParameter2 == null)
            {
                throw new Exception("缺少 NodeXml 参数");
            }
            string str1 = commandParameter.Value.ToString();
            DataTable dataTable = SysDatabase.ExecuteTable(string.Concat("select * from T_E_WF_Instance where InstanceState='处理中' and WorkflowId='", str1, "' order by _CreateTime"));
            _Instance __Instance = new _Instance();
            Instance instance = new Instance();
            int num = 0;
            foreach (DataRow row in dataTable.Rows)
            {
                string str2 = row["_AutoId"].ToString();
                try
                {
                    instance = __Instance.GetModelWithXPDL(str2);
                    XmlDocument xmlDocument1 = new XmlDocument();
                    xmlDocument1.LoadXml(instance.XPDL);
                    if (str == "1")
                    {
                        xmlNodes = xmlDocument1.SelectSingleNode(string.Concat("//Activity[@Id='", commandParameter1.Value.ToString(), "']"));
                        if (xmlNodes != null)
                        {
                            xmlDocument = new XmlDocument();
                            xmlDocument.LoadXml(commandParameter2.Value.ToString());
                            xmlNodes1 = xmlDocument1.ImportNode(xmlDocument.DocumentElement, true);
                            xmlNodes.ParentNode.ReplaceChild(xmlNodes1, xmlNodes);
                            instance.XPDL = xmlDocument1.OuterXml;
                            __Instance.UpdateXPDL(instance);
                            string innerText = ((XmlElement)xmlNodes.SelectSingleNode("Icon")).InnerText;
                            if ((innerText == "0" || innerText == "1" || innerText == "2" ? true : innerText == "3"))
                            {
                                XmlNode xmlNodes2 = xmlNodes.SelectSingleNode("ExtendedAttributes/ExtendedAttribute[@Name='CanBatch']/@Value");
                                XmlNode xmlNodes3 = xmlNodes.SelectSingleNode("ExtendedAttributes/ExtendedAttribute[@Name='CanRollBack']/@Value");
                                XmlNode xmlNodes4 = xmlNodes.SelectSingleNode("ExtendedAttributes/ExtendedAttribute[@Name='CanAssign']/@Value");
                                string innerText1 = ((XmlElement)xmlNodes.SelectSingleNode("StyleName")).InnerText;
                                XmlNode itemOf = xmlNodes.Attributes["Code"];
                                XmlNode itemOf1 = xmlNodes.Attributes["Name"];
                                XmlNode xmlNodes5 = xmlNodes1.SelectSingleNode("ExtendedAttributes/ExtendedAttribute[@Name='CanBatch']/@Value");
                                XmlNode xmlNodes6 = xmlNodes1.SelectSingleNode("ExtendedAttributes/ExtendedAttribute[@Name='CanRollBack']/@Value");
                                XmlNode xmlNodes7 = xmlNodes1.SelectSingleNode("ExtendedAttributes/ExtendedAttribute[@Name='CanAssign']/@Value");
                                string innerText2 = ((XmlElement)xmlNodes1.SelectSingleNode("StyleName")).InnerText;
                                XmlNode itemOf2 = xmlNodes1.Attributes["Code"];
                                XmlNode itemOf3 = xmlNodes1.Attributes["Name"];
                                StringBuilder stringBuilder = new StringBuilder();
                                if (xmlNodes2.Value != xmlNodes5.Value)
                                {
                                    stringBuilder.AppendFormat("CanBatch='{0}',", xmlNodes5.Value);
                                }
                                if (xmlNodes3.Value != xmlNodes6.Value)
                                {
                                    stringBuilder.AppendFormat("CanRollBack='{0}',", xmlNodes6.Value);
                                }
                                if (xmlNodes4.Value != xmlNodes7.Value)
                                {
                                    stringBuilder.AppendFormat("CanAssign='{0}',", xmlNodes7.Value);
                                }
                                if (innerText1 != innerText2)
                                {
                                    stringBuilder.AppendFormat("NodeStyle='{0}',", innerText2);
                                }
                                if (itemOf.Value != itemOf2.Value)
                                {
                                    stringBuilder.AppendFormat("NodeCode='{0}',", itemOf2.Value);
                                }
                                if (itemOf1.Value != itemOf3.Value)
                                {
                                    stringBuilder.AppendFormat("TaskName='{0}',", itemOf3.Value);
                                }
                                if (stringBuilder.Length > 0)
                                {
                                    stringBuilder.Length = stringBuilder.Length - 1;
                                    stringBuilder.Insert(0, "Update T_E_WF_Task set ");
                                    stringBuilder.AppendFormat(" where InstanceId='{0}' and ActivityId='{1}'", str2, commandParameter1.Value);
                                    SysDatabase.ExecuteNonQuery(stringBuilder.ToString());
                                }
                            }
                        }
                        else
                        {
                            continue;
                        }
                    }
                    else if (str == "2")
                    {
                        xmlNodes = xmlDocument1.SelectSingleNode(string.Concat("//Transition[@Id='", commandParameter1.Value.ToString(), "']"));
                        if (xmlNodes != null)
                        {
                            xmlDocument = new XmlDocument();
                            xmlDocument.LoadXml(commandParameter2.Value.ToString());
                            xmlNodes1 = xmlDocument1.ImportNode(xmlDocument.DocumentElement, true);
                            xmlNodes.ParentNode.ReplaceChild(xmlNodes1, xmlNodes);
                            instance.XPDL = xmlDocument1.OuterXml;
                            __Instance.UpdateXPDL(instance);
                        }
                        else
                        {
                            continue;
                        }
                    }
                    num++;
                }
                catch (Exception exception1)
                {
                    Exception exception = exception1;
                    DataService.logger_0.Error<string, object, object>(string.Concat("批量同步节点属性时出错：", exception.Message, "，参数列表[workflowId={0},NodeId={1},NodeXml={2}]"), str1, commandParameter1.Value, commandParameter2.Value);
                    DataService.logger_0.Error<Exception>(exception);
                }
            }
            string str3 = string.Format("需要同步{0}条，同步成功{1}条", dataTable.Rows.Count, num);
            return str3;
        }

        private bool method_6()
        {
            bool flag;
            if (this.myCredential == null)
            {
                flag = false;
            }
            else if ((this.myCredential.UserName == "" ? false : !(this.myCredential.PassWord == "")))
            {
                if (SysConfig.VerifyToken(this.myCredential.Token))
                {
                    LoginInfoType loginInfoType = UserService.LoginChk(this.myCredential.UserName, this.myCredential.PassWord);
                    bool flag1 = false;
                    switch (loginInfoType)
                    {
                        case LoginInfoType.Allowed:
                            {
                                flag1 = true;
                                break;
                            }
                        case LoginInfoType.NotExist:
                            {
                                flag1 = false;
                                break;
                            }
                        case LoginInfoType.WrongPwd:
                            {
                                flag1 = false;
                                break;
                            }
                        case LoginInfoType.IsLocked:
                            {
                                flag1 = false;
                                break;
                            }
                    }
                    flag = flag1;
                }
                else
                {
                    flag = false;
                }
            }
            else
            {
                flag = false;
            }
            return flag;
        }

        private void method_7(DbCommand dbCommand_0, string string_0, DataRow dataRow_0)
        {
            DbParameter string0 = dbCommand_0.CreateParameter();
            string0.ParameterName = string_0;
            object obj = null;
            obj = (dataRow_0.RowState != DataRowState.Deleted ? dataRow_0[string_0] : dataRow_0[string_0, DataRowVersion.Original]);
            string name = dataRow_0.Table.Columns[string_0].DataType.Name;
            if (name != null)
            {
                if (name == "String")
                {
                    string0.DbType = DbType.String;
                    string0.Value = obj.ToString();
                }
                else if (name == "Int32")
                {
                    string0.DbType = DbType.Int32;
                    string0.Value = (obj.ToString() == "" ? 0 : Convert.ToInt32(obj));
                }
                else if (name == "Double" || name == "Decimal" || name == "Float")
                {
                    string0.DbType = DbType.Decimal;
                    string0.Value = (obj.ToString() == "" ? new decimal(0) : Convert.ToDecimal(obj));
                }
                else if (name == "DateTime")
                {
                    string0.DbType = DbType.DateTime;
                    if (obj.ToString() != "")
                    {
                        string0.Value = Convert.ToDateTime(obj.ToString());
                    }
                    else
                    {
                        string0.Value = DBNull.Value;
                    }
                }
            }
            dbCommand_0.Parameters.Add(string0);
        }

        [WebMethod(Description = "使用指定帐户的默认岗位，新建流程，但不向后流转，任务在发起人待办", EnableSession = false)]
        public string NewInstanceByLoginName(string workflowCode, string instanceName, string appId, string loginName)
        {
            string str;
            string str1 = "";
            string defaultPositionById = "";
            Employee modelByLoginName = EmployeeService.GetModelByLoginName(loginName);
            if (!(modelByLoginName == null ? false : modelByLoginName._IsDel != 1))
            {
                str = "指定用户不存在，或者逻辑删除";
            }
            else if (modelByLoginName.IsLocked != "是")
            {
                str1 = modelByLoginName._AutoID;
                defaultPositionById = EmployeeService.GetDefaultPositionById(modelByLoginName._AutoID)._AutoID;
                str = this.NewWorkFlowInstance(workflowCode, instanceName, appId, str1, defaultPositionById);
            }
            else
            {
                str = "指定用户已被锁定";
            }
            return str;
        }

        [WebMethod(Description = "新建流程，但不向后流转，任务在发起人待办", EnableSession = false)]
        public string NewWorkFlowInstance(string workflowCode, string instanceName, string appId, string employeeId, string positionId)
        {
            string message;
            string str = "";
            Define workflowByCode = DefineService.GetWorkflowByCode(workflowCode);
            string appNames = workflowByCode.AppNames;
            string workflowId = workflowByCode.WorkflowId;
            if ((string.IsNullOrEmpty(appId) ? true : !InstanceService.IsRunAlready(appNames, appId)))
            {
                UserContext userContext = new UserContext();
                Employee model = (new _Employee()).GetModel(employeeId);
                DeptEmployee deptEmployeeByPositionId = DeptEmployeeService.GetDeptEmployeeByPositionId(employeeId, positionId);
                Department department = DepartmentService.GetModel(deptEmployeeByPositionId.DeptID);
                userContext.LoginName = model.LoginName;
                userContext.EmployeeId = employeeId;
                userContext.EmployeeName = model.EmployeeName;
                Department companyByDeptId = DepartmentService.GetCompanyByDeptId(department._AutoID);
                userContext.CompanyId = companyByDeptId._AutoID;
                userContext.CompanyCode = companyByDeptId.DeptCode;
                userContext.CompanyWbs = companyByDeptId.DeptWBS;
                userContext.CompanyName = companyByDeptId.DeptName;
                userContext.DeptId = department._AutoID;
                userContext.DeptWbs = department.DeptWBS;
                userContext.DeptName = department.DeptName;
                userContext.PositionId = deptEmployeeByPositionId.PositionId;
                userContext.PositionName = deptEmployeeByPositionId.PositionName;
                userContext.WebId = AppSettings.Instance.WebId;
                DriverEngine driverEngine = new DriverEngine(userContext);
                Instance instance = new Instance()
                {
                    InstanceId = Guid.NewGuid().ToString(),
                    _UserName = employeeId,
                    _OrgCode = userContext.DeptWbs,
                    _CreateTime = DateTime.Now,
                    _UpdateTime = DateTime.Now,
                    _IsDel = 0,
                    InstanceName = instanceName,
                    WorkflowId = workflowId,
                    DeptId = userContext.DeptId,
                    DeptName = userContext.DeptName,
                    CompanyId = userContext.CompanyId,
                    CompanyName = userContext.CompanyName,
                    EmployeeName = model.EmployeeName,
                    AppId = appId,
                    AppName = appNames,
                    Importance = "1",
                    Remark = str
                };
                DbConnection dbConnection = SysDatabase.CreateConnection();
                try
                {
                    Task task = new Task();
                    dbConnection.Open();
                    DbTransaction dbTransaction = dbConnection.BeginTransaction();
                    try
                    {
                        try
                        {
                            driverEngine.NewWorkFlowInstance(instance, dbTransaction);
                            driverEngine.StartWorkFlowInstance(instance, str, false, dbTransaction);
                            driverEngine.UpdateAppDataState(instance, InstanceState.Processing, dbTransaction);
                            dbTransaction.Commit();
                        }
                        catch (Exception exception1)
                        {
                            Exception exception = exception1;
                            dbTransaction.Rollback();
                            message = exception.Message;
                            return message;
                        }
                    }
                    finally
                    {
                        dbConnection.Close();
                    }
                    message = "ok";
                }
                finally
                {
                    if (dbConnection != null)
                    {
                        ((IDisposable)dbConnection).Dispose();
                    }
                }
            }
            else
            {
                message = "每条业务数据只能发起一支流程";
            }
            return message;
        }

        [SoapHeader("myCredential", Direction = SoapHeaderDirection.In)]
        [WebMethod(Description = "查询平台定义的数据集", EnableSession = true)]
        public DataSet QueryData(string queryCode, string condStr, string replacePara)
        {
            if (!this.method_6())
            {
                throw new Exception("验证信息不正确，无权访问");
            }
            if (string.IsNullOrEmpty(queryCode))
            {
                throw new Exception("queryCode参数有错，不能为空！");
            }
            string str = string.Concat("select TableType,ListSQL,ConnectionId from T_E_Sys_TableInfo where tablename='", queryCode, "'");
            DataTable dataTable = SysDatabase.ExecuteTable(str);
            if (dataTable.Rows.Count <= 0)
            {
                throw new Exception("queryCode参数有错，找不到定义信息！");
            }
            DataRow item = dataTable.Rows[0];
            item["TableType"].ToString();
            string str1 = item["ConnectionId"].ToString();
            string str2 = item["ListSQL"].ToString();
            if (string.IsNullOrEmpty(condStr))
            {
                condStr = " 1=1 ";
            }
            str2 = str2.Replace("|^condition^|", condStr.Replace("[QUOTES]", "'")).Replace("|^sortdir^|", "").Replace("\r\n", " ").Replace("\t", "");
            if (!string.IsNullOrEmpty(replacePara))
            {
                str2 = EIS.AppBase.Utility.ReplaceParaValues(str2, replacePara);
            }
            DataSet dataSet = null;
            if (str1.Trim() == "")
            {
                dataSet = SysDatabase.ExecuteDataSet(str2);
            }
            else
            {
                CustomDb customDb = new CustomDb();
                customDb.CreateDatabaseByConnectionId(str1.Trim());
                dataSet = customDb.ExecuteDataSet(str2);
            }
            return dataSet;
        }

        [WebMethod(Description = "新建流程并向后流转，外部系统调用", EnableSession = false)]
        public string StartWorkflow(string workflowCode, string instanceName, string appId, string employeeId, string positionId)
        {
            string message;
            string str = "";
            Define workflowByCode = DefineService.GetWorkflowByCode(workflowCode);
            string appNames = workflowByCode.AppNames;
            string workflowId = workflowByCode.WorkflowId;
            if ((string.IsNullOrEmpty(appId) ? true : !InstanceService.IsRunAlready(appNames, appId)))
            {
                UserContext userContext = new UserContext();
                Employee model = (new _Employee()).GetModel(employeeId);
                DeptEmployee deptEmployeeByPositionId = DeptEmployeeService.GetDeptEmployeeByPositionId(employeeId, positionId);
                Department department = DepartmentService.GetModel(deptEmployeeByPositionId.DeptID);
                userContext.LoginName = model.LoginName;
                userContext.EmployeeId = employeeId;
                userContext.EmployeeName = model.EmployeeName;
                Department companyByDeptId = DepartmentService.GetCompanyByDeptId(department._AutoID);
                userContext.CompanyId = companyByDeptId._AutoID;
                userContext.CompanyCode = companyByDeptId.DeptCode;
                userContext.CompanyWbs = companyByDeptId.DeptWBS;
                userContext.CompanyName = companyByDeptId.DeptName;
                userContext.DeptId = department._AutoID;
                userContext.DeptWbs = department.DeptWBS;
                userContext.DeptName = department.DeptName;
                userContext.PositionId = deptEmployeeByPositionId.PositionId;
                userContext.PositionName = deptEmployeeByPositionId.PositionName;
                userContext.WebId = AppSettings.Instance.WebId;
                DriverEngine driverEngine = new DriverEngine(userContext);
                Instance instance = new Instance()
                {
                    InstanceId = Guid.NewGuid().ToString(),
                    _UserName = employeeId,
                    _OrgCode = userContext.DeptWbs,
                    _CreateTime = DateTime.Now,
                    _UpdateTime = DateTime.Now,
                    _IsDel = 0,
                    InstanceName = instanceName,
                    WorkflowId = workflowId,
                    DeptId = userContext.DeptId,
                    DeptName = userContext.DeptName,
                    CompanyId = userContext.CompanyId,
                    CompanyName = userContext.CompanyName,
                    EmployeeName = model.EmployeeName,
                    AppId = appId,
                    AppName = appNames,
                    Importance = "1",
                    Remark = str
                };
                DbConnection dbConnection = SysDatabase.CreateConnection();
                try
                {
                    Task task = new Task();
                    dbConnection.Open();
                    DbTransaction dbTransaction = dbConnection.BeginTransaction();
                    try
                    {
                        try
                        {
                            driverEngine.NewWorkFlowInstance(instance, dbTransaction);
                            task = driverEngine.StartWorkFlowInstance(instance, str, dbTransaction);
                            List<Task> tasks = driverEngine.NextTask(instance, task, dbTransaction);
                            if ((new _TableInfo(appNames)).GetModel().TableType == 1)
                            {
                                driverEngine.UpdateAppDataState(instance, InstanceState.Processing, dbTransaction);
                            }
                            if (instance.NeedUpdate)
                            {
                                (new _Instance(dbTransaction)).UpdateXPDL(instance);
                            }
                            if (tasks.Count > 0)
                            {
                                UserTask userTaskByTaskId = EIS.WorkFlow.Engine.Utility.GetUserTaskByTaskId(task.TaskId, employeeId, dbTransaction);
                                StringCollection stringCollections = new StringCollection();
                                foreach (Task task1 in tasks)
                                {
                                    TaskService.GetTaskDealUser(task1.TaskId, stringCollections, dbTransaction);
                                }
                                userTaskByTaskId.RecIds = EIS.AppBase.Utility.GetJoinString(stringCollections);
                                userTaskByTaskId.RecNames = EmployeeService.GetEmployeeNameList(stringCollections);
                                (new _UserTask(dbTransaction)).UpdateAdvice(userTaskByTaskId);
                            }
                            dbTransaction.Commit();
                        }
                        catch (Exception exception1)
                        {
                            Exception exception = exception1;
                            dbTransaction.Rollback();
                            message = exception.Message;
                            return message;
                        }
                    }
                    finally
                    {
                        dbConnection.Close();
                    }
                    message = "ok";
                }
                finally
                {
                    if (dbConnection != null)
                    {
                        ((IDisposable)dbConnection).Dispose();
                    }
                }
            }
            else
            {
                message = "每条业务数据只能发起一支流程";
            }
            return message;
        }

        [WebMethod(Description = "使用指定帐户的默认岗位，新建流程并向后流转", EnableSession = false)]
        public string StartWorkflowByLoginName(string workflowCode, string instanceName, string appId, string loginName)
        {
            string str;
            string str1 = "";
            string defaultPositionById = "";
            Employee modelByLoginName = EmployeeService.GetModelByLoginName(loginName);
            if (!(modelByLoginName == null ? false : modelByLoginName._IsDel != 1))
            {
                str = "指定用户不存在，或者逻辑删除";
            }
            else if (modelByLoginName.IsLocked != "是")
            {
                str1 = modelByLoginName._AutoID;
                defaultPositionById = EmployeeService.GetDefaultPositionById(modelByLoginName._AutoID)._AutoID;
                str = this.StartWorkflow(workflowCode, instanceName, appId, str1, defaultPositionById);
            }
            else
            {
                str = "指定用户已被锁定";
            }
            return str;
        }

        [SoapHeader("myCredential", Direction = SoapHeaderDirection.In)]
        [WebMethod(Description = "通过平台定义的逻辑更新数据", EnableSession = true)]
        public string UpdateData(DataTable data, string bizLogicList)
        {
            Match match = null;
            string value;
            DbCommand sqlStringCommand;
            string str;
            Regex regex;
            if (!this.method_6())
            {
                throw new Exception("验证信息不正确，无权访问");
            }
            if (string.IsNullOrEmpty(bizLogicList))
            {
                throw new Exception("bizLogicList参数有错，不能为空！");
            }
            string[] strArrays = bizLogicList.Split(new char[] { '|' });
            try
            {
                DataService.logger_0.Debug(string.Concat("共接收到", data.Rows.Count, "条数据"));
                string tableSQLScript = "";
                string tableSQLScript1 = "";
                string str1 = "";
                if (strArrays[0].Length > 0)
                {
                    tableSQLScript = TableService.GetTableSQLScript(strArrays[0]);
                }
                if (strArrays[1].Length > 0)
                {
                    tableSQLScript1 = TableService.GetTableSQLScript(strArrays[1]);
                }
                if (strArrays[2].Length > 0)
                {
                    str1 = TableService.GetTableSQLScript(strArrays[2]);
                }
                int num = 0;
                foreach (DataRow row in data.Rows)
                {
                    int num1 = num;
                    num = num1 + 1;
                    int num2 = num1;
                    DataService.logger_0.Debug(string.Concat("开始执行第", num2.ToString(), "条命令"));
                    if (row.RowState == DataRowState.Added)
                    {
                        str = EIS.AppBase.Utility.ReplaceWithDataRow(tableSQLScript, row);
                        sqlStringCommand = SysDatabase.GetSqlStringCommand(str);
                        regex = new Regex("@(\\w*)", RegexOptions.IgnoreCase);
                        foreach (Match match1 in regex.Matches(str))
                        {
                            value = match1.Groups[1].Value;
                            if (sqlStringCommand.Parameters.Contains(value))
                            {
                                continue;
                            }
                            this.method_7(sqlStringCommand, value, row);
                        }
                        SysDatabase.ExecuteNonQuery(sqlStringCommand);
                    }
                    else if (row.RowState != DataRowState.Modified)
                    {
                        if (row.RowState != DataRowState.Deleted)
                        {
                            continue;
                        }
                        str = EIS.AppBase.Utility.ReplaceWithDataRow(str1, row);
                        sqlStringCommand = SysDatabase.GetSqlStringCommand(str);
                        regex = new Regex("@(\\w*)", RegexOptions.IgnoreCase);
                        foreach (Match matchA in regex.Matches(str))
                        {
                            value = matchA.Groups[1].Value;
                            if (sqlStringCommand.Parameters.Contains(value))
                            {
                                continue;
                            }
                            this.method_7(sqlStringCommand, value, row);
                        }
                        SysDatabase.ExecuteNonQuery(sqlStringCommand);
                    }
                    else
                    {
                        str = EIS.AppBase.Utility.ReplaceWithDataRow(tableSQLScript1, row);
                        sqlStringCommand = SysDatabase.GetSqlStringCommand(str);
                        regex = new Regex("@(\\w*)", RegexOptions.IgnoreCase);
                        foreach (Match match2 in regex.Matches(str))
                        {
                            value = match2.Groups[1].Value;
                            if (sqlStringCommand.Parameters.Contains(value))
                            {
                                continue;
                            }
                            this.method_7(sqlStringCommand, value, row);
                        }
                        SysDatabase.ExecuteNonQuery(sqlStringCommand);
                    }
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return "";
        }

        [WebMethod(Description = "后台用户登录验证", EnableSession = true)]
        public string UserCheck(string userName, string loginPass)
        {
            string str = "";
            try
            {
                switch (UserService.LoginChk(userName, loginPass))
                {
                    case LoginInfoType.Allowed:
                        {
                            str = "";
                            break;
                        }
                    case LoginInfoType.NotExist:
                        {
                            str = "用户不存在";
                            break;
                        }
                    case LoginInfoType.WrongPwd:
                        {
                            str = "密码不正确";
                            break;
                        }
                    case LoginInfoType.IsLocked:
                        {
                            str = "帐户被锁定";
                            break;
                        }
                }
            }
            catch (Exception ex)
            {
                Exception exception = ex;
           
                DataService.logger_0.Error<Exception>(exception);
            }
            return str;
        }
    }
}
