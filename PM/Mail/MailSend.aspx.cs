using AjaxPro;
using EIS.AppBase;
using EIS.AppMail.BLL;
using EIS.AppMail.DAL;
using System;
using System.Collections;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace EIS.Web.Mail
{
	public partial class MailSend : PageBase
	{
		

		public string @orderby = "";

		public string folderId = "";

		

		[AjaxMethod]    // JustDecompile was unable to locate the assembly where attribute parameters types are defined. Generating parameters values is impossible.
		public void DeleteMail(string[] recIdList)
		{
			string[] strArrays = recIdList;
			for (int i = 0; i < (int)strArrays.Length; i++)
			{
				string str = strArrays[i];
				(new _MailMessage()).Delete(str, 1);
			}
		}

		public void GetMailList()
		{
			StringBuilder stringBuilder = new StringBuilder();
			StringBuilder stringBuilder1 = new StringBuilder();
			StringBuilder stringBuilder2 = new StringBuilder();
			StringBuilder stringBuilder3 = new StringBuilder();
			MailService mailService = new MailService();
			DataTable receiveMailData = mailService.GetReceiveMailData(base.EmployeeID, this.folderId);
			int num = 0;
			int num1 = 0;
			int num2 = 0;
			foreach (DataRow row in receiveMailData.Rows)
			{
				DateTime dateTime = Convert.ToDateTime(row["CreateTime"]);
				if (dateTime.Date == DateTime.Today.Date)
				{
					stringBuilder1.Append(this.method_0(row));
					num++;
				}
				else if ((DateTime.Today.Date - dateTime.Date).Days != 1)
				{
					stringBuilder3.Append(this.method_0(row));
					num2++;
				}
				else
				{
					stringBuilder2.Append(this.method_0(row));
					num1++;
				}
			}
			if (stringBuilder1.Length > 0)
			{
				stringBuilder1.Insert(0, "<div class=toarea>");
				stringBuilder1.Append("</div>");
				stringBuilder1.Insert(0, string.Format("\r\n                    <a class=\"bd talk cur_default\"><input style=\"POSITION: absolute; MARGIN: 0px 0px 0px -33px\" id=\"showtoday\" class=one onclick=getTop().CA(this) type=\"checkbox\"> \r\n                    <label hideFocus for=\"showtoday\"><B>今天</B> (<span hideFocus class=\"newfd underline\" title=选中该组内所有邮件>{0} 封</span></label>) </a>", num));
				stringBuilder.Append(stringBuilder1.ToString());
			}
			if (stringBuilder2.Length > 0)
			{
				stringBuilder2.Insert(0, "<div class=toarea>");
				stringBuilder2.Append("</div>");
				stringBuilder2.Insert(0, string.Format("\r\n                    <a class=\"bd talk cur_default\"><input style=\"POSITION: absolute; MARGIN: 0px 0px 0px -33px\" id=\"showtoday\" class=one onclick=getTop().CA(this) type=\"checkbox\"> \r\n                    <label hideFocus for=\"showtoday\"><B>昨天</B> (<span hideFocus class=\"newfd underline\" title=选中该组内所有邮件>{0} 封</span></label>) </a>", num1));
				stringBuilder.Append(stringBuilder2.ToString());
			}
			if (stringBuilder3.Length > 0)
			{
				stringBuilder3.Insert(0, "<div class=toarea>");
				stringBuilder3.Append("</div>");
				stringBuilder3.Insert(0, string.Format("\r\n                    <a class=\"bd talk cur_default\"><input style=\"POSITION: absolute; MARGIN: 0px 0px 0px -33px\" id=\"showtoday\" class=one onclick=getTop().CA(this) type=\"checkbox\"> \r\n                    <label hideFocus for=\"showtoday\"><B>更早</B> (<span hideFocus class=\"newfd underline\" title=选中该组内所有邮件>{0} 封</span></label>) </a>", num2));
				stringBuilder.Append(stringBuilder3.ToString());
			}
			base.Response.Write(stringBuilder.ToString());
		}

		private string method_0(DataRow dataRow_0)
		{
			StringBuilder stringBuilder = new StringBuilder();
			StringBuilder stringBuilder1 = stringBuilder;
			object[] item = new object[] { (dataRow_0["SenderName"] == DBNull.Value ? dataRow_0["Sender"] : dataRow_0["SenderName"]), dataRow_0["Subject"], null, null, null, null };
			DateTime dateTime = Convert.ToDateTime(dataRow_0["CreateTime"]);
			item[2] = dateTime.ToString("MM月dd日");
			item[3] = this.method_1(dataRow_0["Body"].ToString());
			item[4] = base.Server.UrlEncode(dataRow_0["MailId"].ToString());
			item[5] = this.folderId;
			stringBuilder1.AppendFormat("\r\n<table  class=\"i M\" cellSpacing=0><tbody><tr>\r\n    <td class=cx><input type='checkbox' value=''></td>\r\n    <td class=ci>\r\n        <div class=\"ciz \">&nbsp;</div>\r\n        <div class=\"cir Rr\" title=\"\">&nbsp;</div>\r\n        <div class=cij>&nbsp;</div>\r\n    </td>\r\n    <td class=l onclick=\"\">\r\n        <table class=i cellSpacing=0>\r\n        <tr>\r\n            <td class=\"tl tf\">{0}</td>\r\n            <td class='fg_n '><div></div></td>\r\n            <td class=\"gt tf \">\r\n                <div><U class='black ' tabIndex=0><a href='MailRead.aspx?MailId={4}&folderId={5}' target='_self'>{1}</a></U>&nbsp;-&nbsp;<b class='no'>{3}</b>&nbsp;</div>\r\n                <div class=TagDiv></div>\r\n            </td>\r\n            <td class=\"dt\"><div>{2}</div></td>\r\n        </tr>\r\n        </table>\r\n    </td>\r\n</tr></tbody></table>\r\n                ", item);
			return stringBuilder.ToString();
		}

		private string method_1(string string_0)
		{
			string_0 = Regex.Replace(string_0, "[<].*?[>]|&nbsp;|\\s|\\n|\\r", "");
			return string_0;
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			AjaxPro.Utility.RegisterTypeForAjax(typeof(MailSend));
			this.folderId = base.GetParaValue("folderId");
		}

		[AjaxMethod]    // JustDecompile was unable to locate the assembly where attribute parameters types are defined. Generating parameters values is impossible.
		public void RemoveMail(string[] recIdList)
		{
			string[] strArrays = recIdList;
			for (int i = 0; i < (int)strArrays.Length; i++)
			{
				string str = strArrays[i];
				(new _MailMessage()).Delete(str, 2);
			}
		}
	}
}