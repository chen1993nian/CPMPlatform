using EIS.AppBase;
using EIS.DataAccess;
using EIS.DataModel.Access;
using EIS.DataModel.Model;
using EIS.DataModel.Service;
using EIS.Permission.Service;
using System;
using System.Collections.Specialized;
using System.Data;
using System.Data.Common;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace EIS.Web.WorkAsp.Personal
{
    public partial class PersonalRelation : PageBase
    {
       

        public string companyId = "";

        public string deptId = "";

        public string deptName = "";

        public StringBuilder TipMessage = new StringBuilder();

        public StringBuilder sbUnderling = new StringBuilder();

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

     
        public bool InArray(string[] string_0, string string_1)
        {
            bool flag;
            string[] string0 = string_0;
            int num = 0;
            while (true)
            {
                if (num >= (int)string0.Length)
                {
                    flag = false;
                    break;
                }
                else if (string0[num] == string_1)
                {
                    flag = true;
                    break;
                }
                else
                {
                    num++;
                }
            }
            return flag;
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            StringCollection myLeader;
            string[] strArrays;
            char[] chrArray;
            string[] strArrays1;
            int i;
            string str;
            EmployeeRelation employeeRelation;
            if (!string.IsNullOrEmpty(this.EditEmployeeId))
            {
                DbConnection dbConnection = SysDatabase.CreateConnection();
                dbConnection.Open();
                DbTransaction dbTransaction = dbConnection.BeginTransaction();
                try
                {
                    try
                    {
                        string str1 = "";
                        _EmployeeRelation __EmployeeRelation = new _EmployeeRelation(dbTransaction);
                        string value = this.txtLeaderId.Value;
                        if (value.Length <= 0)
                        {
                            str1 = string.Format("delete T_E_App_Relation where relation='0' and employeeId='{0}'", base.EmployeeID);
                            SysDatabase.ExecuteNonQuery(str1, dbTransaction);
                        }
                        else
                        {
                            myLeader = EmployeeRelationService.GetMyLeader(base.EmployeeID, dbTransaction);
                            strArrays = this.method_0(myLeader);
                            str1 = string.Format("delete T_E_App_Relation where relation='0' and employeeId='{0}' and LeaderId not in ({1})", base.EmployeeID, Utility.GetSplitQuoteString(value, ","));
                            SysDatabase.ExecuteNonQuery(str1, dbTransaction);
                            StringCollection stringCollections = new StringCollection();
                            chrArray = new char[] { ',' };
                            strArrays1 = value.Split(chrArray);
                            for (i = 0; i < (int)strArrays1.Length; i++)
                            {
                                str = strArrays1[i];
                                string str2 = strArrays[0];
                                chrArray = new char[] { ',' };
                                if (!this.InArray(str2.Split(chrArray), str))
                                {
                                    employeeRelation = new EmployeeRelation(base.UserInfo)
                                    {
                                        LeaderId = str,
                                        LeaderName = EmployeeService.GetEmployeeName(str),
                                        EmployeeId = base.EmployeeID,
                                        EmployeeName = base.EmployeeName,
                                        Relation = "0",
                                        State = "1"
                                    };
                                    __EmployeeRelation.Add(employeeRelation);
                                }
                            }
                        }
                        string value1 = this.txtHelperId.Value;
                        if (value1.Length <= 0)
                        {
                            str1 = string.Format("delete T_E_App_Relation where relation='1' and leaderId='{0}'", base.EmployeeID);
                            SysDatabase.ExecuteNonQuery(str1, dbTransaction);
                        }
                        else
                        {
                            myLeader = EmployeeRelationService.GetMyHelper(base.EmployeeID, dbTransaction);
                            strArrays = this.method_0(myLeader);
                            str1 = string.Format("delete T_E_App_Relation where relation='1' and leaderId='{0}' and employeeId not in ({1})", base.EmployeeID, Utility.GetSplitQuoteString(value1, ","));
                            SysDatabase.ExecuteNonQuery(str1, dbTransaction);
                            StringCollection stringCollections1 = new StringCollection();
                            chrArray = new char[] { ',' };
                            strArrays1 = value1.Split(chrArray);
                            for (i = 0; i < (int)strArrays1.Length; i++)
                            {
                                str = strArrays1[i];
                                string str3 = strArrays[0];
                                chrArray = new char[] { ',' };
                                if (!this.InArray(str3.Split(chrArray), str))
                                {
                                    employeeRelation = new EmployeeRelation(base.UserInfo)
                                    {
                                        LeaderId = base.EmployeeID,
                                        LeaderName = base.EmployeeName,
                                        EmployeeId = str,
                                        EmployeeName = EmployeeService.GetEmployeeName(str),
                                        Relation = "1",
                                        State = "1"
                                    };
                                    __EmployeeRelation.Add(employeeRelation);
                                }
                            }
                        }
                        if (this.txtColleagueId.Value.Length > 0)
                        {
                            myLeader = EmployeeRelationService.GetMyColleague(base.EmployeeID, dbTransaction);
                            str1 = string.Format("select * from T_E_Org_Group where EmployeeId='{0}' and GroupType='mostuse'", base.EmployeeID);
                            DataTable dataTable = SysDatabase.ExecuteTable(str1);
                            if (dataTable.Rows.Count <= 0)
                            {
                                StringBuilder stringBuilder = new StringBuilder();
                                stringBuilder.Append("Insert T_E_Org_Group (\r\n \t\t\t\t\t                _AutoID,\r\n\t\t\t\t\t                _UserName,\r\n\t\t\t\t\t                _OrgCode,\r\n\t\t\t\t\t                _CreateTime,\r\n\t\t\t\t\t                _UpdateTime,\r\n\t\t\t\t\t                _IsDel,\r\n\t\t\t\t\t                EmployeeId,\r\n\t\t\t\t\t                EmployeeName,\r\n\t\t\t\t\t                GroupType,\r\n\t\t\t\t\t                UserId,\r\n                                    UserName,\r\n                                    UserPosId,\r\n                                    GroupName,\r\n                                    CompanyId,\r\n                                    OrderId\r\n\r\n\t\t\t                ) values(\r\n\t\t\t\t\t                @_AutoID,\r\n\t\t\t\t\t                @_UserName,\r\n\t\t\t\t\t                @_OrgCode,\r\n\t\t\t\t\t                @_CreateTime,\r\n\t\t\t\t\t                @_UpdateTime,\r\n\t\t\t\t\t                @_IsDel,\r\n\t\t\t\t\t                @EmployeeId,\r\n\t\t\t\t\t                @EmployeeName,\r\n\t\t\t\t\t                @GroupType,\r\n\t\t\t\t\t                @UserId,\r\n                                    @UserName,\r\n                                    @UserPosId,\r\n                                    @GroupName,\r\n                                    @CompanyId,\r\n                                    @OrderId\r\n\t\t\t                )");
                                DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
                                Guid guid = Guid.NewGuid();
                                SysDatabase.AddInParameter(sqlStringCommand, "@_AutoID", DbType.String, guid.ToString());
                                SysDatabase.AddInParameter(sqlStringCommand, "@_UserName", DbType.String, base.EmployeeID);
                                SysDatabase.AddInParameter(sqlStringCommand, "@_OrgCode", DbType.String, base.OrgCode);
                                SysDatabase.AddInParameter(sqlStringCommand, "@_CreateTime", DbType.DateTime, DateTime.Now);
                                SysDatabase.AddInParameter(sqlStringCommand, "@_UpdateTime", DbType.DateTime, DateTime.Now);
                                SysDatabase.AddInParameter(sqlStringCommand, "@_IsDel", DbType.Int32, 0);
                                SysDatabase.AddInParameter(sqlStringCommand, "@EmployeeId", DbType.String, base.EmployeeID);
                                SysDatabase.AddInParameter(sqlStringCommand, "@EmployeeName", DbType.String, base.EmployeeName);
                                SysDatabase.AddInParameter(sqlStringCommand, "@GroupType", DbType.String, "mostuse");
                                SysDatabase.AddInParameter(sqlStringCommand, "@UserId", DbType.String, this.txtColleagueId.Value);
                                SysDatabase.AddInParameter(sqlStringCommand, "@UserName", DbType.String, this.txtColleague.Text);
                                SysDatabase.AddInParameter(sqlStringCommand, "@UserPosId", DbType.String, this.txtPosId.Value);
                                SysDatabase.AddInParameter(sqlStringCommand, "@GroupName", DbType.String, "常用联系人");
                                SysDatabase.AddInParameter(sqlStringCommand, "@CompanyId", DbType.String, "");
                                SysDatabase.AddInParameter(sqlStringCommand, "@OrderId", DbType.Int32, 0);
                                SysDatabase.ExecuteNonQuery(sqlStringCommand, dbTransaction);
                            }
                            else
                            {
                                string str4 = dataTable.Rows[0]["_autoId"].ToString();
                                object[] objArray = new object[] { this.txtColleagueId.Value, this.txtColleague.Text, str4, this.txtPosId.Value };
                                str1 = string.Format("Update T_E_Org_Group set UserId='{0}',UserName='{1}',UserPosId='{3}' where _autoId='{2}'", objArray);
                                SysDatabase.ExecuteNonQuery(str1, dbTransaction);
                            }
                        }
                        dbTransaction.Commit();
                        base.ClientScript.RegisterClientScriptBlock(base.GetType(), "", "<script language=javascript>$.noticeAdd({text:'保存成功！',stay:false});</script>");
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

        private string[] method_0(StringCollection stringCollection_0)
        {
            StringBuilder stringBuilder = new StringBuilder();
            StringBuilder length = new StringBuilder();
            foreach (string stringCollection0 in stringCollection_0)
            {
                string[] strArrays = stringCollection0.Split(new char[] { '|' });
                stringBuilder.AppendFormat("{0},", strArrays[0]);
                length.AppendFormat("{0},", strArrays[1]);
            }
            if (stringBuilder.Length > 1)
            {
                stringBuilder.Length = stringBuilder.Length - 1;
                length.Length = length.Length - 1;
            }
            string[] str = new string[] { stringBuilder.ToString(), length.ToString() };
            return str;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                this.EditEmployeeId = base.EmployeeID;
                if (!string.IsNullOrEmpty(this.EditEmployeeId))
                {
                    StringBuilder stringBuilder = new StringBuilder();
                    StringBuilder stringBuilder1 = new StringBuilder();
                    StringCollection myLeader = EmployeeRelationService.GetMyLeader(base.EmployeeID);
                    string[] strArrays = this.method_0(myLeader);
                    this.txtLeader.Text = strArrays[1];
                    this.txtLeaderId.Value = strArrays[0];
                    myLeader = EmployeeRelationService.GetMyHelper(base.EmployeeID, null);
                    strArrays = this.method_0(myLeader);
                    this.txtHelper.Text = strArrays[1];
                    this.txtHelperId.Value = strArrays[0];
                    myLeader = EmployeeRelationService.GetMyUnderling(base.EmployeeID, null);
                    strArrays = this.method_0(myLeader);
                    this.sbUnderling.Append(strArrays[1]);
                    string str = string.Format("select * from T_E_Org_Group where EmployeeId='{0}' and GroupType='mostuse'", base.EmployeeID);
                    DataTable dataTable = SysDatabase.ExecuteTable(str);
                    if (dataTable.Rows.Count > 0)
                    {
                        this.txtColleague.Text = dataTable.Rows[0]["UserName"].ToString();
                        this.txtColleagueId.Value = dataTable.Rows[0]["UserId"].ToString();
                        this.txtPosId.Value = dataTable.Rows[0]["UserPosId"].ToString();
                    }
                }
            }
        }
    }
}