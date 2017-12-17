using EIS.AppBase;
using EIS.DataAccess;
using EIS.DataModel.Access;
using EIS.DataModel.Model;
using EIS.DataModel.Service;
using EIS.Permission.Service;
using System;
using System.Collections;
using System.Data;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Studio.JZY.Doc
{
    public partial class DocFolderEdit : PageBase
    {
        private _FolderInfo _FolderInfo_0 = new _FolderInfo();

        public StringBuilder sbList = new StringBuilder();

        public StringBuilder arrLimit = new StringBuilder();

        private string string_0 = "";

        private int int_0 = 0;

    

        public string folderId
        {
            get
            {
                string str;
                str = (this.ViewState["folderId"] == null ? "" : this.ViewState["folderId"].ToString());
                return str;
            }
            set
            {
                this.ViewState["folderId"] = value;
            }
        }



        protected void Button2_Click(object sender, EventArgs e)
        {
            FolderInfo folderInfo;
            if (this.folderId == "")
            {
                folderInfo = new FolderInfo(base.UserInfo)
                {
                    FolderName = this.TextBox2.Text,
                    FolderWBS = FolderService.GetNewFolderWbsCode(this.string_0),
                    Inherit = this.RadioButtonList1.SelectedValue,
                    OrderID = int.Parse((this.TextBox3.Text == "" ? "0" : this.TextBox3.Text)),
                    FolderPID = base.GetParaValue("folderPId")
                };
                this._FolderInfo_0.Add(folderInfo);
                this.folderId = folderInfo._AutoID;
                base.ClientScript.RegisterStartupScript(typeof(DocFolderEdit), "", "afterNew();", true);
            }
            else
            {
                folderInfo = this._FolderInfo_0.GetModel(this.folderId);
                folderInfo.FolderName = this.TextBox2.Text;
                folderInfo.OrderID = int.Parse((this.TextBox3.Text == "" ? "0" : this.TextBox3.Text));
                folderInfo.Inherit = this.RadioButtonList1.SelectedValue;
                folderInfo._UpdateTime = DateTime.Now;
                this._FolderInfo_0.Update(folderInfo);
                base.ClientScript.RegisterStartupScript(typeof(DocFolderEdit), "", "afterSave();", true);
            }
            string value = this.LimitData.Value;
            char[] chrArray = new char[] { '$' };
            string[] strArrays = value.Split(chrArray);
            if ((int)strArrays.Length > 0)
            {
                StringBuilder stringBuilder = new StringBuilder();
                string[] strArrays1 = strArrays;
                for (int i = 0; i < (int)strArrays1.Length; i++)
                {
                    string str = strArrays1[i];
                    if (str.Length > 0)
                    {
                        chrArray = new char[] { '|' };
                        string[] strArrays2 = str.Split(chrArray);
                        if (strArrays2[0] == "new")
                        {
                            object[] employeeID = new object[] { base.EmployeeID, base.OrgCode, this.folderId, strArrays2[2], strArrays2[3], strArrays2[4] };
                            stringBuilder.AppendFormat("insert T_E_File_FolderSafe (_AutoID,_UserName,_OrgCode,_CreateTime,_UpdateTime,_IsDel,FolderID,ObjID,ObjType,Limit)\r\n                             values (newid(),'{0}','{1}',getdate(),getdate(),0,'{2}','{3}',{4},'{5}');", employeeID);
                        }
                        else if (strArrays2[0] == "update")
                        {
                            stringBuilder.AppendFormat("update T_E_File_FolderSafe set Limit='{1}' where _AutoID='{0}';", strArrays2[1], strArrays2[4]);
                        }
                        else if (strArrays2[0] == "delete")
                        {
                            stringBuilder.AppendFormat("delete T_E_File_FolderSafe where _AutoID='{0}';", strArrays2[1]);
                        }
                    }
                }
                if (stringBuilder.Length > 0)
                {
                    SysDatabase.ExecuteNonQuery(stringBuilder.ToString());
                }
            }
            this.method_0();
        }

        private void method_0()
        {
            string str = string.Format("select _autoId, Limit,objId,objType from T_E_File_FolderSafe where FolderId='{0}' order by objType", this.folderId);
            foreach (DataRow row in SysDatabase.ExecuteTable(str).Rows)
            {
                string str1 = row["objtype"].ToString();
                string str2 = row["limit"].ToString();
                StringBuilder stringBuilder = this.arrLimit;
                object[] item = new object[] { row["_autoid"], row["objid"], row["objtype"], row["limit"] };
                stringBuilder.AppendFormat("{{autoid:'{0}',objid:'{1}',objtype:'{2}',limit:'{3}',state:'unchanged'}},", item);
                if (str1 == "0")
                {
                    this.sbList.AppendFormat("<tr oindex='{0}'><td>全体员工</td>", this.int_0);
                }
                else if (str1 == "1")
                {
                    this.sbList.AppendFormat("<tr oindex='{0}'><td>{1}</td>", this.int_0, EmployeeService.GetEmployeeName(row["objid"].ToString()));
                }
                else if (str1 == "2")
                {
                    this.sbList.AppendFormat("<tr oindex='{0}'><td>{1}</td>", this.int_0, DepartmentService.GetDepartmentName(row["objid"].ToString()));
                }
                else if (str1 == "3")
                {
                    this.sbList.AppendFormat("<tr oindex='{0}'><td>{1}</td>", this.int_0, RoleService.GetRoleName(row["objid"].ToString()));
                }
                this.sbList.AppendFormat("<td><select class='selLimit' oindex='{0}' size='1'>{1}</select></td>", this.int_0, this.method_1(str2));
                this.sbList.AppendFormat("<td><a class='linkdel' href='javascript:' oindex='{0}'>删除</a></td></tr>", this.int_0);
                DocFolderEdit int0 = this;
                int0.int_0 = int0.int_0 + 1;
            }
            if (this.arrLimit.Length > 0)
            {
                this.arrLimit.Length = this.arrLimit.Length - 1;
            }
        }

        private string method_1(string string_1)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendFormat("<option value='0' {0}>无权限</option>", (string_1 == "0" ? "selected" : ""));
            stringBuilder.AppendFormat("<option value='1' {0}>可见</option>", (string_1 == "1" ? "selected" : ""));
            stringBuilder.AppendFormat("<option value='2' {0}>读取</option>", (string_1 == "2" ? "selected" : ""));
            stringBuilder.AppendFormat("<option value='3' {0}>下载</option>", (string_1 == "3" ? "selected" : ""));
            stringBuilder.AppendFormat("<option value='6' {0}>编辑</option>", (string_1 == "6" ? "selected" : ""));
            stringBuilder.AppendFormat("<option value='9' {0}>全部</option>", (string_1 == "9" ? "selected" : ""));
            return stringBuilder.ToString();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.string_0 = base.GetParaValue("folderPId");
            if (!base.IsPostBack)
            {
                this.folderId = base.GetParaValue("folderId");
                FolderInfo folderModel = FolderService.GetFolderModel(this.folderId);
                if ((folderModel == null ? true : !(this.folderId != "")))
                {
                    TextBox textBox3 = this.TextBox3;
                    int newOrd = this._FolderInfo_0.GetNewOrd(this.string_0);
                    textBox3.Text = newOrd.ToString();
                    this.RadioButtonList1.SelectedValue = "1";
                    this.TextBox1.Text = FolderService.GetNewFolderWbsCode(this.string_0);
                }
                else
                {
                    this.TextBox2.Text = folderModel.FolderName;
                    this.TextBox3.Text = folderModel.OrderID.ToString();
                    this.TextBox1.Text = folderModel.FolderWBS;
                    this.RadioButtonList1.SelectedValue = (folderModel.Inherit == "0" ? "0" : "1");
                }
                this.method_0();
            }
        }
    }
}