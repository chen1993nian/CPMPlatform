using EIS.AppBase;
using System;
using System.Collections;
using System.Text;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Xml;

namespace EIS.Web.SysFolder.AppFrame
{
    public partial class AppDataLog : PageBase
    {
       
        private string string_0 = "";

        private string string_1 = "";

        private string string_2 = "";

        public DataLog LogModel = null;

        public StringBuilder sbLog = new StringBuilder();

        public AppDataLog()
        {
        }

        private string method_0(XmlNode xmlNode_0)
        {
            StringBuilder stringBuilder = new StringBuilder();
            XmlElement xmlNode0 = (XmlElement)xmlNode_0;
            stringBuilder.Append("<tr>");
            stringBuilder.AppendFormat("<td>{0}〔{1}〕</td>", xmlNode0.Name, xmlNode0.Attributes["cn"].Value);
            XmlNode xmlNodes = xmlNode_0.SelectSingleNode("old");
            if (xmlNodes == null)
            {
                stringBuilder.AppendFormat("<td>&nbsp;</td>", new object[0]);
            }
            else
            {
                stringBuilder.AppendFormat("<td>{0}</td>", xmlNodes.InnerText);
            }
            XmlNode xmlNodes1 = xmlNode_0.SelectSingleNode("new");
            if (xmlNodes1 == null)
            {
                stringBuilder.AppendFormat("<td>&nbsp;</td>", new object[0]);
            }
            else
            {
                stringBuilder.AppendFormat("<td>{0}</td>", xmlNodes1.InnerText);
            }
            stringBuilder.Append("</tr>");
            return stringBuilder.ToString();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.string_0 = base.GetParaValue("LogId");
            if (this.string_0 != "")
            {
                this.LogModel = (new _DataLog()).GetDataLog(this.string_0);
                if (this.LogModel == null)
                {
                    this.Session["_sysinfo"] = string.Format("数据日志已经不存在，ID={0}", this.string_0);
                    base.Response.Redirect("AppInfo.aspx?msgType=warning", true);
                }
                else if (this.LogModel.Data.Length > 0)
                {
                    try
                    {
                        XmlDocument xmlDocument = new XmlDocument();
                        xmlDocument.LoadXml(string.Concat("<root>", this.LogModel.Data, "</root>"));
                        foreach (XmlNode childNode in xmlDocument.DocumentElement.ChildNodes)
                        {
                            XmlElement xmlElement = (XmlElement)childNode;
                            string value = "";
                            string lower = "";
                            if (xmlElement.Attributes["id"] != null)
                            {
                                value = xmlElement.Attributes["id"].Value;
                            }
                            if (xmlElement.Attributes["status"] != null)
                            {
                                lower = xmlElement.Attributes["status"].Value.ToLower();
                                if (lower == "detached")
                                {
                                    lower = "新建";
                                }
                                else if (lower == "modified")
                                {
                                    lower = "修改";
                                }
                                else if (lower == "deleted")
                                {
                                    lower = "删除";
                                }
                            }
                            this.sbLog.AppendFormat("<div class='subtitle'><span class='tblname'>【操作】：</span>{2}，<span class='tblname'>【表名】：</span>{0}，<span class='autoid'>【ID】：</span>{1}</div>", xmlElement.Name, value, lower);
                            this.sbLog.Append("<table class='logtbl' border='1'><thead><tr><th class='titlefld'>字段名称</th><th>修改前</th><th>修改后</th></tr></thead>");
                            foreach (XmlNode xmlNodes in xmlElement.ChildNodes)
                            {
                                this.sbLog.Append(this.method_0(xmlNodes));
                            }
                            this.sbLog.Append("</table>");
                        }
                    }
                    catch (Exception exception)
                    {
                        this.Session["_sysinfo"] = exception.Message;
                        base.Response.Redirect("AppInfo.aspx?msgType=error", true);
                    }
                }
            }
        }
    }
}