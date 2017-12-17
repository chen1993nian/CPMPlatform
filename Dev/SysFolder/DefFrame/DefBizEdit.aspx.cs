using EIS.AppBase;
using EIS.DataAccess;
using EIS.DataModel.Access;
using EIS.DataModel.Model;
using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace EIS.Studio.SysFolder.DefFrame
{
    public partial class DefBizEdit : AdminPageBase
    {

        public string OpInfo = "";

    

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            _TableInfo __TableInfo;
            string str;
            TableInfo tableInfo = new TableInfo();
            if (!string.IsNullOrEmpty(base.GetParaValue("tblname")))
            {
                __TableInfo = new _TableInfo(base.GetParaValue("tblname"));
                tableInfo = __TableInfo.GetModel();
                tableInfo.TableNameCn = this.tb_tablenamecn.Text;
                tableInfo.ListSQL = this.tb_sqlcmd.Text;
                tableInfo.FormWidth = this.tb_Width.Text;
                tableInfo.DetailSQL = this.tb_DetailSQL.Text;
                tableInfo.DataLog = int.Parse(this.rblLog.SelectedValue);
                tableInfo.DataLogTmpl = this.txt_DataLog.Text;
                tableInfo.ShowState = int.Parse(this.rblShow.SelectedValue);
                tableInfo.ArchiveState = int.Parse(this.rblArchive.SelectedValue);
                tableInfo.DeleteMode = int.Parse(this.rblDel.SelectedValue);
                tableInfo.PageRecCount = int.Parse(this.tb_PageRecCount.Text);
                if (this.txtOrder.Text.Trim() == "")
                {
                    tableInfo.OrderField = "";
                }
                else
                {
                    tableInfo.OrderField = string.Concat(this.txtOrder.Text.Trim(), " ", this.ddlOrder.SelectedValue);
                }
                __TableInfo.Update(tableInfo);
                if (tableInfo.ArchiveState == 1 && !__TableInfo.FieldExists("_gdstate"))
                {
                    str = string.Format("alter table {0} add _GdState varchar(50);", tableInfo.TableName);
                    SysDatabase.ExecuteNonQuery(str);
                }
                if (tableInfo.ShowState == 1 && !__TableInfo.FieldExists("_WFCurNode"))
                {
                    str = string.Format("alter table {0} add _WFCurNode NVarchar(200) , _WFCurUser NVarchar(200);", tableInfo.TableName);
                    SysDatabase.ExecuteNonQuery(str);
                }
                base.ClientScript.RegisterStartupScript(base.GetType(), "changesave", "$.noticeAdd({text:'保存成功！',stay:false});", true);
                base.Response.Flush();
            }
            else if ((this.tb_tablename.Text.Trim() == "" ? false : !(this.tb_tablenamecn.Text.Trim() == "")))
            {
                tableInfo._AutoID = Guid.NewGuid().ToString();
                tableInfo._UserName = base.EmployeeID;
                tableInfo._OrgCode = base.OrgCode;
                tableInfo._CreateTime = DateTime.Now;
                tableInfo._UpdateTime = DateTime.Now;
                tableInfo._IsDel = 0;
                tableInfo.TableName = this.tb_tablename.Text;
                tableInfo.TableNameCn = this.tb_tablenamecn.Text;
                tableInfo.FormWidth = this.tb_Width.Text;
                tableInfo.DataLog = int.Parse(this.rblLog.SelectedValue);
                tableInfo.DataLogTmpl = this.txt_DataLog.Text;
                tableInfo.ShowState = int.Parse(this.rblShow.SelectedValue);
                tableInfo.ArchiveState = int.Parse(this.rblArchive.SelectedValue);
                tableInfo.DeleteMode = int.Parse(this.rblDel.SelectedValue);
                if (this.txtOrder.Text.Trim() == "")
                {
                    tableInfo.OrderField = "";
                }
                else
                {
                    tableInfo.OrderField = string.Concat(this.txtOrder.Text.Trim(), " ", this.ddlOrder.SelectedValue);
                }
                __TableInfo = new _TableInfo(tableInfo.TableName);
                if (!__TableInfo.Exists())
                {
                    if (this.tb_sqlcmd.Text.Trim() == "")
                    {
                        tableInfo.ListSQL = string.Concat("select * from ", tableInfo.TableName, " where |^condition^| |^sortdir^|");
                    }
                    else
                    {
                        tableInfo.ListSQL = this.tb_sqlcmd.Text;
                    }
                    if (this.tb_DetailSQL.Text.Trim() == "")
                    {
                        tableInfo.DetailSQL = string.Concat("select * from ", tableInfo.TableName, " where |^condition^|");
                    }
                    else
                    {
                        tableInfo.DetailSQL = this.tb_DetailSQL.Text;
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
            if (!base.IsPostBack && !string.IsNullOrEmpty(base.GetParaValue("tblname")))
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
                    this.tb_Width.Text = model.FormWidth;
                    this.tb_tablenamecn.Text = model.TableNameCn;
                    this.tb_tablename.ReadOnly = true;
                    this.tb_sqlcmd.Text = model.ListSQL;
                    this.tb_DetailSQL.Text = model.DetailSQL;
                    this.tb_PageRecCount.Text = model.PageRecCount.ToString();
                    if (model.OrderField != "")
                    {
                        string[] strArrays = model.OrderField.Split(new char[] { ' ' });
                        if ((int)strArrays.Length == 2)
                        {
                            this.txtOrder.Text = strArrays[0];
                            if (strArrays[1] == "asc")
                            {
                                this.ddlOrder.SelectedIndex = 1;
                            }
                            else if (strArrays[1] != "desc")
                            {
                                this.ddlOrder.SelectedIndex = 0;
                            }
                            else
                            {
                                this.ddlOrder.SelectedIndex = 2;
                            }
                        }
                    }
                    this.txt_DataLog.Text = model.DataLogTmpl;
                    this.rblDel.SelectedValue = model.DeleteMode.ToString();
                    this.rblShow.SelectedValue = model.ShowState.ToString();
                    this.rblArchive.SelectedValue = model.ArchiveState.ToString();
                    this.rblLog.SelectedValue = model.DataLog.ToString();
                }
            }
            if (!base.IsPostBack && string.IsNullOrEmpty(base.GetParaValue("tblname")))
            {
                this.tb_PageRecCount.Text = "15";
            }

        }
    }
}