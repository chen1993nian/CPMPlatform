using EIS.AppBase;
using EIS.AppEngine;
using System;
using System.Collections.Specialized;
using System.Web;
using System.Web.UI;
using System.Xml;

namespace EIS.Studio.SysFolder.DefFrame
{
    public partial class ModelExportResponse : AdminPageBase
    {
       

        protected void Page_Load(object sender, EventArgs e)
        {
            string paraValue = base.GetParaValue("tblName");
            XmlDocument xmlDocument = new XmlDocument();
            XmlDeclaration xmlDeclaration = xmlDocument.CreateXmlDeclaration("1.0", "utf-8", null);
            xmlDocument.AppendChild(xmlDeclaration);
            xmlDocument.AppendChild(xmlDocument.CreateElement("root"));
            try
            {
                string[] strArrays = paraValue.Split(new char[] { ',' });
                StringCollection stringCollections = new StringCollection();
                string[] strArrays1 = strArrays;
                for (int i = 0; i < (int)strArrays1.Length; i++)
                {
                    ModelIO.ExportTable(strArrays1[i], stringCollections, xmlDocument);
                }
                base.Response.Charset = "UTF-8";
                base.Response.AddHeader("Content-Type", "application/octet-stream");
                HttpResponse response = base.Response;
                HttpServerUtility server = base.Server;
                DateTime today = DateTime.Today;
                response.AddHeader("Content-Disposition", string.Concat("attachment;filename=\"", server.UrlEncode(today.ToString("业务模型_yyyyMMdd")), ".xml\""));
                base.Response.ContentType = "text/xml";
                base.Response.Write(xmlDocument.OuterXml);
            }
            catch (Exception exception1)
            {
                Exception exception = exception1;
                base.Response.Write(string.Concat("导出模型时出错：", exception.Message));
            }
        }
    }
}