using AjaxPro;
using EIS.AppBase;
using EIS.DataAccess;
using EIS.Permission;
using EIS.Permission.Access;
using EIS.Permission.Model;
using EIS.Permission.Service;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.Common;
using System.Text;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace EIS.Studio.SysFolder.Permission
{
    public partial class CompanyEmployeeEdit : AdminPageBase
    {
        public string deptName = "";

        public string relationId = "";

        public StringBuilder TipMessage = new StringBuilder();


     
      

        public string deptId
        {
            get
            {
                return this.ViewState["deptId"].ToString();
            }
            set
            {
                this.ViewState["deptId"] = value;
            }
        }

        public string EditEmployeeId
        {
            get
            {
                return this.ViewState["EmployeeId"].ToString();
            }
            set
            {
                this.ViewState["EmployeeId"] = value;
            }
        }

        public string UrlRefer
        {
            get
            {
                return this.ViewState["UrlRefer"].ToString();
            }
            set
            {
                this.ViewState["UrlRefer"] = value;
            }
        }

        public CompanyEmployeeEdit()
        {
        }

        [AjaxMethod(HttpSessionStateRequirement.Read)]
        public void InitialPassWord(string employeeId)
        {
            if (!string.IsNullOrEmpty(employeeId))
            {
                EIS.Permission.Utility.ChangePass(employeeId, AppSettings.Instance.EmployeeDefaultPass);
                DataLog dataLog = new DataLog()
                {
                    AppID = employeeId,
                    AppName = "T_E_Org_Employee",
                    ComputeIP = base.GetClientIP(),
                    ServerIP = HttpContext.Current.Request.ServerVariables["LOCAL_ADDR"],
                    LogType = "密码重置",
                    LogUser = HttpContext.Current.Session["userName"].ToString(),
                    UserName = (HttpContext.Current.Session["EmployeeName"] != null ? base.EmployeeName : ""),
                    ModuleCode = "",
                    ModuleName = "",
                    Browser = EIS.AppBase.Utility.GetBrowserByUserAgent(),
                    Platform = base.GetOSNameByUserAgent(),
                    UserAgent = HttpContext.Current.Request.UserAgent
                };
                string employeeName = EmployeeService.GetEmployeeName(employeeId);
                dataLog.Message = string.Format("操作管理员：{0}，被重置密码：{1}（{2}）", dataLog.LogUser, employeeName, employeeId);
                this.dblogger.WriteSecurityLog(dataLog);
            }
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            DbConnection dbConnection;
            DbTransaction dbTransaction;
            _Employee __Employee;
            _DeptEmployee __DeptEmployee;
            Employee employee;
            string str;
            DeptEmployee deptEmployee;
            Department model;
            Exception exception;
            if (this.selPosition.SelectedValue == "")
            {
                if (this.txtNewPos.Text.Trim() == "")
                {
                    this.TipMessage.AppendFormat("<div class=\"tip\">{0}</div>", "创建新岗位时岗位名称不能为空！");
                    return;
                }
                _Position __Position = new _Position();
                Position position = new Position()
                {
                    _AutoID = Guid.NewGuid().ToString(),
                    _OrgCode = "",
                    _UserName = base.UserName,
                    _CreateTime = DateTime.Now,
                    _UpdateTime = DateTime.Now,
                    _IsDel = 0,
                    PositionCode = "",
                    PositionName = this.txtNewPos.Text.Trim(),
                    OrderID = 0,
                    ParentPositionName = "",
                    ParentPositionId = "",
                    PropId = "",
                    PropName = "",
                    DeptID = this.deptId
                };
                __Position.Add(position);
                ListItem listItem = new ListItem(position.PositionName, position._AutoID);
                this.selPosition.Items.Add(listItem);
                this.selPosition.SelectedValue = position._AutoID;
            }
            if (string.IsNullOrEmpty(this.EditEmployeeId))
            {
                dbConnection = SysDatabase.CreateConnection();
                dbConnection.Open();
                dbTransaction = dbConnection.BeginTransaction();
                try
                {
                    try
                    {
                        __Employee = new _Employee(dbTransaction);
                        __DeptEmployee = new _DeptEmployee(dbTransaction);
                        employee = new Employee()
                        {
                            _AutoID = Guid.NewGuid().ToString(),
                            _OrgCode = "",
                            _UserName = base.UserName,
                            _CreateTime = DateTime.Now,
                            _UpdateTime = DateTime.Now,
                            _IsDel = 0,
                            EmployeeName = this.txtEmpName.Text,
                            EmployeeCode = this.txtEmpCode.Text,
                            Sex = this.selSex.SelectedValue
                        };
                        if (this.txtBirthDay.Text != "")
                        {
                            employee.Birthday = new DateTime?(Convert.ToDateTime(this.txtBirthDay.Text));
                        }
                        employee.Officephone = this.txtOfficePhone.Text;
                        employee.EMail = this.txtEmail.Text;
                        employee.Cellphone = this.txtMobile.Text;
                        employee.EmployeeType = this.selType.SelectedValue;
                        employee.EmployeeState = this.selState.SelectedValue;
                        employee.IsLocked = this.selLock.SelectedValue;
                        employee.OutList = this.selOutList.SelectedValue;
                        employee.LockReason = this.txtLockReason.Text;
                        str = this.txtLoginName.Text.Trim();
                        if (__Employee.IsLoginExist(str, employee._AutoID))
                        {
                            throw new Exception(string.Concat("已经存在登录账号：", str));
                        }
                        employee.LoginName = str;
                        employee.LoginPass = Security.Encrypt(AppSettings.Instance.EmployeeDefaultPass);
                        __Employee.Add(employee);
                        deptEmployee = new DeptEmployee()
                        {
                            _AutoID = Guid.NewGuid().ToString(),
                            _OrgCode = "",
                            _UserName = base.UserName,
                            _CreateTime = DateTime.Now,
                            _UpdateTime = DateTime.Now,
                            _IsDel = 0
                        };
                        model = DepartmentService.GetModel(this.deptId);
                        deptEmployee.DeptID = this.deptId;
                        deptEmployee.DeptName = this.txtDeptName.Text;
                        deptEmployee.CompanyID = model.CompanyID;
                        deptEmployee.EmployeeID = employee._AutoID;
                        deptEmployee.EmployeeName = employee.EmployeeName;
                        deptEmployee.OrderID = (this.txtOrder.Text == "" ? 0 : Convert.ToInt32(this.txtOrder.Text));
                        if (this.selPosition.SelectedIndex <= -1)
                        {
                            deptEmployee.PositionId = "";
                            deptEmployee.PositionName = "";
                        }
                        else
                        {
                            deptEmployee.PositionId = this.selPosition.SelectedValue;
                            deptEmployee.PositionName = this.selPosition.SelectedItem.Text;
                        }
                        deptEmployee.IsDefault = 1;
                        __DeptEmployee.Add(deptEmployee);
                        dbTransaction.Commit();
                        this.EditEmployeeId = employee._AutoID;
                        base.ClientScript.RegisterClientScriptBlock(base.GetType(), "", "<script language=javascript>$.noticeAdd({text:'保存成功！',stay:false});returnList();</script>");
                    }
                    catch (Exception exception1)
                    {
                        exception = exception1;
                        dbTransaction.Rollback();
                        this.TipMessage.AppendFormat("<div class=\"tip\">{0}</div>", string.Concat("出现错误:", exception.Message));
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
            else
            {
                dbConnection = SysDatabase.CreateConnection();
                dbConnection.Open();
                dbTransaction = dbConnection.BeginTransaction();
                try
                {
                    try
                    {
                        bool flag = false;
                        __Employee = new _Employee(dbTransaction);
                        __DeptEmployee = new _DeptEmployee(dbTransaction);
                        employee = EmployeeService.GetModel(this.EditEmployeeId);
                        if (employee.EmployeeName != this.txtEmpName.Text)
                        {
                            flag = true;
                        }
                        employee.EmployeeName = this.txtEmpName.Text;
                        employee.EmployeeCode = this.txtEmpCode.Text;
                        employee.Sex = this.selSex.SelectedValue;
                        if (this.txtBirthDay.Text != "")
                        {
                            employee.Birthday = new DateTime?(Convert.ToDateTime(this.txtBirthDay.Text));
                        }
                        employee.Officephone = this.txtOfficePhone.Text;
                        employee.EMail = this.txtEmail.Text;
                        employee.Cellphone = this.txtMobile.Text;
                        employee.EmployeeType = this.selType.SelectedValue;
                        employee.EmployeeState = this.selState.SelectedValue;
                        employee.IsLocked = this.selLock.SelectedValue;
                        employee.OutList = this.selOutList.SelectedValue;
                        employee.LockReason = this.txtLockReason.Text;
                        str = this.txtLoginName.Text.Trim();
                        if (__Employee.IsLoginExist(str, employee._AutoID))
                        {
                            throw new Exception(string.Concat("已经存在登录账号：", str));
                        }
                        employee.LoginName = str;
                        __Employee.Update(employee);
                        if (flag)
                        {
                            __Employee.UpdateName(employee._AutoID, employee.EmployeeName);
                        }
                        deptEmployee = __DeptEmployee.GetModel(this.relationId);
                        if (deptEmployee != null)
                        {
                            deptEmployee.OrderID = (this.txtOrder.Text == "" ? 0 : Convert.ToInt32(this.txtOrder.Text));
                            if (this.selPosition.SelectedIndex <= -1)
                            {
                                deptEmployee.PositionId = "";
                                deptEmployee.PositionName = "";
                            }
                            else
                            {
                                deptEmployee.PositionId = this.selPosition.SelectedValue;
                                deptEmployee.PositionName = this.selPosition.SelectedItem.Text;
                            }
                            model = DepartmentService.GetModel(deptEmployee.DeptID);
                            deptEmployee.DeptName = model.DeptName;
                            deptEmployee.CompanyID = model.CompanyID;
                            deptEmployee.EmployeeName = employee.EmployeeName;
                            __DeptEmployee.Update(deptEmployee);
                        }
                        dbTransaction.Commit();
                        base.ClientScript.RegisterClientScriptBlock(base.GetType(), "", "<script language=javascript>$.noticeAdd({text:'保存成功！',stay:false});</script>");
                    }
                    catch (Exception exception2)
                    {
                        exception = exception2;
                        dbTransaction.Rollback();
                        this.TipMessage.AppendFormat("<div class=\"tip\">{0}</div>", string.Concat("出现错误:", exception.Message));
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
        }

        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            DbConnection dbConnection;
            DbTransaction dbTransaction;
            _Employee __Employee;
            _DeptEmployee __DeptEmployee;
            Employee employee;
            string str;
            DeptEmployee deptEmployee;
            Exception exception;
            if (this.selPosition.SelectedValue == "")
            {
                if (this.txtNewPos.Text.Trim() == "")
                {
                    this.TipMessage.AppendFormat("<div class=\"tip\">{0}</div>", "创建新岗位时岗位名称不能为空！");
                    return;
                }
                _Position __Position = new _Position();
                Position position = new Position()
                {
                    _AutoID = Guid.NewGuid().ToString(),
                    _OrgCode = "",
                    _UserName = base.UserName,
                    _CreateTime = DateTime.Now,
                    _UpdateTime = DateTime.Now,
                    _IsDel = 0,
                    PositionCode = "",
                    PositionName = this.txtNewPos.Text.Trim(),
                    OrderID = 0,
                    ParentPositionName = "",
                    ParentPositionId = "",
                    PropId = "",
                    PropName = "",
                    DeptID = this.deptId
                };
                __Position.Add(position);
                ListItem listItem = new ListItem(position.PositionName, position._AutoID);
                this.selPosition.Items.Add(listItem);
                this.selPosition.SelectedValue = position._AutoID;
            }
            if (string.IsNullOrEmpty(this.EditEmployeeId))
            {
                dbConnection = SysDatabase.CreateConnection();
                dbConnection.Open();
                dbTransaction = dbConnection.BeginTransaction();
                try
                {
                    try
                    {
                        __Employee = new _Employee(dbTransaction);
                        __DeptEmployee = new _DeptEmployee(dbTransaction);
                        employee = new Employee()
                        {
                            _AutoID = Guid.NewGuid().ToString(),
                            _OrgCode = "",
                            _UserName = base.UserName,
                            _CreateTime = DateTime.Now,
                            _UpdateTime = DateTime.Now,
                            _IsDel = 0,
                            EmployeeName = this.txtEmpName.Text,
                            EmployeeCode = this.txtEmpCode.Text,
                            Sex = this.selSex.SelectedValue
                        };
                        if (this.txtBirthDay.Text != "")
                        {
                            employee.Birthday = new DateTime?(Convert.ToDateTime(this.txtBirthDay.Text));
                        }
                        employee.Officephone = this.txtOfficePhone.Text;
                        employee.EMail = this.txtEmail.Text;
                        employee.Cellphone = this.txtMobile.Text;
                        employee.EmployeeType = this.selType.SelectedValue;
                        employee.EmployeeState = this.selState.SelectedValue;
                        employee.IsLocked = this.selLock.SelectedValue;
                        employee.OutList = this.selOutList.SelectedValue;
                        employee.LockReason = this.txtLockReason.Text;
                        str = this.txtLoginName.Text.Trim();
                        if (__Employee.IsLoginExist(str, employee._AutoID))
                        {
                            throw new Exception(string.Concat("已经存在登录账号：", str));
                        }
                        employee.LoginName = str;
                        employee.LoginPass = Security.Encrypt(AppSettings.Instance.EmployeeDefaultPass);
                        __Employee.Add(employee);
                        deptEmployee = new DeptEmployee()
                        {
                            _AutoID = Guid.NewGuid().ToString(),
                            _OrgCode = "",
                            _UserName = base.UserName,
                            _CreateTime = DateTime.Now,
                            _UpdateTime = DateTime.Now,
                            _IsDel = 0,
                            CompanyID = DepartmentService.GetModel(this.deptId).CompanyID,
                            DeptID = this.deptId,
                            DeptName = this.txtDeptName.Text,
                            EmployeeID = employee._AutoID,
                            EmployeeName = employee.EmployeeName,
                            OrderID = (this.txtOrder.Text == "" ? 0 : Convert.ToInt32(this.txtOrder.Text))
                        };
                        if (this.selPosition.SelectedIndex <= -1)
                        {
                            deptEmployee.PositionId = "";
                            deptEmployee.PositionName = "";
                        }
                        else
                        {
                            deptEmployee.PositionId = this.selPosition.SelectedValue;
                            deptEmployee.PositionName = this.selPosition.SelectedItem.Text;
                        }
                        deptEmployee.IsDefault = 1;
                        __DeptEmployee.Add(deptEmployee);
                        dbTransaction.Commit();
                        this.EditEmployeeId = employee._AutoID;
                        base.Response.Redirect(string.Concat("CompanyEmployeeEdit.aspx?DeptId=", this.deptId));
                    }
                    catch (Exception exception1)
                    {
                        exception = exception1;
                        dbTransaction.Rollback();
                        this.TipMessage.AppendFormat("<div class=\"tip\">{0}</div>", string.Concat("出现错误:", exception.Message));
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
            else
            {
                dbConnection = SysDatabase.CreateConnection();
                dbConnection.Open();
                dbTransaction = dbConnection.BeginTransaction();
                try
                {
                    try
                    {
                        __Employee = new _Employee(dbTransaction);
                        __DeptEmployee = new _DeptEmployee(dbTransaction);
                        employee = EmployeeService.GetModel(this.EditEmployeeId);
                        employee.EmployeeName = this.txtEmpName.Text;
                        employee.EmployeeCode = this.txtEmpCode.Text;
                        employee.Sex = this.selSex.SelectedValue;
                        if (this.txtBirthDay.Text != "")
                        {
                            employee.Birthday = new DateTime?(Convert.ToDateTime(this.txtBirthDay.Text));
                        }
                        employee.Officephone = this.txtOfficePhone.Text;
                        employee.EMail = this.txtEmail.Text;
                        employee.Cellphone = this.txtMobile.Text;
                        employee.EmployeeType = this.selType.SelectedValue;
                        employee.EmployeeState = this.selState.SelectedValue;
                        employee.IsLocked = this.selLock.SelectedValue;
                        employee.OutList = this.selOutList.SelectedValue;
                        employee.LockReason = this.txtLockReason.Text;
                        str = this.txtLoginName.Text.Trim();
                        if (__Employee.IsLoginExist(str, employee._AutoID))
                        {
                            throw new Exception(string.Concat("已经存在登录账号：", str));
                        }
                        employee.LoginName = str;
                        __Employee.Update(employee);
                        deptEmployee = __DeptEmployee.GetModel(this.relationId);
                        if (deptEmployee != null)
                        {
                            deptEmployee.OrderID = (this.txtOrder.Text == "" ? 0 : Convert.ToInt32(this.txtOrder.Text));
                            if (this.selPosition.SelectedIndex <= -1)
                            {
                                deptEmployee.PositionId = "";
                                deptEmployee.PositionName = "";
                            }
                            else
                            {
                                deptEmployee.PositionId = this.selPosition.SelectedValue;
                                deptEmployee.PositionName = this.selPosition.SelectedItem.Text;
                            }
                            __DeptEmployee.Update(deptEmployee);
                        }
                        dbTransaction.Commit();
                        base.Response.Redirect(string.Concat("CompanyEmployeeEdit.aspx?DeptId=", deptEmployee.DeptID));
                    }
                    catch (Exception exception2)
                    {
                        exception = exception2;
                        dbTransaction.Rollback();
                        this.TipMessage.AppendFormat("<div class=\"tip\">{0}</div>", string.Concat("出现错误:", exception.Message));
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
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            AjaxPro.Utility.RegisterTypeForAjax(typeof(CompanyEmployeeEdit));
            this.relationId = base.GetParaValue("relationId");
            if (!base.IsPostBack)
            {
                if (base.Request.UrlReferrer == null)
                {
                    this.UrlRefer = "";
                }
                else
                {
                    this.UrlRefer = base.Request.UrlReferrer.OriginalString;
                }
                string paraValue = "";
                if (string.IsNullOrEmpty(this.relationId))
                {
                    this.deptId = base.GetParaValue("deptId");
                    if (this.deptId == "" && base.GetParaValue("positionId") != "")
                    {
                        paraValue = base.GetParaValue("positionId");
                        Position positionById = PositionService.GetPositionById(paraValue);
                        if (positionById != null)
                        {
                            this.deptId = positionById.DeptID;
                        }
                    }
                    this.EditEmployeeId = "";
                    if (string.IsNullOrEmpty(this.deptId))
                    {
                        this.TipMessage.Append("创建员工时deptId参数不能为空");
                        return;
                    }
                    this.selType.SelectedValue = "正式";
                    this.selState.SelectedValue = "在职";
                    this.selLock.SelectedValue = "否";
                    this.selOutList.SelectedValue = "否";
                }
                else
                {
                    DeptEmployee deptEmployeeById = DeptEmployeeService.GetDeptEmployeeById(this.relationId);
                    this.EditEmployeeId = deptEmployeeById.EmployeeID;
                    Employee model = EmployeeService.GetModel(this.EditEmployeeId);
                    this.txtEmpName.Text = model.EmployeeName;
                    this.txtEmpCode.Text = model.EmployeeCode;
                    this.selSex.SelectedValue = (model.Sex == "" ? "男" : model.Sex);
                    this.txtBirthDay.Text = (model.Birthday.HasValue ? model.Birthday.Value.ToString("yyyy-MM-dd") : "");
                    this.txtOfficePhone.Text = model.Officephone;
                    this.txtEmail.Text = model.EMail;
                    this.txtMobile.Text = model.Cellphone;
                    this.selType.SelectedValue = (model.EmployeeType == "" ? "正式" : model.EmployeeType);
                    this.selState.SelectedValue = (model.EmployeeState == "" ? "在职" : model.EmployeeState);
                    this.selLock.SelectedValue = (model.IsLocked == "" ? "否" : model.IsLocked);
                    this.selOutList.SelectedValue = (model.OutList == "" ? "否" : model.OutList);
                    this.txtLockReason.Text = model.LockReason;
                    this.txtLoginName.Text = model.LoginName;
                    paraValue = deptEmployeeById.PositionId;
                    this.txtOrder.Text = deptEmployeeById.OrderID.ToString();
                    this.deptId = deptEmployeeById.DeptID;
                }
                Department department = DepartmentService.GetModel(this.deptId);
                this.txtDeptName.Text = department.DeptName;
                this.txtDeptName.ReadOnly = true;
                List<Position> positionByDeptId = PositionService.GetPositionByDeptId(this.deptId);
                this.selPosition.Items.Add(new ListItem("创建新岗位", ""));
                foreach (Position position in positionByDeptId)
                {
                    ListItem listItem = new ListItem(position.PositionName, position._AutoID);
                    if (paraValue == position._AutoID)
                    {
                        listItem.Selected = true;
                    }
                    this.selPosition.Items.Add(listItem);
                }
            }
        }
    }
}