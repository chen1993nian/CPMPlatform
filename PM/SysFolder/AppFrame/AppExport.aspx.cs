using Aspose.Cells;
using EIS.AppBase;
using EIS.DataAccess;
using EIS.DataModel.Access;
using EIS.DataModel.Model;
using NLog;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.Caching;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Xml;

namespace EIS.Web.SysFolder.AppFrame
{
    public partial class AppExport : PageBase
    {
      

        private string string_0 = "";

        private string string_1 = "";

        private string string_2 = "";

        private string string_3 = "";

        private _FieldInfo _FieldInfo_0 = new _FieldInfo();

        private _FieldInfoExt _FieldInfoExt_0 = new _FieldInfoExt();

  

        public void ExportExcelWithAspose(DataTable dataTable_0)
        {
            int j;
            int k;
            List<FieldInfo> fieldInfos;
            object item;
            if (dataTable_0 != null)
            {
                try
                {
                    Workbook workbook = new Workbook();
                    Worksheet worksheet = workbook.Worksheets[0];
                    worksheet.Name = dataTable_0.TableName;
                    int num = 0;
                    int num1 = 0;
                    int count = dataTable_0.Columns.Count;
                    int count1 = dataTable_0.Rows.Count;
                    if (this.string_1 == "")
                    {
                        if (this.string_0 == "")
                        {
                            fieldInfos = new List<FieldInfo>();
                            string str = base.Request["fieldList"];
                            char[] chrArray = new char[] { '|' };
                            string[] strArrays = str.Split(chrArray);
                            for (int i = 0; i < (int)strArrays.Length; i++)
                            {
                                string str1 = strArrays[i];
                                chrArray = new char[] { '=' };
                                string[] strArrays1 = str1.Split(chrArray);
                                FieldInfo fieldInfo = new FieldInfo()
                                {
                                    FieldName = strArrays1[0],
                                    FieldNameCn = strArrays1[2],
                                    ColumnWidth = strArrays1[1]
                                };
                                fieldInfos.Add(fieldInfo);
                            }
                        }
                        else
                        {
                            fieldInfos = this._FieldInfo_0.GetModelListDisp(this.string_0);
                        }
                        for (j = 0; j < fieldInfos.Count; j++)
                        {
                            worksheet.Cells[num, num1].PutValue(fieldInfos[j].FieldNameCn);
                            worksheet.Cells[num, num1].Style.Font.Name="宋体";
                            worksheet.Cells[num, num1].Style.Font.IsBold = true;
                            worksheet.Cells[num, num1].Style.Font.Color = Color.Blue;
                            worksheet.Cells.SetColumnWidthPixel(j, Convert.ToInt32(fieldInfos[j].ColumnWidth) + 10);
                           
                            //SetColumnWidthPixel(j, Convert.ToInt32(fieldInfos[j].ColumnWidth) + 10);
                            //worksheet.get_Cells.get_Item(num, num1).PutValue(fieldInfos[j].FieldNameCn);
                            //worksheet.get_Cells().get_Item(num, num1).get_Style().get_Font().set_IsBold(true);
                            //worksheet.get_Cells().get_Item(num, num1).get_Style().get_Font().set_Name("宋体");
                            //worksheet.get_Cells().get_Item(num, num1).get_Style().get_Font().set_Color(Color.Blue);
                            //worksheet.get_Cells().SetColumnWidthPixel(j, Convert.ToInt32(fieldInfos[j].ColumnWidth) + 10);
                            num1++;
                        }
                        worksheet.Cells.SetRowHeightPixel(0, 30);
                        count = fieldInfos.Count;
                        num++;
                        for (j = 0; j < count1; j++)
                        {
                            num1 = 0;
                            for (k = 0; k < count; k++)
                            {
                                item = dataTable_0.Rows[j][fieldInfos[k].FieldName];
                                if (item.GetType() != typeof(DateTime))
                                {
                                    worksheet.Cells[num, num1].PutValue(item);
                                }
                                else
                                {
                                    worksheet.Cells[num, num1].PutValue(item.ToString());
                                }
                                worksheet.Cells.SetRowHeightPixel(j, 20);
                                num1++;
                            }
                            if (num % 2 == 0)
                            {
                                worksheet.Cells.Rows[num].Style.BackgroundColor = Color.LightBlue;
                            }
                            num++;
                        }
                    }
                    else
                    {
                        List<FieldInfoExt> modelListDisp = this._FieldInfoExt_0.GetModelListDisp(this.string_0, this.string_1);
                        for (j = 0; j < modelListDisp.Count; j++)
                        {
                            //worksheet.get_Cells().get_Item(num, num1).PutValue(modelListDisp[j].FieldNameCn);
                            //worksheet.get_Cells().get_Item(num, num1).get_Style().get_Font().set_IsBold(true);
                            //worksheet.get_Cells().get_Item(num, num1).get_Style().get_Font().set_Name("宋体");
                            //worksheet.get_Cells().SetColumnWidthPixel(j, Convert.ToInt32(modelListDisp[j].ColumnWidth) + 10);

                            worksheet.Cells[num, num1].PutValue(modelListDisp[j].FieldNameCn);
                            worksheet.Cells[num, num1].Style.Font.Name = "宋体";
                            worksheet.Cells[num, num1].Style.Font.IsBold = true;
                            worksheet.Cells[num, num1].Style.Font.Color = Color.Blue;
                            worksheet.Cells.SetColumnWidthPixel(j, Convert.ToInt32(modelListDisp[j].ColumnWidth) + 10);

                            num1++;
                        }
                        num++;
                        count = modelListDisp.Count;
                        for (j = 0; j < count1; j++)
                        {
                            num1 = 0;
                            for (k = 0; k < count; k++)
                            {
                                item = dataTable_0.Rows[j][modelListDisp[k].FieldName];
                                if (item.GetType() != typeof(DateTime))
                                {
                                    worksheet.Cells[num, num1].PutValue(item);
                                }
                                else
                                {
                                    worksheet.Cells[num, num1].PutValue(item.ToString());
                                }
                                worksheet.Cells.SetRowHeightPixel(j, 20);
                                num1++;
                            }
                            if (num % 2 == 0)
                            {
                                worksheet.Cells.Rows[num].Style.BackgroundColor=Color.LightBlue;
                            }
                            num++;
                        }
                    }
                    //Style style = workbook.get_Styles().get_Item(workbook.get_Styles().Add());

                    Style style = workbook.Styles[workbook.Styles.Add()];
                    style.Font.Name = "Arial";
                    style.Font.Size = 10;
                    //workbook.Save(string.Format("report.xls", new object[0]), SaveType.OpenInExcel, FileFormatType.Default, base.Response);
                    string FileName = DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
                    HttpResponse response = Page.Response;
                    response.Buffer = true;
                    response.Charset = "utf-8";
                    response.AppendHeader("Content-Disposition", "attachment;filename=" + FileName);
                    response.ContentEncoding = System.Text.Encoding.UTF8;
                    response.ContentType = "application/ms-excel";
                    response.BinaryWrite(workbook.SaveToStream().ToArray());
                    response.End();
                }
                catch (Exception exception)
                {
                    throw exception;
                }
            }
        }

