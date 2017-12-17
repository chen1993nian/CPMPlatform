using EIS.AppBase;
using EIS.DataAccess;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Xml;
using EIS.Studio;
using WebBase.JZY.Tools;

namespace EIS.Studio.SysFolder.DefFrame
{
    public partial class DefTableRelationKey : AdminPageBase
    {
      

        public string fieldHTML = "";

        public string fieldHTML2 = "";

        public string fieldXML = "";

        public string rowcount = "0";

        public string disp = "";

        public string tblname = "";

        public DefTableRelationKey()
        {
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            XmlElement xmlElement;
            object[] attribute;
            this.tblname = base.GetParaValue("tblname");
            StringBuilder stringBuilder = new StringBuilder();
            StringBuilder stringBuilder1 = new StringBuilder();
            StringBuilder stringBuilder2 = new StringBuilder();
            DbConnection dbConnection = SysDatabase.CreateConnection();
            dbConnection.Open();
            DbTransaction dbTransaction = dbConnection.BeginTransaction();
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(string.Concat("<?xml version='1.0' encoding='utf-8' ?>", base.Request["txtxml"]));
            XmlElement xmlElement1 = (XmlElement)xmlDocument.SelectSingleNode("tr");
            if ((xmlElement1 == null ? false : xmlElement1.SelectNodes("TD").Count > 0))
            {
                foreach (XmlNode xmlNodesA in xmlElement1.SelectNodes("TD[@state='changed']"))
                {
                    xmlElement = (XmlElement)xmlNodesA;
                    attribute = new object[] { xmlElement.GetAttribute("MainTable"), xmlElement.GetAttribute("MainKey"), xmlElement.GetAttribute("SubTable"), xmlElement.GetAttribute("SubKey").Trim(), xmlElement.GetAttribute("RelationID").Trim() };
                    stringBuilder1.AppendFormat("update T_E_Sys_Relation set MainTable='{0}',MainKey='{1}',SubTable='{2}',SubKey='{3}' where RelationID='{4}' ;", attribute);
                }
            }
            foreach (XmlNode xmlNodes1 in xmlElement1.SelectNodes("TD[@state='add']"))
            {
                xmlElement = (XmlElement)xmlNodes1;
                attribute = new object[] { xmlElement.GetAttribute("MainTable"), xmlElement.GetAttribute("MainKey"), xmlElement.GetAttribute("SubTable"), xmlElement.GetAttribute("SubKey"), this.tblname };
                stringBuilder.AppendFormat("insert into T_E_Sys_Relation (MainTable,MainKey,SubTable,SubKey,TableName) values\r\n                                        ('{0}','{1}','{2}','{3}','{4}');", attribute);
            }
            foreach (XmlNode xmlNodes2 in xmlElement1.SelectNodes("TD[@state='deleted']"))
            {
                xmlElement = (XmlElement)xmlNodes2;
                stringBuilder2.AppendFormat("delete T_E_Sys_Relation where tablename='{0}' and RelationID='{1}' ;", this.tblname, xmlElement.GetAttribute("RelationID"));
            }
            if (stringBuilder.Length > 0)
            {
                SysDatabase.ExecuteNonQuery(stringBuilder.ToString(), dbTransaction);
            }
            if (stringBuilder1.Length > 0)
            {
                SysDatabase.ExecuteNonQuery(stringBuilder1.ToString(), dbTransaction);
            }
            if (stringBuilder2.Length > 0)
            {
                SysDatabase.ExecuteNonQuery(stringBuilder2.ToString(), dbTransaction);
            }
            try
            {
                dbTransaction.Commit();
                base.ClientScript.RegisterClientScriptBlock(base.GetType(), "", "<script language=javascript>$.noticeAdd({text:'保存成功！',stay:false});</script>");
            }
            catch (Exception exception1)
            {
                Exception exception = exception1;
                dbTransaction.Rollback();
                base.Response.Write(string.Concat("出现错误:", exception.ToString()));
            }
            this.method_0();
        }

