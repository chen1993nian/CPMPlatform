using EIS.AppBase;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using WebBase.JZY.Tools;
namespace EIS.WebBase.SysFolder.AppFrame
{
    public partial class AppInfo : PageBase
    {
        
        public StringBuilder DealInfo = new StringBuilder();

        public string taskId = "";

        public string perror = "";

        public string ImgName = "icon64_info.png";

       

        protected void Page_Load(object sender, EventArgs e)
        {
            int num;
            if (this.Session["_sysinfo"] != null)
            {
                this.DealInfo.Append(this.Session["_sysinfo"].ToString());
            }
            if (!string.IsNullOrEmpty(base.Request["msgType"]))
            {
                string str = base.Request["msgType"].ToString();
                if (str != null)
                {
                    if (Class7.dictionary_0 == null)
                    {
                        Class7.dictionary_0 = new Dictionary<string, int>(6)
						{
							{ "error", 0 },
							{ "forbidden", 1 },
							{ "help", 2 },
							{ "info", 3 },
							{ "stop", 4 },
							{ "warning", 5 }
						};
                    }
                    if (Class7.dictionary_0.TryGetValue(str, out num))
                    {
                        switch (num)
                        {
                            case 0:
                                {
                                    this.ImgName = "icon64_error.png";
                                    break;
                                }
                            case 1:
                                {
                                    this.ImgName = "icon64_forbidden.png";
                                    break;
                                }
                            case 2:
                                {
                                    this.ImgName = "icon64_help.png";
                                    break;
                                }
                            case 3:
                                {
                                    this.ImgName = "icon64_info.png";
                                    break;
                                }
                            case 4:
                                {
                                    this.ImgName = "icon64_stop.png";
                                    break;
                                }
                            case 5:
                                {
                                    this.ImgName = "icon64_warning.png";
                                    break;
                                }
                        }
                    }
                }
            }
        }
    }
}