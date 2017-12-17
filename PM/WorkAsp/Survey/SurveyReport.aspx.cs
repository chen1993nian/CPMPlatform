using EIS.AppBase;
using EIS.DataAccess;
using System;
using System.Collections;
using System.Data;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace EIS.Web.WorkAsp.Survey
{
    public partial class SurveyReport : PageBase
    {
      
        private string string_0 = "";

        public string surveyTitle = "";

        public string surveyMemo = "";

        public StringBuilder listItems = new StringBuilder();

  

        protected void Page_Load(object sender, EventArgs e)
        {
            char[] chrArray;
            string[] strArrays;
            int i;
            string str;
            int length;
            double num;
            this.string_0 = base.GetParaValue("surveyId");
            if (!base.IsPostBack)
            {
                string str1 = string.Concat("select * from T_OA_Survey_Info where _AutoId='", this.string_0, "'");
                DataTable dataTable = SysDatabase.ExecuteTable(str1);
                if (dataTable.Rows.Count > 0)
                {
                    this.surveyTitle = dataTable.Rows[0]["SurTitle"].ToString();
                    this.surveyMemo = dataTable.Rows[0]["SurMemo"].ToString();
                    str1 = string.Format("select * from T_OA_Survey_Question where SurveyId='{0}' order by QOrder", this.string_0);
                    dataTable = SysDatabase.ExecuteTable(str1);
                    foreach (DataRow row in dataTable.Rows)
                    {
                        string str2 = row["_autoId"].ToString();
                        string str3 = row["qtype"].ToString();
                        string str4 = row["qselect"].ToString();
                        row["showother"].ToString();
                        string str5 = row["qorder"].ToString();
                        StringBuilder stringBuilder = this.listItems;
                        object[] item = new object[] { row["_AutoId"], str3, str5, row["QMustSel"].ToString() };
                        stringBuilder.AppendFormat("<div class='question' p='{0}|{1}|{2}|{3}'>", item);
                        this.listItems.AppendFormat("<h3><span>第{0}题：</span>{1}？<span>[{2}]</span></h3>", row["QOrder"], row["QTitle"], str3);
                        DataTable dataTable1 = SysDatabase.ExecuteTable(string.Format("select Answer,InSelect from T_OA_Survey_Reply where QuesID='{0}'", str2));
                        int count = dataTable1.Rows.Count;
                        if (str3 == "单选")
                        {
                            this.listItems.Append("<table border='1' class='list'>");
                            this.listItems.Append("<thead><tr><th>选项</th><th width=60>小计</th><th width=240>比例</th></tr></thead>");
                            this.listItems.Append("<tbody>");
                            chrArray = new char[] { '\r' };
                            strArrays = str4.Split(chrArray);
                            for (i = 0; i < (int)strArrays.Length; i++)
                            {
                                string str6 = strArrays[i];
                                chrArray = new char[] { '\n' };
                                str = str6.Trim(chrArray);
                                this.listItems.AppendFormat("<tr class='{0}'>", (i % 2 == 1 ? "erow" : ""));
                                this.listItems.AppendFormat("<td>{0}</td>", str);
                                length = (int)dataTable1.Select(string.Concat("Answer='", str, "'")).Length;
                                this.listItems.AppendFormat("<td>{0}</td>", length);
                                StringBuilder stringBuilder1 = this.listItems;
                                num = (double)length * 1 / (double)count;
                                stringBuilder1.AppendFormat("<td><div class='bar'><div class='precent' style='width:{0}; display: block;'><img width='149' height='13' alt='' src='../../img/vote/vote_cl_v2.png'></div></div>{0}</td>", num.ToString("p"));
                                this.listItems.Append("</tr>");
                            }
                            this.listItems.AppendFormat("</tbody>", new object[0]);
                            this.listItems.AppendFormat("<tfoot><tr><td>本题有效填写人次</td><td>{0}</td><td></td></tr></tfoot>", count);
                            this.listItems.AppendFormat("</table>", new object[0]);
                        }
                        else if (str3 == "多选")
                        {
                            this.listItems.Append("<table border='1' class='list'>");
                            this.listItems.Append("<thead><tr><th>选项</th><th width=60>小计</th><th width=240>比例</th></tr></thead>");
                            this.listItems.Append("<tbody>");
                            chrArray = new char[] { '\r' };
                            strArrays = str4.Split(chrArray);
                            for (i = 0; i < (int)strArrays.Length; i++)
                            {
                                string str7 = strArrays[i];
                                chrArray = new char[] { '\n' };
                                str = str7.Trim(chrArray);
                                this.listItems.AppendFormat("<tr class='{0}'>", (i % 2 == 1 ? "erow" : ""));
                                this.listItems.AppendFormat("<td>{0}</td>", str);
                                length = (int)dataTable1.Select(string.Concat("Answer like '%", str, ",%'")).Length;
                                this.listItems.AppendFormat("<td>{0}</td>", length);
                                StringBuilder stringBuilder2 = this.listItems;
                                num = (double)length * 1 / (double)count;
                                stringBuilder2.AppendFormat("<td><div class='bar'><div class='precent' style='width:{0}; display: block;'><img width='149' height='13' alt='' src='../../img/vote/vote_cl_v2.png'></div></div>{0}</td>", num.ToString("p"));
                                this.listItems.Append("</tr>");
                            }
                            this.listItems.AppendFormat("</tbody>", new object[0]);
                            this.listItems.AppendFormat("<tfoot><tr><td>本题有效填写人次</td><td>{0}</td><td></td></tr></tfoot>", count);
                            this.listItems.AppendFormat("</table>", new object[0]);
                        }
                        else if (str3 == "输入")
                        {
                            this.listItems.AppendFormat("<div class='item'><a href='javascript:' class='detailLink' qid='{0}'>【查看本题答案详细情况】</a></div>", str2);
                        }
                        this.listItems.AppendFormat("</div>", new object[0]);
                    }
                }
            }
        }
    }
}