        private void method_0()
        {
            StringBuilder stringBuilder = new StringBuilder();
            StringBuilder stringBuilder1 = new StringBuilder();
            StringBuilder stringBuilder2 = new StringBuilder();
            this.tblname = base.GetParaValue("tblname");
            string str = string.Format(string.Concat("select * from T_E_Sys_Relation where tablename='", this.tblname, "'"), new object[0]);
            DbDataReader dbDataReaders = SysDatabase.ExecuteReader(str);
            try
            {
                int num = 1;
                if (dbDataReaders.HasRows)
                {
                    while (dbDataReaders.Read())
                    {
                        stringBuilder.Append("<tr>\n");
                        stringBuilder.Append(string.Concat("\t<td align=\"center\">", num.ToString(), "</td>\n"));
                        object[] item = new object[] { "\t<td align=\"center\"><input class='textbox tblinput' id=\"MainTable\"  type=\"text\"  oindex=", num.ToString(), " name=\"MainTable\" value='", dbDataReaders["MainTable"], "'></td>\n" };
                        stringBuilder.Append(string.Concat(item));
                        item = new object[] { "\t<td align=\"center\"><input class='textbox fieldinput' id=\"MainKey\"  type=\"text\"  oindex=", num.ToString(), " name=\"MainKey\" value='", dbDataReaders["MainKey"], "'></td>\n" };
                        stringBuilder.Append(string.Concat(item));
                        item = new object[] { "\t<td align=\"center\"><input class='textbox tblinput' id=\"SubTable\"  type=\"text\"  oindex=", num.ToString(), " name=\"SubTable\" value='", dbDataReaders["SubTable"], "'></td>\n" };
                        stringBuilder.Append(string.Concat(item));
                        item = new object[] { "\t<td align=\"center\"><input class='textbox fieldinput' id=\"SubKey\"  type=\"text\"  oindex=", num.ToString(), " name=\"SubKey\" value='", dbDataReaders["SubKey"], "'></td>\n" };
                        stringBuilder.Append(string.Concat(item));
                        stringBuilder.Append(string.Concat("\t<td align=\"center\"><input id=\"btndel\"  type=\"button\" onclick='dropfield(", num.ToString(), ");'  name=\"btndel\" value='删除'></td>\n"));
                        stringBuilder.Append("</tr>\n");
                        item = new object[] { num, dbDataReaders["MainTable"], dbDataReaders["MainKey"], dbDataReaders["SubTable"], dbDataReaders["SubKey"], dbDataReaders["RelationID"] };
                        stringBuilder2.AppendFormat("<td oindex={0} MainTable='{1}'  MainKey='{2}' SubTable='{3}' SubKey='{4}' RelationID='{5}' state='unchanged'/>", item);
                        num++;
                    }
                    this.fieldHTML = stringBuilder.ToString();
                    this.fieldXML = stringBuilder2.ToString();
                }
                this.rowcount = num.ToString();
            }
            finally
            {
                dbDataReaders.Close();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                this.method_0();
            }
            if (base.Request["t"] == "3")
            {
                this.disp = "display:none";
            }
        }

        public string TransType(string nettype)
        {
            string str;
            int num;
            string str1 = nettype;
            if (str1 != null)
            {
                if (Class7.dictionary_1 == null)
                {
                    Class7.dictionary_1 = new Dictionary<string, int>(6)
					{
						{ "int", 0 },
						{ "datetime", 1 },
						{ "varchar", 2 },
						{ "nvarchar", 3 },
						{ "decimal", 4 },
						{ "text", 5 }
					};
                }
                if (Class7.dictionary_1.TryGetValue(str1, out num))
                {
                    switch (num)
                    {
                        case 0:
                            {
                                str = "2";
                                break;
                            }
                        case 1:
                            {
                                str = "4";
                                break;
                            }
                        case 2:
                        case 3:
                            {
                                str = "1";
                                break;
                            }
                        case 4:
                            {
                                str = "3";
                                break;
                            }
                        case 5:
                            {
                                str = "5";
                                break;
                            }
                        default:
                            {
                                str = "1";
                                return str;
                            }
                    }
                }
                else
                {
                    str = "1";
                    return str;
                }
            }
            else
            {
                str = "1";
                return str;
            }
            return str;
        }
    }
}