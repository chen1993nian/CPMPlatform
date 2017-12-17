using EIS.AppBase;
using EIS.DataAccess;
using EIS.WorkFlow.Engine;
using EIS.WorkFlow.Model;
using EIS.WorkFlow.XPDLParser;
using EIS.WorkFlow.XPDLParser.Elements;
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Data;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Text;
using System.Web.UI;
using System.Xml;


namespace EIS.Web.SysFolder.WorkFlow.UserControl
{
    public partial class InstanceImg : System.Web.UI.UserControl
    {
        public string AppRoot = "";
        public StringBuilder hotPoint = new StringBuilder();
        public Instance defModel = new Instance();
        private int int_0 = 32;
        private int int_1 = 32;
        public string InstanceId
        {
            get;
            set;
        }
        public InstanceImg()
        {
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            ExtendedAttribute extendedAttributeByName;
            int i;
            DataRow dataRow;
            this.AppRoot = EIS.AppBase.Utility.GetRootURI();
            this.defModel = (new DriverEngine()).GetInstanceById(this.InstanceId);
            (new XmlDocument()).LoadXml(this.defModel.XPDL);
            Package packageFromText = XpdlModel.GetPackageFromText(this.defModel.XPDL);
            WorkflowProcess workflowProcessById = packageFromText.GetWorkflowProcesses().GetWorkflowProcessById("1");
            foreach (Activity activity in workflowProcessById.GetActivities().GetActivities())
            {
                extendedAttributeByName = activity.GetExtendedAttributes().GetExtendedAttributeByName("Coordinates");
                int num = int.Parse(extendedAttributeByName.GetValueByName("xPos"));
                int num1 = int.Parse(extendedAttributeByName.GetValueByName("yPos"));
                Convert.ToInt32(activity.GetIcon());
                string id = activity.GetId();
                ActivityType nodeType = activity.GetNodeType();
                DataTable dataTable = new DataTable();
                string str = "";
                string str1 = "";
                if ((nodeType == ActivityType.Start || nodeType == ActivityType.Normal ? false : nodeType != ActivityType.Sign))
                {
                    continue;
                }
                str = string.Format("select * from T_E_WF_UserTask u where  u._isdel=0 and (u.TaskState='1' or u.TaskState='2') \r\n                        and u.TaskId in (select _AutoId from T_E_WF_Task t where t.InstanceId='{0}' and t.ActivityId='{1}')", this.InstanceId, id);
                dataTable = SysDatabase.ExecuteTable(str);
                StringBuilder stringBuilder = new StringBuilder();
                if (dataTable.Rows.Count <= 0)
                {
                    foreach (Performer performer in activity.GetPerformers().GetPerformers())
                    {
                        string description = performer.GetDescription();
                        string[] strArrays = description.Split(new char[] { '|' });
                        if (strArrays[1].Length <= 0)
                        {
                            continue;
                        }
                        stringBuilder.Append(string.Concat(strArrays[1], "，"));
                    }
                    str1 = string.Format("步骤编号：{0}&#13;步骤名称：{1}&#13;处&nbsp;理&nbsp;人：{2}", activity.GetCode(), activity.GetName(), stringBuilder.ToString());
                }
                else
                {
                    StringCollection stringCollections = new StringCollection();
                    DataRow[] dataRowArray = dataTable.Select("TaskState='1'", "DealTime");
                    for (i = 0; i < (int)dataRowArray.Length; i++)
                    {
                        dataRow = dataRowArray[i];
                        stringCollections.Add(dataRow["EmployeeName"].ToString());
                    }
                    StringCollection stringCollections1 = new StringCollection();
                    dataRowArray = dataTable.Select("TaskState='2'", "DealTime");
                    for (i = 0; i < (int)dataRowArray.Length; i++)
                    {
                        dataRow = dataRowArray[i];
                        stringCollections1.Add(string.Format("&#13;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{0}&nbsp;{1:yyyy-MM-dd HH:mm}", dataRow["EmployeeName"], dataRow["DealTime"]));
                    }
                    if (stringCollections1.Count > 0)
                    {
                        stringBuilder.AppendFormat("已处理：{0}", EIS.AppBase.Utility.GetJoinString(stringCollections1));
                    }
                    if (stringCollections.Count > 0)
                    {
                        if (stringCollections1.Count > 0)
                        {
                            stringBuilder.Append("&#13;");
                        }
                        stringBuilder.AppendFormat("处理中：{0}&#13;", EIS.AppBase.Utility.GetJoinString(stringCollections));
                    }
                    str1 = string.Format("步骤编号：{0}&#13;步骤名称：{1}&#13;{2}", activity.GetCode(), activity.GetName(), stringBuilder.ToString());
                }
                StringBuilder stringBuilder1 = this.hotPoint;
                object[] int0 = new object[] { num + 2, num1 + 2, num + this.int_0, num1 + this.int_1, str1, str1, activity.GetId() };
                stringBuilder1.AppendFormat("<area shape=\"rect\" coords=\"{0},{1},{2},{3}\" href=\"javascript:\"  actId=\"{6}\" alt=\"{4}\" title=\"{5}\"/>", int0);
            }
            foreach (Transition transition in workflowProcessById.GetTransitions().GetTransitions())
            {
                extendedAttributeByName = transition.GetExtendedAttributes().GetExtendedAttributeByName("Coordinates");
                transition.GetName();
                for (int j = 0; j < 5; j++)
                {
                    ExtendedAttribute extendedAttribute = transition.GetExtendedAttributes().GetExtendedAttributeByName(string.Concat("p", j.ToString()));
                    int num2 = j + 1;
                    ExtendedAttribute extendedAttributeByName1 = transition.GetExtendedAttributes().GetExtendedAttributeByName(string.Concat("p", num2.ToString()));
                    Point point = new Point(Convert.ToInt32(extendedAttribute.GetValueByName("xPos")), Convert.ToInt32(extendedAttribute.GetValueByName("yPos")));
                    Point point1 = new Point(Convert.ToInt32(extendedAttributeByName1.GetValueByName("xPos")), Convert.ToInt32(extendedAttributeByName1.GetValueByName("yPos")));
                }
            }
        }
    }
}