using AjaxPro;
using EIS.AppBase;
using EIS.DataAccess;
using EIS.DataModel.Access;
using EIS.DataModel.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Web.UI.HtmlControls;

namespace EIS.Web.SysFolder.AppFrame
{
	public partial class AppConditionDef : PageBase
	{
		public StringBuilder fieldlist1 = new StringBuilder();

		public StringBuilder fieldlist2 = new StringBuilder();

		public string sindex = "";

		public string tblName = "";

		

		protected void Page_Load(object sender, EventArgs e)
		{
			FieldInfoExt fieldInfoExt = null;
			FieldInfo fieldInfo = null;
			AjaxPro.Utility.RegisterTypeForAjax(typeof(AppConditionDef));
			StringBuilder stringBuilder = new StringBuilder();
			this.tblName = base.GetParaValue("tblname");
			this.sindex = base.GetParaValue("sindex");
			_FieldInfo __FieldInfo = new _FieldInfo();
			_FieldInfoExt __FieldInfoExt = new _FieldInfoExt();
			if (this.sindex == "")
			{
				foreach (FieldInfo fieldInfoA in __FieldInfo.GetTableFields(this.tblName).FindAll((FieldInfo fieldInfo_0) => fieldInfo_0.QueryDisp == 0))
				{
					this.fieldlist1.AppendFormat("<li class='ui-state-default' id='{0}'>{1}</li>\n", fieldInfoA._AutoID, fieldInfoA.FieldNameCn);
				}
				foreach (FieldInfo modelQueryDisp in __FieldInfo.GetModelQueryDisp(this.tblName))
				{
					this.fieldlist2.AppendFormat("<li class='ui-state-highlight' id='{0}'>{1}</li>\n", modelQueryDisp._AutoID, modelQueryDisp.FieldNameCn);
				}
			}
			else
			{
				List<FieldInfoExt> tableFields = __FieldInfoExt.GetTableFields(this.tblName, int.Parse(this.sindex));
				foreach (FieldInfoExt fieldInfoExtA in tableFields.FindAll((FieldInfoExt fieldInfoExt_0) => fieldInfoExt_0.QueryDisp == 0))
				{
					this.fieldlist1.AppendFormat("<li class='ui-state-default' id='{0}'>{1}</li>\n", fieldInfoExtA._AutoID, fieldInfoExtA.FieldNameCn);
				}
				foreach (FieldInfoExt modelQueryDisp1 in __FieldInfoExt.GetModelQueryDisp(this.tblName, this.sindex))
				{
					this.fieldlist2.AppendFormat("<li class='ui-state-highlight' id='{0}'>{1}</li>\n", modelQueryDisp1._AutoID, modelQueryDisp1.FieldNameCn);
				}
			}
		}

        [AjaxMethod(HttpSessionStateRequirement.Read)]    // JustDecompile was unable to locate the assembly where attribute parameters types are defined. Generating parameters values is impossible.
		public void saveQuery(ArrayList arrayList_0, string tblname, string sindex)
		{
			object[] objArray;
			int i;
			string str = "";
			foreach (string arrayList0 in arrayList_0)
			{
				str = string.Concat(str, ",'", arrayList0, "'");
			}
			if (str.Length != 0)
			{
				StringBuilder stringBuilder = new StringBuilder();
				string str1 = "T_E_Sys_FieldInfo";
				if (sindex != "")
				{
					objArray = new object[] { tblname, str.Substring(1), str1, sindex };
					stringBuilder.AppendFormat("update T_E_Sys_FieldInfoExt set QueryDisp=0 ,QueryOrder=0 where tablename='{0}' and styleindex={3} and _Autoid not in({1});", objArray);
					for (i = 0; i < arrayList_0.Count; i++)
					{
						objArray = new object[] { i, tblname, arrayList_0[i], str1 };
						stringBuilder.AppendFormat("update T_E_Sys_FieldInfoExt set QueryDisp=1 ,QueryOrder={0} where tablename='{1}' and _Autoid='{2}';", objArray);
					}
				}
				else
				{
					stringBuilder.AppendFormat("update T_E_Sys_FieldInfo set QueryDisp=0 ,QueryOrder=0 where tablename='{0}' and _Autoid not in({1});", tblname, str.Substring(1), str1);
					for (i = 0; i < arrayList_0.Count; i++)
					{
						objArray = new object[] { i, tblname, arrayList_0[i], str1 };
						stringBuilder.AppendFormat("update T_E_Sys_FieldInfo set QueryDisp=1 ,QueryOrder={0} where tablename='{1}' and _Autoid='{2}';", objArray);
					}
				}
				if (stringBuilder.Length > 0)
				{
					SysDatabase.ExecuteNonQuery(stringBuilder.ToString());
				}
			}
		}
	}
}