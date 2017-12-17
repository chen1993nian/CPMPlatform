using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace Studio.JZY.SysFolder.Permission
{
    public partial class DefFunNodeDesktopImg : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((Request["Action"] != null) && (Request["Action"].ToString() == "GetData"))
            {
                List<Dictionary<string, string>> f = new List<Dictionary<string, string>>();
                string path = Server.MapPath("../../") + "Theme\\1\\images\\app_Icons\\";
                string[] arr_File = Directory.GetFiles(path);
                if (arr_File.Length > 0)
                {
                    foreach (string filename in arr_File)
                    {
                        string fn = Path.GetFileName(filename);
                        Dictionary<string, string> fd = new Dictionary<string, string>();
                        fd.Add("FileName", fn);
                        f.Add(fd);
                    }
                    //将对象转换为JSON字符串
                    string json_xml = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(f);
                    Response.Write(json_xml);
                }
                else
                {
                    Response.Write("[]");
                }
                Response.End();
            }
        }
    }
}