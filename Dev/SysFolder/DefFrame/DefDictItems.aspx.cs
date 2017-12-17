using EIS.AppBase;
using EIS.DataAccess;
using EIS.DataModel.Access;
using EIS.DataModel.Model;
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

namespace EIS.Studio.SysFolder.DefFrame
{
    public partial class DefDictItems : AdminPageBase
    {
        public string hidstate = "0";

        public string rowcount = "0";

        public string fieldHTML = "";

        public string fieldXML = "";

        public string Tips = "";

     

        public string DictId
        {
            get
            {
                string str;
                str = (this.ViewState["DictId"] == null ? "" : this.ViewState["DictId"].ToString());
                return str;
            }
            set
            {
                this.ViewState["DictId"] = value;
            }
        }

        public DefDictItems()
        {
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            _Dict __Dict;
            Dict dict;
            XmlElement xmlElement;
            DictEntry dictEntry;
            StringBuilder stringBuilder = new StringBuilder();
            StringBuilder stringBuilder1 = new StringBuilder();
            StringBuilder stringBuilder2 = new StringBuilder();
            if (string.IsNullOrEmpty(this.DictId))
            {
                this.DictId = Guid.NewGuid().ToString();
                __Dict = new _Dict();
                dict = new Dict()
                {
                    _AutoID = this.DictId,
                    _OrgCode = base.OrgCode,
                    _UserName = base.EmployeeID,
                    _CreateTime = DateTime.Now,
                    _UpdateTime = DateTime.Now,
                    _IsDel = 0,
                    DictCode = this.TextBox2.Text,
                    DictName = this.TextBox1.Text,
                    DictCat = base.GetParaValue("cat")
                };
                if ((dict.DictCode == "" ? false : __Dict.Exists(this.DictId, dict.DictCode)))
                {
                    this.Tips = "<div class='tip'>字典编码重复！</div>";
                    this.method_0();
                    return;
                }
                __Dict.Add(dict);
            }
            else
            {
                __Dict = new _Dict();
                dict = __Dict.GetModel(this.DictId);
                dict._UpdateTime = DateTime.Now;
                dict.DictName = this.TextBox1.Text;
                dict.DictCode = this.TextBox2.Text;
                if ((dict.DictCode == "" ? false : __Dict.Exists(this.DictId, dict.DictCode)))
                {
                    this.Tips = "<div class='tip'>字典编码重复！</div>";
                    this.method_0();
                    return;
                }
                __Dict.Update(dict);
            }
            DbConnection dbConnection = SysDatabase.CreateConnection();
            dbConnection.Open();
            DbTransaction dbTransaction = dbConnection.BeginTransaction();
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(string.Concat("<?xml version='1.0' encoding='utf-8' ?>", base.Request["txtxml"]));
            try
            {
                try
                {
                    _DictEntry __DictEntry = new _DictEntry(dbTransaction);
                    XmlElement xmlElement1 = (XmlElement)xmlDocument.SelectSingleNode("tr");
                    string str = "td";
                    if (xmlElement1.SelectNodes("TD").Count > 0)
                    {
                        str = "TD";
                    }
                    if ((xmlElement1 == null ? false : xmlElement1.SelectNodes(str).Count > 0))
                    {
                        foreach (XmlNode xmlNodesA in xmlElement1.SelectNodes(string.Concat(str, "[@state='add']")))
                        {
                            xmlElement = (XmlElement)xmlNodesA;
                            dictEntry = new DictEntry()
                            {
                                _AutoID = Guid.NewGuid().ToString(),
                                _OrgCode = base.OrgCode,
                                _UserName = base.EmployeeID,
                                _CreateTime = DateTime.Now,
                                _UpdateTime = DateTime.Now,
                                _IsDel = 0,
                                DictID = this.DictId,
                                ItemCode = xmlElement.GetAttribute("txtcode"),
                                ItemName = xmlElement.GetAttribute("txtname"),
                                ItemOrder = int.Parse(xmlElement.GetAttribute("txtorder"))
                            };
                            __DictEntry.Add(dictEntry);
                        }
                        foreach (XmlNode xmlNodes1 in xmlElement1.SelectNodes(string.Concat(str, "[@state='changed']")))
                        {
                            xmlElement = (XmlElement)xmlNodes1;
                            object[] attribute = new object[] { xmlElement.GetAttribute("txtcode"), xmlElement.GetAttribute("txtname"), xmlElement.GetAttribute("txtorder").Trim(), xmlElement.GetAttribute("txtid") };
                            stringBuilder1.AppendFormat("update T_E_Sys_DictEntry set itemcode='{0}',itemname='{1}',itemorder='{2}' where itemid='{3}';", attribute);
                            dictEntry = __DictEntry.GetModel(xmlElement.GetAttribute("txtid"));
                            dictEntry.ItemCode = xmlElement.GetAttribute("txtcode");
                            dictEntry.ItemName = xmlElement.GetAttribute("txtname");
                            dictEntry.ItemOrder = int.Parse(xmlElement.GetAttribute("txtorder"));
                            __DictEntry.Update(dictEntry);
                        }
                        foreach (XmlNode xmlNodes2 in xmlElement1.SelectNodes(string.Concat(str, "[@state='deleted']")))
                        {
                            xmlElement = (XmlElement)xmlNodes2;
                            __DictEntry.Delete(xmlElement.GetAttribute("txtid"));
                        }
                    }
                    dbTransaction.Commit();
                    base.ClientScript.RegisterStartupScript(base.GetType(), "", "success();", true);
                }
                catch (Exception exception1)
                {
                    Exception exception = exception1;
                    dbTransaction.Rollback();
                    base.Response.Write(string.Concat("出现错误:", exception.ToString()));
                }
            }
            finally
            {
                dbConnection.Close();
            }
            this.method_0();
        }

