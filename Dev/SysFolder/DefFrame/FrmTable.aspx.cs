using EIS.AppBase;
using EIS.DataAccess;
using EIS.DataModel.Access;
using EIS.DataModel.Model;
using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EIS.Studio.SysFolder.DefFrame
{
    public partial class FrmTableInfo : PageBase
    {
       
  

        public string OpInfo = "";

    

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            _TableInfo __TableInfo;
            TableInfo tableInfo = new TableInfo();
            if (!string.IsNullOrEmpty(base.GetParaValue("tblName")))
            {
                __TableInfo = new _TableInfo(base.GetParaValue("tblName"));
                tableInfo = __TableInfo.GetModel();
                tableInfo.TableNameCn = this.txtNameCn.Text;
                tableInfo.ListSQL = this.txtListSQL.Text;
                tableInfo.DetailSQL = this.txtDetailSQL.Text;
                tableInfo.DataLog = int.Parse(this.rblLog.SelectedValue);
                tableInfo.DataLogTmpl = this.txtDataLog.Text;
                tableInfo.ShowState = int.Parse(this.rblShow.SelectedValue);
                tableInfo.ArchiveState = int.Parse(this.rblArchive.SelectedValue);
                tableInfo.DeleteMode = int.Parse(this.rblDel.SelectedValue);
                if (this.txtOrder.Text.Trim() != "")
                {
                    tableInfo.OrderField = string.Concat(this.txtOrder.Text.Trim(), " ", this.ddlOrder.SelectedValue);
                }
                __TableInfo.Update(tableInfo);
                if (tableInfo.ShowState == 1 && !__TableInfo.FieldExists("_gdstate"))
                {
                    string str = string.Format("alter table {0} add _GdState varchar(50);", tableInfo.TableName);
                    SysDatabase.ExecuteNonQuery(str);
                }
                base.ClientScript.RegisterStartupScript(base.GetType(), "changesave", "<script language=javascript>$.noticeAdd({text:'保存成功！',stay:false});</script>");
                base.Response.Flush();
            }
            else if ((this.txtTblName.Text.Trim() == "" ? false : !(this.txtNameCn.Text.Trim() == "")))
            {
                tableInfo._AutoID = Guid.NewGuid().ToString();
                tableInfo._UserName = base.EmployeeID;
                tableInfo._OrgCode = base.OrgCode;
                tableInfo._CreateTime = DateTime.Now;
                tableInfo._UpdateTime = DateTime.Now;
                tableInfo._IsDel = 0;
                tableInfo.TableName = this.txtTblName.Text;
                tableInfo.TableNameCn = this.txtNameCn.Text;
                tableInfo.DataLog = int.Parse(this.rblLog.SelectedValue);
                tableInfo.DataLogTmpl = this.txtDataLog.Text;
                tableInfo.ShowState = int.Parse(this.rblShow.SelectedValue);
                tableInfo.ArchiveState = int.Parse(this.rblArchive.SelectedValue);
                tableInfo.DeleteMode = int.Parse(this.rblDel.SelectedValue);
                if (this.txtOrder.Text.Trim() != "")
                {
                    tableInfo.OrderField = string.Concat(this.txtOrder.Text.Trim(), " ", this.ddlOrder.SelectedValue);
                }
                __TableInfo = new _TableInfo(tableInfo.TableName);
                if (!__TableInfo.Exists())
                {
                    if (this.txtListSQL.Text.Trim() == "")
                    {
                        tableInfo.ListSQL = string.Concat("select * from ", tableInfo.TableName, " where |^condition^| |^sortdir^|");
                    }
                    else
                    {
                        tableInfo.ListSQL = this.txtListSQL.Text;
                    }
                    if (this.txtDetailSQL.Text.Trim() == "")
                    {
                        tableInfo.DetailSQL = string.Concat("select * from ", tableInfo.TableName, " where |^condition^|");
                    }
                    else
                    {
                        tableInfo.DetailSQL = this.txtDetailSQL.Text;
                    }
                    tableInfo.TableCat = base.GetParaValue("nodewbs");
                    tableInfo.ParentName = "";
                    tableInfo.TableType = 1;
                    __TableInfo.Add(tableInfo);
                    if (!__TableInfo.Exists())
                    {
                        base.alert("保存出错！");
                    }
                    else
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), "toDefine", string.Concat("toDefine('", tableInfo.TableName, "');"), true);
                        base.Response.Flush();
                    }
                }
                else
                {
                    base.alert(string.Concat(tableInfo.TableName, "已经存在"));
                    base.Response.Flush();
                }
            }
            else
            {
                this.OpInfo = "<div id='errorInfo' class='tip'>业务名称或者中文名称不能为空</div>";
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack && !string.IsNullOrEmpty(base.GetParaValue("tblName")))
            {
                TableInfo model = (new _TableInfo(base.GetParaValue("tblName"))).GetModel();
                if (model == null)
                {
                    base.Response.Write("<script>alert('不存在该记录！');window.close();</script>");
                    base.Response.Flush();
                }
                else
                {
                    this.txtTblName.Text = model.TableName;
                    this.txtNameCn.Text = model.TableNameCn;
                    this.txtTblName.ReadOnly = true;
                    this.txtListSQL.Text = model.ListSQL;
                    this.txtDetailSQL.Text = model.DetailSQL;
                    if (model.OrderField != "")
                    {
                        string[] strArrays = model.OrderField.Split(new char[] { ' ' });
                        if ((int)strArrays.Length == 2)
                        {
                            this.txtOrder.Text = strArrays[0];
                            if (strArrays[1] != "asc")
                            {
                                this.ddlOrder.SelectedIndex = 1;
                            }
                            else
                            {
                                this.ddlOrder.SelectedIndex = 0;
                            }
                        }
                    }
                    this.txtDataLog.Text = model.DataLogTmpl;
                    this.rblDel.SelectedValue = model.DeleteMode.ToString();
                    this.rblShow.SelectedValue = model.ShowState.ToString();
                    this.rblArchive.SelectedValue = model.ArchiveState.ToString();
                    this.rblLog.SelectedValue = model.DataLog.ToString();
                }
            }
        }
    }
}