using EIS.AppBase;
using EIS.DataModel.Model;
using EIS.DataModel.Service;
using EIS.Permission.Access;
using EIS.Permission.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.HtmlControls;

namespace EIS.WebBase.SysFolder.Common
{
    public partial class FileList : PageBase
    {
      

        public string fileList = "";

        public FileList()
        {
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            StringBuilder stringBuilder = new StringBuilder();
            string paraValue = base.GetParaValue("appId");
            string str = base.GetParaValue("appName");
            IList<AppFile> files = (new FileService()).GetFiles(str, paraValue);
            int num = 1;
            foreach (AppFile file in files)
            {
                Employee model = (new _Employee()).GetModel(file._UserName);
                stringBuilder.AppendFormat("<tr>", new object[0]);
                int num1 = num;
                num = num1 + 1;
                stringBuilder.AppendFormat("<td>{0}</td>", num1);
                stringBuilder.AppendFormat("<td>{0}</td>", file.FactFileName);
                stringBuilder.AppendFormat("<td>{0}K</td>", file.FileSize / 0x400);
                stringBuilder.AppendFormat("<td>{0}</td>", file._CreateTime);
                stringBuilder.AppendFormat("<td>{0}</td>", (model == null ? "" : model.EmployeeName));
                stringBuilder.AppendFormat("<td align=center><a href='FileDown.aspx?para={0}' target='_blank'>下载</a></td>", base.CryptPara(string.Concat("fileId=", file._AutoID)));
                stringBuilder.AppendFormat("</tr>", new object[0]);
            }
            this.fileList = stringBuilder.ToString();
        }
    }
}