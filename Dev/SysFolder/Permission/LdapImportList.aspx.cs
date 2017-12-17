using AjaxPro;
using EIS.AppBase;
using EIS.AppBase.Config;
using EIS.DataAccess;
using EIS.Permission.Access;
using EIS.Permission.Model;
using EIS.Permission.Service;
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Data;
using System.Data.Common;
using System.DirectoryServices;
using System.Web.UI.HtmlControls;

namespace Studio.JZY.SysFolder.Permission
{
    public partial class LdapImportList : AdminPageBase
    {
        public string treedata = "";

        private DirectoryEntry directoryEntry_0 = null;

       
      
        [AjaxMethod(HttpSessionStateRequirement.Read)]
        public void ImportUser(string posId, string oupath, string keys)
        {
            StringCollection stringCollections = new StringCollection();
            char[] chrArray = new char[] { ',' };
            stringCollections.AddRange(keys.Split(chrArray));
            DbConnection dbConnection = SysDatabase.CreateConnection();
            dbConnection.Open();
            DbTransaction dbTransaction = dbConnection.BeginTransaction();
            _Employee __Employee = new _Employee(dbTransaction);
            _DeptEmployee __DeptEmployee = new _DeptEmployee(dbTransaction);
            try
            {
                try
                {
                    LDAPConfig ldapConfig = AppSettings.Instance.LdapConfig;
                    string[] serverIP = new string[] { "LDAP://", ldapConfig.ServerIP, ":", ldapConfig.ServerPort, "/", oupath };
                    oupath = string.Concat(serverIP);
                    DirectoryEntry directoryEntry = new DirectoryEntry(oupath, ldapConfig.Account, ldapConfig.PassWord);
                    if ((directoryEntry == null ? false : directoryEntry.Children != null))
                    {
                        DirectorySearcher directorySearcher = new DirectorySearcher();
                        foreach (string stringCollection in stringCollections)
                        {
                            string str = this.method_2(stringCollection);
                            directorySearcher.SearchRoot = directoryEntry;
                            directorySearcher.SearchScope = SearchScope.OneLevel;
                            directorySearcher.Filter = string.Concat("(&(objectClass=user)(objectGUID=", str, "))");
                            SearchResult searchResult = directorySearcher.FindOne();
                            if (searchResult == null)
                            {
                                continue;
                            }
                            DirectoryEntry directoryEntry1 = searchResult.GetDirectoryEntry();
                            directoryEntry1.Properties["name"].Value.ToString();
                            string str1 = directoryEntry1.Properties["sAMAccountName"].Value.ToString();
                            string str2 = directoryEntry1.Properties["displayName"].Value.ToString();
                            directoryEntry1.Properties["distinguishedName"].Value.ToString();
                            if (__Employee.IsADUserExist(stringCollection))
                            {
                                continue;
                            }
                            Employee employee = new Employee()
                            {
                                _AutoID = Guid.NewGuid().ToString(),
                                _OrgCode = base.OrgCode,
                                _CreateTime = DateTime.Now,
                                _UpdateTime = DateTime.Now,
                                _IsDel = 0,
                                _UserName = base.EmployeeID,
                                OrderID = 0,
                                LoginName = str1,
                                LoginPass = Security.Encrypt(AppSettings.Instance.EmployeeDefaultPass),
                                EmployeeCode = "",
                                EmployeeName = str2,
                                EMail = "",
                                Cellphone = "",
                                EmployeeType = "正式",
                                EmployeeState = "在职",
                                SID = stringCollection,
                                UserType = "AD",
                                SysUser = 0,
                                IsLocked = "否",
                                LockReason = ""
                            };
                            if (__Employee.IsLoginExist(str1, employee._AutoID))
                            {
                                throw new Exception(string.Concat("已经存在登录账号：", str1));
                            }
                            __Employee.Add(employee);
                            DeptEmployee deptEmployee = new DeptEmployee()
                            {
                                _AutoID = Guid.NewGuid().ToString(),
                                _OrgCode = "",
                                _UserName = base.UserName,
                                _CreateTime = DateTime.Now,
                                _UpdateTime = DateTime.Now,
                                _IsDel = 0
                            };
                            Position positionById = PositionService.GetPositionById(posId);
                            Department model = DepartmentService.GetModel(positionById.DeptID);
                            deptEmployee.DeptID = model._AutoID;
                            deptEmployee.DeptName = model.DeptName;
                            deptEmployee.CompanyID = model.CompanyID;
                            deptEmployee.EmployeeID = employee._AutoID;
                            deptEmployee.EmployeeName = employee.EmployeeName;
                            deptEmployee.PositionId = positionById._AutoID;
                            deptEmployee.PositionName = positionById.PositionName;
                            deptEmployee.IsDefault = 1;
                            deptEmployee.DeptEmployeeType = 0;
                            deptEmployee.OrderID = 0;
                            __DeptEmployee.Add(deptEmployee);
                        }
                    }
                    dbTransaction.Commit();
                }
                catch (Exception exception1)
                {
                    Exception exception = exception1;
                    dbTransaction.Rollback();
                    throw new Exception(string.Concat("出现错误:", exception.ToString()));
                }
            }
            finally
            {
                if (dbConnection.State == ConnectionState.Open)
                {
                    dbConnection.Close();
                }
            }
        }

