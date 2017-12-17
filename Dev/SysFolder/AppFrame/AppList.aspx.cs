using AjaxPro;
using EIS.AppBase;
using EIS.AppModel;
using EIS.DataAccess;
using EIS.DataModel.Access;
using EIS.DataModel.Model;
using EIS.DataModel.Service;
using EIS.Permission;
using EIS.WebBase.ModelLib.Service;
using EIS.WorkFlow.Engine;
using EIS.WorkFlow.Service;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Runtime.CompilerServices;
using System.Text;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace EIS.WebBase.SysFolder.AppFrame
{
    public partial class AppList : PageBase
    {

            public string colmodel = "";

            public string querymodel = "";

            public string sindex = "";

            public string sortname = "";

            public string sortorder = "";

            public string TblNameCn = "";

            public string ww = "900";

            public string wh = "700";

            public string limit = "";

            public string addLimit = "0";

            public string editLimit = "0";

            public string delLimit = "0";

            public string condLimit = "0";

            public string layoutLimit = "0";

            public string exportLimit = "0";

            public string gdLimit = "0";

            public string tblname = "";

            public string listfn = "";

            public string preProcess = "";

            public string para = "";

            public string condition = "";

            public string funId = "";

            public string InitCond = "";

            public string workflowCode = "";

            public string addBtnText = "";

            public string addAction = "1";

            public string cryptPara = "";

            public string customScript = "";

            public string bakUrl = "";


            [AjaxMethod(HttpSessionStateRequirement.Read)]     // JustDecompile was unable to locate the assembly where attribute parameters types are defined. Generating parameters values is impossible.
            public void DelRecord(string tblname, string autoid)
            {
                StringBuilder stringBuilder = new StringBuilder();
                if (InstanceService.IsRunAlready(tblname, autoid))
                {
                    if (SysDatabase.ExecuteScalar(string.Format("select count(*) from T_E_WF_Instance i inner join T_E_WF_Task t on i._AutoID=t.InstanceId\r\n                    where i.AppName='{0}' and i.AppId='{1}'", tblname, autoid)).ToString() != "1")
                    {
                        throw new Exception("本条记录已经存在审批数据，不能删除");
                    }
                    string str = string.Format("select top 1 _AutoId from T_E_WF_Instance where AppName='{0}' and AppId='{1}'", tblname, autoid);
                    string str1 = SysDatabase.ExecuteScalar(str).ToString();
                    EIS.WorkFlow.Engine.Utility.RemoveInstance(str1, base.UserInfo);
                }
                if (DaArchiveService.IsArchived(tblname, autoid))
                {
                    throw new Exception("本条记录已经归档，不能删除");
                }
                ModelSave modelSave = new ModelSave(this);
                stringBuilder.AppendFormat("<?xml version=\"1.0\" encoding=\"utf-8\"?><root><Table TableName=\"{0}\">", tblname);
                stringBuilder.AppendFormat("<row state=\"Deleted\" id=\"\"><_AutoID><![CDATA[{0}]]></_AutoID></row></Table></root>", autoid);
                modelSave.SaveData(tblname, stringBuilder.ToString());
            }

            [AjaxMethod(HttpSessionStateRequirement.Read)]     // JustDecompile was unable to locate the assembly where attribute parameters types are defined. Generating parameters values is impossible.
            public string ExecSQL(string scriptCode, string para)
            {
                string str;
                string tableSQLScript = TableService.GetTableSQLScript(scriptCode);
                if (tableSQLScript != "")
                {
                    tableSQLScript = base.ReplaceContext(tableSQLScript);
                    tableSQLScript = EIS.AppBase.Utility.ReplaceParaValues(tableSQLScript, para);
                    object obj = SysDatabase.ExecuteScalar(tableSQLScript);
                    str = (obj == null ? "" : obj.ToString());
                }
                else
                {
                    str = "";
                }
                return str;
            }

            [AjaxMethod(HttpSessionStateRequirement.Read)]     // JustDecompile was unable to locate the assembly where attribute parameters types are defined. Generating parameters values is impossible.
            public DataTable GetDataTable(string scriptCode, string para)
            {
                DataTable dataTable;
                string tableSQLScript = TableService.GetTableSQLScript(scriptCode);
                if (tableSQLScript != "")
                {
                    if (TableService.HasTableSQLScriptCommandPara(para))
                    {
                        DbCommand dbcmmd = SysDatabase.GetSqlStringCommand(tableSQLScript);
                        string[] strArrays = para.Split(new char[] { '|' });
                        for (int i = 0; i < (int)strArrays.Length; i++)
                        {
                            string str = strArrays[i];
                            string[] strArrays1 = str.Split("=".ToCharArray(), 2);
                            if (strArrays1[0].Contains("@int_"))
                            {
                                SysDatabase.AddInParameter(dbcmmd, strArrays1[0], DbType.Int64, Convert.ToInt64(strArrays1[1]));
                            }
                            else if (strArrays1[0].Contains("@str_"))
                            {
                                SysDatabase.AddInParameter(dbcmmd, strArrays1[0], DbType.String, strArrays1[1]);
                            }
                            else if (strArrays1[0].Contains("@date_"))
                            {
                                SysDatabase.AddInParameter(dbcmmd, strArrays1[0], DbType.DateTime, Convert.ToDateTime(strArrays1[1]));
                            }
                            else if (strArrays1[0].Contains("@num_"))
                            {
                                SysDatabase.AddInParameter(dbcmmd, strArrays1[0], DbType.DateTime, Convert.ToDecimal(strArrays1[1]));
                            }
                        }
                        dataTable = SysDatabase.ExecuteTable(dbcmmd);
                    }
                    else
                    {
                        tableSQLScript = base.ReplaceContext(tableSQLScript);
                        tableSQLScript = EIS.AppBase.Utility.ReplaceParaValues(tableSQLScript, para);
                        dataTable = SysDatabase.ExecuteTable(tableSQLScript);
                    }
                }
                else
                {
                    dataTable = null;
                }
                return dataTable;
            }

            private string method_0(string string_2, int int_0, string string_3)
            {
                string str;
                if (string_3 != "0")
                {
                    str = "1";
                }
                else
                {
                    if (string_2.Length >= int_0)
                    {
                        string str1 = string_2.Substring(int_0 - 1, 1);
                        if (str1 != "1")
                        {
                            if (str1 != "0")
                            {
                                goto Label1;
                            }
                            str = "1";
                            return str;
                        }
                        else
                        {
                            str = "0";
                            return str;
                        }
                    }
                Label1:
                    str = "0";
                }
                return str;
            }

            private string method_1(string string_2)
            {
                string str;
                if (string_2 == "1")
                {
                    str = "left";
                }
                else if (string_2 != "2")
                {
                    str = (string_2 != "3" ? "left" : "right");
                }
                else
                {
                    str = "center";
                }
                return str;
            }

            protected void Page_Load(object sender, EventArgs e)
            {
                object[] fieldNameCn;
                ModelBuilder modelBuilder;
                FieldInfo fieldInfo = null;
                AjaxPro.Utility.RegisterTypeForAjax(typeof(AppList));
                StringBuilder stringBuilder = new StringBuilder();
                this.InitCond = base.GetParaValue("InitCond");
                this.tblname = base.GetParaValue("tblname");
                this.condition = base.GetParaValue("condition");
                this.sindex = base.GetParaValue("sindex");
                this.workflowCode = base.GetParaValue("workflowCode");
                this.addBtnText = base.GetParaValue("addBtnText");
                if (this.addBtnText == "")
                {
                    this.addBtnText = "添加";
                }
                this.addAction = base.GetParaValue("addAction");
                string paraValue = base.GetParaValue("ext");
                char[] chrArray = new char[] { '|' };
                string[] strArrays = paraValue.Split(chrArray);
                if (paraValue != "")
                {
                    if (strArrays[0] != "")
                    {
                        this.ww = strArrays[0];
                    }
                    if (strArrays[1] != "")
                    {
                        this.wh = strArrays[1];
                    }
                }
                this.customScript = base.GetCustomScript("ref_AppDefault");
                this.funId = base.GetParaValue("funId");
                if (this.funId.Length > 0)
                {
                    this.bakUrl = EIS.Permission.Utility.GetFunAttrById(this.funId, "FunUrl");
                    this.limit = EIS.Permission.Utility.GetFunLimitByEmployeeId(base.EmployeeID, this.funId);
                    this.addLimit = (this.limit.Substring(1, 1) == "1" ? "0" : "1");
                    this.editLimit = (this.limit.Substring(2, 1) == "1" ? "0" : "1");
                    this.delLimit = (this.limit.Substring(3, 1) == "1" ? "0" : "1");
                    this.condLimit = (this.limit.Substring(4, 1) == "1" ? "0" : "1");
                    this.layoutLimit = (this.limit.Substring(5, 1) == "1" ? "0" : "1");
                    if (base.GetParaValue("hideExport") != "")
                    {
                        this.exportLimit = base.GetParaValue("hideExport");
                    }
                    else
                    {
                        this.exportLimit = (this.limit.Substring(6, 1) == "1" ? "0" : "1");
                    }
                    string str = base.GetParaValue("btnLimit");
                    if (str.Length > 0)
                    {
                        this.addLimit = this.method_0(str, 1, this.addLimit);
                        this.editLimit = this.method_0(str, 2, this.editLimit);
                        this.delLimit = this.method_0(str, 3, this.delLimit);
                        this.condLimit = this.method_0(str, 4, this.condLimit);
                        this.layoutLimit = this.method_0(str, 5, this.layoutLimit);
                        this.exportLimit = this.method_0(str, 6, this.exportLimit);
                    }
                }
                if ((int)strArrays.Length > 2)
                {
                    this.limit = strArrays[2];
                    if (this.limit.Length >= 3)
                    {
                        this.addLimit = (this.limit.Substring(0, 1) == "1" ? "0" : "1");
                        this.editLimit = (this.limit.Substring(1, 1) == "1" ? "0" : "1");
                        this.delLimit = (this.limit.Substring(2, 1) == "1" ? "0" : "1");
                    }
                }
                if (base.GetParaValue("para") == "")
                {
                    this.para = base.Server.UrlDecode(base.Request.Url.Query.Substring(1));
                    this.para = EIS.AppBase.Utility.TrimPara(this.para, "condition");
                    if (this.para.EndsWith("&"))
                    {
                        this.para = this.para.Substring(0, this.para.Length - 1);
                    }
                }
                else
                {
                    string str1 = base.DeCryptPara(base.GetParaValue("para"));
                    this.para = EIS.AppBase.Utility.TrimPara(str1, "condition");
                    if (this.para.EndsWith("&"))
                    {
                        this.para = this.para.Substring(0, this.para.Length - 1);
                    }
                }
                this.cryptPara = base.CryptPara(this.para);
                _FieldInfo __FieldInfo = new _FieldInfo();
                _FieldInfoExt __FieldInfoExt = new _FieldInfoExt();
                _TableInfo __TableInfo = new _TableInfo(this.tblname);
                bool flag = __TableInfo.FieldExists("_wfstate");
                bool flag1 = __TableInfo.FieldExists("_gdstate");
                TableInfo model = __TableInfo.GetModel();
                if (model == null)
                {
                    this.Session["_sysinfo"] = string.Format("参数 TblName = {0} 输入有错误，系统找不到对应的业务定义。", this.tblname);
                    base.Response.Redirect("AppInfo.aspx?msgType=error", true);
                }
                this.tblname = model.TableName;
                this.TblNameCn = model.TableNameCn;
                if (model.OrderField != "")
                {
                    string orderField = model.OrderField;
                    chrArray = new char[] { ' ' };
                    string[] strArrays1 = orderField.Split(chrArray);
                    if ((int)strArrays1.Length == 2)
                    {
                        this.sortname = strArrays1[0];
                        this.sortorder = strArrays1[1];
                    }
                }
                if (this.sindex == "")
                {
                    List<FieldInfo> modelListDisp = __FieldInfo.GetModelListDisp(this.tblname);
                    modelListDisp.RemoveAll((FieldInfo fieldInfo_0) => fieldInfo_0.FieldName.ToLower() == "_wfstate");
                    if ((!flag1 ? true : model.ArchiveState != 1))
                    {
                        this.gdLimit = "1";
                    }
                    else
                    {
                        FieldInfo fieldInfo1 = new FieldInfo()
                        {
                            FieldNameCn = "归档状态",
                            FieldName = "_gdstate",
                            ColumnWidth = "70",
                            ColumnAlign = "center",
                            ColumnHidden = 0,
                            ColumnRender = "gdStateRender",
                            _AutoID = ""
                        };
                        modelListDisp.Add(fieldInfo1);
                    }
                    if (flag)
                    {
                        if ((model.ShowState != 1 || !(base.GetParaValue("wfstate") != "0") ? base.GetParaValue("wfstate") == "1" : true))
                        {
                            FieldInfo fieldInfo2 = new FieldInfo()
                            {
                                FieldNameCn = "流程状态",
                                FieldName = "_wfstate",
                                ColumnWidth = "90",
                                ColumnAlign = "center",
                                ColumnHidden = 0,
                                ColumnRender = "wfStateRender",
                                _AutoID = ""
                            };
                            modelListDisp.Add(fieldInfo2);
                        }
                    }
                    foreach (FieldInfo fieldInfoA in modelListDisp)
                    {
                        StringBuilder stringBuilder1 = stringBuilder;
                        fieldNameCn = new object[] { fieldInfoA.FieldNameCn, fieldInfoA.FieldName, fieldInfoA.ColumnWidth, this.method_1(fieldInfoA.ColumnAlign), null, null, null, null };
                        fieldNameCn[4] = (fieldInfoA.ColumnHidden == 1 ? "true" : "false");
                        fieldNameCn[5] = (fieldInfoA.ColumnRender.Trim() == "" ? "false" : fieldInfo.ColumnRender.Trim());
                        fieldNameCn[6] = fieldInfoA._AutoID;
                        fieldNameCn[7] = (fieldInfoA.FieldType == 5 ? "false" : "true");
                        stringBuilder1.AppendFormat("{{display: '{0}', name : '{1}', width : {2}, sortable :{7}, align: '{3}',hide:{4},renderer:{5},fieldid:'{6}'}},", fieldNameCn);
                    }
                    if (stringBuilder.Length > 0)
                    {
                        stringBuilder.Length = stringBuilder.Length - 1;
                    }
                    this.colmodel = stringBuilder.ToString();
                    stringBuilder.Length = 0;
                    modelBuilder = new ModelBuilder(this);
                    foreach (FieldInfo modelQueryDisp in __FieldInfo.GetModelQueryDisp(this.tblname))
                    {
                        stringBuilder.Append(modelBuilder.GetQueryModel(modelQueryDisp));
                    }
                    if (stringBuilder.Length > 0)
                    {
                        stringBuilder.Length = stringBuilder.Length - 1;
                    }
                    this.querymodel = stringBuilder.ToString();
                }
                else
                {
                    List<FieldInfoExt> fieldInfoExts = __FieldInfoExt.GetModelListDisp(this.tblname, this.sindex);
                    fieldInfoExts.RemoveAll((FieldInfoExt fieldInfoExt_0) => fieldInfoExt_0.FieldName.ToLower() == "_wfstate");
                    if ((!flag1 ? true : model.ArchiveState != 1))
                    {
                        this.gdLimit = "1";
                    }
                    else
                    {
                        FieldInfoExt fieldInfoExt = new FieldInfoExt()
                        {
                            FieldNameCn = "归档状态",
                            FieldName = "_gdstate",
                            ColumnWidth = "70",
                            ColumnAlign = "center",
                            ColumnHidden = 0,
                            ColumnRender = "gdStateRender",
                            _AutoID = ""
                        };
                        fieldInfoExts.Add(fieldInfoExt);
                    }
                    if (flag)
                    {
                        if ((model.ShowState != 1 || !(base.GetParaValue("wfstate") != "0") ? base.GetParaValue("wfstate") == "1" : true))
                        {
                            FieldInfoExt fieldInfoExt1 = new FieldInfoExt()
                            {
                                FieldNameCn = "流程状态",
                                FieldName = "_wfstate",
                                ColumnWidth = "90",
                                ColumnAlign = "center",
                                ColumnHidden = 0,
                                ColumnRender = "wfStateRender",
                                _AutoID = ""
                            };
                            fieldInfoExts.Add(fieldInfoExt1);
                        }
                    }
                    foreach (FieldInfoExt fieldInfoExt2 in fieldInfoExts)
                    {
                        StringBuilder stringBuilder2 = stringBuilder;
                        fieldNameCn = new object[] { fieldInfoExt2.FieldNameCn, fieldInfoExt2.FieldName, fieldInfoExt2.ColumnWidth, this.method_1(fieldInfoExt2.ColumnAlign), null, null, null, null };
                        fieldNameCn[4] = (fieldInfoExt2.ColumnHidden == 1 ? "true" : "false");
                        fieldNameCn[5] = (fieldInfoExt2.ColumnRender.Trim() == "" ? "false" : fieldInfoExt2.ColumnRender.Trim());
                        fieldNameCn[6] = fieldInfoExt2._AutoID;
                        fieldNameCn[7] = (fieldInfoExt2.FieldType == 5 ? "false" : "true");
                        stringBuilder2.AppendFormat("{{display: '{0}', name : '{1}', width : {2}, sortable : {7}, align: '{3}',hide:{4},renderer:{5},fieldid:'{6}'}},", fieldNameCn);
                    }
                    if (stringBuilder.Length > 0)
                    {
                        stringBuilder.Length = stringBuilder.Length - 1;
                    }
                    this.colmodel = stringBuilder.ToString();
                    stringBuilder.Length = 0;
                    _FieldStyle __FieldStyle = new _FieldStyle();
                    modelBuilder = new ModelBuilder(this);
                    List<FieldInfoExt> modelQueryDisp1 = __FieldInfoExt.GetModelQueryDisp(this.tblname, this.sindex);
                    List<FieldStyle> fieldsStyle = __FieldStyle.GetFieldsStyle(this.tblname, int.Parse(this.sindex));
                    foreach (FieldInfoExt fieldInfoExt3 in modelQueryDisp1)
                    {
                        FieldInfo fieldInDispStyle = new FieldInfo()
                        {
                            FieldName = fieldInfoExt3.FieldName,
                            FieldNameCn = fieldInfoExt3.FieldNameCn,
                            FieldType = fieldInfoExt3.FieldType,
                            QueryMatchMode = fieldInfoExt3.QueryMatchMode,
                            QueryDefaultType = fieldInfoExt3.QueryDefaultType,
                            QueryDefaultValue = fieldInfoExt3.QueryDefaultValue,
                            QueryStyle = fieldInfoExt3.QueryStyle,
                            QueryStyleName = fieldInfoExt3.QueryStyleName,
                            QueryStyleTxt = fieldInfoExt3.QueryStyleTxt
                        };
                        FieldStyle fieldStyle = fieldsStyle.Find((FieldStyle fieldStyle_0) => fieldStyle_0.FieldName == fieldInfoExt3.FieldName);
                        if (fieldStyle == null)
                        {
                            fieldInDispStyle.FieldInDispStyle = "";
                            fieldInDispStyle.FieldInDispStyleName = "";
                            fieldInDispStyle.FieldInDispStyleTxt = "";
                        }
                        else
                        {
                            fieldInDispStyle.FieldInDispStyle = fieldStyle.FieldInDispStyle;
                            fieldInDispStyle.FieldInDispStyleName = fieldStyle.FieldInDispStyleName;
                            fieldInDispStyle.FieldInDispStyleTxt = fieldStyle.FieldInDispStyleTxt;
                        }
                        stringBuilder.Append(modelBuilder.GetQueryModel(fieldInDispStyle));
                    }
                    if (stringBuilder.Length > 0)
                    {
                        stringBuilder.Length = stringBuilder.Length - 1;
                    }
                    this.querymodel = stringBuilder.ToString();
                }
                if (model != null)
                {
                    this.listfn = model.ListScriptBlock;
                    if (this.listfn.Trim().Length > 0)
                    {
                        string paraValue1 = base.GetParaValue("ReplaceValue");
                        if (paraValue1 != "")
                        {
                            modelBuilder = new ModelBuilder(this);
                            this.listfn = modelBuilder.ReplaceParaValue(paraValue1, this.listfn);
                            this.listfn = modelBuilder.GetUbbCode(this.listfn, "[CRYPT]", "[/CRYPT]", base.UserName);
                        }
                    }
                    this.preProcess = (model.ListPreProcessFn.Trim() == "" ? "false" : model.ListPreProcessFn);
                }
            }

            [AjaxMethod(HttpSessionStateRequirement.Read)]     // JustDecompile was unable to locate the assembly where attribute parameters types are defined. Generating parameters values is impossible.
            public void saveLayout(ArrayList arrayList_0, string tblname, string sindex, string sortdir)
            {
                int i;
                string[] strArrays;
                object[] objArray;
                StringBuilder stringBuilder = new StringBuilder();
                if (sindex != "")
                {
                    for (i = 0; i < arrayList_0.Count; i++)
                    {
                        strArrays = arrayList_0[i].ToString().Split("=".ToCharArray());
                        if (strArrays[0] != "rowindex")
                        {
                            StringBuilder stringBuilder1 = stringBuilder;
                            objArray = new object[] { i, strArrays[1], null, null, null };
                            objArray[2] = (strArrays[2] == "none" ? 1 : 0);
                            objArray[3] = tblname;
                            objArray[4] = strArrays[0];
                            stringBuilder1.AppendFormat("update T_E_Sys_FieldInfoExt set ColumnOrder={0} ,ColumnWidth={1}, ColumnHidden={2} where tablename='{3}' and _AutoId='{4}';", objArray);
                        }
                    }
                }
                else
                {
                    for (i = 0; i < arrayList_0.Count; i++)
                    {
                        strArrays = arrayList_0[i].ToString().Split("=".ToCharArray());
                        if (strArrays[0] != "rowindex")
                        {
                            StringBuilder stringBuilder2 = stringBuilder;
                            objArray = new object[] { i, strArrays[1], null, null, null };
                            objArray[2] = (strArrays[2] == "none" ? 1 : 0);
                            objArray[3] = tblname;
                            objArray[4] = strArrays[0];
                            stringBuilder2.AppendFormat("update T_E_Sys_FieldInfo set ColumnOrder={0} ,ColumnWidth={1}, ColumnHidden={2} where tablename='{3}' and _AutoId='{4}';", objArray);
                        }
                    }
                }
                if (sortdir != "")
                {
                    stringBuilder.AppendFormat("update T_E_Sys_TableInfo set OrderField='{1}' where tableName='{0}'", tblname, sortdir);
                }
                if (stringBuilder.Length > 0)
                {
                    SysDatabase.ExecuteNonQuery(stringBuilder.ToString());
                }
            }
        
    }
}