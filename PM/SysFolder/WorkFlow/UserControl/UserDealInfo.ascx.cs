using EIS.DataAccess;
using EIS.WorkFlow.Service;
using System;
using System.Collections;
using System.Data;
using System.Runtime.CompilerServices;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EIS.Web.SysFolder.WorkFlow.UserControl
{
    public partial class UserDealInfo : System.Web.UI.UserControl
    {
        protected GridView GridView1;

        public string InstanceId
        {
            get;
            set;
        }

        public bool UserComment
        {
            get;
            set;
        }

        public UserDealInfo()
        {
        }

        public bool CanComment(string instanceId)
        {
            string str = string.Format("select count(*) from T_E_WF_Config where isnull(UseComment,'')='是' and  WFId in (\r\n                select WorkflowCode from T_E_WF_Define d inner join T_E_WF_Instance i on d._AutoID=i.WorkflowId \r\n                where i._AutoID='{0}')", instanceId);
            return Convert.ToInt32(SysDatabase.ExecuteScalar(str)) > 0;
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRow row = ((DataRowView)e.Row.DataItem).Row;
                if (row["TaskState"].ToString() == "2")
                {
                    string str = (row["dealAdvice"] == DBNull.Value ? "" : row["dealAdvice"].ToString());
                    if (str != "******")
                    {
                        string str1 = row["_autoId"].ToString();
                        if (row["fileCount"].ToString() != "0")
                        {
                            TableCell item = e.Row.Cells[4];
                            object[] objArray = new object[] { str, str1, row["fileCount"], this.method_0(str1) };
                            item.Text = string.Format("{0}&nbsp;<a class='ideafile' title='查看意见附件' href='../Common/FileListFrame.aspx?appName=&appId={1}&read=1' target='_blank'>[{2}]</a>\r\n                        <a href='javascript:' class='addComment' taskid='{1}'>&nbsp;</a>{3}", objArray);
                        }
                        if (this.UserComment)
                        {
                            TableCell tableCell = e.Row.Cells[4];
                            tableCell.Text = string.Concat(tableCell.Text, string.Format("<a href='javascript:' title='点击添加评论' class='addComment' taskid='{0}'>&nbsp;</a>{1}<span id='anchor_{0}'></span>", str1, this.method_0(str1)));
                        }
                        string str2 = row["memo"].ToString();
                        if (str2 != "")
                        {
                            TableCell item1 = e.Row.Cells[4];
                            item1.Text = string.Concat(item1.Text, "&nbsp;<a href='javascript:' class='linkMemo' title='点击查看备注'>[备注]</a><p style='display:none;' class='uMemo'>", str2, "</p>");
                        }
                    }
                }
                e.Row.Attributes.Add("class", row["NodeCode"].ToString());
            }
        }

        public bool HideDealTime(string instanceId)
        {
            string str = string.Format("select count(*) from T_E_WF_Config where isnull(HideDealTime,'')='是' and  WFId in (\r\n                select WorkflowCode from T_E_WF_Define d inner join T_E_WF_Instance i on d._AutoID=i.WorkflowId \r\n                where i._AutoID='{0}')", instanceId);
            return Convert.ToInt32(SysDatabase.ExecuteScalar(str)) > 0;
        }

        private string method_0(string string_1)
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (DataRow row in SysDatabase.ExecuteTable(string.Format("select * from T_E_WF_Comment where TaskId='{0}' order by _CreateTime", string_1)).Rows)
            {
                object[] item = new object[] { row["_autoid"], row["_createTime"], row["empName"], row["advice"] };
                stringBuilder.AppendFormat("<div class='comment' cid='c_{0}'>{3}<span class='ucomment'>{2}</span><span class='tcomment'>{1:yyyy-MM-dd HH:mm}</span></div>", item);
            }
            return stringBuilder.ToString();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.UserComment = false;
            if (!string.IsNullOrEmpty(this.InstanceId))
            {
                this.UserComment = this.CanComment(this.InstanceId);
                if (this.HideDealTime(this.InstanceId) && this.GridView1.Columns.Count > 6)
                {
                    this.GridView1.Columns.RemoveAt(5);
                    this.GridView1.Columns.RemoveAt(5);
                }
                DataTable userDealState = InstanceService.GetUserDealState(this.InstanceId);
                this.GridView1.DataSource = userDealState;
                this.GridView1.DataBind();
            }
        }
    }
}