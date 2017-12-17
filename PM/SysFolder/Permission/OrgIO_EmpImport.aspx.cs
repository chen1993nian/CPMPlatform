using Aspose.Cells;
using EIS.AppBase;
using EIS.DataModel.Model;
using EIS.DataModel.Service;
using EIS.Permission.Service;
using System;
using System.Data;
using System.IO;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace EIS.Web.SysFolder.Permission
{
    public partial class OrgIO_EmpImport : AdminPageBase
    {
        public string TipMessage = "";

        public string tblHtml = "";
        protected void Button1_Click(object sender, EventArgs e)
        {
            AppFile lastFileByAppId = FileService.GetLastFileByAppId("OrgIO_EmpImport");
            if (lastFileByAppId == null)
            {
                this.TipMessage = string.Format("<div id='errorInfo' class='tip'>处理信息：请先上传数据文件</div>", new object[0]);
            }
            else
            {
                string basePath = AppFilePath.GetBasePath(lastFileByAppId.BasePath);
                if (File.Exists(string.Concat(basePath, lastFileByAppId.FilePath)))
                {
                    if (this.CheckBox1.Checked)
                    {
                        OrgIO.ClearAllEmployee();
                    }
                    string str = "Sheet1";
                    DataTable dataTable = null;
                    dataTable = this.method_0(string.Concat(basePath, lastFileByAppId.FilePath), str);
                    this.TextBox1.Visible = true;
                    StringBuilder stringBuilder = new StringBuilder();
                    stringBuilder.Append(OrgIO.EmpImport(dataTable));
                    DataRow[] dataRowArray = dataTable.Select("flag=1");
                    for (int i = 0; i < (int)dataRowArray.Length; i++)
                    {
                        DataRow dataRow = dataRowArray[i];
                        string str1 = dataRow[0].ToString();
                        string str2 = dataRow[1].ToString();
                        string str3 = dataRow[3].ToString();
                        dataRow[4].ToString().ToLower().Replace(" ", "");
                        stringBuilder.AppendFormat("\r{0}\t{1}（{2}）", str1, str3, str2);
                    }
                    this.TextBox1.Text = stringBuilder.ToString();
                }
                else
                {
                    this.TipMessage = string.Format("<div class='tip'>处理信息：文件不存在[{0}]</div>", string.Concat(basePath, lastFileByAppId.FilePath));
                }
            }
        }

        private DataTable method_0(string fullPath, string sheetName)
        {
            int i;
            Workbook workbook = new Workbook();
            workbook.Open(fullPath);
            Cells cells = workbook.Worksheets[sheetName].Cells;
            DataTable dataTable = new DataTable();
            for (i = 0; i < cells.MaxDataColumn + 1; i++)
            {
                dataTable.Columns.Add(string.Concat("a", i.ToString()), typeof(string));
            }
            for (i = 1; i < cells.MaxDataRow + 1; i++)
            {
                DataRow dataRow = dataTable.NewRow();
                for (int j = 0; j < cells.MaxDataColumn + 1; j++)
                {
                    string str = cells[i, j].StringValue.Trim();
                    dataRow[j] = str;
                }
                dataTable.Rows.Add(dataRow);
            }
            workbook = null;
            return dataTable;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                this.TextBox1.Visible = false;
            }
        }
    }
}