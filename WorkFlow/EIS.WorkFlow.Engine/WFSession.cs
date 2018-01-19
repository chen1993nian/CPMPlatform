using EIS.AppBase;
using EIS.WorkFlow.Model;
using EIS.WorkFlow.XPDLParser.Elements;
using System;
using System.Data;
using System.Runtime.CompilerServices;

namespace EIS.WorkFlow.Engine
{
	public class WFSession
	{
		public DataRow AppData
		{
			get;
			set;
		}

		public Activity CurActivity
		{
			get;
			set;
		}

		public Instance CurInstance
		{
			get;
			set;
		}

		public UserContext UserInfo
		{
			get;
			set;
		}

		public WFSession()
		{
		}

		public WFSession(UserContext user, Instance ins, DataRow AppData, Activity curAct)
		{
			this.UserInfo = user;
			this.CurInstance = ins;
			this.AppData = AppData;
			this.CurActivity = curAct;
		}
	}
}