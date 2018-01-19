using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using System;
using System.Configuration;
using System.Runtime.CompilerServices;

namespace EIS.WorkFlow.Engine
{
	internal class WFListener
	{
		public static IUnityContainer Unity_WorkflowContainer
		{
			get;
			set;
		}

		static WFListener()
		{
			IUnityContainer unityContainer = new UnityContainer();
			string str = string.Concat(AppDomain.CurrentDomain.BaseDirectory, "unity.config");
			ExeConfigurationFileMap exeConfigurationFileMap = new ExeConfigurationFileMap()
			{
				ExeConfigFilename = str
			};
			Configuration configuration = ConfigurationManager.OpenMappedExeConfiguration(exeConfigurationFileMap, ConfigurationUserLevel.None);
			UnityConfigurationSection section = (UnityConfigurationSection)configuration.GetSection("unity");
			section.Containers["Workflow"].Configure(unityContainer);
			WFListener.Unity_WorkflowContainer = unityContainer;
		}

		public WFListener()
		{
		}
	}
}