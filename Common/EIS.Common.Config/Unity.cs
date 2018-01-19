using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using System;
using System.Configuration;
using System.Runtime.CompilerServices;

namespace EIS.Common.Config
{
	public class Unity
	{
		public static IUnityContainer Unity_BaseContainer
		{
			get;
			set;
		}

		public static IUnityContainer Unity_WorkflowContainer
		{
			get;
			set;
		}

		static Unity()
		{
			string str = string.Concat(AppDomain.CurrentDomain.BaseDirectory, "unity.config");
			ExeConfigurationFileMap exeConfigurationFileMap = new ExeConfigurationFileMap()
			{
				ExeConfigFilename = str
			};
			Configuration configuration = ConfigurationManager.OpenMappedExeConfiguration(exeConfigurationFileMap, ConfigurationUserLevel.None);
			UnityConfigurationSection section = (UnityConfigurationSection)configuration.GetSection("unity");
			IUnityContainer unityContainer = new UnityContainer();
			section.Containers["Workflow"].Configure(unityContainer);
			Unity.Unity_WorkflowContainer = unityContainer;
			IUnityContainer unityContainer1 = new UnityContainer();
			section.Containers["Base"].Configure(unityContainer);
			Unity.Unity_BaseContainer = unityContainer1;
		}

		public Unity()
		{
		}
	}
}