        private void method_0(TreeItem treeItem_0, string string_0, string string_1, string string_2)
        {
            this.directoryEntry_0 = new DirectoryEntry(string_0, string_1, string_2);
            try
            {
                TreeItem treeItem = new TreeItem();
                Guid guid = new Guid((byte[])this.directoryEntry_0.Properties["objectGUID"].Value);
                treeItem.id = guid.ToString();
                treeItem.text = this.directoryEntry_0.Name.Substring(3);
                treeItem.@value = this.directoryEntry_0.Properties["distinguishedName"].Value.ToString();
                treeItem.isexpand = true;
                treeItem_0.Add(treeItem);
                foreach (DirectoryEntry child in this.directoryEntry_0.Children)
                {
                    if (!this.method_1(child))
                    {
                        continue;
                    }
                    this.method_0(treeItem, string.Concat("LDAP://", child.Properties["distinguishedName"].Value.ToString()), string_1, string_2);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        private bool method_1(DirectoryEntry directoryEntry_1)
        {
            bool flag;
            StringCollection stringCollections = new StringCollection();
            stringCollections.Add("organizationalUnit");
            stringCollections.Add("group");
            if (directoryEntry_1.Name.ToLower() != "ou=domain controllers")
            {
                foreach (object item in directoryEntry_1.Properties["objectClass"])
                {
                    if (!stringCollections.Contains(item.ToString()))
                    {
                        continue;
                    }
                    flag = true;
                    return flag;
                }
                flag = false;
            }
            else
            {
                flag = false;
            }
            return flag;
        }

        internal string method_2(string string_0)
        {
            byte[] byteArray = (new Guid(string_0)).ToByteArray();
            string str = "";
            byte[] numArray = byteArray;
            for (int i = 0; i < (int)numArray.Length; i++)
            {
                byte num = numArray[i];
                str = string.Concat(str, "\\", num.ToString("x2"));
            }
            return str;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            AjaxPro.Utility.RegisterTypeForAjax(typeof(LdapImportList));
            TreeItem treeItem = new TreeItem()
            {
                id = "",
                text = "活动目录",
                @value = "",
                isexpand = true
            };
            LDAPConfig ldapConfig = AppSettings.Instance.LdapConfig;
            string[] serverIP = new string[] { "LDAP://", ldapConfig.ServerIP, ":", ldapConfig.ServerPort, "/", ldapConfig.RootPath };
            string str = string.Concat(serverIP);
            this.method_0(treeItem, str, ldapConfig.Account, ldapConfig.PassWord);
            this.treedata = treeItem.ToJsonString();
        }
    }
}