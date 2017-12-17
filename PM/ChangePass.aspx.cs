using EIS.AppBase;
using EIS.Permission;
using System;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace EIS.Web
{
    public partial class ChangePass : PageBase
    {


        protected void Button1_Click(object sender, EventArgs e)
        {
            string userName = base.UserName;
            string str = this.txtOldPass.Text.Trim();
            if (str != this.newPass.Text.Trim())
            {
                string str1 = "";
                switch (EIS.Permission.Utility.LoginChk(userName, str))
                {
                    case LoginInfoType.Allowed:
                        {
                            str1 = "";
                            break;
                        }
                    case LoginInfoType.NotExist:
                        {
                            str1 = "用户不存在";
                            break;
                        }
                    case LoginInfoType.WrongPwd:
                        {
                            str1 = "密码不正确";
                            break;
                        }
                    case LoginInfoType.IsLocked:
                        {
                            str1 = "帐户被锁定";
                            break;
                        }
                }
                if (str1 != "")
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), "", string.Concat("alert('", str1, "');"), true);
                }
                else
                {
                    EIS.Permission.Utility.ChangePass(base.EmployeeID, this.newPass.Text.Trim());
                    if (base.GetParaValue("firstLogin") == "")
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), "", "alert('密码修改成功!');", true);
                    }
                    else
                    {
                        base.Response.Redirect(FormsAuthentication.DefaultUrl);
                    }
                }
            }
            else
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), "", "alert('新密码和旧密码不得相同!');", true);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }
    }
}