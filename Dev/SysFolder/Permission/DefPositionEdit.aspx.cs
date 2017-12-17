using AjaxPro;
using EIS.AppBase;
using EIS.DataAccess;
using EIS.DataModel.Access;
using EIS.DataModel.Model;
using EIS.Permission.Access;
using EIS.Permission.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Runtime.CompilerServices;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace EIS.Studio.SysFolder.Permission
{
    public partial class DefPositionEdit : PageBase
    {
        public string deptName = "";

        public StringBuilder TipMessage = new StringBuilder();

     
        public string positionId
        {
            get
            {
                return this.ViewState["positionId"].ToString();
            }
            set
            {
                this.ViewState["positionId"] = value;
            }
        }

        public DefPositionEdit()
        {
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            DbConnection dbConnection;
            DbTransaction dbTransaction;
            Position position;
            Exception exception;
            if (string.IsNullOrEmpty(this.positionId))
            {
                dbConnection = SysDatabase.CreateConnection();
                dbConnection.Open();
                dbTransaction = dbConnection.BeginTransaction();
                try
                {
                    try
                    {
                        _Position __Position = new _Position(dbTransaction);
                        position = new Position()
                        {
                            _AutoID = Guid.NewGuid().ToString(),
                            _OrgCode = "",
                            _UserName = base.UserName,
                            _CreateTime = DateTime.Now,
                            _UpdateTime = DateTime.Now,
                            _IsDel = 0,
                            PositionCode = this.txtCode.Text,
                            PositionName = this.txtName.Text,
                            OrderID = Convert.ToInt32((this.txtOrder.Text == "" ? "0" : this.txtOrder.Text)),
                            ParentPositionName = this.UpLeader.Text,
                            ParentPositionId = this.UpLeaderId.Value
                        };
                        if (this.listProp.SelectedItem == null)
                        {
                            position.PropId = "";
                            position.PropName = "";
                        }
                        else
                        {
                            position.PropId = this.listProp.SelectedItem.Value;
                            position.PropName = this.listProp.SelectedItem.Text;
                        }
                        if (this.listDJ.SelectedItem == null)
                        {
                            position.RankCode = "";
                            position.RankName = "";
                        }
                        else
                        {
                            position.RankCode = this.listDJ.SelectedItem.Value;
                            position.RankName = this.listDJ.SelectedItem.Text;
                        }
                        position.DeptID = base.GetParaValue("deptId");
                        __Position.Add(position);
                        this.positionId = position._AutoID;
                        dbTransaction.Commit();
                        base.ClientScript.RegisterClientScriptBlock(base.GetType(), "", "success();", true);
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
                        _Position __Position1 = new _Position(dbTransaction);
                        _DeptEmployee __DeptEmployee = new _DeptEmployee(dbTransaction);
                        position = __Position1.GetModel(this.positionId);
                        position._UpdateTime = DateTime.Now;
                        position.PositionCode = this.txtCode.Text;
                        position.PositionName = this.txtName.Text;
                        position.OrderID = Convert.ToInt32((this.txtOrder.Text == "" ? "0" : this.txtOrder.Text));
                        position.ParentPositionName = this.UpLeader.Text;
                        position.ParentPositionId = this.UpLeaderId.Value;
                        if (this.listProp.SelectedItem == null)
                        {
                            position.PropId = "";
                            position.PropName = "";
                        }
                        else
                        {
                            position.PropId = this.listProp.SelectedItem.Value;
                            position.PropName = this.listProp.SelectedItem.Text;
                        }
                        if (this.listDJ.SelectedItem == null)
                        {
                            position.RankCode = "";
                            position.RankName = "";
                        }
                        else
                        {
                            position.RankCode = this.listDJ.SelectedItem.Value;
                            position.RankName = this.listDJ.SelectedItem.Text;
                        }
                        __Position1.Update(position);
                        string str = string.Format("update T_E_Org_DeptEmployee set PositionName='{1}' where positionId='{0}';\r\n                    update T_E_Org_Department set PicPosition='{1}' where PicPositionId='{0}';\r\n                    update T_E_Org_Department set UpPosition='{1}' where UpPositionId='{0}';\r\n                    update T_E_Org_Position set ParentPositionName='{1}' where ParentPositionId='{0}'", this.positionId, position.PositionName);
                        SysDatabase.ExecuteNonQuery(str, dbTransaction);
                        dbTransaction.Commit();
                        base.ClientScript.RegisterClientScriptBlock(base.GetType(), "", "success();", true);
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
            Position position;
            string deptID;
            Exception exception;
            if (string.IsNullOrEmpty(this.positionId))
            {
                dbConnection = SysDatabase.CreateConnection();
                dbConnection.Open();
                dbTransaction = dbConnection.BeginTransaction();
                try
                {
                    try
                    {
                        _Position __Position = new _Position(dbTransaction);
                        position = new Position()
                        {
                            _AutoID = Guid.NewGuid().ToString(),
                            _OrgCode = "",
                            _UserName = base.UserName,
                            _CreateTime = DateTime.Now,
                            _UpdateTime = DateTime.Now,
                            _IsDel = 0,
                            PositionCode = this.txtCode.Text,
                            PositionName = this.txtName.Text,
                            OrderID = Convert.ToInt32((this.txtOrder.Text == "" ? "0" : this.txtOrder.Text)),
                            ParentPositionName = this.UpLeader.Text,
                            ParentPositionId = this.UpLeaderId.Value
                        };
                        if (this.listProp.SelectedItem == null)
                        {
                            position.PropId = "";
                            position.PropName = "";
                        }
                        else
                        {
                            position.PropId = this.listProp.SelectedItem.Value;
                            position.PropName = this.listProp.SelectedItem.Text;
                        }
                        if (this.listDJ.SelectedItem == null)
                        {
                            position.RankCode = "";
                            position.RankName = "";
                        }
                        else
                        {
                            position.RankCode = this.listDJ.SelectedItem.Value;
                            position.RankName = this.listDJ.SelectedItem.Text;
                        }
                        position.DeptID = base.GetParaValue("deptId");
                        __Position.Add(position);
                        this.positionId = position._AutoID;
                        dbTransaction.Commit();
                        deptID = position.DeptID;
                        base.Response.Redirect(string.Concat("DefPositionEdit.aspx?DeptId=", deptID));
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
                        _Position __Position1 = new _Position(dbTransaction);
                        _DeptEmployee __DeptEmployee = new _DeptEmployee(dbTransaction);
                        position = __Position1.GetModel(this.positionId);
                        position._UpdateTime = DateTime.Now;
                        position.PositionCode = this.txtCode.Text;
                        position.PositionName = this.txtName.Text;
                        position.OrderID = Convert.ToInt32((this.txtOrder.Text == "" ? "0" : this.txtOrder.Text));
                        position.ParentPositionName = this.UpLeader.Text;
                        position.ParentPositionId = this.UpLeaderId.Value;
                        if (this.listProp.SelectedItem == null)
                        {
                            position.PropId = "";
                            position.PropName = "";
                        }
                        else
                        {
                            position.PropId = this.listProp.SelectedItem.Value;
                            position.PropName = this.listProp.SelectedItem.Text;
                        }
                        if (this.listDJ.SelectedItem == null)
                        {
                            position.RankCode = "";
                            position.RankName = "";
                        }
                        else
                        {
                            position.RankCode = this.listDJ.SelectedItem.Value;
                            position.RankName = this.listDJ.SelectedItem.Text;
                        }
                        __Position1.Update(position);
                        string str = string.Format("update T_E_Org_DeptEmployee set PositionName='{1}' where positionId='{0}';\r\n                    update T_E_Org_Department set PicPosition='{1}' where PicPositionId='{0}';\r\n                    update T_E_Org_Department set UpPosition='{1}' where UpPositionId='{0}';\r\n                    update T_E_Org_Position set ParentPositionName='{1}' where ParentPositionId='{0}'", this.positionId, position.PositionName);
                        SysDatabase.ExecuteNonQuery(str, dbTransaction);
                        dbTransaction.Commit();
                        deptID = position.DeptID;
                        base.Response.Redirect(string.Concat("DefPositionEdit.aspx?DeptId=", deptID));
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
            int maxOrder;
            AjaxPro.Utility.RegisterTypeForAjax(typeof(DefPositionEdit));
            if (!base.IsPostBack)
            {
                this.positionId = base.GetParaValue("PositionId");
                DataTable dataTable = SysDatabase.ExecuteTable("select _AutoID,RoleName from T_E_Org_Role where roleType='岗位属性' order by OrderId");
                this.listProp.DataTextField = "RoleName";
                this.listProp.DataValueField = "_AutoId";
                this.listProp.DataSource = dataTable;
                this.listProp.DataBind();
                this.listProp.Items.Insert(0, new ListItem("", ""));
                this.listProp.SelectedIndex = 0;
                Dict modelByCode = (new _Dict()).GetModelByCode("_Org_PosGrade");
                List<DictEntry> dictEntries = new List<DictEntry>();
                if (modelByCode != null)
                {
                    dictEntries = (new _DictEntry()).GetModelListByDictId(modelByCode._AutoID);
                    this.listDJ.DataTextField = "ItemName";
                    this.listDJ.DataValueField = "ItemCode";
                    this.listDJ.DataSource = dictEntries;
                    this.listDJ.DataBind();
                }
                this.listDJ.Items.Insert(0, new ListItem("", ""));
                this.listDJ.SelectedIndex = 0;
                _Position __Position = new _Position();
                if (string.IsNullOrEmpty(this.positionId))
                {
                    string paraValue = base.GetParaValue("deptId");
                    maxOrder = __Position.GetMaxOrder(paraValue) + 1;
                    this.txtOrder.Text = maxOrder.ToString();
                }
                else
                {
                    Position model = __Position.GetModel(this.positionId);
                    this.txtCode.Text = model.PositionCode;
                    this.txtName.Text = model.PositionName;
                    if ((int)dataTable.Select(string.Concat("_AutoId='", model.PropId, "'")).Length > 0)
                    {
                        this.listProp.SelectedValue = model.PropId;
                    }
                    if (dictEntries.FindIndex((DictEntry dictEntry_0) => dictEntry_0.ItemCode == model.RankCode) > -1)
                    {
                        this.listDJ.SelectedValue = model.RankCode;
                    }
                    TextBox str = this.txtOrder;
                    maxOrder = model.OrderID;
                    str.Text = maxOrder.ToString();
                    this.UpLeader.Text = model.ParentPositionName;
                    this.UpLeaderId.Value = model.ParentPositionId;
                }
            }
        }
    }
}