using EIS.AppBase;
using EIS.DataAccess;
using EIS.DataModel.Access;
using EIS.DataModel.Model;
using EIS.Permission.Access;
using EIS.Permission.Model;
using EIS.Permission.Service;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace EIS.Web.WorkAsp.Personal
{
    public partial class PersonalInfo : PageBase
    {
        public string companyId = "";

        public string deptId = "";

        public string deptName = "";

        public string signPath = "";

        public string photoPath = "";

        public StringBuilder TipMessage = new StringBuilder();




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


        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            string str;
            string str1;
            string appFileSavePath;
            string str2;
            if (!string.IsNullOrEmpty(this.EditEmployeeId))
            {
                DbConnection dbConnection = SysDatabase.CreateConnection();
                dbConnection.Open();
                DbTransaction dbTransaction = dbConnection.BeginTransaction();
                try
                {
                    try
                    {
                        _Employee __Employee = new _Employee(dbTransaction);
                        _DeptEmployee __DeptEmployee = new _DeptEmployee(dbTransaction);
                        Employee model = EmployeeService.GetModel(this.EditEmployeeId);
                        model.EmployeeName = this.txtEmpName.Text;
                        model.EmployeeCode = this.txtEmpCode.Text;
                        model.Sex = this.selSex.SelectedValue;
                        if (this.txtBirthDay.Text != "")
                        {
                            model.Birthday = new DateTime?(Convert.ToDateTime(this.txtBirthDay.Text));
                        }
                        model.Officephone = this.txtOfficePhone.Text;
                        model.EMail = this.txtEmail.Text;
                        model.Cellphone = this.txtMobile.Text;
                        model.Homephone = this.txtHomePhone.Text;
                        model.Address = this.txtOfficeAddress.Text;
                        model.ZipCode = this.txtPostCode.Text;
                        model.IdCard = this.txtIdCard.Text;
                        model.HideMobile = (this.chkHideMobile.Checked ? 1 : 0);
                       // model.OutList = (this.chkHideAll.Checked ? "是" : "否");
                        if (this.FileUpload1.HasFile)
                        {
                            appFileSavePath = AppSettings.Instance.AppFileSavePath;
                            str = "T_E_Org_Employee";
                            if (!Directory.Exists(string.Concat(appFileSavePath, str)))
                            {
                                Directory.CreateDirectory(string.Concat(appFileSavePath, str));
                            }
                            str1 = string.Concat(str, "\\", model.LoginName, Path.GetExtension(this.FileUpload1.FileName));
                            model.SignId = str1;
                            model.BasePath = AppSettings.Instance.AppFileBaseCode;
                            str2 = string.Concat(appFileSavePath, str1);
                            this.FileUpload1.SaveAs(str2);
                        }
                        if (this.FileUpload2.HasFile)
                        {
                            appFileSavePath = AppSettings.Instance.AppFileSavePath;
                            str = "T_E_Org_Employee";
                            if (!Directory.Exists(string.Concat(appFileSavePath, str)))
                            {
                                Directory.CreateDirectory(string.Concat(appFileSavePath, str));
                            }
                            string str3 = string.Concat("p_", model.LoginName, Path.GetExtension(this.FileUpload2.FileName));
                            str1 = string.Concat(str, "\\", str3);
                            str2 = string.Concat(appFileSavePath, str1);
                            this.FileUpload2.SaveAs(str2);
                            if (string.IsNullOrEmpty(model.PhotoId))
                            {
                                model.PhotoId = Guid.NewGuid().ToString();
                            }
                            AppFile appFile = new AppFile()
                            {
                                _AutoID = Guid.NewGuid().ToString(),
                                _UserName = base.EmployeeID,
                                _OrgCode = base.OrgCode,
                                _CreateTime = DateTime.Now,
                                _UpdateTime = DateTime.Now,
                                _IsDel = 0,
                                FileName = str3,
                                FactFileName = str3,
                                FilePath = str1,
                                BasePath = AppSettings.Instance.AppFileBaseCode,
                                DownCount = 0,
                                FileSize = (int)this.FileUpload2.FileContent.Length,
                                FileType = Path.GetExtension(this.FileUpload2.FileName).ToLower(),
                                FolderID = "",
                                AppId = model.PhotoId,
                                AppName = "T_E_Org_Employee"
                            };
                            (new _AppFile()).Add(appFile);
                        }
                        __Employee.Update(model);
                        dbTransaction.Commit();
                        if (!string.IsNullOrEmpty(this.PositionList.SelectedValue))
                        {
                            DeptEmployeeService.UpdateDefaultPosition(base.EmployeeID, this.PositionList.SelectedValue);
                        }
                        if (model.PhotoId != "")
                        {
                            object[] photoId = new object[] { "../../SysFolder/Common/FileDown.aspx?appId=", model.PhotoId, "&rnd=", null };
                            photoId[3] = DateTime.Now.Ticks;
                            this.photoPath = string.Concat(photoId);
                        }
                        else
                        {
                            this.photoPath = "../../Img/bbs/head_80.jpg";
                        }
                        base.ClientScript.RegisterClientScriptBlock(base.GetType(), "", "$.noticeAdd({text:'保存成功！',stay:false});", true);
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
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            DateTime value;
            string str;
            if (!base.IsPostBack)
            {
                this.EditEmployeeId = base.EmployeeID;
                if (!string.IsNullOrEmpty(this.EditEmployeeId))
                {
                    Employee model = EmployeeService.GetModel(this.EditEmployeeId);
                    this.txtEmpName.Text = model.EmployeeName;
                    this.txtEmpCode.Text = model.EmployeeCode;
                    this.selSex.SelectedValue = (model.Sex == "" ? "男" : model.Sex);
                    TextBox textBox = this.txtBirthDay;
                    if (model.Birthday.HasValue)
                    {
                        value = model.Birthday.Value;
                        str = value.ToString("yyyy-MM-dd");
                    }
                    else
                    {
                        str = "";
                    }
                    textBox.Text = str;
                    this.txtHomePhone.Text = model.Homephone;
                    this.txtOfficeAddress.Text = model.Address;
                    this.txtPostCode.Text = model.ZipCode;
                    this.txtOfficePhone.Text = model.Officephone;
                    this.txtEmail.Text = model.EMail;
                    this.txtMobile.Text = model.Cellphone;
                    this.txtIdCard.Text = model.IdCard;
                    string positionId = model.PositionId;
                    this.chkHideMobile.Checked = model.HideMobile == 1;
                   // this.chkHideAll.Checked = model.OutList == "是";
                    List<DeptEmployee> deptEmployeeByEmployeeId = DeptEmployeeService.GetDeptEmployeeByEmployeeId(this.EditEmployeeId);
                    foreach (DeptEmployee deptEmployee in deptEmployeeByEmployeeId)
                    {
                        string str1 = "";
                        string departmentName = DepartmentService.GetDepartmentName(deptEmployee.CompanyID);
                        if (departmentName != deptEmployee.DeptName)
                        {
                            string[] deptName = new string[] { departmentName, "－", deptEmployee.DeptName, "－", deptEmployee.PositionName };
                            str1 = string.Concat(deptName);
                        }
                        else
                        {
                            str1 = string.Concat(deptEmployee.DeptName, "－", deptEmployee.PositionName);
                        }
                        ListItem listItem = new ListItem()
                        {
                            Text = str1,
                            Value = deptEmployee.PositionId
                        };
                        ListItem listItem1 = listItem;
                        if (deptEmployee.IsDefault == 1)
                        {
                            listItem1.Selected = true;
                        }
                        this.PositionList.Items.Add(listItem1);
                    }
                    if ((deptEmployeeByEmployeeId.Count <= 0 ? false : this.PositionList.SelectedValue == ""))
                    {
                        this.PositionList.SelectedIndex = 0;
                    }
                    if (model.PhotoId != "")
                    {
                        object[] photoId = new object[] { "../../SysFolder/Common/FileDown.aspx?appId=", model.PhotoId, "&rnd=", null };
                        value = DateTime.Now;
                        photoId[3] = value.Ticks;
                        this.photoPath = string.Concat(photoId);
                    }
                    else
                    {
                        this.photoPath = "../../Img/bbs/head_80.jpg";
                    }
                }
            }
        }
    }
}