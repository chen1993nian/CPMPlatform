using EIS.AppBase;
using EIS.DataAccess;
using EIS.DataModel.Access;
using EIS.DataModel.Model;
using EIS.Permission.Service;
using System;
using System.Collections;
using System.Data;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace EIS.Web.Doc
{
    public partial class DocFileEdit : PageBase
    {
        private _AppFile _AppFile_0 = new _AppFile();

        public StringBuilder sbList = new StringBuilder();

        public StringBuilder arrLimit = new StringBuilder();

        private int int_0 = 0;

      
        public string fileId
        {
            get
            {
                string str;
                str = (this.ViewState["fileId"] == null ? "" : this.ViewState["fileId"].ToString());
                return str;
            }
            set
            {
                this.ViewState["fileId"] = value;
            }
        }

        public DocFileEdit()
        {
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            AppFile model = this._AppFile_0.GetModel(this.fileId);
            model.FactFileName = this.TextBox2.Text;
            model.OrderId = int.Parse((this.TextBox3.Text == "" ? "0" : this.TextBox3.Text));
            model.Inherit = this.RadioButtonList1.SelectedValue;
            model._UpdateTime = DateTime.Now;
            this._AppFile_0.Update(model);
            //base.ClientScript.RegisterStartupScript(typeof(DocFolderEdit), "", "afterSave();", true);
            base.ClientScript.RegisterStartupScript(typeof(DocFileEdit), "", "afterSave();", true);
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
                            object[] employeeID = new object[] { base.EmployeeID, base.OrgCode, this.fileId, strArrays2[2], strArrays2[3], strArrays2[4] };
                            stringBuilder.AppendFormat("insert T_E_File_FileSafe (_AutoID,_UserName,_OrgCode,_CreateTime,_UpdateTime,_IsDel,FileID,ObjID,ObjType,Limit)\r\n                             values (newid(),'{0}','{1}',getdate(),getdate(),0,'{2}','{3}',{4},'{5}');", employeeID);
                        }
                        else if (strArrays2[0] == "update")
                        {
                            stringBuilder.AppendFormat("update T_E_File_FileSafe set Limit='{1}' where _AutoID='{0}';", strArrays2[1], strArrays2[4]);
                        }
                        else if (strArrays2[0] == "delete")
                        {
                            stringBuilder.AppendFormat("delete T_E_File_FileSafe where _AutoID='{0}';", strArrays2[1]);
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
            string str = string.Format("select _autoId, Limit,objId,objType from T_E_File_FileSafe where FileId='{0}' order by objType", this.fileId);
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
                DocFileEdit int0 = this;
                int0.int_0 = int0.int_0 + 1;
            }
            if (this.arrLimit.Length > 0)
            {
                this.arrLimit.Length = this.arrLimit.Length - 1;
            }
        }

        private string method_1(string string_0)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendFormat("<option value='0' {0}>无权限</option>", (string_0 == "0" ? "selected" : ""));
            stringBuilder.AppendFormat("<option value='1' {0}>可见</option>", (string_0 == "1" ? "selected" : ""));
            stringBuilder.AppendFormat("<option value='2' {0}>读取</option>", (string_0 == "2" ? "selected" : ""));
            stringBuilder.AppendFormat("<option value='3' {0}>下载</option>", (string_0 == "3" ? "selected" : ""));
            stringBuilder.AppendFormat("<option value='6' {0}>编辑</option>", (string_0 == "6" ? "selected" : ""));
            stringBuilder.AppendFormat("<option value='9' {0}>全部</option>", (string_0 == "9" ? "selected" : ""));
            return stringBuilder.ToString();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                this.fileId = base.GetParaValue("fileId");
                AppFile model = this._AppFile_0.GetModel(this.fileId);
                if (model == null)
                {
                    this.RadioButtonList1.SelectedValue = "1";
                }
                else
                {
                    this.TextBox2.Text = model.FactFileName;
                    this.TextBox3.Text = model.OrderId.ToString();
                    this.TextBox1.Text = string.Concat("", "/SysFolder/Common/FileDown.aspx?fileId=", model._AutoID);
                    this.RadioButtonList1.SelectedValue = (model.Inherit == "0" ? "0" : "1");
                }
                this.method_0();
            }
        }
    }
}