using EIS.AppBase;
using EIS.AppModel.Service;
using EIS.DataModel.Access;
using EIS.DataModel.Model;
using EIS.Permission.Service;
using System;
using System.Collections.Specialized;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Studio.JZY.Doc
{
    public partial class DocFolderSend : PageBase
    {
        public string fullPath = "";

        public string folderId = "";


        protected void Button1_Click(object sender, EventArgs e)
        {
            if (HttpContext.Current.Session["FileSelect"] != null)
            {
                if (this.hidShareId.Value.Length != 0)
                {
                    string str = HttpContext.Current.Session["FileSelect"].ToString();
                    if (str.Length == 0)
                    {
                        return;
                    }
                    string str1 = str.Substring(2);
                    char[] chrArray = new char[] { '$' };
                    string[] strArrays = str1.Split(chrArray);
                    if (str.StartsWith("1"))
                    {
                        _AppFile __AppFile = new _AppFile();
                        StringCollection stringCollections = new StringCollection();
                        string value = this.hidShareId.Value;
                        chrArray = new char[] { ',' };
                        stringCollections.AddRange(value.Split(chrArray));
                        foreach (string stringCollection in stringCollections)
                        {
                            string str2 = strArrays[0];
                            chrArray = new char[] { ',' };
                            string[] strArrays1 = str2.Split(chrArray);
                            for (int i = 0; i < (int)strArrays1.Length; i++)
                            {
                                string str3 = strArrays1[i];
                                if (str3.Length != 0)
                                {
                                    AppFile model = __AppFile.GetModel(str3);
                                    model._AutoID = Guid.NewGuid().ToString();
                                    model._UserName = HttpContext.Current.Session["EmployeeId"].ToString();
                                    model._CreateTime = DateTime.Now;
                                    model._UpdateTime = DateTime.Now;
                                    model.FolderID = string.Concat("myReceive_", stringCollection);
                                    __AppFile.Add(model);
                                }
                            }
                        }
                        AppMsg appMsg = new AppMsg(base.UserInfo)
                        {
                            Title = "转发文件",
                            MsgType = "转发文件",
                            MsgUrl = "",
                            RecIds = this.hidShareId.Value
                        };
                        string value1 = this.hidShareId.Value;
                        chrArray = new char[] { ',' };
                        appMsg.RecNames = EmployeeService.GetEmployeeNameList(value1.Split(chrArray));
                        appMsg.SendTime = new DateTime?(DateTime.Now);
                        appMsg.Sender = base.EmployeeName;
                        appMsg.Content = string.Format("转发文件提醒：【{0}】给您转发了{1}个文件，请到【我的文档】中查看。", base.EmployeeName, stringCollections.Count);
                        AppMsgService.SendMessage(appMsg);
                    }
                }
                else
                {
                    return;
                }
            }
            //base.ClientScript.RegisterStartupScript(typeof(DocFolderShare), "", "afterSave();", true);  //原版
            base.ClientScript.RegisterStartupScript(typeof(DocFolderSend), "", "afterSave();", true); //改进版
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }
    }
}