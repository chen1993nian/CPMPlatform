using EIS.AppBase;
using EIS.DataModel.Access;
using EIS.DataModel.Model;
using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace EIS.Studio.SysFolder.DefFrame
{
    public partial class DefDictEdit : AdminPageBase
    {
        public string editId = "";
        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            _Dict __Dict;
            Dict dict;
            if (string.IsNullOrEmpty(this.editId))
            {
                __Dict = new _Dict();
                dict = new Dict()
                {
                    _AutoID = Guid.NewGuid().ToString(),
                    _OrgCode = base.OrgCode,
                    _UserName = base.EmployeeID,
                    _CreateTime = DateTime.Now,
                    _UpdateTime = DateTime.Now,
                    _IsDel = 0,
                    DictName = this.tb_tablename.Text,
                    DictCat = base.GetParaValue("cat")
                };
                __Dict.Add(dict);
            }
            else
            {
                __Dict = new _Dict();
                dict = __Dict.GetModel(this.editId);
                dict._UpdateTime = DateTime.Now;
                dict.DictName = this.tb_tablename.Text;
                __Dict.Update(dict);
            }
            base.Response.Write("<script>alert('保存成功！');window.opener.app_query();window.close();</script>");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.editId = base.GetParaValue("editid");
            if (!base.IsPostBack && !string.IsNullOrEmpty(this.editId))
            {
                Dict model = (new _Dict()).GetModel(this.editId);
                this.tb_tablename.Text = model.DictName;
            }
        }
    }
}