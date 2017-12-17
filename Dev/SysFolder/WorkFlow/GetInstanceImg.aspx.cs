using EIS.AppBase;
using EIS.WorkFlow.Access;
using EIS.WorkFlow.Engine;
using EIS.WorkFlow.Model;
using EIS.WorkFlow.Service;
using EIS.WorkFlow.XPDLParser;
using EIS.WorkFlow.XPDLParser.Elements;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Xml;
using NLog;
namespace EIS.WebBase.SysFolder.WorkFlow
{
    public partial class GetInstanceImg : PageBase
    {
        private int int_0 = 0;

        private int int_1 = 0;

        public string InstanceId = "";

        public string WorkflowName = "";

        public string Creator = "";

        private int int_2 = 32;

        private int int_3 = 32;

        private string string_0 = "#4677BF";

        private int int_4 = 1;

        public Instance defModel = new Instance();


        Logger fileLogger = LogManager.GetCurrentClassLogger();
        protected void Page_Load(object sender, EventArgs e)
        {

            try{

            int i;
            ExtendedAttribute extendedAttributeByName;
            int num;
            ExtendedAttribute extendedAttribute;
            Point point;
            Point point1;
            ExtendedAttribute extendedAttributeByName1;
            Point point2;
            Point point3;
            int num1;
            TransitionRestriction item;
            this.InstanceId = base.GetParaValue("InstanceId");

            string[] strArrays = new string[] { "node_start.png", "node_stop.png", "node_normal.png", "node_sign.png", "node_mail.png", "node_dll.png", "node_and.png", "node_xor.png", "node_block.png", "node_subWorkflow.png", "node_auto.png" };
            string[] strArrays1 = strArrays;
            strArrays = new string[] { "node_start.png", "node_stop.png", "node_normal1.png", "node_sign1.png", "node_mail.png", "node_dll.png", "node_and.png", "node_xor.png", "node_block.png", "node_subWorkflow.png", "node_auto.png" };
            string[] strArrays2 = strArrays;
            strArrays = new string[] { "node_start.png", "node_stop.png", "node_normal2.png", "node_sign2.png", "node_mail.png", "node_dll.png", "node_and.png", "node_xor.png", "node_block.png", "node_subWorkflow.png", "node_auto.png" };
            string[] strArrays3 = strArrays;
            DriverEngine driverEngine = new DriverEngine(base.UserInfo);
            this.defModel = driverEngine.GetInstanceById(this.InstanceId);
        
            (new XmlDocument()).LoadXml(this.defModel.XPDL);
            Package packageFromText = XpdlModel.GetPackageFromText(this.defModel.XPDL);
        

            WorkflowProcess workflowProcessById = packageFromText.GetWorkflowProcesses().GetWorkflowProcessById("1");
            foreach (Activity activity in workflowProcessById.GetActivities().GetActivities())
            {
                extendedAttributeByName1 = activity.GetExtendedAttributes().GetExtendedAttributeByName("Coordinates");
                int num2 = int.Parse(extendedAttributeByName1.GetValueByName("xPos"));
                int num3 = int.Parse(extendedAttributeByName1.GetValueByName("yPos"));
                num1 = Convert.ToInt32(activity.GetIcon());
                if (num2 > this.int_0)
                {
                    this.int_0 = num2;
                }
                if (num3 <= this.int_1)
                {
                    continue;
                }
                this.int_1 = num3;
     
            }
            _FinishTransition __FinishTransition = new _FinishTransition();
            List<FinishTransition> modelList = __FinishTransition.GetModelList(string.Concat("instanceId='", this.InstanceId, "'"));
            StringCollection stringCollections = new StringCollection();
             foreach (Transition transition in workflowProcessById.GetTransitions().GetTransitions())
            {
                extendedAttributeByName1 = transition.GetExtendedAttributes().GetExtendedAttributeByName("Coordinates");
                transition.GetName();
                   if (modelList.FindIndex((FinishTransition finishTransition_0) => finishTransition_0.TransitionId == transition.GetId()) > -1)
                {
                    string value = transition.GetExtendedAttributes().GetExtendedAttributeByName("From").GetValue();
                    string str = transition.GetExtendedAttributes().GetExtendedAttributeByName("To").GetValue();
                    Activity activityById = driverEngine.GetActivityById(this.defModel, value);
                    Activity activityById1 = driverEngine.GetActivityById(this.defModel, str);
                     if ((activityById.GetNodeType() == ActivityType.And ? true : activityById.GetNodeType() == ActivityType.Xor))
                    {
                        item = (TransitionRestriction)activityById.GetTransitionRestrictions().GetTransitionRestrictions()[0];
                        foreach (TransitionRef transitionRefA in item.GetJoin().GetTransitionRefs().GetTransitionRefs())
                        {
                            stringCollections.Add(transitionRefA.GetId());
                        }
                    }
                    if ((activityById1.GetNodeType() == ActivityType.And ? true : activityById1.GetNodeType() == ActivityType.Xor))
                    {
                        item = (TransitionRestriction)activityById1.GetTransitionRestrictions().GetTransitionRestrictions()[0];
                        foreach (TransitionRef transitionRef1 in item.GetSplit().GetTransitionRefs().GetTransitionRefs())
                        {
                            stringCollections.Add(transitionRef1.GetId());
                        }
                    }
                }
                for (i = 0; i < 5; i++)
                {
                    extendedAttributeByName = transition.GetExtendedAttributes().GetExtendedAttributeByName(string.Concat("p", i.ToString()));
                    num = i + 1;
                    extendedAttribute = transition.GetExtendedAttributes().GetExtendedAttributeByName(string.Concat("p", num.ToString()));
                    point = new Point(Convert.ToInt32(extendedAttributeByName.GetValueByName("xPos")), Convert.ToInt32(extendedAttributeByName.GetValueByName("yPos")));
                    point1 = new Point(Convert.ToInt32(extendedAttribute.GetValueByName("xPos")), Convert.ToInt32(extendedAttribute.GetValueByName("yPos")));
                    if (Math.Max(point.X, point1.X) > this.int_0)
                    {
                        this.int_0 = Math.Max(point.X, point1.X);
                    }
                    if (Math.Max(point.Y, point1.Y) > this.int_1)
                    {
                        this.int_1 = Math.Max(point.Y, point1.Y);
                    }
                }
            }
            GetInstanceImg int0 = this;
            int0.int_0 = int0.int_0 + 60;
            GetInstanceImg int1 = this;
            int1.int_1 = int1.int_1 + 60;
            Bitmap bitmap = new Bitmap(this.int_0, this.int_1);
            Graphics graphic = Graphics.FromImage(bitmap);
            graphic.Clear(Color.White);
            Image image = null;
            string str1 = "";
            Font font = new Font("宋体", 10f);
            int num4 = 0;
            int num5 = 0;
            string name = "";
            foreach (Activity activity1 in workflowProcessById.GetActivities().GetActivities())
            {
                int activityState = TaskService.GetActivityState(this.InstanceId, activity1.GetId());
                name = activity1.GetName();
                activity1.GetId();
                num1 = Convert.ToInt32(activity1.GetIcon());
                str1 = strArrays1[num1];
                if (activityState == 1)
                {
                    str1 = strArrays2[num1];
                }
                else if (activityState == 2)
                {
                    str1 = strArrays3[num1];
                }
                image = Image.FromFile(string.Concat(EIS.AppBase.Utility.GetPhysicalRootPath(), "\\Img\\Workflow\\", str1));
                extendedAttributeByName1 = activity1.GetExtendedAttributes().GetExtendedAttributeByName("Coordinates");
                num4 = int.Parse(extendedAttributeByName1.GetValueByName("xPos"));
                num5 = int.Parse(extendedAttributeByName1.GetValueByName("yPos"));
                graphic.DrawString(name, font, new SolidBrush(Color.FromArgb(1, 132, 1)), (float)(num4 - (name.Length - 3) * 6), (float)(num5 + 40));
                graphic.DrawImage(image, num4 + 2, num5 + 2, this.int_2, this.int_3);
              }
            foreach (Transition transition1 in workflowProcessById.GetTransitions().GetTransitions())
            {
                extendedAttributeByName1 = transition1.GetExtendedAttributes().GetExtendedAttributeByName("Coordinates");
                string name1 = transition1.GetName();
                 graphic.SmoothingMode = SmoothingMode.AntiAlias;
                Pen pen = new Pen(Color.Gray, 1f);
                if ((modelList.FindIndex((FinishTransition finishTransition_0) => finishTransition_0.TransitionId == transition1.GetId()) > -1 ? true : stringCollections.Contains(transition1.GetId())))
                {
                    pen = new Pen(Color.Blue, (float)this.int_4);
                }
                pen.LineJoin = LineJoin.Round;
                int num6 = 0;
                int[] numArray = new int[6];
                Point x = new Point();
                Point[] pointArray = new Point[7];
                for (i = 0; i < 5; i++)
                {
                    extendedAttributeByName = transition1.GetExtendedAttributes().GetExtendedAttributeByName(string.Concat("p", i.ToString()));
                    num = i + 1;
                    extendedAttribute = transition1.GetExtendedAttributes().GetExtendedAttributeByName(string.Concat("p", num.ToString()));
                    point = new Point(Convert.ToInt32(extendedAttributeByName.GetValueByName("xPos")), Convert.ToInt32(extendedAttributeByName.GetValueByName("yPos")));
                    point1 = new Point(Convert.ToInt32(extendedAttribute.GetValueByName("xPos")), Convert.ToInt32(extendedAttribute.GetValueByName("yPos")));
                    pointArray[i] = point;
                    if (i == 4)
                    {
                        pen.CustomEndCap = new AdjustableArrowCap(4f, 6f);
                    }
                    graphic.DrawLine(pen, point, point1);
                    if (point.X != point1.X)
                    {
                        int num7 = num6;
                        num6 = num7 + 1;
                        numArray[num7] = Math.Max(point1.X, point.X) - Math.Min(point1.X, point.X);
                    }
                    else if (point.Y != point1.Y)
                    {
                        int num8 = num6;
                        num6 = num8 + 1;
                        numArray[num8] = Math.Max(point1.Y, point.Y) - Math.Min(point1.Y, point.Y);
                    }
                }
                if (num6 == 5)
                {
                    point2 = pointArray[2];
                    point3 = pointArray[3];
                    x.X = point2.X + (point3.X - point2.X) / 2;
                    x.Y = point2.Y + (point3.Y - point2.Y) / 2;
                }
                else if (num6 == 4)
                {
                    point2 = pointArray[1];
                    point3 = pointArray[2];
                    x.X = point2.X + (point3.X - point2.X) / 2;
                    x.Y = point2.Y + (point3.Y - point2.Y) / 2;
                }
                else if (num6 == 3)
                {
                    point2 = pointArray[1];
                    point3 = pointArray[2];
                    x.X = point2.X + (point3.X - point2.X) / 2;
                    x.Y = point2.Y + (point3.Y - point2.Y) / 2;
                }
                else if (num6 == 2)
                {
                    point2 = pointArray[0];
                    point3 = pointArray[1];
                    x.X = point2.X + (point3.X - point2.X) / 2;
                    x.Y = point2.Y + (point3.Y - point2.Y) / 2;
                }
                else if (num6 == 1)
                {
                    point2 = pointArray[0];
                    point3 = pointArray[1];
                    x.X = point2.X + (point3.X - point2.X) / 2;
                    x.Y = point2.Y + (point3.Y - point2.Y) / 2;
                }
                if (name1 != "")
                {
                    int x1 = x.X;
                    int y = x.Y;
                    SizeF sizeF = graphic.MeasureString(name1, new Font("宋体", 10f));
                    PointF pointF = new PointF((float)x1 - sizeF.Width / 2f, (float)y - sizeF.Height / 2f);
                    graphic.FillRectangle(new SolidBrush(Color.White), new RectangleF(pointF, sizeF));
                    PointF pointF1 = new PointF((float)x1 - sizeF.Width / 2f, (float)y - sizeF.Height / 2f + 2f);
                    graphic.DrawString(name1, new Font("宋体", 10f), new SolidBrush(Color.Black), pointF1);
                }
                pen.Dispose();
            }
            MemoryStream memoryStream = new MemoryStream();
            try
            {
                bitmap.Save(memoryStream, ImageFormat.Png);
                 base.Response.OutputStream.Write(memoryStream.GetBuffer(), 0, (int)memoryStream.Length);
            }
            finally
            {
                if (memoryStream != null)
                {
                    ((IDisposable)memoryStream).Dispose();
                }
            }
            base.Response.ContentType = "Application/octet-stream";
            base.Response.End();
            } catch(Exception ex)
            {
              
                fileLogger.Error("GetInstanceImg:" + ex.ToString());
            }

        }
    }
}