        private void method_0()
        {
            StringBuilder stringBuilder = new StringBuilder();
            StringBuilder stringBuilder1 = new StringBuilder();
            List<DictEntry> modelListByDictId = (new _DictEntry()).GetModelListByDictId(this.DictId);
            int num = 1;
            foreach (DictEntry dictEntry in modelListByDictId)
            {
                stringBuilder.Append("<tr>\n");
                string[] str = new string[] { "\t<td align=\"center\"><input class='textbox' id=\"txtname", num.ToString(), "\"  type=\"text\" oindex=", num.ToString(), " name=\"txtname\" value='", dictEntry.ItemName, "'></td>\n" };
                stringBuilder.Append(string.Concat(str));
                str = new string[] { "\t<td align=\"center\"><input class='textbox' id=\"txtcode", num.ToString(), "\"  type=\"text\"  oindex=", num.ToString(), " name=\"txtcode\" value='", dictEntry.ItemCode, "'></td>\n" };
                stringBuilder.Append(string.Concat(str));
                object[] itemCode = new object[] { "\t<td align=\"center\"><input class='textbox' id=\"txtorder", num.ToString(), "\"  type=\"text\"  oindex=", num.ToString(), " name=\"txtorder\" value='", dictEntry.ItemOrder, "'></td>\n" };
                stringBuilder.Append(string.Concat(itemCode));
                stringBuilder.Append(string.Concat("\t<td align=\"center\"><input type=\"button\" value=\"删除\" onclick=\"dropfield('", num.ToString(), "')\"></td>\n"));
                stringBuilder.Append("</tr>\n");
                itemCode = new object[] { num, dictEntry.ItemCode, dictEntry.ItemName, dictEntry.ItemOrder, dictEntry._AutoID };
                stringBuilder1.AppendFormat("<td oindex='{0}' txtcode='{1}' txtname='{2}' txtorder='{3}' txtid='{4}' state='unchanged'/>", itemCode);
                num++;
            }
            this.rowcount = num.ToString();
            this.fieldHTML = stringBuilder.ToString();
            this.fieldXML = stringBuilder1.ToString();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.DictId = base.GetParaValue("dictid");
            if (!base.IsPostBack && this.DictId != "")
            {
                Dict model = (new _Dict()).GetModel(this.DictId);
                this.TextBox1.Text = model.DictName;
                this.TextBox2.Text = model.DictCode;
                this.method_0();
            }
        }
    }
}