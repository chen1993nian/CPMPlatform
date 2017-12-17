using EIS.AppBase;
using EIS.WorkFlow.Model;
using EIS.WorkFlow.Service;
using EIS.WorkFlow.XPDLParser;
using EIS.WorkFlow.XPDLParser.Elements;
using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Xml;

namespace EIS.Web.SysFolder.WorkFlow
{
    public partial class GetWorkflowImg : PageBase
    {
        private int int_0 = 0;

        private int int_1 = 0;

        public string workflowId = "";

        public string WorkflowName = "";

        public string Creator = "";

        private int int_2 = 32;

        private int int_3 = 32;

        private string string_0 = "#4677BF";

        private int int_4 = 1;

        public Define defModel = new Define();

     

        protected void Page_Load(object sender, EventArgs e)
        {

            Point point;
            Point point1;
            Point point2;
            Point point3;
            int i;
            Transition transition = null;
            ExtendedAttribute extendedAttributeByName;
            ExtendedAttribute extendedAttribute;
            int num;
            ExtendedAttribute extendedAttributeByName1;
            this.workflowId = base.GetParaValue("workflowId");
            this.defModel = DefineService.GetWorkflowDefineModelById(this.workflowId);
            this.WorkflowName = this.defModel.WorkflowName;
            (new XmlDocument()).LoadXml(this.defModel.XPDL);
            Package packageFromText = XpdlModel.GetPackageFromText(this.defModel.XPDL);
            WorkflowProcess workflowProcessById = packageFromText.GetWorkflowProcesses().GetWorkflowProcessById("1");
            foreach (Activity activity in workflowProcessById.GetActivities().GetActivities())
            {
                extendedAttributeByName = activity.GetExtendedAttributes().GetExtendedAttributeByName("Coordinates");
                int num1 = int.Parse(extendedAttributeByName.GetValueByName("xPos"));
                int num2 = int.Parse(extendedAttributeByName.GetValueByName("yPos"));
                Convert.ToInt32(activity.GetIcon());
                if (num1 > this.int_0)
                {
                    this.int_0 = num1;
                }
                if (num2 <= this.int_1)
                {
                    continue;
                }
                this.int_1 = num2;
            }
            foreach (Transition transitionA in workflowProcessById.GetTransitions().GetTransitions())
            {
                extendedAttributeByName = transitionA.GetExtendedAttributes().GetExtendedAttributeByName("Coordinates");
            //    transition.GetName();
                for (i = 0; i < 5; i++)
                {
                    extendedAttribute = transitionA.GetExtendedAttributes().GetExtendedAttributeByName(string.Concat("p", i.ToString()));
                    num = i + 1;
                    extendedAttributeByName1 = transitionA.GetExtendedAttributes().GetExtendedAttributeByName(string.Concat("p", num.ToString()));
                    point = new Point(Convert.ToInt32(extendedAttribute.GetValueByName("xPos")), Convert.ToInt32(extendedAttribute.GetValueByName("yPos")));
                    point1 = new Point(Convert.ToInt32(extendedAttributeByName1.GetValueByName("xPos")), Convert.ToInt32(extendedAttributeByName1.GetValueByName("yPos")));
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
            GetWorkflowImg int0 = this;
            int0.int_0 = int0.int_0 + 60;
            GetWorkflowImg int1 = this;
            int1.int_1 = int1.int_1 + 60;
            string[] strArrays = new string[] { "node_start.png", "node_stop.png", "node_normal.png", "node_sign.png", "node_mail.png", "node_dll.png", "node_and.png", "node_xor.png", "node_block.png", "node_subWorkflow.png", "node_auto.png" };
            string[] strArrays1 = strArrays;
            Bitmap bitmap = new Bitmap(this.int_0, this.int_1);
            Graphics graphic = Graphics.FromImage(bitmap);
            graphic.Clear(Color.White);
            Image image = null;
            string str = "";
            Font font = new Font("宋体", 10f);
            int num3 = 0;
            int num4 = 0;
            string name = "";
            foreach (Activity activity1 in workflowProcessById.GetActivities().GetActivities())
            {
                name = activity1.GetName();
              //  activity1.GetId();
                str = strArrays1[Convert.ToInt32(activity1.GetIcon())];
                image = Image.FromFile(string.Concat(Utility.GetPhysicalRootPath(), "\\Img\\Workflow\\", str));
                extendedAttributeByName = activity1.GetExtendedAttributes().GetExtendedAttributeByName("Coordinates");
                num3 = int.Parse(extendedAttributeByName.GetValueByName("xPos"));
                num4 = int.Parse(extendedAttributeByName.GetValueByName("yPos"));
                graphic.DrawString(name, font, new SolidBrush(Color.FromArgb(1, 132, 1)), (float)(num3 - (name.Length - 3) * 6), (float)(num4 + 40));
                graphic.DrawImage(image, num3 + 2, num4 + 2, this.int_2, this.int_3);
            }
            foreach (Transition transition1 in workflowProcessById.GetTransitions().GetTransitions())
            {
                extendedAttributeByName = transition1.GetExtendedAttributes().GetExtendedAttributeByName("Coordinates");
                string name1 = transition1.GetName();
                graphic.SmoothingMode = SmoothingMode.AntiAlias;
                Pen pen = new Pen(Color.Blue, (float)this.int_4)
                {
                    LineJoin = LineJoin.Round
                };
                int num5 = 0;
                int[] numArray = new int[6];
                Point x = new Point();
                Point[] pointArray = new Point[7];
                for (i = 0; i < 5; i++)
                {
                    extendedAttribute = transition1.GetExtendedAttributes().GetExtendedAttributeByName(string.Concat("p", i.ToString()));
                    num = i + 1;
                    extendedAttributeByName1 = transition1.GetExtendedAttributes().GetExtendedAttributeByName(string.Concat("p", num.ToString()));
                    point = new Point(Convert.ToInt32(extendedAttribute.GetValueByName("xPos")), Convert.ToInt32(extendedAttribute.GetValueByName("yPos")));
                    point1 = new Point(Convert.ToInt32(extendedAttributeByName1.GetValueByName("xPos")), Convert.ToInt32(extendedAttributeByName1.GetValueByName("yPos")));
                    pointArray[i] = point;
                    if (i == 4)
                    {
                        pen.CustomEndCap = new AdjustableArrowCap(4f, 6f);
                    }
                    graphic.DrawLine(pen, point, point1);
                    if (point.X != point1.X)
                    {
                        int num6 = num5;
                        num5 = num6 + 1;
                        numArray[num6] = Math.Max(point1.X, point.X) - Math.Min(point1.X, point.X);
                    }
                    else if (point.Y != point1.Y)
                    {
                        int num7 = num5;
                        num5 = num7 + 1;
                        numArray[num7] = Math.Max(point1.Y, point.Y) - Math.Min(point1.Y, point.Y);
                    }
                }
                if (num5 == 5)
                {
                    point2 = pointArray[2];
                    point3 = pointArray[3];
                    x.X = point2.X + (point3.X - point2.X) / 2;
                    x.Y = point2.Y + (point3.Y - point2.Y) / 2;
                }
                else if (num5 == 4)
                {
                    point2 = pointArray[1];
                    point3 = pointArray[2];
                    x.X = point2.X + (point3.X - point2.X) / 2;
                    x.Y = point2.Y + (point3.Y - point2.Y) / 2;
                }
                else if (num5 == 3)
                {
                    point2 = pointArray[1];
                    point3 = pointArray[2];
                    x.X = point2.X + (point3.X - point2.X) / 2;
                    x.Y = point2.Y + (point3.Y - point2.Y) / 2;
                }
                else if (num5 == 2)
                {
                    point2 = pointArray[0];
                    point3 = pointArray[1];
                    x.X = point2.X + (point3.X - point2.X) / 2;
                    x.Y = point2.Y + (point3.Y - point2.Y) / 2;
                }
                else if (num5 == 1)
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
        }
    }
}