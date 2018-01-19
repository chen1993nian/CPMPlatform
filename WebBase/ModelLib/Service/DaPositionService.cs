using EIS.Web.ModelLib.Access;
using EIS.Web.ModelLib.Model;
using System;
using System.Collections.Generic;

namespace EIS.Web.ModelLib.Service
{
	public class DaPositionService
	{
		public DaPositionService()
		{
		}

		public static List<DaPosition> GetAllFolder()
		{
			return (new _DaPosition()).GetModelList("");
		}
	}
}