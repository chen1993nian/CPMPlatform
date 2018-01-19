using EIS.Permission.Access;
using EIS.Permission.Model;
using System;

namespace EIS.Permission.Service
{
	public class RoleCatalogService
	{
		public RoleCatalogService()
		{
		}

		public static string GetNewWbs(string rolePWBS)
		{
			return (new _RoleCatalog()).GetNewCode(rolePWBS);
		}

		public static RoleCatalog GetTop()
		{
			return (new _RoleCatalog()).GetModelByWbs("0");
		}
	}
}