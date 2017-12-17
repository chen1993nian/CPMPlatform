using EIS.AppBase;
using EIS.DataAccess;
using EIS.Permission.Access;
using EIS.Permission.Model;
using EIS.Permission.Service;
using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace EIS.Web.SysFolder.Permission
{
    public partial class GroupCompanyEdit : AdminPageBase
    {
        public string pwbs = "";

        public StringBuilder TipMessage = new StringBuilder();

       
        private string deptId
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

        public GroupCompanyEdit()
        {
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            DbConnection dbConnection;
            DbTransaction dbTransaction;
            _User __User;
            LoginUser loginUser;
            Exception exception;
            if (this.deptId == "")
            {
                dbConnection = SysDatabase.CreateConnection();
                dbConnection.Open();
                dbTransaction = dbConnection.BeginTransaction();
                try
                {
                    try
                    {
                        _Department __Department = new _Department(dbTransaction);
                        string newDeptWbs = DepartmentService.GetNewDeptWbs(this.pwbs);
                        string strGuID = Guid.NewGuid().ToString();
                        Department department = new Department()
                        {
                            _AutoID = strGuID,
                            _OrgCode = "",
                            _UserName = base.UserName,
                            _CreateTime = DateTime.Now,
                            _UpdateTime = DateTime.Now,
                            _IsDel = 0,
                            CompanyID = strGuID,
                            DeptWBS = newDeptWbs,
                            DeptPWBS = this.pwbs,
                            DeptName = this.txtDeptName.Text,
                            DeptCode = this.txtDeptCode.Text,
                            DeptAbbr = this.txtDeptAbbr.Text,
                            Remark = this.txtDeptNote.Text,
                            OrderID = int.Parse(this.txtOrder.Text),
                            TypeID = this.DropDownList1.SelectedValue,
                            LdapPath = this.txtPath.Text
                        };
                        __Department.Add(department);
                        if (this.txtDeptSa.Text.Trim() != "")
                        {
                            __User = new _User(dbTransaction);
                            if (UserService.IsLoginUserExist(this.txtDeptSa.Text.Trim()))
                            {
                                throw new Exception(string.Concat("已经存在登录账号：", this.txtDeptSa.Text));
                            }
                            loginUser = new LoginUser()
                            {
                                _AutoID = Guid.NewGuid().ToString(),
                                _OrgCode = "",
                                _UserName = base.UserName,
                                _CreateTime = DateTime.Now,
                                _UpdateTime = DateTime.Now,
                                _IsDel = 0,
                                LoginName = this.txtDeptSa.Text.Trim(),
                                LoginPass = Security.Encrypt(AppSettings.Instance.AdminDefaultPass),
                                CompanyId = department._AutoID,
                                IsLock = 0,
                                LoginType = 1
                            };
                            loginUser.LoginType = 2;
                            __User.Add(loginUser);
                        }
                        dbTransaction.Commit();
                        this.deptId = department._AutoID;
                        base.ClientScript.RegisterClientScriptBlock(base.GetType(), "", "<script language=javascript>$.noticeAdd({text:'保存成功！',stay:false});returnList();</script>");
                    }
                    catch (Exception exception1)
                    {
                        exception = exception1;
                        dbTransaction.Rollback();
                        this.TipMessage.AppendFormat("<div class=\"ErrorMsg\">{0}</div>", string.Concat("出现错误:", exception.Message));
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
                        _Department __Department1 = new _Department(dbTransaction);
                        Department model = DepartmentService.GetModel(this.deptId);
                        model.CompanyID = model._AutoID;
                        model.DeptName = this.txtDeptName.Text;
                        model.DeptCode = this.txtDeptCode.Text;
                        model.DeptAbbr = this.txtDeptAbbr.Text;
                        model.Remark = this.txtDeptNote.Text;
                        model.OrderID = int.Parse(this.txtOrder.Text);
                        model.TypeID = this.DropDownList1.SelectedValue;
                        model.LdapPath = this.txtPath.Text;
                        string deptWBS = model.DeptWBS;
                        string deptPWBS = model.DeptPWBS;
                        if (this.txtParentId.Value != model.DeptPWBS)
                        {
                            model.DeptPWBS = this.txtParentId.Value;
                            model.DeptWBS = DepartmentService.GetNewDeptWbs(this.txtParentId.Value);
                            DepartmentService departmentService = new DepartmentService();
                            departmentService.UpdateParent(deptWBS, model.DeptWBS, dbTransaction);
                        }
                        __Department1.Update(model);
                        if (this.txtDeptSa.Text.Trim() != "")
                        {
                            __User = new _User(dbTransaction);
                            string str = this.txtDeptSa.Text.Trim();
                            if (UserService.IsLoginUserExist(str, this.deptId))
                            {
                                throw new Exception(string.Concat("已经存在登录账号：", str));
                            }
                            LoginUser userByCompanyId = UserService.GetUserByCompanyId(model._AutoID);
                            if (userByCompanyId == null)
                            {
                                loginUser = new LoginUser()
                                {
                                    _AutoID = Guid.NewGuid().ToString(),
                                    _OrgCode = "",
                                    _UserName = base.UserName,
                                    _CreateTime = DateTime.Now,
                                    _UpdateTime = DateTime.Now,
                                    _IsDel = 0,
                                    LoginName = str,
                                    LoginPass = Security.Encrypt(AppSettings.Instance.AdminDefaultPass),
                                    CompanyId = model._AutoID,
                                    IsLock = 0,
                                    LoginType = 2
                                };
                                __User.Add(loginUser);
                            }
                            else if (userByCompanyId.LoginName != str)
                            {
                                userByCompanyId.LoginName = str;
                                userByCompanyId.LoginType = 2;
                                userByCompanyId.LoginPass = Security.Encrypt(AppSettings.Instance.AdminDefaultPass);
                                __User.Update(userByCompanyId);
                            }
                        }
                        dbTransaction.Commit();
                        base.ClientScript.RegisterClientScriptBlock(base.GetType(), "", "<script language=javascript>$.noticeAdd({text:'保存成功！',stay:false});</script>");
                    }
                    catch (Exception exception2)
                    {
                        exception = exception2;
                        dbTransaction.Rollback();
                        this.TipMessage.AppendFormat("<div class=\"ErrorMsg\">{0}</div>", string.Concat("出现错误:", exception.Message));
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
            _User __User;
            LoginUser loginUser;
            Exception exception;
            if (this.deptId == "")
            {
                dbConnection = SysDatabase.CreateConnection();
                dbConnection.Open();
                dbTransaction = dbConnection.BeginTransaction();
                try
                {
                    try
                    {
                        _Department __Department = new _Department(dbTransaction);
                        string newDeptWbs = DepartmentService.GetNewDeptWbs(this.pwbs);
                        string strGuID = Guid.NewGuid().ToString();
                        Department department = new Department()
                        {
                            _AutoID = strGuID,
                            _OrgCode = "",
                            _UserName = base.UserName,
                            _CreateTime = DateTime.Now,
                            _UpdateTime = DateTime.Now,
                            _IsDel = 0,
                            CompanyID = strGuID,
                            DeptWBS = newDeptWbs,
                            DeptPWBS = this.pwbs,
                            DeptName = this.txtDeptName.Text,
                            DeptCode = this.txtDeptCode.Text,
                            DeptAbbr = this.txtDeptAbbr.Text,
                            Remark = this.txtDeptNote.Text,
                            OrderID = int.Parse(this.txtOrder.Text),
                            TypeID = this.DropDownList1.SelectedValue,
                            LdapPath = this.txtPath.Text
                        };
                        __Department.Add(department);
                        if (this.txtDeptSa.Text.Trim() != "")
                        {
                            __User = new _User(dbTransaction);
                            if (UserService.IsLoginUserExist(this.txtDeptSa.Text.Trim()))
                            {
                                throw new Exception(string.Concat("已经存在登录账号：", this.txtDeptSa.Text));
                            }
                            loginUser = new LoginUser()
                            {
                                _AutoID = Guid.NewGuid().ToString(),
                                _OrgCode = "",
                                _UserName = base.UserName,
                                _CreateTime = DateTime.Now,
                                _UpdateTime = DateTime.Now,
                                _IsDel = 0,
                                LoginName = this.txtDeptSa.Text.Trim(),
                                LoginPass = Security.Encrypt(AppSettings.Instance.AdminDefaultPass),
                                CompanyId = department._AutoID,
                                IsLock = 0,
                                LoginType = 2
                            };
                            __User.Add(loginUser);
                        }
                        dbTransaction.Commit();
                        this.deptId = department._AutoID;
                        base.Response.Redirect(string.Concat("GroupCompanyEdit.aspx?DeptPWBS=", this.pwbs, "&DeptId="));
                    }
                    catch (Exception exception1)
                    {
                        exception = exception1;
                        dbTransaction.Rollback();
                        this.TipMessage.AppendFormat("<div class=\"ErrorMsg\">{0}</div>", string.Concat("出现错误:", exception.Message));
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
                        _Department __Department1 = new _Department(dbTransaction);
                        Department model = DepartmentService.GetModel(this.deptId);
                        model.CompanyID = model._AutoID;
                        model.DeptName = this.txtDeptName.Text;
                        model.DeptCode = this.txtDeptCode.Text;
                        model.DeptAbbr = this.txtDeptAbbr.Text;
                        model.Remark = this.txtDeptNote.Text;
                        model.OrderID = int.Parse(this.txtOrder.Text);
                        model.TypeID = this.DropDownList1.SelectedValue;
                        model.LdapPath = this.txtPath.Text;
                        __Department1.Update(model);
                        if (this.txtDeptSa.Text.Trim() != "")
                        {
                            __User = new _User(dbTransaction);
                            string str = this.txtDeptSa.Text.Trim();
                            if (UserService.IsLoginUserExist(str, this.deptId))
                            {
                                throw new Exception(string.Concat("已经存在登录账号：", str));
                            }
                            LoginUser userByCompanyId = UserService.GetUserByCompanyId(model._AutoID);
                            if (userByCompanyId == null)
                            {
                                loginUser = new LoginUser()
                                {
                                    _AutoID = Guid.NewGuid().ToString(),
                                    _OrgCode = "",
                                    _UserName = base.UserName,
                                    _CreateTime = DateTime.Now,
                                    _UpdateTime = DateTime.Now,
                                    _IsDel = 0,
                                    LoginName = this.txtDeptSa.Text,
                                    LoginPass = Security.Encrypt(AppSettings.Instance.AdminDefaultPass),
                                    CompanyId = model._AutoID,
                                    IsLock = 0,
                                    LoginType = 2
                                };
                                __User.Add(loginUser);
                            }
                            else if (userByCompanyId.LoginName != str)
                            {
                                userByCompanyId.LoginName = this.txtDeptSa.Text;
                                userByCompanyId.LoginType = 2;
                                __User.Update(userByCompanyId);
                            }
                        }
                        dbTransaction.Commit();
                        base.Response.Redirect(string.Concat("GroupCompanyEdit.aspx?DeptPWBS=", this.pwbs, "&DeptId="));
                    }
                    catch (Exception exception2)
                    {
                        exception = exception2;
                        dbTransaction.Rollback();
                        this.TipMessage.AppendFormat("<div class=\"ErrorMsg\">{0}</div>", string.Concat("出现错误:", exception.Message));
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
            Department modelByWbs;
            this.pwbs = base.GetParaValue("deptPWBS");
            if (!base.IsPostBack)
            {
                string typeID = "";
                if (string.IsNullOrEmpty(base.GetParaValue("deptId")))
                {
                    modelByWbs = DepartmentService.GetModelByWbs(this.pwbs);
                    this.txtParentId.Value = modelByWbs._AutoID;
                    this.txtParentName.Text = modelByWbs.DeptName;
                    this.deptId = "";
                }
                else
                {
                    this.deptId = base.GetParaValue("deptId");
                    Department model = DepartmentService.GetModel(this.deptId);
                    this.txtDeptName.Text = model.DeptName;
                    this.txtDeptCode.Text = model.DeptCode;
                    this.txtDeptAbbr.Text = model.DeptAbbr;
                    this.txtOrder.Text = model.OrderID.ToString();
                    this.txtDeptNote.Text = model.Remark;
                    this.txtPath.Text = model.LdapPath;
                    typeID = model.TypeID;
                    LoginUser userByCompanyId = UserService.GetUserByCompanyId(model._AutoID);
                    if (userByCompanyId != null)
                    {
                        this.txtDeptSa.Text = userByCompanyId.LoginName;
                    }
                    modelByWbs = DepartmentService.GetModelByWbs(model.DeptPWBS);
                    this.txtParentName.Text = modelByWbs.DeptName;
                    this.txtParentId.Value = modelByWbs.DeptWBS;
                }
                _DeptType __DeptType = new _DeptType();
                DataTable list = __DeptType.GetList(string.Format(" TypeProp in ({0},{1},{2})", DeptType.CompanyTypeId, DeptType.GroupTypeId, DeptType.VirtualTypeId));
                foreach (DataRow row in list.Rows)
                {
                    ListItem listItem = new ListItem(row["TypeName"].ToString(), row["_AutoID"].ToString());
                    if (typeID == row["_AutoID"].ToString())
                    {
                        listItem.Selected = true;
                    }
                    this.DropDownList1.Items.Add(listItem);
                }
            }
        }
    }
}