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
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace EIS.Studio.SysFolder.Permission
{
    public partial class CompanyInfo : AdminPageBase
    {
        public string companyId = "";

        public StringBuilder TipMessage = new StringBuilder();

    

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            DbConnection dbConnection = SysDatabase.CreateConnection();
            dbConnection.Open();
            DbTransaction dbTransaction = dbConnection.BeginTransaction();
            try
            {
                try
                {
                    _Department __Department = new _Department(dbTransaction);
                    Department model = DepartmentService.GetModel(this.companyId);
                    model.DeptName = this.txtDeptName.Text;
                    model.DeptCode = this.txtDeptCode.Text;
                    model.DeptAbbr = this.txtDeptAbbr.Text;
                    model.Remark = this.txtDeptNote.Text;
                    model.OrderID = int.Parse(this.txtOrder.Text);
                    model.TypeID = this.DropDownList1.SelectedValue;
                    __Department.Update(model);
                    _User __User = new _User(dbTransaction);
                    string str = this.txtDeptSa.Text.Trim();
                    if (UserService.IsLoginUserExist(str, this.companyId))
                    {
                        throw new Exception(string.Concat("已经存在登录账号：", str));
                    }
                    LoginUser userByCompanyId = UserService.GetUserByCompanyId(model._AutoID);
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
                            LoginName = this.txtDeptSa.Text,
                            CompanyId = model._AutoID,
                            IsLock = 0,
                            LoginType = 1
                        };
                        loginUser.LoginType = 2;
                        __User.Add(loginUser);
                    }
                    else
                    {
                        userByCompanyId.LoginName = this.txtDeptSa.Text;
                        userByCompanyId.LoginType = 2;
                        __User.Update(userByCompanyId);
                    }
                    dbTransaction.Commit();
                    base.ClientScript.RegisterClientScriptBlock(base.GetType(), "", "<script language=javascript>$.noticeAdd({text:'保存成功！',stay:false});</script>");
                }
                catch (Exception exception1)
                {
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
            this.companyId = base.Session["CompanyId"].ToString();
            if (!base.IsPostBack)
            {
                string typeID = "";
                if (string.IsNullOrEmpty(this.companyId))
                {
                    this.TipMessage.AppendFormat("<div class=\"ErrorMsg\">找不到对应的公司</div>", new object[0]);
                }
                else
                {
                    Department model = DepartmentService.GetModel(this.companyId);
                    this.txtDeptName.Text = model.DeptName;
                    this.txtDeptCode.Text = model.DeptCode;
                    this.txtDeptAbbr.Text = model.DeptAbbr;
                    this.txtOrder.Text = model.OrderID.ToString();
                    this.txtDeptNote.Text = model.Remark;
                    typeID = model.TypeID;
                    LoginUser userByCompanyId = UserService.GetUserByCompanyId(model._AutoID);
                    if (userByCompanyId != null)
                    {
                        this.txtDeptSa.Text = userByCompanyId.LoginName;
                    }
                    Department modelByWbs = DepartmentService.GetModelByWbs(model.DeptPWBS);
                    this.txtParentName.Text = modelByWbs.DeptName;
                }
                _DeptType __DeptType = new _DeptType();
                DataTable list = __DeptType.GetList(string.Format(" TypeProp in ({0},{1})", DeptType.CompanyTypeId, DeptType.GroupTypeId));
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