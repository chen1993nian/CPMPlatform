using EIS.AppBase;
using EIS.DataModel.Access;
using EIS.DataModel.Model;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
namespace EIS.Studio.SysFolder.DefFrame
{
    public partial class DefBizEdit4 : AdminPageBase
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
                    if (this.tb_sqlcmd.Text.Trim() != "")
                    {
                        tableInfo.ListSQL = this.tb_sqlcmd.Text;
                    }
                    tableInfo.TableCat = base.GetParaValue("nodewbs");
                    tableInfo.ParentName = this.tb_parent.Text;
                    tableInfo.TableType = 4;
                    tableInfo.DataLog = 0;
                    tableInfo.ConnectionId = this.DropDownList1.SelectedValue;
                    __TableInfo.Add(tableInfo);
                    if (!__TableInfo.Exists())
                    {
                        base.alert("保存出错！");
                    }
                    else
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), "toDefine", string.Concat("<script>toDefine('", tableInfo.TableName, "');</script>"));
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
                tableInfo.ListSQL = this.tb_sqlcmd.Text;
                tableInfo.ConnectionId = this.DropDownList1.SelectedValue;
                tableInfo.DataLog = 0;
                __TableInfo.Update(tableInfo);
                base.ClientScript.RegisterStartupScript(base.GetType(), "changesave", "<script language=javascript>$.noticeAdd({text:'保存成功！',stay:false});</script>");
                base.Response.Flush();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            AppConnection appConnection = null;
            ListItem listItem;
            IList<AppConnection> modelList;
            if (!base.IsPostBack)
            {
                _AppConnection __AppConnection = new _AppConnection();
                if (string.IsNullOrEmpty(base.GetParaValue("tblname")))
                {
                    this.tb_parent.Text = base.GetParaValue("parent");
                    modelList = __AppConnection.GetModelList("");
                    this.DropDownList1.Items.Add("");
                    foreach (AppConnection appConnectionA in modelList)
                    {
                        listItem = new ListItem(appConnectionA.DSName, appConnectionA._AutoID);
                        this.DropDownList1.Items.Add(listItem);
                    }
                }
                else
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
                        this.tb_sqlcmd.Text = model.ListSQL;
                        this.tb_parent.Text = model.ParentName;
                        modelList = __AppConnection.GetModelList("");
                        this.DropDownList1.Items.Add("");
                        foreach (AppConnection appConnection1 in modelList)
                        {
                            listItem = new ListItem(appConnection1.DSName, appConnection1._AutoID);
                            if (model.ConnectionId == appConnection1._AutoID)
                            {
                                listItem.Selected = true;
                            }
                            this.DropDownList1.Items.Add(listItem);
                        }
                    }
                }
            }
        }
    }
}