using EIS.AppBase;
using EIS.DataModel.Access;
using EIS.DataModel.Model;
using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
namespace EIS.Studio.SysFolder.DefFrame
{
    public partial class DefCatalogEdit : AdminPageBase
    {
        

        private _Catalog _Catalog_0 = new _Catalog();

        public string nodeWbs = "";

        protected void Button1_Click(object sender, EventArgs e)
        {
            this.openflag.Value = "add";
            this.Button1.Enabled = false;
            this.TextBox1.Text = this._Catalog_0.GetNewCode(this.nodeWbs);
            this.TextBox2.Text = "";
            TextBox textBox3 = this.TextBox3;
            int newOrd = this._Catalog_0.GetNewOrd(this.nodeWbs);
            textBox3.Text = newOrd.ToString();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Catalog catalog;
            string[] catCode;
            if (this.openflag.Value != "change")
            {
                catalog = new Catalog(base.UserInfo)
                {
                    CatCode = this.TextBox1.Text,
                    CatName = this.TextBox2.Text,
                    CatOdr = int.Parse(this.TextBox3.Text),
                    PCatCode = this.nodeWbs
                };
                this._Catalog_0.Add(catalog);
                this.openflag.Value = "change";
                this.Button1.Enabled = true;
                Catalog model = this._Catalog_0.GetModel(this.nodeWbs);
                if (model != null)
                {
                    this.TextBox1.Text = model.CatCode;
                    this.TextBox2.Text = model.CatName;
                    this.TextBox3.Text = model.CatOdr.ToString();
                }
                ClientScriptManager clientScript = base.ClientScript;
                Type type = base.GetType();
                catCode = new string[] { "<script language=javascript>parent.frames['left'].addNode('", catalog.CatCode, "','", catalog.CatName, "');</script>" };
                clientScript.RegisterClientScriptBlock(type, "", string.Concat(catCode));
            }
            else
            {
                catalog = this._Catalog_0.GetModel(this.nodeWbs);
                catalog.CatName = this.TextBox2.Text;
                catalog.CatOdr = int.Parse(this.TextBox3.Text);
                this._Catalog_0.Update(catalog);
                ClientScriptManager clientScriptManager = base.ClientScript;
                Type type1 = base.GetType();
                catCode = new string[] { "<script language=javascript>parent.frames['left'].changeNode('", this.nodeWbs, "','", this.TextBox2.Text, "');</script>" };
                clientScriptManager.RegisterClientScriptBlock(type1, "", string.Concat(catCode));
            }
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            (new _Catalog()).DeleteByCode(this.nodeWbs);
            base.ClientScript.RegisterClientScriptBlock(base.GetType(), "", "<script type='text/javascript'>parent.frames['left'].delNode();</script>");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.nodeWbs = base.GetParaValue("nodewbs");
            if (!base.IsPostBack)
            {
                Catalog model = this._Catalog_0.GetModel(this.nodeWbs);
                if (model != null)
                {
                    this.TextBox1.Text = model.CatCode;
                    this.TextBox2.Text = model.CatName;
                    this.TextBox3.Text = model.CatOdr.ToString();
                }
            }
        }
    }
}