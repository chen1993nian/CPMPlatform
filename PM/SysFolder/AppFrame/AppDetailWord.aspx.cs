using EIS.AppBase;
using EIS.DataAccess;
using System;
using System.IO;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using HtmlAgilityPack;
using System.Xml;

namespace EIS.Web.SysFolder.AppFrame
{
  
    public partial class AppDetailWord : PageBase
    {
        public string tblName = "";
        public string mainId = "";
        public string sIndex = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            this.tblName = base.GetParaValue("tblName");
            this.sIndex = base.GetParaValue("sIndex");
            string condition = base.GetParaValue("condition").Replace("[QUOTES]", "'");
            if (string.IsNullOrEmpty(condition) && base.GetParaValue("mainId") != "")
            {
                this.mainId = base.GetParaValue("mainId");
                condition = string.Concat("_AutoId='", this.mainId, "'");
            }

            AppImportWord appWord = new AppImportWord();
            string FileName = DateTime.Now.ToString("yyyyMMddHHmmss") + ".doc";
            string wordTempXML = appWord.GetWordTempletFile(tblName, sIndex, out FileName);
            if (wordTempXML != "")
            {
                DataTable mainData = appWord.GetMainData(tblName, condition);
                wordTempXML = appWord.GetMainDetailHtml(tblName, wordTempXML, mainData.Rows[0]);
                List<Dictionary<string, string>> subListDic = appWord.GetSubTableInfo(tblName);
                foreach (Dictionary<string, string> subTbl in subListDic)
                {
                    string subtblname = subTbl["SubTblName"].ToString();
                    string appid = mainData.Rows[0]["_AutoID"].ToString();
                    DataTable subtblData = appWord.GetSubTableData(subtblname, appid);
                    if ((subtblData != null) && (subtblData.Rows.Count > 0))
                    {
                        wordTempXML = appWord.GetSubDetailHtml(subtblname, wordTempXML, subtblData);
                    }
                }
                Response.Buffer = true;
                Response.Charset = "utf-8";
                Response.AppendHeader("Content-Disposition", "attachment;filename=" + FileName);
                Response.ContentEncoding = System.Text.Encoding.UTF8;
                Response.ContentType = "application/ms-excel";
                Response.Write(wordTempXML);
                Response.End();
            }
        }


    }

    public class AppImportWord
    {

        public string GetWordTempletFile(string tblName, string sIndex, out string FileNameCn)
        {
            string wordTempXML = "";
            FileNameCn = DateTime.Now.ToString("yyyyMMddHHmmss") + ".doc";
            try
            {
                string sql = @"select top 1 * from dbo.T_E_File_File
                where AppName='T_E_APP_TableWordData' 
                and AppId in (select top 1 tempfile from T_E_APP_TableWordData 
                    where tablename=@tablename 
                    and getdate() BETWEEN StartDate and EndDate 
                    order by StartDate)
                and _IsDel=0 
                order by _CreateTime desc ";
                DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(sql);
                SysDatabase.AddInParameter(sqlStringCommand, "@tablename", DbType.String, tblName);
                DataTable dataTable = SysDatabase.ExecuteTable(sqlStringCommand);
                if ((dataTable != null) && (dataTable.Rows.Count > 0))
                {
                    string fileFoldPath = AppFilePath.GetBasePath("01");
                    string filepath = fileFoldPath + dataTable.Rows[0]["FilePath"].ToString();
                    if (File.Exists(filepath)) wordTempXML = File.ReadAllText(filepath);
                    FileNameCn = dataTable.Rows[0]["FactFileName"].ToString();
                    FileNameCn = Path.GetFileNameWithoutExtension(FileNameCn);
                    FileNameCn = FileNameCn + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".doc";

                    if (!string.IsNullOrEmpty(sIndex)) {
                        foreach (DataRow dr in dataTable.Rows)
                        {
                            if (sIndex == dr["condition"].ToString())
                            {
                                filepath = fileFoldPath + dr["FilePath"].ToString();
                                if (File.Exists(filepath)) wordTempXML = File.ReadAllText(filepath);
                                FileNameCn = dr["FactFileName"].ToString();
                                FileNameCn = Path.GetFileNameWithoutExtension(FileNameCn);
                                FileNameCn = FileNameCn + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".doc";
                                break;
                            }
                        }
                    }
                }
            }
            catch { }
            finally { }
            return (wordTempXML);
        }

        /// <summary>
        /// 获取主表单XML格式
        /// </summary>
        /// <param name="tblName">表单名称</param>
        /// <param name="tmpl">模板HTML格式</param>
        /// <param name="data">表单数据行</param>
        /// <returns></returns>
        public string GetMainDetailHtml(string tblName, string tmpl, DataRow data)
        {
            string str;
            string[] strArrays = new string[] { "\\{", tblName, ".([a-zA-Z0-9_]+)\\}|\\{([a-zA-Z0-9_]+)\\}|\\[([a-zA-Z0-9_]+)\\]|\\[", tblName, ".([a-zA-Z0-9_]+)\\]" };
            Regex regex = new Regex(string.Concat(strArrays), RegexOptions.IgnoreCase);
            foreach (Match match in regex.Matches(tmpl))
            {
                string str2 = match.Value.Trim("{}[]".ToCharArray());
                if (str2.IndexOf(".") > 0)
                {
                    char[] chrArray = new char[] { '.' };
                    str = str2.Split(chrArray)[1];
                }
                else
                {
                    str = str2;
                }
                string str3 = str;
                if (data.Table.Columns.Contains(str3))
                {
                    tmpl = tmpl.Replace(match.Value, data[str3].ToString());
                }
            }
            return tmpl;
        }

        /// <summary>
        /// 获取表单打印HTML格式
        /// </summary>
        /// <param name="subTableName">子表单名称</param>
        /// <param name="tmpl">XML格式Word模板</param>
        /// <param name="dataSubTable">子表单数据</param>
        /// <returns></returns>
        public string GetSubDetailHtml(string subTableName, string tmpl, DataTable dataSubTable)
        {
            string detailHtml = tmpl;
            XmlDocument htmlDocument = new XmlDocument();
            htmlDocument.LoadXml(tmpl);
            detailHtml = htmlDocument.OuterXml;
            XmlNodeList tableNodeList = htmlDocument.GetElementsByTagName("w:tbl");
            if (tableNodeList != null)
            {
                foreach (XmlNode tblNode in tableNodeList)
                {
                    string subBodyXML = "";
                    Int32 trKeyPosition = 0;
                    foreach (XmlNode trNode in tblNode.ChildNodes)
                    {
                        if (trNode.Name == "w:tr")
                        {
                            string trXml = trNode.OuterXml;
                            string trKeyStr = GetTrXml(trXml);
                            if (trXml.IndexOf(subTableName) > 0)
                            {
                                int p3 = detailHtml.IndexOf(trKeyStr, trKeyPosition + 6);
                                int p4 = detailHtml.IndexOf("</w:tr>", p3);
                                subBodyXML = detailHtml.Substring(p3, (p4 - p3 + 7));
                                break;
                            }
                            trKeyPosition = detailHtml.IndexOf(trKeyStr, trKeyPosition + 6);
                        }
                    }
                    if (subBodyXML != "")
                    {
                        StringBuilder stringBuilder = new StringBuilder();
                        foreach (DataRow subDr in dataSubTable.Rows)
                        {
                            string bakSubBodyXML = subBodyXML;
                            stringBuilder.Append(GetMainDetailHtml(subTableName, bakSubBodyXML, subDr));
                        }
                        detailHtml = detailHtml.Replace(subBodyXML, stringBuilder.ToString());
                    }
                }
            }
            return (detailHtml);
        }

        public DataTable GetMainData(string tblName, string condition)
        {
            DataTable retTable = null;
            string tblInfo = "select * from dbo.T_E_Sys_TableInfo where TableName=@TableName";
            DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(tblInfo);
            SysDatabase.AddInParameter(sqlStringCommand, "@TableName", DbType.String, tblName);
            DataTable dataTableInfo = SysDatabase.ExecuteTable(sqlStringCommand);
            if ((dataTableInfo != null) && (dataTableInfo.Rows.Count > 0))
            {
                string sql = dataTableInfo.Rows[0]["DetailSQL"].ToString();
                sql = sql.Replace("|^condition^|", condition);
                retTable = SysDatabase.ExecuteTable(SysDatabase.GetSqlStringCommand(sql));
            }
            return (retTable);
        }

        public List<Dictionary<string, string>> GetSubTableInfo(string tblName)
        {
            List<Dictionary<string, string>> subList = new List<Dictionary<string, string>>();
            string tblInfo = "select * from dbo.T_E_Sys_TableInfo where ParentName=@TableName";
            DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(tblInfo);
            SysDatabase.AddInParameter(sqlStringCommand, "@TableName", DbType.String, tblName);
            DataTable dataSubTableInfo = SysDatabase.ExecuteTable(sqlStringCommand);
            if ((dataSubTableInfo != null) && (dataSubTableInfo.Rows.Count > 0))
            {
                foreach (DataRow dr in dataSubTableInfo.Rows)
                {
                    Dictionary<string, string> subTblDic = new Dictionary<string, string>();
                    subTblDic.Add("SubTblName", dr["TableName"].ToString());
                    subList.Add(subTblDic);
                }
            }
            return (subList);
        }

        public DataTable GetSubTableData(string subTblName, string MainID)
        {
            string tblInfo = "select * from " + subTblName + " where _MainID=@MainID";
            DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(tblInfo);
            SysDatabase.AddInParameter(sqlStringCommand, "@MainID", DbType.String, MainID);
            return (SysDatabase.ExecuteTable(sqlStringCommand));
        }


        public Boolean IsExistWordTempletFile(string tblName)
        {
            Boolean wordTempXML = false;
            try
            {
                string sql = @"select top 1 * from dbo.T_E_File_File
                where AppName='T_E_APP_TableWordData' 
                and AppId in (select top 1 tempfile from T_E_APP_TableWordData 
                    where tablename=@tablename 
                    and getdate() BETWEEN StartDate and EndDate 
                    order by StartDate)
                and _IsDel=0 
                order by _CreateTime desc ";
                DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(sql);
                SysDatabase.AddInParameter(sqlStringCommand, "@tablename", DbType.String, tblName);
                DataTable dataTable = SysDatabase.ExecuteTable(sqlStringCommand);
                if ((dataTable != null) && (dataTable.Rows.Count > 0))
                {
                    string filepath = AppFilePath.GetBasePath("01");
                    filepath = filepath + dataTable.Rows[0]["FilePath"].ToString();
                    wordTempXML = File.Exists(filepath);
                }
            }
            catch { }
            finally { }
            return (wordTempXML);
        }

        private string GetTrXml(string trxml)
        {
            int p1 = trxml.IndexOf(">");
            string trKeyStr = trxml.Substring(0, p1 + 1);
            string[] arr_trStr = trKeyStr.Split(' ');
            trKeyStr = "";
            foreach (string item in arr_trStr)
            {
                if (item == "<w:tr")
                {
                    trKeyStr = item;
                }
                else if (item.IndexOf("xmlns") == -1)
                {
                    trKeyStr += " " + item;
                }
                else if (item == ">")
                {
                    trKeyStr += item;
                }
            }
            if (!trKeyStr.EndsWith(">")) trKeyStr += ">";
            return (trKeyStr);
        }




    }






}