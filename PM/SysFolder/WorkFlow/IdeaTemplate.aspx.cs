using EIS.AppBase;
using EIS.DataAccess;
using EIS.WorkFlow.Access;
using EIS.WorkFlow.Model;
using EIS.WorkFlow.Service;
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

namespace EIS.Web.SysFolder.WorkFlow
{
    public partial class WfIdeaTemplate : PageBase
    {
     
        public string hidstate = "0";

        public string rowcount = "0";

        public string fieldHTML = "";

        public string fieldXML = "";

   

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            XmlNode xmlNodes = null;
            XmlElement xmlElement;
            IdeaTemplate ideaTemplate;
            StringBuilder stringBuilder = new StringBuilder();
            StringBuilder stringBuilder1 = new StringBuilder();
            StringBuilder stringBuilder2 = new StringBuilder();
            DbConnection dbConnection = SysDatabase.CreateConnection();
            dbConnection.Open();
            DbTransaction dbTransaction = dbConnection.BeginTransaction();
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(string.Concat("<?xml version='1.0' encoding='utf-8' ?>",Server.UrlDecode(base.Request["txtxml"])));
            try
            {
                try
                {
                    _IdeaTemplate __IdeaTemplate = new _IdeaTemplate(dbTransaction);
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
                            ideaTemplate = new IdeaTemplate()
                            {
                                _AutoID = Guid.NewGuid().ToString(),
                                _OrgCode = base.OrgCode,
                                _UserName = base.EmployeeID,
                                _CreateTime = DateTime.Now,
                                _UpdateTime = DateTime.Now,
                                _IsDel = 0,
                                IdeaUser = base.EmployeeID,
                                IdeaName = xmlElement.GetAttribute("txtname"),
                                OrderId = (xmlElement.GetAttribute("txtorder")== "" ? 1 :  Convert.ToInt32(xmlElement.GetAttribute("txtorder")))
                            };
                            __IdeaTemplate.Add(ideaTemplate);
                        }
                        foreach (XmlNode xmlNodes1 in xmlElement1.SelectNodes(string.Concat(str, "[@state='changed']")))
                        {
                            xmlElement = (XmlElement)xmlNodes1;
                            ideaTemplate = __IdeaTemplate.GetModel(xmlElement.GetAttribute("txtid"));
                            ideaTemplate.IdeaName = xmlElement.GetAttribute("txtname");
                            ideaTemplate.OrderId = Convert.ToInt32(xmlElement.GetAttribute("txtorder") == "" ? 1 : Convert.ToInt32(xmlElement.GetAttribute("txtorder")));
                            __IdeaTemplate.Update(ideaTemplate);
                        }
                        foreach (XmlNode xmlNodes2 in xmlElement1.SelectNodes(string.Concat(str, "[@state='deleted']")))
                        {
                            xmlElement = (XmlElement)xmlNodes2;
                            __IdeaTemplate.Delete(xmlElement.GetAttribute("txtid"));
                        }
                    }
                    dbTransaction.Commit();
                    base.ClientScript.RegisterStartupScript(base.GetType(), "", "afterSave();", true);
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
            List<IdeaTemplate> ideaTemplateByEmployeeId = IdeaTemplateService.GetIdeaTemplateByEmployeeId(base.EmployeeID);
            int num = 1;
            foreach (IdeaTemplate ideaTemplate in ideaTemplateByEmployeeId)
            {
                stringBuilder.Append("<tr>\n");
                string[] str = new string[] { "\t<td align=\"center\"><input class='textbox txtname'  type=\"text\" oindex=", num.ToString(), " name=\"txtname\" value='", ideaTemplate.IdeaName, "'></td>\n" };
                stringBuilder.Append(string.Concat(str));
                object[] ideaName = new object[] { "\t<td align=\"center\"><input class='textbox txtorder'  type=\"text\"  oindex=", num.ToString(), " name=\"txtorder\" value='", ideaTemplate.OrderId, "'></td>\n" };
                stringBuilder.Append(string.Concat(ideaName));
                stringBuilder.Append(string.Concat("\t<td align=\"center\"><input type=\"button\" value=\"删除\" onclick=\"dropfield('", num.ToString(), "')\"></td>\n"));
                stringBuilder.Append("</tr>\n");
                ideaName = new object[] { num, ideaTemplate.IdeaName, ideaTemplate.OrderId, ideaTemplate._AutoID };
                stringBuilder1.AppendFormat("<td oindex='{0}' txtname='{1}' txtorder='{2}' txtid='{3}' state='unchanged'/>", ideaName);
                num++;
            }
            this.rowcount = num.ToString();
            this.fieldHTML = stringBuilder.ToString();
            this.fieldXML = stringBuilder1.ToString();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                this.method_0();
            }
        }
    }
}