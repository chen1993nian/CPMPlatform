using AjaxPro;
using EIS.AppBase;
using EIS.DataAccess;
using EIS.DataModel.Access;
using EIS.DataModel.Model;
using EIS.Permission.Access;
using EIS.Permission.Model;
using EIS.Permission.Service;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace EIS.Studio.SysFolder.Permission
{
    public partial class DefDeptEdit : AdminPageBase
    {
      


        public string deptName = "";

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

        public DefDeptEdit()
        {
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            DbConnection dbConnection;
            DbTransaction dbTransaction;
            _Department __Department;
            Department department;
            string companyByDeptId;
            string str;
            Exception exception;
            if (string.IsNullOrEmpty(this.deptId))
            {
                dbConnection = SysDatabase.CreateConnection();
                dbConnection.Open();
                dbTransaction = dbConnection.BeginTransaction();
                try
                {
                    try
                    {
                        __Department = new _Department(dbTransaction);
                        department = new Department()
                        {
                            _AutoID = Guid.NewGuid().ToString(),
                            _OrgCode = "",
                            _UserName = base.UserName,
                            _CreateTime = DateTime.Now,
                            _UpdateTime = DateTime.Now,
                            _IsDel = 0,
                            DeptCode = this.txtCode.Text,
                            DeptName = this.txtName.Text,
                            DeptFullName = this.txtFullName.Text,
                            DeptProp = this.listProp.SelectedValue,
                            DeptPWBS = base.GetParaValue("deptpwbs"),
                            DeptWBS = DepartmentService.GetNewDeptWbs(base.GetParaValue("deptpwbs")),
                            DeptAbbr = this.txtAbbr.Text,
                            OrderID = Convert.ToInt32((this.txtOrder.Text == "" ? "0" : this.txtOrder.Text)),
                            UpPosition = this.UpLeader.Text,
                            UpPositionId = this.UpLeaderId.Value,
                            TypeID = this.listType.SelectedValue,
                            DeptState = this.RadioState.SelectedValue,
                            DeptAdminId = this.AdminId.Value,
                            DeptAdminCn = this.AdminCn.Text,
                            DeptSfwId = this.DeptSfwId.Value,
                            DeptSfwCn = this.DeptSfwCn.Text,
                            PicEmployeeId2 = this.hidPicId2.Value,
                            PicEmployeeName2 = this.txtPicName2.Text,
                            UpEmployeeId = this.UpLeaderId2.Value,
                            UpEmployeeName = this.UpLeader2.Text
                        };
                        if (this.selPosition.SelectedItem != null)
                        {
                            department.PicPositionId = this.selPosition.SelectedValue;
                            department.PicPosition = this.selPosition.SelectedItem.Text;
                        }
                        if (this.selCalendar.SelectedItem != null)
                        {
                            department.CalendarId = this.selCalendar.SelectedValue;
                        }
                        __Department.Add(department);
                        this.deptId = department._AutoID;
                        this.txtWbs.Text = department.DeptWBS;
                        this.deptId = department._AutoID;
                        companyByDeptId = DepartmentService.GetCompanyByDeptId(this.deptId, dbTransaction)._AutoID;
                        str = string.Format("update T_E_Org_Department set CompanyId='{1}' where _AutoId='{0}';", this.deptId, companyByDeptId);
                        SysDatabase.ExecuteNonQuery(str, dbTransaction);
                        dbTransaction.Commit();
                        base.ClientScript.RegisterClientScriptBlock(base.GetType(), "", "<script language=javascript>$.noticeAdd({text:'保存成功！',stay:false});</script>");
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
                        __Department = new _Department(dbTransaction);
                        _DeptEmployee __DeptEmployee = new _DeptEmployee(dbTransaction);
                        department = __Department.GetModel(this.deptId);
                        department._UpdateTime = DateTime.Now;
                        department.DeptCode = this.txtCode.Text;
                        department.DeptName = this.txtName.Text;
                        department.DeptFullName = this.txtFullName.Text;
                        department.DeptProp = this.listProp.SelectedValue;
                        department.DeptAbbr = this.txtAbbr.Text;
                        department.OrderID = Convert.ToInt32((this.txtOrder.Text == "" ? "0" : this.txtOrder.Text));
                        string typeID = department.TypeID;
                        department.TypeID = this.listType.SelectedValue;
                        department.DeptState = this.RadioState.SelectedValue;
                        department.DeptAdminId = this.AdminId.Value;
                        department.DeptAdminCn = this.AdminCn.Text;
                        department.PicEmployeeId2 = this.hidPicId2.Value;
                        department.PicEmployeeName2 = this.txtPicName2.Text;
                        department.UpEmployeeId = this.UpLeaderId2.Value;
                        department.UpEmployeeName = this.UpLeader2.Text;
                        department.DeptSfwId = this.DeptSfwId.Value;
                        department.DeptSfwCn = this.DeptSfwCn.Text;
                        department.UpPosition = this.UpLeader.Text;
                        department.UpPositionId = this.UpLeaderId.Value;
                        if (this.selPosition.SelectedItem != null)
                        {
                            department.PicPositionId = this.selPosition.SelectedValue;
                            department.PicPosition = this.selPosition.SelectedItem.Text;
                        }
                        if (this.selCalendar.SelectedItem != null)
                        {
                            department.CalendarId = this.selCalendar.SelectedValue;
                        }
                        __Department.Update(department);
                        companyByDeptId = DepartmentService.GetCompanyByDeptId(this.deptId, dbTransaction)._AutoID;
                        str = string.Format("\r\n                        update T_E_Org_Department set CompanyId='{2}' where _AutoId='{0}';\r\n                        update T_E_Org_DeptEmployee set DeptName='{1}',CompanyId='{2}' where DeptId='{0}'", this.deptId, department.DeptName, companyByDeptId);
                        SysDatabase.ExecuteNonQuery(str, dbTransaction);
                        if (typeID != department.TypeID)
                        {
                            DepartmentService.UpdateDeptCompany(department.DeptWBS, dbTransaction);
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
            _Department __Department;
            Department department;
            string companyByDeptId;
            string str;
            Exception exception;
            if (string.IsNullOrEmpty(this.deptId))
            {
                dbConnection = SysDatabase.CreateConnection();
                dbConnection.Open();
                dbTransaction = dbConnection.BeginTransaction();
                try
                {
                    try
                    {
                        __Department = new _Department(dbTransaction);
                        department = new Department()
                        {
                            _AutoID = Guid.NewGuid().ToString(),
                            _OrgCode = "",
                            _UserName = base.UserName,
                            _CreateTime = DateTime.Now,
                            _UpdateTime = DateTime.Now,
                            _IsDel = 0,
                            DeptCode = this.txtCode.Text,
                            DeptName = this.txtName.Text,
                            DeptFullName = this.txtFullName.Text,
                            DeptPWBS = base.GetParaValue("deptpwbs"),
                            DeptWBS = DepartmentService.GetNewDeptWbs(base.GetParaValue("deptpwbs")),
                            DeptAbbr = this.txtAbbr.Text,
                            OrderID = Convert.ToInt32((this.txtOrder.Text == "" ? "0" : this.txtOrder.Text)),
                            UpPosition = this.UpLeader.Text,
                            UpPositionId = this.UpLeaderId.Value,
                            TypeID = this.listType.SelectedValue,
                            DeptState = this.RadioState.SelectedValue,
                            DeptAdminId = this.AdminId.Value,
                            DeptAdminCn = this.AdminCn.Text,
                            DeptSfwId = this.DeptSfwId.Value,
                            DeptSfwCn = this.DeptSfwCn.Text
                        };
                        if (this.selPosition.SelectedItem != null)
                        {
                            department.PicPositionId = this.selPosition.SelectedValue;
                            department.PicPosition = this.selPosition.SelectedItem.Text;
                        }
                        if (this.selCalendar.SelectedItem != null)
                        {
                            department.CalendarId = this.selCalendar.SelectedValue;
                        }
                        __Department.Add(department);
                        this.deptId = department._AutoID;
                        companyByDeptId = DepartmentService.GetCompanyByDeptId(this.deptId, dbTransaction)._AutoID;
                        str = string.Format("update T_E_Org_Department set CompanyId='{1}' where _AutoId='{0}';", this.deptId, companyByDeptId);
                        SysDatabase.ExecuteNonQuery(str, dbTransaction);
                        dbTransaction.Commit();
                        base.Response.Redirect(string.Concat("DefDeptEdit.aspx?DeptPWBS=", department.DeptPWBS));
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
                        __Department = new _Department(dbTransaction);
                        _DeptEmployee __DeptEmployee = new _DeptEmployee(dbTransaction);
                        department = __Department.GetModel(this.deptId);
                        department._UpdateTime = DateTime.Now;
                        department.DeptCode = this.txtCode.Text;
                        department.DeptName = this.txtName.Text;
                        department.DeptFullName = this.txtFullName.Text;
                        department.DeptAbbr = this.txtAbbr.Text;
                        department.OrderID = Convert.ToInt32((this.txtOrder.Text == "" ? "0" : this.txtOrder.Text));
                        department.UpPosition = this.UpLeader.Text;
                        department.UpPositionId = this.UpLeaderId.Value;
                        string typeID = department.TypeID;
                        department.TypeID = this.listType.SelectedValue;
                        department.DeptState = this.RadioState.SelectedValue;
                        department.DeptAdminId = this.AdminId.Value;
                        department.DeptAdminCn = this.AdminCn.Text;
                        department.DeptSfwId = this.DeptSfwId.Value;
                        department.DeptSfwCn = this.DeptSfwCn.Text;
                        if (this.selPosition.SelectedItem != null)
                        {
                            department.PicPositionId = this.selPosition.SelectedValue;
                            department.PicPosition = this.selPosition.SelectedItem.Text;
                        }
                        if (this.selCalendar.SelectedItem != null)
                        {
                            department.CalendarId = this.selCalendar.SelectedValue;
                        }
                        __Department.Update(department);
                        companyByDeptId = DepartmentService.GetCompanyByDeptId(this.deptId, dbTransaction)._AutoID;
                        str = string.Format("\r\n                        update T_E_Org_Department set CompanyId='{2}' where _AutoId='{0}';\r\n                        update T_E_Org_DeptEmployee set DeptName='{1}',CompanyId='{2}' where DeptId='{0}'", this.deptId, department.DeptName, companyByDeptId);
                        SysDatabase.ExecuteNonQuery(str, dbTransaction);
                        if (typeID != department.TypeID)
                        {
                            DepartmentService.UpdateDeptCompany(department.DeptWBS, dbTransaction);
                        }
                        dbTransaction.Commit();
                        base.Response.Redirect(string.Concat("DefDeptEdit.aspx?DeptPWBS=", department.DeptPWBS));
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
            ListItem listItem;
            AjaxPro.Utility.RegisterTypeForAjax(typeof(DefDeptEdit));
            if (!base.IsPostBack)
            {
                this.deptId = base.GetParaValue("deptId");
                DataTable dataTable = SysDatabase.ExecuteTable("select _AutoId,TypeName from T_E_Org_DeptType order by OrderID");
                this.listType.DataTextField = "TypeName";
                this.listType.DataValueField = "_AutoId";
                this.listType.DataSource = dataTable;
                this.listType.DataBind();
                Dict modelByCode = (new _Dict()).GetModelByCode("_Org_DeptType");
                if (modelByCode != null)
                {
                    List<DictEntry> modelListByDictId = (new _DictEntry()).GetModelListByDictId(modelByCode._AutoID);
                    this.listProp.DataTextField = "ItemName";
                    this.listProp.DataValueField = "ItemCode";
                    this.listProp.DataSource = modelListByDictId;
                    this.listProp.DataBind();
                }
                this.listProp.Items.Insert(0, new ListItem("", ""));
                string picPositionId = "";
                string calendarId = "";
                if (string.IsNullOrEmpty(this.deptId))
                {
                    this.RadioState.SelectedValue = "正常";
                    this.listType.SelectedValue = "7C2F6B38-EDE8-4EB4-B667-6EABE32A1EEF";
                    this.txtOrder.Text = "0";
                }
                else
                {
                    Department model = DepartmentService.GetModel(this.deptId);
                    this.txtCode.Text = model.DeptCode;
                    this.txtName.Text = model.DeptName;
                    this.txtFullName.Text = model.DeptFullName;
                    this.txtWbs.Text = model.DeptWBS;
                    this.txtAbbr.Text = model.DeptAbbr;
                    if ((int)dataTable.Select(string.Concat("_AutoId='", model.TypeID, "'")).Length > 0)
                    {
                        this.listType.SelectedValue = model.TypeID;
                    }
                    this.listProp.SelectedValue = model.DeptProp;
                    this.RadioState.SelectedValue = (model.DeptState == "" ? "正常" : model.DeptState);
                    this.txtOrder.Text = model.OrderID.ToString();
                    this.UpLeader.Text = model.UpPosition;
                    this.UpLeaderId.Value = model.UpPositionId;
                    this.UpLeader2.Text = model.UpEmployeeName;
                    this.UpLeaderId2.Value = model.UpEmployeeId;
                    this.txtPicName2.Text = model.PicEmployeeName2;
                    this.hidPicId2.Value = model.PicEmployeeId2;
                    this.AdminCn.Text = model.DeptAdminCn;
                    this.AdminId.Value = model.DeptAdminId;
                    this.DeptSfwCn.Text = model.DeptSfwCn;
                    this.DeptSfwId.Value = model.DeptSfwId;
                    picPositionId = model.PicPositionId;
                    calendarId = model.CalendarId;
                }
                this.txtWbs.ReadOnly = true;
                this.selPosition.Items.Add(new ListItem("", ""));
                foreach (Position positionByDeptId in PositionService.GetPositionByDeptId(this.deptId))
                {
                    listItem = new ListItem(positionByDeptId.PositionName, positionByDeptId._AutoID);
                    if (picPositionId == positionByDeptId._AutoID)
                    {
                        listItem.Selected = true;
                    }
                    this.selPosition.Items.Add(listItem);
                }

                //递归查询子部门岗位
                FullPositionByDeptId(this.deptId, picPositionId);

                DataTable dataTable1 = SysDatabase.ExecuteTable("select _AutoId,CalendarName from T_E_App_Calendar order by OrderID");
                this.selCalendar.Items.Add(new ListItem("", ""));
                foreach (DataRow row in dataTable1.Rows)
                {
                    listItem = new ListItem(row["CalendarName"].ToString(), row["_AutoId"].ToString());
                    if (calendarId == row["_AutoId"].ToString())
                    {
                        listItem.Selected = true;
                    }
                    this.selCalendar.Items.Add(listItem);
                }
            }
        }

        private void FullPositionByDeptId(string DeptId, string picPositionId)
        {
            DataTable subDept = SysDatabase.ExecuteTable(@"select * from T_E_Org_Department 
                        where  DeptPWBS IN (SELECT DeptWBS FROM T_E_Org_Department WHERE _AutoID='" + DeptId + "')  order by OrderID");
            if ((subDept != null) && (subDept.Rows.Count > 0))
            {
                string subDeptId = subDept.Rows[0]["_AutoID"].ToString();
                string subDeptName = subDept.Rows[0]["DeptName"].ToString();
                foreach (Position positionByDeptId in PositionService.GetPositionByDeptId(subDeptId))
                {
                    ListItem listItem = new ListItem(positionByDeptId.PositionName+"(" + subDeptName + ")", positionByDeptId._AutoID);
                    if (picPositionId == positionByDeptId._AutoID)
                    {
                        listItem.Selected = true;
                    }
                    this.selPosition.Items.Add(listItem);
                }
                FullPositionByDeptId(subDeptId, picPositionId);
            }
        }





    }
}