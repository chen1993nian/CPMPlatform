using EIS.AppBase;
using EIS.DataAccess;
using EIS.WorkFlow.Model;
using EIS.WorkFlow.Service;
using NLog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace EIS.Web.SysFolder.WorkFlow
{
    public partial class SelectWorkFlow : PageBase
    {
        public string TblName = "";

        public string MainId = "";

        public string Infos = "";

 

        protected void Button1_Click(object sender, EventArgs e)
        {
            this.MainId = base.GetParaValue("MainId");
            if (this.ListBox1.SelectedIndex >= 0)
            {
                string selectedValue = this.ListBox1.SelectedValue;
                HttpResponse response = base.Response;
                string[] mainId = new string[] { "NewFlow.aspx?workflowid=", selectedValue, "&AppId=", this.MainId, "&cpro=", base.GetParaValue("cpro") };
                response.Redirect(string.Concat(mainId));
            }
        }

        public bool CheckConfigCondition(string workFlowCode, string TblName, string MainId)
        {
            bool flag;
            string str;
            string str1 = string.Format("select * from T_E_WF_Config where Enable='是' and WFId='{0}'", workFlowCode, base.CompanyId);
            DataTable dataTable = SysDatabase.ExecuteTable(str1);
            if (dataTable.Rows.Count != 0)
            {
                string str2 = dataTable.Rows[0]["condition"].ToString();
                dataTable.Rows[0]["condition2"].ToString();
                string str3 = dataTable.Rows[0]["companyId"].ToString();
                DataTable dataTable1 = SysDatabase.ExecuteTable(string.Format("select * from {0} where _autoid='{1}'", TblName, MainId));
                this.fileLogger.Info(string.Format("select * from {0} where _autoid='{1}'", TblName, MainId));
                if (str3 == "")
                {
                    if (str2 != "")
                    {
                        str2 = base.ReplaceContext(str2);
                        this.fileLogger.Info(str2);
                        str2 = base.ReplaceWithDataRow(str2, dataTable1.Rows[0]);
                        this.fileLogger.Info(str2);
                        str = string.Concat("select count(*) where ", str2);
                        this.fileLogger.Info(str);
                        flag = (int.Parse(SysDatabase.ExecuteScalar(str).ToString()) <= 0 ? false : true);
                    }
                    else
                    {
                        flag = true;
                    }
                }
                else if (str3 != base.CompanyId)
                {
                    flag = false;
                }
                else if (str2 != "")
                {
                    str2 = base.ReplaceContext(str2);
                    str2 = base.ReplaceWithDataRow(str2, dataTable1.Rows[0]);
                    str = string.Concat("select count(*) where ", str2);
                    flag = (int.Parse(SysDatabase.ExecuteScalar(str).ToString()) <= 0 ? false : true);
                }
                else
                {
                    flag = true;
                }
            }
            else
            {
                flag = true;
            }
            return flag;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Define define = null;
            string value;
            string[] paraValue;
            this.TblName = base.GetParaValue("TblName");
            if (!base.IsPostBack)
            {
                List<Define> defineListByCompanyId = DefineService.GetDefineListByCompanyId(this.TblName, base.CompanyId);
                this.MainId = base.GetParaValue("MainId");
                if (string.IsNullOrEmpty(this.MainId))
                {
                    foreach (Define defineA in defineListByCompanyId)
                    {
                        ListItemCollection items = this.ListBox1.Items;
                        ListItem listItem = new ListItem()
                        {
                            Text = string.Concat(defineA.WorkflowName, "(", defineA.Version, ")"),
                            Value = defineA.WorkflowId
                        };
                        items.Add(listItem);
                    }
                    if (this.ListBox1.Items.Count == 1)
                    {
                        value = this.ListBox1.Items[0].Value;
                        HttpResponse response = base.Response;
                        paraValue = new string[] { "NewFlow.aspx?cpro=", base.GetParaValue("cpro"), "&workflowid=", value, "&AppId=" };
                        response.Redirect(string.Concat(paraValue));
                    }
                }
                else
                {
                    string str = string.Format("select top 1 u._AutoID from T_E_WF_Instance i inner join T_E_WF_UserTask u on i._AutoId=u.InstanceId\r\n                        where i.InstanceState='处理中' and u.TaskState='1' and i.AppId='{0}' and u.OwnerId='{1}'", this.MainId, base.EmployeeID);
                    object obj = SysDatabase.ExecuteScalar(str);
                    if (obj != null)
                    {
                        base.Response.Redirect(string.Concat("DealFlow.aspx?taskId=", obj.ToString()), true);
                    }
                    else if (!InstanceService.IsRunAlready(this.TblName, this.MainId))
                    {
                        foreach (Define define1 in defineListByCompanyId)
                        {
                            if (!this.CheckConfigCondition(define1.WorkflowCode, this.TblName, this.MainId))
                            {
                                continue;
                            }
                            ListItemCollection listItemCollections = this.ListBox1.Items;
                            ListItem listItem1 = new ListItem()
                            {
                                Text = string.Concat(define1.WorkflowName, "(", define1.Version, ")"),
                                Value = define1.WorkflowId
                            };
                            listItemCollections.Add(listItem1);
                        }
                        if (this.ListBox1.Items.Count == 1)
                        {
                            value = this.ListBox1.Items[0].Value;
                            HttpResponse httpResponse = base.Response;
                            paraValue = new string[] { "NewFlow.aspx?workflowid=", value, "&AppId=", this.MainId, "&cpro=", base.GetParaValue("cpro") };
                            httpResponse.Redirect(string.Concat(paraValue));
                        }
                    }
                    else
                    {
                        this.Infos = "该条数据已经发起过流程，不能重复发起";
                        this.Button1.Enabled = false;
                    }
                }
            }
        }
    }
}