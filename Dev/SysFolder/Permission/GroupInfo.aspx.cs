using EIS.AppBase;
using EIS.DataAccess;
using EIS.Permission.Access;
using EIS.Permission.Model;
using EIS.Permission.Service;
using System;
using System.Data;
using System.Data.Common;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace EIS.Studio.SysFolder.Permission
{
    public partial class GroupInfo : AdminPageBase
    {
       

        public StringBuilder TipMessage = new StringBuilder();

        private string SaName
        {
            get
            {
                return (this.ViewState["SaName"] == null ? "" : this.ViewState["SaName"].ToString());
            }
            set
            {
                this.ViewState["SaName"] = value;
            }
        }

 

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            DbConnection dbConnection = SysDatabase.CreateConnection();
            dbConnection.Open();
            DbTransaction dbTransaction = dbConnection.BeginTransaction();
            try
            {
                try
                {
                    Department topDept = DepartmentService.GetTopDept();
                    topDept.DeptName = this.txtDeptName.Text;
                    topDept.DeptCode = this.txtDeptCode.Text;
                    topDept.DeptAbbr = this.txtDeptAbbr.Text;
                    topDept.Remark = this.txtDeptNote.Text;
                    (new _Department(dbTransaction)).Update(topDept);
                    string str = this.txtDeptSa.Text.Trim();
                    if (str != this.SaName)
                    {
                        if (str != "")
                        {
                            _User __User = new _User(dbTransaction);
                            if (UserService.IsLoginUserExist(str))
                            {
                                throw new Exception(string.Concat("管理员账号：", str, "已经存在"));
                            }
                            LoginUser userByCompanyId = UserService.GetUserByCompanyId(topDept._AutoID);
                            if (userByCompanyId == null)
                            {
                                LoginUser loginUser = new LoginUser()
                                {
                                    _AutoID = Guid.NewGuid().ToString(),
                                    _OrgCode = "",
                                    _UserName = base.UserName,
                                    _CreateTime = DateTime.Now,
                                    _UpdateTime = DateTime.Now,
                                    _IsDel = 0,
                                    LoginName = str,
                                    LoginPass = Security.Encrypt(AppSettings.Instance.AdminDefaultPass),
                                    CompanyId = topDept._AutoID,
                                    IsLock = 0,
                                    LoginType = 1
                                };
                                __User.Add(loginUser);
                            }
                            else
                            {
                                userByCompanyId.LoginName = str;
                                __User.Update(userByCompanyId);
                            }
                            this.SaName = str;
                        }
                        else
                        {
                            SysDatabase.ExecuteNonQuery(string.Concat("delete T_E_Org_User where LoginName='", this.SaName, "'"), dbTransaction);
                        }
                    }
                    dbTransaction.Commit();
                    base.ClientScript.RegisterStartupScript(base.GetType(), "", string.Concat("alert('保存成功！');"), true);
                }
                catch (Exception exception1)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), "", string.Concat("alert('保存失败！');"), true);
                    Exception exception = exception1;
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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                Department topDept = DepartmentService.GetTopDept();
                this.txtDeptName.Text = topDept.DeptName;
                this.txtDeptCode.Text = topDept.DeptCode;
                this.txtDeptAbbr.Text = topDept.DeptAbbr;
                this.txtDeptNote.Text = topDept.Remark;
                LoginUser userByCompanyId = UserService.GetUserByCompanyId(topDept._AutoID);
                if (userByCompanyId != null)
                {
                    this.SaName = userByCompanyId.LoginName;
                    this.txtDeptSa.Text = userByCompanyId.LoginName;
                }
            }
        }
    }
}