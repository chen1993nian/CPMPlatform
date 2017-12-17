using EIS.AppBase;
using EIS.DataModel.Access;
using EIS.DataModel.Model;
using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
namespace EIS.Studio.SysFolder.DefFrame
{
    public partial class DefBizEdit2 : AdminPageBase
    {
        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            _TableInfo __TableInfo;
            TableInfo tableInfo = new TableInfo();
            if (string.IsNullOrEmpty(base.GetParaValue("tblname")))
            {
                tableInfo._AutoID = Guid.NewGuid().ToString();
                tableInfo._UserName = base.EmployeeID;
                tableInfo._OrgCode = base.OrgCode;
                tableInfo._CreateTime = DateTime.Now;
                tableInfo._UpdateTime = DateTime.Now;
                tableInfo._IsDel = 0;
                tableInfo.TableName = this.tb_tablename.Text;
                tableInfo.TableNameCn = this.tb_tablenamecn.Text;
                __TableInfo = new _TableInfo(tableInfo.TableName);
                if (!__TableInfo.Exists())
                {
                    tableInfo.ListSQL = "";
                    tableInfo.TableCat = "";
                    tableInfo.ParentName = this.tb_parent.Text;
                    tableInfo.TableType = 2;
                    tableInfo.DataLog = 0;
                    tableInfo.EditMode = this.rblEditMode.SelectedValue;
                    tableInfo.InitRows = (this.tb_InitRows.Text.Trim() == "" ? 1 : int.Parse(this.tb_InitRows.Text));
                    __TableInfo.Add(tableInfo);
                    (new _TableInfo(tableInfo.ParentName)).SetUpdateTime();
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
                __TableInfo = new _TableInfo(base.GetParaValue("tblname"));
                tableInfo = __TableInfo.GetModel();
                tableInfo.TableNameCn = this.tb_tablenamecn.Text;
                tableInfo.ListSQL = "";
                tableInfo.DataLog = 0;
                tableInfo.EditMode = this.rblEditMode.SelectedValue;
                tableInfo.InitRows = (this.tb_InitRows.Text.Trim() == "" ? 1 : int.Parse(this.tb_InitRows.Text));
                __TableInfo.Update(tableInfo);
                (new _TableInfo(tableInfo.ParentName)).SetUpdateTime();
                base.ClientScript.RegisterStartupScript(base.GetType(), "changesave", "toDefine();", true);
                base.Response.Flush();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                if (!string.IsNullOrEmpty(base.GetParaValue("tblname")))
                {
                    TableInfo model = (new _TableInfo(base.GetParaValue("tblname"))).GetModel();
                    if (model == null)
                    {
                        base.Response.Write("<script>alert('不存在该记录！');window.close();</script>");
                        base.Response.Flush();
                    }
                    else
                    {
                        this.tb_tablename.Text = model.TableName;
                        this.tb_tablenamecn.Text = model.TableNameCn;
                        this.tb_tablename.ReadOnly = true;
                        this.tb_parent.Text = model.ParentName;
                        this.tb_InitRows.Text = model.InitRows.ToString();
                        this.rblEditMode.SelectedValue = (model.EditMode == "1" ? "1" : "0");
                    }
                }
                else
                {
                    this.tb_parent.Text = base.GetParaValue("parent");
                }
            }
        }
    }
}