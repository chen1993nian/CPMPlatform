using EIS.DataAccess;
using EIS.Permission.Service;
using System;
using System.Collections.Specialized;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace EIS.Studio.SysFolder.Limit
{
    public partial class FunLimit : System.Web.UI.Page
    {
      

        protected void Button1_Click(object sender, EventArgs e)
        {
             try
             {
                        char[] chrArray;
                        string employeeAttrByCode;
                        string str = base.Request["funId"].ToString();
                        string funWbsById = FunNodeService.GetFunWbsById(str);
                        object[] objArray = new object[] { (this.CheckBoxList1.Items[0].Selected ? "1" : "0"), (this.CheckBoxList1.Items[1].Selected ? "1" : "0"), (this.CheckBoxList1.Items[2].Selected ? "1" : "0"), (this.CheckBoxList1.Items[3].Selected ? "1" : "0"), (this.CheckBoxList1.Items[4].Selected ? "1" : "0"), (this.CheckBoxList1.Items[5].Selected ? "1" : "0"), (this.CheckBoxList1.Items[6].Selected ? "1" : "0") };
                        string str1 = string.Format("{0}{1}{2}{3}{4}{5}{6}111", objArray);
                        StringCollection stringCollections = new StringCollection();
                        if (this.HiddenField1.Value.Length > 0)
                        {
                            string value = this.HiddenField1.Value;
                            chrArray = new char[] { ',' };
                            stringCollections.AddRange(value.Split(chrArray));
                        }
                        else if (this.TextBox1.Text.Length > 0)
                        {
                            string str2 = this.TextBox1.Text.Trim().Replace("\r\n", ",").Replace("\t", ",").Replace(" ", "").Replace("，", ",");
                            chrArray = new char[] { ',' };
                            string[] strArrays = str2.Split(chrArray);
                            for (int i = 0; i < (int)strArrays.Length; i++)
                            {
                                employeeAttrByCode = EmployeeService.GetEmployeeAttrByCode(strArrays[i], "_AutoID");
                                if (employeeAttrByCode != "")
                                {
                                    stringCollections.Add(employeeAttrByCode);
                                }
                            }
                        }
                        DataTable dataTable = SysDatabase.ExecuteTable(string.Concat("select _AutoID funId,FunWbs from T_E_Sys_FunNode where '", funWbsById, "' like FunWBS+'%' order by FunWBS desc"));
                        foreach (string employeeAttrByCodeA in stringCollections)
                        {
                            this.method_0(employeeAttrByCodeA, str, str1, funWbsById);
                            for (int j = 1; j < dataTable.Rows.Count; j++)
                            {
                                string str3 = dataTable.Rows[j]["funId"].ToString();
                                string str4 = dataTable.Rows[j]["FunWbs"].ToString();
                                this.method_0(employeeAttrByCodeA, str3, str1, str4);
                            }
                        }
                        this.Page.ClientScript.RegisterStartupScript(base.GetType(), "", string.Concat(";window.close();"), true);
                        //this.Page.ClientScript.RegisterStartupScript(base.GetType(), "", string.Concat("alert('设置成功！！');window.close();"), true);
               }catch(Exception ex)
               {
               
               }
        }

        private void method_0(string empId, string funId, string funLimit, string funWbs)
        {
            if (SysDatabase.ExecuteScalar(string.Format("select count(*) from T_E_Org_EmployeeLimit where FunID='{0}' and EmployeeID='{1}'", funId, empId)).ToString() == "0")
            {
                object[] objArray = new object[] { funId, empId, funLimit, funWbs };
                SysDatabase.ExecuteNonQuery(string.Format("insert T_E_Org_EmployeeLimit (_AutoID,_UserName,_OrgCode,_CreateTime,_UpdateTime,_IsDel ,FunID,EmployeeID,Limit,FunWBS) \r\n                        values (newid(),'" + Session["EmployeeId"].ToString() + "','',getdate(),getdate(),0 ,'{0}','{1}','{2}','{3}')", objArray));
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }
    }
}