using EIS.WorkFlow.XPDLParser.Elements;
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Data;
using System.Text;

namespace EIS.AppModel.Workflow
{
	public class NoUserException : Exception
	{
		public Activity CurNode;

		public StringCollection DefineList;

		public DataRow AppData;

		public override string Message
		{
			get
			{
				string str = string.Format("【{0}】步骤（ID:{1}）找不到处理人", this.CurNode.GetName(), this.CurNode.GetId());
				return str;
			}
		}

		public NoUserException()
		{
			this.DefineList = new StringCollection();
		}

		public NoUserException(Activity node, StringCollection defs, DataRow appData)
		{
			this.CurNode = node;
			this.DefineList = defs;
			this.AppData = appData;
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("<div class='ErrorPanel'>");
			stringBuilder.AppendFormat("<div><b>错误信息：</b>【{0}】步骤（ID:{1}）找不到处理人</div>", this.CurNode.GetName(), this.CurNode.GetId());
			stringBuilder.AppendFormat("<div class='Performer'><b>处理人定义：</b>", new object[0]);
			foreach (string defineList in this.DefineList)
			{
				stringBuilder.AppendFormat("<div>{0}</div>", defineList);
			}
			foreach (Performer performer in this.CurNode.GetPerformers().GetPerformers())
			{
				stringBuilder.AppendFormat("<div>{0}</div>", performer.GetDescription());
			}
			stringBuilder.Append("</div>");
			if (this.AppData != null)
			{
				stringBuilder.Append("<div><b>业务数据：</b></div>");
				stringBuilder.Append("<table class='appTbl'><thead><tr><td>字段名</td><td>字段值</td></tr></thead>");
				stringBuilder.Append("<tbody>");
				foreach (DataColumn column in this.AppData.Table.Columns)
				{
					stringBuilder.AppendFormat("<tr><td>{0}</td><td>{1}</td></tr>", column.ColumnName, this.AppData[column.ColumnName]);
				}
				stringBuilder.Append("</tbody>");
				stringBuilder.Append("</table>");
			}
			else
			{
				stringBuilder.Append("<div><b>业务数据：空</b></div>");
			}
			stringBuilder.Append("</div>");
			return stringBuilder.ToString();
		}
	}
}