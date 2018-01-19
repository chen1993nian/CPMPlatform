using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Xml;
using System.Xml.Serialization;

namespace EIS.Interface.WebService
{
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[GeneratedCode("System.Xml", "4.0.30319.1")]
	[Serializable]
	[XmlType(Namespace="http://tempuri.org/")]
	public class ServiceCredential : INotifyPropertyChanged
	{
		private string userNameField;

		private string passWordField;

		private XmlAttribute[] anyAttrField;

		[XmlAnyAttribute]
		public XmlAttribute[] AnyAttr
		{
			get
			{
				return this.anyAttrField;
			}
			set
			{
				this.anyAttrField = value;
				this.RaisePropertyChanged("AnyAttr");
			}
		}

		[XmlElement(Order=1)]
		public string PassWord
		{
			get
			{
				return this.passWordField;
			}
			set
			{
				this.passWordField = value;
				this.RaisePropertyChanged("PassWord");
			}
		}

		[XmlElement(Order=0)]
		public string UserName
		{
			get
			{
				return this.userNameField;
			}
			set
			{
				this.userNameField = value;
				this.RaisePropertyChanged("UserName");
			}
		}

		public ServiceCredential()
		{
		}

		protected void RaisePropertyChanged(string propertyName)
		{
			PropertyChangedEventHandler propertyChangedEventHandler = this.PropertyChanged;
			if (propertyChangedEventHandler != null)
			{
				propertyChangedEventHandler(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;
	}
}