using AjaxPro;
using EIS.AppBase;
using EIS.AppCommon;
using EIS.DataModel.Model;
using EIS.DataModel.Service;
using EIS.Permission.Service;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Web.UI.HtmlControls;

namespace EIS.Web.WorkAsp.Calendar
{
	public partial class QueryLeft : PageBase
	{
		

		public string treedata = "";

		public string treeId = "";

		private List<FolderInfo> list_0 = new List<FolderInfo>();

	

		private string[] method_0(StringCollection stringCollection_0)
		{
			StringBuilder stringBuilder = new StringBuilder();
			StringBuilder length = new StringBuilder();
			foreach (string stringCollection0 in stringCollection_0)
			{
				string[] strArrays = stringCollection0.Split(new char[] { '|' });
				if (!EmployeeService.CheckedValid(strArrays[0]))
				{
					continue;
				}
				stringBuilder.AppendFormat("{0},", strArrays[0]);
				length.AppendFormat("{0},", strArrays[1]);
			}
			if (stringBuilder.Length > 1)
			{
				stringBuilder.Length = stringBuilder.Length - 1;
				length.Length = length.Length - 1;
			}
			string[] str = new string[] { stringBuilder.ToString(), length.ToString() };
			return str;
		}

		[AjaxMethod]    // JustDecompile was unable to locate the assembly where attribute parameters types are defined. Generating parameters values is impossible.
		public string NewFolder(string folderName, string folderPId)
		{
			return "";
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			int i;
			zTreeNode _zTreeNode;
			AjaxPro.Utility.RegisterTypeForAjax(typeof(QueryLeft));
			zTreeNode _zTreeNode1 = new zTreeNode();
			if (this.treeId == "")
			{
				_zTreeNode1.id = "0";
				_zTreeNode1.name = "联系人分组";
				_zTreeNode1.icon = "../../img/doc/home.png";
				_zTreeNode1.@value = "0";
				_zTreeNode1.open = true;
			}
			StringCollection myLeader2 = EmployeeRelationService.GetMyLeader2(base.EmployeeID);
			zTreeNode _zTreeNode2 = new zTreeNode()
			{
				name = "我的领导",
				id = "myleader",
				@value = "0",
				icon = "../../img/doc/folder_user.png",
				open = true
			};
			_zTreeNode1.Add(_zTreeNode2);
			string[] strArrays = this.method_0(myLeader2);
			string str = strArrays[0];
			char[] chrArray = new char[] { ',' };
			string[] strArrays1 = str.Split(chrArray);
			string str1 = strArrays[1];
			chrArray = new char[] { ',' };
			string[] strArrays2 = str1.Split(chrArray);
			for (i = 0; i < (int)strArrays1.Length; i++)
			{
				if (strArrays1[i].Length != 0)
				{
					_zTreeNode = new zTreeNode()
					{
						name = strArrays2[i],
						id = strArrays1[i],
						@value = "1"
					};
					_zTreeNode2.Add(_zTreeNode);
				}
			}
			myLeader2 = EmployeeRelationService.GetMyUnderling(base.EmployeeID, null);
			zTreeNode _zTreeNode3 = new zTreeNode()
			{
				name = "我的下级",
				id = "myjunior",
				@value = "0",
				icon = "../../img/doc/folder_user.png",
				open = true
			};
			_zTreeNode1.Add(_zTreeNode3);
			strArrays = this.method_0(myLeader2);
			string str2 = strArrays[0];
			chrArray = new char[] { ',' };
			strArrays1 = str2.Split(chrArray);
			string str3 = strArrays[1];
			chrArray = new char[] { ',' };
			strArrays2 = str3.Split(chrArray);
			for (i = 0; i < (int)strArrays1.Length; i++)
			{
				if (strArrays1[i].Length != 0)
				{
					_zTreeNode = new zTreeNode()
					{
						name = strArrays2[i],
						id = strArrays1[i],
						@value = "1"
					};
					_zTreeNode3.Add(_zTreeNode);
				}
			}
			this.treedata = _zTreeNode1.ToJsonString(true);
		}
	}
}