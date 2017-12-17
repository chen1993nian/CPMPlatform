using EIS.AppBase;
using EIS.Permission;
using EIS.Permission.Service;
using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace EIS.Studio
{
    public partial class ChangePass : AdminPageBase
    {
       

        protected void Button1_Click(object sender, EventArgs e)
        {
            string userName = base.UserName;
            string text = this.txtOldPass.Text;
            string str = "";
            switch (UserService.LoginChk(userName, text))
            {
                case LoginInfoType.Allowed:
                    {
                        str = "";
                        break;
                    }
                case LoginInfoType.NotExist:
                    {
                        str = "用户不存在";
                        break;
                    }
                case LoginInfoType.WrongPwd:
                    {
                        str = "密码不正确";
                        break;
                    }
                case LoginInfoType.IsLocked:
                    {
                        str = "帐户被锁定";
                        break;
                    }
            }
            if (str != "")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), "", string.Concat("<script language=javascript>alert('", str, "');</script>"));
            }
            else
            {
                UserService.ChangePass(userName, this.newPass.Text.Trim());
                base.ClientScript.RegisterStartupScript(base.GetType(), "", "<script language=javascript>success();</script>");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }
    }
}