using AjaxPro;
using EIS.AppBase;
using EIS.AppCommon;
using EIS.DataAccess;
using EIS.DataModel.Access;
using EIS.Permission.Access;
using EIS.Permission.Model;
using EIS.Permission.Service;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Runtime.CompilerServices;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace EIS.Web.SysFolder.Permission
{
    public partial class DefDeptLimitLeft : AdminPageBase
    {

        private _Catalog _Catalog_0 = new _Catalog();

        private List<Department> list_0;

        public string treedata = "";


        [AjaxMethod(HttpSessionStateRequirement.Read)]
        public string AddSonDept(string nodeName, string PWBS)
        {
            string str;
            DbConnection dbConnection = SysDatabase.CreateConnection();
            dbConnection.Open();
            DbTransaction dbTransaction = dbConnection.BeginTransaction();
            try
            {
                try
                {
                    _Department __Department = new _Department(dbTransaction);
                    Department department = new Department()
                    {
                        _AutoID = Guid.NewGuid().ToString(),
                        _OrgCode = "",
                        _UserName = base.UserName,
                        _CreateTime = DateTime.Now,
                        _UpdateTime = DateTime.Now,
                        _IsDel = 0,
                        DeptCode = "",
                        DeptName = nodeName,
                        DeptPWBS = PWBS,
                        DeptWBS = DepartmentService.GetNewDeptWbs(PWBS),
                        DeptAbbr = "",
                        OrderID = DepartmentService.GetMaxOrder(PWBS) + 1,
                        UpPosition = "",
                        UpPositionId = "",
                        TypeID = "7C2F6B38-EDE8-4EB4-B667-6EABE32A1EEF",
                        DeptState = "正常",
                        DeptAdminId = "",
                        DeptAdminCn = "",
                        DeptSfwId = "",
                        DeptSfwCn = "",
                        PicPositionId = "",
                        PicPosition = "",
                        CompanyID = DepartmentService.GetCompanyByDeptWbs(PWBS)._AutoID
                    };
                    __Department.Add(department);
                    dbTransaction.Commit();
                    str = string.Concat(department._AutoID, "|", PWBS);
                }
                catch (Exception exception1)
                {
                    Exception exception = exception1;
                    dbTransaction.Rollback();
                    throw exception;
                }
            }
            finally
            {
                if (dbConnection.State == ConnectionState.Open)
                {
                    dbConnection.Close();
                }
            }
            return str;
        }

        private void method_0(zTreeNode zTreeNode_0)
        {

            List<Department> departments = this.list_0.FindAll((Department department_0) => department_0.DeptPWBS == zTreeNode_0.id);
            if (departments.Count > 0)
            {
                foreach (Department department in departments)
                {
                    zTreeNode _zTreeNode = new zTreeNode()
                    {
                        name = department.DeptName,
                        id = department.DeptWBS,
                        @value = department._AutoID
                    };
                    zTreeNode_0.Add(_zTreeNode);
                    this.method_0(_zTreeNode);
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            AjaxPro.Utility.RegisterTypeForAjax(typeof(DefDeptLimitLeft));
            Department topDept = DepartmentService.GetTopDept();
            if (base.LoginType == "2")
            {
                string str = this.Session["CompanyId"].ToString();
                if (str != "")
                {
                    topDept = DepartmentService.GetModel(str);
                }
            }
            this.list_0 = (new _Department()).GetSubDeptByWbs(topDept.DeptWBS);
            zTreeNode _zTreeNode = new zTreeNode()
            {
                id = topDept.DeptWBS,
                name = topDept.DeptName,
                @value = topDept._AutoID,
                open = true
            };
            this.method_0(_zTreeNode);
            this.treedata = _zTreeNode.ToJsonString(true);
        }

        [AjaxMethod(HttpSessionStateRequirement.Read)]
        public string RemoveDept(string deptId)
        {
            try
            {
                DepartmentService.RemoveDepartment(deptId);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return "";
        }
    }
}