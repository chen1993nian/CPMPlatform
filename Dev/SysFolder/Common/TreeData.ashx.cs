using EIS.Permission.Service;
using System;
using System.Data;
using System.Text;
using System.Web;
using System.Web.SessionState;

namespace EIS.WebBase.SysFolder.Common
{
    /// <summary>
    /// TreeData 的摘要说明
    /// </summary>
    public class TreeData : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            HttpRequest request = context.Request;
            string lower = request["queryid"].ToLower();
            string item = request["value"];
            string str = lower;
            if (str != null)
            {
                if (str == "positionbydeptid")
                {
                    this.stringBuilder_0.Append(PositionService.GetJsonPostionByDeptId(item));
                }
                else if (str == "rolebycatid")
                {
                    this.stringBuilder_0.Append(RoleService.GetJsonRoleByCatId(item));
                }
                else if (str == "employeebydeptid")
                {
                    this.stringBuilder_0.Append(EmployeeService.GetJsonEmployeeByDeptId(item));
                }
                else if (str == "deptandemployeebydeptid")
                {
                    this.stringBuilder_0.Append(DepartmentService.GetJsonDeptAndEmployeeByDeptId(item));
                }
            }
            context.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            context.Response.ContentType = "application/json";
            context.Response.Write(this.stringBuilder_0.ToString());
        }

        private StringBuilder stringBuilder_0 = new StringBuilder();

        private DataTable dataTable_0 = new DataTable();

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}