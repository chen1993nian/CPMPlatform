using EIS.WorkFlow.Access;
using EIS.WorkFlow.Model;
using System;
using System.Collections.Generic;

namespace EIS.WorkFlow.Service
{
	public class InstanceReferService
	{
		public InstanceReferService()
		{
		}

		public static List<InstanceRefer> GetInstanceRefer(string instanceId)
		{
			_InstanceRefer __InstanceRefer = new _InstanceRefer();
			List<InstanceRefer> modelList = __InstanceRefer.GetModelList(string.Concat("instanceId='", instanceId, "'"));
			return modelList;
		}
	}
}