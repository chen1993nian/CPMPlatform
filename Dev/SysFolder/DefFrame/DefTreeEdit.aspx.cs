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
    public partial class DefTreeEdit : AdminPageBase
    {
        
        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            _AppTree __AppTree;
            AppTree appTree = new AppTree();
            if (string.IsNullOrEmpty(base.GetParaValue("treeId")))
            {
                appTree._AutoID = Guid.NewGuid().ToString();
                appTree._UserName = base.EmployeeID;
                appTree._OrgCode = base.OrgCode;
                appTree._CreateTime = DateTime.Now;
                appTree._UpdateTime = DateTime.Now;
                appTree._IsDel = 0;
                __AppTree = new _AppTree();
                appTree.CatCode = this.txtCatCode.Text;
                appTree.TreeSQL = this.txtTreeSQL.Text;
                appTree.ConnectionId = this.dsList.SelectedValue;
                appTree.Connection = this.dsList.SelectedItem.Text;
                appTree.TreeName = this.txtTreeName.Text;
                appTree.TreeScript = this.txtJs.Text;
                appTree.RootPara = this.rootPara.Text;
                appTree.RootValue = this.rootValue.Text;
                __AppTree.Add(appTree);
                base.ClientScript.RegisterClientScriptBlock(base.GetType(), "", "<script language=javascript>$.noticeAdd({text:'保存成功！',stay:false});</script>");
                base.Response.Flush();
            }
            else
            {
                __AppTree = new _AppTree();
                appTree = __AppTree.GetModel(base.GetParaValue("treeId"));
                appTree.TreeSQL = this.txtTreeSQL.Text;
                appTree.ConnectionId = this.dsList.SelectedValue;
                appTree.Connection = this.dsList.SelectedItem.Text;
                appTree.TreeName = this.txtTreeName.Text;
                appTree.CatCode = this.txtCatCode.Text;
                appTree.RootPara = this.rootPara.Text;
                appTree.RootValue = this.rootValue.Text;
                __AppTree.Update(appTree);
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
                if (string.IsNullOrEmpty(base.GetParaValue("treeId")))
                {
                    this.txtCatCode.Text = base.GetParaValue("nodewbs");
                    modelList = __AppConnection.GetModelList("");
                    this.dsList.Items.Add("");
                    foreach (AppConnection appConnectionA in modelList)
                    {
                        listItem = new ListItem(appConnectionA.DSName, appConnectionA._AutoID);
                        this.dsList.Items.Add(listItem);
                    }
                }
                else
                {
                    AppTree model = (new _AppTree()).GetModel(base.GetParaValue("treeId"));
                    if (model == null)
                    {
                        base.Response.Write("<script>alert('不存在该记录！');window.close();</script>");
                        base.Response.Flush();
                    }
                    else
                    {
                        this.txtTreeName.Text = model.TreeName;
                        this.txtJs.Text = model.TreeScript;
                        this.txtTreeSQL.Text = model.TreeSQL;
                        this.txtCatCode.Text = model.CatCode;
                        this.rootPara.Text = model.RootPara;
                        this.rootValue.Text = model.RootValue;
                        modelList = __AppConnection.GetModelList("");
                        this.dsList.Items.Add("");
                        foreach (AppConnection appConnection1 in modelList)
                        {
                            listItem = new ListItem(appConnection1.DSName, appConnection1._AutoID);
                            if (model.ConnectionId == appConnection1._AutoID)
                            {
                                listItem.Selected = true;
                            }
                            this.dsList.Items.Add(listItem);
                        }
                    }
                }
            }
        }
    }
}