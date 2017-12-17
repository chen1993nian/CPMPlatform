using EIS.AppBase;
using EIS.AppEngine;
using EIS.DataAccess;
using EIS.DataModel.Access;
using NLog;
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Data.Common;
using System.IO;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Xml;

namespace EIS.Studio.SysFolder.DefFrame
{
    public partial class ModelImport : AdminPageBase
    {
       

        public StringBuilder tblHtml = new StringBuilder();

        public string view = "display:none;";

        public StringBuilder TipMessage = new StringBuilder();

        private string string_0 = "";

        public string ModelXml
        {
            get
            {
                string str;
                str = (this.ViewState["ModelXml"] == null ? "" : this.ViewState["ModelXml"].ToString());
                return str;
            }
            set
            {
                this.ViewState["ModelXml"] = value;
            }
        }

        public ModelImport()
        {
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            this.view = "";
            StreamReader streamReader = new StreamReader(this.FileUpload1.FileContent);
            this.ModelXml = streamReader.ReadToEnd();
            try
            {
                this.method_0();
            }
            catch (Exception exception1)
            {
                Exception exception = exception1;
                this.TipMessage.AppendFormat("<div class=\"tip\">{0}</div>", string.Concat("在分析模型文件时出错：", exception.Message));
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            if (this.ModelXml.Length == 0)
            {
                this.TipMessage.AppendFormat("<div class=\"ErrorMsg\">{0}</div>", "模型文件不能为空");
            }
            else if (!string.IsNullOrEmpty(base.Request["chkitem"]))
            {
                DbConnection dbConnection = SysDatabase.CreateConnection();
                dbConnection.Open();
                DbTransaction dbTransaction = dbConnection.BeginTransaction();
                try
                {
                    string[] strArrays = base.Request["chkitem"].Split(new char[] { ',' });
                    XmlDocument xmlDocument = new XmlDocument();
                    xmlDocument.LoadXml(this.ModelXml);
                    string[] strArrays1 = strArrays;
                    for (int i = 0; i < (int)strArrays1.Length; i++)
                    {
                        string str = strArrays1[i];
                        XmlNode xmlNodes = xmlDocument.DocumentElement.SelectSingleNode(string.Concat("bizModel[@TableName='", str, "']"));
                        if (xmlNodes == null)
                        {
                            xmlNodes = xmlDocument.DocumentElement.SelectSingleNode(string.Concat("Dict[@_AutoID='", str, "']"));
                            if (xmlNodes != null)
                            {
                                ModelIO.ImportDict((XmlElement)xmlNodes, this.string_0, true, dbTransaction);
                            }
                        }
                        else
                        {
                            string str1 = this.method_1(str);
                            bool flag = false;
                            if ((str1 == "" ? true : str1 == "0"))
                            {
                                flag = true;
                            }
                            ModelIO.ImportTable((XmlElement)xmlNodes, this.string_0, flag, dbTransaction);
                        }
                    }
                    dbTransaction.Commit();
                    this.TipMessage.AppendFormat("<div class=\"tip\">{0}</div>", "模型文件已经成功导入！");
                }
                catch (Exception exception1)
                {
                    Exception exception = exception1;
                    dbTransaction.Rollback();
                    this.TipMessage.AppendFormat("<div class=\"tip\">{0}</div>", string.Concat("在导入模型文件时出错：", exception.Message));
                    if (exception.InnerException == null)
                    {
                        this.fileLogger.Error<Exception>(exception);
                    }
                    else
                    {
                        this.fileLogger.Error<Exception>(exception.InnerException);
                    }
                }
            }
            else
            {
                this.view = "";
                this.method_0();
                this.TipMessage.AppendFormat("<div class=\"tip\">{0}</div>", "请选择业务对象");
            }
        }

        private void method_0()
        {
            XmlElement xmlElement;
            string attribute;
            object[] objArray;
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(this.ModelXml);
            foreach (XmlNode xmlNodesA in xmlDocument.DocumentElement.SelectNodes("bizModel"))
            {
                xmlElement = (XmlElement)xmlNodesA;
                attribute = xmlElement.GetAttribute("TableName");
                string str = xmlElement.GetAttribute("TableType");
                _TableInfo __TableInfo = new _TableInfo(attribute);
                string str1 = this.method_1(attribute);
                str = (str == "1" ? "表单" : "查询");
                StringBuilder stringBuilder = this.tblHtml;
                objArray = new object[] { attribute, str, null, null, null, null };
                objArray[2] = (__TableInfo.Exists() ? "存在" : "");
                objArray[3] = (__TableInfo.Exists() ? "exist" : "");
                objArray[4] = (__TableInfo.Exists() ? "" : "checked");
                objArray[5] = (str1 == "" ? "不存在" : string.Concat(str1, "条记录"));
                stringBuilder.AppendFormat("<tr class='{3}'>\r\n                        <td><input name='chkitem' class='chkitem' {4} type='checkbox' id='chk{0}' value='{0}'><label for='chk{0}'>{0}&nbsp;&nbsp;</label></td>\r\n                        <td>{1}</td>\r\n                        <td>{2}</td>\r\n                        <td>{5}</td>\r\n                        </tr>", objArray);
            }
            XmlNodeList xmlNodeLists = xmlDocument.DocumentElement.SelectNodes("Dict");
            StringCollection stringCollections = new StringCollection();
            foreach (XmlNode xmlNodes1 in xmlNodeLists)
            {
                xmlElement = (XmlElement)xmlNodes1;
                attribute = xmlElement.GetAttribute("DictName");
                string attribute1 = xmlElement.GetAttribute("_AutoID");
                if (stringCollections.Contains(attribute1))
                {
                    continue;
                }
                stringCollections.Add(attribute1);
                _Dict __Dict = new _Dict();
                StringBuilder stringBuilder1 = this.tblHtml;
                objArray = new object[] { attribute1, attribute, null, null, null };
                objArray[2] = (__Dict.Exists(attribute1) ? "存在" : "");
                objArray[3] = (__Dict.Exists(attribute1) ? "exist" : "");
                objArray[4] = (__Dict.Exists(attribute1) ? "" : "checked");
                stringBuilder1.AppendFormat("<tr class='{3}'>\r\n                        <td><input name='chkitem' class='chkitem' {4}  type='checkbox' id='chk{0}' value='{0}'><label for='chk{0}'>{1}&nbsp;&nbsp;</label></td>\r\n                        <td>字典</td>\r\n                        <td class=''>{2}</td>\r\n                        <td class=''></td>\r\n                        </tr>", objArray);
            }
        }

        private string method_1(string string_1)
        {
            string str;
            if (!string.IsNullOrEmpty(string_1))
            {
                object obj = SysDatabase.ExecuteScalar(string.Format("If exists (select * from sysobjects where [name] = '{0}' and xtype='U')\r\n\t                                            select count(*) from {0}\r\n                                            else\r\n\t                                            select null", string_1));
                str = (obj != DBNull.Value ? obj.ToString() : "");
            }
            else
            {
                str = "";
            }
            return str;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.string_0 = base.GetParaValue("catCode");
        }
    }
}