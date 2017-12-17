using EIS.AppBase;
using EIS.AppModel.Service;
using EIS.DataModel.Access;
using EIS.DataModel.Model;
using EIS.DataModel.Service;
using EIS.Permission.Service;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.HtmlControls;

namespace EIS.Web.WorkAsp.Msg
{
    public partial class MsgRead : PageBase
    {
        public string msgId = "";

        public string total = "";

        public string msgUrl = "";

        public AppMsg model = new AppMsg();

        public int Readed = 0;

        public int Unread = 0;

        public string ReadedList = "";

        public string UnReadList = "";

        public string fileList = "";

  
        

        private string method_0(string appName, string appId)
        {
            StringBuilder stringBuilder = new StringBuilder();
            IList<AppFile> files = (new FileService()).GetFiles(appName, appId);
            int num = 1;
            foreach (AppFile file in files)
            {
                stringBuilder.AppendFormat("<div class='fileItem'>", new object[0]);
                object[] factFileName = new object[5];
                int num1 = num;
                num = num1 + 1;
                factFileName[0] = num1;
                factFileName[1] = file.FactFileName;
                factFileName[2] = file.FileSize / 1024;
                factFileName[3] = base.CryptPara(string.Concat("fileId=", file._AutoID));
                factFileName[4] = EmployeeService.GetEmployeeName(file._UserName);
                stringBuilder.AppendFormat("{0}、<a href='../../SysFolder/Common/FileDown.aspx?para={3}' _target='_blank'>{1}</a>（{4}&nbsp;{2}K）", factFileName);
                stringBuilder.AppendFormat("</div>", new object[0]);
            }
            return stringBuilder.ToString();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            AppMsgRec appMsgRec = null;
            this.msgId = base.GetParaValue("msgId");
            this.model = (new _AppMsg()).GetModel(this.msgId);
            if (this.model == null)
            {
                throw new Exception("找不到对应的消息");
            }
            AppMsgRecService.FlagRead(this.msgId, base.EmployeeID);
            if (this.model.MsgUrl.ToLower().StartsWith("sysfolder"))
            {
                this.msgUrl = string.Concat("<a class='linkbtn' href='", base.AppPath, this.model.MsgUrl, "' target='_blank'>[查看详情]</a>");
            }
            else if (this.model.MsgUrl != "")
            {
                this.msgUrl = string.Concat("<a class='linkbtn' href='", this.model.MsgUrl, "' target='_blank'>[查看详情]</a>");
            }
            int unReadMsgCount = AppMsgService.GetUnReadMsgCount(base.EmployeeID);
            EmployeeService.UpdateLastMsgCount(base.EmployeeID, unReadMsgCount);
            IList<AppMsgRec> msgRecRead = AppMsgService.GetMsgRecRead(this.msgId, 1);
            this.Readed = msgRecRead.Count;
            IList<AppMsgRec> appMsgRecs = AppMsgService.GetMsgRecRead(this.msgId, 0);
            this.Unread = appMsgRecs.Count;
            StringBuilder stringBuilder = new StringBuilder();
            foreach (AppMsgRec appMsgReca in msgRecRead)
            {
                stringBuilder.AppendFormat("{0}，", EmployeeService.GetEmployeeName(appMsgReca.RecId));
            }
            if (this.Readed > 0)
            {
                stringBuilder.Length = stringBuilder.Length - 1;
                this.ReadedList = stringBuilder.ToString();
            }
            stringBuilder.Length = 0;
            foreach (AppMsgRec appMsgRec1 in appMsgRecs)
            {
                stringBuilder.AppendFormat("{0}，", EmployeeService.GetEmployeeName(appMsgRec1.RecId));
            }
            if (this.Unread > 0)
            {
                stringBuilder.Length = stringBuilder.Length - 1;
                this.UnReadList = stringBuilder.ToString();
            }
            if (this.Readed + this.Unread > 5)
            {
                this.total = string.Format("{0}人已读，{1}人未读", this.Readed, this.Unread);
            }
            this.fileList = this.method_0("T_E_App_MsgInfo", this.msgId);
        }
    }
}