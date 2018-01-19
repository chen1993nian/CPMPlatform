using EIS.WorkFlow.Access;
using EIS.WorkFlow.Model;
using System;
using System.Collections.Generic;

namespace EIS.WorkFlow.Service
{
	public class IdeaTemplateService
	{
		public IdeaTemplateService()
		{
		}

		public static List<IdeaTemplate> GetIdeaTemplateByEmployeeId(string empId)
		{
			_IdeaTemplate __IdeaTemplate = new _IdeaTemplate();
			List<IdeaTemplate> modelList = __IdeaTemplate.GetModelList(string.Concat("IdeaUser='", empId, "'"));
			return modelList;
		}
	}
}