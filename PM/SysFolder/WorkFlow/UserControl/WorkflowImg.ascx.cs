using EIS.WorkFlow.Model;
using EIS.WorkFlow.Service;
using EIS.WorkFlow.XPDLParser;
using EIS.WorkFlow.XPDLParser.Elements;
using System;
using System.Collections;
using System.Drawing;
using System.Text;
using System.Web.UI;
using System.Xml;

namespace EIS.Web.SysFolder.WorkFlow.UserControl
{
    public partial class WorkflowImg : System.Web.UI.UserControl
    {
        public string workflowId = "";

        public string WorkflowName = "";

        public string AppRoot = "";

        public StringBuilder hotPoint = new StringBuilder();

        public Define defModel = new Define();

        private int int_0 = 32;

        private int int_1 = 32;

        public WorkflowImg()
        {
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            ExtendedAttribute extendedAttributeByName;
            this.AppRoot = EIS.AppBase.Utility.GetRootURI();
			this.defModel = DefineService.GetWorkflowDefineModelById(this.workflowId);
			this.WorkflowName = this.defModel.WorkflowName;
			(new XmlDocument()).LoadXml(this.defModel.XPDL);
			Package packageFromText = XpdlModel.GetPackageFromText(this.defModel.XPDL);
			WorkflowProcess workflowProcessById = packageFromText.GetWorkflowProcesses().GetWorkflowProcessById("1");
			foreach (Activity activity in workflowProcessById.GetActivities().GetActivities())
			{
				extendedAttributeByName = activity.GetExtendedAttributes().GetExtendedAttributeByName("Coordinates");
				int num = int.Parse(extendedAttributeByName.GetValueByName("xPos"));
				int num1 = int.Parse(extendedAttributeByName.GetValueByName("yPos"));
				Convert.ToInt32(activity.GetIcon());
				Performers performers = activity.GetPerformers();
				StringBuilder stringBuilder = new StringBuilder();
				foreach (Performer performer in performers.GetPerformers())
				{
					string description = performer.GetDescription();
					string[] strArrays = description.Split(new char[] { '|' });
					if (strArrays[1].Length <= 0)
					{
						continue;
					}
					stringBuilder.Append(string.Concat(strArrays[1], "，"));
				}
				string str = string.Format("步骤编号：{0}&#13;步骤名称：{1}&#13;处&nbsp;理&nbsp;人：{2}", activity.GetCode(), activity.GetName(), stringBuilder.ToString());
				StringBuilder stringBuilder1 = this.hotPoint;
				object[] int0 = new object[] { num + 2, num1 + 2, num + this.int_0, num1 + this.int_1, str, str };
				stringBuilder1.AppendFormat("<area shape=\"rect\" coords=\"{0},{1},{2},{3}\" href=\"#\" target =\"_blank\" alt=\"{4}\" title=\"{5}\"/>", int0);
			}
            foreach (Transition transition in workflowProcessById.GetTransitions().GetTransitions())
            {
                extendedAttributeByName = transition.GetExtendedAttributes().GetExtendedAttributeByName("Coordinates");
                transition.GetName();
                for (int i = 0; i < 5; i++)
                {
                    ExtendedAttribute extendedAttribute = transition.GetExtendedAttributes().GetExtendedAttributeByName(string.Concat("p", i.ToString()));
                    int num2 = i + 1;
                    ExtendedAttribute extendedAttributeByName1 = transition.GetExtendedAttributes().GetExtendedAttributeByName(string.Concat("p", num2.ToString()));
                    Point point = new Point(Convert.ToInt32(extendedAttribute.GetValueByName("xPos")), Convert.ToInt32(extendedAttribute.GetValueByName("yPos")));
                    Point point1 = new Point(Convert.ToInt32(extendedAttributeByName1.GetValueByName("xPos")), Convert.ToInt32(extendedAttributeByName1.GetValueByName("yPos")));
                }
            }
        }
    }
}