        private DataTable method_0(string script, string connectionId)
        {
            DataTable dataTable;
            if (string.IsNullOrWhiteSpace(connectionId))
            {
                dataTable = SysDatabase.ExecuteTable(script);
            }
            else
            {
                CustomDb customDb = new CustomDb();
                customDb.CreateDatabaseByConnectionId(connectionId.Trim());
                dataTable = customDb.ExecuteTable(script);
            }
            return dataTable;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string item = base.Request["sortname"];
            string str = base.Request["sortorder"];
            string str1 = (string.IsNullOrEmpty(item) ? "" : string.Concat(" order by ", item, " ", base.Request["sortorder"]));
            if ((str1 == "" ? false : base.Request["sortname"].ToLower() == "rowindex"))
            {
                str1 = "";
            }
            string str2 = "";
            str2 = (!string.IsNullOrEmpty(base.GetParaValue("cryptcond")) ? Security.Decrypt(base.GetParaValue("cryptcond"), base.UserName) : base.GetParaValue("condition"));
            if (!string.IsNullOrEmpty(base.Request["query"]))
            {
                str2 = (!string.IsNullOrEmpty(str2) ? string.Concat(str2, " and ", base.Request["query"]) : base.Request["query"]);
            }
            str2 = (!string.IsNullOrEmpty(str2) ? str2.Replace("[QUOTES]", "'") : " 1=1 ");
            this.string_0 = base.GetParaValue("tblName");
            this.string_2 = base.GetParaValue("DefaultValue");
            string innerText = "";
            string connectionId = "";
            if (this.string_0 == "")
            {
                string paraValue = base.GetParaValue("queryId");
                XmlDocument xmlDocument = new XmlDocument();
                string str3 = "Data";
                if (!string.IsNullOrEmpty(base.Request["ds"]))
                {
                    str3 = base.Request["ds"].ToString();
                }
                string str4 = HttpContext.Current.Server.MapPath(string.Concat("~/App_Data/", str3, ".xml"));
                string str5 = string.Concat("App_Data_", str3);
                if (HttpContext.Current.Cache[str5] == null)
                {
                    xmlDocument.Load(str4);
                    CacheDependency cacheDependency = new CacheDependency(str4);
                    HttpRuntime.Cache.Insert(str5, xmlDocument, cacheDependency);
                }
                else
                {
                    xmlDocument = HttpContext.Current.Cache[str5] as XmlDocument;
                }
                XmlElement documentElement = xmlDocument.DocumentElement;
                XmlNode xmlNodes = documentElement.SelectSingleNode(string.Concat("/queryobjs/queryobj[@queryid='", paraValue, "']"));
                if (xmlNodes != null)
                {
                    xmlNodes.SelectSingleNode("querylist");
                    XmlNode xmlNodes1 = xmlNodes.SelectSingleNode("querysql");
                    innerText = xmlNodes1.InnerText;
                    if ((xmlNodes1.Attributes["sortdir"] == null ? false : str1 == ""))
                    {
                        str1 = string.Concat(" order by ", xmlNodes1.Attributes["sortdir"].Value);
                    }
                    if (xmlNodes1.Attributes["distinct"] != null)
                    {
                        this.string_3 = xmlNodes1.Attributes["distinct"].Value;
                    }
                }
            }
            else
            {
                this.string_1 = base.GetParaValue("Sindex");
                TableInfo model = (new _TableInfo(this.string_0)).GetModel();
                innerText = model.ListSQL;
                connectionId = model.ConnectionId;
            }
            if (str2.Trim() == "")
            {
                str2 = " 1=1 ";
            }
            innerText = innerText.Replace("|^condition^|", str2.Replace("[QUOTES]", "'")).Replace("|^sortdir^|", str1).Replace("\r\n", " ").Replace("\t", "");
            if (!string.IsNullOrEmpty(this.string_2))
            {
                innerText = Utility.ReplaceParaValues(innerText, this.string_2);
            }
            innerText = Utility.DealCommandBySeesion(innerText);
            this.fileLogger.Debug("AppExport.ListSQL={0}", innerText);
            DataTable dataTable = this.method_0(innerText, connectionId);
            if (this.string_3.Length > 0)
            {
                DataTable dataTable1 = dataTable.Clone();
                StringCollection stringCollections = new StringCollection();
                int num = 0;
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    DataRow dataRow = dataTable.Rows[i];
                    if (!stringCollections.Contains(dataRow[this.string_3].ToString()))
                    {
                        dataTable1.LoadDataRow(dataRow.ItemArray, true);
                        stringCollections.Add(dataRow[this.string_3].ToString());
                        num++;
                    }
                }
                dataTable = dataTable1;
            }
            this.ExportExcelWithAspose(dataTable);
        }
    }
}