using EIS.WorkFlow.Access;
using System;
using System.Collections.Generic;
using System.Data.Common;

namespace EIS.WorkFlow.Service
{
	public class FinishTransitionService
	{
		public FinishTransitionService()
		{
		}

		public static void ClearFinishSplitTransition(string instanceId, string fromActId, DbTransaction dbTran)
		{
			(new _FinishTransition(dbTran)).DeleteByFromActId(instanceId, fromActId);
		}

		public static int GetFinishJoinTransitionCount(string instanceId, string toActId, DbTransaction dbTran)
		{
			_FinishTransition __FinishTransition = new _FinishTransition(dbTran);
			string str = string.Format(" InstanceId='{0}' and ToActivity='{1}' ", instanceId, toActId);
			return __FinishTransition.GetModelList(str).Count;
		}

		public static int GetFinishJoinTransitionCount(string instanceId, string toActId)
		{
			_FinishTransition __FinishTransition = new _FinishTransition();
			string str = string.Format(" InstanceId='{0}' and ToActivity='{1}' ", instanceId, toActId);
			return __FinishTransition.GetModelList(str).Count;
		}

		public static int GetFinishSplitTransitionCount(string instanceId, string fromActId, DbTransaction dbTran)
		{
			_FinishTransition __FinishTransition = new _FinishTransition(dbTran);
			string str = string.Format(" InstanceId='{0}' and FromActivity='{1}' ", instanceId, fromActId);
			return __FinishTransition.GetModelList(str).Count;
		}

		public static int GetFinishSplitTransitionCount(string instanceId, string fromActId)
		{
			_FinishTransition __FinishTransition = new _FinishTransition();
			string str = string.Format(" InstanceId='{0}' and FromActivity='{1}' ", instanceId, fromActId);
			return __FinishTransition.GetModelList(str).Count;
		}
	}
}