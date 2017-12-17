using EIS.AppBase;
using System;
using System.Web.UI.HtmlControls;

namespace Studio.JZY.Doc
{
    public partial class DocShareFrame : PageBase
    {
      

        public string EmpId = "";

        public string folderId = "";

        public string folderPId = "";

  
        protected void Page_Load(object sender, EventArgs e)
        {
            this.EmpId = base.GetParaValue("employeeId");
            if (this.EmpId.Length == 0)
            {
                this.EmpId = base.EmployeeID;
            }
        }
    }
}