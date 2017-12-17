using EIS.AppBase;
using EIS.AppModel.Service;
using EIS.DataModel.Access;
using EIS.DataModel.Model;
using EIS.DataModel.Service;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace EIS.Web.WorkAsp.Msg
{
    public partial class MsgSend : PageBase
    {
       
        public string appId
        {
            get
            {
                return this.ViewState["appId"].ToString();
            }
            set
            {
                this.ViewState["appId"] = value;
            }
        }

   
        protected void Button1_Click(object sender, EventArgs e)
        {
            AppMsg appMsg = new AppMsg(base.UserInfo)
            {
                _AutoID = this.appId,
                Title = "",
                MsgType = "",
                MsgUrl = "",
                RecIds = this.txtRecIds.Value,
                RecNames = this.txtRecNames.Text,
                SendTime = new DateTime?(DateTime.Now),
                Sender = base.EmployeeName,
                Content = this.txtContent.Text
            };
            try
            {
                AppMsgService.SendMessage(appMsg);
                base.ClientScript.RegisterStartupScript(base.GetType(), "", "afterSend();", true);
            }
            catch (Exception exception)
            {
                base.ClientScript.RegisterClientScriptBlock(base.GetType(), "", "sendError();", true);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                this.appId = Guid.NewGuid().ToString();
                string paraValue = base.GetParaValue("act");
                if (!string.IsNullOrEmpty(paraValue))
                {
                    string str = base.GetParaValue("msgId");
                    AppMsg model = (new _AppMsg()).GetModel(str);
                    if (paraValue == "1")
                    {
                        this.txtRecIds.Value = model._UserName;
                        this.txtRecNames.Text = model.Sender;
                        this.txtContent.Text = string.Concat("\r\n\r\n---------消息原文---------\r\n", model.Content);
                    }
                    else if (paraValue == "2")
                    {
                        this.txtContent.Text = model.Content;
                        FileService fileService = new FileService();
                        _AppFile __AppFile = new _AppFile();
                        foreach (AppFile file in fileService.GetFiles("T_E_App_MsgInfo", model._AutoID))
                        {
                            file.AppId = this.appId;
                            file._AutoID = Guid.NewGuid().ToString();
                            file.BasePath = AppSettings.Instance.AppFileBaseCode;
                            __AppFile.Add(file);
                        }
                    }
                }
            }
        }
    }
}